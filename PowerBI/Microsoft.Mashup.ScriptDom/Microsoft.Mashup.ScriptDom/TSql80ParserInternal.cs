using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using antlr;
using antlr.collections.impl;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000152 RID: 338
	internal class TSql80ParserInternal : TSql80ParserBaseInternal
	{
		// Token: 0x0600157B RID: 5499 RVA: 0x0009A930 File Offset: 0x00098B30
		public TSql80ParserInternal(bool initialQuotedIdentifiersOn)
			: base(initialQuotedIdentifiersOn)
		{
			this.initialize();
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0009A93F File Offset: 0x00098B3F
		protected void initialize()
		{
			this.tokenNames = TSql80ParserInternal.tokenNames_;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0009A94C File Offset: 0x00098B4C
		protected TSql80ParserInternal(TokenBuffer tokenBuf, int k)
			: base(tokenBuf, k)
		{
			this.initialize();
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0009A95C File Offset: 0x00098B5C
		public TSql80ParserInternal(TokenBuffer tokenBuf)
			: this(tokenBuf, 2)
		{
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0009A966 File Offset: 0x00098B66
		protected TSql80ParserInternal(TokenStream lexer, int k)
			: base(lexer, k)
		{
			this.initialize();
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0009A976 File Offset: 0x00098B76
		public TSql80ParserInternal(TokenStream lexer)
			: this(lexer, 2)
		{
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0009A980 File Offset: 0x00098B80
		public TSql80ParserInternal(ParserSharedInputState state)
			: base(state, 2)
		{
			this.initialize();
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0009A990 File Offset: 0x00098B90
		public ChildObjectName entryPointChildObjectName()
		{
			ChildObjectName childObjectName = this.childObjectNameWithThreePrefixes();
			this.match(1);
			return childObjectName;
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0009A9B0 File Offset: 0x00098BB0
		public ChildObjectName childObjectNameWithThreePrefixes()
		{
			ChildObjectName childObjectName = base.FragmentFactory.CreateFragment<ChildObjectName>();
			List<Identifier> list = this.identifierList(4);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(childObjectName, childObjectName.Identifiers, list);
			}
			return childObjectName;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0009A9EC File Offset: 0x00098BEC
		public SchemaObjectName entryPointSchemaObjectName()
		{
			SchemaObjectName schemaObjectName = this.schemaObjectFourPartName();
			this.match(1);
			return schemaObjectName;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0009AA0C File Offset: 0x00098C0C
		public SchemaObjectName schemaObjectFourPartName()
		{
			SchemaObjectName schemaObjectName = base.FragmentFactory.CreateFragment<SchemaObjectName>();
			List<Identifier> list = this.identifierList(4);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(schemaObjectName, schemaObjectName.Identifiers, list);
			}
			return schemaObjectName;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0009AA48 File Offset: 0x00098C48
		public DataTypeReference entryPointScalarDataType()
		{
			DataTypeReference dataTypeReference = this.scalarDataType();
			this.match(1);
			return dataTypeReference;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0009AA68 File Offset: 0x00098C68
		public DataTypeReference scalarDataType()
		{
			SchemaObjectName schemaObjectName = null;
			SqlDataTypeOption sqlDataTypeOption = SqlDataTypeOption.None;
			int num = this.LA(1);
			DataTypeReference dataTypeReference;
			if (num != 53)
			{
				if (num != 96)
				{
					switch (num)
					{
					case 232:
					case 233:
					{
						Identifier identifier = this.identifier();
						if (this.inputState.guessing == 0)
						{
							schemaObjectName = base.FragmentFactory.CreateFragment<SchemaObjectName>();
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(schemaObjectName, schemaObjectName.Identifiers, identifier);
							sqlDataTypeOption = TSql80ParserBaseInternal.ParseDataType(identifier.Value);
						}
						if (TSql80ParserInternal.tokenSet_0_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_1_.member(this.LA(2)) && sqlDataTypeOption != SqlDataTypeOption.None)
						{
							dataTypeReference = this.sqlDataTypeWithoutNational(schemaObjectName, sqlDataTypeOption);
						}
						else
						{
							if (!TSql80ParserInternal.tokenSet_2_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_1_.member(this.LA(2)))
							{
								throw new NoViableAltException(this.LT(1), this.getFilename());
							}
							dataTypeReference = this.userDataType(schemaObjectName);
						}
						break;
					}
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					dataTypeReference = this.sqlDataTypeWithNational();
				}
			}
			else
			{
				dataTypeReference = this.doubleDataType();
			}
			return dataTypeReference;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0009AB88 File Offset: 0x00098D88
		public ScalarExpression entryPointExpression()
		{
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			this.match(1);
			return scalarExpression;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0009ABA8 File Offset: 0x00098DA8
		public ScalarExpression expression(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			return this.expressionBinaryPri2(expressionFlags);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0009ABC0 File Offset: 0x00098DC0
		public BooleanExpression entryPointBooleanExpression()
		{
			BooleanExpression booleanExpression = this.booleanExpression(ExpressionFlags.None);
			this.match(1);
			return booleanExpression;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0009ABE0 File Offset: 0x00098DE0
		public BooleanExpression booleanExpression(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			return this.booleanExpressionOr(expressionFlags);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0009ABF8 File Offset: 0x00098DF8
		public StatementList entryPointStatementList()
		{
			bool flag = false;
			StatementList statementList = this.statementList(ref flag);
			if (this.inputState.guessing == 0 && flag)
			{
				statementList = null;
			}
			this.match(1);
			return statementList;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0009AC2C File Offset: 0x00098E2C
		public StatementList statementList(ref bool vParseErrorOccurred)
		{
			StatementList statementList = base.FragmentFactory.CreateFragment<StatementList>();
			int num = 0;
			while (TSql80ParserInternal.tokenSet_3_.member(this.LA(1)))
			{
				TSqlStatement tsqlStatement = this.statementOptSemi();
				if (this.inputState.guessing == 0)
				{
					if (tsqlStatement != null)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TSqlStatement>(statementList, statementList.Statements, tsqlStatement);
					}
					else
					{
						vParseErrorOccurred = true;
					}
				}
				num++;
			}
			if (num < 1)
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return statementList;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0009ACA4 File Offset: 0x00098EA4
		public SelectStatement entryPointSubqueryExpressionWithOptionalCTE()
		{
			SelectStatement selectStatement = this.subqueryExpressionAsStatement();
			this.match(1);
			return selectStatement;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0009ACC4 File Offset: 0x00098EC4
		public SelectStatement subqueryExpressionAsStatement()
		{
			SelectStatement selectStatement = base.FragmentFactory.CreateFragment<SelectStatement>();
			QueryExpression queryExpression = this.subqueryExpression();
			if (this.inputState.guessing == 0)
			{
				selectStatement.QueryExpression = queryExpression;
			}
			return selectStatement;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0009ACFC File Offset: 0x00098EFC
		public TSqlFragment entryPointConstantOrIdentifier()
		{
			TSqlFragment tsqlFragment = this.possibleNegativeConstantOrIdentifier();
			this.match(1);
			return tsqlFragment;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0009AD1C File Offset: 0x00098F1C
		public ScalarExpression possibleNegativeConstantOrIdentifier()
		{
			int num = this.LA(1);
			if (num <= 193)
			{
				if (num != 100 && num != 193)
				{
					goto IL_007F;
				}
			}
			else if (num != 199)
			{
				switch (num)
				{
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 234:
					break;
				case 226:
				case 227:
				case 228:
				case 229:
					goto IL_007F;
				case 232:
				case 233:
					return this.identifierLiteral();
				default:
					goto IL_007F;
				}
			}
			return this.possibleNegativeConstant();
			IL_007F:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0009ADBC File Offset: 0x00098FBC
		public TSqlFragment entryPointConstantOrIdentifierWithDefault()
		{
			TSqlFragment tsqlFragment = this.possibleNegativeConstantOrIdentifierWithDefault();
			this.match(1);
			return tsqlFragment;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0009ADDC File Offset: 0x00098FDC
		public ScalarExpression possibleNegativeConstantOrIdentifierWithDefault()
		{
			int num = this.LA(1);
			if (num <= 100)
			{
				if (num == 47)
				{
					return this.defaultLiteral();
				}
				if (num != 100)
				{
					goto IL_0081;
				}
			}
			else if (num != 193 && num != 199)
			{
				switch (num)
				{
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
					break;
				case 226:
				case 227:
				case 228:
				case 229:
					goto IL_0081;
				default:
					goto IL_0081;
				}
			}
			return this.possibleNegativeConstantOrIdentifier();
			IL_0081:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0009AE80 File Offset: 0x00099080
		public TSqlScript script()
		{
			TSqlScript tsqlScript = base.FragmentFactory.CreateFragment<TSqlScript>();
			if (tsqlScript.ScriptTokenStream != null && tsqlScript.ScriptTokenStream.Count > 0)
			{
				tsqlScript.UpdateTokenInfo(0, tsqlScript.ScriptTokenStream.Count - 1);
			}
			TSqlBatch tsqlBatch = this.batch();
			if (this.inputState.guessing == 0 && tsqlBatch != null)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TSqlBatch>(tsqlScript, tsqlScript.Batches, tsqlBatch);
			}
			while (this.LA(1) == 219)
			{
				this.match(219);
				if (this.inputState.guessing == 0)
				{
					base.ResetQuotedIdentifiersSettingToInitial();
					base.ThrowPartialAstIfPhaseOne(null);
				}
				tsqlBatch = this.batch();
				if (this.inputState.guessing == 0 && tsqlBatch != null)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TSqlBatch>(tsqlScript, tsqlScript.Batches, tsqlBatch);
				}
			}
			IToken token = this.LT(1);
			this.match(1);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(tsqlScript, token);
			}
			return tsqlScript;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0009AF68 File Offset: 0x00099168
		public TSqlBatch batch()
		{
			TSqlBatch tsqlBatch = null;
			try
			{
				bool flag = false;
				if ((this.LA(1) == 6 || this.LA(1) == 35) && TSql80ParserInternal.tokenSet_4_.member(this.LA(2)))
				{
					int num = this.mark();
					flag = true;
					this.inputState.guessing++;
					try
					{
						int num2 = this.LA(1);
						if (num2 == 6)
						{
							this.match(6);
							int num3 = this.LA(1);
							if (num3 <= 121)
							{
								if (num3 == 73)
								{
									this.match(73);
									goto IL_01FB;
								}
								switch (num3)
								{
								case 120:
									this.match(120);
									goto IL_01FB;
								case 121:
									this.match(121);
									goto IL_01FB;
								}
							}
							else
							{
								if (num3 == 155)
								{
									this.match(155);
									goto IL_01FB;
								}
								if (num3 == 166)
								{
									this.match(166);
									goto IL_01FB;
								}
							}
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						if (num2 == 35)
						{
							this.match(35);
							int num4 = this.LA(1);
							if (num4 <= 121)
							{
								if (num4 == 47)
								{
									this.match(47);
									goto IL_01FB;
								}
								if (num4 == 73)
								{
									this.match(73);
									goto IL_01FB;
								}
								switch (num4)
								{
								case 120:
									this.match(120);
									goto IL_01FB;
								case 121:
									this.match(121);
									goto IL_01FB;
								}
							}
							else
							{
								switch (num4)
								{
								case 137:
									this.match(137);
									goto IL_01FB;
								case 138:
									break;
								case 139:
									this.match(139);
									goto IL_01FB;
								default:
									if (num4 == 155)
									{
										this.match(155);
										goto IL_01FB;
									}
									if (num4 == 166)
									{
										this.match(166);
										goto IL_01FB;
									}
									break;
								}
							}
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						throw new NoViableAltException(this.LT(1), this.getFilename());
						IL_01FB:;
					}
					catch (RecognitionException)
					{
						flag = false;
					}
					this.rewind(num);
					this.inputState.guessing--;
				}
				if (flag)
				{
					TSqlStatement tsqlStatement = this.lastStatement();
					if (this.inputState.guessing == 0 && tsqlStatement != null)
					{
						if (tsqlBatch == null)
						{
							tsqlBatch = base.FragmentFactory.CreateFragment<TSqlBatch>();
						}
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TSqlStatement>(tsqlBatch, tsqlBatch.Statements, tsqlStatement);
					}
				}
				else
				{
					if (!TSql80ParserInternal.tokenSet_5_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_6_.member(this.LA(2)))
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					TSqlStatement tsqlStatement = this.optSimpleExecute();
					if (this.inputState.guessing == 0 && tsqlStatement != null)
					{
						base.ThrowPartialAstIfPhaseOne(tsqlStatement);
						if (tsqlBatch == null)
						{
							tsqlBatch = base.FragmentFactory.CreateFragment<TSqlBatch>();
						}
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TSqlStatement>(tsqlBatch, tsqlBatch.Statements, tsqlStatement);
					}
					while (TSql80ParserInternal.tokenSet_3_.member(this.LA(1)))
					{
						tsqlStatement = this.statementOptSemi();
						if (this.inputState.guessing == 0 && tsqlStatement != null)
						{
							if (tsqlBatch == null)
							{
								tsqlBatch = base.FragmentFactory.CreateFragment<TSqlBatch>();
							}
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TSqlStatement>(tsqlBatch, tsqlBatch.Statements, tsqlStatement);
						}
					}
				}
			}
			catch (TSqlParseErrorException ex)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				if (!ex.DoNotLog)
				{
					base.AddParseError(ex.ParseError);
				}
				base.RecoverAtBatchLevel();
			}
			catch (NoViableAltException ex2)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				ParseError faultTolerantUnexpectedTokenError = TSql80ParserBaseInternal.GetFaultTolerantUnexpectedTokenError(ex2.token, ex2, this._tokenSource.LastToken.Offset);
				base.AddParseError(faultTolerantUnexpectedTokenError);
				base.RecoverAtBatchLevel();
			}
			catch (MismatchedTokenException ex3)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				ParseError faultTolerantUnexpectedTokenError2 = TSql80ParserBaseInternal.GetFaultTolerantUnexpectedTokenError(ex3.token, ex3, this._tokenSource.LastToken.Offset);
				base.AddParseError(faultTolerantUnexpectedTokenError2);
				base.RecoverAtBatchLevel();
			}
			catch (RecognitionException)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				ParseError unexpectedTokenError = base.GetUnexpectedTokenError();
				base.AddParseError(unexpectedTokenError);
				base.RecoverAtBatchLevel();
			}
			catch (TokenStreamRecognitionException ex4)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				ParseError parseError = TSql80ParserBaseInternal.ProcessTokenStreamRecognitionException(ex4, this._tokenSource.LastToken.Offset);
				base.AddParseError(parseError);
				base.RecoverAtBatchLevel();
			}
			catch (ANTLRException ex5)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				base.CreateInternalError("batch", ex5);
			}
			return tsqlBatch;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0009B490 File Offset: 0x00099690
		public TSqlStatement lastStatement()
		{
			TSqlStatement tsqlStatement;
			if (this.LA(1) == 35 && (this.LA(2) == 120 || this.LA(2) == 121))
			{
				tsqlStatement = this.createProcedureStatement();
			}
			else if (this.LA(1) == 6 && (this.LA(2) == 120 || this.LA(2) == 121))
			{
				tsqlStatement = this.alterProcedureStatement();
			}
			else if (this.LA(1) == 35 && this.LA(2) == 155)
			{
				tsqlStatement = this.createTriggerStatement();
			}
			else if (this.LA(1) == 6 && this.LA(2) == 155)
			{
				tsqlStatement = this.alterTriggerStatement();
			}
			else if (this.LA(1) == 35 && this.LA(2) == 47)
			{
				tsqlStatement = this.createDefaultStatement();
			}
			else if (this.LA(1) == 35 && this.LA(2) == 137)
			{
				tsqlStatement = this.createRuleStatement();
			}
			else if (this.LA(1) == 35 && this.LA(2) == 166)
			{
				tsqlStatement = this.createViewStatement();
			}
			else if (this.LA(1) == 6 && this.LA(2) == 166)
			{
				tsqlStatement = this.alterViewStatement();
			}
			else if (this.LA(1) == 35 && this.LA(2) == 73)
			{
				tsqlStatement = this.createFunctionStatement();
			}
			else if (this.LA(1) == 6 && this.LA(2) == 73)
			{
				tsqlStatement = this.alterFunctionStatement();
			}
			else
			{
				if (this.LA(1) != 35 || this.LA(2) != 139)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				tsqlStatement = this.createSchemaStatement();
			}
			return tsqlStatement;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0009B644 File Offset: 0x00099844
		public ExecuteStatement optSimpleExecute()
		{
			ExecuteStatement executeStatement = null;
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 35)
				{
					if (num <= 17)
					{
						if (num == 1 || num == 6)
						{
							return executeStatement;
						}
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return executeStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 22:
						case 23:
							return executeStatement;
						default:
							if (num == 28)
							{
								return executeStatement;
							}
							switch (num)
							{
							case 33:
							case 35:
								return executeStatement;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
						return executeStatement;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
						break;
					default:
						switch (num)
						{
						case 60:
						case 61:
						case 64:
							return executeStatement;
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return executeStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return executeStatement;
				}
			}
			else if (num <= 173)
			{
				if (num <= 119)
				{
					if (num == 95 || num == 106 || num == 119)
					{
						return executeStatement;
					}
				}
				else
				{
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return executeStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return executeStatement;
						case 157:
						case 158:
						case 159:
							break;
						default:
							switch (num)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								return executeStatement;
							}
							break;
						}
						break;
					}
				}
			}
			else
			{
				if (num > 191)
				{
					if (num != 200)
					{
						switch (num)
						{
						case 219:
						case 220:
							return executeStatement;
						default:
							switch (num)
							{
							case 232:
							case 233:
							case 234:
								break;
							default:
								goto IL_029D;
							}
							break;
						}
					}
					ExecutableProcedureReference executableProcedureReference = this.execProc();
					if (this.inputState.guessing == 0)
					{
						executeStatement = base.FragmentFactory.CreateFragment<ExecuteStatement>();
						ExecuteSpecification executeSpecification = base.FragmentFactory.CreateFragment<ExecuteSpecification>();
						executeSpecification.ExecutableEntity = executableProcedureReference;
						executeStatement.ExecuteSpecification = executeSpecification;
					}
					this.optSingleSemicolon(executeStatement);
					return executeStatement;
				}
				if (num == 176)
				{
					return executeStatement;
				}
				switch (num)
				{
				case 180:
				case 181:
					return executeStatement;
				default:
					if (num == 191)
					{
						return executeStatement;
					}
					break;
				}
			}
			IL_029D:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0009B904 File Offset: 0x00099B04
		public TSqlStatement statementOptSemi()
		{
			TSqlStatement tsqlStatement = this.statement();
			this.optSingleSemicolon(tsqlStatement);
			return tsqlStatement;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0009B924 File Offset: 0x00099B24
		public TSqlStatement statement()
		{
			TSqlStatement tsqlStatement = null;
			int line = this.LT(1).getLine();
			int column = this.LT(1).getColumn();
			try
			{
				int num = this.LA(1);
				if (num > 95)
				{
					if (num <= 162)
					{
						if (num <= 119)
						{
							if (num == 106)
							{
								tsqlStatement = this.openCursorStatement();
								goto IL_05D4;
							}
							if (num != 119)
							{
								goto IL_0451;
							}
							tsqlStatement = this.printStatement();
							goto IL_05D4;
						}
						else
						{
							switch (num)
							{
							case 123:
								tsqlStatement = this.raiseErrorStatements();
								goto IL_05D4;
							case 124:
							case 127:
							case 128:
							case 130:
							case 133:
							case 135:
							case 136:
							case 137:
							case 139:
							case 141:
								goto IL_0451;
							case 125:
								tsqlStatement = this.readTextStatement();
								goto IL_05D4;
							case 126:
								tsqlStatement = this.reconfigureStatement();
								goto IL_05D4;
							case 129:
								break;
							case 131:
								tsqlStatement = this.returnStatement();
								goto IL_05D4;
							case 132:
								tsqlStatement = this.revokeStatement80();
								goto IL_05D4;
							case 134:
								tsqlStatement = this.rollbackTransactionStatement();
								goto IL_05D4;
							case 138:
								tsqlStatement = this.saveTransactionStatement();
								goto IL_05D4;
							case 140:
								goto IL_02F5;
							case 142:
								tsqlStatement = this.setStatements();
								goto IL_05D4;
							case 143:
								tsqlStatement = this.setUserStatement();
								goto IL_05D4;
							case 144:
								tsqlStatement = this.shutdownStatement();
								goto IL_05D4;
							default:
								if (num == 156)
								{
									tsqlStatement = this.truncateTableStatement();
									goto IL_05D4;
								}
								switch (num)
								{
								case 161:
									tsqlStatement = this.updateTextStatement();
									goto IL_05D4;
								case 162:
									tsqlStatement = this.useStatement();
									goto IL_05D4;
								default:
									goto IL_0451;
								}
								break;
							}
						}
					}
					else if (num <= 176)
					{
						switch (num)
						{
						case 167:
							tsqlStatement = this.waitForStatement();
							goto IL_05D4;
						case 168:
						case 169:
						case 171:
							goto IL_0451;
						case 170:
							tsqlStatement = this.whileStatement();
							goto IL_05D4;
						case 172:
							tsqlStatement = this.writeTextStatement();
							goto IL_05D4;
						case 173:
							tsqlStatement = this.diskStatement();
							goto IL_05D4;
						default:
							if (num != 176)
							{
								goto IL_0451;
							}
							tsqlStatement = this.revertStatement();
							goto IL_05D4;
						}
					}
					else
					{
						switch (num)
						{
						case 180:
							goto IL_02A1;
						case 181:
							break;
						default:
							if (num == 191)
							{
								goto IL_02F5;
							}
							if (num != 220)
							{
								goto IL_0451;
							}
							tsqlStatement = this.labelStatement();
							goto IL_05D4;
						}
					}
					tsqlStatement = this.restoreStatement();
					goto IL_05D4;
					IL_02F5:
					tsqlStatement = this.select();
					goto IL_05D4;
				}
				if (num <= 54)
				{
					if (num <= 23)
					{
						switch (num)
						{
						case 12:
							break;
						case 13:
							tsqlStatement = this.beginStatements();
							goto IL_05D4;
						case 14:
						case 16:
							goto IL_0451;
						case 15:
							tsqlStatement = this.breakStatement();
							goto IL_05D4;
						case 17:
							tsqlStatement = this.bulkInsertStatement();
							goto IL_05D4;
						default:
							switch (num)
							{
							case 22:
								tsqlStatement = this.checkpointStatement();
								goto IL_05D4;
							case 23:
								tsqlStatement = this.closeCursorStatement();
								goto IL_05D4;
							default:
								goto IL_0451;
							}
							break;
						}
					}
					else
					{
						if (num == 28)
						{
							tsqlStatement = this.commitTransactionStatement();
							goto IL_05D4;
						}
						if (num == 33)
						{
							tsqlStatement = this.continueStatement();
							goto IL_05D4;
						}
						switch (num)
						{
						case 44:
							tsqlStatement = this.dbccStatement();
							goto IL_05D4;
						case 45:
							tsqlStatement = this.deallocateCursorStatement();
							goto IL_05D4;
						case 46:
							tsqlStatement = this.declareStatements();
							goto IL_05D4;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
							goto IL_0451;
						case 48:
							tsqlStatement = this.deleteStatement();
							goto IL_05D4;
						case 49:
							tsqlStatement = this.denyStatement80();
							goto IL_05D4;
						case 54:
							tsqlStatement = this.dropStatements();
							goto IL_05D4;
						default:
							goto IL_0451;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 60:
					case 61:
						tsqlStatement = this.executeStatement();
						goto IL_05D4;
					case 62:
					case 63:
						goto IL_0451;
					case 64:
						tsqlStatement = this.fetchCursorStatement();
						goto IL_05D4;
					default:
						switch (num)
						{
						case 74:
							tsqlStatement = this.gotoStatement();
							goto IL_05D4;
						case 75:
							tsqlStatement = this.grantStatement80();
							goto IL_05D4;
						default:
							goto IL_0451;
						}
						break;
					}
				}
				else
				{
					if (num == 82)
					{
						tsqlStatement = this.ifStatement();
						goto IL_05D4;
					}
					if (num == 92)
					{
						tsqlStatement = this.killStatement();
						goto IL_05D4;
					}
					if (num != 95)
					{
						goto IL_0451;
					}
					tsqlStatement = this.lineNoStatement();
					goto IL_05D4;
				}
				IL_02A1:
				tsqlStatement = this.backupStatement();
				goto IL_05D4;
				IL_0451:
				if (this.LA(1) == 35 && this.LA(2) == 148)
				{
					tsqlStatement = this.createTableStatement();
				}
				else if (this.LA(1) == 6 && this.LA(2) == 148)
				{
					tsqlStatement = this.alterTableStatement();
				}
				else if (this.LA(1) == 35 && TSql80ParserInternal.tokenSet_7_.member(this.LA(2)))
				{
					tsqlStatement = this.createIndexStatement();
				}
				else if (this.LA(1) == 35 && this.LA(2) == 146)
				{
					tsqlStatement = this.createStatisticsStatement();
				}
				else if (this.LA(1) == 160 && this.LA(2) == 146)
				{
					tsqlStatement = this.updateStatisticsStatement();
				}
				else if (this.LA(1) == 6 && this.LA(2) == 43)
				{
					tsqlStatement = this.alterDatabaseStatements();
				}
				else if (this.LA(1) == 86 && TSql80ParserInternal.tokenSet_8_.member(this.LA(2)))
				{
					tsqlStatement = this.insertStatement();
				}
				else if (this.LA(1) == 160 && TSql80ParserInternal.tokenSet_9_.member(this.LA(2)))
				{
					tsqlStatement = this.updateStatement();
				}
				else if (this.LA(1) == 35 && this.LA(2) == 43)
				{
					tsqlStatement = this.createDatabaseStatement();
				}
				else
				{
					if (this.LA(1) != 86 || this.LA(2) != 17)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					tsqlStatement = this.insertBulkStatement();
				}
				IL_05D4:;
			}
			catch (TSqlParseErrorException ex)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				if (!ex.DoNotLog)
				{
					base.AddParseError(ex.ParseError);
				}
				base.RecoverAtStatementLevel(line, column);
			}
			catch (NoViableAltException ex2)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				ParseError faultTolerantUnexpectedTokenError = TSql80ParserBaseInternal.GetFaultTolerantUnexpectedTokenError(ex2.token, ex2, this._tokenSource.LastToken.Offset);
				base.AddParseError(faultTolerantUnexpectedTokenError);
				base.RecoverAtStatementLevel(line, column);
			}
			catch (MismatchedTokenException ex3)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				ParseError faultTolerantUnexpectedTokenError2 = TSql80ParserBaseInternal.GetFaultTolerantUnexpectedTokenError(ex3.token, ex3, this._tokenSource.LastToken.Offset);
				base.AddParseError(faultTolerantUnexpectedTokenError2);
				base.RecoverAtStatementLevel(line, column);
			}
			catch (RecognitionException)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				ParseError unexpectedTokenError = base.GetUnexpectedTokenError();
				base.AddParseError(unexpectedTokenError);
				base.RecoverAtStatementLevel(line, column);
			}
			catch (TokenStreamRecognitionException ex4)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				ParseError parseError = TSql80ParserBaseInternal.ProcessTokenStreamRecognitionException(ex4, this._tokenSource.LastToken.Offset);
				base.AddParseError(parseError);
				base.RecoverAtStatementLevel(line, column);
			}
			catch (ANTLRException ex5)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				base.CreateInternalError("statement", ex5);
			}
			return tsqlStatement;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0009C0F0 File Offset: 0x0009A2F0
		public void optSingleSemicolon(TSqlStatement vParent)
		{
			if (this.LA(1) == 204 && TSql80ParserInternal.tokenSet_10_.member(this.LA(2)))
			{
				IToken token = this.LT(1);
				this.match(204);
				if (this.inputState.guessing == 0 && vParent != null)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					return;
				}
				return;
			}
			else
			{
				if (TSql80ParserInternal.tokenSet_10_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_11_.member(this.LA(2)))
				{
					return;
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0009C188 File Offset: 0x0009A388
		public ExecutableProcedureReference execProc()
		{
			ExecutableProcedureReference executableProcedureReference = base.FragmentFactory.CreateFragment<ExecutableProcedureReference>();
			int num = this.LA(1);
			ProcedureReferenceName procedureReferenceName;
			if (num != 200)
			{
				switch (num)
				{
				case 232:
				case 233:
					break;
				case 234:
					procedureReferenceName = this.varObjectReference();
					goto IL_005B;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			procedureReferenceName = this.procObjectReference();
			IL_005B:
			if (this.inputState.guessing == 0)
			{
				executableProcedureReference.ProcedureReference = procedureReferenceName;
			}
			int num2 = this.LA(1);
			if (num2 <= 92)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 != 1 && num2 != 6)
						{
							goto IL_0326;
						}
						return executableProcedureReference;
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return executableProcedureReference;
						case 14:
						case 16:
							goto IL_0326;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return executableProcedureReference;
							default:
								if (num2 != 28)
								{
									goto IL_0326;
								}
								return executableProcedureReference;
							}
							break;
						}
					}
				}
				else if (num2 <= 75)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return executableProcedureReference;
					case 34:
						goto IL_0326;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return executableProcedureReference;
						case 47:
							break;
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							goto IL_0326;
						default:
							switch (num2)
							{
							case 74:
							case 75:
								return executableProcedureReference;
							default:
								goto IL_0326;
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (num2 != 82 && num2 != 86 && num2 != 92)
					{
						goto IL_0326;
					}
					return executableProcedureReference;
				}
			}
			else if (num2 <= 144)
			{
				if (num2 <= 106)
				{
					if (num2 == 95)
					{
						return executableProcedureReference;
					}
					if (num2 != 100)
					{
						if (num2 != 106)
						{
							goto IL_0326;
						}
						return executableProcedureReference;
					}
				}
				else
				{
					if (num2 == 111 || num2 == 119)
					{
						return executableProcedureReference;
					}
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return executableProcedureReference;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						goto IL_0326;
					default:
						goto IL_0326;
					}
				}
			}
			else if (num2 <= 193)
			{
				switch (num2)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return executableProcedureReference;
				case 157:
				case 158:
				case 159:
					goto IL_0326;
				default:
					switch (num2)
					{
					case 167:
					case 170:
					case 171:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return executableProcedureReference;
					case 168:
					case 169:
					case 174:
					case 175:
					case 177:
					case 178:
					case 179:
						goto IL_0326;
					default:
						switch (num2)
						{
						case 191:
							return executableProcedureReference;
						case 192:
							goto IL_0326;
						case 193:
							break;
						default:
							goto IL_0326;
						}
						break;
					}
					break;
				}
			}
			else if (num2 != 199)
			{
				if (num2 == 204)
				{
					return executableProcedureReference;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return executableProcedureReference;
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
					break;
				case 226:
				case 227:
				case 228:
				case 229:
					goto IL_0326;
				default:
					goto IL_0326;
				}
			}
			this.setParamList(executableProcedureReference);
			return executableProcedureReference;
			IL_0326:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0009C4D0 File Offset: 0x0009A6D0
		public CreateTableStatement createTableStatement()
		{
			CreateTableStatement createTableStatement = base.FragmentFactory.CreateFragment<CreateTableStatement>();
			IToken token = this.LT(1);
			this.match(35);
			this.match(148);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createTableStatement, token);
				createTableStatement.SchemaObjectName = schemaObjectName;
				base.ThrowPartialAstIfPhaseOne(createTableStatement);
			}
			this.match(191);
			TableDefinition tableDefinition = this.tableDefinitionCreateTable();
			if (this.inputState.guessing == 0)
			{
				createTableStatement.Definition = tableDefinition;
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createTableStatement, token2);
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							goto IL_035B;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_035B;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								goto IL_035B;
							default:
								if (num == 28)
								{
									goto IL_035B;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						goto IL_035B;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							goto IL_035B;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								goto IL_035B;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					goto IL_035B;
				}
			}
			else if (num <= 173)
			{
				if (num <= 119)
				{
					if (num == 95)
					{
						goto IL_035B;
					}
					switch (num)
					{
					case 105:
					{
						this.match(105);
						FileGroupOrPartitionScheme fileGroupOrPartitionScheme = this.filegroupOrPartitionScheme();
						if (this.inputState.guessing == 0)
						{
							createTableStatement.OnFileGroupOrPartitionScheme = fileGroupOrPartitionScheme;
							goto IL_035B;
						}
						goto IL_035B;
					}
					case 106:
						goto IL_035B;
					default:
						if (num == 119)
						{
							goto IL_035B;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_035B;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							goto IL_035B;
						case 157:
						case 158:
						case 159:
							break;
						default:
							switch (num)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								goto IL_035B;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 191)
			{
				if (num == 176)
				{
					goto IL_035B;
				}
				switch (num)
				{
				case 180:
				case 181:
					goto IL_035B;
				default:
					if (num == 191)
					{
						goto IL_035B;
					}
					break;
				}
			}
			else
			{
				if (num == 204)
				{
					goto IL_035B;
				}
				switch (num)
				{
				case 219:
				case 220:
					goto IL_035B;
				default:
					if (num == 232)
					{
						goto IL_035B;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_035B:
			int num2 = this.LA(1);
			if (num2 <= 92)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return createTableStatement;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return createTableStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return createTableStatement;
							default:
								if (num2 == 28)
								{
									return createTableStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 75)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return createTableStatement;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return createTableStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num2)
							{
							case 74:
							case 75:
								return createTableStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num2 == 82 || num2 == 86 || num2 == 92)
				{
					return createTableStatement;
				}
			}
			else if (num2 <= 173)
			{
				if (num2 <= 119)
				{
					if (num2 == 95 || num2 == 106 || num2 == 119)
					{
						return createTableStatement;
					}
				}
				else
				{
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return createTableStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num2)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return createTableStatement;
						case 157:
						case 158:
						case 159:
							break;
						default:
							switch (num2)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								return createTableStatement;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num2 <= 191)
			{
				if (num2 == 176)
				{
					return createTableStatement;
				}
				switch (num2)
				{
				case 180:
				case 181:
					return createTableStatement;
				default:
					if (num2 == 191)
					{
						return createTableStatement;
					}
					break;
				}
			}
			else
			{
				if (num2 == 204)
				{
					return createTableStatement;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return createTableStatement;
				default:
					if (num2 == 232)
					{
						IToken token3 = this.LT(1);
						this.match(232);
						IdentifierOrValueExpression identifierOrValueExpression = this.stringOrIdentifier();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.Match(token3, "TEXTIMAGE_ON");
							createTableStatement.TextImageOn = identifierOrValueExpression;
							return createTableStatement;
						}
						return createTableStatement;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0009CAF8 File Offset: 0x0009ACF8
		public AlterTableStatement alterTableStatement()
		{
			AlterTableStatement alterTableStatement = null;
			IToken token = null;
			SchemaObjectName schemaObjectName = null;
			ConstraintEnforcement constraintEnforcement = ConstraintEnforcement.NotSpecified;
			try
			{
				token = this.LT(1);
				this.match(6);
				this.match(148);
				schemaObjectName = this.schemaObjectThreePartName();
				int num = this.LA(1);
				if (num <= 54)
				{
					switch (num)
					{
					case 4:
						break;
					case 5:
						goto IL_0134;
					case 6:
						alterTableStatement = this.alterTableAlterColumnStatement();
						goto IL_0147;
					default:
						if (num != 21)
						{
							if (num != 54)
							{
								goto IL_0134;
							}
							alterTableStatement = this.alterTableDropTableElementStatement();
							goto IL_0147;
						}
						break;
					}
				}
				else if (num != 97 && num != 171)
				{
					if (num != 232)
					{
						goto IL_0134;
					}
					alterTableStatement = this.alterTableTriggerModificationStatement();
					goto IL_0147;
				}
				int num2 = this.LA(1);
				if (num2 <= 21)
				{
					if (num2 == 4 || num2 == 21)
					{
						goto IL_00F1;
					}
				}
				else
				{
					if (num2 == 97)
					{
						goto IL_00F1;
					}
					if (num2 == 171)
					{
						this.match(171);
						constraintEnforcement = this.constraintEnforcement();
						goto IL_00F1;
					}
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
				IL_00F1:
				int num3 = this.LA(1);
				if (num3 == 4)
				{
					alterTableStatement = this.alterTableAddTableElementStatement(constraintEnforcement);
					goto IL_0147;
				}
				if (num3 != 21 && num3 != 97)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				alterTableStatement = this.alterTableConstraintModificationStatement(constraintEnforcement);
				goto IL_0147;
				IL_0134:
				throw new NoViableAltException(this.LT(1), this.getFilename());
				IL_0147:
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(alterTableStatement, token);
					alterTableStatement.SchemaObjectName = schemaObjectName;
				}
			}
			catch (PhaseOnePartialAstException ex)
			{
				if (this.inputState.guessing == 0)
				{
					AlterTableStatement alterTableStatement2 = ex.Statement as AlterTableStatement;
					TSql80ParserBaseInternal.UpdateTokenInfo(alterTableStatement2, token);
					alterTableStatement2.SchemaObjectName = schemaObjectName;
					throw;
				}
				throw;
			}
			return alterTableStatement;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0009CCB8 File Offset: 0x0009AEB8
		public CreateIndexStatement createIndexStatement()
		{
			CreateIndexStatement createIndexStatement = base.FragmentFactory.CreateFragment<CreateIndexStatement>();
			IToken token = this.LT(1);
			this.match(35);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createIndexStatement, token);
			}
			int num = this.LA(1);
			if (num <= 84)
			{
				if (num == 24 || num == 84)
				{
					goto IL_0094;
				}
			}
			else
			{
				if (num == 98)
				{
					goto IL_0094;
				}
				if (num == 159)
				{
					this.match(159);
					if (this.inputState.guessing == 0)
					{
						createIndexStatement.Unique = true;
						goto IL_0094;
					}
					goto IL_0094;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0094:
			int num2 = this.LA(1);
			if (num2 != 24)
			{
				if (num2 != 84)
				{
					if (num2 != 98)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(98);
					if (this.inputState.guessing == 0)
					{
						createIndexStatement.Clustered = new bool?(false);
					}
				}
			}
			else
			{
				this.match(24);
				if (this.inputState.guessing == 0)
				{
					createIndexStatement.Clustered = new bool?(true);
				}
			}
			this.match(84);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				createIndexStatement.Name = identifier;
			}
			this.match(105);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				createIndexStatement.OnName = schemaObjectName;
				base.ThrowPartialAstIfPhaseOne(createIndexStatement);
			}
			this.match(191);
			ColumnWithSortOrder columnWithSortOrder = this.columnWithSortOrder();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnWithSortOrder>(createIndexStatement, createIndexStatement.Columns, columnWithSortOrder);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				columnWithSortOrder = this.columnWithSortOrder();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnWithSortOrder>(createIndexStatement, createIndexStatement.Columns, columnWithSortOrder);
				}
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createIndexStatement, token2);
			}
			int num3 = this.LA(1);
			if (num3 <= 86)
			{
				if (num3 <= 28)
				{
					if (num3 <= 6)
					{
						if (num3 == 1 || num3 == 6)
						{
							goto IL_0494;
						}
					}
					else
					{
						switch (num3)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_0494;
						case 14:
						case 16:
							break;
						default:
							switch (num3)
							{
							case 22:
							case 23:
								goto IL_0494;
							default:
								if (num3 == 28)
								{
									goto IL_0494;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num3 <= 64)
				{
					switch (num3)
					{
					case 33:
					case 35:
						goto IL_0494;
					case 34:
						break;
					default:
						switch (num3)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							goto IL_0494;
						}
						break;
					}
				}
				else
				{
					switch (num3)
					{
					case 74:
					case 75:
						goto IL_0494;
					default:
						if (num3 == 82 || num3 == 86)
						{
							goto IL_0494;
						}
						break;
					}
				}
			}
			else if (num3 <= 144)
			{
				if (num3 <= 95)
				{
					if (num3 == 92 || num3 == 95)
					{
						goto IL_0494;
					}
				}
				else
				{
					switch (num3)
					{
					case 105:
					case 106:
						goto IL_0494;
					default:
						if (num3 == 119)
						{
							goto IL_0494;
						}
						switch (num3)
						{
						case 123:
						case 125:
						case 126:
						case 129:
						case 131:
						case 132:
						case 134:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							goto IL_0494;
						}
						break;
					}
				}
			}
			else if (num3 <= 181)
			{
				switch (num3)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					goto IL_0494;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num3)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						goto IL_0494;
					case 171:
						this.match(171);
						this.indexLegacyOptionList(createIndexStatement);
						if (this.inputState.guessing == 0)
						{
							createIndexStatement.Translated80SyntaxTo90 = true;
							goto IL_0494;
						}
						goto IL_0494;
					}
					break;
				}
			}
			else
			{
				if (num3 == 191 || num3 == 204)
				{
					goto IL_0494;
				}
				switch (num3)
				{
				case 219:
				case 220:
					goto IL_0494;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0494:
			int num4 = this.LA(1);
			if (num4 <= 92)
			{
				if (num4 <= 28)
				{
					if (num4 <= 6)
					{
						if (num4 == 1 || num4 == 6)
						{
							return createIndexStatement;
						}
					}
					else
					{
						switch (num4)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return createIndexStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num4)
							{
							case 22:
							case 23:
								return createIndexStatement;
							default:
								if (num4 == 28)
								{
									return createIndexStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num4 <= 75)
				{
					switch (num4)
					{
					case 33:
					case 35:
						return createIndexStatement;
					case 34:
						break;
					default:
						switch (num4)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return createIndexStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num4)
							{
							case 74:
							case 75:
								return createIndexStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num4 == 82 || num4 == 86 || num4 == 92)
				{
					return createIndexStatement;
				}
			}
			else if (num4 <= 162)
			{
				if (num4 <= 106)
				{
					if (num4 == 95)
					{
						return createIndexStatement;
					}
					switch (num4)
					{
					case 105:
					{
						this.match(105);
						FileGroupOrPartitionScheme fileGroupOrPartitionScheme = this.filegroupOrPartitionScheme();
						if (this.inputState.guessing == 0)
						{
							createIndexStatement.OnFileGroupOrPartitionScheme = fileGroupOrPartitionScheme;
							return createIndexStatement;
						}
						return createIndexStatement;
					}
					case 106:
						return createIndexStatement;
					}
				}
				else
				{
					if (num4 == 119)
					{
						return createIndexStatement;
					}
					switch (num4)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return createIndexStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num4)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return createIndexStatement;
						}
						break;
					}
				}
			}
			else if (num4 <= 181)
			{
				switch (num4)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return createIndexStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num4 == 176)
					{
						return createIndexStatement;
					}
					switch (num4)
					{
					case 180:
					case 181:
						return createIndexStatement;
					}
					break;
				}
			}
			else
			{
				if (num4 == 191 || num4 == 204)
				{
					return createIndexStatement;
				}
				switch (num4)
				{
				case 219:
				case 220:
					return createIndexStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0009D404 File Offset: 0x0009B604
		public TSqlStatement declareStatements()
		{
			TSqlStatement tsqlStatement = null;
			IToken token = null;
			token = this.LT(1);
			this.match(46);
			bool flag = false;
			if (this.LA(1) == 234 && this.LA(2) == 148)
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(234);
					int num2 = this.LA(1);
					if (num2 != 9)
					{
						if (num2 != 148)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
					}
					else
					{
						this.match(9);
					}
					this.match(148);
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag)
			{
				DeclareTableVariableBody declareTableVariableBody = this.declareTableBody(IndexAffectingStatement.DeclareTableVariable);
				if (this.inputState.guessing == 0)
				{
					DeclareTableVariableStatement declareTableVariableStatement = base.FragmentFactory.CreateFragment<DeclareTableVariableStatement>();
					declareTableVariableStatement.Body = declareTableVariableBody;
					tsqlStatement = declareTableVariableStatement;
				}
			}
			else if (this.LA(1) == 234 && TSql80ParserInternal.tokenSet_12_.member(this.LA(2)))
			{
				tsqlStatement = this.declareVariableStatement();
			}
			else
			{
				if (this.LA(1) != 232 && this.LA(1) != 233)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				tsqlStatement = this.declareCursorStatement();
			}
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(tsqlStatement, token);
			}
			return tsqlStatement;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0009D590 File Offset: 0x0009B790
		public TSqlStatement setStatements()
		{
			IToken token = this.LT(1);
			this.match(142);
			int num = this.LA(1);
			TSqlStatement tsqlStatement;
			if (num <= 135)
			{
				if (num <= 80)
				{
					if (num == 57)
					{
						tsqlStatement = this.setErrorLevelStatement();
						goto IL_016E;
					}
					if (num == 80)
					{
						tsqlStatement = this.setIdentityInsertStatement();
						goto IL_016E;
					}
				}
				else
				{
					if (num == 104)
					{
						tsqlStatement = this.setOffsetsStatement();
						goto IL_016E;
					}
					if (num == 135)
					{
						tsqlStatement = this.setRowcountStatement();
						goto IL_016E;
					}
				}
			}
			else if (num <= 149)
			{
				if (num == 146)
				{
					tsqlStatement = this.setStatisticsStatement();
					goto IL_016E;
				}
				if (num == 149)
				{
					tsqlStatement = this.setTextSizeStatement();
					goto IL_016E;
				}
			}
			else
			{
				switch (num)
				{
				case 153:
				case 154:
					tsqlStatement = this.setTransactionIsolationLevelStatement();
					goto IL_016E;
				default:
					if (num == 234)
					{
						tsqlStatement = this.setVariableStatement();
						goto IL_016E;
					}
					break;
				}
			}
			if (this.LA(1) == 232 && (this.LA(2) == 103 || this.LA(2) == 105 || this.LA(2) == 198) && !base.NextTokenMatches("FIPS_FLAGGER"))
			{
				tsqlStatement = this.predicateSetStatement();
			}
			else
			{
				if (this.LA(1) != 232 || !TSql80ParserInternal.tokenSet_13_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				tsqlStatement = this.setCommandStatement();
			}
			IL_016E:
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(tsqlStatement, token);
			}
			return tsqlStatement;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0009D720 File Offset: 0x0009B920
		public TSqlStatement beginStatements()
		{
			bool flag = false;
			if (this.LA(1) == 13 && (this.LA(2) == 52 || this.LA(2) == 153 || this.LA(2) == 154))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(13);
					int num2 = this.LA(1);
					if (num2 != 52)
					{
						switch (num2)
						{
						case 153:
						case 154:
							break;
						default:
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
					}
					else
					{
						this.match(52);
					}
					switch (this.LA(1))
					{
					case 153:
						this.match(153);
						break;
					case 154:
						this.match(154);
						break;
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			TSqlStatement tsqlStatement;
			if (flag)
			{
				tsqlStatement = this.beginTransactionStatement();
			}
			else
			{
				if (this.LA(1) != 13 || !TSql80ParserInternal.tokenSet_3_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				tsqlStatement = this.beginEndBlockStatement();
			}
			return tsqlStatement;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0009D894 File Offset: 0x0009BA94
		public BreakStatement breakStatement()
		{
			BreakStatement breakStatement = base.FragmentFactory.CreateFragment<BreakStatement>();
			IToken token = this.LT(1);
			this.match(15);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(breakStatement, token);
			}
			return breakStatement;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0009D8D4 File Offset: 0x0009BAD4
		public ContinueStatement continueStatement()
		{
			ContinueStatement continueStatement = base.FragmentFactory.CreateFragment<ContinueStatement>();
			IToken token = this.LT(1);
			this.match(33);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(continueStatement, token);
			}
			return continueStatement;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0009D914 File Offset: 0x0009BB14
		public IfStatement ifStatement()
		{
			IfStatement ifStatement = base.FragmentFactory.CreateFragment<IfStatement>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(82);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(ifStatement, token);
			}
			BooleanExpression booleanExpression = this.booleanExpression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				ifStatement.Predicate = booleanExpression;
			}
			TSqlStatement tsqlStatement = this.statementOptSemi();
			if (this.inputState.guessing == 0)
			{
				if (tsqlStatement == null)
				{
					flag = true;
				}
				else
				{
					ifStatement.ThenStatement = tsqlStatement;
				}
			}
			if (this.LA(1) == 55 && TSql80ParserInternal.tokenSet_3_.member(this.LA(2)))
			{
				this.match(55);
				tsqlStatement = this.statementOptSemi();
				if (this.inputState.guessing == 0)
				{
					if (tsqlStatement == null)
					{
						flag = true;
					}
					else
					{
						ifStatement.ElseStatement = tsqlStatement;
					}
				}
			}
			else if (!TSql80ParserInternal.tokenSet_10_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_11_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			if (this.inputState.guessing == 0 && flag)
			{
				ifStatement = null;
			}
			return ifStatement;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0009DA30 File Offset: 0x0009BC30
		public WhileStatement whileStatement()
		{
			WhileStatement whileStatement = base.FragmentFactory.CreateFragment<WhileStatement>();
			IToken token = this.LT(1);
			this.match(170);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(whileStatement, token);
			}
			BooleanExpression booleanExpression = this.booleanExpression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				whileStatement.Predicate = booleanExpression;
			}
			TSqlStatement tsqlStatement = this.statementOptSemi();
			if (this.inputState.guessing == 0)
			{
				if (tsqlStatement == null)
				{
					whileStatement = null;
				}
				else
				{
					whileStatement.Statement = tsqlStatement;
				}
			}
			return whileStatement;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0009DAB4 File Offset: 0x0009BCB4
		public LabelStatement labelStatement()
		{
			LabelStatement labelStatement = base.FragmentFactory.CreateFragment<LabelStatement>();
			IToken token = this.LT(1);
			this.match(220);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(labelStatement, token);
				labelStatement.Value = token.getText();
			}
			return labelStatement;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0009DB04 File Offset: 0x0009BD04
		public BackupStatement backupStatement()
		{
			IToken token = this.backupStart();
			BackupStatement backupStatement = this.backupMain();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(backupStatement, token);
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							goto IL_02A2;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_02A2;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								goto IL_02A2;
							default:
								if (num == 28)
								{
									goto IL_02A2;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						goto IL_02A2;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							goto IL_02A2;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						goto IL_02A2;
					default:
						if (num == 82 || num == 86)
						{
							goto IL_02A2;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						goto IL_02A2;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						goto IL_02A2;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_02A2;
					}
				}
			}
			else if (num <= 181)
			{
				if (num == 151)
				{
					this.match(151);
					this.devList(backupStatement, backupStatement.Devices);
					goto IL_02A2;
				}
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					goto IL_02A2;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 171:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						goto IL_02A2;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					goto IL_02A2;
				}
				switch (num)
				{
				case 219:
				case 220:
					goto IL_02A2;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_02A2:
			int num2 = this.LA(1);
			if (num2 <= 86)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return backupStatement;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return backupStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return backupStatement;
							default:
								if (num2 == 28)
								{
									return backupStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 64)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return backupStatement;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return backupStatement;
						}
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 74:
					case 75:
						return backupStatement;
					default:
						if (num2 == 82 || num2 == 86)
						{
							return backupStatement;
						}
						break;
					}
				}
			}
			else if (num2 <= 144)
			{
				if (num2 <= 95)
				{
					if (num2 == 92 || num2 == 95)
					{
						return backupStatement;
					}
				}
				else
				{
					if (num2 == 106 || num2 == 119)
					{
						return backupStatement;
					}
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return backupStatement;
					}
				}
			}
			else if (num2 <= 181)
			{
				switch (num2)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return backupStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num2)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return backupStatement;
					case 171:
						this.backupOptions(backupStatement);
						return backupStatement;
					}
					break;
				}
			}
			else
			{
				if (num2 == 191 || num2 == 204)
				{
					return backupStatement;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return backupStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0009E018 File Offset: 0x0009C218
		public RestoreStatement restoreStatement()
		{
			RestoreStatement restoreStatement = base.FragmentFactory.CreateFragment<RestoreStatement>();
			IToken token = this.restoreStart();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(restoreStatement, token);
			}
			if (TSql80ParserInternal.tokenSet_14_.member(this.LA(1)) && this.LA(2) >= 232 && this.LA(2) <= 234)
			{
				this.restoreMain(restoreStatement);
				int num = this.LA(1);
				if (num <= 86)
				{
					if (num <= 28)
					{
						if (num <= 6)
						{
							if (num == 1 || num == 6)
							{
								goto IL_0363;
							}
						}
						else
						{
							switch (num)
							{
							case 12:
							case 13:
							case 15:
							case 17:
								goto IL_0363;
							case 14:
							case 16:
								break;
							default:
								switch (num)
								{
								case 22:
								case 23:
									goto IL_0363;
								default:
									if (num == 28)
									{
										goto IL_0363;
									}
									break;
								}
								break;
							}
						}
					}
					else if (num <= 64)
					{
						switch (num)
						{
						case 33:
						case 35:
							goto IL_0363;
						case 34:
							break;
						default:
							switch (num)
							{
							case 44:
							case 45:
							case 46:
							case 48:
							case 49:
							case 54:
							case 55:
							case 56:
							case 60:
							case 61:
							case 64:
								goto IL_0363;
							}
							break;
						}
					}
					else
					{
						switch (num)
						{
						case 71:
							this.match(71);
							this.devList(restoreStatement, restoreStatement.Devices);
							goto IL_0363;
						case 72:
						case 73:
							break;
						case 74:
						case 75:
							goto IL_0363;
						default:
							if (num == 82 || num == 86)
							{
								goto IL_0363;
							}
							break;
						}
					}
				}
				else if (num <= 144)
				{
					if (num <= 95)
					{
						if (num == 92 || num == 95)
						{
							goto IL_0363;
						}
					}
					else
					{
						if (num == 106 || num == 119)
						{
							goto IL_0363;
						}
						switch (num)
						{
						case 123:
						case 125:
						case 126:
						case 129:
						case 131:
						case 132:
						case 134:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							goto IL_0363;
						}
					}
				}
				else if (num <= 181)
				{
					switch (num)
					{
					case 156:
					case 160:
					case 161:
					case 162:
						goto IL_0363;
					case 157:
					case 158:
					case 159:
						break;
					default:
						switch (num)
						{
						case 167:
						case 170:
						case 171:
						case 172:
						case 173:
						case 176:
						case 180:
						case 181:
							goto IL_0363;
						}
						break;
					}
				}
				else
				{
					if (num == 191 || num == 204)
					{
						goto IL_0363;
					}
					switch (num)
					{
					case 219:
					case 220:
						goto IL_0363;
					}
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			if (this.LA(1) != 232 || this.LA(2) != 71)
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			IToken token2 = this.LT(1);
			this.match(232);
			this.match(71);
			this.devList(restoreStatement, restoreStatement.Devices);
			if (this.inputState.guessing == 0)
			{
				restoreStatement.Kind = RestoreStatementKindsHelper.Instance.ParseOption(token2);
			}
			IL_0363:
			int num2 = this.LA(1);
			if (num2 <= 86)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return restoreStatement;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return restoreStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return restoreStatement;
							default:
								if (num2 == 28)
								{
									return restoreStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 64)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return restoreStatement;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return restoreStatement;
						}
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 74:
					case 75:
						return restoreStatement;
					default:
						if (num2 == 82 || num2 == 86)
						{
							return restoreStatement;
						}
						break;
					}
				}
			}
			else if (num2 <= 144)
			{
				if (num2 <= 95)
				{
					if (num2 == 92 || num2 == 95)
					{
						return restoreStatement;
					}
				}
				else
				{
					if (num2 == 106 || num2 == 119)
					{
						return restoreStatement;
					}
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return restoreStatement;
					}
				}
			}
			else if (num2 <= 181)
			{
				switch (num2)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return restoreStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num2)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return restoreStatement;
					case 171:
						this.restoreOptions(restoreStatement);
						return restoreStatement;
					}
					break;
				}
			}
			else
			{
				if (num2 == 191 || num2 == 204)
				{
					return restoreStatement;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return restoreStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0009E60C File Offset: 0x0009C80C
		public GoToStatement gotoStatement()
		{
			GoToStatement goToStatement = base.FragmentFactory.CreateFragment<GoToStatement>();
			IToken token = this.LT(1);
			this.match(74);
			Identifier identifier = this.nonQuotedIdentifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(goToStatement, token);
				goToStatement.LabelName = identifier;
			}
			return goToStatement;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0009E65C File Offset: 0x0009C85C
		public SaveTransactionStatement saveTransactionStatement()
		{
			SaveTransactionStatement saveTransactionStatement = base.FragmentFactory.CreateFragment<SaveTransactionStatement>();
			IToken token = this.LT(1);
			this.match(138);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(saveTransactionStatement, token);
			}
			switch (this.LA(1))
			{
			case 153:
				this.match(153);
				break;
			case 154:
				this.match(154);
				break;
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			this.transactionName(saveTransactionStatement);
			return saveTransactionStatement;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0009E6F4 File Offset: 0x0009C8F4
		public RollbackTransactionStatement rollbackTransactionStatement()
		{
			RollbackTransactionStatement rollbackTransactionStatement = base.FragmentFactory.CreateFragment<RollbackTransactionStatement>();
			IToken token = this.LT(1);
			this.match(134);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(rollbackTransactionStatement, token);
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return rollbackTransactionStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return rollbackTransactionStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return rollbackTransactionStatement;
							default:
								if (num == 28)
								{
									return rollbackTransactionStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return rollbackTransactionStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return rollbackTransactionStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return rollbackTransactionStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return rollbackTransactionStatement;
				}
			}
			else if (num <= 173)
			{
				if (num <= 119)
				{
					if (num == 95 || num == 106 || num == 119)
					{
						return rollbackTransactionStatement;
					}
				}
				else
				{
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return rollbackTransactionStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 153:
						case 154:
						{
							switch (this.LA(1))
							{
							case 153:
							{
								IToken token2 = this.LT(1);
								this.match(153);
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.UpdateTokenInfo(rollbackTransactionStatement, token2);
								}
								break;
							}
							case 154:
							{
								IToken token3 = this.LT(1);
								this.match(154);
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.UpdateTokenInfo(rollbackTransactionStatement, token3);
								}
								break;
							}
							default:
								throw new NoViableAltException(this.LT(1), this.getFilename());
							}
							int num2 = this.LA(1);
							if (num2 <= 95)
							{
								if (num2 <= 35)
								{
									if (num2 <= 17)
									{
										if (num2 == 1 || num2 == 6)
										{
											return rollbackTransactionStatement;
										}
										switch (num2)
										{
										case 12:
										case 13:
										case 15:
										case 17:
											return rollbackTransactionStatement;
										}
									}
									else
									{
										switch (num2)
										{
										case 22:
										case 23:
											return rollbackTransactionStatement;
										default:
											if (num2 == 28)
											{
												return rollbackTransactionStatement;
											}
											switch (num2)
											{
											case 33:
											case 35:
												return rollbackTransactionStatement;
											}
											break;
										}
									}
								}
								else if (num2 <= 82)
								{
									switch (num2)
									{
									case 44:
									case 45:
									case 46:
									case 48:
									case 49:
									case 54:
									case 55:
									case 56:
									case 60:
									case 61:
									case 64:
										return rollbackTransactionStatement;
									case 47:
									case 50:
									case 51:
									case 52:
									case 53:
									case 57:
									case 58:
									case 59:
									case 62:
									case 63:
										break;
									default:
										switch (num2)
										{
										case 74:
										case 75:
											return rollbackTransactionStatement;
										default:
											if (num2 == 82)
											{
												return rollbackTransactionStatement;
											}
											break;
										}
										break;
									}
								}
								else if (num2 == 86 || num2 == 92 || num2 == 95)
								{
									return rollbackTransactionStatement;
								}
							}
							else
							{
								if (num2 > 176)
								{
									if (num2 <= 199)
									{
										switch (num2)
										{
										case 180:
										case 181:
											return rollbackTransactionStatement;
										default:
											if (num2 == 191)
											{
												return rollbackTransactionStatement;
											}
											if (num2 != 199)
											{
												goto IL_0612;
											}
											break;
										}
									}
									else
									{
										if (num2 == 204)
										{
											return rollbackTransactionStatement;
										}
										switch (num2)
										{
										case 219:
										case 220:
											return rollbackTransactionStatement;
										case 221:
											break;
										default:
											switch (num2)
											{
											case 232:
											case 233:
											case 234:
												break;
											default:
												goto IL_0612;
											}
											break;
										}
									}
									this.transactionName(rollbackTransactionStatement);
									return rollbackTransactionStatement;
								}
								if (num2 <= 144)
								{
									if (num2 == 106 || num2 == 119)
									{
										return rollbackTransactionStatement;
									}
									switch (num2)
									{
									case 123:
									case 125:
									case 126:
									case 129:
									case 131:
									case 132:
									case 134:
									case 138:
									case 140:
									case 142:
									case 143:
									case 144:
										return rollbackTransactionStatement;
									}
								}
								else
								{
									switch (num2)
									{
									case 156:
									case 160:
									case 161:
									case 162:
										return rollbackTransactionStatement;
									case 157:
									case 158:
									case 159:
										break;
									default:
										switch (num2)
										{
										case 167:
										case 170:
										case 172:
										case 173:
											return rollbackTransactionStatement;
										case 168:
										case 169:
										case 171:
											break;
										default:
											if (num2 == 176)
											{
												return rollbackTransactionStatement;
											}
											break;
										}
										break;
									}
								}
							}
							IL_0612:
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						case 155:
						case 157:
						case 158:
						case 159:
							break;
						case 156:
						case 160:
						case 161:
						case 162:
							return rollbackTransactionStatement;
						default:
							switch (num)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								return rollbackTransactionStatement;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 191)
			{
				if (num == 176)
				{
					return rollbackTransactionStatement;
				}
				switch (num)
				{
				case 180:
				case 181:
					return rollbackTransactionStatement;
				default:
					if (num == 191)
					{
						return rollbackTransactionStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 204)
				{
					return rollbackTransactionStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return rollbackTransactionStatement;
				default:
					if (num == 232)
					{
						IToken token4 = this.LT(1);
						this.match(232);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.Match(token4, "WORK");
							return rollbackTransactionStatement;
						}
						return rollbackTransactionStatement;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0009ED3C File Offset: 0x0009CF3C
		public CommitTransactionStatement commitTransactionStatement()
		{
			CommitTransactionStatement commitTransactionStatement = base.FragmentFactory.CreateFragment<CommitTransactionStatement>();
			IToken token = this.LT(1);
			this.match(28);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(commitTransactionStatement, token);
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return commitTransactionStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return commitTransactionStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return commitTransactionStatement;
							default:
								if (num == 28)
								{
									return commitTransactionStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return commitTransactionStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return commitTransactionStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return commitTransactionStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return commitTransactionStatement;
				}
			}
			else if (num <= 173)
			{
				if (num <= 119)
				{
					if (num == 95 || num == 106 || num == 119)
					{
						return commitTransactionStatement;
					}
				}
				else
				{
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return commitTransactionStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 153:
						case 154:
						{
							switch (this.LA(1))
							{
							case 153:
							{
								IToken token2 = this.LT(1);
								this.match(153);
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.UpdateTokenInfo(commitTransactionStatement, token2);
								}
								break;
							}
							case 154:
							{
								IToken token3 = this.LT(1);
								this.match(154);
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.UpdateTokenInfo(commitTransactionStatement, token3);
								}
								break;
							}
							default:
								throw new NoViableAltException(this.LT(1), this.getFilename());
							}
							int num2 = this.LA(1);
							if (num2 <= 95)
							{
								if (num2 <= 35)
								{
									if (num2 <= 17)
									{
										if (num2 == 1 || num2 == 6)
										{
											return commitTransactionStatement;
										}
										switch (num2)
										{
										case 12:
										case 13:
										case 15:
										case 17:
											return commitTransactionStatement;
										}
									}
									else
									{
										switch (num2)
										{
										case 22:
										case 23:
											return commitTransactionStatement;
										default:
											if (num2 == 28)
											{
												return commitTransactionStatement;
											}
											switch (num2)
											{
											case 33:
											case 35:
												return commitTransactionStatement;
											}
											break;
										}
									}
								}
								else if (num2 <= 82)
								{
									switch (num2)
									{
									case 44:
									case 45:
									case 46:
									case 48:
									case 49:
									case 54:
									case 55:
									case 56:
									case 60:
									case 61:
									case 64:
										return commitTransactionStatement;
									case 47:
									case 50:
									case 51:
									case 52:
									case 53:
									case 57:
									case 58:
									case 59:
									case 62:
									case 63:
										break;
									default:
										switch (num2)
										{
										case 74:
										case 75:
											return commitTransactionStatement;
										default:
											if (num2 == 82)
											{
												return commitTransactionStatement;
											}
											break;
										}
										break;
									}
								}
								else if (num2 == 86 || num2 == 92 || num2 == 95)
								{
									return commitTransactionStatement;
								}
							}
							else
							{
								if (num2 > 176)
								{
									if (num2 <= 199)
									{
										switch (num2)
										{
										case 180:
										case 181:
											return commitTransactionStatement;
										default:
											if (num2 == 191)
											{
												return commitTransactionStatement;
											}
											if (num2 != 199)
											{
												goto IL_060F;
											}
											break;
										}
									}
									else
									{
										if (num2 == 204)
										{
											return commitTransactionStatement;
										}
										switch (num2)
										{
										case 219:
										case 220:
											return commitTransactionStatement;
										case 221:
											break;
										default:
											switch (num2)
											{
											case 232:
											case 233:
											case 234:
												break;
											default:
												goto IL_060F;
											}
											break;
										}
									}
									this.transactionName(commitTransactionStatement);
									return commitTransactionStatement;
								}
								if (num2 <= 144)
								{
									if (num2 == 106 || num2 == 119)
									{
										return commitTransactionStatement;
									}
									switch (num2)
									{
									case 123:
									case 125:
									case 126:
									case 129:
									case 131:
									case 132:
									case 134:
									case 138:
									case 140:
									case 142:
									case 143:
									case 144:
										return commitTransactionStatement;
									}
								}
								else
								{
									switch (num2)
									{
									case 156:
									case 160:
									case 161:
									case 162:
										return commitTransactionStatement;
									case 157:
									case 158:
									case 159:
										break;
									default:
										switch (num2)
										{
										case 167:
										case 170:
										case 172:
										case 173:
											return commitTransactionStatement;
										case 168:
										case 169:
										case 171:
											break;
										default:
											if (num2 == 176)
											{
												return commitTransactionStatement;
											}
											break;
										}
										break;
									}
								}
							}
							IL_060F:
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						case 155:
						case 157:
						case 158:
						case 159:
							break;
						case 156:
						case 160:
						case 161:
						case 162:
							return commitTransactionStatement;
						default:
							switch (num)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								return commitTransactionStatement;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 191)
			{
				if (num == 176)
				{
					return commitTransactionStatement;
				}
				switch (num)
				{
				case 180:
				case 181:
					return commitTransactionStatement;
				default:
					if (num == 191)
					{
						return commitTransactionStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 204)
				{
					return commitTransactionStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return commitTransactionStatement;
				default:
					if (num == 232)
					{
						IToken token4 = this.LT(1);
						this.match(232);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.Match(token4, "WORK");
							return commitTransactionStatement;
						}
						return commitTransactionStatement;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0009F380 File Offset: 0x0009D580
		public CreateStatisticsStatement createStatisticsStatement()
		{
			CreateStatisticsStatement createStatisticsStatement = base.FragmentFactory.CreateFragment<CreateStatisticsStatement>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(35);
			IToken token2 = this.LT(1);
			this.match(146);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createStatisticsStatement, token);
				TSql80ParserBaseInternal.UpdateTokenInfo(createStatisticsStatement, token2);
				createStatisticsStatement.Name = identifier;
			}
			this.match(105);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				createStatisticsStatement.OnName = schemaObjectName;
				base.ThrowPartialAstIfPhaseOne(createStatisticsStatement);
			}
			this.identifierColumnList(createStatisticsStatement, createStatisticsStatement.Columns);
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return createStatisticsStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return createStatisticsStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return createStatisticsStatement;
							default:
								if (num == 28)
								{
									return createStatisticsStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return createStatisticsStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return createStatisticsStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return createStatisticsStatement;
					default:
						if (num == 82 || num == 86)
						{
							return createStatisticsStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return createStatisticsStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return createStatisticsStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return createStatisticsStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return createStatisticsStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return createStatisticsStatement;
					case 171:
					{
						this.match(171);
						StatisticsOption statisticsOption = this.createStatisticsStatementWithOption(ref flag);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<StatisticsOption>(createStatisticsStatement, createStatisticsStatement.StatisticsOptions, statisticsOption);
						}
						while (this.LA(1) == 198)
						{
							this.match(198);
							statisticsOption = this.createStatisticsStatementWithOption(ref flag);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.AddAndUpdateTokenInfo<StatisticsOption>(createStatisticsStatement, createStatisticsStatement.StatisticsOptions, statisticsOption);
							}
						}
						return createStatisticsStatement;
					}
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return createStatisticsStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return createStatisticsStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0009F718 File Offset: 0x0009D918
		public UpdateStatisticsStatement updateStatisticsStatement()
		{
			UpdateStatisticsStatement updateStatisticsStatement = base.FragmentFactory.CreateFragment<UpdateStatisticsStatement>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(160);
			IToken token2 = this.LT(1);
			this.match(146);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(updateStatisticsStatement, token);
				TSql80ParserBaseInternal.UpdateTokenInfo(updateStatisticsStatement, token2);
			}
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				updateStatisticsStatement.SchemaObjectName = schemaObjectName;
			}
			bool flag2 = false;
			if (this.LA(1) == 191 && (this.LA(2) == 232 || this.LA(2) == 233))
			{
				int num = this.mark();
				flag2 = true;
				this.inputState.guessing++;
				try
				{
					this.match(191);
					this.identifier();
				}
				catch (RecognitionException)
				{
					flag2 = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag2)
			{
				this.columnNameList(updateStatisticsStatement, updateStatisticsStatement.SubElements);
			}
			else if (this.LA(1) == 232 || this.LA(1) == 233)
			{
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(updateStatisticsStatement, updateStatisticsStatement.SubElements, identifier);
				}
			}
			else if (!TSql80ParserInternal.tokenSet_15_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_11_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			int num2 = this.LA(1);
			if (num2 <= 86)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return updateStatisticsStatement;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return updateStatisticsStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return updateStatisticsStatement;
							default:
								if (num2 == 28)
								{
									return updateStatisticsStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 64)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return updateStatisticsStatement;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return updateStatisticsStatement;
						}
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 74:
					case 75:
						return updateStatisticsStatement;
					default:
						if (num2 == 82 || num2 == 86)
						{
							return updateStatisticsStatement;
						}
						break;
					}
				}
			}
			else if (num2 <= 144)
			{
				if (num2 <= 95)
				{
					if (num2 == 92 || num2 == 95)
					{
						return updateStatisticsStatement;
					}
				}
				else
				{
					if (num2 == 106 || num2 == 119)
					{
						return updateStatisticsStatement;
					}
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return updateStatisticsStatement;
					}
				}
			}
			else if (num2 <= 181)
			{
				switch (num2)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return updateStatisticsStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num2)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return updateStatisticsStatement;
					case 171:
					{
						this.match(171);
						StatisticsOption statisticsOption = this.updateStatisticsStatementWithOption(ref flag);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<StatisticsOption>(updateStatisticsStatement, updateStatisticsStatement.StatisticsOptions, statisticsOption);
						}
						while (this.LA(1) == 198)
						{
							this.match(198);
							statisticsOption = this.updateStatisticsStatementWithOption(ref flag);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.AddAndUpdateTokenInfo<StatisticsOption>(updateStatisticsStatement, updateStatisticsStatement.StatisticsOptions, statisticsOption);
							}
						}
						return updateStatisticsStatement;
					}
					}
					break;
				}
			}
			else
			{
				if (num2 == 191 || num2 == 204)
				{
					return updateStatisticsStatement;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return updateStatisticsStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0009FBA4 File Offset: 0x0009DDA4
		public AlterDatabaseStatement alterDatabaseStatements()
		{
			AlterDatabaseStatement alterDatabaseStatement = null;
			IToken token = null;
			Identifier identifier = null;
			try
			{
				token = this.LT(1);
				this.match(6);
				this.match(43);
				int num = this.LA(1);
				if (num != 226)
				{
					switch (num)
					{
					case 232:
					case 233:
						identifier = this.identifier();
						break;
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					identifier = this.sqlCommandIdentifier();
				}
				int num2 = this.LA(1);
				if (num2 != 4)
				{
					if (num2 != 26)
					{
						if (num2 != 142)
						{
							if (this.LA(1) == 232 && (this.LA(2) == 65 || this.LA(2) == 232) && base.NextTokenMatches("REMOVE"))
							{
								alterDatabaseStatement = this.alterDbRemove();
							}
							else
							{
								if (this.LA(1) != 232 || (this.LA(2) != 65 && this.LA(2) != 232) || !base.NextTokenMatches("MODIFY"))
								{
									throw new NoViableAltException(this.LT(1), this.getFilename());
								}
								alterDatabaseStatement = this.alterDbModify();
							}
						}
						else
						{
							alterDatabaseStatement = this.alterDbSet();
						}
					}
					else
					{
						alterDatabaseStatement = this.alterDbCollate();
					}
				}
				else
				{
					alterDatabaseStatement = this.alterDbAdd();
				}
				if (this.inputState.guessing == 0)
				{
					alterDatabaseStatement.DatabaseName = identifier;
					TSql80ParserBaseInternal.UpdateTokenInfo(alterDatabaseStatement, token);
					base.ThrowPartialAstIfPhaseOne(alterDatabaseStatement);
				}
			}
			catch (PhaseOnePartialAstException ex)
			{
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(ex.Statement, token);
					(ex.Statement as AlterDatabaseStatement).DatabaseName = identifier;
					throw;
				}
				throw;
			}
			return alterDatabaseStatement;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0009FD5C File Offset: 0x0009DF5C
		public ExecuteStatement executeStatement()
		{
			ExecuteStatement executeStatement = base.FragmentFactory.CreateFragment<ExecuteStatement>();
			ExecuteSpecification executeSpecification = this.executeSpecification();
			if (this.inputState.guessing == 0)
			{
				executeStatement.ExecuteSpecification = executeSpecification;
				base.ThrowPartialAstIfPhaseOne(executeStatement);
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return executeStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return executeStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return executeStatement;
							default:
								if (num == 28)
								{
									return executeStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return executeStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return executeStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return executeStatement;
					default:
						if (num == 82 || num == 86)
						{
							return executeStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return executeStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return executeStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return executeStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return executeStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return executeStatement;
					case 171:
					{
						this.match(171);
						ExecuteOption executeOption = this.executeOption();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ExecuteOption>(executeStatement, executeStatement.Options, executeOption);
							return executeStatement;
						}
						return executeStatement;
					}
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return executeStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return executeStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000A0024 File Offset: 0x0009E224
		public SelectStatement select()
		{
			SelectStatement selectStatement = base.FragmentFactory.CreateFragment<SelectStatement>();
			QueryExpression queryExpression = this.queryExpression(selectStatement);
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 35)
				{
					if (num <= 17)
					{
						if (num != 1 && num != 6)
						{
							switch (num)
							{
							case 12:
							case 13:
							case 15:
							case 17:
								break;
							case 14:
							case 16:
								goto IL_02BC;
							default:
								goto IL_02BC;
							}
						}
					}
					else
					{
						switch (num)
						{
						case 22:
						case 23:
							break;
						default:
							switch (num)
							{
							case 28:
							case 29:
								break;
							default:
								switch (num)
								{
								case 33:
								case 35:
									break;
								case 34:
									goto IL_02BC;
								default:
									goto IL_02BC;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
						break;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
						goto IL_02BC;
					default:
						if (num != 67)
						{
							switch (num)
							{
							case 74:
							case 75:
								break;
							default:
								goto IL_02BC;
							}
						}
						break;
					}
				}
				else if (num != 82 && num != 86 && num != 92)
				{
					goto IL_02BC;
				}
			}
			else if (num <= 162)
			{
				if (num <= 113)
				{
					if (num != 95 && num != 106)
					{
						switch (num)
						{
						case 111:
							break;
						case 112:
							goto IL_02BC;
						case 113:
						{
							OrderByClause orderByClause = this.orderByClause();
							if (this.inputState.guessing == 0)
							{
								queryExpression.OrderByClause = orderByClause;
							}
							break;
						}
						default:
							goto IL_02BC;
						}
					}
				}
				else if (num != 119)
				{
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						break;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						goto IL_02BC;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							break;
						case 157:
						case 158:
						case 159:
							goto IL_02BC;
						default:
							goto IL_02BC;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					break;
				case 168:
				case 169:
				case 171:
					goto IL_02BC;
				default:
					if (num != 176)
					{
						switch (num)
						{
						case 180:
						case 181:
							break;
						default:
							goto IL_02BC;
						}
					}
					break;
				}
			}
			else if (num != 191 && num != 204)
			{
				switch (num)
				{
				case 219:
				case 220:
					break;
				default:
					goto IL_02BC;
				}
			}
			while (this.LA(1) == 29)
			{
				ComputeClause computeClause = this.computeClause();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ComputeClause>(selectStatement, selectStatement.ComputeClauses, computeClause);
				}
			}
			int num2 = this.LA(1);
			if (num2 <= 92)
			{
				if (num2 <= 35)
				{
					if (num2 <= 17)
					{
						if (num2 == 1 || num2 == 6)
						{
							goto IL_05A4;
						}
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_05A4;
						}
					}
					else
					{
						switch (num2)
						{
						case 22:
						case 23:
							goto IL_05A4;
						default:
							if (num2 == 28)
							{
								goto IL_05A4;
							}
							switch (num2)
							{
							case 33:
							case 35:
								goto IL_05A4;
							}
							break;
						}
					}
				}
				else if (num2 <= 75)
				{
					switch (num2)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
						goto IL_05A4;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
						break;
					default:
						if (num2 != 67)
						{
							switch (num2)
							{
							case 74:
							case 75:
								goto IL_05A4;
							}
						}
						else
						{
							ForClause forClause = this.forClause();
							if (this.inputState.guessing == 0)
							{
								queryExpression.ForClause = forClause;
								goto IL_05A4;
							}
							goto IL_05A4;
						}
						break;
					}
				}
				else if (num2 == 82 || num2 == 86 || num2 == 92)
				{
					goto IL_05A4;
				}
			}
			else if (num2 <= 162)
			{
				if (num2 <= 111)
				{
					if (num2 == 95 || num2 == 106 || num2 == 111)
					{
						goto IL_05A4;
					}
				}
				else
				{
					if (num2 == 119)
					{
						goto IL_05A4;
					}
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_05A4;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num2)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							goto IL_05A4;
						}
						break;
					}
				}
			}
			else if (num2 <= 181)
			{
				switch (num2)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					goto IL_05A4;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num2 == 176)
					{
						goto IL_05A4;
					}
					switch (num2)
					{
					case 180:
					case 181:
						goto IL_05A4;
					}
					break;
				}
			}
			else
			{
				if (num2 == 191 || num2 == 204)
				{
					goto IL_05A4;
				}
				switch (num2)
				{
				case 219:
				case 220:
					goto IL_05A4;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_05A4:
			int num3 = this.LA(1);
			if (num3 <= 92)
			{
				if (num3 <= 28)
				{
					if (num3 <= 6)
					{
						if (num3 == 1 || num3 == 6)
						{
							goto IL_0834;
						}
					}
					else
					{
						switch (num3)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_0834;
						case 14:
						case 16:
							break;
						default:
							switch (num3)
							{
							case 22:
							case 23:
								goto IL_0834;
							default:
								if (num3 == 28)
								{
									goto IL_0834;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num3 <= 75)
				{
					switch (num3)
					{
					case 33:
					case 35:
						goto IL_0834;
					case 34:
						break;
					default:
						switch (num3)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							goto IL_0834;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num3)
							{
							case 74:
							case 75:
								goto IL_0834;
							}
							break;
						}
						break;
					}
				}
				else if (num3 == 82 || num3 == 86 || num3 == 92)
				{
					goto IL_0834;
				}
			}
			else if (num3 <= 162)
			{
				if (num3 <= 111)
				{
					if (num3 == 95 || num3 == 106)
					{
						goto IL_0834;
					}
					if (num3 == 111)
					{
						this.optimizerHints(selectStatement, selectStatement.OptimizerHints);
						goto IL_0834;
					}
				}
				else
				{
					if (num3 == 119)
					{
						goto IL_0834;
					}
					switch (num3)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_0834;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num3)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							goto IL_0834;
						}
						break;
					}
				}
			}
			else if (num3 <= 181)
			{
				switch (num3)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					goto IL_0834;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num3 == 176)
					{
						goto IL_0834;
					}
					switch (num3)
					{
					case 180:
					case 181:
						goto IL_0834;
					}
					break;
				}
			}
			else
			{
				if (num3 == 191 || num3 == 204)
				{
					goto IL_0834;
				}
				switch (num3)
				{
				case 219:
				case 220:
					goto IL_0834;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0834:
			if (this.inputState.guessing == 0)
			{
				selectStatement.QueryExpression = queryExpression;
			}
			return selectStatement;
			IL_02BC:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000A087C File Offset: 0x0009EA7C
		public DeleteStatement deleteStatement()
		{
			DeleteStatement deleteStatement = base.FragmentFactory.CreateFragment<DeleteStatement>();
			DeleteSpecification deleteSpecification = this.deleteSpecification();
			if (this.inputState.guessing == 0)
			{
				deleteStatement.DeleteSpecification = deleteSpecification;
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return deleteStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return deleteStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return deleteStatement;
							default:
								if (num == 28)
								{
									return deleteStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return deleteStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return deleteStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return deleteStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return deleteStatement;
				}
			}
			else if (num <= 162)
			{
				if (num <= 111)
				{
					if (num == 95 || num == 106)
					{
						return deleteStatement;
					}
					if (num == 111)
					{
						this.optimizerHints(deleteStatement, deleteStatement.OptimizerHints);
						return deleteStatement;
					}
				}
				else
				{
					if (num == 119)
					{
						return deleteStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return deleteStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return deleteStatement;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return deleteStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						return deleteStatement;
					}
					switch (num)
					{
					case 180:
					case 181:
						return deleteStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return deleteStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return deleteStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000A0B24 File Offset: 0x0009ED24
		public InsertStatement insertStatement()
		{
			InsertStatement insertStatement = base.FragmentFactory.CreateFragment<InsertStatement>();
			InsertSpecification insertSpecification = this.insertSpecification();
			if (this.inputState.guessing == 0)
			{
				insertStatement.InsertSpecification = insertSpecification;
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return insertStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return insertStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return insertStatement;
							default:
								if (num == 28)
								{
									return insertStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return insertStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return insertStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return insertStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return insertStatement;
				}
			}
			else if (num <= 162)
			{
				if (num <= 111)
				{
					if (num == 95 || num == 106)
					{
						return insertStatement;
					}
					if (num == 111)
					{
						this.optimizerHints(insertStatement, insertStatement.OptimizerHints);
						return insertStatement;
					}
				}
				else
				{
					if (num == 119)
					{
						return insertStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return insertStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return insertStatement;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return insertStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						return insertStatement;
					}
					switch (num)
					{
					case 180:
					case 181:
						return insertStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return insertStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return insertStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000A0DCC File Offset: 0x0009EFCC
		public UpdateStatement updateStatement()
		{
			UpdateStatement updateStatement = base.FragmentFactory.CreateFragment<UpdateStatement>();
			UpdateSpecification updateSpecification = this.updateSpecification();
			if (this.inputState.guessing == 0)
			{
				updateStatement.UpdateSpecification = updateSpecification;
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return updateStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return updateStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return updateStatement;
							default:
								if (num == 28)
								{
									return updateStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return updateStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return updateStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return updateStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return updateStatement;
				}
			}
			else if (num <= 162)
			{
				if (num <= 111)
				{
					if (num == 95 || num == 106)
					{
						return updateStatement;
					}
					if (num == 111)
					{
						this.optimizerHints(updateStatement, updateStatement.OptimizerHints);
						return updateStatement;
					}
				}
				else
				{
					if (num == 119)
					{
						return updateStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return updateStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return updateStatement;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return updateStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						return updateStatement;
					}
					switch (num)
					{
					case 180:
					case 181:
						return updateStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return updateStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return updateStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000A1074 File Offset: 0x0009F274
		public TSqlStatement raiseErrorStatements()
		{
			IToken token = this.LT(1);
			this.match(123);
			int num = this.LA(1);
			TSqlStatement tsqlStatement;
			if (num <= 199)
			{
				if (num == 191)
				{
					tsqlStatement = this.raiseErrorStatement();
					goto IL_006D;
				}
				if (num != 199)
				{
					goto IL_005A;
				}
			}
			else if (num != 221 && num != 234)
			{
				goto IL_005A;
			}
			tsqlStatement = this.raiseErrorLegacyStatement();
			goto IL_006D;
			IL_005A:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_006D:
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(tsqlStatement, token);
			}
			return tsqlStatement;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000A1104 File Offset: 0x0009F304
		public CreateDatabaseStatement createDatabaseStatement()
		{
			CreateDatabaseStatement createDatabaseStatement = base.FragmentFactory.CreateFragment<CreateDatabaseStatement>();
			IToken token = this.LT(1);
			this.match(35);
			this.match(43);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				createDatabaseStatement.DatabaseName = identifier;
				TSql80ParserBaseInternal.UpdateTokenInfo(createDatabaseStatement, token);
				base.ThrowPartialAstIfPhaseOne(createDatabaseStatement);
			}
			this.recoveryUnitList(createDatabaseStatement);
			this.collationOpt(createDatabaseStatement);
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return createDatabaseStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return createDatabaseStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return createDatabaseStatement;
							default:
								if (num == 28)
								{
									return createDatabaseStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 67)
				{
					switch (num)
					{
					case 33:
					case 35:
						return createDatabaseStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return createDatabaseStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							if (num == 67)
							{
								this.dbAddendums(createDatabaseStatement);
								return createDatabaseStatement;
							}
							break;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return createDatabaseStatement;
					default:
						if (num == 82 || num == 86)
						{
							return createDatabaseStatement;
						}
						break;
					}
				}
			}
			else if (num <= 162)
			{
				if (num <= 106)
				{
					if (num == 92 || num == 95 || num == 106)
					{
						return createDatabaseStatement;
					}
				}
				else
				{
					if (num == 119)
					{
						return createDatabaseStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return createDatabaseStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return createDatabaseStatement;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return createDatabaseStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						return createDatabaseStatement;
					}
					switch (num)
					{
					case 180:
					case 181:
						return createDatabaseStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return createDatabaseStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return createDatabaseStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x000A13D4 File Offset: 0x0009F5D4
		public PrintStatement printStatement()
		{
			PrintStatement printStatement = base.FragmentFactory.CreateFragment<PrintStatement>();
			IToken token = this.LT(1);
			this.match(119);
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(printStatement, token);
				printStatement.Expression = scalarExpression;
			}
			return printStatement;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000A1424 File Offset: 0x0009F624
		public WaitForStatement waitForStatement()
		{
			WaitForStatement waitForStatement = base.FragmentFactory.CreateFragment<WaitForStatement>();
			IToken token = this.LT(1);
			this.match(167);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(waitForStatement, token);
			}
			IToken token2 = this.LT(1);
			this.match(232);
			ValueExpression valueExpression = this.stringOrVariable();
			if (this.inputState.guessing == 0)
			{
				waitForStatement.WaitForOption = WaitForOptionHelper.Instance.ParseOption(token2);
				waitForStatement.Parameter = valueExpression;
			}
			return waitForStatement;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x000A14A8 File Offset: 0x0009F6A8
		public ReadTextStatement readTextStatement()
		{
			ReadTextStatement readTextStatement = base.FragmentFactory.CreateFragment<ReadTextStatement>();
			IToken token = this.LT(1);
			this.match(125);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(readTextStatement, token);
			}
			ColumnReferenceExpression columnReferenceExpression = this.column();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckTableNameExistsForColumn(columnReferenceExpression, true);
				readTextStatement.Column = columnReferenceExpression;
			}
			ValueExpression valueExpression = this.binaryOrVariable();
			if (this.inputState.guessing == 0)
			{
				readTextStatement.TextPointer = valueExpression;
			}
			valueExpression = this.integerOrVariable();
			if (this.inputState.guessing == 0)
			{
				readTextStatement.Offset = valueExpression;
			}
			valueExpression = this.integerOrVariable();
			if (this.inputState.guessing == 0)
			{
				readTextStatement.Size = valueExpression;
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return readTextStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return readTextStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return readTextStatement;
							default:
								if (num == 28)
								{
									return readTextStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 78)
				{
					switch (num)
					{
					case 33:
					case 35:
						return readTextStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return readTextStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return readTextStatement;
							case 78:
							{
								IToken token2 = this.LT(1);
								this.match(78);
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.UpdateTokenInfo(readTextStatement, token2);
									readTextStatement.HoldLock = true;
									return readTextStatement;
								}
								return readTextStatement;
							}
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return readTextStatement;
				}
			}
			else if (num <= 162)
			{
				if (num <= 106)
				{
					if (num == 95 || num == 106)
					{
						return readTextStatement;
					}
				}
				else
				{
					if (num == 119)
					{
						return readTextStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return readTextStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return readTextStatement;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return readTextStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						return readTextStatement;
					}
					switch (num)
					{
					case 180:
					case 181:
						return readTextStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return readTextStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return readTextStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x000A1818 File Offset: 0x0009FA18
		public UpdateTextStatement updateTextStatement()
		{
			UpdateTextStatement updateTextStatement = base.FragmentFactory.CreateFragment<UpdateTextStatement>();
			IToken token = this.LT(1);
			this.match(161);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(updateTextStatement, token);
			}
			this.modificationTextStatement(updateTextStatement);
			ScalarExpression scalarExpression = this.signedIntegerOrVariableOrNull();
			if (this.inputState.guessing == 0)
			{
				updateTextStatement.InsertOffset = scalarExpression;
			}
			scalarExpression = this.signedIntegerOrVariableOrNull();
			if (this.inputState.guessing == 0)
			{
				updateTextStatement.DeleteLength = scalarExpression;
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							goto IL_0350;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_0350;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								goto IL_0350;
							default:
								if (num == 28)
								{
									goto IL_0350;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						goto IL_0350;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							goto IL_0350;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						goto IL_0350;
					default:
						switch (num)
						{
						case 81:
						case 82:
							goto IL_0350;
						default:
							if (num == 86)
							{
								goto IL_0350;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						goto IL_0350;
					}
				}
				else
				{
					if (num == 100 || num == 106)
					{
						goto IL_0350;
					}
					switch (num)
					{
					case 119:
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 136:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_0350;
					}
				}
			}
			else if (num <= 191)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					goto IL_0350;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						goto IL_0350;
					case 168:
					case 169:
					case 174:
					case 175:
					case 177:
					case 178:
					case 179:
						break;
					case 171:
						this.modificationTextStatementWithLog(updateTextStatement);
						goto IL_0350;
					default:
						if (num == 191)
						{
							goto IL_0350;
						}
						break;
					}
					break;
				}
			}
			else
			{
				if (num == 200 || num == 204)
				{
					goto IL_0350;
				}
				switch (num)
				{
				case 219:
				case 220:
				case 224:
				case 227:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
					goto IL_0350;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0350:
			int num2 = this.LA(1);
			ValueExpression valueExpression;
			if (num2 <= 92)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 != 1 && num2 != 6)
						{
							goto IL_0677;
						}
						return updateTextStatement;
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return updateTextStatement;
						case 14:
						case 16:
							goto IL_0677;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return updateTextStatement;
							default:
								if (num2 != 28)
								{
									goto IL_0677;
								}
								return updateTextStatement;
							}
							break;
						}
					}
				}
				else if (num2 <= 75)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return updateTextStatement;
					case 34:
						goto IL_0677;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return updateTextStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							goto IL_0677;
						default:
							switch (num2)
							{
							case 74:
							case 75:
								return updateTextStatement;
							default:
								goto IL_0677;
							}
							break;
						}
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 81:
						break;
					case 82:
						return updateTextStatement;
					default:
						if (num2 != 86 && num2 != 92)
						{
							goto IL_0677;
						}
						return updateTextStatement;
					}
				}
			}
			else
			{
				if (num2 <= 173)
				{
					if (num2 <= 106)
					{
						if (num2 == 95)
						{
							return updateTextStatement;
						}
						if (num2 != 100)
						{
							if (num2 != 106)
							{
								goto IL_0677;
							}
							return updateTextStatement;
						}
					}
					else
					{
						switch (num2)
						{
						case 119:
						case 123:
						case 125:
						case 126:
						case 129:
						case 131:
						case 132:
						case 134:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							return updateTextStatement;
						case 120:
						case 121:
						case 122:
						case 124:
						case 127:
						case 128:
						case 130:
						case 133:
						case 135:
						case 137:
						case 139:
						case 141:
							goto IL_0677;
						case 136:
							goto IL_061B;
						default:
							switch (num2)
							{
							case 156:
							case 160:
							case 161:
							case 162:
								return updateTextStatement;
							case 157:
							case 158:
							case 159:
								goto IL_0677;
							default:
								switch (num2)
								{
								case 167:
								case 170:
								case 172:
								case 173:
									return updateTextStatement;
								case 168:
								case 169:
								case 171:
									goto IL_0677;
								default:
									goto IL_0677;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 191)
				{
					if (num2 == 176)
					{
						return updateTextStatement;
					}
					switch (num2)
					{
					case 180:
					case 181:
						return updateTextStatement;
					default:
						if (num2 != 191)
						{
							goto IL_0677;
						}
						return updateTextStatement;
					}
				}
				else
				{
					if (num2 == 200)
					{
						goto IL_061B;
					}
					if (num2 == 204)
					{
						return updateTextStatement;
					}
					switch (num2)
					{
					case 219:
					case 220:
						return updateTextStatement;
					case 221:
					case 222:
					case 223:
					case 225:
					case 226:
					case 228:
					case 229:
						goto IL_0677;
					case 224:
					case 230:
					case 231:
					case 234:
						break;
					case 227:
					case 232:
					case 233:
						goto IL_061B;
					default:
						goto IL_0677;
					}
				}
				valueExpression = this.writeString();
				if (this.inputState.guessing == 0)
				{
					updateTextStatement.SourceParameter = valueExpression;
					return updateTextStatement;
				}
				return updateTextStatement;
			}
			IL_061B:
			ColumnReferenceExpression columnReferenceExpression = this.column();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckTableNameExistsForColumn(columnReferenceExpression, true);
				updateTextStatement.SourceColumn = columnReferenceExpression;
			}
			valueExpression = this.binaryOrVariable();
			if (this.inputState.guessing == 0)
			{
				updateTextStatement.SourceParameter = valueExpression;
				return updateTextStatement;
			}
			return updateTextStatement;
			IL_0677:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x000A1EB0 File Offset: 0x000A00B0
		public WriteTextStatement writeTextStatement()
		{
			WriteTextStatement writeTextStatement = base.FragmentFactory.CreateFragment<WriteTextStatement>();
			IToken token = this.LT(1);
			this.match(172);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(writeTextStatement, token);
			}
			this.modificationTextStatement(writeTextStatement);
			int num = this.LA(1);
			if (num <= 171)
			{
				if (num == 100)
				{
					goto IL_00A1;
				}
				if (num == 171)
				{
					this.modificationTextStatementWithLog(writeTextStatement);
					goto IL_00A1;
				}
			}
			else
			{
				if (num == 224)
				{
					goto IL_00A1;
				}
				switch (num)
				{
				case 230:
				case 231:
				case 234:
					goto IL_00A1;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_00A1:
			ValueExpression valueExpression = this.writeString();
			if (this.inputState.guessing == 0)
			{
				writeTextStatement.SourceParameter = valueExpression;
			}
			return writeTextStatement;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x000A1F7C File Offset: 0x000A017C
		public LineNoStatement lineNoStatement()
		{
			LineNoStatement lineNoStatement = base.FragmentFactory.CreateFragment<LineNoStatement>();
			IToken token = this.LT(1);
			this.match(95);
			IntegerLiteral integerLiteral = this.integer();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(lineNoStatement, token);
				lineNoStatement.LineNo = integerLiteral;
			}
			return lineNoStatement;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000A1FCC File Offset: 0x000A01CC
		public UseStatement useStatement()
		{
			UseStatement useStatement = base.FragmentFactory.CreateFragment<UseStatement>();
			IToken token = this.LT(1);
			this.match(162);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(useStatement, token);
				useStatement.DatabaseName = identifier;
			}
			return useStatement;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000A2020 File Offset: 0x000A0220
		public KillStatement killStatement()
		{
			KillStatement killStatement = base.FragmentFactory.CreateFragment<KillStatement>();
			IToken token = this.LT(1);
			this.match(92);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(killStatement, token);
			}
			int num = this.LA(1);
			ScalarExpression scalarExpression;
			if (num != 199 && num != 221)
			{
				switch (num)
				{
				case 230:
				case 231:
					scalarExpression = this.stringLiteral();
					break;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				scalarExpression = this.signedInteger();
			}
			if (this.inputState.guessing == 0)
			{
				killStatement.Parameter = scalarExpression;
			}
			int num2 = this.LA(1);
			if (num2 <= 86)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return killStatement;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return killStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return killStatement;
							default:
								if (num2 == 28)
								{
									return killStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 64)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return killStatement;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return killStatement;
						}
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 74:
					case 75:
						return killStatement;
					default:
						if (num2 == 82 || num2 == 86)
						{
							return killStatement;
						}
						break;
					}
				}
			}
			else if (num2 <= 144)
			{
				if (num2 <= 95)
				{
					if (num2 == 92 || num2 == 95)
					{
						return killStatement;
					}
				}
				else
				{
					if (num2 == 106 || num2 == 119)
					{
						return killStatement;
					}
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return killStatement;
					}
				}
			}
			else if (num2 <= 181)
			{
				switch (num2)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return killStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num2)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return killStatement;
					case 171:
					{
						this.match(171);
						IToken token2 = this.LT(1);
						this.match(232);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.Match(token2, "STATUSONLY");
							killStatement.WithStatusOnly = true;
							TSql80ParserBaseInternal.UpdateTokenInfo(killStatement, token2);
							return killStatement;
						}
						return killStatement;
					}
					}
					break;
				}
			}
			else
			{
				if (num2 == 191 || num2 == 204)
				{
					return killStatement;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return killStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000A238C File Offset: 0x000A058C
		public BulkInsertStatement bulkInsertStatement()
		{
			BulkInsertStatement bulkInsertStatement = base.FragmentFactory.CreateFragment<BulkInsertStatement>();
			IToken token = this.LT(1);
			this.match(17);
			this.match(86);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(bulkInsertStatement, token);
				bulkInsertStatement.To = schemaObjectName;
				base.ThrowPartialAstIfPhaseOne(bulkInsertStatement);
			}
			this.match(71);
			IdentifierOrValueExpression identifierOrValueExpression = this.bulkInsertFrom();
			if (this.inputState.guessing == 0)
			{
				bulkInsertStatement.From = identifierOrValueExpression;
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return bulkInsertStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return bulkInsertStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return bulkInsertStatement;
							default:
								if (num == 28)
								{
									return bulkInsertStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return bulkInsertStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return bulkInsertStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return bulkInsertStatement;
					default:
						if (num == 82 || num == 86)
						{
							return bulkInsertStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return bulkInsertStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return bulkInsertStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return bulkInsertStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return bulkInsertStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return bulkInsertStatement;
					case 171:
						this.bulkInsertOptions(bulkInsertStatement);
						return bulkInsertStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return bulkInsertStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return bulkInsertStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x000A268C File Offset: 0x000A088C
		public InsertBulkStatement insertBulkStatement()
		{
			InsertBulkStatement insertBulkStatement = base.FragmentFactory.CreateFragment<InsertBulkStatement>();
			IToken token = this.LT(1);
			this.match(86);
			this.match(17);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				insertBulkStatement.To = schemaObjectName;
				TSql80ParserBaseInternal.UpdateTokenInfo(insertBulkStatement, token);
				base.ThrowPartialAstIfPhaseOne(insertBulkStatement);
			}
			if (this.LA(1) == 191 && (this.LA(2) == 232 || this.LA(2) == 233))
			{
				this.coldefList(insertBulkStatement);
			}
			else if (!TSql80ParserInternal.tokenSet_15_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_11_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return insertBulkStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return insertBulkStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return insertBulkStatement;
							default:
								if (num == 28)
								{
									return insertBulkStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return insertBulkStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return insertBulkStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return insertBulkStatement;
					default:
						if (num == 82 || num == 86)
						{
							return insertBulkStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return insertBulkStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return insertBulkStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return insertBulkStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return insertBulkStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return insertBulkStatement;
					case 171:
						this.insertBulkOptions(insertBulkStatement);
						return insertBulkStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return insertBulkStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return insertBulkStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000A29BC File Offset: 0x000A0BBC
		public CheckpointStatement checkpointStatement()
		{
			CheckpointStatement checkpointStatement = base.FragmentFactory.CreateFragment<CheckpointStatement>();
			IToken token = this.LT(1);
			this.match(22);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(checkpointStatement, token);
			}
			return checkpointStatement;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000A29FC File Offset: 0x000A0BFC
		public ReconfigureStatement reconfigureStatement()
		{
			ReconfigureStatement reconfigureStatement = base.FragmentFactory.CreateFragment<ReconfigureStatement>();
			IToken token = this.LT(1);
			this.match(126);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(reconfigureStatement, token);
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return reconfigureStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return reconfigureStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return reconfigureStatement;
							default:
								if (num == 28)
								{
									return reconfigureStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return reconfigureStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return reconfigureStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return reconfigureStatement;
					default:
						if (num == 82 || num == 86)
						{
							return reconfigureStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return reconfigureStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return reconfigureStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return reconfigureStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return reconfigureStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return reconfigureStatement;
					case 171:
					{
						this.match(171);
						IToken token2 = this.LT(1);
						this.match(232);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.Match(token2, "OVERRIDE");
							reconfigureStatement.WithOverride = true;
							TSql80ParserBaseInternal.UpdateTokenInfo(reconfigureStatement, token2);
							return reconfigureStatement;
						}
						return reconfigureStatement;
					}
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return reconfigureStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return reconfigureStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000A2CE0 File Offset: 0x000A0EE0
		public ShutdownStatement shutdownStatement()
		{
			ShutdownStatement shutdownStatement = base.FragmentFactory.CreateFragment<ShutdownStatement>();
			IToken token = this.LT(1);
			this.match(144);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(shutdownStatement, token);
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return shutdownStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return shutdownStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return shutdownStatement;
							default:
								if (num == 28)
								{
									return shutdownStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return shutdownStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return shutdownStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return shutdownStatement;
					default:
						if (num == 82 || num == 86)
						{
							return shutdownStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return shutdownStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return shutdownStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return shutdownStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return shutdownStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return shutdownStatement;
					case 171:
					{
						this.match(171);
						IToken token2 = this.LT(1);
						this.match(232);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.Match(token2, "NOWAIT");
							shutdownStatement.WithNoWait = true;
							TSql80ParserBaseInternal.UpdateTokenInfo(shutdownStatement, token2);
							return shutdownStatement;
						}
						return shutdownStatement;
					}
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return shutdownStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return shutdownStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x000A2FC8 File Offset: 0x000A11C8
		public SetUserStatement setUserStatement()
		{
			SetUserStatement setUserStatement = base.FragmentFactory.CreateFragment<SetUserStatement>();
			IToken token = this.LT(1);
			this.match(143);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(setUserStatement, token);
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return setUserStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return setUserStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return setUserStatement;
							default:
								if (num == 28)
								{
									return setUserStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return setUserStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return setUserStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return setUserStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return setUserStatement;
				}
			}
			else if (num <= 173)
			{
				if (num <= 119)
				{
					if (num == 95 || num == 106 || num == 119)
					{
						return setUserStatement;
					}
				}
				else
				{
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return setUserStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return setUserStatement;
						case 157:
						case 158:
						case 159:
							break;
						default:
							switch (num)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								return setUserStatement;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 191)
			{
				if (num == 176)
				{
					return setUserStatement;
				}
				switch (num)
				{
				case 180:
				case 181:
					return setUserStatement;
				default:
					if (num == 191)
					{
						return setUserStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 204)
				{
					return setUserStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return setUserStatement;
				default:
					switch (num)
					{
					case 230:
					case 231:
					case 234:
					{
						ValueExpression valueExpression = this.stringOrVariable();
						if (this.inputState.guessing == 0)
						{
							setUserStatement.UserName = valueExpression;
						}
						int num2 = this.LA(1);
						if (num2 <= 86)
						{
							if (num2 <= 28)
							{
								if (num2 <= 6)
								{
									if (num2 == 1 || num2 == 6)
									{
										return setUserStatement;
									}
								}
								else
								{
									switch (num2)
									{
									case 12:
									case 13:
									case 15:
									case 17:
										return setUserStatement;
									case 14:
									case 16:
										break;
									default:
										switch (num2)
										{
										case 22:
										case 23:
											return setUserStatement;
										default:
											if (num2 == 28)
											{
												return setUserStatement;
											}
											break;
										}
										break;
									}
								}
							}
							else if (num2 <= 64)
							{
								switch (num2)
								{
								case 33:
								case 35:
									return setUserStatement;
								case 34:
									break;
								default:
									switch (num2)
									{
									case 44:
									case 45:
									case 46:
									case 48:
									case 49:
									case 54:
									case 55:
									case 56:
									case 60:
									case 61:
									case 64:
										return setUserStatement;
									}
									break;
								}
							}
							else
							{
								switch (num2)
								{
								case 74:
								case 75:
									return setUserStatement;
								default:
									if (num2 == 82 || num2 == 86)
									{
										return setUserStatement;
									}
									break;
								}
							}
						}
						else if (num2 <= 144)
						{
							if (num2 <= 95)
							{
								if (num2 == 92 || num2 == 95)
								{
									return setUserStatement;
								}
							}
							else
							{
								if (num2 == 106 || num2 == 119)
								{
									return setUserStatement;
								}
								switch (num2)
								{
								case 123:
								case 125:
								case 126:
								case 129:
								case 131:
								case 132:
								case 134:
								case 138:
								case 140:
								case 142:
								case 143:
								case 144:
									return setUserStatement;
								}
							}
						}
						else if (num2 <= 181)
						{
							switch (num2)
							{
							case 156:
							case 160:
							case 161:
							case 162:
								return setUserStatement;
							case 157:
							case 158:
							case 159:
								break;
							default:
								switch (num2)
								{
								case 167:
								case 170:
								case 172:
								case 173:
								case 176:
								case 180:
								case 181:
									return setUserStatement;
								case 171:
								{
									this.match(171);
									IToken token2 = this.LT(1);
									this.match(232);
									if (this.inputState.guessing == 0)
									{
										TSql80ParserBaseInternal.Match(token2, "NORESET");
										setUserStatement.WithNoReset = true;
										TSql80ParserBaseInternal.UpdateTokenInfo(setUserStatement, token2);
										return setUserStatement;
									}
									return setUserStatement;
								}
								}
								break;
							}
						}
						else
						{
							if (num2 == 191 || num2 == 204)
							{
								return setUserStatement;
							}
							switch (num2)
							{
							case 219:
							case 220:
								return setUserStatement;
							}
						}
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x000A3594 File Offset: 0x000A1794
		public TruncateTableStatement truncateTableStatement()
		{
			TruncateTableStatement truncateTableStatement = base.FragmentFactory.CreateFragment<TruncateTableStatement>();
			IToken token = this.LT(1);
			this.match(156);
			this.match(148);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(truncateTableStatement, token);
				truncateTableStatement.TableName = schemaObjectName;
			}
			return truncateTableStatement;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000A35F0 File Offset: 0x000A17F0
		public GrantStatement80 grantStatement80()
		{
			GrantStatement80 grantStatement = base.FragmentFactory.CreateFragment<GrantStatement80>();
			IToken token = this.LT(1);
			this.match(75);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(grantStatement, token);
			}
			SecurityElement80 securityElement = this.securityElement80();
			if (this.inputState.guessing == 0)
			{
				grantStatement.SecurityElement80 = securityElement;
			}
			this.match(151);
			SecurityUserClause80 securityUserClause = this.securityUserClause80();
			if (this.inputState.guessing == 0)
			{
				grantStatement.SecurityUserClause80 = securityUserClause;
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							goto IL_033A;
						}
					}
					else
					{
						switch (num)
						{
						case 9:
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_033A;
						case 10:
						case 11:
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								goto IL_033A;
							default:
								if (num == 28)
								{
									goto IL_033A;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						goto IL_033A;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							goto IL_033A;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						goto IL_033A;
					default:
						if (num == 82 || num == 86)
						{
							goto IL_033A;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						goto IL_033A;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						goto IL_033A;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_033A;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					goto IL_033A;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						goto IL_033A;
					case 171:
					{
						this.match(171);
						this.match(75);
						IToken token2 = this.LT(1);
						this.match(111);
						if (this.inputState.guessing == 0)
						{
							grantStatement.WithGrantOption = true;
							TSql80ParserBaseInternal.UpdateTokenInfo(grantStatement, token2);
							goto IL_033A;
						}
						goto IL_033A;
					}
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					goto IL_033A;
				}
				switch (num)
				{
				case 219:
				case 220:
					goto IL_033A;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_033A:
			int num2 = this.LA(1);
			if (num2 <= 92)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return grantStatement;
						}
					}
					else
					{
						switch (num2)
						{
						case 9:
						{
							this.match(9);
							Identifier identifier = this.identifier();
							if (this.inputState.guessing == 0)
							{
								grantStatement.AsClause = identifier;
								return grantStatement;
							}
							return grantStatement;
						}
						case 10:
						case 11:
						case 14:
						case 16:
							break;
						case 12:
						case 13:
						case 15:
						case 17:
							return grantStatement;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return grantStatement;
							default:
								if (num2 == 28)
								{
									return grantStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 75)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return grantStatement;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return grantStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num2)
							{
							case 74:
							case 75:
								return grantStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num2 == 82 || num2 == 86 || num2 == 92)
				{
					return grantStatement;
				}
			}
			else if (num2 <= 162)
			{
				if (num2 <= 106)
				{
					if (num2 == 95 || num2 == 106)
					{
						return grantStatement;
					}
				}
				else
				{
					if (num2 == 119)
					{
						return grantStatement;
					}
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return grantStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num2)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return grantStatement;
						}
						break;
					}
				}
			}
			else if (num2 <= 181)
			{
				switch (num2)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return grantStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num2 == 176)
					{
						return grantStatement;
					}
					switch (num2)
					{
					case 180:
					case 181:
						return grantStatement;
					}
					break;
				}
			}
			else
			{
				if (num2 == 191 || num2 == 204)
				{
					return grantStatement;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return grantStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000A3BE4 File Offset: 0x000A1DE4
		public DenyStatement80 denyStatement80()
		{
			DenyStatement80 denyStatement = base.FragmentFactory.CreateFragment<DenyStatement80>();
			IToken token = this.LT(1);
			this.match(49);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(denyStatement, token);
			}
			SecurityElement80 securityElement = this.securityElement80();
			if (this.inputState.guessing == 0)
			{
				denyStatement.SecurityElement80 = securityElement;
			}
			this.match(151);
			SecurityUserClause80 securityUserClause = this.securityUserClause80();
			if (this.inputState.guessing == 0)
			{
				denyStatement.SecurityUserClause80 = securityUserClause;
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return denyStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
						case 22:
						case 23:
							return denyStatement;
						case 14:
						case 16:
						case 18:
						case 20:
						case 21:
							break;
						case 19:
						{
							IToken token2 = this.LT(1);
							this.match(19);
							if (this.inputState.guessing == 0)
							{
								denyStatement.CascadeOption = true;
								TSql80ParserBaseInternal.UpdateTokenInfo(denyStatement, token2);
								return denyStatement;
							}
							return denyStatement;
						}
						default:
							if (num == 28)
							{
								return denyStatement;
							}
							switch (num)
							{
							case 33:
							case 35:
								return denyStatement;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
						return denyStatement;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
						break;
					default:
						switch (num)
						{
						case 74:
						case 75:
							return denyStatement;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return denyStatement;
				}
			}
			else if (num <= 162)
			{
				if (num <= 106)
				{
					if (num == 95 || num == 106)
					{
						return denyStatement;
					}
				}
				else
				{
					if (num == 119)
					{
						return denyStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return denyStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return denyStatement;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return denyStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						return denyStatement;
					}
					switch (num)
					{
					case 180:
					case 181:
						return denyStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return denyStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return denyStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000A3F14 File Offset: 0x000A2114
		public RevokeStatement80 revokeStatement80()
		{
			RevokeStatement80 revokeStatement = base.FragmentFactory.CreateFragment<RevokeStatement80>();
			IToken token = this.LT(1);
			this.match(132);
			int num = this.LA(1);
			if (num <= 61)
			{
				if (num <= 12)
				{
					if (num == 5 || num == 12)
					{
						goto IL_00E3;
					}
				}
				else
				{
					if (num == 35 || num == 48)
					{
						goto IL_00E3;
					}
					switch (num)
					{
					case 60:
					case 61:
						goto IL_00E3;
					}
				}
			}
			else if (num <= 86)
			{
				if (num != 75)
				{
					if (num == 86)
					{
						goto IL_00E3;
					}
				}
				else
				{
					this.match(75);
					this.match(111);
					this.match(67);
					if (this.inputState.guessing == 0)
					{
						revokeStatement.GrantOptionFor = true;
						goto IL_00E3;
					}
					goto IL_00E3;
				}
			}
			else if (num == 127 || num == 140 || num == 160)
			{
				goto IL_00E3;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_00E3:
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(revokeStatement, token);
			}
			SecurityElement80 securityElement = this.securityElement80();
			if (this.inputState.guessing == 0)
			{
				revokeStatement.SecurityElement80 = securityElement;
			}
			int num2 = this.LA(1);
			if (num2 != 71)
			{
				if (num2 != 151)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(151);
			}
			else
			{
				this.match(71);
			}
			SecurityUserClause80 securityUserClause = this.securityUserClause80();
			if (this.inputState.guessing == 0)
			{
				revokeStatement.SecurityUserClause80 = securityUserClause;
			}
			int num3 = this.LA(1);
			if (num3 <= 92)
			{
				if (num3 <= 35)
				{
					if (num3 <= 6)
					{
						if (num3 == 1 || num3 == 6)
						{
							goto IL_042B;
						}
					}
					else
					{
						switch (num3)
						{
						case 9:
						case 12:
						case 13:
						case 15:
						case 17:
						case 22:
						case 23:
							goto IL_042B;
						case 10:
						case 11:
						case 14:
						case 16:
						case 18:
						case 20:
						case 21:
							break;
						case 19:
						{
							IToken token2 = this.LT(1);
							this.match(19);
							if (this.inputState.guessing == 0)
							{
								revokeStatement.CascadeOption = true;
								TSql80ParserBaseInternal.UpdateTokenInfo(revokeStatement, token2);
								goto IL_042B;
							}
							goto IL_042B;
						}
						default:
							if (num3 == 28)
							{
								goto IL_042B;
							}
							switch (num3)
							{
							case 33:
							case 35:
								goto IL_042B;
							}
							break;
						}
					}
				}
				else if (num3 <= 75)
				{
					switch (num3)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
						goto IL_042B;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
						break;
					default:
						switch (num3)
						{
						case 74:
						case 75:
							goto IL_042B;
						}
						break;
					}
				}
				else if (num3 == 82 || num3 == 86 || num3 == 92)
				{
					goto IL_042B;
				}
			}
			else if (num3 <= 162)
			{
				if (num3 <= 106)
				{
					if (num3 == 95 || num3 == 106)
					{
						goto IL_042B;
					}
				}
				else
				{
					if (num3 == 119)
					{
						goto IL_042B;
					}
					switch (num3)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_042B;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num3)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							goto IL_042B;
						}
						break;
					}
				}
			}
			else if (num3 <= 181)
			{
				switch (num3)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					goto IL_042B;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num3 == 176)
					{
						goto IL_042B;
					}
					switch (num3)
					{
					case 180:
					case 181:
						goto IL_042B;
					}
					break;
				}
			}
			else
			{
				if (num3 == 191 || num3 == 204)
				{
					goto IL_042B;
				}
				switch (num3)
				{
				case 219:
				case 220:
					goto IL_042B;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_042B:
			int num4 = this.LA(1);
			if (num4 <= 92)
			{
				if (num4 <= 28)
				{
					if (num4 <= 6)
					{
						if (num4 == 1 || num4 == 6)
						{
							return revokeStatement;
						}
					}
					else
					{
						switch (num4)
						{
						case 9:
						{
							this.match(9);
							Identifier identifier = this.identifier();
							if (this.inputState.guessing == 0)
							{
								revokeStatement.AsClause = identifier;
								return revokeStatement;
							}
							return revokeStatement;
						}
						case 10:
						case 11:
						case 14:
						case 16:
							break;
						case 12:
						case 13:
						case 15:
						case 17:
							return revokeStatement;
						default:
							switch (num4)
							{
							case 22:
							case 23:
								return revokeStatement;
							default:
								if (num4 == 28)
								{
									return revokeStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num4 <= 75)
				{
					switch (num4)
					{
					case 33:
					case 35:
						return revokeStatement;
					case 34:
						break;
					default:
						switch (num4)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return revokeStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num4)
							{
							case 74:
							case 75:
								return revokeStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num4 == 82 || num4 == 86 || num4 == 92)
				{
					return revokeStatement;
				}
			}
			else if (num4 <= 162)
			{
				if (num4 <= 106)
				{
					if (num4 == 95 || num4 == 106)
					{
						return revokeStatement;
					}
				}
				else
				{
					if (num4 == 119)
					{
						return revokeStatement;
					}
					switch (num4)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return revokeStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num4)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return revokeStatement;
						}
						break;
					}
				}
			}
			else if (num4 <= 181)
			{
				switch (num4)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return revokeStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num4 == 176)
					{
						return revokeStatement;
					}
					switch (num4)
					{
					case 180:
					case 181:
						return revokeStatement;
					}
					break;
				}
			}
			else
			{
				if (num4 == 191 || num4 == 204)
				{
					return revokeStatement;
				}
				switch (num4)
				{
				case 219:
				case 220:
					return revokeStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000A45F8 File Offset: 0x000A27F8
		public ReturnStatement returnStatement()
		{
			ReturnStatement returnStatement = base.FragmentFactory.CreateFragment<ReturnStatement>();
			IToken token = this.LT(1);
			this.match(131);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(returnStatement, token);
			}
			bool flag = false;
			if (TSql80ParserInternal.tokenSet_16_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_17_.member(this.LA(2)))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.expression(ExpressionFlags.None);
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag)
			{
				ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
				if (this.inputState.guessing == 0)
				{
					returnStatement.Expression = scalarExpression;
				}
			}
			else if (!TSql80ParserInternal.tokenSet_10_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_11_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return returnStatement;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000A4714 File Offset: 0x000A2914
		public OpenCursorStatement openCursorStatement()
		{
			OpenCursorStatement openCursorStatement = base.FragmentFactory.CreateFragment<OpenCursorStatement>();
			IToken token = this.LT(1);
			this.match(106);
			CursorId cursorId = this.cursorId();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(openCursorStatement, token);
				openCursorStatement.Cursor = cursorId;
			}
			return openCursorStatement;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x000A4764 File Offset: 0x000A2964
		public CloseCursorStatement closeCursorStatement()
		{
			CloseCursorStatement closeCursorStatement = base.FragmentFactory.CreateFragment<CloseCursorStatement>();
			IToken token = this.LT(1);
			this.match(23);
			CursorId cursorId = this.cursorId();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(closeCursorStatement, token);
				closeCursorStatement.Cursor = cursorId;
			}
			return closeCursorStatement;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000A47B4 File Offset: 0x000A29B4
		public DeallocateCursorStatement deallocateCursorStatement()
		{
			DeallocateCursorStatement deallocateCursorStatement = base.FragmentFactory.CreateFragment<DeallocateCursorStatement>();
			IToken token = this.LT(1);
			this.match(45);
			CursorId cursorId = this.cursorId();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(deallocateCursorStatement, token);
				deallocateCursorStatement.Cursor = cursorId;
			}
			return deallocateCursorStatement;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000A4804 File Offset: 0x000A2A04
		public FetchCursorStatement fetchCursorStatement()
		{
			IToken token = this.LT(1);
			this.match(64);
			FetchCursorStatement fetchCursorStatement = this.rowSelector();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(fetchCursorStatement, token);
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return fetchCursorStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return fetchCursorStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return fetchCursorStatement;
							default:
								if (num == 28)
								{
									return fetchCursorStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return fetchCursorStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return fetchCursorStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return fetchCursorStatement;
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (num == 82)
					{
						return fetchCursorStatement;
					}
					switch (num)
					{
					case 86:
						return fetchCursorStatement;
					case 87:
						break;
					case 88:
					{
						this.match(88);
						VariableReference variableReference = this.variable();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<VariableReference>(fetchCursorStatement, fetchCursorStatement.IntoVariables, variableReference);
						}
						while (this.LA(1) == 198)
						{
							this.match(198);
							variableReference = this.variable();
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.AddAndUpdateTokenInfo<VariableReference>(fetchCursorStatement, fetchCursorStatement.IntoVariables, variableReference);
							}
						}
						return fetchCursorStatement;
					}
					default:
						if (num == 92)
						{
							return fetchCursorStatement;
						}
						break;
					}
				}
			}
			else if (num <= 162)
			{
				if (num <= 106)
				{
					if (num == 95 || num == 106)
					{
						return fetchCursorStatement;
					}
				}
				else
				{
					if (num == 119)
					{
						return fetchCursorStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return fetchCursorStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return fetchCursorStatement;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return fetchCursorStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						return fetchCursorStatement;
					}
					switch (num)
					{
					case 180:
					case 181:
						return fetchCursorStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return fetchCursorStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return fetchCursorStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000A4B18 File Offset: 0x000A2D18
		public TSqlStatement dropStatements()
		{
			IToken token = this.LT(1);
			this.match(54);
			int num = this.LA(1);
			TSqlStatement tsqlStatement;
			if (num <= 84)
			{
				if (num <= 47)
				{
					if (num == 43)
					{
						tsqlStatement = this.dropDatabaseStatement();
						goto IL_0105;
					}
					if (num == 47)
					{
						tsqlStatement = this.dropDefaultStatement();
						goto IL_0105;
					}
				}
				else
				{
					if (num == 73)
					{
						tsqlStatement = this.dropFunctionStatement();
						goto IL_0105;
					}
					if (num == 84)
					{
						tsqlStatement = this.dropIndexStatement();
						goto IL_0105;
					}
				}
			}
			else if (num <= 137)
			{
				switch (num)
				{
				case 120:
				case 121:
					tsqlStatement = this.dropProcedureStatement();
					goto IL_0105;
				default:
					if (num == 137)
					{
						tsqlStatement = this.dropRuleStatement();
						goto IL_0105;
					}
					break;
				}
			}
			else
			{
				switch (num)
				{
				case 146:
					tsqlStatement = this.dropStatisticsStatement();
					goto IL_0105;
				case 147:
					break;
				case 148:
					tsqlStatement = this.dropTableStatement();
					goto IL_0105;
				default:
					if (num == 155)
					{
						tsqlStatement = this.dropTriggerStatement();
						goto IL_0105;
					}
					if (num == 166)
					{
						tsqlStatement = this.dropViewStatement();
						goto IL_0105;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0105:
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(tsqlStatement, token);
			}
			return tsqlStatement;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000A4C40 File Offset: 0x000A2E40
		public DbccStatement dbccStatement()
		{
			DbccStatement dbccStatement = base.FragmentFactory.CreateFragment<DbccStatement>();
			IToken token = this.LT(1);
			this.match(44);
			IToken token2 = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				DbccCommand dbccCommand;
				if (DbccCommandsHelper.Instance.TryParseOption(token2, out dbccCommand))
				{
					dbccStatement.Command = dbccCommand;
				}
				else
				{
					dbccStatement.Command = DbccCommand.Free;
					dbccStatement.DllName = token2.getText();
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(dbccStatement, token);
				TSql80ParserBaseInternal.UpdateTokenInfo(dbccStatement, token2);
			}
			if (this.LA(1) == 191 && TSql80ParserInternal.tokenSet_18_.member(this.LA(2)))
			{
				this.dbccNamedLiteralList(dbccStatement);
			}
			else if (!TSql80ParserInternal.tokenSet_15_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_11_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return dbccStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return dbccStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return dbccStatement;
							default:
								if (num == 28)
								{
									return dbccStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return dbccStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return dbccStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return dbccStatement;
					default:
						if (num == 82 || num == 86)
						{
							return dbccStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return dbccStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return dbccStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return dbccStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return dbccStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return dbccStatement;
					case 171:
						this.dbccOptions(dbccStatement);
						return dbccStatement;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return dbccStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return dbccStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000A4FAC File Offset: 0x000A31AC
		public RevertStatement revertStatement()
		{
			RevertStatement revertStatement = base.FragmentFactory.CreateFragment<RevertStatement>();
			IToken token = this.LT(1);
			this.match(176);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(revertStatement, token);
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return revertStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return revertStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return revertStatement;
							default:
								if (num == 28)
								{
									return revertStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return revertStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return revertStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return revertStatement;
					default:
						if (num == 82 || num == 86)
						{
							return revertStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return revertStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return revertStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return revertStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return revertStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return revertStatement;
					case 171:
					{
						this.match(171);
						IToken token2 = this.LT(1);
						this.match(232);
						this.match(206);
						ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.Match(token2, "COOKIE");
							revertStatement.Cookie = scalarExpression;
							return revertStatement;
						}
						return revertStatement;
					}
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return revertStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return revertStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000A52C0 File Offset: 0x000A34C0
		public DiskStatement diskStatement()
		{
			DiskStatement diskStatement = base.FragmentFactory.CreateFragment<DiskStatement>();
			IToken token = this.LT(1);
			this.match(173);
			IToken token2 = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				if (TSql80ParserBaseInternal.TryMatch(token2, "INIT"))
				{
					diskStatement.DiskStatementType = DiskStatementType.Init;
				}
				else
				{
					TSql80ParserBaseInternal.Match(token2, "RESIZE");
					diskStatement.DiskStatementType = DiskStatementType.Resize;
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(diskStatement, token);
			}
			DiskStatementOption diskStatementOption = this.diskStatementOption();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DiskStatementOption>(diskStatement, diskStatement.Options, diskStatementOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				diskStatementOption = this.diskStatementOption();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DiskStatementOption>(diskStatement, diskStatement.Options, diskStatementOption);
				}
			}
			return diskStatement;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000A53A0 File Offset: 0x000A35A0
		public CreateProcedureStatement createProcedureStatement()
		{
			CreateProcedureStatement createProcedureStatement = base.FragmentFactory.CreateFragment<CreateProcedureStatement>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(35);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createProcedureStatement, token);
			}
			this.procedureStatementBody(createProcedureStatement, out flag);
			if (this.inputState.guessing == 0 && flag)
			{
				createProcedureStatement = null;
			}
			return createProcedureStatement;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000A5400 File Offset: 0x000A3600
		public AlterProcedureStatement alterProcedureStatement()
		{
			AlterProcedureStatement alterProcedureStatement = base.FragmentFactory.CreateFragment<AlterProcedureStatement>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(6);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(alterProcedureStatement, token);
			}
			this.procedureStatementBody(alterProcedureStatement, out flag);
			if (this.inputState.guessing == 0 && flag)
			{
				alterProcedureStatement = null;
			}
			return alterProcedureStatement;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000A545C File Offset: 0x000A365C
		public CreateTriggerStatement createTriggerStatement()
		{
			CreateTriggerStatement createTriggerStatement = base.FragmentFactory.CreateFragment<CreateTriggerStatement>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(35);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createTriggerStatement, token);
			}
			this.triggerStatementBody(createTriggerStatement, out flag);
			if (this.inputState.guessing == 0 && flag)
			{
				createTriggerStatement = null;
			}
			return createTriggerStatement;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000A54BC File Offset: 0x000A36BC
		public AlterTriggerStatement alterTriggerStatement()
		{
			AlterTriggerStatement alterTriggerStatement = base.FragmentFactory.CreateFragment<AlterTriggerStatement>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(6);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(alterTriggerStatement, token);
			}
			this.triggerStatementBody(alterTriggerStatement, out flag);
			if (this.inputState.guessing == 0 && flag)
			{
				alterTriggerStatement = null;
			}
			return alterTriggerStatement;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000A5518 File Offset: 0x000A3718
		public CreateDefaultStatement createDefaultStatement()
		{
			CreateDefaultStatement createDefaultStatement = base.FragmentFactory.CreateFragment<CreateDefaultStatement>();
			IToken token = this.LT(1);
			this.match(35);
			this.match(47);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createDefaultStatement, token);
			}
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckTwoPartNameForSchemaObjectName(schemaObjectName, "DEFAULT");
				createDefaultStatement.Name = schemaObjectName;
				base.ThrowPartialAstIfPhaseOne(createDefaultStatement);
			}
			this.match(9);
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				createDefaultStatement.Expression = scalarExpression;
			}
			return createDefaultStatement;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000A55B4 File Offset: 0x000A37B4
		public CreateRuleStatement createRuleStatement()
		{
			CreateRuleStatement createRuleStatement = base.FragmentFactory.CreateFragment<CreateRuleStatement>();
			IToken token = this.LT(1);
			this.match(35);
			this.match(137);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createRuleStatement, token);
			}
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckTwoPartNameForSchemaObjectName(schemaObjectName, "RULE");
				createRuleStatement.Name = schemaObjectName;
				base.ThrowPartialAstIfPhaseOne(createRuleStatement);
			}
			this.match(9);
			BooleanExpression booleanExpression = this.booleanExpression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				createRuleStatement.Expression = booleanExpression;
			}
			return createRuleStatement;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000A5650 File Offset: 0x000A3850
		public CreateViewStatement createViewStatement()
		{
			CreateViewStatement createViewStatement = base.FragmentFactory.CreateFragment<CreateViewStatement>();
			IToken token = this.LT(1);
			this.match(35);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createViewStatement, token);
			}
			this.viewStatementBody(createViewStatement);
			return createViewStatement;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000A5698 File Offset: 0x000A3898
		public AlterViewStatement alterViewStatement()
		{
			AlterViewStatement alterViewStatement = base.FragmentFactory.CreateFragment<AlterViewStatement>();
			IToken token = this.LT(1);
			this.match(6);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(alterViewStatement, token);
			}
			this.viewStatementBody(alterViewStatement);
			return alterViewStatement;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000A56E0 File Offset: 0x000A38E0
		public CreateFunctionStatement createFunctionStatement()
		{
			CreateFunctionStatement createFunctionStatement = base.FragmentFactory.CreateFragment<CreateFunctionStatement>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(35);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createFunctionStatement, token);
			}
			this.functionStatementBody(createFunctionStatement, out flag);
			if (this.inputState.guessing == 0 && flag)
			{
				createFunctionStatement = null;
			}
			return createFunctionStatement;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000A5740 File Offset: 0x000A3940
		public AlterFunctionStatement alterFunctionStatement()
		{
			AlterFunctionStatement alterFunctionStatement = base.FragmentFactory.CreateFragment<AlterFunctionStatement>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(6);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(alterFunctionStatement, token);
			}
			this.functionStatementBody(alterFunctionStatement, out flag);
			if (this.inputState.guessing == 0 && flag)
			{
				alterFunctionStatement = null;
			}
			return alterFunctionStatement;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000A579C File Offset: 0x000A399C
		public CreateSchemaStatement createSchemaStatement()
		{
			CreateSchemaStatement createSchemaStatement = base.FragmentFactory.CreateFragment<CreateSchemaStatement>();
			IToken token = this.LT(1);
			this.match(35);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(createSchemaStatement, token);
			}
			this.match(139);
			int num = this.LA(1);
			if (num != 11)
			{
				switch (num)
				{
				case 232:
				case 233:
				{
					Identifier identifier = this.identifier();
					if (this.inputState.guessing == 0)
					{
						createSchemaStatement.Name = identifier;
					}
					this.authorizationOpt(createSchemaStatement);
					break;
				}
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				this.authorization(createSchemaStatement);
			}
			if (this.inputState.guessing == 0)
			{
				base.ThrowPartialAstIfPhaseOne(createSchemaStatement);
			}
			StatementList statementList = this.createSchemaElementList();
			if (this.inputState.guessing == 0)
			{
				createSchemaStatement.StatementList = statementList;
			}
			return createSchemaStatement;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000A587C File Offset: 0x000A3A7C
		public Identifier identifier()
		{
			Identifier identifier = base.FragmentFactory.CreateFragment<Identifier>();
			switch (this.LA(1))
			{
			case 232:
			{
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(identifier, token);
					identifier.SetUnquotedIdentifier(token.getText());
					TSql80ParserBaseInternal.CheckIdentifierLength(identifier);
				}
				break;
			}
			case 233:
			{
				IToken token2 = this.LT(1);
				this.match(233);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(identifier, token2);
					identifier.SetIdentifier(token2.getText());
					TSql80ParserBaseInternal.CheckIdentifierLength(identifier);
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return identifier;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000A5944 File Offset: 0x000A3B44
		public SqlCommandIdentifier sqlCommandIdentifier()
		{
			SqlCommandIdentifier sqlCommandIdentifier = base.FragmentFactory.CreateFragment<SqlCommandIdentifier>();
			IToken token = this.LT(1);
			this.match(226);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(sqlCommandIdentifier, token);
				sqlCommandIdentifier.SetUnquotedIdentifier(token.getText());
			}
			return sqlCommandIdentifier;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000A5994 File Offset: 0x000A3B94
		public AlterDatabaseStatement alterDbAdd()
		{
			this.match(4);
			AlterDatabaseStatement alterDatabaseStatement;
			if ((this.LA(1) == 65 || this.LA(1) == 232) && (this.LA(2) == 65 || this.LA(2) == 191))
			{
				alterDatabaseStatement = this.alterDbAddFile();
			}
			else
			{
				if (this.LA(1) != 232 || (this.LA(2) != 232 && this.LA(2) != 233))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				alterDatabaseStatement = this.alterDbAddFilegroup();
			}
			return alterDatabaseStatement;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x000A5A2C File Offset: 0x000A3C2C
		public AlterDatabaseStatement alterDbRemove()
		{
			AlterDatabaseStatement alterDatabaseStatement = null;
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "REMOVE");
			}
			int num = this.LA(1);
			if (num != 65)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token2 = this.LT(1);
				this.match(232);
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token2, "FILEGROUP");
					AlterDatabaseRemoveFileGroupStatement alterDatabaseRemoveFileGroupStatement = base.FragmentFactory.CreateFragment<AlterDatabaseRemoveFileGroupStatement>();
					alterDatabaseRemoveFileGroupStatement.FileGroup = identifier;
					alterDatabaseStatement = alterDatabaseRemoveFileGroupStatement;
				}
			}
			else
			{
				this.match(65);
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					AlterDatabaseRemoveFileStatement alterDatabaseRemoveFileStatement = base.FragmentFactory.CreateFragment<AlterDatabaseRemoveFileStatement>();
					alterDatabaseRemoveFileStatement.File = identifier;
					alterDatabaseStatement = alterDatabaseRemoveFileStatement;
				}
			}
			return alterDatabaseStatement;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x000A5B20 File Offset: 0x000A3D20
		public AlterDatabaseStatement alterDbModify()
		{
			AlterDatabaseStatement alterDatabaseStatement = null;
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "MODIFY");
			}
			if (this.LA(1) == 232 && this.LA(2) == 206 && base.NextTokenMatches("NAME"))
			{
				IToken token2 = this.LT(1);
				this.match(232);
				this.match(206);
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token2, "NAME");
					AlterDatabaseModifyNameStatement alterDatabaseModifyNameStatement = base.FragmentFactory.CreateFragment<AlterDatabaseModifyNameStatement>();
					alterDatabaseModifyNameStatement.NewDatabaseName = identifier;
					alterDatabaseStatement = alterDatabaseModifyNameStatement;
				}
			}
			else if (this.LA(1) == 232 && (this.LA(2) == 232 || this.LA(2) == 233))
			{
				IToken token3 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token3, "FILEGROUP");
				}
				alterDatabaseStatement = this.alterDbModifyFilegroup();
			}
			else
			{
				if (this.LA(1) != 65)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				alterDatabaseStatement = this.alterDbModifyFile();
			}
			return alterDatabaseStatement;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000A5C70 File Offset: 0x000A3E70
		public AlterDatabaseSetStatement alterDbSet()
		{
			this.match(142);
			AlterDatabaseSetStatement alterDatabaseSetStatement = this.dbOptionStateList();
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return alterDatabaseSetStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return alterDatabaseSetStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return alterDatabaseSetStatement;
							default:
								if (num == 28)
								{
									return alterDatabaseSetStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return alterDatabaseSetStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return alterDatabaseSetStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return alterDatabaseSetStatement;
					default:
						if (num == 82 || num == 86)
						{
							return alterDatabaseSetStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return alterDatabaseSetStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return alterDatabaseSetStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return alterDatabaseSetStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return alterDatabaseSetStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return alterDatabaseSetStatement;
					case 171:
					{
						AlterDatabaseTermination alterDatabaseTermination = this.xactTermination();
						if (this.inputState.guessing == 0)
						{
							alterDatabaseSetStatement.Termination = alterDatabaseTermination;
							return alterDatabaseSetStatement;
						}
						return alterDatabaseSetStatement;
					}
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return alterDatabaseSetStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return alterDatabaseSetStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000A5F08 File Offset: 0x000A4108
		public AlterDatabaseCollateStatement alterDbCollate()
		{
			AlterDatabaseCollateStatement alterDatabaseCollateStatement = base.FragmentFactory.CreateFragment<AlterDatabaseCollateStatement>();
			this.collation(alterDatabaseCollateStatement);
			return alterDatabaseCollateStatement;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000A5F2C File Offset: 0x000A412C
		public void collation(ICollationSetter vParent)
		{
			this.match(26);
			Identifier identifier = this.nonQuotedIdentifier();
			if (this.inputState.guessing == 0)
			{
				vParent.Collation = identifier;
			}
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000A5F5C File Offset: 0x000A415C
		public AlterDatabaseAddFileStatement alterDbAddFile()
		{
			AlterDatabaseAddFileStatement alterDatabaseAddFileStatement = base.FragmentFactory.CreateFragment<AlterDatabaseAddFileStatement>();
			int num = this.LA(1);
			if (num != 65)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "LOG");
					alterDatabaseAddFileStatement.IsLog = true;
				}
			}
			this.match(65);
			if (this.inputState.guessing == 0)
			{
				base.ThrowPartialAstIfPhaseOne(alterDatabaseAddFileStatement);
			}
			this.fileDeclBodyList(alterDatabaseAddFileStatement, alterDatabaseAddFileStatement.FileDeclarations);
			int num2 = this.LA(1);
			if (num2 <= 92)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return alterDatabaseAddFileStatement;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return alterDatabaseAddFileStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return alterDatabaseAddFileStatement;
							default:
								if (num2 == 28)
								{
									return alterDatabaseAddFileStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 75)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return alterDatabaseAddFileStatement;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return alterDatabaseAddFileStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num2)
							{
							case 74:
							case 75:
								return alterDatabaseAddFileStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num2 == 82 || num2 == 86 || num2 == 92)
				{
					return alterDatabaseAddFileStatement;
				}
			}
			else if (num2 <= 162)
			{
				if (num2 <= 119)
				{
					if (num2 == 95 || num2 == 106 || num2 == 119)
					{
						return alterDatabaseAddFileStatement;
					}
				}
				else
				{
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return alterDatabaseAddFileStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						if (num2 != 151)
						{
							switch (num2)
							{
							case 156:
							case 160:
							case 161:
							case 162:
								return alterDatabaseAddFileStatement;
							}
						}
						else
						{
							Identifier identifier = this.toFilegroup();
							if (this.inputState.guessing != 0)
							{
								return alterDatabaseAddFileStatement;
							}
							if (alterDatabaseAddFileStatement.IsLog)
							{
								throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(identifier);
							}
							alterDatabaseAddFileStatement.FileGroup = identifier;
							return alterDatabaseAddFileStatement;
						}
						break;
					}
				}
			}
			else if (num2 <= 181)
			{
				switch (num2)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return alterDatabaseAddFileStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num2 == 176)
					{
						return alterDatabaseAddFileStatement;
					}
					switch (num2)
					{
					case 180:
					case 181:
						return alterDatabaseAddFileStatement;
					}
					break;
				}
			}
			else
			{
				if (num2 == 191 || num2 == 204)
				{
					return alterDatabaseAddFileStatement;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return alterDatabaseAddFileStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000A62B0 File Offset: 0x000A44B0
		public AlterDatabaseAddFileGroupStatement alterDbAddFilegroup()
		{
			AlterDatabaseAddFileGroupStatement alterDatabaseAddFileGroupStatement = base.FragmentFactory.CreateFragment<AlterDatabaseAddFileGroupStatement>();
			IToken token = this.LT(1);
			this.match(232);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "FILEGROUP");
				alterDatabaseAddFileGroupStatement.FileGroup = identifier;
			}
			return alterDatabaseAddFileGroupStatement;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000A6308 File Offset: 0x000A4508
		public void fileDeclBodyList(TSqlFragment vParent, IList<FileDeclaration> fileDeclarations)
		{
			FileDeclaration fileDeclaration = this.fileDeclBody(false);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<FileDeclaration>(vParent, fileDeclarations, fileDeclaration);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				fileDeclaration = this.fileDeclBody(false);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<FileDeclaration>(vParent, fileDeclarations, fileDeclaration);
				}
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000A636C File Offset: 0x000A456C
		public Identifier toFilegroup()
		{
			this.match(151);
			IToken token = this.LT(1);
			this.match(232);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "FILEGROUP");
			}
			return identifier;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000A63BC File Offset: 0x000A45BC
		public AlterDatabaseModifyFileGroupStatement alterDbModifyFilegroup()
		{
			AlterDatabaseModifyFileGroupStatement alterDatabaseModifyFileGroupStatement = base.FragmentFactory.CreateFragment<AlterDatabaseModifyFileGroupStatement>();
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				alterDatabaseModifyFileGroupStatement.FileGroup = identifier;
			}
			if (this.LA(1) == 232 && this.LA(2) == 206)
			{
				IToken token = this.LT(1);
				this.match(232);
				this.match(206);
				Identifier identifier2 = this.identifier();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "NAME");
					alterDatabaseModifyFileGroupStatement.NewFileGroupName = identifier2;
					base.ThrowPartialAstIfPhaseOne(alterDatabaseModifyFileGroupStatement);
				}
			}
			else if (this.LA(1) == 47)
			{
				IToken token2 = this.LT(1);
				this.match(47);
				if (this.inputState.guessing == 0)
				{
					alterDatabaseModifyFileGroupStatement.MakeDefault = true;
					TSql80ParserBaseInternal.UpdateTokenInfo(alterDatabaseModifyFileGroupStatement, token2);
				}
			}
			else
			{
				if (this.LA(1) != 232 || !TSql80ParserInternal.tokenSet_10_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token3 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					base.ThrowPartialAstIfPhaseOne(alterDatabaseModifyFileGroupStatement);
					alterDatabaseModifyFileGroupStatement.UpdatabilityOption = ModifyFilegroupOptionsHelper.Instance.ParseOption(token3);
					TSql80ParserBaseInternal.UpdateTokenInfo(alterDatabaseModifyFileGroupStatement, token3);
				}
			}
			return alterDatabaseModifyFileGroupStatement;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000A651C File Offset: 0x000A471C
		public AlterDatabaseModifyFileStatement alterDbModifyFile()
		{
			AlterDatabaseModifyFileStatement alterDatabaseModifyFileStatement = base.FragmentFactory.CreateFragment<AlterDatabaseModifyFileStatement>();
			this.match(65);
			if (this.inputState.guessing == 0)
			{
				base.ThrowPartialAstIfPhaseOne(alterDatabaseModifyFileStatement);
			}
			FileDeclaration fileDeclaration = this.fileDecl(true);
			if (this.inputState.guessing == 0)
			{
				alterDatabaseModifyFileStatement.FileDeclaration = fileDeclaration;
			}
			return alterDatabaseModifyFileStatement;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000A6570 File Offset: 0x000A4770
		public FileDeclaration fileDecl(bool isAlterDbModifyFileStatement)
		{
			int num = this.LA(1);
			FileDeclaration fileDeclaration;
			if (num != 118)
			{
				if (num != 191)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				fileDeclaration = this.fileDeclBody(isAlterDbModifyFileStatement);
			}
			else
			{
				IToken token = this.LT(1);
				this.match(118);
				fileDeclaration = this.fileDeclBody(isAlterDbModifyFileStatement);
				if (this.inputState.guessing == 0)
				{
					fileDeclaration.IsPrimary = true;
					TSql80ParserBaseInternal.UpdateTokenInfo(fileDeclaration, token);
				}
			}
			return fileDeclaration;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000A65EC File Offset: 0x000A47EC
		public AlterDatabaseSetStatement dbOptionStateList()
		{
			AlterDatabaseSetStatement alterDatabaseSetStatement = base.FragmentFactory.CreateFragment<AlterDatabaseSetStatement>();
			DatabaseOption databaseOption = this.dbOptionStateItem();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DatabaseOption>(alterDatabaseSetStatement, alterDatabaseSetStatement.Options, databaseOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				databaseOption = this.dbOptionStateItem();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DatabaseOption>(alterDatabaseSetStatement, alterDatabaseSetStatement.Options, databaseOption);
				}
			}
			return alterDatabaseSetStatement;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000A6664 File Offset: 0x000A4864
		public AlterDatabaseTermination xactTermination()
		{
			AlterDatabaseTermination alterDatabaseTermination = base.FragmentFactory.CreateFragment<AlterDatabaseTermination>();
			IToken token = this.LT(1);
			this.match(171);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(alterDatabaseTermination, token);
			}
			int num = this.LA(1);
			if (num != 134)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token2 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token2, "NO_WAIT");
					TSql80ParserBaseInternal.UpdateTokenInfo(alterDatabaseTermination, token2);
					alterDatabaseTermination.NoWait = true;
				}
			}
			else
			{
				this.match(134);
				if (this.LA(1) == 232 && this.LA(2) == 221)
				{
					IToken token3 = this.LT(1);
					this.match(232);
					Literal literal = this.integer();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.Match(token3, "AFTER");
						alterDatabaseTermination.RollbackAfter = literal;
					}
					int num2 = this.LA(1);
					if (num2 <= 92)
					{
						if (num2 <= 28)
						{
							if (num2 <= 6)
							{
								if (num2 == 1 || num2 == 6)
								{
									return alterDatabaseTermination;
								}
							}
							else
							{
								switch (num2)
								{
								case 12:
								case 13:
								case 15:
								case 17:
									return alterDatabaseTermination;
								case 14:
								case 16:
									break;
								default:
									switch (num2)
									{
									case 22:
									case 23:
										return alterDatabaseTermination;
									default:
										if (num2 == 28)
										{
											return alterDatabaseTermination;
										}
										break;
									}
									break;
								}
							}
						}
						else if (num2 <= 75)
						{
							switch (num2)
							{
							case 33:
							case 35:
								return alterDatabaseTermination;
							case 34:
								break;
							default:
								switch (num2)
								{
								case 44:
								case 45:
								case 46:
								case 48:
								case 49:
								case 54:
								case 55:
								case 56:
								case 60:
								case 61:
								case 64:
									return alterDatabaseTermination;
								case 47:
								case 50:
								case 51:
								case 52:
								case 53:
								case 57:
								case 58:
								case 59:
								case 62:
								case 63:
									break;
								default:
									switch (num2)
									{
									case 74:
									case 75:
										return alterDatabaseTermination;
									}
									break;
								}
								break;
							}
						}
						else if (num2 == 82 || num2 == 86 || num2 == 92)
						{
							return alterDatabaseTermination;
						}
					}
					else if (num2 <= 173)
					{
						if (num2 <= 119)
						{
							if (num2 == 95 || num2 == 106 || num2 == 119)
							{
								return alterDatabaseTermination;
							}
						}
						else
						{
							switch (num2)
							{
							case 123:
							case 125:
							case 126:
							case 129:
							case 131:
							case 132:
							case 134:
							case 138:
							case 140:
							case 142:
							case 143:
							case 144:
								return alterDatabaseTermination;
							case 124:
							case 127:
							case 128:
							case 130:
							case 133:
							case 135:
							case 136:
							case 137:
							case 139:
							case 141:
								break;
							default:
								switch (num2)
								{
								case 156:
								case 160:
								case 161:
								case 162:
									return alterDatabaseTermination;
								case 157:
								case 158:
								case 159:
									break;
								default:
									switch (num2)
									{
									case 167:
									case 170:
									case 172:
									case 173:
										return alterDatabaseTermination;
									}
									break;
								}
								break;
							}
						}
					}
					else if (num2 <= 191)
					{
						if (num2 == 176)
						{
							return alterDatabaseTermination;
						}
						switch (num2)
						{
						case 180:
						case 181:
							return alterDatabaseTermination;
						default:
							if (num2 == 191)
							{
								return alterDatabaseTermination;
							}
							break;
						}
					}
					else
					{
						if (num2 == 204)
						{
							return alterDatabaseTermination;
						}
						switch (num2)
						{
						case 219:
						case 220:
							return alterDatabaseTermination;
						default:
							if (num2 == 232)
							{
								IToken token4 = this.LT(1);
								this.match(232);
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.Match(token4, "SECONDS");
									TSql80ParserBaseInternal.UpdateTokenInfo(alterDatabaseTermination, token4);
									return alterDatabaseTermination;
								}
								return alterDatabaseTermination;
							}
							break;
						}
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				if (this.LA(1) != 232 || !TSql80ParserInternal.tokenSet_10_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token5 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token5, "IMMEDIATE");
					TSql80ParserBaseInternal.UpdateTokenInfo(alterDatabaseTermination, token5);
					alterDatabaseTermination.ImmediateRollback = true;
				}
			}
			return alterDatabaseTermination;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000A6AC4 File Offset: 0x000A4CC4
		public IntegerLiteral integer()
		{
			IntegerLiteral integerLiteral = base.FragmentFactory.CreateFragment<IntegerLiteral>();
			IToken token = this.LT(1);
			this.match(221);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(integerLiteral, token);
				integerLiteral.Value = token.getText();
			}
			return integerLiteral;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000A6B14 File Offset: 0x000A4D14
		public DatabaseOption dbOptionStateItem()
		{
			DatabaseOption databaseOption;
			if (this.LA(1) == 232 && this.LA(2) == 232 && base.NextTokenMatches("CURSOR_DEFAULT"))
			{
				databaseOption = this.cursorDefaultDbOption();
			}
			else if (this.LA(1) == 232 && (this.LA(2) == 72 || this.LA(2) == 232) && base.NextTokenMatches("RECOVERY"))
			{
				databaseOption = this.recoveryDbOption();
			}
			else if (this.LA(1) == 232 && TSql80ParserInternal.tokenSet_19_.member(this.LA(2)))
			{
				databaseOption = this.dbSingleIdentOption();
			}
			else
			{
				if (this.LA(1) != 232 || (this.LA(2) != 103 && this.LA(2) != 105))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				databaseOption = this.alterDbOnOffOption();
			}
			return databaseOption;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000A6C00 File Offset: 0x000A4E00
		public CursorDefaultDatabaseOption cursorDefaultDbOption()
		{
			CursorDefaultDatabaseOption cursorDefaultDatabaseOption = base.FragmentFactory.CreateFragment<CursorDefaultDatabaseOption>();
			IToken token = this.LT(1);
			this.match(232);
			IToken token2 = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "CURSOR_DEFAULT");
				cursorDefaultDatabaseOption.OptionKind = DatabaseOptionKind.CursorDefault;
				if (TSql80ParserBaseInternal.TryMatch(token2, "LOCAL"))
				{
					cursorDefaultDatabaseOption.IsLocal = true;
				}
				else
				{
					TSql80ParserBaseInternal.Match(token2, "GLOBAL");
					cursorDefaultDatabaseOption.IsLocal = false;
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(cursorDefaultDatabaseOption, token2);
			}
			return cursorDefaultDatabaseOption;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000A6C94 File Offset: 0x000A4E94
		public RecoveryDatabaseOption recoveryDbOption()
		{
			RecoveryDatabaseOption recoveryDatabaseOption = base.FragmentFactory.CreateFragment<RecoveryDatabaseOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "RECOVERY");
				recoveryDatabaseOption.OptionKind = DatabaseOptionKind.Recovery;
			}
			int num = this.LA(1);
			if (num != 72)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token2 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					recoveryDatabaseOption.Value = RecoveryDbOptionsHelper.Instance.ParseOption(token2);
					TSql80ParserBaseInternal.UpdateTokenInfo(recoveryDatabaseOption, token2);
				}
			}
			else
			{
				IToken token3 = this.LT(1);
				this.match(72);
				if (this.inputState.guessing == 0)
				{
					recoveryDatabaseOption.Value = RecoveryDatabaseOptionKind.Full;
					TSql80ParserBaseInternal.UpdateTokenInfo(recoveryDatabaseOption, token3);
				}
			}
			return recoveryDatabaseOption;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000A6D7C File Offset: 0x000A4F7C
		public DatabaseOption dbSingleIdentOption()
		{
			DatabaseOption databaseOption = base.FragmentFactory.CreateFragment<DatabaseOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				databaseOption.OptionKind = SimpleDbOptionsHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
				TSql80ParserBaseInternal.UpdateTokenInfo(databaseOption, token);
			}
			return databaseOption;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000A6DD4 File Offset: 0x000A4FD4
		public OnOffDatabaseOption alterDbOnOffOption()
		{
			OnOffDatabaseOption onOffDatabaseOption = base.FragmentFactory.CreateFragment<OnOffDatabaseOption>();
			IToken token = this.LT(1);
			this.match(232);
			OptionState optionState = this.optionOnOff(onOffDatabaseOption);
			if (this.inputState.guessing == 0)
			{
				onOffDatabaseOption.OptionKind = OnOffSimpleDbOptionsHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
				onOffDatabaseOption.OptionState = optionState;
			}
			return onOffDatabaseOption;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000A6E34 File Offset: 0x000A5034
		public OptionState optionOnOff(TSqlFragment vParent)
		{
			OptionState optionState = OptionState.NotSet;
			switch (this.LA(1))
			{
			case 103:
			{
				IToken token = this.LT(1);
				this.match(103);
				if (this.inputState.guessing == 0)
				{
					optionState = OptionState.Off;
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					return optionState;
				}
				return optionState;
			}
			case 105:
			{
				IToken token2 = this.LT(1);
				this.match(105);
				if (this.inputState.guessing == 0)
				{
					optionState = OptionState.On;
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
					return optionState;
				}
				return optionState;
			}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x000A6ECC File Offset: 0x000A50CC
		public void recoveryUnitList(CreateDatabaseStatement vParent)
		{
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							goto IL_0284;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_0284;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
							case 26:
							case 28:
								goto IL_0284;
							case 24:
							case 25:
							case 27:
								break;
							default:
								switch (num)
								{
								case 33:
								case 35:
									goto IL_0284;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
						goto IL_0284;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
						break;
					default:
						if (num == 67)
						{
							goto IL_0284;
						}
						switch (num)
						{
						case 74:
						case 75:
							goto IL_0284;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					goto IL_0284;
				}
			}
			else if (num <= 173)
			{
				if (num <= 119)
				{
					if (num == 95)
					{
						goto IL_0284;
					}
					switch (num)
					{
					case 105:
						this.onDisk(vParent);
						goto IL_0284;
					case 106:
						goto IL_0284;
					default:
						if (num == 119)
						{
							goto IL_0284;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_0284;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							goto IL_0284;
						case 157:
						case 158:
						case 159:
							break;
						default:
							switch (num)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								goto IL_0284;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 191)
			{
				if (num == 176)
				{
					goto IL_0284;
				}
				switch (num)
				{
				case 180:
				case 181:
					goto IL_0284;
				default:
					if (num == 191)
					{
						goto IL_0284;
					}
					break;
				}
			}
			else
			{
				if (num == 204)
				{
					goto IL_0284;
				}
				switch (num)
				{
				case 219:
				case 220:
					goto IL_0284;
				default:
					if (num == 232)
					{
						goto IL_0284;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0284:
			if (this.LA(1) == 232 && base.NextTokenMatches("LOG"))
			{
				IToken token = this.LT(1);
				this.match(232);
				this.match(105);
				this.fileDeclBodyList(vParent, vParent.LogOn);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "LOG");
					return;
				}
				return;
			}
			else
			{
				if (TSql80ParserInternal.tokenSet_20_.member(this.LA(1)))
				{
					return;
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x000A71E0 File Offset: 0x000A53E0
		public void collationOpt(ICollationSetter vParent)
		{
			switch (this.LA(1))
			{
			case 1:
			case 6:
			case 7:
			case 9:
			case 10:
			case 12:
			case 13:
			case 14:
			case 15:
			case 17:
			case 21:
			case 22:
			case 23:
			case 28:
			case 29:
			case 30:
			case 33:
			case 35:
			case 36:
			case 44:
			case 45:
			case 46:
			case 47:
			case 48:
			case 49:
			case 50:
			case 54:
			case 55:
			case 56:
			case 58:
			case 59:
			case 60:
			case 61:
			case 64:
			case 67:
			case 68:
			case 71:
			case 72:
			case 74:
			case 75:
			case 76:
			case 77:
			case 79:
			case 82:
			case 83:
			case 85:
			case 86:
			case 87:
			case 88:
			case 89:
			case 90:
			case 92:
			case 93:
			case 94:
			case 95:
			case 99:
			case 100:
			case 105:
			case 106:
			case 111:
			case 112:
			case 113:
			case 114:
			case 118:
			case 119:
			case 123:
			case 125:
			case 126:
			case 127:
			case 129:
			case 131:
			case 132:
			case 133:
			case 134:
			case 136:
			case 138:
			case 140:
			case 142:
			case 143:
			case 144:
			case 150:
			case 156:
			case 158:
			case 159:
			case 160:
			case 161:
			case 162:
			case 167:
			case 168:
			case 169:
			case 170:
			case 171:
			case 172:
			case 173:
			case 176:
			case 180:
			case 181:
			case 188:
			case 189:
			case 190:
			case 191:
			case 192:
			case 193:
			case 194:
			case 195:
			case 196:
			case 197:
			case 198:
			case 199:
			case 201:
			case 204:
			case 205:
			case 206:
			case 207:
			case 208:
			case 209:
			case 210:
			case 219:
			case 220:
			case 230:
			case 231:
			case 232:
			case 233:
			case 234:
				return;
			case 26:
				this.collation(vParent);
				return;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000A75C4 File Offset: 0x000A57C4
		public void dbAddendums(CreateDatabaseStatement vParent)
		{
			this.match(67);
			int num = this.LA(1);
			if (num != 181)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "ATTACH");
					vParent.AttachMode = AttachMode.Attach;
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(181);
				if (this.inputState.guessing == 0)
				{
					vParent.AttachMode = AttachMode.Load;
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
				}
			}
			int num2 = this.LA(1);
			if (num2 <= 92)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return;
						case 14:
						case 16:
							break;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return;
							default:
								if (num2 == 28)
								{
									return;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 75)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num2)
							{
							case 74:
							case 75:
								return;
							}
							break;
						}
						break;
					}
				}
				else if (num2 == 82 || num2 == 86 || num2 == 92)
				{
					return;
				}
			}
			else if (num2 <= 173)
			{
				if (num2 <= 119)
				{
					if (num2 == 95 || num2 == 106 || num2 == 119)
					{
						return;
					}
				}
				else
				{
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num2)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return;
						case 157:
						case 158:
						case 159:
							break;
						default:
							switch (num2)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								return;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num2 <= 191)
			{
				if (num2 == 176)
				{
					return;
				}
				switch (num2)
				{
				case 180:
				case 181:
					return;
				default:
					if (num2 == 191)
					{
						return;
					}
					break;
				}
			}
			else
			{
				if (num2 == 204)
				{
					return;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return;
				default:
					if (num2 == 232)
					{
						this.match(232);
						return;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000A78DC File Offset: 0x000A5ADC
		public void onDisk(CreateDatabaseStatement vParent)
		{
			FileGroupDefinition fileGroupDefinition = base.FragmentFactory.CreateFragment<FileGroupDefinition>();
			vParent.FileGroups.Add(fileGroupDefinition);
			this.match(105);
			FileDeclaration fileDeclaration = this.fileDecl(false);
			if (this.inputState.guessing == 0)
			{
				fileGroupDefinition.FileDeclarations.Add(fileDeclaration);
				vParent.UpdateTokenInfo(fileDeclaration);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				int num = this.LA(1);
				if (num != 118 && num != 191)
				{
					if (num != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					FileGroupDefinition fileGroupDefinition2 = this.fileGroupDecl();
					if (this.inputState.guessing == 0)
					{
						fileGroupDefinition = fileGroupDefinition2;
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<FileGroupDefinition>(vParent, vParent.FileGroups, fileGroupDefinition);
					}
				}
				else
				{
					fileDeclaration = this.fileDecl(false);
					if (this.inputState.guessing == 0)
					{
						fileGroupDefinition.FileDeclarations.Add(fileDeclaration);
						vParent.UpdateTokenInfo(fileDeclaration);
					}
				}
			}
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000A79D4 File Offset: 0x000A5BD4
		public FileGroupDefinition fileGroupDecl()
		{
			FileGroupDefinition fileGroupDefinition = base.FragmentFactory.CreateFragment<FileGroupDefinition>();
			IToken token = this.LT(1);
			this.match(232);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "FILEGROUP");
				TSql80ParserBaseInternal.UpdateTokenInfo(fileGroupDefinition, token);
				fileGroupDefinition.Name = identifier;
			}
			int num = this.LA(1);
			if (num != 47)
			{
				if (num != 191)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				this.match(47);
				if (this.inputState.guessing == 0)
				{
					fileGroupDefinition.IsDefault = true;
				}
			}
			FileDeclaration fileDeclaration = this.fileDeclBody(false);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<FileDeclaration>(fileGroupDefinition, fileGroupDefinition.FileDeclarations, fileDeclaration);
			}
			return fileGroupDefinition;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000A7AA0 File Offset: 0x000A5CA0
		public FileDeclaration fileDeclBody(bool isAlterDbModifyFileStatement)
		{
			FileDeclaration fileDeclaration = base.FragmentFactory.CreateFragment<FileDeclaration>();
			int num = 0;
			IToken token = this.LT(1);
			this.match(191);
			FileDeclarationOption fileDeclarationOption = this.fileOption(isAlterDbModifyFileStatement);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(fileDeclaration, token);
				TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)fileDeclarationOption.OptionKind, fileDeclarationOption);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<FileDeclarationOption>(fileDeclaration, fileDeclaration.Options, fileDeclarationOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				fileDeclarationOption = this.fileOption(isAlterDbModifyFileStatement);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)fileDeclarationOption.OptionKind, fileDeclarationOption);
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<FileDeclarationOption>(fileDeclaration, fileDeclaration.Options, fileDeclarationOption);
				}
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(fileDeclaration, token2);
				if (!isAlterDbModifyFileStatement && (num & 8) == 0)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46065", fileDeclaration, TSqlParserResource.SQL46065Message, new string[0]);
				}
			}
			return fileDeclaration;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000A7B9C File Offset: 0x000A5D9C
		public FileDeclarationOption fileOption(bool newNameAllowed)
		{
			FileDeclarationOption fileDeclarationOption;
			if (this.LA(1) == 232 && this.LA(2) == 206 && base.NextTokenMatches("NAME"))
			{
				fileDeclarationOption = this.nameFileOption();
			}
			else if (this.LA(1) == 232 && this.LA(2) == 206 && base.NextTokenMatches("FILENAME"))
			{
				fileDeclarationOption = this.fileNameFileOption();
			}
			else if (this.LA(1) == 232 && this.LA(2) == 206 && base.NextTokenMatches("SIZE"))
			{
				fileDeclarationOption = this.sizeFileOption();
			}
			else if (this.LA(1) == 232 && this.LA(2) == 206 && base.NextTokenMatches("MAXSIZE"))
			{
				fileDeclarationOption = this.maxSizeFileOption();
			}
			else if (this.LA(1) == 232 && this.LA(2) == 206 && base.NextTokenMatches("FILEGROWTH"))
			{
				fileDeclarationOption = this.fileGrowthFileOption();
			}
			else
			{
				if (this.LA(1) != 232 || this.LA(2) != 206 || !base.NextTokenMatches("NEWNAME"))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				fileDeclarationOption = this.newNameFileOption();
				if (this.inputState.guessing == 0 && !newNameAllowed)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46062", fileDeclarationOption, TSqlParserResource.SQL46062Message, new string[0]);
				}
			}
			return fileDeclarationOption;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000A7D20 File Offset: 0x000A5F20
		public NameFileDeclarationOption nameFileOption()
		{
			NameFileDeclarationOption nameFileDeclarationOption = base.FragmentFactory.CreateFragment<NameFileDeclarationOption>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(206);
			IdentifierOrValueExpression identifierOrValueExpression = this.nonEmptyStringOrIdentifier();
			if (this.inputState.guessing == 0)
			{
				nameFileDeclarationOption.OptionKind = FileDeclarationOptionKind.Name;
				TSql80ParserBaseInternal.Match(token, "NAME");
				TSql80ParserBaseInternal.UpdateTokenInfo(nameFileDeclarationOption, token);
				nameFileDeclarationOption.LogicalFileName = identifierOrValueExpression;
				nameFileDeclarationOption.IsNewName = false;
			}
			return nameFileDeclarationOption;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000A7D98 File Offset: 0x000A5F98
		public FileNameFileDeclarationOption fileNameFileOption()
		{
			FileNameFileDeclarationOption fileNameFileDeclarationOption = base.FragmentFactory.CreateFragment<FileNameFileDeclarationOption>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(206);
			Literal literal = this.nonEmptyString();
			if (this.inputState.guessing == 0)
			{
				fileNameFileDeclarationOption.OptionKind = FileDeclarationOptionKind.FileName;
				TSql80ParserBaseInternal.Match(token, "FILENAME");
				TSql80ParserBaseInternal.UpdateTokenInfo(fileNameFileDeclarationOption, token);
				fileNameFileDeclarationOption.OSFileName = literal;
			}
			return fileNameFileDeclarationOption;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000A7E08 File Offset: 0x000A6008
		public SizeFileDeclarationOption sizeFileOption()
		{
			SizeFileDeclarationOption sizeFileDeclarationOption = base.FragmentFactory.CreateFragment<SizeFileDeclarationOption>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(206);
			Literal literal = this.integer();
			if (this.inputState.guessing == 0)
			{
				sizeFileDeclarationOption.OptionKind = FileDeclarationOptionKind.Size;
				TSql80ParserBaseInternal.Match(token, "SIZE");
				TSql80ParserBaseInternal.UpdateTokenInfo(sizeFileDeclarationOption, token);
				sizeFileDeclarationOption.Size = literal;
			}
			int num = this.LA(1);
			if (num != 192 && num != 198)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				MemoryUnit memoryUnit = this.memUnit();
				if (this.inputState.guessing == 0)
				{
					sizeFileDeclarationOption.Units = memoryUnit;
				}
			}
			return sizeFileDeclarationOption;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000A7ECC File Offset: 0x000A60CC
		public MaxSizeFileDeclarationOption maxSizeFileOption()
		{
			MaxSizeFileDeclarationOption maxSizeFileDeclarationOption = base.FragmentFactory.CreateFragment<MaxSizeFileDeclarationOption>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(206);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "MAXSIZE");
				maxSizeFileDeclarationOption.OptionKind = FileDeclarationOptionKind.MaxSize;
				TSql80ParserBaseInternal.UpdateTokenInfo(maxSizeFileDeclarationOption, token);
			}
			int num = this.LA(1);
			if (num != 221)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token2 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token2, "UNLIMITED");
					maxSizeFileDeclarationOption.Unlimited = true;
				}
			}
			else
			{
				Literal literal = this.integer();
				if (this.inputState.guessing == 0)
				{
					maxSizeFileDeclarationOption.MaxSize = literal;
				}
				int num2 = this.LA(1);
				if (num2 != 192 && num2 != 198)
				{
					if (num2 != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					MemoryUnit memoryUnit = this.memUnit();
					if (this.inputState.guessing == 0)
					{
						maxSizeFileDeclarationOption.Units = memoryUnit;
					}
				}
			}
			return maxSizeFileDeclarationOption;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000A800C File Offset: 0x000A620C
		public FileGrowthFileDeclarationOption fileGrowthFileOption()
		{
			FileGrowthFileDeclarationOption fileGrowthFileDeclarationOption = base.FragmentFactory.CreateFragment<FileGrowthFileDeclarationOption>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(206);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "FILEGROWTH");
				fileGrowthFileDeclarationOption.OptionKind = FileDeclarationOptionKind.FileGrowth;
				TSql80ParserBaseInternal.UpdateTokenInfo(fileGrowthFileDeclarationOption, token);
			}
			Literal literal = this.integer();
			if (this.inputState.guessing == 0)
			{
				fileGrowthFileDeclarationOption.GrowthIncrement = literal;
			}
			int num = this.LA(1);
			if (num <= 192)
			{
				if (num != 189)
				{
					if (num == 192)
					{
						return fileGrowthFileDeclarationOption;
					}
				}
				else
				{
					IToken token2 = this.LT(1);
					this.match(189);
					if (this.inputState.guessing == 0)
					{
						fileGrowthFileDeclarationOption.Units = MemoryUnit.Percent;
						TSql80ParserBaseInternal.UpdateTokenInfo(fileGrowthFileDeclarationOption, token2);
						return fileGrowthFileDeclarationOption;
					}
					return fileGrowthFileDeclarationOption;
				}
			}
			else
			{
				if (num == 198)
				{
					return fileGrowthFileDeclarationOption;
				}
				if (num == 232)
				{
					MemoryUnit memoryUnit = this.memUnit();
					if (this.inputState.guessing == 0)
					{
						fileGrowthFileDeclarationOption.Units = memoryUnit;
						return fileGrowthFileDeclarationOption;
					}
					return fileGrowthFileDeclarationOption;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x000A8124 File Offset: 0x000A6324
		public NameFileDeclarationOption newNameFileOption()
		{
			NameFileDeclarationOption nameFileDeclarationOption = base.FragmentFactory.CreateFragment<NameFileDeclarationOption>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(206);
			IdentifierOrValueExpression identifierOrValueExpression = this.nonEmptyStringOrIdentifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "NEWNAME");
				nameFileDeclarationOption.OptionKind = FileDeclarationOptionKind.NewName;
				TSql80ParserBaseInternal.UpdateTokenInfo(nameFileDeclarationOption, token);
				nameFileDeclarationOption.LogicalFileName = identifierOrValueExpression;
				nameFileDeclarationOption.IsNewName = true;
			}
			return nameFileDeclarationOption;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000A819C File Offset: 0x000A639C
		public IdentifierOrValueExpression nonEmptyStringOrIdentifier()
		{
			IdentifierOrValueExpression identifierOrValueExpression = base.FragmentFactory.CreateFragment<IdentifierOrValueExpression>();
			switch (this.LA(1))
			{
			case 230:
			case 231:
			{
				Literal literal = this.nonEmptyString();
				if (this.inputState.guessing == 0)
				{
					identifierOrValueExpression.ValueExpression = literal;
				}
				break;
			}
			case 232:
			case 233:
			{
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					identifierOrValueExpression.Identifier = identifier;
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return identifierOrValueExpression;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x000A822C File Offset: 0x000A642C
		public StringLiteral nonEmptyString()
		{
			StringLiteral stringLiteral = this.stringLiteral();
			if (this.inputState.guessing == 0 && (stringLiteral.Value == null || stringLiteral.Value.Length == 0))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46063", stringLiteral, TSqlParserResource.SQL46063Message, new string[0]);
			}
			return stringLiteral;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x000A827C File Offset: 0x000A647C
		public MemoryUnit memUnit()
		{
			MemoryUnit memoryUnit = MemoryUnit.Unspecified;
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				memoryUnit = MemoryUnitsHelper.Instance.ParseOption(token);
			}
			return memoryUnit;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000A82BC File Offset: 0x000A64BC
		public IToken backupStart()
		{
			IToken token = null;
			int num = this.LA(1);
			if (num != 12)
			{
				if (num != 180)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token2 = this.LT(1);
				this.match(180);
				if (this.inputState.guessing == 0)
				{
					token = token2;
				}
			}
			else
			{
				IToken token3 = this.LT(1);
				this.match(12);
				if (this.inputState.guessing == 0)
				{
					token = token3;
				}
			}
			return token;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000A8340 File Offset: 0x000A6540
		public BackupStatement backupMain()
		{
			int num = this.LA(1);
			BackupStatement backupStatement;
			if (num != 43)
			{
				switch (num)
				{
				case 153:
				case 154:
					break;
				default:
					if (num != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					break;
				}
				backupStatement = this.backupTransactionLog();
			}
			else
			{
				backupStatement = this.backupDatabase();
			}
			return backupStatement;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000A83A0 File Offset: 0x000A65A0
		public void devList(TSqlFragment vParent, IList<DeviceInfo> deviceInfos)
		{
			DeviceInfo deviceInfo = this.deviceInfo();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DeviceInfo>(vParent, deviceInfos, deviceInfo);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				deviceInfo = this.deviceInfo();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DeviceInfo>(vParent, deviceInfos, deviceInfo);
				}
			}
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000A8400 File Offset: 0x000A6600
		public void backupOptions(BackupStatement vParent)
		{
			this.match(171);
			BackupOption backupOption = this.backupOption();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BackupOption>(vParent, vParent.Options, backupOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				backupOption = this.backupOption();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BackupOption>(vParent, vParent.Options, backupOption);
				}
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000A8478 File Offset: 0x000A6678
		public IToken restoreStart()
		{
			IToken token = null;
			int num = this.LA(1);
			if (num != 129)
			{
				if (num != 181)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token2 = this.LT(1);
				this.match(181);
				if (this.inputState.guessing == 0)
				{
					token = token2;
				}
			}
			else
			{
				IToken token3 = this.LT(1);
				this.match(129);
				if (this.inputState.guessing == 0)
				{
					token = token3;
				}
			}
			return token;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000A8504 File Offset: 0x000A6704
		public void restoreMain(RestoreStatement vParent)
		{
			int num = this.LA(1);
			if (num == 43)
			{
				this.match(43);
				IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrVariable();
				if (this.inputState.guessing == 0)
				{
					vParent.DatabaseName = identifierOrValueExpression;
					vParent.Kind = RestoreStatementKind.Database;
					base.ThrowPartialAstIfPhaseOne(vParent);
				}
				this.restoreFileListOpt(vParent);
				return;
			}
			switch (num)
			{
			case 153:
			case 154:
			{
				switch (this.LA(1))
				{
				case 153:
					this.match(153);
					break;
				case 154:
					this.match(154);
					break;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrVariable();
				if (this.inputState.guessing == 0)
				{
					vParent.DatabaseName = identifierOrValueExpression;
					vParent.Kind = RestoreStatementKind.TransactionLog;
					base.ThrowPartialAstIfPhaseOne(vParent);
					return;
				}
				return;
			}
			default:
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrVariable();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "LOG");
					vParent.DatabaseName = identifierOrValueExpression;
					vParent.Kind = RestoreStatementKind.TransactionLog;
					base.ThrowPartialAstIfPhaseOne(vParent);
				}
				this.restoreFileListOpt(vParent);
				return;
			}
			}
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000A8654 File Offset: 0x000A6854
		public void restoreOptions(RestoreStatement vParent)
		{
			this.match(171);
			this.restoreOptionsList(vParent);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000A8668 File Offset: 0x000A6868
		public BackupDatabaseStatement backupDatabase()
		{
			BackupDatabaseStatement backupDatabaseStatement = base.FragmentFactory.CreateFragment<BackupDatabaseStatement>();
			this.match(43);
			IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrVariable();
			if (this.inputState.guessing == 0)
			{
				backupDatabaseStatement.DatabaseName = identifierOrValueExpression;
				base.ThrowPartialAstIfPhaseOne(backupDatabaseStatement);
			}
			this.backupFileListOpt(backupDatabaseStatement);
			return backupDatabaseStatement;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x000A86B4 File Offset: 0x000A68B4
		public BackupTransactionLogStatement backupTransactionLog()
		{
			BackupTransactionLogStatement backupTransactionLogStatement = base.FragmentFactory.CreateFragment<BackupTransactionLogStatement>();
			int num = this.LA(1);
			switch (num)
			{
			case 153:
				this.match(153);
				break;
			case 154:
				this.match(154);
				break;
			default:
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "LOG");
				}
				break;
			}
			}
			IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrVariable();
			if (this.inputState.guessing == 0)
			{
				backupTransactionLogStatement.DatabaseName = identifierOrValueExpression;
				base.ThrowPartialAstIfPhaseOne(backupTransactionLogStatement);
			}
			return backupTransactionLogStatement;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000A8774 File Offset: 0x000A6974
		public IdentifierOrValueExpression identifierOrVariable()
		{
			IdentifierOrValueExpression identifierOrValueExpression = base.FragmentFactory.CreateFragment<IdentifierOrValueExpression>();
			switch (this.LA(1))
			{
			case 232:
			case 233:
			{
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					identifierOrValueExpression.Identifier = identifier;
				}
				break;
			}
			case 234:
			{
				ValueExpression valueExpression = this.variable();
				if (this.inputState.guessing == 0)
				{
					identifierOrValueExpression.ValueExpression = valueExpression;
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return identifierOrValueExpression;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000A8800 File Offset: 0x000A6A00
		public void backupFileListOpt(BackupDatabaseStatement vParent)
		{
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num != 1 && num != 6)
						{
							goto IL_02C9;
						}
						return;
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return;
						case 14:
						case 16:
							goto IL_02C9;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return;
							default:
								if (num != 28)
								{
									goto IL_02C9;
								}
								return;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return;
					case 34:
						goto IL_02C9;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							goto IL_02C9;
						case 65:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return;
							default:
								goto IL_02C9;
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (num != 82 && num != 86 && num != 92)
					{
						goto IL_02C9;
					}
					return;
				}
			}
			else if (num <= 151)
			{
				if (num <= 106)
				{
					if (num != 95 && num != 106)
					{
						goto IL_02C9;
					}
					return;
				}
				else
				{
					if (num == 119)
					{
						return;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						goto IL_02C9;
					default:
						if (num != 151)
						{
							goto IL_02C9;
						}
						return;
					}
				}
			}
			else if (num <= 191)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return;
				case 157:
				case 158:
				case 159:
					goto IL_02C9;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 171:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return;
					case 168:
					case 169:
					case 174:
					case 175:
					case 177:
					case 178:
					case 179:
						goto IL_02C9;
					default:
						if (num != 191)
						{
							goto IL_02C9;
						}
						return;
					}
					break;
				}
			}
			else
			{
				if (num == 204)
				{
					return;
				}
				switch (num)
				{
				case 219:
				case 220:
					return;
				default:
					if (num != 232)
					{
						goto IL_02C9;
					}
					break;
				}
			}
			BackupRestoreFileInfo backupRestoreFileInfo = this.backupRestoreFile();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BackupRestoreFileInfo>(vParent, vParent.Files, backupRestoreFileInfo);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				backupRestoreFileInfo = this.backupRestoreFile();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BackupRestoreFileInfo>(vParent, vParent.Files, backupRestoreFileInfo);
				}
			}
			return;
			IL_02C9:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000A8AEC File Offset: 0x000A6CEC
		public BackupRestoreFileInfo backupRestoreFile()
		{
			BackupRestoreFileInfo backupRestoreFileInfo = base.FragmentFactory.CreateFragment<BackupRestoreFileInfo>();
			int num = this.LA(1);
			if (num != 65)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				this.match(206);
				int num2 = this.LA(1);
				if (num2 != 191)
				{
					switch (num2)
					{
					case 230:
					case 231:
					case 234:
					{
						ValueExpression valueExpression = this.stringOrVariable();
						if (this.inputState.guessing == 0)
						{
							if (TSql80ParserBaseInternal.TryMatch(token, "PAGE"))
							{
								backupRestoreFileInfo.ItemKind = BackupRestoreItemKind.Page;
							}
							else
							{
								TSql80ParserBaseInternal.Match(token, "FILEGROUP");
								backupRestoreFileInfo.ItemKind = BackupRestoreItemKind.FileGroups;
							}
							TSql80ParserBaseInternal.UpdateTokenInfo(backupRestoreFileInfo, token);
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ValueExpression>(backupRestoreFileInfo, backupRestoreFileInfo.Items, valueExpression);
							return backupRestoreFileInfo;
						}
						return backupRestoreFileInfo;
					}
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.backupRestoreFileNameList(backupRestoreFileInfo);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "FILEGROUP");
					backupRestoreFileInfo.ItemKind = BackupRestoreItemKind.FileGroups;
				}
			}
			else
			{
				this.LT(1);
				this.match(65);
				this.match(206);
				if (this.inputState.guessing == 0)
				{
					backupRestoreFileInfo.ItemKind = BackupRestoreItemKind.Files;
				}
				int num3 = this.LA(1);
				if (num3 != 191)
				{
					switch (num3)
					{
					case 230:
					case 231:
					case 234:
					{
						ValueExpression valueExpression = this.stringOrVariable();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ValueExpression>(backupRestoreFileInfo, backupRestoreFileInfo.Items, valueExpression);
							return backupRestoreFileInfo;
						}
						return backupRestoreFileInfo;
					}
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.backupRestoreFileNameList(backupRestoreFileInfo);
			}
			return backupRestoreFileInfo;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000A8CC8 File Offset: 0x000A6EC8
		public void restoreFileListOpt(RestoreStatement vParent)
		{
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num != 1 && num != 6)
						{
							goto IL_02C7;
						}
						return;
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return;
						case 14:
						case 16:
							goto IL_02C7;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return;
							default:
								if (num != 28)
								{
									goto IL_02C7;
								}
								return;
							}
							break;
						}
					}
				}
				else if (num <= 65)
				{
					switch (num)
					{
					case 33:
					case 35:
						return;
					case 34:
						goto IL_02C7;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							goto IL_02C7;
						case 65:
							break;
						default:
							goto IL_02C7;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 71:
					case 74:
					case 75:
						return;
					case 72:
					case 73:
						goto IL_02C7;
					default:
						if (num != 82 && num != 86)
						{
							goto IL_02C7;
						}
						return;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num != 92 && num != 95)
					{
						goto IL_02C7;
					}
					return;
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						goto IL_02C7;
					default:
						goto IL_02C7;
					}
				}
			}
			else if (num <= 191)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return;
				case 157:
				case 158:
				case 159:
					goto IL_02C7;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 171:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return;
					case 168:
					case 169:
					case 174:
					case 175:
					case 177:
					case 178:
					case 179:
						goto IL_02C7;
					default:
						if (num != 191)
						{
							goto IL_02C7;
						}
						return;
					}
					break;
				}
			}
			else
			{
				if (num == 204)
				{
					return;
				}
				switch (num)
				{
				case 219:
				case 220:
					return;
				default:
					if (num != 232)
					{
						goto IL_02C7;
					}
					break;
				}
			}
			BackupRestoreFileInfo backupRestoreFileInfo = this.backupRestoreFile();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BackupRestoreFileInfo>(vParent, vParent.Files, backupRestoreFileInfo);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				backupRestoreFileInfo = this.backupRestoreFile();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BackupRestoreFileInfo>(vParent, vParent.Files, backupRestoreFileInfo);
				}
			}
			return;
			IL_02C7:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000A8FB0 File Offset: 0x000A71B0
		public ValueExpression stringOrVariable()
		{
			switch (this.LA(1))
			{
			case 230:
			case 231:
				return this.stringLiteral();
			case 234:
				return this.variable();
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000A9010 File Offset: 0x000A7210
		public void backupRestoreFileNameList(BackupRestoreFileInfo vParent)
		{
			this.LT(1);
			this.match(191);
			ValueExpression valueExpression = this.stringOrVariable();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ValueExpression>(vParent, vParent.Items, valueExpression);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				valueExpression = this.stringOrVariable();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ValueExpression>(vParent, vParent.Items, valueExpression);
				}
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000A90B8 File Offset: 0x000A72B8
		public DeviceInfo deviceInfo()
		{
			DeviceInfo deviceInfo = base.FragmentFactory.CreateFragment<DeviceInfo>();
			if (this.LA(1) >= 232 && this.LA(1) <= 234 && TSql80ParserInternal.tokenSet_19_.member(this.LA(2)))
			{
				IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrVariable();
				if (this.inputState.guessing == 0)
				{
					deviceInfo.LogicalDevice = identifierOrValueExpression;
				}
			}
			else
			{
				if ((this.LA(1) != 173 && this.LA(1) != 232) || this.LA(2) != 206)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				int num = this.LA(1);
				if (num != 173)
				{
					if (num != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					IToken token = this.LT(1);
					this.match(232);
					if (this.inputState.guessing == 0)
					{
						deviceInfo.DeviceType = DeviceTypesHelper.Instance.ParseOption(token);
					}
				}
				else
				{
					this.match(173);
					if (this.inputState.guessing == 0)
					{
						deviceInfo.DeviceType = DeviceType.Disk;
					}
				}
				this.match(206);
				ValueExpression valueExpression = this.stringOrVariable();
				if (this.inputState.guessing == 0)
				{
					deviceInfo.PhysicalDevice = valueExpression;
				}
			}
			return deviceInfo;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000A9214 File Offset: 0x000A7414
		public BackupOption backupOption()
		{
			BackupOption backupOption = base.FragmentFactory.CreateFragment<BackupOption>();
			if (this.LA(1) == 232 && TSql80ParserInternal.tokenSet_21_.member(this.LA(2)))
			{
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					backupOption.OptionKind = BackupOptionsNoValueHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
					TSql80ParserBaseInternal.UpdateTokenInfo(backupOption, token);
				}
			}
			else
			{
				if (this.LA(1) != 232 || this.LA(2) != 206)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token2 = this.LT(1);
				this.match(232);
				this.match(206);
				int num = this.LA(1);
				ScalarExpression scalarExpression;
				if (num != 199 && num != 221)
				{
					switch (num)
					{
					case 230:
					case 231:
						scalarExpression = this.stringLiteral();
						goto IL_0115;
					case 234:
						goto IL_00F0;
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IL_00F0:
				scalarExpression = this.signedIntegerOrVariable();
				IL_0115:
				if (this.inputState.guessing == 0)
				{
					backupOption.OptionKind = BackupOptionsWithValueHelper.Instance.ParseOption(token2, SqlVersionFlags.TSql80);
					backupOption.Value = scalarExpression;
				}
			}
			return backupOption;
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000A9374 File Offset: 0x000A7574
		public ScalarExpression signedIntegerOrVariable()
		{
			int num = this.LA(1);
			ScalarExpression scalarExpression;
			if (num != 199 && num != 221)
			{
				if (num != 234)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				scalarExpression = this.variable();
			}
			else
			{
				scalarExpression = this.signedInteger();
			}
			return scalarExpression;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000A93CC File Offset: 0x000A75CC
		public StringLiteral stringLiteral()
		{
			StringLiteral stringLiteral = base.FragmentFactory.CreateFragment<StringLiteral>();
			switch (this.LA(1))
			{
			case 230:
			{
				IToken token = this.LT(1);
				this.match(230);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(stringLiteral, token);
					stringLiteral.Value = TSql80ParserBaseInternal.DecodeAsciiStringLiteral(token.getText());
					stringLiteral.IsLargeObject = TSql80ParserBaseInternal.IsAsciiStringLob(stringLiteral.Value);
				}
				break;
			}
			case 231:
			{
				IToken token2 = this.LT(1);
				this.match(231);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(stringLiteral, token2);
					stringLiteral.IsNational = true;
					stringLiteral.Value = TSql80ParserBaseInternal.DecodeUnicodeStringLiteral(token2.getText());
					stringLiteral.IsLargeObject = TSql80ParserBaseInternal.IsUnicodeStringLob(stringLiteral.Value);
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return stringLiteral;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000A94C0 File Offset: 0x000A76C0
		public void restoreOptionsList(RestoreStatement vParent)
		{
			RestoreOption restoreOption = this.restoreOption();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<RestoreOption>(vParent, vParent.Options, restoreOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				restoreOption = this.restoreOption();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<RestoreOption>(vParent, vParent.Options, restoreOption);
				}
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000A952C File Offset: 0x000A772C
		public RestoreOption restoreOption()
		{
			RestoreOption restoreOption = null;
			if (this.LA(1) == 232 && TSql80ParserInternal.tokenSet_21_.member(this.LA(2)))
			{
				restoreOption = this.simpleRestoreOption();
			}
			else if (this.LA(1) == 232 && this.LA(2) == 206)
			{
				IToken token = this.LT(1);
				this.match(232);
				this.match(206);
				if ((this.LA(1) == 230 || this.LA(1) == 231 || this.LA(1) == 234) && this.LA(2) == 232 && TSql80ParserBaseInternal.IsStopAtBeforeMarkRestoreOption(token))
				{
					ValueExpression valueExpression = this.stringOrVariable();
					ValueExpression valueExpression2 = this.afterClause();
					if (this.inputState.guessing == 0)
					{
						restoreOption = base.CreateStopRestoreOption(token, valueExpression, valueExpression2);
					}
				}
				else if (this.LA(1) == 199 || this.LA(1) == 221)
				{
					ScalarExpression scalarExpression = this.signedInteger();
					if (this.inputState.guessing == 0)
					{
						restoreOption = base.CreateSimpleRestoreOptionWithValue(token, scalarExpression);
					}
				}
				else
				{
					if ((this.LA(1) != 230 && this.LA(1) != 231 && this.LA(1) != 234) || !TSql80ParserInternal.tokenSet_21_.member(this.LA(2)))
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					ValueExpression valueExpression = this.stringOrVariable();
					if (this.inputState.guessing == 0)
					{
						if (TSql80ParserBaseInternal.IsStopAtBeforeMarkRestoreOption(token))
						{
							restoreOption = base.CreateStopRestoreOption(token, valueExpression, null);
						}
						else
						{
							restoreOption = base.CreateSimpleRestoreOptionWithValue(token, valueExpression);
						}
					}
				}
			}
			else if (this.LA(1) == 232 && (this.LA(2) == 230 || this.LA(2) == 231 || this.LA(2) == 234))
			{
				restoreOption = this.moveRestoreOption();
			}
			else
			{
				if (this.LA(1) != 65)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				restoreOption = this.fileRestoreOption();
			}
			return restoreOption;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000A974C File Offset: 0x000A794C
		public RestoreOption simpleRestoreOption()
		{
			RestoreOption restoreOption = base.FragmentFactory.CreateFragment<RestoreOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				restoreOption.OptionKind = RestoreOptionNoValueHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
				TSql80ParserBaseInternal.UpdateTokenInfo(restoreOption, token);
			}
			return restoreOption;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x000A97A4 File Offset: 0x000A79A4
		public ValueExpression afterClause()
		{
			IToken token = this.LT(1);
			this.match(232);
			ValueExpression valueExpression = this.stringOrVariable();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "AFTER");
			}
			return valueExpression;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000A97E8 File Offset: 0x000A79E8
		public ScalarExpression signedInteger()
		{
			ScalarExpression scalarExpression = null;
			UnaryExpression unaryExpression = null;
			int num = this.LA(1);
			if (num != 199)
			{
				if (num != 221)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				IToken token = this.LT(1);
				this.match(199);
				if (this.inputState.guessing == 0)
				{
					unaryExpression = base.FragmentFactory.CreateFragment<UnaryExpression>();
					TSql80ParserBaseInternal.UpdateTokenInfo(unaryExpression, token);
					unaryExpression.UnaryExpressionType = UnaryExpressionType.Negative;
				}
			}
			Literal literal = this.integer();
			if (this.inputState.guessing == 0)
			{
				if (unaryExpression == null)
				{
					scalarExpression = literal;
				}
				else
				{
					unaryExpression.Expression = literal;
					scalarExpression = unaryExpression;
				}
			}
			return scalarExpression;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x000A988C File Offset: 0x000A7A8C
		public MoveRestoreOption moveRestoreOption()
		{
			MoveRestoreOption moveRestoreOption = base.FragmentFactory.CreateFragment<MoveRestoreOption>();
			IToken token = this.LT(1);
			this.match(232);
			ValueExpression valueExpression = this.stringOrVariable();
			this.match(151);
			ValueExpression valueExpression2 = this.stringOrVariable();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "MOVE");
				moveRestoreOption.OptionKind = RestoreOptionKind.Move;
				moveRestoreOption.LogicalFileName = valueExpression;
				moveRestoreOption.OSFileName = valueExpression2;
			}
			return moveRestoreOption;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000A9904 File Offset: 0x000A7B04
		public ScalarExpressionRestoreOption fileRestoreOption()
		{
			ScalarExpressionRestoreOption scalarExpressionRestoreOption = base.FragmentFactory.CreateFragment<ScalarExpressionRestoreOption>();
			this.match(65);
			this.match(206);
			ScalarExpression scalarExpression = this.signedIntegerOrVariable();
			if (this.inputState.guessing == 0)
			{
				scalarExpressionRestoreOption.OptionKind = RestoreOptionKind.File;
				scalarExpressionRestoreOption.Value = scalarExpression;
			}
			return scalarExpressionRestoreOption;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000A9954 File Offset: 0x000A7B54
		public SchemaObjectName schemaObjectThreePartName()
		{
			SchemaObjectName schemaObjectName = base.FragmentFactory.CreateFragment<SchemaObjectName>();
			List<Identifier> list = this.identifierList(3);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(schemaObjectName, schemaObjectName.Identifiers, list);
			}
			return schemaObjectName;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x000A9990 File Offset: 0x000A7B90
		public IdentifierOrValueExpression bulkInsertFrom()
		{
			IdentifierOrValueExpression identifierOrValueExpression = null;
			int num = this.LA(1);
			if (num != 221)
			{
				switch (num)
				{
				case 230:
				case 231:
				case 232:
				case 233:
					identifierOrValueExpression = this.stringOrIdentifier();
					break;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				Literal literal = this.integer();
				if (this.inputState.guessing == 0)
				{
					identifierOrValueExpression = base.IdentifierOrValueExpression(literal);
				}
			}
			return identifierOrValueExpression;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x000A9A08 File Offset: 0x000A7C08
		public void bulkInsertOptions(BulkInsertStatement vParent)
		{
			int num = 0;
			this.match(171);
			this.match(191);
			BulkInsertOption bulkInsertOption = this.bulkInsertOption();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)bulkInsertOption.OptionKind, bulkInsertOption);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BulkInsertOption>(vParent, vParent.Options, bulkInsertOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				bulkInsertOption = this.bulkInsertOption();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)bulkInsertOption.OptionKind, bulkInsertOption);
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BulkInsertOption>(vParent, vParent.Options, bulkInsertOption);
				}
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000A9AD0 File Offset: 0x000A7CD0
		public IdentifierOrValueExpression stringOrIdentifier()
		{
			IdentifierOrValueExpression identifierOrValueExpression = base.FragmentFactory.CreateFragment<IdentifierOrValueExpression>();
			switch (this.LA(1))
			{
			case 230:
			case 231:
			{
				Literal literal = this.stringLiteral();
				if (this.inputState.guessing == 0)
				{
					identifierOrValueExpression.ValueExpression = literal;
				}
				break;
			}
			case 232:
			case 233:
			{
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					identifierOrValueExpression.Identifier = identifier;
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return identifierOrValueExpression;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000A9B60 File Offset: 0x000A7D60
		public BulkInsertOption bulkInsertOption()
		{
			BulkInsertOption bulkInsertOption;
			if (this.LA(1) == 113)
			{
				bulkInsertOption = this.bulkInsertSortOrderOption();
			}
			else if (this.LA(1) == 232 && this.LA(2) == 206)
			{
				bulkInsertOption = this.simpleBulkInsertOptionWithValue();
			}
			else
			{
				if (this.LA(1) != 232 || (this.LA(2) != 192 && this.LA(2) != 198))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				bulkInsertOption = this.simpleBulkInsertOptionNoValue();
			}
			return bulkInsertOption;
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000A9BF0 File Offset: 0x000A7DF0
		public OrderBulkInsertOption bulkInsertSortOrderOption()
		{
			OrderBulkInsertOption orderBulkInsertOption = base.FragmentFactory.CreateFragment<OrderBulkInsertOption>();
			IToken token = this.LT(1);
			this.match(113);
			this.match(191);
			ColumnWithSortOrder columnWithSortOrder = this.columnWithSortOrder();
			if (this.inputState.guessing == 0)
			{
				orderBulkInsertOption.OptionKind = BulkInsertOptionKind.Order;
				TSql80ParserBaseInternal.UpdateTokenInfo(orderBulkInsertOption, token);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnWithSortOrder>(orderBulkInsertOption, orderBulkInsertOption.Columns, columnWithSortOrder);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				columnWithSortOrder = this.columnWithSortOrder();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnWithSortOrder>(orderBulkInsertOption, orderBulkInsertOption.Columns, columnWithSortOrder);
				}
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(orderBulkInsertOption, token2);
			}
			return orderBulkInsertOption;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000A9CBC File Offset: 0x000A7EBC
		public LiteralBulkInsertOption simpleBulkInsertOptionWithValue()
		{
			LiteralBulkInsertOption literalBulkInsertOption = base.FragmentFactory.CreateFragment<LiteralBulkInsertOption>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(206);
			int num = this.LA(1);
			switch (num)
			{
			case 221:
			case 222:
			{
				Literal literal = this.integerOrNumeric();
				if (this.inputState.guessing == 0)
				{
					literalBulkInsertOption.OptionKind = BulkInsertIntOptionsHelper.Instance.ParseOption(token);
					TSql80ParserBaseInternal.UpdateTokenInfo(literalBulkInsertOption, token);
					literalBulkInsertOption.Value = literal;
				}
				break;
			}
			default:
				switch (num)
				{
				case 230:
				case 231:
				{
					Literal literal = this.stringLiteral();
					if (this.inputState.guessing == 0)
					{
						literalBulkInsertOption.OptionKind = BulkInsertStringOptionsHelper.Instance.ParseOption(token);
						if (literalBulkInsertOption.OptionKind == BulkInsertOptionKind.CodePage)
						{
							TSql80ParserBaseInternal.MatchString(literal, new string[] { "ACP", "OEM", "RAW" });
						}
						else if (literalBulkInsertOption.OptionKind == BulkInsertOptionKind.DataFileType)
						{
							TSql80ParserBaseInternal.MatchString(literal, new string[] { "CHAR", "NATIVE", "WIDECHAR", "WIDENATIVE", "WIDECHAR_ANSI", "DTS_BUFFERS" });
						}
						TSql80ParserBaseInternal.UpdateTokenInfo(literalBulkInsertOption, token);
						literalBulkInsertOption.Value = literal;
					}
					break;
				}
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				break;
			}
			return literalBulkInsertOption;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000A9E38 File Offset: 0x000A8038
		public BulkInsertOption simpleBulkInsertOptionNoValue()
		{
			BulkInsertOption bulkInsertOption = base.FragmentFactory.CreateFragment<BulkInsertOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				bulkInsertOption.OptionKind = BulkInsertFlagOptionsHelper.Instance.ParseOption(token);
				TSql80ParserBaseInternal.UpdateTokenInfo(bulkInsertOption, token);
			}
			return bulkInsertOption;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000A9E8C File Offset: 0x000A808C
		public BulkInsertOption insertBulkOption()
		{
			int num = this.LA(1);
			BulkInsertOption bulkInsertOption;
			if (num != 113)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				bulkInsertOption = this.simpleInsertBulkOption();
			}
			else
			{
				bulkInsertOption = this.bulkInsertSortOrderOption();
			}
			return bulkInsertOption;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x000A9ED8 File Offset: 0x000A80D8
		public BulkInsertOption simpleInsertBulkOption()
		{
			BulkInsertOption bulkInsertOption = null;
			IToken token = this.LT(1);
			this.match(232);
			int num = this.LA(1);
			if (num != 192 && num != 198)
			{
				if (num != 206)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(206);
				Literal literal = this.integerOrNumeric();
				if (this.inputState.guessing == 0)
				{
					LiteralBulkInsertOption literalBulkInsertOption = base.FragmentFactory.CreateFragment<LiteralBulkInsertOption>();
					TSql80ParserBaseInternal.UpdateTokenInfo(literalBulkInsertOption, token);
					if (TSql80ParserBaseInternal.TryMatch(token, "ROWS_PER_BATCH"))
					{
						literalBulkInsertOption.OptionKind = BulkInsertOptionKind.RowsPerBatch;
					}
					else
					{
						TSql80ParserBaseInternal.Match(token, "KILOBYTES_PER_BATCH");
						literalBulkInsertOption.OptionKind = BulkInsertOptionKind.KilobytesPerBatch;
					}
					literalBulkInsertOption.Value = literal;
					bulkInsertOption = literalBulkInsertOption;
				}
			}
			else if (this.inputState.guessing == 0)
			{
				bulkInsertOption = base.FragmentFactory.CreateFragment<BulkInsertOption>();
				bulkInsertOption.OptionKind = BulkInsertFlagOptionsHelper.Instance.ParseOption(token);
				TSql80ParserBaseInternal.UpdateTokenInfo(bulkInsertOption, token);
				if (bulkInsertOption.OptionKind == BulkInsertOptionKind.KeepIdentity)
				{
					throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
				}
			}
			return bulkInsertOption;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000A9FE4 File Offset: 0x000A81E4
		public Literal integerOrNumeric()
		{
			Literal literal;
			switch (this.LA(1))
			{
			case 221:
				literal = this.integer();
				break;
			case 222:
				literal = this.numeric();
				break;
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return literal;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000AA038 File Offset: 0x000A8238
		public void coldefList(InsertBulkStatement vParent)
		{
			this.match(191);
			InsertBulkColumnDefinition insertBulkColumnDefinition = this.coldefItem();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<InsertBulkColumnDefinition>(vParent, vParent.ColumnDefinitions, insertBulkColumnDefinition);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				insertBulkColumnDefinition = this.coldefItem();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<InsertBulkColumnDefinition>(vParent, vParent.ColumnDefinitions, insertBulkColumnDefinition);
				}
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000AA0D8 File Offset: 0x000A82D8
		public void insertBulkOptions(InsertBulkStatement vParent)
		{
			int num = 0;
			this.match(171);
			this.match(191);
			BulkInsertOption bulkInsertOption = this.insertBulkOption();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)bulkInsertOption.OptionKind, bulkInsertOption);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BulkInsertOption>(vParent, vParent.Options, bulkInsertOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				bulkInsertOption = this.insertBulkOption();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)bulkInsertOption.OptionKind, bulkInsertOption);
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<BulkInsertOption>(vParent, vParent.Options, bulkInsertOption);
				}
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000AA1A0 File Offset: 0x000A83A0
		public InsertBulkColumnDefinition coldefItem()
		{
			InsertBulkColumnDefinition insertBulkColumnDefinition = base.FragmentFactory.CreateFragment<InsertBulkColumnDefinition>();
			ColumnDefinitionBase columnDefinitionBase = this.columnDefinitionEx();
			if (this.inputState.guessing == 0)
			{
				insertBulkColumnDefinition.Column = columnDefinitionBase;
			}
			int num = this.LA(1);
			switch (num)
			{
			case 99:
			case 100:
			{
				bool flag = this.nullNotNull(insertBulkColumnDefinition);
				if (this.inputState.guessing == 0)
				{
					insertBulkColumnDefinition.NullNotNull = (flag ? NullNotNull.Null : NullNotNull.NotNull);
				}
				break;
			}
			default:
				if (num != 192 && num != 198)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				break;
			}
			return insertBulkColumnDefinition;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000AA238 File Offset: 0x000A8438
		public ColumnDefinitionBase columnDefinitionEx()
		{
			ColumnDefinitionBase columnDefinitionBase = null;
			if ((this.LA(1) == 232 || this.LA(1) == 233) && TSql80ParserInternal.tokenSet_22_.member(this.LA(2)))
			{
				columnDefinitionBase = this.columnDefinitionBasic();
			}
			else
			{
				if (this.LA(1) != 232 || !TSql80ParserInternal.tokenSet_23_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "TIMESTAMP");
					columnDefinitionBase = base.FragmentFactory.CreateFragment<ColumnDefinitionBase>();
					Identifier identifier = base.FragmentFactory.CreateFragment<Identifier>();
					TSql80ParserBaseInternal.UpdateTokenInfo(identifier, token);
					identifier.SetUnquotedIdentifier("TIMESTAMP");
					columnDefinitionBase.ColumnIdentifier = identifier;
				}
			}
			return columnDefinitionBase;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000AA318 File Offset: 0x000A8518
		public bool nullNotNull(TSqlFragment vParent)
		{
			bool flag = true;
			switch (this.LA(1))
			{
			case 99:
			{
				IToken token = this.LT(1);
				this.match(99);
				if (this.inputState.guessing == 0)
				{
					flag = false;
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
				}
				break;
			}
			case 100:
				break;
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			IToken token2 = this.LT(1);
			this.match(100);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
			}
			return flag;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000AA3A8 File Offset: 0x000A85A8
		public ColumnWithSortOrder columnWithSortOrder()
		{
			ColumnWithSortOrder columnWithSortOrder = base.FragmentFactory.CreateFragment<ColumnWithSortOrder>();
			ColumnReferenceExpression columnReferenceExpression = this.identifierColumnReferenceExpression();
			if (this.inputState.guessing == 0)
			{
				columnWithSortOrder.Column = columnReferenceExpression;
			}
			int num = this.LA(1);
			if (num <= 50)
			{
				if (num == 10 || num == 50)
				{
					SortOrder sortOrder = this.orderByOption(columnWithSortOrder);
					if (this.inputState.guessing == 0)
					{
						columnWithSortOrder.SortOrder = sortOrder;
						return columnWithSortOrder;
					}
					return columnWithSortOrder;
				}
			}
			else if (num == 192 || num == 198)
			{
				return columnWithSortOrder;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000AA43C File Offset: 0x000A863C
		public void dbccNamedLiteralList(DbccStatement vParent)
		{
			IToken token = this.LT(1);
			this.match(191);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
			int num = this.LA(1);
			if (num <= 193)
			{
				if (num != 100)
				{
					switch (num)
					{
					case 192:
						if (this.inputState.guessing == 0)
						{
							vParent.ParenthesisRequired = true;
							goto IL_012D;
						}
						goto IL_012D;
					case 193:
						break;
					default:
						goto IL_011A;
					}
				}
			}
			else if (num != 199)
			{
				switch (num)
				{
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
					break;
				case 226:
				case 227:
				case 228:
				case 229:
					goto IL_011A;
				default:
					goto IL_011A;
				}
			}
			DbccNamedLiteral dbccNamedLiteral = this.dbccNamedLiteral();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DbccNamedLiteral>(vParent, vParent.Literals, dbccNamedLiteral);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				dbccNamedLiteral = this.dbccNamedLiteral();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DbccNamedLiteral>(vParent, vParent.Literals, dbccNamedLiteral);
				}
			}
			goto IL_012D;
			IL_011A:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_012D:
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000AA59D File Offset: 0x000A879D
		public void dbccOptions(DbccStatement vParent)
		{
			this.match(171);
			this.dbccOptionsList(vParent);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000AA5B4 File Offset: 0x000A87B4
		public void dbccOptionsList(DbccStatement vParent)
		{
			if (this.LA(1) == 232 && TSql80ParserInternal.tokenSet_21_.member(this.LA(2)))
			{
				this.dbccOptionsListItems(vParent);
				return;
			}
			if (this.LA(1) == 232 && this.LA(2) == 90)
			{
				this.dbccOptionsJoin(vParent);
				return;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000AA620 File Offset: 0x000A8820
		public void dbccOptionsListItems(DbccStatement vParent)
		{
			DbccOption dbccOption = this.dbccOption();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DbccOption>(vParent, vParent.Options, dbccOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				dbccOption = this.dbccOption();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DbccOption>(vParent, vParent.Options, dbccOption);
				}
			}
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000AA68C File Offset: 0x000A888C
		public void dbccOptionsJoin(DbccStatement vParent)
		{
			DbccOption dbccOption = this.dbccJoinOption();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DbccOption>(vParent, vParent.Options, dbccOption);
			}
			int num = 0;
			while (this.LA(1) == 90)
			{
				this.match(90);
				dbccOption = this.dbccJoinOption();
				if (this.inputState.guessing == 0)
				{
					vParent.OptionsUseJoin = true;
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DbccOption>(vParent, vParent.Options, dbccOption);
				}
				num++;
			}
			if (num >= 1)
			{
				return;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000AA718 File Offset: 0x000A8918
		public DbccOption dbccOption()
		{
			DbccOption dbccOption = base.FragmentFactory.CreateFragment<DbccOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				dbccOption.OptionKind = DbccOptionsHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
				TSql80ParserBaseInternal.UpdateTokenInfo(dbccOption, token);
			}
			return dbccOption;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000AA770 File Offset: 0x000A8970
		public DbccOption dbccJoinOption()
		{
			DbccOption dbccOption = base.FragmentFactory.CreateFragment<DbccOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				dbccOption.OptionKind = DbccJoinOptionsHelper.Instance.ParseOption(token);
				TSql80ParserBaseInternal.UpdateTokenInfo(dbccOption, token);
			}
			return dbccOption;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000AA7C4 File Offset: 0x000A89C4
		public DbccNamedLiteral dbccNamedLiteral()
		{
			DbccNamedLiteral dbccNamedLiteral = base.FragmentFactory.CreateFragment<DbccNamedLiteral>();
			if (this.LA(1) == 232 && this.LA(2) == 206)
			{
				IToken token = this.LT(1);
				this.match(232);
				this.match(206);
				if (this.inputState.guessing == 0)
				{
					dbccNamedLiteral.Name = token.getText();
					TSql80ParserBaseInternal.UpdateTokenInfo(dbccNamedLiteral, token);
				}
			}
			else if (!TSql80ParserInternal.tokenSet_24_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_25_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			ScalarExpression scalarExpression = this.possibleNegativeConstantOrIdentifier();
			if (this.inputState.guessing == 0)
			{
				dbccNamedLiteral.Value = scalarExpression;
			}
			return dbccNamedLiteral;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000AA890 File Offset: 0x000A8A90
		public void authorizationOpt(IAuthorization vParent)
		{
			int num = this.LA(1);
			if (num <= 11)
			{
				if (num == 1)
				{
					return;
				}
				if (num == 11)
				{
					this.authorization(vParent);
					return;
				}
			}
			else if (num == 35 || num == 75 || num == 219)
			{
				return;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x000AA8E4 File Offset: 0x000A8AE4
		public void authorization(IAuthorization vParent)
		{
			this.match(11);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				vParent.Owner = identifier;
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000AA914 File Offset: 0x000A8B14
		public StatementList createSchemaElementList()
		{
			StatementList statementList = base.FragmentFactory.CreateFragment<StatementList>();
			while (this.LA(1) == 35 || this.LA(1) == 75)
			{
				TSqlStatement tsqlStatement = this.createSchemaElement();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TSqlStatement>(statementList, statementList.Statements, tsqlStatement);
				}
			}
			return statementList;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000AA968 File Offset: 0x000A8B68
		public TSqlStatement createSchemaElement()
		{
			TSqlStatement tsqlStatement;
			if (this.LA(1) == 35 && this.LA(2) == 166)
			{
				tsqlStatement = this.createViewStatement();
			}
			else if (this.LA(1) == 35 && this.LA(2) == 148)
			{
				tsqlStatement = this.createTableStatement();
			}
			else
			{
				if (this.LA(1) != 75)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				tsqlStatement = this.grantStatement80();
			}
			return tsqlStatement;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000AA9E4 File Offset: 0x000A8BE4
		public void functionStatementBody(FunctionStatementBody vResult, out bool vParseErrorOccurred)
		{
			this.match(73);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckTwoPartNameForSchemaObjectName(schemaObjectName, "TRIGGER");
				vResult.Name = schemaObjectName;
				TSql80ParserBaseInternal.CheckForTemporaryFunction(schemaObjectName);
				base.ThrowPartialAstIfPhaseOne(vResult);
			}
			this.match(191);
			int num = this.LA(1);
			if (num != 192)
			{
				if (num != 234)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.functionParameterList(vResult);
			}
			this.match(192);
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "RETURNS");
			}
			this.functionReturnTypeAndBody(vResult, out vParseErrorOccurred);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000AAAAC File Offset: 0x000A8CAC
		public void functionParameterList(FunctionStatementBody vResult)
		{
			ProcedureParameter procedureParameter = this.functionParameter();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ProcedureParameter>(vResult, vResult.Parameters, procedureParameter);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				procedureParameter = this.functionParameter();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ProcedureParameter>(vResult, vResult.Parameters, procedureParameter);
				}
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000AAB18 File Offset: 0x000A8D18
		public void functionReturnTypeAndBody(FunctionStatementBody vParent, out bool vParseErrorOccurred)
		{
			vParseErrorOccurred = false;
			int num = this.LA(1);
			BeginEndBlockStatement beginEndBlockStatement;
			if (num <= 96)
			{
				if (num != 53 && num != 96)
				{
					goto IL_027F;
				}
			}
			else if (num != 148)
			{
				switch (num)
				{
				case 232:
				case 233:
					break;
				case 234:
				{
					DeclareTableVariableBody declareTableVariableBody = this.declareTableBody(IndexAffectingStatement.CreateOrAlterFunction);
					if (this.inputState.guessing == 0)
					{
						TableValuedFunctionReturnType tableValuedFunctionReturnType = base.FragmentFactory.CreateFragment<TableValuedFunctionReturnType>();
						tableValuedFunctionReturnType.DeclareTableVariableBody = declareTableVariableBody;
						vParent.ReturnType = tableValuedFunctionReturnType;
					}
					int num2 = this.LA(1);
					if (num2 != 9 && num2 != 13)
					{
						if (num2 != 171)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						this.functionAttributes(vParent);
					}
					int num3 = this.LA(1);
					if (num3 != 9)
					{
						if (num3 != 13)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
					}
					else
					{
						this.match(9);
					}
					beginEndBlockStatement = this.beginEndBlockStatement();
					if (this.inputState.guessing == 0)
					{
						base.SetFunctionBodyStatement(vParent, beginEndBlockStatement);
						vParseErrorOccurred = beginEndBlockStatement == null;
						return;
					}
					return;
				}
				default:
					goto IL_027F;
				}
			}
			else
			{
				this.match(148);
				int num4 = this.LA(1);
				if (num4 != 9 && num4 != 131)
				{
					if (num4 != 171)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.functionAttributes(vParent);
				}
				int num5 = this.LA(1);
				if (num5 != 9)
				{
					if (num5 != 131)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					this.match(9);
				}
				this.match(131);
				SelectFunctionReturnType selectFunctionReturnType = this.functionReturnClauseRelational();
				if (this.inputState.guessing == 0)
				{
					vParent.ReturnType = selectFunctionReturnType;
					return;
				}
				return;
			}
			DataTypeReference dataTypeReference = this.scalarDataType();
			if (this.inputState.guessing == 0)
			{
				ScalarFunctionReturnType scalarFunctionReturnType = base.FragmentFactory.CreateFragment<ScalarFunctionReturnType>();
				scalarFunctionReturnType.DataType = dataTypeReference;
				vParent.ReturnType = scalarFunctionReturnType;
			}
			int num6 = this.LA(1);
			if (num6 != 9 && num6 != 13)
			{
				if (num6 != 171)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.functionAttributes(vParent);
			}
			int num7 = this.LA(1);
			if (num7 != 9)
			{
				if (num7 != 13)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				this.match(9);
			}
			beginEndBlockStatement = this.beginEndBlockStatement();
			if (this.inputState.guessing == 0)
			{
				base.SetFunctionBodyStatement(vParent, beginEndBlockStatement);
				vParseErrorOccurred = beginEndBlockStatement == null;
				return;
			}
			return;
			IL_027F:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000AADB8 File Offset: 0x000A8FB8
		public ProcedureParameter functionParameter()
		{
			ProcedureParameter procedureParameter = base.FragmentFactory.CreateFragment<ProcedureParameter>();
			Identifier identifier = this.identifierVariable();
			int num = this.LA(1);
			if (num <= 53)
			{
				if (num == 9)
				{
					this.match(9);
					goto IL_0064;
				}
				if (num == 53)
				{
					goto IL_0064;
				}
			}
			else
			{
				if (num == 96)
				{
					goto IL_0064;
				}
				switch (num)
				{
				case 232:
				case 233:
					goto IL_0064;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0064:
			if (this.inputState.guessing == 0)
			{
				procedureParameter.VariableName = identifier;
			}
			this.scalarProcedureParameter(procedureParameter, false);
			return procedureParameter;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000AAE48 File Offset: 0x000A9048
		public Identifier identifierVariable()
		{
			Identifier identifier = base.FragmentFactory.CreateFragment<Identifier>();
			IToken token = this.LT(1);
			this.match(234);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(identifier, token);
				identifier.SetIdentifier(token.getText());
			}
			return identifier;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000AAE98 File Offset: 0x000A9098
		public void scalarProcedureParameter(ProcedureParameter vParent, bool outputAllowed)
		{
			DataTypeReference dataTypeReference = this.scalarDataType();
			if (this.inputState.guessing == 0)
			{
				vParent.DataType = dataTypeReference;
			}
			int num = this.LA(1);
			if (num <= 171)
			{
				if (num == 9 || num == 67 || num == 171)
				{
					goto IL_00A8;
				}
			}
			else if (num <= 198)
			{
				if (num == 192 || num == 198)
				{
					goto IL_00A8;
				}
			}
			else if (num != 206)
			{
				if (num == 232)
				{
					goto IL_00A8;
				}
			}
			else
			{
				this.match(206);
				ScalarExpression scalarExpression = this.possibleNegativeConstantOrIdentifierWithDefault();
				if (this.inputState.guessing == 0)
				{
					vParent.Value = scalarExpression;
					goto IL_00A8;
				}
				goto IL_00A8;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_00A8:
			int num2 = this.LA(1);
			if (num2 <= 171)
			{
				if (num2 == 9 || num2 == 67 || num2 == 171)
				{
					return;
				}
			}
			else
			{
				if (num2 == 192 || num2 == 198)
				{
					return;
				}
				if (num2 == 232)
				{
					IToken token = this.LT(1);
					this.match(232);
					if (this.inputState.guessing != 0)
					{
						return;
					}
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					TSql80ParserBaseInternal.Match(token, "OUTPUT", "OUT");
					if (outputAllowed)
					{
						vParent.Modifier = ParameterModifier.Output;
						return;
					}
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46039", token, TSqlParserResource.SQL46039Message, new string[0]);
					return;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000AB008 File Offset: 0x000A9208
		public void functionAttributes(FunctionStatementBody vParent)
		{
			int num = 0;
			this.match(171);
			FunctionOption functionOption = this.functionAttribute();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)functionOption.OptionKind, functionOption);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<FunctionOption>(vParent, vParent.Options, functionOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				functionOption = this.functionAttribute();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)functionOption.OptionKind, functionOption);
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<FunctionOption>(vParent, vParent.Options, functionOption);
				}
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000AB09C File Offset: 0x000A929C
		public BeginEndBlockStatement beginEndBlockStatement()
		{
			BeginEndBlockStatement beginEndBlockStatement = base.FragmentFactory.CreateFragment<BeginEndBlockStatement>();
			bool flag = false;
			StatementList statementList = base.FragmentFactory.CreateFragment<StatementList>();
			IToken token = this.LT(1);
			this.match(13);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(beginEndBlockStatement, token);
			}
			int num = 0;
			while (TSql80ParserInternal.tokenSet_3_.member(this.LA(1)))
			{
				TSqlStatement tsqlStatement = this.statementOptSemi();
				if (this.inputState.guessing == 0)
				{
					if (tsqlStatement != null)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TSqlStatement>(statementList, statementList.Statements, tsqlStatement);
					}
					else
					{
						flag = true;
						base.ThrowIfEndOfFileOrBatch();
					}
				}
				num++;
			}
			if (num < 1)
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			IToken token2 = this.LT(1);
			this.match(56);
			if (this.inputState.guessing == 0)
			{
				beginEndBlockStatement.StatementList = statementList;
				TSql80ParserBaseInternal.UpdateTokenInfo(beginEndBlockStatement, token2);
				if (flag)
				{
					beginEndBlockStatement = null;
				}
			}
			return beginEndBlockStatement;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000AB18C File Offset: 0x000A938C
		public SelectFunctionReturnType functionReturnClauseRelational()
		{
			SelectFunctionReturnType selectFunctionReturnType = base.FragmentFactory.CreateFragment<SelectFunctionReturnType>();
			SelectStatement selectStatement = this.subqueryExpressionAsStatement();
			if (this.inputState.guessing == 0)
			{
				selectFunctionReturnType.SelectStatement = selectStatement;
			}
			return selectFunctionReturnType;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000AB1C4 File Offset: 0x000A93C4
		public DeclareTableVariableBody declareTableBody(IndexAffectingStatement statementType)
		{
			bool flag = false;
			Identifier identifier = this.identifierVariable();
			this.match(148);
			DeclareTableVariableBody declareTableVariableBody = this.declareTableBodyMain(statementType);
			if (this.inputState.guessing == 0)
			{
				declareTableVariableBody.VariableName = identifier;
				declareTableVariableBody.AsDefined = flag;
			}
			return declareTableVariableBody;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000AB20C File Offset: 0x000A940C
		public FunctionOption functionAttribute()
		{
			FunctionOption functionOption = base.FragmentFactory.CreateFragment<FunctionOption>();
			if (this.LA(1) == 232 && TSql80ParserInternal.tokenSet_26_.member(this.LA(2)))
			{
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					functionOption.OptionKind = TSql80ParserBaseInternal.ParseAlterCreateFunctionWithOption(token);
					TSql80ParserBaseInternal.UpdateTokenInfo(functionOption, token);
				}
			}
			else if (this.LA(1) == 232 && this.LA(2) == 100)
			{
				IToken token2 = this.LT(1);
				this.match(232);
				this.match(100);
				this.match(105);
				this.match(100);
				IToken token3 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token2, "RETURNS");
					TSql80ParserBaseInternal.Match(token3, "INPUT");
					functionOption.OptionKind = FunctionOptionKind.ReturnsNullOnNullInput;
					TSql80ParserBaseInternal.UpdateTokenInfo(functionOption, token3);
				}
			}
			else
			{
				if (this.LA(1) != 232 || this.LA(2) != 105)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token4 = this.LT(1);
				this.match(232);
				this.match(105);
				this.match(100);
				IToken token5 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token4, "CALLED");
					TSql80ParserBaseInternal.Match(token5, "INPUT");
					functionOption.OptionKind = FunctionOptionKind.CalledOnNullInput;
					TSql80ParserBaseInternal.UpdateTokenInfo(functionOption, token5);
				}
			}
			return functionOption;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000AB3BC File Offset: 0x000A95BC
		public void identifierColumnList(TSqlFragment vParent, IList<ColumnReferenceExpression> columns)
		{
			this.match(191);
			ColumnReferenceExpression columnReferenceExpression = this.identifierColumnReferenceExpression();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(vParent, columns, columnReferenceExpression);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				columnReferenceExpression = this.identifierColumnReferenceExpression();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(vParent, columns, columnReferenceExpression);
				}
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x000AB450 File Offset: 0x000A9650
		public StatisticsOption createStatisticsStatementWithOption(ref bool isConflictingOption)
		{
			StatisticsOption statisticsOption;
			if (this.LA(1) == 232 && this.LA(2) == 221)
			{
				statisticsOption = this.sampleStatisticsOption(ref isConflictingOption);
			}
			else
			{
				if (this.LA(1) != 232 || !TSql80ParserInternal.tokenSet_21_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				statisticsOption = this.simpleStatisticsOption(ref isConflictingOption);
			}
			return statisticsOption;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000AB4C4 File Offset: 0x000A96C4
		public LiteralStatisticsOption sampleStatisticsOption(ref bool isConflictingOption)
		{
			LiteralStatisticsOption literalStatisticsOption = base.FragmentFactory.CreateFragment<LiteralStatisticsOption>();
			IToken token = this.LT(1);
			this.match(232);
			Literal literal = this.integer();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "SAMPLE");
				if (isConflictingOption)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46071", token, TSqlParserResource.SQL46071Message, new string[0]);
				}
				else
				{
					isConflictingOption = true;
				}
				literalStatisticsOption.Literal = literal;
			}
			int num = this.LA(1);
			if (num != 116)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token2 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(literalStatisticsOption, token2);
					literalStatisticsOption.OptionKind = TSql80ParserBaseInternal.ParseSampleOptionsWithOption(token2);
				}
			}
			else
			{
				IToken token3 = this.LT(1);
				this.match(116);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(literalStatisticsOption, token3);
					literalStatisticsOption.OptionKind = StatisticsOptionKind.SamplePercent;
				}
			}
			return literalStatisticsOption;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000AB5CC File Offset: 0x000A97CC
		public StatisticsOption simpleStatisticsOption(ref bool isConflictingOption)
		{
			StatisticsOption statisticsOption = base.FragmentFactory.CreateFragment<StatisticsOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(statisticsOption, token);
				if (TSql80ParserBaseInternal.TryMatch(token, "ROWS"))
				{
					statisticsOption.OptionKind = StatisticsOptionKind.Rows;
					if (isConflictingOption)
					{
						TSql80ParserBaseInternal.ThrowParseErrorException("SQL46071", token, TSqlParserResource.SQL46071Message, new string[0]);
					}
					else
					{
						isConflictingOption = true;
					}
				}
				else
				{
					if (TSql80ParserBaseInternal.TryMatch(token, "FULLSCAN"))
					{
						if (isConflictingOption)
						{
							TSql80ParserBaseInternal.ThrowParseErrorException("SQL46071", token, TSqlParserResource.SQL46071Message, new string[0]);
						}
						else
						{
							isConflictingOption = true;
						}
					}
					statisticsOption.OptionKind = TSql80ParserBaseInternal.ParseCreateStatisticsWithOption(token);
				}
			}
			return statisticsOption;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000AB680 File Offset: 0x000A9880
		public void columnNameList(TSqlFragment vParent, IList<Identifier> columnNames)
		{
			this.match(191);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(vParent, columnNames, identifier);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(vParent, columnNames, identifier);
				}
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x000AB714 File Offset: 0x000A9914
		public StatisticsOption updateStatisticsStatementWithOption(ref bool isConflictingOption)
		{
			StatisticsOption statisticsOption;
			if (this.LA(1) == 232 && this.LA(2) == 221)
			{
				statisticsOption = this.sampleStatisticsOption(ref isConflictingOption);
			}
			else if ((this.LA(1) == 135 || this.LA(1) == 232) && this.LA(2) == 206)
			{
				statisticsOption = this.updateStatisticsLiteralOption();
			}
			else
			{
				if ((this.LA(1) != 5 && this.LA(1) != 84 && this.LA(1) != 232) || !TSql80ParserInternal.tokenSet_21_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				statisticsOption = this.updateStatisticsSimpleOption(ref isConflictingOption);
			}
			return statisticsOption;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x000AB7D4 File Offset: 0x000A99D4
		public LiteralStatisticsOption updateStatisticsLiteralOption()
		{
			LiteralStatisticsOption literalStatisticsOption = base.FragmentFactory.CreateFragment<LiteralStatisticsOption>();
			int num = this.LA(1);
			if (num != 135)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				this.match(206);
				Literal literal = this.integer();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "PAGECOUNT");
					literalStatisticsOption.OptionKind = StatisticsOptionKind.PageCount;
					TSql80ParserBaseInternal.UpdateTokenInfo(literalStatisticsOption, token);
					literalStatisticsOption.Literal = literal;
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(135);
				this.match(206);
				Literal literal = this.integer();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(literalStatisticsOption, token2);
					literalStatisticsOption.OptionKind = StatisticsOptionKind.RowCount;
					literalStatisticsOption.Literal = literal;
				}
			}
			return literalStatisticsOption;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x000AB8C0 File Offset: 0x000A9AC0
		public StatisticsOption updateStatisticsSimpleOption(ref bool isConflictingOption)
		{
			StatisticsOption statisticsOption = base.FragmentFactory.CreateFragment<StatisticsOption>();
			int num = this.LA(1);
			if (num != 5)
			{
				if (num != 84)
				{
					if (num != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					IToken token = this.LT(1);
					this.match(232);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(statisticsOption, token);
						if (TSql80ParserBaseInternal.TryMatch(token, "ROWS"))
						{
							statisticsOption.OptionKind = StatisticsOptionKind.Rows;
							if (isConflictingOption)
							{
								TSql80ParserBaseInternal.ThrowParseErrorException("SQL46071", token, TSqlParserResource.SQL46071Message, new string[0]);
							}
							else
							{
								isConflictingOption = true;
							}
						}
						else
						{
							if (TSql80ParserBaseInternal.TryMatch(token, "FULLSCAN"))
							{
								if (isConflictingOption)
								{
									TSql80ParserBaseInternal.ThrowParseErrorException("SQL46071", token, TSqlParserResource.SQL46071Message, new string[0]);
								}
								else
								{
									isConflictingOption = true;
								}
							}
							statisticsOption.OptionKind = StatisticsOptionHelper.Instance.ParseOption(token);
						}
					}
				}
				else
				{
					IToken token2 = this.LT(1);
					this.match(84);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(statisticsOption, token2);
						statisticsOption.OptionKind = StatisticsOptionKind.Index;
					}
				}
			}
			else
			{
				IToken token3 = this.LT(1);
				this.match(5);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(statisticsOption, token3);
					statisticsOption.OptionKind = StatisticsOptionKind.All;
				}
			}
			return statisticsOption;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x000ABA18 File Offset: 0x000A9C18
		public SecurityElement80 securityElement80()
		{
			SecurityElement80 securityElement;
			if (this.LA(1) == 5 && (this.LA(2) == 71 || this.LA(2) == 151))
			{
				securityElement = this.commandSecurityElementAll80();
			}
			else if (this.LA(1) == 12 || this.LA(1) == 35)
			{
				securityElement = this.commandSecurityElement80();
			}
			else
			{
				if (!TSql80ParserInternal.tokenSet_27_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_28_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				securityElement = this.privilegeSecurityElement80();
			}
			return securityElement;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x000ABAB8 File Offset: 0x000A9CB8
		public SecurityUserClause80 securityUserClause80()
		{
			SecurityUserClause80 securityUserClause = base.FragmentFactory.CreateFragment<SecurityUserClause80>();
			int num = this.LA(1);
			if (num != 100)
			{
				if (num != 122)
				{
					switch (num)
					{
					case 232:
					case 233:
					{
						Identifier identifier = this.identifier();
						if (this.inputState.guessing == 0)
						{
							securityUserClause.UserType80 = UserType80.Users;
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(securityUserClause, securityUserClause.Users, identifier);
						}
						while (this.LA(1) == 198)
						{
							this.match(198);
							identifier = this.identifier();
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(securityUserClause, securityUserClause.Users, identifier);
							}
						}
						break;
					}
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					IToken token = this.LT(1);
					this.match(122);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(securityUserClause, token);
						securityUserClause.UserType80 = UserType80.Public;
					}
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(100);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(securityUserClause, token2);
					securityUserClause.UserType80 = UserType80.Null;
				}
			}
			return securityUserClause;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x000ABBE0 File Offset: 0x000A9DE0
		public CommandSecurityElement80 commandSecurityElementAll80()
		{
			CommandSecurityElement80 commandSecurityElement = base.FragmentFactory.CreateFragment<CommandSecurityElement80>();
			IToken token = this.LT(1);
			this.match(5);
			if (this.inputState.guessing == 0)
			{
				commandSecurityElement.All = true;
				TSql80ParserBaseInternal.UpdateTokenInfo(commandSecurityElement, token);
			}
			return commandSecurityElement;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x000ABC28 File Offset: 0x000A9E28
		public CommandSecurityElement80 commandSecurityElement80()
		{
			CommandSecurityElement80 commandSecurityElement = base.FragmentFactory.CreateFragment<CommandSecurityElement80>();
			this.command80(commandSecurityElement);
			while (this.LA(1) == 198)
			{
				this.match(198);
				this.command80(commandSecurityElement);
			}
			return commandSecurityElement;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x000ABC6C File Offset: 0x000A9E6C
		public PrivilegeSecurityElement80 privilegeSecurityElement80()
		{
			PrivilegeSecurityElement80 privilegeSecurityElement = base.FragmentFactory.CreateFragment<PrivilegeSecurityElement80>();
			Privilege80 privilege = this.privilege80();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Privilege80>(privilegeSecurityElement, privilegeSecurityElement.Privileges, privilege);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				privilege = this.privilege80();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Privilege80>(privilegeSecurityElement, privilegeSecurityElement.Privileges, privilege);
				}
			}
			this.match(105);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				privilegeSecurityElement.SchemaObjectName = schemaObjectName;
			}
			int num = this.LA(1);
			if (num != 71 && num != 151)
			{
				if (num != 191)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.columnNameList(privilegeSecurityElement, privilegeSecurityElement.Columns);
			}
			return privilegeSecurityElement;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000ABD48 File Offset: 0x000A9F48
		public void command80(CommandSecurityElement80 vParent)
		{
			int num = this.LA(1);
			if (num != 12)
			{
				if (num == 35)
				{
					IToken token = this.LT(1);
					this.match(35);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					}
					int num2 = this.LA(1);
					if (num2 <= 73)
					{
						if (num2 != 43)
						{
							if (num2 != 47)
							{
								if (num2 == 73)
								{
									IToken token2 = this.LT(1);
									this.match(73);
									if (this.inputState.guessing == 0)
									{
										TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
										vParent.CommandOptions |= CommandOptions.CreateFunction;
										return;
									}
									return;
								}
							}
							else
							{
								IToken token3 = this.LT(1);
								this.match(47);
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token3);
									vParent.CommandOptions |= CommandOptions.CreateDefault;
									return;
								}
								return;
							}
						}
						else
						{
							IToken token4 = this.LT(1);
							this.match(43);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token4);
								vParent.CommandOptions |= CommandOptions.CreateDatabase;
								return;
							}
							return;
						}
					}
					else if (num2 <= 137)
					{
						switch (num2)
						{
						case 120:
						{
							IToken token5 = this.LT(1);
							this.match(120);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token5);
								vParent.CommandOptions |= CommandOptions.CreateProcedure;
								return;
							}
							return;
						}
						case 121:
						{
							IToken token6 = this.LT(1);
							this.match(121);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token6);
								vParent.CommandOptions |= CommandOptions.CreateProcedure;
								return;
							}
							return;
						}
						default:
							if (num2 == 137)
							{
								IToken token7 = this.LT(1);
								this.match(137);
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token7);
									vParent.CommandOptions |= CommandOptions.CreateRule;
									return;
								}
								return;
							}
							break;
						}
					}
					else if (num2 != 148)
					{
						if (num2 == 166)
						{
							IToken token8 = this.LT(1);
							this.match(166);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token8);
								vParent.CommandOptions |= CommandOptions.CreateView;
								return;
							}
							return;
						}
					}
					else
					{
						IToken token9 = this.LT(1);
						this.match(148);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token9);
							vParent.CommandOptions |= CommandOptions.CreateTable;
							return;
						}
						return;
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			else
			{
				IToken token10 = this.LT(1);
				this.match(12);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token10);
				}
				int num3 = this.LA(1);
				if (num3 != 43)
				{
					if (num3 != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					IToken token11 = this.LT(1);
					this.match(232);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token11);
						TSql80ParserBaseInternal.Match(token11, "LOG");
						vParent.CommandOptions |= CommandOptions.BackupLog;
						return;
					}
				}
				else
				{
					IToken token12 = this.LT(1);
					this.match(43);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token12);
						vParent.CommandOptions |= CommandOptions.BackupDatabase;
						return;
					}
				}
			}
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000AC0F0 File Offset: 0x000AA2F0
		public Privilege80 privilege80()
		{
			Privilege80 privilege = base.FragmentFactory.CreateFragment<Privilege80>();
			int num = this.LA(1);
			if (num <= 61)
			{
				if (num == 5)
				{
					IToken token = this.LT(1);
					this.match(5);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(privilege, token);
						privilege.PrivilegeType80 = PrivilegeType80.All;
					}
					int num2 = this.LA(1);
					if (num2 <= 191)
					{
						if (num2 == 105 || num2 == 191)
						{
							goto IL_02C2;
						}
					}
					else
					{
						if (num2 == 198)
						{
							goto IL_02C2;
						}
						if (num2 == 232)
						{
							IToken token2 = this.LT(1);
							this.match(232);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.Match(token2, "PRIVILEGES");
								TSql80ParserBaseInternal.UpdateTokenInfo(privilege, token2);
								goto IL_02C2;
							}
							goto IL_02C2;
						}
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				if (num != 48)
				{
					switch (num)
					{
					case 60:
					{
						IToken token3 = this.LT(1);
						this.match(60);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(privilege, token3);
							privilege.PrivilegeType80 = PrivilegeType80.Execute;
							goto IL_02C2;
						}
						goto IL_02C2;
					}
					case 61:
					{
						IToken token4 = this.LT(1);
						this.match(61);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(privilege, token4);
							privilege.PrivilegeType80 = PrivilegeType80.Execute;
							goto IL_02C2;
						}
						goto IL_02C2;
					}
					}
				}
				else
				{
					IToken token5 = this.LT(1);
					this.match(48);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(privilege, token5);
						privilege.PrivilegeType80 = PrivilegeType80.Delete;
						goto IL_02C2;
					}
					goto IL_02C2;
				}
			}
			else if (num <= 127)
			{
				if (num != 86)
				{
					if (num == 127)
					{
						IToken token6 = this.LT(1);
						this.match(127);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(privilege, token6);
							privilege.PrivilegeType80 = PrivilegeType80.References;
							goto IL_02C2;
						}
						goto IL_02C2;
					}
				}
				else
				{
					IToken token7 = this.LT(1);
					this.match(86);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(privilege, token7);
						privilege.PrivilegeType80 = PrivilegeType80.Insert;
						goto IL_02C2;
					}
					goto IL_02C2;
				}
			}
			else if (num != 140)
			{
				if (num == 160)
				{
					IToken token8 = this.LT(1);
					this.match(160);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(privilege, token8);
						privilege.PrivilegeType80 = PrivilegeType80.Update;
						goto IL_02C2;
					}
					goto IL_02C2;
				}
			}
			else
			{
				IToken token9 = this.LT(1);
				this.match(140);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(privilege, token9);
					privilege.PrivilegeType80 = PrivilegeType80.Select;
					goto IL_02C2;
				}
				goto IL_02C2;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_02C2:
			int num3 = this.LA(1);
			if (num3 != 105)
			{
				if (num3 != 191)
				{
					if (num3 != 198)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					this.columnNameList(privilege, privilege.Columns);
				}
			}
			return privilege;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000AC408 File Offset: 0x000AA608
		public ColumnReferenceExpression column()
		{
			ColumnReferenceExpression columnReferenceExpression = base.FragmentFactory.CreateFragment<ColumnReferenceExpression>();
			columnReferenceExpression.ColumnType = ColumnType.Regular;
			int num = this.LA(1);
			if (num > 136)
			{
				if (num != 200)
				{
					if (num == 227)
					{
						goto IL_0340;
					}
					switch (num)
					{
					case 232:
					case 233:
						break;
					default:
						goto IL_0349;
					}
				}
				MultiPartIdentifier multiPartIdentifier = this.multiPartIdentifier(-1);
				if (this.inputState.guessing == 0)
				{
					columnReferenceExpression.MultiPartIdentifier = multiPartIdentifier;
				}
				int num2 = this.LA(1);
				if (num2 <= 95)
				{
					if (num2 <= 35)
					{
						if (num2 <= 17)
						{
							if (num2 == 1 || num2 == 6)
							{
								goto IL_035C;
							}
							switch (num2)
							{
							case 12:
							case 13:
							case 15:
							case 17:
								goto IL_035C;
							}
						}
						else
						{
							switch (num2)
							{
							case 22:
							case 23:
								goto IL_035C;
							default:
								if (num2 == 28)
								{
									goto IL_035C;
								}
								switch (num2)
								{
								case 33:
								case 35:
									goto IL_035C;
								}
								break;
							}
						}
					}
					else if (num2 <= 82)
					{
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							goto IL_035C;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num2)
							{
							case 74:
							case 75:
								goto IL_035C;
							default:
								if (num2 == 82)
								{
									goto IL_035C;
								}
								break;
							}
							break;
						}
					}
					else if (num2 == 86 || num2 == 92 || num2 == 95)
					{
						goto IL_035C;
					}
				}
				else if (num2 <= 173)
				{
					if (num2 <= 119)
					{
						if (num2 == 106 || num2 == 111 || num2 == 119)
						{
							goto IL_035C;
						}
					}
					else
					{
						switch (num2)
						{
						case 123:
						case 125:
						case 126:
						case 129:
						case 131:
						case 132:
						case 134:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							goto IL_035C;
						case 124:
						case 127:
						case 128:
						case 130:
						case 133:
						case 135:
						case 136:
						case 137:
						case 139:
						case 141:
							break;
						default:
							switch (num2)
							{
							case 156:
							case 160:
							case 161:
							case 162:
								goto IL_035C;
							case 157:
							case 158:
							case 159:
								break;
							default:
								switch (num2)
								{
								case 167:
								case 170:
								case 172:
								case 173:
									goto IL_035C;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 192)
				{
					if (num2 == 176)
					{
						goto IL_035C;
					}
					switch (num2)
					{
					case 180:
					case 181:
						goto IL_035C;
					default:
						switch (num2)
						{
						case 191:
						case 192:
							goto IL_035C;
						}
						break;
					}
				}
				else if (num2 <= 204)
				{
					switch (num2)
					{
					case 198:
						goto IL_035C;
					case 199:
						break;
					case 200:
						this.match(200);
						this.specialColumn(columnReferenceExpression);
						goto IL_035C;
					default:
						if (num2 == 204)
						{
							goto IL_035C;
						}
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 219:
					case 220:
					case 221:
					case 224:
						goto IL_035C;
					case 222:
					case 223:
						break;
					default:
						if (num2 == 234)
						{
							goto IL_035C;
						}
						break;
					}
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			if (num != 81 && num != 136)
			{
				goto IL_0349;
			}
			IL_0340:
			this.specialColumn(columnReferenceExpression);
			goto IL_035C;
			IL_0349:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_035C:
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckSpecialColumn(columnReferenceExpression);
				TSql80ParserBaseInternal.CheckTableNameExistsForColumn(columnReferenceExpression, false);
			}
			return columnReferenceExpression;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000AC78C File Offset: 0x000AA98C
		public ValueExpression binaryOrVariable()
		{
			int num = this.LA(1);
			ValueExpression valueExpression;
			if (num != 224)
			{
				if (num != 234)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				valueExpression = this.variable();
			}
			else
			{
				valueExpression = this.binary();
			}
			return valueExpression;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000AC7DC File Offset: 0x000AA9DC
		public ValueExpression integerOrVariable()
		{
			int num = this.LA(1);
			ValueExpression valueExpression;
			if (num != 221)
			{
				if (num != 234)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				valueExpression = this.variable();
			}
			else
			{
				valueExpression = this.integer();
			}
			return valueExpression;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000AC82C File Offset: 0x000AAA2C
		public void modificationTextStatement(TextModificationStatement vParent)
		{
			int num = this.LA(1);
			if (num <= 136)
			{
				if (num != 17)
				{
					if (num == 81 || num == 136)
					{
						goto IL_0085;
					}
				}
				else
				{
					this.match(17);
					if (this.inputState.guessing == 0)
					{
						vParent.Bulk = true;
						goto IL_0085;
					}
					goto IL_0085;
				}
			}
			else
			{
				if (num == 200 || num == 227)
				{
					goto IL_0085;
				}
				switch (num)
				{
				case 232:
				case 233:
					goto IL_0085;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0085:
			ColumnReferenceExpression columnReferenceExpression = this.column();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckTableNameExistsForColumn(columnReferenceExpression, true);
				vParent.Column = columnReferenceExpression;
			}
			int num2 = this.LA(1);
			ValueExpression valueExpression;
			if (num2 != 221)
			{
				if (num2 != 224 && num2 != 234)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				valueExpression = this.binaryOrVariable();
			}
			else
			{
				valueExpression = this.integer();
			}
			if (this.inputState.guessing == 0)
			{
				vParent.TextId = valueExpression;
			}
			int num3 = this.LA(1);
			if (num3 <= 199)
			{
				if (num3 == 100 || num3 == 171 || num3 == 199)
				{
					return;
				}
			}
			else
			{
				if (num3 == 221 || num3 == 224)
				{
					return;
				}
				switch (num3)
				{
				case 230:
				case 231:
				case 234:
					return;
				case 232:
				{
					IToken token = this.LT(1);
					this.match(232);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.Match(token, "TIMESTAMP");
					}
					this.match(206);
					Literal literal = this.binary();
					if (this.inputState.guessing == 0)
					{
						vParent.Timestamp = literal;
						return;
					}
					return;
				}
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000ACA18 File Offset: 0x000AAC18
		public ScalarExpression signedIntegerOrVariableOrNull()
		{
			int num = this.LA(1);
			if (num <= 199)
			{
				if (num == 100)
				{
					return this.nullLiteral();
				}
				if (num != 199)
				{
					goto IL_0041;
				}
			}
			else if (num != 221 && num != 234)
			{
				goto IL_0041;
			}
			return this.signedIntegerOrVariable();
			IL_0041:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000ACA7C File Offset: 0x000AAC7C
		public void modificationTextStatementWithLog(TextModificationStatement vParent)
		{
			this.match(171);
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "LOG");
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
				vParent.WithLog = true;
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000ACAD0 File Offset: 0x000AACD0
		public ValueExpression writeString()
		{
			int num = this.LA(1);
			ValueExpression valueExpression;
			if (num != 100)
			{
				if (num != 224)
				{
					switch (num)
					{
					case 230:
					case 231:
						return this.stringLiteral();
					case 234:
						return this.variable();
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				valueExpression = this.binary();
			}
			else
			{
				valueExpression = this.nullLiteral();
			}
			return valueExpression;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000ACB4C File Offset: 0x000AAD4C
		public BinaryLiteral binary()
		{
			BinaryLiteral binaryLiteral = base.FragmentFactory.CreateFragment<BinaryLiteral>();
			IToken token = this.LT(1);
			this.match(224);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(binaryLiteral, token);
				binaryLiteral.Value = token.getText();
				binaryLiteral.IsLargeObject = TSql80ParserBaseInternal.IsBinaryLiteralLob(binaryLiteral.Value);
			}
			return binaryLiteral;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000ACBAC File Offset: 0x000AADAC
		public NullLiteral nullLiteral()
		{
			NullLiteral nullLiteral = base.FragmentFactory.CreateFragment<NullLiteral>();
			IToken token = this.LT(1);
			this.match(100);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(nullLiteral, token);
				nullLiteral.Value = token.getText();
			}
			return nullLiteral;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000ACBF8 File Offset: 0x000AADF8
		public VariableReference variable()
		{
			VariableReference variableReference = base.FragmentFactory.CreateFragment<VariableReference>();
			IToken token = this.LT(1);
			this.match(234);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(variableReference, token);
				variableReference.Name = token.getText();
			}
			return variableReference;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000ACC48 File Offset: 0x000AAE48
		public CursorId cursorId()
		{
			CursorId cursorId = base.FragmentFactory.CreateFragment<CursorId>();
			if (this.LA(1) == 232 && (this.LA(2) == 232 || this.LA(2) == 233) && base.NextTokenMatches("GLOBAL"))
			{
				IToken token = this.LT(1);
				this.match(232);
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "GLOBAL");
					cursorId.Name = base.IdentifierOrValueExpression(identifier);
					cursorId.IsGlobal = true;
				}
			}
			else
			{
				if (this.LA(1) < 232 || this.LA(1) > 234 || !TSql80ParserInternal.tokenSet_29_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrVariable();
				if (this.inputState.guessing == 0)
				{
					cursorId.Name = identifierOrValueExpression;
					cursorId.IsGlobal = false;
				}
			}
			return cursorId;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000ACD4C File Offset: 0x000AAF4C
		public FetchCursorStatement rowSelector()
		{
			FetchCursorStatement fetchCursorStatement = base.FragmentFactory.CreateFragment<FetchCursorStatement>();
			if (this.LA(1) >= 232 && this.LA(1) <= 234 && TSql80ParserInternal.tokenSet_30_.member(this.LA(2)))
			{
				CursorId cursorId = this.cursorId();
				if (this.inputState.guessing == 0)
				{
					fetchCursorStatement.Cursor = cursorId;
				}
			}
			else if (this.LA(1) == 232 && TSql80ParserInternal.tokenSet_31_.member(this.LA(2)))
			{
				FetchType fetchType = this.fetchType();
				this.match(71);
				CursorId cursorId = this.cursorId();
				if (this.inputState.guessing == 0)
				{
					fetchCursorStatement.Cursor = cursorId;
					fetchCursorStatement.FetchType = fetchType;
				}
			}
			else
			{
				if (this.LA(1) != 71)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(71);
				CursorId cursorId = this.cursorId();
				if (this.inputState.guessing == 0)
				{
					fetchCursorStatement.Cursor = cursorId;
				}
			}
			return fetchCursorStatement;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000ACE50 File Offset: 0x000AB050
		public FetchType fetchType()
		{
			FetchType fetchType = base.FragmentFactory.CreateFragment<FetchType>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				fetchType.Orientation = FetchOrientationHelper.Instance.ParseOption(token);
			}
			int num = this.LA(1);
			ScalarExpression scalarExpression;
			if (num <= 199)
			{
				if (num != 71)
				{
					if (num != 199)
					{
						goto IL_010E;
					}
				}
				else
				{
					if (this.inputState.guessing == 0 && (fetchType.Orientation == FetchOrientation.Relative || fetchType.Orientation == FetchOrientation.Absolute))
					{
						throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
					}
					return fetchType;
				}
			}
			else if (num != 221)
			{
				if (num != 234)
				{
					goto IL_010E;
				}
				scalarExpression = this.variable();
				if (this.inputState.guessing != 0)
				{
					return fetchType;
				}
				if (fetchType.Orientation != FetchOrientation.Relative && fetchType.Orientation != FetchOrientation.Absolute)
				{
					throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
				}
				fetchType.RowOffset = scalarExpression;
				return fetchType;
			}
			scalarExpression = this.signedInteger();
			if (this.inputState.guessing != 0)
			{
				return fetchType;
			}
			if (fetchType.Orientation != FetchOrientation.Relative && fetchType.Orientation != FetchOrientation.Absolute)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
			fetchType.RowOffset = scalarExpression;
			return fetchType;
			IL_010E:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000ACF80 File Offset: 0x000AB180
		public DropDatabaseStatement dropDatabaseStatement()
		{
			DropDatabaseStatement dropDatabaseStatement = base.FragmentFactory.CreateFragment<DropDatabaseStatement>();
			this.match(43);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(dropDatabaseStatement, dropDatabaseStatement.Databases, identifier);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(dropDatabaseStatement, dropDatabaseStatement.Databases, identifier);
				}
			}
			return dropDatabaseStatement;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000AD000 File Offset: 0x000AB200
		public DropIndexStatement dropIndexStatement()
		{
			DropIndexStatement dropIndexStatement = base.FragmentFactory.CreateFragment<DropIndexStatement>();
			this.match(84);
			DropIndexClauseBase dropIndexClauseBase = this.indexDropObject();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DropIndexClauseBase>(dropIndexStatement, dropIndexStatement.DropIndexClauses, dropIndexClauseBase);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				dropIndexClauseBase = this.indexDropObject();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DropIndexClauseBase>(dropIndexStatement, dropIndexStatement.DropIndexClauses, dropIndexClauseBase);
				}
			}
			return dropIndexStatement;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000AD080 File Offset: 0x000AB280
		public DropStatisticsStatement dropStatisticsStatement()
		{
			DropStatisticsStatement dropStatisticsStatement = base.FragmentFactory.CreateFragment<DropStatisticsStatement>();
			this.match(146);
			ChildObjectName childObjectName = this.statisticsDropObject();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ChildObjectName>(dropStatisticsStatement, dropStatisticsStatement.Objects, childObjectName);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				childObjectName = this.statisticsDropObject();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ChildObjectName>(dropStatisticsStatement, dropStatisticsStatement.Objects, childObjectName);
				}
			}
			return dropStatisticsStatement;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x000AD104 File Offset: 0x000AB304
		public DropTableStatement dropTableStatement()
		{
			DropTableStatement dropTableStatement = base.FragmentFactory.CreateFragment<DropTableStatement>();
			this.match(148);
			this.dropObjectList(dropTableStatement, false);
			return dropTableStatement;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000AD134 File Offset: 0x000AB334
		public DropProcedureStatement dropProcedureStatement()
		{
			DropProcedureStatement dropProcedureStatement = base.FragmentFactory.CreateFragment<DropProcedureStatement>();
			switch (this.LA(1))
			{
			case 120:
				this.match(120);
				break;
			case 121:
				this.match(121);
				break;
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			this.dropObjectList(dropProcedureStatement, true);
			return dropProcedureStatement;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x000AD198 File Offset: 0x000AB398
		public DropFunctionStatement dropFunctionStatement()
		{
			DropFunctionStatement dropFunctionStatement = base.FragmentFactory.CreateFragment<DropFunctionStatement>();
			this.match(73);
			this.dropObjectList(dropFunctionStatement, true);
			return dropFunctionStatement;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000AD1C4 File Offset: 0x000AB3C4
		public DropViewStatement dropViewStatement()
		{
			DropViewStatement dropViewStatement = base.FragmentFactory.CreateFragment<DropViewStatement>();
			this.match(166);
			this.dropObjectList(dropViewStatement, true);
			return dropViewStatement;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x000AD1F4 File Offset: 0x000AB3F4
		public DropDefaultStatement dropDefaultStatement()
		{
			DropDefaultStatement dropDefaultStatement = base.FragmentFactory.CreateFragment<DropDefaultStatement>();
			this.match(47);
			this.dropObjectList(dropDefaultStatement, true);
			return dropDefaultStatement;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000AD220 File Offset: 0x000AB420
		public DropRuleStatement dropRuleStatement()
		{
			DropRuleStatement dropRuleStatement = base.FragmentFactory.CreateFragment<DropRuleStatement>();
			this.match(137);
			this.dropObjectList(dropRuleStatement, true);
			return dropRuleStatement;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x000AD250 File Offset: 0x000AB450
		public DropTriggerStatement dropTriggerStatement()
		{
			DropTriggerStatement dropTriggerStatement = base.FragmentFactory.CreateFragment<DropTriggerStatement>();
			this.match(155);
			this.dropObjectList(dropTriggerStatement, true);
			return dropTriggerStatement;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000AD280 File Offset: 0x000AB480
		public BackwardsCompatibleDropIndexClause indexDropObject()
		{
			BackwardsCompatibleDropIndexClause backwardsCompatibleDropIndexClause = base.FragmentFactory.CreateFragment<BackwardsCompatibleDropIndexClause>();
			ChildObjectName childObjectName = this.childObjectNameWithThreePrefixes();
			if (this.inputState.guessing == 0)
			{
				if (childObjectName.BaseIdentifier == null)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46027", childObjectName, TSqlParserResource.SQL46027Message, new string[0]);
				}
				backwardsCompatibleDropIndexClause.Index = childObjectName;
			}
			return backwardsCompatibleDropIndexClause;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000AD2D4 File Offset: 0x000AB4D4
		public ChildObjectName statisticsDropObject()
		{
			ChildObjectName childObjectName = this.childObjectNameWithThreePrefixes();
			if (this.inputState.guessing == 0 && childObjectName.BaseIdentifier == null)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46038", childObjectName, TSqlParserResource.SQL46038Message, new string[0]);
			}
			return childObjectName;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000AD314 File Offset: 0x000AB514
		public List<Identifier> identifierList(int vMaxNumber)
		{
			List<Identifier> list = new List<Identifier>();
			int num = this.LA(1);
			if (num != 200)
			{
				switch (num)
				{
				case 232:
				case 233:
				{
					Identifier identifier = this.identifier();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddIdentifierToListWithCheck(list, identifier, vMaxNumber);
					}
					while (this.LA(1) == 200 && (this.LA(2) == 200 || this.LA(2) == 232 || this.LA(2) == 233))
					{
						this.identifierListElement(list, vMaxNumber, false);
					}
					break;
				}
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				this.identifierListElement(list, vMaxNumber, true);
				while (this.LA(1) == 200 && (this.LA(2) == 200 || this.LA(2) == 232 || this.LA(2) == 233))
				{
					this.identifierListElement(list, vMaxNumber, false);
				}
			}
			return list;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x000AD414 File Offset: 0x000AB614
		public void dropObjectList(DropObjectsStatement vParent, bool onlyTwoPartNames)
		{
			SchemaObjectName schemaObjectName = this.dropObject(onlyTwoPartNames);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SchemaObjectName>(vParent, vParent.Objects, schemaObjectName);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				schemaObjectName = this.dropObject(onlyTwoPartNames);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SchemaObjectName>(vParent, vParent.Objects, schemaObjectName);
				}
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000AD480 File Offset: 0x000AB680
		public SchemaObjectName dropObject(bool onlyTwoPartNames)
		{
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0 && onlyTwoPartNames)
			{
				TSql80ParserBaseInternal.CheckTwoPartNameForSchemaObjectName(schemaObjectName, "DROP");
			}
			return schemaObjectName;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000AD4B0 File Offset: 0x000AB6B0
		public Identifier nonQuotedIdentifier()
		{
			Identifier identifier = base.FragmentFactory.CreateFragment<Identifier>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(identifier, token);
				identifier.SetUnquotedIdentifier(token.getText());
			}
			return identifier;
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x000AD500 File Offset: 0x000AB700
		public BeginTransactionStatement beginTransactionStatement()
		{
			BeginTransactionStatement beginTransactionStatement = base.FragmentFactory.CreateFragment<BeginTransactionStatement>();
			IToken token = this.LT(1);
			this.match(13);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(beginTransactionStatement, token);
			}
			int num = this.LA(1);
			if (num != 52)
			{
				switch (num)
				{
				case 153:
				case 154:
					break;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				this.match(52);
				if (this.inputState.guessing == 0)
				{
					beginTransactionStatement.Distributed = true;
				}
			}
			switch (this.LA(1))
			{
			case 153:
			{
				IToken token2 = this.LT(1);
				this.match(153);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(beginTransactionStatement, token2);
				}
				break;
			}
			case 154:
			{
				IToken token3 = this.LT(1);
				this.match(154);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(beginTransactionStatement, token3);
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			int num2 = this.LA(1);
			if (num2 <= 92)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							goto IL_03BE;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_03BE;
						case 14:
						case 16:
							break;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								goto IL_03BE;
							default:
								if (num2 == 28)
								{
									goto IL_03BE;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 75)
				{
					switch (num2)
					{
					case 33:
					case 35:
						goto IL_03BE;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							goto IL_03BE;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num2)
							{
							case 74:
							case 75:
								goto IL_03BE;
							}
							break;
						}
						break;
					}
				}
				else if (num2 == 82 || num2 == 86 || num2 == 92)
				{
					goto IL_03BE;
				}
			}
			else
			{
				if (num2 > 162)
				{
					if (num2 <= 199)
					{
						switch (num2)
						{
						case 167:
						case 170:
						case 171:
						case 172:
						case 173:
						case 176:
						case 180:
						case 181:
							goto IL_03BE;
						case 168:
						case 169:
						case 174:
						case 175:
						case 177:
						case 178:
						case 179:
							goto IL_03AB;
						default:
							if (num2 == 191)
							{
								goto IL_03BE;
							}
							if (num2 != 199)
							{
								goto IL_03AB;
							}
							break;
						}
					}
					else
					{
						if (num2 == 204)
						{
							goto IL_03BE;
						}
						switch (num2)
						{
						case 219:
						case 220:
							goto IL_03BE;
						case 221:
							break;
						default:
							switch (num2)
							{
							case 232:
							case 233:
							case 234:
								break;
							default:
								goto IL_03AB;
							}
							break;
						}
					}
					this.transactionName(beginTransactionStatement);
					goto IL_03BE;
				}
				if (num2 <= 106)
				{
					if (num2 == 95 || num2 == 106)
					{
						goto IL_03BE;
					}
				}
				else
				{
					if (num2 == 119)
					{
						goto IL_03BE;
					}
					switch (num2)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_03BE;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num2)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							goto IL_03BE;
						}
						break;
					}
				}
			}
			IL_03AB:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_03BE:
			int num3 = this.LA(1);
			if (num3 <= 86)
			{
				if (num3 <= 28)
				{
					if (num3 <= 6)
					{
						if (num3 == 1 || num3 == 6)
						{
							return beginTransactionStatement;
						}
					}
					else
					{
						switch (num3)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return beginTransactionStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num3)
							{
							case 22:
							case 23:
								return beginTransactionStatement;
							default:
								if (num3 == 28)
								{
									return beginTransactionStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num3 <= 64)
				{
					switch (num3)
					{
					case 33:
					case 35:
						return beginTransactionStatement;
					case 34:
						break;
					default:
						switch (num3)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return beginTransactionStatement;
						}
						break;
					}
				}
				else
				{
					switch (num3)
					{
					case 74:
					case 75:
						return beginTransactionStatement;
					default:
						if (num3 == 82 || num3 == 86)
						{
							return beginTransactionStatement;
						}
						break;
					}
				}
			}
			else if (num3 <= 144)
			{
				if (num3 <= 95)
				{
					if (num3 == 92 || num3 == 95)
					{
						return beginTransactionStatement;
					}
				}
				else
				{
					if (num3 == 106 || num3 == 119)
					{
						return beginTransactionStatement;
					}
					switch (num3)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return beginTransactionStatement;
					}
				}
			}
			else if (num3 <= 181)
			{
				switch (num3)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return beginTransactionStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num3)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return beginTransactionStatement;
					case 171:
					{
						this.match(171);
						IToken token4 = this.LT(1);
						this.match(232);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.Match(token4, "MARK");
							TSql80ParserBaseInternal.UpdateTokenInfo(beginTransactionStatement, token4);
							beginTransactionStatement.MarkDefined = true;
						}
						int num4 = this.LA(1);
						if (num4 <= 92)
						{
							if (num4 <= 28)
							{
								if (num4 <= 6)
								{
									if (num4 == 1 || num4 == 6)
									{
										return beginTransactionStatement;
									}
								}
								else
								{
									switch (num4)
									{
									case 12:
									case 13:
									case 15:
									case 17:
										return beginTransactionStatement;
									case 14:
									case 16:
										break;
									default:
										switch (num4)
										{
										case 22:
										case 23:
											return beginTransactionStatement;
										default:
											if (num4 == 28)
											{
												return beginTransactionStatement;
											}
											break;
										}
										break;
									}
								}
							}
							else if (num4 <= 75)
							{
								switch (num4)
								{
								case 33:
								case 35:
									return beginTransactionStatement;
								case 34:
									break;
								default:
									switch (num4)
									{
									case 44:
									case 45:
									case 46:
									case 48:
									case 49:
									case 54:
									case 55:
									case 56:
									case 60:
									case 61:
									case 64:
										return beginTransactionStatement;
									case 47:
									case 50:
									case 51:
									case 52:
									case 53:
									case 57:
									case 58:
									case 59:
									case 62:
									case 63:
										break;
									default:
										switch (num4)
										{
										case 74:
										case 75:
											return beginTransactionStatement;
										}
										break;
									}
									break;
								}
							}
							else if (num4 == 82 || num4 == 86 || num4 == 92)
							{
								return beginTransactionStatement;
							}
						}
						else if (num4 <= 173)
						{
							if (num4 <= 119)
							{
								if (num4 == 95 || num4 == 106 || num4 == 119)
								{
									return beginTransactionStatement;
								}
							}
							else
							{
								switch (num4)
								{
								case 123:
								case 125:
								case 126:
								case 129:
								case 131:
								case 132:
								case 134:
								case 138:
								case 140:
								case 142:
								case 143:
								case 144:
									return beginTransactionStatement;
								case 124:
								case 127:
								case 128:
								case 130:
								case 133:
								case 135:
								case 136:
								case 137:
								case 139:
								case 141:
									break;
								default:
									switch (num4)
									{
									case 156:
									case 160:
									case 161:
									case 162:
										return beginTransactionStatement;
									case 157:
									case 158:
									case 159:
										break;
									default:
										switch (num4)
										{
										case 167:
										case 170:
										case 172:
										case 173:
											return beginTransactionStatement;
										}
										break;
									}
									break;
								}
							}
						}
						else if (num4 <= 191)
						{
							if (num4 == 176)
							{
								return beginTransactionStatement;
							}
							switch (num4)
							{
							case 180:
							case 181:
								return beginTransactionStatement;
							default:
								if (num4 == 191)
								{
									return beginTransactionStatement;
								}
								break;
							}
						}
						else
						{
							if (num4 == 204)
							{
								return beginTransactionStatement;
							}
							switch (num4)
							{
							case 219:
							case 220:
								return beginTransactionStatement;
							default:
								switch (num4)
								{
								case 230:
								case 231:
								case 234:
								{
									ValueExpression valueExpression = this.stringOrVariable();
									if (this.inputState.guessing == 0)
									{
										beginTransactionStatement.MarkDescription = valueExpression;
										return beginTransactionStatement;
									}
									return beginTransactionStatement;
								}
								}
								break;
							}
						}
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					}
					break;
				}
			}
			else
			{
				if (num3 == 191 || num3 == 204)
				{
					return beginTransactionStatement;
				}
				switch (num3)
				{
				case 219:
				case 220:
					return beginTransactionStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x000ADE54 File Offset: 0x000AC054
		public void transactionName(TransactionStatement vParent)
		{
			int num = this.LA(1);
			if (num != 199 && num != 221)
			{
				switch (num)
				{
				case 232:
				case 233:
				case 234:
				{
					IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrVariable();
					if (this.inputState.guessing == 0)
					{
						vParent.Name = identifierOrValueExpression;
						return;
					}
					break;
				}
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				Identifier identifier = this.weirdTransactionName();
				if (this.inputState.guessing == 0)
				{
					vParent.Name = base.IdentifierOrValueExpression(identifier);
					return;
				}
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000ADEE4 File Offset: 0x000AC0E4
		public Identifier weirdTransactionName()
		{
			Identifier identifier = base.FragmentFactory.CreateFragment<Identifier>();
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.LA(1);
			if (num != 199)
			{
				if (num != 221)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				IToken token = this.LT(1);
				this.match(199);
				if (this.inputState.guessing == 0)
				{
					stringBuilder.Append(token.getText());
					TSql80ParserBaseInternal.UpdateTokenInfo(identifier, token);
				}
			}
			IToken token2 = this.LT(1);
			this.match(221);
			IToken token3 = this.LT(1);
			this.match(202);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(identifier, token2);
				stringBuilder.Append(token2.getText());
				stringBuilder.Append(token3.getText());
			}
			this.tranIdentifier(stringBuilder, identifier);
			IToken token4 = this.LT(1);
			this.match(200);
			if (this.inputState.guessing == 0)
			{
				stringBuilder.Append(token4.getText());
			}
			this.tranIdentifier(stringBuilder, identifier);
			if (this.inputState.guessing == 0)
			{
				identifier.Value = stringBuilder.ToString();
			}
			return identifier;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x000AE028 File Offset: 0x000AC228
		public void tranIdentifier(StringBuilder vStringBuilder, TSqlFragment vParent)
		{
			switch (this.LA(1))
			{
			case 232:
			{
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					vStringBuilder.Append(token.getText());
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					return;
				}
				break;
			}
			case 233:
			{
				IToken token2 = this.LT(1);
				this.match(233);
				if (this.inputState.guessing == 0)
				{
					vStringBuilder.Append(token2.getText());
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
					return;
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000AE0D4 File Offset: 0x000AC2D4
		public DeclareVariableElement declareVariableElement()
		{
			DeclareVariableElement declareVariableElement = base.FragmentFactory.CreateFragment<DeclareVariableElement>();
			Identifier identifier = this.identifierVariable();
			int num = this.LA(1);
			if (num <= 42)
			{
				if (num == 9)
				{
					this.match(9);
					goto IL_0069;
				}
				if (num == 42)
				{
					goto IL_0069;
				}
			}
			else
			{
				if (num == 53 || num == 96)
				{
					goto IL_0069;
				}
				switch (num)
				{
				case 232:
				case 233:
					goto IL_0069;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0069:
			int num2 = this.LA(1);
			DataTypeReference dataTypeReference;
			if (num2 <= 53)
			{
				if (num2 == 42)
				{
					dataTypeReference = this.cursorDataType();
					goto IL_00C8;
				}
				if (num2 != 53)
				{
					goto IL_00B5;
				}
			}
			else if (num2 != 96)
			{
				switch (num2)
				{
				case 232:
				case 233:
					break;
				default:
					goto IL_00B5;
				}
			}
			dataTypeReference = this.scalarDataType();
			goto IL_00C8;
			IL_00B5:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_00C8:
			if (this.inputState.guessing == 0)
			{
				declareVariableElement.VariableName = identifier;
				declareVariableElement.DataType = dataTypeReference;
			}
			return declareVariableElement;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x000AE1C8 File Offset: 0x000AC3C8
		public SqlDataTypeReference cursorDataType()
		{
			SqlDataTypeReference sqlDataTypeReference = base.FragmentFactory.CreateFragment<SqlDataTypeReference>();
			IToken token = this.LT(1);
			this.match(42);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(sqlDataTypeReference, token);
				sqlDataTypeReference.SqlDataTypeOption = SqlDataTypeOption.Cursor;
			}
			return sqlDataTypeReference;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000AE210 File Offset: 0x000AC410
		public DeclareVariableStatement declareVariableStatement()
		{
			DeclareVariableStatement declareVariableStatement = base.FragmentFactory.CreateFragment<DeclareVariableStatement>();
			DeclareVariableElement declareVariableElement = this.declareVariableElement();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DeclareVariableElement>(declareVariableStatement, declareVariableStatement.Declarations, declareVariableElement);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				declareVariableElement = this.declareVariableElement();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<DeclareVariableElement>(declareVariableStatement, declareVariableStatement.Declarations, declareVariableElement);
				}
			}
			return declareVariableStatement;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000AE288 File Offset: 0x000AC488
		public DeclareCursorStatement declareCursorStatement()
		{
			DeclareCursorStatement declareCursorStatement = base.FragmentFactory.CreateFragment<DeclareCursorStatement>();
			List<CursorOption> list = new List<CursorOption>();
			Identifier identifier = this.identifier();
			this.cursorOpts(true, list);
			CursorDefinition cursorDefinition = this.cursorDefinitionOptions(list);
			if (this.inputState.guessing == 0)
			{
				declareCursorStatement.Name = identifier;
				declareCursorStatement.CursorDefinition = cursorDefinition;
			}
			return declareCursorStatement;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000AE2DC File Offset: 0x000AC4DC
		public PredicateSetStatement predicateSetStatement()
		{
			PredicateSetStatement predicateSetStatement = base.FragmentFactory.CreateFragment<PredicateSetStatement>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				predicateSetStatement.Options = PredicateSetOptionsHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				IToken token2 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					predicateSetStatement.Options |= PredicateSetOptionsHelper.Instance.ParseOption(token2, SqlVersionFlags.TSql80);
				}
			}
			this.setOnOff(predicateSetStatement);
			if (this.inputState.guessing == 0 && (predicateSetStatement.Options & SetOptions.QuotedIdentifier) == SetOptions.QuotedIdentifier)
			{
				this._tokenSource.QuotedIdentifier = predicateSetStatement.IsOn;
			}
			return predicateSetStatement;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000AE3B0 File Offset: 0x000AC5B0
		public SetVariableStatement setVariableStatement()
		{
			SetVariableStatement setVariableStatement = base.FragmentFactory.CreateFragment<SetVariableStatement>();
			VariableReference variableReference = this.variable();
			if (this.inputState.guessing == 0)
			{
				setVariableStatement.Variable = variableReference;
			}
			this.match(206);
			int num = this.LA(1);
			if (num <= 101)
			{
				if (num <= 34)
				{
					if (num != 20 && num != 25 && num != 34)
					{
						goto IL_01FB;
					}
				}
				else if (num <= 81)
				{
					switch (num)
					{
					case 40:
					case 41:
						break;
					case 42:
					{
						CursorDefinition cursorDefinition = this.cursorDefinition();
						if (this.inputState.guessing == 0)
						{
							setVariableStatement.CursorDefinition = cursorDefinition;
							return setVariableStatement;
						}
						return setVariableStatement;
					}
					default:
						if (num != 81)
						{
							goto IL_01FB;
						}
						break;
					}
				}
				else if (num != 93)
				{
					switch (num)
					{
					case 100:
					case 101:
						break;
					default:
						goto IL_01FB;
					}
				}
			}
			else if (num <= 147)
			{
				if (num <= 136)
				{
					if (num != 133 && num != 136)
					{
						goto IL_01FB;
					}
				}
				else if (num != 141 && num != 147)
				{
					goto IL_01FB;
				}
			}
			else if (num <= 193)
			{
				if (num != 163)
				{
					switch (num)
					{
					case 191:
					case 193:
						break;
					case 192:
						goto IL_01FB;
					default:
						goto IL_01FB;
					}
				}
			}
			else
			{
				switch (num)
				{
				case 197:
				case 199:
				case 200:
					break;
				case 198:
					goto IL_01FB;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_01FB;
					default:
						goto IL_01FB;
					}
					break;
				}
			}
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				setVariableStatement.Expression = scalarExpression;
				return setVariableStatement;
			}
			return setVariableStatement;
			IL_01FB:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000AE5CC File Offset: 0x000AC7CC
		public SetStatisticsStatement setStatisticsStatement()
		{
			SetStatisticsStatement setStatisticsStatement = base.FragmentFactory.CreateFragment<SetStatisticsStatement>();
			this.match(146);
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				setStatisticsStatement.Options = SetStatisticsOptionsHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				IToken token2 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					setStatisticsStatement.Options |= SetStatisticsOptionsHelper.Instance.ParseOption(token2, SqlVersionFlags.TSql80);
				}
			}
			this.setOnOff(setStatisticsStatement);
			return setStatisticsStatement;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000AE684 File Offset: 0x000AC884
		public SetRowCountStatement setRowcountStatement()
		{
			SetRowCountStatement setRowCountStatement = base.FragmentFactory.CreateFragment<SetRowCountStatement>();
			this.match(135);
			ValueExpression valueExpression = this.integerOrVariable();
			if (this.inputState.guessing == 0)
			{
				setRowCountStatement.NumberRows = valueExpression;
			}
			return setRowCountStatement;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000AE6C4 File Offset: 0x000AC8C4
		public SetOffsetsStatement setOffsetsStatement()
		{
			SetOffsetsStatement setOffsetsStatement = base.FragmentFactory.CreateFragment<SetOffsetsStatement>();
			this.match(104);
			SetOffsets setOffsets = this.offsetItem();
			if (this.inputState.guessing == 0)
			{
				setOffsetsStatement.Options = setOffsets;
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				setOffsets = this.offsetItem();
				if (this.inputState.guessing == 0)
				{
					setOffsetsStatement.Options |= setOffsets;
				}
			}
			this.setOnOff(setOffsetsStatement);
			return setOffsetsStatement;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000AE748 File Offset: 0x000AC948
		public SetCommandStatement setCommandStatement()
		{
			SetCommandStatement setCommandStatement = base.FragmentFactory.CreateFragment<SetCommandStatement>();
			SetCommand setCommand = this.setCommand();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SetCommand>(setCommandStatement, setCommandStatement.Commands, setCommand);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				setCommand = this.setCommand();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SetCommand>(setCommandStatement, setCommandStatement.Commands, setCommand);
				}
			}
			return setCommandStatement;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x000AE7C0 File Offset: 0x000AC9C0
		public SetTransactionIsolationLevelStatement setTransactionIsolationLevelStatement()
		{
			SetTransactionIsolationLevelStatement setTransactionIsolationLevelStatement = base.FragmentFactory.CreateFragment<SetTransactionIsolationLevelStatement>();
			switch (this.LA(1))
			{
			case 153:
				this.match(153);
				break;
			case 154:
				this.match(154);
				break;
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			IToken token = this.LT(1);
			this.match(232);
			IToken token2 = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "ISOLATION");
				TSql80ParserBaseInternal.Match(token2, "LEVEL");
			}
			if (this.LA(1) == 124)
			{
				this.match(124);
				IToken token3 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					if (TSql80ParserBaseInternal.TryMatch(token3, "COMMITTED"))
					{
						setTransactionIsolationLevelStatement.Level = IsolationLevel.ReadCommitted;
					}
					else
					{
						TSql80ParserBaseInternal.Match(token3, "UNCOMMITTED");
						setTransactionIsolationLevelStatement.Level = IsolationLevel.ReadUncommitted;
					}
					TSql80ParserBaseInternal.UpdateTokenInfo(setTransactionIsolationLevelStatement, token3);
				}
			}
			else if (this.LA(1) == 232 && this.LA(2) == 124)
			{
				IToken token4 = this.LT(1);
				this.match(232);
				IToken token5 = this.LT(1);
				this.match(124);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token4, "REPEATABLE");
					setTransactionIsolationLevelStatement.Level = IsolationLevel.RepeatableRead;
					TSql80ParserBaseInternal.UpdateTokenInfo(setTransactionIsolationLevelStatement, token5);
				}
			}
			else
			{
				if (this.LA(1) != 232 || !TSql80ParserInternal.tokenSet_10_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token6 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token6, "SERIALIZABLE");
					setTransactionIsolationLevelStatement.Level = IsolationLevel.Serializable;
					TSql80ParserBaseInternal.UpdateTokenInfo(setTransactionIsolationLevelStatement, token6);
				}
			}
			return setTransactionIsolationLevelStatement;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x000AE9C8 File Offset: 0x000ACBC8
		public SetTextSizeStatement setTextSizeStatement()
		{
			SetTextSizeStatement setTextSizeStatement = base.FragmentFactory.CreateFragment<SetTextSizeStatement>();
			this.match(149);
			ScalarExpression scalarExpression = this.signedInteger();
			if (this.inputState.guessing == 0)
			{
				setTextSizeStatement.TextSize = scalarExpression;
			}
			return setTextSizeStatement;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x000AEA08 File Offset: 0x000ACC08
		public SetIdentityInsertStatement setIdentityInsertStatement()
		{
			SetIdentityInsertStatement setIdentityInsertStatement = base.FragmentFactory.CreateFragment<SetIdentityInsertStatement>();
			this.match(80);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				setIdentityInsertStatement.Table = schemaObjectName;
			}
			this.setOnOff(setIdentityInsertStatement);
			return setIdentityInsertStatement;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x000AEA4C File Offset: 0x000ACC4C
		public SetErrorLevelStatement setErrorLevelStatement()
		{
			SetErrorLevelStatement setErrorLevelStatement = base.FragmentFactory.CreateFragment<SetErrorLevelStatement>();
			this.match(57);
			ScalarExpression scalarExpression = this.signedInteger();
			if (this.inputState.guessing == 0)
			{
				setErrorLevelStatement.Level = scalarExpression;
			}
			return setErrorLevelStatement;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x000AEA8C File Offset: 0x000ACC8C
		public CursorDefinition cursorDefinition()
		{
			List<CursorOption> list = new List<CursorOption>();
			return this.cursorDefinitionOptions(list);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x000AEAA8 File Offset: 0x000ACCA8
		public void setOnOff(SetOnOffStatement vParent)
		{
			switch (this.LA(1))
			{
			case 103:
			{
				IToken token = this.LT(1);
				this.match(103);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					vParent.IsOn = false;
					return;
				}
				return;
			}
			case 105:
			{
				IToken token2 = this.LT(1);
				this.match(105);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
					vParent.IsOn = true;
					return;
				}
				return;
			}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x000AEB44 File Offset: 0x000ACD44
		public SetOffsets offsetItem()
		{
			SetOffsets setOffsets = SetOffsets.None;
			int num = this.LA(1);
			if (num <= 113)
			{
				if (num <= 61)
				{
					if (num != 29)
					{
						switch (num)
						{
						case 60:
						case 61:
							switch (this.LA(1))
							{
							case 60:
								this.match(60);
								break;
							case 61:
								this.match(61);
								break;
							default:
								throw new NoViableAltException(this.LT(1), this.getFilename());
							}
							if (this.inputState.guessing == 0)
							{
								return SetOffsets.Execute;
							}
							return setOffsets;
						}
					}
					else
					{
						this.match(29);
						if (this.inputState.guessing == 0)
						{
							return SetOffsets.Compute;
						}
						return setOffsets;
					}
				}
				else if (num != 71)
				{
					if (num == 113)
					{
						this.match(113);
						if (this.inputState.guessing == 0)
						{
							return SetOffsets.Order;
						}
						return setOffsets;
					}
				}
				else
				{
					this.match(71);
					if (this.inputState.guessing == 0)
					{
						return SetOffsets.From;
					}
					return setOffsets;
				}
			}
			else if (num <= 140)
			{
				switch (num)
				{
				case 120:
				case 121:
					switch (this.LA(1))
					{
					case 120:
						this.match(120);
						break;
					case 121:
						this.match(121);
						break;
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					if (this.inputState.guessing == 0)
					{
						return SetOffsets.Procedure;
					}
					return setOffsets;
				default:
					if (num == 140)
					{
						this.match(140);
						if (this.inputState.guessing == 0)
						{
							return SetOffsets.Select;
						}
						return setOffsets;
					}
					break;
				}
			}
			else if (num != 148)
			{
				if (num == 232)
				{
					IToken token = this.LT(1);
					this.match(232);
					if (this.inputState.guessing != 0)
					{
						return setOffsets;
					}
					if (TSql80ParserBaseInternal.TryMatch(token, "STATEMENT"))
					{
						return SetOffsets.Statement;
					}
					TSql80ParserBaseInternal.Match(token, "PARAM");
					return SetOffsets.Param;
				}
			}
			else
			{
				this.match(148);
				if (this.inputState.guessing == 0)
				{
					return SetOffsets.Table;
				}
				return setOffsets;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000AED88 File Offset: 0x000ACF88
		public SetCommand setCommand()
		{
			SetCommand setCommand = null;
			if (this.LA(1) == 232 && (this.LA(2) == 72 || this.LA(2) == 103 || this.LA(2) == 232) && base.NextTokenMatches("FIPS_FLAGGER"))
			{
				this.LT(1);
				this.match(232);
				setCommand = this.fipsFlaggerLevel();
			}
			else
			{
				if (this.LA(1) != 232 || !TSql80ParserInternal.tokenSet_24_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				ScalarExpression scalarExpression = this.possibleNegativeConstantOrIdentifier();
				if (this.inputState.guessing == 0)
				{
					GeneralSetCommand generalSetCommand = base.FragmentFactory.CreateFragment<GeneralSetCommand>();
					generalSetCommand.CommandType = GeneralSetCommandTypeHelper.Instance.ParseOption(token);
					generalSetCommand.Parameter = scalarExpression;
					setCommand = generalSetCommand;
				}
			}
			return setCommand;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000AEE7C File Offset: 0x000AD07C
		public SetFipsFlaggerCommand fipsFlaggerLevel()
		{
			SetFipsFlaggerCommand setFipsFlaggerCommand = base.FragmentFactory.CreateFragment<SetFipsFlaggerCommand>();
			int num = this.LA(1);
			if (num != 72)
			{
				if (num != 103)
				{
					if (num != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					IToken token = this.LT(1);
					this.match(232);
					if (this.inputState.guessing == 0)
					{
						setFipsFlaggerCommand.ComplianceLevel = FipsComplianceLevelHelper.Instance.ParseOption(token);
						TSql80ParserBaseInternal.UpdateTokenInfo(setFipsFlaggerCommand, token);
					}
				}
				else
				{
					IToken token2 = this.LT(1);
					this.match(103);
					if (this.inputState.guessing == 0)
					{
						setFipsFlaggerCommand.ComplianceLevel = FipsComplianceLevel.Off;
						TSql80ParserBaseInternal.UpdateTokenInfo(setFipsFlaggerCommand, token2);
					}
				}
			}
			else
			{
				IToken token3 = this.LT(1);
				this.match(72);
				if (this.inputState.guessing == 0)
				{
					setFipsFlaggerCommand.ComplianceLevel = FipsComplianceLevel.Full;
					TSql80ParserBaseInternal.UpdateTokenInfo(setFipsFlaggerCommand, token3);
				}
			}
			return setFipsFlaggerCommand;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000AEF68 File Offset: 0x000AD168
		public DeclareTableVariableBody declareTableBodyMain(IndexAffectingStatement statementType)
		{
			DeclareTableVariableBody declareTableVariableBody = base.FragmentFactory.CreateFragment<DeclareTableVariableBody>();
			this.match(191);
			TableDefinition tableDefinition = this.tableDefinition(statementType, null);
			if (this.inputState.guessing == 0)
			{
				declareTableVariableBody.Definition = tableDefinition;
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(declareTableVariableBody, token);
			}
			return declareTableVariableBody;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000AEFD4 File Offset: 0x000AD1D4
		public TableDefinition tableDefinition(IndexAffectingStatement statementType, AlterTableAddTableElementStatement vStatement)
		{
			TableDefinition tableDefinition = base.FragmentFactory.CreateFragment<TableDefinition>();
			if (base.PhaseOne && vStatement != null)
			{
				vStatement.Definition = tableDefinition;
			}
			this.tableElement(statementType, tableDefinition, vStatement);
			while (this.LA(1) == 198)
			{
				this.match(198);
				this.tableElement(statementType, tableDefinition, vStatement);
			}
			return tableDefinition;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000AF030 File Offset: 0x000AD230
		public void tableElement(IndexAffectingStatement statementType, TableDefinition vParent, AlterTableAddTableElementStatement vStatement)
		{
			int num = this.LA(1);
			if (num <= 47)
			{
				if (num != 21 && num != 30 && num != 47)
				{
					goto IL_0095;
				}
			}
			else if (num <= 118)
			{
				if (num != 68 && num != 118)
				{
					goto IL_0095;
				}
			}
			else if (num != 159)
			{
				switch (num)
				{
				case 232:
				case 233:
				{
					ColumnDefinition columnDefinition = this.columnDefinition(statementType, vStatement);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnDefinition>(vParent, vParent.ColumnDefinitions, columnDefinition);
						return;
					}
					return;
				}
				default:
					goto IL_0095;
				}
			}
			ConstraintDefinition constraintDefinition = this.tableConstraint(statementType, vStatement);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ConstraintDefinition>(vParent, vParent.TableConstraints, constraintDefinition);
				return;
			}
			return;
			IL_0095:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000AF0E8 File Offset: 0x000AD2E8
		public void cursorOpts(bool oldSyntax, IList<CursorOption> vOptions)
		{
			while (this.LA(1) == 232)
			{
				CursorOption cursorOption = this.cursorOption();
				if (this.inputState.guessing == 0)
				{
					if (oldSyntax)
					{
						if (cursorOption.OptionKind != CursorOptionKind.Insensitive && cursorOption.OptionKind != CursorOptionKind.Scroll)
						{
							TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(cursorOption);
						}
					}
					else if (cursorOption.OptionKind == CursorOptionKind.Insensitive)
					{
						TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(cursorOption);
					}
					vOptions.Add(cursorOption);
				}
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000AF14C File Offset: 0x000AD34C
		public CursorDefinition cursorDefinitionOptions(IList<CursorOption> vOptions)
		{
			CursorDefinition cursorDefinition = base.FragmentFactory.CreateFragment<CursorDefinition>();
			this.match(42);
			this.cursorOpts(false, vOptions);
			this.match(67);
			SelectStatement selectStatement = this.selectStatement();
			if (this.inputState.guessing == 0)
			{
				cursorDefinition.Select = selectStatement;
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<CursorOption>(cursorDefinition, cursorDefinition.Options, vOptions);
			}
			return cursorDefinition;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000AF1A8 File Offset: 0x000AD3A8
		public SelectStatement selectStatement()
		{
			return this.select();
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000AF1C0 File Offset: 0x000AD3C0
		public CursorOption cursorOption()
		{
			CursorOption cursorOption = base.FragmentFactory.CreateFragment<CursorOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				cursorOption.OptionKind = CursorOptionsHelper.Instance.ParseOption(token);
			}
			return cursorOption;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000AF210 File Offset: 0x000AD410
		public void indexLegacyOptionList(CreateIndexStatement vParent)
		{
			IndexOption indexOption = this.indexLegacyOption();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.VerifyAllowedIndexOption(IndexAffectingStatement.CreateIndex, indexOption);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<IndexOption>(vParent, vParent.IndexOptions, indexOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				indexOption = this.indexLegacyOption();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.VerifyAllowedIndexOption(IndexAffectingStatement.CreateIndex, indexOption);
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<IndexOption>(vParent, vParent.IndexOptions, indexOption);
				}
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000AF288 File Offset: 0x000AD488
		public FileGroupOrPartitionScheme filegroupOrPartitionScheme()
		{
			FileGroupOrPartitionScheme fileGroupOrPartitionScheme = base.FragmentFactory.CreateFragment<FileGroupOrPartitionScheme>();
			IdentifierOrValueExpression identifierOrValueExpression = this.stringOrIdentifier();
			if (this.inputState.guessing == 0)
			{
				fileGroupOrPartitionScheme.Name = identifierOrValueExpression;
			}
			return fileGroupOrPartitionScheme;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000AF2C0 File Offset: 0x000AD4C0
		public IndexOption indexLegacyOption()
		{
			IndexOption indexOption = null;
			int num = this.LA(1);
			if (num != 66)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					IndexStateOption indexStateOption = base.FragmentFactory.CreateFragment<IndexStateOption>();
					indexOption = indexStateOption;
					indexStateOption.OptionKind = TSql80ParserBaseInternal.ParseIndexLegacyWithOption(token);
					TSql80ParserBaseInternal.UpdateTokenInfo(indexStateOption, token);
					indexStateOption.OptionState = OptionState.On;
				}
			}
			else
			{
				indexOption = this.fillFactorOption();
			}
			return indexOption;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000AF350 File Offset: 0x000AD550
		public IndexExpressionOption fillFactorOption()
		{
			IndexExpressionOption indexExpressionOption = base.FragmentFactory.CreateFragment<IndexExpressionOption>();
			IToken token = this.LT(1);
			this.match(66);
			this.match(206);
			Literal literal = this.integer();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckFillFactorRange(literal);
				indexExpressionOption.OptionKind = IndexOptionKind.FillFactor;
				indexExpressionOption.Expression = literal;
				TSql80ParserBaseInternal.UpdateTokenInfo(indexExpressionOption, token);
			}
			return indexExpressionOption;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000AF3B8 File Offset: 0x000AD5B8
		public QueryExpression subqueryExpression()
		{
			BinaryQueryExpression binaryQueryExpression = null;
			QueryExpression queryExpression = this.subqueryExpressionUnit();
			while (this.LA(1) == 59 || this.LA(1) == 87 || this.LA(1) == 158)
			{
				if (this.inputState.guessing == 0)
				{
					binaryQueryExpression = base.FragmentFactory.CreateFragment<BinaryQueryExpression>();
					binaryQueryExpression.FirstQueryExpression = queryExpression;
				}
				int num = this.LA(1);
				if (num != 59)
				{
					if (num != 87)
					{
						if (num != 158)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						this.match(158);
						if (this.inputState.guessing == 0)
						{
							binaryQueryExpression.BinaryQueryExpressionType = BinaryQueryExpressionType.Union;
						}
					}
					else
					{
						this.match(87);
						if (this.inputState.guessing == 0)
						{
							binaryQueryExpression.BinaryQueryExpressionType = BinaryQueryExpressionType.Intersect;
						}
					}
				}
				else
				{
					this.match(59);
					if (this.inputState.guessing == 0)
					{
						binaryQueryExpression.BinaryQueryExpressionType = BinaryQueryExpressionType.Except;
					}
				}
				int num2 = this.LA(1);
				if (num2 != 5)
				{
					if (num2 != 140 && num2 != 191)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					this.match(5);
					if (this.inputState.guessing == 0)
					{
						binaryQueryExpression.All = true;
					}
				}
				queryExpression = this.subqueryExpressionUnit();
				if (this.inputState.guessing == 0)
				{
					binaryQueryExpression.SecondQueryExpression = queryExpression;
					queryExpression = binaryQueryExpression;
				}
			}
			return queryExpression;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000AF518 File Offset: 0x000AD718
		public QueryExpression queryExpression(SelectStatement vSelectStatement)
		{
			BinaryQueryExpression binaryQueryExpression = null;
			QueryExpression queryExpression = this.queryExpressionUnit(vSelectStatement);
			while (this.LA(1) == 59 || this.LA(1) == 87 || this.LA(1) == 158)
			{
				if (this.inputState.guessing == 0)
				{
					binaryQueryExpression = base.FragmentFactory.CreateFragment<BinaryQueryExpression>();
					binaryQueryExpression.FirstQueryExpression = queryExpression;
				}
				int num = this.LA(1);
				if (num != 59)
				{
					if (num != 87)
					{
						if (num != 158)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						this.match(158);
						if (this.inputState.guessing == 0)
						{
							binaryQueryExpression.BinaryQueryExpressionType = BinaryQueryExpressionType.Union;
						}
					}
					else
					{
						this.match(87);
						if (this.inputState.guessing == 0)
						{
							binaryQueryExpression.BinaryQueryExpressionType = BinaryQueryExpressionType.Intersect;
						}
					}
				}
				else
				{
					this.match(59);
					if (this.inputState.guessing == 0)
					{
						binaryQueryExpression.BinaryQueryExpressionType = BinaryQueryExpressionType.Except;
					}
				}
				int num2 = this.LA(1);
				if (num2 != 5)
				{
					if (num2 != 140 && num2 != 191)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					this.match(5);
					if (this.inputState.guessing == 0)
					{
						binaryQueryExpression.All = true;
					}
				}
				queryExpression = this.queryExpressionUnit(null);
				if (this.inputState.guessing == 0)
				{
					binaryQueryExpression.SecondQueryExpression = queryExpression;
					queryExpression = binaryQueryExpression;
				}
			}
			return queryExpression;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000AF678 File Offset: 0x000AD878
		public OrderByClause orderByClause()
		{
			OrderByClause orderByClause = base.FragmentFactory.CreateFragment<OrderByClause>();
			IToken token = this.LT(1);
			this.match(113);
			this.match(18);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(orderByClause, token);
			}
			ExpressionWithSortOrder expressionWithSortOrder = this.expressionWithSortOrder();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ExpressionWithSortOrder>(orderByClause, orderByClause.OrderByElements, expressionWithSortOrder);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				expressionWithSortOrder = this.expressionWithSortOrder();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ExpressionWithSortOrder>(orderByClause, orderByClause.OrderByElements, expressionWithSortOrder);
				}
			}
			return orderByClause;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000AF720 File Offset: 0x000AD920
		public ComputeClause computeClause()
		{
			ComputeClause computeClause = base.FragmentFactory.CreateFragment<ComputeClause>();
			IToken token = this.LT(1);
			this.match(29);
			ComputeFunction computeFunction = this.computeFunction();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(computeClause, token);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ComputeFunction>(computeClause, computeClause.ComputeFunctions, computeFunction);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				computeFunction = this.computeFunction();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ComputeFunction>(computeClause, computeClause.ComputeFunctions, computeFunction);
				}
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return computeClause;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
						case 22:
						case 23:
							return computeClause;
						case 14:
						case 16:
						case 19:
						case 20:
						case 21:
							break;
						case 18:
							this.match(18);
							this.expressionList(computeClause, computeClause.ByExpressions);
							return computeClause;
						default:
							switch (num)
							{
							case 28:
							case 29:
								return computeClause;
							default:
								switch (num)
								{
								case 33:
								case 35:
									return computeClause;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
						return computeClause;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
						break;
					default:
						if (num == 67)
						{
							return computeClause;
						}
						switch (num)
						{
						case 74:
						case 75:
							return computeClause;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return computeClause;
				}
			}
			else if (num <= 162)
			{
				if (num <= 111)
				{
					if (num == 95 || num == 106 || num == 111)
					{
						return computeClause;
					}
				}
				else
				{
					if (num == 119)
					{
						return computeClause;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return computeClause;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return computeClause;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return computeClause;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						return computeClause;
					}
					switch (num)
					{
					case 180:
					case 181:
						return computeClause;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return computeClause;
				}
				switch (num)
				{
				case 219:
				case 220:
					return computeClause;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000AFA40 File Offset: 0x000ADC40
		public ForClause forClause()
		{
			ForClause forClause = null;
			IToken token = this.LT(1);
			this.match(67);
			int num = this.LA(1);
			if (num <= 124)
			{
				if (num != 16)
				{
					if (num == 124)
					{
						this.match(124);
						IToken token2 = this.LT(1);
						this.match(232);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.Match(token2, "ONLY");
							forClause = base.FragmentFactory.CreateFragment<ReadOnlyForClause>();
							TSql80ParserBaseInternal.UpdateTokenInfo(forClause, token2);
							return forClause;
						}
						return forClause;
					}
				}
				else
				{
					forClause = this.browseForClause();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(forClause, token);
						return forClause;
					}
					return forClause;
				}
			}
			else if (num != 160)
			{
				if (num == 232)
				{
					forClause = this.xmlForClause();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(forClause, token);
						return forClause;
					}
					return forClause;
				}
			}
			else
			{
				forClause = this.updateForClause();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(forClause, token);
					return forClause;
				}
				return forClause;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000AFB50 File Offset: 0x000ADD50
		public void optimizerHints(TSqlFragment vParent, IList<OptimizerHint> hintsCollection)
		{
			IToken token = this.LT(1);
			this.match(111);
			this.match(191);
			OptimizerHint optimizerHint = this.hint();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<OptimizerHint>(vParent, hintsCollection, optimizerHint);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				optimizerHint = this.hint();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<OptimizerHint>(vParent, hintsCollection, optimizerHint);
				}
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000AFC00 File Offset: 0x000ADE00
		public QueryDerivedTable derivedTable()
		{
			QueryDerivedTable queryDerivedTable = base.FragmentFactory.CreateFragment<QueryDerivedTable>();
			IToken token = this.LT(1);
			this.match(191);
			QueryExpression queryExpression = this.subqueryExpression();
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				queryDerivedTable.QueryExpression = queryExpression;
				TSql80ParserBaseInternal.UpdateTokenInfo(queryDerivedTable, token);
				TSql80ParserBaseInternal.UpdateTokenInfo(queryDerivedTable, token2);
			}
			this.simpleTableReferenceAlias(queryDerivedTable);
			if (this.LA(1) == 191 && (this.LA(2) == 232 || this.LA(2) == 233))
			{
				this.columnNameList(queryDerivedTable, queryDerivedTable.Columns);
			}
			else if (!TSql80ParserInternal.tokenSet_32_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_33_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return queryDerivedTable;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000AFCE8 File Offset: 0x000ADEE8
		public void simpleTableReferenceAlias(TableReferenceWithAlias vParent)
		{
			int num = this.LA(1);
			if (num != 9)
			{
				switch (num)
				{
				case 232:
				case 233:
					break;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				this.match(9);
			}
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				vParent.Alias = identifier;
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000AFD50 File Offset: 0x000ADF50
		public ScalarSubquery subquery(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			ScalarSubquery scalarSubquery = base.FragmentFactory.CreateFragment<ScalarSubquery>();
			IToken token = this.LT(1);
			this.match(191);
			QueryExpression queryExpression = this.subqueryExpression();
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				if (ExpressionFlags.ScalarSubqueriesDisallowed == (expressionFlags & ExpressionFlags.ScalarSubqueriesDisallowed))
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46098", queryExpression, TSqlParserResource.SQL46098Message, new string[0]);
				}
				scalarSubquery.QueryExpression = queryExpression;
				TSql80ParserBaseInternal.UpdateTokenInfo(scalarSubquery, token);
				TSql80ParserBaseInternal.UpdateTokenInfo(scalarSubquery, token2);
			}
			return scalarSubquery;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000AFDDC File Offset: 0x000ADFDC
		public QueryExpression subqueryExpressionUnit()
		{
			int num = this.LA(1);
			QueryExpression queryExpression;
			if (num != 140)
			{
				if (num != 191)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				queryExpression = this.subqueryParenthesis();
			}
			else
			{
				queryExpression = this.subquerySpecification();
			}
			return queryExpression;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000AFE2C File Offset: 0x000AE02C
		public QuerySpecification subquerySpecification()
		{
			QuerySpecification querySpecification = base.FragmentFactory.CreateFragment<QuerySpecification>();
			IToken token = this.LT(1);
			this.match(140);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(querySpecification, token);
			}
			int num = this.LA(1);
			if (num <= 93)
			{
				if (num <= 34)
				{
					if (num <= 20)
					{
						if (num != 5)
						{
							if (num != 20)
							{
								goto IL_0213;
							}
							goto IL_0226;
						}
					}
					else
					{
						if (num != 25 && num != 34)
						{
							goto IL_0213;
						}
						goto IL_0226;
					}
				}
				else if (num <= 51)
				{
					switch (num)
					{
					case 40:
					case 41:
						goto IL_0226;
					default:
						if (num != 51)
						{
							goto IL_0213;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 79:
					case 81:
						goto IL_0226;
					case 80:
						goto IL_0213;
					default:
						if (num != 93)
						{
							goto IL_0213;
						}
						goto IL_0226;
					}
				}
				UniqueRowFilter uniqueRowFilter = this.uniqueRowFilter();
				if (this.inputState.guessing == 0)
				{
					querySpecification.UniqueRowFilter = uniqueRowFilter;
					goto IL_0226;
				}
				goto IL_0226;
			}
			else if (num <= 141)
			{
				if (num <= 133)
				{
					switch (num)
					{
					case 100:
					case 101:
						goto IL_0226;
					default:
						if (num == 133)
						{
							goto IL_0226;
						}
						break;
					}
				}
				else if (num == 136 || num == 141)
				{
					goto IL_0226;
				}
			}
			else if (num <= 152)
			{
				if (num == 147 || num == 152)
				{
					goto IL_0226;
				}
			}
			else
			{
				if (num == 163)
				{
					goto IL_0226;
				}
				switch (num)
				{
				case 191:
				case 193:
				case 195:
				case 197:
				case 199:
				case 200:
					goto IL_0226;
				case 192:
				case 194:
				case 196:
				case 198:
					break;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						goto IL_0226;
					}
					break;
				}
			}
			IL_0213:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0226:
			int num2 = this.LA(1);
			if (num2 <= 101)
			{
				if (num2 <= 34)
				{
					if (num2 == 20 || num2 == 25 || num2 == 34)
					{
						goto IL_03F8;
					}
				}
				else if (num2 <= 81)
				{
					switch (num2)
					{
					case 40:
					case 41:
						goto IL_03F8;
					default:
						switch (num2)
						{
						case 79:
						case 81:
							goto IL_03F8;
						}
						break;
					}
				}
				else
				{
					if (num2 == 93)
					{
						goto IL_03F8;
					}
					switch (num2)
					{
					case 100:
					case 101:
						goto IL_03F8;
					}
				}
			}
			else if (num2 <= 147)
			{
				if (num2 <= 136)
				{
					if (num2 == 133 || num2 == 136)
					{
						goto IL_03F8;
					}
				}
				else if (num2 == 141 || num2 == 147)
				{
					goto IL_03F8;
				}
			}
			else if (num2 <= 163)
			{
				if (num2 != 152)
				{
					if (num2 == 163)
					{
						goto IL_03F8;
					}
				}
				else
				{
					TopRowFilter topRowFilter = this.topRowFilter();
					if (this.inputState.guessing == 0)
					{
						querySpecification.TopRowFilter = topRowFilter;
						goto IL_03F8;
					}
					goto IL_03F8;
				}
			}
			else
			{
				switch (num2)
				{
				case 191:
				case 193:
				case 195:
				case 197:
				case 199:
				case 200:
					goto IL_03F8;
				case 192:
				case 194:
				case 196:
				case 198:
					break;
				default:
					switch (num2)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						goto IL_03F8;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_03F8:
			SelectElement selectElement = this.selectColumnOrStarExpression();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SelectElement>(querySpecification, querySpecification.SelectElements, selectElement);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				selectElement = this.selectColumnOrStarExpression();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SelectElement>(querySpecification, querySpecification.SelectElements, selectElement);
				}
			}
			FromClause fromClause = this.fromClauseOpt();
			if (this.inputState.guessing == 0)
			{
				querySpecification.FromClause = fromClause;
			}
			int num3 = this.LA(1);
			if (num3 <= 77)
			{
				if (num3 <= 35)
				{
					if (num3 == 1 || num3 == 35)
					{
						goto IL_054A;
					}
				}
				else
				{
					if (num3 == 59 || num3 == 67)
					{
						goto IL_054A;
					}
					switch (num3)
					{
					case 75:
					case 76:
					case 77:
						goto IL_054A;
					}
				}
			}
			else if (num3 <= 158)
			{
				if (num3 == 87 || num3 == 113 || num3 == 158)
				{
					goto IL_054A;
				}
			}
			else
			{
				switch (num3)
				{
				case 169:
				{
					WhereClause whereClause = this.whereClause();
					if (this.inputState.guessing == 0)
					{
						querySpecification.WhereClause = whereClause;
						goto IL_054A;
					}
					goto IL_054A;
				}
				case 170:
					break;
				case 171:
					goto IL_054A;
				default:
					if (num3 == 192 || num3 == 219)
					{
						goto IL_054A;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_054A:
			int num4 = this.LA(1);
			if (num4 <= 77)
			{
				if (num4 <= 35)
				{
					if (num4 == 1 || num4 == 35)
					{
						goto IL_060E;
					}
				}
				else
				{
					if (num4 == 59 || num4 == 67)
					{
						goto IL_060E;
					}
					switch (num4)
					{
					case 75:
					case 77:
						goto IL_060E;
					case 76:
					{
						GroupByClause groupByClause = this.groupByClause();
						if (this.inputState.guessing == 0)
						{
							querySpecification.GroupByClause = groupByClause;
							goto IL_060E;
						}
						goto IL_060E;
					}
					}
				}
			}
			else if (num4 <= 158)
			{
				if (num4 == 87 || num4 == 113 || num4 == 158)
				{
					goto IL_060E;
				}
			}
			else if (num4 == 171 || num4 == 192 || num4 == 219)
			{
				goto IL_060E;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_060E:
			int num5 = this.LA(1);
			if (num5 <= 77)
			{
				if (num5 <= 35)
				{
					if (num5 == 1 || num5 == 35)
					{
						goto IL_06D2;
					}
				}
				else
				{
					if (num5 == 59 || num5 == 67)
					{
						goto IL_06D2;
					}
					switch (num5)
					{
					case 75:
						goto IL_06D2;
					case 77:
					{
						HavingClause havingClause = this.havingClause();
						if (this.inputState.guessing == 0)
						{
							querySpecification.HavingClause = havingClause;
							goto IL_06D2;
						}
						goto IL_06D2;
					}
					}
				}
			}
			else if (num5 <= 158)
			{
				if (num5 == 87 || num5 == 113 || num5 == 158)
				{
					goto IL_06D2;
				}
			}
			else if (num5 == 171 || num5 == 192 || num5 == 219)
			{
				goto IL_06D2;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_06D2:
			int num6 = this.LA(1);
			if (num6 <= 75)
			{
				if (num6 <= 35)
				{
					if (num6 == 1 || num6 == 35)
					{
						goto IL_077D;
					}
				}
				else if (num6 == 59 || num6 == 67 || num6 == 75)
				{
					goto IL_077D;
				}
			}
			else if (num6 <= 158)
			{
				if (num6 == 87)
				{
					goto IL_077D;
				}
				if (num6 != 113)
				{
					if (num6 == 158)
					{
						goto IL_077D;
					}
				}
				else
				{
					OrderByClause orderByClause = this.orderByClause();
					if (this.inputState.guessing == 0)
					{
						querySpecification.OrderByClause = orderByClause;
						goto IL_077D;
					}
					goto IL_077D;
				}
			}
			else if (num6 == 171 || num6 == 192 || num6 == 219)
			{
				goto IL_077D;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_077D:
			if (this.LA(1) == 67 && this.LA(1) == 67 && this.LA(2) == 16)
			{
				this.match(67);
				BrowseForClause browseForClause = this.browseForClause();
				if (this.inputState.guessing == 0)
				{
					querySpecification.ForClause = browseForClause;
				}
			}
			else if (!TSql80ParserInternal.tokenSet_34_.member(this.LA(1)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			if (this.inputState.guessing == 0)
			{
				if (querySpecification.OrderByClause != null && querySpecification.TopRowFilter == null)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46047", querySpecification, TSqlParserResource.SQL46047Message, new string[0]);
				}
				if (querySpecification.TopRowFilter != null && querySpecification.TopRowFilter.WithTies && querySpecification.OrderByClause == null)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46048", querySpecification, TSqlParserResource.SQL46048Message, new string[0]);
				}
			}
			return querySpecification;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000B068C File Offset: 0x000AE88C
		public QueryParenthesisExpression subqueryParenthesis()
		{
			QueryParenthesisExpression queryParenthesisExpression = base.FragmentFactory.CreateFragment<QueryParenthesisExpression>();
			IToken token = this.LT(1);
			this.match(191);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(queryParenthesisExpression, token);
			}
			QueryExpression queryExpression = this.subqueryExpression();
			if (this.inputState.guessing == 0)
			{
				queryParenthesisExpression.QueryExpression = queryExpression;
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(queryParenthesisExpression, token2);
			}
			return queryParenthesisExpression;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x000B0714 File Offset: 0x000AE914
		public QueryExpression queryExpressionUnit(SelectStatement vSelectStatement)
		{
			int num = this.LA(1);
			QueryExpression queryExpression;
			if (num != 140)
			{
				if (num != 191)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				queryExpression = this.queryParenthesis(vSelectStatement);
			}
			else
			{
				queryExpression = this.querySpecification(vSelectStatement);
			}
			return queryExpression;
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x000B0768 File Offset: 0x000AE968
		public QuerySpecification querySpecification(SelectStatement vSelectStatement)
		{
			QuerySpecification querySpecification = base.FragmentFactory.CreateFragment<QuerySpecification>();
			IToken token = this.LT(1);
			this.match(140);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(querySpecification, token);
			}
			int num = this.LA(1);
			if (num <= 93)
			{
				if (num <= 34)
				{
					if (num <= 20)
					{
						if (num != 5)
						{
							if (num != 20)
							{
								goto IL_0217;
							}
							goto IL_022A;
						}
					}
					else
					{
						if (num != 25 && num != 34)
						{
							goto IL_0217;
						}
						goto IL_022A;
					}
				}
				else if (num <= 51)
				{
					switch (num)
					{
					case 40:
					case 41:
						goto IL_022A;
					default:
						if (num != 51)
						{
							goto IL_0217;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 79:
					case 81:
						goto IL_022A;
					case 80:
						goto IL_0217;
					default:
						if (num != 93)
						{
							goto IL_0217;
						}
						goto IL_022A;
					}
				}
				UniqueRowFilter uniqueRowFilter = this.uniqueRowFilter();
				if (this.inputState.guessing == 0)
				{
					querySpecification.UniqueRowFilter = uniqueRowFilter;
					goto IL_022A;
				}
				goto IL_022A;
			}
			else if (num <= 141)
			{
				if (num <= 133)
				{
					switch (num)
					{
					case 100:
					case 101:
						goto IL_022A;
					default:
						if (num == 133)
						{
							goto IL_022A;
						}
						break;
					}
				}
				else if (num == 136 || num == 141)
				{
					goto IL_022A;
				}
			}
			else if (num <= 152)
			{
				if (num == 147 || num == 152)
				{
					goto IL_022A;
				}
			}
			else
			{
				if (num == 163)
				{
					goto IL_022A;
				}
				switch (num)
				{
				case 191:
				case 193:
				case 195:
				case 197:
				case 199:
				case 200:
					goto IL_022A;
				case 192:
				case 194:
				case 196:
				case 198:
					break;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						goto IL_022A;
					}
					break;
				}
			}
			IL_0217:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_022A:
			int num2 = this.LA(1);
			if (num2 <= 101)
			{
				if (num2 <= 34)
				{
					if (num2 == 20 || num2 == 25 || num2 == 34)
					{
						goto IL_03FC;
					}
				}
				else if (num2 <= 81)
				{
					switch (num2)
					{
					case 40:
					case 41:
						goto IL_03FC;
					default:
						switch (num2)
						{
						case 79:
						case 81:
							goto IL_03FC;
						}
						break;
					}
				}
				else
				{
					if (num2 == 93)
					{
						goto IL_03FC;
					}
					switch (num2)
					{
					case 100:
					case 101:
						goto IL_03FC;
					}
				}
			}
			else if (num2 <= 147)
			{
				if (num2 <= 136)
				{
					if (num2 == 133 || num2 == 136)
					{
						goto IL_03FC;
					}
				}
				else if (num2 == 141 || num2 == 147)
				{
					goto IL_03FC;
				}
			}
			else if (num2 <= 163)
			{
				if (num2 != 152)
				{
					if (num2 == 163)
					{
						goto IL_03FC;
					}
				}
				else
				{
					TopRowFilter topRowFilter = this.topRowFilter();
					if (this.inputState.guessing == 0)
					{
						querySpecification.TopRowFilter = topRowFilter;
						goto IL_03FC;
					}
					goto IL_03FC;
				}
			}
			else
			{
				switch (num2)
				{
				case 191:
				case 193:
				case 195:
				case 197:
				case 199:
				case 200:
					goto IL_03FC;
				case 192:
				case 194:
				case 196:
				case 198:
					break;
				default:
					switch (num2)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						goto IL_03FC;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_03FC:
			this.selectExpression(querySpecification);
			while (this.LA(1) == 198)
			{
				this.match(198);
				this.selectExpression(querySpecification);
			}
			int num3 = this.LA(1);
			if (num3 <= 92)
			{
				if (num3 <= 23)
				{
					if (num3 <= 6)
					{
						if (num3 == 1 || num3 == 6)
						{
							goto IL_0739;
						}
					}
					else
					{
						switch (num3)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_0739;
						case 14:
						case 16:
							break;
						default:
							switch (num3)
							{
							case 22:
							case 23:
								goto IL_0739;
							}
							break;
						}
					}
				}
				else if (num3 <= 35)
				{
					switch (num3)
					{
					case 28:
					case 29:
						goto IL_0739;
					default:
						switch (num3)
						{
						case 33:
						case 35:
							goto IL_0739;
						}
						break;
					}
				}
				else
				{
					switch (num3)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 59:
					case 60:
					case 61:
					case 64:
					case 67:
					case 71:
					case 74:
					case 75:
					case 76:
					case 77:
						goto IL_0739;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 62:
					case 63:
					case 65:
					case 66:
					case 68:
					case 69:
					case 70:
					case 72:
					case 73:
						break;
					default:
						switch (num3)
						{
						case 82:
						case 86:
						case 87:
							goto IL_0739;
						case 83:
						case 84:
						case 85:
							break;
						case 88:
						{
							IToken token2 = this.LT(1);
							this.match(88);
							SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
							if (this.inputState.guessing == 0)
							{
								if (vSelectStatement == null)
								{
									TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(token2);
								}
								vSelectStatement.Into = schemaObjectName;
								goto IL_0739;
							}
							goto IL_0739;
						}
						default:
							if (num3 == 92)
							{
								goto IL_0739;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num3 <= 144)
			{
				if (num3 <= 106)
				{
					if (num3 == 95 || num3 == 106)
					{
						goto IL_0739;
					}
				}
				else
				{
					switch (num3)
					{
					case 111:
					case 113:
						goto IL_0739;
					case 112:
						break;
					default:
						if (num3 == 119)
						{
							goto IL_0739;
						}
						switch (num3)
						{
						case 123:
						case 125:
						case 126:
						case 129:
						case 131:
						case 132:
						case 134:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							goto IL_0739;
						}
						break;
					}
				}
			}
			else if (num3 <= 181)
			{
				switch (num3)
				{
				case 156:
				case 158:
				case 160:
				case 161:
				case 162:
				case 167:
				case 169:
				case 170:
				case 172:
				case 173:
				case 176:
					goto IL_0739;
				case 157:
				case 159:
				case 163:
				case 164:
				case 165:
				case 166:
				case 168:
				case 171:
				case 174:
				case 175:
					break;
				default:
					switch (num3)
					{
					case 180:
					case 181:
						goto IL_0739;
					}
					break;
				}
			}
			else
			{
				switch (num3)
				{
				case 191:
				case 192:
					goto IL_0739;
				default:
					if (num3 == 204)
					{
						goto IL_0739;
					}
					switch (num3)
					{
					case 219:
					case 220:
						goto IL_0739;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0739:
			FromClause fromClause = this.fromClauseOpt();
			if (this.inputState.guessing == 0)
			{
				querySpecification.FromClause = fromClause;
			}
			int num4 = this.LA(1);
			if (num4 <= 92)
			{
				if (num4 <= 29)
				{
					if (num4 <= 6)
					{
						if (num4 == 1 || num4 == 6)
						{
							goto IL_0A46;
						}
					}
					else
					{
						switch (num4)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_0A46;
						case 14:
						case 16:
							break;
						default:
							switch (num4)
							{
							case 22:
							case 23:
								goto IL_0A46;
							default:
								switch (num4)
								{
								case 28:
								case 29:
									goto IL_0A46;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num4 <= 67)
				{
					switch (num4)
					{
					case 33:
					case 35:
						goto IL_0A46;
					case 34:
						break;
					default:
						switch (num4)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 59:
						case 60:
						case 61:
						case 64:
						case 67:
							goto IL_0A46;
						}
						break;
					}
				}
				else
				{
					switch (num4)
					{
					case 74:
					case 75:
					case 76:
					case 77:
					case 82:
						goto IL_0A46;
					case 78:
					case 79:
					case 80:
					case 81:
						break;
					default:
						switch (num4)
						{
						case 86:
						case 87:
							goto IL_0A46;
						default:
							if (num4 == 92)
							{
								goto IL_0A46;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num4 <= 144)
			{
				if (num4 <= 106)
				{
					if (num4 == 95 || num4 == 106)
					{
						goto IL_0A46;
					}
				}
				else
				{
					switch (num4)
					{
					case 111:
					case 113:
						goto IL_0A46;
					case 112:
						break;
					default:
						if (num4 == 119)
						{
							goto IL_0A46;
						}
						switch (num4)
						{
						case 123:
						case 125:
						case 126:
						case 129:
						case 131:
						case 132:
						case 134:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							goto IL_0A46;
						}
						break;
					}
				}
			}
			else if (num4 <= 181)
			{
				switch (num4)
				{
				case 156:
				case 158:
				case 160:
				case 161:
				case 162:
				case 167:
				case 170:
				case 172:
				case 173:
				case 176:
					goto IL_0A46;
				case 157:
				case 159:
				case 163:
				case 164:
				case 165:
				case 166:
				case 168:
				case 171:
				case 174:
				case 175:
					break;
				case 169:
				{
					WhereClause whereClause = this.whereClause();
					if (this.inputState.guessing == 0)
					{
						querySpecification.WhereClause = whereClause;
						goto IL_0A46;
					}
					goto IL_0A46;
				}
				default:
					switch (num4)
					{
					case 180:
					case 181:
						goto IL_0A46;
					}
					break;
				}
			}
			else
			{
				switch (num4)
				{
				case 191:
				case 192:
					goto IL_0A46;
				default:
					if (num4 == 204)
					{
						goto IL_0A46;
					}
					switch (num4)
					{
					case 219:
					case 220:
						goto IL_0A46;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0A46:
			int num5 = this.LA(1);
			if (num5 <= 95)
			{
				if (num5 <= 29)
				{
					if (num5 <= 6)
					{
						if (num5 == 1 || num5 == 6)
						{
							goto IL_0D30;
						}
					}
					else
					{
						switch (num5)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_0D30;
						case 14:
						case 16:
							break;
						default:
							switch (num5)
							{
							case 22:
							case 23:
								goto IL_0D30;
							default:
								switch (num5)
								{
								case 28:
								case 29:
									goto IL_0D30;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num5 <= 82)
				{
					switch (num5)
					{
					case 33:
					case 35:
						goto IL_0D30;
					case 34:
						break;
					default:
						switch (num5)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 59:
						case 60:
						case 61:
						case 64:
						case 67:
							goto IL_0D30;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 62:
						case 63:
						case 65:
						case 66:
							break;
						default:
							switch (num5)
							{
							case 74:
							case 75:
							case 77:
							case 82:
								goto IL_0D30;
							case 76:
							{
								GroupByClause groupByClause = this.groupByClause();
								if (this.inputState.guessing == 0)
								{
									querySpecification.GroupByClause = groupByClause;
									goto IL_0D30;
								}
								goto IL_0D30;
							}
							}
							break;
						}
						break;
					}
				}
				else
				{
					switch (num5)
					{
					case 86:
					case 87:
						goto IL_0D30;
					default:
						if (num5 == 92 || num5 == 95)
						{
							goto IL_0D30;
						}
						break;
					}
				}
			}
			else if (num5 <= 162)
			{
				if (num5 <= 113)
				{
					if (num5 == 106)
					{
						goto IL_0D30;
					}
					switch (num5)
					{
					case 111:
					case 113:
						goto IL_0D30;
					}
				}
				else
				{
					if (num5 == 119)
					{
						goto IL_0D30;
					}
					switch (num5)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_0D30;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num5)
						{
						case 156:
						case 158:
						case 160:
						case 161:
						case 162:
							goto IL_0D30;
						}
						break;
					}
				}
			}
			else if (num5 <= 181)
			{
				switch (num5)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					goto IL_0D30;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num5 == 176)
					{
						goto IL_0D30;
					}
					switch (num5)
					{
					case 180:
					case 181:
						goto IL_0D30;
					}
					break;
				}
			}
			else
			{
				switch (num5)
				{
				case 191:
				case 192:
					goto IL_0D30;
				default:
					if (num5 == 204)
					{
						goto IL_0D30;
					}
					switch (num5)
					{
					case 219:
					case 220:
						goto IL_0D30;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0D30:
			int num6 = this.LA(1);
			if (num6 <= 92)
			{
				if (num6 <= 29)
				{
					if (num6 <= 6)
					{
						if (num6 == 1 || num6 == 6)
						{
							return querySpecification;
						}
					}
					else
					{
						switch (num6)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return querySpecification;
						case 14:
						case 16:
							break;
						default:
							switch (num6)
							{
							case 22:
							case 23:
								return querySpecification;
							default:
								switch (num6)
								{
								case 28:
								case 29:
									return querySpecification;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num6 <= 77)
				{
					switch (num6)
					{
					case 33:
					case 35:
						return querySpecification;
					case 34:
						break;
					default:
						switch (num6)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 59:
						case 60:
						case 61:
						case 64:
						case 67:
							return querySpecification;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 62:
						case 63:
						case 65:
						case 66:
							break;
						default:
							switch (num6)
							{
							case 74:
							case 75:
								return querySpecification;
							case 77:
							{
								HavingClause havingClause = this.havingClause();
								if (this.inputState.guessing == 0)
								{
									querySpecification.HavingClause = havingClause;
									return querySpecification;
								}
								return querySpecification;
							}
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (num6 == 82)
					{
						return querySpecification;
					}
					switch (num6)
					{
					case 86:
					case 87:
						return querySpecification;
					default:
						if (num6 == 92)
						{
							return querySpecification;
						}
						break;
					}
				}
			}
			else if (num6 <= 162)
			{
				if (num6 <= 113)
				{
					if (num6 == 95 || num6 == 106)
					{
						return querySpecification;
					}
					switch (num6)
					{
					case 111:
					case 113:
						return querySpecification;
					}
				}
				else
				{
					if (num6 == 119)
					{
						return querySpecification;
					}
					switch (num6)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return querySpecification;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num6)
						{
						case 156:
						case 158:
						case 160:
						case 161:
						case 162:
							return querySpecification;
						}
						break;
					}
				}
			}
			else if (num6 <= 181)
			{
				switch (num6)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return querySpecification;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num6 == 176)
					{
						return querySpecification;
					}
					switch (num6)
					{
					case 180:
					case 181:
						return querySpecification;
					}
					break;
				}
			}
			else
			{
				switch (num6)
				{
				case 191:
				case 192:
					return querySpecification;
				default:
					if (num6 == 204)
					{
						return querySpecification;
					}
					switch (num6)
					{
					case 219:
					case 220:
						return querySpecification;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x000B1788 File Offset: 0x000AF988
		public QueryParenthesisExpression queryParenthesis(SelectStatement vSelectStatement)
		{
			QueryParenthesisExpression queryParenthesisExpression = base.FragmentFactory.CreateFragment<QueryParenthesisExpression>();
			IToken token = this.LT(1);
			this.match(191);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(queryParenthesisExpression, token);
			}
			QueryExpression queryExpression = this.queryExpression(vSelectStatement);
			if (this.inputState.guessing == 0)
			{
				queryParenthesisExpression.QueryExpression = queryExpression;
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(queryParenthesisExpression, token2);
			}
			return queryParenthesisExpression;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000B1810 File Offset: 0x000AFA10
		public UniqueRowFilter uniqueRowFilter()
		{
			UniqueRowFilter uniqueRowFilter = UniqueRowFilter.NotSpecified;
			int num = this.LA(1);
			if (num != 5)
			{
				if (num != 51)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(51);
				if (this.inputState.guessing == 0)
				{
					uniqueRowFilter = UniqueRowFilter.Distinct;
				}
			}
			else
			{
				this.match(5);
				if (this.inputState.guessing == 0)
				{
					uniqueRowFilter = UniqueRowFilter.All;
				}
			}
			return uniqueRowFilter;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000B1878 File Offset: 0x000AFA78
		public TopRowFilter topRowFilter()
		{
			TopRowFilter topRowFilter = base.FragmentFactory.CreateFragment<TopRowFilter>();
			IToken token = this.LT(1);
			this.match(152);
			ScalarExpression scalarExpression = this.integerOrRealOrNumeric();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(topRowFilter, token);
				topRowFilter.Expression = scalarExpression;
			}
			int num = this.LA(1);
			if (num <= 116)
			{
				if (num <= 41)
				{
					if (num <= 25)
					{
						if (num == 20 || num == 25)
						{
							goto IL_0249;
						}
					}
					else
					{
						if (num == 34)
						{
							goto IL_0249;
						}
						switch (num)
						{
						case 40:
						case 41:
							goto IL_0249;
						}
					}
				}
				else if (num <= 93)
				{
					switch (num)
					{
					case 79:
					case 81:
						goto IL_0249;
					case 80:
						break;
					default:
						if (num == 93)
						{
							goto IL_0249;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 100:
					case 101:
						goto IL_0249;
					default:
						if (num == 116)
						{
							IToken token2 = this.LT(1);
							this.match(116);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.ThrowIfPercentValueOutOfRange(scalarExpression);
								TSql80ParserBaseInternal.UpdateTokenInfo(topRowFilter, token2);
								topRowFilter.Percent = true;
								goto IL_0249;
							}
							goto IL_0249;
						}
						break;
					}
				}
			}
			else if (num <= 147)
			{
				if (num <= 136)
				{
					if (num == 133 || num == 136)
					{
						goto IL_0249;
					}
				}
				else if (num == 141 || num == 147)
				{
					goto IL_0249;
				}
			}
			else if (num <= 171)
			{
				if (num == 163 || num == 171)
				{
					goto IL_0249;
				}
			}
			else
			{
				switch (num)
				{
				case 191:
				case 193:
				case 195:
				case 197:
				case 199:
				case 200:
					goto IL_0249;
				case 192:
				case 194:
				case 196:
				case 198:
					break;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						goto IL_0249;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0249:
			int num2 = this.LA(1);
			if (num2 <= 101)
			{
				if (num2 <= 34)
				{
					if (num2 == 20 || num2 == 25 || num2 == 34)
					{
						return topRowFilter;
					}
				}
				else if (num2 <= 81)
				{
					switch (num2)
					{
					case 40:
					case 41:
						return topRowFilter;
					default:
						switch (num2)
						{
						case 79:
						case 81:
							return topRowFilter;
						}
						break;
					}
				}
				else
				{
					if (num2 == 93)
					{
						return topRowFilter;
					}
					switch (num2)
					{
					case 100:
					case 101:
						return topRowFilter;
					}
				}
			}
			else if (num2 <= 147)
			{
				if (num2 <= 136)
				{
					if (num2 == 133 || num2 == 136)
					{
						return topRowFilter;
					}
				}
				else if (num2 == 141 || num2 == 147)
				{
					return topRowFilter;
				}
			}
			else if (num2 <= 171)
			{
				if (num2 == 163)
				{
					return topRowFilter;
				}
				if (num2 == 171)
				{
					this.match(171);
					IToken token3 = this.LT(1);
					this.match(232);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.Match(token3, "TIES");
						TSql80ParserBaseInternal.UpdateTokenInfo(topRowFilter, token3);
						topRowFilter.WithTies = true;
						return topRowFilter;
					}
					return topRowFilter;
				}
			}
			else
			{
				switch (num2)
				{
				case 191:
				case 193:
				case 195:
				case 197:
				case 199:
				case 200:
					return topRowFilter;
				case 192:
				case 194:
				case 196:
				case 198:
					break;
				default:
					switch (num2)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						return topRowFilter;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x000B1CCC File Offset: 0x000AFECC
		public SelectElement selectColumnOrStarExpression()
		{
			bool flag = false;
			if (TSql80ParserInternal.tokenSet_35_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_36_.member(this.LA(2)))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.selectStarExpression();
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			SelectElement selectElement;
			if (flag)
			{
				selectElement = this.selectStarExpression();
			}
			else
			{
				if (!TSql80ParserInternal.tokenSet_37_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_38_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				selectElement = this.selectColumn();
			}
			return selectElement;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000B1DA4 File Offset: 0x000AFFA4
		public FromClause fromClauseOpt()
		{
			FromClause fromClause = null;
			int num = this.LA(1);
			if (num <= 87)
			{
				if (num <= 23)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return fromClause;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return fromClause;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return fromClause;
							}
							break;
						}
					}
				}
				else if (num <= 35)
				{
					switch (num)
					{
					case 28:
					case 29:
						return fromClause;
					default:
						switch (num)
						{
						case 33:
						case 35:
							return fromClause;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 59:
					case 60:
					case 61:
					case 64:
					case 67:
					case 74:
					case 75:
					case 76:
					case 77:
						return fromClause;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 62:
					case 63:
					case 65:
					case 66:
					case 68:
					case 69:
					case 70:
					case 72:
					case 73:
						break;
					case 71:
						return this.fromClause();
					default:
						if (num == 82)
						{
							return fromClause;
						}
						switch (num)
						{
						case 86:
						case 87:
							return fromClause;
						}
						break;
					}
				}
			}
			else if (num <= 119)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return fromClause;
					}
				}
				else
				{
					if (num == 106)
					{
						return fromClause;
					}
					switch (num)
					{
					case 111:
					case 113:
						return fromClause;
					case 112:
						break;
					default:
						if (num == 119)
						{
							return fromClause;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 123:
				case 125:
				case 126:
				case 129:
				case 131:
				case 132:
				case 134:
				case 138:
				case 140:
				case 142:
				case 143:
				case 144:
					return fromClause;
				case 124:
				case 127:
				case 128:
				case 130:
				case 133:
				case 135:
				case 136:
				case 137:
				case 139:
				case 141:
					break;
				default:
					switch (num)
					{
					case 156:
					case 158:
					case 160:
					case 161:
					case 162:
					case 167:
					case 169:
					case 170:
					case 171:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return fromClause;
					}
					break;
				}
			}
			else
			{
				switch (num)
				{
				case 191:
				case 192:
					return fromClause;
				default:
					if (num == 204)
					{
						return fromClause;
					}
					switch (num)
					{
					case 219:
					case 220:
						return fromClause;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000B2070 File Offset: 0x000B0270
		public WhereClause whereClause()
		{
			WhereClause whereClause = base.FragmentFactory.CreateFragment<WhereClause>();
			IToken token = this.LT(1);
			this.match(169);
			BooleanExpression booleanExpression = this.booleanExpression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(whereClause, token);
				whereClause.SearchCondition = booleanExpression;
			}
			return whereClause;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000B20C4 File Offset: 0x000B02C4
		public GroupByClause groupByClause()
		{
			GroupByClause groupByClause = base.FragmentFactory.CreateFragment<GroupByClause>();
			IToken token = this.LT(1);
			this.match(76);
			this.match(18);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(groupByClause, token);
			}
			int num = this.LA(1);
			if (num <= 101)
			{
				if (num <= 34)
				{
					if (num <= 20)
					{
						if (num != 5)
						{
							if (num == 20)
							{
								goto IL_0209;
							}
						}
						else
						{
							this.match(5);
							if (this.inputState.guessing == 0)
							{
								groupByClause.All = true;
								goto IL_0209;
							}
							goto IL_0209;
						}
					}
					else if (num == 25 || num == 34)
					{
						goto IL_0209;
					}
				}
				else if (num <= 81)
				{
					switch (num)
					{
					case 40:
					case 41:
						goto IL_0209;
					default:
						if (num == 81)
						{
							goto IL_0209;
						}
						break;
					}
				}
				else
				{
					if (num == 93)
					{
						goto IL_0209;
					}
					switch (num)
					{
					case 100:
					case 101:
						goto IL_0209;
					}
				}
			}
			else if (num <= 147)
			{
				if (num <= 136)
				{
					if (num == 133 || num == 136)
					{
						goto IL_0209;
					}
				}
				else if (num == 141 || num == 147)
				{
					goto IL_0209;
				}
			}
			else if (num <= 193)
			{
				if (num == 163)
				{
					goto IL_0209;
				}
				switch (num)
				{
				case 191:
				case 193:
					goto IL_0209;
				}
			}
			else
			{
				switch (num)
				{
				case 197:
				case 199:
				case 200:
					goto IL_0209;
				case 198:
					break;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						goto IL_0209;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0209:
			ExpressionGroupingSpecification expressionGroupingSpecification = this.simpleGroupByItem();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<GroupingSpecification>(groupByClause, groupByClause.GroupingSpecifications, expressionGroupingSpecification);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				expressionGroupingSpecification = this.simpleGroupByItem();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<GroupingSpecification>(groupByClause, groupByClause.GroupingSpecifications, expressionGroupingSpecification);
				}
			}
			bool flag = false;
			if (this.LA(1) == 171 && this.LA(2) == 232)
			{
				int num2 = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(171);
					this.match(232);
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
				this.match(171);
				IToken token2 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					if (groupByClause.All)
					{
						TSql80ParserBaseInternal.ThrowParseErrorException("SQL46084", token2, TSqlParserResource.SQL46084Message, new string[0]);
					}
					TSql80ParserBaseInternal.UpdateTokenInfo(groupByClause, token2);
					groupByClause.GroupByOption = GroupByOptionHelper.Instance.ParseOption(token2);
				}
			}
			else if (!TSql80ParserInternal.tokenSet_39_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_40_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return groupByClause;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000B2460 File Offset: 0x000B0660
		public HavingClause havingClause()
		{
			HavingClause havingClause = base.FragmentFactory.CreateFragment<HavingClause>();
			IToken token = this.LT(1);
			this.match(77);
			BooleanExpression booleanExpression = this.booleanExpression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(havingClause, token);
				havingClause.SearchCondition = booleanExpression;
			}
			return havingClause;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000B24B0 File Offset: 0x000B06B0
		public BrowseForClause browseForClause()
		{
			BrowseForClause browseForClause = base.FragmentFactory.CreateFragment<BrowseForClause>();
			IToken token = this.LT(1);
			this.match(16);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(browseForClause, token);
			}
			return browseForClause;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x000B24F0 File Offset: 0x000B06F0
		public void selectExpression(QuerySpecification vParent)
		{
			bool flag = false;
			if (this.LA(1) == 234 && this.LA(2) == 206)
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(234);
					this.match(206);
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag)
			{
				SelectSetVariable selectSetVariable = this.selectSetVariable();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SelectElement>(vParent, vParent.SelectElements, selectSetVariable);
					return;
				}
			}
			else
			{
				bool flag2 = false;
				if (TSql80ParserInternal.tokenSet_35_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_41_.member(this.LA(2)))
				{
					int num2 = this.mark();
					flag2 = true;
					this.inputState.guessing++;
					try
					{
						this.selectStarExpression();
					}
					catch (RecognitionException)
					{
						flag2 = false;
					}
					this.rewind(num2);
					this.inputState.guessing--;
				}
				if (flag2)
				{
					SelectStarExpression selectStarExpression = this.selectStarExpression();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SelectElement>(vParent, vParent.SelectElements, selectStarExpression);
						return;
					}
				}
				else
				{
					if (!TSql80ParserInternal.tokenSet_37_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_42_.member(this.LA(2)))
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					SelectScalarExpression selectScalarExpression = this.selectColumn();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SelectElement>(vParent, vParent.SelectElements, selectScalarExpression);
						return;
					}
				}
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000B26A8 File Offset: 0x000B08A8
		public Literal integerOrRealOrNumeric()
		{
			Literal literal;
			switch (this.LA(1))
			{
			case 221:
				literal = this.integer();
				break;
			case 222:
				literal = this.numeric();
				break;
			case 223:
				literal = this.real();
				break;
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return literal;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000B2708 File Offset: 0x000B0908
		public SelectSetVariable selectSetVariable()
		{
			SelectSetVariable selectSetVariable = base.FragmentFactory.CreateFragment<SelectSetVariable>();
			VariableReference variableReference = this.variable();
			this.match(206);
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				selectSetVariable.Variable = variableReference;
				selectSetVariable.Expression = scalarExpression;
			}
			return selectSetVariable;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000B2758 File Offset: 0x000B0958
		public SelectStarExpression selectStarExpression()
		{
			SelectStarExpression selectStarExpression = base.FragmentFactory.CreateFragment<SelectStarExpression>();
			int num = this.LA(1);
			if (num != 195)
			{
				if (num != 200)
				{
					switch (num)
					{
					case 232:
					case 233:
						break;
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				MultiPartIdentifier multiPartIdentifier = this.multiPartIdentifier(-1);
				if (this.inputState.guessing == 0)
				{
					selectStarExpression.Qualifier = multiPartIdentifier;
				}
				this.match(200);
				IToken token = this.LT(1);
				this.match(195);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(selectStarExpression, token);
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(195);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(selectStarExpression, token2);
				}
			}
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckStarQualifier(selectStarExpression);
			}
			return selectStarExpression;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000B2848 File Offset: 0x000B0A48
		public SelectScalarExpression selectColumn()
		{
			SelectScalarExpression selectScalarExpression = base.FragmentFactory.CreateFragment<SelectScalarExpression>();
			ScalarExpression scalarExpression;
			IdentifierOrValueExpression identifierOrValueExpression;
			if (TSql80ParserInternal.tokenSet_37_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_43_.member(this.LA(2)))
			{
				scalarExpression = this.selectColumnExpression();
				if (this.inputState.guessing == 0)
				{
					selectScalarExpression.Expression = scalarExpression;
				}
				int num = this.LA(1);
				if (num <= 95)
				{
					if (num <= 29)
					{
						if (num <= 6)
						{
							if (num != 1 && num != 6)
							{
								goto IL_03AB;
							}
							return selectScalarExpression;
						}
						else
						{
							switch (num)
							{
							case 9:
								break;
							case 10:
							case 11:
							case 14:
							case 16:
								goto IL_03AB;
							case 12:
							case 13:
							case 15:
							case 17:
								return selectScalarExpression;
							default:
								switch (num)
								{
								case 22:
								case 23:
									return selectScalarExpression;
								default:
									switch (num)
									{
									case 28:
									case 29:
										return selectScalarExpression;
									default:
										goto IL_03AB;
									}
									break;
								}
								break;
							}
						}
					}
					else if (num <= 77)
					{
						switch (num)
						{
						case 33:
						case 35:
							return selectScalarExpression;
						case 34:
							goto IL_03AB;
						default:
							switch (num)
							{
							case 44:
							case 45:
							case 46:
							case 48:
							case 49:
							case 54:
							case 55:
							case 56:
							case 59:
							case 60:
							case 61:
							case 64:
							case 67:
							case 71:
							case 74:
							case 75:
							case 76:
							case 77:
								return selectScalarExpression;
							case 47:
							case 50:
							case 51:
							case 52:
							case 53:
							case 57:
							case 58:
							case 62:
							case 63:
							case 65:
							case 66:
							case 68:
							case 69:
							case 70:
							case 72:
							case 73:
								goto IL_03AB;
							default:
								goto IL_03AB;
							}
							break;
						}
					}
					else
					{
						switch (num)
						{
						case 82:
						case 86:
						case 87:
						case 88:
							return selectScalarExpression;
						case 83:
						case 84:
						case 85:
							goto IL_03AB;
						default:
							if (num != 92 && num != 95)
							{
								goto IL_03AB;
							}
							return selectScalarExpression;
						}
					}
				}
				else if (num <= 181)
				{
					if (num <= 113)
					{
						if (num == 106)
						{
							return selectScalarExpression;
						}
						switch (num)
						{
						case 111:
						case 113:
							return selectScalarExpression;
						case 112:
							goto IL_03AB;
						default:
							goto IL_03AB;
						}
					}
					else
					{
						if (num == 119)
						{
							return selectScalarExpression;
						}
						switch (num)
						{
						case 123:
						case 125:
						case 126:
						case 129:
						case 131:
						case 132:
						case 134:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							return selectScalarExpression;
						case 124:
						case 127:
						case 128:
						case 130:
						case 133:
						case 135:
						case 136:
						case 137:
						case 139:
						case 141:
							goto IL_03AB;
						default:
							switch (num)
							{
							case 156:
							case 158:
							case 160:
							case 161:
							case 162:
							case 167:
							case 169:
							case 170:
							case 171:
							case 172:
							case 173:
							case 176:
							case 180:
							case 181:
								return selectScalarExpression;
							case 157:
							case 159:
							case 163:
							case 164:
							case 165:
							case 166:
							case 168:
							case 174:
							case 175:
							case 177:
							case 178:
							case 179:
								goto IL_03AB;
							default:
								goto IL_03AB;
							}
							break;
						}
					}
				}
				else if (num <= 198)
				{
					switch (num)
					{
					case 191:
					case 192:
						return selectScalarExpression;
					default:
						if (num != 198)
						{
							goto IL_03AB;
						}
						return selectScalarExpression;
					}
				}
				else
				{
					if (num == 204)
					{
						return selectScalarExpression;
					}
					switch (num)
					{
					case 219:
					case 220:
						return selectScalarExpression;
					default:
						switch (num)
						{
						case 230:
						case 231:
						case 232:
						case 233:
							break;
						default:
							goto IL_03AB;
						}
						break;
					}
				}
				int num2 = this.LA(1);
				if (num2 != 9)
				{
					switch (num2)
					{
					case 230:
					case 231:
					case 232:
					case 233:
						break;
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					this.match(9);
				}
				identifierOrValueExpression = this.stringOrIdentifier();
				if (this.inputState.guessing == 0)
				{
					selectScalarExpression.ColumnName = identifierOrValueExpression;
					return selectScalarExpression;
				}
				return selectScalarExpression;
				IL_03AB:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			if (this.LA(1) < 230 || this.LA(1) > 233 || this.LA(2) != 206)
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			identifierOrValueExpression = this.stringOrIdentifier();
			if (this.inputState.guessing == 0)
			{
				selectScalarExpression.ColumnName = identifierOrValueExpression;
			}
			this.match(206);
			scalarExpression = this.selectColumnExpression();
			if (this.inputState.guessing == 0)
			{
				selectScalarExpression.Expression = scalarExpression;
			}
			return selectScalarExpression;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000B2C94 File Offset: 0x000B0E94
		public ScalarExpression selectColumnExpression()
		{
			int num = this.LA(1);
			if (num <= 101)
			{
				if (num <= 34)
				{
					if (num != 20 && num != 25 && num != 34)
					{
						goto IL_0196;
					}
				}
				else if (num <= 81)
				{
					switch (num)
					{
					case 40:
					case 41:
						break;
					default:
						switch (num)
						{
						case 79:
							return this.identityFunction();
						case 80:
							goto IL_0196;
						case 81:
							break;
						default:
							goto IL_0196;
						}
						break;
					}
				}
				else if (num != 93)
				{
					switch (num)
					{
					case 100:
					case 101:
						break;
					default:
						goto IL_0196;
					}
				}
			}
			else if (num <= 147)
			{
				if (num <= 136)
				{
					if (num != 133 && num != 136)
					{
						goto IL_0196;
					}
				}
				else if (num != 141 && num != 147)
				{
					goto IL_0196;
				}
			}
			else if (num <= 193)
			{
				if (num != 163)
				{
					switch (num)
					{
					case 191:
					case 193:
						break;
					case 192:
						goto IL_0196;
					default:
						goto IL_0196;
					}
				}
			}
			else
			{
				switch (num)
				{
				case 197:
				case 199:
				case 200:
					break;
				case 198:
					goto IL_0196;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_0196;
					default:
						goto IL_0196;
					}
					break;
				}
			}
			return this.expression(ExpressionFlags.None);
			IL_0196:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000B2E4C File Offset: 0x000B104C
		public MultiPartIdentifier multiPartIdentifier(int vMaxNumber)
		{
			MultiPartIdentifier multiPartIdentifier = base.FragmentFactory.CreateFragment<MultiPartIdentifier>();
			List<Identifier> list = this.identifierList(vMaxNumber);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(multiPartIdentifier, multiPartIdentifier.Identifiers, list);
			}
			return multiPartIdentifier;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000B2E88 File Offset: 0x000B1088
		public IdentityFunctionCall identityFunction()
		{
			IdentityFunctionCall identityFunctionCall = base.FragmentFactory.CreateFragment<IdentityFunctionCall>();
			IToken token = this.LT(1);
			this.match(79);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(identityFunctionCall, token);
			}
			this.match(191);
			DataTypeReference dataTypeReference = this.scalarDataType();
			if (this.inputState.guessing == 0)
			{
				identityFunctionCall.DataType = dataTypeReference;
			}
			int num = this.LA(1);
			if (num != 192)
			{
				if (num != 198)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(198);
				ScalarExpression scalarExpression = this.seedIncrement();
				if (this.inputState.guessing == 0)
				{
					identityFunctionCall.Seed = scalarExpression;
				}
				this.match(198);
				scalarExpression = this.seedIncrement();
				if (this.inputState.guessing == 0)
				{
					identityFunctionCall.Increment = scalarExpression;
				}
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(identityFunctionCall, token2);
			}
			return identityFunctionCall;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000B2F98 File Offset: 0x000B1198
		public ScalarExpression seedIncrement()
		{
			ScalarExpression scalarExpression = null;
			UnaryExpression unaryExpression = null;
			int num = this.LA(1);
			switch (num)
			{
			case 197:
			{
				IToken token = this.LT(1);
				this.match(197);
				if (this.inputState.guessing == 0)
				{
					unaryExpression = base.FragmentFactory.CreateFragment<UnaryExpression>();
					TSql80ParserBaseInternal.UpdateTokenInfo(unaryExpression, token);
					unaryExpression.UnaryExpressionType = UnaryExpressionType.Positive;
					goto IL_00D3;
				}
				goto IL_00D3;
			}
			case 198:
				break;
			case 199:
			{
				IToken token2 = this.LT(1);
				this.match(199);
				if (this.inputState.guessing == 0)
				{
					unaryExpression = base.FragmentFactory.CreateFragment<UnaryExpression>();
					TSql80ParserBaseInternal.UpdateTokenInfo(unaryExpression, token2);
					unaryExpression.UnaryExpressionType = UnaryExpressionType.Negative;
					goto IL_00D3;
				}
				goto IL_00D3;
			}
			default:
				switch (num)
				{
				case 221:
				case 222:
					goto IL_00D3;
				}
				break;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_00D3:
			Literal literal = this.integerOrNumeric();
			if (this.inputState.guessing == 0)
			{
				if (unaryExpression != null)
				{
					unaryExpression.Expression = literal;
					scalarExpression = unaryExpression;
				}
				else
				{
					scalarExpression = literal;
				}
			}
			return scalarExpression;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x000B30A0 File Offset: 0x000B12A0
		public FromClause fromClause()
		{
			FromClause fromClause = base.FragmentFactory.CreateFragment<FromClause>();
			IToken token = this.LT(1);
			this.match(71);
			TableReference tableReference = this.selectTableReferenceWithOdbc();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(fromClause, token);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TableReference>(fromClause, fromClause.TableReferences, tableReference);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				tableReference = this.selectTableReferenceWithOdbc();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TableReference>(fromClause, fromClause.TableReferences, tableReference);
				}
			}
			return fromClause;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000B3130 File Offset: 0x000B1330
		public TableReference selectTableReferenceWithOdbc()
		{
			int num = this.LA(1);
			if (num > 110)
			{
				if (num <= 200)
				{
					switch (num)
					{
					case 191:
						goto IL_0084;
					case 192:
						goto IL_0096;
					case 193:
						break;
					default:
						if (num != 200)
						{
							goto IL_0096;
						}
						goto IL_0084;
					}
				}
				else
				{
					if (num == 203)
					{
						goto IL_0084;
					}
					switch (num)
					{
					case 232:
					case 233:
					case 234:
						goto IL_0084;
					case 235:
						break;
					default:
						goto IL_0096;
					}
				}
				return this.odbcQualifiedJoin();
			}
			if (num != 32 && num != 70)
			{
				switch (num)
				{
				case 107:
				case 108:
				case 109:
				case 110:
					break;
				default:
					goto IL_0096;
				}
			}
			IL_0084:
			return this.selectTableReference();
			IL_0096:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x000B31E8 File Offset: 0x000B13E8
		public TableReference selectTableReference()
		{
			TableReference tableReference = null;
			tableReference = this.selectTableReferenceElement();
			while (TSql80ParserInternal.tokenSet_44_.member(this.LA(1)))
			{
				this.joinElement(ref tableReference);
			}
			return tableReference;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000B321C File Offset: 0x000B141C
		public OdbcQualifiedJoinTableReference odbcQualifiedJoin()
		{
			OdbcQualifiedJoinTableReference odbcQualifiedJoinTableReference = base.FragmentFactory.CreateFragment<OdbcQualifiedJoinTableReference>();
			IToken token = null;
			int num = this.LA(1);
			if (num != 193)
			{
				if (num != 235)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.odbcInitiator();
			}
			else
			{
				token = this.LT(1);
				this.match(193);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(odbcQualifiedJoinTableReference, token);
				}
			}
			IToken token2 = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token2, "OJ");
			}
			int num2 = this.LA(1);
			TableReference tableReference;
			if (num2 > 110)
			{
				if (num2 <= 200)
				{
					switch (num2)
					{
					case 191:
						goto IL_013A;
					case 192:
						goto IL_0170;
					case 193:
						break;
					default:
						if (num2 != 200)
						{
							goto IL_0170;
						}
						goto IL_013A;
					}
				}
				else
				{
					if (num2 == 203)
					{
						goto IL_013A;
					}
					switch (num2)
					{
					case 232:
					case 233:
					case 234:
						goto IL_013A;
					case 235:
						break;
					default:
						goto IL_0170;
					}
				}
				tableReference = this.odbcQualifiedJoin();
				goto IL_0183;
			}
			if (num2 != 32 && num2 != 70)
			{
				switch (num2)
				{
				case 107:
				case 108:
				case 109:
				case 110:
					break;
				default:
					goto IL_0170;
				}
			}
			IL_013A:
			tableReference = this.selectTableReference();
			if (this.inputState.guessing == 0 && !(tableReference is QualifiedJoin))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46035", token, TSqlParserResource.SQL46035Message, new string[0]);
				goto IL_0183;
			}
			goto IL_0183;
			IL_0170:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0183:
			if (this.inputState.guessing == 0)
			{
				odbcQualifiedJoinTableReference.TableReference = tableReference;
			}
			IToken token3 = this.LT(1);
			this.match(194);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(odbcQualifiedJoinTableReference, token3);
			}
			return odbcQualifiedJoinTableReference;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000B33EC File Offset: 0x000B15EC
		public TableReference selectTableReferenceElement()
		{
			bool flag = false;
			if (this.LA(1) == 191 && TSql80ParserInternal.tokenSet_45_.member(this.LA(2)))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.joinParenthesis();
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			TableReference tableReference;
			if (flag)
			{
				tableReference = this.joinParenthesis();
			}
			else
			{
				if (!TSql80ParserInternal.tokenSet_45_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_46_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				tableReference = this.selectTableReferenceElementWithOutJoinParenthesis();
			}
			return tableReference;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000B34C4 File Offset: 0x000B16C4
		public void joinElement(ref TableReference vResult)
		{
			int num = this.LA(1);
			if (num <= 85)
			{
				if (num != 36)
				{
					if (num != 72 && num != 85)
					{
						goto IL_004E;
					}
					goto IL_0046;
				}
			}
			else if (num <= 93)
			{
				if (num != 90 && num != 93)
				{
					goto IL_004E;
				}
				goto IL_0046;
			}
			else if (num != 114)
			{
				if (num != 133)
				{
					goto IL_004E;
				}
				goto IL_0046;
			}
			this.unqualifiedJoin(ref vResult);
			return;
			IL_0046:
			this.qualifiedJoin(ref vResult);
			return;
			IL_004E:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000B3534 File Offset: 0x000B1734
		public void odbcInitiator()
		{
			IToken token = this.LT(1);
			this.match(235);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46036", token, TSqlParserResource.SQL46036Message, new string[0]);
			}
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000B357C File Offset: 0x000B177C
		public OdbcConvertSpecification odbcConvertSpecification()
		{
			OdbcConvertSpecification odbcConvertSpecification = base.FragmentFactory.CreateFragment<OdbcConvertSpecification>();
			Identifier identifier = this.nonQuotedIdentifier();
			if (this.inputState.guessing == 0)
			{
				odbcConvertSpecification.Identifier = identifier;
			}
			return odbcConvertSpecification;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000B35B4 File Offset: 0x000B17B4
		public ExtractFromExpression extractFromExpression()
		{
			ExtractFromExpression extractFromExpression = base.FragmentFactory.CreateFragment<ExtractFromExpression>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(71);
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "EXTRACT");
				extractFromExpression.Expression = scalarExpression;
			}
			return extractFromExpression;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000B3614 File Offset: 0x000B1814
		public OdbcFunctionCall odbcFunctionCall()
		{
			OdbcFunctionCall odbcFunctionCall = base.FragmentFactory.CreateFragment<OdbcFunctionCall>();
			Identifier identifier = base.FragmentFactory.CreateFragment<Identifier>();
			odbcFunctionCall.ParametersUsed = true;
			IToken token = this.LT(1);
			this.match(193);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(odbcFunctionCall, token);
			}
			IToken token2 = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token2, "FN");
			}
			if (this.inputState.guessing == 0 && this.LA(1) != 1)
			{
				identifier.SetUnquotedIdentifier(this.LT(1).getText());
				odbcFunctionCall.Name = identifier;
			}
			int num = this.LA(1);
			if (num <= 93)
			{
				if (num <= 43)
				{
					switch (num)
					{
					case 34:
					{
						this.match(34);
						this.match(191);
						ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(odbcFunctionCall, odbcFunctionCall.Parameters, scalarExpression);
						}
						this.match(198);
						scalarExpression = this.odbcConvertSpecification();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(odbcFunctionCall, odbcFunctionCall.Parameters, scalarExpression);
						}
						this.match(192);
						goto IL_064B;
					}
					case 35:
					case 36:
					case 37:
						goto IL_0638;
					case 38:
						break;
					case 39:
					case 40:
					{
						switch (this.LA(1))
						{
						case 39:
							this.match(39);
							break;
						case 40:
							this.match(40);
							break;
						default:
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						int num2 = this.LA(1);
						if (num2 == 191)
						{
							this.match(191);
							int num3 = this.LA(1);
							if (num3 <= 101)
							{
								if (num3 <= 34)
								{
									if (num3 != 20 && num3 != 25 && num3 != 34)
									{
										goto IL_0522;
									}
								}
								else if (num3 <= 81)
								{
									switch (num3)
									{
									case 40:
									case 41:
										break;
									default:
										if (num3 != 81)
										{
											goto IL_0522;
										}
										break;
									}
								}
								else if (num3 != 93)
								{
									switch (num3)
									{
									case 100:
									case 101:
										break;
									default:
										goto IL_0522;
									}
								}
							}
							else if (num3 <= 141)
							{
								if (num3 != 133 && num3 != 136 && num3 != 141)
								{
									goto IL_0522;
								}
							}
							else if (num3 <= 163)
							{
								if (num3 != 147 && num3 != 163)
								{
									goto IL_0522;
								}
							}
							else
							{
								switch (num3)
								{
								case 191:
								case 193:
								case 197:
								case 199:
								case 200:
									break;
								case 192:
									goto IL_0535;
								case 194:
								case 195:
								case 196:
								case 198:
									goto IL_0522;
								default:
									switch (num3)
									{
									case 211:
									case 221:
									case 222:
									case 223:
									case 224:
									case 225:
									case 227:
									case 228:
									case 230:
									case 231:
									case 232:
									case 233:
									case 234:
									case 235:
										break;
									case 212:
									case 213:
									case 214:
									case 215:
									case 216:
									case 217:
									case 218:
									case 219:
									case 220:
									case 226:
									case 229:
										goto IL_0522;
									default:
										goto IL_0522;
									}
									break;
								}
							}
							this.expressionList(odbcFunctionCall, odbcFunctionCall.Parameters);
							goto IL_0535;
							IL_0522:
							throw new NoViableAltException(this.LT(1), this.getFilename());
							IL_0535:
							this.match(192);
							goto IL_064B;
						}
						if (num2 != 194)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						if (this.inputState.guessing == 0)
						{
							odbcFunctionCall.ParametersUsed = false;
							goto IL_064B;
						}
						goto IL_064B;
					}
					default:
						if (num != 43)
						{
							goto IL_0638;
						}
						break;
					}
				}
				else
				{
					if (num != 86 && num != 93)
					{
						goto IL_0638;
					}
					goto IL_02AA;
				}
			}
			else if (num <= 156)
			{
				if (num == 133)
				{
					goto IL_02AA;
				}
				if (num != 156)
				{
					goto IL_0638;
				}
				this.match(156);
				this.match(191);
				ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(odbcFunctionCall, odbcFunctionCall.Parameters, scalarExpression);
				}
				this.match(198);
				scalarExpression = this.expression(ExpressionFlags.None);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(odbcFunctionCall, odbcFunctionCall.Parameters, scalarExpression);
				}
				this.match(192);
				goto IL_064B;
			}
			else if (num != 163)
			{
				if (num != 232)
				{
					goto IL_0638;
				}
				this.match(232);
				this.match(191);
				if (this.LA(1) == 232 && this.LA(2) == 71 && base.NextTokenMatches("EXTRACT"))
				{
					ScalarExpression scalarExpression = this.extractFromExpression();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(odbcFunctionCall, odbcFunctionCall.Parameters, scalarExpression);
					}
				}
				else if (TSql80ParserInternal.tokenSet_16_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_47_.member(this.LA(2)))
				{
					this.expressionList(odbcFunctionCall, odbcFunctionCall.Parameters);
				}
				else if (this.LA(1) != 192)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(192);
				goto IL_064B;
			}
			int num4 = this.LA(1);
			if (num4 != 38)
			{
				if (num4 != 43)
				{
					if (num4 != 163)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(163);
				}
				else
				{
					this.match(43);
				}
			}
			else
			{
				this.match(38);
			}
			this.match(191);
			this.match(192);
			goto IL_064B;
			IL_02AA:
			int num5 = this.LA(1);
			if (num5 != 86)
			{
				if (num5 != 93)
				{
					if (num5 != 133)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(133);
				}
				else
				{
					this.match(93);
				}
			}
			else
			{
				this.match(86);
			}
			this.match(191);
			this.expressionList(odbcFunctionCall, odbcFunctionCall.Parameters);
			this.match(192);
			goto IL_064B;
			IL_0638:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_064B:
			IToken token3 = this.LT(1);
			this.match(194);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(odbcFunctionCall, token3);
			}
			return odbcFunctionCall;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000B3C94 File Offset: 0x000B1E94
		public void expressionList(TSqlFragment vParent, IList<ScalarExpression> expressions)
		{
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(vParent, expressions, scalarExpression);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				scalarExpression = this.expression(ExpressionFlags.None);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(vParent, expressions, scalarExpression);
				}
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x000B3CF8 File Offset: 0x000B1EF8
		public TableReference joinTableReference()
		{
			TableReference tableReference = null;
			bool flag = false;
			if (this.LA(1) == 191 && TSql80ParserInternal.tokenSet_45_.member(this.LA(2)))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.joinParenthesis();
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag)
			{
				tableReference = this.joinParenthesis();
				while (TSql80ParserInternal.tokenSet_44_.member(this.LA(1)))
				{
					this.joinElement(ref tableReference);
				}
			}
			else
			{
				if (!TSql80ParserInternal.tokenSet_45_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_48_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				tableReference = this.selectTableReferenceElementWithOutJoinParenthesis();
				int num2 = 0;
				while (TSql80ParserInternal.tokenSet_44_.member(this.LA(1)))
				{
					this.joinElement(ref tableReference);
					num2++;
				}
				if (num2 < 1)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			return tableReference;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000B3E28 File Offset: 0x000B2028
		public JoinParenthesisTableReference joinParenthesis()
		{
			JoinParenthesisTableReference joinParenthesisTableReference = base.FragmentFactory.CreateFragment<JoinParenthesisTableReference>();
			IToken token = this.LT(1);
			this.match(191);
			TableReference tableReference = this.joinTableReference();
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(joinParenthesisTableReference, token);
				joinParenthesisTableReference.Join = tableReference;
				TSql80ParserBaseInternal.UpdateTokenInfo(joinParenthesisTableReference, token2);
			}
			return joinParenthesisTableReference;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x000B3E98 File Offset: 0x000B2098
		public TableReference selectTableReferenceElementWithOutJoinParenthesis()
		{
			int num = this.LA(1);
			if (num > 110)
			{
				if (num <= 200)
				{
					if (num == 191)
					{
						return this.derivedTable();
					}
					if (num != 200)
					{
						goto IL_00B5;
					}
				}
				else
				{
					if (num == 203)
					{
						return this.builtInFunctionTableReference();
					}
					switch (num)
					{
					case 232:
					case 233:
						break;
					case 234:
						return this.variableTableReference();
					default:
						goto IL_00B5;
					}
				}
				return this.schemaObjectOrFunctionTableReference();
			}
			if (num == 32 || num == 70)
			{
				return this.fulltextTableReference();
			}
			switch (num)
			{
			case 107:
			case 108:
			case 109:
				return this.openRowset();
			case 110:
				return this.openXmlTableReference();
			}
			IL_00B5:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000B3F70 File Offset: 0x000B2170
		public void unqualifiedJoin(ref TableReference vResult)
		{
			UnqualifiedJoin unqualifiedJoin = base.FragmentFactory.CreateFragment<UnqualifiedJoin>();
			int num = this.LA(1);
			if (num != 36)
			{
				if (num != 114)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(114);
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "APPLY");
					unqualifiedJoin.UnqualifiedJoinType = UnqualifiedJoinType.OuterApply;
				}
			}
			else
			{
				this.match(36);
				int num2 = this.LA(1);
				if (num2 != 90)
				{
					if (num2 != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					IToken token2 = this.LT(1);
					this.match(232);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.Match(token2, "APPLY");
						unqualifiedJoin.UnqualifiedJoinType = UnqualifiedJoinType.CrossApply;
					}
				}
				else
				{
					this.match(90);
					if (this.inputState.guessing == 0)
					{
						unqualifiedJoin.UnqualifiedJoinType = UnqualifiedJoinType.CrossJoin;
					}
				}
			}
			TableReference tableReference = this.selectTableReferenceElement();
			if (this.inputState.guessing == 0)
			{
				unqualifiedJoin.FirstTableReference = vResult;
				unqualifiedJoin.SecondTableReference = tableReference;
				vResult = unqualifiedJoin;
			}
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000B40AC File Offset: 0x000B22AC
		public void qualifiedJoin(ref TableReference vResult)
		{
			QualifiedJoin qualifiedJoin = base.FragmentFactory.CreateFragment<QualifiedJoin>();
			int num = this.LA(1);
			if (num <= 85)
			{
				if (num != 72 && num != 85)
				{
					goto IL_022C;
				}
			}
			else if (num != 90)
			{
				if (num != 93 && num != 133)
				{
					goto IL_022C;
				}
			}
			else
			{
				this.match(90);
				if (this.inputState.guessing == 0)
				{
					qualifiedJoin.QualifiedJoinType = QualifiedJoinType.Inner;
					goto IL_023F;
				}
				goto IL_023F;
			}
			int num2 = this.LA(1);
			if (num2 <= 85)
			{
				if (num2 != 72)
				{
					if (num2 == 85)
					{
						this.match(85);
						if (this.inputState.guessing == 0)
						{
							qualifiedJoin.QualifiedJoinType = QualifiedJoinType.Inner;
							goto IL_01EE;
						}
						goto IL_01EE;
					}
				}
				else
				{
					this.match(72);
					int num3 = this.LA(1);
					if (num3 != 90)
					{
						if (num3 != 114)
						{
							if (num3 != 232)
							{
								throw new NoViableAltException(this.LT(1), this.getFilename());
							}
						}
						else
						{
							this.match(114);
						}
					}
					if (this.inputState.guessing == 0)
					{
						qualifiedJoin.QualifiedJoinType = QualifiedJoinType.FullOuter;
						goto IL_01EE;
					}
					goto IL_01EE;
				}
			}
			else if (num2 != 93)
			{
				if (num2 == 133)
				{
					this.match(133);
					int num4 = this.LA(1);
					if (num4 != 90)
					{
						if (num4 != 114)
						{
							if (num4 != 232)
							{
								throw new NoViableAltException(this.LT(1), this.getFilename());
							}
						}
						else
						{
							this.match(114);
						}
					}
					if (this.inputState.guessing == 0)
					{
						qualifiedJoin.QualifiedJoinType = QualifiedJoinType.RightOuter;
						goto IL_01EE;
					}
					goto IL_01EE;
				}
			}
			else
			{
				this.match(93);
				int num5 = this.LA(1);
				if (num5 != 90)
				{
					if (num5 != 114)
					{
						if (num5 != 232)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
					}
					else
					{
						this.match(114);
					}
				}
				if (this.inputState.guessing == 0)
				{
					qualifiedJoin.QualifiedJoinType = QualifiedJoinType.LeftOuter;
					goto IL_01EE;
				}
				goto IL_01EE;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_01EE:
			int num6 = this.LA(1);
			if (num6 != 90)
			{
				if (num6 != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.joinHint(qualifiedJoin);
			}
			this.match(90);
			goto IL_023F;
			IL_022C:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_023F:
			TableReference tableReference = this.selectTableReferenceWithOdbc();
			if (this.inputState.guessing == 0)
			{
				qualifiedJoin.FirstTableReference = vResult;
				qualifiedJoin.SecondTableReference = tableReference;
			}
			this.match(105);
			BooleanExpression booleanExpression = this.booleanExpression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				qualifiedJoin.SearchCondition = booleanExpression;
				vResult = qualifiedJoin;
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000B4344 File Offset: 0x000B2544
		public TableReference schemaObjectOrFunctionTableReference()
		{
			SchemaObjectName schemaObjectName = this.schemaObjectFourPartName();
			TableReference tableReference;
			if (TSql80ParserInternal.tokenSet_49_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_50_.member(this.LA(2)) && base.IsTableReference(true))
			{
				tableReference = this.schemaObjectTableReference(schemaObjectName);
			}
			else
			{
				if (this.LA(1) != 191 || !TSql80ParserInternal.tokenSet_51_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				tableReference = this.schemaObjectFunctionTableReference(schemaObjectName);
			}
			return tableReference;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x000B43D0 File Offset: 0x000B25D0
		public BuiltInFunctionTableReference builtInFunctionTableReference()
		{
			BuiltInFunctionTableReference builtInFunctionTableReference = base.FragmentFactory.CreateFragment<BuiltInFunctionTableReference>();
			IToken token = this.LT(1);
			this.match(203);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(builtInFunctionTableReference, token);
			}
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				builtInFunctionTableReference.Name = identifier;
			}
			this.match(191);
			int num = this.LA(1);
			if (num <= 100)
			{
				if (num != 47 && num != 100)
				{
					goto IL_0143;
				}
			}
			else
			{
				switch (num)
				{
				case 192:
					goto IL_0156;
				case 193:
					break;
				default:
					if (num != 199)
					{
						switch (num)
						{
						case 221:
						case 222:
						case 223:
						case 224:
						case 225:
						case 230:
						case 231:
						case 234:
							break;
						case 226:
						case 227:
						case 228:
						case 229:
						case 232:
						case 233:
							goto IL_0143;
						default:
							goto IL_0143;
						}
					}
					break;
				}
			}
			ScalarExpression scalarExpression = this.possibleNegativeConstantWithDefault();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(builtInFunctionTableReference, builtInFunctionTableReference.Parameters, scalarExpression);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				scalarExpression = this.possibleNegativeConstantWithDefault();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(builtInFunctionTableReference, builtInFunctionTableReference.Parameters, scalarExpression);
				}
			}
			goto IL_0156;
			IL_0143:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0156:
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(builtInFunctionTableReference, token2);
			}
			this.simpleTableReferenceAliasOpt(builtInFunctionTableReference);
			return builtInFunctionTableReference;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000B4564 File Offset: 0x000B2764
		public VariableTableReference variableTableReference()
		{
			VariableTableReference variableTableReference = base.FragmentFactory.CreateFragment<VariableTableReference>();
			VariableReference variableReference = this.variable();
			if (this.inputState.guessing == 0)
			{
				variableTableReference.Variable = variableReference;
			}
			this.simpleTableReferenceAliasOpt(variableTableReference);
			return variableTableReference;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000B45A0 File Offset: 0x000B27A0
		public TableReferenceWithAlias openRowset()
		{
			TableReferenceWithAlias tableReferenceWithAlias;
			switch (this.LA(1))
			{
			case 107:
				tableReferenceWithAlias = this.adhocRowset();
				this.simpleTableReferenceAliasOpt(tableReferenceWithAlias);
				break;
			case 108:
				tableReferenceWithAlias = this.openQueryRowset();
				this.simpleTableReferenceAliasOpt(tableReferenceWithAlias);
				break;
			case 109:
				tableReferenceWithAlias = this.openRowsetRowset();
				break;
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return tableReferenceWithAlias;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000B460C File Offset: 0x000B280C
		public FullTextTableReference fulltextTableReference()
		{
			FullTextTableReference fullTextTableReference = base.FragmentFactory.CreateFragment<FullTextTableReference>();
			this.fullTextTable(fullTextTableReference);
			this.match(191);
			SchemaObjectName schemaObjectName = this.schemaObjectFourPartName();
			if (this.inputState.guessing == 0)
			{
				fullTextTableReference.TableName = schemaObjectName;
			}
			this.match(198);
			this.fulltextTableColumnList(fullTextTableReference);
			this.match(198);
			ValueExpression valueExpression = this.stringOrVariable();
			if (this.inputState.guessing == 0)
			{
				fullTextTableReference.SearchCondition = valueExpression;
			}
			int num = this.LA(1);
			if (num != 192)
			{
				if (num != 198)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.fulltextTableOptions(fullTextTableReference);
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(fullTextTableReference, token);
			}
			this.simpleTableReferenceAliasOpt(fullTextTableReference);
			return fullTextTableReference;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000B46F4 File Offset: 0x000B28F4
		public OpenXmlTableReference openXmlTableReference()
		{
			IToken token = this.LT(1);
			this.match(110);
			this.match(191);
			OpenXmlTableReference openXmlTableReference = this.openXmlParams();
			IToken token2 = this.LT(1);
			this.match(192);
			this.openXmlWithClauseOpt(openXmlTableReference);
			this.simpleTableReferenceAliasOpt(openXmlTableReference);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(openXmlTableReference, token);
				TSql80ParserBaseInternal.UpdateTokenInfo(openXmlTableReference, token2);
			}
			return openXmlTableReference;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000B4764 File Offset: 0x000B2964
		public void joinHint(QualifiedJoin vParent)
		{
			IToken token = this.LT(1);
			this.match(232);
			int num = this.LA(1);
			if (num != 90)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token2 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token, "LOCAL");
					vParent.JoinHint = JoinHintHelper.Instance.ParseOption(token2);
					if (vParent.JoinHint == JoinHint.Remote)
					{
						TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(token2);
						return;
					}
				}
			}
			else if (this.inputState.guessing == 0)
			{
				vParent.JoinHint = JoinHintHelper.Instance.ParseOption(token);
				return;
			}
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000B481C File Offset: 0x000B2A1C
		public ScalarExpression possibleNegativeConstantWithDefault()
		{
			int num = this.LA(1);
			if (num <= 100)
			{
				if (num == 47)
				{
					return this.defaultLiteral();
				}
				if (num != 100)
				{
					goto IL_0083;
				}
			}
			else if (num != 193 && num != 199)
			{
				switch (num)
				{
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 234:
					break;
				case 226:
				case 227:
				case 228:
				case 229:
				case 232:
				case 233:
					goto IL_0083;
				default:
					goto IL_0083;
				}
			}
			return this.possibleNegativeConstant();
			IL_0083:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000B48C0 File Offset: 0x000B2AC0
		public void simpleTableReferenceAliasOpt(TableReferenceWithAlias vParent)
		{
			int num = this.LA(1);
			if (num <= 106)
			{
				if (num <= 17)
				{
					if (num == 1 || num == 6)
					{
						return;
					}
					switch (num)
					{
					case 9:
						break;
					case 10:
					case 11:
					case 14:
					case 16:
						goto IL_030F;
					case 12:
					case 13:
					case 15:
					case 17:
						return;
					default:
						goto IL_030F;
					}
				}
				else if (num <= 36)
				{
					switch (num)
					{
					case 22:
					case 23:
						return;
					default:
						switch (num)
						{
						case 28:
						case 29:
						case 33:
						case 35:
						case 36:
							return;
						case 30:
						case 31:
						case 32:
						case 34:
							goto IL_030F;
						default:
							goto IL_030F;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 59:
					case 60:
					case 61:
					case 64:
					case 67:
					case 71:
					case 72:
					case 74:
					case 75:
					case 76:
					case 77:
					case 82:
					case 85:
					case 86:
					case 87:
					case 90:
					case 92:
					case 93:
					case 95:
						return;
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 62:
					case 63:
					case 65:
					case 66:
					case 68:
					case 69:
					case 70:
					case 73:
					case 78:
					case 79:
					case 80:
					case 81:
					case 83:
					case 84:
					case 88:
					case 89:
					case 91:
					case 94:
						goto IL_030F;
					default:
						switch (num)
						{
						case 105:
						case 106:
							return;
						default:
							goto IL_030F;
						}
						break;
					}
				}
			}
			else if (num <= 194)
			{
				if (num <= 144)
				{
					switch (num)
					{
					case 111:
					case 113:
					case 114:
						return;
					case 112:
						goto IL_030F;
					default:
						switch (num)
						{
						case 119:
						case 123:
						case 125:
						case 126:
						case 129:
						case 131:
						case 132:
						case 133:
						case 134:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							return;
						case 120:
						case 121:
						case 122:
						case 124:
						case 127:
						case 128:
						case 130:
						case 135:
						case 136:
						case 137:
						case 139:
						case 141:
							goto IL_030F;
						default:
							goto IL_030F;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 156:
					case 158:
					case 160:
					case 161:
					case 162:
					case 164:
					case 167:
					case 169:
					case 170:
					case 171:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return;
					case 157:
					case 159:
					case 163:
					case 165:
					case 166:
					case 168:
					case 174:
					case 175:
					case 177:
					case 178:
					case 179:
						goto IL_030F;
					default:
						switch (num)
						{
						case 191:
						case 192:
						case 194:
							return;
						case 193:
							goto IL_030F;
						default:
							goto IL_030F;
						}
						break;
					}
				}
			}
			else if (num <= 204)
			{
				if (num != 198 && num != 204)
				{
					goto IL_030F;
				}
				return;
			}
			else
			{
				switch (num)
				{
				case 219:
				case 220:
					return;
				default:
					switch (num)
					{
					case 232:
					case 233:
						break;
					default:
						goto IL_030F;
					}
					break;
				}
			}
			this.simpleTableReferenceAlias(vParent);
			return;
			IL_030F:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000B4BF0 File Offset: 0x000B2DF0
		public RaiseErrorStatement raiseErrorStatement()
		{
			RaiseErrorStatement raiseErrorStatement = base.FragmentFactory.CreateFragment<RaiseErrorStatement>();
			IToken token = this.LT(1);
			this.match(191);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(raiseErrorStatement, token);
			}
			ScalarExpression scalarExpression = this.signedIntegerOrStringOrVariable();
			if (this.inputState.guessing == 0)
			{
				raiseErrorStatement.FirstParameter = scalarExpression;
			}
			this.match(198);
			scalarExpression = this.signedIntegerOrVariable();
			if (this.inputState.guessing == 0)
			{
				raiseErrorStatement.SecondParameter = scalarExpression;
			}
			this.match(198);
			scalarExpression = this.signedIntegerOrVariable();
			if (this.inputState.guessing == 0)
			{
				raiseErrorStatement.ThirdParameter = scalarExpression;
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				scalarExpression = this.possibleNegativeConstant();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(raiseErrorStatement, raiseErrorStatement.OptionalParameters, scalarExpression);
				}
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(raiseErrorStatement, token2);
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return raiseErrorStatement;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return raiseErrorStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return raiseErrorStatement;
							default:
								if (num == 28)
								{
									return raiseErrorStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return raiseErrorStatement;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return raiseErrorStatement;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return raiseErrorStatement;
					default:
						if (num == 82 || num == 86)
						{
							return raiseErrorStatement;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return raiseErrorStatement;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return raiseErrorStatement;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return raiseErrorStatement;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return raiseErrorStatement;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return raiseErrorStatement;
					case 171:
					{
						this.match(171);
						IToken token3 = this.LT(1);
						this.match(232);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(raiseErrorStatement, token3);
							raiseErrorStatement.RaiseErrorOptions |= RaiseErrorOptionsHelper.Instance.ParseOption(token3);
						}
						while (this.LA(1) == 198)
						{
							this.match(198);
							IToken token4 = this.LT(1);
							this.match(232);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.UpdateTokenInfo(raiseErrorStatement, token4);
								raiseErrorStatement.RaiseErrorOptions |= RaiseErrorOptionsHelper.Instance.ParseOption(token4);
							}
						}
						return raiseErrorStatement;
					}
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return raiseErrorStatement;
				}
				switch (num)
				{
				case 219:
				case 220:
					return raiseErrorStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000B5038 File Offset: 0x000B3238
		public RaiseErrorLegacyStatement raiseErrorLegacyStatement()
		{
			RaiseErrorLegacyStatement raiseErrorLegacyStatement = base.FragmentFactory.CreateFragment<RaiseErrorLegacyStatement>();
			ScalarExpression scalarExpression = this.signedIntegerOrVariable();
			if (this.inputState.guessing == 0)
			{
				raiseErrorLegacyStatement.FirstParameter = scalarExpression;
			}
			ValueExpression valueExpression = this.stringOrVariable();
			if (this.inputState.guessing == 0)
			{
				raiseErrorLegacyStatement.SecondParameter = valueExpression;
			}
			return raiseErrorLegacyStatement;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000B5088 File Offset: 0x000B3288
		public ScalarExpression signedIntegerOrStringOrVariable()
		{
			int num = this.LA(1);
			if (num != 199 && num != 221)
			{
				switch (num)
				{
				case 230:
				case 231:
				case 234:
					return this.stringOrVariable();
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return this.signedInteger();
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000B50F8 File Offset: 0x000B32F8
		public ScalarExpression possibleNegativeConstant()
		{
			int num = this.LA(1);
			if (num <= 193)
			{
				if (num != 100 && num != 193)
				{
					goto IL_007F;
				}
			}
			else
			{
				if (num == 199)
				{
					return this.negativeConstant();
				}
				switch (num)
				{
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 234:
					break;
				case 226:
				case 227:
				case 228:
				case 229:
				case 232:
				case 233:
					goto IL_007F;
				default:
					goto IL_007F;
				}
			}
			return this.literal();
			IL_007F:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000B5198 File Offset: 0x000B3398
		public DeleteSpecification deleteSpecification()
		{
			DeleteSpecification deleteSpecification = base.FragmentFactory.CreateFragment<DeleteSpecification>();
			IToken token = this.LT(1);
			this.match(48);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(deleteSpecification, token);
			}
			int num = this.LA(1);
			if (num <= 109)
			{
				if (num == 71)
				{
					this.match(71);
					goto IL_00A0;
				}
				switch (num)
				{
				case 107:
				case 108:
				case 109:
					goto IL_00A0;
				}
			}
			else
			{
				if (num == 200)
				{
					goto IL_00A0;
				}
				switch (num)
				{
				case 232:
				case 233:
				case 234:
					goto IL_00A0;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_00A0:
			TableReference tableReference = this.dmlTarget();
			if (this.inputState.guessing == 0)
			{
				deleteSpecification.Target = tableReference;
			}
			FromClause fromClause = this.fromClauseOpt();
			if (this.inputState.guessing == 0)
			{
				deleteSpecification.FromClause = fromClause;
			}
			int num2 = this.LA(1);
			if (num2 <= 86)
			{
				if (num2 <= 28)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return deleteSpecification;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return deleteSpecification;
						case 14:
						case 16:
							break;
						default:
							switch (num2)
							{
							case 22:
							case 23:
								return deleteSpecification;
							default:
								if (num2 == 28)
								{
									return deleteSpecification;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num2 <= 64)
				{
					switch (num2)
					{
					case 33:
					case 35:
						return deleteSpecification;
					case 34:
						break;
					default:
						switch (num2)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return deleteSpecification;
						}
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 74:
					case 75:
						return deleteSpecification;
					default:
						if (num2 == 82 || num2 == 86)
						{
							return deleteSpecification;
						}
						break;
					}
				}
			}
			else if (num2 <= 119)
			{
				if (num2 <= 95)
				{
					if (num2 == 92 || num2 == 95)
					{
						return deleteSpecification;
					}
				}
				else if (num2 == 106 || num2 == 111 || num2 == 119)
				{
					return deleteSpecification;
				}
			}
			else if (num2 <= 181)
			{
				switch (num2)
				{
				case 123:
				case 125:
				case 126:
				case 129:
				case 131:
				case 132:
				case 134:
				case 138:
				case 140:
				case 142:
				case 143:
				case 144:
					return deleteSpecification;
				case 124:
				case 127:
				case 128:
				case 130:
				case 133:
				case 135:
				case 136:
				case 137:
				case 139:
				case 141:
					break;
				default:
					switch (num2)
					{
					case 156:
					case 160:
					case 161:
					case 162:
						return deleteSpecification;
					case 157:
					case 158:
					case 159:
						break;
					default:
						switch (num2)
						{
						case 167:
						case 170:
						case 172:
						case 173:
						case 176:
						case 180:
						case 181:
							return deleteSpecification;
						case 169:
						{
							WhereClause whereClause = this.dmlWhereClause();
							if (this.inputState.guessing == 0)
							{
								deleteSpecification.WhereClause = whereClause;
								return deleteSpecification;
							}
							return deleteSpecification;
						}
						}
						break;
					}
					break;
				}
			}
			else
			{
				if (num2 == 191 || num2 == 204)
				{
					return deleteSpecification;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return deleteSpecification;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000B5518 File Offset: 0x000B3718
		public TableReference dmlTarget()
		{
			int num = this.LA(1);
			TableReference tableReference;
			switch (num)
			{
			case 107:
			case 108:
			case 109:
				tableReference = this.openRowset();
				break;
			default:
				if (num != 200)
				{
					switch (num)
					{
					case 232:
					case 233:
						break;
					case 234:
						return this.variableDmlTarget();
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				tableReference = this.schemaObjectDmlTarget();
				break;
			}
			return tableReference;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000B5598 File Offset: 0x000B3798
		public WhereClause dmlWhereClause()
		{
			WhereClause whereClause;
			if (this.LA(1) == 169 && TSql80ParserInternal.tokenSet_52_.member(this.LA(2)))
			{
				whereClause = this.whereClause();
			}
			else
			{
				if (this.LA(1) != 169 || this.LA(2) != 37)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				whereClause = this.whereCurrentOfCursorClause();
			}
			return whereClause;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000B5608 File Offset: 0x000B3808
		public InsertSpecification insertSpecification()
		{
			InsertSpecification insertSpecification = base.FragmentFactory.CreateFragment<InsertSpecification>();
			IToken token = this.LT(1);
			this.match(86);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(insertSpecification, token);
			}
			int num = this.LA(1);
			if (num <= 109)
			{
				if (num != 88)
				{
					switch (num)
					{
					case 107:
					case 108:
					case 109:
						goto IL_00DF;
					}
				}
				else
				{
					this.match(88);
					if (this.inputState.guessing == 0)
					{
						insertSpecification.InsertOption = InsertOption.Into;
						goto IL_00DF;
					}
					goto IL_00DF;
				}
			}
			else if (num != 115)
			{
				if (num == 200)
				{
					goto IL_00DF;
				}
				switch (num)
				{
				case 232:
				case 233:
				case 234:
					goto IL_00DF;
				}
			}
			else
			{
				this.match(115);
				if (this.inputState.guessing == 0)
				{
					insertSpecification.InsertOption = InsertOption.Over;
					goto IL_00DF;
				}
				goto IL_00DF;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_00DF:
			TableReference tableReference = this.dmlTarget();
			if (this.inputState.guessing == 0)
			{
				insertSpecification.Target = tableReference;
			}
			bool flag = false;
			if (this.LA(1) == 191 && (this.LA(2) == 200 || this.LA(2) == 232 || this.LA(2) == 233))
			{
				int num2 = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(191);
					int num3 = this.LA(1);
					if (num3 != 200)
					{
						switch (num3)
						{
						case 232:
							this.match(232);
							break;
						case 233:
							this.match(233);
							break;
						default:
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
					}
					else
					{
						this.match(200);
					}
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
				IToken token2 = this.LT(1);
				this.match(191);
				ColumnReferenceExpression columnReferenceExpression = this.insertColumn();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(insertSpecification, token2);
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(insertSpecification, insertSpecification.Columns, columnReferenceExpression);
				}
				while (this.LA(1) == 198)
				{
					this.match(198);
					columnReferenceExpression = this.insertColumn();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(insertSpecification, insertSpecification.Columns, columnReferenceExpression);
					}
				}
				IToken token3 = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(insertSpecification, token3);
				}
			}
			else if (!TSql80ParserInternal.tokenSet_53_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_54_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			int num4 = this.LA(1);
			InsertSource insertSource;
			if (num4 > 61)
			{
				if (num4 != 140)
				{
					if (num4 == 164)
					{
						goto IL_0316;
					}
					if (num4 != 191)
					{
						goto IL_0334;
					}
				}
				insertSource = this.selectInsertSource();
				goto IL_0347;
			}
			if (num4 != 47)
			{
				switch (num4)
				{
				case 60:
				case 61:
					insertSource = this.executeInsertSource();
					goto IL_0347;
				default:
					goto IL_0334;
				}
			}
			IL_0316:
			insertSource = this.valuesInsertSource();
			goto IL_0347;
			IL_0334:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0347:
			if (this.inputState.guessing == 0)
			{
				insertSpecification.InsertSource = insertSource;
			}
			return insertSpecification;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000B5984 File Offset: 0x000B3B84
		public ColumnReferenceExpression insertColumn()
		{
			ColumnReferenceExpression columnReferenceExpression = base.FragmentFactory.CreateFragment<ColumnReferenceExpression>();
			MultiPartIdentifier multiPartIdentifier = this.multiPartIdentifier(-1);
			if (this.inputState.guessing == 0)
			{
				columnReferenceExpression.MultiPartIdentifier = multiPartIdentifier;
			}
			return columnReferenceExpression;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000B59BC File Offset: 0x000B3BBC
		public ValuesInsertSource valuesInsertSource()
		{
			ValuesInsertSource valuesInsertSource = base.FragmentFactory.CreateFragment<ValuesInsertSource>();
			int num = this.LA(1);
			if (num != 47)
			{
				if (num != 164)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(164);
				RowValue rowValue = this.rowValueExpressionWithDefault();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(valuesInsertSource, token);
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<RowValue>(valuesInsertSource, valuesInsertSource.RowValues, rowValue);
				}
			}
			else
			{
				this.match(47);
				IToken token2 = this.LT(1);
				this.match(164);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(valuesInsertSource, token2);
					valuesInsertSource.IsDefaultValues = true;
				}
			}
			return valuesInsertSource;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000B5A7C File Offset: 0x000B3C7C
		public ExecuteInsertSource executeInsertSource()
		{
			ExecuteInsertSource executeInsertSource = base.FragmentFactory.CreateFragment<ExecuteInsertSource>();
			ExecuteSpecification executeSpecification = this.executeSpecification();
			if (this.inputState.guessing == 0)
			{
				executeInsertSource.Execute = executeSpecification;
			}
			return executeInsertSource;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000B5AB4 File Offset: 0x000B3CB4
		public SelectInsertSource selectInsertSource()
		{
			SelectInsertSource selectInsertSource = base.FragmentFactory.CreateFragment<SelectInsertSource>();
			QueryExpression queryExpression = this.queryExpression(null);
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							goto IL_02A0;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							goto IL_02A0;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								goto IL_02A0;
							default:
								if (num == 28)
								{
									goto IL_02A0;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						goto IL_02A0;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							goto IL_02A0;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								goto IL_02A0;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					goto IL_02A0;
				}
			}
			else if (num <= 162)
			{
				if (num <= 113)
				{
					if (num == 95 || num == 106)
					{
						goto IL_02A0;
					}
					switch (num)
					{
					case 111:
						goto IL_02A0;
					case 113:
					{
						OrderByClause orderByClause = this.orderByClause();
						if (this.inputState.guessing == 0)
						{
							queryExpression.OrderByClause = orderByClause;
							goto IL_02A0;
						}
						goto IL_02A0;
					}
					}
				}
				else
				{
					if (num == 119)
					{
						goto IL_02A0;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_02A0;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							goto IL_02A0;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					goto IL_02A0;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						goto IL_02A0;
					}
					switch (num)
					{
					case 180:
					case 181:
						goto IL_02A0;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					goto IL_02A0;
				}
				switch (num)
				{
				case 219:
				case 220:
					goto IL_02A0;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_02A0:
			if (this.inputState.guessing == 0)
			{
				selectInsertSource.Select = queryExpression;
			}
			return selectInsertSource;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x000B5D78 File Offset: 0x000B3F78
		public UpdateSpecification updateSpecification()
		{
			UpdateSpecification updateSpecification = base.FragmentFactory.CreateFragment<UpdateSpecification>();
			IToken token = this.LT(1);
			this.match(160);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(updateSpecification, token);
			}
			TableReference tableReference = this.dmlTarget();
			if (this.inputState.guessing == 0)
			{
				updateSpecification.Target = tableReference;
			}
			this.setClausesList(updateSpecification, updateSpecification.SetClauses);
			FromClause fromClause = this.fromClauseOpt();
			if (this.inputState.guessing == 0)
			{
				updateSpecification.FromClause = fromClause;
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return updateSpecification;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return updateSpecification;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return updateSpecification;
							default:
								if (num == 28)
								{
									return updateSpecification;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return updateSpecification;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return updateSpecification;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return updateSpecification;
					default:
						if (num == 82 || num == 86)
						{
							return updateSpecification;
						}
						break;
					}
				}
			}
			else if (num <= 119)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return updateSpecification;
					}
				}
				else if (num == 106 || num == 111 || num == 119)
				{
					return updateSpecification;
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 123:
				case 125:
				case 126:
				case 129:
				case 131:
				case 132:
				case 134:
				case 138:
				case 140:
				case 142:
				case 143:
				case 144:
					return updateSpecification;
				case 124:
				case 127:
				case 128:
				case 130:
				case 133:
				case 135:
				case 136:
				case 137:
				case 139:
				case 141:
					break;
				default:
					switch (num)
					{
					case 156:
					case 160:
					case 161:
					case 162:
						return updateSpecification;
					case 157:
					case 158:
					case 159:
						break;
					default:
						switch (num)
						{
						case 167:
						case 170:
						case 172:
						case 173:
						case 176:
						case 180:
						case 181:
							return updateSpecification;
						case 169:
						{
							WhereClause whereClause = this.dmlWhereClause();
							if (this.inputState.guessing == 0)
							{
								updateSpecification.WhereClause = whereClause;
								return updateSpecification;
							}
							return updateSpecification;
						}
						}
						break;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return updateSpecification;
				}
				switch (num)
				{
				case 219:
				case 220:
					return updateSpecification;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000B609C File Offset: 0x000B429C
		public void setClausesList(TSqlFragment vParent, IList<SetClause> setClauses)
		{
			IToken token = this.LT(1);
			this.match(142);
			SetClause setClause = this.setClause();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SetClause>(vParent, setClauses, setClause);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				setClause = this.setClause();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SetClause>(vParent, setClauses, setClause);
				}
			}
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x000B6118 File Offset: 0x000B4318
		public AssignmentSetClause setClause()
		{
			AssignmentSetClause assignmentSetClause = base.FragmentFactory.CreateFragment<AssignmentSetClause>();
			int num = this.LA(1);
			if (num != 200)
			{
				switch (num)
				{
				case 232:
				case 233:
					break;
				case 234:
				{
					VariableReference variableReference = this.variable();
					this.match(206);
					if (this.inputState.guessing == 0)
					{
						assignmentSetClause.Variable = variableReference;
					}
					bool flag = false;
					if ((this.LA(1) == 200 || this.LA(1) == 232 || this.LA(1) == 233) && TSql80ParserInternal.tokenSet_55_.member(this.LA(2)))
					{
						int num2 = this.mark();
						flag = true;
						this.inputState.guessing++;
						try
						{
							this.identifierList(-1);
							this.match(206);
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
						this.setClauseSubItem(assignmentSetClause);
						return assignmentSetClause;
					}
					if (!TSql80ParserInternal.tokenSet_16_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_56_.member(this.LA(2)))
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
					if (this.inputState.guessing == 0)
					{
						assignmentSetClause.NewValue = scalarExpression;
						return assignmentSetClause;
					}
					return assignmentSetClause;
				}
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			this.setClauseSubItem(assignmentSetClause);
			return assignmentSetClause;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000B62AC File Offset: 0x000B44AC
		public void setClauseSubItem(AssignmentSetClause vParent)
		{
			MultiPartIdentifier multiPartIdentifier = this.multiPartIdentifier(-1);
			if (this.inputState.guessing == 0)
			{
				base.CreateSetClauseColumn(vParent, multiPartIdentifier);
			}
			this.match(206);
			ScalarExpression scalarExpression = this.expressionWithDefault();
			if (this.inputState.guessing == 0)
			{
				vParent.NewValue = scalarExpression;
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000B62FC File Offset: 0x000B44FC
		public ScalarExpression expressionWithDefault()
		{
			int num = this.LA(1);
			if (num <= 101)
			{
				if (num <= 41)
				{
					if (num <= 25)
					{
						if (num != 20 && num != 25)
						{
							goto IL_0199;
						}
					}
					else if (num != 34)
					{
						switch (num)
						{
						case 40:
						case 41:
							break;
						default:
							goto IL_0199;
						}
					}
				}
				else if (num <= 81)
				{
					if (num == 47)
					{
						return this.defaultLiteral();
					}
					if (num != 81)
					{
						goto IL_0199;
					}
				}
				else if (num != 93)
				{
					switch (num)
					{
					case 100:
					case 101:
						break;
					default:
						goto IL_0199;
					}
				}
			}
			else if (num <= 147)
			{
				if (num <= 136)
				{
					if (num != 133 && num != 136)
					{
						goto IL_0199;
					}
				}
				else if (num != 141 && num != 147)
				{
					goto IL_0199;
				}
			}
			else if (num <= 193)
			{
				if (num != 163)
				{
					switch (num)
					{
					case 191:
					case 193:
						break;
					case 192:
						goto IL_0199;
					default:
						goto IL_0199;
					}
				}
			}
			else
			{
				switch (num)
				{
				case 197:
				case 199:
				case 200:
					break;
				case 198:
					goto IL_0199;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_0199;
					default:
						goto IL_0199;
					}
					break;
				}
			}
			return this.expression(ExpressionFlags.None);
			IL_0199:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000B64B8 File Offset: 0x000B46B8
		public ExecuteSpecification executeSpecification()
		{
			ExecuteSpecification executeSpecification = base.FragmentFactory.CreateFragment<ExecuteSpecification>();
			this.execStart(executeSpecification);
			this.execTypes(executeSpecification);
			return executeSpecification;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000B64E0 File Offset: 0x000B46E0
		public RowValue rowValueExpressionWithDefault()
		{
			RowValue rowValue = base.FragmentFactory.CreateFragment<RowValue>();
			IToken token = this.LT(1);
			this.match(191);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(rowValue, token);
			}
			this.expressionWithDefaultList(rowValue, rowValue.ColumnValues);
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(rowValue, token2);
			}
			return rowValue;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000B655C File Offset: 0x000B475C
		public void expressionWithDefaultList(TSqlFragment vParent, IList<ScalarExpression> expressions)
		{
			ScalarExpression scalarExpression = this.expressionWithDefault();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(vParent, expressions, scalarExpression);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				scalarExpression = this.expressionWithDefault();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(vParent, expressions, scalarExpression);
				}
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x000B65BC File Offset: 0x000B47BC
		public TableReferenceWithAlias schemaObjectDmlTarget()
		{
			bool flag = false;
			if ((this.LA(1) == 200 || this.LA(1) == 232 || this.LA(1) == 233) && TSql80ParserInternal.tokenSet_57_.member(this.LA(2)))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.schemaObjectFourPartName();
					this.match(191);
					int num2 = this.LA(1);
					if (num2 <= 100)
					{
						if (num2 != 47 && num2 != 100)
						{
							goto IL_0106;
						}
					}
					else
					{
						switch (num2)
						{
						case 192:
							this.match(192);
							goto IL_0119;
						case 193:
							break;
						default:
							if (num2 != 199)
							{
								switch (num2)
								{
								case 221:
								case 222:
								case 223:
								case 224:
								case 225:
								case 230:
								case 231:
								case 234:
									break;
								case 226:
								case 227:
								case 228:
								case 229:
								case 232:
								case 233:
									goto IL_0106;
								default:
									goto IL_0106;
								}
							}
							break;
						}
					}
					this.possibleNegativeConstantWithDefault();
					goto IL_0119;
					IL_0106:
					throw new NoViableAltException(this.LT(1), this.getFilename());
					IL_0119:;
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			TableReferenceWithAlias tableReferenceWithAlias;
			if (flag)
			{
				tableReferenceWithAlias = this.schemaObjectFunctionDmlTarget();
			}
			else
			{
				if ((this.LA(1) != 200 && this.LA(1) != 232 && this.LA(1) != 233) || !TSql80ParserInternal.tokenSet_58_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				tableReferenceWithAlias = this.schemaObjectTableDmlTarget();
			}
			return tableReferenceWithAlias;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000B677C File Offset: 0x000B497C
		public VariableTableReference variableDmlTarget()
		{
			VariableTableReference variableTableReference = base.FragmentFactory.CreateFragment<VariableTableReference>();
			VariableReference variableReference = this.variable();
			if (this.inputState.guessing == 0)
			{
				variableTableReference.Variable = variableReference;
			}
			return variableTableReference;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000B67B4 File Offset: 0x000B49B4
		public SchemaObjectFunctionTableReference schemaObjectFunctionDmlTarget()
		{
			SchemaObjectFunctionTableReference schemaObjectFunctionTableReference = base.FragmentFactory.CreateFragment<SchemaObjectFunctionTableReference>();
			SchemaObjectName schemaObjectName = this.schemaObjectFourPartName();
			if (this.inputState.guessing == 0)
			{
				schemaObjectFunctionTableReference.SchemaObject = schemaObjectName;
			}
			this.parenthesizedOptExpressionWithDefaultList(schemaObjectFunctionTableReference, schemaObjectFunctionTableReference.Parameters);
			return schemaObjectFunctionTableReference;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000B67F8 File Offset: 0x000B49F8
		public NamedTableReference schemaObjectTableDmlTarget()
		{
			NamedTableReference namedTableReference = base.FragmentFactory.CreateFragment<NamedTableReference>();
			SchemaObjectName schemaObjectName = this.schemaObjectFourPartName();
			if (this.inputState.guessing == 0)
			{
				namedTableReference.SchemaObject = schemaObjectName;
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return namedTableReference;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return namedTableReference;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return namedTableReference;
							default:
								if (num == 28)
								{
									return namedTableReference;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return namedTableReference;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 47:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return namedTableReference;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 71:
					case 74:
					case 75:
						return namedTableReference;
					case 72:
					case 73:
						break;
					default:
						if (num == 82 || num == 86)
						{
							return namedTableReference;
						}
						break;
					}
				}
			}
			else if (num <= 119)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return namedTableReference;
					}
				}
				else if (num == 106 || num == 111 || num == 119)
				{
					return namedTableReference;
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 123:
				case 125:
				case 126:
				case 129:
				case 131:
				case 132:
				case 134:
				case 138:
				case 140:
				case 142:
				case 143:
				case 144:
					return namedTableReference;
				case 124:
				case 127:
				case 128:
				case 130:
				case 133:
				case 135:
				case 136:
				case 137:
				case 139:
				case 141:
					break;
				default:
					switch (num)
					{
					case 156:
					case 160:
					case 161:
					case 162:
					case 164:
					case 167:
					case 169:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return namedTableReference;
					case 171:
						this.match(171);
						this.tableHints(namedTableReference, namedTableReference.TableHints, false);
						return namedTableReference;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return namedTableReference;
				}
				switch (num)
				{
				case 219:
				case 220:
					return namedTableReference;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000B6AB8 File Offset: 0x000B4CB8
		public void parenthesizedOptExpressionWithDefaultList(TSqlFragment vParent, IList<ScalarExpression> expressions)
		{
			this.match(191);
			int num = this.LA(1);
			if (num <= 93)
			{
				if (num <= 34)
				{
					if (num != 20 && num != 25 && num != 34)
					{
						goto IL_0193;
					}
				}
				else if (num <= 47)
				{
					switch (num)
					{
					case 40:
					case 41:
						break;
					default:
						if (num != 47)
						{
							goto IL_0193;
						}
						break;
					}
				}
				else if (num != 81 && num != 93)
				{
					goto IL_0193;
				}
			}
			else if (num <= 141)
			{
				if (num <= 133)
				{
					switch (num)
					{
					case 100:
					case 101:
						break;
					default:
						if (num != 133)
						{
							goto IL_0193;
						}
						break;
					}
				}
				else if (num != 136 && num != 141)
				{
					goto IL_0193;
				}
			}
			else if (num <= 163)
			{
				if (num != 147 && num != 163)
				{
					goto IL_0193;
				}
			}
			else
			{
				switch (num)
				{
				case 191:
				case 193:
				case 197:
				case 199:
				case 200:
					break;
				case 192:
					goto IL_01A6;
				case 194:
				case 195:
				case 196:
				case 198:
					goto IL_0193;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_0193;
					default:
						goto IL_0193;
					}
					break;
				}
			}
			this.expressionWithDefaultList(vParent, expressions);
			goto IL_01A6;
			IL_0193:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_01A6:
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000B6C94 File Offset: 0x000B4E94
		public void tableHints(TSqlFragment vParent, IList<TableHint> hints, bool indexHintAllowed)
		{
			IToken token = this.LT(1);
			this.match(191);
			TableHint tableHint = this.tableHint(indexHintAllowed);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TableHint>(vParent, hints, tableHint);
			}
			while (TSql80ParserInternal.tokenSet_59_.member(this.LA(1)))
			{
				int num = this.LA(1);
				if (num <= 84)
				{
					if (num != 78 && num != 84)
					{
						goto IL_0086;
					}
				}
				else if (num != 198)
				{
					if (num != 232)
					{
						goto IL_0086;
					}
				}
				else
				{
					this.match(198);
				}
				tableHint = this.tableHint(indexHintAllowed);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TableHint>(vParent, hints, tableHint);
					continue;
				}
				continue;
				IL_0086:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x000B6D80 File Offset: 0x000B4F80
		public NamedTableReference schemaObjectTableReference(SchemaObjectName vSchemaObjectName)
		{
			NamedTableReference namedTableReference = base.FragmentFactory.CreateFragment<NamedTableReference>();
			namedTableReference.SchemaObject = vSchemaObjectName;
			bool flag = false;
			if ((this.LA(1) == 78 || this.LA(1) == 171 || this.LA(1) == 191) && TSql80ParserInternal.tokenSet_60_.member(this.LA(2)))
			{
				this.nonParameterTableHints(namedTableReference, namedTableReference.TableHints, ref flag);
				int num = this.LA(1);
				if (num <= 106)
				{
					if (num <= 23)
					{
						if (num <= 6)
						{
							if (num != 1 && num != 6)
							{
								goto IL_03C9;
							}
							return namedTableReference;
						}
						else
						{
							switch (num)
							{
							case 9:
								break;
							case 10:
							case 11:
							case 14:
							case 16:
								goto IL_03C9;
							case 12:
							case 13:
							case 15:
							case 17:
								return namedTableReference;
							default:
								switch (num)
								{
								case 22:
								case 23:
									return namedTableReference;
								default:
									goto IL_03C9;
								}
								break;
							}
						}
					}
					else if (num <= 77)
					{
						switch (num)
						{
						case 28:
						case 29:
						case 33:
						case 35:
						case 36:
							return namedTableReference;
						case 30:
						case 31:
						case 32:
						case 34:
							goto IL_03C9;
						default:
							switch (num)
							{
							case 44:
							case 45:
							case 46:
							case 48:
							case 49:
							case 54:
							case 55:
							case 56:
							case 59:
							case 60:
							case 61:
							case 64:
							case 67:
							case 72:
							case 74:
							case 75:
							case 76:
							case 77:
								return namedTableReference;
							case 47:
							case 50:
							case 51:
							case 52:
							case 53:
							case 57:
							case 58:
							case 62:
							case 63:
							case 65:
							case 66:
							case 68:
							case 69:
							case 70:
							case 71:
							case 73:
								goto IL_03C9;
							default:
								goto IL_03C9;
							}
							break;
						}
					}
					else
					{
						switch (num)
						{
						case 82:
						case 85:
						case 86:
						case 87:
						case 90:
						case 92:
						case 93:
						case 95:
							return namedTableReference;
						case 83:
						case 84:
						case 88:
						case 89:
						case 91:
						case 94:
							goto IL_03C9;
						default:
							switch (num)
							{
							case 105:
							case 106:
								return namedTableReference;
							default:
								goto IL_03C9;
							}
							break;
						}
					}
				}
				else if (num <= 194)
				{
					if (num <= 144)
					{
						switch (num)
						{
						case 111:
						case 113:
						case 114:
							return namedTableReference;
						case 112:
							goto IL_03C9;
						default:
							switch (num)
							{
							case 119:
							case 123:
							case 125:
							case 126:
							case 129:
							case 131:
							case 132:
							case 133:
							case 134:
							case 138:
							case 140:
							case 142:
							case 143:
							case 144:
								return namedTableReference;
							case 120:
							case 121:
							case 122:
							case 124:
							case 127:
							case 128:
							case 130:
							case 135:
							case 136:
							case 137:
							case 139:
							case 141:
								goto IL_03C9;
							default:
								goto IL_03C9;
							}
							break;
						}
					}
					else
					{
						switch (num)
						{
						case 156:
						case 158:
						case 160:
						case 161:
						case 162:
						case 167:
						case 169:
						case 170:
						case 171:
						case 172:
						case 173:
						case 176:
						case 180:
						case 181:
							return namedTableReference;
						case 157:
						case 159:
						case 163:
						case 164:
						case 165:
						case 166:
						case 168:
						case 174:
						case 175:
						case 177:
						case 178:
						case 179:
							goto IL_03C9;
						default:
							switch (num)
							{
							case 191:
							case 192:
							case 194:
								return namedTableReference;
							case 193:
								goto IL_03C9;
							default:
								goto IL_03C9;
							}
							break;
						}
					}
				}
				else if (num <= 204)
				{
					if (num != 198 && num != 204)
					{
						goto IL_03C9;
					}
					return namedTableReference;
				}
				else
				{
					switch (num)
					{
					case 219:
					case 220:
						return namedTableReference;
					default:
						switch (num)
						{
						case 232:
						case 233:
							break;
						default:
							goto IL_03C9;
						}
						break;
					}
				}
				this.simpleTableReferenceAlias(namedTableReference);
				if (this.inputState.guessing == 0 && namedTableReference.Alias != null && flag)
				{
					TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(namedTableReference.Alias);
					return namedTableReference;
				}
				return namedTableReference;
				IL_03C9:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			if (this.LA(1) == 9 || this.LA(1) == 232 || this.LA(1) == 233)
			{
				this.simpleTableReferenceAlias(namedTableReference);
				bool flag2 = false;
				if (this.LA(1) == 191 && this.LA(2) == 221)
				{
					int num2 = this.mark();
					flag2 = true;
					this.inputState.guessing++;
					try
					{
						this.match(191);
						this.integer();
					}
					catch (RecognitionException)
					{
						flag2 = false;
					}
					this.rewind(num2);
					this.inputState.guessing--;
				}
				if (flag2)
				{
					IndexTableHint indexTableHint = this.oldForceIndex();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TableHint>(namedTableReference, namedTableReference.TableHints, indexTableHint);
					}
				}
				else
				{
					bool flag3 = false;
					if ((this.LA(1) == 78 || this.LA(1) == 171 || this.LA(1) == 191) && TSql80ParserInternal.tokenSet_61_.member(this.LA(2)))
					{
						int num3 = this.mark();
						flag3 = true;
						this.inputState.guessing++;
						try
						{
							int num4 = this.LA(1);
							if (num4 != 78)
							{
								if (num4 != 171)
								{
									if (num4 != 191)
									{
										throw new NoViableAltException(this.LT(1), this.getFilename());
									}
									this.match(191);
									int num5 = this.LA(1);
									if (num5 != 78)
									{
										if (num5 != 84)
										{
											switch (num5)
											{
											case 232:
											case 233:
												this.identifier();
												break;
											default:
												throw new NoViableAltException(this.LT(1), this.getFilename());
											}
										}
										else
										{
											this.match(84);
										}
									}
									else
									{
										this.match(78);
									}
								}
								else
								{
									this.match(171);
								}
							}
							else
							{
								this.match(78);
							}
						}
						catch (RecognitionException)
						{
							flag3 = false;
						}
						this.rewind(num3);
						this.inputState.guessing--;
					}
					if (flag3)
					{
						this.nonParameterTableHints(namedTableReference, namedTableReference.TableHints, ref flag);
					}
					else if (!TSql80ParserInternal.tokenSet_32_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_33_.member(this.LA(2)))
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
			}
			else if (!TSql80ParserInternal.tokenSet_32_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_33_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return namedTableReference;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000B7418 File Offset: 0x000B5618
		public SchemaObjectFunctionTableReference schemaObjectFunctionTableReference(SchemaObjectName vSchemaObjectName)
		{
			SchemaObjectFunctionTableReference schemaObjectFunctionTableReference = base.FragmentFactory.CreateFragment<SchemaObjectFunctionTableReference>();
			schemaObjectFunctionTableReference.SchemaObject = vSchemaObjectName;
			this.parenthesizedOptExpressionWithDefaultList(schemaObjectFunctionTableReference, schemaObjectFunctionTableReference.Parameters);
			this.simpleTableReferenceAliasOpt(schemaObjectFunctionTableReference);
			if (this.LA(1) == 191 && (this.LA(2) == 232 || this.LA(2) == 233))
			{
				this.columnNameList(schemaObjectFunctionTableReference, schemaObjectFunctionTableReference.Columns);
			}
			else if (!TSql80ParserInternal.tokenSet_32_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_33_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return schemaObjectFunctionTableReference;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x000B74C0 File Offset: 0x000B56C0
		public void nonParameterTableHints(TSqlFragment vParent, IList<TableHint> hints, ref bool withSpecified)
		{
			int num = this.LA(1);
			if (num != 78)
			{
				if (num != 171 && num != 191)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.simpleTableHints(vParent, hints, ref withSpecified);
				return;
			}
			else
			{
				IToken token = this.LT(1);
				this.match(78);
				if (this.inputState.guessing == 0)
				{
					TableHint tableHint = base.FragmentFactory.CreateFragment<TableHint>();
					TSql80ParserBaseInternal.UpdateTokenInfo(tableHint, token);
					tableHint.HintKind = TableHintKind.HoldLock;
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TableHint>(vParent, hints, tableHint);
				}
				bool flag = false;
				if (this.LA(1) == 191 && this.LA(2) == 221)
				{
					int num2 = this.mark();
					flag = true;
					this.inputState.guessing++;
					try
					{
						this.match(191);
						this.integer();
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
					IndexTableHint indexTableHint = this.oldForceIndex();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TableHint>(vParent, hints, indexTableHint);
						return;
					}
					return;
				}
				else
				{
					bool flag2 = false;
					if ((this.LA(1) == 171 || this.LA(1) == 191) && TSql80ParserInternal.tokenSet_62_.member(this.LA(2)))
					{
						int num3 = this.mark();
						flag2 = true;
						this.inputState.guessing++;
						try
						{
							this.match(191);
							int num4 = this.LA(1);
							if (num4 != 84)
							{
								switch (num4)
								{
								case 232:
								case 233:
									this.identifier();
									break;
								default:
									throw new NoViableAltException(this.LT(1), this.getFilename());
								}
							}
							else
							{
								this.match(84);
							}
						}
						catch (RecognitionException)
						{
							flag2 = false;
						}
						this.rewind(num3);
						this.inputState.guessing--;
					}
					if (flag2)
					{
						this.simpleTableHints(vParent, hints, ref withSpecified);
						return;
					}
					if (TSql80ParserInternal.tokenSet_63_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_33_.member(this.LA(2)))
					{
						return;
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000B7720 File Offset: 0x000B5920
		public IndexTableHint oldForceIndex()
		{
			IndexTableHint indexTableHint = base.FragmentFactory.CreateFragment<IndexTableHint>();
			IToken token = this.LT(1);
			this.match(191);
			Literal literal = this.integer();
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(indexTableHint, token);
				indexTableHint.HintKind = TableHintKind.Index;
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<IdentifierOrValueExpression>(indexTableHint, indexTableHint.IndexValues, base.IdentifierOrValueExpression(literal));
				TSql80ParserBaseInternal.UpdateTokenInfo(indexTableHint, token2);
			}
			return indexTableHint;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000B77A4 File Offset: 0x000B59A4
		public void fullTextTable(FullTextTableReference vParent)
		{
			int num = this.LA(1);
			if (num != 32)
			{
				if (num != 70)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(70);
				if (this.inputState.guessing == 0)
				{
					vParent.FullTextFunctionType = FullTextFunctionType.FreeText;
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					return;
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(32);
				if (this.inputState.guessing == 0)
				{
					vParent.FullTextFunctionType = FullTextFunctionType.Contains;
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
					return;
				}
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000B7834 File Offset: 0x000B5A34
		public void fulltextTableColumnList(FullTextTableReference vParent)
		{
			int num = this.LA(1);
			if (num != 195)
			{
				ColumnReferenceExpression columnReferenceExpression;
				if (num != 200)
				{
					switch (num)
					{
					case 232:
					case 233:
						break;
					default:
						if (this.LA(1) == 191 && this.LA(2) == 195)
						{
							this.match(191);
							columnReferenceExpression = this.starColumnReferenceExpression();
							this.match(192);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(vParent, vParent.Columns, columnReferenceExpression);
								return;
							}
							return;
						}
						else
						{
							if (this.LA(1) == 191 && (this.LA(2) == 200 || this.LA(2) == 232 || this.LA(2) == 233))
							{
								this.match(191);
								columnReferenceExpression = this.identifierColumnReferenceExpression();
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(vParent, vParent.Columns, columnReferenceExpression);
								}
								while (this.LA(1) == 198)
								{
									this.match(198);
									columnReferenceExpression = this.identifierColumnReferenceExpression();
									if (this.inputState.guessing == 0)
									{
										TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(vParent, vParent.Columns, columnReferenceExpression);
									}
								}
								this.match(192);
								return;
							}
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						break;
					}
				}
				columnReferenceExpression = this.identifierColumnReferenceExpression();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(vParent, vParent.Columns, columnReferenceExpression);
					return;
				}
			}
			else
			{
				ColumnReferenceExpression columnReferenceExpression = this.starColumnReferenceExpression();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(vParent, vParent.Columns, columnReferenceExpression);
					return;
				}
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x000B79D4 File Offset: 0x000B5BD4
		public void fulltextTableOptions(FullTextTableReference vParent)
		{
			if (this.LA(1) == 198 && this.LA(2) == 232)
			{
				this.match(198);
				ValueExpression valueExpression = this.languageExpression();
				if (this.inputState.guessing == 0)
				{
					vParent.Language = valueExpression;
				}
				int num = this.LA(1);
				if (num != 192)
				{
					if (num != 198)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(198);
					ValueExpression valueExpression2 = this.unsignedInteger();
					if (this.inputState.guessing == 0)
					{
						vParent.TopN = valueExpression2;
						return;
					}
				}
			}
			else
			{
				if (this.LA(1) != 198 || (this.LA(2) != 221 && this.LA(2) != 234))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(198);
				ValueExpression valueExpression2 = this.unsignedInteger();
				if (this.inputState.guessing == 0)
				{
					vParent.TopN = valueExpression2;
				}
				int num2 = this.LA(1);
				if (num2 != 192)
				{
					if (num2 != 198)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(198);
					ValueExpression valueExpression = this.languageExpression();
					if (this.inputState.guessing == 0)
					{
						vParent.Language = valueExpression;
						return;
					}
				}
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000B7B38 File Offset: 0x000B5D38
		public ColumnReferenceExpression identifierColumnReferenceExpression()
		{
			ColumnReferenceExpression columnReferenceExpression = base.FragmentFactory.CreateFragment<ColumnReferenceExpression>();
			MultiPartIdentifier multiPartIdentifier = this.multiPartIdentifier(1);
			if (this.inputState.guessing == 0)
			{
				columnReferenceExpression.ColumnType = ColumnType.Regular;
				columnReferenceExpression.MultiPartIdentifier = multiPartIdentifier;
			}
			return columnReferenceExpression;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000B7B78 File Offset: 0x000B5D78
		public ColumnReferenceExpression starColumnReferenceExpression()
		{
			ColumnReferenceExpression columnReferenceExpression = base.FragmentFactory.CreateFragment<ColumnReferenceExpression>();
			IToken token = this.LT(1);
			this.match(195);
			if (this.inputState.guessing == 0)
			{
				columnReferenceExpression.ColumnType = ColumnType.Wildcard;
				TSql80ParserBaseInternal.UpdateTokenInfo(columnReferenceExpression, token);
			}
			return columnReferenceExpression;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x000B7BC4 File Offset: 0x000B5DC4
		public ValueExpression languageExpression()
		{
			ValueExpression valueExpression = null;
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "LANGUAGE");
			}
			ValueExpression valueExpression2 = this.binaryOrIntegerOrStringOrVariable();
			if (this.inputState.guessing == 0)
			{
				valueExpression = valueExpression2;
			}
			return valueExpression;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x000B7C18 File Offset: 0x000B5E18
		public ValueExpression unsignedInteger()
		{
			int num = this.LA(1);
			ValueExpression valueExpression;
			if (num != 221)
			{
				if (num != 234)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				valueExpression = this.variable();
			}
			else
			{
				valueExpression = this.integer();
			}
			return valueExpression;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x000B7C68 File Offset: 0x000B5E68
		public ValueExpression binaryOrIntegerOrStringOrVariable()
		{
			int num = this.LA(1);
			ValueExpression valueExpression;
			if (num != 221)
			{
				if (num != 224)
				{
					switch (num)
					{
					case 230:
					case 231:
						return this.stringLiteral();
					case 234:
						return this.variable();
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				valueExpression = this.binary();
			}
			else
			{
				valueExpression = this.integer();
			}
			return valueExpression;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000B7CE8 File Offset: 0x000B5EE8
		public OpenXmlTableReference openXmlParams()
		{
			OpenXmlTableReference openXmlTableReference = base.FragmentFactory.CreateFragment<OpenXmlTableReference>();
			VariableReference variableReference = this.variable();
			this.match(198);
			ValueExpression valueExpression = this.stringOrVariable();
			if (this.inputState.guessing == 0)
			{
				openXmlTableReference.Variable = variableReference;
				openXmlTableReference.RowPattern = valueExpression;
			}
			int num = this.LA(1);
			if (num != 192)
			{
				if (num != 198)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(198);
				ValueExpression valueExpression2 = this.unsignedInteger();
				if (this.inputState.guessing == 0)
				{
					openXmlTableReference.Flags = valueExpression2;
				}
			}
			return openXmlTableReference;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000B7D8C File Offset: 0x000B5F8C
		public void openXmlWithClauseOpt(OpenXmlTableReference vParent)
		{
			bool flag = false;
			if (this.LA(1) == 171 && TSql80ParserInternal.tokenSet_57_.member(this.LA(2)))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(171);
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag)
			{
				if (this.LA(1) == 171 && this.LA(2) == 191)
				{
					this.match(171);
					this.match(191);
					this.openXmlSchemaItemList(vParent);
					IToken token = this.LT(1);
					this.match(192);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
						return;
					}
				}
				else
				{
					if (this.LA(1) != 171 || (this.LA(2) != 200 && this.LA(2) != 232 && this.LA(2) != 233))
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(171);
					SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
					if (this.inputState.guessing == 0)
					{
						vParent.TableName = schemaObjectName;
						return;
					}
				}
				return;
			}
			if (TSql80ParserInternal.tokenSet_63_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_33_.member(this.LA(2)))
			{
				return;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000B7F2C File Offset: 0x000B612C
		public void openXmlSchemaItemList(OpenXmlTableReference vParent)
		{
			SchemaDeclarationItem schemaDeclarationItem = this.openXmlSchemaItem();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SchemaDeclarationItem>(vParent, vParent.SchemaDeclarationItems, schemaDeclarationItem);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				schemaDeclarationItem = this.openXmlSchemaItem();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SchemaDeclarationItem>(vParent, vParent.SchemaDeclarationItems, schemaDeclarationItem);
				}
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x000B7F98 File Offset: 0x000B6198
		public SchemaDeclarationItem openXmlSchemaItem()
		{
			SchemaDeclarationItem schemaDeclarationItem = base.FragmentFactory.CreateFragment<SchemaDeclarationItem>();
			ColumnDefinitionBase columnDefinitionBase = this.columnDefinitionBasic();
			if (this.inputState.guessing == 0)
			{
				schemaDeclarationItem.ColumnDefinition = columnDefinitionBase;
			}
			int num = this.LA(1);
			if (num != 192 && num != 198)
			{
				switch (num)
				{
				case 230:
				case 231:
				case 234:
				{
					ValueExpression valueExpression = this.stringOrVariable();
					if (this.inputState.guessing == 0)
					{
						schemaDeclarationItem.Mapping = valueExpression;
						return schemaDeclarationItem;
					}
					return schemaDeclarationItem;
				}
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return schemaDeclarationItem;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000B8038 File Offset: 0x000B6238
		public ColumnDefinitionBase columnDefinitionBasic()
		{
			ColumnDefinitionBase columnDefinitionBase = base.FragmentFactory.CreateFragment<ColumnDefinitionBase>();
			Identifier identifier = this.identifier();
			DataTypeReference dataTypeReference = this.scalarDataType();
			if (this.inputState.guessing == 0)
			{
				columnDefinitionBase.ColumnIdentifier = identifier;
				columnDefinitionBase.DataType = dataTypeReference;
			}
			this.collationOpt(columnDefinitionBase);
			return columnDefinitionBase;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x000B8084 File Offset: 0x000B6284
		public TableReferenceWithAlias openRowsetRowset()
		{
			IToken token = this.LT(1);
			this.match(109);
			this.match(191);
			TableReferenceWithAlias tableReferenceWithAlias;
			switch (this.LA(1))
			{
			case 230:
			case 231:
				tableReferenceWithAlias = this.openRowsetParams();
				break;
			case 232:
			case 233:
				tableReferenceWithAlias = this.internalOpenRowsetArgs();
				break;
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(tableReferenceWithAlias, token);
			}
			return tableReferenceWithAlias;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000B8110 File Offset: 0x000B6310
		public OpenQueryTableReference openQueryRowset()
		{
			OpenQueryTableReference openQueryTableReference = base.FragmentFactory.CreateFragment<OpenQueryTableReference>();
			IToken token = this.LT(1);
			this.match(108);
			this.match(191);
			Identifier identifier = this.identifier();
			this.match(198);
			StringLiteral stringLiteral = this.stringLiteral();
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(openQueryTableReference, token);
				openQueryTableReference.LinkedServer = identifier;
				openQueryTableReference.Query = stringLiteral;
				TSql80ParserBaseInternal.UpdateTokenInfo(openQueryTableReference, token2);
			}
			return openQueryTableReference;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x000B81A0 File Offset: 0x000B63A0
		public AdHocTableReference adhocRowset()
		{
			AdHocTableReference adHocTableReference = base.FragmentFactory.CreateFragment<AdHocTableReference>();
			AdHocDataSource adHocDataSource = this.adhocDataSource();
			this.match(200);
			if (this.inputState.guessing == 0)
			{
				adHocTableReference.DataSource = adHocDataSource;
			}
			SchemaObjectNameOrValueExpression schemaObjectNameOrValueExpression = this.objectOrString();
			if (this.inputState.guessing == 0)
			{
				adHocTableReference.Object = schemaObjectNameOrValueExpression;
			}
			return adHocTableReference;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x000B81FC File Offset: 0x000B63FC
		public OpenRowsetTableReference openRowsetParams()
		{
			OpenRowsetTableReference openRowsetTableReference = base.FragmentFactory.CreateFragment<OpenRowsetTableReference>();
			StringLiteral stringLiteral = this.stringLiteral();
			this.match(198);
			if (this.inputState.guessing == 0)
			{
				openRowsetTableReference.ProviderName = stringLiteral;
			}
			if ((this.LA(1) == 230 || this.LA(1) == 231) && this.LA(2) == 204)
			{
				StringLiteral stringLiteral2 = this.stringLiteral();
				if (this.inputState.guessing == 0)
				{
					openRowsetTableReference.DataSource = stringLiteral2;
				}
				this.match(204);
				int num = this.LA(1);
				if (num != 204)
				{
					switch (num)
					{
					case 230:
					case 231:
					{
						StringLiteral stringLiteral3 = this.stringLiteral();
						if (this.inputState.guessing == 0)
						{
							openRowsetTableReference.UserId = stringLiteral3;
						}
						break;
					}
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				this.match(204);
				int num2 = this.LA(1);
				if (num2 != 198)
				{
					switch (num2)
					{
					case 230:
					case 231:
					{
						StringLiteral stringLiteral4 = this.stringLiteral();
						if (this.inputState.guessing == 0)
						{
							openRowsetTableReference.Password = stringLiteral4;
						}
						break;
					}
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
			}
			else
			{
				if ((this.LA(1) != 230 && this.LA(1) != 231) || this.LA(2) != 198)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				StringLiteral stringLiteral5 = this.stringLiteral();
				if (this.inputState.guessing == 0)
				{
					openRowsetTableReference.ProviderString = stringLiteral5;
				}
			}
			this.match(198);
			int num3 = this.LA(1);
			if (num3 != 200)
			{
				switch (num3)
				{
				case 230:
				case 231:
				{
					StringLiteral stringLiteral6 = this.stringLiteral();
					if (this.inputState.guessing == 0)
					{
						openRowsetTableReference.Query = stringLiteral6;
						goto IL_0237;
					}
					goto IL_0237;
				}
				case 232:
				case 233:
					break;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				openRowsetTableReference.Object = schemaObjectName;
			}
			IL_0237:
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(openRowsetTableReference, token);
			}
			this.simpleTableReferenceAliasOpt(openRowsetTableReference);
			return openRowsetTableReference;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x000B8470 File Offset: 0x000B6670
		public InternalOpenRowset internalOpenRowsetArgs()
		{
			InternalOpenRowset internalOpenRowset = base.FragmentFactory.CreateFragment<InternalOpenRowset>();
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				internalOpenRowset.Identifier = identifier;
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				ScalarExpression scalarExpression = this.possibleNegativeConstant();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(internalOpenRowset, internalOpenRowset.VarArgs, scalarExpression);
				}
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(internalOpenRowset, token);
			}
			this.simpleTableReferenceAliasOpt(internalOpenRowset);
			return internalOpenRowset;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000B8514 File Offset: 0x000B6714
		public AdHocDataSource adhocDataSource()
		{
			AdHocDataSource adHocDataSource = base.FragmentFactory.CreateFragment<AdHocDataSource>();
			IToken token = this.LT(1);
			this.match(107);
			this.match(191);
			StringLiteral stringLiteral = this.stringLiteral();
			this.match(198);
			StringLiteral stringLiteral2 = this.stringLiteral();
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(adHocDataSource, token);
				adHocDataSource.ProviderName = stringLiteral;
				adHocDataSource.InitString = stringLiteral2;
				TSql80ParserBaseInternal.UpdateTokenInfo(adHocDataSource, token2);
			}
			return adHocDataSource;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000B85A4 File Offset: 0x000B67A4
		public SchemaObjectNameOrValueExpression objectOrString()
		{
			SchemaObjectNameOrValueExpression schemaObjectNameOrValueExpression = base.FragmentFactory.CreateFragment<SchemaObjectNameOrValueExpression>();
			int num = this.LA(1);
			if (num != 200)
			{
				switch (num)
				{
				case 230:
				case 231:
				{
					Literal literal = this.stringLiteral();
					if (this.inputState.guessing == 0)
					{
						schemaObjectNameOrValueExpression.ValueExpression = literal;
						return schemaObjectNameOrValueExpression;
					}
					return schemaObjectNameOrValueExpression;
				}
				case 232:
				case 233:
					break;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				schemaObjectNameOrValueExpression.SchemaObjectName = schemaObjectName;
			}
			return schemaObjectNameOrValueExpression;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000B863C File Offset: 0x000B683C
		public void simpleTableHints(TSqlFragment vParent, IList<TableHint> hints, ref bool withSpecified)
		{
			int num = this.LA(1);
			if (num != 171)
			{
				if (num != 191)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.tableHints(vParent, hints, true);
				return;
			}
			else
			{
				IToken token = this.LT(1);
				this.match(171);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					withSpecified = true;
				}
				if (this.LA(1) == 191 && this.LA(2) == 221)
				{
					IndexTableHint indexTableHint = this.oldForceIndex();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TableHint>(vParent, hints, indexTableHint);
						return;
					}
					return;
				}
				else
				{
					if (this.LA(1) == 191 && (this.LA(2) == 78 || this.LA(2) == 84 || this.LA(2) == 232))
					{
						this.tableHints(vParent, hints, true);
						return;
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000B873C File Offset: 0x000B693C
		public TableHint tableHint(bool indexHintAllowed)
		{
			int num = this.LA(1);
			if (num != 78)
			{
				if (num == 84)
				{
					return this.indexTableHint(indexHintAllowed);
				}
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			return this.simpleTableHint();
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000B878C File Offset: 0x000B698C
		public TableHint simpleTableHint()
		{
			TableHint tableHint = base.FragmentFactory.CreateFragment<TableHint>();
			int num = this.LA(1);
			if (num != 78)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					tableHint.HintKind = TableHintOptionsHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
					TSql80ParserBaseInternal.UpdateTokenInfo(tableHint, token);
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(78);
				if (this.inputState.guessing == 0)
				{
					tableHint.HintKind = TableHintKind.HoldLock;
					TSql80ParserBaseInternal.UpdateTokenInfo(tableHint, token2);
				}
			}
			return tableHint;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000B883C File Offset: 0x000B6A3C
		public IndexTableHint indexTableHint(bool indexHintAllowed)
		{
			IndexTableHint indexTableHint = base.FragmentFactory.CreateFragment<IndexTableHint>();
			IToken token = this.LT(1);
			this.match(84);
			if (this.inputState.guessing == 0)
			{
				if (!indexHintAllowed)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46074", token, TSqlParserResource.SQL46074Message, new string[0]);
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(indexTableHint, token);
				indexTableHint.HintKind = TableHintKind.Index;
			}
			int num = this.LA(1);
			if (num != 191)
			{
				if (num != 206)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(206);
				IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrInteger();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<IdentifierOrValueExpression>(indexTableHint, indexTableHint.IndexValues, identifierOrValueExpression);
				}
			}
			else
			{
				this.match(191);
				IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrInteger();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<IdentifierOrValueExpression>(indexTableHint, indexTableHint.IndexValues, identifierOrValueExpression);
				}
				while (this.LA(1) == 198)
				{
					this.match(198);
					identifierOrValueExpression = this.identifierOrInteger();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<IdentifierOrValueExpression>(indexTableHint, indexTableHint.IndexValues, identifierOrValueExpression);
					}
				}
				IToken token2 = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(indexTableHint, token2);
				}
			}
			return indexTableHint;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000B8998 File Offset: 0x000B6B98
		public IdentifierOrValueExpression identifierOrInteger()
		{
			IdentifierOrValueExpression identifierOrValueExpression = base.FragmentFactory.CreateFragment<IdentifierOrValueExpression>();
			int num = this.LA(1);
			if (num != 221)
			{
				switch (num)
				{
				case 232:
				case 233:
				{
					Identifier identifier = this.identifier();
					if (this.inputState.guessing == 0)
					{
						identifierOrValueExpression.Identifier = identifier;
					}
					break;
				}
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				Literal literal = this.integer();
				if (this.inputState.guessing == 0)
				{
					identifierOrValueExpression.ValueExpression = literal;
				}
			}
			return identifierOrValueExpression;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000B8A28 File Offset: 0x000B6C28
		public void singleOldStyleTableHint(TSqlFragment vParent, IList<TableHint> hints)
		{
			IToken token = this.LT(1);
			this.match(191);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
			TableHint tableHint = this.tableHint(true);
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TableHint>(vParent, hints, tableHint);
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
			}
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000B8A98 File Offset: 0x000B6C98
		public WhereClause whereCurrentOfCursorClause()
		{
			WhereClause whereClause = base.FragmentFactory.CreateFragment<WhereClause>();
			IToken token = this.LT(1);
			this.match(169);
			this.match(37);
			this.match(102);
			CursorId cursorId = this.cursorId();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(whereClause, token);
				whereClause.Cursor = cursorId;
			}
			return whereClause;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000B8AFC File Offset: 0x000B6CFC
		public ExpressionGroupingSpecification simpleGroupByItem()
		{
			ExpressionGroupingSpecification expressionGroupingSpecification = base.FragmentFactory.CreateFragment<ExpressionGroupingSpecification>();
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				expressionGroupingSpecification.Expression = scalarExpression;
			}
			return expressionGroupingSpecification;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000B8B34 File Offset: 0x000B6D34
		public ExpressionWithSortOrder expressionWithSortOrder()
		{
			ExpressionWithSortOrder expressionWithSortOrder = base.FragmentFactory.CreateFragment<ExpressionWithSortOrder>();
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				expressionWithSortOrder.Expression = scalarExpression;
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 29)
				{
					if (num <= 6)
					{
						if (num != 1 && num != 6)
						{
							goto IL_02DA;
						}
						return expressionWithSortOrder;
					}
					else
					{
						switch (num)
						{
						case 10:
							break;
						case 11:
						case 14:
						case 16:
							goto IL_02DA;
						case 12:
						case 13:
						case 15:
						case 17:
							return expressionWithSortOrder;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return expressionWithSortOrder;
							default:
								switch (num)
								{
								case 28:
								case 29:
									return expressionWithSortOrder;
								default:
									goto IL_02DA;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return expressionWithSortOrder;
					case 34:
						goto IL_02DA;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 59:
						case 60:
						case 61:
						case 64:
						case 67:
							return expressionWithSortOrder;
						case 47:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 62:
						case 63:
						case 65:
						case 66:
							goto IL_02DA;
						case 50:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return expressionWithSortOrder;
							default:
								goto IL_02DA;
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (num == 82)
					{
						return expressionWithSortOrder;
					}
					switch (num)
					{
					case 86:
					case 87:
						return expressionWithSortOrder;
					default:
						if (num != 92)
						{
							goto IL_02DA;
						}
						return expressionWithSortOrder;
					}
				}
				SortOrder sortOrder = this.orderByOption(expressionWithSortOrder);
				if (this.inputState.guessing == 0)
				{
					expressionWithSortOrder.SortOrder = sortOrder;
					return expressionWithSortOrder;
				}
				return expressionWithSortOrder;
			}
			else if (num <= 144)
			{
				if (num <= 106)
				{
					if (num == 95 || num == 106)
					{
						return expressionWithSortOrder;
					}
				}
				else
				{
					if (num == 111 || num == 119)
					{
						return expressionWithSortOrder;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return expressionWithSortOrder;
					}
				}
			}
			else if (num <= 192)
			{
				switch (num)
				{
				case 156:
				case 158:
				case 160:
				case 161:
				case 162:
				case 167:
				case 170:
				case 171:
				case 172:
				case 173:
				case 176:
					return expressionWithSortOrder;
				case 157:
				case 159:
				case 163:
				case 164:
				case 165:
				case 166:
				case 168:
				case 169:
				case 174:
				case 175:
					break;
				default:
					switch (num)
					{
					case 180:
					case 181:
						return expressionWithSortOrder;
					default:
						switch (num)
						{
						case 191:
						case 192:
							return expressionWithSortOrder;
						}
						break;
					}
					break;
				}
			}
			else
			{
				if (num == 198 || num == 204)
				{
					return expressionWithSortOrder;
				}
				switch (num)
				{
				case 219:
				case 220:
					return expressionWithSortOrder;
				}
			}
			IL_02DA:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x000B8E30 File Offset: 0x000B7030
		public ComputeFunction computeFunction()
		{
			ComputeFunction computeFunction = base.FragmentFactory.CreateFragment<ComputeFunction>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				computeFunction.ComputeFunctionType = ComputeFunctionTypeHelper.Instance.ParseOption(token);
			}
			this.match(191);
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				computeFunction.Expression = scalarExpression;
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(computeFunction, token2);
			}
			return computeFunction;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000B8ED0 File Offset: 0x000B70D0
		public SortOrder orderByOption(TSqlFragment vParent)
		{
			SortOrder sortOrder = SortOrder.NotSpecified;
			int num = this.LA(1);
			if (num != 10)
			{
				if (num != 50)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(50);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					sortOrder = SortOrder.Descending;
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(10);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
					sortOrder = SortOrder.Ascending;
				}
			}
			return sortOrder;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x000B8F5C File Offset: 0x000B715C
		public XmlForClause xmlForClause()
		{
			XmlForClause xmlForClause = base.FragmentFactory.CreateFragment<XmlForClause>();
			XmlForClauseOptions xmlForClauseOptions = XmlForClauseOptions.None;
			IToken token = this.LT(1);
			this.match(232);
			XmlForClauseOption xmlForClauseOption = this.xmlForClauseMode();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "XML");
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<XmlForClauseOption>(xmlForClause, xmlForClause.Options, xmlForClauseOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				xmlForClauseOption = this.xmlParam(xmlForClauseOptions);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<XmlForClauseOption>(xmlForClause, xmlForClause.Options, xmlForClauseOption);
					xmlForClauseOptions |= xmlForClauseOption.OptionKind;
				}
			}
			return xmlForClause;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000B9000 File Offset: 0x000B7200
		public UpdateForClause updateForClause()
		{
			UpdateForClause updateForClause = base.FragmentFactory.CreateFragment<UpdateForClause>();
			IToken token = this.LT(1);
			this.match(160);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(updateForClause, token);
			}
			int num = this.LA(1);
			if (num <= 95)
			{
				if (num <= 35)
				{
					if (num <= 17)
					{
						if (num == 1 || num == 6)
						{
							return updateForClause;
						}
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return updateForClause;
						}
					}
					else
					{
						switch (num)
						{
						case 22:
						case 23:
							return updateForClause;
						default:
							if (num == 28)
							{
								return updateForClause;
							}
							switch (num)
							{
							case 33:
							case 35:
								return updateForClause;
							}
							break;
						}
					}
				}
				else if (num <= 82)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
						return updateForClause;
					case 47:
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
						break;
					default:
						switch (num)
						{
						case 74:
						case 75:
							return updateForClause;
						default:
							if (num == 82)
							{
								return updateForClause;
							}
							break;
						}
						break;
					}
				}
				else if (num == 86 || num == 92 || num == 95)
				{
					return updateForClause;
				}
			}
			else if (num <= 162)
			{
				if (num <= 111)
				{
					if (num == 102)
					{
						this.match(102);
						ColumnReferenceExpression columnReferenceExpression = this.column();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(updateForClause, updateForClause.Columns, columnReferenceExpression);
						}
						while (this.LA(1) == 198)
						{
							this.match(198);
							columnReferenceExpression = this.column();
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(updateForClause, updateForClause.Columns, columnReferenceExpression);
							}
						}
						return updateForClause;
					}
					if (num == 106 || num == 111)
					{
						return updateForClause;
					}
				}
				else
				{
					if (num == 119)
					{
						return updateForClause;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return updateForClause;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return updateForClause;
						}
						break;
					}
				}
			}
			else if (num <= 181)
			{
				switch (num)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return updateForClause;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num == 176)
					{
						return updateForClause;
					}
					switch (num)
					{
					case 180:
					case 181:
						return updateForClause;
					}
					break;
				}
			}
			else
			{
				if (num == 191 || num == 204)
				{
					return updateForClause;
				}
				switch (num)
				{
				case 219:
				case 220:
					return updateForClause;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x000B931C File Offset: 0x000B751C
		public XmlForClauseOption xmlForClauseMode()
		{
			XmlForClauseOption xmlForClauseOption = base.FragmentFactory.CreateFragment<XmlForClauseOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				xmlForClauseOption.OptionKind = XmlForClauseModeHelper.Instance.ParseOption(token);
				TSql80ParserBaseInternal.UpdateTokenInfo(xmlForClauseOption, token);
			}
			if (this.LA(1) == 191 && (this.LA(2) == 230 || this.LA(2) == 231))
			{
				IToken token2 = this.LT(1);
				this.match(191);
				Literal literal = this.stringLiteral();
				IToken token3 = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					if (xmlForClauseOption.OptionKind == XmlForClauseOptions.Explicit || xmlForClauseOption.OptionKind == XmlForClauseOptions.Auto)
					{
						throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token2);
					}
					xmlForClauseOption.Value = literal;
					TSql80ParserBaseInternal.UpdateTokenInfo(xmlForClauseOption, token3);
				}
			}
			else if (!TSql80ParserInternal.tokenSet_64_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_11_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return xmlForClauseOption;
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x000B9440 File Offset: 0x000B7640
		public XmlForClauseOption xmlParam(XmlForClauseOptions encountered)
		{
			XmlForClauseOption xmlForClauseOption = base.FragmentFactory.CreateFragment<XmlForClauseOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.LA(1) == 191 && (this.LA(2) == 230 || this.LA(2) == 231))
			{
				IToken token2 = this.LT(1);
				this.match(191);
				Literal literal = this.stringLiteral();
				IToken token3 = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					if (!TSql80ParserBaseInternal.TryMatch(token, "XMLSCHEMA") && !TSql80ParserBaseInternal.TryMatch(token, "ROOT"))
					{
						throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token2);
					}
					xmlForClauseOption.Value = literal;
					TSql80ParserBaseInternal.UpdateTokenInfo(xmlForClauseOption, token3);
				}
			}
			else if (this.LA(1) == 232)
			{
				IToken token4 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					if (TSql80ParserBaseInternal.TryMatch(token, "BINARY"))
					{
						TSql80ParserBaseInternal.Match(token4, "BASE64");
						xmlForClauseOption.OptionKind = XmlForClauseOptions.BinaryBase64;
					}
					else
					{
						TSql80ParserBaseInternal.Match(token, "ELEMENTS");
						if (TSql80ParserBaseInternal.TryMatch(token4, "XSINIL"))
						{
							xmlForClauseOption.OptionKind = XmlForClauseOptions.ElementsXsiNil;
						}
						else
						{
							TSql80ParserBaseInternal.Match(token4, "ABSENT");
							xmlForClauseOption.OptionKind = XmlForClauseOptions.ElementsAbsent;
						}
					}
					TSql80ParserBaseInternal.UpdateTokenInfo(xmlForClauseOption, token4);
				}
			}
			else if (!TSql80ParserInternal.tokenSet_64_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_11_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			if (this.inputState.guessing == 0)
			{
				if (xmlForClauseOption.OptionKind == XmlForClauseOptions.None)
				{
					xmlForClauseOption.OptionKind = XmlForClauseOptionsHelper.Instance.ParseOption(token);
					TSql80ParserBaseInternal.UpdateTokenInfo(xmlForClauseOption, token);
				}
				TSql80ParserBaseInternal.CheckXmlForClauseOptionDuplication(encountered, xmlForClauseOption.OptionKind, token);
			}
			return xmlForClauseOption;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000B962C File Offset: 0x000B782C
		public OptimizerHint hint()
		{
			OptimizerHint optimizerHint;
			if ((this.LA(1) == 113 || this.LA(1) == 232) && TSql80ParserInternal.tokenSet_65_.member(this.LA(2)))
			{
				optimizerHint = this.simpleOptimizerHint();
			}
			else
			{
				if (this.LA(1) != 232 || this.LA(2) != 221)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				optimizerHint = this.literalOptimizerHint();
			}
			return optimizerHint;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000B96A8 File Offset: 0x000B78A8
		public OptimizerHint simpleOptimizerHint()
		{
			OptimizerHint optimizerHint = base.FragmentFactory.CreateFragment<OptimizerHint>();
			if (this.LA(1) == 232 && this.LA(2) == 90)
			{
				IToken token = this.LT(1);
				this.match(232);
				this.match(90);
				if (this.inputState.guessing == 0)
				{
					optimizerHint.HintKind = TSql80ParserBaseInternal.ParseJoinOptimizerHint(token);
				}
			}
			else if (this.LA(1) == 232 && this.LA(2) == 158)
			{
				IToken token2 = this.LT(1);
				this.match(232);
				this.match(158);
				if (this.inputState.guessing == 0)
				{
					optimizerHint.HintKind = TSql80ParserBaseInternal.ParseUnionOptimizerHint(token2);
				}
			}
			else if (this.LA(1) == 232 && this.LA(2) == 113)
			{
				IToken token3 = this.LT(1);
				this.match(232);
				this.match(113);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token3, "FORCE");
					optimizerHint.HintKind = OptimizerHintKind.ForceOrder;
				}
			}
			else if (this.LA(1) == 232 && this.LA(2) == 76)
			{
				IToken token4 = this.LT(1);
				this.match(232);
				this.match(76);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token4, "HASH");
					optimizerHint.HintKind = OptimizerHintKind.HashGroup;
				}
			}
			else if (this.LA(1) == 113)
			{
				this.LT(1);
				this.match(113);
				this.match(76);
				if (this.inputState.guessing == 0)
				{
					optimizerHint.HintKind = OptimizerHintKind.OrderGroup;
				}
			}
			else if (this.LA(1) == 232 && this.LA(2) == 117)
			{
				IToken token5 = this.LT(1);
				this.match(232);
				this.match(117);
				if (this.inputState.guessing == 0)
				{
					optimizerHint.HintKind = PlanOptimizerHintHelper.Instance.ParseOption(token5, SqlVersionFlags.TSql80);
				}
			}
			else
			{
				if (this.LA(1) != 232 || this.LA(2) != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token6 = this.LT(1);
				this.match(232);
				IToken token7 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					if (TSql80ParserBaseInternal.TryMatch(token6, "EXPAND"))
					{
						TSql80ParserBaseInternal.Match(token7, "VIEWS");
						optimizerHint.HintKind = OptimizerHintKind.ExpandViews;
					}
					else
					{
						TSql80ParserBaseInternal.Match(token6, "BYPASS");
						TSql80ParserBaseInternal.Match(token7, "OPTIMIZER_QUEUE");
						optimizerHint.HintKind = OptimizerHintKind.BypassOptimizerQueue;
					}
				}
			}
			return optimizerHint;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000B9988 File Offset: 0x000B7B88
		public LiteralOptimizerHint literalOptimizerHint()
		{
			LiteralOptimizerHint literalOptimizerHint = base.FragmentFactory.CreateFragment<LiteralOptimizerHint>();
			IToken token = this.LT(1);
			this.match(232);
			Literal literal = this.integer();
			if (this.inputState.guessing == 0)
			{
				literalOptimizerHint.HintKind = IntegerOptimizerHintHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
				literalOptimizerHint.Value = literal;
			}
			return literalOptimizerHint;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000B99E4 File Offset: 0x000B7BE4
		public void viewStatementBody(ViewStatementBody vResult)
		{
			int num = 0;
			this.match(166);
			SchemaObjectName schemaObjectName = this.schemaObjectTwoPartName();
			if (this.inputState.guessing == 0)
			{
				vResult.SchemaObjectName = schemaObjectName;
				TSql80ParserBaseInternal.CheckForTemporaryView(schemaObjectName);
				base.ThrowPartialAstIfPhaseOne(vResult);
			}
			int num2 = this.LA(1);
			if (num2 != 9 && num2 != 171)
			{
				if (num2 != 191)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.columnNameList(vResult, vResult.Columns);
			}
			int num3 = this.LA(1);
			if (num3 != 9)
			{
				if (num3 != 171)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(171);
				ViewOption viewOption = this.viewOption();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)viewOption.OptionKind, viewOption);
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ViewOption>(vResult, vResult.ViewOptions, viewOption);
				}
				while (this.LA(1) == 198)
				{
					this.match(198);
					viewOption = this.viewOption();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)viewOption.OptionKind, viewOption);
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ViewOption>(vResult, vResult.ViewOptions, viewOption);
					}
				}
			}
			IToken token = this.LT(1);
			this.match(9);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vResult, token);
			}
			SelectStatement selectStatement = this.subqueryExpressionAsStatement();
			if (this.inputState.guessing == 0)
			{
				vResult.SelectStatement = selectStatement;
			}
			int num4 = this.LA(1);
			if (num4 <= 35)
			{
				if (num4 == 1 || num4 == 35)
				{
					return;
				}
			}
			else
			{
				if (num4 == 75)
				{
					return;
				}
				if (num4 != 171)
				{
					if (num4 == 219)
					{
						return;
					}
				}
				else
				{
					this.match(171);
					this.match(21);
					IToken token2 = this.LT(1);
					this.match(111);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(vResult, token2);
						vResult.WithCheckOption = true;
						return;
					}
					return;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000B9BF4 File Offset: 0x000B7DF4
		public SchemaObjectName schemaObjectTwoPartName()
		{
			SchemaObjectName schemaObjectName = base.FragmentFactory.CreateFragment<SchemaObjectName>();
			List<Identifier> list = this.identifierList(2);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(schemaObjectName, schemaObjectName.Identifiers, list);
			}
			return schemaObjectName;
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x000B9C30 File Offset: 0x000B7E30
		public ViewOption viewOption()
		{
			ViewOption viewOption = base.FragmentFactory.CreateFragment<ViewOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				viewOption.OptionKind = ViewOptionHelper.Instance.ParseOption(token);
				TSql80ParserBaseInternal.UpdateTokenInfo(viewOption, token);
			}
			return viewOption;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000B9C84 File Offset: 0x000B7E84
		public TriggerOption triggerOption()
		{
			TriggerOption triggerOption = base.FragmentFactory.CreateFragment<TriggerOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				triggerOption.OptionKind = TriggerOptionHelper.Instance.ParseOption(token);
				TSql80ParserBaseInternal.UpdateTokenInfo(triggerOption, token);
			}
			return triggerOption;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x000B9CD8 File Offset: 0x000B7ED8
		public void procedureOptions(ProcedureStatementBody vParent)
		{
			int num = 0;
			this.match(171);
			ProcedureOption procedureOption = this.procedureOption();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)procedureOption.OptionKind, procedureOption);
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ProcedureOption>(vParent, vParent.Options, procedureOption);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				procedureOption = this.procedureOption();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.CheckOptionDuplication(ref num, (int)procedureOption.OptionKind, procedureOption);
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ProcedureOption>(vParent, vParent.Options, procedureOption);
				}
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000B9D6C File Offset: 0x000B7F6C
		public ProcedureOption procedureOption()
		{
			ProcedureOption procedureOption = base.FragmentFactory.CreateFragment<ProcedureOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				procedureOption.OptionKind = ProcedureOptionHelper.Instance.ParseOption(token);
				TSql80ParserBaseInternal.UpdateTokenInfo(procedureOption, token);
			}
			return procedureOption;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000B9DC0 File Offset: 0x000B7FC0
		public void procedureStatementBody(ProcedureStatementBody vResult, out bool vParseErrorOccurred)
		{
			vParseErrorOccurred = false;
			try
			{
				switch (this.LA(1))
				{
				case 120:
					this.match(120);
					break;
				case 121:
					this.match(121);
					break;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				ProcedureReference procedureReference = this.procedureReference();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.CheckTwoPartNameForSchemaObjectName(procedureReference.Name, "PROCEDURE");
					vResult.ProcedureReference = procedureReference;
				}
				if (this.inputState.guessing == 0)
				{
					base.ThrowPartialAstIfPhaseOne(vResult);
				}
				this.procedureParameterListOptionalParen(vResult);
				int num = this.LA(1);
				if (num != 9 && num != 67)
				{
					if (num != 171)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.procedureOptions(vResult);
				}
				int num2 = this.LA(1);
				if (num2 != 9)
				{
					if (num2 != 67)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(67);
					this.match(128);
					if (this.inputState.guessing == 0)
					{
						vResult.IsForReplication = true;
					}
				}
				IToken token = this.LT(1);
				this.match(9);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vResult, token);
				}
				int num3 = this.LA(1);
				if (num3 <= 86)
				{
					if (num3 <= 28)
					{
						if (num3 <= 6)
						{
							if (num3 == 1)
							{
								goto IL_03BC;
							}
							if (num3 != 6)
							{
								goto IL_03A9;
							}
						}
						else
						{
							switch (num3)
							{
							case 12:
							case 13:
							case 15:
							case 17:
								break;
							case 14:
							case 16:
								goto IL_03A9;
							default:
								switch (num3)
								{
								case 22:
								case 23:
									break;
								default:
									if (num3 != 28)
									{
										goto IL_03A9;
									}
									break;
								}
								break;
							}
						}
					}
					else if (num3 <= 64)
					{
						switch (num3)
						{
						case 33:
						case 35:
							break;
						case 34:
							goto IL_03A9;
						default:
							switch (num3)
							{
							case 44:
							case 45:
							case 46:
							case 48:
							case 49:
							case 54:
								break;
							case 47:
							case 50:
							case 51:
							case 52:
							case 53:
								goto IL_03A9;
							default:
								switch (num3)
								{
								case 60:
								case 61:
								case 64:
									break;
								case 62:
								case 63:
									goto IL_03A9;
								default:
									goto IL_03A9;
								}
								break;
							}
							break;
						}
					}
					else
					{
						switch (num3)
						{
						case 74:
						case 75:
							break;
						default:
							if (num3 != 82 && num3 != 86)
							{
								goto IL_03A9;
							}
							break;
						}
					}
				}
				else if (num3 <= 144)
				{
					if (num3 <= 95)
					{
						if (num3 != 92 && num3 != 95)
						{
							goto IL_03A9;
						}
					}
					else if (num3 != 106 && num3 != 119)
					{
						switch (num3)
						{
						case 123:
						case 125:
						case 126:
						case 129:
						case 131:
						case 132:
						case 134:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							break;
						case 124:
						case 127:
						case 128:
						case 130:
						case 133:
						case 135:
						case 136:
						case 137:
						case 139:
						case 141:
							goto IL_03A9;
						default:
							goto IL_03A9;
						}
					}
				}
				else if (num3 <= 176)
				{
					switch (num3)
					{
					case 156:
					case 160:
					case 161:
					case 162:
						break;
					case 157:
					case 158:
					case 159:
						goto IL_03A9;
					default:
						switch (num3)
						{
						case 167:
						case 170:
						case 172:
						case 173:
							break;
						case 168:
						case 169:
						case 171:
							goto IL_03A9;
						default:
							if (num3 != 176)
							{
								goto IL_03A9;
							}
							break;
						}
						break;
					}
				}
				else
				{
					switch (num3)
					{
					case 180:
					case 181:
						break;
					default:
						if (num3 != 191)
						{
							switch (num3)
							{
							case 219:
								goto IL_03BC;
							case 220:
								break;
							default:
								goto IL_03A9;
							}
						}
						break;
					}
				}
				StatementList statementList = this.statementList(ref vParseErrorOccurred);
				if (this.inputState.guessing == 0)
				{
					vResult.StatementList = statementList;
					goto IL_03BC;
				}
				goto IL_03BC;
				IL_03A9:
				throw new NoViableAltException(this.LT(1), this.getFilename());
				IL_03BC:;
			}
			catch (NoViableAltException)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				if (!base.PhaseOne || vResult == null || vResult.ProcedureReference == null || vResult.ProcedureReference.Name == null)
				{
					throw;
				}
				base.ThrowPartialAstIfPhaseOne(vResult);
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000BA1E4 File Offset: 0x000B83E4
		public ProcedureReference procedureReference()
		{
			ProcedureReference procedureReference = base.FragmentFactory.CreateFragment<ProcedureReference>();
			SchemaObjectName schemaObjectName = this.schemaObjectFourPartName();
			Literal literal = this.procNumOpt();
			if (this.inputState.guessing == 0)
			{
				procedureReference.Name = schemaObjectName;
				procedureReference.Number = literal;
			}
			return procedureReference;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x000BA228 File Offset: 0x000B8428
		public void procedureParameterListOptionalParen(ProcedureStatementBodyBase vResult)
		{
			int num = this.LA(1);
			if (num <= 67)
			{
				if (num != 9 && num != 67)
				{
					goto IL_00B6;
				}
			}
			else if (num != 171)
			{
				if (num != 191)
				{
					if (num != 234)
					{
						goto IL_00B6;
					}
				}
				else
				{
					this.match(191);
					this.procedureParameterList(vResult);
					IToken token = this.LT(1);
					this.match(192);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(vResult, token);
						return;
					}
					return;
				}
			}
			int num2 = this.LA(1);
			if (num2 <= 67)
			{
				if (num2 == 9 || num2 == 67)
				{
					return;
				}
			}
			else
			{
				if (num2 == 171)
				{
					return;
				}
				if (num2 == 234)
				{
					this.procedureParameterList(vResult);
					return;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_00B6:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000BA300 File Offset: 0x000B8500
		public void procedureParameterList(ProcedureStatementBodyBase vResult)
		{
			ProcedureParameter procedureParameter = this.procedureParameter();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ProcedureParameter>(vResult, vResult.Parameters, procedureParameter);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				procedureParameter = this.procedureParameter();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ProcedureParameter>(vResult, vResult.Parameters, procedureParameter);
				}
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000BA36C File Offset: 0x000B856C
		public ProcedureParameter procedureParameter()
		{
			ProcedureParameter procedureParameter = base.FragmentFactory.CreateFragment<ProcedureParameter>();
			Identifier identifier = this.identifierVariable();
			int num = this.LA(1);
			if (num <= 42)
			{
				if (num == 9)
				{
					this.match(9);
					goto IL_0069;
				}
				if (num == 42)
				{
					goto IL_0069;
				}
			}
			else
			{
				if (num == 53 || num == 96)
				{
					goto IL_0069;
				}
				switch (num)
				{
				case 232:
				case 233:
					goto IL_0069;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0069:
			if (this.inputState.guessing == 0)
			{
				procedureParameter.VariableName = identifier;
			}
			int num2 = this.LA(1);
			if (num2 <= 53)
			{
				if (num2 == 42)
				{
					this.cursorProcedureParameter(procedureParameter);
					return procedureParameter;
				}
				if (num2 != 53)
				{
					goto IL_00C4;
				}
			}
			else if (num2 != 96)
			{
				switch (num2)
				{
				case 232:
				case 233:
					break;
				default:
					goto IL_00C4;
				}
			}
			this.scalarProcedureParameter(procedureParameter, true);
			return procedureParameter;
			IL_00C4:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x000BA454 File Offset: 0x000B8654
		public void cursorProcedureParameter(ProcedureParameter vParent)
		{
			DataTypeReference dataTypeReference = this.cursorDataType();
			if (this.inputState.guessing == 0)
			{
				vParent.DataType = dataTypeReference;
			}
			IToken token = this.LT(1);
			this.match(165);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
				vParent.IsVarying = true;
			}
			IToken token2 = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token2, "OUTPUT", "OUT");
				vParent.Modifier = ParameterModifier.Output;
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
			}
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000BA4EC File Offset: 0x000B86EC
		public IdentifierLiteral identifierLiteral()
		{
			IdentifierLiteral identifierLiteral = base.FragmentFactory.CreateFragment<IdentifierLiteral>();
			switch (this.LA(1))
			{
			case 232:
			{
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(identifierLiteral, token);
					identifierLiteral.SetUnquotedIdentifier(token.getText());
					TSql80ParserBaseInternal.CheckIdentifierLiteralLength(identifierLiteral);
				}
				break;
			}
			case 233:
			{
				IToken token2 = this.LT(1);
				this.match(233);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(identifierLiteral, token2);
					identifierLiteral.SetIdentifier(token2.getText());
					TSql80ParserBaseInternal.CheckIdentifierLiteralLength(identifierLiteral);
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return identifierLiteral;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000BA5B4 File Offset: 0x000B87B4
		public DefaultLiteral defaultLiteral()
		{
			DefaultLiteral defaultLiteral = base.FragmentFactory.CreateFragment<DefaultLiteral>();
			IToken token = this.LT(1);
			this.match(47);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(defaultLiteral, token);
				defaultLiteral.Value = token.getText();
			}
			return defaultLiteral;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000BA600 File Offset: 0x000B8800
		public ValueExpression literal()
		{
			int num = this.LA(1);
			ValueExpression valueExpression;
			if (num != 100)
			{
				if (num != 193)
				{
					switch (num)
					{
					case 221:
						return this.integer();
					case 222:
						return this.numeric();
					case 223:
						return this.real();
					case 224:
						return this.binary();
					case 225:
						return this.moneyLiteral();
					case 230:
					case 231:
						return this.stringLiteral();
					case 234:
						return this.globalVariableOrVariableReference();
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				valueExpression = this.odbcLiteral();
			}
			else
			{
				valueExpression = this.nullLiteral();
			}
			return valueExpression;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x000BA6D4 File Offset: 0x000B88D4
		public UnaryExpression negativeConstant()
		{
			UnaryExpression unaryExpression = base.FragmentFactory.CreateFragment<UnaryExpression>();
			IToken token = this.LT(1);
			this.match(199);
			Literal literal = this.subroutineParameterLiteral();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(unaryExpression, token);
				unaryExpression.UnaryExpressionType = UnaryExpressionType.Negative;
				unaryExpression.Expression = literal;
			}
			return unaryExpression;
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x000BA72C File Offset: 0x000B892C
		public Literal subroutineParameterLiteral()
		{
			switch (this.LA(1))
			{
			case 221:
				return this.integer();
			case 222:
				return this.numeric();
			case 223:
				return this.real();
			case 225:
				return this.moneyLiteral();
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000BA79C File Offset: 0x000B899C
		public void triggerStatementBody(TriggerStatementBody vResult, out bool vParseErrorOccurred)
		{
			vParseErrorOccurred = false;
			this.match(155);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckTwoPartNameForSchemaObjectName(schemaObjectName, "TRIGGER");
				vResult.Name = schemaObjectName;
			}
			this.match(105);
			TriggerObject triggerObject = this.triggerObject();
			if (this.inputState.guessing == 0)
			{
				vResult.TriggerObject = triggerObject;
				base.ThrowPartialAstIfPhaseOne(vResult);
			}
			int num = this.LA(1);
			if (num != 67)
			{
				if (num != 171)
				{
					if (num != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					this.match(171);
					TriggerOption triggerOption = this.triggerOption();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TriggerOption>(vResult, vResult.Options, triggerOption);
					}
					while (this.LA(1) == 198)
					{
						this.match(198);
						triggerOption = this.triggerOption();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TriggerOption>(vResult, vResult.Options, triggerOption);
						}
					}
				}
			}
			this.dmlTriggerMidSection(vResult);
			IToken token = this.LT(1);
			this.match(9);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vResult, token);
			}
			StatementList statementList = this.statementList(ref vParseErrorOccurred);
			if (this.inputState.guessing == 0)
			{
				vResult.StatementList = statementList;
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000BA8F8 File Offset: 0x000B8AF8
		public TriggerObject triggerObject()
		{
			TriggerObject triggerObject = base.FragmentFactory.CreateFragment<TriggerObject>();
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				triggerObject.Name = schemaObjectName;
				triggerObject.TriggerScope = TriggerScope.Normal;
			}
			return triggerObject;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000BA934 File Offset: 0x000B8B34
		public void dmlTriggerMidSection(TriggerStatementBody vParent)
		{
			bool flag = false;
			int num = this.LA(1);
			if (num != 67)
			{
				if (num != 232)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(232);
				int num2 = this.LA(1);
				if (num2 <= 86)
				{
					if (num2 == 48 || num2 == 86)
					{
						goto IL_00BC;
					}
				}
				else if (num2 != 102)
				{
					if (num2 == 160)
					{
						goto IL_00BC;
					}
				}
				else
				{
					this.LT(1);
					this.match(102);
					if (this.inputState.guessing == 0)
					{
						flag = true;
						goto IL_00BC;
					}
					goto IL_00BC;
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
				IL_00BC:
				if (this.inputState.guessing == 0)
				{
					if (flag)
					{
						TSql80ParserBaseInternal.Match(token, "INSTEAD");
						vParent.TriggerType = TriggerType.InsteadOf;
					}
					else
					{
						TSql80ParserBaseInternal.Match(token, "AFTER");
						vParent.TriggerType = TriggerType.After;
					}
				}
			}
			else
			{
				this.match(67);
				if (this.inputState.guessing == 0)
				{
					vParent.TriggerType = TriggerType.For;
				}
			}
			TriggerAction triggerAction = this.dmlTriggerAction();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TriggerAction>(vParent, vParent.TriggerActions, triggerAction);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				triggerAction = this.dmlTriggerAction();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<TriggerAction>(vParent, vParent.TriggerActions, triggerAction);
				}
			}
			int num3 = this.LA(1);
			if (num3 != 9 && num3 != 99)
			{
				if (num3 != 171)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(171);
				IToken token2 = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.Match(token2, "APPEND");
					vParent.WithAppend = true;
				}
			}
			int num4 = this.LA(1);
			if (num4 != 9)
			{
				if (num4 != 99)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(99);
				this.match(67);
				this.match(128);
				if (this.inputState.guessing == 0)
				{
					vParent.IsNotForReplication = true;
					return;
				}
			}
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000BAB70 File Offset: 0x000B8D70
		public TriggerAction dmlTriggerAction()
		{
			TriggerAction triggerAction = base.FragmentFactory.CreateFragment<TriggerAction>();
			int num = this.LA(1);
			if (num != 48)
			{
				if (num != 86)
				{
					if (num != 160)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					IToken token = this.LT(1);
					this.match(160);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(triggerAction, token);
						triggerAction.TriggerActionType = TriggerActionType.Update;
					}
				}
				else
				{
					IToken token2 = this.LT(1);
					this.match(86);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(triggerAction, token2);
						triggerAction.TriggerActionType = TriggerActionType.Insert;
					}
				}
			}
			else
			{
				IToken token3 = this.LT(1);
				this.match(48);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(triggerAction, token3);
					triggerAction.TriggerActionType = TriggerActionType.Delete;
				}
			}
			return triggerAction;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x000BAC54 File Offset: 0x000B8E54
		public ExecuteOption executeOption()
		{
			ExecuteOption executeOption = base.FragmentFactory.CreateFragment<ExecuteOption>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "RECOMPILE");
				executeOption.OptionKind = ExecuteOptionKind.Recompile;
			}
			return executeOption;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000BACA4 File Offset: 0x000B8EA4
		public void execStart(TSqlFragment vParent)
		{
			switch (this.LA(1))
			{
			case 60:
			{
				IToken token = this.LT(1);
				this.match(60);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					return;
				}
				break;
			}
			case 61:
			{
				IToken token2 = this.LT(1);
				this.match(61);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
					return;
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000BAD30 File Offset: 0x000B8F30
		public void execTypes(ExecuteSpecification vParent)
		{
			if (this.LA(1) == 191)
			{
				this.match(191);
				ExecutableEntity executableEntity = this.execStrTypes();
				IToken token = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					vParent.ExecutableEntity = executableEntity;
					return;
				}
			}
			else if (TSql80ParserInternal.tokenSet_66_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_67_.member(this.LA(2)))
			{
				ExecutableEntity executableEntity = this.execProcEx();
				if (this.inputState.guessing == 0)
				{
					vParent.ExecutableEntity = executableEntity;
					return;
				}
			}
			else
			{
				if (this.LA(1) != 234 || this.LA(2) != 206)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				VariableReference variableReference = this.variable();
				this.match(206);
				ExecutableEntity executableEntity = this.execProcEx();
				if (this.inputState.guessing == 0)
				{
					vParent.Variable = variableReference;
					vParent.ExecutableEntity = executableEntity;
					return;
				}
			}
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000BAE38 File Offset: 0x000B9038
		public ExecutableEntity execStrTypes()
		{
			ExecutableEntity executableEntity = this.execSqlList();
			int num = this.LA(1);
			if (num != 192)
			{
				if (num != 198)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(198);
				this.setParamList(executableEntity);
			}
			return executableEntity;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000BAE8C File Offset: 0x000B908C
		public ExecutableProcedureReference execProcEx()
		{
			int num = this.LA(1);
			ExecutableProcedureReference executableProcedureReference;
			if (num != 107)
			{
				if (num != 200)
				{
					switch (num)
					{
					case 232:
					case 233:
					case 234:
						break;
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				executableProcedureReference = this.execProc();
			}
			else
			{
				executableProcedureReference = this.adhocDataSourceExecproc();
			}
			return executableProcedureReference;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000BAEF0 File Offset: 0x000B90F0
		public ExecutableStringList execSqlList()
		{
			ExecutableStringList executableStringList = base.FragmentFactory.CreateFragment<ExecutableStringList>();
			ValueExpression valueExpression = this.stringOrVariable();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ValueExpression>(executableStringList, executableStringList.Strings, valueExpression);
			}
			while (this.LA(1) == 197)
			{
				this.match(197);
				valueExpression = this.stringOrVariable();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ValueExpression>(executableStringList, executableStringList.Strings, valueExpression);
				}
			}
			return executableStringList;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000BAF68 File Offset: 0x000B9168
		public void setParamList(ExecutableEntity vParent)
		{
			bool flag = false;
			int num = 0;
			ExecuteParameter executeParameter = this.setParam(ref flag, ref num);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ExecuteParameter>(vParent, vParent.Parameters, executeParameter);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				executeParameter = this.setParam(ref flag, ref num);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ExecuteParameter>(vParent, vParent.Parameters, executeParameter);
				}
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x000BAFE0 File Offset: 0x000B91E0
		public ExecutableProcedureReference adhocDataSourceExecproc()
		{
			ExecutableProcedureReference executableProcedureReference = base.FragmentFactory.CreateFragment<ExecutableProcedureReference>();
			AdHocDataSource adHocDataSource = this.adhocDataSource();
			this.match(200);
			ProcedureReferenceName procedureReferenceName = this.procObjectReference();
			if (this.inputState.guessing == 0)
			{
				executableProcedureReference.AdHocDataSource = adHocDataSource;
				executableProcedureReference.ProcedureReference = procedureReferenceName;
			}
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num != 1 && num != 6)
						{
							goto IL_02F7;
						}
						return executableProcedureReference;
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return executableProcedureReference;
						case 14:
						case 16:
							goto IL_02F7;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return executableProcedureReference;
							default:
								if (num != 28)
								{
									goto IL_02F7;
								}
								return executableProcedureReference;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return executableProcedureReference;
					case 34:
						goto IL_02F7;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return executableProcedureReference;
						case 47:
							break;
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							goto IL_02F7;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return executableProcedureReference;
							default:
								goto IL_02F7;
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (num != 82 && num != 86 && num != 92)
					{
						goto IL_02F7;
					}
					return executableProcedureReference;
				}
			}
			else if (num <= 144)
			{
				if (num <= 106)
				{
					if (num == 95)
					{
						return executableProcedureReference;
					}
					if (num != 100)
					{
						if (num != 106)
						{
							goto IL_02F7;
						}
						return executableProcedureReference;
					}
				}
				else
				{
					if (num == 111 || num == 119)
					{
						return executableProcedureReference;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return executableProcedureReference;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						goto IL_02F7;
					default:
						goto IL_02F7;
					}
				}
			}
			else if (num <= 193)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return executableProcedureReference;
				case 157:
				case 158:
				case 159:
					goto IL_02F7;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 171:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return executableProcedureReference;
					case 168:
					case 169:
					case 174:
					case 175:
					case 177:
					case 178:
					case 179:
						goto IL_02F7;
					default:
						switch (num)
						{
						case 191:
							return executableProcedureReference;
						case 192:
							goto IL_02F7;
						case 193:
							break;
						default:
							goto IL_02F7;
						}
						break;
					}
					break;
				}
			}
			else if (num != 199)
			{
				if (num == 204)
				{
					return executableProcedureReference;
				}
				switch (num)
				{
				case 219:
				case 220:
					return executableProcedureReference;
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
					break;
				case 226:
				case 227:
				case 228:
				case 229:
					goto IL_02F7;
				default:
					goto IL_02F7;
				}
			}
			this.setParamList(executableProcedureReference);
			return executableProcedureReference;
			IL_02F7:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x000BB2F8 File Offset: 0x000B94F8
		public ProcedureReferenceName procObjectReference()
		{
			ProcedureReferenceName procedureReferenceName = base.FragmentFactory.CreateFragment<ProcedureReferenceName>();
			ProcedureReference procedureReference = this.procedureReference();
			if (this.inputState.guessing == 0)
			{
				procedureReferenceName.ProcedureReference = procedureReference;
			}
			return procedureReferenceName;
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x000BB330 File Offset: 0x000B9530
		public ProcedureReferenceName varObjectReference()
		{
			ProcedureReferenceName procedureReferenceName = base.FragmentFactory.CreateFragment<ProcedureReferenceName>();
			VariableReference variableReference = this.variable();
			if (this.inputState.guessing == 0)
			{
				procedureReferenceName.ProcedureVariable = variableReference;
			}
			return procedureReferenceName;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x000BB368 File Offset: 0x000B9568
		public Literal procNumOpt()
		{
			Literal literal = null;
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return literal;
						}
					}
					else
					{
						switch (num)
						{
						case 9:
						case 12:
						case 13:
						case 15:
						case 17:
							return literal;
						case 10:
						case 11:
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return literal;
							default:
								if (num == 28)
								{
									return literal;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 33:
					case 35:
						return literal;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 47:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
						case 67:
							return literal;
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
						case 65:
						case 66:
							break;
						default:
							switch (num)
							{
							case 74:
							case 75:
								return literal;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return literal;
				}
			}
			else if (num <= 144)
			{
				if (num <= 106)
				{
					if (num == 95 || num == 100 || num == 106)
					{
						return literal;
					}
				}
				else
				{
					if (num == 111 || num == 119)
					{
						return literal;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return literal;
					}
				}
			}
			else if (num <= 193)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return literal;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 171:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return literal;
					case 168:
					case 169:
					case 174:
					case 175:
					case 177:
					case 178:
					case 179:
						break;
					default:
						switch (num)
						{
						case 191:
						case 193:
							return literal;
						}
						break;
					}
					break;
				}
			}
			else
			{
				if (num == 199 || num == 204)
				{
					return literal;
				}
				switch (num)
				{
				case 219:
				case 220:
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
					return literal;
				case 236:
					this.match(236);
					return this.integer();
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x000BB670 File Offset: 0x000B9870
		public RealLiteral real()
		{
			RealLiteral realLiteral = base.FragmentFactory.CreateFragment<RealLiteral>();
			IToken token = this.LT(1);
			this.match(223);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(realLiteral, token);
				realLiteral.Value = token.getText();
			}
			return realLiteral;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000BB6C0 File Offset: 0x000B98C0
		public NumericLiteral numeric()
		{
			NumericLiteral numericLiteral = base.FragmentFactory.CreateFragment<NumericLiteral>();
			IToken token = this.LT(1);
			this.match(222);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(numericLiteral, token);
				numericLiteral.Value = token.getText();
			}
			return numericLiteral;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000BB710 File Offset: 0x000B9910
		public ExecuteParameter setParam(ref bool nameEqualsValueWasUsed, ref int parameterNumber)
		{
			ExecuteParameter executeParameter = base.FragmentFactory.CreateFragment<ExecuteParameter>();
			executeParameter.IsOutput = false;
			parameterNumber++;
			if (this.LA(1) == 234 && this.LA(2) == 206)
			{
				VariableReference variableReference = this.variable();
				this.match(206);
				if (this.inputState.guessing == 0)
				{
					executeParameter.Variable = variableReference;
				}
			}
			else if (!TSql80ParserInternal.tokenSet_68_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_69_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			int num = this.LA(1);
			if (num <= 100)
			{
				if (num != 47)
				{
					if (num != 100)
					{
						goto IL_024D;
					}
				}
				else
				{
					Literal literal = this.defaultLiteral();
					if (this.inputState.guessing == 0)
					{
						executeParameter.ParameterValue = literal;
						return executeParameter;
					}
					return executeParameter;
				}
			}
			else if (num != 193 && num != 199)
			{
				switch (num)
				{
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
					break;
				case 226:
				case 227:
				case 228:
				case 229:
					goto IL_024D;
				default:
					goto IL_024D;
				}
			}
			ScalarExpression scalarExpression = this.possibleNegativeConstantOrIdentifier();
			if (this.inputState.guessing == 0)
			{
				executeParameter.ParameterValue = scalarExpression;
				if (executeParameter.Variable != null)
				{
					nameEqualsValueWasUsed = true;
				}
				else if (nameEqualsValueWasUsed)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46089", scalarExpression, TSqlParserResource.SQL46089Message, new string[] { parameterNumber.ToString(CultureInfo.CurrentCulture) });
				}
			}
			if (this.LA(1) == 232 && (base.NextTokenMatches("OUTPUT") || base.NextTokenMatches("OUT")))
			{
				IToken token = this.LT(1);
				this.match(232);
				if (this.inputState.guessing == 0)
				{
					VariableReference variableReference2 = scalarExpression as VariableReference;
					GlobalVariableExpression globalVariableExpression = scalarExpression as GlobalVariableExpression;
					if (variableReference2 == null && globalVariableExpression == null)
					{
						TSql80ParserBaseInternal.ThrowParseErrorException("SQL46088", token, TSqlParserResource.SQL46088Message, new string[0]);
					}
					TSql80ParserBaseInternal.Match(token, "OUTPUT", "OUT");
					executeParameter.IsOutput = true;
					TSql80ParserBaseInternal.UpdateTokenInfo(executeParameter, token);
					return executeParameter;
				}
				return executeParameter;
			}
			else
			{
				if (!TSql80ParserInternal.tokenSet_70_.member(this.LA(1)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				return executeParameter;
			}
			IL_024D:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000BB980 File Offset: 0x000B9B80
		public TableDefinition tableDefinitionCreateTable()
		{
			TableDefinition tableDefinition = base.FragmentFactory.CreateFragment<TableDefinition>();
			this.tableElement(IndexAffectingStatement.CreateTable, tableDefinition, null);
			while (this.LA(1) == 198 && TSql80ParserInternal.tokenSet_71_.member(this.LA(2)))
			{
				this.LT(1);
				this.match(198);
				this.tableElement(IndexAffectingStatement.CreateTable, tableDefinition, null);
			}
			int num = this.LA(1);
			if (num != 192)
			{
				if (num != 198)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(198);
			}
			return tableDefinition;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000BBA1C File Offset: 0x000B9C1C
		public AlterTableAlterColumnStatement alterTableAlterColumnStatement()
		{
			AlterTableAlterColumnStatement alterTableAlterColumnStatement = base.FragmentFactory.CreateFragment<AlterTableAlterColumnStatement>();
			bool flag = false;
			this.match(6);
			this.match(27);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				alterTableAlterColumnStatement.ColumnIdentifier = identifier;
				base.ThrowPartialAstIfPhaseOne(alterTableAlterColumnStatement);
			}
			int num = this.LA(1);
			if (num <= 54)
			{
				if (num != 4)
				{
					switch (num)
					{
					case 53:
						goto IL_0092;
					case 54:
						break;
					default:
						goto IL_048C;
					}
				}
				int num2 = this.LA(1);
				if (num2 != 4)
				{
					if (num2 != 54)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(54);
					if (this.inputState.guessing == 0)
					{
						flag = false;
					}
				}
				else
				{
					this.match(4);
					if (this.inputState.guessing == 0)
					{
						flag = true;
					}
				}
				int num3 = this.LA(1);
				if (num3 != 99)
				{
					if (num3 != 136)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					IToken token = this.LT(1);
					this.match(136);
					if (this.inputState.guessing != 0)
					{
						return alterTableAlterColumnStatement;
					}
					TSql80ParserBaseInternal.UpdateTokenInfo(alterTableAlterColumnStatement, token);
					if (flag)
					{
						alterTableAlterColumnStatement.AlterTableAlterColumnOption = AlterTableAlterColumnOption.AddRowGuidCol;
						return alterTableAlterColumnStatement;
					}
					alterTableAlterColumnStatement.AlterTableAlterColumnOption = AlterTableAlterColumnOption.DropRowGuidCol;
					return alterTableAlterColumnStatement;
				}
				else
				{
					this.match(99);
					this.match(67);
					IToken token2 = this.LT(1);
					this.match(128);
					if (this.inputState.guessing != 0)
					{
						return alterTableAlterColumnStatement;
					}
					TSql80ParserBaseInternal.UpdateTokenInfo(alterTableAlterColumnStatement, token2);
					if (flag)
					{
						alterTableAlterColumnStatement.AlterTableAlterColumnOption = AlterTableAlterColumnOption.AddNotForReplication;
						return alterTableAlterColumnStatement;
					}
					alterTableAlterColumnStatement.AlterTableAlterColumnOption = AlterTableAlterColumnOption.DropNotForReplication;
					return alterTableAlterColumnStatement;
				}
			}
			else if (num != 96)
			{
				switch (num)
				{
				case 232:
				case 233:
					break;
				default:
					goto IL_048C;
				}
			}
			IL_0092:
			DataTypeReference dataTypeReference = this.scalarDataType();
			if (this.inputState.guessing == 0)
			{
				alterTableAlterColumnStatement.DataType = dataTypeReference;
			}
			this.collationOpt(alterTableAlterColumnStatement);
			int num4 = this.LA(1);
			if (num4 <= 92)
			{
				if (num4 <= 28)
				{
					if (num4 <= 6)
					{
						if (num4 == 1 || num4 == 6)
						{
							return alterTableAlterColumnStatement;
						}
					}
					else
					{
						switch (num4)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return alterTableAlterColumnStatement;
						case 14:
						case 16:
							break;
						default:
							switch (num4)
							{
							case 22:
							case 23:
								return alterTableAlterColumnStatement;
							default:
								if (num4 == 28)
								{
									return alterTableAlterColumnStatement;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num4 <= 75)
				{
					switch (num4)
					{
					case 33:
					case 35:
						return alterTableAlterColumnStatement;
					case 34:
						break;
					default:
						switch (num4)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return alterTableAlterColumnStatement;
						case 47:
						case 50:
						case 51:
						case 52:
						case 53:
						case 57:
						case 58:
						case 59:
						case 62:
						case 63:
							break;
						default:
							switch (num4)
							{
							case 74:
							case 75:
								return alterTableAlterColumnStatement;
							}
							break;
						}
						break;
					}
				}
				else if (num4 == 82 || num4 == 86 || num4 == 92)
				{
					return alterTableAlterColumnStatement;
				}
			}
			else if (num4 <= 162)
			{
				if (num4 <= 106)
				{
					if (num4 == 95)
					{
						return alterTableAlterColumnStatement;
					}
					switch (num4)
					{
					case 99:
					case 100:
					{
						bool flag2 = this.nullNotNull(alterTableAlterColumnStatement);
						if (this.inputState.guessing == 0)
						{
							alterTableAlterColumnStatement.AlterTableAlterColumnOption = (flag2 ? AlterTableAlterColumnOption.Null : AlterTableAlterColumnOption.NotNull);
							return alterTableAlterColumnStatement;
						}
						return alterTableAlterColumnStatement;
					}
					default:
						if (num4 == 106)
						{
							return alterTableAlterColumnStatement;
						}
						break;
					}
				}
				else
				{
					if (num4 == 119)
					{
						return alterTableAlterColumnStatement;
					}
					switch (num4)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return alterTableAlterColumnStatement;
					case 124:
					case 127:
					case 128:
					case 130:
					case 133:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num4)
						{
						case 156:
						case 160:
						case 161:
						case 162:
							return alterTableAlterColumnStatement;
						}
						break;
					}
				}
			}
			else if (num4 <= 181)
			{
				switch (num4)
				{
				case 167:
				case 170:
				case 172:
				case 173:
					return alterTableAlterColumnStatement;
				case 168:
				case 169:
				case 171:
					break;
				default:
					if (num4 == 176)
					{
						return alterTableAlterColumnStatement;
					}
					switch (num4)
					{
					case 180:
					case 181:
						return alterTableAlterColumnStatement;
					}
					break;
				}
			}
			else
			{
				if (num4 == 191 || num4 == 204)
				{
					return alterTableAlterColumnStatement;
				}
				switch (num4)
				{
				case 219:
				case 220:
					return alterTableAlterColumnStatement;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_048C:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000BBECC File Offset: 0x000BA0CC
		public AlterTableTriggerModificationStatement alterTableTriggerModificationStatement()
		{
			AlterTableTriggerModificationStatement alterTableTriggerModificationStatement = base.FragmentFactory.CreateFragment<AlterTableTriggerModificationStatement>();
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				alterTableTriggerModificationStatement.TriggerEnforcement = TSql80ParserBaseInternal.ParseTriggerEnforcement(token);
			}
			this.match(155);
			int num = this.LA(1);
			if (num != 5)
			{
				switch (num)
				{
				case 232:
				case 233:
				{
					Identifier identifier = this.identifier();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(alterTableTriggerModificationStatement, alterTableTriggerModificationStatement.TriggerNames, identifier);
					}
					while (this.LA(1) == 198)
					{
						this.match(198);
						identifier = this.identifier();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(alterTableTriggerModificationStatement, alterTableTriggerModificationStatement.TriggerNames, identifier);
						}
					}
					break;
				}
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(5);
				if (this.inputState.guessing == 0)
				{
					alterTableTriggerModificationStatement.All = true;
					TSql80ParserBaseInternal.UpdateTokenInfo(alterTableTriggerModificationStatement, token2);
				}
			}
			if (this.inputState.guessing == 0)
			{
				base.ThrowPartialAstIfPhaseOne(alterTableTriggerModificationStatement);
			}
			return alterTableTriggerModificationStatement;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000BBFFC File Offset: 0x000BA1FC
		public AlterTableDropTableElementStatement alterTableDropTableElementStatement()
		{
			AlterTableDropTableElementStatement alterTableDropTableElementStatement = base.FragmentFactory.CreateFragment<AlterTableDropTableElementStatement>();
			this.match(54);
			AlterTableDropTableElement alterTableDropTableElement = this.alterTableDropTableElement();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<AlterTableDropTableElement>(alterTableDropTableElementStatement, alterTableDropTableElementStatement.AlterTableDropTableElements, alterTableDropTableElement);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				alterTableDropTableElement = this.alterTableDropTableElement();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<AlterTableDropTableElement>(alterTableDropTableElementStatement, alterTableDropTableElementStatement.AlterTableDropTableElements, alterTableDropTableElement);
				}
			}
			if (this.inputState.guessing == 0)
			{
				base.ThrowPartialAstIfPhaseOne(alterTableDropTableElementStatement);
			}
			return alterTableDropTableElementStatement;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000BC090 File Offset: 0x000BA290
		public ConstraintEnforcement constraintEnforcement()
		{
			ConstraintEnforcement constraintEnforcement = ConstraintEnforcement.NotSpecified;
			int num = this.LA(1);
			if (num != 21)
			{
				if (num != 97)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(97);
				if (this.inputState.guessing == 0)
				{
					constraintEnforcement = ConstraintEnforcement.NoCheck;
				}
			}
			else
			{
				this.match(21);
				if (this.inputState.guessing == 0)
				{
					constraintEnforcement = ConstraintEnforcement.Check;
				}
			}
			return constraintEnforcement;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000BC0FC File Offset: 0x000BA2FC
		public AlterTableAddTableElementStatement alterTableAddTableElementStatement(ConstraintEnforcement vExistingRowsCheck)
		{
			AlterTableAddTableElementStatement alterTableAddTableElementStatement = base.FragmentFactory.CreateFragment<AlterTableAddTableElementStatement>();
			alterTableAddTableElementStatement.ExistingRowsCheckEnforcement = vExistingRowsCheck;
			this.match(4);
			TableDefinition tableDefinition = this.tableDefinition(IndexAffectingStatement.AlterTableAddElement, alterTableAddTableElementStatement);
			if (this.inputState.guessing == 0)
			{
				alterTableAddTableElementStatement.Definition = tableDefinition;
			}
			return alterTableAddTableElementStatement;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000BC144 File Offset: 0x000BA344
		public AlterTableConstraintModificationStatement alterTableConstraintModificationStatement(ConstraintEnforcement vExistingRowsCheck)
		{
			AlterTableConstraintModificationStatement alterTableConstraintModificationStatement = base.FragmentFactory.CreateFragment<AlterTableConstraintModificationStatement>();
			alterTableConstraintModificationStatement.ExistingRowsCheckEnforcement = vExistingRowsCheck;
			ConstraintEnforcement constraintEnforcement = this.constraintEnforcement();
			this.match(30);
			if (this.inputState.guessing == 0)
			{
				alterTableConstraintModificationStatement.ConstraintEnforcement = constraintEnforcement;
			}
			int num = this.LA(1);
			if (num != 5)
			{
				switch (num)
				{
				case 232:
				case 233:
				{
					Identifier identifier = this.identifier();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(alterTableConstraintModificationStatement, alterTableConstraintModificationStatement.ConstraintNames, identifier);
					}
					while (this.LA(1) == 198)
					{
						this.match(198);
						identifier = this.identifier();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(alterTableConstraintModificationStatement, alterTableConstraintModificationStatement.ConstraintNames, identifier);
						}
					}
					break;
				}
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				IToken token = this.LT(1);
				this.match(5);
				if (this.inputState.guessing == 0)
				{
					alterTableConstraintModificationStatement.All = true;
					TSql80ParserBaseInternal.UpdateTokenInfo(alterTableConstraintModificationStatement, token);
				}
			}
			if (this.inputState.guessing == 0)
			{
				base.ThrowPartialAstIfPhaseOne(alterTableConstraintModificationStatement);
			}
			return alterTableConstraintModificationStatement;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x000BC268 File Offset: 0x000BA468
		public AlterTableDropTableElement alterTableDropTableElement()
		{
			AlterTableDropTableElement alterTableDropTableElement = base.FragmentFactory.CreateFragment<AlterTableDropTableElement>();
			int num = this.LA(1);
			if (num != 27)
			{
				if (num != 30)
				{
					switch (num)
					{
					case 232:
					case 233:
						break;
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				int num2 = this.LA(1);
				if (num2 != 30)
				{
					switch (num2)
					{
					case 232:
					case 233:
						break;
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				else
				{
					this.match(30);
					if (this.inputState.guessing == 0)
					{
						alterTableDropTableElement.TableElementType = TableElementType.Constraint;
					}
				}
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					alterTableDropTableElement.Name = identifier;
				}
			}
			else
			{
				this.match(27);
				Identifier identifier = this.identifier();
				if (this.inputState.guessing == 0)
				{
					alterTableDropTableElement.TableElementType = TableElementType.Column;
					alterTableDropTableElement.Name = identifier;
				}
			}
			return alterTableDropTableElement;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x000BC360 File Offset: 0x000BA560
		public ColumnDefinition columnDefinition(IndexAffectingStatement statementType, AlterTableAddTableElementStatement vStatement)
		{
			ColumnDefinition columnDefinition = base.FragmentFactory.CreateFragment<ColumnDefinition>();
			IToken token = null;
			Identifier identifier = null;
			Identifier identifier2 = this.identifier();
			if (this.inputState.guessing == 0)
			{
				columnDefinition.ColumnIdentifier = identifier2;
				if (base.PhaseOne && vStatement != null)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnDefinition>(vStatement, vStatement.Definition.ColumnDefinitions, columnDefinition);
					base.ThrowPartialAstIfPhaseOne(vStatement);
				}
			}
			int num = this.LA(1);
			if (num <= 100)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num != 1 && num != 6)
						{
							goto IL_06D8;
						}
					}
					else
					{
						switch (num)
						{
						case 9:
						{
							this.match(9);
							ScalarExpression scalarExpression = this.expression(ExpressionFlags.ScalarSubqueriesDisallowed);
							if (this.inputState.guessing == 0)
							{
								columnDefinition.ComputedColumnExpression = scalarExpression;
							}
							int num2 = this.LA(1);
							if (num2 <= 92)
							{
								if (num2 <= 30)
								{
									if (num2 <= 6)
									{
										if (num2 != 1 && num2 != 6)
										{
											goto IL_06B4;
										}
										return columnDefinition;
									}
									else
									{
										switch (num2)
										{
										case 12:
										case 13:
										case 15:
										case 17:
											return columnDefinition;
										case 14:
										case 16:
											goto IL_06B4;
										default:
											switch (num2)
											{
											case 22:
											case 23:
												return columnDefinition;
											default:
												switch (num2)
												{
												case 28:
													return columnDefinition;
												case 29:
													goto IL_06B4;
												case 30:
													break;
												default:
													goto IL_06B4;
												}
												break;
											}
											break;
										}
									}
								}
								else if (num2 <= 75)
								{
									switch (num2)
									{
									case 33:
									case 35:
										return columnDefinition;
									case 34:
										goto IL_06B4;
									default:
										switch (num2)
										{
										case 44:
										case 45:
										case 46:
										case 48:
										case 49:
										case 54:
										case 55:
										case 56:
										case 60:
										case 61:
										case 64:
											return columnDefinition;
										case 47:
										case 50:
										case 51:
										case 52:
										case 53:
										case 57:
										case 58:
										case 59:
										case 62:
										case 63:
											goto IL_06B4;
										default:
											switch (num2)
											{
											case 74:
											case 75:
												return columnDefinition;
											default:
												goto IL_06B4;
											}
											break;
										}
										break;
									}
								}
								else
								{
									if (num2 != 82 && num2 != 86 && num2 != 92)
									{
										goto IL_06B4;
									}
									return columnDefinition;
								}
							}
							else if (num2 <= 173)
							{
								if (num2 <= 106)
								{
									if (num2 != 95 && num2 != 106)
									{
										goto IL_06B4;
									}
									return columnDefinition;
								}
								else
								{
									switch (num2)
									{
									case 118:
										break;
									case 119:
									case 123:
									case 125:
									case 126:
									case 129:
									case 131:
									case 132:
									case 134:
									case 138:
									case 140:
									case 142:
									case 143:
									case 144:
										return columnDefinition;
									case 120:
									case 121:
									case 122:
									case 124:
									case 127:
									case 128:
									case 130:
									case 133:
									case 135:
									case 136:
									case 137:
									case 139:
									case 141:
										goto IL_06B4;
									default:
										switch (num2)
										{
										case 156:
										case 160:
										case 161:
										case 162:
											return columnDefinition;
										case 157:
										case 158:
											goto IL_06B4;
										case 159:
											break;
										default:
											switch (num2)
											{
											case 167:
											case 170:
											case 172:
											case 173:
												return columnDefinition;
											case 168:
											case 169:
											case 171:
												goto IL_06B4;
											default:
												goto IL_06B4;
											}
											break;
										}
										break;
									}
								}
							}
							else if (num2 <= 192)
							{
								if (num2 == 176)
								{
									return columnDefinition;
								}
								switch (num2)
								{
								case 180:
								case 181:
									return columnDefinition;
								default:
									switch (num2)
									{
									case 191:
									case 192:
										return columnDefinition;
									default:
										goto IL_06B4;
									}
									break;
								}
							}
							else
							{
								if (num2 == 198 || num2 == 204)
								{
									return columnDefinition;
								}
								switch (num2)
								{
								case 219:
								case 220:
									return columnDefinition;
								default:
									goto IL_06B4;
								}
							}
							int num3 = this.LA(1);
							if (num3 != 30)
							{
								if (num3 != 118 && num3 != 159)
								{
									throw new NoViableAltException(this.LT(1), this.getFilename());
								}
							}
							else
							{
								token = this.LT(1);
								this.match(30);
								identifier = this.identifier();
								if (this.inputState.guessing == 0)
								{
									TSql80ParserBaseInternal.ThrowSyntaxErrorIfNotCreateAlterTable(statementType, token);
								}
							}
							ConstraintDefinition constraintDefinition = this.uniqueColumnConstraint();
							if (this.inputState.guessing == 0)
							{
								if (identifier != null)
								{
									TSql80ParserBaseInternal.UpdateTokenInfo(constraintDefinition, token);
									constraintDefinition.ConstraintIdentifier = identifier;
								}
								TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ConstraintDefinition>(columnDefinition, columnDefinition.Constraints, constraintDefinition);
								return columnDefinition;
							}
							return columnDefinition;
							IL_06B4:
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						case 10:
						case 11:
						case 14:
						case 16:
						case 18:
						case 19:
						case 20:
							goto IL_06D8;
						case 12:
						case 13:
						case 15:
						case 17:
						case 21:
						case 22:
						case 23:
							break;
						default:
							switch (num)
							{
							case 28:
							case 30:
								break;
							case 29:
								goto IL_06D8;
							default:
								switch (num)
								{
								case 33:
								case 35:
									break;
								case 34:
									goto IL_06D8;
								default:
									goto IL_06D8;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 79)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 53:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
					case 68:
						break;
					case 50:
					case 51:
					case 52:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
					case 65:
					case 66:
					case 67:
						goto IL_06D8;
					default:
						switch (num)
						{
						case 74:
						case 75:
							break;
						default:
							if (num != 79)
							{
								goto IL_06D8;
							}
							break;
						}
						break;
					}
				}
				else if (num != 82 && num != 86)
				{
					switch (num)
					{
					case 92:
					case 95:
					case 96:
					case 99:
					case 100:
						break;
					case 93:
					case 94:
					case 97:
					case 98:
						goto IL_06D8;
					default:
						goto IL_06D8;
					}
				}
			}
			else if (num <= 176)
			{
				if (num <= 144)
				{
					if (num != 106)
					{
						switch (num)
						{
						case 118:
						case 119:
						case 123:
						case 125:
						case 126:
						case 127:
						case 129:
						case 131:
						case 132:
						case 134:
						case 136:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							break;
						case 120:
						case 121:
						case 122:
						case 124:
						case 128:
						case 130:
						case 133:
						case 135:
						case 137:
						case 139:
						case 141:
							goto IL_06D8;
						default:
							goto IL_06D8;
						}
					}
				}
				else
				{
					switch (num)
					{
					case 156:
					case 159:
					case 160:
					case 161:
					case 162:
						break;
					case 157:
					case 158:
						goto IL_06D8;
					default:
						switch (num)
						{
						case 167:
						case 170:
						case 172:
						case 173:
							break;
						case 168:
						case 169:
						case 171:
							goto IL_06D8;
						default:
							if (num != 176)
							{
								goto IL_06D8;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 198)
			{
				switch (num)
				{
				case 180:
				case 181:
					break;
				default:
					switch (num)
					{
					case 191:
					case 192:
						break;
					default:
						if (num != 198)
						{
							goto IL_06D8;
						}
						break;
					}
					break;
				}
			}
			else if (num != 204)
			{
				switch (num)
				{
				case 219:
				case 220:
					break;
				default:
					switch (num)
					{
					case 232:
					case 233:
						break;
					default:
						goto IL_06D8;
					}
					break;
				}
			}
			this.regularColumnBody(columnDefinition);
			this.columnConstraintListOpt(statementType, columnDefinition);
			return columnDefinition;
			IL_06D8:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000BCA5C File Offset: 0x000BAC5C
		public ConstraintDefinition tableConstraint(IndexAffectingStatement statementType, AlterTableAddTableElementStatement vStatement)
		{
			ConstraintDefinition constraintDefinition = null;
			IToken token = null;
			Identifier identifier = null;
			try
			{
				int num = this.LA(1);
				if (num <= 47)
				{
					if (num == 21)
					{
						goto IL_0080;
					}
					if (num != 30)
					{
						if (num == 47)
						{
							goto IL_0080;
						}
					}
					else
					{
						token = this.LT(1);
						this.match(30);
						identifier = this.identifier();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.ThrowSyntaxErrorIfNotCreateAlterTable(statementType, token);
							goto IL_0080;
						}
						goto IL_0080;
					}
				}
				else if (num == 68 || num == 118 || num == 159)
				{
					goto IL_0080;
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
				IL_0080:
				int num2 = this.LA(1);
				if (num2 <= 47)
				{
					if (num2 == 21)
					{
						constraintDefinition = this.checkConstraint(statementType);
						goto IL_00EC;
					}
					if (num2 == 47)
					{
						constraintDefinition = this.defaultTableConstraint(statementType);
						goto IL_00EC;
					}
				}
				else
				{
					if (num2 == 68)
					{
						constraintDefinition = this.foreignKeyTableConstraint(statementType);
						goto IL_00EC;
					}
					if (num2 == 118 || num2 == 159)
					{
						constraintDefinition = this.uniqueTableConstraint();
						goto IL_00EC;
					}
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
				IL_00EC:
				if (this.inputState.guessing == 0 && identifier != null)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(constraintDefinition, token);
					constraintDefinition.ConstraintIdentifier = identifier;
				}
			}
			catch (PhaseOneConstraintException ex)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
				if (identifier != null)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(ex.Constraint, token);
					ex.Constraint.ConstraintIdentifier = identifier;
				}
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ConstraintDefinition>(vStatement, vStatement.Definition.TableConstraints, ex.Constraint);
				base.ThrowPartialAstIfPhaseOne(vStatement);
			}
			return constraintDefinition;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000BCBE0 File Offset: 0x000BADE0
		public UniqueConstraintDefinition uniqueColumnConstraint()
		{
			UniqueConstraintDefinition uniqueConstraintDefinition = base.FragmentFactory.CreateFragment<UniqueConstraintDefinition>();
			this.uniqueConstraintHeader(uniqueConstraintDefinition, false);
			bool flag = false;
			if (this.LA(1) == 191 && (this.LA(2) == 200 || this.LA(2) == 232 || this.LA(2) == 233))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(191);
					this.columnWithSortOrder();
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag)
			{
				this.match(191);
				ColumnWithSortOrder columnWithSortOrder = this.columnWithSortOrder();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnWithSortOrder>(uniqueConstraintDefinition, uniqueConstraintDefinition.Columns, columnWithSortOrder);
				}
				while (this.LA(1) == 198)
				{
					this.match(198);
					columnWithSortOrder = this.columnWithSortOrder();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnWithSortOrder>(uniqueConstraintDefinition, uniqueConstraintDefinition.Columns, columnWithSortOrder);
					}
				}
				IToken token = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(uniqueConstraintDefinition, token);
				}
			}
			else if (!TSql80ParserInternal.tokenSet_72_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_73_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			this.uniqueConstraintTailOpt(uniqueConstraintDefinition);
			return uniqueConstraintDefinition;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000BCD78 File Offset: 0x000BAF78
		public void regularColumnBody(ColumnDefinition vParent)
		{
			int num = this.LA(1);
			if (num <= 100)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num != 1 && num != 6)
						{
							goto IL_02F3;
						}
						goto IL_0306;
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
						case 21:
						case 22:
						case 23:
							goto IL_0306;
						case 14:
						case 16:
						case 18:
						case 19:
						case 20:
							goto IL_02F3;
						default:
							switch (num)
							{
							case 28:
							case 30:
								goto IL_0306;
							case 29:
								goto IL_02F3;
							default:
								switch (num)
								{
								case 33:
								case 35:
									goto IL_0306;
								case 34:
									goto IL_02F3;
								default:
									goto IL_02F3;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 79)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
					case 68:
						goto IL_0306;
					case 50:
					case 51:
					case 52:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
					case 65:
					case 66:
					case 67:
						goto IL_02F3;
					case 53:
						break;
					default:
						switch (num)
						{
						case 74:
						case 75:
							goto IL_0306;
						default:
							if (num != 79)
							{
								goto IL_02F3;
							}
							goto IL_0306;
						}
						break;
					}
				}
				else
				{
					if (num == 82 || num == 86)
					{
						goto IL_0306;
					}
					switch (num)
					{
					case 92:
					case 95:
					case 99:
					case 100:
						goto IL_0306;
					case 93:
					case 94:
					case 97:
					case 98:
						goto IL_02F3;
					case 96:
						break;
					default:
						goto IL_02F3;
					}
				}
			}
			else if (num <= 176)
			{
				if (num <= 144)
				{
					if (num == 106)
					{
						goto IL_0306;
					}
					switch (num)
					{
					case 118:
					case 119:
					case 123:
					case 125:
					case 126:
					case 127:
					case 129:
					case 131:
					case 132:
					case 134:
					case 136:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_0306;
					case 120:
					case 121:
					case 122:
					case 124:
					case 128:
					case 130:
					case 133:
					case 135:
					case 137:
					case 139:
					case 141:
						goto IL_02F3;
					default:
						goto IL_02F3;
					}
				}
				else
				{
					switch (num)
					{
					case 156:
					case 159:
					case 160:
					case 161:
					case 162:
						goto IL_0306;
					case 157:
					case 158:
						goto IL_02F3;
					default:
						switch (num)
						{
						case 167:
						case 170:
						case 172:
						case 173:
							goto IL_0306;
						case 168:
						case 169:
						case 171:
							goto IL_02F3;
						default:
							if (num != 176)
							{
								goto IL_02F3;
							}
							goto IL_0306;
						}
						break;
					}
				}
			}
			else if (num <= 198)
			{
				switch (num)
				{
				case 180:
				case 181:
					goto IL_0306;
				default:
					switch (num)
					{
					case 191:
					case 192:
						goto IL_0306;
					default:
						if (num != 198)
						{
							goto IL_02F3;
						}
						goto IL_0306;
					}
					break;
				}
			}
			else
			{
				if (num == 204)
				{
					goto IL_0306;
				}
				switch (num)
				{
				case 219:
				case 220:
					goto IL_0306;
				default:
					switch (num)
					{
					case 232:
					case 233:
						break;
					default:
						goto IL_02F3;
					}
					break;
				}
			}
			DataTypeReference dataTypeReference = this.scalarDataType();
			if (this.inputState.guessing == 0)
			{
				vParent.DataType = dataTypeReference;
			}
			this.collationOpt(vParent);
			goto IL_0306;
			IL_02F3:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0306:
			if (this.inputState.guessing == 0)
			{
				base.VerifyColumnDataType(vParent);
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000BD0A0 File Offset: 0x000BB2A0
		public void columnConstraintListOpt(IndexAffectingStatement statementType, ColumnDefinition vResult)
		{
			for (;;)
			{
				int num = this.LA(1);
				if (num <= 79)
				{
					if (num <= 30)
					{
						if (num != 21 && num != 30)
						{
							break;
						}
					}
					else if (num != 47 && num != 68)
					{
						if (num != 79)
						{
							return;
						}
						IdentityOptions identityOptions = this.identityConstraint(statementType);
						if (this.inputState.guessing == 0)
						{
							if (vResult.IdentityOptions != null)
							{
								TSql80ParserBaseInternal.ThrowParseErrorException("SQL46043", identityOptions, TSqlParserResource.SQL46043Message, new string[0]);
							}
							vResult.IdentityOptions = identityOptions;
							continue;
						}
						continue;
					}
				}
				else if (num <= 118)
				{
					switch (num)
					{
					case 99:
					case 100:
						break;
					default:
						if (num != 118)
						{
							return;
						}
						break;
					}
				}
				else if (num != 127)
				{
					if (num == 136)
					{
						this.rowguidcolConstraint(vResult);
						continue;
					}
					if (num != 159)
					{
						return;
					}
				}
				ConstraintDefinition constraintDefinition = this.columnConstraint(statementType);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddConstraintToColumn(constraintDefinition, vResult);
				}
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000BD188 File Offset: 0x000BB388
		public void rowguidcolConstraint(ColumnDefinition vParent)
		{
			IToken token = this.LT(1);
			this.match(136);
			if (this.inputState.guessing == 0)
			{
				if (vParent.IsRowGuidCol)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46042", token, TSqlParserResource.SQL46042Message, new string[0]);
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
				vParent.IsRowGuidCol = true;
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000BD1E4 File Offset: 0x000BB3E4
		public IdentityOptions identityConstraint(IndexAffectingStatement statementType)
		{
			IdentityOptions identityOptions = base.FragmentFactory.CreateFragment<IdentityOptions>();
			IToken token = this.LT(1);
			this.match(79);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(identityOptions, token);
			}
			bool flag = false;
			if (this.LA(1) == 191 && TSql80ParserInternal.tokenSet_74_.member(this.LA(2)))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(191);
					this.seedIncrement();
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag)
			{
				this.match(191);
				ScalarExpression scalarExpression = this.seedIncrement();
				if (this.inputState.guessing == 0)
				{
					identityOptions.IdentitySeed = scalarExpression;
				}
				this.match(198);
				scalarExpression = this.seedIncrement();
				if (this.inputState.guessing == 0)
				{
					identityOptions.IdentityIncrement = scalarExpression;
				}
				IToken token2 = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(identityOptions, token2);
				}
			}
			else if (!TSql80ParserInternal.tokenSet_75_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_76_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			bool flag2 = this.replicationClauseOpt(statementType, identityOptions);
			if (this.inputState.guessing == 0)
			{
				identityOptions.IsIdentityNotForReplication = flag2;
			}
			return identityOptions;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000BD380 File Offset: 0x000BB580
		public ConstraintDefinition columnConstraint(IndexAffectingStatement statementType)
		{
			ConstraintDefinition constraintDefinition = null;
			IToken token = null;
			Identifier identifier = null;
			try
			{
				int num = this.LA(1);
				if (num <= 68)
				{
					if (num <= 30)
					{
						if (num == 21)
						{
							goto IL_009C;
						}
						if (num == 30)
						{
							token = this.LT(1);
							this.match(30);
							identifier = this.identifier();
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.ThrowSyntaxErrorIfNotCreateAlterTable(statementType, token);
								goto IL_009C;
							}
							goto IL_009C;
						}
					}
					else if (num == 47 || num == 68)
					{
						goto IL_009C;
					}
				}
				else if (num <= 118)
				{
					switch (num)
					{
					case 99:
					case 100:
						goto IL_009C;
					default:
						if (num == 118)
						{
							goto IL_009C;
						}
						break;
					}
				}
				else if (num == 127 || num == 159)
				{
					goto IL_009C;
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
				IL_009C:
				int num2 = this.LA(1);
				if (num2 > 68)
				{
					if (num2 <= 118)
					{
						switch (num2)
						{
						case 99:
						case 100:
							constraintDefinition = this.nullableConstraint();
							goto IL_0133;
						default:
							if (num2 != 118)
							{
								goto IL_0120;
							}
							break;
						}
					}
					else
					{
						if (num2 == 127)
						{
							goto IL_010C;
						}
						if (num2 != 159)
						{
							goto IL_0120;
						}
					}
					constraintDefinition = this.uniqueColumnConstraint();
					goto IL_0133;
				}
				if (num2 == 21)
				{
					constraintDefinition = this.checkConstraint(statementType);
					goto IL_0133;
				}
				if (num2 == 47)
				{
					constraintDefinition = this.defaultColumnConstraint(statementType);
					goto IL_0133;
				}
				if (num2 != 68)
				{
					goto IL_0120;
				}
				IL_010C:
				constraintDefinition = this.foreignKeyColumnConstraint(statementType);
				goto IL_0133;
				IL_0120:
				throw new NoViableAltException(this.LT(1), this.getFilename());
				IL_0133:
				if (this.inputState.guessing == 0 && identifier != null)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(constraintDefinition, token);
					constraintDefinition.ConstraintIdentifier = identifier;
				}
			}
			catch (PhaseOneConstraintException)
			{
				if (this.inputState.guessing != 0)
				{
					throw;
				}
			}
			return constraintDefinition;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000BD510 File Offset: 0x000BB710
		public bool replicationClauseOpt(IndexAffectingStatement statementType, TSqlFragment vParent)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.LA(1) == 99 && this.LA(2) == 67)
			{
				int num = this.mark();
				flag2 = true;
				this.inputState.guessing++;
				try
				{
					this.match(99);
					this.match(67);
				}
				catch (RecognitionException)
				{
					flag2 = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag2)
			{
				IToken token = this.LT(1);
				this.match(99);
				this.match(67);
				IToken token2 = this.LT(1);
				this.match(128);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.ThrowSyntaxErrorIfNotCreateAlterTable(statementType, token);
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
					flag = true;
				}
			}
			else if (!TSql80ParserInternal.tokenSet_75_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_77_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return flag;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000BD624 File Offset: 0x000BB824
		public NullableConstraintDefinition nullableConstraint()
		{
			NullableConstraintDefinition nullableConstraintDefinition = base.FragmentFactory.CreateFragment<NullableConstraintDefinition>();
			bool flag = this.nullNotNull(nullableConstraintDefinition);
			if (this.inputState.guessing == 0)
			{
				nullableConstraintDefinition.Nullable = flag;
			}
			return nullableConstraintDefinition;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000BD65C File Offset: 0x000BB85C
		public DefaultConstraintDefinition defaultColumnConstraint(IndexAffectingStatement statementType)
		{
			DefaultConstraintDefinition defaultConstraintDefinition = base.FragmentFactory.CreateFragment<DefaultConstraintDefinition>();
			IToken token = this.LT(1);
			this.match(47);
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.ScalarSubqueriesDisallowed);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(defaultConstraintDefinition, token);
				defaultConstraintDefinition.Expression = scalarExpression;
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return defaultConstraintDefinition;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
						case 21:
						case 22:
						case 23:
							return defaultConstraintDefinition;
						case 14:
						case 16:
						case 18:
						case 19:
						case 20:
							break;
						default:
							switch (num)
							{
							case 28:
							case 30:
								return defaultConstraintDefinition;
							case 29:
								break;
							default:
								switch (num)
								{
								case 33:
								case 35:
									return defaultConstraintDefinition;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
					case 68:
						return defaultConstraintDefinition;
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
					case 65:
					case 66:
					case 67:
						break;
					default:
						switch (num)
						{
						case 74:
						case 75:
							return defaultConstraintDefinition;
						}
						break;
					}
				}
				else if (num == 79 || num == 82 || num == 86)
				{
					return defaultConstraintDefinition;
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return defaultConstraintDefinition;
					}
				}
				else
				{
					switch (num)
					{
					case 99:
					case 100:
						return defaultConstraintDefinition;
					default:
						if (num == 106)
						{
							return defaultConstraintDefinition;
						}
						switch (num)
						{
						case 118:
						case 119:
						case 123:
						case 125:
						case 126:
						case 127:
						case 129:
						case 131:
						case 132:
						case 134:
						case 136:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							return defaultConstraintDefinition;
						}
						break;
					}
				}
			}
			else if (num <= 192)
			{
				switch (num)
				{
				case 156:
				case 159:
				case 160:
				case 161:
				case 162:
				case 167:
				case 170:
				case 172:
				case 173:
				case 176:
					return defaultConstraintDefinition;
				case 157:
				case 158:
				case 163:
				case 164:
				case 165:
				case 166:
				case 168:
				case 169:
				case 174:
				case 175:
					break;
				case 171:
				{
					this.match(171);
					IToken token2 = this.LT(1);
					this.match(164);
					if (this.inputState.guessing == 0)
					{
						if (statementType != IndexAffectingStatement.AlterTableAddElement)
						{
							TSql80ParserBaseInternal.ThrowParseErrorException("SQL46013", token, TSqlParserResource.SQL46013Message, new string[0]);
						}
						TSql80ParserBaseInternal.UpdateTokenInfo(defaultConstraintDefinition, token2);
						defaultConstraintDefinition.WithValues = true;
						return defaultConstraintDefinition;
					}
					return defaultConstraintDefinition;
				}
				default:
					switch (num)
					{
					case 180:
					case 181:
						return defaultConstraintDefinition;
					default:
						switch (num)
						{
						case 191:
						case 192:
							return defaultConstraintDefinition;
						}
						break;
					}
					break;
				}
			}
			else
			{
				if (num == 198 || num == 204)
				{
					return defaultConstraintDefinition;
				}
				switch (num)
				{
				case 219:
				case 220:
					return defaultConstraintDefinition;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000BD9EC File Offset: 0x000BBBEC
		public ForeignKeyConstraintDefinition foreignKeyColumnConstraint(IndexAffectingStatement statementType)
		{
			ForeignKeyConstraintDefinition foreignKeyConstraintDefinition = base.FragmentFactory.CreateFragment<ForeignKeyConstraintDefinition>();
			int num = this.LA(1);
			if (num != 68)
			{
				if (num != 127)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			else
			{
				IToken token = this.LT(1);
				this.match(68);
				this.match(91);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.ThrowSyntaxErrorIfNotCreateAlterTable(statementType, token);
					TSql80ParserBaseInternal.UpdateTokenInfo(foreignKeyConstraintDefinition, token);
				}
				this.foreignConstraintColumnsOpt(foreignKeyConstraintDefinition);
			}
			this.foreignKeyConstraintCommonEnd(statementType, foreignKeyConstraintDefinition);
			return foreignKeyConstraintDefinition;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000BDA74 File Offset: 0x000BBC74
		public CheckConstraintDefinition checkConstraint(IndexAffectingStatement statementType)
		{
			CheckConstraintDefinition checkConstraintDefinition = base.FragmentFactory.CreateFragment<CheckConstraintDefinition>();
			IToken token = this.LT(1);
			this.match(21);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(checkConstraintDefinition, token);
				base.ThrowConstraintIfPhaseOne(checkConstraintDefinition);
			}
			bool flag = this.replicationClauseOpt(statementType, checkConstraintDefinition);
			if (this.inputState.guessing == 0)
			{
				checkConstraintDefinition.NotForReplication = flag;
			}
			this.match(191);
			BooleanExpression booleanExpression = this.booleanExpression(ExpressionFlags.ScalarSubqueriesDisallowed);
			if (this.inputState.guessing == 0)
			{
				checkConstraintDefinition.CheckCondition = booleanExpression;
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(checkConstraintDefinition, token2);
			}
			return checkConstraintDefinition;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000BDB2C File Offset: 0x000BBD2C
		public UniqueConstraintDefinition uniqueTableConstraint()
		{
			UniqueConstraintDefinition uniqueConstraintDefinition = base.FragmentFactory.CreateFragment<UniqueConstraintDefinition>();
			this.uniqueConstraintHeader(uniqueConstraintDefinition, true);
			this.match(191);
			ColumnWithSortOrder columnWithSortOrder = this.columnWithSortOrder();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnWithSortOrder>(uniqueConstraintDefinition, uniqueConstraintDefinition.Columns, columnWithSortOrder);
			}
			while (this.LA(1) == 198)
			{
				this.match(198);
				columnWithSortOrder = this.columnWithSortOrder();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnWithSortOrder>(uniqueConstraintDefinition, uniqueConstraintDefinition.Columns, columnWithSortOrder);
				}
			}
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(uniqueConstraintDefinition, token);
			}
			this.uniqueConstraintTailOpt(uniqueConstraintDefinition);
			return uniqueConstraintDefinition;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000BDBE8 File Offset: 0x000BBDE8
		public DefaultConstraintDefinition defaultTableConstraint(IndexAffectingStatement statementType)
		{
			DefaultConstraintDefinition defaultConstraintDefinition = base.FragmentFactory.CreateFragment<DefaultConstraintDefinition>();
			IToken token = this.LT(1);
			this.match(47);
			if (this.inputState.guessing == 0)
			{
				if (statementType != IndexAffectingStatement.AlterTableAddElement)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46014", token, TSqlParserResource.SQL46014Message, new string[0]);
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(defaultConstraintDefinition, token);
				base.ThrowConstraintIfPhaseOne(defaultConstraintDefinition);
			}
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.ScalarSubqueriesDisallowed);
			this.match(67);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				defaultConstraintDefinition.Expression = scalarExpression;
				defaultConstraintDefinition.Column = identifier;
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 28)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return defaultConstraintDefinition;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return defaultConstraintDefinition;
						case 14:
						case 16:
							break;
						default:
							switch (num)
							{
							case 22:
							case 23:
								return defaultConstraintDefinition;
							default:
								if (num == 28)
								{
									return defaultConstraintDefinition;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 64)
				{
					switch (num)
					{
					case 33:
					case 35:
						return defaultConstraintDefinition;
					case 34:
						break;
					default:
						switch (num)
						{
						case 44:
						case 45:
						case 46:
						case 48:
						case 49:
						case 54:
						case 55:
						case 56:
						case 60:
						case 61:
						case 64:
							return defaultConstraintDefinition;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 74:
					case 75:
						return defaultConstraintDefinition;
					default:
						if (num == 82 || num == 86)
						{
							return defaultConstraintDefinition;
						}
						break;
					}
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return defaultConstraintDefinition;
					}
				}
				else
				{
					if (num == 106 || num == 119)
					{
						return defaultConstraintDefinition;
					}
					switch (num)
					{
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return defaultConstraintDefinition;
					}
				}
			}
			else if (num <= 192)
			{
				switch (num)
				{
				case 156:
				case 160:
				case 161:
				case 162:
					return defaultConstraintDefinition;
				case 157:
				case 158:
				case 159:
					break;
				default:
					switch (num)
					{
					case 167:
					case 170:
					case 172:
					case 173:
					case 176:
					case 180:
					case 181:
						return defaultConstraintDefinition;
					case 168:
					case 169:
					case 174:
					case 175:
					case 177:
					case 178:
					case 179:
						break;
					case 171:
					{
						this.match(171);
						IToken token2 = this.LT(1);
						this.match(164);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(defaultConstraintDefinition, token2);
							defaultConstraintDefinition.WithValues = true;
							return defaultConstraintDefinition;
						}
						return defaultConstraintDefinition;
					}
					default:
						switch (num)
						{
						case 191:
						case 192:
							return defaultConstraintDefinition;
						}
						break;
					}
					break;
				}
			}
			else
			{
				if (num == 198 || num == 204)
				{
					return defaultConstraintDefinition;
				}
				switch (num)
				{
				case 219:
				case 220:
					return defaultConstraintDefinition;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000BDF48 File Offset: 0x000BC148
		public ForeignKeyConstraintDefinition foreignKeyTableConstraint(IndexAffectingStatement statementType)
		{
			ForeignKeyConstraintDefinition foreignKeyConstraintDefinition = base.FragmentFactory.CreateFragment<ForeignKeyConstraintDefinition>();
			IToken token = this.LT(1);
			this.match(68);
			this.match(91);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.ThrowSyntaxErrorIfNotCreateAlterTable(statementType, token);
				TSql80ParserBaseInternal.UpdateTokenInfo(foreignKeyConstraintDefinition, token);
				base.ThrowConstraintIfPhaseOne(foreignKeyConstraintDefinition);
			}
			this.foreignConstraintColumnsOpt(foreignKeyConstraintDefinition);
			this.foreignKeyConstraintCommonEnd(statementType, foreignKeyConstraintDefinition);
			return foreignKeyConstraintDefinition;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000BDFB0 File Offset: 0x000BC1B0
		public void uniqueConstraintHeader(UniqueConstraintDefinition vParent, bool throwInPhaseOne)
		{
			int num = this.LA(1);
			if (num != 118)
			{
				if (num != 159)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(159);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					vParent.IsPrimaryKey = false;
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(118);
				IToken token3 = this.LT(1);
				this.match(91);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token3);
					vParent.IsPrimaryKey = true;
				}
			}
			if (this.inputState.guessing == 0 && throwInPhaseOne)
			{
				base.ThrowConstraintIfPhaseOne(vParent);
			}
			int num2 = this.LA(1);
			if (num2 <= 86)
			{
				if (num2 <= 35)
				{
					if (num2 <= 6)
					{
						if (num2 == 1 || num2 == 6)
						{
							return;
						}
					}
					else
					{
						switch (num2)
						{
						case 12:
						case 13:
						case 15:
						case 17:
						case 21:
						case 22:
						case 23:
						case 28:
						case 30:
							return;
						case 14:
						case 16:
						case 18:
						case 19:
						case 20:
						case 25:
						case 26:
						case 27:
						case 29:
							break;
						case 24:
						{
							IToken token4 = this.LT(1);
							this.match(24);
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token4);
								vParent.Clustered = new bool?(true);
								return;
							}
							return;
						}
						default:
							switch (num2)
							{
							case 33:
							case 35:
								return;
							}
							break;
						}
					}
				}
				else if (num2 <= 75)
				{
					switch (num2)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
					case 68:
						return;
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
					case 65:
					case 66:
					case 67:
						break;
					default:
						switch (num2)
						{
						case 74:
						case 75:
							return;
						}
						break;
					}
				}
				else if (num2 == 79 || num2 == 82 || num2 == 86)
				{
					return;
				}
			}
			else if (num2 <= 176)
			{
				if (num2 <= 106)
				{
					switch (num2)
					{
					case 92:
					case 95:
					case 99:
					case 100:
						return;
					case 93:
					case 94:
					case 96:
					case 97:
						break;
					case 98:
					{
						IToken token5 = this.LT(1);
						this.match(98);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token5);
							vParent.Clustered = new bool?(false);
							return;
						}
						return;
					}
					default:
						switch (num2)
						{
						case 105:
						case 106:
							return;
						}
						break;
					}
				}
				else
				{
					switch (num2)
					{
					case 118:
					case 119:
					case 123:
					case 125:
					case 126:
					case 127:
					case 129:
					case 131:
					case 132:
					case 134:
					case 136:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return;
					case 120:
					case 121:
					case 122:
					case 124:
					case 128:
					case 130:
					case 133:
					case 135:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num2)
						{
						case 156:
						case 159:
						case 160:
						case 161:
						case 162:
						case 167:
						case 170:
						case 171:
						case 172:
						case 173:
						case 176:
							return;
						}
						break;
					}
				}
			}
			else if (num2 <= 192)
			{
				switch (num2)
				{
				case 180:
				case 181:
					return;
				default:
					switch (num2)
					{
					case 191:
					case 192:
						return;
					}
					break;
				}
			}
			else
			{
				if (num2 == 198 || num2 == 204)
				{
					return;
				}
				switch (num2)
				{
				case 219:
				case 220:
					return;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000BE3E4 File Offset: 0x000BC5E4
		public void uniqueConstraintTailOpt(UniqueConstraintDefinition vParent)
		{
			this.uniqueConstraintIndexOptionsOpt(vParent);
			int num = this.LA(1);
			if (num <= 92)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
						case 21:
						case 22:
						case 23:
							return;
						case 14:
						case 16:
						case 18:
						case 19:
						case 20:
							break;
						default:
							switch (num)
							{
							case 28:
							case 30:
								return;
							case 29:
								break;
							default:
								switch (num)
								{
								case 33:
								case 35:
									return;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 79)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
					case 68:
						return;
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
					case 65:
					case 66:
					case 67:
						break;
					default:
						switch (num)
						{
						case 74:
						case 75:
							return;
						default:
							if (num == 79)
							{
								return;
							}
							break;
						}
						break;
					}
				}
				else if (num == 82 || num == 86 || num == 92)
				{
					return;
				}
			}
			else if (num <= 173)
			{
				if (num <= 106)
				{
					if (num == 95)
					{
						return;
					}
					switch (num)
					{
					case 99:
					case 100:
						return;
					default:
						switch (num)
						{
						case 105:
						{
							this.LT(1);
							this.match(105);
							FileGroupOrPartitionScheme fileGroupOrPartitionScheme = this.filegroupOrPartitionScheme();
							if (this.inputState.guessing == 0)
							{
								vParent.OnFileGroupOrPartitionScheme = fileGroupOrPartitionScheme;
								return;
							}
							return;
						}
						case 106:
							return;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 118:
					case 119:
					case 123:
					case 125:
					case 126:
					case 127:
					case 129:
					case 131:
					case 132:
					case 134:
					case 136:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return;
					case 120:
					case 121:
					case 122:
					case 124:
					case 128:
					case 130:
					case 133:
					case 135:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num)
						{
						case 156:
						case 159:
						case 160:
						case 161:
						case 162:
							return;
						case 157:
						case 158:
							break;
						default:
							switch (num)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								return;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 192)
			{
				if (num == 176)
				{
					return;
				}
				switch (num)
				{
				case 180:
				case 181:
					return;
				default:
					switch (num)
					{
					case 191:
					case 192:
						return;
					}
					break;
				}
			}
			else
			{
				if (num == 198 || num == 204)
				{
					return;
				}
				switch (num)
				{
				case 219:
				case 220:
					return;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000BE6E8 File Offset: 0x000BC8E8
		public void uniqueConstraintIndexOptionsOpt(UniqueConstraintDefinition vParent)
		{
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							return;
						}
					}
					else
					{
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
						case 21:
						case 22:
						case 23:
							return;
						case 14:
						case 16:
						case 18:
						case 19:
						case 20:
							break;
						default:
							switch (num)
							{
							case 28:
							case 30:
								return;
							case 29:
								break;
							default:
								switch (num)
								{
								case 33:
								case 35:
									return;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
					case 68:
						return;
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
					case 65:
					case 66:
					case 67:
						break;
					default:
						switch (num)
						{
						case 74:
						case 75:
							return;
						}
						break;
					}
				}
				else if (num == 79 || num == 82 || num == 86)
				{
					return;
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						return;
					}
				}
				else
				{
					switch (num)
					{
					case 99:
					case 100:
						return;
					default:
						switch (num)
						{
						case 105:
						case 106:
							return;
						default:
							switch (num)
							{
							case 118:
							case 119:
							case 123:
							case 125:
							case 126:
							case 127:
							case 129:
							case 131:
							case 132:
							case 134:
							case 136:
							case 138:
							case 140:
							case 142:
							case 143:
							case 144:
								return;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 192)
			{
				switch (num)
				{
				case 156:
				case 159:
				case 160:
				case 161:
				case 162:
				case 167:
				case 170:
				case 172:
				case 173:
				case 176:
					return;
				case 157:
				case 158:
				case 163:
				case 164:
				case 165:
				case 166:
				case 168:
				case 169:
				case 174:
				case 175:
					break;
				case 171:
				{
					this.match(171);
					int num2 = this.LA(1);
					if (num2 != 66)
					{
						if (num2 != 232)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						this.sortedDataOptions();
						int num3 = this.LA(1);
						if (num3 <= 92)
						{
							if (num3 <= 35)
							{
								if (num3 <= 6)
								{
									if (num3 == 1 || num3 == 6)
									{
										return;
									}
								}
								else
								{
									switch (num3)
									{
									case 12:
									case 13:
									case 15:
									case 17:
									case 21:
									case 22:
									case 23:
										return;
									case 14:
									case 16:
									case 18:
									case 19:
									case 20:
										break;
									default:
										switch (num3)
										{
										case 28:
										case 30:
											return;
										case 29:
											break;
										default:
											switch (num3)
											{
											case 33:
											case 35:
												return;
											}
											break;
										}
										break;
									}
								}
							}
							else if (num3 <= 79)
							{
								switch (num3)
								{
								case 44:
								case 45:
								case 46:
								case 47:
								case 48:
								case 49:
								case 54:
								case 55:
								case 56:
								case 60:
								case 61:
								case 64:
								case 68:
									return;
								case 50:
								case 51:
								case 52:
								case 53:
								case 57:
								case 58:
								case 59:
								case 62:
								case 63:
								case 65:
								case 67:
									break;
								case 66:
								{
									IndexOption indexOption = this.fillFactorOption();
									if (this.inputState.guessing == 0)
									{
										TSql80ParserBaseInternal.AddAndUpdateTokenInfo<IndexOption>(vParent, vParent.IndexOptions, indexOption);
										return;
									}
									return;
								}
								default:
									switch (num3)
									{
									case 74:
									case 75:
										return;
									default:
										if (num3 == 79)
										{
											return;
										}
										break;
									}
									break;
								}
							}
							else if (num3 == 82 || num3 == 86 || num3 == 92)
							{
								return;
							}
						}
						else if (num3 <= 173)
						{
							if (num3 <= 106)
							{
								if (num3 == 95)
								{
									return;
								}
								switch (num3)
								{
								case 99:
								case 100:
									return;
								default:
									switch (num3)
									{
									case 105:
									case 106:
										return;
									}
									break;
								}
							}
							else
							{
								switch (num3)
								{
								case 118:
								case 119:
								case 123:
								case 125:
								case 126:
								case 127:
								case 129:
								case 131:
								case 132:
								case 134:
								case 136:
								case 138:
								case 140:
								case 142:
								case 143:
								case 144:
									return;
								case 120:
								case 121:
								case 122:
								case 124:
								case 128:
								case 130:
								case 133:
								case 135:
								case 137:
								case 139:
								case 141:
									break;
								default:
									switch (num3)
									{
									case 156:
									case 159:
									case 160:
									case 161:
									case 162:
										return;
									case 157:
									case 158:
										break;
									default:
										switch (num3)
										{
										case 167:
										case 170:
										case 172:
										case 173:
											return;
										}
										break;
									}
									break;
								}
							}
						}
						else if (num3 <= 192)
						{
							if (num3 == 176)
							{
								return;
							}
							switch (num3)
							{
							case 180:
							case 181:
								return;
							default:
								switch (num3)
								{
								case 191:
								case 192:
									return;
								}
								break;
							}
						}
						else
						{
							if (num3 == 198 || num3 == 204)
							{
								return;
							}
							switch (num3)
							{
							case 219:
							case 220:
								return;
							}
						}
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					else
					{
						IndexOption indexOption = this.fillFactorOption();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<IndexOption>(vParent, vParent.IndexOptions, indexOption);
						}
						if (this.LA(1) == 232 && base.NextTokenMatchesOneOf(new string[] { "SORTED_DATA", "SORTED_DATA_REORG" }))
						{
							this.sortedDataOptions();
							return;
						}
						if (TSql80ParserInternal.tokenSet_78_.member(this.LA(1)))
						{
							return;
						}
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					break;
				}
				default:
					switch (num)
					{
					case 180:
					case 181:
						return;
					default:
						switch (num)
						{
						case 191:
						case 192:
							return;
						}
						break;
					}
					break;
				}
			}
			else
			{
				if (num == 198 || num == 204)
				{
					return;
				}
				switch (num)
				{
				case 219:
				case 220:
					return;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000BED94 File Offset: 0x000BCF94
		public void sortedDataOptions()
		{
			IToken token = this.LT(1);
			this.match(232);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "SORTED_DATA", "SORTED_DATA_REORG");
			}
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000BEDD4 File Offset: 0x000BCFD4
		public DeleteUpdateAction deleteUpdateAction(TSqlFragment vParent)
		{
			DeleteUpdateAction deleteUpdateAction = DeleteUpdateAction.NoAction;
			int num = this.LA(1);
			if (num != 19)
			{
				if (num != 142)
				{
					if (num != 232)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					IToken token = this.LT(1);
					this.match(232);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.Match(token, "NO");
					}
					IToken token2 = this.LT(1);
					this.match(232);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token2);
						TSql80ParserBaseInternal.Match(token2, "ACTION");
					}
				}
				else
				{
					this.match(142);
					int num2 = this.LA(1);
					if (num2 != 47)
					{
						if (num2 != 100)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						IToken token3 = this.LT(1);
						this.match(100);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token3);
							deleteUpdateAction = DeleteUpdateAction.SetNull;
						}
					}
					else
					{
						IToken token4 = this.LT(1);
						this.match(47);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token4);
							deleteUpdateAction = DeleteUpdateAction.SetDefault;
						}
					}
				}
			}
			else
			{
				IToken token5 = this.LT(1);
				this.match(19);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token5);
					deleteUpdateAction = DeleteUpdateAction.Cascade;
				}
			}
			return deleteUpdateAction;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000BEF44 File Offset: 0x000BD144
		public void foreignKeyConstraintCommonEnd(IndexAffectingStatement statementType, ForeignKeyConstraintDefinition vParent)
		{
			bool flag = false;
			IToken token = this.LT(1);
			this.match(127);
			SchemaObjectName schemaObjectName = this.schemaObjectThreePartName();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.ThrowSyntaxErrorIfNotCreateAlterTable(statementType, token);
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
				vParent.ReferenceTableName = schemaObjectName;
			}
			bool flag2 = false;
			if (this.LA(1) == 191 && (this.LA(2) == 232 || this.LA(2) == 233))
			{
				int num = this.mark();
				flag2 = true;
				this.inputState.guessing++;
				try
				{
					this.match(191);
					this.identifier();
				}
				catch (RecognitionException)
				{
					flag2 = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag2)
			{
				this.columnNameList(vParent, vParent.ReferencedTableColumns);
			}
			else if (!TSql80ParserInternal.tokenSet_78_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_76_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			bool flag3 = false;
			if (this.LA(1) == 105 && this.LA(2) == 48)
			{
				int num2 = this.mark();
				flag3 = true;
				this.inputState.guessing++;
				try
				{
					this.match(105);
					this.match(48);
				}
				catch (RecognitionException)
				{
					flag3 = false;
				}
				this.rewind(num2);
				this.inputState.guessing--;
			}
			if (flag3)
			{
				this.match(105);
				this.match(48);
				DeleteUpdateAction deleteUpdateAction = this.deleteUpdateAction(vParent);
				if (this.inputState.guessing == 0)
				{
					vParent.DeleteAction = deleteUpdateAction;
					flag = true;
				}
			}
			else if (!TSql80ParserInternal.tokenSet_78_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_76_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			int num3 = this.LA(1);
			if (num3 <= 92)
			{
				if (num3 <= 35)
				{
					if (num3 <= 6)
					{
						if (num3 == 1 || num3 == 6)
						{
							goto IL_082C;
						}
					}
					else
					{
						switch (num3)
						{
						case 12:
						case 13:
						case 15:
						case 17:
						case 21:
						case 22:
						case 23:
							goto IL_082C;
						case 14:
						case 16:
						case 18:
						case 19:
						case 20:
							break;
						default:
							switch (num3)
							{
							case 28:
							case 30:
								goto IL_082C;
							case 29:
								break;
							default:
								switch (num3)
								{
								case 33:
								case 35:
									goto IL_082C;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num3 <= 79)
				{
					switch (num3)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
					case 68:
						goto IL_082C;
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
					case 65:
					case 66:
					case 67:
						break;
					default:
						switch (num3)
						{
						case 74:
						case 75:
							goto IL_082C;
						default:
							if (num3 == 79)
							{
								goto IL_082C;
							}
							break;
						}
						break;
					}
				}
				else if (num3 == 82 || num3 == 86 || num3 == 92)
				{
					goto IL_082C;
				}
			}
			else if (num3 <= 173)
			{
				if (num3 <= 106)
				{
					if (num3 == 95)
					{
						goto IL_082C;
					}
					switch (num3)
					{
					case 99:
					case 100:
						goto IL_082C;
					default:
						switch (num3)
						{
						case 105:
						{
							this.match(105);
							this.match(160);
							DeleteUpdateAction deleteUpdateAction = this.deleteUpdateAction(vParent);
							if (this.inputState.guessing == 0)
							{
								vParent.UpdateAction = deleteUpdateAction;
							}
							int num4 = this.LA(1);
							if (num4 <= 92)
							{
								if (num4 <= 35)
								{
									if (num4 <= 6)
									{
										if (num4 == 1 || num4 == 6)
										{
											goto IL_082C;
										}
									}
									else
									{
										switch (num4)
										{
										case 12:
										case 13:
										case 15:
										case 17:
										case 21:
										case 22:
										case 23:
											goto IL_082C;
										case 14:
										case 16:
										case 18:
										case 19:
										case 20:
											break;
										default:
											switch (num4)
											{
											case 28:
											case 30:
												goto IL_082C;
											case 29:
												break;
											default:
												switch (num4)
												{
												case 33:
												case 35:
													goto IL_082C;
												}
												break;
											}
											break;
										}
									}
								}
								else if (num4 <= 79)
								{
									switch (num4)
									{
									case 44:
									case 45:
									case 46:
									case 47:
									case 48:
									case 49:
									case 54:
									case 55:
									case 56:
									case 60:
									case 61:
									case 64:
									case 68:
										goto IL_082C;
									case 50:
									case 51:
									case 52:
									case 53:
									case 57:
									case 58:
									case 59:
									case 62:
									case 63:
									case 65:
									case 66:
									case 67:
										break;
									default:
										switch (num4)
										{
										case 74:
										case 75:
											goto IL_082C;
										default:
											if (num4 == 79)
											{
												goto IL_082C;
											}
											break;
										}
										break;
									}
								}
								else if (num4 == 82 || num4 == 86 || num4 == 92)
								{
									goto IL_082C;
								}
							}
							else if (num4 <= 173)
							{
								if (num4 <= 106)
								{
									if (num4 == 95)
									{
										goto IL_082C;
									}
									switch (num4)
									{
									case 99:
									case 100:
										goto IL_082C;
									default:
										switch (num4)
										{
										case 105:
										{
											IToken token2 = this.LT(1);
											this.match(105);
											this.match(48);
											deleteUpdateAction = this.deleteUpdateAction(vParent);
											if (this.inputState.guessing != 0)
											{
												goto IL_082C;
											}
											if (flag)
											{
												throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token2);
											}
											vParent.DeleteAction = deleteUpdateAction;
											goto IL_082C;
										}
										case 106:
											goto IL_082C;
										}
										break;
									}
								}
								else
								{
									switch (num4)
									{
									case 118:
									case 119:
									case 123:
									case 125:
									case 126:
									case 127:
									case 129:
									case 131:
									case 132:
									case 134:
									case 136:
									case 138:
									case 140:
									case 142:
									case 143:
									case 144:
										goto IL_082C;
									case 120:
									case 121:
									case 122:
									case 124:
									case 128:
									case 130:
									case 133:
									case 135:
									case 137:
									case 139:
									case 141:
										break;
									default:
										switch (num4)
										{
										case 156:
										case 159:
										case 160:
										case 161:
										case 162:
											goto IL_082C;
										case 157:
										case 158:
											break;
										default:
											switch (num4)
											{
											case 167:
											case 170:
											case 172:
											case 173:
												goto IL_082C;
											}
											break;
										}
										break;
									}
								}
							}
							else if (num4 <= 192)
							{
								if (num4 == 176)
								{
									goto IL_082C;
								}
								switch (num4)
								{
								case 180:
								case 181:
									goto IL_082C;
								default:
									switch (num4)
									{
									case 191:
									case 192:
										goto IL_082C;
									}
									break;
								}
							}
							else
							{
								if (num4 == 198 || num4 == 204)
								{
									goto IL_082C;
								}
								switch (num4)
								{
								case 219:
								case 220:
									goto IL_082C;
								}
							}
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						case 106:
							goto IL_082C;
						}
						break;
					}
				}
				else
				{
					switch (num3)
					{
					case 118:
					case 119:
					case 123:
					case 125:
					case 126:
					case 127:
					case 129:
					case 131:
					case 132:
					case 134:
					case 136:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						goto IL_082C;
					case 120:
					case 121:
					case 122:
					case 124:
					case 128:
					case 130:
					case 133:
					case 135:
					case 137:
					case 139:
					case 141:
						break;
					default:
						switch (num3)
						{
						case 156:
						case 159:
						case 160:
						case 161:
						case 162:
							goto IL_082C;
						case 157:
						case 158:
							break;
						default:
							switch (num3)
							{
							case 167:
							case 170:
							case 172:
							case 173:
								goto IL_082C;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num3 <= 192)
			{
				if (num3 == 176)
				{
					goto IL_082C;
				}
				switch (num3)
				{
				case 180:
				case 181:
					goto IL_082C;
				default:
					switch (num3)
					{
					case 191:
					case 192:
						goto IL_082C;
					}
					break;
				}
			}
			else
			{
				if (num3 == 198 || num3 == 204)
				{
					goto IL_082C;
				}
				switch (num3)
				{
				case 219:
				case 220:
					goto IL_082C;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_082C:
			bool flag4 = this.replicationClauseOpt(statementType, vParent);
			if (this.inputState.guessing == 0)
			{
				vParent.NotForReplication = flag4;
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000BF7B8 File Offset: 0x000BD9B8
		public void foreignConstraintColumnsOpt(ForeignKeyConstraintDefinition vParent)
		{
			int num = this.LA(1);
			if (num == 127)
			{
				return;
			}
			if (num == 191)
			{
				this.columnNameList(vParent, vParent.Columns);
				return;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000BF7FC File Offset: 0x000BD9FC
		public SqlDataTypeReference sqlDataTypeWithoutNational(SchemaObjectName vName, SqlDataTypeOption vType)
		{
			SqlDataTypeReference sqlDataTypeReference = base.FragmentFactory.CreateFragment<SqlDataTypeReference>();
			sqlDataTypeReference.Name = vName;
			sqlDataTypeReference.SqlDataTypeOption = vType;
			sqlDataTypeReference.UpdateTokenInfo(vName);
			bool flag = false;
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							goto IL_035C;
						}
					}
					else
					{
						switch (num)
						{
						case 9:
						case 12:
						case 13:
						case 15:
						case 17:
						case 21:
						case 22:
						case 23:
							goto IL_035C;
						case 10:
						case 11:
						case 14:
						case 16:
						case 18:
						case 19:
						case 20:
							break;
						default:
							switch (num)
							{
							case 26:
							case 28:
							case 30:
								goto IL_035C;
							case 27:
							case 29:
								break;
							default:
								switch (num)
								{
								case 33:
								case 35:
									goto IL_035C;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
					case 67:
					case 68:
						goto IL_035C;
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
					case 65:
					case 66:
						break;
					default:
						switch (num)
						{
						case 74:
						case 75:
							goto IL_035C;
						}
						break;
					}
				}
				else if (num == 79 || num == 82 || num == 86)
				{
					goto IL_035C;
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						goto IL_035C;
					}
				}
				else
				{
					switch (num)
					{
					case 99:
					case 100:
						goto IL_035C;
					default:
						if (num == 106)
						{
							goto IL_035C;
						}
						switch (num)
						{
						case 118:
						case 119:
						case 123:
						case 125:
						case 126:
						case 127:
						case 129:
						case 131:
						case 132:
						case 134:
						case 136:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							goto IL_035C;
						}
						break;
					}
				}
			}
			else if (num <= 198)
			{
				switch (num)
				{
				case 156:
				case 159:
				case 160:
				case 161:
				case 162:
				case 167:
				case 170:
				case 171:
				case 172:
				case 173:
				case 176:
				case 180:
				case 181:
					goto IL_035C;
				case 157:
				case 158:
				case 163:
				case 164:
				case 166:
				case 168:
				case 169:
				case 174:
				case 175:
				case 177:
				case 178:
				case 179:
					break;
				case 165:
				{
					IToken token = this.LT(1);
					this.match(165);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(sqlDataTypeReference, token);
						flag = true;
						goto IL_035C;
					}
					goto IL_035C;
				}
				default:
					switch (num)
					{
					case 191:
					case 192:
						goto IL_035C;
					default:
						if (num == 198)
						{
							goto IL_035C;
						}
						break;
					}
					break;
				}
			}
			else
			{
				switch (num)
				{
				case 204:
				case 206:
					goto IL_035C;
				case 205:
					break;
				default:
					switch (num)
					{
					case 219:
					case 220:
						goto IL_035C;
					default:
						switch (num)
						{
						case 230:
						case 231:
						case 232:
						case 234:
							goto IL_035C;
						}
						break;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_035C:
			this.dataTypeParametersOpt(sqlDataTypeReference);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.ProcessNationalAndVarying(sqlDataTypeReference, null, flag);
				TSql80ParserBaseInternal.CheckSqlDataTypeParameters(sqlDataTypeReference);
			}
			return sqlDataTypeReference;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x000BFB88 File Offset: 0x000BDD88
		public UserDataTypeReference userDataType(SchemaObjectName vName)
		{
			UserDataTypeReference userDataTypeReference = base.FragmentFactory.CreateFragment<UserDataTypeReference>();
			userDataTypeReference.Name = vName;
			userDataTypeReference.UpdateTokenInfo(vName);
			this.dataTypeParametersOpt(userDataTypeReference);
			return userDataTypeReference;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000BFBB8 File Offset: 0x000BDDB8
		public SqlDataTypeReference doubleDataType()
		{
			SqlDataTypeReference sqlDataTypeReference = base.FragmentFactory.CreateFragment<SqlDataTypeReference>();
			IToken token = this.LT(1);
			this.match(53);
			IToken token2 = this.LT(1);
			this.match(174);
			if (this.inputState.guessing == 0)
			{
				base.SetNameForDoublePrecisionType(sqlDataTypeReference, token, token2);
				sqlDataTypeReference.SqlDataTypeOption = SqlDataTypeOption.Float;
			}
			bool flag = false;
			if (this.LA(1) == 191 && this.LA(2) == 221)
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(191);
					int num2 = this.LA(1);
					if (num2 != 221)
					{
						if (num2 != 232)
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						this.match(232);
					}
					else
					{
						this.integer();
					}
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag)
			{
				this.match(191);
				Literal literal = this.integer();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Literal>(sqlDataTypeReference, sqlDataTypeReference.Parameters, literal);
				}
				IToken token3 = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(sqlDataTypeReference, token3);
				}
			}
			else if (!TSql80ParserInternal.tokenSet_2_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_1_.member(this.LA(2)))
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return sqlDataTypeReference;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000BFD74 File Offset: 0x000BDF74
		public SqlDataTypeReference sqlDataTypeWithNational()
		{
			SqlDataTypeReference sqlDataTypeReference = base.FragmentFactory.CreateFragment<SqlDataTypeReference>();
			bool flag = false;
			IToken token = this.LT(1);
			this.match(96);
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				sqlDataTypeReference.SqlDataTypeOption = TSql80ParserBaseInternal.ParseDataType(identifier.Value);
				if (sqlDataTypeReference.SqlDataTypeOption == SqlDataTypeOption.None)
				{
					TSql80ParserBaseInternal.ThrowParseErrorException("SQL46003", token, TSqlParserResource.SQL46003Message, new string[] { TSqlParserResource.UserDefined });
				}
				sqlDataTypeReference.Name = base.FragmentFactory.CreateFragment<SchemaObjectName>();
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Identifier>(sqlDataTypeReference.Name, sqlDataTypeReference.Name.Identifiers, identifier);
				TSql80ParserBaseInternal.UpdateTokenInfo(sqlDataTypeReference, token);
				sqlDataTypeReference.UpdateTokenInfo(identifier);
			}
			int num = this.LA(1);
			if (num <= 86)
			{
				if (num <= 35)
				{
					if (num <= 6)
					{
						if (num == 1 || num == 6)
						{
							goto IL_03FE;
						}
					}
					else
					{
						switch (num)
						{
						case 9:
						case 12:
						case 13:
						case 15:
						case 17:
						case 21:
						case 22:
						case 23:
							goto IL_03FE;
						case 10:
						case 11:
						case 14:
						case 16:
						case 18:
						case 19:
						case 20:
							break;
						default:
							switch (num)
							{
							case 26:
							case 28:
							case 30:
								goto IL_03FE;
							case 27:
							case 29:
								break;
							default:
								switch (num)
								{
								case 33:
								case 35:
									goto IL_03FE;
								}
								break;
							}
							break;
						}
					}
				}
				else if (num <= 75)
				{
					switch (num)
					{
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 54:
					case 55:
					case 56:
					case 60:
					case 61:
					case 64:
					case 67:
					case 68:
						goto IL_03FE;
					case 50:
					case 51:
					case 52:
					case 53:
					case 57:
					case 58:
					case 59:
					case 62:
					case 63:
					case 65:
					case 66:
						break;
					default:
						switch (num)
						{
						case 74:
						case 75:
							goto IL_03FE;
						}
						break;
					}
				}
				else if (num == 79 || num == 82 || num == 86)
				{
					goto IL_03FE;
				}
			}
			else if (num <= 144)
			{
				if (num <= 95)
				{
					if (num == 92 || num == 95)
					{
						goto IL_03FE;
					}
				}
				else
				{
					switch (num)
					{
					case 99:
					case 100:
						goto IL_03FE;
					default:
						if (num == 106)
						{
							goto IL_03FE;
						}
						switch (num)
						{
						case 118:
						case 119:
						case 123:
						case 125:
						case 126:
						case 127:
						case 129:
						case 131:
						case 132:
						case 134:
						case 136:
						case 138:
						case 140:
						case 142:
						case 143:
						case 144:
							goto IL_03FE;
						}
						break;
					}
				}
			}
			else if (num <= 198)
			{
				switch (num)
				{
				case 156:
				case 159:
				case 160:
				case 161:
				case 162:
				case 167:
				case 170:
				case 171:
				case 172:
				case 173:
				case 176:
				case 180:
				case 181:
					goto IL_03FE;
				case 157:
				case 158:
				case 163:
				case 164:
				case 166:
				case 168:
				case 169:
				case 174:
				case 175:
				case 177:
				case 178:
				case 179:
					break;
				case 165:
				{
					IToken token2 = this.LT(1);
					this.match(165);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(sqlDataTypeReference, token2);
						flag = true;
						goto IL_03FE;
					}
					goto IL_03FE;
				}
				default:
					switch (num)
					{
					case 191:
					case 192:
						goto IL_03FE;
					default:
						if (num == 198)
						{
							goto IL_03FE;
						}
						break;
					}
					break;
				}
			}
			else
			{
				switch (num)
				{
				case 204:
				case 206:
					goto IL_03FE;
				case 205:
					break;
				default:
					switch (num)
					{
					case 219:
					case 220:
						goto IL_03FE;
					default:
						switch (num)
						{
						case 230:
						case 231:
						case 232:
						case 234:
							goto IL_03FE;
						}
						break;
					}
					break;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_03FE:
			this.dataTypeParametersOpt(sqlDataTypeReference);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.ProcessNationalAndVarying(sqlDataTypeReference, token, flag);
				TSql80ParserBaseInternal.CheckSqlDataTypeParameters(sqlDataTypeReference);
			}
			return sqlDataTypeReference;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000C01A4 File Offset: 0x000BE3A4
		public void dataTypeParametersOpt(ParameterizedDataTypeReference vParent)
		{
			bool flag = false;
			if (this.LA(1) == 191 && this.LA(2) == 221)
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.match(191);
					this.integer();
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			if (flag)
			{
				this.match(191);
				Literal literal = this.integer();
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Literal>(vParent, vParent.Parameters, literal);
				}
				int num2 = this.LA(1);
				if (num2 != 192)
				{
					if (num2 != 198)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(198);
					literal = this.integer();
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.AddAndUpdateTokenInfo<Literal>(vParent, vParent.Parameters, literal);
					}
				}
				IToken token = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
					return;
				}
				return;
			}
			else
			{
				if (TSql80ParserInternal.tokenSet_2_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_1_.member(this.LA(2)))
				{
					return;
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000C0320 File Offset: 0x000BE520
		public void identifierListElement(List<Identifier> vParent, int vMaxNumber, bool first)
		{
			IToken token = this.LT(1);
			this.match(200);
			Identifier identifier;
			if (this.inputState.guessing == 0 && first)
			{
				identifier = base.GetEmptyIdentifier(token);
				TSql80ParserBaseInternal.AddIdentifierToListWithCheck(vParent, identifier, vMaxNumber);
			}
			while (this.LA(1) == 200)
			{
				IToken token2 = this.LT(1);
				this.match(200);
				if (this.inputState.guessing == 0)
				{
					identifier = base.GetEmptyIdentifier(token2);
					TSql80ParserBaseInternal.AddIdentifierToListWithCheck(vParent, identifier, vMaxNumber);
				}
			}
			identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddIdentifierToListWithCheck(vParent, identifier, vMaxNumber);
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000C03C0 File Offset: 0x000BE5C0
		public BooleanExpression booleanExpressionOr(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			BooleanExpression booleanExpression = null;
			booleanExpression = this.booleanExpressionAnd(expressionFlags);
			while (this.LA(1) == 112)
			{
				this.match(112);
				BooleanExpression booleanExpression2 = this.booleanExpressionAnd(expressionFlags);
				if (this.inputState.guessing == 0)
				{
					base.AddBinaryExpression(ref booleanExpression, booleanExpression2, BooleanBinaryExpressionType.Or);
				}
			}
			return booleanExpression;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000C040C File Offset: 0x000BE60C
		public BooleanExpression booleanExpressionAnd(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			BooleanExpression booleanExpression = null;
			booleanExpression = this.booleanExpressionUnary(expressionFlags);
			while (this.LA(1) == 7)
			{
				this.match(7);
				BooleanExpression booleanExpression2 = this.booleanExpressionUnary(expressionFlags);
				if (this.inputState.guessing == 0)
				{
					base.AddBinaryExpression(ref booleanExpression, booleanExpression2, BooleanBinaryExpressionType.And);
				}
			}
			return booleanExpression;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x000C0458 File Offset: 0x000BE658
		public BooleanExpression booleanExpressionUnary(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			BooleanExpression booleanExpression = null;
			int num = this.LA(1);
			if (num <= 101)
			{
				if (num <= 41)
				{
					if (num <= 25)
					{
						if (num != 20 && num != 25)
						{
							goto IL_0220;
						}
					}
					else if (num != 31 && num != 34)
					{
						switch (num)
						{
						case 40:
						case 41:
							break;
						default:
							goto IL_0220;
						}
					}
				}
				else if (num <= 69)
				{
					if (num != 62 && num != 69)
					{
						goto IL_0220;
					}
				}
				else if (num != 81 && num != 93)
				{
					switch (num)
					{
					case 99:
					{
						IToken token = this.LT(1);
						this.match(99);
						BooleanExpression booleanExpression2 = this.booleanExpressionUnary(expressionFlags);
						if (this.inputState.guessing == 0)
						{
							BooleanNotExpression booleanNotExpression = base.FragmentFactory.CreateFragment<BooleanNotExpression>();
							booleanExpression = booleanNotExpression;
							TSql80ParserBaseInternal.UpdateTokenInfo(booleanNotExpression, token);
							booleanNotExpression.Expression = booleanExpression2;
							return booleanExpression;
						}
						return booleanExpression;
					}
					case 100:
					case 101:
						break;
					default:
						goto IL_0220;
					}
				}
			}
			else if (num <= 157)
			{
				if (num <= 136)
				{
					if (num != 133 && num != 136)
					{
						goto IL_0220;
					}
				}
				else if (num != 141 && num != 147 && num != 157)
				{
					goto IL_0220;
				}
			}
			else if (num <= 163)
			{
				if (num != 160 && num != 163)
				{
					goto IL_0220;
				}
			}
			else
			{
				switch (num)
				{
				case 191:
				case 193:
					break;
				case 192:
					goto IL_0220;
				default:
					switch (num)
					{
					case 197:
					case 199:
					case 200:
						break;
					case 198:
						goto IL_0220;
					default:
						switch (num)
						{
						case 211:
						case 221:
						case 222:
						case 223:
						case 224:
						case 225:
						case 227:
						case 228:
						case 230:
						case 231:
						case 232:
						case 233:
						case 234:
						case 235:
							break;
						case 212:
						case 213:
						case 214:
						case 215:
						case 216:
						case 217:
						case 218:
						case 219:
						case 220:
						case 226:
						case 229:
							goto IL_0220;
						default:
							goto IL_0220;
						}
						break;
					}
					break;
				}
			}
			return this.booleanExpressionPrimary(expressionFlags);
			IL_0220:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x000C069C File Offset: 0x000BE89C
		public BooleanExpression booleanExpressionPrimary(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			IToken token = null;
			bool flag = false;
			BooleanComparisonType booleanComparisonType = BooleanComparisonType.Equals;
			int num = this.LA(1);
			if (num <= 62)
			{
				if (num != 31)
				{
					if (num != 62)
					{
						goto IL_0071;
					}
					return this.existsPredicate(expressionFlags);
				}
			}
			else if (num != 69)
			{
				if (num == 157)
				{
					return this.tsEqualCall();
				}
				if (num != 160)
				{
					goto IL_0071;
				}
				return this.updateCall();
			}
			return this.fulltextPredicate();
			IL_0071:
			BooleanExpression booleanExpression;
			if (this.LA(1) == 191 && TSql80ParserInternal.tokenSet_52_.member(this.LA(2)) && base.IsNextRuleBooleanParenthesis())
			{
				booleanExpression = this.booleanExpressionParenthesis();
			}
			else
			{
				if (TSql80ParserInternal.tokenSet_16_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_79_.member(this.LA(2)))
				{
					ScalarExpression scalarExpression = this.expression(expressionFlags);
					int num2 = this.LA(1);
					if (num2 > 94)
					{
						if (num2 > 188)
						{
							if (num2 != 196)
							{
								switch (num2)
								{
								case 205:
								case 206:
								case 208:
									goto IL_016B;
								case 207:
									break;
								default:
									goto IL_043E;
								}
							}
							return this.joinPredicate(scalarExpression, booleanComparisonType, expressionFlags);
						}
						if (num2 == 99)
						{
							goto IL_0374;
						}
						if (num2 != 188)
						{
							goto IL_043E;
						}
						IL_016B:
						booleanComparisonType = this.comparisonOperator();
						int num3 = this.LA(1);
						if (num3 <= 93)
						{
							if (num3 <= 25)
							{
								if (num3 <= 8)
								{
									if (num3 != 5 && num3 != 8)
									{
										goto IL_0344;
									}
									goto IL_0334;
								}
								else if (num3 != 20 && num3 != 25)
								{
									goto IL_0344;
								}
							}
							else if (num3 <= 41)
							{
								if (num3 != 34)
								{
									switch (num3)
									{
									case 40:
									case 41:
										break;
									default:
										goto IL_0344;
									}
								}
							}
							else if (num3 != 81 && num3 != 93)
							{
								goto IL_0344;
							}
						}
						else if (num3 <= 141)
						{
							if (num3 <= 133)
							{
								switch (num3)
								{
								case 100:
								case 101:
									break;
								default:
									if (num3 != 133)
									{
										goto IL_0344;
									}
									break;
								}
							}
							else if (num3 != 136 && num3 != 141)
							{
								goto IL_0344;
							}
						}
						else if (num3 <= 163)
						{
							switch (num3)
							{
							case 145:
								goto IL_0334;
							case 146:
								goto IL_0344;
							case 147:
								break;
							default:
								if (num3 != 163)
								{
									goto IL_0344;
								}
								break;
							}
						}
						else
						{
							switch (num3)
							{
							case 191:
							case 193:
								break;
							case 192:
								goto IL_0344;
							default:
								switch (num3)
								{
								case 197:
								case 199:
								case 200:
									break;
								case 198:
									goto IL_0344;
								default:
									switch (num3)
									{
									case 211:
									case 221:
									case 222:
									case 223:
									case 224:
									case 225:
									case 227:
									case 228:
									case 230:
									case 231:
									case 232:
									case 233:
									case 234:
									case 235:
										break;
									case 212:
									case 213:
									case 214:
									case 215:
									case 216:
									case 217:
									case 218:
									case 219:
									case 220:
									case 226:
									case 229:
										goto IL_0344;
									default:
										goto IL_0344;
									}
									break;
								}
								break;
							}
						}
						return this.comparisonPredicate(scalarExpression, booleanComparisonType, expressionFlags);
						IL_0334:
						return this.subqueryComparisonPredicate(scalarExpression, booleanComparisonType, expressionFlags);
						IL_0344:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					if (num2 <= 83)
					{
						if (num2 != 14 && num2 != 83)
						{
							goto IL_043E;
						}
					}
					else
					{
						if (num2 == 89)
						{
							return this.isPredicate(scalarExpression);
						}
						if (num2 != 94)
						{
							goto IL_043E;
						}
					}
					IL_0374:
					int num4 = this.LA(1);
					if (num4 <= 83)
					{
						if (num4 == 14 || num4 == 83)
						{
							goto IL_03D1;
						}
					}
					else
					{
						if (num4 == 94)
						{
							goto IL_03D1;
						}
						if (num4 == 99)
						{
							token = this.LT(1);
							this.match(99);
							if (this.inputState.guessing == 0)
							{
								flag = true;
								goto IL_03D1;
							}
							goto IL_03D1;
						}
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
					IL_03D1:
					int num5 = this.LA(1);
					if (num5 != 14)
					{
						if (num5 != 83)
						{
							if (num5 != 94)
							{
								throw new NoViableAltException(this.LT(1), this.getFilename());
							}
							booleanExpression = this.likePredicate(scalarExpression, flag, expressionFlags);
						}
						else
						{
							booleanExpression = this.inPredicate(scalarExpression, flag, expressionFlags);
						}
					}
					else
					{
						booleanExpression = this.betweenPredicate(scalarExpression, flag, expressionFlags);
					}
					if (this.inputState.guessing == 0 && token != null)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(booleanExpression, token);
						return booleanExpression;
					}
					return booleanExpression;
					IL_043E:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return booleanExpression;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x000C0B10 File Offset: 0x000BED10
		public BooleanParenthesisExpression booleanExpressionParenthesis()
		{
			BooleanParenthesisExpression booleanParenthesisExpression = base.FragmentFactory.CreateFragment<BooleanParenthesisExpression>();
			IToken token = this.LT(1);
			this.match(191);
			BooleanExpression booleanExpression = this.booleanExpression(ExpressionFlags.None);
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(booleanParenthesisExpression, token);
				booleanParenthesisExpression.Expression = booleanExpression;
				TSql80ParserBaseInternal.UpdateTokenInfo(booleanParenthesisExpression, token2);
			}
			return booleanParenthesisExpression;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x000C0B80 File Offset: 0x000BED80
		public BooleanComparisonType comparisonOperator()
		{
			BooleanComparisonType booleanComparisonType = BooleanComparisonType.Equals;
			int num = this.LA(1);
			if (num != 188)
			{
				switch (num)
				{
				case 205:
				{
					this.match(205);
					if (this.inputState.guessing == 0)
					{
						booleanComparisonType = BooleanComparisonType.LessThan;
					}
					int num2 = this.LA(1);
					if (num2 <= 93)
					{
						if (num2 <= 25)
						{
							if (num2 <= 8)
							{
								if (num2 == 5 || num2 == 8)
								{
									return booleanComparisonType;
								}
							}
							else if (num2 == 20 || num2 == 25)
							{
								return booleanComparisonType;
							}
						}
						else if (num2 <= 41)
						{
							if (num2 == 34)
							{
								return booleanComparisonType;
							}
							switch (num2)
							{
							case 40:
							case 41:
								return booleanComparisonType;
							}
						}
						else if (num2 == 81 || num2 == 93)
						{
							return booleanComparisonType;
						}
					}
					else if (num2 <= 141)
					{
						if (num2 <= 133)
						{
							switch (num2)
							{
							case 100:
							case 101:
								return booleanComparisonType;
							default:
								if (num2 == 133)
								{
									return booleanComparisonType;
								}
								break;
							}
						}
						else if (num2 == 136 || num2 == 141)
						{
							return booleanComparisonType;
						}
					}
					else if (num2 <= 163)
					{
						switch (num2)
						{
						case 145:
						case 147:
							return booleanComparisonType;
						case 146:
							break;
						default:
							if (num2 == 163)
							{
								return booleanComparisonType;
							}
							break;
						}
					}
					else
					{
						switch (num2)
						{
						case 191:
						case 193:
							return booleanComparisonType;
						case 192:
							break;
						default:
							switch (num2)
							{
							case 197:
							case 199:
							case 200:
								return booleanComparisonType;
							case 198:
								break;
							default:
								switch (num2)
								{
								case 206:
									this.match(206);
									if (this.inputState.guessing == 0)
									{
										return BooleanComparisonType.LessThanOrEqualTo;
									}
									return booleanComparisonType;
								case 208:
									this.match(208);
									if (this.inputState.guessing == 0)
									{
										return BooleanComparisonType.NotEqualToBrackets;
									}
									return booleanComparisonType;
								case 211:
								case 221:
								case 222:
								case 223:
								case 224:
								case 225:
								case 227:
								case 228:
								case 230:
								case 231:
								case 232:
								case 233:
								case 234:
								case 235:
									return booleanComparisonType;
								}
								break;
							}
							break;
						}
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				case 206:
					this.match(206);
					if (this.inputState.guessing == 0)
					{
						return BooleanComparisonType.Equals;
					}
					return booleanComparisonType;
				case 208:
				{
					this.match(208);
					if (this.inputState.guessing == 0)
					{
						booleanComparisonType = BooleanComparisonType.GreaterThan;
					}
					int num3 = this.LA(1);
					if (num3 <= 101)
					{
						if (num3 <= 25)
						{
							if (num3 <= 8)
							{
								if (num3 == 5 || num3 == 8)
								{
									return booleanComparisonType;
								}
							}
							else if (num3 == 20 || num3 == 25)
							{
								return booleanComparisonType;
							}
						}
						else if (num3 <= 41)
						{
							if (num3 == 34)
							{
								return booleanComparisonType;
							}
							switch (num3)
							{
							case 40:
							case 41:
								return booleanComparisonType;
							}
						}
						else
						{
							if (num3 == 81 || num3 == 93)
							{
								return booleanComparisonType;
							}
							switch (num3)
							{
							case 100:
							case 101:
								return booleanComparisonType;
							}
						}
					}
					else if (num3 <= 147)
					{
						if (num3 <= 136)
						{
							if (num3 == 133 || num3 == 136)
							{
								return booleanComparisonType;
							}
						}
						else
						{
							if (num3 == 141)
							{
								return booleanComparisonType;
							}
							switch (num3)
							{
							case 145:
							case 147:
								return booleanComparisonType;
							}
						}
					}
					else if (num3 <= 193)
					{
						if (num3 == 163)
						{
							return booleanComparisonType;
						}
						switch (num3)
						{
						case 191:
						case 193:
							return booleanComparisonType;
						}
					}
					else
					{
						switch (num3)
						{
						case 197:
						case 199:
						case 200:
							return booleanComparisonType;
						case 198:
							break;
						default:
							if (num3 != 206)
							{
								switch (num3)
								{
								case 211:
								case 221:
								case 222:
								case 223:
								case 224:
								case 225:
								case 227:
								case 228:
								case 230:
								case 231:
								case 232:
								case 233:
								case 234:
								case 235:
									return booleanComparisonType;
								}
							}
							else
							{
								this.match(206);
								if (this.inputState.guessing == 0)
								{
									return BooleanComparisonType.GreaterThanOrEqualTo;
								}
								return booleanComparisonType;
							}
							break;
						}
					}
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			this.match(188);
			switch (this.LA(1))
			{
			case 205:
				this.match(205);
				if (this.inputState.guessing == 0)
				{
					return BooleanComparisonType.NotLessThan;
				}
				return booleanComparisonType;
			case 206:
				this.match(206);
				if (this.inputState.guessing == 0)
				{
					return BooleanComparisonType.NotEqualToExclamation;
				}
				return booleanComparisonType;
			case 208:
				this.match(208);
				if (this.inputState.guessing == 0)
				{
					return BooleanComparisonType.NotGreaterThan;
				}
				return booleanComparisonType;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000C10A0 File Offset: 0x000BF2A0
		public BooleanComparisonExpression comparisonPredicate(ScalarExpression vExpressionFirst, BooleanComparisonType vType, ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			BooleanComparisonExpression booleanComparisonExpression = base.FragmentFactory.CreateFragment<BooleanComparisonExpression>();
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				booleanComparisonExpression.ComparisonType = vType;
				booleanComparisonExpression.FirstExpression = vExpressionFirst;
				booleanComparisonExpression.SecondExpression = scalarExpression;
			}
			return booleanComparisonExpression;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000C10E4 File Offset: 0x000BF2E4
		public SubqueryComparisonPredicate subqueryComparisonPredicate(ScalarExpression vExpressionFirst, BooleanComparisonType vType, ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			SubqueryComparisonPredicate subqueryComparisonPredicate = base.FragmentFactory.CreateFragment<SubqueryComparisonPredicate>();
			SubqueryComparisonPredicateType subqueryComparisonPredicateType = this.subqueryComparisonPredicateType();
			ScalarSubquery scalarSubquery = this.subquery(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				subqueryComparisonPredicate.ComparisonType = vType;
				subqueryComparisonPredicate.Expression = vExpressionFirst;
				subqueryComparisonPredicate.SubqueryComparisonPredicateType = subqueryComparisonPredicateType;
				subqueryComparisonPredicate.Subquery = scalarSubquery;
			}
			return subqueryComparisonPredicate;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000C1138 File Offset: 0x000BF338
		public BooleanComparisonExpression joinPredicate(ScalarExpression vExpressionFirst, BooleanComparisonType vType, ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			BooleanComparisonExpression booleanComparisonExpression = base.FragmentFactory.CreateFragment<BooleanComparisonExpression>();
			vType = this.joinOperator();
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				booleanComparisonExpression.ComparisonType = vType;
				booleanComparisonExpression.FirstExpression = vExpressionFirst;
				booleanComparisonExpression.SecondExpression = scalarExpression;
			}
			return booleanComparisonExpression;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x000C1184 File Offset: 0x000BF384
		public BooleanIsNullExpression isPredicate(ScalarExpression vExpressionFirst)
		{
			BooleanIsNullExpression booleanIsNullExpression = base.FragmentFactory.CreateFragment<BooleanIsNullExpression>();
			this.match(89);
			bool flag = this.nullNotNull(booleanIsNullExpression);
			if (this.inputState.guessing == 0)
			{
				booleanIsNullExpression.Expression = vExpressionFirst;
				booleanIsNullExpression.IsNot = !flag;
			}
			return booleanIsNullExpression;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x000C11CC File Offset: 0x000BF3CC
		public InPredicate inPredicate(ScalarExpression vExpressionFirst, bool vNotDefined, ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			InPredicate inPredicate = base.FragmentFactory.CreateFragment<InPredicate>();
			IToken token = this.LT(1);
			this.match(83);
			if (this.inputState.guessing == 0)
			{
				if (vNotDefined)
				{
					inPredicate.NotDefined = true;
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(inPredicate, token);
				inPredicate.Expression = vExpressionFirst;
			}
			if (this.LA(1) == 191 && (this.LA(2) == 140 || this.LA(2) == 191) && base.IsNextRuleSelectParenthesis())
			{
				ScalarSubquery scalarSubquery = this.subquery(expressionFlags);
				if (this.inputState.guessing == 0)
				{
					inPredicate.Subquery = scalarSubquery;
				}
			}
			else
			{
				if (this.LA(1) != 191 || !TSql80ParserInternal.tokenSet_16_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(191);
				this.expressionList(inPredicate, inPredicate.Values);
				IToken token2 = this.LT(1);
				this.match(192);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(inPredicate, token2);
				}
			}
			return inPredicate;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000C12E4 File Offset: 0x000BF4E4
		public BooleanTernaryExpression betweenPredicate(ScalarExpression vExpressionFirst, bool vNotDefined, ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			BooleanTernaryExpression booleanTernaryExpression = base.FragmentFactory.CreateFragment<BooleanTernaryExpression>();
			IToken token = this.LT(1);
			this.match(14);
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				booleanTernaryExpression.SecondExpression = scalarExpression;
			}
			this.match(7);
			scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				booleanTernaryExpression.ThirdExpression = scalarExpression;
				if (vNotDefined)
				{
					booleanTernaryExpression.TernaryExpressionType = BooleanTernaryExpressionType.NotBetween;
				}
				else
				{
					booleanTernaryExpression.TernaryExpressionType = BooleanTernaryExpressionType.Between;
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(booleanTernaryExpression, token);
				booleanTernaryExpression.FirstExpression = vExpressionFirst;
			}
			return booleanTernaryExpression;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000C1370 File Offset: 0x000BF570
		public LikePredicate likePredicate(ScalarExpression vExpressionFirst, bool vNotDefined, ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			LikePredicate likePredicate = base.FragmentFactory.CreateFragment<LikePredicate>();
			IToken token = this.LT(1);
			this.match(94);
			if (this.inputState.guessing == 0)
			{
				if (vNotDefined)
				{
					likePredicate.NotDefined = true;
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(likePredicate, token);
				likePredicate.FirstExpression = vExpressionFirst;
			}
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				likePredicate.SecondExpression = scalarExpression;
			}
			int num = this.LA(1);
			if (num <= 95)
			{
				if (num <= 17)
				{
					if (num == 1)
					{
						return likePredicate;
					}
					switch (num)
					{
					case 6:
					case 7:
						return likePredicate;
					default:
						switch (num)
						{
						case 12:
						case 13:
						case 15:
						case 17:
							return likePredicate;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case 22:
					case 23:
						return likePredicate;
					default:
						switch (num)
						{
						case 28:
						case 29:
						case 33:
						case 35:
						case 36:
							return likePredicate;
						case 30:
						case 31:
						case 32:
						case 34:
							break;
						default:
							switch (num)
							{
							case 44:
							case 45:
							case 46:
							case 48:
							case 49:
							case 54:
							case 55:
							case 56:
							case 59:
							case 60:
							case 61:
							case 64:
							case 67:
							case 72:
							case 74:
							case 75:
							case 76:
							case 77:
							case 82:
							case 85:
							case 86:
							case 87:
							case 90:
							case 92:
							case 93:
							case 95:
								return likePredicate;
							case 58:
								this.escapeExpression(likePredicate, expressionFlags);
								return likePredicate;
							}
							break;
						}
						break;
					}
				}
			}
			else if (num <= 150)
			{
				switch (num)
				{
				case 105:
				case 106:
				case 111:
				case 112:
				case 113:
				case 114:
					return likePredicate;
				case 107:
				case 108:
				case 109:
				case 110:
					break;
				default:
					switch (num)
					{
					case 119:
					case 123:
					case 125:
					case 126:
					case 129:
					case 131:
					case 132:
					case 133:
					case 134:
					case 138:
					case 140:
					case 142:
					case 143:
					case 144:
						return likePredicate;
					case 120:
					case 121:
					case 122:
					case 124:
					case 127:
					case 128:
					case 130:
					case 135:
					case 136:
					case 137:
					case 139:
					case 141:
						break;
					default:
						if (num == 150)
						{
							return likePredicate;
						}
						break;
					}
					break;
				}
			}
			else if (num <= 198)
			{
				switch (num)
				{
				case 156:
				case 158:
				case 160:
				case 161:
				case 162:
				case 167:
				case 169:
				case 170:
				case 171:
				case 172:
				case 173:
				case 176:
				case 180:
				case 181:
					return likePredicate;
				case 157:
				case 159:
				case 163:
				case 164:
				case 165:
				case 166:
				case 168:
				case 174:
				case 175:
				case 177:
				case 178:
				case 179:
					break;
				default:
					switch (num)
					{
					case 191:
					case 192:
					case 194:
					case 198:
						return likePredicate;
					case 193:
					{
						this.match(193);
						if (this.inputState.guessing == 0)
						{
							likePredicate.OdbcEscape = true;
						}
						this.escapeExpression(likePredicate, expressionFlags);
						IToken token2 = this.LT(1);
						this.match(194);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(likePredicate, token2);
							return likePredicate;
						}
						return likePredicate;
					}
					}
					break;
				}
			}
			else
			{
				if (num == 204)
				{
					return likePredicate;
				}
				switch (num)
				{
				case 219:
				case 220:
					return likePredicate;
				}
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000C1758 File Offset: 0x000BF958
		public FullTextPredicate fulltextPredicate()
		{
			FullTextPredicate fullTextPredicate = base.FragmentFactory.CreateFragment<FullTextPredicate>();
			int num = this.LA(1);
			if (num != 31)
			{
				if (num != 69)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				IToken token = this.LT(1);
				this.match(69);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(fullTextPredicate, token);
					fullTextPredicate.FullTextFunctionType = FullTextFunctionType.FreeText;
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(31);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(fullTextPredicate, token2);
					fullTextPredicate.FullTextFunctionType = FullTextFunctionType.Contains;
				}
			}
			this.match(191);
			int num2 = this.LA(1);
			ColumnReferenceExpression columnReferenceExpression;
			if (num2 <= 191)
			{
				if (num2 != 81 && num2 != 136)
				{
					if (num2 != 191)
					{
						goto IL_0290;
					}
					this.match(191);
					bool flag = false;
					if (TSql80ParserInternal.tokenSet_35_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_80_.member(this.LA(2)))
					{
						int num3 = this.mark();
						flag = true;
						this.inputState.guessing++;
						try
						{
							this.starColumn();
						}
						catch (RecognitionException)
						{
							flag = false;
						}
						this.rewind(num3);
						this.inputState.guessing--;
					}
					if (flag)
					{
						columnReferenceExpression = this.starColumn();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(fullTextPredicate, fullTextPredicate.Columns, columnReferenceExpression);
						}
					}
					else
					{
						if (!TSql80ParserInternal.tokenSet_81_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_82_.member(this.LA(2)))
						{
							throw new NoViableAltException(this.LT(1), this.getFilename());
						}
						columnReferenceExpression = this.column();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(fullTextPredicate, fullTextPredicate.Columns, columnReferenceExpression);
						}
						while (this.LA(1) == 198)
						{
							this.match(198);
							columnReferenceExpression = this.column();
							if (this.inputState.guessing == 0)
							{
								TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(fullTextPredicate, fullTextPredicate.Columns, columnReferenceExpression);
							}
						}
					}
					this.match(192);
					goto IL_02A3;
				}
			}
			else if (num2 <= 200)
			{
				if (num2 != 195 && num2 != 200)
				{
					goto IL_0290;
				}
			}
			else if (num2 != 227)
			{
				switch (num2)
				{
				case 232:
				case 233:
					break;
				default:
					goto IL_0290;
				}
			}
			columnReferenceExpression = this.fulltextColumn();
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ColumnReferenceExpression>(fullTextPredicate, fullTextPredicate.Columns, columnReferenceExpression);
				goto IL_02A3;
			}
			goto IL_02A3;
			IL_0290:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_02A3:
			this.match(198);
			ValueExpression valueExpression = this.stringOrVariable();
			if (this.inputState.guessing == 0)
			{
				fullTextPredicate.Value = valueExpression;
			}
			int num4 = this.LA(1);
			if (num4 != 192)
			{
				if (num4 != 198)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(198);
				ValueExpression valueExpression2 = this.languageExpression();
				if (this.inputState.guessing == 0)
				{
					fullTextPredicate.LanguageTerm = valueExpression2;
				}
			}
			IToken token3 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(fullTextPredicate, token3);
			}
			return fullTextPredicate;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000C1AC0 File Offset: 0x000BFCC0
		public ExistsPredicate existsPredicate(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			ExistsPredicate existsPredicate = base.FragmentFactory.CreateFragment<ExistsPredicate>();
			this.match(62);
			ScalarSubquery scalarSubquery = this.subquery(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				existsPredicate.Subquery = scalarSubquery;
			}
			return existsPredicate;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x000C1B00 File Offset: 0x000BFD00
		public TSEqualCall tsEqualCall()
		{
			TSEqualCall tsequalCall = base.FragmentFactory.CreateFragment<TSEqualCall>();
			IToken token = this.LT(1);
			this.match(157);
			this.match(191);
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(tsequalCall, token);
				tsequalCall.FirstExpression = scalarExpression;
			}
			this.match(198);
			scalarExpression = this.expression(ExpressionFlags.None);
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				tsequalCall.SecondExpression = scalarExpression;
				TSql80ParserBaseInternal.UpdateTokenInfo(tsequalCall, token2);
			}
			return tsequalCall;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000C1BA0 File Offset: 0x000BFDA0
		public UpdateCall updateCall()
		{
			UpdateCall updateCall = base.FragmentFactory.CreateFragment<UpdateCall>();
			IToken token = this.LT(1);
			this.match(160);
			this.match(191);
			Identifier identifier = this.identifier();
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(updateCall, token);
				updateCall.Identifier = identifier;
				TSql80ParserBaseInternal.UpdateTokenInfo(updateCall, token2);
			}
			return updateCall;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000C1C18 File Offset: 0x000BFE18
		public ColumnReferenceExpression fulltextColumn()
		{
			bool flag = false;
			if (TSql80ParserInternal.tokenSet_35_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_83_.member(this.LA(2)))
			{
				int num = this.mark();
				flag = true;
				this.inputState.guessing++;
				try
				{
					this.starColumn();
				}
				catch (RecognitionException)
				{
					flag = false;
				}
				this.rewind(num);
				this.inputState.guessing--;
			}
			ColumnReferenceExpression columnReferenceExpression;
			if (flag)
			{
				columnReferenceExpression = this.starColumn();
			}
			else
			{
				if (!TSql80ParserInternal.tokenSet_81_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_83_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				columnReferenceExpression = this.column();
			}
			return columnReferenceExpression;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000C1CF4 File Offset: 0x000BFEF4
		public ColumnReferenceExpression starColumn()
		{
			ColumnReferenceExpression columnReferenceExpression = base.FragmentFactory.CreateFragment<ColumnReferenceExpression>();
			columnReferenceExpression.ColumnType = ColumnType.Wildcard;
			int num = this.LA(1);
			if (num != 195)
			{
				if (num != 200)
				{
					switch (num)
					{
					case 232:
					case 233:
						break;
					default:
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
				}
				MultiPartIdentifier multiPartIdentifier = this.multiPartIdentifier(-1);
				if (this.inputState.guessing == 0)
				{
					columnReferenceExpression.MultiPartIdentifier = multiPartIdentifier;
				}
				this.match(200);
				IToken token = this.LT(1);
				this.match(195);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(columnReferenceExpression, token);
					columnReferenceExpression.ColumnType = ColumnType.Wildcard;
				}
			}
			else
			{
				IToken token2 = this.LT(1);
				this.match(195);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(columnReferenceExpression, token2);
					columnReferenceExpression.ColumnType = ColumnType.Wildcard;
				}
			}
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.CheckSpecialColumn(columnReferenceExpression);
				TSql80ParserBaseInternal.CheckTableNameExistsForColumn(columnReferenceExpression, false);
			}
			return columnReferenceExpression;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x000C1E04 File Offset: 0x000C0004
		public BooleanComparisonType joinOperator()
		{
			BooleanComparisonType booleanComparisonType = BooleanComparisonType.LeftOuterJoin;
			int num = this.LA(1);
			if (num != 196)
			{
				if (num != 207)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(207);
				if (this.inputState.guessing == 0)
				{
					booleanComparisonType = BooleanComparisonType.RightOuterJoin;
				}
			}
			else
			{
				this.match(196);
				if (this.inputState.guessing == 0)
				{
					booleanComparisonType = BooleanComparisonType.LeftOuterJoin;
				}
			}
			return booleanComparisonType;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000C1E7C File Offset: 0x000C007C
		public SubqueryComparisonPredicateType subqueryComparisonPredicateType()
		{
			SubqueryComparisonPredicateType subqueryComparisonPredicateType = SubqueryComparisonPredicateType.None;
			int num = this.LA(1);
			if (num != 5)
			{
				if (num != 8)
				{
					if (num != 145)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					this.match(145);
					if (this.inputState.guessing == 0)
					{
						subqueryComparisonPredicateType = SubqueryComparisonPredicateType.Any;
					}
				}
				else
				{
					this.match(8);
					if (this.inputState.guessing == 0)
					{
						subqueryComparisonPredicateType = SubqueryComparisonPredicateType.Any;
					}
				}
			}
			else
			{
				this.match(5);
				if (this.inputState.guessing == 0)
				{
					subqueryComparisonPredicateType = SubqueryComparisonPredicateType.All;
				}
			}
			return subqueryComparisonPredicateType;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000C1F08 File Offset: 0x000C0108
		public void escapeExpression(LikePredicate vParent, ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			this.match(58);
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				vParent.EscapeExpression = scalarExpression;
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x000C1F3C File Offset: 0x000C013C
		public ScalarExpression expressionBinaryPri2(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			ScalarExpression scalarExpression = null;
			scalarExpression = this.expressionBinaryPri1(expressionFlags);
			for (;;)
			{
				int num = this.LA(1);
				if (num != 190)
				{
					switch (num)
					{
					case 197:
					{
						this.match(197);
						ScalarExpression scalarExpression2 = this.expressionBinaryPri1(expressionFlags);
						if (this.inputState.guessing == 0)
						{
							base.AddBinaryExpression(ref scalarExpression, scalarExpression2, BinaryExpressionType.Add);
						}
						break;
					}
					case 198:
						return scalarExpression;
					case 199:
					{
						this.match(199);
						ScalarExpression scalarExpression2 = this.expressionBinaryPri1(expressionFlags);
						if (this.inputState.guessing == 0)
						{
							base.AddBinaryExpression(ref scalarExpression, scalarExpression2, BinaryExpressionType.Subtract);
						}
						break;
					}
					default:
						switch (num)
						{
						case 209:
						{
							this.match(209);
							ScalarExpression scalarExpression2 = this.expressionBinaryPri1(expressionFlags);
							if (this.inputState.guessing == 0)
							{
								base.AddBinaryExpression(ref scalarExpression, scalarExpression2, BinaryExpressionType.BitwiseXor);
								continue;
							}
							continue;
						}
						case 210:
						{
							this.match(210);
							ScalarExpression scalarExpression2 = this.expressionBinaryPri1(expressionFlags);
							if (this.inputState.guessing == 0)
							{
								base.AddBinaryExpression(ref scalarExpression, scalarExpression2, BinaryExpressionType.BitwiseOr);
								continue;
							}
							continue;
						}
						}
						return scalarExpression;
					}
				}
				else
				{
					this.match(190);
					ScalarExpression scalarExpression2 = this.expressionBinaryPri1(expressionFlags);
					if (this.inputState.guessing == 0)
					{
						base.AddBinaryExpression(ref scalarExpression, scalarExpression2, BinaryExpressionType.BitwiseAnd);
					}
				}
			}
			return scalarExpression;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x000C208C File Offset: 0x000C028C
		public ScalarExpression expressionBinaryPri1(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			ScalarExpression scalarExpression = null;
			scalarExpression = this.expressionUnary(expressionFlags);
			for (;;)
			{
				int num = this.LA(1);
				if (num != 189)
				{
					if (num != 195)
					{
						if (num != 201)
						{
							break;
						}
						this.match(201);
						ScalarExpression scalarExpression2 = this.expressionUnary(expressionFlags);
						if (this.inputState.guessing == 0)
						{
							base.AddBinaryExpression(ref scalarExpression, scalarExpression2, BinaryExpressionType.Divide);
						}
					}
					else
					{
						this.match(195);
						ScalarExpression scalarExpression2 = this.expressionUnary(expressionFlags);
						if (this.inputState.guessing == 0)
						{
							base.AddBinaryExpression(ref scalarExpression, scalarExpression2, BinaryExpressionType.Multiply);
						}
					}
				}
				else
				{
					this.match(189);
					ScalarExpression scalarExpression2 = this.expressionUnary(expressionFlags);
					if (this.inputState.guessing == 0)
					{
						base.AddBinaryExpression(ref scalarExpression, scalarExpression2, BinaryExpressionType.Modulo);
					}
				}
			}
			return scalarExpression;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x000C2154 File Offset: 0x000C0354
		public ScalarExpression expressionUnary(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			ScalarExpression scalarExpression = null;
			UnaryExpression unaryExpression = null;
			int num = this.LA(1);
			if (num <= 101)
			{
				if (num <= 34)
				{
					if (num != 20 && num != 25 && num != 34)
					{
						goto IL_02D2;
					}
				}
				else if (num <= 81)
				{
					switch (num)
					{
					case 40:
					case 41:
						break;
					default:
						if (num != 81)
						{
							goto IL_02D2;
						}
						break;
					}
				}
				else if (num != 93)
				{
					switch (num)
					{
					case 100:
					case 101:
						break;
					default:
						goto IL_02D2;
					}
				}
			}
			else if (num <= 147)
			{
				if (num <= 136)
				{
					if (num != 133 && num != 136)
					{
						goto IL_02D2;
					}
				}
				else if (num != 141 && num != 147)
				{
					goto IL_02D2;
				}
			}
			else if (num <= 193)
			{
				if (num != 163)
				{
					switch (num)
					{
					case 191:
					case 193:
						break;
					case 192:
						goto IL_02D2;
					default:
						goto IL_02D2;
					}
				}
			}
			else
			{
				switch (num)
				{
				case 197:
				case 199:
					break;
				case 198:
					goto IL_02D2;
				case 200:
					goto IL_02C8;
				default:
					switch (num)
					{
					case 211:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_02D2;
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						goto IL_02C8;
					default:
						goto IL_02D2;
					}
					break;
				}
				int num2 = this.LA(1);
				switch (num2)
				{
				case 197:
				{
					IToken token = this.LT(1);
					this.match(197);
					if (this.inputState.guessing == 0)
					{
						unaryExpression = base.FragmentFactory.CreateFragment<UnaryExpression>();
						TSql80ParserBaseInternal.UpdateTokenInfo(unaryExpression, token);
						unaryExpression.UnaryExpressionType = UnaryExpressionType.Positive;
						goto IL_02A4;
					}
					goto IL_02A4;
				}
				case 198:
					break;
				case 199:
				{
					IToken token2 = this.LT(1);
					this.match(199);
					if (this.inputState.guessing == 0)
					{
						unaryExpression = base.FragmentFactory.CreateFragment<UnaryExpression>();
						TSql80ParserBaseInternal.UpdateTokenInfo(unaryExpression, token2);
						unaryExpression.UnaryExpressionType = UnaryExpressionType.Negative;
						goto IL_02A4;
					}
					goto IL_02A4;
				}
				default:
					if (num2 == 211)
					{
						IToken token3 = this.LT(1);
						this.match(211);
						if (this.inputState.guessing == 0)
						{
							unaryExpression = base.FragmentFactory.CreateFragment<UnaryExpression>();
							TSql80ParserBaseInternal.UpdateTokenInfo(unaryExpression, token3);
							unaryExpression.UnaryExpressionType = UnaryExpressionType.BitwiseNot;
							goto IL_02A4;
						}
						goto IL_02A4;
					}
					break;
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
				IL_02A4:
				ScalarExpression scalarExpression2 = this.expressionUnary(expressionFlags);
				if (this.inputState.guessing == 0)
				{
					scalarExpression = unaryExpression;
					unaryExpression.Expression = scalarExpression2;
					return scalarExpression;
				}
				return scalarExpression;
			}
			IL_02C8:
			return this.expressionPrimary(expressionFlags);
			IL_02D2:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x000C2448 File Offset: 0x000C0648
		public PrimaryExpression expressionPrimary(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			PrimaryExpression primaryExpression = null;
			int num = this.LA(1);
			if (num <= 101)
			{
				if (num <= 34)
				{
					if (num == 20)
					{
						primaryExpression = this.caseExpression(expressionFlags);
						goto IL_02FF;
					}
					if (num == 25)
					{
						primaryExpression = this.coalesceExpression(expressionFlags);
						goto IL_02FF;
					}
					if (num != 34)
					{
						goto IL_00FD;
					}
					primaryExpression = this.convertCall();
					goto IL_02FF;
				}
				else
				{
					switch (num)
					{
					case 40:
					case 41:
						break;
					default:
						if (num == 93)
						{
							primaryExpression = this.leftFunctionCall();
							goto IL_02FF;
						}
						if (num != 101)
						{
							goto IL_00FD;
						}
						primaryExpression = this.nullIfExpression(expressionFlags);
						goto IL_02FF;
					}
				}
			}
			else if (num <= 147)
			{
				if (num == 133)
				{
					primaryExpression = this.rightFunctionCall();
					goto IL_02FF;
				}
				if (num != 141 && num != 147)
				{
					goto IL_00FD;
				}
			}
			else if (num != 163)
			{
				if (num == 191)
				{
					primaryExpression = this.paranthesisDisambiguatorForExpressions(expressionFlags);
					goto IL_02FF;
				}
				if (num == 235)
				{
					this.odbcInitiator();
					goto IL_02FF;
				}
				goto IL_00FD;
			}
			primaryExpression = this.parameterlessCall();
			goto IL_02FF;
			IL_00FD:
			if (this.LA(1) == 193 && this.LA(2) == 232 && this.LA(1) == 193 && base.NextTokenMatches("FN", 2))
			{
				primaryExpression = this.odbcFunctionCall();
			}
			else if (TSql80ParserInternal.tokenSet_84_.member(this.LA(1)) && TSql80ParserInternal.tokenSet_85_.member(this.LA(2)))
			{
				primaryExpression = this.literal();
			}
			else if (this.LA(1) == 232 && this.LA(2) == 191 && base.NextTokenMatches("CAST") && this.LA(2) == 191)
			{
				primaryExpression = this.castCall();
			}
			else
			{
				bool flag = false;
				if (this.LA(1) == 232 && this.LA(2) == 191)
				{
					int num2 = this.mark();
					flag = true;
					this.inputState.guessing++;
					try
					{
						this.match(232);
						this.match(191);
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
					primaryExpression = this.identifierBuiltInFunctionCall();
				}
				else if ((this.LA(1) == 228 || this.LA(1) == 232 || this.LA(1) == 233) && this.LA(2) == 200 && (((this.LA(1) == 232 || this.LA(1) == 233) && this.LA(2) == 200 && this.LA(3) == 228) || this.LA(1) == 228))
				{
					primaryExpression = this.partitionFunctionCall();
				}
				else
				{
					if (!TSql80ParserInternal.tokenSet_81_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_86_.member(this.LA(2)))
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					primaryExpression = this.columnOrFunctionCall();
				}
			}
			IL_02FF:
			this.collationOpt(primaryExpression);
			return primaryExpression;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000C276C File Offset: 0x000C096C
		public CastCall castCall()
		{
			CastCall castCall = base.FragmentFactory.CreateFragment<CastCall>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(191);
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			this.match(9);
			DataTypeReference dataTypeReference = this.scalarDataType();
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.Match(token, "CAST");
				TSql80ParserBaseInternal.UpdateTokenInfo(castCall, token);
				castCall.DataType = dataTypeReference;
				castCall.Parameter = scalarExpression;
				TSql80ParserBaseInternal.UpdateTokenInfo(castCall, token2);
			}
			return castCall;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000C2808 File Offset: 0x000C0A08
		public FunctionCall identifierBuiltInFunctionCall()
		{
			FunctionCall functionCall = base.FragmentFactory.CreateFragment<FunctionCall>();
			Identifier identifier = this.nonQuotedIdentifier();
			if (this.inputState.guessing == 0)
			{
				functionCall.FunctionName = identifier;
			}
			this.match(191);
			int num = this.LA(1);
			if (num <= 93)
			{
				if (num <= 34)
				{
					if (num <= 20)
					{
						if (num != 5)
						{
							if (num != 20)
							{
								goto IL_01D3;
							}
							goto IL_01C1;
						}
					}
					else
					{
						if (num != 25 && num != 34)
						{
							goto IL_01D3;
						}
						goto IL_01C1;
					}
				}
				else if (num <= 51)
				{
					switch (num)
					{
					case 40:
					case 41:
						goto IL_01C1;
					default:
						if (num != 51)
						{
							goto IL_01D3;
						}
						break;
					}
				}
				else
				{
					if (num != 81 && num != 93)
					{
						goto IL_01D3;
					}
					goto IL_01C1;
				}
				this.identifierBuiltInFunctionCallUniqueRowFilter(functionCall);
				goto IL_01E6;
			}
			if (num <= 141)
			{
				if (num <= 133)
				{
					switch (num)
					{
					case 100:
					case 101:
						break;
					default:
						if (num != 133)
						{
							goto IL_01D3;
						}
						break;
					}
				}
				else if (num != 136 && num != 141)
				{
					goto IL_01D3;
				}
			}
			else if (num <= 163)
			{
				if (num != 147 && num != 163)
				{
					goto IL_01D3;
				}
			}
			else
			{
				switch (num)
				{
				case 191:
				case 192:
				case 193:
				case 195:
				case 197:
				case 199:
				case 200:
					break;
				case 194:
				case 196:
				case 198:
					goto IL_01D3;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_01D3;
					default:
						goto IL_01D3;
					}
					break;
				}
			}
			IL_01C1:
			this.identifierBuiltInFunctionCallDefaultParams(functionCall);
			goto IL_01E6;
			IL_01D3:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_01E6:
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(functionCall, token);
			}
			return functionCall;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000C2A24 File Offset: 0x000C0C24
		public LeftFunctionCall leftFunctionCall()
		{
			LeftFunctionCall leftFunctionCall = base.FragmentFactory.CreateFragment<LeftFunctionCall>();
			IToken token = this.LT(1);
			this.match(93);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(leftFunctionCall, token);
			}
			this.reservedBuiltInFunctionCallParameters(leftFunctionCall, leftFunctionCall.Parameters);
			return leftFunctionCall;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000C2A74 File Offset: 0x000C0C74
		public RightFunctionCall rightFunctionCall()
		{
			RightFunctionCall rightFunctionCall = base.FragmentFactory.CreateFragment<RightFunctionCall>();
			IToken token = this.LT(1);
			this.match(133);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(rightFunctionCall, token);
			}
			this.reservedBuiltInFunctionCallParameters(rightFunctionCall, rightFunctionCall.Parameters);
			return rightFunctionCall;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x000C2AC4 File Offset: 0x000C0CC4
		public PartitionFunctionCall partitionFunctionCall()
		{
			PartitionFunctionCall partitionFunctionCall = base.FragmentFactory.CreateFragment<PartitionFunctionCall>();
			int num = this.LA(1);
			Identifier identifier;
			if (num != 228)
			{
				switch (num)
				{
				case 232:
				case 233:
					identifier = this.identifier();
					if (this.inputState.guessing == 0)
					{
						partitionFunctionCall.DatabaseName = identifier;
					}
					this.match(200);
					break;
				default:
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
			}
			IToken token = this.LT(1);
			this.match(228);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(partitionFunctionCall, token);
			}
			this.match(200);
			identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				partitionFunctionCall.FunctionName = identifier;
			}
			this.match(191);
			this.expressionList(partitionFunctionCall, partitionFunctionCall.Parameters);
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(partitionFunctionCall, token2);
			}
			return partitionFunctionCall;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x000C2BD4 File Offset: 0x000C0DD4
		public PrimaryExpression columnOrFunctionCall()
		{
			PrimaryExpression primaryExpression = null;
			MultiPartIdentifier multiPartIdentifier = null;
			ColumnReferenceExpression columnReferenceExpression = null;
			int num = this.LA(1);
			if (num <= 136)
			{
				if (num != 81 && num != 136)
				{
					goto IL_0126;
				}
			}
			else
			{
				if (num != 200)
				{
					if (num == 227)
					{
						goto IL_0104;
					}
					switch (num)
					{
					case 232:
					case 233:
						break;
					default:
						goto IL_0126;
					}
				}
				multiPartIdentifier = this.multiPartIdentifier(-1);
				if (this.LA(1) == 200)
				{
					if (this.inputState.guessing == 0)
					{
						columnReferenceExpression = base.FragmentFactory.CreateFragment<ColumnReferenceExpression>();
					}
					this.match(200);
					this.specialColumn(columnReferenceExpression);
					goto IL_0139;
				}
				if (this.LA(1) == 191 && TSql80ParserInternal.tokenSet_87_.member(this.LA(2)))
				{
					primaryExpression = this.userFunctionCall(multiPartIdentifier);
					goto IL_0139;
				}
				if (!TSql80ParserInternal.tokenSet_85_.member(this.LA(1)) || !TSql80ParserInternal.tokenSet_88_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				goto IL_0139;
			}
			IL_0104:
			if (this.inputState.guessing == 0)
			{
				columnReferenceExpression = base.FragmentFactory.CreateFragment<ColumnReferenceExpression>();
			}
			this.specialColumn(columnReferenceExpression);
			goto IL_0139;
			IL_0126:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0139:
			if (this.inputState.guessing == 0 && (primaryExpression == null || primaryExpression is ColumnReferenceExpression))
			{
				if (columnReferenceExpression == null)
				{
					columnReferenceExpression = base.FragmentFactory.CreateFragment<ColumnReferenceExpression>();
				}
				columnReferenceExpression.MultiPartIdentifier = multiPartIdentifier;
				TSql80ParserBaseInternal.CheckSpecialColumn(columnReferenceExpression);
				TSql80ParserBaseInternal.CheckTableNameExistsForColumn(columnReferenceExpression, false);
				primaryExpression = columnReferenceExpression;
			}
			return primaryExpression;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000C2D58 File Offset: 0x000C0F58
		public NullIfExpression nullIfExpression(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			NullIfExpression nullIfExpression = base.FragmentFactory.CreateFragment<NullIfExpression>();
			IToken token = this.LT(1);
			this.match(101);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(nullIfExpression, token);
			}
			this.match(191);
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				nullIfExpression.FirstExpression = scalarExpression;
			}
			this.match(198);
			scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				nullIfExpression.SecondExpression = scalarExpression;
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(nullIfExpression, token2);
			}
			return nullIfExpression;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x000C2E10 File Offset: 0x000C1010
		public CoalesceExpression coalesceExpression(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			CoalesceExpression coalesceExpression = base.FragmentFactory.CreateFragment<CoalesceExpression>();
			IToken token = this.LT(1);
			this.match(25);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(coalesceExpression, token);
			}
			this.match(191);
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(coalesceExpression, coalesceExpression.Expressions, scalarExpression);
			}
			int num = 0;
			while (this.LA(1) == 198)
			{
				this.match(198);
				scalarExpression = this.expression(expressionFlags);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(coalesceExpression, coalesceExpression.Expressions, scalarExpression);
				}
				num++;
			}
			if (num < 1)
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(coalesceExpression, token2);
			}
			return coalesceExpression;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000C2F08 File Offset: 0x000C1108
		public CaseExpression caseExpression(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			IToken token = this.LT(1);
			this.match(20);
			int num = this.LA(1);
			CaseExpression caseExpression;
			if (num <= 133)
			{
				if (num <= 41)
				{
					if (num <= 25)
					{
						if (num != 20 && num != 25)
						{
							goto IL_01DA;
						}
					}
					else if (num != 34)
					{
						switch (num)
						{
						case 40:
						case 41:
							break;
						default:
							goto IL_01DA;
						}
					}
				}
				else if (num <= 93)
				{
					if (num != 81 && num != 93)
					{
						goto IL_01DA;
					}
				}
				else
				{
					switch (num)
					{
					case 100:
					case 101:
						break;
					default:
						if (num != 133)
						{
							goto IL_01DA;
						}
						break;
					}
				}
			}
			else if (num <= 163)
			{
				if (num <= 141)
				{
					if (num != 136 && num != 141)
					{
						goto IL_01DA;
					}
				}
				else if (num != 147 && num != 163)
				{
					goto IL_01DA;
				}
			}
			else if (num <= 193)
			{
				if (num == 168)
				{
					caseExpression = this.searchedCaseExpression(expressionFlags);
					goto IL_01ED;
				}
				switch (num)
				{
				case 191:
				case 193:
					break;
				case 192:
					goto IL_01DA;
				default:
					goto IL_01DA;
				}
			}
			else
			{
				switch (num)
				{
				case 197:
				case 199:
				case 200:
					break;
				case 198:
					goto IL_01DA;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_01DA;
					default:
						goto IL_01DA;
					}
					break;
				}
			}
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			caseExpression = this.simpleCaseExpression(scalarExpression, expressionFlags);
			goto IL_01ED;
			IL_01DA:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_01ED:
			switch (this.LA(1))
			{
			case 55:
				this.match(55);
				scalarExpression = this.expression(expressionFlags);
				if (this.inputState.guessing == 0)
				{
					caseExpression.ElseExpression = scalarExpression;
				}
				break;
			case 56:
				break;
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			IToken token2 = this.LT(1);
			this.match(56);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(caseExpression, token);
				TSql80ParserBaseInternal.UpdateTokenInfo(caseExpression, token2);
			}
			return caseExpression;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000C3184 File Offset: 0x000C1384
		public ConvertCall convertCall()
		{
			ConvertCall convertCall = base.FragmentFactory.CreateFragment<ConvertCall>();
			IToken token = this.LT(1);
			this.match(34);
			this.match(191);
			DataTypeReference dataTypeReference = this.scalarDataType();
			this.match(198);
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(convertCall, token);
				convertCall.DataType = dataTypeReference;
				convertCall.Parameter = scalarExpression;
			}
			int num = this.LA(1);
			if (num != 192)
			{
				if (num != 198)
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				this.match(198);
				scalarExpression = this.expression(ExpressionFlags.None);
				if (this.inputState.guessing == 0)
				{
					convertCall.Style = scalarExpression;
				}
			}
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(convertCall, token2);
			}
			return convertCall;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000C327C File Offset: 0x000C147C
		public ParameterlessCall parameterlessCall()
		{
			ParameterlessCall parameterlessCall = base.FragmentFactory.CreateFragment<ParameterlessCall>();
			int num = this.LA(1);
			if (num <= 141)
			{
				switch (num)
				{
				case 40:
				{
					IToken token = this.LT(1);
					this.match(40);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(parameterlessCall, token);
						parameterlessCall.ParameterlessCallType = ParameterlessCallType.CurrentTimestamp;
						return parameterlessCall;
					}
					return parameterlessCall;
				}
				case 41:
				{
					IToken token2 = this.LT(1);
					this.match(41);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(parameterlessCall, token2);
						parameterlessCall.ParameterlessCallType = ParameterlessCallType.CurrentUser;
						return parameterlessCall;
					}
					return parameterlessCall;
				}
				default:
					if (num == 141)
					{
						IToken token3 = this.LT(1);
						this.match(141);
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.UpdateTokenInfo(parameterlessCall, token3);
							parameterlessCall.ParameterlessCallType = ParameterlessCallType.SessionUser;
							return parameterlessCall;
						}
						return parameterlessCall;
					}
					break;
				}
			}
			else if (num != 147)
			{
				if (num == 163)
				{
					IToken token4 = this.LT(1);
					this.match(163);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(parameterlessCall, token4);
						parameterlessCall.ParameterlessCallType = ParameterlessCallType.User;
						return parameterlessCall;
					}
					return parameterlessCall;
				}
			}
			else
			{
				IToken token5 = this.LT(1);
				this.match(147);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(parameterlessCall, token5);
					parameterlessCall.ParameterlessCallType = ParameterlessCallType.SystemUser;
					return parameterlessCall;
				}
				return parameterlessCall;
			}
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000C3400 File Offset: 0x000C1600
		public PrimaryExpression paranthesisDisambiguatorForExpressions(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			PrimaryExpression primaryExpression;
			if (this.LA(1) == 191 && (this.LA(2) == 140 || this.LA(2) == 191) && base.IsNextRuleSelectParenthesis())
			{
				primaryExpression = this.subquery(expressionFlags);
			}
			else
			{
				if (this.LA(1) != 191 || !TSql80ParserInternal.tokenSet_16_.member(this.LA(2)))
				{
					throw new NoViableAltException(this.LT(1), this.getFilename());
				}
				primaryExpression = this.expressionParenthesis(expressionFlags);
			}
			return primaryExpression;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000C348C File Offset: 0x000C168C
		public ParenthesisExpression expressionParenthesis(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			ParenthesisExpression parenthesisExpression = base.FragmentFactory.CreateFragment<ParenthesisExpression>();
			IToken token = this.LT(1);
			this.match(191);
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			IToken token2 = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(parenthesisExpression, token);
				parenthesisExpression.Expression = scalarExpression;
				TSql80ParserBaseInternal.UpdateTokenInfo(parenthesisExpression, token2);
			}
			return parenthesisExpression;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000C34FC File Offset: 0x000C16FC
		public FunctionCall basicFunctionCall()
		{
			FunctionCall functionCall = base.FragmentFactory.CreateFragment<FunctionCall>();
			Identifier identifier = this.identifier();
			if (this.inputState.guessing == 0)
			{
				functionCall.FunctionName = identifier;
			}
			this.parenthesizedOptExpressionWithDefaultList(functionCall, functionCall.Parameters);
			return functionCall;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000C3540 File Offset: 0x000C1740
		public void identifierBuiltInFunctionCallDefaultParams(FunctionCall vParent)
		{
			int num = this.LA(1);
			if (num <= 101)
			{
				if (num <= 34)
				{
					if (num != 20 && num != 25 && num != 34)
					{
						goto IL_0197;
					}
				}
				else if (num <= 81)
				{
					switch (num)
					{
					case 40:
					case 41:
						break;
					default:
						if (num != 81)
						{
							goto IL_0197;
						}
						break;
					}
				}
				else if (num != 93)
				{
					switch (num)
					{
					case 100:
					case 101:
						break;
					default:
						goto IL_0197;
					}
				}
			}
			else if (num <= 141)
			{
				if (num != 133 && num != 136 && num != 141)
				{
					goto IL_0197;
				}
			}
			else
			{
				if (num > 163)
				{
					switch (num)
					{
					case 191:
					case 193:
					case 197:
					case 199:
					case 200:
						goto IL_0167;
					case 192:
						break;
					case 194:
					case 196:
					case 198:
						goto IL_0197;
					case 195:
					{
						ColumnReferenceExpression columnReferenceExpression = this.starColumnReferenceExpression();
						if (this.inputState.guessing == 0)
						{
							TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(vParent, vParent.Parameters, columnReferenceExpression);
							return;
						}
						break;
					}
					default:
						switch (num)
						{
						case 211:
						case 221:
						case 222:
						case 223:
						case 224:
						case 225:
						case 227:
						case 228:
						case 230:
						case 231:
						case 232:
						case 233:
						case 234:
						case 235:
							goto IL_0167;
						case 212:
						case 213:
						case 214:
						case 215:
						case 216:
						case 217:
						case 218:
						case 219:
						case 220:
						case 226:
						case 229:
							goto IL_0197;
						default:
							goto IL_0197;
						}
						break;
					}
					return;
				}
				if (num != 147 && num != 163)
				{
					goto IL_0197;
				}
			}
			IL_0167:
			this.expressionList(vParent, vParent.Parameters);
			return;
			IL_0197:
			throw new NoViableAltException(this.LT(1), this.getFilename());
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000C36F8 File Offset: 0x000C18F8
		public void identifierBuiltInFunctionCallUniqueRowFilter(FunctionCall vParent)
		{
			UniqueRowFilter uniqueRowFilter = this.uniqueRowFilter();
			ScalarExpression scalarExpression = this.expression(ExpressionFlags.None);
			if (this.inputState.guessing == 0)
			{
				vParent.UniqueRowFilter = uniqueRowFilter;
				TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ScalarExpression>(vParent, vParent.Parameters, scalarExpression);
			}
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000C3738 File Offset: 0x000C1938
		public void reservedBuiltInFunctionCallParameters(TSqlFragment vParent, IList<ScalarExpression> parameters)
		{
			this.match(191);
			int num = this.LA(1);
			if (num <= 101)
			{
				if (num <= 34)
				{
					if (num != 20 && num != 25 && num != 34)
					{
						goto IL_017E;
					}
				}
				else if (num <= 81)
				{
					switch (num)
					{
					case 40:
					case 41:
						break;
					default:
						if (num != 81)
						{
							goto IL_017E;
						}
						break;
					}
				}
				else if (num != 93)
				{
					switch (num)
					{
					case 100:
					case 101:
						break;
					default:
						goto IL_017E;
					}
				}
			}
			else if (num <= 141)
			{
				if (num != 133 && num != 136 && num != 141)
				{
					goto IL_017E;
				}
			}
			else if (num <= 163)
			{
				if (num != 147 && num != 163)
				{
					goto IL_017E;
				}
			}
			else
			{
				switch (num)
				{
				case 191:
				case 193:
				case 197:
				case 199:
				case 200:
					break;
				case 192:
					goto IL_0191;
				case 194:
				case 195:
				case 196:
				case 198:
					goto IL_017E;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_017E;
					default:
						goto IL_017E;
					}
					break;
				}
			}
			this.expressionList(vParent, parameters);
			goto IL_0191;
			IL_017E:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0191:
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(vParent, token);
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x000C3900 File Offset: 0x000C1B00
		public SimpleWhenClause simpleWhenClause(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			SimpleWhenClause simpleWhenClause = base.FragmentFactory.CreateFragment<SimpleWhenClause>();
			IToken token = this.LT(1);
			this.match(168);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(simpleWhenClause, token);
			}
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				simpleWhenClause.WhenExpression = scalarExpression;
			}
			this.match(150);
			scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				simpleWhenClause.ThenExpression = scalarExpression;
			}
			return simpleWhenClause;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000C3988 File Offset: 0x000C1B88
		public SearchedWhenClause searchedWhenClause(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			SearchedWhenClause searchedWhenClause = base.FragmentFactory.CreateFragment<SearchedWhenClause>();
			IToken token = this.LT(1);
			this.match(168);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(searchedWhenClause, token);
			}
			BooleanExpression booleanExpression = this.booleanExpression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				searchedWhenClause.WhenExpression = booleanExpression;
			}
			this.match(150);
			ScalarExpression scalarExpression = this.expression(expressionFlags);
			if (this.inputState.guessing == 0)
			{
				searchedWhenClause.ThenExpression = scalarExpression;
			}
			return searchedWhenClause;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x000C3A14 File Offset: 0x000C1C14
		public SimpleCaseExpression simpleCaseExpression(ScalarExpression inputExpression, ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			SimpleCaseExpression simpleCaseExpression = base.FragmentFactory.CreateFragment<SimpleCaseExpression>();
			simpleCaseExpression.InputExpression = inputExpression;
			int num = 0;
			while (this.LA(1) == 168)
			{
				SimpleWhenClause simpleWhenClause = this.simpleWhenClause(expressionFlags);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SimpleWhenClause>(simpleCaseExpression, simpleCaseExpression.WhenClauses, simpleWhenClause);
				}
				num++;
			}
			if (num < 1)
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return simpleCaseExpression;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x000C3A88 File Offset: 0x000C1C88
		public SearchedCaseExpression searchedCaseExpression(ExpressionFlags expressionFlags = ExpressionFlags.None)
		{
			SearchedCaseExpression searchedCaseExpression = base.FragmentFactory.CreateFragment<SearchedCaseExpression>();
			int num = 0;
			while (this.LA(1) == 168)
			{
				SearchedWhenClause searchedWhenClause = this.searchedWhenClause(expressionFlags);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.AddAndUpdateTokenInfo<SearchedWhenClause>(searchedCaseExpression, searchedCaseExpression.WhenClauses, searchedWhenClause);
				}
				num++;
			}
			if (num < 1)
			{
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			return searchedCaseExpression;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000C3AF4 File Offset: 0x000C1CF4
		public void specialColumn(ColumnReferenceExpression vResult)
		{
			int num = this.LA(1);
			if (num != 81)
			{
				if (num != 136)
				{
					if (num != 227)
					{
						throw new NoViableAltException(this.LT(1), this.getFilename());
					}
					IToken token = this.LT(1);
					this.match(227);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(vResult, token);
						vResult.ColumnType = PseudoColumnHelper.Instance.ParseOption(token, SqlVersionFlags.TSql80);
						return;
					}
				}
				else
				{
					IToken token2 = this.LT(1);
					this.match(136);
					if (this.inputState.guessing == 0)
					{
						TSql80ParserBaseInternal.UpdateTokenInfo(vResult, token2);
						vResult.ColumnType = ColumnType.RowGuidCol;
						return;
					}
				}
			}
			else
			{
				IToken token3 = this.LT(1);
				this.match(81);
				if (this.inputState.guessing == 0)
				{
					TSql80ParserBaseInternal.UpdateTokenInfo(vResult, token3);
					vResult.ColumnType = ColumnType.IdentityCol;
					return;
				}
			}
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000C3BD4 File Offset: 0x000C1DD4
		public FunctionCall userFunctionCall(MultiPartIdentifier vIdentifiers)
		{
			FunctionCall functionCall = base.FragmentFactory.CreateFragment<FunctionCall>();
			this.match(191);
			int num = this.LA(1);
			if (num <= 81)
			{
				if (num <= 34)
				{
					if (num <= 20)
					{
						if (num != 5)
						{
							if (num != 20)
							{
								goto IL_0355;
							}
							goto IL_01AE;
						}
					}
					else
					{
						if (num != 25 && num != 34)
						{
							goto IL_0355;
						}
						goto IL_01AE;
					}
				}
				else if (num <= 47)
				{
					switch (num)
					{
					case 40:
					case 41:
						goto IL_01AE;
					default:
						if (num != 47)
						{
							goto IL_0355;
						}
						goto IL_01AE;
					}
				}
				else if (num != 51)
				{
					if (num != 81)
					{
						goto IL_0355;
					}
					goto IL_01AE;
				}
				this.identifierBuiltInFunctionCallUniqueRowFilter(functionCall);
				goto IL_0368;
			}
			if (num <= 136)
			{
				if (num <= 101)
				{
					if (num != 93)
					{
						switch (num)
						{
						case 100:
						case 101:
							break;
						default:
							goto IL_0355;
						}
					}
				}
				else if (num != 133 && num != 136)
				{
					goto IL_0355;
				}
			}
			else if (num <= 147)
			{
				if (num != 141 && num != 147)
				{
					goto IL_0355;
				}
			}
			else if (num != 163)
			{
				switch (num)
				{
				case 191:
				case 192:
				case 193:
				case 197:
				case 199:
				case 200:
					break;
				case 194:
				case 195:
				case 196:
				case 198:
					goto IL_0355;
				default:
					switch (num)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_0355;
					default:
						goto IL_0355;
					}
					break;
				}
			}
			IL_01AE:
			int num2 = this.LA(1);
			if (num2 <= 93)
			{
				if (num2 <= 34)
				{
					if (num2 != 20 && num2 != 25 && num2 != 34)
					{
						goto IL_0339;
					}
				}
				else if (num2 <= 47)
				{
					switch (num2)
					{
					case 40:
					case 41:
						break;
					default:
						if (num2 != 47)
						{
							goto IL_0339;
						}
						break;
					}
				}
				else if (num2 != 81 && num2 != 93)
				{
					goto IL_0339;
				}
			}
			else if (num2 <= 141)
			{
				if (num2 <= 133)
				{
					switch (num2)
					{
					case 100:
					case 101:
						break;
					default:
						if (num2 != 133)
						{
							goto IL_0339;
						}
						break;
					}
				}
				else if (num2 != 136 && num2 != 141)
				{
					goto IL_0339;
				}
			}
			else if (num2 <= 163)
			{
				if (num2 != 147 && num2 != 163)
				{
					goto IL_0339;
				}
			}
			else
			{
				switch (num2)
				{
				case 191:
				case 193:
				case 197:
				case 199:
				case 200:
					break;
				case 192:
					goto IL_0368;
				case 194:
				case 195:
				case 196:
				case 198:
					goto IL_0339;
				default:
					switch (num2)
					{
					case 211:
					case 221:
					case 222:
					case 223:
					case 224:
					case 225:
					case 227:
					case 228:
					case 230:
					case 231:
					case 232:
					case 233:
					case 234:
					case 235:
						break;
					case 212:
					case 213:
					case 214:
					case 215:
					case 216:
					case 217:
					case 218:
					case 219:
					case 220:
					case 226:
					case 229:
						goto IL_0339;
					default:
						goto IL_0339;
					}
					break;
				}
			}
			this.expressionWithDefaultList(functionCall, functionCall.Parameters);
			goto IL_0368;
			IL_0339:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0355:
			throw new NoViableAltException(this.LT(1), this.getFilename());
			IL_0368:
			IToken token = this.LT(1);
			this.match(192);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(functionCall, token);
				base.PutIdentifiersIntoFunctionCall(functionCall, vIdentifiers);
			}
			return functionCall;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000C3F7C File Offset: 0x000C217C
		public DiskStatementOption diskStatementOption()
		{
			DiskStatementOption diskStatementOption = base.FragmentFactory.CreateFragment<DiskStatementOption>();
			IToken token = this.LT(1);
			this.match(232);
			this.match(206);
			IdentifierOrValueExpression identifierOrValueExpression = this.identifierOrValueExpression();
			if (this.inputState.guessing == 0)
			{
				diskStatementOption.OptionKind = DiskStatementOptionsHelper.Instance.ParseOption(token);
				diskStatementOption.Value = identifierOrValueExpression;
			}
			return diskStatementOption;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000C3FE4 File Offset: 0x000C21E4
		public IdentifierOrValueExpression identifierOrValueExpression()
		{
			IdentifierOrValueExpression identifierOrValueExpression = base.FragmentFactory.CreateFragment<IdentifierOrValueExpression>();
			int num = this.LA(1);
			if (num != 100 && num != 193)
			{
				switch (num)
				{
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 230:
				case 231:
				case 234:
					goto IL_0084;
				case 232:
				case 233:
				{
					Identifier identifier = this.identifier();
					if (this.inputState.guessing == 0)
					{
						identifierOrValueExpression.Identifier = identifier;
						return identifierOrValueExpression;
					}
					return identifierOrValueExpression;
				}
				}
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			IL_0084:
			ValueExpression valueExpression = this.literal();
			if (this.inputState.guessing == 0)
			{
				identifierOrValueExpression.ValueExpression = valueExpression;
			}
			return identifierOrValueExpression;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000C40A8 File Offset: 0x000C22A8
		public MoneyLiteral moneyLiteral()
		{
			MoneyLiteral moneyLiteral = base.FragmentFactory.CreateFragment<MoneyLiteral>();
			IToken token = this.LT(1);
			this.match(225);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(moneyLiteral, token);
				moneyLiteral.Value = token.getText();
			}
			return moneyLiteral;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000C40F8 File Offset: 0x000C22F8
		public ValueExpression globalVariableOrVariableReference()
		{
			ValueExpression valueExpression = null;
			IToken token = this.LT(1);
			this.match(234);
			if (this.inputState.guessing == 0)
			{
				if (token.getText().StartsWith("@@", 4))
				{
					GlobalVariableExpression globalVariableExpression = base.FragmentFactory.CreateFragment<GlobalVariableExpression>();
					globalVariableExpression.Name = token.getText();
					valueExpression = globalVariableExpression;
				}
				else
				{
					VariableReference variableReference = base.FragmentFactory.CreateFragment<VariableReference>();
					variableReference.Name = token.getText();
					valueExpression = variableReference;
				}
				TSql80ParserBaseInternal.UpdateTokenInfo(valueExpression, token);
			}
			return valueExpression;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000C417C File Offset: 0x000C237C
		public OdbcLiteral odbcLiteral()
		{
			OdbcLiteral odbcLiteral = base.FragmentFactory.CreateFragment<OdbcLiteral>();
			IToken token = this.LT(1);
			this.match(193);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(odbcLiteral, token);
			}
			IToken token2 = this.LT(1);
			this.match(232);
			switch (this.LA(1))
			{
			case 230:
			{
				IToken token3 = this.LT(1);
				this.match(230);
				if (this.inputState.guessing == 0)
				{
					odbcLiteral.OdbcLiteralType = TSql80ParserBaseInternal.ParseOdbcLiteralType(token2);
					TSql80ParserBaseInternal.UpdateTokenInfo(odbcLiteral, token3);
					odbcLiteral.Value = TSql80ParserBaseInternal.DecodeAsciiStringLiteral(token3.getText());
				}
				break;
			}
			case 231:
			{
				IToken token4 = this.LT(1);
				this.match(231);
				if (this.inputState.guessing == 0)
				{
					odbcLiteral.OdbcLiteralType = TSql80ParserBaseInternal.ParseOdbcLiteralType(token2);
					odbcLiteral.IsNational = true;
					TSql80ParserBaseInternal.UpdateTokenInfo(odbcLiteral, token4);
					odbcLiteral.Value = TSql80ParserBaseInternal.DecodeUnicodeStringLiteral(token4.getText());
				}
				break;
			}
			default:
				throw new NoViableAltException(this.LT(1), this.getFilename());
			}
			IToken token5 = this.LT(1);
			this.match(194);
			if (this.inputState.guessing == 0)
			{
				TSql80ParserBaseInternal.UpdateTokenInfo(odbcLiteral, token5);
			}
			return odbcLiteral;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000C42D4 File Offset: 0x000C24D4
		private void initializeFactory()
		{
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000C42D8 File Offset: 0x000C24D8
		private static long[] mk_tokenSet_0_()
		{
			long[] array = new long[8];
			array[0] = 3585973655481528898L;
			array[1] = -1675334557835686887L;
			array[2] = -9209513072622709414L;
			array[3] = 6322594533441L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000C4330 File Offset: 0x000C2530
		private static long[] mk_tokenSet_1_()
		{
			long[] array = new long[8];
			array[0] = 9214359473050810082L;
			array[1] = -1454733885903487047L;
			array[2] = -1139059068170799109L;
			array[3] = 17437434049535L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000C4388 File Offset: 0x000C2588
		private static long[] mk_tokenSet_2_()
		{
			long[] array = new long[8];
			array[0] = 3585973655481528898L;
			array[1] = -1675334557835686887L;
			array[2] = -9209513210061662886L;
			array[3] = 6322594533441L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000C43E0 File Offset: 0x000C25E0
		private static long[] mk_tokenSet_3_()
		{
			long[] array = new long[8];
			array[0] = 3477746525793333312L;
			array[1] = 7530022977430359041L;
			array[2] = -9209522008302168998L;
			array[3] = 268435456L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000C4468 File Offset: 0x000C2668
		private static long[] mk_tokenSet_4_()
		{
			return new long[] { 140737488355328L, 216172782113784320L, 275012127232L, 0L, 0L, 0L };
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000C4488 File Offset: 0x000C2688
		private static long[] mk_tokenSet_5_()
		{
			long[] array = new long[8];
			array[0] = 3477746525793333314L;
			array[1] = 7530022977430359041L;
			array[2] = -9209522008302168998L;
			array[3] = 7696984047872L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000C44E0 File Offset: 0x000C26E0
		private static long[] mk_tokenSet_6_()
		{
			long[] array = new long[8];
			array[0] = 8240455983232626786L;
			array[1] = -1474861547081199967L;
			array[2] = -9209512900031547398L;
			array[3] = 35029619577258L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000C4568 File Offset: 0x000C2768
		private static long[] mk_tokenSet_7_()
		{
			return new long[] { 16777216L, 17180917760L, 2147483648L, 0L, 0L, 0L };
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000C4588 File Offset: 0x000C2788
		private static long[] mk_tokenSet_8_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 2313372481617920L;
			array[2] = 0L;
			array[3] = 7696581394688L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000C45D0 File Offset: 0x000C27D0
		private static long[] mk_tokenSet_9_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 61572651155456L;
			array[2] = 0L;
			array[3] = 7696581394688L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x000C4618 File Offset: 0x000C2818
		private static long[] mk_tokenSet_10_()
		{
			long[] array = new long[8];
			array[0] = 3585832916850225218L;
			array[1] = 7530022977430359041L;
			array[2] = -9209522008302168998L;
			array[3] = 402657280L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x000C466C File Offset: 0x000C286C
		private static long[] mk_tokenSet_11_()
		{
			long[] array = new long[8];
			array[0] = 8348542374289518690L;
			array[1] = -1474861547081199967L;
			array[2] = -9209512900031547398L;
			array[3] = 17437433532842L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000C46C4 File Offset: 0x000C28C4
		private static long[] mk_tokenSet_12_()
		{
			long[] array = new long[8];
			array[0] = 9011597301252608L;
			array[1] = 4294967296L;
			array[2] = 0L;
			array[3] = 3298534883328L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x000C4714 File Offset: 0x000C2914
		private static long[] mk_tokenSet_13_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 618475290880L;
			array[2] = 0L;
			array[3] = 8537858113666L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000C475C File Offset: 0x000C295C
		private static long[] mk_tokenSet_14_()
		{
			long[] array = new long[8];
			array[0] = 8796093022208L;
			array[1] = 0L;
			array[2] = 100663296L;
			array[3] = 1099511627776L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000C47A8 File Offset: 0x000C29A8
		private static long[] mk_tokenSet_15_()
		{
			long[] array = new long[8];
			array[0] = 3585832916850225218L;
			array[1] = 7530022977430359041L;
			array[2] = -9209513212209146790L;
			array[3] = 402657280L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000C47FC File Offset: 0x000C29FC
		private static long[] mk_tokenSet_16_()
		{
			long[] array = new long[8];
			array[0] = 3315749355520L;
			array[1] = 206695432192L;
			array[2] = -9223372002494504672L;
			array[3] = 17437030875554L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000C4854 File Offset: 0x000C2A54
		private static long[] mk_tokenSet_17_()
		{
			long[] array = new long[8];
			array[0] = 3585836232666689602L;
			array[1] = 7530023184125791233L;
			array[2] = -2291991846789188230L;
			array[3] = 17437433926570L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000C48AC File Offset: 0x000C2AAC
		private static long[] mk_tokenSet_18_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 68719476736L;
			array[2] = 0L;
			array[3] = 8537858113667L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x000C48F4 File Offset: 0x000C2AF4
		private static long[] mk_tokenSet_19_()
		{
			long[] array = new long[8];
			array[0] = 3585832916850225218L;
			array[1] = 7530022977430359041L;
			array[2] = -9209513212209146790L;
			array[3] = 402657344L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x000C4948 File Offset: 0x000C2B48
		private static long[] mk_tokenSet_20_()
		{
			long[] array = new long[8];
			array[0] = 3585832916917334082L;
			array[1] = 7530022977430359049L;
			array[2] = -9209522008302168998L;
			array[3] = 402657280L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x000C499C File Offset: 0x000C2B9C
		private static long[] mk_tokenSet_21_()
		{
			long[] array = new long[8];
			array[0] = 3585832916850225218L;
			array[1] = 7530022977430359041L;
			array[2] = -9209522008302168998L;
			array[3] = 402657344L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x000C49F0 File Offset: 0x000C2BF0
		private static long[] mk_tokenSet_22_()
		{
			long[] array = new long[8];
			array[0] = 9007199254740992L;
			array[1] = 4294967296L;
			array[2] = 0L;
			array[3] = 3298534883328L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x000C4A40 File Offset: 0x000C2C40
		private static long[] mk_tokenSet_23_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 103079215104L;
			array[2] = 0L;
			array[3] = 65L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x000C4A84 File Offset: 0x000C2C84
		private static long[] mk_tokenSet_24_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 68719476736L;
			array[2] = 0L;
			array[3] = 8537858113666L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000C4ACC File Offset: 0x000C2CCC
		private static long[] mk_tokenSet_25_()
		{
			long[] array = new long[8];
			for (int i = 0; i <= 2; i++)
			{
				array[i] = 0L;
			}
			array[3] = 1111859658817L;
			for (int j = 4; j <= 7; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x000C4B10 File Offset: 0x000C2D10
		private static long[] mk_tokenSet_26_()
		{
			long[] array = new long[8];
			array[0] = 8704L;
			array[1] = 0L;
			array[2] = 8L;
			array[3] = 64L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000C4B80 File Offset: 0x000C2D80
		private static long[] mk_tokenSet_27_()
		{
			return new long[] { 3459045988797251616L, -9223372036850581504L, 4294971392L, 0L, 0L, 0L };
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x000C4BA0 File Offset: 0x000C2DA0
		private static long[] mk_tokenSet_28_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 2199023255552L;
			array[2] = long.MinValue;
			array[3] = 1099511627840L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x000C4BF0 File Offset: 0x000C2DF0
		private static long[] mk_tokenSet_29_()
		{
			long[] array = new long[8];
			array[0] = 3585832916850225218L;
			array[1] = 7530163714935491585L;
			array[2] = -9209522008302168998L;
			array[3] = 402657280L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000C4C44 File Offset: 0x000C2E44
		private static long[] mk_tokenSet_30_()
		{
			long[] array = new long[8];
			array[0] = 3585832916850225218L;
			array[1] = 7530022977447136257L;
			array[2] = -9209522008302168998L;
			array[3] = 3298937540608L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000C4C9C File Offset: 0x000C2E9C
		private static long[] mk_tokenSet_31_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 128L;
			array[2] = 0L;
			array[3] = 4398583382144L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000C4CE4 File Offset: 0x000C2EE4
		private static long[] mk_tokenSet_32_()
		{
			long[] array = new long[8];
			array[0] = 4162293738409996354L;
			array[1] = 7531854764416711945L;
			array[2] = -9209511012112149382L;
			array[3] = 402657349L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000C4D38 File Offset: 0x000C2F38
		private static long[] mk_tokenSet_33_()
		{
			long[] array = new long[8];
			array[0] = 9214359614785058530L;
			array[1] = -301742012686680071L;
			array[2] = -1139059068170799110L;
			array[3] = 17437434051583L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000C4D90 File Offset: 0x000C2F90
		private static long[] mk_tokenSet_34_()
		{
			long[] array = new long[8];
			array[0] = 576460786663161858L;
			array[1] = 8390656L;
			array[2] = 8797166764032L;
			array[3] = 134217729L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000C4DE0 File Offset: 0x000C2FE0
		private static long[] mk_tokenSet_35_()
		{
			long[] array = new long[8];
			for (int i = 0; i <= 2; i++)
			{
				array[i] = 0L;
			}
			array[3] = 3298534883592L;
			for (int j = 4; j <= 7; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x000C4E24 File Offset: 0x000C3024
		private static long[] mk_tokenSet_36_()
		{
			long[] array = new long[8];
			array[0] = 576460786663161858L;
			array[1] = 562949961824392L;
			array[2] = 10996190019584L;
			array[3] = 3298669101377L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000C4E7C File Offset: 0x000C307C
		private static long[] mk_tokenSet_37_()
		{
			long[] array = new long[8];
			array[0] = 3315749355520L;
			array[1] = 206695464960L;
			array[2] = -9223372002494504672L;
			array[3] = 17437030875554L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x000C4ED4 File Offset: 0x000C30D4
		private static long[] mk_tokenSet_38_()
		{
			long[] array = new long[8];
			array[0] = 576464102479626754L;
			array[1] = 563156657256584L;
			array[2] = -2305830879151771360L;
			array[3] = 17437165503467L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000C4F2C File Offset: 0x000C312C
		private static long[] mk_tokenSet_39_()
		{
			long[] array = new long[8];
			array[0] = 4162293669690519618L;
			array[1] = 7530726664880532489L;
			array[2] = -9209513211135404966L;
			array[3] = 402657281L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x000C4F80 File Offset: 0x000C3180
		private static long[] mk_tokenSet_40_()
		{
			long[] array = new long[8];
			array[0] = 9214359473051137762L;
			array[1] = -301812381430857799L;
			array[2] = -1139059068170799110L;
			array[3] = 17437434049535L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000C4FD8 File Offset: 0x000C31D8
		private static long[] mk_tokenSet_41_()
		{
			long[] array = new long[8];
			array[0] = 4162293669690519618L;
			array[1] = 7530726664897313929L;
			array[2] = -9209519808205171622L;
			array[3] = 3298937540929L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000C5030 File Offset: 0x000C3230
		private static long[] mk_tokenSet_42_()
		{
			long[] array = new long[8];
			array[0] = 4162296985506984514L;
			array[1] = 7530726871592746121L;
			array[2] = -2291989646692190854L;
			array[3] = 17437433943019L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000C5088 File Offset: 0x000C3288
		private static long[] mk_tokenSet_43_()
		{
			long[] array = new long[8];
			array[0] = 4162296985506984514L;
			array[1] = 7530726871592746121L;
			array[2] = -2291980850599168646L;
			array[3] = 17437433926635L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000C5110 File Offset: 0x000C3310
		private static long[] mk_tokenSet_44_()
		{
			return new long[] { 68719476736L, 1125900512919808L, 32L, 0L, 0L, 0L };
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x000C5130 File Offset: 0x000C3330
		private static long[] mk_tokenSet_45_()
		{
			long[] array = new long[8];
			array[0] = 4294967296L;
			array[1] = 131941395333184L;
			array[2] = long.MinValue;
			array[3] = 7696581396736L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000C5188 File Offset: 0x000C3388
		private static long[] mk_tokenSet_46_()
		{
			long[] array = new long[8];
			array[0] = 4162293738409996866L;
			array[1] = 7531854764416728329L;
			array[2] = -9209511012112149382L;
			array[3] = 3298937540933L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000C51E0 File Offset: 0x000C33E0
		private static long[] mk_tokenSet_47_()
		{
			long[] array = new long[8];
			array[0] = 3315816464384L;
			array[1] = 206695432192L;
			array[2] = -2305841875341790944L;
			array[3] = 17437031269355L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000C5238 File Offset: 0x000C3438
		private static long[] mk_tokenSet_48_()
		{
			long[] array = new long[8];
			array[0] = 68719477248L;
			array[1] = 1125900512936192L;
			array[2] = -9223363240761749472L;
			array[3] = 3298534883584L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000C5290 File Offset: 0x000C3490
		private static long[] mk_tokenSet_49_()
		{
			long[] array = new long[8];
			array[0] = 4162293738409996866L;
			array[1] = 7531854764416728329L;
			array[2] = -9209511012112149382L;
			array[3] = 3298937540677L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000C52E8 File Offset: 0x000C34E8
		private static long[] mk_tokenSet_50_()
		{
			long[] array = new long[8];
			array[0] = 9214359614785058530L;
			array[1] = -301742012686663687L;
			array[2] = -1139059068170799110L;
			array[3] = 17437434051583L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x000C5340 File Offset: 0x000C3540
		private static long[] mk_tokenSet_51_()
		{
			long[] array = new long[8];
			array[0] = 144053237710848L;
			array[1] = 206695432192L;
			array[2] = -9223372002494504672L;
			array[3] = 17437030875555L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000C5398 File Offset: 0x000C3598
		private static long[] mk_tokenSet_52_()
		{
			long[] array = new long[8];
			array[0] = 4611689336324227072L;
			array[1] = 241055170592L;
			array[2] = -9223371997662666464L;
			array[3] = 17437030875554L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x000C53F0 File Offset: 0x000C35F0
		private static long[] mk_tokenSet_53_()
		{
			long[] array = new long[6];
			array[0] = 3458905251308896256L;
			array[2] = -9223371968135294976L;
			return array;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x000C5420 File Offset: 0x000C3620
		private static long[] mk_tokenSet_54_()
		{
			long[] array = new long[8];
			array[0] = 2255115563040800L;
			array[1] = 9002788487168L;
			array[2] = -9223371933758246624L;
			array[3] = 17437030875562L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000C5478 File Offset: 0x000C3678
		private static long[] mk_tokenSet_55_()
		{
			long[] array = new long[8];
			for (int i = 0; i <= 2; i++)
			{
				array[i] = 0L;
			}
			array[3] = 3298534899968L;
			for (int j = 4; j <= 7; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000C54BC File Offset: 0x000C36BC
		private static long[] mk_tokenSet_56_()
		{
			long[] array = new long[8];
			array[0] = 3585836232666689602L;
			array[1] = 7530163921614146689L;
			array[2] = -2291989647765932678L;
			array[3] = 17437433926634L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x000C5514 File Offset: 0x000C3714
		private static long[] mk_tokenSet_57_()
		{
			long[] array = new long[8];
			for (int i = 0; i <= 1; i++)
			{
				array[i] = 0L;
			}
			array[2] = long.MinValue;
			array[3] = 3298534883584L;
			for (int j = 4; j <= 7; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000C5564 File Offset: 0x000C3764
		private static long[] mk_tokenSet_58_()
		{
			long[] array = new long[8];
			array[0] = 3585973654338580546L;
			array[1] = 7530163714918714497L;
			array[2] = -9209510944466414502L;
			array[3] = 3298937540864L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x000C55BC File Offset: 0x000C37BC
		private static long[] mk_tokenSet_59_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 1064960L;
			array[2] = 0L;
			array[3] = 1099511627840L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000C5604 File Offset: 0x000C3804
		private static long[] mk_tokenSet_60_()
		{
			long[] array = new long[8];
			array[0] = 4162293738409996866L;
			array[1] = 7531854764417776905L;
			array[2] = -9209511012112149382L;
			array[3] = 3298937540677L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000C565C File Offset: 0x000C385C
		private static long[] mk_tokenSet_61_()
		{
			long[] array = new long[8];
			array[0] = 4162293738409996354L;
			array[1] = 7531854764417776905L;
			array[2] = -9209511012112149382L;
			array[3] = 1099914285125L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x000C56B4 File Offset: 0x000C38B4
		private static long[] mk_tokenSet_62_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 1064960L;
			array[2] = long.MinValue;
			array[3] = 1099511627776L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x000C5700 File Offset: 0x000C3900
		private static long[] mk_tokenSet_63_()
		{
			long[] array = new long[8];
			array[0] = 4162293738409996866L;
			array[1] = 7531854764416711945L;
			array[2] = -9209511012112149382L;
			array[3] = 3298937540677L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000C5758 File Offset: 0x000C3958
		private static long[] mk_tokenSet_64_()
		{
			long[] array = new long[8];
			array[0] = 3585832916850225218L;
			array[1] = 7530163714918714369L;
			array[2] = -9209522008302168998L;
			array[3] = 402657344L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000C57AC File Offset: 0x000C39AC
		private static long[] mk_tokenSet_65_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 9570149275275264L;
			array[2] = 1073741824L;
			array[3] = 1099511627776L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000C57F8 File Offset: 0x000C39F8
		private static long[] mk_tokenSet_66_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 8796093022208L;
			array[2] = 0L;
			array[3] = 7696581394688L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000C5840 File Offset: 0x000C3A40
		private static long[] mk_tokenSet_67_()
		{
			long[] array = new long[8];
			array[0] = 3585973654338580546L;
			array[1] = 7530163783638191105L;
			array[2] = -9209513212209146790L;
			array[3] = 26130446815618L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000C5898 File Offset: 0x000C3A98
		private static long[] mk_tokenSet_68_()
		{
			long[] array = new long[8];
			array[0] = 140737488355328L;
			array[1] = 68719476736L;
			array[2] = 0L;
			array[3] = 8537858113666L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000C58E8 File Offset: 0x000C3AE8
		private static long[] mk_tokenSet_69_()
		{
			long[] array = new long[8];
			array[0] = 3585832916850225218L;
			array[1] = 7530163714918714369L;
			array[2] = -9209513212209146790L;
			array[3] = 1112262316097L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000C5940 File Offset: 0x000C3B40
		private static long[] mk_tokenSet_70_()
		{
			long[] array = new long[8];
			array[0] = 3585832916850225218L;
			array[1] = 7530163714918714369L;
			array[2] = -9209513212209146790L;
			array[3] = 402657345L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000C5994 File Offset: 0x000C3B94
		private static long[] mk_tokenSet_71_()
		{
			long[] array = new long[8];
			array[0] = 140738564194304L;
			array[1] = 18014398509482000L;
			array[2] = (long)((ulong)int.MinValue);
			array[3] = 3298534883328L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000C59E8 File Offset: 0x000C3BE8
		private static long[] mk_tokenSet_72_()
		{
			long[] array = new long[8];
			array[0] = 3585973655414419522L;
			array[1] = -1675332358812431343L;
			array[2] = -9209513210061662886L;
			array[3] = 402657345L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000C5A3C File Offset: 0x000C3C3C
		private static long[] mk_tokenSet_73_()
		{
			long[] array = new long[8];
			array[0] = 8348542375365358178L;
			array[1] = -1456844949414244683L;
			array[2] = -9209512900031547398L;
			array[3] = 17437433532907L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x000C5A94 File Offset: 0x000C3C94
		private static long[] mk_tokenSet_74_()
		{
			long[] array = new long[8];
			for (int i = 0; i <= 2; i++)
			{
				array[i] = 0L;
			}
			array[3] = 1610612896L;
			for (int j = 4; j <= 7; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000C5AD4 File Offset: 0x000C3CD4
		private static long[] mk_tokenSet_75_()
		{
			long[] array = new long[8];
			array[0] = 3585973655414419522L;
			array[1] = -1675334557835686895L;
			array[2] = -9209522006154685094L;
			array[3] = 402657345L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x000C5B28 File Offset: 0x000C3D28
		private static long[] mk_tokenSet_76_()
		{
			long[] array = new long[8];
			array[0] = 8348542375365358178L;
			array[1] = -1456844949414244679L;
			array[2] = -9209512900031547398L;
			array[3] = 17437433532907L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000C5B80 File Offset: 0x000C3D80
		private static long[] mk_tokenSet_77_()
		{
			long[] array = new long[8];
			array[0] = 8348542375365358178L;
			array[1] = -1456844949414244687L;
			array[2] = -9209512900031547398L;
			array[3] = 17437433532907L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000C5BD8 File Offset: 0x000C3DD8
		private static long[] mk_tokenSet_78_()
		{
			long[] array = new long[8];
			array[0] = 3585973655414419522L;
			array[1] = -1675332358812431343L;
			array[2] = -9209522006154685094L;
			array[3] = 402657345L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000C5C2C File Offset: 0x000C3E2C
		private static long[] mk_tokenSet_79_()
		{
			long[] array = new long[8];
			array[0] = 3315816480768L;
			array[1] = 242162991104L;
			array[2] = -1152920370734943968L;
			array[3] = 17437031392186L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000C5C84 File Offset: 0x000C3E84
		private static long[] mk_tokenSet_80_()
		{
			long[] array = new long[8];
			for (int i = 0; i <= 2; i++)
			{
				array[i] = 0L;
			}
			array[3] = 3298534883585L;
			for (int j = 4; j <= 7; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x000C5CC8 File Offset: 0x000C3EC8
		private static long[] mk_tokenSet_81_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 131072L;
			array[2] = 256L;
			array[3] = 3332894621952L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x000C5D14 File Offset: 0x000C3F14
		private static long[] mk_tokenSet_82_()
		{
			long[] array = new long[8];
			for (int i = 0; i <= 2; i++)
			{
				array[i] = 0L;
			}
			array[3] = 3298534883649L;
			for (int j = 4; j <= 7; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x000C5D58 File Offset: 0x000C3F58
		private static long[] mk_tokenSet_83_()
		{
			long[] array = new long[8];
			for (int i = 0; i <= 2; i++)
			{
				array[i] = 0L;
			}
			array[3] = 3298534883648L;
			for (int j = 4; j <= 7; j++)
			{
				array[j] = 0L;
			}
			return array;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000C5D9C File Offset: 0x000C3F9C
		private static long[] mk_tokenSet_84_()
		{
			long[] array = new long[8];
			array[0] = 0L;
			array[1] = 68719476736L;
			array[2] = 0L;
			array[3] = 5239323230210L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000C5DE4 File Offset: 0x000C3FE4
		private static long[] mk_tokenSet_85_()
		{
			long[] array = new long[8];
			array[0] = 4451790753099871938L;
			array[1] = -1673221294748025447L;
			array[2] = -1139059378200914566L;
			array[3] = 4123571778303L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000C5E3C File Offset: 0x000C403C
		private static long[] mk_tokenSet_86_()
		{
			long[] array = new long[8];
			array[0] = 4451790753099871938L;
			array[1] = -1673221294748025447L;
			array[2] = -1139059378200914566L;
			array[3] = 4123571778559L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000C5E94 File Offset: 0x000C4094
		private static long[] mk_tokenSet_87_()
		{
			long[] array = new long[8];
			array[0] = 2395853051396128L;
			array[1] = 206695432192L;
			array[2] = -9223372002494504672L;
			array[3] = 17437030875555L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000C5EEC File Offset: 0x000C40EC
		private static long[] mk_tokenSet_88_()
		{
			long[] array = new long[8];
			array[0] = 9223366814039799778L;
			array[1] = -301742008257495047L;
			array[2] = -1139058999451191302L;
			array[3] = 17437434051583L;
			for (int i = 4; i <= 7; i++)
			{
				array[i] = 0L;
			}
			return array;
		}

		// Token: 0x0400140E RID: 5134
		public const int EOF = 1;

		// Token: 0x0400140F RID: 5135
		public const int NULL_TREE_LOOKAHEAD = 3;

		// Token: 0x04001410 RID: 5136
		public const int Add = 4;

		// Token: 0x04001411 RID: 5137
		public const int All = 5;

		// Token: 0x04001412 RID: 5138
		public const int Alter = 6;

		// Token: 0x04001413 RID: 5139
		public const int And = 7;

		// Token: 0x04001414 RID: 5140
		public const int Any = 8;

		// Token: 0x04001415 RID: 5141
		public const int As = 9;

		// Token: 0x04001416 RID: 5142
		public const int Asc = 10;

		// Token: 0x04001417 RID: 5143
		public const int Authorization = 11;

		// Token: 0x04001418 RID: 5144
		public const int Backup = 12;

		// Token: 0x04001419 RID: 5145
		public const int Begin = 13;

		// Token: 0x0400141A RID: 5146
		public const int Between = 14;

		// Token: 0x0400141B RID: 5147
		public const int Break = 15;

		// Token: 0x0400141C RID: 5148
		public const int Browse = 16;

		// Token: 0x0400141D RID: 5149
		public const int Bulk = 17;

		// Token: 0x0400141E RID: 5150
		public const int By = 18;

		// Token: 0x0400141F RID: 5151
		public const int Cascade = 19;

		// Token: 0x04001420 RID: 5152
		public const int Case = 20;

		// Token: 0x04001421 RID: 5153
		public const int Check = 21;

		// Token: 0x04001422 RID: 5154
		public const int Checkpoint = 22;

		// Token: 0x04001423 RID: 5155
		public const int Close = 23;

		// Token: 0x04001424 RID: 5156
		public const int Clustered = 24;

		// Token: 0x04001425 RID: 5157
		public const int Coalesce = 25;

		// Token: 0x04001426 RID: 5158
		public const int Collate = 26;

		// Token: 0x04001427 RID: 5159
		public const int Column = 27;

		// Token: 0x04001428 RID: 5160
		public const int Commit = 28;

		// Token: 0x04001429 RID: 5161
		public const int Compute = 29;

		// Token: 0x0400142A RID: 5162
		public const int Constraint = 30;

		// Token: 0x0400142B RID: 5163
		public const int Contains = 31;

		// Token: 0x0400142C RID: 5164
		public const int ContainsTable = 32;

		// Token: 0x0400142D RID: 5165
		public const int Continue = 33;

		// Token: 0x0400142E RID: 5166
		public const int Convert = 34;

		// Token: 0x0400142F RID: 5167
		public const int Create = 35;

		// Token: 0x04001430 RID: 5168
		public const int Cross = 36;

		// Token: 0x04001431 RID: 5169
		public const int Current = 37;

		// Token: 0x04001432 RID: 5170
		public const int CurrentDate = 38;

		// Token: 0x04001433 RID: 5171
		public const int CurrentTime = 39;

		// Token: 0x04001434 RID: 5172
		public const int CurrentTimestamp = 40;

		// Token: 0x04001435 RID: 5173
		public const int CurrentUser = 41;

		// Token: 0x04001436 RID: 5174
		public const int Cursor = 42;

		// Token: 0x04001437 RID: 5175
		public const int Database = 43;

		// Token: 0x04001438 RID: 5176
		public const int Dbcc = 44;

		// Token: 0x04001439 RID: 5177
		public const int Deallocate = 45;

		// Token: 0x0400143A RID: 5178
		public const int Declare = 46;

		// Token: 0x0400143B RID: 5179
		public const int Default = 47;

		// Token: 0x0400143C RID: 5180
		public const int Delete = 48;

		// Token: 0x0400143D RID: 5181
		public const int Deny = 49;

		// Token: 0x0400143E RID: 5182
		public const int Desc = 50;

		// Token: 0x0400143F RID: 5183
		public const int Distinct = 51;

		// Token: 0x04001440 RID: 5184
		public const int Distributed = 52;

		// Token: 0x04001441 RID: 5185
		public const int Double = 53;

		// Token: 0x04001442 RID: 5186
		public const int Drop = 54;

		// Token: 0x04001443 RID: 5187
		public const int Else = 55;

		// Token: 0x04001444 RID: 5188
		public const int End = 56;

		// Token: 0x04001445 RID: 5189
		public const int Errlvl = 57;

		// Token: 0x04001446 RID: 5190
		public const int Escape = 58;

		// Token: 0x04001447 RID: 5191
		public const int Except = 59;

		// Token: 0x04001448 RID: 5192
		public const int Exec = 60;

		// Token: 0x04001449 RID: 5193
		public const int Execute = 61;

		// Token: 0x0400144A RID: 5194
		public const int Exists = 62;

		// Token: 0x0400144B RID: 5195
		public const int Exit = 63;

		// Token: 0x0400144C RID: 5196
		public const int Fetch = 64;

		// Token: 0x0400144D RID: 5197
		public const int File = 65;

		// Token: 0x0400144E RID: 5198
		public const int FillFactor = 66;

		// Token: 0x0400144F RID: 5199
		public const int For = 67;

		// Token: 0x04001450 RID: 5200
		public const int Foreign = 68;

		// Token: 0x04001451 RID: 5201
		public const int FreeText = 69;

		// Token: 0x04001452 RID: 5202
		public const int FreeTextTable = 70;

		// Token: 0x04001453 RID: 5203
		public const int From = 71;

		// Token: 0x04001454 RID: 5204
		public const int Full = 72;

		// Token: 0x04001455 RID: 5205
		public const int Function = 73;

		// Token: 0x04001456 RID: 5206
		public const int GoTo = 74;

		// Token: 0x04001457 RID: 5207
		public const int Grant = 75;

		// Token: 0x04001458 RID: 5208
		public const int Group = 76;

		// Token: 0x04001459 RID: 5209
		public const int Having = 77;

		// Token: 0x0400145A RID: 5210
		public const int HoldLock = 78;

		// Token: 0x0400145B RID: 5211
		public const int Identity = 79;

		// Token: 0x0400145C RID: 5212
		public const int IdentityInsert = 80;

		// Token: 0x0400145D RID: 5213
		public const int IdentityColumn = 81;

		// Token: 0x0400145E RID: 5214
		public const int If = 82;

		// Token: 0x0400145F RID: 5215
		public const int In = 83;

		// Token: 0x04001460 RID: 5216
		public const int Index = 84;

		// Token: 0x04001461 RID: 5217
		public const int Inner = 85;

		// Token: 0x04001462 RID: 5218
		public const int Insert = 86;

		// Token: 0x04001463 RID: 5219
		public const int Intersect = 87;

		// Token: 0x04001464 RID: 5220
		public const int Into = 88;

		// Token: 0x04001465 RID: 5221
		public const int Is = 89;

		// Token: 0x04001466 RID: 5222
		public const int Join = 90;

		// Token: 0x04001467 RID: 5223
		public const int Key = 91;

		// Token: 0x04001468 RID: 5224
		public const int Kill = 92;

		// Token: 0x04001469 RID: 5225
		public const int Left = 93;

		// Token: 0x0400146A RID: 5226
		public const int Like = 94;

		// Token: 0x0400146B RID: 5227
		public const int LineNo = 95;

		// Token: 0x0400146C RID: 5228
		public const int National = 96;

		// Token: 0x0400146D RID: 5229
		public const int NoCheck = 97;

		// Token: 0x0400146E RID: 5230
		public const int NonClustered = 98;

		// Token: 0x0400146F RID: 5231
		public const int Not = 99;

		// Token: 0x04001470 RID: 5232
		public const int Null = 100;

		// Token: 0x04001471 RID: 5233
		public const int NullIf = 101;

		// Token: 0x04001472 RID: 5234
		public const int Of = 102;

		// Token: 0x04001473 RID: 5235
		public const int Off = 103;

		// Token: 0x04001474 RID: 5236
		public const int Offsets = 104;

		// Token: 0x04001475 RID: 5237
		public const int On = 105;

		// Token: 0x04001476 RID: 5238
		public const int Open = 106;

		// Token: 0x04001477 RID: 5239
		public const int OpenDataSource = 107;

		// Token: 0x04001478 RID: 5240
		public const int OpenQuery = 108;

		// Token: 0x04001479 RID: 5241
		public const int OpenRowSet = 109;

		// Token: 0x0400147A RID: 5242
		public const int OpenXml = 110;

		// Token: 0x0400147B RID: 5243
		public const int Option = 111;

		// Token: 0x0400147C RID: 5244
		public const int Or = 112;

		// Token: 0x0400147D RID: 5245
		public const int Order = 113;

		// Token: 0x0400147E RID: 5246
		public const int Outer = 114;

		// Token: 0x0400147F RID: 5247
		public const int Over = 115;

		// Token: 0x04001480 RID: 5248
		public const int Percent = 116;

		// Token: 0x04001481 RID: 5249
		public const int Plan = 117;

		// Token: 0x04001482 RID: 5250
		public const int Primary = 118;

		// Token: 0x04001483 RID: 5251
		public const int Print = 119;

		// Token: 0x04001484 RID: 5252
		public const int Proc = 120;

		// Token: 0x04001485 RID: 5253
		public const int Procedure = 121;

		// Token: 0x04001486 RID: 5254
		public const int Public = 122;

		// Token: 0x04001487 RID: 5255
		public const int Raiserror = 123;

		// Token: 0x04001488 RID: 5256
		public const int Read = 124;

		// Token: 0x04001489 RID: 5257
		public const int ReadText = 125;

		// Token: 0x0400148A RID: 5258
		public const int Reconfigure = 126;

		// Token: 0x0400148B RID: 5259
		public const int References = 127;

		// Token: 0x0400148C RID: 5260
		public const int Replication = 128;

		// Token: 0x0400148D RID: 5261
		public const int Restore = 129;

		// Token: 0x0400148E RID: 5262
		public const int Restrict = 130;

		// Token: 0x0400148F RID: 5263
		public const int Return = 131;

		// Token: 0x04001490 RID: 5264
		public const int Revoke = 132;

		// Token: 0x04001491 RID: 5265
		public const int Right = 133;

		// Token: 0x04001492 RID: 5266
		public const int Rollback = 134;

		// Token: 0x04001493 RID: 5267
		public const int RowCount = 135;

		// Token: 0x04001494 RID: 5268
		public const int RowGuidColumn = 136;

		// Token: 0x04001495 RID: 5269
		public const int Rule = 137;

		// Token: 0x04001496 RID: 5270
		public const int Save = 138;

		// Token: 0x04001497 RID: 5271
		public const int Schema = 139;

		// Token: 0x04001498 RID: 5272
		public const int Select = 140;

		// Token: 0x04001499 RID: 5273
		public const int SessionUser = 141;

		// Token: 0x0400149A RID: 5274
		public const int Set = 142;

		// Token: 0x0400149B RID: 5275
		public const int SetUser = 143;

		// Token: 0x0400149C RID: 5276
		public const int Shutdown = 144;

		// Token: 0x0400149D RID: 5277
		public const int Some = 145;

		// Token: 0x0400149E RID: 5278
		public const int Statistics = 146;

		// Token: 0x0400149F RID: 5279
		public const int SystemUser = 147;

		// Token: 0x040014A0 RID: 5280
		public const int Table = 148;

		// Token: 0x040014A1 RID: 5281
		public const int TextSize = 149;

		// Token: 0x040014A2 RID: 5282
		public const int Then = 150;

		// Token: 0x040014A3 RID: 5283
		public const int To = 151;

		// Token: 0x040014A4 RID: 5284
		public const int Top = 152;

		// Token: 0x040014A5 RID: 5285
		public const int Tran = 153;

		// Token: 0x040014A6 RID: 5286
		public const int Transaction = 154;

		// Token: 0x040014A7 RID: 5287
		public const int Trigger = 155;

		// Token: 0x040014A8 RID: 5288
		public const int Truncate = 156;

		// Token: 0x040014A9 RID: 5289
		public const int TSEqual = 157;

		// Token: 0x040014AA RID: 5290
		public const int Union = 158;

		// Token: 0x040014AB RID: 5291
		public const int Unique = 159;

		// Token: 0x040014AC RID: 5292
		public const int Update = 160;

		// Token: 0x040014AD RID: 5293
		public const int UpdateText = 161;

		// Token: 0x040014AE RID: 5294
		public const int Use = 162;

		// Token: 0x040014AF RID: 5295
		public const int User = 163;

		// Token: 0x040014B0 RID: 5296
		public const int Values = 164;

		// Token: 0x040014B1 RID: 5297
		public const int Varying = 165;

		// Token: 0x040014B2 RID: 5298
		public const int View = 166;

		// Token: 0x040014B3 RID: 5299
		public const int WaitFor = 167;

		// Token: 0x040014B4 RID: 5300
		public const int When = 168;

		// Token: 0x040014B5 RID: 5301
		public const int Where = 169;

		// Token: 0x040014B6 RID: 5302
		public const int While = 170;

		// Token: 0x040014B7 RID: 5303
		public const int With = 171;

		// Token: 0x040014B8 RID: 5304
		public const int WriteText = 172;

		// Token: 0x040014B9 RID: 5305
		public const int Disk = 173;

		// Token: 0x040014BA RID: 5306
		public const int Precision = 174;

		// Token: 0x040014BB RID: 5307
		public const int External = 175;

		// Token: 0x040014BC RID: 5308
		public const int Revert = 176;

		// Token: 0x040014BD RID: 5309
		public const int Pivot = 177;

		// Token: 0x040014BE RID: 5310
		public const int Unpivot = 178;

		// Token: 0x040014BF RID: 5311
		public const int TableSample = 179;

		// Token: 0x040014C0 RID: 5312
		public const int Dump = 180;

		// Token: 0x040014C1 RID: 5313
		public const int Load = 181;

		// Token: 0x040014C2 RID: 5314
		public const int Merge = 182;

		// Token: 0x040014C3 RID: 5315
		public const int StopList = 183;

		// Token: 0x040014C4 RID: 5316
		public const int SemanticKeyPhraseTable = 184;

		// Token: 0x040014C5 RID: 5317
		public const int SemanticSimilarityTable = 185;

		// Token: 0x040014C6 RID: 5318
		public const int SemanticSimilarityDetailsTable = 186;

		// Token: 0x040014C7 RID: 5319
		public const int TryConvert = 187;

		// Token: 0x040014C8 RID: 5320
		public const int Bang = 188;

		// Token: 0x040014C9 RID: 5321
		public const int PercentSign = 189;

		// Token: 0x040014CA RID: 5322
		public const int Ampersand = 190;

		// Token: 0x040014CB RID: 5323
		public const int LeftParenthesis = 191;

		// Token: 0x040014CC RID: 5324
		public const int RightParenthesis = 192;

		// Token: 0x040014CD RID: 5325
		public const int LeftCurly = 193;

		// Token: 0x040014CE RID: 5326
		public const int RightCurly = 194;

		// Token: 0x040014CF RID: 5327
		public const int Star = 195;

		// Token: 0x040014D0 RID: 5328
		public const int MultiplyEquals = 196;

		// Token: 0x040014D1 RID: 5329
		public const int Plus = 197;

		// Token: 0x040014D2 RID: 5330
		public const int Comma = 198;

		// Token: 0x040014D3 RID: 5331
		public const int Minus = 199;

		// Token: 0x040014D4 RID: 5332
		public const int Dot = 200;

		// Token: 0x040014D5 RID: 5333
		public const int Divide = 201;

		// Token: 0x040014D6 RID: 5334
		public const int Colon = 202;

		// Token: 0x040014D7 RID: 5335
		public const int DoubleColon = 203;

		// Token: 0x040014D8 RID: 5336
		public const int Semicolon = 204;

		// Token: 0x040014D9 RID: 5337
		public const int LessThan = 205;

		// Token: 0x040014DA RID: 5338
		public const int EqualsSign = 206;

		// Token: 0x040014DB RID: 5339
		public const int RightOuterJoin = 207;

		// Token: 0x040014DC RID: 5340
		public const int GreaterThan = 208;

		// Token: 0x040014DD RID: 5341
		public const int Circumflex = 209;

		// Token: 0x040014DE RID: 5342
		public const int VerticalLine = 210;

		// Token: 0x040014DF RID: 5343
		public const int Tilde = 211;

		// Token: 0x040014E0 RID: 5344
		public const int AddEquals = 212;

		// Token: 0x040014E1 RID: 5345
		public const int SubtractEquals = 213;

		// Token: 0x040014E2 RID: 5346
		public const int DivideEquals = 214;

		// Token: 0x040014E3 RID: 5347
		public const int ModEquals = 215;

		// Token: 0x040014E4 RID: 5348
		public const int BitwiseAndEquals = 216;

		// Token: 0x040014E5 RID: 5349
		public const int BitwiseOrEquals = 217;

		// Token: 0x040014E6 RID: 5350
		public const int BitwiseXorEquals = 218;

		// Token: 0x040014E7 RID: 5351
		public const int Go = 219;

		// Token: 0x040014E8 RID: 5352
		public const int Label = 220;

		// Token: 0x040014E9 RID: 5353
		public const int Integer = 221;

		// Token: 0x040014EA RID: 5354
		public const int Numeric = 222;

		// Token: 0x040014EB RID: 5355
		public const int Real = 223;

		// Token: 0x040014EC RID: 5356
		public const int HexLiteral = 224;

		// Token: 0x040014ED RID: 5357
		public const int Money = 225;

		// Token: 0x040014EE RID: 5358
		public const int SqlCommandIdentifier = 226;

		// Token: 0x040014EF RID: 5359
		public const int PseudoColumn = 227;

		// Token: 0x040014F0 RID: 5360
		public const int DollarPartition = 228;

		// Token: 0x040014F1 RID: 5361
		public const int AsciiStringOrQuotedIdentifier = 229;

		// Token: 0x040014F2 RID: 5362
		public const int AsciiStringLiteral = 230;

		// Token: 0x040014F3 RID: 5363
		public const int UnicodeStringLiteral = 231;

		// Token: 0x040014F4 RID: 5364
		public const int Identifier = 232;

		// Token: 0x040014F5 RID: 5365
		public const int QuotedIdentifier = 233;

		// Token: 0x040014F6 RID: 5366
		public const int Variable = 234;

		// Token: 0x040014F7 RID: 5367
		public const int OdbcInitiator = 235;

		// Token: 0x040014F8 RID: 5368
		public const int ProcNameSemicolon = 236;

		// Token: 0x040014F9 RID: 5369
		public const int SingleLineComment = 237;

		// Token: 0x040014FA RID: 5370
		public const int MultilineComment = 238;

		// Token: 0x040014FB RID: 5371
		public const int WhiteSpace = 239;

		// Token: 0x040014FC RID: 5372
		public static readonly string[] tokenNames_ = new string[]
		{
			"\"<0>\"", "\"EOF\"", "\"<2>\"", "\"NULL_TREE_LOOKAHEAD\"", "\"add\"", "\"all\"", "\"alter\"", "\"and\"", "\"any\"", "\"as\"",
			"\"asc\"", "\"authorization\"", "\"backup\"", "\"begin\"", "\"between\"", "\"break\"", "\"browse\"", "\"bulk\"", "\"by\"", "\"cascade\"",
			"\"case\"", "\"check\"", "\"checkpoint\"", "\"close\"", "\"clustered\"", "\"coalesce\"", "\"collate\"", "\"column\"", "\"commit\"", "\"compute\"",
			"\"constraint\"", "\"contains\"", "\"containstable\"", "\"continue\"", "\"convert\"", "\"create\"", "\"cross\"", "\"current\"", "\"current_date\"", "\"current_time\"",
			"\"current_timestamp\"", "\"current_user\"", "\"cursor\"", "\"database\"", "\"dbcc\"", "\"deallocate\"", "\"declare\"", "\"default\"", "\"delete\"", "\"deny\"",
			"\"desc\"", "\"distinct\"", "\"distributed\"", "\"double\"", "\"drop\"", "\"else\"", "\"end\"", "\"errlvl\"", "\"escape\"", "\"except\"",
			"\"exec\"", "\"execute\"", "\"exists\"", "\"exit\"", "\"fetch\"", "\"file\"", "\"fillfactor\"", "\"for\"", "\"foreign\"", "\"freetext\"",
			"\"freetexttable\"", "\"from\"", "\"full\"", "\"function\"", "\"goto\"", "\"grant\"", "\"group\"", "\"having\"", "\"holdlock\"", "\"identity\"",
			"\"identity_insert\"", "\"identitycol\"", "\"if\"", "\"in\"", "\"index\"", "\"inner\"", "\"insert\"", "\"intersect\"", "\"into\"", "\"is\"",
			"\"join\"", "\"key\"", "\"kill\"", "\"left\"", "\"like\"", "\"lineno\"", "\"national\"", "\"nocheck\"", "\"nonclustered\"", "\"not\"",
			"\"null\"", "\"nullif\"", "\"of\"", "\"off\"", "\"offsets\"", "\"on\"", "\"open\"", "\"opendatasource\"", "\"openquery\"", "\"openrowset\"",
			"\"openxml\"", "\"option\"", "\"or\"", "\"order\"", "\"outer\"", "\"over\"", "\"percent\"", "\"plan\"", "\"primary\"", "\"print\"",
			"\"proc\"", "\"procedure\"", "\"public\"", "\"raiserror\"", "\"read\"", "\"readtext\"", "\"reconfigure\"", "\"references\"", "\"replication\"", "\"restore\"",
			"\"restrict\"", "\"return\"", "\"revoke\"", "\"right\"", "\"rollback\"", "\"rowcount\"", "\"rowguidcol\"", "\"rule\"", "\"save\"", "\"schema\"",
			"\"select\"", "\"session_user\"", "\"set\"", "\"setuser\"", "\"shutdown\"", "\"some\"", "\"statistics\"", "\"system_user\"", "\"table\"", "\"textsize\"",
			"\"then\"", "\"to\"", "\"top\"", "\"tran\"", "\"transaction\"", "\"trigger\"", "\"truncate\"", "\"tsequal\"", "\"union\"", "\"unique\"",
			"\"update\"", "\"updatetext\"", "\"use\"", "\"user\"", "\"values\"", "\"varying\"", "\"view\"", "\"waitfor\"", "\"when\"", "\"where\"",
			"\"while\"", "\"with\"", "\"writetext\"", "\"Disk\"", "\"Precision\"", "\"External\"", "\"Revert\"", "\"Pivot\"", "\"Unpivot\"", "\"TableSample\"",
			"\"Dump\"", "\"Load\"", "\"Merge\"", "\"StopList\"", "\"SemanticKeyPhraseTable\"", "\"SemanticSimilarityTable\"", "\"SemanticSimilarityDetailsTable\"", "\"TryConvert\"", "\"Bang\"", "\"PercentSign\"",
			"\"Ampersand\"", "\"LeftParenthesis\"", "\"RightParenthesis\"", "\"LeftCurly\"", "\"RightCurly\"", "\"Star\"", "\"MultiplyEquals\"", "\"Plus\"", "\"Comma\"", "\"Minus\"",
			"\"Dot\"", "\"Divide\"", "\"Colon\"", "\"DoubleColon\"", "\"Semicolon\"", "\"LessThan\"", "\"EqualsSign\"", "\"RightOuterJoin\"", "\"GreaterThan\"", "\"Circumflex\"",
			"\"VerticalLine\"", "\"Tilde\"", "\"AddEquals\"", "\"SubtractEquals\"", "\"DivideEquals\"", "\"ModEquals\"", "\"BitwiseAndEquals\"", "\"BitwiseOrEquals\"", "\"BitwiseXorEquals\"", "\"Go\"",
			"\"Label\"", "\"Integer\"", "\"Numeric\"", "\"Real\"", "\"HexLiteral\"", "\"Money\"", "\"SqlCommandIdentifier\"", "\"PseudoColumn\"", "\"DollarPartition\"", "\"AsciiStringOrQuotedIdentifier\"",
			"\"AsciiStringLiteral\"", "\"UnicodeStringLiteral\"", "\"Identifier\"", "\"QuotedIdentifier\"", "\"Variable\"", "\"OdbcInitiator\"", "\"ProcNameSemicolon\"", "\"SingleLineComment\"", "\"MultilineComment\"", "\"WhiteSpace\""
		};

		// Token: 0x040014FD RID: 5373
		public static readonly BitSet tokenSet_0_ = new BitSet(TSql80ParserInternal.mk_tokenSet_0_());

		// Token: 0x040014FE RID: 5374
		public static readonly BitSet tokenSet_1_ = new BitSet(TSql80ParserInternal.mk_tokenSet_1_());

		// Token: 0x040014FF RID: 5375
		public static readonly BitSet tokenSet_2_ = new BitSet(TSql80ParserInternal.mk_tokenSet_2_());

		// Token: 0x04001500 RID: 5376
		public static readonly BitSet tokenSet_3_ = new BitSet(TSql80ParserInternal.mk_tokenSet_3_());

		// Token: 0x04001501 RID: 5377
		public static readonly BitSet tokenSet_4_ = new BitSet(TSql80ParserInternal.mk_tokenSet_4_());

		// Token: 0x04001502 RID: 5378
		public static readonly BitSet tokenSet_5_ = new BitSet(TSql80ParserInternal.mk_tokenSet_5_());

		// Token: 0x04001503 RID: 5379
		public static readonly BitSet tokenSet_6_ = new BitSet(TSql80ParserInternal.mk_tokenSet_6_());

		// Token: 0x04001504 RID: 5380
		public static readonly BitSet tokenSet_7_ = new BitSet(TSql80ParserInternal.mk_tokenSet_7_());

		// Token: 0x04001505 RID: 5381
		public static readonly BitSet tokenSet_8_ = new BitSet(TSql80ParserInternal.mk_tokenSet_8_());

		// Token: 0x04001506 RID: 5382
		public static readonly BitSet tokenSet_9_ = new BitSet(TSql80ParserInternal.mk_tokenSet_9_());

		// Token: 0x04001507 RID: 5383
		public static readonly BitSet tokenSet_10_ = new BitSet(TSql80ParserInternal.mk_tokenSet_10_());

		// Token: 0x04001508 RID: 5384
		public static readonly BitSet tokenSet_11_ = new BitSet(TSql80ParserInternal.mk_tokenSet_11_());

		// Token: 0x04001509 RID: 5385
		public static readonly BitSet tokenSet_12_ = new BitSet(TSql80ParserInternal.mk_tokenSet_12_());

		// Token: 0x0400150A RID: 5386
		public static readonly BitSet tokenSet_13_ = new BitSet(TSql80ParserInternal.mk_tokenSet_13_());

		// Token: 0x0400150B RID: 5387
		public static readonly BitSet tokenSet_14_ = new BitSet(TSql80ParserInternal.mk_tokenSet_14_());

		// Token: 0x0400150C RID: 5388
		public static readonly BitSet tokenSet_15_ = new BitSet(TSql80ParserInternal.mk_tokenSet_15_());

		// Token: 0x0400150D RID: 5389
		public static readonly BitSet tokenSet_16_ = new BitSet(TSql80ParserInternal.mk_tokenSet_16_());

		// Token: 0x0400150E RID: 5390
		public static readonly BitSet tokenSet_17_ = new BitSet(TSql80ParserInternal.mk_tokenSet_17_());

		// Token: 0x0400150F RID: 5391
		public static readonly BitSet tokenSet_18_ = new BitSet(TSql80ParserInternal.mk_tokenSet_18_());

		// Token: 0x04001510 RID: 5392
		public static readonly BitSet tokenSet_19_ = new BitSet(TSql80ParserInternal.mk_tokenSet_19_());

		// Token: 0x04001511 RID: 5393
		public static readonly BitSet tokenSet_20_ = new BitSet(TSql80ParserInternal.mk_tokenSet_20_());

		// Token: 0x04001512 RID: 5394
		public static readonly BitSet tokenSet_21_ = new BitSet(TSql80ParserInternal.mk_tokenSet_21_());

		// Token: 0x04001513 RID: 5395
		public static readonly BitSet tokenSet_22_ = new BitSet(TSql80ParserInternal.mk_tokenSet_22_());

		// Token: 0x04001514 RID: 5396
		public static readonly BitSet tokenSet_23_ = new BitSet(TSql80ParserInternal.mk_tokenSet_23_());

		// Token: 0x04001515 RID: 5397
		public static readonly BitSet tokenSet_24_ = new BitSet(TSql80ParserInternal.mk_tokenSet_24_());

		// Token: 0x04001516 RID: 5398
		public static readonly BitSet tokenSet_25_ = new BitSet(TSql80ParserInternal.mk_tokenSet_25_());

		// Token: 0x04001517 RID: 5399
		public static readonly BitSet tokenSet_26_ = new BitSet(TSql80ParserInternal.mk_tokenSet_26_());

		// Token: 0x04001518 RID: 5400
		public static readonly BitSet tokenSet_27_ = new BitSet(TSql80ParserInternal.mk_tokenSet_27_());

		// Token: 0x04001519 RID: 5401
		public static readonly BitSet tokenSet_28_ = new BitSet(TSql80ParserInternal.mk_tokenSet_28_());

		// Token: 0x0400151A RID: 5402
		public static readonly BitSet tokenSet_29_ = new BitSet(TSql80ParserInternal.mk_tokenSet_29_());

		// Token: 0x0400151B RID: 5403
		public static readonly BitSet tokenSet_30_ = new BitSet(TSql80ParserInternal.mk_tokenSet_30_());

		// Token: 0x0400151C RID: 5404
		public static readonly BitSet tokenSet_31_ = new BitSet(TSql80ParserInternal.mk_tokenSet_31_());

		// Token: 0x0400151D RID: 5405
		public static readonly BitSet tokenSet_32_ = new BitSet(TSql80ParserInternal.mk_tokenSet_32_());

		// Token: 0x0400151E RID: 5406
		public static readonly BitSet tokenSet_33_ = new BitSet(TSql80ParserInternal.mk_tokenSet_33_());

		// Token: 0x0400151F RID: 5407
		public static readonly BitSet tokenSet_34_ = new BitSet(TSql80ParserInternal.mk_tokenSet_34_());

		// Token: 0x04001520 RID: 5408
		public static readonly BitSet tokenSet_35_ = new BitSet(TSql80ParserInternal.mk_tokenSet_35_());

		// Token: 0x04001521 RID: 5409
		public static readonly BitSet tokenSet_36_ = new BitSet(TSql80ParserInternal.mk_tokenSet_36_());

		// Token: 0x04001522 RID: 5410
		public static readonly BitSet tokenSet_37_ = new BitSet(TSql80ParserInternal.mk_tokenSet_37_());

		// Token: 0x04001523 RID: 5411
		public static readonly BitSet tokenSet_38_ = new BitSet(TSql80ParserInternal.mk_tokenSet_38_());

		// Token: 0x04001524 RID: 5412
		public static readonly BitSet tokenSet_39_ = new BitSet(TSql80ParserInternal.mk_tokenSet_39_());

		// Token: 0x04001525 RID: 5413
		public static readonly BitSet tokenSet_40_ = new BitSet(TSql80ParserInternal.mk_tokenSet_40_());

		// Token: 0x04001526 RID: 5414
		public static readonly BitSet tokenSet_41_ = new BitSet(TSql80ParserInternal.mk_tokenSet_41_());

		// Token: 0x04001527 RID: 5415
		public static readonly BitSet tokenSet_42_ = new BitSet(TSql80ParserInternal.mk_tokenSet_42_());

		// Token: 0x04001528 RID: 5416
		public static readonly BitSet tokenSet_43_ = new BitSet(TSql80ParserInternal.mk_tokenSet_43_());

		// Token: 0x04001529 RID: 5417
		public static readonly BitSet tokenSet_44_ = new BitSet(TSql80ParserInternal.mk_tokenSet_44_());

		// Token: 0x0400152A RID: 5418
		public static readonly BitSet tokenSet_45_ = new BitSet(TSql80ParserInternal.mk_tokenSet_45_());

		// Token: 0x0400152B RID: 5419
		public static readonly BitSet tokenSet_46_ = new BitSet(TSql80ParserInternal.mk_tokenSet_46_());

		// Token: 0x0400152C RID: 5420
		public static readonly BitSet tokenSet_47_ = new BitSet(TSql80ParserInternal.mk_tokenSet_47_());

		// Token: 0x0400152D RID: 5421
		public static readonly BitSet tokenSet_48_ = new BitSet(TSql80ParserInternal.mk_tokenSet_48_());

		// Token: 0x0400152E RID: 5422
		public static readonly BitSet tokenSet_49_ = new BitSet(TSql80ParserInternal.mk_tokenSet_49_());

		// Token: 0x0400152F RID: 5423
		public static readonly BitSet tokenSet_50_ = new BitSet(TSql80ParserInternal.mk_tokenSet_50_());

		// Token: 0x04001530 RID: 5424
		public static readonly BitSet tokenSet_51_ = new BitSet(TSql80ParserInternal.mk_tokenSet_51_());

		// Token: 0x04001531 RID: 5425
		public static readonly BitSet tokenSet_52_ = new BitSet(TSql80ParserInternal.mk_tokenSet_52_());

		// Token: 0x04001532 RID: 5426
		public static readonly BitSet tokenSet_53_ = new BitSet(TSql80ParserInternal.mk_tokenSet_53_());

		// Token: 0x04001533 RID: 5427
		public static readonly BitSet tokenSet_54_ = new BitSet(TSql80ParserInternal.mk_tokenSet_54_());

		// Token: 0x04001534 RID: 5428
		public static readonly BitSet tokenSet_55_ = new BitSet(TSql80ParserInternal.mk_tokenSet_55_());

		// Token: 0x04001535 RID: 5429
		public static readonly BitSet tokenSet_56_ = new BitSet(TSql80ParserInternal.mk_tokenSet_56_());

		// Token: 0x04001536 RID: 5430
		public static readonly BitSet tokenSet_57_ = new BitSet(TSql80ParserInternal.mk_tokenSet_57_());

		// Token: 0x04001537 RID: 5431
		public static readonly BitSet tokenSet_58_ = new BitSet(TSql80ParserInternal.mk_tokenSet_58_());

		// Token: 0x04001538 RID: 5432
		public static readonly BitSet tokenSet_59_ = new BitSet(TSql80ParserInternal.mk_tokenSet_59_());

		// Token: 0x04001539 RID: 5433
		public static readonly BitSet tokenSet_60_ = new BitSet(TSql80ParserInternal.mk_tokenSet_60_());

		// Token: 0x0400153A RID: 5434
		public static readonly BitSet tokenSet_61_ = new BitSet(TSql80ParserInternal.mk_tokenSet_61_());

		// Token: 0x0400153B RID: 5435
		public static readonly BitSet tokenSet_62_ = new BitSet(TSql80ParserInternal.mk_tokenSet_62_());

		// Token: 0x0400153C RID: 5436
		public static readonly BitSet tokenSet_63_ = new BitSet(TSql80ParserInternal.mk_tokenSet_63_());

		// Token: 0x0400153D RID: 5437
		public static readonly BitSet tokenSet_64_ = new BitSet(TSql80ParserInternal.mk_tokenSet_64_());

		// Token: 0x0400153E RID: 5438
		public static readonly BitSet tokenSet_65_ = new BitSet(TSql80ParserInternal.mk_tokenSet_65_());

		// Token: 0x0400153F RID: 5439
		public static readonly BitSet tokenSet_66_ = new BitSet(TSql80ParserInternal.mk_tokenSet_66_());

		// Token: 0x04001540 RID: 5440
		public static readonly BitSet tokenSet_67_ = new BitSet(TSql80ParserInternal.mk_tokenSet_67_());

		// Token: 0x04001541 RID: 5441
		public static readonly BitSet tokenSet_68_ = new BitSet(TSql80ParserInternal.mk_tokenSet_68_());

		// Token: 0x04001542 RID: 5442
		public static readonly BitSet tokenSet_69_ = new BitSet(TSql80ParserInternal.mk_tokenSet_69_());

		// Token: 0x04001543 RID: 5443
		public static readonly BitSet tokenSet_70_ = new BitSet(TSql80ParserInternal.mk_tokenSet_70_());

		// Token: 0x04001544 RID: 5444
		public static readonly BitSet tokenSet_71_ = new BitSet(TSql80ParserInternal.mk_tokenSet_71_());

		// Token: 0x04001545 RID: 5445
		public static readonly BitSet tokenSet_72_ = new BitSet(TSql80ParserInternal.mk_tokenSet_72_());

		// Token: 0x04001546 RID: 5446
		public static readonly BitSet tokenSet_73_ = new BitSet(TSql80ParserInternal.mk_tokenSet_73_());

		// Token: 0x04001547 RID: 5447
		public static readonly BitSet tokenSet_74_ = new BitSet(TSql80ParserInternal.mk_tokenSet_74_());

		// Token: 0x04001548 RID: 5448
		public static readonly BitSet tokenSet_75_ = new BitSet(TSql80ParserInternal.mk_tokenSet_75_());

		// Token: 0x04001549 RID: 5449
		public static readonly BitSet tokenSet_76_ = new BitSet(TSql80ParserInternal.mk_tokenSet_76_());

		// Token: 0x0400154A RID: 5450
		public static readonly BitSet tokenSet_77_ = new BitSet(TSql80ParserInternal.mk_tokenSet_77_());

		// Token: 0x0400154B RID: 5451
		public static readonly BitSet tokenSet_78_ = new BitSet(TSql80ParserInternal.mk_tokenSet_78_());

		// Token: 0x0400154C RID: 5452
		public static readonly BitSet tokenSet_79_ = new BitSet(TSql80ParserInternal.mk_tokenSet_79_());

		// Token: 0x0400154D RID: 5453
		public static readonly BitSet tokenSet_80_ = new BitSet(TSql80ParserInternal.mk_tokenSet_80_());

		// Token: 0x0400154E RID: 5454
		public static readonly BitSet tokenSet_81_ = new BitSet(TSql80ParserInternal.mk_tokenSet_81_());

		// Token: 0x0400154F RID: 5455
		public static readonly BitSet tokenSet_82_ = new BitSet(TSql80ParserInternal.mk_tokenSet_82_());

		// Token: 0x04001550 RID: 5456
		public static readonly BitSet tokenSet_83_ = new BitSet(TSql80ParserInternal.mk_tokenSet_83_());

		// Token: 0x04001551 RID: 5457
		public static readonly BitSet tokenSet_84_ = new BitSet(TSql80ParserInternal.mk_tokenSet_84_());

		// Token: 0x04001552 RID: 5458
		public static readonly BitSet tokenSet_85_ = new BitSet(TSql80ParserInternal.mk_tokenSet_85_());

		// Token: 0x04001553 RID: 5459
		public static readonly BitSet tokenSet_86_ = new BitSet(TSql80ParserInternal.mk_tokenSet_86_());

		// Token: 0x04001554 RID: 5460
		public static readonly BitSet tokenSet_87_ = new BitSet(TSql80ParserInternal.mk_tokenSet_87_());

		// Token: 0x04001555 RID: 5461
		public static readonly BitSet tokenSet_88_ = new BitSet(TSql80ParserInternal.mk_tokenSet_88_());
	}
}
