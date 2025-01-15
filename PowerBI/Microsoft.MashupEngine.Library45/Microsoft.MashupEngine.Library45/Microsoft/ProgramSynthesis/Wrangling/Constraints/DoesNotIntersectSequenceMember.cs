using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000228 RID: 552
	public class DoesNotIntersectSequenceMember<TRegion> : MemberToSubmemberConstraint<TRegion, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x06000BDA RID: 3034 RVA: 0x00024134 File Offset: 0x00022334
		public DoesNotIntersectSequenceMember(TRegion inputMember, TRegion outputSubmember, bool isSoft = false)
			: base(inputMember, outputSubmember, isSoft)
		{
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002413F File Offset: 0x0002233F
		protected override bool Valid(Program<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>> program, IEnumerable<TRegion> outputMember)
		{
			return outputMember == null || outputMember.All(delegate(TRegion po)
			{
				if (po != null)
				{
					TRegion outputSubmember = base.OutputSubmember;
					return !outputSubmember.IntersectNonEmpty(po);
				}
				return true;
			});
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00024158 File Offset: 0x00022358
		public override bool ConflictsWith(Constraint<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>> other)
		{
			Example<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>> example = other as Example<IEnumerable<TRegion>, IEnumerable<IEnumerable<TRegion>>>;
			if (example != null)
			{
				return example.Input.Zip(example.Output, (TRegion input, IEnumerable<TRegion> output) => ValueEquality.Comparer.Equals(input, base.InputMember) && output.Any(delegate(TRegion ex)
				{
					ref TRegion ptr = ref ex;
					if (default(TRegion) == null)
					{
						TRegion tregion = ex;
						ptr = ref tregion;
					}
					return ptr.IntersectNonEmpty(base.OutputSubmember);
				})).Any((bool b) => b);
			}
			MemberToMemberConstraint<TRegion, IEnumerable<TRegion>> memberToMemberConstraint = other as MemberToMemberConstraint<TRegion, IEnumerable<TRegion>>;
			return memberToMemberConstraint != null && (memberToMemberConstraint is CorrespondingMemberEquals<TRegion, IEnumerable<TRegion>> || memberToMemberConstraint is MemberPrefix<TRegion, TRegion> || memberToMemberConstraint is MemberSubset<TRegion, TRegion>) && ValueEquality.Comparer.Equals(memberToMemberConstraint.InputMember, base.InputMember) && memberToMemberConstraint.OutputMember.Any(delegate(TRegion ex)
			{
				ref TRegion ptr2 = ref ex;
				if (default(TRegion) == null)
				{
					TRegion tregion2 = ex;
					ptr2 = ref tregion2;
				}
				return ptr2.IntersectNonEmpty(base.OutputSubmember);
			});
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002421C File Offset: 0x0002241C
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -> !intersects('{1}')", new object[] { base.InputMember, base.OutputSubmember }));
		}
	}
}
