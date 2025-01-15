using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B4 RID: 948
	internal sealed class LeafOp : RulePatternOp
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x00092161 File Offset: 0x00090361
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x00092164 File Offset: 0x00090364
		private LeafOp()
			: base(OpType.Leaf)
		{
		}

		// Token: 0x04000F43 RID: 3907
		internal static readonly LeafOp Instance = new LeafOp();

		// Token: 0x04000F44 RID: 3908
		internal static readonly LeafOp Pattern = LeafOp.Instance;
	}
}
