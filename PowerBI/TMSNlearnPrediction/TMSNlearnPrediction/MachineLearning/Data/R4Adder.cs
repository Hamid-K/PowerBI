using System;
using System.Threading;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000274 RID: 628
	public sealed class R4Adder : Combiner<float>
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0004CEBC File Offset: 0x0004B0BC
		public static R4Adder Instance
		{
			get
			{
				if (R4Adder._instance == null)
				{
					Interlocked.CompareExchange<R4Adder>(ref R4Adder._instance, new R4Adder(), null);
				}
				return R4Adder._instance;
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0004CEDF File Offset: 0x0004B0DF
		private R4Adder()
		{
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0004CEE7 File Offset: 0x0004B0E7
		public override bool IsDefault(float value)
		{
			return value == 0f;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0004CEF1 File Offset: 0x0004B0F1
		public override void Combine(ref float dst, float src)
		{
			dst += src;
		}

		// Token: 0x040007D8 RID: 2008
		private static volatile R4Adder _instance;
	}
}
