using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DBC RID: 7612
	public class ReadablePythonTranslatorDateTime
	{
		// Token: 0x0600FF32 RID: 65330 RVA: 0x0036962C File Offset: 0x0036782C
		private static bool TryTranslateDtRangeString(dtRangeString p, SSARegister dtObjName, DateTimeFormat dtFormat, out PartitionedCode ans)
		{
			dtRangeString_dtRangeSubstring dtRangeString_dtRangeSubstring;
			if (p.Is_dtRangeString_dtRangeSubstring(Language.Build, out dtRangeString_dtRangeSubstring))
			{
				return ReadablePythonTranslatorDateTime.TryTranslateDtRangeSubstring(dtRangeString_dtRangeSubstring.dtRangeSubstring, dtObjName, dtFormat, out ans);
			}
			return ReadablePythonTranslatorDateTime.TryTranslateConcat(p.Cast_DtRangeConcat(Language.Build), dtObjName, dtFormat, out ans);
		}

		// Token: 0x0600FF33 RID: 65331 RVA: 0x00369670 File Offset: 0x00367870
		private static bool TryTranslateDtRangeSubstring(dtRangeSubstring p, SSARegister dtObjName, DateTimeFormat dtFormat, out PartitionedCode ans)
		{
			DtRangeConstStr dtRangeConstStr;
			if (p.Is_DtRangeConstStr(Language.Build, out dtRangeConstStr))
			{
				ans = new PartitionedCode(PythonExpressionUtils.MkPyLiteral(dtRangeConstStr.s.Value), null, null, null);
				return true;
			}
			PartitionedCode partitionedCode;
			if (ReadablePythonTranslatorDateTime.TryTranslateRangeRoundDateTime(p.Cast_RangeFormatDateTime(Language.Build).rangeDateTime.Cast_RangeRoundDateTime(), dtObjName, out partitionedCode))
			{
				ans = ReadablePythonTranslatorDateTime.FormatDtFormat(dtFormat, partitionedCode.Expr, partitionedCode);
				return true;
			}
			ans = null;
			return false;
		}

		// Token: 0x0600FF34 RID: 65332 RVA: 0x003696EC File Offset: 0x003678EC
		private static bool TryTranslateRangeRoundDateTime(RangeRoundDateTime p, SSARegister dtObjName, out PartitionedCode ans)
		{
			return ReadablePythonTranslatorDateTime.TryTranslateRoundDateTime(p.dtRoundingSpec.Value, dtObjName, out ans);
		}

		// Token: 0x0600FF35 RID: 65333 RVA: 0x00369710 File Offset: 0x00367910
		private static bool TryTranslateConcat(DtRangeConcat p, SSARegister dtObjName, DateTimeFormat dtFormat, out PartitionedCode ans)
		{
			PartitionedCode partitionedCode;
			if (!ReadablePythonTranslatorDateTime.TryTranslateDtRangeSubstring(p.dtRangeSubstring, dtObjName, dtFormat, out partitionedCode))
			{
				ans = null;
				return false;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, "part");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			PartitionedCode partitionedCode2;
			if (!ReadablePythonTranslatorDateTime.TryTranslateDtRangeString(p.dtRangeString, dtObjName, dtFormat, out partitionedCode2))
			{
				ans = null;
				return false;
			}
			partitionedCode.Merge(partitionedCode2);
			partitionedCode.SetExpr(PythonExpressionUtils.Add(new SSAValue[] { partitionedCode.Expr, partitionedCode2.Expr }));
			ans = partitionedCode;
			return true;
		}

		// Token: 0x0600FF36 RID: 65334 RVA: 0x00369794 File Offset: 0x00367994
		internal static PartitionedCode ToReadableDateTime(LetSharedParsedDateTime p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslatorDateTime.ToReadableDateTime(p.inputDateTime);
			if (partitionedCode == null)
			{
				return null;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.DateType, "datetime_obj");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			LetSharedDateTimeFormat letSharedDateTimeFormat = p._LetB1.Cast_LetSharedDateTimeFormat();
			DateTimeFormat value = letSharedDateTimeFormat.outputDtFormat.Value;
			PartitionedCode partitionedCode2;
			if (!ReadablePythonTranslatorDateTime.TryTranslateDtRangeString(letSharedDateTimeFormat.dtRangeString, ssaregister, value, out partitionedCode2))
			{
				return null;
			}
			partitionedCode.Merge(partitionedCode2);
			partitionedCode.SetExpr(partitionedCode2.Expr);
			return partitionedCode;
		}

		// Token: 0x0600FF37 RID: 65335 RVA: 0x00369818 File Offset: 0x00367A18
		internal static PartitionedCode ToReadableDateTime(FormatPartialDateTime p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslatorDateTime.ToReadableDateTime(p.datetime);
			if (partitionedCode == null)
			{
				return null;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.ObjType, "dt_obj");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			return ReadablePythonTranslatorDateTime.FormatDtFormat(p.outputDtFormat.Value, partitionedCode.Expr, partitionedCode);
		}

		// Token: 0x0600FF38 RID: 65336 RVA: 0x0036986C File Offset: 0x00367A6C
		private static string NonStandardStrftimeFormatter(DateTimeFormatPart fmt)
		{
			string text;
			if (fmt.IsNumeric && !fmt.AllowsLeadingZeros() && ReadablePythonTranslatorDateTime._nonStandardFormats.TryGetValue(fmt.FullFormatString, out text))
			{
				return text;
			}
			StringDateTimeFormatPart stringDateTimeFormatPart = fmt as StringDateTimeFormatPart;
			string text2;
			string text3;
			if (stringDateTimeFormatPart == null || stringDateTimeFormatPart.BaseFormatString.FirstOrDefault<char>() != 't' || !stringDateTimeFormatPart.StringLookup.TryGetValue(0, out text2) || !(CultureInfo.CurrentCulture.DateTimeFormat.AMDesignator == text2) || !stringDateTimeFormatPart.StringLookup.TryGetValue(1, out text3) || !(CultureInfo.CurrentCulture.DateTimeFormat.PMDesignator == text3))
			{
				return null;
			}
			if (!(text2.ToLower() == text2))
			{
				return "%p";
			}
			return "%P";
		}

		// Token: 0x0600FF39 RID: 65337 RVA: 0x00369924 File Offset: 0x00367B24
		private static PartitionedCode FormatDtFormat(DateTimeFormat fmt, SSAValue dtObj, PartitionedCode code)
		{
			IReadOnlyList<DateTimeFormatPart> readOnlyList = ((fmt != null) ? fmt.FormatParts : null);
			IReadOnlyList<string> readOnlyList2;
			if (readOnlyList == null)
			{
				readOnlyList2 = null;
			}
			else
			{
				readOnlyList2 = readOnlyList.Select((DateTimeFormatPart part) => part.PosixOutputFormatString ?? ReadablePythonTranslatorDateTime.NonStandardStrftimeFormatter(part)).ToList<string>();
			}
			IReadOnlyList<string> readOnlyList3 = readOnlyList2;
			IEnumerable<Record<DateTimeFormatPart, string>> enumerable;
			if (readOnlyList == null)
			{
				enumerable = null;
			}
			else
			{
				IEnumerable<Record<DateTimeFormatPart, string>> enumerable2 = readOnlyList.ZipWith(readOnlyList3);
				enumerable = ((enumerable2 != null) ? enumerable2.ToList<Record<DateTimeFormatPart, string>>() : null);
			}
			SSARegister ssaregister = null;
			foreach (IGrouping<DateTimeFormatPart, Record<DateTimeFormatPart, string>> grouping in enumerable.SplitRuns(delegate(Record<DateTimeFormatPart, string> pf)
			{
				if (pf.Item2 == null)
				{
					return pf.Item1;
				}
				return null;
			}, null))
			{
				if (grouping.Key != null || grouping.Count<Record<DateTimeFormatPart, string>>() != 1)
				{
					goto IL_00EF;
				}
				ConstantDateTimeFormatPart constantDateTimeFormatPart = grouping.First<Record<DateTimeFormatPart, string>>().Item1 as ConstantDateTimeFormatPart;
				if (constantDateTimeFormatPart == null)
				{
					goto IL_00EF;
				}
				SSARegister ssaregister2 = new SSARegister(null, PythonExpressionUtils.StrType, "datetime_str");
				code.LocalAddLine(new SSAStep(ssaregister2, PythonExpressionUtils.MkPyLiteral(constantDateTimeFormatPart.ConstantString), ""));
				IL_020F:
				if (ssaregister2 == null)
				{
					return null;
				}
				if (ssaregister != null)
				{
					SSARegister ssaregister3 = new SSARegister(null, PythonExpressionUtils.StrType, "ans");
					code.LocalAddLine(new SSAStep(ssaregister3, PythonExpressionUtils.Add(new SSAValue[] { ssaregister, ssaregister2 }), ""));
					ssaregister = ssaregister3;
					continue;
				}
				ssaregister = ssaregister2;
				continue;
				IL_00EF:
				if (grouping.Key == null)
				{
					string text = string.Concat(grouping.Select((Record<DateTimeFormatPart, string> part) => part.Item2)).ToPythonLiteral();
					ssaregister2 = new SSARegister(null, PythonExpressionUtils.StrType, "datetime_str");
					string text2 = "";
					string text3 = text;
					if (grouping.Any((Record<DateTimeFormatPart, string> part) => part.Item1.PosixOutputFormatString == null))
					{
						string text4 = text.Replace("%-", "%#");
						if (text == text4)
						{
							text3 = text;
							text2 = "";
						}
						else if (RuntimeUtils.IsRunningOnWindows())
						{
							text3 = text4;
							text2 = "For non-windows platforms, use " + text;
						}
						else
						{
							text3 = text;
							text2 = "For windows, use " + text4;
						}
					}
					code.LocalAddLine(new SSAStep(ssaregister2, PythonExpressionUtils.Strftime(dtObj, text3), text2));
					code.AddImport("datetime");
					goto IL_020F;
				}
				if (grouping.Count<Record<DateTimeFormatPart, string>>() == 1)
				{
					ssaregister2 = ReadablePythonTranslatorDateTime.FormatDtFormatPart(grouping.First<Record<DateTimeFormatPart, string>>().Item1, dtObj, code);
					goto IL_020F;
				}
				return null;
			}
			code.SetExpr(ssaregister);
			return code;
		}

		// Token: 0x0600FF3A RID: 65338 RVA: 0x00369BCC File Offset: 0x00367DCC
		private static SSARegister FormatDtFormatPart(DateTimeFormatPart dtPart, SSAValue dtObj, PartitionedCode code)
		{
			ReadablePythonTranslatorDateTime.<>c__DisplayClass9_0 CS$<>8__locals1;
			CS$<>8__locals1.dtObj = dtObj;
			string text = null;
			SSARValue ssarvalue = null;
			NumericDateTimeFormatPart numericDateTimeFormatPart = dtPart as NumericDateTimeFormatPart;
			if (numericDateTimeFormatPart != null)
			{
				bool flag = true;
				char c = numericDateTimeFormatPart.BaseFormatString.FirstOrDefault<char>();
				if (c <= 'Y')
				{
					if (c <= 'M')
					{
						if (c != 'H')
						{
							if (c == 'M')
							{
								text = "month";
							}
						}
						else
						{
							text = "hour";
						}
					}
					else if (c != 'V')
					{
						if (c == 'Y')
						{
							ssarvalue = ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__dotApp|9_1(ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__dotApp|9_1(CS$<>8__locals1.dtObj, "date"), "isocalendar");
							ssarvalue = PythonExpressionUtils.GetItem(ssarvalue, PythonExpressionUtils.MkLiteral(0U));
						}
					}
					else
					{
						ssarvalue = ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__dotApp|9_1(ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__dotApp|9_1(CS$<>8__locals1.dtObj, "date"), "isocalendar");
						ssarvalue = PythonExpressionUtils.GetItem(ssarvalue, PythonExpressionUtils.MkLiteral(1U));
					}
				}
				else if (c <= 'q')
				{
					switch (c)
					{
					case 'd':
						text = "day";
						break;
					case 'e':
					case 'g':
					case 'k':
					case 'l':
						break;
					case 'f':
						ssarvalue = ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__getA|9_0("microsecond", ref CS$<>8__locals1);
						ssarvalue = PythonExpressionUtils.IntDivideBy(ssarvalue, 1000U);
						break;
					case 'h':
						if (numericDateTimeFormatPart.MinimumLength == 1)
						{
							ssarvalue = PythonExpressionUtils.LStrip(PythonExpressionUtils.Strftime(CS$<>8__locals1.dtObj, "%I".ToPythonLiteral()), "0".ToPythonLiteral());
						}
						else
						{
							if (numericDateTimeFormatPart.MinimumLength != 2)
							{
								throw new NotImplementedException("Unknown hour in period format: " + numericDateTimeFormatPart.BaseFormatString);
							}
							ssarvalue = PythonExpressionUtils.Strftime(CS$<>8__locals1.dtObj, "%I".ToPythonLiteral());
						}
						flag = false;
						break;
					case 'i':
						ssarvalue = PythonExpressionUtils.Add(new SSAValue[]
						{
							PythonExpressionUtils.IntDivideBy(PythonExpressionUtils.Minus(ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__getA|9_0("day", ref CS$<>8__locals1), 1), 7U),
							PythonExpressionUtils.MkLiteral(1U)
						});
						break;
					case 'j':
						ssarvalue = PythonExpressionUtils.Dot(new SSAValue[]
						{
							ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__dotApp|9_1(ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__dotApp|9_1(CS$<>8__locals1.dtObj, "date"), "timetuple"),
							PythonExpressionUtils.MkLiteral("tm_yday")
						});
						break;
					case 'm':
						text = "minute";
						break;
					default:
						if (c == 'q')
						{
							ssarvalue = PythonExpressionUtils.Add(new SSAValue[]
							{
								PythonExpressionUtils.IntDivideBy(PythonExpressionUtils.Minus(ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__getA|9_0("month", ref CS$<>8__locals1), 1), 3U),
								PythonExpressionUtils.MkLiteral(1U)
							});
						}
						break;
					}
				}
				else if (c != 's')
				{
					if (c == 'y')
					{
						text = "year";
					}
				}
				else
				{
					text = "second";
				}
				if (ssarvalue == null && text == null)
				{
					return null;
				}
				ssarvalue = ssarvalue ?? ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__getA|9_0(text, ref CS$<>8__locals1);
				if (flag)
				{
					if (numericDateTimeFormatPart.MinimumLength <= 1)
					{
						ssarvalue = PythonExpressionUtils.Str(ssarvalue);
					}
					else
					{
						ssarvalue = PythonExpressionUtils.Format(FormattableString.Invariant(FormattableStringFactory.Create("{{0:0{0}d}}", new object[] { numericDateTimeFormatPart.MinimumLength })), ssarvalue);
					}
				}
			}
			else
			{
				StringDateTimeFormatPart stringDateTimeFormatPart = dtPart as StringDateTimeFormatPart;
				string text2;
				string text3;
				if (stringDateTimeFormatPart != null && stringDateTimeFormatPart.BaseFormatString.FirstOrDefault<char>() == 't' && stringDateTimeFormatPart.StringLookup.TryGetValue(0, out text2) && stringDateTimeFormatPart.StringLookup.TryGetValue(1, out text3))
				{
					SSAValue ssavalue = PythonExpressionUtils.MkPyLiteral(text2);
					SSAValue ssavalue2 = PythonExpressionUtils.MkPyLiteral(text3);
					SSALiteral ssaliteral = PythonExpressionUtils.MkLiteral(11U);
					ssarvalue = PythonExpressionUtils.IfThenElse(PythonExpressionUtils.LessEquals(ReadablePythonTranslatorDateTime.<FormatDtFormatPart>g__getA|9_0("hour", ref CS$<>8__locals1), ssaliteral), ssavalue, ssavalue2);
				}
			}
			if (ssarvalue == null)
			{
				return null;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, "part_str");
			code.LocalAddLine(new SSAStep(ssaregister, ssarvalue, ""));
			return ssaregister;
		}

		// Token: 0x0600FF3B RID: 65339 RVA: 0x00369F54 File Offset: 0x00368154
		private static PartitionedCode ToReadableDateTime(datetime p)
		{
			datetime_inputDateTime datetime_inputDateTime;
			if (p.Is_datetime_inputDateTime(Language.Build, out datetime_inputDateTime))
			{
				return ReadablePythonTranslatorDateTime.ToReadableDateTime(datetime_inputDateTime.inputDateTime);
			}
			RoundPartialDateTime roundPartialDateTime;
			if (!p.Is_RoundPartialDateTime(Language.Build, out roundPartialDateTime))
			{
				return null;
			}
			PartitionedCode partitionedCode = ReadablePythonTranslatorDateTime.ToReadableDateTime(roundPartialDateTime.inputDateTime);
			if (partitionedCode == null)
			{
				return null;
			}
			dtRoundingSpec dtRoundingSpec = roundPartialDateTime.dtRoundingSpec;
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.DateType, "datetime_obj");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			PartitionedCode partitionedCode2;
			if (!ReadablePythonTranslatorDateTime.TryTranslateRoundDateTime(dtRoundingSpec.Value, ssaregister, out partitionedCode2))
			{
				return null;
			}
			partitionedCode.Merge(partitionedCode2);
			partitionedCode.SetExpr(partitionedCode2.Expr);
			return partitionedCode;
		}

		// Token: 0x0600FF3C RID: 65340 RVA: 0x00369FF0 File Offset: 0x003681F0
		private static SSARegister AddAssignment(PartitionedCode code, string name, SSARValue val)
		{
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.ObjType, name);
			code.LocalAddLine(new SSAStep(ssaregister, val, ""));
			return ssaregister;
		}

		// Token: 0x0600FF3D RID: 65341 RVA: 0x0036A020 File Offset: 0x00368220
		private static bool TryTranslateRoundDateTimeYear(DateTimeRoundingSpec spec, SSARegister dtObj, out PartitionedCode code)
		{
			if (spec.Unit != DateTimePart.Year)
			{
				code = null;
				return false;
			}
			code = new PartitionedCode(null, null, null, null);
			SSARValue ssarvalue = PythonExpressionUtils.Dot(new SSAValue[]
			{
				dtObj,
				PythonExpressionUtils.MkLiteral("year")
			});
			SSARValue ssarvalue2;
			switch (spec.Mode)
			{
			case RoundingMode.Nearest:
				ssarvalue2 = PythonExpressionUtils.Times(PythonExpressionUtils.MkFunApp(typeof(int), "math.floor", new SSAValue[] { PythonExpressionUtils.Add(new SSAValue[]
				{
					PythonExpressionUtils.DivideBy(ssarvalue, PythonExpressionUtils.MkLiteral(spec.Delta)),
					PythonExpressionUtils.MkLiteral(new decimal(0.5))
				}) }), PythonExpressionUtils.MkLiteral(spec.Delta));
				break;
			case RoundingMode.Down:
				ssarvalue2 = PythonExpressionUtils.Times(PythonExpressionUtils.IntDivideBy(ssarvalue, PythonExpressionUtils.MkLiteral(spec.Delta)), spec.Delta);
				break;
			case RoundingMode.Up:
				code.AddImport("math");
				ssarvalue2 = PythonExpressionUtils.Times(PythonExpressionUtils.Ceil(PythonExpressionUtils.DivideBy(ssarvalue, PythonExpressionUtils.MkLiteral(spec.Delta))), spec.Delta);
				break;
			default:
				throw new NotImplementedException(string.Format("Unknown rounding mode: {0}", spec.Mode));
			}
			SSARegister ssaregister = ReadablePythonTranslatorDateTime.AddAssignment(code, "rounded_year", ssarvalue2);
			SSARegister ssaregister2 = ReadablePythonTranslatorDateTime.AddAssignment(code, "rounded_year_obj", PythonExpressionUtils.DateTime(new SSAValue[]
			{
				ssaregister,
				PythonExpressionUtils.MkLiteral(1U),
				PythonExpressionUtils.MkLiteral(1U)
			}));
			code.SetExpr(ssaregister2);
			return true;
		}

		// Token: 0x0600FF3E RID: 65342 RVA: 0x0036A1A0 File Offset: 0x003683A0
		private static bool TryTranslateRoundDateTime(DateTimeRoundingSpec spec, SSARegister dtObj, out PartitionedCode code)
		{
			ReadablePythonTranslatorDateTime.<>c__DisplayClass13_0 CS$<>8__locals1 = new ReadablePythonTranslatorDateTime.<>c__DisplayClass13_0();
			CS$<>8__locals1.dtObj = dtObj;
			if (spec.Unit == DateTimePart.Year)
			{
				return ReadablePythonTranslatorDateTime.TryTranslateRoundDateTimeYear(spec, CS$<>8__locals1.dtObj, out code);
			}
			ReadablePythonTranslatorDateTime.<>c__DisplayClass13_0 CS$<>8__locals2 = CS$<>8__locals1;
			Dictionary<DateTimePart, string> dictionary = new Dictionary<DateTimePart, string>();
			dictionary[DateTimePart.Hour] = "hours";
			dictionary[DateTimePart.Minute] = "minutes";
			dictionary[DateTimePart.Second] = "seconds";
			dictionary[DateTimePart.Millisecond] = "milliseconds";
			CS$<>8__locals2.NameMap = dictionary;
			code = new PartitionedCode(null, null, null, null);
			SSAValue[] array = (from x in CS$<>8__locals1.NameMap.Keys
				where x != DateTimePart.Millisecond
				select PythonExpressionUtils.NamedArg(CS$<>8__locals1.NameMap[x], ReadablePythonTranslatorDateTime.<TryTranslateRoundDateTime>g__GetField|13_0(CS$<>8__locals1.dtObj, CS$<>8__locals1.NameMap[x].Substring(0, CS$<>8__locals1.NameMap[x].Length - 1)))).Concat(new SSARValue[]
			{
				PythonExpressionUtils.NamedArg("microseconds", ReadablePythonTranslatorDateTime.<TryTranslateRoundDateTime>g__GetField|13_0(CS$<>8__locals1.dtObj, "microsecond")),
				PythonExpressionUtils.NamedArg("days", PythonExpressionUtils.MkLiteral(1U))
			}).ToArray<SSARValue>();
			SSAValue[] array2 = array;
			SSARegister ssaregister = ReadablePythonTranslatorDateTime.AddAssignment(code, "base_value", PythonExpressionUtils.TimeDelta(array2));
			SSAValue ssavalue = PythonExpressionUtils.NamedArg(CS$<>8__locals1.NameMap[spec.Unit], PythonExpressionUtils.MkLiteral(spec.Delta));
			SSARegister ssaregister2 = ReadablePythonTranslatorDateTime.AddAssignment(code, "delta_value", PythonExpressionUtils.TimeDelta(new SSAValue[] { ssavalue }));
			CS$<>8__locals1.zero = spec.Zero;
			array = (from x in CS$<>8__locals1.NameMap.Keys
				where CS$<>8__locals1.zero.Get(x).HasValue && CS$<>8__locals1.zero.Get(x).Value != 0
				select PythonExpressionUtils.NamedArg(CS$<>8__locals1.NameMap[x], PythonExpressionUtils.MkLiteral(CS$<>8__locals1.zero.Get(x).Value))).ToArray<SSARValue>();
			SSAValue[] array3 = array;
			if (array3.Length != 0)
			{
				SSARegister ssaregister3 = ReadablePythonTranslatorDateTime.AddAssignment(code, "zero_value", PythonExpressionUtils.TimeDelta(array3));
				ssaregister = ReadablePythonTranslatorDateTime.AddAssignment(code, "base_value_shifted", PythonExpressionUtils.Minus(ssaregister, ssaregister3));
			}
			SSARegister ssaregister4 = ReadablePythonTranslatorDateTime.AddAssignment(code, "offset_value", PythonExpressionUtils.Mod(ssaregister, ssaregister2));
			SSARegister ssaregister5 = ReadablePythonTranslatorDateTime.AddAssignment(code, "rounded_down", PythonExpressionUtils.Minus(CS$<>8__locals1.dtObj, ssaregister4));
			SSARegister ssaregister7;
			if (spec.Mode == RoundingMode.UpOrNext || spec.Mode == RoundingMode.Up)
			{
				SSARegister ssaregister6 = ReadablePythonTranslatorDateTime.AddAssignment(code, "rounded_up_next", PythonExpressionUtils.Add(new SSAValue[] { ssaregister5, ssaregister2 }));
				ssaregister7 = ssaregister6;
				if (spec.Mode == RoundingMode.Up)
				{
					SSARValue ssarvalue = PythonExpressionUtils.IfThenElse(PythonExpressionUtils.Equals(ssaregister5, CS$<>8__locals1.dtObj), CS$<>8__locals1.dtObj, ssaregister6);
					ssaregister7 = ReadablePythonTranslatorDateTime.AddAssignment(code, "rounded_up", ssarvalue);
				}
			}
			else
			{
				if (spec.Mode != RoundingMode.Down)
				{
					return false;
				}
				ssaregister7 = ssaregister5;
			}
			DateTimePart? upperExcludePart = spec.UpperExcludePart;
			if (upperExcludePart != null)
			{
				SSAValue ssavalue2 = PythonExpressionUtils.TimeDelta(new SSAValue[] { PythonExpressionUtils.NamedArg(CS$<>8__locals1.NameMap[upperExcludePart.Value], PythonExpressionUtils.MkLiteral(spec.UpperExcludeAmount)) });
				ssaregister7 = ReadablePythonTranslatorDateTime.AddAssignment(code, "rounded_shifted", PythonExpressionUtils.Minus(ssaregister7, ssavalue2));
			}
			code.SetExpr(ssaregister7);
			return true;
		}

		// Token: 0x0600FF3F RID: 65343 RVA: 0x0036A478 File Offset: 0x00368678
		private static PartitionedCode ToReadableDateTime(inputDateTime p)
		{
			inputDateTime_parsedDateTime inputDateTime_parsedDateTime;
			if (p.Is_inputDateTime_parsedDateTime(Language.Build, out inputDateTime_parsedDateTime))
			{
				return ReadablePythonTranslatorDateTime.ToReadableDateTime(inputDateTime_parsedDateTime.parsedDateTime);
			}
			AsPartialDateTime asPartialDateTime;
			if (p.Is_AsPartialDateTime(Language.Build, out asPartialDateTime))
			{
				return ReadablePythonTranslator.ToReadablePythonExpr(asPartialDateTime.cell.Variable);
			}
			return null;
		}

		// Token: 0x0600FF40 RID: 65344 RVA: 0x0036A4C8 File Offset: 0x003686C8
		private static PartitionedCode ToReadableDateTime(parsedDateTime p)
		{
			return ReadablePythonTranslatorDateTime.ToReadableDateTime(p.Cast_ParsePartialDateTime());
		}

		// Token: 0x0600FF41 RID: 65345 RVA: 0x0036A4D8 File Offset: 0x003686D8
		private static PartitionedCode ToReadableDateTime(ParsePartialDateTime p)
		{
			PartitionedCode partitionedCode = ReadablePythonTranslator.ToReadablePythonExpr(p.SS.Node);
			if (partitionedCode == null)
			{
				return null;
			}
			SSARegister ssaregister = new SSARegister(null, PythonExpressionUtils.StrType, "datetime_str");
			ssaregister = partitionedCode.IntroduceNewVarIf(ssaregister);
			if (ReadablePythonTranslatorDateTime.TranslateDatetimeParsing(p.inputDtFormats.Value, ssaregister, partitionedCode))
			{
				return partitionedCode;
			}
			return null;
		}

		// Token: 0x0600FF42 RID: 65346 RVA: 0x0036A533 File Offset: 0x00368733
		private static bool DateTimeFormatAllows24Hour(DateTimeFormat fmt)
		{
			IEnumerable<DateTimeFormatPart> formatParts = fmt.FormatParts;
			Func<DateTimeFormatPart, bool> func;
			if ((func = ReadablePythonTranslatorDateTime.<>O.<0>__IsDTFormatPartHourAllowing24Hours) == null)
			{
				func = (ReadablePythonTranslatorDateTime.<>O.<0>__IsDTFormatPartHourAllowing24Hours = new Func<DateTimeFormatPart, bool>(ReadablePythonTranslatorDateTime.IsDTFormatPartHourAllowing24Hours));
			}
			return formatParts.Any(func);
		}

		// Token: 0x0600FF43 RID: 65347 RVA: 0x0036A55C File Offset: 0x0036875C
		private static bool IsDTFormatPartHourAllowing24Hours(DateTimeFormatPart part)
		{
			return part.IsNumeric && part.MatchedPart.HasValue && part.MatchedPart.Value == DateTimePart.Hour && (part.Attributes == null || !part.Attributes.Attributes.ContainsKey("disallow24Hour"));
		}

		// Token: 0x0600FF44 RID: 65348 RVA: 0x0036A5B8 File Offset: 0x003687B8
		private static bool TranslateDatetimeParsing(DateTimeFormat[] fmts, SSAValue dtString, PartitionedCode code)
		{
			if (fmts.Length == 1 && !ReadablePythonTranslatorDateTime.DateTimeFormatAllows24Hour(fmts[0]))
			{
				SSARegister ssaregister = ReadablePythonTranslatorDateTime.TranslateDatetimeParsing(fmts[0], dtString, code, true);
				if (ssaregister == null)
				{
					return false;
				}
				code.SetExpr(ssaregister);
				return true;
			}
			else
			{
				bool flag = fmts.CartesianProduct(fmts).Any((Record<DateTimeFormat, DateTimeFormat> x) => x.Item1 != x.Item2 && DateTimeFormatUtil.IsAmbiguous(x.Item1, x.Item2));
				List<SSARegister> list = new List<SSARegister>();
				foreach (DateTimeFormat dateTimeFormat in fmts)
				{
					SSARegister ssaregister2 = ReadablePythonTranslatorDateTime.TranslateDatetimeParsing(dateTimeFormat, dtString, code, false);
					list.Add(ssaregister2);
					if (ReadablePythonTranslatorDateTime.DateTimeFormatAllows24Hour(dateTimeFormat))
					{
						ssaregister2 = ReadablePythonTranslatorDateTime.TranslateDatetimeParsing(new DateTimeFormat(dateTimeFormat.FormatParts.Select(delegate(DateTimeFormatPart x)
						{
							if (!ReadablePythonTranslatorDateTime.IsDTFormatPartHourAllowing24Hours(x))
							{
								return x;
							}
							return new ConstantDateTimeFormatPart("24");
						})), dtString, code, false);
						SSAValue ssavalue = PythonExpressionUtils.TimeDelta(new SSAValue[] { PythonExpressionUtils.NamedArg("days", PythonExpressionUtils.MkLiteral(1U)) });
						list.Add(ReadablePythonTranslatorDateTime.AddAssignment(code, "dt_obj", PythonExpressionUtils.And(new SSAValue[]
						{
							ssaregister2,
							PythonExpressionUtils.Add(new SSAValue[] { ssaregister2, ssavalue })
						})));
					}
				}
				if (list.Any((SSARegister x) => x == null))
				{
					return false;
				}
				SSARValue ssarvalue = PythonExpressionUtils.MakeList(list.ToArray());
				if (flag)
				{
					string text = "unique_non_none_obj";
					Type dateType = PythonExpressionUtils.DateType;
					string text2 = text;
					SSAValue[] array = new SSARValue[] { ssarvalue };
					SSARValue ssarvalue2 = PythonExpressionUtils.MkFunApp(dateType, text2, array);
					SSARegister ssaregister3 = ReadablePythonTranslatorDateTime.AddAssignment(code, "datetime_obj", ssarvalue2);
					code.SetExpr(ssaregister3);
					string text3 = text;
					Func<IGeneratedFunction> func;
					if ((func = ReadablePythonTranslatorDateTime.<>O.<1>__UniqueNonNonePythonFunction) == null)
					{
						func = (ReadablePythonTranslatorDateTime.<>O.<1>__UniqueNonNonePythonFunction = new Func<IGeneratedFunction>(ReadablePythonTranslatorDateTime.UniqueNonNonePythonFunction));
					}
					code.AddContextIfNew(text3, func, 0);
					return true;
				}
				SSARegister ssaregister4 = new SSARegister(null, PythonExpressionUtils.DateType, "dt");
				SSARegister ssaregister5 = ReadablePythonTranslatorDateTime.AddAssignment(code, "datetime_obj", PythonExpressionUtils.Next(PythonExpressionUtils.ForInIf(ssaregister4, ssaregister4, ssarvalue, ssaregister4)));
				code.SetExpr(ssaregister5);
				return true;
			}
		}

		// Token: 0x0600FF45 RID: 65349 RVA: 0x0036A7C8 File Offset: 0x003689C8
		private static bool IsSpecialCase(IEnumerable<string> names)
		{
			if (names.Any((string g) => g.StartsWith("day_of_week")))
			{
				if (names.All((string g) => !g.Equals("day") && !g.Equals("day_of_year")))
				{
					if (names.Any((string g) => g.StartsWith("month")))
					{
						return names.Any((string g) => g.StartsWith("year") || g.StartsWith("week_year"));
					}
				}
			}
			return false;
		}

		// Token: 0x0600FF46 RID: 65350 RVA: 0x0036A874 File Offset: 0x00368A74
		public static Record<string, CodeForGeneratedFunction> GenerateDateTimeFormatParsingCode(DateTimeFormat fmt, string varName)
		{
			PartitionedCode partitionedCode = new PartitionedCode(null, null, null, null);
			SSAValue ssavalue = PythonExpressionUtils.MkLiteral(varName);
			ReadablePythonTranslatorDateTime.TranslateDatetimeParsing(new DateTimeFormat[] { fmt }, ssavalue, partitionedCode);
			return partitionedCode.ToCodeBuilder();
		}

		// Token: 0x0600FF47 RID: 65351 RVA: 0x0036A8AC File Offset: 0x00368AAC
		private static SSARegister TranslateDatetimeParsing(DateTimeFormat fmt, SSAValue dtString, PartitionedCode code, bool canFail)
		{
			IReadOnlyList<DateTimeFormatPart> formatParts = fmt.FormatParts;
			if (ReadablePythonTranslatorDateTime.IsSpecialCase(from p in formatParts
				where !(p is ConstantDateTimeFormatPart)
				select ReadablePythonTranslatorDateTime.BaseFormatToName[p.BaseFormatString]))
			{
				return ReadablePythonTranslatorDateTime.ParseFormatPythonFunction(fmt, dtString, code);
			}
			string text = fmt.PosixParsingFormatString;
			code.AddImport("datetime");
			if (text == null)
			{
				if (!formatParts.Skip(1).Zip(formatParts, (DateTimeFormatPart a, DateTimeFormatPart b) => a.IsNumeric && b.IsNumeric).Any((bool b) => b))
				{
					IReadOnlyList<string> readOnlyList = formatParts.Select((DateTimeFormatPart fp) => ReadablePythonTranslatorDateTime.ToPosixAggressive(fp, canFail)).ToList<string>();
					text = (readOnlyList.Any((string p) => p == null) ? null : string.Concat(readOnlyList));
				}
			}
			if (text != null)
			{
				SSARValue ssarvalue;
				if (canFail)
				{
					ssarvalue = PythonExpressionUtils.Strptime(dtString, text);
				}
				else
				{
					string text2 = "strptime_or_none";
					SSALiteral ssaliteral = PythonExpressionUtils.MkPyLiteral(text);
					ssarvalue = PythonExpressionUtils.MkFunApp(PythonExpressionUtils.DateType, text2, new SSAValue[] { dtString, ssaliteral });
					string text3 = text2;
					Func<IGeneratedFunction> func;
					if ((func = ReadablePythonTranslatorDateTime.<>O.<2>__StrptimeOrNonePythonFunction) == null)
					{
						func = (ReadablePythonTranslatorDateTime.<>O.<2>__StrptimeOrNonePythonFunction = new Func<IGeneratedFunction>(ReadablePythonTranslatorDateTime.StrptimeOrNonePythonFunction));
					}
					code.AddContextIfNew(text3, func, 0);
				}
				return ReadablePythonTranslatorDateTime.AddAssignment(code, "dt_obj", ssarvalue);
			}
			return ReadablePythonTranslatorDateTime.ParseFormatPythonFunction(fmt, dtString, code);
		}

		// Token: 0x0600FF48 RID: 65352 RVA: 0x0036AA58 File Offset: 0x00368C58
		private static string ToPosixAggressive(DateTimeFormatPart dtPart, bool periodInPosix)
		{
			if (dtPart is ConstantDateTimeFormatPart)
			{
				return dtPart.PosixParsingFormatString;
			}
			string text = ReadablePythonTranslatorDateTime.BaseFormatToName[dtPart.BaseFormatString];
			if (periodInPosix && text == "period")
			{
				return "%p";
			}
			return ReadablePythonTranslatorDateTime.NameToPosix[text];
		}

		// Token: 0x0600FF49 RID: 65353 RVA: 0x0036AAA8 File Offset: 0x00368CA8
		private static OpaqueGeneratedFunction UniqueNonNonePythonFunction()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			List<string> list = new List<string>();
			list.Add("parses");
			codeBuilder.AppendLine("[parse] = set([p for p in parses if p is not None])");
			codeBuilder.AppendLine("return parse");
			return new OpaqueGeneratedFunction(list.Select((string x) => Record.Create<string, Type>(x, null)).ToArray<Record<string, Type>>(), null, codeBuilder);
		}

		// Token: 0x0600FF4A RID: 65354 RVA: 0x0036AB14 File Offset: 0x00368D14
		private static OpaqueGeneratedFunction StrptimeOrNonePythonFunction()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			List<string> list = new List<string> { "x", "format" };
			using (codeBuilder.NewScope("try:", 1U))
			{
				codeBuilder.AppendLine("return datetime.datetime.strptime(x, format)");
			}
			using (codeBuilder.NewScope("except ValueError:", 1U))
			{
				codeBuilder.AppendLine("return None");
			}
			return new OpaqueGeneratedFunction(list.Select((string x) => Record.Create<string, Type>(x, null)).ToArray<Record<string, Type>>(), null, codeBuilder);
		}

		// Token: 0x0600FF4B RID: 65355 RVA: 0x0036ABDC File Offset: 0x00368DDC
		private static SSARegister DateTimeFormatToRegex(DateTimeFormat dtFormat, PartitionedCode code, out List<string> allGroups)
		{
			new StringBuilder();
			allGroups = new List<string>();
			SSARegister ssaregister = null;
			foreach (DateTimeFormatPart dateTimeFormatPart in dtFormat.FormatParts)
			{
				ConstantDateTimeFormatPart constantDateTimeFormatPart = dateTimeFormatPart as ConstantDateTimeFormatPart;
				SSARValue ssarvalue;
				if (constantDateTimeFormatPart != null)
				{
					ssarvalue = PythonExpressionUtils.MkPyLiteral("(?:" + Regex.Escape(constantDateTimeFormatPart.ConstantString) + ")");
				}
				else
				{
					if (!dateTimeFormatPart.MatchedPart.HasValue)
					{
						throw new NotImplementedException(string.Format("Unknown type of DateTimeFormatPart {0} found.", dateTimeFormatPart));
					}
					string text = ReadablePythonTranslatorDateTime.BaseFormatToName[dateTimeFormatPart.BaseFormatString];
					allGroups.Add(text);
					string text2;
					if (!dateTimeFormatPart.Token.HasValue)
					{
						text2 = ".";
					}
					else
					{
						string name = dateTimeFormatPart.Token.Value.Name;
						string text3;
						if (!(name == "Digits"))
						{
							if (!(name == "Alphanum"))
							{
								if (!(name == "ALL CAPS"))
								{
									if (!(name == "Lowercase word"))
									{
										if (!(name == "Camel Case"))
										{
											throw new NotImplementedException("Unexpected datetime format token: " + name);
										}
										text3 = "\\p{L}";
									}
									else
									{
										text3 = "\\p{Ll}";
									}
								}
								else
								{
									text3 = "\\p{Lu}";
								}
							}
							else
							{
								text3 = "[\\p{L}\\d]";
							}
						}
						else
						{
							text3 = "\\d";
						}
						text2 = text3;
					}
					string text4 = text2;
					ssarvalue = PythonExpressionUtils.MkPyLiteral(ReadablePythonTranslatorDateTime.<DateTimeFormatToRegex>g__PartToStr|28_1(text, text4, dateTimeFormatPart));
				}
				ssarvalue = ((ssaregister != null) ? PythonExpressionUtils.Add(new SSAValue[] { ssaregister, ssarvalue }) : ssarvalue);
				ssaregister = ReadablePythonTranslatorDateTime.AddAssignment(code, "regex_str", ssarvalue);
			}
			return ssaregister;
		}

		// Token: 0x0600FF4C RID: 65356 RVA: 0x0036ADB0 File Offset: 0x00368FB0
		private static SSARegister ParseFormatPythonFunction(DateTimeFormat dtFormat, SSAValue x, PartitionedCode code)
		{
			List<string> list;
			SSARegister ssaregister = ReadablePythonTranslatorDateTime.DateTimeFormatToRegex(dtFormat, code, out list);
			string text = "parse_as_datetime_obj";
			bool isSpecialCase = ReadablePythonTranslatorDateTime.IsSpecialCase(list);
			if (list.Select((string g) => ReadablePythonTranslatorDateTime.NameToPosix[g]).All((string s) => s != null) && !isSpecialCase)
			{
				string text2 = text;
				Func<IGeneratedFunction> func;
				if ((func = ReadablePythonTranslatorDateTime.<>O.<3>__ParseFormatGenericPythonFunction) == null)
				{
					func = (ReadablePythonTranslatorDateTime.<>O.<3>__ParseFormatGenericPythonFunction = new Func<IGeneratedFunction>(ReadablePythonTranslatorDateTime.ParseFormatGenericPythonFunction));
				}
				code.AddContextIfNew(text2, func, 0);
			}
			else
			{
				int num = (isSpecialCase ? 3 : 2);
				code.AddContextIfNew(text, () => ReadablePythonTranslatorDateTime.ParseFormatGenericFullPythonFunction(isSpecialCase), num);
			}
			code.AddImport("regex");
			code.AddImport("datetime");
			SSARValue ssarvalue = PythonExpressionUtils.MkFunApp(PythonExpressionUtils.DateType, text, new SSAValue[] { x, ssaregister });
			return ReadablePythonTranslatorDateTime.AddAssignment(code, "dt_obj", ssarvalue);
		}

		// Token: 0x0600FF4D RID: 65357 RVA: 0x0036AEB8 File Offset: 0x003690B8
		private static OpaqueGeneratedFunction ParseFormatGenericFullPythonFunction(bool isSpecialCase)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			List<string> list = new List<string> { "x", "regex_str" };
			string text = string.Join(", ", from kvp in ReadablePythonTranslatorDateTime.NameToPosix
				where kvp.Value != null
				select string.Concat(new string[] { "'", kvp.Key, "': '", kvp.Value, "'" }));
			codeBuilder.AppendLine("posix_format = { " + text + " }");
			codeBuilder.AppendLine("match = regex.fullmatch(regex_str, x)");
			using (codeBuilder.NewScope("if match:", 1U))
			{
				codeBuilder.AppendLine("fmt_str, val_str = '', ''");
				codeBuilder.AppendLine("match_dict = match.groupdict()");
				using (codeBuilder.NewScope("for k, v in match_dict.items():", 1U))
				{
					using (codeBuilder.NewScope("if k in ['year2', 'week_year2']:", 1U))
					{
						codeBuilder.AppendLine("year2 = int(match[k])");
						codeBuilder.AppendLine(string.Format("year = year2 + 2000 if year2 + 2000 <= {0} else year2 + 1900", CultureInfo.InvariantCulture.Calendar.TwoDigitYearMax));
						codeBuilder.AppendLine("fmt_str += '%Y ' if k == 'year2' else '%G '");
						codeBuilder.AppendLine("val_str += str(year) + ' '");
					}
					using (codeBuilder.NewScope("elif k == 'millisecond':", 1U))
					{
						codeBuilder.AppendLine("fmt_str += '%f '");
						codeBuilder.AppendLine("val_str += str(int(match['millisecond']) * 1000) + ' '");
					}
					using (codeBuilder.NewScope("elif k.startswith('hour') and match_dict.get('period'):", 1U))
					{
						codeBuilder.AppendLine("hour = int(match[k])");
						codeBuilder.AppendLine("period = match['period']");
						codeBuilder.AppendLine("hour = hour + (12 if period.lower() == 'pm' else 0)");
						using (codeBuilder.NewScope("if (hour == 12 or hour >= 24):", 1U))
						{
							codeBuilder.AppendLine("hour = hour - 12");
						}
						codeBuilder.AppendLine("fmt_str += '%H '");
						codeBuilder.AppendLine("val_str += str(hour) + ' '");
					}
					using (codeBuilder.NewScope("elif posix_format.get(k):", 1U))
					{
						codeBuilder.AppendLine("fmt_str += posix_format[k] + ' '");
						codeBuilder.AppendLine("val_str += v + ' '");
					}
				}
				if (isSpecialCase)
				{
					foreach (string text2 in new string[] { "day_of_week", "day_of_week_full", "day_of_week_in_month", "month", "month_name", "month_name_full", "year", "year2" })
					{
						codeBuilder.AppendLine(text2 + " = match_dict.get('" + text2 + "')");
					}
					codeBuilder.AppendLine("day_of_week = day_of_week or day_of_week_full");
					codeBuilder.AppendLine("month_name = month_name or month_name_full");
					using (codeBuilder.NewScope("if ('day' not in match_dict) and (day_of_week or day_of_week_in_month) and (month or month_name) and (year or year2):", 1U))
					{
						codeBuilder.AppendLine("days_of_week = ['mon', 'tue', 'wed', 'thu', 'fri', 'sat', 'sun']");
						codeBuilder.AppendLine("day_of_week_num = days_of_week.index(day_of_week[0:3].lower()) if day_of_week else 1");
						codeBuilder.AppendLine("day_of_week_in_month = int(day_of_week_in_month) if day_of_week_in_month else 1");
						codeBuilder.AppendLine("month = int(month) if month else datetime.datetime.strptime(month_name[0:3], '%b').month");
						codeBuilder.AppendLine("year = int(year)");
						codeBuilder.AppendLine("weekday_on_first = datetime.datetime(year=year, month=month, day=1).weekday()");
						codeBuilder.AppendLine("offset_of_this_weekday = day_of_week_num - weekday_on_first + 1 if day_of_week_num >= weekday_on_first else day_of_week_num - weekday_on_first + 8");
						codeBuilder.AppendLine("day = offset_of_this_weekday + (day_of_week_in_month - 1) * 7");
						codeBuilder.AppendLine("fmt_str += '%d '");
						codeBuilder.AppendLine("val_str += str(day) + ' '");
					}
				}
				using (codeBuilder.NewScope("try:", 1U))
				{
					codeBuilder.AppendLine("return datetime.datetime.strptime(val_str, fmt_str)");
				}
				using (codeBuilder.NewScope("except:", 1U))
				{
					codeBuilder.AppendLine("return None");
				}
			}
			using (codeBuilder.NewScope("else:", 1U))
			{
				codeBuilder.AppendLine("return None");
			}
			return new OpaqueGeneratedFunction(list.Select((string x) => Record.Create<string, Type>(x, null)).ToArray<Record<string, Type>>(), null, codeBuilder);
		}

		// Token: 0x0600FF4E RID: 65358 RVA: 0x0036B3B8 File Offset: 0x003695B8
		private static OpaqueGeneratedFunction ParseFormatGenericPythonFunction()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			List<string> list = new List<string> { "x", "regex_str" };
			string text = string.Join(", ", from kvp in ReadablePythonTranslatorDateTime.NameToPosix
				where kvp.Value != null
				select string.Concat(new string[] { "'", kvp.Key, "': '", kvp.Value, "'" }));
			codeBuilder.AppendLine("posix_format = { " + text + " }");
			codeBuilder.AppendLine("match = regex.fullmatch(regex_str, x)");
			using (codeBuilder.NewScope("if match:", 1U))
			{
				codeBuilder.AppendLine("fmt_str, val_str = '', ''");
				using (codeBuilder.NewScope("for k, v in match.groupdict().items():", 1U))
				{
					codeBuilder.AppendLine("fmt_str += posix_format[k] + ' '");
					codeBuilder.AppendLine("val_str += v + ' '");
				}
				using (codeBuilder.NewScope("try:", 1U))
				{
					codeBuilder.AppendLine("return datetime.datetime.strptime(val_str, fmt_str)");
				}
				using (codeBuilder.NewScope("except:", 1U))
				{
					codeBuilder.AppendLine("return None");
				}
			}
			using (codeBuilder.NewScope("else:", 1U))
			{
				codeBuilder.AppendLine("return None");
			}
			return new OpaqueGeneratedFunction(list.Select((string x) => Record.Create<string, Type>(x, null)).ToArray<Record<string, Type>>(), null, codeBuilder);
		}

		// Token: 0x0600FF50 RID: 65360 RVA: 0x0036B598 File Offset: 0x00369798
		// Note: this type is marked as 'beforefieldinit'.
		static ReadablePythonTranslatorDateTime()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["M"] = "%-m";
			dictionary["d"] = "%-d";
			dictionary["H"] = "%-H";
			dictionary["h"] = "%-I";
			dictionary["m"] = "%-M";
			dictionary["s"] = "%-S";
			dictionary["j"] = "%-j";
			ReadablePythonTranslatorDateTime._nonStandardFormats = dictionary;
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2["y"] = "year2";
			dictionary2["yy"] = "year2";
			dictionary2["yyy"] = "year";
			dictionary2["yyyy"] = "year";
			dictionary2["yyyyy"] = "year";
			dictionary2["YY"] = "week_year2";
			dictionary2["YYYY"] = "week_year";
			dictionary2["M"] = "month";
			dictionary2["MM"] = "month";
			dictionary2["MMM"] = "month_name";
			dictionary2["MMMM"] = "month_name_full";
			dictionary2["d"] = "day";
			dictionary2["dd"] = "day";
			dictionary2["o"] = "day_ordinal";
			dictionary2["ddd"] = "day_of_week";
			dictionary2["dddd"] = "day_of_week_full";
			dictionary2["f"] = "millisecond";
			dictionary2["ff"] = "millisecond";
			dictionary2["fff"] = "millisecond";
			dictionary2["ffff"] = "millisecond";
			dictionary2["fffff"] = "millisecond";
			dictionary2["ffffff"] = "millisecond";
			dictionary2["fffffff"] = "millisecond";
			dictionary2["H"] = "hour";
			dictionary2["HH"] = "hour";
			dictionary2["h"] = "hour_in_period";
			dictionary2["hh"] = "hour_in_period";
			dictionary2["t"] = "period";
			dictionary2["tt"] = "period";
			dictionary2["m"] = "minute";
			dictionary2["mm"] = "minute";
			dictionary2["s"] = "second";
			dictionary2["ss"] = "second";
			dictionary2["q"] = "quarter";
			dictionary2["j"] = "day_of_year";
			dictionary2["jjj"] = "day_of_year";
			dictionary2["i"] = "day_of_week_in_month";
			dictionary2["V"] = "week_of_year";
			dictionary2["VV"] = "week_of_year";
			dictionary2["Z"] = "time_zone_offset";
			dictionary2["ZZ"] = "time_zone_offset";
			ReadablePythonTranslatorDateTime.BaseFormatToName = dictionary2;
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
			dictionary3["year"] = "%Y";
			dictionary3["week_year"] = "%G";
			dictionary3["month"] = "%m";
			dictionary3["month_name"] = "%b";
			dictionary3["month_name_full"] = "%B";
			dictionary3["day"] = "%d";
			dictionary3["day_of_week"] = "%a";
			dictionary3["day_of_week_full"] = "%A";
			dictionary3["day_ordinal"] = null;
			dictionary3["hour"] = "%H";
			dictionary3["hour_in_period"] = "%I";
			dictionary3["minute"] = "%M";
			dictionary3["second"] = "%S";
			dictionary3["day_of_year"] = "%j";
			dictionary3["week_of_year"] = "%V";
			dictionary3["millisecond"] = null;
			dictionary3["day_of_week_in_month"] = null;
			dictionary3["quarter"] = null;
			dictionary3["period"] = null;
			dictionary3["year2"] = null;
			dictionary3["week_year2"] = null;
			dictionary3["time_zone_offset"] = "%z";
			ReadablePythonTranslatorDateTime.NameToPosix = dictionary3;
		}

		// Token: 0x0600FF51 RID: 65361 RVA: 0x0036BA07 File Offset: 0x00369C07
		[CompilerGenerated]
		internal static SSARValue <FormatDtFormatPart>g__getA|9_0(string a, ref ReadablePythonTranslatorDateTime.<>c__DisplayClass9_0 A_1)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				A_1.dtObj,
				PythonExpressionUtils.MkLiteral(a)
			});
		}

		// Token: 0x0600FF52 RID: 65362 RVA: 0x0036BA26 File Offset: 0x00369C26
		[CompilerGenerated]
		internal static SSARValue <FormatDtFormatPart>g__dotApp|9_1(SSAValue x, string a)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				x,
				PythonExpressionUtils.MkFunApp(PythonExpressionUtils.ObjType, a, Array.Empty<SSAValue>())
			});
		}

		// Token: 0x0600FF53 RID: 65363 RVA: 0x0036BA4A File Offset: 0x00369C4A
		[CompilerGenerated]
		internal static SSARValue <TryTranslateRoundDateTime>g__GetField|13_0(SSAValue dt, string unit)
		{
			return PythonExpressionUtils.Dot(new SSAValue[]
			{
				dt,
				PythonExpressionUtils.MkLiteral(unit)
			});
		}

		// Token: 0x0600FF54 RID: 65364 RVA: 0x0036BA64 File Offset: 0x00369C64
		[CompilerGenerated]
		internal static string <DateTimeFormatToRegex>g__SubRegex|28_0(int min, int max)
		{
			if (min != max)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0},{1}", new object[] { min, max }));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { min }));
		}

		// Token: 0x0600FF55 RID: 65365 RVA: 0x0036BABB File Offset: 0x00369CBB
		[CompilerGenerated]
		internal static string <DateTimeFormatToRegex>g__PartToStr|28_1(string name, string token, DateTimeFormatPart part)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}{{{2}}})", new object[]
			{
				name,
				token,
				ReadablePythonTranslatorDateTime.<DateTimeFormatToRegex>g__SubRegex|28_0(part.MinimumLength, part.MaximumLength)
			}));
		}

		// Token: 0x04005FD0 RID: 24528
		private static Dictionary<string, string> _nonStandardFormats;

		// Token: 0x04005FD1 RID: 24529
		private static readonly IReadOnlyDictionary<string, string> BaseFormatToName;

		// Token: 0x04005FD2 RID: 24530
		private static readonly IReadOnlyDictionary<string, string> NameToPosix;

		// Token: 0x02001DBD RID: 7613
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005FD3 RID: 24531
			public static Func<DateTimeFormatPart, bool> <0>__IsDTFormatPartHourAllowing24Hours;

			// Token: 0x04005FD4 RID: 24532
			public static Func<IGeneratedFunction> <1>__UniqueNonNonePythonFunction;

			// Token: 0x04005FD5 RID: 24533
			public static Func<IGeneratedFunction> <2>__StrptimeOrNonePythonFunction;

			// Token: 0x04005FD6 RID: 24534
			public static Func<IGeneratedFunction> <3>__ParseFormatGenericPythonFunction;
		}
	}
}
