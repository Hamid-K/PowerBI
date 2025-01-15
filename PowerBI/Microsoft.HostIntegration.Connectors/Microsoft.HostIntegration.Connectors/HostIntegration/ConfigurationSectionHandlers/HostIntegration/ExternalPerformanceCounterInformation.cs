using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005CD RID: 1485
	public class ExternalPerformanceCounterInformation : ConfigurationElement
	{
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000AD824 File Offset: 0x000ABA24
		// (set) Token: 0x060033A4 RID: 13220 RVA: 0x000AD836 File Offset: 0x000ABA36
		[Description("Close")]
		[Category("General")]
		[ConfigurationProperty("close", IsRequired = true)]
		public string Close
		{
			get
			{
				return (string)base["close"];
			}
			set
			{
				base["close"] = value;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000AD844 File Offset: 0x000ABA44
		// (set) Token: 0x060033A6 RID: 13222 RVA: 0x000AD856 File Offset: 0x000ABA56
		[Description("Collect")]
		[Category("General")]
		[ConfigurationProperty("collect", IsRequired = true)]
		public string Collect
		{
			get
			{
				return (string)base["collect"];
			}
			set
			{
				base["collect"] = value;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000AD864 File Offset: 0x000ABA64
		// (set) Token: 0x060033A8 RID: 13224 RVA: 0x000AD876 File Offset: 0x000ABA76
		[Description("Open")]
		[Category("General")]
		[ConfigurationProperty("open", IsRequired = true)]
		public string Open
		{
			get
			{
				return (string)base["open"];
			}
			set
			{
				base["open"] = value;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x000AD884 File Offset: 0x000ABA84
		// (set) Token: 0x060033AA RID: 13226 RVA: 0x000AD896 File Offset: 0x000ABA96
		[Description("Library Directory")]
		[Category("General")]
		[ConfigurationProperty("libraryDirectory", IsRequired = true)]
		public DirectoryType LibraryDirectory
		{
			get
			{
				return (DirectoryType)base["libraryDirectory"];
			}
			set
			{
				base["libraryDirectory"] = value;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x000AD8A9 File Offset: 0x000ABAA9
		// (set) Token: 0x060033AC RID: 13228 RVA: 0x000AD8BB File Offset: 0x000ABABB
		[Description("Library")]
		[Category("General")]
		[ConfigurationProperty("library", IsRequired = true)]
		public string Library
		{
			get
			{
				return (string)base["library"];
			}
			set
			{
				base["library"] = value;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x000AD8C9 File Offset: 0x000ABAC9
		// (set) Token: 0x060033AE RID: 13230 RVA: 0x000AD8DB File Offset: 0x000ABADB
		[Description("Ini File Directory")]
		[Category("General")]
		[ConfigurationProperty("iniFileDirectory", IsRequired = true)]
		public DirectoryType IniFileDirectory
		{
			get
			{
				return (DirectoryType)base["iniFileDirectory"];
			}
			set
			{
				base["iniFileDirectory"] = value;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x000AD8EE File Offset: 0x000ABAEE
		// (set) Token: 0x060033B0 RID: 13232 RVA: 0x000AD900 File Offset: 0x000ABB00
		[Description("Ini File")]
		[Category("General")]
		[ConfigurationProperty("iniFile", IsRequired = true)]
		public string IniFile
		{
			get
			{
				return (string)base["iniFile"];
			}
			set
			{
				base["iniFile"] = value;
			}
		}
	}
}
