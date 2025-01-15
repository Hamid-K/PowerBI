using System;
using System.IO;

namespace Microsoft.OData.Client
{
	// Token: 0x02000028 RID: 40
	internal sealed class ContentStream
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00007BC1 File Offset: 0x00005DC1
		public ContentStream(Stream stream, bool isKnownMemoryStream)
		{
			this.stream = stream;
			this.isKnownMemoryStream = isKnownMemoryStream;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007BD7 File Offset: 0x00005DD7
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00007BDF File Offset: 0x00005DDF
		public bool IsKnownMemoryStream
		{
			get
			{
				return this.isKnownMemoryStream;
			}
		}

		// Token: 0x0400006F RID: 111
		private readonly Stream stream;

		// Token: 0x04000070 RID: 112
		private readonly bool isKnownMemoryStream;
	}
}
