using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	internal class KnownUsageProviderElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x00015607 File Offset: 0x00013807
		public KnownUsageProviderElement()
		{
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002038A File Offset: 0x0001E58A
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0002039C File Offset: 0x0001E59C
		[ConfigurationProperty("type", IsRequired = true)]
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

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x00017DC1 File Offset: 0x00015FC1
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

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000236D8 File Offset: 0x000218D8
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x000236EA File Offset: 0x000218EA
		[ConfigurationProperty("properties", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(ProviderPropertyElementCollection), AddItemName = "property")]
		public ProviderPropertyElementCollection ProviderProperties
		{
			get
			{
				return (ProviderPropertyElementCollection)base["properties"];
			}
			set
			{
				base["properties"] = value;
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000236F8 File Offset: 0x000218F8
		protected KnownUsageProviderElement(SerializationInfo info, StreamingContext context)
		{
			this.Type = info.GetString("type");
			this.Name = info.GetString("name");
			try
			{
				this.ProviderProperties = (ProviderPropertyElementCollection)info.GetValue("properties", typeof(ProviderPropertyElementCollection));
			}
			catch (SerializationException)
			{
				this.ProviderProperties = new ProviderPropertyElementCollection();
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00023770 File Offset: 0x00021970
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("type", this.Type);
			info.AddValue("name", this.Name);
			info.AddValue("properties", this.ProviderProperties);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000237A5 File Offset: 0x000219A5
		public override string ToString()
		{
			return this.Type.ToString() + "#" + this.Name;
		}

		// Token: 0x04000751 RID: 1873
		internal const string PROVIDER = "provider";

		// Token: 0x04000752 RID: 1874
		internal const string TYPE = "type";

		// Token: 0x04000753 RID: 1875
		internal const string NAME = "name";

		// Token: 0x04000754 RID: 1876
		internal const string PROPERTIES = "properties";
	}
}
