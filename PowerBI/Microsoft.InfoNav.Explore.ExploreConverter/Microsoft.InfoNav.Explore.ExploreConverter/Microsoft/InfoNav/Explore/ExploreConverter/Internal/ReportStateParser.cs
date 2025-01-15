using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000B1 RID: 177
	internal static class ReportStateParser
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x000139AC File Offset: 0x00011BAC
		public static ReportState Parse(ReportParsingDiagnosticContext diagnosticContext, Stream reportStateStream)
		{
			string text = null;
			double num = 0.0;
			List<ReportSectionState> list = new List<ReportSectionState>();
			FilterAreaVisibility filterAreaVisibility = ReportState.DefaultFilterAreaVisibility;
			using (XmlReader xmlReader = XmlReader.Create(reportStateStream, XmlUtil.CreateSafeXmlReaderSettings()))
			{
				xmlReader.MoveToContent();
				xmlReader.Read();
				while (!xmlReader.EOF)
				{
					if (xmlReader.IsStartElement())
					{
						string localName = xmlReader.LocalName;
						if (localName == "Theme")
						{
							text = xmlReader.ReadElementContentAsString();
							continue;
						}
						if (localName == "FontScale")
						{
							num = double.Parse(xmlReader.ReadElementContentAsString(), CultureInfo.InvariantCulture);
							continue;
						}
						if (localName == "FilterAreaVisibility")
						{
							XElement xelement = XNode.ReadFrom(xmlReader) as XElement;
							filterAreaVisibility = ReportStateParser.ParseFilterAreaVisibility(diagnosticContext, xelement);
							continue;
						}
						if (localName == "ReportSections")
						{
							using (XmlReader xmlReader2 = xmlReader.ReadSubtreeAndMoveToContent())
							{
								xmlReader2.Read();
								while (!xmlReader2.EOF)
								{
									if (xmlReader2.IsStartElement() && xmlReader.LocalName == "ReportSection")
									{
										using (XmlReader xmlReader3 = xmlReader.ReadSubtreeAndMoveToContent())
										{
											XElement xelement2 = XNode.ReadFrom(xmlReader3) as XElement;
											list.Add(ReportStateParser.ParseReportSectionState(xelement2));
										}
									}
									xmlReader2.Skip();
								}
							}
						}
					}
					xmlReader.Skip();
				}
			}
			return new ReportState(list, num, text, filterAreaVisibility);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00013B7C File Offset: 0x00011D7C
		public static Filter ParseFilter(XElement filterNode)
		{
			XAttribute xattribute = filterNode.AttributeByLocalName("Type");
			string text = ((xattribute == null) ? null : xattribute.Value);
			FilterMode filterMode = ReportStateParser.ParseFilterMode(filterNode);
			FilterStateType filterStateType = FilterStateType.None;
			bool flag = filterNode.ElementByLocalName("IsDrilldownFilter") != null;
			if (filterNode.ElementByLocalName("IsMeasure") != null)
			{
				filterStateType |= FilterStateType.MeasureFilter;
			}
			if (flag)
			{
				filterStateType |= FilterStateType.DrillDownFilter;
			}
			IRdmQueryExpression rdmQueryExpression = null;
			XElement xelement = filterNode.ElementByLocalName("Operand");
			if (xelement != null)
			{
				rdmQueryExpression = RdmQueryExpressionParser.ParseQueryExpression(xelement.ElementByLocalName("QueryExpression"));
			}
			CompoundFilterCondition<IRdmQueryExpression> compoundFilterCondition = null;
			XElement xelement2 = filterNode.ElementByLocalName("Condition");
			if (xelement2 != null)
			{
				XElement xelement3 = xelement2.ElementByLocalName("CompoundFilterCondition");
				if (xelement3 != null)
				{
					compoundFilterCondition = ReportStateParser.ParseCompoundFilterCondition(xelement3);
				}
			}
			return new Filter(compoundFilterCondition, text, rdmQueryExpression, filterMode, filterStateType);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013C2F File Offset: 0x00011E2F
		public static List<Filter> ParseFilters(XElement filtersNode)
		{
			return (from filter in filtersNode.ElementsByLocalName("Filter")
				select ReportStateParser.ParseFilter(filter)).ToList<Filter>();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00013C68 File Offset: 0x00011E68
		public static ReportSectionState ParseReportSectionState(XElement reportSectionStateNode)
		{
			List<Filter> list = new List<Filter>();
			Dictionary<string, ReportItemState> dictionary = new Dictionary<string, ReportItemState>();
			XElement xelement = reportSectionStateNode.ElementByLocalName("SectionFilters");
			if (xelement != null)
			{
				XElement xelement2 = xelement.ElementByLocalName("Filters");
				if (xelement2 != null)
				{
					list = ReportStateParser.ParseFilters(xelement2);
				}
			}
			XElement xelement3 = reportSectionStateNode.ElementByLocalName("ReportItems");
			if (xelement3 != null)
			{
				foreach (XElement xelement4 in xelement3.ElementsByLocalName("DataRegion"))
				{
					XAttribute xattribute = xelement4.AttributeByLocalName("Name");
					if (xattribute == null)
					{
						throw new InvalidOperationException("ReportItems require name");
					}
					List<Filter> list2 = new List<Filter>();
					ReportParsingDiagnosticContext reportParsingDiagnosticContext = new ReportParsingDiagnosticContext();
					XElement xelement5 = xelement4.ElementByLocalName("Filters");
					if (xelement5 != null)
					{
						XElement xelement6 = xelement5.ElementByLocalName("Filters");
						if (xelement6 != null)
						{
							try
							{
								list2 = ReportStateParser.ParseFilters(xelement6);
							}
							catch (ArgumentException)
							{
								reportParsingDiagnosticContext.AddError("InvalidFilter", new string[0]);
							}
						}
					}
					dictionary.Add(xattribute.Value, new ReportItemState(xattribute.ToString(), list2, reportParsingDiagnosticContext));
				}
			}
			return new ReportSectionState(list, dictionary);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00013DA0 File Offset: 0x00011FA0
		public static IFilterCondition<IRdmQueryExpression> ParseFilterCondition(XElement filterConditionNode)
		{
			XAttribute xattribute = filterConditionNode.AttributeByLocalName("type");
			if (xattribute == null)
			{
				return null;
			}
			if (xattribute.Value == "SimpleFilterCondition")
			{
				return ReportStateParser.ParseSimpleFilterCondition(filterConditionNode);
			}
			return null;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00013DD8 File Offset: 0x00011FD8
		public static FilterMode ParseFilterMode(XElement filterNode)
		{
			XElement xelement = filterNode.ElementByLocalName("Mode");
			if (xelement != null)
			{
				string value = xelement.Value;
				if (value == "Cleared")
				{
					return FilterMode.Cleared;
				}
				if (value == "Advanced")
				{
					return FilterMode.Advanced;
				}
				if (value == "List")
				{
					return FilterMode.List;
				}
				if (value == "Range")
				{
					return FilterMode.Range;
				}
			}
			throw new InvalidOperationException("Unknown FilterMode");
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00013E44 File Offset: 0x00012044
		public static FilterAreaVisibility ParseFilterAreaVisibility(ReportParsingDiagnosticContext diagnosticContext, XElement filterAreaVisibilityNode)
		{
			string value = filterAreaVisibilityNode.Value;
			if (value == "Hidden")
			{
				return FilterAreaVisibility.Hidden;
			}
			if (value == "Collapsed")
			{
				return FilterAreaVisibility.Collapsed;
			}
			if (value == "Expanded")
			{
				return FilterAreaVisibility.Expanded;
			}
			diagnosticContext.AddError("InvalidFilterAreaVisibility", new string[0]);
			return ReportState.DefaultFilterAreaVisibility;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00013E9C File Offset: 0x0001209C
		public static IFilterCondition<IRdmQueryExpression> ParseSimpleFilterCondition(XElement filterConditionNode)
		{
			XElement xelement = filterConditionNode.ElementByLocalName("LeftExpression");
			Contract.Check(xelement != null, "Expecting LeftExpression node");
			IRdmQueryExpression rdmQueryExpression = RdmQueryExpressionParser.ParseQueryExpression(xelement);
			XElement xelement2 = filterConditionNode.ElementByLocalName("Not");
			bool flag = false;
			if (xelement2 != null && bool.Parse(xelement2.Value))
			{
				flag = true;
			}
			XElement xelement3 = filterConditionNode.ElementByLocalName("RightExpression");
			if (xelement3 == null)
			{
				return new UnaryFilterCondition<IRdmQueryExpression>(rdmQueryExpression, flag);
			}
			FilterOperator filterOperator = FilterOperator.Equal;
			XElement xelement4 = filterConditionNode.ElementByLocalName("Operator");
			if (xelement4 != null)
			{
				filterOperator = ReportStateParser.ParseFilterOperator(xelement4.Value);
			}
			IRdmQueryExpression rdmQueryExpression2 = RdmQueryExpressionParser.ParseQueryExpression(xelement3);
			PrimitiveValue primitiveValue;
			if (rdmQueryExpression2 is RdmQueryLiteralExpression)
			{
				primitiveValue = (rdmQueryExpression2 as RdmQueryLiteralExpression).Value;
			}
			else
			{
				if (!(rdmQueryExpression2 is RdmQueryNullExpression))
				{
					throw new InvalidOperationException("Invalid right expression");
				}
				primitiveValue = PrimitiveValue.Null;
			}
			return new BinaryFilterCondition<IRdmQueryExpression>(rdmQueryExpression, flag, filterOperator, primitiveValue);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00013F6C File Offset: 0x0001216C
		public static FilterOperator ParseFilterOperator(string filterOperator)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(filterOperator);
			if (num <= 296430910U)
			{
				if (num <= 7219433U)
				{
					if (num != 2619683U)
					{
						if (num == 7219433U)
						{
							if (filterOperator == "DateTimeEqualToSecond")
							{
								return FilterOperator.DateTimeEqualToSecond;
							}
						}
					}
					else if (filterOperator == "LessThan")
					{
						return FilterOperator.LessThan;
					}
				}
				else if (num != 24087439U)
				{
					if (num == 296430910U)
					{
						if (filterOperator == "GreaterThan")
						{
							return FilterOperator.GreaterThan;
						}
					}
				}
				else if (filterOperator == "Equal")
				{
					return FilterOperator.Equal;
				}
			}
			else if (num <= 1721518424U)
			{
				if (num != 1519117449U)
				{
					if (num == 1721518424U)
					{
						if (filterOperator == "Contains")
						{
							return FilterOperator.Contains;
						}
					}
				}
				else if (filterOperator == "GreaterThanOrEqual")
				{
					return FilterOperator.GreaterThanOrEqual;
				}
			}
			else if (num != 2007843446U)
			{
				if (num == 3152901116U)
				{
					if (filterOperator == "StartsWith")
					{
						return FilterOperator.StartsWith;
					}
				}
			}
			else if (filterOperator == "LessThanOrEqual")
			{
				return FilterOperator.LessThanOrEqual;
			}
			throw new ArgumentException("Unknown FilterOperator", "filterOperator");
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001408C File Offset: 0x0001228C
		public static CompoundFilterCondition<IRdmQueryExpression> ParseCompoundFilterCondition(XElement compoundFilterConditionNode)
		{
			XElement xelement = compoundFilterConditionNode.ElementByLocalName("Operator");
			CompoundFilterOperator compoundFilterOperator = CompoundFilterOperator.All;
			if (xelement != null && !string.IsNullOrEmpty(xelement.Value))
			{
				string value = xelement.Value;
				if (value == "All")
				{
					compoundFilterOperator = CompoundFilterOperator.All;
				}
				else if (value == "Any")
				{
					compoundFilterOperator = CompoundFilterOperator.Any;
				}
				else if (value == "NotAll")
				{
					compoundFilterOperator = CompoundFilterOperator.NotAll;
				}
				else
				{
					if (!(value == "NotAny"))
					{
						throw new InvalidOperationException("Unknown CompoundFilterOperator");
					}
					compoundFilterOperator = CompoundFilterOperator.NotAny;
				}
			}
			XElement xelement2 = compoundFilterConditionNode.ElementByLocalName("Conditions");
			List<IFilterCondition<IRdmQueryExpression>> list = new List<IFilterCondition<IRdmQueryExpression>>();
			if (xelement2 != null)
			{
				list = (from condition in xelement2.ElementsByLocalName("FilterCondition")
					select ReportStateParser.ParseFilterCondition(condition)).ToList<IFilterCondition<IRdmQueryExpression>>();
			}
			return new CompoundFilterCondition<IRdmQueryExpression>(compoundFilterOperator, list);
		}
	}
}
