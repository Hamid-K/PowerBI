using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000256 RID: 598
	[Serializable]
	internal class Privilege80 : TSqlFragment
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x00163C3A File Offset: 0x00161E3A
		public IList<Identifier> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x00163C42 File Offset: 0x00161E42
		// (set) Token: 0x0600262F RID: 9775 RVA: 0x00163C4A File Offset: 0x00161E4A
		public PrivilegeType80 PrivilegeType80
		{
			get
			{
				return this._privilegeType80;
			}
			set
			{
				this._privilegeType80 = value;
			}
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x00163C53 File Offset: 0x00161E53
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x00163C60 File Offset: 0x00161E60
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B43 RID: 6979
		private List<Identifier> _columns = new List<Identifier>();

		// Token: 0x04001B44 RID: 6980
		private PrivilegeType80 _privilegeType80;
	}
}
