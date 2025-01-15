using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200023E RID: 574
	[Serializable]
	internal class UpdateSpecification : UpdateDeleteSpecificationBase
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x00162F52 File Offset: 0x00161152
		public IList<SetClause> SetClauses
		{
			get
			{
				return this._setClauses;
			}
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x00162F5A File Offset: 0x0016115A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x00162F68 File Offset: 0x00161168
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.SetClauses.Count;
			while (i < count)
			{
				this.SetClauses[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001B0B RID: 6923
		private List<SetClause> _setClauses = new List<SetClause>();
	}
}
