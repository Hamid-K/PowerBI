using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x020000A4 RID: 164
	internal sealed class NormalRandom : Random
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0000FF6C File Offset: 0x0000E16C
		private double BoxMuller()
		{
			int num = 0;
			double num5;
			for (;;)
			{
				double num2 = 2.0 * base.Sample() - 1.0;
				double num3 = 2.0 * base.Sample() - 1.0;
				double num4 = num2 * num2 + num3 * num3;
				if (num4 < 1.0)
				{
					num4 = Math.Sqrt(-2.0 * Math.Log(num4) / num4);
					num5 = num2 * num4;
					num5 = (num5 + 3.0) / 6.0;
					if ((num5 >= 0.0 && num5 < 1.0) || num++ >= 256)
					{
						break;
					}
				}
			}
			if (num >= 256)
			{
				return 0.5;
			}
			return num5;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00010035 File Offset: 0x0000E235
		protected override double Sample()
		{
			return this.BoxMuller();
		}
	}
}
