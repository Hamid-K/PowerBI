using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000C0 RID: 192
	internal sealed class DataTransformRestorationResult
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x0001F8D3 File Offset: 0x0001DAD3
		internal DataTransformRestorationResult(IReadOnlyList<ReadOnlyExpressionTable> expressionTables)
		{
			this.m_expressionTables = expressionTables;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001F8E2 File Offset: 0x0001DAE2
		internal DataTransformRestorationResult(IReadOnlyList<ReadOnlyExpressionTable> expressionTables, ReadOnlyExpressionTable transformExpressionTable, IReadOnlyDictionary<ResultSetReference, DataTransformTable> resultSetToTableMapping)
			: this(expressionTables)
		{
			this.m_transformExpressionTable = transformExpressionTable;
			this.m_resultSetToTableMapping = resultSetToTableMapping;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001F8F9 File Offset: 0x0001DAF9
		internal IReadOnlyList<ReadOnlyExpressionTable> ExpressionTables
		{
			get
			{
				return this.m_expressionTables;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0001F901 File Offset: 0x0001DB01
		internal ReadOnlyExpressionTable TransformExpressionTable
		{
			get
			{
				return this.m_transformExpressionTable;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001F909 File Offset: 0x0001DB09
		internal IReadOnlyDictionary<ResultSetReference, DataTransformTable> ResultSetToTableMapping
		{
			get
			{
				return this.m_resultSetToTableMapping;
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001F911 File Offset: 0x0001DB11
		internal bool TryGetTransformTableForResultSet(ResultSetReference resultSetRef, out DataTransformTable table)
		{
			if (this.m_resultSetToTableMapping == null)
			{
				table = null;
				return false;
			}
			return this.m_resultSetToTableMapping.TryGetValue(resultSetRef, out table);
		}

		// Token: 0x0400040E RID: 1038
		private readonly IReadOnlyList<ReadOnlyExpressionTable> m_expressionTables;

		// Token: 0x0400040F RID: 1039
		private readonly ReadOnlyExpressionTable m_transformExpressionTable;

		// Token: 0x04000410 RID: 1040
		private readonly IReadOnlyDictionary<ResultSetReference, DataTransformTable> m_resultSetToTableMapping;
	}
}
