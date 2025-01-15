using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000265 RID: 613
	[Serializable]
	internal class VariableReference : ValueExpression
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x00164159 File Offset: 0x00162359
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x00164161 File Offset: 0x00162361
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

		// Token: 0x0600267E RID: 9854 RVA: 0x0016416A File Offset: 0x0016236A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x00164176 File Offset: 0x00162376
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B57 RID: 6999
		private string _name;
	}
}
