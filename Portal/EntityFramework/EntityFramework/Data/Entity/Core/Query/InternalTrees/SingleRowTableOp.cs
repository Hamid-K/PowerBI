using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003EA RID: 1002
	internal sealed class SingleRowTableOp : RelOp
	{
		// Token: 0x06002F17 RID: 12055 RVA: 0x0009560E File Offset: 0x0009380E
		private SingleRowTableOp()
			: base(OpType.SingleRowTable)
		{
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x00095618 File Offset: 0x00093818
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x0009561B File Offset: 0x0009381B
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x00095625 File Offset: 0x00093825
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FDD RID: 4061
		internal static readonly SingleRowTableOp Instance = new SingleRowTableOp();

		// Token: 0x04000FDE RID: 4062
		internal static readonly SingleRowTableOp Pattern = SingleRowTableOp.Instance;
	}
}
