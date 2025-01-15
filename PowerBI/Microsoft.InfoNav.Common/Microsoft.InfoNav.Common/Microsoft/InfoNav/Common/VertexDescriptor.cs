using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000053 RID: 83
	[ImmutableObject(true)]
	public struct VertexDescriptor
	{
		// Token: 0x0600036E RID: 878 RVA: 0x00009764 File Offset: 0x00007964
		public VertexDescriptor(double cost)
		{
			this.Cost = cost;
		}

		// Token: 0x040000B5 RID: 181
		public readonly double Cost;
	}
}
