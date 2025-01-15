using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000990 RID: 2448
	public class Parser
	{
		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06004BB7 RID: 19383 RVA: 0x0012ECF6 File Offset: 0x0012CEF6
		// (set) Token: 0x06004BB8 RID: 19384 RVA: 0x0012ECFE File Offset: 0x0012CEFE
		private string SqlStatement { get; set; }

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06004BB9 RID: 19385 RVA: 0x0012ED07 File Offset: 0x0012CF07
		// (set) Token: 0x06004BBA RID: 19386 RVA: 0x0012ED0F File Offset: 0x0012CF0F
		public Parser.StatementType Type { get; private set; }

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06004BBB RID: 19387 RVA: 0x0012ED18 File Offset: 0x0012CF18
		// (set) Token: 0x06004BBC RID: 19388 RVA: 0x0012ED20 File Offset: 0x0012CF20
		public Parser.Syntax SyntaxFlavor { get; set; }

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06004BBD RID: 19389 RVA: 0x0012ED29 File Offset: 0x0012CF29
		// (set) Token: 0x06004BBE RID: 19390 RVA: 0x0012ED31 File Offset: 0x0012CF31
		public List<string> QualifiedNameParts { get; private set; }

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06004BBF RID: 19391 RVA: 0x0012ED3A File Offset: 0x0012CF3A
		// (set) Token: 0x06004BC0 RID: 19392 RVA: 0x0012ED42 File Offset: 0x0012CF42
		public string QualifiedName { get; private set; }

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x0012ED4B File Offset: 0x0012CF4B
		// (set) Token: 0x06004BC2 RID: 19394 RVA: 0x0012ED53 File Offset: 0x0012CF53
		public List<string> AsColumnList { get; private set; }

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06004BC3 RID: 19395 RVA: 0x0012ED5C File Offset: 0x0012CF5C
		// (set) Token: 0x06004BC4 RID: 19396 RVA: 0x0012ED64 File Offset: 0x0012CF64
		public List<object> ParameterList { get; private set; }

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06004BC5 RID: 19397 RVA: 0x0012ED6D File Offset: 0x0012CF6D
		// (set) Token: 0x06004BC6 RID: 19398 RVA: 0x0012ED75 File Offset: 0x0012CF75
		internal List<string> Statements { get; private set; }

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06004BC7 RID: 19399 RVA: 0x0012ED7E File Offset: 0x0012CF7E
		// (set) Token: 0x06004BC8 RID: 19400 RVA: 0x0012ED86 File Offset: 0x0012CF86
		internal int ParameterNumber { get; private set; }

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06004BC9 RID: 19401 RVA: 0x0012ED8F File Offset: 0x0012CF8F
		// (set) Token: 0x06004BCA RID: 19402 RVA: 0x0012ED97 File Offset: 0x0012CF97
		internal TokenData CurrentSemiColonData { get; set; }

		// Token: 0x06004BCB RID: 19403 RVA: 0x0012EDA0 File Offset: 0x0012CFA0
		static Parser()
		{
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Select, "SELECT", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.SelectAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Static, "STATIC", new Action<LinkedListNode<TokenData>>[]
			{
				null,
				new Action<LinkedListNode<TokenData>>(Parser.StaticAction1)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Call, "CALL", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.CallAction),
				new Action<LinkedListNode<TokenData>>(Parser.CallAction1)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Exec, "EXEC", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.CallAction),
				new Action<LinkedListNode<TokenData>>(Parser.CallAction1)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Set, "SET", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.SetAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.With, "WITH", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.WithAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.From, "FROM", new Action<LinkedListNode<TokenData>>[]
			{
				null,
				new Action<LinkedListNode<TokenData>>(Parser.FromAction1)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Update, "UPDATE", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.UpdateAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Insert, "INSERT", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.InsertAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Delete, "DELETE", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.DeleteAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Create, "CREATE", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.CreateAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Drop, "DROP", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.DropAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Grant, "GRANT", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.GrantAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Into, "INTO", new Action<LinkedListNode<TokenData>>[]
			{
				new Action<LinkedListNode<TokenData>>(Parser.IntoAction)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.As, "AS", new Action<LinkedListNode<TokenData>>[]
			{
				null,
				new Action<LinkedListNode<TokenData>>(Parser.AsAction1)
			}));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Begin, "BEGIN", new Action<LinkedListNode<TokenData>>[2]));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.End, "END", new Action<LinkedListNode<TokenData>>[2]));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.If, "IF", new Action<LinkedListNode<TokenData>>[2]));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Procedure, "PROCEDURE", new Action<LinkedListNode<TokenData>>[2]));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Function, "FUNCTION", new Action<LinkedListNode<TokenData>>[2]));
			Parser._tokenTree.AddSupportedToken(new Token(TokenType.Replace, "REPLACE", new Action<LinkedListNode<TokenData>>[2]));
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x0012F13C File Offset: 0x0012D33C
		public Parser(FlagBasedTracePoint tracePoint)
		{
			if (tracePoint == null)
			{
				this._tracePoint = new ApplicationRequesterTracePoint(Parser._traceContainer);
			}
			else
			{
				this._tracePoint = tracePoint;
			}
			this.QualifiedName = string.Empty;
			this.SyntaxFlavor = Parser.Syntax.DB2;
			this.QualifiedNameParts = new List<string>();
			this.AsColumnList = new List<string>();
			this.ParameterList = new List<object>();
			this.Statements = new List<string>();
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x0012F1BC File Offset: 0x0012D3BC
		public string CreateQualifiedName(List<string> parts, bool isQuoted)
		{
			if (parts == null || parts.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < parts.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(".");
				}
				if (parts[i].Length > 0)
				{
					if (isQuoted)
					{
						stringBuilder.Append("\"");
					}
					stringBuilder.Append(parts[i]);
					if (isQuoted)
					{
						stringBuilder.Append("\"");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x0012F244 File Offset: 0x0012D444
		public void ParseFullyQualifiedName(string fullyQualifiedName)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Parsing qualified name:  " + fullyQualifiedName);
			}
			this.Reset();
			this.SqlStatement = fullyQualifiedName;
			this.BuildTokenList(fullyQualifiedName);
			Parser.ParseQualifiedName(this._tokenDataList.AddFirst(new TokenData(TokenType.Text, null, this, -1, -1, 1)));
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Parsed qualified name.");
			}
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0012F2C8 File Offset: 0x0012D4C8
		public void Parse(string sql)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Parsing Sql statment:  " + sql);
			}
			this.Reset();
			this.SqlStatement = sql;
			this.BuildTokenList(sql);
			if (this._tracePoint.IsEnabled(TraceFlags.Verbose))
			{
				this._tracePoint.Trace(TraceFlags.Verbose, "Token list has been built.");
			}
			this.AdjustScopeWeight();
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Processing token list...");
			}
			for (int i = 0; i < 2; i++)
			{
				LinkedListNode<TokenData> linkedListNode = this._tokenDataList.First;
				this.CurrentSemiColonData = null;
				while (linkedListNode != null)
				{
					if (i == 0 && this._tracePoint.IsEnabled(TraceFlags.Debug))
					{
						this._tracePoint.Trace(TraceFlags.Debug, string.Format("Token data: {1} ( {2} ~ {3} ) : {0}", new object[]
						{
							linkedListNode.Value.TokenType,
							linkedListNode.Value.ScopeWeight,
							linkedListNode.Value.StartIndex,
							linkedListNode.Value.EndIndex
						}));
					}
					if (linkedListNode.Value.TokenActions != null && linkedListNode.Value.TokenActions.Length > i && linkedListNode.Value.TokenActions[i] != null)
					{
						linkedListNode.Value.TokenActions[i](linkedListNode);
					}
					linkedListNode = linkedListNode.Next;
				}
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Parsed Sql statment.");
			}
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x0012F468 File Offset: 0x0012D668
		private void AdjustScopeWeight()
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Adjusting the scope weight of token list...");
			}
			LinkedListNode<TokenData> node = this._tokenDataList.First;
			int weightDiff = 0;
			bool flag = false;
			bool adjusted = false;
			Action action = delegate
			{
				node.Value.ScopeWeight += weightDiff;
				node = node.Next;
				adjusted = true;
			};
			Func<Func<LinkedListNode<TokenData>, bool>, bool> func = delegate(Func<LinkedListNode<TokenData>, bool> isExpected)
			{
				if (node.Next != null && isExpected(node.Next))
				{
					int num2 = node.Value.EndIndex + 1;
					int num3 = node.Next.Value.StartIndex - 1;
					int num4 = num2;
					while (num4 <= num3 && char.IsWhiteSpace(this.SqlStatement, num2))
					{
						if (num4 == num3)
						{
							return true;
						}
						num4++;
					}
				}
				return false;
			};
			while (node != null)
			{
				adjusted = false;
				switch (node.Value.TokenType)
				{
				case TokenType.Create:
				case TokenType.Replace:
					if (this.SyntaxFlavor == Parser.Syntax.Informix)
					{
						if (func((LinkedListNode<TokenData> nextNode) => nextNode.Value.TokenType == TokenType.Procedure || nextNode.Value.TokenType == TokenType.Function))
						{
							action();
							int num = weightDiff + 1;
							weightDiff = num;
						}
					}
					break;
				case TokenType.Begin:
				{
					action();
					int num = weightDiff + 1;
					weightDiff = num;
					break;
				}
				case TokenType.End:
				{
					if (flag)
					{
						if (func((LinkedListNode<TokenData> nextNode) => nextNode.Value.TokenType == TokenType.If))
						{
							action();
							flag = false;
							break;
						}
					}
					int num = weightDiff - 1;
					weightDiff = num;
					break;
				}
				case TokenType.If:
					flag = true;
					break;
				}
				if (!adjusted)
				{
					action();
				}
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Completed adjusting the scope weight of token list");
			}
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x0012F61C File Offset: 0x0012D81C
		private void BuildTokenList(string sql)
		{
			int i = 0;
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			int num = -1;
			int num2 = 1;
			bool flag4 = false;
			while (i < sql.Length)
			{
				int num3 = 1;
				char c = sql[i];
				TokenType tokenType = TokenType.SingleQuotedText;
				if (c <= '?')
				{
					switch (c)
					{
					case '"':
					case '\'':
						if (!flag2 && !flag3)
						{
							num = i;
						}
						if (!flag4)
						{
							if (c == '"')
							{
								if (!flag3)
								{
									flag2 = !flag2;
									tokenType = TokenType.DoubleQuotedText;
								}
							}
							else if (!flag2)
							{
								flag3 = !flag3;
								tokenType = TokenType.SingleQuotedText;
							}
						}
						flag = !flag2 && !flag3;
						if (flag && num < i - 1)
						{
							this._tokenDataList.AddLast(new TokenData(tokenType, null, this, num + 1, i - 1, num2));
						}
						break;
					case '#':
					case '$':
					case '%':
					case '&':
						goto IL_03D3;
					case '(':
						if (!flag2 && !flag3)
						{
							num2++;
							this._tokenDataList.AddLast(new TokenData(TokenType.LeftParenthesis, null, this, i, i, num2));
							flag = true;
						}
						break;
					case ')':
						if (!flag2 && !flag3)
						{
							this._tokenDataList.AddLast(new TokenData(TokenType.RightParenthesis, null, this, i, i, num2));
							num2--;
							flag = true;
						}
						break;
					case '*':
						if (!flag2 && !flag3)
						{
							this._tokenDataList.AddLast(new TokenData(TokenType.Asterisk, null, this, i, i, num2));
						}
						break;
					case '+':
						if (!flag2 && !flag3)
						{
							this._tokenDataList.AddLast(new TokenData(TokenType.Plus, null, this, i, i, num2));
						}
						break;
					case ',':
						if (!flag2 && !flag3)
						{
							this._tokenDataList.AddLast(new TokenData(TokenType.Comma, null, this, i, i, num2));
							flag = true;
						}
						break;
					case '-':
						if (!flag2 && !flag3)
						{
							this._tokenDataList.AddLast(new TokenData(TokenType.Minus, null, this, i, i, num2));
						}
						if (i != 0 && sql[i - 1] == '-')
						{
							flag4 = true;
						}
						break;
					case '.':
						if (!flag2 && !flag3)
						{
							this._tokenDataList.AddLast(new TokenData(TokenType.Period, null, this, i, i, num2));
						}
						break;
					default:
						switch (c)
						{
						case ':':
							if (!flag2 && !flag3)
							{
								this._tokenDataList.AddLast(new TokenData(TokenType.Colon, null, this, i, i, num2));
							}
							break;
						case ';':
							if (!flag2 && !flag3)
							{
								this._tokenDataList.AddLast(new TokenData(TokenType.SemiColon, Parser.semiColonActionArray, this, i, i, num2));
							}
							break;
						case '<':
							if (!flag2 && !flag3)
							{
								this._tokenDataList.AddLast(new TokenData(TokenType.LessThan, null, this, i, i, num2));
								flag = true;
							}
							break;
						case '=':
							if (!flag2 && !flag3)
							{
								this._tokenDataList.AddLast(new TokenData(TokenType.Equals, null, this, i, i, num2));
								flag = true;
							}
							break;
						case '>':
							if (!flag2 && !flag3)
							{
								this._tokenDataList.AddLast(new TokenData(TokenType.GreaterThan, null, this, i, i, num2));
								flag = true;
							}
							break;
						case '?':
							if (!flag2 && !flag3)
							{
								this._tokenDataList.AddLast(new TokenData(TokenType.QuestionMark, null, this, i, i, num2));
								int num4 = this.ParameterNumber + 1;
								this.ParameterNumber = num4;
							}
							break;
						default:
							goto IL_03D3;
						}
						break;
					}
				}
				else if (c != '^')
				{
					if (c != '|')
					{
						goto IL_03D3;
					}
					if (!flag2 && !flag3 && i > 0 && sql[i - 1] == '|')
					{
						this._tokenDataList.AddLast(new TokenData(TokenType.Merge, null, this, i - 1, i, num2));
					}
				}
				else if (!flag2 && !flag3)
				{
					this._tokenDataList.AddLast(new TokenData(TokenType.Caret, null, this, i, i, num2));
				}
				IL_0433:
				i += num3;
				continue;
				IL_03D3:
				if (char.IsWhiteSpace(c))
				{
					if (!flag2 && !flag3)
					{
						flag = true;
						goto IL_0433;
					}
					goto IL_0433;
				}
				else
				{
					if (!flag)
					{
						goto IL_0433;
					}
					Token token = Parser._tokenTree.ParseToken(sql, i, ref num3);
					if (token != null)
					{
						this._tokenDataList.AddLast(new TokenData(token.Type, token.TokenActions, this, i, i + num3 - 1, num2));
						goto IL_0433;
					}
					if (!flag2 && !flag3)
					{
						flag = false;
					}
					num3 = 1;
					goto IL_0433;
				}
			}
			this._tokenDataList.AddLast(new TokenData(TokenType.SemiColon, Parser.semiColonActionArray, this, sql.Length, sql.Length, num2));
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x0012FA94 File Offset: 0x0012DC94
		private void SetStatementType(Parser.StatementType type, int scopeWeight)
		{
			if ((this._currentTypeWeight == -1 || this._currentTypeWeight > scopeWeight) && type != Parser.StatementType.With)
			{
				this._currentTypeWeight = scopeWeight;
				this.Type = type;
				return;
			}
			if (this._currentTypeWeight == scopeWeight && this.Type == Parser.StatementType.With && this.Type != type)
			{
				this.Type = type;
			}
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0012FAE8 File Offset: 0x0012DCE8
		internal void Reset()
		{
			this.Type = Parser.StatementType.Unknown;
			this._currentTypeWeight = -1;
			this._tokenDataList.Clear();
			this.QualifiedName = string.Empty;
			this.QualifiedNameParts.Clear();
			this.AsColumnList.Clear();
			this.ParameterList.Clear();
			this.Statements.Clear();
			this.ParameterNumber = 0;
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x0012FB4C File Offset: 0x0012DD4C
		private static void SelectAction(LinkedListNode<TokenData> node)
		{
			node.Value.Parser.SetStatementType(Parser.StatementType.Select, node.Value.ScopeWeight);
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x0012FB6C File Offset: 0x0012DD6C
		private static void CallAction(LinkedListNode<TokenData> node)
		{
			if (node == node.List.First)
			{
				if (node.Next != null && node.Next.Value.TokenType == TokenType.Static)
				{
					node.Value.Parser.SetStatementType(Parser.StatementType.Static, node.Value.ScopeWeight);
					return;
				}
				node.Value.Parser.SetStatementType(Parser.StatementType.Call, node.Value.ScopeWeight);
			}
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x0012FBDC File Offset: 0x0012DDDC
		private static void SetAction(LinkedListNode<TokenData> node)
		{
			node.Value.Parser.SetStatementType(Parser.StatementType.Set, node.Value.ScopeWeight);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0012FBFA File Offset: 0x0012DDFA
		private static void WithAction(LinkedListNode<TokenData> node)
		{
			node.Value.Parser.SetStatementType(Parser.StatementType.With, node.Value.ScopeWeight);
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0012FC18 File Offset: 0x0012DE18
		private static void UpdateAction(LinkedListNode<TokenData> node)
		{
			node.Value.Parser.SetStatementType(Parser.StatementType.Update, node.Value.ScopeWeight);
			Parser.ParseQualifiedName(node);
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x0012FC3C File Offset: 0x0012DE3C
		private static void InsertAction(LinkedListNode<TokenData> node)
		{
			node.Value.Parser.SetStatementType(Parser.StatementType.Insert, node.Value.ScopeWeight);
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x0012FC5A File Offset: 0x0012DE5A
		private static void DeleteAction(LinkedListNode<TokenData> node)
		{
			node.Value.Parser.SetStatementType(Parser.StatementType.Delete, node.Value.ScopeWeight);
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x0012FC78 File Offset: 0x0012DE78
		private static void CreateAction(LinkedListNode<TokenData> node)
		{
			node.Value.Parser.SetStatementType(Parser.StatementType.Create, node.Value.ScopeWeight);
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x0012FC96 File Offset: 0x0012DE96
		private static void DropAction(LinkedListNode<TokenData> node)
		{
			node.Value.Parser.SetStatementType(Parser.StatementType.Drop, node.Value.ScopeWeight);
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0012FCB5 File Offset: 0x0012DEB5
		private static void GrantAction(LinkedListNode<TokenData> node)
		{
			if (node.Previous == null)
			{
				node.Value.Parser.SetStatementType(Parser.StatementType.Grant, node.Value.ScopeWeight);
			}
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0012FCDC File Offset: 0x0012DEDC
		private static void StaticAction1(LinkedListNode<TokenData> node)
		{
			if (node.Value.Parser.Type == Parser.StatementType.Static && node.Value.Parser._currentTypeWeight == node.Value.ScopeWeight)
			{
				Parser.ParseQualifiedName(node);
				Parser.ParseParameters(node);
			}
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0012FD1A File Offset: 0x0012DF1A
		private static void CallAction1(LinkedListNode<TokenData> node)
		{
			if (node.Value.Parser.Type == Parser.StatementType.Call && node.Value.Parser._currentTypeWeight == node.Value.ScopeWeight)
			{
				Parser.ParseQualifiedName(node);
				Parser.ParseParameters(node);
			}
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0012FD58 File Offset: 0x0012DF58
		private static void AsAction1(LinkedListNode<TokenData> node)
		{
			if (node.Value.Parser.Type == Parser.StatementType.Static && node.Value.Parser._currentTypeWeight == node.Value.ScopeWeight)
			{
				if (node.Next == null || node.Next.Value.TokenType != TokenType.LeftParenthesis)
				{
					return;
				}
				Parser.ParseColumnList(node.Next, node.Value.Parser.AsColumnList);
			}
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x0012FDCC File Offset: 0x0012DFCC
		private static void SemiColonAction(LinkedListNode<TokenData> node)
		{
			int num = 0;
			if (node.Value.ScopeWeight != 1)
			{
				return;
			}
			if (node.Value.Parser.CurrentSemiColonData != null)
			{
				num = node.Value.Parser.CurrentSemiColonData.EndIndex + 1;
			}
			int num2 = node.Value.EndIndex - 1;
			if (num2 > num)
			{
				string text = node.Value.Parser.SqlStatement.Substring(num, num2 - num + 1).Trim();
				if (text.Length > 0)
				{
					node.Value.Parser.Statements.Add(text);
				}
			}
			node.Value.Parser.CurrentSemiColonData = node.Value;
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0012FE7C File Offset: 0x0012E07C
		private static void ParseParameters(LinkedListNode<TokenData> node)
		{
			while (node != null && node.Value.TokenType != TokenType.LeftParenthesis)
			{
				if (node.Value.TokenType == TokenType.As)
				{
					return;
				}
				node = node.Next;
			}
			if (node == null)
			{
				return;
			}
			Parser.ParseColumnList(node, node.Value.Parser.ParameterList);
			for (int i = 0; i < node.Value.Parser.ParameterList.Count; i++)
			{
				string text = (string)node.Value.Parser.ParameterList[i];
				if (string.CompareOrdinal(text, "?") == 0)
				{
					node.Value.Parser.ParameterList[i] = null;
				}
				else if (string.Compare(text, "null", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					node.Value.Parser.ParameterList[i] = DBNull.Value;
				}
				else
				{
					string text2 = text;
					if (text.Length >= 2 && text[0] == '\'' && text[text.Length - 1] == '\'')
					{
						text2 = text.Substring(1, text.Length - 2);
					}
					node.Value.Parser.ParameterList[i] = text2.Replace("''", "'");
				}
			}
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0012FFC4 File Offset: 0x0012E1C4
		private static void FromAction1(LinkedListNode<TokenData> node)
		{
			if ((node.Value.Parser.Type == Parser.StatementType.Select || node.Value.Parser.Type == Parser.StatementType.Delete) && node.Value.Parser._currentTypeWeight == node.Value.ScopeWeight)
			{
				Parser.ParseQualifiedName(node);
			}
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x00130019 File Offset: 0x0012E219
		private static void IntoAction(LinkedListNode<TokenData> node)
		{
			if (node.Value.Parser.Type == Parser.StatementType.Insert && node.Value.Parser._currentTypeWeight == node.Value.ScopeWeight)
			{
				Parser.ParseQualifiedName(node);
			}
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x00130054 File Offset: 0x0012E254
		private static void ParseQualifiedName(LinkedListNode<TokenData> node)
		{
			int i = node.Value.EndIndex + 1;
			string sqlStatement = node.Value.Parser.SqlStatement;
			while (i < sqlStatement.Length && char.IsWhiteSpace(sqlStatement[i]))
			{
				i++;
			}
			if (i == sqlStatement.Length)
			{
				return;
			}
			int num = i;
			int num2 = num;
			i = num + 1;
			LinkedListNode<TokenData> linkedListNode = node;
			while (i < sqlStatement.Length)
			{
				while (linkedListNode.Next != null && linkedListNode.Next.Value.StartIndex <= i)
				{
					linkedListNode = linkedListNode.Next;
				}
				if (linkedListNode.Value.EndIndex >= i)
				{
					TokenType tokenType = linkedListNode.Value.TokenType;
					if (tokenType <= TokenType.DoubleQuotedText)
					{
						i++;
						continue;
					}
					if (tokenType == TokenType.Period)
					{
						if (linkedListNode.Value.StartIndex > num2)
						{
							Parser.AddStringToList(node.Value.Parser.QualifiedNameParts, sqlStatement.Substring(num2, linkedListNode.Value.StartIndex - num2));
						}
						num2 = linkedListNode.Value.EndIndex + 1;
					}
				}
				if (Node.IsDelimiter(sqlStatement[i]))
				{
					break;
				}
				i++;
			}
			i--;
			if (i >= num2)
			{
				Parser.AddStringToList(node.Value.Parser.QualifiedNameParts, sqlStatement.Substring(num2, i - num2 + 1));
			}
			if (i >= num)
			{
				node.Value.Parser.QualifiedName = sqlStatement.Substring(num, i - num + 1).Trim();
			}
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x001301C4 File Offset: 0x0012E3C4
		private static void ParseColumnList(LinkedListNode<TokenData> node, IList columns)
		{
			columns.Clear();
			string sqlStatement = node.Value.Parser.SqlStatement;
			int num = node.Value.EndIndex + 1;
			bool flag = false;
			LinkedListNode<TokenData> linkedListNode = node.Next;
			while (!flag)
			{
				if (linkedListNode == null || linkedListNode.Value.TokenType == TokenType.RightParenthesis)
				{
					break;
				}
				if (linkedListNode.Value.TokenType == TokenType.Comma)
				{
					if (linkedListNode.Value.StartIndex > num)
					{
						Parser.AddStringToList(columns, sqlStatement.Substring(num, linkedListNode.Value.StartIndex - num).Trim());
					}
					num = linkedListNode.Value.EndIndex + 1;
				}
				linkedListNode = linkedListNode.Next;
			}
			int num2 = ((linkedListNode == null) ? (sqlStatement.Length - 1) : (linkedListNode.Value.StartIndex - 1));
			if (num2 >= num)
			{
				Parser.AddStringToList(columns, sqlStatement.Substring(num, num2 - num + 1).Trim());
			}
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x001302A4 File Offset: 0x0012E4A4
		private static void AddStringToList(IList listString, string part)
		{
			if (part.Length < 2)
			{
				listString.Add(part);
				return;
			}
			string text = part;
			if (part[0] == '"' && part[part.Length - 1] == '"')
			{
				if (part.Length == 2)
				{
					text = string.Empty;
				}
				else
				{
					text = part.Substring(1, part.Length - 2);
				}
			}
			listString.Add(text);
		}

		// Token: 0x04003BB9 RID: 15289
		private static TokenTree _tokenTree = new TokenTree();

		// Token: 0x04003BBA RID: 15290
		private static DrdaClientTraceContainer _traceContainer = new DrdaClientTraceContainer();

		// Token: 0x04003BBB RID: 15291
		private FlagBasedTracePoint _tracePoint;

		// Token: 0x04003BBC RID: 15292
		private LinkedList<TokenData> _tokenDataList = new LinkedList<TokenData>();

		// Token: 0x04003BBD RID: 15293
		private int _currentTypeWeight = -1;

		// Token: 0x04003BC8 RID: 15304
		private static Action<LinkedListNode<TokenData>>[] semiColonActionArray = new Action<LinkedListNode<TokenData>>[]
		{
			new Action<LinkedListNode<TokenData>>(Parser.SemiColonAction)
		};

		// Token: 0x02000991 RID: 2449
		public enum StatementType : short
		{
			// Token: 0x04003BCA RID: 15306
			Unknown = -1,
			// Token: 0x04003BCB RID: 15307
			Select,
			// Token: 0x04003BCC RID: 15308
			Static,
			// Token: 0x04003BCD RID: 15309
			Call,
			// Token: 0x04003BCE RID: 15310
			Set,
			// Token: 0x04003BCF RID: 15311
			With,
			// Token: 0x04003BD0 RID: 15312
			Insert,
			// Token: 0x04003BD1 RID: 15313
			Delete,
			// Token: 0x04003BD2 RID: 15314
			Update,
			// Token: 0x04003BD3 RID: 15315
			Create,
			// Token: 0x04003BD4 RID: 15316
			Drop,
			// Token: 0x04003BD5 RID: 15317
			Grant
		}

		// Token: 0x02000992 RID: 2450
		public enum Syntax : short
		{
			// Token: 0x04003BD7 RID: 15319
			DB2,
			// Token: 0x04003BD8 RID: 15320
			Informix
		}
	}
}
