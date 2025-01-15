using System;
using System.Threading.Tasks;

namespace Microsoft.OData.Client
{
	// Token: 0x02000015 RID: 21
	public class DataServiceActionQuery
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004363 File Offset: 0x00002563
		public DataServiceActionQuery(DataServiceContext context, string requestUriString, params BodyOperationParameter[] parameters)
		{
			this.Context = context;
			this.RequestUri = new Uri(requestUriString);
			this.Parameters = parameters;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004385 File Offset: 0x00002585
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000438D File Offset: 0x0000258D
		public Uri RequestUri { get; private set; }

		// Token: 0x06000091 RID: 145 RVA: 0x00004396 File Offset: 0x00002596
		public OperationResponse Execute()
		{
			return this.Context.Execute(this.RequestUri, "POST", this.Parameters);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000043B4 File Offset: 0x000025B4
		public IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			return this.Context.BeginExecute(this.RequestUri, callback, state, "POST", this.Parameters);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000043D4 File Offset: 0x000025D4
		public Task<OperationResponse> ExecuteAsync()
		{
			return this.Context.ExecuteAsync(this.RequestUri, "POST", this.Parameters);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000043F2 File Offset: 0x000025F2
		public OperationResponse EndExecute(IAsyncResult asyncResult)
		{
			return this.Context.EndExecute(asyncResult);
		}

		// Token: 0x04000034 RID: 52
		private readonly DataServiceContext Context;

		// Token: 0x04000035 RID: 53
		private readonly BodyOperationParameter[] Parameters;
	}
}
