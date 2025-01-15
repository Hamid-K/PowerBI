using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000054 RID: 84
	[ImmutableObject(true)]
	public sealed class EdgeDescriptor
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000976D File Offset: 0x0000796D
		public EdgeDescriptor(int fromVertex, int toVertex, double cost)
		{
			this.From = fromVertex;
			this.To = toVertex;
			this.Cost = cost;
		}

		// Token: 0x040000B6 RID: 182
		public readonly int From;

		// Token: 0x040000B7 RID: 183
		public readonly int To;

		// Token: 0x040000B8 RID: 184
		public readonly double Cost;
	}
}
