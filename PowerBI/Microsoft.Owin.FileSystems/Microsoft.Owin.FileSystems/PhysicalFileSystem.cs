using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Owin.FileSystems
{
	// Token: 0x02000005 RID: 5
	public class PhysicalFileSystem : IFileSystem
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021CA File Offset: 0x000003CA
		public PhysicalFileSystem(string root)
		{
			this.Root = PhysicalFileSystem.GetFullRoot(root);
			if (!Directory.Exists(this.Root))
			{
				throw new DirectoryNotFoundException(this.Root);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F7 File Offset: 0x000003F7
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021FF File Offset: 0x000003FF
		public string Root { get; private set; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002208 File Offset: 0x00000408
		private static string GetFullRoot(string root)
		{
			string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			string fullRoot = Path.GetFullPath(Path.Combine(applicationBase, root));
			if (!fullRoot.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
			{
				fullRoot += Path.DirectorySeparatorChar.ToString();
			}
			return fullRoot;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002260 File Offset: 0x00000460
		private string GetFullPath(string path)
		{
			string text;
			try
			{
				string fullPath = Path.GetFullPath(Path.Combine(this.Root, path));
				if (!fullPath.StartsWith(this.Root, StringComparison.OrdinalIgnoreCase))
				{
					text = null;
				}
				else
				{
					text = fullPath;
				}
			}
			catch
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022AC File Offset: 0x000004AC
		public bool TryGetFileInfo(string subpath, out IFileInfo fileInfo)
		{
			try
			{
				if (subpath.StartsWith("/", StringComparison.Ordinal))
				{
					subpath = subpath.Substring(1);
				}
				string fullPath = this.GetFullPath(subpath);
				if (fullPath != null)
				{
					FileInfo info = new FileInfo(fullPath);
					if (info.Exists && !this.IsRestricted(info))
					{
						fileInfo = new PhysicalFileSystem.PhysicalFileInfo(info);
						return true;
					}
				}
			}
			catch (ArgumentException)
			{
			}
			fileInfo = null;
			return false;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000231C File Offset: 0x0000051C
		public bool TryGetDirectoryContents(string subpath, out IEnumerable<IFileInfo> contents)
		{
			try
			{
				if (subpath.StartsWith("/", StringComparison.Ordinal))
				{
					subpath = subpath.Substring(1);
				}
				string fullPath = this.GetFullPath(subpath);
				if (fullPath != null)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
					if (!directoryInfo.Exists)
					{
						contents = null;
						return false;
					}
					FileSystemInfo[] physicalInfos = directoryInfo.GetFileSystemInfos();
					IFileInfo[] virtualInfos = new IFileInfo[physicalInfos.Length];
					for (int index = 0; index != physicalInfos.Length; index++)
					{
						FileInfo fileInfo = physicalInfos[index] as FileInfo;
						if (fileInfo != null)
						{
							virtualInfos[index] = new PhysicalFileSystem.PhysicalFileInfo(fileInfo);
						}
						else
						{
							virtualInfos[index] = new PhysicalFileSystem.PhysicalDirectoryInfo((DirectoryInfo)physicalInfos[index]);
						}
					}
					contents = virtualInfos;
					return true;
				}
			}
			catch (ArgumentException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			catch (IOException)
			{
			}
			contents = null;
			return false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023F4 File Offset: 0x000005F4
		private bool IsRestricted(FileInfo fileInfo)
		{
			string fileName = Path.GetFileNameWithoutExtension(fileInfo.Name);
			return PhysicalFileSystem.RestrictedFileNames.ContainsKey(fileName);
		}

		// Token: 0x04000004 RID: 4
		private static readonly Dictionary<string, string> RestrictedFileNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"con",
				string.Empty
			},
			{
				"prn",
				string.Empty
			},
			{
				"aux",
				string.Empty
			},
			{
				"nul",
				string.Empty
			},
			{
				"com1",
				string.Empty
			},
			{
				"com2",
				string.Empty
			},
			{
				"com3",
				string.Empty
			},
			{
				"com4",
				string.Empty
			},
			{
				"com5",
				string.Empty
			},
			{
				"com6",
				string.Empty
			},
			{
				"com7",
				string.Empty
			},
			{
				"com8",
				string.Empty
			},
			{
				"com9",
				string.Empty
			},
			{
				"lpt1",
				string.Empty
			},
			{
				"lpt2",
				string.Empty
			},
			{
				"lpt3",
				string.Empty
			},
			{
				"lpt4",
				string.Empty
			},
			{
				"lpt5",
				string.Empty
			},
			{
				"lpt6",
				string.Empty
			},
			{
				"lpt7",
				string.Empty
			},
			{
				"lpt8",
				string.Empty
			},
			{
				"lpt9",
				string.Empty
			},
			{
				"clock$",
				string.Empty
			}
		};

		// Token: 0x02000007 RID: 7
		private class PhysicalFileInfo : IFileInfo
		{
			// Token: 0x0600001F RID: 31 RVA: 0x00002686 File Offset: 0x00000886
			public PhysicalFileInfo(FileInfo info)
			{
				this._info = info;
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002695 File Offset: 0x00000895
			public long Length
			{
				get
				{
					return this._info.Length;
				}
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000021 RID: 33 RVA: 0x000026A2 File Offset: 0x000008A2
			public string PhysicalPath
			{
				get
				{
					return this._info.FullName;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000022 RID: 34 RVA: 0x000026AF File Offset: 0x000008AF
			public string Name
			{
				get
				{
					return this._info.Name;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000023 RID: 35 RVA: 0x000026BC File Offset: 0x000008BC
			public DateTime LastModified
			{
				get
				{
					return this._info.LastWriteTime;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000024 RID: 36 RVA: 0x000026C9 File Offset: 0x000008C9
			public bool IsDirectory
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000025 RID: 37 RVA: 0x000026CC File Offset: 0x000008CC
			public Stream CreateReadStream()
			{
				return new FileStream(this.PhysicalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 65536, FileOptions.Asynchronous | FileOptions.SequentialScan);
			}

			// Token: 0x0400000B RID: 11
			private readonly FileInfo _info;
		}

		// Token: 0x02000008 RID: 8
		private class PhysicalDirectoryInfo : IFileInfo
		{
			// Token: 0x06000026 RID: 38 RVA: 0x000026E6 File Offset: 0x000008E6
			public PhysicalDirectoryInfo(DirectoryInfo info)
			{
				this._info = info;
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000027 RID: 39 RVA: 0x000026F5 File Offset: 0x000008F5
			public long Length
			{
				get
				{
					return -1L;
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000028 RID: 40 RVA: 0x000026F9 File Offset: 0x000008F9
			public string PhysicalPath
			{
				get
				{
					return this._info.FullName;
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002706 File Offset: 0x00000906
			public string Name
			{
				get
				{
					return this._info.Name;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600002A RID: 42 RVA: 0x00002713 File Offset: 0x00000913
			public DateTime LastModified
			{
				get
				{
					return this._info.LastWriteTime;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002720 File Offset: 0x00000920
			public bool IsDirectory
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00002723 File Offset: 0x00000923
			public Stream CreateReadStream()
			{
				return null;
			}

			// Token: 0x0400000C RID: 12
			private readonly DirectoryInfo _info;
		}
	}
}
