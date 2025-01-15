using System;
using System.Collections.Generic;
using System.Globalization;
using antlr;
using antlr.collections.impl;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000CF RID: 207
	internal abstract class TSql80ParserBaseInternal : LLkParser
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x0001DD8D File Offset: 0x0001BF8D
		protected TSql80ParserBaseInternal(TokenBuffer tokenBuf, int k)
			: base(tokenBuf, k)
		{
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001DDB7 File Offset: 0x0001BFB7
		protected TSql80ParserBaseInternal(ParserSharedInputState state, int k)
			: base(state, k)
		{
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001DDE1 File Offset: 0x0001BFE1
		protected TSql80ParserBaseInternal(TokenStream lexer, int k)
			: base(lexer, k)
		{
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001DE0B File Offset: 0x0001C00B
		public TSql80ParserBaseInternal(bool initialQuotedIdentifiersOn)
			: base(2)
		{
			this._initialQuotedIdentifiersOn = initialQuotedIdentifiersOn;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001DE3C File Offset: 0x0001C03C
		public void InitializeForNewInput(IList<TSqlParserToken> tokens, IList<ParseError> errors, bool phaseOne)
		{
			this._tokenSource = new TSqlWhitespaceTokenFilter(this._initialQuotedIdentifiersOn, tokens);
			this._parseErrors = errors;
			this._fragmentFactory.SetTokenStream(tokens);
			this.PhaseOne = phaseOne;
			this.setTokenBuffer(new TokenBuffer(this._tokenSource));
			this.resetState();
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001DE8C File Offset: 0x0001C08C
		static TSql80ParserBaseInternal()
		{
			HashSet<SqlDataTypeOption> hashSet = new HashSet<SqlDataTypeOption>();
			hashSet.Add(SqlDataTypeOption.Char);
			hashSet.Add(SqlDataTypeOption.VarChar);
			hashSet.Add(SqlDataTypeOption.NChar);
			hashSet.Add(SqlDataTypeOption.NVarChar);
			hashSet.Add(SqlDataTypeOption.Decimal);
			hashSet.Add(SqlDataTypeOption.Float);
			hashSet.Add(SqlDataTypeOption.Numeric);
			hashSet.Add(SqlDataTypeOption.Binary);
			hashSet.Add(SqlDataTypeOption.VarBinary);
			hashSet.Add(SqlDataTypeOption.Time);
			hashSet.Add(SqlDataTypeOption.DateTime2);
			hashSet.Add(SqlDataTypeOption.DateTimeOffset);
			TSql80ParserBaseInternal._possibleSingleParameterDataTypes = hashSet;
			Dictionary<IndexAffectingStatement, string> dictionary = new Dictionary<IndexAffectingStatement, string>();
			dictionary.Add(IndexAffectingStatement.AlterTableAddElement, "ALTER TABLE");
			dictionary.Add(IndexAffectingStatement.AlterTableRebuildAllPartitions, "ALTER TABLE REBUILD PARTITION");
			dictionary.Add(IndexAffectingStatement.AlterTableRebuildOnePartition, "ALTER TABLE REBUILD PARTITION");
			dictionary.Add(IndexAffectingStatement.AlterIndexRebuildAllPartitions, "ALTER INDEX REBUILD PARTITION");
			dictionary.Add(IndexAffectingStatement.AlterIndexRebuildOnePartition, "ALTER INDEX REBUILD PARTITION");
			dictionary.Add(IndexAffectingStatement.AlterIndexSet, "ALTER INDEX");
			dictionary.Add(IndexAffectingStatement.AlterIndexReorganize, "ALTER INDEX REORGANIZE");
			dictionary.Add(IndexAffectingStatement.CreateColumnStoreIndex, "CREATE COLUMNSTORE INDEX");
			dictionary.Add(IndexAffectingStatement.CreateIndex, "CREATE INDEX");
			dictionary.Add(IndexAffectingStatement.CreateTable, "CREATE TABLE");
			dictionary.Add(IndexAffectingStatement.CreateType, "CREATE TYPE");
			dictionary.Add(IndexAffectingStatement.CreateXmlIndex, "CREATE XML INDEX");
			dictionary.Add(IndexAffectingStatement.CreateOrAlterFunction, "CREATE/ALTER FUNCTION");
			dictionary.Add(IndexAffectingStatement.DeclareTableVariable, "DECLARE");
			dictionary.Add(IndexAffectingStatement.CreateSpatialIndex, "CREATE SPATIAL INDEX");
			TSql80ParserBaseInternal._indexOptionContainerStatementNames = dictionary;
			TSql80ParserBaseInternal._ddlStatementBeginnerTokens.add(35);
			TSql80ParserBaseInternal._ddlStatementBeginnerTokens.add(6);
			TSql80ParserBaseInternal._statementLevelRecoveryTokens.add(219);
			TSql80ParserBaseInternal._statementLevelRecoveryTokens.add(204);
			TSql80ParserBaseInternal._statementLevelRecoveryTokens.orInPlace(TSql80ParserBaseInternal._ddlStatementBeginnerTokens);
			TSql80ParserBaseInternal._phaseOneBatchLevelRecoveryTokens.add(219);
			TSql80ParserBaseInternal._phaseOneBatchLevelRecoveryTokens.orInPlace(TSql80ParserBaseInternal._ddlStatementBeginnerTokens);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001E058 File Offset: 0x0001C258
		protected void ResetQuotedIdentifiersSettingToInitial()
		{
			this._tokenSource.QuotedIdentifier = this._initialQuotedIdentifiersOn;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001E06C File Offset: 0x0001C26C
		internal static void UpdateTokenInfo(TSqlFragment fragment, IToken token)
		{
			TSqlWhitespaceTokenFilter.TSqlParserTokenProxyWithIndex tsqlParserTokenProxyWithIndex = (TSqlWhitespaceTokenFilter.TSqlParserTokenProxyWithIndex)token;
			int tokenIndex = tsqlParserTokenProxyWithIndex.TokenIndex;
			if (tokenIndex != -1)
			{
				fragment.UpdateTokenInfo(tokenIndex, tokenIndex);
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001E093 File Offset: 0x0001C293
		protected static void AddAndUpdateTokenInfo<TFragmentType>(TSqlFragment node, IList<TFragmentType> collection, TFragmentType item) where TFragmentType : TSqlFragment
		{
			collection.Add(item);
			node.UpdateTokenInfo(item);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001E0A8 File Offset: 0x0001C2A8
		protected static void AddAndUpdateTokenInfo<TFragmentType>(TSqlFragment node, IList<TFragmentType> collection, IList<TFragmentType> otherCollection) where TFragmentType : TSqlFragment
		{
			foreach (TFragmentType tfragmentType in otherCollection)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TFragmentType>(node, collection, tfragmentType);
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001E0F4 File Offset: 0x0001C2F4
		protected static string DecodeAsciiStringLiteral(string encodedValue)
		{
			int length = encodedValue.Length;
			string text = encodedValue.Substring(1, length - 2);
			if (encodedValue.get_Chars(0) == '"')
			{
				return text.Replace("\"\"", "\"");
			}
			return text.Replace("''", "'");
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001E140 File Offset: 0x0001C340
		protected static string DecodeUnicodeStringLiteral(string encodedValue)
		{
			int length = encodedValue.Length;
			return encodedValue.Substring(2, length - 3).Replace("''", "'");
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001E16D File Offset: 0x0001C36D
		protected static bool IsAsciiStringLob(string asciiValue)
		{
			return asciiValue.Length > 8000;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001E17C File Offset: 0x0001C37C
		protected static bool IsUnicodeStringLob(string unicodeValue)
		{
			return unicodeValue.Length > 8000;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001E18B File Offset: 0x0001C38B
		protected static bool IsBinaryLiteralLob(string binaryValue)
		{
			return binaryValue.Length - 2 > 16000;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0001E19C File Offset: 0x0001C39C
		public TSqlFragmentFactory FragmentFactory
		{
			get
			{
				return this._fragmentFactory;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0001E1AC File Offset: 0x0001C3AC
		public bool PhaseOne
		{
			get
			{
				return this._phaseOne;
			}
			set
			{
				this._phaseOne = value;
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001E1B5 File Offset: 0x0001C3B5
		protected void AddParseError(ParseError parseError)
		{
			this._parseErrors.Add(parseError);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001E1C4 File Offset: 0x0001C3C4
		protected void RecoverAtStatementLevel(int statementStartLine, int statementStartColumn)
		{
			this.consumeUntil(TSql80ParserBaseInternal._statementLevelRecoveryTokens);
			int line = this.LT(1).getLine();
			int column = this.LT(1).getColumn();
			if (line == statementStartLine && column == statementStartColumn)
			{
				if (this.PhaseOne && this._phaseOnePreviousStatementLevelErrorLine != line && this._phaseOnePreviousStatementLevelErrorColumn != column)
				{
					this._phaseOnePreviousStatementLevelErrorLine = line;
					this._phaseOnePreviousStatementLevelErrorColumn = column;
					throw new PhaseOneBatchException();
				}
				this.consume();
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001E232 File Offset: 0x0001C432
		protected void SkipInitialDdlTokens()
		{
			if (TSql80ParserBaseInternal._ddlStatementBeginnerTokens.member(this.LA(1)))
			{
				this.consume();
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001E250 File Offset: 0x0001C450
		protected void RecoverAtBatchLevel()
		{
			if (this.PhaseOne)
			{
				this.SkipInitialDdlTokens();
				this.consumeUntil(TSql80ParserBaseInternal._phaseOneBatchLevelRecoveryTokens);
				if (this.LA(1) != 219 && this.LA(1) != 1)
				{
					throw new PhaseOneBatchException();
				}
			}
			else
			{
				this.consumeUntil(219);
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001E29F File Offset: 0x0001C49F
		protected void ThrowPartialAstIfPhaseOne(TSqlStatement statement)
		{
			if (this.PhaseOne)
			{
				throw new PhaseOnePartialAstException(statement);
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
		protected void ThrowConstraintIfPhaseOne(ConstraintDefinition constraint)
		{
			if (this.PhaseOne)
			{
				throw new PhaseOneConstraintException(constraint);
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001E2C1 File Offset: 0x0001C4C1
		protected bool NextTokenMatches(string keyword)
		{
			return this.LA(1) != 1 && string.Equals(this.LT(1).getText(), keyword, 5);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001E2E2 File Offset: 0x0001C4E2
		protected bool NextTokenMatches(string keyword, int which)
		{
			return this.LA(which) != 1 && string.Equals(this.LT(which).getText(), keyword, 5);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001E304 File Offset: 0x0001C504
		protected bool NextTokenMatchesOneOf(params string[] keywords)
		{
			if (this.LA(1) == 1)
			{
				return false;
			}
			string text = this.LT(1).getText();
			foreach (string text2 in keywords)
			{
				if (string.Equals(text2, text, 5))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001E354 File Offset: 0x0001C554
		protected void ThrowIfEndOfFileOrBatch()
		{
			if (this.LA(1) == 1 || this.LA(1) == 219)
			{
				throw new TSqlParseErrorException(null, true);
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001E378 File Offset: 0x0001C578
		protected void AddBinaryExpression(ref ScalarExpression result, ScalarExpression expression, BinaryExpressionType type)
		{
			BinaryExpression binaryExpression = this.FragmentFactory.CreateFragment<BinaryExpression>();
			binaryExpression.FirstExpression = result;
			binaryExpression.SecondExpression = expression;
			binaryExpression.BinaryExpressionType = type;
			result = binaryExpression;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		protected void AddBinaryExpression(ref BooleanExpression result, BooleanExpression expression, BooleanBinaryExpressionType type)
		{
			BooleanBinaryExpression booleanBinaryExpression = this.FragmentFactory.CreateFragment<BooleanBinaryExpression>();
			booleanBinaryExpression.FirstExpression = result;
			booleanBinaryExpression.SecondExpression = expression;
			booleanBinaryExpression.BinaryExpressionType = type;
			result = booleanBinaryExpression;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		protected Identifier GetEmptyIdentifier(IToken token)
		{
			Identifier identifier = this.FragmentFactory.CreateFragment<Identifier>();
			TSql80ParserBaseInternal.UpdateTokenInfo(identifier, token);
			identifier.SetIdentifier(string.Empty);
			return identifier;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001E40C File Offset: 0x0001C60C
		protected static void CheckXmlForClauseOptionDuplication(XmlForClauseOptions current, XmlForClauseOptions newOption, IToken token)
		{
			if ((current & newOption) != XmlForClauseOptions.None)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
			if ((newOption & XmlForClauseOptions.ElementsAll) != XmlForClauseOptions.None && (current & XmlForClauseOptions.ElementsAll) != XmlForClauseOptions.None)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001E433 File Offset: 0x0001C633
		protected static void AddIdentifierToListWithCheck(List<Identifier> list, Identifier item, int max)
		{
			if (list.Count == max)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(item);
			}
			list.Add(item);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001E44C File Offset: 0x0001C64C
		protected static void CheckOptionDuplication(ref int encountered, int newOption, TSqlFragment vOption)
		{
			TSql80ParserBaseInternal.CheckOptionDuplication(ref encountered, newOption, TSql80ParserBaseInternal.GetFirstToken(vOption));
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001E45C File Offset: 0x0001C65C
		protected static void CheckOptionDuplication(ref int encountered, int newOption, IToken token)
		{
			int num = 1 << newOption;
			if ((encountered & num) == num)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46049", token, TSqlParserResource.SQL46049Message, new string[] { token.getText() });
			}
			encountered |= num;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001E49E File Offset: 0x0001C69E
		protected static void CheckOptionDuplication(ref ulong encountered, int newOption, TSqlFragment vOption)
		{
			TSql80ParserBaseInternal.CheckOptionDuplication(ref encountered, newOption, TSql80ParserBaseInternal.GetFirstToken(vOption));
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		protected static void CheckOptionDuplication(ref ulong encountered, int newOption, IToken token)
		{
			ulong num = 1UL << newOption;
			if ((encountered & num) == num)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46049", token, TSqlParserResource.SQL46049Message, new string[] { token.getText() });
			}
			encountered |= num;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
		protected IdentifierOrValueExpression IdentifierOrValueExpression(Identifier identifier)
		{
			IdentifierOrValueExpression identifierOrValueExpression = this.FragmentFactory.CreateFragment<IdentifierOrValueExpression>();
			identifierOrValueExpression.Identifier = identifier;
			return identifierOrValueExpression;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001E518 File Offset: 0x0001C718
		protected IdentifierOrValueExpression IdentifierOrValueExpression(ValueExpression valueExpression)
		{
			IdentifierOrValueExpression identifierOrValueExpression = this.FragmentFactory.CreateFragment<IdentifierOrValueExpression>();
			identifierOrValueExpression.ValueExpression = valueExpression;
			return identifierOrValueExpression;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001E53C File Offset: 0x0001C73C
		protected static OdbcLiteralType ParseOdbcLiteralType(IToken token)
		{
			if (TSql80ParserBaseInternal.TryMatch(token, "T"))
			{
				return OdbcLiteralType.Time;
			}
			if (TSql80ParserBaseInternal.TryMatch(token, "D"))
			{
				return OdbcLiteralType.Date;
			}
			if (TSql80ParserBaseInternal.TryMatch(token, "TS"))
			{
				return OdbcLiteralType.Timestamp;
			}
			if (TSql80ParserBaseInternal.TryMatch(token, "GUID"))
			{
				return OdbcLiteralType.Guid;
			}
			throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001E58C File Offset: 0x0001C78C
		protected static OptimizerHintKind ParseJoinOptimizerHint(IToken token)
		{
			string text;
			if ((text = token.getText().ToUpperInvariant()) != null)
			{
				if (text == "MERGE")
				{
					return OptimizerHintKind.MergeJoin;
				}
				if (text == "HASH")
				{
					return OptimizerHintKind.HashJoin;
				}
				if (text == "LOOP")
				{
					return OptimizerHintKind.LoopJoin;
				}
			}
			throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001E5E0 File Offset: 0x0001C7E0
		protected static OptimizerHintKind ParseUnionOptimizerHint(IToken token)
		{
			string text;
			if ((text = token.getText().ToUpperInvariant()) != null)
			{
				if (text == "CONCAT")
				{
					return OptimizerHintKind.ConcatUnion;
				}
				if (text == "HASH")
				{
					return OptimizerHintKind.HashUnion;
				}
				if (text == "MERGE")
				{
					return OptimizerHintKind.MergeUnion;
				}
				if (text == "KEEP")
				{
					return OptimizerHintKind.KeepUnion;
				}
			}
			throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001E644 File Offset: 0x0001C844
		protected bool IsNextRuleSelectParenthesis()
		{
			bool flag = false;
			if (this.LA(1) == 191 && this.LA(2) == 140)
			{
				return true;
			}
			int num = this.mark();
			this.consume();
			int num2 = 1;
			bool flag2 = true;
			while (flag2)
			{
				int num3 = this.LA(1);
				if (num3 <= 72)
				{
					if (num3 <= 36)
					{
						if (num3 != 1)
						{
							if (num3 == 36)
							{
								goto IL_00C5;
							}
						}
						else
						{
							flag2 = false;
						}
					}
					else if (num3 == 59 || num3 == 72)
					{
						goto IL_00C5;
					}
				}
				else if (num3 <= 90)
				{
					switch (num3)
					{
					case 85:
					case 87:
						goto IL_00C5;
					case 86:
						break;
					default:
						if (num3 == 90)
						{
							goto IL_00C5;
						}
						break;
					}
				}
				else
				{
					if (num3 == 114 || num3 == 158)
					{
						goto IL_00C5;
					}
					switch (num3)
					{
					case 191:
						num2++;
						break;
					case 192:
						num2--;
						if (num2 == 0)
						{
							flag2 = false;
						}
						break;
					}
				}
				IL_00CD:
				this.consume();
				continue;
				IL_00C5:
				if (num2 == 1)
				{
					flag = true;
					flag2 = false;
					goto IL_00CD;
				}
				goto IL_00CD;
			}
			this.rewind(num);
			return flag;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001E734 File Offset: 0x0001C934
		protected bool IsNextRuleBooleanParenthesis()
		{
			if (this.LA(1) != 191)
			{
				return false;
			}
			bool flag = false;
			int num = this.mark();
			this.consume();
			int num2 = 1;
			int num3 = 0;
			int num4 = 0;
			bool flag2 = true;
			while (flag2)
			{
				int num5 = this.LA(1);
				if (num5 <= 83)
				{
					if (num5 <= 20)
					{
						if (num5 <= 7)
						{
							if (num5 != 1)
							{
								if (num5 == 7)
								{
									goto IL_0182;
								}
							}
							else
							{
								flag2 = false;
							}
						}
						else
						{
							if (num5 == 14)
							{
								goto IL_0182;
							}
							if (num5 == 20)
							{
								num3++;
							}
						}
					}
					else if (num5 <= 56)
					{
						if (num5 == 31)
						{
							goto IL_0182;
						}
						if (num5 == 56)
						{
							num3--;
						}
					}
					else if (num5 == 62 || num5 == 69 || num5 == 83)
					{
						goto IL_0182;
					}
				}
				else if (num5 <= 140)
				{
					if (num5 <= 94)
					{
						if (num5 == 89 || num5 == 94)
						{
							goto IL_0182;
						}
					}
					else
					{
						if (num5 == 99 || num5 == 112)
						{
							goto IL_0182;
						}
						if (num5 == 140)
						{
							if (num4 == 0)
							{
								num4 = num2;
							}
						}
					}
				}
				else if (num5 <= 160)
				{
					if (num5 == 157 || num5 == 160)
					{
						goto IL_0182;
					}
				}
				else
				{
					switch (num5)
					{
					case 188:
						goto IL_0182;
					case 189:
					case 190:
						break;
					case 191:
						num2++;
						break;
					case 192:
						if (num2 == num4)
						{
							num4 = 0;
						}
						num2--;
						if (num2 == 0)
						{
							flag2 = false;
						}
						break;
					default:
						if (num5 == 196)
						{
							goto IL_0182;
						}
						switch (num5)
						{
						case 205:
						case 206:
						case 207:
						case 208:
							goto IL_0182;
						}
						break;
					}
				}
				IL_01A3:
				this.consume();
				continue;
				IL_0182:
				if (num3 == 0 && num4 == 0)
				{
					flag = true;
					flag2 = false;
					goto IL_01A3;
				}
				goto IL_01A3;
			}
			this.rewind(num);
			return flag;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001E8FC File Offset: 0x0001CAFC
		protected static void Match(IToken token, string keyword)
		{
			if (!string.Equals(token.getText(), keyword, 5))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46005", token, TSqlParserResource.SQL46005Message, new string[]
				{
					keyword,
					token.getText()
				});
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0001E93D File Offset: 0x0001CB3D
		protected static void Match(Identifier id, string constant)
		{
			if (!string.Equals(id.Value, constant, 5))
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(id);
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001E955 File Offset: 0x0001CB55
		protected static void Match(Identifier id, string constant, IToken tokenForError)
		{
			if (!string.Equals(id.Value, constant, 5))
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(tokenForError);
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001E96D File Offset: 0x0001CB6D
		protected static void Match(IToken token, string keyword, string alternate)
		{
			if (!string.Equals(token.getText(), keyword, 5) && !string.Equals(token.getText(), alternate, 5))
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001E994 File Offset: 0x0001CB94
		protected static bool TryMatch(IToken token, string keyword)
		{
			return string.Equals(token.getText(), keyword, 5);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001E9A3 File Offset: 0x0001CBA3
		protected static bool TryMatch(Identifier identifier, string keyword)
		{
			return string.Equals(identifier.Value, keyword, 5);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		protected static void MatchString(Literal literal, params string[] keywords)
		{
			string value = literal.Value;
			foreach (string text in keywords)
			{
				if (string.Equals(value, text, 5))
				{
					return;
				}
			}
			TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(TSql80ParserBaseInternal.GetFirstToken(literal));
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		protected static SqlDataTypeOption ParseDataType(string token)
		{
			string text;
			if ((text = token.ToUpperInvariant()) != null)
			{
				if (<PrivateImplementationDetails>{BC0BDBD1-A382-4890-888D-B78DC4BCB031}.$$method0x600099f-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(32);
					dictionary.Add("BIGINT", 0);
					dictionary.Add("INTEGER", 1);
					dictionary.Add("INT", 2);
					dictionary.Add("SMALLINT", 3);
					dictionary.Add("TINYINT", 4);
					dictionary.Add("BIT", 5);
					dictionary.Add("DEC", 6);
					dictionary.Add("DECIMAL", 7);
					dictionary.Add("NUMERIC", 8);
					dictionary.Add("MONEY", 9);
					dictionary.Add("SMALLMONEY", 10);
					dictionary.Add("FLOAT", 11);
					dictionary.Add("REAL", 12);
					dictionary.Add("DATETIME", 13);
					dictionary.Add("SMALLDATETIME", 14);
					dictionary.Add("CHARACTER", 15);
					dictionary.Add("CHAR", 16);
					dictionary.Add("VARCHAR", 17);
					dictionary.Add("TEXT", 18);
					dictionary.Add("NCHAR", 19);
					dictionary.Add("NCHARACTER", 20);
					dictionary.Add("NVARCHAR", 21);
					dictionary.Add("NTEXT", 22);
					dictionary.Add("BINARY", 23);
					dictionary.Add("VARBINARY", 24);
					dictionary.Add("IMAGE", 25);
					dictionary.Add("CURSOR", 26);
					dictionary.Add("SQL_VARIANT", 27);
					dictionary.Add("TABLE", 28);
					dictionary.Add("ROWVERSION", 29);
					dictionary.Add("TIMESTAMP", 30);
					dictionary.Add("UNIQUEIDENTIFIER", 31);
					<PrivateImplementationDetails>{BC0BDBD1-A382-4890-888D-B78DC4BCB031}.$$method0x600099f-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{BC0BDBD1-A382-4890-888D-B78DC4BCB031}.$$method0x600099f-1.TryGetValue(text, ref num))
				{
					switch (num)
					{
					case 0:
						return SqlDataTypeOption.BigInt;
					case 1:
					case 2:
						return SqlDataTypeOption.Int;
					case 3:
						return SqlDataTypeOption.SmallInt;
					case 4:
						return SqlDataTypeOption.TinyInt;
					case 5:
						return SqlDataTypeOption.Bit;
					case 6:
					case 7:
						return SqlDataTypeOption.Decimal;
					case 8:
						return SqlDataTypeOption.Numeric;
					case 9:
						return SqlDataTypeOption.Money;
					case 10:
						return SqlDataTypeOption.SmallMoney;
					case 11:
						return SqlDataTypeOption.Float;
					case 12:
						return SqlDataTypeOption.Real;
					case 13:
						return SqlDataTypeOption.DateTime;
					case 14:
						return SqlDataTypeOption.SmallDateTime;
					case 15:
					case 16:
						return SqlDataTypeOption.Char;
					case 17:
						return SqlDataTypeOption.VarChar;
					case 18:
						return SqlDataTypeOption.Text;
					case 19:
					case 20:
						return SqlDataTypeOption.NChar;
					case 21:
						return SqlDataTypeOption.NVarChar;
					case 22:
						return SqlDataTypeOption.NText;
					case 23:
						return SqlDataTypeOption.Binary;
					case 24:
						return SqlDataTypeOption.VarBinary;
					case 25:
						return SqlDataTypeOption.Image;
					case 26:
						return SqlDataTypeOption.Cursor;
					case 27:
						return SqlDataTypeOption.Sql_Variant;
					case 28:
						return SqlDataTypeOption.Table;
					case 29:
						return SqlDataTypeOption.Rowversion;
					case 30:
						return SqlDataTypeOption.Timestamp;
					case 31:
						return SqlDataTypeOption.UniqueIdentifier;
					}
				}
			}
			return SqlDataTypeOption.None;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		protected static IndexOptionKind ParseIndexLegacyWithOption(IToken token)
		{
			IndexOptionKind indexOptionKind;
			if (!IndexOptionHelper.Instance.TryParseOption(token, SqlVersionFlags.TSql80, out indexOptionKind))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46015", token, TSqlParserResource.SQL46015Message, new string[] { token.getText() });
			}
			return indexOptionKind;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001ECE8 File Offset: 0x0001CEE8
		protected static void ThrowWrongIndexOptionError(IndexAffectingStatement statement, IndexOption option)
		{
			string text = string.Empty;
			if (option.FirstTokenIndex >= 0 && option.ScriptTokenStream != null && option.FirstTokenIndex < option.ScriptTokenStream.Count)
			{
				TSqlParserToken tsqlParserToken = option.ScriptTokenStream[option.FirstTokenIndex];
				text = tsqlParserToken.Text;
			}
			string empty;
			if (!TSql80ParserBaseInternal._indexOptionContainerStatementNames.TryGetValue(statement, ref empty))
			{
				empty = string.Empty;
			}
			TSql80ParserBaseInternal.ThrowParseErrorException("SQL46057", option, TSqlParserResource.SQL46057Message, new string[] { text, empty });
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001ED70 File Offset: 0x0001CF70
		protected static void CheckFillFactorRange(Literal value)
		{
			int num;
			if (!int.TryParse(value.Value, 7, CultureInfo.InvariantCulture, ref num) || num < 1 || num > 100)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46060", value, TSqlParserResource.SQL46060Message, new string[] { value.Value });
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		protected static void CheckIdentifierLength(Identifier value)
		{
			if (value.Value.Length > 128)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46095", value, TSqlParserResource.SQL46095Message, new string[] { value.Value.Substring(0, 128) });
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001EE08 File Offset: 0x0001D008
		protected static void CheckIdentifierLiteralLength(IdentifierLiteral value)
		{
			if (value.Value.Length > 128)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46095", value, TSqlParserResource.SQL46095Message, new string[] { value.Value.Substring(0, 128) });
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001EE54 File Offset: 0x0001D054
		protected static void ThrowIfPercentValueOutOfRange(ScalarExpression expr)
		{
			if (expr is ParenthesisExpression)
			{
				ParenthesisExpression parenthesisExpression = expr as ParenthesisExpression;
				if (parenthesisExpression != null)
				{
					TSql80ParserBaseInternal.ThrowIfPercentValueOutOfRange(parenthesisExpression.Expression);
					return;
				}
			}
			else if (expr is UnaryExpression)
			{
				UnaryExpression unaryExpression = expr as UnaryExpression;
				if (unaryExpression != null)
				{
					if (unaryExpression.UnaryExpressionType == UnaryExpressionType.Negative)
					{
						TSql80ParserBaseInternal.ThrowParseErrorException("SQL46094", expr, TSqlParserResource.SQL46094Message, new string[0]);
						return;
					}
					TSql80ParserBaseInternal.ThrowIfPercentValueOutOfRange(unaryExpression.Expression);
					return;
				}
			}
			else
			{
				Literal literal = expr as Literal;
				double num;
				if (literal != null && (literal.LiteralType == LiteralType.Real || literal.LiteralType == LiteralType.Numeric || literal.LiteralType == LiteralType.Integer) && (!double.TryParse(literal.Value, 167, CultureInfo.InvariantCulture, ref num) || num < 0.0 || num > 100.0))
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46094", expr, TSqlParserResource.SQL46094Message, new string[0]);
				}
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001EF31 File Offset: 0x0001D131
		protected static void VerifyAllowedIndexOption(IndexAffectingStatement statement, IndexOption option)
		{
			TSql80ParserBaseInternal.VerifyAllowedIndexOption(statement, option, SqlVersionFlags.None);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001EF3C File Offset: 0x0001D13C
		protected static void VerifyAllowedIndexOption(IndexAffectingStatement statement, IndexOption option, SqlVersionFlags versionFlags)
		{
			bool flag = false;
			if (option.OptionKind == IndexOptionKind.FileStreamOn && statement != IndexAffectingStatement.AlterTableAddElement)
			{
				flag = true;
			}
			switch (statement)
			{
			case IndexAffectingStatement.AlterTableAddElement:
				if (option.OptionKind == IndexOptionKind.DropExisting || option.OptionKind == IndexOptionKind.LobCompaction)
				{
					flag = true;
				}
				break;
			case IndexAffectingStatement.AlterTableRebuildOnePartition:
			case IndexAffectingStatement.AlterIndexRebuildOnePartition:
				if (option.OptionKind != IndexOptionKind.SortInTempDB && option.OptionKind != IndexOptionKind.MaxDop && option.OptionKind != IndexOptionKind.DataCompression)
				{
					flag = true;
				}
				break;
			case IndexAffectingStatement.AlterTableRebuildAllPartitions:
			case IndexAffectingStatement.AlterIndexRebuildAllPartitions:
				if (option.OptionKind == IndexOptionKind.DropExisting || option.OptionKind == IndexOptionKind.LobCompaction)
				{
					flag = true;
				}
				break;
			case IndexAffectingStatement.AlterIndexSet:
				if (option.OptionKind != IndexOptionKind.AllowRowLocks && option.OptionKind != IndexOptionKind.AllowPageLocks && option.OptionKind != IndexOptionKind.IgnoreDupKey && option.OptionKind != IndexOptionKind.StatisticsNoRecompute)
				{
					flag = true;
				}
				break;
			case IndexAffectingStatement.AlterIndexReorganize:
				if (option.OptionKind != IndexOptionKind.LobCompaction)
				{
					flag = true;
				}
				break;
			case IndexAffectingStatement.CreateColumnStoreIndex:
				if (option.OptionKind != IndexOptionKind.DropExisting && option.OptionKind != IndexOptionKind.MaxDop)
				{
					flag = true;
				}
				break;
			case IndexAffectingStatement.CreateIndex:
				if (option.OptionKind == IndexOptionKind.LobCompaction)
				{
					flag = true;
				}
				break;
			case IndexAffectingStatement.CreateTable:
			case IndexAffectingStatement.CreateOrAlterFunction:
			case IndexAffectingStatement.DeclareTableVariable:
				if (option.OptionKind == IndexOptionKind.SortInTempDB || option.OptionKind == IndexOptionKind.Online || option.OptionKind == IndexOptionKind.MaxDop || option.OptionKind == IndexOptionKind.LobCompaction || option.OptionKind == IndexOptionKind.DropExisting)
				{
					flag = true;
				}
				break;
			case IndexAffectingStatement.CreateType:
				if (option.OptionKind != IndexOptionKind.IgnoreDupKey)
				{
					flag = true;
				}
				break;
			case IndexAffectingStatement.CreateXmlIndex:
				if (option.OptionKind == IndexOptionKind.DataCompression || option.OptionKind == IndexOptionKind.LobCompaction)
				{
					flag = true;
				}
				else if (option.OptionKind == IndexOptionKind.IgnoreDupKey)
				{
					IndexStateOption indexStateOption = option as IndexStateOption;
					if (indexStateOption != null)
					{
						flag = indexStateOption.OptionState == OptionState.On;
					}
				}
				break;
			case IndexAffectingStatement.CreateSpatialIndex:
				if ((option.OptionKind == IndexOptionKind.DataCompression && (versionFlags & SqlVersionFlags.TSql110) == SqlVersionFlags.None) || option.OptionKind == IndexOptionKind.LobCompaction || option.OptionKind == IndexOptionKind.FileStreamOn)
				{
					flag = true;
				}
				break;
			}
			if (flag)
			{
				TSql80ParserBaseInternal.ThrowWrongIndexOptionError(statement, option);
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001F129 File Offset: 0x0001D329
		protected static void ThrowSyntaxErrorIfNotCreateAlterTable(IndexAffectingStatement statement, IToken atToken)
		{
			if (statement != IndexAffectingStatement.CreateTable && statement != IndexAffectingStatement.AlterTableAddElement)
			{
				TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(atToken);
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001F13C File Offset: 0x0001D33C
		protected static FunctionOptionKind ParseAlterCreateFunctionWithOption(IToken token)
		{
			string text;
			if ((text = token.getText().ToUpperInvariant()) != null)
			{
				if (text == "ENCRYPTION")
				{
					return FunctionOptionKind.Encryption;
				}
				if (text == "SCHEMABINDING")
				{
					return FunctionOptionKind.SchemaBinding;
				}
			}
			throw new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46026", token, TSqlParserResource.SQL46026Message, new string[] { token.getText() }));
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001F1A0 File Offset: 0x0001D3A0
		protected static StatisticsOptionKind ParseCreateStatisticsWithOption(IToken token)
		{
			string text;
			if ((text = token.getText().ToUpperInvariant()) != null)
			{
				if (text == "FULLSCAN")
				{
					return StatisticsOptionKind.FullScan;
				}
				if (text == "NORECOMPUTE")
				{
					return StatisticsOptionKind.NoRecompute;
				}
			}
			throw new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46018", token, TSqlParserResource.SQL46018Message, new string[] { token.getText() }));
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001F204 File Offset: 0x0001D404
		protected static StatisticsOptionKind ParseSampleOptionsWithOption(IToken token)
		{
			if (string.Compare("ROWS", token.getText(), 5) == 0)
			{
				return StatisticsOptionKind.SampleRows;
			}
			throw new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46019", token, TSqlParserResource.SQL46019Message, new string[] { token.getText() }));
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001F24C File Offset: 0x0001D44C
		protected static TriggerEnforcement ParseTriggerEnforcement(IToken token)
		{
			string text;
			if ((text = token.getText().ToUpperInvariant()) != null)
			{
				if (text == "ENABLE")
				{
					return TriggerEnforcement.Enable;
				}
				if (text == "DISABLE")
				{
					return TriggerEnforcement.Disable;
				}
			}
			throw new NoViableAltException(token, token.getFilename());
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001F294 File Offset: 0x0001D494
		protected static void CheckSpecialColumn(ColumnReferenceExpression column)
		{
			if (column.ColumnType != ColumnType.Regular && column.MultiPartIdentifier != null && column.MultiPartIdentifier.Count >= 4)
			{
				throw new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46028", TSql80ParserBaseInternal.GetFirstToken(column), TSqlParserResource.SQL46028Message, new string[0]));
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001F2E0 File Offset: 0x0001D4E0
		protected static void CheckStarQualifier(SelectStarExpression column)
		{
			if (column.Qualifier != null)
			{
				int count = column.Qualifier.Count;
				if (count >= 4)
				{
					throw new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46028", TSql80ParserBaseInternal.GetFirstToken(column), TSqlParserResource.SQL46028Message, new string[0]));
				}
				if (count == 0 || (count >= 1 && !string.IsNullOrEmpty(column.Qualifier[count - 1].Value)))
				{
					return;
				}
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46016", column, TSqlParserResource.SQL46016Message, new string[0]);
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001F360 File Offset: 0x0001D560
		protected static void CheckTableNameExistsForColumn(ColumnReferenceExpression column, bool multiPartRequisite)
		{
			int num = ((column.MultiPartIdentifier == null) ? 0 : column.MultiPartIdentifier.Count);
			if (!multiPartRequisite && ((column.ColumnType == ColumnType.Regular && num == 1) || (column.ColumnType != ColumnType.Regular && num == 0)))
			{
				return;
			}
			bool flag = false;
			if (column.ColumnType == ColumnType.Regular)
			{
				if (num >= 2 && !string.IsNullOrEmpty(column.MultiPartIdentifier[num - 2].Value))
				{
					flag = true;
				}
			}
			else if (num >= 1 && !string.IsNullOrEmpty(column.MultiPartIdentifier[num - 1].Value))
			{
				flag = true;
			}
			if (!flag)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46016", column, TSqlParserResource.SQL46016Message, new string[0]);
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001F404 File Offset: 0x0001D604
		protected static void CheckTwoPartNameForSchemaObjectName(SchemaObjectName name, string statementType)
		{
			if (name.DatabaseIdentifier != null && !string.IsNullOrEmpty(name.DatabaseIdentifier.Value))
			{
				throw new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46021", TSql80ParserBaseInternal.GetFirstToken(name), TSqlParserResource.SQL46021Message, new string[] { statementType }));
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0001F452 File Offset: 0x0001D652
		protected static void CheckIfValidLanguageString(Literal inputString)
		{
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001F454 File Offset: 0x0001D654
		protected static void CheckIfValidLanguageIdentifier(Identifier inputString)
		{
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001F456 File Offset: 0x0001D656
		protected static void CheckIfValidLanguageInteger(Literal inputValue)
		{
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001F458 File Offset: 0x0001D658
		protected static void CheckIfValidLanguageHex(Literal inputValue)
		{
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001F45A File Offset: 0x0001D65A
		protected static bool IsStopAtBeforeMarkRestoreOption(IToken token)
		{
			return TSql80ParserBaseInternal.TryMatch(token, "STOPATMARK") || TSql80ParserBaseInternal.TryMatch(token, "STOPBEFOREMARK");
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001F478 File Offset: 0x0001D678
		protected StopRestoreOption CreateStopRestoreOption(IToken optionBeginning, ValueExpression mark, ValueExpression afterClause)
		{
			StopRestoreOption stopRestoreOption = this.FragmentFactory.CreateFragment<StopRestoreOption>();
			if (TSql80ParserBaseInternal.TryMatch(optionBeginning, "STOPATMARK"))
			{
				stopRestoreOption.IsStopAt = true;
				stopRestoreOption.OptionKind = RestoreOptionKind.StopAt;
			}
			else
			{
				stopRestoreOption.OptionKind = RestoreOptionKind.Stop;
			}
			stopRestoreOption.Mark = mark;
			if (afterClause != null)
			{
				stopRestoreOption.After = afterClause;
			}
			return stopRestoreOption;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001F4CC File Offset: 0x0001D6CC
		protected ScalarExpressionRestoreOption CreateSimpleRestoreOptionWithValue(IToken optionBeginning, ScalarExpression optionValue)
		{
			ScalarExpressionRestoreOption scalarExpressionRestoreOption = this.FragmentFactory.CreateFragment<ScalarExpressionRestoreOption>();
			scalarExpressionRestoreOption.OptionKind = RestoreOptionWithValueHelper.Instance.ParseOption(optionBeginning);
			scalarExpressionRestoreOption.Value = optionValue;
			return scalarExpressionRestoreOption;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001F500 File Offset: 0x0001D700
		protected void CreateInternalError(string entryPoint, Exception exception)
		{
			string sql46001Message = TSqlParserResource.SQL46001Message;
			ParseError parseError = new ParseError(46001, this._tokenSource.LastToken.Offset, this._tokenSource.LastToken.Line, this._tokenSource.LastToken.Column, sql46001Message);
			this.AddParseError(parseError);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0001F558 File Offset: 0x0001D758
		protected void SetFunctionBodyStatement(FunctionStatementBody parent, BeginEndBlockStatement compoundStatement)
		{
			if (compoundStatement != null)
			{
				StatementList statementList = this.FragmentFactory.CreateFragment<StatementList>();
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TSqlStatement>(statementList, statementList.Statements, compoundStatement);
				parent.StatementList = statementList;
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0001F588 File Offset: 0x0001D788
		protected static void AddConstraintToColumn(ConstraintDefinition constraint, ColumnDefinition column)
		{
			DefaultConstraintDefinition defaultConstraintDefinition = constraint as DefaultConstraintDefinition;
			if (defaultConstraintDefinition != null)
			{
				if (column.DefaultConstraint != null)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46012", constraint, TSqlParserResource.SQL46012Message, new string[0]);
				}
				column.DefaultConstraint = defaultConstraintDefinition;
				return;
			}
			TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ConstraintDefinition>(column, column.Constraints, constraint);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
		protected void PutIdentifiersIntoFunctionCall(FunctionCall functionCall, MultiPartIdentifier identifiers)
		{
			int count = identifiers.Count;
			functionCall.FunctionName = identifiers[count - 1];
			if (count > 1)
			{
				MultiPartIdentifierCallTarget multiPartIdentifierCallTarget = this.FragmentFactory.CreateFragment<MultiPartIdentifierCallTarget>();
				MultiPartIdentifier multiPartIdentifier = this.FragmentFactory.CreateFragment<MultiPartIdentifier>();
				for (int i = 0; i < count - 1; i++)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(multiPartIdentifier, multiPartIdentifier.Identifiers, identifiers[i]);
				}
				multiPartIdentifierCallTarget.MultiPartIdentifier = multiPartIdentifier;
				functionCall.CallTarget = multiPartIdentifierCallTarget;
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001F642 File Offset: 0x0001D842
		protected void VerifyColumnDataType(ColumnDefinition column)
		{
			if (column.DataType == null && !string.Equals(column.ColumnIdentifier.Value, "TIMESTAMP", 5))
			{
				throw this.GetUnexpectedTokenErrorException();
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001F66C File Offset: 0x0001D86C
		protected void CreateSetClauseColumn(AssignmentSetClause setClause, MultiPartIdentifier multiPartIdentifier)
		{
			ColumnReferenceExpression columnReferenceExpression = this.FragmentFactory.CreateFragment<ColumnReferenceExpression>();
			columnReferenceExpression.ColumnType = ColumnType.Regular;
			columnReferenceExpression.MultiPartIdentifier = multiPartIdentifier;
			setClause.Column = columnReferenceExpression;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001F69C File Offset: 0x0001D89C
		protected static void ProcessNationalAndVarying(SqlDataTypeReference type, IToken nationalToken, bool isVarying)
		{
			if (nationalToken != null && isVarying)
			{
				if (type.SqlDataTypeOption == SqlDataTypeOption.Char)
				{
					type.SqlDataTypeOption = SqlDataTypeOption.NVarChar;
					return;
				}
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46002", nationalToken, TSqlParserResource.SQL46002Message, new string[] { TSql80ParserBaseInternal.GetSqlDataTypeName(type.SqlDataTypeOption) });
				return;
			}
			else
			{
				if (nationalToken != null)
				{
					switch (type.SqlDataTypeOption)
					{
					case SqlDataTypeOption.Char:
						type.SqlDataTypeOption = SqlDataTypeOption.NChar;
						return;
					case SqlDataTypeOption.Text:
						type.SqlDataTypeOption = SqlDataTypeOption.NText;
						return;
					}
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46003", nationalToken, TSqlParserResource.SQL46003Message, new string[] { TSql80ParserBaseInternal.GetSqlDataTypeName(type.SqlDataTypeOption) });
					return;
				}
				if (isVarying)
				{
					SqlDataTypeOption sqlDataTypeOption = type.SqlDataTypeOption;
					if (sqlDataTypeOption == SqlDataTypeOption.Char)
					{
						type.SqlDataTypeOption = SqlDataTypeOption.VarChar;
						return;
					}
					if (sqlDataTypeOption == SqlDataTypeOption.NChar)
					{
						type.SqlDataTypeOption = SqlDataTypeOption.NVarChar;
						return;
					}
					if (sqlDataTypeOption == SqlDataTypeOption.Binary)
					{
						type.SqlDataTypeOption = SqlDataTypeOption.VarBinary;
						return;
					}
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46004", type, TSqlParserResource.SQL46004Message, new string[] { TSql80ParserBaseInternal.GetSqlDataTypeName(type.SqlDataTypeOption) });
				}
				return;
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001F7A0 File Offset: 0x0001D9A0
		protected static string GetSqlDataTypeName(SqlDataTypeOption type)
		{
			if (type == SqlDataTypeOption.None)
			{
				return TSqlParserResource.UserDefined;
			}
			return type.ToString();
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		protected static void CheckSqlDataTypeParameters(SqlDataTypeReference dataType)
		{
			switch (dataType.Parameters.Count)
			{
			case 0:
				break;
			case 1:
				if (!TSql80ParserBaseInternal._possibleSingleParameterDataTypes.Contains(dataType.SqlDataTypeOption))
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46008", dataType, TSqlParserResource.SQL46008Message, new string[] { dataType.SqlDataTypeOption.ToString() });
				}
				if (dataType.Parameters[0].LiteralType == LiteralType.Max && (dataType.SqlDataTypeOption == SqlDataTypeOption.Char || dataType.SqlDataTypeOption == SqlDataTypeOption.NChar || dataType.SqlDataTypeOption == SqlDataTypeOption.Binary))
				{
					TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(TSql80ParserBaseInternal.GetFirstToken(dataType.Parameters[0]));
					return;
				}
				break;
			case 2:
				if (dataType.SqlDataTypeOption != SqlDataTypeOption.Decimal && dataType.SqlDataTypeOption != SqlDataTypeOption.Numeric)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46009", dataType, TSqlParserResource.SQL46009Message, new string[] { dataType.SqlDataTypeOption.ToString() });
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001F8A8 File Offset: 0x0001DAA8
		protected bool IsTableReference(bool allowMultipleTableHints)
		{
			if (this.LA(1) != 191)
			{
				return true;
			}
			int num = this.mark();
			try
			{
				this.consume();
				if ((this.LA(1) == 232 || this.LA(1) == 78) && (this.LA(2) == 192 || allowMultipleTableHints))
				{
					QuoteType quoteType;
					string text = Identifier.DecodeIdentifier(this.LT(1).getText(), out quoteType);
					TableHintKind tableHintKind;
					if (TableHintOptionsHelper.Instance.TryParseOption(text, SqlVersionFlags.TSql80, out tableHintKind))
					{
						return true;
					}
				}
			}
			finally
			{
				this.rewind(num);
			}
			return false;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001F944 File Offset: 0x0001DB44
		internal T ParseRuleWithStandardExceptionHandling<T>(TSql80ParserBaseInternal.ParserEntryPoint<T> entryPoint, string entryPointName) where T : TSqlFragment
		{
			T t = default(T);
			try
			{
				t = entryPoint();
			}
			catch (TSqlParseErrorException ex)
			{
				if (!ex.DoNotLog)
				{
					this.AddParseError(ex.ParseError);
				}
			}
			catch (NoViableAltException ex2)
			{
				ParseError faultTolerantUnexpectedTokenError = TSql80ParserBaseInternal.GetFaultTolerantUnexpectedTokenError(ex2.token, ex2, this._tokenSource.LastToken.Offset);
				this.AddParseError(faultTolerantUnexpectedTokenError);
			}
			catch (MismatchedTokenException ex3)
			{
				ParseError faultTolerantUnexpectedTokenError2 = TSql80ParserBaseInternal.GetFaultTolerantUnexpectedTokenError(ex3.token, ex3, this._tokenSource.LastToken.Offset);
				this.AddParseError(faultTolerantUnexpectedTokenError2);
			}
			catch (RecognitionException)
			{
				ParseError unexpectedTokenError = TSql80ParserBaseInternal.GetUnexpectedTokenError(this.LT(1));
				this.AddParseError(unexpectedTokenError);
			}
			catch (TokenStreamRecognitionException ex4)
			{
				ParseError parseError = TSql80ParserBaseInternal.ProcessTokenStreamRecognitionException(ex4, this._tokenSource.LastToken.Offset);
				this.AddParseError(parseError);
			}
			catch (ANTLRException ex5)
			{
				this.CreateInternalError(entryPointName, ex5);
			}
			catch (StackOverflowException ex6)
			{
				this.CreateInternalError(entryPointName, ex6);
			}
			catch (NullReferenceException ex7)
			{
				this.CreateInternalError(entryPointName, ex7);
			}
			catch (ArgumentException ex8)
			{
				this.CreateInternalError(entryPointName, ex8);
			}
			catch (IndexOutOfRangeException ex9)
			{
				this.CreateInternalError(entryPointName, ex9);
			}
			return t;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
		protected void SetNameForDoublePrecisionType(DataTypeReference dataType, IToken doubleToken, IToken precisionToken)
		{
			Identifier identifier = this.FragmentFactory.CreateFragment<Identifier>();
			identifier.Value = "FLOAT";
			TSql80ParserBaseInternal.UpdateTokenInfo(identifier, doubleToken);
			TSql80ParserBaseInternal.UpdateTokenInfo(identifier, precisionToken);
			dataType.Name = this.FragmentFactory.CreateFragment<SchemaObjectName>();
			TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(dataType.Name, dataType.Name.Identifiers, identifier);
			TSql80ParserBaseInternal.UpdateTokenInfo(dataType, doubleToken);
			TSql80ParserBaseInternal.UpdateTokenInfo(dataType, precisionToken);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001FB3C File Offset: 0x0001DD3C
		protected static void CheckForTemporaryFunction(SchemaObjectName name)
		{
			if (name.BaseIdentifier != null && name.BaseIdentifier.Value != null && name.BaseIdentifier.Value.StartsWith("#", 4))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46093", name, TSqlParserResource.SQL46093Message, new string[] { name.BaseIdentifier.Value });
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		protected static void CheckForTemporaryView(SchemaObjectName name)
		{
			if (name.BaseIdentifier != null && name.BaseIdentifier.Value != null && name.BaseIdentifier.Value.StartsWith("#", 4))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46092", name, TSqlParserResource.SQL46092Message, new string[] { name.BaseIdentifier.Value });
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001FBFC File Offset: 0x0001DDFC
		protected static IToken GetFirstToken(TSqlFragment fragment)
		{
			if (fragment.ScriptTokenStream != null && fragment.FirstTokenIndex != -1)
			{
				return fragment.ScriptTokenStream[fragment.FirstTokenIndex];
			}
			return null;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0001FC22 File Offset: 0x0001DE22
		public static void ThrowParseErrorException(string identifier, TSqlFragment fragment, string messageTemplate, params string[] args)
		{
			TSql80ParserBaseInternal.ThrowParseErrorException(identifier, TSql80ParserBaseInternal.GetFirstToken(fragment), messageTemplate, args);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001FC34 File Offset: 0x0001DE34
		public static void ThrowParseErrorException(string identifier, IToken token, string messageTemplate, params string[] args)
		{
			ParseError parseError = TSql80ParserBaseInternal.CreateParseError(identifier, token, messageTemplate, args);
			throw new TSqlParseErrorException(parseError);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001FC54 File Offset: 0x0001DE54
		public static ParseError CreateParseError(string identifier, IToken token, string messageTemplate, params string[] args)
		{
			TSqlWhitespaceTokenFilter.TSqlParserTokenProxyWithIndex tsqlParserTokenProxyWithIndex = token as TSqlWhitespaceTokenFilter.TSqlParserTokenProxyWithIndex;
			int num;
			int num2;
			int num3;
			if (tsqlParserTokenProxyWithIndex != null)
			{
				num = tsqlParserTokenProxyWithIndex.Token.Line;
				num2 = tsqlParserTokenProxyWithIndex.Token.Column;
				num3 = tsqlParserTokenProxyWithIndex.Token.Offset;
			}
			else
			{
				TSqlParserToken tsqlParserToken = token as TSqlParserToken;
				if (tsqlParserToken != null)
				{
					num = tsqlParserToken.Line;
					num2 = tsqlParserToken.Column;
					num3 = tsqlParserToken.Offset;
				}
				else
				{
					num = 1;
					num2 = 1;
					num3 = 0;
				}
			}
			return TSql80ParserBaseInternal.CreateParseError(identifier, num3, num, num2, messageTemplate, args);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001FCC8 File Offset: 0x0001DEC8
		public static ParseError CreateParseError(string identifier, int offset, int line, int column, string messageTemplate, params string[] args)
		{
			return new ParseError(int.Parse(identifier.Substring(3), CultureInfo.InvariantCulture), offset, line, column, string.Format(CultureInfo.CurrentCulture, messageTemplate, args));
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001FCF4 File Offset: 0x0001DEF4
		internal static ParseError ProcessTokenStreamRecognitionException(TokenStreamRecognitionException exception, int lastOffset)
		{
			NoViableAltException ex = exception.recog as NoViableAltException;
			if (ex != null)
			{
				return TSql80ParserBaseInternal.GetFaultTolerantUnexpectedTokenError(ex.token, ex, lastOffset);
			}
			MismatchedTokenException ex2 = exception.recog as MismatchedTokenException;
			if (ex2 != null)
			{
				return TSql80ParserBaseInternal.GetFaultTolerantUnexpectedTokenError(ex2.token, ex2, lastOffset);
			}
			NoViableAltForCharException ex3 = exception.recog as NoViableAltForCharException;
			if (ex3 != null)
			{
				return TSql80ParserBaseInternal.CreateParseError("SQL46010", lastOffset, ex3.getLine(), ex3.getColumn(), TSqlParserResource.SQL46010Message, new string[] { ex3.foundChar.ToString() });
			}
			return new ParseError(46001, lastOffset, exception.recog.getLine(), exception.recog.getColumn(), TSqlParserResource.SQL46001Message);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001FDA3 File Offset: 0x0001DFA3
		internal static ParseError GetFaultTolerantUnexpectedTokenError(IToken token, RecognitionException exception, int lastOffset)
		{
			if (token == null)
			{
				return new ParseError(46001, lastOffset, exception.getLine(), exception.getColumn(), TSqlParserResource.SQL46001Message);
			}
			return TSql80ParserBaseInternal.GetUnexpectedTokenError(token);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001FDCC File Offset: 0x0001DFCC
		public static ParseError GetIncorrectSyntaxError(IToken token)
		{
			return TSql80ParserBaseInternal.CreateParseError("SQL46010", token, TSqlParserResource.SQL46010Message, new string[] { token.getText() });
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001FDFA File Offset: 0x0001DFFA
		public static void ThrowIncorrectSyntaxErrorException(TSqlFragment fragment)
		{
			TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(TSql80ParserBaseInternal.GetFirstToken(fragment));
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0001FE08 File Offset: 0x0001E008
		public static void ThrowIncorrectSyntaxErrorException(IToken token)
		{
			ParseError incorrectSyntaxError = TSql80ParserBaseInternal.GetIncorrectSyntaxError(token);
			throw new TSqlParseErrorException(incorrectSyntaxError);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001FE22 File Offset: 0x0001E022
		protected TSqlParseErrorException GetUnexpectedTokenErrorException()
		{
			return TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(this.LT(1));
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001FE30 File Offset: 0x0001E030
		protected ParseError GetUnexpectedTokenError()
		{
			return TSql80ParserBaseInternal.GetUnexpectedTokenError(this.LT(1));
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001FE40 File Offset: 0x0001E040
		internal static ParseError GetUnexpectedTokenError(IToken token)
		{
			ParseError parseError;
			if (token.Type == 1)
			{
				parseError = TSql80ParserBaseInternal.CreateParseError("SQL46029", token, TSqlParserResource.SQL46029Message, new string[0]);
			}
			else
			{
				parseError = TSql80ParserBaseInternal.GetIncorrectSyntaxError(token);
			}
			return parseError;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001FE78 File Offset: 0x0001E078
		internal static TSqlParseErrorException GetUnexpectedTokenErrorException(IToken token)
		{
			ParseError unexpectedTokenError = TSql80ParserBaseInternal.GetUnexpectedTokenError(token);
			return new TSqlParseErrorException(unexpectedTokenError);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001FE94 File Offset: 0x0001E094
		protected static TSqlParseErrorException GetUnexpectedTokenErrorException(Identifier identifier)
		{
			string text;
			if (identifier.QuoteType != QuoteType.NotQuoted)
			{
				text = Identifier.EncodeIdentifier(identifier.Value);
			}
			else
			{
				text = identifier.Value;
			}
			return new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46010", TSql80ParserBaseInternal.GetFirstToken(identifier), TSqlParserResource.SQL46010Message, new string[] { text }));
		}

		// Token: 0x04000716 RID: 1814
		private const int LookAhead = 2;

		// Token: 0x04000717 RID: 1815
		private readonly TSqlFragmentFactory _fragmentFactory = new TSqlFragmentFactory();

		// Token: 0x04000718 RID: 1816
		private IList<ParseError> _parseErrors;

		// Token: 0x04000719 RID: 1817
		private bool _phaseOne;

		// Token: 0x0400071A RID: 1818
		protected TSqlWhitespaceTokenFilter _tokenSource;

		// Token: 0x0400071B RID: 1819
		private bool _initialQuotedIdentifiersOn = true;

		// Token: 0x0400071C RID: 1820
		private int _phaseOnePreviousStatementLevelErrorLine = -1;

		// Token: 0x0400071D RID: 1821
		private int _phaseOnePreviousStatementLevelErrorColumn = -1;

		// Token: 0x0400071E RID: 1822
		private static readonly BitSet _statementLevelRecoveryTokens = new BitSet(4);

		// Token: 0x0400071F RID: 1823
		private static readonly BitSet _phaseOneBatchLevelRecoveryTokens = new BitSet(3);

		// Token: 0x04000720 RID: 1824
		private static readonly BitSet _ddlStatementBeginnerTokens = new BitSet(2);

		// Token: 0x04000721 RID: 1825
		private static HashSet<SqlDataTypeOption> _possibleSingleParameterDataTypes;

		// Token: 0x04000722 RID: 1826
		private static Dictionary<IndexAffectingStatement, string> _indexOptionContainerStatementNames;

		// Token: 0x020000D0 RID: 208
		// (Invoke) Token: 0x06000A09 RID: 2569
		internal delegate T ParserEntryPoint<T>() where T : TSqlFragment;
	}
}
