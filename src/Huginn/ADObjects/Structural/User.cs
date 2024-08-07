
using System.CommandLine.Parsing;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Huginn.ADObjects.Abstract;
using Huginn.ADObjects.Auxiliary;

namespace Huginn.ADObjects.Structural {

    /// <summary>
    /// This class is used to store information about an employee or contractor who works for an organization.
    /// It is also possible to apply this class to long term visitors.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-user
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class User : OrganizationalPerson {

        #region Attributes

        /// <summary>
        /// The date when the account expires.
        /// This value represents the number of 100-nanosecond intervals since January 1, 1601 (UTC).
        /// A value of 0 or 0x7FFFFFFFFFFFFFFF (9223372036854775807) indicates that the account never expires.
        /// Accounts configured to never expire may have either value, depending on whether they were originally configured with an expiration value.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-accountexpires
        /// </summary>
        public Int64? AccountExpires { get; set; }

        // ACS-Policy-Name

        /// <summary>
        /// Indicates that a given object has had its ACLs changed to a more secure value by the system
        /// because it was a member of one of the administrative groups (directly or transitively)
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-admincount
        /// </summary>
        public Int32? AdminCount { get; set; }

        // audio

        /// <summary>
        /// The last time and date that an attempt to log on to this account was made with a password that is not valid.
        /// This value is stored as a large integer that represents the number of 100-nanosecond intervals since January 1, 1601 (UTC).
        /// A value of zero means that the last time a incorrect password was used is unknown.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-badpasswordtime
        /// </summary>
        public DateTime? BadPasswordTime { get; set; }

        /// <summary>
        /// The number of times the user tried to log on to the account using an incorrect password.
        ///  value of 0 indicates that the value is unknown.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-badpwdcount
        /// </summary>
        public Int32? BadPasswordCount { get; set; }

        // Business-Category
        // carLicense

        /// <summary>
        /// Specifies the code page for the user's language of choice. This value is not used by Windows 2000.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-codepage
        /// </summary>
        public Int32? CodePage { get; set; }

        // Control-Access-Rights
        // DBCS-Pwd
        // Default-Class-Store
        // departmentNumber

        /// <summary>
        /// The location of the desktop profile for a user or group of users. Not used.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-desktopprofile
        /// </summary>
        public string? DesktopProfile { get; set; }

        // Display-Name
        // Dynamic-LDAP-Server
        // Employee-Number
        // Employee-Type
        // Group-Membership-SAM
        // Group-Priority
        // Groups-to-Ignore

        /// <summary>
        /// The home directory for the account.
        /// If homeDrive is set and specifies a drive letter, homeDirectory must be a UNC path.
        /// Otherwise, homeDirectory is a fully qualified local path including the drive letter (for example, DriveLetter**:\Directory\**Folder). This value can be a null string.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-homedirectory
        /// </summary>
        public string? HomeDirectory { get; set; }
        
        /// <summary>
        /// Specifies the drive letter to which to map the UNC path specified by homeDirectory.
        /// The drive letter must be specified in the form DriveLetter**:** where DriveLetter is the letter of the drive to map.
        /// The DriveLetter must be a single, uppercase letter and the colon (:) is required.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-homedrive
        /// </summary>
        public string? HomeDrive { get; set; }

        // jpegPhoto
        // labeledURI

        /// <summary>
        /// This attribute is not used.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-lastlogoff
        /// </summary>
        public DateTime? LastLogoff { get; set; }

        /// <summary>
        /// The last time the user logged on.
        /// This value is stored as a large integer that represents the number of 100-nanosecond intervals since January 1, 1601 (UTC).
        /// A value of zero means that the last logon time is unknown.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-lastlogon
        /// </summary>
        public DateTime? LastLogon { get; set; }

        /// <summary>
        /// This is the time that the user last logged into the domain.
        /// This value is stored as a large integer that represents the number of 100-nanosecond intervals since January 1, 1601 (UTC).
        /// Whenever a user logs on, the value of this attribute is read from the DC. If the value is older [ current_time - msDS-LogonTimeSyncInterval ],
        /// the value is updated. The initial update after the raise of the domain functional level is calculated as 14 days minus random percentage of 5 days.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-lastlogontimestamp
        /// </summary>
        public DateTime? LastLogonTimestamp { get; set; }

        // Lm-Pwd-History
        // Locale-ID

        /// <summary>
        /// The date and time (UTC) that this account was locked out.
        /// This value is stored as a large integer that represents the number of 100-nanosecond intervals since January 1, 1601 (UTC)
        /// A value of zero means that the account is not currently locked out.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-lockouttime
        /// </summary>
        public DateTime? LockoutTime { get; set; }

        /// <summary>
        /// The number of times the account has successfully logged on.
        /// A value of 0 indicates that the value is unknown.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-logoncount
        /// </summary>
        public Int32? LogonCount { get; set; }

        /// <summary>
        /// The hours that the user is allowed to logon to the domain.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-logonhours
        /// </summary>
        public byte[]? LogonHours { get; set; }

        // Logon-Workstation
        // Max-Storage
        // ms-COM-UserPartitionSetLink
        // MS-DRM-Identity-Certificate
        // ms-DS-AuthenticatedAt-DC
        // ms-DS-Cached-Membership
        // ms-DS-Cached-Membership-Time-Stamp
        // MS-DS-Creator-SID
        // ms-DS-Failed-Interactive-Logon-Count
        // ms-DS-Failed-Interactive-Logon-Count-At-Last-Successful-Logon
        // ms-DS-Last-Failed-Interactive-Logon-Time
        // ms-DS-Last-Successful-Interactive-Logon-Time
        // ms-DS-Primary-Computer
        // ms-DS-Resultant-PSO
        // ms-DS-Secondary-KrbTgt-Number
        // ms-DS-Site-Affinity
        // ms-DS-Source-Object-DN

        /// <summary>
        /// The encryption algorithms supported by user, computer or trust accounts.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-supportedencryptiontypes
        /// </summary>
        public Int32? MSDSSupportedEncryptionTypes { get; set; }

        // ms-DS-User-Account-Control-Computed
        // ms-DS-User-Password-Expiry-Time-Computed
        // ms-IIS-FTP-Dir
        // ms-IIS-FTP-Root
        // MSMQ-Digests
        // MSMQ-Digests-Mig
        // MSMQ-Sign-Certificates
        // MSMQ-Sign-Certificates-Mig
        // msNPAllowDialin
        // msNPCallingStationID
        // msNPSavedCallingStationID
        // ms-PKI-AccountCredentials
        // ms-PKI-Credential-Roaming-Tokens
        // ms-PKI-DPAPIMasterKeys
        // ms-PKI-RoamingTimeStamp
        // msRADIUSCallbackNumber
        // ms-RADIUS-FramedInterfaceId
        // msRADIUSFramedIPAddress
        // ms-RADIUS-FramedIpv6Prefix
        // ms-RADIUS-FramedIpv6Route
        // msRADIUSFramedRoute
        // ms-RADIUS-SavedFramedInterfaceId
        // ms-RADIUS-SavedFramedIpv6Prefix
        // ms-RADIUS-SavedFramedIpv6Route
        // msRADIUSServiceType
        // msRASSavedCallbackNumber
        // msRASSavedFramedIPAddress
        // msRASSavedFramedRoute
        // msSFU-30-Name
        // msSFU-30-Nis-Domain
        // ms-TS-Allow-Logon
        // ms-TS-Broken-Connection-Action
        // ms-TS-Connect-Client-Drives
        // ms-TS-Connect-Printer-Drives
        // ms-TS-Default-To-Main-Printer
        // MS-TS-ExpireDate
        // MS-TS-ExpireDate2
        // MS-TS-ExpireDate3
        // MS-TS-ExpireDate4
        // ms-TS-Home-Directory
        // ms-TS-Home-Drive
        // ms-TS-Initial-Program
        // MS-TS-LicenseVersion
        // MS-TS-LicenseVersion2
        // MS-TS-LicenseVersion3
        // MS-TS-LicenseVersion4
        // MS-TSLS-Property01
        // MS-TSLS-Property02
        // MS-TS-ManagingLS
        // MS-TS-ManagingLS2
        // MS-TS-ManagingLS3
        // MS-TS-ManagingLS4
        // ms-TS-Max-Connection-Time
        // ms-TS-Max-Disconnection-Time
        // ms-TS-Max-Idle-Time
        // ms-TS-Primary-Desktop
        // ms-TS-Profile-Path
        // MS-TS-Property01
        // MS-TS-Property02
        // ms-TS-Reconnection-Action
        // ms-TS-Remote-Control
        // ms-TS-Secondary-Desktops
        // ms-TS-Work-Directory
        // Network-Address
        // Nt-Pwd-History
        // Operator-Count
        // Other-Login-Workstations
        // photo
        // preferredLanguage
        // Preferred-OU

        /// <summary>
        /// Contains the relative identifier (RID) for the primary group of the user.
        /// By default, this is the RID for the Domain Users group.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-primarygroupid
        /// </summary>
        public Int32? PrimaryGroupID { get; set; }

        /// <summary>
        /// Specifies a path to the user's profile.
        /// This value can be a null string, a local absolute path, or a UNC path.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-profilepath
        /// </summary>
        public string? ProfilePath { get; set; }

        /// <summary>
        /// The date and time that the password for this account was last changed.
        /// This value is stored as a large integer that represents the number of 100 nanosecond intervals since January 1, 1601 (UTC).
        /// If this value is set to 0 and the User-Account-Control attribute does not contain the UF_DONT_EXPIRE_PASSWD flag, then the user must set the password at the next logon.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pwdlastset
        /// </summary>
        public DateTime? PwdLastSet { get; set; }

        // roomNumber

        /// <summary>
        /// This attribute specifies the path for the user's logon script. The string can be null.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-scriptpath
        /// </summary>
        public string? ScriptPath { get; set; }

        /// <summary>
        /// List of principal names used for mutual authentication with an instance of a service on this computer.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-serviceprincipalname
        /// </summary>
        public List<string>? ServicePrincipalName { get; set; }

        // Terminal-Server
        // Unicode-Pwd

        /// <summary>
        /// Flags that control the behavior of the user account.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-useraccountcontrol
        /// </summary>
        public Int32? UserAccountControl { get; set; }

        /// <summary>
        /// Parameters of the user. Points to a Unicode string that is set aside for use by applications.
        /// This string can be a null string, or it can have any number of characters before the terminating null character.
        /// Microsoft products use this member to store user data specific to the individual program.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-userparameters
        /// </summary>
        public string? UserParameters { get; set; }

        // userPKCS12

        /// <summary>
        /// This attribute contains the UPN that is an Internet-style login name for a user based on the Internet standard RFC 822.
        /// The UPN is shorter than the distinguished name and easier to remember. By convention, this should map to the user email name.
        /// The value set for this attribute is equal to the length of the user's ID and the domain name. For more information about this attribute, see User Naming Attributes.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-userprincipalname
        /// </summary>
        public string? UserPrincipalName { get; set; }

        /// <summary>
        /// Specifies a UNC path to the user's shared documents folder.
        /// The path must be a network UNC path of the form **\\Server\Share\**Directory. This value can be a null string.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-usersharedfolder
        /// </summary>
        public string? UserSharedFolder { get; set; }

        /// <summary>
        /// Specifies a UNC path to the user's additional shared documents folder.
        /// The path must be a network UNC path of the form **\\Server\Share\**Directory. This value can be a null string.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-usersharedfolderother
        /// </summary>
        public List<string>? UserSharedFolderOther { get; set; }

        /// <summary>
        /// Contains the NetBIOS or DNS names of the computers running Windows NT Workstation or Windows 2000 Professional from which the user can log on.
        /// Each NetBIOS name is separated by a comma. Multiple names should be separated by commas.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-userworkstations
        /// </summary>
        public string? UserWorkstation { get; set; }

        // x500uniqueIdentifier

        /// <summary>
        /// Contains the DER-encoded X.509v3 certificates issued to the user.
        /// Note that this property contains the public key certificates issued to this user by Microsoft Certificate Service.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-usercertificate
        /// </summary>
        public List<X509Certificate2>? X509Cert { get; set; }

        #endregion

        /// <summary>
        /// Security-Principal auxiliary class.
        /// </summary>
        public SecurityPrincipal Principal { get; set; } = new SecurityPrincipal();

        /// <summary>
        /// List if attributes to extract when querying Person object.
        /// </summary>
        protected static new string[] Attributes {
            get {
                return OrganizationalPerson.Attributes.Concat(new string[] {
                    "accountExpires",
                    "adminCount",
                    "badPasswordTime",
                    "badPwdCount",
                    "codePage",
                    "desktopProfile",
                    "homeDirectory",
                    "homeDrive",
                    "lastLogoff",
                    "lastLogon",
                    "lastLogonTimestamp",
                    "lockoutTime",
                    "logonCount",
                    "logonHours",
                    "msDS-SupportedEncryptionTypes",
                    "primaryGroupID",
                    "profilePath",
                    "pwdLastSet",
                    "scriptPath",
                    "servicePrincipalName",
                    "userAccountControl",
                    "userParameters",
                    "userPrincipalName",
                    "userSharedFolder",
                    "userSharedFolderOther",
                    "userWorkstations",
                    "userCertificate"
                }).ToArray();
            }
        }

        /// <summary>
        /// The LDAP display name of the User class.
        /// </summary>
        private static readonly string _ClassName = "user";

        #region Static Methods 

        /// <summary>
        /// Find a group object within the current domain. 
        /// </summary>
        public static User? Find(string Name) {
             // Get the distinguished name of the current domain.
             var Dom = Domain.GetCurrentDomain();
             var Dn  = Util.NameToDistinguishedName(Dom.Name);

             // Build the directory searcher
             var Entry    = new DirectoryEntry($"LDAP://{Dn}");
             var Searcher = new DirectorySearcher(Entry);
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
                 return new User().FromResult(Result);
             } catch (Exception ex) {
                 Logger.Verbose(ex.ToString());
                 return null;
             }
        }

