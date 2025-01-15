using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000CE RID: 206
	[DataContract]
	public sealed class DataShapeExpressionsAxis
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000C205 File Offset: 0x0000A405
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0000C20D File Offset: 0x0000A40D
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public IList<DataShapeExpressionsAxisGrouping> Groupings { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000C216 File Offset: 0x0000A416
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0000C21E File Offset: 0x0000A41E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<SynchronizedGroupingBlock> Synchronization { get; set; }
	}
}
