using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200000B RID: 11
	public interface IValueTaskSource<out TResult>
	{
		// Token: 0x06000033 RID: 51
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x06000034 RID: 52
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x06000035 RID: 53
		TResult GetResult(short token);
	}
}
