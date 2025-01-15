using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200132C RID: 4908
	internal sealed class GetStreamBinaryValue : StreamedBinaryValue
	{
		// Token: 0x060081BA RID: 33210 RVA: 0x001B8BB4 File Offset: 0x001B6DB4
		public GetStreamBinaryValue(Func<Stream> getStream)
		{
			this.getStream = getStream;
		}

		// Token: 0x060081BB RID: 33211 RVA: 0x001B8BC3 File Offset: 0x001B6DC3
		public override Stream Open()
		{
			return this.getStream();
		}

		// Token: 0x0400469F RID: 18079
		private readonly Func<Stream> getStream;
	}
}
