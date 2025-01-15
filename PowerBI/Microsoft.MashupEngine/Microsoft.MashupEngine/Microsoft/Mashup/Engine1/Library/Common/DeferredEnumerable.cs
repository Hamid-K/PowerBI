using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010AB RID: 4267
	internal static class DeferredEnumerable
	{
		// Token: 0x06006F91 RID: 28561 RVA: 0x0018138F File Offset: 0x0017F58F
		public static IEnumerable<T> From<T>(Func<IEnumerable<T>> provider)
		{
			return new DeferredEnumerable.DeferredEnumerableEnumerable<T>(provider);
		}

		// Token: 0x06006F92 RID: 28562 RVA: 0x00181397 File Offset: 0x0017F597
		public static IEnumerable<T> From<T>(Func<IEnumerator<T>> provider)
		{
			return new DeferredEnumerable.DeferredEnumeratorEnumerable<T>(provider);
		}

		// Token: 0x020010AC RID: 4268
		private sealed class DeferredEnumerableEnumerable<T> : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06006F93 RID: 28563 RVA: 0x0018139F File Offset: 0x0017F59F
			public DeferredEnumerableEnumerable(Func<IEnumerable<T>> rowsProvider)
			{
				this.rowsProvider = rowsProvider;
			}

			// Token: 0x06006F94 RID: 28564 RVA: 0x001813AE File Offset: 0x0017F5AE
			public IEnumerator<T> GetEnumerator()
			{
				if (this.rows == null)
				{
					this.rows = this.rowsProvider();
				}
				return this.rows.GetEnumerator();
			}

			// Token: 0x06006F95 RID: 28565 RVA: 0x001813D4 File Offset: 0x0017F5D4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04003DEE RID: 15854
			private readonly Func<IEnumerable<T>> rowsProvider;

			// Token: 0x04003DEF RID: 15855
			private IEnumerable<T> rows;
		}

		// Token: 0x020010AD RID: 4269
		private sealed class DeferredEnumeratorEnumerable<T> : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06006F96 RID: 28566 RVA: 0x001813DC File Offset: 0x0017F5DC
			public DeferredEnumeratorEnumerable(Func<IEnumerator<T>> provider)
			{
				this.provider = provider;
			}

			// Token: 0x06006F97 RID: 28567 RVA: 0x001813EB File Offset: 0x0017F5EB
			public IEnumerator<T> GetEnumerator()
			{
				return this.provider();
			}

			// Token: 0x06006F98 RID: 28568 RVA: 0x001813F8 File Offset: 0x0017F5F8
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04003DF0 RID: 15856
			private readonly Func<IEnumerator<T>> provider;
		}
	}
}
