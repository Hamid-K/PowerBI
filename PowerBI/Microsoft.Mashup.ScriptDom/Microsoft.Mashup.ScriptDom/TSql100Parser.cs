using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000DF RID: 223
	[Serializable]
	internal class TSql100Parser : TSqlParser
	{
		// Token: 0x06001424 RID: 5156 RVA: 0x0008EF5D File Offset: 0x0008D15D
		public TSql100Parser(bool initialQuotedIdentifiers)
			: base(initialQuotedIdentifiers)
		{
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0008EF66 File Offset: 0x0008D166
		internal override TSqlLexerBaseInternal GetNewInternalLexer()
		{
			return new TSql100LexerInternal();
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0008EF6D File Offset: 0x0008D16D
		private TSql100ParserInternal GetNewInternalParser()
		{
			return new TSql100ParserInternal(base.QuotedIdentifier);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0008EF7C File Offset: 0x0008D17C
		private TSql100ParserInternal GetNewInternalParserForInput(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParser = this.GetNewInternalParser();
			base.InitializeInternalParserInput(newInternalParser, input, out errors, startOffset, startLine, startColumn);
			return newInternalParser;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0008EFA0 File Offset: 0x0008D1A0
		public override TSqlFragment Parse(IList<TSqlParserToken> tokens, out IList<ParseError> errors)
		{
			errors = new List<ParseError>();
			TSql100ParserInternal newInternalParser = this.GetNewInternalParser();
			newInternalParser.InitializeForNewInput(tokens, errors, false);
			return newInternalParser.ParseRuleWithStandardExceptionHandling<TSqlScript>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlScript>(newInternalParser.script), "script");
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0008EFDC File Offset: 0x0008D1DC
		public override ChildObjectName ParseChildObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<ChildObjectName>(new TSql80ParserBaseInternal.ParserEntryPoint<ChildObjectName>(newInternalParserForInput.entryPointChildObjectName), "entryPointChildObjectName");
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0008F010 File Offset: 0x0008D210
		public override SchemaObjectName ParseSchemaObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<SchemaObjectName>(new TSql80ParserBaseInternal.ParserEntryPoint<SchemaObjectName>(newInternalParserForInput.entryPointSchemaObjectName), "entryPointSchemaObjectName");
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0008F044 File Offset: 0x0008D244
		public override DataTypeReference ParseScalarDataType(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<DataTypeReference>(new TSql80ParserBaseInternal.ParserEntryPoint<DataTypeReference>(newInternalParserForInput.entryPointScalarDataType), "entryPointScalarDataType");
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0008F078 File Offset: 0x0008D278
		public override ScalarExpression ParseExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<ScalarExpression>(new TSql80ParserBaseInternal.ParserEntryPoint<ScalarExpression>(newInternalParserForInput.entryPointExpression), "entryPointExpression");
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0008F0AC File Offset: 0x0008D2AC
		public override BooleanExpression ParseBooleanExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<BooleanExpression>(new TSql80ParserBaseInternal.ParserEntryPoint<BooleanExpression>(newInternalParserForInput.entryPointBooleanExpression), "entryPointBooleanExpression");
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0008F0E0 File Offset: 0x0008D2E0
		public override StatementList ParseStatementList(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<StatementList>(new TSql80ParserBaseInternal.ParserEntryPoint<StatementList>(newInternalParserForInput.entryPointStatementList), "entryPointStatementList");
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0008F114 File Offset: 0x0008D314
		public override SelectStatement ParseSubQueryExpressionWithOptionalCTE(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<SelectStatement>(new TSql80ParserBaseInternal.ParserEntryPoint<SelectStatement>(newInternalParserForInput.entryPointSubqueryExpressionWithOptionalCTE), "entryPointSubqueryExpressionWithOptionalCTE");
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0008F148 File Offset: 0x0008D348
		internal IPv4 ParseIPv4(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<IPv4>(new TSql80ParserBaseInternal.ParserEntryPoint<IPv4>(newInternalParserForInput.entryPointIPv4Address), "entryPointIPv4Address");
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0008F17C File Offset: 0x0008D37C
		public override TSqlFragment ParseConstantOrIdentifier(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<TSqlFragment>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlFragment>(newInternalParserForInput.entryPointConstantOrIdentifier), "entryPointConstantOrIdentifier");
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0008F1B0 File Offset: 0x0008D3B0
		public override TSqlFragment ParseConstantOrIdentifierWithDefault(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql100ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<TSqlFragment>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlFragment>(newInternalParserForInput.entryPointConstantOrIdentifierWithDefault), "entryPointConstantOrIdentifierWithDefault");
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0008F1E4 File Offset: 0x0008D3E4
		internal override TSqlStatement PhaseOneParse(TextReader input)
		{
			TSql100ParserInternal newInternalParser = this.GetNewInternalParser();
			return base.PhaseOneParseImpl(newInternalParser, new TSql80ParserBaseInternal.ParserEntryPoint<TSqlScript>(newInternalParser.script), "script", input);
		}
	}
}
