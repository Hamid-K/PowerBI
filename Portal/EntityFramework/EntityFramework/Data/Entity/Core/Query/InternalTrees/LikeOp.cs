using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B6 RID: 950
	internal sealed class LikeOp : ScalarOp
	{
		// Token: 0x06002DAD RID: 11693 RVA: 0x000921B8 File Offset: 0x000903B8
		internal LikeOp(TypeUsage boolType)
			: base(OpType.Like, boolType)
		{
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000921C3 File Offset: 0x000903C3
		private LikeOp()
			: base(OpType.Like)
		{
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000921CD File Offset: 0x000903CD
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000921D0 File Offset: 0x000903D0
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000921DA File Offset: 0x000903DA
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F47 RID: 3911
		internal static readonly LikeOp Pattern = new LikeOp();
	}
}
