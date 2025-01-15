using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A6 RID: 166
	public class ScreenFunction : ColorMixFunction
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x0001706A File Offset: 0x0001526A
		protected override double Operate(double a, double b)
		{
			return 255.0 - (255.0 - a) * (255.0 - b) / 255.0;
		}
	}
}
