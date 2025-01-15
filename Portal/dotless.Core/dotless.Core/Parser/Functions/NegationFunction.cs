using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200009A RID: 154
	public class NegationFunction : ColorMixFunction
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00016C21 File Offset: 0x00014E21
		protected override double Operate(double a, double b)
		{
			return 255.0 - Math.Abs(255.0 - b - a);
		}
	}
}
