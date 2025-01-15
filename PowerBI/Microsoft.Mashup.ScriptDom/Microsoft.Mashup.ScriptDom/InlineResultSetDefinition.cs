using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	internal class InlineResultSetDefinition : ResultSetDefinition
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x0015EE7B File Offset: 0x0015D07B
		public IList<ResultColumnDefinition> ResultColumnDefinitions
		{
			get
			{
				return this._resultColumnDefinitions;
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0015EE83 File Offset: 0x0015D083
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0015EE90 File Offset: 0x0015D090
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.ResultColumnDefinitions.Count;
			while (i < count)
			{
				this.ResultColumnDefinitions[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A06 RID: 6662
		private List<ResultColumnDefinition> _resultColumnDefinitions = new List<ResultColumnDefinition>();
	}
}
