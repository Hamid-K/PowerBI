using System;
using System.Collections;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003D2 RID: 978
	internal class EventLogger
	{
		// Token: 0x06002262 RID: 8802 RVA: 0x00002061 File Offset: 0x00000261
		public EventLogger()
		{
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0006A0BC File Offset: 0x000682BC
		public EventLogger(EventLogger original)
		{
			this.sinkName = original.sinkName;
			this.sinkParam = original.sinkParam;
			this.defaultLevel = original.defaultLevel;
			if (original.sourceOverride != null)
			{
				this.sourceOverride = new Hashtable(original.sourceOverride);
			}
			else
			{
				this.sourceOverride = null;
			}
			this.m_sink = original.m_sink;
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x0006A124 File Offset: 0x00068324
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x0006A17E File Offset: 0x0006837E
		public IEventSink Sink
		{
			get
			{
				if (this.m_sink == null)
				{
					IEventSink eventSink = (IEventSink)Utility.CreateInstanceByReflection(this.sinkName);
					if (eventSink == null || !eventSink.Load(this.sinkParam))
					{
						throw new EventLogWriterException("Unable to create sink for " + this.sinkName);
					}
					this.m_sink = eventSink;
				}
				return this.m_sink;
			}
			set
			{
				this.m_sink = value;
			}
		}

		// Token: 0x040015AB RID: 5547
		public string sinkName;

		// Token: 0x040015AC RID: 5548
		public string sinkParam;

		// Token: 0x040015AD RID: 5549
		public int defaultLevel;

		// Token: 0x040015AE RID: 5550
		public Hashtable sourceOverride;

		// Token: 0x040015AF RID: 5551
		private IEventSink m_sink;
	}
}
