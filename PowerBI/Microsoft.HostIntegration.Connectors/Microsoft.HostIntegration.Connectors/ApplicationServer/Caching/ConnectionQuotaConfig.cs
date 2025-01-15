using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000143 RID: 323
	[Serializable]
	internal sealed class ConnectionQuotaConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x0002011F File Offset: 0x0001E31F
		public ConnectionQuotaConfig()
		{
			this.InitializeDefault();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00020AF9 File Offset: 0x0001ECF9
		public ConnectionQuotaConfig(int count)
		{
			this.ConnectionCount = count;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00020B08 File Offset: 0x0001ED08
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00020B1A File Offset: 0x0001ED1A
		[ConfigurationProperty("connectionCount", DefaultValue = 10, IsRequired = false)]
		public int ConnectionCount
		{
			get
			{
				return (int)base["connectionCount"];
			}
			set
			{
				base["connectionCount"] = value;
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00020B2D File Offset: 0x0001ED2D
		public ConnectionQuotaConfig(SerializationInfo info, StreamingContext context)
		{
			this.ConnectionCount = info.GetInt32("connectionCount");
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00020B46 File Offset: 0x0001ED46
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("connectionCount", base["connectionCount"]);
		}

		// Token: 0x040006EA RID: 1770
		private const string CONNECTION = "connectionCount";
	}
}
