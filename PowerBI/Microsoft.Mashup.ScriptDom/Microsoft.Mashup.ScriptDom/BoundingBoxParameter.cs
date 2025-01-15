using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000486 RID: 1158
	[Serializable]
	internal class BoundingBoxParameter : TSqlFragment
	{
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x00170E99 File Offset: 0x0016F099
		// (set) Token: 0x0600332C RID: 13100 RVA: 0x00170EA1 File Offset: 0x0016F0A1
		public BoundingBoxParameterType Parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				this._parameter = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x00170EAA File Offset: 0x0016F0AA
		// (set) Token: 0x0600332E RID: 13102 RVA: 0x00170EB2 File Offset: 0x0016F0B2
		public ScalarExpression Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x00170EC2 File Offset: 0x0016F0C2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x00170ECE File Offset: 0x0016F0CE
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EE1 RID: 7905
		private BoundingBoxParameterType _parameter;

		// Token: 0x04001EE2 RID: 7906
		private ScalarExpression _value;
	}
}
