using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Tict
{
	// Token: 0x0200051F RID: 1311
	public class ApplicationServerCachingCoreVersion : ConfigurationElement
	{
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x00097537 File Offset: 0x00095737
		// (set) Token: 0x06002C57 RID: 11351 RVA: 0x00097549 File Offset: 0x00095749
		[ConfigurationProperty("majorVersion", IsRequired = true)]
		public int MajorVersion
		{
			get
			{
				return (int)base["majorVersion"];
			}
			set
			{
				base["majorVersion"] = value;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x0009755C File Offset: 0x0009575C
		// (set) Token: 0x06002C59 RID: 11353 RVA: 0x0009756E File Offset: 0x0009576E
		[ConfigurationProperty("minorVersion", IsRequired = true)]
		public int MinorVersion
		{
			get
			{
				return (int)base["minorVersion"];
			}
			set
			{
				base["minorVersion"] = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x00097581 File Offset: 0x00095781
		// (set) Token: 0x06002C5B RID: 11355 RVA: 0x00097593 File Offset: 0x00095793
		[ConfigurationProperty("buildNumber", IsRequired = true)]
		public int BuildNumber
		{
			get
			{
				return (int)base["buildNumber"];
			}
			set
			{
				base["buildNumber"] = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000975A6 File Offset: 0x000957A6
		// (set) Token: 0x06002C5D RID: 11357 RVA: 0x000975B8 File Offset: 0x000957B8
		[ConfigurationProperty("revision", IsRequired = true)]
		public int Revision
		{
			get
			{
				return (int)base["revision"];
			}
			set
			{
				base["revision"] = value;
			}
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x00097640 File Offset: 0x00095840
		public object GetElementKey()
		{
			return string.Concat(new string[]
			{
				this.MajorVersion.ToString(),
				".",
				this.MinorVersion.ToString(),
				".",
				this.BuildNumber.ToString(),
				".",
				this.Revision.ToString()
			});
		}
	}
}
