using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200008B RID: 139
	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Xorshift is a well-known algorithm name")]
	internal class XorshiftRandomBatchGenerator : IRandomNumberBatchGenerator
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00013664 File Offset: 0x00011864
		public XorshiftRandomBatchGenerator(ulong seed)
		{
			this.lastX = seed * 5073061188973594169UL + seed * 8760132611124384359UL + seed * 8900702462021224483UL + seed * 6807056130438027397UL;
			this.lastY = 4477743899113974427UL;
			this.lastZ = 2994213561913849757UL;
			this.lastW = 9123831478480964153UL;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000136DC File Offset: 0x000118DC
		public void NextBatch(ulong[] buffer, int index, int count)
		{
			ulong num = this.lastX;
			ulong num2 = this.lastY;
			ulong num3 = this.lastZ;
			ulong num4 = this.lastW;
			for (int i = 0; i < count; i++)
			{
				ulong num5 = num ^ (num << 11);
				num = num2;
				num2 = num3;
				num3 = num4;
				num4 = num4 ^ (num4 >> 19) ^ (num5 ^ (num5 >> 8));
				buffer[index + i] = num4;
			}
			this.lastX = num;
			this.lastY = num2;
			this.lastZ = num3;
			this.lastW = num4;
		}

		// Token: 0x040001B9 RID: 441
		private const ulong Y = 4477743899113974427UL;

		// Token: 0x040001BA RID: 442
		private const ulong Z = 2994213561913849757UL;

		// Token: 0x040001BB RID: 443
		private const ulong W = 9123831478480964153UL;

		// Token: 0x040001BC RID: 444
		private ulong lastX;

		// Token: 0x040001BD RID: 445
		private ulong lastY;

		// Token: 0x040001BE RID: 446
		private ulong lastZ;

		// Token: 0x040001BF RID: 447
		private ulong lastW;
	}
}
