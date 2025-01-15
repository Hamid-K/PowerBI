using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000150 RID: 336
	[DataContract(Name = "ConceptualColumn", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualColumn
	{
		// Token: 0x06000885 RID: 2181 RVA: 0x00011D01 File Offset: 0x0000FF01
		internal ClientConceptualColumn()
		{
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00011D0C File Offset: 0x0000FF0C
		internal ClientConceptualColumn(ConceptualDefaultAggregate defaultAggregate, IList<string> keys, bool idOnEntityKey, bool calculated, string defaultValue, AggregateBehavior aggregateBehavior, IList<ClientConceptualVariationSource> variations, ClientConceptualGroupingMetadata groupingMetadata, ClientConceptualParameterMetadata parameterMetadata, IList<string> orderBy)
		{
			this._defaultAggregate = (int)defaultAggregate;
			this._keys = keys.NullIfEmpty<string>();
			this._idOnEntityKey = idOnEntityKey;
			this._calculated = calculated;
			this._defaultValue = defaultValue;
			this._variations = variations.NullIfEmpty<ClientConceptualVariationSource>();
			this._aggregateBehavior = (int)aggregateBehavior;
			this._groupingMetadata = groupingMetadata;
			this._parameterMetadata = parameterMetadata;
			this._orderBy = orderBy.NullIfEmpty<string>();
		}

		// Token: 0x04000417 RID: 1047
		[DataMember(Name = "DefaultAggregate", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		private int _defaultAggregate;

		// Token: 0x04000418 RID: 1048
		[DataMember(Name = "Keys", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private IList<string> _keys;

		// Token: 0x04000419 RID: 1049
		[DataMember(Name = "IdOnEntityKey", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private bool _idOnEntityKey;

		// Token: 0x0400041A RID: 1050
		[DataMember(Name = "Calculated", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private bool _calculated;

		// Token: 0x0400041B RID: 1051
		[DataMember(Name = "DefaultValue", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private string _defaultValue;

		// Token: 0x0400041C RID: 1052
		[DataMember(Name = "Variations", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		private IList<ClientConceptualVariationSource> _variations;

		// Token: 0x0400041D RID: 1053
		[DataMember(Name = "AggregateBehavior", IsRequired = false, EmitDefaultValue = false, Order = 6)]
		private int _aggregateBehavior;

		// Token: 0x0400041E RID: 1054
		[DataMember(Name = "GroupingMetadata", IsRequired = false, EmitDefaultValue = false, Order = 7)]
		private ClientConceptualGroupingMetadata _groupingMetadata;

		// Token: 0x0400041F RID: 1055
		[DataMember(Name = "ParameterMetadata", IsRequired = false, EmitDefaultValue = false, Order = 8)]
		private ClientConceptualParameterMetadata _parameterMetadata;

		// Token: 0x04000420 RID: 1056
		[DataMember(Name = "OrderBy", IsRequired = false, EmitDefaultValue = false, Order = 9)]
		private IList<string> _orderBy;
	}
}
