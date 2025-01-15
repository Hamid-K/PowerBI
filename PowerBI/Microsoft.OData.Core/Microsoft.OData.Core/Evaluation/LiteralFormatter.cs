using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.Spatial;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200025F RID: 607
	internal abstract class LiteralFormatter
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x000543F2 File Offset: 0x000525F2
		internal static LiteralFormatter ForConstants
		{
			get
			{
				return LiteralFormatter.DefaultInstance;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x000543F9 File Offset: 0x000525F9
		internal static LiteralFormatter ForConstantsWithoutEncoding
		{
			get
			{
				return LiteralFormatter.DefaultInstanceWithoutEncoding;
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00054400 File Offset: 0x00052600
		internal static LiteralFormatter ForKeys(bool keysAsSegment)
		{
			if (!keysAsSegment)
			{
				return LiteralFormatter.DefaultInstance;
			}
			return LiteralFormatter.KeyAsSegmentInstance;
		}

		// Token: 0x06001B55 RID: 6997
		internal abstract string Format(object value);

		// Token: 0x06001B56 RID: 6998 RVA: 0x00054410 File Offset: 0x00052610
		protected virtual string EscapeResultForUri(string result)
		{
			return Uri.EscapeDataString(result);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00054418 File Offset: 0x00052618
		private static string ConvertByteArrayToKeyString(byte[] byteArray)
		{
			return Convert.ToBase64String(byteArray, 0, byteArray.Length);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00054424 File Offset: 0x00052624
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
				return XmlConvert.ToString((short)((byte)value));
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
				return FormatterExtensions.Write(WellKnownTextSqlFormatter.Create(true), geography);
			}
			Geometry geometry = value as Geometry;
			if (geometry != null)
			{
				return FormatterExtensions.Write(WellKnownTextSqlFormatter.Create(true), geometry);
			}
			ODataEnumValue odataEnumValue = value as ODataEnumValue;
			if (odataEnumValue != null)
			{
				return odataEnumValue.Value;
			}
			throw LiteralFormatter.SharedUtils.CreateExceptionForUnconvertableType(value);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000545AC File Offset: 0x000527AC
		private string FormatAndEscapeLiteral(object value)
		{
			string text = LiteralFormatter.FormatRawLiteral(value);
			if (value is string)
			{
				text = text.Replace("'", "''");
			}
			return this.EscapeResultForUri(text);
		}

		// Token: 0x04000B78 RID: 2936
		private static readonly LiteralFormatter DefaultInstance = new LiteralFormatter.DefaultLiteralFormatter();

		// Token: 0x04000B79 RID: 2937
		private static readonly LiteralFormatter DefaultInstanceWithoutEncoding = new LiteralFormatter.DefaultLiteralFormatter(true);

		// Token: 0x04000B7A RID: 2938
		private static readonly LiteralFormatter KeyAsSegmentInstance = new LiteralFormatter.KeysAsSegmentsLiteralFormatter();

		// Token: 0x0200044F RID: 1103
		private static class SharedUtils
		{
			// Token: 0x060021F6 RID: 8694 RVA: 0x0005E729 File Offset: 0x0005C929
			internal static InvalidOperationException CreateExceptionForUnconvertableType(object value)
			{
				return new ODataException(Strings.ODataUriUtils_ConvertToUriLiteralUnsupportedType(value.GetType().ToString()));
			}

			// Token: 0x060021F7 RID: 8695 RVA: 0x0005E740 File Offset: 0x0005C940
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

			// Token: 0x060021F8 RID: 8696 RVA: 0x0005E760 File Offset: 0x0005C960
			internal static string AppendDecimalMarkerToDouble(string input)
			{
				IEnumerable<char> enumerable = input.ToCharArray();
				if (input[0] == '-')
				{
					enumerable = enumerable.Skip(1);
				}
				if (enumerable.All(new Func<char, bool>(char.IsDigit)))
				{
					return input + ".0";
				}
				return input;
			}

			// Token: 0x060021F9 RID: 8697 RVA: 0x0005E7A8 File Offset: 0x0005C9A8
			[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Method is compiled into 3 assemblies, and the parameter is used in 2 of them.")]
			private static bool TryGetByteArrayFromBinary(object value, out byte[] array)
			{
				array = null;
				return false;
			}
		}

		// Token: 0x02000450 RID: 1104
		private sealed class DefaultLiteralFormatter : LiteralFormatter
		{
			// Token: 0x060021FA RID: 8698 RVA: 0x0005E7AE File Offset: 0x0005C9AE
			internal DefaultLiteralFormatter()
				: this(false)
			{
			}

			// Token: 0x060021FB RID: 8699 RVA: 0x0005E7B7 File Offset: 0x0005C9B7
			internal DefaultLiteralFormatter(bool disableUrlEncoding)
			{
				this.disableUrlEncoding = disableUrlEncoding;
			}

			// Token: 0x060021FC RID: 8700 RVA: 0x0005E7C8 File Offset: 0x0005C9C8
			internal override string Format(object value)
			{
				object obj;
				if (LiteralFormatter.SharedUtils.TryConvertToStandardType(value, out obj))
				{
					value = obj;
				}
				return this.FormatLiteralWithTypePrefix(value);
			}

			// Token: 0x060021FD RID: 8701 RVA: 0x0005E7E9 File Offset: 0x0005C9E9
			protected override string EscapeResultForUri(string result)
			{
				if (!this.disableUrlEncoding)
				{
					result = base.EscapeResultForUri(result);
				}
				return result;
			}

			// Token: 0x060021FE RID: 8702 RVA: 0x0005E800 File Offset: 0x0005CA00
			private string FormatLiteralWithTypePrefix(object value)
			{
				ODataEnumValue odataEnumValue = value as ODataEnumValue;
				if (odataEnumValue != null)
				{
					if (string.IsNullOrEmpty(odataEnumValue.TypeName))
					{
						throw new ODataException("Type name should not be null or empty when serializing an Enum value for URI key.");
					}
					return odataEnumValue.TypeName + "'" + base.FormatAndEscapeLiteral(odataEnumValue.Value) + "'";
				}
				else
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
			}

			// Token: 0x0400107E RID: 4222
			private readonly bool disableUrlEncoding;
		}

		// Token: 0x02000451 RID: 1105
		private sealed class KeysAsSegmentsLiteralFormatter : LiteralFormatter
		{
			// Token: 0x060021FF RID: 8703 RVA: 0x0005E8D7 File Offset: 0x0005CAD7
			internal KeysAsSegmentsLiteralFormatter()
			{
			}

			// Token: 0x06002200 RID: 8704 RVA: 0x0005E8E0 File Offset: 0x0005CAE0
			internal override string Format(object value)
			{
				ODataEnumValue odataEnumValue = value as ODataEnumValue;
				if (odataEnumValue != null)
				{
					value = odataEnumValue.Value;
				}
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

			// Token: 0x06002201 RID: 8705 RVA: 0x0005E925 File Offset: 0x0005CB25
			private static string EscapeLeadingDollarSign(string stringValue)
			{
				if (stringValue.Length > 0 && stringValue[0] == '$')
				{
					stringValue = "$" + stringValue;
				}
				return stringValue;
			}
		}
	}
}
