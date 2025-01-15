using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002EE RID: 750
	public class SemanticQueryDefinitionBuilder<TParent> : BaseBuilder<QueryDefinition, TParent>
	{
		// Token: 0x060018E2 RID: 6370 RVA: 0x0002CA8E File Offset: 0x0002AC8E
		public SemanticQueryDefinitionBuilder(TParent parent, int? version)
			: this(parent, null, version)
		{
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0002CA99 File Offset: 0x0002AC99
		public SemanticQueryDefinitionBuilder(TParent parent, Action<QueryDefinition> addToParent, int? version)
			: base(parent)
		{
			this._query = new QueryDefinition();
			this._query.Version = version;
			this._projectionNames = new HashSet<string>(QueryNameComparer.Instance);
			this._addToParent = addToParent;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0002CAD0 File Offset: 0x0002ACD0
		public SemanticQueryDefinitionBuilder<TParent> WithDatabaseName(string name)
		{
			this._query.DatabaseName = name;
			return this;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0002CADF File Offset: 0x0002ACDF
		public SemanticQueryDefinitionBuilder<TParent> WithParameter(string name, QueryExpression typeExpr)
		{
			return this.WithParameter(typeExpr.Container(name));
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0002CAEE File Offset: 0x0002ACEE
		public SemanticQueryDefinitionBuilder<TParent> WithParameter(QueryExpressionContainer parameter)
		{
			if (this._query.Parameters == null)
			{
				this._query.Parameters = new List<QueryExpressionContainer>(1);
			}
			this._query.Parameters.Add(parameter);
			return this;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0002CB20 File Offset: 0x0002AD20
		public SemanticQueryDefinitionBuilder<TParent> WithLet(QueryExpressionContainer letBinding)
		{
			if (this._query.Let == null)
			{
				this._query.Let = new List<QueryExpressionContainer>(1);
			}
			this._query.Let.Add(letBinding);
			return this;
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0002CB52 File Offset: 0x0002AD52
		public SemanticQueryDefinitionBuilder<TParent> WithFrom(EntitySource from)
		{
			this._query.From.Add(from);
			return this;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0002CB66 File Offset: 0x0002AD66
		public SemanticQueryDefinitionBuilder<TParent> WithFrom(string name, IConceptualEntity entity)
		{
			return this.WithFrom(name, entity.Name, ConceptualSchemaNames.NormalizeSchemaNameForSerialization(entity));
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0002CB7B File Offset: 0x0002AD7B
		public SemanticQueryDefinitionBuilder<TParent> WithFrom(string name, string entity, string schema = null)
		{
			return this.WithFrom(SemanticQueryDefinitionBuilder<TParent>.BuildEntitySource(name, entity, schema));
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0002CB8B File Offset: 0x0002AD8B
		public SemanticQueryDefinitionBuilder<TParent> WithFrom(string name, QueryExpressionContainer expression)
		{
			return this.WithFrom(new EntitySource
			{
				Name = name,
				Expression = expression,
				Type = EntitySourceType.Expression
			});
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0002CBAD File Offset: 0x0002ADAD
		public SemanticQueryDefinitionBuilder<TParent> WithWhere(List<QueryExpressionContainer> target, QueryExpressionContainer condition)
		{
			return this.WithWhere(new QueryFilter
			{
				Target = target,
				Condition = condition
			});
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0002CBC8 File Offset: 0x0002ADC8
		public SemanticQueryDefinitionBuilder<TParent> WithWhere(QueryExpressionContainer target, QueryExpressionContainer condition)
		{
			return this.WithWhere(new List<QueryExpressionContainer> { target }, condition);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0002CBDD File Offset: 0x0002ADDD
		public SemanticQueryDefinitionBuilder<TParent> WithWhere(QueryExpressionContainer condition)
		{
			return this.WithWhere(new QueryFilter
			{
				Condition = condition
			});
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0002CBF1 File Offset: 0x0002ADF1
		public SemanticQueryDefinitionBuilder<TParent> WithWhere(QueryExpressionContainer condition, FilterAnnotations annotations)
		{
			return this.WithWhere(new QueryFilter
			{
				Condition = condition,
				Annotations = annotations
			});
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0002CC0C File Offset: 0x0002AE0C
		public SemanticQueryDefinitionBuilder<TParent> WithWhere(QueryFilter filter)
		{
			this._query.Where.Add(filter);
			return this;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0002CC20 File Offset: 0x0002AE20
		public SemanticQueryDefinitionBuilder<TParent> WithOrderBy(QuerySortClause sortClause)
		{
			this._query.OrderBy.Add(sortClause);
			return this;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0002CC34 File Offset: 0x0002AE34
		public SemanticQueryDefinitionBuilder<TParent> WithOrderBy(QueryExpressionContainer sortExpression, QuerySortDirection sortDirection)
		{
			return this.WithOrderBy(new QuerySortClause
			{
				Expression = sortExpression,
				Direction = sortDirection
			});
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0002CC50 File Offset: 0x0002AE50
		public SemanticQueryDefinitionBuilder<TParent> WithSelect(IReadOnlyList<QueryExpressionContainer> expressions)
		{
			foreach (QueryExpressionContainer queryExpressionContainer in expressions)
			{
				this.WithSelect(queryExpressionContainer);
			}
			return this;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0002CC9C File Offset: 0x0002AE9C
		public SemanticQueryDefinitionBuilder<TParent> WithSelect(QueryExpressionContainer expression, string suggestedName = null, string nativeReferenceName = null)
		{
			string text;
			return this.WithSelect(expression, suggestedName, nativeReferenceName, out text);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0002CCB4 File Offset: 0x0002AEB4
		public SemanticQueryDefinitionBuilder<TParent> WithSelect(QueryExpressionContainer expression, string suggestedName, out string selectName)
		{
			return this.WithSelect(expression, suggestedName, null, out selectName);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0002CCC0 File Offset: 0x0002AEC0
		public SemanticQueryDefinitionBuilder<TParent> WithSelect(QueryExpressionContainer expression, string suggestedName, string nativeReferenceName, out string selectName)
		{
			if (suggestedName != null)
			{
				selectName = StringUtil.MakeUniqueName(suggestedName, this._projectionNames);
				this._projectionNames.Add(selectName);
			}
			else
			{
				selectName = null;
			}
			expression.Name = selectName;
			expression.NativeReferenceName = nativeReferenceName;
			return this.WithSelect(expression);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0002CD00 File Offset: 0x0002AF00
		public SemanticQueryDefinitionBuilder<TParent> WithSelect(QueryExpressionContainer expression)
		{
			this._query.Select.Add(expression);
			return this;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0002CD14 File Offset: 0x0002AF14
		public SemanticQueryDefinitionBuilder<TParent> WithGroupBy(QueryExpressionContainer expression, string suggestedName)
		{
			string text;
			return this.WithGroupBy(expression, suggestedName, out text);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0002CD2B File Offset: 0x0002AF2B
		public SemanticQueryDefinitionBuilder<TParent> WithGroupBy(QueryExpressionContainer expression, string suggestedName, out string groupByName)
		{
			groupByName = StringUtil.MakeUniqueName(suggestedName, this._projectionNames);
			this._projectionNames.Add(groupByName);
			expression.Name = groupByName;
			return this.WithGroupBy(expression);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0002CD58 File Offset: 0x0002AF58
		public SemanticQueryDefinitionBuilder<TParent> WithGroupBy(QueryExpressionContainer expression)
		{
			this._query.GroupBy.Add(expression);
			return this;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0002CD6C File Offset: 0x0002AF6C
		public QueryAxisBuilder<SemanticQueryDefinitionBuilder<TParent>> WithVisualShapeAxis(string name)
		{
			if (this._query.VisualShape == null)
			{
				this._query.VisualShape = new List<QueryAxis>();
			}
			QueryAxis queryAxis = new QueryAxis
			{
				Name = name,
				Groups = new List<QueryAxisGroup>()
			};
			this._query.VisualShape.Add(queryAxis);
			return new QueryAxisBuilder<SemanticQueryDefinitionBuilder<TParent>>(queryAxis, this);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0002CDC6 File Offset: 0x0002AFC6
		public SemanticQueryDefinitionBuilder<TParent> WithTransform(QueryTransform transform)
		{
			if (this._query.Transform == null)
			{
				this._query.Transform = new List<QueryTransform>(1);
			}
			this._query.Transform.Add(transform);
			return this;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0002CDF8 File Offset: 0x0002AFF8
		public QueryTransformBuilder<TParent> WithTransform(string name, string algorithm)
		{
			QueryTransform queryTransform = new QueryTransform
			{
				Name = name,
				Algorithm = algorithm
			};
			if (this._query.Transform == null)
			{
				this._query.Transform = new List<QueryTransform>();
			}
			this._query.Transform.Add(queryTransform);
			return new QueryTransformBuilder<TParent>(queryTransform, this);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0002CE4E File Offset: 0x0002B04E
		public SemanticQueryDefinitionBuilder<TParent> WithTransforms(List<QueryTransform> transforms)
		{
			if (this._query.Transform == null)
			{
				this._query.Transform = new List<QueryTransform>(transforms.Count);
			}
			this._query.Transform.AddRange(transforms);
			return this;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0002CE85 File Offset: 0x0002B085
		public SemanticQueryDefinitionBuilder<TParent> WithTop(int count, long? skipCount = null)
		{
			this._query.Top = new int?(count);
			if (skipCount != null)
			{
				this._query.Skip = skipCount;
			}
			return this;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0002CEAE File Offset: 0x0002B0AE
		public SemanticQueryDefinitionBuilder<SemanticQueryDefinitionBuilder<TParent>> CreateSubqueryBuilder(Action<QueryDefinition> addToParent = null)
		{
			return new SemanticQueryDefinitionBuilder<SemanticQueryDefinitionBuilder<TParent>>(this, addToParent, this._query.Version);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0002CEC4 File Offset: 0x0002B0C4
		public SemanticQueryDefinitionBuilder<SemanticQueryDefinitionBuilder<TParent>> WithSubqueryFrom(string name)
		{
			Action<QueryDefinition> action = delegate(QueryDefinition definition)
			{
				this.WithFrom(name, definition.Subquery());
			};
			return this.CreateSubqueryBuilder(action);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0002CEF8 File Offset: 0x0002B0F8
		public SemanticQueryDefinitionBuilder<SemanticQueryDefinitionBuilder<TParent>> WithSubqueryLet(string name)
		{
			Action<QueryDefinition> action = delegate(QueryDefinition definition)
			{
				this.WithLet(definition.Subquery().Container(name));
			};
			return this.CreateSubqueryBuilder(action);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0002CF2B File Offset: 0x0002B12B
		public override QueryDefinition Build()
		{
			return this._query;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0002CF33 File Offset: 0x0002B133
		public QueryDefinition ToQueryDefinition()
		{
			return this.Build();
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0002CF3B File Offset: 0x0002B13B
		public override TParent Parent
		{
			get
			{
				if (this._addToParent != null)
				{
					this._addToParent(this._query);
				}
				return base.Parent;
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0002CF5C File Offset: 0x0002B15C
		public static QueryFilter BuildQueryFilter(QueryExpressionContainer condition)
		{
			return new QueryFilter
			{
				Condition = condition
			};
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0002CF6A File Offset: 0x0002B16A
		public static QuerySortClause BuildSortClause(QueryExpressionContainer expression, QuerySortDirection direction)
		{
			return new QuerySortClause
			{
				Expression = expression,
				Direction = direction
			};
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0002CF7F File Offset: 0x0002B17F
		public static EntitySource BuildEntitySource(string name, string entity, string schema = null)
		{
			return new EntitySource
			{
				Name = name,
				Entity = entity,
				Schema = schema
			};
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0002CF9B File Offset: 0x0002B19B
		public static EntitySource BuildEntitySource(string name, QueryExpressionContainer expression)
		{
			return new EntitySource
			{
				Name = name,
				Type = EntitySourceType.Expression,
				Expression = expression
			};
		}

		// Token: 0x0400090A RID: 2314
		private readonly QueryDefinition _query;

		// Token: 0x0400090B RID: 2315
		private readonly HashSet<string> _projectionNames;

		// Token: 0x0400090C RID: 2316
		private readonly Action<QueryDefinition> _addToParent;
	}
}
