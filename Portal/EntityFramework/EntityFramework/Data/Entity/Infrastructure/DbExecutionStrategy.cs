using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000228 RID: 552
	public abstract class DbExecutionStrategy : IDbExecutionStrategy
	{
		// Token: 0x06001D08 RID: 7432 RVA: 0x00052C5E File Offset: 0x00050E5E
		protected DbExecutionStrategy()
			: this(5, DbExecutionStrategy.DefaultMaxDelay)
		{
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00052C6C File Offset: 0x00050E6C
		protected DbExecutionStrategy(int maxRetryCount, TimeSpan maxDelay)
		{
			if (maxRetryCount < 0)
			{
				throw new ArgumentOutOfRangeException("maxRetryCount");
			}
			if (maxDelay.TotalMilliseconds < 0.0)
			{
				throw new ArgumentOutOfRangeException("maxDelay");
			}
			this._maxRetryCount = maxRetryCount;
			this._maxDelay = maxDelay;
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x00052CCF File Offset: 0x00050ECF
		public bool RetriesOnFailure
		{
			get
			{
				return !DbExecutionStrategy.Suspended;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x00052CDC File Offset: 0x00050EDC
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x00052D00 File Offset: 0x00050F00
		protected internal static bool Suspended
		{
			get
			{
				return ((bool?)CallContext.LogicalGetData("ExecutionStrategySuspended")).GetValueOrDefault();
			}
			set
			{
				CallContext.LogicalSetData("ExecutionStrategySuspended", value);
			}
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x00052D14 File Offset: 0x00050F14
		public void Execute(Action operation)
		{
			Check.NotNull<Action>(operation, "operation");
			this.Execute<object>(delegate
			{
				operation();
				return null;
			});
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00052D54 File Offset: 0x00050F54
		public TResult Execute<TResult>(Func<TResult> operation)
		{
			Check.NotNull<Func<TResult>>(operation, "operation");
			if (this.RetriesOnFailure)
			{
				this.EnsurePreexecutionState();
				TimeSpan? nextDelay;
				for (;;)
				{
					try
					{
						DbExecutionStrategy.Suspended = true;
						return operation();
					}
					catch (Exception ex)
					{
						if (!DbExecutionStrategy.UnwrapAndHandleException<bool>(ex, new Func<Exception, bool>(this.ShouldRetryOn)))
						{
							throw;
						}
						nextDelay = this.GetNextDelay(ex);
						if (nextDelay == null)
						{
							throw new RetryLimitExceededException(Strings.ExecutionStrategy_RetryLimitExceeded(this._maxRetryCount, base.GetType().Name), ex);
						}
					}
					finally
					{
						DbExecutionStrategy.Suspended = false;
					}
					if (nextDelay < TimeSpan.Zero)
					{
						break;
					}
					Thread.Sleep(nextDelay.Value);
				}
				throw new InvalidOperationException(Strings.ExecutionStrategy_NegativeDelay(nextDelay));
			}
			return operation();
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x00052E50 File Offset: 0x00051050
		public Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken)
		{
			DbExecutionStrategy.<>c__DisplayClass19_0 CS$<>8__locals1 = new DbExecutionStrategy.<>c__DisplayClass19_0();
			CS$<>8__locals1.operation = operation;
			Check.NotNull<Func<Task>>(CS$<>8__locals1.operation, "operation");
			if (this.RetriesOnFailure)
			{
				this.EnsurePreexecutionState();
			}
			cancellationToken.ThrowIfCancellationRequested();
			return this.ProtectedExecuteAsync<bool>(delegate
			{
				DbExecutionStrategy.<>c__DisplayClass19_0.<<ExecuteAsync>b__0>d <<ExecuteAsync>b__0>d;
				<<ExecuteAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<ExecuteAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<ExecuteAsync>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder<bool> <>t__builder = <<ExecuteAsync>b__0>d.<>t__builder;
				<>t__builder.Start<DbExecutionStrategy.<>c__DisplayClass19_0.<<ExecuteAsync>b__0>d>(ref <<ExecuteAsync>b__0>d);
				return <<ExecuteAsync>b__0>d.<>t__builder.Task;
			}, cancellationToken);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00052EA3 File Offset: 0x000510A3
		public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<Task<TResult>>>(operation, "operation");
			if (this.RetriesOnFailure)
			{
				this.EnsurePreexecutionState();
			}
			cancellationToken.ThrowIfCancellationRequested();
			return this.ProtectedExecuteAsync<TResult>(operation, cancellationToken);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00052ED0 File Offset: 0x000510D0
		private async Task<TResult> ProtectedExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
		{
			if (this.RetriesOnFailure)
			{
				TimeSpan? nextDelay;
				for (;;)
				{
					try
					{
						DbExecutionStrategy.Suspended = true;
						return await operation().WithCurrentCulture<TResult>();
					}
					catch (Exception ex)
					{
						if (!DbExecutionStrategy.UnwrapAndHandleException<bool>(ex, new Func<Exception, bool>(this.ShouldRetryOn)))
						{
							throw;
						}
						nextDelay = this.GetNextDelay(ex);
						if (nextDelay == null)
						{
							throw new RetryLimitExceededException(Strings.ExecutionStrategy_RetryLimitExceeded(this._maxRetryCount, base.GetType().Name), ex);
						}
					}
					finally
					{
						DbExecutionStrategy.Suspended = false;
					}
					if (nextDelay < TimeSpan.Zero)
					{
						break;
					}
					await Task.Delay(nextDelay.Value, cancellationToken).WithCurrentCulture();
				}
				throw new InvalidOperationException(Strings.ExecutionStrategy_NegativeDelay(nextDelay));
			}
			return await operation().WithCurrentCulture<TResult>();
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00052F25 File Offset: 0x00051125
		private void EnsurePreexecutionState()
		{
			if (Transaction.Current != null)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_ExistingTransaction(base.GetType().Name));
			}
			this._exceptionsEncountered.Clear();
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00052F58 File Offset: 0x00051158
		protected internal virtual TimeSpan? GetNextDelay(Exception lastException)
		{
			this._exceptionsEncountered.Add(lastException);
			int num = this._exceptionsEncountered.Count - 1;
			if (num < this._maxRetryCount)
			{
				double num2 = (Math.Pow(2.0, (double)num) - 1.0) * (1.0 + this._random.NextDouble() * 0.10000000000000009);
				return new TimeSpan?(TimeSpan.FromMilliseconds(Math.Min(DbExecutionStrategy.DefaultCoefficient.TotalMilliseconds * num2, this._maxDelay.TotalMilliseconds)));
			}
			return null;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00052FFC File Offset: 0x000511FC
		public static T UnwrapAndHandleException<T>(Exception exception, Func<Exception, T> exceptionHandler)
		{
			EntityException ex = exception as EntityException;
			if (ex != null)
			{
				return DbExecutionStrategy.UnwrapAndHandleException<T>(ex.InnerException, exceptionHandler);
			}
			DbUpdateException ex2 = exception as DbUpdateException;
			if (ex2 != null)
			{
				return DbExecutionStrategy.UnwrapAndHandleException<T>(ex2.InnerException, exceptionHandler);
			}
			UpdateException ex3 = exception as UpdateException;
			if (ex3 != null)
			{
				return DbExecutionStrategy.UnwrapAndHandleException<T>(ex3.InnerException, exceptionHandler);
			}
			return exceptionHandler(exception);
		}

		// Token: 0x06001D15 RID: 7445
		protected internal abstract bool ShouldRetryOn(Exception exception);

		// Token: 0x04000B12 RID: 2834
		private readonly List<Exception> _exceptionsEncountered = new List<Exception>();

		// Token: 0x04000B13 RID: 2835
		private readonly Random _random = new Random();

		// Token: 0x04000B14 RID: 2836
		private readonly int _maxRetryCount;

		// Token: 0x04000B15 RID: 2837
		private readonly TimeSpan _maxDelay;

		// Token: 0x04000B16 RID: 2838
		private const string ContextName = "ExecutionStrategySuspended";

		// Token: 0x04000B17 RID: 2839
		private const int DefaultMaxRetryCount = 5;

		// Token: 0x04000B18 RID: 2840
		private const double DefaultRandomFactor = 1.1;

		// Token: 0x04000B19 RID: 2841
		private const double DefaultExponentialBase = 2.0;

		// Token: 0x04000B1A RID: 2842
		private static readonly TimeSpan DefaultCoefficient = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000B1B RID: 2843
		private static readonly TimeSpan DefaultMaxDelay = TimeSpan.FromSeconds(30.0);
	}
}
