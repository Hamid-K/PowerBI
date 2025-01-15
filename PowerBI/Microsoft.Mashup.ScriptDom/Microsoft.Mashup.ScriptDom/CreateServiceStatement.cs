using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A2 RID: 930
	[Serializable]
	internal class CreateServiceStatement : AlterCreateServiceStatementBase, IAuthorization
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x0016BD85 File Offset: 0x00169F85
		// (set) Token: 0x06002E10 RID: 11792 RVA: 0x0016BD8D File Offset: 0x00169F8D
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

		// Token: 0x06002E11 RID: 11793 RVA: 0x0016BD9D File Offset: 0x00169F9D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x0016BDAC File Offset: 0x00169FAC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (base.QueueName != null)
			{
				base.QueueName.Accept(visitor);
			}
			int i = 0;
			int count = base.ServiceContracts.Count;
			while (i < count)
			{
				base.ServiceContracts[i].Accept(visitor);
				i++;
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001D82 RID: 7554
		private Identifier _owner;
	}
}
