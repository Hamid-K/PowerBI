using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003BD RID: 957
	internal sealed class NewInstanceOp : ScalarOp
	{
		// Token: 0x06002DD5 RID: 11733 RVA: 0x00092445 File Offset: 0x00090645
		internal NewInstanceOp(TypeUsage type)
			: base(OpType.NewInstance, type)
		{
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x00092450 File Offset: 0x00090650
		private NewInstanceOp()
			: base(OpType.NewInstance)
		{
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x0009245A File Offset: 0x0009065A
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x00092464 File Offset: 0x00090664
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F54 RID: 3924
		internal static readonly NewInstanceOp Pattern = new NewInstanceOp();
	}
}
