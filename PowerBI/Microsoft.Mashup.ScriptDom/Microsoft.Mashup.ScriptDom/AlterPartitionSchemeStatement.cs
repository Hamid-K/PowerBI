using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E8 RID: 1000
	[Serializable]
	internal class AlterPartitionSchemeStatement : TSqlStatement
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x0016D893 File Offset: 0x0016BA93
		// (set) Token: 0x06002FAC RID: 12204 RVA: 0x0016D89B File Offset: 0x0016BA9B
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x0016D8AB File Offset: 0x0016BAAB
		// (set) Token: 0x06002FAE RID: 12206 RVA: 0x0016D8B3 File Offset: 0x0016BAB3
		public IdentifierOrValueExpression FileGroup
		{
			get
			{
				return this._fileGroup;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileGroup = value;
			}
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x0016D8C3 File Offset: 0x0016BAC3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x0016D8CF File Offset: 0x0016BACF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.FileGroup != null)
			{
				this.FileGroup.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DF3 RID: 7667
		private Identifier _name;

		// Token: 0x04001DF4 RID: 7668
		private IdentifierOrValueExpression _fileGroup;
	}
}
