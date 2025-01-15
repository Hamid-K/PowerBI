using System;
using System.IO;
using System.Net;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000E4 RID: 228
	internal abstract class SimpleServiceRequestBase : ServiceRequestBase
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002796C File Offset: 0x0002696C
		internal SimpleServiceRequestBase(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00027978 File Offset: 0x00026978
		internal object InternalExecute()
		{
			IEwsHttpWebRequest ewsHttpWebRequest;
			IEwsHttpWebResponse ewsHttpWebResponse = base.ValidateAndEmitRequest(out ewsHttpWebRequest);
			return this.ReadResponse(ewsHttpWebResponse);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00027998 File Offset: 0x00026998
		internal object EndInternalExecute(IAsyncResult asyncResult)
		{
			AsyncRequestResult asyncRequestResult = (AsyncRequestResult)asyncResult;
			IEwsHttpWebResponse ewsHttpWebResponse = base.EndGetEwsHttpWebResponse(asyncRequestResult.WebRequest, asyncRequestResult.WebAsyncResult);
			return this.ReadResponse(ewsHttpWebResponse);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000279C8 File Offset: 0x000269C8
		internal IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			this.Validate();
			IEwsHttpWebRequest ewsHttpWebRequest = base.BuildEwsHttpWebRequest();
			WebAsyncCallStateAnchor webAsyncCallStateAnchor = new WebAsyncCallStateAnchor(this, ewsHttpWebRequest, callback, state);
			IAsyncResult asyncResult = ewsHttpWebRequest.BeginGetResponse(new AsyncCallback(SimpleServiceRequestBase.WebRequestAsyncCallback), webAsyncCallStateAnchor);
			return new AsyncRequestResult(this, ewsHttpWebRequest, asyncResult, state);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00027A0C File Offset: 0x00026A0C
		private static void WebRequestAsyncCallback(IAsyncResult webAsyncResult)
		{
			WebAsyncCallStateAnchor webAsyncCallStateAnchor = webAsyncResult.AsyncState as WebAsyncCallStateAnchor;
			if (webAsyncCallStateAnchor != null && webAsyncCallStateAnchor.AsyncCallback != null)
			{
				AsyncRequestResult asyncRequestResult = new AsyncRequestResult(webAsyncCallStateAnchor.ServiceRequest, webAsyncCallStateAnchor.WebRequest, webAsyncResult, webAsyncCallStateAnchor.AsyncState);
				webAsyncCallStateAnchor.AsyncCallback.Invoke(asyncRequestResult);
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00027A58 File Offset: 0x00026A58
		private object ReadResponse(IEwsHttpWebResponse response)
		{
			object obj;
			try
			{
				base.Service.ProcessHttpResponseHeaders(TraceFlags.EwsResponseHttpHeaders, response);
				if (base.Service.IsTraceEnabledFor(TraceFlags.EwsResponse))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (Stream responseStream = ServiceRequestBase.GetResponseStream(response))
						{
							EwsUtilities.CopyStream(responseStream, memoryStream);
							memoryStream.Position = 0L;
						}
						if (base.Service.RenderingMethod == ExchangeService.RenderingMode.Xml)
						{
							base.TraceResponseXml(response, memoryStream);
							obj = this.ReadResponseXml(memoryStream);
						}
						else
						{
							if (base.Service.RenderingMethod != ExchangeService.RenderingMode.JSON)
							{
								throw new InvalidOperationException("Unknown RenderingMethod.");
							}
							base.TraceResponseJson(response, memoryStream);
							obj = this.ReadResponseJson(memoryStream);
						}
						goto IL_00E8;
					}
				}
				using (Stream responseStream2 = ServiceRequestBase.GetResponseStream(response))
				{
					if (base.Service.RenderingMethod == ExchangeService.RenderingMode.Xml)
					{
						obj = this.ReadResponseXml(responseStream2);
					}
					else
					{
						if (base.Service.RenderingMethod != ExchangeService.RenderingMode.JSON)
						{
							throw new InvalidOperationException("Unknown RenderingMethod.");
						}
						obj = this.ReadResponseJson(responseStream2);
					}
				}
				IL_00E8:;
			}
			catch (WebException ex)
			{
				if (ex.Response != null)
				{
					IEwsHttpWebResponse ewsHttpWebResponse = base.Service.HttpWebRequestFactory.CreateExceptionResponse(ex);
					base.Service.ProcessHttpResponseHeaders(TraceFlags.EwsResponseHttpHeaders, ewsHttpWebResponse);
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex.Message), ex);
			}
			catch (IOException ex2)
			{
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex2.Message), ex2);
			}
			finally
			{
				if (response != null)
				{
					response.Close();
				}
			}
			return obj;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00027C5C File Offset: 0x00026C5C
		private object ReadResponseJson(Stream responseStream)
		{
			JsonObject jsonObject = new JsonParser(responseStream).Parse();
			return base.BuildResponseObjectFromJson(jsonObject);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00027C7C File Offset: 0x00026C7C
		private object ReadResponseXml(Stream responseStream)
		{
			EwsServiceXmlReader ewsServiceXmlReader = new EwsServiceXmlReader(responseStream, base.Service);
			return base.ReadResponse(ewsServiceXmlReader);
		}
	}
}
