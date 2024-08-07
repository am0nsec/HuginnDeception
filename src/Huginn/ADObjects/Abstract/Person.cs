using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huginn.ADObjects.Abstract {

    /// <summary>
    /// Contains personal information about a user.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-person
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class Person : Top {

        #region Attributes

        /// <summary>
        /// List of distinguished names that are related to an object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-seealso
        /// </summary>
        public List<string>? SeeAlso { get; set; }

        /// <summary>
        /// This attribute contains the family or last name for a user.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-sn
        /// </summary>
        public string? Surname { get; set; }

        /// <summary>
        /// The primary telephone number.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-telephonenumber
        /// </summary>
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// The user's password in UTF-8 format. This is a write-only attribute.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-userpassword
        /// </summary>
        public List<byte[]>? UserPassword { get; set; }

        #endregion

        /// <summary>
        /// List if attributes to extract when querying Person object.
        /// </summary>
        protected static new string[] Attributes {  get {
                return Top.Attributes.Concat(new string[] {
                    "seeAlso",
                    "sn",
                    "telephoneNumber",
                    "userPassword"
                }).ToArray();
            } 
        }

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public new Person FromResult(SearchResult Result) {

            // Get Top auxiliary attributes.
            base.FromResult(Result);

            // Get the Person abstract attributes.
            this.SeeAlso         = Util.GetListOfTypeFromResult<string>(Result, "seeAlso");
            this.Surname         = Util.GetTypeFromResult<string>(Result      , "sn");
            this.TelephoneNumber = Util.GetTypeFromResult<string>(Result      , "telephoneNumber");
            this.UserPassword    = Util.GetListOfTypeFromResult<byte[]>(Result, "userPassword");

            // Success
            return this;
        }

    }
}
