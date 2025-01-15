using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000029 RID: 41
	internal static class FilesHelper
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00006914 File Offset: 0x00004B14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDirectorySeperator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006928 File Offset: 0x00004B28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDirectorySeperator(string path, int index)
		{
			return FilesHelper.IsDirectorySeperator(path[index]);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006936 File Offset: 0x00004B36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string NormalizeDirectoryPath(string path)
		{
			return FilesHelper.GetDirectoryInfoWithNormalizedPath(path).FullName;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006943 File Offset: 0x00004B43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DirectoryInfo GetDirectoryInfoWithNormalizedPath(string path)
		{
			return new DirectoryInfo(path.Trim().TrimEnd(FilesHelper.DirectorySeperators));
		}

		// Token: 0x040000C9 RID: 201
		private static readonly char[] DirectorySeperators = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};
	}
}
