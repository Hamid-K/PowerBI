using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000530 RID: 1328
	public class ApplicationEncoding : ConfigurationElement
	{
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x00097E47 File Offset: 0x00096047
		// (set) Token: 0x06002CD4 RID: 11476 RVA: 0x00097E59 File Offset: 0x00096059
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("scheme", IsRequired = true)]
		public ApplicationEncodingSchemes Scheme
		{
			get
			{
				return (ApplicationEncodingSchemes)base["scheme"];
			}
			set
			{
				base["scheme"] = value;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x00097E6C File Offset: 0x0009606C
		// (set) Token: 0x06002CD6 RID: 11478 RVA: 0x00097E7E File Offset: 0x0009607E
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("ccsid", IsRequired = true, DefaultValue = 37)]
		public int CcsidValue
		{
			get
			{
				return (int)base["ccsid"];
			}
			set
			{
				base["ccsid"] = value;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x00097E91 File Offset: 0x00096091
		// (set) Token: 0x06002CD8 RID: 11480 RVA: 0x00097E7E File Offset: 0x0009607E
		public ApplicationEncodingCcsids Ccsid
		{
			get
			{
				return (ApplicationEncodingCcsids)base["ccsid"];
			}
			set
			{
				base["ccsid"] = (int)value;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x00097EA3 File Offset: 0x000960A3
		// (set) Token: 0x06002CDA RID: 11482 RVA: 0x00097EB5 File Offset: 0x000960B5
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("customCcsid", IsRequired = false, DefaultValue = -1)]
		public int CustomCcsid
		{
			get
			{
				return (int)base["customCcsid"];
			}
			set
			{
				base["customCcsid"] = value;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x00097EC8 File Offset: 0x000960C8
		// (set) Token: 0x06002CDC RID: 11484 RVA: 0x00097EDA File Offset: 0x000960DA
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("database", IsRequired = true)]
		public string Database
		{
			get
			{
				return (string)base["database"];
			}
			set
			{
				base["database"] = value;
			}
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x00097EE8 File Offset: 0x000960E8
		public object GetElementKey()
		{
			return this.Scheme.ToString() + this.Ccsid.ToString() + this.Database;
		}
	}
}
