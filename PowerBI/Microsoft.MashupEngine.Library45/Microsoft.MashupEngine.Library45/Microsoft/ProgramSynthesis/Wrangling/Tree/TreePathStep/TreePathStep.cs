using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep
{
	// Token: 0x020000FD RID: 253
	public abstract class TreePathStep
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005DE RID: 1502
		public abstract double Score { get; }

		// Token: 0x060005DF RID: 1503
		public abstract Node Find(Node node);

		// Token: 0x060005E0 RID: 1504
		internal abstract string Serialize();

		// Token: 0x060005E1 RID: 1505 RVA: 0x000131C4 File Offset: 0x000113C4
		public static TreePathStep From(string step)
		{
			if (step.Length < 3)
			{
				return null;
			}
			if (step[0] != '[' && step[step.Length - 1] != ']')
			{
				return null;
			}
			if (string.Equals(step, "[ParentStep]"))
			{
				return ParentStep.Instance;
			}
			if (string.Equals(step, "[CurrentStep]"))
			{
				return CurrentStep.Instance;
			}
			if (string.Equals(step, "[RightSiblingTokenStep]"))
			{
				return RightSiblingTokenStep.Instance;
			}
			string[] array = step.Substring(1, step.Length - 2).Split(new char[] { ',' });
			if (array.Length == 2)
			{
				int num;
				if (int.TryParse(array[1], out num))
				{
					return new KthLabelStep(array[0], num);
				}
				return null;
			}
			else
			{
				if (array.Length != 1)
				{
					throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown step {0}", new object[] { step })));
				}
				int num2;
				if (int.TryParse(array[0], out num2))
				{
					return new KthStep(num2);
				}
				return null;
			}
		}
	}
}
