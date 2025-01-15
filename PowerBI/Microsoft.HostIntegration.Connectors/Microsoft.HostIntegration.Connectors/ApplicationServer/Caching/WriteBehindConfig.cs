using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200013A RID: 314
	[Serializable]
	internal sealed class WriteBehindConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x0002011F File Offset: 0x0001E31F
		public WriteBehindConfig()
		{
			this.InitializeDefault();
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00019F87 File Offset: 0x00018187
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x00019F99 File Offset: 0x00018199
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

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0001601E File Offset: 0x0001421E
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00016030 File Offset: 0x00014230
		[IntegerValidator(MinValue = 60, MaxValue = 2147483647)]
		[ConfigurationProperty("interval", DefaultValue = 300, IsRequired = false)]
		public int WriteBehindInterval
		{
			get
			{
				return (int)base["interval"];
			}
			set
			{
				base["interval"] = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001EAFD File Offset: 0x0001CCFD
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0001EB0F File Offset: 0x0001CD0F
		[ConfigurationProperty("retryInterval", DefaultValue = 60, IsRequired = false)]
		[IntegerValidator(MinValue = 60, MaxValue = 2147483647)]
		public int WriteBehindRetryInterval
		{
			get
			{
				return (int)base["retryInterval"];
			}
			set
			{
				base["retryInterval"] = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00017D65 File Offset: 0x00015F65
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x00017D77 File Offset: 0x00015F77
		[ConfigurationProperty("retryCount", DefaultValue = -1, IsRequired = false)]
		[IntegerValidator(MinValue = -1, MaxValue = 2147483647)]
		public int WriteBehindRetryCount
		{
			get
			{
				return (int)base["retryCount"];
			}
			set
			{
				base["retryCount"] = value;
			}
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002029C File Offset: 0x0001E49C
		public WriteBehindConfig(SerializationInfo info, StreamingContext context)
		{
			this.Enabled = info.GetBoolean("enabled");
			this.WriteBehindInterval = info.GetInt32("interval");
			this.WriteBehindRetryInterval = info.GetInt32("retryInterval");
			this.WriteBehindRetryCount = info.GetInt32("retryCount");
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x000202F4 File Offset: 0x0001E4F4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("enabled", base["enabled"]);
			info.AddValue("interval", base["interval"]);
			info.AddValue("retryInterval", base["retryInterval"]);
			info.AddValue("retryCount", base["retryCount"]);
		}

		// Token: 0x040006CE RID: 1742
		internal const string ENABLED = "enabled";

		// Token: 0x040006CF RID: 1743
		internal const string INTERVAL = "interval";

		// Token: 0x040006D0 RID: 1744
		internal const string RETRY_INTERVAL = "retryInterval";

		// Token: 0x040006D1 RID: 1745
		internal const string RETRY_COUNT = "retryCount";

		// Token: 0x040006D2 RID: 1746
		internal const int DefaultInterval = 300;

		// Token: 0x040006D3 RID: 1747
		internal const int DefaultRetryInterval = 60;

		// Token: 0x040006D4 RID: 1748
		internal const int DefaultRetryCount = -1;
	}
}
