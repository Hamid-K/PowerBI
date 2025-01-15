using System;
using System.Collections;
using System.Text;
using antlr.collections.impl;

namespace antlr
{
	// Token: 0x0200002B RID: 43
	internal class TokenStreamRewriteEngine : TokenStream
	{
		// Token: 0x0600015D RID: 349 RVA: 0x0000535F File Offset: 0x0000355F
		public TokenStreamRewriteEngine(TokenStream upstream)
			: this(upstream, 1000)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005370 File Offset: 0x00003570
		public TokenStreamRewriteEngine(TokenStream upstream, int initialSize)
		{
			this.stream = upstream;
			this.tokens = new ArrayList(initialSize);
			this.programs = new Hashtable();
			this.programs["default"] = new ArrayList(100);
			this.lastRewriteTokenIndexes = new Hashtable();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000053D0 File Offset: 0x000035D0
		public IToken nextToken()
		{
			TokenWithIndex tokenWithIndex;
			do
			{
				tokenWithIndex = (TokenWithIndex)this.stream.nextToken();
				if (tokenWithIndex != null)
				{
					tokenWithIndex.setIndex(this._index);
					if (tokenWithIndex.Type != 1)
					{
						this.tokens.Add(tokenWithIndex);
					}
					this._index++;
				}
			}
			while (tokenWithIndex != null && this.discardMask.member(tokenWithIndex.Type));
			return tokenWithIndex;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005438 File Offset: 0x00003638
		public void rollback(int instructionIndex)
		{
			this.rollback("default", instructionIndex);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005448 File Offset: 0x00003648
		public void rollback(string programName, int instructionIndex)
		{
			ArrayList arrayList = (ArrayList)this.programs[programName];
			if (arrayList != null)
			{
				this.programs[programName] = arrayList.GetRange(0, instructionIndex);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000547E File Offset: 0x0000367E
		public void deleteProgram()
		{
			this.deleteProgram("default");
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000548B File Offset: 0x0000368B
		public void deleteProgram(string programName)
		{
			this.rollback(programName, 0);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005495 File Offset: 0x00003695
		protected void addToSortedRewriteList(TokenStreamRewriteEngine.RewriteOperation op)
		{
			this.addToSortedRewriteList("default", op);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000054A4 File Offset: 0x000036A4
		protected void addToSortedRewriteList(string programName, TokenStreamRewriteEngine.RewriteOperation op)
		{
			ArrayList arrayList = (ArrayList)this.getProgram(programName);
			if (op.index >= this.getLastRewriteTokenIndex(programName))
			{
				arrayList.Add(op);
				this.setLastRewriteTokenIndex(programName, op.index);
				return;
			}
			int num = arrayList.BinarySearch(op, TokenStreamRewriteEngine.RewriteOperationComparer.Default);
			if (num < 0)
			{
				arrayList.Insert(-num - 1, op);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000054FF File Offset: 0x000036FF
		public void insertAfter(IToken t, string text)
		{
			this.insertAfter("default", t, text);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000550E File Offset: 0x0000370E
		public void insertAfter(int index, string text)
		{
			this.insertAfter("default", index, text);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000551D File Offset: 0x0000371D
		public void insertAfter(string programName, IToken t, string text)
		{
			this.insertAfter(programName, ((TokenWithIndex)t).getIndex(), text);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005532 File Offset: 0x00003732
		public void insertAfter(string programName, int index, string text)
		{
			this.insertBefore(programName, index + 1, text);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000553F File Offset: 0x0000373F
		public void insertBefore(IToken t, string text)
		{
			this.insertBefore("default", t, text);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000554E File Offset: 0x0000374E
		public void insertBefore(int index, string text)
		{
			this.insertBefore("default", index, text);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000555D File Offset: 0x0000375D
		public void insertBefore(string programName, IToken t, string text)
		{
			this.insertBefore(programName, ((TokenWithIndex)t).getIndex(), text);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005572 File Offset: 0x00003772
		public void insertBefore(string programName, int index, string text)
		{
			this.addToSortedRewriteList(programName, new TokenStreamRewriteEngine.InsertBeforeOp(index, text));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005582 File Offset: 0x00003782
		public void replace(int index, string text)
		{
			this.replace("default", index, index, text);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005592 File Offset: 0x00003792
		public void replace(int from, int to, string text)
		{
			this.replace("default", from, to, text);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000055A2 File Offset: 0x000037A2
		public void replace(IToken indexT, string text)
		{
			this.replace("default", indexT, indexT, text);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000055B2 File Offset: 0x000037B2
		public void replace(IToken from, IToken to, string text)
		{
			this.replace("default", from, to, text);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000055C2 File Offset: 0x000037C2
		public void replace(string programName, int from, int to, string text)
		{
			this.addToSortedRewriteList(programName, new TokenStreamRewriteEngine.ReplaceOp(from, to, text));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000055D4 File Offset: 0x000037D4
		public void replace(string programName, IToken from, IToken to, string text)
		{
			this.replace(programName, ((TokenWithIndex)from).getIndex(), ((TokenWithIndex)to).getIndex(), text);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000055F5 File Offset: 0x000037F5
		public void delete(int index)
		{
			this.delete("default", index, index);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005604 File Offset: 0x00003804
		public void delete(int from, int to)
		{
			this.delete("default", from, to);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005613 File Offset: 0x00003813
		public void delete(IToken indexT)
		{
			this.delete("default", indexT, indexT);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005622 File Offset: 0x00003822
		public void delete(IToken from, IToken to)
		{
			this.delete("default", from, to);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005631 File Offset: 0x00003831
		public void delete(string programName, int from, int to)
		{
			this.replace(programName, from, to, null);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000563D File Offset: 0x0000383D
		public void delete(string programName, IToken from, IToken to)
		{
			this.replace(programName, from, to, null);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005649 File Offset: 0x00003849
		public void discard(int ttype)
		{
			this.discardMask.add(ttype);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005657 File Offset: 0x00003857
		public TokenWithIndex getToken(int i)
		{
			return (TokenWithIndex)this.tokens[i];
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000566A File Offset: 0x0000386A
		public int getTokenStreamSize()
		{
			return this.tokens.Count;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005677 File Offset: 0x00003877
		public string ToOriginalString()
		{
			return this.ToOriginalString(0, this.getTokenStreamSize() - 1);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005688 File Offset: 0x00003888
		public string ToOriginalString(int start, int end)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = start;
			while (num >= 0 && num <= end && num < this.tokens.Count)
			{
				stringBuilder.Append(this.getToken(num).getText());
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000056D2 File Offset: 0x000038D2
		public override string ToString()
		{
			return this.ToString(0, this.getTokenStreamSize());
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000056E1 File Offset: 0x000038E1
		public string ToString(string programName)
		{
			return this.ToString(programName, 0, this.getTokenStreamSize());
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000056F1 File Offset: 0x000038F1
		public string ToString(int start, int end)
		{
			return this.ToString("default", start, end);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005700 File Offset: 0x00003900
		public string ToString(string programName, int start, int end)
		{
			IList list = (IList)this.programs[programName];
			if (list == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = start;
			while (num2 >= 0 && num2 <= end && num2 < this.tokens.Count)
			{
				if (num < list.Count)
				{
					TokenStreamRewriteEngine.RewriteOperation rewriteOperation = (TokenStreamRewriteEngine.RewriteOperation)list[num];
					while (num2 == rewriteOperation.index && num < list.Count)
					{
						num2 = rewriteOperation.execute(stringBuilder);
						num++;
						if (num < list.Count)
						{
							rewriteOperation = (TokenStreamRewriteEngine.RewriteOperation)list[num];
						}
					}
				}
				if (num2 < end)
				{
					stringBuilder.Append(this.getToken(num2).getText());
					num2++;
				}
			}
			for (int i = num; i < list.Count; i++)
			{
				TokenStreamRewriteEngine.RewriteOperation rewriteOperation2 = (TokenStreamRewriteEngine.RewriteOperation)list[i];
				rewriteOperation2.execute(stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000057E7 File Offset: 0x000039E7
		public string ToDebugString()
		{
			return this.ToDebugString(0, this.getTokenStreamSize());
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000057F8 File Offset: 0x000039F8
		public string ToDebugString(int start, int end)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = start;
			while (num >= 0 && num <= end && num < this.tokens.Count)
			{
				stringBuilder.Append(this.getToken(num));
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000583D File Offset: 0x00003A3D
		public int getLastRewriteTokenIndex()
		{
			return this.getLastRewriteTokenIndex("default");
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000584C File Offset: 0x00003A4C
		protected int getLastRewriteTokenIndex(string programName)
		{
			object obj = this.lastRewriteTokenIndexes[programName];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005871 File Offset: 0x00003A71
		protected void setLastRewriteTokenIndex(string programName, int i)
		{
			this.lastRewriteTokenIndexes[programName] = i;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005888 File Offset: 0x00003A88
		protected IList getProgram(string name)
		{
			IList list = (IList)this.programs[name];
			if (list == null)
			{
				list = this.initializeProgram(name);
			}
			return list;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000058B4 File Offset: 0x00003AB4
		private IList initializeProgram(string name)
		{
			IList list = new ArrayList(100);
			this.programs[name] = list;
			return list;
		}

		// Token: 0x04000097 RID: 151
		public const int MIN_TOKEN_INDEX = 0;

		// Token: 0x04000098 RID: 152
		public const string DEFAULT_PROGRAM_NAME = "default";

		// Token: 0x04000099 RID: 153
		public const int PROGRAM_INIT_SIZE = 100;

		// Token: 0x0400009A RID: 154
		protected IList tokens;

		// Token: 0x0400009B RID: 155
		protected IDictionary programs;

		// Token: 0x0400009C RID: 156
		protected IDictionary lastRewriteTokenIndexes;

		// Token: 0x0400009D RID: 157
		protected int _index;

		// Token: 0x0400009E RID: 158
		protected TokenStream stream;

		// Token: 0x0400009F RID: 159
		protected BitSet discardMask = new BitSet();

		// Token: 0x0200002C RID: 44
		protected class RewriteOperation
		{
			// Token: 0x0600018A RID: 394 RVA: 0x000058D7 File Offset: 0x00003AD7
			protected RewriteOperation(int index, string text)
			{
				this.index = index;
				this.text = text;
			}

			// Token: 0x0600018B RID: 395 RVA: 0x000058ED File Offset: 0x00003AED
			public virtual int execute(StringBuilder buf)
			{
				return this.index;
			}

			// Token: 0x040000A0 RID: 160
			protected internal int index;

			// Token: 0x040000A1 RID: 161
			protected internal string text;
		}

		// Token: 0x0200002D RID: 45
		protected class InsertBeforeOp : TokenStreamRewriteEngine.RewriteOperation
		{
			// Token: 0x0600018C RID: 396 RVA: 0x000058F5 File Offset: 0x00003AF5
			public InsertBeforeOp(int index, string text)
				: base(index, text)
			{
			}

			// Token: 0x0600018D RID: 397 RVA: 0x000058FF File Offset: 0x00003AFF
			public override int execute(StringBuilder buf)
			{
				buf.Append(this.text);
				return this.index;
			}
		}

		// Token: 0x0200002E RID: 46
		protected class ReplaceOp : TokenStreamRewriteEngine.RewriteOperation
		{
			// Token: 0x0600018E RID: 398 RVA: 0x00005914 File Offset: 0x00003B14
			public ReplaceOp(int from, int to, string text)
				: base(from, text)
			{
				this.lastIndex = to;
			}

			// Token: 0x0600018F RID: 399 RVA: 0x00005925 File Offset: 0x00003B25
			public override int execute(StringBuilder buf)
			{
				if (this.text != null)
				{
					buf.Append(this.text);
				}
				return this.lastIndex + 1;
			}

			// Token: 0x040000A2 RID: 162
			protected int lastIndex;
		}

		// Token: 0x0200002F RID: 47
		protected class DeleteOp : TokenStreamRewriteEngine.ReplaceOp
		{
			// Token: 0x06000190 RID: 400 RVA: 0x00005944 File Offset: 0x00003B44
			public DeleteOp(int from, int to)
				: base(from, to, null)
			{
			}
		}

		// Token: 0x02000030 RID: 48
		internal class RewriteOperationComparer : IComparer
		{
			// Token: 0x06000191 RID: 401 RVA: 0x00005950 File Offset: 0x00003B50
			public virtual int Compare(object o1, object o2)
			{
				TokenStreamRewriteEngine.RewriteOperation rewriteOperation = (TokenStreamRewriteEngine.RewriteOperation)o1;
				TokenStreamRewriteEngine.RewriteOperation rewriteOperation2 = (TokenStreamRewriteEngine.RewriteOperation)o2;
				if (rewriteOperation.index < rewriteOperation2.index)
				{
					return -1;
				}
				if (rewriteOperation.index > rewriteOperation2.index)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x040000A3 RID: 163
			public static readonly TokenStreamRewriteEngine.RewriteOperationComparer Default = new TokenStreamRewriteEngine.RewriteOperationComparer();
		}
	}
}
