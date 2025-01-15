using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200025E RID: 606
	[Serializable]
	internal class SelectInsertSource : InsertSource
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x00163EE5 File Offset: 0x001620E5
		// (set) Token: 0x06002657 RID: 9815 RVA: 0x00163EED File Offset: 0x001620ED
		public QueryExpression Select
		{
			get
			{
				return this._select;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._select = value;
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00163EFD File Offset: 0x001620FD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x00163F09 File Offset: 0x00162109
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Select != null)
			{
				this.Select.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B4E RID: 6990
		private QueryExpression _select;
	}
}
