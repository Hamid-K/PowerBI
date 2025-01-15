using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200021A RID: 538
	[Serializable]
	internal class RightFunctionCall : PrimaryExpression
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x00162175 File Offset: 0x00160375
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0016217D File Offset: 0x0016037D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0016218C File Offset: 0x0016038C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001AD9 RID: 6873
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();
	}
}
