using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Tict
{
	// Token: 0x0200051E RID: 1310
	public class ApplicationServerCachingClientVersion : ConfigurationElement
	{
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x00097537 File Offset: 0x00095737
		// (set) Token: 0x06002C4D RID: 11341 RVA: 0x00097549 File Offset: 0x00095749
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

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x0009755C File Offset: 0x0009575C
		// (set) Token: 0x06002C4F RID: 11343 RVA: 0x0009756E File Offset: 0x0009576E
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

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x00097581 File Offset: 0x00095781
		// (set) Token: 0x06002C51 RID: 11345 RVA: 0x00097593 File Offset: 0x00095793
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

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x000975A6 File Offset: 0x000957A6
		// (set) Token: 0x06002C53 RID: 11347 RVA: 0x000975B8 File Offset: 0x000957B8
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

		// Token: 0x06002C54 RID: 11348 RVA: 0x000975CC File Offset: 0x000957CC
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
