using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200025C RID: 604
	[Serializable]
	internal sealed class FunctionLookup : FunctionAggr
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x0002EFAF File Offset: 0x0002D1AF
		public FunctionLookup(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0002EFB8 File Offset: 0x0002D1B8
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0002EFBC File Offset: 0x0002D1BC
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this.m_args == null || this.m_args.Count != 4)
			{
				return "";
			}
			return string.Concat(new string[]
			{
				"Lookup(",
				this.m_args[0].WriteSource(nameChanges),
				", ",
				this.m_args[1].WriteSource(nameChanges),
				", ",
				this.m_args[2].WriteSource(nameChanges),
				", ",
				this.m_args[3].WriteSource(nameChanges),
				")"
			});
		}
	}
}
