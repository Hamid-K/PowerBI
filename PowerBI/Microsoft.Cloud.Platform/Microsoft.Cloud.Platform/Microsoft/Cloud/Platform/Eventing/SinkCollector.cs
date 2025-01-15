using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000392 RID: 914
	public class SinkCollector
	{
		// Token: 0x06001C2F RID: 7215 RVA: 0x0006B6D0 File Offset: 0x000698D0
		public SinkCollector()
		{
			this.m_sinks = new Dictionary<SinkIdentifier, ISink>();
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0006B6E3 File Offset: 0x000698E3
		public void Collect(ISink sink)
		{
			if (!this.m_sinks.ContainsKey(sink.Id))
			{
				this.m_sinks.Add(sink.Id, sink);
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0006B70A File Offset: 0x0006990A
		public bool Has(SinkIdentifier sid)
		{
			return this.m_sinks.ContainsKey(sid);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x0006B718 File Offset: 0x00069918
		public void ForEach(Action<ISink> action)
		{
			foreach (ISink sink in this.m_sinks.Values)
			{
				action(sink);
			}
		}

		// Token: 0x04000980 RID: 2432
		private Dictionary<SinkIdentifier, ISink> m_sinks;
	}
}
