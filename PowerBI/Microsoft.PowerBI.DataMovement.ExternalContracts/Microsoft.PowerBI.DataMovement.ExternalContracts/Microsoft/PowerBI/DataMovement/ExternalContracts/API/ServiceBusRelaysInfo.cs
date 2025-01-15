using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000079 RID: 121
	[DataContract]
	public sealed class ServiceBusRelaysInfo
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000499C File Offset: 0x00002B9C
		// (set) Token: 0x06000355 RID: 853 RVA: 0x000049A4 File Offset: 0x00002BA4
		[Required]
		[DataMember(Name = "serviceBusName", Order = 10)]
		public string ServiceBusName { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000049AD File Offset: 0x00002BAD
		// (set) Token: 0x06000357 RID: 855 RVA: 0x000049B5 File Offset: 0x00002BB5
		[Required]
		[DataMember(Name = "hostName", Order = 20)]
		public string HostName { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000049BE File Offset: 0x00002BBE
		// (set) Token: 0x06000359 RID: 857 RVA: 0x000049C6 File Offset: 0x00002BC6
		[Required]
		[DataMember(Name = "hostIPAddress", Order = 30)]
		public IPAddress HostIPAddress { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000049CF File Offset: 0x00002BCF
		// (set) Token: 0x0600035B RID: 859 RVA: 0x000049D7 File Offset: 0x00002BD7
		[Required]
		[DataMember(Name = "relays", Order = 50)]
		public List<RelayInstance> Relays { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000049E0 File Offset: 0x00002BE0
		// (set) Token: 0x0600035D RID: 861 RVA: 0x000049E8 File Offset: 0x00002BE8
		[DataMember(Name = "serviceBusNameSecondary", Order = 60)]
		public string ServiceBusNameSecondary { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000049F1 File Offset: 0x00002BF1
		// (set) Token: 0x0600035F RID: 863 RVA: 0x000049F9 File Offset: 0x00002BF9
		[DataMember(Name = "hostNameSecondary", Order = 70)]
		public string HostNameSecondary { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00004A02 File Offset: 0x00002C02
		// (set) Token: 0x06000361 RID: 865 RVA: 0x00004A0A File Offset: 0x00002C0A
		[DataMember(Name = "hostIPAddressSecondary", Order = 80)]
		public IPAddress HostIPAddressSecondary { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00004A13 File Offset: 0x00002C13
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00004A1B File Offset: 0x00002C1B
		[DataMember(Name = "relaysSecondary", Order = 90)]
		public List<RelayInstance> RelaysSecondary { get; set; }
	}
}
