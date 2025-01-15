using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000216 RID: 534
	[CannotApplyEqualityOperator]
	public sealed class FileSystemPath : IEquatable<FileSystemPath>, IComparable<FileSystemPath>
	{
		// Token: 0x06000E0C RID: 3596 RVA: 0x00031A74 File Offset: 0x0002FC74
		public FileSystemPath([NotNull] string path)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(path, "path");
			string text = path.Trim();
			if (text.StartsWith("\\\\?\\", StringComparison.OrdinalIgnoreCase))
			{
				this.isUnc = true;
				text = text.Substring("\\\\?\\".Length);
			}
			else
			{
				this.isUnc = false;
			}
			List<string> list = new List<string>(text.Split(new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			}, StringSplitOptions.None));
			if (list.Count == 0)
			{
				throw new FileSystemPathException(path, "Path is logically empty!");
			}
			if (list[0].EndsWith(Path.VolumeSeparatorChar.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				this.volume = list[0].Substring(0, list[0].Length - 1);
				list.RemoveAt(0);
			}
			else
			{
				this.volume = string.Empty;
			}
			this.fileSystemPathParts = list.ToArray();
			if (this.fileSystemPathParts.Length == 0)
			{
				throw new FileSystemPathException(path, "Path is logically empty! There is no file or directory in the path!");
			}
			int length = text.Length;
			if (string.IsNullOrEmpty(this.fileSystemPathParts[this.fileSystemPathParts.Length - 1]))
			{
				this.isUnc = length >= 248;
			}
			else
			{
				this.isUnc = length >= 260;
			}
			this.hashcode = FileSystemPath.CalcHashCode(this.volume, this.fileSystemPathParts);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00031BCC File Offset: 0x0002FDCC
		public FileSystemPath([NotNull] string volume, [NotNull] string[] fileSystemPathParts)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(volume, "volume");
			ExtendedDiagnostics.EnsureArgumentNotNull<string[]>(fileSystemPathParts, "fileSystemPathParts");
			if (fileSystemPathParts.Length == 0)
			{
				throw new FileSystemPathException(volume, fileSystemPathParts, "Path is logically empty! There is no file or directory in the path!");
			}
			this.volume = volume;
			this.fileSystemPathParts = new string[fileSystemPathParts.Length];
			Array.Copy(fileSystemPathParts, this.fileSystemPathParts, fileSystemPathParts.Length);
			int length = new StringBuilder().Append(this.volume).Append(Path.VolumeSeparatorChar).Append(Path.DirectorySeparatorChar)
				.Append(string.Join(Path.DirectorySeparatorChar.ToString(), this.fileSystemPathParts))
				.ToString()
				.Length;
			if (string.IsNullOrEmpty(this.fileSystemPathParts[fileSystemPathParts.Length - 1]))
			{
				this.isUnc = length >= 248;
			}
			else
			{
				this.isUnc = length >= 260;
			}
			this.hashcode = FileSystemPath.CalcHashCode(volume, fileSystemPathParts);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00031CBA File Offset: 0x0002FEBA
		public FileSystemPath(FileSystemPath basePath, string[] subDirectoriesAndOrFile)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00031CC7 File Offset: 0x0002FEC7
		public bool IsUnc
		{
			get
			{
				return this.isUnc;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00031CCF File Offset: 0x0002FECF
		public string Volume
		{
			get
			{
				return this.volume;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00031CD7 File Offset: 0x0002FED7
		public IEnumerable<string> FileSystemPathParts
		{
			get
			{
				return this.fileSystemPathParts;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00031CDF File Offset: 0x0002FEDF
		public int FileSystemPathPartsCount
		{
			get
			{
				return this.fileSystemPathParts.Length;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00031CEC File Offset: 0x0002FEEC
		public string FullPath
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.isUnc)
				{
					stringBuilder.Append("\\\\?\\");
				}
				if (!string.IsNullOrEmpty(this.volume))
				{
					stringBuilder.Append(this.volume).Append(Path.VolumeSeparatorChar).Append(Path.DirectorySeparatorChar);
				}
				stringBuilder.Append(string.Join(Path.DirectorySeparatorChar.ToString(), this.fileSystemPathParts));
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00031D68 File Offset: 0x0002FF68
		public string FullPathUnc
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("\\\\?\\");
				if (!string.IsNullOrEmpty(this.volume))
				{
					stringBuilder.Append(this.volume).Append(Path.VolumeSeparatorChar).Append(Path.DirectorySeparatorChar);
				}
				stringBuilder.Append(string.Join(new string(Path.DirectorySeparatorChar, 1), this.fileSystemPathParts));
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00031DD4 File Offset: 0x0002FFD4
		public FileSystemPath[] BuildDirectoryChain()
		{
			List<FileSystemPath> list = new List<FileSystemPath>();
			int i;
			int k;
			for (i = 0; i < this.fileSystemPathParts.Length; i = k + 1)
			{
				FileSystemPath fileSystemPath = new FileSystemPath(this.volume, this.fileSystemPathParts.Where((string it, int j) => j <= i).ToArray<string>());
				list.Add(fileSystemPath);
				k = i;
			}
			return list.ToArray();
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00031E4C File Offset: 0x0003004C
		public static long GetDirectorySize(string directoryPath, bool recursive = true)
		{
			SearchOption searchOption = (recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			return (long)Directory.EnumerateFiles(directoryPath, "*", searchOption).Sum((string fileInfo) => fileInfo.Length);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00031E94 File Offset: 0x00030094
		private static int CalcHashCode(string volume, string[] fileSystemPathParts)
		{
			int hashcode = volume.GetHashCode();
			Array.ForEach<string>(fileSystemPathParts, delegate(string it)
			{
				hashcode = 31 * hashcode + it.GetHashCode();
			});
			return hashcode;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00031ECC File Offset: 0x000300CC
		public bool Equals(FileSystemPath other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.fileSystemPathParts.Length != other.fileSystemPathParts.Length)
			{
				return false;
			}
			if (!string.Equals(this.volume, other.volume, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			for (int i = 0; i < this.fileSystemPathParts.Length; i++)
			{
				if (!string.Equals(this.fileSystemPathParts[i], other.fileSystemPathParts[i], StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00031F36 File Offset: 0x00030136
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FileSystemPath);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00031F44 File Offset: 0x00030144
		public override int GetHashCode()
		{
			return this.hashcode;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00031F4C File Offset: 0x0003014C
		public override string ToString()
		{
			return new StringBuilder().Append('[').Append(base.GetType().Name).Append(']')
				.Append(' ')
				.Append("isUnc=")
				.Append(this.isUnc)
				.Append("; ")
				.Append("volume=")
				.Append(this.volume)
				.Append("; ")
				.Append("fileSystemPathParts=")
				.Append('(')
				.Append(string.Join(", ", this.fileSystemPathParts))
				.Append(')')
				.ToString();
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00031FF4 File Offset: 0x000301F4
		public int CompareTo(FileSystemPath other)
		{
			if (other == null)
			{
				return 1;
			}
			int num = string.Compare(this.volume, other.volume, StringComparison.OrdinalIgnoreCase);
			if (num != 0)
			{
				return num;
			}
			string text = string.Join("", this.fileSystemPathParts);
			string text2 = string.Join("", other.fileSystemPathParts);
			return string.Compare(text, text2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000583 RID: 1411
		public const string UNC_PREFIX = "\\\\?\\";

		// Token: 0x04000584 RID: 1412
		public const int MAX_NONUNC_PATH_LENGTH = 260;

		// Token: 0x04000585 RID: 1413
		public const int MAX_NONUNC_DIRECTORY_PATH_LENGTH = 248;

		// Token: 0x04000586 RID: 1414
		private readonly bool isUnc;

		// Token: 0x04000587 RID: 1415
		private readonly string[] fileSystemPathParts;

		// Token: 0x04000588 RID: 1416
		private readonly string volume;

		// Token: 0x04000589 RID: 1417
		private readonly int hashcode;
	}
}
