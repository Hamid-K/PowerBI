using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x02001317 RID: 4887
	public class SuggestionsMode : Constraint<StringRegion, SplitCell[]>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x060092F6 RID: 37622 RVA: 0x001EE9A5 File Offset: 0x001ECBA5
		public SuggestionsMode(bool suggestionsMode)
		{
			this.IsSuggestionsMode = suggestionsMode;
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x060092F7 RID: 37623 RVA: 0x001EE9B4 File Offset: 0x001ECBB4
		public bool IsSuggestionsMode { get; }

		// Token: 0x060092F8 RID: 37624 RVA: 0x001EE9BC File Offset: 0x001ECBBC
		public void SetOptions(Witnesses.Options options)
		{
			options.SuggestionsMode = this.IsSuggestionsMode;
		}

		// Token: 0x060092F9 RID: 37625 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			return true;
		}

		// Token: 0x060092FA RID: 37626 RVA: 0x001EE9CC File Offset: 0x001ECBCC
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> other)
		{
			SuggestionsMode suggestionsMode = other as SuggestionsMode;
			return suggestionsMode != null && this.IsSuggestionsMode != suggestionsMode.IsSuggestionsMode;
		}

		// Token: 0x060092FB RID: 37627 RVA: 0x001EE9FC File Offset: 0x001ECBFC
		public bool Equals(SuggestionsMode other)
		{
			return other != null && (this == other || this.IsSuggestionsMode == other.IsSuggestionsMode);
		}

		// Token: 0x060092FC RID: 37628 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			return this.Equals(other);
		}

		// Token: 0x060092FD RID: 37629 RVA: 0x001EEA17 File Offset: 0x001ECC17
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((SuggestionsMode)obj)));
		}

		// Token: 0x060092FE RID: 37630 RVA: 0x001EEA48 File Offset: 0x001ECC48
		public override int GetHashCode()
		{
			return 5745743 ^ this.IsSuggestionsMode.GetHashCode();
		}

		// Token: 0x060092FF RID: 37631 RVA: 0x001EEA69 File Offset: 0x001ECC69
		public override string ToString()
		{
			return string.Format("{0}({1})", "SuggestionsMode", this.IsSuggestionsMode);
		}
	}
}
