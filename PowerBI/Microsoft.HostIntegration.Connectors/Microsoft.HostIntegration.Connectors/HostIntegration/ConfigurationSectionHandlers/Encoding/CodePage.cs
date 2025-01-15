using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding
{
	// Token: 0x02000525 RID: 1317
	public class CodePage : ConfigurationElement
	{
		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000978B0 File Offset: 0x00095AB0
		// (set) Token: 0x06002C8A RID: 11402 RVA: 0x000978C2 File Offset: 0x00095AC2
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("number", IsRequired = true)]
		public int Number
		{
			get
			{
				return (int)base["number"];
			}
			set
			{
				base["number"] = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06002C8C RID: 11404 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000978D5 File Offset: 0x00095AD5
		// (set) Token: 0x06002C8E RID: 11406 RVA: 0x000978E7 File Offset: 0x00095AE7
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("description", IsRequired = false)]
		public string Description
		{
			get
			{
				return (string)base["description"];
			}
			set
			{
				base["description"] = value;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000978F5 File Offset: 0x00095AF5
		// (set) Token: 0x06002C90 RID: 11408 RVA: 0x00097907 File Offset: 0x00095B07
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("nlsCodePage", IsRequired = true)]
		public int NlsCodePage
		{
			get
			{
				return (int)base["nlsCodePage"];
			}
			set
			{
				base["nlsCodePage"] = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x0009791A File Offset: 0x00095B1A
		// (set) Token: 0x06002C92 RID: 11410 RVA: 0x0009792C File Offset: 0x00095B2C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("unicodeToEbcdicConversions", IsRequired = false)]
		public UnicodeToEbcdicConversionCollection UnicodeToEbcdicConversions
		{
			get
			{
				return (UnicodeToEbcdicConversionCollection)base["unicodeToEbcdicConversions"];
			}
			set
			{
				base["unicodeToEbcdicConversions"] = value;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x0009793A File Offset: 0x00095B3A
		// (set) Token: 0x06002C94 RID: 11412 RVA: 0x0009794C File Offset: 0x00095B4C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("ebcdicToUnicodeConversions", IsRequired = false)]
		public EbcdicToUnicodeConversionCollection EbcdicToUnicodeConversions
		{
			get
			{
				return (EbcdicToUnicodeConversionCollection)base["ebcdicToUnicodeConversions"];
			}
			set
			{
				base["ebcdicToUnicodeConversions"] = value;
			}
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x0009795A File Offset: 0x00095B5A
		public object GetElementKey()
		{
			return this.Name;
		}
	}
}
