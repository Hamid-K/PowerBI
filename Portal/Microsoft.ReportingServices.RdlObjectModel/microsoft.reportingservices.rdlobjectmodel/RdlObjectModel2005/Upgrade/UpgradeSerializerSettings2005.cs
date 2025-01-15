using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlObjectModel2005.Upgrade
{
	// Token: 0x02000058 RID: 88
	internal class UpgradeSerializerSettings2005 : RdlSerializerSettings
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000154F4 File Offset: 0x000136F4
		public SerializerHost2005 SerializerHost
		{
			get
			{
				return this.m_host;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000154FC File Offset: 0x000136FC
		private UpgradeSerializerSettings2005(bool serializing)
		{
			this.m_host = new SerializerHost2005(serializing);
			base.Host = this.m_host;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001551C File Offset: 0x0001371C
		public static UpgradeSerializerSettings2005 CreateReaderSettings()
		{
			UpgradeSerializerSettings2005 upgradeSerializerSettings = new UpgradeSerializerSettings2005(false);
			upgradeSerializerSettings.ValidateXml = true;
			upgradeSerializerSettings.Normalize = false;
			upgradeSerializerSettings.XmlSchema = XmlUtils.LoadSchemaFromResourceWithNullResolver("Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade.Rdl2005ObjectModel.ReportDefinition.xsd");
			UpgradeSerializerSettings2005 upgradeSerializerSettings2 = upgradeSerializerSettings;
			upgradeSerializerSettings2.XmlValidationEventHandler = (ValidationEventHandler)Delegate.Combine(upgradeSerializerSettings2.XmlValidationEventHandler, new ValidationEventHandler(upgradeSerializerSettings.ValidationEventHandler));
			upgradeSerializerSettings.IgnoreWhitespace = true;
			XmlAttributeOverrides xmlAttributeOverrides = new XmlAttributeOverrides();
			upgradeSerializerSettings.XmlAttributeOverrides = xmlAttributeOverrides;
			XmlAttributes xmlAttributes = new XmlAttributes();
			foreach (XmlElementAttribute xmlElementAttribute in UpgradeSerializerSettings2005.m_deserializingReportItems)
			{
				xmlAttributes.XmlElements.Add(xmlElementAttribute);
			}
			xmlAttributeOverrides.Add(typeof(ReportItem), xmlAttributes);
			xmlAttributes = new XmlAttributes();
			xmlAttributes.XmlElements.Add(new XmlElementAttribute("SortBy", typeof(SortBy2005)));
			xmlAttributeOverrides.Add(typeof(SortExpression), xmlAttributes);
			return upgradeSerializerSettings;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000155FF File Offset: 0x000137FF
		public static UpgradeSerializerSettings2005 CreateWriterSettings()
		{
			return new UpgradeSerializerSettings2005(true);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00015608 File Offset: 0x00013808
		private void ValidationEventHandler(object sender, ValidationEventArgs e)
		{
			XmlReader xmlReader = sender as XmlReader;
			if (xmlReader != null)
			{
				string text = RDLUpgrader.Get2005NamespaceURI();
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

		// Token: 0x040000FB RID: 251
		private bool m_skippingInvalidElements;

		// Token: 0x040000FC RID: 252
		private readonly SerializerHost2005 m_host;

		// Token: 0x040000FD RID: 253
		private const string m_xsdResourceId = "Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade.Rdl2005ObjectModel.ReportDefinition.xsd";

		// Token: 0x040000FE RID: 254
		private static readonly XmlElementAttribute[] m_deserializingReportItems = new XmlElementAttribute[]
		{
			new XmlElementClassAttribute("Line", typeof(Line2005)),
			new XmlElementClassAttribute("Rectangle", typeof(Rectangle2005)),
			new XmlElementClassAttribute("Textbox", typeof(Textbox2005)),
			new XmlElementClassAttribute("Image", typeof(Image2005)),
			new XmlElementClassAttribute("Subreport", typeof(Subreport2005)),
			new XmlElementClassAttribute("Chart", typeof(Chart2005)),
			new XmlElementClassAttribute("List", typeof(List2005)),
			new XmlElementClassAttribute("Table", typeof(Table2005)),
			new XmlElementClassAttribute("Matrix", typeof(Matrix2005)),
			new XmlElementClassAttribute("CustomReportItem", typeof(CustomReportItem2005))
		};
	}
}
