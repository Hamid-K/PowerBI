using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000286 RID: 646
	[Serializable]
	internal sealed class FunctionReportName : BaseInternalExpression
	{
		// Token: 0x06001457 RID: 5207 RVA: 0x0002FECF File Offset: 0x0002E0CF
		public FunctionReportName()
		{
			this.Name = "";
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0002FEE2 File Offset: 0x0002E0E2
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.String;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0002FEE6 File Offset: 0x0002E0E6
		public override bool IsConstant()
		{
			return true;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0002FEE9 File Offset: 0x0002E0E9
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Globals!ReportName";
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0002FEF0 File Offset: 0x0002E0F0
		public override object Evaluate()
		{
			return "";
		}

		// Token: 0x040006AA RID: 1706
		internal string Name;
	}
}
