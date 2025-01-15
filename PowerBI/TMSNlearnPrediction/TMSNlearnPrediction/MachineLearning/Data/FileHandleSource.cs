using System;
using System.IO;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000134 RID: 308
	public sealed class FileHandleSource : IMultiStreamSource
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00021A2A File Offset: 0x0001FC2A
		public FileHandleSource(IFileHandle file)
		{
			Contracts.CheckValue<IFileHandle>(file, "file");
			Contracts.CheckParam(file.CanRead, "file", "File handle must be readable");
			this._file = file;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00021A59 File Offset: 0x0001FC59
		public int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00021A5C File Offset: 0x0001FC5C
		public string GetPathOrNull(int index)
		{
			Contracts.CheckParam(0 <= index && index < this.Count, "index");
			return null;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00021A79 File Offset: 0x0001FC79
		public Stream Open(int index)
		{
			Contracts.CheckParam(0 <= index && index < this.Count, "index");
			return this._file.OpenReadStream();
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00021AA0 File Offset: 0x0001FCA0
		public TextReader OpenTextReader(int index)
		{
			return new StreamReader(this.Open(index));
		}

		// Token: 0x0400032E RID: 814
		private readonly IFileHandle _file;
	}
}
