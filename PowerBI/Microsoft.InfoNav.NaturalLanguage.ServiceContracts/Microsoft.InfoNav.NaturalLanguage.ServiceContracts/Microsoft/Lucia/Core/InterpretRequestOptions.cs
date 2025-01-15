using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000A1 RID: 161
	[DataContract]
	[Flags]
	public enum InterpretRequestOptions
	{
		// Token: 0x0400036F RID: 879
		NotSpecified = 0,
		// Token: 0x04000370 RID: 880
		[EnumMember]
		SpellCorrect = 1,
		// Token: 0x04000371 RID: 881
		[EnumMember]
		Restatement = 2,
		// Token: 0x04000372 RID: 882
		[EnumMember]
		AutoSuggest = 4,
		// Token: 0x04000373 RID: 883
		[EnumMember]
		AutoComplete = 8,
		// Token: 0x04000374 RID: 884
		[EnumMember]
		UnrecognizedTerms = 16,
		// Token: 0x04000375 RID: 885
		[EnumMember]
		SemanticQuery = 32,
		// Token: 0x04000376 RID: 886
		[EnumMember]
		BasicAnalysis = 64,
		// Token: 0x04000377 RID: 887
		[EnumMember]
		SearchForHiddenEntities = 128,
		// Token: 0x04000378 RID: 888
		[EnumMember]
		QueryMetadata = 256,
		// Token: 0x04000379 RID: 889
		[EnumMember]
		AllowMultipleEntityInstances = 512,
		// Token: 0x0400037A RID: 890
		[EnumMember]
		VisualConfiguration = 1024,
		// Token: 0x0400037B RID: 891
		[EnumMember]
		Cortana = 4096,
		// Token: 0x0400037C RID: 892
		[EnumMember]
		Personality = 8192,
		// Token: 0x0400037D RID: 893
		[EnumMember]
		TermBinding = 16384,
		// Token: 0x0400037E RID: 894
		[EnumMember]
		Diagnostics = 32768,
		// Token: 0x0400037F RID: 895
		[EnumMember]
		InferNonquotedUnknownInstances = 65536,
		// Token: 0x04000380 RID: 896
		[EnumMember]
		CortanaFreeformResults = 131072,
		// Token: 0x04000381 RID: 897
		[EnumMember]
		FrameTree = 262144,
		// Token: 0x04000382 RID: 898
		[EnumMember]
		AllowFollowUpQuestion = 524288,
		// Token: 0x04000383 RID: 899
		[EnumMember]
		AllowInsightQueries = 1048576,
		// Token: 0x04000384 RID: 900
		[EnumMember]
		InferUnknownModelTerms = 2097152,
		// Token: 0x04000385 RID: 901
		[EnumMember]
		AllowPlaceholderSuggestions = 4194304,
		// Token: 0x04000386 RID: 902
		[EnumMember]
		SuggestFollowUp = 8388608,
		// Token: 0x04000387 RID: 903
		[EnumMember]
		SuggestedUtterances = 16777216,
		// Token: 0x04000388 RID: 904
		[EnumMember]
		OutputQueryOnSemanticError = 33554432,
		// Token: 0x04000389 RID: 905
		[EnumMember]
		DataSearch = 67108864,
		// Token: 0x0400038A RID: 906
		[EnumMember]
		SingleAnswerBias = 134217728,
		// Token: 0x0400038B RID: 907
		[EnumMember]
		SkipTableReferenceExpansionInSubquery = 268435456,
		// Token: 0x0400038C RID: 908
		[EnumMember]
		SamplingRequest = 536870912,
		// Token: 0x0400038D RID: 909
		[EnumMember]
		SkipInference = 1073741824,
		// Token: 0x0400038E RID: 910
		[EnumMember]
		SuggestedDefineCalculationUtterances = -2147483648,
		// Token: 0x0400038F RID: 911
		[EnumMember]
		DefaultFlow = 47,
		// Token: 0x04000390 RID: 912
		[EnumMember]
		AllowMultipleEntityInstancesFlow = 559,
		// Token: 0x04000391 RID: 913
		[EnumMember]
		DefaultCortanaLightWeightFlow = 4105,
		// Token: 0x04000392 RID: 914
		[EnumMember]
		DefaultCortanaFlow = 4139,
		// Token: 0x04000393 RID: 915
		[EnumMember]
		DefaultAnnaTalkFlow = 786467,
		// Token: 0x04000394 RID: 916
		[EnumMember]
		DefaultTestFlow = 268435503
	}
}
