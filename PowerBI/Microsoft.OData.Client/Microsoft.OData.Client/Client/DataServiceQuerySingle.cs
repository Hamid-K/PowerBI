using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.OData.Client
{
	// Token: 0x0200002A RID: 42
	public class DataServiceQuerySingle<TElement>
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00007CA7 File Offset: 0x00005EA7
		public DataServiceQuerySingle(DataServiceContext context, string path)
		{
			this.Context = context;
			this.Query = context.CreateSingletonQuery<TElement>(path);
			this.IsComposable = true;
			this.isFunction = false;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007CD1 File Offset: 0x00005ED1
		public DataServiceQuerySingle(DataServiceContext context, string path, bool isComposable)
		{
			this.Context = context;
			this.Query = context.CreateSingletonQuery<TElement>(path);
			this.IsComposable = isComposable;
			this.isFunction = true;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007CFB File Offset: 0x00005EFB
		public DataServiceQuerySingle(DataServiceQuerySingle<TElement> query)
		{
			this.Context = query.Context;
			this.Query = query.Query;
			this.IsComposable = query.IsComposable;
			this.isFunction = query.isFunction;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007D33 File Offset: 0x00005F33
		internal DataServiceQuerySingle(DataServiceQuery<TElement> query, bool isComposable)
		{
			this.Query = query;
			this.Context = query.Context;
			this.IsComposable = isComposable;
			this.isFunction = query.IsFunction;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007D61 File Offset: 0x00005F61
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00007D69 File Offset: 0x00005F69
		public DataServiceContext Context { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00007D72 File Offset: 0x00005F72
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007D7A File Offset: 0x00005F7A
		public bool IsComposable { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007D83 File Offset: 0x00005F83
		public Uri RequestUri
		{
			get
			{
				if (this.Query == null)
				{
					this.Query = this.Context.CreateSingletonQuery<TElement>(this.GetPath(null));
				}
				return this.Query.RequestUri;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007DB0 File Offset: 0x00005FB0
		public DataServiceQuery<T> CreateFunctionQuery<T>(string functionName, bool isComposable, params UriOperationParameter[] parameters)
		{
			return this.Query.CreateFunctionQuery<T>(functionName, isComposable, parameters);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public DataServiceQuerySingle<T> CreateFunctionQuerySingle<T>(string functionName, bool isComposable, params UriOperationParameter[] parameters)
		{
			return new DataServiceQuerySingle<T>(this.CreateFunctionQuery<T>(functionName, isComposable, parameters), isComposable);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007DD1 File Offset: 0x00005FD1
		public TElement GetValue()
		{
			if (this.isFunction)
			{
				return this.Context.Execute<TElement>(this.RequestUri, "GET", true, new OperationParameter[0]).SingleOrDefault<TElement>();
			}
			return this.Query.Execute().SingleOrDefault<TElement>();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007E0E File Offset: 0x0000600E
		public IAsyncResult BeginGetValue(AsyncCallback callback, object state)
		{
			if (this.isFunction)
			{
				return this.Context.BeginExecute<TElement>(this.RequestUri, callback, state, "GET", true, new OperationParameter[0]);
			}
			return this.Query.BeginExecute(callback, state);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007E45 File Offset: 0x00006045
		public Task<TElement> GetValueAsync()
		{
			return Task<TElement>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetValue), new Func<IAsyncResult, TElement>(this.EndGetValue), null);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007E6A File Offset: 0x0000606A
		public TElement EndGetValue(IAsyncResult asyncResult)
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			if (this.isFunction)
			{
				return this.Context.EndExecute<TElement>(asyncResult).SingleOrDefault<TElement>();
			}
			return this.Query.EndExecute(asyncResult).SingleOrDefault<TElement>();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007EA4 File Offset: 0x000060A4
		public string GetPath(string nextSegment)
		{
			string text = UriUtil.UriToString(this.RequestUri).Substring(UriUtil.UriToString(this.Context.BaseUri).Length);
			if (nextSegment != null)
			{
				return text + "/" + nextSegment;
			}
			return text;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007EE8 File Offset: 0x000060E8
		public string AppendRequestUri(string nextSegment)
		{
			return UriUtil.UriToString(this.RequestUri).Replace(this.RequestUri.AbsolutePath, this.RequestUri.AbsolutePath + "/" + nextSegment);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007F1B File Offset: 0x0000611B
		public DataServiceQuerySingle<TResult> Select<TResult>(Expression<Func<TElement, TResult>> selector)
		{
			if (this.Query == null)
			{
				this.Query = this.Context.CreateSingletonQuery<TElement>(this.GetPath(null));
			}
			return new DataServiceQuerySingle<TResult>((DataServiceQuery<TResult>)this.Query.Select(selector), true);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007F54 File Offset: 0x00006154
		public DataServiceQuerySingle<TElement> Expand<TTarget>(Expression<Func<TElement, TTarget>> navigationPropertyAccessor)
		{
			return new DataServiceQuerySingle<TElement>(this.Query.Expand<TTarget>(navigationPropertyAccessor), true);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007F68 File Offset: 0x00006168
		public DataServiceQuerySingle<TElement> Expand(string path)
		{
			return new DataServiceQuerySingle<TElement>(this.Query.Expand(path), true);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007F7C File Offset: 0x0000617C
		public DataServiceQuerySingle<TResult> CastTo<TResult>()
		{
			return new DataServiceQuerySingle<TResult>((DataServiceQuery<TResult>)this.Query.OfType<TResult>(), true);
		}

		// Token: 0x04000075 RID: 117
		internal DataServiceQuery<TElement> Query;

		// Token: 0x04000076 RID: 118
		private readonly bool isFunction;
	}
}
