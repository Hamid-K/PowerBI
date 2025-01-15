using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000399 RID: 921
	internal sealed class DerefOp : ScalarOp
	{
		// Token: 0x06002CDF RID: 11487 RVA: 0x000901F5 File Offset: 0x0008E3F5
		internal DerefOp(TypeUsage type)
			: base(OpType.Deref, type)
		{
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x00090200 File Offset: 0x0008E400
		private DerefOp()
			: base(OpType.Deref)
		{
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x0009020A File Offset: 0x0008E40A
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x0009020D File Offset: 0x0008E40D
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x00090217 File Offset: 0x0008E417
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F13 RID: 3859
		internal static readonly DerefOp Pattern = new DerefOp();
	}
}
