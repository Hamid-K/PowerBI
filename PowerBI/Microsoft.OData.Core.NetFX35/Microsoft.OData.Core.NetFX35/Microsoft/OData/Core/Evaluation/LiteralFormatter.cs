using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Library;
using Microsoft.Spatial;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x0200007A RID: 122
	internal abstract class LiteralFormatter
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00012341 File Offset: 0x00010541
		internal static LiteralFormatter ForConstants
		{
			get
			{
				return LiteralFormatter.DefaultInstance;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00012348 File Offset: 0x00010548
		internal static LiteralFormatter ForConstantsWithoutEncoding
		{
			get
			{
				return LiteralFormatter.DefaultInstanceWithoutEncoding;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001234F File Offset: 0x0001054F
		internal static LiteralFormatter ForKeys(bool keysAsSegment)
		{
			if (!keysAsSegment)
			{
				return LiteralFormatter.DefaultInstance;
			}
			return LiteralFormatter.KeyAsSegmentInstance;
		}

		// Token: 0x060004E1 RID: 1249
		internal abstract string Format(object value);

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001235F File Offset: 0x0001055F
		[SuppressMessage("DataWeb.Usage", "AC0018:SystemUriEscapeDataStringRule", Justification = "Values are correctly being escaped before the literal delimiters are added.")]
		protected virtual string EscapeResultForUri(string result)
		{
			return Uri.EscapeDataString(result);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00012367 File Offset: 0x00010567
		[SuppressMessage("DataWeb.Usage", "AC0018:SystemUriEscapeDataStringRule", Justification = "Usage is in Debug.Assert only")]
		private static string ConvertByteArrayToKeyString(byte[] byteArray)
		{
			return Convert.ToBase64String(byteArray, 0, byteArray.Length);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00012374 File Offset: 0x00010574
		private static string FormatRawLiteral(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			if (value is bool)
			{
				return XmlConvert.ToString((bool)value);
			}
			if (value is byte)
			{
				return XmlConvert.ToString((byte)value);
			}
			if (value is decimal)
			{
				return XmlConvert.ToString((decimal)value);
			}
			if (value is double)
			{
				string text2 = XmlConvert.ToString((double)value);
				return LiteralFormatter.SharedUtils.AppendDecimalMarkerToDouble(text2);
			}
			if (value is Guid)
			{
				return value.ToString();
			}
			if (value is short)
			{
				return XmlConvert.ToString((short)value);
			}
			if (value is int)
			{
				return XmlConvert.ToString((int)value);
			}
			if (value is long)
			{
				return XmlConvert.ToString((long)value);
			}
			if (value is sbyte)
			{
				return XmlConvert.ToString((sbyte)value);
			}
			if (value is float)
			{
				return XmlConvert.ToString((float)value);
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				return LiteralFormatter.ConvertByteArrayToKeyString(array);
			}
			if (value is Date)
			{
				return value.ToString();
			}
			if (value is DateTimeOffset)
			{
				return XmlConvert.ToString((DateTimeOffset)value);
			}
			if (value is TimeOfDay)
			{
				return value.ToString();
			}
			if (value is TimeSpan)
			{
				return EdmValueWriter.DurationAsXml((TimeSpan)value);
			}
			Geography geography = value as Geography;
			if (geography != null)
			{
				return WellKnownTextSqlFormatter.Create(true).Write(geography);
			}
			Geometry geometry = value as Geometry;
			if (geometry != null)
			{
				return WellKnownTextSqlFormatter.Create(true).Write(geometry);
			}
			throw LiteralFormatter.SharedUtils.CreateExceptionForUnconvertableType(value);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000124E8 File Offset: 0x000106E8
		private string FormatAndEscapeLiteral(object value)
		{
			string text = LiteralFormatter.FormatRawLiteral(value);
			if (value is string)
			{
				text = text.Replace("'", "''");
			}
			return this.EscapeResultForUri(text);
		}

		// Token: 0x04000223 RID: 547
		private static readonly LiteralFormatter DefaultInstance = new LiteralFormatter.DefaultLiteralFormatter();

		// Token: 0x04000224 RID: 548
		private static readonly LiteralFormatter DefaultInstanceWithoutEncoding = new LiteralFormatter.DefaultLiteralFormatter(true);

		// Token: 0x04000225 RID: 549
		private static readonly LiteralFormatter KeyAsSegmentInstance = new LiteralFormatter.KeysAsSegmentsLiteralFormatter();

		// Token: 0x0200007B RID: 123
		private static class SharedUtils
		{
			// Token: 0x060004E8 RID: 1256 RVA: 0x00012545 File Offset: 0x00010745
			internal static InvalidOperationException CreateExceptionForUnconvertableType(object value)
			{
				return new ODataException(Strings.ODataUriUtils_ConvertToUriLiteralUnsupportedType(value.GetType().ToString()));
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x0001255C File Offset: 0x0001075C
			internal static bool TryConvertToStandardType(object value, out object converted)
			{
				byte[] array;
				if (LiteralFormatter.SharedUtils.TryGetByteArrayFromBinary(value, out array))
				{
					converted = array;
					return true;
				}
				converted = null;
				return false;
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x0001257C File Offset: 0x0001077C
			internal static string AppendDecimalMarkerToDouble(string input)
			{
				IEnumerable<char> enumerable = input.ToCharArray();
				if (input.get_Chars(0) == '-')
				{
					enumerable = Enumerable.Skip<char>(enumerable, 1);
				}
				if (Enumerable.All<char>(enumerable, new Func<char, bool>(char.IsDigit)))
				{
					return input + ".0";
				}
				return input;
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x000125C4 File Offset: 0x000107C4
			[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Method is compiled into 3 assemblies, and the parameter is used in 2 of them.")]
			private static bool TryGetByteArrayFromBinary(object value, out byte[] array)
			{
				array = null;
				return false;
			}
		}

		// Token: 0x0200007C RID: 124
		private sealed class DefaultLiteralFormatter : LiteralFormatter
		{
			// Token: 0x060004EC RID: 1260 RVA: 0x000125CA File Offset: 0x000107CA
			internal DefaultLiteralFormatter()
				: this(false)
			{
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x000125D3 File Offset: 0x000107D3
			internal DefaultLiteralFormatter(bool disableUrlEncoding)
			{
				this.disableUrlEncoding = disableUrlEncoding;
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x000125E4 File Offset: 0x000107E4
			internal override string Format(object value)
			{
				object obj;
				if (LiteralFormatter.SharedUtils.TryConvertToStandardType(value, out obj))
				{
					value = obj;
				}
				return this.FormatLiteralWithTypePrefix(value);
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x00012605 File Offset: 0x00010805
			protected override string EscapeResultForUri(string result)
			{
				if (!this.disableUrlEncoding)
				{
					result = base.EscapeResultForUri(result);
				}
				return result;
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x0001261C File Offset: 0x0001081C
			private string FormatLiteralWithTypePrefix(object value)
			{
				string text = base.FormatAndEscapeLiteral(value);
				if (value is byte[])
				{
					return "binary'" + text + "'";
				}
				if (value is Geography)
				{
					return "geography'" + text + "'";
				}
				if (value is Geometry)
				{
					return "geometry'" + text + "'";
				}
				if (value is TimeSpan)
				{
					return "duration'" + text + "'";
				}
				if (value is string)
				{
					return "'" + text + "'";
				}
				return text;
			}

			// Token: 0x04000226 RID: 550
			private readonly bool disableUrlEncoding;
		}

		// Token: 0x0200007D RID: 125
		private sealed class KeysAsSegmentsLiteralFormatter : LiteralFormatter
		{
			// Token: 0x060004F1 RID: 1265 RVA: 0x000126AF File Offset: 0x000108AF
			internal KeysAsSegmentsLiteralFormatter()
			{
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x000126B8 File Offset: 0x000108B8
			internal override string Format(object value)
			{
				object obj;
				if (LiteralFormatter.SharedUtils.TryConvertToStandardType(value, out obj))
				{
					value = obj;
				}
				string text = value as string;
				if (text != null)
				{
					value = LiteralFormatter.KeysAsSegmentsLiteralFormatter.EscapeLeadingDollarSign(text);
				}
				return base.FormatAndEscapeLiteral(value);
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x000126EB File Offset: 0x000108EB
			private static string EscapeLeadingDollarSign(string stringValue)
			{
				if (stringValue.Length > 0 && stringValue.get_Chars(0) == '$')
				{
					stringValue = '$' + stringValue;
				}
				return stringValue;
			}
		}
	}
}
