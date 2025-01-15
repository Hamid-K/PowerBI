using System;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200038E RID: 910
	internal class SinkProxy : ISink
	{
		// Token: 0x06001C1C RID: 7196 RVA: 0x0006B27D File Offset: 0x0006947D
		internal SinkProxy(ISink sink)
		{
			this.m_sink = sink;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0006B28C File Offset: 0x0006948C
		public void Initialize(ISinkServices services, SinkIdentifier sid)
		{
			this.m_sink.Initialize(services, sid);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0006B29B File Offset: 0x0006949B
		public void Submit(WireEventBase e)
		{
			this.m_sink.Submit(e);
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0006B2A9 File Offset: 0x000694A9
		public void OnBatchCompleted()
		{
			this.m_sink.OnBatchCompleted();
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0006B2B6 File Offset: 0x000694B6
		public SinkProperties Properties
		{
			get
			{
				return this.m_sink.Properties;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0006B2C3 File Offset: 0x000694C3
		public SinkIdentifier Id
		{
			get
			{
				return this.m_sink.Id;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0006B2D0 File Offset: 0x000694D0
		internal ISink EmbeddedSink
		{
			get
			{
				return this.m_sink;
			}
		}

		// Token: 0x0400097D RID: 2429
		private ISink m_sink;
	}
}
