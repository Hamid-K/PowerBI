using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000156 RID: 342
	[Serializable]
	internal class ProviderPropertyElement : ConfigurationElement, ISerializable
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x00015607 File Offset: 0x00013807
		public ProviderPropertyElement()
		{
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x00017DC1 File Offset: 0x00015FC1
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

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00020620 File Offset: 0x0001E820
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00020632 File Offset: 0x0001E832
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

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002368A File Offset: 0x0002188A
		protected ProviderPropertyElement(SerializationInfo info, StreamingContext context)
		{
			this.Value = info.GetString("value");
			this.Name = info.GetString("name");
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000236B4 File Offset: 0x000218B4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.Value);
			info.AddValue("name", this.Name);
		}

		// Token: 0x0400074E RID: 1870
		internal const string NAME = "name";

		// Token: 0x0400074F RID: 1871
		internal const string VALUE = "value";

		// Token: 0x04000750 RID: 1872
		internal const string PROPERTY = "property";
	}
}
