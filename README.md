# Huginn
The Huginn project introduces novel adversary deception techniques within Active Directory environments. Huginn provides an open-source program to help realise strategic concepts from the [MITRE Engage framework](https://engage.mitre.org/) and the European Central Bank's cyber resilience report.

### Objectives
- Reduce the security posture requirements for engaging in cyber deception.
- Balance the intrinsic asymmetry of cyber-attacks by raising high-fidelity alerts around advanced attacker activity.
- Impose cost by embedding high-value deception artefacts within critical attack paths.

List of resources:
- [MITRE Engage: A Framework and Community for Cyber Deception](https://www.mitre.org/news-insights/impact-story/mitre-engage-framework-and-community-cyber-deception)
- [European Central Bank: Cyber resilience oversight expectations for financial market infrastructures](https://www.ecb.europa.eu/paym/pdf/cons/cyberresilience/Cyber_resilience_oversight_expectations_for_financial_market_infrastructures.pdf)


### Operations 

#### Pki-Certificate-Template

Adversaries (such as [APT29]()) have often leveraged misconfigured [Microsoft Active Directory Certificate Services (AD CS)](https://attack.mitre.org/techniques/T1649/) to gain high-privilege rights without flagging detection controls.

```
PS C:\> .\Huginn.exe pki-template -h
Description:
  Generate a PKI-Certificate-Template

Usage:
  Huginn pki-template [options]

Options:
  --name <name>                       Name of the new certificate template to create.
  --template <template> (REQUIRED)    Name of the template to clone.
  --principal <principal> (REQUIRED)  The user principal to allow/deny.
  --technique <ESC1|ESC4>             The technique to apply.
  -?, -h, --help                      Show help and usage information


PS C:\>
```

This sub-command of `Huginn` allows creation of deception artefacts as follows:

#### ESC4
1. Clone an existing certificate template to create a new one;
1. Add an Access Control Entry (ACE) to allow the given principal (e.g. Domain Users) have `GenericWrite` and `Enroll` permissions;
1. Add an Access Control Entry (ACE) to deny the given principal (e.g. Domain Users) multiple permissions (e.g. Enroll); and
1. Add the new certificate template to the list of templates of the PKI-Enrollement-Services objects.
   
##### Result:
- Conventional tooling like [Certify] and [CertiPy] will identify the new certificate template as vulnerable because of the "allow" ACE while ignoring the "deny" ACE.
- This would effectively allow the attacker to modify the template to add the EnrolleeSuppliesSubject flag.
- When the attacker proceeded to request an arbitrary Subject Alternative Name (SAN) however, the request will be denied by the certificate enrollment service.
- The attacker will not be able to remove the deny ACE within the ntSecurityDescriptor despite the GenericAll privilege due to removal of Modify Owner and Modify Permissions.

#### ESC1
1. Clone an existing certificate template to create a new one;
1. Add an Access Control Entry (ACE) to allow the given principal (e.g. Domain Users) have `Enroll` permissions;
1. Add an Access Control Entry (ACE) to deny the given principal (e.g. Domain Users) from the `Enroll` permission; and
1. Add the new certificate template to the list of templates of the PKI-Enrollement-Services objects.

##### Result:
- Conventional tooling like [Certify](https://github.com/GhostPack/Certify) and [CertiPy](https://github.com/ly4k/Certipy) will identify the new certificate template as vulnerable because of the "allow" ACE while ignoring the "deny" ACE.
- This would effectively allow the attacker to request the template, but the certificate enrollment service will deny the request when received.

Detection:
The asset offers detection opportunity using Windows Event IDs 4899 and 4898, with `<TEMPLATE NAME>` being substituted with the deception template's name.
```
rule rule_Huginn_AD_CS_ESC4 {
    meta:
        author = "Rohan Durve / Paul Laine"
        severity = "Critical"
        priority = "High"
        mitre_attack_tactic = "Credential Access"
        mitre_attack_technique = "Steal or Forge Authentication Certificates"
        mitre_attack_technique_id = "T1649"
        summary = "[Huginn] A deception asset PKI Certificate Template was triggered."

    events:
        $e.metadata.product_name = "Microsoft-Windows-Security-Auditing" AND
        $e.target.resource.name = "<TEMPLATE NAME>" AND
        (
            $e.metadata.product_event_type = "4899" OR
            $e.metadata.product_event_type = "4898"
        )

    outcome:
        $risk_score = max(99)

    condition:
        $e
}
```

### Kerberos Delegation - WIP

Kerberos Delegation Primitives as documented by [Shenanigans Labs](https://shenaniganslabs.io/2019/01/28/Wagging-the-Dog.html) are another opportunity for deception.

#### Computer Object Takeover via Resource-based Contained Delegation - Generic DACL Abuse
1. Identify computer objects that have been disabled or deprecated.
1. Create a DNS entry in Active Directory to forward requests from our deception asset `deceptionasset.alpha.lab` to an arbitrary live system `fileserver.alpha.lab` with an accessible SMB server
1. Add an Access Control Entry (ACE) to allow the given principal (e.g. Domain Users) to have `GenericWrite` permissions.
1. Add a SASL to raise alerts for modifications of the object.
   
##### Result:
- Conventional tooling like [BloodHound] will identify the object `deceptionasset$` as vulnerable because of the "allow" ACE.
- Attacker can modify the `msDS-AllowedToActOnBehalfOfOtherIdentity` attribute on the asset to add an attcker-controlled Service Principal Name (SPN)
- Modification of the property will set of an alert.
- Port scans will show that a SMB server is accessible at `deceptionasset.alpha.lab`
- Attackers can then forge a TGS on behalf of an administrator for `cifs/deceptionasset.alpha.lab`
- As the computer does not exist, it however cannot be used to laterally move or gain privileges.

```
PS C:\> .\Huginn.exe krb-delegation -h
Description:
  Generate a Machine for resource-based Kerberos delegation.

Usage:
  Huginn krb-delegation [options]

Options:
  --computername <machine.fqdn> (REQUIRED)  Name of the machine to forward DNS to.
  --computerobject <machine$> (REQUIRED)    Name of the machine to repurpose.
  --principal <principal> (REQUIRED)        The user principal to allow delegation.
  --technique <ResourceBased>               The technique to apply.
  -?, -h, --help                            Show help and usage information


PS C:\>
```

This is a work in progress.

### User Credentials - WIP

Attackers have commonly leveraged user credentials in objects with misconfigured access control. 

#### User Object w/ Incorrect Password in Description Field
1. Identify disabled or deprecated user object to re-purpose.
1. Reset user object password to a secure value
1. Add a randomly generated incorrect password into the `Description` attribute as `Pwd:STRING` or `Pass:STRING`

#### User Object w/ Incorrect Password in Domain SYSVOL Script
1. Identify disabled or deprecated user object to re-purpose.
1. Reset user object password to a secure value
1. Add a randomly generated incorrect password into a PowerShell/Batch/VBScript script on the domain SYSVOL share (e.g. `domain.fqdn\SYSVOL\domain\scripts\NessusDeployment.bat`)

#### User Object w/ ForcePasswordChange
1. Identify disabled or deprecated user object to re-purpose.
1. Reset user object password to a secure value
1. Add an `Allow` `ForcePasswordChange` ACE for a principal (e.g. Domain Users) on the user object
1. Add a `Deny` `ForcePasswordChange` ACE for a principal (e.g. Domain Users) on the user object

##### Result:
!!!

#### Group Managed Service Account w/ Retrievable Password
1. Identify disabled or deprecated MSA object to re-purpose or create new one if non-existent.
1. Reset user object password to a secure value
1. Add an `Allow` ACE for a principal (e.g. Domain Users) on the user object's `PrincipalsAllowedToRetrieveManagedPassword` attribute.
1. Add a `Deny` ACE for the principal on the user object's `PrincipalsAllowedToRetrieveManagedPassword` attribute.
 
##### Result:

```
PS C:\> .\Huginn.exe user -h
Description:
  Generate a user deception artefact.

Usage:
  Huginn usr-credentials [options]

Options:
  --user <user> (REQUIRED)                       Name of the target to re-purpose.
  --principal <principal>                        The user principal to allow access from.
  --technique <Description|FPC|GMSA|GPP|Script>  The technique to apply.
  -?, -h, --help                                 Show help and usage information


PS C:\>
```
This is a work in progress.

