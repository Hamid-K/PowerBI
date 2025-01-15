using System;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000038 RID: 56
	[DebuggerDisplay("BoundedCapacity={BoundedCapacity}}")]
	internal class BoundingState
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00008E6C File Offset: 0x0000706C
		internal BoundingState(int boundedCapacity)
		{
			this.BoundedCapacity = boundedCapacity;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008E7B File Offset: 0x0000707B
		internal bool CountIsLessThanBound
		{
			get
			{
				return this.CurrentCount < this.BoundedCapacity;
			}
		}

		// Token: 0x0400008C RID: 140
		internal readonly int BoundedCapacity;

		// Token: 0x0400008D RID: 141
		internal int CurrentCount;
	}
}
