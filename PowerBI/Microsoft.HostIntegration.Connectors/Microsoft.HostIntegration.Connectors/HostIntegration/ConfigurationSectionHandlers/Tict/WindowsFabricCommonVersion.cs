using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Tict
{
	// Token: 0x02000520 RID: 1312
	public class WindowsFabricCommonVersion : ConfigurationElement
	{
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x00097537 File Offset: 0x00095737
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x00097549 File Offset: 0x00095749
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

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x0009755C File Offset: 0x0009575C
		// (set) Token: 0x06002C63 RID: 11363 RVA: 0x0009756E File Offset: 0x0009576E
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

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002C64 RID: 11364 RVA: 0x00097581 File Offset: 0x00095781
		// (set) Token: 0x06002C65 RID: 11365 RVA: 0x00097593 File Offset: 0x00095793
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

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002C66 RID: 11366 RVA: 0x000975A6 File Offset: 0x000957A6
		// (set) Token: 0x06002C67 RID: 11367 RVA: 0x000975B8 File Offset: 0x000957B8
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

		// Token: 0x06002C68 RID: 11368 RVA: 0x000976B4 File Offset: 0x000958B4
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
