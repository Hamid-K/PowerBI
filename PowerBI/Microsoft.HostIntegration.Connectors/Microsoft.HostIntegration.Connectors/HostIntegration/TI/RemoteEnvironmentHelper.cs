using System;
using System.Data.Common;
using System.Xml;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000713 RID: 1811
	public class RemoteEnvironmentHelper
	{
		// Token: 0x06003973 RID: 14707 RVA: 0x000C4930 File Offset: 0x000C2B30
		public static void ToConnectionString(BaseRemoteEnvironment re, ref DbConnectionStringBuilder dbConnectionStringBuilder)
		{
			dbConnectionStringBuilder.Add("CodePage", re.CodePage);
			if (!string.IsNullOrEmpty(re.Name))
			{
				dbConnectionStringBuilder.Add("Name", re.Name);
			}
			dbConnectionStringBuilder.Add("TimeOut", re.TimeOut);
			BaseWithSsoRemoteEnvironment baseWithSsoRemoteEnvironment = re as BaseWithSsoRemoteEnvironment;
			if (baseWithSsoRemoteEnvironment != null)
			{
				if (!string.IsNullOrEmpty(baseWithSsoRemoteEnvironment.SSOApplication))
				{
					dbConnectionStringBuilder.Add("SSOApplication", baseWithSsoRemoteEnvironment.SSOApplication);
				}
				dbConnectionStringBuilder.Add("SecurityFromClientContext", baseWithSsoRemoteEnvironment.SecurityFromClientContext);
			}
			TcpRemoteEnvironment tcpRemoteEnvironment = re as TcpRemoteEnvironment;
			if (tcpRemoteEnvironment != null)
			{
				dbConnectionStringBuilder.Add("IPAddress", tcpRemoteEnvironment.IpAddress);
				dbConnectionStringBuilder.Add("TCPPorts", tcpRemoteEnvironment.Ports);
				return;
			}
			TcpWithSsoRemoteEnvironment tcpWithSsoRemoteEnvironment = re as TcpWithSsoRemoteEnvironment;
			if (tcpWithSsoRemoteEnvironment != null)
			{
				dbConnectionStringBuilder.Add("IPAddress", tcpWithSsoRemoteEnvironment.IpAddress);
				dbConnectionStringBuilder.Add("TCPPorts", tcpWithSsoRemoteEnvironment.Ports);
				TcpWithSslRemoteEnvironment tcpWithSslRemoteEnvironment = tcpWithSsoRemoteEnvironment as TcpWithSslRemoteEnvironment;
				if (tcpWithSslRemoteEnvironment != null)
				{
					dbConnectionStringBuilder.Add("UseSsl", tcpWithSslRemoteEnvironment.UseSsl);
					dbConnectionStringBuilder.Add("NoVerifyCert", !tcpWithSslRemoteEnvironment.ServerVerificationRequired);
					TcpWithCommonNameRemoteEnvironment tcpWithCommonNameRemoteEnvironment = tcpWithSslRemoteEnvironment as TcpWithCommonNameRemoteEnvironment;
					if (tcpWithCommonNameRemoteEnvironment != null)
					{
						if (!string.IsNullOrEmpty(tcpWithCommonNameRemoteEnvironment.CertificateCommonName))
						{
							dbConnectionStringBuilder.Add("CertCommonName", tcpWithCommonNameRemoteEnvironment.CertificateCommonName);
						}
						ImsConnectRemoteEnvironment imsConnectRemoteEnvironment = tcpWithCommonNameRemoteEnvironment as ImsConnectRemoteEnvironment;
						if (imsConnectRemoteEnvironment != null)
						{
							dbConnectionStringBuilder.Add("ImsFormatModName", imsConnectRemoteEnvironment.MfsModName);
							dbConnectionStringBuilder.Add("ItocExitName", imsConnectRemoteEnvironment.ItocExitName);
							dbConnectionStringBuilder.Add("OtmaSystemId", imsConnectRemoteEnvironment.ImsSystemId);
							dbConnectionStringBuilder.Add("UseHWS01SecurityExit", imsConnectRemoteEnvironment.UseHws01SecurityExit);
						}
						ElmTrmBaseRemoteEnvironment elmTrmBaseRemoteEnvironment = re as ElmTrmBaseRemoteEnvironment;
						if (elmTrmBaseRemoteEnvironment != null)
						{
							dbConnectionStringBuilder.Add("UseIbmCicsSecurityExit", elmTrmBaseRemoteEnvironment.UseIbmCicsSecurityExit);
							return;
						}
					}
					else
					{
						HttpBaseRemoteEnvironment httpBaseRemoteEnvironment = tcpWithSslRemoteEnvironment as HttpBaseRemoteEnvironment;
						if (httpBaseRemoteEnvironment != null)
						{
							if (!string.IsNullOrEmpty(httpBaseRemoteEnvironment.AliasTransactionId))
							{
								dbConnectionStringBuilder.Add("AliasTransactionId", httpBaseRemoteEnvironment.AliasTransactionId);
							}
							if (!string.IsNullOrEmpty(httpBaseRemoteEnvironment.UserAgent))
							{
								dbConnectionStringBuilder.Add("UserAgent", httpBaseRemoteEnvironment.UserAgent);
							}
							dbConnectionStringBuilder.Add("AllowRedirects", httpBaseRemoteEnvironment.AllowRedirects);
							HttpLinkRemoteEnvironment httpLinkRemoteEnvironment = httpBaseRemoteEnvironment as HttpLinkRemoteEnvironment;
							if (httpLinkRemoteEnvironment != null && !string.IsNullOrEmpty(httpLinkRemoteEnvironment.MirrorProgram))
							{
								dbConnectionStringBuilder.Add("MirrorProgramName", httpLinkRemoteEnvironment.MirrorProgram);
								return;
							}
						}
					}
				}
				else
				{
					TrmBaseRemoteEnvironment trmBaseRemoteEnvironment = tcpWithSsoRemoteEnvironment as TrmBaseRemoteEnvironment;
					if (trmBaseRemoteEnvironment != null && !string.IsNullOrEmpty(trmBaseRemoteEnvironment.ConcurrentServerTransactionId))
					{
						dbConnectionStringBuilder.Add("ConcurrentServerTransactionId", trmBaseRemoteEnvironment.ConcurrentServerTransactionId);
						return;
					}
				}
			}
			else
			{
				SnaBaseRemoteEnvironment snaBaseRemoteEnvironment = re as SnaBaseRemoteEnvironment;
				if (snaBaseRemoteEnvironment != null)
				{
					dbConnectionStringBuilder.Add("LocalLUName", snaBaseRemoteEnvironment.LocalLuName);
					dbConnectionStringBuilder.Add("RemoteLUName", snaBaseRemoteEnvironment.RemoteLuName);
					dbConnectionStringBuilder.Add("ModeName", snaBaseRemoteEnvironment.ModeName);
					dbConnectionStringBuilder.Add("SyncLevel2Supported", snaBaseRemoteEnvironment.SyncLevel2Supported);
					SnaLinkRemoteEnvironment snaLinkRemoteEnvironment = snaBaseRemoteEnvironment as SnaLinkRemoteEnvironment;
					if (snaLinkRemoteEnvironment != null)
					{
						if (!string.IsNullOrEmpty(snaLinkRemoteEnvironment.MirrorTransactionId))
						{
							dbConnectionStringBuilder.Add("MirrorTranId", snaLinkRemoteEnvironment.MirrorTransactionId);
						}
						dbConnectionStringBuilder.Add("OverrideSNASourceTP", snaLinkRemoteEnvironment.OverrideSnaSourceTp);
						dbConnectionStringBuilder.Add("AllowNonTransactionalSyncPoint", snaLinkRemoteEnvironment.AllowExplicitSyncPoint);
					}
				}
			}
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000C4CB0 File Offset: 0x000C2EB0
		public static void FromConnectionProperties(BaseRemoteEnvironment re, DbConnectionStringBuilder dbConnectionStringBuilder)
		{
			bool flag = false;
			if (dbConnectionStringBuilder.ContainsKey("CodePage"))
			{
				re.CodePage = int.Parse((string)dbConnectionStringBuilder["CodePage"]);
			}
			else
			{
				re.CodePage = 37;
			}
			if (dbConnectionStringBuilder.ContainsKey("TimeOut"))
			{
				re.TimeOut = int.Parse((string)dbConnectionStringBuilder["TimeOut"]);
			}
			if (dbConnectionStringBuilder.ContainsKey("Name"))
			{
				re.Name = (string)dbConnectionStringBuilder["Name"];
			}
			BaseWithSsoRemoteEnvironment baseWithSsoRemoteEnvironment = re as BaseWithSsoRemoteEnvironment;
			if (baseWithSsoRemoteEnvironment != null)
			{
				if (dbConnectionStringBuilder.ContainsKey("SSOApplication"))
				{
					baseWithSsoRemoteEnvironment.SSOApplication = (string)dbConnectionStringBuilder["SSOApplication"];
				}
				if (dbConnectionStringBuilder.ContainsKey("SecurityFromClientContext"))
				{
					baseWithSsoRemoteEnvironment.SecurityFromClientContext = bool.Parse((string)dbConnectionStringBuilder["SecurityFromClientContext"]);
					flag = true;
				}
			}
			TcpRemoteEnvironment tcpRemoteEnvironment = re as TcpRemoteEnvironment;
			if (tcpRemoteEnvironment != null)
			{
				if (dbConnectionStringBuilder.ContainsKey("IPAddress"))
				{
					tcpRemoteEnvironment.IpAddress = (string)dbConnectionStringBuilder["IPAddress"];
				}
				if (dbConnectionStringBuilder.ContainsKey("TCPPorts"))
				{
					tcpRemoteEnvironment.Ports = (string)dbConnectionStringBuilder["TCPPorts"];
				}
			}
			else
			{
				TcpWithSsoRemoteEnvironment tcpWithSsoRemoteEnvironment = re as TcpWithSsoRemoteEnvironment;
				if (tcpWithSsoRemoteEnvironment != null)
				{
					if (dbConnectionStringBuilder.ContainsKey("IPAddress"))
					{
						tcpWithSsoRemoteEnvironment.IpAddress = (string)dbConnectionStringBuilder["IPAddress"];
					}
					if (dbConnectionStringBuilder.ContainsKey("TCPPorts"))
					{
						tcpWithSsoRemoteEnvironment.Ports = (string)dbConnectionStringBuilder["TCPPorts"];
					}
					else if (dbConnectionStringBuilder.ContainsKey("HTTPPorts"))
					{
						tcpWithSsoRemoteEnvironment.Ports = (string)dbConnectionStringBuilder["HTTPPorts"];
					}
					TcpWithSslRemoteEnvironment tcpWithSslRemoteEnvironment = tcpWithSsoRemoteEnvironment as TcpWithSslRemoteEnvironment;
					if (tcpWithSslRemoteEnvironment != null)
					{
						if (dbConnectionStringBuilder.ContainsKey("UseSsl"))
						{
							tcpWithSslRemoteEnvironment.UseSsl = bool.Parse((string)dbConnectionStringBuilder["UseSsl"]);
						}
						if (dbConnectionStringBuilder.ContainsKey("NoVerifyCert"))
						{
							tcpWithSslRemoteEnvironment.ServerVerificationRequired = !bool.Parse((string)dbConnectionStringBuilder["NoVerifyCert"]);
						}
						else
						{
							tcpWithSslRemoteEnvironment.ServerVerificationRequired = true;
						}
						TcpWithCommonNameRemoteEnvironment tcpWithCommonNameRemoteEnvironment = tcpWithSslRemoteEnvironment as TcpWithCommonNameRemoteEnvironment;
						if (tcpWithCommonNameRemoteEnvironment != null)
						{
							if (dbConnectionStringBuilder.ContainsKey("CertCommonName"))
							{
								tcpWithCommonNameRemoteEnvironment.CertificateCommonName = (string)dbConnectionStringBuilder["CertCommonName"];
							}
							ImsConnectRemoteEnvironment imsConnectRemoteEnvironment = tcpWithCommonNameRemoteEnvironment as ImsConnectRemoteEnvironment;
							if (imsConnectRemoteEnvironment != null)
							{
								if (dbConnectionStringBuilder.ContainsKey("ImsFormatModName"))
								{
									imsConnectRemoteEnvironment.MfsModName = (string)dbConnectionStringBuilder["ImsFormatModName"];
								}
								if (dbConnectionStringBuilder.ContainsKey("ItocExitName"))
								{
									imsConnectRemoteEnvironment.ItocExitName = (string)dbConnectionStringBuilder["ItocExitName"];
								}
								if (dbConnectionStringBuilder.ContainsKey("OtmaSystemId"))
								{
									imsConnectRemoteEnvironment.ImsSystemId = (string)dbConnectionStringBuilder["OtmaSystemId"];
								}
								if (dbConnectionStringBuilder.ContainsKey("UseHWS01SecurityExit"))
								{
									imsConnectRemoteEnvironment.UseHws01SecurityExit = bool.Parse((string)dbConnectionStringBuilder["UseHWS01SecurityExit"]);
									flag = true;
								}
							}
							ElmTrmBaseRemoteEnvironment elmTrmBaseRemoteEnvironment = re as ElmTrmBaseRemoteEnvironment;
							if (elmTrmBaseRemoteEnvironment != null && dbConnectionStringBuilder.ContainsKey("UseIbmCicsSecurityExit"))
							{
								elmTrmBaseRemoteEnvironment.UseIbmCicsSecurityExit = bool.Parse((string)dbConnectionStringBuilder["UseIbmCicsSecurityExit"]);
								flag = true;
							}
						}
						else
						{
							HttpBaseRemoteEnvironment httpBaseRemoteEnvironment = tcpWithSslRemoteEnvironment as HttpBaseRemoteEnvironment;
							if (httpBaseRemoteEnvironment != null)
							{
								if (dbConnectionStringBuilder.ContainsKey("AliasTransactionId"))
								{
									httpBaseRemoteEnvironment.AliasTransactionId = (string)dbConnectionStringBuilder["AliasTransactionId"];
								}
								if (dbConnectionStringBuilder.ContainsKey("UserAgent"))
								{
									httpBaseRemoteEnvironment.UserAgent = (string)dbConnectionStringBuilder["UserAgent"];
								}
								if (dbConnectionStringBuilder.ContainsKey("AllowRedirects"))
								{
									httpBaseRemoteEnvironment.AllowRedirects = bool.Parse((string)dbConnectionStringBuilder["AllowRedirects"]);
								}
								HttpLinkRemoteEnvironment httpLinkRemoteEnvironment = httpBaseRemoteEnvironment as HttpLinkRemoteEnvironment;
								if (httpLinkRemoteEnvironment != null && dbConnectionStringBuilder.ContainsKey("MirrorProgramName"))
								{
									httpLinkRemoteEnvironment.MirrorProgram = (string)dbConnectionStringBuilder["MirrorProgramName"];
								}
							}
						}
					}
					else
					{
						TrmBaseRemoteEnvironment trmBaseRemoteEnvironment = tcpWithSsoRemoteEnvironment as TrmBaseRemoteEnvironment;
						if (trmBaseRemoteEnvironment != null && dbConnectionStringBuilder.ContainsKey("ConcurrentServerTransactionId"))
						{
							trmBaseRemoteEnvironment.ConcurrentServerTransactionId = (string)dbConnectionStringBuilder["ConcurrentServerTransactionId"];
						}
					}
				}
				else
				{
					SnaBaseRemoteEnvironment snaBaseRemoteEnvironment = re as SnaBaseRemoteEnvironment;
					if (snaBaseRemoteEnvironment != null)
					{
						if (dbConnectionStringBuilder.ContainsKey("LocalLUName"))
						{
							snaBaseRemoteEnvironment.LocalLuName = (string)dbConnectionStringBuilder["LocalLUName"];
						}
						if (dbConnectionStringBuilder.ContainsKey("RemoteLUName"))
						{
							snaBaseRemoteEnvironment.RemoteLuName = (string)dbConnectionStringBuilder["RemoteLUName"];
						}
						if (dbConnectionStringBuilder.ContainsKey("ModeName"))
						{
							snaBaseRemoteEnvironment.ModeName = (string)dbConnectionStringBuilder["ModeName"];
						}
						if (dbConnectionStringBuilder.ContainsKey("SyncLevel2Supported"))
						{
							snaBaseRemoteEnvironment.SyncLevel2Supported = bool.Parse((string)dbConnectionStringBuilder["SyncLevel2Supported"]);
						}
						SnaLinkRemoteEnvironment snaLinkRemoteEnvironment = snaBaseRemoteEnvironment as SnaLinkRemoteEnvironment;
						if (snaLinkRemoteEnvironment != null)
						{
							if (dbConnectionStringBuilder.ContainsKey("MirrorTranId"))
							{
								snaLinkRemoteEnvironment.MirrorTransactionId = (string)dbConnectionStringBuilder["MirrorTranId"];
							}
							if (dbConnectionStringBuilder.ContainsKey("OverrideSNASourceTP"))
							{
								snaLinkRemoteEnvironment.OverrideSnaSourceTp = bool.Parse((string)dbConnectionStringBuilder["OverrideSNASourceTP"]);
								flag = true;
							}
							if (dbConnectionStringBuilder.ContainsKey("AdministrationFlags"))
							{
								RemoteEnvironmentAdministration remoteEnvironmentAdministration = (RemoteEnvironmentAdministration)RemoteEnvironmentHelper.GetEnumValue(typeof(RemoteEnvironmentAdministration), (string)dbConnectionStringBuilder["AdministrationFlags"]);
								snaLinkRemoteEnvironment.AllowExplicitSyncPoint = remoteEnvironmentAdministration == RemoteEnvironmentAdministration.AllowSyncPoint;
							}
							else if (dbConnectionStringBuilder.ContainsKey("AllowNonTransactionalSyncPoint"))
							{
								snaLinkRemoteEnvironment.AllowExplicitSyncPoint = bool.Parse((string)dbConnectionStringBuilder["AllowNonTransactionalSyncPoint"]);
							}
						}
					}
				}
			}
			if (!flag && dbConnectionStringBuilder.ContainsKey("Security"))
			{
				int num = int.Parse((string)dbConnectionStringBuilder["Security"]);
				BaseWithSsoRemoteEnvironment baseWithSsoRemoteEnvironment2 = re as BaseWithSsoRemoteEnvironment;
				if (baseWithSsoRemoteEnvironment2 != null && (num & 4) == 4)
				{
					baseWithSsoRemoteEnvironment2.SecurityFromClientContext = true;
				}
				if (num != 0 && num != 65535 && num != -1)
				{
					ElmTrmBaseRemoteEnvironment elmTrmBaseRemoteEnvironment2 = re as ElmTrmBaseRemoteEnvironment;
					if (elmTrmBaseRemoteEnvironment2 != null && (num & 131072) == 131072)
					{
						elmTrmBaseRemoteEnvironment2.UseIbmCicsSecurityExit = true;
					}
					ImsConnectRemoteEnvironment imsConnectRemoteEnvironment2 = re as ImsConnectRemoteEnvironment;
					if (imsConnectRemoteEnvironment2 != null && (num & 262144) == 262144)
					{
						imsConnectRemoteEnvironment2.UseHws01SecurityExit = true;
					}
					SnaLinkRemoteEnvironment snaLinkRemoteEnvironment2 = re as SnaLinkRemoteEnvironment;
					if (snaLinkRemoteEnvironment2 != null && (num & 524288) == 524288)
					{
						snaLinkRemoteEnvironment2.OverrideSnaSourceTp = true;
					}
				}
			}
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000C531C File Offset: 0x000C351C
		public static string ExtractRegValue(XmlElement reElement, string tagName)
		{
			XmlNode xmlNode = reElement.SelectSingleNode(tagName + "/RegistryValue");
			if (xmlNode != null)
			{
				return xmlNode.InnerText;
			}
			return null;
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000C5348 File Offset: 0x000C3548
		public static void FromXml(BaseRemoteEnvironment re, XmlElement reElement)
		{
			string text = RemoteEnvironmentHelper.ExtractRegValue(reElement, "CodePage");
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					re.CodePage = int.Parse(text);
				}
				catch
				{
				}
			}
			string text2 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "Timeout");
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					re.TimeOut = int.Parse(text2);
				}
				catch
				{
				}
			}
			string text3 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "Name");
			if (!string.IsNullOrEmpty(text3))
			{
				re.Name = text3;
			}
			BaseWithSsoRemoteEnvironment baseWithSsoRemoteEnvironment = re as BaseWithSsoRemoteEnvironment;
			if (baseWithSsoRemoteEnvironment != null)
			{
				string text4 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "HostDomainName");
				if (!string.IsNullOrEmpty(text4))
				{
					baseWithSsoRemoteEnvironment.SSOApplication = text4;
				}
			}
			TcpRemoteEnvironment tcpRemoteEnvironment = re as TcpRemoteEnvironment;
			if (tcpRemoteEnvironment != null)
			{
				string text5 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "IpAddress");
				if (!string.IsNullOrEmpty(text5))
				{
					tcpRemoteEnvironment.IpAddress = text5;
				}
				string text6 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "TcpPorts");
				if (!string.IsNullOrEmpty(text6))
				{
					tcpRemoteEnvironment.Ports = text6;
				}
			}
			else
			{
				TcpWithSsoRemoteEnvironment tcpWithSsoRemoteEnvironment = re as TcpWithSsoRemoteEnvironment;
				if (tcpWithSsoRemoteEnvironment != null)
				{
					string text7 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "IpAddress");
					if (!string.IsNullOrEmpty(text7))
					{
						tcpWithSsoRemoteEnvironment.IpAddress = text7;
					}
					string text8 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "TcpPorts");
					if (!string.IsNullOrEmpty(text8))
					{
						tcpWithSsoRemoteEnvironment.Ports = text8;
					}
					else
					{
						text8 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "HTTPPort");
						if (!string.IsNullOrEmpty(text8))
						{
							tcpWithSsoRemoteEnvironment.Ports = text8;
						}
					}
					TcpWithSslRemoteEnvironment tcpWithSslRemoteEnvironment = tcpWithSsoRemoteEnvironment as TcpWithSslRemoteEnvironment;
					if (tcpWithSslRemoteEnvironment != null)
					{
						string text9 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "UseSSL");
						if (!string.IsNullOrEmpty(text9))
						{
							try
							{
								tcpWithSslRemoteEnvironment.UseSsl = bool.Parse(text9);
							}
							catch
							{
								try
								{
									tcpWithSslRemoteEnvironment.UseSsl = int.Parse(text9) != 0;
								}
								catch
								{
								}
							}
						}
						TcpWithCommonNameRemoteEnvironment tcpWithCommonNameRemoteEnvironment = tcpWithSslRemoteEnvironment as TcpWithCommonNameRemoteEnvironment;
						if (tcpWithCommonNameRemoteEnvironment != null)
						{
							ImsConnectRemoteEnvironment imsConnectRemoteEnvironment = tcpWithCommonNameRemoteEnvironment as ImsConnectRemoteEnvironment;
							if (imsConnectRemoteEnvironment != null)
							{
								string text10 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "IMSFormatModName");
								if (!string.IsNullOrEmpty(text10))
								{
									imsConnectRemoteEnvironment.MfsModName = text10;
								}
								string text11 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "ItocExitName");
								if (!string.IsNullOrEmpty(text11))
								{
									imsConnectRemoteEnvironment.ItocExitName = text11;
								}
								string text12 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "OtmaSysId");
								if (!string.IsNullOrEmpty(text12))
								{
									imsConnectRemoteEnvironment.ImsSystemId = text12;
								}
							}
						}
						else
						{
							HttpBaseRemoteEnvironment httpBaseRemoteEnvironment = tcpWithSslRemoteEnvironment as HttpBaseRemoteEnvironment;
							if (httpBaseRemoteEnvironment != null)
							{
								string text13 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "AliasTPId");
								if (!string.IsNullOrEmpty(text13))
								{
									httpBaseRemoteEnvironment.AliasTransactionId = text13;
								}
								string text14 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "AllowReDirects");
								if (!string.IsNullOrEmpty(text14))
								{
									try
									{
										httpBaseRemoteEnvironment.AllowRedirects = bool.Parse(text14);
									}
									catch
									{
										try
										{
											httpBaseRemoteEnvironment.AllowRedirects = int.Parse(text14) != 0;
										}
										catch
										{
										}
									}
								}
								string text15 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "UserAgent");
								if (!string.IsNullOrEmpty(text15))
								{
									httpBaseRemoteEnvironment.UserAgent = text15;
								}
								HttpLinkRemoteEnvironment httpLinkRemoteEnvironment = httpBaseRemoteEnvironment as HttpLinkRemoteEnvironment;
								if (httpLinkRemoteEnvironment != null)
								{
									string text16 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "MirrorProg");
									if (!string.IsNullOrEmpty(text16))
									{
										httpLinkRemoteEnvironment.MirrorProgram = text16;
									}
								}
							}
						}
					}
					else if (tcpWithSsoRemoteEnvironment is TrmBaseRemoteEnvironment)
					{
					}
				}
				else
				{
					SnaBaseRemoteEnvironment snaBaseRemoteEnvironment = re as SnaBaseRemoteEnvironment;
					if (snaBaseRemoteEnvironment != null)
					{
						string text17 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "LU");
						if (!string.IsNullOrEmpty(text17))
						{
							snaBaseRemoteEnvironment.LocalLuName = text17;
						}
						string text18 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "PLU");
						if (!string.IsNullOrEmpty(text18))
						{
							snaBaseRemoteEnvironment.RemoteLuName = text18;
						}
						string text19 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "Mode");
						if (!string.IsNullOrEmpty(text19))
						{
							snaBaseRemoteEnvironment.ModeName = text19;
						}
						string text20 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "SyncLevel2");
						if (!string.IsNullOrEmpty(text20))
						{
							try
							{
								snaBaseRemoteEnvironment.SyncLevel2Supported = bool.Parse(text20);
							}
							catch
							{
								try
								{
									snaBaseRemoteEnvironment.SyncLevel2Supported = int.Parse(text20) != 0;
								}
								catch
								{
								}
							}
						}
						SnaLinkRemoteEnvironment snaLinkRemoteEnvironment = snaBaseRemoteEnvironment as SnaLinkRemoteEnvironment;
						if (snaLinkRemoteEnvironment != null)
						{
							string text21 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "AdmFlags");
							if (!string.IsNullOrEmpty(text21))
							{
								RemoteEnvironmentAdministration remoteEnvironmentAdministration = (RemoteEnvironmentAdministration)int.Parse(text21);
								snaLinkRemoteEnvironment.AllowExplicitSyncPoint = remoteEnvironmentAdministration == RemoteEnvironmentAdministration.AllowSyncPoint;
							}
						}
					}
				}
			}
			string text22 = RemoteEnvironmentHelper.ExtractRegValue(reElement, "Security");
			if (!string.IsNullOrEmpty(text22))
			{
				int num = int.Parse(text22);
				BaseWithSsoRemoteEnvironment baseWithSsoRemoteEnvironment2 = re as BaseWithSsoRemoteEnvironment;
				if (baseWithSsoRemoteEnvironment2 != null && (num & 4) == 4)
				{
					baseWithSsoRemoteEnvironment2.SecurityFromClientContext = true;
				}
				if (num != 0 && num != 65535 && num != -1)
				{
					ElmTrmBaseRemoteEnvironment elmTrmBaseRemoteEnvironment = re as ElmTrmBaseRemoteEnvironment;
					if (elmTrmBaseRemoteEnvironment != null && (num & 131072) == 131072)
					{
						elmTrmBaseRemoteEnvironment.UseIbmCicsSecurityExit = true;
					}
					ImsConnectRemoteEnvironment imsConnectRemoteEnvironment2 = re as ImsConnectRemoteEnvironment;
					if (imsConnectRemoteEnvironment2 != null && (num & 262144) == 262144)
					{
						imsConnectRemoteEnvironment2.UseHws01SecurityExit = true;
					}
					SnaLinkRemoteEnvironment snaLinkRemoteEnvironment2 = re as SnaLinkRemoteEnvironment;
					if (snaLinkRemoteEnvironment2 != null && (num & 524288) == 524288)
					{
						snaLinkRemoteEnvironment2.OverrideSnaSourceTp = true;
					}
				}
			}
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000C584C File Offset: 0x000C3A4C
		public static bool IsConnectionStringValid(BaseRemoteEnvironment re)
		{
			bool flag = re.CodePage > 0 && re.TimeOut >= 0;
			TcpBaseRemoteEnvironment tcpBaseRemoteEnvironment = re as TcpBaseRemoteEnvironment;
			if (tcpBaseRemoteEnvironment != null)
			{
				flag = flag && !string.IsNullOrEmpty(tcpBaseRemoteEnvironment.IpAddress) && !string.IsNullOrEmpty(tcpBaseRemoteEnvironment.Ports);
			}
			else
			{
				TcpWithSsoRemoteEnvironment tcpWithSsoRemoteEnvironment = re as TcpWithSsoRemoteEnvironment;
				if (tcpWithSsoRemoteEnvironment != null)
				{
					flag = flag && !string.IsNullOrEmpty(tcpWithSsoRemoteEnvironment.IpAddress) && !string.IsNullOrEmpty(tcpWithSsoRemoteEnvironment.Ports);
					ImsConnectRemoteEnvironment imsConnectRemoteEnvironment = tcpWithSsoRemoteEnvironment as ImsConnectRemoteEnvironment;
					if (imsConnectRemoteEnvironment != null)
					{
						flag = flag && !string.IsNullOrEmpty(imsConnectRemoteEnvironment.MfsModName) && !string.IsNullOrEmpty(imsConnectRemoteEnvironment.ItocExitName) && !string.IsNullOrEmpty(imsConnectRemoteEnvironment.ImsSystemId);
					}
				}
				else
				{
					SnaBaseRemoteEnvironment snaBaseRemoteEnvironment = re as SnaBaseRemoteEnvironment;
					if (snaBaseRemoteEnvironment != null)
					{
						flag = flag && !string.IsNullOrEmpty(snaBaseRemoteEnvironment.LocalLuName) && !string.IsNullOrEmpty(snaBaseRemoteEnvironment.RemoteLuName) && !string.IsNullOrEmpty(snaBaseRemoteEnvironment.ModeName);
					}
				}
			}
			return flag;
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000C594C File Offset: 0x000C3B4C
		public static object GetEnumValue(Type enumType, string name)
		{
			Array values = Enum.GetValues(enumType);
			for (int i = 0; i < values.Length; i++)
			{
				if (Enum.GetName(enumType, values.GetValue(i)) == name)
				{
					return values.GetValue(i);
				}
			}
			throw new Exception("Enum value not found for name = " + name);
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000C59A0 File Offset: 0x000C3BA0
		public static string GetMissingProperties(BaseRemoteEnvironment re)
		{
			string text = "";
			TcpBaseRemoteEnvironment tcpBaseRemoteEnvironment = re as TcpBaseRemoteEnvironment;
			if (tcpBaseRemoteEnvironment != null)
			{
				text = text + (string.IsNullOrEmpty(tcpBaseRemoteEnvironment.IpAddress) ? "IPAddress" : " ") + (string.IsNullOrEmpty(tcpBaseRemoteEnvironment.Ports) ? "TCPPorts" : " ");
			}
			else
			{
				TcpWithSsoRemoteEnvironment tcpWithSsoRemoteEnvironment = re as TcpWithSsoRemoteEnvironment;
				if (tcpWithSsoRemoteEnvironment != null)
				{
					text = text + (string.IsNullOrEmpty(tcpWithSsoRemoteEnvironment.IpAddress) ? "IpAddress" : " ") + (string.IsNullOrEmpty(tcpWithSsoRemoteEnvironment.Ports) ? "TCPPorts" : " ");
					ImsConnectRemoteEnvironment imsConnectRemoteEnvironment = tcpWithSsoRemoteEnvironment as ImsConnectRemoteEnvironment;
					if (imsConnectRemoteEnvironment != null)
					{
						text = text + (string.IsNullOrEmpty(imsConnectRemoteEnvironment.MfsModName) ? "ImsFormatModName" : " ") + (string.IsNullOrEmpty(imsConnectRemoteEnvironment.ItocExitName) ? "ItocExitName" : " ") + (string.IsNullOrEmpty(imsConnectRemoteEnvironment.ImsSystemId) ? "IMSSystemId" : " ");
					}
				}
				else
				{
					SnaBaseRemoteEnvironment snaBaseRemoteEnvironment = re as SnaBaseRemoteEnvironment;
					if (snaBaseRemoteEnvironment != null)
					{
						text = text + (string.IsNullOrEmpty(snaBaseRemoteEnvironment.LocalLuName) ? "LocalLUName" : " ") + (string.IsNullOrEmpty(snaBaseRemoteEnvironment.RemoteLuName) ? "RemoteLUName" : " ") + (string.IsNullOrEmpty(snaBaseRemoteEnvironment.ModeName) ? "ModeName" : " ");
					}
				}
			}
			return text;
		}
	}
}
