using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200029F RID: 671
	[Serializable]
	internal class RouteOption : TSqlFragment
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x00165605 File Offset: 0x00163805
		// (set) Token: 0x060027C5 RID: 10181 RVA: 0x0016560D File Offset: 0x0016380D
		public RouteOptionKind OptionKind
		{
			get
			{
				return this._optionKind;
			}
			set
			{
				this._optionKind = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x00165616 File Offset: 0x00163816
		// (set) Token: 0x060027C7 RID: 10183 RVA: 0x0016561E File Offset: 0x0016381E
		public Literal Literal
		{
			get
			{
				return this._literal;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._literal = value;
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x0016562E File Offset: 0x0016382E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0016563A File Offset: 0x0016383A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Literal != null)
			{
				this.Literal.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BAE RID: 7086
		private RouteOptionKind _optionKind;

		// Token: 0x04001BAF RID: 7087
		private Literal _literal;
	}
}
