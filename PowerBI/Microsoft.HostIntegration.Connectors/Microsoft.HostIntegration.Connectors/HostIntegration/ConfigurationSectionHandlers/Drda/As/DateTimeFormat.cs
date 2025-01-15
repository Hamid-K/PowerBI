using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000544 RID: 1348
	public class DateTimeFormat : ConfigurationElement
	{
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x000988E4 File Offset: 0x00096AE4
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x000988F6 File Offset: 0x00096AF6
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sourceFormat", IsRequired = false, DefaultValue = DateTimeFormats.Undefined)]
		public DateTimeFormats SourceFormat
		{
			get
			{
				return (DateTimeFormats)base["sourceFormat"];
			}
			set
			{
				base["sourceFormat"] = value;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x00098909 File Offset: 0x00096B09
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x0009891B File Offset: 0x00096B1B
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("targetFormat", IsRequired = false, DefaultValue = DateTimeFormats.Undefined)]
		public DateTimeFormats TargetFormat
		{
			get
			{
				return (DateTimeFormats)base["targetFormat"];
			}
			set
			{
				base["targetFormat"] = value;
			}
		}
	}
}
