using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.ApplicationId
{
	// Token: 0x020000C5 RID: 197
	public sealed class ApplicationInsightsApplicationIdProvider : IApplicationIdProvider, IDisposable
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0001787B File Offset: 0x00015A7B
		public ApplicationInsightsApplicationIdProvider()
		{
			this.applicationIdProvider = new ProfileServiceWrapper();
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000178A4 File Offset: 0x00015AA4
		internal ApplicationInsightsApplicationIdProvider(ProfileServiceWrapper profileServiceWrapper)
		{
			this.applicationIdProvider = profileServiceWrapper;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x000178C9 File Offset: 0x00015AC9
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x000178D6 File Offset: 0x00015AD6
		public string ProfileQueryEndpoint
		{
			get
			{
				return this.applicationIdProvider.ProfileQueryEndpoint;
			}
			set
			{
				this.applicationIdProvider.ProfileQueryEndpoint = value;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000178E4 File Offset: 0x00015AE4
		public void Dispose()
		{
			this.applicationIdProvider.Dispose();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000178F1 File Offset: 0x00015AF1
		public bool TryGetApplicationId(string instrumentationKey, out string applicationId)
		{
			applicationId = null;
			if (string.IsNullOrEmpty(instrumentationKey))
			{
				return false;
			}
			if (this.knownApplicationIds.TryGetValue(instrumentationKey, out applicationId))
			{
				return true;
			}
			this.FetchApplicationId(instrumentationKey);
			return false;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00017919 File Offset: 0x00015B19
		internal bool IsFetchAppInProgress(string instrumentationKey)
		{
			return this.FetchTasks.ContainsKey(instrumentationKey);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00017928 File Offset: 0x00015B28
		private void FetchApplicationId(string instrumentationKey)
		{
			if (this.FetchTasks.TryAdd(instrumentationKey, true))
			{
				if (this.knownApplicationIds.Keys.Count >= 100)
				{
					this.knownApplicationIds.Clear();
				}
				Task.Run<string>(() => this.applicationIdProvider.FetchApplicationIdAsync(instrumentationKey)).ContinueWith(delegate(Task<string> applicationIdTask)
				{
					this.FormatAndAddToDictionary(instrumentationKey, applicationIdTask.Result);
					bool flag;
					this.FetchTasks.TryRemove(instrumentationKey, out flag);
				}).ConfigureAwait(false);
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000179A5 File Offset: 0x00015BA5
		private void FormatAndAddToDictionary(string instrumentationKey, string applicationId)
		{
			if (!string.IsNullOrEmpty(instrumentationKey) && !string.IsNullOrEmpty(applicationId))
			{
				this.knownApplicationIds.TryAdd(instrumentationKey, ApplicationIdHelper.ApplyFormatting(applicationId));
			}
		}

		// Token: 0x04000299 RID: 665
		internal ConcurrentDictionary<string, bool> FetchTasks = new ConcurrentDictionary<string, bool>();

		// Token: 0x0400029A RID: 666
		private const int MAXSIZE = 100;

		// Token: 0x0400029B RID: 667
		private readonly ProfileServiceWrapper applicationIdProvider;

		// Token: 0x0400029C RID: 668
		private ConcurrentDictionary<string, string> knownApplicationIds = new ConcurrentDictionary<string, string>();
	}
}
