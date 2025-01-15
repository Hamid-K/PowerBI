using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration.DSQ;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200006B RID: 107
	internal sealed class DsqTargetResolver : IDsqTargetResolver
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x000115C4 File Offset: 0x0000F7C4
		internal DsqTargetResolver(QueryTranslationContext queryTranslationContext, DataShapeBuilderContext dataShapeBuilderContext, DsqScopeLookup dsqScopeLookup, QueryProjections projections, string targetDataShape)
		{
			this._queryTranslationContext = queryTranslationContext;
			this._dataShapeBuilderContext = dataShapeBuilderContext;
			this._dsqScopes = dsqScopeLookup;
			this._projections = projections;
			this._targetDataShape = targetDataShape;
			this._targetComparer = new TargetComparer(queryTranslationContext.Expressions);
			this._normalizer = ContextFilterQueryMemberNormalizer.Instance;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00011618 File Offset: 0x0000F818
		internal string TargetDataShape
		{
			get
			{
				return this._targetDataShape;
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00011620 File Offset: 0x0000F820
		MatchStatus IDsqTargetResolver.TryTranslateToDsqScopeForFilter(IReadOnlyList<ResolvedQueryExpression> targets, DsqFilterType filterType, out Identifier targetScope)
		{
			if (filterType == DsqFilterType.Scope && !this._projections.HasGroups)
			{
				if (targets.Count == 0)
				{
					targetScope = this._targetDataShape;
					return MatchStatus.FullMatch;
				}
				this._queryTranslationContext.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidFilterTargetScope(EngineMessageSeverity.Error, "primary"));
				targetScope = null;
				return MatchStatus.Failed;
			}
			else
			{
				if (targets.All((ResolvedQueryExpression t) => t is ResolvedQuerySourceRefExpression))
				{
					return this.TryGetDefaultScope(filterType, out targetScope);
				}
				if (filterType == DsqFilterType.Exist || filterType == DsqFilterType.DataShape || targets.Count == 0)
				{
					return this.TryGetDefaultScope(filterType, out targetScope);
				}
				List<ResolvedQueryExpression> list = targets.ToList<ResolvedQueryExpression>();
				int num;
				MatchStatus matchStatus = this.ValidateTargetByProjections(list, this._projections.PrimaryMembers, out num);
				if (matchStatus != MatchStatus.FullMatch)
				{
					if (matchStatus == MatchStatus.Failed || matchStatus == MatchStatus.PartiallyFailed)
					{
						matchStatus = MatchStatus.Failed;
						this._queryTranslationContext.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidFilterTargetScope(EngineMessageSeverity.Error, "primary"));
					}
					targetScope = null;
					return matchStatus;
				}
				if (this._projections.SecondaryMembers.Count == 0 && list.Count != 0)
				{
					targetScope = null;
					return MatchStatus.SuperSet;
				}
				QueryMember queryMember = this._projections.PrimaryMembers.Last<QueryMember>();
				DataMemberBuilder dataMemberBuilder;
				this._dsqScopes.TryGetGroupScope(queryMember, true, out dataMemberBuilder);
				if (list.Count == 0)
				{
					targetScope = dataMemberBuilder.Result.Id;
					return MatchStatus.FullMatch;
				}
				matchStatus = this.ValidateTargetByProjections(list, this._projections.SecondaryMembers, out num);
				if (list.Count != 0)
				{
					if (matchStatus == MatchStatus.FullMatch || (matchStatus == MatchStatus.PartiallyFailed && num == 0))
					{
						matchStatus = MatchStatus.SuperSet;
					}
					else
					{
						matchStatus = MatchStatus.Failed;
					}
				}
				if (matchStatus != MatchStatus.FullMatch)
				{
					if (matchStatus == MatchStatus.Failed)
					{
						this._queryTranslationContext.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidFilterTargetScope(EngineMessageSeverity.Error, "secondary"));
					}
					targetScope = null;
					return matchStatus;
				}
				QueryMember queryMember2 = this._projections.SecondaryMembers.Last<QueryMember>();
				DataMemberBuilder dataMemberBuilder2;
				this._dsqScopes.TryGetGroupScope(queryMember2, false, out dataMemberBuilder2);
				this._dsqScopes.TryGetIntersectionScope(dataMemberBuilder, dataMemberBuilder2, out targetScope);
				return MatchStatus.FullMatch;
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000117FC File Offset: 0x0000F9FC
		public bool TryTranslateToDsqScopeForVisualAxis(IReadOnlyList<ResolvedQueryExpression> targets, out Identifier scopeId)
		{
			return this.TryMatchTargetByProjectionsForVisualCalculation(targets, this._projections.PrimaryMembers, true, out scopeId) || this.TryMatchTargetByProjectionsForVisualCalculation(targets, this._projections.SecondaryMembers, false, out scopeId);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00011830 File Offset: 0x0000FA30
		internal bool TryMatchTargetByProjectionsForVisualCalculation(IReadOnlyList<ResolvedQueryExpression> targetsToCompare, IReadOnlyList<QueryMember> members, bool isPrimary, out Identifier id)
		{
			foreach (QueryMember queryMember in members)
			{
				bool flag = true;
				foreach (ResolvedQueryExpression resolvedQueryExpression in targetsToCompare)
				{
					if (!queryMember.ColumnProjectionExpressions.Contains(resolvedQueryExpression, this._targetComparer))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					DataMemberBuilder dataMemberBuilder;
					this._dsqScopes.TryGetGroupScope(queryMember, isPrimary, out dataMemberBuilder);
					id = dataMemberBuilder.Result.Id;
					return true;
				}
			}
			id = null;
			return false;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000118F0 File Offset: 0x0000FAF0
		bool IDsqTargetResolver.TryResolveContextFilter(IReadOnlyList<ResolvedQueryExpression> targets, out DataShapeFilterResolutionResult result)
		{
			result = null;
			DataShapeBuilder dataShapeBuilder = DataShapeBuilder.With("ContextDataShape", null, true, true, DataShapeUsage.Query);
			QuerySortGenerator querySortGenerator;
			if (QuerySortGenerator.TryParseNonMeasureSort(this._queryTranslationContext, out querySortGenerator))
			{
				List<QueryMember> list;
				List<QueryMember> list2;
				if (!this.TryBuildTargetMembers(targets, querySortGenerator, out list, out list2))
				{
					this._queryTranslationContext.SharedContext.ErrorContext.Register(DataShapeGenerationMessages.InvalidFilterTargetScope(EngineMessageSeverity.Error, "primary and secondary"));
					return false;
				}
				QueryProjections queryProjections = new QueryProjections(list, list2, Util.EmptyReadOnlyList<ProjectedDsqExpression>(), Util.EmptyReadOnlyList<ProjectedDsqExpression>(), null, querySortGenerator.HasMeasure);
				if (SemanticQueryDataShapeBuilder.TryBuildContextOnlyDataShape(this._dataShapeBuilderContext, this._queryTranslationContext, queryProjections, dataShapeBuilder))
				{
					result = new DataShapeFilterResolutionResult(this._targetDataShape, dataShapeBuilder.Result);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001199E File Offset: 0x0000FB9E
		private MatchStatus TryGetDefaultScope(DsqFilterType filterType, out Identifier targetScope)
		{
			if (filterType == DsqFilterType.Scope)
			{
				this._dsqScopes.TryGetInnermostScopeId(out targetScope);
				return MatchStatus.FullMatch;
			}
			targetScope = this._targetDataShape;
			return MatchStatus.FullMatch;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000119C4 File Offset: 0x0000FBC4
		private MatchStatus ValidateTargetByProjections(List<ResolvedQueryExpression> targetsToCompare, IReadOnlyList<QueryMember> projections, out int numberOfMatches)
		{
			numberOfMatches = 0;
			if (projections.Count == 0 && targetsToCompare.Count > 0)
			{
				return MatchStatus.Failed;
			}
			MatchStatus matchStatus = MatchStatus.FullMatch;
			foreach (QueryMember queryMember in projections)
			{
				foreach (ResolvedQueryExpression resolvedQueryExpression in queryMember.ColumnProjectionExpressions)
				{
					int num = targetsToCompare.IndexOf(resolvedQueryExpression, this._targetComparer);
					if (num == -1)
					{
						matchStatus = MatchStatus.PartiallyFailed;
					}
					else
					{
						numberOfMatches++;
						targetsToCompare.RemoveAt(num);
					}
				}
			}
			return matchStatus;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00011A78 File Offset: 0x0000FC78
		private bool TryBuildTargetMembers(IReadOnlyList<ResolvedQueryExpression> targets, QuerySortGenerator sort, out List<QueryMember> primaryMembers, out List<QueryMember> secondaryMembers)
		{
			primaryMembers = null;
			secondaryMembers = null;
			ReadOnlyCollection<QueryMember> readOnlyCollection = this._projections.PrimaryMembers.Concat(this._projections.SecondaryMembers).AsReadOnlyCollection<QueryMember>();
			IReadOnlyList<ResolvedQuerySelect> select = this._queryTranslationContext.QueryDefinition.Select;
			List<KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>> list = this.FindNonProjectedTargets(targets, readOnlyCollection, select);
			List<QueryMember> list2;
			if (!this.TryBuildNonProjectedMembers(list, sort, out list2))
			{
				return false;
			}
			primaryMembers = new List<QueryMember>(this._projections.PrimaryMembers.Count + list2.Count);
			foreach (QueryMember queryMember in this._projections.PrimaryMembers)
			{
				QueryMember queryMember2 = this.RebuildGroupForContextFilter(queryMember, sort, select, true);
				QueryMember queryMember3 = this._normalizer.Normalize(queryMember2, sort);
				primaryMembers.Add(queryMember3);
			}
			primaryMembers.AddRange(list2);
			secondaryMembers = new List<QueryMember>(this._projections.SecondaryMembers.Count);
			foreach (QueryMember queryMember4 in this._projections.SecondaryMembers)
			{
				QueryMember queryMember5 = this.RebuildGroupForContextFilter(queryMember4, sort, select, false);
				QueryMember queryMember6 = this._normalizer.Normalize(queryMember5, sort);
				secondaryMembers.Add(queryMember6);
			}
			return true;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		private bool TryBuildNonProjectedMembers([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "NativeReferenceName" })] List<KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>> nonProjectedTargets, QuerySortGenerator sort, out List<QueryMember> nonProjectedMembers)
		{
			nonProjectedMembers = null;
			if (nonProjectedTargets.IsNullOrEmpty<KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>>())
			{
				return false;
			}
			nonProjectedMembers = new List<QueryMember>();
			foreach (KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>> keyValuePair in nonProjectedTargets)
			{
				QueryMember queryMember = this.BuildMember(new KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>[] { keyValuePair }, sort, true);
				nonProjectedMembers.Add(queryMember);
			}
			return true;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00011C64 File Offset: 0x0000FE64
		private QueryMember RebuildGroupForContextFilter(QueryMember queryMember, QuerySortGenerator sort, IReadOnlyList<ResolvedQuerySelect> select, bool isPrimary)
		{
			IEnumerable<KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>> enumerable = queryMember.ColumnProjectionExpressions.Select(delegate(ResolvedQueryExpression c)
			{
				int num = select.FindIndexOf((ResolvedQuerySelect select) => this._targetComparer.Equals(select.Expression, c));
				return Util.ToKeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>(num, new global::System.ValueTuple<ResolvedQueryExpression, string>(c, select[num].NativeReferenceName));
			});
			return this.BuildMember(enumerable, sort, isPrimary);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		private QueryMember BuildMember([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "NativeReferenceName" })] IEnumerable<KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>> projections, QuerySortGenerator sort, bool isPrimary)
		{
			QueryMemberBuilder queryMemberBuilder = new QueryMemberBuilder(this._queryTranslationContext.Expressions, this._queryTranslationContext.SharedContext.ErrorContext, sort, null, isPrimary, SubtotalType.None, this._queryTranslationContext.SourceRefContext, QueryGroupBuilderOptions.AllDisabledOptions, false);
			foreach (KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>> keyValuePair in projections)
			{
				queryMemberBuilder.TryAddProjection(keyValuePair.Value.Item1, keyValuePair.Key, keyValuePair.Value.Item2, false);
			}
			return queryMemberBuilder.ToMember();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00011D4C File Offset: 0x0000FF4C
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "NativeReferenceName" })]
		private List<KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>> FindNonProjectedTargets(IReadOnlyList<ResolvedQueryExpression> targets, ReadOnlyCollection<QueryMember> projections, IReadOnlyList<ResolvedQuerySelect> selects)
		{
			if (projections.Count == 0 && targets.Count > 0)
			{
				return null;
			}
			List<KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>> list = new List<KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>>();
			List<ResolvedQueryExpression> list2 = projections.SelectMany((QueryMember g) => g.ColumnProjectionExpressions).ToList<ResolvedQueryExpression>();
			for (int i = 0; i < targets.Count; i++)
			{
				ResolvedQueryExpression target = targets[i];
				if (!list2.Contains(target, this._targetComparer))
				{
					int num = selects.FindIndexOf((ResolvedQuerySelect select) => this._targetComparer.Equals(select.Expression, target));
					list.Add(new KeyValuePair<int, global::System.ValueTuple<ResolvedQueryExpression, string>>(num, new global::System.ValueTuple<ResolvedQueryExpression, string>(target, (num < 0) ? null : selects[num].NativeReferenceName)));
				}
			}
			return list;
		}

		// Token: 0x0400028E RID: 654
		private readonly QueryTranslationContext _queryTranslationContext;

		// Token: 0x0400028F RID: 655
		private readonly DataShapeBuilderContext _dataShapeBuilderContext;

		// Token: 0x04000290 RID: 656
		private readonly DsqScopeLookup _dsqScopes;

		// Token: 0x04000291 RID: 657
		private readonly QueryProjections _projections;

		// Token: 0x04000292 RID: 658
		private readonly string _targetDataShape;

		// Token: 0x04000293 RID: 659
		private readonly TargetComparer _targetComparer;

		// Token: 0x04000294 RID: 660
		private readonly ContextFilterQueryMemberNormalizer _normalizer;
	}
}
