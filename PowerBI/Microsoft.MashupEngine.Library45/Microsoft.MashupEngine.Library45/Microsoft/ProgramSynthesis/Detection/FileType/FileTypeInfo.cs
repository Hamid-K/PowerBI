using System;
using System.IO;
using Microsoft.ProgramSynthesis.Detection.Encoding;

namespace Microsoft.ProgramSynthesis.Detection.FileType
{
	// Token: 0x02000ABF RID: 2751
	public class FileTypeInfo
	{
		// Token: 0x06004514 RID: 17684 RVA: 0x000D8587 File Offset: 0x000D6787
		internal FileTypeInfo(EncodingType encodingType, FileType fileType, string path)
			: this(encodingType, fileType)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException("The parameter must contain a file path.", "path");
			}
			this.Path = path;
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x000D85B0 File Offset: 0x000D67B0
		internal FileTypeInfo(EncodingType encodingType, FileType fileType, Stream stream)
			: this(encodingType, fileType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.Stream = stream;
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x000D85CF File Offset: 0x000D67CF
		private FileTypeInfo(EncodingType encodingType, FileType fileType)
		{
			this.EncodingType = encodingType;
			this.FileType = fileType;
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x000D85E5 File Offset: 0x000D67E5
		public EncodingType EncodingType { get; }

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004518 RID: 17688 RVA: 0x000D85ED File Offset: 0x000D67ED
		public FileType FileType { get; }

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x000D85F5 File Offset: 0x000D67F5
		public string Path { get; }

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600451A RID: 17690 RVA: 0x000D85FD File Offset: 0x000D67FD
		public Stream Stream { get; }

		// Token: 0x0600451B RID: 17691 RVA: 0x000D8608 File Offset: 0x000D6808
		public TextReader CreateTextReader()
		{
			if (this.EncodingType == EncodingType.Unknown)
			{
				throw new InvalidOperationException("A TextReader may only be created when the encoding type has been successfully detected.");
			}
			if (this.Path != null)
			{
				return new StreamReader(new FileStream(this.Path, FileMode.Open, FileAccess.Read, FileShare.Read), this.EncodingType.GetEncoding(), false, 1, false);
			}
			return new StreamReader(this.Stream, this.EncodingType.GetEncoding(), false, 1, true);
		}
	}
}
