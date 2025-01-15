using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E2 RID: 226
	[Serializable]
	internal class DeploymentModeElement : ConfigurationElement, ISerializable
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x00015607 File Offset: 0x00013807
		internal DeploymentModeElement()
		{
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00019EF5 File Offset: 0x000180F5
		internal DeploymentModeElement(DataCacheDeploymentMode mode)
		{
			this.Value = mode;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00019F04 File Offset: 0x00018104
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x00019F16 File Offset: 0x00018116
		[ConfigurationProperty("value", DefaultValue = DataCacheDeploymentMode.Unknown, IsRequired = false)]
		public DataCacheDeploymentMode Value
		{
			get
			{
				return (DataCacheDeploymentMode)base["value"];
			}
			set
			{
				base["value"] = value;
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00019F29 File Offset: 0x00018129
		protected DeploymentModeElement(SerializationInfo info, StreamingContext context)
		{
			this.Value = (DataCacheDeploymentMode)info.GetValue("value", typeof(DataCacheDeploymentMode));
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00019F51 File Offset: 0x00018151
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.Value);
		}

		// Token: 0x040003EC RID: 1004
		internal const string VALUE = "value";
	}
}
