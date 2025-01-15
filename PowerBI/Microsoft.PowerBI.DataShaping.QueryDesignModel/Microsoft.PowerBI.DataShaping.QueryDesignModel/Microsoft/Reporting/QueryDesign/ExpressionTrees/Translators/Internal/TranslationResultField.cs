using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000152 RID: 338
	internal sealed class TranslationResultField
	{
		// Token: 0x06001292 RID: 4754 RVA: 0x00035C72 File Offset: 0x00033E72
		internal TranslationResultField(ConceptualTypeColumn column, string columnName, string rawUnqualifiedColumnName, QueryResultFieldSortInformation sortInfo)
		{
			this.Field = column;
			this.ColumnName = columnName;
			this.RawUnqualifiedColumnName = rawUnqualifiedColumnName;
			this.SortInfo = sortInfo;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x00035C97 File Offset: 0x00033E97
		internal ConceptualTypeColumn Field { get; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00035C9F File Offset: 0x00033E9F
		internal string ColumnName { get; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00035CA7 File Offset: 0x00033EA7
		internal string RawUnqualifiedColumnName { get; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00035CAF File Offset: 0x00033EAF
		internal QueryResultFieldSortInformation SortInfo { get; }
	}
}
