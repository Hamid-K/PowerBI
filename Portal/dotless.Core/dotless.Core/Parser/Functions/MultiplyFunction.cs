using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000099 RID: 153
	public class MultiplyFunction : ColorMixFunction
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x00016C0A File Offset: 0x00014E0A
		protected override double Operate(double a, double b)
		{
			return a * b / 255.0;
		}
	}
}
