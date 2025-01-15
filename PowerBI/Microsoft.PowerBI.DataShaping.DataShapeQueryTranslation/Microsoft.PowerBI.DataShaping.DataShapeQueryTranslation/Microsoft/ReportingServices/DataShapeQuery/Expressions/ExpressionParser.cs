using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Utils;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200001F RID: 31
	internal sealed class ExpressionParser
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00005872 File Offset: 0x00003A72
		private ExpressionParser(ExpressionContext parserContext, string expressionText)
		{
			this.m_context = parserContext;
			this.m_lexer = new ExpressionLexer(parserContext, expressionText);
			this.NextToken();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005894 File Offset: 0x00003A94
		public static ExpressionNode ParseExpression(ExpressionContext parserContext, string expressionText)
		{
			return new ExpressionParser(parserContext, expressionText).ParseTopLevelExpression();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000058A4 File Offset: 0x00003AA4
		private ExpressionNode ParseTopLevelExpression()
		{
			ExpressionNode expressionNode = this.ParseExpression();
			if (this.m_currentToken.Kind != ExpressionTokenKind.EndOfExpression)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_NotAllTokensConsumed(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.EndOfExpression));
			}
			return expressionNode;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000591A File Offset: 0x00003B1A
		private ExpressionNode ParseExpression()
		{
			return this.ParseLogicalOr();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005924 File Offset: 0x00003B24
		private ExpressionNode ParseLogicalOr()
		{
			ExpressionNode expressionNode = this.ParseLogicalAnd();
			while (this.m_currentToken.Kind == ExpressionTokenKind.Or)
			{
				this.NextToken();
				ExpressionNode expressionNode2 = this.ParseLogicalAnd();
				expressionNode = new BinaryOperatorExpressionNode(BinaryOperatorKind.Or, expressionNode, expressionNode2);
			}
			return expressionNode;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005964 File Offset: 0x00003B64
		private ExpressionNode ParseLogicalAnd()
		{
			ExpressionNode expressionNode = this.ParseEquality();
			while (this.m_currentToken.Kind == ExpressionTokenKind.And)
			{
				this.NextToken();
				ExpressionNode expressionNode2 = this.ParseEquality();
				expressionNode = new BinaryOperatorExpressionNode(BinaryOperatorKind.And, expressionNode, expressionNode2);
			}
			return expressionNode;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000059A0 File Offset: 0x00003BA0
		private ExpressionNode ParseEquality()
		{
			ExpressionNode expressionNode = this.ParseRelational();
			while (this.m_currentToken.Kind == ExpressionTokenKind.Equal)
			{
				this.NextToken();
				ExpressionNode expressionNode2 = this.ParseRelational();
				expressionNode = new BinaryOperatorExpressionNode(BinaryOperatorKind.Equal, expressionNode, expressionNode2);
			}
			return expressionNode;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000059DC File Offset: 0x00003BDC
		private ExpressionNode ParseRelational()
		{
			ExpressionNode expressionNode = this.ParseAdditive();
			while (this.m_currentToken.Kind != ExpressionTokenKind.EndOfExpression)
			{
				BinaryOperatorKind binaryOperatorKind;
				switch (this.m_currentToken.Kind)
				{
				case ExpressionTokenKind.GreaterThan:
					binaryOperatorKind = BinaryOperatorKind.GreaterThan;
					break;
				case ExpressionTokenKind.GreaterThanOrEqual:
					binaryOperatorKind = BinaryOperatorKind.GreaterThanOrEqual;
					break;
				case ExpressionTokenKind.Identifier:
				case ExpressionTokenKind.Int32Literal:
				case ExpressionTokenKind.Int64Literal:
					return expressionNode;
				case ExpressionTokenKind.LessThan:
					binaryOperatorKind = BinaryOperatorKind.LessThan;
					break;
				case ExpressionTokenKind.LessThanOrEqual:
					binaryOperatorKind = BinaryOperatorKind.LessThanOrEqual;
					break;
				default:
					return expressionNode;
				}
				this.NextToken();
				ExpressionNode expressionNode2 = this.ParseAdditive();
				expressionNode = new BinaryOperatorExpressionNode(binaryOperatorKind, expressionNode, expressionNode2);
			}
			return expressionNode;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005A60 File Offset: 0x00003C60
		private ExpressionNode ParseAdditive()
		{
			ExpressionNode expressionNode = this.ParseMultiplicative();
			while (this.m_currentToken.Kind == ExpressionTokenKind.Plus || this.m_currentToken.Kind == ExpressionTokenKind.Minus)
			{
				BinaryOperatorKind binaryOperatorKind = ((this.m_currentToken.Kind == ExpressionTokenKind.Plus) ? BinaryOperatorKind.Add : BinaryOperatorKind.Subtract);
				this.NextToken();
				ExpressionNode expressionNode2 = this.ParseMultiplicative();
				expressionNode = new BinaryOperatorExpressionNode(binaryOperatorKind, expressionNode, expressionNode2);
			}
			return expressionNode;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005AC0 File Offset: 0x00003CC0
		private ExpressionNode ParseMultiplicative()
		{
			ExpressionNode expressionNode = this.ParseUnary();
			while (this.m_currentToken.Kind == ExpressionTokenKind.Star || this.m_currentToken.Kind == ExpressionTokenKind.DoubleSlash)
			{
				BinaryOperatorKind binaryOperatorKind = ((this.m_currentToken.Kind == ExpressionTokenKind.Star) ? BinaryOperatorKind.Multiply : BinaryOperatorKind.Divide);
				this.NextToken();
				ExpressionNode expressionNode2 = this.ParseUnary();
				expressionNode = new BinaryOperatorExpressionNode(binaryOperatorKind, expressionNode, expressionNode2);
			}
			return expressionNode;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005B20 File Offset: 0x00003D20
		private ExpressionNode ParseUnary()
		{
			if (this.m_currentToken.Kind == ExpressionTokenKind.Minus)
			{
				ExpressionToken currentToken = this.m_currentToken;
				this.NextToken();
				if (this.m_lexer.TryHandleMinusPrefixedLiteral(out this.m_currentToken))
				{
					return this.ParsePrimary();
				}
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_UnsupportedUnaryOperator(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, currentToken.Kind));
			}
			else if (this.m_currentToken.Kind == ExpressionTokenKind.Not)
			{
				this.NextToken();
				ExpressionNode expressionNode = this.ParsePrimary();
				return new UnaryOperatorExpressionNode(UnaryOperatorKind.Not, expressionNode);
			}
			return this.ParsePrimary();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005BEC File Offset: 0x00003DEC
		private ExpressionNode ParsePrimary()
		{
			ExpressionTokenKind kind = this.m_currentToken.Kind;
			if (kind <= ExpressionTokenKind.EndOfExpression)
			{
				if (kind == ExpressionTokenKind.At)
				{
					return this.ParseQueryParameterReferenceExpression();
				}
				if (kind == ExpressionTokenKind.EndOfExpression)
				{
					this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_PrematureEndOfExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind));
					return null;
				}
			}
			else
			{
				if (kind == ExpressionTokenKind.Identifier)
				{
					return this.ParseIdentifier();
				}
				if (kind == ExpressionTokenKind.OpenParen)
				{
					return this.ParseParenExpression();
				}
				if (kind == ExpressionTokenKind.OpenSquareBracket)
				{
					return this.ParseStructureOrColumnReference();
				}
			}
			ExpressionNode expressionNode = this.TryParseLiteral();
			if (expressionNode == null)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_InvalidTokenKind(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind));
				return null;
			}
			return expressionNode;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005CF8 File Offset: 0x00003EF8
		private ExpressionNode ParseQueryParameterReferenceExpression()
		{
			this.NextToken();
			string text;
			if (!this.TryParseStringOrIdentifier(out text))
			{
				return null;
			}
			this.NextToken();
			return new QueryParameterReferenceExpressionNode(text);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005D24 File Offset: 0x00003F24
		private ExpressionNode ParseParenExpression()
		{
			if (this.m_currentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_UnexpectedTokenKind(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.OpenParen));
				return null;
			}
			this.NextToken();
			ExpressionNode expressionNode = this.ParseExpression();
			if (this.m_currentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_UnexpectedTokenKind(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.CloseParen));
				return null;
			}
			this.NextToken();
			return expressionNode;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005E10 File Offset: 0x00004010
		private StructureReferenceExpressionNode ParseStructureReference()
		{
			this.NextToken();
			if (this.m_currentToken.Kind != ExpressionTokenKind.Identifier)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_InvalidStructureReferenceSyntax(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.Identifier));
				return null;
			}
			StructureReferenceExpressionNode structureReferenceExpressionNode = new StructureReferenceExpressionNode(new Identifier(this.m_currentToken.Text));
			this.NextToken();
			if (this.m_currentToken.Kind != ExpressionTokenKind.CloseSquareBracket)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_InvalidStructureReferenceSyntax(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.CloseSquareBracket));
				return null;
			}
			this.NextToken();
			return structureReferenceExpressionNode;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005F10 File Offset: 0x00004110
		private ExpressionNode ParseStructureOrColumnReference()
		{
			StructureReferenceExpressionNode structureReferenceExpressionNode = this.ParseStructureReference();
			if (this.m_currentToken.Kind != ExpressionTokenKind.Slash)
			{
				return structureReferenceExpressionNode;
			}
			this.NextToken();
			if (this.m_currentToken.Kind != ExpressionTokenKind.OpenSquareBracket)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_InvalidStructureReferenceSyntax(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.Identifier));
				return null;
			}
			StructureReferenceExpressionNode structureReferenceExpressionNode2 = this.ParseStructureReference();
			if (structureReferenceExpressionNode2 == null)
			{
				return null;
			}
			DataTransformTableColumnReferenceExpressionNode dataTransformTableColumnReferenceExpressionNode = new DataTransformTableColumnReferenceExpressionNode(structureReferenceExpressionNode, structureReferenceExpressionNode2);
			this.NextToken();
			return dataTransformTableColumnReferenceExpressionNode;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005FC0 File Offset: 0x000041C0
		private ExpressionNode ParseNullLiteral()
		{
			ExpressionNode expressionNode = new LiteralExpressionNode(ScalarValue.Null);
			this.NextToken();
			return expressionNode;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005FD4 File Offset: 0x000041D4
		private ExpressionNode TryParseLiteral()
		{
			if (this.m_currentToken.Kind == ExpressionTokenKind.EndOfExpression)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_PrematureEndOfExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind));
				return null;
			}
			return this.ParseLiteral();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000604C File Offset: 0x0000424C
		private LiteralExpressionNode ParseLiteral()
		{
			ScalarValue scalarValue;
			if (!this.TryParseLiteralValue(out scalarValue))
			{
				return null;
			}
			if (scalarValue.IsNaN())
			{
				this.m_context.ErrorContext.Register(TranslationMessages.NaNLiteralNotSupported(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName));
				return null;
			}
			LiteralExpressionNode literalExpressionNode = new LiteralExpressionNode(scalarValue);
			this.NextToken();
			return literalExpressionNode;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000060B4 File Offset: 0x000042B4
		private bool TryParseLiteralValue(out ScalarValue targetValue)
		{
			object obj;
			if (!PrimitiveValueUtils.TryParseFromPrimitiveLiteral(this.m_currentToken.Text, out obj))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_UnrecognizedLiteral(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, this.m_currentToken.Kind));
				return false;
			}
			targetValue = new ScalarValue(obj);
			return true;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006144 File Offset: 0x00004344
		private ExpressionNode ParseIdentifier()
		{
			ExpressionTokenKind kind = this.m_lexer.PeekNextToken().Kind;
			if (kind == ExpressionTokenKind.Dot)
			{
				return this.ParseModelReference();
			}
			if (kind != ExpressionTokenKind.OpenParen)
			{
				return this.ParseKeyWordIdentifier();
			}
			string text = this.m_currentToken.Text;
			if (text == "VisualCalculation")
			{
				return this.ParseVisualCalculation();
			}
			if (text == "Dax")
			{
				return this.ParseDaxText();
			}
			return this.ParseFunctionCall();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000061B8 File Offset: 0x000043B8
		private ExpressionNode ParseVisualCalculation()
		{
			this.NextToken();
			ExpressionNode expressionNode = this.ParseParenExpression();
			if (this.m_context.ErrorContext.HasError)
			{
				return null;
			}
			if (expressionNode == null || !(expressionNode is DaxTextExpressionNode))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_DaxTextExpected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, (expressionNode != null) ? new ExpressionNodeKind?(expressionNode.Kind) : null));
				return null;
			}
			return new VisualCalculationExpressionNode(expressionNode);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000624C File Offset: 0x0000444C
		private ExpressionNode ParseDaxText()
		{
			this.NextToken();
			if (this.m_currentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_OpenParenExpected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.OpenParen));
				return null;
			}
			this.NextToken();
			ScalarValue scalarValue;
			if (!this.TryParseLiteralValue(out scalarValue))
			{
				return null;
			}
			this.NextToken();
			if (this.m_currentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_CloseParenExpected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.CloseParen));
				return null;
			}
			this.NextToken();
			return new DaxTextExpressionNode((string)scalarValue.Value);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006358 File Offset: 0x00004558
		private ExpressionNode ParseFunctionCall()
		{
			string text = this.m_currentToken.Text;
			this.NextToken();
			List<ExpressionNode> list = this.ParseArgumentList();
			FunctionDescriptor functionDescriptor;
			if (!FunctionDescriptorFactory.TryGetDescriptor(text, out functionDescriptor))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.UnknownFunctionName(text)));
				return null;
			}
			this.AddOptionalArguments(list, functionDescriptor);
			return new FunctionCallExpressionNode(functionDescriptor, FunctionUsageKind.Unassigned, list);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000063D8 File Offset: 0x000045D8
		private void AddOptionalArguments(List<ExpressionNode> arguments, FunctionDescriptor functionDescriptor)
		{
			if (arguments != null)
			{
				for (int i = arguments.Count; i < functionDescriptor.Arguments.Count; i++)
				{
					ArgumentDescriptor argumentDescriptor = functionDescriptor.Arguments[i];
					if (argumentDescriptor.IsOptional)
					{
						arguments.Add(new LiteralExpressionNode(argumentDescriptor.DefaultValue.Value));
					}
				}
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006434 File Offset: 0x00004634
		private List<ExpressionNode> ParseArgumentList()
		{
			if (this.m_currentToken.Kind != ExpressionTokenKind.OpenParen)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_OpenParenExpected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.OpenParen));
				return null;
			}
			this.NextToken();
			List<ExpressionNode> list = ((this.m_currentToken.Kind != ExpressionTokenKind.CloseParen) ? this.ParseArguments() : new List<ExpressionNode>());
			if (this.m_currentToken.Kind != ExpressionTokenKind.CloseParen)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_CloseParenExpected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.CloseParen));
				return null;
			}
			this.NextToken();
			return list;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006534 File Offset: 0x00004734
		private List<ExpressionNode> ParseArguments()
		{
			List<ExpressionNode> list = new List<ExpressionNode>();
			for (;;)
			{
				list.Add(this.ParseExpression());
				if (this.m_currentToken.Kind != ExpressionTokenKind.Comma)
				{
					break;
				}
				this.NextToken();
			}
			return list;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000656C File Offset: 0x0000476C
		private ExpressionNode ParseModelReference()
		{
			string text = this.m_currentToken.Text;
			this.NextToken();
			if (this.m_currentToken.Kind != ExpressionTokenKind.Dot)
			{
				this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_InvalidModelReferenceSyntax(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.Dot));
				return null;
			}
			this.NextToken();
			string text2;
			if (!this.TryParseStringOrIdentifier(out text2))
			{
				return null;
			}
			EntitySetExpressionNode entitySetExpressionNode = new EntitySetExpressionNode(text, text2, null);
			this.NextToken();
			if (this.m_currentToken.Kind != ExpressionTokenKind.Slash)
			{
				return entitySetExpressionNode;
			}
			this.NextToken();
			string text3;
			if (!this.TryParseStringOrIdentifier(out text3))
			{
				return null;
			}
			ExpressionNode expressionNode = new PropertyExpressionNode(entitySetExpressionNode, text3, null);
			this.NextToken();
			return expressionNode;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006640 File Offset: 0x00004840
		private bool TryParseStringOrIdentifier(out string text)
		{
			text = null;
			if (this.m_currentToken.Kind == ExpressionTokenKind.StringLiteral)
			{
				ScalarValue scalarValue;
				if (!this.TryParseLiteralValue(out scalarValue))
				{
					return false;
				}
				text = scalarValue.CastValue<string>();
				return true;
			}
			else
			{
				if (this.m_currentToken.Kind != ExpressionTokenKind.Identifier)
				{
					this.m_context.ErrorContext.Register(TranslationMessages.ExpressionParser_InvalidModelReferenceSyntax(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentToken.Text.MarkAsExpressionContent(), this.m_currentToken.Kind, ExpressionTokenKind.Identifier));
					return false;
				}
				text = this.m_currentToken.Text;
				return true;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000066EC File Offset: 0x000048EC
		private ExpressionNode ParseKeyWordIdentifier()
		{
			this.m_currentToken = this.m_lexer.ResolveKeywordToken();
			ExpressionTokenKind kind = this.m_currentToken.Kind;
			if (kind <= ExpressionTokenKind.DateTimeLiteral)
			{
				if (kind != ExpressionTokenKind.BooleanLiteral && kind != ExpressionTokenKind.DateTimeLiteral)
				{
					goto IL_0043;
				}
			}
			else if (kind != ExpressionTokenKind.DoubleLiteral)
			{
				if (kind == ExpressionTokenKind.NullLiteral)
				{
					return this.ParseNullLiteral();
				}
				goto IL_0043;
			}
			return this.ParseLiteral();
			IL_0043:
			this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.UnknownKeyword(this.m_currentToken.Text.MarkAsCustomerContent())));
			return null;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006789 File Offset: 0x00004989
		private void NextToken()
		{
			this.m_currentToken = this.m_lexer.NextToken();
		}

		// Token: 0x04000055 RID: 85
		private readonly ExpressionContext m_context;

		// Token: 0x04000056 RID: 86
		private readonly ExpressionLexer m_lexer;

		// Token: 0x04000057 RID: 87
		private ExpressionToken m_currentToken;
	}
}
