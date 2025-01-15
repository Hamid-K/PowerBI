using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000459 RID: 1113
	[Serializable]
	internal class ResourcePoolParameter : TSqlFragment
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x0016FE29 File Offset: 0x0016E029
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x0016FE31 File Offset: 0x0016E031
		public ResourcePoolParameterType ParameterType
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

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x0016FE3A File Offset: 0x0016E03A
		// (set) Token: 0x06003229 RID: 12841 RVA: 0x0016FE42 File Offset: 0x0016E042
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

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x0016FE52 File Offset: 0x0016E052
		// (set) Token: 0x0600322B RID: 12843 RVA: 0x0016FE5A File Offset: 0x0016E05A
		public ResourcePoolAffinitySpecification AffinitySpecification
		{
			get
			{
				return this._affinitySpecification;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._affinitySpecification = value;
			}
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x0016FE6A File Offset: 0x0016E06A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0016FE76 File Offset: 0x0016E076
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ParameterValue != null)
			{
				this.ParameterValue.Accept(visitor);
			}
			if (this.AffinitySpecification != null)
			{
				this.AffinitySpecification.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E99 RID: 7833
		private ResourcePoolParameterType _parameterType;

		// Token: 0x04001E9A RID: 7834
		private Literal _parameterValue;

		// Token: 0x04001E9B RID: 7835
		private ResourcePoolAffinitySpecification _affinitySpecification;
	}
}
