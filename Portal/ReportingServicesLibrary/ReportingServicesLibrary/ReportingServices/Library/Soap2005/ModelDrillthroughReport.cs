using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x0200030A RID: 778
	public class ModelDrillthroughReport
	{
		// Token: 0x06001B1A RID: 6938 RVA: 0x0006E0FC File Offset: 0x0006C2FC
		public ModelDrillthroughReport(string path, DrillthroughType type)
		{
			this.Path = path;
			this.Type = type;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0006E112 File Offset: 0x0006C312
		public ModelDrillthroughReport()
		{
			this.Type = DrillthroughType.Detail;
			this.Path = null;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0006E128 File Offset: 0x0006C328
		internal static string ToXml(ModelDrillthroughReport[] reports)
		{
			if (reports == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartElement("ModelDrillthroughReports");
			foreach (ModelDrillthroughReport modelDrillthroughReport in reports)
			{
				if (modelDrillthroughReport != null)
				{
					xmlTextWriter.WriteStartElement("ModelDrillthroughReport");
					xmlTextWriter.WriteAttributeString("Path", modelDrillthroughReport.Path);
					xmlTextWriter.WriteAttributeString("Type", modelDrillthroughReport.Type.ToString());
					xmlTextWriter.WriteEndElement();
				}
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0006E1C4 File Offset: 0x0006C3C4
		internal static ModelDrillthroughReport[] FromXml(string xml)
		{
			if (xml == null)
			{
				return null;
			}
			XmlTextReader xmlTextReader = XmlUtil.SafeCreateXmlTextReader(xml);
			List<ModelDrillthroughReport> list = new List<ModelDrillthroughReport>();
			try
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.NodeType != XmlNodeType.Element)
				{
					throw new InternalCatalogException("invalid XML element");
				}
				if (xmlTextReader.Name != "ModelDrillthroughReports")
				{
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "invalid XML element name: {0}", xmlTextReader.Name));
				}
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.IsStartElement())
					{
						if (xmlTextReader.Name != "ModelDrillthroughReport")
						{
							throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "invalid XML element name: {0}", xmlTextReader.Name));
						}
						if (xmlTextReader.AttributeCount != 2)
						{
							throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "invalid XML expected 2 attributes, found " + xmlTextReader.AttributeCount, Array.Empty<object>()));
						}
						string attribute = xmlTextReader.GetAttribute("Path");
						DrillthroughType drillthroughType = (DrillthroughType)Enum.Parse(typeof(DrillthroughType), xmlTextReader.GetAttribute("Type"));
						list.Add(new ModelDrillthroughReport(attribute, drillthroughType));
					}
				}
			}
			catch (XmlException ex)
			{
				throw new InternalCatalogException(ex, "malformed XML");
			}
			return list.ToArray();
		}

		// Token: 0x04000A74 RID: 2676
		public string Path;

		// Token: 0x04000A75 RID: 2677
		public DrillthroughType Type;

		// Token: 0x04000A76 RID: 2678
		private const string ModelDrillthroughReportsTag = "ModelDrillthroughReports";

		// Token: 0x04000A77 RID: 2679
		private const string ModelDrillthroughReportTag = "ModelDrillthroughReport";

		// Token: 0x04000A78 RID: 2680
		private const string ModelDrillthroughEntityName = "Entity";

		// Token: 0x04000A79 RID: 2681
		private const string ModelDrillthroughPathName = "Path";

		// Token: 0x04000A7A RID: 2682
		private const string ModelDrillthroughTypeName = "Type";
	}
}
