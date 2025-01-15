using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000337 RID: 823
	[Serializable]
	internal class ChangeRetentionChangeTrackingOptionDetail : ChangeTrackingOptionDetail
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x00168C38 File Offset: 0x00166E38
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x00168C40 File Offset: 0x00166E40
		public Literal RetentionPeriod
		{
			get
			{
				return this._retentionPeriod;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._retentionPeriod = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x00168C50 File Offset: 0x00166E50
		// (set) Token: 0x06002B36 RID: 11062 RVA: 0x00168C58 File Offset: 0x00166E58
		public TimeUnit Unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				this._unit = value;
			}
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x00168C61 File Offset: 0x00166E61
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x00168C6D File Offset: 0x00166E6D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.RetentionPeriod != null)
			{
				this.RetentionPeriod.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C9B RID: 7323
		private Literal _retentionPeriod;

		// Token: 0x04001C9C RID: 7324
		private TimeUnit _unit;
	}
}
