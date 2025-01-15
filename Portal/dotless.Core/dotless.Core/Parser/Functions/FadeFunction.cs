using System;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000068 RID: 104
	public class FadeFunction : AlphaFunction
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x000155D4 File Offset: 0x000137D4
		protected override double ProcessAlpha(double originalAlpha, double newAlpha)
		{
			return newAlpha;
		}
	}
}
