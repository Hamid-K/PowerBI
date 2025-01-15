using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200020C RID: 524
	internal sealed class QueueNode : INodeWrapper
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00038171 File Offset: 0x00036371
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x0003817C File Offset: 0x0003637C
		public object Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
				INodeWrapperBackReference nodeWrapperBackReference = this._data as INodeWrapperBackReference;
				if (nodeWrapperBackReference != null)
				{
					nodeWrapperBackReference.BackReference = this;
				}
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000381A6 File Offset: 0x000363A6
		public QueueNode(object data)
		{
			this.Data = data;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000381C0 File Offset: 0x000363C0
		internal void Clear()
		{
			INodeWrapperBackReference nodeWrapperBackReference = this._data as INodeWrapperBackReference;
			if (nodeWrapperBackReference != null)
			{
				nodeWrapperBackReference.BackReference = null;
			}
			this._data = null;
		}

		// Token: 0x04000ADF RID: 2783
		internal static readonly QueueNode NullObject = new QueueNode("NULL");

		// Token: 0x04000AE0 RID: 2784
		private object _data;

		// Token: 0x04000AE1 RID: 2785
		public QueueNode Next = QueueNode.NullObject;
	}
}
