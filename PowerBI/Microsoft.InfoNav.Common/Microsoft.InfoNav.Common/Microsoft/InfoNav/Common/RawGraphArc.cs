using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000052 RID: 82
	[ImmutableObject(true)]
	public struct RawGraphArc
	{
		// Token: 0x0600036D RID: 877 RVA: 0x00009754 File Offset: 0x00007954
		public RawGraphArc(int targetId, int edgeId)
		{
			this.TargetId = targetId;
			this.EdgeId = edgeId;
		}

		// Token: 0x040000B3 RID: 179
		public readonly int TargetId;

		// Token: 0x040000B4 RID: 180
		public readonly int EdgeId;
	}
}
