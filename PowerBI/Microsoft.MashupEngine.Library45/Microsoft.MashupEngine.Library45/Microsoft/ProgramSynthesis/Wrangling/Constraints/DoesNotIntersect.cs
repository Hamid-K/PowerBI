using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000226 RID: 550
	public class DoesNotIntersect<TRegion> : ValueToValueConstraint<TRegion, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x06000BD0 RID: 3024 RVA: 0x00023F1A File Offset: 0x0002211A
		public DoesNotIntersect(TRegion input, TRegion output, bool isSoft = false)
			: base(input, output, isSoft)
		{
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00023F28 File Offset: 0x00022128
		public override bool Valid(Program<TRegion, TRegion> program)
		{
			TRegion tregion = program.Run(base.Input);
			if (tregion != null)
			{
				TRegion output = base.Output;
				return !output.IntersectNonEmpty(tregion);
			}
			return true;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00023F64 File Offset: 0x00022164
		public override bool ConflictsWith(Constraint<TRegion, TRegion> other)
		{
			Example<TRegion, TRegion> example = other as Example<TRegion, TRegion>;
			if (!(example != null))
			{
				return false;
			}
			if (ValueEquality.Comparer.Equals(example.Input, base.Input))
			{
				TRegion output = example.Output;
				return output.IntersectNonEmpty(base.Output);
			}
			return false;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00023FC1 File Offset: 0x000221C1
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -> !intersects('{1}')", new object[] { base.Input, base.Output }));
		}
	}
}
