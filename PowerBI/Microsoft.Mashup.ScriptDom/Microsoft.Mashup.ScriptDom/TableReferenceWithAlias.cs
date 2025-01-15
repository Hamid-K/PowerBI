using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001DE RID: 478
	[Serializable]
	internal abstract class TableReferenceWithAlias : TableReference
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x0016038E File Offset: 0x0015E58E
		// (set) Token: 0x0600232A RID: 9002 RVA: 0x00160396 File Offset: 0x0015E596
		public Identifier Alias
		{
			get
			{
				return this._alias;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._alias = value;
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x001603A6 File Offset: 0x0015E5A6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Alias != null)
			{
				this.Alias.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A5A RID: 6746
		private Identifier _alias;
	}
}
