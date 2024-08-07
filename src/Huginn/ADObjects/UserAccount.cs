using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace Huginn.ADObjects {

    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public sealed class UserAccount {

        /// <summary>
        /// Microsoft AD principal object for a user account.
        /// </summary>
        public UserPrincipal _Principal { get; private set; }

        #region Public Methods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Enabled"></param>
        public UserAccount(
            string SamAccountName = "CHANGEME",
            string Name           = "CHANGEME",
            string Description    = "CHANGEME",
            bool   Enabled        = false
         ) {
            // Build a new UserPrincipal.
            var Context = new PrincipalContext(ContextType.Domain);
            var User    = new UserPrincipal(Context);

            // Add additional properties if specified.
            User.SamAccountName = SamAccountName;
            User.Name           = Name;
            User.Description    = Description;
            User.Enabled        = Enabled;

            // Store the UserPrincipal
            this._Principal = User;
        }

        /// <summary>
        /// Commit modifications on a user principal.
        /// This method can be used to modify an already existing user principal or to create a new one.
        /// </summary>
        public bool Commit() {

            // Make sure enough information have been provided.
            if (string.IsNullOrEmpty(this._Principal.SamAccountName)
                || string.IsNullOrEmpty(this._Principal.Name)
            ) {
                return false;
            }

            // Save the new principal object.
            try {
                this._Principal.Save();
                return true;
            }
            // Exception is thrown whenever a user already exist.
            catch (PrincipalExistsException _) {
                Logger.Verbose($"User already exist {this._Principal.SamAccountName}");
                return true;
            }
            // Any other exception
            catch (Exception ex) {
                Logger.Verbose(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Remove a user account from the domain.
        /// </summary>
        public bool Remove() {
            try {
                this._Principal.Delete();
                return true;
            } catch (PrincipalExistsException ex) {
                Logger.Verbose(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Convert current object to a JSON object.
        /// </summary>
        /// <returns>The JSON object.</returns>
        public JsonObject ToJSON() {
            var Obj = new JsonObject() {
                ["sAMAccountName"]     = this._Principal.SamAccountName,
                ["userAccountControl"] = this.GetUserAccountControl(),
                ["sid"]                = this._Principal.Sid.ToString(),
                ["enabled"]            = this._Principal.Enabled
            };

            // Add the logonHours
            if (this._Principal.PermittedLogonTimes == null) {
                Obj.Add(new KeyValuePair<string, JsonNode?>("logonHours", null));
            } else {
                Obj.Add(new KeyValuePair<string, JsonNode?>("logonHours", new JsonArray() {
                    this._Principal.PermittedLogonTimes
                }));
            }

            // Success.
            return Obj;
        }

        /// <summary>
        /// Get the object security descriptor.
        /// </summary>
        public ActiveDirectorySecurity? GetObjectSecurity() {

            // Make sure the underlying object is present.
            if (this._Principal.GetUnderlyingObjectType() != typeof(DirectoryEntry)) {
                return null;
            }

            // Type cast and get the SD
            DirectoryEntry Obj = (DirectoryEntry)this._Principal.GetUnderlyingObject();
            return Obj?.ObjectSecurity;
        }
        
        /// <summary>
        /// Get the user account control bits of the user principal.
        /// </summary>
        public int? GetUserAccountControl() {
            // Make sure the underlying object is present.
            if (this._Principal.GetUnderlyingObjectType() != typeof(DirectoryEntry)) {
                return null;
            }

            // Type cast and get the property value
            DirectoryEntry Obj = (DirectoryEntry)this._Principal.GetUnderlyingObject();
            if (!Obj.Properties.Contains("userAccountControl")) {
                return null;
            }

            return (int?)Obj.Properties["userAccountControl"].Value;
        }

        /// <summary>
        /// Get Security Account Manager (SAM) account type.
        /// </summary>
        public int? GetSamAccountType() {
            // Make sure the underlying object is present.
            if (this._Principal.GetUnderlyingObjectType() != typeof(DirectoryEntry)) {
                return null;
            }

            // Type cast and get the property value
            DirectoryEntry Obj = (DirectoryEntry)this._Principal.GetUnderlyingObject();
            if (!Obj.Properties.Contains("sAMAccountType")) {
                return null;
            }

            return (int?)Obj.Properties["sAMAccountType"].Value;
        }

        /// <summary>
        /// Check whether the object is a critical object.
        /// </summary>
        public bool? IsCriticalObject() {
            // Make sure the underlying object is present.
            if (this._Principal.GetUnderlyingObjectType() != typeof(DirectoryEntry)) {
                return null;
            }

            // Type cast and get the property value
            DirectoryEntry Obj = (DirectoryEntry)this._Principal.GetUnderlyingObject();
            if (!Obj.Properties.Contains("isCriticalSystemObject")) {
                return null;
            }

            return (bool?)Obj.Properties["isCriticalSystemObject"].Value;
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Find the list of all AD user accounts within the domain.
        /// </summary>
        public static List<UserAccount> FindAllAccounts() {

            // Build the principal searcher
            var context   = new PrincipalContext(ContextType.Domain);
            var principal = new UserPrincipal(context);
            var searcher  = new PrincipalSearcher(principal);

            // Reformat all the users found.
            List<UserAccount> UserAccounts = new List<UserAccount>();
            foreach (var User in searcher.FindAll()) {

                // Make sure that the user returned is not null.
                if (User is null) {
                    continue;
                }

                // Make sure that the Principal is a UserPrincipal
                if (User.GetType() != typeof(UserPrincipal)) {
                    continue;
                }

                // Push to the list the new user.
                UserAccounts.Add(new UserAccount() {
                    _Principal = (UserPrincipal)User
                });
            }

            // Success
            searcher.Dispose();
            return UserAccounts;
        }

        /// <summary>
        /// Find one user account based on its SAMAccountName.
        /// </summary>
        /// <returns></returns>
        public static UserAccount? FindAccount(string SamAccountName) {

            // Build the principal searcher
            var context   = new PrincipalContext(ContextType.Domain);
            var principal = new UserPrincipal(context) {
                SamAccountName = SamAccountName
            };
            var searcher = new PrincipalSearcher(principal);

            // Get the principal object and then extract the properties.
            var Principal = searcher.FindOne();
            if (Principal is null) {
                return null;
            }

            // Make sure that the Principal is a UserPrincipal
            if (Principal.GetType() != typeof(UserPrincipal)) {
                return null;
            }

            // Create a new UserAccount
            return new UserAccount() {
                _Principal = (UserPrincipal)Principal
            };
        }
        
        /// <summary>
        /// Create a new user account from a JSON element.
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        public static UserAccount? FromJSON(JsonElement Element) {

            // Make sure the element is not null or undefined.
            if (Element.ValueKind == JsonValueKind.Null
                || Element.ValueKind == JsonValueKind.Undefined
            ) {
                return null;
            }

            // List of variables that are required.
            JsonElement Node           = new JsonElement();
            string?     SamAccountName = null;
            string?     Name           = null;
            string?     Description    = null;
            bool?       Enabled        = null;

            // Get the information
            if (!Element.TryGetProperty("sAMAccountName", out Node)) {
                Logger.Error("Failed to get 'sAMAccountName' property.");
                return null;
            }
            SamAccountName = Node.GetString();

            if (!Element.TryGetProperty("name", out Node)) {
                Logger.Error("Failed to get 'name' property.");
                return null;
            }
            Name = Node.GetString();

            if (!Element.TryGetProperty("description", out Node)) {
                Logger.Error("Failed to get 'description' property.");
                return null;
            }
            Description = Node.GetString();

            if (!Element.TryGetProperty("enabled", out Node)) {
                Logger.Error("Failed to get 'enabled' property.");
                return null;
            }
            Enabled = Node.GetBoolean();

            // Create the user account object.
            if (SamAccountName == null || Name == null || Description == null || Enabled == null) {
                return null;
            }
            return new UserAccount(
                SamAccountName,
                Name,
                Description,
                (bool)Enabled
           );
        }
        #endregion
    }
}
