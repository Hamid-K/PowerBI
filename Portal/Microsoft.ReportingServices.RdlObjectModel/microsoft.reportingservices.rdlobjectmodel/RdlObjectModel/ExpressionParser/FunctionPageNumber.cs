using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000283 RID: 643
	[Serializable]
	internal sealed class FunctionPageNumber : BaseInternalExpression
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x0002FE2C File Offset: 0x0002E02C
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0002FE30 File Offset: 0x0002E030
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Globals!PageNumber";
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0002FE37 File Offset: 0x0002E037
		public override object Evaluate()
		{
			return 1;
		}
	}
}
