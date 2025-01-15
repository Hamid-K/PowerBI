using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001C2 RID: 450
	[DataContract(Name = "Execute", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SemanticQueryExecutionRequest
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00017649 File Offset: 0x00015849
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x00017651 File Offset: 0x00015851
		[DataMember(Name = "Commands", IsRequired = true, Order = 10)]
		public List<SemanticQueryDataShapeCommandRequest> Commands { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0001765A File Offset: 0x0001585A
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x00017662 File Offset: 0x00015862
		[DataMember(Name = "DataReductionConfiguration", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataReductionConfigurationOption? DataReductionConfiguration { get; set; }
	}
}
