using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007CE RID: 1998
	internal abstract class FeatureCalculationContextInputs
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002A8C RID: 10892
		internal abstract IReadOnlyList<State> ReferenceSpecInputs { get; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002A8D RID: 10893
		internal abstract IReadOnlyList<State> ReferenceAdditionalInputs { get; }

		// Token: 0x06002A8E RID: 10894
		internal abstract IReadOnlyList<State> ComputeSpecInputs();

		// Token: 0x06002A8F RID: 10895
		internal abstract IReadOnlyList<State> MaterializeSpecInputs();

		// Token: 0x06002A90 RID: 10896
		internal abstract FeatureCalculationContextInputs GetBaseContextInputsForTransform();

		// Token: 0x06002A91 RID: 10897
		public abstract override bool Equals(object obj);

		// Token: 0x06002A92 RID: 10898 RVA: 0x0007773E File Offset: 0x0007593E
		internal bool EqualsOnComputedInputs(FeatureCalculationContextInputs other)
		{
			return other == this || (other != null && this.ComputeSpecInputs().SequenceEqual(other.ComputeSpecInputs()));
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x0007775C File Offset: 0x0007595C
		public override int GetHashCode()
		{
			if (this._inputsHashCode == null)
			{
				this._inputsHashCode = new int?(this.ComputeHashCode());
			}
			return this._inputsHashCode.Value;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00077788 File Offset: 0x00075988
		internal int GetHashCodeOnComputedInputs()
		{
			if (this._computedInputsHashCode == null)
			{
				this._computedInputsHashCode = new int?((this.ComputeSpecInputs().OrderDependentHashCode<State>() * 25117) ^ (this.ReferenceAdditionalInputs.OrderDependentHashCode<State>() * 27127));
			}
			return this._computedInputsHashCode.Value;
		}

		// Token: 0x06002A95 RID: 10901
		protected abstract int ComputeHashCode();

		// Token: 0x04001483 RID: 5251
		private int? _inputsHashCode;

		// Token: 0x04001484 RID: 5252
		private int? _computedInputsHashCode;
	}
}
