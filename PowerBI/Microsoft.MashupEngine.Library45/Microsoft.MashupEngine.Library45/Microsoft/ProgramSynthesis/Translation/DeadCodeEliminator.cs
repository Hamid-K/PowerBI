using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002D6 RID: 726
	public class DeadCodeEliminator : IOptimizer
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x00002130 File Offset: 0x00000330
		private DeadCodeEliminator()
		{
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0002DABD File Offset: 0x0002BCBD
		public static DeadCodeEliminator Instance { get; } = new DeadCodeEliminator();

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
		public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps)
		{
			LinkedList<SSAStep> linkedList = new LinkedList<SSAStep>(steps);
			bool flag;
			do
			{
				flag = false;
				foreach (LinkedListNode<SSAStep> linkedListNode in from node in linkedList.Nodes<SSAStep>()
					where node.Value.LValue.IsDeadCode
					select node)
				{
					if (linkedListNode.Next != null)
					{
						linkedList.Remove(linkedListNode);
						SSARValue rvalue = linkedListNode.Value.RValue;
						foreach (SSAValue ssavalue in rvalue.ImmediateUpLinks)
						{
							ssavalue.ImmediateDownLinks.Remove(rvalue);
						}
						flag = true;
					}
				}
			}
			while (flag);
			return linkedList.ToList<SSAStep>();
		}
	}
}
