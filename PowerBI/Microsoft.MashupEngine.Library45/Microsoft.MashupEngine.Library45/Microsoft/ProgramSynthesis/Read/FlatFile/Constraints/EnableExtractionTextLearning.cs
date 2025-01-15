using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012DE RID: 4830
	public class EnableExtractionTextLearning : Constraint<string, ITable<string>>, IOptionConstraint<Options>
	{
		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x060091B0 RID: 37296 RVA: 0x001EB235 File Offset: 0x001E9435
		public static EnableExtractionTextLearning Instance { get; } = new EnableExtractionTextLearning();

		// Token: 0x060091B1 RID: 37297 RVA: 0x000EEB91 File Offset: 0x000ECD91
		private EnableExtractionTextLearning()
		{
		}

		// Token: 0x060091B2 RID: 37298 RVA: 0x001EB23C File Offset: 0x001E943C
		public virtual void SetOptions(Options options)
		{
			options.LearnExtractionText = true;
		}

		// Token: 0x060091B3 RID: 37299 RVA: 0x001EB245 File Offset: 0x001E9445
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return other is FixedWidthConstraint || other is CsvConstraint;
		}

		// Token: 0x060091B4 RID: 37300 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x060091B5 RID: 37301 RVA: 0x001EB25A File Offset: 0x001E945A
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return other == this;
		}

		// Token: 0x060091B6 RID: 37302 RVA: 0x001EB260 File Offset: 0x001E9460
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}
	}
}
