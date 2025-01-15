using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200006A RID: 106
	internal sealed class DetailGroupIdentity : IIdentifiable
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00006094 File Offset: 0x00004294
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000609C File Offset: 0x0000429C
		public Identifier Id { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000060A5 File Offset: 0x000042A5
		// (set) Token: 0x06000264 RID: 612 RVA: 0x000060AD File Offset: 0x000042AD
		public Expression Value { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000060B6 File Offset: 0x000042B6
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.DetailGroupIdentity;
			}
		}
	}
}
