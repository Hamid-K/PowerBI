using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.OData.Client
{
	// Token: 0x02000014 RID: 20
	public sealed class DataServiceActionQuerySingle<T>
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000042A7 File Offset: 0x000024A7
		public DataServiceActionQuerySingle(DataServiceContext context, string requestUriString, params BodyOperationParameter[] parameters)
		{
			this.context = context;
			this.RequestUri = new Uri(requestUriString);
			this.parameters = parameters;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000042C9 File Offset: 0x000024C9
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000042D1 File Offset: 0x000024D1
		public Uri RequestUri { get; private set; }

		// Token: 0x0600008A RID: 138 RVA: 0x000042DA File Offset: 0x000024DA
		public T GetValue()
		{
			return this.context.Execute<T>(this.RequestUri, "POST", true, this.parameters).Single<T>();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000042FE File Offset: 0x000024FE
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IAsyncResult BeginGetValue(AsyncCallback callback, object state)
		{
			return this.context.BeginExecute<T>(this.RequestUri, callback, state, "POST", true, this.parameters);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000431F File Offset: 0x0000251F
		public Task<T> GetValueAsync()
		{
			return Task<T>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetValue), new Func<IAsyncResult, T>(this.EndGetValue), null);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004344 File Offset: 0x00002544
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public T EndGetValue(IAsyncResult asyncResult)
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			return this.context.EndExecute<T>(asyncResult).Single<T>();
		}

		// Token: 0x04000031 RID: 49
		private readonly DataServiceContext context;

		// Token: 0x04000032 RID: 50
		private readonly BodyOperationParameter[] parameters;
	}
}
