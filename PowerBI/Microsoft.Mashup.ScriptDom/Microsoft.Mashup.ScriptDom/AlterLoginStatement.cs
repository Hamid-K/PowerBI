using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000402 RID: 1026
	[Serializable]
	internal abstract class AlterLoginStatement : TSqlStatement
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x0016E24F File Offset: 0x0016C44F
		// (set) Token: 0x0600304D RID: 12365 RVA: 0x0016E257 File Offset: 0x0016C457
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

		// Token: 0x0600304E RID: 12366 RVA: 0x0016E267 File Offset: 0x0016C467
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E20 RID: 7712
		private Identifier _name;
	}
}
