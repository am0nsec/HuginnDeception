using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Huginn.ADObjects.Auxiliary {

    /// <summary>
    /// Contains the security information for an object.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-securityprincipal
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public sealed class SecurityPrincipal {

        #region Attributes

        /// <summary>
        /// The length of time that the account has been active.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-accountnamehistory
        /// </summary>
        public List<string>? AccountNameHistory { get; set; }

        /// <summary>
        /// Contains mappings for X.509 certificates or external Kerberos user accounts to this user for the purpose of authentication.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-altsecurityidentities
        /// </summary>
        public List<string>? AltSecurityIdentities { get; set; }

        /// <summary>
        /// The Kerberos version number of the current key for this account. This is a constructed attribute.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-keyversionnumber
        /// </summary>
        public Int32? MSDSKeyVersionNumber { get; set; }

        /// <summary>
        /// A binary value that specifies the security identifier (SID) of the user. The SID is a unique value used to identify the user as a security principal.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-objectsid
        /// </summary>
        public SecurityIdentifier? ObjectSid { get; set; }

        /// <summary>
        /// The relative Identifier of an object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-rid
        /// </summary>
        public Int32? Rid { get; set; }

        /// <summary>
        /// The logon name used to support clients and servers running earlier versions of the operating system,
        /// such as Windows NT 4.0, Windows 95, Windows 98, and LAN Manager.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-samaccountname
        /// </summary>
        public string? SAMAccountName { get; set; }

        /// <summary>
        /// This attribute contains information about every account type object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-samaccounttype
        /// </summary>
        public SAM_ACCOUNT_TYPE? SAMAccountType { get; set; }

        /// <summary>
        /// A unique value of variable length used to identify a user account, group account, or logon session to which an ACE applies.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-securityidentifier
        /// </summary>
        public SecurityIdentifier? SecurityIdentifier { get; set; }

        /// <summary>
        /// Contains previous SIDs used for the object if the object was moved from another domain.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-sidhistory
        /// </summary>
        public List<SecurityIdentifier>? SIDHistory { get; set; }

        /// <summary>
        /// Stored credentials for use in authenticating. The encrypted version of the user's password.
        /// This attribute is neither readable nor writable.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-supplementalcredentials
        /// </summary>
        public List<byte[]>? SupplementalCredentials { get; set; }

        /// <summary>
        /// A computed attribute that contains the list of SIDs due to a transitive group membership expansion operation on a given user or computer.
        /// Token Groups cannot be retrieved if no Global Catalog is present to retrieve the transitive reverse memberships.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-tokengroups
        /// </summary>
        public List<SecurityIdentifier>? TokenGroups { get; set; }

        /// <summary>
        /// Token groups for Exchange
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-tokengroupsglobalanduniversal
        /// </summary>
        public List<SecurityIdentifier>? TokenGroupsGlobalAndUniversal { get; set; }

        /// <summary>
        /// This attribute contains the list of SIDs due to a transitive group membership expansion operation on a given user or computer.
        /// Token groups cannot be retrieved if a Global Catalog is not present to retrieve the transitive reverse memberships.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-tokengroupsnogcacceptable
        /// </summary>
        public List<SecurityIdentifier>? TokenGroupsNoGCAcceptable { get; set; }

        #endregion

        /// <summary>
        /// SAM-Account-Type Attribute values.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-samaccounttype
        /// </summary>
        public enum SAM_ACCOUNT_TYPE : Int32 {
            SAM_DOMAIN_OBJECT             = 0x00,
            SAM_GROUP_OBJECT              = 0x10000000,
            SAM_NON_SECURITY_GROUP_OBJECT = 0x10000001,
            SAM_ALIAS_OBJECT              = 0x20000000,
            SAM_NON_SECURITY_ALIAS_OBJECT = 0x20000001,
            SAM_USER_OBJECT               = 0x30000000,
            SAM_NORMAL_USER_ACCOUNT       = 0x30000000,
            SAM_MACHINE_ACCOUNT           = 0x30000001,
            SAM_TRUST_ACCOUNT             = 0x30000002,
            SAM_APP_BASIC_GROUP           = 0x40000000,
            SAM_APP_QUERY_GROUP           = 0x40000001,
            SAM_ACCOUNT_TYPE_MAX          = 0x7fffffff
        }

        /// <summary>
        /// List if attributes to extract when querying Security-Principal object.
        /// </summary>
        public static string[] Attributes {
            get {
                return new string[] {
                    "accountNameHistory",
                    "altSecurityIdentities",
                    "msDS-KeyVersionNumber",
                    "objectSid",
                    "rid",
                    "sAMAccountName",
                    "sAMAccountType",
                    "securityIdentifier",
                    "sIDHistory",
                    "supplementalCredentials",
                    //"tokenGroups",
                    //"tokenGroupsGlobalAndUniversal",
                    //"tokenGroupsNoGCAcceptable",
                };
            }
        }

        /// <summary>
        /// The LDAP display name of the Security-Principal class.
        /// </summary>
        public static readonly string[] _ClassNames = new string[] { "user", "computer", "group" };

        /// <summary>
        /// Find all Security-Principal objects within the current domain. 
        /// </summary>
        public static SecurityPrincipal? FindAll() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find a Security-Principal object within the current domain. 
        /// </summary>
        public static SecurityPrincipal? Find(string Name) {
            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Build the directory searcher
            var Entry       = new DirectoryEntry($"LDAP://{Dn}");
            var Searcher    = new DirectorySearcher(Entry);

            // Build the filter
            var Filter = string.Join(
                "",
                SecurityPrincipal._ClassNames.Select(x => $"(objectClass={x})")
            );
            Searcher.Filter = $"(&(|{Filter})(name={Name}))";

            // Add the list of the properties to load
            Searcher.PropertiesToLoad.AddRange(SecurityPrincipal.Attributes);

            // Search for the object
            try {
                var Result = Searcher.FindOne();
                if (Result == null) {
                    return null;
                }

                // Return the security principal.
                return new SecurityPrincipal().FromResult(Result);
            } catch (Exception ex) {
                Logger.Verbose(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public SecurityPrincipal FromResult(SearchResult Result) {

            // Get Security-Principal object attributes.
            this.AccountNameHistory      = Util.GetListOfTypeFromResult<string>(Result    , "accountNameHistory");
            this.AltSecurityIdentities   = Util.GetListOfTypeFromResult<string>(Result    , "altSecurityIdentities");
            this.MSDSKeyVersionNumber    = Util.GetTypeFromResult<int>(Result             , "msDS-KeyVersionNumber");
            this.ObjectSid               = Util.GetSecIdentifierFromResult(Result         , "objectSid");
            this.Rid                     = Util.GetTypeFromResult<int>(Result             , "rid");
            this.SAMAccountName          = Util.GetTypeFromResult<string>(Result          , "sAMAccountName");
            this.SAMAccountType          = Util.GetTypeFromResult<SAM_ACCOUNT_TYPE>(Result, "sAMAccountType");
            this.SecurityIdentifier      = Util.GetSecIdentifierFromResult(Result         , "securityIdentifier");
            this.SIDHistory              = Util.GetListOfSecIdentifierFromResult(Result   , "sIDHistory");
            this.SupplementalCredentials = Util.GetListOfTypeFromResult<byte[]>(Result    , "supplementalCredentials");
            // TokenGroups
            // TokenGroupsGlobalAndUniversal
            // TokenGroupsNoGCAcceptable

            // Success
            return this;
        }
    }
}
