using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000074 RID: 116
	public class DifferenceFunction : ColorMixFunction
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00015D0F File Offset: 0x00013F0F
		protected override double Operate(double a, double b)
		{
			return Math.Abs(a - b);
		}
	}
}
