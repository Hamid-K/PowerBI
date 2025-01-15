using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200029D RID: 669
	internal class BufferedReadStream : BufferedReadWriteStream
	{
		// Token: 0x0600187C RID: 6268 RVA: 0x000634E3 File Offset: 0x000616E3
		internal BufferedReadStream(Stream store, int bufferSize)
			: base(store, bufferSize)
		{
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x00005BEF File Offset: 0x00003DEF
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
