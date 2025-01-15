using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000B5 RID: 181
	[DataContract(Name = "CachePartitionConfig", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class CachePartitionConfig
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00014E69 File Offset: 0x00013069
		internal CachePartitionConfig(string primary, long version)
		{
			this.m_primary = primary;
			this.m_version = version;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00014E7F File Offset: 0x0001307F
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00014E87 File Offset: 0x00013087
		[DataMember]
		public string Primary
		{
			get
			{
				return this.m_primary;
			}
			set
			{
				this.m_primary = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00014E90 File Offset: 0x00013090
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00014E98 File Offset: 0x00013098
		[DataMember]
		internal long Version
		{
			get
			{
				return this.m_version;
			}
			private set
			{
				this.m_version = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00014EA1 File Offset: 0x000130A1
		public bool IsUsable
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_primary);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00014EB4 File Offset: 0x000130B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(this.m_primary);
			stringBuilder.AppendFormat(" ({0})", this.m_version);
			return stringBuilder.ToString();
		}

		// Token: 0x0400033F RID: 831
		private string m_primary;

		// Token: 0x04000340 RID: 832
		private long m_version;
	}
}
