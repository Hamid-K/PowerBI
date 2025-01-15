using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001E3 RID: 483
	[Serializable]
	internal class IndexTableHint : TableHint
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x001605A7 File Offset: 0x0015E7A7
		public IList<IdentifierOrValueExpression> IndexValues
		{
			get
			{
				return this._indexValues;
			}
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x001605AF File Offset: 0x0015E7AF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x001605BC File Offset: 0x0015E7BC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.IndexValues.Count;
			while (i < count)
			{
				this.IndexValues[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A62 RID: 6754
		private List<IdentifierOrValueExpression> _indexValues = new List<IdentifierOrValueExpression>();
	}
}
