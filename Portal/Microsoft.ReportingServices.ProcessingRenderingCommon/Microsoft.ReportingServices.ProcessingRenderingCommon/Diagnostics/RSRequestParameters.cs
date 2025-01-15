using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000087 RID: 135
	internal abstract class RSRequestParameters : IReportParameterLookup, IRSRequestParameters
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		static RSRequestParameters()
		{
			RSRequestParameters.m_crescentCommands.Add("RenderEdit", true);
			RSRequestParameters.m_crescentCommands.Add("GetModel", true);
			RSRequestParameters.m_crescentCommands.Add("ExecuteQueries", true);
			RSRequestParameters.m_crescentCommands.Add("LogClientTraceEvents", true);
			RSRequestParameters.m_crescentCommands.Add("CloseSession", true);
			RSRequestParameters.m_crescentCommands.Add("CancelProgressiveSessionJobs", true);
			RSRequestParameters.m_crescentCommands.Add("GetExternalImages", true);
			RSRequestParameters.m_crescentCommands.Add("GetReportAndModels", true);
			RSRequestParameters.m_powerViewCommands.Add("CloseSession", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("OpenSession", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("LogClientActivities", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("LogClientTraces", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("ExecuteCommands", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("LoadDocument", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("LoadReport", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("GetVisual", RSRequestParameters.HttpMethod.GET);
			RSRequestParameters.m_powerViewCommands.Add("SaveReport", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("ExecuteSemanticQuery", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("GetEntityDataModel", RSRequestParameters.HttpMethod.GET);
			RSRequestParameters.m_powerViewCommands.Add("GetSemanticQuery", RSRequestParameters.HttpMethod.POST);
			RSRequestParameters.m_powerViewCommands.Add("GetDocument", RSRequestParameters.HttpMethod.GET);
			RSRequestParameters.m_powerViewServerVrmCommands.Add("ExecuteCommands");
			RSRequestParameters.m_powerViewServerVrmCommands.Add("LoadDocument");
			RSRequestParameters.m_powerViewServerVrmCommands.Add("LoadReport");
			RSRequestParameters.m_powerViewServerVrmCommands.Add("GetVisual");
			RSRequestParameters.m_powerViewServerVrmCommands.Add("ExecuteSemanticQuery");
			RSRequestParameters.m_powerViewServerVrmCommands.Add("SaveReport");
			RSRequestParameters.m_powerViewServerVrmCommands.Add("GetSemanticQuery");
			RSRequestParameters.m_powerViewServerVrmCommands.Add("GetDocument");
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000C1A3 File Offset: 0x0000A3A3
		internal static bool IsCrescentCommand(string command)
		{
			return !string.IsNullOrEmpty(command) && RSRequestParameters.m_crescentCommands.ContainsKey(command);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000C1BA File Offset: 0x0000A3BA
		internal static bool IsPowerViewCommand(string command)
		{
			return !string.IsNullOrEmpty(command) && RSRequestParameters.m_powerViewCommands.ContainsKey(command);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000C1D1 File Offset: 0x0000A3D1
		internal static bool IsPowerViewVrmCommand(string command)
		{
			return !string.IsNullOrEmpty(command) && RSRequestParameters.m_powerViewServerVrmCommands.Contains(command);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		internal static bool IsPOSTOnlyCommand(string command)
		{
			return RSRequestParameters.IsPowerViewCommand(command) && RSRequestParameters.m_powerViewCommands[command] == RSRequestParameters.HttpMethod.POST;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000C204 File Offset: 0x0000A404
		string IReportParameterLookup.GetReportParamsInstanceId(NameValueCollection reportParameters)
		{
			if (this.m_reverseLookupParameters == null)
			{
				return null;
			}
			ReportParameterCollection reportParameterCollection = new ReportParameterCollection(reportParameters);
			return (string)this.m_reverseLookupParameters[reportParameterCollection];
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000C233 File Offset: 0x0000A433
		public static string GetFallbackFormat()
		{
			return "HTML5";
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000C23A File Offset: 0x0000A43A
		public void ParseQueryString(NameValueCollection allParametersCollection, IParametersTranslator paramsTranslator, ExternalItemPath itemPath)
		{
			RSRequestParameters.ParseQueryString(itemPath, paramsTranslator, allParametersCollection, out this.m_catalogParameters, out this.m_renderingParameters, out this.m_reportParameters, out this.m_datasourcesCred, out this.m_reverseLookupParameters);
			this.ApplyDefaultRenderingParameters();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000C268 File Offset: 0x0000A468
		public string GetImageUrl(bool useSessionId, string imageId, ICatalogItemContext ctx)
		{
			string text = null;
			if (this.m_renderingParameters != null)
			{
				text = this.m_renderingParameters["StreamRoot"];
			}
			if (text != null && text != string.Empty)
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				if (imageId != null)
				{
					stringBuilder.Append(imageId);
				}
				return stringBuilder.ToString();
			}
			CatalogItemUrlBuilder catalogItemUrlBuilder = new CatalogItemUrlBuilder(ctx);
			string snapshotParamValue = this.SnapshotParamValue;
			if (snapshotParamValue != null)
			{
				catalogItemUrlBuilder.AppendCatalogParameter("Snapshot", snapshotParamValue);
			}
			string sessionIDParamValue = this.SessionIDParamValue;
			if (sessionIDParamValue != null)
			{
				catalogItemUrlBuilder.AppendCatalogParameter("SessionID", sessionIDParamValue);
			}
			else if (useSessionId && this.m_SessionId != null)
			{
				catalogItemUrlBuilder.AppendCatalogParameter("SessionID", this.m_SessionId);
			}
			string formatParamValue = this.FormatParamValue;
			if (formatParamValue != null)
			{
				catalogItemUrlBuilder.AppendCatalogParameter("Format", formatParamValue);
			}
			catalogItemUrlBuilder.AppendCatalogParameter("ImageID", imageId);
			return catalogItemUrlBuilder.ToString();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000C338 File Offset: 0x0000A538
		public static NameValueCollection ExtractReportParameters(NameValueCollection allParametersCollection, ref bool[] whichParamsAreShared, out NameValueCollection otherParameters)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			otherParameters = new NameValueCollection();
			List<bool> list = new List<bool>();
			for (int i = 0; i < allParametersCollection.Count; i++)
			{
				string text = allParametersCollection.GetKey(i);
				string[] array = allParametersCollection.GetValues(i);
				if (array != null && text != null)
				{
					if (StringSupport.EndsWith(text, ":isnull", true, CultureInfo.InvariantCulture))
					{
						text = text.Substring(0, text.Length - ":isnull".Length);
						array = new string[1];
					}
					if (StringSupport.EndsWith(text, ":isnull", true, CultureInfo.InvariantCulture))
					{
						text = text.Substring(0, text.Length - ":isnull".Length);
						array = new string[1];
					}
					if (StringSupport.StartsWith(text, "rs:", true, CultureInfo.InvariantCulture) || StringSupport.StartsWith(text, "rc:", true, CultureInfo.InvariantCulture) || StringSupport.StartsWith(text, "dsu:", true, CultureInfo.InvariantCulture) || StringSupport.StartsWith(text, "dsp:", true, CultureInfo.InvariantCulture))
					{
						if (!RSRequestParameters.TryToAddToCollection(text, array, null, false, otherParameters))
						{
							throw new InternalCatalogException("expected to add parameter to collection" + text);
						}
					}
					else
					{
						if (!RSRequestParameters.TryToAddToCollection(text, array, "", true, nameValueCollection))
						{
							throw new InternalCatalogException("expected to add parameter to collection" + text);
						}
						if (whichParamsAreShared != null && whichParamsAreShared.Length != 0)
						{
							list.Add(whichParamsAreShared[i]);
						}
					}
				}
			}
			if (whichParamsAreShared != null && whichParamsAreShared.Length != 0)
			{
				whichParamsAreShared = list.ToArray();
			}
			return nameValueCollection;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		public static NameValueCollection ShallowXmlToNameValueCollection(string xml, string topElementTag)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (xml == null || xml == string.Empty)
			{
				return nameValueCollection;
			}
			XmlTextReader xmlTextReader = XmlUtil.SafeCreateXmlTextReader(xml);
			try
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.NodeType != XmlNodeType.Element || string.Compare(xmlTextReader.Name, topElementTag, StringComparison.Ordinal) != 0)
				{
					throw new InvalidXmlException();
				}
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.IsStartElement())
					{
						bool isEmptyElement = xmlTextReader.IsEmptyElement;
						string text = xmlTextReader.Name;
						text = XmlUtil.DecodePropertyName(text);
						string text2 = xmlTextReader.ReadString();
						if (nameValueCollection.GetValues(text) != null)
						{
							throw new InvalidXmlException();
						}
						nameValueCollection[text] = text2;
						if (!isEmptyElement && xmlTextReader.IsStartElement())
						{
							throw new InvalidXmlException();
						}
					}
				}
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
			return nameValueCollection;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000C564 File Offset: 0x0000A764
		public static NameValueCollection DeepXmlToNameValueCollection(string xml, string topElementTag, string eachElementTag, string nameElementTag, string valueElementTag)
		{
			NameValueCollection nameValueCollection = new NameValueCollection(StringComparer.InvariantCulture);
			if (xml == null || xml == string.Empty)
			{
				return nameValueCollection;
			}
			XmlTextReader xmlTextReader = XmlUtil.SafeCreateXmlTextReader(xml);
			try
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.NodeType != XmlNodeType.Element || string.Compare(xmlTextReader.Name, topElementTag, StringComparison.Ordinal) != 0)
				{
					throw new InvalidXmlException();
				}
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.IsStartElement())
					{
						if (xmlTextReader.IsEmptyElement || string.Compare(xmlTextReader.Name, eachElementTag, StringComparison.Ordinal) != 0)
						{
							throw new InvalidXmlException();
						}
						xmlTextReader.Read();
						string text = null;
						string text2 = null;
						while (xmlTextReader.IsStartElement())
						{
							bool isEmptyElement = xmlTextReader.IsEmptyElement;
							string name = xmlTextReader.Name;
							string text3 = xmlTextReader.ReadString();
							if (string.Compare(name, nameElementTag, StringComparison.Ordinal) == 0)
							{
								text = text3;
							}
							else if (string.Compare(name, valueElementTag, StringComparison.Ordinal) == 0)
							{
								text2 = text3;
							}
							if (!isEmptyElement)
							{
								xmlTextReader.ReadEndElement();
							}
							else
							{
								xmlTextReader.Read();
							}
						}
						if (text == null)
						{
							throw new InvalidXmlException();
						}
						nameValueCollection.Add(text, text2);
					}
				}
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
			return nameValueCollection;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000C678 File Offset: 0x0000A878
		public static string NameValueCollectionToShallowXml(NameValueCollection parameters, string topElementTag)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartElement(topElementTag);
			for (int i = 0; i < parameters.Count; i++)
			{
				string key = parameters.GetKey(i);
				string text = parameters.Get(i);
				if (key != null && text != null)
				{
					if (string.IsNullOrEmpty(key))
					{
						throw new InternalCatalogException("Empty Property Name");
					}
					string text2 = XmlUtil.EncodePropertyName(key);
					RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(text2), "encodedName");
					xmlTextWriter.WriteStartElement(text2);
					xmlTextWriter.WriteString(text);
					xmlTextWriter.WriteEndElement();
				}
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000C724 File Offset: 0x0000A924
		public static string NameValueCollectionToDeepXml(NameValueCollection parameters, string topElementTag, string eachElementTag, string nameElementTag, string valueElementTag)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartElement(topElementTag);
			for (int i = 0; i < parameters.Count; i++)
			{
				xmlTextWriter.WriteStartElement(eachElementTag);
				string key = parameters.GetKey(i);
				if (key != null)
				{
					xmlTextWriter.WriteElementString(nameElementTag, key);
				}
				string text = parameters.Get(i);
				if (text != null)
				{
					xmlTextWriter.WriteElementString(valueElementTag, text);
				}
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000C7A5 File Offset: 0x0000A9A5
		public NameValueCollection RenderingParameters
		{
			get
			{
				return this.m_renderingParameters;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000C7AD File Offset: 0x0000A9AD
		public void SetRenderingParameters(string renderingParametersXml)
		{
			this.m_renderingParameters = RSRequestParameters.ShallowXmlToNameValueCollection(renderingParametersXml, "DeviceInfo");
			this.ApplyDefaultRenderingParameters();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000C7C6 File Offset: 0x0000A9C6
		public void SetRenderingParameters(NameValueCollection otherParams)
		{
			if (otherParams != null)
			{
				this.m_renderingParameters = new NameValueCollection(otherParams);
				return;
			}
			this.m_renderingParameters = new NameValueCollection();
		}

		// Token: 0x060003EA RID: 1002
		protected abstract void ApplyDefaultRenderingParameters();

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000C7E3 File Offset: 0x0000A9E3
		public NameValueCollection ReportParameters
		{
			get
			{
				return this.m_reportParameters;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		public string ReportParametersXml
		{
			get
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartElement("Parameters");
				for (int i = 0; i < this.m_reportParameters.Count; i++)
				{
					xmlTextWriter.WriteStartElement("Parameter");
					string key = this.m_reportParameters.GetKey(i);
					if (key != null)
					{
						xmlTextWriter.WriteElementString("Name", key);
					}
					string[] values = this.m_reportParameters.GetValues(i);
					xmlTextWriter.WriteStartElement("Values");
					if (values != null)
					{
						for (int j = 0; j < values.Length; j++)
						{
							xmlTextWriter.WriteElementString("Value", values[j]);
						}
					}
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
				}
				xmlTextWriter.WriteEndElement();
				return stringWriter.ToString();
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000C8B5 File Offset: 0x0000AAB5
		public void SetReportParameters(string reportParametersXml)
		{
			this.m_reportParameters = RSRequestParameters.DeepXmlToNameValueCollection(reportParametersXml, "Parameters", "Parameter", "Name", "Value");
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000C8D7 File Offset: 0x0000AAD7
		public void SetReportParameters(NameValueCollection allReportParameters)
		{
			this.m_reportParameters = allReportParameters;
			if (this.m_reportParameters == null)
			{
				this.m_reportParameters = new NameValueCollection();
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		public void SetReportParameters(NameValueCollection allReportParameters, IParametersTranslator paramsTranslator)
		{
			if (allReportParameters != null)
			{
				string text = allReportParameters["rs:StoredParametersID"];
				if (text != null)
				{
					ExternalItemPath externalItemPath;
					NameValueCollection nameValueCollection;
					paramsTranslator.GetParamsInstance(text, out externalItemPath, out nameValueCollection);
					if (nameValueCollection == null)
					{
						throw new StoredParameterNotFoundException(text);
					}
					NameValueCollection nameValueCollection2 = new NameValueCollection();
					foreach (object obj in nameValueCollection)
					{
						string text2 = (string)obj;
						string[] values = nameValueCollection.GetValues(text2);
						RSRequestParameters.TryToAddToCollection(text2, values, "", true, nameValueCollection2);
					}
					this.m_reportParameters = nameValueCollection2;
					return;
				}
			}
			this.SetReportParameters(allReportParameters);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		public NameValueCollection CatalogParameters
		{
			get
			{
				return this.m_catalogParameters;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		public void SetCatalogParameters(string catalogParametersXml)
		{
			this.m_catalogParameters = RSRequestParameters.ShallowXmlToNameValueCollection(catalogParametersXml, "Parameters");
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000C9BF File Offset: 0x0000ABBF
		public void DetectFormatIfNotPresent()
		{
			RSRequestParameters.GuessFormatIfNotPresent(this.m_catalogParameters);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		public string FormatParamValue
		{
			get
			{
				if (this.CatalogParameters != null)
				{
					return this.CatalogParameters["Format"];
				}
				return null;
			}
			set
			{
				this.CatalogParameters["Format"] = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000C9FB File Offset: 0x0000ABFB
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000CA0D File Offset: 0x0000AC0D
		public string SessionIDParamValue
		{
			get
			{
				return this.CatalogParameters["SessionID"];
			}
			set
			{
				this.CatalogParameters["SessionID"] = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000CA20 File Offset: 0x0000AC20
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000CA32 File Offset: 0x0000AC32
		public string ImageIDParamValue
		{
			get
			{
				return this.CatalogParameters["ImageID"];
			}
			set
			{
				this.CatalogParameters["ImageID"] = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000CA45 File Offset: 0x0000AC45
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000CA57 File Offset: 0x0000AC57
		public string ReturnUrlValue
		{
			get
			{
				return this.CatalogParameters["ReturnUrl"];
			}
			set
			{
				this.CatalogParameters["ReturnUrl"] = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000CA6A File Offset: 0x0000AC6A
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		public string SortIdParamValue
		{
			get
			{
				return this.CatalogParameters["SortId"];
			}
			set
			{
				this.CatalogParameters["SortId"] = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000CA8F File Offset: 0x0000AC8F
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000CAA1 File Offset: 0x0000ACA1
		public string ShowHideToggleParamValue
		{
			get
			{
				return this.CatalogParameters["ShowHideToggle"];
			}
			set
			{
				this.CatalogParameters["ShowHideToggle"] = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000CAC6 File Offset: 0x0000ACC6
		public string SnapshotParamValue
		{
			get
			{
				return this.CatalogParameters["Snapshot"];
			}
			set
			{
				this.CatalogParameters["Snapshot"] = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000CAD9 File Offset: 0x0000ACD9
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000CAEB File Offset: 0x0000ACEB
		public string ClearSessionParamValue
		{
			get
			{
				return this.CatalogParameters["ClearSession"];
			}
			set
			{
				this.CatalogParameters["ClearSession"] = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000CAFE File Offset: 0x0000ACFE
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000CB10 File Offset: 0x0000AD10
		public string AllowNewSessionsParamValue
		{
			get
			{
				return this.CatalogParameters["AllowNewSessions"];
			}
			set
			{
				this.CatalogParameters["AllowNewSessions"] = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000CB23 File Offset: 0x0000AD23
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000CB35 File Offset: 0x0000AD35
		public string CommandParamValue
		{
			get
			{
				return this.CatalogParameters["Command"];
			}
			set
			{
				this.CatalogParameters["Command"] = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public string EntityIdValue
		{
			get
			{
				return this.CatalogParameters["EntityID"];
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000CB5A File Offset: 0x0000AD5A
		public string DrillTypeValue
		{
			get
			{
				return this.CatalogParameters["DrillType"];
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000CB7E File Offset: 0x0000AD7E
		public string PaginationModeValue
		{
			get
			{
				return this.CatalogParameters["PageCountMode"];
			}
			set
			{
				this.CatalogParameters["PageCountMode"] = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000CB91 File Offset: 0x0000AD91
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000CB99 File Offset: 0x0000AD99
		public DatasourceCredentialsCollection DatasourcesCred
		{
			get
			{
				return this.m_datasourcesCred;
			}
			set
			{
				this.m_datasourcesCred = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000CBA2 File Offset: 0x0000ADA2
		public NameValueCollection BrowserCapabilities
		{
			get
			{
				return this.m_browserCapabilities;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000CBAA File Offset: 0x0000ADAA
		public void SetBrowserCapabilities(NameValueCollection browserCapabilities)
		{
			this.m_browserCapabilities = browserCapabilities;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000CBB3 File Offset: 0x0000ADB3
		public static bool HasPrefix(string name, string prefix, out string unprefixedName)
		{
			unprefixedName = name;
			if (prefix != null)
			{
				if (prefix.Length != 0)
				{
					if (!name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
					unprefixedName = name.Substring(prefix.Length);
				}
				else if (name.IndexOf(':') >= 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public NameValueCollection GetAllParameters()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (this.m_catalogParameters != null)
			{
				foreach (object obj in this.m_catalogParameters)
				{
					string text = (string)obj;
					nameValueCollection["rs:" + text] = this.m_catalogParameters[text];
				}
			}
			if (this.m_reportParameters != null)
			{
				foreach (object obj2 in this.m_reportParameters)
				{
					string text2 = (string)obj2;
					nameValueCollection[text2] = this.m_reportParameters[text2];
				}
			}
			if (this.m_renderingParameters != null)
			{
				foreach (object obj3 in this.m_renderingParameters)
				{
					string text3 = (string)obj3;
					nameValueCollection["rc:" + text3] = this.m_renderingParameters[text3];
				}
			}
			return nameValueCollection;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000CD38 File Offset: 0x0000AF38
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000CD40 File Offset: 0x0000AF40
		public string ServerVirtualRoot
		{
			get
			{
				return this.m_ServerVirtualRoot;
			}
			set
			{
				this.m_ServerVirtualRoot = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000CD49 File Offset: 0x0000AF49
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000CD51 File Offset: 0x0000AF51
		public string SessionId
		{
			get
			{
				return this.m_SessionId;
			}
			set
			{
				this.m_SessionId = value;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		private static void ResolveServerParameters(IParametersTranslator paramsTranslator, NameValueCollection allParametersCollection, NameValueCollection rsParameters, NameValueCollection rcParameters, NameValueCollection dsuParameters, NameValueCollection dspParameters, NameValueCollection reportParameters, out Hashtable reverseLookup, out ExternalItemPath itemPath)
		{
			reverseLookup = new Hashtable();
			itemPath = null;
			StringCollection stringCollection = new StringCollection();
			for (int i = 0; i < allParametersCollection.Count; i++)
			{
				string key = allParametersCollection.GetKey(i);
				if (key != null && StringComparer.OrdinalIgnoreCase.Compare(key, "rs:StoredParametersID") == 0)
				{
					string text = allParametersCollection[i];
					NameValueCollection nameValueCollection;
					paramsTranslator.GetParamsInstance(text, out itemPath, out nameValueCollection);
					if (nameValueCollection == null)
					{
						throw new StoredParameterNotFoundException(text);
					}
					reverseLookup.Add(new ReportParameterCollection(nameValueCollection), text);
					stringCollection.Add(key);
					foreach (object obj in nameValueCollection)
					{
						string text2 = (string)obj;
						string[] values = nameValueCollection.GetValues(text2);
						if (!RSRequestParameters.TryToAddToCollection(text2, values, "rs:", false, rsParameters) && !RSRequestParameters.TryToAddToCollection(text2, values, "rc:", false, rcParameters) && !RSRequestParameters.TryToAddToCollection(text2, values, "dsu:", false, dsuParameters) && !RSRequestParameters.TryToAddToCollection(text2, values, "dsp:", false, dspParameters))
						{
							RSRequestParameters.TryToAddToCollection(text2, values, "", true, reportParameters);
						}
					}
				}
			}
			foreach (string text3 in stringCollection)
			{
				allParametersCollection.Remove(text3);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		private static void ParseQueryString(ExternalItemPath itemPath, IParametersTranslator paramsTranslator, NameValueCollection allParametersCollection, out NameValueCollection rsParameters, out NameValueCollection rcParameters, out NameValueCollection reportParameters, out DatasourceCredentialsCollection dsParameters, out Hashtable reverseLookup)
		{
			dsParameters = null;
			reverseLookup = null;
			rsParameters = new NameValueCollection();
			rcParameters = new NameValueCollection();
			reportParameters = new NameValueCollection();
			NameValueCollection nameValueCollection = new NameValueCollection();
			NameValueCollection nameValueCollection2 = new NameValueCollection();
			ExternalItemPath externalItemPath = null;
			if (allParametersCollection == null)
			{
				return;
			}
			RSRequestParameters.ResolveServerParameters(paramsTranslator, allParametersCollection, rsParameters, rcParameters, nameValueCollection, nameValueCollection2, reportParameters, out reverseLookup, out externalItemPath);
			if (externalItemPath != null && Localization.CatalogCultureCompare(itemPath.Value, externalItemPath.Value) != 0)
			{
				rsParameters = new NameValueCollection();
				rcParameters = new NameValueCollection();
				nameValueCollection = new NameValueCollection();
				nameValueCollection2 = new NameValueCollection();
				reportParameters = new NameValueCollection();
				reverseLookup = null;
				if (RSTrace.CatalogTrace.TraceInfo)
				{
					string text = string.Format(CultureInfo.InvariantCulture, "Requested item path '{0}' doesn't match stored parameters path '{1}'.", itemPath.Value, externalItemPath.Value);
					RSTrace.CatalogTrace.Trace(TraceLevel.Info, text);
				}
			}
			for (int i = 0; i < allParametersCollection.Count; i++)
			{
				string text2 = allParametersCollection.GetKey(i);
				string[] array = allParametersCollection.GetValues(i);
				if (array != null && text2 != null)
				{
					if (StringSupport.EndsWith(text2, ":isnull", true, CultureInfo.InvariantCulture))
					{
						text2 = text2.Substring(0, text2.Length - ":isnull".Length);
						array = new string[1];
					}
					if (!RSRequestParameters.TryToAddToCollection(text2, array, "rs:", false, rsParameters) && !RSRequestParameters.TryToAddToCollection(text2, array, "rc:", false, rcParameters) && !RSRequestParameters.TryToAddToCollection(text2, array, "dsu:", false, nameValueCollection) && !RSRequestParameters.TryToAddToCollection(text2, array, "dsp:", false, nameValueCollection2))
					{
						RSRequestParameters.TryToAddToCollection(text2, array, "", true, reportParameters);
					}
				}
			}
			dsParameters = new DatasourceCredentialsCollection(nameValueCollection, nameValueCollection2);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000D088 File Offset: 0x0000B288
		private static bool TryToAddToCollection(string name, string[] val, string prefix, bool allowMultiValue, NameValueCollection collection)
		{
			string text;
			if (!RSRequestParameters.HasPrefix(name, prefix, out text))
			{
				return false;
			}
			if (allowMultiValue)
			{
				foreach (string text2 in val)
				{
					collection.Add(text, text2);
				}
				return true;
			}
			if (val.Length > 1)
			{
				return true;
			}
			if (collection.GetValues(text) == null)
			{
				collection[text] = val[0];
				return true;
			}
			if (val[0] == null)
			{
				collection[text] = null;
				return true;
			}
			return true;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		private static void GuessFormatIfNotPresent(NameValueCollection catalogParameters)
		{
			if (catalogParameters["Format"] == null)
			{
				catalogParameters["Format"] = RSRequestParameters.GetFallbackFormat();
			}
		}

		// Token: 0x04000200 RID: 512
		public const string ReportParameterPrefix = "";

		// Token: 0x04000201 RID: 513
		public const string CatalogParameterPrefix = "rs:";

		// Token: 0x04000202 RID: 514
		public const string RenderingParameterPrefix = "rc:";

		// Token: 0x04000203 RID: 515
		public const string UserNameParameterPrefix = "dsu:";

		// Token: 0x04000204 RID: 516
		public const string PasswordParameterPrefix = "dsp:";

		// Token: 0x04000205 RID: 517
		public const string ParameterNullSuffix = ":isnull";

		// Token: 0x04000206 RID: 518
		public const string PowerViewParameterPrefix = "pv:";

		// Token: 0x04000207 RID: 519
		public const char PrefixTerminatorChar = ':';

		// Token: 0x04000208 RID: 520
		public const string FormatParamName = "Format";

		// Token: 0x04000209 RID: 521
		public const string EncodingParamName = "Encoding";

		// Token: 0x0400020A RID: 522
		public const string FullFeatureFormat = "HTML5";

		// Token: 0x0400020B RID: 523
		public const string OWCFormat = "HTMLOWC";

		// Token: 0x0400020C RID: 524
		public const string StreamRoot = "StreamRoot";

		// Token: 0x0400020D RID: 525
		public const string PrimaryVersion = "2015-02";

		// Token: 0x0400020E RID: 526
		public const string Version201411 = "2014-11";

		// Token: 0x0400020F RID: 527
		public const string Version201409iOSDev = "2014-09-iOSDev";

		// Token: 0x04000210 RID: 528
		public const string Version201409iOS = "2014-09-iOS";

		// Token: 0x04000211 RID: 529
		public const string Version201403 = "2014-03";

		// Token: 0x04000212 RID: 530
		public const string iOSDevVersion = "2014-09-iOSDev";

		// Token: 0x04000213 RID: 531
		public const string iOSVersion = "2014-09-iOS";

		// Token: 0x04000214 RID: 532
		public const string UpgradeVersion = "2014-11";

		// Token: 0x04000215 RID: 533
		public const string HelixVersion = "2014-03";

		// Token: 0x04000216 RID: 534
		public const string ShowHideToggleParamName = "ShowHideToggle";

		// Token: 0x04000217 RID: 535
		public const string SortIdParamName = "SortId";

		// Token: 0x04000218 RID: 536
		public const string ClearSortParamName = "ClearSort";

		// Token: 0x04000219 RID: 537
		public const string SortDirectionParamName = "SortDirection";

		// Token: 0x0400021A RID: 538
		public const string AllowNewSessionsParamName = "AllowNewSessions";

		// Token: 0x0400021B RID: 539
		public const string ResetSessionParamName = "ResetSession";

		// Token: 0x0400021C RID: 540
		public const string CommandParamName = "Command";

		// Token: 0x0400021D RID: 541
		public const string SessionIDParamName = "SessionID";

		// Token: 0x0400021E RID: 542
		public const string PowerViewSessionId = "ocp-sqlrs-session-id";

		// Token: 0x0400021F RID: 543
		public const string PowerViewRoutingToken = "ocp-sqlrs-rtoken";

		// Token: 0x04000220 RID: 544
		public const string ServiceApiVersion = "api-version";

		// Token: 0x04000221 RID: 545
		public const string ResponseApiVersion = "accept-api-version";

		// Token: 0x04000222 RID: 546
		public const string ResponseGroupVersion = "accept-api-version-group";

		// Token: 0x04000223 RID: 547
		public const string ReportId = "ReportId";

		// Token: 0x04000224 RID: 548
		public const string TileId = "TileId";

		// Token: 0x04000225 RID: 549
		public const string InstantiationMode = "InstantiationMode";

		// Token: 0x04000226 RID: 550
		public const string DashboardId = "DashboardId";

		// Token: 0x04000227 RID: 551
		public const string SaveType = "SaveType";

		// Token: 0x04000228 RID: 552
		public const string ReportName = "ReportName";

		// Token: 0x04000229 RID: 553
		public const string ImageIDParamName = "ImageID";

		// Token: 0x0400022A RID: 554
		public const string SnapshotParamName = "Snapshot";

		// Token: 0x0400022B RID: 555
		public const string ClearSessionParamName = "ClearSession";

		// Token: 0x0400022C RID: 556
		public const string ErrorResponseAsXml = "ErrorResponseAsXml";

		// Token: 0x0400022D RID: 557
		public const string StoredParametersID = "StoredParametersID";

		// Token: 0x0400022E RID: 558
		public const string ProgressiveSessionId = "ProgressiveSessionId";

		// Token: 0x0400022F RID: 559
		public const string ParamLanguage = "ParameterLanguage";

		// Token: 0x04000230 RID: 560
		public const string ReturnUrlParamName = "ReturnUrl";

		// Token: 0x04000231 RID: 561
		public const string RendererAccessCommand = "Get";

		// Token: 0x04000232 RID: 562
		public const string RunReportCommand = "Render";

		// Token: 0x04000233 RID: 563
		public const string ListChildrenCommand = "ListChildren";

		// Token: 0x04000234 RID: 564
		public const string GetResourceContentsCommand = "GetResourceContents";

		// Token: 0x04000235 RID: 565
		public const string GetDataSourceContentsCommand = "GetDataSourceContents";

		// Token: 0x04000236 RID: 566
		public const string GetModelDefinitionCommand = "GetModelDefinition";

		// Token: 0x04000237 RID: 567
		public const string DrillthroughCommand = "Drillthrough";

		// Token: 0x04000238 RID: 568
		public const string ExecuteQueryCommand = "ExecuteQuery";

		// Token: 0x04000239 RID: 569
		public const string BlankCommand = "Blank";

		// Token: 0x0400023A RID: 570
		public const string SortCommand = "Sort";

		// Token: 0x0400023B RID: 571
		public const string StyleSheet = "StyleSheet";

		// Token: 0x0400023C RID: 572
		public const string StyleSheetImage = "StyleSheetImage";

		// Token: 0x0400023D RID: 573
		public const string GetComponentDefinitionCommand = "GetComponentDefinition";

		// Token: 0x0400023E RID: 574
		public const string GetDataSetDefinitionCommand = "GetDataSetDefinition";

		// Token: 0x0400023F RID: 575
		public const string Ascending = "Ascending";

		// Token: 0x04000240 RID: 576
		public const string Descending = "Descending";

		// Token: 0x04000241 RID: 577
		public const string DBUserParamName = "DBUser";

		// Token: 0x04000242 RID: 578
		public const string DBPasswordParamName = "DBPassword";

		// Token: 0x04000243 RID: 579
		public const string PersistStreams = "PersistStreams";

		// Token: 0x04000244 RID: 580
		public const string GetNextStream = "GetNextStream";

		// Token: 0x04000245 RID: 581
		public const string EntityID = "EntityID";

		// Token: 0x04000246 RID: 582
		public const string DrillType = "DrillType";

		// Token: 0x04000247 RID: 583
		public const string DataSourceName = "DataSourceName";

		// Token: 0x04000248 RID: 584
		public const string CommandText = "CommandText";

		// Token: 0x04000249 RID: 585
		public const string Timeout = "Timeout";

		// Token: 0x0400024A RID: 586
		public const string GetUserModel = "GetUserModel";

		// Token: 0x0400024B RID: 587
		public const string PerspectiveID = "PerspectiveID";

		// Token: 0x0400024C RID: 588
		public const string OmitModelDefinitions = "OmitModelDefinitions";

		// Token: 0x0400024D RID: 589
		public const string ModelMetadataVersion = "ModelMetadataVersion";

		// Token: 0x0400024E RID: 590
		public const string ItemPath = "ItemPath";

		// Token: 0x0400024F RID: 591
		public const string SourceReportUri = "SourceReportUri";

		// Token: 0x04000250 RID: 592
		public const string ReturnRawDataParamName = "ReturnRawData";

		// Token: 0x04000251 RID: 593
		public const string StyleSheetName = "Name";

		// Token: 0x04000252 RID: 594
		public const string StyleSheetImageName = "Name";

		// Token: 0x04000253 RID: 595
		public const string PaginationMode = "PageCountMode";

		// Token: 0x04000254 RID: 596
		public const string ActualPageMode = "Actual";

		// Token: 0x04000255 RID: 597
		public const string EstimatePageMode = "Estimate";

		// Token: 0x04000256 RID: 598
		internal const string IsCancellable = "IsCancellable";

		// Token: 0x04000257 RID: 599
		public const string RenderEditCommand = "RenderEdit";

		// Token: 0x04000258 RID: 600
		public const string GetModelCommand = "GetModel";

		// Token: 0x04000259 RID: 601
		public const string GetReportAndModelsCommand = "GetReportAndModels";

		// Token: 0x0400025A RID: 602
		public const string GetExternalImagesCommand = "GetExternalImages";

		// Token: 0x0400025B RID: 603
		public const string ExecuteQueriesCommand = "ExecuteQueries";

		// Token: 0x0400025C RID: 604
		public const string LogClientTraceEventsCommand = "LogClientTraceEvents";

		// Token: 0x0400025D RID: 605
		public const string CloseSessionCommand = "CloseSession";

		// Token: 0x0400025E RID: 606
		public const string CancelProgressiveSessionJobsCommand = "CancelProgressiveSessionJobs";

		// Token: 0x0400025F RID: 607
		public const string PowerViewCloseSession = "CloseSession";

		// Token: 0x04000260 RID: 608
		public const string PowerViewOpenSession = "OpenSession";

		// Token: 0x04000261 RID: 609
		public const string PowerViewLoadReport = "LoadReport";

		// Token: 0x04000262 RID: 610
		public const string PowerViewLogClientActivities = "LogClientActivities";

		// Token: 0x04000263 RID: 611
		public const string PowerViewLogClientTraces = "LogClientTraces";

		// Token: 0x04000264 RID: 612
		public const string LoadDocument = "LoadDocument";

		// Token: 0x04000265 RID: 613
		public const string SaveReport = "SaveReport";

		// Token: 0x04000266 RID: 614
		public const string ExecuteCommands = "ExecuteCommands";

		// Token: 0x04000267 RID: 615
		public const string ExecuteSemanticQuery = "ExecuteSemanticQuery";

		// Token: 0x04000268 RID: 616
		public const string GetDocument = "GetDocument";

		// Token: 0x04000269 RID: 617
		public const string GetEntityDataModel = "GetEntityDataModel";

		// Token: 0x0400026A RID: 618
		public const string GetVisual = "GetVisual";

		// Token: 0x0400026B RID: 619
		public const string GetSemanticQuery = "GetSemanticQuery";

		// Token: 0x0400026C RID: 620
		public const string Height = "Height";

		// Token: 0x0400026D RID: 621
		public const string Width = "Width";

		// Token: 0x0400026E RID: 622
		public const string VisualName = "VisualName";

		// Token: 0x0400026F RID: 623
		public const string SheetName = "SheetName";

		// Token: 0x04000270 RID: 624
		public const string IsNew = "IsNew";

		// Token: 0x04000271 RID: 625
		public const string SheetReportSectionMapping = "SheetReportSectionMapping";

		// Token: 0x04000272 RID: 626
		public const string ReportContentType = "ReportContentType";

		// Token: 0x04000273 RID: 627
		public const string Mode = "Mode";

		// Token: 0x04000274 RID: 628
		public const string ModelId = "ModelId";

		// Token: 0x04000275 RID: 629
		public const string IsCloudRlsEnabled = "IsCloudRlsEnabled";

		// Token: 0x04000276 RID: 630
		public const string UserPrincipalName = "UserPrincipalName";

		// Token: 0x04000277 RID: 631
		public const string IsCloudRoleLevelSecurityMembershipEnabled = "IsCloudRoleLevelSecurityMembershipEnabled";

		// Token: 0x04000278 RID: 632
		public const string ImpersonatedUserPrincipalName = "ImpersonatedUserPrincipalName";

		// Token: 0x04000279 RID: 633
		public const string ImpersonatedRoles = "ImpersonatedRoles";

		// Token: 0x0400027A RID: 634
		public const string TenantId = "TenantId";

		// Token: 0x0400027B RID: 635
		public const string ExecuteDaxQuery = "ExecuteDaxQuery";

		// Token: 0x0400027C RID: 636
		public static string PBIDeviceInfoStringFormat = "<DeviceInfo>\r\n                        <UseFullUrls>True</UseFullUrls>\r\n                        <PageHeight>3.125in</PageHeight>\r\n                        <PageWidth>5.3125in</PageWidth>\r\n                        <ActiveXControls>False</ActiveXControls>\r\n                        <OutputFormat>PNG</OutputFormat>                   \r\n                        <ReportItemPath>{0}</ReportItemPath>\r\n                    </DeviceInfo>";

		// Token: 0x0400027D RID: 637
		private static readonly Dictionary<string, bool> m_crescentCommands = new Dictionary<string, bool>(8);

		// Token: 0x0400027E RID: 638
		private static readonly Dictionary<string, RSRequestParameters.HttpMethod> m_powerViewCommands = new Dictionary<string, RSRequestParameters.HttpMethod>(6);

		// Token: 0x0400027F RID: 639
		private static readonly List<string> m_powerViewServerVrmCommands = new List<string>();

		// Token: 0x04000280 RID: 640
		private Hashtable m_reverseLookupParameters;

		// Token: 0x04000281 RID: 641
		protected NameValueCollection m_renderingParameters;

		// Token: 0x04000282 RID: 642
		private NameValueCollection m_reportParameters;

		// Token: 0x04000283 RID: 643
		private NameValueCollection m_catalogParameters;

		// Token: 0x04000284 RID: 644
		private DatasourceCredentialsCollection m_datasourcesCred;

		// Token: 0x04000285 RID: 645
		private NameValueCollection m_browserCapabilities;

		// Token: 0x04000286 RID: 646
		private string m_ServerVirtualRoot;

		// Token: 0x04000287 RID: 647
		private string m_SessionId;

		// Token: 0x04000288 RID: 648
		private const string ParametersXmlElement = "Parameters";

		// Token: 0x04000289 RID: 649
		private const string ParameterXmlElement = "Parameter";

		// Token: 0x0400028A RID: 650
		private const string NameXmlElement = "Name";

		// Token: 0x0400028B RID: 651
		private const string ValueXmlElement = "Value";

		// Token: 0x0400028C RID: 652
		private const string BrowserCapabilitiesXmlElement = "BrowserCapabilities";

		// Token: 0x0400028D RID: 653
		private const string DeviceInfoXmlElement = "DeviceInfo";

		// Token: 0x020000F2 RID: 242
		private enum HttpMethod
		{
			// Token: 0x040004BF RID: 1215
			GET,
			// Token: 0x040004C0 RID: 1216
			POST
		}

		// Token: 0x020000F3 RID: 243
		public enum CacheDeviceInfoTags
		{
			// Token: 0x040004C2 RID: 1218
			Parameters,
			// Token: 0x040004C3 RID: 1219
			ReplacementRoot,
			// Token: 0x040004C4 RID: 1220
			StreamRoot
		}
	}
}
