using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000035 RID: 53
	internal readonly struct QuantileValue
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x00008404 File Offset: 0x00006604
		public QuantileValue(double quantile, double value)
		{
			this.Quantile = quantile;
			this.Value = value;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00008414 File Offset: 0x00006614
		public double Quantile { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000841C File Offset: 0x0000661C
		public double Value { get; }
	}
}
