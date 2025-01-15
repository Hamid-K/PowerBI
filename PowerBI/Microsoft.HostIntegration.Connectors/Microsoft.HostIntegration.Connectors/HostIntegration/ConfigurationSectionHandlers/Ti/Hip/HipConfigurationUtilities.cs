using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Xml;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000564 RID: 1380
	public class HipConfigurationUtilities
	{
		// Token: 0x06002E8D RID: 11917 RVA: 0x0009B4D8 File Offset: 0x000996D8
		public static string ServiceToXml(ref HipConfigurationSectionHandler hipConfig, string serviceName)
		{
			string text = null;
			foreach (object obj in hipConfig.Services)
			{
				Service service = (Service)obj;
				if (service.ServiceName == serviceName)
				{
					text = HipConfigurationUtilities.GetServiceXml(service, HipGeneratedSchemaType.Full);
					switch (service.ProgrammingModel)
					{
					case ProgrammingModel.ElmLink:
						text += HipConfigurationUtilities.GetElmLinkServiceXml(service, HipGeneratedSchemaType.Full);
						break;
					case ProgrammingModel.ElmUserData:
						text += HipConfigurationUtilities.GetElmUserDataServiceXml(service, HipGeneratedSchemaType.Full);
						break;
					case ProgrammingModel.SnaLink:
						text += HipConfigurationUtilities.GetSnaLinkServiceXml(service, HipGeneratedSchemaType.Full);
						break;
					case ProgrammingModel.SnaEndpoint:
						text += HipConfigurationUtilities.GetSnaEndpointServiceXml(service, HipGeneratedSchemaType.Full);
						break;
					case ProgrammingModel.SnaUserData:
						text += HipConfigurationUtilities.GetSnaUserDataServiceXml(service, HipGeneratedSchemaType.Full);
						break;
					case ProgrammingModel.TrmLink:
						text += HipConfigurationUtilities.GetTrmLinkServiceXml(service, HipGeneratedSchemaType.Full);
						break;
					case ProgrammingModel.Http:
						text += HipConfigurationUtilities.GetHttpServiceXml(service, HipGeneratedSchemaType.Full);
						break;
					}
					text += "</service>";
					break;
				}
			}
			return text;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x0009B5F8 File Offset: 0x000997F8
		public static string AddServiceFromXml(ref HipConfigurationSectionHandler hipConfig, string serviceXml)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(serviceXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNode = documentElement.SelectSingleNode("/service");
			if (xmlNode == null)
			{
				throw new Exception("XML does not contain a service node and is therefore an invalid HIP Service definition");
			}
			string text;
			try
			{
				XmlNode xmlNode2 = documentElement.SelectSingleNode("/service/http");
				if (xmlNode2 != null)
				{
					text = HipConfigurationUtilities.MakeHttpService(xmlDocument, xmlNode, xmlNode2, ref hipConfig);
				}
				else
				{
					xmlNode2 = xmlDocument.SelectSingleNode("/service/elmLink");
					if (xmlNode2 != null)
					{
						text = HipConfigurationUtilities.MakeElmLinkService(xmlDocument, xmlNode, xmlNode2, ref hipConfig);
					}
					else
					{
						xmlNode2 = xmlDocument.SelectSingleNode("/service/elmUserData");
						if (xmlNode2 != null)
						{
							text = HipConfigurationUtilities.MakeElmUserDataService(xmlDocument, xmlNode, xmlNode2, ref hipConfig);
						}
						else
						{
							xmlNode2 = xmlDocument.SelectSingleNode("/service/trmLink");
							if (xmlNode2 != null)
							{
								text = HipConfigurationUtilities.MakeTrmLinkService(xmlDocument, xmlNode, xmlNode2, ref hipConfig);
							}
							else
							{
								xmlNode2 = xmlDocument.SelectSingleNode("/service/snaLink");
								if (xmlNode2 != null)
								{
									text = HipConfigurationUtilities.MakeSnaLinkService(xmlDocument, xmlNode, xmlNode2, ref hipConfig);
								}
								else
								{
									xmlNode2 = xmlDocument.SelectSingleNode("/service/snaUserData");
									if (xmlNode2 != null)
									{
										text = HipConfigurationUtilities.MakeSnaUserDataService(xmlDocument, xmlNode, xmlNode2, ref hipConfig);
									}
									else
									{
										xmlNode2 = xmlDocument.SelectSingleNode("/service/snaEndpoint");
										if (xmlNode2 == null)
										{
											throw new Exception("HIP Service type not recognized: " + documentElement.ChildNodes[0].InnerXml);
										}
										text = HipConfigurationUtilities.MakeSnaEnpointService(xmlDocument, xmlNode, xmlNode2, ref hipConfig);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return text;
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x0009B798 File Offset: 0x00099998
		private static void GetServiceAttributes(XmlNode serviceNode, ref HipConfigurationSectionHandler hipConfig, out string serviceName, out string assemblyPath)
		{
			serviceName = null;
			assemblyPath = null;
			foreach (object obj in serviceNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				if (name != null)
				{
					if (!(name == "serviceName"))
					{
						if (name == "assemblyPath")
						{
							assemblyPath = xmlAttribute.Value;
						}
					}
					else
					{
						serviceName = xmlAttribute.Value;
					}
				}
			}
			using (IEnumerator enumerator = hipConfig.Services.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((Service)enumerator.Current).ServiceName == serviceName)
					{
						serviceName = "Copy of " + serviceName;
						break;
					}
				}
			}
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x0009B888 File Offset: 0x00099A88
		private static string MakeHttpService(XmlDocument doc, XmlNode serviceNode, XmlNode httpServiceNode, ref HipConfigurationSectionHandler hipConfig)
		{
			string text = null;
			string text2 = null;
			HipConfigurationUtilities.GetServiceAttributes(serviceNode, ref hipConfig, out text, out text2);
			Service service = new Service();
			service.ServiceName = text;
			service.AssemblyPath = text2;
			Http http = new Http();
			http.Hosts = doc.SelectSingleNode("/service/http/@hosts").Value;
			HttpEndpointCollection httpEndpointCollection = new HttpEndpointCollection();
			foreach (object obj in doc.SelectSingleNode("/service/http/endpoints"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				HttpEndpoint httpEndpoint = new HttpEndpoint();
				foreach (object obj2 in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj2;
					string text3 = xmlAttribute.Name;
					if (text3 != null)
					{
						if (!(text3 == "webSite"))
						{
							if (!(text3 == "port"))
							{
								if (text3 == "useSsl")
								{
									httpEndpoint.UseSsl = Convert.ToBoolean(xmlAttribute.Value);
								}
							}
							else
							{
								httpEndpoint.Port = (int)Convert.ToInt16(xmlAttribute.Value);
							}
						}
						else
						{
							httpEndpoint.WebSite = xmlAttribute.Value;
						}
					}
				}
				httpEndpointCollection.AddEndpoint(httpEndpoint);
			}
			ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
			foreach (object obj3 in doc.SelectSingleNode("/service/http/resolutionEntries"))
			{
				XmlNode xmlNode2 = (XmlNode)obj3;
				ResolutionEntry resolutionEntry = new ResolutionEntry();
				foreach (object obj4 in xmlNode2.Attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj4;
					string text3 = xmlAttribute2.Name;
					if (text3 != null)
					{
						if (!(text3 == "prefixProgramId"))
						{
							if (!(text3 == "interfaceName"))
							{
								if (!(text3 == "method"))
								{
									if (text3 == "essoSecurityPolicyName")
									{
										resolutionEntry.EssoSecurityPolicyName = xmlAttribute2.Value;
									}
								}
								else
								{
									resolutionEntry.Method = xmlAttribute2.Value;
								}
							}
							else
							{
								resolutionEntry.InterfaceName = xmlAttribute2.Value;
							}
						}
						else
						{
							resolutionEntry.PrefixProgramId = xmlAttribute2.Value;
						}
					}
				}
				resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
			}
			http.IsTypeDefined = true;
			http.Endpoints = httpEndpointCollection;
			http.ResolutionEntries = resolutionEntryCollection;
			service.Http = http;
			hipConfig.Services.AddService(service);
			return service.ServiceName;
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x0009BB24 File Offset: 0x00099D24
		private static string MakeElmLinkService(XmlDocument doc, XmlNode serviceNode, XmlNode elmLinkServiceNode, ref HipConfigurationSectionHandler hipConfig)
		{
			string text = null;
			string text2 = null;
			HipConfigurationUtilities.GetServiceAttributes(serviceNode, ref hipConfig, out text, out text2);
			Service service = new Service();
			service.ServiceName = text;
			service.AssemblyPath = text2;
			ElmLink elmLink = new ElmLink();
			foreach (object obj in elmLinkServiceNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text3 = xmlAttribute.Name;
				if (text3 != null)
				{
					if (!(text3 == "ports"))
					{
						if (!(text3 == "hosts"))
						{
							if (text3 == "requestHeaderFormat")
							{
								elmLink.RequestHeaderFormat = xmlAttribute.Value;
							}
						}
						else
						{
							elmLink.Hosts = xmlAttribute.Value;
						}
					}
					else
					{
						elmLink.Ports = xmlAttribute.Value;
					}
				}
			}
			ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
			foreach (object obj2 in doc.SelectSingleNode("/service/elmLink/resolutionEntries"))
			{
				XmlNode xmlNode = (XmlNode)obj2;
				ResolutionEntry resolutionEntry = new ResolutionEntry();
				foreach (object obj3 in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj3;
					string text3 = xmlAttribute2.Name;
					if (text3 != null)
					{
						if (!(text3 == "linkToProgram"))
						{
							if (!(text3 == "interfaceName"))
							{
								if (!(text3 == "method"))
								{
									if (text3 == "essoSecurityPolicyName")
									{
										resolutionEntry.EssoSecurityPolicyName = xmlAttribute2.Value;
									}
								}
								else
								{
									resolutionEntry.Method = xmlAttribute2.Value;
								}
							}
							else
							{
								resolutionEntry.InterfaceName = xmlAttribute2.Value;
							}
						}
						else
						{
							resolutionEntry.LinkToProgram = xmlAttribute2.Value;
						}
					}
				}
				resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
			}
			elmLink.IsTypeDefined = true;
			elmLink.ResolutionEntries = resolutionEntryCollection;
			service.ElmLink = elmLink;
			hipConfig.Services.AddService(service);
			return service.ServiceName;
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x0009BD50 File Offset: 0x00099F50
		private static string MakeElmUserDataService(XmlDocument doc, XmlNode serviceNode, XmlNode elmUserDataServiceNode, ref HipConfigurationSectionHandler hipConfig)
		{
			string text = null;
			string text2 = null;
			HipConfigurationUtilities.GetServiceAttributes(serviceNode, ref hipConfig, out text, out text2);
			Service service = new Service();
			service.ServiceName = text;
			service.AssemblyPath = text2;
			ElmUserData elmUserData = new ElmUserData();
			foreach (object obj in elmUserDataServiceNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text3 = xmlAttribute.Name;
				if (text3 != null)
				{
					if (!(text3 == "ports"))
					{
						if (!(text3 == "hosts"))
						{
							if (text3 == "requestHeaderFormat")
							{
								elmUserData.RequestHeaderFormat = xmlAttribute.Value;
							}
						}
						else
						{
							elmUserData.Hosts = xmlAttribute.Value;
						}
					}
					else
					{
						elmUserData.Ports = xmlAttribute.Value;
					}
				}
			}
			ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
			foreach (object obj2 in doc.SelectSingleNode("/service/elmUserData/resolutionEntries"))
			{
				XmlNode xmlNode = (XmlNode)obj2;
				ResolutionEntry resolutionEntry = new ResolutionEntry();
				foreach (object obj3 in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj3;
					string text3 = xmlAttribute2.Name;
					if (text3 != null)
					{
						if (!(text3 == "data"))
						{
							if (!(text3 == "position"))
							{
								if (!(text3 == "interfaceName"))
								{
									if (!(text3 == "method"))
									{
										if (text3 == "essoSecurityPolicyName")
										{
											resolutionEntry.EssoSecurityPolicyName = xmlAttribute2.Value;
										}
									}
									else
									{
										resolutionEntry.Method = xmlAttribute2.Value;
									}
								}
								else
								{
									resolutionEntry.InterfaceName = xmlAttribute2.Value;
								}
							}
							else
							{
								resolutionEntry.Position = Convert.ToInt32(xmlAttribute2.Value);
							}
						}
						else
						{
							resolutionEntry.Data = xmlAttribute2.Value;
						}
					}
				}
				resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
			}
			elmUserData.IsTypeDefined = true;
			elmUserData.ResolutionEntries = resolutionEntryCollection;
			service.ElmUserData = elmUserData;
			hipConfig.Services.AddService(service);
			return service.ServiceName;
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x0009BFA0 File Offset: 0x0009A1A0
		private static string MakeTrmLinkService(XmlDocument doc, XmlNode serviceNode, XmlNode trmLinkServiceNode, ref HipConfigurationSectionHandler hipConfig)
		{
			string text = null;
			string text2 = null;
			HipConfigurationUtilities.GetServiceAttributes(serviceNode, ref hipConfig, out text, out text2);
			Service service = new Service();
			service.ServiceName = text;
			service.AssemblyPath = text2;
			TrmLink trmLink = new TrmLink();
			foreach (object obj in trmLinkServiceNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text3 = xmlAttribute.Name;
				if (text3 != null)
				{
					if (!(text3 == "ports"))
					{
						if (!(text3 == "hosts"))
						{
							if (text3 == "requestHeaderFormat")
							{
								trmLink.RequestHeaderFormat = xmlAttribute.Value;
							}
						}
						else
						{
							trmLink.Hosts = xmlAttribute.Value;
						}
					}
					else
					{
						trmLink.Ports = xmlAttribute.Value;
					}
				}
			}
			ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
			foreach (object obj2 in doc.SelectSingleNode("/service/trmLink/resolutionEntries"))
			{
				XmlNode xmlNode = (XmlNode)obj2;
				ResolutionEntry resolutionEntry = new ResolutionEntry();
				foreach (object obj3 in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj3;
					string text3 = xmlAttribute2.Name;
					if (text3 != null)
					{
						if (!(text3 == "linkToProgram"))
						{
							if (!(text3 == "interfaceName"))
							{
								if (!(text3 == "method"))
								{
									if (text3 == "essoSecurityPolicyName")
									{
										resolutionEntry.EssoSecurityPolicyName = xmlAttribute2.Value;
									}
								}
								else
								{
									resolutionEntry.Method = xmlAttribute2.Value;
								}
							}
							else
							{
								resolutionEntry.InterfaceName = xmlAttribute2.Value;
							}
						}
						else
						{
							resolutionEntry.LinkToProgram = xmlAttribute2.Value;
						}
					}
				}
				resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
			}
			trmLink.IsTypeDefined = true;
			trmLink.ResolutionEntries = resolutionEntryCollection;
			service.TrmLink = trmLink;
			hipConfig.Services.AddService(service);
			return service.ServiceName;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x0009C1CC File Offset: 0x0009A3CC
		private static string MakeSnaLinkService(XmlDocument doc, XmlNode serviceNode, XmlNode snaLinkServiceNode, ref HipConfigurationSectionHandler hipConfig)
		{
			string text = null;
			string text2 = null;
			HipConfigurationUtilities.GetServiceAttributes(serviceNode, ref hipConfig, out text, out text2);
			Service service = new Service();
			service.ServiceName = text;
			service.AssemblyPath = text2;
			SnaLink snaLink = new SnaLink();
			foreach (object obj in snaLinkServiceNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text3 = xmlAttribute.Name;
				if (text3 != null)
				{
					if (!(text3 == "localLuName"))
					{
						if (text3 == "hosts")
						{
							snaLink.Hosts = xmlAttribute.Value;
						}
					}
					else
					{
						snaLink.LocalLuName = xmlAttribute.Value;
					}
				}
			}
			ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
			foreach (object obj2 in doc.SelectSingleNode("/service/snaLink/resolutionEntries"))
			{
				XmlNode xmlNode = (XmlNode)obj2;
				ResolutionEntry resolutionEntry = new ResolutionEntry();
				foreach (object obj3 in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj3;
					string text3 = xmlAttribute2.Name;
					if (text3 != null)
					{
						if (!(text3 == "linkToProgram"))
						{
							if (!(text3 == "interfaceName"))
							{
								if (!(text3 == "method"))
								{
									if (text3 == "essoSecurityPolicyName")
									{
										resolutionEntry.EssoSecurityPolicyName = xmlAttribute2.Value;
									}
								}
								else
								{
									resolutionEntry.Method = xmlAttribute2.Value;
								}
							}
							else
							{
								resolutionEntry.InterfaceName = xmlAttribute2.Value;
							}
						}
						else
						{
							resolutionEntry.LinkToProgram = xmlAttribute2.Value;
						}
					}
				}
				resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
			}
			snaLink.IsTypeDefined = true;
			snaLink.ResolutionEntries = resolutionEntryCollection;
			service.SnaLink = snaLink;
			hipConfig.Services.AddService(service);
			return service.ServiceName;
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x0009C3D8 File Offset: 0x0009A5D8
		private static string MakeSnaUserDataService(XmlDocument doc, XmlNode serviceNode, XmlNode snaUserDataServiceNode, ref HipConfigurationSectionHandler hipConfig)
		{
			string text = null;
			string text2 = null;
			HipConfigurationUtilities.GetServiceAttributes(serviceNode, ref hipConfig, out text, out text2);
			Service service = new Service();
			service.ServiceName = text;
			service.AssemblyPath = text2;
			SnaUserData snaUserData = new SnaUserData();
			foreach (object obj in snaUserDataServiceNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text3 = xmlAttribute.Name;
				if (text3 != null)
				{
					if (!(text3 == "localLuName"))
					{
						if (text3 == "hosts")
						{
							snaUserData.Hosts = xmlAttribute.Value;
						}
					}
					else
					{
						snaUserData.LocalLuName = xmlAttribute.Value;
					}
				}
			}
			ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
			foreach (object obj2 in doc.SelectSingleNode("/service/snaUserData/resolutionEntries"))
			{
				XmlNode xmlNode = (XmlNode)obj2;
				ResolutionEntry resolutionEntry = new ResolutionEntry();
				foreach (object obj3 in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj3;
					string text3 = xmlAttribute2.Name;
					if (text3 != null)
					{
						if (!(text3 == "data"))
						{
							if (!(text3 == "position"))
							{
								if (!(text3 == "interfaceName"))
								{
									if (!(text3 == "method"))
									{
										if (text3 == "essoSecurityPolicyName")
										{
											resolutionEntry.EssoSecurityPolicyName = xmlAttribute2.Value;
										}
									}
									else
									{
										resolutionEntry.Method = xmlAttribute2.Value;
									}
								}
								else
								{
									resolutionEntry.InterfaceName = xmlAttribute2.Value;
								}
							}
							else
							{
								resolutionEntry.Position = Convert.ToInt32(xmlAttribute2.Value);
							}
						}
						else
						{
							resolutionEntry.Data = xmlAttribute2.Value;
						}
					}
				}
				resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
			}
			snaUserData.IsTypeDefined = true;
			snaUserData.ResolutionEntries = resolutionEntryCollection;
			service.SnaUserData = snaUserData;
			hipConfig.Services.AddService(service);
			return service.ServiceName;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x0009C60C File Offset: 0x0009A80C
		private static string MakeSnaEnpointService(XmlDocument doc, XmlNode serviceNode, XmlNode snaEndpointServiceNode, ref HipConfigurationSectionHandler hipConfig)
		{
			string text = null;
			string text2 = null;
			HipConfigurationUtilities.GetServiceAttributes(serviceNode, ref hipConfig, out text, out text2);
			Service service = new Service();
			service.ServiceName = text;
			service.AssemblyPath = text2;
			SnaEndpoint snaEndpoint = new SnaEndpoint();
			foreach (object obj in snaEndpointServiceNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text3 = xmlAttribute.Name;
				if (text3 != null)
				{
					if (!(text3 == "localLuName"))
					{
						if (text3 == "hosts")
						{
							snaEndpoint.Hosts = xmlAttribute.Value;
						}
					}
					else
					{
						snaEndpoint.LocalLuName = xmlAttribute.Value;
					}
				}
			}
			ResolutionEntryCollection resolutionEntryCollection = new ResolutionEntryCollection();
			foreach (object obj2 in doc.SelectSingleNode("/service/snaEndpoint/resolutionEntries"))
			{
				XmlNode xmlNode = (XmlNode)obj2;
				ResolutionEntry resolutionEntry = new ResolutionEntry();
				foreach (object obj3 in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj3;
					string text3 = xmlAttribute2.Name;
					if (text3 != null)
					{
						if (!(text3 == "endpoint"))
						{
							if (!(text3 == "interfaceName"))
							{
								if (!(text3 == "method"))
								{
									if (text3 == "essoSecurityPolicyName")
									{
										resolutionEntry.EssoSecurityPolicyName = xmlAttribute2.Value;
									}
								}
								else
								{
									resolutionEntry.Method = xmlAttribute2.Value;
								}
							}
							else
							{
								resolutionEntry.InterfaceName = xmlAttribute2.Value;
							}
						}
						else
						{
							resolutionEntry.Endpoint = xmlAttribute2.Value;
						}
					}
				}
				resolutionEntryCollection.AddResolutionEntry(resolutionEntry);
			}
			snaEndpoint.IsTypeDefined = true;
			snaEndpoint.ResolutionEntries = resolutionEntryCollection;
			service.SnaEndpoint = snaEndpoint;
			hipConfig.Services.AddService(service);
			return service.ServiceName;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x0009C818 File Offset: 0x0009AA18
		public static string EssoSecurityPolicyToXml(ref HipConfigurationSectionHandler hipConfig, string essoSecurityPolicyName)
		{
			string text = null;
			foreach (object obj in hipConfig.EssoSecurityPolicies)
			{
				EssoSecurityPolicy essoSecurityPolicy = (EssoSecurityPolicy)obj;
				if (essoSecurityPolicy.Name == essoSecurityPolicyName)
				{
					text = HipConfigurationUtilities.GetEssoSecurityPolicyXml(essoSecurityPolicy, HipGeneratedSchemaType.Full);
				}
			}
			return text;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x0009C884 File Offset: 0x0009AA84
		public static string AddEssoSecurityPolicyFromXml(ref HipConfigurationSectionHandler hipConfig, string essoSecurityPolicyXml)
		{
			EssoSecurityPolicy essoSecurityPolicy = new EssoSecurityPolicy();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(essoSecurityPolicyXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			essoSecurityPolicy.Name = xmlDocument.SelectSingleNode("/essoSecurityPolicy/@name").Value;
			bool flag = false;
			do
			{
				flag = false;
				using (IEnumerator enumerator = hipConfig.EssoSecurityPolicies.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((EssoSecurityPolicy)enumerator.Current).Name == essoSecurityPolicy.Name)
						{
							essoSecurityPolicy.Name = "Copy of " + essoSecurityPolicy.Name;
							flag = true;
							break;
						}
					}
				}
			}
			while (flag);
			essoSecurityPolicy.DefaultCredentialsGroup = xmlDocument.SelectSingleNode("/essoSecurityPolicy/@defaultCredentialsGroup").Value;
			essoSecurityPolicy.AffiliateApplication = xmlDocument.SelectSingleNode("/essoSecurityPolicy/@affiliateApplication").Value;
			hipConfig.EssoSecurityPolicies.AddEssoSecurityPolicy(essoSecurityPolicy);
			return essoSecurityPolicy.Name;
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x0009C9A4 File Offset: 0x0009ABA4
		public static string TcpHostEnvironmentToXml(ref HipConfigurationSectionHandler hipConfig, string tcpHostEnvironmentName)
		{
			string text = null;
			foreach (object obj in hipConfig.TcpHostEnvironments)
			{
				TcpHostEnvironment tcpHostEnvironment = (TcpHostEnvironment)obj;
				if (tcpHostEnvironment.Name == tcpHostEnvironmentName)
				{
					text = HipConfigurationUtilities.GetTcpHostEnvironmentXml(tcpHostEnvironment, HipGeneratedSchemaType.Full);
				}
			}
			return text;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x0009CA10 File Offset: 0x0009AC10
		public static string AddTcpHostEnvironmentFromXml(ref HipConfigurationSectionHandler hipConfig, string tcpHostEnvironmentXml)
		{
			TcpHostEnvironment tcpHostEnvironment = new TcpHostEnvironment();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(tcpHostEnvironmentXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			tcpHostEnvironment.Name = xmlDocument.SelectSingleNode("/tcpHostEnvironment/@name").Value;
			tcpHostEnvironment.IpAddress = xmlDocument.SelectSingleNode("/tcpHostEnvironment/@ipAddress").Value;
			tcpHostEnvironment.CodePage = Convert.ToInt32(xmlDocument.SelectSingleNode("/tcpHostEnvironment/@codePage").Value);
			tcpHostEnvironment.DataConversion = (PrimitiveConverterTypes)Enum.Parse(typeof(PrimitiveConverterTypes), xmlDocument.SelectSingleNode("/tcpHostEnvironment/@dataConversion").Value);
			tcpHostEnvironment.Timeout = Convert.ToInt32(xmlDocument.SelectSingleNode("/tcpHostEnvironment/@timeout").Value);
			bool flag = false;
			do
			{
				flag = false;
				using (IEnumerator enumerator = hipConfig.TcpHostEnvironments.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((TcpHostEnvironment)enumerator.Current).Name == tcpHostEnvironment.Name)
						{
							tcpHostEnvironment.Name = "Copy of " + tcpHostEnvironment.Name;
							flag = true;
							break;
						}
					}
				}
			}
			while (flag);
			hipConfig.TcpHostEnvironments.AddTcpHostEnvironment(tcpHostEnvironment);
			return tcpHostEnvironment.Name;
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x0009CB7C File Offset: 0x0009AD7C
		public static string SnaHostEnvironmentToXml(ref HipConfigurationSectionHandler hipConfig, string snaHostEnvironmentName)
		{
			string text = null;
			foreach (object obj in hipConfig.SnaHostEnvironments)
			{
				SnaHostEnvironment snaHostEnvironment = (SnaHostEnvironment)obj;
				if (snaHostEnvironment.Name == snaHostEnvironmentName)
				{
					text = HipConfigurationUtilities.GetSnaHostEnvironmentXml(snaHostEnvironment, HipGeneratedSchemaType.Full);
				}
			}
			return text;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x0009CBE8 File Offset: 0x0009ADE8
		public static string AddSnaHostEnvironmentFromXml(ref HipConfigurationSectionHandler hipConfig, string snaHostEnvironmentXml)
		{
			SnaHostEnvironment snaHostEnvironment = new SnaHostEnvironment();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(snaHostEnvironmentXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			snaHostEnvironment.Name = xmlDocument.SelectSingleNode("/snaHostEnvironment/@name").Value;
			snaHostEnvironment.RemoteLuName = xmlDocument.SelectSingleNode("/snaHostEnvironment/@remoteLuName").Value;
			snaHostEnvironment.CodePage = Convert.ToInt32(xmlDocument.SelectSingleNode("/snaHostEnvironment/@codePage").Value);
			snaHostEnvironment.DataConversion = (PrimitiveConverterTypes)Enum.Parse(typeof(PrimitiveConverterTypes), xmlDocument.SelectSingleNode("/snaHostEnvironment/@dataConversion").Value);
			snaHostEnvironment.Timeout = Convert.ToInt32(xmlDocument.SelectSingleNode("/snaHostEnvironment/@timeout").Value);
			bool flag = false;
			do
			{
				flag = false;
				using (IEnumerator enumerator = hipConfig.SnaHostEnvironments.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((SnaHostEnvironment)enumerator.Current).Name == snaHostEnvironment.Name)
						{
							snaHostEnvironment.Name = "Copy of " + snaHostEnvironment.Name;
							flag = true;
							break;
						}
					}
				}
			}
			while (flag);
			hipConfig.SnaHostEnvironments.AddSnaHostEnvironment(snaHostEnvironment);
			return snaHostEnvironment.Name;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x0009CD54 File Offset: 0x0009AF54
		public static string ResolutionEntryToXml(string serviceName, string resolutionEntryInterfaceMethod, ref HipConfigurationSectionHandler hipConfig)
		{
			foreach (object obj in hipConfig.Services)
			{
				Service service = (Service)obj;
				if (service.ServiceName == serviceName)
				{
					ResolutionEntryCollection resolutionEntryCollection = null;
					switch (service.ProgrammingModel)
					{
					case ProgrammingModel.ElmLink:
						resolutionEntryCollection = service.ElmLink.ResolutionEntries;
						break;
					case ProgrammingModel.ElmUserData:
						resolutionEntryCollection = service.ElmUserData.ResolutionEntries;
						break;
					case ProgrammingModel.SnaLink:
						resolutionEntryCollection = service.SnaLink.ResolutionEntries;
						break;
					case ProgrammingModel.SnaEndpoint:
						resolutionEntryCollection = service.SnaEndpoint.ResolutionEntries;
						break;
					case ProgrammingModel.SnaUserData:
						resolutionEntryCollection = service.SnaUserData.ResolutionEntries;
						break;
					case ProgrammingModel.TrmLink:
						resolutionEntryCollection = service.TrmLink.ResolutionEntries;
						break;
					case ProgrammingModel.Http:
						resolutionEntryCollection = service.Http.ResolutionEntries;
						break;
					}
					foreach (object obj2 in resolutionEntryCollection)
					{
						ResolutionEntry resolutionEntry = (ResolutionEntry)obj2;
						if (resolutionEntry.InterfaceMethod == resolutionEntryInterfaceMethod)
						{
							return HipConfigurationUtilities.GetResolutionXml(resolutionEntry, HipGeneratedSchemaType.Full);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x0009CECC File Offset: 0x0009B0CC
		public static string AddResolutionEntryFromXml(string serviceName, string resolutionEntryXml, ref HipConfigurationSectionHandler hipConfig)
		{
			ResolutionEntry resolutionEntry = new ResolutionEntry();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(resolutionEntryXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			foreach (object obj in xmlDocument.SelectSingleNode("/resolutionEntry").Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				if (name != null)
				{
					uint num = <fa2731a4-b8df-42ec-a59c-4417eb13e59d><PrivateImplementationDetails>.ComputeStringHash(name);
					if (num <= 2873489200U)
					{
						if (num <= 1082161842U)
						{
							if (num != 1065514131U)
							{
								if (num == 1082161842U)
								{
									if (name == "prefixProgramId")
									{
										resolutionEntry.PrefixProgramId = xmlAttribute.Value;
									}
								}
							}
							else if (name == "interfaceName")
							{
								resolutionEntry.InterfaceName = xmlAttribute.Value;
							}
						}
						else if (num != 2471448074U)
						{
							if (num == 2873489200U)
							{
								if (name == "method")
								{
									resolutionEntry.Method = xmlAttribute.Value;
								}
							}
						}
						else if (name == "position")
						{
							resolutionEntry.Position = Convert.ToInt32(xmlAttribute.Value);
						}
					}
					else if (num <= 3631407781U)
					{
						if (num != 3359483140U)
						{
							if (num == 3631407781U)
							{
								if (name == "data")
								{
									resolutionEntry.Data = xmlAttribute.Value;
								}
							}
						}
						else if (name == "linkToProgram")
						{
							resolutionEntry.LinkToProgram = xmlAttribute.Value;
						}
					}
					else if (num != 3635933308U)
					{
						if (num == 4137520204U)
						{
							if (name == "essoSecurityPolicyName")
							{
								resolutionEntry.EssoSecurityPolicyName = xmlAttribute.Value;
							}
						}
					}
					else if (name == "endpoint")
					{
						resolutionEntry.Endpoint = xmlAttribute.Value;
					}
				}
			}
			resolutionEntry.SetHipObjectCallBack(hipConfig.GetHipObjectsDelegate);
			foreach (object obj2 in hipConfig.Services)
			{
				Service service = (Service)obj2;
				if (service.ServiceName == serviceName)
				{
					switch (service.ProgrammingModel)
					{
					case ProgrammingModel.ElmLink:
						HipConfigurationUtilities.ResolveDuplicateResolutionEntryNames(service.ElmLink.ResolutionEntries, ref resolutionEntry);
						service.ElmLink.ResolutionEntries.AddResolutionEntry(resolutionEntry);
						break;
					case ProgrammingModel.ElmUserData:
						HipConfigurationUtilities.ResolveDuplicateResolutionEntryNames(service.ElmUserData.ResolutionEntries, ref resolutionEntry);
						service.ElmUserData.ResolutionEntries.AddResolutionEntry(resolutionEntry);
						break;
					case ProgrammingModel.SnaLink:
						HipConfigurationUtilities.ResolveDuplicateResolutionEntryNames(service.SnaLink.ResolutionEntries, ref resolutionEntry);
						service.SnaLink.ResolutionEntries.AddResolutionEntry(resolutionEntry);
						break;
					case ProgrammingModel.SnaEndpoint:
						HipConfigurationUtilities.ResolveDuplicateResolutionEntryNames(service.SnaEndpoint.ResolutionEntries, ref resolutionEntry);
						service.SnaEndpoint.ResolutionEntries.AddResolutionEntry(resolutionEntry);
						break;
					case ProgrammingModel.SnaUserData:
						HipConfigurationUtilities.ResolveDuplicateResolutionEntryNames(service.SnaUserData.ResolutionEntries, ref resolutionEntry);
						service.SnaUserData.ResolutionEntries.AddResolutionEntry(resolutionEntry);
						break;
					case ProgrammingModel.TrmLink:
						HipConfigurationUtilities.ResolveDuplicateResolutionEntryNames(service.TrmLink.ResolutionEntries, ref resolutionEntry);
						service.TrmLink.ResolutionEntries.AddResolutionEntry(resolutionEntry);
						break;
					case ProgrammingModel.Http:
						HipConfigurationUtilities.ResolveDuplicateResolutionEntryNames(service.Http.ResolutionEntries, ref resolutionEntry);
						service.Http.ResolutionEntries.AddResolutionEntry(resolutionEntry);
						break;
					}
					return resolutionEntry.InterfaceMethod;
				}
			}
			throw new Exception("HIP Service name does not exist: " + serviceName);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x0009D308 File Offset: 0x0009B508
		private static void ResolveDuplicateResolutionEntryNames(ResolutionEntryCollection resolutionEntries, ref ResolutionEntry newResolutionEntry)
		{
			bool flag = false;
			do
			{
				flag = false;
				foreach (object obj in resolutionEntries)
				{
					ResolutionEntry resolutionEntry = (ResolutionEntry)obj;
					if (resolutionEntry.InterfaceName == newResolutionEntry.InterfaceName && resolutionEntry.Method == newResolutionEntry.Method)
					{
						throw new Exception("Resolution Entry already exists. " + newResolutionEntry.InterfaceName + "." + newResolutionEntry.Method);
					}
				}
			}
			while (flag);
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x0009D3A8 File Offset: 0x0009B5A8
		public static string HttpEndpointToXml(string serviceName, string httpEndpointWebSitePort, ref HipConfigurationSectionHandler hipConfig)
		{
			foreach (object obj in hipConfig.Services)
			{
				Service service = (Service)obj;
				if (service.ServiceName == serviceName)
				{
					HttpEndpointCollection httpEndpointCollection = null;
					ProgrammingModel programmingModel = service.ProgrammingModel;
					if (programmingModel > ProgrammingModel.TrmLink && programmingModel == ProgrammingModel.Http)
					{
						httpEndpointCollection = service.Http.Endpoints;
					}
					foreach (object obj2 in httpEndpointCollection)
					{
						HttpEndpoint httpEndpoint = (HttpEndpoint)obj2;
						if ((string)httpEndpoint.GetElementKey() == httpEndpointWebSitePort)
						{
							return HipConfigurationUtilities.GetHttpEndpointXml(httpEndpoint, HipGeneratedSchemaType.Full);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x0009D498 File Offset: 0x0009B698
		public static string AddHttpEndpointFromXml(string serviceName, string httpEndpointXml, ref HipConfigurationSectionHandler hipConfig, out string httpEndpointNodeName)
		{
			HttpEndpoint httpEndpoint = new HttpEndpoint();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(httpEndpointXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			foreach (object obj in xmlDocument.SelectSingleNode("/endpoint").Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				if (name != null)
				{
					if (!(name == "webSite"))
					{
						if (!(name == "port"))
						{
							if (name == "useSsl")
							{
								httpEndpoint.UseSsl = Convert.ToBoolean(xmlAttribute.Value);
							}
						}
						else
						{
							httpEndpoint.Port = Convert.ToInt32(xmlAttribute.Value);
						}
					}
					else
					{
						httpEndpoint.WebSite = xmlAttribute.Value;
					}
				}
			}
			foreach (object obj2 in hipConfig.Services)
			{
				Service service = (Service)obj2;
				if (service.ServiceName == serviceName)
				{
					ProgrammingModel programmingModel = service.ProgrammingModel;
					if (programmingModel > ProgrammingModel.TrmLink && programmingModel == ProgrammingModel.Http)
					{
						HipConfigurationUtilities.ResolveDuplicateHttpEndpointNames(service.Http.Endpoints, ref httpEndpoint);
						service.Http.Endpoints.AddEndpoint(httpEndpoint);
					}
					httpEndpointNodeName = httpEndpoint.WebSite + "." + httpEndpoint.Port.ToString();
					return (string)httpEndpoint.GetElementKey();
				}
			}
			throw new Exception("HIP Service name does not exist: " + serviceName);
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x0009D678 File Offset: 0x0009B878
		private static void ResolveDuplicateHttpEndpointNames(HttpEndpointCollection httpEndpoints, ref HttpEndpoint newHttpEndpoint)
		{
			bool flag = false;
			do
			{
				flag = false;
				using (IEnumerator enumerator = httpEndpoints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((HttpEndpoint)enumerator.Current).GetElementKey().ToString() == newHttpEndpoint.GetElementKey().ToString())
						{
							throw new Exception("HTTP Endpoint already exists. " + newHttpEndpoint.WebSite + "." + newHttpEndpoint.Port.ToString());
						}
					}
				}
			}
			while (flag);
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0009D714 File Offset: 0x0009B914
		public static string HipObjectToXml(ref HipConfigurationSectionHandler hipConfig, string hipObjectInterface)
		{
			string text = null;
			foreach (object obj in hipConfig.HipObjects)
			{
				HipObject hipObject = (HipObject)obj;
				if (hipObject.MetaDataInterface == hipObjectInterface)
				{
					text = HipConfigurationUtilities.GetHipObjectXml(hipObject, HipGeneratedSchemaType.Full);
					break;
				}
			}
			return text;
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x0009D784 File Offset: 0x0009B984
		public static string AddHipObjectFromXml(ref HipConfigurationSectionHandler hipConfig, string hipObjectXml)
		{
			HipObject hipObject = new HipObject();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(hipObjectXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			hipObject.MetaDataInterface = xmlDocument.SelectSingleNode("/object/@metaDataInterface").Value;
			hipObject.ImplementingClass = xmlDocument.SelectSingleNode("/object/@implementingClass").Value;
			hipObject.ImplementingAssembly = xmlDocument.SelectSingleNode("/object/@implementingAssembly").Value;
			hipObject.MetaDataAssembly = xmlDocument.SelectSingleNode("/object/@metaDataAssembly").Value;
			hipObject.WcfServiceUrl = xmlDocument.SelectSingleNode("/object/@wcfServiceUrl").Value;
			bool flag = false;
			do
			{
				flag = false;
				using (IEnumerator enumerator = hipConfig.HipObjects.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((HipObject)enumerator.Current).MetaDataInterface == hipObject.MetaDataInterface)
						{
							hipObject.MetaDataInterface = "Copy of " + hipObject.MetaDataInterface;
							flag = true;
							break;
						}
					}
				}
			}
			while (flag);
			hipConfig.HipObjects.AddHipObject(hipObject);
			return hipObject.MetaDataInterface;
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x0009D8D0 File Offset: 0x0009BAD0
		public static string CacheToXml(ref HipConfigurationSectionHandler hipConfig)
		{
			return HipConfigurationUtilities.GetCacheXml(hipConfig.Cache, HipGeneratedSchemaType.Full);
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x0009D8E0 File Offset: 0x0009BAE0
		public static void UpdateCacheFromXml(ref HipConfigurationSectionHandler hipConfig, string cacheXml)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(cacheXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			hipConfig.Cache.CacheName = xmlDocument.SelectSingleNode("/cache/@cacheName").Value;
			hipConfig.Cache.Key = xmlDocument.SelectSingleNode("/cache/@key").Value;
			hipConfig.Cache.Region = xmlDocument.SelectSingleNode("/cache/@region").Value;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0009D976 File Offset: 0x0009BB76
		public static string ReadOrderToXml(ref HipConfigurationSectionHandler hipConfig)
		{
			return HipConfigurationUtilities.GetReadOrderXml(hipConfig.ReadOrder, HipGeneratedSchemaType.Full);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x0009D988 File Offset: 0x0009BB88
		public static void UpdateReadOrderFromXml(ref HipConfigurationSectionHandler hipConfig, string readOrderXml)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(readOrderXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			hipConfig.ReadOrder.appConfig = xmlDocument.SelectSingleNode("/readOrder/@appConfig").Value;
			hipConfig.ReadOrder.cache = xmlDocument.SelectSingleNode("/readOrder/@cache").Value;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x0009DA02 File Offset: 0x0009BC02
		public static string ConversionBehaviorToXml(ref HipConfigurationSectionHandler hipConfig)
		{
			return HipConfigurationUtilities.GetConversionBehaviorXml(hipConfig.ConversionBehavior, HipGeneratedSchemaType.Full);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x0009DA14 File Offset: 0x0009BC14
		public static void UpdateConversionBehaviorFromXml(ref HipConfigurationSectionHandler hipConfig, string conversionBehaviorXml)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(conversionBehaviorXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			hipConfig.ConversionBehavior.AcceptAllInvalidNumerics = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@acceptAllInvalidNumerics").Value);
			hipConfig.ConversionBehavior.AcceptBadCOMP3Sign = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@acceptBadCOMP3Sign").Value);
			hipConfig.ConversionBehavior.AcceptNullPacked = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@acceptNullPacked").Value);
			hipConfig.ConversionBehavior.AcceptNullZoned = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@acceptNullZoned").Value);
			hipConfig.ConversionBehavior.AlwaysCheckForNull = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@alwaysCheckForNull").Value);
			hipConfig.ConversionBehavior.StringsAreNullTerminatedAndSpacePadded = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@stringsAreNullTerminatedAndSpacePadded").Value);
			hipConfig.ConversionBehavior.TrimTrailingNulls = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@trimTrailingNulls").Value);
			hipConfig.ConversionBehavior.ConvertReceivedStringsAsIs = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@convertReceivedStringsAsIs").Value);
			hipConfig.ConversionBehavior.AllowNullRedefines = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@allowNullRedefines").Value);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x0009DB80 File Offset: 0x0009BD80
		public static void WriteConfigurationToXmlFile(ref HipConfigurationSectionHandler hipConfig, string fileName, HipGeneratedSchemaType schemaType)
		{
			try
			{
				string text = HipConfigurationUtilities.ConfigurationFileToXml(ref hipConfig, schemaType);
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
				xmlReaderSettings.XmlResolver = null;
				XmlReader xmlReader = XmlReader.Create(new StringReader(text), xmlReaderSettings);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.XmlResolver = null;
				xmlDocument.Load(xmlReader);
				xmlDocument.Save(fileName);
				new ValidateConfigurationFile().ValidateConfigFile(fileName, "HostIntegrationTiHipConfiguration.xsd");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x0009DBFC File Offset: 0x0009BDFC
		public static StreamReader WriteConfigurationToXmlStream(ref HipConfigurationSectionHandler hipConfig, HipGeneratedSchemaType schemaType)
		{
			StreamReader streamReader;
			try
			{
				string text = HipConfigurationUtilities.ConfigurationFileToXml(ref hipConfig, schemaType);
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
				xmlReaderSettings.XmlResolver = null;
				XmlReader xmlReader = XmlReader.Create(new StringReader(text), xmlReaderSettings);
				new XmlDocument
				{
					XmlResolver = null
				}.Load(xmlReader);
				MemoryStream memoryStream = new MemoryStream();
				StreamWriter streamWriter = new StreamWriter(memoryStream);
				streamWriter.Write(text);
				streamWriter.Flush();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				streamReader = new StreamReader(memoryStream);
				new ValidateConfigurationFile().ValidateConfigFile(streamReader, "HostIntegrationTiHipConfiguration.xsd");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return streamReader;
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x0009DC9C File Offset: 0x0009BE9C
		public static string ConfigurationFileToXml(ref HipConfigurationSectionHandler hipConfig, HipGeneratedSchemaType schemaType)
		{
			return "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration><configSections>" + hipConfig.SectionXml + "</configSections>" + HipConfigurationUtilities.TiHipElementXml(hipConfig, schemaType) + "</configuration>";
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x0009DCCB File Offset: 0x0009BECB
		internal static string TiHipElementXml(HipConfigurationSectionHandler hipConfig, HipGeneratedSchemaType schemaType)
		{
			return "<hostIntegration.ti.hip xmlns=\"http://schemas.microsoft.com/his/Config/TiHip/2013\">" + HipConfigurationUtilities.TiHipElementInnerXml(hipConfig, schemaType) + "</hostIntegration.ti.hip>";
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x0009DCE8 File Offset: 0x0009BEE8
		internal static string TiHipElementInnerXml(HipConfigurationSectionHandler hipConfig, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			if (hipConfig.ReadOrder == null)
			{
				hipConfig.ReadOrder = new ReadOrder();
			}
			text = HipConfigurationUtilities.GetReadOrderXml(hipConfig.ReadOrder, schemaType);
			if (hipConfig.Cache == null)
			{
				hipConfig.Cache = new Cache();
			}
			text += HipConfigurationUtilities.GetCacheXml(hipConfig.Cache, schemaType);
			if (schemaType == HipGeneratedSchemaType.Full || (schemaType == HipGeneratedSchemaType.Minimal && hipConfig.EssoSecurityPolicies.Count > 0))
			{
				text += "<essoSecurityPolicies>";
				foreach (object obj in hipConfig.EssoSecurityPolicies)
				{
					EssoSecurityPolicy essoSecurityPolicy = (EssoSecurityPolicy)obj;
					text += HipConfigurationUtilities.GetEssoSecurityPolicyXml(essoSecurityPolicy, schemaType);
				}
				text += "</essoSecurityPolicies>";
			}
			if (schemaType == HipGeneratedSchemaType.Full || (schemaType == HipGeneratedSchemaType.Minimal && hipConfig.TcpHostEnvironments.Count > 0))
			{
				text += "<tcpHostEnvironments>";
				foreach (object obj2 in hipConfig.TcpHostEnvironments)
				{
					TcpHostEnvironment tcpHostEnvironment = (TcpHostEnvironment)obj2;
					text += HipConfigurationUtilities.GetTcpHostEnvironmentXml(tcpHostEnvironment, schemaType);
				}
				text += "</tcpHostEnvironments>";
			}
			if (schemaType == HipGeneratedSchemaType.Full || (schemaType == HipGeneratedSchemaType.Minimal && hipConfig.SnaHostEnvironments.Count > 0))
			{
				text += "<snaHostEnvironments>";
				foreach (object obj3 in hipConfig.SnaHostEnvironments)
				{
					SnaHostEnvironment snaHostEnvironment = (SnaHostEnvironment)obj3;
					text += HipConfigurationUtilities.GetSnaHostEnvironmentXml(snaHostEnvironment, schemaType);
				}
				text += "</snaHostEnvironments>";
			}
			text += "<objects>";
			foreach (object obj4 in hipConfig.HipObjects)
			{
				HipObject hipObject = (HipObject)obj4;
				text += HipConfigurationUtilities.GetHipObjectXml(hipObject, schemaType);
			}
			text += "</objects>";
			if (hipConfig.ConversionBehavior == null)
			{
				hipConfig.ConversionBehavior = new ConversionBehavior();
			}
			text += HipConfigurationUtilities.GetConversionBehaviorXml(hipConfig.ConversionBehavior, schemaType);
			text += "<services>";
			foreach (object obj5 in hipConfig.Services)
			{
				Service service = (Service)obj5;
				text = string.Concat(new string[]
				{
					text,
					"<service serviceName=\"",
					service.ServiceName,
					"\" assemblyPath=\"",
					service.AssemblyPath.Replace("\\\\", "\\"),
					"\" >"
				});
				switch (service.ProgrammingModel)
				{
				case ProgrammingModel.ElmLink:
					text += HipConfigurationUtilities.GetElmLinkServiceXml(service, schemaType);
					break;
				case ProgrammingModel.ElmUserData:
					text += HipConfigurationUtilities.GetElmUserDataServiceXml(service, schemaType);
					break;
				case ProgrammingModel.SnaLink:
					text += HipConfigurationUtilities.GetSnaLinkServiceXml(service, schemaType);
					break;
				case ProgrammingModel.SnaEndpoint:
					text += HipConfigurationUtilities.GetSnaEndpointServiceXml(service, schemaType);
					break;
				case ProgrammingModel.SnaUserData:
					text += HipConfigurationUtilities.GetSnaUserDataServiceXml(service, schemaType);
					break;
				case ProgrammingModel.TrmLink:
					text += HipConfigurationUtilities.GetTrmLinkServiceXml(service, schemaType);
					break;
				case ProgrammingModel.Http:
					text += HipConfigurationUtilities.GetHttpServiceXml(service, schemaType);
					break;
				}
				text += "</service>";
			}
			text += "</services>";
			return text;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x0009E0E0 File Offset: 0x0009C2E0
		private static void AddHttpEndpoints(ref string hipConfigXml, HttpEndpointCollection httpEndpoints, HipGeneratedSchemaType schemaType)
		{
			hipConfigXml += "<endpoints>";
			foreach (object obj in httpEndpoints)
			{
				HttpEndpoint httpEndpoint = (HttpEndpoint)obj;
				hipConfigXml += "<endpoint ";
				PropertyInformation propertyInformation = httpEndpoint.ElementInformation.Properties["webSite"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "webSite=\"" + httpEndpoint.WebSite + "\" ";
				}
				propertyInformation = httpEndpoint.ElementInformation.Properties["port"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (int)propertyInformation.Value != (int)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "port=\"" + httpEndpoint.Port.ToString() + "\" ";
				}
				propertyInformation = httpEndpoint.ElementInformation.Properties["useSsl"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "useSsl=\"" + httpEndpoint.UseSsl.ToString().ToLowerInvariant() + "\" ";
				}
				hipConfigXml += "/>";
			}
			hipConfigXml += "</endpoints>";
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x0009E2B0 File Offset: 0x0009C4B0
		private static void AddLinkResolutionEntries(ref string hipConfigXml, ResolutionEntryCollection resolutionEntries, HipGeneratedSchemaType schemaType)
		{
			hipConfigXml += "<resolutionEntries>";
			foreach (object obj in resolutionEntries)
			{
				ResolutionEntry resolutionEntry = (ResolutionEntry)obj;
				hipConfigXml += "<resolutionEntry ";
				PropertyInformation propertyInformation = resolutionEntry.ElementInformation.Properties["linkToProgram"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "linkToProgram=\"" + resolutionEntry.LinkToProgram + "\" ";
				}
				propertyInformation = resolutionEntry.ElementInformation.Properties["interfaceName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "interfaceName=\"" + resolutionEntry.InterfaceName + "\" ";
				}
				propertyInformation = resolutionEntry.ElementInformation.Properties["method"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "method=\"" + resolutionEntry.Method + "\" ";
				}
				if (!string.IsNullOrEmpty(resolutionEntry.EssoSecurityPolicyName))
				{
					hipConfigXml = hipConfigXml + "essoSecurityPolicyName=\"" + resolutionEntry.EssoSecurityPolicyName + "\" ";
				}
				hipConfigXml += "/>";
			}
			hipConfigXml += "</resolutionEntries>";
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x0009E498 File Offset: 0x0009C698
		private static void AddHttpResolutionEntries(ref string hipConfigXml, ResolutionEntryCollection resolutionEntries, HipGeneratedSchemaType schemaType)
		{
			hipConfigXml += "<resolutionEntries>";
			foreach (object obj in resolutionEntries)
			{
				ResolutionEntry resolutionEntry = (ResolutionEntry)obj;
				hipConfigXml += "<resolutionEntry ";
				PropertyInformation propertyInformation = resolutionEntry.ElementInformation.Properties["prefixProgramId"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "prefixProgramId=\"" + resolutionEntry.PrefixProgramId + "\" ";
				}
				propertyInformation = resolutionEntry.ElementInformation.Properties["interfaceName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "interfaceName=\"" + resolutionEntry.InterfaceName + "\" ";
				}
				propertyInformation = resolutionEntry.ElementInformation.Properties["method"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "method=\"" + resolutionEntry.Method + "\" ";
				}
				if (!string.IsNullOrEmpty(resolutionEntry.EssoSecurityPolicyName))
				{
					hipConfigXml = hipConfigXml + "essoSecurityPolicyName=\"" + resolutionEntry.EssoSecurityPolicyName + "\" ";
				}
				hipConfigXml += "/>";
			}
			hipConfigXml += "</resolutionEntries>";
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x0009E680 File Offset: 0x0009C880
		private static void AddDataResolutionEntries(ref string hipConfigXml, ResolutionEntryCollection resolutionEntries, HipGeneratedSchemaType schemaType)
		{
			hipConfigXml += "<resolutionEntries>";
			foreach (object obj in resolutionEntries)
			{
				ResolutionEntry resolutionEntry = (ResolutionEntry)obj;
				hipConfigXml += "<resolutionEntry ";
				PropertyInformation propertyInformation = resolutionEntry.ElementInformation.Properties["data"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "data=\"" + resolutionEntry.Data + "\" ";
				}
				propertyInformation = resolutionEntry.ElementInformation.Properties["position"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (int)propertyInformation.Value != (int)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "position=\"" + resolutionEntry.Position.ToString() + "\" ";
				}
				propertyInformation = resolutionEntry.ElementInformation.Properties["interfaceName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "interfaceName=\"" + resolutionEntry.InterfaceName + "\" ";
				}
				propertyInformation = resolutionEntry.ElementInformation.Properties["method"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "method=\"" + resolutionEntry.Method + "\" ";
				}
				if (!string.IsNullOrEmpty(resolutionEntry.EssoSecurityPolicyName))
				{
					hipConfigXml = hipConfigXml + "essoSecurityPolicyName=\"" + resolutionEntry.EssoSecurityPolicyName + "\" ";
				}
				hipConfigXml += "/>";
			}
			hipConfigXml += "</resolutionEntries>";
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x0009E8D0 File Offset: 0x0009CAD0
		private static void AddEndpointResolutionEntries(ref string hipConfigXml, ResolutionEntryCollection resolutionEntries, HipGeneratedSchemaType schemaType)
		{
			hipConfigXml += "<resolutionEntries>";
			foreach (object obj in resolutionEntries)
			{
				ResolutionEntry resolutionEntry = (ResolutionEntry)obj;
				hipConfigXml += "<resolutionEntry ";
				PropertyInformation propertyInformation = resolutionEntry.ElementInformation.Properties["endpoint"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "endpoint=\"" + resolutionEntry.Endpoint + "\" ";
				}
				propertyInformation = resolutionEntry.ElementInformation.Properties["interfaceName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "interfaceName=\"" + resolutionEntry.InterfaceName + "\" ";
				}
				propertyInformation = resolutionEntry.ElementInformation.Properties["method"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					hipConfigXml = hipConfigXml + "method=\"" + resolutionEntry.Method + "\" ";
				}
				if (!string.IsNullOrEmpty(resolutionEntry.EssoSecurityPolicyName))
				{
					hipConfigXml = hipConfigXml + "essoSecurityPolicyName=\"" + resolutionEntry.EssoSecurityPolicyName + "\" ";
				}
				hipConfigXml += "/>";
			}
			hipConfigXml += "</resolutionEntries>";
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x0009EAB8 File Offset: 0x0009CCB8
		private static string GetConversionBehaviorXml(ConversionBehavior conversionBehavior, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in conversionBehavior.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if ((bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue)
					{
						flag = true;
						break;
					}
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text = "<conversionBehavior ";
				PropertyInformation propertyInformation2 = conversionBehavior.ElementInformation.Properties["acceptAllInvalidNumerics"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "acceptAllInvalidNumerics=\"" + conversionBehavior.AcceptAllInvalidNumerics.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["acceptBadCOMP3Sign"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "acceptBadCOMP3Sign=\"" + conversionBehavior.AcceptBadCOMP3Sign.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["acceptNullPacked"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "acceptNullPacked=\"" + conversionBehavior.AcceptNullPacked.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["acceptNullZoned"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "acceptNullZoned=\"" + conversionBehavior.AcceptNullZoned.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["alwaysCheckForNull"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "alwaysCheckForNull=\"" + conversionBehavior.AlwaysCheckForNull.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["stringsAreNullTerminatedAndSpacePadded"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "stringsAreNullTerminatedAndSpacePadded=\"" + conversionBehavior.StringsAreNullTerminatedAndSpacePadded.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["trimTrailingNulls"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "trimTrailingNulls=\"" + conversionBehavior.TrimTrailingNulls.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["convertReceivedStringsAsIs"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "convertReceivedStringsAsIs=\"" + conversionBehavior.ConvertReceivedStringsAsIs.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["allowNullRedefines"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "allowNullRedefines=\"" + conversionBehavior.AllowNullRedefines.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x0009EF10 File Offset: 0x0009D110
		private static string GetReadOrderXml(ReadOrder readOrder, HipGeneratedSchemaType schemaType)
		{
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in readOrder.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if ((string)propertyInformation.Value != (string)propertyInformation.DefaultValue)
					{
						flag = true;
						break;
					}
				}
			}
			string text = "<readOrder ";
			text = text + "appConfig=\"" + readOrder.AppConfig.ToString().ToLowerInvariant() + "\" ";
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				PropertyInformation propertyInformation2 = readOrder.ElementInformation.Properties["cache"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "cache=\"" + readOrder.Cache.ToString().ToLowerInvariant() + "\" ";
				}
			}
			return text + "/>";
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x0009F050 File Offset: 0x0009D250
		private static string GetCacheXml(Cache cache, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in cache.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text = "<cache ";
				PropertyInformation propertyInformation2 = cache.ElementInformation.Properties["cacheName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "cacheName=\"" + cache.CacheName + "\" ";
				}
				propertyInformation2 = cache.ElementInformation.Properties["key"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "key=\"" + cache.Key + "\" ";
				}
				propertyInformation2 = cache.ElementInformation.Properties["region"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "region=\"" + cache.Region + "\" ";
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x0009F290 File Offset: 0x0009D490
		private static string GetEssoSecurityPolicyXml(EssoSecurityPolicy essoSecurityPolicy, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in essoSecurityPolicy.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<essoSecurityPolicy ";
				PropertyInformation propertyInformation2 = essoSecurityPolicy.ElementInformation.Properties["name"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "name=\"" + essoSecurityPolicy.Name + "\" ";
				}
				propertyInformation2 = essoSecurityPolicy.ElementInformation.Properties["affiliateApplication"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "affiliateApplication=\"" + essoSecurityPolicy.AffiliateApplication + "\" ";
				}
				propertyInformation2 = essoSecurityPolicy.ElementInformation.Properties["defaultCredentialsGroup"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "defaultCredentialsGroup=\"" + essoSecurityPolicy.DefaultCredentialsGroup + "\" ";
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x0009F4D4 File Offset: 0x0009D6D4
		private static string GetTcpHostEnvironmentXml(TcpHostEnvironment tcpHostEnvironment, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in tcpHostEnvironment.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<tcpHostEnvironment ";
				PropertyInformation propertyInformation2 = tcpHostEnvironment.ElementInformation.Properties["name"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "name=\"" + tcpHostEnvironment.Name + "\" ";
				}
				propertyInformation2 = tcpHostEnvironment.ElementInformation.Properties["ipAddress"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "ipAddress=\"" + tcpHostEnvironment.IpAddress + "\" ";
				}
				propertyInformation2 = tcpHostEnvironment.ElementInformation.Properties["codePage"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (int)propertyInformation2.Value != (int)propertyInformation2.DefaultValue))
				{
					text = text + "codePage=\"" + tcpHostEnvironment.CodePage.ToString() + "\" ";
				}
				propertyInformation2 = tcpHostEnvironment.ElementInformation.Properties["dataConversion"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "dataConversion=\"" + tcpHostEnvironment.DataConversion.ToString() + "\" ";
				}
				propertyInformation2 = tcpHostEnvironment.ElementInformation.Properties["timeout"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (int)propertyInformation2.Value != (int)propertyInformation2.DefaultValue))
				{
					text = text + "timeout=\"" + tcpHostEnvironment.Timeout.ToString() + "\" ";
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x0009F7F0 File Offset: 0x0009D9F0
		private static string GetSnaHostEnvironmentXml(SnaHostEnvironment snaHostEnvironment, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in snaHostEnvironment.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<snaHostEnvironment ";
				PropertyInformation propertyInformation2 = snaHostEnvironment.ElementInformation.Properties["name"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "name=\"" + snaHostEnvironment.Name + "\" ";
				}
				propertyInformation2 = snaHostEnvironment.ElementInformation.Properties["remoteLuName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "remoteLuName=\"" + snaHostEnvironment.RemoteLuName + "\" ";
				}
				propertyInformation2 = snaHostEnvironment.ElementInformation.Properties["codePage"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (int)propertyInformation2.Value != (int)propertyInformation2.DefaultValue))
				{
					text = text + "codePage=\"" + snaHostEnvironment.CodePage.ToString() + "\" ";
				}
				propertyInformation2 = snaHostEnvironment.ElementInformation.Properties["dataConversion"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "dataConversion=\"" + snaHostEnvironment.DataConversion.ToString() + "\" ";
				}
				propertyInformation2 = snaHostEnvironment.ElementInformation.Properties["timeout"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (int)propertyInformation2.Value != (int)propertyInformation2.DefaultValue))
				{
					text = text + "timeout=\"" + snaHostEnvironment.Timeout.ToString() + "\" ";
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x0009FB0C File Offset: 0x0009DD0C
		private static string GetHipObjectXml(HipObject hipObject, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in hipObject.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<object ";
				PropertyInformation propertyInformation2 = hipObject.ElementInformation.Properties["metaDataInterface"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "metaDataInterface=\"" + hipObject.MetaDataInterface + "\" ";
				}
				if (!string.IsNullOrEmpty(hipObject.ImplementingClass))
				{
					propertyInformation2 = hipObject.ElementInformation.Properties["implementingClass"];
					if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
					{
						text = text + "implementingClass=\"" + hipObject.ImplementingClass + "\" ";
					}
				}
				if (!string.IsNullOrEmpty(hipObject.ImplementingAssembly))
				{
					propertyInformation2 = hipObject.ElementInformation.Properties["implementingAssembly"];
					if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
					{
						text = text + "implementingAssembly=\"" + hipObject.ImplementingAssembly + "\" ";
					}
				}
				propertyInformation2 = hipObject.ElementInformation.Properties["metaDataAssembly"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "metaDataAssembly=\"" + hipObject.MetaDataAssembly + "\" ";
				}
				if (!string.IsNullOrEmpty(hipObject.WcfServiceUrl))
				{
					propertyInformation2 = hipObject.ElementInformation.Properties["wcfServiceUrl"];
					if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
					{
						text = text + "wcfServiceUrl=\"" + hipObject.WcfServiceUrl + "\" ";
					}
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x0009FE38 File Offset: 0x0009E038
		private static string GetServiceXml(Service service, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in service.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<service ";
				PropertyInformation propertyInformation2 = service.ElementInformation.Properties["serviceName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "serviceName=\"" + service.ServiceName + "\" ";
				}
				propertyInformation2 = service.ElementInformation.Properties["assemblyPath"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "assemblyPath=\"" + service.AssemblyPath + "\" ";
				}
				text += ">";
			}
			return text;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000A001C File Offset: 0x0009E21C
		private static string GetHttpServiceXml(Service service, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in service.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<http ";
				PropertyInformation propertyInformation2 = service.Http.ElementInformation.Properties["hosts"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "hosts=\"" + service.Http.Hosts + "\" ";
				}
				text += ">";
				HipConfigurationUtilities.AddHttpEndpoints(ref text, service.Http.Endpoints, schemaType);
				HipConfigurationUtilities.AddHttpResolutionEntries(ref text, service.Http.ResolutionEntries, schemaType);
				text += "</http>";
			}
			return text;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000A01DC File Offset: 0x0009E3DC
		private static string GetElmLinkServiceXml(Service service, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in service.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<elmLink ";
				PropertyInformation propertyInformation2 = service.ElmLink.ElementInformation.Properties["ports"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "ports=\"" + service.ElmLink.Ports + "\" ";
				}
				propertyInformation2 = service.ElmLink.ElementInformation.Properties["hosts"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "hosts=\"" + service.ElmLink.Hosts + "\" ";
				}
				propertyInformation2 = service.ElmLink.ElementInformation.Properties["requestHeaderFormat"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "requestHeaderFormat=\"" + service.ElmLink.RequestHeaderFormat + "\" ";
				}
				text += ">";
				HipConfigurationUtilities.AddLinkResolutionEntries(ref text, service.ElmLink.ResolutionEntries, schemaType);
				text += "</elmLink>";
			}
			return text;
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000A045C File Offset: 0x0009E65C
		private static string GetElmUserDataServiceXml(Service service, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in service.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<elmUserData ";
				PropertyInformation propertyInformation2 = service.ElmUserData.ElementInformation.Properties["ports"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "ports=\"" + service.ElmUserData.Ports + "\" ";
				}
				propertyInformation2 = service.ElmUserData.ElementInformation.Properties["hosts"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "hosts=\"" + service.ElmUserData.Hosts + "\" ";
				}
				propertyInformation2 = service.ElmUserData.ElementInformation.Properties["requestHeaderFormat"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "requestHeaderFormat=\"" + service.ElmUserData.RequestHeaderFormat + "\" ";
				}
				text += ">";
				HipConfigurationUtilities.AddDataResolutionEntries(ref text, service.ElmUserData.ResolutionEntries, schemaType);
				text += "</elmUserData>";
			}
			return text;
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000A06DC File Offset: 0x0009E8DC
		private static string GetSnaEndpointServiceXml(Service service, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in service.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<snaEndpoint ";
				PropertyInformation propertyInformation2 = service.SnaEndpoint.ElementInformation.Properties["localLuName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "localLuName=\"" + service.SnaEndpoint.LocalLuName + "\" ";
				}
				propertyInformation2 = service.SnaEndpoint.ElementInformation.Properties["hosts"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "hosts=\"" + service.SnaEndpoint.Hosts + "\" ";
				}
				text += ">";
				HipConfigurationUtilities.AddEndpointResolutionEntries(ref text, service.SnaEndpoint.ResolutionEntries, schemaType);
				text += "</snaEndpoint>";
			}
			return text;
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000A08F4 File Offset: 0x0009EAF4
		private static string GetSnaLinkServiceXml(Service service, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in service.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<snaLink ";
				PropertyInformation propertyInformation2 = service.SnaLink.ElementInformation.Properties["localLuName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "localLuName=\"" + service.SnaLink.LocalLuName + "\" ";
				}
				propertyInformation2 = service.SnaLink.ElementInformation.Properties["hosts"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "hosts=\"" + service.SnaLink.Hosts + "\" ";
				}
				text += ">";
				HipConfigurationUtilities.AddLinkResolutionEntries(ref text, service.SnaLink.ResolutionEntries, schemaType);
				text += "</snaLink>";
			}
			return text;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000A0B0C File Offset: 0x0009ED0C
		private static string GetSnaUserDataServiceXml(Service service, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in service.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<snaUserData ";
				PropertyInformation propertyInformation2 = service.SnaUserData.ElementInformation.Properties["localLuName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "localLuName=\"" + service.SnaUserData.LocalLuName + "\" ";
				}
				propertyInformation2 = service.SnaUserData.ElementInformation.Properties["hosts"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "hosts=\"" + service.SnaUserData.Hosts + "\" ";
				}
				text += ">";
				HipConfigurationUtilities.AddDataResolutionEntries(ref text, service.SnaUserData.ResolutionEntries, schemaType);
				text += "</snaUserData>";
			}
			return text;
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000A0D24 File Offset: 0x0009EF24
		private static string GetTrmLinkServiceXml(Service service, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in service.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<trmLink ";
				PropertyInformation propertyInformation2 = service.TrmLink.ElementInformation.Properties["ports"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "ports=\"" + service.TrmLink.Ports + "\" ";
				}
				propertyInformation2 = service.TrmLink.ElementInformation.Properties["hosts"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "hosts=\"" + service.TrmLink.Hosts + "\" ";
				}
				propertyInformation2 = service.TrmLink.ElementInformation.Properties["requestHeaderFormat"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "requestHeaderFormat=\"" + service.TrmLink.RequestHeaderFormat + "\" ";
				}
				text += ">";
				HipConfigurationUtilities.AddLinkResolutionEntries(ref text, service.TrmLink.ResolutionEntries, schemaType);
				text += "</trmLink>";
			}
			return text;
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000A0FA4 File Offset: 0x0009F1A4
		private static string GetHttpEndpointXml(HttpEndpoint httpEndpoint, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in httpEndpoint.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<endpoint ";
				PropertyInformation propertyInformation2 = httpEndpoint.ElementInformation.Properties["webSite"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "webSite=\"" + httpEndpoint.WebSite + "\" ";
				}
				propertyInformation2 = httpEndpoint.ElementInformation.Properties["port"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (int)propertyInformation2.Value != (int)propertyInformation2.DefaultValue))
				{
					text = text + "port=\"" + httpEndpoint.Port.ToString() + "\" ";
				}
				propertyInformation2 = httpEndpoint.ElementInformation.Properties["useSsl"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "useSsl=\"" + httpEndpoint.UseSsl.ToString() + "\" ";
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000A11F0 File Offset: 0x0009F3F0
		private static string GetResolutionXml(ResolutionEntry resolutionEntry, HipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == HipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in resolutionEntry.ElementInformation.Properties)
				{
					PropertyInformation propertyInformation = (PropertyInformation)obj;
					if (propertyInformation.IsRequired)
					{
						flag = true;
						break;
					}
					string name = propertyInformation.Type.Name;
					if (name != null)
					{
						if (!(name == "String"))
						{
							if (!(name == "Int32"))
							{
								goto IL_00AE;
							}
							if ((int)propertyInformation.DefaultValue != (int)propertyInformation.Value)
							{
								flag = true;
							}
						}
						else if ((string)propertyInformation.DefaultValue != (string)propertyInformation.Value)
						{
							flag = true;
						}
						if (flag)
						{
							break;
						}
						continue;
					}
					IL_00AE:
					throw new Exception("Unknown type");
				}
			}
			if (schemaType == HipGeneratedSchemaType.Full || flag)
			{
				text += "<resolutionEntry ";
				PropertyInformation propertyInformation2 = resolutionEntry.ElementInformation.Properties["interfaceName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "interfaceName=\"" + resolutionEntry.InterfaceName + "\" ";
				}
				propertyInformation2 = resolutionEntry.ElementInformation.Properties["method"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "method=\"" + resolutionEntry.Method + "\" ";
				}
				propertyInformation2 = resolutionEntry.ElementInformation.Properties["essoSecurityPolicyName"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "essoSecurityPolicyName=\"" + resolutionEntry.EssoSecurityPolicyName + "\" ";
				}
				propertyInformation2 = resolutionEntry.ElementInformation.Properties["linkToProgram"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "linkToProgram=\"" + resolutionEntry.LinkToProgram + "\" ";
				}
				propertyInformation2 = resolutionEntry.ElementInformation.Properties["data"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "data=\"" + resolutionEntry.Data + "\" ";
				}
				propertyInformation2 = resolutionEntry.ElementInformation.Properties["position"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (int)propertyInformation2.Value != (int)propertyInformation2.DefaultValue))
				{
					text = text + "position=\"" + resolutionEntry.Position.ToString() + "\" ";
				}
				propertyInformation2 = resolutionEntry.ElementInformation.Properties["prefixprogramid"];
				if (schemaType == HipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == HipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (int)propertyInformation2.Value != (int)propertyInformation2.DefaultValue))
				{
					text = text + "prefixprogramid=\"" + resolutionEntry.PrefixProgramId + "\" ";
				}
				text += "/>";
			}
			return text;
		}
	}
}
