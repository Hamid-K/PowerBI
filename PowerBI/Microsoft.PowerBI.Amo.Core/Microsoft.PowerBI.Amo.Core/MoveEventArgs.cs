using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000A4 RID: 164
	public sealed class MoveEventArgs : CollectionChangeEventArgs
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x000272D1 File Offset: 0x000254D1
		internal MoveEventArgs(object element, int fromIndex, int toIndex)
			: base(element)
		{
			this.fromIndex = fromIndex;
			this.toIndex = toIndex;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x000272F6 File Offset: 0x000254F6
		public int FromIndex
		{
			get
			{
				return this.fromIndex;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x000272FE File Offset: 0x000254FE
		public int ToIndex
		{
			get
			{
				return this.toIndex;
			}
		}

		// Token: 0x040004A4 RID: 1188
		private int fromIndex = -1;

		// Token: 0x040004A5 RID: 1189
		private int toIndex = -1;
	}
}
