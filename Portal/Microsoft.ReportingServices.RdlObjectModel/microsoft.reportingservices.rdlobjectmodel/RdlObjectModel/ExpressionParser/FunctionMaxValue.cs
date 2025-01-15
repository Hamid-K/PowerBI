using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200025E RID: 606
	[Serializable]
	internal sealed class FunctionMaxValue : FunctionAggr
	{
		// Token: 0x060013A1 RID: 5025 RVA: 0x0002F12D File Offset: 0x0002D32D
		public FunctionMaxValue(List<IInternalExpression> args)
			: base(args)
		{
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0002F136 File Offset: 0x0002D336
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Object;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0002F13C File Offset: 0x0002D33C
		public override string WriteSource(NameChanges nameChanges)
		{
			StringBuilder stringBuilder = new StringBuilder("MaxValue(");
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
