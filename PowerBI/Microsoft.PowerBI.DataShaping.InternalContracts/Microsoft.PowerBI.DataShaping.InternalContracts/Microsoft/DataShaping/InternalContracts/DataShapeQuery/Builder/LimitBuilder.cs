using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000FA RID: 250
	internal sealed class LimitBuilder<TParent> : BuilderBase<Limit, TParent>
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0000EA97 File Offset: 0x0000CC97
		internal LimitBuilder(TParent parent, Limit activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0000EAA1 File Offset: 0x0000CCA1
		public Identifier Id
		{
			get
			{
				return base.ActiveObject.Id;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000EAAE File Offset: 0x0000CCAE
		public LimitBuilder<TParent> WithTelemetryId(int? telemetryId)
		{
			base.ActiveObject.TelemetryId = telemetryId;
			return this;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000EABD File Offset: 0x0000CCBD
		public LimitBuilder<TParent> WithTargets(params Expression[] targets)
		{
			if (base.ActiveObject.Targets == null)
			{
				base.ActiveObject.Targets = new List<Expression>();
			}
			base.ActiveObject.Targets.AddRange(targets);
			return this;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		public LimitBuilder<TParent> WithTop(int count, long? skip = null, bool? isStrict = null)
		{
			Limit activeObject = base.ActiveObject;
			Limit activeObject2 = base.ActiveObject;
			TopLimitOperator topLimitOperator = new TopLimitOperator();
			topLimitOperator.Count = count;
			topLimitOperator.Skip = skip;
			bool? flag = isStrict;
			topLimitOperator.IsStrict = ((flag != null) ? flag.GetValueOrDefault() : null);
			activeObject2.Operator = topLimitOperator;
			return this;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0000EB48 File Offset: 0x0000CD48
		public LimitBuilder<TParent> WithSample(int count, bool? preserveKeyPoints = null)
		{
			Limit activeObject = base.ActiveObject;
			SampleLimitOperator sampleLimitOperator = new SampleLimitOperator();
			sampleLimitOperator.Count = count;
			bool? flag = preserveKeyPoints;
			sampleLimitOperator.PreserveKeyPoints = ((flag != null) ? flag.GetValueOrDefault() : null);
			activeObject.Operator = sampleLimitOperator;
			return this;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000EB92 File Offset: 0x0000CD92
		public LimitBuilder<TParent> WithFirst()
		{
			base.ActiveObject.Operator = new FirstLimitOperator
			{
				Count = 1
			};
			return this;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000EBB1 File Offset: 0x0000CDB1
		public LimitBuilder<TParent> WithLast()
		{
			base.ActiveObject.Operator = new LastLimitOperator
			{
				Count = 1
			};
			return this;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public LimitBuilder<TParent> WithBottom(int count)
		{
			base.ActiveObject.Operator = new BottomLimitOperator
			{
				Count = count
			};
			return this;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public LimitBuilder<TParent> WithBinnedLineSample(int count, int minPointsPerSeries, int maxPointsPerSeries, int maxDynamicSeriesCount, List<Expression> measures, Expression primaryScalarKey = null, int? warningCount = null)
		{
			base.ActiveObject.Operator = new BinnedLineSampleLimitOperator
			{
				Count = count,
				MinPointsPerSeries = minPointsPerSeries,
				MaxPointsPerSeries = maxPointsPerSeries,
				MaxDynamicSeriesCount = maxDynamicSeriesCount,
				Measures = measures,
				PrimaryScalarKey = primaryScalarKey,
				WarningCount = warningCount
			};
			return this;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0000EC57 File Offset: 0x0000CE57
		public LimitBuilder<TParent> WithOverlappingPointsSample(int count, LimitPlotAxis x, LimitPlotAxis y)
		{
			return this.WithOverlappingPointsSample(count, (x != null) ? x.Key : null, (y != null) ? y.Key : null, (x != null) ? x.Transform : DataReductionPlotAxisTransform.None, (y != null) ? y.Transform : DataReductionPlotAxisTransform.None);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public LimitBuilder<TParent> WithOverlappingPointsSample(int count, Expression x, Expression y, DataReductionPlotAxisTransform xTransform = DataReductionPlotAxisTransform.None, DataReductionPlotAxisTransform yTransform = DataReductionPlotAxisTransform.None)
		{
			Limit activeObject = base.ActiveObject;
			OverlappingPointsSampleLimitOperator overlappingPointsSampleLimitOperator = new OverlappingPointsSampleLimitOperator();
			overlappingPointsSampleLimitOperator.Count = count;
			LimitPlotAxis limitPlotAxis;
			if (x == null)
			{
				limitPlotAxis = null;
			}
			else
			{
				LimitPlotAxis limitPlotAxis2 = new LimitPlotAxis();
				limitPlotAxis2.Key = x;
				limitPlotAxis = limitPlotAxis2;
				limitPlotAxis2.Transform = xTransform;
			}
			overlappingPointsSampleLimitOperator.X = limitPlotAxis;
			LimitPlotAxis limitPlotAxis3;
			if (y == null)
			{
				limitPlotAxis3 = null;
			}
			else
			{
				LimitPlotAxis limitPlotAxis4 = new LimitPlotAxis();
				limitPlotAxis4.Key = y;
				limitPlotAxis3 = limitPlotAxis4;
				limitPlotAxis4.Transform = yTransform;
			}
			overlappingPointsSampleLimitOperator.Y = limitPlotAxis3;
			activeObject.Operator = overlappingPointsSampleLimitOperator;
			return this;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		public TopNPerLevelLimitBuilder<LimitBuilder<TParent>> WithTopNPerLevel(int count)
		{
			TopNPerLevelLimitOperator topNPerLevelLimitOperator = new TopNPerLevelLimitOperator
			{
				Count = count
			};
			base.ActiveObject.Operator = topNPerLevelLimitOperator;
			return new TopNPerLevelLimitBuilder<LimitBuilder<TParent>>(this, topNPerLevelLimitOperator);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000ED2E File Offset: 0x0000CF2E
		public LimitBuilder<TParent> WithWindow(int count, List<RestartToken> restartTokens = null, RestartMatchingBehavior? restartMatchingBehavior = null)
		{
			base.ActiveObject.Operator = new WindowLimitOperator
			{
				Count = count,
				RestartTokens = restartTokens,
				RestartMatchingBehavior = restartMatchingBehavior
			};
			return this;
		}
	}
}
