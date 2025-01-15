using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F73 RID: 3955
	internal class AdobeAnalyticsOptions
	{
		// Token: 0x0600683A RID: 26682 RVA: 0x00166490 File Offset: 0x00164690
		public AdobeAnalyticsOptions(OptionsRecord options)
		{
			this.options = options;
		}

		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x0600683B RID: 26683 RVA: 0x001664B3 File Offset: 0x001646B3
		public bool HierarchicalNavigation
		{
			get
			{
				if (this.hierarchicalNavigation == null)
				{
					this.hierarchicalNavigation = new bool?(this.options.GetBool("HierarchicalNavigation", false));
				}
				return this.hierarchicalNavigation.Value;
			}
		}

		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x0600683C RID: 26684 RVA: 0x001664E9 File Offset: 0x001646E9
		public int RetryCount
		{
			get
			{
				this.EnsureInitialized<int?>(ref this.retryCount, "MaxRetryCount", new int?(120));
				return this.retryCount.Value;
			}
		}

		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x0600683D RID: 26685 RVA: 0x0016650E File Offset: 0x0016470E
		public TimeSpan RetryDelay
		{
			get
			{
				this.EnsureInitialized<TimeSpan?>(ref this.retryDelay, "RetryInterval", new TimeSpan?(this.defaultRetryDelay));
				return this.retryDelay.Value;
			}
		}

		// Token: 0x17001E27 RID: 7719
		// (get) Token: 0x0600683E RID: 26686 RVA: 0x00166537 File Offset: 0x00164737
		public string Implementation
		{
			get
			{
				this.EnsureInitialized<string>(ref this.implementation, "Implementation", null);
				return this.implementation;
			}
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x00166554 File Offset: 0x00164754
		private void EnsureInitialized<T>(ref T property, string optionsKey, T defaultValue)
		{
			if (property == null)
			{
				object obj;
				if (this.options.TryGetValue(optionsKey, out obj))
				{
					property = (T)((object)obj);
					return;
				}
				property = defaultValue;
			}
		}

		// Token: 0x04003955 RID: 14677
		private const bool defaultHierarchicalNavigation = false;

		// Token: 0x04003956 RID: 14678
		private const int defaultRetryCount = 120;

		// Token: 0x04003957 RID: 14679
		private const string defaultImplementation = null;

		// Token: 0x04003958 RID: 14680
		private readonly TimeSpan defaultRetryDelay = TimeSpan.FromSeconds(1.0);

		// Token: 0x04003959 RID: 14681
		private readonly OptionsRecord options;

		// Token: 0x0400395A RID: 14682
		private bool? hierarchicalNavigation;

		// Token: 0x0400395B RID: 14683
		private int? retryCount;

		// Token: 0x0400395C RID: 14684
		private TimeSpan? retryDelay;

		// Token: 0x0400395D RID: 14685
		private string implementation;
	}
}
