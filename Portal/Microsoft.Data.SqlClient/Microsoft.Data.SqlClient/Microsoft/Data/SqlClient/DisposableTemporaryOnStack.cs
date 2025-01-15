using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000027 RID: 39
	internal ref struct DisposableTemporaryOnStack<T> where T : IDisposable
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0000D652 File Offset: 0x0000B852
		public void Set(T value)
		{
			this._value = value;
			this._hasValue = true;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000D664 File Offset: 0x0000B864
		public T Take()
		{
			T value = this._value;
			this._value = default(T);
			this._hasValue = false;
			return value;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000D68C File Offset: 0x0000B88C
		public void Dispose()
		{
			if (this._hasValue)
			{
				this._value.Dispose();
				this._value = default(T);
				this._hasValue = false;
			}
		}

		// Token: 0x04000087 RID: 135
		private T _value;

		// Token: 0x04000088 RID: 136
		private bool _hasValue;
	}
}
