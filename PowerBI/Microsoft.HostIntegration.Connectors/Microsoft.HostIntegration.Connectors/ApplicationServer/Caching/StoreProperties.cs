using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000BD RID: 189
	[Serializable]
	internal class StoreProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00015607 File Offset: 0x00013807
		internal StoreProperties()
		{
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00015DBC File Offset: 0x00013FBC
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00015DCE File Offset: 0x00013FCE
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00015DE1 File Offset: 0x00013FE1
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00015DF3 File Offset: 0x00013FF3
		[Obsolete("This property is deprecated and not used anymore.")]
		[ConfigurationProperty("subDirSize", IsRequired = false, DefaultValue = 4)]
		[IntegerValidator(MinValue = 1, MaxValue = 8)]
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00015E06 File Offset: 0x00014006
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x00015E18 File Offset: 0x00014018
		[IntegerValidator(MinValue = 1)]
		[ConfigurationProperty("batchSize", IsRequired = false, DefaultValue = 65536)]
		internal int BatchSize
		{
			get
			{
				return (int)base["batchSize"];
			}
			set
			{
				base["batchSize"] = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00015E2B File Offset: 0x0001402B
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x00015E3D File Offset: 0x0001403D
		[ConfigurationProperty("delayPartitionAdditionEnabled", IsRequired = false, DefaultValue = false)]
		internal bool DelayPartitionAdditionEnabled
		{
			get
			{
				return (bool)base["delayPartitionAdditionEnabled"];
			}
			set
			{
				base["delayPartitionAdditionEnabled"] = value;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00015E50 File Offset: 0x00014050
		protected StoreProperties(SerializationInfo info, StreamingContext context)
		{
			this.RootDirSize = (int)info.GetValue("rootDirSize", typeof(int));
			this.SubDirSize = (int)info.GetValue("subDirSize", typeof(int));
			this.BatchSize = (int)info.GetValue("batchSize", typeof(int));
			this.DelayPartitionAdditionEnabled = (bool)info.GetValue("delayPartitionAdditionEnabled", typeof(bool));
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00015EE4 File Offset: 0x000140E4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("rootDirSize", this.RootDirSize);
			info.AddValue("subDirSize", this.SubDirSize);
			info.AddValue("batchSize", this.BatchSize);
			info.AddValue("delayPartitionAdditionEnabled", this.DelayPartitionAdditionEnabled);
		}

		// Token: 0x04000364 RID: 868
		internal const string STORE_ROOT_DIR_SIZE = "rootDirSize";

		// Token: 0x04000365 RID: 869
		internal const string STORE_SUB_DIR_SIZE = "subDirSize";

		// Token: 0x04000366 RID: 870
		internal const string BATCH_SIZE = "batchSize";

		// Token: 0x04000367 RID: 871
		internal const string DELAY_PARTITION_ADDITION_ENABLED = "delayPartitionAdditionEnabled";
	}
}
