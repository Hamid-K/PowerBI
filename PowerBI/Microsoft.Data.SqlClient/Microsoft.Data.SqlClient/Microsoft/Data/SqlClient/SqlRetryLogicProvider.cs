using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000039 RID: 57
	internal class SqlRetryLogicProvider : SqlRetryLogicBaseProvider
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x0000EBD2 File Offset: 0x0000CDD2
		public SqlRetryLogicProvider(SqlRetryLogicBase retryLogic)
		{
			base.RetryLogic = retryLogic;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		private SqlRetryLogicBase GetRetryLogic()
		{
			SqlRetryLogicBase sqlRetryLogicBase;
			if (!this._retryLogicPool.TryTake(out sqlRetryLogicBase))
			{
				sqlRetryLogicBase = base.RetryLogic.Clone() as SqlRetryLogicBase;
			}
			else if (sqlRetryLogicBase != null)
			{
				sqlRetryLogicBase.Reset();
			}
			return sqlRetryLogicBase;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000EC25 File Offset: 0x0000CE25
		private void RetryLogicPoolAdd(SqlRetryLogicBase retryLogic)
		{
			if (retryLogic != null)
			{
				this._retryLogicPool.Add(retryLogic);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000EC38 File Offset: 0x0000CE38
		public override TResult Execute<TResult>(object sender, Func<TResult> function)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			SqlRetryLogicBase sqlRetryLogicBase = null;
			List<Exception> list = new List<Exception>();
			TResult tresult2;
			for (;;)
			{
				try
				{
					TResult tresult = function();
					this.RetryLogicPoolAdd(sqlRetryLogicBase);
					tresult2 = tresult;
				}
				catch (Exception ex)
				{
					if (!base.RetryLogic.RetryCondition(sender) || !base.RetryLogic.TransientPredicate(ex))
					{
						this.RetryLogicPoolAdd(sqlRetryLogicBase);
						throw;
					}
					if (sqlRetryLogicBase == null)
					{
						sqlRetryLogicBase = this.GetRetryLogic();
					}
					SqlClientEventSource.Log.TryTraceEvent<string, int>("<sc.{0}.Execute<TResult>|INFO> Found an action eligible for the retry policy (retried attempts = {1}).", "SqlRetryLogicProvider", sqlRetryLogicBase.Current);
					list.Add(ex);
					TimeSpan timeSpan;
					if (sqlRetryLogicBase.TryNextInterval(out timeSpan))
					{
						this.ApplyRetryingEvent(sender, sqlRetryLogicBase, timeSpan, list, ex);
						Thread.Sleep(timeSpan);
						continue;
					}
					throw this.CreateException(list, sqlRetryLogicBase, false);
				}
				break;
			}
			return tresult2;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000ED08 File Offset: 0x0000CF08
		public override async Task<TResult> ExecuteAsync<TResult>(object sender, Func<Task<TResult>> function, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (function == null)
			{
				throw SqlReliabilityUtil.ArgumentNull("function");
			}
			SqlRetryLogicBase retryLogic = null;
			List<Exception> exceptions = new List<Exception>();
			object obj;
			for (;;)
			{
				int num = 0;
				try
				{
					TResult tresult = await function().ConfigureAwait(false);
					TResult tresult2 = tresult;
					this.RetryLogicPoolAdd(retryLogic);
					return tresult2;
				}
				catch (Exception obj)
				{
					num = 1;
				}
				if (num != 1)
				{
					goto IL_022E;
				}
				Exception ex = (Exception)obj;
				if (!base.RetryLogic.RetryCondition(sender) || !base.RetryLogic.TransientPredicate(ex))
				{
					goto IL_0208;
				}
				if (retryLogic == null)
				{
					retryLogic = this.GetRetryLogic();
				}
				SqlClientEventSource.Log.TryTraceEvent<string, int>("<sc.{0}.ExecuteAsync<TResult>|INFO> Found an action eligible for the retry policy (retried attempts = {1}).", "SqlRetryLogicProvider", retryLogic.Current);
				exceptions.Add(ex);
				TimeSpan timeSpan;
				if (!retryLogic.TryNextInterval(out timeSpan))
				{
					break;
				}
				this.ApplyRetryingEvent(sender, retryLogic, timeSpan, exceptions, ex);
				await Task.Delay(timeSpan, cancellationToken).ConfigureAwait(false);
			}
			throw this.CreateException(exceptions, retryLogic, false);
			IL_0208:
			this.RetryLogicPoolAdd(retryLogic);
			Exception ex2 = obj as Exception;
			if (ex2 == null)
			{
				throw obj;
			}
			ExceptionDispatchInfo.Capture(ex2).Throw();
			IL_022E:
			retryLogic = null;
			exceptions = null;
			TResult tresult3;
			return tresult3;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000ED64 File Offset: 0x0000CF64
		public override async Task ExecuteAsync(object sender, Func<Task> function, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			SqlRetryLogicBase retryLogic = null;
			List<Exception> exceptions = new List<Exception>();
			object obj;
			for (;;)
			{
				int num = 0;
				try
				{
					await function().ConfigureAwait(false);
					this.RetryLogicPoolAdd(retryLogic);
				}
				catch (Exception obj)
				{
					num = 1;
				}
				if (num != 1)
				{
					goto IL_0241;
				}
				Exception ex = (Exception)obj;
				if (!base.RetryLogic.RetryCondition(sender) || !base.RetryLogic.TransientPredicate(ex))
				{
					goto IL_0211;
				}
				if (retryLogic == null)
				{
					retryLogic = this.GetRetryLogic();
				}
				SqlClientEventSource.Log.TryTraceEvent<string, int>("<sc.{0}.ExecuteAsync|INFO> Found an action eligible for the retry policy (retried attempts = {1}).", "SqlRetryLogicProvider", retryLogic.Current);
				exceptions.Add(ex);
				TimeSpan timeSpan;
				if (!retryLogic.TryNextInterval(out timeSpan))
				{
					break;
				}
				this.ApplyRetryingEvent(sender, retryLogic, timeSpan, exceptions, ex);
				await Task.Delay(timeSpan, cancellationToken).ConfigureAwait(false);
			}
			throw this.CreateException(exceptions, retryLogic, false);
			IL_0211:
			this.RetryLogicPoolAdd(retryLogic);
			Exception ex2 = obj as Exception;
			if (ex2 == null)
			{
				throw obj;
			}
			ExceptionDispatchInfo.Capture(ex2).Throw();
			IL_0241:
			obj = null;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		private Exception CreateException(IList<Exception> exceptions, SqlRetryLogicBase retryLogic, bool manualCancellation = false)
		{
			AggregateException ex = SqlReliabilityUtil.ConfigurableRetryFail(exceptions, retryLogic, manualCancellation);
			if (!manualCancellation)
			{
				SqlClientEventSource.Log.TryTraceEvent<string, string, int>("<sc.{0}.{1}|ERR|THROW> Exiting retry scope (exceeded the max allowed attempts = {2}).", "SqlRetryLogicProvider", MethodBase.GetCurrentMethod().Name, retryLogic.NumberOfTries);
			}
			this._retryLogicPool.Add(retryLogic);
			return ex;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000EE0C File Offset: 0x0000D00C
		private void ApplyRetryingEvent(object sender, SqlRetryLogicBase retryLogic, TimeSpan intervalTime, List<Exception> exceptions, Exception lastException)
		{
			string name = MethodBase.GetCurrentMethod().Name;
			if (base.Retrying != null)
			{
				SqlRetryingEventArgs sqlRetryingEventArgs = new SqlRetryingEventArgs(retryLogic.Current, intervalTime, exceptions);
				SqlClientEventSource.Log.TryTraceEvent<string, string>("<sc.{0}.{1}|INFO> Running the retrying event.", "SqlRetryLogicProvider", name);
				EventHandler<SqlRetryingEventArgs> retrying = base.Retrying;
				if (retrying != null)
				{
					retrying(sender, sqlRetryingEventArgs);
				}
				if (sqlRetryingEventArgs.Cancel)
				{
					SqlClientEventSource.Log.TryTraceEvent<string, string, int>("<sc.{0}.{1}|INFO> Retry attempt cancelled (current retry number = {2}).", "SqlRetryLogicProvider", name, retryLogic.Current);
					throw this.CreateException(exceptions, retryLogic, true);
				}
			}
			SqlClientEventSource.Log.TryTraceEvent<string, string, TimeSpan, int>("<sc.{0}.{1}|INFO> Wait '{2}' and run the action for retry number {3}.", "SqlRetryLogicProvider", name, intervalTime, retryLogic.Current);
		}

		// Token: 0x040000C7 RID: 199
		private const string TypeName = "SqlRetryLogicProvider";

		// Token: 0x040000C8 RID: 200
		private readonly ConcurrentBag<SqlRetryLogicBase> _retryLogicPool = new ConcurrentBag<SqlRetryLogicBase>();
	}
}
