using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Text;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001780 RID: 6016
	public static class IDictionaryExtension
	{
		// Token: 0x0600C775 RID: 51061 RVA: 0x002AD8A0 File Offset: 0x002ABAA0
		public static string Render<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> subject)
		{
			subject = subject.ToReadOnlyList<KeyValuePair<TKey, TValue>>();
			if (!subject.Any<KeyValuePair<TKey, TValue>>())
			{
				return "<none>";
			}
			TextTableBuilder textTableBuilder = TextTableBuilder.Create(TextTableBorder.None, null, null);
			string text = "";
			int? num = new int?(0);
			TextTableBuilder textTableBuilder2 = textTableBuilder.AddIdentityColumn(text, null, num).AddStaticColumn(":", false, true, false);
			string text2 = "Key";
			int num2 = 0;
			int? num3 = new int?(60);
			bool flag = false;
			string text3 = null;
			string text4 = null;
			num = null;
			int? num4 = num;
			num = null;
			TextTableBuilder textTableBuilder3 = textTableBuilder2.AddColumn(text2, num2, num3, flag, text3, text4, num4, num).AddBorderColumn();
			string text5 = "Value";
			int num5 = 0;
			int? num6 = new int?(100);
			bool flag2 = false;
			string text6 = null;
			string text7 = null;
			num = null;
			int? num7 = num;
			num = null;
			ITextRowBuilder textRowBuilder = textTableBuilder3.AddColumn(text5, num5, num6, flag2, text6, text7, num7, num).AddHeadingRow().AddBorderRow();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in subject)
			{
				IEnumerable<object> enumerable = keyValuePair.Value as IEnumerable<object>;
				string text8;
				if (enumerable == null)
				{
					TValue value = keyValuePair.Value;
					text8 = value.ToString();
				}
				else
				{
					text8 = enumerable.Select((object o) => o.ToString()).ToJoinNewlineString();
				}
				string text9 = text8;
				ITextRowBuilder textRowBuilder2 = textRowBuilder;
				object[] array = new object[2];
				int num8 = 0;
				TKey key = keyValuePair.Key;
				array[num8] = key.ToString();
				array[1] = text9;
				textRowBuilder2.AddDataRow(array).AddBorderRow();
			}
			return textRowBuilder.Render();
		}

		// Token: 0x0600C776 RID: 51062 RVA: 0x002ADA30 File Offset: 0x002ABC30
		public static string Render<TKey, TValue>(this IDictionary<TKey, TValue> subject)
		{
			return subject.Render<TKey, TValue>();
		}

		// Token: 0x0600C777 RID: 51063 RVA: 0x002ADA38 File Offset: 0x002ABC38
		public static int KeyValueHashCode<TValue>(this IDictionary<int, IEnumerable<TValue>> dictionary) where TValue : IEquatable<TValue>
		{
			return dictionary.Select((KeyValuePair<int, IEnumerable<TValue>> kv) => kv.Value.OrderIndependentHashCode<TValue>() + 31 * kv.Key).OrderIndependentHashCode<int>();
		}

		// Token: 0x0600C778 RID: 51064 RVA: 0x002ADA64 File Offset: 0x002ABC64
		public static int KeyValueHashCode<TValue>(this IDictionary<IRow, IReadOnlyList<TValue>> dictionary) where TValue : IEquatable<TValue>
		{
			return dictionary.Select((KeyValuePair<IRow, IReadOnlyList<TValue>> kv) => kv.Value.OrderIndependentHashCode<TValue>() + 31 + kv.Key.GetHashCode()).OrderIndependentHashCode<int>();
		}

		// Token: 0x0600C779 RID: 51065 RVA: 0x002ADA90 File Offset: 0x002ABC90
		public static IDictionary<TKey, TValue> Set<TKey, TValue>(this IDictionary<TKey, TValue> subject, TKey key, TValue value)
		{
			if (subject == null)
			{
				return null;
			}
			subject[key] = value;
			return subject;
		}

		// Token: 0x0600C77A RID: 51066 RVA: 0x002ADAA0 File Offset: 0x002ABCA0
		public static Dictionary<TKey, TValue> Set<TKey, TValue>(this Dictionary<TKey, TValue> subject, TKey key, TValue value)
		{
			return (Dictionary<TKey, TValue>)subject.Set(key, value);
		}
	}
}
