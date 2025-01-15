using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003D4 RID: 980
	[Serializable]
	internal class VariableTableReference : TableReferenceWithAlias
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x0016D48D File Offset: 0x0016B68D
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x0016D495 File Offset: 0x0016B695
		public VariableReference Variable
		{
			get
			{
				return this._variable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._variable = value;
			}
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x0016D4A5 File Offset: 0x0016B6A5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x0016D4B1 File Offset: 0x0016B6B1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Variable != null)
			{
				this.Variable.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DE7 RID: 7655
		private VariableReference _variable;
	}
}
