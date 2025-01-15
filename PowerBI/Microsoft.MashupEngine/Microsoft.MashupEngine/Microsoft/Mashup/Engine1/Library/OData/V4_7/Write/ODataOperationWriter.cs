using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Write
{
	// Token: 0x02000781 RID: 1921
	internal class ODataOperationWriter : ODataWriteRequestExecuter
	{
		// Token: 0x0600387D RID: 14461 RVA: 0x000B6034 File Offset: 0x000B4234
		public ODataOperationWriter(ODataEnvironment environment)
			: base(environment)
		{
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000B6040 File Offset: 0x000B4240
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

		// Token: 0x0600387F RID: 14463 RVA: 0x000B61A8 File Offset: 0x000B43A8
		public MashupHttpWebRequest CreateWriteRequest(ODataWriteRequest odataWriteRequest)
		{
			MashupHttpWebRequest mashupHttpWebRequest = odataWriteRequest.CreateWebRequest();
			ODataRequestMessage odataRequestMessage = new ODataRequestMessage(mashupHttpWebRequest);
			odataWriteRequest.SetODataRequestContents(odataRequestMessage);
			return mashupHttpWebRequest;
		}
	}
}
