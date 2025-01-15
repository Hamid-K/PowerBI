using System;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.Annotations;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization
{
	// Token: 0x020000FA RID: 250
	internal sealed class ResolvedQueryDataShapeEquivalenceComparer : ResolvedQueryDefinitionEquivalenceComparer
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x0002171C File Offset: 0x0001F91C
		internal ResolvedQueryDataShapeEquivalenceComparer(SemanticQueryDataShapeAnnotations annotations, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName, ITracer tracer)
			: base((ResolvedQueryDefinitionEquivalenceComparer structureComparer, ResolvedQueryEquivalenceComparerContext context) => new ResolvedExpressionDataShapeEquivalenceComparer(structureComparer, context, tracer))
		{
			this._attributesByQueryName = ResolvedQueryDataShapeEquivalenceComparer.CompileQueryAttributes(annotations, suppressedJoinPredicatesByName);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00021758 File Offset: 0x0001F958
		private static IReadOnlyDictionary<string, ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes> CompileQueryAttributes(SemanticQueryDataShapeAnnotations annotations, IReadOnlyList<DataShapeBindingSuppressedJoinPredicate> suppressedJoinPredicatesByName)
		{
			Dictionary<string, ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes> dictionary = new Dictionary<string, ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes>(QueryNameComparer.Instance);
			foreach (ResolvedQueryDefinition resolvedQueryDefinition in annotations.QueryDefinitionByName.Values)
			{
				IReadOnlyDictionary<string, IntermediateTableUsage> readOnlyDictionary;
				if (annotations.ExpressionSourceUsageByQueryName.TryGetValue(resolvedQueryDefinition.Name, out readOnlyDictionary))
				{
					foreach (ResolvedQuerySource resolvedQuerySource in resolvedQueryDefinition.From)
					{
						ResolvedExpressionSource resolvedExpressionSource = resolvedQuerySource as ResolvedExpressionSource;
						IntermediateTableUsage intermediateTableUsage;
						if (resolvedExpressionSource != null && readOnlyDictionary.TryGetValue(resolvedQuerySource.Name, out intermediateTableUsage))
						{
							ResolvedQuerySubqueryExpression resolvedQuerySubqueryExpression = resolvedExpressionSource.Expression as ResolvedQuerySubqueryExpression;
							if (resolvedQuerySubqueryExpression != null)
							{
								ResolvedQueryDefinition subquery = resolvedQuerySubqueryExpression.Subquery;
								ResolvedQueryDataShapeEquivalenceComparer.GetOrCreateAttributes(dictionary, subquery.Name).Usage = new IntermediateTableUsage?(intermediateTableUsage);
							}
						}
					}
				}
				IReadOnlyDictionary<string, IntermediateTableUsage> readOnlyDictionary2;
				if (annotations.LetBindingUsageByQueryName.TryGetValue(resolvedQueryDefinition.Name, out readOnlyDictionary2))
				{
					foreach (ResolvedQueryLetBinding resolvedQueryLetBinding in resolvedQueryDefinition.Let)
					{
						IntermediateTableUsage intermediateTableUsage2;
						if (readOnlyDictionary2.TryGetValue(resolvedQueryLetBinding.Name, out intermediateTableUsage2))
						{
							ResolvedQuerySubqueryExpression resolvedQuerySubqueryExpression2 = resolvedQueryLetBinding.Expression as ResolvedQuerySubqueryExpression;
							if (resolvedQuerySubqueryExpression2 != null)
							{
								ResolvedQueryDefinition subquery2 = resolvedQuerySubqueryExpression2.Subquery;
								ResolvedQueryDataShapeEquivalenceComparer.GetOrCreateAttributes(dictionary, subquery2.Name).Usage = new IntermediateTableUsage?(intermediateTableUsage2);
							}
						}
					}
				}
			}
			if (suppressedJoinPredicatesByName != null)
			{
				foreach (DataShapeBindingSuppressedJoinPredicate dataShapeBindingSuppressedJoinPredicate in suppressedJoinPredicatesByName)
				{
					ResolvedQueryDataShapeEquivalenceComparer.GetOrCreateAttributes(dictionary, dataShapeBindingSuppressedJoinPredicate.QueryReference.SourceName).AddSuppressedJoinPredicate(dataShapeBindingSuppressedJoinPredicate.QueryReference.ExpressionName);
				}
			}
			return dictionary;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002197C File Offset: 0x0001FB7C
		private static ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes GetOrCreateAttributes(Dictionary<string, ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes> attributesByName, string name)
		{
			ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes queryAttributes;
			if (!attributesByName.TryGetValue(name, out queryAttributes))
			{
				queryAttributes = new ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes();
				attributesByName.Add(name, queryAttributes);
			}
			return queryAttributes;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000219A4 File Offset: 0x0001FBA4
		public override bool Equals(ResolvedQueryDefinition left, ResolvedQueryDefinition right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryDefinition>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.HaveSameQueryAttributes(left, right) && base.Equals(left, right);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000219E0 File Offset: 0x0001FBE0
		private bool HaveSameQueryAttributes(ResolvedQueryDefinition left, ResolvedQueryDefinition right)
		{
			ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes queryAttributes;
			ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes queryAttributes2;
			return this.TryGetQueryAttributes(left, out queryAttributes) && this.TryGetQueryAttributes(right, out queryAttributes2) && queryAttributes.Equals(queryAttributes2);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00021A0C File Offset: 0x0001FC0C
		private bool TryGetQueryAttributes(ResolvedQueryDefinition query, out ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes attributes)
		{
			if (query.Name != null && this._attributesByQueryName.TryGetValue(query.Name, out attributes) && attributes.Usage != null)
			{
				return true;
			}
			attributes = null;
			return false;
		}

		// Token: 0x0400044C RID: 1100
		private readonly IReadOnlyDictionary<string, ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes> _attributesByQueryName;

		// Token: 0x02000167 RID: 359
		private sealed class QueryAttributes : IEquatable<ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes>
		{
			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06000A0F RID: 2575 RVA: 0x000266CB File Offset: 0x000248CB
			// (set) Token: 0x06000A10 RID: 2576 RVA: 0x000266D3 File Offset: 0x000248D3
			internal IntermediateTableUsage? Usage { get; set; }

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000A11 RID: 2577 RVA: 0x000266DC File Offset: 0x000248DC
			// (set) Token: 0x06000A12 RID: 2578 RVA: 0x000266E4 File Offset: 0x000248E4
			internal HashSet<string> SuppressedJoinPredicates { get; set; }

			// Token: 0x06000A13 RID: 2579 RVA: 0x000266ED File Offset: 0x000248ED
			internal void AddSuppressedJoinPredicate(string name)
			{
				if (this.SuppressedJoinPredicates == null)
				{
					this.SuppressedJoinPredicates = new HashSet<string>(QueryNameComparer.Instance);
				}
				this.SuppressedJoinPredicates.Add(name);
			}

			// Token: 0x06000A14 RID: 2580 RVA: 0x00026714 File Offset: 0x00024914
			public bool Equals(ResolvedQueryDataShapeEquivalenceComparer.QueryAttributes other)
			{
				if (other != null)
				{
					IntermediateTableUsage? usage = this.Usage;
					IntermediateTableUsage? usage2 = other.Usage;
					if ((usage.GetValueOrDefault() == usage2.GetValueOrDefault()) & (usage != null == (usage2 != null)))
					{
						return this.EqualsSuppressedJoinPredicates(other.SuppressedJoinPredicates);
					}
				}
				return false;
			}

			// Token: 0x06000A15 RID: 2581 RVA: 0x00026764 File Offset: 0x00024964
			private bool EqualsSuppressedJoinPredicates(HashSet<string> other)
			{
				bool? flag = Util.AreEqual<HashSet<string>>(this.SuppressedJoinPredicates, other);
				if (flag != null)
				{
					return flag.Value;
				}
				return this.SuppressedJoinPredicates.SetEquals(other);
			}
		}
	}
}
