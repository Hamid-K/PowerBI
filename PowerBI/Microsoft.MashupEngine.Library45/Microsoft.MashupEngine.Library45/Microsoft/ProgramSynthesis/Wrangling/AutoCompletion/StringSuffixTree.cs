using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.AutoCompletion
{
	// Token: 0x02000249 RID: 585
	public class StringSuffixTree : SuffixTree<char, string, StringSubSequence>
	{
		// Token: 0x06000C74 RID: 3188 RVA: 0x000254F8 File Offset: 0x000236F8
		public StringSuffixTree(IEnumerable<string> inputs)
			: base(inputs)
		{
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002550C File Offset: 0x0002370C
		private string CombinedStringCache
		{
			get
			{
				string text;
				if ((text = this._combinedStringCache) == null)
				{
					text = (this._combinedStringCache = this._combinedStrings.ToString());
				}
				return text;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00025537 File Offset: 0x00023737
		protected override int SumOfInputLengths
		{
			get
			{
				return this._combinedStrings.Length;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00025544 File Offset: 0x00023744
		protected override char Terminator
		{
			get
			{
				return char.MaxValue;
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002554C File Offset: 0x0002374C
		protected override string OnNewInput(string input)
		{
			string text = input + this.Terminator.ToString();
			this._combinedStrings.Append(text);
			this._combinedStringCache = null;
			return input;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00025584 File Offset: 0x00023784
		protected override string RecoverInput(List<StringSubSequence> parts)
		{
			string text = string.Concat(parts.Select((StringSubSequence p) => p.Value));
			if (text.Length < 1 || text[text.Length - 1] != this.Terminator)
			{
				return text;
			}
			return text.Substring(0, text.Length - 1);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000255EC File Offset: 0x000237EC
		protected override char At(int index)
		{
			return this._combinedStrings[index];
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x000255FA File Offset: 0x000237FA
		protected override StringSubSequence SliceFromCombinedInputs(int start, int end)
		{
			return StringSubSequence.Create(this.CombinedStringCache, (uint)start, (uint)end);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00025609 File Offset: 0x00023809
		protected override List<ValueAndCount<string>> FilterResults(List<ValueAndCount<string>> results)
		{
			return results.Where((ValueAndCount<string> s) => !string.IsNullOrEmpty(s.Value)).Distinct<ValueAndCount<string>>().ToList<ValueAndCount<string>>();
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002563A File Offset: 0x0002383A
		public IReadOnlyList<ValueAndCount<string>> LookupPrefix(string prefix, double minFraction, double maxFraction)
		{
			return base.LookupPrefix(prefix, minFraction, maxFraction, null);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00025646 File Offset: 0x00023846
		public IReadOnlyList<ValueAndCount<string>> LookupPrefixIgnoreCase(string prefix, double minFraction, double maxFraction)
		{
			return base.LookupPrefix(prefix, minFraction, maxFraction, delegate(char c)
			{
				char c2 = char.ToLowerInvariant(c);
				char c3 = char.ToUpperInvariant(c);
				if (c == c2)
				{
					return c3;
				}
				if (c != c3)
				{
					return c;
				}
				return c2;
			});
		}

		// Token: 0x0400062C RID: 1580
		private readonly StringBuilder _combinedStrings = new StringBuilder();

		// Token: 0x0400062D RID: 1581
		private string _combinedStringCache;
	}
}
