using System;
using System.IO;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001D4 RID: 468
	internal sealed class WindowsLiveCredentials : WSSecurityBasedCredentials
	{
		// Token: 0x0600154B RID: 5451 RVA: 0x0003BC30 File Offset: 0x0003AC30
		public WindowsLiveCredentials(string windowsLiveId, string password)
		{
			if (windowsLiveId == null)
			{
				throw new ArgumentNullException("windowsLiveId");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this.windowsLiveId = windowsLiveId;
			this.password = password;
			this.windowsLiveUrl = WindowsLiveCredentials.DefaultWindowsLiveUrl;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0003BC83 File Offset: 0x0003AC83
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x0003BC8B File Offset: 0x0003AC8B
		public bool TraceEnabled
		{
			get
			{
				return this.traceEnabled;
			}
			set
			{
				this.traceEnabled = value;
				if (this.traceEnabled && this.traceListener == null)
				{
					this.traceListener = new EwsTraceListener();
				}
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0003BCAF File Offset: 0x0003ACAF
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x0003BCB7 File Offset: 0x0003ACB7
		public ITraceListener TraceListener
		{
			get
			{
				return this.traceListener;
			}
			set
			{
				this.traceListener = value;
				this.traceEnabled = value != null;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0003BCCD File Offset: 0x0003ACCD
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x0003BCD5 File Offset: 0x0003ACD5
		public Uri WindowsLiveUrl
		{
			get
			{
				return this.windowsLiveUrl;
			}
			set
			{
				base.EwsUrl = null;
				this.IsAuthenticated = false;
				this.windowsLiveUrl = value;
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0003BCEC File Offset: 0x0003ACEC
		internal override void PrepareWebRequest(IEwsHttpWebRequest request)
		{
			if (base.EwsUrl == null || base.EwsUrl != request.RequestUri)
			{
				this.IsAuthenticated = false;
				this.MakeTokenRequestToWindowsLive(request.RequestUri);
				this.IsAuthenticated = true;
				base.EwsUrl = request.RequestUri;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0003BD40 File Offset: 0x0003AD40
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x0003BD48 File Offset: 0x0003AD48
		public bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
			internal set
			{
				this.isAuthenticated = value;
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0003BD54 File Offset: 0x0003AD54
		private HttpWebResponse EmitTokenRequest(Uri uriForTokenEndpointReference)
		{
			DateTime utcNow = DateTime.UtcNow;
			SecurityTimestamp securityTimestamp = new SecurityTimestamp(utcNow, utcNow.AddMinutes(5.0), "Timestamp");
			string text = string.Format("<?xml version='1.0' encoding='UTF-8'?><s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope'             xmlns:wsse='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd'             xmlns:saml='urn:oasis:names:tc:SAML:1.0:assertion'             xmlns:wsp='http://schemas.xmlsoap.org/ws/2004/09/policy'             xmlns:wsu='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd'             xmlns:wsa='http://www.w3.org/2005/08/addressing'             xmlns:wssc='http://schemas.xmlsoap.org/ws/2005/02/sc'             xmlns:wst='http://schemas.xmlsoap.org/ws/2005/02/trust'             xmlns:ps='http://schemas.microsoft.com/Passport/SoapServices/PPCRL'>  <s:Header>    <wsa:Action s:mustUnderstand='1'>http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue</wsa:Action>    <wsa:To s:mustUnderstand='1'>{0}</wsa:To>    <ps:AuthInfo Id='PPAuthInfo'>      <ps:HostingApp>{{63f179af-8bcd-49a0-a3e5-1154c02df090}}</ps:HostingApp>      <ps:BinaryVersion>5</ps:BinaryVersion>      <ps:UIVersion>1</ps:UIVersion>      <ps:Cookies></ps:Cookies>      <ps:RequestParams>AQAAAAIAAABsYwQAAAAxMDMz</ps:RequestParams>    </ps:AuthInfo>    <wsse:Security>      <wsse:UsernameToken wsu:Id='user'>        <wsse:Username>{1}</wsse:Username>        <wsse:Password>{2}</wsse:Password>      </wsse:UsernameToken>      <wsu:Timestamp Id='Timestamp'>        <wsu:Created>{3}</wsu:Created>        <wsu:Expires>{4}</wsu:Expires>      </wsu:Timestamp>    </wsse:Security>  </s:Header>  <s:Body>    <ps:RequestMultipleSecurityTokens Id='RSTS'>      <wst:RequestSecurityToken Id='RST0'>        <wst:RequestType>http://schemas.xmlsoap.org/ws/2005/02/trust/Issue</wst:RequestType>        <wsp:AppliesTo>          <wsa:EndpointReference>            <wsa:Address>http://Passport.NET/tb</wsa:Address>          </wsa:EndpointReference>        </wsp:AppliesTo>      </wst:RequestSecurityToken>      <wst:RequestSecurityToken Id='RST1'>        <wst:RequestType>http://schemas.xmlsoap.org/ws/2005/02/trust/Issue</wst:RequestType>        <wsp:AppliesTo>          <wsa:EndpointReference>            <wsa:Address>{5}</wsa:Address>          </wsa:EndpointReference>        </wsp:AppliesTo>        <wsp:PolicyReference URI='LBI_FED_SSL'></wsp:PolicyReference>      </wst:RequestSecurityToken>    </ps:RequestMultipleSecurityTokens>  </s:Body></s:Envelope>", new object[]
			{
				this.windowsLiveUrl,
				this.windowsLiveId,
				this.password,
				securityTimestamp.GetCreationTimeChars(),
				securityTimestamp.GetExpiryTimeChars(),
				uriForTokenEndpointReference.ToString()
			});
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.windowsLiveUrl);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "text/xml; charset=utf-8";
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			httpWebRequest.ContentLength = (long)bytes.Length;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
			}
			return (HttpWebResponse)httpWebRequest.GetResponse();
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0003BE54 File Offset: 0x0003AE54
		private void TraceResponse(HttpWebResponse response, MemoryStream memoryStream)
		{
			EwsUtilities.Assert(memoryStream != null, "WindowsLiveCredentials.TraceResponse", "memoryStream cannot be null");
			if (!this.TraceEnabled)
			{
				return;
			}
			if (!string.IsNullOrEmpty(response.ContentType) && (response.ContentType.StartsWith("text/", 5) || response.ContentType.StartsWith("application/soap", 5)))
			{
				this.traceListener.Trace("WindowsLiveResponse", EwsUtilities.FormatLogMessageWithXmlContent("WindowsLiveResponse", memoryStream));
				return;
			}
			this.traceListener.Trace("WindowsLiveResponse", "Non-textual response");
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0003BEE4 File Offset: 0x0003AEE4
		private void TraceWebException(WebException e)
		{
			if (e.Response == null)
			{
				if (this.TraceEnabled)
				{
					string text = string.Format("Exception Received when sending Windows Live token request: {0}", e);
					this.traceListener.Trace("WindowsLiveResponse", text);
				}
				return;
			}
			if (this.TraceEnabled)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (Stream responseStream = e.Response.GetResponseStream())
					{
						EwsUtilities.CopyStream(responseStream, memoryStream);
						memoryStream.Position = 0L;
					}
					this.TraceResponse((HttpWebResponse)e.Response, memoryStream);
				}
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0003BF90 File Offset: 0x0003AF90
		private void MakeTokenRequestToWindowsLive(Uri uriForTokenEndpointReference)
		{
			HttpWebResponse httpWebResponse;
			try
			{
				httpWebResponse = this.EmitTokenRequest(uriForTokenEndpointReference);
			}
			catch (WebException ex)
			{
				if (ex.Status == 7 && ex.Response != null)
				{
					this.TraceWebException(ex);
				}
				else if (this.TraceEnabled)
				{
					string text = string.Format("Error occurred sending request - status was {0}, exception {1}", ex.Status, ex);
					this.traceListener.Trace("WindowsLiveCredentials", text);
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex.Message), ex);
			}
			try
			{
				this.ProcessTokenResponse(httpWebResponse);
			}
			catch (WebException ex2)
			{
				if (this.TraceEnabled)
				{
					string text2 = string.Format("Error occurred sending request - status was {0}, exception {1}", ex2.Status, ex2);
					this.traceListener.Trace("WindowsLiveCredentials", text2);
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex2.Message), ex2);
			}
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0003C084 File Offset: 0x0003B084
		private void ReadWindowsLiveRSTResponseHeaders(EwsXmlReader rstResponse)
		{
			rstResponse.ReadStartElement("S", "Header");
			rstResponse.ReadToDescendant(XmlNamespace.PassportSoapFault, "pp");
			if (rstResponse.IsEndElement("S", "Header"))
			{
				if (this.TraceEnabled)
				{
					this.traceListener.Trace("WindowsLiveResponse", "Could not find Passport SOAP fault information in Windows Live response");
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, "pp"));
			}
			rstResponse.ReadToDescendant(XmlNamespace.PassportSoapFault, "reqstatus");
			if (rstResponse.IsEndElement(XmlNamespace.PassportSoapFault, "pp"))
			{
				if (this.TraceEnabled)
				{
					this.traceListener.Trace("WindowsLiveResponse", "Could not find reqstatus element in Passport SOAP fault information in Windows Live response");
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, "reqstatus"));
			}
			string text = rstResponse.ReadElementValue();
			while (!rstResponse.IsEndElement("S", "Header"))
			{
				rstResponse.Read();
			}
			if (!string.Equals(text, "0x0"))
			{
				if (this.TraceEnabled)
				{
					string text2 = string.Format("Received status {0} from Windows Live instead of {1}.", text, "0x0");
					this.traceListener.Trace("WindowsLiveResponse", text2);
					rstResponse.ReadStartElement("S", "Body");
					this.traceListener.Trace("WindowsLiveResponse", string.Format("Windows Live reported Fault : {0}", rstResponse.ReadInnerXml()));
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, "reqstatus: " + text));
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0003C1F0 File Offset: 0x0003B1F0
		private void ParseWindowsLiveRSTResponseBody(EwsXmlReader rstResponse)
		{
			rstResponse.ReadStartElement(XmlNamespace.WSTrustFebruary2005, "RequestSecurityTokenResponseCollection");
			rstResponse.SkipElement(XmlNamespace.WSTrustFebruary2005, "RequestSecurityTokenResponse");
			rstResponse.ReadStartElement(XmlNamespace.WSTrustFebruary2005, "RequestSecurityTokenResponse");
			while (!rstResponse.IsEndElement(XmlNamespace.WSTrustFebruary2005, "RequestSecurityTokenResponse"))
			{
				if (rstResponse.IsStartElement() && rstResponse.LocalName == "EncryptedData" && rstResponse.NamespaceUri == "http://www.w3.org/2001/04/xmlenc#")
				{
					base.SecurityToken = rstResponse.ReadOuterXml();
				}
				else if (rstResponse.IsStartElement(XmlNamespace.PassportSoapFault, "pp"))
				{
					if (this.TraceEnabled)
					{
						string text = string.Format("Windows Live reported an error retrieving the token - {0}", rstResponse.ReadOuterXml());
						this.traceListener.Trace("WindowsLiveResponse", text);
					}
					throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, "EncryptedData"));
				}
				rstResponse.Read();
			}
			if (base.SecurityToken == null)
			{
				if (this.TraceEnabled)
				{
					string text2 = string.Format("Did not find all required parts of the Windows Live response - Security Token - {0}", (base.SecurityToken == null) ? "NOT FOUND" : "found");
					this.traceListener.Trace("WindowsLiveResponse", text2);
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, "No security token found."));
			}
			rstResponse.Read();
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0003C32C File Offset: 0x0003B32C
		private void ProcessTokenResponse(HttpWebResponse response)
		{
			using (Stream responseStream = response.GetResponseStream())
			{
				base.SecurityToken = null;
				EwsXmlReader ewsXmlReader = new EwsXmlReader(responseStream);
				ewsXmlReader.Read(17);
				ewsXmlReader.ReadStartElement("S", "Envelope");
				this.ReadWindowsLiveRSTResponseHeaders(ewsXmlReader);
				ewsXmlReader.ReadStartElement("S", "Body");
				this.ParseWindowsLiveRSTResponseBody(ewsXmlReader);
			}
		}

		// Token: 0x04000CE1 RID: 3297
		internal const string XmlEncNamespace = "http://www.w3.org/2001/04/xmlenc#";

		// Token: 0x04000CE2 RID: 3298
		internal const string WindowsLiveSoapNamespacePrefix = "S";

		// Token: 0x04000CE3 RID: 3299
		internal const string RequestSecurityTokenResponseCollectionElementName = "RequestSecurityTokenResponseCollection";

		// Token: 0x04000CE4 RID: 3300
		internal const string RequestSecurityTokenResponseElementName = "RequestSecurityTokenResponse";

		// Token: 0x04000CE5 RID: 3301
		internal const string EncryptedDataElementName = "EncryptedData";

		// Token: 0x04000CE6 RID: 3302
		internal const string PpElementName = "pp";

		// Token: 0x04000CE7 RID: 3303
		internal const string ReqstatusElementName = "reqstatus";

		// Token: 0x04000CE8 RID: 3304
		internal const string SuccessfulReqstatus = "0x0";

		// Token: 0x04000CE9 RID: 3305
		internal const string XmlSignatureReference = "_EWSTKREF";

		// Token: 0x04000CEA RID: 3306
		private string windowsLiveId;

		// Token: 0x04000CEB RID: 3307
		private string password;

		// Token: 0x04000CEC RID: 3308
		private Uri windowsLiveUrl;

		// Token: 0x04000CED RID: 3309
		private bool isAuthenticated;

		// Token: 0x04000CEE RID: 3310
		private bool traceEnabled;

		// Token: 0x04000CEF RID: 3311
		private ITraceListener traceListener = new EwsTraceListener();

		// Token: 0x04000CF0 RID: 3312
		internal static readonly Uri DefaultWindowsLiveUrl = new Uri("https://login.live.com/rst2.srf");
	}
}
