using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200003E RID: 62
	public enum ActivityType
	{
		// Token: 0x04000067 RID: 103
		Interpret,
		// Token: 0x04000068 RID: 104
		LoadDataModel,
		// Token: 0x04000069 RID: 105
		LoadDependentDataModel,
		// Token: 0x0400006A RID: 106
		MergeDataModel,
		// Token: 0x0400006B RID: 107
		TextAnalysis,
		// Token: 0x0400006C RID: 108
		SampleValues,
		// Token: 0x0400006D RID: 109
		SyntaxAnalysis,
		// Token: 0x0400006E RID: 110
		InstanceLookup,
		// Token: 0x0400006F RID: 111
		SemanticAnalysis,
		// Token: 0x04000070 RID: 112
		AutoComplete,
		// Token: 0x04000071 RID: 113
		AutoSuggest,
		// Token: 0x04000072 RID: 114
		PrefixMatch,
		// Token: 0x04000073 RID: 115
		IndexLookup,
		// Token: 0x04000074 RID: 116
		Restatement,
		// Token: 0x04000075 RID: 117
		VisualGeneration,
		// Token: 0x04000076 RID: 118
		NotifyDatabaseChanges,
		// Token: 0x04000077 RID: 119
		LanguageModeling,
		// Token: 0x04000078 RID: 120
		GenerateLinguisticSchema,
		// Token: 0x04000079 RID: 121
		ExtractTerms,
		// Token: 0x0400007A RID: 122
		GenerateSampleUtterance,
		// Token: 0x0400007B RID: 123
		ReadConceptualSchema,
		// Token: 0x0400007C RID: 124
		ReadLinguisticSchema,
		// Token: 0x0400007D RID: 125
		UpgradeLinguisticSchema,
		// Token: 0x0400007E RID: 126
		ValidateLinguisticSchema,
		// Token: 0x0400007F RID: 127
		LinguisticSchemaHeuristics,
		// Token: 0x04000080 RID: 128
		AdjectiveOrAntonymRule,
		// Token: 0x04000081 RID: 129
		ColumnDataTypeDetectionRule,
		// Token: 0x04000082 RID: 130
		PropertyCategoryAndTypeWordsRule,
		// Token: 0x04000083 RID: 131
		ExternalSynonymGenerationRule,
		// Token: 0x04000084 RID: 132
		WordsToNounsRule,
		// Token: 0x04000085 RID: 133
		SemanticTypeDetectionRule,
		// Token: 0x04000086 RID: 134
		DynamicAdjectivePhrasingRule,
		// Token: 0x04000087 RID: 135
		NamePhrasingDefaultRule,
		// Token: 0x04000088 RID: 136
		DateOfBirthVerbRule,
		// Token: 0x04000089 RID: 137
		DateVariationEntitiesRule,
		// Token: 0x0400008A RID: 138
		NamePhrasingRule,
		// Token: 0x0400008B RID: 139
		NameOrIdentifierSynonymRule,
		// Token: 0x0400008C RID: 140
		WordsToRoleNounsRule,
		// Token: 0x0400008D RID: 141
		AdjectivePhrasingRule,
		// Token: 0x0400008E RID: 142
		PrepositionalVerbRule,
		// Token: 0x0400008F RID: 143
		ConceptualEntitiesRule,
		// Token: 0x04000090 RID: 144
		VerbOnDateRule,
		// Token: 0x04000091 RID: 145
		AttributePhrasingRule,
		// Token: 0x04000092 RID: 146
		PrepositionPhrasingRule,
		// Token: 0x04000093 RID: 147
		EntityUnitsRule,
		// Token: 0x04000094 RID: 148
		EntitySynonymsRule,
		// Token: 0x04000095 RID: 149
		AgentNounRule,
		// Token: 0x04000096 RID: 150
		GeoLocationRule,
		// Token: 0x04000097 RID: 151
		HiddenJoinTablesRule,
		// Token: 0x04000098 RID: 152
		LinguisticSchemaAnalyzer,
		// Token: 0x04000099 RID: 153
		LinguisticSchemaServicesConstruction,
		// Token: 0x0400009A RID: 154
		DependentLinguisticSchemaServicesConstruction,
		// Token: 0x0400009B RID: 155
		GetConceptualSchema,
		// Token: 0x0400009C RID: 156
		TermAnalyzer,
		// Token: 0x0400009D RID: 157
		DataLookup,
		// Token: 0x0400009E RID: 158
		FrameTree,
		// Token: 0x0400009F RID: 159
		BuildDataIndex,
		// Token: 0x040000A0 RID: 160
		OpenDataIndex,
		// Token: 0x040000A1 RID: 161
		SuggestFollowUp,
		// Token: 0x040000A2 RID: 162
		UnknownPatternTermInference,
		// Token: 0x040000A3 RID: 163
		NamelessTableEntityRule,
		// Token: 0x040000A4 RID: 164
		ModelTermLookup,
		// Token: 0x040000A5 RID: 165
		ModelTermPrefixLookup,
		// Token: 0x040000A6 RID: 166
		BuildModelTermIndex,
		// Token: 0x040000A7 RID: 167
		AmbiguousDefaultLabelRule,
		// Token: 0x040000A8 RID: 168
		DataSearch,
		// Token: 0x040000A9 RID: 169
		DataSearchFlow,
		// Token: 0x040000AA RID: 170
		ConceptualSemanticAnalyzer,
		// Token: 0x040000AB RID: 171
		InitializePatternRecognitionEngine,
		// Token: 0x040000AC RID: 172
		InitializePatternCompletionEngine,
		// Token: 0x040000AD RID: 173
		InferredRestatement,
		// Token: 0x040000AE RID: 174
		FetchCsdl,
		// Token: 0x040000AF RID: 175
		FetchLsdl,
		// Token: 0x040000B0 RID: 176
		FetchColumnStatistics,
		// Token: 0x040000B1 RID: 177
		FetchMeasureMetadata,
		// Token: 0x040000B2 RID: 178
		SaveSchemaMetadata,
		// Token: 0x040000B3 RID: 179
		InferRelatedEntities
	}
}
