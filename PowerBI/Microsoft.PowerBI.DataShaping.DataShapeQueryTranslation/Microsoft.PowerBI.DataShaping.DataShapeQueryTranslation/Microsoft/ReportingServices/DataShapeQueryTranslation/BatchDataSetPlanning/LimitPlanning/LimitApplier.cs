using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning
{
	// Token: 0x0200023B RID: 571
	internal static class LimitApplier
	{
		// Token: 0x06001384 RID: 4996 RVA: 0x0004B77C File Offset: 0x0004997C
		internal static PlanOperationContext ApplyHierarchyStaticLimitsAndWindows(ILimitPlanningContext context, PlanDeclarationCollection declarations, PlanOperationContext inputTable, DataShapeContext dsContext, IntermediateMemberDiscardConditions memberDiscardConditions, LimitMetadataTableBuilder limitMetadataTableBuilder, PlanLimitInfoBuilder limitInfoBuilder, GroupSynchronizationApplier groupSyncApplier, bool isPrimary, bool useBodyDeclarationName = false)
		{
			IReadOnlyList<Limit> readOnlyList = (isPrimary ? dsContext.PrimaryHierarchyLimits : dsContext.SecondaryHierarchyLimits);
			LimitApplier.LimitDeclarationNameKind limitDeclarationNameKind = (useBodyDeclarationName ? LimitApplier.LimitDeclarationNameKind.Body : (isPrimary ? LimitApplier.LimitDeclarationNameKind.Primary : LimitApplier.LimitDeclarationNameKind.Secondary));
			if (readOnlyList.Count > 1)
			{
				return LimitApplier.ApplyHierarchyScopedLimits(context, declarations, limitDeclarationNameKind, dsContext, inputTable, readOnlyList, memberDiscardConditions, limitMetadataTableBuilder, limitInfoBuilder, groupSyncApplier);
			}
			IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> readOnlyList2 = (isPrimary ? dsContext.PrimaryDynamicsExcludingContextOnly : dsContext.SecondaryDynamicsExcludingContextOnly);
			PlanOperationContext planOperationContext = LimitApplier.ApplyTotalHierarchyLimit(context.Annotations, context.ErrorContext, dsContext, inputTable, readOnlyList2, null);
			if (!isPrimary)
			{
				return planOperationContext.DeclareIfNotDeclared(LimitApplier.CreateLimitedName(limitDeclarationNameKind, dsContext.DataShape.Id), declarations, false, null, false);
			}
			return WindowTableBuilder.ApplyLegacyPrimaryHierarchySegmentation(planOperationContext, declarations, context.ErrorContext, context.Annotations, dsContext.PrimaryDynamicsExcludingContextOnly, dsContext.PrimaryHierarchyLimit, dsContext.DataShape.RequestedPrimaryLeafCount, dsContext.DataShape.RestartMatchingBehavior, dsContext.DataShape.Id).DeclareIfNotDeclared(LimitApplier.CreateLimitedName(limitDeclarationNameKind, dsContext.DataShape.Id), declarations, false, null, false);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0004B870 File Offset: 0x00049A70
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "PrimaryTable", "SecondaryTable" })]
		internal static global::System.ValueTuple<PlanOperationContext, PlanOperationContext> ApplyPrimaryAndSecondaryScopedLimits(ILimitPlanningContext context, PlanDeclarationCollection declarations, DataShapeContext dsContext, PlanOperationContext primaryInputTable, PlanOperationContext secondaryInputTable, GroupSynchronizationApplier groupSyncApplier, IntermediateMemberDiscardConditions memberDiscardConditions, LimitMetadataTableBuilder limitMetadataBuilder, PlanLimitInfoBuilder limitInfoBuilder)
		{
			PlanOperationContext planOperationContext;
			PlanOperationContext planOperationContext2;
			if (dsContext.DataShape.DynamicLimits != null)
			{
				context.TelemetryInfo.UsedDynamicLimits = true;
				IReadOnlyList<IntermediateScopedLimitState> readOnlyList = LimitApplier.CreateScopedLimitInputTables(context, declarations, LimitApplier.LimitDeclarationNameKind.Primary, primaryInputTable, dsContext.PrimaryHierarchyLimits, dsContext.Id);
				IReadOnlyList<IntermediateScopedLimitState> readOnlyList2 = LimitApplier.CreateScopedLimitInputTables(context, declarations, LimitApplier.LimitDeclarationNameKind.Secondary, secondaryInputTable, dsContext.SecondaryHierarchyLimits, dsContext.Id);
				DynamicScopedLimitsBalancer.DetermineScopedDynamicLimitCounts(context, declarations, dsContext, readOnlyList, readOnlyList2);
				planOperationContext = LimitApplier.ApplyAndJoinScopedLimits(context, declarations, LimitApplier.LimitDeclarationNameKind.Primary, readOnlyList, memberDiscardConditions, dsContext, limitMetadataBuilder, limitInfoBuilder, groupSyncApplier);
				planOperationContext2 = LimitApplier.ApplyAndJoinScopedLimits(context, declarations, LimitApplier.LimitDeclarationNameKind.Secondary, readOnlyList2, memberDiscardConditions, dsContext, limitMetadataBuilder, limitInfoBuilder, groupSyncApplier);
			}
			else
			{
				planOperationContext = LimitApplier.ApplyHierarchyStaticLimitsAndWindows(context, declarations, primaryInputTable, dsContext, memberDiscardConditions, limitMetadataBuilder, limitInfoBuilder, groupSyncApplier, true, false);
				planOperationContext2 = LimitApplier.ApplyHierarchyStaticLimitsAndWindows(context, declarations, secondaryInputTable, dsContext, memberDiscardConditions, limitMetadataBuilder, limitInfoBuilder, groupSyncApplier, false, false);
			}
			return new global::System.ValueTuple<PlanOperationContext, PlanOperationContext>(planOperationContext, planOperationContext2);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0004B928 File Offset: 0x00049B28
		internal static PlanOperationContext ApplyPrimaryHierarchyOnlyLimitsAndWindows(ILimitPlanningContext context, PlanDeclarationCollection declarations, PlanOperationContext inputTable, BatchDataSetPlannerJoinPredicates joinPredicates, DataShapeContext dsContext, DataShapeQueryTranslationTelemetry telemetryInfo, IntermediateMemberDiscardConditions memberDiscardConditions, GroupSynchronizationApplier groupSyncApplier, out PlanLimitInfo limitInfo, out PlanOperationContext limitMetadataContext, out BatchRestartIndicator restartIndicator)
		{
			if (dsContext.PrimaryHierarchyLimits.Count <= 1)
			{
				inputTable = LimitApplier.ApplyPrimaryHierarchyOnlySingleLimit(context, declarations, inputTable, joinPredicates, telemetryInfo, dsContext, out limitInfo, out limitMetadataContext, out restartIndicator);
				return WindowTableBuilder.ApplyLegacyPrimaryHierarchySegmentation(inputTable, declarations, context.ErrorContext, context.Annotations, dsContext.PrimaryDynamicsExcludingContextOnly, dsContext.PrimaryHierarchyLimit, dsContext.DataShape.RequestedPrimaryLeafCount, dsContext.DataShape.RestartMatchingBehavior, dsContext.DataShape.Id);
			}
			restartIndicator = null;
			LimitMetadataTableBuilder limitMetadataTableBuilder = new LimitMetadataTableBuilder(context.OutputExpressionTable, context.ErrorContext);
			PlanLimitInfoBuilder planLimitInfoBuilder = new PlanLimitInfoBuilder();
			IReadOnlyList<IntermediateScopedLimitState> readOnlyList = LimitApplier.CreateScopedLimitInputTables(context, declarations, LimitApplier.LimitDeclarationNameKind.Body, inputTable, dsContext.PrimaryHierarchyLimits, dsContext.Id);
			if (dsContext.DataShape.DynamicLimits != null)
			{
				context.TelemetryInfo.UsedDynamicLimits = true;
				DynamicScopedLimitsBalancer.DetermineScopedDynamicLimitCounts(context, declarations, dsContext, readOnlyList, null);
			}
			PlanOperationContext planOperationContext = LimitApplier.ApplyAndJoinScopedLimits(context, declarations, LimitApplier.LimitDeclarationNameKind.Body, readOnlyList, memberDiscardConditions, dsContext, limitMetadataTableBuilder, planLimitInfoBuilder, groupSyncApplier);
			limitInfo = planLimitInfoBuilder.ToLimitInfo();
			limitMetadataContext = limitMetadataTableBuilder.ToTableContext(dsContext.DataShape);
			return planOperationContext;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0004BA24 File Offset: 0x00049C24
		private static PlanOperationContext ApplyPrimaryHierarchyOnlySingleLimit(ILimitPlanningContext context, PlanDeclarationCollection declarations, PlanOperationContext bodyTable, BatchDataSetPlannerJoinPredicates joinPredicates, DataShapeQueryTranslationTelemetry telemetryInfo, DataShapeContext dsContext, out PlanLimitInfo limitInfo, out PlanOperationContext limitMetadataContext, out BatchRestartIndicator restartIndicator)
		{
			limitInfo = null;
			restartIndicator = null;
			limitMetadataContext = null;
			KeyPointsTable keyPointsTable = null;
			if (BatchDataSetPlanningUtils.UseEnhancedSampling(dsContext))
			{
				global::System.ValueTuple<KeyPointsTable, PlanOperationContext, PlanLimitInfo> valueTuple = EnhancedSamplingKeyPointsTableBuilder.CreateEnhancedSamplingPrimaryOnly(context, declarations, bodyTable, dsContext, joinPredicates, telemetryInfo);
				keyPointsTable = valueTuple.Item1;
				limitMetadataContext = valueTuple.Item2;
				limitInfo = valueTuple.Item3;
			}
			if (dsContext.HasBinnedLineSampleLimit)
			{
				LimitMetadataTableBuilder limitMetadataTableBuilder = new LimitMetadataTableBuilder(context.OutputExpressionTable, context.ErrorContext);
				PlanLimitInfoBuilder planLimitInfoBuilder = new PlanLimitInfoBuilder();
				bodyTable = BatchDataSetPlannerBinnedLineSampleLimitTranslator.Translate(context, declarations, dsContext, bodyTable, dsContext.PrimaryHierarchyLimit, limitMetadataTableBuilder, planLimitInfoBuilder);
				bodyTable = bodyTable.DeclareIfNotDeclared(PlanNames.BodyBinnedSample(dsContext.Id), declarations, false, null, false);
				limitMetadataContext = limitMetadataTableBuilder.ToTableContext(dsContext.DataShape);
				limitInfo = planLimitInfoBuilder.ToLimitInfo();
			}
			else if (dsContext.HasOverlappingPointsSampleLimit)
			{
				LimitMetadataTableBuilder limitMetadataTableBuilder2 = new LimitMetadataTableBuilder(context.OutputExpressionTable, context.ErrorContext);
				PlanLimitInfoBuilder planLimitInfoBuilder2 = new PlanLimitInfoBuilder();
				bodyTable = BatchDataSetPlannerOverlappingPointsSampleLimitTranslator.Translate(context, declarations, context.OutputExpressionTable, dsContext, bodyTable, dsContext.PrimaryHierarchyLimit, limitMetadataTableBuilder2, planLimitInfoBuilder2);
				bodyTable = bodyTable.DeclareIfNotDeclared(PlanNames.BodyOverlappingPointsSample(dsContext.Id), declarations, false, null, false);
				limitMetadataContext = limitMetadataTableBuilder2.ToTableContext(dsContext.DataShape);
				limitInfo = planLimitInfoBuilder2.ToLimitInfo();
			}
			else if (dsContext.HasTopNPerLevelSampleLimit)
			{
				bodyTable = BatchDataSetPlannerTopNPerLevelSampleLimitTranslator.Translate(context, bodyTable, dsContext.PrimaryHierarchyLimit, dsContext.PrimaryDynamicsExcludingContextOnly, out restartIndicator);
				bodyTable = bodyTable.DeclareIfNotDeclared(PlanNames.BodyTopNPerLevel(dsContext.Id), declarations, false, null, false);
			}
			else
			{
				bodyTable = LimitApplier.ApplyTotalHierarchyLimit(context.Annotations, context.ErrorContext, dsContext, bodyTable, dsContext.PrimaryDynamicsExcludingContextOnly, null);
				bodyTable = bodyTable.DeclareIfNotDeclared(PlanNames.BodyLimited(dsContext.Id), declarations, false, null, false);
			}
			if (keyPointsTable != null)
			{
				bodyTable = keyPointsTable.ApplyAndDeclare(bodyTable, PlanNames.BodyWithKeyPoints(dsContext.Id));
			}
			return bodyTable;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0004BBDC File Offset: 0x00049DDC
		internal static PlanOperationContext ApplyTotalHierarchyLimit(DataShapeAnnotations annotations, TranslationErrorContext errorContext, DataShapeContext dsContext, PlanOperationContext table, IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> dynamics, ExpressionNode overrideCount = null)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember = dynamics[dynamics.Count - 1];
			Limit limitWithInnermostTarget = annotations.GetLimitWithInnermostTarget(dataMember);
			if (limitWithInnermostTarget != null)
			{
				PlanOperation planOperation = BatchDataSetPlannerSimpleLimitTranslator.Translate(limitWithInnermostTarget, table.Table, dynamics.ToSortItems(annotations, true), dynamics, errorContext, annotations.SubtotalAnnotations, true, null, overrideCount);
				table = table.ReplaceTable(planOperation, null, null, null);
			}
			return table;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004BC38 File Offset: 0x00049E38
		private static PlanOperationContext ApplyHierarchyScopedLimits(ILimitPlanningContext context, PlanDeclarationCollection declarations, LimitApplier.LimitDeclarationNameKind declarationNameKind, DataShapeContext dsContext, PlanOperationContext inputTable, IReadOnlyList<Limit> limits, IntermediateMemberDiscardConditions memberDiscardConditions, LimitMetadataTableBuilder limitMetadataTableBuilder, PlanLimitInfoBuilder limitInfoBuilder, GroupSynchronizationApplier groupSyncApplier)
		{
			IReadOnlyList<IntermediateScopedLimitState> readOnlyList = LimitApplier.CreateScopedLimitInputTables(context, declarations, declarationNameKind, inputTable, limits, dsContext.Id);
			return LimitApplier.ApplyAndJoinScopedLimits(context, declarations, declarationNameKind, readOnlyList, memberDiscardConditions, dsContext, limitMetadataTableBuilder, limitInfoBuilder, groupSyncApplier);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004BC6C File Offset: 0x00049E6C
		private static IReadOnlyList<IntermediateScopedLimitState> CreateScopedLimitInputTables(ILimitPlanningContext context, PlanDeclarationCollection declarations, LimitApplier.LimitDeclarationNameKind declarationNameKind, PlanOperationContext inputTable, IReadOnlyList<Limit> scopedLimits, Identifier dataShapeId)
		{
			IReadOnlyList<IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>> groupScopesFromLimits = LimitApplier.GetGroupScopesFromLimits(scopedLimits, context.OutputExpressionTable);
			IntermediateScopedLimitState[] array = new IntermediateScopedLimitState[scopedLimits.Count];
			for (int i = scopedLimits.Count - 1; i >= 0; i--)
			{
				Limit limit = scopedLimits[i];
				PlanOperationContext planOperationContext = null;
				if (i > 0)
				{
					inputTable = LimitApplier.RegroupToLimitTargetsAndParents(context, declarations, declarationNameKind, inputTable, groupScopesFromLimits, dataShapeId, i);
					planOperationContext = inputTable;
				}
				PlanOperationContext planOperationContext2;
				if (scopedLimits.Count == 1)
				{
					planOperationContext2 = inputTable;
				}
				else
				{
					planOperationContext2 = LimitApplier.RegroupScopedLimitInput(context, declarations, inputTable, dataShapeId, limit);
				}
				string text = PlanNames.ScopedPreLimitCount(dataShapeId, limit.Id);
				ExpressionNode expressionNode = LimitApplier.CountLimitInput(context, declarations, text, limit, planOperationContext2.Table);
				array[i] = new IntermediateScopedLimitState(limit, groupScopesFromLimits[i], expressionNode, planOperationContext2, planOperationContext);
			}
			return array;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004BD20 File Offset: 0x00049F20
		private static PlanOperationContext RegroupToLimitTargetsAndParents(ILimitPlanningContext context, PlanDeclarationCollection declarations, LimitApplier.LimitDeclarationNameKind declarationNameKind, PlanOperationContext inputTable, IReadOnlyList<IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>> scopesList, Identifier dataShapeId, int limitIndex)
		{
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> list = new List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>(scopesList.Count);
			for (int i = 0; i <= limitIndex; i++)
			{
				list.AddRange(scopesList[i]);
			}
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember = list[list.Count - 1];
			if (!inputTable.RowScopes.IsInnermostScope(context.ScopeTree, dataMember))
			{
				inputTable = BatchDataSetPlanningUtils.ApplyRegroupTable(context.Annotations, inputTable, list, context.ScopeTree, SubtotalUsage.Output, true, false);
				inputTable = inputTable.DeclareIfNotDeclared(LimitApplier.CreateRegroupedToName(declarationNameKind, dataShapeId, dataMember.Id), declarations, false, null, false);
			}
			return inputTable;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004BDAC File Offset: 0x00049FAC
		private static PlanOperationContext RegroupScopedLimitInput(ILimitPlanningContext context, PlanDeclarationCollection declarations, PlanOperationContext inputTable, Identifier dataShapeId, Limit limit)
		{
			List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> groupScopesFromTargets = limit.GetGroupScopesFromTargets(context.OutputExpressionTable);
			foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember dataMember in groupScopesFromTargets)
			{
				bool flag = context.Annotations.DataMemberAnnotations.IsPrimaryMember(dataMember);
				IScope parentScope = context.ScopeTree.GetParentScope(dataMember);
				if (flag)
				{
					context.ScopeTree.IsRoot(parentScope);
				}
			}
			return BatchDataSetPlanningUtils.ApplyRegroupTable(context.Annotations, inputTable, groupScopesFromTargets, context.ScopeTree, SubtotalUsage.Output, true, false).DeclareIfNotDeclared(PlanNames.ScopedPreLimit(dataShapeId, limit.Id), declarations, false, null, false);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0004BE5C File Offset: 0x0004A05C
		private static ExpressionNode CountLimitInput(ILimitPlanningContext context, PlanDeclarationCollection declarations, string planName, Limit limit, PlanOperation scopedInput)
		{
			return scopedInput.CountRows().DeclareIfNotDeclared(planName, declarations, context.ErrorContext, ObjectType.Limit, limit.Id);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0004BE7C File Offset: 0x0004A07C
		private static PlanOperationContext ApplyAndJoinScopedLimits(ILimitPlanningContext context, PlanDeclarationCollection declarations, LimitApplier.LimitDeclarationNameKind declarationNameKind, IReadOnlyList<IntermediateScopedLimitState> limitStates, IntermediateMemberDiscardConditions memberDiscardConditions, DataShapeContext dsContext, LimitMetadataTableBuilder limitMetadataBuilder, PlanLimitInfoBuilder limitInfoBuilder, GroupSynchronizationApplier groupSyncApplier)
		{
			PlanOperationContext planOperationContext = LimitApplier.ApplyScopedLimit(context, declarations, limitStates[0], dsContext, limitMetadataBuilder, limitInfoBuilder, groupSyncApplier);
			for (int i = 1; i < limitStates.Count; i++)
			{
				IntermediateScopedLimitState intermediateScopedLimitState = limitStates[i];
				PlanOperationContext planOperationContext2 = LimitApplier.ApplyScopedLimit(context, declarations, intermediateScopedLimitState, dsContext, limitMetadataBuilder, limitInfoBuilder, groupSyncApplier);
				Identifier id = planOperationContext2.RowScopes.InnermostScope.Id;
				PlanOperationContext planOperationContext3 = planOperationContext2.LeftOuterJoin(intermediateScopedLimitState.ParentAndTargetScopeTable, context.ScopeTree);
				planOperationContext3 = planOperationContext3.DeclareIfNotDeclared(PlanNames.BodyRegroupedToScopedLimited(dsContext.Id, id, intermediateScopedLimitState.Limit.Id), declarations, false, null, false);
				planOperationContext3 = LimitApplier.AddIsValidColumn(context.ErrorContext, context.Schema, planOperationContext3, declarations, intermediateScopedLimitState.GroupScopesFromTargets, memberDiscardConditions);
				bool flag = i == limitStates.Count - 1;
				PlanOperationContext planOperationContext4 = planOperationContext.LeftOuterJoin(planOperationContext3, context.ScopeTree);
				string text = (flag ? LimitApplier.CreateLimitedName(declarationNameKind, dsContext.Id) : LimitApplier.CreateRegroupedToLimitedName(declarationNameKind, dsContext.Id, id));
				planOperationContext = planOperationContext4.DeclareIfNotDeclared(text, declarations, false, null, true);
			}
			return planOperationContext;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0004BF84 File Offset: 0x0004A184
		private static PlanOperationContext ApplyScopedLimit(ILimitPlanningContext context, PlanDeclarationCollection declarations, IntermediateScopedLimitState limitState, DataShapeContext dsContext, LimitMetadataTableBuilder limitMetadataBuilder, PlanLimitInfoBuilder limitInfoBuilder, GroupSynchronizationApplier syncApplier)
		{
			Identifier id = dsContext.Id;
			Limit scopedLimit = limitState.Limit;
			ExpressionNode targetCountOverride = limitState.TargetCountOverride;
			PlanOperationContext targetScopeTable = limitState.TargetScopeTable;
			ExpressionNode dbCount = limitState.DbCount;
			ExpressionNode isExceededDbCount = limitState.DbCount;
			PlanOperation planOperation = BatchDataSetPlannerSimpleLimitTranslator.Translate(scopedLimit, targetScopeTable.Table, limitState.GroupScopesFromTargets.ToSortItems(context.Annotations, true), limitState.GroupScopesFromTargets, context.ErrorContext, context.Annotations.SubtotalAnnotations, false, delegate(PlanOperation postRestartTable)
			{
				PlanOperationDeclarationReference planOperationDeclarationReference = postRestartTable.DeclareIfNotDeclared(PlanNames.PreLimitPostRestart(scopedLimit.Id), declarations, false, false, null, false);
				isExceededDbCount = LimitApplier.CountLimitInput(context, declarations, PlanNames.ScopedPreLimitIsExceededCount(scopedLimit.Id), scopedLimit, planOperationDeclarationReference);
				return planOperationDeclarationReference;
			}, targetCountOverride);
			ExpressionId expressionId = limitMetadataBuilder.AddColumn(PlanNames.ScopedPreLimitCount(id, scopedLimit.Id), dbCount, ObjectType.Limit, scopedLimit.Id);
			ExpressionId? expressionId2 = null;
			if (targetCountOverride != null)
			{
				expressionId2 = new ExpressionId?(limitMetadataBuilder.AddColumn(PlanNames.ScopedLimitCount(id, scopedLimit.Id), targetCountOverride, ObjectType.Limit, scopedLimit.Id));
			}
			ExpressionId? expressionId3 = null;
			if (isExceededDbCount != limitState.DbCount)
			{
				expressionId3 = new ExpressionId?(limitMetadataBuilder.AddColumn(PlanNames.ScopedPreLimitIsExceededCount(scopedLimit.Id), isExceededDbCount, ObjectType.Limit, scopedLimit.Id));
			}
			LimitOverride limitOverride = LimitOverride.OverrideLimit(scopedLimit.Id, expressionId2, new ExpressionId?(expressionId), new ExpressionId?(expressionId3 ?? expressionId), new ExceededDetectionKind?(ExceededDetectionKind.DbCountVsCount));
			limitInfoBuilder.AddLimitOverride(limitOverride);
			planOperation = planOperation.DeclareIfNotDeclared(PlanNames.ScopedLimit(id, scopedLimit.Id), declarations, false, false, null, false);
			PlanOperationContext planOperationContext = targetScopeTable.ReplaceTable(planOperation, null, null, null);
			return syncApplier.AddSyncIndex(dsContext, declarations, context.OutputExpressionTable, limitState.GroupScopesFromTargets, planOperationContext);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0004C178 File Offset: 0x0004A378
		private static PlanOperationContext AddIsValidColumn(TranslationErrorContext errorContext, IFederatedConceptualSchema federatedSchema, PlanOperationContext input, PlanDeclarationCollection declarations, IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember> dataMembers, IntermediateMemberDiscardConditions memberDiscardConditions)
		{
			string value = dataMembers[dataMembers.Count - 1].Id.Value;
			string text = PlanNames.IsValid(value);
			ExpressionNode expressionNode;
			if (federatedSchema.RequiresDistinctForNestedOuterJoins())
			{
				expressionNode = LiteralExpressionNode.True;
				input = input.ReplaceTable(input.Table.DistinctRows(), null, null, null);
			}
			else
			{
				expressionNode = input.IsEmptyTable().Not();
				expressionNode = expressionNode.DeclareIfNotDeclared(text, declarations, errorContext, ObjectType.DataMember, value);
			}
			IntermediateDiscardCondition intermediateDiscardCondition = new IntermediateDiscardCondition(text, true, BatchDiscardConditionOperator.NotEqual);
			memberDiscardConditions.Add(dataMembers[0], intermediateDiscardCondition);
			ExpressionContext expressionContext = new ExpressionContext(errorContext, ObjectType.DataMember, value, text);
			PlanNewColumnProjectItem planNewColumnProjectItem = new PlanNewColumnProjectItem(expressionNode, text, expressionContext, ColumnReuseKind.None);
			PlanOperation planOperation = input.Table.Project(new PlanProjectItem[]
			{
				PlanPreserveAllColumnsProjectItem.Instance,
				planNewColumnProjectItem
			}, false);
			return input.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0004C250 File Offset: 0x0004A450
		private static IReadOnlyList<IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>> GetGroupScopesFromLimits(IReadOnlyList<Limit> limits, ExpressionTable expressionTable)
		{
			List<IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>> list = new List<IReadOnlyList<Microsoft.DataShaping.InternalContracts.DataShapeQuery.DataMember>>(limits.Count);
			foreach (Limit limit in limits)
			{
				list.Add(limit.GetGroupScopesFromTargets(expressionTable));
			}
			return list;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0004C2AC File Offset: 0x0004A4AC
		private static string CreateRegroupedToName(LimitApplier.LimitDeclarationNameKind declarationKind, Identifier dataShapeId, Identifier memberId)
		{
			switch (declarationKind)
			{
			case LimitApplier.LimitDeclarationNameKind.Body:
				return PlanNames.BodyRegroupedTo(dataShapeId, memberId);
			case LimitApplier.LimitDeclarationNameKind.Primary:
				return PlanNames.PrimaryRegroupedTo(dataShapeId, memberId);
			case LimitApplier.LimitDeclarationNameKind.Secondary:
				return PlanNames.SecondaryRegroupedTo(dataShapeId, memberId);
			default:
				Microsoft.DataShaping.Contract.RetailFail("Unrecognized ScopedLimitDeclarationKind {0}", declarationKind);
				return null;
			}
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0004C2EB File Offset: 0x0004A4EB
		private static string CreateRegroupedToLimitedName(LimitApplier.LimitDeclarationNameKind declarationKind, Identifier dataShapeId, Identifier memberId)
		{
			switch (declarationKind)
			{
			case LimitApplier.LimitDeclarationNameKind.Body:
				return PlanNames.BodyRegroupedToLimited(dataShapeId, memberId);
			case LimitApplier.LimitDeclarationNameKind.Primary:
				return PlanNames.PrimaryRegroupedToLimited(dataShapeId, memberId);
			case LimitApplier.LimitDeclarationNameKind.Secondary:
				return PlanNames.SecondaryRegroupedToLimited(dataShapeId, memberId);
			default:
				Microsoft.DataShaping.Contract.RetailFail("Unrecognized ScopedLimitDeclarationKind {0}", declarationKind);
				return null;
			}
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004C32A File Offset: 0x0004A52A
		private static string CreateLimitedName(LimitApplier.LimitDeclarationNameKind declarationKind, Identifier dataShapeId)
		{
			switch (declarationKind)
			{
			case LimitApplier.LimitDeclarationNameKind.Body:
				return PlanNames.BodyLimited(dataShapeId);
			case LimitApplier.LimitDeclarationNameKind.Primary:
				return PlanNames.PrimaryLimited(dataShapeId);
			case LimitApplier.LimitDeclarationNameKind.Secondary:
				return PlanNames.SecondaryLimited(dataShapeId);
			default:
				Microsoft.DataShaping.Contract.RetailFail("Unrecognized ScopedLimitDeclarationKind {0}", declarationKind);
				return null;
			}
		}

		// Token: 0x02000322 RID: 802
		private enum LimitDeclarationNameKind
		{
			// Token: 0x04000B76 RID: 2934
			Body,
			// Token: 0x04000B77 RID: 2935
			Primary,
			// Token: 0x04000B78 RID: 2936
			Secondary
		}
	}
}
