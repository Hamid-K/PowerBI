using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000047 RID: 71
	[DataContract]
	public class DiscoverMonikerSystemResponse
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00003BCC File Offset: 0x00001DCC
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00003BD4 File Offset: 0x00001DD4
		[DataMember(Name = "monikers", Order = 0)]
		public IList<MonikerSystemDetails> Monikers { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00003BDD File Offset: 0x00001DDD
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00003BE5 File Offset: 0x00001DE5
		[DataMember(Name = "gateways", Order = 10)]
		public IList<GatewaySystemDetails> Gateways { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00003BEE File Offset: 0x00001DEE
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00003BF6 File Offset: 0x00001DF6
		[DataMember(Name = "dataSources", Order = 20)]
		public IList<DataSourceSystemDetails> DataSources { get; set; }

		// Token: 0x0600020B RID: 523 RVA: 0x00003C00 File Offset: 0x00001E00
		public static DiscoverMonikerSystemResponse GetEmptyDiscoverMonikerSystemResponse()
		{
			return new DiscoverMonikerSystemResponse
			{
				Monikers = new MonikerSystemDetails[]
				{
					new MonikerSystemDetails
					{
						MonikerDataSources = new MonikerDataSourcesSystemDetails[]
						{
							new MonikerDataSourcesSystemDetails()
						}
					}
				},
				Gateways = new GatewaySystemDetails[]
				{
					new GatewaySystemDetails()
				},
				DataSources = new DataSourceSystemDetails[]
				{
					new DataSourceSystemDetails()
				}
			};
		}
	}
}
