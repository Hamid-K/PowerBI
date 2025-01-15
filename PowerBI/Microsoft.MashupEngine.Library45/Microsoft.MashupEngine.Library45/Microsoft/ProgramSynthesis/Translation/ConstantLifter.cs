using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002D3 RID: 723
	public class ConstantLifter
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0002D92A File Offset: 0x0002BB2A
		public IReadOnlyList<SSAStep> ConstantDefinitions
		{
			get
			{
				return this._constantDefinitions;
			}
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002D934 File Offset: 0x0002BB34
		public List<SSAStep> Optimize(IReadOnlyList<SSAStep> steps, OptimizeFor optimization)
		{
			if (optimization != OptimizeFor.Performance)
			{
				return new List<SSAStep>(steps);
			}
			LinkedList<SSAStep> linkedList = new LinkedList<SSAStep>(steps);
			IEnumerable<LinkedListNode<SSAStep>> enumerable = linkedList.Nodes<SSAStep>();
			HashSet<SSAValue> constantValues = new HashSet<SSAValue>();
			List<LinkedListNode<SSAStep>> list = new List<LinkedListNode<SSAStep>>();
			Func<SSAValue, bool> <>9__1;
			foreach (LinkedListNode<SSAStep> linkedListNode in enumerable)
			{
				SSAStep value = linkedListNode.Value;
				IEnumerable<SSAValue> allDependencies = value.LValue.AllDependencies;
				Func<SSAValue, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (SSAValue val) => !(val is SSAVariable) && (!(val is SSARegister) || constantValues.Contains(val)));
				}
				if (allDependencies.All(func))
				{
					constantValues.Add(value.LValue);
					list.Add(linkedListNode);
				}
			}
			foreach (LinkedListNode<SSAStep> linkedListNode2 in list)
			{
				linkedList.Remove(linkedListNode2);
			}
			this._constantDefinitions.AddRange(list.Select((LinkedListNode<SSAStep> node) => node.Value));
			return linkedList.ToList<SSAStep>();
		}

		// Token: 0x040007AC RID: 1964
		private readonly List<SSAStep> _constantDefinitions = new List<SSAStep>();
	}
}
