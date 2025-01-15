using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A9 RID: 937
	internal sealed class GetEntityRefOp : ScalarOp
	{
		// Token: 0x06002D65 RID: 11621 RVA: 0x00091DD6 File Offset: 0x0008FFD6
		internal GetEntityRefOp(TypeUsage type)
			: base(OpType.GetEntityRef, type)
		{
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x00091DE1 File Offset: 0x0008FFE1
		private GetEntityRefOp()
			: base(OpType.GetEntityRef)
		{
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x00091DEB File Offset: 0x0008FFEB
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x00091DEE File Offset: 0x0008FFEE
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x00091DF8 File Offset: 0x0008FFF8
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F33 RID: 3891
		internal static readonly GetEntityRefOp Pattern = new GetEntityRefOp();
	}
}
