using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001F3 RID: 499
	[Serializable]
	internal class XmlForClause : ForClause
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x00160C98 File Offset: 0x0015EE98
		public IList<XmlForClauseOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x00160CA0 File Offset: 0x0015EEA0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x00160CAC File Offset: 0x0015EEAC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A80 RID: 6784
		private List<XmlForClauseOption> _options = new List<XmlForClauseOption>();
	}
}
