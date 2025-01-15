using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000140 RID: 320
	internal interface IInternalQuery<out TElement> : IInternalQuery
	{
		// Token: 0x06001509 RID: 5385
		IInternalQuery<TElement> Include(string path);

		// Token: 0x0600150A RID: 5386
		IInternalQuery<TElement> AsNoTracking();

		// Token: 0x0600150B RID: 5387
		IInternalQuery<TElement> AsStreaming();

		// Token: 0x0600150C RID: 5388
		IInternalQuery<TElement> WithExecutionStrategy(IDbExecutionStrategy executionStrategy);

		// Token: 0x0600150D RID: 5389
		IDbAsyncEnumerator<TElement> GetAsyncEnumerator();

		// Token: 0x0600150E RID: 5390
		IEnumerator<TElement> GetEnumerator();
	}
}
