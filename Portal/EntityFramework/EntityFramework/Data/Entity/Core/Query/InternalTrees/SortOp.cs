using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003EF RID: 1007
	internal sealed class SortOp : SortBaseOp
	{
		// Token: 0x06002F31 RID: 12081 RVA: 0x00095730 File Offset: 0x00093930
		private SortOp()
			: base(OpType.Sort)
		{
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x0009573A File Offset: 0x0009393A
		internal SortOp(List<SortKey> sortKeys)
			: base(OpType.Sort, sortKeys)
		{
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x00095745 File Offset: 0x00093945
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x00095748 File Offset: 0x00093948
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x00095752 File Offset: 0x00093952
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FE7 RID: 4071
		internal static readonly SortOp Pattern = new SortOp();
	}
}
