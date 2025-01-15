using System;
using System.IO;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008A7 RID: 2215
	internal sealed class CreateAndRegisterStreamHandler : IStreamHandler
	{
		// Token: 0x06007933 RID: 31027 RVA: 0x001F3517 File Offset: 0x001F1717
		internal CreateAndRegisterStreamHandler(string streamName, CreateAndRegisterStream createStreamCallback)
		{
			this.m_streamName = streamName;
			this.m_createStreamCallback = createStreamCallback;
		}

		// Token: 0x06007934 RID: 31028 RVA: 0x001F352D File Offset: 0x001F172D
		public Stream OpenStream()
		{
			return this.m_createStreamCallback(this.m_streamName, null, null, null, true, StreamOper.CreateOnly);
		}

		// Token: 0x04003CD9 RID: 15577
		private string m_streamName;

		// Token: 0x04003CDA RID: 15578
		private CreateAndRegisterStream m_createStreamCallback;
	}
}
