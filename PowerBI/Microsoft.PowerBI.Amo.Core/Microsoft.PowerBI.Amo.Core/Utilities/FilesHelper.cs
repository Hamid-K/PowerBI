using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x0200013A RID: 314
	internal static class FilesHelper
	{
		// Token: 0x060010BC RID: 4284 RVA: 0x0003A520 File Offset: 0x00038720
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDirectorySeperator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0003A534 File Offset: 0x00038734
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDirectorySeperator(string path, int index)
		{
			return FilesHelper.IsDirectorySeperator(path[index]);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0003A542 File Offset: 0x00038742
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string NormalizeDirectoryPath(string path)
		{
			return FilesHelper.GetDirectoryInfoWithNormalizedPath(path).FullName;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0003A54F File Offset: 0x0003874F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DirectoryInfo GetDirectoryInfoWithNormalizedPath(string path)
		{
			return new DirectoryInfo(path.Trim().TrimEnd(FilesHelper.DirectorySeperators));
		}

		// Token: 0x04000AC4 RID: 2756
		private static readonly char[] DirectorySeperators = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};
	}
}
