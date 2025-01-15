using System;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.SemanticQueryTranslation.Utils;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000014 RID: 20
	internal sealed class SemanticQueryToDaxTranslator : ISemanticQueryToDaxTranslator
	{
		// Token: 0x060000AB RID: 171 RVA: 0x000044DD File Offset: 0x000026DD
		private SemanticQueryToDaxTranslator()
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000044E8 File Offset: 0x000026E8
		public SemanticQueryToDaxTranslationResult Translate(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context)
		{
			Func<SemanticQueryToDaxTranslationResult> func = () => SemanticQueryToDaxTranslator.TranslateInternal(command, context);
			SemanticQueryTranslatorContext context2 = context;
			Func<SemanticQueryToDaxTranslationResult, bool> func2;
			if ((func2 = SemanticQueryToDaxTranslator.<>O.<0>__IsResultValid) == null)
			{
				func2 = (SemanticQueryToDaxTranslator.<>O.<0>__IsResultValid = new Func<SemanticQueryToDaxTranslationResult, bool>(SemanticQueryTranslationUtils.IsResultValid));
			}
			return SemanticQueryTranslationUtils.EnsureTranslatorContract<SemanticQueryToDaxTranslationResult>(func, context2, func2);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000453C File Offset: 0x0000273C
		private static SemanticQueryToDaxTranslationResult TranslateInternal(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context)
		{
			string text = null;
			SemanticQueryToDaxTranslator.ValidateCommand(command, context);
			if (!context.ErrorContext.HasError)
			{
				text = SemanticQueryToDaxTranslator.TranslateValidatedCommand(command, context);
			}
			if (text == null)
			{
				return SemanticQueryToDaxTranslationResultUtils.ForError(context.ErrorContext);
			}
			return SemanticQueryToDaxTranslationResultUtils.ForSingleExpression(text, context.ErrorContext);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004584 File Offset: 0x00002784
		private static string TranslateValidatedCommand(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context)
		{
			string text = null;
			ResolvedQueryDefinition resolvedQueryDefinition;
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			if (SemanticQueryTranslationUtils.TryUpgradeQuery(command.Query, context) && SemanticQueryTranslationUtils.TryResolveQuery(command.Query, context, out resolvedQueryDefinition) && SemanticQueryToDaxTranslator.TryValidateSemantics(command.Query, context) && SemanticQueryToDaxTranslator.TryCreateQdmExpression(context, resolvedQueryDefinition, out queryExpression))
			{
				SemanticQueryToDaxTranslator.TryTranslateQueryExpressionToDax(context, queryExpression, out text);
			}
			return text;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000045D8 File Offset: 0x000027D8
		private static bool TryValidateSemantics(QueryDefinition query, SemanticQueryTranslatorContext context)
		{
			if (!query.Where.IsNullOrEmpty<QueryFilter>())
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.InvalidQueryForExpressionFilters(EngineMessageSeverity.Error));
			}
			if (!query.OrderBy.IsNullOrEmpty<Microsoft.InfoNav.Data.Contracts.Internal.QuerySortClause>())
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.InvalidQueryForExpressionSorting(EngineMessageSeverity.Error));
			}
			if (query.Select.Count != 1)
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.InvalidQueryForExpressionMultipleProjections(EngineMessageSeverity.Error));
			}
			return !context.ErrorContext.HasError;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004650 File Offset: 0x00002850
		private static void ValidateCommand(TranslateGroupingQueryCommand command, SemanticQueryTranslatorContext context)
		{
			IErrorContext errorContext = context.ErrorContext.CreateAdapter("InvalidSemanticQueryError", ErrorSourceCategory.MalformedExternalInput);
			new QueryDefinitionValidator(new QueryExpressionValidator(errorContext)).Visit(errorContext, command.Query);
			if (errorContext.HasError)
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.InvalidSemanticQueryError(EngineMessageSeverity.Error));
				return;
			}
			SemanticQueryTranslationUtils.TraceCommand(context, command);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000046AB File Offset: 0x000028AB
		private static bool TryCreateQdmExpression(SemanticQueryTranslatorContext context, ResolvedQueryDefinition resolvedQuery, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression)
		{
			expression = SemanticQueryExpressionTranslator.Generate(context.ErrorContext, context.Model, context.Schema, context.FeatureSwitchProvider, resolvedQuery);
			return expression != null;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000046D4 File Offset: 0x000028D4
		internal static bool TryTranslateQueryExpressionToDax(SemanticQueryTranslatorContext context, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression qdmExpression, out string daxExpression)
		{
			bool flag;
			try
			{
				DaxCapabilities daxCapabilities = DaxCapabilitiesBuilder.BuildCapabilities(context.Model, context.Schema, context.FeatureSwitchProvider);
				string text = (context.UseConceptualSchema ? context.Schema.ConceptualCollation.Culture : context.Model.Culture);
				DaxExpression daxExpression2 = qdmExpression.Accept<DaxExpression>(new DaxTransform(daxCapabilities, text, context.CancellationToken));
				if (daxExpression2 == null || string.IsNullOrEmpty(daxExpression2.Text))
				{
					daxExpression = null;
					context.ErrorContext.Register(SemanticQueryTranslationMessages.DaxTransformError(EngineMessageSeverity.Error, ErrorSource.PowerBI, null));
					flag = false;
				}
				else
				{
					daxExpression = daxExpression2.Text;
					flag = true;
				}
			}
			catch (CommandTreeTranslationException ex)
			{
				EngineMessageBase treeTranslationExceptionBaseMessage = DefinitionGenerationUtils.GetTreeTranslationExceptionBaseMessage(ex, context.DataSourceName);
				if (treeTranslationExceptionBaseMessage != null)
				{
					context.ErrorContext.Register(SemanticQueryTranslationMessages.DaxTransformError(treeTranslationExceptionBaseMessage.Severity, treeTranslationExceptionBaseMessage.Source, treeTranslationExceptionBaseMessage.Message));
				}
				throw;
			}
			return flag;
		}

		// Token: 0x04000048 RID: 72
		internal static readonly SemanticQueryToDaxTranslator Instance = new SemanticQueryToDaxTranslator();

		// Token: 0x02000036 RID: 54
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040000A2 RID: 162
			public static Func<SemanticQueryToDaxTranslationResult, bool> <0>__IsResultValid;
		}
	}
}
