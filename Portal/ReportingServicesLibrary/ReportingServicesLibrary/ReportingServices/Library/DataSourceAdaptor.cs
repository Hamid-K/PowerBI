using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000D0 RID: 208
	internal class DataSourceAdaptor
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x00024160 File Offset: 0x00022360
		internal static DataSourceInfo ConstructFromRSDSFile(DataSourceCatalogItem item)
		{
			byte[] content = item.Content;
			if (content == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream(content);
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.Schemas.Add(DataSourceAdaptor.RSDSSchema);
			XmlReader xmlReader = null;
			DataSourceInfo dataSourceInfo3;
			try
			{
				xmlReader = XmlReader.Create(memoryStream, xmlReaderSettings);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(xmlReader);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				xmlNamespaceManager.AddNamespace("rds", DataSourceInfo.GetXmlNamespace());
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/rds:" + DataSourceInfo.GetDataSourceDefinitionXmlTag(), xmlNamespaceManager);
				DataSourceInfo dataSourceInfo = null;
				try
				{
					dataSourceInfo = new DataSourceInfo(item.ItemContext.ItemName, "", xmlNode, DataProtection.Instance);
				}
				catch (MissingElementException ex)
				{
					if (!(ex.MissingElementName == DataSourceInfo.GetUserNameXmlTag()))
					{
						throw;
					}
					DataSourceInfo dataSourceInfo2 = item.GetDataSourceInfo(false);
					string text;
					if (dataSourceInfo2 == null || dataSourceInfo2.UserNameEncrypted == null)
					{
						text = string.Empty;
					}
					else
					{
						text = dataSourceInfo2.GetUserName(DataProtection.Instance);
					}
					XmlElement xmlElement = xmlDocument.CreateElement(DataSourceInfo.GetUserNameXmlTag());
					xmlElement.InnerText = text;
					xmlNode.AppendChild(xmlElement);
					dataSourceInfo = new DataSourceInfo(item.ItemContext.ItemName, "", xmlNode, DataProtection.Instance);
					if (dataSourceInfo2 != null && DataSourceAdaptor.DataSourceRSDSInfoEqual(dataSourceInfo, dataSourceInfo2))
					{
						if (item.DataSourceInfo.PasswordEncrypted != null)
						{
							dataSourceInfo.SetPasswordFromDataSourceInfo(item.DataSourceInfo);
						}
					}
					else
					{
						if (dataSourceInfo2 == null)
						{
							Global.m_Tracer.Trace(item.ItemContext.ItemPath.Value + " not found in the catalog.");
						}
						else
						{
							Global.m_Tracer.Trace(item.ItemContext.ItemPath.Value + " has changed content in the catalog.");
						}
						dataSourceInfo.Enabled = false;
						dataSourceInfo.ResetPassword();
					}
				}
				dataSourceInfo3 = dataSourceInfo;
			}
			catch (InvalidXmlException)
			{
				throw new InvalidDataSourceSchemaException();
			}
			catch (XmlSchemaValidationException)
			{
				throw new InvalidDataSourceSchemaException();
			}
			catch (XmlException ex2)
			{
				throw new MalformedXmlException(ex2);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
			return dataSourceInfo3;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000243CC File Offset: 0x000225CC
		private static bool DataSourceRSDSInfoEqual(DataSourceInfo ds1, DataSourceInfo ds2)
		{
			byte[] xmlBytes = ds1.GetXmlBytes(DataProtection.Instance);
			byte[] xmlBytes2 = ds2.GetXmlBytes(DataProtection.Instance);
			if (xmlBytes.Length == xmlBytes2.Length)
			{
				for (int i = 0; i < xmlBytes.Length; i++)
				{
					if (xmlBytes[i] != xmlBytes2[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00024414 File Offset: 0x00022614
		internal static DataSourceInfo ConstructFromODCFile(DataSourceCatalogItem item)
		{
			byte[] content = item.Content;
			if (content == null)
			{
				return null;
			}
			string @string = Encoding.UTF8.GetString(content);
			Match match = new Regex("<xml id=msodc>(?<odc>.*?)</xml>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(@string);
			if (match.Success && match.Groups.Count > 0)
			{
				string value = match.Groups["odc"].Value;
				DataSourceInfo dataSourceInfo2;
				try
				{
					TextReader textReader = new StringReader(value);
					XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
					XmlReader xmlReader = XmlReader.Create(textReader, xmlReaderSettings);
					using (xmlReader)
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(xmlReader);
						XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
						xmlNamespaceManager.AddNamespace("odc", "urn:schemas-microsoft-com:office:odc");
						XmlNode xmlNode = xmlDocument.SelectSingleNode("/odc:OfficeDataConnection/odc:Connection[@odc:Type='ODBC']", xmlNamespaceManager);
						if (xmlNode == null)
						{
							xmlNode = xmlDocument.SelectSingleNode("/odc:OfficeDataConnection/odc:Connection[@odc:Type='OLEDB']", xmlNamespaceManager);
							if (xmlNode == null)
							{
								throw new MissingElementException("/odc:OfficeDataConnection/odc:Connection");
							}
						}
						string value2 = xmlNode.Attributes["odc:Type"].Value;
						XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/odc:OfficeDataConnection/odc:Connection/odc:CredentialsMethod", xmlNamespaceManager);
						DataSourceInfo.CredentialsRetrievalOption credentialsRetrievalOption;
						if (xmlNode2 != null)
						{
							string innerText = xmlNode2.InnerText;
							if (string.Equals(innerText, "None", StringComparison.OrdinalIgnoreCase))
							{
								credentialsRetrievalOption = DataSourceInfo.CredentialsRetrievalOption.None;
							}
							else if (string.Equals(innerText, "Integrated", StringComparison.OrdinalIgnoreCase))
							{
								credentialsRetrievalOption = DataSourceInfo.CredentialsRetrievalOption.Integrated;
							}
							else if (string.Equals(innerText, "Stored", StringComparison.OrdinalIgnoreCase))
							{
								credentialsRetrievalOption = DataSourceInfo.CredentialsRetrievalOption.Prompt;
							}
							else
							{
								credentialsRetrievalOption = DataSourceInfo.CredentialsRetrievalOption.Prompt;
							}
						}
						else
						{
							credentialsRetrievalOption = DataSourceInfo.CredentialsRetrievalOption.Integrated;
						}
						XmlNode xmlNode3 = xmlNode.SelectSingleNode("odc:ConnectionString", xmlNamespaceManager);
						if (xmlNode3 == null)
						{
							throw new MissingElementException("odc:ConnectionString");
						}
						string innerText2 = xmlNode3.InnerText;
						DataSourceInfo dataSourceInfo = new DataSourceInfo(item.ItemContext.ItemName, "");
						dataSourceInfo.Extension = value2;
						dataSourceInfo.SetConnectionString(innerText2, DataProtection.Instance);
						dataSourceInfo.CredentialsRetrieval = credentialsRetrievalOption;
						if (credentialsRetrievalOption == DataSourceInfo.CredentialsRetrievalOption.Prompt)
						{
							dataSourceInfo.ImpersonateUser = false;
							dataSourceInfo.WindowsCredentials = false;
						}
						dataSourceInfo.ValidateDefinition(false);
						dataSourceInfo2 = dataSourceInfo;
					}
				}
				catch (XmlException ex)
				{
					throw new MalformedXmlException(ex);
				}
				return dataSourceInfo2;
			}
			throw new InvalidXmlException();
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00024640 File Offset: 0x00022840
		internal static XmlSchema RSDSSchema
		{
			get
			{
				if (DataSourceAdaptor.m_rsdsSchema == null)
				{
					DataSourceAdaptor.m_rsdsSchema = XmlSchema.Read(Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.ReportingServices.Library.RSDSSchema.xsd"), null);
				}
				RSTrace.CatalogTrace.Assert(DataSourceAdaptor.m_rsdsSchema != null);
				return DataSourceAdaptor.m_rsdsSchema;
			}
		}

		// Token: 0x04000456 RID: 1110
		internal const string RSDSXmlSchemaResource = "Microsoft.ReportingServices.Library.RSDSSchema.xsd";

		// Token: 0x04000457 RID: 1111
		internal const string ODCXmlNamespace = "urn:schemas-microsoft-com:office:odc";

		// Token: 0x04000458 RID: 1112
		private static XmlSchema m_rsdsSchema;
	}
}
