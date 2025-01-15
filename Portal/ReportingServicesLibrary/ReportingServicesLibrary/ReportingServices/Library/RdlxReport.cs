using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000170 RID: 368
	internal sealed class RdlxReport
	{
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x000315B8 File Offset: 0x0002F7B8
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x000315C0 File Offset: 0x0002F7C0
		internal List<ReportSection> Sections
		{
			get
			{
				return this.m_sections;
			}
			private set
			{
				this.m_sections = value;
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000315CC File Offset: 0x0002F7CC
		private static ReportSection ReadReportSection(XmlReader reader, int sectionIndex)
		{
			ReportSection reportSection = new ReportSection();
			reportSection.Name = reader.GetAttribute("Name", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition");
			if (string.IsNullOrEmpty(reportSection.Name))
			{
				reportSection.Name = "ReportSection" + sectionIndex.ToString(CultureInfo.InvariantCulture);
			}
			bool flag = false;
			do
			{
				reader.Read();
				XmlNodeType nodeType = reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if (reader.LocalName == "ReportSection")
						{
							flag = true;
						}
					}
				}
				else if (reader.LocalName == "PreviewImageRelationshipId")
				{
					reportSection.PreviewId = reader.ReadString();
				}
			}
			while (!flag);
			return reportSection;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00031674 File Offset: 0x0002F874
		internal static RdlxReport Load(Stream stream)
		{
			RdlxReport rdlxReport = new RdlxReport();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.ProhibitDtd = true;
			xmlReaderSettings.CloseInput = true;
			xmlReaderSettings.XmlResolver = new RdlxReport.XmlNullResolver();
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
				{
					xmlReader.ReadToDescendant("ReportSections");
					rdlxReport.Sections = new List<ReportSection>();
					bool flag = false;
					int num = 0;
					do
					{
						xmlReader.Read();
						XmlNodeType nodeType = xmlReader.NodeType;
						if (nodeType != XmlNodeType.Element)
						{
							if (nodeType == XmlNodeType.EndElement)
							{
								if (xmlReader.LocalName == "ReportSections")
								{
									flag = true;
								}
							}
						}
						else if (xmlReader.LocalName == "ReportSection")
						{
							rdlxReport.Sections.Add(RdlxReport.ReadReportSection(xmlReader, num));
							num++;
						}
					}
					while (!flag);
				}
			}
			catch (XmlException ex)
			{
				throw new PowerPointExportException(ErrorCode.rsInvalidXml, ex);
			}
			catch (XmlSchemaException ex2)
			{
				throw new PowerPointExportException(ErrorCode.rsInvalidXml, ex2);
			}
			return rdlxReport;
		}

		// Token: 0x0400059C RID: 1436
		private List<ReportSection> m_sections;

		// Token: 0x02000473 RID: 1139
		private sealed class XmlNullResolver : XmlUrlResolver
		{
			// Token: 0x0600238F RID: 9103 RVA: 0x00084D24 File Offset: 0x00082F24
			public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
			{
				throw new XmlException("Can't resolve URI reference.", null);
			}
		}
	}
}
