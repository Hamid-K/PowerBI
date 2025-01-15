using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000016 RID: 22
	internal abstract class AutodiscoverRequest
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000056A9 File Offset: 0x000046A9
		internal AutodiscoverRequest(AutodiscoverService service, Uri url)
		{
			this.service = service;
			this.url = url;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000056BF File Offset: 0x000046BF
		internal static bool IsRedirectionResponse(IEwsHttpWebResponse httpWebResponse)
		{
			return httpWebResponse.StatusCode == 302 || httpWebResponse.StatusCode == 301 || httpWebResponse.StatusCode == 307 || httpWebResponse.StatusCode == 303;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000056F7 File Offset: 0x000046F7
		internal virtual void Validate()
		{
			this.Service.Validate();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005704 File Offset: 0x00004704
		internal AutodiscoverResponse InternalExecute()
		{
			this.Validate();
			AutodiscoverResponse autodiscoverResponse2;
			try
			{
				IEwsHttpWebRequest ewsHttpWebRequest = this.Service.PrepareHttpWebRequestForUrl(this.Url);
				this.Service.TraceHttpRequestHeaders(TraceFlags.AutodiscoverRequestHttpHeaders, ewsHttpWebRequest);
				bool flag = this.Service.Credentials != null && this.Service.Credentials.NeedSignature;
				bool flag2 = this.Service.IsTraceEnabledFor(TraceFlags.AutodiscoverRequest);
				using (Stream requestStream = ewsHttpWebRequest.GetRequestStream())
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (EwsServiceXmlWriter ewsServiceXmlWriter = new EwsServiceXmlWriter(this.Service, memoryStream))
						{
							ewsServiceXmlWriter.RequireWSSecurityUtilityNamespace = flag;
							this.WriteSoapRequest(this.Url, ewsServiceXmlWriter);
						}
						if (flag)
						{
							this.service.Credentials.Sign(memoryStream);
						}
						if (flag2)
						{
							memoryStream.Position = 0L;
							this.Service.TraceXml(TraceFlags.AutodiscoverRequest, memoryStream);
						}
						EwsUtilities.CopyStream(memoryStream, requestStream);
					}
				}
				using (IEwsHttpWebResponse response = ewsHttpWebRequest.GetResponse())
				{
					if (AutodiscoverRequest.IsRedirectionResponse(response))
					{
						AutodiscoverResponse autodiscoverResponse = this.CreateRedirectionResponse(response);
						if (autodiscoverResponse == null)
						{
							throw new ServiceRemoteException(Strings.InvalidRedirectionResponseReturned);
						}
						autodiscoverResponse2 = autodiscoverResponse;
					}
					else
					{
						using (Stream responseStream = AutodiscoverRequest.GetResponseStream(response))
						{
							using (MemoryStream memoryStream2 = new MemoryStream())
							{
								EwsUtilities.CopyStream(responseStream, memoryStream2);
								memoryStream2.Position = 0L;
								this.Service.TraceResponse(response, memoryStream2);
								EwsXmlReader ewsXmlReader = new EwsXmlReader(memoryStream2);
								ewsXmlReader.Read();
								if (ewsXmlReader.NodeType == 17)
								{
									ewsXmlReader.ReadStartElement(XmlNamespace.Soap, "Envelope");
								}
								else if (ewsXmlReader.NodeType != 1 || ewsXmlReader.LocalName != "Envelope" || ewsXmlReader.NamespaceUri != EwsUtilities.GetNamespaceUri(XmlNamespace.Soap))
								{
									throw new ServiceXmlDeserializationException(Strings.InvalidAutodiscoverServiceResponse);
								}
								this.ReadSoapHeaders(ewsXmlReader);
								AutodiscoverResponse autodiscoverResponse3 = this.ReadSoapBody(ewsXmlReader);
								ewsXmlReader.ReadEndElement(XmlNamespace.Soap, "Envelope");
								if (autodiscoverResponse3.ErrorCode != AutodiscoverErrorCode.NoError)
								{
									throw new AutodiscoverResponseException(autodiscoverResponse3.ErrorCode, autodiscoverResponse3.ErrorMessage);
								}
								autodiscoverResponse2 = autodiscoverResponse3;
							}
						}
					}
				}
			}
			catch (WebException ex)
			{
				if (ex.Status == 7 && ex.Response != null)
				{
					IEwsHttpWebResponse ewsHttpWebResponse = this.Service.HttpWebRequestFactory.CreateExceptionResponse(ex);
					if (AutodiscoverRequest.IsRedirectionResponse(ewsHttpWebResponse))
					{
						this.Service.ProcessHttpResponseHeaders(TraceFlags.AutodiscoverResponseHttpHeaders, ewsHttpWebResponse);
						AutodiscoverResponse autodiscoverResponse4 = this.CreateRedirectionResponse(ewsHttpWebResponse);
						if (autodiscoverResponse4 != null)
						{
							return autodiscoverResponse4;
						}
					}
					else
					{
						this.ProcessWebException(ex);
					}
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex.Message), ex);
			}
			catch (XmlException ex2)
			{
				this.Service.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("XML parsing error: {0}", ex2.Message));
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex2.Message), ex2);
			}
			catch (IOException ex3)
			{
				this.Service.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("I/O error: {0}", ex3.Message));
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex3.Message), ex3);
			}
			return autodiscoverResponse2;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005B18 File Offset: 0x00004B18
		private void ProcessWebException(WebException webException)
		{
			if (webException.Response != null)
			{
				IEwsHttpWebResponse ewsHttpWebResponse = this.Service.HttpWebRequestFactory.CreateExceptionResponse(webException);
				if (ewsHttpWebResponse.StatusCode == 500)
				{
					SoapFaultDetails soapFaultDetails;
					if (this.Service.IsTraceEnabledFor(TraceFlags.AutodiscoverRequest))
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							using (Stream responseStream = AutodiscoverRequest.GetResponseStream(ewsHttpWebResponse))
							{
								EwsUtilities.CopyStream(responseStream, memoryStream);
								memoryStream.Position = 0L;
							}
							this.Service.TraceResponse(ewsHttpWebResponse, memoryStream);
							EwsXmlReader ewsXmlReader = new EwsXmlReader(memoryStream);
							soapFaultDetails = this.ReadSoapFault(ewsXmlReader);
							goto IL_00B6;
						}
					}
					using (Stream responseStream2 = AutodiscoverRequest.GetResponseStream(ewsHttpWebResponse))
					{
						EwsXmlReader ewsXmlReader2 = new EwsXmlReader(responseStream2);
						soapFaultDetails = this.ReadSoapFault(ewsXmlReader2);
					}
					IL_00B6:
					if (soapFaultDetails != null)
					{
						throw new ServiceResponseException(new ServiceResponse(soapFaultDetails));
					}
				}
				else
				{
					this.Service.ProcessHttpErrorResponse(ewsHttpWebResponse, webException);
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005C20 File Offset: 0x00004C20
		private AutodiscoverResponse CreateRedirectionResponse(IEwsHttpWebResponse httpWebResponse)
		{
			string text = httpWebResponse.Headers[23];
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					Uri uri = new Uri(this.Url, text);
					if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
					{
						AutodiscoverResponse autodiscoverResponse = this.CreateServiceResponse();
						autodiscoverResponse.ErrorCode = AutodiscoverErrorCode.RedirectUrl;
						autodiscoverResponse.RedirectionUrl = uri;
						return autodiscoverResponse;
					}
					this.Service.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Invalid redirection URL '{0}' returned by Autodiscover service.", uri));
					goto IL_00AD;
				}
				catch (UriFormatException)
				{
					this.Service.TraceMessage(TraceFlags.AutodiscoverConfiguration, string.Format("Invalid redirection location '{0}' returned by Autodiscover service.", text));
					goto IL_00AD;
				}
			}
			this.Service.TraceMessage(TraceFlags.AutodiscoverConfiguration, "Redirection response returned by Autodiscover service without redirection location.");
			IL_00AD:
			return null;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005CF0 File Offset: 0x00004CF0
		private SoapFaultDetails ReadSoapFault(EwsXmlReader reader)
		{
			SoapFaultDetails soapFaultDetails = null;
			try
			{
				reader.Read();
				if (reader.NodeType == 17)
				{
					reader.Read();
				}
				if (!reader.IsStartElement() || reader.LocalName != "Envelope")
				{
					return soapFaultDetails;
				}
				XmlNamespace namespaceFromUri = EwsUtilities.GetNamespaceFromUri(reader.NamespaceUri);
				if (namespaceFromUri == XmlNamespace.NotSpecified)
				{
					return soapFaultDetails;
				}
				reader.Read();
				if (reader.IsStartElement(namespaceFromUri, "Header"))
				{
					do
					{
						reader.Read();
					}
					while (!reader.IsEndElement(namespaceFromUri, "Header"));
					reader.Read();
				}
				if (reader.IsStartElement(namespaceFromUri, "Body"))
				{
					do
					{
						reader.Read();
						if (reader.IsStartElement(namespaceFromUri, "Fault"))
						{
							soapFaultDetails = SoapFaultDetails.Parse(reader, namespaceFromUri);
						}
					}
					while (!reader.IsEndElement(namespaceFromUri, "Body"));
				}
				reader.ReadEndElement(namespaceFromUri, "Envelope");
			}
			catch (XmlException)
			{
			}
			return soapFaultDetails;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005DD4 File Offset: 0x00004DD4
		internal void WriteSoapRequest(Uri requestUrl, EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Soap, "Envelope");
			writer.WriteAttributeValue("xmlns", "a", "http://schemas.microsoft.com/exchange/2010/Autodiscover");
			writer.WriteAttributeValue("xmlns", "wsa", "http://www.w3.org/2005/08/addressing");
			writer.WriteAttributeValue("xmlns", "xsi", "http://www.w3.org/2001/XMLSchema-instance");
			if (writer.RequireWSSecurityUtilityNamespace)
			{
				writer.WriteAttributeValue("xmlns", "wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
			}
			writer.WriteStartElement(XmlNamespace.Soap, "Header");
			if (this.Service.Credentials != null)
			{
				this.Service.Credentials.EmitExtraSoapHeaderNamespaceAliases(writer.InternalWriter);
			}
			writer.WriteElementValue(XmlNamespace.Autodiscover, "RequestedServerVersion", this.Service.RequestedServerVersion.ToString());
			writer.WriteElementValue(XmlNamespace.WSAddressing, "Action", this.GetWsAddressingActionName());
			writer.WriteElementValue(XmlNamespace.WSAddressing, "To", requestUrl.AbsoluteUri);
			this.WriteExtraCustomSoapHeadersToXml(writer);
			if (this.Service.Credentials != null)
			{
				this.Service.Credentials.SerializeWSSecurityHeaders(writer.InternalWriter);
			}
			this.Service.DoOnSerializeCustomSoapHeaders(writer.InternalWriter);
			writer.WriteEndElement();
			writer.WriteStartElement(XmlNamespace.Soap, "Body");
			this.WriteBodyToXml(writer);
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.Flush();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005F26 File Offset: 0x00004F26
		internal virtual void WriteExtraCustomSoapHeadersToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005F28 File Offset: 0x00004F28
		internal void WriteBodyToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Autodiscover, this.GetRequestXmlElementName());
			this.WriteAttributesToXml(writer);
			this.WriteElementsToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005F4C File Offset: 0x00004F4C
		protected static Stream GetResponseStream(IEwsHttpWebResponse response)
		{
			string contentEncoding = response.ContentEncoding;
			Stream responseStream = response.GetResponseStream();
			if (contentEncoding.ToLowerInvariant().Contains("gzip"))
			{
				return new GZipStream(responseStream, 0);
			}
			if (contentEncoding.ToLowerInvariant().Contains("deflate"))
			{
				return new DeflateStream(responseStream, 0);
			}
			return responseStream;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005F9C File Offset: 0x00004F9C
		internal void ReadSoapHeaders(EwsXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Soap, "Header");
			do
			{
				reader.Read();
				this.ReadSoapHeader(reader);
			}
			while (!reader.IsEndElement(XmlNamespace.Soap, "Header"));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005FC5 File Offset: 0x00004FC5
		internal virtual void ReadSoapHeader(EwsXmlReader reader)
		{
			if (reader.IsStartElement(XmlNamespace.Autodiscover, "ServerVersionInfo"))
			{
				this.service.ServerInfo = this.ReadServerVersionInfo(reader);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005FE8 File Offset: 0x00004FE8
		private ExchangeServerInfo ReadServerVersionInfo(EwsXmlReader reader)
		{
			ExchangeServerInfo exchangeServerInfo = new ExchangeServerInfo();
			do
			{
				reader.Read();
				string localName;
				if (reader.IsStartElement() && (localName = reader.LocalName) != null)
				{
					if (!(localName == "MajorVersion"))
					{
						if (!(localName == "MinorVersion"))
						{
							if (!(localName == "MajorBuildNumber"))
							{
								if (!(localName == "MinorBuildNumber"))
								{
									if (localName == "Version")
									{
										exchangeServerInfo.VersionString = reader.ReadElementValue();
									}
								}
								else
								{
									exchangeServerInfo.MinorBuildNumber = reader.ReadElementValue<int>();
								}
							}
							else
							{
								exchangeServerInfo.MajorBuildNumber = reader.ReadElementValue<int>();
							}
						}
						else
						{
							exchangeServerInfo.MinorVersion = reader.ReadElementValue<int>();
						}
					}
					else
					{
						exchangeServerInfo.MajorVersion = reader.ReadElementValue<int>();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "ServerVersionInfo"));
			return exchangeServerInfo;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000060B4 File Offset: 0x000050B4
		internal AutodiscoverResponse ReadSoapBody(EwsXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Soap, "Body");
			AutodiscoverResponse autodiscoverResponse = this.LoadFromXml(reader);
			reader.ReadEndElement(XmlNamespace.Soap, "Body");
			return autodiscoverResponse;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000060E4 File Offset: 0x000050E4
		internal AutodiscoverResponse LoadFromXml(EwsXmlReader reader)
		{
			string responseXmlElementName = this.GetResponseXmlElementName();
			reader.ReadStartElement(XmlNamespace.Autodiscover, responseXmlElementName);
			AutodiscoverResponse autodiscoverResponse = this.CreateServiceResponse();
			autodiscoverResponse.LoadFromXml(reader, responseXmlElementName);
			return autodiscoverResponse;
		}

		// Token: 0x060000EE RID: 238
		internal abstract string GetRequestXmlElementName();

		// Token: 0x060000EF RID: 239
		internal abstract string GetResponseXmlElementName();

		// Token: 0x060000F0 RID: 240
		internal abstract string GetWsAddressingActionName();

		// Token: 0x060000F1 RID: 241
		internal abstract AutodiscoverResponse CreateServiceResponse();

		// Token: 0x060000F2 RID: 242
		internal abstract void WriteAttributesToXml(EwsServiceXmlWriter writer);

		// Token: 0x060000F3 RID: 243
		internal abstract void WriteElementsToXml(EwsServiceXmlWriter writer);

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006111 File Offset: 0x00005111
		internal AutodiscoverService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006119 File Offset: 0x00005119
		internal Uri Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x0400005C RID: 92
		private AutodiscoverService service;

		// Token: 0x0400005D RID: 93
		private Uri url;
	}
}
