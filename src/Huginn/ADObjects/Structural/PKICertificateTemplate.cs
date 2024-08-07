
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Security.Cryptography;


namespace Huginn.ADObjects.Structural
{

    /// <summary>
    /// Contains information for certificates issued by Certificate Server.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-pkicertificatetemplate
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public sealed class PKICertificateTemplate : Top {

        #region Attributes

        /// <summary>
        /// The application policy OID's in a certificate.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-certificate-application-policy
        /// </summary>
        public List<string>? MSPKICertificateApplicationPolicy { get; set; }

        /// <summary>
        /// Contains the flags related to constructing the subject name in an issued certificate.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-certificate-name-flag
        /// </summary>
        public int? MSPKICertificateNameFlag { get; set; }

        /// <summary>
        /// Contains the list of policy OIDs and their optional CSPs in the issued certificate.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-certificate-policy
        /// </summary>
        public List<string>? MSPKICertificatePolicy { get; set; }

        /// <summary>
        /// Specifies the object identifier for a certificate template.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-cert-template-oid
        /// </summary>
        public Oid? MSPKICertTemplateOID { get; set; }

        /// <summary>
        /// Contains the enrollment related flags.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-enrollment-flag
        /// </summary>
        public int? MSPKIEnrollmentFlag { get; set; }

        /// <summary>
        /// Indicates the minimum private key size.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-minimal-key-size
        /// </summary>
        public int? MSPKIMinimalKeySize { get; set; }

        /// <summary>
        /// Contains the private key related flags.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-private-key-flag
        /// </summary>
        public int? MSPKIPrivateKeySize { get; set; }

        /// <summary>
        /// The required RA application policy OID in the counter signatures of the certificate request.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-ra-application-policies
        /// </summary>
        public List<string>? MSPKIRAApplicationPolicies { get; set; }

        /// <summary>
        /// Contains the list of required policy OIDs from registration authorities who sign the enrollment request.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-ra-policies
        /// </summary>
        public List<string>? MSPKIRAPolicies { get; set; }

        /// <summary>
        /// Specifies the number of enrollment registration authority signatures that are required in an enrollment request.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-ra-signature
        /// </summary>
        public int? MSPKIRASignature { get; set; }

        /// <summary>
        /// Specifies the names of the certificate templates that are superseded by the current template.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-supersede-templates
        /// </summary>
        public List<string>? MSPKISupersedeTemplates { get; set; }

        /// <summary>
        /// Keeps track of attributes in the class changing. However, this will not trigger auto-enrollment.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-template-minor-revision
        /// </summary>
        public int? MSPKITemplateMinorRevision { get; set; }

        /// <summary>
        /// Keeps track of schema updates of the PKI-Certificate-Template class.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-template-schema-version
        /// </summary>
        public int? MSPKITemplateSchemaVersion { get; set; }

        /// <summary>
        /// The list of critical extensions in the certificate template.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pkicriticalextensions
        /// </summary>
        public List<string>? PKICriticalExtensions { get; set; }

        /// <summary>
        /// The list of cryptographic service providers for the certificate template.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pkidefaultcsps
        /// </summary>
        public List<string>? PKIDefaultCSPs { get; set; }

        /// <summary>
        /// The private key specification for the certificate template
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pkidefaultkeyspec
        /// </summary>
        public int? PKIDefaultKeySpec { get; set; }

        /// <summary>
        /// The PKI-Enrollment-Access attribute is for internal use only.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pkienrollmentaccess
        /// </summary>
        public List<ActiveDirectorySecurity>? PKIEnrollmentAccess { get; set; }

        /// <summary>
        /// The validity period for the certificate template.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pkiexpirationperiod
        /// </summary>
        public byte[]? PKIExpirationPeriod { get; set; }

        /// <summary>
        /// The enhanced key usage OIDs for the certificate template.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pkiextendedkeyusage
        /// </summary>
        public List<string>? PKIExtendedKeyUsage { get; set; }

        /// <summary>
        /// The key usage extension for the certificate template.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pkikeyusage
        /// </summary>
        public byte[]? PKIKeyUsage { get; set; }

        /// <summary>
        /// The maximum length of the certificate chain issued by the certificate.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pkimaxissuingdepth
        /// </summary>
        public int? PKIMaxIssuingDepth { get; set; }

        /// <summary>
        /// The period by when the certificate should be renewed before it is expired.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pkioverlapperiod
        /// </summary>
        public byte[]? PKIOverlapPeriod { get; set; }

        #endregion

        /// <summary>
        /// List if attributes to extract when querying PKI-Certificate-Template object.
        /// </summary>
        public static readonly string[] _Attributes = new string[] {
            // Top AD object attribute.
            "cn",
            "displayname",
            "distinguishedname",
            "dscorepropagationdata",
            "flags",
            "instancetype",
            "name",
            "ntsecuritydescriptor",
            "objectcategory",
            "objectclass",
            "objectguid",
            "revision",
            "showinadvancedviewonly",
            "usnchanged",
            "usncreated",
            "whenchanged",
            "whencreated",

            // PKI-Certificate-Template object attributes.
            "mspki-cert-template-oid",
            "msPKI-certificate-policy",
            "mspki-certificate-application-policy",
            "mspki-certificate-name-flag",
            "mspki-enrollment-flag",
            "mspki-minimal-key-size",
            "mspki-private-key-flag",
            "mspki-ra-application-policies",
            "mspki-ra-policies",
            "mspki-ra-signature",
            "mspki-supersede-templates",
            "mspki-template-minor-revision",
            "mspki-template-schema-version",
            "pkicriticalextensions",
            "pkidefaultcsps",
            "pkidefaultkeyspec",
            "pkiexpirationperiod",
            "pkiextendedkeyusage",
            "pkikeyusage",
            "pkimaxissuingdepth",
            "pkioverlapperiod",
        };

        /// <summary>
        /// The LDAP display name of the PKI-Certificate-Template class.
        /// </summary>
        public static readonly string _ClassName = "pKICertificateTemplate";

        /// <summary>
        /// Return a copy of itself.
        /// </summary>
        public PKICertificateTemplate Clone() {
            return (PKICertificateTemplate)MemberwiseClone();
        }

        /// <summary>
        /// Get the list of certificate templates in the domain.
        /// </summary>
        public static List<PKICertificateTemplate> FindAll() {
            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Build the directory searcher
            var Entry       = new DirectoryEntry($"LDAP://CN=Certificate Templates,CN=Public Key Services,CN=Services,CN=Configuration,{Dn}");
            var Searcher    = new DirectorySearcher(Entry);
            Searcher.Filter = $"(objectClass={_ClassName})";

            // Add the list of the properties to load
            Searcher.PropertiesToLoad.AddRange(_Attributes);

            // Get results
            var Templates = new List<PKICertificateTemplate>();
            foreach (SearchResult Result in Searcher.FindAll()) {
                Templates.Add(
                    new PKICertificateTemplate().FromResult(Result)
                );
            }

            // Cleanup
            Searcher.Dispose();
            Entry.Dispose();
            Dom.Dispose();

            // Success
            return Templates;
        }

        /// <summary>
        /// Find a PKI-Certificate-Template object within the current domain. 
        /// </summary>
        public static PKICertificateTemplate? Find(string Name) {
            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Build the directory searcher
            var Entry       = new DirectoryEntry($"LDAP://CN=Certificate Templates,CN=Public Key Services,CN=Services,CN=Configuration,{Dn}");
            var Searcher    = new DirectorySearcher(Entry);
            Searcher.Filter = $"(&(objectClass={_ClassName})(displayName={Name}))";

            // Add the list of the properties to load
            Searcher.PropertiesToLoad.AddRange(_Attributes);

            // Search for the object
            var Result = Searcher.FindOne();
            if (Result == null) {
                return null;
            }

            // Return the certificate template.
            return new PKICertificateTemplate().FromResult(Result);
        }

        /// <summary>
        /// Add a new PKI-Certificate-Template object within the current domain.
        /// </summary>
        public bool Add() {

            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Make the container path
            string ContainerPath = $"LDAP://CN=Certificate Templates,CN=Public Key Services,CN=Services,CN=Configuration,{Dn}";
            string CNPath        = $"CN={CommonName}";

            // Open the container
            using (var Container = new DirectoryEntry(ContainerPath)) {

                // Create the new certificate template object
                using (var NewObject = Container.Children.Add($"{CNPath}", $"{_ClassName}")) {
                    if (NewObject == null) {
                        return false;
                    }

                    // Set Top object attributes.
                    SetAttribute("nTSecurityDescriptor"  , NewObject, NTSecurityDescriptor.GetSecurityDescriptorBinaryForm());
                    SetAttribute("objectCategory"        , NewObject, ObjectCategory);
                    SetAttribute("cn"                    , NewObject, CommonName);
                    SetAttribute("displayName"           , NewObject, DisplayName);
                    SetAttribute("flags"                 , NewObject, Flags);
                    SetAttribute("objectVersion"         , NewObject, ObjectVersion);
                    SetAttribute("revision"              , NewObject, Revision);
                    SetAttribute("showInAdvancedViewOnly", NewObject, ShowInAdvancedViewOnly);

                    // Set MS-PKI and PKI attributes.
                    SetAttribute("msPKI-Certificate-Application-Policy", NewObject, MSPKICertificateApplicationPolicy);
                    SetAttribute("msPKI-Certificate-Name-Flag"         , NewObject, MSPKICertificateNameFlag);
                    SetAttribute("msPKI-Certificate-Policy"            , NewObject, MSPKICertificatePolicy);
                    SetAttribute("msPKI-Cert-Template-OID"             , NewObject, MSPKICertTemplateOID.Value);
                    SetAttribute("msPKI-Enrollment-Flag"               , NewObject, MSPKIEnrollmentFlag);
                    SetAttribute("msPKI-Minimal-Key-Size"              , NewObject, MSPKIMinimalKeySize);
                    SetAttribute("msPKI-Private-Key-Flag"              , NewObject, MSPKIPrivateKeySize);
                    SetAttribute("msPKI-RA-Application-Policies"       , NewObject, MSPKIRAApplicationPolicies);
                    SetAttribute("msPKI-RA-Policies"                   , NewObject, MSPKIRAPolicies);
                    SetAttribute("msPKI-RA-Signature"                  , NewObject, MSPKIRASignature);
                    SetAttribute("msPKI-Supersede-Templates"           , NewObject, MSPKISupersedeTemplates);
                    SetAttribute("msPKI-Template-Minor-Revision"       , NewObject, MSPKITemplateMinorRevision);
                    SetAttribute("msPKI-Template-Schema-Version"       , NewObject, MSPKITemplateSchemaVersion);
                    SetAttribute("pKICriticalExtensions"               , NewObject, PKICriticalExtensions);
                    SetAttribute("pKIDefaultCSPs"                      , NewObject, PKIDefaultCSPs);
                    SetAttribute("pKIDefaultKeySpec"                   , NewObject, PKIDefaultKeySpec);
                    SetAttribute("pKIExpirationPeriod"                 , NewObject, PKIExpirationPeriod);
                    SetAttribute("pKIExtendedKeyUsage"                 , NewObject, PKIExtendedKeyUsage);
                    SetAttribute("pKIKeyUsage"                         , NewObject, PKIKeyUsage);
                    SetAttribute("pKIMaxIssuingDepth"                  , NewObject, PKIMaxIssuingDepth);
                    SetAttribute("pKIOverlapPeriod"                    , NewObject, PKIOverlapPeriod);

                    // Save the object
                    try {
                        NewObject.CommitChanges();
                        return true;
                    }
                    catch (Exception ex) {
                        Logger.Verbose(ex.ToString());
                        return false;
                    }

                }
            }
        }

        /// <summary>
        /// Update a PKI-Certificate-Template object within the current domain
        /// </summary>
        public bool Update() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove a PKI-Certificate-Template object within the current domain.
        /// </summary>
        public bool Remove()  {

            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Make the container path
            string ContainerPath = $"LDAP://CN=Certificate Templates,CN=Public Key Services,CN=Services,CN=Configuration,{Dn}";
            string CNPath        = $"CN={CommonName}";

            // Open the container
            using (DirectoryEntry Container = new DirectoryEntry(ContainerPath)) {

                // Find the certificate template object.
                using (var Template = Container.Children.Find(CNPath)) {
                    if (Template == null) {
                        return false;
                    }

                    // Remove the certificate template object.
                    try {
                        Container.Children.Remove(Template);
                        return true;
                    }
                    catch (Exception ex) {
                        Logger.Verbose(ex.ToString());
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public new PKICertificateTemplate FromResult(SearchResult Result) {

            // Get Top auxiliary attributes.
            base.FromResult(Result);

            // Get PKI-Certificate-Template object attributes.
            this.MSPKICertificateApplicationPolicy = Util.GetListOfTypeFromResult<string>(Result , "mspki-certificate-application-policy");
            this.MSPKICertificateNameFlag          = Util.GetTypeFromResult<int>(Result          , "mspki-certificate-name-flag");
            this.MSPKICertificatePolicy            = Util.GetListOfTypeFromResult<string>(Result , "msPKI-Certificate-Policy");
            this.MSPKICertTemplateOID              = Util.GetOidFromResult(Result                , "mspki-cert-template-oid");
            this.MSPKIEnrollmentFlag               = Util.GetTypeFromResult<int>(Result          , "mspki-enrollment-flag");
            this.MSPKIMinimalKeySize               = Util.GetTypeFromResult<int>(Result          , "msPKI-Minimal-Key-Size");
            this.MSPKIPrivateKeySize               = Util.GetTypeFromResult<int>(Result          , "msPKI-Private-Key-Flag");
            this.MSPKIRAApplicationPolicies        = Util.GetListOfTypeFromResult<string>(Result , "mspki-ra-application-policies");
            this.MSPKIRAPolicies                   = Util.GetListOfTypeFromResult<string>(Result , "mspki-ra-policies");
            this.MSPKIRASignature                  = Util.GetTypeFromResult<int>(Result          , "mspki-ra-signature");
            this.MSPKISupersedeTemplates           = Util.GetListOfTypeFromResult<string>(Result , "msPKI-Supersede-Templates");
            this.MSPKITemplateMinorRevision        = Util.GetTypeFromResult<int>(Result          , "msPKI-Template-Minor-Revision");
            this.MSPKITemplateSchemaVersion        = Util.GetTypeFromResult<int>(Result          , "msPKI-Template-Schema-Version");
            this.PKICriticalExtensions             = Util.GetListOfTypeFromResult<string>(Result , "pKICriticalExtensions");
            this.PKIDefaultCSPs                    = Util.GetListOfTypeFromResult<string>(Result , "pKIDefaultCSPs");
            this.PKIDefaultKeySpec                 = Util.GetTypeFromResult<int>(Result          , "pKIDefaultKeySpec");
            this.PKIEnrollmentAccess               = Util.GetListOfSecDescriptorFromResult(Result, "pKIEnrollmentAccess");
            this.PKIExpirationPeriod               = Util.GetTypeFromResult<byte[]>(Result       , "pKIExpirationPeriod");
            this.PKIExtendedKeyUsage               = Util.GetListOfTypeFromResult<string>(Result , "pKIExtendedKeyUsage");
            this.PKIKeyUsage                       = Util.GetTypeFromResult<byte[]>(Result       , "pKIKeyUsage");
            this.PKIMaxIssuingDepth                = Util.GetTypeFromResult<int>(Result          , "pKIMaxIssuingDepth");
            this.PKIOverlapPeriod                  = Util.GetTypeFromResult<byte[]>(Result       , "pKIOverlapPeriod");

            // Success
            return this;
        }
    }
}
