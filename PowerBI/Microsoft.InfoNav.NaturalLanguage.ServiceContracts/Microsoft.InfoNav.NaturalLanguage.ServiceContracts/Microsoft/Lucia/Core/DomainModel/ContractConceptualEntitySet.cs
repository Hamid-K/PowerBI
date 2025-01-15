using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000174 RID: 372
	[DataContract(Name = "ConceptualEntitySet", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ContractConceptualEntitySet
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x0000C437 File Offset: 0x0000A637
		internal ContractConceptualEntitySet(string name, string referenceName, IList<ContractConceptualProperty> properties, IList<ContractConceptualNavigationTargetMapping> relationships)
		{
			this._name = name;
			this._referenceName = referenceName;
			this._properties = properties;
			this._relationships = relationships;
		}

		// Token: 0x040006B7 RID: 1719
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private string _name;

		// Token: 0x040006B8 RID: 1720
		[DataMember(Name = "ReferenceName", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private string _referenceName;

		// Token: 0x040006B9 RID: 1721
		[DataMember(Name = "Properties", IsRequired = true, EmitDefaultValue = true, Order = 2)]
		private IList<ContractConceptualProperty> _properties;

		// Token: 0x040006BA RID: 1722
		[DataMember(Name = "Relationships", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private IList<ContractConceptualNavigationTargetMapping> _relationships;
	}
}
