using System;
using System.Collections.Generic;

namespace System.Web
{
	// Token: 0x0200000D RID: 13
	internal class PrefixContainer
	{
		// Token: 0x0600005E RID: 94 RVA: 0x0000324C File Offset: 0x0000144C
		internal PrefixContainer(ICollection<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this._originalValues = values;
			this._sortedValues = this._originalValues.ToArrayWithoutNulls<string>();
			Array.Sort<string>(this._sortedValues, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000328C File Offset: 0x0000148C
		internal bool ContainsPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (prefix.Length == 0)
			{
				return this._sortedValues.Length != 0;
			}
			PrefixContainer.PrefixComparer prefixComparer = new PrefixContainer.PrefixComparer(prefix);
			bool flag = Array.BinarySearch<string>(this._sortedValues, prefix, prefixComparer) > -1;
			if (!flag)
			{
				flag = Array.BinarySearch<string>(this._sortedValues, prefix + "[", prefixComparer) > -1;
			}
			return flag;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000032F4 File Offset: 0x000014F4
		internal IDictionary<string, string> GetKeysFromPrefix(string prefix)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in this._originalValues)
			{
				if (text != null && text.Length != prefix.Length)
				{
					if (prefix.Length == 0)
					{
						PrefixContainer.GetKeyFromEmptyPrefix(text, dictionary);
					}
					else if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
					{
						PrefixContainer.GetKeyFromNonEmptyPrefix(prefix, text, dictionary);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000337C File Offset: 0x0000157C
		private static void GetKeyFromEmptyPrefix(string entry, IDictionary<string, string> results)
		{
			int num = entry.IndexOf('.');
			int num2 = entry.IndexOf('[');
			int num3 = -1;
			if (num == -1)
			{
				if (num2 != -1)
				{
					num3 = num2;
				}
			}
			else if (num2 == -1)
			{
				num3 = num;
			}
			else
			{
				num3 = Math.Min(num, num2);
			}
			string text = ((num3 == -1) ? entry : entry.Substring(0, num3));
			results[text] = text;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000033D4 File Offset: 0x000015D4
		private static void GetKeyFromNonEmptyPrefix(string prefix, string entry, IDictionary<string, string> results)
		{
			int num = prefix.Length + 1;
			char c = entry[prefix.Length];
			string text;
			string text2;
			if (c != '.')
			{
				if (c != '[')
				{
					return;
				}
				int num2 = entry.IndexOf(']', num);
				if (num2 == -1)
				{
					return;
				}
				text = entry.Substring(num, num2 - num);
				text2 = entry.Substring(0, num2 + 1);
			}
			else
			{
				int num3 = entry.IndexOf('.', num);
				if (num3 == -1)
				{
					num3 = entry.Length;
				}
				text = entry.Substring(num, num3 - num);
				text2 = entry.Substring(0, num3);
			}
			if (!results.ContainsKey(text))
			{
				results.Add(text, text2);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003470 File Offset: 0x00001670
		internal static bool IsPrefixMatch(string prefix, string testString)
		{
			if (testString == null)
			{
				return false;
			}
			if (prefix.Length == 0)
			{
				return true;
			}
			if (prefix.Length > testString.Length)
			{
				return false;
			}
			if (!testString.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (testString.Length == prefix.Length)
			{
				return true;
			}
			char c = testString[prefix.Length];
			return c == '.' || c == '[';
		}

		// Token: 0x0400000D RID: 13
		private readonly ICollection<string> _originalValues;

		// Token: 0x0400000E RID: 14
		private readonly string[] _sortedValues;

		// Token: 0x02000193 RID: 403
		private class PrefixComparer : IComparer<string>
		{
			// Token: 0x06000A37 RID: 2615 RVA: 0x0001A74E File Offset: 0x0001894E
			public PrefixComparer(string prefix)
			{
				this._prefix = prefix;
			}

			// Token: 0x06000A38 RID: 2616 RVA: 0x0001A760 File Offset: 0x00018960
			public int Compare(string x, string y)
			{
				string text = ((x == this._prefix) ? y : x);
				if (PrefixContainer.IsPrefixMatch(this._prefix, text))
				{
					return 0;
				}
				return StringComparer.OrdinalIgnoreCase.Compare(x, y);
			}

			// Token: 0x040002C3 RID: 707
			private string _prefix;
		}
	}
}
