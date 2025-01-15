using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	internal class EvictionConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x06000942 RID: 2370 RVA: 0x0001FF15 File Offset: 0x0001E115
		public EvictionConfig()
		{
			this.Type = EvictionType.Lru;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0001FF24 File Offset: 0x0001E124
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x0001FF36 File Offset: 0x0001E136
		[ConfigurationProperty("type", IsRequired = true)]
		public EvictionType Type
		{
			get
			{
				return (EvictionType)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001FF49 File Offset: 0x0001E149
		protected EvictionConfig(SerializationInfo info, StreamingContext context)
		{
			base["type"] = (EvictionType)info.GetInt32("type");
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001FF6C File Offset: 0x0001E16C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("type", (int)base["type"]);
		}

		// Token: 0x040006C3 RID: 1731
		internal const string TYPE = "type";
	}
}
