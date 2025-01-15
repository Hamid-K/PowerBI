using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000075 RID: 117
	public class ExclusionFunction : ColorMixFunction
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00015D21 File Offset: 0x00013F21
		protected override double Operate(double a, double b)
		{
			return a + b * (255.0 - a - a) / 255.0;
		}
	}
}
