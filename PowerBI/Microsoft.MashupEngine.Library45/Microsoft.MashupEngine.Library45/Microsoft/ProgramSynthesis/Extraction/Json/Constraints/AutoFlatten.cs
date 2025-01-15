using System;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000B9E RID: 2974
	public class AutoFlatten : Constraint<string, ITable<string>>, IOptionConstraint<SynthesisOptions>
	{
		// Token: 0x06004B90 RID: 19344 RVA: 0x000EEB99 File Offset: 0x000ECD99
		public void SetOptions(SynthesisOptions options)
		{
			options.Auto = true;
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0000D050 File Offset: 0x0000B250
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return this == other;
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x000EEBA2 File Offset: 0x000ECDA2
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return other is JoinAllArrays || other is SplitTopArrays || other is JoinSingleTopArray;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x000EEBBF File Offset: 0x000ECDBF
		public override int GetHashCode()
		{
			return 8599;
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x0000D050 File Offset: 0x0000B250
		public override bool Equals(object obj)
		{
			return this == obj;
		}
	}
}
