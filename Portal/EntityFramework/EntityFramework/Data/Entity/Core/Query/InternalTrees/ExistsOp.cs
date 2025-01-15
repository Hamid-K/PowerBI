using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A3 RID: 931
	internal sealed class ExistsOp : ScalarOp
	{
		// Token: 0x06002D3B RID: 11579 RVA: 0x00091A51 File Offset: 0x0008FC51
		internal ExistsOp(TypeUsage type)
			: base(OpType.Exists, type)
		{
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x00091A5C File Offset: 0x0008FC5C
		private ExistsOp()
			: base(OpType.Exists)
		{
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x00091A66 File Offset: 0x0008FC66
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x00091A69 File Offset: 0x0008FC69
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x00091A73 File Offset: 0x0008FC73
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F22 RID: 3874
		internal static readonly ExistsOp Pattern = new ExistsOp();
	}
}
