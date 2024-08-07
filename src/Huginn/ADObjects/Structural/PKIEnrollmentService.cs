using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Security.Cryptography.X509Certificates;

namespace Huginn.ADObjects.Structural {

    /// <summary>
    /// The certificate server that can process certificate requests and issue certificates.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-pkienrollmentservice
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public sealed class PKIEnrollmentService : Top {

        #region Attributes

        /// <summary>
        /// Certificates of trusted Certification Authorities.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-cacertificate
        /// </summary>
        public List<X509Certificate2>? CACertificate { get; set; }

        /// <summary>
        /// Full distinguished name from the CA certificate.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-cacertificatedn
        /// </summary>
        public string? CACertificateDN { get; set; }

        /// <summary>
        /// Contains information for a certificate issued by a Certificate Server.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-certificatetemplates
        /// </summary>
        public List<string>? CertificateTemplates { get; set; }

        /// <summary>
        /// Name of computer as registered in DNS.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-dnshostname
        /// </summary>
        public string? DNSHostName { get; set; }

        /// <summary>
        /// PKI - Certificate Templates.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-enrollmentproviders
        /// </summary>
        public string? EnrollmentProviders { get; set; }

        /// <summary>
        /// Priority, authentication type, and URI of each certificate enrollment web service
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-enrollment-servers
        /// </summary>
        public List<string>? MSPKIEnrollmentServers { get; set; }

        /// <summary>
        /// Active Directory site to which the CA computer belongs.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mspki-site-name
        /// </summary>
        public string? MSPKISiteName { get; set; }

        /// <summary>
        /// This attribute indicates the type of algorithm that must be used to decode a digital signature during the authentication process.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-signaturealgorithms
        /// </summary>
        public string? SignatureAlgorithms { get; set; }

        #endregion

        /// <summary>
        /// List if attributes to extract when querying PKI-Enrollment-Service object.
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
            "cACertificate",
            "cACertificateDN",
            "certificateTemplates",
            "dNSHostName",
            "enrollmentProviders",
            "msPKI-Enrollment-Servers",
            "msPKI-Site-Name",
            "signatureAlgorithms",
        };

        /// <summary>
        /// The LDAP display name of the PKI-Enrollment-Service class.
        /// </summary>
        public static readonly string _ClassName = "pKIEnrollmentService";

        #region Static Methods

        /// <summary>
        /// Find all PKI-Enrollment-Service objects within the current domain.
        /// </summary>
        public static List<PKIEnrollmentService> FindAll() {
            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Build the directory searcher
            var Entry       = new DirectoryEntry($"LDAP://CN=Enrollment Services,CN=Public Key Services,CN=Services,CN=Configuration,{Dn}");
            var Searcher    = new DirectorySearcher(Entry);
            Searcher.Filter = $"(objectClass={PKIEnrollmentService._ClassName})";

            // Add the list of the properties to load
            Searcher.PropertiesToLoad.AddRange(PKIEnrollmentService._Attributes);

            // Get results
            var EnrollmentServices = new List<PKIEnrollmentService>();
            foreach (SearchResult Result in Searcher.FindAll()) {
                // Add object to list.
                EnrollmentServices.Add(
                    new PKIEnrollmentService().FromResult(Result)
                );
            }

            // Cleanup
            Searcher.Dispose();
            Entry.Dispose();
            Dom.Dispose();

            // Success
            return EnrollmentServices;
        }

        /// <summary>
        /// Find a PKI-Enrollment-Service object within the current domain. 
        /// </summary>
        public static PKIEnrollmentService? Find(string Name) {
            throw new NotImplementedException();
        }

        #endregion
   
        /// <summary>
        /// Return a copy of itself.
        /// </summary>
        public PKIEnrollmentService Clone() {
            return (PKIEnrollmentService)this.MemberwiseClone();
        }

        /// <summary>
        /// Add a new PKI-Enrollment-Service object within the current domain.
        /// </summary>
        public bool Add() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a PKI-Enrollment-Service object within the current domain.
        /// </summary>
        public bool Update() {

            // Get the distinguished name of the current domain.
            var Dom = Domain.GetCurrentDomain();
            var Dn  = Util.NameToDistinguishedName(Dom.Name);

            // Make the container path
            string ContainerPath = $"LDAP://CN=Enrollment Services,CN=Public Key Services,CN=Services,CN=Configuration,{Dn}";
            string CNPath        = $"CN={this.CommonName}";

            // Open the container
            using (DirectoryEntry Container = new DirectoryEntry(ContainerPath)) {

                // Find the enrollment service object.
                using (var EnrollmentService = Container.Children.Find(CNPath)) {
                    if (EnrollmentService == null) {
                        return false;
                    }

                    // Update the attributes.
                    Top.SetAttribute("certificateTemplates", EnrollmentService, this.CertificateTemplates);

                    // Save the object.
                    try {
                        EnrollmentService.CommitChanges();
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
        /// Remove new PKI-Enrollment-Service object within the current domain.
        /// </summary>
        public bool Remove() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public new PKIEnrollmentService FromResult(SearchResult Result) {

            // Get Top auxiliary attributes.
            base.FromResult(Result);

            // Get PKI-Enrollment-Service object attributes.
            this.CACertificate          = Util.GetCaCertificateFromResult(Result     , "cACertificate");
            this.CACertificateDN        = Util.GetTypeFromResult<string>(Result      , "cACertificateDN");
            this.CertificateTemplates   = Util.GetListOfTypeFromResult<string>(Result, "certificateTemplates");
            this.DNSHostName            = Util.GetTypeFromResult<string>(Result      , "dNSHostName");
            this.EnrollmentProviders    = Util.GetTypeFromResult<string>(Result      , "enrollmentProviders");
            this.MSPKIEnrollmentServers = Util.GetListOfTypeFromResult<string>(Result, "msPKI-Enrollment-Servers");
            this.MSPKISiteName          = Util.GetTypeFromResult<string>(Result      , "msPKI-Site-Name");
            this.SignatureAlgorithms    = Util.GetTypeFromResult<string>(Result      , "signatureAlgorithms");

            // Success
            return this;
        }
    }
}
