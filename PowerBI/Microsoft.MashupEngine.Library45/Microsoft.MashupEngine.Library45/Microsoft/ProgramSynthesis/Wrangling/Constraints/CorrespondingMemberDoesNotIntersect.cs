using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000221 RID: 545
	public class CorrespondingMemberDoesNotIntersect<TRegion> : MemberToMemberConstraint<TRegion, TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x00023AFC File Offset: 0x00021CFC
		public CorrespondingMemberDoesNotIntersect(TRegion inputMember, TRegion outputMember, bool isSoft = false)
			: base(inputMember, outputMember, isSoft)
		{
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00023B08 File Offset: 0x00021D08
		protected override bool Valid(Program<IEnumerable<TRegion>, IEnumerable<TRegion>> program, TRegion programOutputMember)
		{
			if (programOutputMember != null)
			{
				ref TRegion ptr = ref programOutputMember;
				if (default(TRegion) == null)
				{
					TRegion tregion = programOutputMember;
					ptr = ref tregion;
				}
				return !ptr.IntersectNonEmpty(base.OutputMember);
			}
			return true;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00023B50 File Offset: 0x00021D50
		public override bool ConflictsWith(Constraint<IEnumerable<TRegion>, IEnumerable<TRegion>> other)
		{
			Example<IEnumerable<TRegion>, IEnumerable<TRegion>> example = other as Example<IEnumerable<TRegion>, IEnumerable<TRegion>>;
			if (example != null)
			{
				return example.Input.Zip(example.Output, delegate(TRegion inputMember, TRegion outputMember)
				{
					if (ValueEquality.Comparer.Equals(inputMember, base.InputMember))
					{
						ref TRegion ptr = ref outputMember;
						if (default(TRegion) == null)
						{
							TRegion tregion = outputMember;
							ptr = ref tregion;
						}
						return ptr.IntersectNonEmpty(base.OutputMember);
					}
					return false;
				}).Any((bool b) => b);
			}
			CorrespondingMemberEquals<TRegion, TRegion> correspondingMemberEquals = other as CorrespondingMemberEquals<TRegion, TRegion>;
			if (!(correspondingMemberEquals != null))
			{
				return false;
			}
			if (ValueEquality.Comparer.Equals(correspondingMemberEquals.InputMember, base.InputMember))
			{
				TRegion outputMember2 = correspondingMemberEquals.OutputMember;
				return outputMember2.IntersectNonEmpty(base.OutputMember);
			}
			return false;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00023BFF File Offset: 0x00021DFF
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("'{0}' -> !intersects('{1}')", new object[] { base.InputMember, base.OutputMember }));
		}
	}
}
