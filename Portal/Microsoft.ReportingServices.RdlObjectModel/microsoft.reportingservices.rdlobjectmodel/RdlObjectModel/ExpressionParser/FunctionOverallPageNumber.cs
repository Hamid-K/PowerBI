using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000280 RID: 640
	[Serializable]
	internal sealed class FunctionOverallPageNumber : BaseInternalExpression
	{
		// Token: 0x0600143D RID: 5181 RVA: 0x0002FDCE File Offset: 0x0002DFCE
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0002FDD2 File Offset: 0x0002DFD2
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Globals!OverallPageNumber";
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0002FDD9 File Offset: 0x0002DFD9
		public override object Evaluate()
		{
			return 1;
		}
	}
}
