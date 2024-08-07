
using Huginn.ADObjects;
using System.Runtime.InteropServices;
using System.Text.Json;


using System.CommandLine;
using Huginn.Operations;

namespace Huginn {

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class Huginn {

        /// <summary>
        /// Command line entry point.
        /// </summary>
        public static async Task<int> Main(string[] args) {

            // Check that we are currently running on a windows platform.
            if (!(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))) {
                Logger.Error("Non windows platforms are not supported.");
                return -1;
            }

            // Define the root command for the CLI
            var Root = new RootCommand("Huginn - Cyber Deception Enablement CLI");
            Root.Add(new Option<bool?>(
                name       : "-v",
                description: "Enable verbose output."
            ));

            // Add the sub command for PKI-Certificate-Template
            OpPkiTemplate.AddCertificateTemplateSubCommand(Root);

            // Add the sub command for Kerberos delegation
            OpKerberosDelegation.AddKerberosDelegationSubCommand(Root);

            // Add the sub command for User crendentials
            OpUserCredentials.AddUserCredentialsSubCommand(Root);

            // Execute the required code and exit.
            return await Root.InvokeAsync(args);
        }
    }
}
