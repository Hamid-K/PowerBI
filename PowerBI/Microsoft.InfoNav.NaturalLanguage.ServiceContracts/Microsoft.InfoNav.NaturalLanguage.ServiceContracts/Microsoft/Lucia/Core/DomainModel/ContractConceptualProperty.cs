using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000177 RID: 375
	[DataContract(Name = "ConceptualProperty", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ContractConceptualProperty
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0000C48C File Offset: 0x0000A68C
		internal ContractConceptualProperty(string name, string referenceName, DataType type, ContractConceptualPropertyBehavior metadata, ConceptualDefaultAggregate defaultAggregate, string formatString, ConceptualDataCategory category)
		{
			this._name = name;
			if (name != referenceName)
			{
				this._referenceName = referenceName;
			}
			this._type = (int)type;
			this._behavior = (int)metadata;
			this._defaultAggregate = (int)defaultAggregate;
			this._formatString = formatString;
			if (category != ConceptualDataCategory.None)
			{
				this._dataCategory = category.ToString();
			}
		}

		// Token: 0x040006C6 RID: 1734
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private string _name;

		// Token: 0x040006C7 RID: 1735
		[DataMember(Name = "ReferenceName", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private string _referenceName;

		// Token: 0x040006C8 RID: 1736
		[DataMember(Name = "Type", IsRequired = true, EmitDefaultValue = true, Order = 2)]
		private int _type;

		// Token: 0x040006C9 RID: 1737
		[DataMember(Name = "Behavior", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private int _behavior;

		// Token: 0x040006CA RID: 1738
		[DataMember(Name = "DefaultAggregate", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private int _defaultAggregate;

		// Token: 0x040006CB RID: 1739
		[DataMember(Name = "FormatString", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		private string _formatString;

		// Token: 0x040006CC RID: 1740
		[DataMember(Name = "DataCategory", IsRequired = false, EmitDefaultValue = false, Order = 6)]
		private string _dataCategory;
	}
}
