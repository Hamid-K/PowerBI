using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000538 RID: 1336
	public class ConversionFormatOptions : ConfigurationElement
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x000981BF File Offset: 0x000963BF
		// (set) Token: 0x06002D23 RID: 11555 RVA: 0x000981D1 File Offset: 0x000963D1
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("charAsTime", IsRequired = false, DefaultValue = false)]
		public bool CharAsTime
		{
			get
			{
				return (bool)base["charAsTime"];
			}
			set
			{
				base["charAsTime"] = value;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000981E4 File Offset: 0x000963E4
		// (set) Token: 0x06002D25 RID: 11557 RVA: 0x000981F6 File Offset: 0x000963F6
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("varCharAsTime", IsRequired = false, DefaultValue = false)]
		public bool VarCharAsTime
		{
			get
			{
				return (bool)base["varCharAsTime"];
			}
			set
			{
				base["varCharAsTime"] = value;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002D26 RID: 11558 RVA: 0x00098209 File Offset: 0x00096409
		// (set) Token: 0x06002D27 RID: 11559 RVA: 0x0009821B File Offset: 0x0009641B
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("charAsDate", IsRequired = false, DefaultValue = false)]
		public bool CharAsDate
		{
			get
			{
				return (bool)base["charAsDate"];
			}
			set
			{
				base["charAsDate"] = value;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002D28 RID: 11560 RVA: 0x0009822E File Offset: 0x0009642E
		// (set) Token: 0x06002D29 RID: 11561 RVA: 0x00098240 File Offset: 0x00096440
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("varCharAsDate", IsRequired = false, DefaultValue = false)]
		public bool VarCharAsDate
		{
			get
			{
				return (bool)base["varCharAsDate"];
			}
			set
			{
				base["varCharAsDate"] = value;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x00098253 File Offset: 0x00096453
		// (set) Token: 0x06002D2B RID: 11563 RVA: 0x00098265 File Offset: 0x00096465
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("charAsTimestamp", IsRequired = false, DefaultValue = false)]
		public bool CharAsTimestamp
		{
			get
			{
				return (bool)base["charAsTimestamp"];
			}
			set
			{
				base["charAsTimestamp"] = value;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x00098278 File Offset: 0x00096478
		// (set) Token: 0x06002D2D RID: 11565 RVA: 0x0009828A File Offset: 0x0009648A
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("varCharAsTimestamp", IsRequired = false, DefaultValue = false)]
		public bool VarCharAsTimestamp
		{
			get
			{
				return (bool)base["varCharAsTimestamp"];
			}
			set
			{
				base["varCharAsTimestamp"] = value;
			}
		}
	}
}
