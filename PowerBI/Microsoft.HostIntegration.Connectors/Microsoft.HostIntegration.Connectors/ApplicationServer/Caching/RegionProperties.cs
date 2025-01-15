using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000BE RID: 190
	[Serializable]
	internal class RegionProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x00015607 File Offset: 0x00013807
		internal RegionProperties()
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00015DBC File Offset: 0x00013FBC
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00015DCE File Offset: 0x00013FCE
		[IntegerValidator(MinValue = 4, MaxValue = 16)]
		[Obsolete("This property is deprecated and not used anymore.")]
		[ConfigurationProperty("rootDirSize", IsRequired = false, DefaultValue = 4)]
		internal int RootDirSize
		{
			get
			{
				return (int)base["rootDirSize"];
			}
			set
			{
				base["rootDirSize"] = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00015DE1 File Offset: 0x00013FE1
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00015DF3 File Offset: 0x00013FF3
		[ConfigurationProperty("subDirSize", IsRequired = false, DefaultValue = 4)]
		[IntegerValidator(MinValue = 1, MaxValue = 8)]
		[Obsolete("This property is deprecated and not used anymore.")]
		internal int SubDirSize
		{
			get
			{
				return (int)base["subDirSize"];
			}
			set
			{
				base["subDirSize"] = value;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00015F38 File Offset: 0x00014138
		protected RegionProperties(SerializationInfo info, StreamingContext context)
		{
			this.RootDirSize = (int)info.GetValue("rootDirSize", typeof(int));
			this.SubDirSize = (int)info.GetValue("subDirSize", typeof(int));
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00015F8B File Offset: 0x0001418B
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("rootDirSize", this.RootDirSize);
			info.AddValue("subDirSize", this.SubDirSize);
		}

		// Token: 0x04000368 RID: 872
		internal const string REGION_ROOT_DIR_SIZE = "rootDirSize";

		// Token: 0x04000369 RID: 873
		internal const string REGION_SUB_DIR_SIZE = "subDirSize";
	}
}
