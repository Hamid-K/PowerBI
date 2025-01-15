using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002A4 RID: 676
	public abstract class DefaultQueryDefinitionVisitor : QueryDefinitionVisitor
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x00025C50 File Offset: 0x00023E50
		protected override void VisitParameterDeclaration(QueryExpressionContainer parameterDeclaration)
		{
			this.VisitExpression(parameterDeclaration);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00025C59 File Offset: 0x00023E59
		protected override void VisitLetBinding(QueryExpressionContainer letBinding)
		{
			this.VisitExpression(letBinding);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00025C62 File Offset: 0x00023E62
		protected override void VisitSelectExpression(QueryExpressionContainer expression)
		{
			this.VisitExpression(expression);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00025C6B File Offset: 0x00023E6B
		protected override void VisitGroupByExpression(QueryExpressionContainer expression)
		{
			this.VisitExpression(expression);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00025C74 File Offset: 0x00023E74
		protected override void VisitFilter(QueryFilter filter)
		{
			if (!filter.Target.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in filter.Target)
				{
					this.VisitExpression(queryExpressionContainer);
				}
			}
			if (filter.Condition != null)
			{
				this.VisitExpression(filter.Condition);
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00025CF0 File Offset: 0x00023EF0
		protected override void VisitAxis(QueryAxis axis)
		{
			if (!axis.Groups.IsNullOrEmpty<QueryAxisGroup>())
			{
				foreach (QueryAxisGroup queryAxisGroup in axis.Groups)
				{
					this.VisitAxisGroup(queryAxisGroup);
				}
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00025D50 File Offset: 0x00023F50
		protected override void VisitAxisGroup(QueryAxisGroup axisGroup)
		{
			if (!axisGroup.Keys.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in axisGroup.Keys)
				{
					this.VisitExpression(queryExpressionContainer);
				}
			}
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00025DB0 File Offset: 0x00023FB0
		protected override void VisitSortClause(QuerySortClause sortClause)
		{
			if (sortClause.Expression != null)
			{
				this.VisitExpression(sortClause.Expression);
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00025DCC File Offset: 0x00023FCC
		protected override void VisitTransform(QueryTransform transform)
		{
			if (transform.Input != null)
			{
				this.VisitTransformInput(transform.Input);
			}
			if (transform.Output != null)
			{
				this.VisitTransformOutput(transform.Output);
			}
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00025DF8 File Offset: 0x00023FF8
		protected virtual void VisitTransformInput(QueryTransformInput transformInput)
		{
			List<QueryExpressionContainer> parameters = transformInput.Parameters;
			if (!parameters.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in parameters)
				{
					this.VisitExpression(queryExpressionContainer);
				}
			}
			if (transformInput.Table != null)
			{
				this.VisitTransformTable(transformInput.Table);
			}
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00025E6C File Offset: 0x0002406C
		protected virtual void VisitTransformOutput(QueryTransformOutput transformOutput)
		{
			if (transformOutput.Table != null)
			{
				this.VisitTransformTable(transformOutput.Table);
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00025E84 File Offset: 0x00024084
		protected virtual void VisitTransformTable(QueryTransformTable transformTable)
		{
			List<QueryTransformTableColumn> columns = transformTable.Columns;
			if (!columns.IsNullOrEmpty<QueryTransformTableColumn>())
			{
				foreach (QueryTransformTableColumn queryTransformTableColumn in columns)
				{
					this.VisitTransformTableColumn(queryTransformTableColumn);
				}
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00025EE4 File Offset: 0x000240E4
		protected virtual void VisitTransformTableColumn(QueryTransformTableColumn transformTableColumn)
		{
			if (transformTableColumn.Expression != null)
			{
				this.VisitExpression(transformTableColumn.Expression);
			}
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00025F00 File Offset: 0x00024100
		protected override void VisitExpression(QueryExpressionContainer expression)
		{
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00025F02 File Offset: 0x00024102
		protected override void VisitEntitySource(EntitySource source)
		{
			if (source.Expression != null)
			{
				this.VisitExpression(source.Expression);
			}
		}
	}
}
