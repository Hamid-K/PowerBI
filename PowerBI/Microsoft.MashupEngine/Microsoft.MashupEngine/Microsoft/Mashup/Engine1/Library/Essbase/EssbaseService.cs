using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Essbase
{
	// Token: 0x02000C81 RID: 3201
	internal class EssbaseService
	{
		// Token: 0x060056B3 RID: 22195 RVA: 0x0012C7B8 File Offset: 0x0012A9B8
		public EssbaseService(IEngineHost host, OptionsRecord options, string urlString, IResource resource)
		{
			this.host = host;
			this.resource = resource;
			this.options = options;
			this.tracer = new Tracer(this.host, "Engine/IO/Essbase/", this.resource, null, null);
			try
			{
				this.url = TextValue.New(new UriBuilder(new Uri(urlString))
				{
					Query = "method=POST"
				}.ToString());
			}
			catch (UriFormatException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.EssbaseInvalidServer, TextValue.New(urlString), ex);
			}
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x060056B4 RID: 22196 RVA: 0x0012C84C File Offset: 0x0012AA4C
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x060056B5 RID: 22197 RVA: 0x0012C854 File Offset: 0x0012AA54
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x0012C85C File Offset: 0x0012AA5C
		private string Wrap(string element, string content)
		{
			if (content.Length != 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", element, content);
			}
			return "";
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x0012C87D File Offset: 0x0012AA7D
		public StreamReader ExecuteDiscoverRequest(string request, string sourceInfo = "", string application = "", string cube = "", IList<KeyValuePair<EssbaseXmlaElement, string>> restrictions = null)
		{
			return this.ExecuteRequest("Discover", this.Wrap("RequestType", request), sourceInfo, application, cube, restrictions, true);
		}

		// Token: 0x060056B8 RID: 22200 RVA: 0x0012C8A0 File Offset: 0x0012AAA0
		public StreamReader ExecuteRequest(string method, string request, string sourceInfo = "", string application = "", string cube = "", IList<KeyValuePair<EssbaseXmlaElement, string>> restrictions = null, bool isMetadata = false)
		{
			ResourceCredentialCollection credentials = this.GetCredentials();
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", this.Wrap(EssbaseXmlaElement.CATALOG_NAME.ToString(), application), this.Wrap(EssbaseXmlaElement.CUBE_NAME.ToString(), cube));
			if (restrictions != null)
			{
				foreach (KeyValuePair<EssbaseXmlaElement, string> keyValuePair in restrictions)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}", this.Wrap(keyValuePair.Key.ToString(), keyValuePair.Value));
				}
			}
			int num;
			DurationValue durationValue = (this.options.TryGetDurationAsSeconds("CommandTimeout", out num) ? DurationValue.New(TimeSpan.FromSeconds((double)num)) : null);
			string xmlaRequest = string.Format(CultureInfo.InvariantCulture, "<?xml version=\"1.0\" encoding='UTF-8'?>\r\n                <SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" \r\n                                   xmlns:xsi=\"http://www.w3.org/1999/XMLSchema-instance\" \r\n                                   xmlns:xsd=\"http://www.w3.org/1999/XMLSchema\">\r\n                  <SOAP-ENV:Body>\r\n                    <{0} xmlns=\"urn:schemas-microsoft-com:xml-analysis\"\r\n                      SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n                    {1}\r\n                      <Restrictions>\r\n                        <RestrictionList>\r\n                          {2}\r\n                        </RestrictionList>\r\n                      </Restrictions>\r\n                      <Properties>\r\n                        <PropertyList>\r\n                          {3}{4}\r\n                          <Format>Tabular</Format>\r\n                          <AxisFormat>TupleFormat</AxisFormat>\r\n                        </PropertyList>\r\n                      </Properties>\r\n                    </{0}>\r\n                  </SOAP-ENV:Body>\r\n                </SOAP-ENV:Envelope>", new object[]
			{
				method,
				request,
				stringBuilder,
				this.Wrap("DataSourceInfo", sourceInfo),
				this.Wrap("Catalog", application)
			});
			this.tracer.Trace("ExecuteRequest", delegate(IHostTrace trace)
			{
				trace.Add("url", this.url, true);
				trace.Add("xmlaRequest", xmlaRequest, true);
			});
			Request request2 = Request.Create(this.host, "Essbase", null, this.url, null, BinaryValue.New(Encoding.UTF8.GetBytes(xmlaRequest)), null, RecordValue.New(Keys.New("User-Agent", "Content-Type", "SOAPAction"), new Value[]
			{
				TextValue.New("XmlaClient"),
				TextValue.New("text/xml"),
				TextValue.New("\"urn:schemas-microsoft-com:xml-analysis:" + method + "\"")
			}), durationValue, null, null, null, null, null);
			request2.IsMetadata = isMetadata;
			Encoding utf = Encoding.UTF8;
			StreamReader streamReader;
			try
			{
				streamReader = new StreamReader(request2.GetResponse(credentials, null, false).GetResponseStream(), utf);
			}
			catch (ResponseException ex)
			{
				throw DataSourceException.NewDataSourceError<Message2>(this.host, Strings.WebRequestFailed("Essbase", ex.Message), request2.RequestResource, "Url", request2.InitialUri, TypeValue.Text, ex.InnerException);
			}
			return streamReader;
		}

		// Token: 0x060056B9 RID: 22201 RVA: 0x0012CB08 File Offset: 0x0012AD08
		public IDataReader ExecuteMdx(string sourceInfo, string application, string query, Dictionary<string, string> aliasDict = null)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			query = query.Replace("\r\n", "\n").Replace("\n", "\r\n");
			query = xmlDocument.CreateCDataSection(query).OuterXml;
			TableValue tableValue = EssbaseXmlaParser.ParseMdxResponse(this.ExecuteRequest("Execute", this.Wrap("Command", this.Wrap("Statement", query)), sourceInfo, application, "", null, false), aliasDict);
			return new TableDataReader(tableValue.Type.AsTableType, new TableValueDataReader(tableValue, true), null);
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x0012CB9C File Offset: 0x0012AD9C
		private ResourceCredentialCollection GetCredentials()
		{
			ResourceCredentialCollection resourceCredentialCollection;
			HttpServices.VerifyPermissionAndGetCredentials(this.host, this.Resource, out resourceCredentialCollection);
			return resourceCredentialCollection;
		}

		// Token: 0x040030D1 RID: 12497
		private const string userAgent = "User-Agent";

		// Token: 0x040030D2 RID: 12498
		private const string contentType = "Content-Type";

		// Token: 0x040030D3 RID: 12499
		private const string soapAction = "SOAPAction";

		// Token: 0x040030D4 RID: 12500
		private readonly IEngineHost host;

		// Token: 0x040030D5 RID: 12501
		private readonly OptionsRecord options;

		// Token: 0x040030D6 RID: 12502
		private readonly IResource resource;

		// Token: 0x040030D7 RID: 12503
		private readonly TextValue url;

		// Token: 0x040030D8 RID: 12504
		private readonly Tracer tracer;
	}
}
