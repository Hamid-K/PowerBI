using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000B2 RID: 178
	[DataContract(Name = "CacheLookupTableEntry", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class CacheLookupTableEntry
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0001483E File Offset: 0x00012A3E
		public CacheLookupTableEntry(CachePartitionId CachePartitionId, CachePartitionConfig Config)
		{
			this.Pid = CachePartitionId;
			this.m_config = Config;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00014854 File Offset: 0x00012A54
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0001485C File Offset: 0x00012A5C
		[DataMember]
		public CachePartitionId Pid
		{
			get
			{
				return this.m_pid;
			}
			private set
			{
				this.m_pid = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00014865 File Offset: 0x00012A65
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0001486D File Offset: 0x00012A6D
		[DataMember]
		public CachePartitionConfig Config
		{
			get
			{
				return this.m_config;
			}
			set
			{
				this.m_config = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00014876 File Offset: 0x00012A76
		public string ServiceNamespace
		{
			get
			{
				return this.m_pid.ServiceNamespace;
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00014884 File Offset: 0x00012A84
		internal static int ComparePartitionConfig(CacheLookupTableEntry x, CacheLookupTableEntry y)
		{
			int num = string.Compare(x.ServiceNamespace, y.ServiceNamespace, StringComparison.Ordinal);
			if (num == 0)
			{
				num = x.Pid.CompareTo(y.Pid);
			}
			return num;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000148BA File Offset: 0x00012ABA
		public override string ToString()
		{
			return this.m_pid.ToString();
		}

		// Token: 0x04000336 RID: 822
		private CachePartitionId m_pid;

		// Token: 0x04000337 RID: 823
		private CachePartitionConfig m_config;

		// Token: 0x04000338 RID: 824
		internal static readonly Comparison<CacheLookupTableEntry> s_comparison = new Comparison<CacheLookupTableEntry>(CacheLookupTableEntry.ComparePartitionConfig);
	}
}
