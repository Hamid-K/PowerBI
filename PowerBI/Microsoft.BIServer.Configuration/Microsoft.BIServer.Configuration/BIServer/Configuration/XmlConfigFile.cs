using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Microsoft.BIServer.Configuration.Exceptions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.Data.SqlClient;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000028 RID: 40
	public sealed class XmlConfigFile
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005920 File Offset: 0x00003B20
		public static Guid GetInstallationId(string configFileFullPath)
		{
			if (!File.Exists(configFileFullPath))
			{
				throw new ConfigException.MissingConfigFile(string.Format("Missing file at path {0}", Path.GetFullPath(configFileFullPath)));
			}
			XElement xelement = XElement.Load(configFileFullPath).Element("InstallationID");
			if (!string.IsNullOrEmpty(xelement.Value))
			{
				return new Guid(xelement.Value);
			}
			return Guid.Empty;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000597F File Offset: 0x00003B7F
		public XmlConfigFile(string installDir)
		{
			this._configFilePath = XmlConfigFile.GetFileFromDirectory(installDir);
			Logger.Info("Reading Config File [{0}]", new object[] { this._configFilePath });
			this._rootElement = XElement.Load(this._configFilePath);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000059BD File Offset: 0x00003BBD
		public XmlConfigFile(XElement configFileElement)
		{
			this._rootElement = configFileElement;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000059CC File Offset: 0x00003BCC
		public static string GetFileFromDirectory(string installDir)
		{
			return Path.GetFullPath(Path.Combine(installDir, "ReportServer\\rsreportserver.config"));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000059E0 File Offset: 0x00003BE0
		public static void Delete(string installDir)
		{
			string fileFromDirectory = XmlConfigFile.GetFileFromDirectory(installDir);
			if (File.Exists(fileFromDirectory))
			{
				Logger.Info("Removing ConfigReader File [{0}]", new object[] { fileFromDirectory });
				File.Delete(fileFromDirectory);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005A18 File Offset: 0x00003C18
		public void CommitChanges()
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = true,
				NewLineHandling = NewLineHandling.Entitize
			};
			using (XmlWriter xmlWriter = XmlWriter.Create(this._configFilePath, xmlWriterSettings))
			{
				this._rootElement.Save(xmlWriter);
				Logger.Info("Writing ConfigReader File [{0}]", new object[] { this._configFilePath });
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005A90 File Offset: 0x00003C90
		public string GetDsn()
		{
			XElement xelement = this._rootElement.XPathSelectElement("//Dsn");
			if (xelement == null || string.IsNullOrWhiteSpace(xelement.Value))
			{
				return null;
			}
			return MachineKeyCrypto.Instance.DecryptToString(xelement.Value);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public void SetDsn(string value)
		{
			XElement xelement = this._rootElement.XPathSelectElement("//Dsn");
			if (xelement == null)
			{
				this._rootElement.Add("Dsn");
				xelement = this._rootElement.XPathSelectElement("//Dsn");
			}
			if (value != null)
			{
				xelement.Value = MachineKeyCrypto.Instance.EncryptToString(value);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005B28 File Offset: 0x00003D28
		public void DeleteUrlApplications()
		{
			XElement urlReservations = this.GetUrlReservations();
			if (urlReservations != null)
			{
				urlReservations.RemoveNodes();
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005B48 File Offset: 0x00003D48
		public void AddUrlApplicationNode(Application urlUrlApplication)
		{
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add(string.Empty, string.Empty);
			XElement xelement;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (TextWriter textWriter = new StreamWriter(memoryStream))
				{
					new XmlSerializer(urlUrlApplication.GetType()).Serialize(textWriter, urlUrlApplication, xmlSerializerNamespaces);
					xelement = XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
				}
			}
			this.GetUrlReservations().Add(xelement);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public bool hasUrlApplications()
		{
			foreach (XElement xelement in this.GetUrlReservations().XPathSelectElements("Application/URLs/URL"))
			{
				if (!xelement.IsEmpty && !string.IsNullOrEmpty(xelement.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005C4C File Offset: 0x00003E4C
		public void AddUrlApplications(IEnumerable<Application> urlApplications)
		{
			Logger.Info("Delete existing URL applications", Array.Empty<object>());
			this.DeleteUrlApplications();
			Logger.Info("Setting URL applications", Array.Empty<object>());
			foreach (Application application in urlApplications)
			{
				this.AddUrlApplicationNode(application);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public void SetInstanceId(InstanceId instanceId)
		{
			this._rootElement.Element("InstanceId").Value = instanceId.ToString();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005CDC File Offset: 0x00003EDC
		public Guid GetInstallationId()
		{
			XElement xelement = this._rootElement.Element("InstallationID");
			if (!string.IsNullOrEmpty(xelement.Value))
			{
				return new Guid(xelement.Value);
			}
			return Guid.Empty;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005D20 File Offset: 0x00003F20
		public void SetInstallationId(Guid installationId)
		{
			Logger.Info("Setting Installation ID to {0}", new object[] { installationId });
			this._rootElement.Element("InstallationID").Value = installationId.ToString("B");
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005D6C File Offset: 0x00003F6C
		public void AddEnterDataToConfig()
		{
			XElement dataElement = this.GetDataElement();
			if (dataElement == null)
			{
				Logger.Info("Data tag does not exist in the config file", new object[] { this._configFilePath });
				return;
			}
			if (this.GetEnterDataElement() == null)
			{
				Logger.Info("Enter Data Extension doesn't exists in the config file", Array.Empty<object>());
				new XDocument();
				XElement xelement = new XElement("Extension", new XElement("Configuration", new XElement("ConfigName", "ENTERDATA")));
				xelement.SetAttributeValue("Name", "ENTERDATA");
				xelement.SetAttributeValue("Type", "Microsoft.ReportingServices.DataExtensions.XmlDPConnection,Microsoft.ReportingServices.DataExtensions");
				dataElement.Add(xelement);
				return;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005E20 File Offset: 0x00004020
		public void AddMaxCatalogConnectionPoolSizePerProcess()
		{
			XElement serviceElement = this.GetServiceElement();
			if (serviceElement == null)
			{
				Logger.Info("Service tag does not exist in the config file", new object[] { this._configFilePath });
				return;
			}
			if (serviceElement.XPathSelectElement("//MaxCatalogConnectionPoolSizePerProcess") == null)
			{
				Logger.Info("MaxCatalogConnectionPoolSizePerProcess doesn't exists in the config file", Array.Empty<object>());
				XElement xelement = new XElement("MaxCatalogConnectionPoolSizePerProcess", 0);
				serviceElement.Add(xelement);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005E8A File Offset: 0x0000408A
		public XElement GetServiceElement()
		{
			return this._rootElement.XPathSelectElement("/Service");
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005E9C File Offset: 0x0000409C
		public XElement GetEnterDataElement()
		{
			return this._rootElement.XPathSelectElement("//Extension[@Name = 'ENTERDATA']");
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005EAE File Offset: 0x000040AE
		public XElement GetAccessiblePDFRendererElement()
		{
			return this._rootElement.XPathSelectElement("//Extension[@Name = 'AccessiblePDF']");
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005EC0 File Offset: 0x000040C0
		public XElement GetDataElement()
		{
			return this._rootElement.XPathSelectElement("/Extensions/Data");
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005ED2 File Offset: 0x000040D2
		public XElement GetRenderElement()
		{
			return this._rootElement.XPathSelectElement("/Extensions/Render");
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005EE4 File Offset: 0x000040E4
		public void AddAccessiblePDFRendererToConfig()
		{
			XElement renderElement = this.GetRenderElement();
			if (renderElement == null)
			{
				Logger.Info("Render tag does not exist in the config file", new object[] { this._configFilePath });
				return;
			}
			XElement accessiblePDFRendererElement = this.GetAccessiblePDFRendererElement();
			if (accessiblePDFRendererElement != null)
			{
				if (accessiblePDFRendererElement.Attribute("Type").Value != "Microsoft.ReportingServices.Rendering.ImageRenderer.AccessiblePDFRenderer,Microsoft.ReportingServices.ImageRendering")
				{
					Logger.Info("Remove exists wrong Accessible PDF Renderer in the config file", Array.Empty<object>());
					accessiblePDFRendererElement.Remove();
					Logger.Info("Add Accessible PDF Renderer Extension to the config file", Array.Empty<object>());
					XElement xelement = new XElement("Extension", new object[]
					{
						new XAttribute("Name", "AccessiblePDF"),
						new XAttribute("Type", "Microsoft.ReportingServices.Rendering.ImageRenderer.AccessiblePDFRenderer,Microsoft.ReportingServices.ImageRendering")
					});
					renderElement.Add(xelement);
					return;
				}
			}
			else
			{
				Logger.Info("Add Accessible PDF Renderer Extension to the config file", Array.Empty<object>());
				XElement xelement2 = new XElement("Extension", new object[]
				{
					new XAttribute("Name", "AccessiblePDF"),
					new XAttribute("Type", "Microsoft.ReportingServices.Rendering.ImageRenderer.AccessiblePDFRenderer,Microsoft.ReportingServices.ImageRendering")
				});
				renderElement.Add(xelement2);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006010 File Offset: 0x00004210
		public void AddEncryptFalseToConfig()
		{
			Logger.Info("Start adding Encrypt=false to the config file", Array.Empty<object>());
			string dsn = this.GetDsn();
			if (!string.IsNullOrEmpty(dsn) && dsn.IndexOf("Encrypt=", StringComparison.OrdinalIgnoreCase) == -1)
			{
				SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(dsn);
				sqlConnectionStringBuilder.Encrypt = false;
				Logger.Info("Add Encrypt False to the config file", Array.Empty<object>());
				this.SetDsn(sqlConnectionStringBuilder.ToString());
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006078 File Offset: 0x00004278
		private XElement GetUrlReservations()
		{
			XElement xelement = this._rootElement.XPathSelectElement("//URLReservations");
			if (xelement == null)
			{
				this._rootElement.Add("URLReservations");
				xelement = this._rootElement.XPathSelectElement("//URLReservations");
			}
			return xelement;
		}

		// Token: 0x04000106 RID: 262
		private readonly XElement _rootElement;

		// Token: 0x04000107 RID: 263
		private readonly string _configFilePath;

		// Token: 0x04000108 RID: 264
		private const string m_enterData = "ENTERDATA";

		// Token: 0x04000109 RID: 265
		private const string m_accessiblePDFRendererName = "AccessiblePDF";

		// Token: 0x0400010A RID: 266
		private const string m_accessiblePDFRendererType = "Microsoft.ReportingServices.Rendering.ImageRenderer.AccessiblePDFRenderer,Microsoft.ReportingServices.ImageRendering";
	}
}
