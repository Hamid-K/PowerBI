using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Batch;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001D7 RID: 471
	public class DefaultODataBatchHandler : ODataBatchHandler
	{
		// Token: 0x06000F79 RID: 3961 RVA: 0x0003EC45 File Offset: 0x0003CE45
		public DefaultODataBatchHandler(HttpServer httpServer)
			: base(httpServer)
		{
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003F458 File Offset: 0x0003D658
		public override async Task<HttpResponseMessage> ProcessBatchAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this.ValidateRequest(request);
			IList<ODataBatchRequestItem> list = await this.ParseBatchRequestsAsync(request, cancellationToken);
			IList<ODataBatchRequestItem> subRequests = list;
			HttpConfiguration configuration = HttpRequestMessageExtensions.GetConfiguration(request);
			bool flag = configuration != null && configuration.HasEnabledContinueOnErrorHeader();
			base.SetContinueOnError(new WebApiRequestHeaders(request.Headers), flag);
			HttpResponseMessage httpResponseMessage;
			try
			{
				httpResponseMessage = await this.CreateResponseMessageAsync(await this.ExecuteRequestMessagesAsync(subRequests, cancellationToken), request, cancellationToken);
			}
			finally
			{
				IEnumerator<ODataBatchRequestItem> enumerator = subRequests.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ODataBatchRequestItem odataBatchRequestItem = enumerator.Current;
						HttpRequestMessageExtensions.RegisterForDispose(request, odataBatchRequestItem.GetResourcesForDisposal());
						HttpRequestMessageExtensions.RegisterForDispose(request, odataBatchRequestItem);
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

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003F4B0 File Offset: 0x0003D6B0
		public virtual async Task<IList<ODataBatchResponseItem>> ExecuteRequestMessagesAsync(IEnumerable<ODataBatchRequestItem> requests, CancellationToken cancellationToken)
		{
			if (requests == null)
			{
				throw Error.ArgumentNull("requests");
			}
			IList<ODataBatchResponseItem> responses = new List<ODataBatchResponseItem>();
			try
			{
				foreach (ODataBatchRequestItem odataBatchRequestItem in requests)
				{
					ODataBatchResponseItem odataBatchResponseItem = await odataBatchRequestItem.SendRequestAsync(base.Invoker, cancellationToken);
					responses.Add(odataBatchResponseItem);
					if (odataBatchResponseItem != null && !odataBatchResponseItem.IsResponseSuccessful() && !base.ContinueOnError)
					{
						break;
					}
				}
				IEnumerator<ODataBatchRequestItem> enumerator = null;
			}
			catch
			{
				IEnumerator<ODataBatchResponseItem> enumerator2 = responses.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						ODataBatchResponseItem odataBatchResponseItem2 = enumerator2.Current;
						if (odataBatchResponseItem2 != null)
						{
							odataBatchResponseItem2.Dispose();
						}
					}
				}
				finally
				{
					int num;
					if (num < 0 && enumerator2 != null)
					{
						enumerator2.Dispose();
					}
				}
				throw;
			}
			return responses;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0003F508 File Offset: 0x0003D708
		public virtual async Task<IList<ODataBatchRequestItem>> ParseBatchRequestsAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			IServiceProvider serviceProvider = request.CreateRequestContainer(base.ODataRouteName);
			ServiceProviderServiceExtensions.GetRequiredService<ODataMessageReaderSettings>(serviceProvider).BaseUri = this.GetBaseUri(request);
			ODataMessageReader odataMessageReader = await request.Content.GetODataMessageReaderAsync(serviceProvider, cancellationToken);
			HttpRequestMessageExtensions.RegisterForDispose(request, odataMessageReader);
			List<ODataBatchRequestItem> requests = new List<ODataBatchRequestItem>();
			ODataBatchReader batchReader = odataMessageReader.CreateODataBatchReader();
			Guid batchId = Guid.NewGuid();
			while (batchReader.Read())
			{
				if (batchReader.State == ODataBatchReaderState.ChangesetStart)
				{
					IList<HttpRequestMessage> list = await batchReader.ReadChangeSetRequestAsync(batchId, cancellationToken);
					foreach (HttpRequestMessage httpRequestMessage in list)
					{
						BatchHttpRequestMessageExtensions.CopyBatchRequestProperties(httpRequestMessage, request);
						httpRequestMessage.DeleteRequestContainer(false);
					}
					requests.Add(new ChangeSetRequestItem(list));
				}
				else if (batchReader.State == ODataBatchReaderState.Operation)
				{
					HttpRequestMessage httpRequestMessage2 = await batchReader.ReadOperationRequestAsync(batchId, true, cancellationToken);
					BatchHttpRequestMessageExtensions.CopyBatchRequestProperties(httpRequestMessage2, request);
					httpRequestMessage2.DeleteRequestContainer(false);
					requests.Add(new OperationRequestItem(httpRequestMessage2));
				}
			}
			return requests;
		}
	}
}
