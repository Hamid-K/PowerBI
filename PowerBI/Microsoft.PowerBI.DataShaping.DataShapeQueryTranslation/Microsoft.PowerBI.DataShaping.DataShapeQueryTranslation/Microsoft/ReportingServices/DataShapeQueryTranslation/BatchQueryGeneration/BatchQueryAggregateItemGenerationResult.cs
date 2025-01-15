using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.Reporting.QueryDesign.BatchQueries;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000131 RID: 305
	internal sealed class BatchQueryAggregateItemGenerationResult
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x0002DB1C File Offset: 0x0002BD1C
		internal BatchQueryAggregateItemGenerationResult(IEnumerable<QueryTableColumn> columns, GeneratedColumnMap columnMap)
		{
			this.Columns = columns.ToReadOnlyCollection<QueryTableColumn>();
			this.ColumnMap = columnMap;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002DB37 File Offset: 0x0002BD37
		public ReadOnlyCollection<QueryTableColumn> Columns { get; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002DB3F File Offset: 0x0002BD3F
		public GeneratedColumnMap ColumnMap { get; }
	}
}
