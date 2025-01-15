using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005FD RID: 1533
	internal static class StringUtil
	{
		// Token: 0x06004B08 RID: 19208 RVA: 0x00109A08 File Offset: 0x00107C08
		internal static string BuildDelimitedList<T>(IEnumerable<T> values, StringUtil.ToStringConverter<T> converter, string delimiter)
		{
			if (values == null)
			{
				return string.Empty;
			}
			if (converter == null)
			{
				converter = new StringUtil.ToStringConverter<T>(StringUtil.InvariantConvertToString<T>);
			}
			if (delimiter == null)
			{
				delimiter = ", ";
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (T t in values)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(delimiter);
				}
				stringBuilder.Append(converter(t));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00109A98 File Offset: 0x00107C98
		internal static string ToCommaSeparatedString(IEnumerable list)
		{
			return StringUtil.ToSeparatedString(list, ", ", string.Empty);
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x00109AAA File Offset: 0x00107CAA
		internal static string ToSeparatedString(IEnumerable list, string separator, string nullValue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringUtil.ToSeparatedString(stringBuilder, list, separator, nullValue);
			return stringBuilder.ToString();
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00109ABF File Offset: 0x00107CBF
		internal static string ToCommaSeparatedStringSorted(IEnumerable list)
		{
			return StringUtil.ToSeparatedStringSorted(list, ", ", string.Empty);
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x00109AD1 File Offset: 0x00107CD1
		internal static string ToSeparatedStringSorted(IEnumerable list, string separator, string nullValue)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringUtil.ToSeparatedStringPrivate(stringBuilder, list, separator, nullValue, true);
			return stringBuilder.ToString();
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x00109AE7 File Offset: 0x00107CE7
		internal static string MembersToCommaSeparatedString(IEnumerable members)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			StringUtil.ToCommaSeparatedString(stringBuilder, members);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x00109B12 File Offset: 0x00107D12
		internal static void ToCommaSeparatedString(StringBuilder builder, IEnumerable list)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, ", ", string.Empty, false);
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x00109B26 File Offset: 0x00107D26
		internal static void ToCommaSeparatedStringSorted(StringBuilder builder, IEnumerable list)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, ", ", string.Empty, true);
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x00109B3A File Offset: 0x00107D3A
		internal static void ToSeparatedString(StringBuilder builder, IEnumerable list, string separator)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, separator, string.Empty, false);
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x00109B4A File Offset: 0x00107D4A
		internal static void ToSeparatedStringSorted(StringBuilder builder, IEnumerable list, string separator)
		{
			StringUtil.ToSeparatedStringPrivate(builder, list, separator, string.Empty, true);
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x00109B5A File Offset: 0x00107D5A
		internal static void ToSeparatedString(StringBuilder stringBuilder, IEnumerable list, string separator, string nullValue)
		{
			StringUtil.ToSeparatedStringPrivate(stringBuilder, list, separator, nullValue, false);
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x00109B68 File Offset: 0x00107D68
		private static void ToSeparatedStringPrivate(StringBuilder stringBuilder, IEnumerable list, string separator, string nullValue, bool toSort)
		{
			if (list == null)
			{
				return;
			}
			bool flag = true;
			List<string> list2 = new List<string>();
			foreach (object obj in list)
			{
				string text;
				if (obj == null)
				{
					text = nullValue;
				}
				else
				{
					text = StringUtil.FormatInvariant("{0}", new object[] { obj });
				}
				list2.Add(text);
			}
			if (toSort)
			{
				list2.Sort(StringComparer.Ordinal);
			}
			foreach (string text2 in list2)
			{
				if (!flag)
				{
					stringBuilder.Append(separator);
				}
				stringBuilder.Append(text2);
				flag = false;
			}
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x00109C44 File Offset: 0x00107E44
		internal static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x00109C52 File Offset: 0x00107E52
		internal static StringBuilder FormatStringBuilder(StringBuilder builder, string format, params object[] args)
		{
			builder.AppendFormat(CultureInfo.InvariantCulture, format, args);
			return builder;
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x00109C64 File Offset: 0x00107E64
		internal static StringBuilder IndentNewLine(StringBuilder builder, int indent)
		{
			builder.AppendLine();
			for (int i = 0; i < indent; i++)
			{
				builder.Append("    ");
			}
			return builder;
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x00109C91 File Offset: 0x00107E91
		internal static string FormatIndex(string arrayVarName, int index)
		{
			return new StringBuilder(arrayVarName.Length + 10 + 2).Append(arrayVarName).Append('[').Append(index)
				.Append(']')
				.ToString();
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x00109CC2 File Offset: 0x00107EC2
		private static string InvariantConvertToString<T>(T value)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { value });
		}

		// Token: 0x04001A46 RID: 6726
		private const string s_defaultDelimiter = ", ";

		// Token: 0x02000C46 RID: 3142
		// (Invoke) Token: 0x06006A45 RID: 27205
		internal delegate string ToStringConverter<T>(T value);
	}
}
