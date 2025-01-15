using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000219 RID: 537
	[Serializable]
	internal class LeftFunctionCall : PrimaryExpression
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x0016210D File Offset: 0x0016030D
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x00162115 File Offset: 0x00160315
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x00162124 File Offset: 0x00160324
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

		// Token: 0x04001AD8 RID: 6872
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();
	}
}
