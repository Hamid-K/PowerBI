using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.OData.Edm;
using Microsoft.Spatial;

namespace Microsoft.OData.Client
{
	// Token: 0x0200000D RID: 13
	internal abstract class LiteralFormatter
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000358E File Offset: 0x0000178E
		internal static LiteralFormatter ForConstants
		{
			get
			{
				return LiteralFormatter.DefaultInstance;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003595 File Offset: 0x00001795
		internal static LiteralFormatter ForKeys(bool keysAsSegment)
		{
			if (!keysAsSegment)
			{
				return LiteralFormatter.DefaultInstance;
			}
			return LiteralFormatter.KeyAsSegmentInstance;
		}

		// Token: 0x06000056 RID: 86
		internal abstract string Format(object value);

		// Token: 0x06000057 RID: 87 RVA: 0x000035A5 File Offset: 0x000017A5
		protected virtual string EscapeResultForUri(string result)
		{
			return Uri.EscapeDataString(result);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000035AD File Offset: 0x000017AD
		private static string ConvertByteArrayToKeyString(byte[] byteArray)
		{
			return Convert.ToBase64String(byteArray, 0, byteArray.Length);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000035BC File Offset: 0x000017BC
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
			if (value is DateTime)
			{
				DateTimeOffset dateTimeOffset = PlatformHelper.ConvertDateTimeToDateTimeOffset((DateTime)value);
				return XmlConvert.ToString(dateTimeOffset);
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

		// Token: 0x0600005A RID: 90 RVA: 0x00003764 File Offset: 0x00001964
		private string FormatAndEscapeLiteral(object value)
		{
			string text = LiteralFormatter.FormatRawLiteral(value);
			if (value is string)
			{
				text = text.Replace("'", "''");
			}
			return this.EscapeResultForUri(text);
		}

		// Token: 0x04000025 RID: 37
		private static readonly LiteralFormatter DefaultInstance = new LiteralFormatter.DefaultLiteralFormatter();

		// Token: 0x04000026 RID: 38
		private static readonly LiteralFormatter KeyAsSegmentInstance = new LiteralFormatter.KeysAsSegmentsLiteralFormatter();

		// Token: 0x02000141 RID: 321
		private static class SharedUtils
		{
			// Token: 0x06000CD8 RID: 3288 RVA: 0x0002D5FD File Offset: 0x0002B7FD
			internal static InvalidOperationException CreateExceptionForUnconvertableType(object value)
			{
				return Error.InvalidOperation(Strings.Context_CannotConvertKey(value));
			}

			// Token: 0x06000CD9 RID: 3289 RVA: 0x0002D60C File Offset: 0x0002B80C
			internal static bool TryConvertToStandardType(object value, out object converted)
			{
				byte[] array;
				if (LiteralFormatter.SharedUtils.TryGetByteArrayFromBinary(value, out array))
				{
					converted = array;
					return true;
				}
				XElement xelement = value as XElement;
				if (xelement != null)
				{
					converted = xelement.ToString();
					return true;
				}
				converted = null;
				return false;
			}

			// Token: 0x06000CDA RID: 3290 RVA: 0x0002D640 File Offset: 0x0002B840
			internal static string AppendDecimalMarkerToDouble(string input)
			{
				IEnumerable<char> enumerable = input.ToCharArray();
				if (enumerable.All(new Func<char, bool>(char.IsDigit)))
				{
					return input + ".0";
				}
				return input;
			}

			// Token: 0x06000CDB RID: 3291 RVA: 0x0002D675 File Offset: 0x0002B875
			[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value", Justification = "Method is compiled into 3 assemblies, and the parameter is used in 2 of them.")]
			private static bool TryGetByteArrayFromBinary(object value, out byte[] array)
			{
				return ClientConvert.TryConvertBinaryToByteArray(value, out array);
			}
		}

		// Token: 0x02000142 RID: 322
		private sealed class DefaultLiteralFormatter : LiteralFormatter
		{
			// Token: 0x06000CDC RID: 3292 RVA: 0x0002D67E File Offset: 0x0002B87E
			internal DefaultLiteralFormatter()
				: this(false)
			{
			}

			// Token: 0x06000CDD RID: 3293 RVA: 0x0002D687 File Offset: 0x0002B887
			private DefaultLiteralFormatter(bool disableUrlEncoding)
			{
				this.disableUrlEncoding = disableUrlEncoding;
			}

			// Token: 0x06000CDE RID: 3294 RVA: 0x0002D698 File Offset: 0x0002B898
			internal override string Format(object value)
			{
				object obj;
				if (LiteralFormatter.SharedUtils.TryConvertToStandardType(value, out obj))
				{
					value = obj;
				}
				return this.FormatLiteralWithTypePrefix(value);
			}

			// Token: 0x06000CDF RID: 3295 RVA: 0x0002D6B9 File Offset: 0x0002B8B9
			protected override string EscapeResultForUri(string result)
			{
				if (!this.disableUrlEncoding)
				{
					result = base.EscapeResultForUri(result);
				}
				return result;
			}

			// Token: 0x06000CE0 RID: 3296 RVA: 0x0002D6D0 File Offset: 0x0002B8D0
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

			// Token: 0x040006B6 RID: 1718
			private readonly bool disableUrlEncoding;
		}

		// Token: 0x02000143 RID: 323
		private sealed class KeysAsSegmentsLiteralFormatter : LiteralFormatter
		{
			// Token: 0x06000CE1 RID: 3297 RVA: 0x0002D7A7 File Offset: 0x0002B9A7
			internal KeysAsSegmentsLiteralFormatter()
			{
			}

			// Token: 0x06000CE2 RID: 3298 RVA: 0x0002D7B0 File Offset: 0x0002B9B0
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

			// Token: 0x06000CE3 RID: 3299 RVA: 0x0002D7F5 File Offset: 0x0002B9F5
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
