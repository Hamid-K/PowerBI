using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000005 RID: 5
	internal interface IDDSEventSink
	{
		// Token: 0x06000020 RID: 32
		void OnError(int errorCode, string description);

		// Token: 0x06000021 RID: 33
		void OnWarning(int warningCode, string description);

		// Token: 0x06000022 RID: 34
		void OnInformation(int informationCode, string description);

		// Token: 0x06000023 RID: 35
		void OnProgress(string description, float percentComplete, long progressLowCount, long progressHighCount);

		// Token: 0x06000024 RID: 36
		void OnCustomEvent(string eventName, string eventText, object customEvent);
	}
}
