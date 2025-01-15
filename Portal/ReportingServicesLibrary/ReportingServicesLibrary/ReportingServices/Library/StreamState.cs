using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000085 RID: 133
	internal class StreamState
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x00017297 File Offset: 0x00015497
		public StreamState(Stream stream)
		{
			this.Stream = stream;
		}

		// Token: 0x040002F7 RID: 759
		public Stream Stream;

		// Token: 0x040002F8 RID: 760
		public bool Persisted;
	}
}
