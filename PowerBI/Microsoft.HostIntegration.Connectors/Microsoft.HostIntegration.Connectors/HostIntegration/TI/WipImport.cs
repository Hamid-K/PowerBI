using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000754 RID: 1876
	public static class WipImport
	{
		// Token: 0x06003B97 RID: 15255 RVA: 0x000CB34C File Offset: 0x000C954C
		public static void Import(string tiExportedFile, out RemoteEnvironmentCollection remoteEnvironments, out WipObjectCollection objects)
		{
			remoteEnvironments = new RemoteEnvironmentCollection();
			objects = new WipObjectCollection();
			XmlReader xmlReader = XmlReader.Create(tiExportedFile, new XmlReaderSettings
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			});
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			xmlReader.Close();
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNode = documentElement["WIPREs"];
			XmlNode xmlNode2 = documentElement["WIPObjects"];
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("RE");
			XmlNodeList xmlNodeList2 = xmlNode2.SelectNodes("WIPObject");
			Dictionary<string, RemoteEnvironment> dictionary = new Dictionary<string, RemoteEnvironment>();
			foreach (object obj in xmlNodeList)
			{
				RemoteEnvironment remoteEnvironment = WipImport.RemoteEnvironmentFromReXml((XmlNode)obj, dictionary);
				remoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
			}
			foreach (object obj2 in xmlNodeList2)
			{
				WipObject wipObject = WipImport.ObjectFromObjectXml((XmlNode)obj2, dictionary);
				foreach (object obj3 in remoteEnvironments)
				{
					RemoteEnvironment remoteEnvironment2 = (RemoteEnvironment)obj3;
					if (remoteEnvironment2.Name == wipObject.RemoteEnvironmentName)
					{
						wipObject.RemoteEnvironmentType = remoteEnvironment2.RemoteEnvironmentType;
						wipObject.RemoteEnvironmentTypeId = remoteEnvironment2.RemoteEnvironmentType.ToString();
					}
				}
				objects.AddWipObject(wipObject);
			}
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x000CB510 File Offset: 0x000C9710
		private static RemoteEnvironment RemoteEnvironmentFromReXml(XmlNode node, Dictionary<string, RemoteEnvironment> guidToRes)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!(xmlNode.Name == "ConnectionInfo"))
				{
					string text;
					string text2;
					if (xmlNode.Name == "GUID")
					{
						text = xmlNode.InnerText;
						text2 = "GUID";
					}
					else
					{
						text = xmlNode["RegistryValue"].InnerText;
						text2 = xmlNode.Name;
					}
					dictionary.Add(text2, text);
				}
			}
			string text3 = null;
			if (dictionary.ContainsKey("GUID"))
			{
				text3 = dictionary["GUID"];
			}
			if (string.IsNullOrWhiteSpace(text3))
			{
				throw new ApplicationException("exported file has an RE with no guid");
			}
			text3 = text3.ToUpperInvariant();
			if (guidToRes.ContainsKey(text3))
			{
				throw new ApplicationException("exported file has duplicate RE guids");
			}
			RemoteEnvironment remoteEnvironment = WipImport.RemoteEnvironmentFromValues(dictionary);
			guidToRes.Add(text3, remoteEnvironment);
			return remoteEnvironment;
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x000CB624 File Offset: 0x000C9824
		private static string GetValue(Dictionary<string, string> nameToValues, string valueName)
		{
			return WipImport.GetValue(nameToValues, valueName, false, null);
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x000CB62F File Offset: 0x000C982F
		private static string GetValue(Dictionary<string, string> nameToValues, string valueName, string defaultString)
		{
			return WipImport.GetValue(nameToValues, valueName, true, defaultString);
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x000CB63C File Offset: 0x000C983C
		private static string GetValue(Dictionary<string, string> nameToValues, string valueName, bool allowDefault, string defaultString)
		{
			string text = null;
			if (nameToValues.ContainsKey(valueName))
			{
				text = nameToValues[valueName];
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				if (!allowDefault)
				{
					throw new ApplicationException("Missing value: " + valueName);
				}
				text = defaultString;
			}
			return text;
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x000CB680 File Offset: 0x000C9880
		private static int GetValue(Dictionary<string, string> nameToValues, string valueName, int defaultInt)
		{
			string text = null;
			if (nameToValues.ContainsKey(valueName))
			{
				text = nameToValues[valueName];
			}
			int num;
			if (string.IsNullOrWhiteSpace(text))
			{
				num = defaultInt;
			}
			else if (!int.TryParse(text, out num))
			{
				throw new ApplicationException(string.Concat(new string[] { "Value: ", valueName, " could not be parsed to an int: '", text, "'" }));
			}
			return num;
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x000CB6E8 File Offset: 0x000C98E8
		private static RemoteEnvironment RemoteEnvironmentFromValues(Dictionary<string, string> nameToValues)
		{
			string text = null;
			int num = 0;
			string value = WipImport.GetValue(nameToValues, "Class");
			string value2 = WipImport.GetValue(nameToValues, "Name");
			string value3 = WipImport.GetValue(nameToValues, "IpAddress", text);
			int value4 = WipImport.GetValue(nameToValues, "CodePage", num);
			int value5 = WipImport.GetValue(nameToValues, "Timeout", num);
			int value6 = WipImport.GetValue(nameToValues, "SyncLevel2", num);
			bool flag = value6 != 0 && value6 != -1;
			string value7 = WipImport.GetValue(nameToValues, "ItocExitName", text);
			WipImport.GetValue(nameToValues, "LinkTran", text);
			string value8 = WipImport.GetValue(nameToValues, "LU", text);
			string value9 = WipImport.GetValue(nameToValues, "Mode", text);
			string value10 = WipImport.GetValue(nameToValues, "OtmaSysId", text);
			string value11 = WipImport.GetValue(nameToValues, "PLU", text);
			string value12 = WipImport.GetValue(nameToValues, "LinkTran", text);
			string value13 = WipImport.GetValue(nameToValues, "HostDomainName", text);
			string value14 = WipImport.GetValue(nameToValues, "TcpPorts", text);
			WipImport.GetValue(nameToValues, "Version", text);
			string value15 = WipImport.GetValue(nameToValues, "MirrorProg", text);
			string value16 = WipImport.GetValue(nameToValues, "UserAgent", text);
			string value17 = WipImport.GetValue(nameToValues, "AliasTPId", text);
			bool value18 = WipImport.GetValue(nameToValues, "AllowReDirects", num) != 0;
			bool value19 = WipImport.GetValue(nameToValues, "UseSSL", num) != 0;
			string value20 = WipImport.GetValue(nameToValues, "HTTPPort", text);
			string value21 = WipImport.GetValue(nameToValues, "IMSFormatModName", text);
			bool flag2 = WipImport.GetValue(nameToValues, "NoVerifyCert", num) == 0;
			string value22 = WipImport.GetValue(nameToValues, "CertCommonName", text);
			int value23 = WipImport.GetValue(nameToValues, "Security", num);
			bool flag3 = false;
			if ((value23 & 4) == 4)
			{
				flag3 = true;
			}
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			if (value23 != 0 && value23 != 65535 && value23 != -1)
			{
				if ((value23 & 131072) == 131072)
				{
					flag4 = true;
				}
				if ((value23 & 262144) == 262144)
				{
					flag5 = true;
				}
				if ((value23 & 524288) == 524288)
				{
					flag6 = true;
				}
			}
			bool flag7 = (WipImport.GetValue(nameToValues, "AdmFlags", num) & 268435456) == 268435456;
			RemoteEnvironmentClass remoteEnvironmentClass = WipImport._recFactory.GetRemoteEnvironmentClass(value);
			RemoteEnvironment remoteEnvironment = new RemoteEnvironment();
			remoteEnvironment.CodePage = value4;
			remoteEnvironment.IsDefault = false;
			remoteEnvironment.Name = value2;
			remoteEnvironment.Timeout = value5;
			RemoteEnvironmentTypes remoteEnvironmentType = remoteEnvironmentClass.RemoteEnvironmentType;
			if (remoteEnvironmentType <= RemoteEnvironmentTypes.SystemzSocketsUserData)
			{
				if (remoteEnvironmentType <= RemoteEnvironmentTypes.HttpUserData)
				{
					if (remoteEnvironmentType == RemoteEnvironmentTypes.SnaUserData)
					{
						remoteEnvironment.SnaUserData = new SnaUserData
						{
							SecurityFromClientContext = flag3,
							EssoAffiliateApplication = value13,
							LocalLuName = value8,
							RemoteLuName = value11,
							ModeName = value9,
							SyncLevel2Supported = flag,
							IsTypeDefined = true
						};
						return remoteEnvironment;
					}
					if (remoteEnvironmentType == RemoteEnvironmentTypes.SnaLink)
					{
						remoteEnvironment.SnaLink = new SnaLink
						{
							SecurityFromClientContext = flag3,
							EssoAffiliateApplication = value13,
							LocalLuName = value8,
							RemoteLuName = value11,
							ModeName = value9,
							SyncLevel2Supported = flag,
							OverrideSnaSourceTransactionProgram = flag6,
							MirrorTransactionId = value12,
							AllowExplicitSyncPoint = flag7,
							IsTypeDefined = true
						};
						return remoteEnvironment;
					}
					if (remoteEnvironmentType == RemoteEnvironmentTypes.HttpUserData)
					{
						remoteEnvironment.HttpUserData = new HttpUserData
						{
							SecurityFromClientContext = flag3,
							EssoAffiliateApplication = value13,
							IpAddress = value3,
							Ports = value20,
							UseSsl = value19,
							ServerVerificationRequired = flag2,
							AllowRedirects = value18,
							AliasTransactionId = value17,
							UserAgent = value16,
							IsTypeDefined = true
						};
						return remoteEnvironment;
					}
				}
				else if (remoteEnvironmentType <= RemoteEnvironmentTypes.ImsLu62)
				{
					if (remoteEnvironmentType == RemoteEnvironmentTypes.HttpLink)
					{
						remoteEnvironment.HttpLink = new HttpLink
						{
							SecurityFromClientContext = flag3,
							EssoAffiliateApplication = value13,
							IpAddress = value3,
							Ports = value20,
							UseSsl = value19,
							ServerVerificationRequired = flag2,
							AllowRedirects = value18,
							AliasTransactionId = value17,
							UserAgent = value16,
							MirrorProgram = value15,
							IsTypeDefined = true
						};
						return remoteEnvironment;
					}
					if (remoteEnvironmentType == RemoteEnvironmentTypes.ImsLu62)
					{
						remoteEnvironment.ImsLu62 = new ImsLu62
						{
							SecurityFromClientContext = flag3,
							EssoAffiliateApplication = value13,
							LocalLuName = value8,
							RemoteLuName = value11,
							ModeName = value9,
							SyncLevel2Supported = flag,
							IsTypeDefined = true
						};
						return remoteEnvironment;
					}
				}
				else
				{
					if (remoteEnvironmentType == RemoteEnvironmentTypes.ImsConnect)
					{
						remoteEnvironment.ImsConnect = new ImsConnect
						{
							SecurityFromClientContext = flag3,
							EssoAffiliateApplication = value13,
							IpAddress = value3,
							Ports = value14,
							UseSsl = value19,
							ServerVerificationRequired = flag2,
							CertificateCommonName = value22,
							ItocExitName = value7,
							ImsSystemId = value10,
							MfsModName = value21,
							InboundHeaderFormat = (flag5 ? ImsConnectInboundHeaderFormat.HWSIMSO1 : ImsConnectInboundHeaderFormat.HWSIMSO0),
							IsTypeDefined = true
						};
						return remoteEnvironment;
					}
					if (remoteEnvironmentType == RemoteEnvironmentTypes.SystemzSocketsUserData)
					{
						remoteEnvironment.SystemzSocketsUserData = new SystemzSocketsUserData
						{
							IpAddress = value3,
							Ports = value14,
							IsTypeDefined = true
						};
						return remoteEnvironment;
					}
				}
			}
			else if (remoteEnvironmentType <= RemoteEnvironmentTypes.TrmUserData)
			{
				if (remoteEnvironmentType == RemoteEnvironmentTypes.SystemzSocketsLink)
				{
					remoteEnvironment.SystemzSocketsLink = new SystemzSocketsLink
					{
						IpAddress = value3,
						Ports = value14,
						IsTypeDefined = true
					};
					return remoteEnvironment;
				}
				if (remoteEnvironmentType == RemoteEnvironmentTypes.SystemiSocketsUserData)
				{
					remoteEnvironment.SystemiSocketsUserData = new SystemiSocketsUserData
					{
						IpAddress = value3,
						Ports = value14,
						IsTypeDefined = true
					};
					return remoteEnvironment;
				}
				if (remoteEnvironmentType == RemoteEnvironmentTypes.TrmUserData)
				{
					remoteEnvironment.TrmUserData = new TrmUserData
					{
						SecurityFromClientContext = flag3,
						EssoAffiliateApplication = value13,
						IpAddress = value3,
						Ports = value14,
						TcpCicsRequestHeaderFormat = (flag4 ? TcpCicsRequestHeaderFormat.IbmSuppliedExitRoutine : TcpCicsRequestHeaderFormat.Microsoft),
						IsTypeDefined = true,
						UseSsl = value19,
						ServerVerificationRequired = flag2,
						CertificateCommonName = value22
					};
					return remoteEnvironment;
				}
			}
			else if (remoteEnvironmentType <= RemoteEnvironmentTypes.ElmUserData)
			{
				if (remoteEnvironmentType == RemoteEnvironmentTypes.TrmLink)
				{
					remoteEnvironment.TrmLink = new TrmLink
					{
						SecurityFromClientContext = flag3,
						EssoAffiliateApplication = value13,
						IpAddress = value3,
						Ports = value14,
						TcpCicsRequestHeaderFormat = (flag4 ? TcpCicsRequestHeaderFormat.IbmSuppliedExitRoutine : TcpCicsRequestHeaderFormat.Microsoft),
						IsTypeDefined = true,
						UseSsl = value19,
						ServerVerificationRequired = flag2,
						CertificateCommonName = value22
					};
					return remoteEnvironment;
				}
				if (remoteEnvironmentType == RemoteEnvironmentTypes.ElmUserData)
				{
					remoteEnvironment.ElmUserData = new ElmUserData
					{
						SecurityFromClientContext = flag3,
						EssoAffiliateApplication = value13,
						IpAddress = value3,
						Ports = value14,
						TcpCicsRequestHeaderFormat = (flag4 ? TcpCicsRequestHeaderFormat.IbmSuppliedExitRoutine : TcpCicsRequestHeaderFormat.Microsoft),
						IsTypeDefined = true,
						UseSsl = value19,
						ServerVerificationRequired = flag2,
						CertificateCommonName = value22
					};
					return remoteEnvironment;
				}
			}
			else
			{
				if (remoteEnvironmentType == RemoteEnvironmentTypes.ElmLink)
				{
					remoteEnvironment.ElmLink = new ElmLink
					{
						SecurityFromClientContext = flag3,
						EssoAffiliateApplication = value13,
						IpAddress = value3,
						Ports = value14,
						TcpCicsRequestHeaderFormat = (flag4 ? TcpCicsRequestHeaderFormat.IbmSuppliedExitRoutine : TcpCicsRequestHeaderFormat.Microsoft),
						IsTypeDefined = true,
						UseSsl = value19,
						ServerVerificationRequired = flag2,
						CertificateCommonName = value22
					};
					return remoteEnvironment;
				}
				if (remoteEnvironmentType == RemoteEnvironmentTypes.DistributedProgramCall)
				{
					remoteEnvironment.DistributedProgramCall = new DistributedProgramCall
					{
						SecurityFromClientContext = flag3,
						EssoAffiliateApplication = value13,
						IpAddress = value3,
						Ports = value14,
						UseSsl = value19,
						ServerVerificationRequired = flag2,
						CertificateCommonName = value22,
						IsTypeDefined = true
					};
					return remoteEnvironment;
				}
			}
			throw new ApplicationException("RE type is not supported");
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x000CBF40 File Offset: 0x000CA140
		private static WipObject ObjectFromObjectXml(XmlNode node, Dictionary<string, RemoteEnvironment> guidToRes)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string text;
				string text2;
				if (xmlNode.Name == "GUID")
				{
					text = xmlNode.InnerText;
					text2 = "GUID";
				}
				else
				{
					text = xmlNode["RegistryValue"].InnerText;
					text2 = xmlNode.Name;
				}
				dictionary.Add(text2, text);
			}
			if (!dictionary.ContainsKey("Name"))
			{
				throw new ApplicationException("exported file has an object with no Name");
			}
			string text3 = null;
			if (dictionary.ContainsKey("RE"))
			{
				text3 = dictionary["RE"];
			}
			string text4 = null;
			if (!string.IsNullOrWhiteSpace(text3))
			{
				text3 = text3.ToUpperInvariant();
				if (!guidToRes.ContainsKey(text3))
				{
					throw new ApplicationException("exported file has an object which references an re (by guid) which is not contained in the file");
				}
				text4 = guidToRes[text3].Name;
			}
			WipObject wipObject = new WipObject();
			string text5 = dictionary["Name"];
			string[] array = text5.Split(new char[] { '.' });
			if (array.Length == 3)
			{
				text5 = array[0] + "." + array[1];
			}
			wipObject.Name = text5;
			wipObject.RemoteEnvironmentName = text4;
			return wipObject;
		}

		// Token: 0x040023A4 RID: 9124
		private static RemoteEnvironmentClassFactory _recFactory = new RemoteEnvironmentClassFactory();
	}
}
