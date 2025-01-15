using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000D0 RID: 208
	[DataContract]
	public sealed class DataShapeExpressionsAxisGroupingAggregates
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000C2AE File Offset: 0x0000A4AE
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0000C2B6 File Offset: 0x0000A4B6
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public string AggregatesMember { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0000C2C7 File Offset: 0x0000A4C7
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 20)]
		public IList<string> Ids { get; set; }
	}
}
