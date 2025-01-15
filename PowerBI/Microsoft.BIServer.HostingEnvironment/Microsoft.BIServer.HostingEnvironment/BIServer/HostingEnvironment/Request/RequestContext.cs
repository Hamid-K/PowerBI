using System;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Microsoft.BIServer.HostingEnvironment.Request
{
	// Token: 0x0200002A RID: 42
	public class RequestContext
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004BD5 File Offset: 0x00002DD5
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00004BDD File Offset: 0x00002DDD
		public string RequestID { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004BE6 File Offset: 0x00002DE6
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00004BEE File Offset: 0x00002DEE
		public string ClientSessionID { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004BF7 File Offset: 0x00002DF7
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00004BFF File Offset: 0x00002DFF
		public bool IsPbiMobileApp { get; private set; }

		// Token: 0x06000129 RID: 297 RVA: 0x00004C08 File Offset: 0x00002E08
		public RequestContext(string requestID, string clientSessionID, bool isPbiMobileApp)
		{
			this.RequestID = requestID;
			this.ClientSessionID = clientSessionID;
			this.IsPbiMobileApp = isPbiMobileApp;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004C25 File Offset: 0x00002E25
		public RequestContext(string requestID, string clientSessionID)
		{
			this.RequestID = requestID;
			this.ClientSessionID = clientSessionID;
			this.IsPbiMobileApp = false;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004C44 File Offset: 0x00002E44
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsPbiMobileApp)
			{
				stringBuilder.Append("PowerBIMobile ");
			}
			if (!string.IsNullOrEmpty(this.RequestID))
			{
				stringBuilder.AppendFormat("RequestID = {0} ", this.RequestID);
			}
			if (!string.IsNullOrEmpty(this.ClientSessionID))
			{
				stringBuilder.AppendFormat("ClientSessionID = {0} ", this.ClientSessionID);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004CAF File Offset: 0x00002EAF
		public void SaveOnCallContext()
		{
			CallContext.LogicalSetData("RequestContext", this);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004CBC File Offset: 0x00002EBC
		public static RequestContext GetFromCallContext()
		{
			return CallContext.LogicalGetData("RequestContext") as RequestContext;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public static void PassOnCurrentRequestContext(HttpClient client)
		{
			RequestContext fromCallContext = RequestContext.GetFromCallContext();
			if (fromCallContext != null)
			{
				client.DefaultRequestHeaders.Add("RequestId", fromCallContext.RequestID);
				client.DefaultRequestHeaders.Add("X-SSRS-ClientSessionId", fromCallContext.ClientSessionID);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004D14 File Offset: 0x00002F14
		public static void PassOnCurrentRequestContext(HttpRequestMessage requestMessage)
		{
			RequestContext fromCallContext = RequestContext.GetFromCallContext();
			if (fromCallContext != null)
			{
				requestMessage.Headers.Add("X-SSRS-ClientSessionId", fromCallContext.ClientSessionID);
				requestMessage.Headers.Add("RequestId", fromCallContext.RequestID);
			}
		}

		// Token: 0x04000089 RID: 137
		public const string ClientSessionIdHeader = "X-SSRS-ClientSessionId";

		// Token: 0x0400008A RID: 138
		public const string RequestIdHeader = "RequestId";

		// Token: 0x0400008B RID: 139
		public const string UserAgent = "User-Agent";
	}
}
