using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DA1 RID: 3489
	internal class CdpaDataflowGraph : Graph<ScopePath, CdpaDataflowNode, CdpaDataflowEdgeConstraint[]>
	{
		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x06005F05 RID: 24325 RVA: 0x00147E47 File Offset: 0x00146047
		// (set) Token: 0x06005F06 RID: 24326 RVA: 0x00147E4F File Offset: 0x0014604F
		public CdpaTimeRange TimeRange { get; set; }

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x06005F07 RID: 24327 RVA: 0x00147E58 File Offset: 0x00146058
		// (set) Token: 0x06005F08 RID: 24328 RVA: 0x00147E60 File Offset: 0x00146060
		public ITimeGranularity Granularity { get; set; }

		// Token: 0x06005F09 RID: 24329 RVA: 0x00147E6C File Offset: 0x0014606C
		public CdpaDataflowGraph ShallowCopy()
		{
			CdpaDataflowGraph cdpaDataflowGraph = new CdpaDataflowGraph();
			foreach (ScopePath scopePath in base.Keys)
			{
				cdpaDataflowGraph.Add(scopePath, base.GetNode(scopePath));
			}
			foreach (ScopePath scopePath2 in base.Keys)
			{
				foreach (ScopePath scopePath3 in base.KeysFrom(scopePath2))
				{
					cdpaDataflowGraph.Add(scopePath2, scopePath3, base.GetEdge(scopePath2, scopePath3));
				}
			}
			cdpaDataflowGraph.TimeRange = this.TimeRange;
			cdpaDataflowGraph.Granularity = this.Granularity;
			return cdpaDataflowGraph;
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override CdpaDataflowNode Merge(CdpaDataflowNode node1, CdpaDataflowNode node2)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x00147F64 File Offset: 0x00146164
		protected override CdpaDataflowEdgeConstraint[] Merge(CdpaDataflowEdgeConstraint[] edge1, CdpaDataflowEdgeConstraint[] edge2)
		{
			if (edge1 == null)
			{
				return edge2;
			}
			if (edge2 == null)
			{
				return edge1;
			}
			HashSet<CdpaDataflowEdgeConstraint> hashSet = new HashSet<CdpaDataflowEdgeConstraint>();
			TimePeriodCdpaDataflowEdgeConstraint timePeriodCdpaDataflowEdgeConstraint = null;
			foreach (CdpaDataflowEdgeConstraint cdpaDataflowEdgeConstraint in edge1.Concat(edge2))
			{
				TimePeriodCdpaDataflowEdgeConstraint timePeriodCdpaDataflowEdgeConstraint2 = cdpaDataflowEdgeConstraint as TimePeriodCdpaDataflowEdgeConstraint;
				if (timePeriodCdpaDataflowEdgeConstraint2 != null)
				{
					if (timePeriodCdpaDataflowEdgeConstraint == null)
					{
						timePeriodCdpaDataflowEdgeConstraint = timePeriodCdpaDataflowEdgeConstraint2;
					}
					else
					{
						timePeriodCdpaDataflowEdgeConstraint = new TimePeriodCdpaDataflowEdgeConstraint(TemporalExtensions.Min(timePeriodCdpaDataflowEdgeConstraint.Duration, timePeriodCdpaDataflowEdgeConstraint2.Duration));
					}
				}
				else
				{
					hashSet.Add(cdpaDataflowEdgeConstraint);
				}
			}
			if (timePeriodCdpaDataflowEdgeConstraint != null)
			{
				hashSet.Add(timePeriodCdpaDataflowEdgeConstraint);
			}
			return hashSet.ToArray<CdpaDataflowEdgeConstraint>();
		}
	}
}
