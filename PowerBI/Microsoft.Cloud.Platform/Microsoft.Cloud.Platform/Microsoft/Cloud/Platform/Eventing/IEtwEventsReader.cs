using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200039F RID: 927
	public interface IEtwEventsReader
	{
		// Token: 0x06001C79 RID: 7289
		IAsyncResult BeginReadProvidersManifests(IEnumerable<string> providersManifestsFiles, AsyncCallback callback, object context);

		// Token: 0x06001C7A RID: 7290
		EtwProvidersManifests EndReadProvidersManifests(IAsyncResult asyncResult);

		// Token: 0x06001C7B RID: 7291
		IAsyncResult BeginReadEtwEvents(EtwProvidersManifests providersManifests, IEnumerable<string> etlPaths, DateTime from, DateTime to, EventsQueryFilter filter, EtwEventsReaderOptions options, AsyncCallback callback, object context);

		// Token: 0x06001C7C RID: 7292
		IAsyncResult BeginReadEtwEvents(EtwProvidersManifests providersManifests, IEnumerable<string> etlPaths, EventsQueryFilter filter, EtwEventsReaderOptions options, AsyncCallback callback, object context);

		// Token: 0x06001C7D RID: 7293
		IEnumerable<EtwEvent> EndReadEtwEvents(IAsyncResult asyncResult);
	}
}
