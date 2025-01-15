using System;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Text.RegularExpressions;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200042D RID: 1069
	internal class UnreliableTransportSpec
	{
		// Token: 0x06002527 RID: 9511 RVA: 0x00071EA0 File Offset: 0x000700A0
		public UnreliableTransportSpec(string channelPattern, string actionPattern, int priority, double faultRatio, int ratio, int low, int high)
		{
			this.m_actionPattern = actionPattern;
			this.m_channelPattern = channelPattern;
			this.m_priority = priority;
			this.m_faultRatio = faultRatio;
			this.m_ratio = ratio;
			this.m_low = low;
			this.m_high = high;
			this.m_channelRegex = (string.IsNullOrEmpty(this.m_channelPattern) ? null : new Regex(this.m_channelPattern, RegexOptions.Compiled));
			this.m_actionRegex = (string.IsNullOrEmpty(this.m_actionPattern) ? null : new Regex(this.m_actionPattern, RegexOptions.Compiled));
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x00071F2C File Offset: 0x0007012C
		public string ChannelPattern
		{
			get
			{
				return this.m_channelPattern;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x00071F34 File Offset: 0x00070134
		public string ActionPattern
		{
			get
			{
				return this.m_actionPattern;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x00071F3C File Offset: 0x0007013C
		public bool IsBlocked
		{
			get
			{
				return this.m_ratio == 100 && (this.m_low < 0 || this.m_low >= UnreliableTransportSpec.MaxDelay);
			}
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00071F68 File Offset: 0x00070168
		public TimeSpan GetDelay()
		{
			Random random = new Random();
			int num = random.Next() % 100;
			if (num >= this.m_ratio)
			{
				return TimeSpan.Zero;
			}
			if (random.NextDouble() < this.m_faultRatio)
			{
				return TimeSpan.MinValue;
			}
			if (this.m_low < 0)
			{
				return TimeSpan.MaxValue;
			}
			int num2 = random.Next(this.m_low, this.m_high);
			if (num2 >= UnreliableTransportSpec.MaxDelay)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromMilliseconds((double)num2);
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00071FE0 File Offset: 0x000701E0
		public int Match(string id, Message msg)
		{
			if (this.m_channelRegex != null && !this.m_channelRegex.IsMatch(id))
			{
				return -1;
			}
			if (this.m_actionRegex != null && (msg == null || !this.m_actionRegex.IsMatch(msg.Headers.Action)))
			{
				return -1;
			}
			return this.m_priority;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00072030 File Offset: 0x00070230
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}, {1}:{2}-{3}:{4}", new object[] { this.m_channelPattern, this.m_actionPattern, this.m_low, this.m_high, this.m_ratio });
		}

		// Token: 0x0400169B RID: 5787
		public static int MaxDelay = ConfigFile.Config.GetIntValue("unreliableTransport/maxDelay", false, 10000);

		// Token: 0x0400169C RID: 5788
		private Regex m_channelRegex;

		// Token: 0x0400169D RID: 5789
		private Regex m_actionRegex;

		// Token: 0x0400169E RID: 5790
		private string m_channelPattern;

		// Token: 0x0400169F RID: 5791
		private string m_actionPattern;

		// Token: 0x040016A0 RID: 5792
		private int m_priority;

		// Token: 0x040016A1 RID: 5793
		private double m_faultRatio;

		// Token: 0x040016A2 RID: 5794
		private int m_ratio;

		// Token: 0x040016A3 RID: 5795
		private int m_low;

		// Token: 0x040016A4 RID: 5796
		private int m_high;
	}
}
