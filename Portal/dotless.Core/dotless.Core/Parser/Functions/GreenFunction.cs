using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200007D RID: 125
	public class GreenFunction : ColorFunctionBase
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00016399 File Offset: 0x00014599
		protected override Node Eval(Color color)
		{
			return new Number(color.G);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000163A8 File Offset: 0x000145A8
		protected override Node EditColor(Color color, Number number)
		{
			base.WarnNotSupportedByLessJS("green(color, number)");
			double num = number.Value;
			if (number.Unit == "%")
			{
				num = num * 255.0 / 100.0;
			}
			return new Color(color.R, color.G + num, color.B);
		}
	}
}
