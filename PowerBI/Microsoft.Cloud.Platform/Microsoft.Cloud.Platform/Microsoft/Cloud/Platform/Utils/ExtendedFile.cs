using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000212 RID: 530
	public static class ExtendedFile
	{
		// Token: 0x06000DF5 RID: 3573 RVA: 0x000313FC File Offset: 0x0002F5FC
		public static bool RemoveReadOnlyIfSet([NotNull] string file)
		{
			Ensure.ArgNotNullOrEmpty(file, "file");
			if (!File.Exists(file))
			{
				throw new FileNotFoundException("RemoveReadOnlyIfSet invoked on '" + file + "', but the file isn't there", file);
			}
			FileAttributes attributes = File.GetAttributes(file);
			if (attributes.HasFlag(FileAttributes.ReadOnly))
			{
				File.SetAttributes(file, attributes & ~FileAttributes.ReadOnly);
				return true;
			}
			return false;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003145C File Offset: 0x0002F65C
		public static bool AreEqual([NotNull] string path1, [NotNull] string path2)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(path1, "path1");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(path2, "path2");
			bool flag;
			using (FileStream fileStream = new FileStream(path1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (FileStream fileStream2 = new FileStream(path2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					flag = ExtendedStream.AreEqual(fileStream, fileStream2);
				}
			}
			return flag;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x000314D0 File Offset: 0x0002F6D0
		public static IEnumerable<string> GetFiles(string path, string[] searchPatterns, SearchOption searchOption)
		{
			List<string> list = new List<string>();
			list.AddRange(searchPatterns.SelectMany((string sp) => Directory.GetFiles(path, sp, searchOption)));
			return list;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003150E File Offset: 0x0002F70E
		public static bool Exists(string path)
		{
			if (path.Length < 260)
			{
				return File.Exists(path);
			}
			return NativeMethods.GetFileAttributes(NativeMethods.ToLongPath(path)) != uint.MaxValue;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00031538 File Offset: 0x0002F738
		public static long GetFileSizeInMB(string path)
		{
			long length = new FileInfo(path).Length;
			if (length == 0L)
			{
				return 0L;
			}
			if (length < 1048576L)
			{
				return 1L;
			}
			return length / 1048576L;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003156C File Offset: 0x0002F76C
		public static void Copy([NotNull] string sourceFileName, [NotNull] string destinationFileName, FileMode destinationFileMode, ExtendedFile.CopyOptions options = ExtendedFile.CopyOptions.None, int bufferSize = 65536)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(sourceFileName, "sourceFileName");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(destinationFileName, "destinationFileName");
			ExtendedDiagnostics.EnsureArgument(sourceFileName, "sourceFileName", File.Exists(sourceFileName));
			ExtendedDiagnostics.EnsureArgument(destinationFileName, "destinationFileName", Directory.Exists(Path.GetDirectoryName(destinationFileName)));
			ExtendedDiagnostics.EnsureArgumentIsPositive(bufferSize, "bufferSize");
			if (options.HasFlag(ExtendedFile.CopyOptions.OverwriteDestination) && File.Exists(destinationFileName))
			{
				ExtendedFile.RemoveReadOnlyIfSet(destinationFileName);
			}
			using (FileStream fileStream = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, bufferSize))
			{
				using (FileStream fileStream2 = new FileStream(destinationFileName, destinationFileMode, FileAccess.Write, FileShare.ReadWrite, bufferSize))
				{
					fileStream.CopyTo(fileStream2, bufferSize);
				}
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00031638 File Offset: 0x0002F838
		public static void AppendAllText([NotNull] string path, string contents, ExtendedFile.AppendAllTextOptions options = ExtendedFile.AppendAllTextOptions.None, FileShare fileShare = FileShare.Read, Encoding encoding = null, int bufferSize = 65536)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(path, "path");
			ExtendedDiagnostics.EnsureArgumentIsPositive(bufferSize, "bufferSize");
			using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write, fileShare, bufferSize))
			{
				using (StreamWriter streamWriter = ((encoding != null) ? new StreamWriter(fileStream, encoding) : new StreamWriter(fileStream)))
				{
					if (options.HasFlag(ExtendedFile.AppendAllTextOptions.AppendNewLine))
					{
						streamWriter.WriteLine(contents);
					}
					else
					{
						streamWriter.Write(contents);
					}
				}
			}
		}

		// Token: 0x0400057F RID: 1407
		public const int BufferSize = 65536;

		// Token: 0x04000580 RID: 1408
		private const int c_defaultRetries = 5;

		// Token: 0x04000581 RID: 1409
		private static readonly TimeSpan s_defaultIntervalBetweenRetries = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x020006B8 RID: 1720
		[Flags]
		public enum CopyOptions
		{
			// Token: 0x04001308 RID: 4872
			None = 0,
			// Token: 0x04001309 RID: 4873
			OverwriteDestination = 1
		}

		// Token: 0x020006B9 RID: 1721
		[Flags]
		public enum AppendAllTextOptions
		{
			// Token: 0x0400130B RID: 4875
			None = 0,
			// Token: 0x0400130C RID: 4876
			AppendNewLine = 1
		}
	}
}
