using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000274 RID: 628
	[Serializable]
	internal class FunctionParametersCollection : BaseInternalExpression
	{
		// Token: 0x06001407 RID: 5127 RVA: 0x0002F9A5 File Offset: 0x0002DBA5
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
		public override string WriteSource()
		{
			return "Parameters";
		}
	}
}
