using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200006B RID: 107
	public class BlueFunction : ColorFunctionBase
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x0001563A File Offset: 0x0001383A
		protected override Node Eval(Color color)
		{
			return new Number(color.B);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00015648 File Offset: 0x00013848
		protected override Node EditColor(Color color, Number number)
		{
			base.WarnNotSupportedByLessJS("blue(color, number)");
			double num = number.Value;
			if (number.Unit == "%")
			{
				num = num * 255.0 / 100.0;
			}
			return new Color(color.R, color.G, color.B + num);
		}
	}
}
