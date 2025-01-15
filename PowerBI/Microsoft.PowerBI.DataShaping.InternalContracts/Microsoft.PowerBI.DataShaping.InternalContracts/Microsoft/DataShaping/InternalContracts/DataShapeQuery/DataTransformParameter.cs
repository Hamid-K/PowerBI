using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200007E RID: 126
	internal sealed class DataTransformParameter : IIdentifiable
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00006A25 File Offset: 0x00004C25
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00006A2D File Offset: 0x00004C2D
		public Identifier Id { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00006A36 File Offset: 0x00004C36
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00006A3E File Offset: 0x00004C3E
		public Expression Value { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00006A47 File Offset: 0x00004C47
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataTransformParameter;
			}
		}
	}
}
