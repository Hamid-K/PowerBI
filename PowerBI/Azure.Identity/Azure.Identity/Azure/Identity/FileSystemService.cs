using System;
using System.IO;

namespace Azure.Identity
{
	// Token: 0x0200005E RID: 94
	internal class FileSystemService : IFileSystemService
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000A9A3 File Offset: 0x00008BA3
		public static IFileSystemService Default { get; } = new FileSystemService();

		// Token: 0x06000370 RID: 880 RVA: 0x0000A9AA File Offset: 0x00008BAA
		private FileSystemService()
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A9B2 File Offset: 0x00008BB2
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000A9BA File Offset: 0x00008BBA
		public string ReadAllText(string path)
		{
			return File.ReadAllText(path);
		}
	}
}
