
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Xml.Linq;
using Huginn.ADObjects.Auxiliary;

namespace Huginn.ADObjects.Structural {

    /// <summary>
    /// Stores a list of user names. Used to apply security principals on resources.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-group
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class Group : Top {

        #region Attributes

        /// <summary>
        /// Indicates that a given object has had its ACLs changed to a more secure value by the system because it was a member of one of the administrative groups (directly or transitively).
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-admincount
        /// </summary>
        public Int32? AdminCount { get; set; }

        /// <summary>
        /// Used by DS Security to determine which users can perform specific operations on the host object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-controlaccessrights
        /// </summary>
        public List<byte[]>? ControlAccessRights { get; set; }

        /// <summary>
        /// The location of the desktop profile for a user or group of users. Not used.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-desktopprofile
        /// </summary>
        public string? DesktopProfile { get; set; }

        /// <summary>
        /// The list of email addresses for a contact.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mail
        /// </summary>
        public string? EmailAddresses { get; set; }

        /// <summary>
        /// The Group-Attributes attribute is not currently used.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-groupattributes
        /// </summary>
        public Int32? GroupAttributes { get; set; }

        /// <summary>
        /// Windows NT Security. Down level Windows NT support.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-groupmembershipsam
        /// </summary>
        public byte[]? GroupMembershipSAM { get; set; }

        /// <summary>
        /// Contains a set of flags that define the type and scope of a group object. For the possible values for this attribute, see Remarks.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-grouptype
        /// </summary>
        public Int32? GroupType { get; set; }

        /// <summary>
        /// The distinguished name of the user that is assigned to manage this object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-managedby
        /// </summary>
        public string? ManagedBy { get; set; }

        /// <summary>
        /// The list of users that belong to the group.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-member
        /// </summary>
        public List<string>? Member { get; set; }

        /// <summary>
        /// Nonsecurity members of a group. Used for Exchange distribution lists.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-nonsecuritymember
        /// </summary>
        public List<string>? NonSecurityMember { get; set; }

        /// <summary>
        /// This attribute is not used.
        /// The member attribute is used to contain the members of a group.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-ntgroupmembers
        /// </summary>
        public List<byte[]>? NTGroupMembers { get; set; }

        /// <summary>
        /// Operator count.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-operatorcount
        /// </summary>
        public Int32? OperatorCount { get; set; }

        /// <summary>
        /// A computed attribute that is used in retrieving the membership list of a group, such as Domain Users.
        /// The complete membership of such groups is not stored explicitly for scaling reasons.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-primarygrouptoken
        /// </summary>
        public Int32? PrimaryGroupToken { get; set; }

        #endregion

        /// <summary>
        /// Security-Principal auxiliary class.
        /// </summary>
        public SecurityPrincipal Principal { get; set; } = new SecurityPrincipal();

        /// <summary>
        /// List if attributes to extract when querying Group object.
        /// </summary>
        protected static new string[] Attributes {
            get {
                return Top.Attributes.Concat(new string[] {
                    "adminCount",
                    "controlAccessRights",
                    "desktopProfile",
                    "mail",
                    "groupAttributes",
                    "groupMembershipSAM",
                    "groupType",
                    "managedBy",
                    "member",
                    "nonSecurityMember",
                    "nTGroupMembers",
                    "operatorCount"
                }).ToArray();
            }
        }

        /// <summary>
        /// The LDAP display name of the Group class.
        /// </summary>
        public static readonly string _ClassName = "group";

        #region Static Methods

        /// <summary>
        /// Find a group object within the current domain. 
        /// </summary>
        public static Group? Find(string Name) {
            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Build the directory searcher
            var Entry       = new DirectoryEntry($"LDAP://{Dn}");
            var Searcher    = new DirectorySearcher(Entry);
            Searcher.Filter = $"(&(objectClass={_ClassName})(name={Name}))";

            // Add the list of the properties to load
            string[] AllAttributes = [];
            AllAttributes = AllAttributes.Concat(Group.Attributes).ToArray();
            AllAttributes = AllAttributes.Concat(SecurityPrincipal.Attributes).ToArray();

            Searcher.PropertiesToLoad.AddRange(AllAttributes);

            // Search for the object
            try {
                var Result = Searcher.FindOne();
                if (Result == null) {
                    return null;
                }

                // Return the security principal.
                return new Group().FromResult(Result);
            } catch (Exception ex) {
                Logger.Verbose(ex.ToString());
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public new Group FromResult(SearchResult Result) {

            // Get Top auxiliary attributes.
            base.FromResult(Result);

            // Get Security-Principal auxiliary attributes.
            this.Principal.FromResult(Result);

            // Get Group structural attributes.
            this.AdminCount          = Util.GetTypeFromResult<int>(Result         , "adminCount");
            this.ControlAccessRights = Util.GetListOfTypeFromResult<byte[]>(Result, "controlAccessRights");
            this.DesktopProfile      = Util.GetTypeFromResult<string>(Result      , "desktopProfile");
            this.EmailAddresses      = Util.GetTypeFromResult<string>(Result      , "mail");
            this.GroupAttributes     = Util.GetTypeFromResult<int>(Result         , "groupAttributes");
            this.GroupMembershipSAM  = Util.GetTypeFromResult<byte[]>(Result      , "groupMembershipSAM");
            this.GroupType           = Util.GetTypeFromResult<int>(Result         , "groupType");
            this.ManagedBy           = Util.GetTypeFromResult<string>(Result      , "managedBy");
            this.Member              = Util.GetListOfTypeFromResult<string>(Result, "member");
            this.NonSecurityMember   = Util.GetListOfTypeFromResult<string>(Result, "nonSecurityMember");
            this.OperatorCount       = Util.GetTypeFromResult<int>(Result         , "nTGroupMembers"); ;
            this.PrimaryGroupToken   = Util.GetTypeFromResult<int>(Result         , "operatorCount");

            // Sucess
            return this;
        }
    }
}
