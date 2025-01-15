using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000B5 RID: 181
	internal enum EventNotificationEventGroup
	{
		// Token: 0x04000421 RID: 1057
		Unknown,
		// Token: 0x04000422 RID: 1058
		DdlEvents = 10001,
		// Token: 0x04000423 RID: 1059
		DdlServerLevelEvents,
		// Token: 0x04000424 RID: 1060
		DdlEndpointEvents,
		// Token: 0x04000425 RID: 1061
		DdlDatabaseEvents,
		// Token: 0x04000426 RID: 1062
		DdlServerSecurityEvents,
		// Token: 0x04000427 RID: 1063
		DdlLoginEvents,
		// Token: 0x04000428 RID: 1064
		DdlGdrServerEvents,
		// Token: 0x04000429 RID: 1065
		DdlAuthorizationServerEvents,
		// Token: 0x0400042A RID: 1066
		DdlCredentialEvents,
		// Token: 0x0400042B RID: 1067
		DdlServiceMasterKeyEvents,
		// Token: 0x0400042C RID: 1068
		DdlExtendedProcedureEvents,
		// Token: 0x0400042D RID: 1069
		DdlLinkedServerEvents,
		// Token: 0x0400042E RID: 1070
		DdlLinkedServerLoginEvents,
		// Token: 0x0400042F RID: 1071
		DdlMessageEvents,
		// Token: 0x04000430 RID: 1072
		DdlRemoteServerEvents,
		// Token: 0x04000431 RID: 1073
		DdlDatabaseLevelEvents,
		// Token: 0x04000432 RID: 1074
		DdlTableViewEvents,
		// Token: 0x04000433 RID: 1075
		DdlTableEvents,
		// Token: 0x04000434 RID: 1076
		DdlViewEvents,
		// Token: 0x04000435 RID: 1077
		DdlIndexEvents,
		// Token: 0x04000436 RID: 1078
		DdlStatisticsEvents,
		// Token: 0x04000437 RID: 1079
		DdlSynonymEvents,
		// Token: 0x04000438 RID: 1080
		DdlFunctionEvents,
		// Token: 0x04000439 RID: 1081
		DdlProcedureEvents,
		// Token: 0x0400043A RID: 1082
		DdlTriggerEvents,
		// Token: 0x0400043B RID: 1083
		DdlEventNotificationEvents,
		// Token: 0x0400043C RID: 1084
		DdlAssemblyEvents,
		// Token: 0x0400043D RID: 1085
		DdlTypeEvents,
		// Token: 0x0400043E RID: 1086
		DdlDatabaseSecurityEvents,
		// Token: 0x0400043F RID: 1087
		DdlCertificateEvents,
		// Token: 0x04000440 RID: 1088
		DdlUserEvents,
		// Token: 0x04000441 RID: 1089
		DdlRoleEvents,
		// Token: 0x04000442 RID: 1090
		DdlApplicationRoleEvents,
		// Token: 0x04000443 RID: 1091
		DdlSchemaEvents,
		// Token: 0x04000444 RID: 1092
		DdlGdrDatabaseEvents,
		// Token: 0x04000445 RID: 1093
		DdlAuthorizationDatabaseEvents,
		// Token: 0x04000446 RID: 1094
		DdlSymmetricKeyEvents,
		// Token: 0x04000447 RID: 1095
		DdlAsymmetricKeyEvents,
		// Token: 0x04000448 RID: 1096
		DdlCryptoSignatureEvents,
		// Token: 0x04000449 RID: 1097
		DdlMasterKeyEvents,
		// Token: 0x0400044A RID: 1098
		DdlSsbEvents,
		// Token: 0x0400044B RID: 1099
		DdlMessageTypeEvents,
		// Token: 0x0400044C RID: 1100
		DdlContractEvents,
		// Token: 0x0400044D RID: 1101
		DdlQueueEvents,
		// Token: 0x0400044E RID: 1102
		DdlServiceEvents,
		// Token: 0x0400044F RID: 1103
		DdlRouteEvents,
		// Token: 0x04000450 RID: 1104
		DdlRemoteServiceBindingEvents,
		// Token: 0x04000451 RID: 1105
		DdlXmlSchemaCollectionEvents,
		// Token: 0x04000452 RID: 1106
		DdlPartitionEvents,
		// Token: 0x04000453 RID: 1107
		DdlPartitionFunctionEvents,
		// Token: 0x04000454 RID: 1108
		DdlPartitionSchemeEvents,
		// Token: 0x04000455 RID: 1109
		DdlDefaultEvents,
		// Token: 0x04000456 RID: 1110
		DdlExtendedPropertyEvents,
		// Token: 0x04000457 RID: 1111
		DdlFullTextCatalogEvents,
		// Token: 0x04000458 RID: 1112
		DdlPlanGuideEvents,
		// Token: 0x04000459 RID: 1113
		DdlRuleEvents,
		// Token: 0x0400045A RID: 1114
		DdlEventSessionEvents,
		// Token: 0x0400045B RID: 1115
		DdlResourceGovernorEvents,
		// Token: 0x0400045C RID: 1116
		DdlResourcePool,
		// Token: 0x0400045D RID: 1117
		DdlWorkloadGroup,
		// Token: 0x0400045E RID: 1118
		DdlCryptographicProviderEvents,
		// Token: 0x0400045F RID: 1119
		DdlDatabaseEncryptionKeyEvents,
		// Token: 0x04000460 RID: 1120
		DdlBrokerPriorityEvents,
		// Token: 0x04000461 RID: 1121
		DdlServerAuditEvents,
		// Token: 0x04000462 RID: 1122
		DdlServerAuditSpecificationEvents,
		// Token: 0x04000463 RID: 1123
		DdlDatabaseAuditSpecificationEvents,
		// Token: 0x04000464 RID: 1124
		DdlFullTextStopListEvents,
		// Token: 0x04000465 RID: 1125
		DdlSearchPropertyListEvents = 10069,
		// Token: 0x04000466 RID: 1126
		DdlSequenceEvents,
		// Token: 0x04000467 RID: 1127
		DdlAvailabilityGroupEvents,
		// Token: 0x04000468 RID: 1128
		TrcAllEvents = 11000,
		// Token: 0x04000469 RID: 1129
		TrcDatabase = 11002,
		// Token: 0x0400046A RID: 1130
		TrcErrorsAndWarnings,
		// Token: 0x0400046B RID: 1131
		TrcLocks,
		// Token: 0x0400046C RID: 1132
		TrcObjects,
		// Token: 0x0400046D RID: 1133
		TrcPerformance,
		// Token: 0x0400046E RID: 1134
		TrcSecurityAudit = 11008,
		// Token: 0x0400046F RID: 1135
		TrcServer,
		// Token: 0x04000470 RID: 1136
		TrcStoredProcedures = 11011,
		// Token: 0x04000471 RID: 1137
		TrcTSql = 11013,
		// Token: 0x04000472 RID: 1138
		TrcUserConfigurable,
		// Token: 0x04000473 RID: 1139
		TrcOledb,
		// Token: 0x04000474 RID: 1140
		TrcFullText = 11017,
		// Token: 0x04000475 RID: 1141
		TrcDeprecation,
		// Token: 0x04000476 RID: 1142
		TrcClr = 11020,
		// Token: 0x04000477 RID: 1143
		TrcQueryNotifications
	}
}
