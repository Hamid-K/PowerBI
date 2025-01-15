using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012E5 RID: 4837
	public class SkipFooter : Constraint<string, ITable<string>>, IOptionConstraint<Options>
	{
		// Token: 0x060091DB RID: 37339 RVA: 0x001EB619 File Offset: 0x001E9819
		public SkipFooter(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("Should be a positive number", "count");
			}
			this.Count = count;
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x060091DC RID: 37340 RVA: 0x001EB63C File Offset: 0x001E983C
		public int Count { get; }

		// Token: 0x060091DD RID: 37341 RVA: 0x001EB644 File Offset: 0x001E9844
		public void SetOptions(Options options)
		{
			options.SkipFooter = new int?(this.Count);
		}

		// Token: 0x060091DE RID: 37342 RVA: 0x001EB658 File Offset: 0x001E9858
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			SkipFooter skipFooter = other as SkipFooter;
			return skipFooter != null && this.Count != skipFooter.Count;
		}

		// Token: 0x060091DF RID: 37343 RVA: 0x001EB684 File Offset: 0x001E9884
		public override bool Valid(Program<string, ITable<string>> program)
		{
			SimpleProgram simpleProgram = program as SimpleProgram;
			return simpleProgram != null && simpleProgram.SkipFooter == this.Count;
		}

		// Token: 0x060091E0 RID: 37344 RVA: 0x001EB6AC File Offset: 0x001E98AC
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			SkipFooter skipFooter = other as SkipFooter;
			return skipFooter != null && this.Count == skipFooter.Count;
		}

		// Token: 0x060091E1 RID: 37345 RVA: 0x001EB6D3 File Offset: 0x001E98D3
		public override int GetHashCode()
		{
			return 1741 ^ this.Count;
		}
	}
}
