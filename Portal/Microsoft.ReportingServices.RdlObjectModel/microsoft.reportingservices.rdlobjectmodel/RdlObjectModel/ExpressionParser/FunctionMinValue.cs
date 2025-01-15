using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200025F RID: 607
	[Serializable]
	internal sealed class FunctionMinValue : FunctionAggr
	{
		// Token: 0x060013A4 RID: 5028 RVA: 0x0002F1A4 File Offset: 0x0002D3A4
		public FunctionMinValue(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0002F1AD File Offset: 0x0002D3AD
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0002F1B0 File Offset: 0x0002D3B0
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("MinValue(");
			for (int i = 0; i < this.m_args.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this.m_args[i].WriteSource());
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}
	}
}
