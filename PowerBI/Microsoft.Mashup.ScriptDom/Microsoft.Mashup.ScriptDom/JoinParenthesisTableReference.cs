using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C0 RID: 960
	[Serializable]
	internal class JoinParenthesisTableReference : TableReference
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x0016CB10 File Offset: 0x0016AD10
		// (set) Token: 0x06002ECA RID: 11978 RVA: 0x0016CB18 File Offset: 0x0016AD18
		public TableReference Join
		{
			get
			{
				return this._join;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._join = value;
			}
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x0016CB28 File Offset: 0x0016AD28
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0016CB34 File Offset: 0x0016AD34
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Join != null)
			{
				this.Join.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DBA RID: 7610
		private TableReference _join;
	}
}
