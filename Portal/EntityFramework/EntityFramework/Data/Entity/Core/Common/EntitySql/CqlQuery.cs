using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000648 RID: 1608
	internal static class CqlQuery
	{
		// Token: 0x06004DBF RID: 19903 RVA: 0x00117C50 File Offset: 0x00115E50
		internal static ParseResult Compile(string commandText, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters)
		{
			return CqlQuery.CompileCommon<ParseResult>(commandText, parserOptions, (Node astCommand, ParserOptions validatedParserOptions) => CqlQuery.AnalyzeCommandSemantics(astCommand, perspective, validatedParserOptions, parameters));
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x00117C84 File Offset: 0x00115E84
		internal static DbLambda CompileQueryCommandLambda(string queryCommandText, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return CqlQuery.CompileCommon<DbLambda>(queryCommandText, parserOptions, (Node astCommand, ParserOptions validatedParserOptions) => CqlQuery.AnalyzeQueryExpressionSemantics(astCommand, perspective, validatedParserOptions, parameters, variables));
		}

		// Token: 0x06004DC1 RID: 19905 RVA: 0x00117CC0 File Offset: 0x00115EC0
		private static Node Parse(string commandText, ParserOptions parserOptions)
		{
			Check.NotEmpty(commandText, "commandText");
			Node node = new CqlParser(parserOptions, true).Parse(commandText);
			if (node == null)
			{
				throw EntitySqlException.Create(commandText, Strings.InvalidEmptyQuery, 0, null, false, null);
			}
			return node;
		}

		// Token: 0x06004DC2 RID: 19906 RVA: 0x00117CFB File Offset: 0x00115EFB
		private static TResult CompileCommon<TResult>(string commandText, ParserOptions parserOptions, Func<Node, ParserOptions, TResult> compilationFunction) where TResult : class
		{
			parserOptions = parserOptions ?? new ParserOptions();
			return compilationFunction(CqlQuery.Parse(commandText, parserOptions), parserOptions);
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x00117D17 File Offset: 0x00115F17
		private static ParseResult AnalyzeCommandSemantics(Node astExpr, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters)
		{
			return CqlQuery.AnalyzeSemanticsCommon<ParseResult>(astExpr, perspective, parserOptions, parameters, null, (SemanticAnalyzer analyzer, Node astExpression) => analyzer.AnalyzeCommand(astExpression));
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x00117D42 File Offset: 0x00115F42
		private static DbLambda AnalyzeQueryExpressionSemantics(Node astQueryCommand, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return CqlQuery.AnalyzeSemanticsCommon<DbLambda>(astQueryCommand, perspective, parserOptions, parameters, variables, (SemanticAnalyzer analyzer, Node astExpr) => analyzer.AnalyzeQueryCommand(astExpr));
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x00117D70 File Offset: 0x00115F70
		private static TResult AnalyzeSemanticsCommon<TResult>(Node astExpr, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables, Func<SemanticAnalyzer, Node, TResult> analysisFunction) where TResult : class
		{
			TResult tresult = default(TResult);
			try
			{
				SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer(SemanticResolver.Create(perspective, parserOptions, parameters, variables));
				tresult = analysisFunction(semanticAnalyzer, astExpr);
			}
			catch (MetadataException ex)
			{
				throw new EntitySqlException(Strings.GeneralExceptionAsQueryInnerException("Metadata"), ex);
			}
			catch (MappingException ex2)
			{
				throw new EntitySqlException(Strings.GeneralExceptionAsQueryInnerException("Mapping"), ex2);
			}
			return tresult;
		}
	}
}
