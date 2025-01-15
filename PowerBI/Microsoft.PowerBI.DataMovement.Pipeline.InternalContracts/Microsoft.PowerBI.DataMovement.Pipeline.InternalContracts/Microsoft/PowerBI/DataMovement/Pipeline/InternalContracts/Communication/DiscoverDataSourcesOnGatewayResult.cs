using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000027 RID: 39
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class DiscoverDataSourcesOnGatewayResult : GatewayResultBase
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000025BA File Offset: 0x000007BA
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000025C2 File Offset: 0x000007C2
		[DataMember(Name = "errorMessages", IsRequired = false)]
		public IList<string> ErrorMessages { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000025CB File Offset: 0x000007CB
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000025D3 File Offset: 0x000007D3
		[DataMember(Name = "resultCode", IsRequired = true)]
		public int ResultCode { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000025DC File Offset: 0x000007DC
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000025E4 File Offset: 0x000007E4
		[DataMember(Name = "dataSourceReferences", IsRequired = false)]
		public IList<DataSourceReferenceMetadata> DataSourceReferences { get; set; }
	}
}
