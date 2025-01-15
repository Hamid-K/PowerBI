using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000532 RID: 1330
	public class CollationName : ConfigurationElement
	{
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000979BA File Offset: 0x00095BBA
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x000979CC File Offset: 0x00095BCC
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

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x000979DA File Offset: 0x00095BDA
		// (set) Token: 0x06002CEE RID: 11502 RVA: 0x000979EC File Offset: 0x00095BEC
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

		// Token: 0x06002CEF RID: 11503 RVA: 0x00097F80 File Offset: 0x00096180
		public object GetElementKey()
		{
			return this.From + this.To;
		}
	}
}
