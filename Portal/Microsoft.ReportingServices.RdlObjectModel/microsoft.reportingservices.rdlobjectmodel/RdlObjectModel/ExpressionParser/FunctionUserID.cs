using System;
using System.Security.Principal;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200028C RID: 652
	[Serializable]
	internal sealed class FunctionUserID : BaseInternalExpression
	{
		// Token: 0x06001483 RID: 5251 RVA: 0x00030289 File Offset: 0x0002E489
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.String;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0003028D File Offset: 0x0002E48D
		public override string WriteSource(NameChanges nameChanges)
		{
			return "User!UserID";
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00030294 File Offset: 0x0002E494
		public override object Evaluate()
		{
			return WindowsIdentity.GetCurrent().Name;
		}
	}
}
