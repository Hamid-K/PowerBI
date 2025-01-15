using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002D7 RID: 727
	[Serializable]
	internal class CursorDefinition : TSqlFragment
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x00166C5C File Offset: 0x00164E5C
		public IList<CursorOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x00166C64 File Offset: 0x00164E64
		// (set) Token: 0x0600290B RID: 10507 RVA: 0x00166C6C File Offset: 0x00164E6C
		public SelectStatement Select
		{
			get
			{
				return this._select;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._select = value;
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00166C7C File Offset: 0x00164E7C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00166C88 File Offset: 0x00164E88
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			if (this.Select != null)
			{
				this.Select.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C0A RID: 7178
		private List<CursorOption> _options = new List<CursorOption>();

		// Token: 0x04001C0B RID: 7179
		private SelectStatement _select;
	}
}
