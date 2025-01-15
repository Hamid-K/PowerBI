using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlObjectModel2008.Upgrade
{
	// Token: 0x02000075 RID: 117
	internal class UpgradeSerializerSettings2008 : RdlSerializerSettings
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00016E3A File Offset: 0x0001503A
		public SerializerHost2008 SerializerHost
		{
			get
			{
				return this.m_host;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00016E42 File Offset: 0x00015042
		private UpgradeSerializerSettings2008(bool serializing)
		{
			this.m_host = new SerializerHost2008(serializing);
			base.Host = this.m_host;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00016E64 File Offset: 0x00015064
		public static UpgradeSerializerSettings2008 CreateReaderSettings()
		{
			UpgradeSerializerSettings2008 upgradeSerializerSettings = new UpgradeSerializerSettings2008(false);
			upgradeSerializerSettings.ValidateXml = true;
			upgradeSerializerSettings.Normalize = false;
			upgradeSerializerSettings.XmlSchema = XmlUtils.LoadSchemaFromResourceWithNullResolver("Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade.Rdl2008ObjectModel.ReportDefinition.xsd");
			UpgradeSerializerSettings2008 upgradeSerializerSettings2 = upgradeSerializerSettings;
			upgradeSerializerSettings2.XmlValidationEventHandler = (ValidationEventHandler)Delegate.Combine(upgradeSerializerSettings2.XmlValidationEventHandler, new ValidationEventHandler(upgradeSerializerSettings.ValidationEventHandler));
			upgradeSerializerSettings.IgnoreWhitespace = false;
			return upgradeSerializerSettings;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00016EC0 File Offset: 0x000150C0
		public static UpgradeSerializerSettings2008 CreateWriterSettings()
		{
			return new UpgradeSerializerSettings2008(true);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00016EC8 File Offset: 0x000150C8
		private void ValidationEventHandler(object sender, ValidationEventArgs e)
		{
			XmlReader xmlReader = sender as XmlReader;
			if (xmlReader != null)
			{
				string text = RDLUpgrader.Get2008NamespaceURI();
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

		// Token: 0x0400010F RID: 271
		private bool m_skippingInvalidElements;

		// Token: 0x04000110 RID: 272
		private readonly SerializerHost2008 m_host;

		// Token: 0x04000111 RID: 273
		private const string m_xsdResourceId = "Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade.Rdl2008ObjectModel.ReportDefinition.xsd";

		// Token: 0x04000112 RID: 274
		private static XmlElementAttribute[] m_deserializingReportItems = new XmlElementAttribute[0];
	}
}
