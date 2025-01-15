using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012F2 RID: 4850
	public class IncludeDelimitersInOutput : Constraint<StringRegion, SplitCell[]>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x0600922A RID: 37418 RVA: 0x001EC197 File Offset: 0x001EA397
		public IncludeDelimitersInOutput(bool includeDelimiters)
		{
			this.IncludeDelimiters = includeDelimiters;
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x0600922B RID: 37419 RVA: 0x001EC1A6 File Offset: 0x001EA3A6
		public bool IncludeDelimiters { get; }

		// Token: 0x0600922C RID: 37420 RVA: 0x001EC1AE File Offset: 0x001EA3AE
		public void SetOptions(Witnesses.Options options)
		{
			options.IncludeDelimiters = this.IncludeDelimiters;
		}

		// Token: 0x0600922D RID: 37421 RVA: 0x001EC1BC File Offset: 0x001EA3BC
		public override bool Valid(Program<StringRegion, SplitCell[]> program)
		{
			SplitRegion splitRegion;
			return this.IncludeDelimiters == (Language.Build.Node.IsRule.SplitRegion(program.ProgramNode, out splitRegion) && splitRegion.includeDelimiters.Value);
		}

		// Token: 0x0600922E RID: 37422 RVA: 0x001EC204 File Offset: 0x001EA404
		public override bool ConflictsWith(Constraint<StringRegion, SplitCell[]> other)
		{
			IncludeDelimitersInOutput includeDelimitersInOutput = other as IncludeDelimitersInOutput;
			return includeDelimitersInOutput != null && this.IncludeDelimiters != includeDelimitersInOutput.IncludeDelimiters;
		}

		// Token: 0x0600922F RID: 37423 RVA: 0x001EC234 File Offset: 0x001EA434
		public bool Equals(IncludeDelimitersInOutput other)
		{
			return other != null && (this == other || this.IncludeDelimiters == other.IncludeDelimiters);
		}

		// Token: 0x06009230 RID: 37424 RVA: 0x00024CEC File Offset: 0x00022EEC
		public override bool Equals(Constraint<StringRegion, SplitCell[]> other)
		{
			return this.Equals(other);
		}

		// Token: 0x06009231 RID: 37425 RVA: 0x001EC24F File Offset: 0x001EA44F
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((IncludeDelimitersInOutput)obj)));
		}

		// Token: 0x06009232 RID: 37426 RVA: 0x001EC280 File Offset: 0x001EA480
		public override int GetHashCode()
		{
			return 5745743 ^ this.IncludeDelimiters.GetHashCode();
		}

		// Token: 0x06009233 RID: 37427 RVA: 0x001EC2A1 File Offset: 0x001EA4A1
		public override string ToString()
		{
			return string.Format("{0}({1})", "IncludeDelimitersInOutput", this.IncludeDelimiters);
		}
	}
}
