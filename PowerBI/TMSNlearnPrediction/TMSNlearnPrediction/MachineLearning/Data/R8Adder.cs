using System;
using System.Threading;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000275 RID: 629
	public sealed class R8Adder : Combiner<double>
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0004CEF9 File Offset: 0x0004B0F9
		public static R8Adder Instance
		{
			get
			{
				if (R8Adder._instance == null)
				{
					Interlocked.CompareExchange<R8Adder>(ref R8Adder._instance, new R8Adder(), null);
				}
				return R8Adder._instance;
			}
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0004CF1C File Offset: 0x0004B11C
		private R8Adder()
		{
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0004CF24 File Offset: 0x0004B124
		public override bool IsDefault(double value)
		{
			return value == 0.0;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0004CF32 File Offset: 0x0004B132
		public override void Combine(ref double dst, double src)
		{
			dst += src;
		}

		// Token: 0x040007D9 RID: 2009
		private static volatile R8Adder _instance;
	}
}
