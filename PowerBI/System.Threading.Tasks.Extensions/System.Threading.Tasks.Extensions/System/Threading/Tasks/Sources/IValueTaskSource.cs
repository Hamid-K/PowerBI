using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200000A RID: 10
	public interface IValueTaskSource
	{
		// Token: 0x06000030 RID: 48
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x06000031 RID: 49
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x06000032 RID: 50
		void GetResult(short token);
	}
}
