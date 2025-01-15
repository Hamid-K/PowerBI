using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000466 RID: 1126
	[Serializable]
	internal class BrokerPriorityParameter : TSqlFragment
	{
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x001701E5 File Offset: 0x0016E3E5
		// (set) Token: 0x06003266 RID: 12902 RVA: 0x001701ED File Offset: 0x0016E3ED
		public BrokerPriorityParameterSpecialType IsDefaultOrAny
		{
			get
			{
				return this._isDefaultOrAny;
			}
			set
			{
				this._isDefaultOrAny = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x001701F6 File Offset: 0x0016E3F6
		// (set) Token: 0x06003268 RID: 12904 RVA: 0x001701FE File Offset: 0x0016E3FE
		public BrokerPriorityParameterType ParameterType
		{
			get
			{
				return this._parameterType;
			}
			set
			{
				this._parameterType = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x00170207 File Offset: 0x0016E407
		// (set) Token: 0x0600326A RID: 12906 RVA: 0x0017020F File Offset: 0x0016E40F
		public IdentifierOrValueExpression ParameterValue
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

		// Token: 0x0600326B RID: 12907 RVA: 0x0017021F File Offset: 0x0016E41F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x0017022B File Offset: 0x0016E42B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ParameterValue != null)
			{
				this.ParameterValue.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EA8 RID: 7848
		private BrokerPriorityParameterSpecialType _isDefaultOrAny;

		// Token: 0x04001EA9 RID: 7849
		private BrokerPriorityParameterType _parameterType;

		// Token: 0x04001EAA RID: 7850
		private IdentifierOrValueExpression _parameterValue;
	}
}
