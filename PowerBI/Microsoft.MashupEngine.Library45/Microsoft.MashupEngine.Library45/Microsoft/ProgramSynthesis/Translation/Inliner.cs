using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002DB RID: 731
	public class Inliner : IOptimizer
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0002DC01 File Offset: 0x0002BE01
		public static Inliner Instance { get; } = new Inliner(null);

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0002DC08 File Offset: 0x0002BE08
		private static bool OperatorsFilter(LinkedListNode<SSAStep> step)
		{
			SSAFunctionApplication ssafunctionApplication = step.Value.RValue as SSAFunctionApplication;
			return ssafunctionApplication != null && ssafunctionApplication.FunctionName.StartsWith("operators.");
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0002DC3C File Offset: 0x0002BE3C
		private static bool DepthUpToKTermFilter(SSAValue ssaValue, int k)
		{
			SSAFunctionApplication ssafunctionApplication = ssaValue as SSAFunctionApplication;
			return ssafunctionApplication == null || (k > 0 && ssafunctionApplication.FunctionArguments.All((SSAValue t) => Inliner.DepthUpToKTermFilter(t, k - 1)));
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0002DC84 File Offset: 0x0002BE84
		private static bool SubtermAtDepthUpToK(SSAValue ssaValue, SSAValue subterm, int k)
		{
			if (!ssaValue.Equals(subterm))
			{
				if (k > 0)
				{
					SSAFunctionApplication ssafunctionApplication = ssaValue as SSAFunctionApplication;
					if (ssafunctionApplication != null)
					{
						return ssafunctionApplication.FunctionArguments.Any((SSAValue t) => Inliner.SubtermAtDepthUpToK(t, subterm, k - 1));
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0002DCE4 File Offset: 0x0002BEE4
		private static bool ShallowTermFilter(LinkedListNode<SSAStep> step)
		{
			return Inliner.OperatorsFilter(step) && (!step.Value.LValue.HasAGivenName() || (Inliner.DepthUpToKTermFilter(step.Value.RValue, 2) && step.Value.LValue.ImmediateDownLinks.All((SSAValue t) => Inliner.SubtermAtDepthUpToK(t, step.Value.LValue, 1))) || (Inliner.DepthUpToKTermFilter(step.Value.RValue, 1) && step.Value.LValue.ImmediateDownLinks.All((SSAValue t) => Inliner.SubtermAtDepthUpToK(t, step.Value.LValue, 2))));
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0002DDAC File Offset: 0x0002BFAC
		public static Inliner OperatorsOnly { get; } = new Inliner(new Func<LinkedListNode<SSAStep>, bool>(Inliner.OperatorsFilter));

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0002DDB3 File Offset: 0x0002BFB3
		public static Inliner ShallowOperatorsOnly { get; } = new Inliner(new Func<LinkedListNode<SSAStep>, bool>(Inliner.ShallowTermFilter));

		// Token: 0x06000FDC RID: 4060 RVA: 0x0002DDBA File Offset: 0x0002BFBA
		private Inliner(Func<LinkedListNode<SSAStep>, bool> allowedToInlineCondition = null)
		{
			this._allowedToInlineCondition = allowedToInlineCondition;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0002DDCC File Offset: 0x0002BFCC
		public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps)
		{
			LinkedList<SSAStep> linkedList = new LinkedList<SSAStep>(steps);
			IEnumerable<LinkedListNode<SSAStep>> enumerable = from node in linkedList.Nodes<SSAStep>()
				where node.Value.LValue.ImmediateDownLinks.Count == 1 || node.Value.RValue is SSAVariable
				select node;
			if (this._allowedToInlineCondition != null)
			{
				enumerable = enumerable.Where(this._allowedToInlineCondition);
			}
			List<LinkedListNode<SSAStep>> list = new List<LinkedListNode<SSAStep>>();
			foreach (LinkedListNode<SSAStep> linkedListNode in enumerable)
			{
				SSARegister lvalue = linkedListNode.Value.LValue;
				SSARValue rvalue = linkedListNode.Value.RValue;
				if (lvalue.ImmediateDownLinks.Any<SSAValue>())
				{
					list.Add(linkedListNode);
				}
				if (linkedListNode.Value.Comment != "")
				{
					HashSet<SSAValue> hashSet = lvalue.ImmediateDownLinks;
					for (;;)
					{
						HashSet<SSAValue> hashSet2 = hashSet.SelectMany(delegate(SSAValue x)
						{
							SSARegister ssaregister2 = x as SSARegister;
							if (ssaregister2 != null)
							{
								return new SSARegister[] { ssaregister2 };
							}
							return x.ImmediateDownLinks;
						}).ConvertToHashSet<SSAValue>();
						if (hashSet2.SetEquals(hashSet))
						{
							break;
						}
						hashSet = hashSet2;
					}
					foreach (SSAValue ssavalue in hashSet)
					{
						ssavalue.StepWhereDefined.Comment = ((ssavalue.StepWhereDefined.Comment == "") ? linkedListNode.Value.Comment : (ssavalue.StepWhereDefined.Comment + "; " + linkedListNode.Value.Comment));
					}
				}
				foreach (SSAValue ssavalue2 in lvalue.ImmediateDownLinks)
				{
					ssavalue2.ImmediateUpLinks.Remove(lvalue);
					SSAFunctionApplication ssafunctionApplication = ssavalue2 as SSAFunctionApplication;
					if (ssafunctionApplication == null)
					{
						SSARegister ssaregister = ssavalue2 as SSARegister;
						if (ssaregister == null)
						{
							string text = "Unexpected SSAValue type to inline: ";
							Type type = ssavalue2.GetType();
							string text2 = ((type != null) ? type.ToString() : null);
							string text3 = ": ";
							SSAValue ssavalue3 = ssavalue2;
							throw new NotImplementedException(text + text2 + text3 + ((ssavalue3 != null) ? ssavalue3.ToString() : null));
						}
						ssaregister.StepWhereDefined.RValue = rvalue;
					}
					else
					{
						ssafunctionApplication.SubstituteArgument(lvalue, rvalue);
					}
				}
			}
			foreach (LinkedListNode<SSAStep> linkedListNode2 in list)
			{
				linkedList.Remove(linkedListNode2);
			}
			return linkedList.ToList<SSAStep>();
		}

		// Token: 0x040007B9 RID: 1977
		private readonly Func<LinkedListNode<SSAStep>, bool> _allowedToInlineCondition;
	}
}
