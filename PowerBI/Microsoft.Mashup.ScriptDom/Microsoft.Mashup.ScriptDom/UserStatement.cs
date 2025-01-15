using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002CE RID: 718
	[Serializable]
	internal abstract class UserStatement : TSqlStatement
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x00166800 File Offset: 0x00164A00
		// (set) Token: 0x060028D5 RID: 10453 RVA: 0x00166808 File Offset: 0x00164A08
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

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x00166818 File Offset: 0x00164A18
		public IList<PrincipalOption> UserOptions
		{
			get
			{
				return this._userOptions;
			}
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x00166820 File Offset: 0x00164A20
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.UserOptions.Count;
			while (i < count)
			{
				this.UserOptions[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BFA RID: 7162
		private Identifier _name;

		// Token: 0x04001BFB RID: 7163
		private List<PrincipalOption> _userOptions = new List<PrincipalOption>();
	}
}
