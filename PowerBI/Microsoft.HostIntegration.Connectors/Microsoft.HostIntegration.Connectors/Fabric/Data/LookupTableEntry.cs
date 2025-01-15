using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003C0 RID: 960
	[DataContract(Name = "LookupTableEntry", Namespace = "http://schemas.microsoft.com/2008/casdata")]
	internal class LookupTableEntry
	{
		// Token: 0x060021C4 RID: 8644 RVA: 0x000683F2 File Offset: 0x000665F2
		public LookupTableEntry(PartitionId partitionId, ServiceReplicaSet config)
		{
			this.Pid = partitionId;
			this.Config = config;
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x00068408 File Offset: 0x00066608
		// (set) Token: 0x060021C6 RID: 8646 RVA: 0x00068410 File Offset: 0x00066610
		[DataMember]
		public PartitionId Pid
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

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x00068419 File Offset: 0x00066619
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x00068421 File Offset: 0x00066621
		[DataMember]
		public ServiceReplicaSet Config
		{
			get
			{
				return this.m_config;
			}
			private set
			{
				this.m_config = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x0006842A File Offset: 0x0006662A
		public string ServiceNamespace
		{
			get
			{
				return this.m_pid.ServiceNamespace;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x00068437 File Offset: 0x00066637
		public long Version
		{
			get
			{
				return this.m_config.Version;
			}
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00068444 File Offset: 0x00066644
		public override string ToString()
		{
			return this.m_pid.ToString() + ":" + this.m_config.ToString();
		}

		// Token: 0x0400157F RID: 5503
		private PartitionId m_pid;

		// Token: 0x04001580 RID: 5504
		private ServiceReplicaSet m_config;
	}
}
