using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000260 RID: 608
	[Serializable]
	internal sealed class FunctionMultiLookup : FunctionAggr
	{
		// Token: 0x060013A7 RID: 5031 RVA: 0x0002F218 File Offset: 0x0002D418
		public FunctionMultiLookup(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0002F221 File Offset: 0x0002D421
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0002F224 File Offset: 0x0002D424
		public override string WriteSource(NameChanges nameChanges)
		{
			if (this.m_args == null || this.m_args.Count != 4)
			{
				return "";
			}
			return string.Concat(new string[]
			{
				"MultiLookup(",
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
