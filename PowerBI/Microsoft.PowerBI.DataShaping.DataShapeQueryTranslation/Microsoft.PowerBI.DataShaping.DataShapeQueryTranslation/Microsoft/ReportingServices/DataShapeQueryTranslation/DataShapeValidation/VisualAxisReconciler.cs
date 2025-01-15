using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ModelReconciliation;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation
{
	// Token: 0x020000D0 RID: 208
	internal static class VisualAxisReconciler
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x00022350 File Offset: 0x00020550
		internal static void Reconcile(VisualAxis visualAxis, ExpressionReconciler expressionReconciler, ExpressionTable expressionTable, Identifier objectId, TranslationErrorContext errorContext)
		{
			foreach (VisualAxisGroup visualAxisGroup in visualAxis.Groups)
			{
				if (!(expressionReconciler.Reconcile(visualAxisGroup.Member, ObjectType.VisualAxisGroupMember, objectId, "Member") is ResolvedStructureReferenceExpressionNode))
				{
					Contract.RetailAssert(errorContext.HasError, "An error should have already been registered if the expression does not resolve to a data member");
				}
			}
		}
	}
}
