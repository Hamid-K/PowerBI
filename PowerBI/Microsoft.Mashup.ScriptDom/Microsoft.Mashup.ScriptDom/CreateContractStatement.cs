using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000373 RID: 883
	[Serializable]
	internal class CreateContractStatement : TSqlStatement, IAuthorization
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x0016AAB4 File Offset: 0x00168CB4
		// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x0016AABC File Offset: 0x00168CBC
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

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x0016AACC File Offset: 0x00168CCC
		public IList<ContractMessage> Messages
		{
			get
			{
				return this._messages;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x0016AAD4 File Offset: 0x00168CD4
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x0016AADC File Offset: 0x00168CDC
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

		// Token: 0x06002CEB RID: 11499 RVA: 0x0016AAEC File Offset: 0x00168CEC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x0016AAF8 File Offset: 0x00168CF8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.Messages.Count;
			while (i < count)
			{
				this.Messages[i].Accept(visitor);
				i++;
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D2B RID: 7467
		private Identifier _name;

		// Token: 0x04001D2C RID: 7468
		private List<ContractMessage> _messages = new List<ContractMessage>();

		// Token: 0x04001D2D RID: 7469
		private Identifier _owner;
	}
}
