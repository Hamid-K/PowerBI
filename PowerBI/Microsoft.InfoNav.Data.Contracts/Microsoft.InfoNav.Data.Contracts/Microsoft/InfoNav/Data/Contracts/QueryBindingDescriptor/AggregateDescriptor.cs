using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000C7 RID: 199
	[DataContract]
	public sealed class AggregateDescriptor
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000BEE5 File Offset: 0x0000A0E5
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0000BEED File Offset: 0x0000A0ED
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public List<string> Ids { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000BEF6 File Offset: 0x0000A0F6
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 20)]
		public QueryBindingDescriptorAggregateContainer Aggregate { get; set; }
	}
}
