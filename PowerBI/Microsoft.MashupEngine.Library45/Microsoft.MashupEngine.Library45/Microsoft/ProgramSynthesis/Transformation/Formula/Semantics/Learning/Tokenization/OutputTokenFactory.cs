using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Vocabulary;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Tokenization
{
	// Token: 0x020016B6 RID: 5814
	public class OutputTokenFactory
	{
		// Token: 0x0600C206 RID: 49670 RVA: 0x0029CC1D File Offset: 0x0029AE1D
		private OutputTokenFactory(Recognition recognition, bool enableDynamicTokens, CancellationToken cancellation, LearnDebugTrace debugTrace)
		{
			this._vocabulary = new VocabularyCatalog(recognition.DataCultures, debugTrace);
			this._debugTrace = debugTrace;
			this._cancellation = cancellation;
			this._recognition = recognition;
			this._enableDynamicTokens = enableDynamicTokens;
		}

		// Token: 0x0600C207 RID: 49671 RVA: 0x0029CC58 File Offset: 0x0029AE58
		public static IDictionary<Example<IRow, object>, Token[]> Compute(IEnumerable<Example<IRow, object>> examples, LearnOptions options = null, bool enableDynamicTokens = true, CancellationToken? cancellation = null, LearnDebugTrace debugTrace = null)
		{
			CancellationToken cancellationToken = cancellation.GetValueOrDefault();
			if (cancellation == null)
			{
				cancellationToken = (Debugger.IsAttached ? default(CancellationToken) : new CancellationTokenSource(TimeSpan.FromSeconds(5.0)).Token);
				cancellation = new CancellationToken?(cancellationToken);
			}
			return OutputTokenFactory.Compute(new Recognition(examples, options, debugTrace, cancellation.Value), enableDynamicTokens, cancellation, debugTrace);
		}

		// Token: 0x0600C208 RID: 49672 RVA: 0x0029CCC4 File Offset: 0x0029AEC4
		public static IDictionary<Example<IRow, object>, Token[]> Compute(Recognition recognition, bool enableDynamicTokens = true, CancellationToken? cancellation = null, LearnDebugTrace debugTrace = null)
		{
			CancellationToken cancellationToken = cancellation.GetValueOrDefault();
			if (cancellation == null)
			{
				cancellationToken = (Debugger.IsAttached ? default(CancellationToken) : new CancellationTokenSource(TimeSpan.FromSeconds(5.0)).Token);
				cancellation = new CancellationToken?(cancellationToken);
			}
			return new OutputTokenFactory(recognition, enableDynamicTokens, cancellation.Value, debugTrace).Tokenize();
		}

		// Token: 0x0600C209 RID: 49673 RVA: 0x0029CD2C File Offset: 0x0029AF2C
		private IDictionary<Example<IRow, object>, Token[]> Tokenize()
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IDictionary<Example<IRow, object>, Token[]> dictionary2;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("OutputTokenFactory", "Tokenize", true, true) : null))
			{
				Dictionary<Example<IRow, object>, Token[]> dictionary = new Dictionary<Example<IRow, object>, Token[]>();
				foreach (Example<IRow, object> example in this._recognition.Examples)
				{
					string text = example.Output as string;
					if (text == null)
					{
						return new Dictionary<Example<IRow, object>, Token[]>();
					}
					IReadOnlyList<Token> readOnlyList = (this._enableDynamicTokens ? this.ResolveDateTimeTokens(example.Input, text) : Utils.Empty<Token>().ToReadOnlyList<Token>());
					this._cancellation.ThrowIfCancellationRequested();
					IEnumerable<Token> enumerable = (this._enableDynamicTokens ? this.ResolveNumberTokens(example.Input, text) : Utils.Empty<Token>().ToReadOnlyList<Token>());
					this._cancellation.ThrowIfCancellationRequested();
					IReadOnlyList<Token> readOnlyList2 = (from t in enumerable.Concat(readOnlyList)
						orderby t.StartIndex, t.Value.Length
						select t).ToReadOnlyList<Token>();
					IReadOnlyList<Token> readOnlyList3 = this.ResolveSubstringTokens(example.Input, text, readOnlyList2);
					Token[] array = (from t in readOnlyList2.Concat(readOnlyList3)
						orderby t.StartIndex, t.Value.Length
						select t).ToArray<Token>();
					dictionary[example] = array;
				}
				using (IEnumerator<Token> enumerator2 = dictionary.Values.SelectMany((Token[] exampleTokens) => exampleTokens.Where((Token t) => !t.Covered)).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Token exampleToken = enumerator2.Current;
						Func<Token, bool> <>9__7;
						if (dictionary.Values.All(delegate(Token[] tokens)
						{
							Func<Token, bool> func;
							if ((func = <>9__7) == null)
							{
								func = (<>9__7 = (Token t) => t.Value == exampleToken.Value);
							}
							return tokens.Any(func);
						}))
						{
							exampleToken.Covered = true;
							exampleToken.Kind = TokenKind.Constant;
						}
					}
				}
				this._cancellation.ThrowIfCancellationRequested();
				if (timedEvent != null)
				{
					timedEvent.Stop();
				}
				dictionary2 = dictionary;
			}
			return dictionary2;
		}

		// Token: 0x0600C20A RID: 49674 RVA: 0x0029CFE4 File Offset: 0x0029B1E4
		private IReadOnlyList<Token> ResolveSubstringTokens(IRow inputRow, string output, IReadOnlyList<Token> dynamicTokens)
		{
			if (dynamicTokens.None<Token>())
			{
				return this.TokenizeSubstrings(0, inputRow, output);
			}
			List<Token> list = new List<Token>();
			Token token = dynamicTokens[0];
			if (token.StartIndex > 0)
			{
				string text = output.Slice(new int?(0), new int?(token.StartIndex), 1);
				list.AddRange(this.TokenizeSubstrings(0, inputRow, text));
			}
			for (int i = 0; i < dynamicTokens.Count; i++)
			{
				Token token2 = dynamicTokens[i];
				Token token3 = dynamicTokens.ElementAtOrDefault(i + 1);
				int num = token2.EndIndex + 1;
				int num2 = ((token3 == null) ? output.Length : token3.StartIndex);
				string text2 = output.Slice(new int?(num), new int?(num2), 1);
				if (!text2.IsNullOrEmpty())
				{
					list.AddRange(this.TokenizeSubstrings(num, inputRow, text2));
				}
			}
			return list;
		}

		// Token: 0x0600C20B RID: 49675 RVA: 0x0029D0BC File Offset: 0x0029B2BC
		private IReadOnlyList<Token> ResolveDateTimeTokens(IRow inputRow, string output)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<Token> readOnlyList;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("OutputTokenFactory", "ResolveDateTimeTokens", true, true) : null))
			{
				if (output.IsNullOrEmpty())
				{
					readOnlyList = Utils.Empty<Token>().ToReadOnlyList<Token>();
				}
				else
				{
					IReadOnlyList<Token> readOnlyList2 = (from item in this._recognition.FindFormattedDateTimes(output, true)
						where this._recognition.TryFormatDateTime(inputRow, item.Substring)
						group item by new { item.StartIndex, item.Substring } into g
						select new Token
						{
							Kind = TokenKind.DateTime,
							StartIndex = g.Key.StartIndex,
							Value = g.Key.Substring,
							Covered = true
						}).ToReadOnlyList<Token>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList = readOnlyList2;
				}
			}
			return readOnlyList;
		}

		// Token: 0x0600C20C RID: 49676 RVA: 0x0029D1A8 File Offset: 0x0029B3A8
		private IReadOnlyList<Token> ResolveNumberTokens(IRow inputRow, string output)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<Token> readOnlyList;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("OutputTokenFactory", "ResolveNumberTokens", true, true) : null))
			{
				if (output.IsNullOrEmpty())
				{
					readOnlyList = Utils.Empty<Token>().ToReadOnlyList<Token>();
				}
				else
				{
					IReadOnlyList<Token> readOnlyList2 = (from item in this._recognition.FindNumbers(output, false, new int?(10))
						where this._recognition.TryFormatNumber(inputRow, item.Substring)
						group item by new { item.StartIndex, item.Substring } into g
						select new Token
						{
							Kind = TokenKind.Number,
							StartIndex = g.Key.StartIndex,
							Value = g.Key.Substring,
							Covered = true
						}).ToReadOnlyList<Token>();
					if (timedEvent != null)
					{
						timedEvent.Stop();
					}
					readOnlyList = readOnlyList2;
				}
			}
			return readOnlyList;
		}

		// Token: 0x0600C20D RID: 49677 RVA: 0x0029D29C File Offset: 0x0029B49C
		private IReadOnlyList<Token> TokenizeSubstrings(int segmentStartIndex, IRow inputRow, string segment)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<Token> readOnlyList;
			using (TimedEvent timedEvent = ((debugTrace != null) ? debugTrace.StartTimedEvent("OutputTokenFactory", "TokenizeSubstrings", true, true) : null))
			{
				if (segment.IsNullOrEmpty())
				{
					readOnlyList = Utils.Empty<Token>().ToReadOnlyList<Token>();
				}
				else if (segment.All((char c) => c.IsDelimiter()))
				{
					readOnlyList = new Token
					{
						Kind = TokenKind.Delimiter,
						StartIndex = segmentStartIndex,
						Value = segment,
						Covered = this._recognition.Contains(inputRow, segment, true)
					}.Yield<Token>().ToReadOnlyList<Token>();
				}
				else
				{
					IReadOnlyList<Token> readOnlyList2 = (from match in OutputTokenFactory._delimiterRegex.NonCachingMatches(segment)
						orderby match.Index
						select new Token
						{
							Kind = TokenKind.Delimiter,
							StartIndex = segmentStartIndex + match.Index,
							Value = match.Value,
							Covered = this._recognition.Contains(inputRow, match.Value, true)
						}).ToReadOnlyList<Token>();
					List<Token> list = new List<Token>();
					if (readOnlyList2.None<Token>())
					{
						list.AddRange(this.TokenizeWord(inputRow, segmentStartIndex, segment));
						readOnlyList = list;
					}
					else
					{
						Token token = readOnlyList2[0];
						if (token.StartIndex > segmentStartIndex)
						{
							string text = segment.Slice(new int?(0), new int?(token.StartIndex), 1);
							list.Add(new Token
							{
								Kind = TokenKind.Substring,
								StartIndex = segmentStartIndex,
								Value = text,
								Covered = this._recognition.Contains(inputRow, text, true)
							});
						}
						for (int i = 0; i < readOnlyList2.Count; i++)
						{
							Token token2 = readOnlyList2[i];
							Token token3 = readOnlyList2.ElementAtOrDefault(i + 1);
							int num = token2.EndIndex + 1 - segmentStartIndex;
							int num2 = ((token3 == null) ? segment.Length : token3.StartIndex) - segmentStartIndex;
							string text2 = segment.Slice(new int?(num), new int?(num2), 1);
							if (!text2.IsNullOrEmpty())
							{
								list.Add(new Token
								{
									Kind = TokenKind.Substring,
									StartIndex = segmentStartIndex + num,
									Value = text2,
									Covered = this._recognition.Contains(inputRow, text2, true)
								});
							}
						}
						IReadOnlyList<Token> readOnlyList3 = list.Union(readOnlyList2).ToReadOnlyList<Token>();
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

		// Token: 0x0600C20E RID: 49678 RVA: 0x0029D55C File Offset: 0x0029B75C
		private IReadOnlyList<Token> TokenizeWord(IRow inputRow, int startIndex, string word)
		{
			LearnDebugTrace debugTrace = this._debugTrace;
			IReadOnlyList<Token> readOnlyList;
			using ((debugTrace != null) ? debugTrace.StartTimedEvent("OutputTokenFactory", "TokenizeWord", true, true) : null)
			{
				if (string.IsNullOrEmpty(word))
				{
					readOnlyList = Utils.Empty<Token>().ToReadOnlyList<Token>();
				}
				else if (this._vocabulary.Contains(word))
				{
					readOnlyList = new Token
					{
						Kind = TokenKind.Substring,
						StartIndex = startIndex,
						Value = word,
						Covered = this._recognition.Contains(inputRow, word, true)
					}.Yield<Token>().ToReadOnlyList<Token>();
				}
				else
				{
					for (int i = word.Length; i > 0; i--)
					{
						string text = word.Substring(i);
						if (this._vocabulary.Contains("##" + text))
						{
							string text2 = word.Substring(0, i);
							return new Token[]
							{
								new Token
								{
									Kind = TokenKind.Substring,
									StartIndex = startIndex,
									Value = text2,
									Covered = this._recognition.Contains(inputRow, text2, true)
								},
								new Token
								{
									Kind = TokenKind.Substring,
									StartIndex = startIndex + i,
									Value = text,
									Covered = this._recognition.Contains(inputRow, text, true)
								}
							};
						}
					}
					readOnlyList = Utils.Empty<Token>().ToReadOnlyList<Token>();
				}
			}
			return readOnlyList;
		}

		// Token: 0x04004B2A RID: 19242
		private readonly CancellationToken _cancellation;

		// Token: 0x04004B2B RID: 19243
		private readonly LearnDebugTrace _debugTrace;

		// Token: 0x04004B2C RID: 19244
		private static readonly Regex _delimiterRegex = "(\\p{P}|\\p{S}|\\p{Z}|\\t|\\r\\n|\\n)+".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x04004B2D RID: 19245
		private readonly bool _enableDynamicTokens;

		// Token: 0x04004B2E RID: 19246
		private readonly Recognition _recognition;

		// Token: 0x04004B2F RID: 19247
		private readonly VocabularyCatalog _vocabulary;
	}
}
