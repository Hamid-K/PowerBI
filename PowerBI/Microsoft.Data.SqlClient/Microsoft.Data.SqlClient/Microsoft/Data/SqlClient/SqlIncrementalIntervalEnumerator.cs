using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000042 RID: 66
	internal class SqlIncrementalIntervalEnumerator : SqlRetryIntervalBaseEnumerator
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		public SqlIncrementalIntervalEnumerator(TimeSpan timeInterval, TimeSpan maxTimeInterval, TimeSpan minTimeInterval)
			: base(timeInterval, maxTimeInterval, minTimeInterval)
		{
			double num = base.GapTimeInterval.TotalMilliseconds * 1.2;
			double num2 = base.GapTimeInterval.TotalMilliseconds * 0.8;
			this.maxRandom = ((num < 2147483647.0) ? Convert.ToInt32(num) : int.MaxValue);
			this.minRandom = ((num2 < 2147483647.0) ? Convert.ToInt32(num2) : Convert.ToInt32(1288490188.2));
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000FE48 File Offset: 0x0000E048
		protected override TimeSpan GetNextInterval()
		{
			TimeSpan timeSpan = base.Current;
			double num = timeSpan.TotalMilliseconds + (double)this.random.Next(this.minRandom, this.maxRandom);
			num = ((num < base.MaxTimeInterval.TotalMilliseconds) ? num : (this.random.NextDouble() * (base.MaxTimeInterval.TotalMilliseconds * 0.2) + base.MaxTimeInterval.TotalMilliseconds * 0.8));
			TimeSpan timeSpan2 = TimeSpan.FromMilliseconds(num);
			base.Current = ((timeSpan2 < base.MinTimeInterval) ? base.MinTimeInterval : timeSpan2);
			return base.Current;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000FDA1 File Offset: 0x0000DFA1
		public override object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x040000DA RID: 218
		private readonly int maxRandom;

		// Token: 0x040000DB RID: 219
		private readonly int minRandom;

		// Token: 0x040000DC RID: 220
		private readonly Random random = new Random();
	}
}
