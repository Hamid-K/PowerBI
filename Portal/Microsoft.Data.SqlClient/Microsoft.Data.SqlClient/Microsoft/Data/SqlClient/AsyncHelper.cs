using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F4 RID: 244
	internal static class AsyncHelper
	{
		// Token: 0x060012EA RID: 4842 RVA: 0x0004C730 File Offset: 0x0004A930
		internal static Task CreateContinuationTask(Task task, Action onSuccess, SqlInternalConnectionTds connectionToDoom = null, Action<Exception> onFailure = null)
		{
			if (task == null)
			{
				onSuccess();
				return null;
			}
			TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
			AsyncHelper.ContinueTask(task, completion, delegate
			{
				onSuccess();
				completion.SetResult(null);
			}, onFailure, null, null, connectionToDoom, null);
			return completion.Task;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0004C790 File Offset: 0x0004A990
		internal static Task CreateContinuationTaskWithState(Task task, object state, Action<object> onSuccess, Action<Exception, object> onFailure = null)
		{
			if (task == null)
			{
				onSuccess(state);
				return null;
			}
			TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
			AsyncHelper.ContinueTaskWithState(task, completion, state, delegate(object continueState)
			{
				onSuccess(continueState);
				completion.SetResult(null);
			}, onFailure, null, null, null, null);
			return completion.Task;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0004C7F0 File Offset: 0x0004A9F0
		internal static Task CreateContinuationTask<T1, T2>(Task task, Action<T1, T2> onSuccess, T1 arg1, T2 arg2, SqlInternalConnectionTds connectionToDoom = null, Action<Exception> onFailure = null)
		{
			return AsyncHelper.CreateContinuationTask(task, delegate
			{
				onSuccess(arg1, arg2);
			}, connectionToDoom, onFailure);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0004C830 File Offset: 0x0004AA30
		internal static void ContinueTask(Task task, TaskCompletionSource<object> completion, Action onSuccess, Action<Exception> onFailure = null, Action onCancellation = null, Func<Exception, Exception> exceptionConverter = null, SqlInternalConnectionTds connectionToDoom = null, SqlConnection connectionToAbort = null)
		{
			task.ContinueWith(delegate(Task tsk)
			{
				if (tsk.Exception != null)
				{
					Exception ex = tsk.Exception.InnerException;
					if (exceptionConverter != null)
					{
						ex = exceptionConverter(ex);
					}
					try
					{
						if (onFailure != null)
						{
							onFailure(ex);
						}
						return;
					}
					finally
					{
						completion.TrySetException(ex);
					}
				}
				if (tsk.IsCanceled)
				{
					try
					{
						if (onCancellation != null)
						{
							onCancellation();
						}
						return;
					}
					finally
					{
						completion.TrySetCanceled();
					}
				}
				if (connectionToDoom != null || connectionToAbort != null)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						onSuccess();
						return;
					}
					catch (OutOfMemoryException ex2)
					{
						if (connectionToDoom != null)
						{
							connectionToDoom.DoomThisConnection();
						}
						else
						{
							connectionToAbort.Abort(ex2);
						}
						completion.SetException(ex2);
						throw;
					}
					catch (StackOverflowException ex3)
					{
						if (connectionToDoom != null)
						{
							connectionToDoom.DoomThisConnection();
						}
						else
						{
							connectionToAbort.Abort(ex3);
						}
						completion.SetException(ex3);
						throw;
					}
					catch (ThreadAbortException ex4)
					{
						if (connectionToDoom != null)
						{
							connectionToDoom.DoomThisConnection();
						}
						else
						{
							connectionToAbort.Abort(ex4);
						}
						completion.SetException(ex4);
						throw;
					}
					catch (Exception ex5)
					{
						completion.SetException(ex5);
						return;
					}
				}
				try
				{
					onSuccess();
				}
				catch (Exception ex6)
				{
					completion.SetException(ex6);
				}
			}, TaskScheduler.Default);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0004C890 File Offset: 0x0004AA90
		internal static void ContinueTaskWithState(Task task, TaskCompletionSource<object> completion, object state, Action<object> onSuccess, Action<Exception, object> onFailure = null, Action<object> onCancellation = null, Func<Exception, object, Exception> exceptionConverter = null, SqlInternalConnectionTds connectionToDoom = null, SqlConnection connectionToAbort = null)
		{
			task.ContinueWith(delegate(Task tsk, object state)
			{
				if (tsk.Exception != null)
				{
					Exception ex = tsk.Exception.InnerException;
					if (exceptionConverter != null)
					{
						ex = exceptionConverter(ex, state);
					}
					try
					{
						Action<Exception, object> onFailure2 = onFailure;
						if (onFailure2 == null)
						{
							return;
						}
						onFailure2(ex, state);
						return;
					}
					finally
					{
						completion.TrySetException(ex);
					}
				}
				if (tsk.IsCanceled)
				{
					try
					{
						Action<object> onCancellation2 = onCancellation;
						if (onCancellation2 == null)
						{
							return;
						}
						onCancellation2(state);
						return;
					}
					finally
					{
						completion.TrySetCanceled();
					}
				}
				if (connectionToDoom != null || connectionToAbort != null)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						onSuccess(state);
						return;
					}
					catch (OutOfMemoryException ex2)
					{
						if (connectionToDoom != null)
						{
							connectionToDoom.DoomThisConnection();
						}
						else
						{
							connectionToAbort.Abort(ex2);
						}
						completion.SetException(ex2);
						throw;
					}
					catch (StackOverflowException ex3)
					{
						if (connectionToDoom != null)
						{
							connectionToDoom.DoomThisConnection();
						}
						else
						{
							connectionToAbort.Abort(ex3);
						}
						completion.SetException(ex3);
						throw;
					}
					catch (ThreadAbortException ex4)
					{
						if (connectionToDoom != null)
						{
							connectionToDoom.DoomThisConnection();
						}
						else
						{
							connectionToAbort.Abort(ex4);
						}
						completion.SetException(ex4);
						throw;
					}
					catch (Exception ex5)
					{
						completion.SetException(ex5);
						return;
					}
				}
				try
				{
					onSuccess(state);
				}
				catch (Exception ex6)
				{
					completion.SetException(ex6);
				}
			}, state, TaskScheduler.Default);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0004C8F4 File Offset: 0x0004AAF4
		internal static void WaitForCompletion(Task task, int timeout, Action onTimeout = null, bool rethrowExceptions = true)
		{
			try
			{
				task.Wait((timeout > 0) ? (1000 * timeout) : (-1));
			}
			catch (AggregateException ex)
			{
				if (rethrowExceptions)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				}
			}
			if (!task.IsCompleted)
			{
				task.ContinueWith(delegate(Task t)
				{
					AggregateException exception = t.Exception;
				});
				if (onTimeout != null)
				{
					onTimeout();
				}
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004C978 File Offset: 0x0004AB78
		internal static void SetTimeoutException(TaskCompletionSource<object> completion, int timeout, Func<Exception> exc, CancellationToken ctoken)
		{
			if (timeout > 0)
			{
				Task.Delay(timeout * 1000, ctoken).ContinueWith(delegate(Task tsk)
				{
					if (!tsk.IsCanceled && !completion.Task.IsCompleted)
					{
						completion.TrySetException(exc());
					}
				});
			}
		}
	}
}
