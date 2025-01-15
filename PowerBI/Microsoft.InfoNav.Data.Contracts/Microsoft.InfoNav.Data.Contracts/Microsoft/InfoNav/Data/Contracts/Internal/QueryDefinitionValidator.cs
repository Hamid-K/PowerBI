using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Common.Internal;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002A1 RID: 673
	public class QueryDefinitionValidator : DefaultQueryDefinitionVisitor<IErrorContext>
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x00024BDD File Offset: 0x00022DDD
		public QueryDefinitionValidator(QueryExpressionValidator expressionValidator)
		{
			this._expressionValidator = expressionValidator;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00024BEC File Offset: 0x00022DEC
		public void Visit(IErrorContext errorContext, QueryAxis queryAxis)
		{
			this.VisitAxis(errorContext, queryAxis);
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00024BF6 File Offset: 0x00022DF6
		public void Visit(IErrorContext errorContext, QueryAxisGroup queryAxisGroup)
		{
			this.VisitAxisGroup(errorContext, queryAxisGroup);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00024C00 File Offset: 0x00022E00
		public void Visit(IErrorContext errorContext, QueryFilter queryFilter)
		{
			this.VisitFilter(errorContext, queryFilter);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x00024C0A File Offset: 0x00022E0A
		public void Visit(IErrorContext errorContext, QuerySortClause sortClause)
		{
			this.VisitSortClause(errorContext, sortClause);
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00024C14 File Offset: 0x00022E14
		public void Visit(IErrorContext errorContext, EntitySource source)
		{
			this.VisitEntitySource(errorContext, source);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00024C1E File Offset: 0x00022E1E
		public void Visit(IErrorContext errorContext, FilterDefinition filter)
		{
			if (filter.From.IsNullOrEmpty<EntitySource>())
			{
				errorContext.RegisterError(QueryValidationMessages.FilterDefinitionMissingEntitySources, new object[0]);
				return;
			}
			base.VisitFilterDefinition(errorContext, filter);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00024C48 File Offset: 0x00022E48
		public static bool IsValid(QueryDefinition queryDefinition)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryDefinitionValidator.CreateQueryDefinitionValidator(errorTrackingOnlyErrorContext).Visit(errorTrackingOnlyErrorContext, queryDefinition);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00024C74 File Offset: 0x00022E74
		public static bool IsValid(EntitySource source)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryDefinitionValidator.CreateQueryDefinitionValidator(errorTrackingOnlyErrorContext).VisitEntitySource(errorTrackingOnlyErrorContext, source);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00024CA0 File Offset: 0x00022EA0
		public static bool IsValid(QuerySortClause sortClause)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryDefinitionValidator.CreateQueryDefinitionValidator(errorTrackingOnlyErrorContext).VisitSortClause(errorTrackingOnlyErrorContext, sortClause);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00024CCC File Offset: 0x00022ECC
		public static bool IsParameterDeclarationValid(QueryExpressionContainer expression)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryDefinitionValidator.CreateQueryDefinitionValidator(errorTrackingOnlyErrorContext).VisitParameterDeclaration(errorTrackingOnlyErrorContext, expression);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00024CF8 File Offset: 0x00022EF8
		public static bool IsLetBindingValid(QueryExpressionContainer expression)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryDefinitionValidator.CreateQueryDefinitionValidator(errorTrackingOnlyErrorContext).VisitLetBinding(errorTrackingOnlyErrorContext, expression);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00024D24 File Offset: 0x00022F24
		public static bool IsValid(QueryFilter queryFilter)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryDefinitionValidator.CreateQueryDefinitionValidator(errorTrackingOnlyErrorContext).Visit(errorTrackingOnlyErrorContext, queryFilter);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00024D50 File Offset: 0x00022F50
		public static bool IsValid(QueryAxis axis)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryDefinitionValidator.CreateQueryDefinitionValidator(errorTrackingOnlyErrorContext).Visit(errorTrackingOnlyErrorContext, axis);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00024D7C File Offset: 0x00022F7C
		public static bool IsValid(QueryAxisGroup axisGroup)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryDefinitionValidator.CreateQueryDefinitionValidator(errorTrackingOnlyErrorContext).Visit(errorTrackingOnlyErrorContext, axisGroup);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00024DA8 File Offset: 0x00022FA8
		public static bool IsValid(QueryTransform transform)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryDefinitionValidator.CreateQueryDefinitionValidator(errorTrackingOnlyErrorContext).VisitTransform(errorTrackingOnlyErrorContext, transform);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00024DD4 File Offset: 0x00022FD4
		public override void Visit(IErrorContext errorContext, QueryDefinition queryDefinition)
		{
			if (queryDefinition.Select.IsNullOrEmpty<QueryExpressionContainer>())
			{
				errorContext.RegisterError(QueryValidationMessages.MissingSelect, new object[0]);
				return;
			}
			if (queryDefinition.Top != null && queryDefinition.Top.Value <= 0)
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidTopValue, new object[0]);
				return;
			}
			if (queryDefinition.Skip != null)
			{
				long? skip = queryDefinition.Skip;
				long num = 0L;
				if ((skip.GetValueOrDefault() < num) & (skip != null))
				{
					errorContext.RegisterError(QueryValidationMessages.InvalidSkipValue, new object[0]);
					return;
				}
			}
			if (queryDefinition.Skip != null && queryDefinition.Top == null)
			{
				errorContext.RegisterError(QueryValidationMessages.SkipWithoutTop, new object[0]);
				return;
			}
			base.Visit(errorContext, queryDefinition);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00024EAD File Offset: 0x000230AD
		protected override void VisitExpression(IErrorContext errorContext, QueryExpressionContainer expression)
		{
			if (expression == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullExpressionContainer, new object[0]);
				return;
			}
			this._expressionValidator.SetQueryValidator(this);
			this._expressionValidator.ValidateExpression(expression);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00024EE4 File Offset: 0x000230E4
		protected override void VisitEntitySource(IErrorContext errorContext, EntitySource source)
		{
			if (source == null)
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidEntitySource(source), new object[0]);
				return;
			}
			if (string.IsNullOrEmpty(source.Name))
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidEntitySourceMissingName(), new object[0]);
				return;
			}
			EntitySourceType type = source.Type;
			if (type > EntitySourceType.Pod)
			{
				if (type != EntitySourceType.Expression)
				{
					errorContext.RegisterError(QueryValidationMessages.InvalidEntitySource(source), new object[0]);
					return;
				}
				if (!string.IsNullOrEmpty(source.EntitySet) || !string.IsNullOrEmpty(source.Entity) || !string.IsNullOrEmpty(source.Schema))
				{
					errorContext.RegisterError(QueryValidationMessages.InvalidEntitySource(source), new object[0]);
					return;
				}
				if (!(source.Expression != null) || (!(source.Expression.Subquery != null) && !(source.Expression.LetRef != null) && !(source.Expression.ParameterRef != null)))
				{
					errorContext.RegisterError(QueryValidationMessages.InvalidEntitySource(source), new object[0]);
					return;
				}
			}
			else
			{
				if (source.Expression != null)
				{
					errorContext.RegisterError(QueryValidationMessages.InvalidEntitySource(source), new object[0]);
					return;
				}
				if (string.IsNullOrEmpty(source.EntitySet) == string.IsNullOrEmpty(source.Entity))
				{
					errorContext.RegisterError(QueryValidationMessages.InvalidEntitySource(source), new object[0]);
					return;
				}
			}
			base.VisitEntitySource(errorContext, source);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00025040 File Offset: 0x00023240
		protected override void VisitSortClause(IErrorContext errorContext, QuerySortClause sortClause)
		{
			if (sortClause == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullSortClause, new object[0]);
				return;
			}
			if (sortClause.Expression == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullSortClauseExpression, new object[0]);
				return;
			}
			if (!sortClause.Direction.IsValid())
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidSortClauseDirection, new object[0]);
				return;
			}
			base.VisitSortClause(errorContext, sortClause);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x000250B0 File Offset: 0x000232B0
		protected override void VisitParameterDeclaration(IErrorContext errorContext, QueryExpressionContainer declaration)
		{
			if (declaration == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullParameterDeclaration, new object[0]);
				return;
			}
			if (string.IsNullOrEmpty(declaration.Name))
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidQueryParameterDeclarationMissingName(), new object[0]);
				return;
			}
			if (declaration.Expression == null)
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidQueryParameterDeclarationMissingExpression(declaration.Name), new object[0]);
				return;
			}
			if (declaration.TypeOf == null && declaration.TableType == null && declaration.PrimitiveType == null)
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidQueryParameterDeclarationInvalidExpressionType(declaration.Name, declaration), new object[0]);
				return;
			}
			this._expressionValidator.ValidateStandaloneExpression(declaration);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0002516C File Offset: 0x0002336C
		protected override void VisitLetBinding(IErrorContext errorContext, QueryExpressionContainer letBinding)
		{
			if (letBinding == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullLetBinding, new object[0]);
				return;
			}
			if (string.IsNullOrEmpty(letBinding.Name))
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidLetBindingMissingName(), new object[0]);
				return;
			}
			if (letBinding.Expression == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullLetBindingExpression, new object[0]);
				return;
			}
			base.VisitLetBinding(errorContext, letBinding);
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x000251DC File Offset: 0x000233DC
		protected override void VisitVisualShape(IErrorContext errorContext, List<QueryAxis> axes)
		{
			if (axes == null)
			{
				return;
			}
			HashSet<string> hashSet = new HashSet<string>(QueryNameComparer.Instance);
			foreach (QueryAxis queryAxis in axes)
			{
				if (queryAxis != null && !hashSet.Add(queryAxis.Name))
				{
					errorContext.RegisterError(QueryValidationMessages.DuplicatedAxisName, new object[] { queryAxis.Name.MarkAsCustomerContent() });
				}
			}
			base.VisitVisualShape(errorContext, axes);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00025270 File Offset: 0x00023470
		protected override void VisitAxis(IErrorContext errorContext, QueryAxis axis)
		{
			if (axis == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullAxis, new object[0]);
				return;
			}
			if (string.IsNullOrEmpty(axis.Name) || !QueryDefinitionValidator.ValidAxisNames.Contains(axis.Name))
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidAxisName, new object[0]);
				return;
			}
			if (axis.Groups.IsNullOrEmpty<QueryAxisGroup>())
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidAxisGroups, new object[0]);
				return;
			}
			base.VisitAxis(errorContext, axis);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x000252F0 File Offset: 0x000234F0
		protected override void VisitAxisGroup(IErrorContext errorContext, QueryAxisGroup axisGroup)
		{
			if (axisGroup == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullAxisGroup, new object[0]);
				return;
			}
			if (axisGroup.Keys.IsNullOrEmpty<QueryExpressionContainer>())
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidAxisGroupKeys, new object[0]);
				return;
			}
			base.VisitAxisGroup(errorContext, axisGroup);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00025340 File Offset: 0x00023540
		protected override void VisitFilter(IErrorContext errorContext, QueryFilter filter)
		{
			if (filter == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullFilter, new object[0]);
				return;
			}
			if (!(filter.Condition != null))
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidFilterCondition, new object[0]);
				return;
			}
			if (filter.Target != null)
			{
				bool flag;
				if (!filter.Target.All((QueryExpressionContainer x) => x.SourceRef != null))
				{
					flag = filter.Target.All((QueryExpressionContainer x) => x.Property != null || x.HierarchyLevel != null);
				}
				else
				{
					flag = true;
				}
				bool flag2 = flag;
				if (filter.Target.Count <= 0 || !flag2)
				{
					errorContext.RegisterError(QueryValidationMessages.InvalidFilterTarget, new object[0]);
					return;
				}
			}
			base.VisitFilter(errorContext, filter);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00025418 File Offset: 0x00023618
		protected override void VisitTransform(IErrorContext errorContext, QueryTransform transform)
		{
			if (transform == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullTransform, new object[0]);
				return;
			}
			if (string.IsNullOrEmpty(transform.Name))
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidTransformMissingName(), new object[0]);
				return;
			}
			if (string.IsNullOrEmpty(transform.Algorithm))
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidTransformMissingAlgorithm, new object[0]);
				return;
			}
			if (transform.Input == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullTransformInput, new object[0]);
				return;
			}
			if (transform.Output == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullTransformOutput, new object[0]);
				return;
			}
			base.VisitTransform(errorContext, transform);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x000254B4 File Offset: 0x000236B4
		protected override void VisitTransformInput(IErrorContext errorContext, QueryTransformInput transformInput)
		{
			if (transformInput.Parameters != null)
			{
				for (int i = 0; i < transformInput.Parameters.Count; i++)
				{
					QueryExpressionContainer queryExpressionContainer = transformInput.Parameters[i];
					if (queryExpressionContainer == null)
					{
						errorContext.RegisterError(QueryValidationMessages.NullTransformInputParameter, new object[0]);
						return;
					}
					if (string.IsNullOrEmpty(queryExpressionContainer.Name))
					{
						errorContext.RegisterError(QueryValidationMessages.InvalidTransformInputParameterMissingName(), new object[0]);
						return;
					}
					if (queryExpressionContainer.Expression == null)
					{
						errorContext.RegisterError(QueryValidationMessages.NullTransformInputParameterExpression, new object[0]);
						return;
					}
				}
			}
			if (transformInput.Table == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullTransformInputTable, new object[0]);
				return;
			}
			base.VisitTransformInput(errorContext, transformInput);
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00025568 File Offset: 0x00023768
		protected override void VisitTransformOutput(IErrorContext errorContext, QueryTransformOutput transformOutput)
		{
			if (transformOutput.Table == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullTransformOutputTable, new object[0]);
				return;
			}
			base.VisitTransformOutput(errorContext, transformOutput);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0002558C File Offset: 0x0002378C
		protected override void VisitTransformTable(IErrorContext errorContext, QueryTransformTable transformTable)
		{
			if (string.IsNullOrEmpty(transformTable.Name))
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidTransformTableMissingName(), new object[0]);
				return;
			}
			if (transformTable.Columns == null || transformTable.Columns.Count <= 0)
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidTransformTableColumns, new object[0]);
				return;
			}
			base.VisitTransformTable(errorContext, transformTable);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x000255E8 File Offset: 0x000237E8
		protected override void VisitTransformTableColumn(IErrorContext errorContext, QueryTransformTableColumn transformTableColumn)
		{
			if (transformTableColumn == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullTransformTableColumn, new object[0]);
				return;
			}
			if (transformTableColumn.Expression == null)
			{
				errorContext.RegisterError(QueryValidationMessages.NullTransformTableColumnExpression, new object[0]);
				return;
			}
			if (string.IsNullOrEmpty(transformTableColumn.Expression.Name))
			{
				errorContext.RegisterError(QueryValidationMessages.InvalidTransformTableColumnExpressionMissingName(), new object[0]);
				return;
			}
			base.VisitTransformTableColumn(errorContext, transformTableColumn);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00025656 File Offset: 0x00023856
		private static QueryDefinitionValidator CreateQueryDefinitionValidator(ErrorTrackingOnlyErrorContext errorContext)
		{
			return new QueryDefinitionValidator(new QueryExpressionValidator(errorContext));
		}

		// Token: 0x04000841 RID: 2113
		private readonly QueryExpressionValidator _expressionValidator;

		// Token: 0x04000842 RID: 2114
		private static readonly HashSet<string> ValidAxisNames = new HashSet<string>(QueryNameComparer.Instance) { "rows", "columns", "rowpages" };
	}
}
