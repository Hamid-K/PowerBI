using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Write
{
	// Token: 0x02000780 RID: 1920
	internal class ODataBatchOperationWriter : ODataWriteRequestExecuter
	{
		// Token: 0x06003878 RID: 14456 RVA: 0x000B5BBC File Offset: 0x000B3DBC
		public ODataBatchOperationWriter(ODataEnvironment environment)
			: base(environment)
		{
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x000B5BD0 File Offset: 0x000B3DD0
		public override List<IValueReference> ExecuteODataWriteRequests(List<ODataWriteRequest> crudRequests)
		{
			List<IValueReference> list = new List<IValueReference>(crudRequests.Count);
			for (int i = 0; i < crudRequests.Count; i += this.BatchSize)
			{
				list.AddRange(this.ExecuteChunkedBatchRequest(crudRequests.GetRange(i, Math.Min(crudRequests.Count - i, this.BatchSize))));
			}
			return list;
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000B5C28 File Offset: 0x000B3E28
		public List<IValueReference> ExecuteChunkedBatchRequest(List<ODataWriteRequest> crudRequests)
		{
			MashupHttpWebRequest mashupHttpWebRequest = this.CreateBatchRequest(crudRequests);
			IHostProgress hostProgress = ProgressService.GetHostProgress(base.OdataEnvironment.Host, base.OdataEnvironment.Resource.Kind, mashupHttpWebRequest.RequestUri.AbsoluteUri);
			List<IValueReference> list = new List<IValueReference>(crudRequests.Count);
			List<IValueReference> list3;
			try
			{
				List<HttpResponseData> list2;
				using (new ProgressRequest(hostProgress))
				{
					using (MashupHttpWebResponse mashupHttpWebResponse = (MashupHttpWebResponse)mashupHttpWebRequest.GetResponse())
					{
						using (HttpResponseData httpResponseData = new HttpResponseData(HttpResponseData.Serialize(mashupHttpWebResponse, hostProgress)))
						{
							list2 = this.ReadBatchResponse(httpResponseData, hostProgress);
						}
					}
				}
				for (int i = 0; i < crudRequests.Count; i++)
				{
					IValueReference valueReference = crudRequests[i].ProcessWebResponse(list2[i]);
					list.Add(valueReference);
				}
				list3 = list;
			}
			catch (WebException ex)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(base.OdataEnvironment.Host, "OData/V4/Write/ODataBatchOperationWriter", TraceEventType.Information, base.OdataEnvironment.Resource))
				{
					hostTrace.Add("FailedBatchWriteOperation", ex.Message, true);
					list3 = new ODataOperationWriter(base.OdataEnvironment).ExecuteODataWriteRequests(crudRequests);
				}
			}
			finally
			{
				mashupHttpWebRequest.Abort();
			}
			return list3;
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000B5DB8 File Offset: 0x000B3FB8
		private MashupHttpWebRequest CreateBatchRequest(List<ODataWriteRequest> crudRequests)
		{
			Uri uri = new Uri(base.OdataEnvironment.ServiceUri, "$batch");
			IResource resource = Resource.New(base.OdataEnvironment.Resource.Kind, uri.AbsoluteUri);
			IResource resource2;
			MashupHttpWebRequest mashupHttpWebRequest = base.OdataEnvironment.GetRequest(uri, false).BuildWebRequest(resource, uri, out resource2);
			mashupHttpWebRequest.Method = "POST";
			mashupHttpWebRequest.Accept = "multipart/mixed";
			using (ODataMessageWriter odataMessageWriter = new ODataMessageWriter(new ODataRequestMessage(mashupHttpWebRequest)))
			{
				ODataBatchWriter odataBatchWriter = odataMessageWriter.CreateODataBatchWriter();
				odataBatchWriter.WriteStartBatch();
				for (int i = 0; i < crudRequests.Count; i++)
				{
					ODataWriteRequest odataWriteRequest = crudRequests[i];
					odataBatchWriter.WriteStartChangeset();
					ODataBatchOperationRequestMessage odataBatchOperationRequestMessage = odataBatchWriter.CreateOperationRequestMessage(odataWriteRequest.Method, odataWriteRequest.OdataUri, i.ToString(CultureInfo.InvariantCulture));
					odataWriteRequest.SetODataRequestContents(odataBatchOperationRequestMessage);
					odataBatchWriter.WriteEndChangeset();
				}
				odataBatchWriter.WriteEndBatch();
			}
			return mashupHttpWebRequest;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000B5EC0 File Offset: 0x000B40C0
		private List<HttpResponseData> ReadBatchResponse(HttpResponseData responseData, IHostProgress hostProgress)
		{
			List<HttpResponseData> list = new List<HttpResponseData>();
			using (ODataResponseMessage odataResponseMessage = new ODataResponseMessage(responseData))
			{
				using (ODataMessageReader odataMessageReader = new ODataMessageReader(odataResponseMessage, ODataResponse.DefaultReaderSettings, base.OdataEnvironment.Settings.EdmModel))
				{
					ODataBatchReader odataBatchReader = odataMessageReader.CreateODataBatchReader();
					while (odataBatchReader.Read())
					{
						switch (odataBatchReader.State)
						{
						case ODataBatchReaderState.Operation:
						{
							IODataResponseMessage iodataResponseMessage = odataBatchReader.CreateOperationResponseMessage();
							Stream stream = new MemoryStream();
							using (Stream stream2 = iodataResponseMessage.GetStream())
							{
								stream2.CopyTo(stream);
								stream.Position = 0L;
							}
							HttpResponseData httpResponseData = new HttpResponseData(HttpResponseData.Serialize(iodataResponseMessage.GetHeader("Content-Type"), stream.Length, iodataResponseMessage.StatusCode, iodataResponseMessage.Headers.Count<KeyValuePair<string, string>>(), iodataResponseMessage.Headers, responseData.ResponseUri, stream, hostProgress));
							list.Add(httpResponseData);
							break;
						}
						case ODataBatchReaderState.Exception:
							throw ValueException.NewDataSourceError<Message0>(Strings.ODataBatchError, Value.Null, null);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04001D32 RID: 7474
		private int BatchSize = 100;
	}
}
