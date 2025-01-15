using System;
using System.IO;
using Microsoft.Mashup.Common;

namespace Microsoft.Internal
{
	// Token: 0x020001B6 RID: 438
	internal class OutsideOfEngineContextStream : DelegatingStream, ILeaveEngineContext<Stream>
	{
		// Token: 0x0600084B RID: 2123 RVA: 0x0000FF57 File Offset: 0x0000E157
		public OutsideOfEngineContextStream(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public Stream Leave()
		{
			return this;
		}
	}
}
