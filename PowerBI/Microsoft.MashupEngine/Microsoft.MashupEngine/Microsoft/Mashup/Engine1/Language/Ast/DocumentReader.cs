using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018A5 RID: 6309
	internal class DocumentReader
	{
		// Token: 0x0600A02A RID: 41002 RVA: 0x00211790 File Offset: 0x0020F990
		public DocumentReader()
		{
			this.currentDepth = 0;
			this.initializers = new List<VariableInitializer>();
			this.expressions = new List<IExpression>();
			this.binaryOperators = new Stack<TokenType>();
			this.binaryOperands = new Stack<IExpression>();
			this.identifiers = new Dictionary<StringSegment, string>();
			this.fieldIdentifiers = new Dictionary<StringSegment, string>();
			this.literalValues = new Dictionary<StringSegment, Value>();
		}

		// Token: 0x17002932 RID: 10546
		// (get) Token: 0x0600A02B RID: 41003 RVA: 0x002117F7 File Offset: 0x0020F9F7
		private TokenType Current
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002933 RID: 10547
		// (get) Token: 0x0600A02C RID: 41004 RVA: 0x002117FF File Offset: 0x0020F9FF
		// (set) Token: 0x0600A02D RID: 41005 RVA: 0x00211807 File Offset: 0x0020FA07
		private TokenReference Position
		{
			get
			{
				return this.tokenIndex;
			}
			set
			{
				this.tokenIndex = value;
				this.type = this.tokens.GetType(this.tokenIndex);
			}
		}

		// Token: 0x0600A02E RID: 41006 RVA: 0x00211827 File Offset: 0x0020FA27
		private bool CurrentIs(ContextualKeyword keyword)
		{
			return this.Current == TokenType.Identifier && this.IsContextualKeyword(this.Position, keyword);
		}

		// Token: 0x0600A02F RID: 41007 RVA: 0x00211844 File Offset: 0x0020FA44
		private bool IsContextualKeyword(TokenReference position, ContextualKeyword keyword)
		{
			ContextualKeyword? contextualKeyword = Keyword.GetContextualKeyword(this.tokens.GetText(position).ToString());
			return (contextualKeyword.GetValueOrDefault() == keyword) & (contextualKeyword != null);
		}

		// Token: 0x0600A030 RID: 41008 RVA: 0x00211888 File Offset: 0x0020FA88
		private void CoalesceBinaryOperations()
		{
			IExpression expression = this.binaryOperands.Pop();
			IExpression expression2 = this.binaryOperands.Pop();
			this.binaryOperands.Push(this.NewBinaryExpression(expression2, this.binaryOperators.Pop(), expression));
		}

		// Token: 0x0600A031 RID: 41009 RVA: 0x002118CB File Offset: 0x0020FACB
		private void Error(string message)
		{
			this.Error(this.Position, message);
		}

		// Token: 0x0600A032 RID: 41010 RVA: 0x002118DC File Offset: 0x0020FADC
		private void Error(TokenReference position, string message)
		{
			SourceLocation sourceLocation = new SourceLocation(this.host, this.tokens.GetRange(position));
			this.Error(sourceLocation, message);
		}

		// Token: 0x0600A033 RID: 41011 RVA: 0x0021190C File Offset: 0x0020FB0C
		private void Error(TokenRange position, string message)
		{
			TextRange range = this.tokens.GetRange(position.Start);
			TextRange range2 = this.tokens.GetRange(position.End);
			TextRange textRange = new TextRange(range.Start, range2.End);
			SourceLocation sourceLocation = new SourceLocation(this.host, textRange);
			this.Error(sourceLocation, message);
		}

		// Token: 0x0600A034 RID: 41012 RVA: 0x0021196C File Offset: 0x0020FB6C
		private void Error(SourceLocation location, string message)
		{
			if (this.tokensToMatch == 0)
			{
				SourceError sourceError = SourceErrors.SyntaxError(location, message);
				this.log(sourceError);
				this.foundError = true;
			}
		}

		// Token: 0x0600A035 RID: 41013 RVA: 0x0021199C File Offset: 0x0020FB9C
		private Identifier GetIdentifier(TokenReference token)
		{
			StringSegment text = this.tokens.GetText(token);
			string text2;
			if (!this.identifiers.TryGetValue(text, out text2))
			{
				if (LanguageServices.Identifier.TryParse(text, out text2))
				{
					this.identifiers.Add(text, text2);
				}
				else
				{
					this.Error(token, Strings.SemanticsBuilder_InvalidIdentifier);
					text2 = text.ToString();
				}
			}
			return Identifier.New(text2, token);
		}

		// Token: 0x0600A036 RID: 41014 RVA: 0x00211A08 File Offset: 0x0020FC08
		private Identifier GetFieldIdentifier(TokenReference start, TokenReference end)
		{
			StringSegment text = this.tokens.GetText(start, end);
			string text2;
			if (!this.fieldIdentifiers.TryGetValue(text, out text2))
			{
				if (LanguageServices.FieldIdentifier.TryParse(text, out text2))
				{
					this.fieldIdentifiers.Add(text, text2);
				}
				else
				{
					this.Error(start, Strings.SemanticsBuilder_InvalidIdentifier);
					text2 = text.ToString();
				}
			}
			return Identifier.New(text2, new TokenRange(start, end));
		}

		// Token: 0x0600A037 RID: 41015 RVA: 0x00211A78 File Offset: 0x0020FC78
		private static int GetPrecedence(TokenType tokenType)
		{
			switch (tokenType)
			{
			case TokenType.Ampersand:
			case TokenType.Minus:
			case TokenType.Plus:
				return 1;
			case TokenType.Divide:
			case TokenType.Multiply:
				return 0;
			case TokenType.Equal:
			case TokenType.NotEqual:
				return 3;
			case TokenType.GreaterThan:
			case TokenType.GreaterThanOrEqual:
			case TokenType.LessThan:
			case TokenType.LessThanOrEqual:
				return 2;
			}
			return -1;
		}

		// Token: 0x0600A038 RID: 41016 RVA: 0x00211AE8 File Offset: 0x0020FCE8
		private static string GetMessageText(ContextualKeyword keyword)
		{
			return string.Format(CultureInfo.InvariantCulture, "'{0}'", ((IEngine)Engine.Instance).ContextualKeywordText(keyword));
		}

		// Token: 0x0600A039 RID: 41017 RVA: 0x00211B04 File Offset: 0x0020FD04
		private static string GetMessageText(TokenType tokenType)
		{
			switch (tokenType)
			{
			case TokenType.Bof:
			case TokenType.Eof:
			case TokenType.TokenError:
			case TokenType.Identifier:
			case TokenType.Literal:
			case TokenType.Whitespace:
				return tokenType.ToString();
			case TokenType.Ampersand:
				return "'&'";
			case TokenType.At:
				return "'@'";
			case TokenType.Bang:
				return "'!'";
			case TokenType.Comma:
				return "','";
			case TokenType.Divide:
				return "'/'";
			case TokenType.DotDot:
				return "'..'";
			case TokenType.Ellipsis:
				return "'...'";
			case TokenType.Equal:
				return "'='";
			case TokenType.GoesTo:
				return "'=>'";
			case TokenType.GreaterThan:
				return "'>'";
			case TokenType.GreaterThanOrEqual:
				return "'>='";
			case TokenType.LeftBrace:
				return "'{'";
			case TokenType.LeftBracket:
				return "'['";
			case TokenType.LeftParen:
				return "'('";
			case TokenType.LessThan:
				return "'<'";
			case TokenType.LessThanOrEqual:
				return "'<='";
			case TokenType.Minus:
				return "'-'";
			case TokenType.Multiply:
				return "'*'";
			case TokenType.NotEqual:
				return "'<>'";
			case TokenType.Plus:
				return "'+'";
			case TokenType.QuestionMark:
				return "'?'";
			case TokenType.RightBrace:
				return "'}'";
			case TokenType.RightBracket:
				return "']'";
			case TokenType.RightParen:
				return "')'";
			case TokenType.Semicolon:
				return "';'";
			case TokenType.As:
			case TokenType.Else:
			case TokenType.Error:
			case TokenType.If:
			case TokenType.In:
			case TokenType.Is:
			case TokenType.Let:
			case TokenType.Meta:
			case TokenType.Not:
			case TokenType.Otherwise:
			case TokenType.Section:
			case TokenType.Shared:
			case TokenType.Then:
			case TokenType.Try:
			case TokenType.Type:
			case TokenType.Each:
				return string.Format(CultureInfo.InvariantCulture, "'{0}'", tokenType.ToString().ToLowerInvariant());
			case TokenType.HashShared:
				return "'#shared'";
			case TokenType.HashSections:
				return "'#sections'";
			case TokenType.LogicalAnd:
				return "'and'";
			case TokenType.LogicalOr:
				return "'or'";
			case TokenType.Verbatim:
				return "'#!'";
			case TokenType.Coalesce:
				return "'??'";
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600A03A RID: 41018 RVA: 0x00211CDF File Offset: 0x0020FEDF
		private void Lost()
		{
			this.tokensToMatch = 8;
		}

		// Token: 0x0600A03B RID: 41019 RVA: 0x00211CE8 File Offset: 0x0020FEE8
		private IExpression NewBinaryExpression(IExpression left, TokenType type, IExpression right)
		{
			TokenRange tokenRange = new TokenRange(left, right);
			switch (type)
			{
			case TokenType.Ampersand:
				return new ConcatenateBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.Divide:
				return new DivideBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.Equal:
				return new EqualsBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.GreaterThan:
				return new GreaterThanBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.GreaterThanOrEqual:
				return new GreaterThanOrEqualsBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.LessThan:
				return new LessThanBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.LessThanOrEqual:
				return new LessThanOrEqualsBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.Minus:
				return new SubtractBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.Multiply:
				return new MultiplyBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.NotEqual:
				return new NotEqualsBinaryExpressionSyntaxNode(left, right, tokenRange);
			case TokenType.Plus:
				return new AddBinaryExpressionSyntaxNode(left, right, tokenRange);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600A03C RID: 41020 RVA: 0x00211DC0 File Offset: 0x0020FFC0
		private TokenReference Read()
		{
			TokenReference tokenReference = this.tokenIndex;
			this.tokenIndex = tokenReference + 1;
			TokenReference tokenReference2 = tokenReference;
			if (this.type != TokenType.Eof)
			{
				this.type = this.ReadWhitespace();
			}
			if (this.tokensToMatch > 0)
			{
				this.tokensToMatch--;
			}
			return tokenReference2;
		}

		// Token: 0x0600A03D RID: 41021 RVA: 0x00211E0A File Offset: 0x0021000A
		private TokenReference Read(TokenType type)
		{
			if (this.Current != type)
			{
				this.Error(Strings.DocumentReader_ExpectedToken(DocumentReader.GetMessageText(type)));
				this.Lost();
				return this.Position;
			}
			return this.Read();
		}

		// Token: 0x0600A03E RID: 41022 RVA: 0x00211E3E File Offset: 0x0021003E
		private TokenReference Read(ContextualKeyword keyword)
		{
			if (!this.CurrentIs(keyword))
			{
				this.Error(Strings.DocumentReader_ExpectedToken(DocumentReader.GetMessageText(keyword)));
				this.Lost();
				return this.Position;
			}
			return this.Read();
		}

		// Token: 0x0600A03F RID: 41023 RVA: 0x00211E74 File Offset: 0x00210074
		private FieldTypeSyntaxNode ReadFieldType()
		{
			bool flag = this.TryRead(ContextualKeyword.Optional);
			Identifier identifier = this.ReadFieldIdentifier();
			IExpression expression = null;
			if (this.TryRead(TokenType.Equal))
			{
				expression = this.ReadType();
			}
			return new FieldTypeSyntaxNode(identifier, expression, flag);
		}

		// Token: 0x0600A040 RID: 41024 RVA: 0x00211EAC File Offset: 0x002100AC
		private IRecordTypeExpression ReadRecordType()
		{
			bool flag = false;
			TokenReference tokenReference = this.Read(TokenType.LeftBracket);
			List<FieldTypeSyntaxNode> list = new List<FieldTypeSyntaxNode>();
			TokenReference tokenReference2 = TokenReference.Null;
			while (this.Current != TokenType.RightBracket && this.Current != TokenType.Eof && tokenReference2 != this.Position)
			{
				tokenReference2 = this.Position;
				if (this.TryRead(TokenType.Ellipsis))
				{
					flag = true;
					break;
				}
				list.Add(this.ReadFieldType());
				this.ReadSeparator(TokenType.RightBracket);
			}
			TokenReference tokenReference3 = this.Read(TokenType.RightBracket);
			return new RecordTypeSyntaxNode(list.ToArray(), flag, new TokenRange(tokenReference, tokenReference3));
		}

		// Token: 0x0600A041 RID: 41025 RVA: 0x00211F34 File Offset: 0x00210134
		private IListTypeExpression ReadListType()
		{
			TokenReference tokenReference = this.Read(TokenType.LeftBrace);
			IExpression expression = this.ReadType();
			TokenReference tokenReference2 = this.Read(TokenType.RightBrace);
			return new ListTypeSyntaxNode(expression, new TokenRange(tokenReference, tokenReference2));
		}

		// Token: 0x0600A042 RID: 41026 RVA: 0x00211F68 File Offset: 0x00210168
		private IExpression ReadTableType(IExpression table)
		{
			TokenType tokenType = this.Current;
			IExpression expression;
			if (tokenType <= TokenType.At)
			{
				if (tokenType != TokenType.Identifier && tokenType != TokenType.At)
				{
					return table;
				}
			}
			else
			{
				if (tokenType == TokenType.LeftBracket)
				{
					expression = this.ReadRecordType();
					goto IL_0035;
				}
				if (tokenType != TokenType.LeftParen)
				{
					return table;
				}
			}
			expression = this.ReadPrimaryExpression();
			IL_0035:
			return new TableTypeSyntaxNode(expression, new TokenRange(table.Range.Start, expression));
		}

		// Token: 0x0600A043 RID: 41027 RVA: 0x00211FC4 File Offset: 0x002101C4
		private INullableTypeExpression ReadNullableType()
		{
			TokenReference tokenReference = this.Read(ContextualKeyword.Nullable);
			IExpression expression = this.ReadType();
			return new NullableTypeSyntaxNode(expression, new TokenRange(tokenReference, expression));
		}

		// Token: 0x0600A044 RID: 41028 RVA: 0x00211FF0 File Offset: 0x002101F0
		private IExpression ReadType()
		{
			IExpression expression;
			if (this.TryReadTypeConstructor(out expression))
			{
				return expression;
			}
			return this.ReadPrimaryExpression();
		}

		// Token: 0x0600A045 RID: 41029 RVA: 0x00212010 File Offset: 0x00210210
		private bool TryReadTypeConstructor(out IExpression expression)
		{
			TokenType tokenType = this.Current;
			if (tokenType == TokenType.LeftBrace)
			{
				expression = this.ReadListType();
				return true;
			}
			if (tokenType == TokenType.LeftBracket)
			{
				expression = this.ReadRecordType();
				return true;
			}
			IConstantExpression constantExpression;
			if (this.TryReadPrimitiveType(out constantExpression))
			{
				if (constantExpression.Value.Equals(TypeValue.Function))
				{
					expression = this.ReadFunctionType(constantExpression);
				}
				else if (constantExpression.Value.Equals(TypeValue.Table))
				{
					expression = this.ReadTableType(constantExpression);
				}
				else
				{
					expression = constantExpression;
				}
				return true;
			}
			if (this.CurrentIs(ContextualKeyword.Nullable))
			{
				expression = this.ReadNullableType();
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x0600A046 RID: 41030 RVA: 0x002120A4 File Offset: 0x002102A4
		private IExpression ReadTypeExpression()
		{
			if (this.Current == TokenType.Type)
			{
				TokenReference tokenReference = this.Read(TokenType.Type);
				IExpression expression;
				if (!this.TryReadTypeConstructor(out expression))
				{
					expression = this.ReadPrimitiveType();
				}
				return new TypeExpressionSyntaxNode(expression, new TokenRange(tokenReference, expression));
			}
			return this.ReadPrimaryExpression();
		}

		// Token: 0x0600A047 RID: 41031 RVA: 0x002120EC File Offset: 0x002102EC
		public IDocument[] ReadDocuments(ITokens tokens, IDocumentHost host, Action<IError> log)
		{
			this.SetTokens(tokens, host, log);
			IDocument document = this.ReadFirstDocument();
			IDocument[] array;
			if (this.Current == TokenType.Eof)
			{
				array = new IDocument[] { document };
			}
			else
			{
				HashSet<string> hashSet = new HashSet<string>();
				ISectionDocument sectionDocument = (ISectionDocument)document;
				hashSet.Add(sectionDocument.Section.SectionName.Name);
				List<IDocument> list = new List<IDocument>();
				list.Add(document);
				while (DocumentReader.IsSectionDocumentStart(this.Current))
				{
					sectionDocument = this.ReadSectionDocument(this.Position);
					list.Add(sectionDocument);
					if (!hashSet.Add(sectionDocument.Section.SectionName.Name))
					{
						this.Error(sectionDocument.Range, Strings.DocumentReader_DuplicateSection(sectionDocument.Section.SectionName.Name));
					}
				}
				this.Read(TokenType.Eof);
				array = list.ToArray();
			}
			this.SetTokens(null, null, null);
			return array;
		}

		// Token: 0x0600A048 RID: 41032 RVA: 0x002121D1 File Offset: 0x002103D1
		private static bool IsSectionDocumentStart(TokenType tokenType)
		{
			return tokenType == TokenType.LeftBracket || tokenType == TokenType.Section;
		}

		// Token: 0x0600A049 RID: 41033 RVA: 0x002121DF File Offset: 0x002103DF
		public IDocument ReadDocument(ITokens tokens, IDocumentHost host, Action<IError> log)
		{
			this.SetTokens(tokens, host, log);
			IDocument document = this.ReadFirstDocument();
			this.Read(TokenType.Eof);
			this.SetTokens(null, null, null);
			return document;
		}

		// Token: 0x0600A04A RID: 41034 RVA: 0x00212201 File Offset: 0x00210401
		private void SetTokens(ITokens tokens, IDocumentHost host, Action<IError> log)
		{
			this.tokens = tokens;
			this.host = host;
			this.log = log;
		}

		// Token: 0x0600A04B RID: 41035 RVA: 0x00212218 File Offset: 0x00210418
		private IDocument ReadFirstDocument()
		{
			this.Reset();
			TokenReference tokenReference = this.Read(TokenType.Bof);
			if (this.Current == TokenType.Section)
			{
				return this.ReadSectionDocument(tokenReference);
			}
			return this.ReadExpressionOrSectionDocument(tokenReference);
		}

		// Token: 0x0600A04C RID: 41036 RVA: 0x0021224C File Offset: 0x0021044C
		private void Reset()
		{
			this.tokenIndex = (TokenReference)0;
			this.type = this.tokens.GetType((TokenReference)0);
			this.tokensToMatch = 0;
		}

		// Token: 0x0600A04D RID: 41037 RVA: 0x00212270 File Offset: 0x00210470
		private IExpression ReadEqualityExpression()
		{
			int count = this.binaryOperators.Count;
			this.binaryOperands.Push(this.ReadMetadataExpression());
			for (;;)
			{
				TokenType tokenType = this.Current;
				int precedence = DocumentReader.GetPrecedence(tokenType);
				if (precedence == -1)
				{
					break;
				}
				while (this.binaryOperators.Count > count && DocumentReader.GetPrecedence(this.binaryOperators.Peek()) <= precedence)
				{
					this.CoalesceBinaryOperations();
				}
				this.binaryOperators.Push(tokenType);
				this.Read();
				this.binaryOperands.Push(this.ReadMetadataExpression());
			}
			while (this.binaryOperators.Count > count)
			{
				this.CoalesceBinaryOperations();
			}
			return this.binaryOperands.Pop();
		}

		// Token: 0x0600A04E RID: 41038 RVA: 0x0021231A File Offset: 0x0021051A
		private IExpression ReadExpression()
		{
			if (this.currentDepth > 800)
			{
				return this.ReadDepthExceeded();
			}
			this.currentDepth++;
			IExpression expression = this.ReadExpressionUnchecked();
			this.currentDepth--;
			return expression;
		}

		// Token: 0x0600A04F RID: 41039 RVA: 0x00212354 File Offset: 0x00210554
		private IExpression ReadExpressionUnchecked()
		{
			TokenType tokenType = this.Current;
			if (tokenType <= TokenType.If)
			{
				if (tokenType == TokenType.LeftParen)
				{
					return this.ReadFunctionOrInExpression();
				}
				if (tokenType == TokenType.Error)
				{
					return this.ReadErrorExpression();
				}
				if (tokenType == TokenType.If)
				{
					return this.ReadIfExpression();
				}
			}
			else
			{
				if (tokenType == TokenType.Let)
				{
					return this.ReadLetExpression();
				}
				if (tokenType == TokenType.Try)
				{
					return this.ReadTryExpression();
				}
				if (tokenType == TokenType.Each)
				{
					return this.ReadEachExpression();
				}
			}
			return this.ReadInExpression();
		}

		// Token: 0x0600A050 RID: 41040 RVA: 0x002123C0 File Offset: 0x002105C0
		private IDocument ReadExpressionOrSectionDocument(TokenReference bof)
		{
			IExpression expression = this.ReadExpression();
			if (expression.Kind == ExpressionKind.Record && this.Current == TokenType.Section)
			{
				if (!this.foundError)
				{
					this.Reset();
					bof = this.Read(TokenType.Bof);
				}
				return this.ReadSectionDocument(bof);
			}
			TokenReference tokenReference = this.Read(TokenType.Eof);
			return new ExpressionDocumentSyntaxNode(this.host, this.tokens, expression, new TokenRange(bof, tokenReference));
		}

		// Token: 0x0600A051 RID: 41041 RVA: 0x00212428 File Offset: 0x00210628
		private IExpression ReadFunctionType(IExpression function)
		{
			if (this.Current == TokenType.LeftParen)
			{
				return this.ReadFunctionType(function.Range.Start, true);
			}
			return function;
		}

		// Token: 0x0600A052 RID: 41042 RVA: 0x00212458 File Offset: 0x00210658
		private IFunctionTypeExpression ReadFunctionType(TokenReference startToken, bool explicitFunctionType)
		{
			this.Read(TokenType.LeftParen);
			int num;
			List<IParameter> list = this.ReadFunctionTypeParameters(explicitFunctionType, out num);
			TokenReference tokenReference = this.Read(TokenType.RightParen);
			IExpression expression = this.ReadReturnType(explicitFunctionType);
			TokenReference tokenReference2;
			if (expression == null)
			{
				tokenReference2 = tokenReference;
			}
			else
			{
				tokenReference2 = expression.Range.End;
			}
			return new FunctionTypeSyntaxNode(expression, list.ToArray(), num, new TokenRange(startToken, tokenReference2));
		}

		// Token: 0x0600A053 RID: 41043 RVA: 0x002124B8 File Offset: 0x002106B8
		private ParameterSyntaxNode ReadFunctionTypeParameter(bool explicitFunctionType, out bool required)
		{
			required = true;
			if (this.CurrentIs(ContextualKeyword.Optional))
			{
				required = false;
				this.Read(ContextualKeyword.Optional);
			}
			Identifier identifier = this.ReadIdentifier();
			IExpression expression = this.ReadReturnType(explicitFunctionType);
			return new ParameterSyntaxNode(identifier, expression);
		}

		// Token: 0x0600A054 RID: 41044 RVA: 0x002124F0 File Offset: 0x002106F0
		private List<IParameter> ReadFunctionTypeParameters(bool explicitFunctionType, out int min)
		{
			List<IParameter> list = new List<IParameter>();
			int num = 0;
			min = 0;
			if (this.Current != TokenType.RightParen)
			{
				TokenReference position;
				do
				{
					position = this.Position;
					bool flag;
					ParameterSyntaxNode parameterSyntaxNode = this.ReadFunctionTypeParameter(explicitFunctionType, out flag);
					if (flag)
					{
						if (num != 0)
						{
							this.Error(parameterSyntaxNode.Identifier.Range.Start, Strings.DocumentReader_InvalidRequiredArgument);
						}
						else
						{
							min++;
						}
					}
					else
					{
						num++;
					}
					list.Add(parameterSyntaxNode);
					this.ReadSeparator(TokenType.RightParen);
				}
				while (this.Current != TokenType.RightParen && this.Current != TokenType.Eof && position != this.Position);
			}
			return list;
		}

		// Token: 0x0600A055 RID: 41045 RVA: 0x0021258C File Offset: 0x0021078C
		private IExpression ReadFunctionExpression()
		{
			IFunctionTypeExpression functionTypeExpression = this.ReadFunctionType(this.Position, false);
			this.Read(TokenType.GoesTo);
			IExpression expression = this.ReadExpression();
			return new FunctionExpressionSyntaxNode(functionTypeExpression, expression, new TokenRange(functionTypeExpression, expression));
		}

		// Token: 0x0600A056 RID: 41046 RVA: 0x002125C5 File Offset: 0x002107C5
		private IExpression ReadFunctionOrInExpression()
		{
			if (this.IsFunctionExpression())
			{
				return this.ReadFunctionExpression();
			}
			return this.ReadInExpression();
		}

		// Token: 0x0600A057 RID: 41047 RVA: 0x002125DC File Offset: 0x002107DC
		private bool IsFunctionExpression()
		{
			TokenReference position = this.Position;
			bool flag = true;
			int num = 0;
			while (flag)
			{
				TokenType tokenType = this.Current;
				if (tokenType == TokenType.Eof)
				{
					this.Position = position;
					return false;
				}
				if (tokenType != TokenType.LeftParen)
				{
					if (tokenType == TokenType.RightParen)
					{
						num--;
						if (num == 0)
						{
							flag = false;
						}
					}
				}
				else
				{
					num++;
				}
				this.Read();
			}
			this.ReadReturnType(false);
			bool flag2 = this.Current == TokenType.GoesTo;
			this.Position = position;
			return flag2;
		}

		// Token: 0x0600A058 RID: 41048 RVA: 0x0021264C File Offset: 0x0021084C
		private IExpression ReadSharedExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.HashShared);
			return new ExportsExpressionSyntaxNode(Identifier.New("Shared"), new TokenRange(tokenReference));
		}

		// Token: 0x0600A059 RID: 41049 RVA: 0x00212678 File Offset: 0x00210878
		private IExpression ReadSectionsExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.HashSections);
			return new ExportsExpressionSyntaxNode(Identifier.New("Sections"), new TokenRange(tokenReference));
		}

		// Token: 0x0600A05A RID: 41050 RVA: 0x002126A4 File Offset: 0x002108A4
		private Identifier ReadIdentifier()
		{
			TokenReference tokenReference = this.Read(TokenType.Identifier);
			return this.GetIdentifier(tokenReference);
		}

		// Token: 0x0600A05B RID: 41051 RVA: 0x002126C0 File Offset: 0x002108C0
		private IExpression ReadIfExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.If);
			IExpression expression = this.ReadExpression();
			this.Read(TokenType.Then);
			IExpression expression2 = this.ReadExpression();
			this.Read(TokenType.Else);
			IExpression expression3 = this.ReadExpression();
			return new IfExpressionSyntaxNode(expression, expression2, expression3, new TokenRange(tokenReference, expression3));
		}

		// Token: 0x0600A05C RID: 41052 RVA: 0x0021270C File Offset: 0x0021090C
		private IExpression ReadItemAccessExpression(IExpression target)
		{
			this.Read(TokenType.LeftBrace);
			IExpression expression = this.ReadExpression();
			TokenReference tokenReference = this.Read(TokenType.RightBrace);
			if (this.Current == TokenType.QuestionMark)
			{
				TokenReference tokenReference2 = this.Read(TokenType.QuestionMark);
				return new OptionalElementAccessExpressionSyntaxNode(target, expression, new TokenRange(target, tokenReference2));
			}
			return new RequiredElementAccessExpressionSyntaxNode(target, expression, new TokenRange(target, tokenReference));
		}

		// Token: 0x0600A05D RID: 41053 RVA: 0x00212762 File Offset: 0x00210962
		private IExpression ReadInExpression()
		{
			return this.ReadCoalesceExpression();
		}

		// Token: 0x0600A05E RID: 41054 RVA: 0x0021276C File Offset: 0x0021096C
		private IExpression ReadInvocationExpression(IExpression lambda)
		{
			int count = this.expressions.Count;
			this.Read(TokenType.LeftParen);
			if (this.Current != TokenType.RightParen)
			{
				TokenReference position;
				do
				{
					position = this.Position;
					this.expressions.Add(this.ReadExpression());
					this.ReadSeparator(TokenType.RightParen);
				}
				while (this.Current != TokenType.RightParen && this.Current != TokenType.Eof && position != this.Position);
			}
			TokenReference tokenReference = this.Read(TokenType.RightParen);
			TokenRange tokenRange = new TokenRange(lambda, tokenReference);
			IInvocationExpression invocationExpression;
			switch (this.expressions.Count - count)
			{
			case 0:
				invocationExpression = new InvocationExpressionSyntaxNode0(lambda, tokenRange);
				break;
			case 1:
				invocationExpression = new InvocationExpressionSyntaxNode1(lambda, this.expressions[count], tokenRange);
				this.expressions.RemoveRange(count, 1);
				break;
			case 2:
				invocationExpression = new InvocationExpressionSyntaxNode2(lambda, this.expressions[count], this.expressions[count + 1], tokenRange);
				this.expressions.RemoveRange(count, 2);
				break;
			default:
				invocationExpression = new InvocationExpressionSyntaxNodeN(lambda, DocumentReader.ToArray<IExpression>(this.expressions, count), tokenRange);
				break;
			}
			return invocationExpression;
		}

		// Token: 0x0600A05F RID: 41055 RVA: 0x00212880 File Offset: 0x00210A80
		private IExpression ReadListExpression()
		{
			int count = this.expressions.Count;
			IList<IRangeExpression> list = null;
			TokenReference tokenReference = this.Read(TokenType.LeftBrace);
			TokenReference tokenReference2 = TokenReference.Null;
			while (this.Current != TokenType.RightBrace && this.Current != TokenType.Eof && tokenReference2 != this.Position)
			{
				tokenReference2 = this.Position;
				IExpression expression = this.ReadExpression();
				if (this.Current == TokenType.DotDot)
				{
					if (list == null)
					{
						list = new List<IRangeExpression>();
						int count2 = this.expressions.Count;
						for (int i = count; i < count2; i++)
						{
							TokenRange tokenRange = new TokenRange(this.expressions[i], this.expressions[i]);
							list.Add(new UnaryRangeExpressionSyntaxNode(this.expressions[i], tokenRange));
						}
						this.expressions.RemoveRange(count, count2 - count);
					}
					this.Read(TokenType.DotDot);
					IExpression expression2 = this.ReadExpression();
					TokenRange tokenRange2 = new TokenRange(expression, expression2);
					list.Add(new BinaryRangeExpressionSyntaxNode(expression, expression2, tokenRange2));
				}
				else if (list == null)
				{
					this.expressions.Add(expression);
				}
				else
				{
					TokenRange tokenRange3 = new TokenRange(expression, expression);
					list.Add(new UnaryRangeExpressionSyntaxNode(expression, tokenRange3));
				}
				this.ReadSeparator(TokenType.RightBrace);
			}
			TokenReference tokenReference3 = this.Read(TokenType.RightBrace);
			if (list == null)
			{
				return new ListExpressionSyntaxNode(DocumentReader.ToArray<IExpression>(this.expressions, count), new TokenRange(tokenReference, tokenReference3));
			}
			return new RangeListExpressionSyntaxNode(list.ToArray<IRangeExpression>(), new TokenRange(tokenReference, tokenReference3));
		}

		// Token: 0x0600A060 RID: 41056 RVA: 0x002129F8 File Offset: 0x00210BF8
		private IConstantExpression2 ReadLiteralExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.Literal);
			StringSegment text = this.tokens.GetText(tokenReference);
			Value value;
			if (!this.literalValues.TryGetValue(text, out value))
			{
				if (!LiteralValue.TryParseValue(text, out value))
				{
					this.Error(tokenReference, Strings.SemanticsBuilder_InvalidLiteral);
				}
				this.literalValues.Add(text, value);
			}
			return new ConstantExpressionSyntaxNode(value, new TokenRange(tokenReference));
		}

		// Token: 0x0600A061 RID: 41057 RVA: 0x00212A60 File Offset: 0x00210C60
		private bool TryReadPrimitiveType(out IConstantExpression expression)
		{
			TokenType tokenType = this.Current;
			TypeValue typeValue;
			if ((tokenType - TokenType.Identifier <= 1 || tokenType == TokenType.Type) && DocumentReader.types.TryGetValue(this.tokens.GetText(this.Position), out typeValue))
			{
				TokenReference tokenReference = this.Read();
				expression = new ConstantExpressionSyntaxNode(typeValue, new TokenRange(tokenReference, tokenReference));
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x0600A062 RID: 41058 RVA: 0x00212ABC File Offset: 0x00210CBC
		private IExpression ReadPrimitiveType()
		{
			IConstantExpression constantExpression;
			if (!this.TryReadPrimitiveType(out constantExpression))
			{
				constantExpression = new ConstantExpressionSyntaxNode(TypeValue.Any, new TokenRange(this.Position, this.Position));
				this.Error(this.Position, Strings.DocumentReader_InvalidPrimitiveType);
				this.Lost();
			}
			return constantExpression;
		}

		// Token: 0x0600A063 RID: 41059 RVA: 0x00212B0C File Offset: 0x00210D0C
		private IExpression ReadNullablePrimitiveType()
		{
			if (this.CurrentIs(ContextualKeyword.Nullable))
			{
				TokenReference tokenReference = this.Read();
				IExpression expression = this.ReadPrimitiveType();
				return new NullableTypeSyntaxNode(expression, new TokenRange(tokenReference, expression));
			}
			return this.ReadPrimitiveType();
		}

		// Token: 0x0600A064 RID: 41060 RVA: 0x00212B44 File Offset: 0x00210D44
		private IExpression ReadAsExpression()
		{
			IExpression expression = this.ReadEqualityExpression();
			while (this.Current == TokenType.As)
			{
				this.Read(TokenType.As);
				IExpression expression2 = this.ReadNullablePrimitiveType();
				expression = new AsBinaryExpressionSyntaxNode(expression, expression2, new TokenRange(expression, expression2));
			}
			return expression;
		}

		// Token: 0x0600A065 RID: 41061 RVA: 0x00212B84 File Offset: 0x00210D84
		private IExpression ReadIsExpression()
		{
			IExpression expression = this.ReadAsExpression();
			while (this.Current == TokenType.Is)
			{
				this.Read(TokenType.Is);
				IExpression expression2 = this.ReadNullablePrimitiveType();
				expression = new IsBinaryExpressionSyntaxNode(expression, expression2, new TokenRange(expression, expression2));
			}
			return expression;
		}

		// Token: 0x0600A066 RID: 41062 RVA: 0x00212BC4 File Offset: 0x00210DC4
		private IExpression CreateBalancedTree(int offset, Func<IExpression, IExpression, IExpression> newBinary)
		{
			for (int i = this.expressions.Count - offset; i > 1; i = (i + 1) / 2)
			{
				for (int j = 0; j < i - 1; j += 2)
				{
					IExpression expression = this.expressions[offset + j];
					IExpression expression2 = this.expressions[offset + j + 1];
					this.expressions[offset + j / 2] = newBinary(expression, expression2);
				}
				if (i % 2 != 0)
				{
					this.expressions[offset + i / 2] = this.expressions[offset + i - 1];
				}
			}
			IExpression expression3 = this.expressions[offset];
			this.expressions.RemoveRange(offset, this.expressions.Count - offset);
			return expression3;
		}

		// Token: 0x0600A067 RID: 41063 RVA: 0x00212C7C File Offset: 0x00210E7C
		private IExpression ReadLogicalAndExpression()
		{
			int count = this.expressions.Count;
			this.expressions.Add(this.ReadIsExpression());
			while (this.Current == TokenType.LogicalAnd)
			{
				this.Read(TokenType.LogicalAnd);
				this.expressions.Add(this.ReadIsExpression());
			}
			return this.CreateBalancedTree(count, (IExpression e1, IExpression e2) => new AndBinaryExpressionSyntaxNode(e1, e2, new TokenRange(e1, e2)));
		}

		// Token: 0x0600A068 RID: 41064 RVA: 0x00212CF4 File Offset: 0x00210EF4
		private IExpression ReadLogicalOrExpression()
		{
			int count = this.expressions.Count;
			this.expressions.Add(this.ReadLogicalAndExpression());
			while (this.Current == TokenType.LogicalOr)
			{
				this.Read(TokenType.LogicalOr);
				this.expressions.Add(this.ReadLogicalAndExpression());
			}
			return this.CreateBalancedTree(count, (IExpression e1, IExpression e2) => new OrBinaryExpressionSyntaxNode(e1, e2, new TokenRange(e1, e2)));
		}

		// Token: 0x0600A069 RID: 41065 RVA: 0x00212D6C File Offset: 0x00210F6C
		private IExpression ReadCoalesceExpression()
		{
			int count = this.expressions.Count;
			this.expressions.Add(this.ReadLogicalOrExpression());
			while (this.Current == TokenType.Coalesce)
			{
				this.Read(TokenType.Coalesce);
				this.expressions.Add(this.ReadLogicalOrExpression());
			}
			return this.CreateBalancedTree(count, (IExpression e1, IExpression e2) => new CoalesceBinaryExpressionSyntaxNode(e1, e2, new TokenRange(e1, e2)));
		}

		// Token: 0x0600A06A RID: 41066 RVA: 0x00212DE4 File Offset: 0x00210FE4
		private IExpression ReadMetadataExpression()
		{
			IExpression expression = this.ReadUnaryExpression();
			while (this.Current == TokenType.Meta)
			{
				this.Read();
				IExpression expression2 = this.ReadUnaryExpression();
				expression = new MetadataAddBinaryExpressionSyntaxNode(expression, expression2, new TokenRange(expression, expression2));
			}
			return expression;
		}

		// Token: 0x0600A06B RID: 41067 RVA: 0x00212E24 File Offset: 0x00211024
		private ISection ReadSection()
		{
			TokenReference position = this.Position;
			IRecordExpression recordExpression = ((this.Current == TokenType.LeftBracket) ? this.ReadConstantRecordExpression() : null);
			this.Read(TokenType.Section);
			Identifier identifier = this.ReadIdentifier();
			TokenReference tokenReference = this.Read(TokenType.Semicolon);
			List<ISectionMember> list = new List<ISectionMember>();
			TokenReference tokenReference2 = TokenReference.Null;
			while (tokenReference2 != this.Position)
			{
				tokenReference2 = this.Position;
				ISectionMember sectionMember = this.ReadSectionMember();
				if (sectionMember == null)
				{
					break;
				}
				list.Add(sectionMember);
				tokenReference = sectionMember.Range.End;
			}
			return new ModuleSyntaxNode(recordExpression, identifier, list.ToArray(), new TokenRange(position, tokenReference));
		}

		// Token: 0x0600A06C RID: 41068 RVA: 0x00212EC0 File Offset: 0x002110C0
		private ISectionDocument ReadSectionDocument(TokenReference begin)
		{
			ISection section = this.ReadSection();
			TokenReference position = this.Position;
			return new ModuleDocumentSyntaxNode(this.host, this.tokens, section, new TokenRange(begin, position));
		}

		// Token: 0x0600A06D RID: 41069 RVA: 0x00212EF4 File Offset: 0x002110F4
		private IListExpression ReadConstantListExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.LeftBrace);
			TokenReference tokenReference2 = TokenReference.Null;
			List<Value> list = new List<Value>();
			List<IExpression> list2 = new List<IExpression>();
			while (this.Current != TokenType.RightBrace && this.Current != TokenType.Eof && tokenReference2 != this.Position)
			{
				tokenReference2 = this.Position;
				IExpression expression = this.ReadConstantExpression();
				list.Add(((IConstantValue)expression).Value);
				list2.Add(expression);
				this.ReadSeparator(TokenType.RightBrace);
			}
			TokenReference tokenReference3 = this.Read(TokenType.RightBrace);
			return new ConstantListExpressionSyntaxNode(ListValue.New(list.ToArray()), list2, new TokenRange(tokenReference, tokenReference3));
		}

		// Token: 0x0600A06E RID: 41070 RVA: 0x00212F8C File Offset: 0x0021118C
		private IRecordExpression ReadConstantRecordExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.LeftBracket);
			int count = this.initializers.Count;
			TokenReference tokenReference2 = TokenReference.Null;
			KeysBuilder keysBuilder = new KeysBuilder(0);
			List<Value> list = new List<Value>();
			while (this.Current != TokenType.RightBracket && this.Current != TokenType.Eof && tokenReference2 != this.Position)
			{
				tokenReference2 = this.Position;
				Identifier identifier = this.ReadFieldIdentifier();
				this.Read(TokenType.Equal);
				IExpression expression = this.ReadConstantExpression();
				this.initializers.Add(new VariableInitializer(identifier, expression));
				if (keysBuilder.Union(identifier.Name))
				{
					list.Add(((IConstantValue)expression).Value);
				}
				else
				{
					this.Error(Strings.SemanticError_DuplicateField(identifier));
				}
				this.ReadSeparator(TokenType.RightBracket);
			}
			TokenReference tokenReference3 = this.Read(TokenType.RightBracket);
			return new ConstantRecordExpressionSyntaxNode(RecordValue.New(keysBuilder.ToKeys(), list.ToArray()), DocumentReader.ToArray<VariableInitializer>(this.initializers, count), new TokenRange(tokenReference, tokenReference3));
		}

		// Token: 0x0600A06F RID: 41071 RVA: 0x00213089 File Offset: 0x00211289
		private IExpression ReadConstantExpression()
		{
			if (this.Current == TokenType.LeftBrace)
			{
				return this.ReadConstantListExpression();
			}
			if (this.Current == TokenType.LeftBracket)
			{
				return this.ReadConstantRecordExpression();
			}
			return this.ReadLiteralExpression();
		}

		// Token: 0x0600A070 RID: 41072 RVA: 0x002130B3 File Offset: 0x002112B3
		private IRecordExpression ReadOptionalAttribute()
		{
			if (this.Current != TokenType.LeftBracket)
			{
				return null;
			}
			return this.ReadConstantRecordExpression();
		}

		// Token: 0x0600A071 RID: 41073 RVA: 0x002130C8 File Offset: 0x002112C8
		private ISectionMember ReadSectionMember()
		{
			TokenReference position = this.Position;
			IRecordExpression recordExpression = this.ReadOptionalAttribute();
			if (this.Current == TokenType.Section)
			{
				this.Position = position;
				return null;
			}
			if (this.Current == TokenType.Eof)
			{
				return null;
			}
			bool flag = this.TryRead(TokenType.Shared);
			Identifier identifier = this.ReadIdentifier();
			this.Read(TokenType.Equal);
			IExpression expression = this.ReadExpression();
			TokenReference tokenReference = this.Read(TokenType.Semicolon);
			return new ModuleMemberSyntaxNode(recordExpression, flag, identifier, expression, new TokenRange(position, tokenReference));
		}

		// Token: 0x0600A072 RID: 41074 RVA: 0x00213140 File Offset: 0x00211340
		private IExpression ReadParenthesesExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.LeftParen);
			IExpression expression = this.ReadExpression();
			TokenReference tokenReference2 = this.Read(TokenType.RightParen);
			return new ParenthesesExpressionSyntaxNode(expression, new TokenRange(tokenReference, tokenReference2));
		}

		// Token: 0x0600A073 RID: 41075 RVA: 0x00213174 File Offset: 0x00211374
		private IExpression ReadPrimaryExpression()
		{
			IExpression expression = this.ReadSimpleExpression();
			for (;;)
			{
				switch (this.Current)
				{
				case TokenType.LeftBrace:
					expression = this.ReadItemAccessExpression(expression);
					continue;
				case TokenType.LeftBracket:
					expression = this.ReadFieldSelectionOrProjection(expression);
					continue;
				case TokenType.LeftParen:
					expression = this.ReadInvocationExpression(expression);
					continue;
				}
				break;
			}
			return expression;
		}

		// Token: 0x0600A074 RID: 41076 RVA: 0x002131C5 File Offset: 0x002113C5
		private IExpression ReadFieldSelectionOrProjection(IExpression recordExpression)
		{
			if (this.IsFieldProjection())
			{
				return this.ReadFieldProjection(recordExpression);
			}
			return this.ReadFieldSelection(recordExpression);
		}

		// Token: 0x0600A075 RID: 41077 RVA: 0x002131E0 File Offset: 0x002113E0
		private bool IsFieldSelectionOrProjection()
		{
			bool flag = false;
			TokenReference position = this.Position;
			if (this.TryRead(TokenType.LeftBracket) && this.Current != TokenType.RightBracket)
			{
				if (this.Current == TokenType.LeftBracket)
				{
					flag = true;
				}
				else
				{
					flag = true;
					while (this.Current != TokenType.Eof && this.Current != TokenType.RightBracket)
					{
						if (this.Current == TokenType.Equal)
						{
							flag = false;
							break;
						}
						this.Read();
					}
				}
			}
			this.Position = position;
			return flag;
		}

		// Token: 0x0600A076 RID: 41078 RVA: 0x0021324C File Offset: 0x0021144C
		private bool IsFieldProjection()
		{
			bool flag = false;
			TokenReference position = this.Position;
			if (this.TryRead(TokenType.LeftBracket) && this.TryRead(TokenType.LeftBracket))
			{
				flag = true;
			}
			this.Position = position;
			return flag;
		}

		// Token: 0x0600A077 RID: 41079 RVA: 0x00213280 File Offset: 0x00211480
		private IExpression ReadFieldSelection(IExpression recordExpression)
		{
			this.Read(TokenType.LeftBracket);
			Identifier identifier = this.ReadFieldIdentifier();
			TokenReference tokenReference = this.Read(TokenType.RightBracket);
			if (this.Current == TokenType.QuestionMark)
			{
				TokenReference tokenReference2 = this.Read();
				return new OptionalFieldAccessExpressionSyntaxNode(recordExpression, identifier, new TokenRange(recordExpression, tokenReference2));
			}
			return new RequiredFieldAccessExpressionSyntaxNode(recordExpression, identifier, new TokenRange(recordExpression, tokenReference));
		}

		// Token: 0x0600A078 RID: 41080 RVA: 0x002132D4 File Offset: 0x002114D4
		private IExpression ReadFieldProjection(IExpression recordExpression)
		{
			this.Read(TokenType.LeftBracket);
			List<Identifier> list = new List<Identifier>();
			TokenReference position;
			do
			{
				position = this.Position;
				this.Read(TokenType.LeftBracket);
				list.Add(this.ReadFieldIdentifier());
				this.Read(TokenType.RightBracket);
				this.ReadSeparator(TokenType.RightBracket);
			}
			while (this.Current != TokenType.Eof && this.Current != TokenType.RightBracket && position != this.Position);
			TokenReference tokenReference = this.Read(TokenType.RightBracket);
			if (this.Current == TokenType.QuestionMark)
			{
				TokenReference tokenReference2 = this.Read();
				return new OptionalMultiFieldRecordProjectionExpressionSyntaxNode(recordExpression, list.ToArray(), new TokenRange(recordExpression, tokenReference2));
			}
			return new RequiredMultiFieldRecordProjectionExpressionSyntaxNode(recordExpression, list.ToArray(), new TokenRange(recordExpression, tokenReference));
		}

		// Token: 0x0600A079 RID: 41081 RVA: 0x0021337C File Offset: 0x0021157C
		private Identifier ReadFieldIdentifier()
		{
			TokenReference position = this.Position;
			TokenReference tokenReference = this.Position;
			while (this.Current != TokenType.Eof && this.Current != TokenType.RightBracket && this.Current != TokenType.Equal && this.Current != TokenType.Comma)
			{
				tokenReference = this.Position;
				this.Read();
			}
			return this.GetFieldIdentifier(position, tokenReference);
		}

		// Token: 0x0600A07A RID: 41082 RVA: 0x002133D8 File Offset: 0x002115D8
		private IExpression ReadRecordExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.LeftBracket);
			IList<VariableInitializer> list = this.ReadRecordInitializers();
			TokenReference tokenReference2 = this.Read(TokenType.RightBracket);
			return new RecordExpressionSyntaxNode(list, new TokenRange(tokenReference, tokenReference2));
		}

		// Token: 0x0600A07B RID: 41083 RVA: 0x0021340C File Offset: 0x0021160C
		private VariableInitializer[] ReadRecordInitializers()
		{
			int count = this.initializers.Count;
			TokenReference tokenReference = TokenReference.Null;
			while (this.Current != TokenType.RightBracket && this.Current != TokenType.Eof && tokenReference != this.Position)
			{
				tokenReference = this.Position;
				Identifier identifier = this.ReadFieldIdentifier();
				this.Read(TokenType.Equal);
				IExpression expression = this.ReadExpression();
				this.initializers.Add(new VariableInitializer(identifier, expression));
				this.ReadSeparator(TokenType.RightBracket);
			}
			return DocumentReader.ToArray<VariableInitializer>(this.initializers, count);
		}

		// Token: 0x0600A07C RID: 41084 RVA: 0x00213489 File Offset: 0x00211689
		private IExpression ReadReturnType(bool required)
		{
			if (!required && this.Current != TokenType.As)
			{
				return null;
			}
			this.Read(TokenType.As);
			if (required)
			{
				return this.ReadType();
			}
			return this.ReadNullablePrimitiveType();
		}

		// Token: 0x0600A07D RID: 41085 RVA: 0x002134B4 File Offset: 0x002116B4
		private void ReadSeparator(TokenType terminator)
		{
			if (this.Current != terminator && this.Current != TokenType.Eof)
			{
				TokenReference tokenReference = this.Read(TokenType.Comma);
				if (this.Current == terminator)
				{
					this.Error(tokenReference, Strings.CommaFollowedByTerminator(DocumentReader.GetMessageText(terminator)));
				}
			}
		}

		// Token: 0x0600A07E RID: 41086 RVA: 0x002134FC File Offset: 0x002116FC
		private IExpression ReadSimpleExpression()
		{
			TokenType tokenType = this.Current;
			if (tokenType <= TokenType.Ellipsis)
			{
				if (tokenType != TokenType.Identifier)
				{
					if (tokenType == TokenType.At)
					{
						TokenReference tokenReference = this.Read(TokenType.At);
						Identifier identifier = this.ReadIdentifier();
						return new InclusiveIdentifierExpressionSyntaxNode(identifier, new TokenRange(tokenReference, identifier));
					}
					if (tokenType == TokenType.Ellipsis)
					{
						return new NotImplementedExpressionSyntaxNode(new TokenRange(this.Read(TokenType.Ellipsis)));
					}
				}
				else
				{
					Identifier identifier2 = this.ReadIdentifier();
					if (this.TryRead(TokenType.Bang))
					{
						Identifier identifier3 = identifier2;
						identifier2 = this.ReadIdentifier();
						return new SectionIdentifierExpressionSyntaxNode(identifier3, identifier2, new TokenRange(identifier3, identifier2));
					}
					return new ExclusiveIdentifierExpressionSyntaxNode(identifier2, new TokenRange(identifier2));
				}
			}
			else if (tokenType <= TokenType.HashShared)
			{
				switch (tokenType)
				{
				case TokenType.LeftBrace:
					return this.ReadListExpression();
				case TokenType.LeftBracket:
					if (this.IsFieldSelectionOrProjection())
					{
						return new ImplicitIdentifierExpressionSyntaxNode(new TokenRange(this.Position));
					}
					return this.ReadRecordExpression();
				case TokenType.LeftParen:
					return this.ReadParenthesesExpression();
				default:
					if (tokenType == TokenType.HashShared)
					{
						return this.ReadSharedExpression();
					}
					break;
				}
			}
			else
			{
				if (tokenType == TokenType.HashSections)
				{
					return this.ReadSectionsExpression();
				}
				if (tokenType == TokenType.Verbatim)
				{
					return this.ReadVerbatimExpression();
				}
			}
			return this.ReadLiteralExpression();
		}

		// Token: 0x0600A07F RID: 41087 RVA: 0x00213618 File Offset: 0x00211818
		private IExpression ReadErrorExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.Error);
			IExpression expression = this.ReadExpression();
			return new ThrowExpressionSyntaxNode(expression, new TokenRange(tokenReference, expression));
		}

		// Token: 0x0600A080 RID: 41088 RVA: 0x00213644 File Offset: 0x00211844
		private IExpression ReadTryExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.Try);
			IExpression expression = this.ReadExpression();
			if (this.Current == TokenType.Otherwise)
			{
				this.Read(TokenType.Otherwise);
				IExpression expression2 = this.ReadExpression();
				TryCatchExceptionCase tryCatchExceptionCase = new TryCatchExceptionCase(Identifier.New(), expression2);
				return new TryCatchExpressionSyntaxNode(expression, tryCatchExceptionCase, new TokenRange(tokenReference, expression2));
			}
			if (this.CurrentIs(ContextualKeyword.Catch))
			{
				this.Read(TokenType.Identifier);
				IFunctionExpression functionExpression = this.ReadFunctionExpression() as IFunctionExpression;
				IFunctionTypeExpression functionTypeExpression = ((functionExpression != null) ? functionExpression.FunctionType : null);
				TryCatchExceptionCase tryCatchExceptionCase2;
				if (functionTypeExpression == null || functionTypeExpression.Parameters == null || functionTypeExpression.Parameters.Count > 1 || functionTypeExpression.Parameters.Count != functionTypeExpression.Min || functionTypeExpression.ReturnType != null || (functionTypeExpression.Parameters.Count == 1 && functionTypeExpression.Parameters[0].Type != null))
				{
					this.Error(Strings.DocumentReader_BadCatch);
					this.Lost();
					tryCatchExceptionCase2 = default(TryCatchExceptionCase);
				}
				else
				{
					Identifier identifier = ((functionTypeExpression.Parameters.Count == 0) ? Identifier.New() : functionTypeExpression.Parameters[0].Identifier);
					tryCatchExceptionCase2 = new TryCatchExceptionCase(identifier, functionExpression.Expression);
				}
				return new TryCatchExpressionSyntaxNode(expression, tryCatchExceptionCase2, new TokenRange(tokenReference, functionExpression));
			}
			Identifier identifier2 = Identifier.New();
			Identifier identifier3 = Identifier.New();
			Identifier identifier4 = Identifier.New();
			IExpression expression3 = new RecordExpressionSyntaxNode(new VariableInitializer[]
			{
				new VariableInitializer(Identifier.New("HasError"), ConstantExpressionSyntaxNode.False),
				new VariableInitializer(Identifier.New("Value"), new ExclusiveIdentifierExpressionSyntaxNode(identifier3))
			});
			IExpression expression4 = new RecordExpressionSyntaxNode(new VariableInitializer[]
			{
				new VariableInitializer(Identifier.New("HasError"), ConstantExpressionSyntaxNode.True),
				new VariableInitializer(Identifier.New("Error"), new ExclusiveIdentifierExpressionSyntaxNode(identifier4))
			});
			IList<VariableInitializer> list = new VariableInitializer[]
			{
				new VariableInitializer(identifier2, new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
				{
					new ParameterSyntaxNode(identifier3, null)
				}, 1), expression3))
			};
			IExpression expression5 = new TryCatchExpressionSyntaxNode(new InvocationExpressionSyntaxNode1(new ExclusiveIdentifierExpressionSyntaxNode(identifier2), expression), new TryCatchExceptionCase(identifier4, expression4), new TokenRange(tokenReference, expression.Range.End));
			return new LetExpressionSyntaxNode(list, expression5, new TokenRange(tokenReference, expression.Range.End));
		}

		// Token: 0x0600A081 RID: 41089 RVA: 0x002138AF File Offset: 0x00211AAF
		private IExpression ReadUnaryExpression()
		{
			if (this.currentDepth > 800)
			{
				return this.ReadDepthExceeded();
			}
			this.currentDepth++;
			IExpression expression = this.ReadUnaryExpressionUnchecked();
			this.currentDepth--;
			return expression;
		}

		// Token: 0x0600A082 RID: 41090 RVA: 0x002138E8 File Offset: 0x00211AE8
		private IExpression ReadUnaryExpressionUnchecked()
		{
			TokenType tokenType = this.Current;
			if (tokenType == TokenType.Minus)
			{
				TokenReference tokenReference = this.Read();
				IExpression expression = this.ReadUnaryExpression();
				return new NegativeUnaryExpressionSyntaxNode(expression, new TokenRange(tokenReference, expression));
			}
			if (tokenType == TokenType.Plus)
			{
				TokenReference tokenReference2 = this.Read();
				IExpression expression2 = this.ReadUnaryExpression();
				return new PositiveUnaryExpressionSyntaxNode(expression2, new TokenRange(tokenReference2, expression2));
			}
			if (tokenType != TokenType.Not)
			{
				return this.ReadTypeExpression();
			}
			TokenReference tokenReference3 = this.Read();
			IExpression expression3 = this.ReadUnaryExpression();
			return new NotUnaryExpressionSyntaxNode(expression3, new TokenRange(tokenReference3, expression3));
		}

		// Token: 0x0600A083 RID: 41091 RVA: 0x00213970 File Offset: 0x00211B70
		private IExpression ReadEachExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.Each);
			IFunctionTypeExpression functionTypeExpression = new FunctionTypeSyntaxNode(null, new IParameter[]
			{
				new ParameterSyntaxNode(Identifier.Underscore, null)
			}, 1, new TokenRange(tokenReference));
			IExpression expression = this.ReadExpression();
			return new FunctionExpressionSyntaxNode(functionTypeExpression, expression, new TokenRange(functionTypeExpression, expression));
		}

		// Token: 0x0600A084 RID: 41092 RVA: 0x002139C0 File Offset: 0x00211BC0
		private IExpression ReadVerbatimExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.Verbatim);
			IConstantExpression2 constantExpression = this.ReadLiteralExpression();
			return new VerbatimExpressionSyntaxNode(constantExpression, new TokenRange(tokenReference, constantExpression));
		}

		// Token: 0x0600A085 RID: 41093 RVA: 0x002139EC File Offset: 0x00211BEC
		private TokenType ReadWhitespace()
		{
			TokenType tokenType;
			for (;;)
			{
				tokenType = this.tokens.GetType(this.tokenIndex);
				if (tokenType != TokenType.Whitespace)
				{
					break;
				}
				this.tokenIndex++;
			}
			return tokenType;
		}

		// Token: 0x0600A086 RID: 41094 RVA: 0x00213A24 File Offset: 0x00211C24
		private IExpression ReadLetExpression()
		{
			TokenReference tokenReference = this.Read(TokenType.Let);
			IList<VariableInitializer> list = this.ReadLetInitializers();
			this.Read(TokenType.In);
			IExpression expression = this.ReadExpression();
			return new LetExpressionSyntaxNode(list, expression, new TokenRange(tokenReference, expression));
		}

		// Token: 0x0600A087 RID: 41095 RVA: 0x00213A60 File Offset: 0x00211C60
		private VariableInitializer[] ReadLetInitializers()
		{
			int count = this.initializers.Count;
			TokenReference tokenReference = TokenReference.Null;
			while (this.Current != TokenType.In && this.Current != TokenType.Eof && tokenReference != this.Position)
			{
				tokenReference = this.Position;
				Identifier identifier = this.ReadIdentifier();
				this.Read(TokenType.Equal);
				IExpression expression = this.ReadExpression();
				this.initializers.Add(new VariableInitializer(identifier, expression));
				this.ReadSeparator(TokenType.In);
			}
			return DocumentReader.ToArray<VariableInitializer>(this.initializers, count);
		}

		// Token: 0x0600A088 RID: 41096 RVA: 0x00213AE0 File Offset: 0x00211CE0
		private static T[] ToArray<T>(List<T> list, int offset)
		{
			int num = list.Count - offset;
			if (num == 0)
			{
				return EmptyArray<T>.Instance;
			}
			T[] array = new T[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = list[offset + i];
			}
			list.RemoveRange(offset, num);
			return array;
		}

		// Token: 0x0600A089 RID: 41097 RVA: 0x00213B2B File Offset: 0x00211D2B
		private bool TryRead(TokenType tokenType)
		{
			if (this.Current != tokenType)
			{
				return false;
			}
			this.Read();
			return true;
		}

		// Token: 0x0600A08A RID: 41098 RVA: 0x00213B40 File Offset: 0x00211D40
		private bool TryRead(ContextualKeyword keyword)
		{
			if (!this.CurrentIs(keyword))
			{
				return false;
			}
			this.Read();
			return true;
		}

		// Token: 0x0600A08B RID: 41099 RVA: 0x00213B58 File Offset: 0x00211D58
		private IExpression ReadDepthExceeded()
		{
			this.Error(Strings.DocumentReader_ParseDepth);
			TokenReference tokenReference = this.tokenIndex;
			while (this.type != TokenType.Eof)
			{
				this.tokenIndex++;
				this.type = this.tokens.GetType(this.tokenIndex);
			}
			return new NotImplementedExpressionSyntaxNode(new TokenRange(tokenReference, this.tokenIndex));
		}

		// Token: 0x040053D4 RID: 21460
		private const int MaxDepth = 800;

		// Token: 0x040053D5 RID: 21461
		private static Dictionary<StringSegment, TypeValue> types = new Dictionary<StringSegment, TypeValue>
		{
			{
				new StringSegment("null"),
				TypeValue.Null
			},
			{
				new StringSegment("time"),
				TypeValue.Time
			},
			{
				new StringSegment("date"),
				TypeValue.Date
			},
			{
				new StringSegment("datetime"),
				TypeValue.DateTime
			},
			{
				new StringSegment("datetimezone"),
				TypeValue.DateTimeZone
			},
			{
				new StringSegment("duration"),
				TypeValue.Duration
			},
			{
				new StringSegment("number"),
				TypeValue.Number
			},
			{
				new StringSegment("logical"),
				TypeValue.Logical
			},
			{
				new StringSegment("text"),
				TypeValue.Text
			},
			{
				new StringSegment("binary"),
				TypeValue.Binary
			},
			{
				new StringSegment("list"),
				TypeValue.List
			},
			{
				new StringSegment("record"),
				TypeValue.Record
			},
			{
				new StringSegment("table"),
				TypeValue.Table
			},
			{
				new StringSegment("function"),
				TypeValue.Function
			},
			{
				new StringSegment("action"),
				TypeValue.Action
			},
			{
				new StringSegment("type"),
				TypeValue._Type
			},
			{
				new StringSegment("any"),
				TypeValue.Any
			},
			{
				new StringSegment("anynonnull"),
				TypeValue.Any.NonNullable
			},
			{
				new StringSegment("none"),
				TypeValue.None
			}
		};

		// Token: 0x040053D6 RID: 21462
		private ITokens tokens;

		// Token: 0x040053D7 RID: 21463
		private TokenReference tokenIndex;

		// Token: 0x040053D8 RID: 21464
		private TokenType type;

		// Token: 0x040053D9 RID: 21465
		private int tokensToMatch;

		// Token: 0x040053DA RID: 21466
		private IDocumentHost host;

		// Token: 0x040053DB RID: 21467
		private Action<IError> log;

		// Token: 0x040053DC RID: 21468
		private int currentDepth;

		// Token: 0x040053DD RID: 21469
		private List<VariableInitializer> initializers;

		// Token: 0x040053DE RID: 21470
		private List<IExpression> expressions;

		// Token: 0x040053DF RID: 21471
		private Stack<TokenType> binaryOperators;

		// Token: 0x040053E0 RID: 21472
		private Stack<IExpression> binaryOperands;

		// Token: 0x040053E1 RID: 21473
		private Dictionary<StringSegment, string> identifiers;

		// Token: 0x040053E2 RID: 21474
		private Dictionary<StringSegment, string> fieldIdentifiers;

		// Token: 0x040053E3 RID: 21475
		private Dictionary<StringSegment, Value> literalValues;

		// Token: 0x040053E4 RID: 21476
		private bool foundError;
	}
}
