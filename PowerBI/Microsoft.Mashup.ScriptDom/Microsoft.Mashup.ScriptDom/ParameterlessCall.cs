using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200021D RID: 541
	[Serializable]
	internal class ParameterlessCall : PrimaryExpression
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x0016235D File Offset: 0x0016055D
		// (set) Token: 0x060024D0 RID: 9424 RVA: 0x00162365 File Offset: 0x00160565
		public ParameterlessCallType ParameterlessCallType
		{
			get
			{
				return this._parameterlessCallType;
			}
			set
			{
				this._parameterlessCallType = value;
			}
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0016236E File Offset: 0x0016056E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0016237A File Offset: 0x0016057A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AE0 RID: 6880
		private ParameterlessCallType _parameterlessCallType;
	}
}
