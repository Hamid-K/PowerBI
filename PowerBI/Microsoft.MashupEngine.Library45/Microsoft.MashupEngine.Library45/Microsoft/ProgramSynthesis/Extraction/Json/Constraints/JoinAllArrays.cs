using System;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BA3 RID: 2979
	public class JoinAllArrays : Constraint<string, ITable<string>>, IOptionConstraint<SynthesisOptions>
	{
		// Token: 0x06004BAD RID: 19373 RVA: 0x000EEB91 File Offset: 0x000ECD91
		private JoinAllArrays()
		{
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06004BAE RID: 19374 RVA: 0x000EED1F File Offset: 0x000ECF1F
		public static JoinAllArrays Instance { get; } = new JoinAllArrays();

		// Token: 0x06004BAF RID: 19375 RVA: 0x000EED26 File Offset: 0x000ECF26
		public void SetOptions(SynthesisOptions options)
		{
			options.JoinAllArrays = true;
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x0000D050 File Offset: 0x0000B250
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return this == other;
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x000EED2F File Offset: 0x000ECF2F
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return other is AutoFlatten || other is SplitTopArrays || other is JoinSingleTopArray;
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x000EED4C File Offset: 0x000ECF4C
		public override int GetHashCode()
		{
			return 4021;
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x0000D050 File Offset: 0x0000B250
		public override bool Equals(object obj)
		{
			return this == obj;
		}
	}
}
