using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies
{
	// Token: 0x02000701 RID: 1793
	internal class KeywordLearner
	{
		// Token: 0x060026F5 RID: 9973 RVA: 0x0006E0A1 File Offset: 0x0006C2A1
		public KeywordLearner(ProgramNode pp0, List<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>> phraseDict)
		{
			this._initialProgram = pp0;
			this._phraseDict = phraseDict;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0006E0B8 File Offset: 0x0006C2B8
		private void Translate()
		{
			this._keywords = new List<string>();
			this._candidatePrograms = new List<Record<ProgramNode, int>>();
			this._rejectedPrograms = new List<Record<ProgramNode, int>>();
			char[] separators = new char[] { ',', ' ', ';' };
			int num = 0;
			int workingIndex = 0;
			Func<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>, bool> <>9__0;
			Func<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>, bool> <>9__1;
			Func<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>, int> <>9__2;
			for (;;)
			{
				IEnumerable<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>> phraseDict = this._phraseDict;
				Func<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode> x) => new Regex(x.Item1).Match(this._sentence, workingIndex).Success);
				}
				IEnumerable<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>> enumerable = phraseDict.Where(func);
				Func<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>, bool> func2;
				if ((func2 = <>9__1) == null)
				{
					func2 = (<>9__1 = (Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode> x) => this._sentence.Slice(new int?(0), new int?(new Regex(x.Item1).Match(this._sentence, workingIndex).Index), 1).Count((char y) => y == '"') % 2 == 0 && separators.Contains(this._sentence[new Regex(x.Item1).Match(this._sentence, workingIndex).Index - 1]) && separators.Contains(this._sentence[new Regex(x.Item1).Match(this._sentence, workingIndex).Index + new Regex(x.Item1).Match(this._sentence, workingIndex).Length]));
				}
				IEnumerable<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>> enumerable2 = enumerable.Where(func2);
				Func<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>, int> func3;
				if ((func3 = <>9__2) == null)
				{
					func3 = (<>9__2 = (Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode> x) => new Regex(x.Item1).Match(this._sentence, workingIndex).Index);
				}
				Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>[] array = enumerable2.OrderBy(func3).ToArray<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>>();
				if (array.Length == 0)
				{
					break;
				}
				Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode> record = array[0];
				Match match = new Regex(record.Item1).Match(this._sentence, workingIndex);
				if (match.Success)
				{
					this._keywords.Add(match.Value);
					ProgramNode programNode = record.Item3.Clone();
					Record<ProgramNode, int, Hole>[] array2 = programNode.Holes.ToArray<Record<ProgramNode, int, Hole>>();
					for (int i = 0; i < record.Item2.Length; i++)
					{
						Record<ProgramNode, int, Hole> record2 = array2[i];
						record2.Item1.Children[record2.Item2] = record.Item2[i](match.Groups);
					}
					this._candidatePrograms.Add(new Record<ProgramNode, int>(programNode, num++));
					workingIndex = match.Index + match.Length;
				}
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0006E274 File Offset: 0x0006C474
		public string[] Synthesize(string sentence)
		{
			this._sentence = " " + sentence + " ";
			this.Translate();
			this.unionFind = new KeywordLearner.UnionFind(this._keywords.Count);
			List<string> list = new List<string>();
			bool flag = true;
			while (this._initialProgram.Holes.ToArray<Record<ProgramNode, int, Hole>>().Length != 0 && flag)
			{
				this._holeIndex = 0;
				string[] array = this.SynthesizeProgram();
				list.AddRange(array);
				flag = array.Length != 0;
				this._candidatePrograms = new List<Record<ProgramNode, int>>(this._rejectedPrograms).OrderBy((Record<ProgramNode, int> x) => -x.Item2).ToList<Record<ProgramNode, int>>();
				this._rejectedPrograms.Clear();
			}
			return list.ToArray();
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0006E33C File Offset: 0x0006C53C
		public string[] Synthesize(string sentence, int hole)
		{
			this._holeIndex = hole;
			this._sentence = " " + sentence + " ";
			this.Translate();
			this.unionFind = new KeywordLearner.UnionFind(this._keywords.Count);
			return this.SynthesizeProgram();
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0006E388 File Offset: 0x0006C588
		private string[] SynthesizeProgram()
		{
			while (this._candidatePrograms.Count > 1)
			{
				ProgramNode item = this._candidatePrograms[0].Item1;
				int item2 = this._candidatePrograms[0].Item2;
				this._candidatePrograms.RemoveAt(0);
				bool flag = false;
				int num = 0;
				while (num < this._candidatePrograms.Count && !flag)
				{
					ProgramNode item3 = this._candidatePrograms[num].Item1;
					int item4 = this._candidatePrograms[num].Item2;
					List<Record<ProgramNode, int, Hole>> list = item3.Holes.ToList<Record<ProgramNode, int, Hole>>();
					int num2 = 0;
					while (num2 < list.Count && !flag)
					{
						if (!(item.Symbol != list[num2].Item3.Symbol))
						{
							list[num2].Item1.Children[list[num2].Item2] = item;
							this.unionFind.Union(item2, item4);
							flag = true;
						}
						num2++;
					}
					num++;
				}
				if (!flag && item.Symbol == this._initialProgram.Holes.ToArray<Record<ProgramNode, int, Hole>>()[this._holeIndex].Item3.Symbol && this._candidatePrograms.Where((Record<ProgramNode, int> x) => x.Item1.Symbol == this._initialProgram.Holes.ToArray<Record<ProgramNode, int, Hole>>()[this._holeIndex].Item3.Symbol).ToArray<Record<ProgramNode, int>>().Length == 0)
				{
					this._candidatePrograms.Add(new Record<ProgramNode, int>(item, item2));
				}
				else if (!flag)
				{
					this._rejectedPrograms.Add(new Record<ProgramNode, int>(item, item2));
				}
			}
			Record<ProgramNode, int, Hole> record = this._initialProgram.Holes.ToArray<Record<ProgramNode, int, Hole>>()[this._holeIndex];
			if (this._candidatePrograms.Count != 1 || record.Item3.Symbol != this._candidatePrograms[0].Item1.Symbol)
			{
				return new string[0];
			}
			record.Item1.Children[record.Item2] = this._candidatePrograms[0].Item1;
			return this._keywords.Where((string k) => this.unionFind.Find(this._keywords.IndexOf(k)) == this.unionFind.Find(this._candidatePrograms[0].Item2)).ToArray<string>();
		}

		// Token: 0x040012CD RID: 4813
		private ProgramNode _initialProgram;

		// Token: 0x040012CE RID: 4814
		private string _sentence;

		// Token: 0x040012CF RID: 4815
		private List<string> _keywords;

		// Token: 0x040012D0 RID: 4816
		private List<Record<string, Func<GroupCollection, ProgramNode>[], ProgramNode>> _phraseDict;

		// Token: 0x040012D1 RID: 4817
		private int _holeIndex;

		// Token: 0x040012D2 RID: 4818
		private List<Record<ProgramNode, int>> _candidatePrograms;

		// Token: 0x040012D3 RID: 4819
		private List<Record<ProgramNode, int>> _rejectedPrograms;

		// Token: 0x040012D4 RID: 4820
		private KeywordLearner.UnionFind unionFind;

		// Token: 0x02000702 RID: 1794
		private class UnionFind
		{
			// Token: 0x060026FC RID: 9980 RVA: 0x0006E621 File Offset: 0x0006C821
			public UnionFind(int nodeCount)
			{
				this.parent = Enumerable.Range(0, nodeCount).ToArray<int>();
			}

			// Token: 0x060026FD RID: 9981 RVA: 0x0006E63B File Offset: 0x0006C83B
			public int Find(int i)
			{
				if (this.parent[i] != i)
				{
					return this.Find(this.parent[i]);
				}
				return i;
			}

			// Token: 0x060026FE RID: 9982 RVA: 0x0006E658 File Offset: 0x0006C858
			public void Union(int i, int j)
			{
				this.parent[this.Find(i)] = this.Find(j);
			}

			// Token: 0x040012D5 RID: 4821
			private int[] parent;
		}
	}
}
