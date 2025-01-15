using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B3 RID: 435
	[Serializable]
	internal class ExecutableStringList : ExecutableEntity
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0015F29B File Offset: 0x0015D49B
		public IList<ValueExpression> Strings
		{
			get
			{
				return this._strings;
			}
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0015F2A3 File Offset: 0x0015D4A3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0015F2B0 File Offset: 0x0015D4B0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Strings.Count;
			while (i < count)
			{
				this.Strings[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A18 RID: 6680
		private List<ValueExpression> _strings = new List<ValueExpression>();
	}
}
