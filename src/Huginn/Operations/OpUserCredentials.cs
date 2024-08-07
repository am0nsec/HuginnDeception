using Huginn.ADObjects.Auxiliary;
using Huginn.ADObjects.Structural;
using System.CommandLine;
using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Huginn.Operations {

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public sealed class OpUserCredentials {

        #region Sub-Command Options

        /// <summary>
        /// Name of the target to re-purpose.
        /// </summary>
        private static readonly Option<string> User = new(
            name       : "--user",
            description: "Name of the target to re-purpose."
        );

        /// <summary>
        /// The security identifier to allow access.
        /// </summary>
        private static readonly Option<string> Principal = new(
            name       : "--principal",
            description: "The user principal to allow access from."
        );

        /// <summary>
        /// The technique to apply.
        /// </summary>
        private static readonly Option<OpUserCredentialsTechnique> Technique = new(
            name       : "--technique",
            description: "The technique to apply."
        );

        #endregion

        /// <summary>
        /// The list of techniques that can be applied to the certificate template.
        /// </summary>
        private enum OpUserCredentialsTechnique {
            Description,
            GMSA,        // Group Policy Preference
            FPC,         // Force Password Change
        }

        /// <summary>
        /// Add the `usr-credentials` sub-command to the CLI.
        /// </summary>
        public static void AddUserCredentialsSubCommand(RootCommand Root) {

            // Define the options that are required.
            OpUserCredentials.User.IsRequired      = true;

            // Define the sub command.
            var SubCommand = new Command("usr-credentials", "Generate a user deception artefact.") {
                OpUserCredentials.User,
                OpUserCredentials.Principal,
                OpUserCredentials.Technique
            };
            Root.AddCommand(SubCommand);

            // Define the handler for this sub command.
            SubCommand.SetHandler(
                OpUserCredentials.AddUserWithCredentials,
                OpUserCredentials.User,
                OpUserCredentials.Principal,
                OpUserCredentials.Technique
            );
        }

        /// <summary>
        /// Generate a user deception artefact.
        /// </summary>
        private static void AddUserWithCredentials(
            string UserName,
            string PrincipalName,
            OpUserCredentialsTechnique Technique
        ) {
            // Banner for style
            Logger.Write("");
            Logger.Write("--[ User-Credentials ]---------------------------");
            Logger.Write("");

            // Check that both the target user and the principal exist.
            var UserSp = SecurityPrincipal.Find(UserName);
            if (UserSp == null && !Util.IsUser(UserSp)) {
                Logger.Error($"Cannot find or invalid user: {UserName}");
                return;
            }
            Util.DisplaySecurityPrincipalInfo(UserSp);
            Logger.Write("");

            // Get the user
            User? UserObj = ADObjects.Structural.User.Find(UserName);
            if (UserObj == null) {
                Logger.Error($"Cannot find user: {UserName}");
                return;
            }

            // Apply the technique requested.
            switch (Technique) {
                case OpUserCredentialsTechnique.Description: {
                    Logger.Info("User-Credentials object ACEs technique: Description");

                    // Create the new description for the object.
                    var DescriptionLine = "old pwd: " + Util.GetRandomString();
                    List<string> Description = new List<string>() {
                        DescriptionLine
                    };
                    Logger.Info($"New description: {DescriptionLine}");

                    // Update the object.
                    bool Success = UserObj.UpdateAttribute("description", Description);
                    if (!Success) {
                        Logger.Error("Failed to update object attribute.");
                        return;
                    }
                    break;
                }
                case OpUserCredentialsTechnique.FPC: {
                    var PrincipalSp = SecurityPrincipal.Find(PrincipalName);
                    if (PrincipalSp == null) {
                        Logger.Error($"Cannot find or invalid principal: {PrincipalName}");
                        return;
                    }
                    Util.DisplaySecurityPrincipalInfo(PrincipalSp);
                    Logger.Write("");

                    Logger.Info("User-Credentials object ACEs technique: ForcePasswordChange");

                    // Modify the security descriptor of the principal.
                    var SecurityDescriptor = UserObj?.NTSecurityDescriptor;
                    SecurityDescriptor!.AddAccessRule(
                        new ActiveDirectoryAccessRule(
                            PrincipalSp.ObjectSid,
                            (ActiveDirectoryRights.ExtendedRight),
                            AccessControlType.Allow,
                            new Guid("00299570-246d-11d0-a768-00aa006e0529"),
                            ActiveDirectorySecurityInheritance.None,
                            new Guid()
                        )
                    );
                    SecurityDescriptor!.AddAccessRule(
                        new ActiveDirectoryAccessRule(
                            PrincipalSp.ObjectSid,
                            (ActiveDirectoryRights.ExtendedRight),
                            AccessControlType.Deny,
                            new Guid("00299570-246d-11d0-a768-00aa006e0529"),
                            ActiveDirectorySecurityInheritance.None,
                            new Guid()
                        )
                    );

                    // Display the new SDDL
                    Logger.Info("User-Credentials object SDDL: ");
                    Logger.Write(SecurityDescriptor!.GetSecurityDescriptorSddlForm(AccessControlSections.Access));
                    Logger.Write("");

                    // Update the attribute.
                    bool Success = UserObj.UpdateAttribute(
                        "nTSecurityDescriptor", 
                        SecurityDescriptor.GetSecurityDescriptorBinaryForm()
                    );
                    if (!Success) {
                        Logger.Error("Failed to update object attribute.");
                        return;
                    }
                    break;
                }
                case OpUserCredentialsTechnique.GMSA: {
                    var PrincipalSp = SecurityPrincipal.Find(PrincipalName);
                    if (PrincipalSp == null) {
                        Logger.Error($"Cannot find or invalid principal: {PrincipalName}");
                        return;
                    }
                    Util.DisplaySecurityPrincipalInfo(PrincipalSp);
                    Logger.Write("");

                    Logger.Info("User-Credentials object ACEs technique: GMSA");

                    // Get the GMSA object
                    var Gmsa = MSDSGroupManagedServiceAccount.Find(PrincipalName);
                    if (Gmsa == null) {
                        Logger.Error("Cannot find the group managed service account object.");
                        return;
                    }

                    // Modify the security descriptor to allow user to read the GMSA password.\
                    var SecurityDescriptor = Gmsa.MSDSGroupMSAMembership;
                    SecurityDescriptor!.AddAccessRule(
                        new ActiveDirectoryAccessRule(
                            UserSp.ObjectSid, ActiveDirectoryRights.GenericAll, AccessControlType.Allow
                        )
                    );

                    // Display the new SDDL
                    Logger.Info("User-Credentials object SDDL: ");
                    Logger.Write(SecurityDescriptor!.GetSecurityDescriptorSddlForm(AccessControlSections.Access));
                    Logger.Write("");

                    // Update the attribute.
                    bool Success = Gmsa.UpdateAttribute(
                        "msDS-GroupMSAMembership",
                        SecurityDescriptor.GetSecurityDescriptorBinaryForm()
                    );
                    if (!Success) {
                        Logger.Error("Failed to update object attribute.");
                        return;
                    }
                    break;
                }
                default: {
                    Logger.Error("Invalid technique supplied.");
                    return;
                }
            }

            // Success
            Logger.Write("");
            Logger.Success("Operation was conducted successfully.");
            return;
        }
    }
}
