using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012E3 RID: 4835
	internal class LearnLineLimit : Constraint<string, ITable<string>>, IOptionConstraint<Options>
	{
		// Token: 0x060091CD RID: 37325 RVA: 0x001EB4C4 File Offset: 0x001E96C4
		public LearnLineLimit(int limit)
		{
			this.Limit = limit;
		}

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x060091CE RID: 37326 RVA: 0x001EB4D3 File Offset: 0x001E96D3
		public int Limit { get; }

		// Token: 0x060091CF RID: 37327 RVA: 0x001EB4DB File Offset: 0x001E96DB
		public void SetOptions(Options options)
		{
			options.LearnLineLimit = new int?(this.Limit);
		}

		// Token: 0x060091D0 RID: 37328 RVA: 0x001EB4F0 File Offset: 0x001E96F0
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			LearnLineLimit learnLineLimit = other as LearnLineLimit;
			return learnLineLimit != null && this.Limit != learnLineLimit.Limit;
		}

		// Token: 0x060091D1 RID: 37329 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x060091D2 RID: 37330 RVA: 0x001EB51C File Offset: 0x001E971C
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			LearnLineLimit learnLineLimit = other as LearnLineLimit;
			return learnLineLimit != null && this.Limit == learnLineLimit.Limit;
		}

		// Token: 0x060091D3 RID: 37331 RVA: 0x001EB543 File Offset: 0x001E9743
		public override int GetHashCode()
		{
			return 1367 ^ this.Limit;
		}
	}
}
