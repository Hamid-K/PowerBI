using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.SemanticValidation
{
	// Token: 0x02000069 RID: 105
	internal sealed class LimitSemanticValidator
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x000136AC File Offset: 0x000118AC
		internal LimitSemanticValidator(ExpressionTable expressionTable, ScopeTree scopeTree, TranslationErrorContext errorContext, DataShapeAnnotations annotations, ExpressionSemanticValidator expressionValidator)
		{
			this.m_expressionTable = expressionTable;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_targetToLimitsMap = new Dictionary<IIdentifiable, LimitsWithAppliedToDataShape>();
			this.m_annotations = annotations;
			this.m_expressionValidator = expressionValidator;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000136E4 File Offset: 0x000118E4
		public void ValidateTargetsAreCovered(IEnumerable<DataMember> dataMembers)
		{
			if (dataMembers.IsNullOrEmpty<DataMember>())
			{
				return;
			}
			DataMember dataMember = dataMembers.First<DataMember>();
			bool flag = this.m_targetToLimitsMap.ContainsKey(dataMember);
			foreach (DataMember dataMember2 in dataMembers)
			{
				if (dataMember2 != dataMember)
				{
					if (dataMember2.ContextOnly)
					{
						break;
					}
					if (flag != this.m_targetToLimitsMap.ContainsKey(dataMember2))
					{
						this.m_errorContext.Register(TranslationMessages.InvalidHierarchyLimitGap(EngineMessageSeverity.Error, ObjectType.DataMember, flag ? dataMember2.Id : dataMember.Id, "Target"));
						flag = true;
					}
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001378C File Offset: 0x0001198C
		public void ValidateLimit(Limit limit, DataShape dataShape)
		{
			this.ValidateOperator(limit, dataShape);
			this.ValidateTargets(limit, dataShape);
			IScope scope = this.ValidateInnermostTarget(limit, dataShape);
			IScope scope2 = this.ValidateWithin(limit);
			if (scope != null && scope2 != null)
			{
				this.ValidateLimitTargetAndWithinParentChildRelationship(scope, scope2, limit.Id.Value);
				this.ValidateLimitTargetAndWithinAreInTheSameDataShape(scope, scope2, limit.Id.Value);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000137F0 File Offset: 0x000119F0
		private IScope ValidateInnermostTarget(Limit limit, DataShape appliesToScope)
		{
			IScope resolvedScope = limit.GetInnermostTarget().GetResolvedScope(this.m_expressionTable);
			this.ValidateTargetScope(limit, resolvedScope);
			return resolvedScope;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00013818 File Offset: 0x00011A18
		private IScope ValidateWithin(Limit limit)
		{
			IScope scope = ((ResolvedScopeReferenceExpressionNode)this.m_expressionTable.GetNode(limit.Within)).Scope;
			this.ValidateWithinScope(limit, scope);
			return scope;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001384C File Offset: 0x00011A4C
		private void ValidateConflictingLimitTarget(Limit limit, IIdentifiable target, DataShape appliesToDataShape)
		{
			LimitsWithAppliedToDataShape limitsWithAppliedToDataShape;
			if (this.m_targetToLimitsMap.TryGetValue(target, out limitsWithAppliedToDataShape))
			{
				if (limitsWithAppliedToDataShape.HasDeclaredLimitAppliedTo(appliesToDataShape))
				{
					this.m_errorContext.Register(TranslationMessages.InvalidConflictingLimits(EngineMessageSeverity.Error, ObjectType.Limit, target.Id, "Target"));
					return;
				}
			}
			else
			{
				limitsWithAppliedToDataShape = new LimitsWithAppliedToDataShape();
				this.m_targetToLimitsMap[target] = limitsWithAppliedToDataShape;
			}
			limitsWithAppliedToDataShape.AddLimitAppliedTo(limit, appliesToDataShape);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000138AC File Offset: 0x00011AAC
		private void ValidateLimitTargetAndWithinParentChildRelationship(IScope targetScope, IScope withinScope, Identifier objectId)
		{
			if (!this.m_scopeTree.IsProperParentScope(withinScope, targetScope))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidLimitScopes(EngineMessageSeverity.Error, ObjectType.Limit, objectId, "Within"));
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000138D8 File Offset: 0x00011AD8
		private void ValidateTargetScope(Limit limit, IScope targetScope)
		{
			if (limit.Operator.ObjectType == ObjectType.LastLimitOperator || limit.Operator.ObjectType == ObjectType.FirstLimitOperator)
			{
				DataMember dataMember = targetScope as DataMember;
				if (dataMember == null)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidLimitOperator(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id, "Operator"));
					return;
				}
				if (dataMember.HasDynamicChild())
				{
					this.m_errorContext.Register(TranslationMessages.InvalidLimitTargetNotInnermostGroup(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id, "Target", ObjectType.DataMember, dataMember.Id, limit.Operator.ObjectType));
					return;
				}
			}
			DataIntersection dataIntersection = targetScope as DataIntersection;
			if (dataIntersection != null && dataIntersection.DataShapes != null)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidIntersectionLimitNotInnerMostScope(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id, "DataIntersections"));
				return;
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00013998 File Offset: 0x00011B98
		private void ValidateWithinScope(Limit limit, IScope withinScope)
		{
			DataMember dataMember = withinScope as DataMember;
			if (dataMember != null)
			{
				if (!dataMember.IsDynamic)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidLimitScopes(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id, "Within"));
					return;
				}
				if (limit.Operator.ObjectType == ObjectType.FirstLimitOperator || limit.Operator.ObjectType == ObjectType.LastLimitOperator)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidLimitWithinDataShapeRequired(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id, "Target", ObjectType.DataMember, dataMember.Id, limit.Operator.ObjectType));
				}
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00013A25 File Offset: 0x00011C25
		private void ValidateLimitTargetAndWithinAreInTheSameDataShape(IScope targetScope, IScope withinScope, Identifier objectId)
		{
			if (this.m_annotations.GetContainingDataShape(targetScope) != this.m_annotations.GetContainingDataShape(withinScope))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidLimitInNestedDataShape(EngineMessageSeverity.Error, ObjectType.Limit, objectId, "Within", "Target"));
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00013A5F File Offset: 0x00011C5F
		private void ValidateOperator(Limit limit, DataShape containingDataShape)
		{
			if (limit.Operator is BinnedLineSampleLimitOperator)
			{
				this.ValidateBinnedLineSampling(limit, containingDataShape);
				return;
			}
			if (limit.Operator is OverlappingPointsSampleLimitOperator)
			{
				this.ValidateOverlappingPointsSample(limit, containingDataShape);
				return;
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00013A90 File Offset: 0x00011C90
		private void ValidateBinnedLineSampling(Limit limit, DataShape containingDataShape)
		{
			BinnedLineSampleLimitOperator binnedLineSampleLimitOperator = (BinnedLineSampleLimitOperator)limit.Operator;
			foreach (Expression expression in binnedLineSampleLimitOperator.Measures)
			{
				this.m_expressionValidator.Validate(expression, ExpressionFeatureFlags.CalculationReferences, limit.Operator.ObjectType, limit.Id, "Measures", containingDataShape);
			}
			if (binnedLineSampleLimitOperator.PrimaryScalarKey != null)
			{
				this.m_expressionValidator.Validate(binnedLineSampleLimitOperator.PrimaryScalarKey, ExpressionFeatureFlags.CalculationReferences, limit.Operator.ObjectType, limit.Id, "PrimaryScalarKey", containingDataShape);
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00013B40 File Offset: 0x00011D40
		private void ValidateOverlappingPointsSample(Limit limit, DataShape containingDataShape)
		{
			OverlappingPointsSampleLimitOperator overlappingPointsSampleLimitOperator = (OverlappingPointsSampleLimitOperator)limit.Operator;
			if (overlappingPointsSampleLimitOperator.X != null)
			{
				this.m_expressionValidator.Validate(overlappingPointsSampleLimitOperator.X.Key, ExpressionFeatureFlags.CalculationReferences, limit.Operator.ObjectType, limit.Id, "X", containingDataShape);
			}
			if (overlappingPointsSampleLimitOperator.Y != null)
			{
				this.m_expressionValidator.Validate(overlappingPointsSampleLimitOperator.Y.Key, ExpressionFeatureFlags.CalculationReferences, limit.Operator.ObjectType, limit.Id, "Y", containingDataShape);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00013BC8 File Offset: 0x00011DC8
		private void ValidateTargets(Limit limit, DataShape appliesToScope)
		{
			IScope resolvedScope = limit.GetInnermostTarget().GetResolvedScope(this.m_expressionTable);
			int count = limit.Targets.Count;
			if (resolvedScope is DataMember)
			{
				this.ValidateContiguousTargets(limit, 0, count - 1, appliesToScope, true);
			}
			if (resolvedScope is DataIntersection)
			{
				this.ValidateTargetsForBothHierarchies(limit, appliesToScope);
				this.ValidateConflictingLimitTarget(limit, resolvedScope, appliesToScope);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00013C24 File Offset: 0x00011E24
		private void ValidateTargetsForBothHierarchies(Limit limit, DataShape appliesToScope)
		{
			int count = limit.Targets.Count;
			int num = -1;
			IScope scope = limit.Targets[0].GetResolvedScope(this.m_expressionTable);
			IScope resolvedScope = limit.GetInnermostTarget().GetResolvedScope(this.m_expressionTable);
			int i = 1;
			while (i < count)
			{
				IScope resolvedScope2 = limit.Targets[i].GetResolvedScope(this.m_expressionTable);
				if (!this.m_scopeTree.IsImmediateParentScope(scope, resolvedScope2))
				{
					if (!this.m_scopeTree.IsImmediateParentScope(scope, resolvedScope))
					{
						this.m_errorContext.Register(TranslationMessages.InvalidLimitTargets(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id.Value, "Targets"));
						return;
					}
					num = i - 1;
					break;
				}
				else
				{
					scope = resolvedScope2;
					i++;
				}
			}
			if (num == -1 || num == count - 1)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidLimitTargets(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id.Value, "Targets"));
				return;
			}
			this.ValidateContiguousTargets(limit, 0, num, appliesToScope, false);
			this.ValidateContiguousTargets(limit, num + 1, count - 1, appliesToScope, false);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00013D34 File Offset: 0x00011F34
		private void ValidateContiguousTargets(Limit limit, int startIndex, int endIndex, DataShape appliesToScope, bool validateForOverlap)
		{
			if (!(limit.Targets[startIndex].GetResolvedScope(this.m_expressionTable) is DataMember))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidLimitTargetsScopeType(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id, "Targets"));
			}
			IScope scope = limit.Targets[startIndex].GetResolvedScope(this.m_expressionTable);
			if (validateForOverlap)
			{
				this.ValidateConflictingLimitTarget(limit, scope, appliesToScope);
			}
			bool flag = limit.Within.GetResolvedScope(this.m_expressionTable) is DataShape;
			for (int i = startIndex + 1; i <= endIndex; i++)
			{
				IScope resolvedScope = limit.Targets[i].GetResolvedScope(this.m_expressionTable);
				if (validateForOverlap)
				{
					this.ValidateConflictingLimitTarget(limit, resolvedScope, appliesToScope);
				}
				if (!(resolvedScope is DataMember) && (!(resolvedScope is DataIntersection) || !flag))
				{
					this.m_errorContext.Register(TranslationMessages.InvalidLimitTargetsScopeType(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id.Value, "Targets"));
					return;
				}
				if (!this.m_scopeTree.IsImmediateParentScope(scope, resolvedScope))
				{
					this.m_errorContext.Register(TranslationMessages.InvalidLimitTargets(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id.Value, "Targets"));
					return;
				}
				scope = resolvedScope;
			}
		}

		// Token: 0x040002A5 RID: 677
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040002A6 RID: 678
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040002A7 RID: 679
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040002A8 RID: 680
		private readonly Dictionary<IIdentifiable, LimitsWithAppliedToDataShape> m_targetToLimitsMap;

		// Token: 0x040002A9 RID: 681
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x040002AA RID: 682
		private readonly ExpressionSemanticValidator m_expressionValidator;
	}
}
