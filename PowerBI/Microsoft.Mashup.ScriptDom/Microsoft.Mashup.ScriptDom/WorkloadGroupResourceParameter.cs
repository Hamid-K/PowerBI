using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000460 RID: 1120
	[Serializable]
	internal class WorkloadGroupResourceParameter : WorkloadGroupParameter
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x0017008F File Offset: 0x0016E28F
		// (set) Token: 0x0600324E RID: 12878 RVA: 0x00170097 File Offset: 0x0016E297
		public Literal ParameterValue
		{
			get
			{
				return this._parameterValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._parameterValue = value;
			}
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x001700A7 File Offset: 0x0016E2A7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x001700B3 File Offset: 0x0016E2B3
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ParameterValue != null)
			{
				this.ParameterValue.Accept(visitor);
			}
		}

		// Token: 0x04001EA4 RID: 7844
		private Literal _parameterValue;
	}
}
