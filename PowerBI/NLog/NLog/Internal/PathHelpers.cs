using System;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x02000130 RID: 304
	internal static class PathHelpers
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x00026352 File Offset: 0x00024552
		internal static string CombinePaths(string path, string dir, string file)
		{
			if (dir != null)
			{
				path = Path.Combine(path, dir);
			}
			if (file != null)
			{
				path = Path.Combine(path, file);
			}
			return path;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0002636D File Offset: 0x0002456D
		public static string TrimDirectorySeparators(string path)
		{
			return ((path != null) ? path.TrimEnd(PathHelpers.DirectorySeparatorChars) : null) ?? string.Empty;
		}

		// Token: 0x0400040D RID: 1037
		private static readonly char[] DirectorySeparatorChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};
	}
}
