using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002AB RID: 683
	public static class MutexUtils
	{
		// Token: 0x0600126F RID: 4719 RVA: 0x000404C0 File Offset: 0x0003E6C0
		public static IDisposable GetNamedSystemMutexScope([NotNull] string mutextName, MutexUtilsOptions options)
		{
			return MutexUtils.GetNamedSystemMutexScope(mutextName, options, TimeConstants.InfiniteTimeSpan);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000404D0 File Offset: 0x0003E6D0
		public static IDisposable GetNamedSystemMutexScope([NotNull] string mutextName, MutexUtilsOptions options, TimeSpan timeout)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(mutextName, "mutextName");
			MutexSecurity mutexSecurity = new MutexSecurity();
			mutexSecurity.AddAccessRule(new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow));
			bool flag;
			return new Mutex(false, mutextName, out flag, mutexSecurity).GetMutexScope(options, timeout);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00040517 File Offset: 0x0003E717
		public static IDisposable GetMutexScope(this Mutex mutex, MutexUtilsOptions options)
		{
			return mutex.GetMutexScope(options, TimeConstants.InfiniteTimeSpan);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00040528 File Offset: 0x0003E728
		public static IDisposable GetMutexScope(this Mutex mutex, MutexUtilsOptions options, TimeSpan timeout)
		{
			return new DeferredDispose(delegate
			{
				try
				{
					if (!mutex.WaitOne(timeout))
					{
						throw new MutexTimeoutException(timeout.TotalSeconds);
					}
				}
				catch (AbandonedMutexException ex)
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceWarning("AbandonedMutexException has been thrown with the following message: {0}. The exception has most likely occurred due to a previous thread(application) which exited unexpectedly", new object[] { ex.Message });
					if (!options.HasFlag(MutexUtilsOptions.ContinueOnMutexAborted))
					{
						throw;
					}
				}
			}, delegate
			{
				if (mutex != null)
				{
					mutex.ReleaseMutex();
					mutex.Dispose();
				}
				mutex = null;
			});
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00040570 File Offset: 0x0003E770
		public static void LockedExecute(object mutex, TimeSpan timeout, Action action)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<object>(mutex, "mutex");
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(action, "action");
			bool flag = false;
			try
			{
				Monitor.TryEnter(mutex, timeout, ref flag);
				if (!flag)
				{
					throw new MutexTimeoutException(timeout.TotalSeconds);
				}
				action();
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(mutex);
				}
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000405D0 File Offset: 0x0003E7D0
		public static bool DoubleCheckedExecute(object mutex, TimeSpan timeout, DoubleCheckedPredicate predicate, Action action)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<object>(mutex, "mutex");
			ExtendedDiagnostics.EnsureArgumentNotNull<DoubleCheckedPredicate>(predicate, "predicate");
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(action, "action");
			if (predicate())
			{
				bool flag = false;
				try
				{
					Monitor.TryEnter(mutex, timeout, ref flag);
					if (!flag)
					{
						throw new MutexTimeoutException(timeout.TotalSeconds);
					}
					if (predicate())
					{
						action();
						return true;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(mutex);
					}
				}
				return false;
			}
			return false;
		}
	}
}
