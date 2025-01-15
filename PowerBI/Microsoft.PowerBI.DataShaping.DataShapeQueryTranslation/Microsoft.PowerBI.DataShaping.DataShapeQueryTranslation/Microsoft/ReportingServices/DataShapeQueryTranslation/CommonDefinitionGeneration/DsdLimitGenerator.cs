using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration
{
	// Token: 0x02000124 RID: 292
	internal sealed class DsdLimitGenerator : LimitVisitor<DataLimitOperator>
	{
		// Token: 0x06000B04 RID: 2820 RVA: 0x0002B07F File Offset: 0x0002927F
		private DsdLimitGenerator(ExpressionTable expressionTableForScopes, ExpressionTable expressionTableForLimitOverrides, BatchSubtotalAnnotations subtotalAnnotations, ScopeTree scopeTree, DsdExpressionGenerator.GetTableIdForFieldReference getTableIdForField, DsdExpressionGenerator.GetTableIdForTransformColumn getTableIdForTransform, LimitOverride limitOverride)
		{
			this.m_expressionTableForScopes = expressionTableForScopes;
			this.m_expressionTableForLimitOverrides = expressionTableForLimitOverrides;
			this.m_subtotalAnnotations = subtotalAnnotations;
			this.m_scopeTree = scopeTree;
			this.m_getTableIdForField = getTableIdForField;
			this.m_getTableIdForTransform = getTableIdForTransform;
			this.m_limitOverride = limitOverride;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002B0BC File Offset: 0x000292BC
		internal static DataLimit Generate(Limit limit, ExpressionTable expressionTableForScopes, ExpressionTable expressionTableForLimitOverrides, BatchSubtotalAnnotations subtotalAnnotations, ScopeTree scopeTree, DsdExpressionGenerator.GetTableIdForFieldReference getTableIdForField, DsdExpressionGenerator.GetTableIdForTransformColumn getTableIdForTransform, LimitOverride limitOverride)
		{
			if (limitOverride != null && limitOverride.ShouldRemove)
			{
				return null;
			}
			return new DsdLimitGenerator(expressionTableForScopes, expressionTableForLimitOverrides, subtotalAnnotations, scopeTree, getTableIdForField, getTableIdForTransform, limitOverride).Build(limit);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002B0E3 File Offset: 0x000292E3
		internal static DataWindow GenerateDataWindow(Limit limit, ExpressionTable expressionTableForScopes, ExpressionTable expressionTableForLimitOverrides, BatchSubtotalAnnotations subtotalAnnotations, ScopeTree scopeTree, DsdExpressionGenerator.GetTableIdForFieldReference getTableIdForField, DsdExpressionGenerator.GetTableIdForTransformColumn getTableIdForTransform, LimitOverride limitOverride, BuildRestartDefinition buildRestartDefinition)
		{
			if (limitOverride != null && limitOverride.ShouldRemove)
			{
				return null;
			}
			return new DsdLimitGenerator(expressionTableForScopes, expressionTableForLimitOverrides, subtotalAnnotations, scopeTree, getTableIdForField, getTableIdForTransform, limitOverride).BuildDataWindow(limit, buildRestartDefinition);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002B10C File Offset: 0x0002930C
		private DataLimit Build(Limit limit)
		{
			DataLimitOperator dataLimitOperator = limit.Operator.Accept<DataLimitOperator>(this);
			if (dataLimitOperator == null)
			{
				return null;
			}
			IScope resolvedScope = limit.GetInnermostTarget().GetResolvedScope(this.m_expressionTableForScopes);
			global::System.ValueTuple<IList<string>, IList<string>> valueTuple = this.BuildTargetsAndAppliesTo(limit, resolvedScope);
			return new DataLimit
			{
				Id = limit.Id.Value,
				Operator = dataLimitOperator,
				Targets = valueTuple.Item1,
				AppliesTo = valueTuple.Item2,
				Within = this.BuildWithin(limit.Within),
				TelemetryId = limit.TelemetryId
			};
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002B19C File Offset: 0x0002939C
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Targets", "AppliesTo" })]
		private global::System.ValueTuple<IList<string>, IList<string>> BuildTargetsAndAppliesTo(Limit limit, IScope innermostTarget)
		{
			if (innermostTarget is Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataIntersection)
			{
				return new global::System.ValueTuple<IList<string>, IList<string>>(innermostTarget.Id.Value.ListWrap<string>(), innermostTarget.Id.Value.ListWrap<string>());
			}
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> groupScopesFromTargets = limit.GetGroupScopesFromTargets(this.m_expressionTableForScopes);
			List<string> list = new List<string>(groupScopesFromTargets.Count + 1);
			List<string> list2 = new List<string>(groupScopesFromTargets.Count * 2);
			list.Add(innermostTarget.Id.Value);
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember in groupScopesFromTargets)
			{
				this.AddSubtotalTarget(dataMember, list);
				list2.Add(dataMember.Id.Value);
				this.AddSubtotalTarget(dataMember, list2);
			}
			return new global::System.ValueTuple<IList<string>, IList<string>>(list, list2);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002B274 File Offset: 0x00029474
		private string BuildWithin(Expression expression)
		{
			return expression.GetResolvedScope(this.m_expressionTableForScopes).Id.Value;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002B28C File Offset: 0x0002948C
		private void AddSubtotalTarget(IScope scope, IList<string> dsdTargets)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			IList<IIdentifiable> list;
			if (this.m_subtotalAnnotations.TryGetSubtotalAnnotation(scope, out batchSubtotalAnnotation) && batchSubtotalAnnotation.Usage.IsIncludeInOutput() && this.m_subtotalAnnotations.TryGetSubtotalAnnotationSources(batchSubtotalAnnotation, out list))
			{
				foreach (IIdentifiable identifiable in list)
				{
					dsdTargets.Add(identifiable.Id.Value);
				}
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002B30C File Offset: 0x0002950C
		internal override DataLimitOperator Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.TopLimitOperator limitOperator)
		{
			Candidate<int> count = limitOperator.Count;
			int? warningCount = limitOperator.WarningCount;
			return this.CreateLimitOperator<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.TopLimitOperator>(count, (warningCount != null) ? warningCount.GetValueOrDefault() : null);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002B344 File Offset: 0x00029544
		internal override DataLimitOperator Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.SampleLimitOperator limitOperator)
		{
			Candidate<int> count = limitOperator.Count;
			int? warningCount = limitOperator.WarningCount;
			return this.CreateLimitOperator<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.SampleLimitOperator>(count, (warningCount != null) ? warningCount.GetValueOrDefault() : null);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002B37C File Offset: 0x0002957C
		internal override DataLimitOperator Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.BinnedLineSampleLimitOperator limitOperator)
		{
			Candidate<int> count = limitOperator.Count;
			int? warningCount = limitOperator.WarningCount;
			return this.CreateLimitOperator<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.BinnedLineSampleLimitOperator>(count, (warningCount != null) ? warningCount.GetValueOrDefault() : null);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002B3B4 File Offset: 0x000295B4
		internal override DataLimitOperator Visit(FirstLimitOperator limitOperator)
		{
			Candidate<int> count = limitOperator.Count;
			int? warningCount = limitOperator.WarningCount;
			return this.CreateLimitOperator<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.TopLimitOperator>(count, (warningCount != null) ? warningCount.GetValueOrDefault() : null);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002B3EC File Offset: 0x000295EC
		internal override DataLimitOperator Visit(LastLimitOperator limitOperator)
		{
			Candidate<int> count = limitOperator.Count;
			int? warningCount = limitOperator.WarningCount;
			return this.CreateLimitOperator<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.TopLimitOperator>(count, (warningCount != null) ? warningCount.GetValueOrDefault() : null);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002B424 File Offset: 0x00029624
		internal override DataLimitOperator Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.BottomLimitOperator limitOperator)
		{
			Candidate<int> count = limitOperator.Count;
			int? warningCount = limitOperator.WarningCount;
			return this.CreateLimitOperator<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.BottomLimitOperator>(count, (warningCount != null) ? warningCount.GetValueOrDefault() : null);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002B45C File Offset: 0x0002965C
		internal override DataLimitOperator Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.OverlappingPointsSampleLimitOperator limitOperator)
		{
			Candidate<int> count = limitOperator.Count;
			int? warningCount = limitOperator.WarningCount;
			return this.CreateLimitOperator<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.OverlappingPointsSampleLimitOperator>(count, (warningCount != null) ? warningCount.GetValueOrDefault() : null);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002B494 File Offset: 0x00029694
		internal override DataLimitOperator Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.TopNPerLevelLimitOperator limitOperator)
		{
			return null;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002B497 File Offset: 0x00029697
		internal override DataLimitOperator Visit(WindowLimitOperator limitOperator)
		{
			throw new InvalidOperationException("WindowLimitOperator needs to be translated to a DataWindow");
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002B4A4 File Offset: 0x000296A4
		internal DataWindow BuildDataWindow(Limit limit, BuildRestartDefinition buildRestartDefinition)
		{
			LimitOperator @operator = limit.Operator;
			DataWindow dataWindow = new DataWindow();
			dataWindow.Id = limit.Id.Value;
			dataWindow.TelemetryId = limit.TelemetryId;
			dataWindow.Count = this.TranslateCount(@operator.Count, this.GetCountOverride());
			dataWindow.IsExceededDbCount = this.TranslateCount(null, this.GetIsExceededDbCount());
			dataWindow.DbCount = this.TranslateCount(null, this.GetDbCount());
			LimitOverride limitOverride = this.m_limitOverride;
			dataWindow.ExceededDetection = ((limitOverride != null) ? limitOverride.ExceededDetection : null).GetValueOrDefault();
			IScope resolvedScope = limit.GetInnermostTarget().GetResolvedScope(this.m_expressionTableForScopes);
			global::System.ValueTuple<IList<string>, IList<string>> valueTuple = this.BuildTargetsAndAppliesTo(limit, resolvedScope);
			dataWindow.Targets = valueTuple.Item1;
			dataWindow.AppliesTo = valueTuple.Item2;
			dataWindow.RestartDefinitions = this.BuildRestartDefinitions(limit, resolvedScope, buildRestartDefinition);
			return dataWindow;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002B584 File Offset: 0x00029784
		private List<IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>> BuildRestartDefinitions(Limit limit, IScope innermostTarget, BuildRestartDefinition buildRestartDefinition)
		{
			IEnumerable<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> segmentationMembers = limit.GetGroupScopesFromTargets(this.m_expressionTableForScopes).GetSegmentationMembers(this.m_subtotalAnnotations);
			List<IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>> list = new List<IList<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>>();
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember in segmentationMembers)
			{
				list.Add(buildRestartDefinition(dataMember));
			}
			return list;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002B5F0 File Offset: 0x000297F0
		private T CreateLimitOperator<T>(Candidate<int> dsqCount, Candidate<int> dsqWarningCount) where T : DataLimitOperator, new()
		{
			T t = new T();
			t.Count = this.TranslateCount(dsqCount, this.GetCountOverride());
			t.DbCount = this.TranslateCount(null, this.GetDbCount());
			t.IsExceededDbCount = this.TranslateCount(null, this.GetIsExceededDbCount());
			DataLimitOperator dataLimitOperator = t;
			LimitOverride limitOverride = this.m_limitOverride;
			dataLimitOperator.Kind = ((limitOverride != null) ? limitOverride.ExceededDetection : null);
			t.WarningCount = this.TranslateCount(dsqWarningCount, null);
			return t;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002B68C File Offset: 0x0002988C
		private ExpressionId? GetCountOverride()
		{
			LimitOverride limitOverride = this.m_limitOverride;
			if (limitOverride == null)
			{
				return null;
			}
			return limitOverride.CountOverride;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002B6B4 File Offset: 0x000298B4
		private ExpressionId? GetIsExceededDbCount()
		{
			LimitOverride limitOverride = this.m_limitOverride;
			if (limitOverride == null)
			{
				return null;
			}
			return limitOverride.IsExceededDbCount;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002B6DC File Offset: 0x000298DC
		private ExpressionId? GetDbCount()
		{
			LimitOverride limitOverride = this.m_limitOverride;
			if (limitOverride == null)
			{
				return null;
			}
			return limitOverride.DbCount;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002B704 File Offset: 0x00029904
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode TranslateCount(Candidate<int> count, ExpressionId? overrideId)
		{
			if (overrideId != null)
			{
				return DsdExpressionGenerator.Generate(this.m_expressionTableForLimitOverrides.GetNode(overrideId.Value), this.m_expressionTableForLimitOverrides, this.m_getTableIdForField, this.m_getTableIdForTransform, null);
			}
			if (count != null)
			{
				return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.LiteralExpressionNode
				{
					Value = count.Value
				};
			}
			return null;
		}

		// Token: 0x04000595 RID: 1429
		private readonly ExpressionTable m_expressionTableForScopes;

		// Token: 0x04000596 RID: 1430
		private readonly ExpressionTable m_expressionTableForLimitOverrides;

		// Token: 0x04000597 RID: 1431
		private readonly BatchSubtotalAnnotations m_subtotalAnnotations;

		// Token: 0x04000598 RID: 1432
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000599 RID: 1433
		private readonly DsdExpressionGenerator.GetTableIdForFieldReference m_getTableIdForField;

		// Token: 0x0400059A RID: 1434
		private readonly DsdExpressionGenerator.GetTableIdForTransformColumn m_getTableIdForTransform;

		// Token: 0x0400059B RID: 1435
		private readonly LimitOverride m_limitOverride;
	}
}
