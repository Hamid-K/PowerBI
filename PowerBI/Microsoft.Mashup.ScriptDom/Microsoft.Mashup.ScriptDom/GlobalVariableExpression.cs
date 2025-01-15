using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000266 RID: 614
	[Serializable]
	internal class GlobalVariableExpression : ValueExpression
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x00164187 File Offset: 0x00162387
		// (set) Token: 0x06002682 RID: 9858 RVA: 0x0016418F File Offset: 0x0016238F
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x00164198 File Offset: 0x00162398
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x001641A4 File Offset: 0x001623A4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B58 RID: 7000
		private string _name;
	}
}
