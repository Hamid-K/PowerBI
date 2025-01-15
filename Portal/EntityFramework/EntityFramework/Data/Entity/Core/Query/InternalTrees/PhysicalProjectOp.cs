using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003CE RID: 974
	internal class PhysicalProjectOp : PhysicalOp
	{
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x00094BB6 File Offset: 0x00092DB6
		internal SimpleCollectionColumnMap ColumnMap
		{
			get
			{
				return this.m_columnMap;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x00094BBE File Offset: 0x00092DBE
		internal VarList Outputs
		{
			get
			{
				return this.m_outputVars;
			}
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x00094BC6 File Offset: 0x00092DC6
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x00094BD0 File Offset: 0x00092DD0
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x00094BDA File Offset: 0x00092DDA
		internal PhysicalProjectOp(VarList outputVars, SimpleCollectionColumnMap columnMap)
			: this()
		{
			this.m_outputVars = outputVars;
			this.m_columnMap = columnMap;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x00094BF0 File Offset: 0x00092DF0
		private PhysicalProjectOp()
			: base(OpType.PhysicalProject)
		{
		}

		// Token: 0x04000FB6 RID: 4022
		internal static readonly PhysicalProjectOp Pattern = new PhysicalProjectOp();

		// Token: 0x04000FB7 RID: 4023
		private readonly SimpleCollectionColumnMap m_columnMap;

		// Token: 0x04000FB8 RID: 4024
		private readonly VarList m_outputVars;
	}
}
