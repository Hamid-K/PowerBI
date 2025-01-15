using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.FilterValueCompletion
{
	// Token: 0x02001E19 RID: 7705
	public class ColumnProfile
	{
		// Token: 0x17002AC3 RID: 10947
		// (get) Token: 0x060101AD RID: 65965 RVA: 0x00374B1C File Offset: 0x00372D1C
		public string ColumnName { get; }

		// Token: 0x17002AC4 RID: 10948
		// (get) Token: 0x060101AE RID: 65966 RVA: 0x00374B24 File Offset: 0x00372D24
		public IReadOnlyList<string> ColumnSample
		{
			get
			{
				return this._columnSample;
			}
		}

		// Token: 0x17002AC5 RID: 10949
		// (get) Token: 0x060101AF RID: 65967 RVA: 0x00374B2C File Offset: 0x00372D2C
		public int SampleSize
		{
			get
			{
				return this.ColumnSample.Count;
			}
		}

		// Token: 0x17002AC6 RID: 10950
		// (get) Token: 0x060101B0 RID: 65968 RVA: 0x00374B3C File Offset: 0x00372D3C
		public IEnumerable<ValueAndCount<string>> Prefixes
		{
			get
			{
				List<ValueAndCount<string>> list;
				if ((list = this._prefixes) == null)
				{
					list = (this._prefixes = this.ComputePrefixes());
				}
				return list;
			}
		}

		// Token: 0x17002AC7 RID: 10951
		// (get) Token: 0x060101B1 RID: 65969 RVA: 0x00374B64 File Offset: 0x00372D64
		public IEnumerable<ValueAndCount<string>> Suffixes
		{
			get
			{
				List<ValueAndCount<string>> list;
				if ((list = this._suffixes) == null)
				{
					list = (this._suffixes = this.ComputeSuffixes());
				}
				return list;
			}
		}

		// Token: 0x17002AC8 RID: 10952
		// (get) Token: 0x060101B2 RID: 65970 RVA: 0x00374B8C File Offset: 0x00372D8C
		public IEnumerable<ValueAndCount<string>> Strings
		{
			get
			{
				List<ValueAndCount<string>> list;
				if ((list = this._strings) == null)
				{
					list = (this._strings = this.ComputeStrings());
				}
				return list;
			}
		}

		// Token: 0x17002AC9 RID: 10953
		// (get) Token: 0x060101B3 RID: 65971 RVA: 0x00374BB4 File Offset: 0x00372DB4
		public IEnumerable<ValueAndCount<string>> Substrings
		{
			get
			{
				List<ValueAndCount<string>> list;
				if ((list = this._substrings) == null)
				{
					list = (this._substrings = this.ComputeSubstrings());
				}
				return list;
			}
		}

		// Token: 0x060101B4 RID: 65972 RVA: 0x00374BDC File Offset: 0x00372DDC
		private List<ValueAndCount<string>> ComputePrefixes()
		{
			PrefixMassTree prefixMassTree = new PrefixMassTree();
			int numSamples = this.ColumnSample.Count;
			Func<BitArray> <>9__0;
			for (int i = 0; i < numSamples; i++)
			{
				string text = this.ColumnSample[i];
				PrefixSearchTree<char, string, StringSubSequence, BitArray, TrieNode<char, string, StringSubSequence, BitArray>, TrieEdge<char, string, StringSubSequence, BitArray>> prefixSearchTree = prefixMassTree;
				string text2 = text;
				Func<BitArray> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = () => new BitArray(numSamples, false));
				}
				prefixSearchTree.GetOrCreate(text2, func).Set(i, true);
			}
			return PrefixMassTreeVisitor.GetLongestPrefixes(prefixMassTree).Distinct<ValueAndCount<string>>().ToList<ValueAndCount<string>>();
		}

		// Token: 0x060101B5 RID: 65973 RVA: 0x00374C63 File Offset: 0x00372E63
		private List<ValueAndCount<string>> ComputeSuffixes()
		{
			return new StringSuffixTree(this.ColumnSample).LookupPrefix("", 0.0, 1.0).ToList<ValueAndCount<string>>();
		}

		// Token: 0x060101B6 RID: 65974 RVA: 0x00374C94 File Offset: 0x00372E94
		private List<ValueAndCount<string>> ComputeStrings()
		{
			return (from s in this.ColumnSample
				group s by s into g
				select new ValueAndCount<string>(g.Key, g.Count<string>())).ToList<ValueAndCount<string>>();
		}

		// Token: 0x060101B7 RID: 65975 RVA: 0x00374CF4 File Offset: 0x00372EF4
		private List<ValueAndCount<string>> ComputeSubstrings()
		{
			return new StringSuffixTree(this.ColumnSample).FindCommonSubSequences(0.0, 1.0).ToList<ValueAndCount<string>>();
		}

		// Token: 0x060101B8 RID: 65976 RVA: 0x00374D20 File Offset: 0x00372F20
		private ColumnProfile(string columnName, IEnumerable<string> columnSample, CancellationToken cancel)
		{
			this.ColumnName = columnName;
			this._columnSample = columnSample.Where((string s) => !string.IsNullOrEmpty(s)).ToList<string>();
			if (this._columnSample.Count > 1024)
			{
				int divisor = this._columnSample.Count / 512;
				this._columnSample = this._columnSample.Where((string s, int i) => i % divisor == 0).ToList<string>();
			}
			else if (this._columnSample.Count > 512)
			{
				this._columnSample = this._columnSample.Take(512).ToList<string>();
			}
			this._prefixes = null;
			this._suffixes = null;
			this._substrings = null;
			this._strings = null;
			cancel.ThrowIfCancellationRequested();
			this._strings = this.ComputeStrings();
			cancel.ThrowIfCancellationRequested();
			this._substrings = this.ComputeSubstrings();
			cancel.ThrowIfCancellationRequested();
			this._prefixes = this.ComputePrefixes();
			cancel.ThrowIfCancellationRequested();
			this._suffixes = this.ComputeSuffixes();
		}

		// Token: 0x060101B9 RID: 65977 RVA: 0x00374E52 File Offset: 0x00373052
		public static Task<ColumnProfile> CreateAsync(string columnName, IEnumerable<string> columnSample, CancellationToken cancel = default(CancellationToken))
		{
			return Task.Run<ColumnProfile>(() => new ColumnProfile(columnName, columnSample, cancel));
		}

		// Token: 0x04006127 RID: 24871
		private const int MaxSamples = 512;

		// Token: 0x04006128 RID: 24872
		private readonly List<string> _columnSample;

		// Token: 0x04006129 RID: 24873
		private List<ValueAndCount<string>> _prefixes;

		// Token: 0x0400612A RID: 24874
		private List<ValueAndCount<string>> _suffixes;

		// Token: 0x0400612B RID: 24875
		private List<ValueAndCount<string>> _substrings;

		// Token: 0x0400612C RID: 24876
		private List<ValueAndCount<string>> _strings;
	}
}
