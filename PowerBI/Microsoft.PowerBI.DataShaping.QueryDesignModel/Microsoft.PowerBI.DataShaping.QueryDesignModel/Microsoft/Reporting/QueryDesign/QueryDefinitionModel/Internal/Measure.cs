using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000F2 RID: 242
	internal abstract class Measure : INamedProjection, INamedItem
	{
		// Token: 0x06000E14 RID: 3604 RVA: 0x00023C10 File Offset: 0x00021E10
		protected Measure(QueryExpression expression, string name)
		{
			this._expression = ArgumentValidation.CheckNotNull<QueryExpression>(expression, "expression");
			this._name = ArgumentValidation.CheckNotNullOrEmpty(name, "name");
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00023C45 File Offset: 0x00021E45
		public QueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00023C4D File Offset: 0x00021E4D
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00023C55 File Offset: 0x00021E55
		public static Measure Create(QueryExpression expression, string name, bool suppressJoinPredicate = false)
		{
			if (suppressJoinPredicate)
			{
				return new NonPredicateMeasure(expression, name);
			}
			return new PredicateMeasure(expression, name);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00023C69 File Offset: 0x00021E69
		internal IEnumerable<EntitySet> FindAnchoredEntityReferences()
		{
			return Measure.FindAnchoredEntitySetReferences(this.Expression);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00023C76 File Offset: 0x00021E76
		internal static IEnumerable<EntitySet> FindAnchoredEntitySetReferences(QueryExpression measureExpression)
		{
			return measureExpression.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.AnchoredOnly);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00023C7F File Offset: 0x00021E7F
		internal static IEnumerable<IConceptualEntity> FindAnchoredEntityReferences(QueryExpression measureExpression)
		{
			return measureExpression.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.AnchoredOnly);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00023C88 File Offset: 0x00021E88
		public static bool IsMeasureExpressionAnchored(QueryExpression measureExpression)
		{
			if (Measure.FindAnchoredEntityReferences(measureExpression).Any<IConceptualEntity>() || Measure.FindAnchoredEntitySetReferences(measureExpression).Any<EntitySet>())
			{
				return !measureExpression.Contains((QueryExpression e) => e is QueryMeasureExpression);
			}
			return false;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00023CD9 File Offset: 0x00021ED9
		internal static QueryExpression CreateJoinPredicateExpressionForMeasureExpression(QueryExpression measureExpr)
		{
			return measureExpr.IsNull().Not();
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00023CE8 File Offset: 0x00021EE8
		internal static bool IsMeasureCrossFilteredByGroupsInQuery(QueryExpression measureExpression, BaseEntitySets crossFilteredEntities)
		{
			List<EntitySet> list = Measure.FindAnchoredEntitySetReferences(measureExpression).Distinct<EntitySet>().ToList<EntitySet>();
			return list.Intersect(crossFilteredEntities.CompleteSet).Count<EntitySet>() == list.Count;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00023D20 File Offset: 0x00021F20
		internal static bool IsMeasureCrossFilteredByGroupsInQuery(QueryExpression measureExpression, BaseConceptualEntities crossFilteredEntities)
		{
			List<IConceptualEntity> list = Measure.FindAnchoredEntityReferences(measureExpression).Distinct(ConceptualEntityExtensionAwareEqualityComparer.Instance).ToList<IConceptualEntity>();
			return list.Intersect(crossFilteredEntities.CompleteSet, ConceptualEntityExtensionAwareEqualityComparer.Instance).Count<IConceptualEntity>() == list.Count;
		}

		// Token: 0x040009BA RID: 2490
		private readonly QueryExpression _expression;

		// Token: 0x040009BB RID: 2491
		private readonly string _name = string.Empty;
	}
}
