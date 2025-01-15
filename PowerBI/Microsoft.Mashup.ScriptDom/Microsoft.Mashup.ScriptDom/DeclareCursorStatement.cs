using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D6 RID: 726
	[Serializable]
	internal class DeclareCursorStatement : TSqlStatement
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x00166BE7 File Offset: 0x00164DE7
		// (set) Token: 0x06002903 RID: 10499 RVA: 0x00166BEF File Offset: 0x00164DEF
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

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x00166BFF File Offset: 0x00164DFF
		// (set) Token: 0x06002905 RID: 10501 RVA: 0x00166C07 File Offset: 0x00164E07
		public CursorDefinition CursorDefinition
		{
			get
			{
				return this._cursorDefinition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._cursorDefinition = value;
			}
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00166C17 File Offset: 0x00164E17
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x00166C23 File Offset: 0x00164E23
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.CursorDefinition != null)
			{
				this.CursorDefinition.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C08 RID: 7176
		private Identifier _name;

		// Token: 0x04001C09 RID: 7177
		private CursorDefinition _cursorDefinition;
	}
}
