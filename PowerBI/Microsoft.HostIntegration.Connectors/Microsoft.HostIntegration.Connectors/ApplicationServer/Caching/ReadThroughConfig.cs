using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	internal sealed class ReadThroughConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x0002011F File Offset: 0x0001E31F
		public ReadThroughConfig()
		{
			this.InitializeDefault();
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00019F87 File Offset: 0x00018187
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00019F99 File Offset: 0x00018199
		[ConfigurationProperty("enabled", DefaultValue = false, IsRequired = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			set
			{
				base["enabled"] = value;
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00020359 File Offset: 0x0001E559
		public ReadThroughConfig(SerializationInfo info, StreamingContext context)
		{
			this.Enabled = info.GetBoolean("enabled");
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00020372 File Offset: 0x0001E572
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("enabled", base["enabled"]);
		}

		// Token: 0x040006D5 RID: 1749
		internal const string ENABLED = "enabled";
	}
}
