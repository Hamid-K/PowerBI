using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002C6 RID: 710
	[Serializable]
	internal class RenameAlterRoleAction : AlterRoleAction
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x00166637 File Offset: 0x00164837
		// (set) Token: 0x060028B2 RID: 10418 RVA: 0x0016663F File Offset: 0x0016483F
		public Identifier NewName
		{
			get
			{
				return this._newName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._newName = value;
			}
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x0016664F File Offset: 0x0016484F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x0016665B File Offset: 0x0016485B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.NewName != null)
			{
				this.NewName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BF4 RID: 7156
		private Identifier _newName;
	}
}
