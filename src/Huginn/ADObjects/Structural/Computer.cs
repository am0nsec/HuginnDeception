using Huginn.ADObjects.Auxiliary;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huginn.ADObjects.Structural {

    /// <summary>
    /// This class represents a computer account in the domain.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-computer
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class Computer : User {

        #region Attributes

        // Catalogs
        // Default-Local-Policy-Object

        /// <summary>
        /// Name of computer as registered in DNS.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-dnshostname
        /// </summary>
        public string? DNSHostName { get; set; }

        /// <summary>
        /// Flags that determine where a computer gets its policy. Local-Policy-Reference.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-localpolicyflags
        /// </summary>
        public Int32? LocalPolicyFlags { get; set; }

        // Location

        /// <summary>
        /// Role for a machine: DC, Server, or Workstation.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-machinerole
        /// </summary>
        public Int32? MachineRole { get; set; }

        /// <summary>
        /// The distinguished name of the user that is assigned to manage this object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-managedby
        /// </summary>
        public string? ManagedBy { get; set; }

        /// <summary>
        /// The attribute is used to store the additional DNS host name of a computer object.
        /// This attribute is used at the time a computer is renamed, or names are managed with "netdom computername".
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-additionaldnshostname
        /// </summary>
        public List<string>? MSDSAdditionalDnsHostName { get; set; }

        /// <summary>
        /// This attribute is used to store the SAM account names that correspond to the DNS host names in ms-DS-Additional-Dns-Host-Name.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-additionalsamaccountname
        /// </summary>
        public List<string>? MSDSAdditionalSamAccountName { get; set; }

        // ms-DS-AuthenticatedAt-DC
        // ms-DS-ExecuteScriptPassword
        // ms-DS-Generation-Id
        // ms-DS-Host-Service-Account
        // ms-DS-isGC
        // ms-DS-isRODC
        // ms-DS-Is-User-Cachable-At-Rodc
        // ms-DS-KrbTgt-Link
        // ms-DS-Never-Reveal-Group
        // ms-DS-Promotion-Settings
        // ms-DS-Revealed-List
        // ms-DS-Revealed-Users
        // ms-DS-Reveal-OnDemand-Group

        /// <summary>
        /// Lists the site name that corresponds to the DC.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-sitename
        /// </summary>
        public string? MSDSSiteName { get; set; }

        /// <summary>
        /// Contains the name of the hash algorithm used to create the Thumbprint Hash for the Scan Repository/Secure Print Device.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msimaging-hashalgorithm
        /// </summary>
        public string? MSImagingHashAlgorithm { get; set; }

        // ms-Imaging-Thumbprint-Hash
        // msSFU-30-Aliases

        /// <summary>
        /// Contains the owner information of a particular TPM.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mstpm-ownerinformation
        /// </summary>
        public string? MSTPMOwnerInformation { get; set; }

        /// <summary>
        /// This attribute links a Computer object to a TPM object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mstpm-tpminformationforcomputer
        /// </summary>
        public string? MSTPMTpmInformationForComputer { get; set; }

        /// <summary>
        /// This attribute represents the VM Name for a computer in a TSV deployment.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mstsendpointdata
        /// </summary>
        public string? MSTSEndpointData { get; set; }

        /// <summary>
        /// This attribute represents the name of the plug-in that handles the orchestration.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mstsendpointplugin
        /// </summary>
        public string? MSTSEndpointPlugin { get; set; }

        /// <summary>
        /// This attribute defines whether the computer is a physical computer or a virtual machine.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mstsendpointtype
        /// </summary>
        public Int32? MSTSEndpointType { get; set; }

        // ms-TS-Primary-Desktop-BL
        // MS-TS-Property01
        // MS-TS-Property02
        // ms-TS-Secondary-Desktop-BL
        // Netboot-GUID
        // Netboot-Initialization
        // Netboot-Machine-File-Path
        // Netboot-Mirror-Data-File
        // Netboot-SIF-File
        // Network-Address
        // nisMapName

        /// <summary>
        /// The Operating System name, for example, Windows Vista Enterprise.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-operatingsystem
        /// </summary>
        public string? OperatingSystem { get; set; }

        /// <summary>
        /// The hotfix level of the operating system.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-operatingsystemhotfix
        /// </summary>
        public string? OperatingSystemHotfix { get; set; }

        /// <summary>
        /// The operating system service pack ID string (for example, SP3).
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-operatingsystemservicepack
        /// </summary>
        public string? OperatingSystemServicePack { get; set; }

        /// <summary>
        /// The operating system version string, for example, 4.0.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-operatingsystemversion
        /// </summary>
        public string? OperatingSystemVersion { get; set; }

        /// <summary>
        /// Used to map a device (for example, a printer, computer, and so on) to a physical location.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-physicallocationobject
        /// </summary>
        public string? PhysicalLocationObject { get; set; }

        /// <summary>
        /// Determines which LSA properties are replicated to clients.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-policyreplicationflags
        /// </summary>
        public Int32? PolicyReplicationFlags { get; set; }

        // RID-Set-References

        /// <summary>
        /// The unique identifier for a site.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-siteguid
        /// </summary>
        public Guid? SiteGUID { get; set; }

        /// <summary>
        /// The tracked volume quota for a given computer.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-volumecount
        /// </summary>
        public Int32? VolumeCount { get; set; }

        #endregion

        /// <summary>
        /// List if attributes to extract when querying Person object.
        /// </summary>
        protected static new string[] Attributes {
            get {
                return User.Attributes.Concat(new string[] {
                    "dNSHostName",
                    "localPolicyFlags",
                    "machineRole",
                    "managedBy",
                    "msDS-AdditionalDnsHostName",
                    "msDS-AdditionalSamAccountName",
                    "msDS-SiteName",
                    "msImaging-HashAlgorithm",
                    "msTPM-OwnerInformation",
                    "msTPM-TpmInformationForComputer",
                    "msTSEndpointData",
                    "msTSEndpointPlugin",
                    "msTSEndpointType",
                    "operatingSystem",
                    "operatingSystemHotfix",
                    "operatingSystemServicePack",
                    "operatingSystemVersion",
                    "physicalLocationObject",
                    "policyReplicationFlags",
                    "siteGUID",
                    "volumeCount"
                }).ToArray();
            }
        }

        /// <summary>
        /// The LDAP display name of the Computer class.
        /// </summary>
        public static readonly string _ClassName = "computer";

        #region Static Methods

        /// <summary>
        /// Find a computer object within the current domain. 
        /// </summary>
        public static Computer? Find(string Name) {
            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Build the directory searcher
            var Entry       = new DirectoryEntry($"LDAP://{Dn}");
            var Searcher    = new DirectorySearcher(Entry);
            Searcher.Filter = $"(&(objectClass={_ClassName})(name={Name}))";

            // Add the list of the properties to load
            string[] AllAttributes = [];

            AllAttributes = AllAttributes.Concat(User.Attributes).ToArray();
            AllAttributes = AllAttributes.Concat(SecurityPrincipal.Attributes).ToArray();

            Searcher.PropertiesToLoad.AddRange(AllAttributes);

            // Search for the object
            try {
                var Result = Searcher.FindOne();
                if (Result == null) {
                    return null;
                }

                // Return the security principal.
                return new Computer().FromResult(Result);
            } catch (Exception ex) {
                Logger.Verbose(ex.ToString());
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public new Computer FromResult(SearchResult Result) {

            // Get User structural attributes.
            base.FromResult(Result);

            // Get Computer structural attributes.
            this.DNSHostName                    = Util.GetTypeFromResult<string>(Result      , "dNSHostName");
            this.LocalPolicyFlags               = Util.GetTypeFromResult<int>(Result         , "localPolicyFlags");
            this.MachineRole                    = Util.GetTypeFromResult<int>(Result         , "machineRole");
            this.ManagedBy                      = Util.GetTypeFromResult<string>(Result      , "managedBy");
            this.MSDSAdditionalDnsHostName      = Util.GetListOfTypeFromResult<string>(Result, "msDS-AdditionalDnsHostName");
            this.MSDSAdditionalSamAccountName   = Util.GetListOfTypeFromResult<string>(Result, "msDS-AdditionalSamAccountName");
            this.MSDSSiteName                   = Util.GetTypeFromResult<string>(Result      , "msDS-SiteName");
            this.MSImagingHashAlgorithm         = Util.GetTypeFromResult<string>(Result      , "msImaging-HashAlgorithm");
            this.MSTPMOwnerInformation          = Util.GetTypeFromResult<string>(Result      , "msTPM-OwnerInformation");
            this.MSTPMTpmInformationForComputer = Util.GetTypeFromResult<string>(Result      , "msTPM-TpmInformationForComputer");
            this.MSTSEndpointData               = Util.GetTypeFromResult<string>(Result      , "msTSEndpointData");
            this.MSTSEndpointPlugin             = Util.GetTypeFromResult<string>(Result      , "msTSEndpointPlugin");
            this.MSTSEndpointType               = Util.GetTypeFromResult<int>(Result         , "msTSEndpointType");
            this.OperatingSystem                = Util.GetTypeFromResult<string>(Result      , "operatingSystem");
            this.OperatingSystemHotfix          = Util.GetTypeFromResult<string>(Result      , "operatingSystemHotfix");
            this.OperatingSystemServicePack     = Util.GetTypeFromResult<string>(Result      , "operatingSystemServicePack");
            this.OperatingSystemVersion         = Util.GetTypeFromResult<string>(Result      , "operatingSystemVersion");
            this.PhysicalLocationObject         = Util.GetTypeFromResult<string>(Result      , "physicalLocationObject");
            this.PolicyReplicationFlags         = Util.GetTypeFromResult<int>(Result         , "policyReplicationFlags");
            this.SiteGUID                       = Util.GetGuidFromResult(Result              , "siteGUID");
            this.VolumeCount                    = Util.GetTypeFromResult<int>(Result         , "volumeCount");

            // Success
            return this;
        }

        /// <summary>
        /// Update one attribute of the object.
        /// </summary>
        public new bool UpdateAttribute(string AttributeName, object? AttributeValue) {
            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Make the container path
            string ContainerPath = $"LDAP://CN=Computers,{Dn}";
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
