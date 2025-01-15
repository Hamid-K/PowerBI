using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000269 RID: 617
	internal sealed class QueryExpressionResolver : QueryExpressionVisitor<ResolvedQueryExpression>
	{
		// Token: 0x06001281 RID: 4737 RVA: 0x00020B25 File Offset: 0x0001ED25
		internal QueryExpressionResolver(QuerySourceContext sourceContext, QueryResolutionErrorContext errorContext, QueryTransformTableContext transformTableContext, HashSet<string> querySourceUsedNames, IQueryDefinitionNameRegistrar queryDefinitionNames, ImmutableDictionary<string, ResolvedQueryLetBinding> letMap, IReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> parameterMap)
		{
			this._sourceContext = sourceContext;
			this._errorContext = errorContext;
			this._transformTableContext = transformTableContext;
			this._querySourceUsedNames = querySourceUsedNames;
			this._queryDefinitionNames = queryDefinitionNames;
			this._letMap = letMap;
			this._parameterMap = parameterMap;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00020B62 File Offset: 0x0001ED62
		internal QuerySourceContext SourceContext
		{
			get
			{
				return this._sourceContext;
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00020B6C File Offset: 0x0001ED6C
		protected internal override ResolvedQueryExpression Visit(QuerySourceRefExpression expression)
		{
			if (!string.IsNullOrEmpty(expression.Source))
			{
				ResolvedQuerySource resolvedQuerySource;
				if (this._sourceContext.SourceMap.TryGetValue(expression.Source, out resolvedQuerySource))
				{
					ResolvedEntitySource resolvedEntitySource = resolvedQuerySource as ResolvedEntitySource;
					if (resolvedEntitySource != null)
					{
						return resolvedEntitySource.SourceRef();
					}
					ResolvedExpressionSource resolvedExpressionSource = resolvedQuerySource as ResolvedExpressionSource;
					if (resolvedExpressionSource != null)
					{
						return resolvedExpressionSource.ExpressionSourceRef();
					}
				}
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveSourceEntityReference(expression.Source));
			}
			if (string.IsNullOrEmpty(expression.Entity))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveSourceEntityReference(string.Empty));
			}
			IConceptualSchema conceptualSchema;
			if (!this._sourceContext.FederatedSchema.TryGetSchema(expression.Schema, out conceptualSchema))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveSourceSchemaReference(expression.Schema));
			}
			IConceptualEntity conceptualEntity;
			if (!conceptualSchema.TryGetEntity(expression.Entity, out conceptualEntity))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveSourceEntityReference(expression.Entity));
			}
			return new ResolvedQuerySourceRefExpression(conceptualEntity);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00020C48 File Offset: 0x0001EE48
		protected internal override ResolvedQueryExpression Visit(QueryColumnExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression;
			if (this.TryResolveTransformTableColumn(expression, out resolvedQueryExpression))
			{
				return resolvedQueryExpression;
			}
			ResolvedQueryExpression resolvedQueryExpression2 = this.VisitExpression(expression.Expression);
			if (this.TryResolveColumnReference(resolvedQueryExpression2, expression.Property, out resolvedQueryExpression))
			{
				return resolvedQueryExpression;
			}
			return this.TryResolveColumn(expression, resolvedQueryExpression2);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00020C8C File Offset: 0x0001EE8C
		private ResolvedQueryExpression TryResolveColumn(QueryColumnExpression expression, ResolvedQueryExpression sourceExpr)
		{
			IConceptualEntity conceptualEntity;
			if (!QueryExpressionResolver.TryGetSourceEntity(sourceExpr, out conceptualEntity))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidPropertyExpression("Column"));
			}
			IConceptualProperty conceptualProperty;
			if (!conceptualEntity.TryGetProperty(expression.Property, out conceptualProperty))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveColumnReference(expression.Property, conceptualEntity.Name));
			}
			IConceptualColumn conceptualColumn = conceptualProperty as IConceptualColumn;
			if (conceptualColumn == null)
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveColumnReference(expression.Property, conceptualEntity.Name));
			}
			return sourceExpr.Column(conceptualColumn);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00020D08 File Offset: 0x0001EF08
		private bool TryResolveTransformTableColumn(QueryColumnExpression expression, out ResolvedQueryExpression resolvedExpr)
		{
			resolvedExpr = null;
			QueryTransformTableRefExpression transformTableRef = expression.Expression.TransformTableRef;
			if (transformTableRef == null)
			{
				return false;
			}
			ResolvedQueryTransformTable resolvedQueryTransformTable;
			if (this.TryResolveTransformTableRef(transformTableRef.Source, out resolvedQueryTransformTable))
			{
				ResolvedQueryTransformTableColumn resolvedQueryTransformTableColumn = this.ResolveTransformTableColumn(resolvedQueryTransformTable, expression.Property);
				resolvedExpr = resolvedQueryTransformTable.TransformTableColumn(resolvedQueryTransformTableColumn);
				return true;
			}
			return false;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00020D59 File Offset: 0x0001EF59
		private bool TryResolveTransformTableRef(string name, out ResolvedQueryTransformTable table)
		{
			if (this._transformTableContext == null)
			{
				table = null;
				return false;
			}
			if (!this._transformTableContext.TryResolveTable(name, out table))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.UnknownTransformTableRef(name));
			}
			return true;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00020D88 File Offset: 0x0001EF88
		private ResolvedQueryTransformTableColumn ResolveTransformTableColumn(ResolvedQueryTransformTable table, string columnName)
		{
			ResolvedQueryTransformTableColumn resolvedQueryTransformTableColumn;
			if (table.TryGetColumn(columnName, out resolvedQueryTransformTableColumn))
			{
				return resolvedQueryTransformTableColumn;
			}
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.UnknownTransformTableColumnRef(table.Name, columnName));
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00020DB4 File Offset: 0x0001EFB4
		private bool TryResolveColumnReference(ResolvedQueryExpression sourceExpr, string propertyName, out ResolvedQueryExpression resolvedExpr)
		{
			resolvedExpr = null;
			if (sourceExpr as ResolvedQueryExpressionSourceRefExpression == null)
			{
				return false;
			}
			resolvedExpr = sourceExpr.ColumnReference(propertyName);
			return true;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		protected internal override ResolvedQueryExpression Visit(QueryMeasureExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Expression);
			IConceptualEntity conceptualEntity;
			if (!QueryExpressionResolver.TryGetSourceEntity(resolvedQueryExpression, out conceptualEntity))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidPropertyExpression("Measure"));
			}
			IConceptualProperty conceptualProperty;
			if (!conceptualEntity.TryGetProperty(expression.Property, out conceptualProperty))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveMeasureReference(expression.Property, conceptualEntity.Name, conceptualEntity.Schema.SchemaId));
			}
			IConceptualMeasure conceptualMeasure = conceptualProperty as IConceptualMeasure;
			if (conceptualMeasure == null)
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveMeasureReference(expression.Property, conceptualEntity.Name, conceptualEntity.Schema.SchemaId));
			}
			return resolvedQueryExpression.Measure(conceptualMeasure);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00020E70 File Offset: 0x0001F070
		protected internal override ResolvedQueryExpression Visit(QueryHierarchyExpression expression)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(expression.Expression);
			IConceptualEntity conceptualEntity;
			if (!QueryExpressionResolver.TryGetSourceEntity(resolvedQueryExpression, out conceptualEntity))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidHierarchyExpression());
			}
			IConceptualHierarchy conceptualHierarchy;
			if (!conceptualEntity.TryGetHierarchy(expression.Hierarchy, out conceptualHierarchy))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveHierarchyReference(expression.Hierarchy, conceptualEntity.Name));
			}
			return resolvedQueryExpression.Hierarchy(conceptualHierarchy);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		protected internal override ResolvedQueryExpression Visit(QueryHierarchyLevelExpression expression)
		{
			ResolvedQueryHierarchyExpression resolvedQueryHierarchyExpression = this.VisitExpression(expression.Expression) as ResolvedQueryHierarchyExpression;
			if (resolvedQueryHierarchyExpression == null)
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidHierarchyLevelExpression());
			}
			IConceptualHierarchyLevel conceptualHierarchyLevel;
			if (!resolvedQueryHierarchyExpression.Hierarchy.TryGetLevel(expression.Level, out conceptualHierarchyLevel))
			{
				ResolvedQuerySourceRefExpression resolvedQuerySourceRefExpression = resolvedQueryHierarchyExpression.Expression as ResolvedQuerySourceRefExpression;
				string text;
				if (resolvedQuerySourceRefExpression != null)
				{
					text = resolvedQuerySourceRefExpression.SourceEntity.Name;
				}
				else
				{
					text = ((ResolvedQueryPropertyVariationSourceExpression)resolvedQueryHierarchyExpression.Expression).VariationSource.Name;
				}
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveHierarchyLevelReference(expression.Level, resolvedQueryHierarchyExpression.Hierarchy.Name, text));
			}
			return resolvedQueryHierarchyExpression.HierarchyLevel(conceptualHierarchyLevel);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00020F78 File Offset: 0x0001F178
		protected internal override ResolvedQueryExpression Visit(QueryPropertyVariationSourceExpression expression)
		{
			ResolvedQuerySourceRefExpression resolvedQuerySourceRefExpression = this.VisitExpression(expression.Expression) as ResolvedQuerySourceRefExpression;
			if (resolvedQuerySourceRefExpression == null)
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidPropertyVariationSourceExpression());
			}
			IConceptualEntity sourceEntity = resolvedQuerySourceRefExpression.SourceEntity;
			IConceptualProperty conceptualProperty;
			sourceEntity.TryGetProperty(expression.Property, out conceptualProperty);
			IConceptualColumn conceptualColumn = conceptualProperty as IConceptualColumn;
			if (conceptualColumn == null)
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveVariationSourcePropertyReference(expression.Property, sourceEntity.Name));
			}
			IConceptualVariationSource conceptualVariationSource;
			if (!conceptualColumn.TryGetVariationSource(expression.Name, out conceptualVariationSource))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveVariationSourceReference(expression.Name, expression.Property, sourceEntity.Name));
			}
			return resolvedQuerySourceRefExpression.VariationSource(conceptualVariationSource, conceptualColumn);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00021018 File Offset: 0x0001F218
		protected internal override ResolvedQueryExpression Visit(QueryNotExpression expression)
		{
			return this.VisitExpression(expression.Expression).Not();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0002102B File Offset: 0x0001F22B
		protected internal override ResolvedQueryExpression Visit(QueryAndExpression expression)
		{
			return this.VisitExpression(expression.Left).And(this.VisitExpression(expression.Right));
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0002104A File Offset: 0x0001F24A
		protected internal override ResolvedQueryExpression Visit(QueryOrExpression expression)
		{
			return this.VisitExpression(expression.Left).Or(this.VisitExpression(expression.Right));
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00021069 File Offset: 0x0001F269
		protected internal override ResolvedQueryExpression Visit(QueryAggregationExpression expression)
		{
			return this.VisitExpression(expression.Expression).Aggregate(expression.Function);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00021082 File Offset: 0x0001F282
		protected internal override ResolvedQueryExpression Visit(QueryArithmeticExpression expression)
		{
			return this.VisitExpression(expression.Left).Arithmetic(this.VisitExpression(expression.Right), expression.Operator);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000210A7 File Offset: 0x0001F2A7
		protected internal override ResolvedQueryExpression Visit(QueryBetweenExpression expression)
		{
			return this.VisitExpression(expression.Expression).Between(this.VisitExpression(expression.LowerBound), this.VisitExpression(expression.UpperBound));
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000210D4 File Offset: 0x0001F2D4
		protected internal override ResolvedQueryExpression Visit(QueryInExpression expression)
		{
			List<ResolvedQueryExpression> list = this.ResolveExpressionList(expression.Expressions);
			if (expression.HasValues)
			{
				List<IReadOnlyList<ResolvedQueryExpression>> list2 = new List<IReadOnlyList<ResolvedQueryExpression>>(expression.Values.Count);
				for (int i = 0; i < expression.Values.Count; i++)
				{
					list2.Add(this.ResolveExpressionList(expression.Values[i]));
				}
				return list.In(list2, expression.EqualityKind);
			}
			return list.In(this.VisitExpression(expression.Table));
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00021155 File Offset: 0x0001F355
		protected internal override ResolvedQueryExpression Visit(QueryScopedEvalExpression expression)
		{
			return this.VisitExpression(expression.Expression).ScopedEval(this.ResolveExpressionList(expression.Scope));
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00021174 File Offset: 0x0001F374
		protected internal override ResolvedQueryExpression Visit(QueryFilteredEvalExpression expression)
		{
			return this.VisitExpression(expression.Expression).FilteredEval(this.ResolveQueryFilterList(expression.Filters));
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00021193 File Offset: 0x0001F393
		protected internal override ResolvedQueryExpression Visit(QueryComparisonExpression expression)
		{
			return this.VisitExpression(expression.Left).Comparison(this.VisitExpression(expression.Right), expression.ComparisonKind);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000211B8 File Offset: 0x0001F3B8
		protected internal override ResolvedQueryExpression Visit(QueryContainsExpression expression)
		{
			return this.VisitExpression(expression.Left).Contains(this.VisitExpression(expression.Right));
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000211D7 File Offset: 0x0001F3D7
		protected internal override ResolvedQueryExpression Visit(QueryDateAddExpression expression)
		{
			return this.VisitExpression(expression.Expression).DateAdd(expression.Amount, expression.TimeUnit);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000211F6 File Offset: 0x0001F3F6
		protected internal override ResolvedQueryExpression Visit(QueryPercentileExpression expression)
		{
			return this.VisitExpression(expression.Expression).Percentile(expression.Exclusive, expression.K);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00021215 File Offset: 0x0001F415
		protected internal override ResolvedQueryExpression Visit(QueryMinExpression expression)
		{
			return this.VisitExpression(expression.Expression).Min(expression.IncludeAllTypes);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0002122E File Offset: 0x0001F42E
		protected internal override ResolvedQueryExpression Visit(QueryMaxExpression expression)
		{
			return this.VisitExpression(expression.Expression).Max(expression.IncludeAllTypes);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00021247 File Offset: 0x0001F447
		protected internal override ResolvedQueryExpression Visit(QueryDateSpanExpression expression)
		{
			return this.VisitExpression(expression.Expression).DateSpan(expression.TimeUnit);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00021260 File Offset: 0x0001F460
		protected internal override ResolvedQueryExpression Visit(QueryExistsExpression expression)
		{
			return this.VisitExpression(expression.Expression).Exists();
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00021273 File Offset: 0x0001F473
		protected internal override ResolvedQueryExpression Visit(QueryFloorExpression expression)
		{
			return this.VisitExpression(expression.Expression).Floor(expression.Size, expression.TimeUnit);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00021292 File Offset: 0x0001F492
		protected internal override ResolvedQueryExpression Visit(QueryDiscretizeExpression expression)
		{
			return this.VisitExpression(expression.Expression).Discretize(expression.Count);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000212AC File Offset: 0x0001F4AC
		protected internal override ResolvedQueryExpression Visit(QuerySparklineDataExpression expression)
		{
			return this.VisitExpression(expression.Measure).SparklineData(this.ResolveExpressionList(expression.Groupings), expression.PointsPerSparkline, (expression.ScalarKey == null) ? null : this.VisitExpression(expression.ScalarKey), expression.IncludeMinGroupingInterval);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000212FF File Offset: 0x0001F4FF
		protected internal override ResolvedQueryExpression Visit(QueryMemberExpression expression)
		{
			return this.VisitExpression(expression.Expression).Member(expression.Member);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00021318 File Offset: 0x0001F518
		protected internal override ResolvedQueryExpression Visit(QueryNativeFormatExpression expression)
		{
			return this.VisitExpression(expression.Expression).NativeFormat(expression.FormatString);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00021331 File Offset: 0x0001F531
		protected internal override ResolvedQueryExpression Visit(QueryNativeMeasureExpression expression)
		{
			return ResolvedQueryExpressionBuilder.NativeMeasure(expression.Language, expression.Expression);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00021344 File Offset: 0x0001F544
		protected internal override ResolvedQueryExpression Visit(QueryLiteralExpression expression)
		{
			PrimitiveValue primitiveValue;
			if (!PrimitiveValueEncoding.TryParseTypeEncodedString(expression.Value, out primitiveValue))
			{
				throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidPrimitiveValue());
			}
			return primitiveValue.Literal();
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00021372 File Offset: 0x0001F572
		protected internal override ResolvedQueryExpression Visit(QueryNowExpression expression)
		{
			return ResolvedQueryExpressionBuilder.Now();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00021379 File Offset: 0x0001F579
		protected internal override ResolvedQueryExpression Visit(QueryStartsWithExpression expression)
		{
			return this.VisitExpression(expression.Left).StartsWith(this.VisitExpression(expression.Right));
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00021398 File Offset: 0x0001F598
		protected internal override ResolvedQueryExpression Visit(QueryEndsWithExpression expression)
		{
			return this.VisitExpression(expression.Left).EndsWith(this.VisitExpression(expression.Right));
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000213B7 File Offset: 0x0001F5B7
		protected internal override ResolvedQueryExpression Visit(QueryDefaultValueExpression expression)
		{
			return ResolvedQueryExpressionBuilder.DefaultValue();
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000213BE File Offset: 0x0001F5BE
		protected internal override ResolvedQueryExpression Visit(QueryAnyValueExpression expression)
		{
			return ResolvedQueryExpressionBuilder.AnyValue(expression.DefaultValueOverridesAncestors);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000213CC File Offset: 0x0001F5CC
		protected internal override ResolvedQueryExpression Visit(QuerySubqueryExpression expression)
		{
			ResolvedQueryDefinition resolvedQueryDefinition;
			if (QueryDefinitionResolver.TryResolveQuery(expression.Query, this._sourceContext.FederatedSchema, this._errorContext, this._querySourceUsedNames, this._queryDefinitionNames, this._letMap, this._parameterMap, out resolvedQueryDefinition))
			{
				return resolvedQueryDefinition.Subquery();
			}
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveSubqueryDefinition());
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00021423 File Offset: 0x0001F623
		protected internal override ResolvedQueryExpression Visit(QueryTransformOutputRoleRefExpression expression)
		{
			return ResolvedQueryExpressionBuilder.TransformOutputRoleRef(expression.Role);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00021430 File Offset: 0x0001F630
		protected internal override ResolvedQueryExpression Visit(QueryTransformTableRefExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.UnexpectedTransformTableRef(expression.Source));
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00021444 File Offset: 0x0001F644
		protected internal override ResolvedQueryExpression Visit(QueryLetRefExpression expression)
		{
			ResolvedQueryLetBinding resolvedQueryLetBinding;
			if (this._letMap.TryGetValue(expression.Name, out resolvedQueryLetBinding) && resolvedQueryLetBinding != null)
			{
				return resolvedQueryLetBinding.LetRef();
			}
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveLetExpression(expression.Name));
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00021481 File Offset: 0x0001F681
		protected internal override ResolvedQueryExpression Visit(QueryRoleRefExpression expression)
		{
			return ResolvedQueryExpressionBuilder.RoleRef(expression.Role);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0002148E File Offset: 0x0001F68E
		protected internal override ResolvedQueryExpression Visit(QuerySummaryValueRefExpression expression)
		{
			return ResolvedQueryExpressionBuilder.SummaryValueRef(expression.Name);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0002149C File Offset: 0x0001F69C
		protected internal override ResolvedQueryExpression Visit(QueryParameterRefExpression expression)
		{
			ResolvedQueryParameterDeclaration resolvedQueryParameterDeclaration;
			if (this._parameterMap.TryGetValue(expression.Name, out resolvedQueryParameterDeclaration) && resolvedQueryParameterDeclaration != null)
			{
				return resolvedQueryParameterDeclaration.ParameterRef();
			}
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.CouldNotResolveParameterExpression(expression.Name));
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000214D9 File Offset: 0x0001F6D9
		protected internal override ResolvedQueryExpression Visit(QueryPrimitiveTypeExpression expression)
		{
			return expression.Type.PrimitiveType();
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000214E6 File Offset: 0x0001F6E6
		protected internal override ResolvedQueryExpression Visit(QueryTableTypeExpression expression)
		{
			return Util.RemapReadOnly<QueryExpressionContainer, ResolvedQueryTableTypeColumn>(expression.Columns, new Func<QueryExpressionContainer, ResolvedQueryTableTypeColumn>(this.ResolveTableTypeColumn)).TableType();
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00021504 File Offset: 0x0001F704
		private ResolvedQueryTableTypeColumn ResolveTableTypeColumn(QueryExpressionContainer column)
		{
			return this.VisitExpression(column.Expression).TableTypeColumn(column.Name);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00021522 File Offset: 0x0001F722
		protected internal override ResolvedQueryExpression Visit(QueryTypeOfExpression expression)
		{
			return this.VisitExpression(expression.Expression).TypeOf();
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00021535 File Offset: 0x0001F735
		protected internal override ResolvedQueryExpression Visit(QueryNativeVisualCalculationExpression expression)
		{
			return ResolvedQueryExpressionBuilder.NativeVisualCalculation(expression.Language, expression.Expression);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00021548 File Offset: 0x0001F748
		protected internal override ResolvedQueryExpression Visit(QueryBooleanConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("Boolean"));
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0002155A File Offset: 0x0001F75A
		protected internal override ResolvedQueryExpression Visit(QueryDecadeConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("Decade"));
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0002156C File Offset: 0x0001F76C
		protected internal override ResolvedQueryExpression Visit(QueryDateConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("Date"));
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0002157E File Offset: 0x0001F77E
		protected internal override ResolvedQueryExpression Visit(QueryDatePartExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("DatePart"));
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00021590 File Offset: 0x0001F790
		protected internal override ResolvedQueryExpression Visit(QueryDateTimeConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("DateTime"));
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x000215A2 File Offset: 0x0001F7A2
		protected internal override ResolvedQueryExpression Visit(QueryDateTimeSecondConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("DateTimeSecond"));
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000215B4 File Offset: 0x0001F7B4
		protected internal override ResolvedQueryExpression Visit(QueryDecimalConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("Decimal"));
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000215C6 File Offset: 0x0001F7C6
		protected internal override ResolvedQueryExpression Visit(QueryIntegerConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("Integer"));
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000215D8 File Offset: 0x0001F7D8
		protected internal override ResolvedQueryExpression Visit(QueryNullConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("Null"));
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x000215EA File Offset: 0x0001F7EA
		protected internal override ResolvedQueryExpression Visit(QueryNumberConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("Number"));
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000215FC File Offset: 0x0001F7FC
		protected internal override ResolvedQueryExpression Visit(QueryPropertyExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("Property"));
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0002160E File Offset: 0x0001F80E
		protected internal override ResolvedQueryExpression Visit(QueryStringConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("String"));
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00021620 File Offset: 0x0001F820
		protected internal override ResolvedQueryExpression Visit(QueryYearConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("Year"));
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00021632 File Offset: 0x0001F832
		protected internal override ResolvedQueryExpression Visit(QueryYearAndMonthConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("YearAndMonth"));
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00021644 File Offset: 0x0001F844
		protected internal override ResolvedQueryExpression Visit(QueryYearAndWeekConstantExpression expression)
		{
			throw this.FailQueryExpressionResolution(QueryResolutionMessages.InvalidExpressionType("YearAndWeek"));
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00021656 File Offset: 0x0001F856
		private ResolvedQueryExpression VisitExpression(QueryExpressionContainer container)
		{
			return container.Expression.Accept<ResolvedQueryExpression>(this);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00021664 File Offset: 0x0001F864
		private List<ResolvedQueryExpression> ResolveExpressionList(List<QueryExpressionContainer> expressions)
		{
			List<ResolvedQueryExpression> list = new List<ResolvedQueryExpression>(expressions.Count);
			for (int i = 0; i < expressions.Count; i++)
			{
				list.Add(this.VisitExpression(expressions[i]));
			}
			return list;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000216A4 File Offset: 0x0001F8A4
		private List<ResolvedQueryFilter> ResolveQueryFilterList(List<QueryFilter> filters)
		{
			List<ResolvedQueryFilter> list = new List<ResolvedQueryFilter>(filters.Count);
			for (int i = 0; i < filters.Count; i++)
			{
				list.Add(this.ResolveQueryFilter(filters[i]));
			}
			return list;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000216E4 File Offset: 0x0001F8E4
		private ResolvedQueryFilter ResolveQueryFilter(QueryFilter queryFilter)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.VisitExpression(queryFilter.Condition.Expression);
			List<ResolvedQueryExpression> list = ((queryFilter.Target != null) ? this.ResolveExpressionList(queryFilter.Target) : null);
			return resolvedQueryExpression.Filter(list, queryFilter.Annotations);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0002172B File Offset: 0x0001F92B
		private QueryResolutionException FailQueryExpressionResolution(QueryResolutionMessage queryResolutionError)
		{
			this._errorContext.RegisterError(queryResolutionError);
			throw new QueryResolutionException();
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00021740 File Offset: 0x0001F940
		internal static bool TryGetSourceEntity(ResolvedQueryExpression expression, out IConceptualEntity entity)
		{
			ResolvedQuerySourceRefExpression resolvedQuerySourceRefExpression = expression as ResolvedQuerySourceRefExpression;
			if (resolvedQuerySourceRefExpression != null)
			{
				entity = resolvedQuerySourceRefExpression.SourceEntity;
				return true;
			}
			ResolvedQueryPropertyVariationSourceExpression resolvedQueryPropertyVariationSourceExpression = expression as ResolvedQueryPropertyVariationSourceExpression;
			if (resolvedQueryPropertyVariationSourceExpression != null)
			{
				entity = resolvedQueryPropertyVariationSourceExpression.SourceEntity;
				return true;
			}
			entity = null;
			return false;
		}

		// Token: 0x040007CA RID: 1994
		private readonly QuerySourceContext _sourceContext;

		// Token: 0x040007CB RID: 1995
		private readonly QueryTransformTableContext _transformTableContext;

		// Token: 0x040007CC RID: 1996
		private readonly QueryResolutionErrorContext _errorContext;

		// Token: 0x040007CD RID: 1997
		private readonly HashSet<string> _querySourceUsedNames;

		// Token: 0x040007CE RID: 1998
		private readonly IQueryDefinitionNameRegistrar _queryDefinitionNames;

		// Token: 0x040007CF RID: 1999
		private readonly ImmutableDictionary<string, ResolvedQueryLetBinding> _letMap;

		// Token: 0x040007D0 RID: 2000
		private readonly IReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> _parameterMap;
	}
}
