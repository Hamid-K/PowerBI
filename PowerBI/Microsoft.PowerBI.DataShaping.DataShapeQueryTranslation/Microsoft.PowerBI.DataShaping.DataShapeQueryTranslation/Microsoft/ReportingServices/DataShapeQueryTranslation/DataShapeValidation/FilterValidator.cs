using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation
{
	// Token: 0x020000CC RID: 204
	internal sealed class FilterValidator : FilterVisitor<FilterCondition>
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x000213B1 File Offset: 0x0001F5B1
		private FilterValidator(Identifier objectId, TranslationErrorContext errorContext, IdentifierValidator idValidator, ExpressionValidator expressionValidator, VisitDataShapeDelegate visitDataShape, FilterUsageKind filterUsageKind)
			: base(visitDataShape)
		{
			this.m_objectId = objectId;
			this.m_errorContext = errorContext;
			this.m_idValidator = idValidator;
			this.m_expressionValidator = expressionValidator;
			this.m_filterUsageKind = filterUsageKind;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000213E0 File Offset: 0x0001F5E0
		public static void Validate(Filter filter, Identifier objectId, TranslationErrorContext errorContext, IdentifierValidator idValidator, ExpressionValidator expressionValidator, VisitDataShapeDelegate visitDataShape)
		{
			new FilterValidator(objectId, errorContext, idValidator, expressionValidator, visitDataShape, filter.UsageKind).Visit(filter);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000213FB File Offset: 0x0001F5FB
		public static void Validate(FilterCondition filter, Identifier objectId, TranslationErrorContext errorContext, IdentifierValidator idValidator, ExpressionValidator expressionValidator, VisitDataShapeDelegate visitDataShape)
		{
			new FilterValidator(objectId, errorContext, idValidator, expressionValidator, visitDataShape, FilterUsageKind.Default).Visit(filter);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00021411 File Offset: 0x0001F611
		protected override FilterCondition Visit(Filter filter)
		{
			this.m_expressionValidator.ValidateRequiredExpression(filter.Target, ObjectType.Filter, this.m_objectId, "Target");
			return base.Visit(filter);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00021438 File Offset: 0x0001F638
		protected override void Visit(List<FilterCondition> conditions, ObjectType objectType, string propertyName)
		{
			if (conditions == null || conditions.Count == 0)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, objectType, this.m_objectId, propertyName));
				return;
			}
			base.Visit(conditions, objectType, propertyName);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00021468 File Offset: 0x0001F668
		protected override FilterCondition Visit(FilterCondition condition)
		{
			if (condition == null)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.FilterCondition, this.m_objectId, "Type"));
				return null;
			}
			this.m_idValidator.ValidateOptionalId(condition);
			return base.Visit(condition);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000214A0 File Offset: 0x0001F6A0
		internal override FilterCondition Visit(UnaryFilterCondition condition)
		{
			this.m_expressionValidator.ValidateRequiredExpression(condition.Expression, condition.ObjectType, this.m_objectId, "Expression");
			this.m_expressionValidator.ValidateCandidateValue<bool>(condition.Not, condition.ObjectType, this.m_objectId, "Not");
			return condition;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000214F4 File Offset: 0x0001F6F4
		internal override FilterCondition Visit(BinaryFilterCondition condition)
		{
			this.m_expressionValidator.ValidateRequiredExpression(condition.LeftExpression, condition.ObjectType, this.m_objectId, "LeftExpression");
			this.m_expressionValidator.ValidateRequiredExpression(condition.RightExpression, condition.ObjectType, this.m_objectId, "RightExpression");
			this.m_expressionValidator.ValidateRequiredCandidateValue<BinaryFilterOperator>(condition.Operator, condition.ObjectType, this.m_objectId, "Operator");
			this.m_expressionValidator.ValidateCandidateValue<bool>(condition.Not, condition.ObjectType, this.m_objectId, "Not");
			return condition;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002158C File Offset: 0x0001F78C
		internal override FilterCondition Visit(InFilterCondition condition)
		{
			List<Expression> expressions = condition.Expressions;
			if (expressions.IsNullOrEmpty<Expression>())
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "Expressions"));
				return null;
			}
			for (int i = 0; i < expressions.Count; i++)
			{
				this.m_expressionValidator.ValidateRequiredExpression(expressions[i], condition.ObjectType, this.m_objectId, "Expressions");
			}
			if (!(condition.HasValues ^ condition.HasTable))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidInFilterValuesAndTable(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "Values"));
				return null;
			}
			if (condition.HasValues)
			{
				return this.ValidateInFilterValues(condition);
			}
			return this.ValidateInFilterTable(condition);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002164C File Offset: 0x0001F84C
		private FilterCondition ValidateInFilterValues(InFilterCondition condition)
		{
			List<Expression> expressions = condition.Expressions;
			List<List<Expression>> values = condition.Values;
			if (values.IsNullOrEmpty<List<Expression>>())
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "Values"));
				return null;
			}
			for (int i = 0; i < values.Count; i++)
			{
				List<Expression> list = values[i];
				if (list == null || list.Count != expressions.Count)
				{
					this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "Values"));
					return null;
				}
				for (int j = 0; j < list.Count; j++)
				{
					this.m_expressionValidator.ValidateRequiredExpression(list[j], condition.ObjectType, this.m_objectId, "Values");
				}
			}
			return condition;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00021720 File Offset: 0x0001F920
		private FilterCondition ValidateInFilterTable(InFilterCondition condition)
		{
			this.m_expressionValidator.ValidateRequiredExpression(condition.Table, condition.ObjectType, this.m_objectId, "Table");
			if (!condition.IdentityComparison)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidInFilterTableWithoutIdentityComparison(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "IdentityComparison"));
			}
			return condition;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002177C File Offset: 0x0001F97C
		internal override FilterCondition Visit(CompoundFilterCondition condition)
		{
			this.m_expressionValidator.ValidateRequiredCandidateValue<CompoundFilterOperator>(condition.Operator, condition.ObjectType, this.m_objectId, "Operator");
			bool inCompoundFilterCondition = this.m_inCompoundFilterCondition;
			this.m_inCompoundFilterCondition = true;
			this.Visit(condition.Conditions, condition.ObjectType, "Conditions");
			this.m_inCompoundFilterCondition = inCompoundFilterCondition;
			return condition;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000217D8 File Offset: 0x0001F9D8
		internal override FilterCondition Visit(ContextFilterCondition condition)
		{
			Contract.RetailAssert(this.m_filterUsageKind == FilterUsageKind.Default, "{0} is allowed only with FilterUsageKind Default", condition.GetType());
			if (!this.VerifyFilterIsNotInCompoundFilterCondition(condition))
			{
				return null;
			}
			if (condition.DataShape == null)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "DataShape"));
				return null;
			}
			return base.Visit(condition);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002183C File Offset: 0x0001FA3C
		internal override FilterCondition Visit(ApplyFilterCondition condition)
		{
			Contract.RetailAssert(this.m_filterUsageKind == FilterUsageKind.Default, "{0} is allowed only with FilterUsageKind Default", condition.GetType());
			if (!this.VerifyFilterIsNotInCompoundFilterCondition(condition))
			{
				return null;
			}
			if (condition.DataShapeReference == null)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "DataShape"));
				return null;
			}
			this.m_expressionValidator.ValidateExpression(condition.DataShapeReference, condition.ObjectType, this.m_objectId, "DataShapeReference");
			return base.Visit(condition);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000218C2 File Offset: 0x0001FAC2
		internal override FilterCondition Visit(FilterEmptyGroupsCondition condition)
		{
			Contract.RetailAssert(this.m_filterUsageKind == FilterUsageKind.Default, "{0} is allowed only with FilterUsageKind Default", condition.GetType());
			this.VerifyFilterIsNotInCompoundFilterCondition(condition);
			return null;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000218E8 File Offset: 0x0001FAE8
		internal override FilterCondition Visit(ExistsFilterCondition condition)
		{
			Contract.RetailAssert(this.m_filterUsageKind == FilterUsageKind.Default, "{0} is allowed only with FilterUsageKind Default", condition.GetType());
			if (!this.VerifyFilterIsNotInCompoundFilterCondition(condition))
			{
				return null;
			}
			if (condition.Items == null || condition.Items.Count == 0)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "Items"));
				return null;
			}
			foreach (ExistsFilterItem existsFilterItem in condition.Items)
			{
				this.ValidateExistsFilterItem(existsFilterItem);
			}
			return condition;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002199C File Offset: 0x0001FB9C
		private void ValidateExistsFilterItem(ExistsFilterItem item)
		{
			if (item.Targets == null || item.Targets.Count == 0)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, ObjectType.ExistsFilterItem, this.m_objectId, "Targets"));
				return;
			}
			foreach (Expression expression in item.Targets)
			{
				this.m_expressionValidator.ValidateRequiredExpression(expression, ObjectType.ExistsFilterItem, this.m_objectId, "Targets");
			}
			this.m_expressionValidator.ValidateRequiredExpression(item.Exists, ObjectType.ExistsFilterItem, this.m_objectId, "Exists");
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00021A54 File Offset: 0x0001FC54
		internal override FilterCondition Visit(AnyValueFilterCondition condition)
		{
			Contract.RetailAssert(this.m_filterUsageKind == FilterUsageKind.Default, "{0} is allowed only with FilterUsageKind Default", condition.GetType());
			if (!this.VerifyFilterIsNotInCompoundFilterCondition(condition))
			{
				return null;
			}
			if (condition.Targets == null || condition.Targets.Count == 0)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "Targets"));
				return null;
			}
			foreach (Expression expression in condition.Targets)
			{
				this.m_expressionValidator.ValidateRequiredExpression(expression, ObjectType.AnyValueFilterCondition, this.m_objectId, "Targets");
			}
			return condition;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00021B18 File Offset: 0x0001FD18
		internal override FilterCondition Visit(DefaultValueFilterCondition condition)
		{
			Contract.RetailAssert(this.m_filterUsageKind == FilterUsageKind.Default, "{0} is allowed only with FilterUsageKind Default", condition.GetType());
			if (!this.VerifyFilterIsNotInCompoundFilterCondition(condition))
			{
				return null;
			}
			if (condition.Targets == null || condition.Targets.Count == 0)
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "Targets"));
				return null;
			}
			foreach (Expression expression in condition.Targets)
			{
				this.m_expressionValidator.ValidateRequiredExpression(expression, ObjectType.DefaultValueFilterCondition, this.m_objectId, "Targets");
			}
			return condition;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00021BDC File Offset: 0x0001FDDC
		private bool VerifyFilterIsNotInCompoundFilterCondition(FilterCondition condition)
		{
			if (this.m_inCompoundFilterCondition)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidNestedFilterCondition(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, condition.ObjectType));
				return false;
			}
			return true;
		}

		// Token: 0x0400042B RID: 1067
		private readonly Identifier m_objectId;

		// Token: 0x0400042C RID: 1068
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x0400042D RID: 1069
		private readonly IdentifierValidator m_idValidator;

		// Token: 0x0400042E RID: 1070
		private readonly ExpressionValidator m_expressionValidator;

		// Token: 0x0400042F RID: 1071
		private bool m_inCompoundFilterCondition;

		// Token: 0x04000430 RID: 1072
		private FilterUsageKind m_filterUsageKind;
	}
}
