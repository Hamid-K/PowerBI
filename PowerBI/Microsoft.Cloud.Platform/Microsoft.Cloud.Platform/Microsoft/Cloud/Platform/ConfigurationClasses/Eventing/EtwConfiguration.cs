using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Eventing
{
	// Token: 0x02000449 RID: 1097
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class EtwConfiguration : ConfigurationClass
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x0007DDC5 File Offset: 0x0007BFC5
		// (set) Token: 0x06002206 RID: 8710 RVA: 0x0007DDCD File Offset: 0x0007BFCD
		[ConfigurationProperty]
		public int FlushTimerPeriodInSeconds { get; set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x0007DDD6 File Offset: 0x0007BFD6
		// (set) Token: 0x06002208 RID: 8712 RVA: 0x0007DDDE File Offset: 0x0007BFDE
		[ConfigurationProperty]
		public int MaxEventsSessionFileSizeInMb { get; set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x0007DDE7 File Offset: 0x0007BFE7
		// (set) Token: 0x0600220A RID: 8714 RVA: 0x0007DDEF File Offset: 0x0007BFEF
		[ConfigurationProperty]
		public int EventsSessionBufferSizeInKb { get; set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x0007DDF8 File Offset: 0x0007BFF8
		// (set) Token: 0x0600220C RID: 8716 RVA: 0x0007DE00 File Offset: 0x0007C000
		[ConfigurationProperty]
		public int EventsSessionMaxBuffers { get; set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x0007DE09 File Offset: 0x0007C009
		// (set) Token: 0x0600220E RID: 8718 RVA: 0x0007DE11 File Offset: 0x0007C011
		[ConfigurationProperty]
		public string EventsSessionNamePrefix { get; set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x0007DE1A File Offset: 0x0007C01A
		// (set) Token: 0x06002210 RID: 8720 RVA: 0x0007DE22 File Offset: 0x0007C022
		[ConfigurationProperty]
		public bool ShouldCloseSessionOnShutdown { get; set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x0007DE2B File Offset: 0x0007C02B
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x0007DE33 File Offset: 0x0007C033
		[ConfigurationProperty]
		public string ProvidersManifestSessionName { get; set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x0007DE3C File Offset: 0x0007C03C
		// (set) Token: 0x06002214 RID: 8724 RVA: 0x0007DE44 File Offset: 0x0007C044
		[ConfigurationProperty]
		public int MaxProvidersManifestSessionFileSizeInMb { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x0007DE4D File Offset: 0x0007C04D
		// (set) Token: 0x06002216 RID: 8726 RVA: 0x0007DE55 File Offset: 0x0007C055
		[ConfigurationProperty]
		public string EventFilesSourceDirectoryPath { get; set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x0007DE5E File Offset: 0x0007C05E
		// (set) Token: 0x06002218 RID: 8728 RVA: 0x0007DE66 File Offset: 0x0007C066
		[ConfigurationProperty]
		public string EventFilesTargetDirectoryPath { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x0007DE6F File Offset: 0x0007C06F
		// (set) Token: 0x0600221A RID: 8730 RVA: 0x0007DE77 File Offset: 0x0007C077
		[ConfigurationProperty]
		public string ProvidersManifestSessionDirectoryPath { get; set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x0007DE80 File Offset: 0x0007C080
		// (set) Token: 0x0600221C RID: 8732 RVA: 0x0007DE88 File Offset: 0x0007C088
		[ConfigurationProperty]
		public int MaxTargetDirectorySizeInMb { get; set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x0007DE91 File Offset: 0x0007C091
		// (set) Token: 0x0600221E RID: 8734 RVA: 0x0007DE99 File Offset: 0x0007C099
		[ConfigurationProperty]
		public long ReduceTargetDirectorySizeByInMb { get; set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x0007DEA2 File Offset: 0x0007C0A2
		// (set) Token: 0x06002220 RID: 8736 RVA: 0x0007DEAA File Offset: 0x0007C0AA
		[ConfigurationProperty]
		public bool EnableLocalDirectoriesProcessing { get; set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x0007DEB3 File Offset: 0x0007C0B3
		// (set) Token: 0x06002222 RID: 8738 RVA: 0x0007DEBB File Offset: 0x0007C0BB
		[ConfigurationProperty]
		public int CollectEventFileMaxRetries { get; set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x0007DEC4 File Offset: 0x0007C0C4
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x0007DECC File Offset: 0x0007C0CC
		[ConfigurationProperty]
		public Guid Epoch { get; set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x0007DED5 File Offset: 0x0007C0D5
		// (set) Token: 0x06002226 RID: 8742 RVA: 0x0007DEDD File Offset: 0x0007C0DD
		[ConfigurationProperty]
		public bool UseManifestSession { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x0007DEE6 File Offset: 0x0007C0E6
		// (set) Token: 0x06002228 RID: 8744 RVA: 0x0007DEEE File Offset: 0x0007C0EE
		[ConfigurationProperty]
		public bool DisableEventsKitOutput { get; set; }
	}
}
