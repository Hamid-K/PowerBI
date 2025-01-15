using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.Spatial;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000224 RID: 548
	internal abstract class LiteralFormatter
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x0004481E File Offset: 0x00042A1E
		internal static LiteralFormatter ForConstants
		{
			get
			{
				return LiteralFormatter.DefaultInstance;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x00044825 File Offset: 0x00042A25
		internal static LiteralFormatter ForConstantsWithoutEncoding
		{
			get
			{
				return LiteralFormatter.DefaultInstanceWithoutEncoding;
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0004482C File Offset: 0x00042A2C
		internal static LiteralFormatter ForKeys(bool keysAsSegment)
		{
			if (!keysAsSegment)
			{
				return LiteralFormatter.DefaultInstance;
			}
			return LiteralFormatter.KeyAsSegmentInstance;
		}

		// Token: 0x06001634 RID: 5684
		internal abstract string Format(object value);

		// Token: 0x06001635 RID: 5685 RVA: 0x0004483C File Offset: 0x00042A3C
		protected virtual string EscapeResultForUri(string result)
		{
			return Uri.EscapeDataString(result);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00044844 File Offset: 0x00042A44
		private static string ConvertByteArrayToKeyString(byte[] byteArray)
		{
			return Convert.ToBase64String(byteArray, 0, byteArray.Length);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00044850 File Offset: 0x00042A50
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
			ODataEnumValue odataEnumValue = value as ODataEnumValue;
			if (odataEnumValue != null)
			{
				return odataEnumValue.Value;
			}
			throw LiteralFormatter.SharedUtils.CreateExceptionForUnconvertableType(value);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000449D8 File Offset: 0x00042BD8
		private string FormatAndEscapeLiteral(object value)
		{
			string text = LiteralFormatter.FormatRawLiteral(value);
			if (value is string)
			{
				text = text.Replace("'", "''");
			}
			return this.EscapeResultForUri(text);
		}

		// Token: 0x04000A4D RID: 2637
		private static readonly LiteralFormatter DefaultInstance = new LiteralFormatter.DefaultLiteralFormatter();

		// Token: 0x04000A4E RID: 2638
		private static readonly LiteralFormatter DefaultInstanceWithoutEncoding = new LiteralFormatter.DefaultLiteralFormatter(true);

		// Token: 0x04000A4F RID: 2639
		private static readonly LiteralFormatter KeyAsSegmentInstance = new LiteralFormatter.KeysAsSegmentsLiteralFormatter();

		// Token: 0x02000368 RID: 872
		private static class SharedUtils
		{
			// Token: 0x06001B4E RID: 6990 RVA: 0x0004D024 File Offset: 0x0004B224
			internal static InvalidOperationException CreateExceptionForUnconvertableType(object value)
			{
				return new ODataException(Strings.ODataUriUtils_ConvertToUriLiteralUnsupportedType(value.GetType().ToString()));
			}

			// Token: 0x06001B4F RID: 6991 RVA: 0x0004D03C File Offset: 0x0004B23C
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

			// Token: 0x06001B50 RID: 6992 RVA: 0x0004D05C File Offset: 0x0004B25C
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

			// Token: 0x06001B51 RID: 6993 RVA: 0x0004D0A4 File Offset: 0x0004B2A4
			[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Method is compiled into 3 assemblies, and the parameter is used in 2 of them.")]
			private static bool TryGetByteArrayFromBinary(object value, out byte[] array)
			{
				array = null;
				return false;
			}
		}

		// Token: 0x02000369 RID: 873
		private sealed class DefaultLiteralFormatter : LiteralFormatter
		{
			// Token: 0x06001B52 RID: 6994 RVA: 0x0004D0AA File Offset: 0x0004B2AA
			internal DefaultLiteralFormatter()
				: this(false)
			{
			}

			// Token: 0x06001B53 RID: 6995 RVA: 0x0004D0B3 File Offset: 0x0004B2B3
			internal DefaultLiteralFormatter(bool disableUrlEncoding)
			{
				this.disableUrlEncoding = disableUrlEncoding;
			}

			// Token: 0x06001B54 RID: 6996 RVA: 0x0004D0C4 File Offset: 0x0004B2C4
			internal override string Format(object value)
			{
				object obj;
				if (LiteralFormatter.SharedUtils.TryConvertToStandardType(value, out obj))
				{
					value = obj;
				}
				return this.FormatLiteralWithTypePrefix(value);
			}

			// Token: 0x06001B55 RID: 6997 RVA: 0x0004D0E5 File Offset: 0x0004B2E5
			protected override string EscapeResultForUri(string result)
			{
				if (!this.disableUrlEncoding)
				{
					result = base.EscapeResultForUri(result);
				}
				return result;
			}

			// Token: 0x06001B56 RID: 6998 RVA: 0x0004D0FC File Offset: 0x0004B2FC
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

			// Token: 0x04000DC1 RID: 3521
			private readonly bool disableUrlEncoding;
		}

		// Token: 0x0200036A RID: 874
		private sealed class KeysAsSegmentsLiteralFormatter : LiteralFormatter
		{
			// Token: 0x06001B57 RID: 6999 RVA: 0x0004D1D3 File Offset: 0x0004B3D3
			internal KeysAsSegmentsLiteralFormatter()
			{
			}

			// Token: 0x06001B58 RID: 7000 RVA: 0x0004D1DC File Offset: 0x0004B3DC
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

			// Token: 0x06001B59 RID: 7001 RVA: 0x0004D221 File Offset: 0x0004B421
			private static string EscapeLeadingDollarSign(string stringValue)
			{
				if (stringValue.Length > 0 && stringValue.get_Chars(0) == '$')
				{
					stringValue = "$" + stringValue;
				}
				return stringValue;
			}
		}
	}
}
