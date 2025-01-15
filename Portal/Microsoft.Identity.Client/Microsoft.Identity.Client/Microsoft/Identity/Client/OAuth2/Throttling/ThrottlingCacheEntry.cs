using System;

namespace Microsoft.Identity.Client.OAuth2.Throttling
{
	// Token: 0x02000219 RID: 537
	internal class ThrottlingCacheEntry
	{
		// Token: 0x06001646 RID: 5702 RVA: 0x00049B50 File Offset: 0x00047D50
		public ThrottlingCacheEntry(MsalServiceException exception, TimeSpan lifetime)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Exception = exception;
			this.CreationTime = DateTimeOffset.UtcNow;
			this.ExpirationTime = this.CreationTime.Add(lifetime);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00049B99 File Offset: 0x00047D99
		public ThrottlingCacheEntry(MsalServiceException exception, DateTimeOffset creationTime, DateTimeOffset expirationTime)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Exception = exception;
			this.CreationTime = creationTime;
			this.ExpirationTime = expirationTime;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00049BC5 File Offset: 0x00047DC5
		public MsalServiceException Exception { get; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00049BCD File Offset: 0x00047DCD
		public DateTimeOffset CreationTime { get; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00049BD5 File Offset: 0x00047DD5
		public DateTimeOffset ExpirationTime { get; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00049BDD File Offset: 0x00047DDD
		public bool IsExpired
		{
			get
			{
				return this.ExpirationTime < DateTimeOffset.Now || this.CreationTime > DateTimeOffset.Now;
			}
		}
	}
}
