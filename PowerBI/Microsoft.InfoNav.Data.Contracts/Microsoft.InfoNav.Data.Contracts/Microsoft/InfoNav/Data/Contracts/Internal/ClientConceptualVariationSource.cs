using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000167 RID: 359
	[DataContract(Name = "ConceptualVariationSource", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualVariationSource
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x00013308 File Offset: 0x00011508
		internal ClientConceptualVariationSource()
		{
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00013310 File Offset: 0x00011510
		internal ClientConceptualVariationSource(string name, bool isDefault, string navigationPropertyName, string defaultPropertyName, string defaultHierarchyName)
		{
			this._name = name;
			this._isDefault = isDefault;
			this._navigationPropertyName = navigationPropertyName;
			this._defaultPropertyName = defaultPropertyName;
			this._defaultHierarchyName = defaultHierarchyName;
		}

		// Token: 0x0400052D RID: 1325
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private string _name;

		// Token: 0x0400052E RID: 1326
		[DataMember(Name = "Default", IsRequired = true, EmitDefaultValue = true, Order = 1)]
		private bool _isDefault;

		// Token: 0x0400052F RID: 1327
		[DataMember(Name = "NavigationProperty", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private string _navigationPropertyName;

		// Token: 0x04000530 RID: 1328
		[DataMember(Name = "DefaultProperty", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private string _defaultPropertyName;

		// Token: 0x04000531 RID: 1329
		[DataMember(Name = "DefaultHierarchy", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private string _defaultHierarchyName;
	}
}
