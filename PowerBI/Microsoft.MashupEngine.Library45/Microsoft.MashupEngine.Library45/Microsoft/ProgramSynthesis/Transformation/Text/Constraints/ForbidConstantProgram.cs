using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DE3 RID: 7651
	public class ForbidConstantProgram : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x0601007A RID: 65658 RVA: 0x002DD5BA File Offset: 0x002DB7BA
		private ForbidConstantProgram()
		{
		}

		// Token: 0x17002A92 RID: 10898
		// (get) Token: 0x0601007B RID: 65659 RVA: 0x003710DB File Offset: 0x0036F2DB
		public static ForbidConstantProgram Instance { get; } = new ForbidConstantProgram();

		// Token: 0x0601007C RID: 65660 RVA: 0x003710E2 File Offset: 0x0036F2E2
		public void SetOptions(Witnesses.Options options)
		{
			options.ForbidConstantProgram = true;
		}

		// Token: 0x0601007D RID: 65661 RVA: 0x00370BAA File Offset: 0x0036EDAA
		public override bool Equals(Constraint<IRow, object> other)
		{
			return other is ForbidConstantProgram;
		}

		// Token: 0x0601007E RID: 65662 RVA: 0x00370B9F File Offset: 0x0036ED9F
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return other is AllowConstantProgram;
		}

		// Token: 0x0601007F RID: 65663 RVA: 0x003710EB File Offset: 0x0036F2EB
		public override bool Valid(Program<IRow, object> program)
		{
			return ((Program)program).AllTransformations.Any((TransformationDescription t) => t.Category != TransformationCategory.Constant);
		}

		// Token: 0x06010080 RID: 65664 RVA: 0x0037111C File Offset: 0x0036F31C
		public override int GetHashCode()
		{
			return 63647;
		}
	}
}
