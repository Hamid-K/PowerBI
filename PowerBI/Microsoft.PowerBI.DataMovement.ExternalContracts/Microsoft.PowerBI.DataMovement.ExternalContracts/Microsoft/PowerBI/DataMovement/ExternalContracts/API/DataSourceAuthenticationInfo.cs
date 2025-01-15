using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000039 RID: 57
	[DataContract]
	public class DataSourceAuthenticationInfo
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000033F2 File Offset: 0x000015F2
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000033FA File Offset: 0x000015FA
		[DataMember(Name = "kind", Order = 0)]
		public string Kind { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00003403 File Offset: 0x00001603
		// (set) Token: 0x06000148 RID: 328 RVA: 0x0000340B File Offset: 0x0000160B
		[DataMember(Name = "properties", Order = 10)]
		public IList<DataSourceAuthenticationProperty> Properties { get; set; }
	}
}
