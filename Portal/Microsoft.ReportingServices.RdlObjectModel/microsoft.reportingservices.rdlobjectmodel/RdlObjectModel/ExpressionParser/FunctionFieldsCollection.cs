using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000273 RID: 627
	[Serializable]
	internal class FunctionFieldsCollection : BaseInternalExpression
	{
		// Token: 0x06001404 RID: 5124 RVA: 0x0002F993 File Offset: 0x0002DB93
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0002F996 File Offset: 0x0002DB96
		public override string WriteSource()
		{
			return "Fields";
		}
	}
}
