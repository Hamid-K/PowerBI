using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000049 RID: 73
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class OleDbQueryResultMdDataset : OleDbQueryResultBase
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002B87 File Offset: 0x00000D87
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00002B8F File Offset: 0x00000D8F
		[DataMember(Name = "axes", IsRequired = false, EmitDefaultValue = true)]
		internal OleDbAxisInfo[] Axes { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00002B98 File Offset: 0x00000D98
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00002BA0 File Offset: 0x00000DA0
		[IgnoreDataMember]
		internal IRowset[] AxisRowsets { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00002BA9 File Offset: 0x00000DA9
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00002BB1 File Offset: 0x00000DB1
		[IgnoreDataMember]
		internal CellsRowsetPageReader CellsReader { get; set; }
	}
}
