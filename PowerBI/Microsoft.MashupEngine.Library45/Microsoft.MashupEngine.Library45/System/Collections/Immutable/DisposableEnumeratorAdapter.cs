using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002067 RID: 8295
	internal struct DisposableEnumeratorAdapter<[Nullable(2)] T, TEnumerator> : IDisposable where TEnumerator : struct, IEnumerator<T>
	{
		// Token: 0x060113FD RID: 70653 RVA: 0x003B60B8 File Offset: 0x003B42B8
		internal DisposableEnumeratorAdapter(TEnumerator enumerator)
		{
			this._enumeratorStruct = enumerator;
			this._enumeratorObject = null;
		}

		// Token: 0x060113FE RID: 70654 RVA: 0x003B60C8 File Offset: 0x003B42C8
		[NullableContext(1)]
		internal DisposableEnumeratorAdapter(IEnumerator<T> enumerator)
		{
			this._enumeratorStruct = default(TEnumerator);
			this._enumeratorObject = enumerator;
		}

		// Token: 0x17002E17 RID: 11799
		// (get) Token: 0x060113FF RID: 70655 RVA: 0x003B60DD File Offset: 0x003B42DD
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

		// Token: 0x06011400 RID: 70656 RVA: 0x003B6104 File Offset: 0x003B4304
		public bool MoveNext()
		{
			if (this._enumeratorObject == null)
			{
				return this._enumeratorStruct.MoveNext();
			}
			return this._enumeratorObject.MoveNext();
		}

		// Token: 0x06011401 RID: 70657 RVA: 0x003B612B File Offset: 0x003B432B
		public void Dispose()
		{
			if (this._enumeratorObject != null)
			{
				this._enumeratorObject.Dispose();
				return;
			}
			this._enumeratorStruct.Dispose();
		}

		// Token: 0x06011402 RID: 70658 RVA: 0x003B6152 File Offset: 0x003B4352
		[return: Nullable(new byte[] { 0, 1, 0 })]
		public DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerator()
		{
			return this;
		}

		// Token: 0x040068A4 RID: 26788
		private readonly IEnumerator<T> _enumeratorObject;

		// Token: 0x040068A5 RID: 26789
		private TEnumerator _enumeratorStruct;
	}
}
