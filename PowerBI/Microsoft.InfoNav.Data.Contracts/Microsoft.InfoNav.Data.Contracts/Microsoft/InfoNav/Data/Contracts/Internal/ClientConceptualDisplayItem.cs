using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000152 RID: 338
	[DataContract(Name = "ConceptualDisplayItem", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualDisplayItem
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x00011DB1 File Offset: 0x0000FFB1
		internal ClientConceptualDisplayItem()
		{
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00011DB9 File Offset: 0x0000FFB9
		internal ClientConceptualDisplayItem(IConceptualProperty property)
		{
			this._propertyRef = property.Name;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00011DCD File Offset: 0x0000FFCD
		internal ClientConceptualDisplayItem(IConceptualHierarchy hierarchy)
		{
			this._hierarchyRef = hierarchy.Name;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00011DE1 File Offset: 0x0000FFE1
		internal ClientConceptualDisplayItem(ClientConceptualDisplayFolder displayFolder)
		{
			this._displayFolder = displayFolder;
		}

		// Token: 0x04000425 RID: 1061
		[DataMember(Name = "PropertyRef", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		private string _propertyRef;

		// Token: 0x04000426 RID: 1062
		[DataMember(Name = "HierarchyRef", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private string _hierarchyRef;

		// Token: 0x04000427 RID: 1063
		[DataMember(Name = "DisplayFolder", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private ClientConceptualDisplayFolder _displayFolder;
	}
}
