using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huginn.ADObjects.Abstract {

    /// <summary>
    /// This class is used for objects that contain organizational information about a user,
    /// such as the employee number, department, manager, title, office address, and so on.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-organizationalperson
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class OrganizationalPerson : Person {

        #region Attributes

        /// <summary>
        /// The user's address.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-streetaddress
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// A user's home address.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-homepostaladdress
        /// </summary>
        public string? AddressHome { get; set; }

        /// <summary>
        /// The distinguished name of a user's administrative assistant.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-assistant
        /// </summary>
        public string? Assistant { get; set; }

        /// <summary>
        /// The user's company name.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-company
        /// </summary>
        public string? Company { get; set; }

        /// <summary>
        /// Specifies the country/region code for the user's language of choice. This value is not used by Windows 2000.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-countrycode
        /// </summary>
        public Int32? CountryCode { get; set; }

        /// <summary>
        /// The country/region in the address of the user.
        /// The country/region is represented as a 2-character code based on ISO-3166.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-c
        /// </summary>
        public string? CountryName { get; set; }

        /// <summary>
        /// Contains the name for the department in which the user works
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-department
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// This is part of the X.500 specification and not used by NTDS.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-destinationindicator
        /// </summary>
        public List<string>? DestinationIndicator { get; set; }

        /// <summary>
        /// Contains the name for the department in which the user works
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-department
        /// </summary>
        public string? Division { get; set; }

        /// <summary>
        /// The list of email addresses for a contact.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mail
        /// </summary>
        public string? EMailAddress { get; set; }

        /// <summary>
        /// The ID of an employee.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-employeeid
        /// </summary>
        public string? EmployeeID { get; set; }

        /// <summary>
        /// Contains telephone number of the user's business fax machine.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-facsimiletelephonenumber
        /// </summary>
        public string? FacsimileTelephoneNumber { get; set; }

        /// <summary>
        /// Indicates a person generation. For example, Jr. or II.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-generationqualifier
        /// </summary>
        public string? GenerationQualifier { get; set; }

        /// <summary>
        /// Contains the given name (first name) of the user.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-givenname
        /// </summary>
        public string? GivenName { get; set; }

        /// <summary>
        /// Contains the initials for parts of the user's full name.
        /// This may be used as the middle initial in the Windows Address Book.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-initials
        /// </summary>
        public string? Initials { get; set; }

        /// <summary>
        /// Specifies an International ISDN Number associated with an object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-internationalisdnnumber
        /// </summary>
        public List<string>? InternationalISDNNumber { get; set; }

        /// <summary>
        /// Represents the name of a locality, such as a town or city.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-l
        /// </summary>
        public string? LocalityName { get; set; }

        /// <summary>
        /// BLOB that contains a logo for this object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-thumbnaillogo
        /// </summary>
        public byte[]? Logo { get; set; }

        /// <summary>
        /// Contains the distinguished name of the user who is the user's manager. 
        /// The manager's user object contains a directReports property that contains references to all user objects
        /// that have their manager properties set to this distinguished name.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-manager
        /// </summary>
        public string? Manager { get; set; }

        /// <summary>
        /// This attribute is used for access checks to determine if a requestor has permission to act on the behalf
        /// of other identities to services running as this account.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-msds-allowedtoactonbehalfofotheridentity
        /// </summary>
        public ActiveDirectorySecurity? MSDSAllowedToActOnBehalfOfOtherIdentity { get; set; }

        /// <summary>
        /// X.400 address.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mhsoraddress
        /// </summary>
        public List<string>? MHSORAddress { get; set; }

        /// <summary>
        /// The name of the organizational unit.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-ou
        /// </summary>
        public List<string>? OrganizationalUnitName { get; set; }

        /// <summary>
        /// The name of the company or organization.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-o
        /// </summary>
        public string? OrganizationName { get; set; }

        /// <summary>
        /// Contains other additional mail addresses in a form such as CCMAIL: BruceKeever.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-othermailbox
        /// </summary>
        public List<string>? OtherMailbox { get; set; }

        /// <summary>
        /// Additional names for a user. For example, middle name, patronymic, matronymic, or others.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-middlename
        /// </summary>
        public string? OtherName { get; set; }

        /// <summary>
        /// The user's title.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-personaltitle
        /// </summary>
        public string? PersonalTitle { get; set; }

        /// <summary>
        /// A list of alternate facsimile numbers.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-otherfacsimiletelephonenumber
        /// </summary>
        public List<string>? PhoneFaxOther { get; set; }

        /// <summary>
        /// A list of alternate home phone numbers.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-otherhomephone
        /// </summary>
        public List<string>? PhoneHomeOther { get; set; }

        /// <summary>
        /// The user's main home phone number.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-homephone
        /// </summary>
        public string? PhoneHomePrimary { get; set; }

        /// <summary>
        /// The list of alternate TCP/IP addresses for the phone. Used by Telephony.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-otheripphone
        /// </summary>
        public List<string>? PhoneIpOther { get; set; }

        /// <summary>
        /// The TCP/IP address for the phone. Used by Telephony.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-ipphone
        /// </summary>
        public string? PhoneIpPrimary { get; set; }

        /// <summary>
        /// The primary ISDN.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-primaryinternationalisdnnumber
        /// </summary>
        public string? PhoneISDNPrimary { get; set; }

        /// <summary>
        /// A list of alternate mobile phone numbers.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-othermobile
        /// </summary>
        public List<string>? PhoneMobileOther { get; set; }

        /// <summary>
        /// The primary mobile phone number.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-mobile
        /// </summary>
        public string? PhoneMobilePrimary { get; set; }

        /// <summary>
        /// A list of alternate office phone numbers.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-othertelephone
        /// </summary>
        public List<string>? PhoneOfficeOther { get; set; }

        /// <summary>
        /// A list of alternate pager numbers.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-otherpager
        /// </summary>
        public List<string>? PhonePagerOther { get; set; }

        /// <summary>
        /// The primary pager number.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-pager
        /// </summary>
        public string? PhonePagerPrimary { get; set; }

        /// <summary>
        /// Contains the office location in the user's place of business.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-physicaldeliveryofficename
        /// </summary>
        public string? PhysicalDeliveryOfficeName { get; set; }

        /// <summary>
        /// An image of the user. A space-efficient format like JPEG or GIF is recommended.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-thumbnailphoto
        /// </summary>
        public byte[]? Picture { get; set; }

        /// <summary>
        /// The mailing address for the object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-postaladdress
        /// </summary>
        public List<string>? PostalAddress { get; set; }

        /// <summary>
        /// The postal or zip code for mail delivery.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-postalcode
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// The post office box number for this object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-postofficebox
        /// </summary>
        public List<string>? PostOfficeBox { get; set; }

        /// <summary>
        /// The X.500-preferred way to deliver to addressee.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-preferreddeliverymethod
        /// </summary>
        public List<Int32>? PreferredDeliveryMethod { get; set; }

        /// <summary>
        /// Specifies a mnemonic for an address associated with an object at a particular city location.
        /// The mnemonic is registered in the country/region in which the city is located and is used in the
        /// provision of the Public Telegram Service
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-registeredaddress
        /// </summary>
        public List<byte[]>? RegisteredAddress { get; set; }

        /// <summary>
        /// The name of a user's state or province.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-st
        /// </summary>
        public string? StateOrProvinceName { get; set; }

        /// <summary>
        /// The street address.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-street
        /// </summary>
        public string? StreetAddress { get; set; }

        /// <summary>
        /// Specifies the Teletex terminal identifier and, optionally, parameters, for a teletex terminal associated with an object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-teletexterminalidentifier
        /// </summary>
        public List<byte[]>? TeletexTerminalIdentifier { get; set; }

        /// <summary>
        /// A list of alternate telex numbers.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-telexnumber
        /// </summary>
        public List<byte[]>? TelexNumber { get; set; }

        /// <summary>
        /// The primary telex number.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-primarytelexnumber
        /// </summary>
        public string? TelexPrimary { get; set; }

        /// <summary>
        /// The country/region in which the user is located.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-co
        /// </summary>
        public string? TextCountry { get; set; }

        /// <summary>
        /// Contains the user's job title.
        /// This property is commonly used to indicate the formal job title, such as Senior Programmer, rather than occupational class, such as programmer.
        /// It is not typically used for suffix titles such as Esq. or DDS.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// The user's comments.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-comment
        /// </summary>
        public string? UserComment { get; set; }

        /// <summary>
        /// The X.121 address for an object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-x121address
        /// </summary>
        public List<byte[]>? X121Address { get; set; }

        #endregion

        /// <summary>
        /// List if attributes to extract when querying Person object.
        /// </summary>
        protected static new string[] Attributes {
            get {
                return Person.Attributes.Concat(new string[] {
                    "streetAddress",
                    "homePostalAddress",
                    "assistant",
                    "company",
                    "countryCode",
                    "c",
                    "department",
                    "destinationIndicator",
                    "division",
                    "mail",
                    "employeeID",
                    "facsimileTelephoneNumber",
                    "generationQualifier",
                    "givenName",
                    "initials",
                    "internationalISDNNumber",
                    "l",
                    "thumbnailLogo",
                    "manager",
                    "msDS-AllowedToActOnBehalfOfOtherIdentity",
                    "mhsORAddress",
                    "ou",
                    "o",
                    "otherMailbox",
                    "middleName",
                    "personalTitle",
                    "otherFacsimileTelephoneNumber",
                    "otherHomePhone",
                    "homePhone",
                    "otherIpPhone",
                    "ipPhone",
                    "primaryInternationalISDNNumber",
                    "otherMobile",
                    "mobile",
                    "otherTelephone",
                    "otherPager",
                    "pager",
                    "physicalDeliveryOfficeName",
                    "thumbnailPhoto",
                    "postalAddress",
                    "postalCode",
                    "postOfficeBox",
                    "preferredDeliveryMethod",
                    "registeredAddress",
                    "st",
                    "street",
                    "teletexTerminalIdentifier",
                    "telexNumber",
                    "primaryTelexNumber",
                    "co",
                    "title",
                    "comment",
                    "x121Address"
                }).ToArray();
            }
        }

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public new OrganizationalPerson FromResult(SearchResult Result) {

            // Get Person abstract attributes.
            base.FromResult(Result);

            // Get Organizational-Person abstract attributes.
            this.Address                                 = Util.GetTypeFromResult<string>(Result      , "streetAddress");
            this.AddressHome                             = Util.GetTypeFromResult<string>(Result      , "homePostalAddress");
            this.Assistant                               = Util.GetTypeFromResult<string>(Result      , "assistant");
            this.Company                                 = Util.GetTypeFromResult<string>(Result      , "company");
            this.CountryCode                             = Util.GetTypeFromResult<int>(Result         , "countryCode");
            this.CountryName                             = Util.GetTypeFromResult<string>(Result      , "c");
            this.Department                              = Util.GetTypeFromResult<string>(Result      , "department");
            this.DestinationIndicator                    = Util.GetListOfTypeFromResult<string>(Result, "destinationIndicator");
            this.Division                                = Util.GetTypeFromResult<string>(Result      , "division");
            this.EMailAddress                            = Util.GetTypeFromResult<string>(Result      , "mail");
            this.EmployeeID                              = Util.GetTypeFromResult<string>(Result      , "employeeID");
            this.FacsimileTelephoneNumber                = Util.GetTypeFromResult<string>(Result      , "facsimileTelephoneNumber");
            this.GenerationQualifier                     = Util.GetTypeFromResult<string>(Result      , "generationQualifier");
            this.GivenName                               = Util.GetTypeFromResult<string>(Result      , "givenName");
            this.Initials                                = Util.GetTypeFromResult<string>(Result      , "initials");
            this.InternationalISDNNumber                 = Util.GetListOfTypeFromResult<string>(Result, "internationalISDNNumber");
            this.LocalityName                            = Util.GetTypeFromResult<string>(Result      , "l");
            this.Logo                                    = Util.GetTypeFromResult<byte[]>(Result      , "thumbnailLogo");
            this.Manager                                 = Util.GetTypeFromResult<string>(Result      , "manager");
            this.MSDSAllowedToActOnBehalfOfOtherIdentity = Util.GetSecDescriptorFromResult(Result, "msDS-AllowedToActOnBehalfOfOtherIdentity");
            this.MHSORAddress                            = Util.GetListOfTypeFromResult<string>(Result, "mhsORAddress");
            this.OrganizationalUnitName                  = Util.GetListOfTypeFromResult<string>(Result, "ou");
            this.OrganizationName                        = Util.GetTypeFromResult<string>(Result      , "o");
            this.OtherMailbox                            = Util.GetListOfTypeFromResult<string>(Result, "otherMailbox");
            this.OtherName                               = Util.GetTypeFromResult<string>(Result      , "middleName");
            this.PersonalTitle                           = Util.GetTypeFromResult<string>(Result      , "personalTitle");
            this.PhoneFaxOther                           = Util.GetListOfTypeFromResult<string>(Result, "otherFacsimileTelephoneNumber");
            this.PhoneHomeOther                          = Util.GetListOfTypeFromResult<string>(Result, "otherHomePhone");
            this.PhoneHomePrimary                        = Util.GetTypeFromResult<string>(Result      , "homePhone");
            this.PhoneIpOther                            = Util.GetListOfTypeFromResult<string>(Result, "otherIpPhone");
            this.PhoneIpPrimary                          = Util.GetTypeFromResult<string>(Result      , "ipPhone");
            this.PhoneISDNPrimary                        = Util.GetTypeFromResult<string>(Result      , "primaryInternationalISDNNumber");
            this.PhoneMobileOther                        = Util.GetListOfTypeFromResult<string>(Result, "otherMobile");
            this.PhoneMobilePrimary                      = Util.GetTypeFromResult<string>(Result      , "mobile");
            this.PhoneOfficeOther                        = Util.GetListOfTypeFromResult<string>(Result, "otherTelephone");
            this.PhonePagerOther                         = Util.GetListOfTypeFromResult<string>(Result, "otherPager");
            this.PhonePagerPrimary                       = Util.GetTypeFromResult<string>(Result      , "pager");
            this.PhysicalDeliveryOfficeName              = Util.GetTypeFromResult<string>(Result      , "physicalDeliveryOfficeName");
            this.Picture                                 = Util.GetTypeFromResult<byte[]>(Result      , "thumbnailPhoto");
            this.PostalAddress                           = Util.GetListOfTypeFromResult<string>(Result, "postalAddress");
            this.PostalCode                              = Util.GetTypeFromResult<string>(Result      , "postalCode");
            this.PostOfficeBox                           = Util.GetListOfTypeFromResult<string>(Result, "postOfficeBox");
            this.PreferredDeliveryMethod                 = Util.GetListOfTypeFromResult<int>(Result   , "preferredDeliveryMethod");
            this.RegisteredAddress                       = Util.GetListOfTypeFromResult<byte[]>(Result, "registeredAddress");
            this.StateOrProvinceName                     = Util.GetTypeFromResult<string>(Result      , "st");
            this.StreetAddress                           = Util.GetTypeFromResult<string>(Result      , "street");
            this.TeletexTerminalIdentifier               = Util.GetListOfTypeFromResult<byte[]>(Result, "teletexTerminalIdentifier");
            this.TelexNumber                             = Util.GetListOfTypeFromResult<byte[]>(Result, "telexNumber");
            this.TelexPrimary                            = Util.GetTypeFromResult<string>(Result      , "primaryTelexNumber");
            this.TextCountry                             = Util.GetTypeFromResult<string>(Result      , "co");
            this.Title                                   = Util.GetTypeFromResult<string>(Result      , "title");
            this.UserComment                             = Util.GetTypeFromResult<string>(Result      , "comment");
            this.TelexNumber                             = Util.GetListOfTypeFromResult<byte[]>(Result, "x121Address");

            // Success
            return this;
        }
    }
}
