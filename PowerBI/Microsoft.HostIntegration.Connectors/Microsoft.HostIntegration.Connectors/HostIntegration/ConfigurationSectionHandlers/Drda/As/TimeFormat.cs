using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000554 RID: 1364
	public class TimeFormat : ConfigurationElement
	{
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x0009AFE9 File Offset: 0x000991E9
		// (set) Token: 0x06002E16 RID: 11798 RVA: 0x0009AFFB File Offset: 0x000991FB
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sourceFormat", IsRequired = false, DefaultValue = TimeFormats.Undefined)]
		public TimeFormats SourceFormat
		{
			get
			{
				return (TimeFormats)base["sourceFormat"];
			}
			set
			{
				base["sourceFormat"] = value;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x0009B00E File Offset: 0x0009920E
		// (set) Token: 0x06002E18 RID: 11800 RVA: 0x0009B020 File Offset: 0x00099220
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("targetFormat", IsRequired = false, DefaultValue = TimeFormats.Undefined)]
		public TimeFormats TargetFormat
		{
			get
			{
				return (TimeFormats)base["targetFormat"];
			}
			set
			{
				base["targetFormat"] = value;
			}
		}
	}
}
