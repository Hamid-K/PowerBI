using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000C5 RID: 197
	[DataContract(Name = "ReportMetadataResult", Namespace = "http://schemas.microsoft.com/sqlbi/2015/09/ReportMetadataService")]
	public sealed class ReportMetadataResult
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00007125 File Offset: 0x00005325
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000712D File Offset: 0x0000532D
		[DataMember(IsRequired = false, Order = 20)]
		public IList<Pod> Pods { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00007136 File Offset: 0x00005336
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000713E File Offset: 0x0000533E
		[DataMember(IsRequired = false, Order = 25)]
		public IList<QueryExtensionSchema> QueryExtensionSchemas { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00007147 File Offset: 0x00005347
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000714F File Offset: 0x0000534F
		[DataMember(IsRequired = false, Order = 30)]
		public ModelLinguisticSchema LinguisticSchema { get; set; }
	}
}
