using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000297 RID: 663
	internal class InternalDispatcher<TInterceptor> where TInterceptor : class, IDbInterceptor
	{
		// Token: 0x06002108 RID: 8456 RVA: 0x0005CB50 File Offset: 0x0005AD50
		public void Add(IDbInterceptor interceptor)
		{
			TInterceptor tinterceptor = interceptor as TInterceptor;
			if (tinterceptor == null)
			{
				return;
			}
			object @lock = this._lock;
			lock (@lock)
			{
				List<TInterceptor> list = this._interceptors.ToList<TInterceptor>();
				list.Add(tinterceptor);
				this._interceptors = list;
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0005CBC0 File Offset: 0x0005ADC0
		public void Remove(IDbInterceptor interceptor)
		{
			TInterceptor tinterceptor = interceptor as TInterceptor;
			if (tinterceptor == null)
			{
				return;
			}
			object @lock = this._lock;
			lock (@lock)
			{
				List<TInterceptor> list = this._interceptors.ToList<TInterceptor>();
				list.Remove(tinterceptor);
				this._interceptors = list;
			}
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0005CC30 File Offset: 0x0005AE30
		public TResult Dispatch<TResult>(TResult result, Func<TResult, TInterceptor, TResult> accumulator)
		{
			if (this._interceptors.Count != 0)
			{
				return this._interceptors.Aggregate(result, accumulator);
			}
			return result;
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x0005CC52 File Offset: 0x0005AE52
		public void Dispatch(Action<TInterceptor> action)
		{
			if (this._interceptors.Count != 0)
			{
				this._interceptors.Each(action);
			}
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0005CC74 File Offset: 0x0005AE74
		public TResult Dispatch<TInterceptionContext, TResult>(TResult result, TInterceptionContext interceptionContext, Action<TInterceptor, TInterceptionContext> intercept) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext<TResult>
		{
			if (this._interceptors.Count == 0)
			{
				return result;
			}
			interceptionContext.MutableData.SetExecuted(result);
			foreach (TInterceptor tinterceptor in this._interceptors)
			{
				intercept(tinterceptor, interceptionContext);
			}
			if (interceptionContext.MutableData.Exception != null)
			{
				throw interceptionContext.MutableData.Exception;
			}
			return interceptionContext.MutableData.Result;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x0005CD20 File Offset: 0x0005AF20
		public void Dispatch<TTarget, TInterceptionContext>(TTarget target, Action<TTarget, TInterceptionContext> operation, TInterceptionContext interceptionContext, Action<TInterceptor, TTarget, TInterceptionContext> executing, Action<TInterceptor, TTarget, TInterceptionContext> executed) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext
		{
			if (this._interceptors.Count == 0)
			{
				operation(target, interceptionContext);
				return;
			}
			foreach (TInterceptor tinterceptor in this._interceptors)
			{
				executing(tinterceptor, target, interceptionContext);
			}
			if (!interceptionContext.MutableData.IsExecutionSuppressed)
			{
				try
				{
					operation(target, interceptionContext);
					interceptionContext.MutableData.HasExecuted = true;
				}
				catch (Exception ex)
				{
					interceptionContext.MutableData.SetExceptionThrown(ex);
					foreach (TInterceptor tinterceptor2 in this._interceptors)
					{
						executed(tinterceptor2, target, interceptionContext);
					}
					if (interceptionContext.MutableData.Exception == ex)
					{
						throw;
					}
				}
			}
			if (interceptionContext.MutableData.OriginalException == null)
			{
				foreach (TInterceptor tinterceptor3 in this._interceptors)
				{
					executed(tinterceptor3, target, interceptionContext);
				}
			}
			if (interceptionContext.MutableData.Exception != null)
			{
				throw interceptionContext.MutableData.Exception;
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0005CEB8 File Offset: 0x0005B0B8
		public TResult Dispatch<TTarget, TInterceptionContext, TResult>(TTarget target, Func<TTarget, TInterceptionContext, TResult> operation, TInterceptionContext interceptionContext, Action<TInterceptor, TTarget, TInterceptionContext> executing, Action<TInterceptor, TTarget, TInterceptionContext> executed) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext<TResult>
		{
			if (this._interceptors.Count == 0)
			{
				return operation(target, interceptionContext);
			}
			foreach (TInterceptor tinterceptor in this._interceptors)
			{
				executing(tinterceptor, target, interceptionContext);
			}
			if (!interceptionContext.MutableData.IsExecutionSuppressed)
			{
				try
				{
					interceptionContext.MutableData.SetExecuted(operation(target, interceptionContext));
				}
				catch (Exception ex)
				{
					interceptionContext.MutableData.SetExceptionThrown(ex);
					foreach (TInterceptor tinterceptor2 in this._interceptors)
					{
						executed(tinterceptor2, target, interceptionContext);
					}
					if (interceptionContext.MutableData.Exception == ex)
					{
						throw;
					}
				}
			}
			if (interceptionContext.MutableData.OriginalException == null)
			{
				foreach (TInterceptor tinterceptor3 in this._interceptors)
				{
					executed(tinterceptor3, target, interceptionContext);
				}
			}
			if (interceptionContext.MutableData.Exception != null)
			{
				throw interceptionContext.MutableData.Exception;
			}
			return interceptionContext.MutableData.Result;
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0005D060 File Offset: 0x0005B260
		public Task DispatchAsync<TTarget, TInterceptionContext>(TTarget target, Func<TTarget, TInterceptionContext, CancellationToken, Task> operation, TInterceptionContext interceptionContext, Action<TInterceptor, TTarget, TInterceptionContext> executing, Action<TInterceptor, TTarget, TInterceptionContext> executed, CancellationToken cancellationToken) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext
		{
			if (this._interceptors.Count == 0)
			{
				return operation(target, interceptionContext, cancellationToken);
			}
			foreach (TInterceptor tinterceptor in this._interceptors)
			{
				executing(tinterceptor, target, interceptionContext);
			}
			Task task = (interceptionContext.MutableData.IsExecutionSuppressed ? Task.FromResult<object>(null) : operation(target, interceptionContext, cancellationToken));
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			task.ContinueWith(delegate(Task t)
			{
				interceptionContext.MutableData.TaskStatus = t.Status;
				if (t.IsFaulted)
				{
					interceptionContext.MutableData.SetExceptionThrown(t.Exception.InnerException);
				}
				else if (!interceptionContext.MutableData.IsExecutionSuppressed)
				{
					interceptionContext.MutableData.HasExecuted = true;
				}
				try
				{
					foreach (TInterceptor tinterceptor2 in this._interceptors)
					{
						executed(tinterceptor2, target, interceptionContext);
					}
				}
				catch (Exception ex)
				{
					interceptionContext.MutableData.Exception = ex;
				}
				if (interceptionContext.MutableData.Exception != null)
				{
					tcs.SetException(interceptionContext.MutableData.Exception);
					return;
				}
				if (t.IsCanceled)
				{
					tcs.SetCanceled();
					return;
				}
				tcs.SetResult(null);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0005D16C File Offset: 0x0005B36C
		public Task<TResult> DispatchAsync<TTarget, TInterceptionContext, TResult>(TTarget target, Func<TTarget, TInterceptionContext, CancellationToken, Task<TResult>> operation, TInterceptionContext interceptionContext, Action<TInterceptor, TTarget, TInterceptionContext> executing, Action<TInterceptor, TTarget, TInterceptionContext> executed, CancellationToken cancellationToken) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext<TResult>
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this._interceptors.Count == 0)
			{
				return operation(target, interceptionContext, cancellationToken);
			}
			foreach (TInterceptor tinterceptor in this._interceptors)
			{
				executing(tinterceptor, target, interceptionContext);
			}
			Task<TResult> task = (interceptionContext.MutableData.IsExecutionSuppressed ? Task.FromResult<TResult>(interceptionContext.MutableData.Result) : operation(target, interceptionContext, cancellationToken));
			TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();
			task.ContinueWith(delegate(Task<TResult> t)
			{
				interceptionContext.MutableData.TaskStatus = t.Status;
				if (t.IsFaulted)
				{
					interceptionContext.MutableData.SetExceptionThrown(t.Exception.InnerException);
				}
				else if (!interceptionContext.MutableData.IsExecutionSuppressed)
				{
					interceptionContext.MutableData.SetExecuted((t.IsCanceled || t.IsFaulted) ? default(TResult) : t.Result);
				}
				try
				{
					foreach (TInterceptor tinterceptor2 in this._interceptors)
					{
						executed(tinterceptor2, target, interceptionContext);
					}
				}
				catch (Exception ex)
				{
					interceptionContext.MutableData.Exception = ex;
				}
				if (interceptionContext.MutableData.Exception != null)
				{
					tcs.SetException(interceptionContext.MutableData.Exception);
					return;
				}
				if (t.IsCanceled)
				{
					tcs.SetCanceled();
					return;
				}
				tcs.SetResult(interceptionContext.MutableData.Result);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x04000B93 RID: 2963
		private volatile List<TInterceptor> _interceptors = new List<TInterceptor>();

		// Token: 0x04000B94 RID: 2964
		private readonly object _lock = new object();
	}
}
