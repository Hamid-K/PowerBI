using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000369 RID: 873
	[Serializable]
	internal class CreateRemoteServiceBindingStatement : RemoteServiceBindingStatementBase, IAuthorization
	{
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x0016A5FE File Offset: 0x001687FE
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x0016A606 File Offset: 0x00168806
		public Literal Service
		{
			get
			{
				return this._service;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._service = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x0016A616 File Offset: 0x00168816
		// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x0016A61E File Offset: 0x0016881E
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

		// Token: 0x06002CAA RID: 11434 RVA: 0x0016A62E File Offset: 0x0016882E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x0016A63C File Offset: 0x0016883C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (this.Service != null)
			{
				this.Service.Accept(visitor);
			}
			int i = 0;
			int count = base.Options.Count;
			while (i < count)
			{
				base.Options[i].Accept(visitor);
				i++;
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001D18 RID: 7448
		private Literal _service;

		// Token: 0x04001D19 RID: 7449
		private Identifier _owner;
	}
}
