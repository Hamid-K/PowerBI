using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	internal class MultiPartIdentifier : TSqlFragment
	{
		// Token: 0x17000015 RID: 21
		public Identifier this[int index]
		{
			get
			{
				return this.Identifiers[index];
			}
			set
			{
				this.Identifiers[index] = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006145 File Offset: 0x00004345
		public int Count
		{
			get
			{
				return this.Identifiers.Count;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006152 File Offset: 0x00004352
		public IList<Identifier> Identifiers
		{
			get
			{
				return this._identifiers;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000615A File Offset: 0x0000435A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006168 File Offset: 0x00004368
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Identifiers.Count;
			while (i < count)
			{
				this.Identifiers[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x0400014E RID: 334
		private List<Identifier> _identifiers = new List<Identifier>();
	}
}
