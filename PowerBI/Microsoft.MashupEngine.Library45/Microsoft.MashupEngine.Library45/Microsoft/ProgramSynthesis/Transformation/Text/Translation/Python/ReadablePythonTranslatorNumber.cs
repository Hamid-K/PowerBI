using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DC5 RID: 7621
	internal class ReadablePythonTranslatorNumber
	{
		// Token: 0x0600FF82 RID: 65410 RVA: 0x0036BE94 File Offset: 0x0036A094
		internal static PartitionedCode ToReadableNumber(FormatNumber p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslatorNumber.ToReadableNumber(p.number);
			if (partitionedCode == null)
			{
				return null;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.NumType, "number_obj");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			NumberFormat numberFormat = ReadablePythonTranslatorNumber.ConvertToNumberFormat(p.numberFormat);
			return ReadablePythonTranslatorNumber.TranslateFormatNumber(ssaregister, numberFormat, partitionedCode);
		}

		// Token: 0x0600FF83 RID: 65411 RVA: 0x0036BEE4 File Offset: 0x0036A0E4
		internal static PartitionedCode ToReadableNumber(LetSharedParsedNumber p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslatorNumber.ToReadableInputNumber(p.inputNumber);
			if (partitionedCode == null)
			{
				return null;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.NumType, "number_obj");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			LetSharedNumberFormat letSharedNumberFormat = p._LetB0.Cast_LetSharedNumberFormat();
			NumberFormat numberFormat = ReadablePythonTranslatorNumber.ConvertToNumberFormat(letSharedNumberFormat.numberFormat);
			PartitionedCode partitionedCode2 = ReadablePythonTranslatorNumber.ToReadableRangeString(letSharedNumberFormat.rangeString, ssaregister, numberFormat);
			if (partitionedCode2 == null)
			{
				return null;
			}
			partitionedCode.Merge(partitionedCode2);
			partitionedCode.SetExpr(partitionedCode2.Expr);
			return partitionedCode;
		}

		// Token: 0x0600FF84 RID: 65412 RVA: 0x0036BF68 File Offset: 0x0036A168
		private static PartitionedCode ToReadableNumber(number input)
		{
			return input.Switch<PartitionedCode>(ReadablePythonTranslatorNumber.g, (number_inputNumber x) => ReadablePythonTranslatorNumber.ToReadableInputNumber(x.inputNumber), (RoundNumber x) => ReadablePythonTranslatorNumber.ToReadableRoundNumber(x));
		}

		// Token: 0x0600FF85 RID: 65413 RVA: 0x0036BFC0 File Offset: 0x0036A1C0
		private static PartitionedCode ToReadableRoundNumber(RoundNumber num)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslatorNumber.ToReadableInputNumber(num.inputNumber);
			if (partitionedCode == null)
			{
				return null;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.NumType, "number_obj");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			RoundingSpec value = num.roundingSpec.Value;
			PartitionedCode partitionedCode2 = ReadablePythonTranslatorNumber.TranslateRoundNumber(ssaregister, value);
			partitionedCode.Merge(partitionedCode2);
			partitionedCode.SetExpr(partitionedCode2.Expr);
			return partitionedCode;
		}

		// Token: 0x0600FF86 RID: 65414 RVA: 0x0036C028 File Offset: 0x0036A228
		private static PartitionedCode ToReadableInputNumber(inputNumber input)
		{
			GrammarBuilders grammarBuilders = ReadablePythonTranslatorNumber.g;
			Func<inputNumber_castToNumber, PartitionedCode> func;
			if ((func = ReadablePythonTranslatorNumber.<>O.<0>__ToReadableCastToNumber) == null)
			{
				func = (ReadablePythonTranslatorNumber.<>O.<0>__ToReadableCastToNumber = new Func<inputNumber_castToNumber, PartitionedCode>(ReadablePythonTranslatorNumber.ToReadableCastToNumber));
			}
			return input.Switch<PartitionedCode>(grammarBuilders, func, (inputNumber_parsedNumber x) => ReadablePythonTranslatorNumber.ToReadableParsedNumber(x));
		}

		// Token: 0x0600FF87 RID: 65415 RVA: 0x0036C07C File Offset: 0x0036A27C
		private static PartitionedCode ToReadableParsedNumber(inputNumber_parsedNumber input)
		{
			ParseNumber parseNumber = input.parsedNumber.Cast_ParseNumber();
			SS ss = parseNumber.SS;
			NumberFormatDetails value = parseNumber.numberFormatDetails.Value;
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(ss.Node);
			if (partitionedCode == null)
			{
				return null;
			}
			SSAValue ssavalue = partitionedCode.Expr;
			if (value.SeparatorChar.HasValue)
			{
				SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, "with_separators_removed");
				SSARValue ssarvalue = PythonExpressionUtils.Replace(value.SeparatorChar.Value.ToString(), "");
				partitionedCode.LocalAddLine(new SSAStep(ssaregister, PythonExpressionUtils.Dot(new SSAValue[] { partitionedCode.Expr, ssarvalue }), ""));
				ssavalue = ssaregister;
			}
			if (value.CurrencySymbol.HasValue)
			{
				string value2 = value.CurrencySymbol.Value;
				SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.BoolType, "currency_occurs");
				SSARValue ssarvalue2 = PythonExpressionUtils.StartsWith(ssavalue, value2, 0);
				SSARValue ssarvalue3 = PythonExpressionUtils.StartsWith(ssavalue, value2, 1);
				SSARValue ssarvalue4 = PythonExpressionUtils.EndsWith(ssavalue, value2, 0);
				SSARValue ssarvalue5 = PythonExpressionUtils.EndsWith(ssavalue, value2, -1);
				SSARValue ssarvalue6 = PythonExpressionUtils.Or(new SSAValue[] { ssarvalue2, ssarvalue3, ssarvalue4, ssarvalue5 });
				partitionedCode.LocalAddLine(new SSAStep(ssaregister2, ssarvalue6, ""));
				SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, "currency_removed");
				SSARValue ssarvalue7 = PythonExpressionUtils.Replace(value2, "", 1);
				SSARValue ssarvalue8 = PythonExpressionUtils.Strip(PythonExpressionUtils.Dot(new SSAValue[] { ssavalue, ssarvalue7 }), null);
				partitionedCode.LocalAddLine(new SSAStep(ssaregister, PythonExpressionUtils.IfThenElse(ssarvalue6, ssarvalue8, ssavalue), ""));
				ssavalue = ssaregister;
			}
			if (value.AllowParseParensAsNegative)
			{
				SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, "paren_removed");
				SSARValue ssarvalue9 = PythonExpressionUtils.Equals(ReadablePythonTranslatorNumber.<ToReadableParsedNumber>g__getItem|6_0(ssavalue, 0), PythonExpressionUtils.MkPyLiteral("("));
				SSARValue ssarvalue10 = PythonExpressionUtils.Equals(ReadablePythonTranslatorNumber.<ToReadableParsedNumber>g__getItem|6_0(ssavalue, -1), PythonExpressionUtils.MkPyLiteral(")"));
				SSARValue ssarvalue11 = PythonExpressionUtils.And(new SSAValue[] { ssarvalue9, ssarvalue10 });
				SSARValue ssarvalue12 = PythonExpressionUtils.Format("-{0}", PythonExpressionUtils.GetItem(ssavalue, PythonExpressionUtils.MkLiteral("1:-1")));
				partitionedCode.LocalAddLine(new SSAStep(ssaregister, PythonExpressionUtils.IfThenElse(ssarvalue11, ssarvalue12, ssavalue), ""));
				ssavalue = ssaregister;
			}
			if (value.TrailingSign)
			{
				SSARegister ssaregister3 = new SSARegister(null, PythonExpressionUtils.StrType, "leading_sign");
				SSARValue item = PythonExpressionUtils.GetItem(ssavalue, PythonExpressionUtils.Minus1);
				SSARValue ssarvalue13 = PythonExpressionUtils.NotIn(item, PythonExpressionUtils.MakeList(new string[] { "-", "+" }));
				SSARValue ssarvalue14 = PythonExpressionUtils.Add(new SSAValue[]
				{
					item,
					PythonExpressionUtils.SliceEndOnly(ssavalue, PythonExpressionUtils.Minus1)
				});
				partitionedCode.LocalAddLine(new SSAStep(ssaregister3, PythonExpressionUtils.IfThenElse(ssarvalue13, ssavalue, ssarvalue14), ""));
				ssavalue = ssaregister3;
			}
			SSARValue ssarvalue15 = PythonExpressionUtils.DDecimal(ssavalue);
			partitionedCode.AddImport("decimal");
			partitionedCode.SetExpr(ssarvalue15);
			return partitionedCode;
		}

		// Token: 0x0600FF88 RID: 65416 RVA: 0x0036C394 File Offset: 0x0036A594
		private static PartitionedCode ToReadableCastToNumber(inputNumber_castToNumber input)
		{
			SSAValue ssavalue = ReadablePythonTranslator.ResolveVariable(input.castToNumber.Cast_AsDecimal().cell.Variable);
			return new PartitionedCode(PythonExpressionUtils.IfThenElse(PythonExpressionUtils.IsInstance(ssavalue, PythonExpressionUtils.Dot(new SSAValue[]
			{
				PythonExpressionUtils.Decimal,
				PythonExpressionUtils.MkLiteral("Decimal")
			})), ssavalue, PythonExpressionUtils.DDecimal(ssavalue)), null, new string[] { "decimal" }, new SSAValue[] { ssavalue });
		}

		// Token: 0x0600FF89 RID: 65417 RVA: 0x0036C418 File Offset: 0x0036A618
		private static PartitionedCode TranslateRoundNumber(SSARegister numObj, RoundingSpec rSpec)
		{
			decimal zero = rSpec.Zero;
			decimal delta = rSpec.Delta;
			RoundingMode mode = rSpec.Mode;
			string text;
			if (mode == RoundingMode.Down)
			{
				text = "ROUND_FLOOR";
			}
			else if (mode == RoundingMode.UpOrNext)
			{
				text = "ROUND_FLOOR";
			}
			else if (mode == RoundingMode.Up)
			{
				text = "ROUND_CEILING";
			}
			else if (mode == RoundingMode.TowardZero)
			{
				text = "ROUND_DOWN";
			}
			else if (mode == RoundingMode.AwayFromZero)
			{
				text = "ROUND_UP";
			}
			else
			{
				text = "ROUND_HALF_UP";
			}
			PartitionedCode partitionedCode = new PartitionedCode(null, null, null, null);
			if (zero == 0m && ReadablePythonTranslatorNumber.precisions.Contains(delta))
			{
				SSARValue ssarvalue;
				if (mode != RoundingMode.Nearest)
				{
					ssarvalue = PythonExpressionUtils.Quantize(numObj, string.Format("{0}", delta).ToPythonLiteral(), text);
					if (mode == RoundingMode.UpOrNext)
					{
						ssarvalue = PythonExpressionUtils.Add(new SSAValue[]
						{
							ssarvalue,
							PythonExpressionUtils.DDecimal(PythonExpressionUtils.MkPyLiteral(string.Format("{0}", delta)))
						});
					}
				}
				else
				{
					ssarvalue = PythonExpressionUtils.Quantize(PythonExpressionUtils.Abs(numObj), string.Format("{0}", delta).ToPythonLiteral(), text);
					ssarvalue = PythonExpressionUtils.Dot(new SSAValue[]
					{
						ssarvalue,
						PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "copy_sign", new SSAValue[] { numObj })
					});
				}
				partitionedCode.SetExpr(PythonExpressionUtils.Dot(new SSAValue[]
				{
					ssarvalue,
					PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "normalize", Array.Empty<SSAValue>())
				}));
				return partitionedCode;
			}
			bool flag = decimal.Floor(delta) == delta;
			SSARValue ssarvalue2 = (flag ? PythonExpressionUtils.MkLiteral(delta) : PythonExpressionUtils.DDecimal(PythonExpressionUtils.MkPyLiteral(string.Format("{0}", delta))));
			SSARValue ssarvalue3 = PythonExpressionUtils.DivideBy((zero == 0m) ? numObj : PythonExpressionUtils.Minus(numObj, zero), ssarvalue2);
			SSARValue ssarvalue4 = PythonExpressionUtils.Dot(new SSAValue[]
			{
				PythonExpressionUtils.Decimal,
				PythonExpressionUtils.MkLiteral(text)
			});
			SSARValue ssarvalue5;
			bool flag2;
			if (mode == RoundingMode.Down || mode == RoundingMode.UpOrNext || mode == RoundingMode.Up)
			{
				ssarvalue5 = PythonExpressionUtils.Dot(new SSAValue[]
				{
					PythonExpressionUtils.MkLiteral("math"),
					PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, (mode == RoundingMode.Up) ? "ceil" : "floor", new SSAValue[] { ssarvalue3 })
				});
				flag2 = true;
				partitionedCode.AddImport("math");
			}
			else
			{
				if (mode != RoundingMode.Nearest)
				{
					ssarvalue5 = PythonExpressionUtils.Dot(new SSAValue[]
					{
						ssarvalue3,
						PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "to_integral_value", new SSAValue[] { PythonExpressionUtils.NamedArg("rounding", ssarvalue4) })
					});
				}
				else
				{
					ssarvalue5 = PythonExpressionUtils.Dot(new SSAValue[]
					{
						PythonExpressionUtils.Abs(ssarvalue3),
						PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "to_integral_value", new SSAValue[] { PythonExpressionUtils.NamedArg("rounding", ssarvalue4) })
					});
					ssarvalue5 = PythonExpressionUtils.Dot(new SSAValue[]
					{
						ssarvalue5,
						PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "copy_sign", new SSAValue[] { numObj })
					});
				}
				SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.NumType, "rounded_num");
				partitionedCode.LocalAddLine(new SSAStep(ssaregister, ssarvalue5, ""));
				ssarvalue5 = PythonExpressionUtils.IfThenElse(PythonExpressionUtils.Dot(new SSAValue[]
				{
					ssaregister,
					PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "is_zero", Array.Empty<SSAValue>())
				}), PythonExpressionUtils.Abs(ssaregister), ssaregister);
				flag2 = false;
			}
			SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.NumType, "rounded_steps");
			partitionedCode.LocalAddLine(new SSAStep(ssaregister2, ssarvalue5, ""));
			ssarvalue5 = PythonExpressionUtils.Times(ssaregister2, ssarvalue2);
			flag2 = flag2 && flag;
			if (mode == RoundingMode.UpOrNext)
			{
				ssarvalue5 = PythonExpressionUtils.Add(new SSAValue[] { ssarvalue5, ssarvalue2 });
			}
			if (zero != 0m)
			{
				ssarvalue5 = PythonExpressionUtils.Add(new SSAValue[]
				{
					ssarvalue5,
					PythonExpressionUtils.MkLiteral(zero)
				});
			}
			if (!flag2)
			{
				ssarvalue5 = PythonExpressionUtils.Dot(new SSAValue[]
				{
					ssarvalue5,
					PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "normalize", Array.Empty<SSAValue>())
				});
			}
			else
			{
				ssarvalue5 = PythonExpressionUtils.DDecimal(ssarvalue5);
			}
			partitionedCode.SetExpr(ssarvalue5);
			return partitionedCode;
		}

		// Token: 0x0600FF8A RID: 65418 RVA: 0x0036C824 File Offset: 0x0036AA24
		private static PartitionedCode ToReadableRangeFormatNumber(RangeFormatNumber r, SSARegister numObj, NumberFormat format)
		{
			RoundingSpec value = r.rangeNumber.Cast_RangeRoundNumber().roundingSpec.Value;
			PartitionedCode partitionedCode = ReadablePythonTranslatorNumber.TranslateRoundNumber(numObj, value);
			numObj = new SSARegister(null, PythonExpressionUtils.NumType, "rounded_num");
			numObj = partitionedCode.IntroduceNewVarIf(numObj);
			return ReadablePythonTranslatorNumber.TranslateFormatNumber(numObj, format, partitionedCode);
		}

		// Token: 0x0600FF8B RID: 65419 RVA: 0x0036C880 File Offset: 0x0036AA80
		private static PartitionedCode TranslateFormatNumber(SSARegister numObj, NumberFormat format, PartitionedCode ans)
		{
			bool flag = false;
			bool flag2 = false;
			if (format.Details != NumberFormatDetails.Default && format.Details.SeparatorChar.HasValue && format.Details.SeparatedSectionSizes.HasValue)
			{
				if (format.Details.SeparatorChar.Value == ',')
				{
					if (format.Details.SeparatedSectionSizes.Value.All((uint x) => x == 3U))
					{
						flag = true;
						goto IL_0098;
					}
				}
				flag2 = true;
			}
			IL_0098:
			NumberFormatDetails details = format.Details;
			SSARegister ssaregister;
			if (details != null && details.Scale.HasValue && format.Details.Scale.Value != 1m)
			{
				ssaregister = new SSARegister(null, PythonExpressionUtils.NumType, "scaled_number");
				decimal value = format.Details.Scale.Value;
				SSARValue ssarvalue = PythonExpressionUtils.DDecimal(FormattableString.Invariant(FormattableStringFactory.Create("\"{0}\"", new object[] { value })));
				SSALiteral ssaliteral = PythonExpressionUtils.MkLiteral(1U);
				ans.LocalAddLine(new SSAStep(ssaregister, (value < 1m) ? PythonExpressionUtils.Times(numObj, ssarvalue) : PythonExpressionUtils.DivideBy(numObj, PythonExpressionUtils.DivideBy(ssaliteral, ssarvalue)), ""));
			}
			else
			{
				ssaregister = numObj;
			}
			bool hasValue = format.MaxTrailingZeros.HasValue;
			bool hasValue2 = format.MinLeadingZeros.HasValue;
			NumberFormatDetails details2 = format.Details;
			bool flag3 = details2 != null && details2.CurrencySymbol.HasValue;
			bool hasValue3 = format.MinLeadingZerosAndWhitespace.HasValue;
			bool hasValue4 = format.MinTrailingZeros.HasValue;
			bool hasValue5 = format.MinTrailingZerosAndWhitespace.HasValue;
			SSARegister ssaregister4;
			if (hasValue)
			{
				uint value2 = format.MaxTrailingZeros.Value;
				string text = "1." + new string('0', (int)value2);
				SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.NumType, "quantized_number");
				SSARValue ssarvalue2 = PythonExpressionUtils.Quantize(ssaregister, text.ToPythonLiteral(), "ROUND_HALF_UP");
				ans.LocalAddLine(new SSAStep(ssaregister2, ssarvalue2, ""));
				SSARegister ssaregister3 = new SSARegister(null, PythonExpressionUtils.NumType, "normalized_number");
				SSARValue ssarvalue3 = PythonExpressionUtils.And(new SSAValue[] { PythonExpressionUtils.Dot(new SSAValue[]
				{
					ssaregister2,
					PythonExpressionUtils.MkFunApp(PythonExpressionUtils.BoolType, "is_zero", Array.Empty<SSAValue>())
				}) });
				SSARValue ssarvalue4 = PythonExpressionUtils.Abs(ssaregister2);
				ans.LocalAddLine(new SSAStep(ssaregister3, PythonExpressionUtils.IfThenElse(ssarvalue3, ssarvalue4, ssaregister2), ""));
				ssarvalue3 = PythonExpressionUtils.LessEquals(ReadablePythonTranslatorNumber.DefineVarForDecimalLength(ssaregister, ans), PythonExpressionUtils.MkLiteral(value2));
				ssaregister4 = new SSARegister(null, PythonExpressionUtils.NumType, "normalized_quantized_num");
				ans.LocalAddLine(new SSAStep(ssaregister4, PythonExpressionUtils.IfThenElse(ssarvalue3, ssaregister, ssaregister3), ""));
			}
			else
			{
				ssaregister4 = ssaregister;
			}
			string text2 = (flag ? "{0:,f}" : "{0:f}");
			if (!flag2 && !hasValue2 && !flag3 && !hasValue3 && !hasValue4 && !hasValue5)
			{
				ans.SetExpr(PythonExpressionUtils.Format(text2, ssaregister4));
				return ans;
			}
			SSARValue ssarvalue5 = PythonExpressionUtils.Split(PythonExpressionUtils.Format(text2, PythonExpressionUtils.Abs(ssaregister4)), PythonExpressionUtils.MkPyLiteral("."), PythonExpressionUtils.MkLiteral(1U));
			SSARegister ssaregister5 = new SSARegister(null, PythonExpressionUtils.StrType, "abs_num_str");
			ans.LocalAddLine(new SSAStep(ssaregister5, ssarvalue5, ""));
			SSARValue ssarvalue6 = PythonExpressionUtils.GetItem(ssaregister5, PythonExpressionUtils.MkLiteral(0U));
			SSARegister ssaregister6 = null;
			if (flag2)
			{
				ssaregister6 = new SSARegister(null, PythonExpressionUtils.NumType, "int_part");
				ans.LocalAddLine(new SSAStep(ssaregister6, ssarvalue6, ""));
				SSARValue ssarvalue7 = PythonExpressionUtils.Len(ssaregister6);
				uint[] value3 = format.Details.SeparatedSectionSizes.Value;
				SSARValue ssarvalue8 = ssarvalue7;
				for (int i = 0; i < value3.Length - 1; i++)
				{
					ssarvalue8 = PythonExpressionUtils.Minus(ssarvalue8, value3[i]);
				}
				SSARegister ssaregister7 = new SSARegister(null, PythonExpressionUtils.NumType, "len_repeated_part");
				if (value3.Length > 1)
				{
					ans.LocalAddLine(new SSAStep(ssaregister7, PythonExpressionUtils.Max0(ssarvalue8), ""));
				}
				else
				{
					ans.LocalAddLine(new SSAStep(ssaregister7, ssarvalue7, ""));
				}
				uint num = value3[value3.Length - 1];
				SSARValue ssarvalue9 = PythonExpressionUtils.IntDivideBy(ssaregister7, num);
				SSARegister ssaregister8 = new SSARegister(null, PythonExpressionUtils.NumType, "num_parts");
				ans.LocalAddLine(new SSAStep(ssaregister8, ssarvalue9, ""));
				SSALiteral ssaliteral2 = PythonExpressionUtils.MkLiteral(num);
				SSAValue ssavalue = PythonExpressionUtils.Slice(ssaregister6, PythonExpressionUtils.Zero, PythonExpressionUtils.Minus(ssaregister7, PythonExpressionUtils.Times(ssaregister8, ssaliteral2)));
				SSALiteral ssaliteral3 = PythonExpressionUtils.MkLiteral("i");
				SSAValue ssavalue2 = PythonExpressionUtils.Minus(ssaregister7, PythonExpressionUtils.Times(ssaliteral3, PythonExpressionUtils.MkLiteral(num)));
				SSAValue ssavalue3 = PythonExpressionUtils.Minus(ssaregister7, PythonExpressionUtils.Times(PythonExpressionUtils.Minus(ssaliteral3, 1), PythonExpressionUtils.MkLiteral(num)));
				SSAValue ssavalue4 = PythonExpressionUtils.ForIn(PythonExpressionUtils.Slice(ssaregister6, ssavalue2, ssavalue3), ssaliteral3, PythonExpressionUtils.Range(ssaregister8, 0, -1));
				ssavalue2 = ssaregister7;
				List<SSAValue> list = new List<SSAValue>();
				for (int j = value3.Length - 2; j >= 0; j--)
				{
					list.Add(PythonExpressionUtils.Slice(ssaregister6, ssavalue2, PythonExpressionUtils.Add(new SSAValue[]
					{
						ssavalue2,
						PythonExpressionUtils.MkLiteral(value3[j])
					})));
					ssavalue2 = PythonExpressionUtils.Add(new SSAValue[]
					{
						ssavalue2,
						PythonExpressionUtils.MkLiteral(value3[j])
					});
				}
				SSAValue ssavalue5 = PythonExpressionUtils.Add(new SSAValue[]
				{
					PythonExpressionUtils.MakeList(new SSAValue[] { ssavalue }),
					PythonExpressionUtils.MakeList(new SSAValue[] { ssavalue4 })
				});
				if (value3.Length > 1)
				{
					ssavalue5 = PythonExpressionUtils.Add(new SSAValue[]
					{
						ssavalue5,
						PythonExpressionUtils.MakeList(list)
					});
				}
				ssarvalue6 = PythonExpressionUtils.Join(PythonExpressionUtils.MkPyLiteral(format.Details.SeparatorChar.Value.ToString()), PythonExpressionUtils.Filter(PythonExpressionUtils.None, ssavalue5));
				ssaregister6 = null;
			}
			if (hasValue2)
			{
				SSARegister ssaregister9 = new SSARegister(null, PythonExpressionUtils.StrType, "int_part");
				ans.LocalAddLine(new SSAStep(ssaregister9, ssarvalue6, ""));
				uint value4 = format.MinLeadingZeros.Value;
				ssaregister6 = new SSARegister(null, PythonExpressionUtils.StrType, "int_part");
				ans.LocalAddLine(new SSAStep(ssaregister6, PythonExpressionUtils.RJust(ssaregister9, value4, '0'), ""));
			}
			if (flag3)
			{
				SSAValue ssavalue6 = ssaregister6 ?? ssarvalue6;
				SSALiteral ssaliteral4 = PythonExpressionUtils.MkPyLiteral(format.Details.CurrencySymbol.Value.ToString());
				SSARValue ssarvalue10 = PythonExpressionUtils.Add(new SSAValue[] { ssaliteral4, ssavalue6 });
				ssaregister6 = new SSARegister(null, PythonExpressionUtils.StrType, "int_part");
				ans.LocalAddLine(new SSAStep(ssaregister6, ssarvalue10, ""));
			}
			SSARValue ssarvalue11 = PythonExpressionUtils.IfThenElse(PythonExpressionUtils.LessEquals(PythonExpressionUtils.MkLiteral(0U), ssaregister4), PythonExpressionUtils.MkPyLiteral(""), PythonExpressionUtils.MkPyLiteral("-"));
			SSARValue ssarvalue12 = PythonExpressionUtils.Add(new SSAValue[]
			{
				ssarvalue11,
				ssaregister6 ?? ssarvalue6
			});
			ssaregister6 = new SSARegister(null, PythonExpressionUtils.StrType, "int_part");
			ans.LocalAddLine(new SSAStep(ssaregister6, ssarvalue12, ""));
			if (hasValue3)
			{
				uint value5 = format.MinLeadingZerosAndWhitespace.Value;
				if (!hasValue2 || format.MinLeadingZeros.Value < value5)
				{
					SSARValue ssarvalue13 = PythonExpressionUtils.RJust(ssaregister6, value5, ' ');
					ssaregister6 = new SSARegister(null, PythonExpressionUtils.StrType, "int_part");
					ans.LocalAddLine(new SSAStep(ssaregister6, ssarvalue13, ""));
				}
			}
			SSARegister ssaregister10 = new SSARegister(null, PythonExpressionUtils.StrType, "dec_part");
			SSAValue ssavalue7 = PythonExpressionUtils.Equals(PythonExpressionUtils.Len(ssaregister5), PythonExpressionUtils.MkLiteral(2U));
			SSAValue item = PythonExpressionUtils.GetItem(ssaregister5, PythonExpressionUtils.MkLiteral(1U));
			SSARValue ssarvalue14 = PythonExpressionUtils.IfThenElse(ssavalue7, item, PythonExpressionUtils.MkPyLiteral(""));
			ans.LocalAddLine(new SSAStep(ssaregister10, ssarvalue14, ""));
			if (hasValue4)
			{
				uint value6 = format.MinTrailingZeros.Value;
				SSARValue ssarvalue15 = PythonExpressionUtils.LJust(ssaregister10, value6, '0');
				ssaregister10 = new SSARegister(null, PythonExpressionUtils.StrType, "dec_part_with_zeros");
				ans.LocalAddLine(new SSAStep(ssaregister10, ssarvalue15, ""));
			}
			if (hasValue5)
			{
				uint value7 = format.MinTrailingZerosAndWhitespace.Value;
				if (!hasValue4 || value7 > format.MinTrailingZeros.Value)
				{
					SSARValue ssarvalue16 = PythonExpressionUtils.LJust(ssaregister10, value7, ' ');
					ssaregister10 = new SSARegister(null, PythonExpressionUtils.StrType, "dec_part_with_ws");
					ans.LocalAddLine(new SSAStep(ssaregister10, ssarvalue16, ""));
				}
			}
			SSAValue ssavalue8 = ssaregister10;
			SSALiteral ssaliteral5 = PythonExpressionUtils.MkPyLiteral(format.Details.DecimalMarkChar.ToString());
			SSARValue ssarvalue17;
			if (hasValue5 || (hasValue4 && format.MinTrailingZeros.Value > 0U))
			{
				ssarvalue17 = PythonExpressionUtils.Add(new SSAValue[] { ssaliteral5, ssavalue8 });
			}
			else
			{
				ssarvalue17 = PythonExpressionUtils.IfThenElse(ssavalue7, PythonExpressionUtils.Add(new SSAValue[] { ssaliteral5, item }), PythonExpressionUtils.MkPyLiteral(""));
			}
			ssaregister10 = new SSARegister(null, PythonExpressionUtils.StrType, "dec_part");
			ans.LocalAddLine(new SSAStep(ssaregister10, ssarvalue17, ""));
			SSARValue ssarvalue18 = PythonExpressionUtils.Add(new SSAValue[] { ssaregister6, ssaregister10 });
			ans.SetExpr(ssarvalue18);
			return ans;
		}

		// Token: 0x0600FF8C RID: 65420 RVA: 0x0036D1F4 File Offset: 0x0036B3F4
		private static SSARegister DefineVarForDecimalLength(SSARegister numObj, PartitionedCode ans)
		{
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.NumType, "decimal_len");
			SSARValue ssarvalue = PythonExpressionUtils.Dot(new SSAValue[]
			{
				numObj,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.NumType, "as_tuple", Array.Empty<SSAValue>()),
				PythonExpressionUtils.MkLiteral("exponent")
			});
			SSARValue ssarvalue2 = PythonExpressionUtils.Minus(PythonExpressionUtils.Zero, ssarvalue);
			ans.LocalAddLine(new SSAStep(ssaregister, PythonExpressionUtils.Max(new SSAValue[]
			{
				ssarvalue2,
				PythonExpressionUtils.Zero
			}), ""));
			return ssaregister;
		}

		// Token: 0x0600FF8D RID: 65421 RVA: 0x0036D27C File Offset: 0x0036B47C
		private static NumberFormat ConvertToNumberFormat(numberFormat format)
		{
			GrammarBuilders grammarBuilders = ReadablePythonTranslatorNumber.g;
			Func<BuildNumberFormat, NumberFormat> func;
			if ((func = ReadablePythonTranslatorNumber.<>O.<1>__HandleBuildNumberFormat) == null)
			{
				func = (ReadablePythonTranslatorNumber.<>O.<1>__HandleBuildNumberFormat = new Func<BuildNumberFormat, NumberFormat>(ReadablePythonTranslatorNumber.HandleBuildNumberFormat));
			}
			Func<numberFormat_numberFormatLiteral, NumberFormat> func2;
			if ((func2 = ReadablePythonTranslatorNumber.<>O.<2>__HandleNumberFormatLiteral) == null)
			{
				func2 = (ReadablePythonTranslatorNumber.<>O.<2>__HandleNumberFormatLiteral = new Func<numberFormat_numberFormatLiteral, NumberFormat>(ReadablePythonTranslatorNumber.HandleNumberFormatLiteral));
			}
			return format.Switch<NumberFormat>(grammarBuilders, func, func2);
		}

		// Token: 0x0600FF8E RID: 65422 RVA: 0x0036D2CC File Offset: 0x0036B4CC
		private static NumberFormat HandleNumberFormatLiteral(numberFormat_numberFormatLiteral p)
		{
			return p.numberFormatLiteral.Value;
		}

		// Token: 0x0600FF8F RID: 65423 RVA: 0x0036D2E8 File Offset: 0x0036B4E8
		private static NumberFormat HandleBuildNumberFormat(BuildNumberFormat p)
		{
			return Semantics.BuildNumberFormat(p.minTrailingZeros.Value, p.maxTrailingZeros.Value, p.minTrailingZerosAndWhitespace.Value, p.minLeadingZeros.Value, p.minLeadingZerosAndWhitespace.Value, p.numberFormatDetails.Value);
		}

		// Token: 0x0600FF90 RID: 65424 RVA: 0x0036D358 File Offset: 0x0036B558
		private static PartitionedCode ToReadableRangeString(rangeString p, SSARegister numObj, NumberFormat numberFormat)
		{
			RangeConcat rangeConcat;
			if (!p.Is_RangeConcat(ReadablePythonTranslatorNumber.g, out rangeConcat))
			{
				return ReadablePythonTranslatorNumber.ToReadableRangeSubstring(p.Cast_rangeString_rangeSubstring(ReadablePythonTranslatorNumber.g).rangeSubstring, numObj, numberFormat);
			}
			PartitionedCode partitionedCode = ReadablePythonTranslatorNumber.ToReadableRangeSubstring(rangeConcat.rangeSubstring, numObj, numberFormat);
			if (partitionedCode == null)
			{
				return null;
			}
			PartitionedCode partitionedCode2 = ReadablePythonTranslatorNumber.ToReadableRangeString(rangeConcat.rangeString, numObj, numberFormat);
			partitionedCode.Merge(partitionedCode2);
			partitionedCode.SetExpr(PythonExpressionUtils.Add(new SSAValue[] { partitionedCode.Expr, partitionedCode2.Expr }));
			return partitionedCode;
		}

		// Token: 0x0600FF91 RID: 65425 RVA: 0x0036D3E0 File Offset: 0x0036B5E0
		private static PartitionedCode ToReadableRangeSubstring(rangeSubstring rss, SSARegister numObj, NumberFormat numberFormat)
		{
			RangeConstStr rangeConstStr;
			if (rss.Is_RangeConstStr(ReadablePythonTranslatorNumber.g, out rangeConstStr))
			{
				return new PartitionedCode(PythonExpressionUtils.MkPyLiteral(rangeConstStr.s.Value), null, null, null);
			}
			return ReadablePythonTranslatorNumber.ToReadableRangeFormatNumber(rss.As_RangeFormatNumber(ReadablePythonTranslatorNumber.g).Value, numObj, numberFormat);
		}

		// Token: 0x0600FF94 RID: 65428 RVA: 0x0036D468 File Offset: 0x0036B668
		[CompilerGenerated]
		internal static SSARValue <ToReadableParsedNumber>g__getItem|6_0(SSAValue a, int b)
		{
			return PythonExpressionUtils.GetItem(a, PythonExpressionUtils.MkLiteral(b));
		}

		// Token: 0x04005FFC RID: 24572
		private static readonly GrammarBuilders g = Language.Build;

		// Token: 0x04005FFD RID: 24573
		private static HashSet<decimal> precisions = (from i in Enumerable.Range(0, 10)
			select 1m / (int)Math.Pow(10.0, (double)i)).ConvertToHashSet<decimal>();

		// Token: 0x02001DC6 RID: 7622
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005FFE RID: 24574
			public static Func<inputNumber_castToNumber, PartitionedCode> <0>__ToReadableCastToNumber;

			// Token: 0x04005FFF RID: 24575
			public static Func<BuildNumberFormat, NumberFormat> <1>__HandleBuildNumberFormat;

			// Token: 0x04006000 RID: 24576
			public static Func<numberFormat_numberFormatLiteral, NumberFormat> <2>__HandleNumberFormatLiteral;
		}
	}
}
