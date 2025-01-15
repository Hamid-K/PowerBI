using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001BB RID: 443
	internal sealed class AggregatesTableModelExpressionRewriter
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x0003F0BC File Offset: 0x0003D2BC
		internal AggregatesTableModelExpressionRewriter(TranslationErrorContext errorContext, WritableExpressionTable expressionTable, NamingContext namingContext)
		{
			this.m_errorContext = errorContext;
			this.m_expressionTable = expressionTable;
			this.m_namingContext = namingContext;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003F0D9 File Offset: 0x0003D2D9
		public List<SingleRowAdditionalColumn> Columns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0003F0E4 File Offset: 0x0003D2E4
		public void Rewrite(Calculation calculation, ExpressionId expressionId)
		{
			ExpressionNode expressionNode = this.m_expressionTable.GetNode(expressionId);
			if (ModelReferenceAnalyzer.ContainsModelReference(expressionNode))
			{
				expressionNode = this.ReplaceWithColumn(expressionNode, calculation);
			}
			this.m_expressionTable.SetNode(expressionId, expressionNode);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0003F11C File Offset: 0x0003D31C
		private ExpressionNode ReplaceWithColumn(ExpressionNode exprNode, Calculation calculation)
		{
			string text = this.m_namingContext.GenerateUniqueName(PlanNames.Argument(calculation.Id.Value));
			ExpressionId expressionId = this.m_expressionTable.Add(exprNode);
			SingleRowAdditionalColumn singleRowAdditionalColumn = new SingleRowAdditionalColumn(text, expressionId, this.GetExpressionContext(calculation));
			this.AddColumn(singleRowAdditionalColumn);
			return new BatchColumnReferenceExpressionNode(text);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0003F16C File Offset: 0x0003D36C
		private void AddColumn(SingleRowAdditionalColumn column)
		{
			if (this.m_columns == null)
			{
				this.m_columns = new List<SingleRowAdditionalColumn>();
			}
			this.m_columns.Add(column);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0003F18D File Offset: 0x0003D38D
		private ExpressionContext GetExpressionContext(Calculation calculation)
		{
			return new ExpressionContext(this.m_errorContext, calculation.ObjectType, calculation.Id, "Value");
		}

		// Token: 0x04000751 RID: 1873
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000752 RID: 1874
		private readonly WritableExpressionTable m_expressionTable;

		// Token: 0x04000753 RID: 1875
		private readonly NamingContext m_namingContext;

		// Token: 0x04000754 RID: 1876
		private List<SingleRowAdditionalColumn> m_columns;
	}
}
