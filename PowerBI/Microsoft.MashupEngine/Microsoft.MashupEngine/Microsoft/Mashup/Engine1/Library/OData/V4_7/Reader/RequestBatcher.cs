using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x020007BB RID: 1979
	internal class RequestBatcher
	{
		// Token: 0x060039B0 RID: 14768 RVA: 0x000BA19C File Offset: 0x000B839C
		public RequestBatcher(ODataEnvironment environment)
		{
			this.environment = environment;
			this.persistentCache = environment.Host.GetPersistentCache();
			this.columns = new Dictionary<int, List<RequestBatcher.Request>>();
			this.messageWriterSettings = new ODataMessageWriterSettings();
			this.messageWriterSettings.MessageQuotas.MaxPartsPerBatch = 100;
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000BA1F0 File Offset: 0x000B83F0
		public Lazy<IODataPayloadReader> GetResponse(GetReaderArgs args)
		{
			if (args.Column == null)
			{
				return this.environment.GetRequestReader(args);
			}
			List<RequestBatcher.Request> list;
			if (!this.columns.TryGetValue(args.Column.Value, out list) || list.Count == 100)
			{
				list = new List<RequestBatcher.Request>();
				this.columns[args.Column.Value] = list;
			}
			Lazy<string> lazy = new Lazy<string>(() => this.environment.Settings.Cache.GetCacheKey(this.environment.Credentials, this.environment.Headers, args.Uri, null));
			RequestBatcher.Request request = new RequestBatcher.Request
			{
				args = args,
				cacheKey = lazy
			};
			list.Add(request);
			return new Lazy<IODataPayloadReader>(this.GetDelayedReader(args, list, request));
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000BA2D1 File Offset: 0x000B84D1
		private Func<IODataPayloadReader> GetDelayedReader(GetReaderArgs args, List<RequestBatcher.Request> columnRequests, RequestBatcher.Request request)
		{
			return delegate
			{
				this.ExecuteRequests(columnRequests);
				if (request.reader != null)
				{
					return request.reader;
				}
				return this.environment.GetRequestReader(args).Value;
			};
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000BA300 File Offset: 0x000B8500
		private void ExecuteRequests(List<RequestBatcher.Request> pendingRequests)
		{
			try
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.environment.Host, "Library/OData/V4/RequestBatcher/ExecuteRequests", TraceEventType.Information, this.environment.Resource))
				{
					hostTrace.Add("preCacheCount", pendingRequests.Count, false);
					List<RequestBatcher.Request> nonCachedRequests = this.ReadFromCache(pendingRequests);
					hostTrace.Add("postCacheCount", nonCachedRequests.Count, false);
					if (nonCachedRequests.Count > 1)
					{
						Uri batchEndpoint = new Uri(this.environment.ServiceUri, "$batch");
						hostTrace.Add("url", batchEndpoint, true);
						MashupHttpWebRequest batchRequest = this.CreateBatchRequest(batchEndpoint);
						SafeExceptions.IgnoreSafeExceptions(this.environment.Host, hostTrace, delegate
						{
							this.WriteBatchRequest(batchRequest, nonCachedRequests);
							IHostProgress hostProgress = ProgressService.GetHostProgress(this.environment.Host, this.environment.Resource.Kind, batchEndpoint.AbsoluteUri);
							using (new ProgressRequest(hostProgress))
							{
								using (MashupHttpWebResponse mashupHttpWebResponse = (MashupHttpWebResponse)batchRequest.GetResponse())
								{
									using (HttpResponseData httpResponseData = new HttpResponseData(HttpResponseData.Serialize(mashupHttpWebResponse, hostProgress)))
									{
										this.ReadBatchResponse(httpResponseData, hostProgress, nonCachedRequests);
									}
								}
							}
						});
					}
				}
			}
			finally
			{
				pendingRequests.Clear();
			}
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000BA418 File Offset: 0x000B8618
		private List<RequestBatcher.Request> ReadFromCache(List<RequestBatcher.Request> pendingRequests)
		{
			bool flag = false;
			foreach (RequestBatcher.Request request in pendingRequests)
			{
				Stream stream;
				if (this.persistentCache.TryGetValue(request.cacheKey.Value, out stream))
				{
					request.reader = this.CreateReader(request.args, stream);
					flag = true;
				}
				else
				{
					this.environment.Settings.Cache.OnCacheMissed();
					if (flag)
					{
						return pendingRequests.Where((RequestBatcher.Request r) => r.reader != null).ToList<RequestBatcher.Request>();
					}
					return pendingRequests;
				}
			}
			return new List<RequestBatcher.Request>();
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000BA4F8 File Offset: 0x000B86F8
		private IODataPayloadReader CreateReader(GetReaderArgs args, Stream stream)
		{
			return ODataResponse.CreateResponseReader(this.environment.Host, this.environment.HttpResource, args.Uri, new HttpResponseData(stream), this.environment.EdmModel);
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000BA52C File Offset: 0x000B872C
		private MashupHttpWebRequest CreateBatchRequest(Uri uri)
		{
			IResource resource = Resource.New(this.environment.Resource.Kind, uri.AbsoluteUri);
			IResource resource2;
			MashupHttpWebRequest mashupHttpWebRequest = this.environment.GetRequest(uri, false).BuildWebRequest(resource, uri, out resource2);
			mashupHttpWebRequest.Method = "POST";
			return mashupHttpWebRequest;
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000BA578 File Offset: 0x000B8778
		private void WriteBatchRequest(MashupHttpWebRequest batchRequest, List<RequestBatcher.Request> pendingRequests)
		{
			using (ODataMessageWriter odataMessageWriter = new ODataMessageWriter(new ODataRequestMessage(batchRequest), this.messageWriterSettings))
			{
				ODataBatchWriter odataBatchWriter = odataMessageWriter.CreateODataBatchWriter();
				odataBatchWriter.WriteStartBatch();
				foreach (RequestBatcher.Request request in pendingRequests)
				{
					odataBatchWriter.CreateOperationRequestMessage("GET", request.args.Uri, request.args.Uri.AbsoluteUri);
				}
				odataBatchWriter.WriteEndBatch();
			}
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000BA624 File Offset: 0x000B8824
		private void ReadBatchResponse(HttpResponseData responseData, IHostProgress hostProgress, List<RequestBatcher.Request> pendingRequests)
		{
			using (ODataResponseMessage odataResponseMessage = new ODataResponseMessage(responseData))
			{
				using (ODataMessageReader odataMessageReader = new ODataMessageReader(odataResponseMessage, ODataResponse.DefaultReaderSettings, this.environment.Settings.EdmModel))
				{
					ODataBatchReader odataBatchReader = odataMessageReader.CreateODataBatchReader();
					foreach (RequestBatcher.Request request in pendingRequests)
					{
						Uri uri = request.args.Uri;
						if (!odataBatchReader.Read())
						{
							break;
						}
						if (odataBatchReader.State != ODataBatchReaderState.Operation)
						{
							break;
						}
						IODataResponseMessage iodataResponseMessage = odataBatchReader.CreateOperationResponseMessage();
						Stream stream = new MemoryStream();
						using (Stream stream2 = iodataResponseMessage.GetStream())
						{
							stream2.CopyTo(stream);
							stream.Position = 0L;
						}
						Stream stream3 = HttpResponseData.Serialize(iodataResponseMessage.GetHeader("Content-Type"), stream.Length, iodataResponseMessage.StatusCode, iodataResponseMessage.Headers.Count<KeyValuePair<string, string>>(), iodataResponseMessage.Headers, uri, stream, hostProgress);
						stream3 = this.persistentCache.Add(request.cacheKey.Value, stream3);
						request.reader = this.CreateReader(request.args, stream3);
					}
				}
			}
		}

		// Token: 0x04001DDB RID: 7643
		public const int BatchSize = 100;

		// Token: 0x04001DDC RID: 7644
		private readonly ODataEnvironment environment;

		// Token: 0x04001DDD RID: 7645
		private readonly ODataMessageWriterSettings messageWriterSettings;

		// Token: 0x04001DDE RID: 7646
		private readonly IPersistentCache persistentCache;

		// Token: 0x04001DDF RID: 7647
		private readonly Dictionary<int, List<RequestBatcher.Request>> columns;

		// Token: 0x020007BC RID: 1980
		private class Request
		{
			// Token: 0x04001DE0 RID: 7648
			public GetReaderArgs args;

			// Token: 0x04001DE1 RID: 7649
			public Lazy<string> cacheKey;

			// Token: 0x04001DE2 RID: 7650
			public IODataPayloadReader reader;
		}
	}
}
