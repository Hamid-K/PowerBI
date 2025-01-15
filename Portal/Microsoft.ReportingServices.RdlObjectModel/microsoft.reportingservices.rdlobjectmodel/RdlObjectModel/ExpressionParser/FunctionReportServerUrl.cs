using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000288 RID: 648
	[Serializable]
	internal sealed class FunctionReportServerUrl : BaseInternalExpression
	{
		// Token: 0x0600146D RID: 5229 RVA: 0x00030149 File Offset: 0x0002E349
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.String;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0003014D File Offset: 0x0002E34D
		public override bool IsConstant()
		{
			return true;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00030150 File Offset: 0x0002E350
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Globals!ReportServerUrl";
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00030157 File Offset: 0x0002E357
		public override object Evaluate()
		{
			return "";
		}
	}
}
