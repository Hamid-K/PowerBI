using System;
using System.Linq.Expressions;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000359 RID: 857
	public interface IEventsListener
	{
		// Token: 0x06001964 RID: 6500
		IAsyncResult BeginWaitForEvent<T>(Expression<Func<T, bool>> predicate, int occurrences, int timeout, AsyncCallback callback, object state) where T : WireEventBase;

		// Token: 0x06001965 RID: 6501
		EventsQueryResult<T> EndWaitForEvent<T>(IAsyncResult result) where T : WireEventBase;

		// Token: 0x06001966 RID: 6502
		EventsQueryResult<T> WaitForEvent<T>(Expression<Func<T, bool>> predicate, int occurrences, int timeout) where T : WireEventBase;
	}
}
