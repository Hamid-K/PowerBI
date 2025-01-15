using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal class EventSupport
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000022DF File Offset: 0x000004DF
		public EventSupport(IDDSEventSink sink)
		{
			this.EventSink = sink;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000022EE File Offset: 0x000004EE
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000022F6 File Offset: 0x000004F6
		public IDDSEventSink EventSink
		{
			get
			{
				return this.m_sink;
			}
			set
			{
				if (value != null)
				{
					this.m_sink = value;
					return;
				}
				this.m_sink = NoopEventSink.Sink;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000230E File Offset: 0x0000050E
		public void FireError(string description)
		{
			this.EventSink.OnError(0, description);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000231D File Offset: 0x0000051D
		public void FireError(int errorCode, string description)
		{
			this.EventSink.OnError(errorCode, description);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000232C File Offset: 0x0000052C
		public void FireWarning(int warningCode, string description)
		{
			this.EventSink.OnWarning(warningCode, description);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000233B File Offset: 0x0000053B
		public void FireInformation(int informationCode, string description)
		{
			this.EventSink.OnInformation(informationCode, description);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000234A File Offset: 0x0000054A
		public void FireProgress(string description)
		{
			this.EventSink.OnProgress(description, -1f, 1L, -1L);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002361 File Offset: 0x00000561
		public void FireProgress(string description, float percentComplete)
		{
			this.EventSink.OnProgress(description, percentComplete, -1L, -1L);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002374 File Offset: 0x00000574
		public void FireProgress(string description, long progressLowCount, long progressHighCount)
		{
			this.EventSink.OnProgress(description, -1f, progressLowCount, progressHighCount);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002389 File Offset: 0x00000589
		public void FireCustomEvent(string eventName, string eventText, object customEvent)
		{
			this.EventSink.OnCustomEvent(eventName, eventText, customEvent);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002399 File Offset: 0x00000599
		public static void ThrowError(string message)
		{
			throw new Exception(message);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000023A1 File Offset: 0x000005A1
		public static void ThrowException(Exception e)
		{
			throw e;
		}

		// Token: 0x04000004 RID: 4
		public static readonly EventSupport NoopEventSupport = new EventSupport(null);

		// Token: 0x04000005 RID: 5
		protected IDDSEventSink m_sink;
	}
}
