using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000254 RID: 596
	[Serializable]
	internal sealed class FunctionAggrPrev : FunctionAggr
	{
		// Token: 0x06001381 RID: 4993 RVA: 0x0002EB89 File Offset: 0x0002CD89
		public FunctionAggrPrev(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0002EB92 File Offset: 0x0002CD92
		internal override string DisplayText()
		{
			return "Previous";
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0002EB99 File Offset: 0x0002CD99
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this._Expr == null)
			{
				return "";
			}
			return "Previous(" + this._Expr.WriteSource(nameChanges) + ")";
		}
	}
}
