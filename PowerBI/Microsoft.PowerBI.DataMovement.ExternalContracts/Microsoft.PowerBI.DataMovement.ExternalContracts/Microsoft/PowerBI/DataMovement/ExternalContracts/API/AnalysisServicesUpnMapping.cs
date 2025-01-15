using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000025 RID: 37
	[DataContract]
	public sealed class AnalysisServicesUpnMapping
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002D9D File Offset: 0x00000F9D
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002DA5 File Offset: 0x00000FA5
		[DataMember(Name = "connectionStringPropertyName", Order = 10)]
		public ConnectionStringPropertyName ConnectionStringPropertyName { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002DAE File Offset: 0x00000FAE
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002DB6 File Offset: 0x00000FB6
		[DataMember(Name = "mappingRules", Order = 20)]
		public IEnumerable<AnalysisServicesUpnMappingRule> MappingRules { get; set; }
	}
}
