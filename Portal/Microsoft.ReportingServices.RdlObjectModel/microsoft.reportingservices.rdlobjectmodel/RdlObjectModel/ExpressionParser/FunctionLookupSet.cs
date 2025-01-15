using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200025D RID: 605
	[Serializable]
	internal sealed class FunctionLookupSet : FunctionAggr
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x0002F06D File Offset: 0x0002D26D
		public FunctionLookupSet(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0002F076 File Offset: 0x0002D276
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0002F07C File Offset: 0x0002D27C
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this.m_args == null || this.m_args.Count != 4)
			{
				return "";
			}
			return string.Concat(new string[]
			{
				"LookupSet(",
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
