using Huginn.ADObjects.Auxiliary;
using Huginn.ADObjects.Structural;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Huginn.Operations {

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public sealed class OpPkiTemplate {

        #region Sub-Command Options

        /// <summary>
        /// Name of the certificate template to clone from the domain.
        /// </summary>
        private static readonly Option<string> Template = new(
            name       : "--template",
            description: "Name of the template to clone."
        );

        /// <summary>
        /// Name of the new certificate template to create.
        /// </summary>
        private static readonly Option<string> Name = new(
            name       : "--name",
            description: "Name of the new certificate template to create."
        );

        /// <summary>
        /// The security identifier to allow/deny
        /// </summary>
        private static readonly Option<string> Principal = new(
            name       : "--principal",
            description: "The user principal to allow/deny."
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
        /// The list of techniques that can be applied to the certificate template.
        /// </summary>
        private enum OpPkiTemplateTechnique {
            ESC1,
            ESC4
        }

        /// <summary>
        /// Add the `pki-template` sub-command to the CLI.
        /// </summary>
        public static void AddCertificateTemplateSubCommand(RootCommand Root) {
            // Define the options that are required.
            OpPkiTemplate.Template.IsRequired  = true;
            OpPkiTemplate.Principal.IsRequired = true;

            // Define the sub command.
            var SubCommand = new Command("pki-template", "Generate a PKI-Certificate-Template") {
                OpPkiTemplate.Name,
                OpPkiTemplate.Template,
                OpPkiTemplate.Principal,
                OpPkiTemplate.Technique
            };
            Root.AddCommand(SubCommand);

            // Define the handler for this sub command.
            SubCommand.SetHandler(
                OpPkiTemplate.AddCertificateTemplate,
                OpPkiTemplate.Name,
                OpPkiTemplate.Template,
                OpPkiTemplate.Principal,
                OpPkiTemplate.Technique
            );
        }

        /// <summary>
        /// Add a new PKI-Certificate-Template object to the domain and then update the `certificateTemplates` attribute
        /// of each PKI-Enrollment-Service objects.
        /// </summary>
        private static void AddCertificateTemplate(
            string Name,
            string TemplateName, 
            string PrincipalName, 
            OpPkiTemplateTechnique Technique
        ) {

            // Banner for style
            Logger.Write("");
            Logger.Write("--[ PKI-Certificate-Template ]-------------------");
            Logger.Write("");

            // Set the default name if non as been specified.
            if (String.IsNullOrEmpty(Name)) {
                Name = $"{TemplateName} Clone";
            }

            // Check that the principal requested does exist.
            var Principal = SecurityPrincipal.Find(PrincipalName);
            if (Principal == null) {
                Logger.Error($"Cannot find principal: {PrincipalName}");
                return;
            } else {
                Util.DisplaySecurityPrincipalInfo(Principal);
                Logger.Write("");
            }

            // Get the certificate template to clone.
            PKICertificateTemplate? TemplateToClone = PKICertificateTemplate.Find(TemplateName);
            if (TemplateToClone == null) {
                Logger.Error($"PKI-Certificate-Template object to clone not found: {TemplateName}");
                return;
            }

            // Clone the requested certificate template and modify the new name
            PKICertificateTemplate ClonedTemplate = TemplateToClone.Clone();
            ClonedTemplate.CommonName  = Name;
            ClonedTemplate.DisplayName = Name;

            // Add the two ACEs
            switch(Technique) {
                case OpPkiTemplateTechnique.ESC4: {
                    Logger.Info("PKI-Certificate-Template object ACEs technique: Enroll");
                    ClonedTemplate.NTSecurityDescriptor!.AddAccessRule(new ActiveDirectoryAccessRule(
                        Principal.ObjectSid,
                        (ActiveDirectoryRights.ExtendedRight),
                        AccessControlType.Allow,
                        new Guid("0e10c968-78fb-11d2-90d4-00c04f79dc55"),
                        ActiveDirectorySecurityInheritance.None,
                        new Guid()
                    ));
                    ClonedTemplate.NTSecurityDescriptor!.AddAccessRule(new ActiveDirectoryAccessRule(
                        Principal.ObjectSid,
                        (ActiveDirectoryRights.ReadProperty | ActiveDirectoryRights.WriteProperty),
                        AccessControlType.Allow,
                        ActiveDirectorySecurityInheritance.None,
                        new Guid()
                    ));
                    ClonedTemplate.NTSecurityDescriptor!.AddAccessRule(new ActiveDirectoryAccessRule(
                        Principal.ObjectSid,
                        (ActiveDirectoryRights.ExtendedRight),
                        AccessControlType.Deny,
                        new Guid("0e10c968-78fb-11d2-90d4-00c04f79dc55"),
                        ActiveDirectorySecurityInheritance.None,
                        new Guid()
                    ));
                    ClonedTemplate.NTSecurityDescriptor!.AddAccessRule(new ActiveDirectoryAccessRule(
                        Principal.ObjectSid,
                        (ActiveDirectoryRights.WriteDacl | ActiveDirectoryRights.WriteOwner),
                        AccessControlType.Deny,
                        ActiveDirectorySecurityInheritance.None,
                        new Guid()
                    ));
                    break;
                }
                case OpPkiTemplateTechnique.ESC1: {
                    Logger.Info("PKI-Certificate-Template object ACEs technique: WriteProperty");
                    ClonedTemplate.NTSecurityDescriptor!.AddAccessRule(new ActiveDirectoryAccessRule(
                        Principal.ObjectSid,
                        (ActiveDirectoryRights.ReadProperty | ActiveDirectoryRights.WriteProperty),
                        AccessControlType.Allow,
                        ActiveDirectorySecurityInheritance.None,
                        new Guid()
                    ));
                    ClonedTemplate.NTSecurityDescriptor!.AddAccessRule(new ActiveDirectoryAccessRule(
                            Principal.ObjectSid,
                        ActiveDirectoryRights.WriteProperty,
                        AccessControlType.Deny,
                        ActiveDirectorySecurityInheritance.None,
                        new Guid()
                    ));
                    break;
                }
            }
            Logger.Info("PKI-Certificate-Template object SDDL: ");
            Logger.Write(ClonedTemplate.NTSecurityDescriptor!.GetSecurityDescriptorSddlForm(AccessControlSections.Access));
            Logger.Write("");

            if (!ClonedTemplate.Add()) {
                Logger.Error("Failed to add new PKI-Certificate-Template object.");
                return;
            }
            Logger.Info($"PKI-Certificate-Template object cloned : {TemplateToClone.CommonName}");
            Logger.Info($"PKI-Certificate-Template object created: {Name}");
            Logger.Write("");

            // Update all certificate enrollement services with the new template.
            List<PKIEnrollmentService> EnrollmentServices = PKIEnrollmentService.FindAll();
            if (EnrollmentServices.Count == 0x00) {
                Logger.Error("No PKI-Enrollment-Service objects found.");
                CleanupCertificateTemplate(
                    ClonedTemplate,
                    EnrollmentServices
                );
                return;
            }

            foreach (PKIEnrollmentService EnrollmentService in EnrollmentServices) {
                EnrollmentService.CertificateTemplates.Add(ClonedTemplate.CommonName);
                if (!EnrollmentService.Update()) {
                    Logger.Error($"Failed to update enrollement service templates for: {EnrollmentService.DisplayName}");
                    CleanupCertificateTemplate(
                        ClonedTemplate,
                        EnrollmentServices
                    );
                    return;
                }
                Logger.Info($"PKI-Enrollment-Service object updated: {EnrollmentService.DisplayName}");
            }

            // Success
            Logger.Write("");
            Logger.Success("Operation was conducted successfully.");
            return;
        }

        /// <summary>
        /// If an error occurred, remove the PKI-Certificate-Template object and then remove its references within the 
        /// `certificateTemplates` attribute of each PKI-Enrollment-Service objects.
        /// </summary>
        private static void CleanupCertificateTemplate(PKICertificateTemplate Template, List<PKIEnrollmentService> EnrollmentServices) {

            // Remove the certificate template
            string Name = Template.CommonName;
            if (Template.Remove()) {
                Logger.Info($"Certificate template removed: {Name}");
            }
            else {
                Logger.Error($"Failed to remove: {Name}");
            }

            // Remove the references to the certificate template in the enrollement services objects.
            foreach (PKIEnrollmentService EnrollmentService in EnrollmentServices) {
                if (EnrollmentService.CertificateTemplates.Contains(Name)) {
                    EnrollmentService.CertificateTemplates.Remove(Name);
                    EnrollmentService.Update();
                }
            }
            Logger.Info("Enrollement services successfully updated");
            Logger.Write("");
        }

    }
}
