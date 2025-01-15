using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Data
{
	// Token: 0x02000014 RID: 20
	internal sealed class EntityRes
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003179 File Offset: 0x00001379
		internal EntityRes()
		{
			this.resources = new ResourceManager("System.Data.Entity", base.GetType().Assembly);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000319C File Offset: 0x0000139C
		private static EntityRes GetLoader()
		{
			if (EntityRes.loader == null)
			{
				EntityRes entityRes = new EntityRes();
				Interlocked.CompareExchange<EntityRes>(ref EntityRes.loader, entityRes, null);
			}
			return EntityRes.loader;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000031C8 File Offset: 0x000013C8
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000031CB File Offset: 0x000013CB
		public static ResourceManager Resources
		{
			get
			{
				return EntityRes.GetLoader().resources;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000031D8 File Offset: 0x000013D8
		public static string GetString(string name, params object[] args)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			string @string = entityRes.resources.GetString(name, EntityRes.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003258 File Offset: 0x00001458
		public static string GetString(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetString(name, EntityRes.Culture);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003281 File Offset: 0x00001481
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return EntityRes.GetString(name);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000328C File Offset: 0x0000148C
		public static object GetObject(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetObject(name, EntityRes.Culture);
		}

		// Token: 0x04000047 RID: 71
		internal const string EntityKey_DataRecordMustBeEntity = "EntityKey_DataRecordMustBeEntity";

		// Token: 0x04000048 RID: 72
		internal const string EntityKey_EntitySetDoesNotMatch = "EntityKey_EntitySetDoesNotMatch";

		// Token: 0x04000049 RID: 73
		internal const string EntityKey_EntityTypesDoNotMatch = "EntityKey_EntityTypesDoNotMatch";

		// Token: 0x0400004A RID: 74
		internal const string EntityKey_IncorrectNumberOfKeyValuePairs = "EntityKey_IncorrectNumberOfKeyValuePairs";

		// Token: 0x0400004B RID: 75
		internal const string EntityKey_IncorrectValueType = "EntityKey_IncorrectValueType";

		// Token: 0x0400004C RID: 76
		internal const string EntityKey_MissingKeyValue = "EntityKey_MissingKeyValue";

		// Token: 0x0400004D RID: 77
		internal const string EntityKey_NoNullsAllowedInKeyValuePairs = "EntityKey_NoNullsAllowedInKeyValuePairs";

		// Token: 0x0400004E RID: 78
		internal const string EntityKey_UnexpectedNull = "EntityKey_UnexpectedNull";

		// Token: 0x0400004F RID: 79
		internal const string EntityKey_DoesntMatchKeyOnEntity = "EntityKey_DoesntMatchKeyOnEntity";

		// Token: 0x04000050 RID: 80
		internal const string EntityKey_EntityKeyMustHaveValues = "EntityKey_EntityKeyMustHaveValues";

		// Token: 0x04000051 RID: 81
		internal const string EntityKey_InvalidQualifiedEntitySetName = "EntityKey_InvalidQualifiedEntitySetName";

		// Token: 0x04000052 RID: 82
		internal const string EntityKey_MissingEntitySetName = "EntityKey_MissingEntitySetName";

		// Token: 0x04000053 RID: 83
		internal const string EntityKey_InvalidName = "EntityKey_InvalidName";

		// Token: 0x04000054 RID: 84
		internal const string EntityKey_CannotChangeKey = "EntityKey_CannotChangeKey";

		// Token: 0x04000055 RID: 85
		internal const string EntityTypesDoNotAgree = "EntityTypesDoNotAgree";

		// Token: 0x04000056 RID: 86
		internal const string EntityKey_NullKeyValue = "EntityKey_NullKeyValue";

		// Token: 0x04000057 RID: 87
		internal const string EdmMembersDefiningTypeDoNotAgreeWithMetadataType = "EdmMembersDefiningTypeDoNotAgreeWithMetadataType";

		// Token: 0x04000058 RID: 88
		internal const string InvalidStringArgument = "InvalidStringArgument";

		// Token: 0x04000059 RID: 89
		internal const string EntityClient_ConnectionStringMissingInfo = "EntityClient_ConnectionStringMissingInfo";

		// Token: 0x0400005A RID: 90
		internal const string EntityClient_ValueNotString = "EntityClient_ValueNotString";

		// Token: 0x0400005B RID: 91
		internal const string EntityClient_KeywordNotSupported = "EntityClient_KeywordNotSupported";

		// Token: 0x0400005C RID: 92
		internal const string EntityClient_NoCommandText = "EntityClient_NoCommandText";

		// Token: 0x0400005D RID: 93
		internal const string EntityClient_ConnectionStringNeededBeforeOperation = "EntityClient_ConnectionStringNeededBeforeOperation";

		// Token: 0x0400005E RID: 94
		internal const string EntityClient_CannotReopenConnection = "EntityClient_CannotReopenConnection";

		// Token: 0x0400005F RID: 95
		internal const string EntityClient_ConnectionNotOpen = "EntityClient_ConnectionNotOpen";

		// Token: 0x04000060 RID: 96
		internal const string EntityClient_DuplicateParameterNames = "EntityClient_DuplicateParameterNames";

		// Token: 0x04000061 RID: 97
		internal const string EntityClient_NoConnectionForCommand = "EntityClient_NoConnectionForCommand";

		// Token: 0x04000062 RID: 98
		internal const string EntityClient_NoConnectionForAdapter = "EntityClient_NoConnectionForAdapter";

		// Token: 0x04000063 RID: 99
		internal const string EntityClient_ClosedConnectionForUpdate = "EntityClient_ClosedConnectionForUpdate";

		// Token: 0x04000064 RID: 100
		internal const string EntityClient_InvalidNamedConnection = "EntityClient_InvalidNamedConnection";

		// Token: 0x04000065 RID: 101
		internal const string EntityClient_NestedNamedConnection = "EntityClient_NestedNamedConnection";

		// Token: 0x04000066 RID: 102
		internal const string EntityClient_InvalidStoreProvider = "EntityClient_InvalidStoreProvider";

		// Token: 0x04000067 RID: 103
		internal const string EntityClient_DataReaderIsStillOpen = "EntityClient_DataReaderIsStillOpen";

		// Token: 0x04000068 RID: 104
		internal const string EntityClient_SettingsCannotBeChangedOnOpenConnection = "EntityClient_SettingsCannotBeChangedOnOpenConnection";

		// Token: 0x04000069 RID: 105
		internal const string EntityClient_ExecutingOnClosedConnection = "EntityClient_ExecutingOnClosedConnection";

		// Token: 0x0400006A RID: 106
		internal const string EntityClient_ConnectionStateClosed = "EntityClient_ConnectionStateClosed";

		// Token: 0x0400006B RID: 107
		internal const string EntityClient_ConnectionStateBroken = "EntityClient_ConnectionStateBroken";

		// Token: 0x0400006C RID: 108
		internal const string EntityClient_CannotCloneStoreProvider = "EntityClient_CannotCloneStoreProvider";

		// Token: 0x0400006D RID: 109
		internal const string EntityClient_UnsupportedCommandType = "EntityClient_UnsupportedCommandType";

		// Token: 0x0400006E RID: 110
		internal const string EntityClient_ErrorInClosingConnection = "EntityClient_ErrorInClosingConnection";

		// Token: 0x0400006F RID: 111
		internal const string EntityClient_ErrorInBeginningTransaction = "EntityClient_ErrorInBeginningTransaction";

		// Token: 0x04000070 RID: 112
		internal const string EntityClient_ExtraParametersWithNamedConnection = "EntityClient_ExtraParametersWithNamedConnection";

		// Token: 0x04000071 RID: 113
		internal const string EntityClient_CommandDefinitionPreparationFailed = "EntityClient_CommandDefinitionPreparationFailed";

		// Token: 0x04000072 RID: 114
		internal const string EntityClient_CommandDefinitionExecutionFailed = "EntityClient_CommandDefinitionExecutionFailed";

		// Token: 0x04000073 RID: 115
		internal const string EntityClient_CommandExecutionFailed = "EntityClient_CommandExecutionFailed";

		// Token: 0x04000074 RID: 116
		internal const string EntityClient_StoreReaderFailed = "EntityClient_StoreReaderFailed";

		// Token: 0x04000075 RID: 117
		internal const string EntityClient_FailedToGetInformation = "EntityClient_FailedToGetInformation";

		// Token: 0x04000076 RID: 118
		internal const string EntityClient_TooFewColumns = "EntityClient_TooFewColumns";

		// Token: 0x04000077 RID: 119
		internal const string EntityClient_InvalidParameterName = "EntityClient_InvalidParameterName";

		// Token: 0x04000078 RID: 120
		internal const string EntityClient_EmptyParameterName = "EntityClient_EmptyParameterName";

		// Token: 0x04000079 RID: 121
		internal const string EntityClient_ReturnedNullOnProviderMethod = "EntityClient_ReturnedNullOnProviderMethod";

		// Token: 0x0400007A RID: 122
		internal const string EntityClient_CannotDeduceDbType = "EntityClient_CannotDeduceDbType";

		// Token: 0x0400007B RID: 123
		internal const string EntityClient_InvalidParameterDirection = "EntityClient_InvalidParameterDirection";

		// Token: 0x0400007C RID: 124
		internal const string EntityClient_UnknownParameterType = "EntityClient_UnknownParameterType";

		// Token: 0x0400007D RID: 125
		internal const string EntityClient_UnsupportedDbType = "EntityClient_UnsupportedDbType";

		// Token: 0x0400007E RID: 126
		internal const string EntityClient_DoesNotImplementIServiceProvider = "EntityClient_DoesNotImplementIServiceProvider";

		// Token: 0x0400007F RID: 127
		internal const string EntityClient_IncompatibleNavigationPropertyResult = "EntityClient_IncompatibleNavigationPropertyResult";

		// Token: 0x04000080 RID: 128
		internal const string EntityClient_TransactionAlreadyStarted = "EntityClient_TransactionAlreadyStarted";

		// Token: 0x04000081 RID: 129
		internal const string EntityClient_InvalidTransactionForCommand = "EntityClient_InvalidTransactionForCommand";

		// Token: 0x04000082 RID: 130
		internal const string EntityClient_NoStoreConnectionForUpdate = "EntityClient_NoStoreConnectionForUpdate";

		// Token: 0x04000083 RID: 131
		internal const string EntityClient_CommandTreeMetadataIncompatible = "EntityClient_CommandTreeMetadataIncompatible";

		// Token: 0x04000084 RID: 132
		internal const string EntityClient_ProviderGeneralError = "EntityClient_ProviderGeneralError";

		// Token: 0x04000085 RID: 133
		internal const string EntityClient_ProviderSpecificError = "EntityClient_ProviderSpecificError";

		// Token: 0x04000086 RID: 134
		internal const string EntityClient_FunctionImportEmptyCommandText = "EntityClient_FunctionImportEmptyCommandText";

		// Token: 0x04000087 RID: 135
		internal const string EntityClient_UnableToFindFunctionImportContainer = "EntityClient_UnableToFindFunctionImportContainer";

		// Token: 0x04000088 RID: 136
		internal const string EntityClient_UnableToFindFunctionImport = "EntityClient_UnableToFindFunctionImport";

		// Token: 0x04000089 RID: 137
		internal const string EntityClient_UnmappedFunctionImport = "EntityClient_UnmappedFunctionImport";

		// Token: 0x0400008A RID: 138
		internal const string EntityClient_InvalidStoredProcedureCommandText = "EntityClient_InvalidStoredProcedureCommandText";

		// Token: 0x0400008B RID: 139
		internal const string EntityClient_ItemCollectionsNotRegisteredInWorkspace = "EntityClient_ItemCollectionsNotRegisteredInWorkspace";

		// Token: 0x0400008C RID: 140
		internal const string EntityClient_ConnectionMustBeClosed = "EntityClient_ConnectionMustBeClosed";

		// Token: 0x0400008D RID: 141
		internal const string EntityClient_DbConnectionHasNoProvider = "EntityClient_DbConnectionHasNoProvider";

		// Token: 0x0400008E RID: 142
		internal const string EntityClient_RequiresNonStoreCommandTree = "EntityClient_RequiresNonStoreCommandTree";

		// Token: 0x0400008F RID: 143
		internal const string EntityClient_CannotReprepareCommandDefinitionBasedCommand = "EntityClient_CannotReprepareCommandDefinitionBasedCommand";

		// Token: 0x04000090 RID: 144
		internal const string ELinq_ExpressionMustBeIQueryable = "ELinq_ExpressionMustBeIQueryable";

		// Token: 0x04000091 RID: 145
		internal const string ELinq_UnsupportedExpressionType = "ELinq_UnsupportedExpressionType";

		// Token: 0x04000092 RID: 146
		internal const string ELinq_UnsupportedUseOfContextParameter = "ELinq_UnsupportedUseOfContextParameter";

		// Token: 0x04000093 RID: 147
		internal const string ELinq_UnboundParameterExpression = "ELinq_UnboundParameterExpression";

		// Token: 0x04000094 RID: 148
		internal const string ELinq_UnsupportedConstructor = "ELinq_UnsupportedConstructor";

		// Token: 0x04000095 RID: 149
		internal const string ELinq_UnsupportedInitializers = "ELinq_UnsupportedInitializers";

		// Token: 0x04000096 RID: 150
		internal const string ELinq_UnsupportedBinding = "ELinq_UnsupportedBinding";

		// Token: 0x04000097 RID: 151
		internal const string ELinq_UnsupportedMethod = "ELinq_UnsupportedMethod";

		// Token: 0x04000098 RID: 152
		internal const string ELinq_UnsupportedMethodSuggestedAlternative = "ELinq_UnsupportedMethodSuggestedAlternative";

		// Token: 0x04000099 RID: 153
		internal const string ELinq_ThenByDoesNotFollowOrderBy = "ELinq_ThenByDoesNotFollowOrderBy";

		// Token: 0x0400009A RID: 154
		internal const string ELinq_UnrecognizedMember = "ELinq_UnrecognizedMember";

		// Token: 0x0400009B RID: 155
		internal const string ELinq_UnresolvableFunctionForMethod = "ELinq_UnresolvableFunctionForMethod";

		// Token: 0x0400009C RID: 156
		internal const string ELinq_UnresolvableFunctionForMethodAmbiguousMatch = "ELinq_UnresolvableFunctionForMethodAmbiguousMatch";

		// Token: 0x0400009D RID: 157
		internal const string ELinq_UnresolvableFunctionForMethodNotFound = "ELinq_UnresolvableFunctionForMethodNotFound";

		// Token: 0x0400009E RID: 158
		internal const string ELinq_UnresolvableFunctionForMember = "ELinq_UnresolvableFunctionForMember";

		// Token: 0x0400009F RID: 159
		internal const string ELinq_UnresolvableStoreFunctionForMember = "ELinq_UnresolvableStoreFunctionForMember";

		// Token: 0x040000A0 RID: 160
		internal const string ELinq_UnresolvableFunctionForExpression = "ELinq_UnresolvableFunctionForExpression";

		// Token: 0x040000A1 RID: 161
		internal const string ELinq_UnresolvableStoreFunctionForExpression = "ELinq_UnresolvableStoreFunctionForExpression";

		// Token: 0x040000A2 RID: 162
		internal const string ELinq_UnsupportedType = "ELinq_UnsupportedType";

		// Token: 0x040000A3 RID: 163
		internal const string ELinq_UnsupportedNullConstant = "ELinq_UnsupportedNullConstant";

		// Token: 0x040000A4 RID: 164
		internal const string ELinq_UnsupportedConstant = "ELinq_UnsupportedConstant";

		// Token: 0x040000A5 RID: 165
		internal const string ELinq_UnsupportedCast = "ELinq_UnsupportedCast";

		// Token: 0x040000A6 RID: 166
		internal const string ELinq_UnsupportedIsOrAs = "ELinq_UnsupportedIsOrAs";

		// Token: 0x040000A7 RID: 167
		internal const string ELinq_UnsupportedQueryableMethod = "ELinq_UnsupportedQueryableMethod";

		// Token: 0x040000A8 RID: 168
		internal const string ELinq_InvalidOfTypeResult = "ELinq_InvalidOfTypeResult";

		// Token: 0x040000A9 RID: 169
		internal const string ELinq_UnsupportedNominalType = "ELinq_UnsupportedNominalType";

		// Token: 0x040000AA RID: 170
		internal const string ELinq_UnsupportedEnumerableType = "ELinq_UnsupportedEnumerableType";

		// Token: 0x040000AB RID: 171
		internal const string ELinq_UnsupportedHeterogeneousInitializers = "ELinq_UnsupportedHeterogeneousInitializers";

		// Token: 0x040000AC RID: 172
		internal const string ELinq_UnsupportedDifferentContexts = "ELinq_UnsupportedDifferentContexts";

		// Token: 0x040000AD RID: 173
		internal const string ELinq_UnsupportedCastToDecimal = "ELinq_UnsupportedCastToDecimal";

		// Token: 0x040000AE RID: 174
		internal const string ELinq_UnsupportedKeySelector = "ELinq_UnsupportedKeySelector";

		// Token: 0x040000AF RID: 175
		internal const string ELinq_CreateOrderedEnumerableNotSupported = "ELinq_CreateOrderedEnumerableNotSupported";

		// Token: 0x040000B0 RID: 176
		internal const string ELinq_UnsupportedPassthrough = "ELinq_UnsupportedPassthrough";

		// Token: 0x040000B1 RID: 177
		internal const string ELinq_UnexpectedTypeForNavigationProperty = "ELinq_UnexpectedTypeForNavigationProperty";

		// Token: 0x040000B2 RID: 178
		internal const string ELinq_SkipWithoutOrder = "ELinq_SkipWithoutOrder";

		// Token: 0x040000B3 RID: 179
		internal const string ELinq_PropertyIndexNotSupported = "ELinq_PropertyIndexNotSupported";

		// Token: 0x040000B4 RID: 180
		internal const string ELinq_NotPropertyOrField = "ELinq_NotPropertyOrField";

		// Token: 0x040000B5 RID: 181
		internal const string ELinq_UnsupportedStringRemoveCase = "ELinq_UnsupportedStringRemoveCase";

		// Token: 0x040000B6 RID: 182
		internal const string ELinq_UnsupportedTrimStartTrimEndCase = "ELinq_UnsupportedTrimStartTrimEndCase";

		// Token: 0x040000B7 RID: 183
		internal const string ELinq_UnsupportedVBDatePartNonConstantInterval = "ELinq_UnsupportedVBDatePartNonConstantInterval";

		// Token: 0x040000B8 RID: 184
		internal const string ELinq_UnsupportedVBDatePartInvalidInterval = "ELinq_UnsupportedVBDatePartInvalidInterval";

		// Token: 0x040000B9 RID: 185
		internal const string ELinq_UnsupportedAsUnicodeAndAsNonUnicode = "ELinq_UnsupportedAsUnicodeAndAsNonUnicode";

		// Token: 0x040000BA RID: 186
		internal const string ELinq_UnsupportedComparison = "ELinq_UnsupportedComparison";

		// Token: 0x040000BB RID: 187
		internal const string ELinq_UnsupportedRefComparison = "ELinq_UnsupportedRefComparison";

		// Token: 0x040000BC RID: 188
		internal const string ELinq_UnsupportedRowComparison = "ELinq_UnsupportedRowComparison";

		// Token: 0x040000BD RID: 189
		internal const string ELinq_UnsupportedRowMemberComparison = "ELinq_UnsupportedRowMemberComparison";

		// Token: 0x040000BE RID: 190
		internal const string ELinq_UnsupportedRowTypeComparison = "ELinq_UnsupportedRowTypeComparison";

		// Token: 0x040000BF RID: 191
		internal const string ELinq_PrimitiveTypesSample = "ELinq_PrimitiveTypesSample";

		// Token: 0x040000C0 RID: 192
		internal const string ELinq_AnonymousType = "ELinq_AnonymousType";

		// Token: 0x040000C1 RID: 193
		internal const string ELinq_ClosureType = "ELinq_ClosureType";

		// Token: 0x040000C2 RID: 194
		internal const string ELinq_UnhandledExpressionType = "ELinq_UnhandledExpressionType";

		// Token: 0x040000C3 RID: 195
		internal const string ELinq_UnhandledBindingType = "ELinq_UnhandledBindingType";

		// Token: 0x040000C4 RID: 196
		internal const string ELinq_UnsupportedNestedFirst = "ELinq_UnsupportedNestedFirst";

		// Token: 0x040000C5 RID: 197
		internal const string ELinq_UnsupportedNestedSingle = "ELinq_UnsupportedNestedSingle";

		// Token: 0x040000C6 RID: 198
		internal const string ELinq_UnsupportedInclude = "ELinq_UnsupportedInclude";

		// Token: 0x040000C7 RID: 199
		internal const string ELinq_UnsupportedMergeAs = "ELinq_UnsupportedMergeAs";

		// Token: 0x040000C8 RID: 200
		internal const string ELinq_MethodNotDirectlyCallable = "ELinq_MethodNotDirectlyCallable";

		// Token: 0x040000C9 RID: 201
		internal const string ELinq_CycleDetected = "ELinq_CycleDetected";

		// Token: 0x040000CA RID: 202
		internal const string ELinq_EdmFunctionAttributeParameterNameNotValid = "ELinq_EdmFunctionAttributeParameterNameNotValid";

		// Token: 0x040000CB RID: 203
		internal const string ELinq_EdmFunctionAttributedFunctionWithWrongReturnType = "ELinq_EdmFunctionAttributedFunctionWithWrongReturnType";

		// Token: 0x040000CC RID: 204
		internal const string ELinq_EdmFunctionAttributedFunctionWithWrongInstance = "ELinq_EdmFunctionAttributedFunctionWithWrongInstance";

		// Token: 0x040000CD RID: 205
		internal const string ELinq_EdmFunctionDirectCall = "ELinq_EdmFunctionDirectCall";

		// Token: 0x040000CE RID: 206
		internal const string CompiledELinq_UnsupportedParameterTypes = "CompiledELinq_UnsupportedParameterTypes";

		// Token: 0x040000CF RID: 207
		internal const string CompiledELinq_UnsupportedNamedParameterType = "CompiledELinq_UnsupportedNamedParameterType";

		// Token: 0x040000D0 RID: 208
		internal const string CompiledELinq_UnsupportedNamedParameterUseAsType = "CompiledELinq_UnsupportedNamedParameterUseAsType";

		// Token: 0x040000D1 RID: 209
		internal const string Update_UnsupportedExpressionKind = "Update_UnsupportedExpressionKind";

		// Token: 0x040000D2 RID: 210
		internal const string Update_UnsupportedCastArgument = "Update_UnsupportedCastArgument";

		// Token: 0x040000D3 RID: 211
		internal const string Update_UnsupportedExtentType = "Update_UnsupportedExtentType";

		// Token: 0x040000D4 RID: 212
		internal const string Update_ConstraintCycle = "Update_ConstraintCycle";

		// Token: 0x040000D5 RID: 213
		internal const string Update_UnsupportedJoinType = "Update_UnsupportedJoinType";

		// Token: 0x040000D6 RID: 214
		internal const string Update_UnsupportedProjection = "Update_UnsupportedProjection";

		// Token: 0x040000D7 RID: 215
		internal const string Update_ConcurrencyError = "Update_ConcurrencyError";

		// Token: 0x040000D8 RID: 216
		internal const string Update_MissingEntity = "Update_MissingEntity";

		// Token: 0x040000D9 RID: 217
		internal const string Update_RelationshipCardinalityConstraintViolation = "Update_RelationshipCardinalityConstraintViolation";

		// Token: 0x040000DA RID: 218
		internal const string Update_GeneralExecutionException = "Update_GeneralExecutionException";

		// Token: 0x040000DB RID: 219
		internal const string Update_MissingRequiredEntity = "Update_MissingRequiredEntity";

		// Token: 0x040000DC RID: 220
		internal const string Update_RelationshipCardinalityViolation = "Update_RelationshipCardinalityViolation";

		// Token: 0x040000DD RID: 221
		internal const string Update_NotSupportedServerGenKey = "Update_NotSupportedServerGenKey";

		// Token: 0x040000DE RID: 222
		internal const string Update_NotSupportedIdentityType = "Update_NotSupportedIdentityType";

		// Token: 0x040000DF RID: 223
		internal const string Update_NotSupportedComputedKeyColumn = "Update_NotSupportedComputedKeyColumn";

		// Token: 0x040000E0 RID: 224
		internal const string Update_AmbiguousServerGenIdentifier = "Update_AmbiguousServerGenIdentifier";

		// Token: 0x040000E1 RID: 225
		internal const string Update_WorkspaceMismatch = "Update_WorkspaceMismatch";

		// Token: 0x040000E2 RID: 226
		internal const string Update_MissingRequiredRelationshipValue = "Update_MissingRequiredRelationshipValue";

		// Token: 0x040000E3 RID: 227
		internal const string Update_MissingResultColumn = "Update_MissingResultColumn";

		// Token: 0x040000E4 RID: 228
		internal const string Update_NullReturnValueForNonNullableMember = "Update_NullReturnValueForNonNullableMember";

		// Token: 0x040000E5 RID: 229
		internal const string Update_ReturnValueHasUnexpectedType = "Update_ReturnValueHasUnexpectedType";

		// Token: 0x040000E6 RID: 230
		internal const string Update_SqlEntitySetWithoutDmlFunctions = "Update_SqlEntitySetWithoutDmlFunctions";

		// Token: 0x040000E7 RID: 231
		internal const string Update_UnableToConvertRowsAffectedParameterToInt32 = "Update_UnableToConvertRowsAffectedParameterToInt32";

		// Token: 0x040000E8 RID: 232
		internal const string Update_MappingNotFound = "Update_MappingNotFound";

		// Token: 0x040000E9 RID: 233
		internal const string Update_ModifyingIdentityColumn = "Update_ModifyingIdentityColumn";

		// Token: 0x040000EA RID: 234
		internal const string Update_GeneratedDependent = "Update_GeneratedDependent";

		// Token: 0x040000EB RID: 235
		internal const string Update_ReferentialConstraintIntegrityViolation = "Update_ReferentialConstraintIntegrityViolation";

		// Token: 0x040000EC RID: 236
		internal const string Update_ErrorLoadingRecord = "Update_ErrorLoadingRecord";

		// Token: 0x040000ED RID: 237
		internal const string Update_NullValue = "Update_NullValue";

		// Token: 0x040000EE RID: 238
		internal const string Update_CircularRelationships = "Update_CircularRelationships";

		// Token: 0x040000EF RID: 239
		internal const string Update_RelationshipCardinalityConstraintViolationSingleValue = "Update_RelationshipCardinalityConstraintViolationSingleValue";

		// Token: 0x040000F0 RID: 240
		internal const string Update_MissingFunctionMapping = "Update_MissingFunctionMapping";

		// Token: 0x040000F1 RID: 241
		internal const string Update_InvalidChanges = "Update_InvalidChanges";

		// Token: 0x040000F2 RID: 242
		internal const string Update_DuplicateKeys = "Update_DuplicateKeys";

		// Token: 0x040000F3 RID: 243
		internal const string Update_AmbiguousForeignKey = "Update_AmbiguousForeignKey";

		// Token: 0x040000F4 RID: 244
		internal const string Update_InsertingOrUpdatingReferenceToDeletedEntity = "Update_InsertingOrUpdatingReferenceToDeletedEntity";

		// Token: 0x040000F5 RID: 245
		internal const string ViewGen_Extent = "ViewGen_Extent";

		// Token: 0x040000F6 RID: 246
		internal const string ViewGen_Null = "ViewGen_Null";

		// Token: 0x040000F7 RID: 247
		internal const string ViewGen_CommaBlank = "ViewGen_CommaBlank";

		// Token: 0x040000F8 RID: 248
		internal const string ViewGen_Entities = "ViewGen_Entities";

		// Token: 0x040000F9 RID: 249
		internal const string ViewGen_Tuples = "ViewGen_Tuples";

		// Token: 0x040000FA RID: 250
		internal const string ViewGen_NotNull = "ViewGen_NotNull";

		// Token: 0x040000FB RID: 251
		internal const string ViewGen_Failure = "ViewGen_Failure";

		// Token: 0x040000FC RID: 252
		internal const string ViewGen_NegatedCellConstant_0 = "ViewGen_NegatedCellConstant_0";

		// Token: 0x040000FD RID: 253
		internal const string ViewGen_Error = "ViewGen_Error";

		// Token: 0x040000FE RID: 254
		internal const string ViewGen_AND = "ViewGen_AND";

		// Token: 0x040000FF RID: 255
		internal const string Viewgen_CannotGenerateQueryViewUnderNoValidation = "Viewgen_CannotGenerateQueryViewUnderNoValidation";

		// Token: 0x04000100 RID: 256
		internal const string ViewGen_Missing_Sets_Mapping_0 = "ViewGen_Missing_Sets_Mapping_0";

		// Token: 0x04000101 RID: 257
		internal const string ViewGen_Missing_Type_Mapping_0 = "ViewGen_Missing_Type_Mapping_0";

		// Token: 0x04000102 RID: 258
		internal const string ViewGen_Missing_Set_Mapping_0 = "ViewGen_Missing_Set_Mapping_0";

		// Token: 0x04000103 RID: 259
		internal const string ViewGen_Concurrency_Derived_Class_2 = "ViewGen_Concurrency_Derived_Class_2";

		// Token: 0x04000104 RID: 260
		internal const string ViewGen_Concurrency_Invalid_Condition_1 = "ViewGen_Concurrency_Invalid_Condition_1";

		// Token: 0x04000105 RID: 261
		internal const string ViewGen_TableKey_Missing_1 = "ViewGen_TableKey_Missing_1";

		// Token: 0x04000106 RID: 262
		internal const string ViewGen_EntitySetKey_Missing_1 = "ViewGen_EntitySetKey_Missing_1";

		// Token: 0x04000107 RID: 263
		internal const string ViewGen_AssociationSetKey_Missing_2 = "ViewGen_AssociationSetKey_Missing_2";

		// Token: 0x04000108 RID: 264
		internal const string ViewGen_Cannot_Recover_Attributes_2 = "ViewGen_Cannot_Recover_Attributes_2";

		// Token: 0x04000109 RID: 265
		internal const string ViewGen_Cannot_Recover_Types_1 = "ViewGen_Cannot_Recover_Types_1";

		// Token: 0x0400010A RID: 266
		internal const string ViewGen_Cannot_Disambiguate_MultiConstant_2 = "ViewGen_Cannot_Disambiguate_MultiConstant_2";

		// Token: 0x0400010B RID: 267
		internal const string ViewGen_No_Default_Value_1 = "ViewGen_No_Default_Value_1";

		// Token: 0x0400010C RID: 268
		internal const string ViewGen_No_Default_Value_For_Configuration_0 = "ViewGen_No_Default_Value_For_Configuration_0";

		// Token: 0x0400010D RID: 269
		internal const string ViewGen_KeyConstraint_Violation_5 = "ViewGen_KeyConstraint_Violation_5";

		// Token: 0x0400010E RID: 270
		internal const string ViewGen_KeyConstraint_Update_Violation_EntitySet_3 = "ViewGen_KeyConstraint_Update_Violation_EntitySet_3";

		// Token: 0x0400010F RID: 271
		internal const string ViewGen_KeyConstraint_Update_Violation_AssociationSet_2 = "ViewGen_KeyConstraint_Update_Violation_AssociationSet_2";

		// Token: 0x04000110 RID: 272
		internal const string ViewGen__AssociationEndShouldBeMappedToKey = "ViewGen__AssociationEndShouldBeMappedToKey";

		// Token: 0x04000111 RID: 273
		internal const string ViewGen_Duplicate_CProperties_0 = "ViewGen_Duplicate_CProperties_0";

		// Token: 0x04000112 RID: 274
		internal const string ViewGen_Duplicate_CProperties_IsMapped_1 = "ViewGen_Duplicate_CProperties_IsMapped_1";

		// Token: 0x04000113 RID: 275
		internal const string ViewGen_NotNull_No_Projected_Slot_0 = "ViewGen_NotNull_No_Projected_Slot_0";

		// Token: 0x04000114 RID: 276
		internal const string ViewGen_InvalidCondition_0 = "ViewGen_InvalidCondition_0";

		// Token: 0x04000115 RID: 277
		internal const string ViewGen_NonKeyProjectedWithOverlappingPartitions_0 = "ViewGen_NonKeyProjectedWithOverlappingPartitions_0";

		// Token: 0x04000116 RID: 278
		internal const string ViewGen_CQ_PartitionConstraint_1 = "ViewGen_CQ_PartitionConstraint_1";

		// Token: 0x04000117 RID: 279
		internal const string ViewGen_CQ_DomainConstraint_1 = "ViewGen_CQ_DomainConstraint_1";

		// Token: 0x04000118 RID: 280
		internal const string ViewGen_OneOfConst_MustBeNonNullable_0 = "ViewGen_OneOfConst_MustBeNonNullable_0";

		// Token: 0x04000119 RID: 281
		internal const string ViewGen_OneOfConst_MustBeNull_0 = "ViewGen_OneOfConst_MustBeNull_0";

		// Token: 0x0400011A RID: 282
		internal const string ViewGen_OneOfConst_MustBeEqualTo_1 = "ViewGen_OneOfConst_MustBeEqualTo_1";

		// Token: 0x0400011B RID: 283
		internal const string ViewGen_OneOfConst_MustNotBeEqualTo_1 = "ViewGen_OneOfConst_MustNotBeEqualTo_1";

		// Token: 0x0400011C RID: 284
		internal const string ViewGen_OneOfConst_MustBeOneOf_1 = "ViewGen_OneOfConst_MustBeOneOf_1";

		// Token: 0x0400011D RID: 285
		internal const string ViewGen_OneOfConst_MustNotBeOneOf_1 = "ViewGen_OneOfConst_MustNotBeOneOf_1";

		// Token: 0x0400011E RID: 286
		internal const string ViewGen_OneOfConst_IsNonNullable_0 = "ViewGen_OneOfConst_IsNonNullable_0";

		// Token: 0x0400011F RID: 287
		internal const string ViewGen_OneOfConst_IsEqualTo_1 = "ViewGen_OneOfConst_IsEqualTo_1";

		// Token: 0x04000120 RID: 288
		internal const string ViewGen_OneOfConst_IsNotEqualTo_1 = "ViewGen_OneOfConst_IsNotEqualTo_1";

		// Token: 0x04000121 RID: 289
		internal const string ViewGen_OneOfConst_IsOneOf_1 = "ViewGen_OneOfConst_IsOneOf_1";

		// Token: 0x04000122 RID: 290
		internal const string ViewGen_OneOfConst_IsNotOneOf_1 = "ViewGen_OneOfConst_IsNotOneOf_1";

		// Token: 0x04000123 RID: 291
		internal const string ViewGen_OneOfConst_IsOneOfTypes_0 = "ViewGen_OneOfConst_IsOneOfTypes_0";

		// Token: 0x04000124 RID: 292
		internal const string ViewGen_ErrorLog_0 = "ViewGen_ErrorLog_0";

		// Token: 0x04000125 RID: 293
		internal const string ViewGen_ErrorLog_1 = "ViewGen_ErrorLog_1";

		// Token: 0x04000126 RID: 294
		internal const string ViewGen_Foreign_Key_Missing_Table_Mapping_1 = "ViewGen_Foreign_Key_Missing_Table_Mapping_1";

		// Token: 0x04000127 RID: 295
		internal const string ViewGen_Foreign_Key_ParentTable_NotMappedToEnd_5 = "ViewGen_Foreign_Key_ParentTable_NotMappedToEnd_5";

		// Token: 0x04000128 RID: 296
		internal const string ViewGen_Foreign_Key_4 = "ViewGen_Foreign_Key_4";

		// Token: 0x04000129 RID: 297
		internal const string ViewGen_Foreign_Key_UpperBound_MustBeOne_2 = "ViewGen_Foreign_Key_UpperBound_MustBeOne_2";

		// Token: 0x0400012A RID: 298
		internal const string ViewGen_Foreign_Key_LowerBound_MustBeOne_2 = "ViewGen_Foreign_Key_LowerBound_MustBeOne_2";

		// Token: 0x0400012B RID: 299
		internal const string ViewGen_Foreign_Key_Missing_Relationship_Mapping_0 = "ViewGen_Foreign_Key_Missing_Relationship_Mapping_0";

		// Token: 0x0400012C RID: 300
		internal const string ViewGen_Foreign_Key_Not_Guaranteed_InCSpace_0 = "ViewGen_Foreign_Key_Not_Guaranteed_InCSpace_0";

		// Token: 0x0400012D RID: 301
		internal const string ViewGen_Foreign_Key_ColumnOrder_Incorrect_8 = "ViewGen_Foreign_Key_ColumnOrder_Incorrect_8";

		// Token: 0x0400012E RID: 302
		internal const string ViewGen_AssociationSet_AsUserString = "ViewGen_AssociationSet_AsUserString";

		// Token: 0x0400012F RID: 303
		internal const string ViewGen_AssociationSet_AsUserString_Negated = "ViewGen_AssociationSet_AsUserString_Negated";

		// Token: 0x04000130 RID: 304
		internal const string ViewGen_EntitySet_AsUserString = "ViewGen_EntitySet_AsUserString";

		// Token: 0x04000131 RID: 305
		internal const string ViewGen_EntitySet_AsUserString_Negated = "ViewGen_EntitySet_AsUserString_Negated";

		// Token: 0x04000132 RID: 306
		internal const string ViewGen_EntityInstanceToken = "ViewGen_EntityInstanceToken";

		// Token: 0x04000133 RID: 307
		internal const string Viewgen_ConfigurationErrorMsg = "Viewgen_ConfigurationErrorMsg";

		// Token: 0x04000134 RID: 308
		internal const string ViewGen_HashOnMappingClosure_Not_Matching = "ViewGen_HashOnMappingClosure_Not_Matching";

		// Token: 0x04000135 RID: 309
		internal const string Viewgen_RightSideNotDisjoint = "Viewgen_RightSideNotDisjoint";

		// Token: 0x04000136 RID: 310
		internal const string Viewgen_QV_RewritingNotFound = "Viewgen_QV_RewritingNotFound";

		// Token: 0x04000137 RID: 311
		internal const string Viewgen_NullableMappingForNonNullableColumn = "Viewgen_NullableMappingForNonNullableColumn";

		// Token: 0x04000138 RID: 312
		internal const string Viewgen_ErrorPattern_ConditionMemberIsMapped = "Viewgen_ErrorPattern_ConditionMemberIsMapped";

		// Token: 0x04000139 RID: 313
		internal const string Viewgen_ErrorPattern_DuplicateConditionValue = "Viewgen_ErrorPattern_DuplicateConditionValue";

		// Token: 0x0400013A RID: 314
		internal const string Viewgen_ErrorPattern_TableMappedToMultipleES = "Viewgen_ErrorPattern_TableMappedToMultipleES";

		// Token: 0x0400013B RID: 315
		internal const string Viewgen_ErrorPattern_Partition_Disj_Eq = "Viewgen_ErrorPattern_Partition_Disj_Eq";

		// Token: 0x0400013C RID: 316
		internal const string Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember = "Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember";

		// Token: 0x0400013D RID: 317
		internal const string Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition = "Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition";

		// Token: 0x0400013E RID: 318
		internal const string Viewgen_ErrorPattern_Partition_Disj_Subs_Ref = "Viewgen_ErrorPattern_Partition_Disj_Subs_Ref";

		// Token: 0x0400013F RID: 319
		internal const string Viewgen_ErrorPattern_Partition_Disj_Subs = "Viewgen_ErrorPattern_Partition_Disj_Subs";

		// Token: 0x04000140 RID: 320
		internal const string Viewgen_ErrorPattern_Partition_Disj_Unk = "Viewgen_ErrorPattern_Partition_Disj_Unk";

		// Token: 0x04000141 RID: 321
		internal const string Viewgen_ErrorPattern_Partition_Eq_Disj = "Viewgen_ErrorPattern_Partition_Eq_Disj";

		// Token: 0x04000142 RID: 322
		internal const string Viewgen_ErrorPattern_Partition_Eq_Subs_Ref = "Viewgen_ErrorPattern_Partition_Eq_Subs_Ref";

		// Token: 0x04000143 RID: 323
		internal const string Viewgen_ErrorPattern_Partition_Eq_Subs = "Viewgen_ErrorPattern_Partition_Eq_Subs";

		// Token: 0x04000144 RID: 324
		internal const string Viewgen_ErrorPattern_Partition_Eq_Unk = "Viewgen_ErrorPattern_Partition_Eq_Unk";

		// Token: 0x04000145 RID: 325
		internal const string Viewgen_ErrorPattern_Partition_Eq_Unk_Association = "Viewgen_ErrorPattern_Partition_Eq_Unk_Association";

		// Token: 0x04000146 RID: 326
		internal const string Viewgen_ErrorPattern_Partition_Sub_Disj = "Viewgen_ErrorPattern_Partition_Sub_Disj";

		// Token: 0x04000147 RID: 327
		internal const string Viewgen_ErrorPattern_Partition_Sub_Eq = "Viewgen_ErrorPattern_Partition_Sub_Eq";

		// Token: 0x04000148 RID: 328
		internal const string Viewgen_ErrorPattern_Partition_Sub_Eq_Ref = "Viewgen_ErrorPattern_Partition_Sub_Eq_Ref";

		// Token: 0x04000149 RID: 329
		internal const string Viewgen_ErrorPattern_Partition_Sub_Unk = "Viewgen_ErrorPattern_Partition_Sub_Unk";

		// Token: 0x0400014A RID: 330
		internal const string Viewgen_NoJoinKeyOrFK = "Viewgen_NoJoinKeyOrFK";

		// Token: 0x0400014B RID: 331
		internal const string Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct = "Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct";

		// Token: 0x0400014C RID: 332
		internal const string InvalidCollectionForMapping = "InvalidCollectionForMapping";

		// Token: 0x0400014D RID: 333
		internal const string OnlyStoreConnectionsSupported = "OnlyStoreConnectionsSupported";

		// Token: 0x0400014E RID: 334
		internal const string StoreItemCollectionMustHaveOneArtifact = "StoreItemCollectionMustHaveOneArtifact";

		// Token: 0x0400014F RID: 335
		internal const string CheckArgumentContainsNullFailed = "CheckArgumentContainsNullFailed";

		// Token: 0x04000150 RID: 336
		internal const string InvalidRelationshipSetName = "InvalidRelationshipSetName";

		// Token: 0x04000151 RID: 337
		internal const string MemberInvalidIdentity = "MemberInvalidIdentity";

		// Token: 0x04000152 RID: 338
		internal const string InvalidEntitySetName = "InvalidEntitySetName";

		// Token: 0x04000153 RID: 339
		internal const string ItemInvalidIdentity = "ItemInvalidIdentity";

		// Token: 0x04000154 RID: 340
		internal const string ItemDuplicateIdentity = "ItemDuplicateIdentity";

		// Token: 0x04000155 RID: 341
		internal const string NotStringTypeForTypeUsage = "NotStringTypeForTypeUsage";

		// Token: 0x04000156 RID: 342
		internal const string NotBinaryTypeForTypeUsage = "NotBinaryTypeForTypeUsage";

		// Token: 0x04000157 RID: 343
		internal const string NotDateTimeTypeForTypeUsage = "NotDateTimeTypeForTypeUsage";

		// Token: 0x04000158 RID: 344
		internal const string NotDateTimeOffsetTypeForTypeUsage = "NotDateTimeOffsetTypeForTypeUsage";

		// Token: 0x04000159 RID: 345
		internal const string NotTimeTypeForTypeUsage = "NotTimeTypeForTypeUsage";

		// Token: 0x0400015A RID: 346
		internal const string NotDecimalTypeForTypeUsage = "NotDecimalTypeForTypeUsage";

		// Token: 0x0400015B RID: 347
		internal const string ArrayTooSmall = "ArrayTooSmall";

		// Token: 0x0400015C RID: 348
		internal const string MoreThanOneItemMatchesIdentity = "MoreThanOneItemMatchesIdentity";

		// Token: 0x0400015D RID: 349
		internal const string MissingDefaultValueForConstantFacet = "MissingDefaultValueForConstantFacet";

		// Token: 0x0400015E RID: 350
		internal const string MinAndMaxValueMustBeSameForConstantFacet = "MinAndMaxValueMustBeSameForConstantFacet";

		// Token: 0x0400015F RID: 351
		internal const string BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet = "BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet";

		// Token: 0x04000160 RID: 352
		internal const string MinAndMaxValueMustBeDifferentForNonConstantFacet = "MinAndMaxValueMustBeDifferentForNonConstantFacet";

		// Token: 0x04000161 RID: 353
		internal const string MinAndMaxMustBePositive = "MinAndMaxMustBePositive";

		// Token: 0x04000162 RID: 354
		internal const string MinMustBeLessThanMax = "MinMustBeLessThanMax";

		// Token: 0x04000163 RID: 355
		internal const string SameRoleNameOnRelationshipAttribute = "SameRoleNameOnRelationshipAttribute";

		// Token: 0x04000164 RID: 356
		internal const string RoleTypeInEdmRelationshipAttributeIsInvalidType = "RoleTypeInEdmRelationshipAttributeIsInvalidType";

		// Token: 0x04000165 RID: 357
		internal const string TargetRoleNameInNavigationPropertyNotValid = "TargetRoleNameInNavigationPropertyNotValid";

		// Token: 0x04000166 RID: 358
		internal const string RelationshipNameInNavigationPropertyNotValid = "RelationshipNameInNavigationPropertyNotValid";

		// Token: 0x04000167 RID: 359
		internal const string NestedClassNotSupported = "NestedClassNotSupported";

		// Token: 0x04000168 RID: 360
		internal const string NullParameterForEdmRelationshipAttribute = "NullParameterForEdmRelationshipAttribute";

		// Token: 0x04000169 RID: 361
		internal const string NullRelationshipNameforEdmRelationshipAttribute = "NullRelationshipNameforEdmRelationshipAttribute";

		// Token: 0x0400016A RID: 362
		internal const string NavigationPropertyRelationshipEndTypeMismatch = "NavigationPropertyRelationshipEndTypeMismatch";

		// Token: 0x0400016B RID: 363
		internal const string AllArtifactsMustTargetSameProvider_InvariantName = "AllArtifactsMustTargetSameProvider_InvariantName";

		// Token: 0x0400016C RID: 364
		internal const string AllArtifactsMustTargetSameProvider_ManifestToken = "AllArtifactsMustTargetSameProvider_ManifestToken";

		// Token: 0x0400016D RID: 365
		internal const string ProviderManifestTokenNotFound = "ProviderManifestTokenNotFound";

		// Token: 0x0400016E RID: 366
		internal const string FailedToRetrieveProviderManifest = "FailedToRetrieveProviderManifest";

		// Token: 0x0400016F RID: 367
		internal const string InvalidMaxLengthSize = "InvalidMaxLengthSize";

		// Token: 0x04000170 RID: 368
		internal const string ArgumentMustBeCSpaceType = "ArgumentMustBeCSpaceType";

		// Token: 0x04000171 RID: 369
		internal const string ArgumentMustBeOSpaceType = "ArgumentMustBeOSpaceType";

		// Token: 0x04000172 RID: 370
		internal const string FailedToFindOSpaceTypeMapping = "FailedToFindOSpaceTypeMapping";

		// Token: 0x04000173 RID: 371
		internal const string FailedToFindCSpaceTypeMapping = "FailedToFindCSpaceTypeMapping";

		// Token: 0x04000174 RID: 372
		internal const string FailedToFindClrTypeMapping = "FailedToFindClrTypeMapping";

		// Token: 0x04000175 RID: 373
		internal const string GenericTypeNotSupported = "GenericTypeNotSupported";

		// Token: 0x04000176 RID: 374
		internal const string Validator_EmptyIdentity = "Validator_EmptyIdentity";

		// Token: 0x04000177 RID: 375
		internal const string Validator_CollectionHasNoTypeUsage = "Validator_CollectionHasNoTypeUsage";

		// Token: 0x04000178 RID: 376
		internal const string Validator_NoKeyMembers = "Validator_NoKeyMembers";

		// Token: 0x04000179 RID: 377
		internal const string Validator_FacetTypeIsNull = "Validator_FacetTypeIsNull";

		// Token: 0x0400017A RID: 378
		internal const string Validator_MemberHasNullDeclaringType = "Validator_MemberHasNullDeclaringType";

		// Token: 0x0400017B RID: 379
		internal const string Validator_MemberHasNullTypeUsage = "Validator_MemberHasNullTypeUsage";

		// Token: 0x0400017C RID: 380
		internal const string Validator_ItemAttributeHasNullTypeUsage = "Validator_ItemAttributeHasNullTypeUsage";

		// Token: 0x0400017D RID: 381
		internal const string Validator_RefTypeHasNullEntityType = "Validator_RefTypeHasNullEntityType";

		// Token: 0x0400017E RID: 382
		internal const string Validator_TypeUsageHasNullEdmType = "Validator_TypeUsageHasNullEdmType";

		// Token: 0x0400017F RID: 383
		internal const string Validator_BaseTypeHasMemberOfSameName = "Validator_BaseTypeHasMemberOfSameName";

		// Token: 0x04000180 RID: 384
		internal const string Validator_CollectionTypesCannotHaveBaseType = "Validator_CollectionTypesCannotHaveBaseType";

		// Token: 0x04000181 RID: 385
		internal const string Validator_RefTypesCannotHaveBaseType = "Validator_RefTypesCannotHaveBaseType";

		// Token: 0x04000182 RID: 386
		internal const string Validator_TypeHasNoName = "Validator_TypeHasNoName";

		// Token: 0x04000183 RID: 387
		internal const string Validator_TypeHasNoNamespace = "Validator_TypeHasNoNamespace";

		// Token: 0x04000184 RID: 388
		internal const string Validator_FacetHasNoName = "Validator_FacetHasNoName";

		// Token: 0x04000185 RID: 389
		internal const string Validator_MemberHasNoName = "Validator_MemberHasNoName";

		// Token: 0x04000186 RID: 390
		internal const string Validator_MetadataPropertyHasNoName = "Validator_MetadataPropertyHasNoName";

		// Token: 0x04000187 RID: 391
		internal const string Validator_NullableEntityKeyProperty = "Validator_NullableEntityKeyProperty";

		// Token: 0x04000188 RID: 392
		internal const string Validator_OSpace_InvalidNavPropReturnType = "Validator_OSpace_InvalidNavPropReturnType";

		// Token: 0x04000189 RID: 393
		internal const string Validator_OSpace_ScalarPropertyNotPrimitive = "Validator_OSpace_ScalarPropertyNotPrimitive";

		// Token: 0x0400018A RID: 394
		internal const string Validator_OSpace_ComplexPropertyNotComplex = "Validator_OSpace_ComplexPropertyNotComplex";

		// Token: 0x0400018B RID: 395
		internal const string Validator_OSpace_Convention_MultipleTypesWithSameName = "Validator_OSpace_Convention_MultipleTypesWithSameName";

		// Token: 0x0400018C RID: 396
		internal const string Validator_OSpace_Convention_NonPrimitiveTypeProperty = "Validator_OSpace_Convention_NonPrimitiveTypeProperty";

		// Token: 0x0400018D RID: 397
		internal const string Validator_OSpace_Convention_MissingRequiredProperty = "Validator_OSpace_Convention_MissingRequiredProperty";

		// Token: 0x0400018E RID: 398
		internal const string Validator_OSpace_Convention_BaseTypeIncompatible = "Validator_OSpace_Convention_BaseTypeIncompatible";

		// Token: 0x0400018F RID: 399
		internal const string Validator_OSpace_Convention_MissingOSpaceType = "Validator_OSpace_Convention_MissingOSpaceType";

		// Token: 0x04000190 RID: 400
		internal const string Validator_OSpace_Convention_RelationshipNotLoaded = "Validator_OSpace_Convention_RelationshipNotLoaded";

		// Token: 0x04000191 RID: 401
		internal const string Validator_OSpace_Convention_AttributeAssemblyReferenced = "Validator_OSpace_Convention_AttributeAssemblyReferenced";

		// Token: 0x04000192 RID: 402
		internal const string Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter = "Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter";

		// Token: 0x04000193 RID: 403
		internal const string Validator_OSpace_Convention_AmbiguousClrType = "Validator_OSpace_Convention_AmbiguousClrType";

		// Token: 0x04000194 RID: 404
		internal const string Validator_OSpace_Convention_Struct = "Validator_OSpace_Convention_Struct";

		// Token: 0x04000195 RID: 405
		internal const string Validator_OSpace_Convention_BaseTypeNotLoaded = "Validator_OSpace_Convention_BaseTypeNotLoaded";

		// Token: 0x04000196 RID: 406
		internal const string ExtraInfo = "ExtraInfo";

		// Token: 0x04000197 RID: 407
		internal const string Metadata_General_Error = "Metadata_General_Error";

		// Token: 0x04000198 RID: 408
		internal const string InvalidNumberOfParametersForAggregateFunction = "InvalidNumberOfParametersForAggregateFunction";

		// Token: 0x04000199 RID: 409
		internal const string InvalidParameterTypeForAggregateFunction = "InvalidParameterTypeForAggregateFunction";

		// Token: 0x0400019A RID: 410
		internal const string ItemCollectionAlreadyRegistered = "ItemCollectionAlreadyRegistered";

		// Token: 0x0400019B RID: 411
		internal const string InvalidSchemaEncountered = "InvalidSchemaEncountered";

		// Token: 0x0400019C RID: 412
		internal const string SystemNamespaceEncountered = "SystemNamespaceEncountered";

		// Token: 0x0400019D RID: 413
		internal const string NoCollectionForSpace = "NoCollectionForSpace";

		// Token: 0x0400019E RID: 414
		internal const string OperationOnReadOnlyCollection = "OperationOnReadOnlyCollection";

		// Token: 0x0400019F RID: 415
		internal const string OperationOnReadOnlyItem = "OperationOnReadOnlyItem";

		// Token: 0x040001A0 RID: 416
		internal const string EntitySetInAnotherContainer = "EntitySetInAnotherContainer";

		// Token: 0x040001A1 RID: 417
		internal const string InvalidKeyMember = "InvalidKeyMember";

		// Token: 0x040001A2 RID: 418
		internal const string InvalidFileExtension = "InvalidFileExtension";

		// Token: 0x040001A3 RID: 419
		internal const string NewTypeConflictsWithExistingType = "NewTypeConflictsWithExistingType";

		// Token: 0x040001A4 RID: 420
		internal const string NotValidInputPath = "NotValidInputPath";

		// Token: 0x040001A5 RID: 421
		internal const string UnableToDetermineApplicationContext = "UnableToDetermineApplicationContext";

		// Token: 0x040001A6 RID: 422
		internal const string WildcardEnumeratorReturnedNull = "WildcardEnumeratorReturnedNull";

		// Token: 0x040001A7 RID: 423
		internal const string InvalidUseOfWebPath = "InvalidUseOfWebPath";

		// Token: 0x040001A8 RID: 424
		internal const string UnableToFindReflectedType = "UnableToFindReflectedType";

		// Token: 0x040001A9 RID: 425
		internal const string AssemblyMissingFromAssembliesToConsider = "AssemblyMissingFromAssembliesToConsider";

		// Token: 0x040001AA RID: 426
		internal const string InvalidCollectionSpecified = "InvalidCollectionSpecified";

		// Token: 0x040001AB RID: 427
		internal const string UnableToLoadResource = "UnableToLoadResource";

		// Token: 0x040001AC RID: 428
		internal const string EdmVersionNotSupportedByRuntime = "EdmVersionNotSupportedByRuntime";

		// Token: 0x040001AD RID: 429
		internal const string AtleastOneSSDLNeeded = "AtleastOneSSDLNeeded";

		// Token: 0x040001AE RID: 430
		internal const string InvalidMetadataPath = "InvalidMetadataPath";

		// Token: 0x040001AF RID: 431
		internal const string UnableToResolveAssembly = "UnableToResolveAssembly";

		// Token: 0x040001B0 RID: 432
		internal const string UnableToDetermineStoreVersion = "UnableToDetermineStoreVersion";

		// Token: 0x040001B1 RID: 433
		internal const string DuplicatedFunctionoverloads = "DuplicatedFunctionoverloads";

		// Token: 0x040001B2 RID: 434
		internal const string EntitySetNotInCSPace = "EntitySetNotInCSPace";

		// Token: 0x040001B3 RID: 435
		internal const string TypeNotInEntitySet = "TypeNotInEntitySet";

		// Token: 0x040001B4 RID: 436
		internal const string TypeNotInAssociationSet = "TypeNotInAssociationSet";

		// Token: 0x040001B5 RID: 437
		internal const string DifferentSchemaVersionInCollection = "DifferentSchemaVersionInCollection";

		// Token: 0x040001B6 RID: 438
		internal const string Mapping_General_Error_0 = "Mapping_General_Error_0";

		// Token: 0x040001B7 RID: 439
		internal const string Mapping_InvalidContent_General_0 = "Mapping_InvalidContent_General_0";

		// Token: 0x040001B8 RID: 440
		internal const string Mapping_InvalidContent_EntityContainer_1 = "Mapping_InvalidContent_EntityContainer_1";

		// Token: 0x040001B9 RID: 441
		internal const string Mapping_InvalidContent_StorageEntityContainer_1 = "Mapping_InvalidContent_StorageEntityContainer_1";

		// Token: 0x040001BA RID: 442
		internal const string Mapping_AlreadyMapped_StorageEntityContainer_1 = "Mapping_AlreadyMapped_StorageEntityContainer_1";

		// Token: 0x040001BB RID: 443
		internal const string Mapping_InvalidContent_Entity_Set_1 = "Mapping_InvalidContent_Entity_Set_1";

		// Token: 0x040001BC RID: 444
		internal const string Mapping_InvalidContent_Entity_Type_1 = "Mapping_InvalidContent_Entity_Type_1";

		// Token: 0x040001BD RID: 445
		internal const string Mapping_InvalidContent_AbstractEntity_FunctionMapping_1 = "Mapping_InvalidContent_AbstractEntity_FunctionMapping_1";

		// Token: 0x040001BE RID: 446
		internal const string Mapping_InvalidContent_ImplicitMappingForAbstractReturnType_FunctionMapping_1 = "Mapping_InvalidContent_ImplicitMappingForAbstractReturnType_FunctionMapping_1";

		// Token: 0x040001BF RID: 447
		internal const string Mapping_InvalidContent_AbstractEntity_Type_1 = "Mapping_InvalidContent_AbstractEntity_Type_1";

		// Token: 0x040001C0 RID: 448
		internal const string Mapping_InvalidContent_AbstractEntity_IsOfType_1 = "Mapping_InvalidContent_AbstractEntity_IsOfType_1";

		// Token: 0x040001C1 RID: 449
		internal const string Mapping_InvalidContent_Entity_Type_For_Entity_Set_3 = "Mapping_InvalidContent_Entity_Type_For_Entity_Set_3";

		// Token: 0x040001C2 RID: 450
		internal const string Mapping_Invalid_Association_Type_For_Association_Set_3 = "Mapping_Invalid_Association_Type_For_Association_Set_3";

		// Token: 0x040001C3 RID: 451
		internal const string Mapping_InvalidContent_Table_1 = "Mapping_InvalidContent_Table_1";

		// Token: 0x040001C4 RID: 452
		internal const string Mapping_InvalidContent_Complex_Type_1 = "Mapping_InvalidContent_Complex_Type_1";

		// Token: 0x040001C5 RID: 453
		internal const string Mapping_InvalidContent_Association_Set_1 = "Mapping_InvalidContent_Association_Set_1";

		// Token: 0x040001C6 RID: 454
		internal const string Mapping_InvalidContent_AssociationSet_Condition_1 = "Mapping_InvalidContent_AssociationSet_Condition_1";

		// Token: 0x040001C7 RID: 455
		internal const string Mapping_InvalidContent_ForeignKey_Association_Set_1 = "Mapping_InvalidContent_ForeignKey_Association_Set_1";

		// Token: 0x040001C8 RID: 456
		internal const string Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK_1 = "Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK_1";

		// Token: 0x040001C9 RID: 457
		internal const string Mapping_InvalidContent_Association_Type_1 = "Mapping_InvalidContent_Association_Type_1";

		// Token: 0x040001CA RID: 458
		internal const string Mapping_InvalidContent_EndProperty_1 = "Mapping_InvalidContent_EndProperty_1";

		// Token: 0x040001CB RID: 459
		internal const string Mapping_InvalidContent_Association_Type_Empty_0 = "Mapping_InvalidContent_Association_Type_Empty_0";

		// Token: 0x040001CC RID: 460
		internal const string Mapping_InvalidContent_Table_Expected_0 = "Mapping_InvalidContent_Table_Expected_0";

		// Token: 0x040001CD RID: 461
		internal const string Mapping_InvalidContent_Cdm_Member_1 = "Mapping_InvalidContent_Cdm_Member_1";

		// Token: 0x040001CE RID: 462
		internal const string Mapping_InvalidContent_Column_1 = "Mapping_InvalidContent_Column_1";

		// Token: 0x040001CF RID: 463
		internal const string Mapping_InvalidContent_End_1 = "Mapping_InvalidContent_End_1";

		// Token: 0x040001D0 RID: 464
		internal const string Mapping_InvalidContent_Set_Mapping_0 = "Mapping_InvalidContent_Set_Mapping_0";

		// Token: 0x040001D1 RID: 465
		internal const string Mapping_InvalidContent_Duplicate_Condition_Member_1 = "Mapping_InvalidContent_Duplicate_Condition_Member_1";

		// Token: 0x040001D2 RID: 466
		internal const string Mapping_InvalidContent_ConditionMapping_Both_Members_0 = "Mapping_InvalidContent_ConditionMapping_Both_Members_0";

		// Token: 0x040001D3 RID: 467
		internal const string Mapping_InvalidContent_ConditionMapping_Either_Members_0 = "Mapping_InvalidContent_ConditionMapping_Either_Members_0";

		// Token: 0x040001D4 RID: 468
		internal const string Mapping_InvalidContent_ConditionMapping_Both_Values_0 = "Mapping_InvalidContent_ConditionMapping_Both_Values_0";

		// Token: 0x040001D5 RID: 469
		internal const string Mapping_InvalidContent_ConditionMapping_Either_Values_0 = "Mapping_InvalidContent_ConditionMapping_Either_Values_0";

		// Token: 0x040001D6 RID: 470
		internal const string Mapping_InvalidContent_ConditionMapping_NonScalar_0 = "Mapping_InvalidContent_ConditionMapping_NonScalar_0";

		// Token: 0x040001D7 RID: 471
		internal const string Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind_2 = "Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind_2";

		// Token: 0x040001D8 RID: 472
		internal const string Mapping_InvalidContent_ConditionMapping_InvalidMember_1 = "Mapping_InvalidContent_ConditionMapping_InvalidMember_1";

		// Token: 0x040001D9 RID: 473
		internal const string Mapping_InvalidContent_ConditionMapping_Computed = "Mapping_InvalidContent_ConditionMapping_Computed";

		// Token: 0x040001DA RID: 474
		internal const string Mapping_InvalidContent_Emtpty_SetMap_1 = "Mapping_InvalidContent_Emtpty_SetMap_1";

		// Token: 0x040001DB RID: 475
		internal const string Mapping_InvalidContent_TypeMapping_QueryView = "Mapping_InvalidContent_TypeMapping_QueryView";

		// Token: 0x040001DC RID: 476
		internal const string Mapping_Default_OCMapping_Clr_Member_3 = "Mapping_Default_OCMapping_Clr_Member_3";

		// Token: 0x040001DD RID: 477
		internal const string Mapping_Default_OCMapping_Clr_Member_4 = "Mapping_Default_OCMapping_Clr_Member_4";

		// Token: 0x040001DE RID: 478
		internal const string Mapping_Default_OCMapping_Invalid_MemberType_6 = "Mapping_Default_OCMapping_Invalid_MemberType_6";

		// Token: 0x040001DF RID: 479
		internal const string Mapping_Default_OCMapping_MemberKind_Mismatch_6 = "Mapping_Default_OCMapping_MemberKind_Mismatch_6";

		// Token: 0x040001E0 RID: 480
		internal const string Mapping_Default_OCMapping_MultiplicityMismatch_6 = "Mapping_Default_OCMapping_MultiplicityMismatch_6";

		// Token: 0x040001E1 RID: 481
		internal const string Mapping_Default_OCMapping_Member_Count_Mismatch_2 = "Mapping_Default_OCMapping_Member_Count_Mismatch_2";

		// Token: 0x040001E2 RID: 482
		internal const string Mapping_Default_OCMapping_Member_Type_Mismatch = "Mapping_Default_OCMapping_Member_Type_Mismatch";

		// Token: 0x040001E3 RID: 483
		internal const string Mapping_NotFound_EntityContainer = "Mapping_NotFound_EntityContainer";

		// Token: 0x040001E4 RID: 484
		internal const string Mapping_Duplicate_CdmAssociationSet_StorageMap_1 = "Mapping_Duplicate_CdmAssociationSet_StorageMap_1";

		// Token: 0x040001E5 RID: 485
		internal const string Mapping_Invalid_CSRootElementMissing_0 = "Mapping_Invalid_CSRootElementMissing_0";

		// Token: 0x040001E6 RID: 486
		internal const string Mapping_ConditionValueTypeMismatch_0 = "Mapping_ConditionValueTypeMismatch_0";

		// Token: 0x040001E7 RID: 487
		internal const string Mapping_Storage_InvalidSpace_1 = "Mapping_Storage_InvalidSpace_1";

		// Token: 0x040001E8 RID: 488
		internal const string Mapping_Invalid_Member_Mapping_6 = "Mapping_Invalid_Member_Mapping_6";

		// Token: 0x040001E9 RID: 489
		internal const string Mapping_Invalid_CSide_ScalarProperty_1 = "Mapping_Invalid_CSide_ScalarProperty_1";

		// Token: 0x040001EA RID: 490
		internal const string Mapping_Duplicate_Type_1 = "Mapping_Duplicate_Type_1";

		// Token: 0x040001EB RID: 491
		internal const string Mapping_Duplicate_PropertyMap_CaseInsensitive = "Mapping_Duplicate_PropertyMap_CaseInsensitive";

		// Token: 0x040001EC RID: 492
		internal const string Mapping_Enum_EmptyValue_1 = "Mapping_Enum_EmptyValue_1";

		// Token: 0x040001ED RID: 493
		internal const string Mapping_Enum_InvalidValue_1 = "Mapping_Enum_InvalidValue_1";

		// Token: 0x040001EE RID: 494
		internal const string Mapping_InvalidMappingSchema_Parsing_1 = "Mapping_InvalidMappingSchema_Parsing_1";

		// Token: 0x040001EF RID: 495
		internal const string Mapping_InvalidMappingSchema_validation_1 = "Mapping_InvalidMappingSchema_validation_1";

		// Token: 0x040001F0 RID: 496
		internal const string Mapping_Object_InvalidType = "Mapping_Object_InvalidType";

		// Token: 0x040001F1 RID: 497
		internal const string Mapping_Provider_WrongConnectionType = "Mapping_Provider_WrongConnectionType";

		// Token: 0x040001F2 RID: 498
		internal const string Mapping_Provider_WrongManifestType = "Mapping_Provider_WrongManifestType";

		// Token: 0x040001F3 RID: 499
		internal const string Mapping_Views_For_Extent_Not_Generated = "Mapping_Views_For_Extent_Not_Generated";

		// Token: 0x040001F4 RID: 500
		internal const string Mapping_TableName_QueryView_1 = "Mapping_TableName_QueryView_1";

		// Token: 0x040001F5 RID: 501
		internal const string Mapping_Empty_QueryView_1 = "Mapping_Empty_QueryView_1";

		// Token: 0x040001F6 RID: 502
		internal const string Mapping_Empty_QueryView_OfType_2 = "Mapping_Empty_QueryView_OfType_2";

		// Token: 0x040001F7 RID: 503
		internal const string Mapping_Empty_QueryView_OfTypeOnly_2 = "Mapping_Empty_QueryView_OfTypeOnly_2";

		// Token: 0x040001F8 RID: 504
		internal const string Mapping_QueryView_PropertyMaps_1 = "Mapping_QueryView_PropertyMaps_1";

		// Token: 0x040001F9 RID: 505
		internal const string Mapping_Invalid_QueryView = "Mapping_Invalid_QueryView";

		// Token: 0x040001FA RID: 506
		internal const string Mapping_Invalid_QueryView_2 = "Mapping_Invalid_QueryView_2";

		// Token: 0x040001FB RID: 507
		internal const string Mapping_Invalid_QueryView_Type_1 = "Mapping_Invalid_QueryView_Type_1";

		// Token: 0x040001FC RID: 508
		internal const string Mapping_TypeName_For_First_QueryView = "Mapping_TypeName_For_First_QueryView";

		// Token: 0x040001FD RID: 509
		internal const string Mapping_AllQueryViewAtCompileTime = "Mapping_AllQueryViewAtCompileTime";

		// Token: 0x040001FE RID: 510
		internal const string Mapping_QueryViewMultipleTypeInTypeName = "Mapping_QueryViewMultipleTypeInTypeName";

		// Token: 0x040001FF RID: 511
		internal const string Mapping_QueryView_Duplicate_OfType = "Mapping_QueryView_Duplicate_OfType";

		// Token: 0x04000200 RID: 512
		internal const string Mapping_QueryView_Duplicate_OfTypeOnly = "Mapping_QueryView_Duplicate_OfTypeOnly";

		// Token: 0x04000201 RID: 513
		internal const string Mapping_QueryView_TypeName_Not_Defined = "Mapping_QueryView_TypeName_Not_Defined";

		// Token: 0x04000202 RID: 514
		internal const string Mapping_QueryView_For_Base_Type = "Mapping_QueryView_For_Base_Type";

		// Token: 0x04000203 RID: 515
		internal const string Mapping_UnsupportedExpressionKind_QueryView_2 = "Mapping_UnsupportedExpressionKind_QueryView_2";

		// Token: 0x04000204 RID: 516
		internal const string Mapping_UnsupportedScanTarget_QueryView_2 = "Mapping_UnsupportedScanTarget_QueryView_2";

		// Token: 0x04000205 RID: 517
		internal const string Mapping_UnsupportedPropertyKind_QueryView_3 = "Mapping_UnsupportedPropertyKind_QueryView_3";

		// Token: 0x04000206 RID: 518
		internal const string Mapping_UnsupportedInitialization_QueryView_2 = "Mapping_UnsupportedInitialization_QueryView_2";

		// Token: 0x04000207 RID: 519
		internal const string Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView_4 = "Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView_4";

		// Token: 0x04000208 RID: 520
		internal const string Mapping_Invalid_Query_Views_MissingSetClosure_1 = "Mapping_Invalid_Query_Views_MissingSetClosure_1";

		// Token: 0x04000209 RID: 521
		internal const string Generated_View_Type_Super_Class_1 = "Generated_View_Type_Super_Class_1";

		// Token: 0x0400020A RID: 522
		internal const string Generated_Views_Changed = "Generated_Views_Changed";

		// Token: 0x0400020B RID: 523
		internal const string Generated_Views_Invalid_Extent_1 = "Generated_Views_Invalid_Extent_1";

		// Token: 0x0400020C RID: 524
		internal const string Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace = "Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace";

		// Token: 0x0400020D RID: 525
		internal const string Mapping_AbstractTypeMappingToNonAbstractType = "Mapping_AbstractTypeMappingToNonAbstractType";

		// Token: 0x0400020E RID: 526
		internal const string StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping = "StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping";

		// Token: 0x0400020F RID: 527
		internal const string Mapping_InvalidContent_IsTypeOfNotTerminated = "Mapping_InvalidContent_IsTypeOfNotTerminated";

		// Token: 0x04000210 RID: 528
		internal const string Mapping_CannotMapCLRTypeMultipleTimes = "Mapping_CannotMapCLRTypeMultipleTimes";

		// Token: 0x04000211 RID: 529
		internal const string Mapping_Invalid_Function_Mapping_In_Table_Context_0 = "Mapping_Invalid_Function_Mapping_In_Table_Context_0";

		// Token: 0x04000212 RID: 530
		internal const string Mapping_Invalid_Function_Mapping_Multiple_Types_0 = "Mapping_Invalid_Function_Mapping_Multiple_Types_0";

		// Token: 0x04000213 RID: 531
		internal const string Mapping_Invalid_Function_Mapping_UnknownFunction_1 = "Mapping_Invalid_Function_Mapping_UnknownFunction_1";

		// Token: 0x04000214 RID: 532
		internal const string Mapping_Invalid_Function_Mapping_AmbiguousFunction_1 = "Mapping_Invalid_Function_Mapping_AmbiguousFunction_1";

		// Token: 0x04000215 RID: 533
		internal const string Mapping_Invalid_Function_Mapping_NotValidFunction_1 = "Mapping_Invalid_Function_Mapping_NotValidFunction_1";

		// Token: 0x04000216 RID: 534
		internal const string Mapping_Invalid_Function_Mapping_NotValidFunctionParameter_3 = "Mapping_Invalid_Function_Mapping_NotValidFunctionParameter_3";

		// Token: 0x04000217 RID: 535
		internal const string Mapping_Invalid_Function_Mapping_MissingParameter_2 = "Mapping_Invalid_Function_Mapping_MissingParameter_2";

		// Token: 0x04000218 RID: 536
		internal const string Mapping_Invalid_Function_Mapping_AssociationSetDoesNotExist_1 = "Mapping_Invalid_Function_Mapping_AssociationSetDoesNotExist_1";

		// Token: 0x04000219 RID: 537
		internal const string Mapping_Invalid_Function_Mapping_AssociationSetRoleDoesNotExist_1 = "Mapping_Invalid_Function_Mapping_AssociationSetRoleDoesNotExist_1";

		// Token: 0x0400021A RID: 538
		internal const string Mapping_Invalid_Function_Mapping_AssociationSetFromRoleIsNotEntitySet_1 = "Mapping_Invalid_Function_Mapping_AssociationSetFromRoleIsNotEntitySet_1";

		// Token: 0x0400021B RID: 539
		internal const string Mapping_Invalid_Function_Mapping_AssociationSetCardinality_1 = "Mapping_Invalid_Function_Mapping_AssociationSetCardinality_1";

		// Token: 0x0400021C RID: 540
		internal const string Mapping_Invalid_Function_Mapping_ComplexTypeNotFound_1 = "Mapping_Invalid_Function_Mapping_ComplexTypeNotFound_1";

		// Token: 0x0400021D RID: 541
		internal const string Mapping_Invalid_Function_Mapping_WrongComplexType_2 = "Mapping_Invalid_Function_Mapping_WrongComplexType_2";

		// Token: 0x0400021E RID: 542
		internal const string Mapping_Invalid_Function_Mapping_MissingVersion_0 = "Mapping_Invalid_Function_Mapping_MissingVersion_0";

		// Token: 0x0400021F RID: 543
		internal const string Mapping_Invalid_Function_Mapping_VersionMustBeOriginal_0 = "Mapping_Invalid_Function_Mapping_VersionMustBeOriginal_0";

		// Token: 0x04000220 RID: 544
		internal const string Mapping_Invalid_Function_Mapping_VersionMustBeCurrent_0 = "Mapping_Invalid_Function_Mapping_VersionMustBeCurrent_0";

		// Token: 0x04000221 RID: 545
		internal const string Mapping_Invalid_Function_Mapping_ParameterNotFound_2 = "Mapping_Invalid_Function_Mapping_ParameterNotFound_2";

		// Token: 0x04000222 RID: 546
		internal const string Mapping_Invalid_Function_Mapping_PropertyNotFound_2 = "Mapping_Invalid_Function_Mapping_PropertyNotFound_2";

		// Token: 0x04000223 RID: 547
		internal const string Mapping_Invalid_Function_Mapping_PropertyNotKey_2 = "Mapping_Invalid_Function_Mapping_PropertyNotKey_2";

		// Token: 0x04000224 RID: 548
		internal const string Mapping_Invalid_Function_Mapping_ParameterBoundTwice_1 = "Mapping_Invalid_Function_Mapping_ParameterBoundTwice_1";

		// Token: 0x04000225 RID: 549
		internal const string Mapping_Invalid_Function_Mapping_RedundantEntityTypeMapping_1 = "Mapping_Invalid_Function_Mapping_RedundantEntityTypeMapping_1";

		// Token: 0x04000226 RID: 550
		internal const string Mapping_Invalid_Function_Mapping_MissingSetClosure_1 = "Mapping_Invalid_Function_Mapping_MissingSetClosure_1";

		// Token: 0x04000227 RID: 551
		internal const string Mapping_Invalid_Function_Mapping_MissingEntityType_1 = "Mapping_Invalid_Function_Mapping_MissingEntityType_1";

		// Token: 0x04000228 RID: 552
		internal const string Mapping_Invalid_Function_Mapping_PropertyParameterTypeMismatch_6 = "Mapping_Invalid_Function_Mapping_PropertyParameterTypeMismatch_6";

		// Token: 0x04000229 RID: 553
		internal const string Mapping_Invalid_Function_Mapping_AssociationSetAmbiguous_1 = "Mapping_Invalid_Function_Mapping_AssociationSetAmbiguous_1";

		// Token: 0x0400022A RID: 554
		internal const string Mapping_Invalid_Function_Mapping_MultipleEndsOfAssociationMapped_3 = "Mapping_Invalid_Function_Mapping_MultipleEndsOfAssociationMapped_3";

		// Token: 0x0400022B RID: 555
		internal const string Mapping_Invalid_Function_Mapping_AmbiguousResultBinding_2 = "Mapping_Invalid_Function_Mapping_AmbiguousResultBinding_2";

		// Token: 0x0400022C RID: 556
		internal const string Mapping_Invalid_Function_Mapping_AssociationSetNotMappedForOperation_4 = "Mapping_Invalid_Function_Mapping_AssociationSetNotMappedForOperation_4";

		// Token: 0x0400022D RID: 557
		internal const string Mapping_Invalid_Function_Mapping_AssociationEndMappingInvalidForEntityType_3 = "Mapping_Invalid_Function_Mapping_AssociationEndMappingInvalidForEntityType_3";

		// Token: 0x0400022E RID: 558
		internal const string Mapping_Invalid_Function_Mapping_AssociationEndMappingForeignKeyAssociation_1 = "Mapping_Invalid_Function_Mapping_AssociationEndMappingForeignKeyAssociation_1";

		// Token: 0x0400022F RID: 559
		internal const string Mapping_StoreTypeMismatch_ScalarPropertyMapping_2 = "Mapping_StoreTypeMismatch_ScalarPropertyMapping_2";

		// Token: 0x04000230 RID: 560
		internal const string Mapping_DistinctFlagInReadWriteContainer = "Mapping_DistinctFlagInReadWriteContainer";

		// Token: 0x04000231 RID: 561
		internal const string Mapping_FunctionImport_StoreFunctionDoesNotExist = "Mapping_FunctionImport_StoreFunctionDoesNotExist";

		// Token: 0x04000232 RID: 562
		internal const string Mapping_FunctionImport_StoreFunctionAmbiguous = "Mapping_FunctionImport_StoreFunctionAmbiguous";

		// Token: 0x04000233 RID: 563
		internal const string Mapping_FunctionImport_FunctionImportDoesNotExist = "Mapping_FunctionImport_FunctionImportDoesNotExist";

		// Token: 0x04000234 RID: 564
		internal const string Mapping_FunctionImport_FunctionImportMappedMultipleTimes = "Mapping_FunctionImport_FunctionImportMappedMultipleTimes";

		// Token: 0x04000235 RID: 565
		internal const string Mapping_FunctionImport_TargetFunctionMustBeComposable = "Mapping_FunctionImport_TargetFunctionMustBeComposable";

		// Token: 0x04000236 RID: 566
		internal const string Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter = "Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter";

		// Token: 0x04000237 RID: 567
		internal const string Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter = "Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter";

		// Token: 0x04000238 RID: 568
		internal const string Mapping_FunctionImport_IncompatibleParameterMode = "Mapping_FunctionImport_IncompatibleParameterMode";

		// Token: 0x04000239 RID: 569
		internal const string Mapping_FunctionImport_IncompatibleParameterType = "Mapping_FunctionImport_IncompatibleParameterType";

		// Token: 0x0400023A RID: 570
		internal const string Mapping_ProviderReturnsNullType = "Mapping_ProviderReturnsNullType";

		// Token: 0x0400023B RID: 571
		internal const string Mapping_FunctionImport_RowsAffectedParameterDoesNotExist_2 = "Mapping_FunctionImport_RowsAffectedParameterDoesNotExist_2";

		// Token: 0x0400023C RID: 572
		internal const string Mapping_FunctionImport_RowsAffectedParameterHasWrongType_2 = "Mapping_FunctionImport_RowsAffectedParameterHasWrongType_2";

		// Token: 0x0400023D RID: 573
		internal const string Mapping_FunctionImport_RowsAffectedParameterHasWrongMode_4 = "Mapping_FunctionImport_RowsAffectedParameterHasWrongMode_4";

		// Token: 0x0400023E RID: 574
		internal const string Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet = "Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet";

		// Token: 0x0400023F RID: 575
		internal const string Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet = "Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet";

		// Token: 0x04000240 RID: 576
		internal const string Mapping_FunctionImport_MultipleConditionsOnSingleColumn = "Mapping_FunctionImport_MultipleConditionsOnSingleColumn";

		// Token: 0x04000241 RID: 577
		internal const string Mapping_FunctionImport_UnreachableType = "Mapping_FunctionImport_UnreachableType";

		// Token: 0x04000242 RID: 578
		internal const string Mapping_FunctionImport_UnreachableIsTypeOf = "Mapping_FunctionImport_UnreachableIsTypeOf";

		// Token: 0x04000243 RID: 579
		internal const string Mapping_FunctionImport_ConditionValueTypeMismatch = "Mapping_FunctionImport_ConditionValueTypeMismatch";

		// Token: 0x04000244 RID: 580
		internal const string Mapping_FunctionImport_UnsupportedType = "Mapping_FunctionImport_UnsupportedType";

		// Token: 0x04000245 RID: 581
		internal const string Mapping_FunctionImport_InvalidComplexTypeName = "Mapping_FunctionImport_InvalidComplexTypeName";

		// Token: 0x04000246 RID: 582
		internal const string Mapping_FunctionImport_DuplicateMemberName = "Mapping_FunctionImport_DuplicateMemberName";

		// Token: 0x04000247 RID: 583
		internal const string Mapping_FunctionImport_InvalidMemberName = "Mapping_FunctionImport_InvalidMemberName";

		// Token: 0x04000248 RID: 584
		internal const string Mapping_DifferentEdmStoreVersion = "Mapping_DifferentEdmStoreVersion";

		// Token: 0x04000249 RID: 585
		internal const string Mapping_DifferentMappingEdmStoreVersion = "Mapping_DifferentMappingEdmStoreVersion";

		// Token: 0x0400024A RID: 586
		internal const string SqlProvider_DdlGeneration_MissingInitialCatalog = "SqlProvider_DdlGeneration_MissingInitialCatalog";

		// Token: 0x0400024B RID: 587
		internal const string SqlProvider_CredentialsMissingForMasterConnection = "SqlProvider_CredentialsMissingForMasterConnection";

		// Token: 0x0400024C RID: 588
		internal const string SqlProvider_IncompleteCreateDatabase = "SqlProvider_IncompleteCreateDatabase";

		// Token: 0x0400024D RID: 589
		internal const string Entity_EntityCantHaveMultipleChangeTrackers = "Entity_EntityCantHaveMultipleChangeTrackers";

		// Token: 0x0400024E RID: 590
		internal const string ComplexObject_NullableComplexTypesNotSupported = "ComplexObject_NullableComplexTypesNotSupported";

		// Token: 0x0400024F RID: 591
		internal const string ComplexObject_ComplexObjectAlreadyAttachedToParent = "ComplexObject_ComplexObjectAlreadyAttachedToParent";

		// Token: 0x04000250 RID: 592
		internal const string ComplexObject_ComplexChangeRequestedOnScalarProperty = "ComplexObject_ComplexChangeRequestedOnScalarProperty";

		// Token: 0x04000251 RID: 593
		internal const string ObjectStateEntry_SetModifiedOnInvalidProperty = "ObjectStateEntry_SetModifiedOnInvalidProperty";

		// Token: 0x04000252 RID: 594
		internal const string ObjectStateEntry_OriginalValuesDoesNotExist = "ObjectStateEntry_OriginalValuesDoesNotExist";

		// Token: 0x04000253 RID: 595
		internal const string ObjectStateEntry_CurrentValuesDoesNotExist = "ObjectStateEntry_CurrentValuesDoesNotExist";

		// Token: 0x04000254 RID: 596
		internal const string ObjectStateEntry_InvalidState = "ObjectStateEntry_InvalidState";

		// Token: 0x04000255 RID: 597
		internal const string ObjectStateEntry_CannotModifyKeyProperty = "ObjectStateEntry_CannotModifyKeyProperty";

		// Token: 0x04000256 RID: 598
		internal const string ObjectStateEntry_CantModifyRelationValues = "ObjectStateEntry_CantModifyRelationValues";

		// Token: 0x04000257 RID: 599
		internal const string ObjectStateEntry_CantModifyRelationState = "ObjectStateEntry_CantModifyRelationState";

		// Token: 0x04000258 RID: 600
		internal const string ObjectStateEntry_CantModifyDetachedDeletedEntries = "ObjectStateEntry_CantModifyDetachedDeletedEntries";

		// Token: 0x04000259 RID: 601
		internal const string ObjectStateEntry_SetModifiedStates = "ObjectStateEntry_SetModifiedStates";

		// Token: 0x0400025A RID: 602
		internal const string ObjectStateEntry_CantSetEntityKey = "ObjectStateEntry_CantSetEntityKey";

		// Token: 0x0400025B RID: 603
		internal const string ObjectStateEntry_CannotAccessKeyEntryValues = "ObjectStateEntry_CannotAccessKeyEntryValues";

		// Token: 0x0400025C RID: 604
		internal const string ObjectStateEntry_CannotModifyKeyEntryState = "ObjectStateEntry_CannotModifyKeyEntryState";

		// Token: 0x0400025D RID: 605
		internal const string ObjectStateEntry_CannotDeleteOnKeyEntry = "ObjectStateEntry_CannotDeleteOnKeyEntry";

		// Token: 0x0400025E RID: 606
		internal const string ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging = "ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging";

		// Token: 0x0400025F RID: 607
		internal const string ObjectStateEntry_ChangeOnUnmappedProperty = "ObjectStateEntry_ChangeOnUnmappedProperty";

		// Token: 0x04000260 RID: 608
		internal const string ObjectStateEntry_ChangeOnUnmappedComplexProperty = "ObjectStateEntry_ChangeOnUnmappedComplexProperty";

		// Token: 0x04000261 RID: 609
		internal const string ObjectStateEntry_ChangedInDifferentStateFromChanging = "ObjectStateEntry_ChangedInDifferentStateFromChanging";

		// Token: 0x04000262 RID: 610
		internal const string ObjectStateEntry_UnableToEnumerateCollection = "ObjectStateEntry_UnableToEnumerateCollection";

		// Token: 0x04000263 RID: 611
		internal const string ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers = "ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers";

		// Token: 0x04000264 RID: 612
		internal const string ObjectStateEntry_InvalidTypeForComplexTypeProperty = "ObjectStateEntry_InvalidTypeForComplexTypeProperty";

		// Token: 0x04000265 RID: 613
		internal const string ObjectStateEntry_ComplexObjectUsedMultipleTimes = "ObjectStateEntry_ComplexObjectUsedMultipleTimes";

		// Token: 0x04000266 RID: 614
		internal const string ObjectStateEntry_SetOriginalComplexProperties = "ObjectStateEntry_SetOriginalComplexProperties";

		// Token: 0x04000267 RID: 615
		internal const string ObjectStateEntry_NullOriginalValueForNonNullableProperty = "ObjectStateEntry_NullOriginalValueForNonNullableProperty";

		// Token: 0x04000268 RID: 616
		internal const string ObjectStateEntry_SetOriginalPrimaryKey = "ObjectStateEntry_SetOriginalPrimaryKey";

		// Token: 0x04000269 RID: 617
		internal const string ObjectStateManager_NoEntryExistForEntityKey = "ObjectStateManager_NoEntryExistForEntityKey";

		// Token: 0x0400026A RID: 618
		internal const string ObjectStateManager_NoEntryExistsForObject = "ObjectStateManager_NoEntryExistsForObject";

		// Token: 0x0400026B RID: 619
		internal const string ObjectStateManager_EntityNotTracked = "ObjectStateManager_EntityNotTracked";

		// Token: 0x0400026C RID: 620
		internal const string ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager = "ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager";

		// Token: 0x0400026D RID: 621
		internal const string ObjectStateManager_ObjectStateManagerContainsThisEntityKey = "ObjectStateManager_ObjectStateManagerContainsThisEntityKey";

		// Token: 0x0400026E RID: 622
		internal const string ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity = "ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity";

		// Token: 0x0400026F RID: 623
		internal const string ObjectStateManager_CannotFixUpKeyToExistingValues = "ObjectStateManager_CannotFixUpKeyToExistingValues";

		// Token: 0x04000270 RID: 624
		internal const string ObjectStateManager_KeyPropertyDoesntMatchValueInKey = "ObjectStateManager_KeyPropertyDoesntMatchValueInKey";

		// Token: 0x04000271 RID: 625
		internal const string ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach = "ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach";

		// Token: 0x04000272 RID: 626
		internal const string ObjectStateManager_InvalidKey = "ObjectStateManager_InvalidKey";

		// Token: 0x04000273 RID: 627
		internal const string ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType = "ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType";

		// Token: 0x04000274 RID: 628
		internal const string ObjectStateManager_GetEntityKeyRequiresObjectToHaveAKey = "ObjectStateManager_GetEntityKeyRequiresObjectToHaveAKey";

		// Token: 0x04000275 RID: 629
		internal const string ObjectStateManager_AcceptChangesEntityKeyIsNotValid = "ObjectStateManager_AcceptChangesEntityKeyIsNotValid";

		// Token: 0x04000276 RID: 630
		internal const string ObjectStateManager_EntityConflictsWithKeyEntry = "ObjectStateManager_EntityConflictsWithKeyEntry";

		// Token: 0x04000277 RID: 631
		internal const string ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity = "ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity";

		// Token: 0x04000278 RID: 632
		internal const string ObjectStateManager_CannotChangeRelationshipStateEntityDeleted = "ObjectStateManager_CannotChangeRelationshipStateEntityDeleted";

		// Token: 0x04000279 RID: 633
		internal const string ObjectStateManager_CannotChangeRelationshipStateEntityAdded = "ObjectStateManager_CannotChangeRelationshipStateEntityAdded";

		// Token: 0x0400027A RID: 634
		internal const string ObjectStateManager_CannotChangeRelationshipStateKeyEntry = "ObjectStateManager_CannotChangeRelationshipStateKeyEntry";

		// Token: 0x0400027B RID: 635
		internal const string ObjectStateManager_ConflictingChangesOfRelationshipDetected = "ObjectStateManager_ConflictingChangesOfRelationshipDetected";

		// Token: 0x0400027C RID: 636
		internal const string ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations = "ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations";

		// Token: 0x0400027D RID: 637
		internal const string ObjectContext_ClientEntityRemovedFromStore = "ObjectContext_ClientEntityRemovedFromStore";

		// Token: 0x0400027E RID: 638
		internal const string ObjectContext_StoreEntityNotPresentInClient = "ObjectContext_StoreEntityNotPresentInClient";

		// Token: 0x0400027F RID: 639
		internal const string ObjectContext_InvalidConnectionString = "ObjectContext_InvalidConnectionString";

		// Token: 0x04000280 RID: 640
		internal const string ObjectContext_InvalidConnection = "ObjectContext_InvalidConnection";

		// Token: 0x04000281 RID: 641
		internal const string ObjectContext_InvalidDataAdapter = "ObjectContext_InvalidDataAdapter";

		// Token: 0x04000282 RID: 642
		internal const string ObjectContext_InvalidDefaultContainerName = "ObjectContext_InvalidDefaultContainerName";

		// Token: 0x04000283 RID: 643
		internal const string ObjectContext_NthElementInAddedState = "ObjectContext_NthElementInAddedState";

		// Token: 0x04000284 RID: 644
		internal const string ObjectContext_NthElementIsDuplicate = "ObjectContext_NthElementIsDuplicate";

		// Token: 0x04000285 RID: 645
		internal const string ObjectContext_NthElementIsNull = "ObjectContext_NthElementIsNull";

		// Token: 0x04000286 RID: 646
		internal const string ObjectContext_NthElementNotInObjectStateManager = "ObjectContext_NthElementNotInObjectStateManager";

		// Token: 0x04000287 RID: 647
		internal const string ObjectContext_ObjectNotFound = "ObjectContext_ObjectNotFound";

		// Token: 0x04000288 RID: 648
		internal const string ObjectContext_CannotDeleteEntityNotInObjectStateManager = "ObjectContext_CannotDeleteEntityNotInObjectStateManager";

		// Token: 0x04000289 RID: 649
		internal const string ObjectContext_CannotDetachEntityNotInObjectStateManager = "ObjectContext_CannotDetachEntityNotInObjectStateManager";

		// Token: 0x0400028A RID: 650
		internal const string ObjectContext_EntitySetNotFoundForName = "ObjectContext_EntitySetNotFoundForName";

		// Token: 0x0400028B RID: 651
		internal const string ObjectContext_EntityContainerNotFoundForName = "ObjectContext_EntityContainerNotFoundForName";

		// Token: 0x0400028C RID: 652
		internal const string ObjectContext_InvalidCommandTimeout = "ObjectContext_InvalidCommandTimeout";

		// Token: 0x0400028D RID: 653
		internal const string ObjectContext_NoMappingForEntityType = "ObjectContext_NoMappingForEntityType";

		// Token: 0x0400028E RID: 654
		internal const string ObjectContext_EntityAlreadyExistsInObjectStateManager = "ObjectContext_EntityAlreadyExistsInObjectStateManager";

		// Token: 0x0400028F RID: 655
		internal const string ObjectContext_InvalidEntitySetInKey = "ObjectContext_InvalidEntitySetInKey";

		// Token: 0x04000290 RID: 656
		internal const string ObjectContext_CannotAttachEntityWithoutKey = "ObjectContext_CannotAttachEntityWithoutKey";

		// Token: 0x04000291 RID: 657
		internal const string ObjectContext_CannotAttachEntityWithTemporaryKey = "ObjectContext_CannotAttachEntityWithTemporaryKey";

		// Token: 0x04000292 RID: 658
		internal const string ObjectContext_EntitySetNameOrEntityKeyRequired = "ObjectContext_EntitySetNameOrEntityKeyRequired";

		// Token: 0x04000293 RID: 659
		internal const string ObjectContext_ExecuteFunctionTypeMismatch = "ObjectContext_ExecuteFunctionTypeMismatch";

		// Token: 0x04000294 RID: 660
		internal const string ObjectContext_ExecuteFunctionCalledWithScalarFunction = "ObjectContext_ExecuteFunctionCalledWithScalarFunction";

		// Token: 0x04000295 RID: 661
		internal const string ObjectContext_ExecuteFunctionCalledWithNonQueryFunction = "ObjectContext_ExecuteFunctionCalledWithNonQueryFunction";

		// Token: 0x04000296 RID: 662
		internal const string ObjectContext_ExecuteFunctionCalledWithNullParameter = "ObjectContext_ExecuteFunctionCalledWithNullParameter";

		// Token: 0x04000297 RID: 663
		internal const string ObjectContext_ContainerQualifiedEntitySetNameRequired = "ObjectContext_ContainerQualifiedEntitySetNameRequired";

		// Token: 0x04000298 RID: 664
		internal const string ObjectContext_CannotSetDefaultContainerName = "ObjectContext_CannotSetDefaultContainerName";

		// Token: 0x04000299 RID: 665
		internal const string ObjectContext_QualfiedEntitySetName = "ObjectContext_QualfiedEntitySetName";

		// Token: 0x0400029A RID: 666
		internal const string ObjectContext_EntitiesHaveDifferentType = "ObjectContext_EntitiesHaveDifferentType";

		// Token: 0x0400029B RID: 667
		internal const string ObjectContext_EntityMustBeUnchangedOrModified = "ObjectContext_EntityMustBeUnchangedOrModified";

		// Token: 0x0400029C RID: 668
		internal const string ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted = "ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted";

		// Token: 0x0400029D RID: 669
		internal const string ObjectContext_AcceptAllChangesFailure = "ObjectContext_AcceptAllChangesFailure";

		// Token: 0x0400029E RID: 670
		internal const string ObjectContext_CommitWithConceptualNull = "ObjectContext_CommitWithConceptualNull";

		// Token: 0x0400029F RID: 671
		internal const string ObjectContext_InvalidEntitySetOnEntity = "ObjectContext_InvalidEntitySetOnEntity";

		// Token: 0x040002A0 RID: 672
		internal const string ObjectContext_InvalidObjectSetTypeForEntitySet = "ObjectContext_InvalidObjectSetTypeForEntitySet";

		// Token: 0x040002A1 RID: 673
		internal const string ObjectContext_RequiredMetadataNotAvailble = "ObjectContext_RequiredMetadataNotAvailble";

		// Token: 0x040002A2 RID: 674
		internal const string ObjectContext_MetadataHasChanged = "ObjectContext_MetadataHasChanged";

		// Token: 0x040002A3 RID: 675
		internal const string ObjectContext_InvalidEntitySetInKeyFromName = "ObjectContext_InvalidEntitySetInKeyFromName";

		// Token: 0x040002A4 RID: 676
		internal const string ObjectContext_ObjectDisposed = "ObjectContext_ObjectDisposed";

		// Token: 0x040002A5 RID: 677
		internal const string ObjectContext_CannotExplicitlyLoadDetachedRelationships = "ObjectContext_CannotExplicitlyLoadDetachedRelationships";

		// Token: 0x040002A6 RID: 678
		internal const string ObjectContext_CannotLoadReferencesUsingDifferentContext = "ObjectContext_CannotLoadReferencesUsingDifferentContext";

		// Token: 0x040002A7 RID: 679
		internal const string ObjectContext_SelectorExpressionMustBeMemberAccess = "ObjectContext_SelectorExpressionMustBeMemberAccess";

		// Token: 0x040002A8 RID: 680
		internal const string ObjectContext_MultipleEntitySetsFoundInSingleContainer = "ObjectContext_MultipleEntitySetsFoundInSingleContainer";

		// Token: 0x040002A9 RID: 681
		internal const string ObjectContext_MultipleEntitySetsFoundInAllContainers = "ObjectContext_MultipleEntitySetsFoundInAllContainers";

		// Token: 0x040002AA RID: 682
		internal const string ObjectContext_NoEntitySetFoundForType = "ObjectContext_NoEntitySetFoundForType";

		// Token: 0x040002AB RID: 683
		internal const string ObjectContext_EntityNotInObjectSet_Delete = "ObjectContext_EntityNotInObjectSet_Delete";

		// Token: 0x040002AC RID: 684
		internal const string ObjectContext_EntityNotInObjectSet_Detach = "ObjectContext_EntityNotInObjectSet_Detach";

		// Token: 0x040002AD RID: 685
		internal const string ObjectContext_InvalidEntityState = "ObjectContext_InvalidEntityState";

		// Token: 0x040002AE RID: 686
		internal const string ObjectContext_InvalidRelationshipState = "ObjectContext_InvalidRelationshipState";

		// Token: 0x040002AF RID: 687
		internal const string ObjectContext_EntityNotTrackedOrHasTempKey = "ObjectContext_EntityNotTrackedOrHasTempKey";

		// Token: 0x040002B0 RID: 688
		internal const string ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues = "ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues";

		// Token: 0x040002B1 RID: 689
		internal const string ObjectContext_InvalidEntitySetForStoreQuery = "ObjectContext_InvalidEntitySetForStoreQuery";

		// Token: 0x040002B2 RID: 690
		internal const string ObjectContext_InvalidTypeForStoreQuery = "ObjectContext_InvalidTypeForStoreQuery";

		// Token: 0x040002B3 RID: 691
		internal const string ObjectContext_TwoPropertiesMappedToSameColumn = "ObjectContext_TwoPropertiesMappedToSameColumn";

		// Token: 0x040002B4 RID: 692
		internal const string RelatedEnd_InvalidOwnerStateForAttach = "RelatedEnd_InvalidOwnerStateForAttach";

		// Token: 0x040002B5 RID: 693
		internal const string RelatedEnd_InvalidNthElementNullForAttach = "RelatedEnd_InvalidNthElementNullForAttach";

		// Token: 0x040002B6 RID: 694
		internal const string RelatedEnd_InvalidNthElementContextForAttach = "RelatedEnd_InvalidNthElementContextForAttach";

		// Token: 0x040002B7 RID: 695
		internal const string RelatedEnd_InvalidNthElementStateForAttach = "RelatedEnd_InvalidNthElementStateForAttach";

		// Token: 0x040002B8 RID: 696
		internal const string RelatedEnd_InvalidEntityContextForAttach = "RelatedEnd_InvalidEntityContextForAttach";

		// Token: 0x040002B9 RID: 697
		internal const string RelatedEnd_InvalidEntityStateForAttach = "RelatedEnd_InvalidEntityStateForAttach";

		// Token: 0x040002BA RID: 698
		internal const string RelatedEnd_UnableToAddEntity = "RelatedEnd_UnableToAddEntity";

		// Token: 0x040002BB RID: 699
		internal const string RelatedEnd_UnableToRemoveEntity = "RelatedEnd_UnableToRemoveEntity";

		// Token: 0x040002BC RID: 700
		internal const string RelatedEnd_UnableToAddRelationshipWithDeletedEntity = "RelatedEnd_UnableToAddRelationshipWithDeletedEntity";

		// Token: 0x040002BD RID: 701
		internal const string RelatedEnd_ConflictingChangeOfRelationshipDetected = "RelatedEnd_ConflictingChangeOfRelationshipDetected";

		// Token: 0x040002BE RID: 702
		internal const string RelatedEnd_InvalidRelationshipFixupDetected = "RelatedEnd_InvalidRelationshipFixupDetected";

		// Token: 0x040002BF RID: 703
		internal const string RelatedEnd_CannotSerialize = "RelatedEnd_CannotSerialize";

		// Token: 0x040002C0 RID: 704
		internal const string RelatedEnd_CannotAddToFixedSizeArray = "RelatedEnd_CannotAddToFixedSizeArray";

		// Token: 0x040002C1 RID: 705
		internal const string RelatedEnd_CannotRemoveFromFixedSizeArray = "RelatedEnd_CannotRemoveFromFixedSizeArray";

		// Token: 0x040002C2 RID: 706
		internal const string Materializer_PropertyIsNotNullable = "Materializer_PropertyIsNotNullable";

		// Token: 0x040002C3 RID: 707
		internal const string Materializer_PropertyIsNotNullableWithName = "Materializer_PropertyIsNotNullableWithName";

		// Token: 0x040002C4 RID: 708
		internal const string Materializer_SetInvalidValue = "Materializer_SetInvalidValue";

		// Token: 0x040002C5 RID: 709
		internal const string Materializer_InvalidCastReference = "Materializer_InvalidCastReference";

		// Token: 0x040002C6 RID: 710
		internal const string Materializer_InvalidCastNullable = "Materializer_InvalidCastNullable";

		// Token: 0x040002C7 RID: 711
		internal const string Materializer_NullReferenceCast = "Materializer_NullReferenceCast";

		// Token: 0x040002C8 RID: 712
		internal const string Materializer_RecyclingEntity = "Materializer_RecyclingEntity";

		// Token: 0x040002C9 RID: 713
		internal const string Materializer_AddedEntityAlreadyExists = "Materializer_AddedEntityAlreadyExists";

		// Token: 0x040002CA RID: 714
		internal const string Materializer_CannotReEnumerateQueryResults = "Materializer_CannotReEnumerateQueryResults";

		// Token: 0x040002CB RID: 715
		internal const string Materializer_UnsupportedType = "Materializer_UnsupportedType";

		// Token: 0x040002CC RID: 716
		internal const string Collections_NoRelationshipSetMatched = "Collections_NoRelationshipSetMatched";

		// Token: 0x040002CD RID: 717
		internal const string Collections_ExpectedCollectionGotReference = "Collections_ExpectedCollectionGotReference";

		// Token: 0x040002CE RID: 718
		internal const string Collections_InvalidEntityStateSource = "Collections_InvalidEntityStateSource";

		// Token: 0x040002CF RID: 719
		internal const string Collections_InvalidEntityStateLoad = "Collections_InvalidEntityStateLoad";

		// Token: 0x040002D0 RID: 720
		internal const string Collections_CannotFillTryDifferentMergeOption = "Collections_CannotFillTryDifferentMergeOption";

		// Token: 0x040002D1 RID: 721
		internal const string Collections_UnableToMergeCollections = "Collections_UnableToMergeCollections";

		// Token: 0x040002D2 RID: 722
		internal const string EntityReference_ExpectedReferenceGotCollection = "EntityReference_ExpectedReferenceGotCollection";

		// Token: 0x040002D3 RID: 723
		internal const string EntityReference_CannotAddMoreThanOneEntityToEntityReference = "EntityReference_CannotAddMoreThanOneEntityToEntityReference";

		// Token: 0x040002D4 RID: 724
		internal const string EntityReference_LessThanExpectedRelatedEntitiesFound = "EntityReference_LessThanExpectedRelatedEntitiesFound";

		// Token: 0x040002D5 RID: 725
		internal const string EntityReference_MoreThanExpectedRelatedEntitiesFound = "EntityReference_MoreThanExpectedRelatedEntitiesFound";

		// Token: 0x040002D6 RID: 726
		internal const string EntityReference_CannotChangeReferentialConstraintProperty = "EntityReference_CannotChangeReferentialConstraintProperty";

		// Token: 0x040002D7 RID: 727
		internal const string EntityReference_CannotSetSpecialKeys = "EntityReference_CannotSetSpecialKeys";

		// Token: 0x040002D8 RID: 728
		internal const string EntityReference_EntityKeyValueMismatch = "EntityReference_EntityKeyValueMismatch";

		// Token: 0x040002D9 RID: 729
		internal const string RelatedEnd_RelatedEndNotFound = "RelatedEnd_RelatedEndNotFound";

		// Token: 0x040002DA RID: 730
		internal const string RelatedEnd_RelatedEndNotAttachedToContext = "RelatedEnd_RelatedEndNotAttachedToContext";

		// Token: 0x040002DB RID: 731
		internal const string RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd = "RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd";

		// Token: 0x040002DC RID: 732
		internal const string RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd = "RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd";

		// Token: 0x040002DD RID: 733
		internal const string RelatedEnd_InvalidContainedType_Collection = "RelatedEnd_InvalidContainedType_Collection";

		// Token: 0x040002DE RID: 734
		internal const string RelatedEnd_InvalidContainedType_Reference = "RelatedEnd_InvalidContainedType_Reference";

		// Token: 0x040002DF RID: 735
		internal const string RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities = "RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities";

		// Token: 0x040002E0 RID: 736
		internal const string RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts = "RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts";

		// Token: 0x040002E1 RID: 737
		internal const string RelatedEnd_MismatchedMergeOptionOnLoad = "RelatedEnd_MismatchedMergeOptionOnLoad";

		// Token: 0x040002E2 RID: 738
		internal const string RelatedEnd_EntitySetIsNotValidForRelationship = "RelatedEnd_EntitySetIsNotValidForRelationship";

		// Token: 0x040002E3 RID: 739
		internal const string RelatedEnd_OwnerIsNull = "RelatedEnd_OwnerIsNull";

		// Token: 0x040002E4 RID: 740
		internal const string RelationshipManager_UnableToRetrieveReferentialConstraintProperties = "RelationshipManager_UnableToRetrieveReferentialConstraintProperties";

		// Token: 0x040002E5 RID: 741
		internal const string RelationshipManager_InconsistentReferentialConstraintProperties = "RelationshipManager_InconsistentReferentialConstraintProperties";

		// Token: 0x040002E6 RID: 742
		internal const string RelationshipManager_CircularRelationshipsWithReferentialConstraints = "RelationshipManager_CircularRelationshipsWithReferentialConstraints";

		// Token: 0x040002E7 RID: 743
		internal const string RelationshipManager_UnableToFindRelationshipTypeInMetadata = "RelationshipManager_UnableToFindRelationshipTypeInMetadata";

		// Token: 0x040002E8 RID: 744
		internal const string RelationshipManager_InvalidTargetRole = "RelationshipManager_InvalidTargetRole";

		// Token: 0x040002E9 RID: 745
		internal const string RelationshipManager_UnexpectedNull = "RelationshipManager_UnexpectedNull";

		// Token: 0x040002EA RID: 746
		internal const string RelationshipManager_InvalidRelationshipManagerOwner = "RelationshipManager_InvalidRelationshipManagerOwner";

		// Token: 0x040002EB RID: 747
		internal const string RelationshipManager_OwnerIsNotSourceType = "RelationshipManager_OwnerIsNotSourceType";

		// Token: 0x040002EC RID: 748
		internal const string RelationshipManager_UnexpectedNullContext = "RelationshipManager_UnexpectedNullContext";

		// Token: 0x040002ED RID: 749
		internal const string RelationshipManager_ReferenceAlreadyInitialized = "RelationshipManager_ReferenceAlreadyInitialized";

		// Token: 0x040002EE RID: 750
		internal const string RelationshipManager_RelationshipManagerAttached = "RelationshipManager_RelationshipManagerAttached";

		// Token: 0x040002EF RID: 751
		internal const string RelationshipManager_InitializeIsForDeserialization = "RelationshipManager_InitializeIsForDeserialization";

		// Token: 0x040002F0 RID: 752
		internal const string RelationshipManager_CollectionAlreadyInitialized = "RelationshipManager_CollectionAlreadyInitialized";

		// Token: 0x040002F1 RID: 753
		internal const string RelationshipManager_CollectionRelationshipManagerAttached = "RelationshipManager_CollectionRelationshipManagerAttached";

		// Token: 0x040002F2 RID: 754
		internal const string RelationshipManager_CollectionInitializeIsForDeserialization = "RelationshipManager_CollectionInitializeIsForDeserialization";

		// Token: 0x040002F3 RID: 755
		internal const string RelationshipManager_NavigationPropertyNotFound = "RelationshipManager_NavigationPropertyNotFound";

		// Token: 0x040002F4 RID: 756
		internal const string RelationshipManager_CannotGetRelatEndForDetachedPocoEntity = "RelationshipManager_CannotGetRelatEndForDetachedPocoEntity";

		// Token: 0x040002F5 RID: 757
		internal const string ObjectView_CannotReplacetheEntityorRow = "ObjectView_CannotReplacetheEntityorRow";

		// Token: 0x040002F6 RID: 758
		internal const string ObjectView_IndexBasedInsertIsNotSupported = "ObjectView_IndexBasedInsertIsNotSupported";

		// Token: 0x040002F7 RID: 759
		internal const string ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList = "ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList";

		// Token: 0x040002F8 RID: 760
		internal const string ObjectView_AddNewOperationNotAllowedOnAbstractBindingList = "ObjectView_AddNewOperationNotAllowedOnAbstractBindingList";

		// Token: 0x040002F9 RID: 761
		internal const string ObjectView_IncompatibleArgument = "ObjectView_IncompatibleArgument";

		// Token: 0x040002FA RID: 762
		internal const string ObjectView_CannotResolveTheEntitySet = "ObjectView_CannotResolveTheEntitySet";

		// Token: 0x040002FB RID: 763
		internal const string CodeGen_ConstructorNoParameterless = "CodeGen_ConstructorNoParameterless";

		// Token: 0x040002FC RID: 764
		internal const string CodeGen_PropertyDeclaringTypeIsValueType = "CodeGen_PropertyDeclaringTypeIsValueType";

		// Token: 0x040002FD RID: 765
		internal const string CodeGen_PropertyStrongNameIdentity = "CodeGen_PropertyStrongNameIdentity";

		// Token: 0x040002FE RID: 766
		internal const string CodeGen_PropertyUnsupportedForm = "CodeGen_PropertyUnsupportedForm";

		// Token: 0x040002FF RID: 767
		internal const string CodeGen_PropertyUnsupportedType = "CodeGen_PropertyUnsupportedType";

		// Token: 0x04000300 RID: 768
		internal const string CodeGen_PropertyIsIndexed = "CodeGen_PropertyIsIndexed";

		// Token: 0x04000301 RID: 769
		internal const string CodeGen_PropertyIsStatic = "CodeGen_PropertyIsStatic";

		// Token: 0x04000302 RID: 770
		internal const string CodeGen_PropertyNoGetter = "CodeGen_PropertyNoGetter";

		// Token: 0x04000303 RID: 771
		internal const string CodeGen_PropertyNoSetter = "CodeGen_PropertyNoSetter";

		// Token: 0x04000304 RID: 772
		internal const string PocoEntityWrapper_UnableToSetFieldOrProperty = "PocoEntityWrapper_UnableToSetFieldOrProperty";

		// Token: 0x04000305 RID: 773
		internal const string PocoEntityWrapper_UnexpectedTypeForNavigationProperty = "PocoEntityWrapper_UnexpectedTypeForNavigationProperty";

		// Token: 0x04000306 RID: 774
		internal const string PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType = "PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType";

		// Token: 0x04000307 RID: 775
		internal const string GeneralQueryError = "GeneralQueryError";

		// Token: 0x04000308 RID: 776
		internal const string CtxAlias = "CtxAlias";

		// Token: 0x04000309 RID: 777
		internal const string CtxAliasedNamespaceImport = "CtxAliasedNamespaceImport";

		// Token: 0x0400030A RID: 778
		internal const string CtxAnd = "CtxAnd";

		// Token: 0x0400030B RID: 779
		internal const string CtxAnyElement = "CtxAnyElement";

		// Token: 0x0400030C RID: 780
		internal const string CtxApplyClause = "CtxApplyClause";

		// Token: 0x0400030D RID: 781
		internal const string CtxBetween = "CtxBetween";

		// Token: 0x0400030E RID: 782
		internal const string CtxCase = "CtxCase";

		// Token: 0x0400030F RID: 783
		internal const string CtxCaseElse = "CtxCaseElse";

		// Token: 0x04000310 RID: 784
		internal const string CtxCaseWhenThen = "CtxCaseWhenThen";

		// Token: 0x04000311 RID: 785
		internal const string CtxCast = "CtxCast";

		// Token: 0x04000312 RID: 786
		internal const string CtxCollatedOrderByClauseItem = "CtxCollatedOrderByClauseItem";

		// Token: 0x04000313 RID: 787
		internal const string CtxCollectionTypeDefinition = "CtxCollectionTypeDefinition";

		// Token: 0x04000314 RID: 788
		internal const string CtxCommandExpression = "CtxCommandExpression";

		// Token: 0x04000315 RID: 789
		internal const string CtxCreateRef = "CtxCreateRef";

		// Token: 0x04000316 RID: 790
		internal const string CtxDeref = "CtxDeref";

		// Token: 0x04000317 RID: 791
		internal const string CtxDivide = "CtxDivide";

		// Token: 0x04000318 RID: 792
		internal const string CtxElement = "CtxElement";

		// Token: 0x04000319 RID: 793
		internal const string CtxEquals = "CtxEquals";

		// Token: 0x0400031A RID: 794
		internal const string CtxEscapedIdentifier = "CtxEscapedIdentifier";

		// Token: 0x0400031B RID: 795
		internal const string CtxExcept = "CtxExcept";

		// Token: 0x0400031C RID: 796
		internal const string CtxExists = "CtxExists";

		// Token: 0x0400031D RID: 797
		internal const string CtxExpressionList = "CtxExpressionList";

		// Token: 0x0400031E RID: 798
		internal const string CtxFlatten = "CtxFlatten";

		// Token: 0x0400031F RID: 799
		internal const string CtxFromApplyClause = "CtxFromApplyClause";

		// Token: 0x04000320 RID: 800
		internal const string CtxFromClause = "CtxFromClause";

		// Token: 0x04000321 RID: 801
		internal const string CtxFromClauseItem = "CtxFromClauseItem";

		// Token: 0x04000322 RID: 802
		internal const string CtxFromClauseList = "CtxFromClauseList";

		// Token: 0x04000323 RID: 803
		internal const string CtxFromJoinClause = "CtxFromJoinClause";

		// Token: 0x04000324 RID: 804
		internal const string CtxFunction = "CtxFunction";

		// Token: 0x04000325 RID: 805
		internal const string CtxFunctionDefinition = "CtxFunctionDefinition";

		// Token: 0x04000326 RID: 806
		internal const string CtxGreaterThan = "CtxGreaterThan";

		// Token: 0x04000327 RID: 807
		internal const string CtxGreaterThanEqual = "CtxGreaterThanEqual";

		// Token: 0x04000328 RID: 808
		internal const string CtxGroupByClause = "CtxGroupByClause";

		// Token: 0x04000329 RID: 809
		internal const string CtxGroupPartition = "CtxGroupPartition";

		// Token: 0x0400032A RID: 810
		internal const string CtxHavingClause = "CtxHavingClause";

		// Token: 0x0400032B RID: 811
		internal const string CtxIdentifier = "CtxIdentifier";

		// Token: 0x0400032C RID: 812
		internal const string CtxIn = "CtxIn";

		// Token: 0x0400032D RID: 813
		internal const string CtxIntersect = "CtxIntersect";

		// Token: 0x0400032E RID: 814
		internal const string CtxIsNotNull = "CtxIsNotNull";

		// Token: 0x0400032F RID: 815
		internal const string CtxIsNotOf = "CtxIsNotOf";

		// Token: 0x04000330 RID: 816
		internal const string CtxIsNull = "CtxIsNull";

		// Token: 0x04000331 RID: 817
		internal const string CtxIsOf = "CtxIsOf";

		// Token: 0x04000332 RID: 818
		internal const string CtxJoinClause = "CtxJoinClause";

		// Token: 0x04000333 RID: 819
		internal const string CtxJoinOnClause = "CtxJoinOnClause";

		// Token: 0x04000334 RID: 820
		internal const string CtxKey = "CtxKey";

		// Token: 0x04000335 RID: 821
		internal const string CtxLessThan = "CtxLessThan";

		// Token: 0x04000336 RID: 822
		internal const string CtxLessThanEqual = "CtxLessThanEqual";

		// Token: 0x04000337 RID: 823
		internal const string CtxLike = "CtxLike";

		// Token: 0x04000338 RID: 824
		internal const string CtxLimitSubClause = "CtxLimitSubClause";

		// Token: 0x04000339 RID: 825
		internal const string CtxLiteral = "CtxLiteral";

		// Token: 0x0400033A RID: 826
		internal const string CtxMemberAccess = "CtxMemberAccess";

		// Token: 0x0400033B RID: 827
		internal const string CtxMethod = "CtxMethod";

		// Token: 0x0400033C RID: 828
		internal const string CtxMinus = "CtxMinus";

		// Token: 0x0400033D RID: 829
		internal const string CtxModulus = "CtxModulus";

		// Token: 0x0400033E RID: 830
		internal const string CtxMultiply = "CtxMultiply";

		// Token: 0x0400033F RID: 831
		internal const string CtxMultisetCtor = "CtxMultisetCtor";

		// Token: 0x04000340 RID: 832
		internal const string CtxNamespaceImport = "CtxNamespaceImport";

		// Token: 0x04000341 RID: 833
		internal const string CtxNamespaceImportList = "CtxNamespaceImportList";

		// Token: 0x04000342 RID: 834
		internal const string CtxNavigate = "CtxNavigate";

		// Token: 0x04000343 RID: 835
		internal const string CtxNot = "CtxNot";

		// Token: 0x04000344 RID: 836
		internal const string CtxNotBetween = "CtxNotBetween";

		// Token: 0x04000345 RID: 837
		internal const string CtxNotEqual = "CtxNotEqual";

		// Token: 0x04000346 RID: 838
		internal const string CtxNotIn = "CtxNotIn";

		// Token: 0x04000347 RID: 839
		internal const string CtxNotLike = "CtxNotLike";

		// Token: 0x04000348 RID: 840
		internal const string CtxNullLiteral = "CtxNullLiteral";

		// Token: 0x04000349 RID: 841
		internal const string CtxOfType = "CtxOfType";

		// Token: 0x0400034A RID: 842
		internal const string CtxOfTypeOnly = "CtxOfTypeOnly";

		// Token: 0x0400034B RID: 843
		internal const string CtxOr = "CtxOr";

		// Token: 0x0400034C RID: 844
		internal const string CtxOrderByClause = "CtxOrderByClause";

		// Token: 0x0400034D RID: 845
		internal const string CtxOrderByClauseItem = "CtxOrderByClauseItem";

		// Token: 0x0400034E RID: 846
		internal const string CtxOverlaps = "CtxOverlaps";

		// Token: 0x0400034F RID: 847
		internal const string CtxParen = "CtxParen";

		// Token: 0x04000350 RID: 848
		internal const string CtxPlus = "CtxPlus";

		// Token: 0x04000351 RID: 849
		internal const string CtxTypeNameWithTypeSpec = "CtxTypeNameWithTypeSpec";

		// Token: 0x04000352 RID: 850
		internal const string CtxQueryExpression = "CtxQueryExpression";

		// Token: 0x04000353 RID: 851
		internal const string CtxQueryStatement = "CtxQueryStatement";

		// Token: 0x04000354 RID: 852
		internal const string CtxRef = "CtxRef";

		// Token: 0x04000355 RID: 853
		internal const string CtxRefTypeDefinition = "CtxRefTypeDefinition";

		// Token: 0x04000356 RID: 854
		internal const string CtxRelationship = "CtxRelationship";

		// Token: 0x04000357 RID: 855
		internal const string CtxRelationshipList = "CtxRelationshipList";

		// Token: 0x04000358 RID: 856
		internal const string CtxRowCtor = "CtxRowCtor";

		// Token: 0x04000359 RID: 857
		internal const string CtxRowTypeDefinition = "CtxRowTypeDefinition";

		// Token: 0x0400035A RID: 858
		internal const string CtxSelectRowClause = "CtxSelectRowClause";

		// Token: 0x0400035B RID: 859
		internal const string CtxSelectValueClause = "CtxSelectValueClause";

		// Token: 0x0400035C RID: 860
		internal const string CtxSet = "CtxSet";

		// Token: 0x0400035D RID: 861
		internal const string CtxSimpleIdentifier = "CtxSimpleIdentifier";

		// Token: 0x0400035E RID: 862
		internal const string CtxSkipSubClause = "CtxSkipSubClause";

		// Token: 0x0400035F RID: 863
		internal const string CtxTopSubClause = "CtxTopSubClause";

		// Token: 0x04000360 RID: 864
		internal const string CtxTreat = "CtxTreat";

		// Token: 0x04000361 RID: 865
		internal const string CtxTypeCtor = "CtxTypeCtor";

		// Token: 0x04000362 RID: 866
		internal const string CtxTypeName = "CtxTypeName";

		// Token: 0x04000363 RID: 867
		internal const string CtxUnaryMinus = "CtxUnaryMinus";

		// Token: 0x04000364 RID: 868
		internal const string CtxUnaryPlus = "CtxUnaryPlus";

		// Token: 0x04000365 RID: 869
		internal const string CtxUnion = "CtxUnion";

		// Token: 0x04000366 RID: 870
		internal const string CtxUnionAll = "CtxUnionAll";

		// Token: 0x04000367 RID: 871
		internal const string CtxWhereClause = "CtxWhereClause";

		// Token: 0x04000368 RID: 872
		internal const string CannotConvertNumericLiteral = "CannotConvertNumericLiteral";

		// Token: 0x04000369 RID: 873
		internal const string GenericSyntaxError = "GenericSyntaxError";

		// Token: 0x0400036A RID: 874
		internal const string InFromClause = "InFromClause";

		// Token: 0x0400036B RID: 875
		internal const string InGroupClause = "InGroupClause";

		// Token: 0x0400036C RID: 876
		internal const string InRowCtor = "InRowCtor";

		// Token: 0x0400036D RID: 877
		internal const string InSelectProjectionList = "InSelectProjectionList";

		// Token: 0x0400036E RID: 878
		internal const string InvalidAliasName = "InvalidAliasName";

		// Token: 0x0400036F RID: 879
		internal const string InvalidEmptyIdentifier = "InvalidEmptyIdentifier";

		// Token: 0x04000370 RID: 880
		internal const string InvalidEmptyQuery = "InvalidEmptyQuery";

		// Token: 0x04000371 RID: 881
		internal const string InvalidEmptyQueryTextArgument = "InvalidEmptyQueryTextArgument";

		// Token: 0x04000372 RID: 882
		internal const string InvalidEscapedIdentifier = "InvalidEscapedIdentifier";

		// Token: 0x04000373 RID: 883
		internal const string InvalidEscapedIdentifierEOF = "InvalidEscapedIdentifierEOF";

		// Token: 0x04000374 RID: 884
		internal const string InvalidEscapedIdentifierUnbalanced = "InvalidEscapedIdentifierUnbalanced";

		// Token: 0x04000375 RID: 885
		internal const string InvalidOperatorSymbol = "InvalidOperatorSymbol";

		// Token: 0x04000376 RID: 886
		internal const string InvalidPunctuatorSymbol = "InvalidPunctuatorSymbol";

		// Token: 0x04000377 RID: 887
		internal const string InvalidSimpleIdentifier = "InvalidSimpleIdentifier";

		// Token: 0x04000378 RID: 888
		internal const string InvalidSimpleIdentifierNonASCII = "InvalidSimpleIdentifierNonASCII";

		// Token: 0x04000379 RID: 889
		internal const string LocalizedCollection = "LocalizedCollection";

		// Token: 0x0400037A RID: 890
		internal const string LocalizedColumn = "LocalizedColumn";

		// Token: 0x0400037B RID: 891
		internal const string LocalizedComplex = "LocalizedComplex";

		// Token: 0x0400037C RID: 892
		internal const string LocalizedEntity = "LocalizedEntity";

		// Token: 0x0400037D RID: 893
		internal const string LocalizedEntityContainerExpression = "LocalizedEntityContainerExpression";

		// Token: 0x0400037E RID: 894
		internal const string LocalizedFunction = "LocalizedFunction";

		// Token: 0x0400037F RID: 895
		internal const string LocalizedInlineFunction = "LocalizedInlineFunction";

		// Token: 0x04000380 RID: 896
		internal const string LocalizedKeyword = "LocalizedKeyword";

		// Token: 0x04000381 RID: 897
		internal const string LocalizedLeft = "LocalizedLeft";

		// Token: 0x04000382 RID: 898
		internal const string LocalizedLine = "LocalizedLine";

		// Token: 0x04000383 RID: 899
		internal const string LocalizedMetadataMemberExpression = "LocalizedMetadataMemberExpression";

		// Token: 0x04000384 RID: 900
		internal const string LocalizedNamespace = "LocalizedNamespace";

		// Token: 0x04000385 RID: 901
		internal const string LocalizedNear = "LocalizedNear";

		// Token: 0x04000386 RID: 902
		internal const string LocalizedPrimitive = "LocalizedPrimitive";

		// Token: 0x04000387 RID: 903
		internal const string LocalizedReference = "LocalizedReference";

		// Token: 0x04000388 RID: 904
		internal const string LocalizedRight = "LocalizedRight";

		// Token: 0x04000389 RID: 905
		internal const string LocalizedRow = "LocalizedRow";

		// Token: 0x0400038A RID: 906
		internal const string LocalizedTerm = "LocalizedTerm";

		// Token: 0x0400038B RID: 907
		internal const string LocalizedType = "LocalizedType";

		// Token: 0x0400038C RID: 908
		internal const string LocalizedValueExpression = "LocalizedValueExpression";

		// Token: 0x0400038D RID: 909
		internal const string PropertyCannotBeChangedAtThisTime = "PropertyCannotBeChangedAtThisTime";

		// Token: 0x0400038E RID: 910
		internal const string AliasNameAlreadyUsed = "AliasNameAlreadyUsed";

		// Token: 0x0400038F RID: 911
		internal const string AmbiguousFunctionArguments = "AmbiguousFunctionArguments";

		// Token: 0x04000390 RID: 912
		internal const string AmbiguousMetadataMemberName = "AmbiguousMetadataMemberName";

		// Token: 0x04000391 RID: 913
		internal const string ArgumentTypesAreIncompatible = "ArgumentTypesAreIncompatible";

		// Token: 0x04000392 RID: 914
		internal const string BetweenLimitsCannotBeUntypedNulls = "BetweenLimitsCannotBeUntypedNulls";

		// Token: 0x04000393 RID: 915
		internal const string BetweenLimitsTypesAreNotCompatible = "BetweenLimitsTypesAreNotCompatible";

		// Token: 0x04000394 RID: 916
		internal const string BetweenLimitsTypesAreNotOrderComparable = "BetweenLimitsTypesAreNotOrderComparable";

		// Token: 0x04000395 RID: 917
		internal const string BetweenValueIsNotOrderComparable = "BetweenValueIsNotOrderComparable";

		// Token: 0x04000396 RID: 918
		internal const string CannotCreateEmptyMultiset = "CannotCreateEmptyMultiset";

		// Token: 0x04000397 RID: 919
		internal const string CannotCreateMultisetofNulls = "CannotCreateMultisetofNulls";

		// Token: 0x04000398 RID: 920
		internal const string CannotInstantiateAbstractType = "CannotInstantiateAbstractType";

		// Token: 0x04000399 RID: 921
		internal const string CannotResolveNameToTypeOrFunction = "CannotResolveNameToTypeOrFunction";

		// Token: 0x0400039A RID: 922
		internal const string ConcatBuiltinNotSupported = "ConcatBuiltinNotSupported";

		// Token: 0x0400039B RID: 923
		internal const string CouldNotResolveIdentifier = "CouldNotResolveIdentifier";

		// Token: 0x0400039C RID: 924
		internal const string CreateRefTypeIdentifierMustBeASubOrSuperType = "CreateRefTypeIdentifierMustBeASubOrSuperType";

		// Token: 0x0400039D RID: 925
		internal const string CreateRefTypeIdentifierMustSpecifyAnEntityType = "CreateRefTypeIdentifierMustSpecifyAnEntityType";

		// Token: 0x0400039E RID: 926
		internal const string DeRefArgIsNotOfRefType = "DeRefArgIsNotOfRefType";

		// Token: 0x0400039F RID: 927
		internal const string DuplicatedInlineFunctionOverload = "DuplicatedInlineFunctionOverload";

		// Token: 0x040003A0 RID: 928
		internal const string ElementOperatorIsNotSupported = "ElementOperatorIsNotSupported";

		// Token: 0x040003A1 RID: 929
		internal const string EntitySetDoesNotBelongToEntityContainer = "EntitySetDoesNotBelongToEntityContainer";

		// Token: 0x040003A2 RID: 930
		internal const string ExpressionCannotBeNull = "ExpressionCannotBeNull";

		// Token: 0x040003A3 RID: 931
		internal const string OfTypeExpressionElementTypeMustBeEntityType = "OfTypeExpressionElementTypeMustBeEntityType";

		// Token: 0x040003A4 RID: 932
		internal const string OfTypeExpressionElementTypeMustBeNominalType = "OfTypeExpressionElementTypeMustBeNominalType";

		// Token: 0x040003A5 RID: 933
		internal const string ExpressionMustBeCollection = "ExpressionMustBeCollection";

		// Token: 0x040003A6 RID: 934
		internal const string ExpressionMustBeNumericType = "ExpressionMustBeNumericType";

		// Token: 0x040003A7 RID: 935
		internal const string ExpressionTypeMustBeBoolean = "ExpressionTypeMustBeBoolean";

		// Token: 0x040003A8 RID: 936
		internal const string ExpressionTypeMustBeEqualComparable = "ExpressionTypeMustBeEqualComparable";

		// Token: 0x040003A9 RID: 937
		internal const string ExpressionTypeMustBeEntityType = "ExpressionTypeMustBeEntityType";

		// Token: 0x040003AA RID: 938
		internal const string ExpressionTypeMustBeNominalType = "ExpressionTypeMustBeNominalType";

		// Token: 0x040003AB RID: 939
		internal const string ExpressionTypeMustNotBeCollection = "ExpressionTypeMustNotBeCollection";

		// Token: 0x040003AC RID: 940
		internal const string ExprIsNotValidEntitySetForCreateRef = "ExprIsNotValidEntitySetForCreateRef";

		// Token: 0x040003AD RID: 941
		internal const string FailedToResolveAggregateFunction = "FailedToResolveAggregateFunction";

		// Token: 0x040003AE RID: 942
		internal const string GeneralExceptionAsQueryInnerException = "GeneralExceptionAsQueryInnerException";

		// Token: 0x040003AF RID: 943
		internal const string GroupingKeysMustBeEqualComparable = "GroupingKeysMustBeEqualComparable";

		// Token: 0x040003B0 RID: 944
		internal const string GroupPartitionOutOfContext = "GroupPartitionOutOfContext";

		// Token: 0x040003B1 RID: 945
		internal const string HavingRequiresGroupClause = "HavingRequiresGroupClause";

		// Token: 0x040003B2 RID: 946
		internal const string ImcompatibleCreateRefKeyElementType = "ImcompatibleCreateRefKeyElementType";

		// Token: 0x040003B3 RID: 947
		internal const string ImcompatibleCreateRefKeyType = "ImcompatibleCreateRefKeyType";

		// Token: 0x040003B4 RID: 948
		internal const string InnerJoinMustHaveOnPredicate = "InnerJoinMustHaveOnPredicate";

		// Token: 0x040003B5 RID: 949
		internal const string InvalidAssociationTypeForUnion = "InvalidAssociationTypeForUnion";

		// Token: 0x040003B6 RID: 950
		internal const string InvalidCaseElseType = "InvalidCaseElseType";

		// Token: 0x040003B7 RID: 951
		internal const string InvalidCaseThenNullType = "InvalidCaseThenNullType";

		// Token: 0x040003B8 RID: 952
		internal const string InvalidCaseThenTypes = "InvalidCaseThenTypes";

		// Token: 0x040003B9 RID: 953
		internal const string InvalidCaseWhenThenNullType = "InvalidCaseWhenThenNullType";

		// Token: 0x040003BA RID: 954
		internal const string InvalidCast = "InvalidCast";

		// Token: 0x040003BB RID: 955
		internal const string InvalidCastExpressionType = "InvalidCastExpressionType";

		// Token: 0x040003BC RID: 956
		internal const string InvalidCastType = "InvalidCastType";

		// Token: 0x040003BD RID: 957
		internal const string InvalidComplexType = "InvalidComplexType";

		// Token: 0x040003BE RID: 958
		internal const string InvalidCreateRefKeyType = "InvalidCreateRefKeyType";

		// Token: 0x040003BF RID: 959
		internal const string InvalidCtorArgumentType = "InvalidCtorArgumentType";

		// Token: 0x040003C0 RID: 960
		internal const string InvalidCtorUseOnType = "InvalidCtorUseOnType";

		// Token: 0x040003C1 RID: 961
		internal const string InvalidDateTimeOffsetLiteral = "InvalidDateTimeOffsetLiteral";

		// Token: 0x040003C2 RID: 962
		internal const string InvalidDay = "InvalidDay";

		// Token: 0x040003C3 RID: 963
		internal const string InvalidDayInMonth = "InvalidDayInMonth";

		// Token: 0x040003C4 RID: 964
		internal const string InvalidDeRefProperty = "InvalidDeRefProperty";

		// Token: 0x040003C5 RID: 965
		internal const string InvalidDistinctArgumentInCtor = "InvalidDistinctArgumentInCtor";

		// Token: 0x040003C6 RID: 966
		internal const string InvalidDistinctArgumentInNonAggFunction = "InvalidDistinctArgumentInNonAggFunction";

		// Token: 0x040003C7 RID: 967
		internal const string InvalidEntityRootTypeArgument = "InvalidEntityRootTypeArgument";

		// Token: 0x040003C8 RID: 968
		internal const string InvalidEntityTypeArgument = "InvalidEntityTypeArgument";

		// Token: 0x040003C9 RID: 969
		internal const string InvalidExpressionResolutionClass = "InvalidExpressionResolutionClass";

		// Token: 0x040003CA RID: 970
		internal const string InvalidFlattenArgument = "InvalidFlattenArgument";

		// Token: 0x040003CB RID: 971
		internal const string InvalidGroupIdentifierReference = "InvalidGroupIdentifierReference";

		// Token: 0x040003CC RID: 972
		internal const string InvalidHour = "InvalidHour";

		// Token: 0x040003CD RID: 973
		internal const string InvalidImplicitRelationshipFromEnd = "InvalidImplicitRelationshipFromEnd";

		// Token: 0x040003CE RID: 974
		internal const string InvalidImplicitRelationshipToEnd = "InvalidImplicitRelationshipToEnd";

		// Token: 0x040003CF RID: 975
		internal const string InvalidInExprArgs = "InvalidInExprArgs";

		// Token: 0x040003D0 RID: 976
		internal const string InvalidJoinLeftCorrelation = "InvalidJoinLeftCorrelation";

		// Token: 0x040003D1 RID: 977
		internal const string InvalidKeyArgument = "InvalidKeyArgument";

		// Token: 0x040003D2 RID: 978
		internal const string InvalidKeyTypeForCollation = "InvalidKeyTypeForCollation";

		// Token: 0x040003D3 RID: 979
		internal const string InvalidLiteralFormat = "InvalidLiteralFormat";

		// Token: 0x040003D4 RID: 980
		internal const string InvalidMetadataMemberName = "InvalidMetadataMemberName";

		// Token: 0x040003D5 RID: 981
		internal const string InvalidMinute = "InvalidMinute";

		// Token: 0x040003D6 RID: 982
		internal const string InvalidModeForWithRelationshipClause = "InvalidModeForWithRelationshipClause";

		// Token: 0x040003D7 RID: 983
		internal const string InvalidMonth = "InvalidMonth";

		// Token: 0x040003D8 RID: 984
		internal const string InvalidNamespaceAlias = "InvalidNamespaceAlias";

		// Token: 0x040003D9 RID: 985
		internal const string InvalidNullArithmetic = "InvalidNullArithmetic";

		// Token: 0x040003DA RID: 986
		internal const string InvalidNullComparison = "InvalidNullComparison";

		// Token: 0x040003DB RID: 987
		internal const string InvalidNullLiteralForNonNullableMember = "InvalidNullLiteralForNonNullableMember";

		// Token: 0x040003DC RID: 988
		internal const string InvalidParameterFormat = "InvalidParameterFormat";

		// Token: 0x040003DD RID: 989
		internal const string InvalidPlaceholderRootTypeArgument = "InvalidPlaceholderRootTypeArgument";

		// Token: 0x040003DE RID: 990
		internal const string InvalidPlaceholderTypeArgument = "InvalidPlaceholderTypeArgument";

		// Token: 0x040003DF RID: 991
		internal const string InvalidPredicateForCrossJoin = "InvalidPredicateForCrossJoin";

		// Token: 0x040003E0 RID: 992
		internal const string InvalidRelationshipMember = "InvalidRelationshipMember";

		// Token: 0x040003E1 RID: 993
		internal const string InvalidRelationshipSourceType = "InvalidRelationshipSourceType";

		// Token: 0x040003E2 RID: 994
		internal const string InvalidMetadataMemberClassResolution = "InvalidMetadataMemberClassResolution";

		// Token: 0x040003E3 RID: 995
		internal const string InvalidRootComplexType = "InvalidRootComplexType";

		// Token: 0x040003E4 RID: 996
		internal const string InvalidRootRowType = "InvalidRootRowType";

		// Token: 0x040003E5 RID: 997
		internal const string InvalidRowType = "InvalidRowType";

		// Token: 0x040003E6 RID: 998
		internal const string InvalidSecond = "InvalidSecond";

		// Token: 0x040003E7 RID: 999
		internal const string InvalidSelectValueAliasedExpression = "InvalidSelectValueAliasedExpression";

		// Token: 0x040003E8 RID: 1000
		internal const string InvalidSelectValueList = "InvalidSelectValueList";

		// Token: 0x040003E9 RID: 1001
		internal const string InvalidUnarySetOpArgument = "InvalidUnarySetOpArgument";

		// Token: 0x040003EA RID: 1002
		internal const string InvalidUnsignedTypeForUnaryMinusOperation = "InvalidUnsignedTypeForUnaryMinusOperation";

		// Token: 0x040003EB RID: 1003
		internal const string InvalidYear = "InvalidYear";

		// Token: 0x040003EC RID: 1004
		internal const string InvalidWithRelationshipTargetEndMultiplicity = "InvalidWithRelationshipTargetEndMultiplicity";

		// Token: 0x040003ED RID: 1005
		internal const string InvalidQueryResultType = "InvalidQueryResultType";

		// Token: 0x040003EE RID: 1006
		internal const string IsNullInvalidType = "IsNullInvalidType";

		// Token: 0x040003EF RID: 1007
		internal const string KeyMustBeCorrelated = "KeyMustBeCorrelated";

		// Token: 0x040003F0 RID: 1008
		internal const string LeftSetExpressionArgsMustBeCollection = "LeftSetExpressionArgsMustBeCollection";

		// Token: 0x040003F1 RID: 1009
		internal const string LikeArgMustBeStringType = "LikeArgMustBeStringType";

		// Token: 0x040003F2 RID: 1010
		internal const string LiteralTypeNotFoundInMetadata = "LiteralTypeNotFoundInMetadata";

		// Token: 0x040003F3 RID: 1011
		internal const string MalformedSingleQuotePayload = "MalformedSingleQuotePayload";

		// Token: 0x040003F4 RID: 1012
		internal const string MalformedStringLiteralPayload = "MalformedStringLiteralPayload";

		// Token: 0x040003F5 RID: 1013
		internal const string MethodInvocationNotSupported = "MethodInvocationNotSupported";

		// Token: 0x040003F6 RID: 1014
		internal const string MultipleDefinitionsOfParameter = "MultipleDefinitionsOfParameter";

		// Token: 0x040003F7 RID: 1015
		internal const string MultipleDefinitionsOfVariable = "MultipleDefinitionsOfVariable";

		// Token: 0x040003F8 RID: 1016
		internal const string MultisetElemsAreNotTypeCompatible = "MultisetElemsAreNotTypeCompatible";

		// Token: 0x040003F9 RID: 1017
		internal const string NamespaceAliasAlreadyUsed = "NamespaceAliasAlreadyUsed";

		// Token: 0x040003FA RID: 1018
		internal const string NamespaceAlreadyImported = "NamespaceAlreadyImported";

		// Token: 0x040003FB RID: 1019
		internal const string NestedAggregateCannotBeUsedInAggregate = "NestedAggregateCannotBeUsedInAggregate";

		// Token: 0x040003FC RID: 1020
		internal const string NoAggrFunctionOverloadMatch = "NoAggrFunctionOverloadMatch";

		// Token: 0x040003FD RID: 1021
		internal const string NoCanonicalAggrFunctionOverloadMatch = "NoCanonicalAggrFunctionOverloadMatch";

		// Token: 0x040003FE RID: 1022
		internal const string NoCanonicalFunctionOverloadMatch = "NoCanonicalFunctionOverloadMatch";

		// Token: 0x040003FF RID: 1023
		internal const string NoFunctionOverloadMatch = "NoFunctionOverloadMatch";

		// Token: 0x04000400 RID: 1024
		internal const string NotAMemberOfCollection = "NotAMemberOfCollection";

		// Token: 0x04000401 RID: 1025
		internal const string NotAMemberOfType = "NotAMemberOfType";

		// Token: 0x04000402 RID: 1026
		internal const string NotASuperOrSubType = "NotASuperOrSubType";

		// Token: 0x04000403 RID: 1027
		internal const string NullLiteralCannotBePromotedToCollectionOfNulls = "NullLiteralCannotBePromotedToCollectionOfNulls";

		// Token: 0x04000404 RID: 1028
		internal const string NumberOfTypeCtorIsLessThenFormalSpec = "NumberOfTypeCtorIsLessThenFormalSpec";

		// Token: 0x04000405 RID: 1029
		internal const string NumberOfTypeCtorIsMoreThenFormalSpec = "NumberOfTypeCtorIsMoreThenFormalSpec";

		// Token: 0x04000406 RID: 1030
		internal const string OrderByKeyIsNotOrderComparable = "OrderByKeyIsNotOrderComparable";

		// Token: 0x04000407 RID: 1031
		internal const string OfTypeOnlyTypeArgumentCannotBeAbstract = "OfTypeOnlyTypeArgumentCannotBeAbstract";

		// Token: 0x04000408 RID: 1032
		internal const string ParameterTypeNotSupported = "ParameterTypeNotSupported";

		// Token: 0x04000409 RID: 1033
		internal const string ParameterWasNotDefined = "ParameterWasNotDefined";

		// Token: 0x0400040A RID: 1034
		internal const string PlaceholderExpressionMustBeCompatibleWithEdm64 = "PlaceholderExpressionMustBeCompatibleWithEdm64";

		// Token: 0x0400040B RID: 1035
		internal const string PlaceholderExpressionMustBeConstant = "PlaceholderExpressionMustBeConstant";

		// Token: 0x0400040C RID: 1036
		internal const string PlaceholderExpressionMustBeGreaterThanOrEqualToZero = "PlaceholderExpressionMustBeGreaterThanOrEqualToZero";

		// Token: 0x0400040D RID: 1037
		internal const string PlaceholderSetArgTypeIsNotEqualComparable = "PlaceholderSetArgTypeIsNotEqualComparable";

		// Token: 0x0400040E RID: 1038
		internal const string PlusLeftExpressionInvalidType = "PlusLeftExpressionInvalidType";

		// Token: 0x0400040F RID: 1039
		internal const string PlusRightExpressionInvalidType = "PlusRightExpressionInvalidType";

		// Token: 0x04000410 RID: 1040
		internal const string PrecisionMustBeGreaterThanScale = "PrecisionMustBeGreaterThanScale";

		// Token: 0x04000411 RID: 1041
		internal const string RefArgIsNotOfEntityType = "RefArgIsNotOfEntityType";

		// Token: 0x04000412 RID: 1042
		internal const string RefTypeIdentifierMustSpecifyAnEntityType = "RefTypeIdentifierMustSpecifyAnEntityType";

		// Token: 0x04000413 RID: 1043
		internal const string RelationshipFromEndIsAmbiguos = "RelationshipFromEndIsAmbiguos";

		// Token: 0x04000414 RID: 1044
		internal const string RelationshipTypeIsNotCompatibleWithEntity = "RelationshipTypeIsNotCompatibleWithEntity";

		// Token: 0x04000415 RID: 1045
		internal const string RelationshipTargetMustBeUnique = "RelationshipTargetMustBeUnique";

		// Token: 0x04000416 RID: 1046
		internal const string ResultingExpressionTypeCannotBeNull = "ResultingExpressionTypeCannotBeNull";

		// Token: 0x04000417 RID: 1047
		internal const string RightSetExpressionArgsMustBeCollection = "RightSetExpressionArgsMustBeCollection";

		// Token: 0x04000418 RID: 1048
		internal const string RowCtorElementCannotBeNull = "RowCtorElementCannotBeNull";

		// Token: 0x04000419 RID: 1049
		internal const string SelectDistinctMustBeEqualComparable = "SelectDistinctMustBeEqualComparable";

		// Token: 0x0400041A RID: 1050
		internal const string SourceTypeMustBePromotoableToFromEndRelationType = "SourceTypeMustBePromotoableToFromEndRelationType";

		// Token: 0x0400041B RID: 1051
		internal const string TopAndLimitCannotCoexist = "TopAndLimitCannotCoexist";

		// Token: 0x0400041C RID: 1052
		internal const string TopAndSkipCannotCoexist = "TopAndSkipCannotCoexist";

		// Token: 0x0400041D RID: 1053
		internal const string TypeDoesNotSupportSpec = "TypeDoesNotSupportSpec";

		// Token: 0x0400041E RID: 1054
		internal const string TypeDoesNotSupportFacet = "TypeDoesNotSupportFacet";

		// Token: 0x0400041F RID: 1055
		internal const string TypeArgumentCountMismatch = "TypeArgumentCountMismatch";

		// Token: 0x04000420 RID: 1056
		internal const string TypeArgumentMustBeLiteral = "TypeArgumentMustBeLiteral";

		// Token: 0x04000421 RID: 1057
		internal const string TypeArgumentBelowMin = "TypeArgumentBelowMin";

		// Token: 0x04000422 RID: 1058
		internal const string TypeArgumentExceedsMax = "TypeArgumentExceedsMax";

		// Token: 0x04000423 RID: 1059
		internal const string TypeArgumentIsNotValid = "TypeArgumentIsNotValid";

		// Token: 0x04000424 RID: 1060
		internal const string TypeKindMismatch = "TypeKindMismatch";

		// Token: 0x04000425 RID: 1061
		internal const string TypeMustBeInheritableType = "TypeMustBeInheritableType";

		// Token: 0x04000426 RID: 1062
		internal const string TypeMustBeEntityType = "TypeMustBeEntityType";

		// Token: 0x04000427 RID: 1063
		internal const string TypeMustBeNominalType = "TypeMustBeNominalType";

		// Token: 0x04000428 RID: 1064
		internal const string TypeNameNotFound = "TypeNameNotFound";

		// Token: 0x04000429 RID: 1065
		internal const string GroupVarNotFoundInScope = "GroupVarNotFoundInScope";

		// Token: 0x0400042A RID: 1066
		internal const string InvalidArgumentTypeForAggregateFunction = "InvalidArgumentTypeForAggregateFunction";

		// Token: 0x0400042B RID: 1067
		internal const string InvalidSavePoint = "InvalidSavePoint";

		// Token: 0x0400042C RID: 1068
		internal const string InvalidScopeIndex = "InvalidScopeIndex";

		// Token: 0x0400042D RID: 1069
		internal const string LiteralTypeNotSupported = "LiteralTypeNotSupported";

		// Token: 0x0400042E RID: 1070
		internal const string ParserFatalError = "ParserFatalError";

		// Token: 0x0400042F RID: 1071
		internal const string ParserInputError = "ParserInputError";

		// Token: 0x04000430 RID: 1072
		internal const string StackOverflowInParser = "StackOverflowInParser";

		// Token: 0x04000431 RID: 1073
		internal const string UnknownAstCommandExpression = "UnknownAstCommandExpression";

		// Token: 0x04000432 RID: 1074
		internal const string UnknownAstExpressionType = "UnknownAstExpressionType";

		// Token: 0x04000433 RID: 1075
		internal const string UnknownBuiltInAstExpressionType = "UnknownBuiltInAstExpressionType";

		// Token: 0x04000434 RID: 1076
		internal const string UnknownExpressionResolutionClass = "UnknownExpressionResolutionClass";

		// Token: 0x04000435 RID: 1077
		internal const string SqlGen_ApplyNotSupportedOnSql8 = "SqlGen_ApplyNotSupportedOnSql8";

		// Token: 0x04000436 RID: 1078
		internal const string SqlGen_InvalidDatePartArgumentExpression = "SqlGen_InvalidDatePartArgumentExpression";

		// Token: 0x04000437 RID: 1079
		internal const string SqlGen_InvalidDatePartArgumentValue = "SqlGen_InvalidDatePartArgumentValue";

		// Token: 0x04000438 RID: 1080
		internal const string SqlGen_NiladicFunctionsCannotHaveParameters = "SqlGen_NiladicFunctionsCannotHaveParameters";

		// Token: 0x04000439 RID: 1081
		internal const string SqlGen_ParameterForLimitNotSupportedOnSql8 = "SqlGen_ParameterForLimitNotSupportedOnSql8";

		// Token: 0x0400043A RID: 1082
		internal const string SqlGen_ParameterForSkipNotSupportedOnSql8 = "SqlGen_ParameterForSkipNotSupportedOnSql8";

		// Token: 0x0400043B RID: 1083
		internal const string SqlGen_PrimitiveTypeNotSupportedPriorSql10 = "SqlGen_PrimitiveTypeNotSupportedPriorSql10";

		// Token: 0x0400043C RID: 1084
		internal const string SqlGen_CanonicalFunctionNotSupportedPriorSql10 = "SqlGen_CanonicalFunctionNotSupportedPriorSql10";

		// Token: 0x0400043D RID: 1085
		internal const string SqlGen_TypedPositiveInfinityNotSupported = "SqlGen_TypedPositiveInfinityNotSupported";

		// Token: 0x0400043E RID: 1086
		internal const string SqlGen_TypedNegativeInfinityNotSupported = "SqlGen_TypedNegativeInfinityNotSupported";

		// Token: 0x0400043F RID: 1087
		internal const string SqlGen_TypedNaNNotSupported = "SqlGen_TypedNaNNotSupported";

		// Token: 0x04000440 RID: 1088
		internal const string Cqt_General_NullTypeInvalid = "Cqt_General_NullTypeInvalid";

		// Token: 0x04000441 RID: 1089
		internal const string Cqt_General_PolymorphicTypeRequired = "Cqt_General_PolymorphicTypeRequired";

		// Token: 0x04000442 RID: 1090
		internal const string Cqt_General_PolymorphicArgRequired = "Cqt_General_PolymorphicArgRequired";

		// Token: 0x04000443 RID: 1091
		internal const string Cqt_General_UnsupportedExpression = "Cqt_General_UnsupportedExpression";

		// Token: 0x04000444 RID: 1092
		internal const string Cqt_General_MetadataNotReadOnly = "Cqt_General_MetadataNotReadOnly";

		// Token: 0x04000445 RID: 1093
		internal const string Cqt_General_NoProviderBooleanType = "Cqt_General_NoProviderBooleanType";

		// Token: 0x04000446 RID: 1094
		internal const string Cqt_General_NoProviderIntegerType = "Cqt_General_NoProviderIntegerType";

		// Token: 0x04000447 RID: 1095
		internal const string Cqt_General_NoProviderStringType = "Cqt_General_NoProviderStringType";

		// Token: 0x04000448 RID: 1096
		internal const string Cqt_Metadata_EdmMemberIncorrectSpace = "Cqt_Metadata_EdmMemberIncorrectSpace";

		// Token: 0x04000449 RID: 1097
		internal const string Cqt_Metadata_EntitySetEntityContainerNull = "Cqt_Metadata_EntitySetEntityContainerNull";

		// Token: 0x0400044A RID: 1098
		internal const string Cqt_Metadata_EntitySetIncorrectSpace = "Cqt_Metadata_EntitySetIncorrectSpace";

		// Token: 0x0400044B RID: 1099
		internal const string Cqt_Metadata_EntityTypeNullKeyMembersInvalid = "Cqt_Metadata_EntityTypeNullKeyMembersInvalid";

		// Token: 0x0400044C RID: 1100
		internal const string Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid = "Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid";

		// Token: 0x0400044D RID: 1101
		internal const string Cqt_Metadata_FunctionReturnParameterNull = "Cqt_Metadata_FunctionReturnParameterNull";

		// Token: 0x0400044E RID: 1102
		internal const string Cqt_Metadata_FunctionIncorrectSpace = "Cqt_Metadata_FunctionIncorrectSpace";

		// Token: 0x0400044F RID: 1103
		internal const string Cqt_Metadata_FunctionParameterIncorrectSpace = "Cqt_Metadata_FunctionParameterIncorrectSpace";

		// Token: 0x04000450 RID: 1104
		internal const string Cqt_Metadata_TypeUsageIncorrectSpace = "Cqt_Metadata_TypeUsageIncorrectSpace";

		// Token: 0x04000451 RID: 1105
		internal const string Cqt_Exceptions_InvalidCommandTree = "Cqt_Exceptions_InvalidCommandTree";

		// Token: 0x04000452 RID: 1106
		internal const string Cqt_Util_CheckListEmptyInvalid = "Cqt_Util_CheckListEmptyInvalid";

		// Token: 0x04000453 RID: 1107
		internal const string Cqt_Util_CheckListDuplicateName = "Cqt_Util_CheckListDuplicateName";

		// Token: 0x04000454 RID: 1108
		internal const string Cqt_ExpressionLink_TypeMismatch = "Cqt_ExpressionLink_TypeMismatch";

		// Token: 0x04000455 RID: 1109
		internal const string Cqt_ExpressionList_IncorrectElementCount = "Cqt_ExpressionList_IncorrectElementCount";

		// Token: 0x04000456 RID: 1110
		internal const string Cqt_Copier_EntityContainerNotFound = "Cqt_Copier_EntityContainerNotFound";

		// Token: 0x04000457 RID: 1111
		internal const string Cqt_Copier_EntitySetNotFound = "Cqt_Copier_EntitySetNotFound";

		// Token: 0x04000458 RID: 1112
		internal const string Cqt_Copier_FunctionNotFound = "Cqt_Copier_FunctionNotFound";

		// Token: 0x04000459 RID: 1113
		internal const string Cqt_Copier_PropertyNotFound = "Cqt_Copier_PropertyNotFound";

		// Token: 0x0400045A RID: 1114
		internal const string Cqt_Copier_NavPropertyNotFound = "Cqt_Copier_NavPropertyNotFound";

		// Token: 0x0400045B RID: 1115
		internal const string Cqt_Copier_EndNotFound = "Cqt_Copier_EndNotFound";

		// Token: 0x0400045C RID: 1116
		internal const string Cqt_Copier_TypeNotFound = "Cqt_Copier_TypeNotFound";

		// Token: 0x0400045D RID: 1117
		internal const string Cqt_CommandTree_InvalidDataSpace = "Cqt_CommandTree_InvalidDataSpace";

		// Token: 0x0400045E RID: 1118
		internal const string Cqt_CommandTree_InvalidParameterName = "Cqt_CommandTree_InvalidParameterName";

		// Token: 0x0400045F RID: 1119
		internal const string Cqt_Validator_InvalidIncompatibleParameterReferences = "Cqt_Validator_InvalidIncompatibleParameterReferences";

		// Token: 0x04000460 RID: 1120
		internal const string Cqt_Validator_InvalidOtherWorkspaceMetadata = "Cqt_Validator_InvalidOtherWorkspaceMetadata";

		// Token: 0x04000461 RID: 1121
		internal const string Cqt_Validator_InvalidIncorrectDataSpaceMetadata = "Cqt_Validator_InvalidIncorrectDataSpaceMetadata";

		// Token: 0x04000462 RID: 1122
		internal const string Cqt_Factory_NewCollectionInvalidCommonType = "Cqt_Factory_NewCollectionInvalidCommonType";

		// Token: 0x04000463 RID: 1123
		internal const string Cqt_Factory_NoSuchProperty = "Cqt_Factory_NoSuchProperty";

		// Token: 0x04000464 RID: 1124
		internal const string Cqt_Factory_NoSuchRelationEnd = "Cqt_Factory_NoSuchRelationEnd";

		// Token: 0x04000465 RID: 1125
		internal const string Cqt_Factory_IncompatibleRelationEnds = "Cqt_Factory_IncompatibleRelationEnds";

		// Token: 0x04000466 RID: 1126
		internal const string Cqt_Factory_MethodResultTypeNotSupported = "Cqt_Factory_MethodResultTypeNotSupported";

		// Token: 0x04000467 RID: 1127
		internal const string Cqt_Aggregate_InvalidFunction = "Cqt_Aggregate_InvalidFunction";

		// Token: 0x04000468 RID: 1128
		internal const string Cqt_Binding_CollectionRequired = "Cqt_Binding_CollectionRequired";

		// Token: 0x04000469 RID: 1129
		internal const string Cqt_Binding_VariableNameNotValid = "Cqt_Binding_VariableNameNotValid";

		// Token: 0x0400046A RID: 1130
		internal const string Cqt_GroupBinding_CollectionRequired = "Cqt_GroupBinding_CollectionRequired";

		// Token: 0x0400046B RID: 1131
		internal const string Cqt_GroupBinding_GroupVariableNameNotValid = "Cqt_GroupBinding_GroupVariableNameNotValid";

		// Token: 0x0400046C RID: 1132
		internal const string Cqt_Binary_CollectionsRequired = "Cqt_Binary_CollectionsRequired";

		// Token: 0x0400046D RID: 1133
		internal const string Cqt_Unary_CollectionRequired = "Cqt_Unary_CollectionRequired";

		// Token: 0x0400046E RID: 1134
		internal const string Cqt_And_BooleanArgumentsRequired = "Cqt_And_BooleanArgumentsRequired";

		// Token: 0x0400046F RID: 1135
		internal const string Cqt_Apply_DuplicateVariableNames = "Cqt_Apply_DuplicateVariableNames";

		// Token: 0x04000470 RID: 1136
		internal const string Cqt_Arithmetic_NumericCommonType = "Cqt_Arithmetic_NumericCommonType";

		// Token: 0x04000471 RID: 1137
		internal const string Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus = "Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus";

		// Token: 0x04000472 RID: 1138
		internal const string Cqt_Case_WhensMustEqualThens = "Cqt_Case_WhensMustEqualThens";

		// Token: 0x04000473 RID: 1139
		internal const string Cqt_Case_InvalidResultType = "Cqt_Case_InvalidResultType";

		// Token: 0x04000474 RID: 1140
		internal const string Cqt_Cast_InvalidCast = "Cqt_Cast_InvalidCast";

		// Token: 0x04000475 RID: 1141
		internal const string Cqt_Comparison_ComparableRequired = "Cqt_Comparison_ComparableRequired";

		// Token: 0x04000476 RID: 1142
		internal const string Cqt_Constant_InvalidType = "Cqt_Constant_InvalidType";

		// Token: 0x04000477 RID: 1143
		internal const string Cqt_Constant_InvalidValueForType = "Cqt_Constant_InvalidValueForType";

		// Token: 0x04000478 RID: 1144
		internal const string Cqt_Constant_InvalidConstantType = "Cqt_Constant_InvalidConstantType";

		// Token: 0x04000479 RID: 1145
		internal const string Cqt_Distinct_InvalidCollection = "Cqt_Distinct_InvalidCollection";

		// Token: 0x0400047A RID: 1146
		internal const string Cqt_DeRef_RefRequired = "Cqt_DeRef_RefRequired";

		// Token: 0x0400047B RID: 1147
		internal const string Cqt_Element_InvalidArgumentForUnwrapSingleProperty = "Cqt_Element_InvalidArgumentForUnwrapSingleProperty";

		// Token: 0x0400047C RID: 1148
		internal const string Cqt_Except_LeftNullTypeInvalid = "Cqt_Except_LeftNullTypeInvalid";

		// Token: 0x0400047D RID: 1149
		internal const string Cqt_Function_VoidResultInvalid = "Cqt_Function_VoidResultInvalid";

		// Token: 0x0400047E RID: 1150
		internal const string Cqt_Function_NonComposableInExpression = "Cqt_Function_NonComposableInExpression";

		// Token: 0x0400047F RID: 1151
		internal const string Cqt_Function_CommandTextInExpression = "Cqt_Function_CommandTextInExpression";

		// Token: 0x04000480 RID: 1152
		internal const string Cqt_Function_CanonicalFunction_NotFound = "Cqt_Function_CanonicalFunction_NotFound";

		// Token: 0x04000481 RID: 1153
		internal const string Cqt_Function_CanonicalFunction_AmbiguousMatch = "Cqt_Function_CanonicalFunction_AmbiguousMatch";

		// Token: 0x04000482 RID: 1154
		internal const string Cqt_GetEntityRef_EntityRequired = "Cqt_GetEntityRef_EntityRequired";

		// Token: 0x04000483 RID: 1155
		internal const string Cqt_GetRefKey_RefRequired = "Cqt_GetRefKey_RefRequired";

		// Token: 0x04000484 RID: 1156
		internal const string Cqt_GroupBy_AtLeastOneKeyOrAggregate = "Cqt_GroupBy_AtLeastOneKeyOrAggregate";

		// Token: 0x04000485 RID: 1157
		internal const string Cqt_GroupBy_KeyNotEqualityComparable = "Cqt_GroupBy_KeyNotEqualityComparable";

		// Token: 0x04000486 RID: 1158
		internal const string Cqt_GroupBy_AggregateColumnExistsAsGroupColumn = "Cqt_GroupBy_AggregateColumnExistsAsGroupColumn";

		// Token: 0x04000487 RID: 1159
		internal const string Cqt_GroupBy_MoreThanOneGroupAggregate = "Cqt_GroupBy_MoreThanOneGroupAggregate";

		// Token: 0x04000488 RID: 1160
		internal const string Cqt_CrossJoin_AtLeastTwoInputs = "Cqt_CrossJoin_AtLeastTwoInputs";

		// Token: 0x04000489 RID: 1161
		internal const string Cqt_CrossJoin_DuplicateVariableNames = "Cqt_CrossJoin_DuplicateVariableNames";

		// Token: 0x0400048A RID: 1162
		internal const string Cqt_IsNull_CollectionNotAllowed = "Cqt_IsNull_CollectionNotAllowed";

		// Token: 0x0400048B RID: 1163
		internal const string Cqt_IsNull_InvalidType = "Cqt_IsNull_InvalidType";

		// Token: 0x0400048C RID: 1164
		internal const string Cqt_InvalidTypeForSetOperation = "Cqt_InvalidTypeForSetOperation";

		// Token: 0x0400048D RID: 1165
		internal const string Cqt_Join_DuplicateVariableNames = "Cqt_Join_DuplicateVariableNames";

		// Token: 0x0400048E RID: 1166
		internal const string Cqt_Limit_ConstantOrParameterRefRequired = "Cqt_Limit_ConstantOrParameterRefRequired";

		// Token: 0x0400048F RID: 1167
		internal const string Cqt_Limit_IntegerRequired = "Cqt_Limit_IntegerRequired";

		// Token: 0x04000490 RID: 1168
		internal const string Cqt_Limit_NonNegativeLimitRequired = "Cqt_Limit_NonNegativeLimitRequired";

		// Token: 0x04000491 RID: 1169
		internal const string Cqt_NewInstance_CollectionTypeRequired = "Cqt_NewInstance_CollectionTypeRequired";

		// Token: 0x04000492 RID: 1170
		internal const string Cqt_NewInstance_StructuralTypeRequired = "Cqt_NewInstance_StructuralTypeRequired";

		// Token: 0x04000493 RID: 1171
		internal const string Cqt_NewInstance_CannotInstantiateMemberlessType = "Cqt_NewInstance_CannotInstantiateMemberlessType";

		// Token: 0x04000494 RID: 1172
		internal const string Cqt_NewInstance_CannotInstantiateAbstractType = "Cqt_NewInstance_CannotInstantiateAbstractType";

		// Token: 0x04000495 RID: 1173
		internal const string Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid = "Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid";

		// Token: 0x04000496 RID: 1174
		internal const string Cqt_Not_BooleanArgumentRequired = "Cqt_Not_BooleanArgumentRequired";

		// Token: 0x04000497 RID: 1175
		internal const string Cqt_Or_BooleanArgumentsRequired = "Cqt_Or_BooleanArgumentsRequired";

		// Token: 0x04000498 RID: 1176
		internal const string Cqt_Property_InstanceRequiredForInstance = "Cqt_Property_InstanceRequiredForInstance";

		// Token: 0x04000499 RID: 1177
		internal const string Cqt_Ref_PolymorphicArgRequired = "Cqt_Ref_PolymorphicArgRequired";

		// Token: 0x0400049A RID: 1178
		internal const string Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship = "Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship";

		// Token: 0x0400049B RID: 1179
		internal const string Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne = "Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne";

		// Token: 0x0400049C RID: 1180
		internal const string Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd = "Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd";

		// Token: 0x0400049D RID: 1181
		internal const string Cqt_RelatedEntityRef_TargetEntityNotRef = "Cqt_RelatedEntityRef_TargetEntityNotRef";

		// Token: 0x0400049E RID: 1182
		internal const string Cqt_RelatedEntityRef_TargetEntityNotCompatible = "Cqt_RelatedEntityRef_TargetEntityNotCompatible";

		// Token: 0x0400049F RID: 1183
		internal const string Cqt_RelNav_NoCompositions = "Cqt_RelNav_NoCompositions";

		// Token: 0x040004A0 RID: 1184
		internal const string Cqt_RelNav_WrongSourceType = "Cqt_RelNav_WrongSourceType";

		// Token: 0x040004A1 RID: 1185
		internal const string Cqt_Skip_ConstantOrParameterRefRequired = "Cqt_Skip_ConstantOrParameterRefRequired";

		// Token: 0x040004A2 RID: 1186
		internal const string Cqt_Skip_IntegerRequired = "Cqt_Skip_IntegerRequired";

		// Token: 0x040004A3 RID: 1187
		internal const string Cqt_Skip_NonNegativeCountRequired = "Cqt_Skip_NonNegativeCountRequired";

		// Token: 0x040004A4 RID: 1188
		internal const string Cqt_Sort_EmptyCollationInvalid = "Cqt_Sort_EmptyCollationInvalid";

		// Token: 0x040004A5 RID: 1189
		internal const string Cqt_Sort_NonStringCollationInvalid = "Cqt_Sort_NonStringCollationInvalid";

		// Token: 0x040004A6 RID: 1190
		internal const string Cqt_Sort_OrderComparable = "Cqt_Sort_OrderComparable";

		// Token: 0x040004A7 RID: 1191
		internal const string Cqt_UDF_FunctionDefinitionGenerationFailed = "Cqt_UDF_FunctionDefinitionGenerationFailed";

		// Token: 0x040004A8 RID: 1192
		internal const string Cqt_UDF_FunctionDefinitionWithCircularReference = "Cqt_UDF_FunctionDefinitionWithCircularReference";

		// Token: 0x040004A9 RID: 1193
		internal const string Cqt_UDF_FunctionDefinitionResultTypeMismatch = "Cqt_UDF_FunctionDefinitionResultTypeMismatch";

		// Token: 0x040004AA RID: 1194
		internal const string Cqt_Validator_VarRefInvalid = "Cqt_Validator_VarRefInvalid";

		// Token: 0x040004AB RID: 1195
		internal const string Cqt_Validator_VarRefTypeMismatch = "Cqt_Validator_VarRefTypeMismatch";

		// Token: 0x040004AC RID: 1196
		internal const string Iqt_General_UnsupportedOp = "Iqt_General_UnsupportedOp";

		// Token: 0x040004AD RID: 1197
		internal const string Iqt_CTGen_UnexpectedAggregate = "Iqt_CTGen_UnexpectedAggregate";

		// Token: 0x040004AE RID: 1198
		internal const string Iqt_CTGen_UnexpectedVarDefList = "Iqt_CTGen_UnexpectedVarDefList";

		// Token: 0x040004AF RID: 1199
		internal const string Iqt_CTGen_UnexpectedVarDef = "Iqt_CTGen_UnexpectedVarDef";

		// Token: 0x040004B0 RID: 1200
		internal const string ADP_MustUseSequentialAccess = "ADP_MustUseSequentialAccess";

		// Token: 0x040004B1 RID: 1201
		internal const string ADP_ProviderDoesNotSupportCommandTrees = "ADP_ProviderDoesNotSupportCommandTrees";

		// Token: 0x040004B2 RID: 1202
		internal const string ADP_ClosedDataReaderError = "ADP_ClosedDataReaderError";

		// Token: 0x040004B3 RID: 1203
		internal const string ADP_DataReaderClosed = "ADP_DataReaderClosed";

		// Token: 0x040004B4 RID: 1204
		internal const string ADP_ImplicitlyClosedDataReaderError = "ADP_ImplicitlyClosedDataReaderError";

		// Token: 0x040004B5 RID: 1205
		internal const string ADP_NoData = "ADP_NoData";

		// Token: 0x040004B6 RID: 1206
		internal const string ADP_GetSchemaTableIsNotSupported = "ADP_GetSchemaTableIsNotSupported";

		// Token: 0x040004B7 RID: 1207
		internal const string ADP_InvalidDataReaderFieldCountForPrimitiveType = "ADP_InvalidDataReaderFieldCountForPrimitiveType";

		// Token: 0x040004B8 RID: 1208
		internal const string ADP_InvalidDataReaderMissingColumnForType = "ADP_InvalidDataReaderMissingColumnForType";

		// Token: 0x040004B9 RID: 1209
		internal const string ADP_InvalidDataReaderMissingDiscriminatorColumn = "ADP_InvalidDataReaderMissingDiscriminatorColumn";

		// Token: 0x040004BA RID: 1210
		internal const string ADP_InvalidDataReaderUnableToDetermineType = "ADP_InvalidDataReaderUnableToDetermineType";

		// Token: 0x040004BB RID: 1211
		internal const string ADP_InvalidDataReaderUnableToMaterializeNonPrimitiveType = "ADP_InvalidDataReaderUnableToMaterializeNonPrimitiveType";

		// Token: 0x040004BC RID: 1212
		internal const string ADP_KeysRequiredForJoinOverNest = "ADP_KeysRequiredForJoinOverNest";

		// Token: 0x040004BD RID: 1213
		internal const string ADP_KeysRequiredForNesting = "ADP_KeysRequiredForNesting";

		// Token: 0x040004BE RID: 1214
		internal const string ADP_NestingNotSupported = "ADP_NestingNotSupported";

		// Token: 0x040004BF RID: 1215
		internal const string ADP_NoQueryMappingView = "ADP_NoQueryMappingView";

		// Token: 0x040004C0 RID: 1216
		internal const string ADP_InternalProviderError = "ADP_InternalProviderError";

		// Token: 0x040004C1 RID: 1217
		internal const string ADP_InvalidEnumerationValue = "ADP_InvalidEnumerationValue";

		// Token: 0x040004C2 RID: 1218
		internal const string ADP_InvalidBufferSizeOrIndex = "ADP_InvalidBufferSizeOrIndex";

		// Token: 0x040004C3 RID: 1219
		internal const string ADP_InvalidDataLength = "ADP_InvalidDataLength";

		// Token: 0x040004C4 RID: 1220
		internal const string ADP_InvalidDataType = "ADP_InvalidDataType";

		// Token: 0x040004C5 RID: 1221
		internal const string ADP_InvalidDestinationBufferIndex = "ADP_InvalidDestinationBufferIndex";

		// Token: 0x040004C6 RID: 1222
		internal const string ADP_InvalidSourceBufferIndex = "ADP_InvalidSourceBufferIndex";

		// Token: 0x040004C7 RID: 1223
		internal const string ADP_NonSequentialChunkAccess = "ADP_NonSequentialChunkAccess";

		// Token: 0x040004C8 RID: 1224
		internal const string ADP_NonSequentialColumnAccess = "ADP_NonSequentialColumnAccess";

		// Token: 0x040004C9 RID: 1225
		internal const string ADP_UnknownDataTypeCode = "ADP_UnknownDataTypeCode";

		// Token: 0x040004CA RID: 1226
		internal const string DataCategory_Data = "DataCategory_Data";

		// Token: 0x040004CB RID: 1227
		internal const string DbParameter_Direction = "DbParameter_Direction";

		// Token: 0x040004CC RID: 1228
		internal const string DbParameter_Size = "DbParameter_Size";

		// Token: 0x040004CD RID: 1229
		internal const string DataCategory_Update = "DataCategory_Update";

		// Token: 0x040004CE RID: 1230
		internal const string DbParameter_SourceColumn = "DbParameter_SourceColumn";

		// Token: 0x040004CF RID: 1231
		internal const string DbParameter_SourceVersion = "DbParameter_SourceVersion";

		// Token: 0x040004D0 RID: 1232
		internal const string ADP_CollectionParameterElementIsNull = "ADP_CollectionParameterElementIsNull";

		// Token: 0x040004D1 RID: 1233
		internal const string ADP_CollectionParameterElementIsNullOrEmpty = "ADP_CollectionParameterElementIsNullOrEmpty";

		// Token: 0x040004D2 RID: 1234
		internal const string ConstantFacetSpecifiedInSchema = "ConstantFacetSpecifiedInSchema";

		// Token: 0x040004D3 RID: 1235
		internal const string DuplicateAnnotation = "DuplicateAnnotation";

		// Token: 0x040004D4 RID: 1236
		internal const string EmptyFile = "EmptyFile";

		// Token: 0x040004D5 RID: 1237
		internal const string EmptySchemaTextReader = "EmptySchemaTextReader";

		// Token: 0x040004D6 RID: 1238
		internal const string EmptyName = "EmptyName";

		// Token: 0x040004D7 RID: 1239
		internal const string InvalidName = "InvalidName";

		// Token: 0x040004D8 RID: 1240
		internal const string MissingName = "MissingName";

		// Token: 0x040004D9 RID: 1241
		internal const string UnexpectedXmlAttribute = "UnexpectedXmlAttribute";

		// Token: 0x040004DA RID: 1242
		internal const string UnexpectedXmlElement = "UnexpectedXmlElement";

		// Token: 0x040004DB RID: 1243
		internal const string TextNotAllowed = "TextNotAllowed";

		// Token: 0x040004DC RID: 1244
		internal const string UnexpectedXmlNodeType = "UnexpectedXmlNodeType";

		// Token: 0x040004DD RID: 1245
		internal const string MalformedXml = "MalformedXml";

		// Token: 0x040004DE RID: 1246
		internal const string ValueNotUnderstood = "ValueNotUnderstood";

		// Token: 0x040004DF RID: 1247
		internal const string EntityContainerAlreadyExists = "EntityContainerAlreadyExists";

		// Token: 0x040004E0 RID: 1248
		internal const string TypeNameAlreadyDefinedDuplicate = "TypeNameAlreadyDefinedDuplicate";

		// Token: 0x040004E1 RID: 1249
		internal const string PropertyNameAlreadyDefinedDuplicate = "PropertyNameAlreadyDefinedDuplicate";

		// Token: 0x040004E2 RID: 1250
		internal const string DuplicateMemberNameInExtendedEntityContainer = "DuplicateMemberNameInExtendedEntityContainer";

		// Token: 0x040004E3 RID: 1251
		internal const string DuplicateEntityContainerMemberName = "DuplicateEntityContainerMemberName";

		// Token: 0x040004E4 RID: 1252
		internal const string PropertyTypeAlreadyDefined = "PropertyTypeAlreadyDefined";

		// Token: 0x040004E5 RID: 1253
		internal const string InvalidSize = "InvalidSize";

		// Token: 0x040004E6 RID: 1254
		internal const string BadNamespaceOrAlias = "BadNamespaceOrAlias";

		// Token: 0x040004E7 RID: 1255
		internal const string InvalidBaseTypeForStructuredType = "InvalidBaseTypeForStructuredType";

		// Token: 0x040004E8 RID: 1256
		internal const string InvalidPropertyType = "InvalidPropertyType";

		// Token: 0x040004E9 RID: 1257
		internal const string NullableComplexType = "NullableComplexType";

		// Token: 0x040004EA RID: 1258
		internal const string InvalidBaseTypeForItemType = "InvalidBaseTypeForItemType";

		// Token: 0x040004EB RID: 1259
		internal const string InvalidBaseTypeForNestedType = "InvalidBaseTypeForNestedType";

		// Token: 0x040004EC RID: 1260
		internal const string DefaultNotAllowed = "DefaultNotAllowed";

		// Token: 0x040004ED RID: 1261
		internal const string FacetNotAllowed = "FacetNotAllowed";

		// Token: 0x040004EE RID: 1262
		internal const string RequiredFacetMissing = "RequiredFacetMissing";

		// Token: 0x040004EF RID: 1263
		internal const string InvalidDefaultBinaryWithNoMaxLength = "InvalidDefaultBinaryWithNoMaxLength";

		// Token: 0x040004F0 RID: 1264
		internal const string InvalidDefaultIntegral = "InvalidDefaultIntegral";

		// Token: 0x040004F1 RID: 1265
		internal const string InvalidDefaultDateTime = "InvalidDefaultDateTime";

		// Token: 0x040004F2 RID: 1266
		internal const string InvalidDefaultTime = "InvalidDefaultTime";

		// Token: 0x040004F3 RID: 1267
		internal const string InvalidDefaultDateTimeOffset = "InvalidDefaultDateTimeOffset";

		// Token: 0x040004F4 RID: 1268
		internal const string InvalidDefaultDecimal = "InvalidDefaultDecimal";

		// Token: 0x040004F5 RID: 1269
		internal const string InvalidDefaultFloatingPoint = "InvalidDefaultFloatingPoint";

		// Token: 0x040004F6 RID: 1270
		internal const string InvalidDefaultGuid = "InvalidDefaultGuid";

		// Token: 0x040004F7 RID: 1271
		internal const string InvalidDefaultBoolean = "InvalidDefaultBoolean";

		// Token: 0x040004F8 RID: 1272
		internal const string DuplicateMemberName = "DuplicateMemberName";

		// Token: 0x040004F9 RID: 1273
		internal const string GeneratorErrorSeverityError = "GeneratorErrorSeverityError";

		// Token: 0x040004FA RID: 1274
		internal const string GeneratorErrorSeverityWarning = "GeneratorErrorSeverityWarning";

		// Token: 0x040004FB RID: 1275
		internal const string GeneratorErrorSeverityUnknown = "GeneratorErrorSeverityUnknown";

		// Token: 0x040004FC RID: 1276
		internal const string SourceUriUnknown = "SourceUriUnknown";

		// Token: 0x040004FD RID: 1277
		internal const string BadPrecisionAndScale = "BadPrecisionAndScale";

		// Token: 0x040004FE RID: 1278
		internal const string InvalidNamespaceInUsing = "InvalidNamespaceInUsing";

		// Token: 0x040004FF RID: 1279
		internal const string BadNavigationPropertyRelationshipNotRelationship = "BadNavigationPropertyRelationshipNotRelationship";

		// Token: 0x04000500 RID: 1280
		internal const string BadNavigationPropertyRolesCannotBeTheSame = "BadNavigationPropertyRolesCannotBeTheSame";

		// Token: 0x04000501 RID: 1281
		internal const string BadNavigationPropertyUndefinedRole = "BadNavigationPropertyUndefinedRole";

		// Token: 0x04000502 RID: 1282
		internal const string BadNavigationPropertyBadFromRoleType = "BadNavigationPropertyBadFromRoleType";

		// Token: 0x04000503 RID: 1283
		internal const string InvalidMemberNameMatchesTypeName = "InvalidMemberNameMatchesTypeName";

		// Token: 0x04000504 RID: 1284
		internal const string InvalidKeyKeyDefinedInBaseClass = "InvalidKeyKeyDefinedInBaseClass";

		// Token: 0x04000505 RID: 1285
		internal const string InvalidKeyNullablePart = "InvalidKeyNullablePart";

		// Token: 0x04000506 RID: 1286
		internal const string InvalidKeyNoProperty = "InvalidKeyNoProperty";

		// Token: 0x04000507 RID: 1287
		internal const string KeyMissingOnEntityType = "KeyMissingOnEntityType";

		// Token: 0x04000508 RID: 1288
		internal const string InvalidDocumentationBothTextAndStructure = "InvalidDocumentationBothTextAndStructure";

		// Token: 0x04000509 RID: 1289
		internal const string ArgumentOutOfRangeExpectedPostiveNumber = "ArgumentOutOfRangeExpectedPostiveNumber";

		// Token: 0x0400050A RID: 1290
		internal const string ArgumentOutOfRange = "ArgumentOutOfRange";

		// Token: 0x0400050B RID: 1291
		internal const string UnacceptableUri = "UnacceptableUri";

		// Token: 0x0400050C RID: 1292
		internal const string UnexpectedTypeInCollection = "UnexpectedTypeInCollection";

		// Token: 0x0400050D RID: 1293
		internal const string AllElementsMustBeInSchema = "AllElementsMustBeInSchema";

		// Token: 0x0400050E RID: 1294
		internal const string AliasNameIsAlreadyDefined = "AliasNameIsAlreadyDefined";

		// Token: 0x0400050F RID: 1295
		internal const string NeedNotUseSystemNamespaceInUsing = "NeedNotUseSystemNamespaceInUsing";

		// Token: 0x04000510 RID: 1296
		internal const string CannotUseSystemNamespaceAsAlias = "CannotUseSystemNamespaceAsAlias";

		// Token: 0x04000511 RID: 1297
		internal const string EntitySetTypeHasNoKeys = "EntitySetTypeHasNoKeys";

		// Token: 0x04000512 RID: 1298
		internal const string TableAndSchemaAreMutuallyExclusiveWithDefiningQuery = "TableAndSchemaAreMutuallyExclusiveWithDefiningQuery";

		// Token: 0x04000513 RID: 1299
		internal const string UnexpectedRootElement = "UnexpectedRootElement";

		// Token: 0x04000514 RID: 1300
		internal const string UnexpectedRootElementNoNamespace = "UnexpectedRootElementNoNamespace";

		// Token: 0x04000515 RID: 1301
		internal const string ParameterNameAlreadyDefinedDuplicate = "ParameterNameAlreadyDefinedDuplicate";

		// Token: 0x04000516 RID: 1302
		internal const string FunctionWithNonPrimitiveTypeNotSupported = "FunctionWithNonPrimitiveTypeNotSupported";

		// Token: 0x04000517 RID: 1303
		internal const string FunctionWithNonEdmPrimitiveTypeNotSupported = "FunctionWithNonEdmPrimitiveTypeNotSupported";

		// Token: 0x04000518 RID: 1304
		internal const string FunctionImportWithUnsupportedReturnTypeV1 = "FunctionImportWithUnsupportedReturnTypeV1";

		// Token: 0x04000519 RID: 1305
		internal const string FunctionImportWithUnsupportedReturnTypeV1_1 = "FunctionImportWithUnsupportedReturnTypeV1_1";

		// Token: 0x0400051A RID: 1306
		internal const string FunctionImportWithUnsupportedReturnTypeV2 = "FunctionImportWithUnsupportedReturnTypeV2";

		// Token: 0x0400051B RID: 1307
		internal const string FunctionImportUnknownEntitySet = "FunctionImportUnknownEntitySet";

		// Token: 0x0400051C RID: 1308
		internal const string FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet = "FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet";

		// Token: 0x0400051D RID: 1309
		internal const string FunctionImportEntityTypeDoesNotMatchEntitySet = "FunctionImportEntityTypeDoesNotMatchEntitySet";

		// Token: 0x0400051E RID: 1310
		internal const string FunctionImportSpecifiesEntitySetButNotEntityType = "FunctionImportSpecifiesEntitySetButNotEntityType";

		// Token: 0x0400051F RID: 1311
		internal const string TVFReturnTypeRowHasNonScalarProperty = "TVFReturnTypeRowHasNonScalarProperty";

		// Token: 0x04000520 RID: 1312
		internal const string DuplicateEntitySetTable = "DuplicateEntitySetTable";

		// Token: 0x04000521 RID: 1313
		internal const string ConcurrencyRedefinedOnSubTypeOfEntitySetType = "ConcurrencyRedefinedOnSubTypeOfEntitySetType";

		// Token: 0x04000522 RID: 1314
		internal const string SimilarRelationshipEnd = "SimilarRelationshipEnd";

		// Token: 0x04000523 RID: 1315
		internal const string InvalidRelationshipEndMultiplicity = "InvalidRelationshipEndMultiplicity";

		// Token: 0x04000524 RID: 1316
		internal const string EndNameAlreadyDefinedDuplicate = "EndNameAlreadyDefinedDuplicate";

		// Token: 0x04000525 RID: 1317
		internal const string InvalidRelationshipEndType = "InvalidRelationshipEndType";

		// Token: 0x04000526 RID: 1318
		internal const string BadParameterDirection = "BadParameterDirection";

		// Token: 0x04000527 RID: 1319
		internal const string InvalidOperationMultipleEndsInAssociation = "InvalidOperationMultipleEndsInAssociation";

		// Token: 0x04000528 RID: 1320
		internal const string InvalidAction = "InvalidAction";

		// Token: 0x04000529 RID: 1321
		internal const string DuplicationOperation = "DuplicationOperation";

		// Token: 0x0400052A RID: 1322
		internal const string NotInNamespaceAlias = "NotInNamespaceAlias";

		// Token: 0x0400052B RID: 1323
		internal const string NotNamespaceQualified = "NotNamespaceQualified";

		// Token: 0x0400052C RID: 1324
		internal const string NotInNamespaceNoAlias = "NotInNamespaceNoAlias";

		// Token: 0x0400052D RID: 1325
		internal const string InvalidValueForParameterTypeSemanticsAttribute = "InvalidValueForParameterTypeSemanticsAttribute";

		// Token: 0x0400052E RID: 1326
		internal const string DuplicatePropertyNameSpecifiedInEntityKey = "DuplicatePropertyNameSpecifiedInEntityKey";

		// Token: 0x0400052F RID: 1327
		internal const string InvalidEntitySetType = "InvalidEntitySetType";

		// Token: 0x04000530 RID: 1328
		internal const string InvalidRelationshipSetType = "InvalidRelationshipSetType";

		// Token: 0x04000531 RID: 1329
		internal const string InvalidEntityContainerNameInExtends = "InvalidEntityContainerNameInExtends";

		// Token: 0x04000532 RID: 1330
		internal const string InvalidNamespaceOrAliasSpecified = "InvalidNamespaceOrAliasSpecified";

		// Token: 0x04000533 RID: 1331
		internal const string PrecisionOutOfRange = "PrecisionOutOfRange";

		// Token: 0x04000534 RID: 1332
		internal const string ScaleOutOfRange = "ScaleOutOfRange";

		// Token: 0x04000535 RID: 1333
		internal const string InvalidEntitySetNameReference = "InvalidEntitySetNameReference";

		// Token: 0x04000536 RID: 1334
		internal const string InvalidEntityEndName = "InvalidEntityEndName";

		// Token: 0x04000537 RID: 1335
		internal const string DuplicateEndName = "DuplicateEndName";

		// Token: 0x04000538 RID: 1336
		internal const string AmbiguousEntityContainerEnd = "AmbiguousEntityContainerEnd";

		// Token: 0x04000539 RID: 1337
		internal const string MissingEntityContainerEnd = "MissingEntityContainerEnd";

		// Token: 0x0400053A RID: 1338
		internal const string InvalidEndEntitySetTypeMismatch = "InvalidEndEntitySetTypeMismatch";

		// Token: 0x0400053B RID: 1339
		internal const string InferRelationshipEndFailedNoEntitySetMatch = "InferRelationshipEndFailedNoEntitySetMatch";

		// Token: 0x0400053C RID: 1340
		internal const string InferRelationshipEndAmbiguous = "InferRelationshipEndAmbiguous";

		// Token: 0x0400053D RID: 1341
		internal const string InferRelationshipEndGivesAlreadyDefinedEnd = "InferRelationshipEndGivesAlreadyDefinedEnd";

		// Token: 0x0400053E RID: 1342
		internal const string TooManyAssociationEnds = "TooManyAssociationEnds";

		// Token: 0x0400053F RID: 1343
		internal const string InvalidEndRoleInRelationshipConstraint = "InvalidEndRoleInRelationshipConstraint";

		// Token: 0x04000540 RID: 1344
		internal const string InvalidFromPropertyInRelationshipConstraint = "InvalidFromPropertyInRelationshipConstraint";

		// Token: 0x04000541 RID: 1345
		internal const string InvalidToPropertyInRelationshipConstraint = "InvalidToPropertyInRelationshipConstraint";

		// Token: 0x04000542 RID: 1346
		internal const string InvalidPropertyInRelationshipConstraint = "InvalidPropertyInRelationshipConstraint";

		// Token: 0x04000543 RID: 1347
		internal const string TypeMismatchRelationshipConstaint = "TypeMismatchRelationshipConstaint";

		// Token: 0x04000544 RID: 1348
		internal const string InvalidMultiplicityFromRoleUpperBoundMustBeOne = "InvalidMultiplicityFromRoleUpperBoundMustBeOne";

		// Token: 0x04000545 RID: 1349
		internal const string InvalidMultiplicityFromRoleToPropertyNonNullableV1 = "InvalidMultiplicityFromRoleToPropertyNonNullableV1";

		// Token: 0x04000546 RID: 1350
		internal const string InvalidMultiplicityFromRoleToPropertyNonNullableV2 = "InvalidMultiplicityFromRoleToPropertyNonNullableV2";

		// Token: 0x04000547 RID: 1351
		internal const string InvalidMultiplicityFromRoleToPropertyNullableV1 = "InvalidMultiplicityFromRoleToPropertyNullableV1";

		// Token: 0x04000548 RID: 1352
		internal const string InvalidMultiplicityToRoleLowerBoundMustBeZero = "InvalidMultiplicityToRoleLowerBoundMustBeZero";

		// Token: 0x04000549 RID: 1353
		internal const string InvalidMultiplicityToRoleUpperBoundMustBeOne = "InvalidMultiplicityToRoleUpperBoundMustBeOne";

		// Token: 0x0400054A RID: 1354
		internal const string InvalidMultiplicityToRoleUpperBoundMustBeMany = "InvalidMultiplicityToRoleUpperBoundMustBeMany";

		// Token: 0x0400054B RID: 1355
		internal const string MismatchNumberOfPropertiesinRelationshipConstraint = "MismatchNumberOfPropertiesinRelationshipConstraint";

		// Token: 0x0400054C RID: 1356
		internal const string MissingConstraintOnRelationshipType = "MissingConstraintOnRelationshipType";

		// Token: 0x0400054D RID: 1357
		internal const string SameRoleReferredInReferentialConstraint = "SameRoleReferredInReferentialConstraint";

		// Token: 0x0400054E RID: 1358
		internal const string InvalidPrimitiveTypeKind = "InvalidPrimitiveTypeKind";

		// Token: 0x0400054F RID: 1359
		internal const string EntityKeyMustBeScalar = "EntityKeyMustBeScalar";

		// Token: 0x04000550 RID: 1360
		internal const string EntityKeyTypeCurrentlyNotSupportedInSSDL = "EntityKeyTypeCurrentlyNotSupportedInSSDL";

		// Token: 0x04000551 RID: 1361
		internal const string EntityKeyTypeCurrentlyNotSupported = "EntityKeyTypeCurrentlyNotSupported";

		// Token: 0x04000552 RID: 1362
		internal const string MissingFacetDescription = "MissingFacetDescription";

		// Token: 0x04000553 RID: 1363
		internal const string EndWithManyMultiplicityCannotHaveOperationsSpecified = "EndWithManyMultiplicityCannotHaveOperationsSpecified";

		// Token: 0x04000554 RID: 1364
		internal const string EndWithoutMultiplicity = "EndWithoutMultiplicity";

		// Token: 0x04000555 RID: 1365
		internal const string EntityContainerCannotExtendItself = "EntityContainerCannotExtendItself";

		// Token: 0x04000556 RID: 1366
		internal const string ComposableFunctionMustDeclareReturnType = "ComposableFunctionMustDeclareReturnType";

		// Token: 0x04000557 RID: 1367
		internal const string NonComposableFunctionMustNotDeclareReturnType = "NonComposableFunctionMustNotDeclareReturnType";

		// Token: 0x04000558 RID: 1368
		internal const string CommandTextFunctionsNotComposable = "CommandTextFunctionsNotComposable";

		// Token: 0x04000559 RID: 1369
		internal const string CommandTextFunctionsCannotDeclareStoreFunctionName = "CommandTextFunctionsCannotDeclareStoreFunctionName";

		// Token: 0x0400055A RID: 1370
		internal const string NonComposableFunctionHasDisallowedAttribute = "NonComposableFunctionHasDisallowedAttribute";

		// Token: 0x0400055B RID: 1371
		internal const string EmptyDefiningQuery = "EmptyDefiningQuery";

		// Token: 0x0400055C RID: 1372
		internal const string EmptyCommandText = "EmptyCommandText";

		// Token: 0x0400055D RID: 1373
		internal const string AmbiguousFunctionOverload = "AmbiguousFunctionOverload";

		// Token: 0x0400055E RID: 1374
		internal const string AmbiguousFunctionAndType = "AmbiguousFunctionAndType";

		// Token: 0x0400055F RID: 1375
		internal const string CycleInTypeHierarchy = "CycleInTypeHierarchy";

		// Token: 0x04000560 RID: 1376
		internal const string IncorrectProviderManifest = "IncorrectProviderManifest";

		// Token: 0x04000561 RID: 1377
		internal const string ComplexTypeAsReturnTypeAndDefinedEntitySet = "ComplexTypeAsReturnTypeAndDefinedEntitySet";

		// Token: 0x04000562 RID: 1378
		internal const string ComplexTypeAsReturnTypeAndNestedComplexProperty = "ComplexTypeAsReturnTypeAndNestedComplexProperty";

		// Token: 0x04000563 RID: 1379
		internal const string FacetsOnNonScalarType = "FacetsOnNonScalarType";

		// Token: 0x04000564 RID: 1380
		internal const string FacetDeclarationRequiresTypeAttribute = "FacetDeclarationRequiresTypeAttribute";

		// Token: 0x04000565 RID: 1381
		internal const string TypeMustBeDeclared = "TypeMustBeDeclared";

		// Token: 0x04000566 RID: 1382
		internal const string RowTypeWithoutProperty = "RowTypeWithoutProperty";

		// Token: 0x04000567 RID: 1383
		internal const string TypeDeclaredAsAttributeAndElement = "TypeDeclaredAsAttributeAndElement";

		// Token: 0x04000568 RID: 1384
		internal const string ReferenceToNonEntityType = "ReferenceToNonEntityType";

		// Token: 0x04000569 RID: 1385
		internal const string NoCodeGenNamespaceInStructuralAnnotation = "NoCodeGenNamespaceInStructuralAnnotation";

		// Token: 0x0400056A RID: 1386
		internal const string CannotLoadDifferentVersionOfSchemaInTheSameItemCollection = "CannotLoadDifferentVersionOfSchemaInTheSameItemCollection";

		// Token: 0x0400056B RID: 1387
		internal const string ObjectQuery_QueryBuilder_InvalidProjectionList = "ObjectQuery_QueryBuilder_InvalidProjectionList";

		// Token: 0x0400056C RID: 1388
		internal const string ObjectQuery_QueryBuilder_InvalidSortKeyList = "ObjectQuery_QueryBuilder_InvalidSortKeyList";

		// Token: 0x0400056D RID: 1389
		internal const string ObjectQuery_QueryBuilder_InvalidGroupKeyList = "ObjectQuery_QueryBuilder_InvalidGroupKeyList";

		// Token: 0x0400056E RID: 1390
		internal const string ObjectQuery_QueryBuilder_InvalidSkipCount = "ObjectQuery_QueryBuilder_InvalidSkipCount";

		// Token: 0x0400056F RID: 1391
		internal const string ObjectQuery_QueryBuilder_InvalidTopCount = "ObjectQuery_QueryBuilder_InvalidTopCount";

		// Token: 0x04000570 RID: 1392
		internal const string ObjectQuery_QueryBuilder_InvalidFilterPredicate = "ObjectQuery_QueryBuilder_InvalidFilterPredicate";

		// Token: 0x04000571 RID: 1393
		internal const string ObjectQuery_QueryBuilder_InvalidResultType = "ObjectQuery_QueryBuilder_InvalidResultType";

		// Token: 0x04000572 RID: 1394
		internal const string ObjectQuery_QueryBuilder_InvalidQueryArgument = "ObjectQuery_QueryBuilder_InvalidQueryArgument";

		// Token: 0x04000573 RID: 1395
		internal const string ObjectQuery_QueryBuilder_NotSupportedLinqSource = "ObjectQuery_QueryBuilder_NotSupportedLinqSource";

		// Token: 0x04000574 RID: 1396
		internal const string ObjectQuery_InvalidEmptyQuery = "ObjectQuery_InvalidEmptyQuery";

		// Token: 0x04000575 RID: 1397
		internal const string ObjectQuery_InvalidConnection = "ObjectQuery_InvalidConnection";

		// Token: 0x04000576 RID: 1398
		internal const string ObjectQuery_InvalidQueryName = "ObjectQuery_InvalidQueryName";

		// Token: 0x04000577 RID: 1399
		internal const string ObjectQuery_UnableToMapResultType = "ObjectQuery_UnableToMapResultType";

		// Token: 0x04000578 RID: 1400
		internal const string ObjectQuery_UnableToMaterializeArray = "ObjectQuery_UnableToMaterializeArray";

		// Token: 0x04000579 RID: 1401
		internal const string ObjectQuery_UnableToMaterializeArbitaryProjectionType = "ObjectQuery_UnableToMaterializeArbitaryProjectionType";

		// Token: 0x0400057A RID: 1402
		internal const string ObjectParameter_InvalidParameterName = "ObjectParameter_InvalidParameterName";

		// Token: 0x0400057B RID: 1403
		internal const string ObjectParameter_InvalidParameterType = "ObjectParameter_InvalidParameterType";

		// Token: 0x0400057C RID: 1404
		internal const string ObjectParameterCollection_ParameterNameNotFound = "ObjectParameterCollection_ParameterNameNotFound";

		// Token: 0x0400057D RID: 1405
		internal const string ObjectParameterCollection_ParameterAlreadyExists = "ObjectParameterCollection_ParameterAlreadyExists";

		// Token: 0x0400057E RID: 1406
		internal const string ObjectParameterCollection_DuplicateParameterName = "ObjectParameterCollection_DuplicateParameterName";

		// Token: 0x0400057F RID: 1407
		internal const string ObjectParameterCollection_ParametersLocked = "ObjectParameterCollection_ParametersLocked";

		// Token: 0x04000580 RID: 1408
		internal const string ProviderReturnedNullForGetDbInformation = "ProviderReturnedNullForGetDbInformation";

		// Token: 0x04000581 RID: 1409
		internal const string ProviderReturnedNullForCreateCommandDefinition = "ProviderReturnedNullForCreateCommandDefinition";

		// Token: 0x04000582 RID: 1410
		internal const string ProviderDidNotReturnAProviderManifest = "ProviderDidNotReturnAProviderManifest";

		// Token: 0x04000583 RID: 1411
		internal const string ProviderDidNotReturnAProviderManifestToken = "ProviderDidNotReturnAProviderManifestToken";

		// Token: 0x04000584 RID: 1412
		internal const string ProviderDoesNotSupportType = "ProviderDoesNotSupportType";

		// Token: 0x04000585 RID: 1413
		internal const string NoStoreTypeForEdmType = "NoStoreTypeForEdmType";

		// Token: 0x04000586 RID: 1414
		internal const string ProviderRequiresStoreCommandTree = "ProviderRequiresStoreCommandTree";

		// Token: 0x04000587 RID: 1415
		internal const string ProviderShouldOverrideEscapeLikeArgument = "ProviderShouldOverrideEscapeLikeArgument";

		// Token: 0x04000588 RID: 1416
		internal const string ProviderEscapeLikeArgumentReturnedNull = "ProviderEscapeLikeArgumentReturnedNull";

		// Token: 0x04000589 RID: 1417
		internal const string ProviderDidNotCreateACommandDefinition = "ProviderDidNotCreateACommandDefinition";

		// Token: 0x0400058A RID: 1418
		internal const string ProviderDoesNotSupportCreateDatabaseScript = "ProviderDoesNotSupportCreateDatabaseScript";

		// Token: 0x0400058B RID: 1419
		internal const string ProviderDoesNotSupportCreateDatabase = "ProviderDoesNotSupportCreateDatabase";

		// Token: 0x0400058C RID: 1420
		internal const string ProviderDoesNotSupportDatabaseExists = "ProviderDoesNotSupportDatabaseExists";

		// Token: 0x0400058D RID: 1421
		internal const string ProviderDoesNotSupportDeleteDatabase = "ProviderDoesNotSupportDeleteDatabase";

		// Token: 0x0400058E RID: 1422
		internal const string EntityConnectionString_Name = "EntityConnectionString_Name";

		// Token: 0x0400058F RID: 1423
		internal const string EntityConnectionString_Provider = "EntityConnectionString_Provider";

		// Token: 0x04000590 RID: 1424
		internal const string EntityConnectionString_Metadata = "EntityConnectionString_Metadata";

		// Token: 0x04000591 RID: 1425
		internal const string EntityConnectionString_ProviderConnectionString = "EntityConnectionString_ProviderConnectionString";

		// Token: 0x04000592 RID: 1426
		internal const string EntityDataCategory_Context = "EntityDataCategory_Context";

		// Token: 0x04000593 RID: 1427
		internal const string EntityDataCategory_NamedConnectionString = "EntityDataCategory_NamedConnectionString";

		// Token: 0x04000594 RID: 1428
		internal const string EntityDataCategory_Source = "EntityDataCategory_Source";

		// Token: 0x04000595 RID: 1429
		internal const string ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection = "ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection";

		// Token: 0x04000596 RID: 1430
		internal const string ObjectQuery_Span_NoNavProp = "ObjectQuery_Span_NoNavProp";

		// Token: 0x04000597 RID: 1431
		internal const string ObjectQuery_Span_SpanPathSyntaxError = "ObjectQuery_Span_SpanPathSyntaxError";

		// Token: 0x04000598 RID: 1432
		internal const string EntityProxyTypeInfo_ProxyHasWrongWrapper = "EntityProxyTypeInfo_ProxyHasWrongWrapper";

		// Token: 0x04000599 RID: 1433
		internal const string EntityProxyTypeInfo_CannotSetEntityCollectionProperty = "EntityProxyTypeInfo_CannotSetEntityCollectionProperty";

		// Token: 0x0400059A RID: 1434
		internal const string EntityProxyTypeInfo_ProxyMetadataIsUnavailable = "EntityProxyTypeInfo_ProxyMetadataIsUnavailable";

		// Token: 0x0400059B RID: 1435
		internal const string EntityProxyTypeInfo_DuplicateOSpaceType = "EntityProxyTypeInfo_DuplicateOSpaceType";

		// Token: 0x0400059C RID: 1436
		private static EntityRes loader;

		// Token: 0x0400059D RID: 1437
		private ResourceManager resources;
	}
}
