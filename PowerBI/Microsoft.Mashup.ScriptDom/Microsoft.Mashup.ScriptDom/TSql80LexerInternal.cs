using System;
using System.Collections;
using System.IO;
using antlr;
using antlr.collections.impl;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200006D RID: 109
	internal class TSql80LexerInternal : TSqlLexerBaseInternal, TokenStream
	{
		// Token: 0x06000243 RID: 579 RVA: 0x00006AEC File Offset: 0x00004CEC
		public override int testLiteralsTable(int ttype)
		{
			string text = this.text.ToString();
			if (text == null || text == string.Empty)
			{
				return ttype;
			}
			if (text.Substring(text.Length - 1, 1) == ":")
			{
				text = text.Substring(0, text.Length - 1);
			}
			return this.testLiteralsTable(text, ttype);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006B4A File Offset: 0x00004D4A
		public TSql80LexerInternal()
			: this(new StringReader(string.Empty))
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006B5C File Offset: 0x00004D5C
		public TSql80LexerInternal(Stream ins)
			: this(new ByteBuffer(ins))
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006B6A File Offset: 0x00004D6A
		public TSql80LexerInternal(TextReader r)
			: this(new CharBuffer(r))
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006B78 File Offset: 0x00004D78
		public TSql80LexerInternal(InputBuffer ib)
			: this(new LexerSharedInputState(ib))
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006B86 File Offset: 0x00004D86
		public TSql80LexerInternal(LexerSharedInputState state)
			: base(state)
		{
			this.initialize();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00006B98 File Offset: 0x00004D98
		private void initialize()
		{
			this.caseSensitiveLiterals = false;
			this.setCaseSensitive(false);
			this.literals = new Hashtable(100, 0.4f, StringComparer.OrdinalIgnoreCase);
			this.literals.Add("index", 84);
			this.literals.Add("full", 72);
			this.literals.Add("cross", 36);
			this.literals.Add("close", 23);
			this.literals.Add("union", 158);
			this.literals.Add("coalesce", 25);
			this.literals.Add("use", 162);
			this.literals.Add("escape", 58);
			this.literals.Add("right", 133);
			this.literals.Add("with", 171);
			this.literals.Add("replication", 128);
			this.literals.Add("over", 115);
			this.literals.Add("collate", 26);
			this.literals.Add("values", 164);
			this.literals.Add("freetexttable", 70);
			this.literals.Add("contains", 31);
			this.literals.Add("percent", 116);
			this.literals.Add("errlvl", 57);
			this.literals.Add("holdlock", 78);
			this.literals.Add("varying", 165);
			this.literals.Add("cursor", 42);
			this.literals.Add("references", 127);
			this.literals.Add("continue", 33);
			this.literals.Add("for", 67);
			this.literals.Add("else", 55);
			this.literals.Add("is", 89);
			this.literals.Add("insert", 86);
			this.literals.Add("of", 102);
			this.literals.Add("and", 7);
			this.literals.Add("inner", 85);
			this.literals.Add("user", 163);
			this.literals.Add("lineno", 95);
			this.literals.Add("as", 9);
			this.literals.Add("some", 145);
			this.literals.Add("database", 43);
			this.literals.Add("nullif", 101);
			this.literals.Add("distinct", 51);
			this.literals.Add("key", 91);
			this.literals.Add("truncate", 156);
			this.literals.Add("desc", 50);
			this.literals.Add("current_timestamp", 40);
			this.literals.Add("public", 122);
			this.literals.Add("exists", 62);
			this.literals.Add("like", 94);
			this.literals.Add("clustered", 24);
			this.literals.Add("left", 93);
			this.literals.Add("shutdown", 144);
			this.literals.Add("openrowset", 109);
			this.literals.Add("fetch", 64);
			this.literals.Add("tran", 153);
			this.literals.Add("open", 106);
			this.literals.Add("between", 14);
			this.literals.Add("function", 73);
			this.literals.Add("system_user", 147);
			this.literals.Add("identity_insert", 80);
			this.literals.Add("backup", 12);
			this.literals.Add("national", 96);
			this.literals.Add("compute", 29);
			this.literals.Add("intersect", 87);
			this.literals.Add("view", 166);
			this.literals.Add("bulk", 17);
			this.literals.Add("any", 8);
			this.literals.Add("fillfactor", 66);
			this.literals.Add("not", 99);
			this.literals.Add("off", 103);
			this.literals.Add("updatetext", 161);
			this.literals.Add("all", 5);
			this.literals.Add("in", 83);
			this.literals.Add("except", 59);
			this.literals.Add("file", 65);
			this.literals.Add("declare", 46);
			this.literals.Add("column", 27);
			this.literals.Add("opendatasource", 107);
			this.literals.Add("execute", 61);
			this.literals.Add("reconfigure", 126);
			this.literals.Add("transaction", 154);
			this.literals.Add("proc", 120);
			this.literals.Add("unique", 159);
			this.literals.Add("identitycol", 81);
			this.literals.Add("offsets", 104);
			this.literals.Add("restrict", 130);
			this.literals.Add("delete", 48);
			this.literals.Add("when", 168);
			this.literals.Add("null", 100);
			this.literals.Add("join", 90);
			this.literals.Add("top", 152);
			this.literals.Add("foreign", 68);
			this.literals.Add("rule", 137);
			this.literals.Add("deallocate", 45);
			this.literals.Add("procedure", 121);
			this.literals.Add("freetext", 69);
			this.literals.Add("disk", 173);
			this.literals.Add("convert", 34);
			this.literals.Add("restore", 129);
			this.literals.Add("readtext", 125);
			this.literals.Add("begin", 13);
			this.literals.Add("cascade", 19);
			this.literals.Add("to", 151);
			this.literals.Add("into", 88);
			this.literals.Add("current_date", 38);
			this.literals.Add("case", 20);
			this.literals.Add("check", 21);
			this.literals.Add("outer", 114);
			this.literals.Add("deny", 49);
			this.literals.Add("primary", 118);
			this.literals.Add("plan", 117);
			this.literals.Add("current_user", 41);
			this.literals.Add("textsize", 149);
			this.literals.Add("grant", 75);
			this.literals.Add("break", 15);
			this.literals.Add("containstable", 32);
			this.literals.Add("print", 119);
			this.literals.Add("openquery", 108);
			this.literals.Add("current_time", 39);
			this.literals.Add("checkpoint", 22);
			this.literals.Add("nonclustered", 98);
			this.literals.Add("current", 37);
			this.literals.Add("having", 77);
			this.literals.Add("statistics", 146);
			this.literals.Add("precision", 174);
			this.literals.Add("then", 150);
			this.literals.Add("set", 142);
			this.literals.Add("update", 160);
			this.literals.Add("schema", 139);
			this.literals.Add("save", 138);
			this.literals.Add("browse", 16);
			this.literals.Add("or", 112);
			this.literals.Add("if", 82);
			this.literals.Add("exec", 60);
			this.literals.Add("by", 18);
			this.literals.Add("add", 4);
			this.literals.Add("distributed", 52);
			this.literals.Add("return", 131);
			this.literals.Add("dump", 180);
			this.literals.Add("from", 71);
			this.literals.Add("openxml", 110);
			this.literals.Add("read", 124);
			this.literals.Add("setuser", 143);
			this.literals.Add("load", 181);
			this.literals.Add("nocheck", 97);
			this.literals.Add("drop", 54);
			this.literals.Add("rowguidcol", 136);
			this.literals.Add("kill", 92);
			this.literals.Add("waitfor", 167);
			this.literals.Add("dbcc", 44);
			this.literals.Add("revoke", 132);
			this.literals.Add("identity", 79);
			this.literals.Add("option", 111);
			this.literals.Add("while", 170);
			this.literals.Add("rowcount", 135);
			this.literals.Add("end", 56);
			this.literals.Add("commit", 28);
			this.literals.Add("goto", 74);
			this.literals.Add("on", 105);
			this.literals.Add("create", 35);
			this.literals.Add("select", 140);
			this.literals.Add("authorization", 11);
			this.literals.Add("raiserror", 123);
			this.literals.Add("session_user", 141);
			this.literals.Add("order", 113);
			this.literals.Add("constraint", 30);
			this.literals.Add("tsequal", 157);
			this.literals.Add("where", 169);
			this.literals.Add("alter", 6);
			this.literals.Add("writetext", 172);
			this.literals.Add("asc", 10);
			this.literals.Add("double", 53);
			this.literals.Add("default", 47);
			this.literals.Add("trigger", 155);
			this.literals.Add("rollback", 134);
			this.literals.Add("group", 76);
			this.literals.Add("exit", 63);
			this.literals.Add("table", 148);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007BE4 File Offset: 0x00005DE4
		public override IToken nextToken()
		{
			IToken returnToken_34;
			for (;;)
			{
				this.resetText();
				try
				{
					char cached_LA = this.cached_LA1;
					switch (cached_LA)
					{
					case '\u0001':
					case '\u0002':
					case '\u0003':
					case '\u0004':
					case '\u0005':
					case '\u0006':
					case '\a':
					case '\b':
					case '\t':
					case '\n':
					case '\v':
					case '\f':
					case '\r':
					case '\u000e':
					case '\u000f':
					case '\u0010':
					case '\u0011':
					case '\u0012':
					case '\u0013':
					case '\u0014':
					case '\u0015':
					case '\u0016':
					case '\u0017':
					case '\u0018':
					case '\u0019':
					case '\u001a':
					case '\u001b':
					case '\u001c':
					case '\u001d':
					case '\u001e':
					case '\u001f':
					case ' ':
					{
						this.mWhiteSpace(true);
						IToken returnToken_ = this.returnToken_;
						goto IL_0535;
					}
					case '!':
					{
						this.mBang(true);
						IToken returnToken_2 = this.returnToken_;
						goto IL_0535;
					}
					case '"':
					case '[':
					{
						this.mQuotedIdentifier(true);
						IToken returnToken_3 = this.returnToken_;
						goto IL_0535;
					}
					case '#':
					case '$':
					case '*':
					case '-':
					case '/':
					case ':':
					case '=':
					case '?':
					case 'A':
					case 'B':
					case 'C':
					case 'D':
					case 'E':
					case 'F':
					case 'G':
					case 'H':
					case 'I':
					case 'J':
					case 'K':
					case 'L':
					case 'M':
					case 'N':
					case 'O':
					case 'P':
					case 'Q':
					case 'R':
					case 'S':
					case 'T':
					case 'U':
					case 'V':
					case 'W':
					case 'X':
					case 'Y':
					case 'Z':
					case '\\':
					case ']':
						break;
					case '%':
					{
						this.mPercentSign(true);
						IToken returnToken_4 = this.returnToken_;
						goto IL_0535;
					}
					case '&':
					{
						this.mAmpersand(true);
						IToken returnToken_5 = this.returnToken_;
						goto IL_0535;
					}
					case '\'':
					{
						this.mAsciiStringLiteral(true);
						IToken returnToken_6 = this.returnToken_;
						goto IL_0535;
					}
					case '(':
					{
						this.mLeftParenthesis(true);
						IToken returnToken_7 = this.returnToken_;
						goto IL_0535;
					}
					case ')':
					{
						this.mRightParenthesis(true);
						IToken returnToken_8 = this.returnToken_;
						goto IL_0535;
					}
					case '+':
					{
						this.mPlus(true);
						IToken returnToken_9 = this.returnToken_;
						goto IL_0535;
					}
					case ',':
					{
						this.mComma(true);
						IToken returnToken_10 = this.returnToken_;
						goto IL_0535;
					}
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					{
						this.mNumber(true);
						IToken returnToken_11 = this.returnToken_;
						goto IL_0535;
					}
					case ';':
					{
						this.mProcNameSemicolon(true);
						IToken returnToken_12 = this.returnToken_;
						goto IL_0535;
					}
					case '<':
					{
						this.mLessThan(true);
						IToken returnToken_13 = this.returnToken_;
						goto IL_0535;
					}
					case '>':
					{
						this.mGreaterThan(true);
						IToken returnToken_14 = this.returnToken_;
						goto IL_0535;
					}
					case '@':
					{
						this.mVariable(true);
						IToken returnToken_15 = this.returnToken_;
						goto IL_0535;
					}
					case '^':
					{
						this.mCircumflex(true);
						IToken returnToken_16 = this.returnToken_;
						goto IL_0535;
					}
					default:
						switch (cached_LA)
						{
						case '{':
						{
							this.mLeftCurly(true);
							IToken returnToken_17 = this.returnToken_;
							goto IL_0535;
						}
						case '|':
						{
							this.mVerticalLine(true);
							IToken returnToken_18 = this.returnToken_;
							goto IL_0535;
						}
						case '}':
						{
							this.mRightCurly(true);
							IToken returnToken_19 = this.returnToken_;
							goto IL_0535;
						}
						case '~':
						{
							this.mTilde(true);
							IToken returnToken_20 = this.returnToken_;
							goto IL_0535;
						}
						}
						break;
					}
					if (this.cached_LA1 == '*' && this.cached_LA2 == '=')
					{
						this.mMultiplyEquals(true);
						IToken returnToken_21 = this.returnToken_;
					}
					else if (this.cached_LA1 == ':' && this.cached_LA2 == ':')
					{
						this.mDoubleColon(true);
						IToken returnToken_22 = this.returnToken_;
					}
					else if (this.cached_LA1 == '=' && this.cached_LA2 == '*')
					{
						this.mRightOuterJoin(true);
						IToken returnToken_23 = this.returnToken_;
					}
					else if (this.cached_LA1 == 'g' && this.cached_LA2 == 'o' && base.CurrentOffset == this._acceptableGoOffset)
					{
						this.mGo(true);
						IToken returnToken_24 = this.returnToken_;
					}
					else if (this.cached_LA1 == 'n' && this.cached_LA2 == '\'')
					{
						this.mUnicodeStringLiteral(true);
						IToken returnToken_25 = this.returnToken_;
					}
					else if (this.cached_LA1 == '-' && this.cached_LA2 == '-')
					{
						this.mSingleLineComment(true);
						IToken returnToken_26 = this.returnToken_;
					}
					else if (this.cached_LA1 == '/' && this.cached_LA2 == '*')
					{
						this.mMultilineComment(true);
						IToken returnToken_27 = this.returnToken_;
					}
					else if (this.cached_LA1 == '*')
					{
						this.mStar(true);
						IToken returnToken_28 = this.returnToken_;
					}
					else if (this.cached_LA1 == '-')
					{
						this.mMinus(true);
						IToken returnToken_29 = this.returnToken_;
					}
					else if (this.cached_LA1 == '/')
					{
						this.mDivide(true);
						IToken returnToken_30 = this.returnToken_;
					}
					else if (this.cached_LA1 == ':')
					{
						this.mColon(true);
						IToken returnToken_31 = this.returnToken_;
					}
					else if (this.cached_LA1 == '=')
					{
						this.mEqualsSign(true);
						IToken returnToken_32 = this.returnToken_;
					}
					else if (TSql80LexerInternal.tokenSet_0_.member((int)this.cached_LA1))
					{
						this.mIdentifier(true);
						IToken returnToken_33 = this.returnToken_;
					}
					else
					{
						if (this.cached_LA1 != CharScanner.EOF_CHAR)
						{
							throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
						}
						this.uponEOF();
						this.returnToken_ = this.makeToken(1);
					}
					IL_0535:
					if (this.returnToken_ == null)
					{
						continue;
					}
					int type = this.returnToken_.Type;
					this.returnToken_.Type = type;
					returnToken_34 = this.returnToken_;
				}
				catch (RecognitionException ex)
				{
					throw new TokenStreamRecognitionException(ex);
				}
				break;
			}
			return returnToken_34;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000817C File Offset: 0x0000637C
		public void mBang(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 188;
			this.match('!');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000081E4 File Offset: 0x000063E4
		public void mPercentSign(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 189;
			this.match('%');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000824C File Offset: 0x0000644C
		public void mAmpersand(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 190;
			this.match('&');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000082B4 File Offset: 0x000064B4
		public void mLeftParenthesis(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 191;
			this.match('(');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000831C File Offset: 0x0000651C
		public void mRightParenthesis(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 192;
			this.match(')');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008384 File Offset: 0x00006584
		public void mLeftCurly(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 193;
			this.match('{');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000083EC File Offset: 0x000065EC
		public void mRightCurly(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 194;
			this.match('}');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008454 File Offset: 0x00006654
		public void mStar(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 195;
			this.match('*');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000084BC File Offset: 0x000066BC
		public void mMultiplyEquals(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 196;
			this.match("*=");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008524 File Offset: 0x00006724
		public void mPlus(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 197;
			this.match('+');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000858C File Offset: 0x0000678C
		public void mComma(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 198;
			this.match(',');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000085F4 File Offset: 0x000067F4
		public void mMinus(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 199;
			this.match('-');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000865C File Offset: 0x0000685C
		protected void mDot(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 200;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000086BC File Offset: 0x000068BC
		public void mDivide(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 201;
			this.match('/');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008724 File Offset: 0x00006924
		public void mColon(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 202;
			this.match(':');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000878C File Offset: 0x0000698C
		public void mDoubleColon(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 203;
			this.match("::");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000087F4 File Offset: 0x000069F4
		public void mLessThan(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 205;
			this.match('<');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000885C File Offset: 0x00006A5C
		public void mEqualsSign(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 206;
			this.match('=');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000088C4 File Offset: 0x00006AC4
		public void mRightOuterJoin(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 207;
			this.match("=*");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000892C File Offset: 0x00006B2C
		public void mGreaterThan(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 208;
			this.match('>');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008994 File Offset: 0x00006B94
		public void mCircumflex(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 209;
			this.match('^');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000089FC File Offset: 0x00006BFC
		public void mVerticalLine(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 210;
			this.match('|');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008A64 File Offset: 0x00006C64
		public void mTilde(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 211;
			this.match('~');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008ACC File Offset: 0x00006CCC
		protected void mSemicolon(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 204;
			this.match(';');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008B34 File Offset: 0x00006D34
		protected void mDigit(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 240;
			this.matchRange('0', '9');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00008B9C File Offset: 0x00006D9C
		protected void mFirstLetter(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 241;
			char cached_LA = this.cached_LA1;
			if (cached_LA != '#')
			{
				switch (cached_LA)
				{
				case '_':
					this.match('_');
					goto IL_0108;
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
					this.matchRange('a', 'z');
					goto IL_0108;
				}
				if (this.cached_LA1 < '\u0080' || this.cached_LA1 > '\ufffe')
				{
					throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
				}
				this.matchRange('\u0080', '\ufffe');
			}
			else
			{
				this.match('#');
			}
			IL_0108:
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008CF0 File Offset: 0x00006EF0
		protected void mLetter(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 242;
			char cached_LA = this.cached_LA1;
			switch (cached_LA)
			{
			case '#':
				this.match('#');
				break;
			case '$':
				this.match('$');
				break;
			default:
				if (cached_LA != '@')
				{
					switch (cached_LA)
					{
					case '_':
						this.match('_');
						goto IL_012D;
					case 'a':
					case 'b':
					case 'c':
					case 'd':
					case 'e':
					case 'f':
					case 'g':
					case 'h':
					case 'i':
					case 'j':
					case 'k':
					case 'l':
					case 'm':
					case 'n':
					case 'o':
					case 'p':
					case 'q':
					case 'r':
					case 's':
					case 't':
					case 'u':
					case 'v':
					case 'w':
					case 'x':
					case 'y':
					case 'z':
						this.matchRange('a', 'z');
						goto IL_012D;
					}
					if (this.cached_LA1 < '\u0080' || this.cached_LA1 > '\ufffe')
					{
						throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
					}
					this.matchRange('\u0080', '\ufffe');
				}
				else
				{
					this.match('@');
				}
				break;
			}
			IL_012D:
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008E68 File Offset: 0x00007068
		protected void mMoneySign(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 243;
			char cached_LA = this.cached_LA1;
			if (cached_LA <= '¥')
			{
				if (cached_LA == '$')
				{
					this.match('$');
					goto IL_01C8;
				}
				switch (cached_LA)
				{
				case '£':
					this.match('£');
					goto IL_01C8;
				case '¤':
					this.match('¤');
					goto IL_01C8;
				case '¥':
					this.match('¥');
					goto IL_01C8;
				}
			}
			else
			{
				switch (cached_LA)
				{
				case '৲':
					this.match('৲');
					goto IL_01C8;
				case '৳':
					this.match('৳');
					goto IL_01C8;
				default:
					if (cached_LA == '฿')
					{
						this.match('฿');
						goto IL_01C8;
					}
					switch (cached_LA)
					{
					case '₡':
						this.match('₡');
						goto IL_01C8;
					case '₢':
						this.match('₢');
						goto IL_01C8;
					case '₣':
						this.match('₣');
						goto IL_01C8;
					case '₤':
						this.match('₤');
						goto IL_01C8;
					case '₦':
						this.match('₦');
						goto IL_01C8;
					case '₧':
						this.match('₧');
						goto IL_01C8;
					case '₨':
						this.match('₨');
						goto IL_01C8;
					case '₩':
						this.match('₩');
						goto IL_01C8;
					case '₪':
						this.match('₪');
						goto IL_01C8;
					case '₫':
						this.match('₫');
						goto IL_01C8;
					case '€':
						this.match('€');
						goto IL_01C8;
					}
					break;
				}
			}
			throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
			IL_01C8:
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000907C File Offset: 0x0000727C
		public void mProcNameSemicolon(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 236;
			bool flag = false;
			if (this.cached_LA1 == ';')
			{
				int num2 = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.mSemicolon(false);
					while (TSql80LexerInternal.tokenSet_1_.member((int)this.cached_LA1))
					{
						this.mWS_CHAR_WO_NEWLINE(false);
					}
					this.mNumber(false);
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num2);
				this.inputState.guessing--;
			}
			if (flag)
			{
				this.mSemicolon(false);
			}
			else
			{
				if (this.cached_LA1 != ';')
				{
					throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
				}
				this.mSemicolon(false);
				if (this.inputState.guessing == 0)
				{
					num = 204;
				}
			}
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000091B0 File Offset: 0x000073B0
		protected void mWS_CHAR_WO_NEWLINE(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 244;
			switch (this.cached_LA1)
			{
			case '\u0001':
				this.match('\u0001');
				goto IL_0226;
			case '\u0002':
				this.match('\u0002');
				goto IL_0226;
			case '\u0003':
				this.match('\u0003');
				goto IL_0226;
			case '\u0004':
				this.match('\u0004');
				goto IL_0226;
			case '\u0005':
				this.match('\u0005');
				goto IL_0226;
			case '\u0006':
				this.match('\u0006');
				goto IL_0226;
			case '\a':
				this.match('\a');
				goto IL_0226;
			case '\b':
				this.match('\b');
				goto IL_0226;
			case '\t':
				this.match('\t');
				goto IL_0226;
			case '\v':
				this.match('\v');
				goto IL_0226;
			case '\f':
				this.match('\f');
				goto IL_0226;
			case '\u000e':
				this.match('\u000e');
				goto IL_0226;
			case '\u000f':
				this.match('\u000f');
				goto IL_0226;
			case '\u0010':
				this.match('\u0010');
				goto IL_0226;
			case '\u0011':
				this.match('\u0011');
				goto IL_0226;
			case '\u0012':
				this.match('\u0012');
				goto IL_0226;
			case '\u0013':
				this.match('\u0013');
				goto IL_0226;
			case '\u0014':
				this.match('\u0014');
				goto IL_0226;
			case '\u0015':
				this.match('\u0015');
				goto IL_0226;
			case '\u0016':
				this.match('\u0016');
				goto IL_0226;
			case '\u0017':
				this.match('\u0017');
				goto IL_0226;
			case '\u0018':
				this.match('\u0018');
				goto IL_0226;
			case '\u0019':
				this.match('\u0019');
				goto IL_0226;
			case '\u001a':
				this.match('\u001a');
				goto IL_0226;
			case '\u001b':
				this.match('\u001b');
				goto IL_0226;
			case '\u001c':
				this.match('\u001c');
				goto IL_0226;
			case '\u001d':
				this.match('\u001d');
				goto IL_0226;
			case '\u001e':
				this.match('\u001e');
				goto IL_0226;
			case '\u001f':
				this.match('\u001f');
				goto IL_0226;
			case ' ':
				this.match(' ');
				goto IL_0226;
			}
			throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
			IL_0226:
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009420 File Offset: 0x00007620
		public void mNumber(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 245;
			if (this.cached_LA1 == '0' && this.cached_LA2 == 'x')
			{
				this.match("0x");
				for (;;)
				{
					char cached_LA = this.cached_LA1;
					switch (cached_LA)
					{
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						this.mDigit(false);
						break;
					default:
						switch (cached_LA)
						{
						case '\\':
							this.match('\\');
							this.mEndOfLine(false);
							continue;
						case 'a':
						case 'b':
						case 'c':
						case 'd':
						case 'e':
						case 'f':
							this.matchRange('a', 'f');
							continue;
						}
						goto Block_4;
					}
				}
				Block_4:
				if (this.inputState.guessing == 0)
				{
					num = 224;
				}
			}
			else if (this.cached_LA1 >= '0' && this.cached_LA1 <= '9')
			{
				int num2 = 0;
				while (this.cached_LA1 >= '0' && this.cached_LA1 <= '9')
				{
					this.mDigit(false);
					num2++;
				}
				if (num2 < 1)
				{
					throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
				}
				if (this.inputState.guessing == 0)
				{
					if (TSqlLexerBaseInternal.IsValueTooLargeForTokenInteger(this.text.ToString()))
					{
						num = 222;
					}
					else
					{
						num = 221;
					}
				}
				if (this.cached_LA1 == '.')
				{
					this.match('.');
					while (this.cached_LA1 >= '0' && this.cached_LA1 <= '9')
					{
						this.mDigit(false);
					}
					if (this.inputState.guessing == 0)
					{
						num = 222;
					}
				}
				if (this.cached_LA1 == 'e')
				{
					this.mExponent(false);
					if (this.inputState.guessing == 0)
					{
						num = 223;
					}
				}
			}
			else
			{
				if (this.cached_LA1 != '.')
				{
					throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
				}
				this.match('.');
				if (this.inputState.guessing == 0)
				{
					num = 200;
				}
				if (this.cached_LA1 >= '0' && this.cached_LA1 <= '9')
				{
					int num3 = 0;
					while (this.cached_LA1 >= '0' && this.cached_LA1 <= '9')
					{
						this.mDigit(false);
						num3++;
					}
					if (num3 < 1)
					{
						throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
					}
					if (this.inputState.guessing == 0)
					{
						num = 222;
					}
					if (this.cached_LA1 == 'e')
					{
						this.mExponent(false);
						if (this.inputState.guessing == 0)
						{
							num = 223;
						}
					}
				}
			}
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009740 File Offset: 0x00007940
		public void mWhiteSpace(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 239;
			bool flag = base.CurrentOffset == this._acceptableGoOffset;
			switch (this.cached_LA1)
			{
			case '\u0001':
			case '\u0002':
			case '\u0003':
			case '\u0004':
			case '\u0005':
			case '\u0006':
			case '\a':
			case '\b':
			case '\t':
			case '\v':
			case '\f':
			case '\u000e':
			case '\u000f':
			case '\u0010':
			case '\u0011':
			case '\u0012':
			case '\u0013':
			case '\u0014':
			case '\u0015':
			case '\u0016':
			case '\u0017':
			case '\u0018':
			case '\u0019':
			case '\u001a':
			case '\u001b':
			case '\u001c':
			case '\u001d':
			case '\u001e':
			case '\u001f':
			case ' ':
			{
				int num2 = 0;
				while (TSql80LexerInternal.tokenSet_1_.member((int)this.cached_LA1))
				{
					this.mWS_CHAR_WO_NEWLINE(false);
					if (this.inputState.guessing == 0 && flag)
					{
						this._acceptableGoOffset = base.CurrentOffset;
					}
					num2++;
				}
				if (num2 < 1)
				{
					throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
				}
				break;
			}
			case '\n':
			case '\r':
				this.mEndOfLine(false);
				if (this.inputState.guessing == 0)
				{
					this._acceptableGoOffset = base.CurrentOffset;
				}
				break;
			default:
				throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
			}
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000098E8 File Offset: 0x00007AE8
		protected void mEndOfLine(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 247;
			if (this.cached_LA1 == '\r' && this.cached_LA2 == '\n')
			{
				this.match("\r\n");
			}
			else if (this.cached_LA1 == '\r')
			{
				this.match('\r');
			}
			else
			{
				if (this.cached_LA1 != '\n')
				{
					throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
				}
				this.match('\n');
			}
			if (this.inputState.guessing == 0)
			{
				this.newline();
			}
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000099C0 File Offset: 0x00007BC0
		public void mGo(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 219;
			if (base.CurrentOffset != this._acceptableGoOffset)
			{
				throw new SemanticException(" CurrentOffset==_acceptableGoOffset ");
			}
			this.match("go");
			for (;;)
			{
				if (TSql80LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
				{
					this.mLetter(false);
					if (this.inputState.guessing == 0)
					{
						num = 232;
					}
				}
				else
				{
					if (this.cached_LA1 < '0' || this.cached_LA1 > '9')
					{
						break;
					}
					this.mDigit(false);
					if (this.inputState.guessing == 0)
					{
						num = 232;
					}
				}
			}
			bool flag = false;
			if (this.cached_LA1 == ':')
			{
				int num2 = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.mColon(false);
					this.matchNot(':');
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num2);
				this.inputState.guessing--;
			}
			if (flag)
			{
				this.mColon(false);
				if (this.inputState.guessing == 0)
				{
					num = 220;
				}
			}
			num = this.testLiteralsTable(num);
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009B30 File Offset: 0x00007D30
		protected void mLabel(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 220;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009B90 File Offset: 0x00007D90
		protected void mInteger(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 221;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009BF0 File Offset: 0x00007DF0
		protected void mNumeric(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 222;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009C50 File Offset: 0x00007E50
		protected void mReal(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 223;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009CB0 File Offset: 0x00007EB0
		protected void mHexLiteral(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 224;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009D10 File Offset: 0x00007F10
		protected void mExponent(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 246;
			this.match('e');
			switch (this.cached_LA1)
			{
			case '+':
				this.match('+');
				break;
			case '-':
				this.match('-');
				break;
			}
			while (this.cached_LA1 >= '0' && this.cached_LA1 <= '9')
			{
				this.mDigit(false);
			}
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009DC4 File Offset: 0x00007FC4
		protected void mMoney(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 225;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009E24 File Offset: 0x00008024
		protected void mSqlCommandIdentifier(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 226;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009E84 File Offset: 0x00008084
		protected void mPseudoColumn(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 227;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009EE4 File Offset: 0x000080E4
		protected void mDollarPartition(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 228;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009F44 File Offset: 0x00008144
		protected void mAsciiStringOrQuotedIdentifier(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 229;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009FA4 File Offset: 0x000081A4
		public void mAsciiStringLiteral(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 230;
			if (this.inputState.guessing == 0)
			{
				base.beginComplexToken();
			}
			this.match('\'');
			for (;;)
			{
				if (this.cached_LA1 == '\'' && this.cached_LA2 == '\'')
				{
					this.match('\'');
					this.match('\'');
				}
				else if (TSql80LexerInternal.tokenSet_3_.member((int)this.cached_LA1))
				{
					if (this.inputState.guessing == 0)
					{
						base.checkEOF(TSqlLexerBaseInternal.TokenKind.String);
					}
					this.match(TSql80LexerInternal.tokenSet_3_);
				}
				else
				{
					if (this.cached_LA1 != '\n' && this.cached_LA1 != '\r')
					{
						break;
					}
					this.mEndOfLine(false);
				}
			}
			this.match('\'');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A09C File Offset: 0x0000829C
		public void mUnicodeStringLiteral(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 231;
			if (this.inputState.guessing == 0)
			{
				base.beginComplexToken();
			}
			this.match('n');
			this.match('\'');
			for (;;)
			{
				if (this.cached_LA1 == '\'' && this.cached_LA2 == '\'')
				{
					this.match('\'');
					this.match('\'');
				}
				else if (TSql80LexerInternal.tokenSet_3_.member((int)this.cached_LA1))
				{
					if (this.inputState.guessing == 0)
					{
						base.checkEOF(TSqlLexerBaseInternal.TokenKind.String);
					}
					this.match(TSql80LexerInternal.tokenSet_3_);
				}
				else
				{
					if (this.cached_LA1 != '\n' && this.cached_LA1 != '\r')
					{
						break;
					}
					this.mEndOfLine(false);
				}
			}
			this.match('\'');
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A19C File Offset: 0x0000839C
		public void mIdentifier(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 232;
			bool flag = false;
			if (this.cached_LA1 == '$' && this.cached_LA2 == '(')
			{
				int num2 = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match('$');
					this.mLeftParenthesis(false);
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num2);
				this.inputState.guessing--;
			}
			if (flag)
			{
				if (this.inputState.guessing == 0)
				{
					base.beginComplexToken();
				}
				this.match('$');
				this.mLeftParenthesis(false);
				int num3 = 0;
				for (;;)
				{
					if (TSql80LexerInternal.tokenSet_4_.member((int)this.cached_LA1))
					{
						if (this.inputState.guessing == 0)
						{
							base.checkEOF(TSqlLexerBaseInternal.TokenKind.SqlCommandIdentifier);
						}
						this.match(TSql80LexerInternal.tokenSet_4_);
					}
					else
					{
						if (this.cached_LA1 != '\n' && this.cached_LA1 != '\r')
						{
							break;
						}
						this.mEndOfLine(false);
					}
					num3++;
				}
				if (num3 < 1)
				{
					throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
				}
				this.mRightParenthesis(false);
				if (this.inputState.guessing == 0)
				{
					num = 226;
				}
			}
			else
			{
				bool flag2 = false;
				if (this.cached_LA1 == '$' && TSql80LexerInternal.tokenSet_5_.member((int)this.cached_LA2))
				{
					int num4 = this.mark();
					flag2 = true;
					this.inputState.guessing++;
					try
					{
						this.match('$');
						if (this.cached_LA1 == '@')
						{
							this.match('@');
						}
						else
						{
							if (!TSql80LexerInternal.tokenSet_6_.member((int)this.cached_LA1))
							{
								throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
							}
							this.mFirstLetter(false);
						}
					}
					catch (RecognitionException)
					{
						flag2 = false;
					}
					this.rewind(num4);
					this.inputState.guessing--;
				}
				if (flag2)
				{
					this.match('$');
					if (this.cached_LA1 == '@')
					{
						this.match('@');
					}
					else
					{
						if (!TSql80LexerInternal.tokenSet_6_.member((int)this.cached_LA1))
						{
							throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
						}
						this.mFirstLetter(false);
					}
					for (;;)
					{
						if (TSql80LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
						{
							this.mLetter(false);
						}
						else
						{
							if (this.cached_LA1 < '0' || this.cached_LA1 > '9')
							{
								break;
							}
							this.mDigit(false);
						}
					}
					if (this.inputState.guessing == 0)
					{
						if (string.Equals(this.text.ToString(), "$PARTITION", 5))
						{
							num = 228;
						}
						else
						{
							num = 227;
						}
					}
				}
				else
				{
					bool flag3 = false;
					if (TSql80LexerInternal.tokenSet_7_.member((int)this.cached_LA1) && TSql80LexerInternal.tokenSet_8_.member((int)this.cached_LA2))
					{
						int num5 = this.mark();
						flag3 = true;
						this.inputState.guessing++;
						try
						{
							this.mMoneySign(false);
						}
						catch (RecognitionException)
						{
							flag3 = false;
						}
						this.rewind(num5);
						this.inputState.guessing--;
					}
					if (flag3)
					{
						this.mMoneySign(false);
						while (this.cached_LA1 == ' ')
						{
							this.match(' ');
						}
						switch (this.cached_LA1)
						{
						case '+':
							this.mPlus(false);
							goto IL_03DE;
						case '-':
							this.mMinus(false);
							goto IL_03DE;
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_03DE;
						}
						throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
						IL_03DE:
						int num6 = 0;
						while (this.cached_LA1 >= '0' && this.cached_LA1 <= '9')
						{
							this.mDigit(false);
							num6++;
						}
						if (num6 < 1)
						{
							throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
						}
						char cached_LA = this.cached_LA1;
						if (cached_LA != '.')
						{
							if (cached_LA == 'e')
							{
								this.mExponent(false);
							}
						}
						else
						{
							this.match('.');
							while (this.cached_LA1 >= '0' && this.cached_LA1 <= '9')
							{
								this.mDigit(false);
							}
							if (this.cached_LA1 == 'e')
							{
								this.mExponent(false);
							}
						}
						if (this.inputState.guessing == 0)
						{
							num = 225;
						}
					}
					else
					{
						if (!TSql80LexerInternal.tokenSet_6_.member((int)this.cached_LA1))
						{
							throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
						}
						this.mFirstLetter(false);
						for (;;)
						{
							if (TSql80LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
							{
								this.mLetter(false);
							}
							else
							{
								if (this.cached_LA1 < '0' || this.cached_LA1 > '9')
								{
									break;
								}
								this.mDigit(false);
							}
						}
						bool flag4 = false;
						if (this.cached_LA1 == ':')
						{
							int num7 = this.mark();
							flag4 = true;
							this.inputState.guessing++;
							try
							{
								this.mColon(false);
								this.matchNot(':');
							}
							catch (RecognitionException)
							{
								flag4 = false;
							}
							this.rewind(num7);
							this.inputState.guessing--;
						}
						if (flag4)
						{
							this.mColon(false);
							if (this.inputState.guessing == 0)
							{
								num = 220;
							}
						}
					}
				}
			}
			num = this.testLiteralsTable(num);
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A7AC File Offset: 0x000089AC
		public void mQuotedIdentifier(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 233;
			char cached_LA = this.cached_LA1;
			if (cached_LA != '"')
			{
				if (cached_LA != '[')
				{
					throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
				}
				if (this.inputState.guessing == 0)
				{
					base.beginComplexToken();
				}
				this.match('[');
				int num2 = 0;
				for (;;)
				{
					if (this.cached_LA1 == ']' && this.cached_LA2 == ']')
					{
						this.match(']');
						this.match(']');
					}
					else if (TSql80LexerInternal.tokenSet_9_.member((int)this.cached_LA1))
					{
						if (this.inputState.guessing == 0)
						{
							base.checkEOF(TSqlLexerBaseInternal.TokenKind.QuotedIdentifier);
						}
						this.match(TSql80LexerInternal.tokenSet_9_);
					}
					else
					{
						if (this.cached_LA1 != '\n' && this.cached_LA1 != '\r')
						{
							break;
						}
						this.mEndOfLine(false);
					}
					num2++;
				}
				if (num2 < 1)
				{
					throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
				}
				this.match(']');
			}
			else
			{
				if (this.inputState.guessing == 0)
				{
					base.beginComplexToken();
				}
				this.match('"');
				for (;;)
				{
					if (this.cached_LA1 == '"' && this.cached_LA2 == '"')
					{
						this.match('"');
						this.match('"');
					}
					else if (TSql80LexerInternal.tokenSet_10_.member((int)this.cached_LA1))
					{
						if (this.inputState.guessing == 0)
						{
							base.checkEOF(TSqlLexerBaseInternal.TokenKind.QuotedIdentifier);
						}
						this.match(TSql80LexerInternal.tokenSet_10_);
					}
					else
					{
						if (this.cached_LA1 != '\n' && this.cached_LA1 != '\r')
						{
							break;
						}
						this.mEndOfLine(false);
					}
				}
				if (this.inputState.guessing == 0)
				{
					if (this.text.Length > 1)
					{
						num = 229;
					}
					else
					{
						num = 230;
					}
				}
				this.match('"');
			}
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public void mVariable(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 234;
			this.match('@');
			for (;;)
			{
				if (TSql80LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
				{
					this.mLetter(false);
				}
				else
				{
					if (this.cached_LA1 < '0' || this.cached_LA1 > '9')
					{
						break;
					}
					this.mDigit(false);
				}
			}
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000AA70 File Offset: 0x00008C70
		protected void mOdbcInitiator(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 235;
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public void mSingleLineComment(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 237;
			this.match("--");
			bool flag = false;
			if (this.cached_LA1 == '(' && this.cached_LA2 == '*')
			{
				int num2 = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match('(');
					this.match('*');
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num2);
				this.inputState.guessing--;
			}
			if (flag)
			{
				this.match('(');
				this.match('*');
				if (this.inputState.guessing == 0)
				{
					num = 235;
				}
			}
			else
			{
				while (TSql80LexerInternal.tokenSet_11_.member((int)this.cached_LA1) && this.LA(1) != CharScanner.EOF_CHAR)
				{
					this.match(TSql80LexerInternal.tokenSet_11_);
				}
				if (this.inputState.guessing == 0)
				{
					this._acceptableGoOffset = base.CurrentOffset;
				}
			}
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AC1C File Offset: 0x00008E1C
		public void mMultilineComment(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 238;
			bool flag = base.CurrentOffset == this._acceptableGoOffset;
			if (this.inputState.guessing == 0)
			{
				base.beginComplexToken();
			}
			this.match("/*");
			for (;;)
			{
				if (this.cached_LA1 == '*' && this.cached_LA2 >= '\0' && this.cached_LA2 <= '\uffff' && this.LA(2) != '/')
				{
					this.match('*');
				}
				else if (this.cached_LA1 == '/' && this.cached_LA2 >= '\0' && this.cached_LA2 <= '\uffff' && this.LA(2) != '*')
				{
					this.match('/');
				}
				else if (this.cached_LA1 == '/' && this.cached_LA2 == '*')
				{
					this.mMultilineComment(false);
				}
				else if (this.cached_LA1 == '\n' || this.cached_LA1 == '\r')
				{
					this.mEndOfLine(false);
				}
				else
				{
					if (!TSql80LexerInternal.tokenSet_12_.member((int)this.cached_LA1))
					{
						break;
					}
					if (this.inputState.guessing == 0)
					{
						base.checkEOF(TSqlLexerBaseInternal.TokenKind.MultiLineComment);
					}
					this.match(TSql80LexerInternal.tokenSet_12_);
				}
			}
			this.match("*/");
			if (this.inputState.guessing == 0 && flag)
			{
				this._acceptableGoOffset = base.CurrentOffset;
			}
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		private static long[] mk_tokenSet_0_()
		{
			long[] array = new long[3072];
			array[0] = 103079215104L;
			array[1] = 576460745860972544L;
			for (int i = 2; i <= 1022; i++)
			{
				array[i] = -1L;
			}
			array[1023] = long.MaxValue;
			for (int j = 1024; j <= 3071; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000AE20 File Offset: 0x00009020
		private static long[] mk_tokenSet_1_()
		{
			long[] array = new long[1025];
			array[0] = 8589925374L;
			for (int i = 1; i <= 1024; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000AE5C File Offset: 0x0000905C
		private static long[] mk_tokenSet_2_()
		{
			long[] array = new long[3072];
			array[0] = 103079215104L;
			array[1] = 576460745860972545L;
			for (int i = 2; i <= 1022; i++)
			{
				array[i] = -1L;
			}
			array[1023] = long.MaxValue;
			for (int j = 1024; j <= 3071; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AECC File Offset: 0x000090CC
		private static long[] mk_tokenSet_3_()
		{
			long[] array = new long[2048];
			array[0] = -549755823105L;
			for (int i = 1; i <= 1023; i++)
			{
				array[i] = -1L;
			}
			for (int j = 1024; j <= 2047; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000AF20 File Offset: 0x00009120
		private static long[] mk_tokenSet_4_()
		{
			long[] array = new long[2048];
			array[0] = -2199023264769L;
			for (int i = 1; i <= 1023; i++)
			{
				array[i] = -1L;
			}
			for (int j = 1024; j <= 2047; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AF74 File Offset: 0x00009174
		private static long[] mk_tokenSet_5_()
		{
			long[] array = new long[3072];
			array[0] = 34359738368L;
			array[1] = 576460745860972545L;
			for (int i = 2; i <= 1022; i++)
			{
				array[i] = -1L;
			}
			array[1023] = long.MaxValue;
			for (int j = 1024; j <= 3071; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000AFE4 File Offset: 0x000091E4
		private static long[] mk_tokenSet_6_()
		{
			long[] array = new long[3072];
			array[0] = 34359738368L;
			array[1] = 576460745860972544L;
			for (int i = 2; i <= 1022; i++)
			{
				array[i] = -1L;
			}
			array[1023] = long.MaxValue;
			for (int j = 1024; j <= 3071; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B054 File Offset: 0x00009254
		private static long[] mk_tokenSet_7_()
		{
			long[] array = new long[1025];
			array[0] = 68719476736L;
			array[1] = 0L;
			array[2] = 240518168576L;
			for (int i = 3; i <= 38; i++)
			{
				array[i] = 0L;
			}
			array[39] = 3377699720527872L;
			for (int j = 40; j <= 55; j++)
			{
				array[j] = 0L;
			}
			array[56] = long.MinValue;
			for (int k = 57; k <= 129; k++)
			{
				array[k] = 0L;
			}
			array[130] = 35038343200768L;
			for (int l = 131; l <= 1024; l++)
			{
				array[l] = 0L;
			}
			return array;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B110 File Offset: 0x00009310
		private static long[] mk_tokenSet_8_()
		{
			long[] array = new long[1025];
			array[0] = 287992885935079424L;
			for (int i = 1; i <= 1024; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B14C File Offset: 0x0000934C
		private static long[] mk_tokenSet_9_()
		{
			long[] array = new long[2048];
			array[0] = -9217L;
			array[1] = -536870913L;
			for (int i = 2; i <= 1023; i++)
			{
				array[i] = -1L;
			}
			for (int j = 1024; j <= 2047; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B1A8 File Offset: 0x000093A8
		private static long[] mk_tokenSet_10_()
		{
			long[] array = new long[2048];
			array[0] = -17179878401L;
			for (int i = 1; i <= 1023; i++)
			{
				array[i] = -1L;
			}
			for (int j = 1024; j <= 2047; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B1FC File Offset: 0x000093FC
		private static long[] mk_tokenSet_11_()
		{
			long[] array = new long[2048];
			array[0] = -9217L;
			for (int i = 1; i <= 1023; i++)
			{
				array[i] = -1L;
			}
			for (int j = 1024; j <= 2047; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B24C File Offset: 0x0000944C
		private static long[] mk_tokenSet_12_()
		{
			long[] array = new long[2048];
			array[0] = -145135534875649L;
			for (int i = 1; i <= 1023; i++)
			{
				array[i] = -1L;
			}
			for (int j = 1024; j <= 2047; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x04000195 RID: 405
		public const int EOF = 1;

		// Token: 0x04000196 RID: 406
		public const int NULL_TREE_LOOKAHEAD = 3;

		// Token: 0x04000197 RID: 407
		public const int Add = 4;

		// Token: 0x04000198 RID: 408
		public const int All = 5;

		// Token: 0x04000199 RID: 409
		public const int Alter = 6;

		// Token: 0x0400019A RID: 410
		public const int And = 7;

		// Token: 0x0400019B RID: 411
		public const int Any = 8;

		// Token: 0x0400019C RID: 412
		public const int As = 9;

		// Token: 0x0400019D RID: 413
		public const int Asc = 10;

		// Token: 0x0400019E RID: 414
		public const int Authorization = 11;

		// Token: 0x0400019F RID: 415
		public const int Backup = 12;

		// Token: 0x040001A0 RID: 416
		public const int Begin = 13;

		// Token: 0x040001A1 RID: 417
		public const int Between = 14;

		// Token: 0x040001A2 RID: 418
		public const int Break = 15;

		// Token: 0x040001A3 RID: 419
		public const int Browse = 16;

		// Token: 0x040001A4 RID: 420
		public const int Bulk = 17;

		// Token: 0x040001A5 RID: 421
		public const int By = 18;

		// Token: 0x040001A6 RID: 422
		public const int Cascade = 19;

		// Token: 0x040001A7 RID: 423
		public const int Case = 20;

		// Token: 0x040001A8 RID: 424
		public const int Check = 21;

		// Token: 0x040001A9 RID: 425
		public const int Checkpoint = 22;

		// Token: 0x040001AA RID: 426
		public const int Close = 23;

		// Token: 0x040001AB RID: 427
		public const int Clustered = 24;

		// Token: 0x040001AC RID: 428
		public const int Coalesce = 25;

		// Token: 0x040001AD RID: 429
		public const int Collate = 26;

		// Token: 0x040001AE RID: 430
		public const int Column = 27;

		// Token: 0x040001AF RID: 431
		public const int Commit = 28;

		// Token: 0x040001B0 RID: 432
		public const int Compute = 29;

		// Token: 0x040001B1 RID: 433
		public const int Constraint = 30;

		// Token: 0x040001B2 RID: 434
		public const int Contains = 31;

		// Token: 0x040001B3 RID: 435
		public const int ContainsTable = 32;

		// Token: 0x040001B4 RID: 436
		public const int Continue = 33;

		// Token: 0x040001B5 RID: 437
		public const int Convert = 34;

		// Token: 0x040001B6 RID: 438
		public const int Create = 35;

		// Token: 0x040001B7 RID: 439
		public const int Cross = 36;

		// Token: 0x040001B8 RID: 440
		public const int Current = 37;

		// Token: 0x040001B9 RID: 441
		public const int CurrentDate = 38;

		// Token: 0x040001BA RID: 442
		public const int CurrentTime = 39;

		// Token: 0x040001BB RID: 443
		public const int CurrentTimestamp = 40;

		// Token: 0x040001BC RID: 444
		public const int CurrentUser = 41;

		// Token: 0x040001BD RID: 445
		public const int Cursor = 42;

		// Token: 0x040001BE RID: 446
		public const int Database = 43;

		// Token: 0x040001BF RID: 447
		public const int Dbcc = 44;

		// Token: 0x040001C0 RID: 448
		public const int Deallocate = 45;

		// Token: 0x040001C1 RID: 449
		public const int Declare = 46;

		// Token: 0x040001C2 RID: 450
		public const int Default = 47;

		// Token: 0x040001C3 RID: 451
		public const int Delete = 48;

		// Token: 0x040001C4 RID: 452
		public const int Deny = 49;

		// Token: 0x040001C5 RID: 453
		public const int Desc = 50;

		// Token: 0x040001C6 RID: 454
		public const int Distinct = 51;

		// Token: 0x040001C7 RID: 455
		public const int Distributed = 52;

		// Token: 0x040001C8 RID: 456
		public const int Double = 53;

		// Token: 0x040001C9 RID: 457
		public const int Drop = 54;

		// Token: 0x040001CA RID: 458
		public const int Else = 55;

		// Token: 0x040001CB RID: 459
		public const int End = 56;

		// Token: 0x040001CC RID: 460
		public const int Errlvl = 57;

		// Token: 0x040001CD RID: 461
		public const int Escape = 58;

		// Token: 0x040001CE RID: 462
		public const int Except = 59;

		// Token: 0x040001CF RID: 463
		public const int Exec = 60;

		// Token: 0x040001D0 RID: 464
		public const int Execute = 61;

		// Token: 0x040001D1 RID: 465
		public const int Exists = 62;

		// Token: 0x040001D2 RID: 466
		public const int Exit = 63;

		// Token: 0x040001D3 RID: 467
		public const int Fetch = 64;

		// Token: 0x040001D4 RID: 468
		public const int File = 65;

		// Token: 0x040001D5 RID: 469
		public const int FillFactor = 66;

		// Token: 0x040001D6 RID: 470
		public const int For = 67;

		// Token: 0x040001D7 RID: 471
		public const int Foreign = 68;

		// Token: 0x040001D8 RID: 472
		public const int FreeText = 69;

		// Token: 0x040001D9 RID: 473
		public const int FreeTextTable = 70;

		// Token: 0x040001DA RID: 474
		public const int From = 71;

		// Token: 0x040001DB RID: 475
		public const int Full = 72;

		// Token: 0x040001DC RID: 476
		public const int Function = 73;

		// Token: 0x040001DD RID: 477
		public const int GoTo = 74;

		// Token: 0x040001DE RID: 478
		public const int Grant = 75;

		// Token: 0x040001DF RID: 479
		public const int Group = 76;

		// Token: 0x040001E0 RID: 480
		public const int Having = 77;

		// Token: 0x040001E1 RID: 481
		public const int HoldLock = 78;

		// Token: 0x040001E2 RID: 482
		public const int Identity = 79;

		// Token: 0x040001E3 RID: 483
		public const int IdentityInsert = 80;

		// Token: 0x040001E4 RID: 484
		public const int IdentityColumn = 81;

		// Token: 0x040001E5 RID: 485
		public const int If = 82;

		// Token: 0x040001E6 RID: 486
		public const int In = 83;

		// Token: 0x040001E7 RID: 487
		public const int Index = 84;

		// Token: 0x040001E8 RID: 488
		public const int Inner = 85;

		// Token: 0x040001E9 RID: 489
		public const int Insert = 86;

		// Token: 0x040001EA RID: 490
		public const int Intersect = 87;

		// Token: 0x040001EB RID: 491
		public const int Into = 88;

		// Token: 0x040001EC RID: 492
		public const int Is = 89;

		// Token: 0x040001ED RID: 493
		public const int Join = 90;

		// Token: 0x040001EE RID: 494
		public const int Key = 91;

		// Token: 0x040001EF RID: 495
		public const int Kill = 92;

		// Token: 0x040001F0 RID: 496
		public const int Left = 93;

		// Token: 0x040001F1 RID: 497
		public const int Like = 94;

		// Token: 0x040001F2 RID: 498
		public const int LineNo = 95;

		// Token: 0x040001F3 RID: 499
		public const int National = 96;

		// Token: 0x040001F4 RID: 500
		public const int NoCheck = 97;

		// Token: 0x040001F5 RID: 501
		public const int NonClustered = 98;

		// Token: 0x040001F6 RID: 502
		public const int Not = 99;

		// Token: 0x040001F7 RID: 503
		public const int Null = 100;

		// Token: 0x040001F8 RID: 504
		public const int NullIf = 101;

		// Token: 0x040001F9 RID: 505
		public const int Of = 102;

		// Token: 0x040001FA RID: 506
		public const int Off = 103;

		// Token: 0x040001FB RID: 507
		public const int Offsets = 104;

		// Token: 0x040001FC RID: 508
		public const int On = 105;

		// Token: 0x040001FD RID: 509
		public const int Open = 106;

		// Token: 0x040001FE RID: 510
		public const int OpenDataSource = 107;

		// Token: 0x040001FF RID: 511
		public const int OpenQuery = 108;

		// Token: 0x04000200 RID: 512
		public const int OpenRowSet = 109;

		// Token: 0x04000201 RID: 513
		public const int OpenXml = 110;

		// Token: 0x04000202 RID: 514
		public const int Option = 111;

		// Token: 0x04000203 RID: 515
		public const int Or = 112;

		// Token: 0x04000204 RID: 516
		public const int Order = 113;

		// Token: 0x04000205 RID: 517
		public const int Outer = 114;

		// Token: 0x04000206 RID: 518
		public const int Over = 115;

		// Token: 0x04000207 RID: 519
		public const int Percent = 116;

		// Token: 0x04000208 RID: 520
		public const int Plan = 117;

		// Token: 0x04000209 RID: 521
		public const int Primary = 118;

		// Token: 0x0400020A RID: 522
		public const int Print = 119;

		// Token: 0x0400020B RID: 523
		public const int Proc = 120;

		// Token: 0x0400020C RID: 524
		public const int Procedure = 121;

		// Token: 0x0400020D RID: 525
		public const int Public = 122;

		// Token: 0x0400020E RID: 526
		public const int Raiserror = 123;

		// Token: 0x0400020F RID: 527
		public const int Read = 124;

		// Token: 0x04000210 RID: 528
		public const int ReadText = 125;

		// Token: 0x04000211 RID: 529
		public const int Reconfigure = 126;

		// Token: 0x04000212 RID: 530
		public const int References = 127;

		// Token: 0x04000213 RID: 531
		public const int Replication = 128;

		// Token: 0x04000214 RID: 532
		public const int Restore = 129;

		// Token: 0x04000215 RID: 533
		public const int Restrict = 130;

		// Token: 0x04000216 RID: 534
		public const int Return = 131;

		// Token: 0x04000217 RID: 535
		public const int Revoke = 132;

		// Token: 0x04000218 RID: 536
		public const int Right = 133;

		// Token: 0x04000219 RID: 537
		public const int Rollback = 134;

		// Token: 0x0400021A RID: 538
		public const int RowCount = 135;

		// Token: 0x0400021B RID: 539
		public const int RowGuidColumn = 136;

		// Token: 0x0400021C RID: 540
		public const int Rule = 137;

		// Token: 0x0400021D RID: 541
		public const int Save = 138;

		// Token: 0x0400021E RID: 542
		public const int Schema = 139;

		// Token: 0x0400021F RID: 543
		public const int Select = 140;

		// Token: 0x04000220 RID: 544
		public const int SessionUser = 141;

		// Token: 0x04000221 RID: 545
		public const int Set = 142;

		// Token: 0x04000222 RID: 546
		public const int SetUser = 143;

		// Token: 0x04000223 RID: 547
		public const int Shutdown = 144;

		// Token: 0x04000224 RID: 548
		public const int Some = 145;

		// Token: 0x04000225 RID: 549
		public const int Statistics = 146;

		// Token: 0x04000226 RID: 550
		public const int SystemUser = 147;

		// Token: 0x04000227 RID: 551
		public const int Table = 148;

		// Token: 0x04000228 RID: 552
		public const int TextSize = 149;

		// Token: 0x04000229 RID: 553
		public const int Then = 150;

		// Token: 0x0400022A RID: 554
		public const int To = 151;

		// Token: 0x0400022B RID: 555
		public const int Top = 152;

		// Token: 0x0400022C RID: 556
		public const int Tran = 153;

		// Token: 0x0400022D RID: 557
		public const int Transaction = 154;

		// Token: 0x0400022E RID: 558
		public const int Trigger = 155;

		// Token: 0x0400022F RID: 559
		public const int Truncate = 156;

		// Token: 0x04000230 RID: 560
		public const int TSEqual = 157;

		// Token: 0x04000231 RID: 561
		public const int Union = 158;

		// Token: 0x04000232 RID: 562
		public const int Unique = 159;

		// Token: 0x04000233 RID: 563
		public const int Update = 160;

		// Token: 0x04000234 RID: 564
		public const int UpdateText = 161;

		// Token: 0x04000235 RID: 565
		public const int Use = 162;

		// Token: 0x04000236 RID: 566
		public const int User = 163;

		// Token: 0x04000237 RID: 567
		public const int Values = 164;

		// Token: 0x04000238 RID: 568
		public const int Varying = 165;

		// Token: 0x04000239 RID: 569
		public const int View = 166;

		// Token: 0x0400023A RID: 570
		public const int WaitFor = 167;

		// Token: 0x0400023B RID: 571
		public const int When = 168;

		// Token: 0x0400023C RID: 572
		public const int Where = 169;

		// Token: 0x0400023D RID: 573
		public const int While = 170;

		// Token: 0x0400023E RID: 574
		public const int With = 171;

		// Token: 0x0400023F RID: 575
		public const int WriteText = 172;

		// Token: 0x04000240 RID: 576
		public const int Disk = 173;

		// Token: 0x04000241 RID: 577
		public const int Precision = 174;

		// Token: 0x04000242 RID: 578
		public const int External = 175;

		// Token: 0x04000243 RID: 579
		public const int Revert = 176;

		// Token: 0x04000244 RID: 580
		public const int Pivot = 177;

		// Token: 0x04000245 RID: 581
		public const int Unpivot = 178;

		// Token: 0x04000246 RID: 582
		public const int TableSample = 179;

		// Token: 0x04000247 RID: 583
		public const int Dump = 180;

		// Token: 0x04000248 RID: 584
		public const int Load = 181;

		// Token: 0x04000249 RID: 585
		public const int Merge = 182;

		// Token: 0x0400024A RID: 586
		public const int StopList = 183;

		// Token: 0x0400024B RID: 587
		public const int SemanticKeyPhraseTable = 184;

		// Token: 0x0400024C RID: 588
		public const int SemanticSimilarityTable = 185;

		// Token: 0x0400024D RID: 589
		public const int SemanticSimilarityDetailsTable = 186;

		// Token: 0x0400024E RID: 590
		public const int TryConvert = 187;

		// Token: 0x0400024F RID: 591
		public const int Bang = 188;

		// Token: 0x04000250 RID: 592
		public const int PercentSign = 189;

		// Token: 0x04000251 RID: 593
		public const int Ampersand = 190;

		// Token: 0x04000252 RID: 594
		public const int LeftParenthesis = 191;

		// Token: 0x04000253 RID: 595
		public const int RightParenthesis = 192;

		// Token: 0x04000254 RID: 596
		public const int LeftCurly = 193;

		// Token: 0x04000255 RID: 597
		public const int RightCurly = 194;

		// Token: 0x04000256 RID: 598
		public const int Star = 195;

		// Token: 0x04000257 RID: 599
		public const int MultiplyEquals = 196;

		// Token: 0x04000258 RID: 600
		public const int Plus = 197;

		// Token: 0x04000259 RID: 601
		public const int Comma = 198;

		// Token: 0x0400025A RID: 602
		public const int Minus = 199;

		// Token: 0x0400025B RID: 603
		public const int Dot = 200;

		// Token: 0x0400025C RID: 604
		public const int Divide = 201;

		// Token: 0x0400025D RID: 605
		public const int Colon = 202;

		// Token: 0x0400025E RID: 606
		public const int DoubleColon = 203;

		// Token: 0x0400025F RID: 607
		public const int Semicolon = 204;

		// Token: 0x04000260 RID: 608
		public const int LessThan = 205;

		// Token: 0x04000261 RID: 609
		public const int EqualsSign = 206;

		// Token: 0x04000262 RID: 610
		public const int RightOuterJoin = 207;

		// Token: 0x04000263 RID: 611
		public const int GreaterThan = 208;

		// Token: 0x04000264 RID: 612
		public const int Circumflex = 209;

		// Token: 0x04000265 RID: 613
		public const int VerticalLine = 210;

		// Token: 0x04000266 RID: 614
		public const int Tilde = 211;

		// Token: 0x04000267 RID: 615
		public const int AddEquals = 212;

		// Token: 0x04000268 RID: 616
		public const int SubtractEquals = 213;

		// Token: 0x04000269 RID: 617
		public const int DivideEquals = 214;

		// Token: 0x0400026A RID: 618
		public const int ModEquals = 215;

		// Token: 0x0400026B RID: 619
		public const int BitwiseAndEquals = 216;

		// Token: 0x0400026C RID: 620
		public const int BitwiseOrEquals = 217;

		// Token: 0x0400026D RID: 621
		public const int BitwiseXorEquals = 218;

		// Token: 0x0400026E RID: 622
		public const int Go = 219;

		// Token: 0x0400026F RID: 623
		public const int Label = 220;

		// Token: 0x04000270 RID: 624
		public const int Integer = 221;

		// Token: 0x04000271 RID: 625
		public const int Numeric = 222;

		// Token: 0x04000272 RID: 626
		public const int Real = 223;

		// Token: 0x04000273 RID: 627
		public const int HexLiteral = 224;

		// Token: 0x04000274 RID: 628
		public const int Money = 225;

		// Token: 0x04000275 RID: 629
		public const int SqlCommandIdentifier = 226;

		// Token: 0x04000276 RID: 630
		public const int PseudoColumn = 227;

		// Token: 0x04000277 RID: 631
		public const int DollarPartition = 228;

		// Token: 0x04000278 RID: 632
		public const int AsciiStringOrQuotedIdentifier = 229;

		// Token: 0x04000279 RID: 633
		public const int AsciiStringLiteral = 230;

		// Token: 0x0400027A RID: 634
		public const int UnicodeStringLiteral = 231;

		// Token: 0x0400027B RID: 635
		public const int Identifier = 232;

		// Token: 0x0400027C RID: 636
		public const int QuotedIdentifier = 233;

		// Token: 0x0400027D RID: 637
		public const int Variable = 234;

		// Token: 0x0400027E RID: 638
		public const int OdbcInitiator = 235;

		// Token: 0x0400027F RID: 639
		public const int ProcNameSemicolon = 236;

		// Token: 0x04000280 RID: 640
		public const int SingleLineComment = 237;

		// Token: 0x04000281 RID: 641
		public const int MultilineComment = 238;

		// Token: 0x04000282 RID: 642
		public const int WhiteSpace = 239;

		// Token: 0x04000283 RID: 643
		public const int Digit = 240;

		// Token: 0x04000284 RID: 644
		public const int FirstLetter = 241;

		// Token: 0x04000285 RID: 645
		public const int Letter = 242;

		// Token: 0x04000286 RID: 646
		public const int MoneySign = 243;

		// Token: 0x04000287 RID: 647
		public const int WS_CHAR_WO_NEWLINE = 244;

		// Token: 0x04000288 RID: 648
		public const int Number = 245;

		// Token: 0x04000289 RID: 649
		public const int Exponent = 246;

		// Token: 0x0400028A RID: 650
		public const int EndOfLine = 247;

		// Token: 0x0400028B RID: 651
		public static readonly BitSet tokenSet_0_ = new BitSet(TSql80LexerInternal.mk_tokenSet_0_());

		// Token: 0x0400028C RID: 652
		public static readonly BitSet tokenSet_1_ = new BitSet(TSql80LexerInternal.mk_tokenSet_1_());

		// Token: 0x0400028D RID: 653
		public static readonly BitSet tokenSet_2_ = new BitSet(TSql80LexerInternal.mk_tokenSet_2_());

		// Token: 0x0400028E RID: 654
		public static readonly BitSet tokenSet_3_ = new BitSet(TSql80LexerInternal.mk_tokenSet_3_());

		// Token: 0x0400028F RID: 655
		public static readonly BitSet tokenSet_4_ = new BitSet(TSql80LexerInternal.mk_tokenSet_4_());

		// Token: 0x04000290 RID: 656
		public static readonly BitSet tokenSet_5_ = new BitSet(TSql80LexerInternal.mk_tokenSet_5_());

		// Token: 0x04000291 RID: 657
		public static readonly BitSet tokenSet_6_ = new BitSet(TSql80LexerInternal.mk_tokenSet_6_());

		// Token: 0x04000292 RID: 658
		public static readonly BitSet tokenSet_7_ = new BitSet(TSql80LexerInternal.mk_tokenSet_7_());

		// Token: 0x04000293 RID: 659
		public static readonly BitSet tokenSet_8_ = new BitSet(TSql80LexerInternal.mk_tokenSet_8_());

		// Token: 0x04000294 RID: 660
		public static readonly BitSet tokenSet_9_ = new BitSet(TSql80LexerInternal.mk_tokenSet_9_());

		// Token: 0x04000295 RID: 661
		public static readonly BitSet tokenSet_10_ = new BitSet(TSql80LexerInternal.mk_tokenSet_10_());

		// Token: 0x04000296 RID: 662
		public static readonly BitSet tokenSet_11_ = new BitSet(TSql80LexerInternal.mk_tokenSet_11_());

		// Token: 0x04000297 RID: 663
		public static readonly BitSet tokenSet_12_ = new BitSet(TSql80LexerInternal.mk_tokenSet_12_());
	}
}
