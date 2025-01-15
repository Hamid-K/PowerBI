using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200003C RID: 60
	internal class RSConfigurationFileManager : RSConfigurationManager
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001E1 RID: 481 RVA: 0x00008124 File Offset: 0x00006324
		// (remove) Token: 0x060001E2 RID: 482 RVA: 0x0000815C File Offset: 0x0000635C
		public override event ConfigurationChangeEventHandler Changed;

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00004F49 File Offset: 0x00003149
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00008191 File Offset: 0x00006391
		public override bool EnableRaisingEvents
		{
			get
			{
				return false;
			}
			set
			{
				RSConfigurationFileManager.m_Tracer.Assert(false, "Cannot enable raising events from RSConfigurationFileManager.");
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000081A4 File Offset: 0x000063A4
		private void OnChanged(ConfigurationChangeEventArgs e, RSConfigurationManager source)
		{
			if (this.Changed != null)
			{
				try
				{
					this.Changed(source, e);
				}
				catch (AppDomainUnloadedException)
				{
				}
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000081DC File Offset: 0x000063DC
		public override void ChangeConfiguration(ConfigurationPropertyBag properties, bool resetProperties, RSConfigurationManager source)
		{
			this.m_config.Load(properties, resetProperties);
			this.OnChanged(new ConfigurationChangeEventArgs(properties, resetProperties), source);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000081FC File Offset: 0x000063FC
		public RSConfigurationFileManager(string configFilePath)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			if (!string.IsNullOrWhiteSpace(configFilePath))
			{
				text2 = Path.GetDirectoryName(configFilePath);
				text = Path.GetFileName(configFilePath);
			}
			this.Init(text, text2);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000825A File Offset: 0x0000645A
		public RSConfigurationFileManager(string configFileName, string configLocation)
		{
			this.Init(configFileName, configLocation);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000828C File Offset: 0x0000648C
		private void Init(string configFileName, string configLocation)
		{
			this.m_configFileName = configFileName;
			this.m_configLocation = configLocation;
			this.m_config = new RSConfiguration(this.m_configFileName, this.m_configLocation);
			if (this.m_configLocation != null && this.m_configFileName != null && Directory.Exists(this.m_configLocation))
			{
				this.m_properties = this.LoadConfiguration();
				this.m_config.Load(this.m_properties, true);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000082FC File Offset: 0x000064FC
		public RSConfigurationFileManager(XmlDocument xmlConfiguration)
		{
			this.m_config = new RSConfiguration(string.Empty, string.Empty);
			this.m_properties = this.ParseDocument(xmlConfiguration);
			this.m_config.Validate(this.m_properties);
			this.m_config.Load(this.m_properties, true);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008378 File Offset: 0x00006578
		protected virtual ConfigurationPropertyBag LoadConfiguration()
		{
			ConfigurationPropertyBag configurationPropertyBag = null;
			try
			{
				configurationPropertyBag = this.LoadDocument();
			}
			catch (Exception ex)
			{
				if (RSConfigurationFileManager.m_Tracer.TraceError)
				{
					RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Error loading configuration file: {0}", new object[] { ex.Message });
				}
				throw new ServerConfigurationErrorException(ex, null);
			}
			return configurationPropertyBag;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000083D8 File Offset: 0x000065D8
		internal ConfigurationPropertyBag LoadDocument()
		{
			XmlDocument xmlConfiguration = new XmlDocument();
			RevertImpersonationContext.Run(delegate
			{
				int num = 3;
				bool flag = false;
				string text = Path.Combine(this.m_configLocation, this.m_configFileName);
				for (int i = 0; i < num; i++)
				{
					try
					{
						Thread.Sleep(100);
						if (File.Exists(text))
						{
							XmlUtil.SafeOpenXmlDocumentFile(xmlConfiguration, text);
							flag = true;
						}
						break;
					}
					catch (IOException)
					{
					}
				}
				if (!flag)
				{
					RSEventLog.Current.WriteError(Event.ConfigFileNotFound, new object[]
					{
						RSEventLog.Current.SourceName,
						this.m_configFileName
					});
					throw new ServerConfigurationErrorException(null, "Could not find config file at '" + text + "'");
				}
			});
			ConfigurationPropertyBag configurationPropertyBag = this.ParseDocument(xmlConfiguration);
			this.m_config.Validate(configurationPropertyBag);
			return configurationPropertyBag;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008428 File Offset: 0x00006628
		private ConfigurationPropertyBag ParseDocument(XmlDocument xmlConfiguration)
		{
			if (xmlConfiguration.DocumentElement.Name != "Configuration")
			{
				RSConfigurationFileManager.ThrowInvalidFormat(xmlConfiguration.DocumentElement.Name);
			}
			ConfigurationPropertyBag configurationPropertyBag = new ConfigurationPropertyBag();
			foreach (object obj in xmlConfiguration.DocumentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string innerText = xmlNode.InnerText;
				string name = xmlNode.Name;
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 2079568635U)
				{
					if (num <= 1397601249U)
					{
						if (num <= 841511370U)
						{
							if (num != 49567391U)
							{
								if (num == 841511370U)
								{
									if (name == "Dsn")
									{
										configurationPropertyBag.Add(ConfigurationProperty.Dsn, innerText);
									}
								}
							}
							else if (name == "InstanceId")
							{
								configurationPropertyBag.Add(ConfigurationProperty.InstanceId, innerText);
							}
						}
						else if (num != 987428440U)
						{
							if (num == 1397601249U)
							{
								if (name == "URLReservations")
								{
									ConfigurationPropertyInfo configurationPropertyInfo = new ConfigurationPropertyInfo();
									configurationPropertyInfo.Value = this.ParseUrlReservations(xmlNode);
									if (configurationPropertyInfo.Value != null)
									{
										configurationPropertyBag.Add(ConfigurationProperty.UrlConfiguration, configurationPropertyInfo);
									}
								}
							}
						}
						else if (name == "MapTileServerConfiguration")
						{
							MapTileServerConfiguration.Parse(xmlNode, configurationPropertyBag);
						}
					}
					else if (num <= 1958077276U)
					{
						if (num != 1778099637U)
						{
							if (num == 1958077276U)
							{
								if (name == "RDLSandboxing")
								{
									RDLSandboxingConfiguration.Parse(xmlNode, configurationPropertyBag);
								}
							}
						}
						else if (name == "InstallationIDWebApp")
						{
							configurationPropertyBag.Add(ConfigurationProperty.InstallationIDWebApp, innerText);
						}
					}
					else if (num != 2073288530U)
					{
						if (num == 2079568635U)
						{
							if (name == "UI")
							{
								RSConfigurationFileManager.ParseUIConfiguration(xmlNode, configurationPropertyBag);
							}
						}
					}
					else if (name == "HTMLViewerStyleSheet")
					{
						configurationPropertyBag.Add(ConfigurationProperty.DefaultViewerStyleSheet, innerText);
					}
				}
				else if (num <= 2758440520U)
				{
					if (num <= 2646845972U)
					{
						if (num != 2443586712U)
						{
							if (num == 2646845972U)
							{
								if (name == "Add")
								{
									RSConfigurationFileManager.ParseSetting(xmlNode, configurationPropertyBag);
								}
							}
						}
						else if (name == "InstallationID")
						{
							configurationPropertyBag.Add(ConfigurationProperty.InstallationID, innerText);
						}
					}
					else if (num != 2662131947U)
					{
						if (num == 2758440520U)
						{
							if (name == "LogonDomain")
							{
								configurationPropertyBag.Add(ConfigurationProperty.LogonDomain, innerText, "LogonDomain");
							}
						}
					}
					else if (name == "Extensions")
					{
						RSConfigurationFileManager.ParseExtensions(xmlNode, configurationPropertyBag);
					}
				}
				else if (num <= 3173861841U)
				{
					if (num != 3057895172U)
					{
						if (num == 3173861841U)
						{
							if (name == "ConnectionType")
							{
								configurationPropertyBag.Add(ConfigurationProperty.ConnectionType, innerText);
							}
						}
					}
					else if (name == "Service")
					{
						RSConfigurationFileManager.ParseServiceConfiguration(xmlNode, configurationPropertyBag);
					}
				}
				else if (num != 3658716948U)
				{
					if (num != 3693709087U)
					{
						if (num == 4165494657U)
						{
							if (name == "Authentication")
							{
								this.ParseAuthentication(xmlNode, configurationPropertyBag);
							}
						}
					}
					else if (name == "LogonUser")
					{
						configurationPropertyBag.Add(ConfigurationProperty.LogonUser, innerText, "LogonUser");
					}
				}
				else if (name == "LogonCred")
				{
					configurationPropertyBag.Add(ConfigurationProperty.LogonCred, innerText, "LogonCred");
				}
			}
			return configurationPropertyBag;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000886C File Offset: 0x00006A6C
		private static void ParseExtensions(XmlNode child, ConfigurationPropertyBag properties)
		{
			ConfigurationPropertyInfo configurationPropertyInfo = new ConfigurationPropertyInfo();
			ExtensionsConfiguration extensionsConfiguration = new ExtensionsConfiguration();
			extensionsConfiguration.ParseXML(child);
			configurationPropertyInfo.Value = extensionsConfiguration;
			properties.Add(ConfigurationProperty.Extensions, configurationPropertyInfo);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000889C File Offset: 0x00006A9C
		private static void ParseSetting(XmlNode node, ConfigurationPropertyBag properties)
		{
			XmlAttribute xmlAttribute = node.Attributes["Key"];
			XmlAttribute xmlAttribute2 = node.Attributes["Value"];
			if (xmlAttribute == null || xmlAttribute2 == null)
			{
				new Exception(ErrorStrings.InvalidKeyValue);
			}
			string value = xmlAttribute.Value;
			ConfigurationProperty configurationProperty = ConfigurationProperty.None;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(value);
			if (num <= 2432634228U)
			{
				if (num <= 1423830291U)
				{
					if (num <= 1270204537U)
					{
						if (num != 1077838016U)
						{
							if (num != 1148380456U)
							{
								if (num == 1270204537U)
								{
									if (value == "MaxScheduleWait")
									{
										configurationProperty = ConfigurationProperty.MaxScheduleWait;
										goto IL_0522;
									}
								}
							}
							else if (value == "RunningRequestsScavengerCycle")
							{
								configurationProperty = ConfigurationProperty.RunningRequestsScavengerCycle;
								goto IL_0522;
							}
						}
						else if (value == "WatsonFlags")
						{
							configurationProperty = ConfigurationProperty.WatsonFlags;
							goto IL_0522;
						}
					}
					else if (num != 1346772848U)
					{
						if (num != 1375497109U)
						{
							if (num == 1423830291U)
							{
								if (value == "ProcessRecycleOptions")
								{
									goto IL_0522;
								}
							}
						}
						else if (value == "DisplayErrorLink")
						{
							configurationProperty = ConfigurationProperty.DisplayErrorLink;
							goto IL_0522;
						}
					}
					else if (value == "CleanupCycleMinutes")
					{
						configurationProperty = ConfigurationProperty.CleanupCycleMinutes;
						goto IL_0522;
					}
				}
				else if (num <= 1613300743U)
				{
					if (num != 1442973662U)
					{
						if (num != 1444061756U)
						{
							if (num == 1613300743U)
							{
								if (value == "WatsonDumpExcludeIfContainsExceptions")
								{
									configurationProperty = ConfigurationProperty.WatsonDumpExcludeIfContainsExceptions;
									goto IL_0522;
								}
							}
						}
						else if (value == "WatsonDumpOnExceptions")
						{
							configurationProperty = ConfigurationProperty.WatsonDumpOnExceptions;
							goto IL_0522;
						}
					}
					else if (value == "SecureConnectionLevel")
					{
						configurationProperty = ConfigurationProperty.SecureConnectionRequired;
						goto IL_0522;
					}
				}
				else if (num <= 1778160689U)
				{
					if (num != 1683888478U)
					{
						if (num == 1778160689U)
						{
							if (value == "WebServiceUseFileShareStorage")
							{
								configurationProperty = ConfigurationProperty.WebServiceUseFileShareStorage;
								goto IL_0522;
							}
						}
					}
					else if (value == "AlertingCleanupCycleMinutes")
					{
						configurationProperty = ConfigurationProperty.AlertingCleanupCycleMinutes;
						goto IL_0522;
					}
				}
				else if (num != 1986206910U)
				{
					if (num == 2432634228U)
					{
						if (value == "ConnectionTimeout")
						{
							configurationProperty = ConfigurationProperty.ConnectionTimeout;
							goto IL_0522;
						}
					}
				}
				else if (value == "RunningRequestsDbCycle")
				{
					configurationProperty = ConfigurationProperty.RunningRequestsDbCycle;
					goto IL_0522;
				}
			}
			else if (num <= 3673114931U)
			{
				if (num <= 3292542973U)
				{
					if (num != 2586894946U)
					{
						if (num != 2725411839U)
						{
							if (num == 3292542973U)
							{
								if (value == "RequestCacheSlots")
								{
									configurationProperty = ConfigurationProperty.RequestCacheSlots;
									goto IL_0522;
								}
							}
						}
						else if (value == "DatabaseCleanupTimeout")
						{
							configurationProperty = ConfigurationProperty.DatabaseCleanupTimeout;
							goto IL_0522;
						}
					}
					else if (value == "AlertingExecutionLogCleanupMinutes")
					{
						configurationProperty = ConfigurationProperty.AlertingExecutionLogCleanupMinutes;
						goto IL_0522;
					}
				}
				else if (num <= 3469089499U)
				{
					if (num != 3422991485U)
					{
						if (num == 3469089499U)
						{
							if (value == "MaxActiveReqForOneUser")
							{
								configurationProperty = ConfigurationProperty.MaxActiveReqForOneUser;
								goto IL_0522;
							}
						}
					}
					else if (value == "RunningRequestsAge")
					{
						configurationProperty = ConfigurationProperty.RunningRequestsAge;
						goto IL_0522;
					}
				}
				else if (num != 3648526691U)
				{
					if (num == 3673114931U)
					{
						if (value == "UsernameSIDRefreshMinutes")
						{
							configurationProperty = ConfigurationProperty.UsernameSIDRefreshMinutes;
							goto IL_0522;
						}
					}
				}
				else if (value == "DisableSecureFormsAuthenticationCookie")
				{
					configurationProperty = ConfigurationProperty.DisableSecureFormsAuthenticationCookie;
					goto IL_0522;
				}
			}
			else if (num <= 3777694503U)
			{
				if (num != 3729573746U)
				{
					if (num != 3739879821U)
					{
						if (num == 3777694503U)
						{
							if (value == "DatabaseCleanupBatchFactor")
							{
								configurationProperty = ConfigurationProperty.DatabaseCleanupBatchFactor;
								goto IL_0522;
							}
						}
					}
					else if (value == "ProcessTimeout")
					{
						configurationProperty = ConfigurationProperty.ProcessTimeout;
						goto IL_0522;
					}
				}
				else if (value == "AlertingDataCleanupMinutes")
				{
					configurationProperty = ConfigurationProperty.AlertingDataCleanupMinutes;
					goto IL_0522;
				}
			}
			else if (num <= 4011873143U)
			{
				if (num != 3932769000U)
				{
					if (num == 4011873143U)
					{
						if (value == "DatabaseQueryTimeout")
						{
							configurationProperty = ConfigurationProperty.DatabaseQueryTimeout;
							goto IL_0522;
						}
					}
				}
				else if (value == "AlertingMaxDataRetentionDays")
				{
					configurationProperty = ConfigurationProperty.AlertingMaxDataRetentionDays;
					goto IL_0522;
				}
			}
			else if (num != 4195691248U)
			{
				if (num == 4284675511U)
				{
					if (value == "DailyCleanupMinuteOfDay")
					{
						configurationProperty = ConfigurationProperty.CleanupCycleMinuteOfDay;
						goto IL_0522;
					}
				}
			}
			else if (value == "ProcessTimeoutGcExtension")
			{
				configurationProperty = ConfigurationProperty.ProcessTimeoutGcExtension;
				goto IL_0522;
			}
			NameValueCollection nameValueCollection;
			if (properties.ContainsKey(ConfigurationProperty.MiscellaneousProperty))
			{
				configurationProperty = ConfigurationProperty.None;
				nameValueCollection = (NameValueCollection)properties[ConfigurationProperty.MiscellaneousProperty].Value;
			}
			else
			{
				ConfigurationPropertyInfo configurationPropertyInfo = new ConfigurationPropertyInfo();
				nameValueCollection = new NameValueCollection();
				configurationPropertyInfo.Value = nameValueCollection;
				properties.Add(ConfigurationProperty.MiscellaneousProperty, configurationPropertyInfo);
			}
			nameValueCollection.Add(value, xmlAttribute2.Value);
			IL_0522:
			if (configurationProperty != ConfigurationProperty.None)
			{
				properties.Add(configurationProperty, xmlAttribute2.Value, value);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008DDC File Offset: 0x00006FDC
		private static void ParseServiceConfiguration(XmlNode node, ConfigurationPropertyBag properties)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				ConfigurationProperty configurationProperty = ConfigurationProperty.None;
				string text = xmlNode.InnerText;
				string name = xmlNode.Name;
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 1727440780U)
				{
					if (num <= 820970212U)
					{
						if (num <= 395299918U)
						{
							if (num != 248035501U)
							{
								if (num != 351097549U)
								{
									if (num != 395299918U)
									{
										goto IL_0574;
									}
									if (!(name == "IsReportBuilderAnonymousAccessEnabled"))
									{
										goto IL_0574;
									}
									configurationProperty = ConfigurationProperty.IsReportBuilderAnonymousAccessEnabled;
								}
								else
								{
									if (!(name == "MaxQueueThreads"))
									{
										goto IL_0574;
									}
									configurationProperty = ConfigurationProperty.MaxConcurrencyUnattendedExecution;
								}
							}
							else
							{
								if (!(name == "IsNotificationService"))
								{
									goto IL_0574;
								}
								configurationProperty = ConfigurationProperty.IsNotificationService;
							}
						}
						else if (num <= 524058279U)
						{
							if (num != 407859974U)
							{
								if (num != 524058279U)
								{
									goto IL_0574;
								}
								if (!(name == "RecycleTime"))
								{
									goto IL_0574;
								}
								configurationProperty = ConfigurationProperty.AppDomainRecycleTime;
							}
							else
							{
								if (!(name == "DefaultFileShareAccount"))
								{
									goto IL_0574;
								}
								RSConfigurationFileManager.ParseFileShareUser(xmlNode, properties);
								configurationProperty = ConfigurationProperty.None;
							}
						}
						else if (num != 684187218U)
						{
							if (num != 820970212U)
							{
								goto IL_0574;
							}
							if (!(name == "WorkingSetMinimum"))
							{
								goto IL_0574;
							}
							configurationProperty = ConfigurationProperty.WorkingSetMin;
						}
						else
						{
							if (!(name == "IsEventService"))
							{
								goto IL_0574;
							}
							configurationProperty = ConfigurationProperty.IsEventService;
						}
					}
					else if (num <= 1113835850U)
					{
						if (num != 890224879U)
						{
							if (num != 1007545750U)
							{
								if (num != 1113835850U)
								{
									goto IL_0574;
								}
								if (!(name == "UrlRoot"))
								{
									goto IL_0574;
								}
								configurationProperty = ConfigurationProperty.UrlRoot;
							}
							else
							{
								if (!(name == "WindowsServiceUseFileShareStorage"))
								{
									goto IL_0574;
								}
								configurationProperty = ConfigurationProperty.WindowsServiceUseFileShareStorage;
							}
						}
						else
						{
							if (!(name == "MaxCatalogConnectionPoolSizePerProcess"))
							{
								goto IL_0574;
							}
							configurationProperty = ConfigurationProperty.MaxCatalogConnectionPoolSizePerProcess;
						}
					}
					else if (num <= 1632891839U)
					{
						if (num != 1223599726U)
						{
							if (num != 1632891839U)
							{
								goto IL_0574;
							}
							if (!(name == "WebServiceAccount"))
							{
								goto IL_0574;
							}
						}
						else
						{
							if (!(name == "MaxAppDomainUnloadTime"))
							{
								goto IL_0574;
							}
							configurationProperty = ConfigurationProperty.MaxAppDomainUnloadTime;
						}
					}
					else if (num != 1710716505U)
					{
						if (num != 1727440780U)
						{
							goto IL_0574;
						}
						if (!(name == "Hostname"))
						{
							goto IL_0574;
						}
						configurationProperty = ConfigurationProperty.Hostname;
					}
					else
					{
						if (!(name == "MemoryThreshold"))
						{
							goto IL_0574;
						}
						configurationProperty = ConfigurationProperty.MemoryThreshold;
					}
				}
				else if (num <= 2986505298U)
				{
					if (num <= 2310893432U)
					{
						if (num != 1848186706U)
						{
							if (num != 1999800890U)
							{
								if (num != 2310893432U)
								{
									goto IL_0574;
								}
								if (!(name == "FileShareStorageLocation"))
								{
									goto IL_0574;
								}
								XmlNode xmlNode2 = xmlNode.SelectSingleNode("Path");
								if (xmlNode2 != null)
								{
									text = xmlNode2.InnerText;
									if (text != null)
									{
										text = text.Trim();
										if (text.Length > 0)
										{
											configurationProperty = ConfigurationProperty.FileShareStorageLocation;
										}
									}
								}
							}
							else
							{
								if (!(name == "IsAlertingService"))
								{
									goto IL_0574;
								}
								configurationProperty = ConfigurationProperty.IsAlertingService;
							}
						}
						else
						{
							if (!(name == "IsSchedulingService"))
							{
								goto IL_0574;
							}
							configurationProperty = ConfigurationProperty.IsSchedulingService;
						}
					}
					else if (num <= 2819025535U)
					{
						if (num != 2320578481U)
						{
							if (num != 2819025535U)
							{
								goto IL_0574;
							}
							if (!(name == "PolicyLevel"))
							{
								goto IL_0574;
							}
							configurationProperty = ConfigurationProperty.PolicyLevelServer;
						}
						else
						{
							if (!(name == "IsWebServiceEnabled"))
							{
								goto IL_0574;
							}
							configurationProperty = ConfigurationProperty.IsWebService;
						}
					}
					else if (num != 2850982966U)
					{
						if (num != 2986505298U)
						{
							goto IL_0574;
						}
						if (!(name == "MemorySafetyMargin"))
						{
							goto IL_0574;
						}
						configurationProperty = ConfigurationProperty.MemorySafetyMargin;
					}
					else
					{
						if (!(name == "WorkingSetMaximum"))
						{
							goto IL_0574;
						}
						configurationProperty = ConfigurationProperty.WorkingSetMax;
					}
				}
				else if (num <= 3245636698U)
				{
					if (num != 3089152436U)
					{
						if (num != 3133232105U)
						{
							if (num != 3245636698U)
							{
								goto IL_0574;
							}
							if (!(name == "UnattendedExecutionAccount"))
							{
								goto IL_0574;
							}
							RSConfigurationFileManager.ParseSurrogateUser(xmlNode, properties);
							configurationProperty = ConfigurationProperty.None;
						}
						else
						{
							if (!(name == "PollingInterval"))
							{
								goto IL_0574;
							}
							configurationProperty = ConfigurationProperty.PollingInterval;
						}
					}
					else
					{
						if (!(name == "IsRdceEnabled"))
						{
							goto IL_0574;
						}
						configurationProperty = ConfigurationProperty.IsRdceEnabled;
					}
				}
				else if (num <= 3739806083U)
				{
					if (num != 3577696599U)
					{
						if (num != 3739806083U)
						{
							goto IL_0574;
						}
						if (!(name == "UpdatePoliciesSeconds"))
						{
							goto IL_0574;
						}
						configurationProperty = ConfigurationProperty.UpdatePoliciesSeconds;
					}
					else
					{
						if (!(name == "IsReportManagerEnabled"))
						{
							goto IL_0574;
						}
						configurationProperty = ConfigurationProperty.None;
					}
				}
				else if (num != 3811667554U)
				{
					if (num != 4225068784U)
					{
						goto IL_0574;
					}
					if (!(name == "IsDataModelRefreshService"))
					{
						goto IL_0574;
					}
				}
				else
				{
					if (!(name == "UpdatePoliciesChunkSize"))
					{
						goto IL_0574;
					}
					configurationProperty = ConfigurationProperty.UpdatePoliciesChunkSize;
				}
				IL_0588:
				if (configurationProperty != ConfigurationProperty.None)
				{
					properties.Add(configurationProperty, text, xmlNode.Name);
					continue;
				}
				continue;
				IL_0574:
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					RSConfigurationFileManager.ThrowInvalidFormat(xmlNode.Name);
					goto IL_0588;
				}
				goto IL_0588;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000093C0 File Offset: 0x000075C0
		private static void ParseSurrogateUser(XmlNode surrogate, ConfigurationPropertyBag properties)
		{
			foreach (object obj in surrogate.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string innerText = xmlNode.InnerText;
				if (xmlNode.Name == "UserName")
				{
					properties.Add(ConfigurationProperty.UnattendedExecutionAccountUser, innerText, "UserName");
				}
				else if (xmlNode.Name == "Password")
				{
					properties.Add(ConfigurationProperty.UnattendedExecutionAccountPassword, innerText, "Password");
				}
				else if (xmlNode.Name == "Domain")
				{
					properties.Add(ConfigurationProperty.UnattendedExecutionAccountDomain, innerText, "Domain");
				}
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009480 File Offset: 0x00007680
		private static void ParseFileShareUser(XmlNode fileShareUser, ConfigurationPropertyBag properties)
		{
			foreach (object obj in fileShareUser.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string innerText = xmlNode.InnerText;
				if (xmlNode.Name == "UserName")
				{
					properties.Add(ConfigurationProperty.FileShareAccountUser, innerText, "UserName");
				}
				else if (xmlNode.Name == "Password")
				{
					properties.Add(ConfigurationProperty.FileShareAccountPassword, innerText, "Password");
				}
				else if (xmlNode.Name == "Domain")
				{
					properties.Add(ConfigurationProperty.FileShareAccountDomain, innerText, "Domain");
				}
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009540 File Offset: 0x00007740
		private static void ParseUIConfiguration(XmlNode node, ConfigurationPropertyBag properties)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				ConfigurationProperty configurationProperty = ConfigurationProperty.None;
				string innerText = xmlNode.InnerText;
				string name = xmlNode.Name;
				if (!(name == "ReportServerUrl"))
				{
					if (!(name == "EnableReportDesignClientButton"))
					{
						if (!(name == "ReportServerExternalUrl"))
						{
							if (!(name == "CustomAuthenticationUI"))
							{
								if (!(name == "PageCountMode"))
								{
									if (!(name == "PostbackTimeout"))
									{
										if (xmlNode.NodeType != XmlNodeType.Comment)
										{
											RSConfigurationFileManager.ThrowInvalidFormat(xmlNode.Name);
										}
									}
									else
									{
										configurationProperty = ConfigurationProperty.PostbackTimeout;
									}
								}
								else
								{
									configurationProperty = ConfigurationProperty.PageCountMode;
								}
							}
							else
							{
								RSConfigurationFileManager.ReadCustomAuthenticationUI(xmlNode, properties);
							}
						}
						else
						{
							configurationProperty = ConfigurationProperty.ReportManagerReportServerExternalUrl;
						}
					}
					else
					{
						configurationProperty = ConfigurationProperty.EnableReportDesignClientButton;
					}
				}
				else
				{
					configurationProperty = ConfigurationProperty.ReportManagerReportServerUrl;
				}
				if (configurationProperty != ConfigurationProperty.None)
				{
					properties.Add(configurationProperty, innerText, xmlNode.Name);
				}
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000964C File Offset: 0x0000784C
		private static void ReadCustomAuthenticationUI(XmlNode node, ConfigurationPropertyBag properties)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string innerText = xmlNode.InnerText;
				if (xmlNode.Name == "loginUrl")
				{
					properties.Add(ConfigurationProperty.LoginPageUrl, xmlNode.InnerText, "loginUrl");
				}
				else if (xmlNode.Name == "UseSSL")
				{
					properties.Add(ConfigurationProperty.LoginPageUseSSL, xmlNode.InnerText, "UseSSL");
				}
				else if (xmlNode.Name == "PassThroughCookies")
				{
					RSConfigurationFileManager.ReadPassThroughCookies(xmlNode, properties);
				}
				else if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					RSConfigurationFileManager.ThrowInvalidFormat(xmlNode.Name);
				}
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009728 File Offset: 0x00007928
		private static void ReadPassThroughCookies(XmlNode node, ConfigurationPropertyBag properties)
		{
			StringCollection stringCollection = null;
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "PassThroughCookie")
				{
					if (stringCollection == null)
					{
						stringCollection = new StringCollection();
					}
					stringCollection.Add(xmlNode.InnerText);
				}
				else if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					RSConfigurationFileManager.ThrowInvalidFormat(xmlNode.Name);
				}
			}
			if (stringCollection != null)
			{
				properties.Add(ConfigurationProperty.PassthroughCookies, new ConfigurationPropertyInfo
				{
					Value = stringCollection
				});
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000097D8 File Offset: 0x000079D8
		private Dictionary<RunningApplication, UrlConfiguration> ParseUrlReservations(XmlNode node)
		{
			Dictionary<RunningApplication, UrlConfiguration> dictionary = new Dictionary<RunningApplication, UrlConfiguration>();
			using (IEnumerator enumerator = node.ChildNodes.GetEnumerator())
			{
				IL_03F8:
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					XmlNode xmlNode = (XmlNode)obj;
					string text = null;
					if (xmlNode.Name != "Application")
					{
						if (RSConfigurationFileManager.m_Tracer.TraceError)
						{
							RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Configuration file element URLReservations has an invalid child element '{0}'. It will be ignored.", new object[] { xmlNode.Name });
						}
					}
					else
					{
						RunningApplication runningApplication = RunningApplication.Unknown;
						string text2 = null;
						List<UrlReservation> list = null;
						foreach (object obj2 in xmlNode.ChildNodes)
						{
							XmlNode xmlNode2 = (XmlNode)obj2;
							string name = xmlNode2.Name;
							if (!(name == "Name"))
							{
								if (!(name == "VirtualDirectory"))
								{
									if (!(name == "URLs"))
									{
										if (xmlNode2.NodeType != XmlNodeType.Comment)
										{
											RSConfigurationFileManager.ThrowInvalidFormat(xmlNode2.Name);
										}
									}
									else
									{
										list = new List<UrlReservation>();
										foreach (object obj3 in xmlNode2.ChildNodes)
										{
											XmlNode xmlNode3 = (XmlNode)obj3;
											if (xmlNode3.Name != "URL")
											{
												if (RSConfigurationFileManager.m_Tracer.TraceError)
												{
													RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Configuration file element URLs has an invalid child element '{0}'. It will be ignored.", new object[] { xmlNode3.Name });
												}
											}
											else
											{
												XmlNode xmlNode4 = xmlNode3.SelectSingleNode("UrlString");
												if (xmlNode4 == null)
												{
													if (RSConfigurationFileManager.m_Tracer.TraceError)
													{
														RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Configuration file element URL is missing a UrlString element. It will be ignored.");
													}
												}
												else
												{
													string innerText = xmlNode4.InnerText;
													string text3;
													string text4;
													string text5;
													string text6;
													string text7;
													if (!RSConfigurationFileManager.ParseUrlPrefix(innerText, out text3, out text4, out text5, out text6, out text7) || (string.CompareOrdinal(text3, Uri.UriSchemeHttp) != 0 && string.CompareOrdinal(text3, Uri.UriSchemeHttps) != 0) || !string.IsNullOrEmpty(text7))
													{
														if (RSConfigurationFileManager.m_Tracer.TraceError)
														{
															RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Configuration file element URLs has an invalid URL '{0}'. It will be ignored.", new object[] { innerText });
														}
													}
													else
													{
														list.Add(new UrlReservation
														{
															UrlPrefix = text6
														});
													}
												}
											}
										}
									}
								}
								else if (!RSConfigurationFileManager.ParseVirtualRoot(xmlNode2.InnerText, out text2) && RSConfigurationFileManager.m_Tracer.TraceError)
								{
									RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Configuration file element UrlReservation has a invalid VirtualRoot. It will be ignored.");
									goto IL_03F8;
								}
							}
							else
							{
								text = xmlNode2.InnerText;
								if (!(text == "ReportServerWebService"))
								{
									if (!(text == "ReportManager"))
									{
										if (!(text == "ReportServerWebApp"))
										{
											RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Configuration file element UrlReservation has a invalid ApplicationName '{0}'. It will be ignored.", new object[] { text });
											goto IL_03F8;
										}
										runningApplication = RunningApplication.ReportServerWebApp;
									}
									else
									{
										runningApplication = RunningApplication.ReportManager;
									}
								}
								else
								{
									runningApplication = RunningApplication.WebService;
								}
							}
						}
						if (runningApplication == RunningApplication.Unknown)
						{
							if (RSConfigurationFileManager.m_Tracer.TraceError)
							{
								RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Configuration file element UrlReservation is missing an Application element. It will be ignored.");
							}
						}
						else if (text2 == null)
						{
							if (RSConfigurationFileManager.m_Tracer.TraceError)
							{
								RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Configuration file element UrlReservation is missing a VirtualRoot element. It will be ignored.");
							}
						}
						else if (list == null || list.Count == 0)
						{
							if (RSConfigurationFileManager.m_Tracer.TraceError)
							{
								RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "No URLs specifed for Application {0}. It will be ignored.", new object[] { text });
							}
						}
						else
						{
							UrlConfiguration urlConfiguration = default(UrlConfiguration);
							urlConfiguration.VirtualRoot = text2;
							UrlReservation[] array = new UrlReservation[list.Count];
							for (int i = 0; i < array.Length; i++)
							{
								UrlReservation urlReservation = list[i];
								urlReservation.UrlPrefix = urlReservation.UrlPrefix + text2 + "/";
								array[i] = urlReservation;
							}
							urlConfiguration.UrlReservations = array;
							dictionary.Add(runningApplication, urlConfiguration);
						}
					}
				}
			}
			if (dictionary.Count == 0)
			{
				return null;
			}
			return dictionary;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009C58 File Offset: 0x00007E58
		private void ParseAuthentication(XmlNode node, ConfigurationPropertyBag properties)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string innerText = xmlNode.InnerText;
				string name = xmlNode.Name;
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 2486520346U)
				{
					if (num != 1791427791U)
					{
						if (num != 2306491539U)
						{
							if (num == 2486520346U)
							{
								if (name == "UnauthenticatedRequestLockoutTime")
								{
									properties.Add(ConfigurationProperty.UnauthenticatedRequestLockoutTime, innerText);
									continue;
								}
							}
						}
						else if (name == "EnableAuthPersistence")
						{
							properties.Add(ConfigurationProperty.AuthPersistence, innerText);
							continue;
						}
					}
					else if (name == "RSWindowsExtendedProtectionScenario")
					{
						properties.Add(ConfigurationProperty.ExtendedProtectionScenario, innerText);
						continue;
					}
				}
				else if (num <= 3185590243U)
				{
					if (num != 3019916541U)
					{
						if (num == 3185590243U)
						{
							if (name == "MaxUnauthenticatedRequests")
							{
								properties.Add(ConfigurationProperty.MaxUnauthenticatedRequests, innerText);
								continue;
							}
						}
					}
					else if (name == "RSWindowsExtendedProtectionLevel")
					{
						properties.Add(ConfigurationProperty.ExtendedProtectionLevel, innerText);
						continue;
					}
				}
				else if (num != 3626525074U)
				{
					if (num == 3700203374U)
					{
						if (name == "AuthenticationTypes")
						{
							this.ParseAuthenticationTypes(xmlNode, properties);
							continue;
						}
					}
				}
				else if (name == "UnauthenticatedRequestWindow")
				{
					properties.Add(ConfigurationProperty.UnauthenticatedRequestWindow, innerText);
					continue;
				}
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					RSConfigurationFileManager.ThrowInvalidFormat(xmlNode.Name);
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009E08 File Offset: 0x00008008
		private void ParseAuthenticationTypes(XmlNode node, ConfigurationPropertyBag properties)
		{
			AuthenticationTypes authenticationTypes = AuthenticationTypes.None;
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				ConfigurationProperty configurationProperty = ConfigurationProperty.None;
				string text = xmlNode.Name;
				if (!(text == "RSWindowsKerberos"))
				{
					if (!(text == "RSWindowsNTLM"))
					{
						if (!(text == "RSWindowsBasic"))
						{
							if (!(text == "RSWindowsNegotiate"))
							{
								if (text == "Custom")
								{
									authenticationTypes |= AuthenticationTypes.Custom;
									goto IL_0250;
								}
								if (text == "OAuth")
								{
									authenticationTypes |= AuthenticationTypes.OAuth;
									goto IL_0250;
								}
								if (xmlNode.NodeType != XmlNodeType.Comment)
								{
									RSConfigurationFileManager.ThrowInvalidFormat(xmlNode.Name);
									goto IL_0250;
								}
								goto IL_0250;
							}
						}
						else
						{
							authenticationTypes |= AuthenticationTypes.RSWindowsBasic;
							using (IEnumerator enumerator2 = xmlNode.ChildNodes.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									XmlNode xmlNode2 = (XmlNode)obj2;
									ConfigurationProperty configurationProperty2 = ConfigurationProperty.None;
									string innerText = xmlNode2.InnerText;
									text = xmlNode2.Name;
									uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
									if (num <= 1413120113U)
									{
										if (num != 400411952U)
										{
											if (num != 556987153U)
											{
												if (num != 1413120113U)
												{
													goto IL_01DA;
												}
												if (!(text == "AuthTokenCacheEntryTimeout"))
												{
													goto IL_01DA;
												}
												configurationProperty2 = ConfigurationProperty.AuthTokenCacheEntryTimeout;
											}
											else
											{
												if (!(text == "AuthTokenCacheMaxSize"))
												{
													goto IL_01DA;
												}
												configurationProperty2 = ConfigurationProperty.AuthTokenCacheMaxSize;
											}
										}
										else
										{
											if (!(text == "Realm"))
											{
												goto IL_01DA;
											}
											configurationProperty2 = ConfigurationProperty.AuthRealm;
										}
									}
									else if (num <= 2925190818U)
									{
										if (num != 2906604352U)
										{
											if (num != 2925190818U)
											{
												goto IL_01DA;
											}
											if (!(text == "DefaultDomain"))
											{
												goto IL_01DA;
											}
											configurationProperty2 = ConfigurationProperty.AuthDomain;
										}
										else
										{
											if (!(text == "AuthTokenCacheLogonTimeout"))
											{
												goto IL_01DA;
											}
											configurationProperty2 = ConfigurationProperty.AuthTokenCacheLogonTimeout;
										}
									}
									else if (num != 3908379756U)
									{
										if (num != 4059531605U)
										{
											goto IL_01DA;
										}
										if (!(text == "LogonMethod"))
										{
											goto IL_01DA;
										}
										configurationProperty2 = ConfigurationProperty.LogonMethod;
									}
									else
									{
										if (!(text == "AuthTokenCacheMaintenanceInterval"))
										{
											goto IL_01DA;
										}
										configurationProperty2 = ConfigurationProperty.AuthTokenCacheMaintenanceInterval;
									}
									IL_01F0:
									if (configurationProperty2 != ConfigurationProperty.None)
									{
										properties.Add(configurationProperty2, innerText, xmlNode2.Name);
										continue;
									}
									continue;
									IL_01DA:
									if (xmlNode2.NodeType != XmlNodeType.Comment)
									{
										RSConfigurationFileManager.ThrowInvalidFormat(xmlNode2.Name);
										goto IL_01F0;
									}
									goto IL_01F0;
								}
								goto IL_0250;
							}
						}
						authenticationTypes |= AuthenticationTypes.RSWindowsNegotiate;
					}
					else
					{
						authenticationTypes |= AuthenticationTypes.RSWindowsNTLM;
					}
				}
				else
				{
					authenticationTypes |= AuthenticationTypes.RSWindowsKerberos;
				}
				IL_0250:
				if (configurationProperty != ConfigurationProperty.None)
				{
					properties.Add(configurationProperty, xmlNode.InnerText, xmlNode.Name);
				}
			}
			ConfigurationProperty configurationProperty3 = ConfigurationProperty.AuthenticationTypes;
			int num2 = (int)authenticationTypes;
			properties.Add(configurationProperty3, num2.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public static bool ParseUrlPrefix(string url, out string scheme, out string host, out string port, out string prefix, out string path)
		{
			scheme = null;
			prefix = null;
			path = null;
			host = null;
			port = null;
			Match match = Regex.Match(url, "^(?<prefix>(?<scheme>.+)://(?<host>[^/]+):(?<port>[0-9]+))/?(?<path>.*)");
			if (!match.Success)
			{
				return false;
			}
			scheme = match.Groups["scheme"].Value;
			prefix = match.Groups["prefix"].Value;
			path = match.Groups["path"].Value;
			host = match.Groups["host"].Value;
			port = match.Groups["port"].Value;
			return true;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A194 File Offset: 0x00008394
		private static bool ParseVirtualRoot(string url, out string path)
		{
			path = null;
			Match match = Regex.Match(url, "^/?(([^/]+/)*[^/]+)/?$");
			if (!match.Success)
			{
				return false;
			}
			path = "/" + match.Groups[1].Value;
			return true;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007FC8 File Offset: 0x000061C8
		private static void ThrowInvalidFormat(string element)
		{
			throw new Exception(ErrorStringsWrapper.InvalidConfigElement(element));
		}

		// Token: 0x040001E9 RID: 489
		internal static RSTrace m_Tracer = RSTrace.ConfigManagerTracer;

		// Token: 0x040001EA RID: 490
		protected ConfigurationPropertyBag m_properties;

		// Token: 0x040001EB RID: 491
		protected string m_configLocation = "";

		// Token: 0x040001EC RID: 492
		protected string m_configFileName = "";

		// Token: 0x040001ED RID: 493
		protected object m_configChangeLockObject = new object();
	}
}
