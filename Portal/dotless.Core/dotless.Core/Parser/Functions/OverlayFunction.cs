using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200009C RID: 156
	public class OverlayFunction : ColorMixFunction
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x00016CC8 File Offset: 0x00014EC8
		protected override double Operate(double a, double b)
		{
			if (a >= 128.0)
			{
				return 255.0 - 2.0 * (255.0 - a) * (255.0 - b) / 255.0;
			}
			return 2.0 * a * b / 255.0;
		}
	}
}
