using System;
using System.Threading;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000272 RID: 626
	public sealed class TextCombiner : Combiner<DvText>
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0004CE2E File Offset: 0x0004B02E
		public static TextCombiner Instance
		{
			get
			{
				if (TextCombiner._instance == null)
				{
					Interlocked.CompareExchange<TextCombiner>(ref TextCombiner._instance, new TextCombiner(), null);
				}
				return TextCombiner._instance;
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0004CE51 File Offset: 0x0004B051
		private TextCombiner()
		{
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0004CE59 File Offset: 0x0004B059
		public override bool IsDefault(DvText value)
		{
			return value.Length == 0;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0004CE65 File Offset: 0x0004B065
		public override void Combine(ref DvText dst, DvText src)
		{
			Contracts.Check(this.IsDefault(dst));
			dst = src;
		}

		// Token: 0x040007D6 RID: 2006
		private static volatile TextCombiner _instance;
	}
}
