using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002DF RID: 735
	[DataContract(Name = "TransformOutput", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryTransformOutput
	{
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0002C02B File Offset: 0x0002A22B
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0002C033 File Offset: 0x0002A233
		[DataMember(IsRequired = true, Order = 1)]
		public QueryTransformTable Table { get; set; }
	}
}
