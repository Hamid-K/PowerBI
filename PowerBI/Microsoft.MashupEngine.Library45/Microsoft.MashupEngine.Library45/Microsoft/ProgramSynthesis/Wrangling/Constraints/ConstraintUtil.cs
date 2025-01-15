using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x0200021C RID: 540
	public static class ConstraintUtil
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x000232BE File Offset: 0x000214BE
		public static IEnumerable<TOutputSubmember> SelectOutputSubmembers<TInputMember, TOutputSubmember>(this IEnumerable<Constraint<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>>> xs)
		{
			foreach (Constraint<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> x in xs)
			{
				ValueToValueConstraint<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>> valueToValueConstraint = x as ValueToValueConstraint<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>>;
				if (valueToValueConstraint != null)
				{
					foreach (IEnumerable<TOutputSubmember> enumerable in valueToValueConstraint.Output)
					{
						foreach (TOutputSubmember toutputSubmember in enumerable)
						{
							yield return toutputSubmember;
						}
						IEnumerator<TOutputSubmember> enumerator3 = null;
					}
					IEnumerator<IEnumerable<TOutputSubmember>> enumerator2 = null;
				}
				ValueToMemberConstraint<IEnumerable<TInputMember>, IEnumerable<TOutputSubmember>> valueToMemberConstraint = x as ValueToMemberConstraint<IEnumerable<TInputMember>, IEnumerable<TOutputSubmember>>;
				if (valueToMemberConstraint != null)
				{
					foreach (TOutputSubmember toutputSubmember2 in valueToMemberConstraint.OutputMember)
					{
						yield return toutputSubmember2;
					}
					IEnumerator<TOutputSubmember> enumerator3 = null;
				}
				MemberToMemberConstraint<TInputMember, IEnumerable<TOutputSubmember>> memberToMemberConstraint = x as MemberToMemberConstraint<TInputMember, IEnumerable<TOutputSubmember>>;
				if (memberToMemberConstraint != null)
				{
					foreach (TOutputSubmember toutputSubmember3 in memberToMemberConstraint.OutputMember)
					{
						yield return toutputSubmember3;
					}
					IEnumerator<TOutputSubmember> enumerator3 = null;
				}
				MemberToSubmemberConstraint<TInputMember, TOutputSubmember> memberToSubmemberConstraint = x as MemberToSubmemberConstraint<TInputMember, TOutputSubmember>;
				if (memberToSubmemberConstraint != null)
				{
					yield return memberToSubmemberConstraint.OutputSubmember;
				}
				x = null;
			}
			IEnumerator<Constraint<IEnumerable<TInputMember>, IEnumerable<IEnumerable<TOutputSubmember>>>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000232CE File Offset: 0x000214CE
		public static IEnumerable<TOutputMember> SelectOutputMembers<TInput, TOutputMember>(this IEnumerable<Constraint<TInput, IEnumerable<TOutputMember>>> xs)
		{
			foreach (Constraint<TInput, IEnumerable<TOutputMember>> x in xs)
			{
				ValueToValueConstraint<TInput, IEnumerable<TOutputMember>> valueToValueConstraint = x as ValueToValueConstraint<TInput, IEnumerable<TOutputMember>>;
				if (valueToValueConstraint != null)
				{
					foreach (TOutputMember toutputMember in valueToValueConstraint.Output)
					{
						yield return toutputMember;
					}
					IEnumerator<TOutputMember> enumerator2 = null;
				}
				ValueToMemberConstraint<TInput, TOutputMember> valueToMemberConstraint = x as ValueToMemberConstraint<TInput, TOutputMember>;
				if (valueToMemberConstraint != null)
				{
					yield return valueToMemberConstraint.OutputMember;
				}
				x = null;
			}
			IEnumerator<Constraint<TInput, IEnumerable<TOutputMember>>> enumerator = null;
			yield break;
			yield break;
		}
	}
}
