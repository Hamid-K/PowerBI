using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4.Write
{
	// Token: 0x020008A2 RID: 2210
	internal class ODataOperationWriter : ODataWriteRequestExecuter
	{
		// Token: 0x06003F43 RID: 16195 RVA: 0x000D03A8 File Offset: 0x000CE5A8
		public ODataOperationWriter(ODataEnvironment environment)
			: base(environment)
		{
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x000D03B4 File Offset: 0x000CE5B4
		public override List<IValueReference> ExecuteODataWriteRequests(List<ODataWriteRequest> crudRequests)
		{
			List<IValueReference> list = new List<IValueReference>(crudRequests.Count);
			new List<HttpResponseData>(crudRequests.Count);
			foreach (ODataWriteRequest odataWriteRequest in crudRequests)
			{
				MashupHttpWebRequest mashupHttpWebRequest = this.CreateWriteRequest(odataWriteRequest);
				IHostProgress hostProgress = ProgressService.GetHostProgress(base.OdataEnvironment.Host, base.OdataEnvironment.Resource.Kind, mashupHttpWebRequest.RequestUri.AbsoluteUri);
				try
				{
					using (new ProgressRequest(hostProgress))
					{
						using (MashupHttpWebResponse mashupHttpWebResponse = (MashupHttpWebResponse)mashupHttpWebRequest.GetResponse())
						{
							using (HttpResponseData httpResponseData = new HttpResponseData(HttpResponseData.Serialize(mashupHttpWebResponse, hostProgress)))
							{
								IValueReference valueReference = odataWriteRequest.ProcessWebResponse(httpResponseData);
								list.Add(valueReference);
							}
						}
					}
				}
				catch (WebException ex)
				{
					throw ODataCommonErrors.RequestFailed(base.OdataEnvironment.Host, ex, mashupHttpWebRequest.RequestUri, base.OdataEnvironment.HttpResource);
				}
				finally
				{
					mashupHttpWebRequest.Abort();
				}
			}
			return list;
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x000D051C File Offset: 0x000CE71C
		public MashupHttpWebRequest CreateWriteRequest(ODataWriteRequest odataWriteRequest)
		{
			MashupHttpWebRequest mashupHttpWebRequest = odataWriteRequest.CreateWebRequest();
			ODataRequestMessage odataRequestMessage = new ODataRequestMessage(mashupHttpWebRequest);
			odataWriteRequest.SetODataRequestContents(odataRequestMessage);
			return mashupHttpWebRequest;
		}
	}
}
