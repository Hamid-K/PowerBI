using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012DC RID: 4828
	public abstract class CsvConstraint : Constraint<string, ITable<string>>, IOptionConstraint<Options>
	{
		// Token: 0x060091A5 RID: 37285 RVA: 0x001EB138 File Offset: 0x001E9338
		public virtual void SetOptions(Options options)
		{
			options.LearnFw = false;
		}

		// Token: 0x060091A6 RID: 37286 RVA: 0x001EB141 File Offset: 0x001E9341
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return other is FixedWidthConstraint;
		}

		// Token: 0x060091A7 RID: 37287 RVA: 0x001EB14C File Offset: 0x001E934C
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return program is CsvProgram;
		}
	}
}
