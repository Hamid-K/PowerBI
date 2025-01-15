using System;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x020003A4 RID: 932
	public interface IEventingRepositoryDownloadNotifications
	{
		// Token: 0x06001C98 RID: 7320
		void OnEventFileDownloadCompleted(string roleInstance, string downloadedFile, int howManySoFar, int outOf);
	}
}
