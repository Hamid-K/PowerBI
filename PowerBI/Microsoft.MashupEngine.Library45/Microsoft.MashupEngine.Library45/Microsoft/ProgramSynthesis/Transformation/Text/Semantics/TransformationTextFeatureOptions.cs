using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CC9 RID: 7369
	public class TransformationTextFeatureOptions : FeatureOptions
	{
		// Token: 0x170029DA RID: 10714
		// (get) Token: 0x0600F9EF RID: 63983 RVA: 0x0035284A File Offset: 0x00350A4A
		public bool SkipNullProportionCheck { get; }

		// Token: 0x170029DB RID: 10715
		// (get) Token: 0x0600F9F0 RID: 63984 RVA: 0x00352852 File Offset: 0x00350A52
		public bool AvoidImperialDateTimeFormat { get; }

		// Token: 0x0600F9F1 RID: 63985 RVA: 0x0035285A File Offset: 0x00350A5A
		public TransformationTextFeatureOptions(bool skipNullProportionsCheck, bool avoidImperialDateTimeFormat, IReadOnlyDictionary<Symbol, Symbol> substitutions = null)
			: base(substitutions)
		{
			this.SkipNullProportionCheck = skipNullProportionsCheck;
			this.AvoidImperialDateTimeFormat = avoidImperialDateTimeFormat;
		}

		// Token: 0x0600F9F2 RID: 63986 RVA: 0x00352871 File Offset: 0x00350A71
		public bool Equals(TransformationTextFeatureOptions other)
		{
			return other == this || (other != null && (base.Equals(other) && this.SkipNullProportionCheck == other.SkipNullProportionCheck) && this.AvoidImperialDateTimeFormat == other.AvoidImperialDateTimeFormat);
		}

		// Token: 0x0600F9F3 RID: 63987 RVA: 0x003528A5 File Offset: 0x00350AA5
		public override bool Equals(IFeatureOptions other)
		{
			return other == this || (other != null && !(other.GetType() != base.GetType()) && this.Equals((TransformationTextFeatureOptions)other));
		}

		// Token: 0x0600F9F4 RID: 63988 RVA: 0x003528D3 File Offset: 0x00350AD3
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && base.Equals((FeatureOptions)obj)));
		}

		// Token: 0x0600F9F5 RID: 63989 RVA: 0x00352904 File Offset: 0x00350B04
		public override int GetHashCode()
		{
			return HashHelpers.Combine(this.SkipNullProportionCheck.GetHashCode(), this.AvoidImperialDateTimeFormat.GetHashCode());
		}

		// Token: 0x0600F9F6 RID: 63990 RVA: 0x00352932 File Offset: 0x00350B32
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("SkipNullProportionCheck={0}, AvoidImperialDateTimeFormat={1}, {2}", new object[]
			{
				this.SkipNullProportionCheck,
				this.AvoidImperialDateTimeFormat,
				base.ToString()
			}));
		}

		// Token: 0x0600F9F7 RID: 63991 RVA: 0x0035296E File Offset: 0x00350B6E
		protected override FeatureOptions Clone()
		{
			return new TransformationTextFeatureOptions(this.SkipNullProportionCheck, this.AvoidImperialDateTimeFormat, base.Substitutions);
		}
	}
}
