using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000139 RID: 313
	[Serializable]
	internal sealed class BackingStoreConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x0002011F File Offset: 0x0001E31F
		public BackingStoreConfig()
		{
			this.InitializeDefault();
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0002012D File Offset: 0x0001E32D
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0002013F File Offset: 0x0001E33F
		[ConfigurationProperty("writeBehind", IsRequired = false)]
		public WriteBehindConfig WriteBehind
		{
			get
			{
				return (WriteBehindConfig)base["writeBehind"];
			}
			set
			{
				base["writeBehind"] = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002014D File Offset: 0x0001E34D
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0002015F File Offset: 0x0001E35F
		[ConfigurationProperty("readThrough", IsRequired = false)]
		public ReadThroughConfig ReadThrough
		{
			get
			{
				return (ReadThroughConfig)base["readThrough"];
			}
			set
			{
				base["readThrough"] = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0002016D File Offset: 0x0001E36D
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x0001EA8A File Offset: 0x0001CC8A
		[ConfigurationProperty("provider", IsRequired = false)]
		public ProviderConfig Provider
		{
			get
			{
				return (ProviderConfig)base["provider"];
			}
			set
			{
				base["provider"] = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00017B0D File Offset: 0x00015D0D
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x00017B1F File Offset: 0x00015D1F
		[ConfigurationProperty("serializationProperties", IsRequired = false)]
		public SerializationConfig SerializationConfig
		{
			get
			{
				return (SerializationConfig)base["serializationProperties"];
			}
			set
			{
				base["serializationProperties"] = value;
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00020180 File Offset: 0x0001E380
		public BackingStoreConfig(SerializationInfo info, StreamingContext context)
		{
			this.WriteBehind = (WriteBehindConfig)info.GetValue("writeBehind", typeof(WriteBehindConfig));
			this.ReadThrough = (ReadThroughConfig)info.GetValue("readThrough", typeof(ReadThroughConfig));
			this.Provider = (ProviderConfig)info.GetValue("provider", typeof(ProviderConfig));
			try
			{
				this.SerializationConfig = (SerializationConfig)info.GetValue("serializationProperties", typeof(SerializationConfig));
			}
			catch (SerializationException)
			{
				this.SerializationConfig = new SerializationConfig();
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00020234 File Offset: 0x0001E434
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("writeBehind", base["writeBehind"]);
			info.AddValue("readThrough", base["readThrough"]);
			info.AddValue("provider", base["provider"]);
			info.AddValue("serializationProperties", base["serializationProperties"]);
		}

		// Token: 0x040006CA RID: 1738
		internal const string WRITE_BEHIND = "writeBehind";

		// Token: 0x040006CB RID: 1739
		internal const string READ_THROUGH = "readThrough";

		// Token: 0x040006CC RID: 1740
		internal const string PROVIDER = "provider";

		// Token: 0x040006CD RID: 1741
		internal const string SERIALIZATION_PROPERTIES = "serializationProperties";
	}
}
