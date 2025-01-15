using System;
using System.IO;
using System.Text;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000030 RID: 48
	public sealed class StreamablePowerBIPackagePartContent : IStreamablePowerBIPackagePartContent
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00005593 File Offset: 0x00003793
		public StreamablePowerBIPackagePartContent(string content, string contentType = "")
			: this((content == null) ? null : Encoding.Unicode.GetBytes(content), contentType)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000055B0 File Offset: 0x000037B0
		public StreamablePowerBIPackagePartContent(byte[] content, string contentType = "")
			: this(delegate
			{
				if (content != null)
				{
					return new MemoryStream(content, false);
				}
				return null;
			}, contentType)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000055DD File Offset: 0x000037DD
		public StreamablePowerBIPackagePartContent(Func<Stream> getPowerBIPackagePartContentReadonlyStream, string contentType = "")
		{
			this.getPowerBiPackagePartContentReadonlyStream = getPowerBIPackagePartContentReadonlyStream;
			this.contentType = contentType;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000055F3 File Offset: 0x000037F3
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000055FC File Offset: 0x000037FC
		public Stream GetStream()
		{
			Stream stream = this.getPowerBiPackagePartContentReadonlyStream();
			if (stream != null)
			{
				MemoryStream memoryStream = stream as MemoryStream;
				return stream;
			}
			return null;
		}

		// Token: 0x040000AD RID: 173
		private readonly Func<Stream> getPowerBiPackagePartContentReadonlyStream;

		// Token: 0x040000AE RID: 174
		private readonly string contentType;
	}
}
