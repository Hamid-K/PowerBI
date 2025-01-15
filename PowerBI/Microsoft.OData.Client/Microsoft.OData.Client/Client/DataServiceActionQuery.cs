using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.OData.Client
{
	// Token: 0x02000010 RID: 16
	public sealed class DataServiceActionQuery<T>
	{
		// Token: 0x06000064 RID: 100 RVA: 0x0000389E File Offset: 0x00001A9E
		public DataServiceActionQuery(DataServiceContext context, string requestUriString, params BodyOperationParameter[] parameters)
		{
			this.Context = context;
			this.RequestUri = new Uri(requestUriString);
			this.Parameters = parameters;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000038C0 File Offset: 0x00001AC0
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000038C8 File Offset: 0x00001AC8
		public Uri RequestUri { get; private set; }

		// Token: 0x06000067 RID: 103 RVA: 0x000038D1 File Offset: 0x00001AD1
		public IEnumerable<T> Execute()
		{
			return this.Context.Execute<T>(this.RequestUri, "POST", false, this.Parameters);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000038F0 File Offset: 0x00001AF0
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			return this.Context.BeginExecute<T>(this.RequestUri, callback, state, "POST", false, this.Parameters);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003911 File Offset: 0x00001B11
		public Task<IEnumerable<T>> ExecuteAsync()
		{
			return this.Context.ExecuteAsync<T>(this.RequestUri, "POST", false, this.Parameters);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003930 File Offset: 0x00001B30
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Type is used to infer result")]
		public IEnumerable<T> EndExecute(IAsyncResult asyncResult)
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			return this.Context.EndExecute<T>(asyncResult);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000394A File Offset: 0x00001B4A
		public IEnumerator<T> GetEnumerator()
		{
			return this.Execute().GetEnumerator();
		}

		// Token: 0x04000029 RID: 41
		private readonly DataServiceContext Context;

		// Token: 0x0400002A RID: 42
		private readonly BodyOperationParameter[] Parameters;
	}
}
