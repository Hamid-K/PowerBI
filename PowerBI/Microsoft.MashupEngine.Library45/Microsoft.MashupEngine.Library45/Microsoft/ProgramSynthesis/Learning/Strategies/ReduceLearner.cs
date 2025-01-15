using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Learning.Strategies
{
	// Token: 0x02000706 RID: 1798
	internal static class ReduceLearner
	{
		// Token: 0x06002708 RID: 9992 RVA: 0x0006E805 File Offset: 0x0006CA05
		public static IEnumerable<string[][]> OutputSplits(string[] input, string[] output)
		{
			int num = input.Length;
			int num2 = output.Length;
			HashSet<ReduceLearner.OutputSubstr>[] seq = (from i in Enumerable.Range(0, num)
				select new HashSet<ReduceLearner.OutputSubstr>()).ToArray<HashSet<ReduceLearner.OutputSubstr>>();
			Enumerable.Range(0, num).ForEach(delegate(int i)
			{
				if (input[i].Length == 1 && char.IsDigit(input[i][0]))
				{
					seq[i].AddRange(ReduceLearner.Search(output, ReduceLearner.Expander[input[i][0]]));
				}
				seq[i].AddRange(ReduceLearner.Search(output, new string[] { input[i] }));
			});
			ReduceLearner.Choice[] array = ReduceLearner.Choose(seq, num, num2).ToArray<ReduceLearner.Choice>();
			List<List<ReduceLearner.ChoiceCombo>> list2 = new List<List<ReduceLearner.ChoiceCombo>>
			{
				new List<ReduceLearner.ChoiceCombo>
				{
					new ReduceLearner.ChoiceCombo(array)
				}
			};
			using (IEnumerator<Record<int, int, int, int>> enumerator = ReduceLearner.NegativeGroups(array.ToDictionary((ReduceLearner.Choice choice) => choice.inputIndex), num, num2).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Record<int, int, int, int> group = enumerator.Current;
					Func<ReduceLearner.OutputSubstr, int, ReduceLearner.Choice> <>9__4;
					list2.Add(new List<ReduceLearner.ChoiceCombo>(ReduceLearner.Partitions(group.Item1, group.Item2, group.Item3, group.Item4).Select(delegate(List<ReduceLearner.OutputSubstr> list)
					{
						Func<ReduceLearner.OutputSubstr, int, ReduceLearner.Choice> func2;
						if ((func2 = <>9__4) == null)
						{
							func2 = (<>9__4 = (ReduceLearner.OutputSubstr outSubstr, int i) => new ReduceLearner.Choice(group.Item1 + i, outSubstr));
						}
						return new ReduceLearner.ChoiceCombo(list.Select(func2));
					})));
				}
			}
			Func<int, string> <>9__8;
			Func<ReduceLearner.Choice, string[]> <>9__7;
			foreach (IEnumerable<ReduceLearner.ChoiceCombo> enumerable in list2.CartesianProduct<ReduceLearner.ChoiceCombo>())
			{
				IEnumerable<ReduceLearner.Choice> enumerable2 = from choice in enumerable.SelectMany((ReduceLearner.ChoiceCombo combo) => combo.Choices)
					orderby choice.inputIndex
					select choice;
				Func<ReduceLearner.Choice, string[]> func;
				if ((func = <>9__7) == null)
				{
					func = (<>9__7 = delegate(ReduceLearner.Choice choice)
					{
						IEnumerable<int> enumerable3 = Enumerable.Range(choice.outputIndex.begin, choice.outputIndex.end - choice.outputIndex.begin);
						Func<int, string> func3;
						if ((func3 = <>9__8) == null)
						{
							func3 = (<>9__8 = (int i) => output[i]);
						}
						return enumerable3.Select(func3).ToArray<string>();
					});
				}
				yield return enumerable2.Select(func).ToArray<string[]>();
			}
			IEnumerator<IEnumerable<ReduceLearner.ChoiceCombo>> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0006E81C File Offset: 0x0006CA1C
		private static IEnumerable<ReduceLearner.OutputSubstr> Search(string[] tokens, string[] key)
		{
			int i;
			Func<string, int, bool> <>9__0;
			int k;
			for (i = 0; i < tokens.Length - key.Length + 1; i = k + 1)
			{
				Func<string, int, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (string str, int j) => str == tokens[i + j]);
				}
				if (key.Select(func).All((bool b) => b))
				{
					yield return new ReduceLearner.OutputSubstr(i, i + key.Length);
				}
				k = i;
			}
			yield break;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x0006E833 File Offset: 0x0006CA33
		private static IEnumerable<ReduceLearner.Choice> Choose(HashSet<ReduceLearner.OutputSubstr>[] choices, int length, int outlength)
		{
			Dictionary<ReduceLearner.Choice, ReduceLearner.Choice> pointer = new Dictionary<ReduceLearner.Choice, ReduceLearner.Choice>();
			Dictionary<ReduceLearner.Choice, int> score = new Dictionary<ReduceLearner.Choice, int>();
			choices[0].ForEach(delegate(ReduceLearner.OutputSubstr substr)
			{
				if (substr.begin == 0)
				{
					score.Add(new ReduceLearner.Choice(0, substr), 1);
				}
			});
			choices.Skip(1).ForEach(delegate(HashSet<ReduceLearner.OutputSubstr> substrSet, int i)
			{
				substrSet.ForEach(delegate(ReduceLearner.OutputSubstr substr)
				{
					ReduceLearner.Choice choice = new ReduceLearner.Choice(i + 1, substr);
					IList<KeyValuePair<ReduceLearner.Choice, int>> list = score.Where((KeyValuePair<ReduceLearner.Choice, int> kvp) => kvp.Key.Compatible(choice)).MaxBy((KeyValuePair<ReduceLearner.Choice, int> kvp) => kvp.Value);
					if (list.IsEmpty<KeyValuePair<ReduceLearner.Choice, int>>())
					{
						score.Add(choice, 1);
						return;
					}
					KeyValuePair<ReduceLearner.Choice, int> keyValuePair = list.First<KeyValuePair<ReduceLearner.Choice, int>>();
					pointer.Add(choice, keyValuePair.Key);
					score.Add(choice, keyValuePair.Value + 1);
				});
			});
			ReduceLearner.Choice last = score.Where((KeyValuePair<ReduceLearner.Choice, int> kvp) => kvp.Key.inputIndex != length - 1 || kvp.Key.outputIndex.end == outlength).MaxBy((KeyValuePair<ReduceLearner.Choice, int> kvp) => kvp.Value).First<KeyValuePair<ReduceLearner.Choice, int>>()
				.Key;
			yield return last;
			while (pointer.ContainsKey(last))
			{
				last = pointer[last];
				yield return last;
			}
			yield break;
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0006E851 File Offset: 0x0006CA51
		private static IEnumerable<Record<int, int, int, int>> NegativeGroups(Dictionary<int, ReduceLearner.Choice> positiveChoices, int length, int outlength)
		{
			bool[] arr = Enumerable.Repeat<bool>(false, length).ToArray<bool>();
			positiveChoices.ForEach(delegate(KeyValuePair<int, ReduceLearner.Choice> kvp)
			{
				arr[kvp.Key] = true;
			});
			int curr = 0;
			int num = 0;
			while (curr < length)
			{
				if (!arr[curr])
				{
					int num2 = curr;
					int num3 = curr;
					curr = num3 + 1;
					while (curr < length && !arr[curr])
					{
						num3 = curr;
						curr = num3 + 1;
					}
					int outEnd = ((curr < length) ? positiveChoices[curr].outputIndex.begin : outlength);
					yield return Record.Create<int, int, int, int>(num2, curr - 1, num, outEnd);
					num = outEnd;
				}
				else
				{
					num = positiveChoices[curr].outputIndex.end;
					int num3 = curr;
					curr = num3 + 1;
				}
			}
			yield break;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0006E86F File Offset: 0x0006CA6F
		private static IEnumerable<List<ReduceLearner.OutputSubstr>> Partitions(int inputBegin, int inputEnd, int outBegin, int outEnd)
		{
			int num = outEnd - outBegin - 1;
			int num2 = inputEnd - inputBegin;
			foreach (List<int> list in ReduceLearner.Choose(num, num2))
			{
				int num3 = 0;
				List<ReduceLearner.OutputSubstr> list2 = new List<ReduceLearner.OutputSubstr>();
				foreach (int num4 in list)
				{
					list2.Add(new ReduceLearner.OutputSubstr(num3 + outBegin, num4 + outBegin));
					num3 = num4;
				}
				list2.Add(new ReduceLearner.OutputSubstr(num3 + outBegin, outEnd));
				yield return list2;
			}
			IEnumerator<List<int>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0006E894 File Offset: 0x0006CA94
		private static IEnumerable<List<int>> Choose(int n, int k)
		{
			if (k == 0)
			{
				return new List<int>().Yield<List<int>>();
			}
			if (k < 0 || k > n)
			{
				return Enumerable.Empty<List<int>>();
			}
			List<int>[] array = ReduceLearner.Choose(n - 1, k - 1).ToArray<List<int>>();
			array.ForEach(delegate(List<int> list)
			{
				list.Add(n);
			});
			return ReduceLearner.Choose(n - 1, k).Concat(array);
		}

		// Token: 0x040012DF RID: 4831
		private static readonly Dictionary<char, string[]> Expander = new Dictionary<char, string[]>
		{
			{
				'0',
				new string[] { "zero" }
			},
			{
				'1',
				new string[] { "one" }
			},
			{
				'2',
				new string[] { "two" }
			},
			{
				'3',
				new string[] { "three" }
			},
			{
				'4',
				new string[] { "four" }
			},
			{
				'5',
				new string[] { "five" }
			},
			{
				'6',
				new string[] { "six" }
			},
			{
				'7',
				new string[] { "seven" }
			},
			{
				'8',
				new string[] { "eight" }
			},
			{
				'9',
				new string[] { "nine" }
			}
		};

		// Token: 0x02000707 RID: 1799
		private class ChoiceCombo
		{
			// Token: 0x170006D8 RID: 1752
			// (get) Token: 0x0600270F RID: 9999 RVA: 0x0006EA01 File Offset: 0x0006CC01
			public HashSet<ReduceLearner.Choice> Choices { get; }

			// Token: 0x06002710 RID: 10000 RVA: 0x0006EA09 File Offset: 0x0006CC09
			public ChoiceCombo(IEnumerable<ReduceLearner.Choice> choices)
			{
				this.Choices = choices.ConvertToHashSet<ReduceLearner.Choice>();
			}
		}

		// Token: 0x02000708 RID: 1800
		private struct OutputSubstr
		{
			// Token: 0x06002711 RID: 10001 RVA: 0x0006EA1D File Offset: 0x0006CC1D
			public OutputSubstr(int b, int e)
			{
				this.begin = b;
				this.end = e;
			}

			// Token: 0x06002712 RID: 10002 RVA: 0x0006EA2D File Offset: 0x0006CC2D
			public override string ToString()
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("({0},{1})", new object[] { this.begin, this.end }));
			}

			// Token: 0x040012E1 RID: 4833
			public int begin;

			// Token: 0x040012E2 RID: 4834
			public int end;
		}

		// Token: 0x02000709 RID: 1801
		private struct Choice
		{
			// Token: 0x06002713 RID: 10003 RVA: 0x0006EA60 File Offset: 0x0006CC60
			public Choice(int i, ReduceLearner.OutputSubstr o)
			{
				this.inputIndex = i;
				this.outputIndex = o;
			}

			// Token: 0x06002714 RID: 10004 RVA: 0x0006EA70 File Offset: 0x0006CC70
			public bool Compatible(ReduceLearner.Choice that)
			{
				ReduceLearner.Choice choice = ((this.inputIndex < that.inputIndex) ? this : that);
				ReduceLearner.Choice choice2 = ((this.inputIndex < that.inputIndex) ? that : this);
				if (choice2.inputIndex == choice.inputIndex + 1)
				{
					return choice.outputIndex.end == choice2.outputIndex.begin;
				}
				int num = choice2.outputIndex.begin - choice.outputIndex.end;
				int num2 = choice2.inputIndex - choice.inputIndex - 1;
				return num >= num2;
			}

			// Token: 0x06002715 RID: 10005 RVA: 0x0006EB03 File Offset: 0x0006CD03
			public override string ToString()
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("({0}, {1})", new object[] { this.inputIndex, this.outputIndex }));
			}

			// Token: 0x040012E3 RID: 4835
			public int inputIndex;

			// Token: 0x040012E4 RID: 4836
			public ReduceLearner.OutputSubstr outputIndex;
		}
	}
}
