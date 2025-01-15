using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003D7 RID: 983
	internal sealed class RelPropertyOp : ScalarOp
	{
		// Token: 0x06002EC8 RID: 11976 RVA: 0x00094FDA File Offset: 0x000931DA
		private RelPropertyOp()
			: base(OpType.RelProperty)
		{
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x00094FE4 File Offset: 0x000931E4
		internal RelPropertyOp(TypeUsage type, RelProperty property)
			: base(OpType.RelProperty, type)
		{
			this.m_property = property;
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x00094FF6 File Offset: 0x000931F6
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x00094FF9 File Offset: 0x000931F9
		public RelProperty PropertyInfo
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x00095001 File Offset: 0x00093201
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x0009500B File Offset: 0x0009320B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000FC6 RID: 4038
		private readonly RelProperty m_property;

		// Token: 0x04000FC7 RID: 4039
		internal static readonly RelPropertyOp Pattern = new RelPropertyOp();
	}
}
