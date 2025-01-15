using System;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003126 RID: 12582
	internal static class XsdTypeExtension
	{
		// Token: 0x0601B4AC RID: 111788 RVA: 0x00375B2C File Offset: 0x00373D2C
		public static string GetXsdDataTypeName(this XsdType xsdType)
		{
			switch (xsdType)
			{
			case XsdType.AnySimpleType:
				return "anySimpleType";
			case XsdType.String:
				return "string";
			case XsdType.NormalizedString:
				return "normalizedString";
			case XsdType.Token:
				return "token";
			case XsdType.Base64Binary:
				return "base64Binary";
			case XsdType.HexBinary:
				return "hexBinary";
			case XsdType.Integer:
				return "integer";
			case XsdType.PositiveInteger:
				return "positiveInteger";
			case XsdType.NegativeInteger:
				return "negativeInteger";
			case XsdType.NonNegativeInteger:
				return "nonNegativeInteger";
			case XsdType.NonPositiveInteger:
				return "nonPositiveInteger";
			case XsdType.Long:
				return "long";
			case XsdType.UnsignedLong:
				return "unsignedLong";
			case XsdType.Int:
				return "int";
			case XsdType.UnsignedInt:
				return "unsignedInt";
			case XsdType.Short:
				return "short";
			case XsdType.UnsignedShort:
				return "unsignedShort";
			case XsdType.Byte:
				return "byte";
			case XsdType.UnsignedByte:
				return "unsignedByte";
			case XsdType.Decimal:
				return "decimal";
			case XsdType.Float:
				return "float";
			case XsdType.Double:
				return "double";
			case XsdType.Boolean:
				return "boolean";
			case XsdType.Duration:
				return "duration";
			case XsdType.DateTime:
				return "dateTime";
			case XsdType.Date:
				return "date";
			case XsdType.Time:
				return "time";
			case XsdType.GYear:
				return "gYear";
			case XsdType.GYearMonth:
				return "gYearMonth";
			case XsdType.GMonth:
				return "gMonth";
			case XsdType.GMonthDay:
				return "gMonthDay";
			case XsdType.GDay:
				return "gDay";
			case XsdType.Name:
				return "Name";
			case XsdType.QName:
				return "QName";
			case XsdType.NCName:
				return "NCName";
			case XsdType.AnyURI:
				return "anyURI";
			case XsdType.Language:
				return "language";
			case XsdType.ID:
				return "ID";
			case XsdType.IDREF:
				return "IDREF";
			case XsdType.IDREFS:
				return "IDREFS";
			case XsdType.ENTITY:
				return "ENTITY";
			case XsdType.ENTITIES:
				return "ENTITIES";
			case XsdType.NOTATION:
				return "NOTATION";
			case XsdType.NMTOKEN:
				return "NMTOKEN";
			case XsdType.NMTOKENS:
				return "NMTOKENS";
			}
			throw new ArgumentOutOfRangeException("xsdType");
		}
	}
}
