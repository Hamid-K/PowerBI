using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding
{
	// Token: 0x02000528 RID: 1320
	public class EbcdicToUnicodeConversion : ConfigurationElement
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x000979BA File Offset: 0x00095BBA
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x000979CC File Offset: 0x00095BCC
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

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x000979DA File Offset: 0x00095BDA
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x000979EC File Offset: 0x00095BEC
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

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x000979FA File Offset: 0x00095BFA
		// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x00097A0C File Offset: 0x00095C0C
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

		// Token: 0x06002CAA RID: 11434 RVA: 0x00097A1F File Offset: 0x00095C1F
		public object GetElementKey()
		{
			return this.To + this.From;
		}
	}
}
