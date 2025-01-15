using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200015C RID: 348
	internal static class Extensions
	{
		// Token: 0x06001405 RID: 5125 RVA: 0x0003A0A8 File Offset: 0x000382A8
		internal static ConceptualRowType GetRowResultType(this QueryCommandTree queryTree)
		{
			return queryTree.Query.GetRowResultType();
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0003A0B8 File Offset: 0x000382B8
		internal static ConceptualRowType GetRowResultType(this QueryExpression query)
		{
			ConceptualResultType conceptualResultType = query.ConceptualResultType;
			ConceptualTableType conceptualTableType = conceptualResultType as ConceptualTableType;
			if (conceptualTableType != null)
			{
				conceptualResultType = conceptualTableType.RowType;
			}
			return conceptualResultType as ConceptualRowType;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0003A0E3 File Offset: 0x000382E3
		internal static bool Contains(this QueryExpression expression, QueryExpression subexpression)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(expression, "expression");
			ArgumentValidation.CheckNotNull<QueryExpression>(subexpression, "subexpression");
			return expression.Contains(new Predicate<QueryExpression>(subexpression.Equals));
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0003A110 File Offset: 0x00038310
		internal static bool Contains(this QueryExpression expression, Predicate<QueryExpression> predicate)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(expression, "expression");
			ArgumentValidation.CheckNotNull<Predicate<QueryExpression>>(predicate, "predicate");
			if (predicate(expression))
			{
				return true;
			}
			Extensions.ContainmentVisitor containmentVisitor = new Extensions.ContainmentVisitor(predicate);
			expression.Accept<QueryExpression>(containmentVisitor);
			return containmentVisitor.MatchFound;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0003A158 File Offset: 0x00038358
		internal static QueryExpression Replace(this QueryExpression expression, QueryExpression findSubexpression, QueryExpression replacementSubexpression, bool firstInstanceOnly = false)
		{
			if (QueryExpression.Comparer.Equals(expression, findSubexpression))
			{
				return replacementSubexpression;
			}
			Extensions.ReplacementVisitor replacementVisitor = new Extensions.ReplacementVisitor(new Dictionary<QueryExpression, QueryExpression>(QueryExpression.Comparer) { { findSubexpression, replacementSubexpression } }, firstInstanceOnly);
			return expression.Accept<QueryExpression>(replacementVisitor);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0003A198 File Offset: 0x00038398
		internal static QueryExpression ReplaceMultiple(this QueryExpression expression, IReadOnlyDictionary<QueryExpression, QueryExpression> subexpressionReplacements, bool firstInstanceOnly = false)
		{
			QueryExpression queryExpression;
			if (subexpressionReplacements.TryGetValue(expression, out queryExpression))
			{
				return queryExpression;
			}
			Extensions.ReplacementVisitor replacementVisitor = new Extensions.ReplacementVisitor(subexpressionReplacements, firstInstanceOnly);
			return expression.Accept<QueryExpression>(replacementVisitor);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0003A1C4 File Offset: 0x000383C4
		internal static string GetDefaultName(this QueryExpression expression)
		{
			Func<QueryFieldExpression, string> func;
			if ((func = Extensions.<>O.<0>__GetDefaultName) == null)
			{
				func = (Extensions.<>O.<0>__GetDefaultName = new Func<QueryFieldExpression, string>(Extensions.GetDefaultName));
			}
			return expression.GetInfoFromExpression(func, (QueryFunctionExpression funcExpr, QueryExpression argExpr) => funcExpr.Function.Name + argExpr.GetDefaultName(), delegate(QueryMeasureExpression measureExpr)
			{
				IConceptualMeasure targetMeasure = measureExpr.TargetMeasure;
				return ((targetMeasure != null) ? targetMeasure.EdmName : null) ?? measureExpr.Measure.Name;
			});
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0003A230 File Offset: 0x00038430
		private static string GetDefaultName(QueryFieldExpression fieldExpr)
		{
			return fieldExpr.Column.EdmName;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0003A240 File Offset: 0x00038440
		private static TResult GetInfoFromExpression<TResult>(this QueryExpression expression, Func<QueryFieldExpression, TResult> getInfoFromField, Func<QueryFunctionExpression, QueryExpression, TResult> getInfoFromSingleArgumentFunction, Func<QueryMeasureExpression, TResult> getInfoFromMeasure)
		{
			QueryExpression leafExpression = expression.GetLeafExpression();
			QueryFieldExpression queryFieldExpression = leafExpression as QueryFieldExpression;
			QueryFunctionExpression queryFunctionExpression = leafExpression as QueryFunctionExpression;
			QueryMeasureExpression queryMeasureExpression = leafExpression as QueryMeasureExpression;
			if (queryFieldExpression != null)
			{
				return getInfoFromField(queryFieldExpression);
			}
			if (queryFunctionExpression != null && queryFunctionExpression.Arguments.Count == 1)
			{
				return getInfoFromSingleArgumentFunction(queryFunctionExpression, queryFunctionExpression.Arguments[0]);
			}
			if (queryMeasureExpression != null)
			{
				return getInfoFromMeasure(queryMeasureExpression);
			}
			return default(TResult);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0003A2AC File Offset: 0x000384AC
		private static QueryExpression GetLeafExpression(this QueryExpression expression)
		{
			QueryCalculateExpression queryCalculateExpression = expression as QueryCalculateExpression;
			if (queryCalculateExpression != null)
			{
				return queryCalculateExpression.Argument.GetLeafExpression();
			}
			QueryFieldExpression queryFieldExpression = expression as QueryFieldExpression;
			if (queryFieldExpression != null)
			{
				return queryFieldExpression;
			}
			QueryFunctionExpression queryFunctionExpression = expression as QueryFunctionExpression;
			if (queryFunctionExpression != null && queryFunctionExpression.Arguments.Count == 1)
			{
				return queryFunctionExpression;
			}
			QueryMeasureExpression queryMeasureExpression = expression as QueryMeasureExpression;
			if (queryMeasureExpression != null)
			{
				return queryMeasureExpression;
			}
			QueryProjectExpression queryProjectExpression = expression as QueryProjectExpression;
			if (queryProjectExpression != null)
			{
				return queryProjectExpression.Projection.GetLeafExpression();
			}
			return null;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0003A31C File Offset: 0x0003851C
		internal static IEnumerable<IEdmFieldInstance> GetReferencedFields(this QueryExpression expression)
		{
			return from p in expression.GetReferencedEdmProperties()
				select p.ToIEdmFieldInstance() into p
				where p.IsValid
				select p;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0003A378 File Offset: 0x00038578
		internal static IEnumerable<IConceptualColumn> GetReferencedColumns(this QueryExpression expression)
		{
			return from p in expression.GetReferencedProperties()
				select p.AsColumn() into p
				where p != null
				select p;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0003A3D4 File Offset: 0x000385D4
		internal static IList<EdmPropertyInstance> GetReferencedEdmProperties(this QueryExpression expression)
		{
			Extensions.ReferencedPropertiesVisitor referencedPropertiesVisitor = new Extensions.ReferencedPropertiesVisitor();
			expression.Accept<QueryExpression>(referencedPropertiesVisitor);
			return referencedPropertiesVisitor.GetEdmProperties();
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0003A3F8 File Offset: 0x000385F8
		internal static IList<IConceptualProperty> GetReferencedProperties(this QueryExpression expression)
		{
			Extensions.ReferencedPropertiesVisitor referencedPropertiesVisitor = new Extensions.ReferencedPropertiesVisitor();
			expression.Accept<QueryExpression>(referencedPropertiesVisitor);
			return referencedPropertiesVisitor.GetProperties();
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0003A419 File Offset: 0x00038619
		internal static IEnumerable<ConceptualTypeColumn> GetGroupKeyColumns(this IEnumerable<IGroupItem> groupItems)
		{
			return groupItems.SelectMany((IGroupItem g) => from kvp in g.GetGroupKeys()
				select kvp.Value.ConceptualResultType.Column(kvp.Key, null));
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0003A440 File Offset: 0x00038640
		internal static IEnumerable<ConceptualTypeColumn> GetSubtotalIndicatorColumns(this IEnumerable<IGroupItem> groupItems)
		{
			return from NamedRollupGroupItem r in groupItems.OfType<RollupAddIsSubtotalGroupItem>().SelectMany((RollupAddIsSubtotalGroupItem g) => g.GroupItems)
				select ConceptualPrimitiveResultType.Boolean.Column(r.SubtotalIndicatorColumnName, null);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0003A4A0 File Offset: 0x000386A0
		internal static bool IsDaxLiteralBlank(this QueryExpression valueExpr)
		{
			if (valueExpr is QueryNullExpression)
			{
				return true;
			}
			QueryLiteralExpression queryLiteralExpression = valueExpr as QueryLiteralExpression;
			return queryLiteralExpression != null && queryLiteralExpression.Value.Value == null;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0003A4D8 File Offset: 0x000386D8
		internal static bool IsDaxLiteralBlankEquivalent(this QueryExpression valueExpr)
		{
			QueryLiteralExpression queryLiteralExpression = valueExpr as QueryLiteralExpression;
			if (queryLiteralExpression == null)
			{
				return false;
			}
			ScalarValue value = queryLiteralExpression.Value;
			if (value == null)
			{
				return false;
			}
			if (value.IsOfType<long>())
			{
				return value.CastValue<long>() == 0L;
			}
			if (value.IsOfType<double>())
			{
				return value.CastValue<double>() == 0.0;
			}
			if (value.IsOfType<decimal>())
			{
				return value.CastValue<decimal>() == 0m;
			}
			if (value.IsOfType<DateTime>())
			{
				return value.CastValue<DateTime>() == Literals.ZeroDateTimeValue;
			}
			if (value.IsOfType<string>())
			{
				return value.CastValue<string>().Length == 0;
			}
			return value.IsOfType<bool>() && !value.CastValue<bool>();
		}

		// Token: 0x020003B0 RID: 944
		private sealed class ContainmentVisitor : DefaultExpressionVisitor
		{
			// Token: 0x06002042 RID: 8258 RVA: 0x0005869F File Offset: 0x0005689F
			internal ContainmentVisitor(Predicate<QueryExpression> predicate)
			{
				this._predicate = predicate;
			}

			// Token: 0x1700082B RID: 2091
			// (get) Token: 0x06002043 RID: 8259 RVA: 0x000586AE File Offset: 0x000568AE
			internal bool MatchFound
			{
				get
				{
					return this._matchFound;
				}
			}

			// Token: 0x06002044 RID: 8260 RVA: 0x000586B6 File Offset: 0x000568B6
			protected override QueryExpression VisitExpression(QueryExpression expression)
			{
				if (this._matchFound)
				{
					return expression;
				}
				if (this._predicate(expression))
				{
					this._matchFound = true;
					return expression;
				}
				return base.VisitExpression(expression);
			}

			// Token: 0x04001365 RID: 4965
			private readonly Predicate<QueryExpression> _predicate;

			// Token: 0x04001366 RID: 4966
			private bool _matchFound;
		}

		// Token: 0x020003B1 RID: 945
		private sealed class ReferencedPropertiesVisitor : DefaultExpressionVisitor
		{
			// Token: 0x06002045 RID: 8261 RVA: 0x000586E0 File Offset: 0x000568E0
			internal IList<EdmPropertyInstance> GetEdmProperties()
			{
				return this._edmProperties.AsReadOnly();
			}

			// Token: 0x06002046 RID: 8262 RVA: 0x000586ED File Offset: 0x000568ED
			internal IList<IConceptualProperty> GetProperties()
			{
				return this._properties.AsReadOnly();
			}

			// Token: 0x06002047 RID: 8263 RVA: 0x000586FA File Offset: 0x000568FA
			private void Add(EdmPropertyInstance property)
			{
				if (!this._edmProperties.Contains(property))
				{
					this._edmProperties.Add(property);
				}
			}

			// Token: 0x06002048 RID: 8264 RVA: 0x00058716 File Offset: 0x00056916
			private void Add(IConceptualProperty property)
			{
				if (!this._properties.Contains(property))
				{
					this._properties.Add(property);
				}
			}

			// Token: 0x06002049 RID: 8265 RVA: 0x00058732 File Offset: 0x00056932
			protected internal override QueryExpression Visit(QueryExtensionExpression expression)
			{
				return expression;
			}

			// Token: 0x0600204A RID: 8266 RVA: 0x00058738 File Offset: 0x00056938
			protected internal override QueryExpression Visit(QueryFieldExpression expression)
			{
				ConceptualTypeColumn column = expression.Column;
				if (column != null)
				{
					QdmEntityPlaceholderExpression qdmEntityPlaceholderExpression = expression.Instance as QdmEntityPlaceholderExpression;
					if (qdmEntityPlaceholderExpression != null)
					{
						if (qdmEntityPlaceholderExpression.Target != null)
						{
							this.Add(qdmEntityPlaceholderExpression.Target.FieldInstance(column.EdmName));
						}
						if (qdmEntityPlaceholderExpression.TargetEntity != null)
						{
							this.Add(qdmEntityPlaceholderExpression.TargetEntity.GetPropertyByEdmName(column.EdmName));
						}
					}
				}
				return base.Visit(expression);
			}

			// Token: 0x0600204B RID: 8267 RVA: 0x000587A8 File Offset: 0x000569A8
			protected internal override QueryExpression Visit(QueryAllExpression expression)
			{
				if (!expression.Fields.IsNullOrEmpty<EdmField>())
				{
					for (int i = 0; i < expression.Fields.Count; i++)
					{
						this.Add(expression.Target.FieldInstance(expression.Fields[i]));
					}
				}
				if (!expression.Columns.IsNullOrEmpty<IConceptualColumn>())
				{
					foreach (IConceptualColumn conceptualColumn in expression.Columns)
					{
						this.Add(conceptualColumn);
					}
				}
				return base.Visit(expression);
			}

			// Token: 0x0600204C RID: 8268 RVA: 0x00058850 File Offset: 0x00056A50
			protected internal override QueryExpression Visit(QueryMeasureExpression expression)
			{
				if (expression.Target != null)
				{
					this.Add(expression.Target.PropertyInstance(expression.Measure));
				}
				if (expression.TargetMeasure != null)
				{
					this.Add(expression.TargetMeasure);
				}
				return base.Visit(expression);
			}

			// Token: 0x0600204D RID: 8269 RVA: 0x0005888C File Offset: 0x00056A8C
			protected internal override QueryExpression Visit(QueryProjectExpression expression)
			{
				QueryScanExpression queryScanExpression = expression.Input.Expression as QueryScanExpression;
				QueryFieldExpression queryFieldExpression = expression.Projection as QueryFieldExpression;
				if (queryScanExpression != null && queryFieldExpression != null && queryFieldExpression.Instance.Equals(expression.Input.Variable))
				{
					if (queryScanExpression.Target != null)
					{
						EdmFieldInstance edmFieldInstance = queryScanExpression.Target.FieldInstance(queryFieldExpression.Column.EdmName);
						this.Add(edmFieldInstance);
					}
					if (queryScanExpression.TargetEntity != null)
					{
						this.Add(queryScanExpression.TargetEntity.GetPropertyByEdmName(queryFieldExpression.Column.EdmName));
					}
				}
				return base.Visit(expression);
			}

			// Token: 0x04001367 RID: 4967
			private readonly List<EdmPropertyInstance> _edmProperties = new List<EdmPropertyInstance>();

			// Token: 0x04001368 RID: 4968
			private readonly List<IConceptualProperty> _properties = new List<IConceptualProperty>();
		}

		// Token: 0x020003B2 RID: 946
		private sealed class ReplacementVisitor : DefaultExpressionVisitor
		{
			// Token: 0x0600204F RID: 8271 RVA: 0x00058948 File Offset: 0x00056B48
			internal ReplacementVisitor(IReadOnlyDictionary<QueryExpression, QueryExpression> subexpressionReplacements, bool firstInstanceOnly)
			{
				this._subexpressionReplacements = subexpressionReplacements;
				this._firstInstanceOnly = firstInstanceOnly;
			}

			// Token: 0x06002050 RID: 8272 RVA: 0x00058960 File Offset: 0x00056B60
			protected override QueryExpression VisitExpression(QueryExpression expression)
			{
				QueryExpression queryExpression;
				if ((!this._firstInstanceOnly || !this._replacementMade) && this._subexpressionReplacements.TryGetValue(expression, out queryExpression))
				{
					this._replacementMade = true;
					return queryExpression;
				}
				return base.VisitExpression(expression);
			}

			// Token: 0x04001369 RID: 4969
			private readonly IReadOnlyDictionary<QueryExpression, QueryExpression> _subexpressionReplacements;

			// Token: 0x0400136A RID: 4970
			private readonly bool _firstInstanceOnly;

			// Token: 0x0400136B RID: 4971
			private bool _replacementMade;
		}

		// Token: 0x020003B3 RID: 947
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400136C RID: 4972
			public static Func<QueryFieldExpression, string> <0>__GetDefaultName;
		}
	}
}
