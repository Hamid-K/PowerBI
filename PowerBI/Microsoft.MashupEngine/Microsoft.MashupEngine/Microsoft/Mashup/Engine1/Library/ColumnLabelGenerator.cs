using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x0200023B RID: 571
	internal static class ColumnLabelGenerator
	{
		// Token: 0x060018EE RID: 6382 RVA: 0x00030E58 File Offset: 0x0002F058
		public static Keys GenerateKeys(int columnCount)
		{
			if (columnCount >= 0)
			{
				KeysBuilder keysBuilder = new KeysBuilder(columnCount);
				for (int i = 1; i <= columnCount; i++)
				{
					keysBuilder.Add("Column" + i.ToString(CultureInfo.InvariantCulture));
				}
				return keysBuilder.ToKeys();
			}
			throw ValueException.NewExpressionError<Message0>(Strings.TableType_FromValue, NumberValue.New(columnCount), null);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00030EB4 File Offset: 0x0002F0B4
		public static Keys GenerateKeys(string[] initialKeys, int columnCount)
		{
			if (initialKeys == null || initialKeys.Length == 0)
			{
				return ColumnLabelGenerator.GenerateKeys(columnCount);
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			int num = 0;
			while (num < initialKeys.Length && num < columnCount)
			{
				string text = initialKeys[num] ?? "";
				string text2 = (dictionary.ContainsKey(text) ? ColumnLabelGenerator.PickUniqueKey(dictionary, text, 2) : text);
				dictionary.Add(text2, num);
				num++;
			}
			for (int i = dictionary.Count; i < columnCount; i++)
			{
				dictionary.Add(ColumnLabelGenerator.PickUniqueKey(dictionary, "Column", i + 1), i);
			}
			string[] array = new string[dictionary.Count];
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				array[keyValuePair.Value] = keyValuePair.Key;
			}
			return Keys.New(array);
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00030FA0 File Offset: 0x0002F1A0
		public static string PickUniqueKey(Dictionary<string, int> labels, string desiredKey, int startingSuffix)
		{
			int num = startingSuffix;
			string text = desiredKey + num.ToString();
			while (labels.ContainsKey(text))
			{
				num++;
				text = desiredKey + num.ToString();
			}
			return text;
		}

		// Token: 0x040006A2 RID: 1698
		private const string columnConstant = "Column";
	}
}
