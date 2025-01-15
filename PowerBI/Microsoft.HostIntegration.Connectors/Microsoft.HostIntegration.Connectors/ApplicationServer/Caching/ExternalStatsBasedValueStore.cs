using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200032B RID: 811
	internal class ExternalStatsBasedValueStore : IValueStore
	{
		// Token: 0x06001D42 RID: 7490 RVA: 0x000586DD File Offset: 0x000568DD
		internal ExternalStatsBasedValueStore(string statsName, StatsValue getStatsValue)
		{
			this._statsName = statsName;
			this._getStatsValue = getStatsValue;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x000586F3 File Offset: 0x000568F3
		public long GetValue()
		{
			return this._getStatsValue(this._statsName);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x000036A9 File Offset: 0x000018A9
		public void Add(long count)
		{
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x000036A9 File Offset: 0x000018A9
		public void Increment()
		{
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x000036A9 File Offset: 0x000018A9
		public void Decrement()
		{
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000036A9 File Offset: 0x000018A9
		public void SetValue(long value)
		{
		}

		// Token: 0x04001039 RID: 4153
		private string _statsName;

		// Token: 0x0400103A RID: 4154
		private StatsValue _getStatsValue;
	}
}
