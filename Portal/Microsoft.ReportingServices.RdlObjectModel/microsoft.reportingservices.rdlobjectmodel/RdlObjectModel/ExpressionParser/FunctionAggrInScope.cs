using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200024E RID: 590
	[Serializable]
	internal sealed class FunctionAggrInScope : FunctionAggr
	{
		// Token: 0x0600136E RID: 4974 RVA: 0x0002E903 File Offset: 0x0002CB03
		public FunctionAggrInScope(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0002E90C File Offset: 0x0002CB0C
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0002E910 File Offset: 0x0002CB10
		public override string WriteSource(NameChanges nameChanges)
		{
			string scopeAsStringForWrite = base.GetScopeAsStringForWrite(nameChanges);
			return "InScope(" + scopeAsStringForWrite + ")";
		}
	}
}
