using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200012A RID: 298
	internal class ETWMonitorConfigElement : ConfigurationElement
	{
		// Token: 0x06000891 RID: 2193 RVA: 0x00015607 File Offset: 0x00013807
		internal ETWMonitorConfigElement()
		{
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0001601E File Offset: 0x0001421E
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x00016030 File Offset: 0x00014230
		[ConfigurationProperty("interval", DefaultValue = 5000, IsRequired = false)]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		internal int Interval
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

		// Token: 0x04000690 RID: 1680
		internal const string INTERVAL = "interval";
	}
}
