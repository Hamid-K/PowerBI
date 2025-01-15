using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200013C RID: 316
	[Serializable]
	internal sealed class ProviderConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x00015607 File Offset: 0x00013807
		public ProviderConfig()
		{
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x0002038A File Offset: 0x0001E58A
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0002039C File Offset: 0x0001E59C
		[ConfigurationProperty("type", IsRequired = false)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x000203AA File Offset: 0x0001E5AA
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x000203BC File Offset: 0x0001E5BC
		[ConfigurationProperty("settings", IsDefaultCollection = false)]
		public ProviderSettings Settings
		{
			get
			{
				return (ProviderSettings)base["settings"];
			}
			set
			{
				base["settings"] = value;
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000203CA File Offset: 0x0001E5CA
		public ProviderConfig(SerializationInfo info, StreamingContext context)
		{
			this.Type = info.GetString("type");
			this.Settings = (ProviderSettings)info.GetValue("settings", typeof(ProviderSettings));
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00020403 File Offset: 0x0001E603
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("type", base["type"]);
			info.AddValue("settings", base["settings"]);
		}

		// Token: 0x040006D6 RID: 1750
		internal const string TYPE = "type";

		// Token: 0x040006D7 RID: 1751
		internal const string SETTINGS = "settings";
	}
}
