using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000063 RID: 99
	[DataContract]
	public class MonikerSystemDetails
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000044CC File Offset: 0x000026CC
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x000044D4 File Offset: 0x000026D4
		[DataMember(Name = "provider", Order = 0)]
		public string Provider { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000044DD File Offset: 0x000026DD
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x000044E5 File Offset: 0x000026E5
		[DataMember(Name = "connectionString", Order = 10)]
		public string ConnectionString { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000044EE File Offset: 0x000026EE
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x000044F6 File Offset: 0x000026F6
		[DataMember(Name = "monikerId", Order = 20)]
		public long MonikerId { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000044FF File Offset: 0x000026FF
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00004507 File Offset: 0x00002707
		[DataMember(Name = "moniker", Order = 30)]
		public string Moniker { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00004510 File Offset: 0x00002710
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00004518 File Offset: 0x00002718
		[DataMember(Name = "monikerDataSources", Order = 40)]
		public IList<MonikerDataSourcesSystemDetails> MonikerDataSources { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00004521 File Offset: 0x00002721
		public bool HasDataSource
		{
			get
			{
				return this.MonikerDataSources != null && this.MonikerDataSources.Any<MonikerDataSourcesSystemDetails>();
			}
		}
	}
}
