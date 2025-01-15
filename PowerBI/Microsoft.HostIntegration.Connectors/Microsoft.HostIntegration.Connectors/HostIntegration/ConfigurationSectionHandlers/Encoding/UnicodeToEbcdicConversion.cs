using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding
{
	// Token: 0x0200052C RID: 1324
	public class UnicodeToEbcdicConversion : ConfigurationElement
	{
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x000979BA File Offset: 0x00095BBA
		// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x000979CC File Offset: 0x00095BCC
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("to", IsRequired = true)]
		public string To
		{
			get
			{
				return (string)base["to"];
			}
			set
			{
				base["to"] = value;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000979DA File Offset: 0x00095BDA
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x000979EC File Offset: 0x00095BEC
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("from", IsRequired = true)]
		public string From
		{
			get
			{
				return (string)base["from"];
			}
			set
			{
				base["from"] = value;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x000979FA File Offset: 0x00095BFA
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x00097A0C File Offset: 0x00095C0C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("reversible", IsRequired = false, DefaultValue = false)]
		public bool Reversible
		{
			get
			{
				return (bool)base["reversible"];
			}
			set
			{
				base["reversible"] = value;
			}
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x00097DDC File Offset: 0x00095FDC
		public object GetElementKey()
		{
			return this.To + this.From;
		}
	}
}
