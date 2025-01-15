using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E7 RID: 999
	[Serializable]
	internal class AlterPartitionFunctionStatement : TSqlStatement
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x0016D80D File Offset: 0x0016BA0D
		// (set) Token: 0x06002FA3 RID: 12195 RVA: 0x0016D815 File Offset: 0x0016BA15
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x0016D825 File Offset: 0x0016BA25
		// (set) Token: 0x06002FA5 RID: 12197 RVA: 0x0016D82D File Offset: 0x0016BA2D
		public bool IsSplit
		{
			get
			{
				return this._isSplit;
			}
			set
			{
				this._isSplit = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x0016D836 File Offset: 0x0016BA36
		// (set) Token: 0x06002FA7 RID: 12199 RVA: 0x0016D83E File Offset: 0x0016BA3E
		public ScalarExpression Boundary
		{
			get
			{
				return this._boundary;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._boundary = value;
			}
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x0016D84E File Offset: 0x0016BA4E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x0016D85A File Offset: 0x0016BA5A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Boundary != null)
			{
				this.Boundary.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DF0 RID: 7664
		private Identifier _name;

		// Token: 0x04001DF1 RID: 7665
		private bool _isSplit;

		// Token: 0x04001DF2 RID: 7666
		private ScalarExpression _boundary;
	}
}
