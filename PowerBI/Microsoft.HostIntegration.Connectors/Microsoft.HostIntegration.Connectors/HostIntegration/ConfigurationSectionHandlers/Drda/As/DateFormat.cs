using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x0200053A RID: 1338
	public class DateFormat : ConfigurationElement
	{
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x0009829D File Offset: 0x0009649D
		// (set) Token: 0x06002D30 RID: 11568 RVA: 0x000982AF File Offset: 0x000964AF
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sourceFormat", IsRequired = false, DefaultValue = DateFormats.Undefined)]
		public DateFormats SourceFormat
		{
			get
			{
				return (DateFormats)base["sourceFormat"];
			}
			set
			{
				base["sourceFormat"] = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x000982C2 File Offset: 0x000964C2
		// (set) Token: 0x06002D32 RID: 11570 RVA: 0x000982D4 File Offset: 0x000964D4
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("targetFormat", IsRequired = false, DefaultValue = DateFormats.Undefined)]
		public DateFormats TargetFormat
		{
			get
			{
				return (DateFormats)base["targetFormat"];
			}
			set
			{
				base["targetFormat"] = value;
			}
		}
	}
}
