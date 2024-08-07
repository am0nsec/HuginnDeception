
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace Huginn.ADObjects {

    /// <summary>
    /// This represents the Group Policy Object. It is used to define group polices.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-grouppolicycontainer
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public sealed class GroupPolicyContainer {

        #region Public Properties

        /// <summary>
        /// Path to the object in AD.
        /// </summary>
        public string ObjectPath { get; private set; } = String.Empty;

        /// <summary>
        /// A security descriptor.
        /// </summary>
        public ActiveDirectorySecurity ObjectSecurity { get; private set; } = new ActiveDirectorySecurity();

        /// <summary>
        /// The common name of the GPO; all GPO common names are curly braced GUID strings of the form {XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}.
        /// </summary>
        public string CommonName { get; private set; } = String.Empty;

        /// <summary>
        /// A human-readable  directory string description of the GPO.
        /// </summary>
        public string DisplayName { get; private set; } = String.Empty;

        /// <summary>
        /// The path to the group policy.
        /// </summary>
        public string PolicyPath { get; private set; } = String.Empty;

        /// <summary>
        /// The version of the GPO container.
        /// </summary>
        public int VersionNumber { get; private set; }

        /// <summary>
        /// ???
        /// </summary>
        public string MachineExtensionNames { get; private set; } = String.Empty;

        /// <summary>
        /// ???
        /// </summary>
        public string UserExtensionNames { get; private set; } = String.Empty;

        /// <summary>
        /// ???
        /// </summary>
        public int FunctionalityVersion { get; private set; }

        /// <summary>
        /// The bit flags of the GPO.
        /// </summary>
        public int Flags { get; private set; }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Find the list of all group policies within the domain.
        /// </summary>
        public static List<GroupPolicyContainer> FindAllPolicies() {

            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Build the principal searcher
            var Entry    = new DirectoryEntry($"LDAP://{Dn}");
            var Searcher = new DirectorySearcher(Entry);
            Searcher.Filter = "(objectClass=groupPolicyContainer)";

            // List all the properties to get from the query
            // https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-gpol/b0e5c9e8-e858-4a7a-a94a-4a3d0a9d87a2
            Searcher.PropertiesToLoad.Add("nTSecurityDescriptor");
            Searcher.PropertiesToLoad.Add("cn");
            Searcher.PropertiesToLoad.Add("displayName");
            Searcher.PropertiesToLoad.Add("gPCFileSysPath");
            Searcher.PropertiesToLoad.Add("versionNumber");
            Searcher.PropertiesToLoad.Add("gPCMachineExtensionNames");
            Searcher.PropertiesToLoad.Add("gPCUserExtensionNames");
            Searcher.PropertiesToLoad.Add("gPCFunctionalityVersion");
            Searcher.PropertiesToLoad.Add("flags");
            Searcher.PropertiesToLoad.Add("objectClass");
            Searcher.PropertiesToLoad.Add("gPCWQLFilter");
            Searcher.PropertiesToLoad.Add("sid");

            // Parse the result
            var Policies = new List<GroupPolicyContainer>();
            foreach (SearchResult Result in Searcher.FindAll()) {
                var Policy = GroupPolicyContainer.FromRawObject(Result);
                if (Policy != null) {
                    Policies.Add(Policy);
                }
            }

            // Cleanup
            Searcher.Dispose();
            Entry.Dispose();
            Dom.Dispose();

            // Success
            return Policies;
        }
        #endregion

        #region Private Static Methods

        /// <summary>
        /// Create a GroupPolicy object from a raw object returned by an LDAP query.
        /// </summary>
        private static GroupPolicyContainer? FromRawObject(SearchResult Result) {

            var Object = new GroupPolicyContainer();

            // Get the security descriptor of the GPO
            if (Result.Properties.Contains("nTSecurityDescriptor")) {
                var Sd = (byte[])Result.Properties["nTSecurityDescriptor"][0];

                Object.ObjectSecurity = new ActiveDirectorySecurity();
                Object.ObjectSecurity.SetSecurityDescriptorBinaryForm(Sd);
            }

            // Get the common name of the policy.
            if (Result.Properties.Contains("cn")) {
                Object.CommonName = (string)Result.Properties["cn"][0];
            }

            // Get the display name of the policy.
            if (Result.Properties.Contains("displayName")) {
                Object.DisplayName = (string)Result.Properties["displayName"][0];
            }

            // Get the path of the policy.
            if (Result.Properties.Contains("gPCFileSysPath")) {
                Object.PolicyPath = (string)Result.Properties["gPCFileSysPath"][0];
            }

            // Get the version of the policy.
            if (Result.Properties.Contains("versionNumber")) {
                Object.VersionNumber = Convert.ToInt32(Result.Properties["versionNumber"][0]);
            }

            // ????
            if (Result.Properties.Contains("gPCMachineExtensionNames")) {
                Object.MachineExtensionNames = (string)Result.Properties["gPCMachineExtensionNames"][0];
            }

            // ????
            if (Result.Properties.Contains("gPCUserExtensionNames")) {
                Object.UserExtensionNames = (string)Result.Properties["gPCMachineExtensionNames"][0];
            }

            // ????
            if (Result.Properties.Contains("gPCFunctionalityVersion")) {
                Object.FunctionalityVersion = Convert.ToInt32(Result.Properties["gPCFunctionalityVersion"][0]);
            }

            // Get the bit flag of the group policy.
            if (Result.Properties.Contains("flags")) {
                Object.Flags = Convert.ToInt32(Result.Properties["flags"][0]);
            }

            // Get the path to the object in AD.
            if (Result.Properties.Contains("adspath")) {
                Object.ObjectPath = (string)Result.Properties["adspath"][0];
            }

            // Success
            return Object;
        }

        #endregion
    }
}
