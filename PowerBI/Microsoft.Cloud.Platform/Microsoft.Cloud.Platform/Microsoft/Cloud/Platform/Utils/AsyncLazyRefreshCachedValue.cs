using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200014B RID: 331
	public sealed class AsyncLazyRefreshCachedValue<TValue> where TValue : class
	{
		// Token: 0x0600089E RID: 2206 RVA: 0x0001E0CC File Offset: 0x0001C2CC
		public AsyncLazyRefreshCachedValue(Func<Task<TValue>> valueFactory, TimeSpan minExpirationTime, TimeSpan maxExpirationTime, ITraceSource tracer, string callerPrefix)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Task<TValue>>>(valueFactory, "valueFactory");
			ExtendedDiagnostics.EnsureOperation(maxExpirationTime >= minExpirationTime, "maxExpirationTime should be >= minExpirationTime");
			this.m_valueFactory = valueFactory;
			this.m_expirationTime = minExpirationTime + new TimeSpan((long)(AsyncLazyRefreshCachedValue<TValue>.s_randomValuesGenerator.NextDouble() * (double)(maxExpirationTime.Ticks - minExpirationTime.Ticks)));
			this.m_traceSource = tracer;
			this.m_callerPrefix = (string.IsNullOrEmpty(callerPrefix) ? "AsyncLazyRefreshCache" : callerPrefix);
			this.m_valueCachedTime = DateTime.UtcNow;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001E168 File Offset: 0x0001C368
		public async Task<TValue> GetValueAsync()
		{
			if (this.m_valueCachedTime.Add(this.m_expirationTime) <= DateTime.UtcNow || Volatile.Read<TValue>(ref this.m_currentValue) == null)
			{
				ITraceSource traceSource = this.m_traceSource;
				if (traceSource != null)
				{
					traceSource.TraceInformation("{0} - cache invalidated or empty, refreshing".FormatWithInvariantCulture(new object[] { this.m_callerPrefix }));
				}
				if (Volatile.Read<TValue>(ref this.m_currentValue) == null)
				{
					await this.RefreshValueTask();
				}
				else if (Interlocked.Increment(ref this.m_singleRefreshCounter) == 1)
				{
					try
					{
						await this.RefreshValueTask();
					}
					catch (Exception ex)
					{
						ITraceSource traceSource2 = this.m_traceSource;
						if (traceSource2 != null)
						{
							traceSource2.TraceError("{0} - stale copy cache refresh completed with failure {1}".FormatWithInvariantCulture(new object[] { this.m_callerPrefix, ex.Message }));
						}
					}
					finally
					{
						Interlocked.Exchange(ref this.m_singleRefreshCounter, 0);
					}
				}
			}
			return Volatile.Read<TValue>(ref this.m_currentValue);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		private Task RefreshValueTask()
		{
			Task task = this.m_singleRefreshTask;
			if (task != null)
			{
				ITraceSource traceSource = this.m_traceSource;
				if (traceSource != null)
				{
					traceSource.TraceInformation("{0} - Refresh task already in progress".FormatWithInvariantCulture(new object[] { this.m_callerPrefix }));
				}
				return task;
			}
			object singleRefreshTaskLock = this.m_singleRefreshTaskLock;
			lock (singleRefreshTaskLock)
			{
				task = this.m_singleRefreshTask;
				if (task == null)
				{
					if (Volatile.Read<TValue>(ref this.m_currentValue) != null && this.m_valueCachedTime.Add(this.m_expirationTime) > DateTime.UtcNow)
					{
						return Task.FromResult<bool>(true);
					}
					task = (this.m_singleRefreshTask = Task.Run(async delegate
					{
						try
						{
							ITraceSource traceSource2 = this.m_traceSource;
							if (traceSource2 != null)
							{
								traceSource2.TraceInformation("{0} - cache refresh started".FormatWithInvariantCulture(new object[] { this.m_callerPrefix }));
							}
							TValue tvalue = await this.m_valueFactory();
							this.m_currentValue = tvalue;
							this.m_valueCachedTime = DateTime.UtcNow;
							ITraceSource traceSource3 = this.m_traceSource;
							if (traceSource3 != null)
							{
								traceSource3.TraceInformation("{0} - cache refresh complete".FormatWithInvariantCulture(new object[] { this.m_callerPrefix }));
							}
						}
						catch (Exception ex)
						{
							ITraceSource traceSource4 = this.m_traceSource;
							if (traceSource4 != null)
							{
								traceSource4.TraceError("{0} - cache refresh completed with failure {1}".FormatWithInvariantCulture(new object[] { this.m_callerPrefix, ex.Message }));
							}
							throw;
						}
						finally
						{
							this.m_singleRefreshTask = null;
						}
					}));
				}
			}
			return task;
		}

		// Token: 0x0400033E RID: 830
		private static readonly Random s_randomValuesGenerator = new Random();

		// Token: 0x0400033F RID: 831
		private readonly object m_singleRefreshTaskLock = new object();

		// Token: 0x04000340 RID: 832
		private readonly Func<Task<TValue>> m_valueFactory;

		// Token: 0x04000341 RID: 833
		private readonly TimeSpan m_expirationTime;

		// Token: 0x04000342 RID: 834
		private readonly ITraceSource m_traceSource;

		// Token: 0x04000343 RID: 835
		private readonly string m_callerPrefix;

		// Token: 0x04000344 RID: 836
		private Task m_singleRefreshTask;

		// Token: 0x04000345 RID: 837
		private int m_singleRefreshCounter;

		// Token: 0x04000346 RID: 838
		private DateTime m_valueCachedTime;

		// Token: 0x04000347 RID: 839
		private TValue m_currentValue;
	}
}
