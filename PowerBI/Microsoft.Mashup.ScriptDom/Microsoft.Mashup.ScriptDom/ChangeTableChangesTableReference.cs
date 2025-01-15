using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003CD RID: 973
	[Serializable]
	internal class ChangeTableChangesTableReference : TableReferenceWithAliasAndColumns
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x0016D0BB File Offset: 0x0016B2BB
		// (set) Token: 0x06002F1E RID: 12062 RVA: 0x0016D0C3 File Offset: 0x0016B2C3
		public SchemaObjectName Target
		{
			get
			{
				return this._target;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._target = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x0016D0D3 File Offset: 0x0016B2D3
		// (set) Token: 0x06002F20 RID: 12064 RVA: 0x0016D0DB File Offset: 0x0016B2DB
		public ValueExpression SinceVersion
		{
			get
			{
				return this._sinceVersion;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sinceVersion = value;
			}
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x0016D0EB File Offset: 0x0016B2EB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x0016D0F7 File Offset: 0x0016B2F7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Target != null)
			{
				this.Target.Accept(visitor);
			}
			if (this.SinceVersion != null)
			{
				this.SinceVersion.Accept(visitor);
			}
		}

		// Token: 0x04001DD3 RID: 7635
		private SchemaObjectName _target;

		// Token: 0x04001DD4 RID: 7636
		private ValueExpression _sinceVersion;
	}
}
