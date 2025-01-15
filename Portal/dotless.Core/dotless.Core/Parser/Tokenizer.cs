using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser
{
	// Token: 0x02000026 RID: 38
	[DebuggerDisplay("{Remaining}")]
	public class Tokenizer
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007985 File Offset: 0x00005B85
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000798D File Offset: 0x00005B8D
		public int Optimization { get; set; }

		// Token: 0x06000146 RID: 326 RVA: 0x00007996 File Offset: 0x00005B96
		public Tokenizer(int optimization)
		{
			this.Optimization = optimization;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000079D4 File Offset: 0x00005BD4
		public void SetupInput(string input, string fileName)
		{
			this._fileName = fileName;
			this._i = (this._j = (this._current = 0));
			this._chunks = new List<Tokenizer.Chunk>();
			this._input = input.Replace("\r\n", "\n");
			this._inputLength = this._input.Length;
			if (this.Optimization == 0)
			{
				this._chunks.Add(new Tokenizer.Chunk(this._input));
			}
			else
			{
				Regex regex = new Regex("\\G(@\\{[a-zA-Z0-9_-]+\\}|[^\\\"'{}/\\\\\\(\\)]+)");
				Regex regex2 = this.GetRegex(this._commentRegEx, RegexOptions.None);
				Regex regex3 = this.GetRegex(this._quotedRegEx, RegexOptions.None);
				int num = 0;
				int num2 = 0;
				bool flag = false;
				int i = 0;
				while (i < this._inputLength)
				{
					Match match = regex.Match(this._input, i);
					if (match.Success)
					{
						Tokenizer.Chunk.Append(match.Value, this._chunks);
						i += match.Length;
					}
					else
					{
						char c = this._input[i];
						if (i < this._inputLength - 1 && c == '/')
						{
							char c2 = this._input[i + 1];
							if ((!flag && c2 == '/') || c2 == '*')
							{
								match = regex2.Match(this._input, i);
								if (match.Success)
								{
									i += match.Length;
									this._chunks.Add(new Tokenizer.Chunk(match.Value, Tokenizer.ChunkType.Comment));
									continue;
								}
								throw new ParsingException("Missing closing comment", this.GetNodeLocation(i));
							}
						}
						if (c == '"' || c == '\'')
						{
							match = regex3.Match(this._input, i);
							if (!match.Success)
							{
								throw new ParsingException(string.Format("Missing closing quote ({0})", c), this.GetNodeLocation(i));
							}
							i += match.Length;
							this._chunks.Add(new Tokenizer.Chunk(match.Value, Tokenizer.ChunkType.QuotedString));
						}
						else
						{
							if (!flag && c == '{')
							{
								num++;
								num2 = i;
							}
							else if (!flag && c == '}')
							{
								num--;
								if (num < 0)
								{
									throw new ParsingException("Unexpected '}'", this.GetNodeLocation(i));
								}
								Tokenizer.Chunk.Append(c, this._chunks, true);
								i++;
								continue;
							}
							if (c == '(')
							{
								flag = true;
							}
							else if (c == ')')
							{
								flag = false;
							}
							Tokenizer.Chunk.Append(c, this._chunks);
							i++;
						}
					}
				}
				if (num > 0)
				{
					throw new ParsingException("Missing closing '}'", this.GetNodeLocation(num2));
				}
				this._input = Tokenizer.Chunk.CommitAll(this._chunks);
				this._inputLength = this._input.Length;
			}
			this.Advance(0);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007C98 File Offset: 0x00005E98
		public string GetComment()
		{
			if (this._i == this._inputLength)
			{
				return null;
			}
			int i = this._i;
			string text;
			int num;
			if (this.Optimization == 0)
			{
				if (this.CurrentChar != '/')
				{
					return null;
				}
				RegexMatchResult regexMatchResult = this.Match(this._commentRegEx);
				if (regexMatchResult == null)
				{
					return null;
				}
				text = regexMatchResult.Value;
				num = i + regexMatchResult.Value.Length;
			}
			else
			{
				if (this._chunks[this._j].Type != Tokenizer.ChunkType.Comment)
				{
					return null;
				}
				text = this._chunks[this._j].Value;
				num = this._i + this._chunks[this._j].Value.Length;
				this.Advance(this._chunks[this._j].Value.Length);
			}
			if (this._lastCommentEnd != i)
			{
				this._lastCommentStart = i;
			}
			this._lastCommentEnd = num;
			return text;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007D8C File Offset: 0x00005F8C
		public string GetQuotedString()
		{
			if (this._i == this._inputLength)
			{
				return null;
			}
			if (this.Optimization == 0)
			{
				if (this.CurrentChar != '"' && this.CurrentChar != '\'')
				{
					return null;
				}
				return this.Match(this._quotedRegEx).Value;
			}
			else
			{
				if (this._chunks[this._j].Type == Tokenizer.ChunkType.QuotedString)
				{
					string value = this._chunks[this._j].Value;
					this.Advance(this._chunks[this._j].Value.Length);
					return value;
				}
				return null;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007E2C File Offset: 0x0000602C
		public string MatchString(char tok)
		{
			CharMatchResult charMatchResult = this.Match(tok);
			if (charMatchResult != null)
			{
				return charMatchResult.Value;
			}
			return null;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007E4C File Offset: 0x0000604C
		public string MatchString(string tok)
		{
			RegexMatchResult regexMatchResult = this.Match(tok);
			if (regexMatchResult != null)
			{
				return regexMatchResult.Value;
			}
			return null;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007E6C File Offset: 0x0000606C
		public CharMatchResult Match(char tok)
		{
			if (this._i == this._inputLength || this._chunks[this._j].Type != Tokenizer.ChunkType.Text)
			{
				return null;
			}
			if (this._input[this._i] == tok)
			{
				int i = this._i;
				this.Advance(1);
				return new CharMatchResult(tok)
				{
					Location = this.GetNodeLocation(i)
				};
			}
			return null;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007ED8 File Offset: 0x000060D8
		public RegexMatchResult Match(string tok)
		{
			return this.Match(tok, false);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007EE4 File Offset: 0x000060E4
		public RegexMatchResult Match(string tok, bool caseInsensitive)
		{
			if (this._i == this._inputLength || this._chunks[this._j].Type != Tokenizer.ChunkType.Text)
			{
				return null;
			}
			RegexOptions regexOptions = RegexOptions.None;
			if (caseInsensitive)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			Match match = this.GetRegex(tok, regexOptions).Match(this._chunks[this._j].Value, this._i - this._current);
			if (!match.Success)
			{
				return null;
			}
			int i = this._i;
			this.Advance(match.Length);
			return new RegexMatchResult(match)
			{
				Location = this.GetNodeLocation(i)
			};
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007F84 File Offset: 0x00006184
		public RegexMatchResult MatchAny(string tok)
		{
			if (this._i == this._inputLength)
			{
				return null;
			}
			Match match = this.GetRegex(tok, RegexOptions.None).Match(this._input, this._i);
			if (!match.Success)
			{
				return null;
			}
			this.Advance(match.Length);
			if (this._i > this._current && this._i < this._current + this._chunks[this._j].Value.Length && this._chunks[this._j].Type == Tokenizer.ChunkType.Comment && this._chunks[this._j].Value.StartsWith("//"))
			{
				this._chunks[this._j].Type = Tokenizer.ChunkType.Text;
			}
			return new RegexMatchResult(match);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008064 File Offset: 0x00006264
		public void Advance(int length)
		{
			if (this._i == this._inputLength)
			{
				return;
			}
			this._i += length;
			int num = this._current + this._chunks[this._j].Value.Length;
			while (this._i != this._inputLength)
			{
				if (this._i >= num)
				{
					if (this._j >= this._chunks.Count - 1)
					{
						break;
					}
					this._current = num;
					int num2 = num;
					List<Tokenizer.Chunk> chunks = this._chunks;
					int num3 = this._j + 1;
					this._j = num3;
					num = num2 + chunks[num3].Value.Length;
				}
				else
				{
					if (!char.IsWhiteSpace(this._input[this._i]))
					{
						break;
					}
					this._i++;
				}
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000813A File Offset: 0x0000633A
		public bool Peek(char tok)
		{
			return this._i != this._inputLength && this._input[this._i] == tok;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008160 File Offset: 0x00006360
		public bool Peek(string tok)
		{
			return this.GetRegex(tok, RegexOptions.None).Match(this._input, this._i).Success;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008180 File Offset: 0x00006380
		public bool PeekAfterComments(char tok)
		{
			Location location = this.Location;
			while (this.GetComment() != null)
			{
			}
			bool flag = this.Peek(tok);
			this.Location = location;
			return flag;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000081AA File Offset: 0x000063AA
		private Regex GetRegex(string pattern, RegexOptions options)
		{
			if (!this.regexCache.ContainsKey(pattern))
			{
				this.regexCache.Add(pattern, new Regex("\\G" + pattern, options));
			}
			return this.regexCache[pattern];
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000081E4 File Offset: 0x000063E4
		public char GetPreviousCharIgnoringComments()
		{
			if (this._i == 0)
			{
				return '\0';
			}
			if (this._i != this._lastCommentEnd)
			{
				return this.PreviousChar;
			}
			int num = this._lastCommentStart - 1;
			if (num < 0)
			{
				return '\0';
			}
			return this._input[num];
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000822B File Offset: 0x0000642B
		public char PreviousChar
		{
			get
			{
				if (this._i != 0)
				{
					return this._input[this._i - 1];
				}
				return '\0';
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000824A File Offset: 0x0000644A
		public char CurrentChar
		{
			get
			{
				if (this._i != this._inputLength)
				{
					return this._input[this._i];
				}
				return '\0';
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000826D File Offset: 0x0000646D
		public char NextChar
		{
			get
			{
				if (this._i + 1 != this._inputLength)
				{
					return this._input[this._i + 1];
				}
				return '\0';
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008294 File Offset: 0x00006494
		public bool HasCompletedParsing()
		{
			return this._i == this._inputLength;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000082A4 File Offset: 0x000064A4
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000082CF File Offset: 0x000064CF
		public Location Location
		{
			get
			{
				return new Location
				{
					Index = this._i,
					CurrentChunk = this._j,
					CurrentChunkIndex = this._current
				};
			}
			set
			{
				this._i = value.Index;
				this._j = value.CurrentChunk;
				this._current = value.CurrentChunkIndex;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000082F5 File Offset: 0x000064F5
		public NodeLocation GetNodeLocation(int index)
		{
			return new NodeLocation(index, this._input, this._fileName);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008309 File Offset: 0x00006509
		public NodeLocation GetNodeLocation()
		{
			return this.GetNodeLocation(this.Location.Index);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000831C File Offset: 0x0000651C
		private string Remaining
		{
			get
			{
				return this._input.Substring(this._i);
			}
		}

		// Token: 0x04000038 RID: 56
		private string _input;

		// Token: 0x04000039 RID: 57
		private List<Tokenizer.Chunk> _chunks;

		// Token: 0x0400003A RID: 58
		private int _i;

		// Token: 0x0400003B RID: 59
		private int _j;

		// Token: 0x0400003C RID: 60
		private int _current;

		// Token: 0x0400003D RID: 61
		private int _lastCommentStart = -1;

		// Token: 0x0400003E RID: 62
		private int _lastCommentEnd = -1;

		// Token: 0x0400003F RID: 63
		private int _inputLength;

		// Token: 0x04000040 RID: 64
		private readonly string _commentRegEx = "(//[^\\n]*|(/\\*(.|[\\r\\n])*?\\*/))";

		// Token: 0x04000041 RID: 65
		private readonly string _quotedRegEx = "(\"((?:[^\"\\\\\\r\\n]|\\\\.)*)\"|'((?:[^'\\\\\\r\\n]|\\\\.)*)')";

		// Token: 0x04000042 RID: 66
		private string _fileName;

		// Token: 0x04000043 RID: 67
		private IDictionary<string, Regex> regexCache = new Dictionary<string, Regex>();

		// Token: 0x020000DD RID: 221
		private enum ChunkType
		{
			// Token: 0x04000174 RID: 372
			Text,
			// Token: 0x04000175 RID: 373
			Comment,
			// Token: 0x04000176 RID: 374
			QuotedString
		}

		// Token: 0x020000DE RID: 222
		private class Chunk
		{
			// Token: 0x0600060F RID: 1551 RVA: 0x00018CEE File Offset: 0x00016EEE
			public Chunk(string val)
			{
				this.Value = val;
				this.Type = Tokenizer.ChunkType.Text;
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x00018D04 File Offset: 0x00016F04
			public Chunk(string val, Tokenizer.ChunkType type)
			{
				this.Value = val;
				this.Type = type;
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x00018D1A File Offset: 0x00016F1A
			public Chunk()
			{
				this._builder = new StringBuilder();
				this.Type = Tokenizer.ChunkType.Text;
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000612 RID: 1554 RVA: 0x00018D34 File Offset: 0x00016F34
			// (set) Token: 0x06000613 RID: 1555 RVA: 0x00018D3C File Offset: 0x00016F3C
			public Tokenizer.ChunkType Type { get; set; }

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000614 RID: 1556 RVA: 0x00018D45 File Offset: 0x00016F45
			// (set) Token: 0x06000615 RID: 1557 RVA: 0x00018D4D File Offset: 0x00016F4D
			public string Value { get; set; }

			// Token: 0x06000616 RID: 1558 RVA: 0x00018D56 File Offset: 0x00016F56
			public void Append(string str)
			{
				this._builder.Append(str);
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x00018D65 File Offset: 0x00016F65
			public void Append(char c)
			{
				this._builder.Append(c);
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x00018D74 File Offset: 0x00016F74
			private static Tokenizer.Chunk ReadyForText(List<Tokenizer.Chunk> chunks)
			{
				Tokenizer.Chunk chunk = chunks.LastOrDefault<Tokenizer.Chunk>();
				if (chunk == null || chunk.Type != Tokenizer.ChunkType.Text || chunk._final)
				{
					chunk = new Tokenizer.Chunk();
					chunks.Add(chunk);
				}
				return chunk;
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x00018DA9 File Offset: 0x00016FA9
			public static void Append(char c, List<Tokenizer.Chunk> chunks, bool final)
			{
				Tokenizer.Chunk chunk = Tokenizer.Chunk.ReadyForText(chunks);
				chunk.Append(c);
				chunk._final = final;
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x00018DBE File Offset: 0x00016FBE
			public static void Append(char c, List<Tokenizer.Chunk> chunks)
			{
				Tokenizer.Chunk.ReadyForText(chunks).Append(c);
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x00018DCC File Offset: 0x00016FCC
			public static void Append(string s, List<Tokenizer.Chunk> chunks)
			{
				Tokenizer.Chunk.ReadyForText(chunks).Append(s);
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x00018DDC File Offset: 0x00016FDC
			public static string CommitAll(List<Tokenizer.Chunk> chunks)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Tokenizer.Chunk chunk in chunks)
				{
					if (chunk._builder != null)
					{
						string text = chunk._builder.ToString();
						chunk._builder = null;
						chunk.Value = text;
					}
					stringBuilder.Append(chunk.Value);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04000177 RID: 375
			private StringBuilder _builder;

			// Token: 0x0400017A RID: 378
			private bool _final;
		}
	}
}
