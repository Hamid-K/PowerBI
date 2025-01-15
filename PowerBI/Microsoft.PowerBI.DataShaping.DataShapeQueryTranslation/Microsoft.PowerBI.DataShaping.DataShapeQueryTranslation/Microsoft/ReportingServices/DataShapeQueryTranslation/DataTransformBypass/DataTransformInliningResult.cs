using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000BF RID: 191
	internal sealed class DataTransformInliningResult
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x0001F884 File Offset: 0x0001DA84
		internal DataTransformInliningResult(ReadOnlyExpressionTable expressionTable)
		{
			this.m_expressionTable = expressionTable;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001F893 File Offset: 0x0001DA93
		internal DataTransformInliningResult(ReadOnlyExpressionTable expressionTable, IReadOnlyList<ExpressionRestorationInfo> expressionsToRestore, IReadOnlyDictionary<DataTransformTableColumn, DataTransformColumnInliningInfo> sourceColumns)
		{
			this.m_expressionTable = expressionTable;
			this.m_expressionsToRestore = expressionsToRestore;
			this.m_sourceColumns = sourceColumns;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0001F8B0 File Offset: 0x0001DAB0
		internal bool RequiresRestoration
		{
			get
			{
				return this.m_expressionsToRestore != null;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001F8BB File Offset: 0x0001DABB
		internal ReadOnlyExpressionTable ExpressionTable
		{
			get
			{
				return this.m_expressionTable;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0001F8C3 File Offset: 0x0001DAC3
		internal IReadOnlyList<ExpressionRestorationInfo> ExpressionsToRestore
		{
			get
			{
				return this.m_expressionsToRestore;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001F8CB File Offset: 0x0001DACB
		internal IReadOnlyDictionary<DataTransformTableColumn, DataTransformColumnInliningInfo> SourceColumns
		{
			get
			{
				return this.m_sourceColumns;
			}
		}

		// Token: 0x0400040B RID: 1035
		private readonly ReadOnlyExpressionTable m_expressionTable;

		// Token: 0x0400040C RID: 1036
		private readonly IReadOnlyList<ExpressionRestorationInfo> m_expressionsToRestore;

		// Token: 0x0400040D RID: 1037
		private readonly IReadOnlyDictionary<DataTransformTableColumn, DataTransformColumnInliningInfo> m_sourceColumns;
	}
}
