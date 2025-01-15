using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Learning;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FBB RID: 4027
	public class PredictiveProgramsConstraint : Constraint<WebRegion, IEnumerable<IEnumerable<string>>>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06006F2D RID: 28461 RVA: 0x0016B75F File Offset: 0x0016995F
		public PredictiveProgramsConstraint(IReadOnlyList<TextTableProgram> programs)
		{
			this.PredictivePrograms = programs;
		}

		// Token: 0x06006F2E RID: 28462 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<WebRegion, IEnumerable<IEnumerable<string>>> program)
		{
			return true;
		}

		// Token: 0x06006F2F RID: 28463 RVA: 0x0016B76E File Offset: 0x0016996E
		public override bool ConflictsWith(Constraint<WebRegion, IEnumerable<IEnumerable<string>>> other)
		{
			return other is PredictiveProgramsConstraint && !this.Equals(other);
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x0016B784 File Offset: 0x00169984
		public void SetOptions(Witnesses.Options options)
		{
			options.PredictiveRowColumnSelectors = (from p in this.PredictivePrograms
				where p.RowSelectorNode != null
				select Tuple.Create<resultSequence, resultSequence[]>(p.RowSelectorNode.Value, p.ColumnSelectorNodes)).ToList<Tuple<resultSequence, resultSequence[]>>();
		}

		// Token: 0x06006F31 RID: 28465 RVA: 0x0016B7EA File Offset: 0x001699EA
		public bool Equals(PredictiveProgramsConstraint other)
		{
			return other != null && (this == other || this.PredictivePrograms.SequenceEqual(other.PredictivePrograms));
		}

		// Token: 0x06006F32 RID: 28466 RVA: 0x0016B808 File Offset: 0x00169A08
		public override bool Equals(Constraint<WebRegion, IEnumerable<IEnumerable<string>>> other)
		{
			return this.Equals(other as PredictiveProgramsConstraint);
		}

		// Token: 0x06006F33 RID: 28467 RVA: 0x0016B816 File Offset: 0x00169A16
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((PredictiveProgramsConstraint)obj)));
		}

		// Token: 0x06006F34 RID: 28468 RVA: 0x0016B844 File Offset: 0x00169A44
		public override int GetHashCode()
		{
			int num = 17;
			foreach (TextTableProgram textTableProgram in this.PredictivePrograms)
			{
				num = num * 23 + textTableProgram.GetHashCode();
			}
			return num;
		}

		// Token: 0x0400305E RID: 12382
		public IReadOnlyList<TextTableProgram> PredictivePrograms;
	}
}
