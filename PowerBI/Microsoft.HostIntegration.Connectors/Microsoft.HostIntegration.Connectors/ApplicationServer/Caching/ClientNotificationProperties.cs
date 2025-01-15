using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000CF RID: 207
	internal class ClientNotificationProperties : ConfigurationElement
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00017CBB File Offset: 0x00015EBB
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00017CCD File Offset: 0x00015ECD
		[ConfigurationProperty("pollInterval", IsRequired = false, DefaultValue = 300)]
		public int PollInterval
		{
			get
			{
				return (int)base["pollInterval"];
			}
			set
			{
				base["pollInterval"] = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00017CE0 File Offset: 0x00015EE0
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00017CF2 File Offset: 0x00015EF2
		[ConfigurationProperty("maxQueueLength", IsRequired = false, DefaultValue = 100000)]
		public int MaxQueueLength
		{
			get
			{
				return (int)base["maxQueueLength"];
			}
			set
			{
				base["maxQueueLength"] = value;
			}
		}

		// Token: 0x040003C3 RID: 963
		internal const string POLL_INTERVAL = "pollInterval";

		// Token: 0x040003C4 RID: 964
		internal const string MAX_QUEUE_LENGTH = "maxQueueLength";
	}
}
