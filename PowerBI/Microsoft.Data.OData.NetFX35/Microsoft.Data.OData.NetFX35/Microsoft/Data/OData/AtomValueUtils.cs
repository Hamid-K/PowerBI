using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Atom;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000297 RID: 663
	internal static class AtomValueUtils
	{
		// Token: 0x0600152A RID: 5418 RVA: 0x0004D0E4 File Offset: 0x0004B2E4
		internal static void WritePrimitiveValue(XmlWriter writer, object value)
		{
			if (!PrimitiveConverter.Instance.TryWriteAtom(value, writer))
			{
				string text = AtomValueUtils.ConvertPrimitiveToString(value);
				ODataAtomWriterUtils.WriteString(writer, text);
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0004D110 File Offset: 0x0004B310
		internal static string ConvertPrimitiveToString(object value)
		{
			string text;
			if (!AtomValueUtils.TryConvertPrimitiveToString(value, out text))
			{
				throw new ODataException(Strings.AtomValueUtils_CannotConvertValueToAtomPrimitive(value.GetType().FullName));
			}
			return text;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0004D140 File Offset: 0x0004B340
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

		// Token: 0x0600152D RID: 5421 RVA: 0x0004D174 File Offset: 0x0004B374
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

		// Token: 0x0600152E RID: 5422 RVA: 0x0004D1BC File Offset: 0x0004B3BC
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
			case 16:
				result = ((DateTime)value).ToString();
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
			else
			{
				if (!(value is TimeSpan))
				{
					return false;
				}
				result = ((TimeSpan)value).ToString();
			}
			return true;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0004D33C File Offset: 0x0004B53C
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
				case EdmPrimitiveTypeKind.DateTime:
					return PlatformHelper.ConvertStringToDateTime(text);
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
				case EdmPrimitiveTypeKind.Time:
					return XmlConvert.ToTimeSpan(text);
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.AtomValueUtils_ConvertStringToPrimitive));
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ReaderValidationUtils.GetPrimitiveTypeConversionException(targetTypeReference, ex);
			}
			object obj;
			return obj;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0004D4FC File Offset: 0x0004B6FC
		private static bool ConvertXmlBooleanValue(string text)
		{
			text = text.Trim(AtomValueUtils.XmlWhitespaceChars);
			string text2;
			if ((text2 = text) != null)
			{
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6001495-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(6);
					dictionary.Add("true", 0);
					dictionary.Add("True", 1);
					dictionary.Add("1", 2);
					dictionary.Add("false", 3);
					dictionary.Add("False", 4);
					dictionary.Add("0", 5);
					<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6001495-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{700B1CD8-E52F-4261-B8B3-1E258BCBAEA9}.$$method0x6001495-1.TryGetValue(text2, ref num))
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

		// Token: 0x040008F7 RID: 2295
		private static readonly char[] XmlWhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
