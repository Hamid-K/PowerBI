using System;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000247 RID: 583
	internal static class IDbAsyncEnumeratorExtensions
	{
		// Token: 0x06001EAB RID: 7851 RVA: 0x000558B9 File Offset: 0x00053AB9
		public static Task<bool> MoveNextAsync(this IDbAsyncEnumerator enumerator)
		{
			Check.NotNull<IDbAsyncEnumerator>(enumerator, "enumerator");
			return enumerator.MoveNextAsync(CancellationToken.None);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000558D2 File Offset: 0x00053AD2
		internal static IDbAsyncEnumerator<TResult> Cast<TResult>(this IDbAsyncEnumerator source)
		{
			return new IDbAsyncEnumeratorExtensions.CastDbAsyncEnumerator<TResult>(source);
		}

		// Token: 0x02000973 RID: 2419
		private class CastDbAsyncEnumerator<TResult> : IDbAsyncEnumerator<TResult>, IDbAsyncEnumerator, IDisposable
		{
			// Token: 0x06005E06 RID: 24070 RVA: 0x00145656 File Offset: 0x00143856
			public CastDbAsyncEnumerator(IDbAsyncEnumerator sourceEnumerator)
			{
				this._underlyingEnumerator = sourceEnumerator;
			}

			// Token: 0x06005E07 RID: 24071 RVA: 0x00145665 File Offset: 0x00143865
			public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				return this._underlyingEnumerator.MoveNextAsync(cancellationToken);
			}

			// Token: 0x1700106E RID: 4206
			// (get) Token: 0x06005E08 RID: 24072 RVA: 0x00145673 File Offset: 0x00143873
			public TResult Current
			{
				get
				{
					return (TResult)((object)this._underlyingEnumerator.Current);
				}
			}

			// Token: 0x1700106F RID: 4207
			// (get) Token: 0x06005E09 RID: 24073 RVA: 0x00145685 File Offset: 0x00143885
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this._underlyingEnumerator.Current;
				}
			}

			// Token: 0x06005E0A RID: 24074 RVA: 0x00145692 File Offset: 0x00143892
			public void Dispose()
			{
				this._underlyingEnumerator.Dispose();
			}

			// Token: 0x040026E3 RID: 9955
			private readonly IDbAsyncEnumerator _underlyingEnumerator;
		}
	}
}
