using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000E3 RID: 227
	internal abstract class ServiceRequestBase
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x000267A8 File Offset: 0x000257A8
		protected static Stream GetResponseStream(IEwsHttpWebResponse response)
		{
			string contentEncoding = response.ContentEncoding;
			Stream responseStream = response.GetResponseStream();
			return ServiceRequestBase.WrapStream(responseStream, response.ContentEncoding);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000267D0 File Offset: 0x000257D0
		protected static Stream GetResponseStream(IEwsHttpWebResponse response, int readTimeout)
		{
			Stream responseStream = response.GetResponseStream();
			responseStream.ReadTimeout = readTimeout;
			return ServiceRequestBase.WrapStream(responseStream, response.ContentEncoding);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000267F7 File Offset: 0x000257F7
		private static Stream WrapStream(Stream responseStream, string contentEncoding)
		{
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

		// Token: 0x06000B8D RID: 2957
		internal abstract string GetXmlElementName();

		// Token: 0x06000B8E RID: 2958
		internal abstract string GetResponseXmlElementName();

		// Token: 0x06000B8F RID: 2959
		internal abstract ExchangeVersion GetMinimumRequiredServerVersion();

		// Token: 0x06000B90 RID: 2960
		internal abstract void WriteElementsToXml(EwsServiceXmlWriter writer);

		// Token: 0x06000B91 RID: 2961
		internal abstract object ParseResponse(EwsServiceXmlReader reader);

		// Token: 0x06000B92 RID: 2962 RVA: 0x00026830 File Offset: 0x00025830
		internal virtual object ParseResponse(JsonObject jsonBody)
		{
			ServiceResponse serviceResponse = new ServiceResponse();
			serviceResponse.LoadFromJson(jsonBody, this.Service);
			return serviceResponse;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00026851 File Offset: 0x00025851
		internal virtual bool EmitTimeZoneHeader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00026854 File Offset: 0x00025854
		internal virtual void Validate()
		{
			this.Service.Validate();
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00026861 File Offset: 0x00025861
		internal void WriteBodyToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, this.GetXmlElementName());
			this.WriteAttributesToXml(writer);
			this.WriteElementsToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00026884 File Offset: 0x00025884
		internal virtual void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00026886 File Offset: 0x00025886
		internal ServiceRequestBase(ExchangeService service)
		{
			this.service = service;
			this.ThrowIfNotSupportedByRequestedServerVersion();
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002689B File Offset: 0x0002589B
		internal ExchangeService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000268A3 File Offset: 0x000258A3
		internal void ThrowIfNotSupportedByRequestedServerVersion()
		{
			if (this.Service.RequestedServerVersion < this.GetMinimumRequiredServerVersion())
			{
				throw new ServiceVersionException(string.Format(Strings.RequestIncompatibleWithRequestVersion, this.GetXmlElementName(), this.GetMinimumRequiredServerVersion()));
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000268E0 File Offset: 0x000258E0
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Soap, "Envelope");
			writer.WriteAttributeValue("xmlns", "xsi", "http://www.w3.org/2001/XMLSchema-instance");
			writer.WriteAttributeValue("xmlns", "m", "http://schemas.microsoft.com/exchange/services/2006/messages");
			writer.WriteAttributeValue("xmlns", "t", "http://schemas.microsoft.com/exchange/services/2006/types");
			if (writer.RequireWSSecurityUtilityNamespace)
			{
				writer.WriteAttributeValue("xmlns", "wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
			}
			writer.WriteStartElement(XmlNamespace.Soap, "Header");
			if (this.Service.Credentials != null)
			{
				this.Service.Credentials.EmitExtraSoapHeaderNamespaceAliases(writer.InternalWriter);
			}
			if (!this.Service.SuppressXmlVersionHeader)
			{
				writer.WriteStartElement(XmlNamespace.Types, "RequestServerVersion");
				writer.WriteAttributeValue("Version", this.GetRequestedServiceVersionString());
				writer.WriteEndElement();
			}
			if ((this.Service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1 || this.EmitTimeZoneHeader) && !this.Service.Exchange2007CompatibilityMode)
			{
				writer.WriteStartElement(XmlNamespace.Types, "TimeZoneContext");
				this.Service.TimeZoneDefinition.WriteToXml(writer);
				writer.WriteEndElement();
				writer.IsTimeZoneHeaderEmitted = true;
			}
			if (this.Service.PreferredCulture != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "MailboxCulture", this.Service.PreferredCulture.Name);
			}
			if (this.Service.DateTimePrecision != DateTimePrecision.Default)
			{
				writer.WriteElementValue(XmlNamespace.Types, "DateTimePrecision", this.Service.DateTimePrecision.ToString());
			}
			if (this.Service.ImpersonatedUserId != null)
			{
				this.Service.ImpersonatedUserId.WriteToXml(writer);
			}
			else if (this.Service.PrivilegedUserId != null)
			{
				this.Service.PrivilegedUserId.WriteToXml(writer, this.Service.RequestedServerVersion);
			}
			else if (this.Service.ManagementRoles != null)
			{
				this.Service.ManagementRoles.WriteToXml(writer);
			}
			if (this.Service.Credentials != null)
			{
				this.Service.Credentials.SerializeExtraSoapHeaders(writer.InternalWriter, this.GetXmlElementName());
			}
			this.Service.DoOnSerializeCustomSoapHeaders(writer.InternalWriter);
			writer.WriteEndElement();
			writer.WriteStartElement(XmlNamespace.Soap, "Body");
			this.WriteBodyToXml(writer);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00026B20 File Offset: 0x00025B20
		internal JsonObject CreateJsonRequest()
		{
			IJsonSerializable jsonSerializable = this as IJsonSerializable;
			if (jsonSerializable == null)
			{
				throw new JsonSerializationNotImplementedException();
			}
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Header", this.CreateJsonHeaders());
			jsonObject.Add("Body", jsonSerializable.ToJson(this.service));
			return jsonObject;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00026B6C File Offset: 0x00025B6C
		private JsonObject CreateJsonHeaders()
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("RequestServerVersion", this.GetRequestedServiceVersionString());
			if ((this.Service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1 || this.EmitTimeZoneHeader) && !this.Service.Exchange2007CompatibilityMode)
			{
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.Add("TimeZoneDefinition", this.Service.TimeZoneDefinition.InternalToJson(this.Service));
				jsonObject.Add("TimeZoneContext", jsonObject2);
			}
			if (this.Service.PreferredCulture != null)
			{
				jsonObject.Add("MailboxCulture", this.Service.PreferredCulture.Name);
			}
			if (this.Service.DateTimePrecision != DateTimePrecision.Default)
			{
				jsonObject.Add("DateTimePrecision", this.Service.DateTimePrecision.ToString());
			}
			if (this.Service.ManagementRoles != null)
			{
				jsonObject.Add("ManagementRole", this.Service.ManagementRoles.ToJsonObject());
			}
			return jsonObject;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00026C63 File Offset: 0x00025C63
		private string GetRequestedServiceVersionString()
		{
			if (this.Service.Exchange2007CompatibilityMode && this.Service.RequestedServerVersion == ExchangeVersion.Exchange2007_SP1)
			{
				return "Exchange2007";
			}
			return this.Service.RequestedServerVersion.ToString();
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00026C9C File Offset: 0x00025C9C
		private void EmitRequest(IEwsHttpWebRequest request)
		{
			if (this.Service.RenderingMethod == ExchangeService.RenderingMode.Xml)
			{
				using (Stream webRequestStream = this.GetWebRequestStream(request))
				{
					using (EwsServiceXmlWriter ewsServiceXmlWriter = new EwsServiceXmlWriter(this.Service, webRequestStream))
					{
						this.WriteToXml(ewsServiceXmlWriter);
					}
					return;
				}
			}
			if (this.Service.RenderingMethod == ExchangeService.RenderingMode.JSON)
			{
				JsonObject jsonObject = this.CreateJsonRequest();
				using (Stream webRequestStream2 = this.GetWebRequestStream(request))
				{
					jsonObject.SerializeToJson(webRequestStream2);
				}
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00026D44 File Offset: 0x00025D44
		private void TraceAndEmitRequest(IEwsHttpWebRequest request, bool needSignature, bool needTrace)
		{
			if (this.service.RenderingMethod == ExchangeService.RenderingMode.Xml)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (EwsServiceXmlWriter ewsServiceXmlWriter = new EwsServiceXmlWriter(this.Service, memoryStream))
					{
						ewsServiceXmlWriter.RequireWSSecurityUtilityNamespace = needSignature;
						this.WriteToXml(ewsServiceXmlWriter);
					}
					if (needSignature)
					{
						this.service.Credentials.Sign(memoryStream);
					}
					if (needTrace)
					{
						this.TraceXmlRequest(memoryStream);
					}
					using (Stream webRequestStream = this.GetWebRequestStream(request))
					{
						EwsUtilities.CopyStream(memoryStream, webRequestStream);
					}
					return;
				}
			}
			if (this.service.RenderingMethod == ExchangeService.RenderingMode.JSON)
			{
				JsonObject jsonObject = this.CreateJsonRequest();
				this.TraceJsonRequest(jsonObject);
				using (Stream webRequestStream2 = this.GetWebRequestStream(request))
				{
					jsonObject.SerializeToJson(webRequestStream2);
				}
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00026E40 File Offset: 0x00025E40
		private Stream GetWebRequestStream(IEwsHttpWebRequest request)
		{
			return request.EndGetRequestStream(request.BeginGetRequestStream(null, null));
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00026E50 File Offset: 0x00025E50
		protected object ReadResponse(EwsServiceXmlReader ewsXmlReader)
		{
			this.ReadPreamble(ewsXmlReader);
			ewsXmlReader.ReadStartElement(XmlNamespace.Soap, "Envelope");
			this.ReadSoapHeader(ewsXmlReader);
			ewsXmlReader.ReadStartElement(XmlNamespace.Soap, "Body");
			ewsXmlReader.ReadStartElement(XmlNamespace.Messages, this.GetResponseXmlElementName());
			object obj = this.ParseResponse(ewsXmlReader);
			ewsXmlReader.ReadEndElementIfNecessary(XmlNamespace.Messages, this.GetResponseXmlElementName());
			ewsXmlReader.ReadEndElement(XmlNamespace.Soap, "Body");
			ewsXmlReader.ReadEndElement(XmlNamespace.Soap, "Envelope");
			return obj;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00026EBE File Offset: 0x00025EBE
		protected object BuildResponseObjectFromJson(JsonObject jsonResponse)
		{
			if (jsonResponse.ContainsKey("Header"))
			{
				this.ReadSoapHeader(jsonResponse.ReadAsJsonObject("Header"));
			}
			return this.ParseResponse(jsonResponse.ReadAsJsonObject("Body"));
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00026EEF File Offset: 0x00025EEF
		protected virtual void ReadPreamble(EwsServiceXmlReader ewsXmlReader)
		{
			this.ReadXmlDeclaration(ewsXmlReader);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00026EF8 File Offset: 0x00025EF8
		private void ReadSoapHeader(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Soap, "Header");
			do
			{
				reader.Read();
				if (reader.IsStartElement(XmlNamespace.Types, "ServerVersionInfo"))
				{
					this.Service.ServerInfo = ExchangeServerInfo.Parse(reader);
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Soap, "Header"));
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00026F44 File Offset: 0x00025F44
		private void ReadSoapHeader(JsonObject jsonHeader)
		{
			if (jsonHeader.ContainsKey("ServerVersionInfo"))
			{
				this.Service.ServerInfo = ExchangeServerInfo.Parse(jsonHeader.ReadAsJsonObject("ServerVersionInfo"));
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00026F70 File Offset: 0x00025F70
		protected SoapFaultDetails ReadSoapFault(EwsServiceXmlReader reader)
		{
			SoapFaultDetails soapFaultDetails = null;
			try
			{
				this.ReadXmlDeclaration(reader);
				reader.Read();
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
						if (reader.IsStartElement(XmlNamespace.Types, "ServerVersionInfo"))
						{
							this.Service.ServerInfo = ExchangeServerInfo.Parse(reader);
						}
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

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002706C File Offset: 0x0002606C
		private SoapFaultDetails ReadSoapFault(JsonObject jsonSoapFault)
		{
			SoapFaultDetails soapFaultDetails = null;
			if (jsonSoapFault.ContainsKey("Header"))
			{
				this.ReadSoapHeader(jsonSoapFault.ReadAsJsonObject("Header"));
			}
			if (jsonSoapFault.ContainsKey("Body"))
			{
				soapFaultDetails = SoapFaultDetails.Parse(jsonSoapFault.ReadAsJsonObject("Body"));
			}
			return soapFaultDetails;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000270B8 File Offset: 0x000260B8
		protected IEwsHttpWebResponse ValidateAndEmitRequest(out IEwsHttpWebRequest request)
		{
			this.Validate();
			request = this.BuildEwsHttpWebRequest();
			if (this.service.SendClientLatencies)
			{
				string text = null;
				lock (ServiceRequestBase.clientStatisticsCache)
				{
					if (ServiceRequestBase.clientStatisticsCache.Count > 0)
					{
						text = ServiceRequestBase.clientStatisticsCache[0];
						ServiceRequestBase.clientStatisticsCache.RemoveAt(0);
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					if (request.Headers["X-ClientStatistics"] != null)
					{
						request.Headers["X-ClientStatistics"] = request.Headers["X-ClientStatistics"] + text;
					}
					else
					{
						request.Headers.Add("X-ClientStatistics", text);
					}
				}
			}
			DateTime utcNow = DateTime.UtcNow;
			IEwsHttpWebResponse ewsHttpWebResponse = null;
			try
			{
				ewsHttpWebResponse = this.GetEwsHttpWebResponse(request);
			}
			finally
			{
				if (this.service.SendClientLatencies)
				{
					int num = (int)(DateTime.UtcNow - utcNow).TotalMilliseconds;
					string text2 = string.Empty;
					string text3 = base.GetType().Name.Replace("Request", string.Empty);
					if (ewsHttpWebResponse != null && ewsHttpWebResponse.Headers != null)
					{
						foreach (string text4 in ServiceRequestBase.RequestIdResponseHeaders)
						{
							string text5 = ewsHttpWebResponse.Headers.Get(text4);
							if (!string.IsNullOrEmpty(text5))
							{
								text2 = text5;
								break;
							}
						}
					}
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("MessageId=");
					stringBuilder.Append(text2);
					stringBuilder.Append(",ResponseTime=");
					stringBuilder.Append(num);
					stringBuilder.Append(",SoapAction=");
					stringBuilder.Append(text3);
					stringBuilder.Append(";");
					lock (ServiceRequestBase.clientStatisticsCache)
					{
						ServiceRequestBase.clientStatisticsCache.Add(stringBuilder.ToString());
					}
				}
			}
			return ewsHttpWebResponse;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000272F0 File Offset: 0x000262F0
		protected IEwsHttpWebRequest BuildEwsHttpWebRequest()
		{
			IEwsHttpWebRequest ewsHttpWebRequest2;
			try
			{
				IEwsHttpWebRequest ewsHttpWebRequest = this.Service.PrepareHttpWebRequest(this.GetXmlElementName());
				this.Service.TraceHttpRequestHeaders(TraceFlags.EwsRequestHttpHeaders, ewsHttpWebRequest);
				bool flag = this.Service.Credentials != null && this.Service.Credentials.NeedSignature;
				bool flag2 = this.Service.IsTraceEnabledFor(TraceFlags.EwsRequest);
				if (flag || flag2)
				{
					this.TraceAndEmitRequest(ewsHttpWebRequest, flag, flag2);
				}
				else
				{
					this.EmitRequest(ewsHttpWebRequest);
				}
				ewsHttpWebRequest2 = ewsHttpWebRequest;
			}
			catch (WebException ex)
			{
				if (ex.Status == 7 && ex.Response != null)
				{
					this.ProcessWebException(ex);
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex.Message), ex);
			}
			catch (IOException ex2)
			{
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex2.Message), ex2);
			}
			return ewsHttpWebRequest2;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000273E0 File Offset: 0x000263E0
		protected IEwsHttpWebResponse GetEwsHttpWebResponse(IEwsHttpWebRequest request)
		{
			IEwsHttpWebResponse response;
			try
			{
				response = request.GetResponse();
			}
			catch (WebException ex)
			{
				if (ex.Status == 7 && ex.Response != null)
				{
					this.ProcessWebException(ex);
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex.Message), ex);
			}
			catch (IOException ex2)
			{
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex2.Message), ex2);
			}
			return response;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00027468 File Offset: 0x00026468
		protected IEwsHttpWebResponse EndGetEwsHttpWebResponse(IEwsHttpWebRequest request, IAsyncResult asyncResult)
		{
			IEwsHttpWebResponse ewsHttpWebResponse;
			try
			{
				ewsHttpWebResponse = request.EndGetResponse(asyncResult);
			}
			catch (WebException ex)
			{
				if (ex.Status == 7 && ex.Response != null)
				{
					this.ProcessWebException(ex);
				}
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex.Message), ex);
			}
			catch (IOException ex2)
			{
				throw new ServiceRequestException(string.Format(Strings.ServiceRequestFailed, ex2.Message), ex2);
			}
			return ewsHttpWebResponse;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000274F0 File Offset: 0x000264F0
		private void ProcessWebException(WebException webException)
		{
			if (webException.Response != null)
			{
				IEwsHttpWebResponse ewsHttpWebResponse = this.Service.HttpWebRequestFactory.CreateExceptionResponse(webException);
				SoapFaultDetails soapFaultDetails = null;
				if (ewsHttpWebResponse.StatusCode == 500)
				{
					this.Service.ProcessHttpResponseHeaders(TraceFlags.EwsResponseHttpHeaders, ewsHttpWebResponse);
					if (this.Service.IsTraceEnabledFor(TraceFlags.EwsResponse))
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							using (Stream responseStream = ServiceRequestBase.GetResponseStream(ewsHttpWebResponse))
							{
								EwsUtilities.CopyStream(responseStream, memoryStream);
								memoryStream.Position = 0L;
							}
							if (this.Service.RenderingMethod != ExchangeService.RenderingMode.Xml)
							{
								if (this.Service.RenderingMethod == ExchangeService.RenderingMode.JSON)
								{
									this.TraceResponseJson(ewsHttpWebResponse, memoryStream);
									try
									{
										JsonObject jsonObject = new JsonParser(memoryStream).Parse();
										soapFaultDetails = this.ReadSoapFault(jsonObject);
										goto IL_00DC;
									}
									catch (ServiceJsonDeserializationException)
									{
										goto IL_00DC;
									}
								}
								throw new InvalidOperationException();
							}
							this.TraceResponseXml(ewsHttpWebResponse, memoryStream);
							EwsServiceXmlReader ewsServiceXmlReader = new EwsServiceXmlReader(memoryStream, this.Service);
							soapFaultDetails = this.ReadSoapFault(ewsServiceXmlReader);
							IL_00DC:
							goto IL_0155;
						}
					}
					using (Stream responseStream2 = ServiceRequestBase.GetResponseStream(ewsHttpWebResponse))
					{
						if (this.Service.RenderingMethod != ExchangeService.RenderingMode.Xml)
						{
							if (this.Service.RenderingMethod == ExchangeService.RenderingMode.JSON)
							{
								try
								{
									JsonObject jsonObject2 = new JsonParser(responseStream2).Parse();
									soapFaultDetails = this.ReadSoapFault(jsonObject2);
									goto IL_0147;
								}
								catch (ServiceJsonDeserializationException)
								{
									goto IL_0147;
								}
							}
							throw new InvalidOperationException();
						}
						EwsServiceXmlReader ewsServiceXmlReader2 = new EwsServiceXmlReader(responseStream2, this.Service);
						soapFaultDetails = this.ReadSoapFault(ewsServiceXmlReader2);
						IL_0147:;
					}
					IL_0155:
					if (soapFaultDetails != null)
					{
						ServiceError responseCode = soapFaultDetails.ResponseCode;
						if (responseCode <= ServiceError.ErrorInvalidServerVersion)
						{
							if (responseCode != ServiceError.ErrorIncorrectSchemaVersion)
							{
								if (responseCode == ServiceError.ErrorInvalidServerVersion)
								{
									throw new ServiceVersionException(Strings.ServerVersionNotSupported);
								}
							}
							else
							{
								EwsUtilities.Assert(false, "ServiceRequestBase.ProcessWebException", "Exchange server supports requested version but request was invalid for that version");
							}
						}
						else if (responseCode != ServiceError.ErrorSchemaValidation)
						{
							if (responseCode == ServiceError.ErrorServerBusy)
							{
								throw new ServerBusyException(new ServiceResponse(soapFaultDetails));
							}
						}
						else if (this.Service.ServerInfo != null && this.Service.ServerInfo.MajorVersion == 8 && this.Service.ServerInfo.MinorVersion == 0)
						{
							throw new ServiceVersionException(Strings.ServerVersionNotSupported);
						}
						throw new ServiceResponseException(new ServiceResponse(soapFaultDetails));
					}
				}
				else
				{
					this.Service.ProcessHttpErrorResponse(ewsHttpWebResponse, webException);
				}
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002775C File Offset: 0x0002675C
		protected void TraceXmlRequest(MemoryStream memoryStream)
		{
			this.Service.TraceXml(TraceFlags.EwsRequest, memoryStream);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002776C File Offset: 0x0002676C
		protected void TraceJsonRequest(JsonObject requestObject)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				requestObject.SerializeToJson(memoryStream, this.Service.TraceEnablePrettyPrinting);
				memoryStream.Position = 0L;
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					this.Service.TraceMessage(TraceFlags.EwsRequest, streamReader.ReadToEnd());
				}
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000277E8 File Offset: 0x000267E8
		protected void TraceResponseXml(IEwsHttpWebResponse response, MemoryStream memoryStream)
		{
			if (!string.IsNullOrEmpty(response.ContentType) && (response.ContentType.StartsWith("text/", 5) || response.ContentType.StartsWith("application/soap", 5)))
			{
				this.Service.TraceXml(TraceFlags.EwsResponse, memoryStream);
				return;
			}
			this.Service.TraceMessage(TraceFlags.EwsResponse, "Non-textual response");
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002784C File Offset: 0x0002684C
		protected void TraceResponseJson(IEwsHttpWebResponse response, MemoryStream memoryStream)
		{
			JsonObject jsonObject = new JsonParser(memoryStream).Parse();
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				jsonObject.SerializeToJson(memoryStream2, this.Service.TraceEnablePrettyPrinting);
				memoryStream2.Position = 0L;
				using (StreamReader streamReader = new StreamReader(memoryStream2))
				{
					this.Service.TraceMessage(TraceFlags.EwsResponse, streamReader.ReadToEnd());
				}
			}
			memoryStream.Seek(0L, 0);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000278E0 File Offset: 0x000268E0
		private void ReadXmlDeclaration(EwsServiceXmlReader reader)
		{
			try
			{
				reader.Read(17);
			}
			catch (XmlException ex)
			{
				throw new ServiceRequestException(Strings.ServiceResponseDoesNotContainXml, ex);
			}
			catch (ServiceXmlDeserializationException ex2)
			{
				throw new ServiceRequestException(Strings.ServiceResponseDoesNotContainXml, ex2);
			}
		}

		// Token: 0x040008A9 RID: 2217
		private const string XMLSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x040008AA RID: 2218
		private const string XMLSchemaInstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040008AB RID: 2219
		private const string ClientStatisticsRequestHeader = "X-ClientStatistics";

		// Token: 0x040008AC RID: 2220
		private static readonly string[] RequestIdResponseHeaders = new string[] { "RequestId", "request-id" };

		// Token: 0x040008AD RID: 2221
		private static List<string> clientStatisticsCache = new List<string>();

		// Token: 0x040008AE RID: 2222
		private ExchangeService service;
	}
}
