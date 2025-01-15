using System;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x02000454 RID: 1108
	[Serializable]
	public sealed class ProviderConfiguration : ConfigurationClass
	{
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x0007E303 File Offset: 0x0007C503
		// (set) Token: 0x0600228B RID: 8843 RVA: 0x0007E30B File Offset: 0x0007C50B
		[ConfigurationProperty]
		public ConfigurationCollection<EndpointConfiguration> EndpointsConfiguration { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x0007E314 File Offset: 0x0007C514
		// (set) Token: 0x0600228D RID: 8845 RVA: 0x0007E31C File Offset: 0x0007C51C
		[ConfigurationProperty]
		public int MaxConcurrentCalls { get; set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x0007E325 File Offset: 0x0007C525
		// (set) Token: 0x0600228F RID: 8847 RVA: 0x0007E32D File Offset: 0x0007C52D
		[ConfigurationProperty]
		public int MaxConcurrentSessions { get; set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x0007E336 File Offset: 0x0007C536
		// (set) Token: 0x06002291 RID: 8849 RVA: 0x0007E33E File Offset: 0x0007C53E
		[ConfigurationProperty]
		public NonContractualExceptionBehavior CrashServerOnNonContractualException { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x0007E347 File Offset: 0x0007C547
		// (set) Token: 0x06002293 RID: 8851 RVA: 0x0007E34F File Offset: 0x0007C54F
		[ConfigurationProperty]
		public bool DisableDefaultErrorHandler { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x0007E358 File Offset: 0x0007C558
		// (set) Token: 0x06002295 RID: 8853 RVA: 0x0007E360 File Offset: 0x0007C560
		[ConfigurationProperty]
		public string ServiceCertificateName { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x0007E369 File Offset: 0x0007C569
		// (set) Token: 0x06002297 RID: 8855 RVA: 0x0007E371 File Offset: 0x0007C571
		[ConfigurationProperty]
		public long RequestInitializationTimeoutInSeconds { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x0007E37A File Offset: 0x0007C57A
		// (set) Token: 0x06002299 RID: 8857 RVA: 0x0007E382 File Offset: 0x0007C582
		[ConfigurationProperty]
		public int MaxPendingAccepts { get; set; }

		// Token: 0x0600229A RID: 8858 RVA: 0x0007E38B File Offset: 0x0007C58B
		public ProviderConfiguration()
		{
			this.MaxConcurrentCalls = 512;
			this.MaxConcurrentSessions = -1;
			this.CrashServerOnNonContractualException = NonContractualExceptionBehavior.CrashOnNonMonitoredExceptions;
		}
	}
}
