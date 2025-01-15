using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002A5 RID: 677
	public abstract class DefaultQueryDefinitionVisitor<TContext> : QueryDefinitionVisitor<TContext>
	{
		// Token: 0x060014B9 RID: 5305 RVA: 0x00025F26 File Offset: 0x00024126
		protected override void VisitParameterDeclaration(TContext context, QueryExpressionContainer parameterDeclaration)
		{
			this.VisitExpression(context, parameterDeclaration);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00025F30 File Offset: 0x00024130
		protected override void VisitLetBinding(TContext context, QueryExpressionContainer letBinding)
		{
			this.VisitExpression(context, letBinding);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00025F3C File Offset: 0x0002413C
		protected override void VisitFilter(TContext context, QueryFilter filter)
		{
			List<QueryExpressionContainer> target = filter.Target;
			if (!target.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in target)
				{
					this.VisitExpression(context, queryExpressionContainer);
				}
			}
			if (filter.Condition != null)
			{
				this.VisitExpression(context, filter.Condition);
			}
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00025FB8 File Offset: 0x000241B8
		protected override void VisitVisualShape(TContext context, List<QueryAxis> axes)
		{
			foreach (QueryAxis queryAxis in axes)
			{
				this.VisitAxis(context, queryAxis);
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00026008 File Offset: 0x00024208
		protected override void VisitAxis(TContext context, QueryAxis axis)
		{
			List<QueryAxisGroup> groups = axis.Groups;
			if (!groups.IsNullOrEmpty<QueryAxisGroup>())
			{
				foreach (QueryAxisGroup queryAxisGroup in groups)
				{
					this.VisitAxisGroup(context, queryAxisGroup);
				}
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00026068 File Offset: 0x00024268
		protected override void VisitAxisGroup(TContext context, QueryAxisGroup axisGroup)
		{
			List<QueryExpressionContainer> keys = axisGroup.Keys;
			if (!keys.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in keys)
				{
					this.VisitExpression(context, queryExpressionContainer);
				}
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000260C8 File Offset: 0x000242C8
		protected override void VisitSortClause(TContext context, QuerySortClause sortClause)
		{
			if (sortClause.Expression != null)
			{
				this.VisitExpression(context, sortClause.Expression);
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x000260E5 File Offset: 0x000242E5
		protected override void VisitTransform(TContext context, QueryTransform transform)
		{
			if (transform.Input != null)
			{
				this.VisitTransformInput(context, transform.Input);
			}
			if (transform.Output != null)
			{
				this.VisitTransformOutput(context, transform.Output);
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00026114 File Offset: 0x00024314
		protected virtual void VisitTransformInput(TContext context, QueryTransformInput transformInput)
		{
			List<QueryExpressionContainer> parameters = transformInput.Parameters;
			if (!parameters.IsNullOrEmpty<QueryExpressionContainer>())
			{
				foreach (QueryExpressionContainer queryExpressionContainer in parameters)
				{
					this.VisitExpression(context, queryExpressionContainer);
				}
			}
			if (transformInput.Table != null)
			{
				this.VisitTransformTable(context, transformInput.Table);
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00026188 File Offset: 0x00024388
		protected virtual void VisitTransformOutput(TContext context, QueryTransformOutput transformOutput)
		{
			if (transformOutput.Table != null)
			{
				this.VisitTransformTable(context, transformOutput.Table);
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000261A0 File Offset: 0x000243A0
		protected virtual void VisitTransformTable(TContext context, QueryTransformTable transformTable)
		{
			List<QueryTransformTableColumn> columns = transformTable.Columns;
			if (!columns.IsNullOrEmpty<QueryTransformTableColumn>())
			{
				foreach (QueryTransformTableColumn queryTransformTableColumn in columns)
				{
					this.VisitTransformTableColumn(context, queryTransformTableColumn);
				}
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00026200 File Offset: 0x00024400
		protected virtual void VisitTransformTableColumn(TContext context, QueryTransformTableColumn transformTableColumn)
		{
			if (transformTableColumn.Expression != null)
			{
				this.VisitExpression(context, transformTableColumn.Expression);
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0002621D File Offset: 0x0002441D
		protected override void VisitExpression(TContext context, QueryExpressionContainer expression)
		{
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0002621F File Offset: 0x0002441F
		protected override void VisitEntitySource(TContext context, EntitySource source)
		{
			if (source.Expression != null)
			{
				this.VisitExpression(context, source.Expression);
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0002623C File Offset: 0x0002443C
		protected void VisitFilterDefinition(TContext context, FilterDefinition filter)
		{
			if (!filter.From.IsNullOrEmpty<EntitySource>())
			{
				foreach (EntitySource entitySource in filter.From)
				{
					this.VisitEntitySource(context, entitySource);
				}
			}
			if (!filter.Where.IsNullOrEmpty<QueryFilter>())
			{
				foreach (QueryFilter queryFilter in filter.Where)
				{
					this.VisitFilter(context, queryFilter);
				}
			}
		}
	}
}
