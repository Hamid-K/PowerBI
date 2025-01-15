using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.InfoNav;
using Newtonsoft.Json;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace Microsoft.Lucia.Yaml
{
	// Token: 0x02000020 RID: 32
	internal static class YamlCoreSchema
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002BA4 File Offset: 0x00000DA4
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Type", "Value" })]
		internal static global::System.ValueTuple<JsonToken, object> ParseValueToJson(Scalar scalar)
		{
			string tagForScalar = YamlCoreSchema.GetTagForScalar(scalar);
			if (tagForScalar == "tag:yaml.org,2002:null")
			{
				return new global::System.ValueTuple<JsonToken, object>(JsonToken.Null, null);
			}
			if (tagForScalar == "tag:yaml.org,2002:bool")
			{
				return new global::System.ValueTuple<JsonToken, object>(JsonToken.Boolean, YamlCoreSchema.ParseBool(scalar.Value));
			}
			if (tagForScalar == "tag:yaml.org,2002:int")
			{
				return new global::System.ValueTuple<JsonToken, object>(JsonToken.Integer, YamlCoreSchema.ParseInt(scalar.Value));
			}
			if (!(tagForScalar == "tag:yaml.org,2002:float"))
			{
				if (!(tagForScalar == "tag:yaml.org,2002:str"))
				{
				}
				return new global::System.ValueTuple<JsonToken, object>(JsonToken.String, scalar.Value);
			}
			return new global::System.ValueTuple<JsonToken, object>(JsonToken.Float, YamlCoreSchema.ParseFloat(scalar.Value));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002C57 File Offset: 0x00000E57
		internal static void Emit(this IEmitter emitter, double value)
		{
			YamlCoreSchema.EmitPlainScalar(emitter, "tag:yaml.org,2002:float", JsonConvert.ToString(value));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002C6A File Offset: 0x00000E6A
		internal static void Emit(this IEmitter emitter, float value)
		{
			YamlCoreSchema.EmitPlainScalar(emitter, "tag:yaml.org,2002:float", JsonConvert.ToString(value));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002C7D File Offset: 0x00000E7D
		internal static void EmitNull(this IEmitter emitter)
		{
			YamlCoreSchema.EmitPlainScalar(emitter, "tag:yaml.org,2002:null", JsonConvert.ToString(null));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002C90 File Offset: 0x00000E90
		private static void EmitPlainScalar(IEmitter emitter, string tag, string value)
		{
			emitter.Emit(new Scalar(null, tag, value, 1, true, false));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002CA3 File Offset: 0x00000EA3
		private static string GetTagForScalar(Scalar scalar)
		{
			if (!string.IsNullOrEmpty(scalar.Tag))
			{
				return scalar.Tag;
			}
			if (scalar.Style == 1)
			{
				return YamlCoreSchema.GetTagForPlainScalar(scalar.Value);
			}
			return "tag:yaml.org,2002:str";
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002CD4 File Offset: 0x00000ED4
		private static string GetTagForPlainScalar(string value)
		{
			if (value.Length == 0 || YamlCoreSchema._nullRegex.IsMatch(value))
			{
				return "tag:yaml.org,2002:null";
			}
			if (YamlCoreSchema._boolRegex.IsMatch(value))
			{
				return "tag:yaml.org,2002:bool";
			}
			if (YamlCoreSchema._intRegex.IsMatch(value))
			{
				return "tag:yaml.org,2002:int";
			}
			if (YamlCoreSchema._floatRegex.IsMatch(value))
			{
				return "tag:yaml.org,2002:float";
			}
			return "tag:yaml.org,2002:str";
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002D3C File Offset: 0x00000F3C
		private static bool ParseBool(string value)
		{
			bool flag;
			if (bool.TryParse(value, out flag))
			{
				return flag;
			}
			throw YamlCoreSchema.CreateFormatException("bool", value);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002D60 File Offset: 0x00000F60
		private static long ParseInt(string value)
		{
			NumberStyles numberStyles;
			if (value.StartsWith("0x", StringComparison.Ordinal))
			{
				value = value.Substring(2);
				numberStyles = NumberStyles.HexNumber;
			}
			else
			{
				numberStyles = NumberStyles.Integer;
			}
			long num;
			if (long.TryParse(value, numberStyles, YamlCoreSchema._numberFormatInfo, out num))
			{
				return num;
			}
			throw YamlCoreSchema.CreateFormatException("int", value);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002DAC File Offset: 0x00000FAC
		private static double ParseFloat(string value)
		{
			double num;
			if (double.TryParse(value, NumberStyles.Float, YamlCoreSchema._numberFormatInfo, out num))
			{
				return num;
			}
			if (value.StartsWith("+", StringComparison.Ordinal))
			{
				value = value.Substring(1);
			}
			if (double.TryParse(value.ToLowerInvariant(), NumberStyles.Float, YamlCoreSchema._numberFormatInfo, out num))
			{
				return num;
			}
			throw YamlCoreSchema.CreateFormatException("float", value);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002E0C File Offset: 0x0000100C
		private static Exception CreateFormatException(string type, string value)
		{
			throw new FormatException(StringUtil.FormatInvariant("The value '{0}' is not valid for the specified type '{1}'.", value, type));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002E1F File Offset: 0x0000101F
		private static Regex CreateRegex(params string[] patterns)
		{
			return new Regex((patterns.Length > 1) ? ("(^" + patterns.StringJoin("$)|(^") + "$)") : ("^" + patterns[0] + "$"), RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
		}

		// Token: 0x0400003E RID: 62
		private const string StrTag = "tag:yaml.org,2002:str";

		// Token: 0x0400003F RID: 63
		private const string NullTag = "tag:yaml.org,2002:null";

		// Token: 0x04000040 RID: 64
		private const string BoolTag = "tag:yaml.org,2002:bool";

		// Token: 0x04000041 RID: 65
		private const string IntTag = "tag:yaml.org,2002:int";

		// Token: 0x04000042 RID: 66
		private const string FloatTag = "tag:yaml.org,2002:float";

		// Token: 0x04000043 RID: 67
		private static readonly Regex _nullRegex = YamlCoreSchema.CreateRegex(new string[] { "(null | Null | NULL | ~)" });

		// Token: 0x04000044 RID: 68
		private static readonly Regex _boolRegex = YamlCoreSchema.CreateRegex(new string[] { "(true | True | TRUE | false | False | FALSE)" });

		// Token: 0x04000045 RID: 69
		private static readonly Regex _intRegex = YamlCoreSchema.CreateRegex(new string[] { "[-+]? [0-9]+", "0x [0-9a-fA-F]+" });

		// Token: 0x04000046 RID: 70
		private static readonly Regex _floatRegex = YamlCoreSchema.CreateRegex(new string[] { "[-+]? ( \\. [0-9]+ | [0-9]+ ( \\. [0-9]* )? ) ( [eE] [-+]? [0-9]+ )?", "[-+]? ( \\.inf | \\.Inf | \\.INF )", "(\\.nan | \\.NaN | \\.NAN)" });

		// Token: 0x04000047 RID: 71
		private static readonly NumberFormatInfo _numberFormatInfo = new NumberFormatInfo
		{
			NumberDecimalSeparator = ".",
			PositiveInfinitySymbol = ".inf",
			NegativeInfinitySymbol = "-.inf",
			NaNSymbol = ".nan"
		};
	}
}
