using System;
using antlr.collections.impl;

namespace antlr
{
	// Token: 0x02000018 RID: 24
	internal abstract class Parser
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00003E6B File Offset: 0x0000206B
		public Parser()
		{
			this.inputState = new ParserSharedInputState();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003E7E File Offset: 0x0000207E
		public Parser(ParserSharedInputState state)
		{
			this.inputState = state;
		}

		// Token: 0x060000E3 RID: 227
		public abstract void consume();

		// Token: 0x060000E4 RID: 228 RVA: 0x00003E8D File Offset: 0x0000208D
		public virtual void consumeUntil(int tokenType)
		{
			while (this.LA(1) != 1 && this.LA(1) != tokenType)
			{
				this.consume();
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003EAB File Offset: 0x000020AB
		public virtual void consumeUntil(BitSet bset)
		{
			while (this.LA(1) != 1 && !bset.member(this.LA(1)))
			{
				this.consume();
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003ECE File Offset: 0x000020CE
		protected internal virtual void defaultDebuggingSetup(TokenStream lexer, TokenBuffer tokBuf)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003ED0 File Offset: 0x000020D0
		public virtual string getFilename()
		{
			return this.inputState.filename;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003EDD File Offset: 0x000020DD
		public virtual ParserSharedInputState getInputState()
		{
			return this.inputState;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003EE5 File Offset: 0x000020E5
		public virtual void setInputState(ParserSharedInputState state)
		{
			this.inputState = state;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003EEE File Offset: 0x000020EE
		public virtual void resetState()
		{
			this.traceDepth = 0;
			this.inputState.reset();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003F02 File Offset: 0x00002102
		public virtual string getTokenName(int num)
		{
			return this.tokenNames[num];
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003F0C File Offset: 0x0000210C
		public virtual string[] getTokenNames()
		{
			return this.tokenNames;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003F14 File Offset: 0x00002114
		public virtual bool isDebugMode()
		{
			return false;
		}

		// Token: 0x060000EE RID: 238
		public abstract int LA(int i);

		// Token: 0x060000EF RID: 239
		public abstract IToken LT(int i);

		// Token: 0x060000F0 RID: 240 RVA: 0x00003F17 File Offset: 0x00002117
		public virtual int mark()
		{
			return this.inputState.input.mark();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003F29 File Offset: 0x00002129
		public virtual void match(int t)
		{
			if (this.LA(1) != t)
			{
				throw new MismatchedTokenException(this.tokenNames, this.LT(1), t, false, this.getFilename());
			}
			this.consume();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003F56 File Offset: 0x00002156
		public virtual void match(BitSet b)
		{
			if (!b.member(this.LA(1)))
			{
				throw new MismatchedTokenException(this.tokenNames, this.LT(1), b, false, this.getFilename());
			}
			this.consume();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003F88 File Offset: 0x00002188
		public virtual void matchNot(int t)
		{
			if (this.LA(1) == t)
			{
				throw new MismatchedTokenException(this.tokenNames, this.LT(1), t, true, this.getFilename());
			}
			this.consume();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003FB5 File Offset: 0x000021B5
		public virtual void reportError(RecognitionException ex)
		{
			Console.Error.WriteLine(ex);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003FC2 File Offset: 0x000021C2
		public virtual void reportError(string s)
		{
			if (this.getFilename() == null)
			{
				Console.Error.WriteLine("error: " + s);
				return;
			}
			Console.Error.WriteLine(this.getFilename() + ": error: " + s);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003FFD File Offset: 0x000021FD
		public virtual void reportWarning(string s)
		{
			if (this.getFilename() == null)
			{
				Console.Error.WriteLine("warning: " + s);
				return;
			}
			Console.Error.WriteLine(this.getFilename() + ": warning: " + s);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004038 File Offset: 0x00002238
		public virtual void recover(RecognitionException ex, BitSet tokenSet)
		{
			this.consume();
			this.consumeUntil(tokenSet);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004047 File Offset: 0x00002247
		public virtual void rewind(int pos)
		{
			this.inputState.input.rewind(pos);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000405A File Offset: 0x0000225A
		public virtual void setDebugMode(bool debugMode)
		{
			if (!this.ignoreInvalidDebugCalls)
			{
				throw new ANTLRException("setDebugMode() only valid if parser built for debugging");
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000406F File Offset: 0x0000226F
		public virtual void setFilename(string f)
		{
			this.inputState.filename = f;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000407D File Offset: 0x0000227D
		public virtual void setIgnoreInvalidDebugCalls(bool Value)
		{
			this.ignoreInvalidDebugCalls = Value;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004086 File Offset: 0x00002286
		public virtual void setTokenBuffer(TokenBuffer t)
		{
			this.inputState.input = t;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004094 File Offset: 0x00002294
		public virtual void traceIndent()
		{
			for (int i = 0; i < this.traceDepth; i++)
			{
				Console.Out.Write(" ");
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000040C4 File Offset: 0x000022C4
		public virtual void traceIn(string rname)
		{
			this.traceDepth++;
			this.traceIndent();
			Console.Out.WriteLine(string.Concat(new string[]
			{
				"> ",
				rname,
				"; LA(1)==",
				this.LT(1).getText(),
				(this.inputState.guessing > 0) ? " [guessing]" : ""
			}));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000413C File Offset: 0x0000233C
		public virtual void traceOut(string rname)
		{
			this.traceIndent();
			Console.Out.WriteLine(string.Concat(new string[]
			{
				"< ",
				rname,
				"; LA(1)==",
				this.LT(1).getText(),
				(this.inputState.guessing > 0) ? " [guessing]" : ""
			}));
			this.traceDepth--;
		}

		// Token: 0x0400004F RID: 79
		internal static readonly object EnterRuleEventKey = new object();

		// Token: 0x04000050 RID: 80
		internal static readonly object ExitRuleEventKey = new object();

		// Token: 0x04000051 RID: 81
		internal static readonly object DoneEventKey = new object();

		// Token: 0x04000052 RID: 82
		internal static readonly object ReportErrorEventKey = new object();

		// Token: 0x04000053 RID: 83
		internal static readonly object ReportWarningEventKey = new object();

		// Token: 0x04000054 RID: 84
		internal static readonly object NewLineEventKey = new object();

		// Token: 0x04000055 RID: 85
		internal static readonly object MatchEventKey = new object();

		// Token: 0x04000056 RID: 86
		internal static readonly object MatchNotEventKey = new object();

		// Token: 0x04000057 RID: 87
		internal static readonly object MisMatchEventKey = new object();

		// Token: 0x04000058 RID: 88
		internal static readonly object MisMatchNotEventKey = new object();

		// Token: 0x04000059 RID: 89
		internal static readonly object ConsumeEventKey = new object();

		// Token: 0x0400005A RID: 90
		internal static readonly object LAEventKey = new object();

		// Token: 0x0400005B RID: 91
		internal static readonly object SemPredEvaluatedEventKey = new object();

		// Token: 0x0400005C RID: 92
		internal static readonly object SynPredStartedEventKey = new object();

		// Token: 0x0400005D RID: 93
		internal static readonly object SynPredFailedEventKey = new object();

		// Token: 0x0400005E RID: 94
		internal static readonly object SynPredSucceededEventKey = new object();

		// Token: 0x0400005F RID: 95
		protected internal ParserSharedInputState inputState;

		// Token: 0x04000060 RID: 96
		protected internal string[] tokenNames;

		// Token: 0x04000061 RID: 97
		private bool ignoreInvalidDebugCalls;

		// Token: 0x04000062 RID: 98
		protected internal int traceDepth;
	}
}
