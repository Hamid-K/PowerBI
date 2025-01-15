using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000037 RID: 55
	internal static class Utils
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000BBCA File Offset: 0x00009DCA
		public static IEnumerable<string> SplitByLength(string text, int length = 75)
		{
			for (int i = 0; i < text.Length; i += length)
			{
				yield return (text.Length > i + length) ? text.Substring(i, length) : text.Substring(i);
			}
			yield break;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000BBE1 File Offset: 0x00009DE1
		public static IEnumerable<string> SplitByLengthConsideringSpaces(string text, int length)
		{
			int lengthminus = length - 1;
			for (int i = 0; i < text.Length; i += length)
			{
				if (text.Length - i > length)
				{
					bool flag = false;
					int num;
					for (int j = 0; j < lengthminus; j = num + 1)
					{
						if (text[i + lengthminus - j].Equals(' '))
						{
							yield return text.Substring(i, length - j);
							i -= j;
							flag = true;
							j = length;
						}
						num = j;
					}
					if (!flag)
					{
						yield return text.Substring(i, length);
					}
				}
				else
				{
					yield return text.Substring(i);
				}
			}
			yield break;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		public static string RemoveLineEndings(string value, string replace = " ")
		{
			if (!string.IsNullOrEmpty(value))
			{
				return Regex.Replace(value, "(\\r|\\n|\\u2028|\\u2029)", replace);
			}
			return value;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000BC10 File Offset: 0x00009E10
		public static string GetStableHash(string key)
		{
			HashBuilder hashBuilder = default(HashBuilder);
			foreach (char c in key)
			{
				hashBuilder.Add((int)c);
			}
			return hashBuilder.ToHash().ToString("X16", CultureInfo.InvariantCulture);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000BC64 File Offset: 0x00009E64
		public static void SwallowSafeExceptions(Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000BC94 File Offset: 0x00009E94
		public static T SwallowSafeExceptions<T>(Func<T> action)
		{
			try
			{
				return action();
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
			return default(T);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public static bool IsEscaped(string text)
		{
			return text.IndexOf("_-", StringComparison.Ordinal) >= 0;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		public static string EscapeBasXmlIdentifier(string name)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < name.Length; i++)
			{
				char c = name[i];
				if (c == '_' || ((i == 0) ? char.IsLetter(c) : char.IsLetterOrDigit(c)))
				{
					stringBuilder.Append(c);
				}
				else if (c == '/')
				{
					stringBuilder.Append("_-");
				}
				else
				{
					stringBuilder.Append("_--");
					stringBuilder.Append(Convert.ToByte(c).ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000BD78 File Offset: 0x00009F78
		public static SapBwDecimalNotation ToDecimalNotation(string dcpfm)
		{
			if (!string.IsNullOrEmpty(dcpfm))
			{
				char c = dcpfm[0];
				if (c == 'X')
				{
					return SapBwDecimalNotation.DotDecimalSeparatorCommaThousands;
				}
				if (c == 'Y')
				{
					return SapBwDecimalNotation.CommaDecimalSeparatorSpaceThousands;
				}
			}
			return SapBwDecimalNotation.CommaDecimalSeparatorDotThousands;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BDA8 File Offset: 0x00009FA8
		public static string ToDateFormat(string datfm)
		{
			if (!string.IsNullOrEmpty(datfm))
			{
				switch (datfm[0])
				{
				case '1':
					return "dd.mm.yyyy";
				case '2':
					return "mm/dd/yyyy";
				case '3':
					return "mm-dd-yyyy";
				case '4':
					return "yyyy.mm.dd";
				case '5':
					return "yyyy/mm/dd";
				case '6':
					return "yyyy-mm-dd";
				case '7':
					return "yy.mm.dd";
				case '8':
					return "yy/mm/dd";
				case '9':
					return "yy-mm-dd";
				case 'A':
				case 'B':
				case 'C':
					return "yyyy/mm/dd";
				}
			}
			return "dd.mm.yyyy";
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000BE60 File Offset: 0x0000A060
		public static string ToTimeFormat(string timefm)
		{
			if (!string.IsNullOrEmpty(timefm))
			{
				switch (timefm[0])
				{
				case '0':
					return "hh:nn:ss";
				case '1':
					return "hh:nn:ss AM/PM";
				case '2':
					return "hh:nn:ss am/pm";
				case '3':
					return "hh:nn:ss AM/PM";
				case '4':
					return "hh:nn:ss am/pm";
				}
			}
			return "hh:nn:ss";
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000BEBF File Offset: 0x0000A0BF
		public static SapBwDecimalNotation CurrentCultureDecimalNotation()
		{
			if (!(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ","))
			{
				return SapBwDecimalNotation.DotDecimalSeparatorCommaThousands;
			}
			return SapBwDecimalNotation.CommaDecimalSeparatorDotThousands;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public static string UnescapeBasXmlIdentifier(string tag)
		{
			int i = 0;
			int length = tag.Length;
			StringBuilder stringBuilder = new StringBuilder();
			while (i < length)
			{
				if (tag[i] != '_')
				{
					stringBuilder.Append(tag[i++]);
				}
				else if (tag[++i] != '-')
				{
					stringBuilder.Append('_');
				}
				else if (tag[++i] != '-')
				{
					stringBuilder.Append('/');
				}
				else
				{
					try
					{
						i++;
						stringBuilder.Append(Convert.ToChar(byte.Parse(tag.Substring(i, 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture)));
						i += 2;
					}
					catch (Exception ex)
					{
						if (!SafeExceptions.IsSafeException(ex))
						{
							throw;
						}
						throw new FormatException(Resources.InvalidEscapeSequence((int)(tag[i] + tag[i + 1]), ex.ToString()));
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
		public static object[] ToArray(this IEnumerable<string> source, int length)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			object[] array = new object[length];
			int num = 0;
			foreach (string text in source)
			{
				array[num++] = text;
			}
			return array;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C048 File Offset: 0x0000A248
		public static bool ToBoolean(char flag)
		{
			return flag == 'X';
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C04F File Offset: 0x0000A24F
		public static int ToInt(char c)
		{
			return (int)char.GetNumericValue(c);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000C058 File Offset: 0x0000A258
		public static bool TrySetCurrentIndex(this IRfcTable table, int index)
		{
			if (index >= 0 && table.RowCount > 0 && index < table.RowCount)
			{
				table.CurrentIndex = index;
				return true;
			}
			return false;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000C07A File Offset: 0x0000A27A
		public static string AsTrimmedString(this SapBwParameter parameter)
		{
			return Utils.RemoveLineEndings(parameter.AsString(), string.Empty).Trim();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C091 File Offset: 0x0000A291
		public static string BuildColumnName(string uniqueName, string propertyName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", uniqueName, propertyName);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
		public static string ExtractMeasureName(string columnName)
		{
			int num = columnName.IndexOf(".", StringComparison.Ordinal);
			if (num > -1)
			{
				columnName = columnName.Substring(num + 1);
			}
			return columnName.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
		}

		// Token: 0x040001DE RID: 478
		private const string Pattern = "(\\r|\\n|\\u2028|\\u2029)";

		// Token: 0x040001DF RID: 479
		public const char SpaceChar = ' ';

		// Token: 0x040001E0 RID: 480
		private const string SpaceString = " ";
	}
}
