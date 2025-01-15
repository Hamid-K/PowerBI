using System;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x0200011A RID: 282
	internal static class FileInfoExt
	{
		// Token: 0x06000EB7 RID: 3767 RVA: 0x0002474E File Offset: 0x0002294E
		public static DateTime GetLastWriteTimeUtc(this FileInfo fileInfo)
		{
			return fileInfo.LastWriteTimeUtc;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00024756 File Offset: 0x00022956
		public static DateTime GetCreationTimeUtc(this FileInfo fileInfo)
		{
			return fileInfo.CreationTimeUtc;
		}
	}
}
