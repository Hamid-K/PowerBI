using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000386 RID: 902
	internal sealed class CollectOp : ScalarOp
	{
		// Token: 0x06002BCC RID: 11212 RVA: 0x0008DF21 File Offset: 0x0008C121
		internal CollectOp(TypeUsage type)
			: base(OpType.Collect, type)
		{
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x0008DF2C File Offset: 0x0008C12C
		private CollectOp()
			: base(OpType.Collect)
		{
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x0008DF36 File Offset: 0x0008C136
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x0008DF39 File Offset: 0x0008C139
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0008DF43 File Offset: 0x0008C143
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000EE6 RID: 3814
		internal static readonly CollectOp Pattern = new CollectOp();
	}
}
