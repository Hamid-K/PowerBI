using System;

namespace Microsoft.MachineLearning.Data.IO.Zlib
{
	// Token: 0x020000DC RID: 220
	public static class Constants
	{
		// Token: 0x040001FE RID: 510
		public const int MaxBufferSize = 15;

		// Token: 0x020000DD RID: 221
		public enum Flush
		{
			// Token: 0x04000200 RID: 512
			NoFlush,
			// Token: 0x04000201 RID: 513
			PartialFlush,
			// Token: 0x04000202 RID: 514
			SyncFlush,
			// Token: 0x04000203 RID: 515
			FullFlush,
			// Token: 0x04000204 RID: 516
			Finish,
			// Token: 0x04000205 RID: 517
			Block,
			// Token: 0x04000206 RID: 518
			Trees
		}

		// Token: 0x020000DE RID: 222
		public enum RetCode
		{
			// Token: 0x04000208 RID: 520
			VersionError = -6,
			// Token: 0x04000209 RID: 521
			BufError,
			// Token: 0x0400020A RID: 522
			MemError,
			// Token: 0x0400020B RID: 523
			DataError,
			// Token: 0x0400020C RID: 524
			StreamError,
			// Token: 0x0400020D RID: 525
			Errno,
			// Token: 0x0400020E RID: 526
			OK,
			// Token: 0x0400020F RID: 527
			StreamEnd,
			// Token: 0x04000210 RID: 528
			NeedDict
		}

		// Token: 0x020000DF RID: 223
		public enum Level
		{
			// Token: 0x04000212 RID: 530
			DefaultCompression = -1,
			// Token: 0x04000213 RID: 531
			Level0,
			// Token: 0x04000214 RID: 532
			NoCompression = 0,
			// Token: 0x04000215 RID: 533
			BestSpeed,
			// Token: 0x04000216 RID: 534
			Level1 = 1,
			// Token: 0x04000217 RID: 535
			Level2,
			// Token: 0x04000218 RID: 536
			Level3,
			// Token: 0x04000219 RID: 537
			Level4,
			// Token: 0x0400021A RID: 538
			Level5,
			// Token: 0x0400021B RID: 539
			Level6,
			// Token: 0x0400021C RID: 540
			Level7,
			// Token: 0x0400021D RID: 541
			Level8,
			// Token: 0x0400021E RID: 542
			BestCompression,
			// Token: 0x0400021F RID: 543
			Level9 = 9
		}

		// Token: 0x020000E0 RID: 224
		public enum Strategy
		{
			// Token: 0x04000221 RID: 545
			DefaultStrategy,
			// Token: 0x04000222 RID: 546
			Filtered,
			// Token: 0x04000223 RID: 547
			HuffmanOnly,
			// Token: 0x04000224 RID: 548
			Rle,
			// Token: 0x04000225 RID: 549
			Fixed
		}

		// Token: 0x020000E1 RID: 225
		public enum Type
		{
			// Token: 0x04000227 RID: 551
			Binary,
			// Token: 0x04000228 RID: 552
			ASCII,
			// Token: 0x04000229 RID: 553
			Text = 1,
			// Token: 0x0400022A RID: 554
			Unknown
		}
	}
}
