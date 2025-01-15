using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlObjectModel2010.Upgrade
{
	// Token: 0x0200006E RID: 110
	internal class UpgradeSerializerSettings2010 : RdlSerializerSettings
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00016981 File Offset: 0x00014B81
		public SerializerHost2010 SerializerHost
		{
			get
			{
				return this.m_host;
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00016989 File Offset: 0x00014B89
		private UpgradeSerializerSettings2010(bool serializing)
		{
			this.m_host = new SerializerHost2010(serializing);
			base.Host = this.m_host;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000169AC File Offset: 0x00014BAC
		public static UpgradeSerializerSettings2010 CreateReaderSettings()
		{
			UpgradeSerializerSettings2010 upgradeSerializerSettings = new UpgradeSerializerSettings2010(false);
			upgradeSerializerSettings.ValidateXml = true;
			upgradeSerializerSettings.Normalize = false;
			upgradeSerializerSettings.XmlSchema = XmlUtils.LoadSchemaFromResourceWithNullResolver("Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade.Rdl2010ObjectModel.ReportDefinition.xsd");
			UpgradeSerializerSettings2010 upgradeSerializerSettings2 = upgradeSerializerSettings;
			upgradeSerializerSettings2.XmlValidationEventHandler = (ValidationEventHandler)Delegate.Combine(upgradeSerializerSettings2.XmlValidationEventHandler, new ValidationEventHandler(upgradeSerializerSettings.ValidationEventHandler));
			upgradeSerializerSettings.IgnoreWhitespace = false;
			return upgradeSerializerSettings;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00016A08 File Offset: 0x00014C08
		public static UpgradeSerializerSettings2010 CreateWriterSettings()
		{
			return new UpgradeSerializerSettings2010(true);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00016A10 File Offset: 0x00014C10
		private void ValidationEventHandler(object sender, ValidationEventArgs e)
		{
			XmlReader xmlReader = sender as XmlReader;
			if (xmlReader != null)
			{
				string text = RDLUpgrader.Get2010NamespaceURI();
				if (xmlReader.NamespaceURI == text)
				{
					throw e.Exception;
				}
				if (!this.m_skippingInvalidElements)
				{
					this.m_skippingInvalidElements = true;
					StringBuilder stringBuilder = new StringBuilder();
					while (!xmlReader.EOF && (xmlReader.NodeType == XmlNodeType.Element || xmlReader.NodeType == XmlNodeType.Text) && xmlReader.NamespaceURI != text)
					{
						if (xmlReader.NodeType == XmlNodeType.Text)
						{
							stringBuilder.Append(xmlReader.ReadString());
						}
						else
						{
							xmlReader.Skip();
						}
						xmlReader.MoveToContent();
					}
					this.m_host.ExtraStringData = stringBuilder.ToString();
					this.m_skippingInvalidElements = false;
				}
			}
		}

		// Token: 0x04000107 RID: 263
		private bool m_skippingInvalidElements;

		// Token: 0x04000108 RID: 264
		private readonly SerializerHost2010 m_host;

		// Token: 0x04000109 RID: 265
		private const string m_xsdResourceId = "Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade.Rdl2010ObjectModel.ReportDefinition.xsd";

		// Token: 0x0400010A RID: 266
		private static XmlElementAttribute[] m_deserializingReportItems = new XmlElementAttribute[0];
	}
}
