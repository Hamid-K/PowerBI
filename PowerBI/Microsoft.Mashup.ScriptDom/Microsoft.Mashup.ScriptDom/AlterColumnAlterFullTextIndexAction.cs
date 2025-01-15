using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003F0 RID: 1008
	[Serializable]
	internal class AlterColumnAlterFullTextIndexAction : AlterFullTextIndexAction
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x0016DB61 File Offset: 0x0016BD61
		// (set) Token: 0x06002FDB RID: 12251 RVA: 0x0016DB69 File Offset: 0x0016BD69
		public FullTextIndexColumn Column
		{
			get
			{
				return this._column;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._column = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x0016DB79 File Offset: 0x0016BD79
		// (set) Token: 0x06002FDD RID: 12253 RVA: 0x0016DB81 File Offset: 0x0016BD81
		public bool WithNoPopulation
		{
			get
			{
				return this._withNoPopulation;
			}
			set
			{
				this._withNoPopulation = value;
			}
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x0016DB8A File Offset: 0x0016BD8A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x0016DB96 File Offset: 0x0016BD96
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Column != null)
			{
				this.Column.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E00 RID: 7680
		private FullTextIndexColumn _column;

		// Token: 0x04001E01 RID: 7681
		private bool _withNoPopulation;
	}
}
