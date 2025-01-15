using System;
using System.IO;
using Microsoft.Mashup.Common;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200004A RID: 74
	internal sealed class NonDisposableStream : DelegatingStream
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000D8CB File Offset: 0x0000BACB
		public NonDisposableStream(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		public override void Close()
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000D8D6 File Offset: 0x0000BAD6
		protected override void Dispose(bool disposing)
		{
		}
	}
}
