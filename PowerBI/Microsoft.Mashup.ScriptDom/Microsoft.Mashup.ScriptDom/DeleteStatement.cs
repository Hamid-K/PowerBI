using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000237 RID: 567
	[Serializable]
	internal class DeleteStatement : DataModificationStatement
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x00162C83 File Offset: 0x00160E83
		// (set) Token: 0x06002558 RID: 9560 RVA: 0x00162C8B File Offset: 0x00160E8B
		public DeleteSpecification DeleteSpecification
		{
			get
			{
				return this._deleteSpecification;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._deleteSpecification = value;
			}
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x00162C9B File Offset: 0x00160E9B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00162CA7 File Offset: 0x00160EA7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.DeleteSpecification != null)
			{
				this.DeleteSpecification.Accept(visitor);
			}
		}

		// Token: 0x04001AFF RID: 6911
		private DeleteSpecification _deleteSpecification;
	}
}
