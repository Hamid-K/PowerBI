using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000276 RID: 630
	[Serializable]
	internal class FunctionGlobalsCollection : BaseInternalExpression
	{
		// Token: 0x0600140D RID: 5133 RVA: 0x0002F9C9 File Offset: 0x0002DBC9
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0002F9CC File Offset: 0x0002DBCC
		public override string WriteSource()
		{
			return "Globals";
		}
	}
}
