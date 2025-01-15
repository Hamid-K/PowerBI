using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200007F RID: 127
	internal sealed class DataTransformTable : IIdentifiable
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00006A53 File Offset: 0x00004C53
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00006A5B File Offset: 0x00004C5B
		public Identifier Id { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00006A64 File Offset: 0x00004C64
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00006A6C File Offset: 0x00004C6C
		public List<DataTransformTableColumn> Columns { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00006A75 File Offset: 0x00004C75
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataTransformTable;
			}
		}
	}
}
