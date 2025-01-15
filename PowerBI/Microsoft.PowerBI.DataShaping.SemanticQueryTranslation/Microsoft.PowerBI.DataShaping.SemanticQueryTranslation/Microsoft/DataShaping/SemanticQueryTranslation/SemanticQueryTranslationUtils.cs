using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;
using Microsoft.InfoNav.Utils;
using Newtonsoft.Json;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000016 RID: 22
	internal static class SemanticQueryTranslationUtils
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000048DC File Offset: 0x00002ADC
		internal static T EnsureTranslatorContract<T>(Func<T> translateInternal, SemanticQueryTranslatorContext context, Func<T, bool> isValid)
		{
			return context.TelemetryService.RunInActivity<T>(ActivityKind.SemanticTranslation, delegate
			{
				T t = translateInternal();
				bool flag = isValid(t);
				if (context.ErrorContext.HasError)
				{
					throw SemanticQueryTranslationException.Create(context.ErrorContext);
				}
				if (!flag)
				{
					throw new NullReferenceException("Result should be valid if ErrorContext has no error.");
				}
				return t;
			});
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004922 File Offset: 0x00002B22
		internal static bool IsResultValid(SemanticQueryToDaxTranslationResult result)
		{
			return result != null && (result.ClusteringTranslationResult != null || result.DaxExpression != null || result.ClusteringColumnTranslationResult != null);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004944 File Offset: 0x00002B44
		internal static void EnsureContextError(SemanticQueryTranslationErrorContext errorContext, SemanticQueryTranslationMessage message)
		{
			if (!errorContext.HasError)
			{
				errorContext.Register(message);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004958 File Offset: 0x00002B58
		internal static IReadOnlyList<KeyValuePair<ResolvedQueryExpression, string>> ConvertDisplayNames(IReadOnlyList<ResolvedPartition> partitions)
		{
			if (partitions == null)
			{
				return null;
			}
			List<KeyValuePair<ResolvedQueryExpression, string>> list = new List<KeyValuePair<ResolvedQueryExpression, string>>(partitions.Count);
			foreach (ResolvedPartition resolvedPartition in partitions)
			{
				Contract.RetailAssert(resolvedPartition.PartitionIds.Count == 1, "Currently supporting only 1 partitionId per display name");
				ResolvedQueryExpression resolvedQueryExpression = resolvedPartition.PartitionIds[0];
				list.Add(new KeyValuePair<ResolvedQueryExpression, string>(resolvedQueryExpression, resolvedPartition.DisplayName));
			}
			return list;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000049E4 File Offset: 0x00002BE4
		internal static bool TryUpgradeQuery(QueryDefinition query, SemanticQueryTranslatorContext context)
		{
			if (QueryDefinitionUpgrader.TryUpgrade(context.ErrorContext.CreateAdapter("QueryUpgradeError", ErrorSourceCategory.MalformedExternalInput), query, context.Schema.ToFederatedSchema(), null))
			{
				return true;
			}
			SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.UnknownQueryUpgradeError(EngineMessageSeverity.Error));
			return false;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004A38 File Offset: 0x00002C38
		internal static bool TryResolveQuery(QueryDefinition query, SemanticQueryTranslatorContext context, out ResolvedQueryDefinition resolvedQueryExpression)
		{
			QueryResolutionErrorContext queryResolutionErrorContext = new QueryResolutionErrorContext(context.ErrorContext.CreateAdapter("QueryResolutionError", ErrorSourceCategory.InputDoesNotMatchModel));
			QueryDefinitionNameRegistrar queryDefinitionNameRegistrar = new QueryDefinitionNameRegistrar();
			queryDefinitionNameRegistrar.PushName("Query", false);
			if (QueryDefinitionResolver.TryResolveQuery(query, context.Schema.ToFederatedSchema(), queryResolutionErrorContext, new HashSet<string>(QueryNameComparer.Instance), queryDefinitionNameRegistrar, out resolvedQueryExpression))
			{
				return true;
			}
			SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.UnknownQueryResolutionError(EngineMessageSeverity.Error));
			return false;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004AA8 File Offset: 0x00002CA8
		internal static bool TryValidateQuery(QueryDefinition query, SemanticQueryTranslatorContext context)
		{
			IErrorContext errorContext = context.ErrorContext.CreateAdapter("InvalidSemanticQueryError", ErrorSourceCategory.MalformedExternalInput);
			new QueryDefinitionValidator(new QueryExpressionValidator(errorContext)).Visit(errorContext, query);
			if (errorContext.HasError)
			{
				SemanticQueryTranslationUtils.EnsureContextError(context.ErrorContext, SemanticQueryTranslationMessages.InvalidSemanticQueryError(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004AFC File Offset: 0x00002CFC
		internal static void TraceCommand(SemanticQueryTranslatorContext context, TranslateGroupingQueryCommand command)
		{
			string text = JsonConvert.SerializeObject(command).MarkAsCustomerContent();
			context.Tracer.SanitizedTrace(TraceLevel.Info, "Generating Dax for: {0}", new string[] { text });
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004B30 File Offset: 0x00002D30
		internal static void TraceCommand(SemanticQueryTranslatorContext context, TranslateQueryCommand command)
		{
			string text = JsonConvert.SerializeObject(command).MarkAsCustomerContent();
			context.Tracer.SanitizedTrace(TraceLevel.Info, "Generating Dax for: {0}", new string[] { text });
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004B64 File Offset: 0x00002D64
		internal static void TraceResult(SemanticQueryTranslatorContext context, SemanticQueryToDaxTranslationResult result)
		{
			string text = JsonConvert.SerializeObject(result);
			string text2 = text.MarkAsCustomerContent();
			context.Tracer.SanitizedTrace(TraceLevel.Info, "Generated Dax result: (length: {0}) {1}", text.Length, text2);
		}

		// Token: 0x04000054 RID: 84
		private const string CandidateNameForTopLevelQuery = "Query";
	}
}
