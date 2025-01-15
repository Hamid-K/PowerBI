using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination
{
	// Token: 0x02000102 RID: 258
	internal sealed class MergeableSubqueryCollector
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x00021EA9 File Offset: 0x000200A9
		private MergeableSubqueryCollector(ResolvedQueryDefinitionEquivalenceComparer queryComparer)
		{
			this._queryComparer = queryComparer;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00021EB8 File Offset: 0x000200B8
		public static MergeableSubqueryCollectorResult Collect(ResolvedQueryDefinition query, ResolvedQueryDefinitionEquivalenceComparer queryComparer)
		{
			MergeableSubqueryCollector mergeableSubqueryCollector = new MergeableSubqueryCollector(queryComparer);
			mergeableSubqueryCollector.Visit(query);
			return new MergeableSubqueryCollectorResult(mergeableSubqueryCollector.GetFinalGroups(), mergeableSubqueryCollector._usedLetNames, mergeableSubqueryCollector._consideredSubqueryCount, mergeableSubqueryCollector._comparedSubqueryCount);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00021EF0 File Offset: 0x000200F0
		public void Visit(ResolvedQueryDefinition query)
		{
			MergeableSubqueryCollector.Visit<ResolvedQuerySource>(query.From, new Action<ResolvedQuerySource>(this.Visit));
			MergeableSubqueryCollector.Visit<ResolvedQueryLetBinding>(query.Let, new Action<ResolvedQueryLetBinding>(this.Visit));
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00021F20 File Offset: 0x00020120
		private void Visit(ResolvedQueryLetBinding letBinding)
		{
			if (this._usedLetNames == null)
			{
				this._usedLetNames = new HashSet<string>(QueryNameComparer.Instance);
			}
			this._usedLetNames.Add(letBinding.Name);
			this.Visit(letBinding.Expression);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00021F58 File Offset: 0x00020158
		private void Visit(ResolvedQuerySource source)
		{
			ResolvedExpressionSource resolvedExpressionSource = source as ResolvedExpressionSource;
			if (resolvedExpressionSource != null)
			{
				this.Visit(resolvedExpressionSource);
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00021F76 File Offset: 0x00020176
		private void Visit(ResolvedExpressionSource source)
		{
			this.Visit(source.Expression);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00021F84 File Offset: 0x00020184
		private void Visit(ResolvedQueryExpression expression)
		{
			ResolvedQuerySubqueryExpression resolvedQuerySubqueryExpression = expression as ResolvedQuerySubqueryExpression;
			if (resolvedQuerySubqueryExpression != null)
			{
				this.Visit(resolvedQuerySubqueryExpression);
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00021FA4 File Offset: 0x000201A4
		private void Visit(ResolvedQuerySubqueryExpression subqueryExpr)
		{
			ResolvedQueryDefinition subquery = subqueryExpr.Subquery;
			this._consideredSubqueryCount++;
			QueryShape queryShape = new QueryShape(subquery);
			Util.EnsureDictionary<QueryShape, MergeableSubqueryCollector.SimiliarQueryEntry>(ref this._entriesByShape, null);
			MergeableSubqueryCollector.SimiliarQueryEntry similiarQueryEntry;
			if (!this._entriesByShape.TryGetValue(queryShape, out similiarQueryEntry))
			{
				similiarQueryEntry = new MergeableSubqueryCollector.SimiliarQueryEntry();
				this._entriesByShape.Add(queryShape, similiarQueryEntry);
			}
			if (similiarQueryEntry.TryAddToExistingGroup(subquery, this._queryComparer, ref this._comparedSubqueryCount))
			{
				return;
			}
			EquivalentQueryGroup equivalentQueryGroup = new EquivalentQueryGroup(subquery);
			similiarQueryEntry.AddGrop(equivalentQueryGroup);
			Util.EnsureList<EquivalentQueryGroup>(ref this._groups);
			this._groups.Add(equivalentQueryGroup);
			this.Visit(subquery);
			this.RecordQueryPosition(subquery);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00022048 File Offset: 0x00020248
		private void RecordQueryPosition(ResolvedQueryDefinition subquery)
		{
			if (this._queryPositions == null)
			{
				this._queryPositions = new Dictionary<ResolvedQueryDefinition, int>(ReferenceEqualityComparer<ResolvedQueryDefinition>.Instance);
			}
			this._queryPositions.Add(subquery, this._queryPositions.Count);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002207C File Offset: 0x0002027C
		private List<EquivalentQueryGroup> GetFinalGroups()
		{
			if (this._groups == null)
			{
				return null;
			}
			List<EquivalentQueryGroup> list = null;
			foreach (EquivalentQueryGroup equivalentQueryGroup in this._groups)
			{
				if (equivalentQueryGroup.EquivalentQueries != null)
				{
					if (list == null)
					{
						list = new List<EquivalentQueryGroup>(this._groups.Count);
					}
					list.Add(equivalentQueryGroup);
				}
			}
			if (list != null)
			{
				list.Sort(new Comparison<EquivalentQueryGroup>(this.CompareGroupsByExemplarPosition));
			}
			return list;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00022110 File Offset: 0x00020310
		private int CompareGroupsByExemplarPosition(EquivalentQueryGroup x, EquivalentQueryGroup y)
		{
			return this._queryPositions[x.Exemplar].CompareTo(this._queryPositions[y.Exemplar]);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00022148 File Offset: 0x00020348
		private static void Visit<T>(IEnumerable<T> collection, Action<T> visit)
		{
			if (collection == null)
			{
				return;
			}
			foreach (T t in collection)
			{
				visit(t);
			}
		}

		// Token: 0x0400045C RID: 1116
		private readonly ResolvedQueryDefinitionEquivalenceComparer _queryComparer;

		// Token: 0x0400045D RID: 1117
		private Dictionary<QueryShape, MergeableSubqueryCollector.SimiliarQueryEntry> _entriesByShape;

		// Token: 0x0400045E RID: 1118
		private HashSet<string> _usedLetNames;

		// Token: 0x0400045F RID: 1119
		private List<EquivalentQueryGroup> _groups;

		// Token: 0x04000460 RID: 1120
		private int _consideredSubqueryCount;

		// Token: 0x04000461 RID: 1121
		private int _comparedSubqueryCount;

		// Token: 0x04000462 RID: 1122
		private Dictionary<ResolvedQueryDefinition, int> _queryPositions;

		// Token: 0x02000169 RID: 361
		private sealed class SimiliarQueryEntry
		{
			// Token: 0x06000A19 RID: 2585 RVA: 0x000267BA File Offset: 0x000249BA
			public SimiliarQueryEntry()
			{
				this._groups = new List<EquivalentQueryGroup>(4);
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000267CE File Offset: 0x000249CE
			public IReadOnlyList<EquivalentQueryGroup> Groups
			{
				get
				{
					return this._groups;
				}
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x000267D6 File Offset: 0x000249D6
			public void AddGrop(EquivalentQueryGroup group)
			{
				this._groups.Add(group);
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x000267E4 File Offset: 0x000249E4
			public bool TryAddToExistingGroup(ResolvedQueryDefinition query, ResolvedQueryDefinitionEquivalenceComparer queryComparer, ref int comparedSubqueryCount)
			{
				if (this._groups == null)
				{
					return false;
				}
				foreach (EquivalentQueryGroup equivalentQueryGroup in this._groups)
				{
					comparedSubqueryCount++;
					if (queryComparer.Equals(equivalentQueryGroup.Exemplar, query))
					{
						equivalentQueryGroup.AddEquivalentQuery(query);
						return true;
					}
				}
				return false;
			}

			// Token: 0x0400057C RID: 1404
			private readonly List<EquivalentQueryGroup> _groups;
		}
	}
}
