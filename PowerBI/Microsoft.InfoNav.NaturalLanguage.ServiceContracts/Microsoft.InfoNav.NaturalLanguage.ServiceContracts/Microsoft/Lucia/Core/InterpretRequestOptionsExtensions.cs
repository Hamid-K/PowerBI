using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000A2 RID: 162
	internal static class InterpretRequestOptionsExtensions
	{
		// Token: 0x0600031D RID: 797 RVA: 0x00006813 File Offset: 0x00004A13
		internal static bool ShouldSpellCorrect(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SpellCorrect);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000681C File Offset: 0x00004A1C
		internal static bool ShouldGenerateRestatement(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.Restatement);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00006825 File Offset: 0x00004A25
		internal static bool ShouldGenerateAutoSuggests(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.AutoSuggest);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000682E File Offset: 0x00004A2E
		internal static bool ShouldGenerateAutoCompletes(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.AutoComplete);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00006837 File Offset: 0x00004A37
		internal static bool ShouldIncludeUnrecognizedTerms(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.UnrecognizedTerms);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00006841 File Offset: 0x00004A41
		internal static bool ShouldGenerateSemanticQuery(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SemanticQuery);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000684B File Offset: 0x00004A4B
		internal static bool UseBasicAnalysis(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.BasicAnalysis);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00006855 File Offset: 0x00004A55
		internal static bool ShouldSearchForHiddenEntities(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SearchForHiddenEntities);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00006862 File Offset: 0x00004A62
		internal static bool ShouldGenerateQueryMetadata(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.QueryMetadata);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000686F File Offset: 0x00004A6F
		internal static bool ShouldAllowMultipleEntityInstances(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.AllowMultipleEntityInstances);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000687C File Offset: 0x00004A7C
		internal static bool ShouldGenerateVisualConfiguration(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.VisualConfiguration);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00006889 File Offset: 0x00004A89
		internal static bool IsCortana(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.Cortana);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00006896 File Offset: 0x00004A96
		internal static bool IsCortanaFreeformResults(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.CortanaFreeformResults);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000068A3 File Offset: 0x00004AA3
		internal static bool ShouldUsePersonality(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.Personality);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000068B0 File Offset: 0x00004AB0
		internal static bool ShouldGenerateDiagnostics(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.Diagnostics);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000068BD File Offset: 0x00004ABD
		internal static bool ShouldInferNonquotedUnknownInstances(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.InferNonquotedUnknownInstances);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000068CA File Offset: 0x00004ACA
		internal static bool ShouldInferUnknownModelTerms(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.InferUnknownModelTerms);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000068D7 File Offset: 0x00004AD7
		internal static bool ShouldGenerateFrameTree(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.FrameTree);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000068E4 File Offset: 0x00004AE4
		internal static bool AllowFollowUpQuestion(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.AllowFollowUpQuestion);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000068F1 File Offset: 0x00004AF1
		internal static bool AllowInsightQueries(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.AllowInsightQueries);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000068FE File Offset: 0x00004AFE
		internal static bool SingleAnswerBias(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SingleAnswerBias);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000690B File Offset: 0x00004B0B
		internal static bool AllowPlaceholderSuggestions(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.AllowPlaceholderSuggestions);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00006918 File Offset: 0x00004B18
		internal static bool SuggestFollowUp(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SuggestFollowUp);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00006925 File Offset: 0x00004B25
		internal static bool GenerateSuggestedUtterances(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SuggestedUtterances);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00006932 File Offset: 0x00004B32
		internal static bool OutputQueryOnError(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.OutputQueryOnSemanticError);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000693F File Offset: 0x00004B3F
		internal static bool IsDataSearchRequest(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.DataSearch);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000694C File Offset: 0x00004B4C
		internal static bool ShouldSkipTableReferenceExpansionInSubquery(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SkipTableReferenceExpansionInSubquery);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00006959 File Offset: 0x00004B59
		internal static bool IsSamplingRequest(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SamplingRequest);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00006966 File Offset: 0x00004B66
		internal static bool ShouldSkipInference(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SkipInference);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00006973 File Offset: 0x00004B73
		internal static bool ShouldSuggestDefineCalculationUtterances(this InterpretRequestOptions option)
		{
			return InterpretRequestOptionsExtensions.HasFlag(option, InterpretRequestOptions.SuggestedDefineCalculationUtterances);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00006980 File Offset: 0x00004B80
		private static bool HasFlag(InterpretRequestOptions option, InterpretRequestOptions flag)
		{
			return (option & flag) == flag;
		}
	}
}
