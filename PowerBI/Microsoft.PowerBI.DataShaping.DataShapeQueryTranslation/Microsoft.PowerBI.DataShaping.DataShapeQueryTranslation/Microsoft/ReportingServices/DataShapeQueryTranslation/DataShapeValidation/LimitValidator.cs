using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation
{
	// Token: 0x020000CE RID: 206
	internal sealed class LimitValidator : LimitVisitor<LimitOperator>
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		private LimitValidator(Identifier objectId, TranslationErrorContext errorContext, IdentifierValidator idValidator, ExpressionValidator expressionValidator)
		{
			this.m_objectId = objectId;
			this.m_errorContext = errorContext;
			this.m_idValidator = idValidator;
			this.m_expressionValidator = expressionValidator;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00021D0D File Offset: 0x0001FF0D
		public static void Validate(Limit limit, Identifier objectId, TranslationErrorContext errorContext, IdentifierValidator idValidator, ExpressionValidator expressionValidator)
		{
			new LimitValidator(objectId, errorContext, idValidator, expressionValidator).Visit(limit);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00021D20 File Offset: 0x0001FF20
		protected override LimitOperator Visit(Limit limit)
		{
			this.m_idValidator.ValidateId(limit);
			this.m_expressionValidator.ValidateRequiredExpression(limit.Within, ObjectType.Limit, this.m_objectId, "Within");
			if (limit.Targets.IsNullOrEmpty<Expression>())
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.Limit, this.m_objectId, "Targets"));
				return null;
			}
			foreach (Expression expression in limit.Targets)
			{
				this.m_expressionValidator.ValidateRequiredExpression(expression, ObjectType.Limit, this.m_objectId, "Targets");
			}
			return base.Visit(limit);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00021DE4 File Offset: 0x0001FFE4
		protected override LimitOperator Visit(LimitOperator limitOperator)
		{
			if (limitOperator == null)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.LimitOperator, this.m_objectId, "Type"));
				return null;
			}
			return base.Visit(limitOperator);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00021E10 File Offset: 0x00020010
		internal override LimitOperator Visit(TopLimitOperator limitOperator)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.Count, ObjectType.LimitOperator, this.m_objectId, "Count");
			if (limitOperator.Skip != null && limitOperator.Skip.Value < 0L)
			{
				this.m_errorContext.Register(TranslationMessages.NonNegativeIntegerValueRequired(EngineMessageSeverity.Error, ObjectType.TopLimitOperator, this.m_objectId, "Skip"));
				return null;
			}
			return limitOperator;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00021E7E File Offset: 0x0002007E
		internal override LimitOperator Visit(SampleLimitOperator limitOperator)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.Count, ObjectType.LimitOperator, this.m_objectId, "Count");
			this.m_expressionValidator.ValidateCandidateValue<bool>(limitOperator.PreserveKeyPoints, ObjectType.LimitOperator, this.m_objectId, "PreserveKeyPoints");
			return limitOperator;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00021EBD File Offset: 0x000200BD
		internal override LimitOperator Visit(FirstLimitOperator limitOperator)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.Count, ObjectType.LimitOperator, this.m_objectId, "Count");
			return limitOperator;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00021EDE File Offset: 0x000200DE
		internal override LimitOperator Visit(LastLimitOperator limitOperator)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.Count, ObjectType.LimitOperator, this.m_objectId, "Count");
			return limitOperator;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00021EFF File Offset: 0x000200FF
		internal override LimitOperator Visit(BottomLimitOperator limitOperator)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.Count, ObjectType.LimitOperator, this.m_objectId, "Count");
			return limitOperator;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00021F20 File Offset: 0x00020120
		internal override LimitOperator Visit(BinnedLineSampleLimitOperator limitOperator)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.Count, ObjectType.BinnedLineSampleLimitOperator, this.m_objectId, "Count");
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.MinPointsPerSeries, ObjectType.BinnedLineSampleLimitOperator, this.m_objectId, "MinPointsPerSeries");
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.MaxPointsPerSeries, ObjectType.BinnedLineSampleLimitOperator, this.m_objectId, "MaxPointsPerSeries");
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.MaxDynamicSeriesCount, ObjectType.BinnedLineSampleLimitOperator, this.m_objectId, "MaxDynamicSeriesCount");
			if (limitOperator.Measures.IsNullOrEmpty<Expression>())
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.BinnedLineSampleLimitOperator, this.m_objectId, "Measures"));
			}
			this.m_expressionValidator.ValidateExpressions(limitOperator.Measures, ObjectType.BinnedLineSampleLimitOperator, this.m_objectId, "Measures");
			this.m_expressionValidator.ValidateExpression(limitOperator.PrimaryScalarKey, ObjectType.BinnedLineSampleLimitOperator, this.m_objectId, "PrimaryScalarKey");
			return limitOperator;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00022008 File Offset: 0x00020208
		internal override LimitOperator Visit(OverlappingPointsSampleLimitOperator limitOperator)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.Count, ObjectType.OverlappingPointsSampleLimitOperator, this.m_objectId, "Count");
			if (limitOperator.X != null)
			{
				this.m_expressionValidator.ValidateRequiredExpression(limitOperator.X.Key, ObjectType.OverlappingPointsSampleLimitOperator, this.m_objectId, "X");
			}
			if (limitOperator.Y != null)
			{
				this.m_expressionValidator.ValidateRequiredExpression(limitOperator.Y.Key, ObjectType.OverlappingPointsSampleLimitOperator, this.m_objectId, "Y");
			}
			return limitOperator;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002208C File Offset: 0x0002028C
		internal override LimitOperator Visit(TopNPerLevelLimitOperator limitOperator)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.Count, ObjectType.TopNPerLevelLimitOperator, this.m_objectId, "Count");
			if (!limitOperator.Levels.IsNullOrEmpty<List<Expression>>())
			{
				foreach (List<Expression> list in limitOperator.Levels)
				{
					this.m_expressionValidator.ValidateExpressions(list, ObjectType.TopNPerLevelLimitOperator, this.m_objectId, "Levels");
				}
			}
			if (limitOperator.WindowExpansionInstance != null)
			{
				this.Visit(limitOperator.WindowExpansionInstance);
			}
			return limitOperator;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00022134 File Offset: 0x00020334
		internal override LimitOperator Visit(WindowLimitOperator limitOperator)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<int>(limitOperator.Count, ObjectType.WindowLimitOperator, this.m_objectId, "Count");
			if (!limitOperator.RestartTokens.IsNullOrEmpty<RestartToken>())
			{
				foreach (RestartToken restartToken in limitOperator.RestartTokens)
				{
					foreach (Candidate<ScalarValue> candidate in restartToken)
					{
						this.m_expressionValidator.ValidateCandidateValue<ScalarValue>(candidate, ObjectType.WindowLimitOperator, this.m_objectId, "RestartTokens");
					}
				}
			}
			return limitOperator;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000221FC File Offset: 0x000203FC
		private void Visit(LimitWindowExpansionInstance limitWindowExpansionInstance)
		{
			if (!limitWindowExpansionInstance.Values.IsNullOrEmpty<Expression>())
			{
				this.m_expressionValidator.ValidateExpressions(limitWindowExpansionInstance.Values, ObjectType.TopNPerLevelLimitOperator, this.m_objectId, "Values");
			}
			if (!limitWindowExpansionInstance.WindowValues.IsNullOrEmpty<LimitWindowExpansionValue>())
			{
				foreach (LimitWindowExpansionValue limitWindowExpansionValue in limitWindowExpansionInstance.WindowValues)
				{
					this.m_expressionValidator.ValidateExpressions(limitWindowExpansionValue.Values, ObjectType.TopNPerLevelLimitOperator, this.m_objectId, "WindowValues");
				}
			}
			if (!limitWindowExpansionInstance.Children.IsNullOrEmpty<LimitWindowExpansionInstance>())
			{
				foreach (LimitWindowExpansionInstance limitWindowExpansionInstance2 in limitWindowExpansionInstance.Children)
				{
					this.Visit(limitWindowExpansionInstance2);
				}
			}
		}

		// Token: 0x04000433 RID: 1075
		private readonly Identifier m_objectId;

		// Token: 0x04000434 RID: 1076
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000435 RID: 1077
		private readonly IdentifierValidator m_idValidator;

		// Token: 0x04000436 RID: 1078
		private readonly ExpressionValidator m_expressionValidator;
	}
}
