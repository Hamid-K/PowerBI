using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000CE RID: 206
	[Serializable]
	internal class TSql110Parser : TSqlParser
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0001DAD9 File Offset: 0x0001BCD9
		public TSql110Parser(bool initialQuotedIdentifiers)
			: base(initialQuotedIdentifiers)
		{
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001DAE2 File Offset: 0x0001BCE2
		internal override TSqlLexerBaseInternal GetNewInternalLexer()
		{
			return new TSql110LexerInternal();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001DAE9 File Offset: 0x0001BCE9
		private TSql110ParserInternal GetNewInternalParser()
		{
			return new TSql110ParserInternal(base.QuotedIdentifier);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		private TSql110ParserInternal GetNewInternalParserForInput(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParser = this.GetNewInternalParser();
			base.InitializeInternalParserInput(newInternalParser, input, out errors, startOffset, startLine, startColumn);
			return newInternalParser;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001DB1C File Offset: 0x0001BD1C
		public override TSqlFragment Parse(IList<TSqlParserToken> tokens, out IList<ParseError> errors)
		{
			errors = new List<ParseError>();
			TSql110ParserInternal newInternalParser = this.GetNewInternalParser();
			newInternalParser.InitializeForNewInput(tokens, errors, false);
			return newInternalParser.ParseRuleWithStandardExceptionHandling<TSqlScript>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlScript>(newInternalParser.script), "script");
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001DB58 File Offset: 0x0001BD58
		public override ChildObjectName ParseChildObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<ChildObjectName>(new TSql80ParserBaseInternal.ParserEntryPoint<ChildObjectName>(newInternalParserForInput.entryPointChildObjectName), "entryPointChildObjectName");
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001DB8C File Offset: 0x0001BD8C
		public override SchemaObjectName ParseSchemaObjectName(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<SchemaObjectName>(new TSql80ParserBaseInternal.ParserEntryPoint<SchemaObjectName>(newInternalParserForInput.entryPointSchemaObjectName), "entryPointSchemaObjectName");
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001DBC0 File Offset: 0x0001BDC0
		public override DataTypeReference ParseScalarDataType(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<DataTypeReference>(new TSql80ParserBaseInternal.ParserEntryPoint<DataTypeReference>(newInternalParserForInput.entryPointScalarDataType), "entryPointScalarDataType");
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		public override ScalarExpression ParseExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<ScalarExpression>(new TSql80ParserBaseInternal.ParserEntryPoint<ScalarExpression>(newInternalParserForInput.entryPointExpression), "entryPointExpression");
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001DC28 File Offset: 0x0001BE28
		public override BooleanExpression ParseBooleanExpression(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<BooleanExpression>(new TSql80ParserBaseInternal.ParserEntryPoint<BooleanExpression>(newInternalParserForInput.entryPointBooleanExpression), "entryPointBooleanExpression");
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001DC5C File Offset: 0x0001BE5C
		public override StatementList ParseStatementList(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<StatementList>(new TSql80ParserBaseInternal.ParserEntryPoint<StatementList>(newInternalParserForInput.entryPointStatementList), "entryPointStatementList");
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001DC90 File Offset: 0x0001BE90
		public override SelectStatement ParseSubQueryExpressionWithOptionalCTE(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<SelectStatement>(new TSql80ParserBaseInternal.ParserEntryPoint<SelectStatement>(newInternalParserForInput.entryPointSubqueryExpressionWithOptionalCTE), "entryPointSubqueryExpressionWithOptionalCTE");
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001DCC4 File Offset: 0x0001BEC4
		internal IPv4 ParseIPv4(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<IPv4>(new TSql80ParserBaseInternal.ParserEntryPoint<IPv4>(newInternalParserForInput.entryPointIPv4Address), "entryPointIPv4Address");
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001DCF8 File Offset: 0x0001BEF8
		public override TSqlFragment ParseConstantOrIdentifier(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<TSqlFragment>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlFragment>(newInternalParserForInput.entryPointConstantOrIdentifier), "entryPointConstantOrIdentifier");
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		public override TSqlFragment ParseConstantOrIdentifierWithDefault(TextReader input, out IList<ParseError> errors, int startOffset, int startLine, int startColumn)
		{
			TSql110ParserInternal newInternalParserForInput = this.GetNewInternalParserForInput(input, out errors, startOffset, startLine, startColumn);
			return newInternalParserForInput.ParseRuleWithStandardExceptionHandling<TSqlFragment>(new TSql80ParserBaseInternal.ParserEntryPoint<TSqlFragment>(newInternalParserForInput.entryPointConstantOrIdentifierWithDefault), "entryPointConstantOrIdentifierWithDefault");
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001DD60 File Offset: 0x0001BF60
		internal override TSqlStatement PhaseOneParse(TextReader input)
		{
			TSql110ParserInternal newInternalParser = this.GetNewInternalParser();
			return base.PhaseOneParseImpl(newInternalParser, new TSql80ParserBaseInternal.ParserEntryPoint<TSqlScript>(newInternalParser.script), "script", input);
		}
	}
}
