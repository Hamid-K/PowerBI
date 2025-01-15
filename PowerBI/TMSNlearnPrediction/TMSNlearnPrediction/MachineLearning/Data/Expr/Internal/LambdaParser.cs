using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Internal.Lexer;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001B6 RID: 438
	internal sealed class LambdaParser
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x00032FA8 File Offset: 0x000311A8
		private LambdaParser()
		{
			this._pool = new NormStr.Pool();
			this._kwt = new KeyWordTable(this._pool);
			this.InitKeyWordTable();
			this._lex = new Lexer(this._pool, this._kwt);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00032FF4 File Offset: 0x000311F4
		private void InitKeyWordTable()
		{
			Action<string, TokKind> action = new Action<string, TokKind>(this._kwt.AddPunctuator);
			action("^", 24);
			action("*", 18);
			action("/", 20);
			action("%", 22);
			action("+", 11);
			action("-", 14);
			action("&&", 27);
			action("||", 30);
			action("!", 33);
			action("!=", 34);
			action("=", 35);
			action("==", 36);
			action("=>", 37);
			action("<", 38);
			action("<=", 40);
			action("<>", 41);
			action(">", 43);
			action(">=", 45);
			action(".", 49);
			action(",", 50);
			action(":", 51);
			action(";", 53);
			action("?", 47);
			action("??", 48);
			action("(", 55);
			action(")", 58);
			Action<string, TokKind> action2 = new Action<string, TokKind>(this._kwt.AddKeyWord);
			action2("false", 61);
			action2("true", 62);
			action2("not", 63);
			action2("and", 64);
			action2("or", 65);
			action2("with", 94);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000331C8 File Offset: 0x000313C8
		public static LambdaNode Parse(out List<Error> errors, out List<int> lineMap, CharCursor chars, int[] perm, params ColumnType[] types)
		{
			LambdaParser lambdaParser = new LambdaParser();
			return lambdaParser.ParseCore(out errors, out lineMap, chars, perm, types);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x000331E8 File Offset: 0x000313E8
		private LambdaNode ParseCore(out List<Error> errors, out List<int> lineMap, CharCursor chars, int[] perm, ColumnType[] types)
		{
			this._errors = null;
			this._lineMap = new List<int>();
			this._curs = new TokenCursor(this._lex.LexSource(chars));
			this._types = types;
			this._perm = perm;
			this.SkipJunk();
			LambdaNode lambdaNode = this.ParseLambda(this.TokCur);
			if (this.TidCur != 1)
			{
				this.PostError(this.TokCur, "Expected end of input");
			}
			errors = this._errors;
			lineMap = this._lineMap;
			this._errors = null;
			this._lineMap = null;
			this._curs = null;
			return lambdaNode;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00033284 File Offset: 0x00031484
		private void AddError(Error err)
		{
			if (Utils.Size<Error>(this._errors) > 0 && ListExtensions.Peek<Error>(this._errors).Token == err.Token)
			{
				return;
			}
			if (this._errors == null)
			{
				this._errors = new List<Error>();
			}
			this._errors.Add(err);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000332D8 File Offset: 0x000314D8
		private void PostError(Token tok, string msg)
		{
			Error error = new Error(tok, msg);
			this.AddError(error);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000332F4 File Offset: 0x000314F4
		private void PostError(Token tok, string msg, params object[] args)
		{
			Error error = new Error(tok, msg, args);
			this.AddError(error);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00033314 File Offset: 0x00031514
		private void PostTidError(Token tok, TokKind tidWanted)
		{
			this.PostError(tok, "Expected: '{0}', Found: '{1}'", new object[]
			{
				this.Stringize(tidWanted),
				this.Stringize(tok)
			});
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0003334C File Offset: 0x0003154C
		private string Stringize(Token tok)
		{
			TokKind kind = tok.Kind;
			if (kind == 60)
			{
				return tok.As<IdentToken>().Value;
			}
			return this.Stringize(tok.Kind);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00033380 File Offset: 0x00031580
		private string Stringize(TokKind tid)
		{
			if (this._mapTidStr == null)
			{
				this._mapTidStr = new Dictionary<TokKind, string>();
				foreach (KeyValuePair<NormStr, KeyWordTable.KeyWordKind> keyValuePair in this._kwt.KeyWords)
				{
					if (!keyValuePair.Value.IsContextKeyWord)
					{
						this._mapTidStr[keyValuePair.Value.Kind] = keyValuePair.Key;
					}
				}
				foreach (KeyValuePair<NormStr, TokKind> keyValuePair2 in this._kwt.Punctuators)
				{
					this._mapTidStr[keyValuePair2.Value] = keyValuePair2.Key;
				}
			}
			string text;
			if (this._mapTidStr.TryGetValue(tid, out text))
			{
				return text;
			}
			return string.Format("<{0}>", tid);
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00033494 File Offset: 0x00031694
		private TokKind TidCur
		{
			get
			{
				return this._curs.TidCur;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x000334A1 File Offset: 0x000316A1
		private TokKind CtxCur
		{
			get
			{
				return this._curs.CtxCur;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x000334AE File Offset: 0x000316AE
		private Token TokCur
		{
			get
			{
				return this._curs.TokCur;
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000334BC File Offset: 0x000316BC
		private Token TokPeek(int cv = 1)
		{
			int num = 0;
			Token token;
			for (;;)
			{
				token = this._curs.TokPeek(++num);
				TokKind kind = token.Kind;
				switch (kind)
				{
				case 2:
				case 3:
					break;
				default:
					switch (kind)
					{
					case 9:
					case 10:
						break;
					default:
						if (--cv <= 0)
						{
							return token;
						}
						break;
					}
					break;
				}
			}
			return token;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0003350E File Offset: 0x0003170E
		private TokKind TidPeek(int cv = 1)
		{
			return this.TokPeek(cv).Kind;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0003351C File Offset: 0x0003171C
		private TokKind TidNext()
		{
			this._curs.TidNext();
			this.SkipJunk();
			return this._curs.TidCur;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0003353C File Offset: 0x0003173C
		private void SkipJunk()
		{
			for (;;)
			{
				TokKind tidCur = this._curs.TidCur;
				switch (tidCur)
				{
				case 2:
				case 3:
					this.PostError(this._curs.TokCur, this._curs.TokCur.As<ErrorToken>().ToString());
					break;
				default:
					switch (tidCur)
					{
					case 9:
						goto IL_0076;
					case 10:
						this._lineMap.Add(this._curs.TokCur.Span.Lim);
						goto IL_0076;
					}
					return;
				}
				IL_0076:
				this._curs.TidNext();
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000335D0 File Offset: 0x000317D0
		private Token TokMove()
		{
			Token tokCur = this.TokCur;
			this.TidNext();
			return tokCur;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000335EC File Offset: 0x000317EC
		private bool EatTid(TokKind tid)
		{
			if (this.TidCur == tid || this.CtxCur == tid)
			{
				this.TidNext();
				return true;
			}
			this.PostTidError(this.TokCur, tid);
			return false;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00033617 File Offset: 0x00031817
		private Token TokEat(TokKind tid)
		{
			if (this.TidCur == tid)
			{
				return this.TokMove();
			}
			this.PostTidError(this.TokCur, tid);
			return null;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00033638 File Offset: 0x00031838
		private LambdaNode ParseLambda(Token tokFirst)
		{
			List<ParamNode> list = new List<ParamNode>();
			if (this.TidCur == 60)
			{
				list.Add(this.ParseParam(0));
			}
			else
			{
				this.EatTid(55);
				for (;;)
				{
					list.Add(this.ParseParam(list.Count));
					if (this.TidCur != 50)
					{
						break;
					}
					this.TidNext();
				}
				this.EatTid(58);
			}
			if (list.Count != this._types.Length)
			{
				this.PostError(tokFirst, "Wrong number of parameters, expected: {0}", new object[] { this._types.Length });
			}
			Token token = ((this.TidCur == 51) ? this.TokMove() : this.TokEat(37));
			ExprNode exprNode = this.ParseExpr();
			return new LambdaNode(token ?? tokFirst, list.ToArray(), exprNode);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00033704 File Offset: 0x00031904
		private ParamNode ParseParam(int index)
		{
			Token tokCur = this.TokCur;
			string text;
			if (tokCur.Kind == 60)
			{
				text = tokCur.As<IdentToken>().Value;
				this.TidNext();
			}
			else
			{
				this.PostTidError(this.TokCur, 60);
				text = "<missing>";
			}
			ColumnType columnType;
			if (index < this._types.Length)
			{
				columnType = this._types[index];
				int num = 0;
				while (this._perm[num] != index)
				{
					num++;
				}
				index = num;
			}
			else
			{
				this.PostError(tokCur, "Too many parameters, expected {0}", new object[] { this._types.Length });
				columnType = null;
			}
			ParamNode paramNode = new ParamNode(tokCur, text, index, columnType);
			if (paramNode.ExprType.Kind == ExprTypeKind.None)
			{
				this.PostError(tokCur, "Unsupported type");
			}
			return paramNode;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000337C7 File Offset: 0x000319C7
		private ExprNode ParseExpr()
		{
			return this.ParseExpr(Precedence.None);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000337D0 File Offset: 0x000319D0
		private ExprNode ParseExpr(Precedence precMin)
		{
			ExprNode exprNode = this.ParsePrimary();
			for (;;)
			{
				TokKind tidCur = this.TidCur;
				switch (tidCur)
				{
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
					goto IL_0296;
				case 9:
				case 10:
				case 12:
				case 13:
				case 15:
				case 16:
				case 17:
				case 19:
				case 21:
				case 23:
				case 25:
				case 26:
				case 28:
				case 29:
				case 31:
				case 32:
				case 33:
				case 37:
				case 39:
				case 42:
				case 44:
				case 46:
					return exprNode;
				case 11:
					if (precMin > Precedence.Add)
					{
						return exprNode;
					}
					exprNode = new BinaryOpNode(this.TokMove(), BinaryOp.Add, exprNode, this.ParseExpr(Precedence.Mul));
					continue;
				case 14:
					if (precMin > Precedence.Add)
					{
						return exprNode;
					}
					exprNode = new BinaryOpNode(this.TokMove(), BinaryOp.Sub, exprNode, this.ParseExpr(Precedence.Mul));
					continue;
				case 18:
					if (precMin > Precedence.Mul)
					{
						return exprNode;
					}
					exprNode = new BinaryOpNode(this.TokMove(), BinaryOp.Mul, exprNode, this.ParseExpr(Precedence.Error));
					continue;
				case 20:
					if (precMin > Precedence.Mul)
					{
						return exprNode;
					}
					exprNode = new BinaryOpNode(this.TokMove(), BinaryOp.Div, exprNode, this.ParseExpr(Precedence.Error));
					continue;
				case 22:
					if (precMin > Precedence.Mul)
					{
						return exprNode;
					}
					exprNode = new BinaryOpNode(this.TokMove(), BinaryOp.Mod, exprNode, this.ParseExpr(Precedence.Error));
					continue;
				case 24:
					exprNode = new BinaryOpNode(this.TokMove(), BinaryOp.Power, exprNode, this.ParseExpr(Precedence.PrefixUnary));
					continue;
				case 27:
					break;
				case 30:
					goto IL_01CA;
				case 34:
				case 41:
					if (precMin > Precedence.Compare)
					{
						return exprNode;
					}
					exprNode = this.ParseCompareExpr(exprNode, CompareOp.NotEqual, 41, 34);
					continue;
				case 35:
				case 36:
					if (precMin > Precedence.Compare)
					{
						return exprNode;
					}
					exprNode = this.ParseCompareExpr(exprNode, CompareOp.Equal, 35, 36);
					continue;
				case 38:
				case 40:
					if (precMin > Precedence.Compare)
					{
						return exprNode;
					}
					exprNode = this.ParseCompareExpr(exprNode, CompareOp.IncrChain, 40, 38);
					continue;
				case 43:
				case 45:
					if (precMin > Precedence.Compare)
					{
						return exprNode;
					}
					exprNode = this.ParseCompareExpr(exprNode, CompareOp.DecrChain, 45, 43);
					continue;
				case 47:
					if (precMin > Precedence.Conditional)
					{
						return exprNode;
					}
					exprNode = new ConditionalNode(this.TokMove(), exprNode, this.ParseExpr(), this.TokEat(51), this.ParseExpr());
					continue;
				case 48:
					if (precMin > Precedence.Coalesce)
					{
						return exprNode;
					}
					exprNode = new BinaryOpNode(this.TokMove(), BinaryOp.Coalesce, exprNode, this.ParseExpr(Precedence.Coalesce));
					continue;
				default:
					switch (tidCur)
					{
					case 61:
					case 62:
						goto IL_0296;
					case 64:
						goto IL_01AA;
					case 65:
						goto IL_01CA;
					}
					goto Block_2;
				}
				IL_01AA:
				if (precMin > Precedence.And)
				{
					return exprNode;
				}
				exprNode = new BinaryOpNode(this.TokMove(), BinaryOp.And, exprNode, this.ParseExpr(Precedence.Compare));
				continue;
				IL_01CA:
				if (precMin > Precedence.Or)
				{
					return exprNode;
				}
				exprNode = new BinaryOpNode(this.TokMove(), BinaryOp.Or, exprNode, this.ParseExpr(Precedence.And));
				continue;
				IL_0296:
				this.PostError(this.TokCur, "Operator expected");
				exprNode = new BinaryOpNode(this.TokCur, BinaryOp.Error, exprNode, this.ParseExpr(Precedence.Error));
			}
			Block_2:
			return exprNode;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00033AA4 File Offset: 0x00031CA4
		private ExprNode ParsePrimary()
		{
			TokKind tidCur = this.TidCur;
			if (tidCur <= 33)
			{
				switch (tidCur)
				{
				case 4:
				case 5:
				case 6:
					return new NumLitNode(this.TokMove().As<NumLitToken>());
				case 7:
				{
					IdentNode identNode = this.ParseIdent();
					this.TokMove();
					return identNode;
				}
				case 8:
					return new StrLitNode(this.TokMove().As<StrLitToken>());
				case 9:
				case 10:
				case 11:
				case 12:
				case 13:
					goto IL_013C;
				case 14:
					return new UnaryOpNode(this.TokMove(), UnaryOp.Minus, this.ParseExpr(Precedence.PrefixUnary));
				default:
					if (tidCur != 33)
					{
						goto IL_013C;
					}
					break;
				}
			}
			else
			{
				switch (tidCur)
				{
				case 55:
					return this.ParseParenExpr();
				case 56:
				case 57:
				case 58:
				case 59:
					goto IL_013C;
				case 60:
					if (this.TidPeek(1) == 55)
					{
						return this.ParseInvocation();
					}
					if (this.TidPeek(1) == 49 && this.TidPeek(2) == 60 && this.TidPeek(3) == 55)
					{
						return this.ParseInvocationWithNameSpace();
					}
					return this.ParseIdent();
				case 61:
				case 62:
					return new BoolLitNode(this.TokMove());
				case 63:
					break;
				default:
					if (tidCur != 94)
					{
						goto IL_013C;
					}
					return this.ParseWith(null);
				}
			}
			return new UnaryOpNode(this.TokMove(), UnaryOp.Not, this.ParseExpr(Precedence.PrefixUnary));
			IL_013C:
			return this.ParseIdent();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00033BF4 File Offset: 0x00031DF4
		private CompareNode ParseCompareExpr(ExprNode node, CompareOp op, TokKind tidLax, TokKind tidStrict)
		{
			Token tokCur = this.TokCur;
			List<Node> list = new List<Node>();
			List<Token> list2 = new List<Token>();
			list.Add(node);
			while (this.TidCur == tidLax || this.TidCur == tidStrict)
			{
				list2.Add(this.TokMove());
				list.Add(this.ParseExpr(Precedence.Concat));
			}
			switch (this.TidCur)
			{
			case 34:
			case 35:
			case 36:
			case 38:
			case 40:
			case 41:
			case 43:
			case 45:
				this.PostError(this.TokCur, "Mixed direction not allowed");
				break;
			}
			return new CompareNode(tokCur, op, new ListNode(tokCur, list.ToArray(), list2.ToArray()));
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00033CB5 File Offset: 0x00031EB5
		private IdentNode ParseIdent()
		{
			if (this.TidCur == 60)
			{
				return new IdentNode(this.TokMove().As<IdentToken>());
			}
			this.PostTidError(this.TokCur, 60);
			return new IdentNode(this.TokCur, "<missing>", true);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00033CF4 File Offset: 0x00031EF4
		private CallNode ParseInvocation()
		{
			NameNode nameNode = new NameNode(this.TokMove().As<IdentToken>());
			Token token = this.TokMove();
			return new CallNode(token, nameNode, this.ParseList(token, 58), this.TokEat(58));
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00033D34 File Offset: 0x00031F34
		private CallNode ParseInvocationWithNameSpace()
		{
			NameNode nameNode = new NameNode(this.TokMove().As<IdentToken>());
			Token token = this.TokMove();
			NameNode nameNode2 = new NameNode(this.TokMove().As<IdentToken>());
			Token token2 = this.TokMove();
			return new CallNode(token2, nameNode, token, nameNode2, this.ParseList(token2, 58), this.TokEat(58));
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00033D8C File Offset: 0x00031F8C
		private ExprNode ParseParenExpr()
		{
			this.TidNext();
			ExprNode exprNode = this.ParseExpr(Precedence.None);
			this.EatTid(58);
			return exprNode;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00033DB4 File Offset: 0x00031FB4
		private ListNode ParseList(Token tok, TokKind tidEmpty)
		{
			if (this.TidCur == tidEmpty)
			{
				return new ListNode(tok, new Node[0], null);
			}
			List<Token> list = null;
			List<Node> list2 = new List<Node>();
			for (;;)
			{
				list2.Add(this.ParseExpr());
				if (this.TidCur != 50)
				{
					break;
				}
				Utils.Add<Token>(ref list, this.TokMove());
			}
			return new ListNode(tok, list2.ToArray(), Utils.ToArray<Token>(list));
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00033E18 File Offset: 0x00032018
		private WithNode ParseWith(Token tokWith = null)
		{
			Token token;
			if (tokWith == null)
			{
				tokWith = this.TokMove();
				token = tokWith;
				Token tokCur = this.TokCur;
				this.EatTid(55);
			}
			else
			{
				token = this.TokMove();
			}
			WithLocalNode withLocalNode = this.ParseWithLocal();
			ExprNode exprNode;
			if (this.TidCur == 50)
			{
				exprNode = this.ParseWith(tokWith);
			}
			else
			{
				this.EatTid(53);
				exprNode = this.ParseExpr();
				this.EatTid(58);
			}
			return new WithNode(token, withLocalNode, exprNode);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00033E88 File Offset: 0x00032088
		private WithLocalNode ParseWithLocal()
		{
			Token token = this.TokCur;
			string text;
			if (token.Kind == 60)
			{
				text = token.As<IdentToken>().Value;
				this.TidNext();
			}
			else
			{
				this.PostTidError(this.TokCur, 60);
				text = "<missing>";
			}
			if (this.TidCur == 35)
			{
				token = this.TokCur;
			}
			this.EatTid(35);
			ExprNode exprNode = this.ParseExpr();
			return new WithLocalNode(token, text, exprNode);
		}

		// Token: 0x04000505 RID: 1285
		private readonly NormStr.Pool _pool;

		// Token: 0x04000506 RID: 1286
		private readonly KeyWordTable _kwt;

		// Token: 0x04000507 RID: 1287
		private readonly Lexer _lex;

		// Token: 0x04000508 RID: 1288
		private Dictionary<TokKind, string> _mapTidStr;

		// Token: 0x04000509 RID: 1289
		private int[] _perm;

		// Token: 0x0400050A RID: 1290
		private ColumnType[] _types;

		// Token: 0x0400050B RID: 1291
		private TokenCursor _curs;

		// Token: 0x0400050C RID: 1292
		private List<Error> _errors;

		// Token: 0x0400050D RID: 1293
		private List<int> _lineMap;

		// Token: 0x020001B7 RID: 439
		public struct SourcePos
		{
			// Token: 0x060009BD RID: 2493 RVA: 0x00033EF8 File Offset: 0x000320F8
			public SourcePos(List<int> lineMap, TextSpan span, int lineMin = 1)
			{
				this.IchMin = span.Min;
				this.IchLim = span.Lim;
				if (Utils.Size<int>(lineMap) == 0)
				{
					this.LineMin = lineMin;
					this.ColumnMin = this.IchMin + 1;
					this.LineLim = lineMin;
					this.ColumnLim = this.IchLim + 1;
					return;
				}
				int num = LambdaParser.SourcePos.FindIndex(lineMap, this.IchMin, 0);
				this.LineMin = num + lineMin;
				int num2 = ((num == 0) ? 0 : lineMap[num - 1]);
				this.ColumnMin = this.IchMin - num2 + 1;
				if (num == lineMap.Count || this.IchLim < lineMap[num])
				{
					this.LineLim = this.LineMin;
					this.ColumnLim = this.IchLim - num2 + 1;
					return;
				}
				num = LambdaParser.SourcePos.FindIndex(lineMap, this.IchLim, num);
				num2 = lineMap[num - 1];
				this.LineLim = num + lineMin;
				this.ColumnLim = this.IchLim - num2 + 1;
			}

			// Token: 0x060009BE RID: 2494 RVA: 0x00033FF0 File Offset: 0x000321F0
			private static int FindIndex(List<int> map, int value, int ivMin)
			{
				int num = map.Count;
				while (ivMin < num)
				{
					int num2 = (ivMin + num) / 2;
					if (value >= map[num2])
					{
						ivMin = num2 + 1;
					}
					else
					{
						num = num2;
					}
				}
				return ivMin;
			}

			// Token: 0x0400050E RID: 1294
			public readonly int IchMin;

			// Token: 0x0400050F RID: 1295
			public readonly int IchLim;

			// Token: 0x04000510 RID: 1296
			public readonly int LineMin;

			// Token: 0x04000511 RID: 1297
			public readonly int ColumnMin;

			// Token: 0x04000512 RID: 1298
			public readonly int LineLim;

			// Token: 0x04000513 RID: 1299
			public readonly int ColumnLim;
		}
	}
}
