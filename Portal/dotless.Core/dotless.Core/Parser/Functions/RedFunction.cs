using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200009F RID: 159
	public class RedFunction : ColorFunctionBase
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x00016DF6 File Offset: 0x00014FF6
		protected override Node Eval(Color color)
		{
			return new Number(color.RGB[0]);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016E08 File Offset: 0x00015008
		protected override Node EditColor(Color color, Number number)
		{
			base.WarnNotSupportedByLessJS("red(color, number)");
			double num = number.Value;
			if (number.Unit == "%")
			{
				num = num * 255.0 / 100.0;
			}
			return new Color(color.R + num, color.G, color.B);
		}
	}
}
