using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000C4 RID: 196
	public class TruncatedCollection<T> : List<T>, ITruncatedCollection, IEnumerable, IEnumerable<T>, ICountOptionCollection
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x000166A3 File Offset: 0x000148A3
		public TruncatedCollection(IEnumerable<T> source, int pageSize)
			: base(source.Take(checked(pageSize + 1)))
		{
			this.Initialize(pageSize);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000166BB File Offset: 0x000148BB
		public TruncatedCollection(IQueryable<T> source, int pageSize)
			: this(source, pageSize, false)
		{
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000166C6 File Offset: 0x000148C6
		public TruncatedCollection(IQueryable<T> source, int pageSize, bool parameterize)
			: base(TruncatedCollection<T>.Take(source, pageSize, parameterize))
		{
			this.Initialize(pageSize);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000166DD File Offset: 0x000148DD
		public TruncatedCollection(IEnumerable<T> source, int pageSize, long? totalCount)
			: base((pageSize > 0) ? source.Take(checked(pageSize + 1)) : source)
		{
			if (pageSize > 0)
			{
				this.Initialize(pageSize);
			}
			this._totalCount = totalCount;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00016707 File Offset: 0x00014907
		public TruncatedCollection(IQueryable<T> source, int pageSize, long? totalCount)
			: this(source, pageSize, totalCount, false)
		{
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00016713 File Offset: 0x00014913
		public TruncatedCollection(IQueryable<T> source, int pageSize, long? totalCount, bool parameterize)
			: base((pageSize > 0) ? TruncatedCollection<T>.Take(source, pageSize, parameterize) : source)
		{
			if (pageSize > 0)
			{
				this.Initialize(pageSize);
			}
			this._totalCount = totalCount;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00016740 File Offset: 0x00014940
		private void Initialize(int pageSize)
		{
			if (pageSize < 1)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("pageSize", pageSize, 1);
			}
			this._pageSize = pageSize;
			if (base.Count > pageSize)
			{
				this._isTruncated = true;
				base.RemoveAt(base.Count - 1);
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001678D File Offset: 0x0001498D
		private static IQueryable<T> Take(IQueryable<T> source, int pageSize, bool parameterize)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return ExpressionHelpers.Take(source, checked(pageSize + 1), typeof(T), parameterize) as IQueryable<T>;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x000167B6 File Offset: 0x000149B6
		public int PageSize
		{
			get
			{
				return this._pageSize;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x000167BE File Offset: 0x000149BE
		public bool IsTruncated
		{
			get
			{
				return this._isTruncated;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x000167C6 File Offset: 0x000149C6
		public long? TotalCount
		{
			get
			{
				return this._totalCount;
			}
		}

		// Token: 0x0400018E RID: 398
		private const int MinPageSize = 1;

		// Token: 0x0400018F RID: 399
		private bool _isTruncated;

		// Token: 0x04000190 RID: 400
		private int _pageSize;

		// Token: 0x04000191 RID: 401
		private long? _totalCount;
	}
}
