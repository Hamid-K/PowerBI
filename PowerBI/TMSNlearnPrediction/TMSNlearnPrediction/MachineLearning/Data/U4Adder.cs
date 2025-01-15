using System;
using System.Threading;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000276 RID: 630
	public sealed class U4Adder : Combiner<uint>
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0004CF3A File Offset: 0x0004B13A
		public static U4Adder Instance
		{
			get
			{
				if (U4Adder._instance == null)
				{
					Interlocked.CompareExchange<U4Adder>(ref U4Adder._instance, new U4Adder(), null);
				}
				return U4Adder._instance;
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0004CF5D File Offset: 0x0004B15D
		private U4Adder()
		{
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0004CF65 File Offset: 0x0004B165
		public override bool IsDefault(uint value)
		{
			return value == 0U;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0004CF6B File Offset: 0x0004B16B
		public override void Combine(ref uint dst, uint src)
		{
			dst += src;
		}

		// Token: 0x040007DA RID: 2010
		private static volatile U4Adder _instance;
	}
}
