using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	internal sealed class ProviderSetting : ConfigurationElement, ISerializable
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x00015607 File Offset: 0x00013807
		public ProviderSetting()
		{
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00020600 File Offset: 0x0001E800
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00020612 File Offset: 0x0001E812
		[ConfigurationProperty("key", IsRequired = true, IsKey = true)]
		public string Key
		{
			get
			{
				return (string)base["key"];
			}
			set
			{
				base["key"] = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00020620 File Offset: 0x0001E820
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x00020632 File Offset: 0x0001E832
		[ConfigurationProperty("value", IsRequired = true, IsKey = true)]
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

		// Token: 0x0600098E RID: 2446 RVA: 0x00020640 File Offset: 0x0001E840
		public ProviderSetting(SerializationInfo info, StreamingContext context)
		{
			this.Key = info.GetString("key");
			this.Value = info.GetString("value");
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002066A File Offset: 0x0001E86A
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("key", (string)base["key"]);
			info.AddValue("value", (string)base["value"]);
		}

		// Token: 0x040006DC RID: 1756
		internal const string KEY = "key";

		// Token: 0x040006DD RID: 1757
		internal const string VALUE = "value";
	}
}
