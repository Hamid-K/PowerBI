using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000456 RID: 1110
	[Serializable]
	internal class AlterDatabaseEncryptionKeyStatement : DatabaseEncryptionKeyStatement
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x0016FD4A File Offset: 0x0016DF4A
		// (set) Token: 0x06003219 RID: 12825 RVA: 0x0016FD52 File Offset: 0x0016DF52
		public bool Regenerate
		{
			get
			{
				return this._regenerate;
			}
			set
			{
				this._regenerate = value;
			}
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x0016FD5B File Offset: 0x0016DF5B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x0016FD67 File Offset: 0x0016DF67
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E96 RID: 7830
		private bool _regenerate;
	}
}
