using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000030 RID: 48
	internal class NamespacesMgr
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000CB34 File Offset: 0x0000AD34
		static NamespacesMgr()
		{
			XmlReader xmlReader = null;
			try
			{
				xmlReader = XmlReader.Create(new StringReader(NamespacesMgr.NamespaceCompatibilityXmlSchema));
				RowsetFormatter.DataSetCreator dataSetCreator = new RowsetFormatter.DataSetCreator(null, false, null, NamespacesMgr.columnNameLookupTable);
				FormattersHelpers.LoadSchema(xmlReader, new ColumnDefinitionDelegate(dataSetCreator.ConstructDataSetColumnDelegate));
				NamespacesMgr.compatibilityDataSet = dataSetCreator.DataSet;
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000CDC0 File Offset: 0x0000AFC0
		public bool IsNamespaceSkippable(string theNamespace)
		{
			if (this.namespacesToIgnore == null)
			{
				for (int i = 0; i < NamespacesMgr.known.Length; i++)
				{
					if (NamespacesMgr.known[i] == theNamespace)
					{
						return false;
					}
				}
				return true;
			}
			return this.namespacesToIgnore.ContainsKey(theNamespace);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CE08 File Offset: 0x0000B008
		private bool IsKnown(string theNamespace)
		{
			for (int i = 0; i < NamespacesMgr.known.Length; i++)
			{
				if (NamespacesMgr.known[i] == theNamespace)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CE3C File Offset: 0x0000B03C
		internal void PopulateIgnorableNamespaces(XmlReader reader)
		{
			if (!reader.IsStartElement("NamespaceCompatibility", "http://schemas.microsoft.com/analysisservices/2003/engine"))
			{
				this.namespacesToIgnore = null;
				return;
			}
			this.namespacesToIgnore = new Hashtable();
			DataSet dataSet = NamespacesMgr.compatibilityDataSet.Clone();
			reader.ReadStartElement("NamespaceCompatibility", "http://schemas.microsoft.com/analysisservices/2003/engine");
			if (!reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset"))
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1} element, got {2}", "urn:schemas-microsoft-com:xml-analysis:rowset", "root", reader.Name));
			}
			if (reader.IsEmptyElement)
			{
				reader.ReadStartElement();
			}
			else
			{
				reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
				new RowsetFormatter(NamespacesMgr.columnNameLookupTable).PopulateDataset(reader, "row", "urn:schemas-microsoft-com:xml-analysis:rowset", InlineErrorHandlingType.StoreInCell, dataSet.Tables["rowsetTable"]);
				foreach (object obj in dataSet.Tables["rowsetTable"].Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text = dataRow["Namespace"] as string;
					if (text != null)
					{
						DataRow[] childRows = dataRow.GetChildRows("rowsetTableCompatibility");
						if (childRows.Length != 0)
						{
							foreach (DataRow dataRow2 in childRows)
							{
								string text2 = dataRow2["ProductBranch"] as string;
								object obj2 = dataRow2["VersionTimeStamp"];
								DateTime dateTime = ((obj2 == null || obj2 is DBNull) ? DateTime.MinValue : ((DateTime)obj2));
								if ((text2 == "MicrosoftAnalysisServices" || text2 == "xmla") && dateTime <= NamespacesMgr.AdomdVersionTimeStamp && !this.IsKnown(text))
								{
									this.namespacesToIgnore[text] = 1;
								}
							}
						}
						else if (!this.IsKnown(text))
						{
							this.namespacesToIgnore[text] = 1;
						}
					}
				}
				reader.ReadEndElement();
			}
			reader.ReadEndElement();
		}

		// Token: 0x040001FE RID: 510
		internal const string NamespaceCompatibilityElementName = "NamespaceCompatibility";

		// Token: 0x040001FF RID: 511
		internal const string NamespaceCompatibilityNamespace = "http://schemas.microsoft.com/analysisservices/2003/engine";

		// Token: 0x04000200 RID: 512
		private const string NamespaceElementName = "Namespace";

		// Token: 0x04000201 RID: 513
		private const string CompatibilityElementName = "Compatibility";

		// Token: 0x04000202 RID: 514
		private const string ProductBranchElementName = "ProductBranch";

		// Token: 0x04000203 RID: 515
		private const string VersionTimeStampElementName = "VersionTimeStamp";

		// Token: 0x04000204 RID: 516
		private const string MsasBranch = "MicrosoftAnalysisServices";

		// Token: 0x04000205 RID: 517
		private const string XmlaBranch = "xmla";

		// Token: 0x04000206 RID: 518
		internal const string NamespaceCompatibilityRequest = "<NamespaceCompatibility xmlns=\"http://schemas.microsoft.com/analysisservices/2003/xmla\" mustUnderstand=\"0\"/>";

		// Token: 0x04000207 RID: 519
		private static readonly DateTime AdomdVersionTimeStamp = new DateTime(2003, 11, 1, CultureInfo.InvariantCulture.Calendar);

		// Token: 0x04000208 RID: 520
		private const string NamespaceCompatibilityXmlSchemaTemplate = "<xsd:schema targetNamespace=\"{0}\" xmlns = \"{1}\" xmlns:sql=\"{2}\" xmlns:xsd=\"{3}\" elementFormDefault=\"qualified\">\r\n                <xsd:element name=\"{4}\">\r\n                    <xsd:complexType>\r\n                        <xsd:sequence minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n                            <xsd:element name=\"{5}\" type=\"{5}\"/>\r\n                        </xsd:sequence>\r\n                    </xsd:complexType>\r\n                </xsd:element>\r\n                <xsd:complexType name=\"{5}\">\r\n                    <xsd:sequence>\r\n                        <xsd:element sql:field=\"{6}\" name=\"{6}\" type=\"xsd:string\"/>\r\n                        <xsd:element sql:field=\"{7}\" name=\"{7}\" minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n                            <xsd:complexType>\r\n                                <xsd:sequence>\r\n                                    <xsd:element sql:field=\"{8}\" name=\"{8}\" type=\"xsd:string\" minOccurs=\"0\"/>\r\n                                    <xsd:element sql:field=\"{9}\" name=\"{9}\" type=\"xsd:dateTime\" minOccurs=\"0\"/>\r\n                                </xsd:sequence>\r\n                            </xsd:complexType>\r\n                        </xsd:element>\r\n                    </xsd:sequence>\r\n                </xsd:complexType>\r\n            </xsd:schema>";

		// Token: 0x04000209 RID: 521
		private static readonly string NamespaceCompatibilityXmlSchema = string.Format(CultureInfo.InvariantCulture, "<xsd:schema targetNamespace=\"{0}\" xmlns = \"{1}\" xmlns:sql=\"{2}\" xmlns:xsd=\"{3}\" elementFormDefault=\"qualified\">\r\n                <xsd:element name=\"{4}\">\r\n                    <xsd:complexType>\r\n                        <xsd:sequence minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n                            <xsd:element name=\"{5}\" type=\"{5}\"/>\r\n                        </xsd:sequence>\r\n                    </xsd:complexType>\r\n                </xsd:element>\r\n                <xsd:complexType name=\"{5}\">\r\n                    <xsd:sequence>\r\n                        <xsd:element sql:field=\"{6}\" name=\"{6}\" type=\"xsd:string\"/>\r\n                        <xsd:element sql:field=\"{7}\" name=\"{7}\" minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n                            <xsd:complexType>\r\n                                <xsd:sequence>\r\n                                    <xsd:element sql:field=\"{8}\" name=\"{8}\" type=\"xsd:string\" minOccurs=\"0\"/>\r\n                                    <xsd:element sql:field=\"{9}\" name=\"{9}\" type=\"xsd:dateTime\" minOccurs=\"0\"/>\r\n                                </xsd:sequence>\r\n                            </xsd:complexType>\r\n                        </xsd:element>\r\n                    </xsd:sequence>\r\n                </xsd:complexType>\r\n            </xsd:schema>", new object[] { "urn:schemas-microsoft-com:xml-analysis:rowset", "urn:schemas-microsoft-com:xml-analysis:rowset", "urn:schemas-microsoft-com:xml-sql", "http://www.w3.org/2001/XMLSchema", "root", "row", "Namespace", "Compatibility", "ProductBranch", "VersionTimeStamp" });

		// Token: 0x0400020A RID: 522
		private static readonly DataSet compatibilityDataSet = null;

		// Token: 0x0400020B RID: 523
		private static readonly string[] known = new string[]
		{
			"http://schemas.xmlsoap.org/soap/envelope/", "urn:schemas-microsoft-com:xml-analysis", "http://schemas.microsoft.com/analysisservices/2003/engine", "http://schemas.microsoft.com/analysisservices/2003/engine/2", "http://schemas.microsoft.com/analysisservices/2003/engine/2/2", "http://schemas.microsoft.com/analysisservices/2008/engine/100", "http://schemas.microsoft.com/analysisservices/2008/engine/100/100", "http://schemas.microsoft.com/analysisservices/2010/engine/200", "http://schemas.microsoft.com/analysisservices/2010/engine/200/200", "http://schemas.microsoft.com/analysisservices/2011/engine/300",
			"http://schemas.microsoft.com/analysisservices/2011/engine/300/300", "http://schemas.microsoft.com/analysisservices/2012/engine/400", "http://schemas.microsoft.com/analysisservices/2012/engine/400/400", "http://schemas.microsoft.com/analysisservices/2012/engine/410", "http://schemas.microsoft.com/analysisservices/2012/engine/410/410", "http://schemas.microsoft.com/analysisservices/2013/engine/500", "http://schemas.microsoft.com/analysisservices/2013/engine/500/500", "http://schemas.microsoft.com/analysisservices/2013/engine/600", "http://schemas.microsoft.com/analysisservices/2013/engine/600/600", "http://schemas.microsoft.com/analysisservices/2019/engine/900",
			"http://schemas.microsoft.com/analysisservices/2019/engine/900/900", "http://schemas.microsoft.com/analysisservices/2020/engine/910", "http://schemas.microsoft.com/analysisservices/2020/engine/910/910", "http://schemas.microsoft.com/analysisservices/2020/engine/920", "http://schemas.microsoft.com/analysisservices/2020/engine/920/920", "http://schemas.microsoft.com/analysisservices/2021/engine/921", "http://schemas.microsoft.com/analysisservices/2021/engine/921/921", "http://schemas.microsoft.com/analysisservices/2022/engine/922", "http://schemas.microsoft.com/analysisservices/2022/engine/922/922", "urn:schemas-microsoft-com:xml-analysis:rowset",
			"urn:schemas-microsoft-com:xml-analysis:mddataset", "urn:schemas-microsoft-com:xml-analysis:exception", "http://www.w3.org/2001/XMLSchema", "http://www.w3.org/2001/XMLSchema-instance", "urn:schemas-microsoft-com:xml-analysis:empty", "http://schemas.microsoft.com/analysisservices/2003/xmla", "http://schemas.microsoft.com/analysisservices/2003/ext", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults", "urn:schemas-microsoft-com:xml-analysis:fault", "",
			"urn:schemas-microsoft-com:xml-analysis:xmlDocumentDataset", "urn:schemas-microsoft-com:xml-sql"
		};

		// Token: 0x0400020C RID: 524
		private static readonly Dictionary<string, IDictionary<string, string>> columnNameLookupTable = new Dictionary<string, IDictionary<string, string>>();

		// Token: 0x0400020D RID: 525
		private Hashtable namespacesToIgnore;
	}
}
