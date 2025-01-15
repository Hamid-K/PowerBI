using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	internal abstract class TSqlParser
	{
		// Token: 0x06000335 RID: 821 RVA: 0x000129B4 File Offset: 0x00010BB4
		internal TSqlParser(bool quotedIdentifiersOn)
		{
			this._quotedIdentifier = quotedIdentifiersOn;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000129C4 File Offset: 0x00010BC4
		public TSqlParser Create(SqlVersion tsqlParserVersion, bool initialQuotedIdentifiers)
		{
			switch (tsqlParserVersion)
			{
			case SqlVersion.Sql90:
				return new TSql90Parser(initialQuotedIdentifiers);
			case SqlVersion.Sql80:
				return new TSql80Parser(initialQuotedIdentifiers);
			case SqlVersion.Sql100:
				return new TSql100Parser(initialQuotedIdentifiers);
			case SqlVersion.Sql110:
				return new TSql110Parser(initialQuotedIdentifiers);
			default:
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SqlScriptGeneratorResource.UnknownEnumValue, new object[] { tsqlParserVersion, "TSqlParserVersion" }), "tsqlParserVersion");
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00012A39 File Offset: 0x00010C39
		public TSqlFragment Parse(TextReader input, out IList<ParseError> errors)
		{
			return this.Parse(input, out errors, 0, 1, 1);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00012A48 File Offset: 0x00010C48
		public TSqlFragment Parse(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			IList<TSqlParserToken> tokenStream = this.GetTokenStream(input, out errors, startOffset, startLine, startColumn);
			if (errors.Count > 0)
			{
				return new TSqlScript();
			}
			return this.Parse(tokenStream, out errors);
		}

		// Token: 0x06000339 RID: 825
		public abstract TSqlFragment Parse(IList<TSqlParserToken> tokens, out IList<ParseError> errors);

		// Token: 0x0600033A RID: 826
		public abstract ChildObjectName ParseChildObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn);

		// Token: 0x0600033B RID: 827 RVA: 0x00012A7B File Offset: 0x00010C7B
		public ChildObjectName ParseChildObjectName(TextReader input, out IList<ParseError> errors)
		{
			return this.ParseChildObjectName(input, out errors, 0, 1, 1);
		}

		// Token: 0x0600033C RID: 828
		public abstract SchemaObjectName ParseSchemaObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn);

		// Token: 0x0600033D RID: 829 RVA: 0x00012A88 File Offset: 0x00010C88
		public SchemaObjectName ParseSchemaObjectName(TextReader input, out IList<ParseError> errors)
		{
			return this.ParseSchemaObjectName(input, out errors, 0, 1, 1);
		}

		// Token: 0x0600033E RID: 830
		public abstract DataTypeReference ParseScalarDataType(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn);

		// Token: 0x0600033F RID: 831 RVA: 0x00012A95 File Offset: 0x00010C95
		public DataTypeReference ParseScalarDataType(TextReader input, out IList<ParseError> errors)
		{
			return this.ParseScalarDataType(input, out errors, 0, 1, 1);
		}

		// Token: 0x06000340 RID: 832
		public abstract TSqlFragment ParseConstantOrIdentifier(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn);

		// Token: 0x06000341 RID: 833 RVA: 0x00012AA2 File Offset: 0x00010CA2
		public TSqlFragment ParseConstantOrIdentifier(TextReader input, out IList<ParseError> errors)
		{
			return this.ParseConstantOrIdentifier(input, out errors, 0, 1, 1);
		}

		// Token: 0x06000342 RID: 834
		public abstract TSqlFragment ParseConstantOrIdentifierWithDefault(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn);

		// Token: 0x06000343 RID: 835 RVA: 0x00012AAF File Offset: 0x00010CAF
		public TSqlFragment ParseConstantOrIdentifierWithDefault(TextReader input, out IList<ParseError> errors)
		{
			return this.ParseConstantOrIdentifierWithDefault(input, out errors, 0, 1, 1);
		}

		// Token: 0x06000344 RID: 836
		public abstract ScalarExpression ParseExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn);

		// Token: 0x06000345 RID: 837 RVA: 0x00012ABC File Offset: 0x00010CBC
		public ScalarExpression ParseExpression(TextReader input, out IList<ParseError> errors)
		{
			return this.ParseExpression(input, out errors, 0, 1, 1);
		}

		// Token: 0x06000346 RID: 838
		public abstract BooleanExpression ParseBooleanExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn);

		// Token: 0x06000347 RID: 839 RVA: 0x00012AC9 File Offset: 0x00010CC9
		public BooleanExpression ParseBooleanExpression(TextReader input, out IList<ParseError> errors)
		{
			return this.ParseBooleanExpression(input, out errors, 0, 1, 1);
		}

		// Token: 0x06000348 RID: 840
		public abstract StatementList ParseStatementList(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn);

		// Token: 0x06000349 RID: 841 RVA: 0x00012AD6 File Offset: 0x00010CD6
		public StatementList ParseStatementList(TextReader input, out IList<ParseError> errors)
		{
			return this.ParseStatementList(input, out errors, 0, 1, 1);
		}

		// Token: 0x0600034A RID: 842
		public abstract SelectStatement ParseSubQueryExpressionWithOptionalCTE(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn);

		// Token: 0x0600034B RID: 843 RVA: 0x00012AE3 File Offset: 0x00010CE3
		public SelectStatement ParseSubQueryExpressionWithOptionalCTE(TextReader input, out IList<ParseError> errors)
		{
			return this.ParseSubQueryExpressionWithOptionalCTE(input, out errors, 0, 1, 1);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00012AF0 File Offset: 0x00010CF0
		public bool TryParseSqlModuleObjectName(TextReader input, out SchemaObjectName result)
		{
			TSqlStatement tsqlStatement = this.PhaseOneParse(input);
			TSqlParser.ExtractSchemaObjectNameVisitor extractSchemaObjectNameVisitor = new TSqlParser.ExtractSchemaObjectNameVisitor();
			result = null;
			if (tsqlStatement != null)
			{
				tsqlStatement.Accept(extractSchemaObjectNameVisitor);
				result = extractSchemaObjectNameVisitor.SchemaObjectName;
			}
			return result != null;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00012B28 File Offset: 0x00010D28
		public bool TryParseTriggerModule(TextReader input, out SchemaObjectName triggerName, out SchemaObjectName targetName)
		{
			TSqlStatement tsqlStatement = this.PhaseOneParse(input);
			TSqlParser.ExtractSchemaObjectNameVisitor extractSchemaObjectNameVisitor = new TSqlParser.ExtractSchemaObjectNameVisitor();
			triggerName = null;
			targetName = null;
			if (tsqlStatement != null)
			{
				tsqlStatement.Accept(extractSchemaObjectNameVisitor);
				triggerName = extractSchemaObjectNameVisitor.SchemaObjectName;
				targetName = extractSchemaObjectNameVisitor.TriggerTargetName;
			}
			return targetName != null;
		}

		// Token: 0x0600034E RID: 846
		internal abstract TSqlStatement PhaseOneParse(TextReader input);

		// Token: 0x0600034F RID: 847 RVA: 0x00012B6B File Offset: 0x00010D6B
		public IList<TSqlParserToken> GetTokenStream(TextReader input, out IList<ParseError> errors)
		{
			return this.GetTokenStream(input, out errors, 0, 1, 1);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00012B78 File Offset: 0x00010D78
		public IList<TSqlParserToken> GetTokenStream(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			errors = new List<ParseError>();
			Collection<TSqlParserToken> collection = new Collection<TSqlParserToken>();
			TSqlLexerBaseInternal newInternalLexer = this.GetNewInternalLexer();
			newInternalLexer.InitializeForNewInput(startOffset, startLine, startColumn, input);
			TSqlParserToken tsqlParserToken = null;
			do
			{
				try
				{
					tsqlParserToken = (TSqlParserToken)newInternalLexer.nextToken();
					collection.Add(tsqlParserToken);
				}
				catch (TokenStreamRecognitionException ex)
				{
					ParseError parseError = TSql80ParserBaseInternal.ProcessTokenStreamRecognitionException(ex, newInternalLexer.CurrentOffset);
					errors.Add(parseError);
					break;
				}
				catch (TSqlParseErrorException ex2)
				{
					errors.Add(ex2.ParseError);
				}
			}
			while (tsqlParserToken != null && tsqlParserToken.TokenType != TSqlTokenType.EndOfFile);
			return collection;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00012C14 File Offset: 0x00010E14
		public bool ValidateIdentifier(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			using (StringReader stringReader = new StringReader(name))
			{
				IList<ParseError> list;
				IList<TSqlParserToken> tokenStream = this.GetTokenStream(stringReader, out list, 0, 1, 1);
				if (tokenStream.Count == 2 && tokenStream[1].TokenType == TSqlTokenType.EndOfFile && (tokenStream[0].TokenType == TSqlTokenType.Identifier || tokenStream[0].TokenType == TSqlTokenType.QuotedIdentifier || tokenStream[0].TokenType == TSqlTokenType.AsciiStringOrQuotedIdentifier) && string.Equals(name, tokenStream[0].Text, 4))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00012CC8 File Offset: 0x00010EC8
		public bool QuotedIdentifier
		{
			get
			{
				return this._quotedIdentifier;
			}
		}

		// Token: 0x06000353 RID: 851
		internal abstract TSqlLexerBaseInternal GetNewInternalLexer();

		// Token: 0x06000354 RID: 852 RVA: 0x00012CD0 File Offset: 0x00010ED0
		internal void InitializeInternalParserInput(TSql80ParserBaseInternal parser, TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			IList<TSqlParserToken> tokenStream = this.GetTokenStream(input, out errors, startOffset, startLine, startColumn);
			parser.InitializeForNewInput(tokenStream, errors, false);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00012CF8 File Offset: 0x00010EF8
		internal TSqlStatement PhaseOneParseImpl(TSql80ParserBaseInternal parser, TSql80ParserBaseInternal.ParserEntryPoint<TSqlScript> entryPoint, string entryPointName, TextReader input)
		{
			IList<ParseError> list;
			IList<TSqlParserToken> tokenStream = this.GetTokenStream(input, out list, 0, 1, 1);
			parser.InitializeForNewInput(tokenStream, list, true);
			bool flag = true;
			while (flag)
			{
				flag = false;
				try
				{
					parser.ParseRuleWithStandardExceptionHandling<TSqlScript>(entryPoint, entryPointName);
				}
				catch (PhaseOnePartialAstException ex)
				{
					return ex.Statement;
				}
				catch (PhaseOneBatchException)
				{
					flag = true;
				}
			}
			return null;
		}

		// Token: 0x04000711 RID: 1809
		internal const string ScriptEntryMethod = "script";

		// Token: 0x04000712 RID: 1810
		private readonly bool _quotedIdentifier;

		// Token: 0x020000CD RID: 205
		private class ExtractSchemaObjectNameVisitor : TSqlFragmentVisitor
		{
			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000986 RID: 2438 RVA: 0x0001DA61 File Offset: 0x0001BC61
			// (set) Token: 0x06000987 RID: 2439 RVA: 0x0001DA69 File Offset: 0x0001BC69
			public SchemaObjectName SchemaObjectName { get; private set; }

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001DA72 File Offset: 0x0001BC72
			// (set) Token: 0x06000989 RID: 2441 RVA: 0x0001DA7A File Offset: 0x0001BC7A
			public SchemaObjectName TriggerTargetName { get; private set; }

			// Token: 0x0600098A RID: 2442 RVA: 0x0001DA83 File Offset: 0x0001BC83
			public override void Visit(ProcedureStatementBody node)
			{
				this.SchemaObjectName = node.ProcedureReference.Name;
			}

			// Token: 0x0600098B RID: 2443 RVA: 0x0001DA96 File Offset: 0x0001BC96
			public override void Visit(ViewStatementBody node)
			{
				this.SchemaObjectName = node.SchemaObjectName;
			}

			// Token: 0x0600098C RID: 2444 RVA: 0x0001DAA4 File Offset: 0x0001BCA4
			public override void Visit(FunctionStatementBody node)
			{
				this.SchemaObjectName = node.Name;
			}

			// Token: 0x0600098D RID: 2445 RVA: 0x0001DAB2 File Offset: 0x0001BCB2
			public override void Visit(TriggerStatementBody node)
			{
				this.SchemaObjectName = node.Name;
				this.TriggerTargetName = node.TriggerObject.Name;
			}
		}
	}
}
