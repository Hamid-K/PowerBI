using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Batch;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001CC RID: 460
	public abstract class ODataBatchHandler : HttpBatchHandler
	{
		// Token: 0x06000F2F RID: 3887 RVA: 0x0003E785 File Offset: 0x0003C985
		protected ODataBatchHandler(HttpServer httpServer)
			: base(httpServer)
		{
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
		public virtual Task<HttpResponseMessage> CreateResponseMessageAsync(IEnumerable<ODataBatchResponseItem> responses, HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.CreateODataBatchResponseAsync(responses, this.MessageQuotas);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003E7C5 File Offset: 0x0003C9C5
		public virtual void ValidateRequest(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.ValidateODataBatchRequest();
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003E7DB File Offset: 0x0003C9DB
		public virtual Uri GetBaseUri(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.GetODataBatchBaseUri(this.ODataRouteName);
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0003E7F7 File Offset: 0x0003C9F7
		public ODataMessageQuotas MessageQuotas
		{
			get
			{
				return this._messageQuotas;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0003E7FF File Offset: 0x0003C9FF
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x0003E807 File Offset: 0x0003CA07
		public string ODataRouteName { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0003E810 File Offset: 0x0003CA10
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x0003E818 File Offset: 0x0003CA18
		internal bool ContinueOnError { get; private set; }

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003E824 File Offset: 0x0003CA24
		internal void SetContinueOnError(IWebApiHeaders header, bool enableContinueOnErrorHeader)
		{
			string requestPreferHeader = RequestPreferenceHelpers.GetRequestPreferHeader(header);
			if ((requestPreferHeader != null && requestPreferHeader.Contains("continue-on-error") && !requestPreferHeader.Contains("continue-on-error=false")) || !enableContinueOnErrorHeader)
			{
				this.ContinueOnError = true;
				return;
			}
			this.ContinueOnError = false;
		}

		// Token: 0x04000437 RID: 1079
		private ODataMessageQuotas _messageQuotas = new ODataMessageQuotas
		{
			MaxReceivedMessageSize = long.MaxValue
		};

		// Token: 0x04000438 RID: 1080
		internal const string PreferenceContinueOnError = "continue-on-error";

		// Token: 0x04000439 RID: 1081
		internal const string PreferenceContinueOnErrorFalse = "continue-on-error=false";
	}
}
