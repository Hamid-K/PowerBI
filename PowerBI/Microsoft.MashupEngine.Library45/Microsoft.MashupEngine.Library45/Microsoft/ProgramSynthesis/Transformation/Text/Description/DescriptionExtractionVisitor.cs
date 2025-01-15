using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C81 RID: 7297
	internal class DescriptionExtractionVisitor : ProgramNodeVisitor<IEnumerable<TransformationDescription>>
	{
		// Token: 0x17002938 RID: 10552
		// (get) Token: 0x0600F72B RID: 63275 RVA: 0x0034A583 File Offset: 0x00348783
		public static DescriptionExtractionVisitor Instance { get; } = new DescriptionExtractionVisitor();

		// Token: 0x0600F72C RID: 63276 RVA: 0x0034A58C File Offset: 0x0034878C
		private IEnumerable<TransformationDescription> VisitNonterminal(NonterminalNode node, string columnName)
		{
			DescriptionExtractionVisitor.<>c__DisplayClass3_0 CS$<>8__locals1 = new DescriptionExtractionVisitor.<>c__DisplayClass3_0();
			CS$<>8__locals1.columnName = columnName;
			Grammar grammar = node.GrammarRule.Grammar;
			CS$<>8__locals1.build = GrammarBuilders.Instance(grammar);
			if (CS$<>8__locals1.build.Node.IsRule.WholeColumn(node))
			{
				return new WholeColumn[]
				{
					new WholeColumn(node, CS$<>8__locals1.columnName)
				};
			}
			if (node.Rule is ConversionRule)
			{
				NonterminalNode nonterminalNode = node.Children[0] as NonterminalNode;
				if (nonterminalNode != null)
				{
					return this.VisitNonterminal(nonterminalNode, CS$<>8__locals1.columnName);
				}
			}
			LetSharedParsedNumber letSharedParsedNumber;
			if (CS$<>8__locals1.build.Node.IsRule.LetSharedParsedNumber(node, out letSharedParsedNumber))
			{
				inputNumber inputNumber = letSharedParsedNumber.inputNumber;
				LetSharedNumberFormat letSharedNumberFormat = letSharedParsedNumber._LetB0.Cast_LetSharedNumberFormat();
				numberFormat numberFormat = letSharedNumberFormat.numberFormat;
				RangeConcat rangeConcat = letSharedNumberFormat.rangeString.Cast_RangeConcat(CS$<>8__locals1.build);
				RangeFormatNumber rangeFormatNumber = rangeConcat.rangeSubstring.Cast_RangeFormatNumber(CS$<>8__locals1.build);
				RangeRoundNumber rangeRoundNumber = rangeFormatNumber.rangeNumber.Cast_RangeRoundNumber();
				VariableNode variable = rangeRoundNumber.sharedParsedNumber.Variable;
				RoundingSpec value = rangeRoundNumber.roundingSpec.Value;
				VariableNode variable2 = rangeFormatNumber.sharedNumberFormat.Variable;
				RangeConcat rangeConcat2 = rangeConcat.rangeString.Cast_RangeConcat(CS$<>8__locals1.build);
				string value2 = rangeConcat2.rangeSubstring.Cast_RangeConstStr(CS$<>8__locals1.build).s.Value;
				RangeFormatNumber rangeFormatNumber2 = rangeConcat2.rangeString.Cast_rangeString_rangeSubstring(CS$<>8__locals1.build).rangeSubstring.Cast_RangeFormatNumber(CS$<>8__locals1.build);
				RangeRoundNumber rangeRoundNumber2 = rangeFormatNumber2.rangeNumber.Cast_RangeRoundNumber();
				VariableNode variable3 = rangeRoundNumber2.sharedParsedNumber.Variable;
				RoundingSpec value3 = rangeRoundNumber2.roundingSpec.Value;
				VariableNode variable4 = rangeFormatNumber2.sharedNumberFormat.Variable;
				NumberFormat numberFormat2 = (NumberFormat)numberFormat.Node.Invoke(null);
				TransformationDescription transformationDescription = new FormatNumericRange(letSharedNumberFormat.Node, numberFormat2, value2, value, value3, CS$<>8__locals1.columnName);
				return this.VisitNonterminal((NonterminalNode)inputNumber.Node, CS$<>8__locals1.columnName).AppendItem(transformationDescription);
			}
			LetSharedParsedDateTime letSharedParsedDateTime;
			if (CS$<>8__locals1.build.Node.IsRule.LetSharedParsedDateTime(node, out letSharedParsedDateTime))
			{
				inputDateTime inputDateTime = letSharedParsedDateTime.inputDateTime;
				LetSharedDateTimeFormat letSharedDateTimeFormat = letSharedParsedDateTime._LetB1.Cast_LetSharedDateTimeFormat();
				outputDtFormat outputDtFormat = letSharedDateTimeFormat.outputDtFormat;
				DtRangeConcat dtRangeConcat = letSharedDateTimeFormat.dtRangeString.Cast_DtRangeConcat(CS$<>8__locals1.build);
				RangeFormatDateTime rangeFormatDateTime = dtRangeConcat.dtRangeSubstring.Cast_RangeFormatDateTime(CS$<>8__locals1.build);
				RangeRoundDateTime rangeRoundDateTime = rangeFormatDateTime.rangeDateTime.Cast_RangeRoundDateTime();
				VariableNode variable5 = rangeRoundDateTime.sharedParsedDt.Variable;
				DateTimeRoundingSpec value4 = rangeRoundDateTime.dtRoundingSpec.Value;
				VariableNode variable6 = rangeFormatDateTime.sharedDtFormat.Variable;
				DtRangeConcat dtRangeConcat2 = dtRangeConcat.dtRangeString.Cast_DtRangeConcat(CS$<>8__locals1.build);
				string value5 = dtRangeConcat2.dtRangeSubstring.Cast_DtRangeConstStr(CS$<>8__locals1.build).s.Value;
				RangeFormatDateTime rangeFormatDateTime2 = dtRangeConcat2.dtRangeString.Cast_dtRangeString_dtRangeSubstring(CS$<>8__locals1.build).dtRangeSubstring.Cast_RangeFormatDateTime(CS$<>8__locals1.build);
				RangeRoundDateTime rangeRoundDateTime2 = rangeFormatDateTime2.rangeDateTime.Cast_RangeRoundDateTime();
				VariableNode variable7 = rangeRoundDateTime2.sharedParsedDt.Variable;
				DateTimeRoundingSpec value6 = rangeRoundDateTime2.dtRoundingSpec.Value;
				VariableNode variable8 = rangeFormatDateTime2.sharedDtFormat.Variable;
				DateTimeFormat value7 = outputDtFormat.Value;
				TransformationDescription transformationDescription2 = new FormatDateTimeRange(letSharedDateTimeFormat.Node, value7, value5, value4, value6, CS$<>8__locals1.columnName);
				return this.VisitNonterminal((NonterminalNode)inputDateTime.Node, CS$<>8__locals1.columnName).AppendItem(transformationDescription2);
			}
			BlackBoxRule blackBoxRule = node.Rule as BlackBoxRule;
			if (blackBoxRule == null)
			{
				throw new NotImplementedException(node.Rule.Id);
			}
			string name = blackBoxRule.Name;
			if (name != null)
			{
				switch (name.Length)
				{
				case 6:
				{
					char c = name[0];
					if (c != 'L')
					{
						if (c != 'S')
						{
							goto IL_0D83;
						}
						if (!(name == "SubStr"))
						{
							goto IL_0D83;
						}
						return new Substring[]
						{
							new Substring(CS$<>8__locals1.build.Node.CastRule.SubStr(node), CS$<>8__locals1.columnName)
						};
					}
					else
					{
						if (!(name == "Lookup"))
						{
							goto IL_0D83;
						}
						Lookup lookup = CS$<>8__locals1.build.Node.CastRule.Lookup(node);
						return new TransformationDescription[]
						{
							new WholeColumn(lookup.x.Node, CS$<>8__locals1.columnName),
							new Lookup(lookup)
						};
					}
					break;
				}
				case 7:
				case 8:
				case 10:
				case 13:
				case 14:
				case 15:
				case 16:
					goto IL_0D83;
				case 9:
					if (!(name == "AsDecimal"))
					{
						goto IL_0D83;
					}
					return new InputNumber[]
					{
						new InputNumber(node, CS$<>8__locals1.columnName)
					};
				case 11:
				{
					char c = name[2];
					if (c <= 'U')
					{
						if (c != 'L')
						{
							if (c != 'U')
							{
								goto IL_0D83;
							}
							if (!(name == "ToUppercase"))
							{
								goto IL_0D83;
							}
						}
						else if (!(name == "ToLowercase"))
						{
							goto IL_0D83;
						}
					}
					else if (c != 'r')
					{
						if (c != 'u')
						{
							goto IL_0D83;
						}
						if (!(name == "RoundNumber"))
						{
							goto IL_0D83;
						}
						RoundNumber roundNumber = CS$<>8__locals1.build.Node.CastRule.RoundNumber(node);
						return this.VisitNonterminal((NonterminalNode)roundNumber.inputNumber.Node, CS$<>8__locals1.columnName).Concat(new RoundNumber[]
						{
							new RoundNumber(roundNumber, CS$<>8__locals1.columnName)
						});
					}
					else
					{
						if (!(name == "ParseNumber"))
						{
							goto IL_0D83;
						}
						ParseNumber parseNumber = CS$<>8__locals1.build.Node.CastRule.ParseNumber(node);
						return this.VisitNonterminal((NonterminalNode)parseNumber.SS.Node, CS$<>8__locals1.columnName).Concat(new ParseNumber[]
						{
							new ParseNumber(parseNumber, CS$<>8__locals1.columnName)
						});
					}
					break;
				}
				case 12:
				{
					if (!(name == "FormatNumber"))
					{
						goto IL_0D83;
					}
					FormatNumber formatNumber = CS$<>8__locals1.build.Node.CastRule.FormatNumber(node);
					return this.VisitNonterminal((NonterminalNode)formatNumber.number.Node, CS$<>8__locals1.columnName).Concat(new FormatNumber[]
					{
						new FormatNumber(formatNumber)
					});
				}
				case 17:
				{
					char c = name[0];
					if (c != 'A')
					{
						if (c != 'T')
						{
							goto IL_0D83;
						}
						if (!(name == "ToSimpleTitleCase"))
						{
							goto IL_0D83;
						}
					}
					else
					{
						if (!(name == "AsPartialDateTime"))
						{
							goto IL_0D83;
						}
						return new InputDate[]
						{
							new InputDate(node, CS$<>8__locals1.columnName)
						};
					}
					break;
				}
				case 18:
				{
					if (!(name == "FormatNumericRange"))
					{
						goto IL_0D83;
					}
					FormatNumericRange formatNumericRange = CS$<>8__locals1.build.Node.CastRule.FormatNumericRange(node);
					return this.VisitNonterminal((NonterminalNode)formatNumericRange.inputNumber.Node, CS$<>8__locals1.columnName).Concat(new FormatNumericRange[]
					{
						new FormatNumericRange(formatNumericRange, CS$<>8__locals1.columnName)
					});
				}
				case 19:
				{
					if (!(name == "FormatDateTimeRange"))
					{
						goto IL_0D83;
					}
					FormatDateTimeRange formatDateTimeRange = CS$<>8__locals1.build.Node.CastRule.FormatDateTimeRange(node);
					return this.VisitNonterminal((NonterminalNode)formatDateTimeRange.inputDateTime.Node, CS$<>8__locals1.columnName).Concat(new FormatDateTimeRange[]
					{
						new FormatDateTimeRange(formatDateTimeRange, CS$<>8__locals1.columnName)
					});
				}
				case 20:
				{
					char c = name[0];
					if (c != 'P')
					{
						if (c != 'R')
						{
							goto IL_0D83;
						}
						if (!(name == "RoundPartialDateTime"))
						{
							goto IL_0D83;
						}
						RoundPartialDateTime roundPartialDateTime2 = CS$<>8__locals1.build.Node.CastRule.RoundPartialDateTime(node);
						return this.VisitNonterminal((NonterminalNode)roundPartialDateTime2.inputDateTime.Node, CS$<>8__locals1.columnName).Concat(new RoundDateTime[]
						{
							new RoundDateTime(roundPartialDateTime2, CS$<>8__locals1.columnName)
						});
					}
					else
					{
						if (!(name == "ParsePartialDateTime"))
						{
							goto IL_0D83;
						}
						ParsePartialDateTime parse = CS$<>8__locals1.build.Node.CastRule.ParsePartialDateTime(node);
						DateTimeFormat[] value8 = parse.inputDtFormats.Value;
						return this.VisitNonterminal((NonterminalNode)parse.SS.Node, CS$<>8__locals1.columnName).Concat(value8.Select((DateTimeFormat _, int formatIdx) => new ParseDateTime(parse, CS$<>8__locals1.columnName, formatIdx, null)));
					}
					break;
				}
				case 21:
				{
					if (!(name == "FormatPartialDateTime"))
					{
						goto IL_0D83;
					}
					FormatPartialDateTime formatPartialDateTime = CS$<>8__locals1.build.Node.CastRule.FormatPartialDateTime(node);
					DateTimeFormat value9 = formatPartialDateTime.outputDtFormat.Value;
					datetime datetime = formatPartialDateTime.datetime;
					ParsePartialDateTime? unroundedParse = datetime.Switch<ParsePartialDateTime?>(CS$<>8__locals1.build, (datetime_inputDateTime datetime_inputDateTime) => datetime_inputDateTime.inputDateTime.Switch<ParsePartialDateTime?>(CS$<>8__locals1.build, (AsPartialDateTime asPartialDateTime) => null, (inputDateTime_parsedDateTime inputDateTime_parsedDateTime) => new ParsePartialDateTime?(inputDateTime_parsedDateTime.parsedDateTime.Cast_ParsePartialDateTime())), (RoundPartialDateTime roundPartialDateTime) => null);
					if (unroundedParse == null)
					{
						ProgramNode programNode = datetime.Switch<ProgramNode>(CS$<>8__locals1.build, (datetime_inputDateTime datetime_inputDateTime) => datetime_inputDateTime.inputDateTime.Switch<ProgramNode>(CS$<>8__locals1.build, (AsPartialDateTime asPartialDateTime) => asPartialDateTime.Node, (inputDateTime_parsedDateTime inputDateTime_parsedDateTime) => null), (RoundPartialDateTime roundPartialDateTime) => roundPartialDateTime.Node);
						return this.VisitNonterminal((NonterminalNode)programNode, CS$<>8__locals1.columnName).Concat(value9.FormatParts.Select(delegate(DateTimeFormatPart part, int partIdx)
						{
							if (!(part2 is ConstantDateTimeFormatPart))
							{
								return new FormatDateTime(formatPartialDateTime, partIdx);
							}
							return new Constant(formatPartialDateTime, partIdx);
						})).ToList<TransformationDescription>();
					}
					IEnumerable<DateTimeFormat> value10 = unroundedParse.Value.inputDtFormats.Value;
					List<TransformationDescription> list = new List<TransformationDescription>();
					using (var enumerator = value10.Select((DateTimeFormat format, int formatIdx) => new { format, formatIdx }).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							<>f__AnonymousType7<DateTimeFormat, int> formatTup = enumerator.Current;
							HashSet<int?> hashSet = new HashSet<int?>();
							using (IEnumerator<DateTimeFormatPart> enumerator2 = value9.FormatParts.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									DateTimeFormatPart part2 = enumerator2.Current;
									if (part2.MatchedPart.HasValue)
									{
										hashSet.Add((from o in formatTup.format.FormatParts.Select((DateTimeFormatPart p, int idx) => new
											{
												idx = idx,
												part = p.MatchedPart
											})
											where o.part == part2.MatchedPart
											select new int?(o.idx)).FirstOrDefault<int?>());
									}
								}
							}
							list.AddRange(hashSet.Select((int? partIdx) => new ParseDateTime(unroundedParse.Value, CS$<>8__locals1.columnName, formatTup.formatIdx, partIdx)));
						}
					}
					return this.VisitNonterminal((NonterminalNode)unroundedParse.Value.SS.Node, CS$<>8__locals1.columnName).Concat(list).Concat(value9.FormatParts.Select(delegate(DateTimeFormatPart part, int partIdx)
					{
						if (!(part is ConstantDateTimeFormatPart))
						{
							return new FormatDateTime(formatPartialDateTime, partIdx);
						}
						return new Constant(formatPartialDateTime, partIdx);
					}))
						.ToList<TransformationDescription>();
				}
				default:
					goto IL_0D83;
				}
				return this.VisitNonterminal((NonterminalNode)node.Children[0], CS$<>8__locals1.columnName).Concat(new CaseTransformation[]
				{
					new CaseTransformation(CS$<>8__locals1.build, CS$<>8__locals1.build.Node.Cast.conv(node), CS$<>8__locals1.columnName)
				});
			}
			IL_0D83:
			throw new NotImplementedException(blackBoxRule.Name);
		}

		// Token: 0x0600F72D RID: 63277 RVA: 0x0034B35C File Offset: 0x0034955C
		public override IEnumerable<TransformationDescription> VisitNonterminal(NonterminalNode node)
		{
			if (node.Rule is ConversionRule)
			{
				return node.Children[0].AcceptVisitor<IEnumerable<TransformationDescription>>(this);
			}
			BlackBoxRule blackBoxRule = node.Rule as BlackBoxRule;
			if (blackBoxRule == null)
			{
				throw new NotImplementedException(node.Rule.Id);
			}
			GrammarBuilders grammarBuilders = GrammarBuilders.Instance(node.GrammarRule.Grammar);
			Concat concat;
			if (grammarBuilders.Node.IsRule.Concat(node, out concat))
			{
				return concat.f.Node.AcceptVisitor<IEnumerable<TransformationDescription>>(this).Concat(concat.e.Node.AcceptVisitor<IEnumerable<TransformationDescription>>(this));
			}
			ConstStr constStr;
			if (grammarBuilders.Node.IsRule.ConstStr(node, out constStr))
			{
				return new Constant(constStr).Yield<Constant>();
			}
			IfThenElse ifThenElse;
			if (grammarBuilders.Node.IsRule.IfThenElse(node, out ifThenElse))
			{
				return ifThenElse.st.Node.AcceptVisitor<IEnumerable<TransformationDescription>>(this).Concat(ifThenElse.@switch.Node.AcceptVisitor<IEnumerable<TransformationDescription>>(this));
			}
			throw new NotImplementedException(blackBoxRule.Name);
		}

		// Token: 0x0600F72E RID: 63278 RVA: 0x0034B470 File Offset: 0x00349670
		public override IEnumerable<TransformationDescription> VisitLet(LetNode node)
		{
			GrammarBuilders grammarBuilders = GrammarBuilders.Instance(node.GrammarRule.Grammar);
			LetColumnName letColumnName;
			if (grammarBuilders.Node.IsRule.LetColumnName(node, out letColumnName))
			{
				string value = letColumnName.idx.Value;
				return this.VisitNonterminal((NonterminalNode)letColumnName.letOptions.Switch<conv>(grammarBuilders, (LetCell letCell) => letCell.conv, (LetX letX) => letX.conv).Node, value);
			}
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("symbol = {0}; rule = {1}", new object[]
			{
				node.Symbol.Name,
				node.Rule.Id
			})));
		}

		// Token: 0x0600F72F RID: 63279 RVA: 0x0034B54E File Offset: 0x0034974E
		public override IEnumerable<TransformationDescription> VisitLambda(LambdaNode node)
		{
			throw new NotImplementedException(node.Symbol.Name);
		}

		// Token: 0x0600F730 RID: 63280 RVA: 0x0034B54E File Offset: 0x0034974E
		public override IEnumerable<TransformationDescription> VisitLiteral(LiteralNode node)
		{
			throw new NotImplementedException(node.Symbol.Name);
		}

		// Token: 0x0600F731 RID: 63281 RVA: 0x0034B54E File Offset: 0x0034974E
		public override IEnumerable<TransformationDescription> VisitVariable(VariableNode node)
		{
			throw new NotImplementedException(node.Symbol.Name);
		}

		// Token: 0x0600F732 RID: 63282 RVA: 0x0034B54E File Offset: 0x0034974E
		public override IEnumerable<TransformationDescription> VisitHole(Hole node)
		{
			throw new NotImplementedException(node.Symbol.Name);
		}
	}
}
