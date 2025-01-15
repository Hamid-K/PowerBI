using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000275 RID: 629
	[Serializable]
	internal class FunctionReportItemsCollection : BaseInternalExpression
	{
		// Token: 0x0600140A RID: 5130 RVA: 0x0002F9B7 File Offset: 0x0002DBB7
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0002F9BA File Offset: 0x0002DBBA
		public override string WriteSource()
		{
			return "ReportItems";
		}
	}
}
