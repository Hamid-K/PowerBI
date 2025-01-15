using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000E1 RID: 225
	[Serializable]
	internal class TSql80Parser : TSqlParser
	{
		// Token: 0x06001444 RID: 5188 RVA: 0x0008F4C5 File Offset: 0x0008D6C5
		public TSql80Parser(bool initialQuotedIdentifiers)
			: base(initialQuotedIdentifiers)
		{
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0008F4CE File Offset: 0x0008D6CE
		internal override TSqlLexerBaseInternal GetNewInternalLexer()
		{
			return new TSql80LexerInternal();
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0008F4D5 File Offset: 0x0008D6D5
		private TSql80ParserInternal GetNewInternalParser()
		{
			return new TSql80ParserInternal(base.QuotedIdentifier);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0008F4E4 File Offset: 0x0008D6E4
		private TSql80ParserInternal GetNewInternalParserForInput(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParser = this.GetNewInternalParser();
			base.InitializeInternalParserInput(newInternalParser, input, out errors, startOffset, startLine, startColumn);
			return newInternalParser;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0008F508 File Offset: 0x0008D708
		public override TSqlFragment Parse(IList<TSqlParserToken> tokens, out IList<ParseError> errors)
		{
			errors = new List<ParseError>();
			TSql80ParserInternal newInternalParser = this.GetNewInternalParser();
			newInternalParser.InitializeForNewInput(tokens, errors, false);
			return newInternalParser.ParseRuleWithStandardExceptionHandling<TSqlScript>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlScript>(newInternalParser.script), "script");
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0008F544 File Offset: 0x0008D744
		public override ChildObjectName ParseChildObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<ChildObjectName>(new TSql80ParserBaseInternal.ParserEntryPoint<ChildObjectName>(newInternalParserForInput.entryPointChildObjectName), "entryPointChildObjectName");
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0008F578 File Offset: 0x0008D778
		public override SchemaObjectName ParseSchemaObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<SchemaObjectName>(new TSql80ParserBaseInternal.ParserEntryPoint<SchemaObjectName>(newInternalParserForInput.entryPointSchemaObjectName), "entryPointSchemaObjectName");
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0008F5AC File Offset: 0x0008D7AC
		public override DataTypeReference ParseScalarDataType(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<DataTypeReference>(new TSql80ParserBaseInternal.ParserEntryPoint<DataTypeReference>(newInternalParserForInput.entryPointScalarDataType), "entryPointScalarDataType");
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0008F5E0 File Offset: 0x0008D7E0
		public override ScalarExpression ParseExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<ScalarExpression>(new TSql80ParserBaseInternal.ParserEntryPoint<ScalarExpression>(newInternalParserForInput.entryPointExpression), "entryPointExpression");
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0008F614 File Offset: 0x0008D814
		public override BooleanExpression ParseBooleanExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<BooleanExpression>(new TSql80ParserBaseInternal.ParserEntryPoint<BooleanExpression>(newInternalParserForInput.entryPointBooleanExpression), "entryPointBooleanExpression");
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0008F648 File Offset: 0x0008D848
		public override StatementList ParseStatementList(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<StatementList>(new TSql80ParserBaseInternal.ParserEntryPoint<StatementList>(newInternalParserForInput.entryPointStatementList), "entryPointStatementList");
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0008F67C File Offset: 0x0008D87C
		public override SelectStatement ParseSubQueryExpressionWithOptionalCTE(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<SelectStatement>(new TSql80ParserBaseInternal.ParserEntryPoint<SelectStatement>(newInternalParserForInput.entryPointSubqueryExpressionWithOptionalCTE), "entryPointSubqueryExpressionWithOptionalCTE");
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0008F6B0 File Offset: 0x0008D8B0
		public override TSqlFragment ParseConstantOrIdentifier(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<TSqlFragment>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlFragment>(newInternalParserForInput.entryPointConstantOrIdentifier), "entryPointConstantOrIdentifier");
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0008F6E4 File Offset: 0x0008D8E4
		public override TSqlFragment ParseConstantOrIdentifierWithDefault(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql80ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<TSqlFragment>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlFragment>(newInternalParserForInput.entryPointConstantOrIdentifierWithDefault), "entryPointConstantOrIdentifierWithDefault");
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0008F718 File Offset: 0x0008D918
		internal override TSqlStatement PhaseOneParse(TextReader input)
		{
			TSql80ParserInternal newInternalParser = this.GetNewInternalParser();
			return base.PhaseOneParseImpl(newInternalParser, new TSql80ParserBaseInternal.ParserEntryPoint<TSqlScript>(newInternalParser.script), "script", input);
		}
	}
}
