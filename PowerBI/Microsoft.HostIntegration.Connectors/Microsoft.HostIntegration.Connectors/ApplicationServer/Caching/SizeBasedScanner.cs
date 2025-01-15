using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000290 RID: 656
	internal abstract class SizeBasedScanner : IScanner
	{
		// Token: 0x0600180B RID: 6155 RVA: 0x00048DB3 File Offset: 0x00046FB3
		internal SizeBasedScanner(int size)
		{
			this._size = size;
		}

		// Token: 0x0600180C RID: 6156
		protected abstract void AddToBatch(AOMCacheItem item);

		// Token: 0x0600180D RID: 6157 RVA: 0x00048DC4 File Offset: 0x00046FC4
		protected bool Scan(AOMCacheItem item)
		{
			int size = item.Size;
			this._currentSize += size;
			if (this._currentSize < this._size)
			{
				this.AddToBatch(item);
				return true;
			}
			this._batchCompleted = true;
			if (this._currentSize - this._size <= this._size - this._currentSize + size || this._currentSize == size)
			{
				this.AddToBatch(item);
				return true;
			}
			this._currentSize -= size;
			return false;
		}

		// Token: 0x0600180E RID: 6158
		public abstract bool Scan(object item);

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00048E43 File Offset: 0x00047043
		public bool BatchCompleted
		{
			get
			{
				return this._batchCompleted;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001810 RID: 6160
		public abstract bool InvalidateOnChange { get; }

		// Token: 0x04000D3E RID: 3390
		private readonly int _size;

		// Token: 0x04000D3F RID: 3391
		private int _currentSize;

		// Token: 0x04000D40 RID: 3392
		private bool _batchCompleted;
	}
}
