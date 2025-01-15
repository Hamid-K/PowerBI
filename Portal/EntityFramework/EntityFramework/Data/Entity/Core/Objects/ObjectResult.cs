using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200041B RID: 1051
	public abstract class ObjectResult : IEnumerable, IDisposable, IListSource, IDbAsyncEnumerable
	{
		// Token: 0x06003274 RID: 12916 RVA: 0x000A1D55 File Offset: 0x0009FF55
		protected internal ObjectResult()
		{
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000A1D5D File Offset: 0x0009FF5D
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetAsyncEnumeratorInternal();
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000A1D65 File Offset: 0x0009FF65
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorInternal();
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000A1D6D File Offset: 0x0009FF6D
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000A1D70 File Offset: 0x0009FF70
		IList IListSource.GetList()
		{
			return this.GetIListSourceListInternal();
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06003279 RID: 12921
		public abstract Type ElementType { get; }

		// Token: 0x0600327A RID: 12922 RVA: 0x000A1D78 File Offset: 0x0009FF78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600327B RID: 12923
		protected abstract void Dispose(bool disposing);

		// Token: 0x0600327C RID: 12924 RVA: 0x000A1D87 File Offset: 0x0009FF87
		public virtual ObjectResult<TElement> GetNextResult<TElement>()
		{
			return this.GetNextResultInternal<TElement>();
		}

		// Token: 0x0600327D RID: 12925
		internal abstract IDbAsyncEnumerator GetAsyncEnumeratorInternal();

		// Token: 0x0600327E RID: 12926
		internal abstract IEnumerator GetEnumeratorInternal();

		// Token: 0x0600327F RID: 12927
		internal abstract IList GetIListSourceListInternal();

		// Token: 0x06003280 RID: 12928
		internal abstract ObjectResult<TElement> GetNextResultInternal<TElement>();
	}
}
