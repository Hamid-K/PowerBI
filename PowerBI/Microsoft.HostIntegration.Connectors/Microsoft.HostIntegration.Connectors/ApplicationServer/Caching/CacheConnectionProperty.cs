using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200029A RID: 666
	internal class CacheConnectionProperty
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00049B11 File Offset: 0x00047D11
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x00049B19 File Offset: 0x00047D19
		public DateTime AcsValidTill { get; set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x00049B22 File Offset: 0x00047D22
		// (set) Token: 0x06001843 RID: 6211 RVA: 0x00049B2A File Offset: 0x00047D2A
		public bool IsRedirected { get; internal set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x00049B33 File Offset: 0x00047D33
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x00049B3B File Offset: 0x00047D3B
		public string CacheName { get; internal set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00049B44 File Offset: 0x00047D44
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x00049B4C File Offset: 0x00047D4C
		public bool TerminateCacheConnection
		{
			get
			{
				return this.terminateCacheConnection;
			}
			set
			{
				this.terminateCacheConnection = this.terminateCacheConnection || value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00049B60 File Offset: 0x00047D60
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x00049B68 File Offset: 0x00047D68
		public ErrStatus TerminationReason
		{
			get
			{
				return this.terminationReason;
			}
			set
			{
				if (this.terminationReason == ErrStatus.UNINITIALIZED_ERROR)
				{
					this.terminationReason = value;
				}
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x00049B79 File Offset: 0x00047D79
		// (set) Token: 0x0600184B RID: 6219 RVA: 0x00049B81 File Offset: 0x00047D81
		public string CacheEndpoint { get; private set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x00049B8A File Offset: 0x00047D8A
		// (set) Token: 0x0600184D RID: 6221 RVA: 0x00049B92 File Offset: 0x00047D92
		public bool IsRoutingClientConnection { get; private set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600184E RID: 6222 RVA: 0x00049B9B File Offset: 0x00047D9B
		// (set) Token: 0x0600184F RID: 6223 RVA: 0x00049BA3 File Offset: 0x00047DA3
		public string AuthenticationToken { get; internal set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x00049BAC File Offset: 0x00047DAC
		// (set) Token: 0x06001851 RID: 6225 RVA: 0x00049BB4 File Offset: 0x00047DB4
		public ClientLocationType? ClientRequestLocation { get; private set; }

		// Token: 0x06001852 RID: 6226 RVA: 0x00049BBD File Offset: 0x00047DBD
		public CacheConnectionProperty(string cacheEndpoint, string authenticationToken, ClientLocationType? clientLocation)
		{
			this.terminationReason = ErrStatus.UNINITIALIZED_ERROR;
			this.CacheEndpoint = cacheEndpoint;
			this.AuthenticationToken = authenticationToken;
			this.ClientRequestLocation = clientLocation;
			this.IsRoutingClientConnection = false;
			this.AcsValidTill = DateTime.MinValue;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00049BF3 File Offset: 0x00047DF3
		public void InitCloudChannelRoutingProperties(bool isRoutingClient)
		{
			this.IsRoutingClientConnection = isRoutingClient;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00049BFC File Offset: 0x00047DFC
		public void InitCloudChannelRoutingProperties(bool isRoutingClient, string vipEndpoint)
		{
			this.IsRoutingClientConnection = isRoutingClient;
			this.CacheEndpoint = vipEndpoint;
		}

		// Token: 0x04000D5D RID: 3421
		private bool terminateCacheConnection;

		// Token: 0x04000D5E RID: 3422
		private ErrStatus terminationReason;
	}
}
