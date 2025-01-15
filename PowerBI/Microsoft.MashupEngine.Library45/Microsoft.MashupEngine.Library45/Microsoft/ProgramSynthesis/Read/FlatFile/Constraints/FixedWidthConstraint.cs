using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012E2 RID: 4834
	public abstract class FixedWidthConstraint : Constraint<string, ITable<string>>, IOptionConstraint<Options>
	{
		// Token: 0x060091C9 RID: 37321 RVA: 0x001EB4A5 File Offset: 0x001E96A5
		public virtual void SetOptions(Options options)
		{
			options.LearnCsv = false;
		}

		// Token: 0x060091CA RID: 37322 RVA: 0x001EB4AE File Offset: 0x001E96AE
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return other is CsvConstraint;
		}

		// Token: 0x060091CB RID: 37323 RVA: 0x001EB4B9 File Offset: 0x001E96B9
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return program is FwProgram;
		}
	}
}
