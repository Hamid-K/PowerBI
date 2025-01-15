using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.SqlTranslator;

namespace Microsoft.Data.Mashup.ProviderCommon.CommandText
{
	// Token: 0x0200001B RID: 27
	internal class CommandTextParser
	{
		// Token: 0x06000193 RID: 403 RVA: 0x000065BA File Offset: 0x000047BA
		public CommandTextParser(IEvaluationConstants evaluationConstants = null)
		{
			this.evaluationConstants = evaluationConstants;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000065C9 File Offset: 0x000047C9
		public CommandTextParseResult ParseAsTableName(string commandText)
		{
			return new CommandTextParseResult(string.Format(CultureInfo.InvariantCulture, "{0}", MashupEngines.Version1.EscapeIdentifier(commandText)), new string[] { commandText });
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000065F4 File Offset: 0x000047F4
		public bool TryParseAsSql(string commandText, out CommandTextParseResult result)
		{
			try
			{
				SqlParseResult sqlParseResult = SqlParser.Parse(commandText);
				if (sqlParseResult.IsRecognized)
				{
					result = new CommandTextParseResult(string.Format(CultureInfo.InvariantCulture, "Expression.Evaluate(SqlExpression.ToExpression({1}, {0}), #shared)({0})", ExpressionTransforms.EnvironmentParameter, MashupEngines.Version1.EscapeString(sqlParseResult.Sql)), sqlParseResult.ResourceNames);
					return true;
				}
			}
			catch (Exception ex) when (ProviderTracing.TraceIsSafeException("CommandTextParser/TryParseAsSql", ex, this.evaluationConstants, null))
			{
			}
			result = null;
			return false;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006688 File Offset: 0x00004888
		public bool TryParseAsM(string commandText, ISectionDocument[] sectionDocuments, out CommandTextParseResult result)
		{
			bool hasErrors = false;
			ITokens tokens = MashupEngines.Version1.Tokenize(commandText);
			IExpressionDocument expressionDocument = MashupEngines.Version1.Parse(tokens, new TextDocumentHost(commandText), delegate(IError e)
			{
				hasErrors = true;
			}) as IExpressionDocument;
			if (!hasErrors && expressionDocument != null)
			{
				result = new CommandTextParseResult(commandText, CommandTextParser.GetResources(sectionDocuments.GetSharedResourceNames(), expressionDocument.Expression));
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000066FC File Offset: 0x000048FC
		private static IEnumerable<string> GetResources(IEnumerable<string> resources, IExpression expression)
		{
			HashSet<string> externalReferences = new ExternalReferencesVisitor().GetExternalReferences(expression);
			return resources.Where((string r) => externalReferences.Contains(r));
		}

		// Token: 0x0400009B RID: 155
		private readonly IEvaluationConstants evaluationConstants;
	}
}
