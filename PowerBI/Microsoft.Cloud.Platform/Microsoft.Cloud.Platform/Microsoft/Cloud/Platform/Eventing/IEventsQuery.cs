using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200039B RID: 923
	public interface IEventsQuery
	{
		// Token: 0x06001C6A RID: 7274
		IAsyncResult BeginQueryEvents(DateTime from, DateTime to, EventsQueryFilter filter, string downloadDirectory, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C6B RID: 7275
		IAsyncResult BeginQueryEvents(string connectionString, DateTime from, DateTime to, EventsQueryFilter filter, string downloadDirectory, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C6C RID: 7276
		IEnumerable<EtwEvent> EndQueryEvents(IAsyncResult asyncResult);

		// Token: 0x06001C6D RID: 7277
		IAsyncResult BeginQueryEventsContinued(DateTime from, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C6E RID: 7278
		IAsyncResult BeginQueryEventsContinued(string connectionString, DateTime from, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C6F RID: 7279
		IAsyncResult BeginQueryEventsContinued(IEventingRepositoryContinuation continuation, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C70 RID: 7280
		IAsyncResult BeginQueryEventsContinued(string connectionString, IEventingRepositoryContinuation continuation, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context);

		// Token: 0x06001C71 RID: 7281
		EventsQueryResult EndQueryEventsContinued(IAsyncResult asyncResult);
	}
}
