using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Features.InputTransformation;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007E2 RID: 2018
	internal class TransformedFeatureCalculationContextInputs : FeatureCalculationContextInputs
	{
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x0007852C File Offset: 0x0007672C
		private FeatureCalculationContextInputs Parent { get; }

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x00078534 File Offset: 0x00076734
		private IInputTransformer InputTransformation { get; }

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x0007853C File Offset: 0x0007673C
		internal override IReadOnlyList<State> ReferenceSpecInputs
		{
			get
			{
				return this.Parent.ReferenceSpecInputs;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x00078549 File Offset: 0x00076749
		internal override IReadOnlyList<State> ReferenceAdditionalInputs
		{
			get
			{
				return this.Parent.ReferenceAdditionalInputs;
			}
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x00078556 File Offset: 0x00076756
		internal TransformedFeatureCalculationContextInputs(FeatureCalculationContextInputs parent, IInputTransformer transform)
		{
			this.InputTransformation = transform;
			this.Parent = parent;
			this._specInputs = new Lazy<IReadOnlyList<State>>(() => this.InputTransformation.Transform(this.Parent.ComputeSpecInputs()).ToList<State>());
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x00078583 File Offset: 0x00076783
		internal override IReadOnlyList<State> ComputeSpecInputs()
		{
			return this._specInputs.Value;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x00078590 File Offset: 0x00076790
		internal override IReadOnlyList<State> MaterializeSpecInputs()
		{
			IReadOnlyList<State> readOnlyList = this.ComputeSpecInputs();
			this._specInputsMaterialized = true;
			return readOnlyList;
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000785A1 File Offset: 0x000767A1
		internal override FeatureCalculationContextInputs GetBaseContextInputsForTransform()
		{
			if (!this._specInputsMaterialized)
			{
				return this;
			}
			return new ReferenceFeatureCalculationContextInputs(this.ComputeSpecInputs(), this.ReferenceAdditionalInputs);
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000785C0 File Offset: 0x000767C0
		public bool Equals(TransformedFeatureCalculationContextInputs other)
		{
			return other != null && (this == other || (this.InputTransformation.Equals(other.InputTransformation) && this.Parent.Equals(other.Parent)));
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000785F4 File Offset: 0x000767F4
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
			TransformedFeatureCalculationContextInputs transformedFeatureCalculationContextInputs = other as TransformedFeatureCalculationContextInputs;
			return transformedFeatureCalculationContextInputs != null && this.Equals(transformedFeatureCalculationContextInputs);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000784FF File Offset: 0x000766FF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x0007861F File Offset: 0x0007681F
		protected override int ComputeHashCode()
		{
			return this.Parent.GetHashCode() ^ (this.InputTransformation.GetHashCode() * 12853);
		}

		// Token: 0x040014A9 RID: 5289
		private readonly Lazy<IReadOnlyList<State>> _specInputs;

		// Token: 0x040014AC RID: 5292
		private volatile bool _specInputsMaterialized;
	}
}
