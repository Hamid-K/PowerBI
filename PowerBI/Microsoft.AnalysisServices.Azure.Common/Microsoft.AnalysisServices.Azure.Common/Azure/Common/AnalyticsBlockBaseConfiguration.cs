using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000035 RID: 53
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class AnalyticsBlockBaseConfiguration : ConfigurationClass
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000E887 File Offset: 0x0000CA87
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000E88F File Offset: 0x0000CA8F
		[ConfigurationProperty]
		public TimeSpan DefaultDrainWorkTicketTimeout { get; set; }

		// Token: 0x0600034F RID: 847 RVA: 0x0000E898 File Offset: 0x0000CA98
		public override string ToString()
		{
			return base.ToXml();
		}
	}
}
