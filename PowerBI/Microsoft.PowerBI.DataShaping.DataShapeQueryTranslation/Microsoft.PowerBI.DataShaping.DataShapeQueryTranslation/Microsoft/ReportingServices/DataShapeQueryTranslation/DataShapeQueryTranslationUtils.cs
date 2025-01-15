using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000045 RID: 69
	internal static class DataShapeQueryTranslationUtils
	{
		// Token: 0x060002CB RID: 715 RVA: 0x00007EA0 File Offset: 0x000060A0
		internal static DataShapeQueryTranslationUtils.SubtotalKind DetermineSubtotalKind(DataShapeContext dsContext, DataShapeAnnotations annotations, ScopeTree scopeTree, IScope containingScope)
		{
			switch (containingScope.ObjectType)
			{
			case ObjectType.DataIntersection:
			{
				DataIntersection dataIntersection = (DataIntersection)containingScope;
				if (scopeTree.GetPrimaryParentScope(dataIntersection) == dsContext.LastPrimaryDynamic)
				{
					return DataShapeQueryTranslationUtils.SubtotalKind.ColumnTotal;
				}
				if (scopeTree.GetSecondaryParentScope(dataIntersection) == dsContext.LastSecondaryDynamic)
				{
					return DataShapeQueryTranslationUtils.SubtotalKind.RowTotal;
				}
				return DataShapeQueryTranslationUtils.SubtotalKind.RowAndColumnTotal;
			}
			case ObjectType.DataMember:
			{
				DataMember dataMember = (DataMember)containingScope;
				if (annotations.IsPrimaryMember(dataMember))
				{
					DataMember lastPrimaryDynamic = dsContext.LastPrimaryDynamic;
					if (dataMember == lastPrimaryDynamic)
					{
						return DataShapeQueryTranslationUtils.SubtotalKind.ColumnTotal;
					}
					if (!dsContext.HasAnySecondaryDynamic)
					{
						return DataShapeQueryTranslationUtils.SubtotalKind.RowTotal;
					}
				}
				else
				{
					DataMember lastSecondaryDynamic = dsContext.LastSecondaryDynamic;
					if (dataMember == lastSecondaryDynamic)
					{
						return DataShapeQueryTranslationUtils.SubtotalKind.RowTotal;
					}
					if (!dsContext.HasAnyPrimaryDynamic)
					{
						return DataShapeQueryTranslationUtils.SubtotalKind.ColumnTotal;
					}
				}
				return DataShapeQueryTranslationUtils.SubtotalKind.RowAndColumnTotal;
			}
			case ObjectType.DataShape:
				return DataShapeQueryTranslationUtils.SubtotalKind.GrandTotal;
			}
			throw new NotSupportedException("Unsupported containing scope for a subtotal calculation.");
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00007F4D File Offset: 0x0000614D
		public static void ValidateCandidateValue<T>(this Candidate<T> candidate, TranslationErrorContext errorContext, ObjectType objectType, Identifier objectId, string propertyName)
		{
			if (candidate != null && !candidate.IsValid)
			{
				errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, objectType, objectId, propertyName));
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00007F70 File Offset: 0x00006170
		public static void ValidateRequiredCandidateValue<T>(this Candidate<T> candidate, TranslationErrorContext errorContext, ObjectType objectType, Identifier objectId, string propertyName)
		{
			if (candidate == null)
			{
				errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, objectType, objectId, propertyName));
			}
			candidate.ValidateCandidateValue(errorContext, objectType, objectId, propertyName);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007F96 File Offset: 0x00006196
		public static ExpressionContext CreateValueExpressionContext(this IIdentifiable owner, TranslationErrorContext errorContext)
		{
			return new ExpressionContext(errorContext, owner.ObjectType, owner.Id, "Value");
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00007FAF File Offset: 0x000061AF
		internal static DataShape GetApplyFilterDataShape(this ApplyFilterCondition applyFilter, ExpressionTable expressionTable)
		{
			ResolvedScopeReferenceExpressionNode nodeAs = expressionTable.GetNodeAs<ResolvedScopeReferenceExpressionNode>(applyFilter.DataShapeReference);
			return ((nodeAs != null) ? nodeAs.Target : null) as DataShape;
		}

		// Token: 0x0200026E RID: 622
		internal enum SubtotalKind
		{
			// Token: 0x04000975 RID: 2421
			None,
			// Token: 0x04000976 RID: 2422
			RowTotal,
			// Token: 0x04000977 RID: 2423
			ColumnTotal,
			// Token: 0x04000978 RID: 2424
			RowAndColumnTotal,
			// Token: 0x04000979 RID: 2425
			GrandTotal
		}
	}
}
