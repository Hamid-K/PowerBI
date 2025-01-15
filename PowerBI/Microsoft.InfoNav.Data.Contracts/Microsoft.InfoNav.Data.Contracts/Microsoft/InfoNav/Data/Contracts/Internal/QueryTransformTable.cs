using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002E2 RID: 738
	[DataContract(Name = "TransformTable", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryTransformTable
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x0002C152 File Offset: 0x0002A352
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x0002C15A File Offset: 0x0002A35A
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x0002C163 File Offset: 0x0002A363
		// (set) Token: 0x0600189F RID: 6303 RVA: 0x0002C16B File Offset: 0x0002A36B
		[DataMember(IsRequired = true, Order = 2)]
		public List<QueryTransformTableColumn> Columns { get; set; }
	}
}
