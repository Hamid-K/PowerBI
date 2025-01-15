using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000122 RID: 290
	internal class LazyEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x0600143D RID: 5181 RVA: 0x000347B1 File Offset: 0x000329B1
		public LazyEnumerator(Func<ObjectResult<T>> getObjectResult)
		{
			this._getObjectResult = getObjectResult;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x000347C0 File Offset: 0x000329C0
		public T Current
		{
			get
			{
				if (this._objectResultEnumerator != null)
				{
					return this._objectResultEnumerator.Current;
				}
				return default(T);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x000347EA File Offset: 0x000329EA
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x000347F7 File Offset: 0x000329F7
		public void Dispose()
		{
			if (this._objectResultEnumerator != null)
			{
				this._objectResultEnumerator.Dispose();
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0003480C File Offset: 0x00032A0C
		public bool MoveNext()
		{
			if (this._objectResultEnumerator == null)
			{
				ObjectResult<T> objectResult = this._getObjectResult();
				try
				{
					this._objectResultEnumerator = objectResult.GetEnumerator();
				}
				catch
				{
					objectResult.Dispose();
					throw;
				}
			}
			return this._objectResultEnumerator.MoveNext();
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00034860 File Offset: 0x00032A60
		public void Reset()
		{
			if (this._objectResultEnumerator != null)
			{
				this._objectResultEnumerator.Reset();
			}
		}

		// Token: 0x04000986 RID: 2438
		private readonly Func<ObjectResult<T>> _getObjectResult;

		// Token: 0x04000987 RID: 2439
		private IEnumerator<T> _objectResultEnumerator;
	}
}
