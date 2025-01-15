using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x02000223 RID: 547
	internal class ManagedIdentityRequest
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0004AFE2 File Offset: 0x000491E2
		public HttpMethod Method { get; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0004AFEA File Offset: 0x000491EA
		public IDictionary<string, string> Headers { get; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0004AFF2 File Offset: 0x000491F2
		public IDictionary<string, string> BodyParameters { get; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0004AFFA File Offset: 0x000491FA
		public IDictionary<string, string> QueryParameters { get; }

		// Token: 0x0600168F RID: 5775 RVA: 0x0004B002 File Offset: 0x00049202
		public ManagedIdentityRequest(HttpMethod method, Uri endpoint)
		{
			this.Method = method;
			this._baseEndpoint = endpoint;
			this.Headers = new Dictionary<string, string>();
			this.BodyParameters = new Dictionary<string, string>();
			this.QueryParameters = new Dictionary<string, string>();
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0004B039 File Offset: 0x00049239
		public Uri ComputeUri()
		{
			UriBuilder uriBuilder = new UriBuilder(this._baseEndpoint);
			uriBuilder.AppendQueryParameters(this.QueryParameters);
			return uriBuilder.Uri;
		}

		// Token: 0x04000994 RID: 2452
		private readonly Uri _baseEndpoint;
	}
}
