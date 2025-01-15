using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;
using Microsoft.ReportingServices.RdlObjectModel2008.Upgrade;
using Microsoft.ReportingServices.RdlObjectModel2010.Upgrade;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200007B RID: 123
	public sealed class RDLUpgrader
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0001710E File Offset: 0x0001530E
		internal static Stream UpgradeToCurrent(XmlReader rdlReader, string namespaceURI, bool throwUpgradeException, bool upgradeDundasCRIToNative, out RDLUpgradeResult upgradeResults)
		{
			return new RDLUpgrader.RdlUpgrader().Upgrade(rdlReader, namespaceURI, throwUpgradeException, upgradeDundasCRIToNative, out upgradeResults);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00017120 File Offset: 0x00015320
		internal static Stream UpgradeToCurrent(XmlReader rdlReader, string namespaceURI, bool throwUpgradeException, bool upgradeDundasCRIToNative)
		{
			RDLUpgradeResult rdlupgradeResult = null;
			return RDLUpgrader.UpgradeToCurrent(rdlReader, namespaceURI, throwUpgradeException, upgradeDundasCRIToNative, out rdlupgradeResult);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001713C File Offset: 0x0001533C
		internal static Stream UpgradeToCurrent(Stream stream, bool throwUpgradeException)
		{
			RDLUpgradeResult rdlupgradeResult = null;
			return RDLUpgrader.UpgradeToCurrent(stream, throwUpgradeException, true, out rdlupgradeResult);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00017158 File Offset: 0x00015358
		internal static Stream UpgradeToCurrent(Stream stream, bool throwUpgradeException, bool renameInvalidDataSources)
		{
			RDLUpgradeResult rdlupgradeResult = null;
			return RDLUpgrader.UpgradeToCurrent(stream, throwUpgradeException, renameInvalidDataSources, out rdlupgradeResult);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00017171 File Offset: 0x00015371
		public static Stream UpgradeToCurrent(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return RDLUpgrader.UpgradeToCurrent(stream, false, true);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00017189 File Offset: 0x00015389
		internal static Stream UpgradeToCurrent(Stream stream, bool throwUpgradeException, bool renameInvalidDataSources, out RDLUpgradeResult upgradeResults)
		{
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Upgrade reqires Stream.CanSeek.");
			}
			return new RDLUpgrader.RdlUpgrader().Upgrade(stream, throwUpgradeException, true, renameInvalidDataSources, out upgradeResults);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000171AD File Offset: 0x000153AD
		internal static XmlReader UpgradeTo2005(XmlReader rdlReader)
		{
			return new RDLUpgrader.RdlUpgrader().UpgradeTo2005(rdlReader);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000171BC File Offset: 0x000153BC
		internal static RDLUpgradeResult CheckForDundasCRI(XmlTextReader xmlTextReader)
		{
			RDLUpgradeResult rdlupgradeResult = new RDLUpgradeResult();
			try
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.NamespaceURI.Equals(RDLUpgrader.RdlUpgrader.Get2005NamespaceURI(), StringComparison.OrdinalIgnoreCase))
				{
					while (xmlTextReader.ReadToFollowing("CustomReportItem"))
					{
						bool flag = false;
						bool flag2 = false;
						bool flag3 = false;
						bool flag4 = false;
						bool flag5 = false;
						bool flag6 = false;
						bool flag7 = false;
						bool flag8 = false;
						bool flag9 = false;
						bool flag10 = false;
						bool flag11 = false;
						xmlTextReader.ReadStartElement();
						do
						{
							if (xmlTextReader.NodeType == XmlNodeType.Element)
							{
								if (xmlTextReader.Name == "Type")
								{
									string text = xmlTextReader.ReadInnerXml();
									flag = text.Equals("DUNDASCHARTCONTROL", StringComparison.OrdinalIgnoreCase);
									flag2 = text.Equals("DUNDASGAUGECONTROL", StringComparison.OrdinalIgnoreCase);
									if (!flag && !flag2)
									{
										break;
									}
								}
								else if (xmlTextReader.Name == "CustomProperties")
								{
									xmlTextReader.ReadStartElement();
									do
									{
										if (xmlTextReader.Name == "Name")
										{
											string text2 = xmlTextReader.ReadInnerXml();
											if (text2.StartsWith("CHART.ANNOTATIONS.", StringComparison.OrdinalIgnoreCase))
											{
												flag3 = true;
											}
											if (text2.StartsWith("CHART.LEGENDS", StringComparison.OrdinalIgnoreCase) && (text2.IndexOf("LEGEND.CUSTOMITEMS.", StringComparison.OrdinalIgnoreCase) > 0 || text2.IndexOf("LEGEND.CELLCOLUMNS.", StringComparison.OrdinalIgnoreCase) > 0))
											{
												flag4 = true;
											}
											if (text2.StartsWith("GAUGECORE.NUMERICINDICATORS.", StringComparison.OrdinalIgnoreCase))
											{
												flag5 = true;
											}
											if (text2.StartsWith("GAUGECORE.STATEINDICATORS.", StringComparison.OrdinalIgnoreCase))
											{
												flag6 = true;
											}
											if (text2.StartsWith("GAUGECORE.NAMEDIMAGES.", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("GAUGECORE.IMAGES.", StringComparison.OrdinalIgnoreCase))
											{
												flag7 = true;
											}
											if (text2.StartsWith("CUSTOM_CODE_CS", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("CUSTOM_CODE_VB", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("CUSTOM_CODE_COMPILED_ASSEMBLY", StringComparison.OrdinalIgnoreCase))
											{
												flag8 = true;
											}
										}
										if (xmlTextReader.Name == "CustomProperties" && xmlTextReader.NodeType == XmlNodeType.EndElement)
										{
											break;
										}
									}
									while (xmlTextReader.Read());
								}
								else if (xmlTextReader.Name == "CustomData")
								{
									bool flag12 = false;
									bool flag13 = false;
									xmlTextReader.ReadStartElement();
									do
									{
										if (xmlTextReader.Name == "DataRowGroupings")
										{
											if (xmlTextReader.NodeType == XmlNodeType.Element)
											{
												flag12 = true;
											}
											else if (xmlTextReader.NodeType == XmlNodeType.EndElement)
											{
												flag12 = false;
											}
										}
										else if (flag12 && xmlTextReader.Name == "Name")
										{
											string text3 = xmlTextReader.ReadInnerXml();
											if (text3.Equals("ERRORFORMULA:BOXPLOT", StringComparison.OrdinalIgnoreCase) || text3.Equals("FINANCIALFORMULA:FORECASTING", StringComparison.OrdinalIgnoreCase))
											{
												flag9 = true;
											}
											if (text3.StartsWith("ERRORFORMULA", StringComparison.OrdinalIgnoreCase) || text3.StartsWith("FINANCIALFORMULA", StringComparison.OrdinalIgnoreCase) || text3.StartsWith("STATISTICALFORMULA", StringComparison.OrdinalIgnoreCase))
											{
												xmlTextReader.Skip();
												string[] array = xmlTextReader.ReadInnerXml().Split(new char[] { ';' });
												for (int i = 0; i < array.Length; i++)
												{
													if (array[i].Trim().StartsWith("SECONDARYAXIS", StringComparison.OrdinalIgnoreCase))
													{
														flag10 = true;
													}
												}
											}
										}
										else if (xmlTextReader.Name == "DataRows")
										{
											if (xmlTextReader.NodeType == XmlNodeType.Element)
											{
												flag13 = true;
											}
											else if (xmlTextReader.NodeType == XmlNodeType.EndElement)
											{
												flag13 = false;
											}
										}
										else if (flag13 && xmlTextReader.Name == "Name" && xmlTextReader.ReadInnerXml().StartsWith("CUSTOMVALUE:", StringComparison.OrdinalIgnoreCase))
										{
											flag11 = true;
										}
									}
									while ((!(xmlTextReader.Name == "CustomData") || xmlTextReader.NodeType != XmlNodeType.EndElement) && xmlTextReader.Read());
								}
							}
							xmlTextReader.Skip();
						}
						while (xmlTextReader.NodeType != XmlNodeType.EndElement);
						IL_0369:
						if (flag)
						{
							if (flag3 || flag4 || flag8 || flag9 || flag10 || flag11)
							{
								rdlupgradeResult.HasUnsupportedDundasChartFeatures = true;
								continue;
							}
							continue;
						}
						else
						{
							if (flag2 && (flag5 || flag6 || flag7 || flag8))
							{
								rdlupgradeResult.HasUnsupportedDundasGaugeFeatures = true;
								continue;
							}
							continue;
						}
						goto IL_0369;
					}
				}
			}
			catch
			{
			}
			return rdlupgradeResult;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001759C File Offset: 0x0001579C
		internal static string GetCurrentNamespaceURI()
		{
			return RDLUpgrader.RdlUpgrader.Get2016NamespaceURI();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000175A3 File Offset: 0x000157A3
		internal static string Get2010NamespaceURI()
		{
			return RDLUpgrader.RdlUpgrader.Get2010NamespaceURI();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000175AA File Offset: 0x000157AA
		internal static string Get2009NamespaceURI()
		{
			return RDLUpgrader.RdlUpgrader.Get2009NamespaceURI();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000175B1 File Offset: 0x000157B1
		internal static string Get2008NamespaceURI()
		{
			return RDLUpgrader.RdlUpgrader.Get2008NamespaceURI();
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000175B8 File Offset: 0x000157B8
		internal static string Get2007NamespaceURI()
		{
			return RDLUpgrader.RdlUpgrader.Get2007NamespaceURI();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000175BF File Offset: 0x000157BF
		internal static string Get2005NamespaceURI()
		{
			return RDLUpgrader.RdlUpgrader.Get2005NamespaceURI();
		}

		// Token: 0x0200032F RID: 815
		private sealed class RdlUpgrader
		{
			// Token: 0x0600176B RID: 5995 RVA: 0x00037889 File Offset: 0x00035A89
			internal RdlUpgrader()
			{
			}

			// Token: 0x0600176C RID: 5996 RVA: 0x00037891 File Offset: 0x00035A91
			internal static string Get2007NamespaceURI()
			{
				return "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition";
			}

			// Token: 0x0600176D RID: 5997 RVA: 0x00037898 File Offset: 0x00035A98
			internal static string Get2008NamespaceURI()
			{
				return "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition";
			}

			// Token: 0x0600176E RID: 5998 RVA: 0x0003789F File Offset: 0x00035A9F
			internal static string Get2009NamespaceURI()
			{
				return "http://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition";
			}

			// Token: 0x0600176F RID: 5999 RVA: 0x000378A6 File Offset: 0x00035AA6
			internal static string Get2010NamespaceURI()
			{
				return "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition";
			}

			// Token: 0x06001770 RID: 6000 RVA: 0x000378AD File Offset: 0x00035AAD
			internal static string Get2016NamespaceURI()
			{
				return "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition";
			}

			// Token: 0x06001771 RID: 6001 RVA: 0x000378B4 File Offset: 0x00035AB4
			internal static string Get2005NamespaceURI()
			{
				return "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";
			}

			// Token: 0x06001772 RID: 6002 RVA: 0x000378BB File Offset: 0x00035ABB
			internal Stream Upgrade(XmlReader xmlReader, string namespaceURI, bool throwUpgradeException, bool upgradeDundasCRIToNative, out RDLUpgradeResult upgradeResults)
			{
				return this.UpgradeUnified(null, xmlReader, namespaceURI, throwUpgradeException, upgradeDundasCRIToNative, true, out upgradeResults);
			}

			// Token: 0x06001773 RID: 6003 RVA: 0x000378CC File Offset: 0x00035ACC
			internal Stream Upgrade(Stream stream, bool throwUpgradeException, bool upgradeDundasCRIToNative, bool renameInvalidDataSources, out RDLUpgradeResult upgradeResults)
			{
				return this.UpgradeUnified(stream, null, null, throwUpgradeException, upgradeDundasCRIToNative, renameInvalidDataSources, out upgradeResults);
			}

			// Token: 0x06001774 RID: 6004 RVA: 0x000378E0 File Offset: 0x00035AE0
			private Stream UpgradeUnified(Stream stream, XmlReader xmlReader, string namespaceURI, bool throwUpgradeException, bool upgradeDundasCRIToNative, bool renameInvalidDataSources, out RDLUpgradeResult upgradeResults)
			{
				if (stream == null && xmlReader == null)
				{
					throw new ArgumentNullException("Stream or XmlReader must be non-null");
				}
				if (xmlReader != null && namespaceURI == null)
				{
					throw new ArgumentException("namespaceURI must not be null if xmlReader is specified");
				}
				if (namespaceURI == null)
				{
					xmlReader = this.CreateXmlReader(stream);
					xmlReader.MoveToContent();
					namespaceURI = xmlReader.NamespaceURI;
					xmlReader.Close();
					xmlReader = null;
					stream.Seek(0L, SeekOrigin.Begin);
				}
				upgradeResults = null;
				if (namespaceURI != null)
				{
					int length = namespaceURI.Length;
					if (length != 67)
					{
						if (length == 73)
						{
							switch (namespaceURI[52])
							{
							case '0':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition"))
								{
									goto IL_0259;
								}
								goto IL_0223;
							case '1':
							case '2':
							case '4':
								goto IL_0259;
							case '3':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2003/04/reportdefinition") && !(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2003/10/reportdefinition"))
								{
									goto IL_0259;
								}
								goto IL_01AC;
							case '5':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition"))
								{
									goto IL_0259;
								}
								goto IL_01C5;
							case '6':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition"))
								{
									goto IL_0259;
								}
								goto IL_0235;
							case '7':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition"))
								{
									goto IL_0259;
								}
								this.LoadDefinitionXml(ref xmlReader, stream, true);
								this.UpgradeFrom200701();
								stream = this.SaveDefinitionXml();
								break;
							case '8':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition"))
								{
									goto IL_0259;
								}
								break;
							case '9':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition"))
								{
									goto IL_0259;
								}
								this.LoadDefinitionXml(ref xmlReader, stream, true);
								this.UpgradeFrom200901();
								stream = this.SaveDefinitionXml();
								goto IL_0223;
							default:
								goto IL_0259;
							}
							stream = this.UpgradeFrom200801(this.EnsureReaderSetup(xmlReader, stream), out upgradeResults);
							goto IL_0235;
							IL_0223:
							stream = this.UpgradeFrom201001(this.EnsureReaderSetup(xmlReader, stream), out upgradeResults);
							goto IL_0235;
						}
						if (length != 75)
						{
							goto IL_0259;
						}
						if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2003/04/reportdefinition-1"))
						{
							goto IL_0259;
						}
					}
					else
					{
						char c = namespaceURI[66];
						if (c != '1')
						{
							if (c != '2')
							{
								goto IL_0259;
							}
							if (!(namespaceURI == "http://schemas.microsoft.com/SQLServer/reporting/reportdefinition-2"))
							{
								goto IL_0259;
							}
						}
						else if (!(namespaceURI == "http://schemas.microsoft.com/SQLServer/reporting/reportdefinition-1"))
						{
							goto IL_0259;
						}
					}
					IL_01AC:
					this.LoadDefinitionXml(ref xmlReader, stream, false);
					this.UpgradeTo200501();
					stream = this.SaveDefinitionXml();
					IL_01C5:
					stream = this.UpgradeFrom200501(this.EnsureReaderSetup(xmlReader, stream), throwUpgradeException, upgradeDundasCRIToNative, renameInvalidDataSources, out upgradeResults);
					IL_0235:
					if (upgradeResults == null)
					{
						upgradeResults = new RDLUpgradeResult();
					}
					if (stream == null)
					{
						this.LoadDefinitionXml(ref xmlReader, stream, true);
						stream = this.SaveDefinitionXml();
					}
					return stream;
				}
				IL_0259:
				throw new RDLUpgradeException(RDLUpgradeStringsWrapper.rdlInvalidTargetNamespace(namespaceURI));
			}

			// Token: 0x06001775 RID: 6005 RVA: 0x00037B53 File Offset: 0x00035D53
			private XmlReader EnsureReaderSetup(XmlReader xmlReader, Stream stream)
			{
				if (xmlReader == null)
				{
					xmlReader = new XmlTextReader(stream)
					{
						DtdProcessing = DtdProcessing.Prohibit
					};
				}
				return xmlReader;
			}

			// Token: 0x06001776 RID: 6006 RVA: 0x00037B68 File Offset: 0x00035D68
			private void LoadDefinitionXml(ref XmlReader xmlReader, Stream stream, bool preserveWhitespace)
			{
				this.m_definition = XmlUtils.CreateXmlDocumentWithNullResolver();
				if (preserveWhitespace)
				{
					this.m_definition.PreserveWhitespace = true;
				}
				xmlReader = this.EnsureReaderSetup(xmlReader, stream);
				this.m_definition.Load(xmlReader);
				xmlReader.Close();
				xmlReader = null;
			}

			// Token: 0x06001777 RID: 6007 RVA: 0x00037BA8 File Offset: 0x00035DA8
			private Stream SaveDefinitionXml()
			{
				Stream stream = new MemoryStream();
				this.m_definition.Save(stream);
				this.m_definition = null;
				stream.Seek(0L, SeekOrigin.Begin);
				return stream;
			}

			// Token: 0x06001778 RID: 6008 RVA: 0x00037BDC File Offset: 0x00035DDC
			private XmlReader CreateXmlReader(Stream stream)
			{
				return XmlReader.Create(stream, new XmlReaderSettings
				{
					CheckCharacters = false
				});
			}

			// Token: 0x06001779 RID: 6009 RVA: 0x00037C00 File Offset: 0x00035E00
			private Stream UpgradeFrom200501(XmlReader xmlReader, bool throwUpgradeException, bool upgradeDundasCRIToNative, bool renameInvalidDataSources, out RDLUpgradeResult upgradeResults)
			{
				MemoryStream memoryStream = new MemoryStream();
				UpgradeImpl2005 upgradeImpl = new UpgradeImpl2005(throwUpgradeException, upgradeDundasCRIToNative, renameInvalidDataSources);
				upgradeImpl.Upgrade(xmlReader, memoryStream);
				upgradeResults = upgradeImpl.UpgradeResults;
				memoryStream.Seek(0L, SeekOrigin.Begin);
				return memoryStream;
			}

			// Token: 0x0600177A RID: 6010 RVA: 0x00037C3C File Offset: 0x00035E3C
			private Stream UpgradeFrom200801(XmlReader xmlReader, out RDLUpgradeResult upgradeResults)
			{
				MemoryStream memoryStream = new MemoryStream();
				UpgradeImpl2008 upgradeImpl = new UpgradeImpl2008();
				upgradeImpl.Upgrade(xmlReader, memoryStream);
				upgradeResults = upgradeImpl.UpgradeResults;
				memoryStream.Seek(0L, SeekOrigin.Begin);
				return memoryStream;
			}

			// Token: 0x0600177B RID: 6011 RVA: 0x00037C70 File Offset: 0x00035E70
			private Stream UpgradeFrom201001(XmlReader xmlReader, out RDLUpgradeResult upgradeResults)
			{
				MemoryStream memoryStream = new MemoryStream();
				UpgradeImpl2010 upgradeImpl = new UpgradeImpl2010();
				upgradeImpl.Upgrade(xmlReader, memoryStream);
				upgradeResults = upgradeImpl.UpgradeResults;
				memoryStream.Seek(0L, SeekOrigin.Begin);
				return memoryStream;
			}

			// Token: 0x0600177C RID: 6012 RVA: 0x00037CA4 File Offset: 0x00035EA4
			private void UpgradeFrom200701()
			{
				XmlElement documentElement = this.m_definition.DocumentElement;
				string prefixOfNamespace = documentElement.GetPrefixOfNamespace("http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition");
				string text = this.BuildTempNamespacePrefix(prefixOfNamespace, documentElement);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_definition.NameTable);
				xmlNamespaceManager.AddNamespace(text, "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition");
				foreach (object obj in documentElement.GetElementsByTagName("Textbox"))
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlNode xmlNode2 = xmlNode.SelectSingleNode(text + ":Style", xmlNamespaceManager);
					XmlNode xmlNode3 = xmlNode.SelectSingleNode(text + ":Value", xmlNamespaceManager);
					XmlElement xmlElement = this.m_definition.CreateElement(prefixOfNamespace, "Paragraphs", "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition");
					XmlElement xmlElement2 = this.m_definition.CreateElement(prefixOfNamespace, "Paragraph", "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition");
					XmlElement xmlElement3 = this.m_definition.CreateElement(prefixOfNamespace, "TextRuns", "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition");
					XmlElement xmlElement4 = this.m_definition.CreateElement(prefixOfNamespace, "TextRun", "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition");
					xmlElement3.AppendChild(xmlElement4);
					xmlElement2.AppendChild(xmlElement3);
					xmlElement.AppendChild(xmlElement2);
					xmlNode.AppendChild(xmlElement);
					if (xmlNode3 != null)
					{
						xmlNode3 = xmlNode.RemoveChild(xmlNode3);
						xmlElement4.AppendChild(xmlNode3);
					}
					else
					{
						xmlNode3 = this.m_definition.CreateElement(prefixOfNamespace, "Value", "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition");
						xmlElement4.AppendChild(xmlNode3);
					}
					if (xmlNode2 != null)
					{
						string value = xmlNode.Attributes.GetNamedItem("Name").Value;
						XmlNode xmlNode4 = this.m_definition.CreateElement(prefixOfNamespace, "Style", "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition");
						this.MoveStyleItemIfExists("LineHeight", xmlNode2, xmlNode4, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("TextAlign", xmlNode2, xmlNode4, text, xmlNamespaceManager);
						if (xmlNode4.HasChildNodes)
						{
							this.ConvertMeDotValueExpressions(xmlNode4.ChildNodes, value);
						}
						xmlElement2.AppendChild(xmlNode4);
						XmlNode xmlNode5 = this.m_definition.CreateElement(prefixOfNamespace, "Style", "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition");
						this.MoveStyleItemIfExists("FontStyle", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("FontFamily", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("FontSize", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("FontWeight", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("Format", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("TextDecoration", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("Color", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("Language", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("Calendar", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("NumeralLanguage", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						this.MoveStyleItemIfExists("NumeralVariant", xmlNode2, xmlNode5, text, xmlNamespaceManager);
						if (xmlNode5.HasChildNodes)
						{
							this.ConvertMeDotValueExpressions(xmlNode5.ChildNodes, value);
						}
						xmlElement4.AppendChild(xmlNode5);
					}
				}
				this.UpgradeCharts(documentElement, xmlNamespaceManager, "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition", prefixOfNamespace, text);
				this.UpdateNamespaceURI(documentElement, "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition", RDLUpgrader.RdlUpgrader.Get2008NamespaceURI());
			}

			// Token: 0x0600177D RID: 6013 RVA: 0x00037FD4 File Offset: 0x000361D4
			private string BuildTempNamespacePrefix(string nsPrefix, XmlElement root)
			{
				string text = nsPrefix;
				if (string.IsNullOrEmpty(text))
				{
					text = "rs";
					int num = 0;
					while (!string.IsNullOrEmpty(root.GetNamespaceOfPrefix(text)))
					{
						text = "rs" + num.ToString();
						num++;
					}
				}
				return text;
			}

			// Token: 0x0600177E RID: 6014 RVA: 0x0003801C File Offset: 0x0003621C
			private void UpgradeFrom200901()
			{
				XmlElement documentElement = this.m_definition.DocumentElement;
				string prefixOfNamespace = documentElement.GetPrefixOfNamespace("http://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition");
				string text = this.BuildTempNamespacePrefix(prefixOfNamespace, documentElement);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_definition.NameTable);
				xmlNamespaceManager.AddNamespace(text, "http://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition");
				foreach (object obj in documentElement.SelectNodes(string.Format(CultureInfo.InvariantCulture, "//{0}:Chart", text), xmlNamespaceManager))
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlNode xmlNode2 = xmlNode.SelectSingleNode(text + ":Code", xmlNamespaceManager);
					if (xmlNode2 != null)
					{
						xmlNode.RemoveChild(xmlNode2);
					}
					XmlNode xmlNode3 = xmlNode.SelectSingleNode(text + ":CodeLanguage", xmlNamespaceManager);
					if (xmlNode3 != null)
					{
						xmlNode.RemoveChild(xmlNode3);
					}
					XmlNode xmlNode4 = xmlNode.SelectSingleNode(text + ":ChartCodeParameters", xmlNamespaceManager);
					if (xmlNode4 != null)
					{
						xmlNode.RemoveChild(xmlNode4);
					}
					string text2 = string.Format(CultureInfo.InvariantCulture, "{0}:ChartAreas/{0}:ChartArea/*/{0}:ChartAxis/{0}:ChartStripLines/{0}:ChartStripLine", text);
					foreach (object obj2 in xmlNode.SelectNodes(text2, xmlNamespaceManager))
					{
						XmlNode xmlNode5 = (XmlNode)obj2;
						XmlNode xmlNode6 = xmlNode5.SelectSingleNode(text + ":TitleAngle", xmlNamespaceManager);
						if (xmlNode6 != null)
						{
							xmlNode5.RemoveChild(xmlNode6);
						}
					}
				}
				this.UpdateNamespaceURI(documentElement, "http://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition", RDLUpgrader.RdlUpgrader.Get2010NamespaceURI());
			}

			// Token: 0x0600177F RID: 6015 RVA: 0x000381E0 File Offset: 0x000363E0
			private void UpgradeCharts(XmlElement report, XmlNamespaceManager nsm, string oldNamespaceURI, string oldNsPrefix, string tempNsPrefix)
			{
				foreach (object obj in report.GetElementsByTagName("ChartCategoryHierarchy"))
				{
					XmlNode xmlNode = (XmlNode)obj;
					bool flag = false;
					XmlNode xmlNode2 = xmlNode.ParentNode.SelectSingleNode(tempNsPrefix + ":CustomProperties", nsm);
					if (xmlNode2 != null)
					{
						foreach (object obj2 in xmlNode2.ChildNodes)
						{
							XmlNode xmlNode3 = ((XmlNode)obj2).SelectSingleNode(tempNsPrefix + ":Value", nsm);
							if (xmlNode3 != null && xmlNode3.InnerText == "__Upgraded2005__")
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						bool flag2 = true;
						XmlNode xmlNode4 = xmlNode;
						do
						{
							XmlNode xmlNode5 = xmlNode4.SelectSingleNode(tempNsPrefix + ":ChartMembers", nsm);
							if (xmlNode5 == null)
							{
								if (!flag2 && xmlNode4.SelectSingleNode(tempNsPrefix + ":Group", nsm) == null && xmlNode4.ParentNode.ParentNode.SelectSingleNode(tempNsPrefix + ":Group", nsm) != null)
								{
									xmlNode4.ParentNode.ParentNode.RemoveChild(xmlNode4.ParentNode);
								}
								xmlNode4 = null;
							}
							else
							{
								XmlNodeList xmlNodeList = xmlNode5.SelectNodes(tempNsPrefix + ":ChartMember", nsm);
								if (xmlNodeList.Count == 1)
								{
									xmlNode4 = xmlNodeList[0];
									flag2 = false;
								}
								else
								{
									xmlNode4 = null;
								}
							}
						}
						while (xmlNode4 != null);
					}
				}
				foreach (object obj3 in report.GetElementsByTagName("ChartThreeDProperties"))
				{
					XmlNode xmlNode6 = (XmlNode)obj3;
					XmlNode xmlNode7 = xmlNode6.SelectSingleNode(tempNsPrefix + ":Inclination", nsm);
					XmlNode xmlNode8 = xmlNode6.SelectSingleNode(tempNsPrefix + ":Rotation", nsm);
					if (xmlNode7 != null)
					{
						this.RenameElement(oldNsPrefix, "Rotation", oldNamespaceURI, (XmlElement)xmlNode7);
					}
					if (xmlNode8 != null)
					{
						this.RenameElement(oldNsPrefix, "Inclination", oldNamespaceURI, (XmlElement)xmlNode8);
					}
					XmlNode xmlNode9 = xmlNode6.SelectSingleNode(tempNsPrefix + ":Clustered", nsm);
					if (xmlNode9 != null)
					{
						bool flag3 = false;
						if (bool.TryParse(xmlNode9.InnerText, out flag3))
						{
							xmlNode9.InnerText = (flag3 ? "false" : "true");
						}
						else
						{
							string text = xmlNode9.InnerText.Trim().TrimStart(new char[] { '=' }).Trim();
							xmlNode9.InnerText = "=NOT(" + text + ")";
						}
					}
					else
					{
						XmlNode xmlNode10 = this.m_definition.CreateElement(oldNsPrefix, "Clustered", oldNamespaceURI);
						xmlNode10.InnerText = "true";
						xmlNode6.AppendChild(xmlNode10);
					}
				}
				foreach (object obj4 in report.GetElementsByTagName("Chart"))
				{
					XmlNode xmlNode11 = (XmlNode)obj4;
					XmlNode xmlNode12 = xmlNode11.SelectSingleNode(tempNsPrefix + ":Palette", nsm);
					if (xmlNode12 != null && xmlNode12.InnerText == "GrayScale")
					{
						XmlNode xmlNode13 = xmlNode11.SelectSingleNode(tempNsPrefix + ":CustomProperties", nsm);
						if (xmlNode13 != null)
						{
							foreach (object obj5 in xmlNode13.ChildNodes)
							{
								XmlNode xmlNode14 = ((XmlNode)obj5).SelectSingleNode(tempNsPrefix + ":Value", nsm);
								if (xmlNode14 != null && xmlNode14.InnerText == "__Upgraded2005__")
								{
									XmlNode xmlNode15 = this.m_definition.CreateElement(oldNsPrefix, "PaletteHatchBehavior", oldNamespaceURI);
									xmlNode15.InnerText = "Always";
									xmlNode11.AppendChild(xmlNode15);
									break;
								}
							}
						}
					}
				}
				foreach (object obj6 in report.GetElementsByTagName("ChartEmptyPoints"))
				{
					XmlNode xmlNode16 = (XmlNode)obj6;
					XmlNode xmlNode17 = xmlNode16.SelectSingleNode(tempNsPrefix + ":ChartDataPointInLegend", nsm);
					if (xmlNode17 != null)
					{
						xmlNode16.RemoveChild(xmlNode17);
					}
				}
				XmlNodeList elementsByTagName = report.GetElementsByTagName("ChartDataPointInLegend");
				for (int i = 0; i < elementsByTagName.Count; i++)
				{
					this.RenameElement(oldNsPrefix, "ChartItemInLegend", oldNamespaceURI, (XmlElement)elementsByTagName[i]);
				}
				foreach (object obj7 in report.GetElementsByTagName("ChartSeries"))
				{
					XmlNode xmlNode18 = (XmlNode)obj7;
					XmlNode xmlNode19 = xmlNode18.SelectSingleNode(tempNsPrefix + ":LegendText", nsm);
					XmlNode xmlNode20 = xmlNode18.SelectSingleNode(tempNsPrefix + ":ToolTip", nsm);
					XmlNode xmlNode21 = xmlNode18.SelectSingleNode(tempNsPrefix + ":ActionInfo", nsm);
					XmlNode xmlNode22 = xmlNode18.SelectSingleNode(tempNsPrefix + ":HideInLegend", nsm);
					if (xmlNode19 != null || xmlNode20 != null || xmlNode21 != null || xmlNode22 != null)
					{
						XmlNode xmlNode23 = this.m_definition.CreateElement(oldNsPrefix, "ChartItemInLegend", oldNamespaceURI);
						if (xmlNode19 != null)
						{
							xmlNode23.AppendChild(xmlNode19);
						}
						if (xmlNode20 != null)
						{
							xmlNode23.AppendChild(xmlNode20);
						}
						if (xmlNode21 != null)
						{
							xmlNode23.AppendChild(xmlNode21);
						}
						if (xmlNode22 != null)
						{
							xmlNode23.AppendChild(xmlNode22);
							this.RenameElement(oldNsPrefix, "Hidden", oldNamespaceURI, (XmlElement)xmlNode22);
						}
						xmlNode18.AppendChild(xmlNode23);
					}
					XmlNode xmlNode24 = xmlNode18.SelectSingleNode(tempNsPrefix + ":ChartDataPoints", nsm);
					if (xmlNode24 != null && xmlNode18.ParentNode.LocalName != "ChartDerivedSeries")
					{
						XmlNode xmlNode25 = xmlNode18.SelectSingleNode(tempNsPrefix + ":ChartMarker", nsm);
						XmlNode xmlNode26 = xmlNode18.SelectSingleNode(tempNsPrefix + ":ChartDataLabel", nsm);
						XmlNode xmlNode27 = xmlNode18.SelectSingleNode(tempNsPrefix + ":Style", nsm);
						XmlNode xmlNode28 = null;
						XmlNode xmlNode29 = null;
						if (xmlNode27 != null)
						{
							xmlNode28 = xmlNode27.SelectSingleNode(tempNsPrefix + ":ShadowOffset", nsm);
							if (xmlNode28 != null)
							{
								xmlNode28 = xmlNode27.RemoveChild(xmlNode28);
							}
							xmlNode29 = xmlNode27.SelectSingleNode(tempNsPrefix + ":ShadowColor", nsm);
							if (xmlNode29 != null)
							{
								xmlNode29 = xmlNode27.RemoveChild(xmlNode29);
							}
						}
						foreach (object obj8 in xmlNode24.SelectNodes(tempNsPrefix + ":ChartDataPoint", nsm))
						{
							XmlNode xmlNode30 = (XmlNode)obj8;
							this.MergeElementsWithPreference(tempNsPrefix, "Style", xmlNode18, xmlNode30, nsm);
							this.MergeElementsWithPreference(tempNsPrefix, "ChartMarker", xmlNode18, xmlNode30, nsm);
							this.MergeElementsWithPreference(tempNsPrefix, "ChartDataLabel", xmlNode18, xmlNode30, nsm);
						}
						if (xmlNode27 != null)
						{
							xmlNode18.RemoveChild(xmlNode27);
						}
						if (xmlNode25 != null)
						{
							xmlNode18.RemoveChild(xmlNode25);
						}
						if (xmlNode26 != null)
						{
							xmlNode18.RemoveChild(xmlNode26);
						}
						if (xmlNode29 != null || xmlNode28 != null)
						{
							XmlNode xmlNode31 = this.m_definition.CreateElement(oldNsPrefix, "Style", oldNamespaceURI);
							if (xmlNode29 != null)
							{
								xmlNode31.AppendChild(xmlNode29);
							}
							if (xmlNode28 != null)
							{
								xmlNode31.AppendChild(xmlNode28);
							}
							xmlNode18.AppendChild(xmlNode31);
						}
					}
				}
			}

			// Token: 0x06001780 RID: 6016 RVA: 0x00038A24 File Offset: 0x00036C24
			private void MoveStyleItemIfExists(string name, XmlNode sourceStyle, XmlNode destStyle, string tempNsPrefix, XmlNamespaceManager nsManager)
			{
				XmlNode xmlNode = sourceStyle.SelectSingleNode(tempNsPrefix + ":" + name, nsManager);
				if (xmlNode != null)
				{
					xmlNode = sourceStyle.RemoveChild(xmlNode);
					destStyle.AppendChild(xmlNode);
				}
			}

			// Token: 0x06001781 RID: 6017 RVA: 0x00038A5C File Offset: 0x00036C5C
			private void ConvertMeDotValueExpressions(XmlNodeList styleNodes, string textboxName)
			{
				foreach (object obj in styleNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.HasChildNodes)
					{
						this.ConvertMeDotValueExpressions(xmlNode.ChildNodes, textboxName);
					}
					else if (xmlNode.Value != null)
					{
						xmlNode.Value = this.ConvertMeDotValue(xmlNode.Value, textboxName);
					}
				}
			}

			// Token: 0x06001782 RID: 6018 RVA: 0x00038ADC File Offset: 0x00036CDC
			private string ConvertMeDotValue(string expression, string textboxName)
			{
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				MatchCollection matchCollection = ReportRegularExpressions.Value.MeDotValueExpression.Matches(expression);
				for (int i = 0; i < matchCollection.Count; i++)
				{
					global::System.Text.RegularExpressions.Group group = matchCollection[i].Groups["medotvalue"];
					if (group.Value != null && group.Value.Length > 0)
					{
						stringBuilder.Append(expression.Substring(num, group.Index - num));
						stringBuilder.Append("ReportItems!");
						stringBuilder.Append(textboxName);
						stringBuilder.Append(".Value");
						num = group.Index + group.Length;
					}
				}
				if (num == 0)
				{
					return expression;
				}
				if (num < expression.Length)
				{
					stringBuilder.Append(expression.Substring(num));
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06001783 RID: 6019 RVA: 0x00038BB0 File Offset: 0x00036DB0
			private void MergeElementsWithPreference(string prefix, string elementToMerge, XmlNode sourceParent, XmlNode targetParent, XmlNamespaceManager nsManager)
			{
				if (sourceParent == null || targetParent == null)
				{
					return;
				}
				XmlNode xmlNode = sourceParent.SelectSingleNode(prefix + ":" + elementToMerge, nsManager);
				if (xmlNode == null)
				{
					return;
				}
				XmlNode xmlNode2 = targetParent.SelectSingleNode(prefix + ":" + elementToMerge, nsManager);
				if (xmlNode2 == null)
				{
					targetParent.AppendChild(xmlNode.CloneNode(true));
					return;
				}
				int num = 0;
				while (xmlNode.ChildNodes.Count > num)
				{
					XmlNode xmlNode3 = xmlNode.ChildNodes[num];
					if (xmlNode3.HasChildNodes)
					{
						this.MergeElementsWithPreference(prefix, xmlNode3.LocalName, xmlNode, xmlNode2, nsManager);
					}
					else if (xmlNode3.NodeType == XmlNodeType.Text && targetParent.SelectSingleNode(prefix + ":" + xmlNode3.ParentNode.LocalName, nsManager) == null)
					{
						targetParent.AppendChild(xmlNode.CloneNode(true));
					}
					num++;
				}
			}

			// Token: 0x06001784 RID: 6020 RVA: 0x00038C80 File Offset: 0x00036E80
			private void UpdateNamespaceURI(XmlNode root, string oldNamespaceURI, string newNamespaceURI)
			{
				string prefixOfNamespace = root.GetPrefixOfNamespace(oldNamespaceURI);
				string text = "xmlns";
				if (prefixOfNamespace.Length > 0)
				{
					text = text + ":" + prefixOfNamespace;
				}
				XmlAttribute xmlAttribute = root.Attributes[text];
				if (xmlAttribute != null)
				{
					xmlAttribute.Value = newNamespaceURI;
				}
			}

			// Token: 0x06001785 RID: 6021 RVA: 0x00038CC8 File Offset: 0x00036EC8
			internal XmlReader UpgradeTo2005(XmlReader xmlReader)
			{
				this.m_definition = XmlUtils.CreateXmlDocumentWithNullResolver();
				this.m_definition.Load(xmlReader);
				xmlReader.Close();
				XmlElement documentElement = this.m_definition.DocumentElement;
				if ((documentElement.NamespaceURI != "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition" && documentElement.NamespaceURI != "http://schemas.microsoft.com/sqlserver/reporting/2003/10/reportdefinition" && documentElement.NamespaceURI != "http://schemas.microsoft.com/sqlserver/reporting/2003/04/reportdefinition-1" && documentElement.NamespaceURI != "http://schemas.microsoft.com/sqlserver/reporting/2003/04/reportdefinition" && documentElement.NamespaceURI != "http://schemas.microsoft.com/SQLServer/reporting/reportdefinition-1" && documentElement.NamespaceURI != "http://schemas.microsoft.com/SQLServer/reporting/reportdefinition-2") || documentElement.LocalName != "Report")
				{
					throw new RDLUpgradeException(RDLUpgradeStringsWrapper.rdlInvalidTargetNamespace(documentElement.NamespaceURI));
				}
				this.UpgradeTo200501();
				XmlTextReader xmlTextReader;
				try
				{
					xmlTextReader = new XmlTextReader(new StringReader(this.m_definition.InnerXml));
				}
				catch (ArgumentException ex)
				{
					throw new RDLUpgradeException(RDLUpgradeStringsWrapper.rdlInvalidXmlContents(ex.Message));
				}
				xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
				return xmlTextReader;
			}

			// Token: 0x06001786 RID: 6022 RVA: 0x00038DD4 File Offset: 0x00036FD4
			private bool UpgradeTo200501()
			{
				XmlElement documentElement = this.m_definition.DocumentElement;
				string namespaceURI = documentElement.NamespaceURI;
				if (namespaceURI != null)
				{
					int length = namespaceURI.Length;
					if (length != 67)
					{
						if (length == 73)
						{
							switch (namespaceURI[52])
							{
							case '3':
								if (namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2003/04/reportdefinition")
								{
									goto IL_012B;
								}
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2003/10/reportdefinition"))
								{
									goto IL_0165;
								}
								this.UpgradeFrom200310(documentElement, namespaceURI);
								goto IL_0165;
							case '4':
							case '6':
								goto IL_0165;
							case '5':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition"))
								{
									goto IL_0165;
								}
								return false;
							case '7':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition"))
								{
									goto IL_0165;
								}
								break;
							case '8':
								if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition"))
								{
									goto IL_0165;
								}
								break;
							default:
								goto IL_0165;
							}
							return false;
						}
						if (length != 75)
						{
							goto IL_0165;
						}
						if (!(namespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2003/04/reportdefinition-1"))
						{
							goto IL_0165;
						}
						this.UpgradeFromBeta2_1(documentElement, namespaceURI);
						this.UpgradeFrom200310(documentElement, namespaceURI);
						goto IL_0165;
					}
					else
					{
						char c = namespaceURI[66];
						if (c != '1')
						{
							if (c != '2')
							{
								goto IL_0165;
							}
							if (!(namespaceURI == "http://schemas.microsoft.com/SQLServer/reporting/reportdefinition-2"))
							{
								goto IL_0165;
							}
						}
						else
						{
							if (!(namespaceURI == "http://schemas.microsoft.com/SQLServer/reporting/reportdefinition-1"))
							{
								goto IL_0165;
							}
							this.UpgradeFromM5(documentElement, namespaceURI);
							this.UpgradeFromBeta1(documentElement, namespaceURI);
							this.UpgradeFromBeta2_1(documentElement, namespaceURI);
							this.UpgradeFrom200310(documentElement, namespaceURI);
							goto IL_0165;
						}
					}
					IL_012B:
					this.UpgradeFromBeta1(documentElement, namespaceURI);
					this.UpgradeFromBeta2_1(documentElement, namespaceURI);
					this.UpgradeFrom200310(documentElement, namespaceURI);
				}
				IL_0165:
				this.UpgradeBeta2_OWC(documentElement, namespaceURI);
				this.UpdateNamespaceURI(documentElement, namespaceURI, RDLUpgrader.RdlUpgrader.Get2005NamespaceURI());
				return true;
			}

			// Token: 0x06001787 RID: 6023 RVA: 0x00038F60 File Offset: 0x00037160
			private void UpgradeFrom200310(XmlElement root, string oldNamespaceURI)
			{
				string prefixOfNamespace = root.GetPrefixOfNamespace(oldNamespaceURI);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_definition.NameTable);
				xmlNamespaceManager.AddNamespace("q", oldNamespaceURI);
				foreach (object obj in root.SelectNodes("//q:Custom", xmlNamespaceManager))
				{
					XmlNode xmlNode = (XmlNode)obj;
					this.UpgradeCustom(xmlNode.ParentNode, xmlNode, xmlNamespaceManager, oldNamespaceURI, prefixOfNamespace);
				}
				foreach (object obj2 in root.SelectNodes("//q:ReportItems", xmlNamespaceManager))
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					if (xmlNode2.ParentNode.LocalName == "CustomReportItem")
					{
						this.UpgradeCRI(xmlNode2, xmlNamespaceManager, oldNamespaceURI, prefixOfNamespace);
					}
				}
				foreach (object obj3 in root.SelectNodes("//q:HeightRatio", xmlNamespaceManager))
				{
					XmlNode xmlNode3 = (XmlNode)obj3;
					xmlNode3.ParentNode.RemoveChild(xmlNode3);
				}
			}

			// Token: 0x06001788 RID: 6024 RVA: 0x000390B8 File Offset: 0x000372B8
			private void UpgradeCustom(XmlNode parent, XmlNode custom, XmlNamespaceManager nsm, string oldNamespaceURI, string oldNsPrefix)
			{
				parent.RemoveChild(custom);
				XmlElement xmlElement = this.m_definition.CreateElement(oldNsPrefix, "CustomProperty", oldNamespaceURI);
				XmlElement xmlElement2 = this.m_definition.CreateElement(oldNsPrefix, "Name", oldNamespaceURI);
				XmlElement xmlElement3 = this.m_definition.CreateElement(oldNsPrefix, "Value", oldNamespaceURI);
				xmlElement2.InnerXml = "Custom";
				xmlElement3.InnerXml = HttpUtility.HtmlEncode(custom.InnerXml);
				xmlElement.AppendChild(xmlElement2);
				xmlElement.AppendChild(xmlElement3);
				XmlNode xmlNode = parent.SelectSingleNode("q:CustomProperties", nsm);
				if (xmlNode != null)
				{
					xmlNode.AppendChild(xmlElement);
					return;
				}
				XmlElement xmlElement4 = this.m_definition.CreateElement(oldNsPrefix, "CustomProperties", oldNamespaceURI);
				parent.AppendChild(xmlElement4);
				xmlElement4.AppendChild(xmlElement);
			}

			// Token: 0x06001789 RID: 6025 RVA: 0x0003917C File Offset: 0x0003737C
			private void UpgradeCRI(XmlNode criReportItems, XmlNamespaceManager nsm, string oldNamespaceURI, string oldNsPrefix)
			{
				XmlNode parentNode = criReportItems.ParentNode;
				XmlNode parentNode2 = parentNode.ParentNode;
				XmlAttributeCollection attributes = parentNode.Attributes;
				XmlAttribute xmlAttribute = attributes["Type"];
				attributes.Remove(xmlAttribute);
				XmlAttribute xmlAttribute2 = attributes["Name"];
				XmlElement xmlElement = this.m_definition.CreateElement(oldNsPrefix, "Type", oldNamespaceURI);
				xmlElement.InnerText = xmlAttribute.InnerText;
				parentNode.AppendChild(xmlElement);
				parentNode.RemoveChild(criReportItems);
				XmlElement xmlElement2 = this.m_definition.CreateElement(oldNsPrefix, "AltReportItem", oldNamespaceURI);
				XmlElement xmlElement3 = this.m_definition.CreateElement(oldNsPrefix, "Rectangle", oldNamespaceURI);
				xmlElement3.SetAttribute("Name", xmlAttribute2.InnerText + "UpgradedRectangle");
				parentNode.AppendChild(xmlElement2);
				xmlElement2.AppendChild(xmlElement3);
				xmlElement3.AppendChild(criReportItems);
			}

			// Token: 0x0600178A RID: 6026 RVA: 0x0003924C File Offset: 0x0003744C
			private bool UpgradeBeta2_OWC(XmlElement root, string oldNamespaceURI)
			{
				int num = 0;
				string prefixOfNamespace = root.GetPrefixOfNamespace(oldNamespaceURI);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_definition.NameTable);
				xmlNamespaceManager.AddNamespace("q", oldNamespaceURI);
				foreach (object obj in root.SelectNodes("//q:ReportItems", xmlNamespaceManager))
				{
					XmlNode xmlNode = (XmlNode)obj;
					foreach (object obj2 in xmlNode.SelectNodes("q:OWCChart", xmlNamespaceManager))
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						XmlElement xmlElement = this.m_definition.CreateElement(prefixOfNamespace, "Textbox", oldNamespaceURI);
						XmlElement xmlElement2 = this.m_definition.CreateElement(prefixOfNamespace, "Value", oldNamespaceURI);
						xmlElement2.InnerText = "OWC Chart is no longer supported.";
						xmlElement.AppendChild(xmlElement2);
						XmlAttribute xmlAttribute = xmlNode2.Attributes["Name"];
						if (xmlAttribute != null)
						{
							xmlElement.SetAttribute("Name", xmlAttribute.InnerText);
						}
						this.MoveChild(xmlNode2, xmlElement, "q:Top", xmlNamespaceManager);
						this.MoveChild(xmlNode2, xmlElement, "q:Left", xmlNamespaceManager);
						this.MoveChild(xmlNode2, xmlElement, "q:Width", xmlNamespaceManager);
						this.MoveChild(xmlNode2, xmlElement, "q:Height", xmlNamespaceManager);
						this.MoveChild(xmlNode2, xmlElement, "q:Label", xmlNamespaceManager);
						xmlNode.RemoveChild(xmlNode2);
						xmlNode.AppendChild(xmlElement);
						num++;
					}
				}
				return num > 0;
			}

			// Token: 0x0600178B RID: 6027 RVA: 0x00039418 File Offset: 0x00037618
			private void UpgradeFromBeta2_1(XmlElement root, string oldNamespaceURI)
			{
				root.GetPrefixOfNamespace(oldNamespaceURI);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_definition.NameTable);
				xmlNamespaceManager.AddNamespace("q", oldNamespaceURI);
				foreach (object obj in root.SelectNodes("//q:Grouping", xmlNamespaceManager))
				{
					XmlNode xmlNode = (XmlNode)obj;
					foreach (object obj2 in xmlNode.SelectNodes("q:NaturalGroup", xmlNamespaceManager))
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						xmlNode.RemoveChild(xmlNode2);
					}
				}
				foreach (object obj3 in root.SelectNodes("//q:Style", xmlNamespaceManager))
				{
					XmlNode xmlNode3 = (XmlNode)obj3;
					foreach (object obj4 in xmlNode3.SelectNodes("q:TextAlign", xmlNamespaceManager))
					{
						XmlNode xmlNode4 = (XmlNode)obj4;
						if (string.Equals("Justify", xmlNode4.InnerText, StringComparison.OrdinalIgnoreCase))
						{
							xmlNode3.RemoveChild(xmlNode4);
						}
					}
				}
				foreach (object obj5 in root.SelectNodes("//q:ReportItems", xmlNamespaceManager))
				{
					XmlNode xmlNode5 = (XmlNode)obj5;
					foreach (object obj6 in xmlNode5.SelectNodes("q:ActiveXControl", xmlNamespaceManager))
					{
						XmlNode xmlNode6 = (XmlNode)obj6;
						xmlNode5.RemoveChild(xmlNode6);
					}
				}
			}

			// Token: 0x0600178C RID: 6028 RVA: 0x00039644 File Offset: 0x00037844
			private void UpgradeFromBeta1(XmlElement root, string oldNamespaceURI)
			{
				string prefixOfNamespace = root.GetPrefixOfNamespace(oldNamespaceURI);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_definition.NameTable);
				xmlNamespaceManager.AddNamespace("q", oldNamespaceURI);
				foreach (object obj in root.SelectNodes("//q:Title", xmlNamespaceManager))
				{
					XmlNode xmlNode = (XmlNode)obj;
					this.RenameElement(prefixOfNamespace, "ToolTip", oldNamespaceURI, (XmlElement)xmlNode);
				}
				foreach (object obj2 in root.SelectNodes("//q:Field", xmlNamespaceManager))
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					XmlAttribute xmlAttribute = xmlNode2.Attributes["Name"];
					if (xmlAttribute != null)
					{
						XmlElement xmlElement = this.m_definition.CreateElement(prefixOfNamespace, "DataField", oldNamespaceURI);
						xmlElement.InnerText = xmlAttribute.Value;
						xmlNode2.AppendChild(xmlElement);
						xmlNode2.Attributes.Remove(xmlAttribute);
					}
					foreach (object obj3 in xmlNode2.SelectNodes("q:Alias", xmlNamespaceManager))
					{
						XmlNode xmlNode3 = (XmlNode)obj3;
						XmlAttribute xmlAttribute2 = this.m_definition.CreateAttribute("Name");
						xmlAttribute2.Value = xmlNode3.InnerText;
						xmlNode2.Attributes.Append(xmlAttribute2);
						xmlNode2.RemoveChild(xmlNode3);
					}
					foreach (object obj4 in xmlNode2.SelectNodes("q:Collation", xmlNamespaceManager))
					{
						XmlNode xmlNode4 = (XmlNode)obj4;
						xmlNode2.RemoveChild(xmlNode4);
					}
				}
				foreach (object obj5 in root.SelectNodes("//q:ConnectionProperties", xmlNamespaceManager))
				{
					foreach (object obj6 in ((XmlNode)obj5).SelectNodes("q:Extension", xmlNamespaceManager))
					{
						XmlNode xmlNode5 = (XmlNode)obj6;
						this.RenameElement(prefixOfNamespace, "DataProvider", oldNamespaceURI, (XmlElement)xmlNode5);
					}
				}
				foreach (object obj7 in root.SelectNodes("//q:Source", xmlNamespaceManager))
				{
					XmlNode xmlNode6 = (XmlNode)obj7;
					string text = xmlNode6.InnerText.ToUpperInvariant();
					if (!(text == "URL"))
					{
						if (!(text == "EMBEDDED") && !(text == "DATABASE"))
						{
						}
					}
					else
					{
						xmlNode6.InnerText = "External";
					}
				}
				foreach (object obj8 in root.SelectNodes("//q:Sorting", xmlNamespaceManager))
				{
					XmlNode xmlNode7 = (XmlNode)obj8;
					XmlNodeList xmlNodeList = xmlNode7.SelectNodes("q:NaturalSort", xmlNamespaceManager);
					if (xmlNodeList.Count != 0)
					{
						xmlNode7.RemoveChild(xmlNodeList[0]);
					}
					xmlNodeList = xmlNode7.SelectNodes("q:SortExpressions", xmlNamespaceManager);
					foreach (object obj9 in xmlNodeList)
					{
						XmlNode xmlNode8 = (XmlNode)obj9;
						foreach (object obj10 in xmlNode8.SelectNodes("q:SortBy", xmlNamespaceManager))
						{
							XmlNode xmlNode9 = (XmlNode)obj10;
							xmlNode7.AppendChild(xmlNode9);
						}
						xmlNode7.RemoveChild(xmlNode8);
					}
				}
				foreach (object obj11 in root.SelectNodes("//q:HeaderRows", xmlNamespaceManager))
				{
					XmlNode xmlNode10 = (XmlNode)obj11;
					foreach (object obj12 in xmlNode10.SelectNodes("q:HeaderRow", xmlNamespaceManager))
					{
						XmlNode xmlNode11 = (XmlNode)obj12;
						this.RenameElement(prefixOfNamespace, "TableRow", oldNamespaceURI, (XmlElement)xmlNode11);
					}
					this.RenameElement(prefixOfNamespace, "TableRows", oldNamespaceURI, (XmlElement)xmlNode10);
				}
				foreach (object obj13 in root.SelectNodes("//q:FooterRows", xmlNamespaceManager))
				{
					XmlNode xmlNode12 = (XmlNode)obj13;
					foreach (object obj14 in xmlNode12.SelectNodes("q:FooterRow", xmlNamespaceManager))
					{
						XmlNode xmlNode13 = (XmlNode)obj14;
						this.RenameElement(prefixOfNamespace, "TableRow", oldNamespaceURI, (XmlElement)xmlNode13);
					}
					this.RenameElement(prefixOfNamespace, "TableRows", oldNamespaceURI, (XmlElement)xmlNode12);
				}
				foreach (object obj15 in root.SelectNodes("//q:DetailsRows", xmlNamespaceManager))
				{
					XmlNode xmlNode14 = (XmlNode)obj15;
					foreach (object obj16 in xmlNode14.SelectNodes("q:DetailsRow", xmlNamespaceManager))
					{
						XmlNode xmlNode15 = (XmlNode)obj16;
						this.RenameElement(prefixOfNamespace, "TableRow", oldNamespaceURI, (XmlElement)xmlNode15);
					}
					this.RenameElement(prefixOfNamespace, "TableRows", oldNamespaceURI, (XmlElement)xmlNode14);
				}
				foreach (object obj17 in root.SelectNodes("//q:OWCControl", xmlNamespaceManager))
				{
					XmlNode xmlNode16 = (XmlNode)obj17;
					foreach (object obj18 in xmlNode16.SelectNodes("q:OWCType", xmlNamespaceManager))
					{
						XmlNode xmlNode17 = (XmlNode)obj18;
						xmlNode16.RemoveChild(xmlNode17);
					}
					this.RenameElement(prefixOfNamespace, "OWCChart", oldNamespaceURI, (XmlElement)xmlNode16);
				}
				foreach (object obj19 in root.SelectNodes("//q:Style", xmlNamespaceManager))
				{
					XmlNode xmlNode18 = (XmlNode)obj19;
					foreach (object obj20 in xmlNode18.SelectNodes("q:CanSort", xmlNamespaceManager))
					{
						XmlNode xmlNode19 = (XmlNode)obj20;
						xmlNode18.RemoveChild(xmlNode19);
					}
				}
				foreach (object obj21 in root.SelectNodes("//q:Hidden", xmlNamespaceManager))
				{
					XmlNode xmlNode20 = (XmlNode)obj21;
					XmlElement xmlElement2 = this.RenameElement(prefixOfNamespace, "Visibility", oldNamespaceURI, (XmlElement)xmlNode20);
					XmlNodeList xmlNodeList2 = xmlElement2.SelectNodes("q:StartVisible", xmlNamespaceManager);
					if (xmlNodeList2.Count != 0)
					{
						XmlElement xmlElement3 = (XmlElement)xmlNodeList2[0];
						try
						{
							bool flag = bool.Parse(xmlElement3.InnerText);
							xmlElement3.InnerText = (flag ? "false" : "true");
						}
						catch
						{
							string text2 = xmlElement3.InnerText.Trim().TrimStart("=".ToCharArray()).Trim();
							xmlElement3.InnerText = "=NOT(" + text2 + ")";
						}
						this.RenameElement(prefixOfNamespace, "Hidden", oldNamespaceURI, xmlElement3);
					}
					else
					{
						XmlElement xmlElement4 = this.m_definition.CreateElement(prefixOfNamespace, "Hidden", oldNamespaceURI);
						xmlElement4.InnerText = "true";
						xmlElement2.AppendChild(xmlElement4);
					}
					xmlNodeList2 = xmlElement2.SelectNodes("q:Toggle", xmlNamespaceManager);
					if (xmlNodeList2.Count != 0)
					{
						XmlNodeList xmlNodeList3 = xmlNodeList2[0].SelectNodes("q:Item", xmlNamespaceManager);
						XmlElement xmlElement5 = this.m_definition.CreateElement(prefixOfNamespace, "ToggleItem", oldNamespaceURI);
						xmlElement5.InnerText = ((XmlElement)xmlNodeList3[0]).InnerText;
						xmlElement2.ReplaceChild(xmlElement5, xmlNodeList2[0]);
					}
				}
			}

			// Token: 0x0600178D RID: 6029 RVA: 0x0003A138 File Offset: 0x00038338
			private void UpgradeFromM5(XmlElement root, string oldNamespaceURI)
			{
				string prefixOfNamespace = root.GetPrefixOfNamespace(oldNamespaceURI);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_definition.NameTable);
				xmlNamespaceManager.AddNamespace("q", oldNamespaceURI);
				foreach (object obj in root.SelectNodes("q:UseOWCCharts | q:UseOWCPivots", xmlNamespaceManager))
				{
					XmlNode xmlNode = (XmlNode)obj;
					root.RemoveChild(xmlNode);
				}
				XmlNodeList xmlNodeList = root.SelectNodes("//q:Chart", xmlNamespaceManager);
				int count = xmlNodeList.Count;
				for (int i = 0; i < count; i++)
				{
					XmlElement xmlElement = this.RenameElement(prefixOfNamespace, "OWCControl", oldNamespaceURI, (XmlElement)xmlNodeList[i]);
					XmlNodeList xmlNodeList2 = xmlElement.SelectNodes("q:ChartDefinition", xmlNamespaceManager);
					this.RenameElement(prefixOfNamespace, "OWCDefinition", oldNamespaceURI, (XmlElement)xmlNodeList2[0]);
					xmlNodeList2 = xmlElement.SelectNodes("q:ChartColumns", xmlNamespaceManager);
					if (xmlNodeList2.Count > 0)
					{
						xmlNodeList2 = this.RenameElement(prefixOfNamespace, "OWCColumns", oldNamespaceURI, (XmlElement)xmlNodeList2[0]).SelectNodes("q:ChartColumn", xmlNamespaceManager);
						for (int j = xmlNodeList2.Count - 1; j >= 0; j--)
						{
							this.RenameElement(prefixOfNamespace, "OWCColumn", oldNamespaceURI, (XmlElement)xmlNodeList2[j]);
						}
					}
					XmlElement xmlElement2 = this.m_definition.CreateElement(prefixOfNamespace, "OWCType", oldNamespaceURI);
					xmlElement2.InnerText = "Chart";
					xmlElement.AppendChild(xmlElement2);
				}
				XmlNode xmlNode2 = root.SelectSingleNode("q:DataSets", xmlNamespaceManager);
				int num = 0;
				string text = null;
				if (xmlNode2 != null)
				{
					num = xmlNode2.ChildNodes.Count;
				}
				if (num == 1)
				{
					XmlAttributeCollection attributes = xmlNode2.FirstChild.Attributes;
					if (attributes != null)
					{
						XmlAttribute xmlAttribute = attributes["Name"];
						if (xmlAttribute != null)
						{
							text = xmlAttribute.Value;
						}
					}
				}
				XmlNode xmlNode3 = root.SelectSingleNode("q:Body/q:ReportItems", xmlNamespaceManager);
				if (xmlNode3 != null)
				{
					XmlNodeList childNodes = xmlNode3.ChildNodes;
					for (int k = 0; k < childNodes.Count; k++)
					{
						XmlNode xmlNode4 = childNodes[k];
						if (string.Equals(xmlNode4.NamespaceURI, oldNamespaceURI, StringComparison.OrdinalIgnoreCase))
						{
							string localName = xmlNode4.LocalName;
							if (!(localName == "List") && !(localName == "Table") && !(localName == "Matrix"))
							{
								if (!(localName == "Rectangle") && !(localName == "Textbox"))
								{
									if (localName == "Checkbox")
									{
										xmlNode3.RemoveChild(xmlNode4);
									}
								}
								else
								{
									this.ConvertHideDuplicates(text, xmlNode4, xmlNamespaceManager);
								}
							}
							else
							{
								XmlNode xmlNode5 = xmlNode4.SelectSingleNode("q:DataSetName", xmlNamespaceManager);
								string text2;
								if (xmlNode5 != null)
								{
									text2 = xmlNode5.InnerText;
								}
								else
								{
									text2 = text;
								}
								this.ConvertHideDuplicates(text2, xmlNode4, xmlNamespaceManager);
							}
						}
					}
				}
				XmlNodeList xmlNodeList3 = root.SelectNodes("q:PageHeader | q:PageFooter", xmlNamespaceManager);
				for (int l = 0; l < xmlNodeList3.Count; l++)
				{
					this.ConvertHideDuplicates(null, xmlNodeList3[l], xmlNamespaceManager);
				}
			}

			// Token: 0x0600178E RID: 6030 RVA: 0x0003A464 File Offset: 0x00038664
			private void ConvertHideDuplicates(string dataSetName, XmlNode reportItem, XmlNamespaceManager nsm)
			{
				XmlNodeList xmlNodeList = reportItem.SelectNodes(".//q:HideDuplicates", nsm);
				for (int i = 0; i < xmlNodeList.Count; i++)
				{
					XmlNode xmlNode = xmlNodeList[i];
					bool flag = false;
					try
					{
						flag = bool.Parse(xmlNode.InnerText);
					}
					catch
					{
					}
					if (dataSetName != null && flag)
					{
						xmlNode.InnerText = dataSetName;
					}
					else
					{
						xmlNode.ParentNode.RemoveChild(xmlNode);
					}
				}
			}

			// Token: 0x0600178F RID: 6031 RVA: 0x0003A4D8 File Offset: 0x000386D8
			private void MoveChild(XmlNode oldParent, XmlElement newParent, string searchString, XmlNamespaceManager nsm)
			{
				foreach (object obj in oldParent.SelectNodes(searchString, nsm))
				{
					XmlNode xmlNode = (XmlNode)obj;
					newParent.AppendChild(xmlNode);
				}
			}

			// Token: 0x06001790 RID: 6032 RVA: 0x0003A538 File Offset: 0x00038738
			private XmlElement RenameElement(string prefix, string name, string URI, XmlElement oldElement)
			{
				XmlElement xmlElement = this.m_definition.CreateElement(prefix, name, URI);
				int count = oldElement.Attributes.Count;
				for (int i = 0; i < count; i++)
				{
					xmlElement.Attributes.Append(oldElement.Attributes[i]);
				}
				while (oldElement.ChildNodes.Count > 0)
				{
					xmlElement.AppendChild(oldElement.ChildNodes[0]);
				}
				oldElement.ParentNode.ReplaceChild(xmlElement, oldElement);
				return xmlElement;
			}

			// Token: 0x04000752 RID: 1874
			private XmlDocument m_definition;

			// Token: 0x04000753 RID: 1875
			private const string M5NamespaceURI = "http://schemas.microsoft.com/SQLServer/reporting/reportdefinition-1";

			// Token: 0x04000754 RID: 1876
			private const string M5NamespaceURI2 = "http://schemas.microsoft.com/SQLServer/reporting/reportdefinition-2";

			// Token: 0x04000755 RID: 1877
			private const string NamespaceURI200304 = "http://schemas.microsoft.com/sqlserver/reporting/2003/04/reportdefinition";

			// Token: 0x04000756 RID: 1878
			private const string NamespaceURI200304_2 = "http://schemas.microsoft.com/sqlserver/reporting/2003/04/reportdefinition-1";

			// Token: 0x04000757 RID: 1879
			private const string NamespaceURI200310 = "http://schemas.microsoft.com/sqlserver/reporting/2003/10/reportdefinition";

			// Token: 0x04000758 RID: 1880
			private const string NamespaceURI200501 = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition";

			// Token: 0x04000759 RID: 1881
			private const string NamespaceURI200701 = "http://schemas.microsoft.com/sqlserver/reporting/2007/01/reportdefinition";

			// Token: 0x0400075A RID: 1882
			private const string NamespaceURI200801 = "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition";

			// Token: 0x0400075B RID: 1883
			private const string NamespaceURI200901 = "http://schemas.microsoft.com/sqlserver/reporting/2009/01/reportdefinition";

			// Token: 0x0400075C RID: 1884
			private const string NamespaceURI201001 = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition";

			// Token: 0x0400075D RID: 1885
			private const string NamespaceURI201601 = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition";

			// Token: 0x0400075E RID: 1886
			private const string LatestNamespaceURI = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition";
		}
	}
}
