using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000160 RID: 352
	[DataContract(Name = "ConceptualNavigationProperty", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualNavigationProperty
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00012375 File Offset: 0x00010575
		internal ClientConceptualNavigationProperty()
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00012380 File Offset: 0x00010580
		internal ClientConceptualNavigationProperty(string name, bool isActive, string sourceColumnName, string targetEntityName, ConceptualMultiplicity sourceMultiplicity, ConceptualMultiplicity targetMultiplicity, ConceptualNavigationBehavior behavior, CrossFilterDirection crossFilterDirection)
		{
			this._name = name;
			this._isActive = isActive;
			this._sourceColumnName = sourceColumnName;
			this._targetEntityName = targetEntityName;
			this._sourceMultiplicity = sourceMultiplicity;
			this._targetMultiplicity = targetMultiplicity;
			this._behavior = behavior;
			this._crossFilterDirection = crossFilterDirection;
		}

		// Token: 0x0400046E RID: 1134
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private string _name;

		// Token: 0x0400046F RID: 1135
		[DataMember(Name = "Active", IsRequired = true, EmitDefaultValue = true, Order = 1)]
		private bool _isActive;

		// Token: 0x04000470 RID: 1136
		[DataMember(Name = "SourceColumn", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private string _sourceColumnName;

		// Token: 0x04000471 RID: 1137
		[DataMember(Name = "TargetEntity", IsRequired = true, EmitDefaultValue = true, Order = 3)]
		private string _targetEntityName;

		// Token: 0x04000472 RID: 1138
		[DataMember(Name = "SourceMultiplicity", IsRequired = true, EmitDefaultValue = true, Order = 4)]
		private ConceptualMultiplicity _sourceMultiplicity;

		// Token: 0x04000473 RID: 1139
		[DataMember(Name = "TargetMultiplicity", IsRequired = true, EmitDefaultValue = true, Order = 5)]
		private ConceptualMultiplicity _targetMultiplicity;

		// Token: 0x04000474 RID: 1140
		[DataMember(Name = "Behavior", IsRequired = false, EmitDefaultValue = false, Order = 6)]
		private ConceptualNavigationBehavior _behavior;

		// Token: 0x04000475 RID: 1141
		[DataMember(Name = "CrossFilterDirection", IsRequired = false, EmitDefaultValue = false, Order = 7)]
		private CrossFilterDirection _crossFilterDirection;
	}
}
