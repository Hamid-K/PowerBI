using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x0200000F RID: 15
	internal class HttpPipelineMessageHandler : HttpMessageHandler
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002243 File Offset: 0x00000443
		public HttpPipelineMessageHandler(HttpPipeline pipeline)
		{
			this._pipeline = pipeline;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002254 File Offset: 0x00000454
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			Request request2 = await this.ToPipelineRequestAsync(request).ConfigureAwait(false);
			return HttpPipelineMessageHandler.ToHttpResponseMessage(await this._pipeline.SendRequestAsync(request2, cancellationToken).ConfigureAwait(false));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000022A8 File Offset: 0x000004A8
		private async Task<Request> ToPipelineRequestAsync(HttpRequestMessage request)
		{
			Request pipelineRequest = this._pipeline.CreateRequest();
			pipelineRequest.Method = RequestMethod.Parse(request.Method.Method);
			pipelineRequest.Uri.Reset(request.RequestUri);
			Request request2 = pipelineRequest;
			RequestContent requestContent = await HttpPipelineMessageHandler.ToPipelineRequestContentAsync(request.Content).ConfigureAwait(false);
			request2.Content = requestContent;
			request2 = null;
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in request.Headers)
			{
				foreach (string text in keyValuePair.Value)
				{
					pipelineRequest.Headers.Add(keyValuePair.Key, text);
				}
			}
			if (request.Content != null)
			{
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair2 in request.Content.Headers)
				{
					foreach (string text2 in keyValuePair2.Value)
					{
						pipelineRequest.Headers.Add(keyValuePair2.Key, text2);
					}
				}
			}
			return pipelineRequest;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000022F4 File Offset: 0x000004F4
		private static HttpResponseMessage ToHttpResponseMessage(Response response)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = (HttpStatusCode)response.Status
			};
			if (response.ContentStream != null)
			{
				httpResponseMessage.Content = new StreamContent(response.ContentStream);
			}
			foreach (HttpHeader httpHeader in response.Headers)
			{
				IEnumerable<string> enumerable;
				if (response.Headers.TryGetValues(httpHeader.Name, out enumerable) && !httpResponseMessage.Headers.TryAddWithoutValidation(httpHeader.Name, enumerable) && (httpResponseMessage.Content == null || !httpResponseMessage.Content.Headers.TryAddWithoutValidation(httpHeader.Name, enumerable)))
				{
					throw new InvalidOperationException("Unable to add header to response or content");
				}
			}
			return httpResponseMessage;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000023C8 File Offset: 0x000005C8
		private static async Task<RequestContent> ToPipelineRequestContentAsync(HttpContent content)
		{
			RequestContent requestContent;
			if (content != null)
			{
				requestContent = RequestContent.Create(await content.ReadAsStreamAsync().ConfigureAwait(false));
			}
			else
			{
				requestContent = null;
			}
			return requestContent;
		}

		// Token: 0x04000027 RID: 39
		private readonly HttpPipeline _pipeline;
	}
}
