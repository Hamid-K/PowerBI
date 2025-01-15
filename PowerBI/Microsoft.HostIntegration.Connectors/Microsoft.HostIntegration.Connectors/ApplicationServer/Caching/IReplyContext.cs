using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200019E RID: 414
	internal interface IReplyContext : IDisposable
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D77 RID: 3447
		RemoteEndpoint RemoteEndpoint { get; }

		// Token: 0x06000D78 RID: 3448
		void Reply(ResponseBody response);

		// Token: 0x06000D79 RID: 3449
		void AsyncReply(ResponseBody response, WaitCallback callback, object callerContext);

		// Token: 0x06000D7A RID: 3450
		void AbortRequestChannel();

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D7B RID: 3451
		object State { get; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000D7C RID: 3452
		ClientVersionInfo RemoteVersionInfo { get; }

		// Token: 0x06000D7D RID: 3453
		RequestBody GetRequest(out IDictionary<string, string> headers);

		// Token: 0x06000D7E RID: 3454
		ResponseBody GetResponse();

		// Token: 0x06000D7F RID: 3455
		T GetRequestTracker<T>(string tracker);

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000D80 RID: 3456
		CacheConnectionProperty ConnectionProperty { get; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000D81 RID: 3457
		string MessageAuthorizationToken { get; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000D82 RID: 3458
		VelocityPacketException PacketException { get; }
	}
}
