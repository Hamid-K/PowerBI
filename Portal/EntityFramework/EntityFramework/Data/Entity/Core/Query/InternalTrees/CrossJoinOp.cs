using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000398 RID: 920
	internal sealed class CrossJoinOp : JoinBaseOp
	{
		// Token: 0x06002CDA RID: 11482 RVA: 0x000901BE File Offset: 0x0008E3BE
		private CrossJoinOp()
			: base(OpType.CrossJoin)
		{
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x000901C8 File Offset: 0x0008E3C8
		internal override int Arity
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000901CB File Offset: 0x0008E3CB
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000901D5 File Offset: 0x0008E3D5
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F11 RID: 3857
		internal static readonly CrossJoinOp Instance = new CrossJoinOp();

		// Token: 0x04000F12 RID: 3858
		internal static readonly CrossJoinOp Pattern = CrossJoinOp.Instance;
	}
}
