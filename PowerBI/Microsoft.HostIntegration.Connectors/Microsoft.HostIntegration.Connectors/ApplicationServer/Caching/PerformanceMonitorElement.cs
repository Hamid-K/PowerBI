using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000128 RID: 296
	internal class PerformanceMonitorElement : ConfigurationElement
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x00015607 File Offset: 0x00013807
		internal PerformanceMonitorElement()
		{
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00015FAF File Offset: 0x000141AF
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x00015FC1 File Offset: 0x000141C1
		[ConfigurationProperty("isEnabled", DefaultValue = true, IsRequired = false)]
		internal bool IsEnable
		{
			get
			{
				return (bool)base["isEnabled"];
			}
			set
			{
				base["isEnabled"] = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0001E9EA File Offset: 0x0001CBEA
		[ConfigurationProperty("sampleInterval", DefaultValue = 950L, IsRequired = false)]
		internal long SampleInterval
		{
			get
			{
				return (long)base["sampleInterval"];
			}
			set
			{
				base["sampleInterval"] = value;
			}
		}

		// Token: 0x0400068A RID: 1674
		private const string ISENABLED = "isEnabled";

		// Token: 0x0400068B RID: 1675
		private const string SAMPLEINTERVAL = "sampleInterval";
	}
}
