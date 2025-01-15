using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000537 RID: 1335
	public class ConversionFormats : ConfigurationElement
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x0009815F File Offset: 0x0009635F
		// (set) Token: 0x06002D1C RID: 11548 RVA: 0x00098171 File Offset: 0x00096371
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("dateTimeMasks", IsRequired = false)]
		public DateTimeMaskCollection DateTimeMasks
		{
			get
			{
				return (DateTimeMaskCollection)base["dateTimeMasks"];
			}
			set
			{
				base["dateTimeMasks"] = value;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x0009817F File Offset: 0x0009637F
		// (set) Token: 0x06002D1E RID: 11550 RVA: 0x00098191 File Offset: 0x00096391
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("timeMasks", IsRequired = false)]
		public TimeMaskCollection TimeMasks
		{
			get
			{
				return (TimeMaskCollection)base["timeMasks"];
			}
			set
			{
				base["timeMasks"] = value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x0009819F File Offset: 0x0009639F
		// (set) Token: 0x06002D20 RID: 11552 RVA: 0x000981B1 File Offset: 0x000963B1
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("dateMasks", IsRequired = false)]
		public DateMaskCollection DateMasks
		{
			get
			{
				return (DateMaskCollection)base["dateMasks"];
			}
			set
			{
				base["dateMasks"] = value;
			}
		}
	}
}
