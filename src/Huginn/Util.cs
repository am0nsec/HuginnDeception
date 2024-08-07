using Huginn.ADObjects;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Huginn.ADObjects.Auxiliary;
using System.DirectoryServices.AccountManagement;

namespace Huginn {

    /// <summary>
    /// Collection of utility methods.
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static class Util {

        /// <summary>
        /// Generate a random string.
        /// </summary>
        public static string GetRandomString(int length = 0x10) {
            var chr =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";

            var str = new char[length];
            var rdm = new Random();

            for (int cx = 0; cx < str.Length; cx++) {
                str[cx] = chr[rdm.Next(chr.Length)];
            }

            return new String(str);
        }

        /// <summary>
        /// Display SecurityIdentifier object information.
        /// </summary>
        public static void DisplaySecurityPrincipalInfo(SecurityPrincipal? Sp) {
            if (Sp == null) {
                return;
            }

            switch (Sp.SAMAccountType) {
                case SecurityPrincipal.SAM_ACCOUNT_TYPE.SAM_ALIAS_OBJECT: {
                        Logger.Info("Security-Principal is a Alias object.");
                        break;
                    }
                case SecurityPrincipal.SAM_ACCOUNT_TYPE.SAM_GROUP_OBJECT: {
                        Logger.Info("Security-Principal is a Group object.");
                        break;
                    }
                case SecurityPrincipal.SAM_ACCOUNT_TYPE.SAM_USER_OBJECT: {
                        Logger.Info("Security-Principal is a User object.");
                        break;
                    }
                case SecurityPrincipal.SAM_ACCOUNT_TYPE.SAM_MACHINE_ACCOUNT: {
                        Logger.Info("Security-Principal is a Machine object.");
                        break;
                    }
            }
            Logger.Info($"Security-Principal SID : {Sp?.ObjectSid.Value}");
        }

        /// <summary>
        /// Check if a security principal is a user.
        /// </summary>
        public static bool IsUser(SecurityPrincipal? Sp) {
            if (Sp == null) {
                return false;
            }

            return Sp.SAMAccountType == SecurityPrincipal.SAM_ACCOUNT_TYPE.SAM_USER_OBJECT;
        }

        /// <summary>
        /// Convert a domain name to a distinguished name
        /// </summary>
        public static string? NameToDistinguishedName(string Name) {
            if (string.IsNullOrEmpty(Name)) {
                return null;
            }

            // Replace all the dots with ',DC='
            var dn = Name.Replace(".", ",DC=");
            dn = $"DC={dn}";

            return dn;
        }

        /// <summary>
        /// Get a basic data type from a result property.
        /// 
        /// Works for: Int32, Int64, String, Boolean, byte[]
        /// </summary>
        public static T? GetTypeFromResult<T>(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return default;
            }
            return (T)(Result.Properties[Name][0]);
        }

        /// <summary>
        /// Get a basic list data type from a result property.
        /// 
        /// Works for: List<bool>, List<int>, List<long>, List<string>, List<byte[]>
        /// </summary>
        public static List<T>? GetListOfTypeFromResult<T>(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return null;
            }
            var v = new List<T>();
            for (var x = 0x00; x < Result.Properties[Name].Count; x++) {
                v.Add((T)Result.Properties[Name][x]);
            }
            return v;
        }

        /// <summary>
        /// Get a date from a result property.
        /// </summary>
        public static DateTime? GetDateTimeFromResult(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return null;
            }

            var v = new DateTime();
            if (!DateTime.TryParse(Result.Properties[Name][0].ToString(), out v)) {
                v = DateTime.FromBinary((Int64)Result.Properties[Name][0]);
                if (v == DateTime.MinValue) {
                    return null;
                }
            }
            return v;
        }

        /// <summary>
        /// Get a date from a result property.
        /// </summary>
        public static Guid? GetGuidFromResult(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return null;
            }
            return new Guid((byte[])Result.Properties[Name][0]);
        }

        /// <summary>
        /// Get an Oid from a result property.
        /// </summar>
        public static Oid? GetOidFromResult(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return null;
            }
            return new Oid(Result.Properties[Name][0].ToString());
        }

        /// <summary>
        /// Get a security descriptor from a result property.
        /// </summary
        public static ActiveDirectorySecurity? GetSecDescriptorFromResult(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return null;
            }

            var v = new ActiveDirectorySecurity();
            v.SetSecurityDescriptorBinaryForm(
                (byte[])Result.Properties[Name][0]
            );
            return v;
        }

        /// <summary>
        /// Get a security identifier from a result property.
        /// </summary>
        public static SecurityIdentifier? GetSecIdentifierFromResult(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return null;
            }
            return new SecurityIdentifier(
                (byte[])Result.Properties[Name][0],
                0x00
            );
        }

        /// <summary>
        /// Get a list of security descriptor from a result property.
        /// </summary
        public static List<ActiveDirectorySecurity>? GetListOfSecDescriptorFromResult(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return null;
            }

            var v = new List<ActiveDirectorySecurity>();
            for (var x = 0x00; x < Result.Properties[Name].Count; x++) {
                var ads = new ActiveDirectorySecurity();
                ads.SetSecurityDescriptorBinaryForm(
                    (byte[])Result.Properties[Name][x]
                );
                v.Add(ads);
            }
            return v;
        }

        /// <summary>
        /// Get a list of X509 certificates from a result property.
        /// </summary>
        public static List<X509Certificate2>? GetCaCertificateFromResult(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return null;
            }

            var v = new List<X509Certificate2>();
            for (var x = 0x00; x < Result.Properties[Name].Count; x++) {
                v.Add(
                    new X509Certificate2(
                        (byte[])Result.Properties[Name][x]
                    )
                );
            }
            return v;
        }

        /// <summary>
        /// Get a list of security identifier from a result property.
        /// </summary>
        public static List<SecurityIdentifier>? GetListOfSecIdentifierFromResult(SearchResult Result, string Name) {
            if (Result == null || !Result.Properties.Contains(Name)) {
                return null;
            }

            var v = new List<SecurityIdentifier>();
            for (var x = 0x00; x < Result.Properties[Name].Count; x++) {
                v.Add(new SecurityIdentifier(
                    (byte[])Result.Properties[Name][x],
                    0x00
                ));
            }
            return v;
        }
    }
}
