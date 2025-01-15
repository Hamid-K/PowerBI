using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200024B RID: 587
	[Serializable]
	internal class Permission : TSqlFragment
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x001637D7 File Offset: 0x001619D7
		public IList<Identifier> Identifiers
		{
			get
			{
				return this._identifiers;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x001637DF File Offset: 0x001619DF
		public IList<Identifier> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x001637E7 File Offset: 0x001619E7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x001637F4 File Offset: 0x001619F4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Identifiers.Count;
			while (i < count)
			{
				this.Identifiers[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.Columns.Count;
			while (j < count2)
			{
				this.Columns[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B2E RID: 6958
		private List<Identifier> _identifiers = new List<Identifier>();

		// Token: 0x04001B2F RID: 6959
		private List<Identifier> _columns = new List<Identifier>();
	}
}
