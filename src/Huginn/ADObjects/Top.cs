using System.DirectoryServices;

namespace Huginn.ADObjects {

    /// <summary>
    /// The top level class of any Active Directory object from which all classes are derived.
    /// 
    /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/c-top
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class Top {

        #region Attributes

        /// <summary>
        /// The description displayed on admin screens.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-admindescription
        /// </summary>
        public string? AdminDescription { get; set; }

        /// <summary>
        /// The name to be displayed on admin screens.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-admindisplayname
        /// </summary>
        public string? AdminDisplayName { get; set; }

        // Allowed-Attributes
        // Allowed-Attributes-Effective
        // Allowed-Child-Classes
        // Allowed-Child-Classes-Effective
        // Bridgehead-Server-List-BL
        // Canonical-Name

        /// <summary>
        /// The name that represents an object. Used to perform searches.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-cn
        /// </summary>
        public string? CommonName { get; set; }

        /// <summary>
        /// The date when this object was created. This value is replicated.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-createtimestamp
        /// </summary>
        public DateTime? CreateTimeStamp { get; set; }

        /// <summary>
        /// Contains the description to display for an object.
        /// This value is restricted as single-valued for backward compatibility in some cases but is allowed to be multi-valued in others.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-description
        /// </summary>
        public List<string>? Description { get; set; }

        /// <summary>
        /// The display name for an object. This is usually the combination of the users first name, middle initial, and last name.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-displayname#windows-server-2012
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// The printable display name for an object.
        /// The printable display name is usually the combination of the user's first name, middle initial, and last name.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-displaynameprintable
        /// </summary>
        public string? DisplayNamePrintable { get; set; }

        // DSA-Signature
        // DS-Core-Propagation-Data

        /// <summary>
        /// The name of a property page used to extend the UI of a directory object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-extensionname
        /// </summary>
        public List<string>? ExtensionName { get; set; }

        /// <summary>
        /// To be used by the object to store bit information.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-flags
        /// </summary>
        public Int32? Flags { get; set; }

        // fromEntry
        // Frs-Computer-Reference-BL
        // FRS-Member-Reference-BL
        // FSMO-Role-Owner

        /// <summary>
        /// A bitfield that dictates how the object is instantiated on a particular server.
        /// The value of this attribute can differ on different replicas even if the replicas are in sync.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-instancetype
        /// </summary>
        public Int32? InstanceType { get; set; }

        /// <summary>
        /// If TRUE, the object hosting this attribute must be replicated during installation of a new replica.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-iscriticalsystemobject
        /// </summary>
        public bool? IsCriticalSystemObject { get; set; }

        /// <summary>
        /// If TRUE, this object has been marked for deletion and cannot be instantiated.
        /// After the tombstone period has expired, it will be removed from the system.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-isdeleted
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// The distinguished name of the groups to which this object belongs.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-memberof
        /// </summary>
        public List<string>? IsMemberOfDL { get; set; }

        // Is-Privilege-Holder

        /// <summary>
        /// Is the object recycled. For use with AD Recycle Bin.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-isrecycled
        /// </summary>
        public bool? IsRecycled { get; set; }

        /// <summary>
        /// The Distinguished Name (DN) of the last known parent of an orphaned object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-lastknownparent
        /// </summary>
        public string? LastKnownParent { get; set; }

        /// <summary>
        /// Contains the list of objects that are managed by the user.
        /// The objects listed are those that have the property managedBy property set to this user. Each item in the list is a linked reference to the managed object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-managedobjects
        /// </summary>
        public List<string>? ManagedObjects { get; set; }

        /// <summary>
        /// Backward link for Has-Master-NCs attribute. The distinguished name for its NTDS Settings objects.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-masteredby
        /// </summary>
        public List<string>? MasteredBy { get; set; }

        /// <summary>
        /// A computed attribute that represents the date when this object was last changed. This value is not replicated.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-modifytimestamp
        /// </summary>
        public DateTime? ModifyTimeStamp { get; set; }

        // ms-COM-PartitionSetLink
        // ms-COM-UserLink
        // ms-DFSR-ComputerReferenceBL
        // ms-DFSR-MemberReferenceBL
        // ms-DS-Approx-Immed-Subordinates
        // ms-DS-AuthenticatedTo-Accountlist
        // ms-DS-Claim-Shares-Possible-Values-With-BL
        // MS-DS-Consistency-Child-Count
        // MS-DS-Consistency-Guid
        // ms-DS-Enabled-Feature-BL
        // ms-DS-Host-Service-Account-BL
        // ms-DS-Is-Domain-For
        // ms-DS-Is-Full-Replica-For
        // ms-DS-Is-Partial-Replica-For
        // ms-DS-Is-Primary-Computer-For
        // ms-DS-KrbTgt-Link-BL
        // ms-DS-Last-Known-RDN
        // ms-DS-local-Effective-Deletion-Time
        // ms-DS-local-Effective-Recycle-Time
        // ms-DS-Mastered-By
        // ms-DS-Members-For-Az-Role-BL
        // ms-DS-Members-Of-Resource-Property-List-BL
        // ms-DS-NC-Repl-Cursors
        // ms-DS-NC-Repl-Inbound-Neighbors
        // ms-DS-NC-Repl-Outbound-Neighbors
        // ms-DS-NC-RO-Replica-Locations-BL
        // ms-DS-NC-Type
        // ms-DS-Non-Members-BL
        // ms-DS-Object-Reference-BL
        // ms-DS-OIDToGroup-Link-BL
        // ms-DS-Operations-For-Az-Role-BL
        // ms-DS-Operations-For-Az-Task-BL
        // ms-DS-Principal-Name
        // ms-DS-PSO-Applied
        // ms-DS-Repl-Attribute-Meta-Data
        // ms-DS-Repl-Value-Meta-Data
        // ms-DS-Revealed-DSAs
        // ms-DS-Revealed-List-BL
        // ms-DS-Tasks-For-Az-Role-BL
        // ms-DS-Tasks-For-Az-Task-BL
        // ms-DS-TDO-Egress-BL
        // ms-DS-TDO-Ingress-BL
        // ms-DS-Value-Type-Reference-BL
        // ms-Exch-Owner-BL
        // msSFU-30-Posix-Member-Of
        // netboot-SCP-BL
        // Non-Security-Member-BL

        /// <summary>
        /// The Windows NT security descriptor for the schema object. 
        /// A security descriptor is a data structure that contains security information about an object, such as the ownership and permissions of the object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-ntsecuritydescriptor
        /// </summary>
        public ActiveDirectorySecurity? NTSecurityDescriptor { get; set; }

        /// <summary>
        /// Same as the Distinguished Name for an object. Used by Exchange.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-distinguishedname
        /// </summary>
        public string? ObjectDistName { get; set; }

        /// <summary>
        /// An object class name used to group objects of this or derived classes.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-objectcategory
        /// </summary>
        public string? ObjectCategory { get; set; }

        /// <summary>
        /// The list of classes from which this class is derived.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-objectclass
        /// </summary>
        public List<string>? ObjectClass { get; set; }

        /// <summary>
        /// The unique identifier for an object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-objectguid
        /// </summary>
        public Guid? ObjectGuid { get; set; }

        /// <summary>
        /// This can be used to store a version number for the object.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-objectversion
        /// </summary>
        public Int32? ObjectVersion { get; set; }

        // Other-Well-Known-Objects
        // Partial-Attribute-Deletion-List
        // Partial-Attribute-Set
        // Possible-Inferiors
        // Proxied-Object-Name
        // Proxy-Addresses
        // Query-Policy-BL
        // RDN
        // Repl-Property-Meta-Data
        // Repl-UpToDate-Vector
        // Reports
        // Reps-From
        // Reps-To

        /// <summary>
        /// The revision level for a security descriptor or other change. Only used in the sam-server and ds-ui-settings objects.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-revision
        /// </summary>
        public Int32? Revision { get; set; }

        // SD-Rights-Effective
        // Server-Reference-BL

        /// <summary>
        /// TRUE if this attribute is to be visible in the Advanced mode of the UI.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-showinadvancedviewonly
        /// </summary>
        public bool? ShowInAdvancedViewOnly { get; set; }

        // Site-Object-BL
        // Structural-Object-Class
        // Sub-Refs
        // SubSchemaSubEntry
        // System-Flags

        /// <summary>
        /// The update sequence number (USN) assigned by the local directory for the latest change, including creation. See also , USN-Created.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-usnchanged
        /// </summary>
        public Int64? USNChanged { get; set; }

        /// <summary>
        /// The update sequence number (USN) assigned at object creation. See also, USN-Changed.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-usncreated
        /// </summary>
        public Int64? USNCreated { get; set; }

        /// <summary>
        /// Contains the update sequence number (USN) for the last system object that was removed from a server.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-usndsalastobjremoved
        /// </summary>
        public Int64? USNDSALastObjRemoved { get; set; }

        /// <summary>
        /// The update sequence number (USN) for inter-site replication.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-usnintersite
        /// </summary>
        public Int32? USNIntersite { get; set; }

        /// <summary>
        /// Contains the update sequence number (USN) for the last non-system object that was removed from a server.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-usnlastobjrem
        /// </summary>
        public Int64? USNLastObjRem { get; set; }

        /// <summary>
        /// Value of the USN-Changed attribute of the object from the remote directory that replicated the change to the local server.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-usnsource
        /// </summary>
        public Int64? USNSource { get; set; }

        /// <summary>
        /// References to objects in other ADSI namespaces.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-wbempath
        /// </summary>
        public List<string>? WbemPath { get; set; }

        // Well-Known-Objects

        /// <summary>
        /// The date when this object was last changed. This value is not replicated and exists in the global catalog.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-whenchanged
        /// </summary>
        public DateTime? WhenChanged { get; set; }

        /// <summary>
        /// The date when this object was created. This value is replicated and is in the global catalog.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-whencreated
        /// </summary>
        public DateTime? WhenCreated { get; set; }

        /// <summary>
        /// A web page that is the primary landing page of a website.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-wwwhomepage
        /// </summary>
        public string? WWWHomePage { get; set; }

        /// <summary>
        /// A list of alternate webpages.
        /// 
        /// Link: https://learn.microsoft.com/en-us/windows/win32/adschema/a-url
        /// </summary>
        public List<string>? WWWPageOther { get; set; }

        #endregion

        /// <summary>
        /// List if attributes to extract when querying Top object.
        /// </summary>
        protected static string[] Attributes {  get {
                return new string[] {
                    "cn",
                    "description",
                    "displayName",
                    "distinguishedName",
                    "flags",
                    "instanceType",
                    "isCriticalSystemObject",
                    "memberOf",
                    "nTSecurityDescriptor",
                    "objectCategory",
                    "objectClass",
                    "objectGUID",
                    "objectVersion",
                    "revision",
                    "showInAdvancedViewOnly",
                    "uSNChanged",
                    "uSNCreated",
                    "whenChanged",
                    "whenCreated"
                };
            }
        }

        /// <summary>
        /// Set an attribute to a property list within a DirectoryEntry.
        /// </summary>
        public static void SetAttribute(string Attribute, DirectoryEntry NewObject, object? Value) {
            if (Value != null) {
                if (Value is IEnumerable<object> v) {
                    NewObject.Properties[Attribute].Value = v.Reverse().ToArray();
                    return;
                }
                NewObject.Properties[Attribute].Value = Value;
            }
        }

        /// <summary>
        /// Extract all the attributes from an LDAP search query.
        /// </summary>
        public Top FromResult(SearchResult Result) {
            this.InstanceType           = Util.GetTypeFromResult<int>(Result         , "instanceType");
            this.NTSecurityDescriptor   = Util.GetSecDescriptorFromResult(Result     , "nTSecurityDescriptor");
            this.ObjectCategory         = Util.GetTypeFromResult<string>(Result      , "objectCategory");
            this.ObjectClass            = Util.GetListOfTypeFromResult<string>(Result, "objectClass");
            this.CommonName             = Util.GetTypeFromResult<string>(Result      , "cn");
            this.Description            = Util.GetListOfTypeFromResult<string>(Result, "description");
            this.DisplayName            = Util.GetTypeFromResult<string>(Result      , "displayName");
            this.ObjectDistName         = Util.GetTypeFromResult<string>(Result      , "distinguishedName");
            this.Flags                  = Util.GetTypeFromResult<int>(Result         , "flags");
            this.ObjectGuid             = Util.GetGuidFromResult(Result              , "objectGUID");
            this.ObjectVersion          = Util.GetTypeFromResult<int>(Result         , "objectVersion");
            this.Revision               = Util.GetTypeFromResult<int>(Result         , "revision");
            this.ShowInAdvancedViewOnly = Util.GetTypeFromResult<bool>(Result        , "showInAdvancedViewOnly");
            this.USNChanged             = Util.GetTypeFromResult<long>(Result        , "uSNChanged");
            this.USNCreated             = Util.GetTypeFromResult<long>(Result        , "uSNCreated");
            this.WhenChanged            = Util.GetDateTimeFromResult(Result          , "whenChanged");
            this.WhenCreated            = Util.GetDateTimeFromResult(Result          , "whenCreated");
            this.IsMemberOfDL           = Util.GetListOfTypeFromResult<string>(Result, "memberOf");
            this.IsCriticalSystemObject = Util.GetTypeFromResult<bool>(Result        , "isCriticalSystemObject");

            // Success
            return this;
        }
    }
}
