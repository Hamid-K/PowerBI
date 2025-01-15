using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200006B RID: 107
	public sealed class AddEventArgs : CollectionChangeEventArgs
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x00022282 File Offset: 0x00020482
		internal AddEventArgs(object element, int index)
			: base(element)
		{
			this.index = index;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00022299 File Offset: 0x00020499
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04000408 RID: 1032
		private int index = -1;
	}
}
