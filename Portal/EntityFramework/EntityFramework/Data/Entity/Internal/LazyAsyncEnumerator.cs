using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000121 RID: 289
	internal class LazyAsyncEnumerator<T> : IDbAsyncEnumerator<T>, IDbAsyncEnumerator, IDisposable
	{
		// Token: 0x06001437 RID: 5175 RVA: 0x000346E0 File Offset: 0x000328E0
		public LazyAsyncEnumerator(Func<CancellationToken, Task<ObjectResult<T>>> getObjectResultAsync)
		{
			this._getObjectResultAsync = getObjectResultAsync;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x000346F0 File Offset: 0x000328F0
		public T Current
		{
			get
			{
				if (this._objectResultAsyncEnumerator != null)
				{
					return this._objectResultAsyncEnumerator.Current;
				}
				return default(T);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0003471A File Offset: 0x0003291A
		object IDbAsyncEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00034727 File Offset: 0x00032927
		public void Dispose()
		{
			if (this._objectResultAsyncEnumerator != null)
			{
				this._objectResultAsyncEnumerator.Dispose();
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0003473C File Offset: 0x0003293C
		public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this._objectResultAsyncEnumerator != null)
			{
				return this._objectResultAsyncEnumerator.MoveNextAsync(cancellationToken);
			}
			return this.FirstMoveNextAsync(cancellationToken);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00034764 File Offset: 0x00032964
		private async Task<bool> FirstMoveNextAsync(CancellationToken cancellationToken)
		{
			ObjectResult<T> objectResult = await this._getObjectResultAsync(cancellationToken).WithCurrentCulture<ObjectResult<T>>();
			try
			{
				this._objectResultAsyncEnumerator = ((IDbAsyncEnumerable<T>)objectResult).GetAsyncEnumerator();
			}
			catch
			{
				objectResult.Dispose();
				throw;
			}
			return await this._objectResultAsyncEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>();
		}

		// Token: 0x04000984 RID: 2436
		private readonly Func<CancellationToken, Task<ObjectResult<T>>> _getObjectResultAsync;

		// Token: 0x04000985 RID: 2437
		private IDbAsyncEnumerator<T> _objectResultAsyncEnumerator;
	}
}
