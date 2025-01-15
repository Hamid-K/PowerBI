using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002C2 RID: 706
	[Serializable]
	internal abstract class RoleStatement : TSqlStatement
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060028A1 RID: 10401 RVA: 0x00166557 File Offset: 0x00164757
		// (set) Token: 0x060028A2 RID: 10402 RVA: 0x0016655F File Offset: 0x0016475F
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

		// Token: 0x060028A3 RID: 10403 RVA: 0x0016656F File Offset: 0x0016476F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BF1 RID: 7153
		private Identifier _name;
	}
}
