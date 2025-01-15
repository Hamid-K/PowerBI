using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders.DynamicLimits;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C9 RID: 457
	internal static class DynamicScopedLimitsBalancer
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x00041C20 File Offset: 0x0003FE20
		internal static void DetermineScopedDynamicLimitCounts(ILimitPlanningContext context, PlanDeclarationCollection declarations, DataShapeContext dsContext, IReadOnlyList<IntermediateScopedLimitState> primaryLimitStates, IReadOnlyList<IntermediateScopedLimitState> secondaryLimitStates)
		{
			DynamicLimits dynamicLimits = dsContext.DataShape.DynamicLimits;
			DynamicScopedLimitsBalancer.LimitStateTable limitStateTable = new DynamicScopedLimitsBalancer.LimitStateTable(context.OutputExpressionTable, primaryLimitStates, secondaryLimitStates);
			DynamicScopedLimitsBalancer.LimitBlockInfo[] array = DynamicScopedLimitsBalancer.ApplyBlockBalancing(context, declarations, dsContext, limitStateTable);
			DynamicScopedLimitsBalancer.ApplyLimitBalancingWithinBlocks(context, declarations, dsContext, limitStateTable, array);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00041C5C File Offset: 0x0003FE5C
		private static DynamicScopedLimitsBalancer.LimitBlockInfo[] ApplyBlockBalancing(ILimitPlanningContext context, PlanDeclarationCollection declarations, DataShapeContext dsContext, DynamicScopedLimitsBalancer.LimitStateTable limitStateTable)
		{
			DynamicLimits dynamicLimits = dsContext.DataShape.DynamicLimits;
			List<DynamicLimitBlock> blocks = dynamicLimits.Blocks;
			DynamicScopedLimitsBalancer.LimitBlockInfo[] array = new DynamicScopedLimitsBalancer.LimitBlockInfo[blocks.Count];
			ExpressionNode expressionNode = null;
			int i = 0;
			int count = blocks.Count;
			while (i < count)
			{
				DynamicLimitBlock dynamicLimitBlock = blocks[i];
				DynamicScopedLimitsBalancer.LimitBlockInfo limitBlockInfo = DynamicScopedLimitsBalancer.DetermineInitialBlockInfo(limitStateTable, dynamicLimitBlock);
				limitBlockInfo.CombinedLimitCount = limitBlockInfo.CombinedLimitCount.DeclareIfNotDeclared(PlanNames.LimitBlockDbCount(dsContext.Id, i), declarations, context.ErrorContext, ObjectType.DynamicLimits, dsContext.Id);
				if (limitBlockInfo.CombinedMandatoryLimitCount != null)
				{
					limitBlockInfo.CombinedMandatoryLimitCount = limitBlockInfo.CombinedMandatoryLimitCount.DeclareIfNotDeclared(PlanNames.LimitBlockMandatoryCount(dsContext.Id, i), declarations, context.ErrorContext, ObjectType.DynamicLimits, dsContext.Id);
					expressionNode = expressionNode.MultiplyCoalesce(limitBlockInfo.CombinedMandatoryLimitCount);
				}
				array[i] = limitBlockInfo;
				i++;
			}
			expressionNode = ((expressionNode != null) ? expressionNode.DeclareIfNotDeclared(PlanNames.TotalMandatoryCount(dsContext.Id), declarations, context.ErrorContext, ObjectType.DynamicLimits, dsContext.Id) : null);
			ExpressionNode expressionNode2 = dynamicLimits.TargetIntersectionCount.Value.ToLiteralExpr();
			if (blocks.Count == 1)
			{
				DynamicLimitBlock dynamicLimitBlock2 = blocks[0];
				if (dynamicLimitBlock2.Count == null)
				{
					array[0].TargetCount = expressionNode2;
				}
				else
				{
					array[0].TargetCount = dynamicLimitBlock2.Count.Max.ToLiteralExpr();
				}
			}
			else
			{
				ExpressionNode expressionNode3 = expressionNode2;
				if (expressionNode != null)
				{
					expressionNode3 = expressionNode3.Divide(expressionNode);
					expressionNode3 = expressionNode3.DeclareIfNotDeclared(PlanNames.RemainingCapacityAtStart(dsContext.Id), declarations, context.ErrorContext, ObjectType.DynamicLimits, dsContext.Id);
				}
				int j = 0;
				int count2 = blocks.Count;
				while (j < count2)
				{
					DynamicLimitBlock dynamicLimitBlock3 = blocks[j];
					ref DynamicScopedLimitsBalancer.LimitBlockInfo ptr = ref array[j];
					if (ptr.NumNonMandatoryConstraints == 0)
					{
						DynamicLimitRecommendation count3 = dynamicLimitBlock3.Count;
						int num = ((count3 != null) ? count3.Min.Value : 1);
						ptr.TargetCount = ExprNodes.MaxValue(new ExpressionNode[]
						{
							ptr.CombinedMandatoryLimitCount,
							num.ToLiteralExpr()
						});
					}
					else
					{
						ExpressionNode combinedMandatoryLimitCount = ptr.CombinedMandatoryLimitCount;
						ExpressionNode expressionNode4;
						if (combinedMandatoryLimitCount != null)
						{
							expressionNode4 = expressionNode3.Multiply(combinedMandatoryLimitCount);
						}
						else
						{
							expressionNode4 = expressionNode3;
						}
						if (dynamicLimitBlock3.Count != null)
						{
							expressionNode4 = ExprNodes.MinValue(new ExpressionNode[]
							{
								expressionNode4,
								dynamicLimitBlock3.Count.Max.ToLiteralExpr()
							});
							expressionNode4 = ExprNodes.MinValue(new ExpressionNode[] { expressionNode4, ptr.CombinedLimitCount });
							expressionNode4 = ExprNodes.MaxValue(new ExpressionNode[]
							{
								expressionNode4,
								dynamicLimitBlock3.Count.Min.ToLiteralExpr()
							});
						}
						else
						{
							expressionNode4 = ExprNodes.MaxValue(new ExpressionNode[]
							{
								expressionNode4,
								LiteralExpressionNode.OneInt64
							});
						}
						expressionNode4 = expressionNode4.Ceiling(null, FunctionUsageKind.Query);
						expressionNode4 = expressionNode4.DeclareIfNotDeclared(PlanNames.LimitBlockCount(dsContext.Id, j), declarations, context.ErrorContext, ObjectType.DynamicLimits, dsContext.Id);
						ptr.TargetCount = expressionNode4;
						if (j < count2 - 1)
						{
							ExpressionNode expressionNode5 = expressionNode4;
							if (combinedMandatoryLimitCount != null)
							{
								expressionNode5 = expressionNode5.Divide(combinedMandatoryLimitCount);
							}
							expressionNode3 = expressionNode3.Divide(expressionNode5);
							expressionNode3 = expressionNode3.DeclareIfNotDeclared(PlanNames.RemainingCapacityAfterBlock(dsContext.Id, j), declarations, context.ErrorContext, ObjectType.DynamicLimits, dsContext.Id);
						}
					}
					j++;
				}
			}
			return array;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00041FAC File Offset: 0x000401AC
		private static DynamicScopedLimitsBalancer.LimitBlockInfo DetermineInitialBlockInfo(DynamicScopedLimitsBalancer.LimitStateTable limitStateTable, DynamicLimitBlock block)
		{
			DynamicLimitEvenDistributionBlock dynamicLimitEvenDistributionBlock = block as DynamicLimitEvenDistributionBlock;
			if (dynamicLimitEvenDistributionBlock != null)
			{
				return DynamicScopedLimitsBalancer.DetermineInitialEvenDistributionBlockInfo(limitStateTable, dynamicLimitEvenDistributionBlock);
			}
			DynamicLimitPrimarySecondaryBlock dynamicLimitPrimarySecondaryBlock = block as DynamicLimitPrimarySecondaryBlock;
			if (dynamicLimitPrimarySecondaryBlock == null)
			{
				throw new InvalidOperationException("Unexpected strategy " + block.GetType().Name);
			}
			return DynamicScopedLimitsBalancer.DetermineInitialPrimarySecondaryBlockInfo(limitStateTable, dynamicLimitPrimarySecondaryBlock);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00041FFC File Offset: 0x000401FC
		private static DynamicScopedLimitsBalancer.LimitBlockInfo DetermineInitialPrimarySecondaryBlockInfo(DynamicScopedLimitsBalancer.LimitStateTable limitStateTable, DynamicLimitPrimarySecondaryBlock primarySecondaryBlock)
		{
			DynamicScopedLimitsBalancer.LimitBlockInfo limitBlockInfo = default(DynamicScopedLimitsBalancer.LimitBlockInfo);
			DynamicScopedLimitsBalancer.AccumulateInitialLimitInfo(primarySecondaryBlock.Primary, limitStateTable, ref limitBlockInfo);
			DynamicScopedLimitsBalancer.AccumulateInitialLimitInfo(primarySecondaryBlock.Secondary, limitStateTable, ref limitBlockInfo);
			return limitBlockInfo;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00042030 File Offset: 0x00040230
		private static DynamicScopedLimitsBalancer.LimitBlockInfo DetermineInitialEvenDistributionBlockInfo(DynamicScopedLimitsBalancer.LimitStateTable limitStateTable, DynamicLimitEvenDistributionBlock evenDistributionBlock)
		{
			DynamicScopedLimitsBalancer.LimitBlockInfo limitBlockInfo = default(DynamicScopedLimitsBalancer.LimitBlockInfo);
			foreach (DynamicLimit dynamicLimit in evenDistributionBlock.Limits)
			{
				DynamicScopedLimitsBalancer.AccumulateInitialLimitInfo(dynamicLimit, limitStateTable, ref limitBlockInfo);
			}
			return limitBlockInfo;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0004208C File Offset: 0x0004028C
		private static void AccumulateInitialLimitInfo(DynamicLimit dynamicLimit, DynamicScopedLimitsBalancer.LimitStateTable limitStateTable, ref DynamicScopedLimitsBalancer.LimitBlockInfo blockInfo)
		{
			IntermediateScopedLimitState limitState = limitStateTable.GetLimitState(dynamicLimit);
			ExpressionNode expressionNode;
			if (dynamicLimit.Count.IsMandatoryConstraint)
			{
				expressionNode = ExprNodes.MinValue(new ExpressionNode[]
				{
					limitState.DbCount,
					dynamicLimit.Count.Max.ToLiteralExpr()
				});
				blockInfo.CombinedMandatoryLimitCount = blockInfo.CombinedMandatoryLimitCount.MultiplyCoalesce(expressionNode);
			}
			else
			{
				expressionNode = limitState.DbCount;
				int numNonMandatoryConstraints = blockInfo.NumNonMandatoryConstraints;
				blockInfo.NumNonMandatoryConstraints = numNonMandatoryConstraints + 1;
			}
			blockInfo.CombinedLimitCount = blockInfo.CombinedLimitCount.MultiplyCoalesce(expressionNode);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00042114 File Offset: 0x00040314
		private static void ApplyLimitBalancingWithinBlocks(ILimitPlanningContext context, PlanDeclarationCollection declarations, DataShapeContext dsContext, DynamicScopedLimitsBalancer.LimitStateTable limitStateTable, DynamicScopedLimitsBalancer.LimitBlockInfo[] blockInfos)
		{
			List<DynamicLimitBlock> blocks = dsContext.DataShape.DynamicLimits.Blocks;
			for (int i = 0; i < blocks.Count; i++)
			{
				DynamicLimitBlock dynamicLimitBlock = blocks[i];
				DynamicScopedLimitsBalancer.LimitBlockInfo limitBlockInfo = blockInfos[i];
				DynamicLimitEvenDistributionBlock dynamicLimitEvenDistributionBlock = dynamicLimitBlock as DynamicLimitEvenDistributionBlock;
				if (dynamicLimitEvenDistributionBlock == null)
				{
					DynamicLimitPrimarySecondaryBlock dynamicLimitPrimarySecondaryBlock = dynamicLimitBlock as DynamicLimitPrimarySecondaryBlock;
					if (dynamicLimitPrimarySecondaryBlock == null)
					{
						throw new InvalidOperationException("Unexpected strategy " + dynamicLimitBlock.GetType().Name);
					}
					DynamicScopedLimitsBalancer.BalanceLimitsWithinPrimarySecondaryBlock(dynamicLimitPrimarySecondaryBlock, limitBlockInfo, context, declarations, dsContext, limitStateTable);
				}
				else
				{
					DynamicScopedLimitsBalancer.BalanceLimitsWithinEvenDistributionBlock(dynamicLimitEvenDistributionBlock, i, limitBlockInfo, context, declarations, dsContext, limitStateTable);
				}
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000421A8 File Offset: 0x000403A8
		private static void BalanceLimitsWithinEvenDistributionBlock(DynamicLimitEvenDistributionBlock evenDistributionBlock, int blockIndex, DynamicScopedLimitsBalancer.LimitBlockInfo blockInfo, ILimitPlanningContext context, PlanDeclarationCollection declarations, DataShapeContext dsContext, DynamicScopedLimitsBalancer.LimitStateTable limitStateTable)
		{
			ExpressionNode targetCount = blockInfo.TargetCount;
			ExpressionNode expressionNode = blockInfo.CombinedLimitCount.LessThanOrEqualNoNan(targetCount);
			ExpressionNode expressionNode2 = null;
			foreach (DynamicLimit dynamicLimit in evenDistributionBlock.Limits)
			{
				IntermediateScopedLimitState limitState = limitStateTable.GetLimitState(dynamicLimit);
				ExpressionNode expressionNode3;
				if (dynamicLimit.Count.IsMandatoryConstraint)
				{
					expressionNode3 = dynamicLimit.Count.Max.ToLiteralExpr();
				}
				else
				{
					if (expressionNode2 == null)
					{
						ExpressionNode expressionNode4 = targetCount;
						if (blockInfo.CombinedMandatoryLimitCount != null)
						{
							expressionNode4 = expressionNode4.Divide(blockInfo.CombinedMandatoryLimitCount);
						}
						ExpressionNode expressionNode5 = expressionNode4.NthRoot(blockInfo.NumNonMandatoryConstraints);
						expressionNode2 = expressionNode.If(targetCount, expressionNode5).Ceiling(null, FunctionUsageKind.Query);
						expressionNode2 = expressionNode2.DeclareIfNotDeclared(PlanNames.LimitBlockLimitCount(dsContext.Id, blockIndex), declarations, context.ErrorContext, ObjectType.DynamicLimits, dsContext.Id);
					}
					expressionNode3 = expressionNode2;
					expressionNode3 = expressionNode3.DeclareIfNotDeclared(PlanNames.ScopedLimitCount(dsContext.Id, limitState.Limit.Id), declarations, context.ErrorContext, ObjectType.DynamicLimit, dsContext.Id);
				}
				limitState.TargetCountOverride = expressionNode3;
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000422E8 File Offset: 0x000404E8
		private static void BalanceLimitsWithinPrimarySecondaryBlock(DynamicLimitPrimarySecondaryBlock primarySecondaryBlock, DynamicScopedLimitsBalancer.LimitBlockInfo blockInfo, ILimitPlanningContext context, PlanDeclarationCollection declarations, DataShapeContext dsContext, DynamicScopedLimitsBalancer.LimitStateTable limitStateTable)
		{
			ExpressionNode targetCount = blockInfo.TargetCount;
			ExpressionNode expressionNode = blockInfo.CombinedLimitCount.LessThanOrEqualNoNan(targetCount);
			DynamicLimit primary = primarySecondaryBlock.Primary;
			IntermediateScopedLimitState limitState = limitStateTable.GetLimitState(primary);
			DynamicLimit secondary = primarySecondaryBlock.Secondary;
			IntermediateScopedLimitState limitState2 = limitStateTable.GetLimitState(secondary);
			ExpressionNode expressionNode2;
			ExpressionNode expressionNode3;
			DynamicLimitsTablesBuilder.DetermineBalancedCounts(context.ErrorContext, declarations, dsContext, primary.Count, secondary.Count, limitState.DbCount, limitState2.DbCount, targetCount, targetCount, expressionNode, targetCount, out expressionNode2, out expressionNode3);
			limitState.TargetCountOverride = expressionNode2;
			limitState2.TargetCountOverride = expressionNode3;
		}

		// Token: 0x0200030E RID: 782
		private struct LimitBlockInfo
		{
			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x0600172B RID: 5931 RVA: 0x000526EF File Offset: 0x000508EF
			// (set) Token: 0x0600172C RID: 5932 RVA: 0x000526F7 File Offset: 0x000508F7
			internal ExpressionNode CombinedLimitCount { readonly get; set; }

			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x0600172D RID: 5933 RVA: 0x00052700 File Offset: 0x00050900
			// (set) Token: 0x0600172E RID: 5934 RVA: 0x00052708 File Offset: 0x00050908
			internal ExpressionNode CombinedMandatoryLimitCount { readonly get; set; }

			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x0600172F RID: 5935 RVA: 0x00052711 File Offset: 0x00050911
			// (set) Token: 0x06001730 RID: 5936 RVA: 0x00052719 File Offset: 0x00050919
			internal int NumNonMandatoryConstraints { readonly get; set; }

			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x06001731 RID: 5937 RVA: 0x00052722 File Offset: 0x00050922
			// (set) Token: 0x06001732 RID: 5938 RVA: 0x0005272A File Offset: 0x0005092A
			internal ExpressionNode TargetCount { readonly get; set; }
		}

		// Token: 0x0200030F RID: 783
		private sealed class LimitStateTable
		{
			// Token: 0x06001733 RID: 5939 RVA: 0x00052734 File Offset: 0x00050934
			internal LimitStateTable(ExpressionTable expressionTable, IReadOnlyList<IntermediateScopedLimitState> primaryLimitStates, IReadOnlyList<IntermediateScopedLimitState> secondaryLimitStates)
			{
				this._expressionTable = expressionTable;
				int num2;
				if (primaryLimitStates == null)
				{
					int? num = ((secondaryLimitStates != null) ? new int?(secondaryLimitStates.Count) : null);
					num2 = ((num != null) ? new int?(num.GetValueOrDefault()) : null).GetValueOrDefault();
				}
				else
				{
					num2 = primaryLimitStates.Count;
				}
				this._limitStatesByLimit = new Dictionary<Limit, IntermediateScopedLimitState>(num2, ReferenceEqualityComparer<Limit>.Instance);
				if (primaryLimitStates != null)
				{
					foreach (IntermediateScopedLimitState intermediateScopedLimitState in primaryLimitStates)
					{
						this._limitStatesByLimit.Add(intermediateScopedLimitState.Limit, intermediateScopedLimitState);
					}
				}
				if (secondaryLimitStates != null)
				{
					foreach (IntermediateScopedLimitState intermediateScopedLimitState2 in secondaryLimitStates)
					{
						this._limitStatesByLimit.Add(intermediateScopedLimitState2.Limit, intermediateScopedLimitState2);
					}
				}
			}

			// Token: 0x06001734 RID: 5940 RVA: 0x0005283C File Offset: 0x00050A3C
			internal IntermediateScopedLimitState GetLimitState(DynamicLimit dynamicLimit)
			{
				ResolvedLimitReferenceExpressionNode nodeAs = this._expressionTable.GetNodeAs<ResolvedLimitReferenceExpressionNode>(dynamicLimit.LimitRef);
				return this._limitStatesByLimit[nodeAs.Limit];
			}

			// Token: 0x04000B3C RID: 2876
			private readonly ExpressionTable _expressionTable;

			// Token: 0x04000B3D RID: 2877
			private readonly Dictionary<Limit, IntermediateScopedLimitState> _limitStatesByLimit;
		}
	}
}
