using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Azure
{
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Pageable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000314E File Offset: 0x0000134E
		protected virtual CancellationToken CancellationToken { get; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00003156 File Offset: 0x00001356
		protected Pageable()
		{
			this.CancellationToken = CancellationToken.None;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003169 File Offset: 0x00001369
		protected Pageable(CancellationToken cancellationToken)
		{
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x060000B3 RID: 179
		public abstract IEnumerable<Page<T>> AsPages([Nullable(2)] string continuationToken = null, int? pageSizeHint = null);

		// Token: 0x060000B4 RID: 180 RVA: 0x00003178 File Offset: 0x00001378
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003180 File Offset: 0x00001380
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003188 File Offset: 0x00001388
		public virtual IEnumerator<T> GetEnumerator()
		{
			foreach (Page<T> page in this.AsPages(null, null))
			{
				foreach (T t in page.Values)
				{
					yield return t;
				}
				IEnumerator<T> enumerator2 = null;
			}
			IEnumerator<Page<T>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003197 File Offset: 0x00001397
		public static Pageable<T> FromPages(IEnumerable<Page<T>> pages)
		{
			return new Pageable<T>.StaticPageable(pages);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000319F File Offset: 0x0000139F
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000031A8 File Offset: 0x000013A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x020000D9 RID: 217
		[Nullable(new byte[] { 0, 1 })]
		private class StaticPageable : Pageable<T>
		{
			// Token: 0x060006F5 RID: 1781 RVA: 0x00017CD7 File Offset: 0x00015ED7
			public StaticPageable(IEnumerable<Page<T>> pages)
			{
				this._pages = pages;
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x00017CE6 File Offset: 0x00015EE6
			public override IEnumerable<Page<T>> AsPages([Nullable(2)] string continuationToken = null, int? pageSizeHint = null)
			{
				bool shouldReturnPages = continuationToken == null;
				foreach (Page<T> page in this._pages)
				{
					if (shouldReturnPages)
					{
						yield return page;
					}
					else if (continuationToken == page.ContinuationToken)
					{
						shouldReturnPages = true;
					}
				}
				IEnumerator<Page<T>> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x040002F1 RID: 753
			private readonly IEnumerable<Page<T>> _pages;
		}
	}
}
