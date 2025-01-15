using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using antlr.collections.impl;

namespace antlr
{
	// Token: 0x0200000A RID: 10
	internal abstract class CharScanner : TokenStream
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002E46 File Offset: 0x00001046
		public CharScanner()
		{
			this.text = new StringBuilder();
			this.setTokenCreator(new CommonToken.CommonTokenCreator());
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E80 File Offset: 0x00001080
		public CharScanner(InputBuffer cb)
			: this()
		{
			this.inputState = new LexerSharedInputState(cb);
			this.cached_LA2 = this.inputState.input.LA(2);
			this.cached_LA1 = this.inputState.input.LA(1);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002ED0 File Offset: 0x000010D0
		public CharScanner(LexerSharedInputState sharedState)
			: this()
		{
			this.inputState = sharedState;
			if (this.inputState != null)
			{
				this.cached_LA2 = this.inputState.input.LA(2);
				this.cached_LA1 = this.inputState.input.LA(1);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F20 File Offset: 0x00001120
		public virtual IToken nextToken()
		{
			return null;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F23 File Offset: 0x00001123
		public virtual void append(char c)
		{
			if (this.saveConsumedInput)
			{
				this.text.Append(c);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F3A File Offset: 0x0000113A
		public virtual void append(string s)
		{
			if (this.saveConsumedInput)
			{
				this.text.Append(s);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F51 File Offset: 0x00001151
		public virtual void commit()
		{
			this.inputState.input.commit();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F64 File Offset: 0x00001164
		public virtual void consume()
		{
			if (this.inputState.guessing == 0)
			{
				if (this.caseSensitive)
				{
					this.append(this.cached_LA1);
				}
				else
				{
					this.append(this.inputState.input.LA(1));
				}
				if (this.cached_LA1 == '\t')
				{
					this.tab();
				}
				else
				{
					this.inputState.column++;
				}
			}
			if (this.caseSensitive)
			{
				this.cached_LA1 = this.inputState.input.consume();
				this.cached_LA2 = this.inputState.input.LA(2);
				return;
			}
			this.cached_LA1 = this.toLower((int)this.inputState.input.consume());
			this.cached_LA2 = this.toLower((int)this.inputState.input.LA(2));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000303F File Offset: 0x0000123F
		public virtual void consumeUntil(int c)
		{
			while (CharScanner.EOF_CHAR != this.cached_LA1 && c != (int)this.cached_LA1)
			{
				this.consume();
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000305F File Offset: 0x0000125F
		public virtual void consumeUntil(BitSet bset)
		{
			while (this.cached_LA1 != CharScanner.EOF_CHAR && !bset.member((int)this.cached_LA1))
			{
				this.consume();
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003084 File Offset: 0x00001284
		public virtual bool getCaseSensitive()
		{
			return this.caseSensitive;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000308C File Offset: 0x0000128C
		public bool getCaseSensitiveLiterals()
		{
			return this.caseSensitiveLiterals;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003094 File Offset: 0x00001294
		public virtual int getColumn()
		{
			return this.inputState.column;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030A1 File Offset: 0x000012A1
		public virtual void setColumn(int c)
		{
			this.inputState.column = c;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000030AF File Offset: 0x000012AF
		public virtual bool getCommitToPath()
		{
			return this.commitToPath;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000030B7 File Offset: 0x000012B7
		public virtual string getFilename()
		{
			return this.inputState.filename;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000030C4 File Offset: 0x000012C4
		public virtual InputBuffer getInputBuffer()
		{
			return this.inputState.input;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000030D1 File Offset: 0x000012D1
		public virtual LexerSharedInputState getInputState()
		{
			return this.inputState;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000030D9 File Offset: 0x000012D9
		public virtual void setInputState(LexerSharedInputState state)
		{
			this.inputState = state;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030E2 File Offset: 0x000012E2
		public virtual int getLine()
		{
			return this.inputState.line;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000030EF File Offset: 0x000012EF
		public virtual string getText()
		{
			return this.text.ToString();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030FC File Offset: 0x000012FC
		public virtual IToken getTokenObject()
		{
			return this.returnToken_;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003104 File Offset: 0x00001304
		public virtual char LA(int i)
		{
			if (i == 1)
			{
				return this.cached_LA1;
			}
			if (i == 2)
			{
				return this.cached_LA2;
			}
			if (this.caseSensitive)
			{
				return this.inputState.input.LA(i);
			}
			return this.toLower((int)this.inputState.input.LA(i));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003158 File Offset: 0x00001358
		protected internal virtual IToken makeToken(int t)
		{
			IToken token = null;
			bool flag;
			try
			{
				token = this.tokenCreator.Create();
				if (token != null)
				{
					token.Type = t;
					token.setColumn(this.inputState.tokenStartColumn);
					token.setLine(this.inputState.tokenStartLine);
					token.setFilename(this.inputState.filename);
				}
				flag = true;
			}
			catch
			{
				flag = false;
			}
			if (!flag)
			{
				this.panic("Can't create Token object '" + this.tokenCreator.TokenTypeName + "'");
				token = Token.badToken;
			}
			return token;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000031F4 File Offset: 0x000013F4
		public virtual int mark()
		{
			return this.inputState.input.mark();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003206 File Offset: 0x00001406
		public virtual void match(char c)
		{
			this.match((int)c);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000320F File Offset: 0x0000140F
		public virtual void match(int c)
		{
			if ((int)this.cached_LA1 != c)
			{
				throw new MismatchedCharException(this.cached_LA1, Convert.ToChar(c), false, this);
			}
			this.consume();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003234 File Offset: 0x00001434
		public virtual void match(BitSet b)
		{
			if (!b.member((int)this.cached_LA1))
			{
				throw new MismatchedCharException(this.cached_LA1, b, false, this);
			}
			this.consume();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000325C File Offset: 0x0000145C
		public virtual void match(string s)
		{
			int length = s.Length;
			for (int i = 0; i < length; i++)
			{
				if (this.cached_LA1 != s.get_Chars(i))
				{
					throw new MismatchedCharException(this.cached_LA1, s.get_Chars(i), false, this);
				}
				this.consume();
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000032A6 File Offset: 0x000014A6
		public virtual void matchNot(char c)
		{
			this.matchNot((int)c);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000032AF File Offset: 0x000014AF
		public virtual void matchNot(int c)
		{
			if ((int)this.cached_LA1 == c)
			{
				throw new MismatchedCharException(this.cached_LA1, Convert.ToChar(c), true, this);
			}
			this.consume();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000032D4 File Offset: 0x000014D4
		public virtual void matchRange(int c1, int c2)
		{
			if ((int)this.cached_LA1 < c1 || (int)this.cached_LA1 > c2)
			{
				throw new MismatchedCharException(this.cached_LA1, Convert.ToChar(c1), Convert.ToChar(c2), false, this);
			}
			this.consume();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003308 File Offset: 0x00001508
		public virtual void matchRange(char c1, char c2)
		{
			this.matchRange((int)c1, (int)c2);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003312 File Offset: 0x00001512
		public virtual void newline()
		{
			this.inputState.line++;
			this.inputState.column = 1;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003334 File Offset: 0x00001534
		public virtual void tab()
		{
			int column = this.getColumn();
			int num = ((column - 1) / this.tabsize + 1) * this.tabsize + 1;
			this.setColumn(num);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003365 File Offset: 0x00001565
		public virtual void setTabSize(int size)
		{
			this.tabsize = size;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual int getTabSize()
		{
			return this.tabsize;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003376 File Offset: 0x00001576
		public virtual void panic()
		{
			this.panic("");
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003383 File Offset: 0x00001583
		public virtual void panic(string s)
		{
			throw new ANTLRPanicException("CharScanner::panic: " + s);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003395 File Offset: 0x00001595
		public virtual void reportError(RecognitionException ex)
		{
			Console.Error.WriteLine(ex);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033A2 File Offset: 0x000015A2
		public virtual void reportError(string s)
		{
			if (this.getFilename() == null)
			{
				Console.Error.WriteLine("error: " + s);
				return;
			}
			Console.Error.WriteLine(this.getFilename() + ": error: " + s);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000033DD File Offset: 0x000015DD
		public virtual void reportWarning(string s)
		{
			if (this.getFilename() == null)
			{
				Console.Error.WriteLine("warning: " + s);
				return;
			}
			Console.Error.WriteLine(this.getFilename() + ": warning: " + s);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003418 File Offset: 0x00001618
		public virtual void refresh()
		{
			if (this.caseSensitive)
			{
				this.cached_LA2 = this.inputState.input.LA(2);
				this.cached_LA1 = this.inputState.input.LA(1);
				return;
			}
			this.cached_LA2 = this.toLower((int)this.inputState.input.LA(2));
			this.cached_LA1 = this.toLower((int)this.inputState.input.LA(1));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003496 File Offset: 0x00001696
		public virtual void resetState(InputBuffer ib)
		{
			this.text.Length = 0;
			this.traceDepth = 0;
			this.inputState.resetInput(ib);
			this.refresh();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000034BD File Offset: 0x000016BD
		public void resetState(Stream s)
		{
			this.resetState(new ByteBuffer(s));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000034CB File Offset: 0x000016CB
		public void resetState(TextReader tr)
		{
			this.resetState(new CharBuffer(tr));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000034D9 File Offset: 0x000016D9
		public virtual void resetText()
		{
			this.text.Length = 0;
			this.inputState.tokenStartColumn = this.inputState.column;
			this.inputState.tokenStartLine = this.inputState.line;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003514 File Offset: 0x00001714
		public virtual void rewind(int pos)
		{
			this.inputState.input.rewind(pos);
			this.cached_LA2 = this.inputState.input.LA(2);
			this.cached_LA1 = this.inputState.input.LA(1);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003560 File Offset: 0x00001760
		public virtual void setCaseSensitive(bool t)
		{
			this.caseSensitive = t;
			if (this.caseSensitive)
			{
				this.cached_LA2 = this.inputState.input.LA(2);
				this.cached_LA1 = this.inputState.input.LA(1);
				return;
			}
			this.cached_LA2 = this.toLower((int)this.inputState.input.LA(2));
			this.cached_LA1 = this.toLower((int)this.inputState.input.LA(1));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000035E5 File Offset: 0x000017E5
		public virtual void setCommitToPath(bool commit)
		{
			this.commitToPath = commit;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000035EE File Offset: 0x000017EE
		public virtual void setFilename(string f)
		{
			this.inputState.filename = f;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000035FC File Offset: 0x000017FC
		public virtual void setLine(int line)
		{
			this.inputState.line = line;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000360A File Offset: 0x0000180A
		public virtual void setText(string s)
		{
			this.resetText();
			this.text.Append(s);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000361F File Offset: 0x0000181F
		public virtual void setTokenObjectClass(string cl)
		{
			this.tokenCreator = new CharScanner.ReflectionBasedTokenCreator(cl);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000362D File Offset: 0x0000182D
		public virtual void setTokenCreator(TokenCreator newTokenCreator)
		{
			this.tokenCreator = newTokenCreator;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003638 File Offset: 0x00001838
		public virtual int testLiteralsTable(int ttype)
		{
			string text = this.text.ToString();
			if (text == null || text == string.Empty)
			{
				return ttype;
			}
			object obj = this.literals[text];
			if (obj != null)
			{
				return (int)obj;
			}
			return ttype;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000367C File Offset: 0x0000187C
		public virtual int testLiteralsTable(string someText, int ttype)
		{
			if (someText == null || someText == string.Empty)
			{
				return ttype;
			}
			object obj = this.literals[someText];
			if (obj != null)
			{
				return (int)obj;
			}
			return ttype;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000036B3 File Offset: 0x000018B3
		public virtual char toLower(int c)
		{
			return char.ToLower(Convert.ToChar(c), CultureInfo.InvariantCulture);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000036C8 File Offset: 0x000018C8
		public virtual void traceIndent()
		{
			for (int i = 0; i < this.traceDepth; i++)
			{
				Console.Out.Write(" ");
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000036F8 File Offset: 0x000018F8
		public virtual void traceIn(string rname)
		{
			this.traceDepth++;
			this.traceIndent();
			Console.Out.WriteLine(string.Concat(new object[]
			{
				"> lexer ",
				rname,
				"; c==",
				this.LA(1)
			}));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003754 File Offset: 0x00001954
		public virtual void traceOut(string rname)
		{
			this.traceIndent();
			Console.Out.WriteLine(string.Concat(new object[]
			{
				"< lexer ",
				rname,
				"; c==",
				this.LA(1)
			}));
			this.traceDepth--;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000037AF File Offset: 0x000019AF
		public virtual void uponEOF()
		{
		}

		// Token: 0x04000014 RID: 20
		internal const char NO_CHAR = '\0';

		// Token: 0x04000015 RID: 21
		public static readonly char EOF_CHAR = char.MaxValue;

		// Token: 0x04000016 RID: 22
		internal static readonly object EnterRuleEventKey = new object();

		// Token: 0x04000017 RID: 23
		internal static readonly object ExitRuleEventKey = new object();

		// Token: 0x04000018 RID: 24
		internal static readonly object DoneEventKey = new object();

		// Token: 0x04000019 RID: 25
		internal static readonly object ReportErrorEventKey = new object();

		// Token: 0x0400001A RID: 26
		internal static readonly object ReportWarningEventKey = new object();

		// Token: 0x0400001B RID: 27
		internal static readonly object NewLineEventKey = new object();

		// Token: 0x0400001C RID: 28
		internal static readonly object MatchEventKey = new object();

		// Token: 0x0400001D RID: 29
		internal static readonly object MatchNotEventKey = new object();

		// Token: 0x0400001E RID: 30
		internal static readonly object MisMatchEventKey = new object();

		// Token: 0x0400001F RID: 31
		internal static readonly object MisMatchNotEventKey = new object();

		// Token: 0x04000020 RID: 32
		internal static readonly object ConsumeEventKey = new object();

		// Token: 0x04000021 RID: 33
		internal static readonly object LAEventKey = new object();

		// Token: 0x04000022 RID: 34
		internal static readonly object SemPredEvaluatedEventKey = new object();

		// Token: 0x04000023 RID: 35
		internal static readonly object SynPredStartedEventKey = new object();

		// Token: 0x04000024 RID: 36
		internal static readonly object SynPredFailedEventKey = new object();

		// Token: 0x04000025 RID: 37
		internal static readonly object SynPredSucceededEventKey = new object();

		// Token: 0x04000026 RID: 38
		protected internal StringBuilder text;

		// Token: 0x04000027 RID: 39
		protected bool saveConsumedInput = true;

		// Token: 0x04000028 RID: 40
		protected TokenCreator tokenCreator;

		// Token: 0x04000029 RID: 41
		protected char cached_LA1;

		// Token: 0x0400002A RID: 42
		protected char cached_LA2;

		// Token: 0x0400002B RID: 43
		protected bool caseSensitive = true;

		// Token: 0x0400002C RID: 44
		protected bool caseSensitiveLiterals = true;

		// Token: 0x0400002D RID: 45
		protected Hashtable literals;

		// Token: 0x0400002E RID: 46
		protected internal int tabsize = 8;

		// Token: 0x0400002F RID: 47
		protected internal IToken returnToken_;

		// Token: 0x04000030 RID: 48
		protected internal LexerSharedInputState inputState;

		// Token: 0x04000031 RID: 49
		protected internal bool commitToPath;

		// Token: 0x04000032 RID: 50
		protected internal int traceDepth;

		// Token: 0x0200000C RID: 12
		private class ReflectionBasedTokenCreator : TokenCreator
		{
			// Token: 0x0600008E RID: 142 RVA: 0x00003873 File Offset: 0x00001A73
			public ReflectionBasedTokenCreator(string tokenTypeName)
			{
				this.SetTokenType(tokenTypeName);
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00003884 File Offset: 0x00001A84
			private void SetTokenType(string tokenTypeName)
			{
				this._tokenTypeName = tokenTypeName;
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					try
					{
						this._tokenTypeObject = assembly.GetType(tokenTypeName);
						if (this._tokenTypeObject != null)
						{
							break;
						}
					}
					catch
					{
						throw new TypeLoadException("Unable to load Type for Token class '" + tokenTypeName + "'");
					}
				}
				if (this._tokenTypeObject == null)
				{
					throw new TypeLoadException("Unable to load Type for Token class '" + tokenTypeName + "'");
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00003914 File Offset: 0x00001B14
			public override string TokenTypeName
			{
				get
				{
					return this._tokenTypeName;
				}
			}

			// Token: 0x06000091 RID: 145 RVA: 0x0000391C File Offset: 0x00001B1C
			public override IToken Create()
			{
				return (Token)Activator.CreateInstance(this._tokenTypeObject);
			}

			// Token: 0x04000033 RID: 51
			private string _tokenTypeName;

			// Token: 0x04000034 RID: 52
			private Type _tokenTypeObject;
		}
	}
}
