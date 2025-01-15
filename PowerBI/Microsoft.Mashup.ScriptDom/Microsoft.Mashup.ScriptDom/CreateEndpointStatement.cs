using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200037D RID: 893
	[Serializable]
	internal class CreateEndpointStatement : AlterCreateEndpointStatementBase, IAuthorization
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x0016AFDE File Offset: 0x001691DE
		// (set) Token: 0x06002D2E RID: 11566 RVA: 0x0016AFE6 File Offset: 0x001691E6
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

		// Token: 0x06002D2F RID: 11567 RVA: 0x0016AFF6 File Offset: 0x001691F6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x0016B004 File Offset: 0x00169204
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (base.Affinity != null)
			{
				base.Affinity.Accept(visitor);
			}
			int i = 0;
			int count = base.ProtocolOptions.Count;
			while (i < count)
			{
				base.ProtocolOptions[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = base.PayloadOptions.Count;
			while (j < count2)
			{
				base.PayloadOptions[j].Accept(visitor);
				j++;
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001D43 RID: 7491
		private Identifier _owner;
	}
}
