using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000145 RID: 325
	internal static class FilesHelper
	{
		// Token: 0x0600102E RID: 4142 RVA: 0x00037C1C File Offset: 0x00035E1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDirectorySeperator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00037C30 File Offset: 0x00035E30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDirectorySeperator(string path, int index)
		{
			return FilesHelper.IsDirectorySeperator(path[index]);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00037C3E File Offset: 0x00035E3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string NormalizeDirectoryPath(string path)
		{
			return FilesHelper.GetDirectoryInfoWithNormalizedPath(path).FullName;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00037C4B File Offset: 0x00035E4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DirectoryInfo GetDirectoryInfoWithNormalizedPath(string path)
		{
			return new DirectoryInfo(path.Trim().TrimEnd(FilesHelper.DirectorySeperators));
		}

		// Token: 0x04000B0B RID: 2827
		private static readonly char[] DirectorySeperators = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};
	}
}
