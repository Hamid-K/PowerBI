using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D1 RID: 721
	[Serializable]
	internal class StatisticsOption : TSqlFragment
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x00166930 File Offset: 0x00164B30
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x00166938 File Offset: 0x00164B38
		public StatisticsOptionKind OptionKind
		{
			get
			{
				return this._optionKind;
			}
			set
			{
				this._optionKind = value;
			}
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x00166941 File Offset: 0x00164B41
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0016694D File Offset: 0x00164B4D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BFD RID: 7165
		private StatisticsOptionKind _optionKind;
	}
}
