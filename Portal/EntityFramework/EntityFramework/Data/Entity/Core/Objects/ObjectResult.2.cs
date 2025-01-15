using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200041C RID: 1052
	public class ObjectResult<T> : ObjectResult, IEnumerable<T>, IEnumerable, IDbAsyncEnumerable<T>, IDbAsyncEnumerable
	{
		// Token: 0x06003281 RID: 12929 RVA: 0x000A1D8F File Offset: 0x0009FF8F
		protected ObjectResult()
		{
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000A1D97 File Offset: 0x0009FF97
		internal ObjectResult(Shaper<T> shaper, EntitySet singleEntitySet, TypeUsage resultItemType)
			: this(shaper, singleEntitySet, resultItemType, true, true, null)
		{
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000A1DA8 File Offset: 0x0009FFA8
		internal ObjectResult(Shaper<T> shaper, EntitySet singleEntitySet, TypeUsage resultItemType, bool readerOwned, bool shouldReleaseConnection, DbCommand command = null)
			: this(shaper, singleEntitySet, resultItemType, readerOwned, shouldReleaseConnection, null, null, command)
		{
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000A1DC8 File Offset: 0x0009FFC8
		internal ObjectResult(Shaper<T> shaper, EntitySet singleEntitySet, TypeUsage resultItemType, bool readerOwned, bool shouldReleaseConnection, NextResultGenerator nextResultGenerator, Action<object, EventArgs> onReaderDispose, DbCommand command = null)
		{
			this._shaper = shaper;
			this._reader = this._shaper.Reader;
			this._command = command;
			this._singleEntitySet = singleEntitySet;
			this._resultItemType = resultItemType;
			this._readerOwned = readerOwned;
			this._shouldReleaseConnection = shouldReleaseConnection;
			this._nextResultGenerator = nextResultGenerator;
			this._onReaderDispose = onReaderDispose;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000A1E29 File Offset: 0x000A0029
		private void EnsureCanEnumerateResults()
		{
			if (this._shaper == null)
			{
				throw new InvalidOperationException(Strings.Materializer_CannotReEnumerateQueryResults);
			}
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000A1E3E File Offset: 0x000A003E
		public virtual IEnumerator<T> GetEnumerator()
		{
			return this.GetDbEnumerator();
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000A1E46 File Offset: 0x000A0046
		internal virtual IDbEnumerator<T> GetDbEnumerator()
		{
			this.EnsureCanEnumerateResults();
			Shaper<T> shaper = this._shaper;
			this._shaper = null;
			return shaper.GetEnumerator();
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000A1E60 File Offset: 0x000A0060
		IDbAsyncEnumerator<T> IDbAsyncEnumerable<T>.GetAsyncEnumerator()
		{
			return this.GetDbEnumerator();
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000A1E68 File Offset: 0x000A0068
		protected override void Dispose(bool disposing)
		{
			DbDataReader reader = this._reader;
			this._reader = null;
			this._nextResultGenerator = null;
			if (reader != null && this._readerOwned)
			{
				reader.Dispose();
				if (this._onReaderDispose != null)
				{
					this._onReaderDispose(this, new EventArgs());
					this._onReaderDispose = null;
				}
			}
			if (this._shaper != null)
			{
				if (this._shaper.Context != null && this._readerOwned && this._shouldReleaseConnection)
				{
					this._shaper.Context.ReleaseConnection();
				}
				this._shaper = null;
			}
			if (this._command != null)
			{
				this._command.Dispose();
				this._command = null;
			}
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000A1F11 File Offset: 0x000A0111
		internal override IDbAsyncEnumerator GetAsyncEnumeratorInternal()
		{
			return this.GetDbEnumerator();
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000A1F19 File Offset: 0x000A0119
		internal override IEnumerator GetEnumeratorInternal()
		{
			return this.GetDbEnumerator();
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000A1F24 File Offset: 0x000A0124
		internal override IList GetIListSourceListInternal()
		{
			if (this._cachedBindingList == null)
			{
				this.EnsureCanEnumerateResults();
				bool flag = this._shaper.MergeOption == MergeOption.NoTracking;
				this._cachedBindingList = ObjectViewFactory.CreateViewForQuery<T>(this._resultItemType, this, this._shaper.Context, flag, this._singleEntitySet);
			}
			return this._cachedBindingList;
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000A1F78 File Offset: 0x000A0178
		internal override ObjectResult<TElement> GetNextResultInternal<TElement>()
		{
			if (this._nextResultGenerator == null)
			{
				return null;
			}
			return this._nextResultGenerator.GetNextResult<TElement>(this._reader);
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000A1F95 File Offset: 0x000A0195
		public override Type ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x0400107B RID: 4219
		private Shaper<T> _shaper;

		// Token: 0x0400107C RID: 4220
		private DbDataReader _reader;

		// Token: 0x0400107D RID: 4221
		private DbCommand _command;

		// Token: 0x0400107E RID: 4222
		private readonly EntitySet _singleEntitySet;

		// Token: 0x0400107F RID: 4223
		private readonly TypeUsage _resultItemType;

		// Token: 0x04001080 RID: 4224
		private readonly bool _readerOwned;

		// Token: 0x04001081 RID: 4225
		private readonly bool _shouldReleaseConnection;

		// Token: 0x04001082 RID: 4226
		private IBindingList _cachedBindingList;

		// Token: 0x04001083 RID: 4227
		private NextResultGenerator _nextResultGenerator;

		// Token: 0x04001084 RID: 4228
		private Action<object, EventArgs> _onReaderDispose;
	}
}
