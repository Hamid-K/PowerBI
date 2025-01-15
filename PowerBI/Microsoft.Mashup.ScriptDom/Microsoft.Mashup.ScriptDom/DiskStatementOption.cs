using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020004A1 RID: 1185
	[Serializable]
	internal class DiskStatementOption : TSqlFragment
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x001717E5 File Offset: 0x0016F9E5
		// (set) Token: 0x060033C4 RID: 13252 RVA: 0x001717ED File Offset: 0x0016F9ED
		public DiskStatementOptionKind OptionKind
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

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x001717F6 File Offset: 0x0016F9F6
		// (set) Token: 0x060033C6 RID: 13254 RVA: 0x001717FE File Offset: 0x0016F9FE
		public IdentifierOrValueExpression Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x0017180E File Offset: 0x0016FA0E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x0017181A File Offset: 0x0016FA1A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001F0A RID: 7946
		private DiskStatementOptionKind _optionKind;

		// Token: 0x04001F0B RID: 7947
		private IdentifierOrValueExpression _value;
	}
}
