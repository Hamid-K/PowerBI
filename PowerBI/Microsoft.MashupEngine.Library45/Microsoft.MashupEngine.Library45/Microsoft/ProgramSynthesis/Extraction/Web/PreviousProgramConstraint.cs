using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Extraction.Web.Learning;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FBD RID: 4029
	public class PreviousProgramConstraint : KnownProgram<WebRegion, IEnumerable<IEnumerable<string>>>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06006F39 RID: 28473 RVA: 0x0016B8EA File Offset: 0x00169AEA
		public PreviousProgramConstraint(TextTableProgram program, TextTableConstraint previousConstraint)
			: base(program)
		{
			this.PreviousTableConstraint = previousConstraint;
		}

		// Token: 0x06006F3A RID: 28474 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<WebRegion, IEnumerable<IEnumerable<string>>> program)
		{
			return true;
		}

		// Token: 0x06006F3B RID: 28475 RVA: 0x0016B8FA File Offset: 0x00169AFA
		public override bool ConflictsWith(Constraint<WebRegion, IEnumerable<IEnumerable<string>>> other)
		{
			return other is PreviousProgramConstraint && !this.Equals(other);
		}

		// Token: 0x06006F3C RID: 28476 RVA: 0x0016B910 File Offset: 0x00169B10
		public void SetOptions(Witnesses.Options options)
		{
			TextTableProgram textTableProgram = base.Program as TextTableProgram;
			options.PreviouslyLearntRowSelector = textTableProgram.RowSelectorNode;
			options.PreviouslyLearntColumnSelectors = textTableProgram.ColumnSelectorNodes;
			options.PreviousTextTableExamples = this.PreviousTableConstraint.ColumnExamples;
		}

		// Token: 0x06006F3D RID: 28477 RVA: 0x0016B952 File Offset: 0x00169B52
		public bool Equals(PreviousProgramConstraint other)
		{
			return other != null && (this == other || (base.Program.Equals(other.Program) && this.PreviousTableConstraint.Equals(other.PreviousTableConstraint)));
		}

		// Token: 0x06006F3E RID: 28478 RVA: 0x0016B985 File Offset: 0x00169B85
		public override bool Equals(Constraint<WebRegion, IEnumerable<IEnumerable<string>>> other)
		{
			return this.Equals(other as PreviousProgramConstraint);
		}

		// Token: 0x06006F3F RID: 28479 RVA: 0x0016B993 File Offset: 0x00169B93
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((PreviousProgramConstraint)obj)));
		}

		// Token: 0x06006F40 RID: 28480 RVA: 0x0016B9C1 File Offset: 0x00169BC1
		public override int GetHashCode()
		{
			return (391 + base.Program.GetHashCode()) * 23 + this.PreviousTableConstraint.GetHashCode();
		}

		// Token: 0x04003062 RID: 12386
		public TextTableConstraint PreviousTableConstraint;
	}
}
