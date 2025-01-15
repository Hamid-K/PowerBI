using System;
using System.Threading;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000273 RID: 627
	public sealed class FloatAdder : Combiner<float>
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0004CE7F File Offset: 0x0004B07F
		public static FloatAdder Instance
		{
			get
			{
				if (FloatAdder._instance == null)
				{
					Interlocked.CompareExchange<FloatAdder>(ref FloatAdder._instance, new FloatAdder(), null);
				}
				return FloatAdder._instance;
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0004CEA2 File Offset: 0x0004B0A2
		private FloatAdder()
		{
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0004CEAA File Offset: 0x0004B0AA
		public override bool IsDefault(float value)
		{
			return value == 0f;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0004CEB4 File Offset: 0x0004B0B4
		public override void Combine(ref float dst, float src)
		{
			dst += src;
		}

		// Token: 0x040007D7 RID: 2007
		private static volatile FloatAdder _instance;
	}
}
