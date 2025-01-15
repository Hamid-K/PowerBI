using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200005F RID: 95
	internal static class XmlUtil
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000CACC File Offset: 0x0000ACCC
		internal static XmlTypeCode GetXmlTypeCode(Type clrType)
		{
			if (clrType == null)
			{
				throw new ArgumentNullException("clrType");
			}
			switch (Type.GetTypeCode(clrType))
			{
			case TypeCode.Boolean:
				return XmlTypeCode.Boolean;
			case TypeCode.SByte:
				return XmlTypeCode.Byte;
			case TypeCode.Byte:
				return XmlTypeCode.UnsignedByte;
			case TypeCode.Int16:
				return XmlTypeCode.Short;
			case TypeCode.UInt16:
				return XmlTypeCode.UnsignedShort;
			case TypeCode.Int32:
				return XmlTypeCode.Int;
			case TypeCode.UInt32:
				return XmlTypeCode.UnsignedInt;
			case TypeCode.Int64:
				return XmlTypeCode.Long;
			case TypeCode.UInt64:
				return XmlTypeCode.UnsignedLong;
			case TypeCode.Single:
				return XmlTypeCode.Float;
			case TypeCode.Double:
				return XmlTypeCode.Double;
			case TypeCode.Decimal:
				return XmlTypeCode.Decimal;
			case TypeCode.DateTime:
				return XmlTypeCode.DateTime;
			case TypeCode.String:
				return XmlTypeCode.String;
			}
			if (clrType == typeof(TimeSpan))
			{
				return XmlTypeCode.Duration;
			}
			if (clrType == typeof(byte[]))
			{
				return XmlTypeCode.Base64Binary;
			}
			if (clrType == typeof(Uri))
			{
				return XmlTypeCode.AnyUri;
			}
			if (clrType == typeof(XmlQualifiedName) || clrType == typeof(QName))
			{
				return XmlTypeCode.QName;
			}
			throw new ArgumentOutOfRangeException(DevExceptionMessages.Xml_UnexpectedValueType(clrType));
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		internal static void WriteXsiTypeAttribute(XmlWriter xw, XmlTypeCode typeCode)
		{
			XmlSchemaType builtInSimpleType = XmlSchemaType.GetBuiltInSimpleType(typeCode);
			if (builtInSimpleType == null)
			{
				throw new ArgumentOutOfRangeException("typeCode");
			}
			XmlUtil.WriteXsiTypeAttribute(xw, builtInSimpleType.QualifiedName);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000CC0A File Offset: 0x0000AE0A
		internal static void WriteXsiTypeAttribute(XmlWriter xw, XmlQualifiedName xmlType)
		{
			xw.WriteStartAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
			xw.WriteQualifiedName(xmlType.Name, xmlType.Namespace);
			xw.WriteEndAttribute();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000CC34 File Offset: 0x0000AE34
		internal static void WriteXsiNilAttribute(XmlWriter xw)
		{
			xw.WriteStartAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			xw.WriteValue(true);
			xw.WriteEndAttribute();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000CC54 File Offset: 0x0000AE54
		internal static void WrapXmlExceptions(Operation operation, ModelingErrorCode errorCode, XmlUtil.ErrorMessageWrap wrapMessage)
		{
			try
			{
				operation();
			}
			catch (XmlSchemaValidationException ex)
			{
				throw new XmlValidationException(errorCode, wrapMessage(ex.Message), null, ex.LineNumber, ex.LinePosition);
			}
			catch (XmlException ex2)
			{
				throw new XmlValidationException(errorCode, wrapMessage(ex2.Message), null, ex2.LineNumber, ex2.LinePosition, true);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		internal static void CheckElement(XmlReader xr, string localName, string namespaceUri)
		{
			if (!xr.IsStartElement(localName, namespaceUri))
			{
				throw XmlUtil.CreateXmlException(SRErrors.Xml_NodeMismatch(XmlNodeType.Element, localName, namespaceUri, xr.NodeType, xr.LocalName, xr.NamespaceURI), xr as IXmlLineInfo);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000CCFA File Offset: 0x0000AEFA
		private static XmlException CreateXmlException(string message, IXmlLineInfo lineInfo)
		{
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				return new XmlException(message, null, lineInfo.LineNumber, lineInfo.LinePosition);
			}
			return new XmlException(message);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000CD24 File Offset: 0x0000AF24
		internal static void LoadDirectChildren(XmlReader xr, string parentElementName, string parentElementNamespaceURI, XmlUtil.LoadXmlElementLDC loadXmlElement)
		{
			if (xr.NodeType != XmlNodeType.Element || xr.LocalName != parentElementName)
			{
				throw new InternalModelingException(string.Concat(new string[]
				{
					"Unexpected xr state at the end of reading DataSourceView: xr.NodeType == ",
					xr.NodeType.ToString(),
					", expected Element; xr.LocalName == '",
					xr.LocalName,
					"', expected '",
					parentElementName,
					"'"
				}));
			}
			if (xr.IsEmptyElement)
			{
				xr.Read();
				return;
			}
			xr.Read();
			int depth = xr.Depth;
			for (;;)
			{
				if (xr.Depth == depth && xr.NodeType == XmlNodeType.Element && xr.NamespaceURI == parentElementNamespaceURI)
				{
					if (!loadXmlElement(xr))
					{
						xr.Read();
					}
				}
				else
				{
					if (xr.Depth < depth)
					{
						break;
					}
					xr.Read();
				}
			}
			if (xr.NodeType != XmlNodeType.EndElement || xr.LocalName != parentElementName)
			{
				throw new InternalModelingException(string.Concat(new string[]
				{
					"Unexpected xr state at the end of reading DataSourceView: xr.NodeType == ",
					xr.NodeType.ToString(),
					", expected EndElement; xr.LocalName == '",
					xr.LocalName,
					"', expected '",
					parentElementName,
					"'"
				}));
			}
		}

		// Token: 0x04000225 RID: 549
		internal const string XmlnsNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000226 RID: 550
		internal const string XmlnsPrefix = "xmlns";

		// Token: 0x04000227 RID: 551
		internal const string XsiTypeAttr = "type";

		// Token: 0x04000228 RID: 552
		internal const string XsiNilAttr = "nil";

		// Token: 0x04000229 RID: 553
		internal static readonly ReadOnlyCollection<QName> XmlSchemaNsPrefixes = new ReadOnlyCollection<QName>(new QName[]
		{
			new QName("xsd", "http://www.w3.org/2001/XMLSchema"),
			new QName("xsi", "http://www.w3.org/2001/XMLSchema-instance")
		});

		// Token: 0x0200012B RID: 299
		// (Invoke) Token: 0x06000DD9 RID: 3545
		internal delegate string ErrorMessageWrap(string detailMessage);

		// Token: 0x0200012C RID: 300
		// (Invoke) Token: 0x06000DDD RID: 3549
		internal delegate bool LoadXmlElementLDC(XmlReader xr);
	}
}
