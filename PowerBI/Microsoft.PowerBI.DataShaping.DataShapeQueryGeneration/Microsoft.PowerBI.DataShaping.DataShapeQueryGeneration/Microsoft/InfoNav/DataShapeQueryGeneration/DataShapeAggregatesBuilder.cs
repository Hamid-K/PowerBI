using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000007 RID: 7
	internal sealed class DataShapeAggregatesBuilder<TParent> : DsqExpressionAggregatesVisitorBase
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020C8 File Offset: 0x000002C8
		internal DataShapeAggregatesBuilder(ICalculationContainer<TParent> container, DataShapeIdGenerator ids, string id, bool projectedItemIsContextOnly, IReadOnlyDictionary<int, IProjectionBinding> selectBindings, DataShapeExpressionsAxisGroupingBuilder groupingBuilder, int? aggregateGroupIndex, int? subtotalIndex, IReadOnlyList<DsqExpressionAggregateKind> suppressedAggregates, bool suppressJoinPredicate, AggregateContextOnlyImpact aggregateContextOnlyImpact, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "DynamicFormatId", "DynamicFormatCultureId" })] IDictionary<string, global::System.ValueTuple<string, string>> measureDynamicFormattingIds)
		{
			this._container = container;
			this._ids = ids;
			this._projectedItemId = id;
			this._projectedItemIsContextOnly = projectedItemIsContextOnly;
			this._selectBindings = selectBindings;
			this._aggregateGroupIdx = aggregateGroupIndex;
			this._subtotalIdx = subtotalIndex;
			this._groupingBuilder = groupingBuilder;
			this._suppressedAggregates = suppressedAggregates;
			this._suppressJoinPredicate = suppressJoinPredicate;
			this._aggregateContextOnlyImpact = aggregateContextOnlyImpact;
			this._measureDynamicFormattingIds = measureDynamicFormattingIds;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002138 File Offset: 0x00000338
		internal override void Visit(DsqExpressionCountAggregate aggregate)
		{
			if (!this._suppressedAggregates.HasCount())
			{
				this.AddCountCalculation();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002150 File Offset: 0x00000350
		internal override void Visit(DsqExpressionMaxAggregate aggregate)
		{
			if (!this._suppressedAggregates.HasMax())
			{
				int? aggregateGroupIdx = this._aggregateGroupIdx;
				int? primaryGroupIndex = aggregate.PrimaryGroupIndex;
				if ((aggregateGroupIdx.GetValueOrDefault() == primaryGroupIndex.GetValueOrDefault()) & (aggregateGroupIdx != null == (primaryGroupIndex != null)))
				{
					DataShapeBindingAggregateContainer aggregate2 = aggregate.Aggregate;
					Func<ExpressionNode, FunctionUsageKind, ExpressionNode> func;
					if ((func = DataShapeAggregatesBuilder<TParent>.<>O.<0>__Max) == null)
					{
						func = (DataShapeAggregatesBuilder<TParent>.<>O.<0>__Max = new Func<ExpressionNode, FunctionUsageKind, ExpressionNode>(ExpressionNodeBuilder.Max));
					}
					this.AddAggregateCalculation(aggregate2, func, aggregate.SelectIndex);
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C8 File Offset: 0x000003C8
		internal override void Visit(DsqExpressionMinAggregate aggregate)
		{
			if (!this._suppressedAggregates.HasMin())
			{
				int? aggregateGroupIdx = this._aggregateGroupIdx;
				int? primaryGroupIndex = aggregate.PrimaryGroupIndex;
				if ((aggregateGroupIdx.GetValueOrDefault() == primaryGroupIndex.GetValueOrDefault()) & (aggregateGroupIdx != null == (primaryGroupIndex != null)))
				{
					DataShapeBindingAggregateContainer aggregate2 = aggregate.Aggregate;
					Func<ExpressionNode, FunctionUsageKind, ExpressionNode> func;
					if ((func = DataShapeAggregatesBuilder<TParent>.<>O.<1>__Min) == null)
					{
						func = (DataShapeAggregatesBuilder<TParent>.<>O.<1>__Min = new Func<ExpressionNode, FunctionUsageKind, ExpressionNode>(ExpressionNodeBuilder.Min));
					}
					this.AddAggregateCalculation(aggregate2, func, aggregate.SelectIndex);
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002240 File Offset: 0x00000440
		internal override void Visit(DsqExpressionMedianAggregate aggregate)
		{
			if (!this._suppressedAggregates.HasMedian())
			{
				int? aggregateGroupIdx = this._aggregateGroupIdx;
				int? primaryGroupIndex = aggregate.PrimaryGroupIndex;
				if ((aggregateGroupIdx.GetValueOrDefault() == primaryGroupIndex.GetValueOrDefault()) & (aggregateGroupIdx != null == (primaryGroupIndex != null)))
				{
					DataShapeBindingAggregateContainer aggregate2 = aggregate.Aggregate;
					Func<ExpressionNode, FunctionUsageKind, ExpressionNode> func;
					if ((func = DataShapeAggregatesBuilder<TParent>.<>O.<2>__Median) == null)
					{
						func = (DataShapeAggregatesBuilder<TParent>.<>O.<2>__Median = new Func<ExpressionNode, FunctionUsageKind, ExpressionNode>(ExpressionNodeBuilder.Median));
					}
					this.AddAggregateCalculation(aggregate2, func, aggregate.SelectIndex);
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022B8 File Offset: 0x000004B8
		internal override void Visit(DsqExpressionAverageAggregate aggregate)
		{
			if (!this._suppressedAggregates.HasAverage())
			{
				int? aggregateGroupIdx = this._aggregateGroupIdx;
				int? primaryGroupIndex = aggregate.PrimaryGroupIndex;
				if ((aggregateGroupIdx.GetValueOrDefault() == primaryGroupIndex.GetValueOrDefault()) & (aggregateGroupIdx != null == (primaryGroupIndex != null)))
				{
					DataShapeBindingAggregateContainer aggregate2 = aggregate.Aggregate;
					Func<ExpressionNode, FunctionUsageKind, ExpressionNode> func;
					if ((func = DataShapeAggregatesBuilder<TParent>.<>O.<3>__Average) == null)
					{
						func = (DataShapeAggregatesBuilder<TParent>.<>O.<3>__Average = new Func<ExpressionNode, FunctionUsageKind, ExpressionNode>(ExpressionNodeBuilder.Average));
					}
					this.AddAggregateCalculation(aggregate2, func, aggregate.SelectIndex);
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002330 File Offset: 0x00000530
		internal override void Visit(DsqExpressionPercentileAggregate aggregate)
		{
			if (!this._suppressedAggregates.HasPercentile())
			{
				int? aggregateGroupIdx = this._aggregateGroupIdx;
				int? primaryGroupIndex = aggregate.PrimaryGroupIndex;
				if ((aggregateGroupIdx.GetValueOrDefault() == primaryGroupIndex.GetValueOrDefault()) & (aggregateGroupIdx != null == (primaryGroupIndex != null)))
				{
					DataShapeBindingPercentileAggregate pctDef = aggregate.Aggregate.Percentile;
					this.AddAggregateCalculation(aggregate.Aggregate, delegate(ExpressionNode expr, FunctionUsageKind usageKind)
					{
						if (!pctDef.Exclusive)
						{
							return expr.PercentileInc(pctDef.K, usageKind);
						}
						return expr.PercentileExc(pctDef.K, usageKind);
					}, aggregate.SelectIndex);
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023B0 File Offset: 0x000005B0
		internal override void Visit(DsqExpressionSubtotalAggregate aggregate)
		{
			if (!this._suppressedAggregates.HasSubtotal())
			{
				this.AddSubtotalCalculation();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023C8 File Offset: 0x000005C8
		private void AddAggregateCalculation(DataShapeBindingAggregateContainer aggregateBinding, Func<ExpressionNode, FunctionUsageKind, ExpressionNode> buildCalcExpression, int? selectIndex)
		{
			bool flag = aggregateBinding.Scope != null;
			string text;
			if (this.TryGetTargetCalculationId(aggregateBinding, flag, selectIndex, out text))
			{
				string text2 = this._ids.CreateAggregateId();
				this.UpdateSelectBindingWithAggregateCalculationInfo(aggregateBinding, text2);
				this._container.WithCalculation(text2, buildCalcExpression(text.StructureReference(), FunctionUsageKind.Unassigned), this._suppressJoinPredicate, new bool?(aggregateBinding.RespectInstanceFilters), null, false);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002438 File Offset: 0x00000638
		private void UpdateSelectBindingWithAggregateCalculationInfo(DataShapeBindingAggregateContainer aggregateBinding, string aggregateId)
		{
			bool flag = aggregateBinding.Max != null && new DataShapeBindingAggregateContainer
			{
				Max = aggregateBinding.Max
			} == aggregateBinding;
			bool flag2 = aggregateBinding.Min != null && new DataShapeBindingAggregateContainer
			{
				Min = aggregateBinding.Min
			} == aggregateBinding;
			foreach (IProjectionBinding projectionBinding in this._selectBindings.Values)
			{
				AggregateDescriptor orAddAggregateDescriptor = DataShapeAggregatesBuilder<TParent>.GetOrAddAggregateDescriptor(projectionBinding, aggregateBinding);
				orAddAggregateDescriptor.Ids = this.AppendAggregateId(orAddAggregateDescriptor.Ids, aggregateId);
				if (this._groupingBuilder != null)
				{
					DataMemberBuilder dataMemberBuilder = this._container as DataMemberBuilder;
					if (dataMemberBuilder != null)
					{
						string value = dataMemberBuilder.Result.Id.Value;
						this._groupingBuilder.WithAggregates(value, orAddAggregateDescriptor.Ids);
					}
				}
				if (flag)
				{
					projectionBinding.Max = this.AppendAggregateId(projectionBinding.Max, aggregateId);
				}
				if (flag2)
				{
					projectionBinding.Min = this.AppendAggregateId(projectionBinding.Min, aggregateId);
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002568 File Offset: 0x00000768
		private bool TryGetTargetCalculationId(DataShapeBindingAggregateContainer aggregateBinding, bool isScopedAggregate, int? targetSelectIndex, out string targetCalcId)
		{
			targetCalcId = this._projectedItemId;
			IProjectionBinding projectionBinding;
			if (targetSelectIndex != null && this._selectBindings.TryGetValue(targetSelectIndex.Value, out projectionBinding))
			{
				targetCalcId = projectionBinding.Value;
				if (isScopedAggregate)
				{
					SelectBinding selectBinding = projectionBinding as SelectBinding;
					if (selectBinding != null && selectBinding.Kind == SelectKind.Group)
					{
						return true;
					}
					if (projectionBinding.Subtotal.IsNullOrEmpty<string>() || aggregateBinding.Scope.SecondaryDepth != null)
					{
						return true;
					}
					int num = DataShapeBuilderExtensions.ComputeAggregateIndex(aggregateBinding.Scope.PrimaryDepth, 0, 0, 0);
					if (num < projectionBinding.Subtotal.Count)
					{
						if (projectionBinding.Subtotal[num] == null)
						{
							targetCalcId = null;
							return false;
						}
						targetCalcId = projectionBinding.Subtotal[num];
					}
				}
			}
			return true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002630 File Offset: 0x00000830
		private void AddCountCalculation()
		{
			string text = this._ids.CreateAggregateId();
			foreach (IProjectionBinding projectionBinding in this._selectBindings.Values)
			{
				projectionBinding.Count = this.AppendAggregateId(projectionBinding.Count, text);
			}
			this._container.WithCalculation(text, this._projectedItemId.StructureReference().Count(FunctionUsageKind.Unassigned), this._suppressJoinPredicate, null, null, false);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026D0 File Offset: 0x000008D0
		private void AddSubtotalCalculation()
		{
			string text = this._ids.CreateAggregateId();
			ICalculationContainer<TParent> container = this._container;
			string text2 = text;
			Expression expression = this._projectedItemId.StructureReference().Subtotal(FunctionUsageKind.Unassigned);
			bool suppressJoinPredicate = this._suppressJoinPredicate;
			bool projectedItemIsContextOnly = this._projectedItemIsContextOnly;
			container.WithCalculation(text2, expression, suppressJoinPredicate, null, null, projectedItemIsContextOnly);
			foreach (IProjectionBinding projectionBinding in this._selectBindings.Values)
			{
				if (this._aggregateContextOnlyImpact != AggregateContextOnlyImpact.InsideAContextOnlyMember)
				{
					if (this._aggregateContextOnlyImpact == AggregateContextOnlyImpact.NewInnermostIntersection)
					{
						projectionBinding.Value = text;
					}
					else
					{
						projectionBinding.Subtotal = this.AddAggregateId(projectionBinding.Subtotal, text);
					}
				}
				if (projectionBinding.DynamicFormat != null)
				{
					global::System.ValueTuple<string, string> valueTuple;
					this._measureDynamicFormattingIds.TryGetValue(this._projectedItemId, out valueTuple);
					string text3 = null;
					string text4 = null;
					this.AddDynamicFormattingSubtotalCalculation(ref text3, valueTuple.Item1);
					this.AddDynamicFormattingSubtotalCalculation(ref text4, valueTuple.Item2);
					if (this._aggregateContextOnlyImpact != AggregateContextOnlyImpact.InsideAContextOnlyMember)
					{
						if (this._aggregateContextOnlyImpact == AggregateContextOnlyImpact.NewInnermostIntersection)
						{
							projectionBinding.DynamicFormat.Format = text3;
							projectionBinding.DynamicFormat.Culture = text4;
						}
						else
						{
							projectionBinding.SubtotalDynamicFormat = projectionBinding.SubtotalDynamicFormat ?? new List<DynamicFormatBinding>();
							this.AddDynamicFormattingBinding(projectionBinding.SubtotalDynamicFormat, text3, text4);
						}
					}
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002838 File Offset: 0x00000A38
		private void AddDynamicFormattingSubtotalCalculation(ref string calcId, string dynamicFormattingDataIdentifier)
		{
			if (dynamicFormattingDataIdentifier != null)
			{
				calcId = this._ids.CreateAggregateId();
				this._container.WithCalculation(calcId, dynamicFormattingDataIdentifier.StructureReference().Subtotal(FunctionUsageKind.Unassigned), true, null, null, false);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002880 File Offset: 0x00000A80
		private void AddDynamicFormattingBinding(List<DynamicFormatBinding> list, string dynamicFormatId, string dynamicCultureId)
		{
			int num = ((this._subtotalIdx != null) ? this._subtotalIdx.Value : ((this._aggregateGroupIdx != null) ? this._aggregateGroupIdx.Value : 0));
			list.SetAtExtendedIndex(num, new DynamicFormatBinding
			{
				Format = dynamicFormatId,
				Culture = dynamicCultureId
			});
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028E0 File Offset: 0x00000AE0
		private List<string> AddAggregateId(List<string> list, string id)
		{
			Util.EnsureList<string>(ref list);
			int num = ((this._subtotalIdx != null) ? this._subtotalIdx.Value : ((this._aggregateGroupIdx != null) ? this._aggregateGroupIdx.Value : 0));
			list.SetAtExtendedIndex(num, id);
			return list;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002935 File Offset: 0x00000B35
		private List<string> AppendAggregateId(List<string> list, string id)
		{
			Util.EnsureList<string>(ref list);
			list.Add(id);
			return list;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002948 File Offset: 0x00000B48
		private static AggregateDescriptor GetOrAddAggregateDescriptor(IProjectionBinding selectBinding, DataShapeBindingAggregateContainer aggregateBinding)
		{
			selectBinding.Aggregates = selectBinding.Aggregates ?? new List<AggregateDescriptor>();
			QueryBindingDescriptorAggregateContainer queryBindingDescriptorAggregateContainer = aggregateBinding.CreateQueryBindingDescriptorAggregateContainer();
			AggregateDescriptor aggregateDescriptor = new AggregateDescriptor
			{
				Aggregate = queryBindingDescriptorAggregateContainer
			};
			selectBinding.Aggregates.Add(aggregateDescriptor);
			return aggregateDescriptor;
		}

		// Token: 0x0400002A RID: 42
		private readonly ICalculationContainer<TParent> _container;

		// Token: 0x0400002B RID: 43
		private readonly DataShapeIdGenerator _ids;

		// Token: 0x0400002C RID: 44
		private readonly string _projectedItemId;

		// Token: 0x0400002D RID: 45
		private readonly bool _projectedItemIsContextOnly;

		// Token: 0x0400002E RID: 46
		private readonly IReadOnlyDictionary<int, IProjectionBinding> _selectBindings;

		// Token: 0x0400002F RID: 47
		private readonly DataShapeExpressionsAxisGroupingBuilder _groupingBuilder;

		// Token: 0x04000030 RID: 48
		private readonly int? _aggregateGroupIdx;

		// Token: 0x04000031 RID: 49
		private readonly int? _subtotalIdx;

		// Token: 0x04000032 RID: 50
		private readonly IReadOnlyList<DsqExpressionAggregateKind> _suppressedAggregates;

		// Token: 0x04000033 RID: 51
		private readonly bool _suppressJoinPredicate;

		// Token: 0x04000034 RID: 52
		private readonly AggregateContextOnlyImpact _aggregateContextOnlyImpact;

		// Token: 0x04000035 RID: 53
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "DynamicFormatId", "DynamicFormatCultureId" })]
		private readonly IDictionary<string, global::System.ValueTuple<string, string>> _measureDynamicFormattingIds;

		// Token: 0x02000118 RID: 280
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004AF RID: 1199
			public static Func<ExpressionNode, FunctionUsageKind, ExpressionNode> <0>__Max;

			// Token: 0x040004B0 RID: 1200
			public static Func<ExpressionNode, FunctionUsageKind, ExpressionNode> <1>__Min;

			// Token: 0x040004B1 RID: 1201
			public static Func<ExpressionNode, FunctionUsageKind, ExpressionNode> <2>__Median;

			// Token: 0x040004B2 RID: 1202
			public static Func<ExpressionNode, FunctionUsageKind, ExpressionNode> <3>__Average;
		}
	}
}
