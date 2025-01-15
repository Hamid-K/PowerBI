using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x020003A3 RID: 931
	public interface IEventingRepository
	{
		// Token: 0x06001C87 RID: 7303
		IAsyncResult BeginGetEventFiles(string storageConnectionString, string downloadDirectory, EventsRepositoryOptions options, DateTime from, DateTime to, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C88 RID: 7304
		IAsyncResult BeginGetEventFiles(string downloadDirectory, EventsRepositoryOptions options, DateTime from, DateTime to, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C89 RID: 7305
		IEnumerable<string> EndGetEventFiles(IAsyncResult result);

		// Token: 0x06001C8A RID: 7306
		IAsyncResult BeginGetEventFilesContinued(string storageConnectionString, string downloadDirectory, EventsRepositoryOptions options, IEventingRepositoryContinuation continuation, int maxSizeToDownloadInMb, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C8B RID: 7307
		IAsyncResult BeginGetEventFilesContinued(string downloadDirectory, EventsRepositoryOptions options, IEventingRepositoryContinuation continuation, int maxSizeToDownloadInMb, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C8C RID: 7308
		IAsyncResult BeginGetEventFilesContinued(string storageConnectionString, string downloadDirectory, EventsRepositoryOptions options, DateTime from, int maxSizeToDownloadInMb, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C8D RID: 7309
		IAsyncResult BeginGetEventFilesContinued(string downloadDirectory, EventsRepositoryOptions options, DateTime from, int maxSizeToDownloadInMb, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C8E RID: 7310
		EventFilesDownloadResult EndGetEventFilesContinued(IAsyncResult result);

		// Token: 0x06001C8F RID: 7311
		IAsyncResult BeginGetProvidersManifestsFiles(string storageConnectionString, string downloadDirectory, AsyncCallback callback, object context);

		// Token: 0x06001C90 RID: 7312
		IAsyncResult BeginGetProvidersManifestsFiles(string downloadDirectory, AsyncCallback callback, object context);

		// Token: 0x06001C91 RID: 7313
		IAsyncResult BeginGetDeploymentProvidersManifestsFiles(string storageConnectionString, string downloadDirectory, AsyncCallback callback, object context);

		// Token: 0x06001C92 RID: 7314
		IAsyncResult BeginGetDeploymentProvidersManifestsFiles(string downloadDirectory, AsyncCallback callback, object context);

		// Token: 0x06001C93 RID: 7315
		IEnumerable<string> EndGetDeploymentProvidersManifestsFiles(IAsyncResult asyncResult);

		// Token: 0x06001C94 RID: 7316
		IAsyncResult BeginGetProvidersManifestsFiles(string storageConnectionString, string downloadDirectory, IEventingRepositoryContinuation continuation, AsyncCallback callback, object context);

		// Token: 0x06001C95 RID: 7317
		IAsyncResult BeginGetProvidersManifestsFiles(string downloadDirectory, IEventingRepositoryContinuation continuation, AsyncCallback callback, object context);

		// Token: 0x06001C96 RID: 7318
		IEnumerable<string> EndGetProvidersManifestsFiles(IAsyncResult result);

		// Token: 0x06001C97 RID: 7319
		IEventingRepositoryContinuation DeserializeContinuation(string serializedContinuation);
	}
}
