using System;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200009E RID: 158
	[NullableContext(1)]
	[Nullable(0)]
	internal class ThreadSafeRandom : Random
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0000F530 File Offset: 0x0000D730
		public override int Next()
		{
			Random random = this._random;
			int num;
			lock (random)
			{
				num = this._random.Next();
			}
			return num;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000F578 File Offset: 0x0000D778
		public override int Next(int minValue, int maxValue)
		{
			Random random = this._random;
			int num;
			lock (random)
			{
				num = this._random.Next(minValue, maxValue);
			}
			return num;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		public override int Next(int maxValue)
		{
			Random random = this._random;
			int num;
			lock (random)
			{
				num = this._random.Next(maxValue);
			}
			return num;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000F60C File Offset: 0x0000D80C
		public override double NextDouble()
		{
			Random random = this._random;
			double num;
			lock (random)
			{
				num = this._random.NextDouble();
			}
			return num;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000F654 File Offset: 0x0000D854
		public override void NextBytes(byte[] buffer)
		{
			Random random = this._random;
			lock (random)
			{
				this._random.NextBytes(buffer);
			}
		}

		// Token: 0x04000210 RID: 528
		private readonly Random _random = new Random();
	}
}
