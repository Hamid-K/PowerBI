using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace NLog.Internal
{
	// Token: 0x02000150 RID: 336
	public static class XmlHelper
	{
		// Token: 0x06001007 RID: 4103 RVA: 0x000294E0 File Offset: 0x000276E0
		private static string RemoveInvalidXmlChars(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (!XmlConvert.IsXmlChar(text[i]))
				{
					return XmlHelper.CreateValidXmlString(text);
				}
			}
			return text;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00029524 File Offset: 0x00027724
		private static string CreateValidXmlString(string text)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			foreach (char c in text)
			{
				if (XmlConvert.IsXmlChar(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0002956C File Offset: 0x0002776C
		internal static string EscapeXmlString(string text, bool xmlEncodeNewlines, StringBuilder result = null)
		{
			if (result == null && XmlHelper.SmallAndNoEscapeNeeded(text, xmlEncodeNewlines))
			{
				return text;
			}
			StringBuilder stringBuilder = result ?? new StringBuilder(text.Length);
			int i = 0;
			while (i < text.Length)
			{
				char c = text[i];
				if (c <= '"')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c != '"')
							{
								goto IL_00F8;
							}
							stringBuilder.Append("&quot;");
						}
						else if (xmlEncodeNewlines)
						{
							stringBuilder.Append("&#13;");
						}
						else
						{
							stringBuilder.Append(text[i]);
						}
					}
					else if (xmlEncodeNewlines)
					{
						stringBuilder.Append("&#10;");
					}
					else
					{
						stringBuilder.Append(text[i]);
					}
				}
				else if (c <= '\'')
				{
					if (c != '&')
					{
						if (c != '\'')
						{
							goto IL_00F8;
						}
						stringBuilder.Append("&apos;");
					}
					else
					{
						stringBuilder.Append("&amp;");
					}
				}
				else if (c != '<')
				{
					if (c != '>')
					{
						goto IL_00F8;
					}
					stringBuilder.Append("&gt;");
				}
				else
				{
					stringBuilder.Append("&lt;");
				}
				IL_0106:
				i++;
				continue;
				IL_00F8:
				stringBuilder.Append(text[i]);
				goto IL_0106;
			}
			if (result != null)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0002969A File Offset: 0x0002789A
		private static bool SmallAndNoEscapeNeeded(string text, bool xmlEncodeNewlines)
		{
			return text.Length < 4096 && text.IndexOfAny(xmlEncodeNewlines ? XmlHelper.XmlEscapeNewlineChars : XmlHelper.XmlEscapeChars) < 0;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000296C3 File Offset: 0x000278C3
		internal static string XmlConvertToStringSafe(object value)
		{
			return XmlHelper.XmlConvertToString(value, true);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x000296CC File Offset: 0x000278CC
		internal static string XmlConvertToString(object value)
		{
			return XmlHelper.XmlConvertToString(value, false);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x000296D8 File Offset: 0x000278D8
		internal static string XmlConvertToElementName(string xmlElementName, bool allowNamespace)
		{
			XmlHelper.<>c__DisplayClass8_0 CS$<>8__locals1;
			CS$<>8__locals1.xmlElementName = xmlElementName;
			if (string.IsNullOrEmpty(CS$<>8__locals1.xmlElementName))
			{
				return CS$<>8__locals1.xmlElementName;
			}
			CS$<>8__locals1.xmlElementName = XmlHelper.RemoveInvalidXmlChars(CS$<>8__locals1.xmlElementName);
			StringBuilder stringBuilder = null;
			for (int i = 0; i < CS$<>8__locals1.xmlElementName.Length; i++)
			{
				char c = CS$<>8__locals1.xmlElementName[i];
				if (char.IsLetter(c))
				{
					if (stringBuilder != null)
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					bool flag = false;
					switch (c)
					{
					case '-':
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						if (i != 0)
						{
							if (stringBuilder != null)
							{
								stringBuilder.Append(c);
								goto IL_0103;
							}
							goto IL_0103;
						}
						else
						{
							flag = true;
						}
						break;
					case '/':
						break;
					case ':':
						if (i != 0 && allowNamespace)
						{
							allowNamespace = false;
							if (stringBuilder != null)
							{
								stringBuilder.Append(c);
								goto IL_0103;
							}
							goto IL_0103;
						}
						break;
					default:
						if (c == '_')
						{
							if (stringBuilder != null)
							{
								stringBuilder.Append(c);
								goto IL_0103;
							}
							goto IL_0103;
						}
						break;
					}
					if (stringBuilder == null)
					{
						stringBuilder = XmlHelper.<XmlConvertToElementName>g__CreateStringBuilder|8_0(i, ref CS$<>8__locals1);
					}
					stringBuilder.Append('_');
					if (flag)
					{
						stringBuilder.Append(c);
					}
				}
				IL_0103:;
			}
			if (stringBuilder != null)
			{
				stringBuilder.TrimRight(0);
			}
			return ((stringBuilder != null) ? stringBuilder.ToString() : null) ?? CS$<>8__locals1.xmlElementName;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00029820 File Offset: 0x00027A20
		private static string XmlConvertToString(object value, bool safeConversion)
		{
			string text;
			try
			{
				IConvertible convertible = value as IConvertible;
				TypeCode typeCode = ((value == null) ? TypeCode.Empty : ((convertible != null) ? convertible.GetTypeCode() : TypeCode.Object));
				if (typeCode != TypeCode.Object)
				{
					text = XmlHelper.XmlConvertToString(convertible, typeCode, safeConversion);
				}
				else
				{
					text = XmlHelper.XmlConvertToStringInvariant(value, safeConversion);
				}
			}
			catch
			{
				text = (safeConversion ? "" : null);
			}
			return text;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00029880 File Offset: 0x00027A80
		private static string XmlConvertToStringInvariant(object value, bool safeConversion)
		{
			string text2;
			try
			{
				string text = Convert.ToString(value, CultureInfo.InvariantCulture);
				text2 = (safeConversion ? XmlHelper.RemoveInvalidXmlChars(text) : text);
			}
			catch
			{
				text2 = (safeConversion ? "" : null);
			}
			return text2;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x000298C8 File Offset: 0x00027AC8
		internal static string XmlConvertToString(IConvertible value, TypeCode objTypeCode, bool safeConversion = false)
		{
			if (value == null)
			{
				return "null";
			}
			switch (objTypeCode)
			{
			case TypeCode.Boolean:
				return XmlConvert.ToString(value.ToBoolean(CultureInfo.InvariantCulture));
			case TypeCode.Char:
				return XmlConvert.ToString(value.ToChar(CultureInfo.InvariantCulture));
			case TypeCode.SByte:
				return XmlConvert.ToString(value.ToSByte(CultureInfo.InvariantCulture));
			case TypeCode.Byte:
				return XmlConvert.ToString(value.ToByte(CultureInfo.InvariantCulture));
			case TypeCode.Int16:
				return XmlConvert.ToString(value.ToInt16(CultureInfo.InvariantCulture));
			case TypeCode.UInt16:
				return XmlConvert.ToString(value.ToUInt16(CultureInfo.InvariantCulture));
			case TypeCode.Int32:
				return XmlConvert.ToString(value.ToInt32(CultureInfo.InvariantCulture));
			case TypeCode.UInt32:
				return XmlConvert.ToString(value.ToUInt32(CultureInfo.InvariantCulture));
			case TypeCode.Int64:
				return XmlConvert.ToString(value.ToInt64(CultureInfo.InvariantCulture));
			case TypeCode.UInt64:
				return XmlConvert.ToString(value.ToUInt64(CultureInfo.InvariantCulture));
			case TypeCode.Single:
			{
				float num = value.ToSingle(CultureInfo.InvariantCulture);
				if (!float.IsInfinity(num))
				{
					return XmlConvert.ToString(num);
				}
				return Convert.ToString(num, CultureInfo.InvariantCulture);
			}
			case TypeCode.Double:
			{
				double num2 = value.ToDouble(CultureInfo.InvariantCulture);
				if (!double.IsInfinity(num2))
				{
					return XmlConvert.ToString(num2);
				}
				return Convert.ToString(num2, CultureInfo.InvariantCulture);
			}
			case TypeCode.Decimal:
				return XmlConvert.ToString(value.ToDecimal(CultureInfo.InvariantCulture));
			case TypeCode.DateTime:
				return XmlConvert.ToString(value.ToDateTime(CultureInfo.InvariantCulture), XmlDateTimeSerializationMode.Utc);
			case TypeCode.String:
				if (!safeConversion)
				{
					return value.ToString(CultureInfo.InvariantCulture);
				}
				return XmlHelper.RemoveInvalidXmlChars(value.ToString(CultureInfo.InvariantCulture));
			}
			return XmlHelper.XmlConvertToStringInvariant(value, safeConversion);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00029A6D File Offset: 0x00027C6D
		public static void WriteAttributeSafeString(this XmlWriter writer, string prefix, string localName, string ns, string value)
		{
			writer.WriteAttributeString(XmlHelper.RemoveInvalidXmlChars(prefix), XmlHelper.RemoveInvalidXmlChars(localName), XmlHelper.RemoveInvalidXmlChars(ns), XmlHelper.RemoveInvalidXmlChars(value));
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00029A8E File Offset: 0x00027C8E
		public static void WriteAttributeSafeString(this XmlWriter writer, string localName, string value)
		{
			writer.WriteAttributeString(XmlHelper.RemoveInvalidXmlChars(localName), XmlHelper.RemoveInvalidXmlChars(value));
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00029AA2 File Offset: 0x00027CA2
		public static void WriteElementSafeString(this XmlWriter writer, string prefix, string localName, string ns, string value)
		{
			writer.WriteElementString(XmlHelper.RemoveInvalidXmlChars(prefix), XmlHelper.RemoveInvalidXmlChars(localName), XmlHelper.RemoveInvalidXmlChars(ns), XmlHelper.RemoveInvalidXmlChars(value));
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00029AC3 File Offset: 0x00027CC3
		public static void WriteSafeCData(this XmlWriter writer, string text)
		{
			writer.WriteCData(XmlHelper.RemoveInvalidXmlChars(text));
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00029B00 File Offset: 0x00027D00
		[CompilerGenerated]
		internal static StringBuilder <XmlConvertToElementName>g__CreateStringBuilder|8_0(int i, ref XmlHelper.<>c__DisplayClass8_0 A_1)
		{
			StringBuilder stringBuilder = new StringBuilder(A_1.xmlElementName.Length);
			if (i > 0)
			{
				stringBuilder.Append(A_1.xmlElementName, 0, i);
			}
			return stringBuilder;
		}

		// Token: 0x04000450 RID: 1104
		private static readonly char[] XmlEscapeChars = new char[] { '<', '>', '&', '\'', '"' };

		// Token: 0x04000451 RID: 1105
		private static readonly char[] XmlEscapeNewlineChars = new char[] { '<', '>', '&', '\'', '"', '\r', '\n' };
	}
}
