using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003C4 RID: 964
	internal sealed class NullOp : ConstantBaseOp
	{
		// Token: 0x06002E1F RID: 11807 RVA: 0x00093BCC File Offset: 0x00091DCC
		internal NullOp(TypeUsage type)
			: base(OpType.Null, type, null)
		{
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x00093BD7 File Offset: 0x00091DD7
		private NullOp()
			: base(OpType.Null)
		{
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x00093BE0 File Offset: 0x00091DE0
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x00093BEA File Offset: 0x00091DEA
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F5F RID: 3935
		internal static readonly NullOp Pattern = new NullOp();
	}
}
