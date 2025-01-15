using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000153 RID: 339
	[DataContract(Name = "ConceptualEntity", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualEntity
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x00011DF0 File Offset: 0x0000FFF0
		internal ClientConceptualEntity()
		{
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00011DF8 File Offset: 0x0000FFF8
		internal ClientConceptualEntity(string name, string edmName, string displayName, string description, bool hidden, bool showAsVariationsOnly, bool isPrivate, bool isDateTable, bool calculated, ClientConceptualQueryableState queryable, string errorMessage, IList<ClientConceptualProperty> properties, IList<ClientConceptualHierarchy> hierarchies, IList<ClientConceptualDisplayFolder> displayFolders, IList<ClientConceptualNavigationProperty> navigationProperties, ClientConceptualEntityCapabilities capabilities, string defaultLabelColumnRef = null, IList<string> defaultFieldPropertyRefs = null, ClientConceptualEntitySource source = null, string stableName = null)
		{
			this.Name = name;
			this.EdmName = edmName;
			if (name != displayName)
			{
				this.DisplayName = displayName;
			}
			this.Description = description;
			this.Hidden = hidden;
			this.ShowAsVariationsOnly = showAsVariationsOnly;
			this.Private = isPrivate;
			this.Calculated = calculated;
			this.IsDateTable = isDateTable;
			this.Queryable = queryable;
			this.Properties = properties;
			this.ErrorMessage = errorMessage;
			this.DefaultLabelColumnRef = defaultLabelColumnRef;
			this.Capabilities = capabilities;
			this.Source = source;
			this.StableName = stableName;
			if (hierarchies != null && !hierarchies.IsNullOrEmptyCollection<ClientConceptualHierarchy>())
			{
				this.Hierarchies = hierarchies;
			}
			if (displayFolders != null && !displayFolders.IsNullOrEmptyCollection<ClientConceptualDisplayFolder>())
			{
				this.DisplayFolders = displayFolders;
			}
			if (navigationProperties != null && !navigationProperties.IsEmptyCollection<ClientConceptualNavigationProperty>())
			{
				this.NavigationProperties = navigationProperties;
			}
			if (defaultFieldPropertyRefs != null && !defaultFieldPropertyRefs.IsNullOrEmptyCollection<string>())
			{
				this.DefaultFieldPropertyRefs = defaultFieldPropertyRefs;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00011EE5 File Offset: 0x000100E5
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x00011EED File Offset: 0x000100ED
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		internal string Name { get; private set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00011EF6 File Offset: 0x000100F6
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x00011EFE File Offset: 0x000100FE
		[DataMember(Name = "EdmName", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		internal string EdmName { get; private set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00011F07 File Offset: 0x00010107
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00011F0F File Offset: 0x0001010F
		[DataMember(Name = "DisplayName", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		internal string DisplayName { get; private set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00011F18 File Offset: 0x00010118
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00011F20 File Offset: 0x00010120
		[DataMember(Name = "Hidden", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		internal bool Hidden { get; private set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00011F29 File Offset: 0x00010129
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00011F31 File Offset: 0x00010131
		[DataMember(Name = "ShowAsVariationsOnly", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		internal bool ShowAsVariationsOnly { get; private set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00011F3A File Offset: 0x0001013A
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x00011F42 File Offset: 0x00010142
		[DataMember(Name = "Private", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		internal bool Private { get; private set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00011F4B File Offset: 0x0001014B
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x00011F53 File Offset: 0x00010153
		[DataMember(Name = "Calculated", IsRequired = false, EmitDefaultValue = false, Order = 6)]
		internal bool Calculated { get; private set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00011F5C File Offset: 0x0001015C
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x00011F64 File Offset: 0x00010164
		[DataMember(Name = "Queryable", IsRequired = false, EmitDefaultValue = false, Order = 7)]
		internal ClientConceptualQueryableState Queryable { get; private set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00011F6D File Offset: 0x0001016D
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00011F75 File Offset: 0x00010175
		[DataMember(Name = "ErrorMessage", IsRequired = false, EmitDefaultValue = false, Order = 8)]
		internal string ErrorMessage { get; private set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00011F7E File Offset: 0x0001017E
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x00011F86 File Offset: 0x00010186
		[DataMember(Name = "Properties", IsRequired = false, EmitDefaultValue = false, Order = 9)]
		internal IList<ClientConceptualProperty> Properties { get; private set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00011F8F File Offset: 0x0001018F
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x00011F97 File Offset: 0x00010197
		[DataMember(Name = "Hierarchies", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		internal IList<ClientConceptualHierarchy> Hierarchies { get; private set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00011FA0 File Offset: 0x000101A0
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x00011FA8 File Offset: 0x000101A8
		[DataMember(Name = "DisplayFolders", IsRequired = false, EmitDefaultValue = false, Order = 11)]
		internal IList<ClientConceptualDisplayFolder> DisplayFolders { get; private set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00011FB1 File Offset: 0x000101B1
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x00011FB9 File Offset: 0x000101B9
		[DataMember(Name = "NavigationProperties", IsRequired = false, EmitDefaultValue = false, Order = 12)]
		internal IList<ClientConceptualNavigationProperty> NavigationProperties { get; private set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00011FC2 File Offset: 0x000101C2
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x00011FCA File Offset: 0x000101CA
		[DataMember(Name = "Description", IsRequired = false, EmitDefaultValue = false, Order = 17)]
		internal string Description { get; private set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00011FD3 File Offset: 0x000101D3
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00011FDB File Offset: 0x000101DB
		[DataMember(Name = "IsDateTable", IsRequired = false, EmitDefaultValue = false, Order = 18)]
		internal bool IsDateTable { get; private set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00011FE4 File Offset: 0x000101E4
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00011FEC File Offset: 0x000101EC
		[DataMember(Name = "Capabilities", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		internal ClientConceptualEntityCapabilities Capabilities { get; private set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00011FF5 File Offset: 0x000101F5
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00011FFD File Offset: 0x000101FD
		[DataMember(Name = "DefaultLabelColumnRef", IsRequired = false, EmitDefaultValue = false, Order = 21)]
		internal string DefaultLabelColumnRef { get; private set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00012006 File Offset: 0x00010206
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0001200E File Offset: 0x0001020E
		[DataMember(Name = "DefaultFieldPropertyRefs", IsRequired = false, EmitDefaultValue = false, Order = 22)]
		internal IList<string> DefaultFieldPropertyRefs { get; private set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00012017 File Offset: 0x00010217
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0001201F File Offset: 0x0001021F
		[DataMember(Name = "Source", IsRequired = false, EmitDefaultValue = false, Order = 26)]
		internal ClientConceptualEntitySource Source { get; private set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00012028 File Offset: 0x00010228
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00012030 File Offset: 0x00010230
		[DataMember(Name = "StableName", IsRequired = false, EmitDefaultValue = false, Order = 27)]
		internal string StableName { get; private set; }
	}
}
