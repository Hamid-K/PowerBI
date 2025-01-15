using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000290 RID: 656
	[Serializable]
	internal class AlterTableDropTableElement : TSqlFragment
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x00165017 File Offset: 0x00163217
		// (set) Token: 0x06002769 RID: 10089 RVA: 0x0016501F File Offset: 0x0016321F
		public TableElementType TableElementType
		{
			get
			{
				return this._tableElementType;
			}
			set
			{
				this._tableElementType = value;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x00165028 File Offset: 0x00163228
		// (set) Token: 0x0600276B RID: 10091 RVA: 0x00165030 File Offset: 0x00163230
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x00165040 File Offset: 0x00163240
		public IList<DropClusteredConstraintOption> DropClusteredConstraintOptions
		{
			get
			{
				return this._dropClusteredConstraintOptions;
			}
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00165048 File Offset: 0x00163248
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x00165054 File Offset: 0x00163254
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.DropClusteredConstraintOptions.Count;
			while (i < count)
			{
				this.DropClusteredConstraintOptions[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B94 RID: 7060
		private TableElementType _tableElementType;

		// Token: 0x04001B95 RID: 7061
		private Identifier _name;

		// Token: 0x04001B96 RID: 7062
		private List<DropClusteredConstraintOption> _dropClusteredConstraintOptions = new List<DropClusteredConstraintOption>();
	}
}
