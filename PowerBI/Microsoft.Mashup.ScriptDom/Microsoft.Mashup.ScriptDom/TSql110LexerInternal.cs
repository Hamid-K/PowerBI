using System;
using System.Collections;
using System.IO;
using antlr;
using antlr.collections.impl;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000CA RID: 202
	internal class TSql110LexerInternal : TSqlLexerBaseInternal, TokenStream
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000DCCD File Offset: 0x0000BECD
		public TSql110LexerInternal()
			: this(new StringReader(string.Empty))
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000DCDF File Offset: 0x0000BEDF
		public TSql110LexerInternal(Stream ins)
			: this(new ByteBuffer(ins))
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000DCED File Offset: 0x0000BEED
		public TSql110LexerInternal(TextReader r)
			: this(new CharBuffer(r))
		{
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000DCFB File Offset: 0x0000BEFB
		public TSql110LexerInternal(InputBuffer ib)
			: this(new LexerSharedInputState(ib))
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000DD09 File Offset: 0x0000BF09
		public TSql110LexerInternal(LexerSharedInputState state)
			: base(state)
		{
			this.initialize();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000DD18 File Offset: 0x0000BF18
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
			this.literals.Add("semanticsimilaritydetailstable", 186);
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
			this.literals.Add("stoplist", 183);
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
			this.literals.Add("merge", 182);
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
			this.literals.Add("try_convert", 187);
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
			this.literals.Add("semanticsimilaritytable", 185);
			this.literals.Add("unpivot", 178);
			this.literals.Add("distributed", 52);
			this.literals.Add("return", 131);
			this.literals.Add("from", 71);
			this.literals.Add("openxml", 110);
			this.literals.Add("read", 124);
			this.literals.Add("setuser", 143);
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
			this.literals.Add("semantickeyphrasetable", 184);
			this.literals.Add("double", 53);
			this.literals.Add("default", 47);
			this.literals.Add("trigger", 155);
			this.literals.Add("rollback", 134);
			this.literals.Add("group", 76);
			this.literals.Add("exit", 63);
			this.literals.Add("table", 148);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public override IToken nextToken()
		{
			IToken returnToken_41;
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
						goto IL_066C;
					}
					case '!':
					{
						this.mBang(true);
						IToken returnToken_2 = this.returnToken_;
						goto IL_066C;
					}
					case '"':
					case '[':
					{
						this.mQuotedIdentifier(true);
						IToken returnToken_3 = this.returnToken_;
						goto IL_066C;
					}
					case '#':
					case '$':
					case '%':
					case '&':
					case '*':
					case '+':
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
						break;
					case '\'':
					{
						this.mAsciiStringLiteral(true);
						IToken returnToken_4 = this.returnToken_;
						goto IL_066C;
					}
					case '(':
					{
						this.mLeftParenthesis(true);
						IToken returnToken_5 = this.returnToken_;
						goto IL_066C;
					}
					case ')':
					{
						this.mRightParenthesis(true);
						IToken returnToken_6 = this.returnToken_;
						goto IL_066C;
					}
					case ',':
					{
						this.mComma(true);
						IToken returnToken_7 = this.returnToken_;
						goto IL_066C;
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
						IToken returnToken_8 = this.returnToken_;
						goto IL_066C;
					}
					case ';':
					{
						this.mProcNameSemicolon(true);
						IToken returnToken_9 = this.returnToken_;
						goto IL_066C;
					}
					case '<':
					{
						this.mLessThan(true);
						IToken returnToken_10 = this.returnToken_;
						goto IL_066C;
					}
					case '>':
					{
						this.mGreaterThan(true);
						IToken returnToken_11 = this.returnToken_;
						goto IL_066C;
					}
					case '@':
					{
						this.mVariable(true);
						IToken returnToken_12 = this.returnToken_;
						goto IL_066C;
					}
					default:
						switch (cached_LA)
						{
						case '{':
						{
							this.mLeftCurly(true);
							IToken returnToken_13 = this.returnToken_;
							goto IL_066C;
						}
						case '}':
						{
							this.mRightCurly(true);
							IToken returnToken_14 = this.returnToken_;
							goto IL_066C;
						}
						case '~':
						{
							this.mTilde(true);
							IToken returnToken_15 = this.returnToken_;
							goto IL_066C;
						}
						}
						break;
					}
					if (this.cached_LA1 == '*' && this.cached_LA2 == '=')
					{
						this.mMultiplyEquals(true);
						IToken returnToken_16 = this.returnToken_;
					}
					else if (this.cached_LA1 == ':' && this.cached_LA2 == ':')
					{
						this.mDoubleColon(true);
						IToken returnToken_17 = this.returnToken_;
					}
					else if (this.cached_LA1 == '=' && this.cached_LA2 == '*')
					{
						this.mRightOuterJoin(true);
						IToken returnToken_18 = this.returnToken_;
					}
					else if (this.cached_LA1 == '+' && this.cached_LA2 == '=')
					{
						this.mAddEquals(true);
						IToken returnToken_19 = this.returnToken_;
					}
					else if (this.cached_LA1 == '-' && this.cached_LA2 == '=')
					{
						this.mSubtractEquals(true);
						IToken returnToken_20 = this.returnToken_;
					}
					else if (this.cached_LA1 == '/' && this.cached_LA2 == '=')
					{
						this.mDivideEquals(true);
						IToken returnToken_21 = this.returnToken_;
					}
					else if (this.cached_LA1 == '%' && this.cached_LA2 == '=')
					{
						this.mModEquals(true);
						IToken returnToken_22 = this.returnToken_;
					}
					else if (this.cached_LA1 == '&' && this.cached_LA2 == '=')
					{
						this.mBitwiseAndEquals(true);
						IToken returnToken_23 = this.returnToken_;
					}
					else if (this.cached_LA1 == '|' && this.cached_LA2 == '=')
					{
						this.mBitwiseOrEquals(true);
						IToken returnToken_24 = this.returnToken_;
					}
					else if (this.cached_LA1 == '^' && this.cached_LA2 == '=')
					{
						this.mBitwiseXorEquals(true);
						IToken returnToken_25 = this.returnToken_;
					}
					else if (this.cached_LA1 == 'g' && this.cached_LA2 == 'o' && base.CurrentOffset == this._acceptableGoOffset)
					{
						this.mGo(true);
						IToken returnToken_26 = this.returnToken_;
					}
					else if (this.cached_LA1 == 'n' && this.cached_LA2 == '\'')
					{
						this.mUnicodeStringLiteral(true);
						IToken returnToken_27 = this.returnToken_;
					}
					else if (this.cached_LA1 == '-' && this.cached_LA2 == '-')
					{
						this.mSingleLineComment(true);
						IToken returnToken_28 = this.returnToken_;
					}
					else if (this.cached_LA1 == '/' && this.cached_LA2 == '*')
					{
						this.mMultilineComment(true);
						IToken returnToken_29 = this.returnToken_;
					}
					else if (this.cached_LA1 == '%')
					{
						this.mPercentSign(true);
						IToken returnToken_30 = this.returnToken_;
					}
					else if (this.cached_LA1 == '&')
					{
						this.mAmpersand(true);
						IToken returnToken_31 = this.returnToken_;
					}
					else if (this.cached_LA1 == '*')
					{
						this.mStar(true);
						IToken returnToken_32 = this.returnToken_;
					}
					else if (this.cached_LA1 == '+')
					{
						this.mPlus(true);
						IToken returnToken_33 = this.returnToken_;
					}
					else if (this.cached_LA1 == '-')
					{
						this.mMinus(true);
						IToken returnToken_34 = this.returnToken_;
					}
					else if (this.cached_LA1 == '/')
					{
						this.mDivide(true);
						IToken returnToken_35 = this.returnToken_;
					}
					else if (this.cached_LA1 == ':')
					{
						this.mColon(true);
						IToken returnToken_36 = this.returnToken_;
					}
					else if (this.cached_LA1 == '=')
					{
						this.mEqualsSign(true);
						IToken returnToken_37 = this.returnToken_;
					}
					else if (this.cached_LA1 == '^')
					{
						this.mCircumflex(true);
						IToken returnToken_38 = this.returnToken_;
					}
					else if (this.cached_LA1 == '|')
					{
						this.mVerticalLine(true);
						IToken returnToken_39 = this.returnToken_;
					}
					else if (TSql110LexerInternal.tokenSet_0_.member((int)this.cached_LA1))
					{
						this.mIdentifier(true);
						IToken returnToken_40 = this.returnToken_;
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
					IL_066C:
					if (this.returnToken_ == null)
					{
						continue;
					}
					int type = this.returnToken_.Type;
					this.returnToken_.Type = type;
					returnToken_41 = this.returnToken_;
				}
				catch (RecognitionException ex)
				{
					throw new TokenStreamRecognitionException(ex);
				}
				break;
			}
			return returnToken_41;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
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

		// Token: 0x060002EC RID: 748 RVA: 0x0000F550 File Offset: 0x0000D750
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

		// Token: 0x060002ED RID: 749 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
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

		// Token: 0x060002EE RID: 750 RVA: 0x0000F620 File Offset: 0x0000D820
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

		// Token: 0x060002EF RID: 751 RVA: 0x0000F688 File Offset: 0x0000D888
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

		// Token: 0x060002F0 RID: 752 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
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

		// Token: 0x060002F1 RID: 753 RVA: 0x0000F758 File Offset: 0x0000D958
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

		// Token: 0x060002F2 RID: 754 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
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

		// Token: 0x060002F3 RID: 755 RVA: 0x0000F828 File Offset: 0x0000DA28
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

		// Token: 0x060002F4 RID: 756 RVA: 0x0000F890 File Offset: 0x0000DA90
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

		// Token: 0x060002F5 RID: 757 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
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

		// Token: 0x060002F6 RID: 758 RVA: 0x0000F960 File Offset: 0x0000DB60
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

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
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

		// Token: 0x060002F8 RID: 760 RVA: 0x0000FA28 File Offset: 0x0000DC28
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

		// Token: 0x060002F9 RID: 761 RVA: 0x0000FA90 File Offset: 0x0000DC90
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

		// Token: 0x060002FA RID: 762 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
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

		// Token: 0x060002FB RID: 763 RVA: 0x0000FB60 File Offset: 0x0000DD60
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

		// Token: 0x060002FC RID: 764 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
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

		// Token: 0x060002FD RID: 765 RVA: 0x0000FC30 File Offset: 0x0000DE30
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

		// Token: 0x060002FE RID: 766 RVA: 0x0000FC98 File Offset: 0x0000DE98
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

		// Token: 0x060002FF RID: 767 RVA: 0x0000FD00 File Offset: 0x0000DF00
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

		// Token: 0x06000300 RID: 768 RVA: 0x0000FD68 File Offset: 0x0000DF68
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

		// Token: 0x06000301 RID: 769 RVA: 0x0000FDD0 File Offset: 0x0000DFD0
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

		// Token: 0x06000302 RID: 770 RVA: 0x0000FE38 File Offset: 0x0000E038
		public void mAddEquals(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 212;
			this.match("+=");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public void mSubtractEquals(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 213;
			this.match("-=");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000FF08 File Offset: 0x0000E108
		public void mDivideEquals(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 214;
			this.match("/=");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000FF70 File Offset: 0x0000E170
		public void mModEquals(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 215;
			this.match("%=");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		public void mBitwiseAndEquals(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 216;
			this.match("&=");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00010040 File Offset: 0x0000E240
		public void mBitwiseOrEquals(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 217;
			this.match("|=");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000100A8 File Offset: 0x0000E2A8
		public void mBitwiseXorEquals(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 218;
			this.match("^=");
			if (_createToken && token == null && num != Token.SKIP)
			{
				token = this.makeToken(num);
				token.setText(this.text.ToString(length, this.text.Length - length));
			}
			this.returnToken_ = token;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00010110 File Offset: 0x0000E310
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

		// Token: 0x0600030A RID: 778 RVA: 0x00010178 File Offset: 0x0000E378
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

		// Token: 0x0600030B RID: 779 RVA: 0x000101E0 File Offset: 0x0000E3E0
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

		// Token: 0x0600030C RID: 780 RVA: 0x00010334 File Offset: 0x0000E534
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

		// Token: 0x0600030D RID: 781 RVA: 0x000104AC File Offset: 0x0000E6AC
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

		// Token: 0x0600030E RID: 782 RVA: 0x000106C0 File Offset: 0x0000E8C0
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
					while (TSql110LexerInternal.tokenSet_1_.member((int)this.cached_LA1))
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

		// Token: 0x0600030F RID: 783 RVA: 0x000107F4 File Offset: 0x0000E9F4
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

		// Token: 0x06000310 RID: 784 RVA: 0x00010A64 File Offset: 0x0000EC64
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

		// Token: 0x06000311 RID: 785 RVA: 0x00010D84 File Offset: 0x0000EF84
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
				while (TSql110LexerInternal.tokenSet_1_.member((int)this.cached_LA1))
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

		// Token: 0x06000312 RID: 786 RVA: 0x00010F2C File Offset: 0x0000F12C
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

		// Token: 0x06000313 RID: 787 RVA: 0x00011004 File Offset: 0x0000F204
		public void mGo(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 219;
			if (base.CurrentOffset != this._acceptableGoOffset)
			{
				throw new SemanticException("CurrentOffset==_acceptableGoOffset");
			}
			this.match("go");
			for (;;)
			{
				if (TSql110LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
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

		// Token: 0x06000314 RID: 788 RVA: 0x00011174 File Offset: 0x0000F374
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

		// Token: 0x06000315 RID: 789 RVA: 0x000111D4 File Offset: 0x0000F3D4
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

		// Token: 0x06000316 RID: 790 RVA: 0x00011234 File Offset: 0x0000F434
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

		// Token: 0x06000317 RID: 791 RVA: 0x00011294 File Offset: 0x0000F494
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

		// Token: 0x06000318 RID: 792 RVA: 0x000112F4 File Offset: 0x0000F4F4
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

		// Token: 0x06000319 RID: 793 RVA: 0x00011354 File Offset: 0x0000F554
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

		// Token: 0x0600031A RID: 794 RVA: 0x00011408 File Offset: 0x0000F608
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

		// Token: 0x0600031B RID: 795 RVA: 0x00011468 File Offset: 0x0000F668
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

		// Token: 0x0600031C RID: 796 RVA: 0x000114C8 File Offset: 0x0000F6C8
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

		// Token: 0x0600031D RID: 797 RVA: 0x00011528 File Offset: 0x0000F728
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

		// Token: 0x0600031E RID: 798 RVA: 0x00011588 File Offset: 0x0000F788
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

		// Token: 0x0600031F RID: 799 RVA: 0x000115E8 File Offset: 0x0000F7E8
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
				else if (TSql110LexerInternal.tokenSet_3_.member((int)this.cached_LA1))
				{
					if (this.inputState.guessing == 0)
					{
						base.checkEOF(TSqlLexerBaseInternal.TokenKind.String);
					}
					this.match(TSql110LexerInternal.tokenSet_3_);
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

		// Token: 0x06000320 RID: 800 RVA: 0x000116E0 File Offset: 0x0000F8E0
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
				else if (TSql110LexerInternal.tokenSet_3_.member((int)this.cached_LA1))
				{
					if (this.inputState.guessing == 0)
					{
						base.checkEOF(TSqlLexerBaseInternal.TokenKind.String);
					}
					this.match(TSql110LexerInternal.tokenSet_3_);
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

		// Token: 0x06000321 RID: 801 RVA: 0x000117E0 File Offset: 0x0000F9E0
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
					if (TSql110LexerInternal.tokenSet_4_.member((int)this.cached_LA1))
					{
						if (this.inputState.guessing == 0)
						{
							base.checkEOF(TSqlLexerBaseInternal.TokenKind.SqlCommandIdentifier);
						}
						this.match(TSql110LexerInternal.tokenSet_4_);
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
				if (this.cached_LA1 == '$' && TSql110LexerInternal.tokenSet_5_.member((int)this.cached_LA2))
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
							if (!TSql110LexerInternal.tokenSet_6_.member((int)this.cached_LA1))
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
						if (!TSql110LexerInternal.tokenSet_6_.member((int)this.cached_LA1))
						{
							throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
						}
						this.mFirstLetter(false);
					}
					for (;;)
					{
						if (TSql110LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
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
					if (TSql110LexerInternal.tokenSet_7_.member((int)this.cached_LA1) && TSql110LexerInternal.tokenSet_8_.member((int)this.cached_LA2))
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
						if (!TSql110LexerInternal.tokenSet_6_.member((int)this.cached_LA1))
						{
							throw new NoViableAltForCharException(this.cached_LA1, this.getFilename(), this.getLine(), this.getColumn());
						}
						this.mFirstLetter(false);
						for (;;)
						{
							if (TSql110LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
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

		// Token: 0x06000322 RID: 802 RVA: 0x00011DF0 File Offset: 0x0000FFF0
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
					else if (TSql110LexerInternal.tokenSet_9_.member((int)this.cached_LA1))
					{
						if (this.inputState.guessing == 0)
						{
							base.checkEOF(TSqlLexerBaseInternal.TokenKind.QuotedIdentifier);
						}
						this.match(TSql110LexerInternal.tokenSet_9_);
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
					else if (TSql110LexerInternal.tokenSet_10_.member((int)this.cached_LA1))
					{
						if (this.inputState.guessing == 0)
						{
							base.checkEOF(TSqlLexerBaseInternal.TokenKind.QuotedIdentifier);
						}
						this.match(TSql110LexerInternal.tokenSet_10_);
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

		// Token: 0x06000323 RID: 803 RVA: 0x00012014 File Offset: 0x00010214
		public void mVariable(bool _createToken)
		{
			IToken token = null;
			int length = this.text.Length;
			int num = 234;
			this.match('@');
			for (;;)
			{
				if (TSql110LexerInternal.tokenSet_2_.member((int)this.cached_LA1))
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

		// Token: 0x06000324 RID: 804 RVA: 0x000120B4 File Offset: 0x000102B4
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

		// Token: 0x06000325 RID: 805 RVA: 0x00012114 File Offset: 0x00010314
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
				while (TSql110LexerInternal.tokenSet_11_.member((int)this.cached_LA1) && this.LA(1) != CharScanner.EOF_CHAR)
				{
					this.match(TSql110LexerInternal.tokenSet_11_);
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

		// Token: 0x06000326 RID: 806 RVA: 0x00012260 File Offset: 0x00010460
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
					if (!TSql110LexerInternal.tokenSet_12_.member((int)this.cached_LA1))
					{
						break;
					}
					if (this.inputState.guessing == 0)
					{
						base.checkEOF(TSqlLexerBaseInternal.TokenKind.MultiLineComment);
					}
					this.match(TSql110LexerInternal.tokenSet_12_);
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

		// Token: 0x06000327 RID: 807 RVA: 0x000123F4 File Offset: 0x000105F4
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

		// Token: 0x06000328 RID: 808 RVA: 0x00012464 File Offset: 0x00010664
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

		// Token: 0x06000329 RID: 809 RVA: 0x000124A0 File Offset: 0x000106A0
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

		// Token: 0x0600032A RID: 810 RVA: 0x00012510 File Offset: 0x00010710
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

		// Token: 0x0600032B RID: 811 RVA: 0x00012564 File Offset: 0x00010764
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

		// Token: 0x0600032C RID: 812 RVA: 0x000125B8 File Offset: 0x000107B8
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

		// Token: 0x0600032D RID: 813 RVA: 0x00012628 File Offset: 0x00010828
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

		// Token: 0x0600032E RID: 814 RVA: 0x00012698 File Offset: 0x00010898
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

		// Token: 0x0600032F RID: 815 RVA: 0x00012754 File Offset: 0x00010954
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

		// Token: 0x06000330 RID: 816 RVA: 0x00012790 File Offset: 0x00010990
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

		// Token: 0x06000331 RID: 817 RVA: 0x000127EC File Offset: 0x000109EC
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

		// Token: 0x06000332 RID: 818 RVA: 0x00012840 File Offset: 0x00010A40
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

		// Token: 0x06000333 RID: 819 RVA: 0x00012890 File Offset: 0x00010A90
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

		// Token: 0x0400060E RID: 1550
		public const int EOF = 1;

		// Token: 0x0400060F RID: 1551
		public const int NULL_TREE_LOOKAHEAD = 3;

		// Token: 0x04000610 RID: 1552
		public const int Add = 4;

		// Token: 0x04000611 RID: 1553
		public const int All = 5;

		// Token: 0x04000612 RID: 1554
		public const int Alter = 6;

		// Token: 0x04000613 RID: 1555
		public const int And = 7;

		// Token: 0x04000614 RID: 1556
		public const int Any = 8;

		// Token: 0x04000615 RID: 1557
		public const int As = 9;

		// Token: 0x04000616 RID: 1558
		public const int Asc = 10;

		// Token: 0x04000617 RID: 1559
		public const int Authorization = 11;

		// Token: 0x04000618 RID: 1560
		public const int Backup = 12;

		// Token: 0x04000619 RID: 1561
		public const int Begin = 13;

		// Token: 0x0400061A RID: 1562
		public const int Between = 14;

		// Token: 0x0400061B RID: 1563
		public const int Break = 15;

		// Token: 0x0400061C RID: 1564
		public const int Browse = 16;

		// Token: 0x0400061D RID: 1565
		public const int Bulk = 17;

		// Token: 0x0400061E RID: 1566
		public const int By = 18;

		// Token: 0x0400061F RID: 1567
		public const int Cascade = 19;

		// Token: 0x04000620 RID: 1568
		public const int Case = 20;

		// Token: 0x04000621 RID: 1569
		public const int Check = 21;

		// Token: 0x04000622 RID: 1570
		public const int Checkpoint = 22;

		// Token: 0x04000623 RID: 1571
		public const int Close = 23;

		// Token: 0x04000624 RID: 1572
		public const int Clustered = 24;

		// Token: 0x04000625 RID: 1573
		public const int Coalesce = 25;

		// Token: 0x04000626 RID: 1574
		public const int Collate = 26;

		// Token: 0x04000627 RID: 1575
		public const int Column = 27;

		// Token: 0x04000628 RID: 1576
		public const int Commit = 28;

		// Token: 0x04000629 RID: 1577
		public const int Compute = 29;

		// Token: 0x0400062A RID: 1578
		public const int Constraint = 30;

		// Token: 0x0400062B RID: 1579
		public const int Contains = 31;

		// Token: 0x0400062C RID: 1580
		public const int ContainsTable = 32;

		// Token: 0x0400062D RID: 1581
		public const int Continue = 33;

		// Token: 0x0400062E RID: 1582
		public const int Convert = 34;

		// Token: 0x0400062F RID: 1583
		public const int Create = 35;

		// Token: 0x04000630 RID: 1584
		public const int Cross = 36;

		// Token: 0x04000631 RID: 1585
		public const int Current = 37;

		// Token: 0x04000632 RID: 1586
		public const int CurrentDate = 38;

		// Token: 0x04000633 RID: 1587
		public const int CurrentTime = 39;

		// Token: 0x04000634 RID: 1588
		public const int CurrentTimestamp = 40;

		// Token: 0x04000635 RID: 1589
		public const int CurrentUser = 41;

		// Token: 0x04000636 RID: 1590
		public const int Cursor = 42;

		// Token: 0x04000637 RID: 1591
		public const int Database = 43;

		// Token: 0x04000638 RID: 1592
		public const int Dbcc = 44;

		// Token: 0x04000639 RID: 1593
		public const int Deallocate = 45;

		// Token: 0x0400063A RID: 1594
		public const int Declare = 46;

		// Token: 0x0400063B RID: 1595
		public const int Default = 47;

		// Token: 0x0400063C RID: 1596
		public const int Delete = 48;

		// Token: 0x0400063D RID: 1597
		public const int Deny = 49;

		// Token: 0x0400063E RID: 1598
		public const int Desc = 50;

		// Token: 0x0400063F RID: 1599
		public const int Distinct = 51;

		// Token: 0x04000640 RID: 1600
		public const int Distributed = 52;

		// Token: 0x04000641 RID: 1601
		public const int Double = 53;

		// Token: 0x04000642 RID: 1602
		public const int Drop = 54;

		// Token: 0x04000643 RID: 1603
		public const int Else = 55;

		// Token: 0x04000644 RID: 1604
		public const int End = 56;

		// Token: 0x04000645 RID: 1605
		public const int Errlvl = 57;

		// Token: 0x04000646 RID: 1606
		public const int Escape = 58;

		// Token: 0x04000647 RID: 1607
		public const int Except = 59;

		// Token: 0x04000648 RID: 1608
		public const int Exec = 60;

		// Token: 0x04000649 RID: 1609
		public const int Execute = 61;

		// Token: 0x0400064A RID: 1610
		public const int Exists = 62;

		// Token: 0x0400064B RID: 1611
		public const int Exit = 63;

		// Token: 0x0400064C RID: 1612
		public const int Fetch = 64;

		// Token: 0x0400064D RID: 1613
		public const int File = 65;

		// Token: 0x0400064E RID: 1614
		public const int FillFactor = 66;

		// Token: 0x0400064F RID: 1615
		public const int For = 67;

		// Token: 0x04000650 RID: 1616
		public const int Foreign = 68;

		// Token: 0x04000651 RID: 1617
		public const int FreeText = 69;

		// Token: 0x04000652 RID: 1618
		public const int FreeTextTable = 70;

		// Token: 0x04000653 RID: 1619
		public const int From = 71;

		// Token: 0x04000654 RID: 1620
		public const int Full = 72;

		// Token: 0x04000655 RID: 1621
		public const int Function = 73;

		// Token: 0x04000656 RID: 1622
		public const int GoTo = 74;

		// Token: 0x04000657 RID: 1623
		public const int Grant = 75;

		// Token: 0x04000658 RID: 1624
		public const int Group = 76;

		// Token: 0x04000659 RID: 1625
		public const int Having = 77;

		// Token: 0x0400065A RID: 1626
		public const int HoldLock = 78;

		// Token: 0x0400065B RID: 1627
		public const int Identity = 79;

		// Token: 0x0400065C RID: 1628
		public const int IdentityInsert = 80;

		// Token: 0x0400065D RID: 1629
		public const int IdentityColumn = 81;

		// Token: 0x0400065E RID: 1630
		public const int If = 82;

		// Token: 0x0400065F RID: 1631
		public const int In = 83;

		// Token: 0x04000660 RID: 1632
		public const int Index = 84;

		// Token: 0x04000661 RID: 1633
		public const int Inner = 85;

		// Token: 0x04000662 RID: 1634
		public const int Insert = 86;

		// Token: 0x04000663 RID: 1635
		public const int Intersect = 87;

		// Token: 0x04000664 RID: 1636
		public const int Into = 88;

		// Token: 0x04000665 RID: 1637
		public const int Is = 89;

		// Token: 0x04000666 RID: 1638
		public const int Join = 90;

		// Token: 0x04000667 RID: 1639
		public const int Key = 91;

		// Token: 0x04000668 RID: 1640
		public const int Kill = 92;

		// Token: 0x04000669 RID: 1641
		public const int Left = 93;

		// Token: 0x0400066A RID: 1642
		public const int Like = 94;

		// Token: 0x0400066B RID: 1643
		public const int LineNo = 95;

		// Token: 0x0400066C RID: 1644
		public const int National = 96;

		// Token: 0x0400066D RID: 1645
		public const int NoCheck = 97;

		// Token: 0x0400066E RID: 1646
		public const int NonClustered = 98;

		// Token: 0x0400066F RID: 1647
		public const int Not = 99;

		// Token: 0x04000670 RID: 1648
		public const int Null = 100;

		// Token: 0x04000671 RID: 1649
		public const int NullIf = 101;

		// Token: 0x04000672 RID: 1650
		public const int Of = 102;

		// Token: 0x04000673 RID: 1651
		public const int Off = 103;

		// Token: 0x04000674 RID: 1652
		public const int Offsets = 104;

		// Token: 0x04000675 RID: 1653
		public const int On = 105;

		// Token: 0x04000676 RID: 1654
		public const int Open = 106;

		// Token: 0x04000677 RID: 1655
		public const int OpenDataSource = 107;

		// Token: 0x04000678 RID: 1656
		public const int OpenQuery = 108;

		// Token: 0x04000679 RID: 1657
		public const int OpenRowSet = 109;

		// Token: 0x0400067A RID: 1658
		public const int OpenXml = 110;

		// Token: 0x0400067B RID: 1659
		public const int Option = 111;

		// Token: 0x0400067C RID: 1660
		public const int Or = 112;

		// Token: 0x0400067D RID: 1661
		public const int Order = 113;

		// Token: 0x0400067E RID: 1662
		public const int Outer = 114;

		// Token: 0x0400067F RID: 1663
		public const int Over = 115;

		// Token: 0x04000680 RID: 1664
		public const int Percent = 116;

		// Token: 0x04000681 RID: 1665
		public const int Plan = 117;

		// Token: 0x04000682 RID: 1666
		public const int Primary = 118;

		// Token: 0x04000683 RID: 1667
		public const int Print = 119;

		// Token: 0x04000684 RID: 1668
		public const int Proc = 120;

		// Token: 0x04000685 RID: 1669
		public const int Procedure = 121;

		// Token: 0x04000686 RID: 1670
		public const int Public = 122;

		// Token: 0x04000687 RID: 1671
		public const int Raiserror = 123;

		// Token: 0x04000688 RID: 1672
		public const int Read = 124;

		// Token: 0x04000689 RID: 1673
		public const int ReadText = 125;

		// Token: 0x0400068A RID: 1674
		public const int Reconfigure = 126;

		// Token: 0x0400068B RID: 1675
		public const int References = 127;

		// Token: 0x0400068C RID: 1676
		public const int Replication = 128;

		// Token: 0x0400068D RID: 1677
		public const int Restore = 129;

		// Token: 0x0400068E RID: 1678
		public const int Restrict = 130;

		// Token: 0x0400068F RID: 1679
		public const int Return = 131;

		// Token: 0x04000690 RID: 1680
		public const int Revoke = 132;

		// Token: 0x04000691 RID: 1681
		public const int Right = 133;

		// Token: 0x04000692 RID: 1682
		public const int Rollback = 134;

		// Token: 0x04000693 RID: 1683
		public const int RowCount = 135;

		// Token: 0x04000694 RID: 1684
		public const int RowGuidColumn = 136;

		// Token: 0x04000695 RID: 1685
		public const int Rule = 137;

		// Token: 0x04000696 RID: 1686
		public const int Save = 138;

		// Token: 0x04000697 RID: 1687
		public const int Schema = 139;

		// Token: 0x04000698 RID: 1688
		public const int Select = 140;

		// Token: 0x04000699 RID: 1689
		public const int SessionUser = 141;

		// Token: 0x0400069A RID: 1690
		public const int Set = 142;

		// Token: 0x0400069B RID: 1691
		public const int SetUser = 143;

		// Token: 0x0400069C RID: 1692
		public const int Shutdown = 144;

		// Token: 0x0400069D RID: 1693
		public const int Some = 145;

		// Token: 0x0400069E RID: 1694
		public const int Statistics = 146;

		// Token: 0x0400069F RID: 1695
		public const int SystemUser = 147;

		// Token: 0x040006A0 RID: 1696
		public const int Table = 148;

		// Token: 0x040006A1 RID: 1697
		public const int TextSize = 149;

		// Token: 0x040006A2 RID: 1698
		public const int Then = 150;

		// Token: 0x040006A3 RID: 1699
		public const int To = 151;

		// Token: 0x040006A4 RID: 1700
		public const int Top = 152;

		// Token: 0x040006A5 RID: 1701
		public const int Tran = 153;

		// Token: 0x040006A6 RID: 1702
		public const int Transaction = 154;

		// Token: 0x040006A7 RID: 1703
		public const int Trigger = 155;

		// Token: 0x040006A8 RID: 1704
		public const int Truncate = 156;

		// Token: 0x040006A9 RID: 1705
		public const int TSEqual = 157;

		// Token: 0x040006AA RID: 1706
		public const int Union = 158;

		// Token: 0x040006AB RID: 1707
		public const int Unique = 159;

		// Token: 0x040006AC RID: 1708
		public const int Update = 160;

		// Token: 0x040006AD RID: 1709
		public const int UpdateText = 161;

		// Token: 0x040006AE RID: 1710
		public const int Use = 162;

		// Token: 0x040006AF RID: 1711
		public const int User = 163;

		// Token: 0x040006B0 RID: 1712
		public const int Values = 164;

		// Token: 0x040006B1 RID: 1713
		public const int Varying = 165;

		// Token: 0x040006B2 RID: 1714
		public const int View = 166;

		// Token: 0x040006B3 RID: 1715
		public const int WaitFor = 167;

		// Token: 0x040006B4 RID: 1716
		public const int When = 168;

		// Token: 0x040006B5 RID: 1717
		public const int Where = 169;

		// Token: 0x040006B6 RID: 1718
		public const int While = 170;

		// Token: 0x040006B7 RID: 1719
		public const int With = 171;

		// Token: 0x040006B8 RID: 1720
		public const int WriteText = 172;

		// Token: 0x040006B9 RID: 1721
		public const int Disk = 173;

		// Token: 0x040006BA RID: 1722
		public const int Precision = 174;

		// Token: 0x040006BB RID: 1723
		public const int External = 175;

		// Token: 0x040006BC RID: 1724
		public const int Revert = 176;

		// Token: 0x040006BD RID: 1725
		public const int Pivot = 177;

		// Token: 0x040006BE RID: 1726
		public const int Unpivot = 178;

		// Token: 0x040006BF RID: 1727
		public const int TableSample = 179;

		// Token: 0x040006C0 RID: 1728
		public const int Dump = 180;

		// Token: 0x040006C1 RID: 1729
		public const int Load = 181;

		// Token: 0x040006C2 RID: 1730
		public const int Merge = 182;

		// Token: 0x040006C3 RID: 1731
		public const int StopList = 183;

		// Token: 0x040006C4 RID: 1732
		public const int SemanticKeyPhraseTable = 184;

		// Token: 0x040006C5 RID: 1733
		public const int SemanticSimilarityTable = 185;

		// Token: 0x040006C6 RID: 1734
		public const int SemanticSimilarityDetailsTable = 186;

		// Token: 0x040006C7 RID: 1735
		public const int TryConvert = 187;

		// Token: 0x040006C8 RID: 1736
		public const int Bang = 188;

		// Token: 0x040006C9 RID: 1737
		public const int PercentSign = 189;

		// Token: 0x040006CA RID: 1738
		public const int Ampersand = 190;

		// Token: 0x040006CB RID: 1739
		public const int LeftParenthesis = 191;

		// Token: 0x040006CC RID: 1740
		public const int RightParenthesis = 192;

		// Token: 0x040006CD RID: 1741
		public const int LeftCurly = 193;

		// Token: 0x040006CE RID: 1742
		public const int RightCurly = 194;

		// Token: 0x040006CF RID: 1743
		public const int Star = 195;

		// Token: 0x040006D0 RID: 1744
		public const int MultiplyEquals = 196;

		// Token: 0x040006D1 RID: 1745
		public const int Plus = 197;

		// Token: 0x040006D2 RID: 1746
		public const int Comma = 198;

		// Token: 0x040006D3 RID: 1747
		public const int Minus = 199;

		// Token: 0x040006D4 RID: 1748
		public const int Dot = 200;

		// Token: 0x040006D5 RID: 1749
		public const int Divide = 201;

		// Token: 0x040006D6 RID: 1750
		public const int Colon = 202;

		// Token: 0x040006D7 RID: 1751
		public const int DoubleColon = 203;

		// Token: 0x040006D8 RID: 1752
		public const int Semicolon = 204;

		// Token: 0x040006D9 RID: 1753
		public const int LessThan = 205;

		// Token: 0x040006DA RID: 1754
		public const int EqualsSign = 206;

		// Token: 0x040006DB RID: 1755
		public const int RightOuterJoin = 207;

		// Token: 0x040006DC RID: 1756
		public const int GreaterThan = 208;

		// Token: 0x040006DD RID: 1757
		public const int Circumflex = 209;

		// Token: 0x040006DE RID: 1758
		public const int VerticalLine = 210;

		// Token: 0x040006DF RID: 1759
		public const int Tilde = 211;

		// Token: 0x040006E0 RID: 1760
		public const int AddEquals = 212;

		// Token: 0x040006E1 RID: 1761
		public const int SubtractEquals = 213;

		// Token: 0x040006E2 RID: 1762
		public const int DivideEquals = 214;

		// Token: 0x040006E3 RID: 1763
		public const int ModEquals = 215;

		// Token: 0x040006E4 RID: 1764
		public const int BitwiseAndEquals = 216;

		// Token: 0x040006E5 RID: 1765
		public const int BitwiseOrEquals = 217;

		// Token: 0x040006E6 RID: 1766
		public const int BitwiseXorEquals = 218;

		// Token: 0x040006E7 RID: 1767
		public const int Go = 219;

		// Token: 0x040006E8 RID: 1768
		public const int Label = 220;

		// Token: 0x040006E9 RID: 1769
		public const int Integer = 221;

		// Token: 0x040006EA RID: 1770
		public const int Numeric = 222;

		// Token: 0x040006EB RID: 1771
		public const int Real = 223;

		// Token: 0x040006EC RID: 1772
		public const int HexLiteral = 224;

		// Token: 0x040006ED RID: 1773
		public const int Money = 225;

		// Token: 0x040006EE RID: 1774
		public const int SqlCommandIdentifier = 226;

		// Token: 0x040006EF RID: 1775
		public const int PseudoColumn = 227;

		// Token: 0x040006F0 RID: 1776
		public const int DollarPartition = 228;

		// Token: 0x040006F1 RID: 1777
		public const int AsciiStringOrQuotedIdentifier = 229;

		// Token: 0x040006F2 RID: 1778
		public const int AsciiStringLiteral = 230;

		// Token: 0x040006F3 RID: 1779
		public const int UnicodeStringLiteral = 231;

		// Token: 0x040006F4 RID: 1780
		public const int Identifier = 232;

		// Token: 0x040006F5 RID: 1781
		public const int QuotedIdentifier = 233;

		// Token: 0x040006F6 RID: 1782
		public const int Variable = 234;

		// Token: 0x040006F7 RID: 1783
		public const int OdbcInitiator = 235;

		// Token: 0x040006F8 RID: 1784
		public const int ProcNameSemicolon = 236;

		// Token: 0x040006F9 RID: 1785
		public const int SingleLineComment = 237;

		// Token: 0x040006FA RID: 1786
		public const int MultilineComment = 238;

		// Token: 0x040006FB RID: 1787
		public const int WhiteSpace = 239;

		// Token: 0x040006FC RID: 1788
		public const int Digit = 240;

		// Token: 0x040006FD RID: 1789
		public const int FirstLetter = 241;

		// Token: 0x040006FE RID: 1790
		public const int Letter = 242;

		// Token: 0x040006FF RID: 1791
		public const int MoneySign = 243;

		// Token: 0x04000700 RID: 1792
		public const int WS_CHAR_WO_NEWLINE = 244;

		// Token: 0x04000701 RID: 1793
		public const int Number = 245;

		// Token: 0x04000702 RID: 1794
		public const int Exponent = 246;

		// Token: 0x04000703 RID: 1795
		public const int EndOfLine = 247;

		// Token: 0x04000704 RID: 1796
		public static readonly BitSet tokenSet_0_ = new BitSet(TSql110LexerInternal.mk_tokenSet_0_());

		// Token: 0x04000705 RID: 1797
		public static readonly BitSet tokenSet_1_ = new BitSet(TSql110LexerInternal.mk_tokenSet_1_());

		// Token: 0x04000706 RID: 1798
		public static readonly BitSet tokenSet_2_ = new BitSet(TSql110LexerInternal.mk_tokenSet_2_());

		// Token: 0x04000707 RID: 1799
		public static readonly BitSet tokenSet_3_ = new BitSet(TSql110LexerInternal.mk_tokenSet_3_());

		// Token: 0x04000708 RID: 1800
		public static readonly BitSet tokenSet_4_ = new BitSet(TSql110LexerInternal.mk_tokenSet_4_());

		// Token: 0x04000709 RID: 1801
		public static readonly BitSet tokenSet_5_ = new BitSet(TSql110LexerInternal.mk_tokenSet_5_());

		// Token: 0x0400070A RID: 1802
		public static readonly BitSet tokenSet_6_ = new BitSet(TSql110LexerInternal.mk_tokenSet_6_());

		// Token: 0x0400070B RID: 1803
		public static readonly BitSet tokenSet_7_ = new BitSet(TSql110LexerInternal.mk_tokenSet_7_());

		// Token: 0x0400070C RID: 1804
		public static readonly BitSet tokenSet_8_ = new BitSet(TSql110LexerInternal.mk_tokenSet_8_());

		// Token: 0x0400070D RID: 1805
		public static readonly BitSet tokenSet_9_ = new BitSet(TSql110LexerInternal.mk_tokenSet_9_());

		// Token: 0x0400070E RID: 1806
		public static readonly BitSet tokenSet_10_ = new BitSet(TSql110LexerInternal.mk_tokenSet_10_());

		// Token: 0x0400070F RID: 1807
		public static readonly BitSet tokenSet_11_ = new BitSet(TSql110LexerInternal.mk_tokenSet_11_());

		// Token: 0x04000710 RID: 1808
		public static readonly BitSet tokenSet_12_ = new BitSet(TSql110LexerInternal.mk_tokenSet_12_());
	}
}
