using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000379 RID: 889
	public sealed class ConversionUtils
	{
		// Token: 0x06001D16 RID: 7446 RVA: 0x000760F8 File Offset: 0x000742F8
		public static string Utf8BytesToString(byte[] bytes)
		{
			if (bytes == null)
			{
				return string.Empty;
			}
			byte[] preamble = Encoding.UTF8.GetPreamble();
			int num = ((bytes.Length >= preamble.Length && bytes.Take(preamble.Length).SequenceEqual(preamble)) ? preamble.Length : 0);
			return Encoding.UTF8.GetString(bytes, num, bytes.Length - num);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0007614C File Offset: 0x0007434C
		public static IList<string> CsvToValuesList(string csv, char delimiter, bool surroundValuesWithQuotes)
		{
			List<string> list = new List<string>();
			bool flag = false;
			int num = 0;
			if (string.IsNullOrEmpty(csv))
			{
				return list;
			}
			for (int i = 0; i < csv.Length - 1; i++)
			{
				if (csv[i] == delimiter && !flag)
				{
					string text = (surroundValuesWithQuotes ? ConversionUtils.EnsureSurroundingQuotes(csv.Substring(num, i - num).Trim()) : csv.Substring(num, i - num).Trim());
					list.Add(text);
					num = i + 1;
				}
				else if (csv[i] == '"')
				{
					if (csv[i + 1] != '"')
					{
						flag = !flag;
					}
					else
					{
						i++;
					}
				}
			}
			string text2 = csv.Substring(num, csv.Length - num).Trim();
			if (csv[csv.Length - 1] == delimiter && !flag)
			{
				text2 = (surroundValuesWithQuotes ? ConversionUtils.EnsureSurroundingQuotes(text2.Substring(0, text2.Length - 1)) : text2.Substring(0, text2.Length - 1));
				list.Add(text2);
				list.Add(surroundValuesWithQuotes ? "\"\"" : "");
			}
			else
			{
				if (surroundValuesWithQuotes)
				{
					text2 = ConversionUtils.EnsureSurroundingQuotes(text2);
				}
				list.Add(text2);
			}
			return list;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0007627C File Offset: 0x0007447C
		private static string EnsureSurroundingQuotes(string input)
		{
			if (input.Length > 0)
			{
				if (input[0] != '"')
				{
					input = "\"" + input;
				}
				if (input[input.Length - 1] != '"')
				{
					input += "\"";
				}
				return input;
			}
			return "\"\"";
		}
	}
}
