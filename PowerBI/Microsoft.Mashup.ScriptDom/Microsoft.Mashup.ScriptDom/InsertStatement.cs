using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200023B RID: 571
	[Serializable]
	internal class InsertStatement : DataModificationStatement
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x00162E1E File Offset: 0x0016101E
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x00162E26 File Offset: 0x00161026
		public InsertSpecification InsertSpecification
		{
			get
			{
				return this._insertSpecification;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._insertSpecification = value;
			}
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00162E36 File Offset: 0x00161036
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x00162E42 File Offset: 0x00161042
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.InsertSpecification != null)
			{
				this.InsertSpecification.Accept(visitor);
			}
		}

		// Token: 0x04001B06 RID: 6918
		private InsertSpecification _insertSpecification;
	}
}
