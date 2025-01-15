using System;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x0200000B RID: 11
	internal sealed class LocationPower
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00004940 File Offset: 0x00002B40
		internal LocationPower(double location, double power)
		{
			this.m_xnumLocation = location;
			this.m_xnumPower = power;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00004956 File Offset: 0x00002B56
		internal double Location
		{
			get
			{
				return this.m_xnumLocation;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000495E File Offset: 0x00002B5E
		internal double Power
		{
			get
			{
				return this.m_xnumPower;
			}
		}

		// Token: 0x0400005B RID: 91
		private readonly double m_xnumLocation;

		// Token: 0x0400005C RID: 92
		private readonly double m_xnumPower;
	}
}
