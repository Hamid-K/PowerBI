using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000306 RID: 774
	[Serializable]
	internal class SetStatisticsStatement : SetOnOffStatement
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x00167B92 File Offset: 0x00165D92
		// (set) Token: 0x06002A0B RID: 10763 RVA: 0x00167B9A File Offset: 0x00165D9A
		public SetStatisticsOptions Options
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = value;
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00167BA3 File Offset: 0x00165DA3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x00167BAF File Offset: 0x00165DAF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C4A RID: 7242
		private SetStatisticsOptions _options;
	}
}
