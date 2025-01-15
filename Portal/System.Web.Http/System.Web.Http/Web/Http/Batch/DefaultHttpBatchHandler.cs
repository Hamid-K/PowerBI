using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Batch
{
	// Token: 0x02000112 RID: 274
	public class DefaultHttpBatchHandler : HttpBatchHandler
	{
		// Token: 0x06000750 RID: 1872 RVA: 0x00011FFD File Offset: 0x000101FD
		public DefaultHttpBatchHandler(HttpServer httpServer)
			: base(httpServer)
		{
			this.ExecutionOrder = BatchExecutionOrder.Sequential;
			this.SupportedContentTypes = new List<string> { "multipart/mixed" };
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00012023 File Offset: 0x00010223
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x0001202B File Offset: 0x0001022B
		public BatchExecutionOrder ExecutionOrder
		{
			get
			{
				return this._executionOrder;
			}
			set
			{
				if (!Enum.IsDefined(typeof(BatchExecutionOrder), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BatchExecutionOrder));
				}
				this._executionOrder = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00012061 File Offset: 0x00010261
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x00012069 File Offset: 0x00010269
		public IList<string> SupportedContentTypes { get; private set; }

		// Token: 0x06000755 RID: 1877 RVA: 0x00012074 File Offset: 0x00010274
		public virtual Task<HttpResponseMessage> CreateResponseMessageAsync(IList<HttpResponseMessage> responses, HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (responses == null)
			{
				throw Error.ArgumentNull("responses");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			MultipartContent multipartContent = new MultipartContent("mixed");
			foreach (HttpResponseMessage httpResponseMessage in responses)
			{
				multipartContent.Add(new HttpMessageContent(httpResponseMessage));
			}
			HttpResponseMessage httpResponseMessage2 = request.CreateResponse();
			httpResponseMessage2.Content = multipartContent;
			return Task.FromResult<HttpResponseMessage>(httpResponseMessage2);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000120FC File Offset: 0x000102FC
		public override async Task<HttpResponseMessage> ProcessBatchAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this.ValidateRequest(request);
			IList<HttpRequestMessage> list = await this.ParseBatchRequestsAsync(request, cancellationToken);
			IList<HttpRequestMessage> subRequests = list;
			HttpResponseMessage httpResponseMessage;
			try
			{
				httpResponseMessage = await this.CreateResponseMessageAsync(await this.ExecuteRequestMessagesAsync(subRequests, cancellationToken), request, cancellationToken);
			}
			finally
			{
				IEnumerator<HttpRequestMessage> enumerator = subRequests.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						HttpRequestMessage httpRequestMessage = enumerator.Current;
						request.RegisterForDispose(httpRequestMessage.GetResourcesForDisposal());
						request.RegisterForDispose(httpRequestMessage);
					}
				}
				finally
				{
					int num;
					if (num < 0 && enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}
			return httpResponseMessage;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00012154 File Offset: 0x00010354
		public virtual async Task<IList<HttpResponseMessage>> ExecuteRequestMessagesAsync(IEnumerable<HttpRequestMessage> requests, CancellationToken cancellationToken)
		{
			if (requests == null)
			{
				throw Error.ArgumentNull("requests");
			}
			List<HttpResponseMessage> responses = new List<HttpResponseMessage>();
			try
			{
				BatchExecutionOrder executionOrder = this.ExecutionOrder;
				if (executionOrder != BatchExecutionOrder.Sequential)
				{
					if (executionOrder == BatchExecutionOrder.NonSequential)
					{
						List<HttpResponseMessage> list = responses;
						list.AddRange(await Task.WhenAll<HttpResponseMessage>(requests.Select((HttpRequestMessage request) => this.Invoker.SendAsync(request, cancellationToken))));
						list = null;
					}
				}
				else
				{
					foreach (HttpRequestMessage httpRequestMessage in requests)
					{
						List<HttpResponseMessage> list = responses;
						HttpResponseMessage httpResponseMessage = await base.Invoker.SendAsync(httpRequestMessage, cancellationToken);
						list.Add(httpResponseMessage);
						list = null;
					}
					IEnumerator<HttpRequestMessage> enumerator = null;
				}
			}
			catch
			{
				List<HttpResponseMessage>.Enumerator enumerator2 = responses.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						HttpResponseMessage httpResponseMessage2 = enumerator2.Current;
						if (httpResponseMessage2 != null)
						{
							httpResponseMessage2.Dispose();
						}
					}
				}
				finally
				{
					int num;
					if (num < 0)
					{
						((IDisposable)enumerator2).Dispose();
					}
				}
				throw;
			}
			return responses;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000121AC File Offset: 0x000103AC
		public virtual async Task<IList<HttpRequestMessage>> ParseBatchRequestsAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			List<HttpRequestMessage> requests = new List<HttpRequestMessage>();
			cancellationToken.ThrowIfCancellationRequested();
			MultipartStreamProvider multipartStreamProvider = await request.Content.ReadAsMultipartAsync();
			foreach (HttpContent httpContent in multipartStreamProvider.Contents)
			{
				cancellationToken.ThrowIfCancellationRequested();
				HttpRequestMessage httpRequestMessage = ((!(request.RequestUri == null)) ? (await httpContent.ReadAsHttpRequestMessageAsync(request.RequestUri.Scheme)) : (await httpContent.ReadAsHttpRequestMessageAsync()));
				httpRequestMessage.CopyBatchRequestProperties(request);
				requests.Add(httpRequestMessage);
			}
			IEnumerator<HttpContent> enumerator = null;
			return requests;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000121FC File Offset: 0x000103FC
		public virtual void ValidateRequest(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (request.Content == null)
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BatchRequestMissingContent));
			}
			MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
			if (contentType == null)
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, SRResources.BatchContentTypeMissing));
			}
			if (!this.SupportedContentTypes.Contains(contentType.MediaType, StringComparer.OrdinalIgnoreCase))
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, Error.Format(SRResources.BatchMediaTypeNotSupported, new object[] { contentType.MediaType })));
			}
		}

		// Token: 0x040001D9 RID: 473
		private const string MultiPartContentSubtype = "mixed";

		// Token: 0x040001DA RID: 474
		private const string MultiPartMixed = "multipart/mixed";

		// Token: 0x040001DB RID: 475
		private BatchExecutionOrder _executionOrder;
	}
}
