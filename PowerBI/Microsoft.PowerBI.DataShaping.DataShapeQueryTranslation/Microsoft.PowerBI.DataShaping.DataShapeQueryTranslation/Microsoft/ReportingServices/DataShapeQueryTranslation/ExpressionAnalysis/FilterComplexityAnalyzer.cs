using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B0 RID: 176
	internal sealed class FilterComplexityAnalyzer : FilterExpressionVisitor
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x0001DCB0 File Offset: 0x0001BEB0
		private FilterComplexityAnalyzer(IFederatedConceptualSchema schema, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool allowDisjunctionForIn)
			: base(null)
		{
			this.m_expressionTable = expressionTable;
			this.m_requiresJoinPredicateSubQuery = false;
			this.m_depthOfConditionsWithCorrelatedContext = 0;
			this.m_allowDisjunctionForIn = allowDisjunctionForIn;
			this.m_allowHierarchicalDisjunction = allowHierarchicalDisjunction;
			this.m_maxDepthOfDisjunctions = 0;
			this.m_entities = new HashSet<IConceptualEntity>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
			this.m_schema = schema;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001DD06 File Offset: 0x0001BF06
		public static bool TryGetFilterConditionsForJoinPredicateSubQuery(Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition, IFederatedConceptualSchema schema, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool allowDisjunctionForIn, out List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> filterConditions, out bool foundUnsplittableDisjunction)
		{
			filterConditions = null;
			if (!FilterComplexityAnalyzer.IsComplexFilter(filterCondition, expressionTable, allowHierarchicalDisjunction, allowDisjunctionForIn, schema))
			{
				foundUnsplittableDisjunction = false;
				return false;
			}
			filterConditions = FilterComplexityAnalyzer.SplitUpDisjunctions(filterCondition, expressionTable, allowHierarchicalDisjunction, allowDisjunctionForIn, schema, out foundUnsplittableDisjunction);
			return true;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001DD30 File Offset: 0x0001BF30
		public static List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> SplitUpDisjunctions(Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool allowDisjunctionForIn, IFederatedConceptualSchema schema, out bool foundUnsplittableDisjunction)
		{
			InFilterCondition inFilterCondition = filterCondition as InFilterCondition;
			if (inFilterCondition != null)
			{
				Microsoft.DataShaping.Contract.RetailAssert(!allowDisjunctionForIn, "Split up disjunctions should only be called for complex filters requiring predicate subqueries.");
				return FilterComplexityAnalyzer.SplitInFilterDisjunctions(inFilterCondition, expressionTable, allowHierarchicalDisjunction, false, schema, out foundUnsplittableDisjunction);
			}
			Microsoft.DataShaping.Contract.RetailAssert(filterCondition.ObjectType == ObjectType.CompoundFilterCondition, "filterCondition must be compound condition at this point.");
			return FilterComplexityAnalyzer.SplitUpCompoundDisjunctions((Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition)filterCondition, schema, expressionTable, allowHierarchicalDisjunction, allowDisjunctionForIn, out foundUnsplittableDisjunction);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001DD8C File Offset: 0x0001BF8C
		private static List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> SplitUpCompoundDisjunctions(Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition compound, IFederatedConceptualSchema schema, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool allowDisjunctionForIn, out bool foundUnsplittableDisjunction)
		{
			foundUnsplittableDisjunction = false;
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> conditions = compound.Conditions;
			if (compound.IsNegated)
			{
				Microsoft.DataShaping.Contract.RetailAssert(conditions.Single("DSQGen should only pass in negated compound filters to negate an individual filter condition.", Array.Empty<string>()) is InFilterCondition, "Compound filters with a single nested condition can only be complex if it's an IN filter.");
			}
			if (compound.IsNegated || (!allowDisjunctionForIn && (compound.Operator == Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.Any || compound.Operator == Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.NotAny) && conditions.OfType<InFilterCondition>().Any<InFilterCondition>()))
			{
				List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> list = new List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition>(conditions.Count);
				for (int i = 0; i < conditions.Count; i++)
				{
					Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition = conditions[i];
					InFilterCondition inFilterCondition = filterCondition as InFilterCondition;
					if (inFilterCondition == null)
					{
						list.Add(filterCondition);
					}
					else
					{
						bool flag;
						FilterComplexityAnalyzer.SplitInFilterDisjunctions(inFilterCondition, compound.IsNegated, list, expressionTable, allowHierarchicalDisjunction, allowDisjunctionForIn, schema, out flag);
						foundUnsplittableDisjunction = foundUnsplittableDisjunction || flag;
					}
				}
				return list;
			}
			return conditions;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001DE6C File Offset: 0x0001C06C
		internal static List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> SplitInFilterDisjunctions(InFilterCondition inFilter, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool isNegated, IFederatedConceptualSchema schema, out bool foundUnsplittableDisjunction)
		{
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> list = new List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition>();
			FilterComplexityAnalyzer.SplitInFilterDisjunctions(inFilter, isNegated, list, expressionTable, allowHierarchicalDisjunction, true, schema, out foundUnsplittableDisjunction);
			return list;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001DE90 File Offset: 0x0001C090
		private static void SplitInFilterDisjunctions(InFilterCondition inFilter, bool isNegated, List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> newConditions, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool assertComplexity, IFederatedConceptualSchema schema, out bool foundUnsplittableDisjunction)
		{
			if (!FilterComplexityAnalyzer.IsComplex(inFilter, expressionTable, allowHierarchicalDisjunction, isNegated, false, schema))
			{
				Microsoft.DataShaping.Contract.RetailAssert(!assertComplexity, "Expected a complex IN filter.");
				foundUnsplittableDisjunction = false;
				Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition = inFilter;
				if (isNegated)
				{
					filterCondition = new Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition
					{
						Operator = Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.NotAny,
						Conditions = inFilter.AsList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition>()
					};
				}
				newConditions.Add(filterCondition);
				return;
			}
			if (inFilter.HasTable)
			{
				foundUnsplittableDisjunction = true;
				return;
			}
			foundUnsplittableDisjunction = false;
			List<Expression> expressions = inFilter.Expressions;
			List<List<Expression>> values = inFilter.Values;
			newConditions.Capacity += values.Count;
			for (int i = 0; i < values.Count; i++)
			{
				newConditions.Add(FilterComplexityAnalyzer.GenerateSingleTupleCondition(expressions, values[i], isNegated, inFilter.IdentityComparison));
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001DF48 File Offset: 0x0001C148
		private static Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition GenerateSingleTupleCondition(List<Expression> expressions, List<Expression> inFilterTuple, bool isNegated, bool identityComparison)
		{
			if (!isNegated)
			{
				return new InFilterCondition
				{
					Expressions = expressions,
					Values = inFilterTuple.AsList<List<Expression>>(),
					IdentityComparison = identityComparison
				};
			}
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> list = new List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition>(inFilterTuple.Count);
			for (int i = 0; i < inFilterTuple.Count; i++)
			{
				BinaryFilterCondition binaryFilterCondition = new BinaryFilterCondition
				{
					Operator = (identityComparison ? BinaryFilterOperator.EqualIdentity : BinaryFilterOperator.Equal),
					LeftExpression = expressions[i],
					RightExpression = inFilterTuple[i],
					Not = true
				};
				list.Add(binaryFilterCondition);
			}
			return new Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition
			{
				Conditions = list,
				Operator = Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.Any
			};
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001DFF4 File Offset: 0x0001C1F4
		public static bool IsComplexFilter(Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool allowDisjunctionForIn, IFederatedConceptualSchema schema)
		{
			bool flag;
			return FilterComplexityAnalyzer.IsComplexFilter(filterCondition, expressionTable, allowHierarchicalDisjunction, allowDisjunctionForIn, out flag, schema);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001E010 File Offset: 0x0001C210
		public static bool IsComplexFilter(Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool allowDisjunctionForIn, out bool overMaxDepthLimit, IFederatedConceptualSchema schema)
		{
			bool flag;
			return FilterComplexityAnalyzer.IsComplexFilter(filterCondition, expressionTable, allowHierarchicalDisjunction, allowDisjunctionForIn, out overMaxDepthLimit, out flag, schema);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001E02C File Offset: 0x0001C22C
		public static bool IsComplexFilter(Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool allowDisjunctionForIn, out bool overMaxDepthLimit, out bool hasInFilter, IFederatedConceptualSchema schema)
		{
			if (filterCondition == null)
			{
				overMaxDepthLimit = false;
				hasInFilter = false;
				return false;
			}
			if (filterCondition.ObjectType != ObjectType.CompoundFilterCondition && filterCondition.ObjectType != ObjectType.InFilterCondition)
			{
				overMaxDepthLimit = false;
				hasInFilter = false;
				return false;
			}
			FilterComplexityAnalyzer filterComplexityAnalyzer = new FilterComplexityAnalyzer(schema, expressionTable, allowHierarchicalDisjunction, allowDisjunctionForIn);
			filterCondition.Accept<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition>(filterComplexityAnalyzer);
			overMaxDepthLimit = filterComplexityAnalyzer.m_maxDepthOfDisjunctions > 3;
			hasInFilter = filterComplexityAnalyzer.m_hasInFilter;
			return filterComplexityAnalyzer.m_requiresJoinPredicateSubQuery;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001E094 File Offset: 0x0001C294
		private static bool IsComplex(InFilterCondition inFilter, ExpressionTable expressionTable, bool allowHierarchicalDisjunction, bool isNegated, bool allowDisjunctionForIn, IFederatedConceptualSchema schema)
		{
			FilterComplexityAnalyzer filterComplexityAnalyzer = new FilterComplexityAnalyzer(schema, expressionTable, allowHierarchicalDisjunction, allowDisjunctionForIn);
			filterComplexityAnalyzer.m_isNegated = isNegated;
			inFilter.Accept<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition>(filterComplexityAnalyzer);
			return filterComplexityAnalyzer.m_requiresJoinPredicateSubQuery;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001E0C4 File Offset: 0x0001C2C4
		internal static bool ConditionsAreCorrelated(Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition condition, bool isNegated)
		{
			bool flag = condition.Operator == Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.Any || condition.Operator == Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.NotAll;
			return condition.Conditions.Count > 1 && (isNegated ^ flag);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001E10C File Offset: 0x0001C30C
		internal static bool IsDisjunction(InFilterCondition condition, bool isNegated)
		{
			return condition.Expressions.Count != 1 && (!condition.HasValues || condition.Values.Count > 1 || (isNegated && condition.Values.Count > 0));
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001E14C File Offset: 0x0001C34C
		internal override Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition Visit(InFilterCondition condition)
		{
			this.m_hasInFilter = true;
			if (this.m_allowDisjunctionForIn && this.m_depthOfConditionsWithCorrelatedContext == 0 && !this.m_isNegated)
			{
				return base.Visit(condition);
			}
			bool flag = FilterComplexityAnalyzer.IsDisjunction(condition, this.m_isNegated);
			this.VisitDisjoinableCondition<InFilterCondition>(condition, flag, delegate(InFilterCondition c)
			{
				base.Visit(c);
			});
			return condition;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
		internal override Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition condition)
		{
			bool flag = FilterComplexityAnalyzer.ConditionsAreCorrelated(condition, this.m_isNegated);
			this.VisitDisjoinableCondition<Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition>(condition, flag, delegate(Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition c)
			{
				base.Visit(c);
			});
			return condition;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001E1D4 File Offset: 0x0001C3D4
		internal override void VisitExpression(Expression expression, Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition owner, string propertyName)
		{
			if (this.m_depthOfConditionsWithCorrelatedContext == 0)
			{
				return;
			}
			ExpressionNode node = this.m_expressionTable.GetNode(expression);
			if (node.Kind != ExpressionNodeKind.ResolvedProperty)
			{
				return;
			}
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = (ResolvedPropertyExpressionNode)node;
			this.m_entities.Add(resolvedPropertyExpressionNode.Property.Entity.GetBaseModelEntity());
			if (this.m_entities.Count > 1 && (!this.m_allowHierarchicalDisjunction || !QueryAlgorithms.HasHierarchicalFilterPath(this.m_entities, this.m_schema.GetDefaultSchema())))
			{
				this.m_requiresJoinPredicateSubQuery = true;
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001E25C File Offset: 0x0001C45C
		private void VisitDisjoinableCondition<T>(T condition, bool tableJoinRequired, Action<T> visitAction) where T : Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition
		{
			if (tableJoinRequired)
			{
				this.m_depthOfConditionsWithCorrelatedContext++;
				if (this.m_depthOfConditionsWithCorrelatedContext > this.m_maxDepthOfDisjunctions)
				{
					this.m_maxDepthOfDisjunctions = this.m_depthOfConditionsWithCorrelatedContext;
				}
			}
			visitAction(condition);
			if (tableJoinRequired)
			{
				this.m_depthOfConditionsWithCorrelatedContext--;
			}
			if (this.m_depthOfConditionsWithCorrelatedContext == 0)
			{
				this.m_entities.Clear();
			}
		}

		// Token: 0x040003D4 RID: 980
		internal const int MaxDepthLimit = 3;

		// Token: 0x040003D5 RID: 981
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x040003D6 RID: 982
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040003D7 RID: 983
		private readonly bool m_allowHierarchicalDisjunction;

		// Token: 0x040003D8 RID: 984
		private readonly bool m_allowDisjunctionForIn;

		// Token: 0x040003D9 RID: 985
		private readonly HashSet<IConceptualEntity> m_entities;

		// Token: 0x040003DA RID: 986
		private bool m_requiresJoinPredicateSubQuery;

		// Token: 0x040003DB RID: 987
		private int m_depthOfConditionsWithCorrelatedContext;

		// Token: 0x040003DC RID: 988
		private int m_maxDepthOfDisjunctions;

		// Token: 0x040003DD RID: 989
		private bool m_hasInFilter;
	}
}
