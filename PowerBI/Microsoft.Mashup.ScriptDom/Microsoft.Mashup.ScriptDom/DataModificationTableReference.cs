using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003CC RID: 972
	[Serializable]
	internal class DataModificationTableReference : TableReferenceWithAliasAndColumns
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x0016D072 File Offset: 0x0016B272
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x0016D07A File Offset: 0x0016B27A
		public DataModificationSpecification DataModificationSpecification
		{
			get
			{
				return this._dataModificationSpecification;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._dataModificationSpecification = value;
			}
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x0016D08A File Offset: 0x0016B28A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x0016D096 File Offset: 0x0016B296
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.DataModificationSpecification != null)
			{
				this.DataModificationSpecification.Accept(visitor);
			}
		}

		// Token: 0x04001DD2 RID: 7634
		private DataModificationSpecification _dataModificationSpecification;
	}
}
