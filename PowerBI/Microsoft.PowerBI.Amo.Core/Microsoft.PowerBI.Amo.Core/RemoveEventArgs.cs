using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000AE RID: 174
	public sealed class RemoveEventArgs : CollectionChangeEventArgs
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x00027EE8 File Offset: 0x000260E8
		internal RemoveEventArgs(object element, int index)
			: base(element)
		{
			this.index = index;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00027EFF File Offset: 0x000260FF
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x040004CC RID: 1228
		private int index = -1;
	}
}
