using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000213 RID: 531
	public static class ExtendedDirectory
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x000316EC File Offset: 0x0002F8EC
		public static void CreateDirectory([NotNull] string path)
		{
			Ensure.ArgNotNullOrEmpty(path, "path");
			if (!NativeMethods.CreateDirectory(path, IntPtr.Zero))
			{
				int lastError = NativeMethods.GetLastError();
				throw new IOException(new StringBuilder().Append("Can't create directory ").Append('\'').Append(path)
					.Append('\'')
					.Append('!')
					.ToString(), lastError);
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00031750 File Offset: 0x0002F950
		public static bool Exists([NotNull] string path)
		{
			Ensure.ArgNotNullOrEmpty(path, "path");
			uint fileAttributes = NativeMethods.GetFileAttributes(path);
			return fileAttributes != uint.MaxValue && (fileAttributes & 16U) == 16U;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00031780 File Offset: 0x0002F980
		public static IEnumerable<string> GetFilesExact([NotNull] string path, [NotNull] string searchPattern, SearchOption searchOption)
		{
			Ensure.ArgNotNullOrEmpty(path, "path");
			Ensure.ArgNotNullOrEmpty(searchPattern, "searchPattern");
			string[] files = Directory.GetFiles(path, searchPattern, searchOption);
			int length = searchPattern.Length;
			if (length > 3)
			{
				string lastThreeCharsInPattern = searchPattern.Substring(length - 3);
				if (lastThreeCharsInPattern.IndexOf('*') == -1 && lastThreeCharsInPattern.IndexOf('.') == -1 && lastThreeCharsInPattern.IndexOf('?') == -1 && (searchPattern[length - 4] == '*' || (length > 4 && searchPattern.Substring(length - 5, 2) == "*.")))
				{
					return files.Where((string file) => file.EndsWith(lastThreeCharsInPattern, StringComparison.OrdinalIgnoreCase)).Materialize<string>();
				}
			}
			return files;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003183F File Offset: 0x0002FA3F
		public static long GetDirectorySizeInBytes(string directoryPath, IEnumerable<string> extensions, SearchOption searchOption)
		{
			return ExtendedDirectory.EnumerateFiles(directoryPath, extensions, searchOption).Sum((FileInfo fi) => fi.Length);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003186D File Offset: 0x0002FA6D
		public static long GetDirectorySizeInBytes(string directoryPath, string searchPattern, SearchOption searchOption)
		{
			return new DirectoryInfo(directoryPath).EnumerateFiles(searchPattern, searchOption).Sum((FileInfo fi) => fi.Length);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x000318A0 File Offset: 0x0002FAA0
		public static IEnumerable<FileInfo> EnumerateFiles(string directoryPath, IEnumerable<string> extensions, SearchOption searchOption)
		{
			return from file in new DirectoryInfo(directoryPath).EnumerateFiles("*.*", searchOption)
				where extensions.Any((string ext) => ext.Equals(Path.GetExtension(file.Name), StringComparison.OrdinalIgnoreCase))
				select file;
		}
	}
}
