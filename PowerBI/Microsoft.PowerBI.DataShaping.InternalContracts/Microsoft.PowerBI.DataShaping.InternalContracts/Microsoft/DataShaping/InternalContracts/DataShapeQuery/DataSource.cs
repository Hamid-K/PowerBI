using System;
using System.Diagnostics;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000079 RID: 121
	[DebuggerDisplay("[DataSource] Id={Id}")]
	internal sealed class DataSource : IIdentifiable
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000693A File Offset: 0x00004B3A
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00006942 File Offset: 0x00004B42
		public Identifier Id { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000694B File Offset: 0x00004B4B
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00006953 File Offset: 0x00004B53
		public DataSourceReference DataSourceReference { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000695C File Offset: 0x00004B5C
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataSource;
			}
		}
	}
}
