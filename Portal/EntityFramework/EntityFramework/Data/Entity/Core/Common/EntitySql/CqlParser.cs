using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Resources;
using System.Globalization;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000647 RID: 1607
	internal sealed class CqlParser
	{
		// Token: 0x06004D9E RID: 19870 RVA: 0x00112D40 File Offset: 0x00110F40
		private void debug(string msg)
		{
			if (this.yydebug)
			{
				Console.WriteLine(msg);
			}
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x00112D50 File Offset: 0x00110F50
		private void state_push(int state)
		{
			if (this.stateptr >= CqlParser.YYSTACKSIZE - 1)
			{
				this.yyerror_stackoverflow();
			}
			int[] array = this.statestk;
			int num = this.stateptr + 1;
			this.stateptr = num;
			array[num] = state;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x00112D8C File Offset: 0x00110F8C
		private int state_pop()
		{
			if (this.stateptr < 0)
			{
				return -1;
			}
			int[] array = this.statestk;
			int num = this.stateptr;
			this.stateptr = num - 1;
			return array[num];
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x00112DBC File Offset: 0x00110FBC
		private void state_drop(int cnt)
		{
			int num = this.stateptr - cnt;
			if (num < 0)
			{
				return;
			}
			this.stateptr = num;
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x00112DE0 File Offset: 0x00110FE0
		private int state_peek(int relative)
		{
			int num = this.stateptr - relative;
			if (num < 0)
			{
				return -1;
			}
			return this.statestk[num];
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x00112E04 File Offset: 0x00111004
		private bool init_stacks()
		{
			this.statestk = new int[CqlParser.YYSTACKSIZE];
			this.stateptr = -1;
			this.val_init();
			return true;
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x00112E24 File Offset: 0x00111024
		private void dump_stacks(int count)
		{
			Console.WriteLine("=index==state====value=     s:" + this.stateptr.ToString() + "  v:" + this.valptr.ToString());
			for (int i = 0; i < count; i++)
			{
				string[] array = new string[6];
				array[0] = " ";
				array[1] = i.ToString();
				array[2] = "    ";
				array[3] = this.statestk[i].ToString();
				array[4] = "      ";
				int num = 5;
				object obj = this.valstk[i];
				array[num] = ((obj != null) ? obj.ToString() : null);
				Console.WriteLine(string.Concat(array));
			}
			Console.WriteLine("======================");
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x00112ECE File Offset: 0x001110CE
		private void val_init()
		{
			this.valstk = new object[CqlParser.YYSTACKSIZE];
			this.yyval = 0;
			this.yylval = 0;
			this.valptr = -1;
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x00112F00 File Offset: 0x00111100
		private void val_push(object val)
		{
			if (this.valptr >= CqlParser.YYSTACKSIZE)
			{
				return;
			}
			object[] array = this.valstk;
			int num = this.valptr + 1;
			this.valptr = num;
			array[num] = val;
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x00112F34 File Offset: 0x00111134
		private object val_pop()
		{
			if (this.valptr < 0)
			{
				return -1;
			}
			object[] array = this.valstk;
			int num = this.valptr;
			this.valptr = num - 1;
			return array[num];
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x00112F6C File Offset: 0x0011116C
		private void val_drop(int cnt)
		{
			int num = this.valptr - cnt;
			if (num < 0)
			{
				return;
			}
			this.valptr = num;
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x00112F90 File Offset: 0x00111190
		private object val_peek(int relative)
		{
			int num = this.valptr - relative;
			if (num < 0)
			{
				return -1;
			}
			return this.valstk[num];
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x00112FBC File Offset: 0x001111BC
		private void yylexdebug(int state, int ch)
		{
			string text = null;
			if (ch < 0)
			{
				ch = 0;
			}
			if (ch <= (int)CqlParser.YYMAXTOKEN)
			{
				text = CqlParser.yyname[ch];
			}
			if (text == null)
			{
				text = "illegal-symbol";
			}
			this.debug(string.Concat(new string[]
			{
				"state ",
				state.ToString(),
				", reading ",
				ch.ToString(),
				" (",
				text,
				")"
			}));
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x00113034 File Offset: 0x00111234
		private int yyparse()
		{
			this.init_stacks();
			this.yynerrs = 0;
			this.yyerrflag = 0;
			this.yychar = -1;
			int num = 0;
			this.state_push(num);
			Identifier identifier;
			Identifier identifier3;
			for (;;)
			{
				IL_0025:
				int num2 = (int)CqlParser.yydefred[num];
				if (num2 == 0)
				{
					if (this.yychar < 0)
					{
						this.yychar = (int)this.yylex();
						if (this.yychar < 0)
						{
							this.yychar = 0;
						}
					}
					num2 = (int)CqlParser.yysindex[num];
					if (num2 != 0 && (num2 += this.yychar) >= 0 && num2 <= CqlParser.YYTABLESIZE && (int)CqlParser.yycheck[num2] == this.yychar)
					{
						num = (int)CqlParser.yytable[num2];
						this.state_push(num);
						this.val_push(this.yylval);
						this.yychar = -1;
						if (this.yyerrflag > 0)
						{
							this.yyerrflag--;
							continue;
						}
						continue;
					}
					else
					{
						num2 = (int)CqlParser.yyrindex[num];
						if (num2 != 0 && (num2 += this.yychar) >= 0 && num2 <= CqlParser.YYTABLESIZE && (int)CqlParser.yycheck[num2] == this.yychar)
						{
							num2 = (int)CqlParser.yytable[num2];
						}
						else
						{
							if (this.yyerrflag == 0)
							{
								this.yyerror("syntax error");
								this.yynerrs++;
							}
							if (this.yyerrflag < 3)
							{
								this.yyerrflag = 3;
								while (this.stateptr >= 0)
								{
									num2 = (int)CqlParser.yysindex[this.state_peek(0)];
									if (num2 != 0 && (num2 += (int)CqlParser.YYERRCODE) >= 0 && num2 <= CqlParser.YYTABLESIZE && CqlParser.yycheck[num2] == CqlParser.YYERRCODE)
									{
										if (this.stateptr >= 0)
										{
											num = (int)CqlParser.yytable[num2];
											this.state_push(num);
											this.val_push(this.yylval);
											goto IL_0025;
										}
										break;
									}
									else
									{
										if (this.stateptr < 0)
										{
											break;
										}
										this.state_pop();
										this.val_pop();
									}
								}
								goto IL_3754;
							}
							if (this.yychar != 0)
							{
								this.yychar = -1;
								continue;
							}
							return 1;
						}
					}
				}
				int num3 = (int)CqlParser.yylen[num2];
				this.yyval = this.val_peek(num3 - 1);
				switch (num2)
				{
				case 1:
					this.yyval = (this._parsedTree = null);
					break;
				case 2:
					this.yyval = (this._parsedTree = (Node)this.val_peek(0));
					break;
				case 3:
					this.yyval = new Command(CqlParser.ToNodeList<NamespaceImport>(this.val_peek(1)), (Statement)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), (this.val_peek(1) != null) ? CqlParser.AstNodePos(this.val_peek(1)) : CqlParser.AstNodePos(this.val_peek(0)), "CtxCommandExpression");
					break;
				case 4:
					this.yyval = null;
					break;
				case 5:
					this.yyval = this.val_peek(0);
					break;
				case 6:
					this.yyval = new NodeList<NamespaceImport>((NamespaceImport)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxNamespaceImportList");
					break;
				case 7:
					this.yyval = CqlParser.ToNodeList<NamespaceImport>(this.val_peek(1)).Add((NamespaceImport)this.val_peek(0));
					break;
				case 8:
					this.yyval = new NamespaceImport((Identifier)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxNamespaceImport");
					break;
				case 9:
					this.yyval = new NamespaceImport((DotExpr)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxNamespaceImport");
					break;
				case 10:
					this.yyval = new NamespaceImport((BuiltInExpr)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxAliasedNamespaceImport");
					break;
				case 11:
					this.yyval = new QueryStatement(CqlParser.ToNodeList<FunctionDefinition>(this.val_peek(2)), (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), (this.val_peek(2) != null) ? CqlParser.AstNodePos(this.val_peek(2)) : CqlParser.AstNodePos(this.val_peek(1)), "CtxQueryStatement");
					break;
				case 12:
					this.yyval = null;
					break;
				case 13:
					this.yyval = this.val_peek(0);
					break;
				case 14:
					this.yyval = new NodeList<FunctionDefinition>((FunctionDefinition)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 15:
					this.yyval = CqlParser.ToNodeList<FunctionDefinition>(this.val_peek(1)).Add((FunctionDefinition)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.yyval), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 16:
					this.yyval = new FunctionDefinition((Identifier)this.val_peek(5), CqlParser.ToNodeList<PropDefinition>(this.val_peek(4)), (Node)this.val_peek(1), CqlParser.Terminal(this.val_peek(6)).IPos, CqlParser.Terminal(this.val_peek(0)).IPos);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(6)), "CtxFunctionDefinition");
					break;
				case 17:
					this.yyval = null;
					break;
				case 18:
					this.yyval = this.val_peek(1);
					break;
				case 19:
					this.yyval = new NodeList<PropDefinition>((PropDefinition)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 20:
					this.yyval = CqlParser.ToNodeList<PropDefinition>(this.val_peek(2)).Add((PropDefinition)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.yyval), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 21:
					this.yyval = new PropDefinition((Identifier)this.val_peek(1), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(1)), "CtxFunctionDefinition");
					break;
				case 22:
					this.yyval = this.val_peek(0);
					break;
				case 23:
					this.yyval = this.val_peek(0);
					break;
				case 24:
					this.yyval = null;
					break;
				case 25:
					this.yyval = null;
					break;
				case 26:
					this.yyval = new QueryExpr((SelectClause)this.val_peek(5), (FromClause)this.val_peek(4), (Node)this.val_peek(3), (GroupByClause)this.val_peek(2), (HavingClause)this.val_peek(1), (OrderByClause)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(5)), "CtxQueryExpression");
					break;
				case 27:
					this.StartMethodExprCounting();
					break;
				case 28:
					this.yyval = new SelectClause(CqlParser.ToNodeList<AliasedExpr>(this.val_peek(0)), SelectKind.Row, (DistinctKind)this.val_peek(2), (Node)this.val_peek(1), this.EndMethodExprCounting());
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(4)), "CtxSelectRowClause");
					break;
				case 29:
					this.StartMethodExprCounting();
					break;
				case 30:
					this.yyval = new SelectClause(CqlParser.ToNodeList<AliasedExpr>(this.val_peek(0)), SelectKind.Value, (DistinctKind)this.val_peek(2), (Node)this.val_peek(1), this.EndMethodExprCounting());
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxSelectValueClause");
					break;
				case 31:
					this.yyval = DistinctKind.None;
					break;
				case 32:
					this.yyval = DistinctKind.All;
					break;
				case 33:
					this.yyval = DistinctKind.Distinct;
					break;
				case 34:
					this.yyval = null;
					break;
				case 35:
					this.yyval = this.val_peek(1);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxTopSubClause");
					break;
				case 36:
					this.yyval = new FromClause(CqlParser.ToNodeList<FromClauseItem>(this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxFromClause");
					break;
				case 37:
					this.yyval = new NodeList<FromClauseItem>((FromClauseItem)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 38:
					this.yyval = CqlParser.ToNodeList<FromClauseItem>(this.val_peek(2)).Add((FromClauseItem)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(2)), "CtxFromClauseList");
					break;
				case 39:
					this.yyval = new FromClauseItem((AliasedExpr)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxFromClauseItem");
					break;
				case 40:
					this.yyval = new FromClauseItem((JoinClauseItem)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(1)), "CtxFromJoinClause");
					break;
				case 41:
					this.yyval = new FromClauseItem((JoinClauseItem)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxFromJoinClause");
					break;
				case 42:
					this.yyval = new FromClauseItem((ApplyClauseItem)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(1)), "CtxFromApplyClause");
					break;
				case 43:
					this.yyval = new FromClauseItem((ApplyClauseItem)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxFromApplyClause");
					break;
				case 44:
					this.yyval = new JoinClauseItem((FromClauseItem)this.val_peek(2), (FromClauseItem)this.val_peek(0), (JoinKind)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(2)), "CtxJoinClause");
					break;
				case 45:
					this.yyval = new JoinClauseItem((FromClauseItem)this.val_peek(4), (FromClauseItem)this.val_peek(2), (JoinKind)this.val_peek(3), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(4)), "CtxJoinOnClause");
					break;
				case 46:
					this.yyval = new ApplyClauseItem((FromClauseItem)this.val_peek(2), (FromClauseItem)this.val_peek(0), (ApplyKind)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(2)), "CtxApplyClause");
					break;
				case 47:
					this.yyval = JoinKind.Cross;
					break;
				case 48:
					this.yyval = JoinKind.LeftOuter;
					break;
				case 49:
					this.yyval = JoinKind.LeftOuter;
					break;
				case 50:
					this.yyval = JoinKind.RightOuter;
					break;
				case 51:
					this.yyval = JoinKind.RightOuter;
					break;
				case 52:
					this.yyval = JoinKind.Inner;
					break;
				case 53:
					this.yyval = JoinKind.Inner;
					break;
				case 54:
					this.yyval = JoinKind.FullOuter;
					break;
				case 55:
					this.yyval = JoinKind.FullOuter;
					break;
				case 56:
					this.yyval = JoinKind.FullOuter;
					break;
				case 57:
					this.yyval = ApplyKind.Cross;
					break;
				case 58:
					this.yyval = ApplyKind.Outer;
					break;
				case 59:
					this.yyval = null;
					break;
				case 60:
					this.yyval = this.val_peek(0);
					break;
				case 61:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxWhereClause");
					break;
				case 62:
					this.yyval = null;
					break;
				case 63:
					this.yyval = this.val_peek(0);
					break;
				case 64:
					this.yyval = new GroupByClause(CqlParser.ToNodeList<AliasedExpr>(this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxGroupByClause");
					break;
				case 65:
					this.yyval = null;
					break;
				case 66:
					this.yyval = this.val_peek(0);
					break;
				case 67:
					this.StartMethodExprCounting();
					break;
				case 68:
					this.yyval = new HavingClause((Node)this.val_peek(0), this.EndMethodExprCounting());
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxHavingClause");
					break;
				case 69:
					this.yyval = null;
					break;
				case 70:
					this.yyval = this.val_peek(0);
					break;
				case 71:
					this.StartMethodExprCounting();
					break;
				case 72:
					this.yyval = new OrderByClause(CqlParser.ToNodeList<OrderByClauseItem>(this.val_peek(2)), (Node)this.val_peek(1), (Node)this.val_peek(0), this.EndMethodExprCounting());
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxOrderByClauseItem");
					break;
				case 73:
					this.yyval = null;
					break;
				case 74:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxSkipSubClause");
					break;
				case 75:
					this.yyval = null;
					break;
				case 76:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxLimitSubClause");
					break;
				case 77:
					this.yyval = new NodeList<OrderByClauseItem>((OrderByClauseItem)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 78:
					this.yyval = CqlParser.ToNodeList<OrderByClauseItem>(this.val_peek(2)).Add((OrderByClauseItem)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(2)), "CtxOrderByClause");
					break;
				case 79:
					this.yyval = new OrderByClauseItem((Node)this.val_peek(1), (OrderKind)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(1)), "CtxOrderByClauseItem");
					break;
				case 80:
					this.yyval = new OrderByClauseItem((Node)this.val_peek(3), (OrderKind)this.val_peek(0), (Identifier)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(3)), "CtxCollatedOrderByClauseItem");
					break;
				case 81:
					this.yyval = OrderKind.None;
					break;
				case 82:
					this.yyval = OrderKind.Asc;
					break;
				case 83:
					this.yyval = OrderKind.Desc;
					break;
				case 84:
					this.yyval = new NodeList<Node>((Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 85:
					this.yyval = CqlParser.ToNodeList<Node>(this.val_peek(2)).Add((Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(2)), "CtxExpressionList");
					break;
				case 86:
					this.yyval = this.val_peek(0);
					break;
				case 87:
					this.yyval = this.val_peek(0);
					break;
				case 88:
					this.yyval = this.val_peek(0);
					break;
				case 89:
					this.yyval = this.val_peek(0);
					break;
				case 90:
					this.yyval = this.val_peek(0);
					break;
				case 91:
					this.yyval = this.val_peek(0);
					break;
				case 92:
					this.yyval = this.val_peek(0);
					break;
				case 93:
					this.yyval = this.val_peek(0);
					break;
				case 94:
					this.yyval = this.val_peek(0);
					this.IncrementMethodExprCount();
					break;
				case 95:
					this.yyval = this.val_peek(0);
					this.IncrementMethodExprCount();
					break;
				case 96:
					this.yyval = this.val_peek(0);
					break;
				case 97:
					this.yyval = this.val_peek(0);
					break;
				case 98:
					this.yyval = this.val_peek(0);
					break;
				case 99:
					this.yyval = this.val_peek(0);
					break;
				case 100:
					this.yyval = new ParenExpr((Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxParen");
					break;
				case 101:
					this.yyval = new NodeList<Node>((Node)this.val_peek(2)).Add((Node)this.val_peek(0));
					break;
				case 102:
					this.yyval = new NodeList<Node>((Node)this.val_peek(3)).Add((Node)this.val_peek(0));
					break;
				case 103:
					this.yyval = new BuiltInExpr(BuiltInKind.Plus, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxPlus");
					break;
				case 104:
					this.yyval = new BuiltInExpr(BuiltInKind.Minus, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxMinus");
					break;
				case 105:
					this.yyval = new BuiltInExpr(BuiltInKind.Multiply, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxMultiply");
					break;
				case 106:
					this.yyval = new BuiltInExpr(BuiltInKind.Divide, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxDivide");
					break;
				case 107:
					this.yyval = new BuiltInExpr(BuiltInKind.Modulus, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxModulus");
					break;
				case 108:
				{
					Literal literal = this.val_peek(0) as Literal;
					if (literal != null && literal.IsNumber && !literal.IsSignedNumber)
					{
						literal.PrefixSign(CqlParser.Terminal(this.val_peek(1)).Token);
						this.yyval = this.val_peek(0);
					}
					else
					{
						this.yyval = new BuiltInExpr(BuiltInKind.UnaryMinus, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(0));
						this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxUnaryMinus");
					}
					break;
				}
				case 109:
				{
					Literal literal2 = this.val_peek(0) as Literal;
					if (literal2 != null && literal2.IsNumber && !literal2.IsSignedNumber)
					{
						literal2.PrefixSign(CqlParser.Terminal(this.val_peek(1)).Token);
						this.yyval = this.val_peek(0);
					}
					else
					{
						this.yyval = new BuiltInExpr(BuiltInKind.UnaryPlus, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(0));
						this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxUnaryPlus");
					}
					break;
				}
				case 110:
					this.yyval = new BuiltInExpr(BuiltInKind.NotEqual, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxNotEqual");
					break;
				case 111:
					this.yyval = new BuiltInExpr(BuiltInKind.GreaterThan, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxGreaterThan");
					break;
				case 112:
					this.yyval = new BuiltInExpr(BuiltInKind.GreaterEqual, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxGreaterThanEqual");
					break;
				case 113:
					this.yyval = new BuiltInExpr(BuiltInKind.LessThan, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxLessThan");
					break;
				case 114:
					this.yyval = new BuiltInExpr(BuiltInKind.LessEqual, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxLessThanEqual");
					break;
				case 115:
					this.yyval = new BuiltInExpr(BuiltInKind.Intersect, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxIntersect");
					break;
				case 116:
					this.yyval = new BuiltInExpr(BuiltInKind.Union, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxUnion");
					break;
				case 117:
					this.yyval = new BuiltInExpr(BuiltInKind.UnionAll, CqlParser.Terminal(this.val_peek(2)).Token, (Node)this.val_peek(3), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxUnionAll");
					break;
				case 118:
					this.yyval = new BuiltInExpr(BuiltInKind.Except, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxExcept");
					break;
				case 119:
					this.yyval = new BuiltInExpr(BuiltInKind.Overlaps, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxOverlaps");
					break;
				case 120:
					this.yyval = new BuiltInExpr(BuiltInKind.In, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxIn");
					break;
				case 121:
					this.yyval = new BuiltInExpr(BuiltInKind.NotIn, CqlParser.Terminal(this.val_peek(2)).Token, (Node)this.val_peek(3), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxNotIn");
					break;
				case 122:
					this.yyval = new BuiltInExpr(BuiltInKind.Exists, CqlParser.Terminal(this.val_peek(3)).Token, (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxExists");
					break;
				case 123:
					this.yyval = new BuiltInExpr(BuiltInKind.AnyElement, CqlParser.Terminal(this.val_peek(3)).Token, (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxAnyElement");
					break;
				case 124:
					this.yyval = new BuiltInExpr(BuiltInKind.Element, CqlParser.Terminal(this.val_peek(3)).Token, (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxElement");
					break;
				case 125:
					this.yyval = new BuiltInExpr(BuiltInKind.Flatten, CqlParser.Terminal(this.val_peek(3)).Token, (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxFlatten");
					break;
				case 126:
					this.yyval = new BuiltInExpr(BuiltInKind.Distinct, CqlParser.Terminal(this.val_peek(3)).Token, (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxSet");
					break;
				case 127:
					this.yyval = new BuiltInExpr(BuiltInKind.IsNull, "IsNull", (Node)this.val_peek(2));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxIsNull");
					break;
				case 128:
					this.yyval = new BuiltInExpr(BuiltInKind.IsNotNull, "IsNotNull", (Node)this.val_peek(3));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxIsNotNull");
					break;
				case 129:
					this.yyval = this.val_peek(0);
					break;
				case 130:
					this.yyval = new BuiltInExpr(BuiltInKind.Treat, CqlParser.Terminal(this.val_peek(5)).Token, (Node)this.val_peek(3), (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxTreat");
					break;
				case 131:
					this.yyval = new BuiltInExpr(BuiltInKind.Cast, CqlParser.Terminal(this.val_peek(5)).Token, (Node)this.val_peek(3), (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxCast");
					break;
				case 132:
					this.yyval = new BuiltInExpr(BuiltInKind.OfType, CqlParser.Terminal(this.val_peek(5)).Token, (Node)this.val_peek(3), (Node)this.val_peek(1), Literal.NewBooleanLiteral(false));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxOfType");
					break;
				case 133:
					this.yyval = new BuiltInExpr(BuiltInKind.OfType, "OFTYPE ONLY", (Node)this.val_peek(4), (Node)this.val_peek(1), Literal.NewBooleanLiteral(true));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(6)), "CtxOfTypeOnly");
					break;
				case 134:
					this.yyval = new BuiltInExpr(BuiltInKind.IsOf, "IS OF", (Node)this.val_peek(5), (Node)this.val_peek(1), Literal.NewBooleanLiteral(false), Literal.NewBooleanLiteral(false));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(4)), "CtxIsOf");
					break;
				case 135:
					this.yyval = new BuiltInExpr(BuiltInKind.IsOf, "IS NOT OF", (Node)this.val_peek(6), (Node)this.val_peek(1), Literal.NewBooleanLiteral(false), Literal.NewBooleanLiteral(true));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxIsNotOf");
					break;
				case 136:
					this.yyval = new BuiltInExpr(BuiltInKind.IsOf, "IS OF ONLY", (Node)this.val_peek(6), (Node)this.val_peek(1), Literal.NewBooleanLiteral(true), Literal.NewBooleanLiteral(false));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxIsOf");
					break;
				case 137:
					this.yyval = new BuiltInExpr(BuiltInKind.IsOf, "IS NOT OF ONLY", (Node)this.val_peek(7), (Node)this.val_peek(1), Literal.NewBooleanLiteral(true), Literal.NewBooleanLiteral(true));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(6)), "CtxIsNotOf");
					break;
				case 138:
					this.yyval = new BuiltInExpr(BuiltInKind.Like, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxLike");
					break;
				case 139:
					this.yyval = new BuiltInExpr(BuiltInKind.Not, CqlParser.Terminal(this.val_peek(2)).Token, new BuiltInExpr(BuiltInKind.Like, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(3), (Node)this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxNotLike");
					break;
				case 140:
					this.yyval = new BuiltInExpr(BuiltInKind.Like, CqlParser.Terminal(this.val_peek(3)).Token, (Node)this.val_peek(4), (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxLike");
					break;
				case 141:
					this.yyval = new BuiltInExpr(BuiltInKind.Not, CqlParser.Terminal(this.val_peek(4)).Token, new BuiltInExpr(BuiltInKind.Like, CqlParser.Terminal(this.val_peek(3)).Token, (Node)this.val_peek(5), (Node)this.val_peek(2), (Node)this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(4)), "CtxNotLike");
					break;
				case 142:
				{
					NodeList<Node> nodeList = (NodeList<Node>)this.val_peek(2);
					this.yyval = new BuiltInExpr(BuiltInKind.Between, "between", nodeList[0], nodeList[1], (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxBetween");
					break;
				}
				case 143:
				{
					NodeList<Node> nodeList2 = (NodeList<Node>)this.val_peek(2);
					this.yyval = new BuiltInExpr(BuiltInKind.NotBetween, "notbetween", nodeList2[0], nodeList2[1], (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxNotBetween");
					break;
				}
				case 144:
					this.yyval = new BuiltInExpr(BuiltInKind.Or, "or", (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxOr");
					break;
				case 145:
					this.yyval = new BuiltInExpr(BuiltInKind.Not, "not", (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxNot");
					break;
				case 146:
					this.yyval = new BuiltInExpr(BuiltInKind.And, "and", (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxAnd");
					break;
				case 147:
					this.yyval = this.val_peek(0);
					break;
				case 148:
					this.yyval = this.val_peek(0);
					break;
				case 149:
					this.yyval = this.val_peek(0);
					break;
				case 150:
					this.yyval = new BuiltInExpr(BuiltInKind.Equal, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxEquals");
					break;
				case 151:
					this.yyval = new BuiltInExpr(BuiltInKind.Equal, CqlParser.Terminal(this.val_peek(1)).Token, (Node)this.val_peek(2), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxEquals");
					break;
				case 152:
					this.yyval = new AliasedExpr((Node)this.val_peek(2), (Identifier)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxAlias");
					break;
				case 153:
					this.yyval = new AliasedExpr((Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 154:
					this.yyval = new NodeList<AliasedExpr>((AliasedExpr)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 155:
					this.yyval = CqlParser.ToNodeList<AliasedExpr>(this.val_peek(2)).Add((AliasedExpr)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.yyval), "CtxExpressionList");
					break;
				case 156:
					this.yyval = new CaseExpr(CqlParser.ToNodeList<WhenThenExpr>(this.val_peek(1)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxCase");
					break;
				case 157:
					this.yyval = new CaseExpr(CqlParser.ToNodeList<WhenThenExpr>(this.val_peek(2)), (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxCase");
					break;
				case 158:
					this.yyval = new NodeList<WhenThenExpr>(new WhenThenExpr((Node)this.val_peek(2), (Node)this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxCaseWhenThen");
					break;
				case 159:
					this.yyval = CqlParser.ToNodeList<WhenThenExpr>(this.val_peek(4)).Add(new WhenThenExpr((Node)this.val_peek(2), (Node)this.val_peek(0)));
					break;
				case 160:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxCaseElse");
					break;
				case 161:
					this.yyval = new RowConstructorExpr(CqlParser.ToNodeList<AliasedExpr>(this.val_peek(1)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxRowCtor");
					break;
				case 162:
					this.yyval = new MultisetConstructorExpr(CqlParser.ToNodeList<Node>(this.val_peek(1)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxMultisetCtor");
					break;
				case 163:
					this.yyval = new MultisetConstructorExpr(CqlParser.ToNodeList<Node>(this.val_peek(1)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(2)), "CtxMultisetCtor");
					break;
				case 164:
					this.yyval = new DotExpr((Node)this.val_peek(2), (Identifier)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(1)), "CtxMemberAccess");
					break;
				case 165:
					this.yyval = new RefExpr((Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxRef");
					break;
				case 166:
					this.yyval = new DerefExpr((Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxDeref");
					break;
				case 167:
					this.yyval = new CreateRefExpr((Node)this.val_peek(3), (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxCreateRef");
					break;
				case 168:
					this.yyval = new CreateRefExpr((Node)this.val_peek(5), (Node)this.val_peek(3), (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(7)), "CtxCreateRef");
					break;
				case 169:
					this.yyval = new KeyExpr((Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxKey");
					break;
				case 170:
					this.yyval = new GroupPartitionExpr((DistinctKind)this.val_peek(2), (Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(4)), "CtxGroupPartition");
					break;
				case 171:
					this.yyval = new MethodExpr((Node)this.val_peek(2), DistinctKind.None, null);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(((DotExpr)this.val_peek(2)).Identifier), "CtxMethod");
					break;
				case 172:
					this.yyval = new MethodExpr((Node)this.val_peek(5), (DistinctKind)this.val_peek(3), CqlParser.ToNodeList<Node>(this.val_peek(2)), CqlParser.ToNodeList<RelshipNavigationExpr>(this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(((DotExpr)this.val_peek(5)).Identifier), "CtxMethod");
					break;
				case 173:
					this.yyval = new MethodExpr((Node)this.val_peek(5), (DistinctKind)this.val_peek(3), new NodeList<Node>((Node)this.val_peek(2)), CqlParser.ToNodeList<RelshipNavigationExpr>(this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(((DotExpr)this.val_peek(5)).Identifier), "CtxMethod");
					break;
				case 174:
					this.yyval = new MethodExpr((Identifier)this.val_peek(2), DistinctKind.None, null);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(2)), "CtxMethod");
					break;
				case 175:
					this.yyval = new MethodExpr((Identifier)this.val_peek(5), (DistinctKind)this.val_peek(3), CqlParser.ToNodeList<Node>(this.val_peek(2)), CqlParser.ToNodeList<RelshipNavigationExpr>(this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(5)), "CtxMethod");
					break;
				case 176:
					this.yyval = new MethodExpr((Identifier)this.val_peek(5), (DistinctKind)this.val_peek(3), new NodeList<Node>((Node)this.val_peek(2)), CqlParser.ToNodeList<RelshipNavigationExpr>(this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(5)), "CtxMethod");
					break;
				case 177:
					this.yyval = new RelshipNavigationExpr((Node)this.val_peek(3), (Node)this.val_peek(1), null, null);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxNavigate");
					break;
				case 178:
					this.yyval = new RelshipNavigationExpr((Node)this.val_peek(5), (Node)this.val_peek(3), (Identifier)this.val_peek(1), null);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(7)), "CtxNavigate");
					break;
				case 179:
					this.yyval = new RelshipNavigationExpr((Node)this.val_peek(7), (Node)this.val_peek(5), (Identifier)this.val_peek(3), (Identifier)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(9)), "CtxNavigate");
					break;
				case 180:
					this.yyval = null;
					break;
				case 181:
					this.yyval = this.val_peek(0);
					break;
				case 182:
					this.yyval = new NodeList<RelshipNavigationExpr>((RelshipNavigationExpr)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxRelationshipList");
					break;
				case 183:
					this.yyval = CqlParser.ToNodeList<RelshipNavigationExpr>(this.val_peek(1)).Add((RelshipNavigationExpr)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(1)), "CtxRelationshipList");
					break;
				case 184:
					this.yyval = new RelshipNavigationExpr((Node)this.val_peek(3), (Node)this.val_peek(1), null, null);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(5)), "CtxRelationship");
					break;
				case 185:
					this.yyval = new RelshipNavigationExpr((Node)this.val_peek(5), (Node)this.val_peek(3), null, (Identifier)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(7)), "CtxRelationship");
					break;
				case 186:
					this.yyval = new RelshipNavigationExpr((Node)this.val_peek(7), (Node)this.val_peek(5), (Identifier)this.val_peek(1), (Identifier)this.val_peek(3));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(9)), "CtxRelationship");
					break;
				case 187:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxTypeName");
					break;
				case 188:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxTypeName");
					break;
				case 189:
				{
					identifier = (Identifier)this.val_peek(1);
					Identifier identifier2 = (Identifier)this.val_peek(0);
					if (identifier.IsEscaped || identifier2.Name.Length > 0)
					{
						goto IL_30DC;
					}
					this.yyval = new Identifier(identifier.Name + "[]", false, this._query, CqlParser.AstNodePos(this.val_peek(1)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(1)), "CtxTypeName");
					break;
				}
				case 190:
				{
					DotExpr dotExpr = (DotExpr)this.val_peek(1);
					identifier3 = dotExpr.Identifier;
					Identifier identifier4 = (Identifier)this.val_peek(0);
					if (identifier3.IsEscaped || identifier4.Name.Length > 0)
					{
						goto IL_3186;
					}
					this.yyval = new DotExpr(dotExpr.Left, new Identifier(identifier3.Name + "[]", false, this._query, CqlParser.AstNodePos(this.val_peek(1))));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(1)), "CtxTypeName");
					break;
				}
				case 191:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxTypeName");
					break;
				case 192:
					this.yyval = new DotExpr((Node)this.val_peek(2), (Identifier)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(2)), "CtxTypeName");
					break;
				case 193:
					this.yyval = new MethodExpr((Node)this.val_peek(2), DistinctKind.None, null);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(((DotExpr)this.val_peek(2)).Identifier), "CtxTypeNameWithTypeSpec");
					break;
				case 194:
					this.yyval = new MethodExpr((Node)this.val_peek(3), DistinctKind.None, CqlParser.ToNodeList<Node>(this.val_peek(1)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(((DotExpr)this.val_peek(3)).Identifier), "CtxTypeNameWithTypeSpec");
					break;
				case 195:
					this.yyval = new MethodExpr((Identifier)this.val_peek(2), DistinctKind.None, null);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(2)), "CtxTypeNameWithTypeSpec");
					break;
				case 196:
					this.yyval = new MethodExpr((Identifier)this.val_peek(3), DistinctKind.None, CqlParser.ToNodeList<Node>(this.val_peek(1)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(3)), "CtxTypeNameWithTypeSpec");
					break;
				case 197:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxEscapedIdentifier");
					break;
				case 198:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxSimpleIdentifier");
					break;
				case 199:
					this.yyval = this.val_peek(0);
					break;
				case 200:
					this.yyval = this.val_peek(0);
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), "CtxLiteral");
					break;
				case 201:
					this.yyval = new Literal(null, LiteralKind.Null, this._query, CqlParser.TerminalPos(this.val_peek(0)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(0)), "CtxNullLiteral");
					break;
				case 202:
					this.yyval = this.val_peek(0);
					break;
				case 203:
					this.yyval = this.val_peek(0);
					break;
				case 204:
					this.yyval = this.val_peek(0);
					break;
				case 205:
					this.yyval = this.val_peek(0);
					break;
				case 206:
					this.yyval = new CollectionTypeDefinition((Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxCollectionTypeDefinition");
					break;
				case 207:
					this.yyval = new RefTypeDefinition((Node)this.val_peek(1));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxRefTypeDefinition");
					break;
				case 208:
					this.yyval = new RowTypeDefinition(CqlParser.ToNodeList<PropDefinition>(this.val_peek(1)));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.Terminal(this.val_peek(3)), "CtxRowTypeDefinition");
					break;
				case 209:
					this.yyval = new NodeList<PropDefinition>((PropDefinition)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(0)), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 210:
					this.yyval = CqlParser.ToNodeList<PropDefinition>(this.val_peek(2)).Add((PropDefinition)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.yyval), CqlParser.AstNode(this.val_peek(0)).ErrCtx.ErrorContextInfo);
					break;
				case 211:
					this.yyval = new PropDefinition((Identifier)this.val_peek(1), (Node)this.val_peek(0));
					this.SetErrCtx(CqlParser.AstNode(this.yyval), CqlParser.AstNodePos(this.val_peek(1)), "CtxRowTypeDefinition");
					break;
				}
				this.state_drop(num3);
				num = this.state_peek(0);
				this.val_drop(num3);
				num3 = (int)CqlParser.yylhs[num2];
				if (num == 0 && num3 == 0)
				{
					num = (int)CqlParser.YYFINAL;
					this.state_push((int)CqlParser.YYFINAL);
					this.val_push(this.yyval);
					if (this.yychar < 0)
					{
						this.yychar = (int)this.yylex();
						if (this.yychar < 0)
						{
							this.yychar = 0;
						}
					}
					if (this.yychar == 0)
					{
						return 0;
					}
				}
				else
				{
					num2 = (int)CqlParser.yygindex[num3];
					if (num2 != 0 && (num2 += num) >= 0 && num2 <= CqlParser.YYTABLESIZE && (int)CqlParser.yycheck[num2] == num)
					{
						num = (int)CqlParser.yytable[num2];
					}
					else
					{
						num = (int)CqlParser.yydgoto[num3];
					}
					if (this.stateptr < 0)
					{
						goto IL_3754;
					}
					this.state_push(num);
					this.val_push(this.yyval);
				}
			}
			IL_30DC:
			ErrorContext errCtx = identifier.ErrCtx;
			string invalidMetadataMemberName = Strings.InvalidMetadataMemberName;
			throw EntitySqlException.Create(errCtx, invalidMetadataMemberName, null);
			IL_3186:
			ErrorContext errCtx2 = identifier3.ErrCtx;
			string invalidMetadataMemberName2 = Strings.InvalidMetadataMemberName;
			throw EntitySqlException.Create(errCtx2, invalidMetadataMemberName2, null);
			IL_3754:
			this.yyerror("yacc stack overflow");
			return 1;
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x001167A3 File Offset: 0x001149A3
		internal CqlParser(ParserOptions parserOptions, bool debug)
		{
			this._parserOptions = parserOptions;
			this.yydebug = debug;
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x001167B9 File Offset: 0x001149B9
		internal Node Parse(string query)
		{
			this._query = query;
			this._parsedTree = null;
			this._methodExprCounter = 0U;
			this._methodExprCounterStack = new Stack<uint>();
			this.internalParseEntryPoint();
			return this._parsedTree;
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06004DAE RID: 19886 RVA: 0x001167E7 File Offset: 0x001149E7
		internal string Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06004DAF RID: 19887 RVA: 0x001167EF File Offset: 0x001149EF
		internal ParserOptions ParserOptions
		{
			get
			{
				return this._parserOptions;
			}
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x001167F7 File Offset: 0x001149F7
		private void internalParseEntryPoint()
		{
			this._lexer = new CqlLexer(this.Query, this.ParserOptions);
			this.yyparse();
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x00116817 File Offset: 0x00114A17
		private static Node AstNode(object o)
		{
			return (Node)o;
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x0011681F File Offset: 0x00114A1F
		private static int AstNodePos(object o)
		{
			return ((Node)o).ErrCtx.InputPosition;
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x00116831 File Offset: 0x00114A31
		private static CqlLexer.TerminalToken Terminal(object o)
		{
			return (CqlLexer.TerminalToken)o;
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x00116839 File Offset: 0x00114A39
		private static int TerminalPos(object o)
		{
			return ((CqlLexer.TerminalToken)o).IPos;
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x00116846 File Offset: 0x00114A46
		private static NodeList<T> ToNodeList<T>(object o) where T : Node
		{
			return (NodeList<T>)o;
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x00116850 File Offset: 0x00114A50
		private short yylex()
		{
			CqlLexer.Token token = this._lexer.yylex();
			if (token == null)
			{
				return 0;
			}
			this._lexer.AdvanceIPos();
			this.yylval = token.Value;
			return token.TokenId;
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x0011688E File Offset: 0x00114A8E
		private void yyerror_stackoverflow()
		{
			this.yyerror(Strings.StackOverflowInParser);
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x0011689C File Offset: 0x00114A9C
		private void yyerror(string s)
		{
			if (s.Equals("syntax error", StringComparison.Ordinal))
			{
				int num = this._lexer.IPos;
				string text = null;
				string text2 = this._lexer.YYText;
				if (!string.IsNullOrEmpty(text2))
				{
					text = Strings.LocalizedTerm;
					ErrorContext errorContext = null;
					Node node = this.yylval as Node;
					if (node != null && node.ErrCtx != null && !string.IsNullOrEmpty(node.ErrCtx.ErrorContextInfo))
					{
						errorContext = node.ErrCtx;
						num = Math.Min(num, num - text2.Length);
					}
					if (this.yylval is CqlLexer.TerminalToken && CqlLexer.IsReservedKeyword(text2) && !(node is Identifier))
					{
						text = Strings.LocalizedKeyword;
						text2 = text2.ToUpperInvariant();
						num = Math.Min(num, num - text2.Length);
					}
					else if (errorContext != null)
					{
						text = EntityRes.GetString(errorContext.ErrorContextInfo);
					}
					text = string.Format(CultureInfo.CurrentCulture, "{0} '{1}'", new object[] { text, text2 });
				}
				string genericSyntaxError = Strings.GenericSyntaxError;
				throw EntitySqlException.Create(this._query, genericSyntaxError, num, text, false, null);
			}
			int ipos = this._lexer.IPos;
			throw EntitySqlException.Create(this._query, s, ipos, null, false, null);
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x001169CD File Offset: 0x00114BCD
		private void SetErrCtx(Node astExpr, CqlLexer.TerminalToken tokenValue, string info)
		{
			this.SetErrCtx(astExpr, tokenValue.IPos, info);
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x001169DD File Offset: 0x00114BDD
		private void SetErrCtx(Node astExpr, int inputPos, string info)
		{
			astExpr.ErrCtx.InputPosition = inputPos;
			astExpr.ErrCtx.ErrorContextInfo = info;
			astExpr.ErrCtx.CommandText = this._query;
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x00116A08 File Offset: 0x00114C08
		private void StartMethodExprCounting()
		{
			this._methodExprCounterStack.Push(this._methodExprCounter);
			this._methodExprCounter = 0U;
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x00116A22 File Offset: 0x00114C22
		private void IncrementMethodExprCount()
		{
			this._methodExprCounter += 1U;
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x00116A32 File Offset: 0x00114C32
		private uint EndMethodExprCounting()
		{
			uint methodExprCounter = this._methodExprCounter;
			this._methodExprCounter += this._methodExprCounterStack.Pop();
			return methodExprCounter;
		}

		// Token: 0x04001B9C RID: 7068
		private readonly bool yydebug;

		// Token: 0x04001B9D RID: 7069
		private static int YYMAJOR = 1;

		// Token: 0x04001B9E RID: 7070
		private static int YYMINOR = 9;

		// Token: 0x04001B9F RID: 7071
		private int yynerrs;

		// Token: 0x04001BA0 RID: 7072
		private int yyerrflag;

		// Token: 0x04001BA1 RID: 7073
		private int yychar;

		// Token: 0x04001BA2 RID: 7074
		private static int YYSTACKSIZE = 500;

		// Token: 0x04001BA3 RID: 7075
		private int[] statestk;

		// Token: 0x04001BA4 RID: 7076
		private int stateptr;

		// Token: 0x04001BA5 RID: 7077
		private object yyval;

		// Token: 0x04001BA6 RID: 7078
		private object yylval;

		// Token: 0x04001BA7 RID: 7079
		private object[] valstk;

		// Token: 0x04001BA8 RID: 7080
		private int valptr;

		// Token: 0x04001BA9 RID: 7081
		public static short IDENTIFIER = 257;

		// Token: 0x04001BAA RID: 7082
		public static short ESCAPED_IDENTIFIER = 258;

		// Token: 0x04001BAB RID: 7083
		public static short PARAMETER = 259;

		// Token: 0x04001BAC RID: 7084
		public static short LITERAL = 260;

		// Token: 0x04001BAD RID: 7085
		public static short ALL = 261;

		// Token: 0x04001BAE RID: 7086
		public static short AND = 262;

		// Token: 0x04001BAF RID: 7087
		public static short ANYELEMENT = 263;

		// Token: 0x04001BB0 RID: 7088
		public static short APPLY = 264;

		// Token: 0x04001BB1 RID: 7089
		public static short AS = 265;

		// Token: 0x04001BB2 RID: 7090
		public static short ASC = 266;

		// Token: 0x04001BB3 RID: 7091
		public static short BETWEEN = 267;

		// Token: 0x04001BB4 RID: 7092
		public static short BY = 268;

		// Token: 0x04001BB5 RID: 7093
		public static short CASE = 269;

		// Token: 0x04001BB6 RID: 7094
		public static short CAST = 270;

		// Token: 0x04001BB7 RID: 7095
		public static short COLLATE = 271;

		// Token: 0x04001BB8 RID: 7096
		public static short COLLECTION = 272;

		// Token: 0x04001BB9 RID: 7097
		public static short CROSS = 273;

		// Token: 0x04001BBA RID: 7098
		public static short CREATEREF = 274;

		// Token: 0x04001BBB RID: 7099
		public static short DEREF = 275;

		// Token: 0x04001BBC RID: 7100
		public static short DESC = 276;

		// Token: 0x04001BBD RID: 7101
		public static short DISTINCT = 277;

		// Token: 0x04001BBE RID: 7102
		public static short ELEMENT = 278;

		// Token: 0x04001BBF RID: 7103
		public static short ELSE = 279;

		// Token: 0x04001BC0 RID: 7104
		public static short END = 280;

		// Token: 0x04001BC1 RID: 7105
		public static short EXCEPT = 281;

		// Token: 0x04001BC2 RID: 7106
		public static short EXISTS = 282;

		// Token: 0x04001BC3 RID: 7107
		public static short ESCAPE = 283;

		// Token: 0x04001BC4 RID: 7108
		public static short FLATTEN = 284;

		// Token: 0x04001BC5 RID: 7109
		public static short FROM = 285;

		// Token: 0x04001BC6 RID: 7110
		public static short FULL = 286;

		// Token: 0x04001BC7 RID: 7111
		public static short FUNCTION = 287;

		// Token: 0x04001BC8 RID: 7112
		public static short GROUP = 288;

		// Token: 0x04001BC9 RID: 7113
		public static short GROUPPARTITION = 289;

		// Token: 0x04001BCA RID: 7114
		public static short HAVING = 290;

		// Token: 0x04001BCB RID: 7115
		public static short IN = 291;

		// Token: 0x04001BCC RID: 7116
		public static short INNER = 292;

		// Token: 0x04001BCD RID: 7117
		public static short INTERSECT = 293;

		// Token: 0x04001BCE RID: 7118
		public static short IS = 294;

		// Token: 0x04001BCF RID: 7119
		public static short JOIN = 295;

		// Token: 0x04001BD0 RID: 7120
		public static short KEY = 296;

		// Token: 0x04001BD1 RID: 7121
		public static short LEFT = 297;

		// Token: 0x04001BD2 RID: 7122
		public static short LIKE = 298;

		// Token: 0x04001BD3 RID: 7123
		public static short LIMIT = 299;

		// Token: 0x04001BD4 RID: 7124
		public static short MULTISET = 300;

		// Token: 0x04001BD5 RID: 7125
		public static short NAVIGATE = 301;

		// Token: 0x04001BD6 RID: 7126
		public static short NOT = 302;

		// Token: 0x04001BD7 RID: 7127
		public static short NULL = 303;

		// Token: 0x04001BD8 RID: 7128
		public static short OF = 304;

		// Token: 0x04001BD9 RID: 7129
		public static short OFTYPE = 305;

		// Token: 0x04001BDA RID: 7130
		public static short ON = 306;

		// Token: 0x04001BDB RID: 7131
		public static short OR = 307;

		// Token: 0x04001BDC RID: 7132
		public static short ORDER = 308;

		// Token: 0x04001BDD RID: 7133
		public static short OUTER = 309;

		// Token: 0x04001BDE RID: 7134
		public static short OVERLAPS = 310;

		// Token: 0x04001BDF RID: 7135
		public static short ONLY = 311;

		// Token: 0x04001BE0 RID: 7136
		public static short QMARK = 312;

		// Token: 0x04001BE1 RID: 7137
		public static short REF = 313;

		// Token: 0x04001BE2 RID: 7138
		public static short RELATIONSHIP = 314;

		// Token: 0x04001BE3 RID: 7139
		public static short RIGHT = 315;

		// Token: 0x04001BE4 RID: 7140
		public static short ROW = 316;

		// Token: 0x04001BE5 RID: 7141
		public static short SELECT = 317;

		// Token: 0x04001BE6 RID: 7142
		public static short SET = 318;

		// Token: 0x04001BE7 RID: 7143
		public static short SKIP = 319;

		// Token: 0x04001BE8 RID: 7144
		public static short THEN = 320;

		// Token: 0x04001BE9 RID: 7145
		public static short TOP = 321;

		// Token: 0x04001BEA RID: 7146
		public static short TREAT = 322;

		// Token: 0x04001BEB RID: 7147
		public static short UNION = 323;

		// Token: 0x04001BEC RID: 7148
		public static short USING = 324;

		// Token: 0x04001BED RID: 7149
		public static short VALUE = 325;

		// Token: 0x04001BEE RID: 7150
		public static short WHEN = 326;

		// Token: 0x04001BEF RID: 7151
		public static short WHERE = 327;

		// Token: 0x04001BF0 RID: 7152
		public static short WITH = 328;

		// Token: 0x04001BF1 RID: 7153
		public static short COMMA = 329;

		// Token: 0x04001BF2 RID: 7154
		public static short COLON = 330;

		// Token: 0x04001BF3 RID: 7155
		public static short SCOLON = 331;

		// Token: 0x04001BF4 RID: 7156
		public static short DOT = 332;

		// Token: 0x04001BF5 RID: 7157
		public static short EQUAL = 333;

		// Token: 0x04001BF6 RID: 7158
		public static short L_PAREN = 334;

		// Token: 0x04001BF7 RID: 7159
		public static short R_PAREN = 335;

		// Token: 0x04001BF8 RID: 7160
		public static short L_BRACE = 336;

		// Token: 0x04001BF9 RID: 7161
		public static short R_BRACE = 337;

		// Token: 0x04001BFA RID: 7162
		public static short L_CURLY = 338;

		// Token: 0x04001BFB RID: 7163
		public static short R_CURLY = 339;

		// Token: 0x04001BFC RID: 7164
		public static short PLUS = 340;

		// Token: 0x04001BFD RID: 7165
		public static short MINUS = 341;

		// Token: 0x04001BFE RID: 7166
		public static short STAR = 342;

		// Token: 0x04001BFF RID: 7167
		public static short FSLASH = 343;

		// Token: 0x04001C00 RID: 7168
		public static short PERCENT = 344;

		// Token: 0x04001C01 RID: 7169
		public static short OP_EQ = 345;

		// Token: 0x04001C02 RID: 7170
		public static short OP_NEQ = 346;

		// Token: 0x04001C03 RID: 7171
		public static short OP_LT = 347;

		// Token: 0x04001C04 RID: 7172
		public static short OP_LE = 348;

		// Token: 0x04001C05 RID: 7173
		public static short OP_GT = 349;

		// Token: 0x04001C06 RID: 7174
		public static short OP_GE = 350;

		// Token: 0x04001C07 RID: 7175
		public static short UNARYPLUS = 351;

		// Token: 0x04001C08 RID: 7176
		public static short UNARYMINUS = 352;

		// Token: 0x04001C09 RID: 7177
		public static short YYERRCODE = 256;

		// Token: 0x04001C0A RID: 7178
		private static readonly short[] yylhs = new short[]
		{
			-1, 0, 0, 1, 2, 2, 4, 4, 5, 5,
			5, 3, 9, 9, 12, 12, 13, 14, 14, 15,
			15, 16, 10, 10, 11, 11, 18, 27, 20, 30,
			20, 26, 26, 26, 28, 28, 21, 31, 31, 32,
			32, 32, 32, 32, 34, 34, 35, 36, 36, 36,
			36, 36, 36, 36, 36, 36, 36, 37, 37, 22,
			22, 38, 23, 23, 39, 24, 24, 41, 40, 25,
			25, 44, 42, 45, 45, 46, 46, 43, 43, 47,
			47, 48, 48, 48, 50, 50, 19, 19, 19, 19,
			19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
			51, 62, 63, 52, 52, 52, 52, 52, 52, 52,
			52, 52, 52, 52, 52, 52, 52, 52, 52, 52,
			52, 52, 52, 52, 52, 52, 52, 52, 52, 52,
			52, 52, 52, 52, 52, 52, 52, 52, 52, 52,
			52, 52, 52, 52, 52, 52, 52, 52, 66, 66,
			8, 67, 33, 33, 29, 29, 64, 64, 68, 68,
			69, 58, 58, 58, 7, 53, 59, 54, 54, 55,
			56, 57, 57, 57, 57, 57, 57, 60, 60, 60,
			70, 70, 71, 71, 72, 72, 72, 65, 65, 65,
			65, 65, 73, 74, 74, 74, 74, 6, 6, 49,
			61, 61, 17, 17, 17, 17, 75, 76, 77, 78,
			78, 79
		};

		// Token: 0x04001C0B RID: 7179
		private static readonly short[] yylen = new short[]
		{
			2, 0, 1, 2, 0, 1, 1, 2, 3, 3,
			3, 3, 0, 1, 1, 2, 7, 2, 3, 1,
			3, 2, 1, 1, 0, 1, 6, 0, 5, 0,
			6, 0, 1, 1, 0, 4, 2, 1, 3, 1,
			3, 1, 3, 1, 3, 5, 3, 2, 3, 2,
			3, 2, 1, 2, 2, 3, 2, 2, 2, 0,
			1, 2, 0, 1, 3, 0, 1, 0, 3, 0,
			1, 0, 6, 0, 2, 0, 2, 1, 3, 2,
			4, 0, 1, 1, 1, 3, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			3, 3, 4, 3, 3, 3, 3, 3, 2, 2,
			3, 3, 3, 3, 3, 3, 3, 4, 3, 3,
			3, 4, 4, 4, 4, 4, 4, 3, 4, 1,
			6, 6, 6, 7, 6, 7, 7, 8, 3, 4,
			5, 6, 3, 3, 3, 2, 3, 1, 1, 1,
			3, 3, 3, 1, 1, 3, 3, 4, 4, 5,
			2, 4, 4, 3, 3, 4, 4, 6, 8, 4,
			5, 3, 6, 6, 3, 6, 6, 6, 8, 10,
			0, 1, 2, 2, 6, 8, 10, 1, 1, 2,
			2, 1, 3, 3, 4, 3, 4, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 4, 4, 4, 1,
			3, 2
		};

		// Token: 0x04001C0C RID: 7180
		private static readonly short[] yydefred = new short[]
		{
			0, 0, 0, 2, 0, 0, 6, 199, 197, 87,
			200, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 201, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 198, 86,
			89, 91, 92, 93, 94, 95, 96, 97, 98, 99,
			0, 0, 129, 147, 149, 0, 3, 0, 0, 14,
			7, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 148, 0, 0, 0,
			0, 0, 0, 0, 0, 22, 0, 0, 0, 0,
			0, 0, 8, 0, 9, 0, 10, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 15, 0, 0, 0, 156,
			0, 0, 0, 0, 0, 0, 0, 0, 32, 33,
			0, 0, 0, 0, 0, 0, 0, 0, 154, 0,
			0, 0, 0, 100, 0, 0, 0, 163, 174, 0,
			171, 0, 0, 0, 0, 0, 0, 0, 127, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 164, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 25, 11, 123, 0, 0,
			0, 157, 0, 0, 166, 124, 122, 125, 0, 169,
			162, 0, 0, 165, 0, 0, 161, 126, 0, 0,
			0, 0, 0, 0, 39, 41, 43, 0, 0, 60,
			0, 0, 0, 0, 0, 128, 0, 0, 0, 0,
			0, 0, 0, 17, 0, 0, 19, 0, 0, 0,
			0, 0, 0, 191, 0, 170, 0, 0, 0, 152,
			155, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 52, 0, 0, 0, 0, 0, 0,
			0, 0, 63, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 21, 0, 203, 204, 205,
			0, 18, 0, 0, 189, 0, 0, 131, 190, 0,
			0, 167, 0, 177, 0, 132, 130, 0, 0, 0,
			40, 42, 0, 57, 47, 54, 0, 53, 49, 0,
			58, 51, 0, 0, 46, 0, 67, 0, 66, 0,
			176, 0, 175, 173, 172, 0, 0, 0, 134, 0,
			0, 0, 0, 20, 0, 195, 0, 192, 193, 0,
			0, 0, 133, 0, 0, 55, 48, 50, 0, 0,
			0, 0, 26, 70, 0, 182, 183, 0, 135, 136,
			0, 0, 0, 0, 209, 16, 196, 194, 168, 0,
			178, 35, 0, 0, 71, 0, 137, 206, 207, 211,
			0, 208, 0, 0, 0, 210, 179, 0, 0, 77,
			0, 82, 0, 83, 79, 0, 0, 0, 0, 0,
			0, 78, 0, 72, 0, 184, 80, 0, 0, 0,
			185, 0, 186
		};

		// Token: 0x04001C0D RID: 7181
		private static readonly short[] yydgoto = new short[]
		{
			2, 3, 4, 56, 5, 6, 74, 75, 76, 57,
			84, 196, 58, 59, 194, 245, 246, 295, 85, 86,
			87, 155, 228, 281, 337, 372, 140, 151, 263, 147,
			152, 222, 223, 224, 225, 226, 277, 278, 229, 282,
			338, 370, 373, 408, 403, 417, 423, 409, 414, 38,
			89, 39, 40, 41, 42, 43, 44, 45, 46, 47,
			48, 49, 50, 51, 52, 296, 53, 54, 63, 131,
			340, 341, 375, 252, 253, 297, 298, 299, 383, 384
		};

		// Token: 0x04001C0E RID: 7182
		private static readonly short[] yysindex = new short[]
		{
			-321, 4723, 0, 0, -269, -321, 0, 0, 0, 0,
			0, -309, -271, -284, -204, -176, -162, -155, -131, -118,
			-94, -84, -75, 4723, 0, -73, -51, -46, -42, -37,
			4054, 4723, 4723, 4723, -182, -181, -211, 3614, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			-111, 14, 0, 0, 0, -171, 0, 4054, -269, 0,
			0, 4054, 4723, -207, 4723, 4723, 4054, 4054, 4054, 4054,
			-225, 4054, 4723, 4723, -36, -28, 0, 5025, 4723, 4054,
			4723, 4054, 4723, 0, -13, 0, 3614, 22, 3614, -291,
			-8, -8, 0, -232, 0, -226, 0, 4723, 4723, 4723,
			4723, 4723, 174, 4723, -186, 4723, 4723, 4139, -171, 4723,
			4723, 4723, 4723, 4723, 4723, 4723, 4723, 4723, 4723, 4723,
			4723, 4723, 4723, -6, -5, 0, -3, 3098, 4723, 0,
			4723, 53, 3170, 3198, 3, 13, 18, 19, 0, 0,
			4054, 41, -213, 3270, 3298, 42, 3370, -159, 0, 47,
			3442, -225, 62, 0, 4796, 68, 4723, 0, 0, 4054,
			0, 4054, 5025, 4955, 5124, 5053, 3413, -141, 0, 59,
			2998, 4723, 4723, 4723, 3814, 2926, 4723, 3341, 0, 4037,
			-177, -177, -8, -8, -8, 4037, 4037, 1461, 1461, 1461,
			1461, 5025, 5025, -256, 160, 0, 0, 0, 4723, 3614,
			3514, 0, -171, 4723, 0, 0, 0, 0, 99, 0,
			0, -171, -241, 0, -171, 4723, 0, 0, -171, 124,
			-225, 4212, 122, -58, 0, 0, 0, 4723, 167, 0,
			3614, 125, -110, 127, -81, 0, 123, -194, 4723, 4955,
			5053, 3786, 3341, 0, -216, -69, 0, 135, 3614, 4723,
			-254, -161, -245, 0, 3026, 0, -214, -171, -128, 0,
			0, -127, 139, 4723, 124, 3370, -58, 149, 151, 4796,
			-249, -272, 179, 0, -265, 223, -248, 4796, 4796, 3614,
			226, 207, 0, 170, 170, 170, 170, -183, -171, -112,
			5096, 4723, 161, 175, 176, 0, 169, 0, 0, 0,
			-171, 0, 4054, 3614, 0, 4285, -171, 0, 0, 4358,
			-171, 0, -171, 0, -102, 0, 0, 4054, 178, 4723,
			0, 0, -58, 0, 0, 0, 216, 0, 0, 230,
			0, 0, 236, 228, 0, 4723, 0, 229, 0, 222,
			0, 222, 0, 0, 0, -171, -100, -39, 0, 5096,
			-216, -171, -171, 0, 204, 0, -66, 0, 0, -61,
			-20, -57, 0, 209, 178, 0, 0, 0, 4723, 178,
			4723, 272, 0, 0, 212, 0, 0, 73, 0, 0,
			214, 115, -216, -56, 0, 0, 0, 0, 0, -171,
			0, 0, 3614, 3614, 0, 4723, 0, 0, 0, 0,
			-171, 0, 227, 4723, 3542, 0, 0, 2954, -276, 0,
			-171, 0, 294, 0, 0, 4723, 4723, 259, -151, -199,
			3614, 0, 4723, 0, -171, 0, 0, 3614, -45, -171,
			0, 231, 0
		};

		// Token: 0x04001C0F RID: 7183
		private static readonly short[] yyrindex = new short[]
		{
			1510, 0, 0, 0, 4431, 3981, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 3642, 3714, 3742, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 4504, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			4577, 0, 0, 0, 173, 262, 0, 2591, 0, 0,
			0, 0, 0, 3908, 0, 0, 76, 0, -117, 0,
			532, 621, 0, 4577, 0, 4577, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 563, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 28, 0, 0, 0,
			0, 4650, 0, 0, 0, 39, 0, 0, 0, 0,
			0, 0, 2662, 303, 2023, 2307, 1576, 0, 0, 0,
			2378, 0, 0, 0, 2861, 2094, 0, 1876, 0, 1647,
			979, 1068, 711, 800, 889, 1723, 1798, 1157, 1242, 1331,
			1420, 2733, 2804, 0, 0, 0, 0, 0, 0, 287,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 4869,
			4650, 0, 1185, 31, 0, 0, 0, 0, 82, 0,
			-108, 0, 0, 0, 0, 0, 0, 0, 0, 311,
			2449, 2520, 1947, 0, 0, 0, 0, 0, -169, 0,
			-139, 0, -83, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 4869, -109, 0, -15, 1294, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 96,
			0, 33, 0, 352, 352, 352, 352, 0, 0, 0,
			2165, 0, 0, 0, 0, 0, -44, 0, 0, 0,
			0, 0, 0, -166, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 289, 0,
			0, 0, 95, 0, 0, 0, 4942, 0, 0, 0,
			0, 0, 0, 2909, 0, 0, 0, 84, 0, 0,
			0, 443, 0, 0, 0, 0, 0, 0, 0, 2236,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 290, 0, 0, 0, 0, 195,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 83, 71, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 11, 66, 0,
			0, 0, 0, 0, 0, 0, 0, 85, 0, 11,
			101, 0, 0, 0, 0, 0, 0, 102, 0, 0,
			0, 0, 0
		};

		// Token: 0x04001C10 RID: 7184
		private static readonly short[] yygindex = new short[]
		{
			0, 0, 0, 0, 0, 571, -1, 577, 578, 0,
			-47, 0, 0, 522, 0, 0, 283, -342, 30, 26,
			0, 0, 0, 0, 0, 0, -86, 0, 320, -257,
			0, 0, -209, -54, 365, 366, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 180, 171, 186,
			-67, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, -43, 0, 0, 0, 0,
			205, 0, 251, 0, 0, 0, 0, 0, 0, 199
		};

		// Token: 0x04001C11 RID: 7185
		private static int YYTABLESIZE = 5474;

		// Token: 0x04001C12 RID: 7186
		private static readonly short[] yytable = new short[]
		{
			34, 7, 8, 1, 304, 142, 318, 159, 380, 161,
			124, 81, 266, 308, 126, 323, 7, 8, 55, 134,
			135, 136, 137, 325, 141, 61, 148, 37, 153, 138,
			328, 37, 145, 65, 149, 138, 138, 326, 156, 59,
			399, 7, 8, 415, 329, 139, 324, 331, 157, 77,
			64, 139, 139, 416, 123, 62, 292, 88, 90, 91,
			322, 332, 364, 7, 8, 219, 73, 411, 333, 334,
			257, 68, 128, 129, 7, 8, 23, 413, 369, 243,
			305, 171, 62, 45, 69, 75, 7, 8, 127, 309,
			132, 133, 232, 208, 234, 38, 61, 293, 88, 143,
			294, 74, 76, 158, 144, 172, 146, 178, 150, 160,
			158, 158, 173, 159, 159, 312, 156, 288, 306, 130,
			96, 313, 210, 162, 163, 164, 165, 166, 345, 170,
			65, 174, 175, 177, 264, 179, 180, 181, 182, 183,
			184, 185, 186, 187, 188, 189, 190, 191, 192, 92,
			94, 121, 93, 95, 199, 108, 200, 158, 66, 251,
			159, 260, 235, 236, 153, 112, 113, 114, 256, 258,
			215, 306, 67, 88, 307, 261, 216, 153, 424, 68,
			146, 306, 230, 153, 425, 88, 153, 88, 153, 231,
			187, 233, 244, 187, 289, 64, 187, 239, 240, 241,
			153, 250, 242, 69, 306, 306, 153, 315, 316, 148,
			250, 250, 84, 259, 314, 270, 70, 250, 84, 156,
			306, 85, 84, 348, 248, 284, 23, 85, 271, 254,
			306, 85, 306, 362, 272, 378, 250, 273, 356, 274,
			71, 146, 359, 250, 346, 347, 188, 265, 156, 188,
			72, 275, 188, 279, 286, 354, 250, 276, 41, 73,
			300, 78, 90, 156, 290, 148, 301, 360, 156, 386,
			363, 41, 389, 400, 387, 303, 122, 41, 390, 401,
			41, 148, 41, 79, 429, 202, 250, 250, 80, 146,
			430, 202, 81, 306, 41, 146, 379, 82, 93, 244,
			41, 153, 377, 146, 146, 357, 95, 154, 381, 250,
			81, 361, 306, 153, 153, 388, 153, 349, 153, 37,
			153, 37, 153, 153, 108, 153, 195, 59, 193, 59,
			81, 88, 197, 201, 153, 88, 153, 153, 204, 37,
			81, 65, 81, 153, 250, 146, 81, 59, 205, 250,
			250, 382, 180, 206, 207, 153, 45, 153, 37, 153,
			37, 146, 37, 153, 65, 73, 37, 418, 65, 45,
			59, 45, 62, 45, 59, 45, 209, 213, 45, 68,
			45, 250, 217, 38, 61, 38, 61, 220, 402, 45,
			62, 45, 45, 237, 392, 227, 393, 73, 45, 382,
			74, 73, 68, 38, 61, 306, 68, 23, 396, 250,
			45, 23, 45, 62, 45, 69, 75, 62, 45, 69,
			75, 404, 38, 428, 38, 247, 38, 61, 431, 407,
			38, 61, 74, 76, 255, 88, 74, 76, 88, 88,
			88, 420, 407, 181, 88, 262, 88, 306, 427, 88,
			398, 269, 88, 88, 88, 280, 88, 287, 88, 88,
			283, 88, 285, 88, 88, 88, 88, 88, 88, 302,
			88, 88, 88, 317, 327, 88, 167, 168, 169, 88,
			88, 88, 88, 88, 320, 64, 321, 330, 88, 342,
			343, 344, 88, 88, 335, 350, 88, 336, 339, 88,
			88, 306, 88, 64, 88, 88, 88, 215, 88, 351,
			352, 365, 88, 88, 88, 88, 88, 88, 88, 88,
			88, 88, 88, 88, 90, 366, 64, 90, 90, 90,
			64, 367, 109, 90, 368, 90, 374, 371, 90, 385,
			394, 90, 90, 90, 391, 90, 395, 90, 90, 397,
			90, 7, 90, 90, 90, 90, 90, 90, 422, 90,
			90, 90, 406, 24, 90, 101, 432, 160, 90, 90,
			90, 90, 90, 102, 28, 30, 60, 90, 35, 36,
			125, 90, 90, 353, 319, 90, 267, 268, 90, 90,
			426, 90, 376, 90, 90, 90, 421, 90, 419, 405,
			0, 90, 90, 90, 90, 90, 90, 90, 90, 90,
			90, 90, 90, 0, 180, 0, 0, 180, 180, 180,
			0, 108, 0, 180, 0, 180, 0, 0, 180, 0,
			0, 180, 180, 180, 0, 180, 0, 180, 180, 0,
			180, 0, 180, 180, 180, 180, 180, 180, 0, 180,
			180, 180, 0, 0, 180, 0, 0, 0, 180, 180,
			180, 180, 180, 0, 0, 0, 0, 180, 0, 0,
			0, 180, 180, 0, 0, 180, 0, 0, 180, 180,
			0, 180, 0, 180, 180, 180, 0, 180, 0, 0,
			0, 180, 180, 180, 180, 180, 180, 180, 180, 180,
			180, 180, 180, 0, 0, 181, 0, 0, 181, 181,
			181, 105, 0, 0, 181, 0, 181, 0, 0, 181,
			0, 0, 181, 181, 181, 0, 181, 0, 181, 181,
			0, 181, 0, 181, 181, 181, 181, 181, 181, 0,
			181, 181, 181, 0, 0, 181, 0, 0, 0, 181,
			181, 181, 181, 181, 0, 0, 0, 0, 181, 0,
			0, 0, 181, 181, 0, 0, 181, 0, 0, 181,
			181, 0, 181, 0, 181, 181, 181, 0, 181, 0,
			0, 0, 181, 181, 181, 181, 181, 181, 181, 181,
			181, 181, 181, 181, 109, 0, 0, 109, 109, 109,
			106, 0, 0, 109, 0, 109, 0, 0, 109, 0,
			0, 109, 109, 109, 0, 109, 0, 109, 109, 0,
			109, 0, 109, 109, 109, 109, 109, 109, 0, 109,
			109, 109, 0, 0, 109, 0, 0, 0, 109, 109,
			109, 109, 109, 0, 0, 0, 0, 109, 0, 0,
			0, 109, 109, 0, 0, 109, 0, 0, 109, 109,
			0, 109, 0, 109, 0, 109, 0, 109, 0, 0,
			0, 109, 109, 109, 109, 109, 109, 109, 109, 109,
			109, 109, 109, 108, 0, 0, 108, 108, 108, 107,
			0, 0, 108, 0, 108, 0, 0, 108, 0, 0,
			108, 108, 108, 0, 108, 0, 108, 108, 0, 108,
			0, 108, 108, 108, 108, 108, 108, 0, 108, 108,
			108, 0, 0, 108, 0, 0, 0, 108, 108, 108,
			108, 108, 0, 0, 0, 0, 108, 0, 0, 0,
			108, 108, 0, 0, 108, 0, 0, 108, 108, 0,
			108, 0, 108, 0, 108, 0, 108, 0, 0, 0,
			108, 108, 108, 108, 108, 108, 108, 108, 108, 108,
			108, 108, 0, 105, 0, 0, 105, 105, 105, 103,
			0, 0, 105, 0, 105, 0, 0, 105, 0, 0,
			105, 105, 105, 0, 105, 0, 105, 105, 0, 105,
			0, 105, 105, 105, 105, 105, 105, 0, 105, 105,
			105, 0, 0, 105, 0, 0, 0, 105, 105, 105,
			105, 105, 0, 0, 0, 0, 105, 0, 0, 0,
			105, 105, 0, 0, 105, 0, 0, 105, 105, 0,
			105, 0, 105, 0, 105, 0, 105, 0, 0, 0,
			105, 105, 105, 105, 105, 105, 105, 105, 105, 105,
			105, 105, 106, 0, 0, 106, 106, 106, 104, 0,
			0, 106, 0, 106, 0, 0, 106, 0, 0, 106,
			106, 106, 0, 106, 0, 106, 106, 0, 106, 0,
			106, 106, 106, 106, 106, 106, 0, 106, 106, 106,
			0, 0, 106, 0, 0, 0, 106, 106, 106, 106,
			106, 0, 0, 0, 0, 106, 0, 0, 0, 106,
			106, 0, 0, 106, 0, 0, 106, 106, 0, 106,
			0, 106, 0, 106, 0, 106, 0, 0, 0, 106,
			106, 106, 106, 106, 106, 106, 106, 106, 106, 106,
			106, 107, 0, 0, 107, 107, 107, 113, 0, 0,
			107, 0, 107, 0, 0, 107, 0, 0, 107, 107,
			107, 0, 107, 0, 107, 107, 0, 107, 0, 107,
			107, 107, 107, 107, 107, 36, 107, 107, 107, 0,
			0, 107, 0, 0, 0, 107, 107, 107, 107, 107,
			0, 0, 0, 0, 107, 0, 0, 0, 107, 107,
			0, 0, 107, 0, 0, 107, 107, 0, 107, 0,
			107, 0, 107, 0, 107, 0, 0, 0, 107, 107,
			107, 107, 107, 107, 107, 107, 107, 107, 107, 107,
			0, 103, 114, 0, 103, 103, 103, 0, 0, 0,
			103, 0, 103, 0, 0, 103, 0, 0, 103, 103,
			103, 0, 103, 0, 103, 103, 0, 103, 0, 103,
			103, 103, 103, 103, 103, 0, 103, 103, 103, 0,
			0, 103, 0, 0, 0, 103, 103, 103, 103, 103,
			0, 0, 0, 0, 103, 0, 0, 0, 103, 103,
			0, 0, 103, 0, 0, 103, 103, 0, 103, 0,
			103, 0, 103, 0, 103, 0, 0, 0, 103, 103,
			103, 0, 0, 0, 103, 103, 103, 103, 103, 103,
			104, 111, 0, 104, 104, 104, 0, 0, 0, 104,
			0, 104, 0, 0, 104, 0, 0, 104, 104, 104,
			0, 104, 0, 104, 104, 0, 104, 0, 104, 104,
			104, 104, 104, 104, 0, 104, 104, 104, 0, 0,
			104, 0, 0, 0, 104, 104, 104, 104, 104, 0,
			0, 0, 0, 104, 0, 0, 0, 104, 104, 0,
			0, 104, 0, 0, 104, 104, 0, 104, 0, 104,
			0, 104, 0, 104, 0, 0, 0, 104, 104, 104,
			0, 0, 0, 104, 104, 104, 104, 104, 104, 113,
			112, 0, 113, 113, 113, 0, 0, 0, 113, 0,
			113, 0, 0, 113, 0, 0, 113, 113, 113, 0,
			113, 0, 113, 113, 0, 113, 0, 113, 113, 113,
			113, 113, 113, 0, 113, 113, 113, 0, 0, 113,
			0, 0, 0, 113, 113, 113, 113, 113, 0, 0,
			0, 0, 113, 36, 0, 36, 113, 113, 0, 0,
			113, 0, 0, 113, 113, 0, 113, 0, 113, 0,
			113, 0, 113, 36, 0, 0, 113, 0, 0, 0,
			0, 0, 113, 113, 114, 0, 0, 114, 114, 114,
			1, 0, 36, 114, 0, 114, 36, 0, 114, 0,
			36, 114, 114, 114, 0, 114, 0, 114, 114, 0,
			114, 0, 114, 114, 114, 114, 114, 114, 0, 114,
			114, 114, 0, 0, 114, 0, 0, 0, 114, 114,
			114, 114, 114, 0, 0, 0, 0, 114, 0, 0,
			0, 114, 114, 0, 0, 114, 0, 43, 114, 114,
			0, 114, 0, 114, 0, 114, 115, 114, 0, 0,
			43, 114, 0, 0, 0, 0, 43, 114, 114, 43,
			0, 43, 0, 111, 0, 0, 111, 111, 111, 0,
			0, 0, 111, 43, 111, 0, 0, 111, 0, 43,
			111, 111, 111, 0, 111, 0, 111, 111, 0, 111,
			0, 111, 111, 111, 111, 111, 111, 0, 111, 111,
			111, 0, 0, 111, 0, 0, 0, 111, 111, 111,
			111, 111, 0, 0, 0, 0, 111, 150, 0, 0,
			111, 111, 0, 0, 111, 0, 0, 111, 111, 0,
			111, 0, 111, 0, 111, 0, 111, 0, 0, 0,
			111, 0, 0, 0, 0, 0, 111, 111, 0, 0,
			0, 0, 112, 0, 0, 112, 112, 112, 0, 0,
			0, 112, 0, 112, 0, 0, 112, 0, 0, 112,
			112, 112, 0, 112, 0, 112, 112, 0, 112, 0,
			112, 112, 112, 112, 112, 112, 0, 112, 112, 112,
			0, 0, 112, 151, 0, 0, 112, 112, 112, 112,
			112, 0, 0, 0, 0, 112, 0, 0, 0, 112,
			112, 0, 0, 112, 0, 0, 112, 112, 0, 112,
			0, 112, 0, 112, 0, 112, 0, 0, 0, 112,
			0, 0, 0, 0, 0, 112, 112, 4, 4, 4,
			4, 0, 0, 4, 0, 0, 0, 0, 0, 4,
			4, 0, 0, 0, 4, 4, 0, 0, 4, 0,
			0, 0, 4, 108, 4, 0, 0, 4, 110, 4,
			0, 110, 111, 112, 113, 114, 4, 0, 0, 0,
			4, 4, 4, 4, 0, 4, 0, 0, 0, 0,
			0, 0, 0, 4, 0, 0, 4, 4, 4, 0,
			0, 0, 4, 0, 0, 0, 0, 0, 115, 0,
			0, 115, 115, 115, 4, 0, 0, 115, 4, 115,
			4, 4, 115, 0, 0, 115, 115, 115, 0, 115,
			0, 115, 115, 0, 115, 0, 115, 115, 115, 115,
			115, 115, 0, 115, 115, 115, 116, 0, 115, 0,
			0, 0, 115, 115, 115, 115, 115, 0, 0, 0,
			0, 115, 0, 0, 0, 115, 115, 0, 0, 115,
			0, 0, 115, 115, 0, 115, 0, 115, 0, 150,
			0, 115, 150, 150, 150, 115, 0, 0, 150, 0,
			150, 0, 0, 150, 0, 0, 150, 150, 150, 0,
			150, 0, 150, 150, 0, 150, 0, 150, 150, 150,
			150, 150, 150, 0, 150, 150, 150, 117, 0, 150,
			0, 0, 0, 150, 150, 150, 150, 150, 0, 0,
			0, 0, 150, 0, 0, 0, 150, 150, 0, 0,
			150, 0, 0, 150, 150, 0, 150, 0, 150, 0,
			0, 0, 150, 0, 0, 151, 150, 0, 151, 151,
			151, 0, 0, 0, 151, 0, 151, 0, 0, 151,
			0, 0, 151, 151, 151, 0, 151, 0, 151, 151,
			0, 151, 0, 151, 151, 151, 151, 151, 151, 0,
			151, 151, 151, 118, 0, 151, 0, 0, 0, 151,
			151, 151, 151, 151, 0, 0, 0, 0, 151, 0,
			0, 0, 151, 151, 0, 0, 151, 0, 0, 151,
			151, 0, 151, 0, 151, 0, 0, 0, 151, 0,
			110, 0, 151, 110, 110, 110, 0, 0, 0, 110,
			0, 110, 0, 0, 110, 0, 0, 110, 110, 110,
			0, 110, 0, 110, 110, 0, 110, 0, 110, 110,
			110, 110, 110, 110, 119, 110, 110, 110, 0, 0,
			110, 0, 0, 0, 110, 110, 110, 110, 110, 0,
			0, 0, 0, 110, 0, 0, 0, 110, 110, 0,
			0, 110, 0, 0, 110, 110, 0, 110, 0, 110,
			0, 0, 0, 110, 0, 0, 0, 110, 116, 0,
			0, 116, 116, 116, 0, 0, 0, 116, 0, 116,
			0, 0, 116, 0, 0, 116, 116, 116, 0, 116,
			0, 116, 116, 0, 116, 140, 116, 116, 116, 0,
			116, 116, 0, 116, 116, 116, 0, 0, 116, 0,
			0, 0, 116, 116, 116, 116, 116, 0, 0, 0,
			0, 116, 0, 0, 0, 116, 116, 0, 0, 116,
			0, 0, 116, 116, 0, 116, 0, 116, 0, 117,
			0, 116, 117, 117, 117, 116, 0, 0, 117, 0,
			117, 0, 0, 117, 0, 0, 117, 117, 117, 0,
			117, 0, 117, 117, 0, 117, 141, 117, 117, 117,
			0, 117, 117, 0, 117, 117, 117, 0, 0, 117,
			0, 0, 0, 117, 117, 117, 117, 117, 0, 0,
			0, 0, 117, 0, 0, 0, 117, 117, 0, 0,
			117, 0, 0, 117, 117, 0, 117, 0, 117, 0,
			0, 0, 117, 0, 0, 118, 117, 0, 118, 118,
			118, 0, 0, 0, 118, 0, 118, 0, 0, 118,
			0, 0, 118, 118, 118, 0, 118, 120, 118, 118,
			0, 118, 0, 118, 118, 118, 0, 118, 118, 0,
			118, 118, 118, 0, 0, 118, 0, 0, 0, 118,
			118, 118, 118, 118, 0, 0, 0, 0, 118, 0,
			0, 0, 118, 118, 0, 0, 0, 0, 0, 118,
			118, 0, 118, 0, 118, 0, 119, 0, 118, 119,
			119, 119, 118, 0, 0, 119, 0, 119, 0, 0,
			119, 0, 0, 119, 119, 0, 0, 119, 138, 119,
			119, 0, 119, 0, 119, 119, 119, 0, 119, 119,
			0, 119, 119, 119, 0, 0, 119, 0, 0, 0,
			119, 119, 119, 119, 119, 0, 0, 0, 0, 119,
			0, 0, 0, 119, 119, 0, 0, 0, 0, 0,
			119, 119, 0, 119, 0, 119, 0, 140, 0, 119,
			140, 140, 140, 119, 0, 0, 140, 0, 140, 0,
			0, 140, 0, 0, 140, 140, 0, 0, 140, 121,
			140, 140, 0, 140, 0, 140, 140, 140, 0, 140,
			140, 0, 140, 140, 140, 0, 0, 140, 0, 0,
			0, 140, 140, 140, 140, 0, 0, 0, 0, 0,
			140, 0, 0, 0, 140, 140, 0, 0, 0, 0,
			0, 140, 140, 0, 140, 0, 140, 0, 141, 0,
			140, 141, 141, 141, 140, 0, 0, 141, 0, 141,
			0, 0, 141, 0, 0, 141, 141, 0, 0, 141,
			139, 141, 141, 0, 141, 0, 141, 141, 141, 0,
			141, 141, 0, 141, 141, 141, 0, 0, 141, 0,
			0, 0, 141, 141, 141, 141, 0, 0, 0, 0,
			0, 141, 0, 0, 0, 141, 141, 0, 0, 0,
			0, 0, 141, 141, 0, 141, 0, 141, 0, 120,
			0, 141, 120, 120, 120, 141, 0, 0, 120, 0,
			120, 0, 0, 120, 0, 0, 120, 120, 0, 0,
			120, 145, 120, 120, 0, 120, 0, 120, 0, 120,
			0, 120, 120, 0, 120, 0, 120, 0, 0, 120,
			0, 0, 0, 120, 120, 120, 120, 0, 0, 0,
			0, 0, 120, 0, 0, 0, 120, 120, 0, 0,
			0, 0, 0, 120, 120, 0, 120, 0, 120, 0,
			138, 0, 120, 138, 138, 138, 120, 0, 0, 138,
			0, 138, 0, 0, 138, 0, 0, 138, 138, 0,
			0, 0, 146, 138, 138, 0, 138, 0, 138, 138,
			138, 0, 138, 138, 0, 138, 0, 138, 0, 0,
			138, 0, 0, 0, 138, 138, 138, 138, 0, 0,
			0, 0, 0, 138, 0, 0, 0, 138, 138, 0,
			0, 0, 0, 0, 138, 138, 0, 138, 0, 138,
			0, 121, 0, 138, 121, 121, 121, 138, 0, 0,
			121, 0, 121, 0, 0, 121, 0, 0, 121, 121,
			0, 0, 121, 142, 121, 121, 0, 121, 0, 121,
			0, 121, 0, 121, 121, 0, 121, 0, 121, 0,
			0, 121, 0, 0, 0, 121, 121, 121, 121, 0,
			0, 0, 0, 0, 121, 0, 0, 0, 121, 121,
			0, 0, 0, 0, 0, 121, 121, 0, 121, 0,
			121, 0, 139, 0, 121, 139, 139, 139, 121, 0,
			0, 139, 0, 139, 0, 0, 139, 0, 0, 139,
			139, 0, 0, 0, 143, 139, 139, 0, 139, 0,
			139, 139, 139, 0, 139, 139, 0, 139, 0, 139,
			0, 0, 139, 0, 0, 0, 139, 139, 139, 139,
			0, 0, 0, 0, 0, 139, 0, 0, 0, 139,
			139, 0, 0, 0, 0, 0, 139, 139, 0, 139,
			0, 139, 0, 145, 0, 139, 145, 145, 0, 139,
			0, 144, 145, 0, 145, 0, 0, 145, 0, 0,
			145, 145, 0, 0, 145, 0, 145, 145, 0, 145,
			0, 145, 0, 145, 0, 0, 145, 0, 145, 0,
			145, 0, 0, 0, 0, 0, 0, 145, 145, 145,
			145, 0, 0, 0, 0, 0, 145, 0, 0, 44,
			145, 145, 0, 0, 0, 0, 0, 145, 145, 0,
			145, 0, 145, 0, 146, 0, 145, 146, 146, 0,
			145, 0, 0, 146, 0, 146, 0, 0, 146, 0,
			0, 146, 146, 0, 0, 146, 0, 146, 146, 0,
			146, 0, 146, 0, 146, 0, 0, 146, 0, 146,
			0, 146, 0, 0, 0, 0, 0, 0, 146, 146,
			146, 146, 0, 0, 0, 0, 0, 146, 0, 0,
			0, 146, 146, 0, 0, 0, 0, 0, 146, 146,
			0, 146, 0, 146, 0, 142, 0, 146, 142, 142,
			0, 146, 0, 0, 142, 0, 142, 0, 0, 142,
			0, 0, 142, 142, 0, 0, 142, 0, 142, 142,
			0, 142, 0, 142, 0, 142, 0, 0, 142, 0,
			142, 0, 142, 0, 0, 0, 0, 0, 0, 142,
			142, 142, 142, 0, 0, 0, 0, 0, 142, 0,
			0, 0, 142, 142, 0, 0, 0, 0, 0, 142,
			142, 0, 142, 0, 142, 0, 143, 0, 142, 143,
			143, 0, 142, 0, 0, 143, 0, 143, 0, 0,
			143, 0, 0, 143, 143, 0, 0, 143, 0, 143,
			143, 0, 143, 0, 143, 0, 143, 0, 0, 143,
			0, 143, 0, 143, 0, 0, 0, 0, 0, 0,
			143, 143, 143, 143, 0, 0, 0, 0, 0, 143,
			0, 0, 0, 143, 143, 0, 144, 144, 0, 0,
			143, 143, 144, 143, 144, 143, 0, 144, 0, 143,
			144, 144, 0, 143, 144, 0, 144, 144, 0, 144,
			0, 144, 0, 144, 0, 0, 144, 0, 144, 0,
			144, 0, 0, 0, 0, 0, 0, 144, 144, 144,
			144, 0, 0, 0, 0, 0, 144, 0, 0, 0,
			144, 144, 44, 0, 0, 0, 0, 144, 144, 0,
			144, 0, 144, 0, 0, 44, 144, 44, 0, 44,
			144, 44, 0, 0, 44, 0, 44, 99, 0, 0,
			0, 0, 0, 0, 0, 0, 97, 44, 44, 101,
			411, 98, 0, 0, 44, 412, 0, 0, 0, 0,
			413, 0, 0, 0, 0, 99, 44, 0, 44, 0,
			44, 0, 0, 0, 44, 100, 0, 101, 102, 107,
			0, 0, 103, 0, 0, 0, 104, 0, 108, 109,
			0, 105, 0, 0, 106, 0, 110, 111, 112, 113,
			114, 115, 116, 117, 118, 119, 120, 107, 0, 99,
			0, 238, 0, 0, 0, 0, 108, 109, 97, 0,
			0, 101, 0, 98, 110, 111, 112, 113, 114, 115,
			116, 117, 118, 119, 120, 0, 0, 99, 106, 0,
			0, 0, 0, 0, 0, 0, 0, 100, 0, 101,
			102, 107, 0, 0, 103, 0, 0, 0, 104, 0,
			108, 109, 0, 105, 0, 0, 106, 0, 110, 111,
			112, 113, 114, 115, 116, 117, 118, 119, 120, 107,
			0, 0, 0, 0, 0, 310, 0, 0, 108, 109,
			97, 311, 0, 0, 0, 98, 110, 111, 112, 113,
			114, 115, 116, 117, 118, 119, 120, 0, 0, 99,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 100,
			0, 101, 102, 0, 0, 0, 103, 0, 0, 0,
			104, 0, 0, 0, 0, 105, 0, 0, 106, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 198, 0,
			0, 107, 0, 0, 0, 0, 0, 0, 0, 0,
			108, 109, 97, 0, 0, 202, 0, 98, 110, 111,
			112, 113, 114, 115, 116, 117, 118, 119, 120, 0,
			0, 99, 0, 0, 0, 0, 0, 0, 0, 0,
			97, 100, 0, 101, 102, 98, 0, 0, 103, 0,
			0, 0, 104, 0, 0, 0, 0, 105, 0, 99,
			106, 0, 0, 0, 0, 0, 0, 0, 0, 100,
			0, 101, 102, 107, 0, 0, 103, 0, 0, 0,
			104, 0, 108, 109, 0, 105, 0, 0, 106, 0,
			110, 111, 112, 113, 114, 115, 116, 117, 118, 119,
			120, 107, 0, 0, 0, 0, 0, 203, 0, 0,
			108, 109, 97, 0, 0, 0, 0, 98, 110, 111,
			112, 113, 114, 115, 116, 117, 118, 119, 120, 0,
			0, 99, 0, 0, 0, 0, 0, 0, 0, 0,
			97, 100, 0, 101, 102, 98, 0, 0, 103, 0,
			0, 0, 104, 0, 0, 0, 0, 105, 0, 99,
			106, 0, 0, 0, 0, 0, 0, 0, 0, 100,
			0, 101, 102, 107, 0, 0, 103, 0, 0, 211,
			104, 0, 108, 109, 0, 105, 0, 0, 106, 0,
			110, 111, 112, 113, 114, 115, 116, 117, 118, 119,
			120, 107, 0, 0, 0, 0, 0, 212, 0, 0,
			108, 109, 97, 0, 101, 214, 0, 98, 110, 111,
			112, 113, 114, 115, 116, 117, 118, 119, 120, 0,
			0, 99, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 100, 0, 101, 102, 0, 0, 0, 103, 0,
			0, 0, 104, 108, 109, 0, 0, 105, 0, 0,
			106, 110, 111, 112, 113, 114, 115, 116, 117, 118,
			119, 120, 0, 107, 0, 0, 0, 0, 0, 0,
			0, 0, 108, 109, 97, 0, 0, 218, 0, 98,
			110, 111, 112, 113, 114, 115, 116, 117, 118, 119,
			120, 0, 0, 99, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 100, 0, 101, 102, 0, 0, 0,
			103, 0, 0, 0, 104, 108, 109, 0, 0, 105,
			0, 0, 106, 110, 111, 112, 113, 114, 115, 116,
			117, 118, 119, 120, 0, 107, 0, 0, 0, 0,
			0, 0, 0, 0, 108, 109, 97, 0, 0, 0,
			0, 98, 110, 111, 112, 113, 114, 115, 116, 117,
			118, 119, 120, 0, 0, 99, 0, 0, 0, 0,
			0, 0, 0, 0, 97, 100, 0, 101, 102, 98,
			0, 0, 103, 0, 0, 0, 104, 0, 0, 0,
			0, 105, 0, 99, 106, 0, 0, 0, 0, 0,
			0, 0, 0, 100, 249, 101, 102, 107, 0, 0,
			103, 0, 0, 0, 104, 0, 108, 109, 0, 105,
			0, 0, 106, 0, 110, 111, 112, 113, 114, 115,
			116, 117, 118, 119, 120, 107, 0, 0, 0, 0,
			0, 410, 0, 0, 108, 109, 97, 0, 0, 0,
			0, 98, 110, 111, 112, 113, 114, 115, 116, 117,
			118, 119, 120, 0, 0, 99, 0, 0, 0, 0,
			0, 0, 0, 0, 88, 100, 0, 101, 102, 88,
			0, 0, 103, 0, 0, 0, 104, 0, 0, 0,
			0, 105, 0, 88, 106, 0, 0, 0, 0, 0,
			0, 0, 0, 88, 0, 88, 88, 107, 0, 0,
			88, 0, 0, 0, 88, 0, 108, 109, 0, 88,
			0, 0, 88, 0, 110, 111, 112, 113, 114, 115,
			116, 117, 118, 119, 120, 88, 0, 0, 0, 0,
			0, 0, 0, 0, 88, 88, 90, 0, 0, 0,
			0, 90, 88, 88, 88, 88, 88, 88, 88, 88,
			88, 88, 88, 0, 0, 90, 0, 0, 0, 0,
			0, 0, 0, 0, 148, 90, 0, 90, 90, 148,
			0, 0, 90, 0, 0, 0, 90, 0, 0, 0,
			0, 90, 0, 148, 90, 0, 0, 0, 0, 0,
			0, 0, 0, 148, 0, 148, 148, 90, 0, 0,
			148, 0, 0, 0, 148, 0, 90, 90, 0, 148,
			0, 0, 148, 0, 90, 90, 90, 90, 90, 90,
			90, 90, 90, 90, 90, 148, 0, 99, 0, 291,
			0, 0, 0, 0, 148, 148, 97, 0, 0, 101,
			0, 98, 148, 148, 148, 148, 148, 148, 148, 148,
			148, 148, 148, 0, 0, 99, 106, 0, 0, 0,
			0, 0, 0, 0, 0, 100, 0, 101, 102, 107,
			0, 0, 103, 0, 0, 0, 104, 0, 108, 109,
			0, 0, 0, 0, 106, 0, 110, 111, 112, 113,
			114, 115, 116, 117, 118, 119, 120, 107, 0, 0,
			0, 0, 0, 0, 0, 0, 108, 109, 0, 0,
			0, 0, 0, 0, 110, 111, 112, 113, 114, 115,
			116, 117, 118, 119, 120, 27, 27, 27, 27, 27,
			0, 27, 0, 0, 0, 0, 0, 27, 27, 0,
			0, 0, 27, 27, 0, 27, 27, 0, 0, 0,
			27, 0, 27, 0, 0, 0, 0, 27, 0, 0,
			0, 0, 0, 0, 27, 0, 0, 0, 27, 27,
			27, 27, 0, 27, 0, 0, 0, 0, 0, 0,
			0, 27, 0, 0, 27, 0, 27, 0, 0, 27,
			27, 0, 0, 29, 0, 0, 0, 0, 5, 5,
			5, 5, 27, 0, 5, 0, 27, 0, 27, 27,
			5, 5, 0, 0, 0, 5, 5, 0, 0, 5,
			0, 0, 0, 5, 0, 5, 0, 0, 5, 0,
			5, 0, 0, 0, 0, 0, 0, 5, 0, 0,
			0, 5, 5, 5, 5, 0, 5, 0, 0, 0,
			0, 0, 0, 0, 5, 0, 0, 5, 5, 5,
			0, 0, 0, 5, 0, 0, 0, 0, 0, 0,
			0, 7, 8, 9, 10, 5, 0, 11, 0, 5,
			0, 5, 5, 12, 13, 0, 0, 0, 14, 15,
			0, 0, 16, 0, 0, 0, 17, 0, 18, 0,
			0, 0, 0, 19, 0, 0, 0, 0, 0, 0,
			20, 0, 0, 0, 21, 22, 23, 24, 0, 25,
			0, 0, 0, 0, 0, 0, 0, 26, 0, 108,
			27, 83, 28, 0, 0, 0, 29, 110, 111, 112,
			113, 114, 0, 0, 117, 118, 119, 120, 30, 0,
			0, 0, 31, 0, 32, 33, 7, 8, 9, 10,
			176, 0, 11, 0, 0, 0, 0, 0, 12, 13,
			0, 0, 0, 14, 15, 0, 0, 16, 0, 0,
			0, 17, 0, 18, 0, 0, 0, 0, 19, 0,
			0, 0, 0, 0, 0, 20, 0, 0, 0, 21,
			22, 23, 24, 0, 25, 0, 0, 0, 0, 0,
			0, 0, 26, 0, 0, 27, 0, 28, 0, 0,
			0, 29, 0, 0, 0, 0, 0, 0, 0, 7,
			8, 9, 10, 30, 0, 11, 0, 31, 0, 32,
			33, 12, 13, 0, 0, 0, 14, 15, 0, 0,
			16, 0, 0, 0, 17, 0, 18, 0, 0, 0,
			0, 19, 0, 0, 0, 0, 0, 0, 20, 0,
			0, 0, 21, 22, 23, 24, 0, 25, 0, 0,
			0, 0, 0, 0, 0, 26, 0, 0, 27, 83,
			28, 0, 0, 0, 29, 0, 0, 0, 0, 0,
			0, 0, 7, 8, 9, 10, 221, 0, 11, 0,
			31, 0, 32, 33, 12, 13, 0, 0, 0, 14,
			15, 0, 0, 16, 0, 0, 0, 17, 0, 18,
			0, 0, 0, 0, 19, 0, 0, 0, 0, 0,
			0, 20, 0, 0, 0, 21, 22, 23, 24, 0,
			25, 0, 0, 0, 0, 0, 0, 0, 26, 0,
			0, 27, 0, 28, 0, 0, 0, 29, 0, 0,
			0, 0, 0, 0, 0, 7, 8, 9, 10, 30,
			355, 11, 0, 31, 0, 32, 33, 12, 13, 0,
			0, 0, 14, 15, 0, 0, 16, 0, 0, 0,
			17, 0, 18, 0, 0, 0, 0, 19, 0, 0,
			0, 0, 0, 0, 20, 0, 0, 0, 21, 22,
			23, 24, 0, 25, 0, 0, 0, 0, 0, 0,
			0, 26, 0, 0, 27, 0, 28, 0, 0, 0,
			29, 0, 0, 0, 0, 0, 0, 0, 12, 12,
			12, 12, 30, 358, 12, 0, 31, 0, 32, 33,
			12, 12, 0, 0, 0, 12, 12, 0, 0, 12,
			0, 0, 0, 12, 0, 12, 0, 0, 0, 0,
			12, 0, 0, 0, 0, 0, 0, 12, 0, 0,
			0, 12, 12, 12, 12, 0, 12, 0, 0, 0,
			0, 0, 0, 0, 12, 0, 0, 12, 12, 12,
			0, 0, 0, 12, 0, 0, 0, 0, 0, 0,
			0, 13, 13, 13, 13, 12, 0, 13, 0, 12,
			0, 12, 12, 13, 13, 0, 0, 0, 13, 13,
			0, 0, 13, 0, 0, 0, 13, 0, 13, 0,
			0, 0, 0, 13, 0, 0, 0, 0, 0, 0,
			13, 0, 0, 0, 13, 13, 13, 13, 0, 13,
			0, 0, 0, 0, 0, 0, 0, 13, 0, 0,
			13, 13, 13, 0, 0, 0, 13, 0, 0, 0,
			0, 0, 0, 0, 31, 31, 31, 31, 13, 0,
			31, 0, 13, 0, 13, 13, 31, 31, 0, 0,
			0, 31, 31, 0, 0, 31, 0, 0, 0, 31,
			0, 31, 0, 0, 0, 0, 31, 0, 0, 0,
			0, 0, 0, 31, 0, 0, 0, 31, 31, 31,
			31, 0, 31, 0, 0, 0, 0, 0, 0, 0,
			31, 0, 0, 31, 31, 31, 0, 0, 0, 31,
			0, 0, 0, 0, 0, 0, 0, 31, 31, 31,
			31, 31, 0, 31, 0, 31, 0, 31, 31, 31,
			31, 0, 0, 0, 31, 31, 0, 0, 31, 0,
			0, 0, 31, 0, 31, 0, 0, 0, 0, 31,
			0, 0, 0, 0, 0, 0, 31, 0, 0, 0,
			31, 31, 31, 31, 0, 31, 0, 0, 0, 0,
			0, 0, 0, 31, 0, 0, 31, 0, 31, 0,
			0, 31, 31, 0, 0, 0, 0, 0, 0, 0,
			7, 8, 9, 10, 31, 0, 11, 0, 31, 0,
			31, 31, 12, 13, 0, 0, 0, 14, 15, 0,
			0, 16, 0, 0, 0, 17, 0, 18, 0, 0,
			0, 0, 19, 0, 0, 0, 0, 0, 0, 20,
			0, 0, 0, 21, 22, 23, 24, 0, 25, 0,
			0, 0, 0, 0, 0, 0, 26, 0, 0, 27,
			0, 28, 0, 0, 0, 29, 0, 0, 0, 0,
			0, 0, 0, 7, 8, 9, 10, 30, 0, 11,
			0, 31, 0, 32, 33, 12, 13, 0, 0, 0,
			14, 15, 0, 0, 16, 0, 0, 0, 17, 0,
			18, 0, 0, 0, 0, 19, 0, 0, 0, 0,
			0, 0, 20, 0, 0, 0, 21, 22, 23, 24,
			0, 25, 0, 0, 0, 0, 0, 0, 0, 26,
			0, 0, 27, 0, 28, 0, 0, 0, 29, 0,
			0, 0, 0, 0, 0, 0, 34, 34, 34, 34,
			221, 0, 34, 0, 31, 0, 32, 33, 34, 34,
			0, 0, 0, 34, 34, 0, 0, 34, 0, 0,
			0, 34, 0, 34, 0, 0, 0, 0, 34, 0,
			0, 0, 0, 0, 0, 34, 0, 0, 0, 34,
			34, 34, 34, 0, 34, 0, 0, 0, 0, 0,
			0, 0, 34, 0, 0, 34, 0, 34, 0, 0,
			0, 34, 0, 0, 0, 0, 0, 0, 0, 56,
			56, 56, 56, 34, 0, 56, 0, 34, 0, 34,
			34, 56, 56, 0, 0, 0, 56, 56, 0, 0,
			56, 0, 98, 0, 56, 0, 56, 0, 0, 0,
			0, 56, 0, 0, 0, 0, 99, 0, 56, 0,
			0, 0, 56, 56, 56, 56, 100, 56, 101, 102,
			0, 0, 0, 103, 0, 56, 0, 104, 56, 0,
			56, 0, 105, 0, 56, 106, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 56, 0, 107, 0,
			56, 0, 56, 56, 0, 0, 0, 108, 109, 0,
			0, 0, 98, 0, 0, 110, 111, 112, 113, 114,
			115, 116, 117, 118, 119, 120, 99, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 100, 0, 101, 102,
			0, 0, 0, 103, 0, 0, 0, 104, 0, 0,
			0, 0, 0, 0, 99, 106, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 101, 0, 107, 0,
			0, 103, 0, 0, 0, 0, 0, 108, 109, 0,
			0, 0, 0, 106, 0, 110, 111, 112, 113, 114,
			115, 116, 117, 118, 119, 120, 107, 99, 0, 0,
			0, 0, 0, 0, 0, 108, 109, 0, 0, 101,
			0, 0, 0, 110, 111, 112, 113, 114, 115, 116,
			117, 118, 119, 120, 0, 0, 106, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 101, 0, 107,
			0, 0, 0, 0, 0, 0, 0, 0, 108, 109,
			0, 0, 0, 0, 0, 0, 110, 111, 112, 113,
			114, 115, 116, 117, 118, 119, 120, 107, 0, 0,
			0, 0, 0, 0, 0, 0, 108, 109, 0, 0,
			0, 0, 0, 0, 110, 111, 112, 113, 114, 115,
			116, 117, 118, 119, 120
		};

		// Token: 0x04001C13 RID: 7187
		private static readonly short[] yycheck = new short[]
		{
			1, 257, 258, 324, 258, 72, 263, 93, 350, 95,
			57, 0, 221, 258, 61, 264, 257, 258, 287, 66,
			67, 68, 69, 295, 71, 334, 80, 1, 0, 261,
			295, 0, 79, 0, 81, 261, 261, 309, 329, 0,
			382, 257, 258, 319, 309, 277, 295, 295, 339, 23,
			334, 277, 277, 329, 55, 326, 272, 31, 32, 33,
			269, 309, 319, 257, 258, 151, 0, 266, 277, 278,
			311, 0, 279, 280, 257, 258, 0, 276, 335, 335,
			334, 267, 0, 0, 0, 0, 257, 258, 62, 334,
			64, 65, 159, 140, 161, 0, 0, 313, 72, 73,
			316, 0, 0, 335, 78, 291, 80, 108, 82, 335,
			279, 280, 298, 279, 280, 329, 329, 311, 332, 326,
			331, 335, 335, 97, 98, 99, 100, 101, 311, 103,
			334, 105, 106, 107, 220, 109, 110, 111, 112, 113,
			114, 115, 116, 117, 118, 119, 120, 121, 122, 331,
			331, 262, 334, 334, 128, 332, 130, 326, 334, 202,
			326, 215, 303, 304, 273, 342, 343, 344, 211, 212,
			329, 332, 334, 0, 335, 218, 335, 286, 329, 334,
			154, 332, 156, 292, 335, 159, 295, 161, 297, 159,
			329, 161, 193, 332, 237, 0, 335, 171, 172, 173,
			309, 202, 176, 334, 332, 332, 315, 335, 335, 263,
			211, 212, 329, 214, 257, 273, 334, 218, 335, 329,
			332, 329, 339, 335, 198, 335, 335, 335, 286, 203,
			332, 339, 332, 335, 292, 335, 237, 295, 305, 297,
			334, 215, 309, 244, 287, 288, 329, 221, 329, 332,
			334, 309, 335, 227, 335, 302, 257, 315, 273, 334,
			329, 334, 0, 329, 238, 319, 335, 310, 329, 335,
			317, 286, 329, 329, 335, 249, 262, 292, 335, 335,
			295, 335, 297, 334, 329, 329, 287, 288, 334, 263,
			335, 335, 334, 332, 309, 269, 335, 334, 334, 300,
			315, 273, 345, 277, 278, 306, 334, 285, 351, 310,
			299, 312, 332, 285, 286, 335, 288, 291, 290, 288,
			292, 290, 335, 295, 332, 297, 331, 288, 334, 290,
			319, 305, 335, 280, 306, 309, 308, 309, 335, 308,
			329, 308, 331, 315, 345, 319, 335, 308, 335, 350,
			351, 352, 0, 335, 335, 327, 273, 329, 327, 331,
			329, 335, 331, 335, 331, 299, 335, 410, 335, 286,
			331, 288, 290, 290, 335, 292, 335, 335, 295, 308,
			297, 382, 335, 288, 288, 290, 290, 325, 389, 306,
			308, 308, 309, 334, 368, 327, 370, 331, 315, 400,
			299, 335, 331, 308, 308, 332, 335, 331, 335, 410,
			327, 335, 329, 331, 331, 331, 331, 335, 335, 335,
			335, 395, 327, 424, 329, 265, 331, 331, 429, 403,
			335, 335, 331, 331, 335, 262, 335, 335, 265, 266,
			267, 415, 416, 0, 271, 321, 273, 332, 422, 276,
			335, 329, 279, 280, 281, 288, 283, 334, 285, 286,
			335, 288, 335, 290, 291, 292, 293, 294, 295, 334,
			297, 298, 299, 334, 295, 302, 302, 303, 304, 306,
			307, 308, 309, 310, 335, 290, 335, 264, 315, 284,
			285, 286, 319, 320, 268, 334, 323, 290, 328, 326,
			327, 332, 329, 308, 331, 332, 333, 329, 335, 334,
			334, 295, 339, 340, 341, 342, 343, 344, 345, 346,
			347, 348, 349, 350, 262, 295, 331, 265, 266, 267,
			335, 295, 0, 271, 306, 273, 314, 308, 276, 335,
			268, 279, 280, 281, 335, 283, 334, 285, 286, 335,
			288, 257, 290, 291, 292, 293, 294, 295, 299, 297,
			298, 299, 335, 0, 302, 262, 335, 280, 306, 307,
			308, 309, 310, 262, 285, 285, 5, 315, 1, 1,
			58, 319, 320, 300, 264, 323, 221, 221, 326, 327,
			419, 329, 341, 331, 332, 333, 416, 335, 412, 400,
			-1, 339, 340, 341, 342, 343, 344, 345, 346, 347,
			348, 349, 350, -1, 262, -1, -1, 265, 266, 267,
			-1, 0, -1, 271, -1, 273, -1, -1, 276, -1,
			-1, 279, 280, 281, -1, 283, -1, 285, 286, -1,
			288, -1, 290, 291, 292, 293, 294, 295, -1, 297,
			298, 299, -1, -1, 302, -1, -1, -1, 306, 307,
			308, 309, 310, -1, -1, -1, -1, 315, -1, -1,
			-1, 319, 320, -1, -1, 323, -1, -1, 326, 327,
			-1, 329, -1, 331, 332, 333, -1, 335, -1, -1,
			-1, 339, 340, 341, 342, 343, 344, 345, 346, 347,
			348, 349, 350, -1, -1, 262, -1, -1, 265, 266,
			267, 0, -1, -1, 271, -1, 273, -1, -1, 276,
			-1, -1, 279, 280, 281, -1, 283, -1, 285, 286,
			-1, 288, -1, 290, 291, 292, 293, 294, 295, -1,
			297, 298, 299, -1, -1, 302, -1, -1, -1, 306,
			307, 308, 309, 310, -1, -1, -1, -1, 315, -1,
			-1, -1, 319, 320, -1, -1, 323, -1, -1, 326,
			327, -1, 329, -1, 331, 332, 333, -1, 335, -1,
			-1, -1, 339, 340, 341, 342, 343, 344, 345, 346,
			347, 348, 349, 350, 262, -1, -1, 265, 266, 267,
			0, -1, -1, 271, -1, 273, -1, -1, 276, -1,
			-1, 279, 280, 281, -1, 283, -1, 285, 286, -1,
			288, -1, 290, 291, 292, 293, 294, 295, -1, 297,
			298, 299, -1, -1, 302, -1, -1, -1, 306, 307,
			308, 309, 310, -1, -1, -1, -1, 315, -1, -1,
			-1, 319, 320, -1, -1, 323, -1, -1, 326, 327,
			-1, 329, -1, 331, -1, 333, -1, 335, -1, -1,
			-1, 339, 340, 341, 342, 343, 344, 345, 346, 347,
			348, 349, 350, 262, -1, -1, 265, 266, 267, 0,
			-1, -1, 271, -1, 273, -1, -1, 276, -1, -1,
			279, 280, 281, -1, 283, -1, 285, 286, -1, 288,
			-1, 290, 291, 292, 293, 294, 295, -1, 297, 298,
			299, -1, -1, 302, -1, -1, -1, 306, 307, 308,
			309, 310, -1, -1, -1, -1, 315, -1, -1, -1,
			319, 320, -1, -1, 323, -1, -1, 326, 327, -1,
			329, -1, 331, -1, 333, -1, 335, -1, -1, -1,
			339, 340, 341, 342, 343, 344, 345, 346, 347, 348,
			349, 350, -1, 262, -1, -1, 265, 266, 267, 0,
			-1, -1, 271, -1, 273, -1, -1, 276, -1, -1,
			279, 280, 281, -1, 283, -1, 285, 286, -1, 288,
			-1, 290, 291, 292, 293, 294, 295, -1, 297, 298,
			299, -1, -1, 302, -1, -1, -1, 306, 307, 308,
			309, 310, -1, -1, -1, -1, 315, -1, -1, -1,
			319, 320, -1, -1, 323, -1, -1, 326, 327, -1,
			329, -1, 331, -1, 333, -1, 335, -1, -1, -1,
			339, 340, 341, 342, 343, 344, 345, 346, 347, 348,
			349, 350, 262, -1, -1, 265, 266, 267, 0, -1,
			-1, 271, -1, 273, -1, -1, 276, -1, -1, 279,
			280, 281, -1, 283, -1, 285, 286, -1, 288, -1,
			290, 291, 292, 293, 294, 295, -1, 297, 298, 299,
			-1, -1, 302, -1, -1, -1, 306, 307, 308, 309,
			310, -1, -1, -1, -1, 315, -1, -1, -1, 319,
			320, -1, -1, 323, -1, -1, 326, 327, -1, 329,
			-1, 331, -1, 333, -1, 335, -1, -1, -1, 339,
			340, 341, 342, 343, 344, 345, 346, 347, 348, 349,
			350, 262, -1, -1, 265, 266, 267, 0, -1, -1,
			271, -1, 273, -1, -1, 276, -1, -1, 279, 280,
			281, -1, 283, -1, 285, 286, -1, 288, -1, 290,
			291, 292, 293, 294, 295, 0, 297, 298, 299, -1,
			-1, 302, -1, -1, -1, 306, 307, 308, 309, 310,
			-1, -1, -1, -1, 315, -1, -1, -1, 319, 320,
			-1, -1, 323, -1, -1, 326, 327, -1, 329, -1,
			331, -1, 333, -1, 335, -1, -1, -1, 339, 340,
			341, 342, 343, 344, 345, 346, 347, 348, 349, 350,
			-1, 262, 0, -1, 265, 266, 267, -1, -1, -1,
			271, -1, 273, -1, -1, 276, -1, -1, 279, 280,
			281, -1, 283, -1, 285, 286, -1, 288, -1, 290,
			291, 292, 293, 294, 295, -1, 297, 298, 299, -1,
			-1, 302, -1, -1, -1, 306, 307, 308, 309, 310,
			-1, -1, -1, -1, 315, -1, -1, -1, 319, 320,
			-1, -1, 323, -1, -1, 326, 327, -1, 329, -1,
			331, -1, 333, -1, 335, -1, -1, -1, 339, 340,
			341, -1, -1, -1, 345, 346, 347, 348, 349, 350,
			262, 0, -1, 265, 266, 267, -1, -1, -1, 271,
			-1, 273, -1, -1, 276, -1, -1, 279, 280, 281,
			-1, 283, -1, 285, 286, -1, 288, -1, 290, 291,
			292, 293, 294, 295, -1, 297, 298, 299, -1, -1,
			302, -1, -1, -1, 306, 307, 308, 309, 310, -1,
			-1, -1, -1, 315, -1, -1, -1, 319, 320, -1,
			-1, 323, -1, -1, 326, 327, -1, 329, -1, 331,
			-1, 333, -1, 335, -1, -1, -1, 339, 340, 341,
			-1, -1, -1, 345, 346, 347, 348, 349, 350, 262,
			0, -1, 265, 266, 267, -1, -1, -1, 271, -1,
			273, -1, -1, 276, -1, -1, 279, 280, 281, -1,
			283, -1, 285, 286, -1, 288, -1, 290, 291, 292,
			293, 294, 295, -1, 297, 298, 299, -1, -1, 302,
			-1, -1, -1, 306, 307, 308, 309, 310, -1, -1,
			-1, -1, 315, 288, -1, 290, 319, 320, -1, -1,
			323, -1, -1, 326, 327, -1, 329, -1, 331, -1,
			333, -1, 335, 308, -1, -1, 339, -1, -1, -1,
			-1, -1, 345, 346, 262, -1, -1, 265, 266, 267,
			0, -1, 327, 271, -1, 273, 331, -1, 276, -1,
			335, 279, 280, 281, -1, 283, -1, 285, 286, -1,
			288, -1, 290, 291, 292, 293, 294, 295, -1, 297,
			298, 299, -1, -1, 302, -1, -1, -1, 306, 307,
			308, 309, 310, -1, -1, -1, -1, 315, -1, -1,
			-1, 319, 320, -1, -1, 323, -1, 273, 326, 327,
			-1, 329, -1, 331, -1, 333, 0, 335, -1, -1,
			286, 339, -1, -1, -1, -1, 292, 345, 346, 295,
			-1, 297, -1, 262, -1, -1, 265, 266, 267, -1,
			-1, -1, 271, 309, 273, -1, -1, 276, -1, 315,
			279, 280, 281, -1, 283, -1, 285, 286, -1, 288,
			-1, 290, 291, 292, 293, 294, 295, -1, 297, 298,
			299, -1, -1, 302, -1, -1, -1, 306, 307, 308,
			309, 310, -1, -1, -1, -1, 315, 0, -1, -1,
			319, 320, -1, -1, 323, -1, -1, 326, 327, -1,
			329, -1, 331, -1, 333, -1, 335, -1, -1, -1,
			339, -1, -1, -1, -1, -1, 345, 346, -1, -1,
			-1, -1, 262, -1, -1, 265, 266, 267, -1, -1,
			-1, 271, -1, 273, -1, -1, 276, -1, -1, 279,
			280, 281, -1, 283, -1, 285, 286, -1, 288, -1,
			290, 291, 292, 293, 294, 295, -1, 297, 298, 299,
			-1, -1, 302, 0, -1, -1, 306, 307, 308, 309,
			310, -1, -1, -1, -1, 315, -1, -1, -1, 319,
			320, -1, -1, 323, -1, -1, 326, 327, -1, 329,
			-1, 331, -1, 333, -1, 335, -1, -1, -1, 339,
			-1, -1, -1, -1, -1, 345, 346, 257, 258, 259,
			260, -1, -1, 263, -1, -1, -1, -1, -1, 269,
			270, -1, -1, -1, 274, 275, -1, -1, 278, -1,
			-1, -1, 282, 332, 284, -1, -1, 287, 0, 289,
			-1, 340, 341, 342, 343, 344, 296, -1, -1, -1,
			300, 301, 302, 303, -1, 305, -1, -1, -1, -1,
			-1, -1, -1, 313, -1, -1, 316, 317, 318, -1,
			-1, -1, 322, -1, -1, -1, -1, -1, 262, -1,
			-1, 265, 266, 267, 334, -1, -1, 271, 338, 273,
			340, 341, 276, -1, -1, 279, 280, 281, -1, 283,
			-1, 285, 286, -1, 288, -1, 290, 291, 292, 293,
			294, 295, -1, 297, 298, 299, 0, -1, 302, -1,
			-1, -1, 306, 307, 308, 309, 310, -1, -1, -1,
			-1, 315, -1, -1, -1, 319, 320, -1, -1, 323,
			-1, -1, 326, 327, -1, 329, -1, 331, -1, 262,
			-1, 335, 265, 266, 267, 339, -1, -1, 271, -1,
			273, -1, -1, 276, -1, -1, 279, 280, 281, -1,
			283, -1, 285, 286, -1, 288, -1, 290, 291, 292,
			293, 294, 295, -1, 297, 298, 299, 0, -1, 302,
			-1, -1, -1, 306, 307, 308, 309, 310, -1, -1,
			-1, -1, 315, -1, -1, -1, 319, 320, -1, -1,
			323, -1, -1, 326, 327, -1, 329, -1, 331, -1,
			-1, -1, 335, -1, -1, 262, 339, -1, 265, 266,
			267, -1, -1, -1, 271, -1, 273, -1, -1, 276,
			-1, -1, 279, 280, 281, -1, 283, -1, 285, 286,
			-1, 288, -1, 290, 291, 292, 293, 294, 295, -1,
			297, 298, 299, 0, -1, 302, -1, -1, -1, 306,
			307, 308, 309, 310, -1, -1, -1, -1, 315, -1,
			-1, -1, 319, 320, -1, -1, 323, -1, -1, 326,
			327, -1, 329, -1, 331, -1, -1, -1, 335, -1,
			262, -1, 339, 265, 266, 267, -1, -1, -1, 271,
			-1, 273, -1, -1, 276, -1, -1, 279, 280, 281,
			-1, 283, -1, 285, 286, -1, 288, -1, 290, 291,
			292, 293, 294, 295, 0, 297, 298, 299, -1, -1,
			302, -1, -1, -1, 306, 307, 308, 309, 310, -1,
			-1, -1, -1, 315, -1, -1, -1, 319, 320, -1,
			-1, 323, -1, -1, 326, 327, -1, 329, -1, 331,
			-1, -1, -1, 335, -1, -1, -1, 339, 262, -1,
			-1, 265, 266, 267, -1, -1, -1, 271, -1, 273,
			-1, -1, 276, -1, -1, 279, 280, 281, -1, 283,
			-1, 285, 286, -1, 288, 0, 290, 291, 292, -1,
			294, 295, -1, 297, 298, 299, -1, -1, 302, -1,
			-1, -1, 306, 307, 308, 309, 310, -1, -1, -1,
			-1, 315, -1, -1, -1, 319, 320, -1, -1, 323,
			-1, -1, 326, 327, -1, 329, -1, 331, -1, 262,
			-1, 335, 265, 266, 267, 339, -1, -1, 271, -1,
			273, -1, -1, 276, -1, -1, 279, 280, 281, -1,
			283, -1, 285, 286, -1, 288, 0, 290, 291, 292,
			-1, 294, 295, -1, 297, 298, 299, -1, -1, 302,
			-1, -1, -1, 306, 307, 308, 309, 310, -1, -1,
			-1, -1, 315, -1, -1, -1, 319, 320, -1, -1,
			323, -1, -1, 326, 327, -1, 329, -1, 331, -1,
			-1, -1, 335, -1, -1, 262, 339, -1, 265, 266,
			267, -1, -1, -1, 271, -1, 273, -1, -1, 276,
			-1, -1, 279, 280, 281, -1, 283, 0, 285, 286,
			-1, 288, -1, 290, 291, 292, -1, 294, 295, -1,
			297, 298, 299, -1, -1, 302, -1, -1, -1, 306,
			307, 308, 309, 310, -1, -1, -1, -1, 315, -1,
			-1, -1, 319, 320, -1, -1, -1, -1, -1, 326,
			327, -1, 329, -1, 331, -1, 262, -1, 335, 265,
			266, 267, 339, -1, -1, 271, -1, 273, -1, -1,
			276, -1, -1, 279, 280, -1, -1, 283, 0, 285,
			286, -1, 288, -1, 290, 291, 292, -1, 294, 295,
			-1, 297, 298, 299, -1, -1, 302, -1, -1, -1,
			306, 307, 308, 309, 310, -1, -1, -1, -1, 315,
			-1, -1, -1, 319, 320, -1, -1, -1, -1, -1,
			326, 327, -1, 329, -1, 331, -1, 262, -1, 335,
			265, 266, 267, 339, -1, -1, 271, -1, 273, -1,
			-1, 276, -1, -1, 279, 280, -1, -1, 283, 0,
			285, 286, -1, 288, -1, 290, 291, 292, -1, 294,
			295, -1, 297, 298, 299, -1, -1, 302, -1, -1,
			-1, 306, 307, 308, 309, -1, -1, -1, -1, -1,
			315, -1, -1, -1, 319, 320, -1, -1, -1, -1,
			-1, 326, 327, -1, 329, -1, 331, -1, 262, -1,
			335, 265, 266, 267, 339, -1, -1, 271, -1, 273,
			-1, -1, 276, -1, -1, 279, 280, -1, -1, 283,
			0, 285, 286, -1, 288, -1, 290, 291, 292, -1,
			294, 295, -1, 297, 298, 299, -1, -1, 302, -1,
			-1, -1, 306, 307, 308, 309, -1, -1, -1, -1,
			-1, 315, -1, -1, -1, 319, 320, -1, -1, -1,
			-1, -1, 326, 327, -1, 329, -1, 331, -1, 262,
			-1, 335, 265, 266, 267, 339, -1, -1, 271, -1,
			273, -1, -1, 276, -1, -1, 279, 280, -1, -1,
			283, 0, 285, 286, -1, 288, -1, 290, -1, 292,
			-1, 294, 295, -1, 297, -1, 299, -1, -1, 302,
			-1, -1, -1, 306, 307, 308, 309, -1, -1, -1,
			-1, -1, 315, -1, -1, -1, 319, 320, -1, -1,
			-1, -1, -1, 326, 327, -1, 329, -1, 331, -1,
			262, -1, 335, 265, 266, 267, 339, -1, -1, 271,
			-1, 273, -1, -1, 276, -1, -1, 279, 280, -1,
			-1, -1, 0, 285, 286, -1, 288, -1, 290, 291,
			292, -1, 294, 295, -1, 297, -1, 299, -1, -1,
			302, -1, -1, -1, 306, 307, 308, 309, -1, -1,
			-1, -1, -1, 315, -1, -1, -1, 319, 320, -1,
			-1, -1, -1, -1, 326, 327, -1, 329, -1, 331,
			-1, 262, -1, 335, 265, 266, 267, 339, -1, -1,
			271, -1, 273, -1, -1, 276, -1, -1, 279, 280,
			-1, -1, 283, 0, 285, 286, -1, 288, -1, 290,
			-1, 292, -1, 294, 295, -1, 297, -1, 299, -1,
			-1, 302, -1, -1, -1, 306, 307, 308, 309, -1,
			-1, -1, -1, -1, 315, -1, -1, -1, 319, 320,
			-1, -1, -1, -1, -1, 326, 327, -1, 329, -1,
			331, -1, 262, -1, 335, 265, 266, 267, 339, -1,
			-1, 271, -1, 273, -1, -1, 276, -1, -1, 279,
			280, -1, -1, -1, 0, 285, 286, -1, 288, -1,
			290, 291, 292, -1, 294, 295, -1, 297, -1, 299,
			-1, -1, 302, -1, -1, -1, 306, 307, 308, 309,
			-1, -1, -1, -1, -1, 315, -1, -1, -1, 319,
			320, -1, -1, -1, -1, -1, 326, 327, -1, 329,
			-1, 331, -1, 262, -1, 335, 265, 266, -1, 339,
			-1, 0, 271, -1, 273, -1, -1, 276, -1, -1,
			279, 280, -1, -1, 283, -1, 285, 286, -1, 288,
			-1, 290, -1, 292, -1, -1, 295, -1, 297, -1,
			299, -1, -1, -1, -1, -1, -1, 306, 307, 308,
			309, -1, -1, -1, -1, -1, 315, -1, -1, 0,
			319, 320, -1, -1, -1, -1, -1, 326, 327, -1,
			329, -1, 331, -1, 262, -1, 335, 265, 266, -1,
			339, -1, -1, 271, -1, 273, -1, -1, 276, -1,
			-1, 279, 280, -1, -1, 283, -1, 285, 286, -1,
			288, -1, 290, -1, 292, -1, -1, 295, -1, 297,
			-1, 299, -1, -1, -1, -1, -1, -1, 306, 307,
			308, 309, -1, -1, -1, -1, -1, 315, -1, -1,
			-1, 319, 320, -1, -1, -1, -1, -1, 326, 327,
			-1, 329, -1, 331, -1, 262, -1, 335, 265, 266,
			-1, 339, -1, -1, 271, -1, 273, -1, -1, 276,
			-1, -1, 279, 280, -1, -1, 283, -1, 285, 286,
			-1, 288, -1, 290, -1, 292, -1, -1, 295, -1,
			297, -1, 299, -1, -1, -1, -1, -1, -1, 306,
			307, 308, 309, -1, -1, -1, -1, -1, 315, -1,
			-1, -1, 319, 320, -1, -1, -1, -1, -1, 326,
			327, -1, 329, -1, 331, -1, 262, -1, 335, 265,
			266, -1, 339, -1, -1, 271, -1, 273, -1, -1,
			276, -1, -1, 279, 280, -1, -1, 283, -1, 285,
			286, -1, 288, -1, 290, -1, 292, -1, -1, 295,
			-1, 297, -1, 299, -1, -1, -1, -1, -1, -1,
			306, 307, 308, 309, -1, -1, -1, -1, -1, 315,
			-1, -1, -1, 319, 320, -1, 265, 266, -1, -1,
			326, 327, 271, 329, 273, 331, -1, 276, -1, 335,
			279, 280, -1, 339, 283, -1, 285, 286, -1, 288,
			-1, 290, -1, 292, -1, -1, 295, -1, 297, -1,
			299, -1, -1, -1, -1, -1, -1, 306, 307, 308,
			309, -1, -1, -1, -1, -1, 315, -1, -1, -1,
			319, 320, 273, -1, -1, -1, -1, 326, 327, -1,
			329, -1, 331, -1, -1, 286, 335, 288, -1, 290,
			339, 292, -1, -1, 295, -1, 297, 281, -1, -1,
			-1, -1, -1, -1, -1, -1, 262, 308, 309, 293,
			266, 267, -1, -1, 315, 271, -1, -1, -1, -1,
			276, -1, -1, -1, -1, 281, 327, -1, 329, -1,
			331, -1, -1, -1, 335, 291, -1, 293, 294, 323,
			-1, -1, 298, -1, -1, -1, 302, -1, 332, 333,
			-1, 307, -1, -1, 310, -1, 340, 341, 342, 343,
			344, 345, 346, 347, 348, 349, 350, 323, -1, 281,
			-1, 283, -1, -1, -1, -1, 332, 333, 262, -1,
			-1, 293, -1, 267, 340, 341, 342, 343, 344, 345,
			346, 347, 348, 349, 350, -1, -1, 281, 310, -1,
			-1, -1, -1, -1, -1, -1, -1, 291, -1, 293,
			294, 323, -1, -1, 298, -1, -1, -1, 302, -1,
			332, 333, -1, 307, -1, -1, 310, -1, 340, 341,
			342, 343, 344, 345, 346, 347, 348, 349, 350, 323,
			-1, -1, -1, -1, -1, 329, -1, -1, 332, 333,
			262, 335, -1, -1, -1, 267, 340, 341, 342, 343,
			344, 345, 346, 347, 348, 349, 350, -1, -1, 281,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, 291,
			-1, 293, 294, -1, -1, -1, 298, -1, -1, -1,
			302, -1, -1, -1, -1, 307, -1, -1, 310, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, 320, -1,
			-1, 323, -1, -1, -1, -1, -1, -1, -1, -1,
			332, 333, 262, -1, -1, 265, -1, 267, 340, 341,
			342, 343, 344, 345, 346, 347, 348, 349, 350, -1,
			-1, 281, -1, -1, -1, -1, -1, -1, -1, -1,
			262, 291, -1, 293, 294, 267, -1, -1, 298, -1,
			-1, -1, 302, -1, -1, -1, -1, 307, -1, 281,
			310, -1, -1, -1, -1, -1, -1, -1, -1, 291,
			-1, 293, 294, 323, -1, -1, 298, -1, -1, -1,
			302, -1, 332, 333, -1, 307, -1, -1, 310, -1,
			340, 341, 342, 343, 344, 345, 346, 347, 348, 349,
			350, 323, -1, -1, -1, -1, -1, 329, -1, -1,
			332, 333, 262, -1, -1, -1, -1, 267, 340, 341,
			342, 343, 344, 345, 346, 347, 348, 349, 350, -1,
			-1, 281, -1, -1, -1, -1, -1, -1, -1, -1,
			262, 291, -1, 293, 294, 267, -1, -1, 298, -1,
			-1, -1, 302, -1, -1, -1, -1, 307, -1, 281,
			310, -1, -1, -1, -1, -1, -1, -1, -1, 291,
			-1, 293, 294, 323, -1, -1, 298, -1, -1, 329,
			302, -1, 332, 333, -1, 307, -1, -1, 310, -1,
			340, 341, 342, 343, 344, 345, 346, 347, 348, 349,
			350, 323, -1, -1, -1, -1, -1, 329, -1, -1,
			332, 333, 262, -1, 293, 265, -1, 267, 340, 341,
			342, 343, 344, 345, 346, 347, 348, 349, 350, -1,
			-1, 281, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, 291, -1, 293, 294, -1, -1, -1, 298, -1,
			-1, -1, 302, 332, 333, -1, -1, 307, -1, -1,
			310, 340, 341, 342, 343, 344, 345, 346, 347, 348,
			349, 350, -1, 323, -1, -1, -1, -1, -1, -1,
			-1, -1, 332, 333, 262, -1, -1, 265, -1, 267,
			340, 341, 342, 343, 344, 345, 346, 347, 348, 349,
			350, -1, -1, 281, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, 291, -1, 293, 294, -1, -1, -1,
			298, -1, -1, -1, 302, 332, 333, -1, -1, 307,
			-1, -1, 310, 340, 341, 342, 343, 344, 345, 346,
			347, 348, 349, 350, -1, 323, -1, -1, -1, -1,
			-1, -1, -1, -1, 332, 333, 262, -1, -1, -1,
			-1, 267, 340, 341, 342, 343, 344, 345, 346, 347,
			348, 349, 350, -1, -1, 281, -1, -1, -1, -1,
			-1, -1, -1, -1, 262, 291, -1, 293, 294, 267,
			-1, -1, 298, -1, -1, -1, 302, -1, -1, -1,
			-1, 307, -1, 281, 310, -1, -1, -1, -1, -1,
			-1, -1, -1, 291, 320, 293, 294, 323, -1, -1,
			298, -1, -1, -1, 302, -1, 332, 333, -1, 307,
			-1, -1, 310, -1, 340, 341, 342, 343, 344, 345,
			346, 347, 348, 349, 350, 323, -1, -1, -1, -1,
			-1, 329, -1, -1, 332, 333, 262, -1, -1, -1,
			-1, 267, 340, 341, 342, 343, 344, 345, 346, 347,
			348, 349, 350, -1, -1, 281, -1, -1, -1, -1,
			-1, -1, -1, -1, 262, 291, -1, 293, 294, 267,
			-1, -1, 298, -1, -1, -1, 302, -1, -1, -1,
			-1, 307, -1, 281, 310, -1, -1, -1, -1, -1,
			-1, -1, -1, 291, -1, 293, 294, 323, -1, -1,
			298, -1, -1, -1, 302, -1, 332, 333, -1, 307,
			-1, -1, 310, -1, 340, 341, 342, 343, 344, 345,
			346, 347, 348, 349, 350, 323, -1, -1, -1, -1,
			-1, -1, -1, -1, 332, 333, 262, -1, -1, -1,
			-1, 267, 340, 341, 342, 343, 344, 345, 346, 347,
			348, 349, 350, -1, -1, 281, -1, -1, -1, -1,
			-1, -1, -1, -1, 262, 291, -1, 293, 294, 267,
			-1, -1, 298, -1, -1, -1, 302, -1, -1, -1,
			-1, 307, -1, 281, 310, -1, -1, -1, -1, -1,
			-1, -1, -1, 291, -1, 293, 294, 323, -1, -1,
			298, -1, -1, -1, 302, -1, 332, 333, -1, 307,
			-1, -1, 310, -1, 340, 341, 342, 343, 344, 345,
			346, 347, 348, 349, 350, 323, -1, 281, -1, 283,
			-1, -1, -1, -1, 332, 333, 262, -1, -1, 293,
			-1, 267, 340, 341, 342, 343, 344, 345, 346, 347,
			348, 349, 350, -1, -1, 281, 310, -1, -1, -1,
			-1, -1, -1, -1, -1, 291, -1, 293, 294, 323,
			-1, -1, 298, -1, -1, -1, 302, -1, 332, 333,
			-1, -1, -1, -1, 310, -1, 340, 341, 342, 343,
			344, 345, 346, 347, 348, 349, 350, 323, -1, -1,
			-1, -1, -1, -1, -1, -1, 332, 333, -1, -1,
			-1, -1, -1, -1, 340, 341, 342, 343, 344, 345,
			346, 347, 348, 349, 350, 257, 258, 259, 260, 261,
			-1, 263, -1, -1, -1, -1, -1, 269, 270, -1,
			-1, -1, 274, 275, -1, 277, 278, -1, -1, -1,
			282, -1, 284, -1, -1, -1, -1, 289, -1, -1,
			-1, -1, -1, -1, 296, -1, -1, -1, 300, 301,
			302, 303, -1, 305, -1, -1, -1, -1, -1, -1,
			-1, 313, -1, -1, 316, -1, 318, -1, -1, 321,
			322, -1, -1, 325, -1, -1, -1, -1, 257, 258,
			259, 260, 334, -1, 263, -1, 338, -1, 340, 341,
			269, 270, -1, -1, -1, 274, 275, -1, -1, 278,
			-1, -1, -1, 282, -1, 284, -1, -1, 287, -1,
			289, -1, -1, -1, -1, -1, -1, 296, -1, -1,
			-1, 300, 301, 302, 303, -1, 305, -1, -1, -1,
			-1, -1, -1, -1, 313, -1, -1, 316, 317, 318,
			-1, -1, -1, 322, -1, -1, -1, -1, -1, -1,
			-1, 257, 258, 259, 260, 334, -1, 263, -1, 338,
			-1, 340, 341, 269, 270, -1, -1, -1, 274, 275,
			-1, -1, 278, -1, -1, -1, 282, -1, 284, -1,
			-1, -1, -1, 289, -1, -1, -1, -1, -1, -1,
			296, -1, -1, -1, 300, 301, 302, 303, -1, 305,
			-1, -1, -1, -1, -1, -1, -1, 313, -1, 332,
			316, 317, 318, -1, -1, -1, 322, 340, 341, 342,
			343, 344, -1, -1, 347, 348, 349, 350, 334, -1,
			-1, -1, 338, -1, 340, 341, 257, 258, 259, 260,
			261, -1, 263, -1, -1, -1, -1, -1, 269, 270,
			-1, -1, -1, 274, 275, -1, -1, 278, -1, -1,
			-1, 282, -1, 284, -1, -1, -1, -1, 289, -1,
			-1, -1, -1, -1, -1, 296, -1, -1, -1, 300,
			301, 302, 303, -1, 305, -1, -1, -1, -1, -1,
			-1, -1, 313, -1, -1, 316, -1, 318, -1, -1,
			-1, 322, -1, -1, -1, -1, -1, -1, -1, 257,
			258, 259, 260, 334, -1, 263, -1, 338, -1, 340,
			341, 269, 270, -1, -1, -1, 274, 275, -1, -1,
			278, -1, -1, -1, 282, -1, 284, -1, -1, -1,
			-1, 289, -1, -1, -1, -1, -1, -1, 296, -1,
			-1, -1, 300, 301, 302, 303, -1, 305, -1, -1,
			-1, -1, -1, -1, -1, 313, -1, -1, 316, 317,
			318, -1, -1, -1, 322, -1, -1, -1, -1, -1,
			-1, -1, 257, 258, 259, 260, 334, -1, 263, -1,
			338, -1, 340, 341, 269, 270, -1, -1, -1, 274,
			275, -1, -1, 278, -1, -1, -1, 282, -1, 284,
			-1, -1, -1, -1, 289, -1, -1, -1, -1, -1,
			-1, 296, -1, -1, -1, 300, 301, 302, 303, -1,
			305, -1, -1, -1, -1, -1, -1, -1, 313, -1,
			-1, 316, -1, 318, -1, -1, -1, 322, -1, -1,
			-1, -1, -1, -1, -1, 257, 258, 259, 260, 334,
			335, 263, -1, 338, -1, 340, 341, 269, 270, -1,
			-1, -1, 274, 275, -1, -1, 278, -1, -1, -1,
			282, -1, 284, -1, -1, -1, -1, 289, -1, -1,
			-1, -1, -1, -1, 296, -1, -1, -1, 300, 301,
			302, 303, -1, 305, -1, -1, -1, -1, -1, -1,
			-1, 313, -1, -1, 316, -1, 318, -1, -1, -1,
			322, -1, -1, -1, -1, -1, -1, -1, 257, 258,
			259, 260, 334, 335, 263, -1, 338, -1, 340, 341,
			269, 270, -1, -1, -1, 274, 275, -1, -1, 278,
			-1, -1, -1, 282, -1, 284, -1, -1, -1, -1,
			289, -1, -1, -1, -1, -1, -1, 296, -1, -1,
			-1, 300, 301, 302, 303, -1, 305, -1, -1, -1,
			-1, -1, -1, -1, 313, -1, -1, 316, 317, 318,
			-1, -1, -1, 322, -1, -1, -1, -1, -1, -1,
			-1, 257, 258, 259, 260, 334, -1, 263, -1, 338,
			-1, 340, 341, 269, 270, -1, -1, -1, 274, 275,
			-1, -1, 278, -1, -1, -1, 282, -1, 284, -1,
			-1, -1, -1, 289, -1, -1, -1, -1, -1, -1,
			296, -1, -1, -1, 300, 301, 302, 303, -1, 305,
			-1, -1, -1, -1, -1, -1, -1, 313, -1, -1,
			316, 317, 318, -1, -1, -1, 322, -1, -1, -1,
			-1, -1, -1, -1, 257, 258, 259, 260, 334, -1,
			263, -1, 338, -1, 340, 341, 269, 270, -1, -1,
			-1, 274, 275, -1, -1, 278, -1, -1, -1, 282,
			-1, 284, -1, -1, -1, -1, 289, -1, -1, -1,
			-1, -1, -1, 296, -1, -1, -1, 300, 301, 302,
			303, -1, 305, -1, -1, -1, -1, -1, -1, -1,
			313, -1, -1, 316, 317, 318, -1, -1, -1, 322,
			-1, -1, -1, -1, -1, -1, -1, 257, 258, 259,
			260, 334, -1, 263, -1, 338, -1, 340, 341, 269,
			270, -1, -1, -1, 274, 275, -1, -1, 278, -1,
			-1, -1, 282, -1, 284, -1, -1, -1, -1, 289,
			-1, -1, -1, -1, -1, -1, 296, -1, -1, -1,
			300, 301, 302, 303, -1, 305, -1, -1, -1, -1,
			-1, -1, -1, 313, -1, -1, 316, -1, 318, -1,
			-1, 321, 322, -1, -1, -1, -1, -1, -1, -1,
			257, 258, 259, 260, 334, -1, 263, -1, 338, -1,
			340, 341, 269, 270, -1, -1, -1, 274, 275, -1,
			-1, 278, -1, -1, -1, 282, -1, 284, -1, -1,
			-1, -1, 289, -1, -1, -1, -1, -1, -1, 296,
			-1, -1, -1, 300, 301, 302, 303, -1, 305, -1,
			-1, -1, -1, -1, -1, -1, 313, -1, -1, 316,
			-1, 318, -1, -1, -1, 322, -1, -1, -1, -1,
			-1, -1, -1, 257, 258, 259, 260, 334, -1, 263,
			-1, 338, -1, 340, 341, 269, 270, -1, -1, -1,
			274, 275, -1, -1, 278, -1, -1, -1, 282, -1,
			284, -1, -1, -1, -1, 289, -1, -1, -1, -1,
			-1, -1, 296, -1, -1, -1, 300, 301, 302, 303,
			-1, 305, -1, -1, -1, -1, -1, -1, -1, 313,
			-1, -1, 316, -1, 318, -1, -1, -1, 322, -1,
			-1, -1, -1, -1, -1, -1, 257, 258, 259, 260,
			334, -1, 263, -1, 338, -1, 340, 341, 269, 270,
			-1, -1, -1, 274, 275, -1, -1, 278, -1, -1,
			-1, 282, -1, 284, -1, -1, -1, -1, 289, -1,
			-1, -1, -1, -1, -1, 296, -1, -1, -1, 300,
			301, 302, 303, -1, 305, -1, -1, -1, -1, -1,
			-1, -1, 313, -1, -1, 316, -1, 318, -1, -1,
			-1, 322, -1, -1, -1, -1, -1, -1, -1, 257,
			258, 259, 260, 334, -1, 263, -1, 338, -1, 340,
			341, 269, 270, -1, -1, -1, 274, 275, -1, -1,
			278, -1, 267, -1, 282, -1, 284, -1, -1, -1,
			-1, 289, -1, -1, -1, -1, 281, -1, 296, -1,
			-1, -1, 300, 301, 302, 303, 291, 305, 293, 294,
			-1, -1, -1, 298, -1, 313, -1, 302, 316, -1,
			318, -1, 307, -1, 322, 310, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, 334, -1, 323, -1,
			338, -1, 340, 341, -1, -1, -1, 332, 333, -1,
			-1, -1, 267, -1, -1, 340, 341, 342, 343, 344,
			345, 346, 347, 348, 349, 350, 281, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, 291, -1, 293, 294,
			-1, -1, -1, 298, -1, -1, -1, 302, -1, -1,
			-1, -1, -1, -1, 281, 310, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, 293, -1, 323, -1,
			-1, 298, -1, -1, -1, -1, -1, 332, 333, -1,
			-1, -1, -1, 310, -1, 340, 341, 342, 343, 344,
			345, 346, 347, 348, 349, 350, 323, 281, -1, -1,
			-1, -1, -1, -1, -1, 332, 333, -1, -1, 293,
			-1, -1, -1, 340, 341, 342, 343, 344, 345, 346,
			347, 348, 349, 350, -1, -1, 310, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, 293, -1, 323,
			-1, -1, -1, -1, -1, -1, -1, -1, 332, 333,
			-1, -1, -1, -1, -1, -1, 340, 341, 342, 343,
			344, 345, 346, 347, 348, 349, 350, 323, -1, -1,
			-1, -1, -1, -1, -1, -1, 332, 333, -1, -1,
			-1, -1, -1, -1, 340, 341, 342, 343, 344, 345,
			346, 347, 348, 349, 350
		};

		// Token: 0x04001C14 RID: 7188
		private static short YYFINAL = 2;

		// Token: 0x04001C15 RID: 7189
		private static short YYMAXTOKEN = 352;

		// Token: 0x04001C16 RID: 7190
		private static readonly string[] yyname = new string[]
		{
			"end-of-file", null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, null, null, null,
			null, null, null, null, null, null, null, "IDENTIFIER", "ESCAPED_IDENTIFIER", "PARAMETER",
			"LITERAL", "ALL", "AND", "ANYELEMENT", "APPLY", "AS", "ASC", "BETWEEN", "BY", "CASE",
			"CAST", "COLLATE", "COLLECTION", "CROSS", "CREATEREF", "DEREF", "DESC", "DISTINCT", "ELEMENT", "ELSE",
			"END", "EXCEPT", "EXISTS", "ESCAPE", "FLATTEN", "FROM", "FULL", "FUNCTION", "GROUP", "GROUPPARTITION",
			"HAVING", "IN", "INNER", "INTERSECT", "IS", "JOIN", "KEY", "LEFT", "LIKE", "LIMIT",
			"MULTISET", "NAVIGATE", "NOT", "NULL", "OF", "OFTYPE", "ON", "OR", "ORDER", "OUTER",
			"OVERLAPS", "ONLY", "QMARK", "REF", "RELATIONSHIP", "RIGHT", "ROW", "SELECT", "SET", "SKIP",
			"THEN", "TOP", "TREAT", "UNION", "USING", "VALUE", "WHEN", "WHERE", "WITH", "COMMA",
			"COLON", "SCOLON", "DOT", "EQUAL", "L_PAREN", "R_PAREN", "L_BRACE", "R_BRACE", "L_CURLY", "R_CURLY",
			"PLUS", "MINUS", "STAR", "FSLASH", "PERCENT", "OP_EQ", "OP_NEQ", "OP_LT", "OP_LE", "OP_GT",
			"OP_GE", "UNARYPLUS", "UNARYMINUS"
		};

		// Token: 0x04001C17 RID: 7191
		private static string[] yyrule = new string[]
		{
			"$accept : commandStart", "commandStart :", "commandStart : command", "command : optNamespaceImportList queryStatement", "optNamespaceImportList :", "optNamespaceImportList : namespaceImportList", "namespaceImportList : namespaceImport", "namespaceImportList : namespaceImportList namespaceImport", "namespaceImport : USING identifier SCOLON", "namespaceImport : USING dotExpr SCOLON",
			"namespaceImport : USING assignExpr SCOLON", "queryStatement : optQueryDefList generalExpr optSemiColon", "optQueryDefList :", "optQueryDefList : functionDefList", "functionDefList : functionDef", "functionDefList : functionDefList functionDef", "functionDef : FUNCTION identifier functionParamsDef AS L_PAREN generalExpr R_PAREN", "functionParamsDef : L_PAREN R_PAREN", "functionParamsDef : L_PAREN functionParamDefList R_PAREN", "functionParamDefList : functionParamDef",
			"functionParamDefList : functionParamDefList COMMA functionParamDef", "functionParamDef : identifier typeDef", "generalExpr : queryExpr", "generalExpr : Expr", "optSemiColon :", "optSemiColon : SCOLON", "queryExpr : selectClause fromClause optWhereClause optGroupByClause optHavingClause optOrderByClause", "$$1 :", "selectClause : SELECT $$1 optAllOrDistinct optTopClause aliasExprList", "$$2 :",
			"selectClause : SELECT $$2 VALUE optAllOrDistinct optTopClause aliasExprList", "optAllOrDistinct :", "optAllOrDistinct : ALL", "optAllOrDistinct : DISTINCT", "optTopClause :", "optTopClause : TOP L_PAREN generalExpr R_PAREN", "fromClause : FROM fromClauseList", "fromClauseList : fromClauseItem", "fromClauseList : fromClauseList COMMA fromClauseItem", "fromClauseItem : aliasExpr",
			"fromClauseItem : L_PAREN joinClauseItem R_PAREN", "fromClauseItem : joinClauseItem", "fromClauseItem : L_PAREN applyClauseItem R_PAREN", "fromClauseItem : applyClauseItem", "joinClauseItem : fromClauseItem joinType fromClauseItem", "joinClauseItem : fromClauseItem joinType fromClauseItem ON Expr", "applyClauseItem : fromClauseItem applyType fromClauseItem", "joinType : CROSS JOIN", "joinType : LEFT OUTER JOIN", "joinType : LEFT JOIN",
			"joinType : RIGHT OUTER JOIN", "joinType : RIGHT JOIN", "joinType : JOIN", "joinType : INNER JOIN", "joinType : FULL JOIN", "joinType : FULL OUTER JOIN", "joinType : FULL OUTER", "applyType : CROSS APPLY", "applyType : OUTER APPLY", "optWhereClause :",
			"optWhereClause : whereClause", "whereClause : WHERE Expr", "optGroupByClause :", "optGroupByClause : groupByClause", "groupByClause : GROUP BY aliasExprList", "optHavingClause :", "optHavingClause : havingClause", "$$3 :", "havingClause : HAVING $$3 Expr", "optOrderByClause :",
			"optOrderByClause : orderByClause", "$$4 :", "orderByClause : ORDER BY $$4 orderByItemList optSkipSubClause optLimitSubClause", "optSkipSubClause :", "optSkipSubClause : SKIP Expr", "optLimitSubClause :", "optLimitSubClause : LIMIT Expr", "orderByItemList : orderByClauseItem", "orderByItemList : orderByItemList COMMA orderByClauseItem", "orderByClauseItem : Expr optAscDesc",
			"orderByClauseItem : Expr COLLATE simpleIdentifier optAscDesc", "optAscDesc :", "optAscDesc : ASC", "optAscDesc : DESC", "exprList : Expr", "exprList : exprList COMMA Expr", "Expr : parenExpr", "Expr : PARAMETER", "Expr : identifier", "Expr : builtInExpr",
			"Expr : dotExpr", "Expr : refExpr", "Expr : createRefExpr", "Expr : keyExpr", "Expr : groupPartitionExpr", "Expr : methodExpr", "Expr : ctorExpr", "Expr : derefExpr", "Expr : navigateExpr", "Expr : literalExpr",
			"parenExpr : L_PAREN generalExpr R_PAREN", "betweenPrefix : Expr BETWEEN Expr", "notBetweenPrefix : Expr NOT BETWEEN Expr", "builtInExpr : Expr PLUS Expr", "builtInExpr : Expr MINUS Expr", "builtInExpr : Expr STAR Expr", "builtInExpr : Expr FSLASH Expr", "builtInExpr : Expr PERCENT Expr", "builtInExpr : MINUS Expr", "builtInExpr : PLUS Expr",
			"builtInExpr : Expr OP_NEQ Expr", "builtInExpr : Expr OP_GT Expr", "builtInExpr : Expr OP_GE Expr", "builtInExpr : Expr OP_LT Expr", "builtInExpr : Expr OP_LE Expr", "builtInExpr : Expr INTERSECT Expr", "builtInExpr : Expr UNION Expr", "builtInExpr : Expr UNION ALL Expr", "builtInExpr : Expr EXCEPT Expr", "builtInExpr : Expr OVERLAPS Expr",
			"builtInExpr : Expr IN Expr", "builtInExpr : Expr NOT IN Expr", "builtInExpr : EXISTS L_PAREN generalExpr R_PAREN", "builtInExpr : ANYELEMENT L_PAREN generalExpr R_PAREN", "builtInExpr : ELEMENT L_PAREN generalExpr R_PAREN", "builtInExpr : FLATTEN L_PAREN generalExpr R_PAREN", "builtInExpr : SET L_PAREN generalExpr R_PAREN", "builtInExpr : Expr IS NULL", "builtInExpr : Expr IS NOT NULL", "builtInExpr : searchedCaseExpr",
			"builtInExpr : TREAT L_PAREN Expr AS typeName R_PAREN", "builtInExpr : CAST L_PAREN Expr AS typeName R_PAREN", "builtInExpr : OFTYPE L_PAREN Expr COMMA typeName R_PAREN", "builtInExpr : OFTYPE L_PAREN Expr COMMA ONLY typeName R_PAREN", "builtInExpr : Expr IS OF L_PAREN typeName R_PAREN", "builtInExpr : Expr IS NOT OF L_PAREN typeName R_PAREN", "builtInExpr : Expr IS OF L_PAREN ONLY typeName R_PAREN", "builtInExpr : Expr IS NOT OF L_PAREN ONLY typeName R_PAREN", "builtInExpr : Expr LIKE Expr", "builtInExpr : Expr NOT LIKE Expr",
			"builtInExpr : Expr LIKE Expr ESCAPE Expr", "builtInExpr : Expr NOT LIKE Expr ESCAPE Expr", "builtInExpr : betweenPrefix AND Expr", "builtInExpr : notBetweenPrefix AND Expr", "builtInExpr : Expr OR Expr", "builtInExpr : NOT Expr", "builtInExpr : Expr AND Expr", "builtInExpr : equalsOrAssignExpr", "equalsOrAssignExpr : assignExpr", "equalsOrAssignExpr : equalsExpr",
			"assignExpr : Expr EQUAL Expr", "equalsExpr : Expr OP_EQ Expr", "aliasExpr : Expr AS identifier", "aliasExpr : Expr", "aliasExprList : aliasExpr", "aliasExprList : aliasExprList COMMA aliasExpr", "searchedCaseExpr : CASE whenThenExprList END", "searchedCaseExpr : CASE whenThenExprList caseElseExpr END", "whenThenExprList : WHEN Expr THEN Expr", "whenThenExprList : whenThenExprList WHEN Expr THEN Expr",
			"caseElseExpr : ELSE Expr", "ctorExpr : ROW L_PAREN aliasExprList R_PAREN", "ctorExpr : MULTISET L_PAREN exprList R_PAREN", "ctorExpr : L_CURLY exprList R_CURLY", "dotExpr : Expr DOT identifier", "refExpr : REF L_PAREN generalExpr R_PAREN", "derefExpr : DEREF L_PAREN generalExpr R_PAREN", "createRefExpr : CREATEREF L_PAREN Expr COMMA Expr R_PAREN", "createRefExpr : CREATEREF L_PAREN Expr COMMA Expr COMMA typeName R_PAREN", "keyExpr : KEY L_PAREN generalExpr R_PAREN",
			"groupPartitionExpr : GROUPPARTITION L_PAREN optAllOrDistinct generalExpr R_PAREN", "methodExpr : dotExpr L_PAREN R_PAREN", "methodExpr : dotExpr L_PAREN optAllOrDistinct exprList R_PAREN optWithRelationship", "methodExpr : dotExpr L_PAREN optAllOrDistinct queryExpr R_PAREN optWithRelationship", "methodExpr : identifier L_PAREN R_PAREN", "methodExpr : identifier L_PAREN optAllOrDistinct exprList R_PAREN optWithRelationship", "methodExpr : identifier L_PAREN optAllOrDistinct queryExpr R_PAREN optWithRelationship", "navigateExpr : NAVIGATE L_PAREN Expr COMMA typeName R_PAREN", "navigateExpr : NAVIGATE L_PAREN Expr COMMA typeName COMMA identifier R_PAREN", "navigateExpr : NAVIGATE L_PAREN Expr COMMA typeName COMMA identifier COMMA identifier R_PAREN",
			"optWithRelationship :", "optWithRelationship : relationshipList", "relationshipList : WITH relationshipExpr", "relationshipList : relationshipList relationshipExpr", "relationshipExpr : RELATIONSHIP L_PAREN Expr COMMA typeName R_PAREN", "relationshipExpr : RELATIONSHIP L_PAREN Expr COMMA typeName COMMA identifier R_PAREN", "relationshipExpr : RELATIONSHIP L_PAREN Expr COMMA typeName COMMA identifier COMMA identifier R_PAREN", "typeName : identifier", "typeName : qualifiedTypeName", "typeName : identifier ESCAPED_IDENTIFIER",
			"typeName : qualifiedTypeName ESCAPED_IDENTIFIER", "typeName : typeNameWithTypeSpec", "qualifiedTypeName : typeName DOT identifier", "typeNameWithTypeSpec : qualifiedTypeName L_PAREN R_PAREN", "typeNameWithTypeSpec : qualifiedTypeName L_PAREN exprList R_PAREN", "typeNameWithTypeSpec : identifier L_PAREN R_PAREN", "typeNameWithTypeSpec : identifier L_PAREN exprList R_PAREN", "identifier : ESCAPED_IDENTIFIER", "identifier : simpleIdentifier", "simpleIdentifier : IDENTIFIER",
			"literalExpr : LITERAL", "literalExpr : NULL", "typeDef : typeName", "typeDef : collectionTypeDef", "typeDef : refTypeDef", "typeDef : rowTypeDef", "collectionTypeDef : COLLECTION L_PAREN typeDef R_PAREN", "refTypeDef : REF L_PAREN typeName R_PAREN", "rowTypeDef : ROW L_PAREN propertyDefList R_PAREN", "propertyDefList : propertyDef",
			"propertyDefList : propertyDefList COMMA propertyDef", "propertyDef : identifier typeDef"
		};

		// Token: 0x04001C18 RID: 7192
		private Node _parsedTree;

		// Token: 0x04001C19 RID: 7193
		private CqlLexer _lexer;

		// Token: 0x04001C1A RID: 7194
		private string _query;

		// Token: 0x04001C1B RID: 7195
		private readonly ParserOptions _parserOptions;

		// Token: 0x04001C1C RID: 7196
		private const string _internalYaccSyntaxErrorMessage = "syntax error";

		// Token: 0x04001C1D RID: 7197
		private uint _methodExprCounter;

		// Token: 0x04001C1E RID: 7198
		private Stack<uint> _methodExprCounterStack;
	}
}
