using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Xml;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x020005AB RID: 1451
	public class WipConfigurationUtilities
	{
		// Token: 0x060032D2 RID: 13010 RVA: 0x000A85B4 File Offset: 0x000A67B4
		public static string RemoteEnvironmentToXml(ref WipConfigurationSectionHandler wipConfig, string remoteEnvironmentName)
		{
			string text = null;
			foreach (object obj in wipConfig.RemoteEnvironments)
			{
				RemoteEnvironment remoteEnvironment = (RemoteEnvironment)obj;
				if (remoteEnvironment.Name == remoteEnvironmentName)
				{
					text = WipConfigurationUtilities.GetRemoteEnvironmentXml(remoteEnvironment, WipGeneratedSchemaType.Full);
					break;
				}
			}
			return text;
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000A8624 File Offset: 0x000A6824
		public static string AddRemoteEnvironmentFromXml(ref WipConfigurationSectionHandler wipConfig, string remoteEnvironmentXml)
		{
			RemoteEnvironment remoteEnvironment = new RemoteEnvironment();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(remoteEnvironmentXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			remoteEnvironment.Name = xmlDocument.SelectSingleNode("/remoteEnvironment/@name").Value;
			bool flag = false;
			do
			{
				flag = false;
				using (IEnumerator enumerator = wipConfig.RemoteEnvironments.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((RemoteEnvironment)enumerator.Current).Name == remoteEnvironment.Name)
						{
							remoteEnvironment.Name = "Copy of " + remoteEnvironment.Name;
							flag = true;
							break;
						}
					}
				}
			}
			while (flag);
			remoteEnvironment.IsDefault = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/@isDefault").Value);
			remoteEnvironment.CodePage = Convert.ToInt32(xmlDocument.SelectSingleNode("/remoteEnvironment/@codePage").Value);
			remoteEnvironment.Timeout = Convert.ToInt32(xmlDocument.SelectSingleNode("/remoteEnvironment/@timeout").Value);
			try
			{
				remoteEnvironment.DistributedProgramCall = new DistributedProgramCall
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/distributedProgramCall/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/distributedProgramCall/@ports").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/distributedProgramCall/@essoAffiliateApplication").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/distributedProgramCall/@securityFromClientContext").Value),
					UseSsl = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/distributedProgramCall/@useSsl").Value),
					CertificateCommonName = xmlDocument.SelectSingleNode("/remoteEnvironment/distributedProgramCall/@certificateCommonName").Value,
					ServerVerificationRequired = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/distributedProgramCall/@serverVerificationRequired").Value),
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.ElmLink = new ElmLink
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/elmLink/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/elmLink/@ports").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/elmLink/@essoAffiliateApplication").Value,
					RequestHeaderFormat = xmlDocument.SelectSingleNode("/remoteEnvironment/elmLink/@requestHeaderFormat").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/elmLink/@securityFromClientContext").Value),
					UseSsl = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/elmLink/@useSsl").Value),
					CertificateCommonName = xmlDocument.SelectSingleNode("/remoteEnvironment/elmLink/@certificateCommonName").Value,
					ServerVerificationRequired = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/elmLink/@serverVerificationRequired").Value),
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.ElmUserData = new ElmUserData
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/elmUserData/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/elmUserData/@ports").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/elmUserData/@essoAffiliateApplication").Value,
					RequestHeaderFormat = xmlDocument.SelectSingleNode("/remoteEnvironment/elmUserData/@requestHeaderFormat").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/elmUserData/@securityFromClientContext").Value),
					UseSsl = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/elmUserData/@useSsl").Value),
					CertificateCommonName = xmlDocument.SelectSingleNode("/remoteEnvironment/elmUserData/@certificateCommonName").Value,
					ServerVerificationRequired = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/elmUserData/@serverVerificationRequired").Value),
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.TrmLink = new TrmLink
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/trmLink/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/trmLink/@ports").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/trmLink/@essoAffiliateApplication").Value,
					RequestHeaderFormat = xmlDocument.SelectSingleNode("/remoteEnvironment/trmLink/@requestHeaderFormat").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/trmLink/@securityFromClientContext").Value),
					UseSsl = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/trmLink/@useSsl").Value),
					CertificateCommonName = xmlDocument.SelectSingleNode("/remoteEnvironment/trmLink/@certificateCommonName").Value,
					ServerVerificationRequired = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/trmLink/@serverVerificationRequired").Value),
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.TrmUserData = new TrmUserData
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/trmUserData/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/trmUserData/@ports").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/trmUserData/@essoAffiliateApplication").Value,
					RequestHeaderFormat = xmlDocument.SelectSingleNode("/remoteEnvironment/trmUserData/@requestHeaderFormat").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/trmUserData/@securityFromClientContext").Value),
					UseSsl = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/trmUserData/@useSsl").Value),
					CertificateCommonName = xmlDocument.SelectSingleNode("/remoteEnvironment/trmUserData/@certificateCommonName").Value,
					ServerVerificationRequired = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/trmUserData/@serverVerificationRequired").Value),
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.HttpLink = new HttpLink
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/httpLink/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/httpLink/@ports").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/httpLink/@essoAffiliateApplication").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/httpLink/@securityFromClientContext").Value),
					AliasTransactionId = xmlDocument.SelectSingleNode("/remoteEnvironment/httpLink/@aliasTransactionId").Value,
					AllowRedirects = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/httpLink/@allowRedirects").Value),
					UseSsl = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/httpLink/@useSsl").Value),
					MirrorProgram = xmlDocument.SelectSingleNode("/remoteEnvironment/httpLink/@mirrorProgram").Value,
					UserAgent = xmlDocument.SelectSingleNode("/remoteEnvironment/httpLink/@userAgent").Value,
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.HttpUserData = new HttpUserData
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/httpUserData/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/httpUserData/@ports").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/httpUserData/@essoAffiliateApplication").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/httpUserData/@securityFromClientContext").Value),
					AliasTransactionId = xmlDocument.SelectSingleNode("/remoteEnvironment/httpUserData/@aliasTransactionId").Value,
					AllowRedirects = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/httpUserData/@allowRedirects").Value),
					UseSsl = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/httpUserData/@useSsl").Value),
					UserAgent = xmlDocument.SelectSingleNode("/remoteEnvironment/httpUserData/@userAgent").Value,
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.SystemzSocketsLink = new SystemzSocketsLink
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/systemzSocketsLink/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/systemzSocketsLink/@ports").Value,
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.SystemzSocketsUserData = new SystemzSocketsUserData
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/systemzSocketsUserData/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/systemzSocketsUserData/@ports").Value,
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.SystemiSocketsUserData = new SystemiSocketsUserData
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/systemiSocketsUserData/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/systemiSocketsUserData/@ports").Value,
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.SnaLink = new SnaLink
				{
					RemoteLuName = xmlDocument.SelectSingleNode("/remoteEnvironment/snaLink/@remoteLuName").Value,
					LocalLuName = xmlDocument.SelectSingleNode("/remoteEnvironment/snaLink/@localLuName").Value,
					ModeName = xmlDocument.SelectSingleNode("/remoteEnvironment/snaLink/@modeName").Value,
					MirrorTransactionId = xmlDocument.SelectSingleNode("/remoteEnvironment/snaLink/@mirrorTransactionId").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/snaLink/@essoAffiliateApplication").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/snaLink/@securityFromClientContext").Value),
					AllowExplicitSyncPoint = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/snaLink/@allowExplicitSyncPoint").Value),
					OverrideSnaSourceTransactionProgram = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/snaLink/@overrideSnaSourceTransactionProgram").Value),
					SyncLevel2Supported = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/snaLink/@syncLevel2Supported").Value),
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.SnaUserData = new SnaUserData
				{
					RemoteLuName = xmlDocument.SelectSingleNode("/remoteEnvironment/snaUserData/@remoteLuName").Value,
					LocalLuName = xmlDocument.SelectSingleNode("/remoteEnvironment/snaUserData/@localLuName").Value,
					ModeName = xmlDocument.SelectSingleNode("/remoteEnvironment/snaUserData/@modeName").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/snaUserData/@essoAffiliateApplication").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/snaUserData/@securityFromClientContext").Value),
					SyncLevel2Supported = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/snaUserData/@syncLevel2Supported").Value),
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.ImsLu62 = new ImsLu62
				{
					RemoteLuName = xmlDocument.SelectSingleNode("/remoteEnvironment/imsLu62/@remoteLuName").Value,
					LocalLuName = xmlDocument.SelectSingleNode("/remoteEnvironment/imsLu62/@localLuName").Value,
					ModeName = xmlDocument.SelectSingleNode("/remoteEnvironment/imsLu62/@modeName").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/imsLu62/@essoAffiliateApplication").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/imsLu62/@securityFromClientContext").Value),
					SyncLevel2Supported = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/imsLu62/@syncLevel2Supported").Value),
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			try
			{
				remoteEnvironment.ImsConnect = new ImsConnect
				{
					IpAddress = xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@ipAddress").Value,
					Ports = xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@ports").Value,
					EssoAffiliateApplication = xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@essoAffiliateApplication").Value,
					SecurityFromClientContext = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@securityFromClientContext").Value),
					UseSsl = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@useSsl").Value),
					CertificateCommonName = xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@certificateCommonName").Value,
					ServerVerificationRequired = Convert.ToBoolean(xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@serverVerificationRequired").Value),
					ImsInboundHeaderFormat = xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@imsInboundHeaderFormat").Value,
					ImsSystemId = xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@imsSystemId").Value,
					ItocExitName = xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@itocExitName").Value,
					MfsModName = xmlDocument.SelectSingleNode("/remoteEnvironment/imsConnect/@mfsModName").Value,
					IsTypeDefined = true
				};
				wipConfig.RemoteEnvironments.AddRemoteEnvironment(remoteEnvironment);
				return remoteEnvironment.Name;
			}
			catch
			{
			}
			throw new Exception("Remote Environment type not recognized: " + remoteEnvironmentXml);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000A94BC File Offset: 0x000A76BC
		public static string WipObjectToXml(ref WipConfigurationSectionHandler wipConfig, string wipObjectName)
		{
			string text = null;
			foreach (object obj in wipConfig.WipObjects)
			{
				WipObject wipObject = (WipObject)obj;
				if (wipObject.Name == wipObjectName)
				{
					text = WipConfigurationUtilities.GetWipObject(wipObject, WipGeneratedSchemaType.Full);
					break;
				}
			}
			return text;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000A952C File Offset: 0x000A772C
		public static string AddWipObjectFromXml(ref WipConfigurationSectionHandler wipConfig, string wipObjectXml, string remoteEnvironmentXml)
		{
			WipObject wipObject = new WipObject();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(wipObjectXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			wipObject.Name = xmlDocument.SelectSingleNode("/object/@name").Value;
			wipObject.RemoteEnvironmentName = xmlDocument.SelectSingleNode("/object/@remoteEnvironmentName").Value;
			wipObject.RemoteEnvironmentTypeId = xmlDocument.SelectSingleNode("/object/@remoteEnvironmentTypeId").Value;
			foreach (object obj in wipConfig.WipObjects)
			{
				WipObject wipObject2 = (WipObject)obj;
				if (wipObject2.Name == wipObject.Name)
				{
					throw new Exception("WIP Object already exists: " + wipObject2.Name);
				}
			}
			if (!string.IsNullOrEmpty(remoteEnvironmentXml))
			{
				wipObject.RemoteEnvironmentName = WipConfigurationUtilities.AddRemoteEnvironmentFromXml(ref wipConfig, remoteEnvironmentXml);
			}
			wipConfig.WipObjects.AddWipObject(wipObject);
			return wipObject.Name;
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000A9658 File Offset: 0x000A7858
		public static string CacheToXml(ref WipConfigurationSectionHandler wipConfig)
		{
			return WipConfigurationUtilities.GetCacheXml(wipConfig.Cache, WipGeneratedSchemaType.Full);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000A9668 File Offset: 0x000A7868
		public static void UpdateCacheFromXml(ref WipConfigurationSectionHandler wipConfig, string cacheXml)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(cacheXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			wipConfig.Cache.CacheName = xmlDocument.SelectSingleNode("/cache/@cacheName").Value;
			wipConfig.Cache.Key = xmlDocument.SelectSingleNode("/cache/@key").Value;
			wipConfig.Cache.Region = xmlDocument.SelectSingleNode("/cache/@region").Value;
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000A96FE File Offset: 0x000A78FE
		public static string ReadOrderToXml(ref WipConfigurationSectionHandler wipConfig)
		{
			return WipConfigurationUtilities.GetReadOrderXml(wipConfig.ReadOrder, WipGeneratedSchemaType.Full);
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000A9710 File Offset: 0x000A7910
		public static void UpdateReadOrderFromXml(ref WipConfigurationSectionHandler wipConfig, string readOrderXml)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(readOrderXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			wipConfig.ReadOrder.appConfig = xmlDocument.SelectSingleNode("/readOrder/@appConfig").Value;
			wipConfig.ReadOrder.cache = xmlDocument.SelectSingleNode("/readOrder/@cache").Value;
			wipConfig.ReadOrder.registry = xmlDocument.SelectSingleNode("/readOrder/@registry").Value;
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000A97A6 File Offset: 0x000A79A6
		public static string ConversionBehaviorToXml(ref WipConfigurationSectionHandler wipConfig)
		{
			return WipConfigurationUtilities.GetConversionBehaviorXml(wipConfig.ConversionBehavior, WipGeneratedSchemaType.Full);
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000A97B8 File Offset: 0x000A79B8
		public static void UpdateConversionBehaviorFromXml(ref WipConfigurationSectionHandler wipConfig, string conversionBehaviorXml)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(conversionBehaviorXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			wipConfig.ConversionBehavior.AcceptAllInvalidNumerics = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@acceptAllInvalidNumerics").Value);
			wipConfig.ConversionBehavior.AcceptBadCOMP3Sign = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@acceptBadCOMP3Sign").Value);
			wipConfig.ConversionBehavior.AcceptNullPacked = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@acceptNullPacked").Value);
			wipConfig.ConversionBehavior.AcceptNullZoned = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@acceptNullZoned").Value);
			wipConfig.ConversionBehavior.AlwaysCheckForNull = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@alwaysCheckForNull").Value);
			wipConfig.ConversionBehavior.StringsAreNullTerminatedAndSpacePadded = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@stringsAreNullTerminatedAndSpacePadded").Value);
			wipConfig.ConversionBehavior.TrimTrailingNulls = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@trimTrailingNulls").Value);
			wipConfig.ConversionBehavior.ConvertReceivedStringsAsIs = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@convertReceivedStringsAsIs").Value);
			wipConfig.ConversionBehavior.AllowNullRedefines = Convert.ToBoolean(xmlDocument.SelectSingleNode("/conversionBehavior/@allowNullRedefines").Value);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000A9923 File Offset: 0x000A7B23
		public static string TiWipBehaviorToXml(ref WipConfigurationSectionHandler wipConfig)
		{
			return WipConfigurationUtilities.GetTiWipBehaviorXml(wipConfig.TiWipBehavior, WipGeneratedSchemaType.Full);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000A9934 File Offset: 0x000A7B34
		public static void UpdateTiWipBehaviorFromXml(ref WipConfigurationSectionHandler wipConfig, string tiWipBehaviorXml)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			XmlReader xmlReader = XmlReader.Create(new StringReader(tiWipBehaviorXml), xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(xmlReader);
			wipConfig.TiWipBehavior.CallAccountingProcessor = xmlDocument.SelectSingleNode("/tiWipBehavior/@callAccountingProcessor").Value;
			wipConfig.TiWipBehavior.NineCharacterImsTransactionId = Convert.ToBoolean(xmlDocument.SelectSingleNode("/tiWipBehavior/@nineCharacterImsTransactionId").Value);
			wipConfig.TiWipBehavior.SourceTransactionProgramNameOverride = Convert.ToBoolean(xmlDocument.SelectSingleNode("/tiWipBehavior/@sourceTransactionProgramNameOverride").Value);
			wipConfig.TiWipBehavior.UseSyncLevel1 = Convert.ToBoolean(xmlDocument.SelectSingleNode("/tiWipBehavior/@useSyncLevel1").Value);
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000A99F8 File Offset: 0x000A7BF8
		public static void WriteConfigurationToXmlFile(ref WipConfigurationSectionHandler wipConfig, string fileName, WipGeneratedSchemaType schemaType)
		{
			try
			{
				string text = WipConfigurationUtilities.ConfigurationFileToXml(ref wipConfig, schemaType);
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
				xmlReaderSettings.XmlResolver = null;
				XmlReader xmlReader = XmlReader.Create(new StringReader(text), xmlReaderSettings);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.XmlResolver = null;
				xmlDocument.Load(xmlReader);
				xmlDocument.Save(fileName);
				new ValidateConfigurationFile().ValidateConfigFile(fileName, "HostIntegrationTiWipConfiguration.xsd");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000A9A74 File Offset: 0x000A7C74
		public static StreamReader WriteConfigurationToXmlStream(ref WipConfigurationSectionHandler wipConfig, WipGeneratedSchemaType schemaType)
		{
			StreamReader streamReader;
			try
			{
				string text = WipConfigurationUtilities.ConfigurationFileToXml(ref wipConfig, schemaType);
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
				new ValidateConfigurationFile().ValidateConfigFile(streamReader, "HostIntegrationTiWipConfiguration.xsd");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return streamReader;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000A9B14 File Offset: 0x000A7D14
		private static string ConfigurationFileToXml(ref WipConfigurationSectionHandler wipConfig, WipGeneratedSchemaType schemaType)
		{
			return "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration><configSections>" + wipConfig.SectionXml + "</configSections>" + WipConfigurationUtilities.TiWipElementXml(wipConfig, schemaType) + "</configuration>";
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000A9B44 File Offset: 0x000A7D44
		internal static string TiWipElementInnerXml(WipConfigurationSectionHandler wipConfig, WipGeneratedSchemaType schemaType)
		{
			string text = WipConfigurationUtilities.GetReadOrderXml(wipConfig.ReadOrder, schemaType);
			text += WipConfigurationUtilities.GetCacheXml(wipConfig.Cache, schemaType);
			text += "<remoteEnvironments>";
			foreach (object obj in wipConfig.RemoteEnvironments)
			{
				RemoteEnvironment remoteEnvironment = (RemoteEnvironment)obj;
				text += WipConfigurationUtilities.GetRemoteEnvironmentXml(remoteEnvironment, schemaType);
			}
			text += "</remoteEnvironments>";
			if (schemaType == WipGeneratedSchemaType.Full || (schemaType == WipGeneratedSchemaType.Minimal && wipConfig.WipObjects.Count > 0))
			{
				text += "<objects>";
				foreach (object obj2 in wipConfig.WipObjects)
				{
					WipObject wipObject = (WipObject)obj2;
					text += WipConfigurationUtilities.GetWipObject(wipObject, schemaType);
				}
				text += "</objects>";
			}
			text += WipConfigurationUtilities.GetConversionBehaviorXml(wipConfig.ConversionBehavior, schemaType);
			text += WipConfigurationUtilities.GetTiWipBehaviorXml(wipConfig.TiWipBehavior, schemaType);
			return text;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000A9C84 File Offset: 0x000A7E84
		internal static string TiWipElementXml(WipConfigurationSectionHandler wipConfig, WipGeneratedSchemaType schemaType)
		{
			return "<hostIntegration.ti.wip xmlns=\"http://schemas.microsoft.com/his/Config/TiWip/2013\">" + WipConfigurationUtilities.TiWipElementInnerXml(wipConfig, schemaType) + "</hostIntegration.ti.wip>";
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000A9CA4 File Offset: 0x000A7EA4
		private static string GetWipObject(WipObject wipObject, WipGeneratedSchemaType schemaType)
		{
			string text = "<object ";
			PropertyInformation propertyInformation = wipObject.ElementInformation.Properties["name"];
			if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
			{
				text = text + "name=\"" + wipObject.Name + "\" ";
			}
			propertyInformation = wipObject.ElementInformation.Properties["remoteEnvironmentName"];
			if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
			{
				text = text + "remoteEnvironmentName=\"" + wipObject.RemoteEnvironmentName + "\" ";
			}
			propertyInformation = wipObject.ElementInformation.Properties["remoteEnvironmentTypeId"];
			if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
			{
				text = text + "remoteEnvironmentTypeId=\"" + wipObject.RemoteEnvironmentTypeId + "\" ";
			}
			return text + "/>";
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000A9DEC File Offset: 0x000A7FEC
		private static string GetTiWipBehaviorXml(TiWipBehavior tiWipBehavior, WipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == WipGeneratedSchemaType.Minimal)
			{
				foreach (object obj in tiWipBehavior.ElementInformation.Properties)
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
							if (!(name == "Boolean"))
							{
								goto IL_00AE;
							}
							if ((bool)propertyInformation.DefaultValue != (bool)propertyInformation.Value)
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
			if (schemaType == WipGeneratedSchemaType.Full || flag)
			{
				text = "<tiWipBehavior ";
				PropertyInformation propertyInformation2 = tiWipBehavior.ElementInformation.Properties["callAccountingProcessor"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "callAccountingProcessor=\"" + tiWipBehavior.CallAccountingProcessor + "\" ";
				}
				propertyInformation2 = tiWipBehavior.ElementInformation.Properties["nineCharacterImsTransactionId"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "nineCharacterImsTransactionId=\"" + tiWipBehavior.NineCharacterImsTransactionId.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = tiWipBehavior.ElementInformation.Properties["sourceTransactionProgramNameOverride"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "sourceTransactionProgramNameOverride=\"" + tiWipBehavior.SourceTransactionProgramNameOverride.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = tiWipBehavior.ElementInformation.Properties["useSyncLevel1"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "useSyncLevel1=\"" + tiWipBehavior.UseSyncLevel1.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000AA0A8 File Offset: 0x000A82A8
		private static string GetConversionBehaviorXml(ConversionBehavior conversionBehavior, WipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == WipGeneratedSchemaType.Minimal)
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
			if (schemaType == WipGeneratedSchemaType.Full || flag)
			{
				text = "<conversionBehavior ";
				PropertyInformation propertyInformation2 = conversionBehavior.ElementInformation.Properties["acceptAllInvalidNumerics"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "acceptAllInvalidNumerics=\"" + conversionBehavior.AcceptAllInvalidNumerics.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["acceptBadCOMP3Sign"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "acceptBadCOMP3Sign=\"" + conversionBehavior.AcceptBadCOMP3Sign.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["acceptNullPacked"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "acceptNullPacked=\"" + conversionBehavior.AcceptNullPacked.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["acceptNullZoned"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "acceptNullZoned=\"" + conversionBehavior.AcceptNullZoned.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["alwaysCheckForNull"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "alwaysCheckForNull=\"" + conversionBehavior.AlwaysCheckForNull.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["stringsAreNullTerminatedAndSpacePadded"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "stringsAreNullTerminatedAndSpacePadded=\"" + conversionBehavior.StringsAreNullTerminatedAndSpacePadded.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["trimTrailingNulls"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "trimTrailingNulls=\"" + conversionBehavior.TrimTrailingNulls.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["convertReceivedStringsAsIs"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "convertReceivedStringsAsIs=\"" + conversionBehavior.ConvertReceivedStringsAsIs.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = conversionBehavior.ElementInformation.Properties["allowNullRedefines"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (bool)propertyInformation2.Value != (bool)propertyInformation2.DefaultValue))
				{
					text = text + "allowNullRedefines=\"" + conversionBehavior.AllowNullRedefines.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000AA500 File Offset: 0x000A8700
		private static string GetReadOrderXml(ReadOrder readOrder, WipGeneratedSchemaType schemaType)
		{
			bool flag = false;
			if (schemaType == WipGeneratedSchemaType.Minimal)
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
			if (schemaType == WipGeneratedSchemaType.Full || flag)
			{
				PropertyInformation propertyInformation2 = readOrder.ElementInformation.Properties["cache"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "cache=\"" + readOrder.Cache.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation2 = readOrder.ElementInformation.Properties["registry"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "registry=\"" + readOrder.Registry.ToString().ToLowerInvariant() + "\" ";
				}
			}
			return text + "/>";
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000AA6B8 File Offset: 0x000A88B8
		private static string GetCacheXml(Cache cache, WipGeneratedSchemaType schemaType)
		{
			string text = null;
			bool flag = false;
			if (schemaType == WipGeneratedSchemaType.Minimal)
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
			if (schemaType == WipGeneratedSchemaType.Full || flag)
			{
				text = "<cache ";
				PropertyInformation propertyInformation2 = cache.ElementInformation.Properties["cacheName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "cacheName=\"" + cache.CacheName + "\" ";
				}
				propertyInformation2 = cache.ElementInformation.Properties["key"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "key=\"" + cache.Key + "\" ";
				}
				propertyInformation2 = cache.ElementInformation.Properties["region"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation2.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation2.IsRequired && (string)propertyInformation2.Value != (string)propertyInformation2.DefaultValue))
				{
					text = text + "region=\"" + cache.Region + "\" ";
				}
				text += "/>";
			}
			return text;
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000AA8F8 File Offset: 0x000A8AF8
		private static string GetRemoteEnvironmentXml(RemoteEnvironment remoteEnvironment, WipGeneratedSchemaType schemaType)
		{
			string text = null;
			WipConfigurationUtilities.AddRemoteEnvironmentAttributes(ref text, remoteEnvironment);
			switch (remoteEnvironment.RemoteEnvironmentType)
			{
			case RemoteEnvironmentType.ElmLink:
			{
				text += "<elmLink ";
				PropertyInformation propertyInformation = remoteEnvironment.ElmLink.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.ElmLink.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.ElmLink.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.ElmLink.Ports + "\" ";
				}
				propertyInformation = remoteEnvironment.ElmLink.ElementInformation.Properties["requestHeaderFormat"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "requestHeaderFormat=\"" + remoteEnvironment.ElmLink.RequestHeaderFormat + "\" ";
				}
				propertyInformation = remoteEnvironment.ElmLink.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.ElmLink.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.ElmLink.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.ElmLink.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.ElmUserData:
			{
				text += "<elmUserData ";
				PropertyInformation propertyInformation = remoteEnvironment.ElmUserData.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.ElmUserData.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.ElmUserData.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.ElmUserData.Ports + "\" ";
				}
				propertyInformation = remoteEnvironment.ElmUserData.ElementInformation.Properties["requestHeaderFormat"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "requestHeaderFormat=\"" + remoteEnvironment.ElmUserData.RequestHeaderFormat + "\" ";
				}
				propertyInformation = remoteEnvironment.ElmUserData.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.ElmUserData.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.ElmUserData.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.ElmUserData.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.TrmLink:
			{
				text += "<trmLink ";
				PropertyInformation propertyInformation = remoteEnvironment.TrmLink.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.TrmLink.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.TrmLink.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.TrmLink.Ports + "\" ";
				}
				propertyInformation = remoteEnvironment.TrmLink.ElementInformation.Properties["requestHeaderFormat"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "requestHeaderFormat=\"" + remoteEnvironment.TrmLink.RequestHeaderFormat + "\" ";
				}
				propertyInformation = remoteEnvironment.TrmLink.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.TrmLink.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.TrmLink.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.TrmLink.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.TrmUserData:
			{
				text += "<trmUserData ";
				PropertyInformation propertyInformation = remoteEnvironment.TrmUserData.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.TrmUserData.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.TrmUserData.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.TrmUserData.Ports + "\" ";
				}
				propertyInformation = remoteEnvironment.TrmUserData.ElementInformation.Properties["requestHeaderFormat"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "requestHeaderFormat=\"" + remoteEnvironment.TrmUserData.RequestHeaderFormat + "\" ";
				}
				propertyInformation = remoteEnvironment.TrmUserData.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.TrmUserData.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.TrmUserData.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.TrmUserData.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.HttpLink:
			{
				text += "<httpLink ";
				PropertyInformation propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.HttpLink.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.HttpLink.Ports + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["useSsl"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "useSsl=\"" + remoteEnvironment.HttpLink.UseSsl.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["serverVerificationRequired"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "serverVerificationRequired=\"" + remoteEnvironment.HttpLink.ServerVerificationRequired.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["aliasTransactionId"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "aliasTransactionId=\"" + remoteEnvironment.HttpLink.AliasTransactionId + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["allowRedirects"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "allowRedirects=\"" + remoteEnvironment.HttpLink.AllowRedirects.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["mirrorProgram"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "mirrorProgram=\"" + remoteEnvironment.HttpLink.MirrorProgram + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["userAgent"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "userAgent=\"" + remoteEnvironment.HttpLink.UserAgent + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.HttpLink.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.HttpLink.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.HttpUserData:
			{
				text += "<httpUserData ";
				PropertyInformation propertyInformation = remoteEnvironment.HttpUserData.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.HttpUserData.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpUserData.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.HttpUserData.Ports + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpUserData.ElementInformation.Properties["useSsl"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "useSsl=\"" + remoteEnvironment.HttpUserData.UseSsl.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpUserData.ElementInformation.Properties["serverVerificationRequired"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "serverVerificationRequired=\"" + remoteEnvironment.HttpUserData.ServerVerificationRequired.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["aliasTransactionId"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "aliasTransactionId=\"" + remoteEnvironment.HttpUserData.AliasTransactionId + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpUserData.ElementInformation.Properties["allowRedirects"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "allowRedirects=\"" + remoteEnvironment.HttpUserData.AllowRedirects.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpLink.ElementInformation.Properties["userAgent"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "userAgent=\"" + remoteEnvironment.HttpUserData.UserAgent + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpUserData.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.HttpUserData.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.HttpUserData.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.HttpUserData.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.DistributedProgramCall:
			{
				text += "<distributedProgramCall ";
				PropertyInformation propertyInformation = remoteEnvironment.DistributedProgramCall.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.DistributedProgramCall.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.DistributedProgramCall.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.DistributedProgramCall.Ports + "\" ";
				}
				propertyInformation = remoteEnvironment.DistributedProgramCall.ElementInformation.Properties["useSsl"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "useSsl=\"" + remoteEnvironment.DistributedProgramCall.UseSsl.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.DistributedProgramCall.ElementInformation.Properties["certificateCommonName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "certificateCommonName=\"" + remoteEnvironment.DistributedProgramCall.CertificateCommonName + "\" ";
				}
				propertyInformation = remoteEnvironment.DistributedProgramCall.ElementInformation.Properties["serverVerificationRequired"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "serverVerificationRequired=\"" + remoteEnvironment.DistributedProgramCall.ServerVerificationRequired.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.DistributedProgramCall.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.DistributedProgramCall.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.DistributedProgramCall.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.DistributedProgramCall.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.ImsConnect:
			{
				text += "<imsConnect ";
				PropertyInformation propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.ImsConnect.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.ImsConnect.Ports + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["useSsl"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "useSsl=\"" + remoteEnvironment.ImsConnect.UseSsl.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["certificateCommonName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "certificateCommonName=\"" + remoteEnvironment.ImsConnect.CertificateCommonName + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["serverVerificationRequired"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "serverVerificationRequired=\"" + remoteEnvironment.ImsConnect.ServerVerificationRequired.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["imsInboundHeaderFormat"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "imsInboundHeaderFormat=\"" + remoteEnvironment.ImsConnect.ImsInboundHeaderFormat + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["imsSystemId"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "imsSystemId=\"" + remoteEnvironment.ImsConnect.ImsSystemId + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["itocExitName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "itocExitName=\"" + remoteEnvironment.ImsConnect.ItocExitName + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["mfsModName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "mfsModName=\"" + remoteEnvironment.ImsConnect.MfsModName + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.ImsConnect.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsConnect.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.ImsConnect.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.SnaLink:
			{
				text += "<snaLink ";
				PropertyInformation propertyInformation = remoteEnvironment.SnaLink.ElementInformation.Properties["localLuName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "localLuName=\"" + remoteEnvironment.SnaLink.LocalLuName + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaLink.ElementInformation.Properties["remoteLuName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "remoteLuName=\"" + remoteEnvironment.SnaLink.RemoteLuName + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaLink.ElementInformation.Properties["modeName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "modeName=\"" + remoteEnvironment.SnaLink.ModeName + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaLink.ElementInformation.Properties["mirrorTransactionId"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "mirrorTransactionId=\"" + remoteEnvironment.SnaLink.MirrorTransactionId + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaLink.ElementInformation.Properties["allowExplicitSyncPoint"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "allowExplicitSyncPoint=\"" + remoteEnvironment.SnaLink.AllowExplicitSyncPoint.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaLink.ElementInformation.Properties["overrideSnaSourceTransactionProgram"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "overrideSnaSourceTransactionProgram=\"" + remoteEnvironment.SnaLink.OverrideSnaSourceTransactionProgram.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaLink.ElementInformation.Properties["syncLevel2Supported"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "syncLevel2Supported=\"" + remoteEnvironment.SnaLink.SyncLevel2Supported.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaLink.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.SnaLink.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaLink.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.SnaLink.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.SnaUserData:
			{
				text += "<snaUserData ";
				PropertyInformation propertyInformation = remoteEnvironment.SnaUserData.ElementInformation.Properties["localLuName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "localLuName=\"" + remoteEnvironment.SnaUserData.LocalLuName + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaUserData.ElementInformation.Properties["remoteLuName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "remoteLuName=\"" + remoteEnvironment.SnaUserData.RemoteLuName + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaUserData.ElementInformation.Properties["modeName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "modeName=\"" + remoteEnvironment.SnaUserData.ModeName + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaUserData.ElementInformation.Properties["syncLevel2Supported"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "syncLevel2Supported=\"" + remoteEnvironment.SnaUserData.SyncLevel2Supported.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaUserData.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.SnaUserData.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.SnaUserData.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.SnaUserData.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.ImsLu62:
			{
				text += "<imsLu62 ";
				PropertyInformation propertyInformation = remoteEnvironment.ImsLu62.ElementInformation.Properties["localLuName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "localLuName=\"" + remoteEnvironment.ImsLu62.LocalLuName + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsLu62.ElementInformation.Properties["remoteLuName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "remoteLuName=\"" + remoteEnvironment.ImsLu62.RemoteLuName + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsLu62.ElementInformation.Properties["modeName"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "modeName=\"" + remoteEnvironment.ImsLu62.ModeName + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsLu62.ElementInformation.Properties["syncLevel2Supported"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "syncLevel2Supported=\"" + remoteEnvironment.ImsLu62.SyncLevel2Supported.ToString().ToLowerInvariant() + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsLu62.ElementInformation.Properties["essoAffiliateApplication"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "essoAffiliateApplication=\"" + remoteEnvironment.ImsLu62.EssoAffiliateApplication + "\" ";
				}
				propertyInformation = remoteEnvironment.ImsLu62.ElementInformation.Properties["securityFromClientContext"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (bool)propertyInformation.Value != (bool)propertyInformation.DefaultValue))
				{
					text = text + "securityFromClientContext=\"" + remoteEnvironment.ImsLu62.SecurityFromClientContext.ToString().ToLowerInvariant() + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.SystemzSocketsLink:
			{
				text += "<systemzSocketsLink ";
				PropertyInformation propertyInformation = remoteEnvironment.SystemzSocketsLink.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.SystemzSocketsLink.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.SystemzSocketsLink.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.SystemzSocketsLink.Ports + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.SystemzSocketsUserData:
			{
				text += "<systemzSocketsUserData ";
				PropertyInformation propertyInformation = remoteEnvironment.SystemzSocketsUserData.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.SystemzSocketsUserData.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.SystemzSocketsUserData.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.SystemzSocketsUserData.Ports + "\" ";
				}
				text += "/>";
				break;
			}
			case RemoteEnvironmentType.SystemiSocketsUserData:
			{
				text += "<systemiSocketsUserData ";
				PropertyInformation propertyInformation = remoteEnvironment.SystemiSocketsUserData.ElementInformation.Properties["ipAddress"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ipAddress=\"" + remoteEnvironment.SystemiSocketsUserData.IpAddress + "\" ";
				}
				propertyInformation = remoteEnvironment.SystemiSocketsUserData.ElementInformation.Properties["ports"];
				if (schemaType == WipGeneratedSchemaType.Full || propertyInformation.IsRequired || (schemaType == WipGeneratedSchemaType.Minimal && !propertyInformation.IsRequired && (string)propertyInformation.Value != (string)propertyInformation.DefaultValue))
				{
					text = text + "ports=\"" + remoteEnvironment.SystemiSocketsUserData.Ports + "\" ";
				}
				text += "/>";
				break;
			}
			}
			text += "</remoteEnvironment>";
			return text;
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000ACEB0 File Offset: 0x000AB0B0
		private static void AddRemoteEnvironmentAttributes(ref string wipRemoteEnvironmentAttributesXml, RemoteEnvironment remoteEnvironment)
		{
			wipRemoteEnvironmentAttributesXml = string.Concat(new string[]
			{
				wipRemoteEnvironmentAttributesXml,
				"<remoteEnvironment name=\"",
				remoteEnvironment.Name,
				"\" isDefault=\"",
				remoteEnvironment.IsDefault.ToString().ToLowerInvariant(),
				"\" codePage=\"",
				remoteEnvironment.CodePage.ToString(),
				"\" timeout=\"",
				remoteEnvironment.Timeout.ToString(),
				"\" >"
			});
		}
	}
}
