using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation
{
	// Token: 0x020000CF RID: 207
	internal static class VisualAxisValidator
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x000222F0 File Offset: 0x000204F0
		internal static void Validate(VisualAxis visualAxis, Identifier dataShapeId, ExpressionValidator expressionValidator)
		{
			foreach (VisualAxisGroup visualAxisGroup in visualAxis.Groups)
			{
				expressionValidator.ValidateRequiredExpression(visualAxisGroup.Member, ObjectType.VisualAxisGroupMember, dataShapeId, "Member");
			}
		}
	}
}
