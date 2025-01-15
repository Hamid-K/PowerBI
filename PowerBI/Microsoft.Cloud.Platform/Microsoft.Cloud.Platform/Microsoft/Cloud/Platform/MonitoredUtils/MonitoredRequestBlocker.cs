using System;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000141 RID: 321
	public sealed class MonitoredRequestBlocker<TKey> : IRequestBlocker<TKey>
	{
		// Token: 0x0600085A RID: 2138 RVA: 0x0001C2B0 File Offset: 0x0001A4B0
		public MonitoredRequestBlocker(string name, int limit, TimeSpan durationLimit, int maxKeys, TimeSpan blockingDuration, IEventsKitFactory eventsKitFactory)
		{
			this.m_limit = limit;
			this.m_durationLimit = durationLimit;
			this.m_protector = new MonitoredDenialOfServiceProtection<TKey>(name, durationLimit, maxKeys, limit, blockingDuration, maxKeys, MonitoredRequestBlocker<TKey>.EmptyTKeyList, MonitoredRequestBlocker<TKey>.EmptyTKeyList, eventsKitFactory);
			this.Tracer.TraceInformation("Request blocker {0} of type {1} was configured with <CountLimit={2}, DurationLimit={3}>", new object[]
			{
				name,
				typeof(TKey),
				limit,
				durationLimit
			});
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001C32A File Offset: 0x0001A52A
		private RequestBlockerTrace Tracer
		{
			get
			{
				return TraceSourceBase<RequestBlockerTrace>.Tracer;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001C331 File Offset: 0x0001A531
		public BlockingStatus<TKey> NotifyAndCheckBlockingStatus(TKey key)
		{
			return this.NotifyAndCheckBlockingStatus(key, null);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001C33C File Offset: 0x0001A53C
		public BlockingStatus<TKey> NotifyAndCheckBlockingStatus(TKey key, string clientIdentifier)
		{
			BlockingStatus<TKey> blockingStatus = this.m_protector.QueryBlockingStatus(key, ExtendedDateTime.UtcNow);
			if (blockingStatus.IsBlocked)
			{
				this.Tracer.TraceInformation("Request exceeded permitted threshold and has been blocked <Key={0}, CountLimit={1}, DurationLimit={2}, ClientIP={3}>", new object[]
				{
					key,
					this.m_limit,
					this.m_durationLimit,
					clientIdentifier.MarkAsInternal()
				});
				return blockingStatus;
			}
			this.Tracer.TraceInformation("Request within permitted threshold <Key={0}>", new object[] { key });
			return this.m_protector.ReportAndCheckRequestBlockingStatus(key, ExtendedDateTime.UtcNow, clientIdentifier);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001C3DC File Offset: 0x0001A5DC
		public void Block(TKey key)
		{
			this.m_protector.Block(key);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001C3EA File Offset: 0x0001A5EA
		public BlockingStatus<TKey> CheckBlockingStatus(TKey key)
		{
			return this.CheckBlockingStatus(key, null);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
		public BlockingStatus<TKey> CheckBlockingStatus(TKey key, string clientIdentifier)
		{
			BlockingStatus<TKey> blockingStatus = this.m_protector.QueryBlockingStatus(key, ExtendedDateTime.UtcNow);
			if (blockingStatus.IsBlocked)
			{
				this.Tracer.TraceInformation("Request exceeded permitted threshold and has been blocked <Key={0}, CountLimit={1}, DurationLimit={2}, ClientIP={3}>", new object[]
				{
					key,
					this.m_limit,
					this.m_durationLimit,
					clientIdentifier.MarkAsInternal()
				});
			}
			return blockingStatus;
		}

		// Token: 0x04000311 RID: 785
		private static readonly TKey[] EmptyTKeyList = new TKey[0];

		// Token: 0x04000312 RID: 786
		private readonly int m_limit;

		// Token: 0x04000313 RID: 787
		private readonly TimeSpan m_durationLimit;

		// Token: 0x04000314 RID: 788
		private readonly MonitoredDenialOfServiceProtection<TKey> m_protector;
	}
}
