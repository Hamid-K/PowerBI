using System;
using System.Data.Entity;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200005A RID: 90
	internal static class Utils
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x00013682 File Offset: 0x00011882
		internal static void ExtractNamespaceAndName(SchemaDataModelOption dataModel, string qualifiedTypeName, out string namespaceName, out string name)
		{
			Utils.GetBeforeAndAfterLastPeriod(qualifiedTypeName, out namespaceName, out name);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001368C File Offset: 0x0001188C
		internal static string ExtractTypeName(SchemaDataModelOption dataModel, string qualifiedTypeName)
		{
			return Utils.GetEverythingAfterLastPeriod(qualifiedTypeName);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00013694 File Offset: 0x00011894
		private static void GetBeforeAndAfterLastPeriod(string qualifiedTypeName, out string before, out string after)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				before = null;
				after = qualifiedTypeName;
				return;
			}
			before = qualifiedTypeName.Substring(0, num);
			after = qualifiedTypeName.Substring(num + 1);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000136CC File Offset: 0x000118CC
		private static string GetEverythingAfterLastPeriod(string qualifiedTypeName)
		{
			int num = qualifiedTypeName.LastIndexOf('.');
			if (num < 0)
			{
				return qualifiedTypeName;
			}
			return qualifiedTypeName.Substring(num + 1);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000136F1 File Offset: 0x000118F1
		public static bool GetString(Schema schema, XmlReader reader, out string value)
		{
			value = reader.Value;
			if (string.IsNullOrEmpty(value))
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(value, reader.Name));
				return false;
			}
			return true;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001371E File Offset: 0x0001191E
		public static bool GetDottedName(Schema schema, XmlReader reader, out string name)
		{
			return Utils.GetString(schema, reader, out name) && Utils.ValidateDottedName(schema, reader, name);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00013738 File Offset: 0x00011938
		internal static bool ValidateDottedName(Schema schema, XmlReader reader, string name)
		{
			if (schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				string[] array = name.Split(new char[] { '.' });
				for (int i = 0; i < array.Length; i++)
				{
					if (!Utils.ValidUndottedName(array[i]))
					{
						schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(name, reader.Name));
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00013790 File Offset: 0x00011990
		public static bool GetUndottedName(Schema schema, XmlReader reader, out string name)
		{
			name = reader.Value;
			if (string.IsNullOrEmpty(name))
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.EmptyName(reader.Name));
				return false;
			}
			if (schema.DataModel == SchemaDataModelOption.EntityDataModel && !Utils.ValidUndottedName(name))
			{
				schema.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidName(name, reader.Name));
				return false;
			}
			return true;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000137F0 File Offset: 0x000119F0
		internal static bool ValidUndottedName(string name)
		{
			return !string.IsNullOrEmpty(name) && Utils.UndottedNameValidator.IsMatch(name) && Utils.IsValidLanguageIndependentIdentifier(name);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001380F File Offset: 0x00011A0F
		private static bool IsValidLanguageIndependentIdentifier(string name)
		{
			return Utils.IsValidTypeNameOrIdentifier(name, false);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00013818 File Offset: 0x00011A18
		private static bool IsValidTypeNameOrIdentifier(string value, bool isTypeName)
		{
			bool flag = true;
			if (value.Length == 0)
			{
				return false;
			}
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				switch (char.GetUnicodeCategory(c))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.LetterNumber:
					flag = false;
					break;
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.ConnectorPunctuation:
					if (flag && c != '_')
					{
						return false;
					}
					flag = false;
					break;
				case UnicodeCategory.EnclosingMark:
				case UnicodeCategory.OtherNumber:
				case UnicodeCategory.SpaceSeparator:
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Control:
				case UnicodeCategory.Format:
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_0088;
				default:
					goto IL_0088;
				}
				IL_0097:
				i++;
				continue;
				IL_0088:
				if (!isTypeName || !Utils.IsSpecialTypeChar(c, ref flag))
				{
					return false;
				}
				goto IL_0097;
			}
			return true;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000138D0 File Offset: 0x00011AD0
		private static bool IsSpecialTypeChar(char ch, ref bool nextMustBeStartChar)
		{
			if (ch <= '>')
			{
				switch (ch)
				{
				case '$':
				case '&':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
					break;
				case '%':
				case '\'':
				case '(':
				case ')':
					return false;
				default:
					switch (ch)
					{
					case ':':
					case '<':
					case '>':
						break;
					case ';':
					case '=':
						return false;
					default:
						return false;
					}
					break;
				}
			}
			else if (ch != '[' && ch != ']')
			{
				if (ch != '`')
				{
					return false;
				}
				return true;
			}
			nextMustBeStartChar = true;
			return true;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00013950 File Offset: 0x00011B50
		public static bool GetBool(Schema schema, XmlReader reader, out bool value)
		{
			try
			{
				value = reader.ReadContentAsBoolean();
				return true;
			}
			catch (XmlException)
			{
			}
			throw new Exception(string.Format(CultureInfo.CurrentCulture, "{1} value ({0}) is not valid: Expected 'true' or 'false'.", reader.Value, reader.Name));
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000139A0 File Offset: 0x00011BA0
		public static bool GetInt(Schema schema, XmlReader reader, out int value)
		{
			string value2 = reader.Value;
			value = int.MinValue;
			if (int.TryParse(value2, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
			{
				return true;
			}
			schema.AddError(ErrorCode.IntegerExpected, EdmSchemaErrorSeverity.Error, reader, Strings.ValueNotUnderstood(reader.Value, reader.Name));
			return false;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000139DB File Offset: 0x00011BDB
		public static bool GetByte(Schema schema, XmlReader reader, out byte value)
		{
			string value2 = reader.Value;
			value = 0;
			if (byte.TryParse(value2, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
			{
				return true;
			}
			schema.AddError(ErrorCode.ByteValueExpected, EdmSchemaErrorSeverity.Error, reader, Strings.ValueNotUnderstood(reader.Value, reader.Name));
			return false;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00013A12 File Offset: 0x00011C12
		public static int CompareNames(string lhsName, string rhsName)
		{
			return string.Compare(lhsName, rhsName, StringComparison.Ordinal);
		}

		// Token: 0x040006DF RID: 1759
		private const string StartCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}]";

		// Token: 0x040006E0 RID: 1760
		private const string OtherCharacterExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]";

		// Token: 0x040006E1 RID: 1761
		private const string NameExp = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}";

		// Token: 0x040006E2 RID: 1762
		private static Regex UndottedNameValidator = new Regex("^[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Lm}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]{0,}$", RegexOptions.Singleline);
	}
}
