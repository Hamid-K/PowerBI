using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200039F RID: 927
	internal sealed class ElementOp : ScalarOp
	{
		// Token: 0x06002D29 RID: 11561 RVA: 0x0009197C File Offset: 0x0008FB7C
		internal ElementOp(TypeUsage type)
			: base(OpType.Element, type)
		{
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x00091987 File Offset: 0x0008FB87
		private ElementOp()
			: base(OpType.Element)
		{
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x00091991 File Offset: 0x0008FB91
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x00091994 File Offset: 0x0008FB94
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x0009199E File Offset: 0x0008FB9E
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F1E RID: 3870
		internal static readonly ElementOp Pattern = new ElementOp();
	}
}
