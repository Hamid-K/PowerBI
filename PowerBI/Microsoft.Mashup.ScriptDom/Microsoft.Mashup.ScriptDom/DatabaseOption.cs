using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000326 RID: 806
	[Serializable]
	internal class DatabaseOption : TSqlFragment
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x001687FD File Offset: 0x001669FD
		// (set) Token: 0x06002AD9 RID: 10969 RVA: 0x00168805 File Offset: 0x00166A05
		public DatabaseOptionKind OptionKind
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

		// Token: 0x06002ADA RID: 10970 RVA: 0x0016880E File Offset: 0x00166A0E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0016881A File Offset: 0x00166A1A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C86 RID: 7302
		private DatabaseOptionKind _optionKind;
	}
}
