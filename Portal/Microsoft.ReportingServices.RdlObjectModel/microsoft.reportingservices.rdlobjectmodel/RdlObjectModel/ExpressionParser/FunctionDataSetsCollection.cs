using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000279 RID: 633
	[Serializable]
	internal class FunctionDataSetsCollection : BaseInternalExpression
	{
		// Token: 0x06001416 RID: 5142 RVA: 0x0002F9FF File Offset: 0x0002DBFF
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0002FA02 File Offset: 0x0002DC02
		public override string WriteSource()
		{
			return "DataSets";
		}
	}
}
