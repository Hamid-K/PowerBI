using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200038F RID: 911
	internal class SingletonSinkManager
	{
		// Token: 0x06001C23 RID: 7203 RVA: 0x0006B2D8 File Offset: 0x000694D8
		internal SingletonSinkManager()
		{
			this.m_sinks = new Dictionary<SinkIdentifier, SingletonSinkManager.SingletonSink>();
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0006B2EC File Offset: 0x000694EC
		internal ISink Find(SinkIdentifier sid)
		{
			SingletonSinkManager.SingletonSink singletonSink = null;
			if (!this.m_sinks.TryGetValue(sid, out singletonSink))
			{
				return null;
			}
			return singletonSink.Sink;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0006B314 File Offset: 0x00069514
		internal void Add(ISink sink)
		{
			SingletonSinkManager.SingletonSink singletonSink = null;
			if (this.m_sinks.TryGetValue(sink.Id, out singletonSink))
			{
				singletonSink.AddRef();
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "SingletonSinkManager: Adding reference to singleton: {0}", new object[] { sink.Id });
				return;
			}
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "SingletonSinkManager: Adding new singleton: {0}", new object[] { sink.Id });
			this.m_sinks.Add(sink.Id, new SingletonSinkManager.SingletonSink(sink));
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0006B398 File Offset: 0x00069598
		internal int Remove(ISink sink)
		{
			SingletonSinkManager.SingletonSink singletonSink = null;
			if (this.m_sinks.TryGetValue(sink.Id, out singletonSink))
			{
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "SingletonSinkManager: Releasing reference to singleton: {0}", new object[] { sink.Id });
				int num = singletonSink.Release();
				if (num == 0)
				{
					TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Verbose, "SingletonSinkManager: Removing singleton: {0}", new object[] { sink.Id });
					this.m_sinks.Remove(sink.Id);
				}
				return num;
			}
			throw new SinkNotFoundException(sink.Id);
		}

		// Token: 0x0400097E RID: 2430
		private Dictionary<SinkIdentifier, SingletonSinkManager.SingletonSink> m_sinks;

		// Token: 0x020007BC RID: 1980
		private class SingletonSink
		{
			// Token: 0x06003169 RID: 12649 RVA: 0x000A7D68 File Offset: 0x000A5F68
			internal SingletonSink(ISink sink)
			{
				this.m_sink = sink;
				this.m_refCount = 1;
			}

			// Token: 0x1700076B RID: 1899
			// (get) Token: 0x0600316A RID: 12650 RVA: 0x000A7D7E File Offset: 0x000A5F7E
			internal ISink Sink
			{
				get
				{
					return this.m_sink;
				}
			}

			// Token: 0x0600316B RID: 12651 RVA: 0x000A7D88 File Offset: 0x000A5F88
			internal int AddRef()
			{
				int num = this.m_refCount + 1;
				this.m_refCount = num;
				return num;
			}

			// Token: 0x0600316C RID: 12652 RVA: 0x000A7DA8 File Offset: 0x000A5FA8
			internal int Release()
			{
				int num = this.m_refCount - 1;
				this.m_refCount = num;
				return num;
			}

			// Token: 0x040016CF RID: 5839
			private ISink m_sink;

			// Token: 0x040016D0 RID: 5840
			private int m_refCount;
		}
	}
}
