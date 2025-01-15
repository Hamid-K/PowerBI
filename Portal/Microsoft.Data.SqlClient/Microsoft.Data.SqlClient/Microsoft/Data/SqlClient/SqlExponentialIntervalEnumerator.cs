using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000041 RID: 65
	internal class SqlExponentialIntervalEnumerator : SqlRetryIntervalBaseEnumerator
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0000FC0C File Offset: 0x0000DE0C
		public SqlExponentialIntervalEnumerator(TimeSpan deltaBackoffTime, TimeSpan maxTimeInterval, TimeSpan minTimeInterval)
			: base(deltaBackoffTime, maxTimeInterval, minTimeInterval)
		{
			double num = base.GapTimeInterval.TotalMilliseconds * 1.2;
			double num2 = base.GapTimeInterval.TotalMilliseconds * 0.8;
			this.maxRandom = ((num < 2147483647.0) ? Convert.ToInt32(num) : int.MaxValue);
			this.minRandom = ((num2 < 2147483647.0) ? Convert.ToInt32(num2) : Convert.ToInt32(1288490188.2));
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000FCB0 File Offset: 0x0000DEB0
		protected override TimeSpan GetNextInterval()
		{
			double num = 2.0;
			int num2 = this.internalCounter;
			this.internalCounter = num2 + 1;
			double num3 = (Math.Pow(num, (double)num2) - 1.0) * (double)this.random.Next(this.minRandom, this.maxRandom);
			double num4 = base.MinTimeInterval.TotalMilliseconds + num3;
			num4 = ((num4 < base.MaxTimeInterval.TotalMilliseconds) ? num4 : (this.random.NextDouble() * (base.MaxTimeInterval.TotalMilliseconds * 0.2) + base.MaxTimeInterval.TotalMilliseconds * 0.8));
			TimeSpan timeSpan = TimeSpan.FromMilliseconds(num4);
			base.Current = ((timeSpan < base.MinTimeInterval) ? base.MinTimeInterval : timeSpan);
			return base.Current;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000FD92 File Offset: 0x0000DF92
		public override void Reset()
		{
			base.Reset();
			this.internalCounter = 1;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000FDA1 File Offset: 0x0000DFA1
		public override object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x040000D6 RID: 214
		private int internalCounter = 1;

		// Token: 0x040000D7 RID: 215
		private readonly int maxRandom;

		// Token: 0x040000D8 RID: 216
		private readonly int minRandom;

		// Token: 0x040000D9 RID: 217
		private readonly Random random = new Random();
	}
}
