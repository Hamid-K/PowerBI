using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005B6 RID: 1462
	public class MessageIntegration : FeatureWithService
	{
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000AD192 File Offset: 0x000AB392
		// (set) Token: 0x06003318 RID: 13080 RVA: 0x000AD1A4 File Offset: 0x000AB3A4
		[Description("Microsoft Client for MQ Only")]
		[Category("General")]
		[ConfigurationProperty("mqClientOnly", IsRequired = false, DefaultValue = "false")]
		public bool MqClientOnly
		{
			get
			{
				return (bool)base["mqClientOnly"];
			}
			set
			{
				base["mqClientOnly"] = value;
			}
		}
	}
}
