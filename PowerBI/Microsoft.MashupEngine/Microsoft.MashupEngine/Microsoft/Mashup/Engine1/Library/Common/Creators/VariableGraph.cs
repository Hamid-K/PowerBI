using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x020011B1 RID: 4529
	internal class VariableGraph<TVariableInitializer>
	{
		// Token: 0x060077A4 RID: 30628 RVA: 0x0019EAE1 File Offset: 0x0019CCE1
		public VariableGraph()
		{
			this.predecessors = new Dictionary<TVariableInitializer, HashSet<TVariableInitializer>>();
			this.sortedItems = null;
			this.successors = new Dictionary<TVariableInitializer, HashSet<TVariableInitializer>>();
			this.vertices = new HashSet<TVariableInitializer>();
		}

		// Token: 0x170020B9 RID: 8377
		// (get) Token: 0x060077A5 RID: 30629 RVA: 0x0019EB11 File Offset: 0x0019CD11
		public IEnumerable<TVariableInitializer> CyclicItems
		{
			get
			{
				return this.vertices.Except(this.SortedItems);
			}
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x060077A6 RID: 30630 RVA: 0x0019EB24 File Offset: 0x0019CD24
		public IList<TVariableInitializer> SortedItems
		{
			get
			{
				if (this.sortedItems == null)
				{
					this.Sort();
				}
				return this.sortedItems;
			}
		}

		// Token: 0x060077A7 RID: 30631 RVA: 0x0019EB3A File Offset: 0x0019CD3A
		public void AddEdge(TVariableInitializer independentVariable, TVariableInitializer dependentVariable)
		{
			this.sortedItems = null;
			this.predecessors[dependentVariable].Add(independentVariable);
			this.successors[independentVariable].Add(dependentVariable);
		}

		// Token: 0x060077A8 RID: 30632 RVA: 0x0019EB69 File Offset: 0x0019CD69
		public void AddVertex(TVariableInitializer variable)
		{
			this.sortedItems = null;
			if (this.vertices.Add(variable))
			{
				this.predecessors[variable] = new HashSet<TVariableInitializer>();
				this.successors[variable] = new HashSet<TVariableInitializer>();
			}
		}

		// Token: 0x060077A9 RID: 30633 RVA: 0x0019EBA4 File Offset: 0x0019CDA4
		private void Sort()
		{
			Dictionary<TVariableInitializer, int> dictionary = new Dictionary<TVariableInitializer, int>();
			Queue<TVariableInitializer> queue = new Queue<TVariableInitializer>();
			foreach (TVariableInitializer tvariableInitializer in this.vertices)
			{
				int count = this.predecessors[tvariableInitializer].Count;
				dictionary[tvariableInitializer] = count;
				if (count == 0)
				{
					queue.Enqueue(tvariableInitializer);
				}
			}
			this.sortedItems = new List<TVariableInitializer>();
			while (queue.Count > 0)
			{
				TVariableInitializer tvariableInitializer2 = queue.Dequeue();
				this.sortedItems.Add(tvariableInitializer2);
				foreach (TVariableInitializer tvariableInitializer3 in this.successors[tvariableInitializer2])
				{
					int num = dictionary[tvariableInitializer3] - 1;
					dictionary[tvariableInitializer3] = num;
					if (num == 0)
					{
						queue.Enqueue(tvariableInitializer3);
					}
				}
			}
		}

		// Token: 0x04004112 RID: 16658
		private readonly Dictionary<TVariableInitializer, HashSet<TVariableInitializer>> predecessors;

		// Token: 0x04004113 RID: 16659
		private IList<TVariableInitializer> sortedItems;

		// Token: 0x04004114 RID: 16660
		private readonly Dictionary<TVariableInitializer, HashSet<TVariableInitializer>> successors;

		// Token: 0x04004115 RID: 16661
		private readonly HashSet<TVariableInitializer> vertices;
	}
}
