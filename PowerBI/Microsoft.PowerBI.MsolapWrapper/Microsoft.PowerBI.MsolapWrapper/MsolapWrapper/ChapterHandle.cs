using System;

namespace MsolapWrapper
{
	// Token: 0x0200007A RID: 122
	public class ChapterHandle
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00005420 File Offset: 0x00004820
		public ChapterHandle(ulong chapterValue)
		{
			this.Key = chapterValue;
			this.NativeKey = chapterValue;
		}

		// Token: 0x0400018D RID: 397
		public ulong Key = 0UL;

		// Token: 0x0400018E RID: 398
		public ulong NativeKey = 0UL;
	}
}
