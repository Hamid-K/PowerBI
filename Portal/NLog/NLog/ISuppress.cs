using System;
using System.Threading.Tasks;

namespace NLog
{
	// Token: 0x02000009 RID: 9
	public interface ISuppress
	{
		// Token: 0x0600017C RID: 380
		void Swallow(Action action);

		// Token: 0x0600017D RID: 381
		T Swallow<T>(Func<T> func);

		// Token: 0x0600017E RID: 382
		T Swallow<T>(Func<T> func, T fallback);

		// Token: 0x0600017F RID: 383
		void Swallow(Task task);

		// Token: 0x06000180 RID: 384
		Task SwallowAsync(Task task);

		// Token: 0x06000181 RID: 385
		Task SwallowAsync(Func<Task> asyncAction);

		// Token: 0x06000182 RID: 386
		Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc);

		// Token: 0x06000183 RID: 387
		Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc, TResult fallback);
	}
}
