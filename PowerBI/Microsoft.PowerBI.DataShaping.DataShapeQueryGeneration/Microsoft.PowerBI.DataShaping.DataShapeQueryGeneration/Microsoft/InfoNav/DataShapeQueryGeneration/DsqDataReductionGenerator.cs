using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200005B RID: 91
	internal static class DsqDataReductionGenerator
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x0000E394 File Offset: 0x0000C594
		internal static bool TryGenerate(DataShapeGenerationInternalContext context, DataShapeBuilderContext builderContext, DataShapeBuilder dataShapeBuilder, IntermediateDataReduction reduction, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, DsqExpressionGenerator expressionGenerator)
		{
			if (reduction == null)
			{
				return true;
			}
			DataShape dataShape = dataShapeBuilder.Parent();
			bool flag = reduction.DynamicLimits != null;
			if (reduction.Primary != null)
			{
				IntermediateReductionAlgorithm primary = reduction.Primary;
				bool flag2 = flag && reduction.DynamicLimits.Primary != null;
				bool useDynamicLimits = context.UseDynamicLimits;
				IntermediateDataWindow intermediateDataWindow = primary as IntermediateDataWindow;
				string text;
				if (intermediateDataWindow != null)
				{
					DsqDataReductionGenerator.CreateDataReductionWindow(dataShapeBuilder, intermediateDataWindow);
				}
				else if (!DsqDataReductionGenerator.TryCreateDataReductionLimitForAxis(context.ErrorContext, builderContext, dataShapeBuilder, primaryDynamics, dataShape, primary, flag2, useDynamicLimits, expressionGenerator, context.FederatedConceptualSchema, out text))
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.CouldNotCreateLimit(EngineMessageSeverity.Error, "primary"));
					return false;
				}
			}
			if (reduction.Secondary != null)
			{
				IntermediateReductionAlgorithm secondary = reduction.Secondary;
				bool flag3 = flag && reduction.DynamicLimits.Secondary != null;
				string text;
				if (!DsqDataReductionGenerator.TryCreateDataReductionLimitForAxis(context.ErrorContext, builderContext, dataShapeBuilder, secondaryDynamics, dataShape, secondary, flag3, false, expressionGenerator, context.FederatedConceptualSchema, out text))
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.CouldNotCreateLimit(EngineMessageSeverity.Error, "secondary"));
					return false;
				}
			}
			string text2 = null;
			if (reduction.Intersection != null)
			{
				IntermediateReductionAlgorithm intersection = reduction.Intersection;
				if (!DsqDataReductionGenerator.TryCreateDataReductionIntersectionLimit(context.ErrorContext, builderContext, dataShapeBuilder, primaryDynamics, secondaryDynamics, dataShape, intersection, flag, expressionGenerator, context.FederatedConceptualSchema, out text2))
				{
					context.ErrorContext.Register(DataShapeGenerationMessages.CouldNotCreateLimit(EngineMessageSeverity.Error, "intersection"));
					return false;
				}
			}
			List<string> list = null;
			if (reduction.Scoped != null)
			{
				if (flag)
				{
					list = new List<string>(reduction.Scoped.Count);
				}
				HashSet<ExpressionNode> hashSet = new HashSet<ExpressionNode>();
				foreach (IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm in reduction.Scoped)
				{
					string text3;
					if (!DsqDataReductionGenerator.TryCreateScopedDataReductionLimit(context, builderContext, dataShapeBuilder, primaryDynamics, secondaryDynamics, dataShape, intermediateScopedReductionAlgorithm, flag, expressionGenerator, context.FederatedConceptualSchema, context.UseDynamicLimits, hashSet, out text3))
					{
						context.ErrorContext.Register(DataShapeGenerationMessages.CouldNotCreateLimit(EngineMessageSeverity.Error, "DataReduction.Scoped"));
						return false;
					}
					if (list != null)
					{
						list.Add(text3);
					}
				}
			}
			if (flag)
			{
				DsqDataReductionGenerator.CreateDynamicLimits(dataShapeBuilder, reduction.DynamicLimits, text2, list);
			}
			return true;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		private static void CreateDynamicLimits(DataShapeBuilder dataShapeBuilder, IntermediateDynamicLimits dynamicLimits, string intersectionLimitId, IReadOnlyList<string> scopedLimitIds)
		{
			DynamicLimitsBuilder<DataShapeBuilder<DataShape>> dynamicLimitsBuilder = dataShapeBuilder.WithDynamicLimits();
			dynamicLimitsBuilder.WithTargetIntersectionCount(dynamicLimits.TargetIntersectionCount);
			if (!string.IsNullOrEmpty(intersectionLimitId) && dynamicLimits.SuppressIntersectionLimit)
			{
				dynamicLimitsBuilder.WithIntersectionLimit(intersectionLimitId.StructureReference());
			}
			if (dynamicLimits.Primary != null)
			{
				DsqDataReductionGenerator.CreateDynamicLimitRecommendation<DynamicLimitsBuilder<DataShapeBuilder<DataShape>>>(dynamicLimitsBuilder.WithPrimary(), dynamicLimits.Primary);
			}
			if (dynamicLimits.Secondary != null)
			{
				DsqDataReductionGenerator.CreateDynamicLimitRecommendation<DynamicLimitsBuilder<DataShapeBuilder<DataShape>>>(dynamicLimitsBuilder.WithSecondary(), dynamicLimits.Secondary);
			}
			if (dynamicLimits.Blocks != null)
			{
				DsqDataReductionGenerator.CreateDynamicLimitBlocks<DataShapeBuilder<DataShape>>(dynamicLimitsBuilder, dynamicLimits.Blocks, scopedLimitIds);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000E634 File Offset: 0x0000C834
		private static void CreateDynamicLimitBlocks<TParent>(DynamicLimitsBuilder<TParent> builder, IReadOnlyList<IntermediateDynamicLimitBlock> blocks, IReadOnlyList<string> scopedLimitIds)
		{
			foreach (IntermediateDynamicLimitBlock intermediateDynamicLimitBlock in blocks)
			{
				IntermediateDynamicLimitEvenDistributionBlock intermediateDynamicLimitEvenDistributionBlock = intermediateDynamicLimitBlock as IntermediateDynamicLimitEvenDistributionBlock;
				if (intermediateDynamicLimitEvenDistributionBlock == null)
				{
					IntermediateDynamicLimitPrimarySecondaryBlock intermediateDynamicLimitPrimarySecondaryBlock = intermediateDynamicLimitBlock as IntermediateDynamicLimitPrimarySecondaryBlock;
					if (intermediateDynamicLimitPrimarySecondaryBlock == null)
					{
						throw new InvalidOperationException("Unknown dynamic limit block type " + intermediateDynamicLimitBlock.GetType().Name);
					}
					DsqDataReductionGenerator.CreateDynamicLimitPrimarySecondaryBlock<DynamicLimitsBuilder<TParent>>(builder.WithPrimarySecondaryBlock(), intermediateDynamicLimitPrimarySecondaryBlock, scopedLimitIds);
				}
				else
				{
					DsqDataReductionGenerator.CreateDynamicLimitEvenDistributionBlock<DynamicLimitsBuilder<TParent>>(builder.WithEvenDistributionBlock(), intermediateDynamicLimitEvenDistributionBlock, scopedLimitIds);
				}
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		private static void CreateDynamicLimitEvenDistributionBlock<TParent>(DynamicLimitEvenDistributionBlockBuilder<TParent> builder, IntermediateDynamicLimitEvenDistributionBlock block, IReadOnlyList<string> scopedLimitIds)
		{
			DsqDataReductionGenerator.CreateDynamicLimitRecommendation<DynamicLimitEvenDistributionBlockBuilder<TParent>>(builder.WithCount(), block.Count);
			foreach (IntermediateDynamicLimit intermediateDynamicLimit in block.Limits)
			{
				DsqDataReductionGenerator.CreateDynamicLimit<DynamicLimitEvenDistributionBlockBuilder<TParent>>(builder.WithDynamicLimit(), intermediateDynamicLimit, scopedLimitIds);
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000E728 File Offset: 0x0000C928
		private static void CreateDynamicLimitPrimarySecondaryBlock<TParent>(DynamicLimitPrimarySecondaryBlockBuilder<TParent> builder, IntermediateDynamicLimitPrimarySecondaryBlock block, IReadOnlyList<string> scopedLimitIds)
		{
			if (block.Count != null)
			{
				DsqDataReductionGenerator.CreateDynamicLimitRecommendation<DynamicLimitPrimarySecondaryBlockBuilder<TParent>>(builder.WithCount(), block.Count);
			}
			DsqDataReductionGenerator.CreateDynamicLimit<DynamicLimitPrimarySecondaryBlockBuilder<TParent>>(builder.WithPrimary(), block.Primary, scopedLimitIds);
			DsqDataReductionGenerator.CreateDynamicLimit<DynamicLimitPrimarySecondaryBlockBuilder<TParent>>(builder.WithSecondary(), block.Secondary, scopedLimitIds);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000E768 File Offset: 0x0000C968
		private static void CreateDynamicLimit<TParent>(DynamicLimitBuilder<TParent> builder, IntermediateDynamicLimit dynamicLimit, IReadOnlyList<string> scopedLimitIds)
		{
			string text = scopedLimitIds[dynamicLimit.ScopedReductionIndex];
			builder.WithLimitRef(text.StructureReference());
			DsqDataReductionGenerator.CreateDynamicLimitRecommendation<DynamicLimitBuilder<TParent>>(builder.WithCount(), dynamicLimit.Count);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000E7A5 File Offset: 0x0000C9A5
		private static void CreateDynamicLimitRecommendation<TParent>(DynamicLimitRecommendationBuilder<TParent> builder, IntermediateDynamicLimitRange range)
		{
			builder.WithMin(range.Min);
			builder.WithMax(range.Max);
			builder.WithIsMandatoryConstraint(range.IsMandatoryConstraint);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		private static void CreateDataReductionWindow(DataShapeBuilder dataShapeBuilder, IntermediateDataWindow window)
		{
			dataShapeBuilder.WithRequestedPrimaryLeafCount(window.Count.Value);
			if (window.IncludeRestartToken)
			{
				dataShapeBuilder.IncludeRestartToken();
			}
			if (window.RestartTokens != null)
			{
				dataShapeBuilder.WithRestartTokens(window.RestartTokens.ToRestartTokens());
			}
			if (window.RestartMatchingBehavior != null)
			{
				dataShapeBuilder.WithRestartMatchingBehavior(window.RestartMatchingBehavior.Value);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000E840 File Offset: 0x0000CA40
		private static void CreateDataReductionWindowAsLimit<T>(LimitBuilder<T> limitBuilder, IntermediateDataWindow window)
		{
			limitBuilder.WithWindow(window.Count.Value, window.RestartTokens.ToRestartTokens(), window.RestartMatchingBehavior);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000E874 File Offset: 0x0000CA74
		private static bool TryCreateDataReductionLimitForAxis(DataShapeGenerationErrorContext errorContext, DataShapeBuilderContext builderContext, DataShapeBuilder dataShapeBuilder, IReadOnlyList<DataMemberBuilderPair> dynamics, DataShape dataShape, IntermediateReductionAlgorithm algorithm, bool shouldGenerateReferenceCalc, bool shouldPreserveKeyPoints, DsqExpressionGenerator expressionGenerator, IFederatedConceptualSchema conceptualSchema, out string limitId)
		{
			limitId = null;
			if (algorithm is IntermediateDataWindow)
			{
				errorContext.Register(DataShapeGenerationMessages.UnexpectedReductionAlgorithmType(EngineMessageSeverity.Error));
				return false;
			}
			DataMember dataMember;
			if (!dynamics.TryGetInnermostDynamic(out dataMember))
			{
				errorContext.Register(DataShapeGenerationMessages.NoInnermostDynamicDataMemberFound(EngineMessageSeverity.Error));
				return false;
			}
			StructureReferenceExpressionNode structureReferenceExpressionNode = dataShape.Id.StructureReference();
			limitId = builderContext.CreateLimitId();
			List<Expression> list = DsqDataReductionGenerator.BuildHierachyTargets(dynamics);
			LimitBuilder<DataShapeBuilder<DataShape>> limitBuilder = dataShapeBuilder.WithLimit(limitId, list, structureReferenceExpressionNode);
			return DsqDataReductionGenerator.TryCreateDataReductionLimit<DataShapeBuilder<DataShape>>(errorContext, builderContext, dataShapeBuilder, limitBuilder, algorithm, shouldGenerateReferenceCalc, shouldPreserveKeyPoints, expressionGenerator, conceptualSchema, false);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000E900 File Offset: 0x0000CB00
		private static bool TryCreateScopedDataReductionLimit(DataShapeGenerationInternalContext context, DataShapeBuilderContext builderContext, DataShapeBuilder dataShapeBuilder, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, DataShape dataShape, IntermediateScopedReductionAlgorithm scopedReduction, bool shouldGenerateReferenceCalc, DsqExpressionGenerator expressionGenerator, IFederatedConceptualSchema conceptualSchema, bool useDynamicLimits, HashSet<ExpressionNode> overallScopedLimitTargets, out string limitId)
		{
			IntermediateReductionScope scope = scopedReduction.Scope;
			List<int> primary = scope.Primary;
			int num = ((primary != null) ? primary.Count : 0);
			List<int> secondary = scope.Secondary;
			int num2 = ((secondary != null) ? secondary.Count : 0);
			bool flag = num > 0 && num2 > 0;
			List<Expression> list = new List<Expression>(num + num2 + (flag ? 1 : 0));
			DsqDataReductionGenerator.AddTargets(list, overallScopedLimitTargets, scope.Primary, primaryDynamics, context);
			DsqDataReductionGenerator.AddTargets(list, overallScopedLimitTargets, scope.Secondary, secondaryDynamics, context);
			if (flag)
			{
				DsqDataReductionGenerator.AddInnermostIntersectionTarget(list, scope, primaryDynamics, secondaryDynamics);
			}
			StructureReferenceExpressionNode structureReferenceExpressionNode = dataShape.Id.StructureReference();
			limitId = builderContext.CreateLimitId();
			LimitBuilder<DataShapeBuilder<DataShape>> limitBuilder = dataShapeBuilder.WithLimit(limitId, list, structureReferenceExpressionNode);
			limitBuilder.WithTelemetryId(scopedReduction.TelemetryId);
			bool flag2 = useDynamicLimits && num > 0 && num2 == 0;
			bool flag3 = flag;
			return DsqDataReductionGenerator.TryCreateDataReductionLimit<DataShapeBuilder<DataShape>>(context.ErrorContext, builderContext, dataShapeBuilder, limitBuilder, scopedReduction.Algorithm, shouldGenerateReferenceCalc, flag2, expressionGenerator, conceptualSchema, flag3);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000EA04 File Offset: 0x0000CC04
		private static void AddTargets(List<Expression> targets, HashSet<ExpressionNode> overallScopedLimitTargets, IReadOnlyList<int> scopeIndices, IReadOnlyList<DataMemberBuilderPair> axisDynamics, DataShapeGenerationInternalContext context)
		{
			if (scopeIndices == null)
			{
				return;
			}
			HashSet<ExpressionNode> hashSet = new HashSet<ExpressionNode>();
			foreach (int num in scopeIndices)
			{
				DataMemberBuilder dynamic = axisDynamics[num].Dynamic;
				targets.Add(dynamic.Id.StructureReference());
				foreach (GroupKey groupKey in dynamic.Result.Group.GroupKeys)
				{
					ExpressionNode originalNode = groupKey.Value.OriginalNode;
					if (hashSet.Add(originalNode) && !overallScopedLimitTargets.Add(originalNode))
					{
						context.Tracer.SanitizedTrace(TraceLevel.Warning, "Duplicate group key found across scoped limits.");
					}
				}
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		private static void AddInnermostIntersectionTarget(List<Expression> targets, IntermediateReductionScope scope, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics)
		{
			ExpressionNode lastTargetDynamic = DsqDataReductionGenerator.GetLastTargetDynamic(scope.Primary, primaryDynamics);
			ExpressionNode lastTargetDynamic2 = DsqDataReductionGenerator.GetLastTargetDynamic(scope.Secondary, secondaryDynamics);
			ExpressionNode expressionNode = lastTargetDynamic.Intersect(lastTargetDynamic2);
			targets.Add(expressionNode);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		private static ExpressionNode GetLastTargetDynamic(IReadOnlyList<int> scopeIndices, IReadOnlyList<DataMemberBuilderPair> axisDynamics)
		{
			int num = scopeIndices[scopeIndices.Count - 1];
			return axisDynamics[num].Dynamic.Id.StructureReference();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000EB60 File Offset: 0x0000CD60
		private static bool TryCreateDataReductionIntersectionLimit(DataShapeGenerationErrorContext errorContext, DataShapeBuilderContext builderContext, DataShapeBuilder dataShapeBuilder, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, DataShape dataShape, IntermediateReductionAlgorithm algorithm, bool shouldGenerateReferenceCalc, DsqExpressionGenerator expressionGenerator, IFederatedConceptualSchema conceptualSchema, out string limitId)
		{
			limitId = null;
			if (algorithm is IntermediateDataWindow || algorithm is IntermediateTopNPerLevelSampleLimit)
			{
				errorContext.Register(DataShapeGenerationMessages.UnexpectedReductionAlgorithmType(EngineMessageSeverity.Error));
				return false;
			}
			DataMember dataMember;
			if (!primaryDynamics.TryGetInnermostDynamic(out dataMember))
			{
				errorContext.Register(DataShapeGenerationMessages.NoInnermostDynamicDataMemberFoundForIntersectionLimit(EngineMessageSeverity.Error, "primary"));
				return false;
			}
			DataMember dataMember2;
			if (!secondaryDynamics.TryGetInnermostDynamic(out dataMember2))
			{
				errorContext.Register(DataShapeGenerationMessages.NoInnermostDynamicDataMemberFoundForIntersectionLimit(EngineMessageSeverity.Error, "secondary"));
				return false;
			}
			ExpressionNode expressionNode = dataMember.Id.StructureReference().Intersect(dataMember2.Id.StructureReference());
			StructureReferenceExpressionNode structureReferenceExpressionNode = dataShape.Id.StructureReference();
			limitId = builderContext.CreateLimitId();
			List<Expression> list = DsqDataReductionGenerator.BuildIntersectionTargets(primaryDynamics, secondaryDynamics, expressionNode);
			LimitBuilder<DataShapeBuilder<DataShape>> limitBuilder = dataShapeBuilder.WithLimit(limitId, list, structureReferenceExpressionNode);
			return DsqDataReductionGenerator.TryCreateDataReductionLimit<DataShapeBuilder<DataShape>>(errorContext, builderContext, dataShapeBuilder, limitBuilder, algorithm, shouldGenerateReferenceCalc, false, expressionGenerator, conceptualSchema, true);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000EC40 File Offset: 0x0000CE40
		private static bool TryCreateDataReductionLimit<T>(DataShapeGenerationErrorContext errorContext, DataShapeBuilderContext builderContext, DataShapeBuilder dataShapeBuilder, LimitBuilder<T> limitBuilder, IntermediateReductionAlgorithm algorithm, bool shouldGenerateReferenceCalc, bool shouldPreserveKeyPoints, DsqExpressionGenerator expressionGenerator, IFederatedConceptualSchema conceptualSchema, bool shouldGenerateBinnedLineSampleHierarchyCounts)
		{
			algorithm.Id = limitBuilder.Id.Value;
			IntermediateSimpleLimit intermediateSimpleLimit = algorithm as IntermediateSimpleLimit;
			if (intermediateSimpleLimit != null)
			{
				DsqDataReductionGenerator.CreateSimpleLimit<T>(intermediateSimpleLimit, shouldPreserveKeyPoints, limitBuilder);
				if (shouldGenerateReferenceCalc)
				{
					intermediateSimpleLimit.Calc = builderContext.AddLimitPropertyCalculation<DataShapeBuilder<DataShape>>(dataShapeBuilder, limitBuilder.Id.Value, null);
				}
				return true;
			}
			IntermediateBinnedLineSampleLimit intermediateBinnedLineSampleLimit = algorithm as IntermediateBinnedLineSampleLimit;
			if (intermediateBinnedLineSampleLimit != null)
			{
				DsqDataReductionGenerator.CreateBinnedLineSampleLimit<T>(builderContext, intermediateBinnedLineSampleLimit, limitBuilder);
				intermediateBinnedLineSampleLimit.DbCountCalc = builderContext.AddLimitPropertyCalculation<DataShapeBuilder<DataShape>>(dataShapeBuilder, limitBuilder.Id.Value, "DbCount");
				if (shouldGenerateBinnedLineSampleHierarchyCounts)
				{
					intermediateBinnedLineSampleLimit.DbPrimaryCalc = builderContext.AddLimitPropertyCalculation<DataShapeBuilder<DataShape>>(dataShapeBuilder, limitBuilder.Id.Value, "DbPrimaryCount");
					intermediateBinnedLineSampleLimit.DbSecondaryCalc = builderContext.AddLimitPropertyCalculation<DataShapeBuilder<DataShape>>(dataShapeBuilder, limitBuilder.Id.Value, "DbSecondaryCount");
				}
				return true;
			}
			IntermediateOverlappingPointsSampleLimit intermediateOverlappingPointsSampleLimit = algorithm as IntermediateOverlappingPointsSampleLimit;
			if (intermediateOverlappingPointsSampleLimit != null)
			{
				DsqDataReductionGenerator.CreateOverlappingPointsSampleLimit<T>(builderContext, dataShapeBuilder, intermediateOverlappingPointsSampleLimit, limitBuilder);
				return true;
			}
			IntermediateTopNPerLevelSampleLimit intermediateTopNPerLevelSampleLimit = algorithm as IntermediateTopNPerLevelSampleLimit;
			if (intermediateTopNPerLevelSampleLimit != null)
			{
				DsqDataReductionGenerator.CreateTopNPerLevelSampleLimit<T>(intermediateTopNPerLevelSampleLimit, expressionGenerator, conceptualSchema, errorContext, limitBuilder, dataShapeBuilder.Id.Value);
				return true;
			}
			IntermediateDataWindow intermediateDataWindow = algorithm as IntermediateDataWindow;
			if (intermediateDataWindow == null)
			{
				errorContext.Register(DataShapeGenerationMessages.UnexpectedReductionAlgorithmType(EngineMessageSeverity.Error));
				return false;
			}
			DsqDataReductionGenerator.CreateDataReductionWindowAsLimit<T>(limitBuilder, intermediateDataWindow);
			if (shouldGenerateReferenceCalc)
			{
				intermediateDataWindow.Calc = builderContext.AddLimitPropertyCalculation<DataShapeBuilder<DataShape>>(dataShapeBuilder, limitBuilder.Id.Value, null);
			}
			return true;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		private static void CreateSimpleLimit<T>(IntermediateSimpleLimit limitAlgorithm, bool shouldPreserveKeyPoints, LimitBuilder<T> limitBuilder)
		{
			switch (limitAlgorithm.Kind)
			{
			case IntermediateSimpleLimitKind.Top:
				limitBuilder.WithTop(limitAlgorithm.Count.Value, limitAlgorithm.Skip, limitAlgorithm.IsStrict);
				return;
			case IntermediateSimpleLimitKind.Sample:
			{
				bool? flag = ((shouldPreserveKeyPoints && !limitAlgorithm.DisablePreserveKeyPoints) ? new bool?(true) : null);
				limitBuilder.WithSample(limitAlgorithm.Count.Value, flag);
				return;
			}
			case IntermediateSimpleLimitKind.Bottom:
				limitBuilder.WithBottom(limitAlgorithm.Count.Value);
				return;
			case IntermediateSimpleLimitKind.First:
				limitBuilder.WithFirst();
				return;
			case IntermediateSimpleLimitKind.Last:
				limitBuilder.WithLast();
				return;
			default:
				throw new InvalidOperationException("Unsupported limit kind.");
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000EE44 File Offset: 0x0000D044
		private static void CreateBinnedLineSampleLimit<T>(DataShapeBuilderContext builderContext, IntermediateBinnedLineSampleLimit limitAlgorithm, LimitBuilder<T> limitBuilder)
		{
			List<Expression> measureIdsStructureReferences = builderContext.GetMeasureIdsStructureReferences(limitAlgorithm.Measures);
			Expression expression = ((limitAlgorithm.PrimaryScalarKey != null) ? builderContext.GetSelectIndexExpression(limitAlgorithm.PrimaryScalarKey) : null);
			limitBuilder.WithBinnedLineSample(limitAlgorithm.Count.Value, limitAlgorithm.MinPointsPerSeries.Value, limitAlgorithm.MaxPointsPerSeries.Value, limitAlgorithm.MaxDynamicSeriesCount.Value, measureIdsStructureReferences, expression, limitAlgorithm.WarningCount);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
		private static void CreateOverlappingPointsSampleLimit<T>(DataShapeBuilderContext builderContext, DataShapeBuilder dataShapeBuilder, IntermediateOverlappingPointsSampleLimit limitAlgorithm, LimitBuilder<T> limitBuilder)
		{
			LimitPlotAxis limitPlotAxis = DsqDataReductionGenerator.CreateLimitPlotAxis(builderContext, limitAlgorithm.X);
			bool flag = IntermediatePlotAxisBinding.AreEquivalent(limitAlgorithm.X, limitAlgorithm.Y);
			IntermediatePlotAxisBinding intermediatePlotAxisBinding = (flag ? null : limitAlgorithm.Y);
			LimitPlotAxis limitPlotAxis2 = DsqDataReductionGenerator.CreateLimitPlotAxis(builderContext, intermediatePlotAxisBinding);
			limitBuilder.WithOverlappingPointsSample(limitAlgorithm.Count.Value, limitPlotAxis, limitPlotAxis2);
			string value = limitBuilder.Id.Value;
			if (limitAlgorithm.X != null && limitAlgorithm.X.Transform != DataReductionPlotAxisTransform.None)
			{
				limitAlgorithm.X.Applied = builderContext.AddLimitPropertyCalculation<DataShapeBuilder<DataShape>>(dataShapeBuilder, value, "XTransformApplied");
			}
			if (limitAlgorithm.Y != null && limitAlgorithm.Y.Transform != DataReductionPlotAxisTransform.None)
			{
				limitAlgorithm.Y.Applied = (flag ? limitAlgorithm.X.Applied : builderContext.AddLimitPropertyCalculation<DataShapeBuilder<DataShape>>(dataShapeBuilder, value, "YTransformApplied"));
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000EF9C File Offset: 0x0000D19C
		private static LimitPlotAxis CreateLimitPlotAxis(DataShapeBuilderContext builderContext, IntermediatePlotAxisBinding binding)
		{
			Expression expression = ((binding != null) ? builderContext.GetSelectIndexExpression(new int?(binding.Index)) : null);
			if (expression == null)
			{
				return null;
			}
			return new LimitPlotAxis
			{
				Key = expression,
				Transform = binding.Transform
			};
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		private static void CreateTopNPerLevelSampleLimit<T>(IntermediateTopNPerLevelSampleLimit limitAlgorithm, DsqExpressionGenerator expressionGenerator, IFederatedConceptualSchema conceptualSchema, DataShapeGenerationErrorContext errorContext, LimitBuilder<T> limitBuilder, string owningQueryName)
		{
			DsqWindowExpansionStateGenerator<LimitBuilder<T>>.Generate(limitBuilder.WithTopNPerLevel(limitAlgorithm.Count.GetValueOrDefault()), expressionGenerator, errorContext, limitAlgorithm.WindowExpansionState, owningQueryName);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000F014 File Offset: 0x0000D214
		private static List<Expression> BuildHierachyTargets(IReadOnlyList<DataMemberBuilderPair> dynamics)
		{
			List<Expression> list;
			if (dynamics == null)
			{
				list = null;
			}
			else
			{
				list = (from d in dynamics
					where !d.Dynamic.Result.ContextOnly
					select d into dynamicPair
					select dynamicPair.Dynamic.Id.StructureReference()).ToList<Expression>();
			}
			List<Expression> list2 = list;
			if (!list2.IsNullOrEmpty<Expression>())
			{
				return list2;
			}
			return null;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000F084 File Offset: 0x0000D284
		private static List<Expression> BuildIntersectionTargets(IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, Expression intersection)
		{
			List<Expression> list = new List<Expression>(primaryDynamics.Count + secondaryDynamics.Count + 1);
			foreach (DataMemberBuilderPair dataMemberBuilderPair in primaryDynamics)
			{
				list.Add(dataMemberBuilderPair.Dynamic.Id.StructureReference());
			}
			foreach (DataMemberBuilderPair dataMemberBuilderPair2 in secondaryDynamics)
			{
				list.Add(dataMemberBuilderPair2.Dynamic.Id.StructureReference());
			}
			list.Add(intersection);
			return list;
		}
	}
}
