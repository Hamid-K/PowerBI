using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.FilterValueCompletion
{
	// Token: 0x02001E20 RID: 7712
	internal class CompressedStringTrie : CompressedTrie<char, string, StringSubSequence, List<ValueAndCount<string>>>
	{
		// Token: 0x17002ACC RID: 10956
		// (get) Token: 0x060101C9 RID: 65993 RVA: 0x00025475 File Offset: 0x00023675
		protected override StringSubSequence EmptySubSequence
		{
			get
			{
				return StringSubSequence.Empty;
			}
		}

		// Token: 0x060101CA RID: 65994 RVA: 0x000254D8 File Offset: 0x000236D8
		protected override StringSubSequence SubSequenceForSequence(string sequence)
		{
			return StringSubSequence.Create(sequence.Normalize());
		}

		// Token: 0x060101CB RID: 65995 RVA: 0x00374EFF File Offset: 0x003730FF
		protected override StringSubSequence TraversalSubSequenceForKey(string key)
		{
			return StringSubSequence.Create(key.Normalize().ToLowerInvariant());
		}

		// Token: 0x060101CC RID: 65996 RVA: 0x00374F14 File Offset: 0x00373114
		public IEnumerable<ValueAndCount<string>> Suggest(string prefix)
		{
			List<ValueAndCount<string>> list = base.PrefixLookup(prefix).SelectMany((KeyValuePair<string, List<ValueAndCount<string>>> kvp) => kvp.Value).ToList<ValueAndCount<string>>();
			int num = int.MinValue;
			int minLength = int.MaxValue;
			int maxCount = int.MinValue;
			foreach (ValueAndCount<string> valueAndCount in list)
			{
				num = Math.Max(num, valueAndCount.Value.Length);
				minLength = Math.Min(minLength, valueAndCount.Value.Length);
				maxCount = Math.Max(num, valueAndCount.Count);
			}
			int lengthSpan = num - minLength;
			return list.OrderByDescending(delegate(ValueAndCount<string> cr)
			{
				double num2 = (double)(cr.Value.Length - minLength) / (double)lengthSpan;
				return 0.3 * num2 + 0.7 * ((double)cr.Count / (double)maxCount);
			});
		}

		// Token: 0x060101CD RID: 65997 RVA: 0x00375014 File Offset: 0x00373214
		public void Add(string key, ValueAndCount<string> value)
		{
			base.GetOrCreate(key, () => new List<ValueAndCount<string>>()).Add(value);
		}

		// Token: 0x04006148 RID: 24904
		private const double WeightForLength = 0.3;

		// Token: 0x04006149 RID: 24905
		private const double WeightForCount = 0.7;
	}
}
