using System;
using System.IO;

namespace dotless.Core.Input
{
	// Token: 0x020000B4 RID: 180
	public class FileReader : IFileReader
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00017436 File Offset: 0x00015636
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0001743E File Offset: 0x0001563E
		public IPathResolver PathResolver { get; set; }

		// Token: 0x06000527 RID: 1319 RVA: 0x00017447 File Offset: 0x00015647
		public FileReader()
			: this(new RelativePathResolver())
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00017454 File Offset: 0x00015654
		public FileReader(IPathResolver pathResolver)
		{
			this.PathResolver = pathResolver;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00017463 File Offset: 0x00015663
		public byte[] GetBinaryFileContents(string fileName)
		{
			fileName = this.PathResolver.GetFullPath(fileName);
			return File.ReadAllBytes(fileName);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00017479 File Offset: 0x00015679
		public string GetFileContents(string fileName)
		{
			fileName = this.PathResolver.GetFullPath(fileName);
			return File.ReadAllText(fileName);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001748F File Offset: 0x0001568F
		public bool DoesFileExist(string fileName)
		{
			fileName = this.PathResolver.GetFullPath(fileName);
			return File.Exists(fileName);
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x000174A5 File Offset: 0x000156A5
		public bool UseCacheDependencies
		{
			get
			{
				return true;
			}
		}
	}
}
