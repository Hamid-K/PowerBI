using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003AF RID: 943
	internal sealed class InternalConstantOp : ConstantBaseOp
	{
		// Token: 0x06002D88 RID: 11656 RVA: 0x00091F3F File Offset: 0x0009013F
		internal InternalConstantOp(TypeUsage type, object value)
			: base(OpType.InternalConstant, type, value)
		{
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x00091F4A File Offset: 0x0009014A
		private InternalConstantOp()
			: base(OpType.InternalConstant)
		{
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x00091F53 File Offset: 0x00090153
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x00091F5D File Offset: 0x0009015D
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F3C RID: 3900
		internal static readonly InternalConstantOp Pattern = new InternalConstantOp();
	}
}
