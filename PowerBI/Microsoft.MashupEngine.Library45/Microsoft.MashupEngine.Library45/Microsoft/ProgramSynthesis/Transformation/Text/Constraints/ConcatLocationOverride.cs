using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DDF RID: 7647
	public class ConcatLocationOverride : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06010058 RID: 65624 RVA: 0x00370D92 File Offset: 0x0036EF92
		public ConcatLocationOverride(ConcatLocation concatLocation)
		{
			this.ConcatLocation = concatLocation;
		}

		// Token: 0x17002A8B RID: 10891
		// (get) Token: 0x06010059 RID: 65625 RVA: 0x00370DA1 File Offset: 0x0036EFA1
		private ConcatLocation ConcatLocation { get; }

		// Token: 0x0601005A RID: 65626 RVA: 0x00370DA9 File Offset: 0x0036EFA9
		public void SetOptions(Witnesses.Options options)
		{
			options.DisableConcatHeuristics = true;
			options.ConcatLocation = this.ConcatLocation;
		}

		// Token: 0x0601005B RID: 65627 RVA: 0x00370DBE File Offset: 0x0036EFBE
		public override bool Equals(Constraint<IRow, object> other)
		{
			return other is ConcatLocationOverride && ((ConcatLocationOverride)other).ConcatLocation == this.ConcatLocation;
		}

		// Token: 0x0601005C RID: 65628 RVA: 0x00370DDD File Offset: 0x0036EFDD
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			return other is ConcatLocationOverride && ((ConcatLocationOverride)other).ConcatLocation != this.ConcatLocation;
		}

		// Token: 0x0601005D RID: 65629 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IRow, object> program)
		{
			return true;
		}

		// Token: 0x0601005E RID: 65630 RVA: 0x00370E00 File Offset: 0x0036F000
		public override int GetHashCode()
		{
			return 37181 ^ this.ConcatLocation.GetHashCode();
		}
	}
}
