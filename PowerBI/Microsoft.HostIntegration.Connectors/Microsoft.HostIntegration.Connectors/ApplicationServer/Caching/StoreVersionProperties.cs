using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	internal class StoreVersionProperties : ConfigurationElement, ISerializable
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x00015607 File Offset: 0x00013807
		internal StoreVersionProperties()
		{
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00017886 File Offset: 0x00015A86
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00017898 File Offset: 0x00015A98
		[ConfigurationProperty("clusterConfigurationStoreVersion", IsRequired = false, DefaultValue = "3.0.0.0")]
		internal string ClusterConfigStoreVersion
		{
			get
			{
				return (string)base["clusterConfigurationStoreVersion"];
			}
			set
			{
				base["clusterConfigurationStoreVersion"] = value;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000178A8 File Offset: 0x00015AA8
		protected StoreVersionProperties(SerializationInfo info, StreamingContext context)
		{
			try
			{
				this.ClusterConfigStoreVersion = info.GetString("clusterConfigurationStoreVersion");
			}
			catch (SerializationException)
			{
				this.ClusterConfigStoreVersion = "3.0.0.0";
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000178EC File Offset: 0x00015AEC
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info != null)
			{
				info.AddValue("clusterConfigurationStoreVersion", this.ClusterConfigStoreVersion);
				return;
			}
			throw new ArgumentNullException("info");
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00017910 File Offset: 0x00015B10
		public StoreVersionChange ComputeDifferences(StoreVersionProperties other)
		{
			StoreVersionChange storeVersionChange = default(StoreVersionChange);
			storeVersionChange[StoreVersionChanges.ClusterConfigStoreVersionChange] = this.ClusterConfigStoreVersion != other.ClusterConfigStoreVersion;
			return storeVersionChange;
		}

		// Token: 0x040003A9 RID: 937
		internal const string CONFIG_STORE_VERSION = "clusterConfigurationStoreVersion";
	}
}
