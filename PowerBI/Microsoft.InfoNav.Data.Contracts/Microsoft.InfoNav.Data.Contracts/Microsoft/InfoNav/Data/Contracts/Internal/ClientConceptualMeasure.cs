using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200015E RID: 350
	[DataContract(Name = "ConceptualMeasure", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualMeasure
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x00012304 File Offset: 0x00010504
		internal ClientConceptualMeasure()
		{
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001230C File Offset: 0x0001050C
		internal ClientConceptualMeasure(ClientConceptualKpi kpi, ClientConceptualMeasureTemplate template, ConceptualDistributiveAggregateKind? distributiveAggregate, IList<string> distributiveBy, string formatBy, bool canEdit, ClientConceptualChangeDetectionMetadata changeDetectionMetadata)
		{
			this._kpi = kpi;
			this._template = template;
			this._distributiveAggregate = distributiveAggregate;
			this._distributiveBy = distributiveBy.NullIfEmpty<string>();
			this._formatBy = formatBy;
			this._changeDetectionMetadata = changeDetectionMetadata;
			this._canEdit = ClientConceptualSchemaFactory.ConvertTrueToNull(canEdit);
		}

		// Token: 0x04000466 RID: 1126
		[DataMember(Name = "Kpi", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		private ClientConceptualKpi _kpi;

		// Token: 0x04000467 RID: 1127
		[DataMember(Name = "Template", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private ClientConceptualMeasureTemplate _template;

		// Token: 0x04000468 RID: 1128
		[DataMember(Name = "DistributiveAggregate", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private ConceptualDistributiveAggregateKind? _distributiveAggregate;

		// Token: 0x04000469 RID: 1129
		[DataMember(Name = "DistributiveBy", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private IList<string> _distributiveBy;

		// Token: 0x0400046A RID: 1130
		[DataMember(Name = "FormatBy", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private string _formatBy;

		// Token: 0x0400046B RID: 1131
		[DataMember(Name = "canEdit", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		private bool? _canEdit;

		// Token: 0x0400046C RID: 1132
		[DataMember(Name = "ChangeDetectionMetadata", IsRequired = false, EmitDefaultValue = false, Order = 6)]
		private ClientConceptualChangeDetectionMetadata _changeDetectionMetadata;
	}
}
