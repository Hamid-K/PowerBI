using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.RdlObjectModel.Expression;
using Microsoft.VisualBasic;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000230 RID: 560
	internal sealed class ExpressionParser
	{
		// Token: 0x060012D5 RID: 4821 RVA: 0x0002A566 File Offset: 0x00028766
		internal ExpressionParser(EnvironmentContext environment)
		{
			this.m_environment = environment;
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0002A575 File Offset: 0x00028775
		internal List<IInternalExpression> ObjectDependencyList
		{
			get
			{
				return this._ObjectDependencyList;
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0002A580 File Offset: 0x00028780
		internal IInternalExpression Parse(string expression)
		{
			this._ObjectDependencyList = new List<IInternalExpression>();
			if (expression != null)
			{
				expression.Trim();
			}
			if (expression.Trim() == "" || expression.Trim().Substring(0, 1) != "=")
			{
				return new ConstantNonExpression(expression);
			}
			IInternalExpression internalExpression = this.ParseExpr(new StringReader(expression));
			if (internalExpression == null)
			{
				internalExpression = new ConstantNonExpression(expression);
			}
			return internalExpression;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0002A5EC File Offset: 0x000287EC
		private IInternalExpression ParseExpr(TextReader reader)
		{
			IInternalExpression internalExpression = null;
			Lexer lexer = new Lexer(reader);
			this.tokens = lexer.Lex();
			if (this.tokens.Peek()._TokenType == TokenTypes.EQUAL)
			{
				this.GetNextToken();
				this.GetNextToken();
				if (this.curToken._TokenType == TokenTypes.EOF)
				{
					RDLExceptionHelper.WriteExpectedOperator("RDLEngine.Error.RDLObjects.Expression.ConstantOrIdentifier", this.curToken.ToString(), this.curToken.StartColumn, this.curToken.EndColumn);
				}
				this.MatchExprXor(out internalExpression);
			}
			if (this.curToken._TokenType != TokenTypes.EOF)
			{
				RDLExceptionHelper.WriteEndExpected(this.curToken.ToString(), this.curToken.StartColumn, this.curToken.EndColumn);
			}
			return internalExpression;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0002A6A6 File Offset: 0x000288A6
		private void MatchExprXor(out IInternalExpression result)
		{
			this.MatchExprXor(false, out result);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0002A6B0 File Offset: 0x000288B0
		private void MatchExprXor(bool inTypeOf, out IInternalExpression result)
		{
			bool inTypeOf2 = this._InTypeOf;
			this._InTypeOf = inTypeOf;
			IInternalExpression internalExpression;
			this.MatchExprOrOrElse(out internalExpression);
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.XOR)
			{
				this.GetNextToken();
				if (this.curToken._TokenType == TokenTypes.EOF)
				{
					RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", this.curToken.StartColumn, this.curToken.EndColumn);
				}
				IInternalExpression internalExpression2;
				this.MatchExprOrOrElse(out internalExpression2);
				if (internalExpression2 == null)
				{
					RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", this.curToken.StartColumn, this.curToken.EndColumn);
				}
				if (tokenType == TokenTypes.XOR)
				{
					result = new FunctionLogicXor(internalExpression, internalExpression2);
				}
				internalExpression = result;
			}
			this._InTypeOf = inTypeOf2;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0002A784 File Offset: 0x00028984
		private void MatchExprOrOrElse(out IInternalExpression result)
		{
			IInternalExpression internalExpression;
			this.MatchExprAndAndAlso(out internalExpression);
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.OR || tokenType == TokenTypes.ORELSE)
			{
				this.GetNextToken();
				if (this.curToken._TokenType == TokenTypes.EOF)
				{
					RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", this.curToken.StartColumn, this.curToken.EndColumn);
				}
				IInternalExpression internalExpression2;
				this.MatchExprAndAndAlso(out internalExpression2);
				if (internalExpression2 == null)
				{
					RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", this.curToken.StartColumn, this.curToken.EndColumn);
				}
				if (tokenType != TokenTypes.OR)
				{
					if (tokenType == TokenTypes.ORELSE)
					{
						result = new FunctionLogicOrElse(internalExpression, internalExpression2);
					}
				}
				else
				{
					result = new FunctionLogicOr(internalExpression, internalExpression2);
				}
				internalExpression = result;
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0002A85C File Offset: 0x00028A5C
		private void MatchExprAndAndAlso(out IInternalExpression result)
		{
			IInternalExpression internalExpression;
			this.MatchExprNot(out internalExpression);
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.AND || tokenType == TokenTypes.ANDALSO)
			{
				this.GetNextToken();
				if (this.curToken._TokenType == TokenTypes.EOF)
				{
					RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", this.curToken.StartColumn, this.curToken.EndColumn);
				}
				IInternalExpression internalExpression2;
				this.MatchExprNot(out internalExpression2);
				if (internalExpression2 == null)
				{
					RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", this.curToken.StartColumn, this.curToken.EndColumn);
				}
				if (tokenType != TokenTypes.AND)
				{
					if (tokenType == TokenTypes.ANDALSO)
					{
						result = new FunctionLogicAndAlso(internalExpression, internalExpression2);
					}
				}
				else
				{
					result = new FunctionLogicAnd(internalExpression, internalExpression2);
				}
				internalExpression = result;
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0002A92C File Offset: 0x00028B2C
		private void MatchExprNot(out IInternalExpression result)
		{
			TokenTypes tokenType = this.curToken._TokenType;
			if (this.curToken._TokenType == TokenTypes.EOF)
			{
				RDLExceptionHelper.WriteInvalidExpression(this.curToken.EndColumn);
			}
			if (tokenType == TokenTypes.NOT)
			{
				this.GetNextToken();
			}
			if (this.curToken._TokenType == TokenTypes.EOF)
			{
				RDLExceptionHelper.WriteExpectedOperand(tokenType.ToString(), "Boolean", this.curToken.StartColumn, this.curToken.EndColumn);
			}
			this.MatchExprRelational(out result);
			if (result == null)
			{
				RDLExceptionHelper.WriteExpectedOperand("NOT", "Boolean", this.curToken.StartColumn, this.curToken.EndColumn);
			}
			if (tokenType == TokenTypes.NOT)
			{
				result = new FunctionLogicNot(result);
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0002A9EC File Offset: 0x00028BEC
		private void MatchExprRelationalTypeOf(out IInternalExpression result)
		{
			if (this.curToken._TokenType == TokenTypes.TYPEOF)
			{
				this.GetNextToken();
				IInternalExpression internalExpression;
				this.MatchExprXor(true, out internalExpression);
				if (this.curToken._TokenType != TokenTypes.IS)
				{
					RDLExceptionHelper.WriteExpectedOperand("IS", "Type", this.prevToken.StartColumn, this.prevToken.EndColumn);
				}
				this.GetNextToken();
				IInternalExpression internalExpression2;
				if (!this.MatchTypeName(this.m_environment, out internalExpression2))
				{
					RDLExceptionHelper.WriteTypeNotFound(this.curToken._Value, this.curToken);
				}
				result = new FunctionRelationalTypeOf(internalExpression, (FunctionType)internalExpression2);
				return;
			}
			this.MatchExprNew(out result);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0002AA90 File Offset: 0x00028C90
		private void MatchExprRelational(out IInternalExpression result)
		{
			IInternalExpression internalExpression;
			this.MatchExprShift(out internalExpression);
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.EQUAL || tokenType == TokenTypes.NOTEQUAL || tokenType == TokenTypes.GREATERTHAN || tokenType == TokenTypes.GREATERTHANOREQUAL || tokenType == TokenTypes.LESSTHAN || tokenType == TokenTypes.LESSTHANOREQUAL || tokenType == TokenTypes.LIKE || tokenType == TokenTypes.IS || tokenType == TokenTypes.ISNOT)
			{
				if (tokenType == TokenTypes.IS && this._InTypeOf)
				{
					return;
				}
				this.GetNextToken();
				IInternalExpression internalExpression2;
				this.MatchExprShift(out internalExpression2);
				switch (tokenType)
				{
				case TokenTypes.EQUAL:
					result = new FunctionRelationalEqual(internalExpression, internalExpression2);
					break;
				case TokenTypes.EXP:
				case TokenTypes.FALSE:
				case TokenTypes.FORWARDSLASH:
				case TokenTypes.GETTYPE:
				case TokenTypes.IDENTIFIER:
				case TokenTypes.INTEGER:
				case TokenTypes.LCURLY:
					break;
				case TokenTypes.GREATERTHAN:
					result = new FunctionRelationalGreaterThan(internalExpression, internalExpression2);
					break;
				case TokenTypes.GREATERTHANOREQUAL:
					result = new FunctionRelationalGreaterThanEqual(internalExpression, internalExpression2);
					break;
				case TokenTypes.IS:
					result = new FunctionRelationalIs(internalExpression, internalExpression2);
					break;
				case TokenTypes.ISNOT:
					result = new FunctionRelationalIsNot(internalExpression, internalExpression2);
					break;
				case TokenTypes.LESSTHAN:
					result = new FunctionRelationalLessThan(internalExpression, internalExpression2);
					break;
				case TokenTypes.LESSTHANOREQUAL:
					result = new FunctionRelationalLessThanEqual(internalExpression, internalExpression2);
					break;
				case TokenTypes.LIKE:
					result = new FunctionRelationalLike(internalExpression, internalExpression2);
					break;
				default:
					if (tokenType == TokenTypes.NOTEQUAL)
					{
						result = new FunctionRelationalNotEqual(internalExpression, internalExpression2);
					}
					break;
				}
				internalExpression = result;
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0002ABD0 File Offset: 0x00028DD0
		private void MatchExprShift(out IInternalExpression result)
		{
			ExpressionToken expressionToken = this.curToken;
			IInternalExpression internalExpression;
			this.MatchExprConcatenate(out internalExpression);
			ExpressionToken expressionToken2 = this.curToken;
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.SHIFTLEFT || tokenType == TokenTypes.SHIFTRIGHT)
			{
				this.GetNextToken();
				IInternalExpression internalExpression2;
				this.MatchExprConcatenate(out internalExpression2);
				if (tokenType != TokenTypes.SHIFTLEFT)
				{
					if (tokenType == TokenTypes.SHIFTRIGHT)
					{
						result = new FunctionShiftRight(internalExpression, internalExpression2);
					}
				}
				else
				{
					result = new FunctionShiftLeft(internalExpression, internalExpression2);
				}
				internalExpression = result;
			}
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0002AC44 File Offset: 0x00028E44
		private void MatchExprConcatenate(out IInternalExpression result)
		{
			IInternalExpression internalExpression;
			this.MatchExprAddSub(out internalExpression);
			ExpressionToken expressionToken = this.curToken;
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.CONCATENATE)
			{
				this.GetNextToken();
				IInternalExpression internalExpression2;
				this.MatchExprAddSub(out internalExpression2);
				if (tokenType == TokenTypes.CONCATENATE)
				{
					result = new FunctionConcatenate(internalExpression, internalExpression2, TokenTypes.CONCATENATE);
				}
				internalExpression = result;
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0002AC94 File Offset: 0x00028E94
		private void MatchExprAddSub(out IInternalExpression result)
		{
			ExpressionToken expressionToken = this.curToken;
			IInternalExpression internalExpression;
			this.MatchExprMod(out internalExpression);
			ExpressionToken expressionToken2 = this.curToken;
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.PLUS || tokenType == TokenTypes.MINUS)
			{
				this.GetNextToken();
				IInternalExpression internalExpression2;
				this.MatchExprMod(out internalExpression2);
				if (tokenType != TokenTypes.MINUS)
				{
					if (tokenType == TokenTypes.PLUS)
					{
						result = new FunctionPlus(internalExpression, internalExpression2);
					}
				}
				else
				{
					result = new FunctionMinus(internalExpression, internalExpression2);
				}
				internalExpression = result;
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0002AD04 File Offset: 0x00028F04
		private void MatchExprMod(out IInternalExpression result)
		{
			ExpressionToken expressionToken = this.curToken;
			IInternalExpression internalExpression;
			this.MatchExprIntDiv(out internalExpression);
			ExpressionToken expressionToken2 = this.curToken;
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.MODULUS)
			{
				this.GetNextToken();
				IInternalExpression internalExpression2;
				this.MatchExprIntDiv(out internalExpression2);
				if (tokenType == TokenTypes.MODULUS)
				{
					result = new FunctionModulus(internalExpression, internalExpression2);
				}
				internalExpression = result;
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0002AD5C File Offset: 0x00028F5C
		private void MatchExprIntDiv(out IInternalExpression result)
		{
			ExpressionToken expressionToken = this.curToken;
			IInternalExpression internalExpression;
			this.MatchExprMultDiv(out internalExpression);
			ExpressionToken expressionToken2 = this.curToken;
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.BACKWARDSLASH)
			{
				this.GetNextToken();
				IInternalExpression internalExpression2;
				this.MatchExprMultDiv(out internalExpression2);
				if (tokenType == TokenTypes.BACKWARDSLASH)
				{
					result = new FunctionIntDiv(internalExpression, internalExpression2);
				}
				internalExpression = result;
			}
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		private void MatchExprMultDiv(out IInternalExpression result)
		{
			ExpressionToken expressionToken = this.curToken;
			IInternalExpression internalExpression;
			this.MatchExprUnary(out internalExpression);
			ExpressionToken expressionToken2 = this.curToken;
			result = internalExpression;
			TokenTypes tokenType;
			while ((tokenType = this.curToken._TokenType) == TokenTypes.FORWARDSLASH || tokenType == TokenTypes.STAR)
			{
				this.GetNextToken();
				IInternalExpression internalExpression2;
				this.MatchExprUnary(out internalExpression2);
				if (tokenType != TokenTypes.FORWARDSLASH)
				{
					if (tokenType == TokenTypes.STAR)
					{
						result = new FunctionMult(internalExpression, internalExpression2);
					}
				}
				else
				{
					result = new FunctionDiv(internalExpression, internalExpression2);
				}
				internalExpression = result;
			}
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0002AE24 File Offset: 0x00029024
		private void MatchExprUnary(out IInternalExpression result)
		{
			TokenTypes tokenType = this.curToken._TokenType;
			if (tokenType == TokenTypes.PLUS || tokenType == TokenTypes.MINUS)
			{
				this.GetNextToken();
			}
			if (this.curToken._TokenType == TokenTypes.PLUS || this.curToken._TokenType == TokenTypes.MINUS)
			{
				this.MatchExprUnary(out result);
			}
			else
			{
				this.MatchExprExponent(out result);
			}
			if (tokenType == TokenTypes.MINUS)
			{
				result = new FunctionUnaryMinus(result);
				return;
			}
			if (tokenType == TokenTypes.PLUS)
			{
				result = new FunctionUnaryPlus(result);
			}
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0002AE98 File Offset: 0x00029098
		private void MatchExprExponent(out IInternalExpression result)
		{
			ExpressionToken expressionToken = this.curToken;
			IInternalExpression internalExpression;
			this.MatchExprMethodCall(out internalExpression);
			ExpressionToken expressionToken2 = this.curToken;
			if (this.curToken._TokenType == TokenTypes.EXP)
			{
				this.GetNextToken();
				IInternalExpression internalExpression2;
				this.MatchExprUnary(out internalExpression2);
				result = new FunctionExp(internalExpression, internalExpression2);
				return;
			}
			result = internalExpression;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0002AEE8 File Offset: 0x000290E8
		private void MatchExprMethodCall(out IInternalExpression result)
		{
			this.MatchExprRelationalTypeOf(out result);
			for (;;)
			{
				TokenTypes tokenType = this.curToken._TokenType;
				if (tokenType != TokenTypes.BANG)
				{
					if (tokenType != TokenTypes.LPAREN)
					{
						if (tokenType != TokenTypes.PERIOD)
						{
							break;
						}
						this.GetNextToken();
						result = this.MatchObjectMethod(result);
					}
					else
					{
						List<IInternalExpression> list;
						if (!this.MatchFunctionArgs(out list))
						{
							RDLExceptionHelper.WriteMissingArgumentsForExpression(this.curToken);
						}
						result = new FunctionDefaultPropertyOrIndexer(result, list);
					}
				}
				else
				{
					this.GetNextToken();
					if (this.curToken._TokenType != TokenTypes.IDENTIFIER)
					{
						RDLExceptionHelper.WriteMissingIdentifierForDictionaryOperator(this.curToken);
					}
					result = new FunctionDictionaryAccessor(result, this.curToken._Value);
					this.GetNextToken();
				}
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0002AF88 File Offset: 0x00029188
		private void MatchExprNew(out IInternalExpression result)
		{
			if (this.curToken._TokenType != TokenTypes.NEW)
			{
				this.MatchExprParen(out result);
				return;
			}
			this.GetNextToken();
			IInternalExpression internalExpression;
			if (!this.MatchTypeName(this.m_environment, out internalExpression))
			{
				RDLExceptionHelper.WriteTypeNotFound(this.curToken._Value, this.curToken);
			}
			TypeContext typeContext = ((FunctionType)internalExpression).TypeContext;
			if (this.curToken._TokenType == TokenTypes.PERIOD)
			{
				RDLExceptionHelper.WriteTypeNotFound(((FunctionType)internalExpression).TypeContext.Name + "." + this.curToken._Value, this.curToken);
			}
			bool flag = false;
			int num = 0;
			List<IInternalExpression> list = new List<IInternalExpression>();
			if (this.curToken._TokenType == TokenTypes.LPAREN)
			{
				this.GetNextToken();
				while (this.curToken._TokenType != TokenTypes.RPAREN)
				{
					num++;
					if (this.curToken._TokenType == TokenTypes.COMMA)
					{
						this.GetNextToken();
						if (list.Count == 0)
						{
							flag = true;
							continue;
						}
					}
					else if (list != null && list.Count > 0)
					{
						RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.COMMA, this.curToken);
					}
					if (this.curToken._TokenType == TokenTypes.INTEGER && this.tokens.Peek()._TokenType == TokenTypes.IDENTIFIER && StringUtil.EqualsIgnoreCase(this.tokens.Peek()._Value, "To"))
					{
						this.GetNextToken();
						this.GetNextToken();
						flag = true;
					}
					IInternalExpression internalExpression2;
					this.MatchExprXor(out internalExpression2);
					list.Add(internalExpression2);
				}
				this.ConsumeExpectedToken(TokenTypes.RPAREN);
			}
			while (this.curToken._TokenType == TokenTypes.LPAREN && (this.tokens.Peek()._TokenType == TokenTypes.RPAREN || this.tokens.Peek()._TokenType == TokenTypes.COMMA))
			{
				internalExpression = new FunctionArrayType(internalExpression, num);
				flag = true;
				num = 1;
				this.GetNextToken();
				while (this.curToken._TokenType == TokenTypes.COMMA)
				{
					num++;
					this.GetNextToken();
				}
				this.ConsumeExpectedToken(TokenTypes.RPAREN);
			}
			FunctionArrayInit functionArrayInit;
			if (this.MatchArrayInitExpr(out functionArrayInit))
			{
				flag = true;
			}
			else if (flag)
			{
				RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.LCURLY, this.curToken);
			}
			if (flag)
			{
				if (!typeContext.AllowNewArray)
				{
					RDLExceptionHelper.WriteInvalidArrayType(typeContext.Name, this.curToken);
				}
				FunctionArrayType functionArrayType = new FunctionArrayType(internalExpression, num);
				result = new FunctionNewArray(functionArrayType, functionArrayInit);
				return;
			}
			if (!typeContext.AllowNew)
			{
				RDLExceptionHelper.WriteInvalidNewType(typeContext.Name, this.curToken);
			}
			result = new FunctionNewObject((FunctionType)internalExpression, list);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0002B1E8 File Offset: 0x000293E8
		private bool MatchArrayInitExpr(out FunctionArrayInit result)
		{
			if (this.curToken._TokenType == TokenTypes.LCURLY)
			{
				this.GetNextToken();
				List<IInternalExpression> list = new List<IInternalExpression>();
				while (this.curToken._TokenType != TokenTypes.RCURLY)
				{
					if (list.Count > 0)
					{
						this.ConsumeExpectedToken(TokenTypes.COMMA);
					}
					FunctionArrayInit functionArrayInit;
					if (this.MatchArrayInitExpr(out functionArrayInit))
					{
						list.Add(functionArrayInit);
					}
					else
					{
						IInternalExpression internalExpression;
						this.MatchExprXor(out internalExpression);
						list.Add(internalExpression);
					}
				}
				this.ConsumeExpectedToken(TokenTypes.RCURLY);
				result = new FunctionArrayInit(list);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0002B269 File Offset: 0x00029469
		private void MatchExprParen(out IInternalExpression result)
		{
			if (this.curToken._TokenType == TokenTypes.LPAREN)
			{
				this.GetNextToken();
				this.MatchExprXor(out result);
				this.ConsumeExpectedToken(TokenTypes.RPAREN);
				result.Bracketed = true;
				return;
			}
			this.MatchBaseDataType(out result);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0002B2A0 File Offset: 0x000294A0
		private void MatchBaseDataType(out IInternalExpression result)
		{
			if (this.MatchIdentifierOrFunction(out result))
			{
				return;
			}
			try
			{
				TokenTypes tokenType = this.curToken._TokenType;
				if (tokenType <= TokenTypes.IDENTIFIER)
				{
					switch (tokenType)
					{
					case TokenTypes.CHAR:
						result = new ConstantChar(this.curToken._Value);
						goto IL_0345;
					case TokenTypes.COMMA:
					case TokenTypes.DISPLAYIDENTIFIER:
					case TokenTypes.EOF:
					case TokenTypes.EQUAL:
					case TokenTypes.EXP:
						goto IL_02F3;
					case TokenTypes.CONCATENATE:
					{
						ExpressionToken expressionToken = this.tokens.Peek();
						if (expressionToken._TokenType == TokenTypes.IDENTIFIER && expressionToken._Value.Length > 1)
						{
							string text = expressionToken._Value.ToUpperInvariant();
							if (text[0] == 'H')
							{
								int num;
								if (!int.TryParse(text.Substring(1), NumberStyles.AllowHexSpecifier, RDLUtil.GetFormatProvider(false), out num))
								{
									goto IL_02F3;
								}
								result = new ConstantInteger(num);
							}
							else
							{
								if (text[0] != 'O')
								{
									goto IL_02F3;
								}
								for (int i = 1; i < text.Length; i++)
								{
									char c = text[i];
									if (c < '0' || c > '7')
									{
										goto IL_02F3;
									}
								}
								double num2 = Conversion.Val("&" + text);
								if (num2 > 2147483647.0)
								{
									goto IL_02F3;
								}
								result = new ConstantInteger((int)num2);
							}
							this.GetNextToken();
							goto IL_0345;
						}
						goto IL_02F3;
					}
					case TokenTypes.DATETIME:
						try
						{
							result = new ConstantDateTime(this.curToken._Value);
							goto IL_0345;
						}
						catch (FormatException)
						{
							RDLExceptionHelper.WriteInvalidDateTimeLiteral(this.curToken);
							goto IL_0345;
						}
						break;
					case TokenTypes.DECIMAL:
						result = new ConstantDecimal(this.curToken._Value);
						goto IL_0345;
					case TokenTypes.DOUBLE:
						break;
					case TokenTypes.FALSE:
						result = new ConstantBoolean(this.curToken._Value);
						goto IL_0345;
					default:
						if (tokenType != TokenTypes.IDENTIFIER)
						{
							goto IL_02F3;
						}
						if (StringUtil.EqualsIgnoreCase(this.curToken._Value, "Nothing"))
						{
							result = new FunctionNothing();
							goto IL_0345;
						}
						goto IL_02F3;
					}
					result = new ConstantDouble(this.curToken._Value);
					goto IL_0345;
				}
				if (tokenType == TokenTypes.INTEGER)
				{
					result = new ConstantInteger(this.curToken._Value);
					goto IL_0345;
				}
				if (tokenType == TokenTypes.LONG)
				{
					result = new ConstantLong(this.curToken._Value);
					goto IL_0345;
				}
				switch (tokenType)
				{
				case TokenTypes.QUOTE:
					result = new ConstantString(this.curToken._Value);
					goto IL_0345;
				case TokenTypes.SHORT:
					result = new ConstantShort(this.curToken._Value);
					goto IL_0345;
				case TokenTypes.SINGLE:
					result = new ConstantSingle(this.curToken._Value);
					goto IL_0345;
				case TokenTypes.TRUE:
					result = new ConstantBoolean(this.curToken._Value);
					goto IL_0345;
				case TokenTypes.UINTEGER:
					result = new ConstantLong(this.curToken._Value);
					goto IL_0345;
				case TokenTypes.ULONG:
					result = new ConstantDecimal(this.curToken._Value);
					goto IL_0345;
				case TokenTypes.USHORT:
					result = new ConstantInteger(this.curToken._Value);
					goto IL_0345;
				}
				IL_02F3:
				if (this.curToken._TokenType == TokenTypes.IDENTIFIER)
				{
					RDLExceptionHelper.WriteUnknownIdentifier(this.curToken._Value, this.curToken);
				}
				else
				{
					RDLExceptionHelper.WriteExpectedOperator("RDLEngine.Error.RDLObjects.Expression.ConstantOrIdentifier", this.curToken.ToString(), this.curToken.StartColumn, this.curToken.EndColumn);
				}
				IL_0345:;
			}
			catch (OverflowException)
			{
				RDLExceptionHelper.WriteOverflow("Integer", this.curToken._Value, this.curToken.StartColumn, this.curToken.EndColumn);
			}
			this.GetNextToken();
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0002B65C File Offset: 0x0002985C
		private bool MatchIdentifierOrFunction(out IInternalExpression result)
		{
			if (!this.MatchRdlFunctionOrCollection(out result))
			{
				LookupContext environment = this.m_environment;
				if (!this.MatchTypeName(environment, out result))
				{
					return this.MatchMemberAccessor(environment, null, out result);
				}
			}
			return true;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0002B690 File Offset: 0x00029890
		private bool MatchRdlFunctionOrCollection(out IInternalExpression result)
		{
			result = null;
			if (this.curToken._TokenType != TokenTypes.IDENTIFIER)
			{
				return false;
			}
			string value = this.curToken._Value;
			ReportObjectModelContext reportObjectModelContext = this.m_environment.ReportObjectModelContext;
			RdlFunctionDefinition rdlFunctionDefinition;
			if (reportObjectModelContext.TryMatchRdlFunction(value, out rdlFunctionDefinition))
			{
				this.GetNextToken();
				List<IInternalExpression> list = this.MatchRdlFunctionArgs(reportObjectModelContext, rdlFunctionDefinition);
				object obj = Activator.CreateInstance(rdlFunctionDefinition.NodeType, new object[] { list });
				result = (IInternalExpression)obj;
				return true;
			}
			string text = value.ToUpperInvariant();
			if (text != null)
			{
				switch (text.Length)
				{
				case 4:
					if (text == "USER")
					{
						result = this.MatchSimpleRdlCollection(reportObjectModelContext.User);
						return true;
					}
					break;
				case 6:
					if (text == "FIELDS")
					{
						result = this.MatchComplexRdlCollection(reportObjectModelContext.Fields, true);
						if (result != null)
						{
							this._ObjectDependencyList.Add(result);
						}
						return true;
					}
					break;
				case 7:
					if (text == "GLOBALS")
					{
						result = this.MatchSimpleRdlCollection(reportObjectModelContext.Globals);
						return true;
					}
					break;
				case 8:
					if (text == "DATASETS")
					{
						result = this.MatchComplexRdlCollection(reportObjectModelContext.DataSets, false);
						if (result != null)
						{
							this._ObjectDependencyList.Add(result);
						}
						return true;
					}
					break;
				case 9:
					if (text == "VARIABLES")
					{
						result = this.MatchComplexRdlCollection(reportObjectModelContext.Variables, false);
						return true;
					}
					break;
				case 10:
					if (text == "PARAMETERS")
					{
						result = this.MatchComplexRdlCollection(reportObjectModelContext.Parameters, false);
						if (result != null)
						{
							this._ObjectDependencyList.Add(result);
						}
						return true;
					}
					break;
				case 11:
				{
					char c = text[0];
					if (c != 'D')
					{
						if (c == 'R')
						{
							if (text == "REPORTITEMS")
							{
								result = this.MatchComplexRdlCollection(reportObjectModelContext.ReportItems, false);
								if (result != null)
								{
									this._ObjectDependencyList.Add(result);
								}
								return true;
							}
						}
					}
					else if (text == "DATASOURCES")
					{
						result = this.MatchComplexRdlCollection(reportObjectModelContext.DataSources, false);
						if (result != null)
						{
							this._ObjectDependencyList.Add(result);
						}
						return true;
					}
					break;
				}
				}
			}
			return false;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0002B8F4 File Offset: 0x00029AF4
		private IInternalExpression MatchSimpleRdlCollection(ISimpleRdlCollection itemCollection)
		{
			this.GetNextToken();
			IInternalExpression internalExpression;
			IInternalExpression internalExpression2;
			if (this.MatchCollectionAccessor("item", out internalExpression))
			{
				internalExpression2 = itemCollection.CreateCollectionReference(internalExpression);
			}
			else if (this.curToken._TokenType == TokenTypes.PERIOD && this.tokens.Peek()._TokenType == TokenTypes.IDENTIFIER)
			{
				this.GetNextToken();
				string value = this.curToken._Value;
				ISimpleRdlCollection simpleRdlCollection;
				if (itemCollection.IsPredefinedChildCollection(value, out simpleRdlCollection))
				{
					internalExpression2 = this.MatchSimpleRdlCollection(simpleRdlCollection);
				}
				else if (itemCollection.IsPredefinedCollectionProperty(value))
				{
					internalExpression2 = itemCollection.CreatePropertyReference(value);
					this.GetNextToken();
				}
				else
				{
					RDLExceptionHelper.WriteMethodOrPropertyNotFound(this.curToken, itemCollection.Name);
					internalExpression2 = null;
					this.GetNextToken();
				}
			}
			else
			{
				internalExpression2 = itemCollection.CreateReference();
			}
			return internalExpression2;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0002B9AC File Offset: 0x00029BAC
		private IInternalExpression MatchComplexRdlCollection(IComplexRdlCollection itemCollection, bool allowLevelTwoCollection)
		{
			this.GetNextToken();
			IInternalExpression internalExpression;
			IInternalExpression internalExpression3;
			if (this.MatchCollectionAccessor("item", out internalExpression))
			{
				IInternalExpression internalExpression2;
				if (allowLevelTwoCollection && this.MatchCollectionAccessor("properties", out internalExpression2))
				{
					internalExpression3 = itemCollection.CreateReference(internalExpression, internalExpression2);
				}
				else if (this.curToken._TokenType == TokenTypes.PERIOD && this.tokens.Peek()._TokenType == TokenTypes.IDENTIFIER)
				{
					this.GetNextToken();
					if (itemCollection.IsPredefinedItemProperty(this.curToken._Value))
					{
						internalExpression3 = itemCollection.CreateReference(internalExpression, new ConstantString(this.curToken._Value));
						this.GetNextToken();
					}
					else if (itemCollection.IsPredefinedItemMethod(this.curToken._Value))
					{
						string value = this.curToken._Value;
						this.GetNextToken();
						List<IInternalExpression> list;
						if (!this.MatchFunctionArgs(out list))
						{
							RDLExceptionHelper.WriteMissingArgumentsForExpression(this.curToken);
							internalExpression3 = null;
						}
						else
						{
							internalExpression3 = new FunctionMethodOrProperty(new FunctionVariable(internalExpression), value, list);
						}
					}
					else
					{
						RDLExceptionHelper.WriteMethodOrPropertyNotFound(this.curToken, itemCollection.ItemName);
						internalExpression3 = null;
						this.GetNextToken();
					}
				}
				else
				{
					internalExpression3 = itemCollection.CreateReference(internalExpression);
				}
			}
			else
			{
				if (this.curToken._TokenType == TokenTypes.PERIOD && this.tokens.Peek()._TokenType == TokenTypes.IDENTIFIER)
				{
					this.GetNextToken();
					if (itemCollection.IsPredefinedCollectionProperty(this.curToken._Value))
					{
						internalExpression3 = itemCollection.CreateReference(internalExpression, new ConstantString(this.curToken._Value));
					}
					else
					{
						RDLExceptionHelper.WriteMethodOrPropertyNotFound(this.curToken, itemCollection.Name);
					}
					this.GetNextToken();
				}
				internalExpression3 = itemCollection.CreateReference();
			}
			return internalExpression3;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0002BB4C File Offset: 0x00029D4C
		private bool MatchCollectionAccessor(string indexerPropertyName, out IInternalExpression itemNameExpr)
		{
			itemNameExpr = null;
			TokenTypes tokenType = this.curToken._TokenType;
			if (tokenType == TokenTypes.BANG)
			{
				this.GetNextToken();
				if (this.curToken._TokenType != TokenTypes.IDENTIFIER)
				{
					RDLExceptionHelper.WriteMissingIdentifierForDictionaryOperator(this.curToken);
				}
				itemNameExpr = new ConstantString(this.curToken._Value);
				this.GetNextToken();
				return true;
			}
			if (tokenType != TokenTypes.LPAREN)
			{
				if (tokenType == TokenTypes.PERIOD)
				{
					ExpressionToken expressionToken = this.tokens.Peek();
					if (expressionToken._TokenType == TokenTypes.IDENTIFIER && StringUtil.EqualsIgnoreCase(expressionToken._Value, indexerPropertyName))
					{
						this.GetNextToken();
						string value = this.curToken._Value;
						this.GetNextToken();
						this.ConsumeExpectedToken(TokenTypes.LPAREN);
						this.MatchExprXor(out itemNameExpr);
						this.ConsumeExpectedToken(TokenTypes.RPAREN);
						return true;
					}
				}
				return false;
			}
			this.GetNextToken();
			this.MatchExprXor(out itemNameExpr);
			this.ConsumeExpectedToken(TokenTypes.RPAREN);
			return true;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0002BC28 File Offset: 0x00029E28
		private bool MatchTypeName(LookupContext context, out IInternalExpression result)
		{
			result = null;
			if (this.curToken._TokenType != TokenTypes.IDENTIFIER)
			{
				return false;
			}
			string text = this.curToken._Value;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			LookupContext lookupContext;
			while (context.TryMatchSubContext(text, out lookupContext))
			{
				context = lookupContext;
				if (flag)
				{
					this.GetNextToken();
					stringBuilder.Append(".");
				}
				stringBuilder.Append(text);
				flag = true;
				this.GetNextToken();
				if (this.curToken._TokenType == TokenTypes.PERIOD)
				{
					ExpressionToken expressionToken = this.tokens.Peek();
					if (expressionToken._TokenType != TokenTypes.IDENTIFIER)
					{
						RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.IDENTIFIER, this.curToken);
					}
					text = expressionToken._Value;
				}
			}
			if (flag)
			{
				if (!(context is TypeContext))
				{
					stringBuilder.Append(".");
					stringBuilder.Append(text);
					RDLExceptionHelper.WriteTypeNotFound(stringBuilder.ToString(), this.curToken);
				}
				result = new FunctionType((TypeContext)context);
				return true;
			}
			return false;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0002BD0C File Offset: 0x00029F0C
		private bool MatchMemberAccessor(LookupContext context, IInternalExpression callTarget, out IInternalExpression result)
		{
			result = null;
			if (this.curToken._TokenType != TokenTypes.IDENTIFIER)
			{
				return false;
			}
			string value = this.curToken._Value;
			MemberContext memberContext;
			if (context.TryMatchMember(value, out memberContext))
			{
				this.GetNextToken();
				if (callTarget == null)
				{
					callTarget = new FunctionType(memberContext.OwningType);
				}
				switch (memberContext.MemberContextType)
				{
				case MemberContext.MemberContextTypes.Method:
				{
					List<IInternalExpression> list;
					this.MatchFunctionArgs(out list);
					result = new FunctionMethodOrProperty(callTarget, value, list);
					if (value.ToUpperInvariant() == "CODE")
					{
						this._ObjectDependencyList.Add(result);
					}
					break;
				}
				case MemberContext.MemberContextTypes.Field:
					result = new FunctionMemberField(callTarget, value);
					if (value.ToUpperInvariant() == "CODE")
					{
						this._ObjectDependencyList.Add(result);
					}
					break;
				case MemberContext.MemberContextTypes.Property:
				{
					List<IInternalExpression> list2;
					this.MatchFunctionArgs(out list2);
					result = new FunctionMethodOrProperty(callTarget, value, list2);
					if (value.ToUpperInvariant() == "CODE")
					{
						this._ObjectDependencyList.Add(result);
					}
					break;
				}
				case MemberContext.MemberContextTypes.Unknown:
				{
					List<IInternalExpression> list3;
					this.MatchFunctionArgs(out list3);
					result = new FunctionLateBoundAccessor(callTarget, value, list3);
					break;
				}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0002BE2C File Offset: 0x0002A02C
		private List<IInternalExpression> MatchRdlFunctionArgs(ReportObjectModelContext context, RdlFunctionDefinition funcDef)
		{
			this.ConsumeExpectedToken(TokenTypes.LPAREN);
			List<IInternalExpression> list = new List<IInternalExpression>(funcDef.Args.Length);
			for (int i = 0; i < funcDef.Args.Length; i++)
			{
				RdlFunctionArg rdlFunctionArg = funcDef.Args[i];
				if (this.curToken._TokenType == TokenTypes.RPAREN)
				{
					if (!rdlFunctionArg.IsRequired)
					{
						break;
					}
					RDLExceptionHelper.WriteBadSyntax(funcDef.Name, this.curToken.ToString(), this.curToken.StartColumn, this.curToken.EndColumn);
				}
				if (list.Count > 0)
				{
					this.ConsumeExpectedToken(TokenTypes.COMMA);
				}
				IInternalExpression internalExpression = null;
				switch (rdlFunctionArg.ArgType)
				{
				case RdlArgTypes.Scope:
				{
					TokenTypes tokenType = this.curToken._TokenType;
					if (tokenType != TokenTypes.IDENTIFIER)
					{
						if (tokenType == TokenTypes.QUOTE)
						{
							internalExpression = new ConstantString(this.curToken._Value);
							this._ObjectDependencyList.Add(internalExpression);
							this.GetNextToken();
						}
						else
						{
							RDLExceptionHelper.WriteBadSyntax(funcDef.Name, this.curToken);
						}
					}
					else
					{
						if (!StringUtil.EqualsIgnoreCase("Nothing", this.curToken._Value))
						{
							RDLExceptionHelper.WriteBadSyntax(funcDef.Name, this.curToken);
						}
						internalExpression = new ConstantString(this.curToken._Value);
						this.GetNextToken();
					}
					break;
				}
				case RdlArgTypes.Recursive:
				{
					if (this.curToken._TokenType != TokenTypes.IDENTIFIER)
					{
						RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.IDENTIFIER, this.curToken);
					}
					string value = this.curToken._Value;
					if (StringUtil.EqualsIgnoreCase("Recursive", value))
					{
						internalExpression = new Recursive(RecursiveOption.Recursive);
					}
					else if (StringUtil.EqualsIgnoreCase("Simple", value))
					{
						internalExpression = new Recursive(RecursiveOption.Simple);
					}
					else
					{
						RDLExceptionHelper.WriteBadSyntax(funcDef.Name, this.curToken);
					}
					this.GetNextToken();
					break;
				}
				case RdlArgTypes.AggregateFunction:
				{
					if (this.curToken._TokenType != TokenTypes.IDENTIFIER)
					{
						RDLExceptionHelper.WriteUnexpectedToken(TokenTypes.IDENTIFIER, this.curToken);
					}
					RdlFunctionDefinition rdlFunctionDefinition;
					if (!context.TryMatchRdlFunction(this.curToken._Value, out rdlFunctionDefinition))
					{
						RDLExceptionHelper.WriteBadSyntax(funcDef.Name, this.curToken);
					}
					internalExpression = new Identifier(this.curToken._Value);
					this.GetNextToken();
					break;
				}
				default:
					this.MatchExprXor(out internalExpression);
					break;
				}
				list.Add(internalExpression);
				if (rdlFunctionArg.IsVarArg)
				{
					i--;
				}
			}
			this.ConsumeExpectedToken(TokenTypes.RPAREN);
			return list;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0002C078 File Offset: 0x0002A278
		private bool MatchFunctionArgs(out List<IInternalExpression> args)
		{
			args = null;
			if (this.curToken._TokenType != TokenTypes.LPAREN)
			{
				return false;
			}
			this.GetNextToken();
			int num = 0;
			args = new List<IInternalExpression>();
			while (this.curToken._TokenType != TokenTypes.RPAREN)
			{
				if (num != 0)
				{
					if (this.curToken._TokenType == TokenTypes.COMMA)
					{
						this.GetNextToken();
					}
					else
					{
						RDLExceptionHelper.WriteInvalidFunction(this.curToken.ToString(), this.curToken.StartColumn, this.curToken.EndColumn);
					}
				}
				IInternalExpression internalExpression = null;
				this.MatchExprXor(out internalExpression);
				if (internalExpression == null)
				{
					RDLExceptionHelper.WriteExpectedOperator("',' or ')'", this.curToken.ToString(), this.curToken.StartColumn, this.curToken.EndColumn);
				}
				args.Add(internalExpression);
				num++;
			}
			this.GetNextToken();
			return true;
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0002C14C File Offset: 0x0002A34C
		private bool MatchDefaultPropertyOrIndexer(IInternalExpression callTarget, out IInternalExpression result)
		{
			List<IInternalExpression> list;
			if (this.MatchFunctionArgs(out list))
			{
				result = new FunctionDefaultPropertyOrIndexer(callTarget, list);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0002C174 File Offset: 0x0002A374
		private IInternalExpression MatchObjectMethod(IInternalExpression prevExpression)
		{
			LookupContext lookupContext = null;
			string text = "";
			if (prevExpression is FunctionType)
			{
				text = (lookupContext = ((FunctionType)prevExpression).TypeContext).Name;
			}
			if (lookupContext == null)
			{
				lookupContext = this.m_environment.LateBoundContext;
			}
			IInternalExpression internalExpression;
			if (!this.MatchMemberAccessor(lookupContext, prevExpression, out internalExpression))
			{
				if (this.curToken._TokenType != TokenTypes.EOF)
				{
					RDLExceptionHelper.WriteMethodOrPropertyNotFound(this.curToken._Value, text, this.prevToken.StartColumn - 2, this.prevToken.EndColumn - 2);
				}
				else
				{
					RDLExceptionHelper.WriteMethodOrPropertyExpected(this.curToken._Value, text, this.curToken.StartColumn, this.curToken.EndColumn);
				}
			}
			return internalExpression;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0002C223 File Offset: 0x0002A423
		private void GetNextToken()
		{
			this.olderToken = this.prevToken;
			this.prevToken = this.curToken;
			this.curToken = this.tokens.Extract();
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0002C24E File Offset: 0x0002A44E
		private void ConsumeExpectedToken(TokenTypes tokenType)
		{
			if (this.curToken._TokenType != tokenType)
			{
				RDLExceptionHelper.WriteUnexpectedToken(tokenType, this.curToken);
			}
			this.GetNextToken();
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0002C270 File Offset: 0x0002A470
		private ExpressionToken FindOldTokenByType(TokenTypes tokenType)
		{
			if (this.olderToken != null && this.olderToken._TokenType == tokenType)
			{
				return this.olderToken;
			}
			if (this.prevToken == null || this.prevToken._TokenType != tokenType)
			{
				return this.curToken;
			}
			return this.prevToken;
		}

		// Token: 0x040005F9 RID: 1529
		private static List<Type> TypeList = new List<Type>();

		// Token: 0x040005FA RID: 1530
		private List<IInternalExpression> _ObjectDependencyList;

		// Token: 0x040005FB RID: 1531
		private TokenList tokens;

		// Token: 0x040005FC RID: 1532
		private ExpressionToken curToken;

		// Token: 0x040005FD RID: 1533
		private ExpressionToken prevToken;

		// Token: 0x040005FE RID: 1534
		private ExpressionToken olderToken;

		// Token: 0x040005FF RID: 1535
		private bool _InTypeOf;

		// Token: 0x04000600 RID: 1536
		private readonly EnvironmentContext m_environment;
	}
}
