using System;
using System.Net;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000075 RID: 117
	[DataContract]
	public sealed class RelayInstance
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00004858 File Offset: 0x00002A58
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00004860 File Offset: 0x00002A60
		[DataMember(Name = "ipAddress", Order = 10)]
		public IPAddress IPAddress { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00004869 File Offset: 0x00002A69
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00004871 File Offset: 0x00002A71
		[DataMember(Name = "name", Order = 20)]
		public string Name { get; set; }

		// Token: 0x0600033D RID: 829 RVA: 0x0000487A File Offset: 0x00002A7A
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00004888 File Offset: 0x00002A88
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			RelayInstance relayInstance = (RelayInstance)obj;
			return relayInstance.Name == this.Name && relayInstance.IPAddress == this.IPAddress;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000048D7 File Offset: 0x00002AD7
		public override string ToString()
		{
			return this.Name + " " + this.IPAddress.ToString();
		}
	}
}
