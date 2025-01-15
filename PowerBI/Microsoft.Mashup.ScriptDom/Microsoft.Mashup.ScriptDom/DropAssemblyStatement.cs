using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003DA RID: 986
	[Serializable]
	internal class DropAssemblyStatement : DropObjectsStatement
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x0016D609 File Offset: 0x0016B809
		// (set) Token: 0x06002F72 RID: 12146 RVA: 0x0016D611 File Offset: 0x0016B811
		public bool WithNoDependents
		{
			get
			{
				return this._withNoDependents;
			}
			set
			{
				this._withNoDependents = value;
			}
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x0016D61A File Offset: 0x0016B81A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x0016D626 File Offset: 0x0016B826
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DEB RID: 7659
		private bool _withNoDependents;
	}
}
