using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000005 RID: 5
	internal sealed class DefaultSqlExecutionStrategy : IDbExecutionStrategy
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002368 File Offset: 0x00000568
		public bool RetriesOnFailure
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000236C File Offset: 0x0000056C
		public void Execute(Action operation)
		{
			if (operation == null)
			{
				throw new ArgumentNullException("operation");
			}
			this.Execute<object>(delegate
			{
				operation();
				return null;
			});
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023AC File Offset: 0x000005AC
		public TResult Execute<TResult>(Func<TResult> operation)
		{
			Check.NotNull<Func<TResult>>(operation, "operation");
			TResult tresult;
			try
			{
				tresult = operation();
			}
			catch (Exception ex)
			{
				if (DbExecutionStrategy.UnwrapAndHandleException<bool>(ex, new Func<Exception, bool>(SqlAzureRetriableExceptionDetector.ShouldRetryOn)))
				{
					throw new EntityException(Strings.TransientExceptionDetected, ex);
				}
				throw;
			}
			return tresult;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002404 File Offset: 0x00000604
		public Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken)
		{
			DefaultSqlExecutionStrategy.<>c__DisplayClass4_0 CS$<>8__locals1 = new DefaultSqlExecutionStrategy.<>c__DisplayClass4_0();
			CS$<>8__locals1.operation = operation;
			Check.NotNull<Func<Task>>(CS$<>8__locals1.operation, "operation");
			cancellationToken.ThrowIfCancellationRequested();
			return DefaultSqlExecutionStrategy.ExecuteAsyncImplementation<bool>(delegate
			{
				DefaultSqlExecutionStrategy.<>c__DisplayClass4_0.<<ExecuteAsync>b__0>d <<ExecuteAsync>b__0>d;
				<<ExecuteAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<ExecuteAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
				<<ExecuteAsync>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder<bool> <>t__builder = <<ExecuteAsync>b__0>d.<>t__builder;
				<>t__builder.Start<DefaultSqlExecutionStrategy.<>c__DisplayClass4_0.<<ExecuteAsync>b__0>d>(ref <<ExecuteAsync>b__0>d);
				return <<ExecuteAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000243A File Offset: 0x0000063A
		public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<Task<TResult>>>(operation, "operation");
			cancellationToken.ThrowIfCancellationRequested();
			return DefaultSqlExecutionStrategy.ExecuteAsyncImplementation<TResult>(operation);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002458 File Offset: 0x00000658
		private static async Task<TResult> ExecuteAsyncImplementation<TResult>(Func<Task<TResult>> func)
		{
			TResult tresult;
			try
			{
				tresult = await func().ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (DbExecutionStrategy.UnwrapAndHandleException<bool>(ex, new Func<Exception, bool>(SqlAzureRetriableExceptionDetector.ShouldRetryOn)))
				{
					throw new EntityException(Strings.TransientExceptionDetected, ex);
				}
				throw;
			}
			return tresult;
		}
	}
}
