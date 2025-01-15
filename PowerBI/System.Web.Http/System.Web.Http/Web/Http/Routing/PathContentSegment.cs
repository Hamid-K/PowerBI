using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x02000166 RID: 358
	internal sealed class PathContentSegment : PathSegment
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x00018CC1 File Offset: 0x00016EC1
		public PathContentSegment(List<PathSubsegment> subsegments)
		{
			this.Subsegments = subsegments;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00018CD0 File Offset: 0x00016ED0
		public bool IsCatchAll
		{
			get
			{
				int count = this.Subsegments.Count;
				for (int i = 0; i < count; i++)
				{
					PathParameterSubsegment pathParameterSubsegment = this.Subsegments[i] as PathParameterSubsegment;
					if (pathParameterSubsegment != null && pathParameterSubsegment.IsCatchAll)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00018D15 File Offset: 0x00016F15
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x00018D1D File Offset: 0x00016F1D
		public List<PathSubsegment> Subsegments { get; private set; }
	}
}
