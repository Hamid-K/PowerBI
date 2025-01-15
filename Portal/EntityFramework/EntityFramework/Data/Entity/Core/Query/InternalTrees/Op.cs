using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003C6 RID: 966
	internal abstract class Op
	{
		// Token: 0x06002E29 RID: 11817 RVA: 0x00093C34 File Offset: 0x00091E34
		internal Op(OpType opType)
		{
			this.m_opType = opType;
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x00093C43 File Offset: 0x00091E43
		internal OpType OpType
		{
			get
			{
				return this.m_opType;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x00093C4B File Offset: 0x00091E4B
		internal virtual int Arity
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x00093C4E File Offset: 0x00091E4E
		internal virtual bool IsScalarOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x00093C51 File Offset: 0x00091E51
		internal virtual bool IsRulePatternOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x00093C54 File Offset: 0x00091E54
		internal virtual bool IsRelOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x00093C57 File Offset: 0x00091E57
		internal virtual bool IsAncillaryOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x00093C5A File Offset: 0x00091E5A
		internal virtual bool IsPhysicalOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x00093C5D File Offset: 0x00091E5D
		internal virtual bool IsEquivalent(Op other)
		{
			return false;
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x00093C60 File Offset: 0x00091E60
		// (set) Token: 0x06002E33 RID: 11827 RVA: 0x00093C63 File Offset: 0x00091E63
		internal virtual TypeUsage Type
		{
			get
			{
				return null;
			}
			set
			{
				throw Error.NotSupported();
			}
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x00093C6A File Offset: 0x00091E6A
		[DebuggerNonUserCode]
		internal virtual void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x00093C74 File Offset: 0x00091E74
		[DebuggerNonUserCode]
		internal virtual TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F61 RID: 3937
		private readonly OpType m_opType;

		// Token: 0x04000F62 RID: 3938
		internal const int ArityVarying = -1;
	}
}
