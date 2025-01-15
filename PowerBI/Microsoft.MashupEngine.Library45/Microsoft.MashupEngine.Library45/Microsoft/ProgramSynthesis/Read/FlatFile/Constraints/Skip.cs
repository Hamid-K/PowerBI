using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012E4 RID: 4836
	public class Skip : Constraint<string, ITable<string>>, IOptionConstraint<Options>
	{
		// Token: 0x060091D4 RID: 37332 RVA: 0x001EB551 File Offset: 0x001E9751
		public Skip(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("Should be a positive number", "count");
			}
			this.Count = count;
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x060091D5 RID: 37333 RVA: 0x001EB574 File Offset: 0x001E9774
		public int Count { get; }

		// Token: 0x060091D6 RID: 37334 RVA: 0x001EB57C File Offset: 0x001E977C
		public void SetOptions(Options options)
		{
			options.Skip = new int?(this.Count);
		}

		// Token: 0x060091D7 RID: 37335 RVA: 0x001EB590 File Offset: 0x001E9790
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			Skip skip = other as Skip;
			return skip != null && this.Count != skip.Count;
		}

		// Token: 0x060091D8 RID: 37336 RVA: 0x001EB5BC File Offset: 0x001E97BC
		public override bool Valid(Program<string, ITable<string>> program)
		{
			SimpleProgram simpleProgram = program as SimpleProgram;
			return simpleProgram != null && simpleProgram.Skip == this.Count;
		}

		// Token: 0x060091D9 RID: 37337 RVA: 0x001EB5E4 File Offset: 0x001E97E4
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			Skip skip = other as Skip;
			return skip != null && this.Count == skip.Count;
		}

		// Token: 0x060091DA RID: 37338 RVA: 0x001EB60B File Offset: 0x001E980B
		public override int GetHashCode()
		{
			return 983 ^ this.Count;
		}
	}
}
