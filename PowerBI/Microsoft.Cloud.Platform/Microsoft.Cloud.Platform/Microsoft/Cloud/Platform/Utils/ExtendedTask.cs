using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200029B RID: 667
	public static class ExtendedTask
	{
		// Token: 0x060011F3 RID: 4595 RVA: 0x0003EA9C File Offset: 0x0003CC9C
		public static IAsyncResult ToApm<TResult>(this Task<TResult> task, AsyncCallback callback, object state)
		{
			TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>(state);
			task.ContinueWith(delegate
			{
				if (task.IsFaulted)
				{
					tcs.TrySetException(task.Exception.InnerExceptions);
				}
				else if (task.IsCanceled)
				{
					tcs.TrySetCanceled();
				}
				else
				{
					tcs.TrySetResult(task.Result);
				}
				if (callback != null)
				{
					callback(tcs.Task);
				}
			}, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
			return tcs.Task;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0003EAF8 File Offset: 0x0003CCF8
		public static IAsyncResult ToApm(this Task task, AsyncCallback callback, object state)
		{
			TaskCompletionSource<int> tcs = new TaskCompletionSource<int>(state);
			task.ContinueWith(delegate
			{
				if (task.IsFaulted)
				{
					tcs.TrySetException(task.Exception.InnerExceptions);
				}
				else if (task.IsCanceled)
				{
					tcs.TrySetCanceled();
				}
				else
				{
					tcs.TrySetResult(0);
				}
				if (callback != null)
				{
					callback(tcs.Task);
				}
			}, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
			return tcs.Task;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0003EB53 File Offset: 0x0003CD53
		public static IAsyncResult BeginTask<T>(Task<T> task, AsyncCallback ac, object s)
		{
			return task.ToApm(ac, s);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0003EB5D File Offset: 0x0003CD5D
		public static T EndTask<T>(IAsyncResult ar)
		{
			return ((Task<T>)ar).ExtendedResult<T>();
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0003EB6A File Offset: 0x0003CD6A
		public static IAsyncResult BeginTask(Task task, AsyncCallback ac, object s)
		{
			return task.ToApm(ac, s);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0003EB74 File Offset: 0x0003CD74
		public static void EndTask(IAsyncResult ar)
		{
			((Task)ar).ExtendedWait();
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0003EB84 File Offset: 0x0003CD84
		[CanBeNull]
		public static Exception Unwrap(this AggregateException ex)
		{
			if (ex == null)
			{
				return null;
			}
			ReadOnlyCollection<Exception> innerExceptions = ex.InnerExceptions;
			if (innerExceptions == null)
			{
				return null;
			}
			if (innerExceptions.Count == 0)
			{
				return null;
			}
			Exception ex2 = innerExceptions.First<Exception>();
			AggregateException ex3 = ex2 as AggregateException;
			if (ex3 != null)
			{
				return ex3.Unwrap();
			}
			return ex2;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0003EBC5 File Offset: 0x0003CDC5
		public static void ExtendedWait(this Task task)
		{
			task.ExtendedWait(-1, CancellationToken.None);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0003EBD4 File Offset: 0x0003CDD4
		public static void ExtendedWait(this Task task, CancellationToken cancellationToken)
		{
			task.ExtendedWait(-1, cancellationToken);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0003EBDF File Offset: 0x0003CDDF
		public static bool ExtendedWait(this Task task, int millisecondsTimeout)
		{
			return task.ExtendedWait(millisecondsTimeout, CancellationToken.None);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0003EBED File Offset: 0x0003CDED
		public static bool ExtendedWait(this Task task, TimeSpan timeout)
		{
			return task.ExtendedWait(timeout.AsIntTimeoutIfInRange("timeout"), CancellationToken.None);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0003EC08 File Offset: 0x0003CE08
		public static bool ExtendedWait(this Task task, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			try
			{
				return task.Wait(millisecondsTimeout, cancellationToken);
			}
			catch (AggregateException ex)
			{
				Exception ex2 = ex.Unwrap();
				ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(task, ex2);
				ExceptionDispatchInfo.Capture(ex2).Throw();
			}
			return false;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0003EC50 File Offset: 0x0003CE50
		public static T ExtendedResult<T>(this Task<T> task)
		{
			try
			{
				return task.Result;
			}
			catch (AggregateException ex)
			{
				Exception ex2 = ex.Unwrap();
				ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(task, ex2);
				ExceptionDispatchInfo.Capture(ex2).Throw();
			}
			return default(T);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0003EC9C File Offset: 0x0003CE9C
		public static Task<T> StartTask<T>(object caller, TopLevelHandlerOption options, Func<T> action)
		{
			return Task.Factory.StartNew<T>(delegate
			{
				T result = default(T);
				TopLevelHandler.Run(caller, options, delegate
				{
					result = action();
				});
				return result;
			});
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0003ECDC File Offset: 0x0003CEDC
		public static Task StartTask(object caller, TopLevelHandlerOption options, Action action)
		{
			return Task.Factory.StartNew(delegate
			{
				TopLevelHandler.Run(caller, options, action);
			});
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0003ED1A File Offset: 0x0003CF1A
		public static Task<TResult> FromResult<TResult>(TResult value)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetResult(value);
			return taskCompletionSource.Task;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0003ED2D File Offset: 0x0003CF2D
		public static Task FromResult()
		{
			return ExtendedTask.FromResult<bool>(true);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0003ED35 File Offset: 0x0003CF35
		public static Task<TResult> FromException<TResult>(Exception ex)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetException(ex);
			return taskCompletionSource.Task;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0003ED48 File Offset: 0x0003CF48
		public static Task FromException(Exception ex)
		{
			return ExtendedTask.FromException<bool>(ex);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0003ED50 File Offset: 0x0003CF50
		public static Task Then(this Task task, [NotNull] Action<Task> continuationFunction)
		{
			Ensure.ArgNotNull<Action<Task>>(continuationFunction, "continuationFunction");
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			task.ContinueWith(delegate(Task task1)
			{
				if (task1.IsFaulted)
				{
					tcs.TrySetException(task1.Exception.InnerExceptions);
					return;
				}
				if (task1.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				Exception ex = TopLevelHandler.Run(task1, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					continuationFunction(task1);
					tcs.TrySetResult(true);
				});
				if (ex != null)
				{
					tcs.TrySetException(ex);
				}
			});
			return tcs.Task;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0003EDA3 File Offset: 0x0003CFA3
		public static void DoNotWait(this Task task)
		{
			task.ContinueWith(delegate(Task t)
			{
				if (t.Exception != null)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "ExtendedTask.DoNotWait error: \n{0}", new object[] { t.Exception });
				}
			});
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0003EDCC File Offset: 0x0003CFCC
		public static Task Then<TResult>(this Task<TResult> task, [NotNull] Action<Task<TResult>> continuationFunction)
		{
			Ensure.ArgNotNull<Action<Task<TResult>>>(continuationFunction, "continuationFunction");
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			task.ContinueWith(delegate(Task<TResult> task1)
			{
				if (task1.IsFaulted)
				{
					tcs.TrySetException(task1.Exception.InnerExceptions);
					return;
				}
				if (task1.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				Exception ex = TopLevelHandler.Run(task1, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					continuationFunction(task1);
					tcs.TrySetResult(true);
				});
				if (ex != null)
				{
					tcs.TrySetException(ex);
				}
			});
			return tcs.Task;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0003EE20 File Offset: 0x0003D020
		public static Task<T> Then<T>(this Task<T> task, [NotNull] Func<Task<T>, T> continuationFunction)
		{
			Ensure.ArgNotNull<Func<Task<T>, T>>(continuationFunction, "continuationFunction");
			TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
			task.ContinueWith(delegate(Task<T> task1)
			{
				if (task1.IsFaulted)
				{
					tcs.TrySetException(task1.Exception.InnerExceptions);
					return;
				}
				if (task1.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				Exception ex = TopLevelHandler.Run(task1, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					tcs.TrySetResult(continuationFunction(task1));
				});
				if (ex != null)
				{
					tcs.TrySetException(ex);
				}
			});
			return tcs.Task;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0003EE74 File Offset: 0x0003D074
		public static Task<T2> Then<T1, T2>([NotNull] this Task<T1> first, [NotNull] Func<T1, Task<T2>> next)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Task<T1>>(first, "first");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<T1, Task<T2>>>(next, "next");
			TaskCompletionSource<T2> tcs = new TaskCompletionSource<T2>();
			Action<Task<T2>> <>9__2;
			first.ContinueWith(delegate(Task<T1> first1)
			{
				if (first1.IsFaulted)
				{
					tcs.TrySetException(first1.Exception.InnerException);
					return;
				}
				if (first1.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				Exception ex = TopLevelHandler.Run(first1, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					Task<T2> task = next(first1.Result);
					if (task == null)
					{
						tcs.TrySetCanceled();
						return;
					}
					Task<T2> task2 = task;
					Action<Task<T2>> action;
					if ((action = <>9__2) == null)
					{
						action = (<>9__2 = delegate(Task<T2> nextTask1)
						{
							if (nextTask1.IsFaulted)
							{
								tcs.TrySetException(nextTask1.Exception.InnerException);
								return;
							}
							if (nextTask1.IsCanceled)
							{
								tcs.TrySetCanceled();
								return;
							}
							tcs.TrySetResult(nextTask1.Result);
						});
					}
					task2.ContinueWith(action, TaskContinuationOptions.ExecuteSynchronously);
				});
				if (ex != null)
				{
					tcs.TrySetException(ex);
				}
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0003EED8 File Offset: 0x0003D0D8
		public static Task<TResult> SetWhenTaskEnds<TResult>(this TaskCompletionSource<TResult> tcs, [NotNull] Task<TResult> task, Action action)
		{
			Ensure.ArgNotNull<Task<TResult>>(task, "task");
			task.ContinueWith(delegate(Task<TResult> task1)
			{
				if (action != null)
				{
					TopLevelHandler.Run(tcs, TopLevelHandlerOption.SwallowNothing, action);
				}
				if (task1.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				if (task1.IsFaulted)
				{
					tcs.TrySetException(task1.Exception.InnerExceptions);
					return;
				}
				tcs.TrySetResult(task1.Result);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0003EF28 File Offset: 0x0003D128
		public static Task SetWhenTaskEnds(this TaskCompletionSource<bool> tcs, [NotNull] Task task, Action action)
		{
			Ensure.ArgNotNull<Task>(task, "task");
			task.ContinueWith(delegate(Task task1)
			{
				if (action != null)
				{
					TopLevelHandler.Run(tcs, TopLevelHandlerOption.SwallowNothing, action);
				}
				if (task1.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				if (task1.IsFaulted)
				{
					tcs.TrySetException(task1.Exception.InnerExceptions);
					return;
				}
				tcs.TrySetResult(true);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0003EF78 File Offset: 0x0003D178
		public static Task<TResult> SetWhenTaskEnds<TResult>(this TaskCompletionSource<TResult> tcs, [NotNull] Task task, Action action, TResult value)
		{
			Ensure.ArgNotNull<Task>(task, "task");
			task.ContinueWith(delegate(Task task1)
			{
				if (action != null)
				{
					TopLevelHandler.Run(tcs, TopLevelHandlerOption.SwallowNothing, action);
				}
				if (task1.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				if (task1.IsFaulted)
				{
					tcs.TrySetException(task1.Exception.InnerExceptions);
					return;
				}
				tcs.TrySetResult(value);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0003EFD0 File Offset: 0x0003D1D0
		public static async Task TimeoutAfter(this Task task, TimeSpan timeout, Action<Task> continuationOnTimeout = null)
		{
			if (task.IsCompleted || timeout == TimeConstants.InfiniteTimeSpan)
			{
				await task;
			}
			else
			{
				if (timeout == TimeSpan.Zero)
				{
					if (continuationOnTimeout != null)
					{
						task.ContinueWith(continuationOnTimeout).DoNotWait();
					}
					else
					{
						task.DoNotWait();
					}
					throw new TaskTimedOutException();
				}
				CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
				Task task2 = Task.Delay(timeout, cancellationTokenSource.Token);
				TaskAwaiter<Task> taskAwaiter = Task.WhenAny(new Task[] { task, task2 }).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<Task> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<Task>);
				}
				if (taskAwaiter.GetResult() != task)
				{
					if (continuationOnTimeout != null)
					{
						task.ContinueWith(continuationOnTimeout).DoNotWait();
					}
					else
					{
						task.DoNotWait();
					}
					throw new TaskTimedOutException();
				}
				cancellationTokenSource.Cancel();
				await task;
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0003F028 File Offset: 0x0003D228
		public static async Task TimeoutAfter(this Task task, AsyncTaskDelayer asyncTaskDelayer, TimeSpan timeout, Action<Task> continuationOnTimeout = null)
		{
			if (task.IsCompleted || timeout == TimeConstants.InfiniteTimeSpan)
			{
				await task;
			}
			else
			{
				if (timeout == TimeSpan.Zero)
				{
					if (continuationOnTimeout != null)
					{
						task.ContinueWith(continuationOnTimeout).DoNotWait();
					}
					else
					{
						task.DoNotWait();
					}
					throw new TaskTimedOutException();
				}
				using (AsyncTaskDelayer.DelayedTask delayedTask = asyncTaskDelayer.Delay(timeout))
				{
					TaskAwaiter<Task> taskAwaiter = Task.WhenAny(new Task[] { task, delayedTask.Task }).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<Task> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<Task>);
					}
					if (taskAwaiter.GetResult() != task)
					{
						if (continuationOnTimeout != null)
						{
							task.ContinueWith(continuationOnTimeout).DoNotWait();
						}
						else
						{
							task.DoNotWait();
						}
						throw new TaskTimedOutException();
					}
					await task;
				}
				AsyncTaskDelayer.DelayedTask delayedTask = null;
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0003F088 File Offset: 0x0003D288
		public static async Task<bool> TimeoutAfter(this Task task, TimeSpan timeout, bool throwException)
		{
			bool flag;
			try
			{
				await task.TimeoutAfter(timeout, null);
				flag = false;
			}
			catch (TaskTimedOutException)
			{
				if (throwException)
				{
					throw;
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0003F0E0 File Offset: 0x0003D2E0
		public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, AsyncTaskDelayer asyncTaskDelayer, TimeSpan timeout, Action<Task> continuationOnTimeout = null)
		{
			await task.TimeoutAfter(asyncTaskDelayer, timeout, continuationOnTimeout);
			return task.Result;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0003F140 File Offset: 0x0003D340
		public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout, Action<Task> continuationOnTimeout = null)
		{
			await task.TimeoutAfter(timeout, continuationOnTimeout);
			return task.Result;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0003F198 File Offset: 0x0003D398
		public static async Task SwallowNonfatal(Func<Task> asyncMethod)
		{
			Task taskFromAsyncMethod = null;
			try
			{
				taskFromAsyncMethod = asyncMethod();
				if (taskFromAsyncMethod != null)
				{
					await taskFromAsyncMethod;
				}
			}
			catch (Exception ex)
			{
				if (taskFromAsyncMethod == null || taskFromAsyncMethod.Exception == null)
				{
					if (ex.IsFatal())
					{
						throw;
					}
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("Non-fatal exception swallowed in ExtendedTask.SwallowNonFatal: {0}{1}", new object[]
					{
						Environment.NewLine,
						ex
					});
				}
				else
				{
					if (taskFromAsyncMethod.Exception.IsFatal())
					{
						throw taskFromAsyncMethod.Exception;
					}
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("Non-fatal exception swallowed in ExtendedTask.SwallowNonFatal: {0}{1}", new object[]
					{
						Environment.NewLine,
						taskFromAsyncMethod.Exception
					});
				}
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0003F1E0 File Offset: 0x0003D3E0
		public static async Task WithCancellation(this Task task, CancellationToken cancellationToken)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
			using (cancellationToken.Register(delegate(object s)
			{
				((TaskCompletionSource<bool>)s).TrySetResult(true);
			}, taskCompletionSource))
			{
				object obj = task;
				Task task2 = await Task.WhenAny(new Task[] { task, taskCompletionSource.Task });
				if (obj != task2)
				{
					obj = null;
					throw new OperationCanceledException(cancellationToken);
				}
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			await task;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0003F230 File Offset: 0x0003D430
		public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
		{
			await task.WithCancellation(cancellationToken);
			return task.Result;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0003F280 File Offset: 0x0003D480
		public static Task<T> WhenFirst<T>(this IEnumerable<Task<T>> tasks, Predicate<T> predicate = null)
		{
			TaskCompletionSource<T> sharedTaskCompletionForAllTasks = new TaskCompletionSource<T>();
			int completedCount = 0;
			int tasksCount = tasks.Count<Task<T>>();
			ConcurrentBag<Exception> exceptionBag = new ConcurrentBag<Exception>();
			Action<Task<T>> <>9__0;
			foreach (Task<T> task in tasks)
			{
				Action<Task<T>> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(Task<T> t)
					{
						if (t.Exception != null)
						{
							exceptionBag.Add(t.Exception);
						}
						else if (predicate == null || predicate(t.Result))
						{
							sharedTaskCompletionForAllTasks.TrySetResult(t.Result);
						}
						if (Interlocked.Increment(ref completedCount) == tasksCount)
						{
							sharedTaskCompletionForAllTasks.TrySetException(new AggregateException(exceptionBag));
						}
					});
				}
				task.ContinueWith(action);
			}
			return sharedTaskCompletionForAllTasks.Task;
		}
	}
}
