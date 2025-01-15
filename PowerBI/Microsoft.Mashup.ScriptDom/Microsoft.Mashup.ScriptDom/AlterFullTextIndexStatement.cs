using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E9 RID: 1001
	[Serializable]
	internal class AlterFullTextIndexStatement : TSqlStatement
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x0016D908 File Offset: 0x0016BB08
		// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x0016D910 File Offset: 0x0016BB10
		public SchemaObjectName OnName
		{
			get
			{
				return this._onName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._onName = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x0016D920 File Offset: 0x0016BB20
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x0016D928 File Offset: 0x0016BB28
		public AlterFullTextIndexAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._action = value;
			}
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x0016D938 File Offset: 0x0016BB38
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x0016D944 File Offset: 0x0016BB44
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.OnName != null)
			{
				this.OnName.Accept(visitor);
			}
			if (this.Action != null)
			{
				this.Action.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DF5 RID: 7669
		private SchemaObjectName _onName;

		// Token: 0x04001DF6 RID: 7670
		private AlterFullTextIndexAction _action;
	}
}
