using System;
using System.IO;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000293 RID: 659
	internal class EwsTraceListener : ITraceListener
	{
		// Token: 0x06001743 RID: 5955 RVA: 0x0003F6AA File Offset: 0x0003E6AA
		internal EwsTraceListener()
			: this(Console.Out)
		{
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0003F6B7 File Offset: 0x0003E6B7
		internal EwsTraceListener(TextWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0003F6C6 File Offset: 0x0003E6C6
		public void Trace(string traceType, string traceMessage)
		{
			this.writer.Write(traceMessage);
		}

		// Token: 0x0400134D RID: 4941
		private TextWriter writer;
	}
}
