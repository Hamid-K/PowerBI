using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000379 RID: 889
	[Serializable]
	internal class CreateMessageTypeStatement : MessageTypeStatementBase, IAuthorization
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x0016AD4C File Offset: 0x00168F4C
		// (set) Token: 0x06002D0E RID: 11534 RVA: 0x0016AD54 File Offset: 0x00168F54
		public Identifier Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._owner = value;
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x0016AD64 File Offset: 0x00168F64
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0016AD70 File Offset: 0x00168F70
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (base.XmlSchemaCollectionName != null)
			{
				base.XmlSchemaCollectionName.Accept(visitor);
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001D37 RID: 7479
		private Identifier _owner;
	}
}
