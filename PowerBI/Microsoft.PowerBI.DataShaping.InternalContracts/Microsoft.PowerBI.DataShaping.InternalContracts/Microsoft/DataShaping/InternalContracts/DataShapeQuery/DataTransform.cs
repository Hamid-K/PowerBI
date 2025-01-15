using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x0200007B RID: 123
	internal sealed class DataTransform : IIdentifiable
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00006992 File Offset: 0x00004B92
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000699A File Offset: 0x00004B9A
		public Identifier Id { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000069A3 File Offset: 0x00004BA3
		// (set) Token: 0x060002FA RID: 762 RVA: 0x000069AB File Offset: 0x00004BAB
		public Candidate<string> Algorithm { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000069B4 File Offset: 0x00004BB4
		// (set) Token: 0x060002FC RID: 764 RVA: 0x000069BC File Offset: 0x00004BBC
		public DataTransformInput Input { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000069C5 File Offset: 0x00004BC5
		// (set) Token: 0x060002FE RID: 766 RVA: 0x000069CD File Offset: 0x00004BCD
		public DataTransformOutput Output { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000069D6 File Offset: 0x00004BD6
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataTransform;
			}
		}
	}
}
