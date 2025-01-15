using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003AA RID: 938
	internal sealed class GetRefKeyOp : ScalarOp
	{
		// Token: 0x06002D6B RID: 11627 RVA: 0x00091E0E File Offset: 0x0009000E
		internal GetRefKeyOp(TypeUsage type)
			: base(OpType.GetRefKey, type)
		{
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x00091E19 File Offset: 0x00090019
		private GetRefKeyOp()
			: base(OpType.GetRefKey)
		{
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002D6D RID: 11629 RVA: 0x00091E23 File Offset: 0x00090023
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x00091E26 File Offset: 0x00090026
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x00091E30 File Offset: 0x00090030
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F34 RID: 3892
		internal static readonly GetRefKeyOp Pattern = new GetRefKeyOp();
	}
}
