using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200003E RID: 62
	public sealed class DelegatingEnumerable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000250 RID: 592 RVA: 0x000080F2 File Offset: 0x000062F2
		public DelegatingEnumerable()
		{
			this._source = Enumerable.Empty<T>();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008105 File Offset: 0x00006305
		public DelegatingEnumerable(IEnumerable<T> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this._source = source;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008122 File Offset: 0x00006322
		public IEnumerator<T> GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000653F File Offset: 0x0000473F
		public void Add(object item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008122 File Offset: 0x00006322
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._source.GetEnumerator();
		}

		// Token: 0x040000AA RID: 170
		private IEnumerable<T> _source;
	}
}
