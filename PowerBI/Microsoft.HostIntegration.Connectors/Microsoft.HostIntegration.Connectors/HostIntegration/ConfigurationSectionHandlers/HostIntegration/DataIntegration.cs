using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005C6 RID: 1478
	public class DataIntegration : FeatureWithService
	{
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000AD514 File Offset: 0x000AB714
		// (set) Token: 0x06003358 RID: 13144 RVA: 0x000AD526 File Offset: 0x000AB726
		[Description("Include DRDA-AS Service")]
		[Category("General")]
		[ConfigurationProperty("includeDrdaAsService", IsRequired = false, DefaultValue = "true")]
		public bool IncludeDrdaAsService
		{
			get
			{
				return (bool)base["includeDrdaAsService"];
			}
			set
			{
				base["includeDrdaAsService"] = value;
			}
		}
	}
}
