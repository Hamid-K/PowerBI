using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000277 RID: 631
	[Serializable]
	internal class FunctionUserCollection : BaseInternalExpression
	{
		// Token: 0x06001410 RID: 5136 RVA: 0x0002F9DB File Offset: 0x0002DBDB
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0002F9DE File Offset: 0x0002DBDE
		public override string WriteSource()
		{
			return "User";
		}
	}
}
