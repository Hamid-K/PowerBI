using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B0F RID: 2831
	internal static class RichDataTypeExtensions
	{
		// Token: 0x060046AA RID: 18090 RVA: 0x000DCB6C File Offset: 0x000DAD6C
		public static string HumanReadableTypeName(this IRichDataType dataType)
		{
			switch (dataType.Kind)
			{
			case DataKind.Numeric:
			{
				RichNumericType richNumericType = (RichNumericType)dataType;
				if (richNumericType.ContainsIntegerSubtype)
				{
					return "Integer";
				}
				if (richNumericType.ContainsRealSubtype)
				{
					return "Float";
				}
				throw new NotImplementedException("Encountered a numeric type that's neither integer nor real.");
			}
			case DataKind.Date:
				return "Date";
			case DataKind.Time:
				return "Time";
			case DataKind.DateTime:
				return "DateTime";
			case DataKind.Boolean:
				return "Boolean";
			case DataKind.String:
				return "String";
			}
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported data kind: {0}.", new object[] { dataType.Kind })));
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x000DCC18 File Offset: 0x000DAE18
		public static string PostprocessDateParseExpression(this RichDateType dateType, string parseExpression)
		{
			if (dateType.HasDate && !dateType.HasTime)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.date()", new object[] { parseExpression }));
			}
			if (dateType.HasTime && !dateType.HasDate)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}.time()", new object[] { parseExpression }));
			}
			if (dateType.HasTime && dateType.HasDate)
			{
				return parseExpression;
			}
			throw new InvalidOperationException("Unreachable code.");
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x000DCC98 File Offset: 0x000DAE98
		public static string PysparkTypeName(this IRichDataType dataType)
		{
			switch (dataType.Kind)
			{
			case DataKind.Numeric:
			{
				RichNumericType richNumericType = (RichNumericType)dataType;
				if (richNumericType.ContainsRealSubtype)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("DoubleType()", Array.Empty<object>()));
				}
				if (richNumericType.MinValue == Optional<BigInteger>.Nothing && richNumericType.MaxValue == Optional<BigInteger>.Nothing)
				{
					if (richNumericType.ContainsNonUniformPrecisionAndScale)
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("DecimalType(38, 18)", Array.Empty<object>()));
					}
					return FormattableString.Invariant(FormattableStringFactory.Create("DecimalType({0}, {1})", new object[]
					{
						richNumericType.Precision.Value,
						richNumericType.Scale.Value
					}));
				}
				else
				{
					BigInteger value = richNumericType.MinValue.Value;
					BigInteger value2 = richNumericType.MaxValue.Value;
					if (value >= -32768L && value2 <= 32767L)
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("ShortType()", Array.Empty<object>()));
					}
					if (value >= -2147483648L && value2 <= 2147483647L)
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("IntegerType()", Array.Empty<object>()));
					}
					if (value >= -9223372036854775808L && value2 <= 9223372036854775807L)
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("LongType()", Array.Empty<object>()));
					}
					return FormattableString.Invariant(FormattableStringFactory.Create("DecimalType(38, 18)", Array.Empty<object>()));
				}
				break;
			}
			case DataKind.Date:
			case DataKind.Time:
			case DataKind.DateTime:
				return FormattableString.Invariant(FormattableStringFactory.Create("TimestampType()", Array.Empty<object>()));
			case DataKind.Boolean:
				return FormattableString.Invariant(FormattableStringFactory.Create("BooleanType()", Array.Empty<object>()));
			default:
				return FormattableString.Invariant(FormattableStringFactory.Create("StringType()", Array.Empty<object>()));
			}
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x000DCE80 File Offset: 0x000DB080
		public static bool CanBeParsedByDefaultParser(this SyntacticNumericType syntacticType)
		{
			return !syntacticType.IsNaValue && (!syntacticType.Substitutions.Any<KeyValuePair<string, string>>() || (syntacticType.IsNegated && syntacticType.Substitutions.Count == 1 && syntacticType.Substitutions.Single<KeyValuePair<string, string>>().Key == "-" && syntacticType.HasLeadingSign));
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000DCEE6 File Offset: 0x000DB0E6
		public static string GetFormatStringForCluster(this IEnumerable<SyntacticDateType> dateCluster)
		{
			return dateCluster.FirstOrDefault((SyntacticDateType st) => st.Format.PosixParsingFormatString != null).Format.PosixParsingFormatString;
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000DCF17 File Offset: 0x000DB117
		private static bool RequiresOutOfLineParser(RichNumericType numericType)
		{
			if (numericType.PickOneInterpretation.Count<SyntacticNumericType>() > 1)
			{
				return numericType.PickOneInterpretation.Any((SyntacticNumericType st) => !st.CanBeParsedByDefaultParser());
			}
			return false;
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000DCF54 File Offset: 0x000DB154
		private static bool RequiresOutOfLineParser(RichDateType dateType)
		{
			List<IEnumerable<SyntacticDateType>> list = dateType.SyntacticClusters.Where((IEnumerable<SyntacticDateType> st) => st.All((SyntacticDateType t) => !t.IsNaValue)).ToList<IEnumerable<SyntacticDateType>>();
			IEnumerable<IEnumerable<SyntacticDateType>> enumerable = dateType.SyntacticClusters.Where((IEnumerable<SyntacticDateType> st) => st.Any((SyntacticDateType t) => t.IsNaValue));
			bool flag = list.Any((IEnumerable<SyntacticDateType> cluster) => cluster.All((SyntacticDateType st) => string.IsNullOrEmpty(st.Format.PosixParsingFormatString)));
			return ((IReadOnlyCollection<IEnumerable<SyntacticDateType>>)list).Count > 1 || enumerable.Any<IEnumerable<SyntacticDateType>>() || flag;
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x000DCFF4 File Offset: 0x000DB1F4
		private static bool RequiresOutOfLineParser(RichBooleanType booleanType)
		{
			return booleanType.ExampleTrueValues.Count<string>() > 1 || booleanType.ExampleFalseValues.Count<string>() > 1 || booleanType.NaValueCount > 0;
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000DD020 File Offset: 0x000DB220
		private static bool RequiresOutOfLineParser(this IRichDataType dataType)
		{
			if (dataType == null)
			{
				return false;
			}
			if (dataType is RichStringType)
			{
				return false;
			}
			if (dataType.EmptyStringsExpectedInData || dataType.NormalizableStringsExpectedInData || dataType.NullsExpectedInData)
			{
				return true;
			}
			RichNumericType richNumericType = dataType as RichNumericType;
			if (richNumericType != null)
			{
				return RichDataTypeExtensions.RequiresOutOfLineParser(richNumericType);
			}
			RichDateType richDateType = dataType as RichDateType;
			if (richDateType != null)
			{
				return RichDataTypeExtensions.RequiresOutOfLineParser(richDateType);
			}
			RichBooleanType richBooleanType = dataType as RichBooleanType;
			return richBooleanType == null || RichDataTypeExtensions.RequiresOutOfLineParser(richBooleanType);
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x000DD08C File Offset: 0x000DB28C
		public static bool RequiresOutOfLineParser(this IRichDataType dataType, IPythonColumnInfo columnInfo, CodeGenerationMode codeGenerationMode)
		{
			if (codeGenerationMode == CodeGenerationMode.PysparkDataFrame)
			{
				RichDateType richDateType = dataType as RichDateType;
				if (richDateType != null && richDateType.HasTime && !richDateType.HasDate)
				{
					return true;
				}
			}
			if (dataType is RichStringType)
			{
				return false;
			}
			RichNumericType richNumericType = dataType as RichNumericType;
			if (richNumericType == null)
			{
				return dataType.RequiresOutOfLineParser();
			}
			if (!richNumericType.RequiresOutOfLineParser())
			{
				return false;
			}
			if (!columnInfo.FixPandasNaNBug)
			{
				return true;
			}
			IReadOnlyList<SyntacticNumericType> readOnlyList = richNumericType.PickOneInterpretation.ToList<SyntacticNumericType>();
			if (readOnlyList.Count((SyntacticNumericType st) => st.IsNaN) <= 1)
			{
				return readOnlyList.Where((SyntacticNumericType st) => !st.IsNaN).Any((SyntacticNumericType st) => !st.CanBeParsedByDefaultParser());
			}
			return true;
		}
	}
}
