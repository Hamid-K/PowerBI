using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.OData.Client
{
	// Token: 0x020000E9 RID: 233
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "required for this feature")]
	public class DataServiceQuery<TElement> : DataServiceQuery, IQueryable<TElement>, IEnumerable<TElement>, IEnumerable, IQueryable
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x000236EB File Offset: 0x000218EB
		public DataServiceQuery(Expression expression, DataServiceQueryProvider provider)
			: this(expression, provider, true)
		{
			this.IsFunction = false;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000236FD File Offset: 0x000218FD
		public DataServiceQuery(Expression expression, DataServiceQueryProvider provider, bool isComposable)
		{
			this.queryExpression = expression;
			this.queryProvider = provider;
			this.IsComposable = isComposable;
			this.IsFunction = true;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00023721 File Offset: 0x00021921
		public override Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0002372D File Offset: 0x0002192D
		public override Expression Expression
		{
			get
			{
				return this.queryExpression;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00023735 File Offset: 0x00021935
		public override IQueryProvider Provider
		{
			get
			{
				return this.queryProvider;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0002373D File Offset: 0x0002193D
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0002374A File Offset: 0x0002194A
		public override Uri RequestUri
		{
			get
			{
				return this.Translate().Uri;
			}
			internal set
			{
				this.Translate().Uri = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00023758 File Offset: 0x00021958
		public DataServiceContext Context
		{
			get
			{
				return this.queryProvider.Context;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00023765 File Offset: 0x00021965
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0002376D File Offset: 0x0002196D
		public bool IsComposable { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00023776 File Offset: 0x00021976
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0002377E File Offset: 0x0002197E
		internal bool IsFunction { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00003487 File Offset: 0x00001687
		internal override ProjectionPlan Plan
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00023788 File Offset: 0x00021988
		public string GetKeyPath(string keyString)
		{
			string text = UriUtil.UriToString(this.RequestUri).Substring(UriUtil.UriToString(this.Context.BaseUri).Length);
			if (this.Context.UrlKeyDelimiter == DataServiceUrlKeyDelimiter.Slash)
			{
				return text + "/" + keyString;
			}
			return text + "(" + keyString + ")";
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000237EC File Offset: 0x000219EC
		public DataServiceQuery<T> CreateFunctionQuery<T>(string functionName, bool isComposable, params UriOperationParameter[] parameters)
		{
			Dictionary<string, string> dictionary = this.Context.SerializeOperationParameters(parameters);
			ResourceSetExpression resourceSetExpression = new ResourceSetExpression(typeof(IOrderedQueryable<T>), this.Expression, null, typeof(T), null, CountOption.None, null, null, null, null, functionName, dictionary, false);
			return new DataServiceQuery<T>.DataServiceOrderedQuery(resourceSetExpression, new DataServiceQueryProvider(this.Context), isComposable);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00023842 File Offset: 0x00021A42
		public DataServiceQuerySingle<T> CreateFunctionQuerySingle<T>(string functionName, bool isComposable, params UriOperationParameter[] parameters)
		{
			return new DataServiceQuerySingle<T>(this.CreateFunctionQuery<T>(functionName, isComposable, parameters), isComposable);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00023854 File Offset: 0x00021A54
		public string AppendRequestUri(string nextSegment)
		{
			Uri requestUri = this.RequestUri;
			return UriUtil.UriToString(requestUri).Replace(requestUri.AbsolutePath, requestUri.AbsolutePath + "/" + nextSegment);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002388C File Offset: 0x00021A8C
		public string GetPath(string nextSegment)
		{
			string text = UriUtil.UriToString(this.RequestUri).Substring(UriUtil.UriToString(this.Context.BaseUri).Length);
			return text + "/" + nextSegment;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000238CB File Offset: 0x00021ACB
		public new IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			if (this.IsFunction)
			{
				return this.Context.BeginExecute<TElement>(this.RequestUri, callback, state, "GET", false, new OperationParameter[0]);
			}
			return base.BeginExecute(this, this.Context, callback, state, "Execute");
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00023909 File Offset: 0x00021B09
		public new Task<IEnumerable<TElement>> ExecuteAsync()
		{
			return Task<IEnumerable<TElement>>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginExecute), new Func<IAsyncResult, IEnumerable<TElement>>(this.EndExecute), null);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002392E File Offset: 0x00021B2E
		public new IEnumerable<TElement> EndExecute(IAsyncResult asyncResult)
		{
			if (this.IsFunction)
			{
				return this.Context.EndExecute<TElement>(asyncResult);
			}
			return DataServiceRequest.EndExecute<TElement>(this, this.Context, "Execute", asyncResult);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00023958 File Offset: 0x00021B58
		public Task<IEnumerable<TElement>> GetAllPagesAsync()
		{
			Task<IEnumerable<TElement>> task = Task<IEnumerable<TElement>>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginExecute), new Func<IAsyncResult, IEnumerable<TElement>>(this.EndExecute), null);
			return task.ContinueWith<IEnumerable<TElement>>((Task<IEnumerable<TElement>> t) => this.ContinuePage(t.Result));
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002399D File Offset: 0x00021B9D
		public new IEnumerable<TElement> Execute()
		{
			if (this.IsFunction)
			{
				return this.Context.Execute<TElement>(this.RequestUri, "GET", false, new OperationParameter[0]);
			}
			return base.Execute<TElement>(this.Context, this.Translate());
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000239D8 File Offset: 0x00021BD8
		public IEnumerable<TElement> GetAllPages()
		{
			QueryOperationResponse<TElement> queryOperationResponse = base.Execute<TElement>(this.Context, this.Translate());
			return this.GetRestPages(queryOperationResponse);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00023A00 File Offset: 0x00021C00
		public DataServiceQuery<TElement> Expand(string path)
		{
			Util.CheckArgumentNullAndEmpty(path, "path");
			return (DataServiceQuery<TElement>)this.Provider.CreateQuery<TElement>(Expression.Call(Expression.Convert(this.Expression, typeof(DataServiceQuery<TElement>.DataServiceOrderedQuery)), DataServiceQuery<TElement>.expandMethodInfo, new Expression[] { Expression.Constant(path) }));
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00023A58 File Offset: 0x00021C58
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
		public DataServiceQuery<TElement> Expand<TTarget>(Expression<Func<TElement, TTarget>> navigationPropertyAccessor)
		{
			Util.CheckArgumentNull<Expression<Func<TElement, TTarget>>>(navigationPropertyAccessor, "navigationPropertyAccessor");
			MethodInfo methodInfo = DataServiceQuery<TElement>.expandGenericMethodInfo.MakeGenericMethod(new Type[] { typeof(TTarget) });
			return (DataServiceQuery<TElement>)this.Provider.CreateQuery<TElement>(Expression.Call(Expression.Convert(this.Expression, typeof(DataServiceQuery<TElement>.DataServiceOrderedQuery)), methodInfo, new Expression[] { navigationPropertyAccessor }));
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00023AC4 File Offset: 0x00021CC4
		public DataServiceQuery<TElement> IncludeTotalCount()
		{
			MethodInfo method = typeof(DataServiceQuery<TElement>).GetMethod("IncludeTotalCount");
			return (DataServiceQuery<TElement>)this.Provider.CreateQuery<TElement>(Expression.Call(Expression.Convert(this.Expression, typeof(DataServiceQuery<TElement>.DataServiceOrderedQuery)), method));
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00023B14 File Offset: 0x00021D14
		public DataServiceQuery<TElement> AddQueryOption(string name, object value)
		{
			Util.CheckArgumentNull<string>(name, "name");
			Util.CheckArgumentNull<object>(value, "value");
			MethodInfo method = typeof(DataServiceQuery<TElement>).GetMethod("AddQueryOption");
			return (DataServiceQuery<TElement>)this.Provider.CreateQuery<TElement>(Expression.Call(Expression.Convert(this.Expression, typeof(DataServiceQuery<TElement>.DataServiceOrderedQuery)), method, new Expression[]
			{
				Expression.Constant(name),
				Expression.Constant(value, typeof(object))
			}));
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00023B9B File Offset: 0x00021D9B
		public IEnumerator<TElement> GetEnumerator()
		{
			return this.Execute().GetEnumerator();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00023BA8 File Offset: 0x00021DA8
		public override string ToString()
		{
			string text;
			try
			{
				text = this.QueryComponents(this.Context.Model).Uri.ToString();
			}
			catch (NotSupportedException ex)
			{
				text = Strings.ALinq_TranslationError(ex.Message);
			}
			return text;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00023BF4 File Offset: 0x00021DF4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00023BFC File Offset: 0x00021DFC
		internal override QueryComponents QueryComponents(ClientEdmModel model)
		{
			return this.Translate();
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00023C04 File Offset: 0x00021E04
		internal override IEnumerable ExecuteInternal()
		{
			return this.Execute();
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00023C0C File Offset: 0x00021E0C
		internal override IAsyncResult BeginExecuteInternal(AsyncCallback callback, object state)
		{
			return this.BeginExecute(callback, state);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00023C16 File Offset: 0x00021E16
		internal override IEnumerable EndExecuteInternal(IAsyncResult asyncResult)
		{
			return this.EndExecute(asyncResult);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00023C1F File Offset: 0x00021E1F
		private QueryComponents Translate()
		{
			if (this.queryComponents == null)
			{
				this.queryComponents = this.queryProvider.Translate(this.queryExpression);
			}
			return this.queryComponents;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00023C46 File Offset: 0x00021E46
		private IEnumerable<TElement> ContinuePage(IEnumerable<TElement> response)
		{
			foreach (TElement telement in response)
			{
				yield return telement;
			}
			IEnumerator<TElement> enumerator = null;
			DataServiceQueryContinuation<TElement> dataServiceQueryContinuation = (response as QueryOperationResponse).GetContinuation() as DataServiceQueryContinuation<TElement>;
			if (dataServiceQueryContinuation != null)
			{
				Task<IEnumerable<TElement>> task = Task<IEnumerable<TElement>>.Factory.FromAsync(this.Context.BeginExecute<TElement>(dataServiceQueryContinuation, null, null), new Func<IAsyncResult, IEnumerable<TElement>>(this.Context.EndExecute<TElement>));
				Task<IEnumerable<TElement>> task2 = task.ContinueWith<IEnumerable<TElement>>((Task<IEnumerable<TElement>> t) => this.ContinuePage(t.Result));
				task2.Wait();
				foreach (TElement telement2 in task2.Result)
				{
					yield return telement2;
				}
				enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00023C5D File Offset: 0x00021E5D
		private IEnumerable<TElement> GetRestPages(IEnumerable<TElement> response)
		{
			foreach (TElement telement in response)
			{
				yield return telement;
			}
			IEnumerator<TElement> enumerator = null;
			for (DataServiceQueryContinuation<TElement> dataServiceQueryContinuation = (response as QueryOperationResponse<TElement>).GetContinuation(); dataServiceQueryContinuation != null; dataServiceQueryContinuation = (response as QueryOperationResponse<TElement>).GetContinuation())
			{
				response = this.Context.Execute<TElement>(dataServiceQueryContinuation);
				foreach (TElement telement2 in response)
				{
					yield return telement2;
				}
				enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x040003A2 RID: 930
		private static readonly MethodInfo expandMethodInfo = typeof(DataServiceQuery<TElement>).GetMethod("Expand", new Type[] { typeof(string) });

		// Token: 0x040003A3 RID: 931
		private static readonly MethodInfo expandGenericMethodInfo = (MethodInfo)typeof(DataServiceQuery<TElement>).GetMember("Expand*").Single((MemberInfo m) => ((MethodInfo)m).GetGenericArguments().Count<Type>() == 1);

		// Token: 0x040003A4 RID: 932
		private readonly Expression queryExpression;

		// Token: 0x040003A5 RID: 933
		private readonly DataServiceQueryProvider queryProvider;

		// Token: 0x040003A6 RID: 934
		private QueryComponents queryComponents;

		// Token: 0x020001BC RID: 444
		public class DataServiceOrderedQuery : DataServiceQuery<TElement>, IOrderedQueryable<TElement>, IQueryable<TElement>, IEnumerable<TElement>, IEnumerable, IQueryable, IOrderedQueryable
		{
			// Token: 0x06000EE1 RID: 3809 RVA: 0x00031C96 File Offset: 0x0002FE96
			internal DataServiceOrderedQuery(Expression expression, DataServiceQueryProvider provider)
				: base(expression, provider)
			{
			}

			// Token: 0x06000EE2 RID: 3810 RVA: 0x00031CA0 File Offset: 0x0002FEA0
			internal DataServiceOrderedQuery(Expression expression, DataServiceQueryProvider provider, bool isComposable)
				: base(expression, provider, isComposable)
			{
			}
		}
	}
}
