using System;
using System.Globalization;

namespace System.Data.Entity
{
	// Token: 0x02000015 RID: 21
	internal static class Strings
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000032B5 File Offset: 0x000014B5
		internal static string ReturnTypeDeclaredAsAttributeAndElement
		{
			get
			{
				return StringRes.ReturnTypeDeclaredAsAttributeAndElement;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000032BC File Offset: 0x000014BC
		internal static string ReturnTypeMustBeDeclared(object p0)
		{
			return string.Format(CultureInfo.InvariantCulture, StringRes.ReturnTypeMustBeDeclared, p0);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000032CE File Offset: 0x000014CE
		internal static string EntityKey_DataRecordMustBeEntity
		{
			get
			{
				return EntityRes.GetString("EntityKey_DataRecordMustBeEntity");
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000032DA File Offset: 0x000014DA
		internal static string EntityKey_EntitySetDoesNotMatch(object p0)
		{
			return EntityRes.GetString("EntityKey_EntitySetDoesNotMatch", new object[] { p0 });
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032F0 File Offset: 0x000014F0
		internal static string EntityKey_EntityTypesDoNotMatch(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_EntityTypesDoNotMatch", new object[] { p0, p1 });
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000330A File Offset: 0x0000150A
		internal static string EntityKey_IncorrectNumberOfKeyValuePairs(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKey_IncorrectNumberOfKeyValuePairs", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003328 File Offset: 0x00001528
		internal static string EntityKey_IncorrectValueType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKey_IncorrectValueType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003346 File Offset: 0x00001546
		internal static string EntityKey_MissingKeyValue(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_MissingKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003360 File Offset: 0x00001560
		internal static string EntityKey_NoNullsAllowedInKeyValuePairs
		{
			get
			{
				return EntityRes.GetString("EntityKey_NoNullsAllowedInKeyValuePairs");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000336C File Offset: 0x0000156C
		internal static string EntityKey_UnexpectedNull
		{
			get
			{
				return EntityRes.GetString("EntityKey_UnexpectedNull");
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003378 File Offset: 0x00001578
		internal static string EntityKey_DoesntMatchKeyOnEntity(object p0)
		{
			return EntityRes.GetString("EntityKey_DoesntMatchKeyOnEntity", new object[] { p0 });
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000338E File Offset: 0x0000158E
		internal static string EntityKey_EntityKeyMustHaveValues
		{
			get
			{
				return EntityRes.GetString("EntityKey_EntityKeyMustHaveValues");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000339A File Offset: 0x0000159A
		internal static string EntityKey_InvalidQualifiedEntitySetName
		{
			get
			{
				return EntityRes.GetString("EntityKey_InvalidQualifiedEntitySetName");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000033A6 File Offset: 0x000015A6
		internal static string EntityKey_MissingEntitySetName
		{
			get
			{
				return EntityRes.GetString("EntityKey_MissingEntitySetName");
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000033B2 File Offset: 0x000015B2
		internal static string EntityKey_InvalidName(object p0)
		{
			return EntityRes.GetString("EntityKey_InvalidName", new object[] { p0 });
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000033C8 File Offset: 0x000015C8
		internal static string EntityKey_CannotChangeKey
		{
			get
			{
				return EntityRes.GetString("EntityKey_CannotChangeKey");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000033D4 File Offset: 0x000015D4
		internal static string EntityTypesDoNotAgree
		{
			get
			{
				return EntityRes.GetString("EntityTypesDoNotAgree");
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000033E0 File Offset: 0x000015E0
		internal static string EntityKey_NullKeyValue(object p0, object p1)
		{
			return EntityRes.GetString("EntityKey_NullKeyValue", new object[] { p0, p1 });
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000033FA File Offset: 0x000015FA
		internal static string EdmMembersDefiningTypeDoNotAgreeWithMetadataType
		{
			get
			{
				return EntityRes.GetString("EdmMembersDefiningTypeDoNotAgreeWithMetadataType");
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003406 File Offset: 0x00001606
		internal static string InvalidStringArgument(object p0)
		{
			return EntityRes.GetString("InvalidStringArgument", new object[] { p0 });
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000341C File Offset: 0x0000161C
		internal static string EntityClient_ConnectionStringMissingInfo(object p0)
		{
			return EntityRes.GetString("EntityClient_ConnectionStringMissingInfo", new object[] { p0 });
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003432 File Offset: 0x00001632
		internal static string EntityClient_ValueNotString
		{
			get
			{
				return EntityRes.GetString("EntityClient_ValueNotString");
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000343E File Offset: 0x0000163E
		internal static string EntityClient_KeywordNotSupported(object p0)
		{
			return EntityRes.GetString("EntityClient_KeywordNotSupported", new object[] { p0 });
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003454 File Offset: 0x00001654
		internal static string EntityClient_NoCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoCommandText");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003460 File Offset: 0x00001660
		internal static string EntityClient_ConnectionStringNeededBeforeOperation
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStringNeededBeforeOperation");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000346C File Offset: 0x0000166C
		internal static string EntityClient_CannotReopenConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotReopenConnection");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003478 File Offset: 0x00001678
		internal static string EntityClient_ConnectionNotOpen
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionNotOpen");
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003484 File Offset: 0x00001684
		internal static string EntityClient_DuplicateParameterNames(object p0)
		{
			return EntityRes.GetString("EntityClient_DuplicateParameterNames", new object[] { p0 });
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000349A File Offset: 0x0000169A
		internal static string EntityClient_NoConnectionForCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoConnectionForCommand");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000034A6 File Offset: 0x000016A6
		internal static string EntityClient_NoConnectionForAdapter
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoConnectionForAdapter");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000034B2 File Offset: 0x000016B2
		internal static string EntityClient_ClosedConnectionForUpdate
		{
			get
			{
				return EntityRes.GetString("EntityClient_ClosedConnectionForUpdate");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000034BE File Offset: 0x000016BE
		internal static string EntityClient_InvalidNamedConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidNamedConnection");
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000034CA File Offset: 0x000016CA
		internal static string EntityClient_NestedNamedConnection(object p0)
		{
			return EntityRes.GetString("EntityClient_NestedNamedConnection", new object[] { p0 });
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000034E0 File Offset: 0x000016E0
		internal static string EntityClient_InvalidStoreProvider
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidStoreProvider");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000034EC File Offset: 0x000016EC
		internal static string EntityClient_DataReaderIsStillOpen
		{
			get
			{
				return EntityRes.GetString("EntityClient_DataReaderIsStillOpen");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000034F8 File Offset: 0x000016F8
		internal static string EntityClient_SettingsCannotBeChangedOnOpenConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_SettingsCannotBeChangedOnOpenConnection");
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003504 File Offset: 0x00001704
		internal static string EntityClient_ExecutingOnClosedConnection(object p0)
		{
			return EntityRes.GetString("EntityClient_ExecutingOnClosedConnection", new object[] { p0 });
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000351A File Offset: 0x0000171A
		internal static string EntityClient_ConnectionStateClosed
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStateClosed");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003526 File Offset: 0x00001726
		internal static string EntityClient_ConnectionStateBroken
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionStateBroken");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003532 File Offset: 0x00001732
		internal static string EntityClient_CannotCloneStoreProvider
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotCloneStoreProvider");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000353E File Offset: 0x0000173E
		internal static string EntityClient_UnsupportedCommandType
		{
			get
			{
				return EntityRes.GetString("EntityClient_UnsupportedCommandType");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000354A File Offset: 0x0000174A
		internal static string EntityClient_ErrorInClosingConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_ErrorInClosingConnection");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003556 File Offset: 0x00001756
		internal static string EntityClient_ErrorInBeginningTransaction
		{
			get
			{
				return EntityRes.GetString("EntityClient_ErrorInBeginningTransaction");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003562 File Offset: 0x00001762
		internal static string EntityClient_ExtraParametersWithNamedConnection
		{
			get
			{
				return EntityRes.GetString("EntityClient_ExtraParametersWithNamedConnection");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000356E File Offset: 0x0000176E
		internal static string EntityClient_CommandDefinitionPreparationFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandDefinitionPreparationFailed");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000357A File Offset: 0x0000177A
		internal static string EntityClient_CommandDefinitionExecutionFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandDefinitionExecutionFailed");
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003586 File Offset: 0x00001786
		internal static string EntityClient_CommandExecutionFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandExecutionFailed");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003592 File Offset: 0x00001792
		internal static string EntityClient_StoreReaderFailed
		{
			get
			{
				return EntityRes.GetString("EntityClient_StoreReaderFailed");
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000359E File Offset: 0x0000179E
		internal static string EntityClient_FailedToGetInformation(object p0)
		{
			return EntityRes.GetString("EntityClient_FailedToGetInformation", new object[] { p0 });
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000035B4 File Offset: 0x000017B4
		internal static string EntityClient_TooFewColumns
		{
			get
			{
				return EntityRes.GetString("EntityClient_TooFewColumns");
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000035C0 File Offset: 0x000017C0
		internal static string EntityClient_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidParameterName", new object[] { p0 });
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000035D6 File Offset: 0x000017D6
		internal static string EntityClient_EmptyParameterName
		{
			get
			{
				return EntityRes.GetString("EntityClient_EmptyParameterName");
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000035E2 File Offset: 0x000017E2
		internal static string EntityClient_ReturnedNullOnProviderMethod(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_ReturnedNullOnProviderMethod", new object[] { p0, p1 });
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000035FC File Offset: 0x000017FC
		internal static string EntityClient_CannotDeduceDbType
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotDeduceDbType");
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003608 File Offset: 0x00001808
		internal static string EntityClient_InvalidParameterDirection(object p0)
		{
			return EntityRes.GetString("EntityClient_InvalidParameterDirection", new object[] { p0 });
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000361E File Offset: 0x0000181E
		internal static string EntityClient_UnknownParameterType(object p0)
		{
			return EntityRes.GetString("EntityClient_UnknownParameterType", new object[] { p0 });
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003634 File Offset: 0x00001834
		internal static string EntityClient_UnsupportedDbType(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_UnsupportedDbType", new object[] { p0, p1 });
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000364E File Offset: 0x0000184E
		internal static string EntityClient_DoesNotImplementIServiceProvider(object p0)
		{
			return EntityRes.GetString("EntityClient_DoesNotImplementIServiceProvider", new object[] { p0 });
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003664 File Offset: 0x00001864
		internal static string EntityClient_IncompatibleNavigationPropertyResult(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_IncompatibleNavigationPropertyResult", new object[] { p0, p1 });
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000367E File Offset: 0x0000187E
		internal static string EntityClient_TransactionAlreadyStarted
		{
			get
			{
				return EntityRes.GetString("EntityClient_TransactionAlreadyStarted");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000368A File Offset: 0x0000188A
		internal static string EntityClient_InvalidTransactionForCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidTransactionForCommand");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003696 File Offset: 0x00001896
		internal static string EntityClient_NoStoreConnectionForUpdate
		{
			get
			{
				return EntityRes.GetString("EntityClient_NoStoreConnectionForUpdate");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000036A2 File Offset: 0x000018A2
		internal static string EntityClient_CommandTreeMetadataIncompatible
		{
			get
			{
				return EntityRes.GetString("EntityClient_CommandTreeMetadataIncompatible");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000036AE File Offset: 0x000018AE
		internal static string EntityClient_ProviderGeneralError
		{
			get
			{
				return EntityRes.GetString("EntityClient_ProviderGeneralError");
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000036BA File Offset: 0x000018BA
		internal static string EntityClient_ProviderSpecificError(object p0)
		{
			return EntityRes.GetString("EntityClient_ProviderSpecificError", new object[] { p0 });
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000036D0 File Offset: 0x000018D0
		internal static string EntityClient_FunctionImportEmptyCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_FunctionImportEmptyCommandText");
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000036DC File Offset: 0x000018DC
		internal static string EntityClient_UnableToFindFunctionImportContainer(object p0)
		{
			return EntityRes.GetString("EntityClient_UnableToFindFunctionImportContainer", new object[] { p0 });
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000036F2 File Offset: 0x000018F2
		internal static string EntityClient_UnableToFindFunctionImport(object p0, object p1)
		{
			return EntityRes.GetString("EntityClient_UnableToFindFunctionImport", new object[] { p0, p1 });
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000370C File Offset: 0x0000190C
		internal static string EntityClient_UnmappedFunctionImport
		{
			get
			{
				return EntityRes.GetString("EntityClient_UnmappedFunctionImport");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003718 File Offset: 0x00001918
		internal static string EntityClient_InvalidStoredProcedureCommandText
		{
			get
			{
				return EntityRes.GetString("EntityClient_InvalidStoredProcedureCommandText");
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003724 File Offset: 0x00001924
		internal static string EntityClient_ItemCollectionsNotRegisteredInWorkspace(object p0)
		{
			return EntityRes.GetString("EntityClient_ItemCollectionsNotRegisteredInWorkspace", new object[] { p0 });
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000373A File Offset: 0x0000193A
		internal static string EntityClient_ConnectionMustBeClosed
		{
			get
			{
				return EntityRes.GetString("EntityClient_ConnectionMustBeClosed");
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003746 File Offset: 0x00001946
		internal static string EntityClient_DbConnectionHasNoProvider(object p0)
		{
			return EntityRes.GetString("EntityClient_DbConnectionHasNoProvider", new object[] { p0 });
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000375C File Offset: 0x0000195C
		internal static string EntityClient_RequiresNonStoreCommandTree
		{
			get
			{
				return EntityRes.GetString("EntityClient_RequiresNonStoreCommandTree");
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003768 File Offset: 0x00001968
		internal static string EntityClient_CannotReprepareCommandDefinitionBasedCommand
		{
			get
			{
				return EntityRes.GetString("EntityClient_CannotReprepareCommandDefinitionBasedCommand");
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003774 File Offset: 0x00001974
		internal static string ELinq_ExpressionMustBeIQueryable
		{
			get
			{
				return EntityRes.GetString("ELinq_ExpressionMustBeIQueryable");
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003780 File Offset: 0x00001980
		internal static string ELinq_UnsupportedExpressionType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedExpressionType", new object[] { p0 });
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003796 File Offset: 0x00001996
		internal static string ELinq_UnsupportedUseOfContextParameter(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedUseOfContextParameter", new object[] { p0 });
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000037AC File Offset: 0x000019AC
		internal static string ELinq_UnboundParameterExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnboundParameterExpression", new object[] { p0 });
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000037C2 File Offset: 0x000019C2
		internal static string ELinq_UnsupportedConstructor
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedConstructor");
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000037CE File Offset: 0x000019CE
		internal static string ELinq_UnsupportedInitializers
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedInitializers");
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000037DA File Offset: 0x000019DA
		internal static string ELinq_UnsupportedBinding
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedBinding");
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000037E6 File Offset: 0x000019E6
		internal static string ELinq_UnsupportedMethod(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedMethod", new object[] { p0 });
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000037FC File Offset: 0x000019FC
		internal static string ELinq_UnsupportedMethodSuggestedAlternative(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedMethodSuggestedAlternative", new object[] { p0, p1 });
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003816 File Offset: 0x00001A16
		internal static string ELinq_ThenByDoesNotFollowOrderBy
		{
			get
			{
				return EntityRes.GetString("ELinq_ThenByDoesNotFollowOrderBy");
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003822 File Offset: 0x00001A22
		internal static string ELinq_UnrecognizedMember(object p0)
		{
			return EntityRes.GetString("ELinq_UnrecognizedMember", new object[] { p0 });
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003838 File Offset: 0x00001A38
		internal static string ELinq_UnresolvableFunctionForMethod(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethod", new object[] { p0, p1 });
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003852 File Offset: 0x00001A52
		internal static string ELinq_UnresolvableFunctionForMethodAmbiguousMatch(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethodAmbiguousMatch", new object[] { p0, p1 });
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000386C File Offset: 0x00001A6C
		internal static string ELinq_UnresolvableFunctionForMethodNotFound(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMethodNotFound", new object[] { p0, p1 });
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003886 File Offset: 0x00001A86
		internal static string ELinq_UnresolvableFunctionForMember(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForMember", new object[] { p0, p1 });
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000038A0 File Offset: 0x00001AA0
		internal static string ELinq_UnresolvableStoreFunctionForMember(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnresolvableStoreFunctionForMember", new object[] { p0, p1 });
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000038BA File Offset: 0x00001ABA
		internal static string ELinq_UnresolvableFunctionForExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnresolvableFunctionForExpression", new object[] { p0 });
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000038D0 File Offset: 0x00001AD0
		internal static string ELinq_UnresolvableStoreFunctionForExpression(object p0)
		{
			return EntityRes.GetString("ELinq_UnresolvableStoreFunctionForExpression", new object[] { p0 });
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000038E6 File Offset: 0x00001AE6
		internal static string ELinq_UnsupportedType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedType", new object[] { p0 });
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000038FC File Offset: 0x00001AFC
		internal static string ELinq_UnsupportedNullConstant(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedNullConstant", new object[] { p0, p1 });
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003916 File Offset: 0x00001B16
		internal static string ELinq_UnsupportedConstant(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedConstant", new object[] { p0, p1 });
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003930 File Offset: 0x00001B30
		internal static string ELinq_UnsupportedCast(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedCast", new object[] { p0, p1 });
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000394A File Offset: 0x00001B4A
		internal static string ELinq_UnsupportedIsOrAs(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_UnsupportedIsOrAs", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003968 File Offset: 0x00001B68
		internal static string ELinq_UnsupportedQueryableMethod
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedQueryableMethod");
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003974 File Offset: 0x00001B74
		internal static string ELinq_InvalidOfTypeResult(object p0)
		{
			return EntityRes.GetString("ELinq_InvalidOfTypeResult", new object[] { p0 });
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000398A File Offset: 0x00001B8A
		internal static string ELinq_UnsupportedNominalType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedNominalType", new object[] { p0 });
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000039A0 File Offset: 0x00001BA0
		internal static string ELinq_UnsupportedEnumerableType(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedEnumerableType", new object[] { p0 });
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000039B6 File Offset: 0x00001BB6
		internal static string ELinq_UnsupportedHeterogeneousInitializers(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedHeterogeneousInitializers", new object[] { p0 });
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000039CC File Offset: 0x00001BCC
		internal static string ELinq_UnsupportedDifferentContexts
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedDifferentContexts");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000039D8 File Offset: 0x00001BD8
		internal static string ELinq_UnsupportedCastToDecimal
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedCastToDecimal");
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000039E4 File Offset: 0x00001BE4
		internal static string ELinq_UnsupportedKeySelector(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedKeySelector", new object[] { p0 });
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000039FA File Offset: 0x00001BFA
		internal static string ELinq_CreateOrderedEnumerableNotSupported
		{
			get
			{
				return EntityRes.GetString("ELinq_CreateOrderedEnumerableNotSupported");
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003A06 File Offset: 0x00001C06
		internal static string ELinq_UnsupportedPassthrough(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedPassthrough", new object[] { p0, p1 });
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003A20 File Offset: 0x00001C20
		internal static string ELinq_UnexpectedTypeForNavigationProperty(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ELinq_UnexpectedTypeForNavigationProperty", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003A42 File Offset: 0x00001C42
		internal static string ELinq_SkipWithoutOrder
		{
			get
			{
				return EntityRes.GetString("ELinq_SkipWithoutOrder");
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003A4E File Offset: 0x00001C4E
		internal static string ELinq_PropertyIndexNotSupported
		{
			get
			{
				return EntityRes.GetString("ELinq_PropertyIndexNotSupported");
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003A5A File Offset: 0x00001C5A
		internal static string ELinq_NotPropertyOrField(object p0)
		{
			return EntityRes.GetString("ELinq_NotPropertyOrField", new object[] { p0 });
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003A70 File Offset: 0x00001C70
		internal static string ELinq_UnsupportedStringRemoveCase(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedStringRemoveCase", new object[] { p0, p1 });
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003A8A File Offset: 0x00001C8A
		internal static string ELinq_UnsupportedTrimStartTrimEndCase(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedTrimStartTrimEndCase", new object[] { p0 });
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003AA0 File Offset: 0x00001CA0
		internal static string ELinq_UnsupportedVBDatePartNonConstantInterval(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedVBDatePartNonConstantInterval", new object[] { p0, p1 });
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003ABA File Offset: 0x00001CBA
		internal static string ELinq_UnsupportedVBDatePartInvalidInterval(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_UnsupportedVBDatePartInvalidInterval", new object[] { p0, p1, p2 });
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003AD8 File Offset: 0x00001CD8
		internal static string ELinq_UnsupportedAsUnicodeAndAsNonUnicode(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedAsUnicodeAndAsNonUnicode", new object[] { p0 });
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003AEE File Offset: 0x00001CEE
		internal static string ELinq_UnsupportedComparison(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedComparison", new object[] { p0, p1 });
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003B08 File Offset: 0x00001D08
		internal static string ELinq_UnsupportedRefComparison(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedRefComparison", new object[] { p0, p1 });
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003B22 File Offset: 0x00001D22
		internal static string ELinq_UnsupportedRowComparison(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowComparison", new object[] { p0, p1 });
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003B3C File Offset: 0x00001D3C
		internal static string ELinq_UnsupportedRowMemberComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowMemberComparison", new object[] { p0 });
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003B52 File Offset: 0x00001D52
		internal static string ELinq_UnsupportedRowTypeComparison(object p0)
		{
			return EntityRes.GetString("ELinq_UnsupportedRowTypeComparison", new object[] { p0 });
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003B68 File Offset: 0x00001D68
		internal static string ELinq_PrimitiveTypesSample
		{
			get
			{
				return EntityRes.GetString("ELinq_PrimitiveTypesSample");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003B74 File Offset: 0x00001D74
		internal static string ELinq_AnonymousType
		{
			get
			{
				return EntityRes.GetString("ELinq_AnonymousType");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00003B80 File Offset: 0x00001D80
		internal static string ELinq_ClosureType
		{
			get
			{
				return EntityRes.GetString("ELinq_ClosureType");
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003B8C File Offset: 0x00001D8C
		internal static string ELinq_UnhandledExpressionType(object p0)
		{
			return EntityRes.GetString("ELinq_UnhandledExpressionType", new object[] { p0 });
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003BA2 File Offset: 0x00001DA2
		internal static string ELinq_UnhandledBindingType(object p0)
		{
			return EntityRes.GetString("ELinq_UnhandledBindingType", new object[] { p0 });
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003BB8 File Offset: 0x00001DB8
		internal static string ELinq_UnsupportedNestedFirst
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedNestedFirst");
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003BC4 File Offset: 0x00001DC4
		internal static string ELinq_UnsupportedNestedSingle
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedNestedSingle");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003BD0 File Offset: 0x00001DD0
		internal static string ELinq_UnsupportedInclude
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedInclude");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003BDC File Offset: 0x00001DDC
		internal static string ELinq_UnsupportedMergeAs
		{
			get
			{
				return EntityRes.GetString("ELinq_UnsupportedMergeAs");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003BE8 File Offset: 0x00001DE8
		internal static string ELinq_MethodNotDirectlyCallable
		{
			get
			{
				return EntityRes.GetString("ELinq_MethodNotDirectlyCallable");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003BF4 File Offset: 0x00001DF4
		internal static string ELinq_CycleDetected
		{
			get
			{
				return EntityRes.GetString("ELinq_CycleDetected");
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003C00 File Offset: 0x00001E00
		internal static string ELinq_EdmFunctionAttributeParameterNameNotValid(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ELinq_EdmFunctionAttributeParameterNameNotValid", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003C1E File Offset: 0x00001E1E
		internal static string ELinq_EdmFunctionAttributedFunctionWithWrongReturnType(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_EdmFunctionAttributedFunctionWithWrongReturnType", new object[] { p0, p1 });
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003C38 File Offset: 0x00001E38
		internal static string ELinq_EdmFunctionAttributedFunctionWithWrongInstance(object p0, object p1)
		{
			return EntityRes.GetString("ELinq_EdmFunctionAttributedFunctionWithWrongInstance", new object[] { p0, p1 });
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003C52 File Offset: 0x00001E52
		internal static string ELinq_EdmFunctionDirectCall
		{
			get
			{
				return EntityRes.GetString("ELinq_EdmFunctionDirectCall");
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003C5E File Offset: 0x00001E5E
		internal static string CompiledELinq_UnsupportedParameterTypes(object p0)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedParameterTypes", new object[] { p0 });
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003C74 File Offset: 0x00001E74
		internal static string CompiledELinq_UnsupportedNamedParameterType(object p0, object p1)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedNamedParameterType", new object[] { p0, p1 });
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003C8E File Offset: 0x00001E8E
		internal static string CompiledELinq_UnsupportedNamedParameterUseAsType(object p0, object p1)
		{
			return EntityRes.GetString("CompiledELinq_UnsupportedNamedParameterUseAsType", new object[] { p0, p1 });
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003CA8 File Offset: 0x00001EA8
		internal static string Update_UnsupportedExpressionKind(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnsupportedExpressionKind", new object[] { p0, p1 });
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003CC2 File Offset: 0x00001EC2
		internal static string Update_UnsupportedCastArgument(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedCastArgument", new object[] { p0 });
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003CD8 File Offset: 0x00001ED8
		internal static string Update_UnsupportedExtentType(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnsupportedExtentType", new object[] { p0, p1 });
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003CF2 File Offset: 0x00001EF2
		internal static string Update_ConstraintCycle
		{
			get
			{
				return EntityRes.GetString("Update_ConstraintCycle");
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003CFE File Offset: 0x00001EFE
		internal static string Update_UnsupportedJoinType(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedJoinType", new object[] { p0 });
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003D14 File Offset: 0x00001F14
		internal static string Update_UnsupportedProjection(object p0)
		{
			return EntityRes.GetString("Update_UnsupportedProjection", new object[] { p0 });
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003D2A File Offset: 0x00001F2A
		internal static string Update_ConcurrencyError(object p0)
		{
			return EntityRes.GetString("Update_ConcurrencyError", new object[] { p0 });
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003D40 File Offset: 0x00001F40
		internal static string Update_MissingEntity(object p0, object p1)
		{
			return EntityRes.GetString("Update_MissingEntity", new object[] { p0, p1 });
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003D5A File Offset: 0x00001F5A
		internal static string Update_RelationshipCardinalityConstraintViolation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityConstraintViolation", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003D86 File Offset: 0x00001F86
		internal static string Update_GeneralExecutionException
		{
			get
			{
				return EntityRes.GetString("Update_GeneralExecutionException");
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003D92 File Offset: 0x00001F92
		internal static string Update_MissingRequiredEntity(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_MissingRequiredEntity", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003DB0 File Offset: 0x00001FB0
		internal static string Update_RelationshipCardinalityViolation(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityViolation", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003DDC File Offset: 0x00001FDC
		internal static string Update_NotSupportedServerGenKey(object p0)
		{
			return EntityRes.GetString("Update_NotSupportedServerGenKey", new object[] { p0 });
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003DF2 File Offset: 0x00001FF2
		internal static string Update_NotSupportedIdentityType(object p0, object p1)
		{
			return EntityRes.GetString("Update_NotSupportedIdentityType", new object[] { p0, p1 });
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003E0C File Offset: 0x0000200C
		internal static string Update_NotSupportedComputedKeyColumn(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("Update_NotSupportedComputedKeyColumn", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00003E33 File Offset: 0x00002033
		internal static string Update_AmbiguousServerGenIdentifier
		{
			get
			{
				return EntityRes.GetString("Update_AmbiguousServerGenIdentifier");
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00003E3F File Offset: 0x0000203F
		internal static string Update_WorkspaceMismatch
		{
			get
			{
				return EntityRes.GetString("Update_WorkspaceMismatch");
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003E4B File Offset: 0x0000204B
		internal static string Update_MissingRequiredRelationshipValue(object p0, object p1)
		{
			return EntityRes.GetString("Update_MissingRequiredRelationshipValue", new object[] { p0, p1 });
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003E65 File Offset: 0x00002065
		internal static string Update_MissingResultColumn(object p0)
		{
			return EntityRes.GetString("Update_MissingResultColumn", new object[] { p0 });
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003E7B File Offset: 0x0000207B
		internal static string Update_NullReturnValueForNonNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("Update_NullReturnValueForNonNullableMember", new object[] { p0, p1 });
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003E95 File Offset: 0x00002095
		internal static string Update_ReturnValueHasUnexpectedType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Update_ReturnValueHasUnexpectedType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003EB7 File Offset: 0x000020B7
		internal static string Update_SqlEntitySetWithoutDmlFunctions(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_SqlEntitySetWithoutDmlFunctions", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003ED5 File Offset: 0x000020D5
		internal static string Update_UnableToConvertRowsAffectedParameterToInt32(object p0, object p1)
		{
			return EntityRes.GetString("Update_UnableToConvertRowsAffectedParameterToInt32", new object[] { p0, p1 });
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003EEF File Offset: 0x000020EF
		internal static string Update_MappingNotFound(object p0)
		{
			return EntityRes.GetString("Update_MappingNotFound", new object[] { p0 });
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003F05 File Offset: 0x00002105
		internal static string Update_ModifyingIdentityColumn(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_ModifyingIdentityColumn", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003F23 File Offset: 0x00002123
		internal static string Update_GeneratedDependent(object p0)
		{
			return EntityRes.GetString("Update_GeneratedDependent", new object[] { p0 });
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00003F39 File Offset: 0x00002139
		internal static string Update_ReferentialConstraintIntegrityViolation
		{
			get
			{
				return EntityRes.GetString("Update_ReferentialConstraintIntegrityViolation");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003F45 File Offset: 0x00002145
		internal static string Update_ErrorLoadingRecord
		{
			get
			{
				return EntityRes.GetString("Update_ErrorLoadingRecord");
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003F51 File Offset: 0x00002151
		internal static string Update_NullValue(object p0)
		{
			return EntityRes.GetString("Update_NullValue", new object[] { p0 });
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00003F67 File Offset: 0x00002167
		internal static string Update_CircularRelationships
		{
			get
			{
				return EntityRes.GetString("Update_CircularRelationships");
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003F73 File Offset: 0x00002173
		internal static string Update_RelationshipCardinalityConstraintViolationSingleValue(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("Update_RelationshipCardinalityConstraintViolationSingleValue", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003F9A File Offset: 0x0000219A
		internal static string Update_MissingFunctionMapping(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Update_MissingFunctionMapping", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00003FB8 File Offset: 0x000021B8
		internal static string Update_InvalidChanges
		{
			get
			{
				return EntityRes.GetString("Update_InvalidChanges");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003FC4 File Offset: 0x000021C4
		internal static string Update_DuplicateKeys
		{
			get
			{
				return EntityRes.GetString("Update_DuplicateKeys");
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003FD0 File Offset: 0x000021D0
		internal static string Update_AmbiguousForeignKey(object p0)
		{
			return EntityRes.GetString("Update_AmbiguousForeignKey", new object[] { p0 });
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00003FE6 File Offset: 0x000021E6
		internal static string Update_InsertingOrUpdatingReferenceToDeletedEntity(object p0)
		{
			return EntityRes.GetString("Update_InsertingOrUpdatingReferenceToDeletedEntity", new object[] { p0 });
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003FFC File Offset: 0x000021FC
		internal static string ViewGen_Extent
		{
			get
			{
				return EntityRes.GetString("ViewGen_Extent");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004008 File Offset: 0x00002208
		internal static string ViewGen_Null
		{
			get
			{
				return EntityRes.GetString("ViewGen_Null");
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004014 File Offset: 0x00002214
		internal static string ViewGen_CommaBlank
		{
			get
			{
				return EntityRes.GetString("ViewGen_CommaBlank");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004020 File Offset: 0x00002220
		internal static string ViewGen_Entities
		{
			get
			{
				return EntityRes.GetString("ViewGen_Entities");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000402C File Offset: 0x0000222C
		internal static string ViewGen_Tuples
		{
			get
			{
				return EntityRes.GetString("ViewGen_Tuples");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004038 File Offset: 0x00002238
		internal static string ViewGen_NotNull
		{
			get
			{
				return EntityRes.GetString("ViewGen_NotNull");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004044 File Offset: 0x00002244
		internal static string ViewGen_Failure
		{
			get
			{
				return EntityRes.GetString("ViewGen_Failure");
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004050 File Offset: 0x00002250
		internal static string ViewGen_NegatedCellConstant_0(object p0)
		{
			return EntityRes.GetString("ViewGen_NegatedCellConstant_0", new object[] { p0 });
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004066 File Offset: 0x00002266
		internal static string ViewGen_Error
		{
			get
			{
				return EntityRes.GetString("ViewGen_Error");
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004072 File Offset: 0x00002272
		internal static string ViewGen_AND
		{
			get
			{
				return EntityRes.GetString("ViewGen_AND");
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000407E File Offset: 0x0000227E
		internal static string Viewgen_CannotGenerateQueryViewUnderNoValidation(object p0)
		{
			return EntityRes.GetString("Viewgen_CannotGenerateQueryViewUnderNoValidation", new object[] { p0 });
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004094 File Offset: 0x00002294
		internal static string ViewGen_Missing_Sets_Mapping_0(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Sets_Mapping_0", new object[] { p0 });
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000040AA File Offset: 0x000022AA
		internal static string ViewGen_Missing_Type_Mapping_0(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Type_Mapping_0", new object[] { p0 });
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000040C0 File Offset: 0x000022C0
		internal static string ViewGen_Missing_Set_Mapping_0(object p0)
		{
			return EntityRes.GetString("ViewGen_Missing_Set_Mapping_0", new object[] { p0 });
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000040D6 File Offset: 0x000022D6
		internal static string ViewGen_Concurrency_Derived_Class_2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Concurrency_Derived_Class_2", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000040F4 File Offset: 0x000022F4
		internal static string ViewGen_Concurrency_Invalid_Condition_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Concurrency_Invalid_Condition_1", new object[] { p0, p1 });
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000410E File Offset: 0x0000230E
		internal static string ViewGen_TableKey_Missing_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_TableKey_Missing_1", new object[] { p0, p1 });
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004128 File Offset: 0x00002328
		internal static string ViewGen_EntitySetKey_Missing_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySetKey_Missing_1", new object[] { p0, p1 });
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004142 File Offset: 0x00002342
		internal static string ViewGen_AssociationSetKey_Missing_2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSetKey_Missing_2", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004160 File Offset: 0x00002360
		internal static string ViewGen_Cannot_Recover_Attributes_2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Cannot_Recover_Attributes_2", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000417E File Offset: 0x0000237E
		internal static string ViewGen_Cannot_Recover_Types_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Cannot_Recover_Types_1", new object[] { p0, p1 });
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004198 File Offset: 0x00002398
		internal static string ViewGen_Cannot_Disambiguate_MultiConstant_2(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Cannot_Disambiguate_MultiConstant_2", new object[] { p0, p1 });
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000041B2 File Offset: 0x000023B2
		internal static string ViewGen_No_Default_Value_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_No_Default_Value_1", new object[] { p0, p1 });
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000041CC File Offset: 0x000023CC
		internal static string ViewGen_No_Default_Value_For_Configuration_0(object p0)
		{
			return EntityRes.GetString("ViewGen_No_Default_Value_For_Configuration_0", new object[] { p0 });
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000041E2 File Offset: 0x000023E2
		internal static string ViewGen_KeyConstraint_Violation_5(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Violation_5", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000420E File Offset: 0x0000240E
		internal static string ViewGen_KeyConstraint_Update_Violation_EntitySet_3(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Update_Violation_EntitySet_3", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004230 File Offset: 0x00002430
		internal static string ViewGen_KeyConstraint_Update_Violation_AssociationSet_2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_KeyConstraint_Update_Violation_AssociationSet_2", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000424E File Offset: 0x0000244E
		internal static string ViewGen__AssociationEndShouldBeMappedToKey(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen__AssociationEndShouldBeMappedToKey", new object[] { p0, p1 });
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004268 File Offset: 0x00002468
		internal static string ViewGen_Duplicate_CProperties_0(object p0)
		{
			return EntityRes.GetString("ViewGen_Duplicate_CProperties_0", new object[] { p0 });
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000427E File Offset: 0x0000247E
		internal static string ViewGen_Duplicate_CProperties_IsMapped_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Duplicate_CProperties_IsMapped_1", new object[] { p0, p1 });
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004298 File Offset: 0x00002498
		internal static string ViewGen_NotNull_No_Projected_Slot_0(object p0)
		{
			return EntityRes.GetString("ViewGen_NotNull_No_Projected_Slot_0", new object[] { p0 });
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000042AE File Offset: 0x000024AE
		internal static string ViewGen_InvalidCondition_0(object p0)
		{
			return EntityRes.GetString("ViewGen_InvalidCondition_0", new object[] { p0 });
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000042C4 File Offset: 0x000024C4
		internal static string ViewGen_NonKeyProjectedWithOverlappingPartitions_0(object p0)
		{
			return EntityRes.GetString("ViewGen_NonKeyProjectedWithOverlappingPartitions_0", new object[] { p0 });
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000042DA File Offset: 0x000024DA
		internal static string ViewGen_CQ_PartitionConstraint_1(object p0)
		{
			return EntityRes.GetString("ViewGen_CQ_PartitionConstraint_1", new object[] { p0 });
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000042F0 File Offset: 0x000024F0
		internal static string ViewGen_CQ_DomainConstraint_1(object p0)
		{
			return EntityRes.GetString("ViewGen_CQ_DomainConstraint_1", new object[] { p0 });
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004306 File Offset: 0x00002506
		internal static string ViewGen_OneOfConst_MustBeNonNullable_0(object p0)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustBeNonNullable_0", new object[] { p0 });
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000431C File Offset: 0x0000251C
		internal static string ViewGen_OneOfConst_MustBeNull_0(object p0)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustBeNull_0", new object[] { p0 });
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004332 File Offset: 0x00002532
		internal static string ViewGen_OneOfConst_MustBeEqualTo_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustBeEqualTo_1", new object[] { p0, p1 });
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000434C File Offset: 0x0000254C
		internal static string ViewGen_OneOfConst_MustNotBeEqualTo_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustNotBeEqualTo_1", new object[] { p0, p1 });
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004366 File Offset: 0x00002566
		internal static string ViewGen_OneOfConst_MustBeOneOf_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustBeOneOf_1", new object[] { p0, p1 });
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004380 File Offset: 0x00002580
		internal static string ViewGen_OneOfConst_MustNotBeOneOf_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_MustNotBeOneOf_1", new object[] { p0, p1 });
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000439A File Offset: 0x0000259A
		internal static string ViewGen_OneOfConst_IsNonNullable_0(object p0)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsNonNullable_0", new object[] { p0 });
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000043B0 File Offset: 0x000025B0
		internal static string ViewGen_OneOfConst_IsEqualTo_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsEqualTo_1", new object[] { p0, p1 });
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000043CA File Offset: 0x000025CA
		internal static string ViewGen_OneOfConst_IsNotEqualTo_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsNotEqualTo_1", new object[] { p0, p1 });
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000043E4 File Offset: 0x000025E4
		internal static string ViewGen_OneOfConst_IsOneOf_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsOneOf_1", new object[] { p0, p1 });
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000043FE File Offset: 0x000025FE
		internal static string ViewGen_OneOfConst_IsNotOneOf_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsNotOneOf_1", new object[] { p0, p1 });
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004418 File Offset: 0x00002618
		internal static string ViewGen_OneOfConst_IsOneOfTypes_0(object p0)
		{
			return EntityRes.GetString("ViewGen_OneOfConst_IsOneOfTypes_0", new object[] { p0 });
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000442E File Offset: 0x0000262E
		internal static string ViewGen_ErrorLog_0(object p0)
		{
			return EntityRes.GetString("ViewGen_ErrorLog_0", new object[] { p0 });
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004444 File Offset: 0x00002644
		internal static string ViewGen_ErrorLog_1(object p0)
		{
			return EntityRes.GetString("ViewGen_ErrorLog_1", new object[] { p0 });
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000445A File Offset: 0x0000265A
		internal static string ViewGen_Foreign_Key_Missing_Table_Mapping_1(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Missing_Table_Mapping_1", new object[] { p0, p1 });
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004474 File Offset: 0x00002674
		internal static string ViewGen_Foreign_Key_ParentTable_NotMappedToEnd_5(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_ParentTable_NotMappedToEnd_5", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000044A0 File Offset: 0x000026A0
		internal static string ViewGen_Foreign_Key_4(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_4", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000044C7 File Offset: 0x000026C7
		internal static string ViewGen_Foreign_Key_UpperBound_MustBeOne_2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_UpperBound_MustBeOne_2", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000044E5 File Offset: 0x000026E5
		internal static string ViewGen_Foreign_Key_LowerBound_MustBeOne_2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_LowerBound_MustBeOne_2", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004503 File Offset: 0x00002703
		internal static string ViewGen_Foreign_Key_Missing_Relationship_Mapping_0(object p0)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Missing_Relationship_Mapping_0", new object[] { p0 });
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004519 File Offset: 0x00002719
		internal static string ViewGen_Foreign_Key_Not_Guaranteed_InCSpace_0(object p0)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_Not_Guaranteed_InCSpace_0", new object[] { p0 });
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000452F File Offset: 0x0000272F
		internal static string ViewGen_Foreign_Key_ColumnOrder_Incorrect_8(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
		{
			return EntityRes.GetString("ViewGen_Foreign_Key_ColumnOrder_Incorrect_8", new object[] { p0, p1, p2, p3, p4, p5, p6 });
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004560 File Offset: 0x00002760
		internal static string ViewGen_AssociationSet_AsUserString(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSet_AsUserString", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000457E File Offset: 0x0000277E
		internal static string ViewGen_AssociationSet_AsUserString_Negated(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ViewGen_AssociationSet_AsUserString_Negated", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000459C File Offset: 0x0000279C
		internal static string ViewGen_EntitySet_AsUserString(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySet_AsUserString", new object[] { p0, p1 });
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000045B6 File Offset: 0x000027B6
		internal static string ViewGen_EntitySet_AsUserString_Negated(object p0, object p1)
		{
			return EntityRes.GetString("ViewGen_EntitySet_AsUserString_Negated", new object[] { p0, p1 });
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000045D0 File Offset: 0x000027D0
		internal static string ViewGen_EntityInstanceToken
		{
			get
			{
				return EntityRes.GetString("ViewGen_EntityInstanceToken");
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000045DC File Offset: 0x000027DC
		internal static string Viewgen_ConfigurationErrorMsg(object p0)
		{
			return EntityRes.GetString("Viewgen_ConfigurationErrorMsg", new object[] { p0 });
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000045F2 File Offset: 0x000027F2
		internal static string ViewGen_HashOnMappingClosure_Not_Matching(object p0)
		{
			return EntityRes.GetString("ViewGen_HashOnMappingClosure_Not_Matching", new object[] { p0 });
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004608 File Offset: 0x00002808
		internal static string Viewgen_RightSideNotDisjoint(object p0)
		{
			return EntityRes.GetString("Viewgen_RightSideNotDisjoint", new object[] { p0 });
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000461E File Offset: 0x0000281E
		internal static string Viewgen_QV_RewritingNotFound(object p0)
		{
			return EntityRes.GetString("Viewgen_QV_RewritingNotFound", new object[] { p0 });
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004634 File Offset: 0x00002834
		internal static string Viewgen_NullableMappingForNonNullableColumn(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_NullableMappingForNonNullableColumn", new object[] { p0, p1 });
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000464E File Offset: 0x0000284E
		internal static string Viewgen_ErrorPattern_ConditionMemberIsMapped(object p0)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_ConditionMemberIsMapped", new object[] { p0 });
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004664 File Offset: 0x00002864
		internal static string Viewgen_ErrorPattern_DuplicateConditionValue(object p0)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_DuplicateConditionValue", new object[] { p0 });
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000467A File Offset: 0x0000287A
		internal static string Viewgen_ErrorPattern_TableMappedToMultipleES(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_TableMappedToMultipleES", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00004698 File Offset: 0x00002898
		internal static string Viewgen_ErrorPattern_Partition_Disj_Eq
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Eq");
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000046A4 File Offset: 0x000028A4
		internal static string Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember", new object[] { p0, p1 });
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000046BE File Offset: 0x000028BE
		internal static string Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition", new object[] { p0, p1 });
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000046D8 File Offset: 0x000028D8
		internal static string Viewgen_ErrorPattern_Partition_Disj_Subs_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Subs_Ref");
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000046E4 File Offset: 0x000028E4
		internal static string Viewgen_ErrorPattern_Partition_Disj_Subs
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Subs");
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000046F0 File Offset: 0x000028F0
		internal static string Viewgen_ErrorPattern_Partition_Disj_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Disj_Unk");
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000046FC File Offset: 0x000028FC
		internal static string Viewgen_ErrorPattern_Partition_Eq_Disj
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Disj");
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00004708 File Offset: 0x00002908
		internal static string Viewgen_ErrorPattern_Partition_Eq_Subs_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Subs_Ref");
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004714 File Offset: 0x00002914
		internal static string Viewgen_ErrorPattern_Partition_Eq_Subs
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Subs");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00004720 File Offset: 0x00002920
		internal static string Viewgen_ErrorPattern_Partition_Eq_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Unk");
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000472C File Offset: 0x0000292C
		internal static string Viewgen_ErrorPattern_Partition_Eq_Unk_Association
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Eq_Unk_Association");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00004738 File Offset: 0x00002938
		internal static string Viewgen_ErrorPattern_Partition_Sub_Disj
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Disj");
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00004744 File Offset: 0x00002944
		internal static string Viewgen_ErrorPattern_Partition_Sub_Eq
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Eq");
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004750 File Offset: 0x00002950
		internal static string Viewgen_ErrorPattern_Partition_Sub_Eq_Ref
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Eq_Ref");
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000475C File Offset: 0x0000295C
		internal static string Viewgen_ErrorPattern_Partition_Sub_Unk
		{
			get
			{
				return EntityRes.GetString("Viewgen_ErrorPattern_Partition_Sub_Unk");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00004768 File Offset: 0x00002968
		internal static string Viewgen_NoJoinKeyOrFK
		{
			get
			{
				return EntityRes.GetString("Viewgen_NoJoinKeyOrFK");
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004774 File Offset: 0x00002974
		internal static string Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct(object p0, object p1)
		{
			return EntityRes.GetString("Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct", new object[] { p0, p1 });
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000478E File Offset: 0x0000298E
		internal static string InvalidCollectionForMapping(object p0)
		{
			return EntityRes.GetString("InvalidCollectionForMapping", new object[] { p0 });
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000047A4 File Offset: 0x000029A4
		internal static string OnlyStoreConnectionsSupported
		{
			get
			{
				return EntityRes.GetString("OnlyStoreConnectionsSupported");
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000047B0 File Offset: 0x000029B0
		internal static string StoreItemCollectionMustHaveOneArtifact(object p0)
		{
			return EntityRes.GetString("StoreItemCollectionMustHaveOneArtifact", new object[] { p0 });
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000047C6 File Offset: 0x000029C6
		internal static string CheckArgumentContainsNullFailed(object p0)
		{
			return EntityRes.GetString("CheckArgumentContainsNullFailed", new object[] { p0 });
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000047DC File Offset: 0x000029DC
		internal static string InvalidRelationshipSetName(object p0)
		{
			return EntityRes.GetString("InvalidRelationshipSetName", new object[] { p0 });
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000047F2 File Offset: 0x000029F2
		internal static string MemberInvalidIdentity(object p0)
		{
			return EntityRes.GetString("MemberInvalidIdentity", new object[] { p0 });
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004808 File Offset: 0x00002A08
		internal static string InvalidEntitySetName(object p0)
		{
			return EntityRes.GetString("InvalidEntitySetName", new object[] { p0 });
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000481E File Offset: 0x00002A1E
		internal static string ItemInvalidIdentity(object p0)
		{
			return EntityRes.GetString("ItemInvalidIdentity", new object[] { p0 });
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004834 File Offset: 0x00002A34
		internal static string ItemDuplicateIdentity(object p0)
		{
			return EntityRes.GetString("ItemDuplicateIdentity", new object[] { p0 });
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000484A File Offset: 0x00002A4A
		internal static string NotStringTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotStringTypeForTypeUsage");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00004856 File Offset: 0x00002A56
		internal static string NotBinaryTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotBinaryTypeForTypeUsage");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00004862 File Offset: 0x00002A62
		internal static string NotDateTimeTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDateTimeTypeForTypeUsage");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000486E File Offset: 0x00002A6E
		internal static string NotDateTimeOffsetTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDateTimeOffsetTypeForTypeUsage");
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000487A File Offset: 0x00002A7A
		internal static string NotTimeTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotTimeTypeForTypeUsage");
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00004886 File Offset: 0x00002A86
		internal static string NotDecimalTypeForTypeUsage
		{
			get
			{
				return EntityRes.GetString("NotDecimalTypeForTypeUsage");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00004892 File Offset: 0x00002A92
		internal static string ArrayTooSmall
		{
			get
			{
				return EntityRes.GetString("ArrayTooSmall");
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000489E File Offset: 0x00002A9E
		internal static string MoreThanOneItemMatchesIdentity(object p0)
		{
			return EntityRes.GetString("MoreThanOneItemMatchesIdentity", new object[] { p0 });
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000048B4 File Offset: 0x00002AB4
		internal static string MissingDefaultValueForConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MissingDefaultValueForConstantFacet", new object[] { p0, p1 });
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000048CE File Offset: 0x00002ACE
		internal static string MinAndMaxValueMustBeSameForConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxValueMustBeSameForConstantFacet", new object[] { p0, p1 });
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000048E8 File Offset: 0x00002AE8
		internal static string BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet", new object[] { p0, p1 });
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004902 File Offset: 0x00002B02
		internal static string MinAndMaxValueMustBeDifferentForNonConstantFacet(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxValueMustBeDifferentForNonConstantFacet", new object[] { p0, p1 });
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000491C File Offset: 0x00002B1C
		internal static string MinAndMaxMustBePositive(object p0, object p1)
		{
			return EntityRes.GetString("MinAndMaxMustBePositive", new object[] { p0, p1 });
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004936 File Offset: 0x00002B36
		internal static string MinMustBeLessThanMax(object p0, object p1, object p2)
		{
			return EntityRes.GetString("MinMustBeLessThanMax", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00004954 File Offset: 0x00002B54
		internal static string SameRoleNameOnRelationshipAttribute(object p0, object p1)
		{
			return EntityRes.GetString("SameRoleNameOnRelationshipAttribute", new object[] { p0, p1 });
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000496E File Offset: 0x00002B6E
		internal static string RoleTypeInEdmRelationshipAttributeIsInvalidType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("RoleTypeInEdmRelationshipAttributeIsInvalidType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000498C File Offset: 0x00002B8C
		internal static string TargetRoleNameInNavigationPropertyNotValid(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TargetRoleNameInNavigationPropertyNotValid", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000049AE File Offset: 0x00002BAE
		internal static string RelationshipNameInNavigationPropertyNotValid(object p0, object p1, object p2)
		{
			return EntityRes.GetString("RelationshipNameInNavigationPropertyNotValid", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000049CC File Offset: 0x00002BCC
		internal static string NestedClassNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("NestedClassNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000049E6 File Offset: 0x00002BE6
		internal static string NullParameterForEdmRelationshipAttribute(object p0, object p1)
		{
			return EntityRes.GetString("NullParameterForEdmRelationshipAttribute", new object[] { p0, p1 });
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00004A00 File Offset: 0x00002C00
		internal static string NullRelationshipNameforEdmRelationshipAttribute(object p0)
		{
			return EntityRes.GetString("NullRelationshipNameforEdmRelationshipAttribute", new object[] { p0 });
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00004A16 File Offset: 0x00002C16
		internal static string NavigationPropertyRelationshipEndTypeMismatch(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("NavigationPropertyRelationshipEndTypeMismatch", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004A3D File Offset: 0x00002C3D
		internal static string AllArtifactsMustTargetSameProvider_InvariantName(object p0, object p1)
		{
			return EntityRes.GetString("AllArtifactsMustTargetSameProvider_InvariantName", new object[] { p0, p1 });
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00004A57 File Offset: 0x00002C57
		internal static string AllArtifactsMustTargetSameProvider_ManifestToken(object p0, object p1)
		{
			return EntityRes.GetString("AllArtifactsMustTargetSameProvider_ManifestToken", new object[] { p0, p1 });
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00004A71 File Offset: 0x00002C71
		internal static string ProviderManifestTokenNotFound
		{
			get
			{
				return EntityRes.GetString("ProviderManifestTokenNotFound");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00004A7D File Offset: 0x00002C7D
		internal static string FailedToRetrieveProviderManifest
		{
			get
			{
				return EntityRes.GetString("FailedToRetrieveProviderManifest");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004A89 File Offset: 0x00002C89
		internal static string InvalidMaxLengthSize
		{
			get
			{
				return EntityRes.GetString("InvalidMaxLengthSize");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00004A95 File Offset: 0x00002C95
		internal static string ArgumentMustBeCSpaceType
		{
			get
			{
				return EntityRes.GetString("ArgumentMustBeCSpaceType");
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00004AA1 File Offset: 0x00002CA1
		internal static string ArgumentMustBeOSpaceType
		{
			get
			{
				return EntityRes.GetString("ArgumentMustBeOSpaceType");
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00004AAD File Offset: 0x00002CAD
		internal static string FailedToFindOSpaceTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindOSpaceTypeMapping", new object[] { p0 });
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00004AC3 File Offset: 0x00002CC3
		internal static string FailedToFindCSpaceTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindCSpaceTypeMapping", new object[] { p0 });
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00004AD9 File Offset: 0x00002CD9
		internal static string FailedToFindClrTypeMapping(object p0)
		{
			return EntityRes.GetString("FailedToFindClrTypeMapping", new object[] { p0 });
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00004AEF File Offset: 0x00002CEF
		internal static string GenericTypeNotSupported(object p0)
		{
			return EntityRes.GetString("GenericTypeNotSupported", new object[] { p0 });
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00004B05 File Offset: 0x00002D05
		internal static string Validator_EmptyIdentity
		{
			get
			{
				return EntityRes.GetString("Validator_EmptyIdentity");
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004B11 File Offset: 0x00002D11
		internal static string Validator_CollectionHasNoTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_CollectionHasNoTypeUsage");
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00004B1D File Offset: 0x00002D1D
		internal static string Validator_NoKeyMembers(object p0)
		{
			return EntityRes.GetString("Validator_NoKeyMembers", new object[] { p0 });
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00004B33 File Offset: 0x00002D33
		internal static string Validator_FacetTypeIsNull
		{
			get
			{
				return EntityRes.GetString("Validator_FacetTypeIsNull");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00004B3F File Offset: 0x00002D3F
		internal static string Validator_MemberHasNullDeclaringType
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNullDeclaringType");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00004B4B File Offset: 0x00002D4B
		internal static string Validator_MemberHasNullTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNullTypeUsage");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00004B57 File Offset: 0x00002D57
		internal static string Validator_ItemAttributeHasNullTypeUsage
		{
			get
			{
				return EntityRes.GetString("Validator_ItemAttributeHasNullTypeUsage");
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00004B63 File Offset: 0x00002D63
		internal static string Validator_RefTypeHasNullEntityType
		{
			get
			{
				return EntityRes.GetString("Validator_RefTypeHasNullEntityType");
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00004B6F File Offset: 0x00002D6F
		internal static string Validator_TypeUsageHasNullEdmType
		{
			get
			{
				return EntityRes.GetString("Validator_TypeUsageHasNullEdmType");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00004B7B File Offset: 0x00002D7B
		internal static string Validator_BaseTypeHasMemberOfSameName
		{
			get
			{
				return EntityRes.GetString("Validator_BaseTypeHasMemberOfSameName");
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00004B87 File Offset: 0x00002D87
		internal static string Validator_CollectionTypesCannotHaveBaseType
		{
			get
			{
				return EntityRes.GetString("Validator_CollectionTypesCannotHaveBaseType");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004B93 File Offset: 0x00002D93
		internal static string Validator_RefTypesCannotHaveBaseType
		{
			get
			{
				return EntityRes.GetString("Validator_RefTypesCannotHaveBaseType");
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00004B9F File Offset: 0x00002D9F
		internal static string Validator_TypeHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_TypeHasNoName");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00004BAB File Offset: 0x00002DAB
		internal static string Validator_TypeHasNoNamespace
		{
			get
			{
				return EntityRes.GetString("Validator_TypeHasNoNamespace");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00004BB7 File Offset: 0x00002DB7
		internal static string Validator_FacetHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_FacetHasNoName");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00004BC3 File Offset: 0x00002DC3
		internal static string Validator_MemberHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_MemberHasNoName");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00004BCF File Offset: 0x00002DCF
		internal static string Validator_MetadataPropertyHasNoName
		{
			get
			{
				return EntityRes.GetString("Validator_MetadataPropertyHasNoName");
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00004BDB File Offset: 0x00002DDB
		internal static string Validator_NullableEntityKeyProperty(object p0, object p1)
		{
			return EntityRes.GetString("Validator_NullableEntityKeyProperty", new object[] { p0, p1 });
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00004BF5 File Offset: 0x00002DF5
		internal static string Validator_OSpace_InvalidNavPropReturnType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_InvalidNavPropReturnType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00004C13 File Offset: 0x00002E13
		internal static string Validator_OSpace_ScalarPropertyNotPrimitive(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_ScalarPropertyNotPrimitive", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00004C31 File Offset: 0x00002E31
		internal static string Validator_OSpace_ComplexPropertyNotComplex(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_ComplexPropertyNotComplex", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00004C4F File Offset: 0x00002E4F
		internal static string Validator_OSpace_Convention_MultipleTypesWithSameName(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MultipleTypesWithSameName", new object[] { p0 });
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00004C65 File Offset: 0x00002E65
		internal static string Validator_OSpace_Convention_NonPrimitiveTypeProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_NonPrimitiveTypeProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00004C83 File Offset: 0x00002E83
		internal static string Validator_OSpace_Convention_MissingRequiredProperty(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MissingRequiredProperty", new object[] { p0, p1 });
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00004C9D File Offset: 0x00002E9D
		internal static string Validator_OSpace_Convention_BaseTypeIncompatible(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_BaseTypeIncompatible", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00004CBB File Offset: 0x00002EBB
		internal static string Validator_OSpace_Convention_MissingOSpaceType(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_MissingOSpaceType", new object[] { p0 });
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00004CD1 File Offset: 0x00002ED1
		internal static string Validator_OSpace_Convention_RelationshipNotLoaded(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_RelationshipNotLoaded", new object[] { p0, p1 });
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00004CEB File Offset: 0x00002EEB
		internal static string Validator_OSpace_Convention_AttributeAssemblyReferenced(object p0)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_AttributeAssemblyReferenced", new object[] { p0 });
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00004D01 File Offset: 0x00002F01
		internal static string Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_ScalarPropertyMissginGetterOrSetter", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00004D1F File Offset: 0x00002F1F
		internal static string Validator_OSpace_Convention_AmbiguousClrType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_AmbiguousClrType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00004D3D File Offset: 0x00002F3D
		internal static string Validator_OSpace_Convention_Struct(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_Struct", new object[] { p0, p1 });
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00004D57 File Offset: 0x00002F57
		internal static string Validator_OSpace_Convention_BaseTypeNotLoaded(object p0, object p1)
		{
			return EntityRes.GetString("Validator_OSpace_Convention_BaseTypeNotLoaded", new object[] { p0, p1 });
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00004D71 File Offset: 0x00002F71
		internal static string ExtraInfo
		{
			get
			{
				return EntityRes.GetString("ExtraInfo");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00004D7D File Offset: 0x00002F7D
		internal static string Metadata_General_Error
		{
			get
			{
				return EntityRes.GetString("Metadata_General_Error");
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00004D89 File Offset: 0x00002F89
		internal static string InvalidNumberOfParametersForAggregateFunction(object p0)
		{
			return EntityRes.GetString("InvalidNumberOfParametersForAggregateFunction", new object[] { p0 });
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00004D9F File Offset: 0x00002F9F
		internal static string InvalidParameterTypeForAggregateFunction(object p0, object p1)
		{
			return EntityRes.GetString("InvalidParameterTypeForAggregateFunction", new object[] { p0, p1 });
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00004DB9 File Offset: 0x00002FB9
		internal static string ItemCollectionAlreadyRegistered(object p0)
		{
			return EntityRes.GetString("ItemCollectionAlreadyRegistered", new object[] { p0 });
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00004DCF File Offset: 0x00002FCF
		internal static string InvalidSchemaEncountered(object p0)
		{
			return EntityRes.GetString("InvalidSchemaEncountered", new object[] { p0 });
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00004DE5 File Offset: 0x00002FE5
		internal static string SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("SystemNamespaceEncountered", new object[] { p0 });
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00004DFB File Offset: 0x00002FFB
		internal static string NoCollectionForSpace(object p0)
		{
			return EntityRes.GetString("NoCollectionForSpace", new object[] { p0 });
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00004E11 File Offset: 0x00003011
		internal static string OperationOnReadOnlyCollection
		{
			get
			{
				return EntityRes.GetString("OperationOnReadOnlyCollection");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00004E1D File Offset: 0x0000301D
		internal static string OperationOnReadOnlyItem
		{
			get
			{
				return EntityRes.GetString("OperationOnReadOnlyItem");
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00004E29 File Offset: 0x00003029
		internal static string EntitySetInAnotherContainer
		{
			get
			{
				return EntityRes.GetString("EntitySetInAnotherContainer");
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00004E35 File Offset: 0x00003035
		internal static string InvalidKeyMember(object p0)
		{
			return EntityRes.GetString("InvalidKeyMember", new object[] { p0 });
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00004E4B File Offset: 0x0000304B
		internal static string InvalidFileExtension(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidFileExtension", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00004E69 File Offset: 0x00003069
		internal static string NewTypeConflictsWithExistingType(object p0, object p1)
		{
			return EntityRes.GetString("NewTypeConflictsWithExistingType", new object[] { p0, p1 });
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00004E83 File Offset: 0x00003083
		internal static string NotValidInputPath
		{
			get
			{
				return EntityRes.GetString("NotValidInputPath");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00004E8F File Offset: 0x0000308F
		internal static string UnableToDetermineApplicationContext
		{
			get
			{
				return EntityRes.GetString("UnableToDetermineApplicationContext");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00004E9B File Offset: 0x0000309B
		internal static string WildcardEnumeratorReturnedNull
		{
			get
			{
				return EntityRes.GetString("WildcardEnumeratorReturnedNull");
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00004EA7 File Offset: 0x000030A7
		internal static string InvalidUseOfWebPath(object p0)
		{
			return EntityRes.GetString("InvalidUseOfWebPath", new object[] { p0 });
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00004EBD File Offset: 0x000030BD
		internal static string UnableToFindReflectedType(object p0, object p1)
		{
			return EntityRes.GetString("UnableToFindReflectedType", new object[] { p0, p1 });
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00004ED7 File Offset: 0x000030D7
		internal static string AssemblyMissingFromAssembliesToConsider(object p0)
		{
			return EntityRes.GetString("AssemblyMissingFromAssembliesToConsider", new object[] { p0 });
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00004EED File Offset: 0x000030ED
		internal static string InvalidCollectionSpecified(object p0)
		{
			return EntityRes.GetString("InvalidCollectionSpecified", new object[] { p0 });
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00004F03 File Offset: 0x00003103
		internal static string UnableToLoadResource
		{
			get
			{
				return EntityRes.GetString("UnableToLoadResource");
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00004F0F File Offset: 0x0000310F
		internal static string EdmVersionNotSupportedByRuntime(object p0, object p1)
		{
			return EntityRes.GetString("EdmVersionNotSupportedByRuntime", new object[] { p0, p1 });
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00004F29 File Offset: 0x00003129
		internal static string AtleastOneSSDLNeeded
		{
			get
			{
				return EntityRes.GetString("AtleastOneSSDLNeeded");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00004F35 File Offset: 0x00003135
		internal static string InvalidMetadataPath
		{
			get
			{
				return EntityRes.GetString("InvalidMetadataPath");
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00004F41 File Offset: 0x00003141
		internal static string UnableToResolveAssembly(object p0)
		{
			return EntityRes.GetString("UnableToResolveAssembly", new object[] { p0 });
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00004F57 File Offset: 0x00003157
		internal static string UnableToDetermineStoreVersion
		{
			get
			{
				return EntityRes.GetString("UnableToDetermineStoreVersion");
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00004F63 File Offset: 0x00003163
		internal static string DuplicatedFunctionoverloads(object p0, object p1)
		{
			return EntityRes.GetString("DuplicatedFunctionoverloads", new object[] { p0, p1 });
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00004F7D File Offset: 0x0000317D
		internal static string EntitySetNotInCSPace(object p0)
		{
			return EntityRes.GetString("EntitySetNotInCSPace", new object[] { p0 });
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00004F93 File Offset: 0x00003193
		internal static string TypeNotInEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeNotInEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00004FB1 File Offset: 0x000031B1
		internal static string TypeNotInAssociationSet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeNotInAssociationSet", new object[] { p0, p1, p2 });
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00004FCF File Offset: 0x000031CF
		internal static string DifferentSchemaVersionInCollection(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DifferentSchemaVersionInCollection", new object[] { p0, p1, p2 });
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00004FED File Offset: 0x000031ED
		internal static string Mapping_General_Error_0
		{
			get
			{
				return EntityRes.GetString("Mapping_General_Error_0");
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00004FF9 File Offset: 0x000031F9
		internal static string Mapping_InvalidContent_General_0
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_General_0");
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005005 File Offset: 0x00003205
		internal static string Mapping_InvalidContent_EntityContainer_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_EntityContainer_1", new object[] { p0 });
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000501B File Offset: 0x0000321B
		internal static string Mapping_InvalidContent_StorageEntityContainer_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_StorageEntityContainer_1", new object[] { p0 });
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005031 File Offset: 0x00003231
		internal static string Mapping_AlreadyMapped_StorageEntityContainer_1(object p0)
		{
			return EntityRes.GetString("Mapping_AlreadyMapped_StorageEntityContainer_1", new object[] { p0 });
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00005047 File Offset: 0x00003247
		internal static string Mapping_InvalidContent_Entity_Set_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Set_1", new object[] { p0 });
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000505D File Offset: 0x0000325D
		internal static string Mapping_InvalidContent_Entity_Type_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Type_1", new object[] { p0 });
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00005073 File Offset: 0x00003273
		internal static string Mapping_InvalidContent_AbstractEntity_FunctionMapping_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_FunctionMapping_1", new object[] { p0 });
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00005089 File Offset: 0x00003289
		internal static string Mapping_InvalidContent_ImplicitMappingForAbstractReturnType_FunctionMapping_1(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ImplicitMappingForAbstractReturnType_FunctionMapping_1", new object[] { p0, p1 });
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000050A3 File Offset: 0x000032A3
		internal static string Mapping_InvalidContent_AbstractEntity_Type_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_Type_1", new object[] { p0 });
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000050B9 File Offset: 0x000032B9
		internal static string Mapping_InvalidContent_AbstractEntity_IsOfType_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AbstractEntity_IsOfType_1", new object[] { p0 });
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000050CF File Offset: 0x000032CF
		internal static string Mapping_InvalidContent_Entity_Type_For_Entity_Set_3(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Entity_Type_For_Entity_Set_3", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000050ED File Offset: 0x000032ED
		internal static string Mapping_Invalid_Association_Type_For_Association_Set_3(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_Association_Type_For_Association_Set_3", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000510B File Offset: 0x0000330B
		internal static string Mapping_InvalidContent_Table_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Table_1", new object[] { p0 });
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00005121 File Offset: 0x00003321
		internal static string Mapping_InvalidContent_Complex_Type_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Complex_Type_1", new object[] { p0 });
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00005137 File Offset: 0x00003337
		internal static string Mapping_InvalidContent_Association_Set_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Association_Set_1", new object[] { p0 });
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000514D File Offset: 0x0000334D
		internal static string Mapping_InvalidContent_AssociationSet_Condition_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_AssociationSet_Condition_1", new object[] { p0 });
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00005163 File Offset: 0x00003363
		internal static string Mapping_InvalidContent_ForeignKey_Association_Set_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ForeignKey_Association_Set_1", new object[] { p0 });
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005179 File Offset: 0x00003379
		internal static string Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK_1", new object[] { p0 });
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000518F File Offset: 0x0000338F
		internal static string Mapping_InvalidContent_Association_Type_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Association_Type_1", new object[] { p0 });
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000051A5 File Offset: 0x000033A5
		internal static string Mapping_InvalidContent_EndProperty_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_EndProperty_1", new object[] { p0 });
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000051BB File Offset: 0x000033BB
		internal static string Mapping_InvalidContent_Association_Type_Empty_0
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Association_Type_Empty_0");
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000051C7 File Offset: 0x000033C7
		internal static string Mapping_InvalidContent_Table_Expected_0
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Table_Expected_0");
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000051D3 File Offset: 0x000033D3
		internal static string Mapping_InvalidContent_Cdm_Member_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Cdm_Member_1", new object[] { p0 });
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000051E9 File Offset: 0x000033E9
		internal static string Mapping_InvalidContent_Column_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Column_1", new object[] { p0 });
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000051FF File Offset: 0x000033FF
		internal static string Mapping_InvalidContent_End_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_End_1", new object[] { p0 });
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00005215 File Offset: 0x00003415
		internal static string Mapping_InvalidContent_Set_Mapping_0
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_Set_Mapping_0");
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00005221 File Offset: 0x00003421
		internal static string Mapping_InvalidContent_Duplicate_Condition_Member_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Duplicate_Condition_Member_1", new object[] { p0 });
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00005237 File Offset: 0x00003437
		internal static string Mapping_InvalidContent_ConditionMapping_Both_Members_0
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Both_Members_0");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00005243 File Offset: 0x00003443
		internal static string Mapping_InvalidContent_ConditionMapping_Either_Members_0
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Either_Members_0");
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000524F File Offset: 0x0000344F
		internal static string Mapping_InvalidContent_ConditionMapping_Both_Values_0
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Both_Values_0");
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000525B File Offset: 0x0000345B
		internal static string Mapping_InvalidContent_ConditionMapping_Either_Values_0
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Either_Values_0");
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00005267 File Offset: 0x00003467
		internal static string Mapping_InvalidContent_ConditionMapping_NonScalar_0
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_NonScalar_0");
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00005273 File Offset: 0x00003473
		internal static string Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind_2", new object[] { p0, p1 });
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000528D File Offset: 0x0000348D
		internal static string Mapping_InvalidContent_ConditionMapping_InvalidMember_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_InvalidMember_1", new object[] { p0 });
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000052A3 File Offset: 0x000034A3
		internal static string Mapping_InvalidContent_ConditionMapping_Computed(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_ConditionMapping_Computed", new object[] { p0 });
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000052B9 File Offset: 0x000034B9
		internal static string Mapping_InvalidContent_Emtpty_SetMap_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidContent_Emtpty_SetMap_1", new object[] { p0 });
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000052CF File Offset: 0x000034CF
		internal static string Mapping_InvalidContent_TypeMapping_QueryView
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_TypeMapping_QueryView");
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000052DB File Offset: 0x000034DB
		internal static string Mapping_Default_OCMapping_Clr_Member_3(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Clr_Member_3", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000052F9 File Offset: 0x000034F9
		internal static string Mapping_Default_OCMapping_Clr_Member_4(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Clr_Member_4", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00005317 File Offset: 0x00003517
		internal static string Mapping_Default_OCMapping_Invalid_MemberType_6(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Invalid_MemberType_6", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00005343 File Offset: 0x00003543
		internal static string Mapping_Default_OCMapping_MemberKind_Mismatch_6(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_MemberKind_Mismatch_6", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000536F File Offset: 0x0000356F
		internal static string Mapping_Default_OCMapping_MultiplicityMismatch_6(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_MultiplicityMismatch_6", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000539B File Offset: 0x0000359B
		internal static string Mapping_Default_OCMapping_Member_Count_Mismatch_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Member_Count_Mismatch_2", new object[] { p0, p1 });
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000053B5 File Offset: 0x000035B5
		internal static string Mapping_Default_OCMapping_Member_Type_Mismatch(object p0, object p1, object p2, object p3, object p4, object p5, object p6)
		{
			return EntityRes.GetString("Mapping_Default_OCMapping_Member_Type_Mismatch", new object[] { p0, p1, p2, p3, p4, p5, p6 });
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000053E6 File Offset: 0x000035E6
		internal static string Mapping_NotFound_EntityContainer(object p0)
		{
			return EntityRes.GetString("Mapping_NotFound_EntityContainer", new object[] { p0 });
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000053FC File Offset: 0x000035FC
		internal static string Mapping_Duplicate_CdmAssociationSet_StorageMap_1(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_CdmAssociationSet_StorageMap_1", new object[] { p0 });
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00005412 File Offset: 0x00003612
		internal static string Mapping_Invalid_CSRootElementMissing_0(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_CSRootElementMissing_0", new object[] { p0, p1, p2 });
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00005430 File Offset: 0x00003630
		internal static string Mapping_ConditionValueTypeMismatch_0
		{
			get
			{
				return EntityRes.GetString("Mapping_ConditionValueTypeMismatch_0");
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000543C File Offset: 0x0000363C
		internal static string Mapping_Storage_InvalidSpace_1(object p0)
		{
			return EntityRes.GetString("Mapping_Storage_InvalidSpace_1", new object[] { p0 });
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00005452 File Offset: 0x00003652
		internal static string Mapping_Invalid_Member_Mapping_6(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Invalid_Member_Mapping_6", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000547E File Offset: 0x0000367E
		internal static string Mapping_Invalid_CSide_ScalarProperty_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_CSide_ScalarProperty_1", new object[] { p0 });
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00005494 File Offset: 0x00003694
		internal static string Mapping_Duplicate_Type_1(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_Type_1", new object[] { p0 });
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000054AA File Offset: 0x000036AA
		internal static string Mapping_Duplicate_PropertyMap_CaseInsensitive(object p0)
		{
			return EntityRes.GetString("Mapping_Duplicate_PropertyMap_CaseInsensitive", new object[] { p0 });
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000054C0 File Offset: 0x000036C0
		internal static string Mapping_Enum_EmptyValue_1(object p0)
		{
			return EntityRes.GetString("Mapping_Enum_EmptyValue_1", new object[] { p0 });
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000054D6 File Offset: 0x000036D6
		internal static string Mapping_Enum_InvalidValue_1(object p0)
		{
			return EntityRes.GetString("Mapping_Enum_InvalidValue_1", new object[] { p0 });
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000054EC File Offset: 0x000036EC
		internal static string Mapping_InvalidMappingSchema_Parsing_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidMappingSchema_Parsing_1", new object[] { p0 });
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00005502 File Offset: 0x00003702
		internal static string Mapping_InvalidMappingSchema_validation_1(object p0)
		{
			return EntityRes.GetString("Mapping_InvalidMappingSchema_validation_1", new object[] { p0 });
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00005518 File Offset: 0x00003718
		internal static string Mapping_Object_InvalidType(object p0)
		{
			return EntityRes.GetString("Mapping_Object_InvalidType", new object[] { p0 });
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000552E File Offset: 0x0000372E
		internal static string Mapping_Provider_WrongConnectionType(object p0)
		{
			return EntityRes.GetString("Mapping_Provider_WrongConnectionType", new object[] { p0 });
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00005544 File Offset: 0x00003744
		internal static string Mapping_Provider_WrongManifestType(object p0)
		{
			return EntityRes.GetString("Mapping_Provider_WrongManifestType", new object[] { p0 });
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000555A File Offset: 0x0000375A
		internal static string Mapping_Views_For_Extent_Not_Generated(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Views_For_Extent_Not_Generated", new object[] { p0, p1 });
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00005574 File Offset: 0x00003774
		internal static string Mapping_TableName_QueryView_1(object p0)
		{
			return EntityRes.GetString("Mapping_TableName_QueryView_1", new object[] { p0 });
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000558A File Offset: 0x0000378A
		internal static string Mapping_Empty_QueryView_1(object p0)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView_1", new object[] { p0 });
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000055A0 File Offset: 0x000037A0
		internal static string Mapping_Empty_QueryView_OfType_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView_OfType_2", new object[] { p0, p1 });
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000055BA File Offset: 0x000037BA
		internal static string Mapping_Empty_QueryView_OfTypeOnly_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Empty_QueryView_OfTypeOnly_2", new object[] { p0, p1 });
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000055D4 File Offset: 0x000037D4
		internal static string Mapping_QueryView_PropertyMaps_1(object p0)
		{
			return EntityRes.GetString("Mapping_QueryView_PropertyMaps_1", new object[] { p0 });
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000055EA File Offset: 0x000037EA
		internal static string Mapping_Invalid_QueryView(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView", new object[] { p0, p1 });
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00005604 File Offset: 0x00003804
		internal static string Mapping_Invalid_QueryView_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView_2", new object[] { p0, p1 });
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000561E File Offset: 0x0000381E
		internal static string Mapping_Invalid_QueryView_Type_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_QueryView_Type_1", new object[] { p0 });
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00005634 File Offset: 0x00003834
		internal static string Mapping_TypeName_For_First_QueryView
		{
			get
			{
				return EntityRes.GetString("Mapping_TypeName_For_First_QueryView");
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00005640 File Offset: 0x00003840
		internal static string Mapping_AllQueryViewAtCompileTime(object p0)
		{
			return EntityRes.GetString("Mapping_AllQueryViewAtCompileTime", new object[] { p0 });
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00005656 File Offset: 0x00003856
		internal static string Mapping_QueryViewMultipleTypeInTypeName(object p0)
		{
			return EntityRes.GetString("Mapping_QueryViewMultipleTypeInTypeName", new object[] { p0 });
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000566C File Offset: 0x0000386C
		internal static string Mapping_QueryView_Duplicate_OfType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_Duplicate_OfType", new object[] { p0, p1 });
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00005686 File Offset: 0x00003886
		internal static string Mapping_QueryView_Duplicate_OfTypeOnly(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_Duplicate_OfTypeOnly", new object[] { p0, p1 });
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000056A0 File Offset: 0x000038A0
		internal static string Mapping_QueryView_TypeName_Not_Defined(object p0)
		{
			return EntityRes.GetString("Mapping_QueryView_TypeName_Not_Defined", new object[] { p0 });
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000056B6 File Offset: 0x000038B6
		internal static string Mapping_QueryView_For_Base_Type(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_QueryView_For_Base_Type", new object[] { p0, p1 });
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000056D0 File Offset: 0x000038D0
		internal static string Mapping_UnsupportedExpressionKind_QueryView_2(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_UnsupportedExpressionKind_QueryView_2", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000056EE File Offset: 0x000038EE
		internal static string Mapping_UnsupportedScanTarget_QueryView_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedScanTarget_QueryView_2", new object[] { p0, p1 });
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005708 File Offset: 0x00003908
		internal static string Mapping_UnsupportedPropertyKind_QueryView_3(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_UnsupportedPropertyKind_QueryView_3", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00005726 File Offset: 0x00003926
		internal static string Mapping_UnsupportedInitialization_QueryView_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_UnsupportedInitialization_QueryView_2", new object[] { p0, p1 });
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00005740 File Offset: 0x00003940
		internal static string Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView_4(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView_4", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00005762 File Offset: 0x00003962
		internal static string Mapping_Invalid_Query_Views_MissingSetClosure_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Query_Views_MissingSetClosure_1", new object[] { p0 });
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00005778 File Offset: 0x00003978
		internal static string Generated_View_Type_Super_Class_1(object p0)
		{
			return EntityRes.GetString("Generated_View_Type_Super_Class_1", new object[] { p0 });
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000578E File Offset: 0x0000398E
		internal static string Generated_Views_Changed
		{
			get
			{
				return EntityRes.GetString("Generated_Views_Changed");
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000579A File Offset: 0x0000399A
		internal static string Generated_Views_Invalid_Extent_1(object p0)
		{
			return EntityRes.GetString("Generated_Views_Invalid_Extent_1", new object[] { p0 });
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000057B0 File Offset: 0x000039B0
		internal static string Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace(object p0)
		{
			return EntityRes.GetString("Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace", new object[] { p0 });
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000057C6 File Offset: 0x000039C6
		internal static string Mapping_AbstractTypeMappingToNonAbstractType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_AbstractTypeMappingToNonAbstractType", new object[] { p0, p1 });
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000057E0 File Offset: 0x000039E0
		internal static string StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping(object p0, object p1, object p2)
		{
			return EntityRes.GetString("StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping", new object[] { p0, p1, p2 });
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000057FE File Offset: 0x000039FE
		internal static string Mapping_InvalidContent_IsTypeOfNotTerminated
		{
			get
			{
				return EntityRes.GetString("Mapping_InvalidContent_IsTypeOfNotTerminated");
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000580A File Offset: 0x00003A0A
		internal static string Mapping_CannotMapCLRTypeMultipleTimes(object p0)
		{
			return EntityRes.GetString("Mapping_CannotMapCLRTypeMultipleTimes", new object[] { p0 });
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00005820 File Offset: 0x00003A20
		internal static string Mapping_Invalid_Function_Mapping_In_Table_Context_0
		{
			get
			{
				return EntityRes.GetString("Mapping_Invalid_Function_Mapping_In_Table_Context_0");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000582C File Offset: 0x00003A2C
		internal static string Mapping_Invalid_Function_Mapping_Multiple_Types_0
		{
			get
			{
				return EntityRes.GetString("Mapping_Invalid_Function_Mapping_Multiple_Types_0");
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00005838 File Offset: 0x00003A38
		internal static string Mapping_Invalid_Function_Mapping_UnknownFunction_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_UnknownFunction_1", new object[] { p0 });
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000584E File Offset: 0x00003A4E
		internal static string Mapping_Invalid_Function_Mapping_AmbiguousFunction_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AmbiguousFunction_1", new object[] { p0 });
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00005864 File Offset: 0x00003A64
		internal static string Mapping_Invalid_Function_Mapping_NotValidFunction_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_NotValidFunction_1", new object[] { p0 });
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000587A File Offset: 0x00003A7A
		internal static string Mapping_Invalid_Function_Mapping_NotValidFunctionParameter_3(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_NotValidFunctionParameter_3", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00005898 File Offset: 0x00003A98
		internal static string Mapping_Invalid_Function_Mapping_MissingParameter_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_MissingParameter_2", new object[] { p0, p1 });
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000058B2 File Offset: 0x00003AB2
		internal static string Mapping_Invalid_Function_Mapping_AssociationSetDoesNotExist_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AssociationSetDoesNotExist_1", new object[] { p0 });
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000058C8 File Offset: 0x00003AC8
		internal static string Mapping_Invalid_Function_Mapping_AssociationSetRoleDoesNotExist_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AssociationSetRoleDoesNotExist_1", new object[] { p0 });
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000058DE File Offset: 0x00003ADE
		internal static string Mapping_Invalid_Function_Mapping_AssociationSetFromRoleIsNotEntitySet_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AssociationSetFromRoleIsNotEntitySet_1", new object[] { p0 });
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000058F4 File Offset: 0x00003AF4
		internal static string Mapping_Invalid_Function_Mapping_AssociationSetCardinality_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AssociationSetCardinality_1", new object[] { p0 });
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000590A File Offset: 0x00003B0A
		internal static string Mapping_Invalid_Function_Mapping_ComplexTypeNotFound_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_ComplexTypeNotFound_1", new object[] { p0 });
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00005920 File Offset: 0x00003B20
		internal static string Mapping_Invalid_Function_Mapping_WrongComplexType_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_WrongComplexType_2", new object[] { p0, p1 });
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000593A File Offset: 0x00003B3A
		internal static string Mapping_Invalid_Function_Mapping_MissingVersion_0
		{
			get
			{
				return EntityRes.GetString("Mapping_Invalid_Function_Mapping_MissingVersion_0");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00005946 File Offset: 0x00003B46
		internal static string Mapping_Invalid_Function_Mapping_VersionMustBeOriginal_0
		{
			get
			{
				return EntityRes.GetString("Mapping_Invalid_Function_Mapping_VersionMustBeOriginal_0");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00005952 File Offset: 0x00003B52
		internal static string Mapping_Invalid_Function_Mapping_VersionMustBeCurrent_0
		{
			get
			{
				return EntityRes.GetString("Mapping_Invalid_Function_Mapping_VersionMustBeCurrent_0");
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000595E File Offset: 0x00003B5E
		internal static string Mapping_Invalid_Function_Mapping_ParameterNotFound_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_ParameterNotFound_2", new object[] { p0, p1 });
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00005978 File Offset: 0x00003B78
		internal static string Mapping_Invalid_Function_Mapping_PropertyNotFound_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_PropertyNotFound_2", new object[] { p0, p1 });
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00005992 File Offset: 0x00003B92
		internal static string Mapping_Invalid_Function_Mapping_PropertyNotKey_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_PropertyNotKey_2", new object[] { p0, p1 });
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000059AC File Offset: 0x00003BAC
		internal static string Mapping_Invalid_Function_Mapping_ParameterBoundTwice_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_ParameterBoundTwice_1", new object[] { p0 });
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000059C2 File Offset: 0x00003BC2
		internal static string Mapping_Invalid_Function_Mapping_RedundantEntityTypeMapping_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_RedundantEntityTypeMapping_1", new object[] { p0 });
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000059D8 File Offset: 0x00003BD8
		internal static string Mapping_Invalid_Function_Mapping_MissingSetClosure_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_MissingSetClosure_1", new object[] { p0 });
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000059EE File Offset: 0x00003BEE
		internal static string Mapping_Invalid_Function_Mapping_MissingEntityType_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_MissingEntityType_1", new object[] { p0 });
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00005A04 File Offset: 0x00003C04
		internal static string Mapping_Invalid_Function_Mapping_PropertyParameterTypeMismatch_6(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_PropertyParameterTypeMismatch_6", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00005A30 File Offset: 0x00003C30
		internal static string Mapping_Invalid_Function_Mapping_AssociationSetAmbiguous_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AssociationSetAmbiguous_1", new object[] { p0 });
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00005A46 File Offset: 0x00003C46
		internal static string Mapping_Invalid_Function_Mapping_MultipleEndsOfAssociationMapped_3(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_MultipleEndsOfAssociationMapped_3", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00005A64 File Offset: 0x00003C64
		internal static string Mapping_Invalid_Function_Mapping_AmbiguousResultBinding_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AmbiguousResultBinding_2", new object[] { p0, p1 });
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00005A7E File Offset: 0x00003C7E
		internal static string Mapping_Invalid_Function_Mapping_AssociationSetNotMappedForOperation_4(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AssociationSetNotMappedForOperation_4", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00005AA0 File Offset: 0x00003CA0
		internal static string Mapping_Invalid_Function_Mapping_AssociationEndMappingInvalidForEntityType_3(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AssociationEndMappingInvalidForEntityType_3", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00005ABE File Offset: 0x00003CBE
		internal static string Mapping_Invalid_Function_Mapping_AssociationEndMappingForeignKeyAssociation_1(object p0)
		{
			return EntityRes.GetString("Mapping_Invalid_Function_Mapping_AssociationEndMappingForeignKeyAssociation_1", new object[] { p0 });
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00005AD4 File Offset: 0x00003CD4
		internal static string Mapping_StoreTypeMismatch_ScalarPropertyMapping_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_StoreTypeMismatch_ScalarPropertyMapping_2", new object[] { p0, p1 });
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00005AEE File Offset: 0x00003CEE
		internal static string Mapping_DistinctFlagInReadWriteContainer
		{
			get
			{
				return EntityRes.GetString("Mapping_DistinctFlagInReadWriteContainer");
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00005AFA File Offset: 0x00003CFA
		internal static string Mapping_FunctionImport_StoreFunctionDoesNotExist(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_StoreFunctionDoesNotExist", new object[] { p0 });
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00005B10 File Offset: 0x00003D10
		internal static string Mapping_FunctionImport_StoreFunctionAmbiguous(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_StoreFunctionAmbiguous", new object[] { p0 });
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00005B26 File Offset: 0x00003D26
		internal static string Mapping_FunctionImport_FunctionImportDoesNotExist(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionImportDoesNotExist", new object[] { p0, p1 });
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00005B40 File Offset: 0x00003D40
		internal static string Mapping_FunctionImport_FunctionImportMappedMultipleTimes(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_FunctionImportMappedMultipleTimes", new object[] { p0 });
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00005B56 File Offset: 0x00003D56
		internal static string Mapping_FunctionImport_TargetFunctionMustBeComposable(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetFunctionMustBeComposable", new object[] { p0 });
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00005B6C File Offset: 0x00003D6C
		internal static string Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter", new object[] { p0 });
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00005B82 File Offset: 0x00003D82
		internal static string Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter", new object[] { p0 });
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00005B98 File Offset: 0x00003D98
		internal static string Mapping_FunctionImport_IncompatibleParameterMode(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleParameterMode", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00005BB6 File Offset: 0x00003DB6
		internal static string Mapping_FunctionImport_IncompatibleParameterType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_IncompatibleParameterType", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00005BD4 File Offset: 0x00003DD4
		internal static string Mapping_ProviderReturnsNullType(object p0)
		{
			return EntityRes.GetString("Mapping_ProviderReturnsNullType", new object[] { p0 });
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00005BEA File Offset: 0x00003DEA
		internal static string Mapping_FunctionImport_RowsAffectedParameterDoesNotExist_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterDoesNotExist_2", new object[] { p0, p1 });
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00005C04 File Offset: 0x00003E04
		internal static string Mapping_FunctionImport_RowsAffectedParameterHasWrongType_2(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterHasWrongType_2", new object[] { p0, p1 });
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00005C1E File Offset: 0x00003E1E
		internal static string Mapping_FunctionImport_RowsAffectedParameterHasWrongMode_4(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_RowsAffectedParameterHasWrongMode_4", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00005C40 File Offset: 0x00003E40
		internal static string Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet", new object[] { p0, p1 });
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00005C5A File Offset: 0x00003E5A
		internal static string Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00005C7C File Offset: 0x00003E7C
		internal static string Mapping_FunctionImport_MultipleConditionsOnSingleColumn(object p0)
		{
			return EntityRes.GetString("Mapping_FunctionImport_MultipleConditionsOnSingleColumn", new object[] { p0 });
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00005C92 File Offset: 0x00003E92
		internal static string Mapping_FunctionImport_UnreachableType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnreachableType", new object[] { p0, p1 });
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00005CAC File Offset: 0x00003EAC
		internal static string Mapping_FunctionImport_UnreachableIsTypeOf(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnreachableIsTypeOf", new object[] { p0, p1 });
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00005CC6 File Offset: 0x00003EC6
		internal static string Mapping_FunctionImport_ConditionValueTypeMismatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_ConditionValueTypeMismatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00005CE4 File Offset: 0x00003EE4
		internal static string Mapping_FunctionImport_UnsupportedType(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_UnsupportedType", new object[] { p0, p1 });
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00005CFE File Offset: 0x00003EFE
		internal static string Mapping_FunctionImport_InvalidComplexTypeName(object p0, object p1)
		{
			return EntityRes.GetString("Mapping_FunctionImport_InvalidComplexTypeName", new object[] { p0, p1 });
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00005D18 File Offset: 0x00003F18
		internal static string Mapping_FunctionImport_DuplicateMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_DuplicateMemberName", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00005D36 File Offset: 0x00003F36
		internal static string Mapping_FunctionImport_InvalidMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Mapping_FunctionImport_InvalidMemberName", new object[] { p0, p1, p2 });
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00005D54 File Offset: 0x00003F54
		internal static string Mapping_DifferentEdmStoreVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_DifferentEdmStoreVersion");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00005D60 File Offset: 0x00003F60
		internal static string Mapping_DifferentMappingEdmStoreVersion
		{
			get
			{
				return EntityRes.GetString("Mapping_DifferentMappingEdmStoreVersion");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00005D6C File Offset: 0x00003F6C
		internal static string SqlProvider_DdlGeneration_MissingInitialCatalog
		{
			get
			{
				return EntityRes.GetString("SqlProvider_DdlGeneration_MissingInitialCatalog");
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00005D78 File Offset: 0x00003F78
		internal static string SqlProvider_CredentialsMissingForMasterConnection
		{
			get
			{
				return EntityRes.GetString("SqlProvider_CredentialsMissingForMasterConnection");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00005D84 File Offset: 0x00003F84
		internal static string SqlProvider_IncompleteCreateDatabase
		{
			get
			{
				return EntityRes.GetString("SqlProvider_IncompleteCreateDatabase");
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00005D90 File Offset: 0x00003F90
		internal static string Entity_EntityCantHaveMultipleChangeTrackers
		{
			get
			{
				return EntityRes.GetString("Entity_EntityCantHaveMultipleChangeTrackers");
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00005D9C File Offset: 0x00003F9C
		internal static string ComplexObject_NullableComplexTypesNotSupported(object p0)
		{
			return EntityRes.GetString("ComplexObject_NullableComplexTypesNotSupported", new object[] { p0 });
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00005DB2 File Offset: 0x00003FB2
		internal static string ComplexObject_ComplexObjectAlreadyAttachedToParent
		{
			get
			{
				return EntityRes.GetString("ComplexObject_ComplexObjectAlreadyAttachedToParent");
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00005DBE File Offset: 0x00003FBE
		internal static string ComplexObject_ComplexChangeRequestedOnScalarProperty(object p0)
		{
			return EntityRes.GetString("ComplexObject_ComplexChangeRequestedOnScalarProperty", new object[] { p0 });
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00005DD4 File Offset: 0x00003FD4
		internal static string ObjectStateEntry_SetModifiedOnInvalidProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetModifiedOnInvalidProperty", new object[] { p0 });
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00005DEA File Offset: 0x00003FEA
		internal static string ObjectStateEntry_OriginalValuesDoesNotExist
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_OriginalValuesDoesNotExist");
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00005DF6 File Offset: 0x00003FF6
		internal static string ObjectStateEntry_CurrentValuesDoesNotExist
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CurrentValuesDoesNotExist");
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00005E02 File Offset: 0x00004002
		internal static string ObjectStateEntry_InvalidState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_InvalidState");
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00005E0E File Offset: 0x0000400E
		internal static string ObjectStateEntry_CannotModifyKeyProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_CannotModifyKeyProperty", new object[] { p0 });
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00005E24 File Offset: 0x00004024
		internal static string ObjectStateEntry_CantModifyRelationValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyRelationValues");
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00005E30 File Offset: 0x00004030
		internal static string ObjectStateEntry_CantModifyRelationState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyRelationState");
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00005E3C File Offset: 0x0000403C
		internal static string ObjectStateEntry_CantModifyDetachedDeletedEntries
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantModifyDetachedDeletedEntries");
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00005E48 File Offset: 0x00004048
		internal static string ObjectStateEntry_SetModifiedStates
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_SetModifiedStates");
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00005E54 File Offset: 0x00004054
		internal static string ObjectStateEntry_CantSetEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CantSetEntityKey");
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00005E60 File Offset: 0x00004060
		internal static string ObjectStateEntry_CannotAccessKeyEntryValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotAccessKeyEntryValues");
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00005E6C File Offset: 0x0000406C
		internal static string ObjectStateEntry_CannotModifyKeyEntryState
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotModifyKeyEntryState");
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00005E78 File Offset: 0x00004078
		internal static string ObjectStateEntry_CannotDeleteOnKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_CannotDeleteOnKeyEntry");
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00005E84 File Offset: 0x00004084
		internal static string ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging");
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00005E90 File Offset: 0x00004090
		internal static string ObjectStateEntry_ChangeOnUnmappedProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangeOnUnmappedProperty", new object[] { p0 });
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00005EA6 File Offset: 0x000040A6
		internal static string ObjectStateEntry_ChangeOnUnmappedComplexProperty(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangeOnUnmappedComplexProperty", new object[] { p0 });
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00005EBC File Offset: 0x000040BC
		internal static string ObjectStateEntry_ChangedInDifferentStateFromChanging(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_ChangedInDifferentStateFromChanging", new object[] { p0, p1 });
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00005ED6 File Offset: 0x000040D6
		internal static string ObjectStateEntry_UnableToEnumerateCollection(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_UnableToEnumerateCollection", new object[] { p0, p1 });
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00005EF0 File Offset: 0x000040F0
		internal static string ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_RelationshipAndKeyEntriesDoNotHaveRelationshipManagers");
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00005EFC File Offset: 0x000040FC
		internal static string ObjectStateEntry_InvalidTypeForComplexTypeProperty
		{
			get
			{
				return EntityRes.GetString("ObjectStateEntry_InvalidTypeForComplexTypeProperty");
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00005F08 File Offset: 0x00004108
		internal static string ObjectStateEntry_ComplexObjectUsedMultipleTimes(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateEntry_ComplexObjectUsedMultipleTimes", new object[] { p0, p1 });
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00005F22 File Offset: 0x00004122
		internal static string ObjectStateEntry_SetOriginalComplexProperties(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetOriginalComplexProperties", new object[] { p0 });
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00005F38 File Offset: 0x00004138
		internal static string ObjectStateEntry_NullOriginalValueForNonNullableProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectStateEntry_NullOriginalValueForNonNullableProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00005F56 File Offset: 0x00004156
		internal static string ObjectStateEntry_SetOriginalPrimaryKey(object p0)
		{
			return EntityRes.GetString("ObjectStateEntry_SetOriginalPrimaryKey", new object[] { p0 });
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00005F6C File Offset: 0x0000416C
		internal static string ObjectStateManager_NoEntryExistForEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_NoEntryExistForEntityKey");
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00005F78 File Offset: 0x00004178
		internal static string ObjectStateManager_NoEntryExistsForObject(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_NoEntryExistsForObject", new object[] { p0 });
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00005F8E File Offset: 0x0000418E
		internal static string ObjectStateManager_EntityNotTracked
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_EntityNotTracked");
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00005F9A File Offset: 0x0000419A
		internal static string ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager");
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00005FA6 File Offset: 0x000041A6
		internal static string ObjectStateManager_ObjectStateManagerContainsThisEntityKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_ObjectStateManagerContainsThisEntityKey");
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00005FB2 File Offset: 0x000041B2
		internal static string ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity", new object[] { p0 });
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00005FC8 File Offset: 0x000041C8
		internal static string ObjectStateManager_CannotFixUpKeyToExistingValues
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotFixUpKeyToExistingValues");
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00005FD4 File Offset: 0x000041D4
		internal static string ObjectStateManager_KeyPropertyDoesntMatchValueInKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_KeyPropertyDoesntMatchValueInKey");
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00005FE0 File Offset: 0x000041E0
		internal static string ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach");
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00005FEC File Offset: 0x000041EC
		internal static string ObjectStateManager_InvalidKey
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_InvalidKey");
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00005FF8 File Offset: 0x000041F8
		internal static string ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType", new object[] { p0, p1 });
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00006012 File Offset: 0x00004212
		internal static string ObjectStateManager_GetEntityKeyRequiresObjectToHaveAKey(object p0)
		{
			return EntityRes.GetString("ObjectStateManager_GetEntityKeyRequiresObjectToHaveAKey", new object[] { p0 });
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00006028 File Offset: 0x00004228
		internal static string ObjectStateManager_AcceptChangesEntityKeyIsNotValid
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_AcceptChangesEntityKeyIsNotValid");
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00006034 File Offset: 0x00004234
		internal static string ObjectStateManager_EntityConflictsWithKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_EntityConflictsWithKeyEntry");
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00006040 File Offset: 0x00004240
		internal static string ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotGetRelationshipManagerForDetachedPocoEntity");
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000604C File Offset: 0x0000424C
		internal static string ObjectStateManager_CannotChangeRelationshipStateEntityDeleted
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateEntityDeleted");
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00006058 File Offset: 0x00004258
		internal static string ObjectStateManager_CannotChangeRelationshipStateEntityAdded
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateEntityAdded");
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00006064 File Offset: 0x00004264
		internal static string ObjectStateManager_CannotChangeRelationshipStateKeyEntry
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_CannotChangeRelationshipStateKeyEntry");
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00006070 File Offset: 0x00004270
		internal static string ObjectStateManager_ConflictingChangesOfRelationshipDetected(object p0, object p1)
		{
			return EntityRes.GetString("ObjectStateManager_ConflictingChangesOfRelationshipDetected", new object[] { p0, p1 });
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000608A File Offset: 0x0000428A
		internal static string ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations
		{
			get
			{
				return EntityRes.GetString("ObjectStateManager_ChangeRelationshipStateNotSupportedForForeignKeyAssociations");
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00006096 File Offset: 0x00004296
		internal static string ObjectContext_ClientEntityRemovedFromStore(object p0)
		{
			return EntityRes.GetString("ObjectContext_ClientEntityRemovedFromStore", new object[] { p0 });
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x000060AC File Offset: 0x000042AC
		internal static string ObjectContext_StoreEntityNotPresentInClient
		{
			get
			{
				return EntityRes.GetString("ObjectContext_StoreEntityNotPresentInClient");
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000060B8 File Offset: 0x000042B8
		internal static string ObjectContext_InvalidConnectionString
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidConnectionString");
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000060C4 File Offset: 0x000042C4
		internal static string ObjectContext_InvalidConnection
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidConnection");
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000060D0 File Offset: 0x000042D0
		internal static string ObjectContext_InvalidDataAdapter
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidDataAdapter");
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000060DC File Offset: 0x000042DC
		internal static string ObjectContext_InvalidDefaultContainerName(object p0)
		{
			return EntityRes.GetString("ObjectContext_InvalidDefaultContainerName", new object[] { p0 });
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000060F2 File Offset: 0x000042F2
		internal static string ObjectContext_NthElementInAddedState(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementInAddedState", new object[] { p0 });
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00006108 File Offset: 0x00004308
		internal static string ObjectContext_NthElementIsDuplicate(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementIsDuplicate", new object[] { p0 });
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000611E File Offset: 0x0000431E
		internal static string ObjectContext_NthElementIsNull(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementIsNull", new object[] { p0 });
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00006134 File Offset: 0x00004334
		internal static string ObjectContext_NthElementNotInObjectStateManager(object p0)
		{
			return EntityRes.GetString("ObjectContext_NthElementNotInObjectStateManager", new object[] { p0 });
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000614A File Offset: 0x0000434A
		internal static string ObjectContext_ObjectNotFound
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ObjectNotFound");
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00006156 File Offset: 0x00004356
		internal static string ObjectContext_CannotDeleteEntityNotInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotDeleteEntityNotInObjectStateManager");
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00006162 File Offset: 0x00004362
		internal static string ObjectContext_CannotDetachEntityNotInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotDetachEntityNotInObjectStateManager");
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000616E File Offset: 0x0000436E
		internal static string ObjectContext_EntitySetNotFoundForName(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntitySetNotFoundForName", new object[] { p0 });
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00006184 File Offset: 0x00004384
		internal static string ObjectContext_EntityContainerNotFoundForName(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityContainerNotFoundForName", new object[] { p0 });
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000619A File Offset: 0x0000439A
		internal static string ObjectContext_InvalidCommandTimeout
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidCommandTimeout");
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000061A6 File Offset: 0x000043A6
		internal static string ObjectContext_NoMappingForEntityType(object p0)
		{
			return EntityRes.GetString("ObjectContext_NoMappingForEntityType", new object[] { p0 });
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000061BC File Offset: 0x000043BC
		internal static string ObjectContext_EntityAlreadyExistsInObjectStateManager
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntityAlreadyExistsInObjectStateManager");
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000061C8 File Offset: 0x000043C8
		internal static string ObjectContext_InvalidEntitySetInKey(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetInKey", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000061EA File Offset: 0x000043EA
		internal static string ObjectContext_CannotAttachEntityWithoutKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotAttachEntityWithoutKey");
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000061F6 File Offset: 0x000043F6
		internal static string ObjectContext_CannotAttachEntityWithTemporaryKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotAttachEntityWithTemporaryKey");
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00006202 File Offset: 0x00004402
		internal static string ObjectContext_EntitySetNameOrEntityKeyRequired
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntitySetNameOrEntityKeyRequired");
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000620E File Offset: 0x0000440E
		internal static string ObjectContext_ExecuteFunctionTypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionTypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00006228 File Offset: 0x00004428
		internal static string ObjectContext_ExecuteFunctionCalledWithScalarFunction(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithScalarFunction", new object[] { p0, p1 });
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00006242 File Offset: 0x00004442
		internal static string ObjectContext_ExecuteFunctionCalledWithNonQueryFunction(object p0)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithNonQueryFunction", new object[] { p0 });
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00006258 File Offset: 0x00004458
		internal static string ObjectContext_ExecuteFunctionCalledWithNullParameter(object p0)
		{
			return EntityRes.GetString("ObjectContext_ExecuteFunctionCalledWithNullParameter", new object[] { p0 });
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000626E File Offset: 0x0000446E
		internal static string ObjectContext_ContainerQualifiedEntitySetNameRequired
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ContainerQualifiedEntitySetNameRequired");
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000627A File Offset: 0x0000447A
		internal static string ObjectContext_CannotSetDefaultContainerName
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CannotSetDefaultContainerName");
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00006286 File Offset: 0x00004486
		internal static string ObjectContext_QualfiedEntitySetName
		{
			get
			{
				return EntityRes.GetString("ObjectContext_QualfiedEntitySetName");
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00006292 File Offset: 0x00004492
		internal static string ObjectContext_EntitiesHaveDifferentType(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_EntitiesHaveDifferentType", new object[] { p0, p1 });
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000062AC File Offset: 0x000044AC
		internal static string ObjectContext_EntityMustBeUnchangedOrModified(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityMustBeUnchangedOrModified", new object[] { p0 });
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000062C2 File Offset: 0x000044C2
		internal static string ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted(object p0)
		{
			return EntityRes.GetString("ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted", new object[] { p0 });
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000062D8 File Offset: 0x000044D8
		internal static string ObjectContext_AcceptAllChangesFailure(object p0)
		{
			return EntityRes.GetString("ObjectContext_AcceptAllChangesFailure", new object[] { p0 });
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000062EE File Offset: 0x000044EE
		internal static string ObjectContext_CommitWithConceptualNull
		{
			get
			{
				return EntityRes.GetString("ObjectContext_CommitWithConceptualNull");
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000062FA File Offset: 0x000044FA
		internal static string ObjectContext_InvalidEntitySetOnEntity(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetOnEntity", new object[] { p0, p1 });
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00006314 File Offset: 0x00004514
		internal static string ObjectContext_InvalidObjectSetTypeForEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectContext_InvalidObjectSetTypeForEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00006332 File Offset: 0x00004532
		internal static string ObjectContext_RequiredMetadataNotAvailble
		{
			get
			{
				return EntityRes.GetString("ObjectContext_RequiredMetadataNotAvailble");
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000633E File Offset: 0x0000453E
		internal static string ObjectContext_MetadataHasChanged
		{
			get
			{
				return EntityRes.GetString("ObjectContext_MetadataHasChanged");
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000634A File Offset: 0x0000454A
		internal static string ObjectContext_InvalidEntitySetInKeyFromName(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetInKeyFromName", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00006371 File Offset: 0x00004571
		internal static string ObjectContext_ObjectDisposed
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ObjectDisposed");
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000637D File Offset: 0x0000457D
		internal static string ObjectContext_CannotExplicitlyLoadDetachedRelationships(object p0)
		{
			return EntityRes.GetString("ObjectContext_CannotExplicitlyLoadDetachedRelationships", new object[] { p0 });
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00006393 File Offset: 0x00004593
		internal static string ObjectContext_CannotLoadReferencesUsingDifferentContext(object p0)
		{
			return EntityRes.GetString("ObjectContext_CannotLoadReferencesUsingDifferentContext", new object[] { p0 });
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000063A9 File Offset: 0x000045A9
		internal static string ObjectContext_SelectorExpressionMustBeMemberAccess
		{
			get
			{
				return EntityRes.GetString("ObjectContext_SelectorExpressionMustBeMemberAccess");
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000063B5 File Offset: 0x000045B5
		internal static string ObjectContext_MultipleEntitySetsFoundInSingleContainer(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_MultipleEntitySetsFoundInSingleContainer", new object[] { p0, p1 });
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000063CF File Offset: 0x000045CF
		internal static string ObjectContext_MultipleEntitySetsFoundInAllContainers(object p0)
		{
			return EntityRes.GetString("ObjectContext_MultipleEntitySetsFoundInAllContainers", new object[] { p0 });
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000063E5 File Offset: 0x000045E5
		internal static string ObjectContext_NoEntitySetFoundForType(object p0)
		{
			return EntityRes.GetString("ObjectContext_NoEntitySetFoundForType", new object[] { p0 });
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000063FB File Offset: 0x000045FB
		internal static string ObjectContext_EntityNotInObjectSet_Delete(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_EntityNotInObjectSet_Delete", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000641D File Offset: 0x0000461D
		internal static string ObjectContext_EntityNotInObjectSet_Detach(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ObjectContext_EntityNotInObjectSet_Detach", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000643F File Offset: 0x0000463F
		internal static string ObjectContext_InvalidEntityState
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidEntityState");
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000644B File Offset: 0x0000464B
		internal static string ObjectContext_InvalidRelationshipState
		{
			get
			{
				return EntityRes.GetString("ObjectContext_InvalidRelationshipState");
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00006457 File Offset: 0x00004657
		internal static string ObjectContext_EntityNotTrackedOrHasTempKey
		{
			get
			{
				return EntityRes.GetString("ObjectContext_EntityNotTrackedOrHasTempKey");
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00006463 File Offset: 0x00004663
		internal static string ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues
		{
			get
			{
				return EntityRes.GetString("ObjectContext_ExecuteCommandWithMixOfDbParameterAndValues");
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000646F File Offset: 0x0000466F
		internal static string ObjectContext_InvalidEntitySetForStoreQuery(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ObjectContext_InvalidEntitySetForStoreQuery", new object[] { p0, p1, p2 });
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000648D File Offset: 0x0000468D
		internal static string ObjectContext_InvalidTypeForStoreQuery(object p0)
		{
			return EntityRes.GetString("ObjectContext_InvalidTypeForStoreQuery", new object[] { p0 });
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000064A3 File Offset: 0x000046A3
		internal static string ObjectContext_TwoPropertiesMappedToSameColumn(object p0, object p1)
		{
			return EntityRes.GetString("ObjectContext_TwoPropertiesMappedToSameColumn", new object[] { p0, p1 });
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000064BD File Offset: 0x000046BD
		internal static string RelatedEnd_InvalidOwnerStateForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidOwnerStateForAttach");
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000064C9 File Offset: 0x000046C9
		internal static string RelatedEnd_InvalidNthElementNullForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementNullForAttach", new object[] { p0 });
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000064DF File Offset: 0x000046DF
		internal static string RelatedEnd_InvalidNthElementContextForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementContextForAttach", new object[] { p0 });
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000064F5 File Offset: 0x000046F5
		internal static string RelatedEnd_InvalidNthElementStateForAttach(object p0)
		{
			return EntityRes.GetString("RelatedEnd_InvalidNthElementStateForAttach", new object[] { p0 });
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000650B File Offset: 0x0000470B
		internal static string RelatedEnd_InvalidEntityContextForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidEntityContextForAttach");
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00006517 File Offset: 0x00004717
		internal static string RelatedEnd_InvalidEntityStateForAttach
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_InvalidEntityStateForAttach");
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00006523 File Offset: 0x00004723
		internal static string RelatedEnd_UnableToAddEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToAddEntity");
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000652F File Offset: 0x0000472F
		internal static string RelatedEnd_UnableToRemoveEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToRemoveEntity");
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000653B File Offset: 0x0000473B
		internal static string RelatedEnd_UnableToAddRelationshipWithDeletedEntity
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_UnableToAddRelationshipWithDeletedEntity");
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00006547 File Offset: 0x00004747
		internal static string RelatedEnd_ConflictingChangeOfRelationshipDetected
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_ConflictingChangeOfRelationshipDetected");
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00006553 File Offset: 0x00004753
		internal static string RelatedEnd_InvalidRelationshipFixupDetected(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidRelationshipFixupDetected", new object[] { p0, p1 });
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000656D File Offset: 0x0000476D
		internal static string RelatedEnd_CannotSerialize(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotSerialize", new object[] { p0 });
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00006583 File Offset: 0x00004783
		internal static string RelatedEnd_CannotAddToFixedSizeArray(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotAddToFixedSizeArray", new object[] { p0 });
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00006599 File Offset: 0x00004799
		internal static string RelatedEnd_CannotRemoveFromFixedSizeArray(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotRemoveFromFixedSizeArray", new object[] { p0 });
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000065AF File Offset: 0x000047AF
		internal static string Materializer_PropertyIsNotNullable
		{
			get
			{
				return EntityRes.GetString("Materializer_PropertyIsNotNullable");
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000065BB File Offset: 0x000047BB
		internal static string Materializer_PropertyIsNotNullableWithName(object p0)
		{
			return EntityRes.GetString("Materializer_PropertyIsNotNullableWithName", new object[] { p0 });
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000065D1 File Offset: 0x000047D1
		internal static string Materializer_SetInvalidValue(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Materializer_SetInvalidValue", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000065F3 File Offset: 0x000047F3
		internal static string Materializer_InvalidCastReference(object p0, object p1)
		{
			return EntityRes.GetString("Materializer_InvalidCastReference", new object[] { p0, p1 });
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000660D File Offset: 0x0000480D
		internal static string Materializer_InvalidCastNullable(object p0, object p1)
		{
			return EntityRes.GetString("Materializer_InvalidCastNullable", new object[] { p0, p1 });
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00006627 File Offset: 0x00004827
		internal static string Materializer_NullReferenceCast(object p0)
		{
			return EntityRes.GetString("Materializer_NullReferenceCast", new object[] { p0 });
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000663D File Offset: 0x0000483D
		internal static string Materializer_RecyclingEntity(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("Materializer_RecyclingEntity", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000665F File Offset: 0x0000485F
		internal static string Materializer_AddedEntityAlreadyExists(object p0)
		{
			return EntityRes.GetString("Materializer_AddedEntityAlreadyExists", new object[] { p0 });
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00006675 File Offset: 0x00004875
		internal static string Materializer_CannotReEnumerateQueryResults
		{
			get
			{
				return EntityRes.GetString("Materializer_CannotReEnumerateQueryResults");
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00006681 File Offset: 0x00004881
		internal static string Materializer_UnsupportedType
		{
			get
			{
				return EntityRes.GetString("Materializer_UnsupportedType");
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000668D File Offset: 0x0000488D
		internal static string Collections_NoRelationshipSetMatched(object p0)
		{
			return EntityRes.GetString("Collections_NoRelationshipSetMatched", new object[] { p0 });
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000066A3 File Offset: 0x000048A3
		internal static string Collections_ExpectedCollectionGotReference(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Collections_ExpectedCollectionGotReference", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000066C1 File Offset: 0x000048C1
		internal static string Collections_InvalidEntityStateSource
		{
			get
			{
				return EntityRes.GetString("Collections_InvalidEntityStateSource");
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000066CD File Offset: 0x000048CD
		internal static string Collections_InvalidEntityStateLoad(object p0)
		{
			return EntityRes.GetString("Collections_InvalidEntityStateLoad", new object[] { p0 });
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000066E3 File Offset: 0x000048E3
		internal static string Collections_CannotFillTryDifferentMergeOption(object p0, object p1)
		{
			return EntityRes.GetString("Collections_CannotFillTryDifferentMergeOption", new object[] { p0, p1 });
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000066FD File Offset: 0x000048FD
		internal static string Collections_UnableToMergeCollections
		{
			get
			{
				return EntityRes.GetString("Collections_UnableToMergeCollections");
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00006709 File Offset: 0x00004909
		internal static string EntityReference_ExpectedReferenceGotCollection(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityReference_ExpectedReferenceGotCollection", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00006727 File Offset: 0x00004927
		internal static string EntityReference_CannotAddMoreThanOneEntityToEntityReference(object p0, object p1)
		{
			return EntityRes.GetString("EntityReference_CannotAddMoreThanOneEntityToEntityReference", new object[] { p0, p1 });
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00006741 File Offset: 0x00004941
		internal static string EntityReference_LessThanExpectedRelatedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("EntityReference_LessThanExpectedRelatedEntitiesFound");
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000674D File Offset: 0x0000494D
		internal static string EntityReference_MoreThanExpectedRelatedEntitiesFound
		{
			get
			{
				return EntityRes.GetString("EntityReference_MoreThanExpectedRelatedEntitiesFound");
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00006759 File Offset: 0x00004959
		internal static string EntityReference_CannotChangeReferentialConstraintProperty
		{
			get
			{
				return EntityRes.GetString("EntityReference_CannotChangeReferentialConstraintProperty");
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00006765 File Offset: 0x00004965
		internal static string EntityReference_CannotSetSpecialKeys
		{
			get
			{
				return EntityRes.GetString("EntityReference_CannotSetSpecialKeys");
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00006771 File Offset: 0x00004971
		internal static string EntityReference_EntityKeyValueMismatch
		{
			get
			{
				return EntityRes.GetString("EntityReference_EntityKeyValueMismatch");
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000677D File Offset: 0x0000497D
		internal static string RelatedEnd_RelatedEndNotFound
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_RelatedEndNotFound");
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00006789 File Offset: 0x00004989
		internal static string RelatedEnd_RelatedEndNotAttachedToContext(object p0)
		{
			return EntityRes.GetString("RelatedEnd_RelatedEndNotAttachedToContext", new object[] { p0 });
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000679F File Offset: 0x0000499F
		internal static string RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd");
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000067AB File Offset: 0x000049AB
		internal static string RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd");
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000067B7 File Offset: 0x000049B7
		internal static string RelatedEnd_InvalidContainedType_Collection(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidContainedType_Collection", new object[] { p0, p1 });
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000067D1 File Offset: 0x000049D1
		internal static string RelatedEnd_InvalidContainedType_Reference(object p0, object p1)
		{
			return EntityRes.GetString("RelatedEnd_InvalidContainedType_Reference", new object[] { p0, p1 });
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000067EB File Offset: 0x000049EB
		internal static string RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(object p0)
		{
			return EntityRes.GetString("RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities", new object[] { p0 });
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00006801 File Offset: 0x00004A01
		internal static string RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts");
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000680D File Offset: 0x00004A0D
		internal static string RelatedEnd_MismatchedMergeOptionOnLoad(object p0)
		{
			return EntityRes.GetString("RelatedEnd_MismatchedMergeOptionOnLoad", new object[] { p0 });
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00006823 File Offset: 0x00004A23
		internal static string RelatedEnd_EntitySetIsNotValidForRelationship(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("RelatedEnd_EntitySetIsNotValidForRelationship", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000684A File Offset: 0x00004A4A
		internal static string RelatedEnd_OwnerIsNull
		{
			get
			{
				return EntityRes.GetString("RelatedEnd_OwnerIsNull");
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00006856 File Offset: 0x00004A56
		internal static string RelationshipManager_UnableToRetrieveReferentialConstraintProperties
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnableToRetrieveReferentialConstraintProperties");
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00006862 File Offset: 0x00004A62
		internal static string RelationshipManager_InconsistentReferentialConstraintProperties
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InconsistentReferentialConstraintProperties");
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000686E File Offset: 0x00004A6E
		internal static string RelationshipManager_CircularRelationshipsWithReferentialConstraints
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CircularRelationshipsWithReferentialConstraints");
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000687A File Offset: 0x00004A7A
		internal static string RelationshipManager_UnableToFindRelationshipTypeInMetadata(object p0)
		{
			return EntityRes.GetString("RelationshipManager_UnableToFindRelationshipTypeInMetadata", new object[] { p0 });
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00006890 File Offset: 0x00004A90
		internal static string RelationshipManager_InvalidTargetRole(object p0, object p1)
		{
			return EntityRes.GetString("RelationshipManager_InvalidTargetRole", new object[] { p0, p1 });
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600032D RID: 813 RVA: 0x000068AA File Offset: 0x00004AAA
		internal static string RelationshipManager_UnexpectedNull
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnexpectedNull");
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000068B6 File Offset: 0x00004AB6
		internal static string RelationshipManager_InvalidRelationshipManagerOwner
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InvalidRelationshipManagerOwner");
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000068C2 File Offset: 0x00004AC2
		internal static string RelationshipManager_OwnerIsNotSourceType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("RelationshipManager_OwnerIsNotSourceType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000068E4 File Offset: 0x00004AE4
		internal static string RelationshipManager_UnexpectedNullContext
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_UnexpectedNullContext");
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000068F0 File Offset: 0x00004AF0
		internal static string RelationshipManager_ReferenceAlreadyInitialized(object p0)
		{
			return EntityRes.GetString("RelationshipManager_ReferenceAlreadyInitialized", new object[] { p0 });
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00006906 File Offset: 0x00004B06
		internal static string RelationshipManager_RelationshipManagerAttached(object p0)
		{
			return EntityRes.GetString("RelationshipManager_RelationshipManagerAttached", new object[] { p0 });
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000691C File Offset: 0x00004B1C
		internal static string RelationshipManager_InitializeIsForDeserialization
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_InitializeIsForDeserialization");
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00006928 File Offset: 0x00004B28
		internal static string RelationshipManager_CollectionAlreadyInitialized(object p0)
		{
			return EntityRes.GetString("RelationshipManager_CollectionAlreadyInitialized", new object[] { p0 });
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000693E File Offset: 0x00004B3E
		internal static string RelationshipManager_CollectionRelationshipManagerAttached(object p0)
		{
			return EntityRes.GetString("RelationshipManager_CollectionRelationshipManagerAttached", new object[] { p0 });
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00006954 File Offset: 0x00004B54
		internal static string RelationshipManager_CollectionInitializeIsForDeserialization
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CollectionInitializeIsForDeserialization");
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00006960 File Offset: 0x00004B60
		internal static string RelationshipManager_NavigationPropertyNotFound(object p0)
		{
			return EntityRes.GetString("RelationshipManager_NavigationPropertyNotFound", new object[] { p0 });
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00006976 File Offset: 0x00004B76
		internal static string RelationshipManager_CannotGetRelatEndForDetachedPocoEntity
		{
			get
			{
				return EntityRes.GetString("RelationshipManager_CannotGetRelatEndForDetachedPocoEntity");
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00006982 File Offset: 0x00004B82
		internal static string ObjectView_CannotReplacetheEntityorRow
		{
			get
			{
				return EntityRes.GetString("ObjectView_CannotReplacetheEntityorRow");
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000698E File Offset: 0x00004B8E
		internal static string ObjectView_IndexBasedInsertIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ObjectView_IndexBasedInsertIsNotSupported");
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000699A File Offset: 0x00004B9A
		internal static string ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList
		{
			get
			{
				return EntityRes.GetString("ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList");
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000069A6 File Offset: 0x00004BA6
		internal static string ObjectView_AddNewOperationNotAllowedOnAbstractBindingList
		{
			get
			{
				return EntityRes.GetString("ObjectView_AddNewOperationNotAllowedOnAbstractBindingList");
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000069B2 File Offset: 0x00004BB2
		internal static string ObjectView_IncompatibleArgument
		{
			get
			{
				return EntityRes.GetString("ObjectView_IncompatibleArgument");
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000069BE File Offset: 0x00004BBE
		internal static string ObjectView_CannotResolveTheEntitySet(object p0)
		{
			return EntityRes.GetString("ObjectView_CannotResolveTheEntitySet", new object[] { p0 });
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000069D4 File Offset: 0x00004BD4
		internal static string CodeGen_ConstructorNoParameterless(object p0)
		{
			return EntityRes.GetString("CodeGen_ConstructorNoParameterless", new object[] { p0 });
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000340 RID: 832 RVA: 0x000069EA File Offset: 0x00004BEA
		internal static string CodeGen_PropertyDeclaringTypeIsValueType
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyDeclaringTypeIsValueType");
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000069F6 File Offset: 0x00004BF6
		internal static string CodeGen_PropertyStrongNameIdentity
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyStrongNameIdentity");
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00006A02 File Offset: 0x00004C02
		internal static string CodeGen_PropertyUnsupportedForm
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyUnsupportedForm");
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00006A0E File Offset: 0x00004C0E
		internal static string CodeGen_PropertyUnsupportedType
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyUnsupportedType");
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00006A1A File Offset: 0x00004C1A
		internal static string CodeGen_PropertyIsIndexed
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyIsIndexed");
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00006A26 File Offset: 0x00004C26
		internal static string CodeGen_PropertyIsStatic
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyIsStatic");
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00006A32 File Offset: 0x00004C32
		internal static string CodeGen_PropertyNoGetter
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyNoGetter");
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00006A3E File Offset: 0x00004C3E
		internal static string CodeGen_PropertyNoSetter
		{
			get
			{
				return EntityRes.GetString("CodeGen_PropertyNoSetter");
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00006A4A File Offset: 0x00004C4A
		internal static string PocoEntityWrapper_UnableToSetFieldOrProperty(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnableToSetFieldOrProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00006A64 File Offset: 0x00004C64
		internal static string PocoEntityWrapper_UnexpectedTypeForNavigationProperty(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnexpectedTypeForNavigationProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00006A7E File Offset: 0x00004C7E
		internal static string PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType(object p0, object p1)
		{
			return EntityRes.GetString("PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType", new object[] { p0, p1 });
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00006A98 File Offset: 0x00004C98
		internal static string GeneralQueryError
		{
			get
			{
				return EntityRes.GetString("GeneralQueryError");
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00006AA4 File Offset: 0x00004CA4
		internal static string CtxAlias
		{
			get
			{
				return EntityRes.GetString("CtxAlias");
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00006AB0 File Offset: 0x00004CB0
		internal static string CtxAliasedNamespaceImport
		{
			get
			{
				return EntityRes.GetString("CtxAliasedNamespaceImport");
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00006ABC File Offset: 0x00004CBC
		internal static string CtxAnd
		{
			get
			{
				return EntityRes.GetString("CtxAnd");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00006AC8 File Offset: 0x00004CC8
		internal static string CtxAnyElement
		{
			get
			{
				return EntityRes.GetString("CtxAnyElement");
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00006AD4 File Offset: 0x00004CD4
		internal static string CtxApplyClause
		{
			get
			{
				return EntityRes.GetString("CtxApplyClause");
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00006AE0 File Offset: 0x00004CE0
		internal static string CtxBetween
		{
			get
			{
				return EntityRes.GetString("CtxBetween");
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00006AEC File Offset: 0x00004CEC
		internal static string CtxCase
		{
			get
			{
				return EntityRes.GetString("CtxCase");
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00006AF8 File Offset: 0x00004CF8
		internal static string CtxCaseElse
		{
			get
			{
				return EntityRes.GetString("CtxCaseElse");
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00006B04 File Offset: 0x00004D04
		internal static string CtxCaseWhenThen
		{
			get
			{
				return EntityRes.GetString("CtxCaseWhenThen");
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00006B10 File Offset: 0x00004D10
		internal static string CtxCast
		{
			get
			{
				return EntityRes.GetString("CtxCast");
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00006B1C File Offset: 0x00004D1C
		internal static string CtxCollatedOrderByClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxCollatedOrderByClauseItem");
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00006B28 File Offset: 0x00004D28
		internal static string CtxCollectionTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxCollectionTypeDefinition");
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00006B34 File Offset: 0x00004D34
		internal static string CtxCommandExpression
		{
			get
			{
				return EntityRes.GetString("CtxCommandExpression");
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00006B40 File Offset: 0x00004D40
		internal static string CtxCreateRef
		{
			get
			{
				return EntityRes.GetString("CtxCreateRef");
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00006B4C File Offset: 0x00004D4C
		internal static string CtxDeref
		{
			get
			{
				return EntityRes.GetString("CtxDeref");
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00006B58 File Offset: 0x00004D58
		internal static string CtxDivide
		{
			get
			{
				return EntityRes.GetString("CtxDivide");
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00006B64 File Offset: 0x00004D64
		internal static string CtxElement
		{
			get
			{
				return EntityRes.GetString("CtxElement");
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00006B70 File Offset: 0x00004D70
		internal static string CtxEquals
		{
			get
			{
				return EntityRes.GetString("CtxEquals");
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00006B7C File Offset: 0x00004D7C
		internal static string CtxEscapedIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxEscapedIdentifier");
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00006B88 File Offset: 0x00004D88
		internal static string CtxExcept
		{
			get
			{
				return EntityRes.GetString("CtxExcept");
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00006B94 File Offset: 0x00004D94
		internal static string CtxExists
		{
			get
			{
				return EntityRes.GetString("CtxExists");
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00006BA0 File Offset: 0x00004DA0
		internal static string CtxExpressionList
		{
			get
			{
				return EntityRes.GetString("CtxExpressionList");
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00006BAC File Offset: 0x00004DAC
		internal static string CtxFlatten
		{
			get
			{
				return EntityRes.GetString("CtxFlatten");
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00006BB8 File Offset: 0x00004DB8
		internal static string CtxFromApplyClause
		{
			get
			{
				return EntityRes.GetString("CtxFromApplyClause");
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00006BC4 File Offset: 0x00004DC4
		internal static string CtxFromClause
		{
			get
			{
				return EntityRes.GetString("CtxFromClause");
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00006BD0 File Offset: 0x00004DD0
		internal static string CtxFromClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxFromClauseItem");
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00006BDC File Offset: 0x00004DDC
		internal static string CtxFromClauseList
		{
			get
			{
				return EntityRes.GetString("CtxFromClauseList");
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00006BE8 File Offset: 0x00004DE8
		internal static string CtxFromJoinClause
		{
			get
			{
				return EntityRes.GetString("CtxFromJoinClause");
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00006BF4 File Offset: 0x00004DF4
		internal static string CtxFunction(object p0)
		{
			return EntityRes.GetString("CtxFunction", new object[] { p0 });
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00006C0A File Offset: 0x00004E0A
		internal static string CtxFunctionDefinition
		{
			get
			{
				return EntityRes.GetString("CtxFunctionDefinition");
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00006C16 File Offset: 0x00004E16
		internal static string CtxGreaterThan
		{
			get
			{
				return EntityRes.GetString("CtxGreaterThan");
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00006C22 File Offset: 0x00004E22
		internal static string CtxGreaterThanEqual
		{
			get
			{
				return EntityRes.GetString("CtxGreaterThanEqual");
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00006C2E File Offset: 0x00004E2E
		internal static string CtxGroupByClause
		{
			get
			{
				return EntityRes.GetString("CtxGroupByClause");
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00006C3A File Offset: 0x00004E3A
		internal static string CtxGroupPartition
		{
			get
			{
				return EntityRes.GetString("CtxGroupPartition");
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00006C46 File Offset: 0x00004E46
		internal static string CtxHavingClause
		{
			get
			{
				return EntityRes.GetString("CtxHavingClause");
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00006C52 File Offset: 0x00004E52
		internal static string CtxIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxIdentifier");
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00006C5E File Offset: 0x00004E5E
		internal static string CtxIn
		{
			get
			{
				return EntityRes.GetString("CtxIn");
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00006C6A File Offset: 0x00004E6A
		internal static string CtxIntersect
		{
			get
			{
				return EntityRes.GetString("CtxIntersect");
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00006C76 File Offset: 0x00004E76
		internal static string CtxIsNotNull
		{
			get
			{
				return EntityRes.GetString("CtxIsNotNull");
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00006C82 File Offset: 0x00004E82
		internal static string CtxIsNotOf
		{
			get
			{
				return EntityRes.GetString("CtxIsNotOf");
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00006C8E File Offset: 0x00004E8E
		internal static string CtxIsNull
		{
			get
			{
				return EntityRes.GetString("CtxIsNull");
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00006C9A File Offset: 0x00004E9A
		internal static string CtxIsOf
		{
			get
			{
				return EntityRes.GetString("CtxIsOf");
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00006CA6 File Offset: 0x00004EA6
		internal static string CtxJoinClause
		{
			get
			{
				return EntityRes.GetString("CtxJoinClause");
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00006CB2 File Offset: 0x00004EB2
		internal static string CtxJoinOnClause
		{
			get
			{
				return EntityRes.GetString("CtxJoinOnClause");
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00006CBE File Offset: 0x00004EBE
		internal static string CtxKey
		{
			get
			{
				return EntityRes.GetString("CtxKey");
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00006CCA File Offset: 0x00004ECA
		internal static string CtxLessThan
		{
			get
			{
				return EntityRes.GetString("CtxLessThan");
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00006CD6 File Offset: 0x00004ED6
		internal static string CtxLessThanEqual
		{
			get
			{
				return EntityRes.GetString("CtxLessThanEqual");
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00006CE2 File Offset: 0x00004EE2
		internal static string CtxLike
		{
			get
			{
				return EntityRes.GetString("CtxLike");
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00006CEE File Offset: 0x00004EEE
		internal static string CtxLimitSubClause
		{
			get
			{
				return EntityRes.GetString("CtxLimitSubClause");
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00006CFA File Offset: 0x00004EFA
		internal static string CtxLiteral
		{
			get
			{
				return EntityRes.GetString("CtxLiteral");
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00006D06 File Offset: 0x00004F06
		internal static string CtxMemberAccess
		{
			get
			{
				return EntityRes.GetString("CtxMemberAccess");
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00006D12 File Offset: 0x00004F12
		internal static string CtxMethod
		{
			get
			{
				return EntityRes.GetString("CtxMethod");
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00006D1E File Offset: 0x00004F1E
		internal static string CtxMinus
		{
			get
			{
				return EntityRes.GetString("CtxMinus");
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00006D2A File Offset: 0x00004F2A
		internal static string CtxModulus
		{
			get
			{
				return EntityRes.GetString("CtxModulus");
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00006D36 File Offset: 0x00004F36
		internal static string CtxMultiply
		{
			get
			{
				return EntityRes.GetString("CtxMultiply");
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00006D42 File Offset: 0x00004F42
		internal static string CtxMultisetCtor
		{
			get
			{
				return EntityRes.GetString("CtxMultisetCtor");
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00006D4E File Offset: 0x00004F4E
		internal static string CtxNamespaceImport
		{
			get
			{
				return EntityRes.GetString("CtxNamespaceImport");
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00006D5A File Offset: 0x00004F5A
		internal static string CtxNamespaceImportList
		{
			get
			{
				return EntityRes.GetString("CtxNamespaceImportList");
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00006D66 File Offset: 0x00004F66
		internal static string CtxNavigate
		{
			get
			{
				return EntityRes.GetString("CtxNavigate");
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00006D72 File Offset: 0x00004F72
		internal static string CtxNot
		{
			get
			{
				return EntityRes.GetString("CtxNot");
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00006D7E File Offset: 0x00004F7E
		internal static string CtxNotBetween
		{
			get
			{
				return EntityRes.GetString("CtxNotBetween");
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00006D8A File Offset: 0x00004F8A
		internal static string CtxNotEqual
		{
			get
			{
				return EntityRes.GetString("CtxNotEqual");
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00006D96 File Offset: 0x00004F96
		internal static string CtxNotIn
		{
			get
			{
				return EntityRes.GetString("CtxNotIn");
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00006DA2 File Offset: 0x00004FA2
		internal static string CtxNotLike
		{
			get
			{
				return EntityRes.GetString("CtxNotLike");
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00006DAE File Offset: 0x00004FAE
		internal static string CtxNullLiteral
		{
			get
			{
				return EntityRes.GetString("CtxNullLiteral");
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00006DBA File Offset: 0x00004FBA
		internal static string CtxOfType
		{
			get
			{
				return EntityRes.GetString("CtxOfType");
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00006DC6 File Offset: 0x00004FC6
		internal static string CtxOfTypeOnly
		{
			get
			{
				return EntityRes.GetString("CtxOfTypeOnly");
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00006DD2 File Offset: 0x00004FD2
		internal static string CtxOr
		{
			get
			{
				return EntityRes.GetString("CtxOr");
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00006DDE File Offset: 0x00004FDE
		internal static string CtxOrderByClause
		{
			get
			{
				return EntityRes.GetString("CtxOrderByClause");
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00006DEA File Offset: 0x00004FEA
		internal static string CtxOrderByClauseItem
		{
			get
			{
				return EntityRes.GetString("CtxOrderByClauseItem");
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00006DF6 File Offset: 0x00004FF6
		internal static string CtxOverlaps
		{
			get
			{
				return EntityRes.GetString("CtxOverlaps");
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00006E02 File Offset: 0x00005002
		internal static string CtxParen
		{
			get
			{
				return EntityRes.GetString("CtxParen");
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00006E0E File Offset: 0x0000500E
		internal static string CtxPlus
		{
			get
			{
				return EntityRes.GetString("CtxPlus");
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00006E1A File Offset: 0x0000501A
		internal static string CtxTypeNameWithTypeSpec
		{
			get
			{
				return EntityRes.GetString("CtxTypeNameWithTypeSpec");
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00006E26 File Offset: 0x00005026
		internal static string CtxQueryExpression
		{
			get
			{
				return EntityRes.GetString("CtxQueryExpression");
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00006E32 File Offset: 0x00005032
		internal static string CtxQueryStatement
		{
			get
			{
				return EntityRes.GetString("CtxQueryStatement");
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00006E3E File Offset: 0x0000503E
		internal static string CtxRef
		{
			get
			{
				return EntityRes.GetString("CtxRef");
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00006E4A File Offset: 0x0000504A
		internal static string CtxRefTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxRefTypeDefinition");
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00006E56 File Offset: 0x00005056
		internal static string CtxRelationship
		{
			get
			{
				return EntityRes.GetString("CtxRelationship");
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00006E62 File Offset: 0x00005062
		internal static string CtxRelationshipList
		{
			get
			{
				return EntityRes.GetString("CtxRelationshipList");
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00006E6E File Offset: 0x0000506E
		internal static string CtxRowCtor
		{
			get
			{
				return EntityRes.GetString("CtxRowCtor");
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00006E7A File Offset: 0x0000507A
		internal static string CtxRowTypeDefinition
		{
			get
			{
				return EntityRes.GetString("CtxRowTypeDefinition");
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00006E86 File Offset: 0x00005086
		internal static string CtxSelectRowClause
		{
			get
			{
				return EntityRes.GetString("CtxSelectRowClause");
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00006E92 File Offset: 0x00005092
		internal static string CtxSelectValueClause
		{
			get
			{
				return EntityRes.GetString("CtxSelectValueClause");
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00006E9E File Offset: 0x0000509E
		internal static string CtxSet
		{
			get
			{
				return EntityRes.GetString("CtxSet");
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00006EAA File Offset: 0x000050AA
		internal static string CtxSimpleIdentifier
		{
			get
			{
				return EntityRes.GetString("CtxSimpleIdentifier");
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00006EB6 File Offset: 0x000050B6
		internal static string CtxSkipSubClause
		{
			get
			{
				return EntityRes.GetString("CtxSkipSubClause");
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00006EC2 File Offset: 0x000050C2
		internal static string CtxTopSubClause
		{
			get
			{
				return EntityRes.GetString("CtxTopSubClause");
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00006ECE File Offset: 0x000050CE
		internal static string CtxTreat
		{
			get
			{
				return EntityRes.GetString("CtxTreat");
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00006EDA File Offset: 0x000050DA
		internal static string CtxTypeCtor(object p0)
		{
			return EntityRes.GetString("CtxTypeCtor", new object[] { p0 });
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00006EF0 File Offset: 0x000050F0
		internal static string CtxTypeName
		{
			get
			{
				return EntityRes.GetString("CtxTypeName");
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00006EFC File Offset: 0x000050FC
		internal static string CtxUnaryMinus
		{
			get
			{
				return EntityRes.GetString("CtxUnaryMinus");
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00006F08 File Offset: 0x00005108
		internal static string CtxUnaryPlus
		{
			get
			{
				return EntityRes.GetString("CtxUnaryPlus");
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00006F14 File Offset: 0x00005114
		internal static string CtxUnion
		{
			get
			{
				return EntityRes.GetString("CtxUnion");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00006F20 File Offset: 0x00005120
		internal static string CtxUnionAll
		{
			get
			{
				return EntityRes.GetString("CtxUnionAll");
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00006F2C File Offset: 0x0000512C
		internal static string CtxWhereClause
		{
			get
			{
				return EntityRes.GetString("CtxWhereClause");
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00006F38 File Offset: 0x00005138
		internal static string CannotConvertNumericLiteral(object p0, object p1)
		{
			return EntityRes.GetString("CannotConvertNumericLiteral", new object[] { p0, p1 });
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00006F52 File Offset: 0x00005152
		internal static string GenericSyntaxError
		{
			get
			{
				return EntityRes.GetString("GenericSyntaxError");
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00006F5E File Offset: 0x0000515E
		internal static string InFromClause
		{
			get
			{
				return EntityRes.GetString("InFromClause");
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00006F6A File Offset: 0x0000516A
		internal static string InGroupClause
		{
			get
			{
				return EntityRes.GetString("InGroupClause");
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00006F76 File Offset: 0x00005176
		internal static string InRowCtor
		{
			get
			{
				return EntityRes.GetString("InRowCtor");
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00006F82 File Offset: 0x00005182
		internal static string InSelectProjectionList
		{
			get
			{
				return EntityRes.GetString("InSelectProjectionList");
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00006F8E File Offset: 0x0000518E
		internal static string InvalidAliasName(object p0)
		{
			return EntityRes.GetString("InvalidAliasName", new object[] { p0 });
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00006FA4 File Offset: 0x000051A4
		internal static string InvalidEmptyIdentifier
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyIdentifier");
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00006FB0 File Offset: 0x000051B0
		internal static string InvalidEmptyQuery
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyQuery");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00006FBC File Offset: 0x000051BC
		internal static string InvalidEmptyQueryTextArgument
		{
			get
			{
				return EntityRes.GetString("InvalidEmptyQueryTextArgument");
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00006FC8 File Offset: 0x000051C8
		internal static string InvalidEscapedIdentifier(object p0)
		{
			return EntityRes.GetString("InvalidEscapedIdentifier", new object[] { p0 });
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00006FDE File Offset: 0x000051DE
		internal static string InvalidEscapedIdentifierEOF
		{
			get
			{
				return EntityRes.GetString("InvalidEscapedIdentifierEOF");
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00006FEA File Offset: 0x000051EA
		internal static string InvalidEscapedIdentifierUnbalanced(object p0)
		{
			return EntityRes.GetString("InvalidEscapedIdentifierUnbalanced", new object[] { p0 });
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00007000 File Offset: 0x00005200
		internal static string InvalidOperatorSymbol
		{
			get
			{
				return EntityRes.GetString("InvalidOperatorSymbol");
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000700C File Offset: 0x0000520C
		internal static string InvalidPunctuatorSymbol
		{
			get
			{
				return EntityRes.GetString("InvalidPunctuatorSymbol");
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00007018 File Offset: 0x00005218
		internal static string InvalidSimpleIdentifier(object p0)
		{
			return EntityRes.GetString("InvalidSimpleIdentifier", new object[] { p0 });
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000702E File Offset: 0x0000522E
		internal static string InvalidSimpleIdentifierNonASCII(object p0)
		{
			return EntityRes.GetString("InvalidSimpleIdentifierNonASCII", new object[] { p0 });
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00007044 File Offset: 0x00005244
		internal static string LocalizedCollection
		{
			get
			{
				return EntityRes.GetString("LocalizedCollection");
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00007050 File Offset: 0x00005250
		internal static string LocalizedColumn
		{
			get
			{
				return EntityRes.GetString("LocalizedColumn");
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000705C File Offset: 0x0000525C
		internal static string LocalizedComplex
		{
			get
			{
				return EntityRes.GetString("LocalizedComplex");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00007068 File Offset: 0x00005268
		internal static string LocalizedEntity
		{
			get
			{
				return EntityRes.GetString("LocalizedEntity");
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00007074 File Offset: 0x00005274
		internal static string LocalizedEntityContainerExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedEntityContainerExpression");
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00007080 File Offset: 0x00005280
		internal static string LocalizedFunction
		{
			get
			{
				return EntityRes.GetString("LocalizedFunction");
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000708C File Offset: 0x0000528C
		internal static string LocalizedInlineFunction
		{
			get
			{
				return EntityRes.GetString("LocalizedInlineFunction");
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00007098 File Offset: 0x00005298
		internal static string LocalizedKeyword
		{
			get
			{
				return EntityRes.GetString("LocalizedKeyword");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x000070A4 File Offset: 0x000052A4
		internal static string LocalizedLeft
		{
			get
			{
				return EntityRes.GetString("LocalizedLeft");
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x000070B0 File Offset: 0x000052B0
		internal static string LocalizedLine
		{
			get
			{
				return EntityRes.GetString("LocalizedLine");
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x000070BC File Offset: 0x000052BC
		internal static string LocalizedMetadataMemberExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedMetadataMemberExpression");
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x000070C8 File Offset: 0x000052C8
		internal static string LocalizedNamespace
		{
			get
			{
				return EntityRes.GetString("LocalizedNamespace");
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x000070D4 File Offset: 0x000052D4
		internal static string LocalizedNear
		{
			get
			{
				return EntityRes.GetString("LocalizedNear");
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000070E0 File Offset: 0x000052E0
		internal static string LocalizedPrimitive
		{
			get
			{
				return EntityRes.GetString("LocalizedPrimitive");
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000070EC File Offset: 0x000052EC
		internal static string LocalizedReference
		{
			get
			{
				return EntityRes.GetString("LocalizedReference");
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000070F8 File Offset: 0x000052F8
		internal static string LocalizedRight
		{
			get
			{
				return EntityRes.GetString("LocalizedRight");
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00007104 File Offset: 0x00005304
		internal static string LocalizedRow
		{
			get
			{
				return EntityRes.GetString("LocalizedRow");
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00007110 File Offset: 0x00005310
		internal static string LocalizedTerm
		{
			get
			{
				return EntityRes.GetString("LocalizedTerm");
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000711C File Offset: 0x0000531C
		internal static string LocalizedType
		{
			get
			{
				return EntityRes.GetString("LocalizedType");
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00007128 File Offset: 0x00005328
		internal static string LocalizedValueExpression
		{
			get
			{
				return EntityRes.GetString("LocalizedValueExpression");
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00007134 File Offset: 0x00005334
		internal static string PropertyCannotBeChangedAtThisTime
		{
			get
			{
				return EntityRes.GetString("PropertyCannotBeChangedAtThisTime");
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00007140 File Offset: 0x00005340
		internal static string AliasNameAlreadyUsed(object p0)
		{
			return EntityRes.GetString("AliasNameAlreadyUsed", new object[] { p0 });
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00007156 File Offset: 0x00005356
		internal static string AmbiguousFunctionArguments
		{
			get
			{
				return EntityRes.GetString("AmbiguousFunctionArguments");
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00007162 File Offset: 0x00005362
		internal static string AmbiguousMetadataMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("AmbiguousMetadataMemberName", new object[] { p0, p1, p2 });
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00007180 File Offset: 0x00005380
		internal static string ArgumentTypesAreIncompatible(object p0, object p1)
		{
			return EntityRes.GetString("ArgumentTypesAreIncompatible", new object[] { p0, p1 });
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000719A File Offset: 0x0000539A
		internal static string BetweenLimitsCannotBeUntypedNulls
		{
			get
			{
				return EntityRes.GetString("BetweenLimitsCannotBeUntypedNulls");
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000071A6 File Offset: 0x000053A6
		internal static string BetweenLimitsTypesAreNotCompatible(object p0, object p1)
		{
			return EntityRes.GetString("BetweenLimitsTypesAreNotCompatible", new object[] { p0, p1 });
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000071C0 File Offset: 0x000053C0
		internal static string BetweenLimitsTypesAreNotOrderComparable(object p0, object p1)
		{
			return EntityRes.GetString("BetweenLimitsTypesAreNotOrderComparable", new object[] { p0, p1 });
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000071DA File Offset: 0x000053DA
		internal static string BetweenValueIsNotOrderComparable(object p0, object p1)
		{
			return EntityRes.GetString("BetweenValueIsNotOrderComparable", new object[] { p0, p1 });
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000071F4 File Offset: 0x000053F4
		internal static string CannotCreateEmptyMultiset
		{
			get
			{
				return EntityRes.GetString("CannotCreateEmptyMultiset");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00007200 File Offset: 0x00005400
		internal static string CannotCreateMultisetofNulls
		{
			get
			{
				return EntityRes.GetString("CannotCreateMultisetofNulls");
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000720C File Offset: 0x0000540C
		internal static string CannotInstantiateAbstractType(object p0)
		{
			return EntityRes.GetString("CannotInstantiateAbstractType", new object[] { p0 });
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00007222 File Offset: 0x00005422
		internal static string CannotResolveNameToTypeOrFunction(object p0)
		{
			return EntityRes.GetString("CannotResolveNameToTypeOrFunction", new object[] { p0 });
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00007238 File Offset: 0x00005438
		internal static string ConcatBuiltinNotSupported
		{
			get
			{
				return EntityRes.GetString("ConcatBuiltinNotSupported");
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00007244 File Offset: 0x00005444
		internal static string CouldNotResolveIdentifier(object p0)
		{
			return EntityRes.GetString("CouldNotResolveIdentifier", new object[] { p0 });
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000725A File Offset: 0x0000545A
		internal static string CreateRefTypeIdentifierMustBeASubOrSuperType(object p0, object p1)
		{
			return EntityRes.GetString("CreateRefTypeIdentifierMustBeASubOrSuperType", new object[] { p0, p1 });
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00007274 File Offset: 0x00005474
		internal static string CreateRefTypeIdentifierMustSpecifyAnEntityType(object p0, object p1)
		{
			return EntityRes.GetString("CreateRefTypeIdentifierMustSpecifyAnEntityType", new object[] { p0, p1 });
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000728E File Offset: 0x0000548E
		internal static string DeRefArgIsNotOfRefType(object p0)
		{
			return EntityRes.GetString("DeRefArgIsNotOfRefType", new object[] { p0 });
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000072A4 File Offset: 0x000054A4
		internal static string DuplicatedInlineFunctionOverload(object p0)
		{
			return EntityRes.GetString("DuplicatedInlineFunctionOverload", new object[] { p0 });
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x000072BA File Offset: 0x000054BA
		internal static string ElementOperatorIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ElementOperatorIsNotSupported");
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000072C6 File Offset: 0x000054C6
		internal static string EntitySetDoesNotBelongToEntityContainer(object p0, object p1)
		{
			return EntityRes.GetString("EntitySetDoesNotBelongToEntityContainer", new object[] { p0, p1 });
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000072E0 File Offset: 0x000054E0
		internal static string ExpressionCannotBeNull
		{
			get
			{
				return EntityRes.GetString("ExpressionCannotBeNull");
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000072EC File Offset: 0x000054EC
		internal static string OfTypeExpressionElementTypeMustBeEntityType(object p0, object p1)
		{
			return EntityRes.GetString("OfTypeExpressionElementTypeMustBeEntityType", new object[] { p0, p1 });
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00007306 File Offset: 0x00005506
		internal static string OfTypeExpressionElementTypeMustBeNominalType(object p0, object p1)
		{
			return EntityRes.GetString("OfTypeExpressionElementTypeMustBeNominalType", new object[] { p0, p1 });
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00007320 File Offset: 0x00005520
		internal static string ExpressionMustBeCollection
		{
			get
			{
				return EntityRes.GetString("ExpressionMustBeCollection");
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000732C File Offset: 0x0000552C
		internal static string ExpressionMustBeNumericType
		{
			get
			{
				return EntityRes.GetString("ExpressionMustBeNumericType");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00007338 File Offset: 0x00005538
		internal static string ExpressionTypeMustBeBoolean
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustBeBoolean");
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00007344 File Offset: 0x00005544
		internal static string ExpressionTypeMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustBeEqualComparable");
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00007350 File Offset: 0x00005550
		internal static string ExpressionTypeMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ExpressionTypeMustBeEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000736E File Offset: 0x0000556E
		internal static string ExpressionTypeMustBeNominalType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ExpressionTypeMustBeNominalType", new object[] { p0, p1, p2 });
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000738C File Offset: 0x0000558C
		internal static string ExpressionTypeMustNotBeCollection
		{
			get
			{
				return EntityRes.GetString("ExpressionTypeMustNotBeCollection");
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00007398 File Offset: 0x00005598
		internal static string ExprIsNotValidEntitySetForCreateRef
		{
			get
			{
				return EntityRes.GetString("ExprIsNotValidEntitySetForCreateRef");
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000073A4 File Offset: 0x000055A4
		internal static string FailedToResolveAggregateFunction(object p0)
		{
			return EntityRes.GetString("FailedToResolveAggregateFunction", new object[] { p0 });
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000073BA File Offset: 0x000055BA
		internal static string GeneralExceptionAsQueryInnerException(object p0)
		{
			return EntityRes.GetString("GeneralExceptionAsQueryInnerException", new object[] { p0 });
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000073D0 File Offset: 0x000055D0
		internal static string GroupingKeysMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("GroupingKeysMustBeEqualComparable");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000073DC File Offset: 0x000055DC
		internal static string GroupPartitionOutOfContext
		{
			get
			{
				return EntityRes.GetString("GroupPartitionOutOfContext");
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000073E8 File Offset: 0x000055E8
		internal static string HavingRequiresGroupClause
		{
			get
			{
				return EntityRes.GetString("HavingRequiresGroupClause");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000073F4 File Offset: 0x000055F4
		internal static string ImcompatibleCreateRefKeyElementType
		{
			get
			{
				return EntityRes.GetString("ImcompatibleCreateRefKeyElementType");
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00007400 File Offset: 0x00005600
		internal static string ImcompatibleCreateRefKeyType
		{
			get
			{
				return EntityRes.GetString("ImcompatibleCreateRefKeyType");
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000740C File Offset: 0x0000560C
		internal static string InnerJoinMustHaveOnPredicate
		{
			get
			{
				return EntityRes.GetString("InnerJoinMustHaveOnPredicate");
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00007418 File Offset: 0x00005618
		internal static string InvalidAssociationTypeForUnion(object p0)
		{
			return EntityRes.GetString("InvalidAssociationTypeForUnion", new object[] { p0 });
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000742E File Offset: 0x0000562E
		internal static string InvalidCaseElseType
		{
			get
			{
				return EntityRes.GetString("InvalidCaseElseType");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000743A File Offset: 0x0000563A
		internal static string InvalidCaseThenNullType
		{
			get
			{
				return EntityRes.GetString("InvalidCaseThenNullType");
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00007446 File Offset: 0x00005646
		internal static string InvalidCaseThenTypes
		{
			get
			{
				return EntityRes.GetString("InvalidCaseThenTypes");
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00007452 File Offset: 0x00005652
		internal static string InvalidCaseWhenThenNullType
		{
			get
			{
				return EntityRes.GetString("InvalidCaseWhenThenNullType");
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000745E File Offset: 0x0000565E
		internal static string InvalidCast(object p0, object p1)
		{
			return EntityRes.GetString("InvalidCast", new object[] { p0, p1 });
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00007478 File Offset: 0x00005678
		internal static string InvalidCastExpressionType
		{
			get
			{
				return EntityRes.GetString("InvalidCastExpressionType");
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00007484 File Offset: 0x00005684
		internal static string InvalidCastType
		{
			get
			{
				return EntityRes.GetString("InvalidCastType");
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00007490 File Offset: 0x00005690
		internal static string InvalidComplexType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidComplexType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x000074B2 File Offset: 0x000056B2
		internal static string InvalidCreateRefKeyType
		{
			get
			{
				return EntityRes.GetString("InvalidCreateRefKeyType");
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000074BE File Offset: 0x000056BE
		internal static string InvalidCtorArgumentType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidCtorArgumentType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000074DC File Offset: 0x000056DC
		internal static string InvalidCtorUseOnType(object p0)
		{
			return EntityRes.GetString("InvalidCtorUseOnType", new object[] { p0 });
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000074F2 File Offset: 0x000056F2
		internal static string InvalidDateTimeOffsetLiteral(object p0)
		{
			return EntityRes.GetString("InvalidDateTimeOffsetLiteral", new object[] { p0 });
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00007508 File Offset: 0x00005708
		internal static string InvalidDay(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDay", new object[] { p0, p1 });
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00007522 File Offset: 0x00005722
		internal static string InvalidDayInMonth(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDayInMonth", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00007540 File Offset: 0x00005740
		internal static string InvalidDeRefProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDeRefProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000755E File Offset: 0x0000575E
		internal static string InvalidDistinctArgumentInCtor
		{
			get
			{
				return EntityRes.GetString("InvalidDistinctArgumentInCtor");
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000756A File Offset: 0x0000576A
		internal static string InvalidDistinctArgumentInNonAggFunction
		{
			get
			{
				return EntityRes.GetString("InvalidDistinctArgumentInNonAggFunction");
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00007576 File Offset: 0x00005776
		internal static string InvalidEntityRootTypeArgument(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntityRootTypeArgument", new object[] { p0, p1 });
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00007590 File Offset: 0x00005790
		internal static string InvalidEntityTypeArgument(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidEntityTypeArgument", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000075B2 File Offset: 0x000057B2
		internal static string InvalidExpressionResolutionClass(object p0, object p1)
		{
			return EntityRes.GetString("InvalidExpressionResolutionClass", new object[] { p0, p1 });
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000075CC File Offset: 0x000057CC
		internal static string InvalidFlattenArgument
		{
			get
			{
				return EntityRes.GetString("InvalidFlattenArgument");
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000075D8 File Offset: 0x000057D8
		internal static string InvalidGroupIdentifierReference(object p0)
		{
			return EntityRes.GetString("InvalidGroupIdentifierReference", new object[] { p0 });
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000075EE File Offset: 0x000057EE
		internal static string InvalidHour(object p0, object p1)
		{
			return EntityRes.GetString("InvalidHour", new object[] { p0, p1 });
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00007608 File Offset: 0x00005808
		internal static string InvalidImplicitRelationshipFromEnd(object p0)
		{
			return EntityRes.GetString("InvalidImplicitRelationshipFromEnd", new object[] { p0 });
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000761E File Offset: 0x0000581E
		internal static string InvalidImplicitRelationshipToEnd(object p0)
		{
			return EntityRes.GetString("InvalidImplicitRelationshipToEnd", new object[] { p0 });
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00007634 File Offset: 0x00005834
		internal static string InvalidInExprArgs(object p0, object p1)
		{
			return EntityRes.GetString("InvalidInExprArgs", new object[] { p0, p1 });
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000764E File Offset: 0x0000584E
		internal static string InvalidJoinLeftCorrelation
		{
			get
			{
				return EntityRes.GetString("InvalidJoinLeftCorrelation");
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000765A File Offset: 0x0000585A
		internal static string InvalidKeyArgument(object p0)
		{
			return EntityRes.GetString("InvalidKeyArgument", new object[] { p0 });
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00007670 File Offset: 0x00005870
		internal static string InvalidKeyTypeForCollation(object p0)
		{
			return EntityRes.GetString("InvalidKeyTypeForCollation", new object[] { p0 });
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00007686 File Offset: 0x00005886
		internal static string InvalidLiteralFormat(object p0, object p1)
		{
			return EntityRes.GetString("InvalidLiteralFormat", new object[] { p0, p1 });
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000076A0 File Offset: 0x000058A0
		internal static string InvalidMetadataMemberName
		{
			get
			{
				return EntityRes.GetString("InvalidMetadataMemberName");
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000076AC File Offset: 0x000058AC
		internal static string InvalidMinute(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMinute", new object[] { p0, p1 });
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000076C6 File Offset: 0x000058C6
		internal static string InvalidModeForWithRelationshipClause
		{
			get
			{
				return EntityRes.GetString("InvalidModeForWithRelationshipClause");
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000076D2 File Offset: 0x000058D2
		internal static string InvalidMonth(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMonth", new object[] { p0, p1 });
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000076EC File Offset: 0x000058EC
		internal static string InvalidNamespaceAlias
		{
			get
			{
				return EntityRes.GetString("InvalidNamespaceAlias");
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000076F8 File Offset: 0x000058F8
		internal static string InvalidNullArithmetic
		{
			get
			{
				return EntityRes.GetString("InvalidNullArithmetic");
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00007704 File Offset: 0x00005904
		internal static string InvalidNullComparison
		{
			get
			{
				return EntityRes.GetString("InvalidNullComparison");
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00007710 File Offset: 0x00005910
		internal static string InvalidNullLiteralForNonNullableMember(object p0, object p1)
		{
			return EntityRes.GetString("InvalidNullLiteralForNonNullableMember", new object[] { p0, p1 });
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000772A File Offset: 0x0000592A
		internal static string InvalidParameterFormat(object p0)
		{
			return EntityRes.GetString("InvalidParameterFormat", new object[] { p0 });
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00007740 File Offset: 0x00005940
		internal static string InvalidPlaceholderRootTypeArgument(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidPlaceholderRootTypeArgument", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00007762 File Offset: 0x00005962
		internal static string InvalidPlaceholderTypeArgument(object p0, object p1, object p2, object p3, object p4, object p5)
		{
			return EntityRes.GetString("InvalidPlaceholderTypeArgument", new object[] { p0, p1, p2, p3, p4, p5 });
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000778E File Offset: 0x0000598E
		internal static string InvalidPredicateForCrossJoin
		{
			get
			{
				return EntityRes.GetString("InvalidPredicateForCrossJoin");
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000779A File Offset: 0x0000599A
		internal static string InvalidRelationshipMember(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipMember", new object[] { p0, p1 });
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000077B4 File Offset: 0x000059B4
		internal static string InvalidRelationshipSourceType
		{
			get
			{
				return EntityRes.GetString("InvalidRelationshipSourceType");
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000077C0 File Offset: 0x000059C0
		internal static string InvalidMetadataMemberClassResolution(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidMetadataMemberClassResolution", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000077DE File Offset: 0x000059DE
		internal static string InvalidRootComplexType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRootComplexType", new object[] { p0, p1 });
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000077F8 File Offset: 0x000059F8
		internal static string InvalidRootRowType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRootRowType", new object[] { p0, p1 });
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00007812 File Offset: 0x00005A12
		internal static string InvalidRowType(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidRowType", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00007834 File Offset: 0x00005A34
		internal static string InvalidSecond(object p0, object p1)
		{
			return EntityRes.GetString("InvalidSecond", new object[] { p0, p1 });
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000784E File Offset: 0x00005A4E
		internal static string InvalidSelectValueAliasedExpression
		{
			get
			{
				return EntityRes.GetString("InvalidSelectValueAliasedExpression");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000785A File Offset: 0x00005A5A
		internal static string InvalidSelectValueList
		{
			get
			{
				return EntityRes.GetString("InvalidSelectValueList");
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00007866 File Offset: 0x00005A66
		internal static string InvalidUnarySetOpArgument(object p0)
		{
			return EntityRes.GetString("InvalidUnarySetOpArgument", new object[] { p0 });
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000787C File Offset: 0x00005A7C
		internal static string InvalidUnsignedTypeForUnaryMinusOperation(object p0)
		{
			return EntityRes.GetString("InvalidUnsignedTypeForUnaryMinusOperation", new object[] { p0 });
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00007892 File Offset: 0x00005A92
		internal static string InvalidYear(object p0, object p1)
		{
			return EntityRes.GetString("InvalidYear", new object[] { p0, p1 });
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000078AC File Offset: 0x00005AAC
		internal static string InvalidWithRelationshipTargetEndMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("InvalidWithRelationshipTargetEndMultiplicity", new object[] { p0, p1 });
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000078C6 File Offset: 0x00005AC6
		internal static string InvalidQueryResultType(object p0)
		{
			return EntityRes.GetString("InvalidQueryResultType", new object[] { p0 });
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000078DC File Offset: 0x00005ADC
		internal static string IsNullInvalidType
		{
			get
			{
				return EntityRes.GetString("IsNullInvalidType");
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000078E8 File Offset: 0x00005AE8
		internal static string KeyMustBeCorrelated(object p0)
		{
			return EntityRes.GetString("KeyMustBeCorrelated", new object[] { p0 });
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x000078FE File Offset: 0x00005AFE
		internal static string LeftSetExpressionArgsMustBeCollection
		{
			get
			{
				return EntityRes.GetString("LeftSetExpressionArgsMustBeCollection");
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000790A File Offset: 0x00005B0A
		internal static string LikeArgMustBeStringType
		{
			get
			{
				return EntityRes.GetString("LikeArgMustBeStringType");
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00007916 File Offset: 0x00005B16
		internal static string LiteralTypeNotFoundInMetadata(object p0)
		{
			return EntityRes.GetString("LiteralTypeNotFoundInMetadata", new object[] { p0 });
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000792C File Offset: 0x00005B2C
		internal static string MalformedSingleQuotePayload
		{
			get
			{
				return EntityRes.GetString("MalformedSingleQuotePayload");
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00007938 File Offset: 0x00005B38
		internal static string MalformedStringLiteralPayload
		{
			get
			{
				return EntityRes.GetString("MalformedStringLiteralPayload");
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00007944 File Offset: 0x00005B44
		internal static string MethodInvocationNotSupported
		{
			get
			{
				return EntityRes.GetString("MethodInvocationNotSupported");
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00007950 File Offset: 0x00005B50
		internal static string MultipleDefinitionsOfParameter(object p0)
		{
			return EntityRes.GetString("MultipleDefinitionsOfParameter", new object[] { p0 });
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00007966 File Offset: 0x00005B66
		internal static string MultipleDefinitionsOfVariable(object p0)
		{
			return EntityRes.GetString("MultipleDefinitionsOfVariable", new object[] { p0 });
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000797C File Offset: 0x00005B7C
		internal static string MultisetElemsAreNotTypeCompatible
		{
			get
			{
				return EntityRes.GetString("MultisetElemsAreNotTypeCompatible");
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00007988 File Offset: 0x00005B88
		internal static string NamespaceAliasAlreadyUsed(object p0)
		{
			return EntityRes.GetString("NamespaceAliasAlreadyUsed", new object[] { p0 });
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000799E File Offset: 0x00005B9E
		internal static string NamespaceAlreadyImported(object p0)
		{
			return EntityRes.GetString("NamespaceAlreadyImported", new object[] { p0 });
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000079B4 File Offset: 0x00005BB4
		internal static string NestedAggregateCannotBeUsedInAggregate(object p0, object p1)
		{
			return EntityRes.GetString("NestedAggregateCannotBeUsedInAggregate", new object[] { p0, p1 });
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000079CE File Offset: 0x00005BCE
		internal static string NoAggrFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoAggrFunctionOverloadMatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000079EC File Offset: 0x00005BEC
		internal static string NoCanonicalAggrFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoCanonicalAggrFunctionOverloadMatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00007A0A File Offset: 0x00005C0A
		internal static string NoCanonicalFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoCanonicalFunctionOverloadMatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00007A28 File Offset: 0x00005C28
		internal static string NoFunctionOverloadMatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NoFunctionOverloadMatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00007A46 File Offset: 0x00005C46
		internal static string NotAMemberOfCollection(object p0, object p1)
		{
			return EntityRes.GetString("NotAMemberOfCollection", new object[] { p0, p1 });
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00007A60 File Offset: 0x00005C60
		internal static string NotAMemberOfType(object p0, object p1)
		{
			return EntityRes.GetString("NotAMemberOfType", new object[] { p0, p1 });
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00007A7A File Offset: 0x00005C7A
		internal static string NotASuperOrSubType(object p0, object p1)
		{
			return EntityRes.GetString("NotASuperOrSubType", new object[] { p0, p1 });
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00007A94 File Offset: 0x00005C94
		internal static string NullLiteralCannotBePromotedToCollectionOfNulls
		{
			get
			{
				return EntityRes.GetString("NullLiteralCannotBePromotedToCollectionOfNulls");
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00007AA0 File Offset: 0x00005CA0
		internal static string NumberOfTypeCtorIsLessThenFormalSpec(object p0)
		{
			return EntityRes.GetString("NumberOfTypeCtorIsLessThenFormalSpec", new object[] { p0 });
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00007AB6 File Offset: 0x00005CB6
		internal static string NumberOfTypeCtorIsMoreThenFormalSpec(object p0)
		{
			return EntityRes.GetString("NumberOfTypeCtorIsMoreThenFormalSpec", new object[] { p0 });
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00007ACC File Offset: 0x00005CCC
		internal static string OrderByKeyIsNotOrderComparable
		{
			get
			{
				return EntityRes.GetString("OrderByKeyIsNotOrderComparable");
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00007AD8 File Offset: 0x00005CD8
		internal static string OfTypeOnlyTypeArgumentCannotBeAbstract(object p0)
		{
			return EntityRes.GetString("OfTypeOnlyTypeArgumentCannotBeAbstract", new object[] { p0 });
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00007AEE File Offset: 0x00005CEE
		internal static string ParameterTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("ParameterTypeNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00007B08 File Offset: 0x00005D08
		internal static string ParameterWasNotDefined(object p0)
		{
			return EntityRes.GetString("ParameterWasNotDefined", new object[] { p0 });
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00007B1E File Offset: 0x00005D1E
		internal static string PlaceholderExpressionMustBeCompatibleWithEdm64(object p0, object p1)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeCompatibleWithEdm64", new object[] { p0, p1 });
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00007B38 File Offset: 0x00005D38
		internal static string PlaceholderExpressionMustBeConstant(object p0)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeConstant", new object[] { p0 });
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00007B4E File Offset: 0x00005D4E
		internal static string PlaceholderExpressionMustBeGreaterThanOrEqualToZero(object p0)
		{
			return EntityRes.GetString("PlaceholderExpressionMustBeGreaterThanOrEqualToZero", new object[] { p0 });
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00007B64 File Offset: 0x00005D64
		internal static string PlaceholderSetArgTypeIsNotEqualComparable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("PlaceholderSetArgTypeIsNotEqualComparable", new object[] { p0, p1, p2 });
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00007B82 File Offset: 0x00005D82
		internal static string PlusLeftExpressionInvalidType
		{
			get
			{
				return EntityRes.GetString("PlusLeftExpressionInvalidType");
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00007B8E File Offset: 0x00005D8E
		internal static string PlusRightExpressionInvalidType
		{
			get
			{
				return EntityRes.GetString("PlusRightExpressionInvalidType");
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00007B9A File Offset: 0x00005D9A
		internal static string PrecisionMustBeGreaterThanScale(object p0, object p1)
		{
			return EntityRes.GetString("PrecisionMustBeGreaterThanScale", new object[] { p0, p1 });
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00007BB4 File Offset: 0x00005DB4
		internal static string RefArgIsNotOfEntityType(object p0)
		{
			return EntityRes.GetString("RefArgIsNotOfEntityType", new object[] { p0 });
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00007BCA File Offset: 0x00005DCA
		internal static string RefTypeIdentifierMustSpecifyAnEntityType(object p0, object p1)
		{
			return EntityRes.GetString("RefTypeIdentifierMustSpecifyAnEntityType", new object[] { p0, p1 });
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00007BE4 File Offset: 0x00005DE4
		internal static string RelationshipFromEndIsAmbiguos
		{
			get
			{
				return EntityRes.GetString("RelationshipFromEndIsAmbiguos");
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00007BF0 File Offset: 0x00005DF0
		internal static string RelationshipTypeIsNotCompatibleWithEntity(object p0, object p1)
		{
			return EntityRes.GetString("RelationshipTypeIsNotCompatibleWithEntity", new object[] { p0, p1 });
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00007C0A File Offset: 0x00005E0A
		internal static string RelationshipTargetMustBeUnique(object p0)
		{
			return EntityRes.GetString("RelationshipTargetMustBeUnique", new object[] { p0 });
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00007C20 File Offset: 0x00005E20
		internal static string ResultingExpressionTypeCannotBeNull
		{
			get
			{
				return EntityRes.GetString("ResultingExpressionTypeCannotBeNull");
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00007C2C File Offset: 0x00005E2C
		internal static string RightSetExpressionArgsMustBeCollection
		{
			get
			{
				return EntityRes.GetString("RightSetExpressionArgsMustBeCollection");
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00007C38 File Offset: 0x00005E38
		internal static string RowCtorElementCannotBeNull
		{
			get
			{
				return EntityRes.GetString("RowCtorElementCannotBeNull");
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00007C44 File Offset: 0x00005E44
		internal static string SelectDistinctMustBeEqualComparable
		{
			get
			{
				return EntityRes.GetString("SelectDistinctMustBeEqualComparable");
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00007C50 File Offset: 0x00005E50
		internal static string SourceTypeMustBePromotoableToFromEndRelationType(object p0, object p1)
		{
			return EntityRes.GetString("SourceTypeMustBePromotoableToFromEndRelationType", new object[] { p0, p1 });
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00007C6A File Offset: 0x00005E6A
		internal static string TopAndLimitCannotCoexist
		{
			get
			{
				return EntityRes.GetString("TopAndLimitCannotCoexist");
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00007C76 File Offset: 0x00005E76
		internal static string TopAndSkipCannotCoexist
		{
			get
			{
				return EntityRes.GetString("TopAndSkipCannotCoexist");
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00007C82 File Offset: 0x00005E82
		internal static string TypeDoesNotSupportSpec(object p0)
		{
			return EntityRes.GetString("TypeDoesNotSupportSpec", new object[] { p0 });
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00007C98 File Offset: 0x00005E98
		internal static string TypeDoesNotSupportFacet(object p0, object p1)
		{
			return EntityRes.GetString("TypeDoesNotSupportFacet", new object[] { p0, p1 });
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00007CB2 File Offset: 0x00005EB2
		internal static string TypeArgumentCountMismatch(object p0, object p1)
		{
			return EntityRes.GetString("TypeArgumentCountMismatch", new object[] { p0, p1 });
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00007CCC File Offset: 0x00005ECC
		internal static string TypeArgumentMustBeLiteral
		{
			get
			{
				return EntityRes.GetString("TypeArgumentMustBeLiteral");
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00007CD8 File Offset: 0x00005ED8
		internal static string TypeArgumentBelowMin(object p0)
		{
			return EntityRes.GetString("TypeArgumentBelowMin", new object[] { p0 });
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00007CEE File Offset: 0x00005EEE
		internal static string TypeArgumentExceedsMax(object p0)
		{
			return EntityRes.GetString("TypeArgumentExceedsMax", new object[] { p0 });
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00007D04 File Offset: 0x00005F04
		internal static string TypeArgumentIsNotValid
		{
			get
			{
				return EntityRes.GetString("TypeArgumentIsNotValid");
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00007D10 File Offset: 0x00005F10
		internal static string TypeKindMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TypeKindMismatch", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00007D32 File Offset: 0x00005F32
		internal static string TypeMustBeInheritableType
		{
			get
			{
				return EntityRes.GetString("TypeMustBeInheritableType");
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00007D3E File Offset: 0x00005F3E
		internal static string TypeMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeMustBeEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00007D5C File Offset: 0x00005F5C
		internal static string TypeMustBeNominalType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("TypeMustBeNominalType", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00007D7A File Offset: 0x00005F7A
		internal static string TypeNameNotFound(object p0)
		{
			return EntityRes.GetString("TypeNameNotFound", new object[] { p0 });
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00007D90 File Offset: 0x00005F90
		internal static string GroupVarNotFoundInScope
		{
			get
			{
				return EntityRes.GetString("GroupVarNotFoundInScope");
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00007D9C File Offset: 0x00005F9C
		internal static string InvalidArgumentTypeForAggregateFunction
		{
			get
			{
				return EntityRes.GetString("InvalidArgumentTypeForAggregateFunction");
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00007DA8 File Offset: 0x00005FA8
		internal static string InvalidSavePoint
		{
			get
			{
				return EntityRes.GetString("InvalidSavePoint");
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00007DB4 File Offset: 0x00005FB4
		internal static string InvalidScopeIndex
		{
			get
			{
				return EntityRes.GetString("InvalidScopeIndex");
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00007DC0 File Offset: 0x00005FC0
		internal static string LiteralTypeNotSupported(object p0)
		{
			return EntityRes.GetString("LiteralTypeNotSupported", new object[] { p0 });
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00007DD6 File Offset: 0x00005FD6
		internal static string ParserFatalError
		{
			get
			{
				return EntityRes.GetString("ParserFatalError");
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00007DE2 File Offset: 0x00005FE2
		internal static string ParserInputError
		{
			get
			{
				return EntityRes.GetString("ParserInputError");
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00007DEE File Offset: 0x00005FEE
		internal static string StackOverflowInParser
		{
			get
			{
				return EntityRes.GetString("StackOverflowInParser");
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00007DFA File Offset: 0x00005FFA
		internal static string UnknownAstCommandExpression
		{
			get
			{
				return EntityRes.GetString("UnknownAstCommandExpression");
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00007E06 File Offset: 0x00006006
		internal static string UnknownAstExpressionType
		{
			get
			{
				return EntityRes.GetString("UnknownAstExpressionType");
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00007E12 File Offset: 0x00006012
		internal static string UnknownBuiltInAstExpressionType
		{
			get
			{
				return EntityRes.GetString("UnknownBuiltInAstExpressionType");
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00007E1E File Offset: 0x0000601E
		internal static string UnknownExpressionResolutionClass(object p0)
		{
			return EntityRes.GetString("UnknownExpressionResolutionClass", new object[] { p0 });
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00007E34 File Offset: 0x00006034
		internal static string SqlGen_ApplyNotSupportedOnSql8
		{
			get
			{
				return EntityRes.GetString("SqlGen_ApplyNotSupportedOnSql8");
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00007E40 File Offset: 0x00006040
		internal static string SqlGen_InvalidDatePartArgumentExpression(object p0, object p1)
		{
			return EntityRes.GetString("SqlGen_InvalidDatePartArgumentExpression", new object[] { p0, p1 });
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00007E5A File Offset: 0x0000605A
		internal static string SqlGen_InvalidDatePartArgumentValue(object p0, object p1, object p2)
		{
			return EntityRes.GetString("SqlGen_InvalidDatePartArgumentValue", new object[] { p0, p1, p2 });
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00007E78 File Offset: 0x00006078
		internal static string SqlGen_NiladicFunctionsCannotHaveParameters
		{
			get
			{
				return EntityRes.GetString("SqlGen_NiladicFunctionsCannotHaveParameters");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00007E84 File Offset: 0x00006084
		internal static string SqlGen_ParameterForLimitNotSupportedOnSql8
		{
			get
			{
				return EntityRes.GetString("SqlGen_ParameterForLimitNotSupportedOnSql8");
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00007E90 File Offset: 0x00006090
		internal static string SqlGen_ParameterForSkipNotSupportedOnSql8
		{
			get
			{
				return EntityRes.GetString("SqlGen_ParameterForSkipNotSupportedOnSql8");
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00007E9C File Offset: 0x0000609C
		internal static string SqlGen_PrimitiveTypeNotSupportedPriorSql10(object p0)
		{
			return EntityRes.GetString("SqlGen_PrimitiveTypeNotSupportedPriorSql10", new object[] { p0 });
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00007EB2 File Offset: 0x000060B2
		internal static string SqlGen_CanonicalFunctionNotSupportedPriorSql10(object p0)
		{
			return EntityRes.GetString("SqlGen_CanonicalFunctionNotSupportedPriorSql10", new object[] { p0 });
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00007EC8 File Offset: 0x000060C8
		internal static string SqlGen_TypedPositiveInfinityNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("SqlGen_TypedPositiveInfinityNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00007EE2 File Offset: 0x000060E2
		internal static string SqlGen_TypedNegativeInfinityNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("SqlGen_TypedNegativeInfinityNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00007EFC File Offset: 0x000060FC
		internal static string SqlGen_TypedNaNNotSupported(object p0)
		{
			return EntityRes.GetString("SqlGen_TypedNaNNotSupported", new object[] { p0 });
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00007F12 File Offset: 0x00006112
		internal static string Cqt_General_NullTypeInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NullTypeInvalid");
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00007F1E File Offset: 0x0000611E
		internal static string Cqt_General_PolymorphicTypeRequired(object p0)
		{
			return EntityRes.GetString("Cqt_General_PolymorphicTypeRequired", new object[] { p0 });
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00007F34 File Offset: 0x00006134
		internal static string Cqt_General_PolymorphicArgRequired(object p0)
		{
			return EntityRes.GetString("Cqt_General_PolymorphicArgRequired", new object[] { p0 });
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00007F4A File Offset: 0x0000614A
		internal static string Cqt_General_UnsupportedExpression(object p0)
		{
			return EntityRes.GetString("Cqt_General_UnsupportedExpression", new object[] { p0 });
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00007F60 File Offset: 0x00006160
		internal static string Cqt_General_MetadataNotReadOnly
		{
			get
			{
				return EntityRes.GetString("Cqt_General_MetadataNotReadOnly");
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00007F6C File Offset: 0x0000616C
		internal static string Cqt_General_NoProviderBooleanType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderBooleanType");
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00007F78 File Offset: 0x00006178
		internal static string Cqt_General_NoProviderIntegerType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderIntegerType");
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00007F84 File Offset: 0x00006184
		internal static string Cqt_General_NoProviderStringType
		{
			get
			{
				return EntityRes.GetString("Cqt_General_NoProviderStringType");
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00007F90 File Offset: 0x00006190
		internal static string Cqt_Metadata_EdmMemberIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EdmMemberIncorrectSpace");
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00007F9C File Offset: 0x0000619C
		internal static string Cqt_Metadata_EntitySetEntityContainerNull
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntitySetEntityContainerNull");
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00007FA8 File Offset: 0x000061A8
		internal static string Cqt_Metadata_EntitySetIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntitySetIncorrectSpace");
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00007FB4 File Offset: 0x000061B4
		internal static string Cqt_Metadata_EntityTypeNullKeyMembersInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntityTypeNullKeyMembersInvalid");
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00007FC0 File Offset: 0x000061C0
		internal static string Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_EntityTypeEmptyKeyMembersInvalid");
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00007FCC File Offset: 0x000061CC
		internal static string Cqt_Metadata_FunctionReturnParameterNull
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionReturnParameterNull");
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00007FD8 File Offset: 0x000061D8
		internal static string Cqt_Metadata_FunctionIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionIncorrectSpace");
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00007FE4 File Offset: 0x000061E4
		internal static string Cqt_Metadata_FunctionParameterIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_FunctionParameterIncorrectSpace");
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00007FF0 File Offset: 0x000061F0
		internal static string Cqt_Metadata_TypeUsageIncorrectSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_Metadata_TypeUsageIncorrectSpace");
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00007FFC File Offset: 0x000061FC
		internal static string Cqt_Exceptions_InvalidCommandTree
		{
			get
			{
				return EntityRes.GetString("Cqt_Exceptions_InvalidCommandTree");
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00008008 File Offset: 0x00006208
		internal static string Cqt_Util_CheckListEmptyInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Util_CheckListEmptyInvalid");
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00008014 File Offset: 0x00006214
		internal static string Cqt_Util_CheckListDuplicateName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_Util_CheckListDuplicateName", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00008032 File Offset: 0x00006232
		internal static string Cqt_ExpressionLink_TypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_ExpressionLink_TypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000804C File Offset: 0x0000624C
		internal static string Cqt_ExpressionList_IncorrectElementCount
		{
			get
			{
				return EntityRes.GetString("Cqt_ExpressionList_IncorrectElementCount");
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00008058 File Offset: 0x00006258
		internal static string Cqt_Copier_EntityContainerNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_EntityContainerNotFound", new object[] { p0 });
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000806E File Offset: 0x0000626E
		internal static string Cqt_Copier_EntitySetNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_EntitySetNotFound", new object[] { p0, p1 });
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00008088 File Offset: 0x00006288
		internal static string Cqt_Copier_FunctionNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_FunctionNotFound", new object[] { p0 });
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000809E File Offset: 0x0000629E
		internal static string Cqt_Copier_PropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_PropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000080B8 File Offset: 0x000062B8
		internal static string Cqt_Copier_NavPropertyNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_NavPropertyNotFound", new object[] { p0, p1 });
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000080D2 File Offset: 0x000062D2
		internal static string Cqt_Copier_EndNotFound(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Copier_EndNotFound", new object[] { p0, p1 });
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000080EC File Offset: 0x000062EC
		internal static string Cqt_Copier_TypeNotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Copier_TypeNotFound", new object[] { p0 });
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00008102 File Offset: 0x00006302
		internal static string Cqt_CommandTree_InvalidDataSpace
		{
			get
			{
				return EntityRes.GetString("Cqt_CommandTree_InvalidDataSpace");
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000810E File Offset: 0x0000630E
		internal static string Cqt_CommandTree_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("Cqt_CommandTree_InvalidParameterName", new object[] { p0 });
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00008124 File Offset: 0x00006324
		internal static string Cqt_Validator_InvalidIncompatibleParameterReferences(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidIncompatibleParameterReferences", new object[] { p0 });
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000813A File Offset: 0x0000633A
		internal static string Cqt_Validator_InvalidOtherWorkspaceMetadata(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidOtherWorkspaceMetadata", new object[] { p0 });
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00008150 File Offset: 0x00006350
		internal static string Cqt_Validator_InvalidIncorrectDataSpaceMetadata(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Validator_InvalidIncorrectDataSpaceMetadata", new object[] { p0, p1 });
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000816A File Offset: 0x0000636A
		internal static string Cqt_Factory_NewCollectionInvalidCommonType
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_NewCollectionInvalidCommonType");
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00008176 File Offset: 0x00006376
		internal static string Cqt_Factory_NoSuchProperty(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Factory_NoSuchProperty", new object[] { p0, p1 });
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00008190 File Offset: 0x00006390
		internal static string Cqt_Factory_NoSuchRelationEnd
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_NoSuchRelationEnd");
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000819C File Offset: 0x0000639C
		internal static string Cqt_Factory_IncompatibleRelationEnds
		{
			get
			{
				return EntityRes.GetString("Cqt_Factory_IncompatibleRelationEnds");
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000081A8 File Offset: 0x000063A8
		internal static string Cqt_Factory_MethodResultTypeNotSupported(object p0)
		{
			return EntityRes.GetString("Cqt_Factory_MethodResultTypeNotSupported", new object[] { p0 });
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000081BE File Offset: 0x000063BE
		internal static string Cqt_Aggregate_InvalidFunction
		{
			get
			{
				return EntityRes.GetString("Cqt_Aggregate_InvalidFunction");
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000081CA File Offset: 0x000063CA
		internal static string Cqt_Binding_CollectionRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Binding_CollectionRequired");
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000081D6 File Offset: 0x000063D6
		internal static string Cqt_Binding_VariableNameNotValid
		{
			get
			{
				return EntityRes.GetString("Cqt_Binding_VariableNameNotValid");
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000081E2 File Offset: 0x000063E2
		internal static string Cqt_GroupBinding_CollectionRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBinding_CollectionRequired");
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x000081EE File Offset: 0x000063EE
		internal static string Cqt_GroupBinding_GroupVariableNameNotValid
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBinding_GroupVariableNameNotValid");
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000081FA File Offset: 0x000063FA
		internal static string Cqt_Binary_CollectionsRequired(object p0)
		{
			return EntityRes.GetString("Cqt_Binary_CollectionsRequired", new object[] { p0 });
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00008210 File Offset: 0x00006410
		internal static string Cqt_Unary_CollectionRequired(object p0)
		{
			return EntityRes.GetString("Cqt_Unary_CollectionRequired", new object[] { p0 });
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00008226 File Offset: 0x00006426
		internal static string Cqt_And_BooleanArgumentsRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_And_BooleanArgumentsRequired");
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00008232 File Offset: 0x00006432
		internal static string Cqt_Apply_DuplicateVariableNames
		{
			get
			{
				return EntityRes.GetString("Cqt_Apply_DuplicateVariableNames");
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000823E File Offset: 0x0000643E
		internal static string Cqt_Arithmetic_NumericCommonType
		{
			get
			{
				return EntityRes.GetString("Cqt_Arithmetic_NumericCommonType");
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000824A File Offset: 0x0000644A
		internal static string Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus(object p0)
		{
			return EntityRes.GetString("Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus", new object[] { p0 });
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00008260 File Offset: 0x00006460
		internal static string Cqt_Case_WhensMustEqualThens
		{
			get
			{
				return EntityRes.GetString("Cqt_Case_WhensMustEqualThens");
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000826C File Offset: 0x0000646C
		internal static string Cqt_Case_InvalidResultType
		{
			get
			{
				return EntityRes.GetString("Cqt_Case_InvalidResultType");
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00008278 File Offset: 0x00006478
		internal static string Cqt_Cast_InvalidCast(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_Cast_InvalidCast", new object[] { p0, p1 });
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00008292 File Offset: 0x00006492
		internal static string Cqt_Comparison_ComparableRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Comparison_ComparableRequired");
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000829E File Offset: 0x0000649E
		internal static string Cqt_Constant_InvalidType
		{
			get
			{
				return EntityRes.GetString("Cqt_Constant_InvalidType");
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000082AA File Offset: 0x000064AA
		internal static string Cqt_Constant_InvalidValueForType(object p0)
		{
			return EntityRes.GetString("Cqt_Constant_InvalidValueForType", new object[] { p0 });
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000082C0 File Offset: 0x000064C0
		internal static string Cqt_Constant_InvalidConstantType(object p0)
		{
			return EntityRes.GetString("Cqt_Constant_InvalidConstantType", new object[] { p0 });
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000082D6 File Offset: 0x000064D6
		internal static string Cqt_Distinct_InvalidCollection
		{
			get
			{
				return EntityRes.GetString("Cqt_Distinct_InvalidCollection");
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x000082E2 File Offset: 0x000064E2
		internal static string Cqt_DeRef_RefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_DeRef_RefRequired");
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x000082EE File Offset: 0x000064EE
		internal static string Cqt_Element_InvalidArgumentForUnwrapSingleProperty
		{
			get
			{
				return EntityRes.GetString("Cqt_Element_InvalidArgumentForUnwrapSingleProperty");
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000082FA File Offset: 0x000064FA
		internal static string Cqt_Except_LeftNullTypeInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Except_LeftNullTypeInvalid");
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00008306 File Offset: 0x00006506
		internal static string Cqt_Function_VoidResultInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_VoidResultInvalid");
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00008312 File Offset: 0x00006512
		internal static string Cqt_Function_NonComposableInExpression
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_NonComposableInExpression");
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000831E File Offset: 0x0000651E
		internal static string Cqt_Function_CommandTextInExpression
		{
			get
			{
				return EntityRes.GetString("Cqt_Function_CommandTextInExpression");
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000832A File Offset: 0x0000652A
		internal static string Cqt_Function_CanonicalFunction_NotFound(object p0)
		{
			return EntityRes.GetString("Cqt_Function_CanonicalFunction_NotFound", new object[] { p0 });
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00008340 File Offset: 0x00006540
		internal static string Cqt_Function_CanonicalFunction_AmbiguousMatch(object p0)
		{
			return EntityRes.GetString("Cqt_Function_CanonicalFunction_AmbiguousMatch", new object[] { p0 });
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00008356 File Offset: 0x00006556
		internal static string Cqt_GetEntityRef_EntityRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GetEntityRef_EntityRequired");
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00008362 File Offset: 0x00006562
		internal static string Cqt_GetRefKey_RefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_GetRefKey_RefRequired");
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000836E File Offset: 0x0000656E
		internal static string Cqt_GroupBy_AtLeastOneKeyOrAggregate
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBy_AtLeastOneKeyOrAggregate");
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000837A File Offset: 0x0000657A
		internal static string Cqt_GroupBy_KeyNotEqualityComparable(object p0)
		{
			return EntityRes.GetString("Cqt_GroupBy_KeyNotEqualityComparable", new object[] { p0 });
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00008390 File Offset: 0x00006590
		internal static string Cqt_GroupBy_AggregateColumnExistsAsGroupColumn(object p0)
		{
			return EntityRes.GetString("Cqt_GroupBy_AggregateColumnExistsAsGroupColumn", new object[] { p0 });
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000083A6 File Offset: 0x000065A6
		internal static string Cqt_GroupBy_MoreThanOneGroupAggregate
		{
			get
			{
				return EntityRes.GetString("Cqt_GroupBy_MoreThanOneGroupAggregate");
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000083B2 File Offset: 0x000065B2
		internal static string Cqt_CrossJoin_AtLeastTwoInputs
		{
			get
			{
				return EntityRes.GetString("Cqt_CrossJoin_AtLeastTwoInputs");
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000083BE File Offset: 0x000065BE
		internal static string Cqt_CrossJoin_DuplicateVariableNames(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_CrossJoin_DuplicateVariableNames", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x000083DC File Offset: 0x000065DC
		internal static string Cqt_IsNull_CollectionNotAllowed
		{
			get
			{
				return EntityRes.GetString("Cqt_IsNull_CollectionNotAllowed");
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000083E8 File Offset: 0x000065E8
		internal static string Cqt_IsNull_InvalidType
		{
			get
			{
				return EntityRes.GetString("Cqt_IsNull_InvalidType");
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000083F4 File Offset: 0x000065F4
		internal static string Cqt_InvalidTypeForSetOperation(object p0, object p1)
		{
			return EntityRes.GetString("Cqt_InvalidTypeForSetOperation", new object[] { p0, p1 });
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000840E File Offset: 0x0000660E
		internal static string Cqt_Join_DuplicateVariableNames
		{
			get
			{
				return EntityRes.GetString("Cqt_Join_DuplicateVariableNames");
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000841A File Offset: 0x0000661A
		internal static string Cqt_Limit_ConstantOrParameterRefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_ConstantOrParameterRefRequired");
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00008426 File Offset: 0x00006626
		internal static string Cqt_Limit_IntegerRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_IntegerRequired");
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00008432 File Offset: 0x00006632
		internal static string Cqt_Limit_NonNegativeLimitRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Limit_NonNegativeLimitRequired");
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000843E File Offset: 0x0000663E
		internal static string Cqt_NewInstance_CollectionTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_CollectionTypeRequired");
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000844A File Offset: 0x0000664A
		internal static string Cqt_NewInstance_StructuralTypeRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_StructuralTypeRequired");
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00008456 File Offset: 0x00006656
		internal static string Cqt_NewInstance_CannotInstantiateMemberlessType(object p0)
		{
			return EntityRes.GetString("Cqt_NewInstance_CannotInstantiateMemberlessType", new object[] { p0 });
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000846C File Offset: 0x0000666C
		internal static string Cqt_NewInstance_CannotInstantiateAbstractType(object p0)
		{
			return EntityRes.GetString("Cqt_NewInstance_CannotInstantiateAbstractType", new object[] { p0 });
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00008482 File Offset: 0x00006682
		internal static string Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid
		{
			get
			{
				return EntityRes.GetString("Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid");
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0000848E File Offset: 0x0000668E
		internal static string Cqt_Not_BooleanArgumentRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Not_BooleanArgumentRequired");
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000849A File Offset: 0x0000669A
		internal static string Cqt_Or_BooleanArgumentsRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Or_BooleanArgumentsRequired");
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x000084A6 File Offset: 0x000066A6
		internal static string Cqt_Property_InstanceRequiredForInstance
		{
			get
			{
				return EntityRes.GetString("Cqt_Property_InstanceRequiredForInstance");
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000084B2 File Offset: 0x000066B2
		internal static string Cqt_Ref_PolymorphicArgRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Ref_PolymorphicArgRequired");
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x000084BE File Offset: 0x000066BE
		internal static string Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship");
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000084CA File Offset: 0x000066CA
		internal static string Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne");
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x000084D6 File Offset: 0x000066D6
		internal static string Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd");
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000084E2 File Offset: 0x000066E2
		internal static string Cqt_RelatedEntityRef_TargetEntityNotRef
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEntityNotRef");
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000084EE File Offset: 0x000066EE
		internal static string Cqt_RelatedEntityRef_TargetEntityNotCompatible
		{
			get
			{
				return EntityRes.GetString("Cqt_RelatedEntityRef_TargetEntityNotCompatible");
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000084FA File Offset: 0x000066FA
		internal static string Cqt_RelNav_NoCompositions
		{
			get
			{
				return EntityRes.GetString("Cqt_RelNav_NoCompositions");
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00008506 File Offset: 0x00006706
		internal static string Cqt_RelNav_WrongSourceType(object p0)
		{
			return EntityRes.GetString("Cqt_RelNav_WrongSourceType", new object[] { p0 });
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000851C File Offset: 0x0000671C
		internal static string Cqt_Skip_ConstantOrParameterRefRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_ConstantOrParameterRefRequired");
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00008528 File Offset: 0x00006728
		internal static string Cqt_Skip_IntegerRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_IntegerRequired");
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00008534 File Offset: 0x00006734
		internal static string Cqt_Skip_NonNegativeCountRequired
		{
			get
			{
				return EntityRes.GetString("Cqt_Skip_NonNegativeCountRequired");
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00008540 File Offset: 0x00006740
		internal static string Cqt_Sort_EmptyCollationInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_EmptyCollationInvalid");
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000854C File Offset: 0x0000674C
		internal static string Cqt_Sort_NonStringCollationInvalid
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_NonStringCollationInvalid");
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00008558 File Offset: 0x00006758
		internal static string Cqt_Sort_OrderComparable
		{
			get
			{
				return EntityRes.GetString("Cqt_Sort_OrderComparable");
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00008564 File Offset: 0x00006764
		internal static string Cqt_UDF_FunctionDefinitionGenerationFailed(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionGenerationFailed", new object[] { p0 });
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000857A File Offset: 0x0000677A
		internal static string Cqt_UDF_FunctionDefinitionWithCircularReference(object p0)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionWithCircularReference", new object[] { p0 });
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00008590 File Offset: 0x00006790
		internal static string Cqt_UDF_FunctionDefinitionResultTypeMismatch(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Cqt_UDF_FunctionDefinitionResultTypeMismatch", new object[] { p0, p1, p2 });
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000085AE File Offset: 0x000067AE
		internal static string Cqt_Validator_VarRefInvalid(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_VarRefInvalid", new object[] { p0 });
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000085C4 File Offset: 0x000067C4
		internal static string Cqt_Validator_VarRefTypeMismatch(object p0)
		{
			return EntityRes.GetString("Cqt_Validator_VarRefTypeMismatch", new object[] { p0 });
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000085DA File Offset: 0x000067DA
		internal static string Iqt_General_UnsupportedOp(object p0)
		{
			return EntityRes.GetString("Iqt_General_UnsupportedOp", new object[] { p0 });
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000085F0 File Offset: 0x000067F0
		internal static string Iqt_CTGen_UnexpectedAggregate
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedAggregate");
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x000085FC File Offset: 0x000067FC
		internal static string Iqt_CTGen_UnexpectedVarDefList
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedVarDefList");
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00008608 File Offset: 0x00006808
		internal static string Iqt_CTGen_UnexpectedVarDef
		{
			get
			{
				return EntityRes.GetString("Iqt_CTGen_UnexpectedVarDef");
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00008614 File Offset: 0x00006814
		internal static string ADP_MustUseSequentialAccess
		{
			get
			{
				return EntityRes.GetString("ADP_MustUseSequentialAccess");
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00008620 File Offset: 0x00006820
		internal static string ADP_ProviderDoesNotSupportCommandTrees
		{
			get
			{
				return EntityRes.GetString("ADP_ProviderDoesNotSupportCommandTrees");
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000862C File Offset: 0x0000682C
		internal static string ADP_ClosedDataReaderError
		{
			get
			{
				return EntityRes.GetString("ADP_ClosedDataReaderError");
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00008638 File Offset: 0x00006838
		internal static string ADP_DataReaderClosed(object p0)
		{
			return EntityRes.GetString("ADP_DataReaderClosed", new object[] { p0 });
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000864E File Offset: 0x0000684E
		internal static string ADP_ImplicitlyClosedDataReaderError
		{
			get
			{
				return EntityRes.GetString("ADP_ImplicitlyClosedDataReaderError");
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000865A File Offset: 0x0000685A
		internal static string ADP_NoData
		{
			get
			{
				return EntityRes.GetString("ADP_NoData");
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00008666 File Offset: 0x00006866
		internal static string ADP_GetSchemaTableIsNotSupported
		{
			get
			{
				return EntityRes.GetString("ADP_GetSchemaTableIsNotSupported");
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00008672 File Offset: 0x00006872
		internal static string ADP_InvalidDataReaderFieldCountForPrimitiveType
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataReaderFieldCountForPrimitiveType");
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000867E File Offset: 0x0000687E
		internal static string ADP_InvalidDataReaderMissingColumnForType(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderMissingColumnForType", new object[] { p0, p1 });
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00008698 File Offset: 0x00006898
		internal static string ADP_InvalidDataReaderMissingDiscriminatorColumn(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderMissingDiscriminatorColumn", new object[] { p0, p1 });
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x000086B2 File Offset: 0x000068B2
		internal static string ADP_InvalidDataReaderUnableToDetermineType
		{
			get
			{
				return EntityRes.GetString("ADP_InvalidDataReaderUnableToDetermineType");
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000086BE File Offset: 0x000068BE
		internal static string ADP_InvalidDataReaderUnableToMaterializeNonPrimitiveType(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDataReaderUnableToMaterializeNonPrimitiveType", new object[] { p0, p1 });
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000086D8 File Offset: 0x000068D8
		internal static string ADP_KeysRequiredForJoinOverNest(object p0)
		{
			return EntityRes.GetString("ADP_KeysRequiredForJoinOverNest", new object[] { p0 });
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000086EE File Offset: 0x000068EE
		internal static string ADP_KeysRequiredForNesting
		{
			get
			{
				return EntityRes.GetString("ADP_KeysRequiredForNesting");
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000086FA File Offset: 0x000068FA
		internal static string ADP_NestingNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NestingNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00008714 File Offset: 0x00006914
		internal static string ADP_NoQueryMappingView(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NoQueryMappingView", new object[] { p0, p1 });
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000872E File Offset: 0x0000692E
		internal static string ADP_InternalProviderError(object p0)
		{
			return EntityRes.GetString("ADP_InternalProviderError", new object[] { p0 });
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00008744 File Offset: 0x00006944
		internal static string ADP_InvalidEnumerationValue(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidEnumerationValue", new object[] { p0, p1 });
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000875E File Offset: 0x0000695E
		internal static string ADP_InvalidBufferSizeOrIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidBufferSizeOrIndex", new object[] { p0, p1 });
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00008778 File Offset: 0x00006978
		internal static string ADP_InvalidDataLength(object p0)
		{
			return EntityRes.GetString("ADP_InvalidDataLength", new object[] { p0 });
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000878E File Offset: 0x0000698E
		internal static string ADP_InvalidDataType(object p0)
		{
			return EntityRes.GetString("ADP_InvalidDataType", new object[] { p0 });
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000087A4 File Offset: 0x000069A4
		internal static string ADP_InvalidDestinationBufferIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidDestinationBufferIndex", new object[] { p0, p1 });
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000087BE File Offset: 0x000069BE
		internal static string ADP_InvalidSourceBufferIndex(object p0, object p1)
		{
			return EntityRes.GetString("ADP_InvalidSourceBufferIndex", new object[] { p0, p1 });
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000087D8 File Offset: 0x000069D8
		internal static string ADP_NonSequentialChunkAccess(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ADP_NonSequentialChunkAccess", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000087F6 File Offset: 0x000069F6
		internal static string ADP_NonSequentialColumnAccess(object p0, object p1)
		{
			return EntityRes.GetString("ADP_NonSequentialColumnAccess", new object[] { p0, p1 });
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00008810 File Offset: 0x00006A10
		internal static string ADP_UnknownDataTypeCode(object p0, object p1)
		{
			return EntityRes.GetString("ADP_UnknownDataTypeCode", new object[] { p0, p1 });
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000882A File Offset: 0x00006A2A
		internal static string DataCategory_Data
		{
			get
			{
				return EntityRes.GetString("DataCategory_Data");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00008836 File Offset: 0x00006A36
		internal static string DbParameter_Direction
		{
			get
			{
				return EntityRes.GetString("DbParameter_Direction");
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00008842 File Offset: 0x00006A42
		internal static string DbParameter_Size
		{
			get
			{
				return EntityRes.GetString("DbParameter_Size");
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000884E File Offset: 0x00006A4E
		internal static string DataCategory_Update
		{
			get
			{
				return EntityRes.GetString("DataCategory_Update");
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000885A File Offset: 0x00006A5A
		internal static string DbParameter_SourceColumn
		{
			get
			{
				return EntityRes.GetString("DbParameter_SourceColumn");
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00008866 File Offset: 0x00006A66
		internal static string DbParameter_SourceVersion
		{
			get
			{
				return EntityRes.GetString("DbParameter_SourceVersion");
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00008872 File Offset: 0x00006A72
		internal static string ADP_CollectionParameterElementIsNull(object p0)
		{
			return EntityRes.GetString("ADP_CollectionParameterElementIsNull", new object[] { p0 });
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00008888 File Offset: 0x00006A88
		internal static string ADP_CollectionParameterElementIsNullOrEmpty(object p0)
		{
			return EntityRes.GetString("ADP_CollectionParameterElementIsNullOrEmpty", new object[] { p0 });
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000889E File Offset: 0x00006A9E
		internal static string ConstantFacetSpecifiedInSchema(object p0, object p1)
		{
			return EntityRes.GetString("ConstantFacetSpecifiedInSchema", new object[] { p0, p1 });
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000088B8 File Offset: 0x00006AB8
		internal static string DuplicateAnnotation(object p0, object p1)
		{
			return EntityRes.GetString("DuplicateAnnotation", new object[] { p0, p1 });
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000088D2 File Offset: 0x00006AD2
		internal static string EmptyFile(object p0)
		{
			return EntityRes.GetString("EmptyFile", new object[] { p0 });
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000088E8 File Offset: 0x00006AE8
		internal static string EmptySchemaTextReader
		{
			get
			{
				return EntityRes.GetString("EmptySchemaTextReader");
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000088F4 File Offset: 0x00006AF4
		internal static string EmptyName(object p0)
		{
			return EntityRes.GetString("EmptyName", new object[] { p0 });
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000890A File Offset: 0x00006B0A
		internal static string InvalidName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidName", new object[] { p0, p1 });
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00008924 File Offset: 0x00006B24
		internal static string MissingName
		{
			get
			{
				return EntityRes.GetString("MissingName");
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00008930 File Offset: 0x00006B30
		internal static string UnexpectedXmlAttribute(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlAttribute", new object[] { p0 });
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00008946 File Offset: 0x00006B46
		internal static string UnexpectedXmlElement(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlElement", new object[] { p0 });
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000895C File Offset: 0x00006B5C
		internal static string TextNotAllowed(object p0)
		{
			return EntityRes.GetString("TextNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00008972 File Offset: 0x00006B72
		internal static string UnexpectedXmlNodeType(object p0)
		{
			return EntityRes.GetString("UnexpectedXmlNodeType", new object[] { p0 });
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00008988 File Offset: 0x00006B88
		internal static string MalformedXml(object p0, object p1)
		{
			return EntityRes.GetString("MalformedXml", new object[] { p0, p1 });
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000089A2 File Offset: 0x00006BA2
		internal static string ValueNotUnderstood(object p0, object p1)
		{
			return EntityRes.GetString("ValueNotUnderstood", new object[] { p0, p1 });
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000089BC File Offset: 0x00006BBC
		internal static string EntityContainerAlreadyExists(object p0)
		{
			return EntityRes.GetString("EntityContainerAlreadyExists", new object[] { p0 });
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000089D2 File Offset: 0x00006BD2
		internal static string TypeNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("TypeNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000089E8 File Offset: 0x00006BE8
		internal static string PropertyNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("PropertyNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000089FE File Offset: 0x00006BFE
		internal static string DuplicateMemberNameInExtendedEntityContainer(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateMemberNameInExtendedEntityContainer", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00008A1C File Offset: 0x00006C1C
		internal static string DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("DuplicateEntityContainerMemberName", new object[] { p0 });
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00008A32 File Offset: 0x00006C32
		internal static string PropertyTypeAlreadyDefined(object p0)
		{
			return EntityRes.GetString("PropertyTypeAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00008A48 File Offset: 0x00006C48
		internal static string InvalidSize(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("InvalidSize", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00008A6A File Offset: 0x00006C6A
		internal static string BadNamespaceOrAlias(object p0)
		{
			return EntityRes.GetString("BadNamespaceOrAlias", new object[] { p0 });
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00008A80 File Offset: 0x00006C80
		internal static string InvalidBaseTypeForStructuredType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForStructuredType", new object[] { p0, p1 });
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00008A9A File Offset: 0x00006C9A
		internal static string InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("InvalidPropertyType", new object[] { p0 });
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00008AB0 File Offset: 0x00006CB0
		internal static string NullableComplexType(object p0)
		{
			return EntityRes.GetString("NullableComplexType", new object[] { p0 });
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00008AC6 File Offset: 0x00006CC6
		internal static string InvalidBaseTypeForItemType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForItemType", new object[] { p0, p1 });
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00008AE0 File Offset: 0x00006CE0
		internal static string InvalidBaseTypeForNestedType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidBaseTypeForNestedType", new object[] { p0, p1 });
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00008AFA File Offset: 0x00006CFA
		internal static string DefaultNotAllowed
		{
			get
			{
				return EntityRes.GetString("DefaultNotAllowed");
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00008B06 File Offset: 0x00006D06
		internal static string FacetNotAllowed(object p0, object p1)
		{
			return EntityRes.GetString("FacetNotAllowed", new object[] { p0, p1 });
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00008B20 File Offset: 0x00006D20
		internal static string RequiredFacetMissing(object p0, object p1)
		{
			return EntityRes.GetString("RequiredFacetMissing", new object[] { p0, p1 });
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00008B3A File Offset: 0x00006D3A
		internal static string InvalidDefaultBinaryWithNoMaxLength(object p0)
		{
			return EntityRes.GetString("InvalidDefaultBinaryWithNoMaxLength", new object[] { p0 });
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00008B50 File Offset: 0x00006D50
		internal static string InvalidDefaultIntegral(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultIntegral", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00008B6E File Offset: 0x00006D6E
		internal static string InvalidDefaultDateTime(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultDateTime", new object[] { p0, p1 });
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00008B88 File Offset: 0x00006D88
		internal static string InvalidDefaultTime(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultTime", new object[] { p0, p1 });
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00008BA2 File Offset: 0x00006DA2
		internal static string InvalidDefaultDateTimeOffset(object p0, object p1)
		{
			return EntityRes.GetString("InvalidDefaultDateTimeOffset", new object[] { p0, p1 });
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00008BBC File Offset: 0x00006DBC
		internal static string InvalidDefaultDecimal(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultDecimal", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00008BDA File Offset: 0x00006DDA
		internal static string InvalidDefaultFloatingPoint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidDefaultFloatingPoint", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00008BF8 File Offset: 0x00006DF8
		internal static string InvalidDefaultGuid(object p0)
		{
			return EntityRes.GetString("InvalidDefaultGuid", new object[] { p0 });
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00008C0E File Offset: 0x00006E0E
		internal static string InvalidDefaultBoolean(object p0)
		{
			return EntityRes.GetString("InvalidDefaultBoolean", new object[] { p0 });
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00008C24 File Offset: 0x00006E24
		internal static string DuplicateMemberName(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateMemberName", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00008C42 File Offset: 0x00006E42
		internal static string GeneratorErrorSeverityError
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityError");
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00008C4E File Offset: 0x00006E4E
		internal static string GeneratorErrorSeverityWarning
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityWarning");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00008C5A File Offset: 0x00006E5A
		internal static string GeneratorErrorSeverityUnknown
		{
			get
			{
				return EntityRes.GetString("GeneratorErrorSeverityUnknown");
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00008C66 File Offset: 0x00006E66
		internal static string SourceUriUnknown
		{
			get
			{
				return EntityRes.GetString("SourceUriUnknown");
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00008C72 File Offset: 0x00006E72
		internal static string BadPrecisionAndScale(object p0, object p1)
		{
			return EntityRes.GetString("BadPrecisionAndScale", new object[] { p0, p1 });
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00008C8C File Offset: 0x00006E8C
		internal static string InvalidNamespaceInUsing(object p0)
		{
			return EntityRes.GetString("InvalidNamespaceInUsing", new object[] { p0 });
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00008CA2 File Offset: 0x00006EA2
		internal static string BadNavigationPropertyRelationshipNotRelationship(object p0)
		{
			return EntityRes.GetString("BadNavigationPropertyRelationshipNotRelationship", new object[] { p0 });
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00008CB8 File Offset: 0x00006EB8
		internal static string BadNavigationPropertyRolesCannotBeTheSame
		{
			get
			{
				return EntityRes.GetString("BadNavigationPropertyRolesCannotBeTheSame");
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00008CC4 File Offset: 0x00006EC4
		internal static string BadNavigationPropertyUndefinedRole(object p0, object p1)
		{
			return EntityRes.GetString("BadNavigationPropertyUndefinedRole", new object[] { p0, p1 });
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00008CDE File Offset: 0x00006EDE
		internal static string BadNavigationPropertyBadFromRoleType(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("BadNavigationPropertyBadFromRoleType", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00008D05 File Offset: 0x00006F05
		internal static string InvalidMemberNameMatchesTypeName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMemberNameMatchesTypeName", new object[] { p0, p1 });
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00008D1F File Offset: 0x00006F1F
		internal static string InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyKeyDefinedInBaseClass", new object[] { p0, p1 });
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00008D39 File Offset: 0x00006F39
		internal static string InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyNullablePart", new object[] { p0, p1 });
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00008D53 File Offset: 0x00006F53
		internal static string InvalidKeyNoProperty(object p0, object p1)
		{
			return EntityRes.GetString("InvalidKeyNoProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00008D6D File Offset: 0x00006F6D
		internal static string KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("KeyMissingOnEntityType", new object[] { p0 });
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00008D83 File Offset: 0x00006F83
		internal static string InvalidDocumentationBothTextAndStructure
		{
			get
			{
				return EntityRes.GetString("InvalidDocumentationBothTextAndStructure");
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00008D8F File Offset: 0x00006F8F
		internal static string ArgumentOutOfRangeExpectedPostiveNumber(object p0)
		{
			return EntityRes.GetString("ArgumentOutOfRangeExpectedPostiveNumber", new object[] { p0 });
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00008DA5 File Offset: 0x00006FA5
		internal static string ArgumentOutOfRange(object p0)
		{
			return EntityRes.GetString("ArgumentOutOfRange", new object[] { p0 });
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00008DBB File Offset: 0x00006FBB
		internal static string UnacceptableUri(object p0)
		{
			return EntityRes.GetString("UnacceptableUri", new object[] { p0 });
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00008DD1 File Offset: 0x00006FD1
		internal static string UnexpectedTypeInCollection(object p0, object p1)
		{
			return EntityRes.GetString("UnexpectedTypeInCollection", new object[] { p0, p1 });
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00008DEB File Offset: 0x00006FEB
		internal static string AllElementsMustBeInSchema
		{
			get
			{
				return EntityRes.GetString("AllElementsMustBeInSchema");
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00008DF7 File Offset: 0x00006FF7
		internal static string AliasNameIsAlreadyDefined(object p0)
		{
			return EntityRes.GetString("AliasNameIsAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00008E0D File Offset: 0x0000700D
		internal static string NeedNotUseSystemNamespaceInUsing(object p0)
		{
			return EntityRes.GetString("NeedNotUseSystemNamespaceInUsing", new object[] { p0 });
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00008E23 File Offset: 0x00007023
		internal static string CannotUseSystemNamespaceAsAlias(object p0)
		{
			return EntityRes.GetString("CannotUseSystemNamespaceAsAlias", new object[] { p0 });
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00008E39 File Offset: 0x00007039
		internal static string EntitySetTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EntitySetTypeHasNoKeys", new object[] { p0, p1 });
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00008E53 File Offset: 0x00007053
		internal static string TableAndSchemaAreMutuallyExclusiveWithDefiningQuery(object p0)
		{
			return EntityRes.GetString("TableAndSchemaAreMutuallyExclusiveWithDefiningQuery", new object[] { p0 });
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00008E69 File Offset: 0x00007069
		internal static string UnexpectedRootElement(object p0, object p1, object p2)
		{
			return EntityRes.GetString("UnexpectedRootElement", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00008E87 File Offset: 0x00007087
		internal static string UnexpectedRootElementNoNamespace(object p0, object p1, object p2)
		{
			return EntityRes.GetString("UnexpectedRootElementNoNamespace", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00008EA5 File Offset: 0x000070A5
		internal static string ParameterNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("ParameterNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00008EBB File Offset: 0x000070BB
		internal static string FunctionWithNonPrimitiveTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("FunctionWithNonPrimitiveTypeNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00008ED5 File Offset: 0x000070D5
		internal static string FunctionWithNonEdmPrimitiveTypeNotSupported(object p0, object p1)
		{
			return EntityRes.GetString("FunctionWithNonEdmPrimitiveTypeNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00008EEF File Offset: 0x000070EF
		internal static string FunctionImportWithUnsupportedReturnTypeV1(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV1", new object[] { p0 });
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00008F05 File Offset: 0x00007105
		internal static string FunctionImportWithUnsupportedReturnTypeV1_1(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV1_1", new object[] { p0 });
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00008F1B File Offset: 0x0000711B
		internal static string FunctionImportWithUnsupportedReturnTypeV2(object p0)
		{
			return EntityRes.GetString("FunctionImportWithUnsupportedReturnTypeV2", new object[] { p0 });
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00008F31 File Offset: 0x00007131
		internal static string FunctionImportUnknownEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("FunctionImportUnknownEntitySet", new object[] { p0, p1 });
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00008F4B File Offset: 0x0000714B
		internal static string FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet(object p0)
		{
			return EntityRes.GetString("FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet", new object[] { p0 });
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00008F61 File Offset: 0x00007161
		internal static string FunctionImportEntityTypeDoesNotMatchEntitySet(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("FunctionImportEntityTypeDoesNotMatchEntitySet", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00008F83 File Offset: 0x00007183
		internal static string FunctionImportSpecifiesEntitySetButNotEntityType(object p0, object p1)
		{
			return EntityRes.GetString("FunctionImportSpecifiesEntitySetButNotEntityType", new object[] { p0, p1 });
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00008F9D File Offset: 0x0000719D
		internal static string TVFReturnTypeRowHasNonScalarProperty
		{
			get
			{
				return EntityRes.GetString("TVFReturnTypeRowHasNonScalarProperty");
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00008FA9 File Offset: 0x000071A9
		internal static string DuplicateEntitySetTable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("DuplicateEntitySetTable", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00008FC7 File Offset: 0x000071C7
		internal static string ConcurrencyRedefinedOnSubTypeOfEntitySetType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ConcurrencyRedefinedOnSubTypeOfEntitySetType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00008FE5 File Offset: 0x000071E5
		internal static string SimilarRelationshipEnd(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("SimilarRelationshipEnd", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000900C File Offset: 0x0000720C
		internal static string InvalidRelationshipEndMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipEndMultiplicity", new object[] { p0, p1 });
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00009026 File Offset: 0x00007226
		internal static string EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EndNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000903C File Offset: 0x0000723C
		internal static string InvalidRelationshipEndType(object p0, object p1)
		{
			return EntityRes.GetString("InvalidRelationshipEndType", new object[] { p0, p1 });
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00009056 File Offset: 0x00007256
		internal static string BadParameterDirection(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("BadParameterDirection", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00009078 File Offset: 0x00007278
		internal static string InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00009084 File Offset: 0x00007284
		internal static string InvalidAction(object p0, object p1)
		{
			return EntityRes.GetString("InvalidAction", new object[] { p0, p1 });
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000909E File Offset: 0x0000729E
		internal static string DuplicationOperation(object p0)
		{
			return EntityRes.GetString("DuplicationOperation", new object[] { p0 });
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000090B4 File Offset: 0x000072B4
		internal static string NotInNamespaceAlias(object p0, object p1, object p2)
		{
			return EntityRes.GetString("NotInNamespaceAlias", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000090D2 File Offset: 0x000072D2
		internal static string NotNamespaceQualified(object p0)
		{
			return EntityRes.GetString("NotNamespaceQualified", new object[] { p0 });
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000090E8 File Offset: 0x000072E8
		internal static string NotInNamespaceNoAlias(object p0, object p1)
		{
			return EntityRes.GetString("NotInNamespaceNoAlias", new object[] { p0, p1 });
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00009102 File Offset: 0x00007302
		internal static string InvalidValueForParameterTypeSemanticsAttribute(object p0)
		{
			return EntityRes.GetString("InvalidValueForParameterTypeSemanticsAttribute", new object[] { p0 });
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00009118 File Offset: 0x00007318
		internal static string DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("DuplicatePropertyNameSpecifiedInEntityKey", new object[] { p0, p1 });
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00009132 File Offset: 0x00007332
		internal static string InvalidEntitySetType(object p0)
		{
			return EntityRes.GetString("InvalidEntitySetType", new object[] { p0 });
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00009148 File Offset: 0x00007348
		internal static string InvalidRelationshipSetType(object p0)
		{
			return EntityRes.GetString("InvalidRelationshipSetType", new object[] { p0 });
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000915E File Offset: 0x0000735E
		internal static string InvalidEntityContainerNameInExtends(object p0)
		{
			return EntityRes.GetString("InvalidEntityContainerNameInExtends", new object[] { p0 });
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00009174 File Offset: 0x00007374
		internal static string InvalidNamespaceOrAliasSpecified(object p0)
		{
			return EntityRes.GetString("InvalidNamespaceOrAliasSpecified", new object[] { p0 });
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000918A File Offset: 0x0000738A
		internal static string PrecisionOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("PrecisionOutOfRange", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000091AC File Offset: 0x000073AC
		internal static string ScaleOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("ScaleOutOfRange", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000091CE File Offset: 0x000073CE
		internal static string InvalidEntitySetNameReference(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntitySetNameReference", new object[] { p0, p1 });
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000091E8 File Offset: 0x000073E8
		internal static string InvalidEntityEndName(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEntityEndName", new object[] { p0, p1 });
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00009202 File Offset: 0x00007402
		internal static string DuplicateEndName(object p0)
		{
			return EntityRes.GetString("DuplicateEndName", new object[] { p0 });
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00009218 File Offset: 0x00007418
		internal static string AmbiguousEntityContainerEnd(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousEntityContainerEnd", new object[] { p0, p1 });
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00009232 File Offset: 0x00007432
		internal static string MissingEntityContainerEnd(object p0, object p1)
		{
			return EntityRes.GetString("MissingEntityContainerEnd", new object[] { p0, p1 });
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000924C File Offset: 0x0000744C
		internal static string InvalidEndEntitySetTypeMismatch(object p0)
		{
			return EntityRes.GetString("InvalidEndEntitySetTypeMismatch", new object[] { p0 });
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00009262 File Offset: 0x00007462
		internal static string InferRelationshipEndFailedNoEntitySetMatch(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("InferRelationshipEndFailedNoEntitySetMatch", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00009289 File Offset: 0x00007489
		internal static string InferRelationshipEndAmbiguous(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("InferRelationshipEndAmbiguous", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000092B0 File Offset: 0x000074B0
		internal static string InferRelationshipEndGivesAlreadyDefinedEnd(object p0, object p1)
		{
			return EntityRes.GetString("InferRelationshipEndGivesAlreadyDefinedEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000092CA File Offset: 0x000074CA
		internal static string TooManyAssociationEnds(object p0)
		{
			return EntityRes.GetString("TooManyAssociationEnds", new object[] { p0 });
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000092E0 File Offset: 0x000074E0
		internal static string InvalidEndRoleInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("InvalidEndRoleInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000092FA File Offset: 0x000074FA
		internal static string InvalidFromPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidFromPropertyInRelationshipConstraint", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00009318 File Offset: 0x00007518
		internal static string InvalidToPropertyInRelationshipConstraint(object p0, object p1, object p2)
		{
			return EntityRes.GetString("InvalidToPropertyInRelationshipConstraint", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00009336 File Offset: 0x00007536
		internal static string InvalidPropertyInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("InvalidPropertyInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00009350 File Offset: 0x00007550
		internal static string TypeMismatchRelationshipConstaint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("TypeMismatchRelationshipConstaint", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00009377 File Offset: 0x00007577
		internal static string InvalidMultiplicityFromRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleUpperBoundMustBeOne", new object[] { p0, p1 });
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00009391 File Offset: 0x00007591
		internal static string InvalidMultiplicityFromRoleToPropertyNonNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNonNullableV1", new object[] { p0, p1 });
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000093AB File Offset: 0x000075AB
		internal static string InvalidMultiplicityFromRoleToPropertyNonNullableV2(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNonNullableV2", new object[] { p0, p1 });
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000093C5 File Offset: 0x000075C5
		internal static string InvalidMultiplicityFromRoleToPropertyNullableV1(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityFromRoleToPropertyNullableV1", new object[] { p0, p1 });
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000093DF File Offset: 0x000075DF
		internal static string InvalidMultiplicityToRoleLowerBoundMustBeZero(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleLowerBoundMustBeZero", new object[] { p0, p1 });
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000093F9 File Offset: 0x000075F9
		internal static string InvalidMultiplicityToRoleUpperBoundMustBeOne(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleUpperBoundMustBeOne", new object[] { p0, p1 });
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00009413 File Offset: 0x00007613
		internal static string InvalidMultiplicityToRoleUpperBoundMustBeMany(object p0, object p1)
		{
			return EntityRes.GetString("InvalidMultiplicityToRoleUpperBoundMustBeMany", new object[] { p0, p1 });
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000942D File Offset: 0x0000762D
		internal static string MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00009439 File Offset: 0x00007639
		internal static string MissingConstraintOnRelationshipType(object p0)
		{
			return EntityRes.GetString("MissingConstraintOnRelationshipType", new object[] { p0 });
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000944F File Offset: 0x0000764F
		internal static string SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("SameRoleReferredInReferentialConstraint", new object[] { p0 });
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00009465 File Offset: 0x00007665
		internal static string InvalidPrimitiveTypeKind(object p0)
		{
			return EntityRes.GetString("InvalidPrimitiveTypeKind", new object[] { p0 });
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000947B File Offset: 0x0000767B
		internal static string EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EntityKeyMustBeScalar", new object[] { p0, p1 });
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00009495 File Offset: 0x00007695
		internal static string EntityKeyTypeCurrentlyNotSupportedInSSDL(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EntityKeyTypeCurrentlyNotSupportedInSSDL", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000094BC File Offset: 0x000076BC
		internal static string EntityKeyTypeCurrentlyNotSupported(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EntityKeyTypeCurrentlyNotSupported", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000094DA File Offset: 0x000076DA
		internal static string MissingFacetDescription(object p0, object p1, object p2)
		{
			return EntityRes.GetString("MissingFacetDescription", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000094F8 File Offset: 0x000076F8
		internal static string EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0, object p1)
		{
			return EntityRes.GetString("EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[] { p0, p1 });
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00009512 File Offset: 0x00007712
		internal static string EndWithoutMultiplicity(object p0, object p1)
		{
			return EntityRes.GetString("EndWithoutMultiplicity", new object[] { p0, p1 });
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000952C File Offset: 0x0000772C
		internal static string EntityContainerCannotExtendItself(object p0)
		{
			return EntityRes.GetString("EntityContainerCannotExtendItself", new object[] { p0 });
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00009542 File Offset: 0x00007742
		internal static string ComposableFunctionMustDeclareReturnType
		{
			get
			{
				return EntityRes.GetString("ComposableFunctionMustDeclareReturnType");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000954E File Offset: 0x0000774E
		internal static string NonComposableFunctionMustNotDeclareReturnType
		{
			get
			{
				return EntityRes.GetString("NonComposableFunctionMustNotDeclareReturnType");
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000955A File Offset: 0x0000775A
		internal static string CommandTextFunctionsNotComposable
		{
			get
			{
				return EntityRes.GetString("CommandTextFunctionsNotComposable");
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00009566 File Offset: 0x00007766
		internal static string CommandTextFunctionsCannotDeclareStoreFunctionName
		{
			get
			{
				return EntityRes.GetString("CommandTextFunctionsCannotDeclareStoreFunctionName");
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00009572 File Offset: 0x00007772
		internal static string NonComposableFunctionHasDisallowedAttribute
		{
			get
			{
				return EntityRes.GetString("NonComposableFunctionHasDisallowedAttribute");
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000957E File Offset: 0x0000777E
		internal static string EmptyDefiningQuery
		{
			get
			{
				return EntityRes.GetString("EmptyDefiningQuery");
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000958A File Offset: 0x0000778A
		internal static string EmptyCommandText
		{
			get
			{
				return EntityRes.GetString("EmptyCommandText");
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00009596 File Offset: 0x00007796
		internal static string AmbiguousFunctionOverload(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousFunctionOverload", new object[] { p0, p1 });
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000095B0 File Offset: 0x000077B0
		internal static string AmbiguousFunctionAndType(object p0, object p1)
		{
			return EntityRes.GetString("AmbiguousFunctionAndType", new object[] { p0, p1 });
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000095CA File Offset: 0x000077CA
		internal static string CycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("CycleInTypeHierarchy", new object[] { p0 });
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000095E0 File Offset: 0x000077E0
		internal static string IncorrectProviderManifest
		{
			get
			{
				return EntityRes.GetString("IncorrectProviderManifest");
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000095EC File Offset: 0x000077EC
		internal static string ComplexTypeAsReturnTypeAndDefinedEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ComplexTypeAsReturnTypeAndDefinedEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000960A File Offset: 0x0000780A
		internal static string ComplexTypeAsReturnTypeAndNestedComplexProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("ComplexTypeAsReturnTypeAndNestedComplexProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00009628 File Offset: 0x00007828
		internal static string FacetsOnNonScalarType(object p0)
		{
			return EntityRes.GetString("FacetsOnNonScalarType", new object[] { p0 });
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000963E File Offset: 0x0000783E
		internal static string FacetDeclarationRequiresTypeAttribute
		{
			get
			{
				return EntityRes.GetString("FacetDeclarationRequiresTypeAttribute");
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000964A File Offset: 0x0000784A
		internal static string TypeMustBeDeclared
		{
			get
			{
				return EntityRes.GetString("TypeMustBeDeclared");
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00009656 File Offset: 0x00007856
		internal static string RowTypeWithoutProperty
		{
			get
			{
				return EntityRes.GetString("RowTypeWithoutProperty");
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00009662 File Offset: 0x00007862
		internal static string TypeDeclaredAsAttributeAndElement
		{
			get
			{
				return EntityRes.GetString("TypeDeclaredAsAttributeAndElement");
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000966E File Offset: 0x0000786E
		internal static string ReferenceToNonEntityType(object p0)
		{
			return EntityRes.GetString("ReferenceToNonEntityType", new object[] { p0 });
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00009684 File Offset: 0x00007884
		internal static string NoCodeGenNamespaceInStructuralAnnotation(object p0)
		{
			return EntityRes.GetString("NoCodeGenNamespaceInStructuralAnnotation", new object[] { p0 });
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000969A File Offset: 0x0000789A
		internal static string CannotLoadDifferentVersionOfSchemaInTheSameItemCollection
		{
			get
			{
				return EntityRes.GetString("CannotLoadDifferentVersionOfSchemaInTheSameItemCollection");
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000096A6 File Offset: 0x000078A6
		internal static string ObjectQuery_QueryBuilder_InvalidProjectionList
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidProjectionList");
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x000096B2 File Offset: 0x000078B2
		internal static string ObjectQuery_QueryBuilder_InvalidSortKeyList
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidSortKeyList");
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000096BE File Offset: 0x000078BE
		internal static string ObjectQuery_QueryBuilder_InvalidGroupKeyList
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidGroupKeyList");
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x000096CA File Offset: 0x000078CA
		internal static string ObjectQuery_QueryBuilder_InvalidSkipCount
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidSkipCount");
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000096D6 File Offset: 0x000078D6
		internal static string ObjectQuery_QueryBuilder_InvalidTopCount
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidTopCount");
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000096E2 File Offset: 0x000078E2
		internal static string ObjectQuery_QueryBuilder_InvalidFilterPredicate
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidFilterPredicate");
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000096EE File Offset: 0x000078EE
		internal static string ObjectQuery_QueryBuilder_InvalidResultType(object p0)
		{
			return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidResultType", new object[] { p0 });
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00009704 File Offset: 0x00007904
		internal static string ObjectQuery_QueryBuilder_InvalidQueryArgument
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_InvalidQueryArgument");
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00009710 File Offset: 0x00007910
		internal static string ObjectQuery_QueryBuilder_NotSupportedLinqSource
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_QueryBuilder_NotSupportedLinqSource");
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000971C File Offset: 0x0000791C
		internal static string ObjectQuery_InvalidEmptyQuery
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_InvalidEmptyQuery");
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00009728 File Offset: 0x00007928
		internal static string ObjectQuery_InvalidConnection
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_InvalidConnection");
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00009734 File Offset: 0x00007934
		internal static string ObjectQuery_InvalidQueryName(object p0)
		{
			return EntityRes.GetString("ObjectQuery_InvalidQueryName", new object[] { p0 });
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000974A File Offset: 0x0000794A
		internal static string ObjectQuery_UnableToMapResultType
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_UnableToMapResultType");
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00009756 File Offset: 0x00007956
		internal static string ObjectQuery_UnableToMaterializeArray(object p0, object p1)
		{
			return EntityRes.GetString("ObjectQuery_UnableToMaterializeArray", new object[] { p0, p1 });
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00009770 File Offset: 0x00007970
		internal static string ObjectQuery_UnableToMaterializeArbitaryProjectionType(object p0)
		{
			return EntityRes.GetString("ObjectQuery_UnableToMaterializeArbitaryProjectionType", new object[] { p0 });
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00009786 File Offset: 0x00007986
		internal static string ObjectParameter_InvalidParameterName(object p0)
		{
			return EntityRes.GetString("ObjectParameter_InvalidParameterName", new object[] { p0 });
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000979C File Offset: 0x0000799C
		internal static string ObjectParameter_InvalidParameterType(object p0)
		{
			return EntityRes.GetString("ObjectParameter_InvalidParameterType", new object[] { p0 });
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000097B2 File Offset: 0x000079B2
		internal static string ObjectParameterCollection_ParameterNameNotFound(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_ParameterNameNotFound", new object[] { p0 });
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000097C8 File Offset: 0x000079C8
		internal static string ObjectParameterCollection_ParameterAlreadyExists(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_ParameterAlreadyExists", new object[] { p0 });
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000097DE File Offset: 0x000079DE
		internal static string ObjectParameterCollection_DuplicateParameterName(object p0)
		{
			return EntityRes.GetString("ObjectParameterCollection_DuplicateParameterName", new object[] { p0 });
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000097F4 File Offset: 0x000079F4
		internal static string ObjectParameterCollection_ParametersLocked
		{
			get
			{
				return EntityRes.GetString("ObjectParameterCollection_ParametersLocked");
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00009800 File Offset: 0x00007A00
		internal static string ProviderReturnedNullForGetDbInformation(object p0)
		{
			return EntityRes.GetString("ProviderReturnedNullForGetDbInformation", new object[] { p0 });
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00009816 File Offset: 0x00007A16
		internal static string ProviderReturnedNullForCreateCommandDefinition
		{
			get
			{
				return EntityRes.GetString("ProviderReturnedNullForCreateCommandDefinition");
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00009822 File Offset: 0x00007A22
		internal static string ProviderDidNotReturnAProviderManifest
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnAProviderManifest");
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000982E File Offset: 0x00007A2E
		internal static string ProviderDidNotReturnAProviderManifestToken
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotReturnAProviderManifestToken");
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000983A File Offset: 0x00007A3A
		internal static string ProviderDoesNotSupportType(object p0)
		{
			return EntityRes.GetString("ProviderDoesNotSupportType", new object[] { p0 });
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00009850 File Offset: 0x00007A50
		internal static string NoStoreTypeForEdmType(object p0, object p1)
		{
			return EntityRes.GetString("NoStoreTypeForEdmType", new object[] { p0, p1 });
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0000986A File Offset: 0x00007A6A
		internal static string ProviderRequiresStoreCommandTree
		{
			get
			{
				return EntityRes.GetString("ProviderRequiresStoreCommandTree");
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00009876 File Offset: 0x00007A76
		internal static string ProviderShouldOverrideEscapeLikeArgument
		{
			get
			{
				return EntityRes.GetString("ProviderShouldOverrideEscapeLikeArgument");
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00009882 File Offset: 0x00007A82
		internal static string ProviderEscapeLikeArgumentReturnedNull
		{
			get
			{
				return EntityRes.GetString("ProviderEscapeLikeArgumentReturnedNull");
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000988E File Offset: 0x00007A8E
		internal static string ProviderDidNotCreateACommandDefinition
		{
			get
			{
				return EntityRes.GetString("ProviderDidNotCreateACommandDefinition");
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000989A File Offset: 0x00007A9A
		internal static string ProviderDoesNotSupportCreateDatabaseScript
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportCreateDatabaseScript");
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000098A6 File Offset: 0x00007AA6
		internal static string ProviderDoesNotSupportCreateDatabase
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportCreateDatabase");
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000098B2 File Offset: 0x00007AB2
		internal static string ProviderDoesNotSupportDatabaseExists
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportDatabaseExists");
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000098BE File Offset: 0x00007ABE
		internal static string ProviderDoesNotSupportDeleteDatabase
		{
			get
			{
				return EntityRes.GetString("ProviderDoesNotSupportDeleteDatabase");
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000098CA File Offset: 0x00007ACA
		internal static string EntityConnectionString_Name
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Name");
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000098D6 File Offset: 0x00007AD6
		internal static string EntityConnectionString_Provider
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Provider");
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000098E2 File Offset: 0x00007AE2
		internal static string EntityConnectionString_Metadata
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_Metadata");
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000098EE File Offset: 0x00007AEE
		internal static string EntityConnectionString_ProviderConnectionString
		{
			get
			{
				return EntityRes.GetString("EntityConnectionString_ProviderConnectionString");
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000098FA File Offset: 0x00007AFA
		internal static string EntityDataCategory_Context
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_Context");
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00009906 File Offset: 0x00007B06
		internal static string EntityDataCategory_NamedConnectionString
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_NamedConnectionString");
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00009912 File Offset: 0x00007B12
		internal static string EntityDataCategory_Source
		{
			get
			{
				return EntityRes.GetString("EntityDataCategory_Source");
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000991E File Offset: 0x00007B1E
		internal static string ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection");
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000992A File Offset: 0x00007B2A
		internal static string ObjectQuery_Span_NoNavProp(object p0, object p1)
		{
			return EntityRes.GetString("ObjectQuery_Span_NoNavProp", new object[] { p0, p1 });
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00009944 File Offset: 0x00007B44
		internal static string ObjectQuery_Span_SpanPathSyntaxError
		{
			get
			{
				return EntityRes.GetString("ObjectQuery_Span_SpanPathSyntaxError");
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00009950 File Offset: 0x00007B50
		internal static string EntityProxyTypeInfo_ProxyHasWrongWrapper
		{
			get
			{
				return EntityRes.GetString("EntityProxyTypeInfo_ProxyHasWrongWrapper");
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000995C File Offset: 0x00007B5C
		internal static string EntityProxyTypeInfo_CannotSetEntityCollectionProperty(object p0, object p1)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_CannotSetEntityCollectionProperty", new object[] { p0, p1 });
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00009976 File Offset: 0x00007B76
		internal static string EntityProxyTypeInfo_ProxyMetadataIsUnavailable(object p0)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_ProxyMetadataIsUnavailable", new object[] { p0 });
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000998C File Offset: 0x00007B8C
		internal static string EntityProxyTypeInfo_DuplicateOSpaceType(object p0)
		{
			return EntityRes.GetString("EntityProxyTypeInfo_DuplicateOSpaceType", new object[] { p0 });
		}
	}
}
