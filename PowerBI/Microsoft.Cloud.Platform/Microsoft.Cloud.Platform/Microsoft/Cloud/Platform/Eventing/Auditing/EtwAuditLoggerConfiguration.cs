using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.Eventing.Auditing
{
	// Token: 0x020003E8 RID: 1000
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.None)]
	[Serializable]
	public sealed class EtwAuditLoggerConfiguration : ConfigurationClass
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x000735AE File Offset: 0x000717AE
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x000735B6 File Offset: 0x000717B6
		[ConfigurationProperty]
		public string AuditLoggerName { get; set; }
	}
}
