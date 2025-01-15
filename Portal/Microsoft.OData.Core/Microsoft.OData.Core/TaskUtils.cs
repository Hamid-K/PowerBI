using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000D0 RID: 208
	internal static class TaskUtils
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00018404 File Offset: 0x00016604
		internal static Task CompletedTask
		{
			get
			{
				if (TaskUtils.completedTask == null)
				{
					TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
					taskCompletionSource.SetResult(null);
					TaskUtils.completedTask = taskCompletionSource.Task;
				}
				return TaskUtils.completedTask;
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00018438 File Offset: 0x00016638
		internal static Task<T> GetCompletedTask<T>(T value)
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetResult(value);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00018458 File Offset: 0x00016658
		internal static Task GetFaultedTask(Exception exception)
		{
			return TaskUtils.GetFaultedTask<object>(exception);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00018460 File Offset: 0x00016660
		internal static Task<T> GetFaultedTask<T>(Exception exception)
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00018480 File Offset: 0x00016680
		internal static Task GetTaskForSynchronousOperation(Action synchronousOperation)
		{
			Task faultedTask;
			try
			{
				synchronousOperation();
				faultedTask = TaskUtils.CompletedTask;
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				faultedTask = TaskUtils.GetFaultedTask(ex);
			}
			return faultedTask;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000184C0 File Offset: 0x000166C0
		internal static Task<T> GetTaskForSynchronousOperation<T>(Func<T> synchronousOperation)
		{
			Task<T> faultedTask;
			try
			{
				T t = synchronousOperation();
				faultedTask = TaskUtils.GetCompletedTask<T>(t);
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				faultedTask = TaskUtils.GetFaultedTask<T>(ex);
			}
			return faultedTask;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00018504 File Offset: 0x00016704
		internal static Task GetTaskForSynchronousOperationReturningTask(Func<Task> synchronousOperation)
		{
			Task task;
			try
			{
				task = synchronousOperation();
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				task = TaskUtils.GetFaultedTask(ex);
			}
			return task;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00018540 File Offset: 0x00016740
		internal static Task<TResult> GetTaskForSynchronousOperationReturningTask<TResult>(Func<Task<TResult>> synchronousOperation)
		{
			Task<TResult> task;
			try
			{
				task = synchronousOperation();
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				task = TaskUtils.GetFaultedTask<TResult>(ex);
			}
			return task;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001857C File Offset: 0x0001677C
		internal static Task FollowOnSuccessWith(this Task antecedentTask, Action<Task> operation)
		{
			return TaskUtils.FollowOnSuccessWithImplementation<object>(antecedentTask, delegate(Task t)
			{
				operation(t);
				return null;
			});
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000185A8 File Offset: 0x000167A8
		internal static Task<TFollowupTaskResult> FollowOnSuccessWith<TFollowupTaskResult>(this Task antecedentTask, Func<Task, TFollowupTaskResult> operation)
		{
			return TaskUtils.FollowOnSuccessWithImplementation<TFollowupTaskResult>(antecedentTask, operation);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000185B4 File Offset: 0x000167B4
		internal static Task FollowOnSuccessWith<TAntecedentTaskResult>(this Task<TAntecedentTaskResult> antecedentTask, Action<Task<TAntecedentTaskResult>> operation)
		{
			return TaskUtils.FollowOnSuccessWithImplementation<object>(antecedentTask, delegate(Task t)
			{
				operation((Task<TAntecedentTaskResult>)t);
				return null;
			});
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000185E0 File Offset: 0x000167E0
		internal static Task<TFollowupTaskResult> FollowOnSuccessWith<TAntecedentTaskResult, TFollowupTaskResult>(this Task<TAntecedentTaskResult> antecedentTask, Func<Task<TAntecedentTaskResult>, TFollowupTaskResult> operation)
		{
			return TaskUtils.FollowOnSuccessWithImplementation<TFollowupTaskResult>(antecedentTask, (Task t) => operation((Task<TAntecedentTaskResult>)t));
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001860C File Offset: 0x0001680C
		internal static Task FollowOnSuccessWithTask(this Task antecedentTask, Func<Task, Task> operation)
		{
			TaskCompletionSource<Task> taskCompletionSource = new TaskCompletionSource<Task>();
			antecedentTask.ContinueWith(delegate(Task taskToContinueOn)
			{
				TaskUtils.FollowOnSuccessWithContinuation<Task>(taskToContinueOn, taskCompletionSource, operation);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return taskCompletionSource.Task.Unwrap();
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001865C File Offset: 0x0001685C
		internal static Task<TFollowupTaskResult> FollowOnSuccessWithTask<TFollowupTaskResult>(this Task antecedentTask, Func<Task, Task<TFollowupTaskResult>> operation)
		{
			TaskCompletionSource<Task<TFollowupTaskResult>> taskCompletionSource = new TaskCompletionSource<Task<TFollowupTaskResult>>();
			antecedentTask.ContinueWith(delegate(Task taskToContinueOn)
			{
				TaskUtils.FollowOnSuccessWithContinuation<Task<TFollowupTaskResult>>(taskToContinueOn, taskCompletionSource, operation);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return taskCompletionSource.Task.Unwrap<TFollowupTaskResult>();
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000186AC File Offset: 0x000168AC
		internal static Task FollowOnSuccessWithTask<TAntecedentTaskResult>(this Task<TAntecedentTaskResult> antecedentTask, Func<Task<TAntecedentTaskResult>, Task> operation)
		{
			TaskCompletionSource<Task> taskCompletionSource = new TaskCompletionSource<Task>();
			Func<Task, Task> <>9__1;
			antecedentTask.ContinueWith(delegate(Task<TAntecedentTaskResult> taskToContinueOn)
			{
				TaskCompletionSource<Task> taskCompletionSource2 = taskCompletionSource;
				Func<Task, Task> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (Task taskForOperation) => operation((Task<TAntecedentTaskResult>)taskForOperation));
				}
				TaskUtils.FollowOnSuccessWithContinuation<Task>(taskToContinueOn, taskCompletionSource2, func);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return taskCompletionSource.Task.Unwrap();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000186FC File Offset: 0x000168FC
		internal static Task<TFollowupTaskResult> FollowOnSuccessWithTask<TAntecedentTaskResult, TFollowupTaskResult>(this Task<TAntecedentTaskResult> antecedentTask, Func<Task<TAntecedentTaskResult>, Task<TFollowupTaskResult>> operation)
		{
			TaskCompletionSource<Task<TFollowupTaskResult>> taskCompletionSource = new TaskCompletionSource<Task<TFollowupTaskResult>>();
			Func<Task, Task<TFollowupTaskResult>> <>9__1;
			antecedentTask.ContinueWith(delegate(Task<TAntecedentTaskResult> taskToContinueOn)
			{
				TaskCompletionSource<Task<TFollowupTaskResult>> taskCompletionSource2 = taskCompletionSource;
				Func<Task, Task<TFollowupTaskResult>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (Task taskForOperation) => operation((Task<TAntecedentTaskResult>)taskForOperation));
				}
				TaskUtils.FollowOnSuccessWithContinuation<Task<TFollowupTaskResult>>(taskToContinueOn, taskCompletionSource2, func);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return taskCompletionSource.Task.Unwrap<TFollowupTaskResult>();
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00018749 File Offset: 0x00016949
		internal static Task FollowOnFaultWith(this Task antecedentTask, Action<Task> operation)
		{
			return TaskUtils.FollowOnFaultWithImplementation<object>(antecedentTask, (Task t) => null, operation);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00018774 File Offset: 0x00016974
		internal static Task<TResult> FollowOnFaultWith<TResult>(this Task<TResult> antecedentTask, Action<Task<TResult>> operation)
		{
			return TaskUtils.FollowOnFaultWithImplementation<TResult>(antecedentTask, (Task t) => ((Task<TResult>)t).Result, delegate(Task t)
			{
				operation((Task<TResult>)t);
			});
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000187BF File Offset: 0x000169BF
		internal static Task<TResult> FollowOnFaultAndCatchExceptionWith<TResult, TExceptionType>(this Task<TResult> antecedentTask, Func<TExceptionType, TResult> catchBlock) where TExceptionType : Exception
		{
			return TaskUtils.FollowOnFaultAndCatchExceptionWithImplementation<TResult, TExceptionType>(antecedentTask, (Task t) => ((Task<TResult>)t).Result, catchBlock);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000187E7 File Offset: 0x000169E7
		internal static Task FollowAlwaysWith(this Task antecedentTask, Action<Task> operation)
		{
			return antecedentTask.FollowAlwaysWithImplementation((Task t) => null, operation);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00018810 File Offset: 0x00016A10
		internal static Task<TResult> FollowAlwaysWith<TResult>(this Task<TResult> antecedentTask, Action<Task<TResult>> operation)
		{
			return antecedentTask.FollowAlwaysWithImplementation((Task t) => ((Task<TResult>)t).Result, delegate(Task t)
			{
				operation((Task<TResult>)t);
			});
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001885B File Offset: 0x00016A5B
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Need to access t.Exception to invoke the getter which will mark the Task to not throw the exception.")]
		internal static Task IgnoreExceptions(this Task task)
		{
			task.ContinueWith(delegate(Task t)
			{
				AggregateException exception = t.Exception;
			}, CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			return task;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00018893 File Offset: 0x00016A93
		internal static TaskScheduler GetTargetScheduler(this TaskFactory factory)
		{
			return factory.Scheduler ?? TaskScheduler.Current;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000188A4 File Offset: 0x00016AA4
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Stores the exception so that it doesn't bring down the process but isntead rethrows on the task calling thread.")]
		internal static Task Iterate(this TaskFactory factory, IEnumerable<Task> source)
		{
			IEnumerator<Task> enumerator = source.GetEnumerator();
			TaskCompletionSource<object> trc = new TaskCompletionSource<object>(null, factory.CreationOptions);
			trc.Task.ContinueWith(delegate(Task<object> _)
			{
				enumerator.Dispose();
			}, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			Action<Task> recursiveBody = null;
			recursiveBody = delegate(Task antecedent)
			{
				try
				{
					if (antecedent != null && antecedent.IsFaulted)
					{
						trc.TrySetException(antecedent.Exception);
					}
					else if (enumerator.MoveNext())
					{
						Task task = enumerator.Current;
						task.ContinueWith(recursiveBody).IgnoreExceptions();
					}
					else
					{
						trc.TrySetResult(null);
					}
				}
				catch (Exception ex)
				{
					if (!ExceptionUtils.IsCatchableExceptionType(ex))
					{
						throw;
					}
					OperationCanceledException ex2 = ex as OperationCanceledException;
					if (ex2 != null && ex2.CancellationToken == factory.CancellationToken)
					{
						trc.TrySetCanceled();
					}
					else
					{
						trc.TrySetException(ex);
					}
				}
			};
			factory.StartNew(delegate
			{
				recursiveBody(null);
			}, CancellationToken.None, TaskCreationOptions.None, factory.GetTargetScheduler()).IgnoreExceptions();
			return trc.Task;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00018960 File Offset: 0x00016B60
		private static void FollowOnSuccessWithContinuation<TResult>(Task antecedentTask, TaskCompletionSource<TResult> taskCompletionSource, Func<Task, TResult> operation)
		{
			switch (antecedentTask.Status)
			{
			case TaskStatus.RanToCompletion:
				try
				{
					taskCompletionSource.TrySetResult(operation(antecedentTask));
					return;
				}
				catch (Exception ex)
				{
					if (!ExceptionUtils.IsCatchableExceptionType(ex))
					{
						throw;
					}
					taskCompletionSource.TrySetException(ex);
					return;
				}
				break;
			case TaskStatus.Canceled:
				taskCompletionSource.TrySetCanceled();
				return;
			case TaskStatus.Faulted:
				break;
			default:
				return;
			}
			taskCompletionSource.TrySetException(antecedentTask.Exception);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000189D4 File Offset: 0x00016BD4
		private static Task<TResult> FollowOnSuccessWithImplementation<TResult>(Task antecedentTask, Func<Task, TResult> operation)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			antecedentTask.ContinueWith(delegate(Task taskToContinueOn)
			{
				TaskUtils.FollowOnSuccessWithContinuation<TResult>(taskToContinueOn, taskCompletionSource, operation);
			}, TaskContinuationOptions.ExecuteSynchronously).IgnoreExceptions();
			return taskCompletionSource.Task;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00018A24 File Offset: 0x00016C24
		private static Task<TResult> FollowOnFaultWithImplementation<TResult>(Task antecedentTask, Func<Task, TResult> getTaskResult, Action<Task> operation)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			antecedentTask.ContinueWith(delegate(Task t)
			{
				switch (t.Status)
				{
				case TaskStatus.RanToCompletion:
					taskCompletionSource.TrySetResult(getTaskResult(t));
					return;
				case TaskStatus.Canceled:
					break;
				case TaskStatus.Faulted:
					try
					{
						operation(t);
						taskCompletionSource.TrySetException(t.Exception);
						return;
					}
					catch (Exception ex)
					{
						if (!ExceptionUtils.IsCatchableExceptionType(ex))
						{
							throw;
						}
						AggregateException ex2 = new AggregateException(new Exception[] { t.Exception, ex });
						taskCompletionSource.TrySetException(ex2);
						return;
					}
					break;
				default:
					return;
				}
				taskCompletionSource.TrySetCanceled();
			}, TaskContinuationOptions.ExecuteSynchronously).IgnoreExceptions();
			return taskCompletionSource.Task;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00018A78 File Offset: 0x00016C78
		private static Task<TResult> FollowOnFaultAndCatchExceptionWithImplementation<TResult, TExceptionType>(Task antecedentTask, Func<Task, TResult> getTaskResult, Func<TExceptionType, TResult> catchBlock) where TExceptionType : Exception
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			antecedentTask.ContinueWith(delegate(Task t)
			{
				switch (t.Status)
				{
				case TaskStatus.RanToCompletion:
					taskCompletionSource.TrySetResult(getTaskResult(t));
					return;
				case TaskStatus.Canceled:
					taskCompletionSource.TrySetCanceled();
					break;
				case TaskStatus.Faulted:
				{
					Exception ex = t.Exception;
					AggregateException ex2 = ex as AggregateException;
					if (ex2 != null)
					{
						ex2 = ex2.Flatten();
						if (ex2.InnerExceptions.Count == 1)
						{
							ex = ex2.InnerExceptions[0];
						}
					}
					if (ex is TExceptionType)
					{
						try
						{
							taskCompletionSource.TrySetResult(catchBlock((TExceptionType)((object)ex)));
							break;
						}
						catch (Exception ex3)
						{
							if (!ExceptionUtils.IsCatchableExceptionType(ex3))
							{
								throw;
							}
							AggregateException ex4 = new AggregateException(new Exception[] { ex, ex3 });
							taskCompletionSource.TrySetException(ex4);
							break;
						}
					}
					taskCompletionSource.TrySetException(ex);
					return;
				}
				default:
					return;
				}
			}, TaskContinuationOptions.ExecuteSynchronously).IgnoreExceptions();
			return taskCompletionSource.Task;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00018ACC File Offset: 0x00016CCC
		private static Task<TResult> FollowAlwaysWithImplementation<TResult>(this Task antecedentTask, Func<Task, TResult> getTaskResult, Action<Task> operation)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			antecedentTask.ContinueWith(delegate(Task t)
			{
				Exception ex = null;
				try
				{
					operation(t);
				}
				catch (Exception ex2)
				{
					if (!ExceptionUtils.IsCatchableExceptionType(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				switch (t.Status)
				{
				case TaskStatus.RanToCompletion:
					if (ex != null)
					{
						taskCompletionSource.TrySetException(ex);
						return;
					}
					taskCompletionSource.TrySetResult(getTaskResult(t));
					return;
				case TaskStatus.Canceled:
					if (ex != null)
					{
						taskCompletionSource.TrySetException(ex);
						return;
					}
					taskCompletionSource.TrySetCanceled();
					return;
				case TaskStatus.Faulted:
				{
					Exception ex3 = t.Exception;
					if (ex != null)
					{
						ex3 = new AggregateException(new Exception[] { ex3, ex });
					}
					taskCompletionSource.TrySetException(ex3);
					return;
				}
				default:
					return;
				}
			}, TaskContinuationOptions.ExecuteSynchronously).IgnoreExceptions();
			return taskCompletionSource.Task;
		}

		// Token: 0x0400035D RID: 861
		private static Task completedTask;
	}
}
