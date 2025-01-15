using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200001A RID: 26
	internal struct DisposableEnumeratorAdapter<[Nullable(2)] T, TEnumerator> : IDisposable where TEnumerator : struct, IEnumerator<T>
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002E58 File Offset: 0x00001058
		internal DisposableEnumeratorAdapter(TEnumerator enumerator)
		{
			this._enumeratorStruct = enumerator;
			this._enumeratorObject = null;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E68 File Offset: 0x00001068
		[NullableContext(1)]
		internal DisposableEnumeratorAdapter(IEnumerator<T> enumerator)
		{
			this._enumeratorStruct = default(TEnumerator);
			this._enumeratorObject = enumerator;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E7D File Offset: 0x0000107D
		[Nullable(1)]
		public T Current
		{
			[NullableContext(1)]
			get
			{
				if (this._enumeratorObject == null)
				{
					return this._enumeratorStruct.Current;
				}
				return this._enumeratorObject.Current;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EA4 File Offset: 0x000010A4
		public bool MoveNext()
		{
			if (this._enumeratorObject == null)
			{
				return this._enumeratorStruct.MoveNext();
			}
			return this._enumeratorObject.MoveNext();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002ECB File Offset: 0x000010CB
		public void Dispose()
		{
			if (this._enumeratorObject != null)
			{
				this._enumeratorObject.Dispose();
				return;
			}
			this._enumeratorStruct.Dispose();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002EF2 File Offset: 0x000010F2
		[return: Nullable(new byte[] { 0, 1, 0 })]
		public DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04000011 RID: 17
		private readonly IEnumerator<T> _enumeratorObject;

		// Token: 0x04000012 RID: 18
		private TEnumerator _enumeratorStruct;
	}
}
