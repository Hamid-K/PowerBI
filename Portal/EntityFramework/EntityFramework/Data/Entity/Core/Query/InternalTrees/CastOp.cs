using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000383 RID: 899
	internal sealed class CastOp : ScalarOp
	{
		// Token: 0x06002BBB RID: 11195 RVA: 0x0008DE37 File Offset: 0x0008C037
		internal CastOp(TypeUsage type)
			: base(OpType.Cast, type)
		{
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x0008DE42 File Offset: 0x0008C042
		private CastOp()
			: base(OpType.Cast)
		{
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x0008DE4C File Offset: 0x0008C04C
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x0008DE4F File Offset: 0x0008C04F
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x0008DE59 File Offset: 0x0008C059
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000EDC RID: 3804
		internal static readonly CastOp Pattern = new CastOp();
	}
}
