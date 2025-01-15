using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200037C RID: 892
	internal abstract class AncillaryOp : Op
	{
		// Token: 0x06002B0E RID: 11022 RVA: 0x0008D603 File Offset: 0x0008B803
		internal AncillaryOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x0008D60C File Offset: 0x0008B80C
		internal override bool IsAncillaryOp
		{
			get
			{
				return true;
			}
		}
	}
}
