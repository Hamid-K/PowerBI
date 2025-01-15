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
	// Token: 0x020001CE RID: 462
	public class UnbufferedODataBatchHandler : ODataBatchHandler
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x0003EC45 File Offset: 0x0003CE45
		public UnbufferedODataBatchHandler(HttpServer httpServer)
			: base(httpServer)
		{
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003EC50 File Offset: 0x0003CE50
		public override async Task<HttpResponseMessage> ProcessBatchAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this.ValidateRequest(request);
			IServiceProvider serviceProvider = request.CreateRequestContainer(base.ODataRouteName);
			ServiceProviderServiceExtensions.GetRequiredService<ODataMessageReaderSettings>(serviceProvider).BaseUri = this.GetBaseUri(request);
			ODataMessageReader odataMessageReader = await request.Content.GetODataMessageReaderAsync(serviceProvider, cancellationToken);
			HttpRequestMessageExtensions.RegisterForDispose(request, odataMessageReader);
			ODataBatchReader batchReader = odataMessageReader.CreateODataBatchReader();
			List<ODataBatchResponseItem> responses = new List<ODataBatchResponseItem>();
			Guid batchId = Guid.NewGuid();
			HttpConfiguration configuration = HttpRequestMessageExtensions.GetConfiguration(request);
			bool flag = configuration != null && configuration.HasEnabledContinueOnErrorHeader();
			base.SetContinueOnError(new WebApiRequestHeaders(request.Headers), flag);
			try
			{
				while (batchReader.Read())
				{
					ODataBatchResponseItem odataBatchResponseItem = null;
					if (batchReader.State == ODataBatchReaderState.ChangesetStart)
					{
						odataBatchResponseItem = await this.ExecuteChangeSetAsync(batchReader, batchId, request, cancellationToken);
					}
					else if (batchReader.State == ODataBatchReaderState.Operation)
					{
						odataBatchResponseItem = await this.ExecuteOperationAsync(batchReader, batchId, request, cancellationToken);
					}
					if (odataBatchResponseItem != null)
					{
						responses.Add(odataBatchResponseItem);
						if (!odataBatchResponseItem.IsResponseSuccessful() && !base.ContinueOnError)
						{
							break;
						}
					}
				}
			}
			catch
			{
				List<ODataBatchResponseItem>.Enumerator enumerator = responses.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ODataBatchResponseItem odataBatchResponseItem2 = enumerator.Current;
						if (odataBatchResponseItem2 != null)
						{
							odataBatchResponseItem2.Dispose();
						}
					}
				}
				finally
				{
					int num;
					if (num < 0)
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				throw;
			}
			return await this.CreateResponseMessageAsync(responses, request, cancellationToken);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0003ECA8 File Offset: 0x0003CEA8
		public virtual async Task<ODataBatchResponseItem> ExecuteOperationAsync(ODataBatchReader batchReader, Guid batchId, HttpRequestMessage originalRequest, CancellationToken cancellationToken)
		{
			if (batchReader == null)
			{
				throw Error.ArgumentNull("batchReader");
			}
			if (originalRequest == null)
			{
				throw Error.ArgumentNull("originalRequest");
			}
			cancellationToken.ThrowIfCancellationRequested();
			HttpRequestMessage httpRequestMessage = await batchReader.ReadOperationRequestAsync(batchId, false);
			BatchHttpRequestMessageExtensions.CopyBatchRequestProperties(httpRequestMessage, originalRequest);
			httpRequestMessage.DeleteRequestContainer(false);
			OperationRequestItem operation = new OperationRequestItem(httpRequestMessage);
			ODataBatchResponseItem odataBatchResponseItem;
			try
			{
				odataBatchResponseItem = await operation.SendRequestAsync(base.Invoker, cancellationToken);
			}
			finally
			{
				HttpRequestMessageExtensions.RegisterForDispose(originalRequest, operation.GetResourcesForDisposal());
				HttpRequestMessageExtensions.RegisterForDispose(originalRequest, operation);
			}
			return odataBatchResponseItem;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003ED10 File Offset: 0x0003CF10
		public virtual async Task<ODataBatchResponseItem> ExecuteChangeSetAsync(ODataBatchReader batchReader, Guid batchId, HttpRequestMessage originalRequest, CancellationToken cancellationToken)
		{
			if (batchReader == null)
			{
				throw Error.ArgumentNull("batchReader");
			}
			if (originalRequest == null)
			{
				throw Error.ArgumentNull("originalRequest");
			}
			Guid changeSetId = Guid.NewGuid();
			List<HttpResponseMessage> changeSetResponse = new List<HttpResponseMessage>();
			Dictionary<string, string> contentIdToLocationMapping = new Dictionary<string, string>();
			try
			{
				while (batchReader.Read() && batchReader.State != ODataBatchReaderState.ChangesetEnd)
				{
					if (batchReader.State == ODataBatchReaderState.Operation)
					{
						HttpRequestMessage httpRequestMessage = await batchReader.ReadChangeSetOperationRequestAsync(batchId, changeSetId, false);
						HttpRequestMessage changeSetOperationRequest = httpRequestMessage;
						BatchHttpRequestMessageExtensions.CopyBatchRequestProperties(changeSetOperationRequest, originalRequest);
						changeSetOperationRequest.DeleteRequestContainer(false);
						try
						{
							HttpResponseMessage httpResponseMessage = await ODataBatchRequestItem.SendMessageAsync(base.Invoker, changeSetOperationRequest, cancellationToken, contentIdToLocationMapping);
							if (!httpResponseMessage.IsSuccessStatusCode)
							{
								ChangeSetRequestItem.DisposeResponses(changeSetResponse);
								changeSetResponse.Clear();
								changeSetResponse.Add(httpResponseMessage);
								return new ChangeSetResponseItem(changeSetResponse);
							}
							changeSetResponse.Add(httpResponseMessage);
						}
						finally
						{
							HttpRequestMessageExtensions.RegisterForDispose(originalRequest, HttpRequestMessageExtensions.GetResourcesForDisposal(changeSetOperationRequest));
							HttpRequestMessageExtensions.RegisterForDispose(originalRequest, changeSetOperationRequest);
						}
						changeSetOperationRequest = null;
					}
				}
			}
			catch
			{
				ChangeSetRequestItem.DisposeResponses(changeSetResponse);
				throw;
			}
			return new ChangeSetResponseItem(changeSetResponse);
		}
	}
}
