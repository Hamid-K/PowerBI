using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Tict
{
	// Token: 0x02000521 RID: 1313
	public class WindowsFabricDataCommonVersion : ConfigurationElement
	{
		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x00097537 File Offset: 0x00095737
		// (set) Token: 0x06002C6B RID: 11371 RVA: 0x00097549 File Offset: 0x00095749
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

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x0009755C File Offset: 0x0009575C
		// (set) Token: 0x06002C6D RID: 11373 RVA: 0x0009756E File Offset: 0x0009576E
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

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002C6E RID: 11374 RVA: 0x00097581 File Offset: 0x00095781
		// (set) Token: 0x06002C6F RID: 11375 RVA: 0x00097593 File Offset: 0x00095793
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

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000975A6 File Offset: 0x000957A6
		// (set) Token: 0x06002C71 RID: 11377 RVA: 0x000975B8 File Offset: 0x000957B8
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

		// Token: 0x06002C72 RID: 11378 RVA: 0x00097728 File Offset: 0x00095928
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