        #endregion

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public new User FromResult(SearchResult Result) {

            // Get Organizational-Person abstract attributes.
            base.FromResult(Result);

            // Get Security-Principal auxiliary attributes.
            this.Principal.FromResult(Result);

            // Get User structural attributes.
            this.AccountExpires               = Util.GetTypeFromResult<long>(Result        , "accountExpires");
            this.AdminCount                   = Util.GetTypeFromResult<int>(Result         , "adminCount");
            this.BadPasswordTime              = Util.GetDateTimeFromResult(Result          , "badPasswordTime");
            this.BadPasswordCount             = Util.GetTypeFromResult<int>(Result         , "badPwdCount");
            this.CodePage                     = Util.GetTypeFromResult<int>(Result         , "codePage");
            this.DesktopProfile               = Util.GetTypeFromResult<string>(Result      , "desktopProfile");
            this.HomeDirectory                = Util.GetTypeFromResult<string>(Result      , "homeDirectory");
            this.HomeDrive                    = Util.GetTypeFromResult<string>(Result      , "homeDrive");
            this.LastLogoff                   = Util.GetDateTimeFromResult(Result          , "lastLogoff");
            this.LastLogon                    = Util.GetDateTimeFromResult(Result          , "lastLogon");
            this.LastLogonTimestamp           = Util.GetDateTimeFromResult(Result          , "lastLogonTimestamp");
            this.LockoutTime                  = Util.GetDateTimeFromResult(Result          , "lockoutTime");
            this.LogonCount                   = Util.GetTypeFromResult<int>(Result         , "logonCount");
            this.LogonHours                   = Util.GetTypeFromResult<byte[]>(Result      , "logonHours");
            this.MSDSSupportedEncryptionTypes = Util.GetTypeFromResult<int>(Result         , "msDS-SupportedEncryptionTypes");
            this.PrimaryGroupID               = Util.GetTypeFromResult<int>(Result         , "primaryGroupID");
            this.ProfilePath                  = Util.GetTypeFromResult<string>(Result      , "profilePath");
            this.PwdLastSet                   = Util.GetDateTimeFromResult(Result          , "pwdLastSet");
            this.ScriptPath                   = Util.GetTypeFromResult<string>(Result      , "scriptPath");
            this.ServicePrincipalName         = Util.GetListOfTypeFromResult<string>(Result, "servicePrincipalName");
            this.UserAccountControl           = Util.GetTypeFromResult<int>(Result         , "userAccountControl");
            this.UserParameters               = Util.GetTypeFromResult<string>(Result      , "userParameters");
            this.UserPrincipalName            = Util.GetTypeFromResult<string>(Result      , "userPrincipalName");
            this.UserSharedFolder             = Util.GetTypeFromResult<string>(Result      , "userSharedFolder");
            this.UserSharedFolderOther        = Util.GetListOfTypeFromResult<string>(Result, "userSharedFolderOther");
            this.UserWorkstation              = Util.GetTypeFromResult<string>(Result      , "userWorkstations");
            this.X509Cert                     = Util.GetCaCertificateFromResult(Result     , "userCertificate");

            // Success
            return this;
        }

        /// <summary>
        /// Update one attribute of the object.
        /// </summary>
        public bool UpdateAttribute(string AttributeName, object? AttributeValue) {

            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Make the container path
            string ContainerPath = $"LDAP://CN=Users,{Dn}";
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
