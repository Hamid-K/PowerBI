using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Vocabulary;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014CD RID: 5325
	public class LearnConfidenceFactory
	{
		// Token: 0x0600A31A RID: 41754 RVA: 0x0022880E File Offset: 0x00226A0E
		private LearnConfidenceFactory(Recognition recognition, CancellationToken cancellation, LearnDebugTrace debugTrace)
		{
			this._vocabulary = new VocabularyCatalog(recognition.DataCultures, debugTrace);
			this._debugTrace = debugTrace;
			this._cancellation = cancellation;
			this._recognition = recognition;
		}

		// Token: 0x0600A31B RID: 41755 RVA: 0x00228840 File Offset: 0x00226A40
		public static LearnConfidenceResult Compute(IEnumerable<Example<IRow, object>> examples, IEnumerable<CultureInfo> cultures = null, CancellationToken? cancellation = null, LearnDebugTrace debugTrace = null)
		{
			LearnOptions learnOptions = new LearnOptions
			{
				DataCultures = (cultures ?? new CultureInfo("en-US").Yield<CultureInfo>()).ToReadOnlyList<CultureInfo>()
			};
			CancellationToken cancellationToken = cancellation.GetValueOrDefault();
			if (cancellation == null)
			{
				cancellationToken = (Debugger.IsAttached ? default(CancellationToken) : new CancellationTokenSource(TimeSpan.FromSeconds(5.0)).Token);
				cancellation = new CancellationToken?(cancellationToken);
			}
			return LearnConfidenceFactory.Compute(new Recognition(examples, learnOptions, debugTrace, cancellation.Value), cancellation, debugTrace);
		}

		// Token: 0x0600A31C RID: 41756 RVA: 0x002288CC File Offset: 0x00226ACC
		public static LearnConfidenceResult Compute(Recognition recognition, CancellationToken? cancellation = null, LearnDebugTrace debugTrace = null)
		{
			CancellationToken cancellationToken = cancellation.GetValueOrDefault();
			if (cancellation == null)
			{
				cancellationToken = (Debugger.IsAttached ? default(CancellationToken) : new CancellationTokenSource(TimeSpan.FromSeconds(5.0)).Token);
				cancellation = new CancellationToken?(cancellationToken);
			}
			return new LearnConfidenceFactory(recognition, cancellation.Value, debugTrace).ComputeInternal();
		}

		// Token: 0x0600A31D RID: 41757 RVA: 0x00228930 File Offset: 0x00226B30
		private LearnConfidenceResult ComputeInternal()
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			LearnConfidenceResult learnConfidenceResult;
			using ((debugTrace != null) ? debugTrace.StartTimedEvent("LearnConfidenceFactory", "Compute", true, true) : null)
			{
				IReadOnlyList<Example<IRow, object>> examples = this._recognition.Examples;
				IReadOnlyList<CultureInfo> dataCultures = this._recognition.DataCultures;
				bool flag = (((examples != null) ? new int?(examples.Count) : null) ?? 0) == 0;
				if (flag)
				{
					learnConfidenceResult = new LearnConfidenceResult
					{
						Reason = LearnConfidenceReason.NoExamples
					};
				}
				else if (examples.Count > 1000)
				{
					learnConfidenceResult = new LearnConfidenceResult
					{
						Reason = LearnConfidenceReason.TooManyRows
					};
				}
				else if (examples[0].Input.ValidColumnNames().Count<string>() > 100)
				{
					learnConfidenceResult = new LearnConfidenceResult
					{
						Reason = LearnConfidenceReason.TooManyColumns
					};
				}
				else if (examples.Any((Example<IRow, object> ex) => !(ex.Output is string)))
				{
					learnConfidenceResult = new LearnConfidenceResult
					{
						Reason = LearnConfidenceReason.OutputType
					};
				}
				else if (dataCultures.Any(delegate(CultureInfo c)
				{
					string name = c.Name;
					bool flag2 = name == "en-US" || name == "en-GB";
					return !flag2;
				}))
				{
					learnConfidenceResult = new LearnConfidenceResult
					{
						Reason = LearnConfidenceReason.CultureNotSupported
					};
				}
				else
				{
					IReadOnlyList<string> readOnlyList = examples.Select((Example<IRow, object> e) => e.Output as string).ToReadOnlyList<string>();
					if (readOnlyList.All((string o) => this._recognition.IsFormattedNumber(o)))
					{
						learnConfidenceResult = new LearnConfidenceResult
						{
							Reason = LearnConfidenceReason.AllFormattedNumbers
						};
					}
					else if (readOnlyList.All((string o) => this._recognition.IsFormattedDateTime(o, false)))
					{
						learnConfidenceResult = new LearnConfidenceResult
						{
							Reason = LearnConfidenceReason.AllFormattedDateTimes
						};
					}
					else
					{
						List<LearnConfidenceFactory.Token[]> list = new List<LearnConfidenceFactory.Token[]>();
						foreach (Example<IRow, object> example in examples)
						{
							string text = (string)example.Output;
							IReadOnlyList<LearnConfidenceFactory.Token> readOnlyList2 = this.Tokenize(example.Input, text);
							this._cancellation.ThrowIfCancellationRequested();
							list.Add(readOnlyList2.ToArray<LearnConfidenceFactory.Token>());
						}
						using (IEnumerator<LearnConfidenceFactory.Token> enumerator2 = list.SelectMany((LearnConfidenceFactory.Token[] exampleTokens) => exampleTokens.Where((LearnConfidenceFactory.Token t) => !t.Covered)).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								LearnConfidenceFactory.Token exampleToken = enumerator2.Current;
								Func<LearnConfidenceFactory.Token, bool> <>9__11;
								exampleToken.Covered = list.All(delegate(LearnConfidenceFactory.Token[] tokens)
								{
									Func<LearnConfidenceFactory.Token, bool> func;
									if ((func = <>9__11) == null)
									{
										func = (<>9__11 = (LearnConfidenceFactory.Token t) => t.Value == exampleToken.Value);
									}
									return tokens.Any(func);
								});
							}
						}
						List<List<int>> list2 = new List<List<int>>();
						int num = 0;
						using (IEnumerator<LearnConfidenceFactory.Token> enumerator2 = list.SelectMany((LearnConfidenceFactory.Token[] exampleTokens) => exampleTokens.Where((LearnConfidenceFactory.Token t) => !t.Covered)).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								LearnConfidenceFactory.Token uncoveredToken = enumerator2.Current;
								num++;
								Func<LearnConfidenceFactory.Token, bool> <>9__17;
								List<int> indices = (from i in list.Select(delegate(LearnConfidenceFactory.Token[] tokens, int i)
									{
										Func<LearnConfidenceFactory.Token, bool> func2;
										if ((func2 = <>9__17) == null)
										{
											func2 = (<>9__17 = (LearnConfidenceFactory.Token t) => t.Value == uncoveredToken.Value);
										}
										if (!tokens.Any(func2))
										{
											return -1;
										}
										return i;
									})
									where i != -1
									select i).ToList<int>();
								if (!list2.Any((List<int> p) => p.SequenceEqual(indices)))
								{
									list2.Add(indices);
								}
							}
						}
						LearnConfidenceFactory.Token token;
						if (list2.Count < list.Count && list2.Count < num)
						{
							using (IEnumerator<LearnConfidenceFactory.Token> enumerator2 = list.SelectMany((LearnConfidenceFactory.Token[] exampleTokens) => exampleTokens.Where((LearnConfidenceFactory.Token t) => !t.Covered)).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									token = enumerator2.Current;
									token.Covered = true;
								}
							}
						}
						this._cancellation.ThrowIfCancellationRequested();
						int num2 = list.Sum((LearnConfidenceFactory.Token[] tokenList) => tokenList.Sum((LearnConfidenceFactory.Token token) => token.Value.Length));
						int num3 = list.Sum((LearnConfidenceFactory.Token[] tokenList) => tokenList.Where((LearnConfidenceFactory.Token token) => !token.Covered).Sum((LearnConfidenceFactory.Token token) => token.Value.Length));
						int num4 = list.Sum((LearnConfidenceFactory.Token[] tokenList) => tokenList.Where((LearnConfidenceFactory.Token token) => !token.Covered).Where(delegate(LearnConfidenceFactory.Token token)
						{
							if (token.Value.Length > 3)
							{
								IEnumerable<char> value = token.Value;
								Func<char, bool> func3;
								if ((func3 = LearnConfidenceFactory.<>O.<0>__IsDigit) == null)
								{
									func3 = (LearnConfidenceFactory.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
								}
								return value.All(func3);
							}
							return true;
						}).Sum((LearnConfidenceFactory.Token token) => token.Value.Length));
						num2 -= num4;
						num3 -= num4;
						double? num5 = ((num2 == 0) ? null : new double?(1.0 - (double)num3 / (double)num2));
						learnConfidenceResult = new LearnConfidenceResult
						{
							Score = num5
						};
					}
				}
			}
			return learnConfidenceResult;
		}

		// Token: 0x0600A31E RID: 41758 RVA: 0x00228E88 File Offset: 0x00227088
		private IReadOnlyList<LearnConfidenceFactory.Token> Tokenize(IRow inputRow, string output)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<LearnConfidenceFactory.Token> readOnlyList;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("LearnConfidenceFactory", "Tokenize", true, true) : null))
			{
				if (output.IsNullOrEmpty())
				{
					readOnlyList = Utils.Empty<LearnConfidenceFactory.Token>().ToReadOnlyList<LearnConfidenceFactory.Token>();
				}
				else
				{
					IReadOnlyList<LearnConfidenceFactory.Token> readOnlyList2 = (from match in LearnConfidenceFactory._delimiterRegex.NonCachingMatches(output)
						orderby match.Index
						select new LearnConfidenceFactory.Token
						{
							Kind = LearnConfidenceFactory.TokenKind.Delimiter,
							StartIndex = match.Index,
							Value = match.Value,
							Covered = this._recognition.Contains(inputRow, match.Value, true)
						}).ToReadOnlyList<LearnConfidenceFactory.Token>();
					List<LearnConfidenceFactory.Token> list = new List<LearnConfidenceFactory.Token>();
					if (readOnlyList2.None<LearnConfidenceFactory.Token>())
					{
						list.AddRange(this.TokenizeWord(inputRow, 0, output));
						readOnlyList = list;
					}
					else
					{
						LearnConfidenceFactory.Token token = readOnlyList2[0];
						if (token.StartIndex != 0)
						{
							string text = output.Slice(new int?(0), new int?(token.StartIndex), 1);
							list.AddRange(this.TokenizeWord(inputRow, 0, text));
						}
						for (int i = 0; i < readOnlyList2.Count; i++)
						{
							LearnConfidenceFactory.Token token2 = readOnlyList2[i];
							LearnConfidenceFactory.Token token3 = readOnlyList2.ElementAtOrDefault(i + 1);
							int num = token2.EndIndex + 1;
							int num2 = ((token3 == null) ? output.Length : token3.StartIndex);
							string text2 = output.Slice(new int?(num), new int?(num2), 1);
							if (!text2.IsNullOrEmpty())
							{
								list.AddRange(this.TokenizeWord(inputRow, num, text2));
							}
						}
						IReadOnlyList<LearnConfidenceFactory.Token> readOnlyList3 = list.Union(readOnlyList2).ToReadOnlyList<LearnConfidenceFactory.Token>();
						if (timedEvent != null)
						{
							timedEvent.Stop();
						}
						readOnlyList = readOnlyList3;
					}
				}
			}
			return readOnlyList;
		}

		// Token: 0x0600A31F RID: 41759 RVA: 0x00229058 File Offset: 0x00227258
		private IReadOnlyList<LearnConfidenceFactory.Token> TokenizeWord(IRow inputRow, int startIndex, string word)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<LearnConfidenceFactory.Token> readOnlyList;
			using ((debugTrace != null) ? debugTrace.StartTimedEvent("LearnConfidenceFactory", "TokenizeWord", true, true) : null)
			{
				if (string.IsNullOrEmpty(word))
				{
					readOnlyList = Utils.Empty<LearnConfidenceFactory.Token>().ToReadOnlyList<LearnConfidenceFactory.Token>();
				}
				else if (this._vocabulary.Contains(word))
				{
					readOnlyList = new LearnConfidenceFactory.Token
					{
						Kind = LearnConfidenceFactory.TokenKind.Substring,
						StartIndex = startIndex,
						Value = word,
						Covered = this._recognition.Contains(inputRow, word, true)
					}.Yield<LearnConfidenceFactory.Token>().ToReadOnlyList<LearnConfidenceFactory.Token>();
				}
				else
				{
					int i = word.Length - startIndex;
					List<LearnConfidenceFactory.Token> list = new List<LearnConfidenceFactory.Token>();
					int num = i;
					while (i > 0)
					{
						string text = ((startIndex > 0) ? "##" : "");
						string text2 = word.Substring(startIndex, num);
						while (num > 0 && !this._vocabulary.Contains(text + text2))
						{
							num--;
							text2 = word.Substring(startIndex, num);
						}
						if (num == 0)
						{
							string text3 = word.Substring(startIndex);
							list.Add(new LearnConfidenceFactory.Token
							{
								Kind = LearnConfidenceFactory.TokenKind.Substring,
								StartIndex = startIndex,
								Value = text3,
								Covered = this._recognition.Contains(inputRow, text3, true)
							});
							return list.ToReadOnlyList<LearnConfidenceFactory.Token>();
						}
						list.Add(new LearnConfidenceFactory.Token
						{
							Kind = LearnConfidenceFactory.TokenKind.Substring,
							StartIndex = startIndex,
							Value = text2,
							Covered = this._recognition.Contains(inputRow, text2, true)
						});
						startIndex += num;
						i = word.Length - startIndex;
						num = i;
					}
					readOnlyList = list;
				}
			}
			return readOnlyList;
		}

		// Token: 0x0600A320 RID: 41760 RVA: 0x0022920C File Offset: 0x0022740C
		private string RenderTokens(IEnumerable<LearnConfidenceFactory.Token[]> subject)
		{
			return subject.Select((LearnConfidenceFactory.Token[] t) => t.Select((LearnConfidenceFactory.Token i) => i.ToString()).RenderNumbered(1).Concat((Environment.NewLine + "---").Yield<string>())).RenderNumbered(1);
		}

		// Token: 0x040041FE RID: 16894
		private const int _columnLimit = 100;

		// Token: 0x040041FF RID: 16895
		private const int _rowLimit = 1000;

		// Token: 0x04004200 RID: 16896
		private readonly CancellationToken _cancellation;

		// Token: 0x04004201 RID: 16897
		private readonly LearnDebugTrace _debugTrace;

		// Token: 0x04004202 RID: 16898
		private readonly Recognition _recognition;

		// Token: 0x04004203 RID: 16899
		private static readonly Regex _delimiterRegex = "(\\p{P}|\\p{S}|\\p{Z}|\\t|\\r\\n|\\n)+".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x04004204 RID: 16900
		private readonly VocabularyCatalog _vocabulary;

		// Token: 0x020014CE RID: 5326
		private class Token : IEquatable<LearnConfidenceFactory.Token>
		{
			// Token: 0x17001C87 RID: 7303
			// (get) Token: 0x0600A324 RID: 41764 RVA: 0x0022926C File Offset: 0x0022746C
			// (set) Token: 0x0600A325 RID: 41765 RVA: 0x00229274 File Offset: 0x00227474
			public bool Covered
			{
				get
				{
					return this._covered;
				}
				set
				{
					this._toString = null;
					this._covered = value;
				}
			}

			// Token: 0x17001C88 RID: 7304
			// (get) Token: 0x0600A326 RID: 41766 RVA: 0x00229284 File Offset: 0x00227484
			public int EndIndex
			{
				get
				{
					int num = this._endIndex.GetValueOrDefault();
					if (this._endIndex == null)
					{
						num = this.StartIndex + this.Value.Length - 1;
						this._endIndex = new int?(num);
						return num;
					}
					return num;
				}
			}

			// Token: 0x17001C89 RID: 7305
			// (get) Token: 0x0600A327 RID: 41767 RVA: 0x002292CE File Offset: 0x002274CE
			// (set) Token: 0x0600A328 RID: 41768 RVA: 0x002292D6 File Offset: 0x002274D6
			public LearnConfidenceFactory.TokenKind Kind { get; set; }

			// Token: 0x17001C8A RID: 7306
			// (get) Token: 0x0600A329 RID: 41769 RVA: 0x002292DF File Offset: 0x002274DF
			// (set) Token: 0x0600A32A RID: 41770 RVA: 0x002292E7 File Offset: 0x002274E7
			public int StartIndex { get; set; }

			// Token: 0x17001C8B RID: 7307
			// (get) Token: 0x0600A32B RID: 41771 RVA: 0x002292F0 File Offset: 0x002274F0
			// (set) Token: 0x0600A32C RID: 41772 RVA: 0x002292F8 File Offset: 0x002274F8
			public string Value { get; set; }

			// Token: 0x0600A32D RID: 41773 RVA: 0x00229301 File Offset: 0x00227501
			public bool Equals(LearnConfidenceFactory.Token other)
			{
				return other != null && this.ToString() == other.ToString();
			}

			// Token: 0x0600A32E RID: 41774 RVA: 0x0022931F File Offset: 0x0022751F
			public override bool Equals(object other)
			{
				return this.Equals(other as LearnConfidenceFactory.Token);
			}

			// Token: 0x0600A32F RID: 41775 RVA: 0x00218E7F File Offset: 0x0021707F
			public override int GetHashCode()
			{
				return this.ToString().GetHashCode();
			}

			// Token: 0x0600A330 RID: 41776 RVA: 0x0022932D File Offset: 0x0022752D
			public static bool operator ==(LearnConfidenceFactory.Token left, LearnConfidenceFactory.Token right)
			{
				return (left == null && right == null) || (left != null && left.Equals(right));
			}

			// Token: 0x0600A331 RID: 41777 RVA: 0x00229343 File Offset: 0x00227543
			public static bool operator !=(LearnConfidenceFactory.Token left, LearnConfidenceFactory.Token right)
			{
				return !(left == right);
			}

			// Token: 0x0600A332 RID: 41778 RVA: 0x00229350 File Offset: 0x00227550
			public override string ToString()
			{
				string text = (this.Covered ? "covered" : string.Empty);
				string text2 = ("\"" + this.Value + "\"").PadRight(10);
				string text3;
				if ((text3 = this._toString) == null)
				{
					text3 = (this._toString = string.Format("[{0}..{1}] {2,-10}: {3} {4}", new object[] { this.StartIndex, this.EndIndex, this.Kind, text2, text }));
				}
				return text3;
			}

			// Token: 0x04004205 RID: 16901
			private bool _covered;

			// Token: 0x04004206 RID: 16902
			private int? _endIndex;

			// Token: 0x04004207 RID: 16903
			private string _toString;
		}

		// Token: 0x020014CF RID: 5327
		private enum TokenKind
		{
			// Token: 0x0400420C RID: 16908
			Substring,
			// Token: 0x0400420D RID: 16909
			Delimiter,
			// Token: 0x0400420E RID: 16910
			Number,
			// Token: 0x0400420F RID: 16911
			DateTime
		}

		// Token: 0x020014D0 RID: 5328
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004210 RID: 16912
			public static Func<char, bool> <0>__IsDigit;
		}
	}
}
