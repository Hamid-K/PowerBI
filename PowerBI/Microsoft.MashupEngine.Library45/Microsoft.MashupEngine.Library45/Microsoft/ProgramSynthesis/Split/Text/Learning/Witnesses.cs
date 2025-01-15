using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Split.Text.Build;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Split.Text.Learning
{
	// Token: 0x020013A1 RID: 5025
	public class Witnesses : DomainGuidedCBSLearningLogic
	{
		// Token: 0x17001AB6 RID: 6838
		// (get) Token: 0x06009C05 RID: 39941 RVA: 0x0020F01C File Offset: 0x0020D21C
		// (set) Token: 0x06009C06 RID: 39942 RVA: 0x0020F024 File Offset: 0x0020D224
		public bool LearnZeroLengthDelimiters { get; private set; }

		// Token: 0x06009C07 RID: 39943 RVA: 0x0020F02D File Offset: 0x0020D22D
		public Witnesses(Grammar grammar, Witnesses.Options options = null)
			: this(grammar, options, null)
		{
		}

		// Token: 0x06009C08 RID: 39944 RVA: 0x0020F038 File Offset: 0x0020D238
		public Witnesses(Grammar grammar, Witnesses.Options options, DomainGuidedCBS.Config config)
			: base(grammar, config)
		{
			this._options = options ?? new Witnesses.Options();
			this._build = GrammarBuilders.Instance(grammar);
			this.ExampleConstraints = null;
		}

		// Token: 0x17001AB7 RID: 6839
		// (get) Token: 0x06009C09 RID: 39945 RVA: 0x0020F0AF File Offset: 0x0020D2AF
		// (set) Token: 0x06009C0A RID: 39946 RVA: 0x0020F0B7 File Offset: 0x0020D2B7
		public Dictionary<int, Dictionary<string, string>> ExampleConstraints { get; private set; }

		// Token: 0x17001AB8 RID: 6840
		// (get) Token: 0x06009C0B RID: 39947 RVA: 0x0020F0C0 File Offset: 0x0020D2C0
		private DomainGuidedCBS.Config DomainGuidedSynthesisConfig { get; }

		// Token: 0x06009C0C RID: 39948 RVA: 0x0020F0C8 File Offset: 0x0020D2C8
		private IEnumerable<Match> GetRegexMatches(Regex r, string s)
		{
			return this._regexMatchesCache.LookupOrCompute(r, (Regex _) => new ConcurrentLruCache<string, IReadOnlyList<Match>>(4096, null, null, null)).LookupOrCompute(s, (string _) => r.NonCachingMatches(s).ToArray<Match>());
		}

		// Token: 0x06009C0D RID: 39949 RVA: 0x0020F130 File Offset: 0x0020D330
		public void AddNthExampleConstraint(string input, int index, string example)
		{
			if (this.ExampleConstraints == null)
			{
				this.ExampleConstraints = new Dictionary<int, Dictionary<string, string>>();
			}
			Dictionary<string, string> orCreateValue = this.ExampleConstraints.GetOrCreateValue(index);
			if (orCreateValue.ContainsKey(input) && orCreateValue[input] != example)
			{
				throw new Exception("Conflicting example constraints provided");
			}
			orCreateValue[input] = example;
		}

		// Token: 0x06009C0E RID: 39950 RVA: 0x0020F188 File Offset: 0x0020D388
		private void ComputeMaxNumMatchFrequencies(IEnumerable<MatchRecord[]> delimiterList)
		{
			this._learnedDelimitersMaxFrequencyMap = new Dictionary<MatchRecord[], Record<int, int>>();
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (MatchRecord[] array in delimiterList)
			{
				dictionary.Clear();
				foreach (MatchRecord matchRecord in array)
				{
					dictionary[matchRecord.NumMatches] = dictionary.GetOrAdd(matchRecord.NumMatches, 0) + 1;
				}
				KeyValuePair<int, int> keyValuePair = dictionary.ArgMax((KeyValuePair<int, int> kvp) => kvp.Value);
				this._learnedDelimitersMaxFrequencyMap[array] = Record.Create<int, int>(keyValuePair.Value, keyValuePair.Key);
			}
		}

		// Token: 0x06009C0F RID: 39951 RVA: 0x0020F268 File Offset: 0x0020D468
		public override object[][][] Ranker(object[][] states, object[][] nonRankingStates, object[][] inputRegions)
		{
			string[] inputStrings = inputRegions.Select((object[] s) => (from StringRegion r in s
				select r.Value).ToArray<string>()).First<string[]>();
			MatchRecord[][] array = (from s in states
				select s.Cast<MatchRecord>().ToArray<MatchRecord>() into d
				where d.Any((MatchRecord m) => m.NumMatches > 0)
				select d).ToArray<MatchRecord[]>();
			this._learnedDelimiters = array;
			if (array.Length == 0)
			{
				return new MatchRecord[0][][];
			}
			int numInputs = inputStrings.Length;
			this.ComputeMaxNumMatchFrequencies(array);
			MatchRecord[][] array2 = array.Where((MatchRecord[] d) => d[0].NumMatches > 0 && d.All((MatchRecord m) => m.NumMatches == d[0].NumMatches)).ToArray<MatchRecord[]>();
			MatchRecord[][] array3 = array2;
			if (!this.LearnZeroLengthDelimiters && !this._options.LearnSimpleSingleDelimiterPrograms)
			{
				MatchRecord[][] array4 = array2.Where((MatchRecord[] d) => d.All((MatchRecord m) => m.StartIndexes.SequenceEqual(m.EndIndexes)) || !this.HasSpecialCharAdjacentMatches(d, inputStrings)).ToArray<MatchRecord[]>();
				if (array4.Length != 0)
				{
					array3 = array4;
				}
			}
			if (array3.Length != 0)
			{
				return this.GetBestAlignmentSets(array3, inputStrings, null);
			}
			if (this._options.LearnSimpleSingleDelimiterPrograms)
			{
				return new MatchRecord[0][][];
			}
			int num = 0;
			Dictionary<MatchRecord[], int> dictionary = new Dictionary<MatchRecord[], int>();
			Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
			Func<int, bool> <>9__10;
			foreach (MatchRecord[] array6 in array)
			{
				Witnesses.<>c__DisplayClass40_2 CS$<>8__locals2 = new Witnesses.<>c__DisplayClass40_2();
				dictionary2.Clear();
				foreach (MatchRecord matchRecord in array6)
				{
					if (matchRecord.NumMatches != 0)
					{
						if (!dictionary2.ContainsKey(matchRecord.NumMatches))
						{
							dictionary2[matchRecord.NumMatches] = 1;
						}
						else
						{
							Dictionary<int, int> dictionary3 = dictionary2;
							int numMatches = matchRecord.NumMatches;
							dictionary3[numMatches]++;
						}
					}
				}
				Witnesses.<>c__DisplayClass40_2 CS$<>8__locals3 = CS$<>8__locals2;
				IEnumerable<int> values = dictionary2.Values;
				Func<int, bool> func;
				if ((func = <>9__10) == null)
				{
					func = (<>9__10 = (int v) => v < numInputs);
				}
				CS$<>8__locals3.maxMatchFrequency = values.Where(func).Max();
				if (CS$<>8__locals2.maxMatchFrequency >= num)
				{
					if (CS$<>8__locals2.maxMatchFrequency > num)
					{
						dictionary.Clear();
					}
					num = CS$<>8__locals2.maxMatchFrequency;
					dictionary[array6] = dictionary2.First((KeyValuePair<int, int> kvp) => kvp.Value == CS$<>8__locals2.maxMatchFrequency).Key;
				}
			}
			KeyValuePair<MatchRecord[], int> keyValuePair = dictionary.OrderByDescending((KeyValuePair<MatchRecord[], int> kvp) => kvp.Value).First<KeyValuePair<MatchRecord[], int>>();
			List<string> list = new List<string>();
			int i;
			int k;
			for (i = 0; i < keyValuePair.Key.Length; i = k + 1)
			{
				if (keyValuePair.Key[i].NumMatches == keyValuePair.Value)
				{
					foreach (KeyValuePair<MatchRecord[], int> keyValuePair2 in dictionary.Where((KeyValuePair<MatchRecord[], int> kvp) => kvp.Key[i].NumMatches != kvp.Value).ToArray<KeyValuePair<MatchRecord[], int>>())
					{
						dictionary.Remove(keyValuePair2.Key);
					}
					list.Add(inputStrings[i]);
				}
				k = i;
			}
			Dictionary<MatchRecord[], MatchRecord[]> dictionary4 = new Dictionary<MatchRecord[], MatchRecord[]>();
			using (Dictionary<MatchRecord[], int>.Enumerator enumerator = dictionary.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<MatchRecord[], int> kvp = enumerator.Current;
					MatchRecord[] array9 = kvp.Key.Where((MatchRecord m) => m.NumMatches == kvp.Value).ToArray<MatchRecord>();
					dictionary4[array9] = kvp.Key;
				}
			}
			return this.GetBestAlignmentSets(dictionary4.Keys.ToArray<MatchRecord[]>(), list.ToArray(), dictionary4);
		}

		// Token: 0x06009C10 RID: 39952 RVA: 0x0020F684 File Offset: 0x0020D884
		private MatchRecord[][][] GetBestAlignmentSets(MatchRecord[][] candidateLists, string[] inputStrings, Dictionary<MatchRecord[], MatchRecord[]> projectionMap = null)
		{
			Dictionary<MatchRecord[], HashSet<MatchRecord[]>> dictionary = new Dictionary<MatchRecord[], HashSet<MatchRecord[]>>();
			int k;
			for (k = 0; k < candidateLists.Length; k++)
			{
				MatchRecord[] array = candidateLists[k];
				HashSet<MatchRecord[]> orCreateValue = dictionary.GetOrCreateValue(array);
				for (int j = k + 1; j < candidateLists.Length; j++)
				{
					MatchRecord[] array2 = candidateLists[j];
					if (MatchRecord.DisjointUnion(array, array2) != null)
					{
						orCreateValue.Add(array2);
						dictionary.GetOrCreateValue(array2).Add(array);
					}
				}
			}
			Dictionary<MatchRecord[], HashSet<MatchRecord[]>> dictionary2 = new Dictionary<MatchRecord[], HashSet<MatchRecord[]>>();
			foreach (KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>> keyValuePair in dictionary)
			{
				HashSet<MatchRecord[]> hashSet = new HashSet<MatchRecord[]> { keyValuePair.Key };
				MatchRecord[] array3 = keyValuePair.Key;
				foreach (MatchRecord[] array4 in keyValuePair.Value.OrderByDescending((MatchRecord[] m) => m[0].StartIndexes.Select((int x, int i) => m[0].EndIndexes[k] - x).Sum()))
				{
					MatchRecord[] array5 = MatchRecord.DisjointUnion(array3, array4);
					if (array5 != null)
					{
						array3 = array5;
						hashSet.Add(array4);
					}
				}
				dictionary2[array3] = hashSet;
			}
			KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>>[] array6 = this.RankAlignmentCombinations(dictionary2, inputStrings);
			Func<MatchRecord[], MatchRecord[]> <>9__5;
			this._learnedTopAlignedDelimiterSets = array6.Select(delegate(KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>> kvp)
			{
				if (projectionMap != null)
				{
					IEnumerable<MatchRecord[]> value2 = kvp.Value;
					Func<MatchRecord[], MatchRecord[]> func2;
					if ((func2 = <>9__5) == null)
					{
						func2 = (<>9__5 = (MatchRecord[] m) => projectionMap[m]);
					}
					return value2.Select(func2).ConvertToHashSet<MatchRecord[]>();
				}
				return kvp.Value;
			}).ToArray<HashSet<MatchRecord[]>>();
			KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>> keyValuePair2 = array6.FirstOrDefault((KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>> c) => !this.AllIrrelevantDelimiters(c.Key, inputStrings));
			if (keyValuePair2.Equals(default(KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>>)))
			{
				return new MatchRecord[0][][];
			}
			MatchRecord[] bestCombinationMatchRecord = keyValuePair2.Key;
			HashSet<MatchRecord[]> value = keyValuePair2.Value;
			HashSet<int> trivialZeroLengthSplits = this.GetTrivialZeroLengthSplits(bestCombinationMatchRecord, inputStrings, new HashSet<int>());
			HashSet<int> hashSet2 = this.IgnoreSpecialRegexDelimiters(bestCombinationMatchRecord, inputStrings, trivialZeroLengthSplits).ConvertToHashSet<int>();
			hashSet2.UnionWith(this.IgnoreMajorityIrrelevantDelimiters(bestCombinationMatchRecord, inputStrings, hashSet2));
			HashSet<Record<int, int>> ignoreMatchesOnFirstInput = new HashSet<Record<int, int>>(hashSet2.Select((int k) => Record.Create<int, int>(bestCombinationMatchRecord[0].StartIndexes[k], bestCombinationMatchRecord[0].EndIndexes[k])));
			HashSet<MatchRecord[]> hashSet3 = new HashSet<MatchRecord[]>();
			using (HashSet<MatchRecord[]>.Enumerator enumerator3 = value.GetEnumerator())
			{
				Func<Record<int, int>, bool> <>9__7;
				while (enumerator3.MoveNext())
				{
					MatchRecord[] list = enumerator3.Current;
					IEnumerable<Record<int, int>> enumerable = list[0].StartIndexes.Select((int s, int i) => Record.Create<int, int>(s, list[0].EndIndexes[i]));
					Func<Record<int, int>, bool> func;
					if ((func = <>9__7) == null)
					{
						func = (<>9__7 = (Record<int, int> t) => ignoreMatchesOnFirstInput.Contains(t));
					}
					if (!enumerable.All(func))
					{
						hashSet3.Add((projectionMap == null) ? list : projectionMap[list]);
					}
				}
			}
			MatchRecord[][] array7 = (this._options.LearnSimpleSingleDelimiterPrograms ? this.RankSimpleDelimiters(hashSet3, inputStrings) : hashSet3.ToArray<MatchRecord[]>());
			return new MatchRecord[][][] { array7 };
		}

		// Token: 0x06009C11 RID: 39953 RVA: 0x0020F9DC File Offset: 0x0020DBDC
		private KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>>[] RankAlignmentCombinations(Dictionary<MatchRecord[], HashSet<MatchRecord[]>> combinations, string[] inputStrings)
		{
			Dictionary<MatchRecord[], HashSet<int>> relevantDelimiters = combinations.ToDictionary((KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>> kvp) => kvp.Key, (KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>> kvp) => this.GetAllRelevantDelimiters(kvp.Key, inputStrings));
			if (this._options.LearnSimpleSingleDelimiterPrograms)
			{
				return combinations.OrderByDescending((KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>> kvp) => kvp.Key[0].NumNonZLDs(relevantDelimiters[kvp.Key])).ToArray<KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>>>();
			}
			if (this.LearnZeroLengthDelimiters)
			{
				return (from kvp in combinations
					orderby kvp.Key[0].NumZLDs(relevantDelimiters[kvp.Key]) descending, kvp.Key.Sum((MatchRecord m) => m.StartIndexes.Where((int x, int i) => x == m.EndIndexes[i]).Sum())
					select kvp).ToArray<KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>>>();
			}
			return (from kvp in combinations
				orderby kvp.Value.Sum(delegate(MatchRecord[] ms)
				{
					Func<MatchRecord, int> func;
					if ((func = Witnesses.<>O.<0>__TotalFieldsSize) == null)
					{
						func = (Witnesses.<>O.<0>__TotalFieldsSize = new Func<MatchRecord, int>(MatchRecordLearningExtensions.TotalFieldsSize));
					}
					return ms.Sum(func);
				}) descending, kvp.Value.Sum((MatchRecord[] ms) => ms[0].NumFields()) descending, kvp.Key.Sum((MatchRecord m) => m.StartIndexes.Select((int x, int i) => m.EndIndexes[i] - x).Sum()) descending, kvp.Key[0].NumDisjointNonZLDs(relevantDelimiters[kvp.Key]) descending, kvp.Key[0].NumNonZLDs(relevantDelimiters[kvp.Key]), kvp.Key.Sum((MatchRecord m) => m.StartIndexes.Sum())
				select kvp).ToArray<KeyValuePair<MatchRecord[], HashSet<MatchRecord[]>>>();
		}

		// Token: 0x06009C12 RID: 39954 RVA: 0x0020FB58 File Offset: 0x0020DD58
		private HashSet<int> GetTrivialZeroLengthSplits(MatchRecord[] matchRecords, string[] inputStrings, HashSet<int> ignoreIndexes)
		{
			HashSet<int> hashSet = Enumerable.Range(0, matchRecords[0].NumMatches).ConvertToHashSet<int>();
			int i = 0;
			Predicate<int> <>9__0;
			while (i < matchRecords.Length && !hashSet.IsEmpty<int>())
			{
				HashSet<int> hashSet2 = hashSet;
				Predicate<int> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (int k) => !matchRecords[i].IsTrivialZeroLengthSplit(k, inputStrings[i], ignoreIndexes));
				}
				hashSet2.RemoveWhere(predicate);
				int j = i;
				i = j + 1;
			}
			return hashSet;
		}

		// Token: 0x06009C13 RID: 39955 RVA: 0x0020FBF4 File Offset: 0x0020DDF4
		private int GetCommonDelScore(MatchRecord[] m, string[] inputStrings)
		{
			if (m[0].NumMatches <= 0)
			{
				return 0;
			}
			int num = m[0].StartIndexes[0];
			int num2 = m[0].EndIndexes[0];
			string text = inputStrings[0].Substring(num, num2 - num);
			return -Witnesses.CommonDelScores.MaybeGet(text).OrElse(0);
		}

		// Token: 0x06009C14 RID: 39956 RVA: 0x0020FC4C File Offset: 0x0020DE4C
		private MatchRecord[][] RankSimpleDelimiters(IEnumerable<MatchRecord[]> delimiterOccurrences, string[] inputStrings)
		{
			Func<MatchRecord[], int> func = (MatchRecord[] m) => -1 * m[0].NumMatches;
			Func<MatchRecord[], int> func2 = (MatchRecord[] m) => this.GetCommonDelScore(m, inputStrings);
			Func<MatchRecord[], int> func3 = delegate(MatchRecord[] m)
			{
				if (m[0].NumMatches <= 0 || m[0].EndIndexes[0] - m[0].StartIndexes[0] != 1)
				{
					return 1;
				}
				return 0;
			};
			return delimiterOccurrences.OrderBy(func2).ThenBy(func3).ThenBy(func)
				.ToArray<MatchRecord[]>();
		}

		// Token: 0x06009C15 RID: 39957 RVA: 0x0020FCD0 File Offset: 0x0020DED0
		private bool AllIrrelevantDelimiters(MatchRecord[] matchRecords, string[] inputStrings)
		{
			return this.GetRelevantSplitIndexes(matchRecords, inputStrings).All((HashSet<int> s) => s.IsEmpty<int>());
		}

		// Token: 0x06009C16 RID: 39958 RVA: 0x0020FD00 File Offset: 0x0020DF00
		private HashSet<int> GetAllRelevantDelimiters(MatchRecord[] matchRecords, string[] inputStrings)
		{
			IEnumerable<HashSet<int>> relevantSplitIndexes = this.GetRelevantSplitIndexes(matchRecords, inputStrings);
			HashSet<int> hashSet = new HashSet<int>();
			foreach (HashSet<int> hashSet2 in relevantSplitIndexes)
			{
				if (hashSet.Count == matchRecords[0].NumMatches)
				{
					break;
				}
				hashSet.UnionWith(hashSet2);
			}
			return hashSet;
		}

		// Token: 0x06009C17 RID: 39959 RVA: 0x0020FD68 File Offset: 0x0020DF68
		private bool IsCommonDelimiter(MatchRecord[] matchArray, string[] inputStrings)
		{
			Witnesses.<>c__DisplayClass48_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass48_0();
			CS$<>8__locals1.inputStrings = inputStrings;
			CS$<>8__locals1.matchArray = matchArray;
			CS$<>8__locals1.<>4__this = this;
			int k;
			int j;
			for (k = 0; k < CS$<>8__locals1.matchArray.Length; k = j + 1)
			{
				IEnumerable<string> enumerable = from str in CS$<>8__locals1.matchArray[k].StartIndexes.Select((int s, int i) => CS$<>8__locals1.inputStrings[k].Substring(s, CS$<>8__locals1.matchArray[k].EndIndexes[i] - s).Trim())
					where !string.IsNullOrEmpty(str)
					select str;
				if (enumerable.Any<string>())
				{
					IEnumerable<string> enumerable2 = enumerable;
					Func<string, bool> func;
					if ((func = CS$<>8__locals1.<>9__2) == null)
					{
						func = (CS$<>8__locals1.<>9__2 = (string s) => CS$<>8__locals1.<>4__this.GetRegexMatches(Witnesses.CommonDelimiters, s).Any((Match m) => m.Length == s.Length));
					}
					if (enumerable2.All(func))
					{
						return true;
					}
				}
				j = k;
			}
			return false;
		}

		// Token: 0x06009C18 RID: 39960 RVA: 0x0020FE64 File Offset: 0x0020E064
		private bool HasSpecialCharAdjacentMatches(MatchRecord[] matchArray, string[] inputStrings)
		{
			Witnesses.<>c__DisplayClass49_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass49_0();
			CS$<>8__locals1.matchArray = matchArray;
			CS$<>8__locals1.inputStrings = inputStrings;
			if (this.IsCommonDelimiter(CS$<>8__locals1.matchArray, CS$<>8__locals1.inputStrings))
			{
				return false;
			}
			MatchRecord matchRecord = CS$<>8__locals1.matchArray[0];
			int i;
			int j;
			for (i = 0; i < matchRecord.StartIndexes.Count; i = j + 1)
			{
				Witnesses.<>c__DisplayClass49_2 CS$<>8__locals3 = new Witnesses.<>c__DisplayClass49_2();
				int[] array = CS$<>8__locals1.inputStrings.Select((string s, int k) => CS$<>8__locals1.matchArray[k].EndIndexes[i]).ToArray<int>();
				IEnumerable<int> enumerable = array;
				Func<int, int, bool> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = (int index, int k) => index >= CS$<>8__locals1.inputStrings[k].Length);
				}
				if (!enumerable.Select(func).Any((bool b) => b))
				{
					Witnesses.<>c__DisplayClass49_2 CS$<>8__locals4 = CS$<>8__locals3;
					IEnumerable<int> enumerable2 = array;
					Func<int, int, char> func2;
					if ((func2 = CS$<>8__locals1.<>9__3) == null)
					{
						func2 = (CS$<>8__locals1.<>9__3 = (int index, int k) => CS$<>8__locals1.inputStrings[k][index]);
					}
					CS$<>8__locals4.rightChars = enumerable2.Select(func2).ToArray<char>();
					if (!this.IsPossibleDataCharacter(CS$<>8__locals3.rightChars[0]) && CS$<>8__locals3.rightChars.All((char c) => c == CS$<>8__locals3.rightChars[0]))
					{
						return true;
					}
					int[] array2 = CS$<>8__locals1.inputStrings.Select((string s, int k) => CS$<>8__locals1.matchArray[k].StartIndexes[i] - 1).ToArray<int>();
					if (!array2.Any((int index) => index < 0))
					{
						Witnesses.<>c__DisplayClass49_2 CS$<>8__locals5 = CS$<>8__locals3;
						IEnumerable<int> enumerable3 = array2;
						Func<int, int, char> func3;
						if ((func3 = CS$<>8__locals1.<>9__7) == null)
						{
							func3 = (CS$<>8__locals1.<>9__7 = (int index, int k) => CS$<>8__locals1.inputStrings[k][index]);
						}
						CS$<>8__locals5.leftChars = enumerable3.Select(func3).ToArray<char>();
						if (!this.IsPossibleDataCharacter(CS$<>8__locals3.leftChars[0]) && CS$<>8__locals3.leftChars.All((char c) => c == CS$<>8__locals3.leftChars[0]))
						{
							return true;
						}
					}
				}
				j = i;
			}
			return false;
		}

		// Token: 0x06009C19 RID: 39961 RVA: 0x00210094 File Offset: 0x0020E294
		private bool IsPossibleDataCharacter(char c)
		{
			return c == '-' || c == '+' || char.IsLetterOrDigit(c);
		}

		// Token: 0x06009C1A RID: 39962 RVA: 0x002100A8 File Offset: 0x0020E2A8
		private int[] IgnoreSpecialRegexDelimiters(MatchRecord[] delimiterMatches, string[] lines, HashSet<int> previousIgnoreIndexes)
		{
			Witnesses.<>c__DisplayClass51_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass51_0();
			CS$<>8__locals1.delimiterMatches = delimiterMatches;
			CS$<>8__locals1.validDelimiterIndexes = (from i in this.GetAllRelevantDelimiters(CS$<>8__locals1.delimiterMatches, lines).Except(previousIgnoreIndexes)
				orderby i
				select i).ToArray<int>();
			CS$<>8__locals1.stringToDelimiterSpanMap = this.GetDelimiterSpanIndexes(lines, CS$<>8__locals1.delimiterMatches, CS$<>8__locals1.validDelimiterIndexes);
			CS$<>8__locals1.stringToMatchingRegexesMap = this.GetSpecialRegexMatches(CS$<>8__locals1.stringToDelimiterSpanMap.Keys, lines);
			Record<int, int, HashSet<Regex>>[] array = (from t in CS$<>8__locals1.stringToMatchingRegexesMap.Keys.Where((string s) => CS$<>8__locals1.stringToDelimiterSpanMap.ContainsKey(s)).SelectMany((string s) => CS$<>8__locals1.stringToDelimiterSpanMap[s].Select((Record<int, int> t) => Record.Create<int, int, HashSet<Regex>>(t.Item1, t.Item2, CS$<>8__locals1.stringToMatchingRegexesMap[s])))
				orderby t.Item2 - t.Item1 descending
				select t).ToArray<Record<int, int, HashSet<Regex>>>();
			HashSet<Record<int, int>> hashSet = new HashSet<Record<int, int>>();
			CS$<>8__locals1.minMatchCount = (int)Math.Ceiling((double)lines.Length * 0.7);
			Record<int, int, HashSet<Regex>>[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				Witnesses.<>c__DisplayClass51_2 CS$<>8__locals2 = new Witnesses.<>c__DisplayClass51_2();
				CS$<>8__locals2.CS$<>8__locals2 = CS$<>8__locals1;
				CS$<>8__locals2.t = array2[j];
				int next = CS$<>8__locals2.CS$<>8__locals2.validDelimiterIndexes.FirstOrDefault((int x) => x > CS$<>8__locals2.t.Item2);
				if (next == 0)
				{
					next = CS$<>8__locals2.t.Item2;
				}
				int prev = CS$<>8__locals2.CS$<>8__locals2.validDelimiterIndexes.LastOrDefault((int x) => x < CS$<>8__locals2.t.Item1);
				if (prev == 0)
				{
					prev = CS$<>8__locals2.t.Item1;
				}
				if (!hashSet.Any((Record<int, int> t1) => t1.Item2 >= prev && t1.Item1 <= next))
				{
					string[] spanningStrings = lines.Select((string s, int i) => Witnesses.GetSpanningString(s, CS$<>8__locals2.CS$<>8__locals2.delimiterMatches[i], CS$<>8__locals2.t.Item1, CS$<>8__locals2.t.Item2, CS$<>8__locals2.CS$<>8__locals2.validDelimiterIndexes.ConvertToHashSet<int>())).ToArray<string>();
					if (CS$<>8__locals2.t.Item3.Any((Regex r) => spanningStrings.Count((string s) => r.Match(s.Trim()).Success) >= CS$<>8__locals2.CS$<>8__locals2.minMatchCount))
					{
						hashSet.Add(Record.Create<int, int>(CS$<>8__locals2.t.Item1, CS$<>8__locals2.t.Item2));
					}
				}
			}
			return (from i in hashSet.SelectMany((Record<int, int> t) => Enumerable.Range(t.Item1, 1 + t.Item2 - t.Item1)).Union(previousIgnoreIndexes)
				orderby i
				select i).ToArray<int>();
		}

		// Token: 0x06009C1B RID: 39963 RVA: 0x00210370 File Offset: 0x0020E570
		private IEnumerable<HashSet<int>> GetRelevantSplitIndexes(MatchRecord[] matchRecords, string[] inputs)
		{
			HashSet<int>[] array = new HashSet<int>[inputs.Length];
			for (int i = 0; i < inputs.Length; i++)
			{
				string input = inputs[i];
				MatchRecord matchRecord = matchRecords[i];
				Func<Record<int, int>, bool> isRelevantSplit = (Record<int, int> t) => t.Item1 < this._inputRelevantEndPoints[input].Item2 && t.Item2 > this._inputRelevantEndPoints[input].Item1;
				array[i] = (from x in matchRecord.StartIndexes.ZipWith(matchRecord.EndIndexes).Select(delegate(Record<int, int> t, int ind)
					{
						if (!isRelevantSplit(t))
						{
							return -1;
						}
						return ind;
					})
					where x != -1
					select x).ConvertToHashSet<int>();
			}
			return array;
		}

		// Token: 0x06009C1C RID: 39964 RVA: 0x00210414 File Offset: 0x0020E614
		private Dictionary<string, HashSet<Record<int, int>>> GetDelimiterSpanIndexes(string[] inputs, MatchRecord[] delimiters, int[] validIndexes)
		{
			HashSet<int> hashSet = validIndexes.ConvertToHashSet<int>();
			Dictionary<string, HashSet<Record<int, int>>> dictionary = new Dictionary<string, HashSet<Record<int, int>>>();
			int num = 0;
			int num2 = 0;
			int num3 = 5000;
			while (num2 < num3 && num < validIndexes.Length)
			{
				for (int i = 0; i < validIndexes.Length - num; i++)
				{
					int num4 = i + num;
					num2++;
					if (i != 0 || num4 != validIndexes.Length - 1)
					{
						string text = Witnesses.GetSpanningString(inputs[0], delimiters[0], validIndexes[i], validIndexes[num4], hashSet).Trim();
						if (!dictionary.ContainsKey(text))
						{
							dictionary[text] = new HashSet<Record<int, int>>();
						}
						dictionary[text].Add(Record.Create<int, int>(validIndexes[i], validIndexes[num4]));
					}
				}
				num++;
			}
			return dictionary;
		}

		// Token: 0x06009C1D RID: 39965 RVA: 0x002104C8 File Offset: 0x0020E6C8
		private static string GetSpanningString(string s, MatchRecord delimiterMatches, int startDelimiter, int endDelimiter, HashSet<int> validIndexes)
		{
			int previousValidDelimiterEnd = Witnesses.GetPreviousValidDelimiterEnd(s, delimiterMatches, startDelimiter, validIndexes);
			int nextValidDelimiterStart = Witnesses.GetNextValidDelimiterStart(s, delimiterMatches, endDelimiter, validIndexes);
			return s.Substring(previousValidDelimiterEnd, nextValidDelimiterStart - previousValidDelimiterEnd);
		}

		// Token: 0x06009C1E RID: 39966 RVA: 0x002104F8 File Offset: 0x0020E6F8
		private static int GetNextValidDelimiterStart(string s, MatchRecord m, int currentIndex, HashSet<int> validIndexes)
		{
			for (int i = currentIndex + 1; i < m.NumMatches; i++)
			{
				if (validIndexes.Contains(i))
				{
					return m.StartIndexes[i];
				}
			}
			return s.Length;
		}

		// Token: 0x06009C1F RID: 39967 RVA: 0x00210534 File Offset: 0x0020E734
		private static int GetPreviousValidDelimiterEnd(string s, MatchRecord m, int currentIndex, HashSet<int> validIndexes)
		{
			for (int i = currentIndex - 1; i >= 0; i--)
			{
				if (validIndexes.Contains(i))
				{
					return m.EndIndexes[i];
				}
			}
			return 0;
		}

		// Token: 0x06009C20 RID: 39968 RVA: 0x00210568 File Offset: 0x0020E768
		private Dictionary<string, HashSet<Regex>> GetSpecialRegexMatches(IEnumerable<string> strings, string[] lines)
		{
			Dictionary<string, HashSet<Regex>> dictionary = new Dictionary<string, HashSet<Regex>>();
			foreach (Regex regex in Semantics.SpecialRegexes.Keys)
			{
				HashSet<string> hashSet = (from m in this.GetRegexMatches(regex, lines[0])
					select m.Value).ConvertToHashSet<string>();
				using (IEnumerator<string> enumerator2 = strings.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string s = enumerator2.Current;
						if (hashSet.Contains(s) || (!Semantics.SpecialRegexes[regex].Item1 && this.GetRegexMatches(regex, s).Any((Match m) => m.Value == s)))
						{
							dictionary.GetOrCreateValue(s).Add(Semantics.SpecialRegexes[regex].Item2);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06009C21 RID: 39969 RVA: 0x002106A4 File Offset: 0x0020E8A4
		private int[] IgnoreMajorityIrrelevantDelimiters(MatchRecord[] matchRecords, string[] inputStrings, HashSet<int> ignoreIndices)
		{
			IEnumerable<HashSet<int>> relevantSplitIndexes = this.GetRelevantSplitIndexes(matchRecords, inputStrings);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (HashSet<int> hashSet in relevantSplitIndexes)
			{
				foreach (int num in hashSet.Except(ignoreIndices))
				{
					if (dictionary.ContainsKey(num))
					{
						Dictionary<int, int> dictionary2 = dictionary;
						int num2 = num;
						dictionary2[num2]++;
					}
					else
					{
						dictionary[num] = 1;
					}
				}
			}
			double minRelevanceFrequency = 0.30000000000000004 * (double)inputStrings.Length;
			int[] array = (from kvp in dictionary
				where (double)kvp.Value < minRelevanceFrequency
				select kvp.Key).ToArray<int>();
			if (array.Length == matchRecords[0].NumMatches - ignoreIndices.Count)
			{
				return new int[0];
			}
			return array;
		}

		// Token: 0x06009C22 RID: 39970 RVA: 0x002107D0 File Offset: 0x0020E9D0
		private BlackBoxRule GetRuleByName(string name)
		{
			return base.Grammar.Rules.OfType<BlackBoxRule>().Single((BlackBoxRule r) => r.Name == name);
		}

		// Token: 0x06009C23 RID: 39971 RVA: 0x0021080C File Offset: 0x0020EA0C
		private Witnesses.DominantSplitResultInfo GetDominantSplitResult(splitMatches? program, Spec spec)
		{
			if (program == null)
			{
				return null;
			}
			MatchRecord[] array = spec.ProvidedInputs.Select((State s) => (MatchRecord)program.Value.Node.Invoke(s)).ToArray<MatchRecord>();
			IGrouping<int, MatchRecord>[] array2 = (from m in array
				group m by m.NumMatches).OrderByDescending(delegate(IGrouping<int, MatchRecord> gp)
			{
				if (gp.Key <= 0)
				{
					return -1;
				}
				return gp.Count<MatchRecord>();
			}).ToArray<IGrouping<int, MatchRecord>>();
			return new Witnesses.DominantSplitResultInfo(array, array2.Length == 1, array2[0].Key, array2[0].Count<MatchRecord>());
		}

		// Token: 0x06009C24 RID: 39972 RVA: 0x002108BC File Offset: 0x0020EABC
		private static Record<int, int> GetRelevantEndPoints(string s)
		{
			int num = s.TakeWhile((char c) => char.IsWhiteSpace(c)).Count<char>();
			int num2 = s.Length;
			int num3 = s.Length - 1;
			while (num3 >= 0 && char.IsWhiteSpace(s[num3]))
			{
				num2--;
				num3--;
			}
			return Record.Create<int, int>(num, num2);
		}

		// Token: 0x06009C25 RID: 39973 RVA: 0x00210927 File Offset: 0x0020EB27
		[RuleLearner("SplitRegion")]
		public Optional<ProgramSet> LearnSplitRegionRule(SynthesisEngine engine, GrammarRule rule, LearningTask<WithInputTopSpec> task, CancellationToken cancel)
		{
			if (this.ExampleConstraints != null)
			{
				return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
			}
			return this.LearnSplitRegionPrograms(engine, rule, task, cancel).Set.Some<ProgramSet>();
		}

		// Token: 0x06009C26 RID: 39974 RVA: 0x00210958 File Offset: 0x0020EB58
		private bool ContainsMultispaceBetweenDelimiters(string[] inputs, string delimiter)
		{
			string[][] splitValues = inputs.Select((string s) => (from s1 in s.Split(new string[] { delimiter }, StringSplitOptions.None)
				select s1.Trim()).ToArray<string>()).ToArray<string[]>();
			if (splitValues.Any((string[] s) => s.Length != splitValues[0].Length))
			{
				return false;
			}
			int i;
			int j;
			for (i = 0; i < splitValues[0].Length; i = j + 1)
			{
				string[] array = splitValues.Select((string[] s) => s[i]).ToArray<string>();
				int num = array.Select((string s) => s.Length).Min();
				if (num > 2 && this.GetAlignedPositions(array, Enumerable.Range(1, num - 1), 0, num).Any<int>())
				{
					return true;
				}
				j = i;
			}
			return false;
		}

		// Token: 0x06009C27 RID: 39975 RVA: 0x00210A48 File Offset: 0x0020EC48
		private multipleMatches? GetDelimiterProgram(IEnumerable<string> delimiters)
		{
			v input = this._build.Node.Variable.v;
			r e = this._build.Node.Rule.Empty(input);
			IEnumerable<d> enumerable = delimiters.Select((string s) => this._build.Node.Rule.LookAround(e, this._build.Node.Rule.ConstStr(input, this._build.Node.Rule.s(s)), e));
			return this.ConstructSplitMultiple(enumerable);
		}

		// Token: 0x06009C28 RID: 39976 RVA: 0x00210AB8 File Offset: 0x0020ECB8
		public ProgramSetBuilder<regionSplit> LearnSplitRegionPrograms(SynthesisEngine engine, GrammarRule rule, LearningTask<WithInputTopSpec> task, CancellationToken cancel)
		{
			Witnesses.<>c__DisplayClass65_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass65_0();
			CS$<>8__locals1.rule = rule;
			CS$<>8__locals1.<>4__this = this;
			splitMatches? splitMatches = null;
			CS$<>8__locals1.dominantResult = null;
			string[] array = task.Spec.ProvidedInputs.Select((State s) => ((StringRegion)s[CS$<>8__locals1.rule.Grammar.InputSymbol]).Value).ToArray<string>();
			this.InitializeInputLines(array);
			if (this._options.ProvidedDelimiterStrings != null)
			{
				bool flag = this._options.LearnSimpleSingleDelimiterPrograms || this._options.LearnSimpleDelimitersOrFixedWidth;
				if (this._options.ProvidedDelimiterStrings.Count == 1 && flag && !this._options.LearnFixedWidth)
				{
					string text = this._options.ProvidedDelimiterStrings.First<string>();
					constantDelimiterMatches constantDelimiterMatches;
					if (this._options.ProvidedQuotingConfigurations != null && this._options.ProvidedQuotingConfigurations.Count > 0)
					{
						QuotingConfiguration quotingConfiguration = this._options.ProvidedQuotingConfigurations.First<QuotingConfiguration>();
						constantDelimiterMatches = this._build.Node.Rule.ConstantDelimiterWithQuoting(this._build.Node.Variable.v, this._build.Node.Rule.s(text), this._build.Node.Rule.quotingConf(quotingConfiguration));
					}
					else
					{
						constantDelimiterMatches = this._build.Node.Rule.ConstantDelimiter(this._build.Node.Variable.v, this._build.Node.Rule.s(text));
					}
					splitMatches = new splitMatches?(this._build.Node.UnnamedConversion.splitMatches_constantDelimiterMatches(constantDelimiterMatches));
				}
				else if (!flag && !this._options.LearnFixedWidth)
				{
					splitMatches = new splitMatches?(this._build.Node.UnnamedConversion.splitMatches_multipleMatches(this.GetDelimiterProgram(this._options.ProvidedDelimiterStrings).Value));
				}
			}
			else if (this._options.LearnSimpleDelimitersOrFixedWidth)
			{
				this._options.LearnSimpleSingleDelimiterPrograms = true;
				ProgramSet programSet = engine.LearnSymbol(this._build.Symbol.constantDelimiterMatches, task.Spec, cancel);
				IEnumerable<constantDelimiterMatches> enumerable = programSet.RealizedPrograms.Select(new Func<ProgramNode, constantDelimiterMatches>(this._build.Node.Cast.constantDelimiterMatches));
				this._options.LearnSimpleSingleDelimiterPrograms = false;
				this._options.LearnFixedWidth = true;
				ProgramSet programSet2 = engine.LearnSymbol(this._build.Symbol.fixedWidthMatches, task.Spec, cancel);
				IEnumerable<fixedWidthMatches> enumerable2 = programSet2.RealizedPrograms.Select(new Func<ProgramNode, fixedWidthMatches>(this._build.Node.Cast.fixedWidthMatches));
				this._options.LearnFixedWidth = false;
				IEnumerable<splitMatches> enumerable3;
				if (programSet2.IsEmpty)
				{
					enumerable3 = enumerable.Select(new Func<constantDelimiterMatches, splitMatches>(this._build.Node.UnnamedConversion.splitMatches_constantDelimiterMatches));
				}
				else if (programSet.IsEmpty)
				{
					enumerable3 = enumerable2.Select(new Func<fixedWidthMatches, splitMatches>(this._build.Node.UnnamedConversion.splitMatches_fixedWidthMatches));
				}
				else
				{
					string value = enumerable.First<constantDelimiterMatches>().Switch<s>(this._build, (ConstantDelimiterWithQuoting c) => c.s, (ConstantDelimiter c) => c.s).Value;
					int[] value2 = enumerable2.First<fixedWidthMatches>().Cast_FixedWidth(this._build).fieldStartPositions.Value;
					string[] array2 = (from s in array[0].Trim().Split(new string[] { value }, StringSplitOptions.None)
						select s.Trim()).ToArray<string>();
					int num = array2.Count((string s) => !string.IsNullOrEmpty(s));
					int num2 = array2.Length - num;
					int num3 = value2.Length + 1;
					if ((value.All((char c) => c == ' ') && (num2 > 0 || num <= num3)) || (this.ContainsMultispaceBetweenDelimiters(array, value) && num <= num3))
					{
						enumerable3 = enumerable2.Select(new Func<fixedWidthMatches, splitMatches>(this._build.Node.UnnamedConversion.splitMatches_fixedWidthMatches));
					}
					else
					{
						enumerable3 = enumerable.Select(new Func<constantDelimiterMatches, splitMatches>(this._build.Node.UnnamedConversion.splitMatches_constantDelimiterMatches));
					}
				}
				splitMatches = enumerable3.FirstOrNull<splitMatches>();
			}
			else if (this._options.LearnSimpleSingleDelimiterPrograms)
			{
				splitMatches = engine.LearnSymbol(this._build.Symbol.constantDelimiterMatches, task.Spec, cancel).RealizedPrograms.Select(new Func<ProgramNode, constantDelimiterMatches>(this._build.Node.Cast.constantDelimiterMatches)).Select(new Func<constantDelimiterMatches, splitMatches>(this._build.Node.UnnamedConversion.splitMatches_constantDelimiterMatches)).FirstOrNull<splitMatches>();
			}
			else if (this._options.LearnFixedWidth)
			{
				splitMatches = engine.LearnSymbol(this._build.Symbol.fixedWidthMatches, task.Spec, cancel).RealizedPrograms.Select(new Func<ProgramNode, fixedWidthMatches>(this._build.Node.Cast.fixedWidthMatches)).Select(new Func<fixedWidthMatches, splitMatches>(this._build.Node.UnnamedConversion.splitMatches_fixedWidthMatches)).FirstOrNull<splitMatches>();
			}
			else
			{
				bool flag2 = Convert.ToInt16(array.Average(delegate(string s)
				{
					if (s == null)
					{
						return 0;
					}
					return s.Length;
				})) >= 70;
				IReadOnlyCollection<State> providedInputs = task.Spec.ProvidedInputs;
				IReadOnlyCollection<State> readOnlyCollection = providedInputs ?? task.Spec.ProvidedInputs.ToList<State>();
				if (flag2)
				{
					readOnlyCollection = readOnlyCollection.DeterministicallySample(10).ToList<State>();
				}
				splitMatches = engine.LearnSymbol(this._build.Symbol.multipleMatches, new WithInputTopSpec(readOnlyCollection), cancel).RealizedPrograms.Select(new Func<ProgramNode, multipleMatches>(this._build.Node.Cast.multipleMatches)).Select(new Func<multipleMatches, splitMatches>(this._build.Node.UnnamedConversion.splitMatches_multipleMatches)).FirstOrNull<splitMatches>();
				CS$<>8__locals1.dominantResult = this.GetDominantSplitResult(splitMatches, task.Spec);
				IEnumerable<State> enumerable4 = null;
				if (flag2 && (CS$<>8__locals1.dominantResult == null || CS$<>8__locals1.dominantResult.ConformingInputsRatio < 0.97))
				{
					if (splitMatches == null)
					{
						enumerable4 = task.Spec.ProvidedInputs;
					}
					else
					{
						IEnumerable<State> enumerable5 = task.Spec.ProvidedInputs.Where((State s, int i) => CS$<>8__locals1.dominantResult.SplitResults[i].NumMatches != CS$<>8__locals1.dominantResult.DominantNumMatches);
						HashSet<State> hashSet = new HashSet<State>(readOnlyCollection.Concat(enumerable5), IdentityEquality.Comparer);
						int num4 = hashSet.Count - readOnlyCollection.Count;
						if (num4 > 0)
						{
							IEnumerable<State> enumerable6 = task.Spec.ProvidedInputs.Except(hashSet, IdentityEquality.Comparer).ToArray<object>().DeterministicallySample(num4 * 4)
								.Cast<State>();
							hashSet.UnionWith(enumerable6);
							enumerable4 = hashSet;
						}
					}
				}
				if (enumerable4 != null)
				{
					splitMatches = engine.LearnSymbol(this._build.Symbol.multipleMatches, new WithInputTopSpec(enumerable4), cancel).RealizedPrograms.Select(new Func<ProgramNode, multipleMatches>(this._build.Node.Cast.multipleMatches)).Select(new Func<multipleMatches, splitMatches>(this._build.Node.UnnamedConversion.splitMatches_multipleMatches)).FirstOrNull<splitMatches>();
					CS$<>8__locals1.dominantResult = this.GetDominantSplitResult(splitMatches, task.Spec);
				}
				if (splitMatches == null)
				{
					splitMatches? splitMatches2 = this.LearnInconsistentDelimiterPrograms(array);
					splitMatches = ((splitMatches2 != null) ? splitMatches2 : this.LearnDelimiterPrograms(engine, task, cancel, true));
				}
			}
			if (splitMatches == null)
			{
				return ProgramSetBuilder.Empty<regionSplit>(CS$<>8__locals1.rule.Head);
			}
			if (CS$<>8__locals1.dominantResult == null)
			{
				CS$<>8__locals1.dominantResult = this.GetDominantSplitResult(splitMatches, task.Spec);
			}
			bool flag3 = splitMatches.Value.Node.GrammarRule == this._build.UnnamedConversion.splitMatches_fixedWidthMatches;
			bool flag4 = splitMatches.Value.Node.GrammarRule == this._build.UnnamedConversion.splitMatches_constantDelimiterMatches;
			CS$<>8__locals1.conformingSplitResults = CS$<>8__locals1.dominantResult.SplitResults.Where((MatchRecord m) => m.NumMatches == CS$<>8__locals1.dominantResult.DominantNumMatches).ToArray<MatchRecord>();
			CS$<>8__locals1.conformingInputs = (from l in this._inputLines.Select(delegate(string l, int i)
				{
					if (CS$<>8__locals1.dominantResult.SplitResults[i].NumMatches != CS$<>8__locals1.dominantResult.DominantNumMatches)
					{
						return null;
					}
					return l;
				})
				where l != null
				select l).ToArray<string>();
			CS$<>8__locals1.ignoreMatchesSet = new HashSet<int>();
			bool flag5 = false;
			bool flag6 = false;
			int num5 = -1;
			int num6 = -1;
			FillStrategy fillStrategy = this._options.FillStrategy ?? (this._options.SuggestionsMode ? FillStrategy.Null : FillStrategy.LeftToRight);
			if (this._options.ProvidedDelimiterStrings != null || flag4)
			{
				if (fillStrategy != FillStrategy.Null)
				{
					int num7 = CS$<>8__locals1.dominantResult.SplitResults.Max((MatchRecord r) => r.NumMatches);
					num5 = 1 + num7 + (this._options.IncludeDelimiters ? num7 : 0);
				}
			}
			else if (!flag3)
			{
				HashSet<int> trivialZeroLengthSplits = this.GetTrivialZeroLengthSplits(CS$<>8__locals1.conformingSplitResults, CS$<>8__locals1.conformingInputs, CS$<>8__locals1.ignoreMatchesSet);
				CS$<>8__locals1.ignoreMatchesSet = this.IgnoreSpecialRegexDelimiters(CS$<>8__locals1.conformingSplitResults, CS$<>8__locals1.conformingInputs, trivialZeroLengthSplits).ConvertToHashSet<int>();
				flag5 = CS$<>8__locals1.dominantResult.DominantNumMatches > CS$<>8__locals1.ignoreMatchesSet.Count && CS$<>8__locals1.conformingSplitResults.All(delegate(MatchRecord m)
				{
					IEnumerable<int> startIndexes = m.StartIndexes;
					Func<int, int, bool> func;
					if ((func = CS$<>8__locals1.<>9__16) == null)
					{
						func = (CS$<>8__locals1.<>9__16 = (int s, int j) => !CS$<>8__locals1.ignoreMatchesSet.Contains(j));
					}
					return startIndexes.Where(func).First<int>() == 0;
				});
				bool flag7;
				if (CS$<>8__locals1.dominantResult.DominantNumMatches > CS$<>8__locals1.ignoreMatchesSet.Count)
				{
					flag7 = CS$<>8__locals1.conformingSplitResults.Select(delegate(MatchRecord m, int i)
					{
						IEnumerable<int> endIndexes = m.EndIndexes;
						Func<int, int, bool> func2;
						if ((func2 = CS$<>8__locals1.<>9__17) == null)
						{
							func2 = (CS$<>8__locals1.<>9__17 = (int s, int j) => !CS$<>8__locals1.ignoreMatchesSet.Contains(j));
						}
						return endIndexes.Where(func2).Last<int>() == CS$<>8__locals1.conformingInputs[i].Length;
					}).All((bool b) => b);
				}
				else
				{
					flag7 = false;
				}
				flag6 = flag7;
			}
			if (num5 == -1)
			{
				num5 = 1 + CS$<>8__locals1.dominantResult.DominantNumMatches - CS$<>8__locals1.ignoreMatchesSet.Count;
				if (flag5)
				{
					num5--;
				}
				if (flag6)
				{
					num5--;
				}
				if (this._options.IncludeDelimiters)
				{
					num6 = CS$<>8__locals1.conformingSplitResults[0].StartIndexes.Where((int idx, int i) => idx != CS$<>8__locals1.conformingSplitResults[0].EndIndexes[i] && !CS$<>8__locals1.ignoreMatchesSet.Contains(i)).Count<int>();
					num5 += num6;
				}
			}
			regionSplit regionSplit = this.BuildRegionSplitProgram(splitMatches.Value, CS$<>8__locals1.ignoreMatchesSet.ToArray<int>(), num5, flag5, flag6, !flag3 && this._options.IncludeDelimiters, fillStrategy);
			if (!this._options.SuggestionsMode)
			{
				return ProgramSetBuilder.List<regionSplit>(new regionSplit[] { regionSplit });
			}
			List<List<string>> splitColumns;
			HashSet<string> hashSet2;
			if (this.HandRaiseOutputCheck(task, regionSplit, out hashSet2, out splitColumns))
			{
				foreach (string text2 in from d1 in hashSet2.Select(delegate(string s)
					{
						if (!string.IsNullOrWhiteSpace(s))
						{
							return s.Trim();
						}
						return s;
					}).ToArray<string>()
					where Witnesses.CommonDelimiters.IsMatch(d1)
					select d1)
				{
					char d = text2[0];
					Func<char, bool> <>9__25;
					Func<Match, bool> <>9__27;
					if (Enumerable.Range(0, this._inputLines.Length).Any((int k) => splitColumns.Count(delegate(List<string> c)
					{
						if (c[k] != null && c[k].Contains(d))
						{
							IEnumerable<char> enumerable7 = c[k];
							Func<char, bool> func3;
							if ((func3 = <>9__25) == null)
							{
								func3 = (<>9__25 = (char x) => x == d || char.IsWhiteSpace(x));
							}
							return enumerable7.All(func3);
						}
						return false;
					}) < base.<LearnSplitRegionPrograms>g__numDelOccurrences|20(CS$<>8__locals1.<>4__this._inputLines[k])) && this._inputLines.All((string s) => Semantics.GetDataTypeRegexes().All(delegate(Regex r)
					{
						IEnumerable<Match> enumerable8 = r.Matches(s).Cast<Match>();
						Func<Match, bool> func4;
						if ((func4 = <>9__27) == null)
						{
							func4 = (<>9__27 = (Match m) => !m.Value.Contains(d));
						}
						return enumerable8.All(func4);
					})))
					{
						splitMatches splitMatches3 = this._build.Node.UnnamedConversion.splitMatches_constantDelimiterMatches(this._build.Node.Rule.ConstantDelimiter(this._build.Node.Variable.v, this._build.Node.Rule.s(text2)));
						Func<char, bool> <>9__29;
						int num8 = 1 + task.ProvidedInputs.Max(delegate(State s)
						{
							string value3 = ((StringRegion)s[CS$<>8__locals1.<>4__this.Grammar.InputSymbol]).Value;
							if (value3 == null)
							{
								return null;
							}
							Func<char, bool> func5;
							if ((func5 = <>9__29) == null)
							{
								func5 = (<>9__29 = (char c) => c == d);
							}
							return new int?(value3.Count(func5));
						}).Value;
						regionSplit regionSplit2 = this.BuildRegionSplitProgram(splitMatches3, new int[0], num8, false, false, true, FillStrategy.LeftToRight);
						HashSet<string> hashSet3;
						List<List<string>> list;
						if (this.HandRaiseOutputCheck(task, regionSplit2, out hashSet3, out list))
						{
							regionSplit regionSplit3 = this.BuildRegionSplitProgram(splitMatches3, new int[0], num8, false, false, false, FillStrategy.LeftToRight);
							return ProgramSetBuilder.List<regionSplit>(new regionSplit[] { regionSplit3 });
						}
					}
				}
				regionSplit regionSplit4 = this.BuildRegionSplitProgram(splitMatches.Value, CS$<>8__locals1.ignoreMatchesSet.ToArray<int>(), num5 - num6, flag5, flag6, false, fillStrategy);
				return ProgramSetBuilder.List<regionSplit>(new regionSplit[] { regionSplit4 });
			}
			return ProgramSetBuilder.Empty<regionSplit>(CS$<>8__locals1.rule.Head);
		}

		// Token: 0x06009C29 RID: 39977 RVA: 0x00211864 File Offset: 0x0020FA64
		private regionSplit BuildRegionSplitProgram(splitMatches splitMatchesProgram, int[] ignoreMatchesSet, int numSplits, bool delimiterStart, bool delimiterEnd, bool includeDelimiters, FillStrategy fillStrategy)
		{
			return this._build.Node.Rule.SplitRegion(this._build.Node.Variable.v, splitMatchesProgram, this._build.Node.Rule.ignoreIndexes(ignoreMatchesSet.ToArray<int>()), this._build.Node.Rule.numSplits(numSplits), this._build.Node.Rule.delimiterStart(delimiterStart), this._build.Node.Rule.delimiterEnd(delimiterEnd), this._build.Node.Rule.includeDelimiters(includeDelimiters), this._build.Node.Rule.fillStrategy(fillStrategy));
		}

		// Token: 0x06009C2A RID: 39978 RVA: 0x0021192C File Offset: 0x0020FB2C
		private bool HandRaiseOutputCheck(LearningTask<WithInputTopSpec> task, regionSplit splitRegionProgram, out HashSet<string> delimiterStrings, out List<List<string>> splitColumns)
		{
			List<List<string>> splitRows = new List<List<string>>();
			HashSet<int> delimiterColumns = new HashSet<int>();
			splitColumns = null;
			delimiterStrings = new HashSet<string>();
			foreach (State state in task.Spec.ProvidedInputs)
			{
				SplitCell[] r2 = (SplitCell[])splitRegionProgram.Node.Invoke(state);
				splitRows.Add(r2.Select(delegate(SplitCell c)
				{
					StringRegion cellValue = c.CellValue;
					if (cellValue == null)
					{
						return null;
					}
					return cellValue.Value;
				}).ToList<string>());
				delimiterColumns.AddRange(from k in Enumerable.Range(0, r2.Length)
					where r2[k].IsDelimiter
					select k);
				delimiterStrings.AddRange(from c in r2.Where(delegate(SplitCell c)
					{
						if (c.IsDelimiter)
						{
							StringRegion cellValue2 = c.CellValue;
							return !string.IsNullOrWhiteSpace((cellValue2 != null) ? cellValue2.Value : null);
						}
						return false;
					})
					select c.CellValue.Value);
			}
			string[] inputLines = task.Spec.ProvidedInputs.Select((State s) => ((StringRegion)s[this.Grammar.InputSymbol]).Value).ToArray<string>();
			if (splitRows.IsEmpty<List<string>>())
			{
				return false;
			}
			if (splitRows.All((List<string> r) => r.Count == 1))
			{
				return false;
			}
			if (delimiterStrings.Any((string s) => inputLines.Contains(s)))
			{
				return false;
			}
			int count = splitRows.Count;
			if ((double)splitRows.Count((List<string> r) => !r.All((string s) => s == null)) / (double)count >= 0.7)
			{
				if (inputLines.Where((string s, int k) => !splitRows[k].All((string s) => s == null)).ToArray<string>().All((string s) => Semantics.GetDataTypeRegexes().Any((Regex r) => Semantics.IsFullMatch(r, s))))
				{
					return false;
				}
				splitColumns = splitRows[0].Select((string r, int k) => splitRows.Select((List<string> r1) => r1[k] ?? "").ToList<string>()).ToList<List<string>>();
				List<List<string>> list = splitColumns.Where((List<string> c, int k) => !delimiterColumns.Contains(k)).ToList<List<string>>();
				if (Witnesses.IsWellFormedResult(splitColumns, list))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009C2B RID: 39979 RVA: 0x00211BE0 File Offset: 0x0020FDE0
		private static bool IsWellFormedResult(List<List<string>> splitResult, List<List<string>> splitResultWithoutDelimiters)
		{
			if (splitResultWithoutDelimiters.Any((List<string> c) => c.Any((string s) => !Witnesses.HasBalancedBrackets(s) || !Witnesses.HasBalancedQuotes(s))))
			{
				return false;
			}
			int num = splitResultWithoutDelimiters.Count(delegate(List<string> c)
			{
				Func<string, bool> func;
				if ((func = Witnesses.<>O.<1>__IsNullOrWhiteSpace) == null)
				{
					func = (Witnesses.<>O.<1>__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace));
				}
				return (double)c.Count(func) / (double)c.Count > 0.7;
			});
			return (num <= 0 || splitResultWithoutDelimiters.Count - num > 1) && !Witnesses.IsSplittingWord(splitResult) && !Witnesses.IsSplittingSentence(splitResult);
		}

		// Token: 0x06009C2C RID: 39980 RVA: 0x00211C60 File Offset: 0x0020FE60
		private static bool IsMatchingBracketPair(char c1, char c2)
		{
			return (c1 == '(' && c2 == ')') || (c1 == '{' && c2 == '}') || (c1 == '[' && c2 == ']');
		}

		// Token: 0x06009C2D RID: 39981 RVA: 0x00211C84 File Offset: 0x0020FE84
		private static bool HasBalancedBrackets(string s)
		{
			Stack<char> stack = new Stack<char>();
			char[] array = new char[] { '{', '[', '(' };
			char[] array2 = new char[] { '}', ']', ')' };
			for (int i = 0; i < s.Length; i++)
			{
				if (array.Contains(s[i]))
				{
					stack.Push(s[i]);
				}
				if (array2.Contains(s[i]) && (stack.Count == 0 || !Witnesses.IsMatchingBracketPair(stack.Pop(), s[i])))
				{
					return false;
				}
			}
			return !stack.Any<char>();
		}

		// Token: 0x06009C2E RID: 39982 RVA: 0x00211D20 File Offset: 0x0020FF20
		private static bool HasBalancedQuotes(string s)
		{
			if (s.Count((char c) => c == '"') % 2 != 0)
			{
				return false;
			}
			Func<string, int, bool> isInWordApostrophe = (string s, int k) => s[k] == '\'' && k > 0 && char.IsLetter(s[k - 1]) && k < s.Length - 1 && char.IsLetter(s[k + 1]);
			return Enumerable.Range(0, s.Length).Count((int k) => s[k] == '\'' && !isInWordApostrophe(s, k)) % 2 == 0;
		}

		// Token: 0x06009C2F RID: 39983 RVA: 0x00211DB8 File Offset: 0x0020FFB8
		private static bool IsSplittingWord(List<List<string>> splitResult)
		{
			int count = splitResult.Count;
			if (count == 0)
			{
				return false;
			}
			int count2 = splitResult[0].Count;
			if (count2 == 0)
			{
				return false;
			}
			for (int i = 0; i < count2; i++)
			{
				for (int j = 1; j < count; j++)
				{
					string text = splitResult[j - 1][i];
					string text2 = splitResult[j][i];
					if (text != null && text2 != null && text.Length > 0 && text2.Length > 0 && char.IsLetter(text.Last<char>()) && char.IsLetter(text2.First<char>()))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06009C30 RID: 39984 RVA: 0x00211E58 File Offset: 0x00210058
		private static bool IsSplittingSentence(List<List<string>> splitResult)
		{
			int count = splitResult.Count;
			if (count == 0)
			{
				return false;
			}
			int count2 = splitResult[0].Count;
			if (count2 == 0)
			{
				return false;
			}
			for (int i = 0; i < count2; i++)
			{
				for (int j = 1; j < count; j++)
				{
					string text = splitResult[j - 1][i];
					string text2 = splitResult[j][i];
					string text3 = ((j < count - 1) ? splitResult[j + 1][i] : null);
					if (text != null && text.Length > 0 && text2 != null && text2.Length > 0 && text3 != null && text3.Length > 0 && char.IsLetter(text.Last<char>()) && string.IsNullOrWhiteSpace(text2) && char.IsLetter(text3.First<char>()))
					{
						return true;
					}
					if (text2.StartsWith("."))
					{
						char? c = null;
						if (text2.Length > 1)
						{
							c = new char?(text2[1]);
						}
						int num = j + 1;
						while (num < count && c == null)
						{
							if (!string.IsNullOrEmpty(splitResult[num][i]))
							{
								c = new char?(splitResult[num][i][0]);
							}
							num++;
						}
						if ((c == null || char.IsWhiteSpace(c.Value)) && Witnesses._wordSequenceEnd.IsMatch(text))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06009C31 RID: 39985 RVA: 0x00211FD8 File Offset: 0x002101D8
		[RuleLearner("FixedWidth")]
		public Optional<ProgramSet> LearnFixedWidthRule(SynthesisEngine engine, GrammarRule rule, LearningTask<WithInputTopSpec> task, CancellationToken cancel)
		{
			Witnesses.<>c__DisplayClass74_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass74_0();
			CS$<>8__locals1.rule = rule;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.inputLines = task.Spec.ProvidedInputs.Select((State s) => ((StringRegion)s[CS$<>8__locals1.rule.Grammar.InputSymbol]).Value).ToArray<string>();
			if (CS$<>8__locals1.inputLines.Length == 0)
			{
				return ProgramSet.Empty(CS$<>8__locals1.rule.Head).Some<ProgramSet>();
			}
			CS$<>8__locals1.maxWidth = CS$<>8__locals1.inputLines.Min((string s) => s.Length);
			if (CS$<>8__locals1.maxWidth == 0)
			{
				return ProgramSet.Empty(CS$<>8__locals1.rule.Head).Some<ProgramSet>();
			}
			List<int> list = new List<int>();
			int n;
			int l;
			for (n = 0; n < CS$<>8__locals1.maxWidth; n = l + 1)
			{
				if (CS$<>8__locals1.inputLines.All((string s) => char.IsWhiteSpace(s[n])))
				{
					int num = CS$<>8__locals1.inputLines.Count((string s) => n < s.Length - 1 && !char.IsWhiteSpace(s[n + 1]));
					if (num != 0 && (num > 1 || (n > 0 && CS$<>8__locals1.inputLines.All((string s) => char.IsWhiteSpace(s[n - 1])))))
					{
						list.Add(n + 1);
					}
				}
				l = n;
			}
			HashSet<Record<int, int>> hashSet = new HashSet<Record<int, int>>();
			using (List<Regex>.Enumerator enumerator = new List<Regex>(from kvp in Semantics.SpecialRegexes
				where kvp.Value.Item3 != "Phrase pattern"
				select kvp.Key)
			{
				new Regex("-?[0-9]+(\\,[0-9]{3})*(\\.[0-9]+)?")
			}.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Witnesses.<>c__DisplayClass74_2 CS$<>8__locals3 = new Witnesses.<>c__DisplayClass74_2();
					CS$<>8__locals3.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals3.regex = enumerator.Current;
					HashSet<Record<int, int>>[] array = CS$<>8__locals3.CS$<>8__locals1.inputLines.Select(delegate(string s)
					{
						IEnumerable<Match> regexMatches = CS$<>8__locals3.CS$<>8__locals1.<>4__this.GetRegexMatches(CS$<>8__locals3.regex, s);
						Func<Match, bool> func5;
						if ((func5 = CS$<>8__locals3.CS$<>8__locals1.<>9__18) == null)
						{
							func5 = (CS$<>8__locals3.CS$<>8__locals1.<>9__18 = (Match m) => m.Index + m.Length <= CS$<>8__locals3.CS$<>8__locals1.maxWidth);
						}
						return (from m in regexMatches.Where(func5)
							select Record.Create<int, int>(m.Index, m.Index + m.Length)).ConvertToHashSet<Record<int, int>>();
					}).ToArray<HashSet<Record<int, int>>>();
					IEnumerable<Record<int, int>> enumerable = array.SelectMany((HashSet<Record<int, int>> t) => t2);
					Func<Record<int, int>, bool> func;
					if ((func = CS$<>8__locals3.CS$<>8__locals1.<>9__14) == null)
					{
						func = (CS$<>8__locals3.CS$<>8__locals1.<>9__14 = (Record<int, int> t) => t2.Item1 < CS$<>8__locals3.CS$<>8__locals1.maxWidth);
					}
					Record<int, int>[] array2 = enumerable.Where(func).Distinct<Record<int, int>>().ToArray<Record<int, int>>();
					Dictionary<int, int> maximalMatchesMap = new Dictionary<int, int>();
					Record<int, int>[] array3 = array2;
					for (l = 0; l < array3.Length; l++)
					{
						Record<int, int> record = array3[l];
						if (maximalMatchesMap.GetOrAdd(record.Item1, record.Item2) < record.Item2)
						{
							maximalMatchesMap[record.Item1] = record.Item2;
						}
					}
					IEnumerable<int> enumerable2 = array2.Select((Record<int, int> t) => t2.Item1).Distinct<int>();
					Func<int, bool> func2;
					if ((func2 = CS$<>8__locals3.CS$<>8__locals1.<>9__16) == null)
					{
						func2 = (CS$<>8__locals3.CS$<>8__locals1.<>9__16 = (int i) => CS$<>8__locals3.CS$<>8__locals1.inputLines.All((string s) => l < s.Length));
					}
					(from i in enumerable2.Where(func2)
						orderby l
						select i).ToArray<int>();
					foreach (KeyValuePair<int, int> keyValuePair in maximalMatchesMap.OrderBy((KeyValuePair<int, int> kvp) => kvp.Key))
					{
						int start = keyValuePair.Key;
						foreach (int num2 in (from i in (from t in array2
								where t2.Item2 > start && t2.Item2 <= CS$<>8__locals3.CS$<>8__locals1.maxWidth && t2.Item1 <= maximalMatchesMap[start]
								select t2.Item2).Distinct<int>()
							orderby l
							select i).ToArray<int>())
						{
							bool flag = true;
							for (int j = 0; j < CS$<>8__locals3.CS$<>8__locals1.inputLines.Length; j++)
							{
								string text = CS$<>8__locals3.CS$<>8__locals1.inputLines[j];
								Record<int, int>? record2 = null;
								foreach (Record<int, int> record3 in array[j])
								{
									if (record3.Item1 >= start && record3.Item2 <= num2)
									{
										record2 = new Record<int, int>?(record3);
										break;
									}
								}
								if (record2 != null)
								{
									if (!string.IsNullOrWhiteSpace(text.Substring(start, record2.Value.Item1 - start)) || !string.IsNullOrWhiteSpace(text.Substring(record2.Value.Item2, num2 - record2.Value.Item2)))
									{
										flag = false;
										break;
									}
								}
								else if (!string.IsNullOrWhiteSpace(text.Substring(start, num2 - start)))
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								hashSet.Add(Record.Create<int, int>(start, num2));
							}
						}
					}
				}
			}
			CS$<>8__locals1.largestDisjoinDataTypeColumns = new List<Record<int, int>>();
			using (IEnumerator<Record<int, int>> enumerator4 = hashSet.OrderByDescending((Record<int, int> t) => t2.Item2 - t2.Item1).GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					Record<int, int> t2 = enumerator4.Current;
					if (CS$<>8__locals1.largestDisjoinDataTypeColumns.All((Record<int, int> t1) => t1.Item2 <= t2.Item1 || t1.Item1 >= t2.Item2))
					{
						CS$<>8__locals1.largestDisjoinDataTypeColumns.Add(t2);
					}
				}
			}
			IEnumerable<int> enumerable3 = CS$<>8__locals1.largestDisjoinDataTypeColumns.Select((Record<int, int> t) => t.Item1);
			IEnumerable<int> enumerable4 = CS$<>8__locals1.largestDisjoinDataTypeColumns.Select((Record<int, int> t) => t.Item2);
			Func<int, bool> func3 = (int x) => !CS$<>8__locals1.largestDisjoinDataTypeColumns.Any((Record<int, int> t) => x > t.Item1 && x < t.Item2);
			CS$<>8__locals1.candidatePositions = (from i in enumerable3.Concat(enumerable4).Concat(list.Where(func3)).Distinct<int>()
				orderby l
				select i).ToArray<int>();
			HashSet<int> hashSet2 = new HashSet<int>();
			do
			{
				hashSet2.Clear();
				bool flag2 = true;
				int num3 = 0;
				int num4 = 0;
				while (flag2)
				{
					int num5 = num3;
					if (num4 == CS$<>8__locals1.candidatePositions.Length)
					{
						num3 = CS$<>8__locals1.maxWidth;
						flag2 = false;
					}
					else
					{
						num3 = CS$<>8__locals1.candidatePositions[num4];
					}
					int num6 = num3 - num5;
					if (num6 > 2)
					{
						IEnumerable<int> positions = Enumerable.Range(num5 + 1, num6 - 2);
						IEnumerable<int> enumerable5 = CS$<>8__locals1.inputLines.SelectMany((string s) => positions.Where((int p) => CS$<>8__locals1.<>4__this.HasMultipleAdjacentSpaces(s, p - 1, 2, true))).ConvertToHashSet<int>();
						HashSet<int> possibleEndPositions = CS$<>8__locals1.inputLines.SelectMany((string s) => positions.Where((int p) => CS$<>8__locals1.<>4__this.HasMultipleAdjacentSpaces(s, p, 2, false))).ConvertToHashSet<int>();
						if (enumerable5.Any((int p1) => possibleEndPositions.Any((int p2) => p1 >= p2)))
						{
							hashSet2.AddRange(this.GetAlignedPositions(CS$<>8__locals1.inputLines, positions, num5, num3));
						}
					}
					num4++;
				}
				CS$<>8__locals1.candidatePositions = (from k in CS$<>8__locals1.candidatePositions.Union(hashSet2)
					orderby k
					select k).ToArray<int>();
			}
			while (hashSet2.Count > 0);
			hashSet2.Clear();
			CS$<>8__locals1.numericSpecialChars = new HashSet<char> { '.', ',', '-' };
			Witnesses.<>c__DisplayClass74_0 CS$<>8__locals8 = CS$<>8__locals1;
			Func<char, bool> func4;
			if ((func4 = Witnesses.<>O.<2>__IsLetter) == null)
			{
				func4 = (Witnesses.<>O.<2>__IsLetter = new Func<char, bool>(char.IsLetter));
			}
			CS$<>8__locals8.isNonNumeric = func4;
			int i2;
			for (i2 = 0; i2 < CS$<>8__locals1.maxWidth; i2 = l + 1)
			{
				if (!CS$<>8__locals1.largestDisjoinDataTypeColumns.Any((Record<int, int> t) => t.Item1 <= i2 + 1 && t.Item2 >= i2 + 1))
				{
					double num7 = 0.95;
					double num8 = 0.5;
					double num9 = (double)CS$<>8__locals1.inputLines.Count((string s) => char.IsDigit(s[i2])) / (double)CS$<>8__locals1.inputLines.Length;
					double num10 = (double)CS$<>8__locals1.inputLines.Count((string s) => i2 >= s.Length - 1 || CS$<>8__locals1.isNonNumeric(s[i2 + 1])) / (double)CS$<>8__locals1.inputLines.Length;
					if (!CS$<>8__locals1.inputLines.Any((string s) => i2 < s.Length - 1 && CS$<>8__locals1.numericSpecialChars.Contains(s[i2 + 1])) && num9 >= num7 && num10 >= num8)
					{
						hashSet2.Add(i2 + 1);
					}
					else
					{
						double num11 = (double)CS$<>8__locals1.inputLines.Count((string s) => CS$<>8__locals1.isNonNumeric(s[i2])) / (double)CS$<>8__locals1.inputLines.Length;
						bool flag3 = !CS$<>8__locals1.inputLines.Any((string s) => CS$<>8__locals1.numericSpecialChars.Contains(s[i2]));
						double num12 = (double)CS$<>8__locals1.inputLines.Count((string s) => i2 < s.Length - 1 && char.IsDigit(s[i2 + 1])) / (double)CS$<>8__locals1.inputLines.Length;
						if (flag3 && num12 >= num7 && num11 >= num8)
						{
							hashSet2.Add(i2 + 1);
						}
					}
				}
				l = i2;
			}
			CS$<>8__locals1.candidatePositions = (from k in CS$<>8__locals1.candidatePositions.Union(hashSet2)
				orderby k
				select k).ToArray<int>();
			HashSet<int> hashSet3 = new HashSet<int>();
			int i;
			for (i = 0; i < CS$<>8__locals1.candidatePositions.Length; i = l + 1)
			{
				int pos = CS$<>8__locals1.candidatePositions[i];
				if (i == 0 && CS$<>8__locals1.inputLines.All((string s) => string.IsNullOrWhiteSpace(s.Substring(0, pos))))
				{
					hashSet3.Add(pos);
				}
				if (i < CS$<>8__locals1.candidatePositions.Length - 1 && CS$<>8__locals1.inputLines.All((string s) => string.IsNullOrWhiteSpace(s.Substring(pos, CS$<>8__locals1.candidatePositions[i + 1] - pos))))
				{
					hashSet3.Add(pos);
				}
				if (i == CS$<>8__locals1.candidatePositions.Length - 1 && CS$<>8__locals1.inputLines.All((string s) => string.IsNullOrWhiteSpace(s.Substring(pos, s.Length - pos))))
				{
					hashSet3.Add(pos);
				}
				l = i;
			}
			CS$<>8__locals1.candidatePositions = CS$<>8__locals1.candidatePositions.Except(hashSet3).ToArray<int>();
			CS$<>8__locals1.candidatePositions = this.MergeConstantColumns(CS$<>8__locals1.candidatePositions, CS$<>8__locals1.inputLines, new Func<string, string, bool>(this.SpacingBetween));
			CS$<>8__locals1.candidatePositions = this.MergeConstantColumns(CS$<>8__locals1.candidatePositions, CS$<>8__locals1.inputLines, new Func<string, string, bool>(this.ContainNumericValues));
			if (CS$<>8__locals1.candidatePositions.IsEmpty<int>())
			{
				return ProgramSet.Empty(CS$<>8__locals1.rule.Head).Some<ProgramSet>();
			}
			return ProgramSetBuilder.List<fixedWidthMatches>(new fixedWidthMatches[] { this._build.Node.Rule.FixedWidth(this._build.Node.Variable.v, this._build.Node.Rule.fieldStartPositions(CS$<>8__locals1.candidatePositions)) }).Set.Some<ProgramSet>();
		}

		// Token: 0x06009C32 RID: 39986 RVA: 0x00212D94 File Offset: 0x00210F94
		private IEnumerable<int> GetAlignedPositions(string[] inputLines, IEnumerable<int> charPositions, int colStart, int colEnd)
		{
			double num = 0.95;
			double num2 = ((inputLines.Length < 10) ? 0.5 : 0.3);
			Func<int, double> func = (int x) => (double)x / (double)inputLines.Length;
			List<int> list = new List<int>();
			using (IEnumerator<int> enumerator = charPositions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int pos = enumerator.Current;
					int num3 = inputLines.Count((string s) => this.IsLeftAligned(s, pos, colEnd));
					if (func(num3) >= num)
					{
						string[] array = inputLines.Where((string s) => pos < s.Length - 1 && !char.IsWhiteSpace(s[pos + 1])).ToArray<string>();
						if (array.Length != 0 && (double)array.Count((string s) => this.HasMultipleAdjacentSpaces(s, pos, 2, true)) / (double)array.Length >= num2)
						{
							list.Add(pos + 1);
						}
					}
					num3 = inputLines.Count((string s) => this.IsRightAligned(s, pos, colStart));
					if (func(num3) >= num)
					{
						string[] array2 = inputLines.Where((string s) => pos > 0 && !char.IsWhiteSpace(s[pos - 1])).ToArray<string>();
						if (array2.Length != 0 && (double)array2.Count((string s) => this.HasMultipleAdjacentSpaces(s, pos, 2, false)) / (double)array2.Length > num2)
						{
							list.Add(pos);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06009C33 RID: 39987 RVA: 0x00212F6C File Offset: 0x0021116C
		private bool IsLeftAligned(string s, int p, int colEnd)
		{
			int num = colEnd - (p + 1);
			if (p == s.Length - 1)
			{
				return true;
			}
			if (p >= s.Length - 1)
			{
				return false;
			}
			if (char.IsWhiteSpace(s[p + 1]))
			{
				IEnumerable<char> enumerable = s.Skip(p + 1).Take(num);
				Func<char, bool> func;
				if ((func = Witnesses.<>O.<3>__IsWhiteSpace) == null)
				{
					func = (Witnesses.<>O.<3>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
				}
				return enumerable.All(func);
			}
			return true;
		}

		// Token: 0x06009C34 RID: 39988 RVA: 0x00212FDC File Offset: 0x002111DC
		private bool IsRightAligned(string s, int p, int colStart)
		{
			int num = p - colStart;
			if (p <= 0)
			{
				return false;
			}
			if (char.IsWhiteSpace(s[p - 1]))
			{
				IEnumerable<char> enumerable = s.Skip(colStart).Take(num);
				Func<char, bool> func;
				if ((func = Witnesses.<>O.<3>__IsWhiteSpace) == null)
				{
					func = (Witnesses.<>O.<3>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
				}
				return enumerable.All(func);
			}
			return true;
		}

		// Token: 0x06009C35 RID: 39989 RVA: 0x00213034 File Offset: 0x00211234
		private bool HasMultipleAdjacentSpaces(string s, int p, int numSpaces, bool onLeft)
		{
			if (onLeft)
			{
				return p >= numSpaces && Enumerable.Range(0, numSpaces).All((int i) => char.IsWhiteSpace(s[p - i])) && (p >= s.Length - 1 || !char.IsWhiteSpace(s[p + 1]));
			}
			return p < s.Length - numSpaces && Enumerable.Range(0, numSpaces).All((int i) => char.IsWhiteSpace(s[p + i])) && (p <= 0 || !char.IsWhiteSpace(s[p - 1]));
		}

		// Token: 0x06009C36 RID: 39990 RVA: 0x00213108 File Offset: 0x00211308
		private int[] MergeConstantColumns(int[] candidatePositions, string[] inputLines, Func<string, string, bool> skipCondition)
		{
			Witnesses.<>c__DisplayClass79_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass79_0();
			CS$<>8__locals1.candidatePositions = candidatePositions;
			HashSet<int> hashSet = new HashSet<int>();
			if (CS$<>8__locals1.candidatePositions.Length != 0)
			{
				Witnesses.<>c__DisplayClass79_1 CS$<>8__locals2 = new Witnesses.<>c__DisplayClass79_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.lastInitialVal = inputLines[0].Substring(0, CS$<>8__locals2.CS$<>8__locals1.candidatePositions[0]);
				bool flag = inputLines.All((string s) => s.Substring(0, CS$<>8__locals2.CS$<>8__locals1.candidatePositions[0]) == CS$<>8__locals2.lastInitialVal);
				Witnesses.<>c__DisplayClass79_2 CS$<>8__locals3 = new Witnesses.<>c__DisplayClass79_2();
				CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
				CS$<>8__locals3.i = 0;
				while (CS$<>8__locals3.i < CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.candidatePositions.Length)
				{
					Witnesses.<>c__DisplayClass79_3 CS$<>8__locals4 = new Witnesses.<>c__DisplayClass79_3();
					CS$<>8__locals4.CS$<>8__locals3 = CS$<>8__locals3;
					Witnesses.<>c__DisplayClass79_3 CS$<>8__locals5 = CS$<>8__locals4;
					Func<string, int, int> func;
					if ((func = CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>9__1) == null)
					{
						func = (CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>9__1 = (string s, int k) => ((k < CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.candidatePositions.Length - 1) ? CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.candidatePositions[k + 1] : s.Length) - CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.candidatePositions[k]);
					}
					CS$<>8__locals5.length = func;
					CS$<>8__locals4.initialVal = inputLines[0].Substring(CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.candidatePositions[CS$<>8__locals4.CS$<>8__locals3.i], CS$<>8__locals4.length(inputLines[0], CS$<>8__locals4.CS$<>8__locals3.i));
					bool flag2 = inputLines.All((string s) => s.Substring(CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.candidatePositions[CS$<>8__locals4.CS$<>8__locals3.i], CS$<>8__locals4.length(s, CS$<>8__locals4.CS$<>8__locals3.i)) == CS$<>8__locals4.initialVal);
					if (flag2 && flag && !skipCondition(CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.lastInitialVal, CS$<>8__locals4.initialVal))
					{
						hashSet.Add(CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.candidatePositions[CS$<>8__locals4.CS$<>8__locals3.i]);
					}
					flag = flag2;
					CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.lastInitialVal = CS$<>8__locals4.initialVal;
					int i = CS$<>8__locals3.i;
					CS$<>8__locals3.i = i + 1;
				}
			}
			return CS$<>8__locals1.candidatePositions.Except(hashSet).ToArray<int>();
		}

		// Token: 0x06009C37 RID: 39991 RVA: 0x00213300 File Offset: 0x00211500
		private bool ContainNumericValues(string left, string right)
		{
			Func<char, bool> func;
			if ((func = Witnesses.<>O.<4>__IsNumber) == null)
			{
				func = (Witnesses.<>O.<4>__IsNumber = new Func<char, bool>(char.IsNumber));
			}
			if (!left.Any(func))
			{
				Func<char, bool> func2;
				if ((func2 = Witnesses.<>O.<4>__IsNumber) == null)
				{
					func2 = (Witnesses.<>O.<4>__IsNumber = new Func<char, bool>(char.IsNumber));
				}
				return right.Any(func2);
			}
			return true;
		}

		// Token: 0x06009C38 RID: 39992 RVA: 0x00213353 File Offset: 0x00211553
		private bool SpacingBetween(string left, string right)
		{
			return (left.Length > 0 && char.IsWhiteSpace(left.Last<char>())) || (right.Length > 0 && char.IsWhiteSpace(right.First<char>()));
		}

		// Token: 0x06009C39 RID: 39993 RVA: 0x00213384 File Offset: 0x00211584
		[RuleLearner("SplitMultiple")]
		public Optional<ProgramSet> LearnSplitMultipleRule(SynthesisEngine engine, GrammarRule rule, LearningTask<WithInputTopSpec> task, CancellationToken cancel)
		{
			IEnumerable<d> enumerable = engine.LearnSymbol(this._build.Symbol.d, task.Spec, default(CancellationToken)).RealizedPrograms.Select(new Func<ProgramNode, d>(this._build.Node.Cast.d));
			multipleMatches? multipleMatches = this.ConstructSplitMultiple(enumerable);
			if (multipleMatches == null)
			{
				return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
			}
			return ProgramSetBuilder.List<multipleMatches>(new multipleMatches[] { multipleMatches.Value }).Set.Some<ProgramSet>();
		}

		// Token: 0x06009C3A RID: 39994 RVA: 0x00213424 File Offset: 0x00211624
		private multipleMatches? ConstructSplitMultiple(IEnumerable<d> delimiterPrograms)
		{
			if (!delimiterPrograms.Any<d>())
			{
				return null;
			}
			return new multipleMatches?(delimiterPrograms.AggregateSeedFunc(new Func<d, multipleMatches>(this._build.Node.UnnamedConversion.multipleMatches_d), new Func<multipleMatches, d, multipleMatches>(this._build.Node.Rule.SplitMultiple)));
		}

		// Token: 0x06009C3B RID: 39995 RVA: 0x00213484 File Offset: 0x00211684
		private delimiterList ConstructDelimitersList(IEnumerable<d> delimiterPrograms)
		{
			return delimiterPrograms.Aggregate(this._build.Node.Rule.EmptyDelimitersList(), new Func<delimiterList, d, delimiterList>(this._build.Node.Rule.DelimitersList));
		}

		// Token: 0x06009C3C RID: 39996 RVA: 0x002134BC File Offset: 0x002116BC
		private extractionPoints ConstructExtPointsList(IEnumerable<cndExtPoint> extPointPrograms)
		{
			return extPointPrograms.Aggregate(this._build.Node.Rule.EmptyExtPointsList(), new Func<extractionPoints, cndExtPoint, extractionPoints>(this._build.Node.Rule.ExtPointsList));
		}

		// Token: 0x06009C3D RID: 39997 RVA: 0x002134F4 File Offset: 0x002116F4
		private cndExtPoint ConstructCndExtPointProgram(Record<int, int, int, int> basePoint, Dictionary<string, Record<int, int, int, int>> branches)
		{
			cndExtPoint cndExtPoint = this._build.Node.UnnamedConversion.cndExtPoint_extPoint(this._build.Node.Rule.extPoint(new Record<int, int, int, int>?(basePoint)));
			foreach (KeyValuePair<string, Record<int, int, int, int>> keyValuePair in branches)
			{
				pred pred = this._build.Node.Rule.SpecialCharPattern(this._build.Node.Variable.v, this._build.Node.Rule.pattern(keyValuePair.Key));
				extPoint extPoint = this._build.Node.Rule.extPoint(new Record<int, int, int, int>?(keyValuePair.Value));
				cndExtPoint = this._build.Node.Rule.ConditionalExtract(pred, extPoint, cndExtPoint);
			}
			return cndExtPoint;
		}

		// Token: 0x06009C3E RID: 39998 RVA: 0x002135F8 File Offset: 0x002117F8
		private splitMatches? LearnInconsistentDelimiterPrograms(string[] inputLines)
		{
			IEnumerable<string> enumerable = (from string s in base.Config.TerminalGenerators["s"]
				where !string.IsNullOrWhiteSpace(s)
				select s.Trim() into s
				where s.Length == 1
				select s).Concat(Witnesses.CommonDelScores.Keys).Distinct<string>();
			Dictionary<string, int> delTotalOccurrences = enumerable.ToDictionary((string s) => s, delegate(string s)
			{
				Func<char, bool> <>9__12;
				return inputLines.Sum(delegate(string line)
				{
					Func<char, bool> func;
					if ((func = <>9__12) == null)
					{
						func = (<>9__12 = (char c) => c == s[0]);
					}
					return line.Count(func);
				});
			});
			Dictionary<string, int> numDelRows = enumerable.ToDictionary((string s) => s, (string s) => inputLines.Count((string line) => line.Contains(s)));
			int num = inputLines.Length;
			int minRows = Math.Max(4, num * 5 / 100);
			string text = (from d in enumerable
				where numDelRows[d] > minRows
				orderby Witnesses.CommonDelScores.MaybeGet(d).OrElse(0) descending, delTotalOccurrences[d] descending
				select d).FirstOrDefault<string>();
			if (text == null)
			{
				if (inputLines.Any((string line) => line.Contains(" ")))
				{
					text = " ";
				}
			}
			if (text == null)
			{
				return null;
			}
			return new splitMatches?(this._build.Node.UnnamedConversion.splitMatches_multipleMatches(this.GetDelimiterProgram(new string[] { text }).Value));
		}

		// Token: 0x06009C3F RID: 39999 RVA: 0x002137FC File Offset: 0x002119FC
		private splitMatches? LearnDelimiterPrograms(SynthesisEngine engine, LearningTask<WithInputTopSpec> task, CancellationToken cancel, bool zeroLength)
		{
			engine.ClearLearningCache();
			if (zeroLength)
			{
				this.LearnZeroLengthDelimiters = true;
				base.Config.TerminalGenerators["s"] = base.Config.TerminalGenerators["s"].AppendItem("").ToArray<object>();
			}
			else
			{
				this.LearnZeroLengthDelimiters = false;
				base.Config.TerminalGenerators["s"] = base.Config.TerminalGenerators["s"].Where((object o) => o as string != "").ToArray<object>();
			}
			return engine.LearnSymbol(this._build.Symbol.multipleMatches, task.Spec, cancel).RealizedPrograms.Select(new Func<ProgramNode, multipleMatches>(this._build.Node.Cast.multipleMatches)).Select(new Func<multipleMatches, splitMatches>(this._build.Node.UnnamedConversion.splitMatches_multipleMatches)).FirstOrNull<splitMatches>();
		}

		// Token: 0x06009C40 RID: 40000 RVA: 0x00213916 File Offset: 0x00211B16
		private static bool IsDelimiterRegex(RegularExpression regex)
		{
			if (regex.Count > 0)
			{
				return regex.Tokens.All((Token t) => t.IsSymbol);
			}
			return false;
		}

		// Token: 0x06009C41 RID: 40001 RVA: 0x00213950 File Offset: 0x00211B50
		private Dictionary<int, Dictionary<string, Record<int, int>?>> GetPositionExamples(Dictionary<int, Dictionary<string, string>> stringExamples)
		{
			Dictionary<int, Dictionary<string, Record<int, int>?>> dictionary = new Dictionary<int, Dictionary<string, Record<int, int>?>>();
			Dictionary<string, List<Record<int, int>>> dictionary2 = new Dictionary<string, List<Record<int, int>>>();
			string[] array = stringExamples.SelectMany((KeyValuePair<int, Dictionary<string, string>> kvp) => kvp.Value.Keys).Distinct<string>().ToArray<string>();
			Dictionary<string, List<int>> dictionary3 = new Dictionary<string, List<int>>();
			foreach (int num in stringExamples.Keys.OrderBy((int x) => x))
			{
				foreach (string text in stringExamples[num].Keys)
				{
					dictionary3.GetOrCreateValue(text).Add(num);
				}
			}
			foreach (string text2 in array)
			{
				foreach (int num2 in dictionary3[text2])
				{
					if (!dictionary.ContainsKey(num2))
					{
						dictionary[num2] = new Dictionary<string, Record<int, int>?>();
					}
					string text3 = stringExamples[num2][text2];
					List<Record<int, int>> orCreateValue = dictionary2.GetOrCreateValue(text2);
					Record<int, int>? firstDisjointOccurrence = this.GetFirstDisjointOccurrence(text2, text3, orCreateValue);
					dictionary[num2][text2] = firstDisjointOccurrence;
					if (firstDisjointOccurrence != null)
					{
						dictionary2[text2].Add(firstDisjointOccurrence.Value);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06009C42 RID: 40002 RVA: 0x00213B28 File Offset: 0x00211D28
		private Dictionary<int, Dictionary<string, Record<int, int>[]>> GetNegativePositionExamples(Dictionary<int, Dictionary<string, Record<int, int>?>> positionExamples)
		{
			Dictionary<int, Dictionary<string, Record<int, int>[]>> dictionary = new Dictionary<int, Dictionary<string, Record<int, int>[]>>();
			string[] array = positionExamples.SelectMany((KeyValuePair<int, Dictionary<string, Record<int, int>?>> kvp) => kvp.Value.Keys).Distinct<string>().ToArray<string>();
			foreach (KeyValuePair<int, Dictionary<string, Record<int, int>?>> keyValuePair in positionExamples)
			{
				dictionary[keyValuePair.Key] = new Dictionary<string, Record<int, int>[]>();
			}
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string input = array2[i];
				Record<int, int>[] array3 = (from p in positionExamples
					where p.Value.ContainsKey(input)
					select p.Value[input] into t
					where t != null
					select t.Value into t
					orderby t.Item1
					select t).ToArray<Record<int, int>>();
				Func<KeyValuePair<int, Dictionary<string, Record<int, int>?>>, bool> func;
				Func<KeyValuePair<int, Dictionary<string, Record<int, int>?>>, bool> <>9__6;
				if ((func = <>9__6) == null)
				{
					func = (<>9__6 = (KeyValuePair<int, Dictionary<string, Record<int, int>?>> p) => !p.Value.ContainsKey(input));
				}
				foreach (KeyValuePair<int, Dictionary<string, Record<int, int>?>> keyValuePair2 in positionExamples.Where(func))
				{
					int key = keyValuePair2.Key;
					dictionary[key][input] = array3;
				}
			}
			return dictionary;
		}

		// Token: 0x06009C43 RID: 40003 RVA: 0x00213D00 File Offset: 0x00211F00
		private void InitializeInputLines(string[] inputLines)
		{
			this._inputLines = inputLines;
			this._startPositionDelimiter = this._inputLines.Select((string l) => new MatchRecord(new int[1], new int[1])).ToArray<MatchRecord>();
			this._endPositionDelimiter = this._inputLines.Select((string l) => new MatchRecord(new int[] { l.Length }, new int[] { l.Length })).ToArray<MatchRecord>();
			this._inputRelevantEndPoints.Clear();
			this._inputStringIndexes.Clear();
			for (int i = 0; i < this._inputLines.Length; i++)
			{
				string text = this._inputLines[i];
				IDictionary<string, Record<int, int>> inputRelevantEndPoints = this._inputRelevantEndPoints;
				string text2 = text;
				Func<string, Record<int, int>> func;
				if ((func = Witnesses.<>O.<5>__GetRelevantEndPoints) == null)
				{
					func = (Witnesses.<>O.<5>__GetRelevantEndPoints = new Func<string, Record<int, int>>(Witnesses.GetRelevantEndPoints));
				}
				inputRelevantEndPoints.GetOrAdd(text2, func);
				this._inputStringIndexes.GetOrAdd(text, i);
				this._inputStringFormats.GetOrAdd(text, new Func<string, string>(this.GetSpecialCharFormat));
			}
			foreach (KeyValuePair<string, string> keyValuePair in this._inputStringFormats)
			{
				this._inputFormatFrequencies[keyValuePair.Value] = this._inputFormatFrequencies.GetOrAdd(keyValuePair.Value, 0) + 1;
			}
		}

		// Token: 0x06009C44 RID: 40004 RVA: 0x00213E68 File Offset: 0x00212068
		private string GetSpecialCharFormat(string s)
		{
			return new string(s.Where((char c) => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)).ToArray<char>());
		}

		// Token: 0x06009C45 RID: 40005 RVA: 0x00213E99 File Offset: 0x00212099
		private int GetNumGivenExamples()
		{
			if (this.ExampleConstraints == null)
			{
				return 0;
			}
			return this.ExampleConstraints.SelectMany((KeyValuePair<int, Dictionary<string, string>> k) => k.Value.Keys).Distinct<string>().Count<string>();
		}

		// Token: 0x06009C46 RID: 40006 RVA: 0x00213EDC File Offset: 0x002120DC
		private IEnumerable<RegularExpression> GetNonEmptyRegexes(LearningTask<WithInputTopSpec> task)
		{
			StringRegion[] array = task.Spec.ProvidedInputs.Select((State s) => (StringRegion)s[base.Grammar.InputSymbol]).ToArray<StringRegion>();
			HashSet<Token> hashSet = new HashSet<Token>();
			foreach (StringRegion stringRegion in array)
			{
				IEnumerable<Token> enumerable = from kvp in stringRegion.Cache.GetAllTokensMatchPositions(0U, stringRegion.Length)
					where kvp.Value.All((PositionMatch m) => m.Length > 0U)
					select kvp.Key;
				hashSet.AddRange(enumerable);
			}
			return hashSet.Select((Token t) => RegularExpression.Create(new Token[] { t }, 0)).ToArray<RegularExpression>();
		}

		// Token: 0x06009C47 RID: 40007 RVA: 0x00213FB4 File Offset: 0x002121B4
		[RuleLearner("ExtractionSplit")]
		public Optional<ProgramSet> LearnExtractionSplitRule(SynthesisEngine engine, GrammarRule rule, LearningTask<WithInputTopSpec> task, CancellationToken cancel)
		{
			if (this.ExampleConstraints == null)
			{
				return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
			}
			string[] array = task.Spec.ProvidedInputs.Select((State s) => ((StringRegion)s[rule.Grammar.InputSymbol]).Value).ToArray<string>();
			this.InitializeInputLines(array);
			if (base.LearnedPrograms == null || this._learnedTopAlignedDelimiterSets == null || this._inputLines == null || !array.SequenceEqual(this._inputLines))
			{
				this.LearnDelimiterPrograms(engine, task, cancel, false);
			}
			if (base.LearnedPrograms == null || this._learnedTopAlignedDelimiterSets == null || this._inputLines == null || !array.SequenceEqual(this._inputLines))
			{
				return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
			}
			int[] array2 = Enumerable.Range(0, this.ExampleConstraints.Count).ToArray<int>();
			if (!array2.SequenceEqual(this.ExampleConstraints.Keys.OrderBy((int x) => x)))
			{
				throw new Exception("Examples must be given for all columns");
			}
			Dictionary<int, Dictionary<string, Record<int, int>?>> positionExamples = this.GetPositionExamples(this.ExampleConstraints);
			Dictionary<int, Dictionary<string, Record<int, int>[]>> negativePositionExamples = this.GetNegativePositionExamples(positionExamples);
			Dictionary<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>> dictionary = this.LearnExtractions(positionExamples, negativePositionExamples, cancel);
			if (dictionary == null)
			{
				base.Config.TerminalGenerators["fregex"] = base.Config.TerminalGenerators["fregex"].Concat(this.GetNonEmptyRegexes(task)).ToArray<object>();
				this.LearnDelimiterPrograms(engine, task, cancel, false);
				dictionary = this.LearnExtractions(positionExamples, negativePositionExamples, cancel);
			}
			if (dictionary == null)
			{
				this.LearnDelimiterPrograms(engine, task, cancel, true);
				dictionary = this.LearnExtractions(positionExamples, negativePositionExamples, cancel);
			}
			if (dictionary == null)
			{
				return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
			}
			Dictionary<MatchRecord[], int> dictionary2 = new Dictionary<MatchRecord[], int>();
			int num = 0;
			foreach (Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos> record in dictionary.Values)
			{
				List<MatchRecord[]> list = new List<MatchRecord[]>();
				list.Add(record.Item1.DelimiterMatches);
				list.AddRange(record.Item1.ConditionalPos.Select((KeyValuePair<string, Witnesses.DelimiterPos> kvp) => kvp.Value.DelimiterMatches));
				list.Add(record.Item2.DelimiterMatches);
				list.AddRange(record.Item2.ConditionalPos.Select((KeyValuePair<string, Witnesses.DelimiterPos> kvp) => kvp.Value.DelimiterMatches));
				foreach (MatchRecord[] array3 in list)
				{
					if (!dictionary2.ContainsKey(array3) && array3 != this._startPositionDelimiter && array3 != this._endPositionDelimiter)
					{
						dictionary2[array3] = num;
						num++;
					}
				}
			}
			Record<int, int, int, int>[] array4 = new Record<int, int, int, int>[array2.Length];
			Dictionary<string, Record<int, int, int, int>>[] cndExtPoints = new Dictionary<string, Record<int, int, int, int>>[array2.Length];
			foreach (int num2 in array2)
			{
				cndExtPoints[num2] = new Dictionary<string, Record<int, int, int, int>>();
				Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos> record2 = dictionary[num2];
				array4[num2] = this.GetExtractionPoint(record2.Item1, record2.Item2, dictionary2);
				foreach (string text in record2.Item1.ConditionalPos.Keys.Concat(record2.Item2.ConditionalPos.Keys).Distinct<string>().ToArray<string>())
				{
					Witnesses.DelimiterPos delimiterPos;
					record2.Item1.ConditionalPos.TryGetValue(text, out delimiterPos);
					Witnesses.DelimiterPos delimiterPos2;
					record2.Item2.ConditionalPos.TryGetValue(text, out delimiterPos2);
					cndExtPoints[num2][text] = this.GetExtractionPoint(delimiterPos ?? record2.Item1, delimiterPos2 ?? record2.Item2, dictionary2);
				}
			}
			IEnumerable<cndExtPoint> enumerable = array4.Select((Record<int, int, int, int> e, int i) => this.ConstructCndExtPointProgram(e, cndExtPoints[i]));
			extractionPoints extractionPoints = this.ConstructExtPointsList(enumerable);
			IEnumerable<d> enumerable2 = from d in (from kvp in dictionary2
					orderby kvp.Value
					select kvp.Key).ToArray<MatchRecord[]>()
				select this._build.Node.Cast.d(this.LearnedPrograms[this._build.Symbol.d][d].Program);
			delimiterList delimiterList = this.ConstructDelimitersList(enumerable2);
			return ProgramSetBuilder.List<regionSplit>(new regionSplit[] { this._build.Node.Rule.ExtractionSplit(this._build.Node.Variable.v, delimiterList, extractionPoints) }).Set.Some<ProgramSet>();
		}

		// Token: 0x06009C48 RID: 40008 RVA: 0x002144F0 File Offset: 0x002126F0
		private Record<int, int, int, int> GetExtractionPoint(Witnesses.DelimiterPos d1, Witnesses.DelimiterPos d2, Dictionary<MatchRecord[], int> delimiterIndexMap)
		{
			int num;
			int num2;
			if (d1.DelimiterMatches == this._startPositionDelimiter || d1.DelimiterMatches == this._endPositionDelimiter)
			{
				num = -1;
				num2 = -1;
			}
			else
			{
				num = delimiterIndexMap[d1.DelimiterMatches];
				num2 = d1.Index;
			}
			int num3;
			int num4;
			if (d2.DelimiterMatches == this._startPositionDelimiter || d2.DelimiterMatches == this._endPositionDelimiter)
			{
				num3 = -1;
				num4 = -1;
			}
			else
			{
				num3 = delimiterIndexMap[d2.DelimiterMatches];
				num4 = d2.Index;
			}
			return Record.Create<int, int, int, int>(num, num2, num3, num4);
		}

		// Token: 0x06009C49 RID: 40009 RVA: 0x00214574 File Offset: 0x00212774
		private Dictionary<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>> LearnExtractions(Dictionary<int, Dictionary<string, Record<int, int>?>> positionExamples, Dictionary<int, Dictionary<string, Record<int, int>[]>> negativePositionExamples, CancellationToken cancel)
		{
			Dictionary<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>> dictionary = new Dictionary<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>>();
			Dictionary<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>> dictionary2 = new Dictionary<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>>();
			foreach (KeyValuePair<int, Dictionary<string, Record<int, int>?>> keyValuePair in positionExamples)
			{
				int key = keyValuePair.Key;
				List<Witnesses.DelimiterPos> satisfyingDelimiterPositions = this.GetSatisfyingDelimiterPositions(true, keyValuePair.Value, negativePositionExamples[key]);
				List<Witnesses.DelimiterPos> satisfyingDelimiterPositions2 = this.GetSatisfyingDelimiterPositions(false, keyValuePair.Value, negativePositionExamples[key]);
				if (satisfyingDelimiterPositions.IsEmpty<Witnesses.DelimiterPos>() || satisfyingDelimiterPositions2.IsEmpty<Witnesses.DelimiterPos>())
				{
					return null;
				}
				dictionary2[key] = Record.Create<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>(satisfyingDelimiterPositions, satisfyingDelimiterPositions2);
			}
			HashSet<MatchRecord[]> topRankedDelimiters = this._learnedTopAlignedDelimiterSets.SelectMany((HashSet<MatchRecord[]> s) => s).ConvertToHashSet<MatchRecord[]>();
			Func<Witnesses.DelimiterPos, int> priorityScore = delegate(Witnesses.DelimiterPos d)
			{
				if (d.DelimiterMatches != this._startPositionDelimiter && d.DelimiterMatches != this._endPositionDelimiter && !topRankedDelimiters.Contains(d.DelimiterMatches) && !d.ConditionalPos.IsEmpty<KeyValuePair<string, Witnesses.DelimiterPos>>())
				{
					return 0;
				}
				return 1;
			};
			KeyValuePair<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>>[] array = (from kvp in dictionary2
				orderby priorityScore(kvp.Value.Item1[0]) + priorityScore(kvp.Value.Item2[0]) descending, kvp.Key
				select kvp).ToArray<KeyValuePair<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>>>();
			List<Record<int, int>>[] array2 = this._inputLines.Select((string l) => new List<Record<int, int>>()).ToArray<List<Record<int, int>>>();
			IEnumerable<KeyValuePair<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>>> disjointColumnExtractions = this.GetDisjointColumnExtractions(array2, array, cancel);
			if (disjointColumnExtractions == null)
			{
				foreach (KeyValuePair<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>> keyValuePair2 in array)
				{
					Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos, List<Record<int, int>>[]> record = this.GetColumnExtraction(this._inputLines.Select((string l) => new List<Record<int, int>>()).ToArray<List<Record<int, int>>>(), keyValuePair2.Value.Item1, keyValuePair2.Value.Item2, cancel).FirstOrDefault<Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos, List<Record<int, int>>[]>>();
					dictionary[keyValuePair2.Key] = Record.Create<Witnesses.DelimiterPos, Witnesses.DelimiterPos>(record.Item1, record.Item2);
				}
			}
			else
			{
				foreach (KeyValuePair<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>> keyValuePair3 in disjointColumnExtractions)
				{
					dictionary[keyValuePair3.Key] = keyValuePair3.Value;
				}
			}
			return dictionary;
		}

		// Token: 0x06009C4A RID: 40010 RVA: 0x002147F0 File Offset: 0x002129F0
		private IEnumerable<KeyValuePair<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>>> GetDisjointColumnExtractions(List<Record<int, int>>[] disjointPositions, KeyValuePair<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>>[] columnLists, CancellationToken cancel)
		{
			if (columnLists.IsEmpty<KeyValuePair<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>>>())
			{
				return new KeyValuePair<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>>[0];
			}
			KeyValuePair<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>> keyValuePair = columnLists[0];
			KeyValuePair<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>>[] array = columnLists.Skip(1).ToArray<KeyValuePair<int, Record<List<Witnesses.DelimiterPos>, List<Witnesses.DelimiterPos>>>>();
			foreach (Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos, List<Record<int, int>>[]> record in this.GetColumnExtraction(disjointPositions, keyValuePair.Value.Item1, keyValuePair.Value.Item2, cancel).Take(10))
			{
				IEnumerable<KeyValuePair<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>>> disjointColumnExtractions = this.GetDisjointColumnExtractions(record.Item3, array, cancel);
				if (disjointColumnExtractions != null)
				{
					if (cancel.IsCancellationRequested)
					{
						return null;
					}
					List<KeyValuePair<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>>> list = new List<KeyValuePair<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>>>();
					KeyValuePair<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>> keyValuePair2 = new KeyValuePair<int, Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos>>(keyValuePair.Key, Record.Create<Witnesses.DelimiterPos, Witnesses.DelimiterPos>(record.Item1, record.Item2));
					list.Add(keyValuePair2);
					list.AddRange(disjointColumnExtractions);
					return list;
				}
			}
			return null;
		}

		// Token: 0x06009C4B RID: 40011 RVA: 0x002148DC File Offset: 0x00212ADC
		private IEnumerable<Record<Witnesses.DelimiterPos, Witnesses.DelimiterPos, List<Record<int, int>>[]>> GetColumnExtraction(List<Record<int, int>>[] disjointPositions, List<Witnesses.DelimiterPos> startList, List<Witnesses.DelimiterPos> endList, CancellationToken cancel)
		{
			IEnumerable<Witnesses.DelimiterPos> enumerable = startList.Where((Witnesses.DelimiterPos d) => d.IsDisjoint(disjointPositions));
			IEnumerable<Witnesses.DelimiterPos> disjointEnds = endList.Where((Witnesses.DelimiterPos d) => d.IsDisjoint(disjointPositions));
			foreach (Witnesses.DelimiterPos start in enumerable)
			{
				foreach (Witnesses.DelimiterPos delimiterPos in disjointEnds)
				{
					if (cancel.IsCancellationRequested)
					{
						yield break;
					}
					List<Record<int, int>>[] array = Witnesses.DelimiterPos.CheckConsistentStartEndPair(disjointPositions, start, delimiterPos);
					if (array != null)
					{
						yield return Record.Create<Witnesses.DelimiterPos, Witnesses.DelimiterPos, List<Record<int, int>>[]>(start, delimiterPos, array);
					}
				}
				IEnumerator<Witnesses.DelimiterPos> enumerator2 = null;
				start = null;
			}
			IEnumerator<Witnesses.DelimiterPos> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06009C4C RID: 40012 RVA: 0x00214904 File Offset: 0x00212B04
		private bool IsEmptyOrWhitespace(MatchRecord[] delimiterMatches)
		{
			for (int i = 0; i < delimiterMatches.Length; i++)
			{
				if (delimiterMatches[i].StartIndexes.Count > 0)
				{
					for (int j = 0; j < delimiterMatches[i].StartIndexes.Count; j++)
					{
						int num = delimiterMatches[i].StartIndexes[j];
						int num2 = delimiterMatches[i].EndIndexes[j];
						if (num == num2 || string.IsNullOrWhiteSpace(this._inputLines[i].Substring(num, num2 - num)))
						{
							return true;
						}
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06009C4D RID: 40013 RVA: 0x00214988 File Offset: 0x00212B88
		private List<Witnesses.DelimiterPos> GetSatisfyingDelimiterPositions(bool isStart, Dictionary<string, Record<int, int>?> positionExamples, Dictionary<string, Record<int, int>[]> negativePositionExamples)
		{
			if (isStart)
			{
				if (positionExamples.All((KeyValuePair<string, Record<int, int>?> kvp) => kvp.Value != null && kvp.Value.Value.Item1 == 0))
				{
					return new List<Witnesses.DelimiterPos>
					{
						new Witnesses.DelimiterPos(this._startPositionDelimiter, 0, true, new HashSet<int>(), this, null)
					};
				}
			}
			if (!isStart)
			{
				if (positionExamples.All((KeyValuePair<string, Record<int, int>?> kvp) => kvp.Value != null && kvp.Value.Value.Item2 == kvp.Key.Length))
				{
					return new List<Witnesses.DelimiterPos>
					{
						new Witnesses.DelimiterPos(this._endPositionDelimiter, 0, false, new HashSet<int>(), this, null)
					};
				}
			}
			List<Witnesses.DelimiterPos> list = new List<Witnesses.DelimiterPos>();
			HashSet<MatchRecord[]> collectedDelimiters = new HashSet<MatchRecord[]>();
			Func<MatchRecord[], bool> <>9__9;
			Func<MatchRecord[], Witnesses.DelimiterPos> <>9__10;
			Func<Witnesses.DelimiterPos, int> <>9__12;
			Func<Witnesses.DelimiterPos, int> <>9__13;
			Func<Witnesses.DelimiterPos, int> <>9__14;
			foreach (IEnumerable<MatchRecord[]> enumerable in this._learnedTopAlignedDelimiterSets.Where((HashSet<MatchRecord[]> set) => set.Count > 1))
			{
				Func<MatchRecord[], bool> func;
				if ((func = <>9__9) == null)
				{
					func = (<>9__9 = (MatchRecord[] d) => !collectedDelimiters.Contains(d));
				}
				IEnumerable<MatchRecord[]> enumerable2 = enumerable.Where(func);
				Func<MatchRecord[], Witnesses.DelimiterPos> func2;
				if ((func2 = <>9__10) == null)
				{
					func2 = (<>9__10 = (MatchRecord[] d) => this.GetSatisfyingDelimiterPos(d, isStart, positionExamples, negativePositionExamples));
				}
				IEnumerable<Witnesses.DelimiterPos> enumerable3 = from t in enumerable2.Select(func2)
					where t != null
					select t;
				Func<Witnesses.DelimiterPos, int> func3;
				if ((func3 = <>9__12) == null)
				{
					func3 = (<>9__12 = (Witnesses.DelimiterPos t) => (!this.IsEmptyOrWhitespace(t.DelimiterMatches)) ? 1 : 0);
				}
				IOrderedEnumerable<Witnesses.DelimiterPos> orderedEnumerable = enumerable3.OrderByDescending(func3);
				Func<Witnesses.DelimiterPos, int> func4;
				if ((func4 = <>9__13) == null)
				{
					func4 = (<>9__13 = (Witnesses.DelimiterPos t) => this._learnedDelimitersMaxFrequencyMap[t.DelimiterMatches].Item2);
				}
				IOrderedEnumerable<Witnesses.DelimiterPos> orderedEnumerable2 = orderedEnumerable.ThenByDescending(func4);
				Func<Witnesses.DelimiterPos, int> func5;
				if ((func5 = <>9__14) == null)
				{
					func5 = (<>9__14 = (Witnesses.DelimiterPos t) => this._learnedDelimitersMaxFrequencyMap[t.DelimiterMatches].Item1);
				}
				IOrderedEnumerable<Witnesses.DelimiterPos> orderedEnumerable3 = orderedEnumerable2.ThenByDescending(func5);
				list.AddRange(orderedEnumerable3);
				collectedDelimiters.AddRange(orderedEnumerable3.Select((Witnesses.DelimiterPos t) => t.DelimiterMatches));
			}
			IOrderedEnumerable<Witnesses.DelimiterPos> orderedEnumerable4 = from d in this._learnedDelimiters
				where !collectedDelimiters.Contains(d)
				select this.GetSatisfyingDelimiterPos(d, isStart, positionExamples, negativePositionExamples) into t
				where t != null
				orderby (!this.IsEmptyOrWhitespace(t.DelimiterMatches)) ? 1 : 0 descending, this._learnedDelimitersMaxFrequencyMap[t.DelimiterMatches].Item2 descending, this._learnedDelimitersMaxFrequencyMap[t.DelimiterMatches].Item1 descending
				select t;
			list.AddRange(orderedEnumerable4);
			list.AddRange(this.GetSatisfyingConditionalDelimiterPositions(isStart, positionExamples));
			return list;
		}

		// Token: 0x06009C4E RID: 40014 RVA: 0x00214C9C File Offset: 0x00212E9C
		private List<Witnesses.DelimiterPos> GetSatisfyingConditionalDelimiterPositions(bool isStart, Dictionary<string, Record<int, int>?> positionExamples)
		{
			Dictionary<string, Record<int, int>?> nonEmptyPositionExamples = positionExamples.Where((KeyValuePair<string, Record<int, int>?> kvp) => kvp.Value != null).ToDictionary<string, Record<int, int>?>();
			if (nonEmptyPositionExamples.Select((KeyValuePair<string, Record<int, int>?> kvp) => this._inputStringFormats[kvp.Key]).Distinct<string>().Count<string>() <= 1)
			{
				return new List<Witnesses.DelimiterPos>();
			}
			IEnumerable<Dictionary<string, Record<int, int>?>> enumerable = nonEmptyPositionExamples.Select((KeyValuePair<string, Record<int, int>?> kvp) => new Dictionary<string, Record<int, int>?> { { kvp.Key, kvp.Value } }).ToArray<Dictionary<string, Record<int, int>?>>();
			Dictionary<string, Record<int, int>[]> emptyNegativeExamples = new Dictionary<string, Record<int, int>[]>();
			IEnumerable<List<Witnesses.DelimiterPos>> enumerable2 = enumerable.Select((Dictionary<string, Record<int, int>?> e) => this.GetSatisfyingDelimiterPositions(isStart, e, emptyNegativeExamples));
			if (enumerable2.Any((List<Witnesses.DelimiterPos> l) => l.IsEmpty<Witnesses.DelimiterPos>()))
			{
				return new List<Witnesses.DelimiterPos>();
			}
			IOrderedEnumerable<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>> orderedEnumerable = from d in enumerable2.SelectMany((List<Witnesses.DelimiterPos> l) => l).ToDictionary((Witnesses.DelimiterPos d) => d, (Witnesses.DelimiterPos d) => d.GetSatisfiedExamples(nonEmptyPositionExamples))
				group d by d.Key.DelimiterMatches into g
				select g.ArgMax((KeyValuePair<Witnesses.DelimiterPos, HashSet<string>> kvp) => kvp.Value.Count) into kvp
				orderby kvp.Value.Count descending, this.GetTotalFormatFrequency(kvp.Value) descending
				select kvp;
			List<HashSet<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>>> list = new List<HashSet<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>>>();
			HashSet<string> hashSet = (from kvp in positionExamples
				where kvp.Value != null
				select kvp.Key).ConvertToHashSet<string>();
			foreach (KeyValuePair<Witnesses.DelimiterPos, HashSet<string>> keyValuePair in orderedEnumerable)
			{
				HashSet<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>> hashSet2 = new HashSet<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>> { keyValuePair };
				HashSet<string> hashSet3 = new HashSet<string>(keyValuePair.Value);
				if (hashSet3.IsSupersetOf(hashSet))
				{
					list.Add(hashSet2);
					new HashSet<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>>().Add(keyValuePair);
					hashSet3 = new HashSet<string>(keyValuePair.Value);
				}
				else
				{
					foreach (KeyValuePair<Witnesses.DelimiterPos, HashSet<string>> keyValuePair2 in orderedEnumerable)
					{
						if (!keyValuePair2.Value.IsSubsetOf(hashSet3))
						{
							hashSet2.Add(keyValuePair2);
							hashSet3.AddRange(keyValuePair2.Value);
							if (hashSet3.IsSupersetOf(hashSet))
							{
								list.Add(hashSet2);
								hashSet2 = new HashSet<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>> { keyValuePair };
								hashSet3 = new HashSet<string>(keyValuePair.Value);
							}
						}
					}
				}
			}
			HashSet<int> hashSet4 = (from kvp in positionExamples
				where kvp.Value == null
				select this._inputStringIndexes[kvp.Key]).ConvertToHashSet<int>();
			List<Witnesses.DelimiterPos> list2 = new List<Witnesses.DelimiterPos>();
			Func<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>, int> <>9__17;
			foreach (IEnumerable<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>> enumerable3 in list)
			{
				Func<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>, int> func;
				if ((func = <>9__17) == null)
				{
					func = (<>9__17 = (KeyValuePair<Witnesses.DelimiterPos, HashSet<string>> kvp) => this.GetTotalFormatFrequency(kvp.Value));
				}
				KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>[] array = enumerable3.OrderByDescending(func).ToArray<KeyValuePair<Witnesses.DelimiterPos, HashSet<string>>>();
				Witnesses.DelimiterPos key = array[0].Key;
				HashSet<string> value = array[0].Value;
				Dictionary<string, Witnesses.DelimiterPos> dictionary = new Dictionary<string, Witnesses.DelimiterPos>();
				foreach (KeyValuePair<Witnesses.DelimiterPos, HashSet<string>> keyValuePair3 in array.Skip(1))
				{
					foreach (string text in keyValuePair3.Value.Except(value))
					{
						string text2 = this._inputStringFormats[text];
						dictionary.GetOrAdd(text2, keyValuePair3.Key);
					}
				}
				Witnesses.DelimiterPos delimiterPos = new Witnesses.DelimiterPos(key.DelimiterMatches, key.Index, key.IsStart, hashSet4, this, dictionary);
				if (delimiterPos.GetSatisfiedExamples(positionExamples).SetEquals(positionExamples.Keys))
				{
					list2.Add(delimiterPos);
				}
			}
			return list2;
		}

		// Token: 0x06009C4F RID: 40015 RVA: 0x002151C8 File Offset: 0x002133C8
		private int GetTotalFormatFrequency(HashSet<string> set)
		{
			return set.Select((string s) => this._inputFormatFrequencies[this._inputStringFormats[s]]).Sum();
		}

		// Token: 0x06009C50 RID: 40016 RVA: 0x002151E4 File Offset: 0x002133E4
		private Witnesses.DelimiterPos GetSatisfyingDelimiterPos(MatchRecord[] delimiter, bool isStartPosition, Dictionary<string, Record<int, int>?> positionExamples, Dictionary<string, Record<int, int>[]> negativePositionExamples)
		{
			int num = -1;
			int num2 = -1;
			bool flag = true;
			if (positionExamples.IsEmpty<KeyValuePair<string, Record<int, int>?>>())
			{
				throw new ArgumentException("No positive position examples found");
			}
			IEnumerable<KeyValuePair<string, Record<int, int>>> enumerable = from kvp in positionExamples
				where kvp.Value != null
				select new KeyValuePair<string, Record<int, int>>(kvp.Key, kvp.Value.Value);
			if (enumerable.IsEmpty<KeyValuePair<string, Record<int, int>>>())
			{
				return null;
			}
			foreach (KeyValuePair<string, Record<int, int>> keyValuePair in enumerable)
			{
				if (!this._inputStringIndexes.ContainsKey(keyValuePair.Key))
				{
					throw new ArgumentException("Encountered new unknown input");
				}
				int num3 = this._inputStringIndexes[keyValuePair.Key];
				MatchRecord matchRecord = delimiter[num3];
				int num4;
				IReadOnlyList<int> readOnlyList;
				if (isStartPosition)
				{
					num4 = keyValuePair.Value.Item1;
					readOnlyList = matchRecord.EndIndexes;
				}
				else
				{
					num4 = keyValuePair.Value.Item2;
					readOnlyList = matchRecord.StartIndexes;
				}
				if (flag)
				{
					int? num5 = readOnlyList.IndexOf(num4);
					if (num5 == null)
					{
						return null;
					}
					num = num5.Value;
					num2 = matchRecord.NumMatches - num5.Value;
					flag = false;
				}
				else
				{
					if (num >= 0 && (matchRecord.NumMatches <= num || readOnlyList[num] != num4))
					{
						num = -1;
					}
					if (num2 > 0 && (matchRecord.NumMatches < num2 || readOnlyList[matchRecord.NumMatches - num2] != num4))
					{
						num2 = -1;
					}
					if (num < 0 && num2 < 0)
					{
						return null;
					}
				}
			}
			foreach (KeyValuePair<string, Record<int, int>[]> keyValuePair2 in negativePositionExamples)
			{
				if (!this._inputStringIndexes.ContainsKey(keyValuePair2.Key))
				{
					throw new Exception("Encountered new unknown input");
				}
				int num6 = this._inputStringIndexes[keyValuePair2.Key];
				MatchRecord matchRecord2 = delimiter[num6];
				IReadOnlyList<int> readOnlyList2 = (isStartPosition ? matchRecord2.EndIndexes : matchRecord2.StartIndexes);
				if (num >= 0 && matchRecord2.NumMatches > num)
				{
					int num7 = readOnlyList2[num];
					foreach (Record<int, int> record in keyValuePair2.Value)
					{
						if ((num7 > record.Item1 && num7 < record.Item2) || (isStartPosition && num7 == record.Item1) || (!isStartPosition && num7 == record.Item2))
						{
							return null;
						}
						if (num7 < record.Item1)
						{
							break;
						}
					}
				}
				if (num2 > 0 && matchRecord2.NumMatches >= num)
				{
					int num8 = readOnlyList2[matchRecord2.NumMatches - num2];
					foreach (Record<int, int> record2 in keyValuePair2.Value)
					{
						if ((num8 > record2.Item1 && num8 < record2.Item2) || (isStartPosition && num8 == record2.Item1) || (!isStartPosition && num8 == record2.Item2))
						{
							return null;
						}
						if (num8 < record2.Item1)
						{
							break;
						}
					}
				}
			}
			HashSet<int> hashSet = (from kvp in positionExamples
				where kvp.Value == null
				select this._inputStringIndexes[kvp.Key]).ConvertToHashSet<int>();
			if (num >= 0)
			{
				return new Witnesses.DelimiterPos(delimiter, num, isStartPosition, hashSet, this, null);
			}
			if (num2 > 0)
			{
				return new Witnesses.DelimiterPos(delimiter, -1 * num2, isStartPosition, hashSet, this, null);
			}
			return null;
		}

		// Token: 0x06009C51 RID: 40017 RVA: 0x002155B8 File Offset: 0x002137B8
		private Record<int, int>? GetFirstDisjointOccurrence(string input, string example, IEnumerable<Record<int, int>> disallowedRanges)
		{
			if (string.IsNullOrEmpty(example))
			{
				return null;
			}
			int startIndex = -1;
			int endIndex = -1;
			if (disallowedRanges.Any<Record<int, int>>())
			{
				startIndex = input.IndexOf(example, disallowedRanges.Select((Record<int, int> t) => t.Item2).Max(), StringComparison.Ordinal);
				if (startIndex >= 0)
				{
					return new Record<int, int>?(Record.Create<int, int>(startIndex, startIndex + example.Length));
				}
			}
			for (int i = 0; i <= input.Length; i = startIndex + 1)
			{
				startIndex = input.IndexOf(example, i, StringComparison.Ordinal);
				if (startIndex < 0)
				{
					break;
				}
				endIndex = startIndex + example.Length;
				if (disallowedRanges.All((Record<int, int> t) => t.Item1 >= endIndex || t.Item2 <= startIndex))
				{
					return new Record<int, int>?(Record.Create<int, int>(startIndex, endIndex));
				}
			}
			throw new Exception("Cannot find disjoint occurrences of the given examples in the input string");
		}

		// Token: 0x06009C52 RID: 40018 RVA: 0x002156C8 File Offset: 0x002138C8
		[WitnessFunction("Split", 1, DependsOnParameters = new int[] { 0 })]
		public static DisjunctiveExamplesSpec WitnessDelimiterInSplit(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec vSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				State key = keyValuePair.Key;
				StringRegion v = (StringRegion)vSpec.Examples[key];
				object[] array = (from o in keyValuePair.Value
					let pair = (Record<StringRegion, StringRegion>)o
					let delimiterRegion = v.Slice(pair.Item1.End, pair.Item2.Start)
					from matching in RegularExpression.LearnFullMatches(delimiterRegion, 3, 0)
					where Witnesses.IsDelimiterRegex(matching)
					from lookbehind in RegularExpression.LearnLeftMatches(pair.Item1, pair.Item1.End, 3, 0)
					where Witnesses.IsLikelyRegex(lookbehind)
					from lookahead in RegularExpression.LearnRightMatches(pair.Item2, pair.Item2.Start, 3, 0)
					where Witnesses.IsLikelyRegex(lookahead)
					select new Record<RegularExpression, RegularExpression, RegularExpression>(lookbehind, matching, lookahead)).ToArray<object>();
				dictionary[key] = array;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x06009C53 RID: 40019 RVA: 0x0000E945 File Offset: 0x0000CB45
		[WitnessFunction("Append", 1)]
		public static Spec WitnessSequenceInAppend(GrammarRule rule, Spec spec)
		{
			return spec;
		}

		// Token: 0x06009C54 RID: 40020 RVA: 0x002158FC File Offset: 0x00213AFC
		private static bool IsLikelyRegex(RegularExpression regex)
		{
			return regex.Tokens.All((Token t) => t.Name != "Line Separator");
		}

		// Token: 0x06009C55 RID: 40021 RVA: 0x00215928 File Offset: 0x00213B28
		// Note: this type is marked as 'beforefieldinit'.
		static Witnesses()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary[","] = 3;
			dictionary["\t"] = 3;
			dictionary["|"] = 2;
			dictionary[";"] = 1;
			Witnesses.CommonDelScores = dictionary;
		}

		// Token: 0x04003E5E RID: 15966
		private const int MaxTokenCount = 3;

		// Token: 0x04003E5F RID: 15967
		private const int MaxNonConformingInputsCount = 1;

		// Token: 0x04003E60 RID: 15968
		private const int MinLeftAlignmentInputsCheck = 1;

		// Token: 0x04003E61 RID: 15969
		public const double MinMatchRatio = 0.7;

		// Token: 0x04003E62 RID: 15970
		public const double HighFrequencyThreshold = 0.97;

		// Token: 0x04003E63 RID: 15971
		private const int MaxColumnSearchCount = 10;

		// Token: 0x04003E64 RID: 15972
		private const int LargeStringThreshold = 70;

		// Token: 0x04003E65 RID: 15973
		public static Regex CommonDelimiters = new Regex(",|;|\\t|\\|");

		// Token: 0x04003E66 RID: 15974
		private static Regex _wordSequenceEnd = new Regex("\\p{L}+(\\s\\p{L}+)+$");

		// Token: 0x04003E67 RID: 15975
		private static readonly Dictionary<string, int> CommonDelScores;

		// Token: 0x04003E68 RID: 15976
		private readonly Dictionary<string, int> _inputFormatFrequencies = new Dictionary<string, int>();

		// Token: 0x04003E69 RID: 15977
		private readonly Dictionary<string, Record<int, int>> _inputRelevantEndPoints = new Dictionary<string, Record<int, int>>();

		// Token: 0x04003E6A RID: 15978
		private readonly Dictionary<string, string> _inputStringFormats = new Dictionary<string, string>();

		// Token: 0x04003E6B RID: 15979
		private readonly Dictionary<string, int> _inputStringIndexes = new Dictionary<string, int>();

		// Token: 0x04003E6C RID: 15980
		private readonly LruCache<Regex, LruCache<string, IReadOnlyList<Match>>> _regexMatchesCache = new ConcurrentLruCache<Regex, LruCache<string, IReadOnlyList<Match>>>(4096, null, null, null);

		// Token: 0x04003E6D RID: 15981
		private MatchRecord[] _endPositionDelimiter;

		// Token: 0x04003E6E RID: 15982
		private string[] _inputLines;

		// Token: 0x04003E6F RID: 15983
		private MatchRecord[][] _learnedDelimiters;

		// Token: 0x04003E70 RID: 15984
		private Dictionary<MatchRecord[], Record<int, int>> _learnedDelimitersMaxFrequencyMap;

		// Token: 0x04003E71 RID: 15985
		private HashSet<MatchRecord[]>[] _learnedTopAlignedDelimiterSets;

		// Token: 0x04003E72 RID: 15986
		private MatchRecord[] _startPositionDelimiter;

		// Token: 0x04003E74 RID: 15988
		private readonly Witnesses.Options _options;

		// Token: 0x04003E75 RID: 15989
		private readonly GrammarBuilders _build;

		// Token: 0x020013A2 RID: 5026
		public class Options : DSLOptions
		{
			// Token: 0x17001AB9 RID: 6841
			// (get) Token: 0x06009C59 RID: 40025 RVA: 0x002159D2 File Offset: 0x00213BD2
			// (set) Token: 0x06009C5A RID: 40026 RVA: 0x002159DA File Offset: 0x00213BDA
			public bool LearnSimpleSingleDelimiterPrograms { get; set; }

			// Token: 0x17001ABA RID: 6842
			// (get) Token: 0x06009C5B RID: 40027 RVA: 0x002159E3 File Offset: 0x00213BE3
			// (set) Token: 0x06009C5C RID: 40028 RVA: 0x002159EB File Offset: 0x00213BEB
			public bool LearnFixedWidth { get; set; }

			// Token: 0x17001ABB RID: 6843
			// (get) Token: 0x06009C5D RID: 40029 RVA: 0x002159F4 File Offset: 0x00213BF4
			// (set) Token: 0x06009C5E RID: 40030 RVA: 0x002159FC File Offset: 0x00213BFC
			public bool LearnSimpleDelimitersOrFixedWidth { get; set; }

			// Token: 0x17001ABC RID: 6844
			// (get) Token: 0x06009C5F RID: 40031 RVA: 0x00215A05 File Offset: 0x00213C05
			// (set) Token: 0x06009C60 RID: 40032 RVA: 0x00215A0D File Offset: 0x00213C0D
			public bool IncludeDelimiters { get; set; } = true;

			// Token: 0x17001ABD RID: 6845
			// (get) Token: 0x06009C61 RID: 40033 RVA: 0x00215A16 File Offset: 0x00213C16
			// (set) Token: 0x06009C62 RID: 40034 RVA: 0x00215A1E File Offset: 0x00213C1E
			public HashSet<string> ProvidedDelimiterStrings { get; set; }

			// Token: 0x17001ABE RID: 6846
			// (get) Token: 0x06009C63 RID: 40035 RVA: 0x00215A27 File Offset: 0x00213C27
			// (set) Token: 0x06009C64 RID: 40036 RVA: 0x00215A2F File Offset: 0x00213C2F
			public List<QuotingConfiguration> ProvidedQuotingConfigurations { get; set; }

			// Token: 0x17001ABF RID: 6847
			// (get) Token: 0x06009C65 RID: 40037 RVA: 0x00215A38 File Offset: 0x00213C38
			// (set) Token: 0x06009C66 RID: 40038 RVA: 0x00215A40 File Offset: 0x00213C40
			public FillStrategy? FillStrategy { get; set; }

			// Token: 0x17001AC0 RID: 6848
			// (get) Token: 0x06009C67 RID: 40039 RVA: 0x00215A49 File Offset: 0x00213C49
			// (set) Token: 0x06009C68 RID: 40040 RVA: 0x00215A51 File Offset: 0x00213C51
			public bool SuggestionsMode { get; set; }
		}

		// Token: 0x020013A3 RID: 5027
		private class DelimiterPos
		{
			// Token: 0x06009C6A RID: 40042 RVA: 0x00215A69 File Offset: 0x00213C69
			public DelimiterPos(MatchRecord[] delimiter, int index, bool isStart, HashSet<int> nullPositionInputs, Witnesses learningState, Dictionary<string, Witnesses.DelimiterPos> conditionalPos = null)
			{
				this.DelimiterMatches = delimiter;
				this.Index = index;
				this.IsStart = isStart;
				this.NullPositionInputs = nullPositionInputs;
				this.LearningState = learningState;
				this.ConditionalPos = conditionalPos ?? new Dictionary<string, Witnesses.DelimiterPos>();
			}

			// Token: 0x17001AC1 RID: 6849
			// (get) Token: 0x06009C6B RID: 40043 RVA: 0x00215AA8 File Offset: 0x00213CA8
			public int[] Positions
			{
				get
				{
					if (this._positions == null)
					{
						this._positions = new int[this.DelimiterMatches.Length];
						for (int i = 0; i < this._positions.Length; i++)
						{
							string text = this.LearningState._inputLines[i];
							string text2 = this.LearningState._inputStringFormats[text];
							if (this.ConditionalPos.ContainsKey(text2))
							{
								this._positions[i] = this.ConditionalPos[text2].Positions[i];
							}
							else
							{
								this._positions[i] = this.GetPosition(this.DelimiterMatches[i]);
							}
						}
					}
					return this._positions;
				}
			}

			// Token: 0x06009C6C RID: 40044 RVA: 0x00215B50 File Offset: 0x00213D50
			private int GetPosition(MatchRecord m)
			{
				int num = ((this.Index < 0) ? (m.NumMatches + this.Index) : this.Index);
				if (num < 0 || num >= m.NumMatches)
				{
					return -1;
				}
				if (!this.IsStart)
				{
					return m.StartIndexes[num];
				}
				return m.EndIndexes[num];
			}

			// Token: 0x06009C6D RID: 40045 RVA: 0x00215BAC File Offset: 0x00213DAC
			public bool IsDisjoint(List<Record<int, int>>[] disjointPositions)
			{
				for (int i = 0; i < this.Positions.Length; i++)
				{
					if (!this.NullPositionInputs.Contains(i))
					{
						int p = this.Positions[i];
						if (p != -1 && disjointPositions[i].Any((Record<int, int> t) => p > t.Item1 && p < t.Item2))
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x06009C6E RID: 40046 RVA: 0x00215C10 File Offset: 0x00213E10
			public static List<Record<int, int>>[] CheckConsistentStartEndPair(List<Record<int, int>>[] disjointPositions, Witnesses.DelimiterPos start, Witnesses.DelimiterPos end)
			{
				if (!start.IsStart || end.IsStart)
				{
					throw new ArgumentException("start and end delimiter positions are not valid");
				}
				List<Record<int, int>>[] array = new List<Record<int, int>>[start.Positions.Length];
				for (int i = 0; i < start.Positions.Length; i++)
				{
					int p1 = start.Positions[i];
					int p2 = end.Positions[i];
					array[i] = new List<Record<int, int>>(disjointPositions[i]);
					if (p1 != -1 && p2 != -1 && p1 < p2)
					{
						if (disjointPositions[i].Any((Record<int, int> t) => p2 > t.Item1 && p1 < t.Item2))
						{
							return null;
						}
						if (start.NullPositionInputs.Contains(i) || end.NullPositionInputs.Contains(i))
						{
							return null;
						}
						array[i].Add(Record.Create<int, int>(p1, p2));
					}
				}
				return array;
			}

			// Token: 0x06009C6F RID: 40047 RVA: 0x00215CFC File Offset: 0x00213EFC
			public HashSet<string> GetSatisfiedExamples(Dictionary<string, Record<int, int>?> positionExamples)
			{
				HashSet<string> hashSet = new HashSet<string>();
				foreach (KeyValuePair<string, Record<int, int>?> keyValuePair in positionExamples)
				{
					int num = this.LearningState._inputStringIndexes[keyValuePair.Key];
					if (keyValuePair.Value == null)
					{
						if (this.NullPositionInputs.Contains(num) || this.Positions[num] == -1)
						{
							hashSet.Add(keyValuePair.Key);
						}
					}
					else
					{
						int num2 = (this.IsStart ? keyValuePair.Value.Value.Item1 : keyValuePair.Value.Value.Item2);
						if (this.Positions[num] == num2)
						{
							hashSet.Add(keyValuePair.Key);
						}
					}
				}
				return hashSet;
			}

			// Token: 0x04003E80 RID: 16000
			public readonly Dictionary<string, Witnesses.DelimiterPos> ConditionalPos;

			// Token: 0x04003E81 RID: 16001
			public readonly MatchRecord[] DelimiterMatches;

			// Token: 0x04003E82 RID: 16002
			public readonly int Index;

			// Token: 0x04003E83 RID: 16003
			public readonly bool IsStart;

			// Token: 0x04003E84 RID: 16004
			public readonly Witnesses LearningState;

			// Token: 0x04003E85 RID: 16005
			public readonly HashSet<int> NullPositionInputs;

			// Token: 0x04003E86 RID: 16006
			private int[] _positions;
		}

		// Token: 0x020013A6 RID: 5030
		private class DominantSplitResultInfo
		{
			// Token: 0x06009C74 RID: 40052 RVA: 0x00215E34 File Offset: 0x00214034
			public DominantSplitResultInfo(MatchRecord[] splitResults, bool satisfyAllInputs, int numMatches, int conformingInputs)
			{
				this.SplitResults = splitResults;
				this.SatisfyAllInputs = satisfyAllInputs;
				this.DominantNumMatches = numMatches;
				this.ConformingInputsCount = conformingInputs;
				this.ConformingInputsRatio = (double)conformingInputs / (double)splitResults.Length;
			}

			// Token: 0x04003E8A RID: 16010
			public readonly int ConformingInputsCount;

			// Token: 0x04003E8B RID: 16011
			public readonly double ConformingInputsRatio;

			// Token: 0x04003E8C RID: 16012
			public readonly int DominantNumMatches;

			// Token: 0x04003E8D RID: 16013
			public readonly bool SatisfyAllInputs;

			// Token: 0x04003E8E RID: 16014
			public readonly MatchRecord[] SplitResults;
		}

		// Token: 0x020013A7 RID: 5031
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003E8F RID: 16015
			public static Func<MatchRecord, int> <0>__TotalFieldsSize;

			// Token: 0x04003E90 RID: 16016
			public static Func<string, bool> <1>__IsNullOrWhiteSpace;

			// Token: 0x04003E91 RID: 16017
			public static Func<char, bool> <2>__IsLetter;

			// Token: 0x04003E92 RID: 16018
			public static Func<char, bool> <3>__IsWhiteSpace;

			// Token: 0x04003E93 RID: 16019
			public static Func<char, bool> <4>__IsNumber;

			// Token: 0x04003E94 RID: 16020
			public static Func<string, Record<int, int>> <5>__GetRelevantEndPoints;
		}
	}
}
