using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000393 RID: 915
	internal sealed class RmlValidatingReader : Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader
	{
		// Token: 0x06002538 RID: 9528 RVA: 0x000B2070 File Offset: 0x000B0270
		internal RmlValidatingReader(Stream stream, List<Pair<string, Stream>> namespaceSchemaStreamMap, PublishingErrorContext errorContext, RmlValidatingReader.ItemType itemType, PublishingVersioning publishingVersioning = null)
			: base(stream, namespaceSchemaStreamMap)
		{
			this.m_rdlElementHierarchy = new Stack<Pair<string, string>>();
			this.m_microversioningValidationStructureElements = RmlValidatingReader.SetupMicroVersioningValidationStructureForElements();
			this.m_microversioningValidationStructureAttributes = RmlValidatingReader.SetupMicroVersioningValidationStructureForAttributes();
			this.m_publishingVersioning = publishingVersioning;
			this.SetupMicroVersioningSchemas();
			base.ValidationEventHandler += this.ValidationCallBack;
			this.m_errorContext = errorContext;
			this.m_itemType = itemType;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000B20D8 File Offset: 0x000B02D8
		public override bool Read()
		{
			bool flag;
			try
			{
				if (RmlValidatingReader.CustomFlags.AfterCustomElement != this.m_custom)
				{
					base.Read();
					string text;
					if (!base.Validate(out text))
					{
						this.RegisterErrorAndThrow(text);
					}
					if (this.m_itemType == RmlValidatingReader.ItemType.Rdl || this.m_itemType == RmlValidatingReader.ItemType.Rdlx)
					{
						if (!this.RdlAdditionElementLocationValidation(out text))
						{
							this.RegisterErrorAndThrow(text);
						}
						if (!this.RdlAdditionAttributeLocationValidation(out text))
						{
							this.RegisterErrorAndThrow(text);
						}
						if (!this.ForceLaxSkippedValidation(out text))
						{
							this.RegisterErrorAndThrow(text);
						}
					}
				}
				else
				{
					this.m_custom = RmlValidatingReader.CustomFlags.None;
				}
				if (RmlValidatingReader.CustomFlags.InCustomElement != this.m_custom)
				{
					while (!base.EOF && XmlNodeType.Element == base.NodeType && !ListUtils.ContainsWithOrdinalComparer(base.NamespaceURI, this.m_validationNamespaceList))
					{
						this.Skip();
					}
				}
				flag = !base.EOF;
			}
			catch (ArgumentException ex)
			{
				this.RegisterErrorAndThrow(ex.Message);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000B21B8 File Offset: 0x000B03B8
		private static NameValueCollection SetupMicroVersioningValidationStructureForElements()
		{
			NameValueCollection nameValueCollection = new NameValueCollection(StringComparer.Ordinal);
			RmlValidatingReader.SetMicroVersionValidationStructure(nameValueCollection, "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition");
			RmlValidatingReader.SetMicroVersionValidationStructure(nameValueCollection, "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition");
			return nameValueCollection;
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000B21DC File Offset: 0x000B03DC
		private static void SetMicroVersionValidationStructure(NameValueCollection validationStructure, string expandToThisNamespace)
		{
			validationStructure.Add(RmlValidatingReader.GetExpandedName("CanScroll", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Tablix", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("CanScrollVertically", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Textbox", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("NaturalGroup", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Group", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("NaturalSort", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("SortExpression", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("DeferredSort", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("SortExpression", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationship", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Tablix", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationship", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Chart", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationship", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("MapDataRegion", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationship", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("GaugePanel", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationship", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("CustomData", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationship", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Group", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationship", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Relationships", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationships", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("TablixCell", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationships", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPoint", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Relationships", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("DataCell", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("DataSetName", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Group", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("DataSetName", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("TablixCell", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("DataSetName", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPoint", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("DataSetName", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("DataCell", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("DefaultRelationships", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("DataSet", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("BandLayoutOptions", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Tablix", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("LeftMargin", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Tablix", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("RightMargin", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Tablix", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("TopMargin", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Tablix", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("BottomMargin", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Tablix", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("DataSetName", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("LabelData", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("HighlightX", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPointValues", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("HighlightY", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPointValues", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("HighlightSize", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPointValues", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("AggregateIndicatorField", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Field", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("NullsAsBlanks", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("DataSet", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("CollationCulture", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("DataSet", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Tag", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Image", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Subtype", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartSeries", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("EmbeddingMode", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Image", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("EmbeddingMode", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("BackgroundImage", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("LayoutDirection", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ReportSection", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("FontFamily", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Style", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("EnableDrilldown", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("TablixRowHierarchy", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("EnableDrilldown", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("TablixColumnHierarchy", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("BackgroundColor", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Style", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Color", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Style", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("EnableDrilldown", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartCategoryHierarchy", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("BackgroundRepeat", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("BackgroundImage", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Transparency", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"), RmlValidatingReader.GetExpandedName("BackgroundImage", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("KeyFields", "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition"), RmlValidatingReader.GetExpandedName("LabelData", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("Tags", "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Image", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("FormatX", "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPointValues", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("FormatY", "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPointValues", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("FormatSize", "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPointValues", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("CurrencyLanguageX", "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPointValues", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("CurrencyLanguageY", "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPointValues", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("CurrencyLanguageSize", "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition"), RmlValidatingReader.GetExpandedName("ChartDataPointValues", expandToThisNamespace));
			validationStructure.Add(RmlValidatingReader.GetExpandedName("CurrencyLanguage", "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition"), RmlValidatingReader.GetExpandedName("Style", expandToThisNamespace));
			if (expandToThisNamespace == "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition")
			{
				RmlValidatingReader.SetMicroVersionValidationStructureForRDL2016(validationStructure);
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000B28C8 File Offset: 0x000B0AC8
		private static void SetMicroVersionValidationStructureForRDL2016(NameValueCollection validationStructure)
		{
			validationStructure.Add(RmlValidatingReader.GetExpandedName("DefaultFontFamily", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition/defaultfontfamily"), RmlValidatingReader.GetExpandedName("Report", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition"));
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000B28F0 File Offset: 0x000B0AF0
		private static NameValueCollection SetupMicroVersioningValidationStructureForAttributes()
		{
			return new NameValueCollection(StringComparer.Ordinal)
			{
				{
					RmlValidatingReader.GetExpandedName("Name", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"),
					RmlValidatingReader.GetExpandedName("ReportSection", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition")
				},
				{
					RmlValidatingReader.GetExpandedName("Name", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"),
					RmlValidatingReader.GetExpandedName("ReportSection", "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition")
				},
				{
					RmlValidatingReader.GetExpandedName("ValueType", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"),
					RmlValidatingReader.GetExpandedName("FontFamily", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition")
				},
				{
					RmlValidatingReader.GetExpandedName("ValueType", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"),
					RmlValidatingReader.GetExpandedName("Color", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition")
				},
				{
					RmlValidatingReader.GetExpandedName("ValueType", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition"),
					RmlValidatingReader.GetExpandedName("BackgroundColor", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition")
				}
			};
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000B29BB File Offset: 0x000B0BBB
		private void SetupMicroVersioningSchemas()
		{
			this.m_serverSupportedSchemas = new List<string> { "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition/defaultfontfamily" };
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000B29DE File Offset: 0x000B0BDE
		private static string GetExpandedName(string localName, string namespaceURI)
		{
			StringBuilder stringBuilder = new StringBuilder(namespaceURI);
			stringBuilder.Append(":");
			stringBuilder.Append(localName);
			return stringBuilder.ToString();
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000B2A00 File Offset: 0x000B0C00
		private bool RdlAdditionElementLocationValidation(out string message)
		{
			Pair<string, string>? pair = null;
			bool flag = false;
			message = null;
			if (ListUtils.ContainsWithOrdinalComparer(this.NamespaceURI, this.m_validationNamespaceList))
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						this.m_rdlElementHierarchy.Pop();
					}
				}
				else
				{
					string expandedName = RmlValidatingReader.GetExpandedName(this.LocalName, this.NamespaceURI);
					bool flag2 = this.IsPowerViewMicroVersionedNamespace();
					if (this.ShouldPerformMicroversionCheck(expandedName) && (this.m_itemType == RmlValidatingReader.ItemType.Rdl || this.m_itemType == RmlValidatingReader.ItemType.Rsd) && flag2)
					{
						message = RDLValidatingReaderStringsWrapper.rdlValidationInvalidNamespaceElement(expandedName, this.NamespaceURI, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
						return false;
					}
					if (this.m_rdlElementHierarchy.Count > 0)
					{
						pair = new Pair<string, string>?(this.m_rdlElementHierarchy.Peek());
					}
					if (pair == null)
					{
						Global.Tracer.Assert(this.LocalName == "Report", "(this.LocalName == Constants.Report)");
						Global.Tracer.Assert(this.NamespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition" || this.NamespaceURI == "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition", "(this.NamespaceURI == Constants.RDL2010NamespaceURI) || (this.NamespaceURI == Constants.RDL2016NamespaceURI)");
					}
					if (!this.IsEmptyElement)
					{
						this.m_rdlElementHierarchy.Push(new Pair<string, string>(expandedName, this.NamespaceURI));
					}
					if (flag2)
					{
						string[] values = this.m_microversioningValidationStructureElements.GetValues(expandedName);
						if (values != null)
						{
							for (int i = 0; i < values.Length; i++)
							{
								if (pair.Value.First.Equals(values[i], StringComparison.Ordinal))
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								message = RDLValidatingReaderStringsWrapper.rdlValidationInvalidParent(this.Name, this.NamespaceURI, pair.Value.First, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
								return false;
							}
						}
						else if (!pair.Value.Second.Equals(this.NamespaceURI, StringComparison.Ordinal))
						{
							message = RDLValidatingReaderStringsWrapper.rdlValidationInvalidMicroVersionedElement(this.Name, pair.Value.First, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000B2C7F File Offset: 0x000B0E7F
		private bool ShouldPerformMicroversionCheck(string currentRdlElement)
		{
			if (currentRdlElement != RmlValidatingReader.GetExpandedName("AggregateIndicatorField", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition"))
			{
				return true;
			}
			PublishingVersioning publishingVersioning = this.m_publishingVersioning;
			return publishingVersioning == null || publishingVersioning.IsRdlFeatureRestricted(RdlFeatures.AggregateIndicatorField);
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000B2CB0 File Offset: 0x000B0EB0
		private bool RdlAdditionAttributeLocationValidation(out string message)
		{
			message = null;
			HashSet<string> hashSet = null;
			if (this.NodeType == XmlNodeType.Element && this.HasAttributes)
			{
				string expandedName = RmlValidatingReader.GetExpandedName(this.LocalName, this.NamespaceURI);
				string namespaceURI = this.NamespaceURI;
				if (string.CompareOrdinal(expandedName, RmlValidatingReader.GetExpandedName("Report", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition")) == 0 && base.GetAttributeLocalName("MustUnderstand") != null)
				{
					hashSet = new HashSet<string>(base.GetAttributeLocalName("MustUnderstand").Split(Array.Empty<char>()));
				}
				while (this.MoveToNextAttribute())
				{
					string text = this.NamespaceURI;
					if (string.IsNullOrEmpty(text))
					{
						text = namespaceURI;
					}
					if (this.IsMicroVersionedAttributeNamespace(text))
					{
						string expandedName2 = RmlValidatingReader.GetExpandedName(this.LocalName, text);
						if (this.m_itemType == RmlValidatingReader.ItemType.Rdl || this.m_itemType == RmlValidatingReader.ItemType.Rsd)
						{
							message = RDLValidatingReaderStringsWrapper.rdlValidationInvalidNamespaceAttribute(expandedName2, text, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
						}
						else
						{
							string[] values = this.m_microversioningValidationStructureAttributes.GetValues(expandedName2);
							if (values != null)
							{
								for (int i = 0; i < values.Length; i++)
								{
									if (values[i].Equals(expandedName, StringComparison.Ordinal))
									{
										this.MoveToElement();
										return true;
									}
								}
							}
							message = RDLValidatingReaderStringsWrapper.rdlValidationInvalidMicroVersionedAttribute(expandedName2, expandedName, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
						}
					}
					if (hashSet != null && this.Prefix == "xmlns" && hashSet.Contains(this.LocalName))
					{
						hashSet.Remove(this.LocalName);
						if (!this.m_serverSupportedSchemas.Contains(this.Value))
						{
							message = RDLValidatingReaderStringsWrapper.rdlValidationUnsupportedSchema(this.Value, this.LocalName, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
							this.m_errorContext.Register(ProcessingErrorCode.rsInvalidMustUnderstandNamespaces, Severity.Error, ObjectType.Report, null, "MustUnderstand", new string[] { message });
							throw new ReportProcessingException(this.m_errorContext.Messages);
						}
					}
				}
				if (hashSet != null && hashSet.Count != 0)
				{
					if (hashSet.Count == 1)
					{
						message = RDLValidatingReaderStringsWrapper.rdlValidationUndefinedSchemaNamespace(hashSet.First<string>(), "MustUnderstand", base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidMustUnderstandNamespaces, Severity.Error, ObjectType.Report, null, "MustUnderstand", new string[] { message });
						throw new ReportProcessingException(this.m_errorContext.Messages);
					}
					message = RDLValidatingReaderStringsWrapper.rdlValidationMultipleUndefinedSchemaNamespaces(string.Join(", ", hashSet.ToArray<string>()), "MustUnderstand", base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), base.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidMustUnderstandNamespaces, Severity.Error, ObjectType.Report, null, "MustUnderstand", new string[] { message });
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				else
				{
					this.MoveToElement();
				}
			}
			return message == null;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000B3024 File Offset: 0x000B1224
		private bool IsPowerViewMicroVersionedNamespace()
		{
			Global.Tracer.Assert(ListUtils.ContainsWithOrdinalComparer(this.NamespaceURI, this.m_validationNamespaceList), "Not rdl namespace: " + this.NamespaceURI);
			return string.CompareOrdinal(this.NamespaceURI, "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition") == 0 || string.CompareOrdinal(this.NamespaceURI, "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition") == 0 || string.CompareOrdinal(this.NamespaceURI, "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition") == 0;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000B3095 File Offset: 0x000B1295
		private bool IsMicroVersionedAttributeNamespace(string namespaceUri)
		{
			return string.CompareOrdinal(namespaceUri, "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition") == 0;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000B30A8 File Offset: 0x000B12A8
		private bool ForceLaxSkippedValidation(out string message)
		{
			bool flag = true;
			message = null;
			if (Microsoft.ReportingServices.ReportPublishing.RDLValidatingReader.m_processContent == XmlSchemaContentProcessing.Lax && this.m_reader.NodeType == XmlNodeType.EndElement && this.m_reader.SchemaInfo != null && this.m_reader.SchemaInfo.Validity == XmlSchemaValidity.NotKnown && ListUtils.ContainsWithOrdinalComparer(this.m_reader.NamespaceURI, this.m_validationNamespaceList))
			{
				flag = false;
				message = RDLValidatingReaderStringsWrapper.rdlValidationNoElementDecl(RmlValidatingReader.GetExpandedName(this.m_reader.LocalName, this.m_reader.NamespaceURI), this.m_reader.LocalName, this.m_reader.NamespaceURI, base.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat));
			}
			return flag;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000B3161 File Offset: 0x000B1361
		public override string ReadString()
		{
			if (base.IsEmptyElement)
			{
				return string.Empty;
			}
			return base.ReadString();
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000B3178 File Offset: 0x000B1378
		internal bool ReadBoolean(ObjectType objectType, string objectName, string propertyName)
		{
			bool flag = false;
			if (base.IsEmptyElement)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidBooleanConstant, Severity.Error, objectType, objectName, propertyName, new string[] { string.Empty });
			}
			else
			{
				string text = base.ReadString();
				try
				{
					flag = XmlConvert.ToBoolean(text);
				}
				catch
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidBooleanConstant, Severity.Error, objectType, objectName, propertyName, new string[] { text });
				}
			}
			return flag;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000B31F4 File Offset: 0x000B13F4
		internal int ReadInteger(ObjectType objectType, string objectName, string propertyName)
		{
			int num = 0;
			if (base.IsEmptyElement)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidIntegerConstant, Severity.Error, objectType, objectName, propertyName, new string[] { string.Empty });
			}
			else
			{
				string text = base.ReadString();
				try
				{
					num = XmlConvert.ToInt32(text);
				}
				catch
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidIntegerConstant, Severity.Error, objectType, objectName, propertyName, new string[] { text });
				}
			}
			return num;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000B3270 File Offset: 0x000B1470
		internal string ReadCustomXml()
		{
			Global.Tracer.Assert(this.m_custom == RmlValidatingReader.CustomFlags.None);
			if (base.IsEmptyElement)
			{
				return string.Empty;
			}
			this.m_custom = RmlValidatingReader.CustomFlags.InCustomElement;
			string text = base.ReadInnerXml();
			this.m_custom = RmlValidatingReader.CustomFlags.AfterCustomElement;
			return text;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000B32A7 File Offset: 0x000B14A7
		private void ValidationCallBack(object sender, ValidationEventArgs args)
		{
			if (ListUtils.ContainsWithOrdinalComparer(base.NamespaceURI, this.m_validationNamespaceList))
			{
				this.RegisterErrorAndThrow(args.Message);
				return;
			}
			XmlNodeType nodeType = base.NodeType;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000B32D4 File Offset: 0x000B14D4
		private void RegisterErrorAndThrow(string message)
		{
			if (this.m_itemType == RmlValidatingReader.ItemType.Rdl || this.m_itemType == RmlValidatingReader.ItemType.Rdlx)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidReportDefinition, Severity.Error, ObjectType.Report, null, null, new string[] { message });
				throw new ReportProcessingException(this.m_errorContext.Messages);
			}
			this.m_errorContext.Register(ProcessingErrorCode.rsInvalidSharedDataSetDefinition, Severity.Error, ObjectType.SharedDataSet, null, null, new string[] { message });
			throw new DataSetPublishingException(this.m_errorContext.Messages);
		}

		// Token: 0x040015CF RID: 5583
		private RmlValidatingReader.CustomFlags m_custom;

		// Token: 0x040015D0 RID: 5584
		private PublishingErrorContext m_errorContext;

		// Token: 0x040015D1 RID: 5585
		private RmlValidatingReader.ItemType m_itemType;

		// Token: 0x040015D2 RID: 5586
		private readonly NameValueCollection m_microversioningValidationStructureElements;

		// Token: 0x040015D3 RID: 5587
		private readonly NameValueCollection m_microversioningValidationStructureAttributes;

		// Token: 0x040015D4 RID: 5588
		private readonly PublishingVersioning m_publishingVersioning;

		// Token: 0x040015D5 RID: 5589
		private readonly Stack<Pair<string, string>> m_rdlElementHierarchy;

		// Token: 0x040015D6 RID: 5590
		private List<string> m_serverSupportedSchemas;

		// Token: 0x0200095A RID: 2394
		internal enum CustomFlags
		{
			// Token: 0x04004081 RID: 16513
			None,
			// Token: 0x04004082 RID: 16514
			InCustomElement,
			// Token: 0x04004083 RID: 16515
			AfterCustomElement
		}

		// Token: 0x0200095B RID: 2395
		internal enum ItemType
		{
			// Token: 0x04004085 RID: 16517
			Rdl,
			// Token: 0x04004086 RID: 16518
			Rdlx,
			// Token: 0x04004087 RID: 16519
			Rsd
		}
	}
}
