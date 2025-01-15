using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000043 RID: 67
	internal class SqlFixedIntervalEnumerator : SqlRetryIntervalBaseEnumerator
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x0000FEFC File Offset: 0x0000E0FC
		public SqlFixedIntervalEnumerator(TimeSpan gapTimeInterval, TimeSpan maxTimeInterval, TimeSpan minTimeInterval)
			: base(gapTimeInterval, maxTimeInterval, minTimeInterval)
		{
			double num = base.GapTimeInterval.TotalMilliseconds * 1.2;
			double num2 = base.GapTimeInterval.TotalMilliseconds * 0.8;
			this.maxRandom = ((num < 2147483647.0) ? Convert.ToInt32(num) : int.MaxValue);
			this.minRandom = ((num2 < 2147483647.0) ? Convert.ToInt32(num2) : Convert.ToInt32(1288490188.2));
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000FF96 File Offset: 0x0000E196
		protected override TimeSpan GetNextInterval()
		{
			base.Current = TimeSpan.FromMilliseconds((double)this.random.Next(this.minRandom, this.maxRandom));
			return base.Current;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000FDA1 File Offset: 0x0000DFA1
		public override object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x040000DD RID: 221
		private readonly int maxRandom;

		// Token: 0x040000DE RID: 222
		private readonly int minRandom;

		// Token: 0x040000DF RID: 223
		private readonly Random random = new Random();
	}
}
