using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal class NoopEventSink : IDDSEventSink
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000022C1 File Offset: 0x000004C1
		public virtual void OnError(int errorCode, string description)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000022C3 File Offset: 0x000004C3
		public virtual void OnWarning(int warningCode, string description)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022C5 File Offset: 0x000004C5
		public virtual void OnInformation(int informationCode, string description)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000022C7 File Offset: 0x000004C7
		public virtual void OnProgress(string description, float percentComplete, long progressLowCount, long progressHighCount)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000022C9 File Offset: 0x000004C9
		public virtual void OnCustomEvent(string eventName, string eventText, object customEvent)
		{
		}

		// Token: 0x04000003 RID: 3
		public static readonly NoopEventSink Sink = new NoopEventSink();
	}
}
