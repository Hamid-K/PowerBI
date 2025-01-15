using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000227 RID: 551
	public class DoesNotIntersectSequence<TRegion> : ValueToMemberConstraint<TRegion, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x06000BD4 RID: 3028 RVA: 0x00023FF4 File Offset: 0x000221F4
		public DoesNotIntersectSequence(TRegion input, TRegion output, bool isSoft = false)
			: base(input, output, isSoft)
		{
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00023FFF File Offset: 0x000221FF
		public override bool Valid(Program<TRegion, IEnumerable<TRegion>> program)
		{
			IEnumerable<TRegion> enumerable = program.Run(base.Input);
			return enumerable == null || enumerable.All(delegate(TRegion po)
			{
				if (po != null)
				{
					TRegion outputMember = base.OutputMember;
					return !outputMember.IntersectNonEmpty(po);
				}
				return true;
			});
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00024024 File Offset: 0x00022224
		public override bool ConflictsWith(Constraint<TRegion, IEnumerable<TRegion>> other)
		{
			ValueToValueConstraint<TRegion, IEnumerable<TRegion>> valueToValueConstraint = other as ValueToValueConstraint<TRegion, IEnumerable<TRegion>>;
			return valueToValueConstraint != null && (valueToValueConstraint is Example<TRegion, IEnumerable<TRegion>> || valueToValueConstraint is Prefix<TRegion, TRegion> || valueToValueConstraint is Subset<TRegion, TRegion>) && ValueEquality.Comparer.Equals(valueToValueConstraint.Input, base.Input) && valueToValueConstraint.Output.Any(delegate(TRegion ex)
			{
				ref TRegion ptr = ref ex;
				if (default(TRegion) == null)
				{
					TRegion tregion = ex;
					ptr = ref tregion;
				}
				return ptr.IntersectNonEmpty(base.OutputMember);
			});
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00024096 File Offset: 0x00022296
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -> !intersects('{1}')", new object[] { base.Input, base.OutputMember }));
		}
	}
}
