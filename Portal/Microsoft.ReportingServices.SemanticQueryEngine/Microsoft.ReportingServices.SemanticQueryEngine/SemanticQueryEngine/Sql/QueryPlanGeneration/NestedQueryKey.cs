using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000058 RID: 88
	internal sealed class NestedQueryKey
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x00012418 File Offset: 0x00010618
		internal NestedQueryKey(ExpressionProcessInfo info, int pathPointIndexBeforeAdjusting)
		{
			if (info.Path == null || info.Path.IsEmpty)
			{
				throw SQEAssert.AssertFalseAndThrow("Attempt to create a nested query key for an empty path.", Array.Empty<object>());
			}
			this.m_path = info.Path;
			this.m_pathStartIndex = info.PathPointIndex;
			this.m_forDistinct = info.IsDistinctKey;
			this.m_nonTransitive = info.GetNonTransitiveProcessingRequired() || this.m_forDistinct;
			this.m_transitiveCompatibilityPathLength = this.GetCompatibilityPathLength(this.m_nonTransitive);
			this.m_nonTransitiveCompatibilityPathLength = (this.m_nonTransitive ? this.m_transitiveCompatibilityPathLength : this.GetCompatibilityPathLength(true));
			this.m_filteredPathItem = this.GetPathItem(0);
			this.m_pathItemOptionality = this.GetPathItemOptionality(pathPointIndexBeforeAdjusting);
			this.m_performAggregation = this.GetPerformAggregation(0, out this.m_performAggregationOptional);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000124E7 File Offset: 0x000106E7
		internal FilteredPathItem FilteredPathItem
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_filteredPathItem;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x000124EF File Offset: 0x000106EF
		internal Optionality PathItemOptionality
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_pathItemOptionality;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000124F7 File Offset: 0x000106F7
		internal bool PerformAggregation
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_performAggregation;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000124FF File Offset: 0x000106FF
		internal bool PerformAggregationOptional
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_performAggregationOptional;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00012507 File Offset: 0x00010707
		internal bool HasOuterAggregation
		{
			[DebuggerStepThrough]
			get
			{
				return this.HasPriorToMany(0);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00012510 File Offset: 0x00010710
		internal bool NonTransitive
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_nonTransitive;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00012518 File Offset: 0x00010718
		internal int TransitiveCompatibilityPathLength
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_transitiveCompatibilityPathLength;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00012520 File Offset: 0x00010720
		internal int NonTransitiveCompatibilityPathLength
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_nonTransitiveCompatibilityPathLength;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00012528 File Offset: 0x00010728
		internal bool ForDistinct
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_forDistinct;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00012530 File Offset: 0x00010730
		internal SqlNestedQuery CreatedNestedQuery(QueryPlanBuilder qpBuilder)
		{
			if (this.m_filteredPathItem.ExpressionPathItem is RolePathItem)
			{
				return new SqlQueryOnRolePathItem(qpBuilder, this);
			}
			if (this.m_filteredPathItem.ExpressionPathItem is TotalAggregationPathItem)
			{
				return new SqlQueryOnBaseEntity(qpBuilder, this);
			}
			if (this.m_filteredPathItem.ExpressionPathItem is SelfPathItem)
			{
				return new SqlQueryOnSelfPathItem(qpBuilder, this);
			}
			if (this.m_filteredPathItem.ExpressionPathItem is InheritancePathItem)
			{
				return new SqlQueryOnInheritancePathItem(qpBuilder, this);
			}
			throw SQEAssert.AssertFalseAndThrow("Unknown or invalid m_filteredPathItem.ExpressionPathItem: {0}.", new object[] { this.m_filteredPathItem.ExpressionPathItem });
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000125C4 File Offset: 0x000107C4
		internal bool IsReuseableBy(NestedQueryKey compareToKey)
		{
			if (compareToKey.ForDistinct)
			{
				return compareToKey.NonTransitiveCompatibilityPathLength <= this.NonTransitiveCompatibilityPathLength && this.CompareCompatibilityPathSegment(compareToKey, compareToKey.NonTransitiveCompatibilityPathLength);
			}
			int num = (this.NonTransitive ? this.NonTransitiveCompatibilityPathLength : this.TransitiveCompatibilityPathLength);
			if (num > compareToKey.NonTransitiveCompatibilityPathLength)
			{
				return false;
			}
			if (compareToKey.NonTransitive)
			{
				if (!this.NonTransitive)
				{
					throw SQEAssert.AssertFalseAndThrow("Invalid order of NQK matching: IsReusableBy is called on a transitive key to check a non-transitive key.", Array.Empty<object>());
				}
				if (compareToKey.TransitiveCompatibilityPathLength != compareToKey.NonTransitiveCompatibilityPathLength)
				{
					throw SQEAssert.AssertFalseAndThrow("TCPL != NTCPL on a non-transitive key.", Array.Empty<object>());
				}
				if (num != compareToKey.NonTransitiveCompatibilityPathLength)
				{
					return false;
				}
			}
			return this.CompareCompatibilityPathSegment(compareToKey, num);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001266C File Offset: 0x0001086C
		private bool CompareCompatibilityPathSegment(NestedQueryKey compareToKey, int compatibilityPathLength)
		{
			for (int i = 0; i < compatibilityPathLength; i++)
			{
				FilteredPathItem pathItem = this.GetPathItem(i);
				FilteredPathItem pathItem2 = compareToKey.GetPathItem(i);
				if (!pathItem.IsSameAs(pathItem2))
				{
					return false;
				}
				bool flag;
				bool performAggregation = this.GetPerformAggregation(i, out flag);
				bool flag2;
				bool performAggregation2 = compareToKey.GetPerformAggregation(i, out flag2);
				if (flag && !flag2)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				if (!flag && !flag2 && performAggregation != performAggregation2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000126CF File Offset: 0x000108CF
		internal FilteredPath GetNonTransitivePathSegment()
		{
			return this.m_path.GetSegment(this.m_pathStartIndex, this.m_nonTransitiveCompatibilityPathLength);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000126E8 File Offset: 0x000108E8
		public override string ToString()
		{
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("NT{0} PA{1} PAO{2} PSI={3} TCPL={4} NTCPL={5} NTP={6}", new object[]
			{
				this.m_nonTransitive ? "+" : "-",
				this.m_performAggregation ? "+" : "-",
				this.m_performAggregationOptional ? "+" : "-",
				this.m_pathStartIndex,
				this.m_transitiveCompatibilityPathLength,
				this.m_nonTransitiveCompatibilityPathLength,
				this.GetNonTransitivePathSegment()
			});
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001277D File Offset: 0x0001097D
		private void CheckRelativeIndex(int relativeIndex)
		{
			if (relativeIndex < 0 || relativeIndex >= this.m_nonTransitiveCompatibilityPathLength)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("relativeIndex"));
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001279C File Offset: 0x0001099C
		private FilteredPathItem GetPathItem(int relativeIndex)
		{
			this.CheckRelativeIndex(relativeIndex);
			return this.m_path[this.m_pathStartIndex + relativeIndex];
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000127B8 File Offset: 0x000109B8
		private bool GetPerformAggregation(int relativeIndex, out bool optional)
		{
			this.CheckRelativeIndex(relativeIndex);
			optional = false;
			if (this.m_path[this.m_pathStartIndex + relativeIndex].Cardinality != Cardinality.Many)
			{
				return false;
			}
			if (!this.HasPriorToMany(relativeIndex))
			{
				return true;
			}
			if (this.m_nonTransitive)
			{
				return false;
			}
			optional = true;
			return true;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00012805 File Offset: 0x00010A05
		private bool HasPriorToMany(int relativeIndex)
		{
			this.CheckRelativeIndex(relativeIndex);
			return this.m_path.GetLeadingScalarLength() < this.m_pathStartIndex + relativeIndex;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00012824 File Offset: 0x00010A24
		private int GetCompatibilityPathLength(bool nonTransitive)
		{
			int num = this.m_pathStartIndex;
			if (this.m_path[this.m_pathStartIndex].Cardinality == Cardinality.Many && nonTransitive)
			{
				for (int i = this.m_pathStartIndex + 1; i < this.m_path.Length; i++)
				{
					if (this.m_path[i].Cardinality == Cardinality.Many || (this.m_path[i].ReverseCardinality == Cardinality.Many && this.m_path[i].Cardinality == Cardinality.One))
					{
						num = i;
					}
				}
			}
			return num - this.m_pathStartIndex + 1;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000128BC File Offset: 0x00010ABC
		private Optionality GetPathItemOptionality(int pathPointIndexBeforeAdjusting)
		{
			Optionality optionality = this.m_filteredPathItem.Optionality;
			if (pathPointIndexBeforeAdjusting != this.m_pathStartIndex && this.m_path.GetSegment(pathPointIndexBeforeAdjusting, this.m_pathStartIndex - pathPointIndexBeforeAdjusting).GetOptionality() == Optionality.Optional)
			{
				optionality = Optionality.Optional;
			}
			return optionality;
		}

		// Token: 0x040001D0 RID: 464
		private readonly FilteredPath m_path;

		// Token: 0x040001D1 RID: 465
		private readonly int m_pathStartIndex;

		// Token: 0x040001D2 RID: 466
		private readonly bool m_nonTransitive;

		// Token: 0x040001D3 RID: 467
		private readonly bool m_performAggregation;

		// Token: 0x040001D4 RID: 468
		private readonly bool m_performAggregationOptional;

		// Token: 0x040001D5 RID: 469
		private readonly FilteredPathItem m_filteredPathItem;

		// Token: 0x040001D6 RID: 470
		private readonly Optionality m_pathItemOptionality;

		// Token: 0x040001D7 RID: 471
		private readonly int m_transitiveCompatibilityPathLength;

		// Token: 0x040001D8 RID: 472
		private readonly int m_nonTransitiveCompatibilityPathLength;

		// Token: 0x040001D9 RID: 473
		private readonly bool m_forDistinct;

		// Token: 0x020000D0 RID: 208
		internal sealed class OptimizingComparer : Comparer<NestedQueryKey>
		{
			// Token: 0x06000751 RID: 1873 RVA: 0x0001C552 File Offset: 0x0001A752
			internal OptimizingComparer()
			{
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x0001C55C File Offset: 0x0001A75C
			public override int Compare(NestedQueryKey x, NestedQueryKey y)
			{
				if (x == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("x"));
				}
				if (y == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("y"));
				}
				if (!x.PerformAggregationOptional && y.PerformAggregationOptional)
				{
					return -1;
				}
				if (x.PerformAggregationOptional && !y.PerformAggregationOptional)
				{
					return 1;
				}
				if (x.NonTransitive && !y.NonTransitive)
				{
					return -1;
				}
				if (!x.NonTransitive && y.NonTransitive)
				{
					return 1;
				}
				if (!x.ForDistinct && y.ForDistinct)
				{
					return -1;
				}
				if (x.ForDistinct && !y.ForDistinct)
				{
					return 1;
				}
				return 0;
			}
		}
	}
}
