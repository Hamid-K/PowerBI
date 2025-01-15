using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.OData.Core.Atom;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x0200001D RID: 29
	internal static class AtomValueUtils
	{
		// Token: 0x060000DD RID: 221 RVA: 0x0000374C File Offset: 0x0000194C
		internal static void WritePrimitiveValue(XmlWriter writer, object value)
		{
			if (!PrimitiveConverter.Instance.TryWriteAtom(value, writer))
			{
				string text = AtomValueUtils.ConvertPrimitiveToString(value);
				ODataAtomWriterUtils.WriteString(writer, text);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003778 File Offset: 0x00001978
		internal static string ConvertPrimitiveToString(object value)
		{
			string text;
			if (!AtomValueUtils.TryConvertPrimitiveToString(value, out text))
			{
				throw new ODataException(Strings.AtomValueUtils_CannotConvertValueToAtomPrimitive(value.GetType().FullName));
			}
			return text;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000037A8 File Offset: 0x000019A8
		internal static ODataEnumValue ReadEnumValue(XmlReader reader, IEdmEnumTypeReference enumTypeReference)
		{
			string text = reader.ReadElementContentValue();
			string text2 = ((enumTypeReference != null) ? enumTypeReference.FullName() : null);
			return new ODataEnumValue(text, text2);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000037D0 File Offset: 0x000019D0
		internal static object ReadPrimitiveValue(XmlReader reader, IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			object obj;
			if (!PrimitiveConverter.Instance.TryTokenizeFromXml(reader, EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference), out obj))
			{
				string text = reader.ReadElementContentValue();
				return AtomValueUtils.ConvertStringToPrimitive(text, primitiveTypeReference);
			}
			return obj;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003804 File Offset: 0x00001A04
		internal static string ToString(AtomTextConstructKind textConstructKind)
		{
			switch (textConstructKind)
			{
			case AtomTextConstructKind.Text:
				return "text";
			case AtomTextConstructKind.Html:
				return "html";
			case AtomTextConstructKind.Xhtml:
				return "xhtml";
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataAtomConvert_ToString));
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000384C File Offset: 0x00001A4C
		internal static bool TryConvertPrimitiveToString(object value, out string result)
		{
			result = null;
			switch (PlatformHelper.GetTypeCode(value.GetType()))
			{
			case 3:
				result = ODataAtomConvert.ToString((bool)value);
				return true;
			case 5:
				result = ((sbyte)value).ToString();
				return true;
			case 6:
				result = ODataAtomConvert.ToString((byte)value);
				return true;
			case 7:
				result = ((short)value).ToString();
				return true;
			case 9:
				result = ((int)value).ToString();
				return true;
			case 11:
				result = ((long)value).ToString();
				return true;
			case 13:
				result = ((float)value).ToString();
				return true;
			case 14:
				result = ((double)value).ToString();
				return true;
			case 15:
				result = ODataAtomConvert.ToString((decimal)value);
				return true;
			case 18:
				result = (string)value;
				return true;
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				result = array.ToString();
			}
			else if (value is DateTimeOffset)
			{
				result = ODataAtomConvert.ToString((DateTimeOffset)value);
			}
			else if (value is Guid)
			{
				result = ((Guid)value).ToString();
			}
			else if (value is TimeSpan)
			{
				result = ((TimeSpan)value).ToString();
			}
			else if (value is Date)
			{
				result = ODataAtomConvert.ToString((Date)value);
			}
			else
			{
				if (!(value is TimeOfDay))
				{
					return false;
				}
				result = ODataAtomConvert.ToString((TimeOfDay)value);
			}
			return true;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000039F0 File Offset: 0x00001BF0
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		internal static object ConvertStringToPrimitive(string text, IEdmPrimitiveTypeReference targetTypeReference)
		{
			try
			{
				switch (targetTypeReference.PrimitiveKind())
				{
				case EdmPrimitiveTypeKind.Binary:
					return Convert.FromBase64String(text);
				case EdmPrimitiveTypeKind.Boolean:
					return AtomValueUtils.ConvertXmlBooleanValue(text);
				case EdmPrimitiveTypeKind.Byte:
					return XmlConvert.ToByte(text);
				case EdmPrimitiveTypeKind.DateTimeOffset:
					return PlatformHelper.ConvertStringToDateTimeOffset(text);
				case EdmPrimitiveTypeKind.Decimal:
					return XmlConvert.ToDecimal(text);
				case EdmPrimitiveTypeKind.Double:
					return XmlConvert.ToDouble(text);
				case EdmPrimitiveTypeKind.Guid:
					return new Guid(text);
				case EdmPrimitiveTypeKind.Int16:
					return XmlConvert.ToInt16(text);
				case EdmPrimitiveTypeKind.Int32:
					return XmlConvert.ToInt32(text);
				case EdmPrimitiveTypeKind.Int64:
					return XmlConvert.ToInt64(text);
				case EdmPrimitiveTypeKind.SByte:
					return XmlConvert.ToSByte(text);
				case EdmPrimitiveTypeKind.Single:
					return XmlConvert.ToSingle(text);
				case EdmPrimitiveTypeKind.String:
					return text;
				case EdmPrimitiveTypeKind.Duration:
					return EdmValueParser.ParseDuration(text);
				case EdmPrimitiveTypeKind.Date:
					return PlatformHelper.ConvertStringToDate(text);
				case EdmPrimitiveTypeKind.TimeOfDay:
					return PlatformHelper.ConvertStringToTimeOfDay(text);
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.AtomValueUtils_ConvertStringToPrimitive));
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ReaderValidationUtils.GetPrimitiveTypeConversionException(targetTypeReference, ex, text);
			}
			object obj;
			return obj;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003BC8 File Offset: 0x00001DC8
		private static bool ConvertXmlBooleanValue(string text)
		{
			text = text.Trim(AtomValueUtils.XmlWhitespaceChars);
			string text2;
			if ((text2 = text) != null)
			{
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60000e1-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
					dictionary.Add("true", 0);
					dictionary.Add("True", 1);
					dictionary.Add("1", 2);
					dictionary.Add("false", 3);
					dictionary.Add("False", 4);
					dictionary.Add("0", 5);
					<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60000e1-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{62D55117-3DCE-48DB-9813-EE17180469C5}.$$method0x60000e1-1.TryGetValue(text2, ref num))
				{
					switch (num)
					{
					case 0:
					case 1:
					case 2:
						return true;
					case 3:
					case 4:
					case 5:
						return false;
					}
				}
			}
			return XmlConvert.ToBoolean(text);
		}

		// Token: 0x040000D4 RID: 212
		private static readonly char[] XmlWhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
