using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000062 RID: 98
	public class RetryOptions
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00009F15 File Offset: 0x00008115
		internal RetryOptions()
			: this(ClientOptions.Default.Retry)
		{
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00009F28 File Offset: 0x00008128
		[NullableContext(2)]
		internal RetryOptions(RetryOptions retryOptions)
		{
			if (retryOptions != null)
			{
				this.MaxRetries = retryOptions.MaxRetries;
				this.Delay = retryOptions.Delay;
				this.MaxDelay = retryOptions.MaxDelay;
				this.Mode = retryOptions.Mode;
				this.NetworkTimeout = retryOptions.NetworkTimeout;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00009FC4 File Offset: 0x000081C4
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00009FCC File Offset: 0x000081CC
		public int MaxRetries { get; set; } = 3;

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00009FD5 File Offset: 0x000081D5
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00009FDD File Offset: 0x000081DD
		public TimeSpan Delay { get; set; } = TimeSpan.FromSeconds(0.8);

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00009FE6 File Offset: 0x000081E6
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00009FEE File Offset: 0x000081EE
		public TimeSpan MaxDelay { get; set; } = TimeSpan.FromMinutes(1.0);

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00009FF7 File Offset: 0x000081F7
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00009FFF File Offset: 0x000081FF
		public RetryMode Mode { get; set; } = RetryMode.Exponential;

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000A008 File Offset: 0x00008208
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000A010 File Offset: 0x00008210
		public TimeSpan NetworkTimeout { get; set; } = TimeSpan.FromSeconds(100.0);
	}
}
