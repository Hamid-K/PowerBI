using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200007E RID: 126
	public class HardlightFunction : ColorMixFunction
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x00016410 File Offset: 0x00014610
		protected override double Operate(double a, double b)
		{
			if (b >= 128.0)
			{
				return 255.0 - 2.0 * (255.0 - b) * (255.0 - a) / 255.0;
			}
			return 2.0 * b * a / 255.0;
		}
	}
}
