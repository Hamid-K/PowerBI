using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000071 RID: 113
	internal interface IProjectedDsqExpressionValue
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004CC RID: 1228
		ExpressionNode DsqExpression { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004CD RID: 1229
		string FormatString { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004CE RID: 1230
		IConceptualProperty LineageProperty { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004CF RID: 1231
		// (set) Token: 0x060004D0 RID: 1232
		ProjectedDsqExpression DynamicFormatString { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004D1 RID: 1233
		// (set) Token: 0x060004D2 RID: 1234
		ProjectedDsqExpression DynamicFormatCulture { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004D3 RID: 1235
		bool IsEmpty { get; }

		// Token: 0x060004D4 RID: 1236
		IProjectedDsqExpressionValue CloneWithOverrides(ExpressionNode component = null);
	}
}
