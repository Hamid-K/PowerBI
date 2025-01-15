using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001CE RID: 462
	internal static class RetryOperationHelper
	{
		// Token: 0x06001452 RID: 5202 RVA: 0x000451D8 File Offset: 0x000433D8
		public static async Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> func, int maxAttempts, TimeSpan? retryInterval = null, Action<int, Exception> onAttemptFailed = null, ISet<Type> allowedExceptions = null)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if (maxAttempts < 1)
			{
				throw new ArgumentOutOfRangeException("maxAttempts", maxAttempts, "The maximum number of attempts must not be less than 1.");
			}
			int attempt = 0;
			T t;
			for (;;)
			{
				if (attempt > 0 && retryInterval != null)
				{
					await Task.Delay(retryInterval.Value).ConfigureAwait(true);
				}
				try
				{
					t = await func().ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					if (allowedExceptions != null && !allowedExceptions.Contains(ex.GetType()))
					{
						throw;
					}
					attempt++;
					if (onAttemptFailed != null)
					{
						onAttemptFailed(attempt, ex);
					}
					if (attempt >= maxAttempts)
					{
						throw;
					}
					continue;
				}
				break;
			}
			return t;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004523C File Offset: 0x0004343C
		public static async Task ExecuteWithRetryAsync(Func<Task> func, int maxAttempts, TimeSpan? retryInterval = null, Action<int, Exception> onAttemptFailed = null, ISet<Type> allowedExceptions = null)
		{
			RetryOperationHelper.<>c__DisplayClass1_0 CS$<>8__locals1 = new RetryOperationHelper.<>c__DisplayClass1_0();
			CS$<>8__locals1.func = func;
			if (CS$<>8__locals1.func == null)
			{
				throw new ArgumentNullException("func");
			}
			await RetryOperationHelper.ExecuteWithRetryAsync<bool>(delegate
			{
				RetryOperationHelper.<>c__DisplayClass1_0.<<ExecuteWithRetryAsync>b__0>d <<ExecuteWithRetryAsync>b__0>d;
				<<ExecuteWithRetryAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<ExecuteWithRetryAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<ExecuteWithRetryAsync>b__0>d.<>1__state = -1;
				<<ExecuteWithRetryAsync>b__0>d.<>t__builder.Start<RetryOperationHelper.<>c__DisplayClass1_0.<<ExecuteWithRetryAsync>b__0>d>(ref <<ExecuteWithRetryAsync>b__0>d);
				return <<ExecuteWithRetryAsync>b__0>d.<>t__builder.Task;
			}, maxAttempts, retryInterval, onAttemptFailed, allowedExceptions).ConfigureAwait(true);
		}
	}
}
