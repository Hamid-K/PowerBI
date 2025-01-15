using System;
using System.Web.Configuration;
using System.Web.Security;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200005D RID: 93
	internal static class WebConfigUtil
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0001131A File Offset: 0x0000F51A
		public static byte AuthenticationType
		{
			get
			{
				return (byte)WebConfigUtil.WebServerAuthMode;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00011322 File Offset: 0x0000F522
		public static bool UsingFormsAuth
		{
			get
			{
				return WebConfigUtil.WebServerAuthMode == Microsoft.ReportingServices.Interfaces.AuthenticationType.Forms;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001132C File Offset: 0x0000F52C
		public static bool UsingWindowsAuth
		{
			get
			{
				return WebConfigUtil.WebServerAuthMode == Microsoft.ReportingServices.Interfaces.AuthenticationType.Windows;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00011336 File Offset: 0x0000F536
		public static string LoginUrl
		{
			get
			{
				if (WebConfigUtil.m_loginUrl == null)
				{
					WebConfigUtil.m_loginUrl = WebConfigUtil.GetLoginUrl();
				}
				return WebConfigUtil.m_loginUrl;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001134E File Offset: 0x0000F54E
		public static string FormsCookieName
		{
			get
			{
				if (WebConfigUtil.m_cookieName == null)
				{
					WebConfigUtil.m_cookieName = WebConfigUtil.GetFormsCookieName();
				}
				return WebConfigUtil.m_cookieName;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00011366 File Offset: 0x0000F566
		public static AuthenticationType WebServerAuthMode
		{
			get
			{
				if (WebConfigUtil.m_authMode == Microsoft.ReportingServices.Interfaces.AuthenticationType.None)
				{
					WebConfigUtil.m_authMode = WebConfigUtil.GetAuthenticationType();
				}
				return WebConfigUtil.m_authMode;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00011380 File Offset: 0x0000F580
		private static AuthenticationType GetAuthenticationType()
		{
			XmlAttribute webConfigAuthenticationAttribute = WebConfigUtil.GetWebConfigAuthenticationAttribute("mode");
			AuthenticationMode authenticationMode;
			if (webConfigAuthenticationAttribute != null)
			{
				authenticationMode = (AuthenticationMode)Enum.Parse(typeof(AuthenticationMode), webConfigAuthenticationAttribute.InnerText);
			}
			else
			{
				authenticationMode = AuthenticationMode.Windows;
			}
			return WebConfigUtil.ConvertWebAuthenticationMode(authenticationMode);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000113C4 File Offset: 0x0000F5C4
		private static AuthenticationType ConvertWebAuthenticationMode(AuthenticationMode mode)
		{
			switch (mode)
			{
			case AuthenticationMode.None:
				if (Globals.Configuration != null && (Globals.Configuration.AuthenticationTypes & AuthenticationTypes.OAuth) != AuthenticationTypes.None)
				{
					return Microsoft.ReportingServices.Interfaces.AuthenticationType.OAuth;
				}
				return Microsoft.ReportingServices.Interfaces.AuthenticationType.None;
			case AuthenticationMode.Windows:
				return Microsoft.ReportingServices.Interfaces.AuthenticationType.Windows;
			case AuthenticationMode.Passport:
				return Microsoft.ReportingServices.Interfaces.AuthenticationType.Passport;
			case AuthenticationMode.Forms:
				return Microsoft.ReportingServices.Interfaces.AuthenticationType.Forms;
			default:
				throw new ServerConfigurationErrorException("web Config Authentication mode invalid value");
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00011414 File Offset: 0x0000F614
		private static string GetLoginUrl()
		{
			XmlAttribute webConfigFormsAuthenticationAttribute = WebConfigUtil.GetWebConfigFormsAuthenticationAttribute("loginUrl");
			if (webConfigFormsAuthenticationAttribute != null)
			{
				return webConfigFormsAuthenticationAttribute.InnerText;
			}
			return WebConfigUtil.DefaultLoginPage;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001143C File Offset: 0x0000F63C
		private static string GetFormsCookieName()
		{
			XmlAttribute webConfigFormsAuthenticationAttribute = WebConfigUtil.GetWebConfigFormsAuthenticationAttribute("name");
			if (webConfigFormsAuthenticationAttribute != null)
			{
				return webConfigFormsAuthenticationAttribute.InnerText;
			}
			return FormsAuthentication.FormsCookieName;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00011463 File Offset: 0x0000F663
		private static XmlAttribute GetWebConfigAuthenticationAttribute(string attrName)
		{
			XmlAttribute attr = null;
			RevertImpersonationContext.Run(delegate
			{
				XmlNode webConfigAuthenticationNode = WebConfigUtil.GetWebConfigAuthenticationNode();
				if (webConfigAuthenticationNode != null)
				{
					attr = webConfigAuthenticationNode.Attributes[attrName];
				}
			});
			return attr;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001148E File Offset: 0x0000F68E
		private static XmlAttribute GetWebConfigFormsAuthenticationAttribute(string attrName)
		{
			XmlAttribute attr = null;
			RevertImpersonationContext.Run(delegate
			{
				XmlNode webConfigAuthenticationNode = WebConfigUtil.GetWebConfigAuthenticationNode();
				XmlNode xmlNode = null;
				if (webConfigAuthenticationNode != null)
				{
					foreach (object obj in webConfigAuthenticationNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						if (string.Equals(xmlNode2.LocalName, "forms", StringComparison.Ordinal))
						{
							xmlNode = xmlNode2;
							break;
						}
					}
					if (xmlNode != null)
					{
						attr = xmlNode.Attributes[attrName];
					}
				}
			});
			return attr;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000114BC File Offset: 0x0000F6BC
		private static XmlNode GetWebConfigAuthenticationNode()
		{
			RequestContext reqContext = ProcessingContext.ReqContext;
			string text;
			if (reqContext != null)
			{
				text = reqContext.MapPath(reqContext.ApplicationPath);
			}
			else
			{
				text = Globals.Configuration.ConfigFilePath;
			}
			string text2 = text + "\\web.config";
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentFile(xmlDocument, text2);
			string namespaceURI = xmlDocument.DocumentElement.NamespaceURI;
			XmlNode xmlNode;
			if (!string.IsNullOrEmpty(namespaceURI))
			{
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				xmlNamespaceManager.AddNamespace("configns", namespaceURI);
				xmlNode = xmlDocument.SelectSingleNode("configns:configuration/configns:system.web/configns:authentication", xmlNamespaceManager);
			}
			else
			{
				xmlNode = xmlDocument.SelectSingleNode("configuration/system.web/authentication");
			}
			return xmlNode;
		}

		// Token: 0x040001AC RID: 428
		private static AuthenticationType m_authMode = Microsoft.ReportingServices.Interfaces.AuthenticationType.None;

		// Token: 0x040001AD RID: 429
		private static string m_loginUrl;

		// Token: 0x040001AE RID: 430
		private static string m_cookieName;

		// Token: 0x040001AF RID: 431
		private static readonly string DefaultLoginPage = "login.aspx";
	}
}
