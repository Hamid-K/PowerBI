using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A7 RID: 167
	public class SoftlightFunction : ColorMixFunction
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x000170A0 File Offset: 0x000152A0
		protected override double Operate(double a, double b)
		{
			double num = b * a / 255.0;
			return num + a * (255.0 - (255.0 - a) * (255.0 - b) / 255.0 - num) / 255.0;
		}
	}
}
