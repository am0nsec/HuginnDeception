using Huginn.ADObjects.Auxiliary;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Huginn.ADObjects.Structural {


    /// <summary>
    /// This class represents a group managed service account in the domain.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-msds-groupmanagedserviceaccount
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class MSDSGroupManagedServiceAccount : Computer {

        #region Attributes

        /// <summary>
        /// This attribute is used for access checks to determine if a requestor has permission to retrieve the password for a group MSA.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-groupmsamembership
        /// </summary>
        public ActiveDirectorySecurity? MSDSGroupMSAMembership { get; set; }

        /// <summary>
        /// This constructed attribute returns a blob that contains the clear-text previous and current password, TimeUntilEpochExpires, 
        /// and TimeUntilNextScheduledUpdate for a group MSA.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-managedpassword
        /// </summary>
        public byte[]? MSDSManagedPassword { get; set; }

        /// <summary>
        /// This attribute contains the key identifier for the current managed password data for a group MSA.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-managedpasswordid
        /// </summary>
        public byte[]? MSDSManagedPasswordId { get; set; }

        /// <summary>
        /// This attribute is used to retrieve the number of days before a managed password is automatically changed for a group MSA.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-managedpasswordinterval
        /// </summary>
        public Int32? MSDSManagedPasswordInterval { get; set; }

        /// <summary>
        /// This constructed attribute contains the key identifier for the previous managed password data for a group MSA.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-managedpasswordpreviousid
        /// </summary>
        public byte[]? MSDSManagedPasswordPreviousId { get; set; }

        #endregion Attributes

        /// <summary>
        /// List if attributes to extract when querying Person object.
        /// </summary>
        protected static new string[] Attributes {
            get {
                return Computer.Attributes.Concat(new string[] {
                    "msDS-GroupMSAMembership",
                    //"msDS-ManagedPassword",
                    "msDS-ManagedPasswordId",
                    "msDS-ManagedPasswordInterval",
                    "msDS-ManagedPasswordPreviousId"
                }).ToArray();
            }
        }

        /// <summary>
        /// The LDAP display name of the PKI-Certificate-Template class.
        /// </summary>
        public static readonly string _ClassName = "msDS-GroupManagedServiceAccount";

        /// <summary>
        /// Get the list of group managed service accounts in the domain.
        /// </summary>
        public static List<MSDSGroupManagedServiceAccount> FindAll() {
            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Build the directory searcher
            var Entry = new DirectoryEntry($"LDAP://CN=Managed Service Accounts,{Dn}");
            var Searcher = new DirectorySearcher(Entry);
            Searcher.Filter = $"(objectClass={_ClassName})";

            // Add the list of the properties to load
            Searcher.PropertiesToLoad.AddRange(Attributes);

            // Get results
            var Gmsa = new List<MSDSGroupManagedServiceAccount>();
            foreach (SearchResult Result in Searcher.FindAll()) {
                Gmsa.Add(
                    new MSDSGroupManagedServiceAccount().FromResult(Result)
                );
            }

            // Cleanup
            Searcher.Dispose();
            Entry.Dispose();
            Dom.Dispose();

            // Success
            return Gmsa;
        }

        /// <summary>
        /// Find a MS-DS-Group-Managed-Service-Account object within the current domain. 
        /// </summary>
        public static new MSDSGroupManagedServiceAccount? Find(string Name) {
            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn = Util.NameToDistinguishedName(Dom.Name);

            // Build the directory searcher
            var Entry       = new DirectoryEntry($"LDAP://CN=Managed Service Accounts,{Dn}");
            var Searcher    = new DirectorySearcher(Entry);
            Searcher.Filter = $"(&(objectClass={_ClassName})(name={Name}))";

            // Add the list of the properties to load
            string[] AllAttributes = [];
            AllAttributes = AllAttributes.Concat(MSDSGroupManagedServiceAccount.Attributes).ToArray();
            AllAttributes = AllAttributes.Concat(SecurityPrincipal.Attributes).ToArray();

            Searcher.PropertiesToLoad.AddRange(AllAttributes);

            // Search for the object
            try {
                var Result = Searcher.FindOne();
                if (Result == null) {
                    return null;
                }

                // Return the security principal.
                return new MSDSGroupManagedServiceAccount().FromResult(Result);
            } catch (Exception ex) {
                Logger.Verbose(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public new MSDSGroupManagedServiceAccount FromResult(SearchResult Result) {

            // Get Top auxiliary attributes.
            base.FromResult(Result);

            // Get MS-DS-Group-Managed-Service-Account object attributes.
            this.MSDSGroupMSAMembership        = Util.GetSecDescriptorFromResult(Result, "msDS-GroupMSAMembership");
            this.MSDSManagedPassword           = Util.GetTypeFromResult<byte[]>(Result , "msDS-ManagedPassword");
            this.MSDSManagedPasswordId         = Util.GetTypeFromResult<byte[]>(Result , "msDS-ManagedPasswordId");
            this.MSDSManagedPasswordInterval   = Util.GetTypeFromResult<int>(Result    , "msDS-ManagedPasswordInterval");
            this.MSDSManagedPasswordPreviousId = Util.GetTypeFromResult<byte[]>(Result , "msDS-ManagedPasswordPreviousId");

            // Success
            return this;
        }

        /// <summary>
        /// Update one attribute of the object.
        /// </summary>
        /// <returns></returns>
        public bool UpdateAttribute(string AttributeName, object? AttributeValue) {

            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Make the container path
            string ContainerPath = $"LDAP://CN=Managed Service Accounts,{Dn}";
            string CNPath        = $"CN={CommonName}";

            // Open the container
            using (var Container = new DirectoryEntry(ContainerPath)) {

                // Update the user object
                using (var Object = Container.Children.Find($"{CNPath}", $"{_ClassName}")) {
                    if (Object == null) {
                        return false;
                    }

                    if (!Object.Properties.Contains(AttributeName)) {
                        Logger.Error("Invalid attribute supplied.");
                        return false;
                    }
                    SetAttribute(AttributeName, Object, AttributeValue);

                    // Save the object
                    try {
                        Object.CommitChanges();
                        return true;
                    } catch (Exception ex) {
                        Logger.Verbose(ex.ToString());
                        return false;
                    }
                }
            }
        }
    }
}
