using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001C1 RID: 449
	public static class Checksum
	{
		// Token: 0x06000B88 RID: 2952 RVA: 0x00027FD0 File Offset: 0x000261D0
		public static byte[] CalculateFileChecksum(string filePath, ChecksumType checksumType)
		{
			return Checksum.Calculate(new string[] { filePath }, checksumType);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00027FE2 File Offset: 0x000261E2
		public static byte[] CalculateFolderChecksum(string folderPath, ChecksumType checksumType)
		{
			return Checksum.CalculateFoldersChecksum(new string[] { folderPath }, checksumType);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00027FF4 File Offset: 0x000261F4
		public static byte[] CalculateFoldersChecksum(IEnumerable<string> folders, ChecksumType checksumType)
		{
			return Checksum.Calculate(folders.SelectMany((string f) => Directory.EnumerateFiles(f, "*.*", SearchOption.AllDirectories)).ToArray<string>(), checksumType);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00028028 File Offset: 0x00026228
		public static byte[] CalculateFoldersChecksum(IEnumerable<string> folders, ChecksumType checksumType, IEnumerable<string> exemptionFiles)
		{
			return Checksum.Calculate((from file in folders.SelectMany((string f) => Directory.EnumerateFiles(f, "*.*", SearchOption.AllDirectories))
				where !exemptionFiles.Contains(Path.GetFileName(file), StringComparer.OrdinalIgnoreCase)
				select file).ToArray<string>(), checksumType);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00028084 File Offset: 0x00026284
		public static byte[] Calculate(IEnumerable<string> files, ChecksumType checksumType)
		{
			byte[] hash;
			using (MD5 md = MD5.Create())
			{
				string[] array = files.Select((string file) => Path.GetFullPath(file)).ToArray<string>();
				string relativeRoot = Checksum.GetRelativeRoot(array);
				for (int i = 0; i < array.Length; i++)
				{
					Checksum.HashFilePath(array[i], relativeRoot, checksumType, md);
					byte[] array2 = File.ReadAllBytes(array[i]);
					if (i < array.Length - 1)
					{
						md.TransformBlock(array2, 0, array2.Length, array2, 0);
					}
					else
					{
						md.TransformFinalBlock(array2, 0, array2.Length);
					}
				}
				hash = md.Hash;
			}
			return hash;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002813C File Offset: 0x0002633C
		public static byte[] Calculate(IList<byte[]> vectors)
		{
			byte[] hash;
			using (MD5 md = MD5.Create())
			{
				for (int i = 0; i < vectors.Count; i++)
				{
					byte[] array = vectors[i];
					if (i < vectors.Count - 1)
					{
						md.TransformBlock(array, 0, array.Length, array, 0);
					}
					else
					{
						md.TransformFinalBlock(array, 0, array.Length);
					}
				}
				hash = md.Hash;
			}
			return hash;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000281B4 File Offset: 0x000263B4
		private static void HashFilePath(string filePath, string relativeRoot, ChecksumType checksumType, MD5 hasher)
		{
			string text = null;
			switch (checksumType)
			{
			case ChecksumType.IncludeFileContentAndName:
				text = Path.GetFileName(filePath);
				break;
			case ChecksumType.IncludeFileContentAndRelativePath:
				text = filePath.Substring(relativeRoot.Length);
				break;
			case ChecksumType.IncludeFileContentAndAbsolutePath:
				text = Path.GetFullPath(filePath);
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				hasher.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002821C File Offset: 0x0002641C
		private static string GetRelativeRoot(string[] filePaths)
		{
			if (filePaths.Length == 0)
			{
				return string.Empty;
			}
			int num = filePaths.Select((string filePath) => filePath.Length).Min();
			string text = filePaths[0];
			int num2 = -1;
			int i;
			int j;
			for (i = 0; i < num; i = j + 1)
			{
				char pathChar = text[i];
				if (filePaths.Any((string filePath) => char.ToLowerInvariant(filePath[i]) != char.ToLowerInvariant(pathChar)))
				{
					break;
				}
				if (pathChar == Path.DirectorySeparatorChar || pathChar == Path.AltDirectorySeparatorChar)
				{
					num2 = i;
				}
				j = i;
			}
			return text.Substring(0, num2 + 1);
		}
	}
}
