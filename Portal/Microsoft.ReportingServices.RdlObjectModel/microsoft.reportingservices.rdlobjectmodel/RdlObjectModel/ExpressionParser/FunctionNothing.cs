using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000261 RID: 609
	[Serializable]
	internal class FunctionNothing : BaseInternalExpression
	{
		// Token: 0x060013AB RID: 5035 RVA: 0x0002F2DD File Offset: 0x0002D4DD
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0002F2E0 File Offset: 0x0002D4E0
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Nothing";
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0002F2E7 File Offset: 0x0002D4E7
		public override object Evaluate()
		{
			return null;
		}
	}
}
