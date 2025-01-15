using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000E0 RID: 224
	[Serializable]
	internal class TSql90Parser : TSqlParser
	{
		// Token: 0x06001434 RID: 5172 RVA: 0x0008F211 File Offset: 0x0008D411
		public TSql90Parser(bool initialQuotedIdentifiers)
			: base(initialQuotedIdentifiers)
		{
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0008F21A File Offset: 0x0008D41A
		internal override TSqlLexerBaseInternal GetNewInternalLexer()
		{
			return new TSql90LexerInternal();
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0008F221 File Offset: 0x0008D421
		private TSql90ParserInternal GetNewInternalParser()
		{
			return new TSql90ParserInternal(base.QuotedIdentifier);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0008F230 File Offset: 0x0008D430
		private TSql90ParserInternal GetNewInternalParserForInput(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParser = this.GetNewInternalParser();
			base.InitializeInternalParserInput(newInternalParser, input, out errors, startOffset, startLine, startColumn);
			return newInternalParser;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0008F254 File Offset: 0x0008D454
		public override TSqlFragment Parse(IList<TSqlParserToken> tokens, out IList<ParseError> errors)
		{
			errors = new List<ParseError>();
			TSql90ParserInternal newInternalParser = this.GetNewInternalParser();
			newInternalParser.InitializeForNewInput(tokens, errors, false);
			return newInternalParser.ParseRuleWithStandardExceptionHandling<TSqlScript>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlScript>(newInternalParser.script), "script");
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0008F290 File Offset: 0x0008D490
		public override ChildObjectName ParseChildObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<ChildObjectName>(new TSql80ParserBaseInternal.ParserEntryPoint<ChildObjectName>(newInternalParserForInput.entryPointChildObjectName), "entryPointChildObjectName");
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0008F2C4 File Offset: 0x0008D4C4
		public override SchemaObjectName ParseSchemaObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<SchemaObjectName>(new TSql80ParserBaseInternal.ParserEntryPoint<SchemaObjectName>(newInternalParserForInput.entryPointSchemaObjectName), "entryPointSchemaObjectName");
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0008F2F8 File Offset: 0x0008D4F8
		public override DataTypeReference ParseScalarDataType(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<DataTypeReference>(new TSql80ParserBaseInternal.ParserEntryPoint<DataTypeReference>(newInternalParserForInput.entryPointScalarDataType), "entryPointScalarDataType");
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0008F32C File Offset: 0x0008D52C
		public override ScalarExpression ParseExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<ScalarExpression>(new TSql80ParserBaseInternal.ParserEntryPoint<ScalarExpression>(newInternalParserForInput.entryPointExpression), "entryPointExpression");
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0008F360 File Offset: 0x0008D560
		public override BooleanExpression ParseBooleanExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<BooleanExpression>(new TSql80ParserBaseInternal.ParserEntryPoint<BooleanExpression>(newInternalParserForInput.entryPointBooleanExpression), "entryPointBooleanExpression");
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0008F394 File Offset: 0x0008D594
		public override StatementList ParseStatementList(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<StatementList>(new TSql80ParserBaseInternal.ParserEntryPoint<StatementList>(newInternalParserForInput.entryPointStatementList), "entryPointStatementList");
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0008F3C8 File Offset: 0x0008D5C8
		public override SelectStatement ParseSubQueryExpressionWithOptionalCTE(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<SelectStatement>(new TSql80ParserBaseInternal.ParserEntryPoint<SelectStatement>(newInternalParserForInput.entryPointSubqueryExpressionWithOptionalCTE), "entryPointSubqueryExpressionWithOptionalCTE");
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0008F3FC File Offset: 0x0008D5FC
		internal IPv4 ParseIPv4(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<IPv4>(new TSql80ParserBaseInternal.ParserEntryPoint<IPv4>(newInternalParserForInput.entryPointIPv4Address), "entryPointIPv4Address");
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0008F430 File Offset: 0x0008D630
		public override TSqlFragment ParseConstantOrIdentifier(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<TSqlFragment>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlFragment>(newInternalParserForInput.entryPointConstantOrIdentifier), "entryPointConstantOrIdentifier");
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0008F464 File Offset: 0x0008D664
		public override TSqlFragment ParseConstantOrIdentifierWithDefault(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql90ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<TSqlFragment>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlFragment>(newInternalParserForInput.entryPointConstantOrIdentifierWithDefault), "entryPointConstantOrIdentifierWithDefault");
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0008F498 File Offset: 0x0008D698
		internal override TSqlStatement PhaseOneParse(TextReader input)
		{
			TSql90ParserInternal newInternalParser = this.GetNewInternalParser();
			return base.PhaseOneParseImpl(newInternalParser, new TSql80ParserBaseInternal.ParserEntryPoint<TSqlScript>(newInternalParser.script), "script", input);
		}
	}
}
