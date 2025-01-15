using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200006A RID: 106
	public class AverageFunction : ColorMixFunction
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00015623 File Offset: 0x00013823
		protected override double Operate(double a, double b)
		{
			return (a + b) / 2.0;
		}
	}
}
