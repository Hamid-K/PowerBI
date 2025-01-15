using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DDB RID: 7643
	public class AllowConstantProgram : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x0601003B RID: 65595 RVA: 0x002DD5BA File Offset: 0x002DB7BA
		private AllowConstantProgram()
		{
		}

		// Token: 0x17002A88 RID: 10888
		// (get) Token: 0x0601003C RID: 65596 RVA: 0x00370B8F File Offset: 0x0036ED8F
		public static AllowConstantProgram Instance { get; } = new AllowConstantProgram();

		// Token: 0x0601003D RID: 65597 RVA: 0x00370B96 File Offset: 0x0036ED96
		public void SetOptions(Witnesses.Options options)
		{
			options.ForbidConstantProgram = false;
		}

		// Token: 0x0601003E RID: 65598 RVA: 0x00370B9F File Offset: 0x0036ED9F
		public override bool Equals(Constraint<IRow, object> other)
		{
			return other is AllowConstantProgram;
		}

		// Token: 0x0601003F RID: 65599 RVA: 0x00370BAA File Offset: 0x0036EDAA
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return other is ForbidConstantProgram;
		}

		// Token: 0x06010040 RID: 65600 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IRow, object> program)
		{
			return true;
		}

		// Token: 0x06010041 RID: 65601 RVA: 0x00370BB5 File Offset: 0x0036EDB5
		public override int GetHashCode()
		{
			return 31723;
		}
	}
}
