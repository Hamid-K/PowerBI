using System;

namespace System.IO
{
	// Token: 0x02000008 RID: 8
	public static class PathHelpers
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002135 File Offset: 0x00000335
		public static string Root
		{
			get
			{
				return "c:\\";
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000213C File Offset: 0x0000033C
		public static string Combine(params string[] paths)
		{
			return Path.Combine(paths);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002144 File Offset: 0x00000344
		public static bool IsUnixRootPath(string filePath)
		{
			return !string.IsNullOrEmpty(filePath) && filePath[0] == '/';
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000215C File Offset: 0x0000035C
		public static bool IsWindowsRootPath(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				return false;
			}
			char c = filePath[0];
			return c == '\\' || c == '/' || (filePath.Length >= 2 && ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')) && filePath[1] == ':');
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021B4 File Offset: 0x000003B4
		public static string NormalizePath(string directoryPath)
		{
			if (directoryPath != null)
			{
				char[] array = new char[] { '\\', '/' };
				return PathHelpers.Combine(directoryPath.Split(array, StringSplitOptions.RemoveEmptyEntries));
			}
			return string.Empty;
		}
	}
}
