using System;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BA8 RID: 2984
	public class SplitTopArrays : Constraint<string, ITable<string>>, IOptionConstraint<SynthesisOptions>
	{
		// Token: 0x06004BDB RID: 19419 RVA: 0x000EEB91 File Offset: 0x000ECD91
		private SplitTopArrays()
		{
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x000EEE9D File Offset: 0x000ED09D
		public static SplitTopArrays Instance { get; } = new SplitTopArrays();

		// Token: 0x06004BDD RID: 19421 RVA: 0x000EEEA4 File Offset: 0x000ED0A4
		public void SetOptions(SynthesisOptions options)
		{
			options.SplitTopArrays = true;
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0000D050 File Offset: 0x0000B250
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return this == other;
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x000EEEAD File Offset: 0x000ED0AD
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return other is AutoFlatten || other is JoinAllArrays || other is JoinSingleTopArray;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x000EEECA File Offset: 0x000ED0CA
		public override int GetHashCode()
		{
			return 2267;
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0000D050 File Offset: 0x0000B250
		public override bool Equals(object obj)
		{
			return this == obj;
		}
	}
}
