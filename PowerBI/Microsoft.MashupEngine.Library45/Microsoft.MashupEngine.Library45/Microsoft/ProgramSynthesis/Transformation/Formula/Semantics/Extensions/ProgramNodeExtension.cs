using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001797 RID: 6039
	public static class ProgramNodeExtension
	{
		// Token: 0x0600C7F0 RID: 51184 RVA: 0x002AEAC8 File Offset: 0x002ACCC8
		public static object Run(this ProgramNode node, IRow input)
		{
			State state = State.CreateForExecution(node.Grammar.InputSymbol, input);
			return node.Invoke(state);
		}

		// Token: 0x0600C7F1 RID: 51185 RVA: 0x002AEAEE File Offset: 0x002ACCEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string CanonicalName(this ProgramNodeDetail nodeDetail)
		{
			return nodeDetail.Node.CanonicalName();
		}

		// Token: 0x0600C7F2 RID: 51186 RVA: 0x002AEAFC File Offset: 0x002ACCFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Cast<T>(this ProgramNode node, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			string text = node.CanonicalName();
			if (text != null)
			{
				object obj;
				switch (text.Length)
				{
				case 2:
				{
					char c = text[0];
					if (c != 'I')
					{
						if (c != 'O')
						{
							goto IL_1821;
						}
						if (!(text == "Or"))
						{
							goto IL_1821;
						}
						obj = Or.CreateSafe(build, node);
					}
					else
					{
						if (!(text == "If"))
						{
							goto IL_1821;
						}
						obj = If.CreateSafe(build, node);
					}
					break;
				}
				case 3:
				{
					char c = text[1];
					if (c <= 'd')
					{
						if (c != 'b')
						{
							if (c != 'd')
							{
								goto IL_1821;
							}
							if (!(text == "Add"))
							{
								goto IL_1821;
							}
							obj = Add.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "Abs"))
							{
								goto IL_1821;
							}
							obj = Abs.CreateSafe(build, node);
						}
					}
					else if (c != 'o')
					{
						if (c != 't')
						{
							if (c != 'u')
							{
								goto IL_1821;
							}
							if (!(text == "Sum"))
							{
								goto IL_1821;
							}
							obj = Sum.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "Str"))
							{
								goto IL_1821;
							}
							obj = Str.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "row"))
						{
							goto IL_1821;
						}
						obj = row.CreateSafe(build, node);
					}
					break;
				}
				case 4:
				{
					char c = text[0];
					if (c <= 'F')
					{
						if (c != 'D')
						{
							if (c != 'F')
							{
								goto IL_1821;
							}
							if (!(text == "Find"))
							{
								goto IL_1821;
							}
							obj = Find.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "Date"))
							{
								goto IL_1821;
							}
							obj = Date.CreateSafe(build, node);
						}
					}
					else if (c != 'L')
					{
						if (c != 'N')
						{
							if (c != 'T')
							{
								goto IL_1821;
							}
							if (!(text == "Trim"))
							{
								goto IL_1821;
							}
							obj = Trim.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "Null"))
							{
								goto IL_1821;
							}
							obj = Null.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "LetX"))
						{
							goto IL_1821;
						}
						obj = LetX.CreateSafe(build, node);
					}
					break;
				}
				case 5:
				{
					char c = text[2];
					if (c <= 'S')
					{
						if (c != 'I')
						{
							if (c != 'S')
							{
								goto IL_1821;
							}
							if (!(text == "ToStr"))
							{
								goto IL_1821;
							}
							obj = ToStr.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "ToInt"))
							{
								goto IL_1821;
							}
							obj = ToInt.CreateSafe(build, node);
						}
					}
					else if (c != 'i')
					{
						if (c != 'l')
						{
							if (c != 't')
							{
								goto IL_1821;
							}
							if (!(text == "Match"))
							{
								goto IL_1821;
							}
							obj = Match.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "Split"))
							{
								goto IL_1821;
							}
							obj = Split.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "Slice"))
						{
							goto IL_1821;
						}
						obj = Slice.CreateSafe(build, node);
					}
					break;
				}
				case 6:
				{
					char c = text[0];
					if (c <= 'L')
					{
						if (c != 'C')
						{
							if (c != 'D')
							{
								if (c != 'L')
								{
									goto IL_1821;
								}
								if (!(text == "Length"))
								{
									goto IL_1821;
								}
								obj = Length.CreateSafe(build, node);
							}
							else
							{
								if (!(text == "Divide"))
								{
									goto IL_1821;
								}
								obj = Divide.CreateSafe(build, node);
							}
						}
						else
						{
							if (!(text == "Concat"))
							{
								goto IL_1821;
							}
							obj = Concat.CreateSafe(build, node);
						}
					}
					else if (c != 'N')
					{
						if (c != 'a')
						{
							if (c != 'l')
							{
								goto IL_1821;
							}
							if (!(text == "locale"))
							{
								goto IL_1821;
							}
							obj = locale.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "absPos"))
							{
								goto IL_1821;
							}
							obj = absPos.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "Number"))
						{
							goto IL_1821;
						}
						obj = Number.CreateSafe(build, node);
					}
					break;
				}
				case 7:
				{
					char c = text[0];
					if (c <= 'I')
					{
						if (c != 'A')
						{
							if (c != 'F')
							{
								if (c != 'I')
								{
									goto IL_1821;
								}
								if (!(text == "IsBlank"))
								{
									if (!(text == "IsMatch"))
									{
										goto IL_1821;
									}
									obj = Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.IsMatch.CreateSafe(build, node);
								}
								else
								{
									obj = IsBlank.CreateSafe(build, node);
								}
							}
							else
							{
								if (!(text == "FromStr"))
								{
									goto IL_1821;
								}
								obj = FromStr.CreateSafe(build, node);
							}
						}
						else
						{
							if (!(text == "Average"))
							{
								goto IL_1821;
							}
							obj = Average.CreateSafe(build, node);
						}
					}
					else if (c != 'P')
					{
						if (c != 'R')
						{
							if (c != 'c')
							{
								goto IL_1821;
							}
							if (!(text == "constDt"))
							{
								goto IL_1821;
							}
							obj = constDt.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "Replace"))
							{
								goto IL_1821;
							}
							obj = Replace.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "Product"))
						{
							goto IL_1821;
						}
						obj = Product.CreateSafe(build, node);
					}
					break;
				}
				case 8:
				{
					char c = text[2];
					if (c <= 'S')
					{
						if (c != 'D')
						{
							if (c != 'N')
							{
								if (c != 'S')
								{
									goto IL_1821;
								}
								if (!(text == "IsString"))
								{
									goto IL_1821;
								}
								obj = IsString.CreateSafe(build, node);
							}
							else
							{
								if (!(text == "IsNumber"))
								{
									goto IL_1821;
								}
								obj = IsNumber.CreateSafe(build, node);
							}
						}
						else
						{
							if (!(text == "ToDouble"))
							{
								goto IL_1821;
							}
							obj = ToDouble.CreateSafe(build, node);
						}
					}
					else if (c != 'b')
					{
						switch (c)
						{
						case 'i':
							if (!(text == "TrimFull"))
							{
								goto IL_1821;
							}
							obj = TrimFull.CreateSafe(build, node);
							break;
						case 'j':
						case 'k':
							goto IL_1821;
						case 'l':
							if (!(text == "Multiply"))
							{
								goto IL_1821;
							}
							obj = Multiply.CreateSafe(build, node);
							break;
						case 'm':
							if (!(text == "TimePart"))
							{
								goto IL_1821;
							}
							obj = TimePart.CreateSafe(build, node);
							break;
						case 'n':
							if (!(text == "constNum"))
							{
								if (!(text == "constStr"))
								{
									if (!(text == "Contains"))
									{
										goto IL_1821;
									}
									obj = Contains.CreateSafe(build, node);
								}
								else
								{
									obj = constStr.CreateSafe(build, node);
								}
							}
							else
							{
								obj = constNum.CreateSafe(build, node);
							}
							break;
						case 'o':
							if (!(text == "FromTime"))
							{
								goto IL_1821;
							}
							obj = FromTime.CreateSafe(build, node);
							break;
						default:
							if (c != 't')
							{
								goto IL_1821;
							}
							if (!(text == "MatchEnd"))
							{
								goto IL_1821;
							}
							obj = MatchEnd.CreateSafe(build, node);
							break;
						}
					}
					else
					{
						if (!(text == "Subtract"))
						{
							goto IL_1821;
						}
						obj = Subtract.CreateSafe(build, node);
					}
					break;
				}
				case 9:
				{
					char c = text[5];
					if (c <= 'i')
					{
						switch (c)
						{
						case 'C':
							if (!(text == "LowerCase"))
							{
								if (!(text == "UpperCase"))
								{
									goto IL_1821;
								}
								obj = UpperCase.CreateSafe(build, node);
							}
							else
							{
								obj = LowerCase.CreateSafe(build, node);
							}
							break;
						case 'D':
							if (!(text == "matchDesc"))
							{
								goto IL_1821;
							}
							obj = matchDesc.CreateSafe(build, node);
							break;
						case 'E':
							goto IL_1821;
						case 'F':
							if (!(text == "MatchFull"))
							{
								goto IL_1821;
							}
							obj = MatchFull.CreateSafe(build, node);
							break;
						default:
							if (c != 'i')
							{
								goto IL_1821;
							}
							if (!(text == "ToDecimal"))
							{
								goto IL_1821;
							}
							obj = ToDecimal.CreateSafe(build, node);
							break;
						}
					}
					else if (c != 'l')
					{
						if (c != 'p')
						{
							goto IL_1821;
						}
						if (!(text == "TrimSplit"))
						{
							goto IL_1821;
						}
						obj = TrimSplit.CreateSafe(build, node);
					}
					else
					{
						if (!(text == "TrimSlice"))
						{
							goto IL_1821;
						}
						obj = TrimSlice.CreateSafe(build, node);
					}
					break;
				}
				case 10:
				{
					char c = text[0];
					if (c <= 'I')
					{
						if (c != 'F')
						{
							if (c != 'I')
							{
								goto IL_1821;
							}
							if (!(text == "IsNotBlank"))
							{
								goto IL_1821;
							}
							obj = IsNotBlank.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "FromNumber"))
							{
								goto IL_1821;
							}
							obj = FromNumber.CreateSafe(build, node);
						}
					}
					else
					{
						switch (c)
						{
						case 'P':
							if (!(text == "ProperCase"))
							{
								goto IL_1821;
							}
							obj = ProperCase.CreateSafe(build, node);
							break;
						case 'Q':
						case 'R':
							goto IL_1821;
						case 'S':
							if (!(text == "StartsWith"))
							{
								goto IL_1821;
							}
							obj = StartsWith.CreateSafe(build, node);
							break;
						case 'T':
							if (!(text == "ToDateTime"))
							{
								goto IL_1821;
							}
							obj = ToDateTime.CreateSafe(build, node);
							break;
						default:
							switch (c)
							{
							case 'c':
								if (!(text == "columnName"))
								{
									goto IL_1821;
								}
								obj = columnName.CreateSafe(build, node);
								break;
							case 'd':
								goto IL_1821;
							case 'e':
								if (!(text == "equalsText"))
								{
									goto IL_1821;
								}
								obj = equalsText.CreateSafe(build, node);
								break;
							case 'f':
								if (!(text == "findOffset"))
								{
									goto IL_1821;
								}
								obj = findOffset.CreateSafe(build, node);
								break;
							default:
								if (c != 'm')
								{
									goto IL_1821;
								}
								if (!(text == "matchCount"))
								{
									goto IL_1821;
								}
								obj = matchCount.CreateSafe(build, node);
								break;
							}
							break;
						}
					}
					break;
				}
				case 11:
				{
					char c = text[0];
					if (c <= 'S')
					{
						if (c != 'F')
						{
							switch (c)
							{
							case 'P':
								if (!(text == "ParseNumber"))
								{
									goto IL_1821;
								}
								obj = ParseNumber.CreateSafe(build, node);
								break;
							case 'Q':
								goto IL_1821;
							case 'R':
								if (!(text == "RoundNumber"))
								{
									goto IL_1821;
								}
								obj = RoundNumber.CreateSafe(build, node);
								break;
							case 'S':
								if (!(text == "SlicePrefix"))
								{
									if (!(text == "SliceSuffix"))
									{
										goto IL_1821;
									}
									obj = SliceSuffix.CreateSafe(build, node);
								}
								else
								{
									obj = SlicePrefix.CreateSafe(build, node);
								}
								break;
							default:
								goto IL_1821;
							}
						}
						else
						{
							if (!(text == "FromNumbers"))
							{
								goto IL_1821;
							}
							obj = FromNumbers.CreateSafe(build, node);
						}
					}
					else if (c != 'c')
					{
						if (c != 'r')
						{
							goto IL_1821;
						}
						if (!(text == "replaceText"))
						{
							goto IL_1821;
						}
						obj = replaceText.CreateSafe(build, node);
					}
					else
					{
						if (!(text == "columnNames"))
						{
							goto IL_1821;
						}
						obj = columnNames.CreateSafe(build, node);
					}
					break;
				}
				case 12:
				{
					char c = text[1];
					if (c != 'a')
					{
						switch (c)
						{
						case 'i':
							if (!(text == "findInstance"))
							{
								if (!(text == "timePartKind"))
								{
									goto IL_1821;
								}
								obj = timePartKind.CreateSafe(build, node);
							}
							else
							{
								obj = findInstance.CreateSafe(build, node);
							}
							break;
						case 'j':
						case 'k':
						case 'm':
						case 'n':
						case 'p':
						case 'q':
							goto IL_1821;
						case 'l':
							if (!(text == "SliceBetween"))
							{
								goto IL_1821;
							}
							obj = SliceBetween.CreateSafe(build, node);
							break;
						case 'o':
							if (!(text == "FormatNumber"))
							{
								goto IL_1821;
							}
							obj = FormatNumber.CreateSafe(build, node);
							break;
						case 'r':
							if (!(text == "FromDateTime"))
							{
								goto IL_1821;
							}
							obj = FromDateTime.CreateSafe(build, node);
							break;
						case 's':
							if (!(text == "isMatchRegex"))
							{
								goto IL_1821;
							}
							obj = isMatchRegex.CreateSafe(build, node);
							break;
						case 't':
							if (!(text == "StringEquals"))
							{
								goto IL_1821;
							}
							obj = StringEquals.CreateSafe(build, node);
							break;
						case 'u':
							if (!(text == "NumberEquals"))
							{
								goto IL_1821;
							}
							obj = NumberEquals.CreateSafe(build, node);
							break;
						default:
							goto IL_1821;
						}
					}
					else
					{
						if (!(text == "DateTimePart"))
						{
							goto IL_1821;
						}
						obj = DateTimePart.CreateSafe(build, node);
					}
					break;
				}
				case 13:
				{
					char c = text[0];
					if (c <= 'c')
					{
						switch (c)
						{
						case 'C':
							if (!(text == "ContainsMatch"))
							{
								goto IL_1821;
							}
							obj = ContainsMatch.CreateSafe(build, node);
							break;
						case 'D':
							goto IL_1821;
						case 'E':
							if (!(text == "EndsWithDigit"))
							{
								goto IL_1821;
							}
							obj = EndsWithDigit.CreateSafe(build, node);
							break;
						case 'F':
							if (!(text == "FromNumberStr"))
							{
								if (!(text == "FromRowNumber"))
								{
									goto IL_1821;
								}
								obj = FromRowNumber.CreateSafe(build, node);
							}
							else
							{
								obj = FromNumberStr.CreateSafe(build, node);
							}
							break;
						default:
							switch (c)
							{
							case 'P':
								if (!(text == "ParseDateTime"))
								{
									goto IL_1821;
								}
								obj = ParseDateTime.CreateSafe(build, node);
								break;
							case 'Q':
							case 'S':
								goto IL_1821;
							case 'R':
								if (!(text == "RoundDateTime"))
								{
									goto IL_1821;
								}
								obj = RoundDateTime.CreateSafe(build, node);
								break;
							case 'T':
								if (!(text == "TrimFullSlice"))
								{
									if (!(text == "TrimFullSplit"))
									{
										goto IL_1821;
									}
									obj = TrimFullSplit.CreateSafe(build, node);
								}
								else
								{
									obj = TrimFullSlice.CreateSafe(build, node);
								}
								break;
							default:
								if (c != 'c')
								{
									goto IL_1821;
								}
								if (!(text == "containsCount"))
								{
									goto IL_1821;
								}
								obj = containsCount.CreateSafe(build, node);
								break;
							}
							break;
						}
					}
					else if (c != 'f')
					{
						if (c != 'm')
						{
							if (c != 's')
							{
								goto IL_1821;
							}
							if (!(text == "splitInstance"))
							{
								goto IL_1821;
							}
							obj = splitInstance.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "matchInstance"))
							{
								goto IL_1821;
							}
							obj = matchInstance.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "findDelimiter"))
						{
							goto IL_1821;
						}
						obj = findDelimiter.CreateSafe(build, node);
					}
					break;
				}
				case 14:
				{
					char c = text[0];
					if (c <= 'F')
					{
						if (c != 'A')
						{
							if (c != 'F')
							{
								goto IL_1821;
							}
							if (!(text == "FormatDateTime"))
							{
								goto IL_1821;
							}
							obj = FormatDateTime.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "AddRightNumber"))
							{
								goto IL_1821;
							}
							obj = AddRightNumber.CreateSafe(build, node);
						}
					}
					else if (c != 'N')
					{
						if (c != 'S')
						{
							if (c != 's')
							{
								goto IL_1821;
							}
							if (!(text == "splitDelimiter"))
							{
								goto IL_1821;
							}
							obj = splitDelimiter.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "SlicePrefixAbs"))
							{
								goto IL_1821;
							}
							obj = SlicePrefixAbs.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "NumberLessThan"))
						{
							goto IL_1821;
						}
						obj = NumberLessThan.CreateSafe(build, node);
					}
					break;
				}
				case 15:
				{
					char c = text[0];
					if (c <= 'S')
					{
						if (c != 'L')
						{
							if (c != 'S')
							{
								goto IL_1821;
							}
							if (!(text == "StartsWithDigit"))
							{
								goto IL_1821;
							}
							obj = StartsWithDigit.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "LowerCaseConcat"))
							{
								goto IL_1821;
							}
							obj = LowerCaseConcat.CreateSafe(build, node);
						}
					}
					else if (c != 'U')
					{
						if (c != 'n')
						{
							if (c != 'r')
							{
								goto IL_1821;
							}
							if (!(text == "replaceFindText"))
							{
								goto IL_1821;
							}
							obj = replaceFindText.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "numberRoundDesc"))
							{
								goto IL_1821;
							}
							obj = numberRoundDesc.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "UpperCaseConcat"))
						{
							goto IL_1821;
						}
						obj = UpperCaseConcat.CreateSafe(build, node);
					}
					break;
				}
				case 16:
				{
					char c = text[0];
					if (c <= 'P')
					{
						if (c != 'F')
						{
							if (c != 'P')
							{
								goto IL_1821;
							}
							if (!(text == "ProperCaseConcat"))
							{
								goto IL_1821;
							}
							obj = ProperCaseConcat.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "FromDateTimePart"))
							{
								goto IL_1821;
							}
							obj = FromDateTimePart.CreateSafe(build, node);
						}
					}
					else if (c != 'c')
					{
						if (c != 'd')
						{
							if (c != 'n')
							{
								goto IL_1821;
							}
							if (!(text == "numberFormatDesc"))
							{
								goto IL_1821;
							}
							obj = numberFormatDesc.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "dateTimePartKind"))
							{
								goto IL_1821;
							}
							obj = dateTimePartKind.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "containsFindText"))
						{
							goto IL_1821;
						}
						obj = containsFindText.CreateSafe(build, node);
					}
					break;
				}
				case 17:
				{
					char c = text[8];
					if (c <= 'R')
					{
						if (c != 'P')
						{
							if (c != 'R')
							{
								goto IL_1821;
							}
							if (!(text == "dateTimeRoundDesc"))
							{
								goto IL_1821;
							}
							obj = dateTimeRoundDesc.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "dateTimeParseDesc"))
							{
								goto IL_1821;
							}
							obj = dateTimeParseDesc.CreateSafe(build, node);
						}
					}
					else
					{
						switch (c)
						{
						case 'b':
							if (!(text == "scaleNumberFactor"))
							{
								goto IL_1821;
							}
							obj = scaleNumberFactor.CreateSafe(build, node);
							break;
						case 'c':
						case 'd':
							goto IL_1821;
						case 'e':
							if (!(text == "NumberGreaterThan"))
							{
								goto IL_1821;
							}
							obj = NumberGreaterThan.CreateSafe(build, node);
							break;
						case 'f':
							if (!(text == "slicePrefixAbsPos"))
							{
								goto IL_1821;
							}
							obj = slicePrefixAbsPos.CreateSafe(build, node);
							break;
						case 'g':
							if (!(text == "DivideRightNumber"))
							{
								goto IL_1821;
							}
							obj = DivideRightNumber.CreateSafe(build, node);
							break;
						default:
							if (c != 'u')
							{
								goto IL_1821;
							}
							if (!(text == "numberEqualsValue"))
							{
								goto IL_1821;
							}
							obj = numberEqualsValue.CreateSafe(build, node);
							break;
						}
					}
					break;
				}
				case 18:
				{
					char c = text[0];
					if (c != 'c')
					{
						if (c != 'd')
						{
							if (c != 's')
							{
								goto IL_1821;
							}
							if (!(text == "startsWithFindText"))
							{
								goto IL_1821;
							}
							obj = startsWithFindText.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "dateTimeFormatDesc"))
							{
								goto IL_1821;
							}
							obj = dateTimeFormatDesc.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "containsMatchRegex"))
						{
							goto IL_1821;
						}
						obj = containsMatchRegex.CreateSafe(build, node);
					}
					break;
				}
				case 19:
				{
					char c = text[0];
					if (c <= 'M')
					{
						if (c != 'F')
						{
							if (c != 'M')
							{
								goto IL_1821;
							}
							if (!(text == "MultiplyRightNumber"))
							{
								goto IL_1821;
							}
							obj = MultiplyRightNumber.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "FromNumberCoalesced"))
							{
								goto IL_1821;
							}
							obj = FromNumberCoalesced.CreateSafe(build, node);
						}
					}
					else if (c != 'S')
					{
						if (c != 'n')
						{
							if (c != 's')
							{
								goto IL_1821;
							}
							if (!(text == "sliceBetweenEndText"))
							{
								goto IL_1821;
							}
							obj = sliceBetweenEndText.CreateSafe(build, node);
						}
						else
						{
							if (!(text == "numberLessThanValue"))
							{
								goto IL_1821;
							}
							obj = numberLessThanValue.CreateSafe(build, node);
						}
					}
					else
					{
						if (!(text == "SubtractRightNumber"))
						{
							goto IL_1821;
						}
						obj = SubtractRightNumber.CreateSafe(build, node);
					}
					break;
				}
				case 20:
					if (!(text == "fromDateTimePartKind"))
					{
						goto IL_1821;
					}
					obj = fromDateTimePartKind.CreateSafe(build, node);
					break;
				case 21:
					if (!(text == "sliceBetweenStartText"))
					{
						goto IL_1821;
					}
					obj = sliceBetweenStartText.CreateSafe(build, node);
					break;
				case 22:
					if (!(text == "numberGreaterThanValue"))
					{
						goto IL_1821;
					}
					obj = numberGreaterThanValue.CreateSafe(build, node);
					break;
				case 23:
				case 25:
				case 26:
				case 27:
					goto IL_1821;
				case 24:
					if (!(text == "RowNumberLinearTransform"))
					{
						goto IL_1821;
					}
					obj = RowNumberLinearTransform.CreateSafe(build, node);
					break;
				case 28:
					if (!(text == "rowNumberLinearTransformDesc"))
					{
						goto IL_1821;
					}
					obj = rowNumberLinearTransformDesc.CreateSafe(build, node);
					break;
				default:
					goto IL_1821;
				}
				object obj2 = obj;
				if (obj2 != null)
				{
					return (T)((object)obj2);
				}
				throw new Exception("Cannot Cast " + text + " as " + typeof(T).Name);
			}
			IL_1821:
			throw new Exception("Unknown NodeType: " + text);
		}

		// Token: 0x0600C7F3 RID: 51187 RVA: 0x002B036B File Offset: 0x002AE56B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T>(this ProgramNode node) where T : struct, IProgramNodeBuilder
		{
			return node.CanonicalName() == typeof(T).Name;
		}

		// Token: 0x0600C7F4 RID: 51188 RVA: 0x002B0388 File Offset: 0x002AE588
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T>(this ProgramNode node, GrammarBuilders build, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			bool flag = node.Is<T>();
			grammarNode = (flag ? node.Cast(build) : default(T));
			return flag;
		}

		// Token: 0x0600C7F5 RID: 51189 RVA: 0x002B03B8 File Offset: 0x002AE5B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2>(this ProgramNode node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return node.Is<T1>() || node.Is<T2>();
		}

		// Token: 0x0600C7F6 RID: 51190 RVA: 0x002B03CA File Offset: 0x002AE5CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3>(this ProgramNode node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return node.Is<T1>() || node.Is<T2>() || node.Is<T3>();
		}

		// Token: 0x0600C7F7 RID: 51191 RVA: 0x002B03E4 File Offset: 0x002AE5E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4>(this ProgramNode node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return node.Is<T1>() || node.Is<T2>() || node.Is<T3>() || node.Is<T4>();
		}

		// Token: 0x0600C7F8 RID: 51192 RVA: 0x002B0406 File Offset: 0x002AE606
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4, T5>(this ProgramNode node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder
		{
			return node.Is<T1>() || node.Is<T2>() || node.Is<T3>() || node.Is<T4>() || node.Is<T5>();
		}

		// Token: 0x0600C7F9 RID: 51193 RVA: 0x002B0430 File Offset: 0x002AE630
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4, T5, T6>(this ProgramNode node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder
		{
			return node.Is<T1>() || node.Is<T2>() || node.Is<T3>() || node.Is<T4>() || node.Is<T5>() || node.Is<T6>();
		}

		// Token: 0x0600C7FA RID: 51194 RVA: 0x002B0462 File Offset: 0x002AE662
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4, T5, T6, T7>(this ProgramNode node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder where T7 : struct, IProgramNodeBuilder
		{
			return node.Is<T1>() || node.Is<T2>() || node.Is<T3>() || node.Is<T4>() || node.Is<T5>() || node.Is<T6>() || node.Is<T7>();
		}

		// Token: 0x0600C7FB RID: 51195 RVA: 0x002B049C File Offset: 0x002AE69C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDefault<T>(this T node) where T : struct, IProgramNodeBuilder
		{
			return node.Node == null || node.Equals(default(T));
		}

		// Token: 0x0600C7FC RID: 51196 RVA: 0x002B04DB File Offset: 0x002AE6DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsArithmetic(this ProgramNode node)
		{
			return node.Is<Add, Subtract, Multiply, Divide>() || node.IsArithmeticAggregate();
		}

		// Token: 0x0600C7FD RID: 51197 RVA: 0x002B04ED File Offset: 0x002AE6ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsArithmeticAggregate(this ProgramNode node)
		{
			return node.Is<Sum, Product, Average>();
		}

		// Token: 0x0600C7FE RID: 51198 RVA: 0x002B04F5 File Offset: 0x002AE6F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCase(this ProgramNode node)
		{
			return node.IsUpperCase() || node.IsLowerCase() || node.IsProperCase();
		}

		// Token: 0x0600C7FF RID: 51199 RVA: 0x002B050F File Offset: 0x002AE70F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsConstant(this ProgramNode node)
		{
			return node.Is<Str, Number>() || node.Is<AddRightNumber, SubtractRightNumber, MultiplyRightNumber, DivideRightNumber>();
		}

		// Token: 0x0600C800 RID: 51200 RVA: 0x002B0521 File Offset: 0x002AE721
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsConstantNumber(this ProgramNode node)
		{
			return node.Is<Number>() || node.IsArithmeticRightNumber();
		}

		// Token: 0x0600C801 RID: 51201 RVA: 0x002B0534 File Offset: 0x002AE734
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsConstantNumber(this ProgramNode node, GrammarBuilders build, out decimal value)
		{
			Number number;
			if (node.Is(build, out number))
			{
				value = number.constNum.Value;
				return true;
			}
			if (node.IsArithmeticRightNumber(build, out value))
			{
				return true;
			}
			value = 0m;
			return false;
		}

		// Token: 0x0600C802 RID: 51202 RVA: 0x002B0577 File Offset: 0x002AE777
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFrom(this ProgramNode node)
		{
			return node.IsFromNumber() || node.IsFromStr() || node.IsFromDateTime() || node.IsFromTime();
		}

		// Token: 0x0600C803 RID: 51203 RVA: 0x002B0599 File Offset: 0x002AE799
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFromDateTime(this ProgramNode node)
		{
			return node.Is<FromDateTime>();
		}

		// Token: 0x0600C804 RID: 51204 RVA: 0x002B05A1 File Offset: 0x002AE7A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFromNumber(this ProgramNode node)
		{
			return node.Is<FromNumber, FromNumberCoalesced>();
		}

		// Token: 0x0600C805 RID: 51205 RVA: 0x002B05A9 File Offset: 0x002AE7A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFromStr(this ProgramNode node)
		{
			return node.Is<FromStr, FromNumberStr>();
		}

		// Token: 0x0600C806 RID: 51206 RVA: 0x002B05B1 File Offset: 0x002AE7B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFromTime(this ProgramNode node)
		{
			return node.Is<FromTime>();
		}

		// Token: 0x0600C807 RID: 51207 RVA: 0x002B05B9 File Offset: 0x002AE7B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsLowerCase(this ProgramNode node)
		{
			return node.Is<LowerCase, LowerCaseConcat>();
		}

		// Token: 0x0600C808 RID: 51208 RVA: 0x002B05C1 File Offset: 0x002AE7C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsMatch(this ProgramNode node)
		{
			return node.Is<Match, MatchFull, MatchEnd>();
		}

		// Token: 0x0600C809 RID: 51209 RVA: 0x002B05C9 File Offset: 0x002AE7C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOutputCast(this ProgramNode node)
		{
			return node.Is<ToInt, ToDouble, ToDecimal, ToDateTime, ToStr>();
		}

		// Token: 0x0600C80A RID: 51210 RVA: 0x002B05D1 File Offset: 0x002AE7D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsProperCase(this ProgramNode node)
		{
			return node.Is<ProperCase, ProperCaseConcat>();
		}

		// Token: 0x0600C80B RID: 51211 RVA: 0x002B05D9 File Offset: 0x002AE7D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSlice(this ProgramNode node)
		{
			return node.Is<Slice, SlicePrefix, SlicePrefixAbs, SliceSuffix, SliceBetween>();
		}

		// Token: 0x0600C80C RID: 51212 RVA: 0x002B05E1 File Offset: 0x002AE7E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsArithmeticRightNumber(this ProgramNode node)
		{
			return node.Is<AddRightNumber, SubtractRightNumber, MultiplyRightNumber, DivideRightNumber>();
		}

		// Token: 0x0600C80D RID: 51213 RVA: 0x002B05EC File Offset: 0x002AE7EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsArithmeticRightNumber(this ProgramNode node, GrammarBuilders build, out decimal value)
		{
			AddRightNumber addRightNumber;
			SubtractRightNumber subtractRightNumber;
			MultiplyRightNumber multiplyRightNumber;
			DivideRightNumber divideRightNumber;
			decimal? num = (node.Is(build, out addRightNumber) ? new decimal?(addRightNumber.constNum.Value) : (node.Is(build, out subtractRightNumber) ? new decimal?(subtractRightNumber.constNum.Value) : (node.Is(build, out multiplyRightNumber) ? new decimal?(multiplyRightNumber.constNum.Value) : (node.Is(build, out divideRightNumber) ? new decimal?(divideRightNumber.constNum.Value) : null))));
			if (num == null)
			{
				value = 0m;
				return false;
			}
			value = num.Value;
			return true;
		}

		// Token: 0x0600C80E RID: 51214 RVA: 0x002B06AC File Offset: 0x002AE8AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSubstring(this ProgramNode node)
		{
			return node.IsSlice() || node.Is<Split>() || node.Is<MatchFull>();
		}

		// Token: 0x0600C80F RID: 51215 RVA: 0x002B06C6 File Offset: 0x002AE8C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsTrim(this ProgramNode node)
		{
			return node.Is<Trim, TrimFull, TrimFullSlice, TrimFullSplit, TrimSlice, TrimSplit>();
		}

		// Token: 0x0600C810 RID: 51216 RVA: 0x002B06CE File Offset: 0x002AE8CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsUpperCase(this ProgramNode node)
		{
			return node.Is<UpperCase, UpperCaseConcat>();
		}

		// Token: 0x0600C811 RID: 51217 RVA: 0x002B06D8 File Offset: 0x002AE8D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> Ancestors(this ProgramNode node, IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return from nodeDetail in nodeDetails
				where nodeDetail.Node.Equals(node)
				from ancestor in nodeDetail.Ancestors
				select ancestor;
		}

		// Token: 0x0600C812 RID: 51218 RVA: 0x002B0747 File Offset: 0x002AE947
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> Ancestors<T>(this ProgramNode node, IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			return node.Ancestors(nodeDetails).OfGrammarType(build);
		}

		// Token: 0x0600C813 RID: 51219 RVA: 0x002B0758 File Offset: 0x002AE958
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> Descendents(this ProgramNode node, IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return nodeDetails.Where((ProgramNodeDetail nodeDetail) => nodeDetail.Ancestors.Any((ProgramNodeDetail a) => a.Node.Equals(node)));
		}

		// Token: 0x0600C814 RID: 51220 RVA: 0x002B0784 File Offset: 0x002AE984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> Descendents<T>(this ProgramNode node, IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			return node.Descendents(nodeDetails).OfGrammarType(build);
		}
	}
}
