using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200039C RID: 924
	[Serializable]
	internal abstract class FullTextCatalogStatement : TSqlStatement
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x0016BA97 File Offset: 0x00169C97
		// (set) Token: 0x06002DEB RID: 11755 RVA: 0x0016BA9F File Offset: 0x00169C9F
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

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x0016BAAF File Offset: 0x00169CAF
		public IList<FullTextCatalogOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x0016BAB8 File Offset: 0x00169CB8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D76 RID: 7542
		private Identifier _name;

		// Token: 0x04001D77 RID: 7543
		private List<FullTextCatalogOption> _options = new List<FullTextCatalogOption>();
	}
}
