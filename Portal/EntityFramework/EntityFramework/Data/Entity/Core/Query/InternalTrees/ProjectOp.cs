using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003CF RID: 975
	internal sealed class ProjectOp : RelOp
	{
		// Token: 0x06002E9C RID: 11932 RVA: 0x00094C06 File Offset: 0x00092E06
		private ProjectOp()
			: base(OpType.Project)
		{
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x00094C10 File Offset: 0x00092E10
		internal ProjectOp(VarVec vars)
			: this()
		{
			this.m_vars = vars;
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x00094C1F File Offset: 0x00092E1F
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x00094C22 File Offset: 0x00092E22
		internal VarVec Outputs
		{
			get
			{
				return this.m_vars;
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x00094C2A File Offset: 0x00092E2A
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x00094C34 File Offset: 0x00092E34
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FB9 RID: 4025
		private readonly VarVec m_vars;

		// Token: 0x04000FBA RID: 4026
		internal static readonly ProjectOp Pattern = new ProjectOp();
	}
}
