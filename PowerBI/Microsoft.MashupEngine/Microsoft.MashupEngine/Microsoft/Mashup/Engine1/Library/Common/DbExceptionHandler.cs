using System;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001084 RID: 4228
	internal class DbExceptionHandler
	{
		// Token: 0x06006EB8 RID: 28344 RVA: 0x0017E32C File Offset: 0x0017C52C
		public DbExceptionHandler(IEngineHost host, Tracer tracer, string dataSourceName, IResource resource, Func<DbException, DbExceptionInfo> getDbExceptionInfo, bool handleUnexpected)
		{
			this.engineHost = host;
			this.tracer = tracer;
			this.evaluationConstants = host.GetEvaluationConstants();
			this.dataSourceName = dataSourceName;
			this.resource = resource;
			this.getDbExceptionInfo = getDbExceptionInfo;
			this.handleUnexpected = handleUnexpected;
		}

		// Token: 0x06006EB9 RID: 28345 RVA: 0x0017E378 File Offset: 0x0017C578
		public T InvokeWithRetry<T>(Func<T> func)
		{
			return this.Invoke<T>(3, func);
		}

		// Token: 0x06006EBA RID: 28346 RVA: 0x0017E382 File Offset: 0x0017C582
		public T InvokeWithoutRetry<T>(Func<T> func)
		{
			return this.Invoke<T>(0, func);
		}

		// Token: 0x06006EBB RID: 28347 RVA: 0x0017E38C File Offset: 0x0017C58C
		public void InvokeWithoutRetry(Action action)
		{
			this.InvokeWithoutRetry<int>(delegate
			{
				action();
				return 0;
			});
		}

		// Token: 0x06006EBC RID: 28348 RVA: 0x0017E3BC File Offset: 0x0017C5BC
		protected T Invoke<T>(int maxRetry, Func<T> action)
		{
			T t;
			try
			{
				t = this.RunWithRetryGuard<T>(maxRetry, action);
			}
			catch (Exception ex)
			{
				this.TraceRetriesFailed(maxRetry, ex);
				if (ex is DbException)
				{
					throw this.getDbExceptionInfo(ex as DbException).GetEngineException(this.dataSourceName, this.resource);
				}
				if (!SafeExceptions.IsSafeException(ex) || ex is RuntimeException || !this.handleUnexpected)
				{
					throw;
				}
				throw DataSourceException.NewDataSourceError(this.engineHost, ex.Message, this.resource, null, ex);
			}
			return t;
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x0017E450 File Offset: 0x0017C650
		private T RunWithRetryGuard<T>(int maxRetryAttempts, Func<T> action)
		{
			int num = maxRetryAttempts + 1;
			for (int i = 0; i < num; i++)
			{
				try
				{
					T t = action();
					this.TraceRetriesSucceeded(maxRetryAttempts, i);
					return t;
				}
				catch (DbException ex) when (this.ShouldRetry(maxRetryAttempts, i, ex))
				{
				}
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x0017E4C0 File Offset: 0x0017C6C0
		private void TraceRetriesFailed(int maxRetry, Exception e)
		{
			using (IHostTrace hostTrace = this.tracer.CreateTrace("RunWithRetry/Failure", TraceEventType.Error))
			{
				hostTrace.Add("MaxAttempt", maxRetry, false);
				if (!(e is DbException))
				{
					hostTrace.Add(e, true);
				}
			}
		}

		// Token: 0x06006EBF RID: 28351 RVA: 0x0017E520 File Offset: 0x0017C720
		private void TraceRetriesSucceeded(int maxRetryAttempts, int attempt)
		{
			if (attempt > 0)
			{
				using (IHostTrace hostTrace = this.tracer.CreateTrace("RunWithRetry/Success", TraceEventType.Information))
				{
					hostTrace.Add("Attempt", attempt, false);
					hostTrace.Add("MaxAttempt", maxRetryAttempts, false);
				}
			}
		}

		// Token: 0x06006EC0 RID: 28352 RVA: 0x0017E584 File Offset: 0x0017C784
		private bool ShouldRetry(int maxRetryAttempts, int attempt, DbException e)
		{
			bool flag;
			using (IHostTrace hostTrace = this.tracer.CreateTrace("RunWithRetry/Exception", TraceEventType.Information))
			{
				DbExceptionInfo dbExceptionInfo = this.getDbExceptionInfo(e);
				flag = dbExceptionInfo.IsRetryable(this.tracer, this.resource, attempt + 1) && attempt < maxRetryAttempts;
				hostTrace.AddResource(this.resource);
				hostTrace.Add("Attempt", attempt, false);
				hostTrace.Add("MaxAttempt", maxRetryAttempts, false);
				dbExceptionInfo.TraceDetails(hostTrace);
				hostTrace.Add(e, flag ? TraceEventType.Warning : TraceEventType.Error, true);
			}
			return flag;
		}

		// Token: 0x04003D6A RID: 15722
		private readonly IEngineHost engineHost;

		// Token: 0x04003D6B RID: 15723
		private readonly Tracer tracer;

		// Token: 0x04003D6C RID: 15724
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x04003D6D RID: 15725
		private readonly string dataSourceName;

		// Token: 0x04003D6E RID: 15726
		private readonly Func<DbException, DbExceptionInfo> getDbExceptionInfo;

		// Token: 0x04003D6F RID: 15727
		private readonly bool handleUnexpected;

		// Token: 0x04003D70 RID: 15728
		private readonly IResource resource;
	}
}
