using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000344 RID: 836
	internal class GroupAggregateVarInfo
	{
		// Token: 0x060027D3 RID: 10195 RVA: 0x00075CD8 File Offset: 0x00073ED8
		internal GroupAggregateVarInfo(Node defingingGroupNode, Var groupAggregateVar)
		{
			this._definingGroupByNode = defingingGroupNode;
			this._groupAggregateVar = groupAggregateVar;
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x00075CEE File Offset: 0x00073EEE
		internal HashSet<KeyValuePair<Node, List<Node>>> CandidateAggregateNodes
		{
			get
			{
				if (this._candidateAggregateNodes == null)
				{
					this._candidateAggregateNodes = new HashSet<KeyValuePair<Node, List<Node>>>();
				}
				return this._candidateAggregateNodes;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x00075D09 File Offset: 0x00073F09
		internal bool HasCandidateAggregateNodes
		{
			get
			{
				return this._candidateAggregateNodes != null && this._candidateAggregateNodes.Count != 0;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x00075D23 File Offset: 0x00073F23
		internal Node DefiningGroupNode
		{
			get
			{
				return this._definingGroupByNode;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x00075D2B File Offset: 0x00073F2B
		internal Var GroupAggregateVar
		{
			get
			{
				return this._groupAggregateVar;
			}
		}

		// Token: 0x04000DE8 RID: 3560
		private readonly Node _definingGroupByNode;

		// Token: 0x04000DE9 RID: 3561
		private HashSet<KeyValuePair<Node, List<Node>>> _candidateAggregateNodes;

		// Token: 0x04000DEA RID: 3562
		private readonly Var _groupAggregateVar;
	}
}
