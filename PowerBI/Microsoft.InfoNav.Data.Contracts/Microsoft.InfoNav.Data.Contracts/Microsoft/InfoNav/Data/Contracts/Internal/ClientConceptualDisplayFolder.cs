using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000151 RID: 337
	[DataContract(Name = "ConceptualDisplayFolder", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualDisplayFolder
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x00011D7B File Offset: 0x0000FF7B
		internal ClientConceptualDisplayFolder()
		{
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00011D83 File Offset: 0x0000FF83
		internal ClientConceptualDisplayFolder(string name, string displayName, string description, IList<ClientConceptualDisplayItem> displayItems)
		{
			this._name = name;
			if (name != displayName)
			{
				this._displayName = displayName;
			}
			this._description = description;
			this._displayItems = displayItems;
		}

		// Token: 0x04000421 RID: 1057
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private string _name;

		// Token: 0x04000422 RID: 1058
		[DataMember(Name = "DisplayName", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private string _displayName;

		// Token: 0x04000423 RID: 1059
		[DataMember(Name = "DisplayItems", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private IList<ClientConceptualDisplayItem> _displayItems;

		// Token: 0x04000424 RID: 1060
		[DataMember(Name = "Description", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private string _description;
	}
}
