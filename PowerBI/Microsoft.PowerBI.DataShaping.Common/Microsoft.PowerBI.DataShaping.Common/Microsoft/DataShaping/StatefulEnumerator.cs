using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping
{
	// Token: 0x02000009 RID: 9
	internal sealed class StatefulEnumerator<T> : IDisposable
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002ED1 File Offset: 0x000010D1
		internal StatefulEnumerator(IEnumerable<T> enumerable)
		{
			this._enumerator = enumerable.GetEnumerator();
			this._hasItem = false;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002EEC File Offset: 0x000010EC
		public T Current
		{
			get
			{
				return this._enumerator.Current;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002EF9 File Offset: 0x000010F9
		public bool HasItem
		{
			get
			{
				return this._hasItem;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F01 File Offset: 0x00001101
		public bool MoveNext()
		{
			if (this._enumerator == null)
			{
				return false;
			}
			this._hasItem = this._enumerator.MoveNext();
			if (!this._hasItem)
			{
				this.Dispose();
			}
			return this._hasItem;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F32 File Offset: 0x00001132
		public T ConsumeCurrent()
		{
			T t = this.Current;
			this.MoveNext();
			return t;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F41 File Offset: 0x00001141
		public void Dispose()
		{
			if (this._enumerator != null)
			{
				this._enumerator.Dispose();
				this._enumerator = null;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F60 File Offset: 0x00001160
		public static StatefulEnumerator<T> CreateAtFirstItem(IEnumerable<T> enumerator)
		{
			StatefulEnumerator<T> statefulEnumerator = null;
			StatefulEnumerator<T> statefulEnumerator2;
			try
			{
				statefulEnumerator = new StatefulEnumerator<T>(enumerator);
				statefulEnumerator.MoveNext();
				statefulEnumerator2 = statefulEnumerator;
			}
			catch (Exception)
			{
				if (statefulEnumerator != null)
				{
					statefulEnumerator.Dispose();
				}
				throw;
			}
			return statefulEnumerator2;
		}

		// Token: 0x04000038 RID: 56
		private IEnumerator<T> _enumerator;

		// Token: 0x04000039 RID: 57
		private bool _hasItem;
	}
}
