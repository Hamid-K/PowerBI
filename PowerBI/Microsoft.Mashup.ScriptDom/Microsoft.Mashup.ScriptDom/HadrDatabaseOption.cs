using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000329 RID: 809
	[Serializable]
	internal class HadrDatabaseOption : DatabaseOption
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x00168887 File Offset: 0x00166A87
		// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x0016888F File Offset: 0x00166A8F
		public HadrDatabaseOptionKind HadrOption
		{
			get
			{
				return this._hadrOption;
			}
			set
			{
				this._hadrOption = value;
			}
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x00168898 File Offset: 0x00166A98
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x001688A4 File Offset: 0x00166AA4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C89 RID: 7305
		private HadrDatabaseOptionKind _hadrOption;
	}
}
