using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000C2 RID: 194
	internal static class ProgramVersion00Converter
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public static XElement Convert(XElement progElement)
		{
			if (progElement.Attribute("symbol").Value != "N2")
			{
				return progElement;
			}
			XElement xelement = progElement.Elements().First<XElement>();
			XAttribute xattribute = xelement.Attribute("rule");
			XElement xelement5;
			if (xattribute == null)
			{
				if (xelement.Name != "LetNode")
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting Let rule at {0}", new object[] { xelement })));
				}
				XElement xelement2 = xelement.Element("Variable");
				XElement xelement3 = xelement.Element("NonterminalNode");
				if (xelement2 == null || xelement3 == null)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting Variable and NonterminalNode children in {0}", new object[] { xelement })));
				}
				XElement xelement4 = xelement2.Elements().First<XElement>();
				string text = xelement4.Attribute("rule").Value;
				if (!(text == "StartSubstring"))
				{
					if (!(text == "EndSubstring"))
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting StartSubstring or EndSubstring in {0}", new object[] { xelement4 })));
					}
					xelement5 = ProgramVersion00Converter.ConvertRefEndPositionPairToOutputRegion(xelement4, xelement3);
				}
				else
				{
					xelement5 = ProgramVersion00Converter.ConvertRefStartPositionPairToOutputRegion(xelement4, xelement3);
				}
			}
			else
			{
				string text = xattribute.Value;
				if (!(text == "Substring"))
				{
					if (!(text == "PositionPair"))
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting Substring or PositionPair in {0}", new object[] { xattribute })));
					}
					xelement5 = ProgramVersion00Converter.ConvertPositionPairToOutputRegion(xelement);
				}
				else
				{
					xelement5 = ProgramVersion00Converter.ConvertSubstringToOutputRegion(xelement);
				}
			}
			XElement xelement6 = new XElement(progElement.Name, new object[]
			{
				progElement.Attributes(),
				xelement5
			});
			xelement6.SetAttributeValue("symbol", "outputRegion");
			return xelement6;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000EC38 File Offset: 0x0000CE38
		private static XElement ConvertPositionPairToOutputRegion(XElement positionPairElement)
		{
			List<XElement> list = positionPairElement.Elements("NonterminalNode").ToList<XElement>();
			if (list.Count != 2)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting two NonterminalNode in {0}", new object[] { positionPairElement })));
			}
			return new XElement("LetNode", new object[]
			{
				new XAttribute("symbol", "outputRegion"),
				new XElement[]
				{
					new XElement("Variable", new object[]
					{
						new XAttribute("symbol", "s"),
						new XElement("VariableNode", new XAttribute("symbol", "v"))
					}),
					new XElement("NonterminalNode", new object[]
					{
						new XAttribute("symbol", "posPairRegion"),
						new XAttribute("rule", "PositionPairRegion"),
						new XElement("VariableNode", new XAttribute("symbol", "s")),
						new XElement[]
						{
							ProgramVersion00Converter.ConvertPosition(list[0]),
							ProgramVersion00Converter.ConvertPosition(list[1])
						}
					})
				}
			});
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		private static XElement ConvertSubstringToOutputRegion(XElement substringElement)
		{
			XElement xelement = new XElement(substringElement.Elements().First<XElement>());
			xelement.SetAttributeValue("symbol", "regexMatchRegion");
			xelement.SetAttributeValue("rule", "RegexRegion");
			ProgramVersion00Converter.RemoveDollarSign(xelement);
			xelement.Elements().First<XElement>().SetAttributeValue("symbol", "s");
			return new XElement("LetNode", new object[]
			{
				new XAttribute("symbol", "outputRegion"),
				new XElement[]
				{
					new XElement("Variable", new object[]
					{
						new XAttribute("symbol", "s"),
						new XElement("VariableNode", new XAttribute("symbol", "v"))
					}),
					xelement
				}
			});
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000EE9C File Offset: 0x0000D09C
		private static XElement ConvertRefStartPositionPairToOutputRegion(XElement startSubstringElement, XElement refStartPositionPairElement)
		{
			XElement xelement = new XElement("NonterminalNode", new object[]
			{
				new XAttribute("symbol", "posToEndRegion"),
				new XAttribute("rule", "PosToEndRegion"),
				new XElement[]
				{
					new XElement("VariableNode", new XAttribute("symbol", "s")),
					ProgramVersion00Converter.ConvertPosition(startSubstringElement.Element("NonterminalNode"))
				}
			});
			XElement xelement2 = new XElement("NonterminalNode", new object[]
			{
				new XAttribute("symbol", "startToPosRegion"),
				new XAttribute("rule", "StartToPosRegion"),
				new XElement[]
				{
					new XElement("VariableNode", new XAttribute("symbol", "s")),
					ProgramVersion00Converter.ConvertPosition(refStartPositionPairElement.Element("NonterminalNode"))
				}
			});
			return new XElement("LetNode", new object[]
			{
				new XAttribute("symbol", "outputRegion"),
				new XElement[]
				{
					new XElement("Variable", new object[]
					{
						new XAttribute("symbol", "s"),
						new XElement("VariableNode", new XAttribute("symbol", "v"))
					}),
					new XElement("LetNode", new object[]
					{
						new XAttribute("symbol", "endRefStartRegion"),
						new XElement[]
						{
							new XElement("Variable", new object[]
							{
								new XAttribute("symbol", "s"),
								xelement
							}),
							xelement2
						}
					})
				}
			});
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		private static XElement ConvertRefEndPositionPairToOutputRegion(XElement endSubstringElement, XElement refEndPositionPairElement)
		{
			XElement xelement = new XElement("NonterminalNode", new object[]
			{
				new XAttribute("symbol", "startToPosRegion"),
				new XAttribute("rule", "StartToPosRegion"),
				new XElement[]
				{
					new XElement("VariableNode", new XAttribute("symbol", "s")),
					ProgramVersion00Converter.ConvertPosition(endSubstringElement.Element("NonterminalNode"))
				}
			});
			XElement xelement2 = new XElement("NonterminalNode", new object[]
			{
				new XAttribute("symbol", "posToEndRegion"),
				new XAttribute("rule", "PosToEndRegion"),
				new XElement[]
				{
					new XElement("VariableNode", new XAttribute("symbol", "s")),
					ProgramVersion00Converter.ConvertPosition(refEndPositionPairElement.Element("NonterminalNode"))
				}
			});
			return new XElement("LetNode", new object[]
			{
				new XAttribute("symbol", "outputRegion"),
				new XElement[]
				{
					new XElement("Variable", new object[]
					{
						new XAttribute("symbol", "s"),
						new XElement("VariableNode", new XAttribute("symbol", "v"))
					}),
					new XElement("LetNode", new object[]
					{
						new XAttribute("symbol", "startRefEndRegion"),
						new XElement[]
						{
							new XElement("Variable", new object[]
							{
								new XAttribute("symbol", "s"),
								xelement
							}),
							xelement2
						}
					})
				}
			});
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		private static void RemoveDollarSign(XElement node)
		{
			XAttribute xattribute = node.Attribute("symbol");
			if (xattribute != null && xattribute.Value.StartsWith("$", StringComparison.Ordinal))
			{
				node.SetAttributeValue(xattribute.Name, xattribute.Value.Substring(1));
			}
			foreach (XElement xelement in node.Elements())
			{
				ProgramVersion00Converter.RemoveDollarSign(xelement);
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000F360 File Offset: 0x0000D560
		private static XElement ConvertPosition(XElement positionElement)
		{
			XElement xelement = new XElement(positionElement);
			ProgramVersion00Converter.RemoveDollarSign(xelement);
			string value = xelement.Attribute("rule").Value;
			if (!(value == "AbsPosSubstr"))
			{
				if (!(value == "RegPosSubstr"))
				{
					if (!(value == "AbsPosLine"))
					{
						if (!(value == "RegPosLine"))
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting AbsPosSubstr or RegPosSubstr or AbsPosLine or RegPosLine in {0}", new object[] { positionElement })));
						}
						xelement.SetAttributeValue("rule", "RegexPosition");
						XElement[] array = xelement.Elements("NonterminalNode").ToArray<XElement>();
						XElement xelement2 = array.FirstOrDefault((XElement e) => e.Attribute("rule").Value == "KthLine");
						xelement2.ReplaceWith(ProgramVersion00Converter.ConvertLToRegion(xelement2));
						XElement xelement3 = array.FirstOrDefault((XElement e) => e.Attribute("rule").Value == "RegexPair");
						if (xelement3 == null)
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting RegexPair in {0}", new object[] { xelement })));
						}
						xelement3.SetAttributeValue("symbol", "regexPair");
					}
					else
					{
						xelement.SetAttributeValue("rule", "AbsolutePosition");
						XElement xelement4 = xelement.Element("NonterminalNode");
						if (xelement4 == null)
						{
							throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting NonterminalNode in {0}", new object[] { xelement })));
						}
						xelement4.ReplaceWith(ProgramVersion00Converter.ConvertLToRegion(xelement4));
					}
				}
				else
				{
					xelement.SetAttributeValue("rule", "RegexPosition");
					XElement xelement5 = xelement.Element("VariableNode");
					if (xelement5 == null)
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting VariableNode in {0}", new object[] { xelement })));
					}
					xelement5.SetAttributeValue("symbol", "s");
					XElement xelement6 = xelement.Element("NonterminalNode");
					if (xelement6 == null)
					{
						throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting NonterminalNode in {0}", new object[] { xelement })));
					}
					xelement6.SetAttributeValue("symbol", "regexPair");
				}
			}
			else
			{
				xelement.SetAttributeValue("rule", "AbsolutePosition");
				XElement xelement7 = xelement.Element("VariableNode");
				if (xelement7 == null)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting VariableNode in {0}", new object[] { xelement })));
				}
				xelement7.SetAttributeValue("symbol", "s");
			}
			return xelement;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000F61C File Offset: 0x0000D81C
		private static XElement ConvertLToRegion(XElement lNode)
		{
			string value = lNode.Attribute("rule").Value;
			if (!(value == "KthBoolLine"))
			{
				if (!(value == "KthLine"))
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting KthBoolLine or KthBoolLine in {0}", new object[] { lNode })));
				}
				XElement xelement = new XElement(lNode);
				xelement.SetAttributeValue("symbol", "lineRegion");
				ProgramVersion00Converter.UpdateSplitLines(xelement.Element("NonterminalNode"));
				return new XElement("LetNode", new object[]
				{
					new XAttribute("symbol", "region"),
					new XElement[]
					{
						new XElement("Variable", new object[]
						{
							new XAttribute("symbol", "inputStr"),
							new XElement("VariableNode", new XAttribute("symbol", "s"))
						}),
						xelement
					}
				});
			}
			else
			{
				XElement xelement2 = new XElement(lNode);
				xelement2.SetAttributeValue("symbol", "lineRegion");
				XElement xelement3 = xelement2.Element("NonterminalNode");
				if (xelement3 != null)
				{
					xelement3.SetAttributeValue("symbol", "boolLineSequence");
				}
				if (xelement3 != null)
				{
					xelement3.SetAttributeValue("rule", "FilterBoolLineSquence");
				}
				XElement[] array = ((xelement3 != null) ? xelement3.Elements("NonterminalNode").ToArray<XElement>() : null);
				XElement xelement4;
				if (array == null)
				{
					xelement4 = null;
				}
				else
				{
					xelement4 = array.FirstOrDefault((XElement e) => e.Attribute("symbol").Value == "BB");
				}
				XElement xelement5 = xelement4;
				if (xelement5 == null)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting BB in {0}", new object[] { array })));
				}
				xelement5.SetAttributeValue("symbol", "basicLinePredicate");
				XElement xelement6 = xelement5.Element("VariableNode");
				if (xelement6 != null)
				{
					xelement6.SetAttributeValue("symbol", "s");
				}
				XElement xelement7 = array.FirstOrDefault((XElement e) => e.Attribute("symbol").Value == "AL");
				if (xelement7 == null)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Expecting AL in {0}", new object[] { array })));
				}
				ProgramVersion00Converter.UpdateSplitLines(xelement7);
				return new XElement("LetNode", new object[]
				{
					new XAttribute("symbol", "region"),
					new XElement[]
					{
						new XElement("Variable", new object[]
						{
							new XAttribute("symbol", "inputStr"),
							new XElement("VariableNode", new XAttribute("symbol", "s"))
						}),
						xelement2
					}
				});
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000F92E File Offset: 0x0000DB2E
		private static void UpdateSplitLines(XElement splitLinesNode)
		{
			splitLinesNode.SetAttributeValue("symbol", "allLines");
			XElement xelement = splitLinesNode.Element("VariableNode");
			if (xelement == null)
			{
				return;
			}
			xelement.SetAttributeValue("symbol", "inputStr");
		}
	}
}
