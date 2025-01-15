using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000153 RID: 339
	internal sealed class WritableGeneratedColumnMap : GeneratedColumnMap
	{
		// Token: 0x06000C85 RID: 3205 RVA: 0x00033F16 File Offset: 0x00032116
		internal WritableGeneratedColumnMap()
		{
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00033F1E File Offset: 0x0003211E
		internal WritableGeneratedColumnMap(Dictionary<ExpressionId, QueryTableColumn> expressionMap, Dictionary<string, QueryTableColumn> planColumnNameMap)
			: base(expressionMap, planColumnNameMap)
		{
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00033F28 File Offset: 0x00032128
		public void Add(ExpressionId expressionId, QueryTableColumn column)
		{
			this.m_expressionMap.Add(expressionId, column);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00033F37 File Offset: 0x00032137
		public void AddColumnMap(GeneratedColumnMap map, HashSet<QueryTableColumn> excludedColumns = null)
		{
			GeneratedColumnMap.AddPlanColumnNameMap(this.m_planColumnNameMap, map.PlanColumnNameMap, excludedColumns);
			GeneratedColumnMap.AddExpressionMap(this.m_expressionMap, map.ExpressionMap, excludedColumns);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00033F5D File Offset: 0x0003215D
		public void Add(string planColumnName, QueryTableColumn column)
		{
			this.m_planColumnNameMap.Add(planColumnName, column);
		}
	}
}
