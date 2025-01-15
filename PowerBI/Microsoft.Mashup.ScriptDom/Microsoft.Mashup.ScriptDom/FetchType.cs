using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E4 RID: 740
	[Serializable]
	internal class FetchType : TSqlFragment
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x0016710E File Offset: 0x0016530E
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x00167116 File Offset: 0x00165316
		public FetchOrientation Orientation
		{
			get
			{
				return this._orientation;
			}
			set
			{
				this._orientation = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x0016711F File Offset: 0x0016531F
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x00167127 File Offset: 0x00165327
		public ScalarExpression RowOffset
		{
			get
			{
				return this._rowOffset;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._rowOffset = value;
			}
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x00167137 File Offset: 0x00165337
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00167143 File Offset: 0x00165343
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.RowOffset != null)
			{
				this.RowOffset.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C20 RID: 7200
		private FetchOrientation _orientation;

		// Token: 0x04001C21 RID: 7201
		private ScalarExpression _rowOffset;
	}
}
