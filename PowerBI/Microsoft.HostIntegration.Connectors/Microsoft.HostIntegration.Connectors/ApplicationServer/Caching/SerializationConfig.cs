using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	internal class SerializationConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x00015607 File Offset: 0x00013807
		public SerializationConfig()
		{
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00020431 File Offset: 0x0001E631
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00020443 File Offset: 0x0001E643
		[ConfigurationProperty("serializer", IsRequired = false, DefaultValue = DataCacheObjectSerializerType.NetDataContractSerializer)]
		public DataCacheObjectSerializerType SerializerType
		{
			get
			{
				return (DataCacheObjectSerializerType)base["serializer"];
			}
			set
			{
				base["serializer"] = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00020456 File Offset: 0x0001E656
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00020468 File Offset: 0x0001E668
		[ConfigurationProperty("customSerializerType", IsRequired = false)]
		public string CustomSerializerTypeName
		{
			get
			{
				return (string)base["customSerializerType"];
			}
			set
			{
				base["customSerializerType"] = value;
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00020476 File Offset: 0x0001E676
		protected SerializationConfig(SerializationInfo info, StreamingContext context)
		{
			this.SerializerType = (DataCacheObjectSerializerType)info.GetInt32("serializer");
			this.CustomSerializerTypeName = info.GetString("customSerializerType");
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000204A0 File Offset: 0x0001E6A0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("serializer", (int)this.SerializerType);
			info.AddValue("customSerializerType", this.CustomSerializerTypeName);
		}

		// Token: 0x040006D8 RID: 1752
		internal const string SERIALIZER = "serializer";

		// Token: 0x040006D9 RID: 1753
		internal const string CUSTOM_SERIALIZER_TYPE = "customSerializerType";
	}
}
