using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.FilterValueCompletion
{
	// Token: 0x02001E2A RID: 7722
	internal class PrefixMassTreeVisitor : PrefixSearchTreeVisitor<char, string, StringSubSequence, BitArray, TrieNode<char, string, StringSubSequence, BitArray>, TrieEdge<char, string, StringSubSequence, BitArray>, BitArray>
	{
		// Token: 0x17002AD0 RID: 10960
		// (get) Token: 0x060101F4 RID: 66036 RVA: 0x00375772 File Offset: 0x00373972
		public int LowThreshold { get; }

		// Token: 0x17002AD1 RID: 10961
		// (get) Token: 0x060101F5 RID: 66037 RVA: 0x0037577A File Offset: 0x0037397A
		public int HighThreshold { get; }

		// Token: 0x17002AD2 RID: 10962
		// (get) Token: 0x060101F6 RID: 66038 RVA: 0x00375782 File Offset: 0x00373982
		private IEnumerable<ValueAndCount<string>> Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x060101F7 RID: 66039 RVA: 0x0037578C File Offset: 0x0037398C
		private string GetPrefixString(TrieNode<char, string, StringSubSequence, BitArray> node)
		{
			List<string> list = new List<string>();
			while (node.EdgeToParent != null)
			{
				list.Add(node.EdgeToParent.MatchedPrefix.Value);
				node = node.EdgeToParent.Parent;
			}
			list.Reverse();
			return string.Concat(list);
		}

		// Token: 0x060101F8 RID: 66040 RVA: 0x003757D8 File Offset: 0x003739D8
		public override BitArray Visit(TrieNode<char, string, StringSubSequence, BitArray> node)
		{
			TrieLeafNode<char, string, StringSubSequence, BitArray> trieLeafNode = node as TrieLeafNode<char, string, StringSubSequence, BitArray>;
			if (trieLeafNode != null)
			{
				int num = trieLeafNode.Value.BitCount();
				if (num >= this.LowThreshold && num <= this.HighThreshold)
				{
					this._result.Add(new ValueAndCount<string>(this.GetPrefixString(node), num));
				}
				return trieLeafNode.Value;
			}
			BitArray bitArray = null;
			bool flag = true;
			foreach (TrieEdge<char, string, StringSubSequence, BitArray> trieEdge in node.GetEdges().ToList<TrieEdge<char, string, StringSubSequence, BitArray>>())
			{
				BitArray bitArray2 = trieEdge.Child.Accept<BitArray>(this);
				if (bitArray2.BitCount() < this.LowThreshold)
				{
					flag = false;
				}
				if (bitArray == null)
				{
					bitArray = new BitArray(bitArray2.Length);
				}
				bitArray.Or(bitArray2);
			}
			int num2 = bitArray.BitCount();
			if (!flag && num2 >= this.LowThreshold && num2 <= this.HighThreshold)
			{
				this._result.Add(new ValueAndCount<string>(this.GetPrefixString(node), num2));
			}
			return bitArray;
		}

		// Token: 0x060101F9 RID: 66041 RVA: 0x003758E4 File Offset: 0x00373AE4
		public PrefixMassTreeVisitor(int lowThreshold, int highThreshold)
		{
			if (lowThreshold > highThreshold)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("lowThreshold must be less than or equal to highThreshold", Array.Empty<object>())));
			}
			this.LowThreshold = lowThreshold;
			this.HighThreshold = highThreshold;
			this._result = new List<ValueAndCount<string>>();
		}

		// Token: 0x060101FA RID: 66042 RVA: 0x00375924 File Offset: 0x00373B24
		public static IEnumerable<ValueAndCount<string>> GetLongestPrefixes(int lowThreshold, int highThreshold, PrefixMassTree tree)
		{
			PrefixMassTreeVisitor prefixMassTreeVisitor = new PrefixMassTreeVisitor(lowThreshold, highThreshold);
			tree.Accept<BitArray>(prefixMassTreeVisitor);
			return prefixMassTreeVisitor.Result;
		}

		// Token: 0x060101FB RID: 66043 RVA: 0x00375947 File Offset: 0x00373B47
		public static IEnumerable<ValueAndCount<string>> GetLongestPrefixes(PrefixMassTree tree)
		{
			return PrefixMassTreeVisitor.GetLongestPrefixes(0, int.MaxValue, tree);
		}

		// Token: 0x04006171 RID: 24945
		private readonly List<ValueAndCount<string>> _result;
	}
}
