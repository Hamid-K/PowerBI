using System;
using System.Collections.Generic;

namespace Microsoft.Reporting
{
	// Token: 0x020000C7 RID: 199
	internal sealed class StatefulEnumerator<T> : IDisposable
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x000214E4 File Offset: 0x0001F6E4
		internal StatefulEnumerator(IEnumerable<T> enumerable)
		{
			this._enumerator = enumerable.GetEnumerator();
			this._hasItem = false;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x000214FF File Offset: 0x0001F6FF
		public T Current
		{
			get
			{
				return this._enumerator.Current;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002150C File Offset: 0x0001F70C
		public bool HasItem
		{
			get
			{
				return this._hasItem;
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00021514 File Offset: 0x0001F714
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

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00021545 File Offset: 0x0001F745
		public T ConsumeCurrent()
		{
			T t = this.Current;
			this.MoveNext();
			return t;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00021554 File Offset: 0x0001F754
		public void Dispose()
		{
			if (this._enumerator != null)
			{
				this._enumerator.Dispose();
				this._enumerator = null;
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00021570 File Offset: 0x0001F770
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

		// Token: 0x0400096D RID: 2413
		private IEnumerator<T> _enumerator;

		// Token: 0x0400096E RID: 2414
		private bool _hasItem;
	}
}
