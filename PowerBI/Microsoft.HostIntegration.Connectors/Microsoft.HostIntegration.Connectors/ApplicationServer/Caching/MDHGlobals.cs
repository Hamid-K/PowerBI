using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200023C RID: 572
	internal static class MDHGlobals
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x0003AF27 File Offset: 0x00039127
		public static int GetLSBOffset(short maskOffSet)
		{
			return (int)(maskOffSet * 4);
		}

		// Token: 0x04000B6C RID: 2924
		internal const int DirectorySizeInBits = 4;

		// Token: 0x04000B6D RID: 2925
		public const int SlotCount = 16;

		// Token: 0x04000B6E RID: 2926
		public static readonly int[] IndexMasks = new int[] { 15, 240, 3840, 61440, 983040, 15728640, 251658240, -268435456 };
	}
}
