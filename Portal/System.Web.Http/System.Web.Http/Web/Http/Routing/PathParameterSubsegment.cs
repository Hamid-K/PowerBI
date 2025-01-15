using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000168 RID: 360
	internal sealed class PathParameterSubsegment : PathSubsegment
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x00018D46 File Offset: 0x00016F46
		public PathParameterSubsegment(string parameterName)
		{
			if (parameterName.StartsWith("*", StringComparison.Ordinal))
			{
				this.ParameterName = parameterName.Substring(1);
				this.IsCatchAll = true;
				return;
			}
			this.ParameterName = parameterName;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00018D78 File Offset: 0x00016F78
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00018D80 File Offset: 0x00016F80
		public bool IsCatchAll { get; private set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00018D89 File Offset: 0x00016F89
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x00018D91 File Offset: 0x00016F91
		public string ParameterName { get; private set; }
	}
}
