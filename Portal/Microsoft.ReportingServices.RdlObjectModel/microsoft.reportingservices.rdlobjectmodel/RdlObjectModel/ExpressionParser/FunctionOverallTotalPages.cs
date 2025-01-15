using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000281 RID: 641
	[Serializable]
	internal sealed class FunctionOverallTotalPages : BaseInternalExpression
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x0002FDE9 File Offset: 0x0002DFE9
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0002FDED File Offset: 0x0002DFED
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Globals!OverallTotalPages";
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0002FDF4 File Offset: 0x0002DFF4
		public override object Evaluate()
		{
			return 1;
		}
	}
}
