using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	internal sealed class CacheSizeQuotaConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0002011F File Offset: 0x0001E31F
		public CacheSizeQuotaConfig()
		{
			this.InitializeDefault();
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000209A7 File Offset: 0x0001EBA7
		public CacheSizeQuotaConfig(int sizeInMB, int highWaterMark, int lowWaterMark)
		{
			this.SizeInMB = sizeInMB;
			this.HighWatermarkInQuotaPercent = highWaterMark;
			this.LowWatermarkInQuotaPercent = lowWaterMark;
			this.Enabled = true;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00019F87 File Offset: 0x00018187
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00019F99 File Offset: 0x00018199
		[ConfigurationProperty("enabled", DefaultValue = false, IsRequired = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			set
			{
				base["enabled"] = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000209CB File Offset: 0x0001EBCB
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x000209DD File Offset: 0x0001EBDD
		[ConfigurationProperty("sizeInMB", DefaultValue = 256, IsRequired = false)]
		public int SizeInMB
		{
			get
			{
				return (int)base["sizeInMB"];
			}
			set
			{
				base["sizeInMB"] = value;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x000209F0 File Offset: 0x0001EBF0
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00020A02 File Offset: 0x0001EC02
		[ConfigurationProperty("highWatermarkInQuotaPercent", DefaultValue = 100, IsRequired = false)]
		public int HighWatermarkInQuotaPercent
		{
			get
			{
				return (int)base["highWatermarkInQuotaPercent"];
			}
			set
			{
				base["highWatermarkInQuotaPercent"] = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00020A15 File Offset: 0x0001EC15
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00020A27 File Offset: 0x0001EC27
		[ConfigurationProperty("lowWatermarkInQuotaPercent", DefaultValue = 100, IsRequired = false)]
		public int LowWatermarkInQuotaPercent
		{
			get
			{
				return (int)base["lowWatermarkInQuotaPercent"];
			}
			set
			{
				base["lowWatermarkInQuotaPercent"] = value;
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00020A3C File Offset: 0x0001EC3C
		public CacheSizeQuotaConfig(SerializationInfo info, StreamingContext context)
		{
			this.Enabled = info.GetBoolean("enabled");
			this.SizeInMB = info.GetInt32("sizeInMB");
			this.HighWatermarkInQuotaPercent = info.GetInt32("highWatermarkInQuotaPercent");
			this.LowWatermarkInQuotaPercent = info.GetInt32("lowWatermarkInQuotaPercent");
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00020A94 File Offset: 0x0001EC94
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("enabled", base["enabled"]);
			info.AddValue("sizeInMB", base["sizeInMB"]);
			info.AddValue("highWatermarkInQuotaPercent", base["highWatermarkInQuotaPercent"]);
			info.AddValue("lowWatermarkInQuotaPercent", base["lowWatermarkInQuotaPercent"]);
		}

		// Token: 0x040006E6 RID: 1766
		internal const string ENABLED = "enabled";

		// Token: 0x040006E7 RID: 1767
		internal const string SIZE_IN_MB = "sizeInMB";

		// Token: 0x040006E8 RID: 1768
		internal const string HIGH_WATERMARK_IN_QUOTA_PERCENT = "highWatermarkInQuotaPercent";

		// Token: 0x040006E9 RID: 1769
		internal const string LOW_WATERMARK_IN_QUOTA_PERCENT = "lowWatermarkInQuotaPercent";
	}
}
