using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.ApplicationId
{
	// Token: 0x020000C8 RID: 200
	internal class ProfileServiceWrapper : IDisposable
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x00017BBA File Offset: 0x00015DBA
		internal ProfileServiceWrapper()
		{
			this.FailedRequestsManager = new FailedRequestsManager();
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00017BD8 File Offset: 0x00015DD8
		internal ProfileServiceWrapper(TimeSpan failedRequestRetryWaitTime)
		{
			this.FailedRequestsManager = new FailedRequestsManager(failedRequestRetryWaitTime);
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00017BF7 File Offset: 0x00015DF7
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x00017BFF File Offset: 0x00015DFF
		public string ProfileQueryEndpoint { get; set; }

		// Token: 0x0600068C RID: 1676 RVA: 0x00017C08 File Offset: 0x00015E08
		public async Task<string> FetchApplicationIdAsync(string instrumentationKey)
		{
			if (this.FailedRequestsManager.CanRetry(instrumentationKey))
			{
				try
				{
					return await this.SendRequestAsync(instrumentationKey).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					this.FailedRequestsManager.RegisterFetchFailure(instrumentationKey, ex);
					return null;
				}
			}
			return null;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00017C55 File Offset: 0x00015E55
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00017C64 File Offset: 0x00015E64
		public void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.httpClient.Dispose();
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00017C74 File Offset: 0x00015E74
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Implementation expects lower case. GUID is safe for lower case.")]
		internal virtual Task<HttpResponseMessage> GetAsync(string instrumentationKey)
		{
			Uri applicationIdEndPointUri = this.GetApplicationIdEndPointUri(instrumentationKey.ToLowerInvariant());
			return this.httpClient.GetAsync(applicationIdEndPointUri);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00017C9C File Offset: 0x00015E9C
		private async Task<string> SendRequestAsync(string instrumentationKey)
		{
			string text;
			try
			{
				SdkInternalOperationsMonitor.Enter();
				HttpResponseMessage httpResponseMessage = await this.GetAsync(instrumentationKey).ConfigureAwait(false);
				if (httpResponseMessage.IsSuccessStatusCode)
				{
					text = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
				}
				else
				{
					this.FailedRequestsManager.RegisterFetchFailure(instrumentationKey, httpResponseMessage.StatusCode);
					text = null;
				}
			}
			finally
			{
				SdkInternalOperationsMonitor.Exit();
			}
			return text;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00017CE9 File Offset: 0x00015EE9
		private Uri GetApplicationIdEndPointUri(string instrumentationKey)
		{
			return new Uri(string.Format(CultureInfo.InvariantCulture, this.ProfileQueryEndpoint ?? "https://dc.services.visualstudio.com/api/profiles/{0}/appId", new object[] { instrumentationKey }));
		}

		// Token: 0x040002A2 RID: 674
		internal readonly FailedRequestsManager FailedRequestsManager;

		// Token: 0x040002A3 RID: 675
		private readonly HttpClient httpClient = new HttpClient();
	}
}
