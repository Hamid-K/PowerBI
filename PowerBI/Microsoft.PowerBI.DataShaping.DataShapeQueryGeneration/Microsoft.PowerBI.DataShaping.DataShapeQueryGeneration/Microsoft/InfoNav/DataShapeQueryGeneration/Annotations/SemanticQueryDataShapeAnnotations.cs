using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Annotations
{
	// Token: 0x02000116 RID: 278
	internal sealed class SemanticQueryDataShapeAnnotations
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x00024F3B File Offset: 0x0002313B
		internal SemanticQueryDataShapeAnnotations(IReadOnlyDictionary<string, ResolvedQueryDefinition> queryDefinitionByName, IReadOnlyDictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>> expressionSourceUsageByQueryName, IReadOnlyDictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>> letBindingUsageByQueryName, IReadOnlyDictionary<string, int> visualCalculationExpressionCountByQueryName)
		{
			this.QueryDefinitionByName = queryDefinitionByName;
			this.ExpressionSourceUsageByQueryName = expressionSourceUsageByQueryName;
			this.LetBindingUsageByQueryName = letBindingUsageByQueryName;
			this.VisualCalculationExpressionCountByQueryName = visualCalculationExpressionCountByQueryName;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00024F60 File Offset: 0x00023160
		internal IReadOnlyDictionary<string, ResolvedQueryDefinition> QueryDefinitionByName { get; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00024F68 File Offset: 0x00023168
		internal int SubqueryCount
		{
			get
			{
				return this.QueryDefinitionByName.Count - 1;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00024F78 File Offset: 0x00023178
		internal IntermediateTableUsage ConsolidatedExpressionSourceUsage
		{
			get
			{
				IntermediateTableUsage intermediateTableUsage = IntermediateTableUsage.None;
				foreach (IReadOnlyDictionary<string, IntermediateTableUsage> readOnlyDictionary in this.ExpressionSourceUsageByQueryName.Values)
				{
					foreach (IntermediateTableUsage intermediateTableUsage2 in readOnlyDictionary.Values)
					{
						intermediateTableUsage |= intermediateTableUsage2;
					}
				}
				return intermediateTableUsage;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00025000 File Offset: 0x00023200
		internal IReadOnlyDictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>> ExpressionSourceUsageByQueryName { get; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00025008 File Offset: 0x00023208
		internal IReadOnlyDictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>> LetBindingUsageByQueryName { get; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00025010 File Offset: 0x00023210
		internal IReadOnlyDictionary<string, int> VisualCalculationExpressionCountByQueryName { get; }

		// Token: 0x0600091B RID: 2331 RVA: 0x00025018 File Offset: 0x00023218
		internal int GetExpressionSourceForRegroupingCount(ResolvedQueryDefinition queryDefinition)
		{
			string name = queryDefinition.Name;
			int num = 0;
			IReadOnlyDictionary<string, IntermediateTableUsage> readOnlyDictionary;
			if (this.ExpressionSourceUsageByQueryName.TryGetValue(queryDefinition.Name, out readOnlyDictionary))
			{
				foreach (KeyValuePair<string, IntermediateTableUsage> keyValuePair in readOnlyDictionary)
				{
					if (keyValuePair.Value.HasFlag(IntermediateTableUsage.Regrouping))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00025098 File Offset: 0x00023298
		internal bool ContainsSubqueryReferencesForRegrouping(ResolvedQueryDefinition queryDefinition)
		{
			string name = queryDefinition.Name;
			IReadOnlyDictionary<string, IntermediateTableUsage> readOnlyDictionary;
			if (this.ExpressionSourceUsageByQueryName.TryGetValue(queryDefinition.Name, out readOnlyDictionary))
			{
				foreach (KeyValuePair<string, IntermediateTableUsage> keyValuePair in readOnlyDictionary)
				{
					if (keyValuePair.Value.HasFlag(IntermediateTableUsage.Regrouping))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00025118 File Offset: 0x00023318
		internal HashSet<ResolvedQueryDefinition> GetSubqueriesUsedInFiltering(ResolvedQueryDefinition queryDefinition)
		{
			string name = queryDefinition.Name;
			HashSet<ResolvedQueryDefinition> hashSet = new HashSet<ResolvedQueryDefinition>(ReferenceEqualityComparer<ResolvedQueryDefinition>.Instance);
			IReadOnlyDictionary<string, IntermediateTableUsage> readOnlyDictionary;
			if (this.ExpressionSourceUsageByQueryName.TryGetValue(name, out readOnlyDictionary))
			{
				this.AddQuerySourcesUsedInFiltering(readOnlyDictionary, hashSet);
			}
			if (this.LetBindingUsageByQueryName.TryGetValue(name, out readOnlyDictionary))
			{
				this.AddQuerySourcesUsedInFiltering(readOnlyDictionary, hashSet);
			}
			return hashSet;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00025168 File Offset: 0x00023368
		internal bool QueryHasVisualCalculationsExpressions(string queryName)
		{
			int num;
			return this.VisualCalculationExpressionCountByQueryName.TryGetValue(queryName, out num) && num > 0;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002518B File Offset: 0x0002338B
		internal bool AnyQueryHasVisualCalculationExpressions()
		{
			return this.VisualCalculationExpressionCountByQueryName.Count > 0;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002519C File Offset: 0x0002339C
		private void AddQuerySourcesUsedInFiltering(IReadOnlyDictionary<string, IntermediateTableUsage> sourceUsageMap, HashSet<ResolvedQueryDefinition> querySourcesUsedInFiltering)
		{
			foreach (KeyValuePair<string, IntermediateTableUsage> keyValuePair in sourceUsageMap)
			{
				ResolvedQueryDefinition resolvedQueryDefinition;
				if (keyValuePair.Value.HasFlag(IntermediateTableUsage.Filtering) && this.QueryDefinitionByName.TryGetValue(keyValuePair.Key, out resolvedQueryDefinition))
				{
					querySourcesUsedInFiltering.Add(resolvedQueryDefinition);
				}
			}
		}

		// Token: 0x040004A4 RID: 1188
		internal static readonly SemanticQueryDataShapeAnnotations Empty = new SemanticQueryDataShapeAnnotations(new Dictionary<string, ResolvedQueryDefinition>(QueryNameComparer.Instance), new Dictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>>(QueryNameComparer.Instance), new Dictionary<string, IReadOnlyDictionary<string, IntermediateTableUsage>>(QueryNameComparer.Instance), new Dictionary<string, int>(QueryNameComparer.Instance));
	}
}
