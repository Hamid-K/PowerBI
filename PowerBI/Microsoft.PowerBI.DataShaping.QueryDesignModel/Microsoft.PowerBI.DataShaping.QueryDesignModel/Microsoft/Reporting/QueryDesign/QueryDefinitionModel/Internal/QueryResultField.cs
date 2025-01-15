using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000115 RID: 277
	public sealed class QueryResultField
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0002C21F File Offset: 0x0002A41F
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x0002C227 File Offset: 0x0002A427
		internal ConceptualTypeColumn Field { get; private set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0002C230 File Offset: 0x0002A430
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x0002C238 File Offset: 0x0002A438
		public string DataFieldName { get; private set; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0002C241 File Offset: 0x0002A441
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x0002C249 File Offset: 0x0002A449
		public string RawUnqualifiedFieldName { get; private set; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0002C252 File Offset: 0x0002A452
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x0002C25A File Offset: 0x0002A45A
		internal ConceptualTypeColumn AggregateIndicatorField { get; private set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x0002C263 File Offset: 0x0002A463
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x0002C26B File Offset: 0x0002A46B
		public bool IsNonAggregatedGroupField { get; private set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0002C274 File Offset: 0x0002A474
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x0002C27C File Offset: 0x0002A47C
		internal QueryResultFieldSortInformation SortInfo { get; private set; }

		// Token: 0x06000FFF RID: 4095 RVA: 0x0002C285 File Offset: 0x0002A485
		internal QueryResultField(ConceptualTypeColumn field, string dataFieldName, string rawUnqualifiedFieldName, ConceptualTypeColumn aggregateIndicatorField, bool isNonAggregatedGroupField, QueryResultFieldSortInformation sortInfo)
		{
			this.Field = field;
			this.DataFieldName = dataFieldName;
			this.RawUnqualifiedFieldName = rawUnqualifiedFieldName;
			this.AggregateIndicatorField = aggregateIndicatorField;
			this.IsNonAggregatedGroupField = isNonAggregatedGroupField;
			this.SortInfo = sortInfo;
		}
	}
}
