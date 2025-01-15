using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000060 RID: 96
	[ImmutableObject(false)]
	internal sealed class DsqFilterGenerator
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x00010AF3 File Offset: 0x0000ECF3
		internal DsqFilterGenerator(IDsqTargetResolver dsqFilterTargetResolver, DataShapeGenerationErrorContext errorContext, IDsqFilterConditionGenerator dsqFilterConditionGenerator, ISet<DsqFilterType> suppressedFilterTypes)
		{
			this._dsqFilterTargetScopeResolver = dsqFilterTargetResolver;
			this._errorContext = errorContext;
			this._suppressedFilterTypes = suppressedFilterTypes ?? Util.EmptySet<DsqFilterType>();
			this._conditionGenerator = dsqFilterConditionGenerator;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00010B24 File Offset: 0x0000ED24
		internal List<GeneratedFilter> Generate(ResolvedQueryExpression expr, ExpressionContext expressionContext, IReadOnlyList<ResolvedQueryExpression> targets, out FilterUsageKind filterUsageKind)
		{
			IReadOnlyList<ResolvedQueryExpression> readOnlyList = targets ?? Util.EmptyReadOnlyCollection<ResolvedQueryExpression>();
			List<GeneratedFilterCondition> list = this._conditionGenerator.Generate(expr, expressionContext, targets, out filterUsageKind);
			List<GeneratedFilter> list2 = new List<GeneratedFilter>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(this.GenerateFilter(list[i], readOnlyList));
			}
			return list2;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00010B80 File Offset: 0x0000ED80
		private GeneratedFilter GenerateFilter(GeneratedFilterCondition resultCondition, IReadOnlyList<ResolvedQueryExpression> targets)
		{
			FilterConversionStatus conversionStatus = resultCondition.ConversionStatus;
			if (conversionStatus == FilterConversionStatus.Failed)
			{
				return GeneratedFilter.Empty;
			}
			DsqFilterType? filterType = resultCondition.FilterType;
			if (conversionStatus == FilterConversionStatus.Ignored || (filterType != null && this._suppressedFilterTypes.Contains(filterType.Value)))
			{
				return GeneratedFilter.Ignored;
			}
			FilterCondition condition = resultCondition.Condition;
			Identifier identifier;
			MatchStatus matchStatus = this._dsqFilterTargetScopeResolver.TryTranslateToDsqScopeForFilter(targets, filterType.Value, out identifier);
			if (matchStatus == MatchStatus.Failed)
			{
				return GeneratedFilter.Empty;
			}
			if (matchStatus == MatchStatus.SuperSet)
			{
				return this.CreateContextFilterCondition(targets);
			}
			return new GeneratedFilter(condition, FilterConversionStatus.Succeeded, filterType, identifier);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00010C0C File Offset: 0x0000EE0C
		private GeneratedFilter CreateContextFilterCondition(IReadOnlyList<ResolvedQueryExpression> targets)
		{
			FilterConversionStatus filterConversionStatus = FilterConversionStatus.Failed;
			DsqFilterType dsqFilterType;
			Identifier identifier;
			FilterCondition filterCondition;
			if (this.TryBuildContextFilterCondition(targets, out dsqFilterType, out identifier, out filterCondition))
			{
				if (this._contextFilterConditionTargets == null)
				{
					filterConversionStatus = FilterConversionStatus.Succeeded;
					this._contextFilterConditionTargets = targets;
				}
				else
				{
					if (this._contextFilterConditionTargets.Count != targets.Count || !this._contextFilterConditionTargets.ContainsAll(targets))
					{
						this._errorContext.Register(DataShapeGenerationMessages.DifferentContextDataShapeFilterTargets(EngineMessageSeverity.Error));
						return GeneratedFilter.Empty;
					}
					return GeneratedFilter.Ignored;
				}
			}
			return new GeneratedFilter(filterCondition, filterConversionStatus, new DsqFilterType?(dsqFilterType), identifier);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00010C8C File Offset: 0x0000EE8C
		private bool TryBuildContextFilterCondition(IReadOnlyList<ResolvedQueryExpression> targets, out DsqFilterType filterType, out Identifier filterTarget, out FilterCondition condition)
		{
			DataShapeFilterResolutionResult dataShapeFilterResolutionResult;
			if (this._dsqFilterTargetScopeResolver.TryResolveContextFilter(targets, out dataShapeFilterResolutionResult))
			{
				filterType = DsqFilterType.Context;
				filterTarget = dataShapeFilterResolutionResult.TargetScope;
				condition = new ContextFilterCondition
				{
					DataShape = dataShapeFilterResolutionResult.ContextDataShape
				};
				return true;
			}
			filterType = DsqFilterType.DataShape;
			filterTarget = null;
			condition = null;
			return false;
		}

		// Token: 0x04000262 RID: 610
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x04000263 RID: 611
		private readonly IDsqTargetResolver _dsqFilterTargetScopeResolver;

		// Token: 0x04000264 RID: 612
		private readonly ISet<DsqFilterType> _suppressedFilterTypes;

		// Token: 0x04000265 RID: 613
		private readonly IDsqFilterConditionGenerator _conditionGenerator;

		// Token: 0x04000266 RID: 614
		private IReadOnlyList<ResolvedQueryExpression> _contextFilterConditionTargets;
	}
}
