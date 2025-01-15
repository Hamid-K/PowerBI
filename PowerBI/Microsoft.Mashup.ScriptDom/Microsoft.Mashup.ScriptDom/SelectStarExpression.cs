using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003CA RID: 970
	[Serializable]
	internal class SelectStarExpression : SelectElement
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x0016CFA3 File Offset: 0x0016B1A3
		// (set) Token: 0x06002F0B RID: 12043 RVA: 0x0016CFAB File Offset: 0x0016B1AB
		public MultiPartIdentifier Qualifier
		{
			get
			{
				return this._qualifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._qualifier = value;
			}
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x0016CFBB File Offset: 0x0016B1BB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x0016CFC7 File Offset: 0x0016B1C7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Qualifier != null)
			{
				this.Qualifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DCE RID: 7630
		private MultiPartIdentifier _qualifier;
	}
}
