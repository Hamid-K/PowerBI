using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000175 RID: 373
	[DataContract(Name = "ConceptualNavigationTargetMapping", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ContractConceptualNavigationTargetMapping
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x0000C45C File Offset: 0x0000A65C
		internal ContractConceptualNavigationTargetMapping(string sourceProperty, string targetEntitySet, ConceptualMultiplicity sourceMultiplicity, ConceptualMultiplicity targetMultiplicity, bool isActive)
		{
			this._sourceProperty = sourceProperty;
			this._targetEntitySet = targetEntitySet;
			this._sourceMultiplicity = (int)sourceMultiplicity;
			this._targetMultiplicity = (int)targetMultiplicity;
			this._isActive = isActive;
		}

		// Token: 0x040006BB RID: 1723
		[DataMember(Name = "SourceProperty", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		private string _sourceProperty;

		// Token: 0x040006BC RID: 1724
		[DataMember(Name = "TargetEntitySet", IsRequired = true, EmitDefaultValue = false, Order = 1)]
		private string _targetEntitySet;

		// Token: 0x040006BD RID: 1725
		[DataMember(Name = "SourceMultiplicity", IsRequired = false, EmitDefaultValue = true, Order = 2)]
		private int _sourceMultiplicity;

		// Token: 0x040006BE RID: 1726
		[DataMember(Name = "TargetMultiplicity", IsRequired = false, EmitDefaultValue = true, Order = 3)]
		private int _targetMultiplicity;

		// Token: 0x040006BF RID: 1727
		[DataMember(Name = "IsActive", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private bool _isActive;
	}
}
