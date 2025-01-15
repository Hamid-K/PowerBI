using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003CD RID: 973
	internal abstract class PhysicalOp : Op
	{
		// Token: 0x06002E93 RID: 11923 RVA: 0x00094BAA File Offset: 0x00092DAA
		internal PhysicalOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x00094BB3 File Offset: 0x00092DB3
		internal override bool IsPhysicalOp
		{
			get
			{
				return true;
			}
		}
	}
}
