using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200045A RID: 1114
	[Serializable]
	internal class ResourcePoolAffinitySpecification : TSqlFragment
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x0016FEAF File Offset: 0x0016E0AF
		// (set) Token: 0x06003230 RID: 12848 RVA: 0x0016FEB7 File Offset: 0x0016E0B7
		public ResourcePoolAffinityType AffinityType
		{
			get
			{
				return this._affinityType;
			}
			set
			{
				this._affinityType = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x0016FEC0 File Offset: 0x0016E0C0
		// (set) Token: 0x06003232 RID: 12850 RVA: 0x0016FEC8 File Offset: 0x0016E0C8
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

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x0016FED8 File Offset: 0x0016E0D8
		// (set) Token: 0x06003234 RID: 12852 RVA: 0x0016FEE0 File Offset: 0x0016E0E0
		public bool IsAuto
		{
			get
			{
				return this._isAuto;
			}
			set
			{
				this._isAuto = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x0016FEE9 File Offset: 0x0016E0E9
		public IList<LiteralRange> PoolAffinityRanges
		{
			get
			{
				return this._poolAffinityRanges;
			}
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x0016FEF1 File Offset: 0x0016E0F1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x0016FF00 File Offset: 0x0016E100
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ParameterValue != null)
			{
				this.ParameterValue.Accept(visitor);
			}
			int i = 0;
			int count = this.PoolAffinityRanges.Count;
			while (i < count)
			{
				this.PoolAffinityRanges[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E9C RID: 7836
		private ResourcePoolAffinityType _affinityType;

		// Token: 0x04001E9D RID: 7837
		private Literal _parameterValue;

		// Token: 0x04001E9E RID: 7838
		private bool _isAuto;

		// Token: 0x04001E9F RID: 7839
		private List<LiteralRange> _poolAffinityRanges = new List<LiteralRange>();
	}
}
