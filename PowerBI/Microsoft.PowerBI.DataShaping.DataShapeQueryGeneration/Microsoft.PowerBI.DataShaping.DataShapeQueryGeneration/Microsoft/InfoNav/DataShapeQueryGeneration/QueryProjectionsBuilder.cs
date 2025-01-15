using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000BD RID: 189
	internal sealed class QueryProjectionsBuilder
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00019555 File Offset: 0x00017755
		internal QueryProjectionsBuilder(bool hasSortByMeasure, DataShapeGenerationErrorContext errorContext)
		{
			this._hasSortByMeasure = hasSortByMeasure;
			this._errorContext = errorContext;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001956B File Offset: 0x0001776B
		internal int PrimaryMemberCount
		{
			get
			{
				return QueryProjectionsBuilder.Count<QueryMemberBuilder>(this._primaryMembers);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00019578 File Offset: 0x00017778
		internal IList<QueryMemberBuilder> PrimaryMembers
		{
			get
			{
				return this._primaryMembers;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00019580 File Offset: 0x00017780
		internal int SecondaryMemberCount
		{
			get
			{
				return QueryProjectionsBuilder.Count<QueryMemberBuilder>(this._secondaryMembers);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001958D File Offset: 0x0001778D
		internal IList<QueryMemberBuilder> SecondaryMembers
		{
			get
			{
				return this._secondaryMembers;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00019595 File Offset: 0x00017795
		internal int MeasureCount
		{
			get
			{
				return QueryProjectionsBuilder.Count<ProjectedDsqExpression>(this._measures);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x000195A4 File Offset: 0x000177A4
		private bool HasInstanceFilters
		{
			get
			{
				if (!this.PrimaryMembers.IsNullOrEmptyCollection<QueryMemberBuilder>())
				{
					if (this.PrimaryMembers.Any((QueryMemberBuilder m) => m.HasInstanceFilters()))
					{
						return true;
					}
				}
				if (!this.SecondaryMembers.IsNullOrEmptyCollection<QueryMemberBuilder>())
				{
					return this.SecondaryMembers.Any((QueryMemberBuilder m) => m.HasInstanceFilters());
				}
				return false;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00019625 File Offset: 0x00017825
		private bool HasPrimaryScopedAggregates
		{
			get
			{
				if (!this.PrimaryMembers.IsNullOrEmptyCollection<QueryMemberBuilder>())
				{
					return this.PrimaryMembers.Any((QueryMemberBuilder m) => m.ValueBuilders.Any((QueryGroupValueBuilder vb) => vb.GetAggregates() != null && vb.GetAggregates().HasScopedAggregate));
				}
				return false;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00019660 File Offset: 0x00017860
		private bool HasPrimarySubtotals
		{
			get
			{
				if (!this.PrimaryMembers.IsNullOrEmptyCollection<QueryMemberBuilder>())
				{
					return this.PrimaryMembers.Any((QueryMemberBuilder m) => m.Group.HasSubtotal);
				}
				return false;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001969B File Offset: 0x0001789B
		private bool HasSecondarySubtotals
		{
			get
			{
				if (!this.SecondaryMembers.IsNullOrEmptyCollection<QueryMemberBuilder>())
				{
					return this.SecondaryMembers.Any((QueryMemberBuilder m) => m.Group.HasSubtotal);
				}
				return false;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000196D6 File Offset: 0x000178D6
		private bool HasAnySubtotals
		{
			get
			{
				return this.HasPrimarySubtotals || this.HasSecondarySubtotals;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000196E8 File Offset: 0x000178E8
		internal IList<ProjectedDsqExpression> Measures
		{
			get
			{
				return this._measures;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000196F0 File Offset: 0x000178F0
		internal void AddPrimaryMember(QueryMemberBuilder member)
		{
			Util.EnsureList<QueryMemberBuilder>(ref this._primaryMembers).Add(member);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00019703 File Offset: 0x00017903
		internal void AddSecondaryMember(QueryMemberBuilder member)
		{
			Util.EnsureList<QueryMemberBuilder>(ref this._secondaryMembers).Add(member);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00019716 File Offset: 0x00017916
		internal void AddMeasure(ProjectedDsqExpression measure)
		{
			Util.EnsureList<ProjectedDsqExpression>(ref this._measures).Add(measure);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001972C File Offset: 0x0001792C
		internal void AddFilters(IReadOnlyList<ResolvedQueryFilter> filters, int selectIndex)
		{
			Dictionary<int, HashSet<ResolvedQueryFilter>> dictionary = Util.EnsureDictionary<int, HashSet<ResolvedQueryFilter>>(ref this._projectionFiltersBySelectIndex, null);
			HashSet<ResolvedQueryFilter> hashSet;
			if (!dictionary.TryGetValue(selectIndex, out hashSet))
			{
				dictionary.Add(selectIndex, filters.ToHashSet(null));
				return;
			}
			hashSet.UnionWith(filters);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00019767 File Offset: 0x00017967
		internal void AddDataShapeProjection(ProjectedDsqExpression projection)
		{
			Util.EnsureList<ProjectedDsqExpression>(ref this._dataShapeProjections).Add(projection);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001977A File Offset: 0x0001797A
		private void SuppressAllSubtotals()
		{
			this.SuppressPrimarySubtotals();
			this.SuppressSecondarySubtotals();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00019788 File Offset: 0x00017988
		private void SetExplicitSubtotals()
		{
			QueryProjectionsBuilder.SetExplicitSubtotals(this._primaryMembers);
			QueryProjectionsBuilder.SetExplicitSubtotals(this._secondaryMembers);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000197A0 File Offset: 0x000179A0
		private void SuppressPrimarySubtotals()
		{
			QueryProjectionsBuilder.SuppressSubtotals(this._primaryMembers);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000197AD File Offset: 0x000179AD
		private void SuppressSecondarySubtotals()
		{
			QueryProjectionsBuilder.SuppressSubtotals(this._secondaryMembers);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000197BC File Offset: 0x000179BC
		private static void SuppressSubtotals(List<QueryMemberBuilder> groups)
		{
			if (groups == null)
			{
				return;
			}
			for (int i = 0; i < groups.Count; i++)
			{
				groups[i].SuppressSubtotals();
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000197EC File Offset: 0x000179EC
		private static void SetExplicitSubtotals(List<QueryMemberBuilder> groups)
		{
			if (groups == null)
			{
				return;
			}
			for (int i = 0; i < groups.Count; i++)
			{
				groups[i].SetExplicitSubtotals();
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001981C File Offset: 0x00017A1C
		internal QueryProjections ToProjections(IReadOnlyList<IReadOnlyList<PrimitiveValue>> restartTokens)
		{
			this.DetermineSubtotalState(restartTokens);
			this.DetermineScopedAggregatesState();
			IReadOnlyList<QueryMember> readOnlyList = QueryProjectionsBuilder.ToGroups(this._primaryMembers);
			IReadOnlyList<QueryMember> readOnlyList2 = QueryProjectionsBuilder.ToGroups(this._secondaryMembers);
			IReadOnlyList<ProjectedDsqExpression> readOnlyList3 = this._measures;
			IReadOnlyList<ProjectedDsqExpression> readOnlyList4 = readOnlyList3 ?? Util.EmptyReadOnlyList<ProjectedDsqExpression>();
			readOnlyList3 = this._dataShapeProjections;
			return new QueryProjections(readOnlyList, readOnlyList2, readOnlyList4, readOnlyList3 ?? Util.EmptyReadOnlyList<ProjectedDsqExpression>(), this._projectionFiltersBySelectIndex, this._hasSortByMeasure);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00019880 File Offset: 0x00017A80
		private void DetermineSubtotalState(IReadOnlyList<IReadOnlyList<PrimitiveValue>> restartTokens)
		{
			QueryProjectionsBuilder.SuppressDuplicateSubtotals(this._primaryMembers, this._hasSortByMeasure, this._errorContext, true);
			QueryProjectionsBuilder.SuppressDuplicateSubtotals(this._secondaryMembers, false, this._errorContext, false);
			if (!this.HasAnySubtotals)
			{
				return;
			}
			if (this.MeasureCount > 0)
			{
				QueryProjectionsBuilder.SetSubtotalStateForNonAggregatable(this._primaryMembers);
				QueryProjectionsBuilder.SetSubtotalStateForNonAggregatable(this._secondaryMembers);
				return;
			}
			if (this.HasInstanceFilters || this.HasPrimaryScopedAggregates)
			{
				this.SetExplicitSubtotals();
				return;
			}
			if (!restartTokens.IsNullOrEmpty<IReadOnlyList<PrimitiveValue>>() && this.HasPrimarySubtotals)
			{
				List<RestartTokenGroupingBinding> list = QueryProjectionsBuilder.BuildRestartGroupingBindings(this._primaryMembers);
				if (!list.IsNullOrEmpty<RestartTokenGroupingBinding>())
				{
					RestartTokenGroupingBinding[] array;
					List<RestartTokenGroupingValues> list2 = RestartTokenUtils.MapRestartTokenToGroupingValues(restartTokens.ToRestartTokens(), list, out array);
					bool flag;
					if (QueryProjectionsBuilder.AllRestartBindingsHaveValues(list, list2, out flag) || flag)
					{
						this.SetExplicitSubtotals();
						return;
					}
					this.SuppressAllSubtotals();
					return;
				}
			}
			else
			{
				this.SuppressAllSubtotals();
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001994C File Offset: 0x00017B4C
		private void DetermineScopedAggregatesState()
		{
			if (this._primaryMembers.IsNullOrEmpty<QueryMemberBuilder>())
			{
				return;
			}
			for (int i = 0; i < this._primaryMembers.Count; i++)
			{
				if (!this._primaryMembers[i].Group.HasSubtotal)
				{
					QueryProjectionsBuilder.SuppressScopedAggregates(this._primaryMembers, this._measures, i);
				}
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000199A8 File Offset: 0x00017BA8
		private static List<RestartTokenGroupingBinding> BuildRestartGroupingBindings(IList<QueryMemberBuilder> members)
		{
			if (members.IsNullOrEmpty<QueryMemberBuilder>())
			{
				return null;
			}
			List<RestartTokenGroupingBinding> list = new List<RestartTokenGroupingBinding>(members.Count);
			foreach (QueryMemberBuilder queryMemberBuilder in members)
			{
				list.Add(new RestartTokenGroupingBinding(queryMemberBuilder.Group.Subtotal));
			}
			return list;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00019A18 File Offset: 0x00017C18
		internal static bool AllRestartBindingsHaveValues(List<RestartTokenGroupingBinding> restartBindings, List<RestartTokenGroupingValues> restartValues, out bool hasPartialMatch)
		{
			hasPartialMatch = false;
			for (int i = 0; i < restartBindings.Count; i++)
			{
				if (restartValues[i] == null)
				{
					if (i == 0)
					{
						return false;
					}
					hasPartialMatch = true;
				}
				else
				{
					if (restartBindings[i].SubtotalPosition != SubtotalType.None && restartValues[i].SubtotalToken == null)
					{
						hasPartialMatch = false;
						return false;
					}
					if (hasPartialMatch)
					{
						hasPartialMatch = false;
						return false;
					}
				}
			}
			return !hasPartialMatch;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00019A80 File Offset: 0x00017C80
		private static void SetSubtotalStateForNonAggregatable(IList<QueryMemberBuilder> builders)
		{
			if (builders.IsNullOrEmpty<QueryMemberBuilder>())
			{
				return;
			}
			bool flag = builders.Any((QueryMemberBuilder m) => m.HasInstanceFilters());
			bool flag2 = false;
			for (int i = builders.Count - 1; i >= 0; i--)
			{
				QueryMemberBuilder queryMemberBuilder = builders[i];
				if (queryMemberBuilder.IsNonAggregatable)
				{
					flag2 = true;
				}
				if (flag2 && queryMemberBuilder.Group.HasSubtotal)
				{
					if (flag)
					{
						queryMemberBuilder.SetExplicitSubtotals();
					}
					else
					{
						queryMemberBuilder.SuppressSubtotals();
					}
				}
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00019B04 File Offset: 0x00017D04
		private static void SuppressDuplicateSubtotals(IList<QueryMemberBuilder> builders, bool hasSortByMeasure, DataShapeGenerationErrorContext errorContext, bool isPrimary)
		{
			if (builders.IsNullOrEmpty<QueryMemberBuilder>() || builders.Count == 1)
			{
				return;
			}
			bool flag = builders.Any((QueryMemberBuilder b) => b.Group.HasSubtotal);
			if (hasSortByMeasure || flag)
			{
				List<QueryRollupGroupingScope> list = QueryRollupGroupingScope.CreateGroupingScopes(builders);
				HashSet<ExpressionNode> hashSet = new HashSet<ExpressionNode>();
				for (int i = 0; i < list.Count; i++)
				{
					QueryRollupGroupingScope queryRollupGroupingScope = list[i];
					if (!queryRollupGroupingScope.TryAddGroupKeyExpressions(hashSet))
					{
						queryRollupGroupingScope.SuppressAllRollupsIfNeeded(hasSortByMeasure);
						errorContext.Register(DataShapeGenerationMessages.DuplicateGroupingKeysDetected(EngineMessageSeverity.Warning, isPrimary ? "primary" : "secondary", i + 1));
					}
					queryRollupGroupingScope.AddNonGroupKeyExpressions(hashSet);
				}
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00019BB0 File Offset: 0x00017DB0
		private static void SuppressScopedAggregates(IList<QueryMemberBuilder> builders, IList<ProjectedDsqExpression> measures, int primaryGroupIndex)
		{
			if (measures.IsNullOrEmpty<ProjectedDsqExpression>())
			{
				for (int i = 0; i <= primaryGroupIndex; i++)
				{
					bool flag = true;
					foreach (QueryGroupValueBuilder queryGroupValueBuilder in builders[i].ValueBuilders)
					{
						DsqExpressionAggregates aggregates = queryGroupValueBuilder.GetAggregates();
						if (aggregates == null || !aggregates.TrySuppressScopedAggregates(primaryGroupIndex))
						{
							flag = false;
						}
					}
					if (flag)
					{
						builders[i].ClearExplicitSubtotals();
					}
				}
				return;
			}
			foreach (ProjectedDsqExpression projectedDsqExpression in measures)
			{
				projectedDsqExpression.Aggregates.TrySuppressScopedAggregates(primaryGroupIndex);
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00019C78 File Offset: 0x00017E78
		private static IReadOnlyList<QueryMember> ToGroups(List<QueryMemberBuilder> builders)
		{
			if (builders == null)
			{
				return Util.EmptyReadOnlyList<QueryMember>();
			}
			List<QueryMember> list = new List<QueryMember>(builders.Count);
			for (int i = 0; i < builders.Count; i++)
			{
				list.Add(builders[i].ToMember());
			}
			return list;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00019CBE File Offset: 0x00017EBE
		private static int Count<T>(List<T> list)
		{
			if (list == null)
			{
				return 0;
			}
			return list.Count;
		}

		// Token: 0x0400039C RID: 924
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x0400039D RID: 925
		private readonly bool _hasSortByMeasure;

		// Token: 0x0400039E RID: 926
		private List<QueryMemberBuilder> _primaryMembers;

		// Token: 0x0400039F RID: 927
		private List<QueryMemberBuilder> _secondaryMembers;

		// Token: 0x040003A0 RID: 928
		private List<ProjectedDsqExpression> _measures;

		// Token: 0x040003A1 RID: 929
		private List<ProjectedDsqExpression> _dataShapeProjections;

		// Token: 0x040003A2 RID: 930
		private Dictionary<int, HashSet<ResolvedQueryFilter>> _projectionFiltersBySelectIndex;
	}
}
