using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D2 RID: 210
	internal class DataCacheReadyRetryPolicyProperty : ConfigurationElement
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00017D65 File Offset: 0x00015F65
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00017D77 File Offset: 0x00015F77
		[IntegerValidator(MinValue = 0)]
		[ConfigurationProperty("retryCount", DefaultValue = 60)]
		public int RetryCount
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

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00017D8A File Offset: 0x00015F8A
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00017D9C File Offset: 0x00015F9C
		[IntegerValidator(MinValue = 1)]
		[ConfigurationProperty("maximumRetryIntervalInSeconds", DefaultValue = 1)]
		public int MaximumRetryIntervalInSeconds
		{
			get
			{
				return (int)base["maximumRetryIntervalInSeconds"];
			}
			set
			{
				base["maximumRetryIntervalInSeconds"] = value;
			}
		}

		// Token: 0x040003CB RID: 971
		internal const string RETRY_COUNT = "retryCount";

		// Token: 0x040003CC RID: 972
		internal const string MAXIMUM_RETRY_INTERVAL = "maximumRetryIntervalInSeconds";
	}
}
