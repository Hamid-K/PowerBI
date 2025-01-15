using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000093 RID: 147
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.None)]
	[Serializable]
	public sealed class AnalyticsSampleModelCollectionConfiguration : ConfigurationClass
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00010D03 File Offset: 0x0000EF03
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00010D0B File Offset: 0x0000EF0B
		[ConfigurationProperty]
		public ConfigurationCollection<SampleModelConfiguration> SampleModelCollection { get; set; }

		// Token: 0x06000524 RID: 1316 RVA: 0x0000E898 File Offset: 0x0000CA98
		public override string ToString()
		{
			return base.ToXml();
		}
	}
}
