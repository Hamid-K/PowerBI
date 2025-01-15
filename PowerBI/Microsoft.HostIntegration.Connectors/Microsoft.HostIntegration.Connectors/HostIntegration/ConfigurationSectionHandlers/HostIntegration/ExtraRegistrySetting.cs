using System;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005CB RID: 1483
	public class ExtraRegistrySetting : ConfigurationElement
	{
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000AD70B File Offset: 0x000AB90B
		// (set) Token: 0x0600338D RID: 13197 RVA: 0x000AD71D File Offset: 0x000AB91D
		[Description("Registry Hive")]
		[Category("General")]
		[ConfigurationProperty("hive", IsRequired = true)]
		public RegistryHive Hive
		{
			get
			{
				return (RegistryHive)base["hive"];
			}
			set
			{
				base["hive"] = value;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000AD730 File Offset: 0x000AB930
		// (set) Token: 0x0600338F RID: 13199 RVA: 0x000AD742 File Offset: 0x000AB942
		[Description("Path to registry key, under the Hive")]
		[Category("General")]
		[ConfigurationProperty("path", IsRequired = true)]
		public string Path
		{
			get
			{
				return (string)base["path"];
			}
			set
			{
				base["path"] = value;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06003391 RID: 13201 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("Name of the value, under the Path")]
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

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x00020620 File Offset: 0x0001E820
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x00020632 File Offset: 0x0001E832
		[Description("Value as a string")]
		[Category("General")]
		[ConfigurationProperty("value", IsRequired = true)]
		public string Value
		{
			get
			{
				return (string)base["value"];
			}
			set
			{
				base["value"] = value;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000AD750 File Offset: 0x000AB950
		// (set) Token: 0x06003395 RID: 13205 RVA: 0x000AD762 File Offset: 0x000AB962
		[Description("Directory if the Value is a file")]
		[Category("General")]
		[ConfigurationProperty("directory", IsRequired = true)]
		public DirectoryType Directory
		{
			get
			{
				return (DirectoryType)base["directory"];
			}
			set
			{
				base["directory"] = value;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000AD775 File Offset: 0x000AB975
		// (set) Token: 0x06003397 RID: 13207 RVA: 0x000AD787 File Offset: 0x000AB987
		[Description("Kind of Registry Value")]
		[Category("General")]
		[ConfigurationProperty("kind", IsRequired = true)]
		public RegistryValueKind Kind
		{
			get
			{
				return (RegistryValueKind)base["kind"];
			}
			set
			{
				base["kind"] = value;
			}
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000AD79C File Offset: 0x000AB99C
		public object GetElementKey()
		{
			return string.Concat(new string[]
			{
				this.Hive.ToString(),
				"\\",
				this.Path,
				"\\",
				this.Name
			});
		}
	}
}
