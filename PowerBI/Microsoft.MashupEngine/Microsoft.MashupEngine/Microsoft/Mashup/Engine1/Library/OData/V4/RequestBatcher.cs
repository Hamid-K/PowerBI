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
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000897 RID: 2199
	internal class RequestBatcher
	{
		// Token: 0x06003F20 RID: 16160 RVA: 0x000CF3BC File Offset: 0x000CD5BC
		public RequestBatcher(ODataEnvironment environment)
		{
			this.environment = environment;
			this.persistentCache = environment.Host.GetPersistentCache();
			this.columns = new Dictionary<int, List<RequestBatcher.Request>>();
			this.messageWriterSettings = new ODataMessageWriterSettings();
			this.messageWriterSettings.MessageQuotas.MaxPartsPerBatch = 100;
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x000CF410 File Offset: 0x000CD610
		public Lazy<ODataReaderWrapper> GetResponse(GetReaderArgs args)
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
			return new Lazy<ODataReaderWrapper>(this.GetDelayedReader(args, list, request));
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x000CF4F1 File Offset: 0x000CD6F1
		private Func<ODataReaderWrapper> GetDelayedReader(GetReaderArgs args, List<RequestBatcher.Request> columnRequests, RequestBatcher.Request request)
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

		// Token: 0x06003F23 RID: 16163 RVA: 0x000CF520 File Offset: 0x000CD720
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

		// Token: 0x06003F24 RID: 16164 RVA: 0x000CF638 File Offset: 0x000CD838
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

		// Token: 0x06003F25 RID: 16165 RVA: 0x000CF718 File Offset: 0x000CD918
		private ODataReaderWrapper CreateReader(GetReaderArgs args, Stream stream)
		{
			return ODataReaderWrapper.CreateFromHttpResponseData(this.environment.Host, new HttpResponseData(stream), args.Uri, this.environment.Resource.Kind, args.IsFeed, this.environment.EdmModel);
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x000CF758 File Offset: 0x000CD958
		private MashupHttpWebRequest CreateBatchRequest(Uri uri)
		{
			IResource resource = Resource.New(this.environment.Resource.Kind, uri.AbsoluteUri);
			IResource resource2;
			MashupHttpWebRequest mashupHttpWebRequest = this.environment.GetRequest(uri, false).BuildWebRequest(resource, uri, out resource2);
			mashupHttpWebRequest.Method = "POST";
			return mashupHttpWebRequest;
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x000CF7A4 File Offset: 0x000CD9A4
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

		// Token: 0x06003F28 RID: 16168 RVA: 0x000CF850 File Offset: 0x000CDA50
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

		// Token: 0x04002123 RID: 8483
		public const int BatchSize = 100;

		// Token: 0x04002124 RID: 8484
		private readonly ODataEnvironment environment;

		// Token: 0x04002125 RID: 8485
		private readonly ODataMessageWriterSettings messageWriterSettings;

		// Token: 0x04002126 RID: 8486
		private readonly IPersistentCache persistentCache;

		// Token: 0x04002127 RID: 8487
		private readonly Dictionary<int, List<RequestBatcher.Request>> columns;

		// Token: 0x02000898 RID: 2200
		private class Request
		{
			// Token: 0x04002128 RID: 8488
			public GetReaderArgs args;

			// Token: 0x04002129 RID: 8489
			public Lazy<string> cacheKey;

			// Token: 0x0400212A RID: 8490
			public ODataReaderWrapper reader;
		}
	}
}
