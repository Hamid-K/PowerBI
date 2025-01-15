using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000269 RID: 617
	[Serializable]
	internal class ColumnReferenceExpression : PrimaryExpression
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x00164273 File Offset: 0x00162473
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x0016427B File Offset: 0x0016247B
		public ColumnType ColumnType
		{
			get
			{
				return this._columnType;
			}
			set
			{
				this._columnType = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x00164284 File Offset: 0x00162484
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x0016428C File Offset: 0x0016248C
		public MultiPartIdentifier MultiPartIdentifier
		{
			get
			{
				return this._multiPartIdentifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._multiPartIdentifier = value;
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0016429C File Offset: 0x0016249C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x001642A8 File Offset: 0x001624A8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.MultiPartIdentifier != null)
			{
				this.MultiPartIdentifier.Accept(visitor);
			}
		}

		// Token: 0x04001B5C RID: 7004
		private ColumnType _columnType;

		// Token: 0x04001B5D RID: 7005
		private MultiPartIdentifier _multiPartIdentifier;
	}
}
