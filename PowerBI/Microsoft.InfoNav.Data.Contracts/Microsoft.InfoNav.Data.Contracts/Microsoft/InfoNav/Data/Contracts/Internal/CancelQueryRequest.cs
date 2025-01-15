using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000182 RID: 386
	[DataContract(Name = "CancelQueryRequest", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class CancelQueryRequest
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0001441D File Offset: 0x0001261D
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00014425 File Offset: 0x00012625
		[DataMember(Name = "QueryId", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string QueryId { get; set; }
	}
}
