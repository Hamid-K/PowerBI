using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200027A RID: 634
	[Serializable]
	internal class FunctionVariablesCollection : BaseInternalExpression
	{
		// Token: 0x06001419 RID: 5145 RVA: 0x0002FA11 File Offset: 0x0002DC11
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0002FA14 File Offset: 0x0002DC14
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Variables";
		}
	}
}
