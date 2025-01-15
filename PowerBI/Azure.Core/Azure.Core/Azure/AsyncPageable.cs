using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Azure
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AsyncPageable<T> : IAsyncEnumerable<T>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000022B8 File Offset: 0x000004B8
		protected virtual CancellationToken CancellationToken { get; }

		// Token: 0x06000038 RID: 56 RVA: 0x000022C0 File Offset: 0x000004C0
		protected AsyncPageable()
		{
			this.CancellationToken = CancellationToken.None;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000022D3 File Offset: 0x000004D3
		protected AsyncPageable(CancellationToken cancellationToken)
		{
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x0600003A RID: 58
		public abstract IAsyncEnumerable<Page<T>> AsPages([Nullable(2)] string continuationToken = null, int? pageSizeHint = null);

		// Token: 0x0600003B RID: 59 RVA: 0x000022E2 File Offset: 0x000004E2
		[AsyncIteratorStateMachine(typeof(AsyncPageable<>.<GetAsyncEnumerator>d__6))]
		public virtual IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			AsyncPageable<T>.<GetAsyncEnumerator>d__6 <GetAsyncEnumerator>d__ = new AsyncPageable<T>.<GetAsyncEnumerator>d__6(-3);
			<GetAsyncEnumerator>d__.<>4__this = this;
			<GetAsyncEnumerator>d__.cancellationToken = cancellationToken;
			return <GetAsyncEnumerator>d__;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000022F9 File Offset: 0x000004F9
		public static AsyncPageable<T> FromPages(IEnumerable<Page<T>> pages)
		{
			return new AsyncPageable<T>.StaticPageable(pages);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002301 File Offset: 0x00000501
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002309 File Offset: 0x00000509
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002312 File Offset: 0x00000512
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x020000CE RID: 206
		[Nullable(new byte[] { 0, 1 })]
		private class StaticPageable : AsyncPageable<T>
		{
			// Token: 0x060006D3 RID: 1747 RVA: 0x000170B0 File Offset: 0x000152B0
			public StaticPageable(IEnumerable<Page<T>> pages)
			{
				this._pages = pages;
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x000170BF File Offset: 0x000152BF
			[AsyncIteratorStateMachine(typeof(AsyncPageable<>.StaticPageable.<AsPages>d__2))]
			public override IAsyncEnumerable<Page<T>> AsPages([Nullable(2)] string continuationToken = null, int? pageSizeHint = null)
			{
				AsyncPageable<T>.StaticPageable.<AsPages>d__2 <AsPages>d__ = new AsyncPageable<T>.StaticPageable.<AsPages>d__2(-2);
				<AsPages>d__.<>4__this = this;
				<AsPages>d__.<>3__continuationToken = continuationToken;
				return <AsPages>d__;
			}

			// Token: 0x040002B3 RID: 691
			private readonly IEnumerable<Page<T>> _pages;
		}
	}
}
