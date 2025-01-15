using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Resolution
{
	// Token: 0x020000F3 RID: 243
	internal sealed class QueryExtensionReferenceReplacer : SemanticQueryDataShapeCommandVisitor
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x000209CC File Offset: 0x0001EBCC
		private QueryExtensionReferenceReplacer(SemanticQueryDataShapeCommand command, QuerySchemaMapping schemaMapping)
		{
			this._command = command;
			this._schemaMapping = schemaMapping;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000209E2 File Offset: 0x0001EBE2
		internal static void Rewrite(SemanticQueryDataShapeCommand command, QuerySchemaMapping schemaMapping)
		{
			new QueryExtensionReferenceReplacer(command, schemaMapping).Visit(command);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000209F4 File Offset: 0x0001EBF4
		protected override void Visit(QueryDefinition definition)
		{
			QueryExtensionReferenceReplacer.ExtensionReferenceContext extensionReferenceContext = new QueryExtensionReferenceReplacer.ExtensionReferenceContext(this._schemaMapping, null);
			QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer queryExpressionExtensionReferenceReplacer = new QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer(extensionReferenceContext);
			QueryExtensionReferenceReplacer.EntitySourceExtensionReferenceReplacer entitySourceExtensionReferenceReplacer = new QueryExtensionReferenceReplacer.EntitySourceExtensionReferenceReplacer(extensionReferenceContext, queryExpressionExtensionReferenceReplacer);
			QueryDefinition queryDefinition = QueryDefinitionRewriter.Rewrite(definition, queryExpressionExtensionReferenceReplacer, new Func<EntitySource, EntitySource>(entitySourceExtensionReferenceReplacer.RewriteSource));
			if (queryDefinition != definition)
			{
				this._command.Query = queryDefinition;
			}
			base.Visit(definition);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00020A48 File Offset: 0x0001EC48
		protected override void Visit(DataShapeBinding binding)
		{
			DataShapeBindingAxis dataShapeBindingAxis = this.RewriteDataShapeBindingAxis(binding.Primary);
			DataShapeBindingAxis dataShapeBindingAxis2 = null;
			if (binding.Secondary != null)
			{
				dataShapeBindingAxis2 = this.RewriteDataShapeBindingAxis(binding.Secondary);
			}
			IList<FilterDefinition> list = null;
			if (!binding.Highlights.IsNullOrEmpty<FilterDefinition>())
			{
				list = binding.Highlights.Rewrite(new Func<FilterDefinition, FilterDefinition>(this.RewriteFilterDefinition));
			}
			if (dataShapeBindingAxis != binding.Primary || dataShapeBindingAxis2 != binding.Secondary || list != binding.Highlights)
			{
				binding.Primary = dataShapeBindingAxis;
				binding.Secondary = dataShapeBindingAxis2;
				binding.Highlights = list;
			}
			base.Visit(binding);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00020AE0 File Offset: 0x0001ECE0
		private DataShapeBindingAxis RewriteDataShapeBindingAxis(DataShapeBindingAxis bindingAxis)
		{
			IList<DataShapeBindingAxisGrouping> list = bindingAxis.Groupings.Rewrite(new Func<DataShapeBindingAxisGrouping, DataShapeBindingAxisGrouping>(this.RewriteAxisGroupings));
			DataShapeBindingAxisExpansionState dataShapeBindingAxisExpansionState = null;
			if (bindingAxis.Expansion != null)
			{
				dataShapeBindingAxisExpansionState = this.RewriteExpansionState(bindingAxis.Expansion);
			}
			if (list == bindingAxis.Groupings && dataShapeBindingAxisExpansionState == bindingAxis.Expansion)
			{
				return bindingAxis;
			}
			return new DataShapeBindingAxis
			{
				Groupings = list,
				Expansion = (dataShapeBindingAxisExpansionState ?? bindingAxis.Expansion)
			};
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00020B54 File Offset: 0x0001ED54
		private DataShapeBindingAxisGrouping RewriteAxisGroupings(DataShapeBindingAxisGrouping axisGroupings)
		{
			if (axisGroupings.InstanceFilters.IsNullOrEmpty<FilterDefinition>())
			{
				return axisGroupings;
			}
			IList<FilterDefinition> list = axisGroupings.InstanceFilters.Rewrite(new Func<FilterDefinition, FilterDefinition>(this.RewriteFilterDefinition));
			if (list == axisGroupings.InstanceFilters)
			{
				return axisGroupings;
			}
			return new DataShapeBindingAxisGrouping
			{
				Projections = axisGroupings.Projections,
				GroupBy = axisGroupings.GroupBy,
				SuppressedProjections = axisGroupings.SuppressedProjections,
				ShowItemsWithNoData = axisGroupings.ShowItemsWithNoData,
				Subtotal = axisGroupings.Subtotal,
				InstanceFilters = list
			};
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00020BDC File Offset: 0x0001EDDC
		private DataShapeBindingAxisExpansionState RewriteExpansionState(DataShapeBindingAxisExpansionState expansionState)
		{
			QueryExtensionReferenceReplacer.ExtensionReferenceContext extensionReferenceContext = new QueryExtensionReferenceReplacer.ExtensionReferenceContext(this._schemaMapping, null);
			QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer queryExpressionExtensionReferenceReplacer = new QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer(extensionReferenceContext);
			List<EntitySource> list = null;
			if (!expansionState.From.IsNullOrEmpty<EntitySource>())
			{
				QueryExtensionReferenceReplacer.EntitySourceExtensionReferenceReplacer entitySourceExtensionReferenceReplacer = new QueryExtensionReferenceReplacer.EntitySourceExtensionReferenceReplacer(extensionReferenceContext, queryExpressionExtensionReferenceReplacer);
				list = expansionState.From.Rewrite(new Func<EntitySource, EntitySource>(entitySourceExtensionReferenceReplacer.RewriteSource));
			}
			List<DataShapeBindingAxisExpansionLevel> list2 = new List<DataShapeBindingAxisExpansionLevel>(expansionState.Levels.Count);
			foreach (DataShapeBindingAxisExpansionLevel dataShapeBindingAxisExpansionLevel in expansionState.Levels)
			{
				List<QueryExpressionContainer> list3 = dataShapeBindingAxisExpansionLevel.Expressions.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(queryExpressionExtensionReferenceReplacer.RewriteContainer));
				dataShapeBindingAxisExpansionLevel.Expressions = list3;
				list2.Add(new DataShapeBindingAxisExpansionLevel
				{
					Default = dataShapeBindingAxisExpansionLevel.Default,
					Expressions = list3
				});
			}
			return new DataShapeBindingAxisExpansionState
			{
				From = list,
				Instances = expansionState.Instances,
				Levels = list2
			};
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00020CE0 File Offset: 0x0001EEE0
		private FilterDefinition RewriteFilterDefinition(FilterDefinition definition)
		{
			QueryExtensionReferenceReplacer.ExtensionReferenceContext extensionReferenceContext = new QueryExtensionReferenceReplacer.ExtensionReferenceContext(this._schemaMapping, null);
			QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer queryExpressionExtensionReferenceReplacer = new QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer(extensionReferenceContext);
			QueryExtensionReferenceReplacer.EntitySourceExtensionReferenceReplacer entitySourceExtensionReferenceReplacer = new QueryExtensionReferenceReplacer.EntitySourceExtensionReferenceReplacer(extensionReferenceContext, queryExpressionExtensionReferenceReplacer);
			return FilterDefinitionRewriter.Rewrite(definition, queryExpressionExtensionReferenceReplacer, new Func<EntitySource, EntitySource>(entitySourceExtensionReferenceReplacer.RewriteSource));
		}

		// Token: 0x04000435 RID: 1077
		private readonly QuerySchemaMapping _schemaMapping;

		// Token: 0x04000436 RID: 1078
		private SemanticQueryDataShapeCommand _command;

		// Token: 0x02000163 RID: 355
		private sealed class EntitySourceExtensionReferenceReplacer
		{
			// Token: 0x060009FA RID: 2554 RVA: 0x000261A2 File Offset: 0x000243A2
			internal EntitySourceExtensionReferenceReplacer(QueryExtensionReferenceReplacer.ExtensionReferenceContext context, QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer exprRewriter)
			{
				this._context = context;
				this._exprRewriter = exprRewriter;
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x000261B8 File Offset: 0x000243B8
			internal EntitySource RewriteSource(EntitySource source)
			{
				EntitySourceType type = source.Type;
				if (type == EntitySourceType.Table)
				{
					return this.RewriteTableSource(source);
				}
				if (type != EntitySourceType.Expression)
				{
					return source;
				}
				return this.RewriteExpressionSource(source);
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x000261E8 File Offset: 0x000243E8
			private EntitySource RewriteTableSource(EntitySource source)
			{
				string text;
				if (this._context.ResolveEntitySource(source.Name, source.Schema, source.Entity, out text))
				{
					return SemanticQueryDefinitionBuilder<SemanticQueryDataShapeCommandBuilder>.BuildEntitySource(source.Name, text, source.Schema);
				}
				return source;
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x0002622C File Offset: 0x0002442C
			private EntitySource RewriteExpressionSource(EntitySource source)
			{
				QueryExpressionContainer queryExpressionContainer = this._exprRewriter.RewriteContainer(source.Expression);
				if (source.Expression != queryExpressionContainer)
				{
					return SemanticQueryDefinitionBuilder<SemanticQueryDataShapeCommandBuilder>.BuildEntitySource(source.Name, queryExpressionContainer);
				}
				return source;
			}

			// Token: 0x04000570 RID: 1392
			private readonly QueryExtensionReferenceReplacer.ExtensionReferenceContext _context;

			// Token: 0x04000571 RID: 1393
			private readonly QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer _exprRewriter;
		}

		// Token: 0x02000164 RID: 356
		private sealed class ExtensionReferenceContext
		{
			// Token: 0x060009FE RID: 2558 RVA: 0x00026262 File Offset: 0x00024462
			internal ExtensionReferenceContext(QuerySchemaMapping schemaMapping, QueryExtensionReferenceReplacer.ExtensionReferenceContext parentContext = null)
			{
				this._schemaMapping = schemaMapping;
				this._parentContext = parentContext;
				this._sourceToOriginalEntity = new Dictionary<string, string>(QueryNameComparer.Instance);
			}

			// Token: 0x060009FF RID: 2559 RVA: 0x00026288 File Offset: 0x00024488
			internal QueryExtensionReferenceReplacer.ExtensionReferenceContext ForkForSubquery()
			{
				return new QueryExtensionReferenceReplacer.ExtensionReferenceContext(this._schemaMapping, this);
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x00026296 File Offset: 0x00024496
			private bool TryGetOriginalEntityForSource(string source, out string originalEntityName)
			{
				if (!this._sourceToOriginalEntity.TryGetValue(source, out originalEntityName))
				{
					QueryExtensionReferenceReplacer.ExtensionReferenceContext parentContext = this._parentContext;
					return parentContext != null && parentContext.TryGetOriginalEntityForSource(source, out originalEntityName);
				}
				return true;
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x000262BC File Offset: 0x000244BC
			internal bool ResolveEntitySource(string source, string schema, string entity, out string resolvedEntity)
			{
				ExtensionEntityMapping extensionEntityMapping;
				if (ConceptualNameComparer.Instance.Equals(this._schemaMapping.SchemaName, schema) && this._schemaMapping.EntitiesByName.TryGetValue(entity, out extensionEntityMapping))
				{
					if (!source.IsNullOrEmpty<char>())
					{
						this._sourceToOriginalEntity[source] = entity;
					}
					resolvedEntity = extensionEntityMapping.ResolvedName;
					return true;
				}
				resolvedEntity = null;
				return false;
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x0002631C File Offset: 0x0002451C
			internal bool ResolveMeasure(string source, string measureName, out string resolvedMeasureName)
			{
				string text;
				ExtensionEntityMapping extensionEntityMapping;
				ExtensionPropertyMapping extensionPropertyMapping;
				if (this.TryGetOriginalEntityForSource(source, out text) && this._schemaMapping.EntitiesByName.TryGetValue(text, out extensionEntityMapping) && extensionEntityMapping.TryGetMeasure(measureName, out extensionPropertyMapping))
				{
					resolvedMeasureName = extensionPropertyMapping.ResolvedName;
					return true;
				}
				resolvedMeasureName = null;
				return false;
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x00026364 File Offset: 0x00024564
			internal bool ResolveMeasure(string schemaName, string entityName, string columnName, out string resolvedColumnName)
			{
				ExtensionEntityMapping extensionEntityMapping;
				ExtensionPropertyMapping extensionPropertyMapping;
				if (ConceptualNameComparer.Instance.Equals(this._schemaMapping.SchemaName, schemaName) && this._schemaMapping.EntitiesByName.TryGetValue(entityName, out extensionEntityMapping) && extensionEntityMapping.TryGetMeasure(columnName, out extensionPropertyMapping))
				{
					resolvedColumnName = extensionPropertyMapping.ResolvedName;
					return true;
				}
				resolvedColumnName = null;
				return false;
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x000263BC File Offset: 0x000245BC
			internal bool ResolveColumn(string source, string columnName, out string resolvedColumnName)
			{
				string text;
				ExtensionEntityMapping extensionEntityMapping;
				ExtensionPropertyMapping extensionPropertyMapping;
				if (this.TryGetOriginalEntityForSource(source, out text) && this._schemaMapping.EntitiesByName.TryGetValue(text, out extensionEntityMapping) && extensionEntityMapping.TryGetColumn(columnName, out extensionPropertyMapping))
				{
					resolvedColumnName = extensionPropertyMapping.ResolvedName;
					return true;
				}
				resolvedColumnName = null;
				return false;
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x00026404 File Offset: 0x00024604
			internal bool ResolveColumn(string schemaName, string entityName, string columnName, out string resolvedColumnName)
			{
				ExtensionEntityMapping extensionEntityMapping;
				ExtensionPropertyMapping extensionPropertyMapping;
				if (ConceptualNameComparer.Instance.Equals(this._schemaMapping.SchemaName, schemaName) && this._schemaMapping.EntitiesByName.TryGetValue(entityName, out extensionEntityMapping) && extensionEntityMapping.TryGetColumn(columnName, out extensionPropertyMapping))
				{
					resolvedColumnName = extensionPropertyMapping.ResolvedName;
					return true;
				}
				resolvedColumnName = null;
				return false;
			}

			// Token: 0x04000572 RID: 1394
			private readonly QuerySchemaMapping _schemaMapping;

			// Token: 0x04000573 RID: 1395
			private readonly QueryExtensionReferenceReplacer.ExtensionReferenceContext _parentContext;

			// Token: 0x04000574 RID: 1396
			private readonly Dictionary<string, string> _sourceToOriginalEntity;
		}

		// Token: 0x02000165 RID: 357
		private sealed class QueryExpressionExtensionReferenceReplacer : QueryExpressionRewriter
		{
			// Token: 0x06000A06 RID: 2566 RVA: 0x00026459 File Offset: 0x00024659
			internal QueryExpressionExtensionReferenceReplacer(QueryExtensionReferenceReplacer.ExtensionReferenceContext context)
			{
				this._context = context;
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x00026468 File Offset: 0x00024668
			protected internal override QueryExpression Visit(QuerySourceRefExpression expression)
			{
				string text;
				if (this._context.ResolveEntitySource(expression.Source, expression.Schema, expression.Entity, out text) && !ConceptualNameComparer.Instance.Equals(expression.Entity, text))
				{
					return expression.Schema.SourceRef(text);
				}
				return expression;
			}

			// Token: 0x06000A08 RID: 2568 RVA: 0x000264B8 File Offset: 0x000246B8
			protected internal override QueryExpression Visit(QuerySubqueryExpression expression)
			{
				QueryExtensionReferenceReplacer.ExtensionReferenceContext extensionReferenceContext = this._context.ForkForSubquery();
				QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer queryExpressionExtensionReferenceReplacer = new QueryExtensionReferenceReplacer.QueryExpressionExtensionReferenceReplacer(extensionReferenceContext);
				QueryExtensionReferenceReplacer.EntitySourceExtensionReferenceReplacer entitySourceExtensionReferenceReplacer = new QueryExtensionReferenceReplacer.EntitySourceExtensionReferenceReplacer(extensionReferenceContext, queryExpressionExtensionReferenceReplacer);
				QueryDefinition queryDefinition = QueryDefinitionRewriter.Rewrite(expression.Query, queryExpressionExtensionReferenceReplacer, new Func<EntitySource, EntitySource>(entitySourceExtensionReferenceReplacer.RewriteSource));
				if (expression.Query == queryDefinition)
				{
					return expression;
				}
				return queryDefinition.Subquery();
			}

			// Token: 0x06000A09 RID: 2569 RVA: 0x00026508 File Offset: 0x00024708
			protected internal override QueryExpression Visit(QueryMeasureExpression expression)
			{
				QuerySourceRefExpression sourceRef = expression.Expression.SourceRef;
				if (sourceRef != null)
				{
					string text2;
					if (sourceRef.Source != null)
					{
						string text;
						if (this._context.ResolveMeasure(sourceRef.Source, expression.Property, out text) && !ConceptualNameComparer.Instance.Equals(expression.Property, text))
						{
							return sourceRef.Measure(text);
						}
					}
					else if (this._context.ResolveMeasure(sourceRef.Schema, sourceRef.Entity, expression.Property, out text2) && !ConceptualNameComparer.Instance.Equals(expression.Property, text2))
					{
						return this.Visit(sourceRef).Measure(text2);
					}
				}
				return expression;
			}

			// Token: 0x06000A0A RID: 2570 RVA: 0x000265B0 File Offset: 0x000247B0
			protected internal override QueryExpression Visit(QueryColumnExpression expression)
			{
				QuerySourceRefExpression sourceRef = expression.Expression.SourceRef;
				if (sourceRef != null)
				{
					string text2;
					if (sourceRef.Source != null)
					{
						string text;
						if (this._context.ResolveColumn(sourceRef.Source, expression.Property, out text) && !ConceptualNameComparer.Instance.Equals(expression.Property, text))
						{
							return sourceRef.Column(text);
						}
					}
					else if (this._context.ResolveColumn(sourceRef.Schema, sourceRef.Entity, expression.Property, out text2) && !ConceptualNameComparer.Instance.Equals(expression.Property, text2))
					{
						return this.Visit(sourceRef).Column(text2);
					}
				}
				return expression;
			}

			// Token: 0x04000575 RID: 1397
			private readonly QueryExtensionReferenceReplacer.ExtensionReferenceContext _context;
		}
	}
}
