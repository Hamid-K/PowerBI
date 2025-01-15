using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007DE RID: 2014
	internal class ReferenceFeatureCalculationContextInputs : FeatureCalculationContextInputs
	{
		// Token: 0x06002AEB RID: 10987 RVA: 0x0007844B File Offset: 0x0007664B
		internal ReferenceFeatureCalculationContextInputs(IReadOnlyList<State> referenceSpecInputs, IReadOnlyList<State> referenceAdditionalInputs)
		{
			this._referenceSpecInputs = referenceSpecInputs;
			this._referenceAdditionalInputs = referenceAdditionalInputs;
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x00078461 File Offset: 0x00076661
		internal override IReadOnlyList<State> ReferenceSpecInputs
		{
			get
			{
				return this._referenceSpecInputs;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x00078469 File Offset: 0x00076669
		internal override IReadOnlyList<State> ReferenceAdditionalInputs
		{
			get
			{
				return this._referenceAdditionalInputs;
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x00078461 File Offset: 0x00076661
		internal override IReadOnlyList<State> ComputeSpecInputs()
		{
			return this._referenceSpecInputs;
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x00078461 File Offset: 0x00076661
		internal override IReadOnlyList<State> MaterializeSpecInputs()
		{
			return this._referenceSpecInputs;
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x00004FAE File Offset: 0x000031AE
		internal override FeatureCalculationContextInputs GetBaseContextInputsForTransform()
		{
			return this;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x00078474 File Offset: 0x00076674
		public bool Equals(ReferenceFeatureCalculationContextInputs other)
		{
			return other != null && (this == other || ((this.ReferenceSpecInputs == other.ReferenceSpecInputs || this.ReferenceSpecInputs.SequenceEqual(other.ReferenceSpecInputs)) && (this.ReferenceAdditionalInputs == other.ReferenceAdditionalInputs || this.ReferenceAdditionalInputs.SequenceEqual(other.ReferenceAdditionalInputs))));
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000784D4 File Offset: 0x000766D4
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			ReferenceFeatureCalculationContextInputs referenceFeatureCalculationContextInputs = other as ReferenceFeatureCalculationContextInputs;
			return referenceFeatureCalculationContextInputs != null && this.Equals(referenceFeatureCalculationContextInputs);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000784FF File Offset: 0x000766FF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x00078507 File Offset: 0x00076707
		protected override int ComputeHashCode()
		{
			return (this.ReferenceSpecInputs.OrderDependentHashCode<State>() * 25117) ^ (this.ReferenceAdditionalInputs.OrderDependentHashCode<State>() * 27127);
		}

		// Token: 0x040014A7 RID: 5287
		private readonly IReadOnlyList<State> _referenceSpecInputs;

		// Token: 0x040014A8 RID: 5288
		private readonly IReadOnlyList<State> _referenceAdditionalInputs;
	}
}
