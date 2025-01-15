using System;
using System.Collections;
using System.IO;
using antlr;
using antlr.collections.impl;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000150 RID: 336
	internal class TSql90LexerInternal : TSqlLexerBaseInternal, TokenStream
	{
		// Token: 0x060014E0 RID: 5344 RVA: 0x0009143B File Offset: 0x0008F63B
		public TSql90LexerInternal()
			: this(new StringReader(string.Empty))
		{
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0009144D File Offset: 0x0008F64D
		public TSql90LexerInternal(Stream ins)
			: this(new ByteBuffer(ins))
		{
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0009145B File Offset: 0x0008F65B
		public TSql90LexerInternal(TextReader r)
			: this(new CharBuffer(r))
		{
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00091469 File Offset: 0x0008F669
		public TSql90LexerInternal(InputBuffer ib)
			: this(new LexerSharedInputState(ib))
		{
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00091477 File Offset: 0x0008F677
		public TSql90LexerInternal(LexerSharedInputState state)
			: base(state)
		{
			this.initialize();
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00091488 File Offset: 0x0008F688
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
			this.literals.Add("pivot", 177);
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
			this.literals.Add("external", 175);
			this.literals.Add("execute", 61);
			this.literals.Add("reconfigure", 126);
			this.literals.Add("transaction", 154);
			this.literals.Add("proc", 120);
			this.literals.Add("tablesample", 179);
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
			this.literals.Add("unpivot", 178);
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
			this.literals.Add("revert", 176);
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

		// Token: 0x060014E6 RID: 5350 RVA: 0x00092524 File Offset: 0x00090724
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
					else if (TSql90LexerInternal.tokenSet_0_.member((int)this.cached_LA1))
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

		// Token: 0x060014E7 RID: 5351 RVA: 0x00092ABC File Offset: 0x00090CBC
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

		// Token: 0x060014E8 RID: 5352 RVA: 0x00092B24 File Offset: 0x00090D24
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

		// Token: 0x060014E9 RID: 5353 RVA: 0x00092B8C File Offset: 0x00090D8C
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

		// Token: 0x060014EA RID: 5354 RVA: 0x00092BF4 File Offset: 0x00090DF4
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

		// Token: 0x060014EB RID: 5355 RVA: 0x00092C5C File Offset: 0x00090E5C
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

		// Token: 0x060014EC RID: 5356 RVA: 0x00092CC4 File Offset: 0x00090EC4
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

		// Token: 0x060014ED RID: 5357 RVA: 0x00092D2C File Offset: 0x00090F2C
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

		// Token: 0x060014EE RID: 5358 RVA: 0x00092D94 File Offset: 0x00090F94
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

		// Token: 0x060014EF RID: 5359 RVA: 0x00092DFC File Offset: 0x00090FFC
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

		// Token: 0x060014F0 RID: 5360 RVA: 0x00092E64 File Offset: 0x00091064
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

		// Token: 0x060014F1 RID: 5361 RVA: 0x00092ECC File Offset: 0x000910CC
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

		// Token: 0x060014F2 RID: 5362 RVA: 0x00092F34 File Offset: 0x00091134
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

		// Token: 0x060014F3 RID: 5363 RVA: 0x00092F9C File Offset: 0x0009119C
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

		// Token: 0x060014F4 RID: 5364 RVA: 0x00092FFC File Offset: 0x000911FC
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

		// Token: 0x060014F5 RID: 5365 RVA: 0x00093064 File Offset: 0x00091264
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

		// Token: 0x060014F6 RID: 5366 RVA: 0x000930CC File Offset: 0x000912CC
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

		// Token: 0x060014F7 RID: 5367 RVA: 0x00093134 File Offset: 0x00091334
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

		// Token: 0x060014F8 RID: 5368 RVA: 0x0009319C File Offset: 0x0009139C
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

		// Token: 0x060014F9 RID: 5369 RVA: 0x00093204 File Offset: 0x00091404
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

		// Token: 0x060014FA RID: 5370 RVA: 0x0009326C File Offset: 0x0009146C
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

		// Token: 0x060014FB RID: 5371 RVA: 0x000932D4 File Offset: 0x000914D4
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

		// Token: 0x060014FC RID: 5372 RVA: 0x0009333C File Offset: 0x0009153C
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

		// Token: 0x060014FD RID: 5373 RVA: 0x000933A4 File Offset: 0x000915A4
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

		// Token: 0x060014FE RID: 5374 RVA: 0x0009340C File Offset: 0x0009160C
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

		// Token: 0x060014FF RID: 5375 RVA: 0x00093474 File Offset: 0x00091674
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

		// Token: 0x06001500 RID: 5376 RVA: 0x000934DC File Offset: 0x000916DC
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

		// Token: 0x06001501 RID: 5377 RVA: 0x00093630 File Offset: 0x00091830
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

		// Token: 0x06001502 RID: 5378 RVA: 0x000937A8 File Offset: 0x000919A8
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

		// Token: 0x06001503 RID: 5379 RVA: 0x000939BC File Offset: 0x00091BBC
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
					while (TSql90LexerInternal.tokenSet_1_.member((int)this.cached_LA1))
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

		// Token: 0x06001504 RID: 5380 RVA: 0x00093AF0 File Offset: 0x00091CF0
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

		// Token: 0x06001505 RID: 5381 RVA: 0x00093D60 File Offset: 0x00091F60
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

		// Token: 0x06001506 RID: 5382 RVA: 0x00094080 File Offset: 0x00092280
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
				while (TSql90LexerInternal.tokenSet_1_.member((int)this.cached_LA1))
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

		// Token: 0x06001507 RID: 5383 RVA: 0x00094228 File Offset: 0x00092428
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

		// Token: 0x06001508 RID: 5384 RVA: 0x00094300 File Offset: 0x00092500
		public void mGo(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 219;
			if (base.CurrentOffset != this._acceptableGoOffset)
			{
				throw new SemanticException(" (CurrentOffset==_acceptableGoOffset) ");
			}
			this.match("go");
			for (;;)
			{
				if (TSql90LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
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

		// Token: 0x06001509 RID: 5385 RVA: 0x00094470 File Offset: 0x00092670
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

		// Token: 0x0600150A RID: 5386 RVA: 0x000944D0 File Offset: 0x000926D0
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

		// Token: 0x0600150B RID: 5387 RVA: 0x00094530 File Offset: 0x00092730
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

		// Token: 0x0600150C RID: 5388 RVA: 0x00094590 File Offset: 0x00092790
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

		// Token: 0x0600150D RID: 5389 RVA: 0x000945F0 File Offset: 0x000927F0
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

		// Token: 0x0600150E RID: 5390 RVA: 0x00094650 File Offset: 0x00092850
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

		// Token: 0x0600150F RID: 5391 RVA: 0x00094704 File Offset: 0x00092904
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

		// Token: 0x06001510 RID: 5392 RVA: 0x00094764 File Offset: 0x00092964
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

		// Token: 0x06001511 RID: 5393 RVA: 0x000947C4 File Offset: 0x000929C4
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

		// Token: 0x06001512 RID: 5394 RVA: 0x00094824 File Offset: 0x00092A24
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

		// Token: 0x06001513 RID: 5395 RVA: 0x00094884 File Offset: 0x00092A84
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

		// Token: 0x06001514 RID: 5396 RVA: 0x000948E4 File Offset: 0x00092AE4
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
				else if (TSql90LexerInternal.tokenSet_3_.member((int)this.cached_LA1))
				{
					if (this.inputState.guessing == 0)
					{
						base.checkEOF(TSqlLexerBaseInternal.TokenKind.String);
					}
					this.match(TSql90LexerInternal.tokenSet_3_);
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

		// Token: 0x06001515 RID: 5397 RVA: 0x000949DC File Offset: 0x00092BDC
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
				else if (TSql90LexerInternal.tokenSet_3_.member((int)this.cached_LA1))
				{
					if (this.inputState.guessing == 0)
					{
						base.checkEOF(TSqlLexerBaseInternal.TokenKind.String);
					}
					this.match(TSql90LexerInternal.tokenSet_3_);
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

		// Token: 0x06001516 RID: 5398 RVA: 0x00094ADC File Offset: 0x00092CDC
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
					if (TSql90LexerInternal.tokenSet_4_.member((int)this.cached_LA1))
					{
						if (this.inputState.guessing == 0)
						{
							base.checkEOF(TSqlLexerBaseInternal.TokenKind.SqlCommandIdentifier);
						}
						this.match(TSql90LexerInternal.tokenSet_4_);
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
				if (this.cached_LA1 == '$' && TSql90LexerInternal.tokenSet_5_.member((int)this.cached_LA2))
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
							if (!TSql90LexerInternal.tokenSet_6_.member((int)this.cached_LA1))
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
						if (!TSql90LexerInternal.tokenSet_6_.member((int)this.cached_LA1))
						{
							throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
						}
						this.mFirstLetter(false);
					}
					for (;;)
					{
						if (TSql90LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
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
					if (TSql90LexerInternal.tokenSet_7_.member((int)this.cached_LA1) && TSql90LexerInternal.tokenSet_8_.member((int)this.cached_LA2))
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
						if (!TSql90LexerInternal.tokenSet_6_.member((int)this.cached_LA1))
						{
							throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
						}
						this.mFirstLetter(false);
						for (;;)
						{
							if (TSql90LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
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

		// Token: 0x06001517 RID: 5399 RVA: 0x000950EC File Offset: 0x000932EC
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
					else if (TSql90LexerInternal.tokenSet_9_.member((int)this.cached_LA1))
					{
						if (this.inputState.guessing == 0)
						{
							base.checkEOF(TSqlLexerBaseInternal.TokenKind.QuotedIdentifier);
						}
						this.match(TSql90LexerInternal.tokenSet_9_);
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
					else if (TSql90LexerInternal.tokenSet_10_.member((int)this.cached_LA1))
					{
						if (this.inputState.guessing == 0)
						{
							base.checkEOF(TSqlLexerBaseInternal.TokenKind.QuotedIdentifier);
						}
						this.match(TSql90LexerInternal.tokenSet_10_);
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

		// Token: 0x06001518 RID: 5400 RVA: 0x00095310 File Offset: 0x00093510
		public void mVariable(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 234;
			this.match('@');
			for (;;)
			{
				if (TSql90LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
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

		// Token: 0x06001519 RID: 5401 RVA: 0x000953B0 File Offset: 0x000935B0
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

		// Token: 0x0600151A RID: 5402 RVA: 0x00095410 File Offset: 0x00093610
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
				while (TSql90LexerInternal.tokenSet_11_.member((int)this.cached_LA1) && this.LA(1) != CharScanner.EOF_CHAR)
				{
					this.match(TSql90LexerInternal.tokenSet_11_);
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

		// Token: 0x0600151B RID: 5403 RVA: 0x0009555C File Offset: 0x0009375C
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
					if (!TSql90LexerInternal.tokenSet_12_.member((int)this.cached_LA1))
					{
						break;
					}
					if (this.inputState.guessing == 0)
					{
						base.checkEOF(TSqlLexerBaseInternal.TokenKind.MultiLineComment);
					}
					this.match(TSql90LexerInternal.tokenSet_12_);
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

		// Token: 0x0600151C RID: 5404 RVA: 0x000956F0 File Offset: 0x000938F0
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

		// Token: 0x0600151D RID: 5405 RVA: 0x00095760 File Offset: 0x00093960
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

		// Token: 0x0600151E RID: 5406 RVA: 0x0009579C File Offset: 0x0009399C
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

		// Token: 0x0600151F RID: 5407 RVA: 0x0009580C File Offset: 0x00093A0C
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

		// Token: 0x06001520 RID: 5408 RVA: 0x00095860 File Offset: 0x00093A60
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

		// Token: 0x06001521 RID: 5409 RVA: 0x000958B4 File Offset: 0x00093AB4
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

		// Token: 0x06001522 RID: 5410 RVA: 0x00095924 File Offset: 0x00093B24
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

		// Token: 0x06001523 RID: 5411 RVA: 0x00095994 File Offset: 0x00093B94
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

		// Token: 0x06001524 RID: 5412 RVA: 0x00095A50 File Offset: 0x00093C50
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

		// Token: 0x06001525 RID: 5413 RVA: 0x00095A8C File Offset: 0x00093C8C
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

		// Token: 0x06001526 RID: 5414 RVA: 0x00095AE8 File Offset: 0x00093CE8
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

		// Token: 0x06001527 RID: 5415 RVA: 0x00095B3C File Offset: 0x00093D3C
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

		// Token: 0x06001528 RID: 5416 RVA: 0x00095B8C File Offset: 0x00093D8C
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

		// Token: 0x04001208 RID: 4616
		public const int EOF = 1;

		// Token: 0x04001209 RID: 4617
		public const int NULL_TREE_LOOKAHEAD = 3;

		// Token: 0x0400120A RID: 4618
		public const int Add = 4;

		// Token: 0x0400120B RID: 4619
		public const int All = 5;

		// Token: 0x0400120C RID: 4620
		public const int Alter = 6;

		// Token: 0x0400120D RID: 4621
		public const int And = 7;

		// Token: 0x0400120E RID: 4622
		public const int Any = 8;

		// Token: 0x0400120F RID: 4623
		public const int As = 9;

		// Token: 0x04001210 RID: 4624
		public const int Asc = 10;

		// Token: 0x04001211 RID: 4625
		public const int Authorization = 11;

		// Token: 0x04001212 RID: 4626
		public const int Backup = 12;

		// Token: 0x04001213 RID: 4627
		public const int Begin = 13;

		// Token: 0x04001214 RID: 4628
		public const int Between = 14;

		// Token: 0x04001215 RID: 4629
		public const int Break = 15;

		// Token: 0x04001216 RID: 4630
		public const int Browse = 16;

		// Token: 0x04001217 RID: 4631
		public const int Bulk = 17;

		// Token: 0x04001218 RID: 4632
		public const int By = 18;

		// Token: 0x04001219 RID: 4633
		public const int Cascade = 19;

		// Token: 0x0400121A RID: 4634
		public const int Case = 20;

		// Token: 0x0400121B RID: 4635
		public const int Check = 21;

		// Token: 0x0400121C RID: 4636
		public const int Checkpoint = 22;

		// Token: 0x0400121D RID: 4637
		public const int Close = 23;

		// Token: 0x0400121E RID: 4638
		public const int Clustered = 24;

		// Token: 0x0400121F RID: 4639
		public const int Coalesce = 25;

		// Token: 0x04001220 RID: 4640
		public const int Collate = 26;

		// Token: 0x04001221 RID: 4641
		public const int Column = 27;

		// Token: 0x04001222 RID: 4642
		public const int Commit = 28;

		// Token: 0x04001223 RID: 4643
		public const int Compute = 29;

		// Token: 0x04001224 RID: 4644
		public const int Constraint = 30;

		// Token: 0x04001225 RID: 4645
		public const int Contains = 31;

		// Token: 0x04001226 RID: 4646
		public const int ContainsTable = 32;

		// Token: 0x04001227 RID: 4647
		public const int Continue = 33;

		// Token: 0x04001228 RID: 4648
		public const int Convert = 34;

		// Token: 0x04001229 RID: 4649
		public const int Create = 35;

		// Token: 0x0400122A RID: 4650
		public const int Cross = 36;

		// Token: 0x0400122B RID: 4651
		public const int Current = 37;

		// Token: 0x0400122C RID: 4652
		public const int CurrentDate = 38;

		// Token: 0x0400122D RID: 4653
		public const int CurrentTime = 39;

		// Token: 0x0400122E RID: 4654
		public const int CurrentTimestamp = 40;

		// Token: 0x0400122F RID: 4655
		public const int CurrentUser = 41;

		// Token: 0x04001230 RID: 4656
		public const int Cursor = 42;

		// Token: 0x04001231 RID: 4657
		public const int Database = 43;

		// Token: 0x04001232 RID: 4658
		public const int Dbcc = 44;

		// Token: 0x04001233 RID: 4659
		public const int Deallocate = 45;

		// Token: 0x04001234 RID: 4660
		public const int Declare = 46;

		// Token: 0x04001235 RID: 4661
		public const int Default = 47;

		// Token: 0x04001236 RID: 4662
		public const int Delete = 48;

		// Token: 0x04001237 RID: 4663
		public const int Deny = 49;

		// Token: 0x04001238 RID: 4664
		public const int Desc = 50;

		// Token: 0x04001239 RID: 4665
		public const int Distinct = 51;

		// Token: 0x0400123A RID: 4666
		public const int Distributed = 52;

		// Token: 0x0400123B RID: 4667
		public const int Double = 53;

		// Token: 0x0400123C RID: 4668
		public const int Drop = 54;

		// Token: 0x0400123D RID: 4669
		public const int Else = 55;

		// Token: 0x0400123E RID: 4670
		public const int End = 56;

		// Token: 0x0400123F RID: 4671
		public const int Errlvl = 57;

		// Token: 0x04001240 RID: 4672
		public const int Escape = 58;

		// Token: 0x04001241 RID: 4673
		public const int Except = 59;

		// Token: 0x04001242 RID: 4674
		public const int Exec = 60;

		// Token: 0x04001243 RID: 4675
		public const int Execute = 61;

		// Token: 0x04001244 RID: 4676
		public const int Exists = 62;

		// Token: 0x04001245 RID: 4677
		public const int Exit = 63;

		// Token: 0x04001246 RID: 4678
		public const int Fetch = 64;

		// Token: 0x04001247 RID: 4679
		public const int File = 65;

		// Token: 0x04001248 RID: 4680
		public const int FillFactor = 66;

		// Token: 0x04001249 RID: 4681
		public const int For = 67;

		// Token: 0x0400124A RID: 4682
		public const int Foreign = 68;

		// Token: 0x0400124B RID: 4683
		public const int FreeText = 69;

		// Token: 0x0400124C RID: 4684
		public const int FreeTextTable = 70;

		// Token: 0x0400124D RID: 4685
		public const int From = 71;

		// Token: 0x0400124E RID: 4686
		public const int Full = 72;

		// Token: 0x0400124F RID: 4687
		public const int Function = 73;

		// Token: 0x04001250 RID: 4688
		public const int GoTo = 74;

		// Token: 0x04001251 RID: 4689
		public const int Grant = 75;

		// Token: 0x04001252 RID: 4690
		public const int Group = 76;

		// Token: 0x04001253 RID: 4691
		public const int Having = 77;

		// Token: 0x04001254 RID: 4692
		public const int HoldLock = 78;

		// Token: 0x04001255 RID: 4693
		public const int Identity = 79;

		// Token: 0x04001256 RID: 4694
		public const int IdentityInsert = 80;

		// Token: 0x04001257 RID: 4695
		public const int IdentityColumn = 81;

		// Token: 0x04001258 RID: 4696
		public const int If = 82;

		// Token: 0x04001259 RID: 4697
		public const int In = 83;

		// Token: 0x0400125A RID: 4698
		public const int Index = 84;

		// Token: 0x0400125B RID: 4699
		public const int Inner = 85;

		// Token: 0x0400125C RID: 4700
		public const int Insert = 86;

		// Token: 0x0400125D RID: 4701
		public const int Intersect = 87;

		// Token: 0x0400125E RID: 4702
		public const int Into = 88;

		// Token: 0x0400125F RID: 4703
		public const int Is = 89;

		// Token: 0x04001260 RID: 4704
		public const int Join = 90;

		// Token: 0x04001261 RID: 4705
		public const int Key = 91;

		// Token: 0x04001262 RID: 4706
		public const int Kill = 92;

		// Token: 0x04001263 RID: 4707
		public const int Left = 93;

		// Token: 0x04001264 RID: 4708
		public const int Like = 94;

		// Token: 0x04001265 RID: 4709
		public const int LineNo = 95;

		// Token: 0x04001266 RID: 4710
		public const int National = 96;

		// Token: 0x04001267 RID: 4711
		public const int NoCheck = 97;

		// Token: 0x04001268 RID: 4712
		public const int NonClustered = 98;

		// Token: 0x04001269 RID: 4713
		public const int Not = 99;

		// Token: 0x0400126A RID: 4714
		public const int Null = 100;

		// Token: 0x0400126B RID: 4715
		public const int NullIf = 101;

		// Token: 0x0400126C RID: 4716
		public const int Of = 102;

		// Token: 0x0400126D RID: 4717
		public const int Off = 103;

		// Token: 0x0400126E RID: 4718
		public const int Offsets = 104;

		// Token: 0x0400126F RID: 4719
		public const int On = 105;

		// Token: 0x04001270 RID: 4720
		public const int Open = 106;

		// Token: 0x04001271 RID: 4721
		public const int OpenDataSource = 107;

		// Token: 0x04001272 RID: 4722
		public const int OpenQuery = 108;

		// Token: 0x04001273 RID: 4723
		public const int OpenRowSet = 109;

		// Token: 0x04001274 RID: 4724
		public const int OpenXml = 110;

		// Token: 0x04001275 RID: 4725
		public const int Option = 111;

		// Token: 0x04001276 RID: 4726
		public const int Or = 112;

		// Token: 0x04001277 RID: 4727
		public const int Order = 113;

		// Token: 0x04001278 RID: 4728
		public const int Outer = 114;

		// Token: 0x04001279 RID: 4729
		public const int Over = 115;

		// Token: 0x0400127A RID: 4730
		public const int Percent = 116;

		// Token: 0x0400127B RID: 4731
		public const int Plan = 117;

		// Token: 0x0400127C RID: 4732
		public const int Primary = 118;

		// Token: 0x0400127D RID: 4733
		public const int Print = 119;

		// Token: 0x0400127E RID: 4734
		public const int Proc = 120;

		// Token: 0x0400127F RID: 4735
		public const int Procedure = 121;

		// Token: 0x04001280 RID: 4736
		public const int Public = 122;

		// Token: 0x04001281 RID: 4737
		public const int Raiserror = 123;

		// Token: 0x04001282 RID: 4738
		public const int Read = 124;

		// Token: 0x04001283 RID: 4739
		public const int ReadText = 125;

		// Token: 0x04001284 RID: 4740
		public const int Reconfigure = 126;

		// Token: 0x04001285 RID: 4741
		public const int References = 127;

		// Token: 0x04001286 RID: 4742
		public const int Replication = 128;

		// Token: 0x04001287 RID: 4743
		public const int Restore = 129;

		// Token: 0x04001288 RID: 4744
		public const int Restrict = 130;

		// Token: 0x04001289 RID: 4745
		public const int Return = 131;

		// Token: 0x0400128A RID: 4746
		public const int Revoke = 132;

		// Token: 0x0400128B RID: 4747
		public const int Right = 133;

		// Token: 0x0400128C RID: 4748
		public const int Rollback = 134;

		// Token: 0x0400128D RID: 4749
		public const int RowCount = 135;

		// Token: 0x0400128E RID: 4750
		public const int RowGuidColumn = 136;

		// Token: 0x0400128F RID: 4751
		public const int Rule = 137;

		// Token: 0x04001290 RID: 4752
		public const int Save = 138;

		// Token: 0x04001291 RID: 4753
		public const int Schema = 139;

		// Token: 0x04001292 RID: 4754
		public const int Select = 140;

		// Token: 0x04001293 RID: 4755
		public const int SessionUser = 141;

		// Token: 0x04001294 RID: 4756
		public const int Set = 142;

		// Token: 0x04001295 RID: 4757
		public const int SetUser = 143;

		// Token: 0x04001296 RID: 4758
		public const int Shutdown = 144;

		// Token: 0x04001297 RID: 4759
		public const int Some = 145;

		// Token: 0x04001298 RID: 4760
		public const int Statistics = 146;

		// Token: 0x04001299 RID: 4761
		public const int SystemUser = 147;

		// Token: 0x0400129A RID: 4762
		public const int Table = 148;

		// Token: 0x0400129B RID: 4763
		public const int TextSize = 149;

		// Token: 0x0400129C RID: 4764
		public const int Then = 150;

		// Token: 0x0400129D RID: 4765
		public const int To = 151;

		// Token: 0x0400129E RID: 4766
		public const int Top = 152;

		// Token: 0x0400129F RID: 4767
		public const int Tran = 153;

		// Token: 0x040012A0 RID: 4768
		public const int Transaction = 154;

		// Token: 0x040012A1 RID: 4769
		public const int Trigger = 155;

		// Token: 0x040012A2 RID: 4770
		public const int Truncate = 156;

		// Token: 0x040012A3 RID: 4771
		public const int TSEqual = 157;

		// Token: 0x040012A4 RID: 4772
		public const int Union = 158;

		// Token: 0x040012A5 RID: 4773
		public const int Unique = 159;

		// Token: 0x040012A6 RID: 4774
		public const int Update = 160;

		// Token: 0x040012A7 RID: 4775
		public const int UpdateText = 161;

		// Token: 0x040012A8 RID: 4776
		public const int Use = 162;

		// Token: 0x040012A9 RID: 4777
		public const int User = 163;

		// Token: 0x040012AA RID: 4778
		public const int Values = 164;

		// Token: 0x040012AB RID: 4779
		public const int Varying = 165;

		// Token: 0x040012AC RID: 4780
		public const int View = 166;

		// Token: 0x040012AD RID: 4781
		public const int WaitFor = 167;

		// Token: 0x040012AE RID: 4782
		public const int When = 168;

		// Token: 0x040012AF RID: 4783
		public const int Where = 169;

		// Token: 0x040012B0 RID: 4784
		public const int While = 170;

		// Token: 0x040012B1 RID: 4785
		public const int With = 171;

		// Token: 0x040012B2 RID: 4786
		public const int WriteText = 172;

		// Token: 0x040012B3 RID: 4787
		public const int Disk = 173;

		// Token: 0x040012B4 RID: 4788
		public const int Precision = 174;

		// Token: 0x040012B5 RID: 4789
		public const int External = 175;

		// Token: 0x040012B6 RID: 4790
		public const int Revert = 176;

		// Token: 0x040012B7 RID: 4791
		public const int Pivot = 177;

		// Token: 0x040012B8 RID: 4792
		public const int Unpivot = 178;

		// Token: 0x040012B9 RID: 4793
		public const int TableSample = 179;

		// Token: 0x040012BA RID: 4794
		public const int Dump = 180;

		// Token: 0x040012BB RID: 4795
		public const int Load = 181;

		// Token: 0x040012BC RID: 4796
		public const int Merge = 182;

		// Token: 0x040012BD RID: 4797
		public const int StopList = 183;

		// Token: 0x040012BE RID: 4798
		public const int SemanticKeyPhraseTable = 184;

		// Token: 0x040012BF RID: 4799
		public const int SemanticSimilarityTable = 185;

		// Token: 0x040012C0 RID: 4800
		public const int SemanticSimilarityDetailsTable = 186;

		// Token: 0x040012C1 RID: 4801
		public const int TryConvert = 187;

		// Token: 0x040012C2 RID: 4802
		public const int Bang = 188;

		// Token: 0x040012C3 RID: 4803
		public const int PercentSign = 189;

		// Token: 0x040012C4 RID: 4804
		public const int Ampersand = 190;

		// Token: 0x040012C5 RID: 4805
		public const int LeftParenthesis = 191;

		// Token: 0x040012C6 RID: 4806
		public const int RightParenthesis = 192;

		// Token: 0x040012C7 RID: 4807
		public const int LeftCurly = 193;

		// Token: 0x040012C8 RID: 4808
		public const int RightCurly = 194;

		// Token: 0x040012C9 RID: 4809
		public const int Star = 195;

		// Token: 0x040012CA RID: 4810
		public const int MultiplyEquals = 196;

		// Token: 0x040012CB RID: 4811
		public const int Plus = 197;

		// Token: 0x040012CC RID: 4812
		public const int Comma = 198;

		// Token: 0x040012CD RID: 4813
		public const int Minus = 199;

		// Token: 0x040012CE RID: 4814
		public const int Dot = 200;

		// Token: 0x040012CF RID: 4815
		public const int Divide = 201;

		// Token: 0x040012D0 RID: 4816
		public const int Colon = 202;

		// Token: 0x040012D1 RID: 4817
		public const int DoubleColon = 203;

		// Token: 0x040012D2 RID: 4818
		public const int Semicolon = 204;

		// Token: 0x040012D3 RID: 4819
		public const int LessThan = 205;

		// Token: 0x040012D4 RID: 4820
		public const int EqualsSign = 206;

		// Token: 0x040012D5 RID: 4821
		public const int RightOuterJoin = 207;

		// Token: 0x040012D6 RID: 4822
		public const int GreaterThan = 208;

		// Token: 0x040012D7 RID: 4823
		public const int Circumflex = 209;

		// Token: 0x040012D8 RID: 4824
		public const int VerticalLine = 210;

		// Token: 0x040012D9 RID: 4825
		public const int Tilde = 211;

		// Token: 0x040012DA RID: 4826
		public const int AddEquals = 212;

		// Token: 0x040012DB RID: 4827
		public const int SubtractEquals = 213;

		// Token: 0x040012DC RID: 4828
		public const int DivideEquals = 214;

		// Token: 0x040012DD RID: 4829
		public const int ModEquals = 215;

		// Token: 0x040012DE RID: 4830
		public const int BitwiseAndEquals = 216;

		// Token: 0x040012DF RID: 4831
		public const int BitwiseOrEquals = 217;

		// Token: 0x040012E0 RID: 4832
		public const int BitwiseXorEquals = 218;

		// Token: 0x040012E1 RID: 4833
		public const int Go = 219;

		// Token: 0x040012E2 RID: 4834
		public const int Label = 220;

		// Token: 0x040012E3 RID: 4835
		public const int Integer = 221;

		// Token: 0x040012E4 RID: 4836
		public const int Numeric = 222;

		// Token: 0x040012E5 RID: 4837
		public const int Real = 223;

		// Token: 0x040012E6 RID: 4838
		public const int HexLiteral = 224;

		// Token: 0x040012E7 RID: 4839
		public const int Money = 225;

		// Token: 0x040012E8 RID: 4840
		public const int SqlCommandIdentifier = 226;

		// Token: 0x040012E9 RID: 4841
		public const int PseudoColumn = 227;

		// Token: 0x040012EA RID: 4842
		public const int DollarPartition = 228;

		// Token: 0x040012EB RID: 4843
		public const int AsciiStringOrQuotedIdentifier = 229;

		// Token: 0x040012EC RID: 4844
		public const int AsciiStringLiteral = 230;

		// Token: 0x040012ED RID: 4845
		public const int UnicodeStringLiteral = 231;

		// Token: 0x040012EE RID: 4846
		public const int Identifier = 232;

		// Token: 0x040012EF RID: 4847
		public const int QuotedIdentifier = 233;

		// Token: 0x040012F0 RID: 4848
		public const int Variable = 234;

		// Token: 0x040012F1 RID: 4849
		public const int OdbcInitiator = 235;

		// Token: 0x040012F2 RID: 4850
		public const int ProcNameSemicolon = 236;

		// Token: 0x040012F3 RID: 4851
		public const int SingleLineComment = 237;

		// Token: 0x040012F4 RID: 4852
		public const int MultilineComment = 238;

		// Token: 0x040012F5 RID: 4853
		public const int WhiteSpace = 239;

		// Token: 0x040012F6 RID: 4854
		public const int Digit = 240;

		// Token: 0x040012F7 RID: 4855
		public const int FirstLetter = 241;

		// Token: 0x040012F8 RID: 4856
		public const int Letter = 242;

		// Token: 0x040012F9 RID: 4857
		public const int MoneySign = 243;

		// Token: 0x040012FA RID: 4858
		public const int WS_CHAR_WO_NEWLINE = 244;

		// Token: 0x040012FB RID: 4859
		public const int Number = 245;

		// Token: 0x040012FC RID: 4860
		public const int Exponent = 246;

		// Token: 0x040012FD RID: 4861
		public const int EndOfLine = 247;

		// Token: 0x040012FE RID: 4862
		public static readonly BitSet tokenSet_0_ = new BitSet(TSql90LexerInternal.mk_tokenSet_0_());

		// Token: 0x040012FF RID: 4863
		public static readonly BitSet tokenSet_1_ = new BitSet(TSql90LexerInternal.mk_tokenSet_1_());

		// Token: 0x04001300 RID: 4864
		public static readonly BitSet tokenSet_2_ = new BitSet(TSql90LexerInternal.mk_tokenSet_2_());

		// Token: 0x04001301 RID: 4865
		public static readonly BitSet tokenSet_3_ = new BitSet(TSql90LexerInternal.mk_tokenSet_3_());

		// Token: 0x04001302 RID: 4866
		public static readonly BitSet tokenSet_4_ = new BitSet(TSql90LexerInternal.mk_tokenSet_4_());

		// Token: 0x04001303 RID: 4867
		public static readonly BitSet tokenSet_5_ = new BitSet(TSql90LexerInternal.mk_tokenSet_5_());

		// Token: 0x04001304 RID: 4868
		public static readonly BitSet tokenSet_6_ = new BitSet(TSql90LexerInternal.mk_tokenSet_6_());

		// Token: 0x04001305 RID: 4869
		public static readonly BitSet tokenSet_7_ = new BitSet(TSql90LexerInternal.mk_tokenSet_7_());

		// Token: 0x04001306 RID: 4870
		public static readonly BitSet tokenSet_8_ = new BitSet(TSql90LexerInternal.mk_tokenSet_8_());

		// Token: 0x04001307 RID: 4871
		public static readonly BitSet tokenSet_9_ = new BitSet(TSql90LexerInternal.mk_tokenSet_9_());

		// Token: 0x04001308 RID: 4872
		public static readonly BitSet tokenSet_10_ = new BitSet(TSql90LexerInternal.mk_tokenSet_10_());

		// Token: 0x04001309 RID: 4873
		public static readonly BitSet tokenSet_11_ = new BitSet(TSql90LexerInternal.mk_tokenSet_11_());

		// Token: 0x0400130A RID: 4874
		public static readonly BitSet tokenSet_12_ = new BitSet(TSql90LexerInternal.mk_tokenSet_12_());
	}
}
