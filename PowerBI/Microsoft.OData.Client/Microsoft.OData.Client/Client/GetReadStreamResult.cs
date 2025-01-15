using System;
using System.IO;
using System.Net;

namespace Microsoft.OData.Client
{
	// Token: 0x02000092 RID: 146
	internal class GetReadStreamResult : BaseAsyncResult
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x0000F614 File Offset: 0x0000D814
		internal GetReadStreamResult(DataServiceContext context, string method, ODataRequestMessageWrapper request, AsyncCallback callback, object state, StreamDescriptor streamDescriptor)
			: base(context, method, callback, state)
		{
			this.requestMessage = request;
			base.Abortable = request;
			this.streamDescriptor = streamDescriptor;
			this.requestInfo = new RequestInfo(context);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000F644 File Offset: 0x0000D844
		internal void Begin()
		{
			try
			{
				IAsyncResult asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(this.requestMessage.BeginGetResponse), new AsyncCallback(this.AsyncEndGetResponse), null);
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
			}
			catch (Exception ex)
			{
				base.HandleFailure(ex);
				throw;
			}
			finally
			{
				base.HandleCompleted();
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000F6B4 File Offset: 0x0000D8B4
		internal DataServiceStreamResponse End()
		{
			if (this.responseMessage != null)
			{
				this.streamDescriptor.ETag = this.responseMessage.GetHeader("ETag");
				this.streamDescriptor.ContentType = this.responseMessage.GetHeader("Content-Type");
				return new DataServiceStreamResponse(this.responseMessage);
			}
			return null;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000F710 File Offset: 0x0000D910
		internal DataServiceStreamResponse Execute()
		{
			try
			{
				this.responseMessage = this.requestInfo.GetSyncronousResponse(this.requestMessage, true);
			}
			catch (Exception ex)
			{
				base.HandleFailure(ex);
				throw;
			}
			finally
			{
				base.SetCompleted();
				this.CompletedRequest();
			}
			if (base.Failure != null)
			{
				throw base.Failure;
			}
			return this.End();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000F780 File Offset: 0x0000D980
		protected override void CompletedRequest()
		{
			if (this.responseMessage != null)
			{
				InvalidOperationException ex = null;
				if (!WebUtil.SuccessStatusCode((HttpStatusCode)this.responseMessage.StatusCode))
				{
					ex = BaseSaveResult.GetResponseText(new Func<Stream>(this.responseMessage.GetStream), (HttpStatusCode)this.responseMessage.StatusCode);
				}
				if (ex != null)
				{
					WebUtil.DisposeMessage(this.responseMessage);
					base.HandleFailure(ex);
				}
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000F7E2 File Offset: 0x0000D9E2
		protected override void HandleCompleted(BaseAsyncResult.PerRequest pereq)
		{
			Error.ThrowInternalError(InternalError.InvalidHandleCompleted);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000F7EC File Offset: 0x0000D9EC
		protected override void AsyncEndGetResponse(IAsyncResult asyncResult)
		{
			try
			{
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
				ODataRequestMessageWrapper odataRequestMessageWrapper = Util.NullCheck<ODataRequestMessageWrapper>(this.requestMessage, InternalError.InvalidEndGetResponseRequest);
				this.responseMessage = this.requestInfo.EndGetResponse(odataRequestMessageWrapper, asyncResult);
				base.SetCompleted();
			}
			catch (Exception ex)
			{
				if (base.HandleFailure(ex))
				{
					throw;
				}
			}
			finally
			{
				base.HandleCompleted();
			}
		}

		// Token: 0x0400013F RID: 319
		private readonly ODataRequestMessageWrapper requestMessage;

		// Token: 0x04000140 RID: 320
		private readonly StreamDescriptor streamDescriptor;

		// Token: 0x04000141 RID: 321
		private readonly RequestInfo requestInfo;

		// Token: 0x04000142 RID: 322
		private IODataResponseMessage responseMessage;
	}
}
