using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002A0 RID: 672
	internal class BufferedWriteStream : BufferedWriteStreamBase
	{
		// Token: 0x0600189D RID: 6301 RVA: 0x00063AA8 File Offset: 0x00061CA8
		public BufferedWriteStream(Stream store, int bufferSize)
			: base(store, bufferSize)
		{
		}
	}
}
