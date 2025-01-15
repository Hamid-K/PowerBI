using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NLog.Internal;

namespace NLog.Common
{
	// Token: 0x020001B7 RID: 439
	public static class AsyncHelpers
	{
		// Token: 0x0600136D RID: 4973 RVA: 0x0003505C File Offset: 0x0003325C
		internal static int GetManagedThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00035068 File Offset: 0x00033268
		internal static void StartAsyncTask(AsyncHelpersTask asyncTask, object state)
		{
			ThreadPool.QueueUserWorkItem(asyncTask.AsyncDelegate, state);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00035077 File Offset: 0x00033277
		internal static void WaitForDelay(TimeSpan delay)
		{
			Thread.Sleep(delay);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0003507F File Offset: 0x0003327F
		public static void ForEachItemSequentially<T>(IEnumerable<T> items, AsyncContinuation asyncContinuation, AsynchronousAction<T> action)
		{
			AsyncHelpers.<>c__DisplayClass3_0<T> CS$<>8__locals1 = new AsyncHelpers.<>c__DisplayClass3_0<T>();
			CS$<>8__locals1.asyncContinuation = asyncContinuation;
			CS$<>8__locals1.action = action;
			CS$<>8__locals1.action = AsyncHelpers.ExceptionGuard<T>(CS$<>8__locals1.action);
			CS$<>8__locals1.enumerator = items.GetEnumerator();
			CS$<>8__locals1.<ForEachItemSequentially>g__InvokeNext|0(null);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000350B7 File Offset: 0x000332B7
		public static void Repeat(int repeatCount, AsyncContinuation asyncContinuation, AsynchronousAction action)
		{
			AsyncHelpers.<>c__DisplayClass4_0 CS$<>8__locals1 = new AsyncHelpers.<>c__DisplayClass4_0();
			CS$<>8__locals1.asyncContinuation = asyncContinuation;
			CS$<>8__locals1.action = action;
			CS$<>8__locals1.action = AsyncHelpers.ExceptionGuard(CS$<>8__locals1.action);
			CS$<>8__locals1.remaining = repeatCount;
			CS$<>8__locals1.<Repeat>g__InvokeNext|0(null);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000350EA File Offset: 0x000332EA
		public static AsyncContinuation PrecededBy(AsyncContinuation asyncContinuation, AsynchronousAction action)
		{
			action = AsyncHelpers.ExceptionGuard(action);
			return delegate(Exception ex)
			{
				if (ex != null)
				{
					asyncContinuation(ex);
					return;
				}
				action(AsyncHelpers.PreventMultipleCalls(asyncContinuation));
			};
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0003511B File Offset: 0x0003331B
		public static AsyncContinuation WithTimeout(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			return new AsyncContinuation(new TimeoutContinuation(asyncContinuation, timeout).Function);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00035130 File Offset: 0x00033330
		public static void ForEachItemInParallel<T>(IEnumerable<T> values, AsyncContinuation asyncContinuation, AsynchronousAction<T> action)
		{
			action = AsyncHelpers.ExceptionGuard<T>(action);
			List<T> list = new List<T>(values);
			int remaining = list.Count;
			List<Exception> exceptions = new List<Exception>();
			InternalLogger.Trace<int>("ForEachItemInParallel() {0} items", list.Count);
			if (remaining == 0)
			{
				asyncContinuation(null);
				return;
			}
			AsyncContinuation continuation = delegate(Exception ex)
			{
				InternalLogger.Trace<Exception>("Continuation invoked: {0}", ex);
				if (ex != null)
				{
					List<Exception> exceptions2 = exceptions;
					lock (exceptions2)
					{
						exceptions.Add(ex);
					}
				}
				int num = Interlocked.Decrement(ref remaining);
				InternalLogger.Trace<int>("Parallel task completed. {0} items remaining", num);
				if (num == 0)
				{
					asyncContinuation(AsyncHelpers.GetCombinedException(exceptions));
				}
			};
			foreach (T t in list)
			{
				T itemCopy = t;
				AsyncHelpers.StartAsyncTask(new AsyncHelpersTask(delegate(object s)
				{
					try
					{
						action(itemCopy, AsyncHelpers.PreventMultipleCalls(continuation));
					}
					catch (Exception ex)
					{
						InternalLogger.Error(ex, "ForEachItemInParallel - Unhandled Exception");
						if (ex.MustBeRethrownImmediately())
						{
							throw;
						}
					}
				}), null);
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0003521C File Offset: 0x0003341C
		public static void RunSynchronously(AsynchronousAction action)
		{
			ManualResetEvent ev = new ManualResetEvent(false);
			Exception lastException = null;
			action(AsyncHelpers.PreventMultipleCalls(delegate(Exception ex)
			{
				lastException = ex;
				ev.Set();
			}));
			ev.WaitOne();
			if (lastException != null)
			{
				throw new NLogRuntimeException("Asynchronous exception has occurred.", lastException);
			}
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0003527E File Offset: 0x0003347E
		public static AsyncContinuation PreventMultipleCalls(AsyncContinuation asyncContinuation)
		{
			if (asyncContinuation.Target is SingleCallContinuation)
			{
				return asyncContinuation;
			}
			return new AsyncContinuation(new SingleCallContinuation(asyncContinuation).Function);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000352A0 File Offset: 0x000334A0
		public static Exception GetCombinedException(IList<Exception> exceptions)
		{
			if (exceptions.Count == 0)
			{
				return null;
			}
			if (exceptions.Count == 1)
			{
				return exceptions[0];
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			string newLine = EnvironmentHelper.NewLine;
			foreach (Exception ex in exceptions)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(ex.ToString());
				stringBuilder.Append(newLine);
				text = newLine;
			}
			return new NLogRuntimeException("Got multiple exceptions:\r\n" + stringBuilder);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00035340 File Offset: 0x00033540
		private static AsynchronousAction ExceptionGuard(AsynchronousAction action)
		{
			return delegate(AsyncContinuation cont)
			{
				try
				{
					action(cont);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					cont(ex);
				}
			};
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00035359 File Offset: 0x00033559
		private static AsynchronousAction<T> ExceptionGuard<T>(AsynchronousAction<T> action)
		{
			return delegate(T argument, AsyncContinuation cont)
			{
				try
				{
					action(argument, cont);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					cont(ex);
				}
			};
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00035374 File Offset: 0x00033574
		internal static bool WaitForDispose(this Timer timer, TimeSpan timeout)
		{
			timer.Change(-1, -1);
			if (timeout != TimeSpan.Zero)
			{
				ManualResetEvent manualResetEvent = new ManualResetEvent(false);
				if (timer.Dispose(manualResetEvent) && !manualResetEvent.WaitOne((int)timeout.TotalMilliseconds))
				{
					return false;
				}
				manualResetEvent.Close();
			}
			else
			{
				timer.Dispose();
			}
			return true;
		}
	}
}
