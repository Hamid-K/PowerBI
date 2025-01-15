using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018B3 RID: 6323
	internal class TokensBuilder
	{
		// Token: 0x0600A118 RID: 41240 RVA: 0x0021694E File Offset: 0x00214B4E
		public TokensBuilder()
		{
			this.arrays = null;
			this.arraysLength = 0;
			this.shortTokens = new TokensBuilder.ShortToken[4];
			this.index = 0;
			this.longTokens = null;
		}

		// Token: 0x0600A119 RID: 41241 RVA: 0x00216980 File Offset: 0x00214B80
		private void ExpandAndAdd(TokenType type, int length)
		{
			if (this.arrays == null)
			{
				this.arrays = new List<TokensBuilder.ShortToken[]>();
			}
			this.arrays.Add(this.shortTokens);
			this.arraysLength += this.shortTokens.Length;
			int num = checked(this.shortTokens.Length * 2);
			this.shortTokens = new TokensBuilder.ShortToken[num];
			this.shortTokens[0].Type = type;
			this.shortTokens[0].Length = (byte)length;
			this.index = 1;
		}

		// Token: 0x0600A11A RID: 41242 RVA: 0x00216A0C File Offset: 0x00214C0C
		private void AddLongToken(int index, int length)
		{
			if (this.longTokens == null)
			{
				this.longTokens = new List<TokensBuilder.LongToken>();
			}
			TokensBuilder.LongToken longToken;
			longToken.Index = index;
			longToken.Length = length;
			this.longTokens.Add(longToken);
		}

		// Token: 0x0600A11B RID: 41243 RVA: 0x00216A48 File Offset: 0x00214C48
		public void Add(TokenType type, int length)
		{
			if (length >= 255)
			{
				this.AddLongToken(this.arraysLength + this.index, length);
				length = 255;
			}
			TokensBuilder.ShortToken[] array = this.shortTokens;
			int num = this.index;
			if (num < array.Length)
			{
				array[num].Type = type;
				array[num].Length = (byte)length;
				this.index++;
				return;
			}
			this.ExpandAndAdd(type, length);
		}

		// Token: 0x0600A11C RID: 41244 RVA: 0x00216AC0 File Offset: 0x00214CC0
		public void AddShort(TokenType type, byte length)
		{
			TokensBuilder.ShortToken[] array = this.shortTokens;
			int num = this.index;
			if (num < array.Length)
			{
				array[num].Type = type;
				array[num].Length = length;
				this.index++;
				return;
			}
			this.ExpandAndAdd(type, (int)length);
		}

		// Token: 0x0600A11D RID: 41245 RVA: 0x00216B14 File Offset: 0x00214D14
		public ITokens ToTokens(SegmentedString text)
		{
			List<TokensBuilder.ShortToken[]> list = this.arrays;
			TokensBuilder.ShortToken[] array = new TokensBuilder.ShortToken[this.arraysLength + this.index];
			int num = 0;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					int num2 = list[i].Length;
					Array.Copy(list[i], 0, array, num, num2);
					num += num2;
				}
			}
			Array.Copy(this.shortTokens, 0, array, num, this.index);
			if (this.longTokens == null)
			{
				return new TokensBuilder.ShortTokens(text, array);
			}
			return new TokensBuilder.LongAndShortTokens(text, this.longTokens.ToArray(), array);
		}

		// Token: 0x0400546F RID: 21615
		private List<TokensBuilder.ShortToken[]> arrays;

		// Token: 0x04005470 RID: 21616
		private int arraysLength;

		// Token: 0x04005471 RID: 21617
		private TokensBuilder.ShortToken[] shortTokens;

		// Token: 0x04005472 RID: 21618
		private int index;

		// Token: 0x04005473 RID: 21619
		private List<TokensBuilder.LongToken> longTokens;

		// Token: 0x020018B4 RID: 6324
		[DebuggerDisplay("{Type} ({Length})")]
		private struct ShortToken
		{
			// Token: 0x04005474 RID: 21620
			public TokenType Type;

			// Token: 0x04005475 RID: 21621
			public byte Length;
		}

		// Token: 0x020018B5 RID: 6325
		private struct LongToken
		{
			// Token: 0x04005476 RID: 21622
			public int Index;

			// Token: 0x04005477 RID: 21623
			public int Length;
		}

		// Token: 0x020018B6 RID: 6326
		private class ShortTokens : ISegmentedTokens, ITokens, IEquatable<ITokens>
		{
			// Token: 0x0600A11E RID: 41246 RVA: 0x00216BA8 File Offset: 0x00214DA8
			public ShortTokens(SegmentedString text, TokensBuilder.ShortToken[] tokens)
			{
				this.text = text;
				this.tokens = tokens;
			}

			// Token: 0x1700293B RID: 10555
			// (get) Token: 0x0600A11F RID: 41247 RVA: 0x00216BBE File Offset: 0x00214DBE
			public string Text
			{
				get
				{
					return this.text.ToString();
				}
			}

			// Token: 0x1700293C RID: 10556
			// (get) Token: 0x0600A120 RID: 41248 RVA: 0x00216BD1 File Offset: 0x00214DD1
			public SegmentedString SegmentedText
			{
				get
				{
					return this.text;
				}
			}

			// Token: 0x1700293D RID: 10557
			// (get) Token: 0x0600A121 RID: 41249 RVA: 0x00216BD9 File Offset: 0x00214DD9
			public int Count
			{
				get
				{
					return this.tokens.Length;
				}
			}

			// Token: 0x0600A122 RID: 41250 RVA: 0x00216BE3 File Offset: 0x00214DE3
			public TokenType GetType(TokenReference token)
			{
				return this.tokens[(int)token].Type;
			}

			// Token: 0x0600A123 RID: 41251 RVA: 0x00216BF6 File Offset: 0x00214DF6
			public virtual int GetLength(TokenReference index)
			{
				return (int)this.tokens[(int)index].Length;
			}

			// Token: 0x0600A124 RID: 41252 RVA: 0x00216C0C File Offset: 0x00214E0C
			public int GetOffset(TokenReference token)
			{
				if (this.offsets == null)
				{
					TokensBuilder.ShortToken[] array = this.tokens;
					lock (array)
					{
						if (this.offsets == null)
						{
							int[] array2 = new int[this.tokens.Length + 1];
							int num = 0;
							for (int i = 0; i < this.tokens.Length; i++)
							{
								num += this.GetLength((TokenReference)i);
								array2[i + 1] = num;
							}
							this.offsets = array2;
						}
					}
				}
				return this.offsets[(int)token];
			}

			// Token: 0x0600A125 RID: 41253 RVA: 0x00216CA0 File Offset: 0x00214EA0
			public int GetOffset(TextPosition position)
			{
				return this.LineBreaks[position.Row] + position.Column;
			}

			// Token: 0x0600A126 RID: 41254 RVA: 0x00216CB8 File Offset: 0x00214EB8
			public TextRange GetRange(TokenReference token)
			{
				int offset = this.GetOffset(token);
				TextPosition position = this.GetPosition(offset);
				int length = this.GetLength(token);
				TextPosition position2 = this.GetPosition(offset + length);
				return new TextRange(position, position2);
			}

			// Token: 0x0600A127 RID: 41255 RVA: 0x00216CEC File Offset: 0x00214EEC
			public TextPosition GetPosition(int offset)
			{
				int num = Array.BinarySearch<int>(this.LineBreaks, offset);
				if (num < 0)
				{
					num = ~num - 1;
				}
				int num2 = num;
				int num3 = offset - this.LineBreaks[num];
				return new TextPosition(num2, num3);
			}

			// Token: 0x0600A128 RID: 41256 RVA: 0x00216D21 File Offset: 0x00214F21
			public bool Equals(ITokens other)
			{
				return other != null && this.SegmentedText == other.GetSegmentedText();
			}

			// Token: 0x0600A129 RID: 41257 RVA: 0x00216D39 File Offset: 0x00214F39
			public override bool Equals(object other)
			{
				return this.Equals(other as ITokens);
			}

			// Token: 0x0600A12A RID: 41258 RVA: 0x00216D47 File Offset: 0x00214F47
			public override int GetHashCode()
			{
				return this.text.GetHashCode();
			}

			// Token: 0x1700293E RID: 10558
			// (get) Token: 0x0600A12B RID: 41259 RVA: 0x00216D5C File Offset: 0x00214F5C
			private int[] LineBreaks
			{
				get
				{
					if (this.lineBreaks == null)
					{
						TokensBuilder.ShortToken[] array = this.tokens;
						lock (array)
						{
							if (this.lineBreaks == null)
							{
								List<int> list = new List<int>();
								int num = 0;
								list.Add(0);
								for (int i = 0; i < this.text.Length; i++)
								{
									num++;
									char c = this.text[i];
									if (c != '\n')
									{
										if (c == '\r' && (i + 1 == this.text.Length || this.text[i + 1] != '\n'))
										{
											list.Add(num);
										}
									}
									else
									{
										list.Add(num);
									}
								}
								this.lineBreaks = list.ToArray();
							}
						}
					}
					return this.lineBreaks;
				}
			}

			// Token: 0x04005478 RID: 21624
			private SegmentedString text;

			// Token: 0x04005479 RID: 21625
			private TokensBuilder.ShortToken[] tokens;

			// Token: 0x0400547A RID: 21626
			private int[] offsets;

			// Token: 0x0400547B RID: 21627
			private int[] lineBreaks;
		}

		// Token: 0x020018B7 RID: 6327
		private class LongAndShortTokens : TokensBuilder.ShortTokens
		{
			// Token: 0x0600A12C RID: 41260 RVA: 0x00216E3C File Offset: 0x0021503C
			public LongAndShortTokens(SegmentedString text, TokensBuilder.LongToken[] longTokens, TokensBuilder.ShortToken[] shortTokens)
				: base(text, shortTokens)
			{
				this.longTokens = longTokens;
			}

			// Token: 0x0600A12D RID: 41261 RVA: 0x00216E50 File Offset: 0x00215050
			public override int GetLength(TokenReference token)
			{
				int num = base.GetLength(token);
				if (num == 255)
				{
					TokensBuilder.LongToken longToken = default(TokensBuilder.LongToken);
					longToken.Index = (int)token;
					longToken.Length = 0;
					num = this.longTokens[Array.BinarySearch<TokensBuilder.LongToken>(this.longTokens, longToken, TokensBuilder.LongAndShortTokens.comparer)].Length;
				}
				return num;
			}

			// Token: 0x0400547C RID: 21628
			private static readonly IComparer<TokensBuilder.LongToken> comparer = new TokensBuilder.LongAndShortTokens.LongTokenComparer();

			// Token: 0x0400547D RID: 21629
			private TokensBuilder.LongToken[] longTokens;

			// Token: 0x020018B8 RID: 6328
			private class LongTokenComparer : IComparer<TokensBuilder.LongToken>
			{
				// Token: 0x0600A12F RID: 41263 RVA: 0x00216EB4 File Offset: 0x002150B4
				public int Compare(TokensBuilder.LongToken x, TokensBuilder.LongToken y)
				{
					return x.Index.CompareTo(y.Index);
				}
			}
		}
	}
}
