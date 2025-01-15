using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200007A RID: 122
	public abstract class CollectionChangeEventArgs : EventArgs
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x000238D3 File Offset: 0x00021AD3
		internal CollectionChangeEventArgs(object element)
		{
			this.element = element;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000238E2 File Offset: 0x00021AE2
		public object Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x04000429 RID: 1065
		private object element;
	}
}
