using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000145 RID: 325
	internal static class FilesHelper
	{
		// Token: 0x06001021 RID: 4129 RVA: 0x000378EC File Offset: 0x00035AEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDirectorySeperator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00037900 File Offset: 0x00035B00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDirectorySeperator(string path, int index)
		{
			return FilesHelper.IsDirectorySeperator(path[index]);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0003790E File Offset: 0x00035B0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string NormalizeDirectoryPath(string path)
		{
			return FilesHelper.GetDirectoryInfoWithNormalizedPath(path).FullName;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0003791B File Offset: 0x00035B1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DirectoryInfo GetDirectoryInfoWithNormalizedPath(string path)
		{
			return new DirectoryInfo(path.Trim().TrimEnd(FilesHelper.DirectorySeperators));
		}

		// Token: 0x04000AFE RID: 2814
		private static readonly char[] DirectorySeperators = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};
	}
}
