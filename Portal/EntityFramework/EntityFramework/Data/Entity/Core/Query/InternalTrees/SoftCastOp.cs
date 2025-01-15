using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003EC RID: 1004
	internal sealed class SoftCastOp : ScalarOp
	{
		// Token: 0x06002F23 RID: 12067 RVA: 0x00095699 File Offset: 0x00093899
		internal SoftCastOp(TypeUsage type)
			: base(OpType.SoftCast, type)
		{
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000956A4 File Offset: 0x000938A4
		private SoftCastOp()
			: base(OpType.SoftCast)
		{
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000956AE File Offset: 0x000938AE
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000956B1 File Offset: 0x000938B1
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000956BB File Offset: 0x000938BB
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FE2 RID: 4066
		internal static readonly SoftCastOp Pattern = new SoftCastOp();
	}
}
