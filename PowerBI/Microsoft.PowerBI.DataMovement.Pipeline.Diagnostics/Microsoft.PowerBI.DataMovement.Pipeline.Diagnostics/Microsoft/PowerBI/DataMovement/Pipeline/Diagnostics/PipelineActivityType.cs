using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x02000018 RID: 24
	public enum PipelineActivityType
	{
		// Token: 0x0400004A RID: 74
		TEST = 1413829460,
		// Token: 0x0400004B RID: 75
		MCGS = 1296254803,
		// Token: 0x0400004C RID: 76
		GatewayClientGlobalSetup = 1296254803,
		// Token: 0x0400004D RID: 77
		MCTG = 1296258119,
		// Token: 0x0400004E RID: 78
		GatewayClientTestConnection = 1296258119,
		// Token: 0x0400004F RID: 79
		MCTD = 1296258116,
		// Token: 0x04000050 RID: 80
		GatewayClientTestDataSourceConnection = 1296258116,
		// Token: 0x04000051 RID: 81
		MCEC = 1296254275,
		// Token: 0x04000052 RID: 82
		GatewayClientEncryptCredentialsWithTestConnection = 1296254275,
		// Token: 0x04000053 RID: 83
		MCDC = 1296254019,
		// Token: 0x04000054 RID: 84
		GatewayClientDisconnect = 1296254019,
		// Token: 0x04000055 RID: 85
		MCMC = 1296256323,
		// Token: 0x04000056 RID: 86
		GatewayClientDiscoverCustomConnectorsOnGateway = 1296256323,
		// Token: 0x04000057 RID: 87
		MCDD = 1296254020,
		// Token: 0x04000058 RID: 88
		GatewayClientDiscoverDataSourcesOnGateway = 1296254020,
		// Token: 0x04000059 RID: 89
		MCDH = 1296254024,
		// Token: 0x0400005A RID: 90
		GatewayClientDiscoverDataSourcesOnGatewayUsingMashupDSR = 1296254024,
		// Token: 0x0400005B RID: 91
		MCOI = 1296256841,
		// Token: 0x0400005C RID: 92
		GatewayClientOAuthInitiateLoginOnGateway = 1296256841,
		// Token: 0x0400005D RID: 93
		MCOF = 1296256838,
		// Token: 0x0400005E RID: 94
		GatewayClientOAuthFinishLoginOnGateway = 1296256838,
		// Token: 0x0400005F RID: 95
		MCTR = 1296258130,
		// Token: 0x04000060 RID: 96
		GatewayClientOAuthRenewTokenOnGateway = 1296258130,
		// Token: 0x04000061 RID: 97
		GRDA = 1196573761,
		// Token: 0x04000062 RID: 98
		GatewayClientRenewDiagnosticsAccess = 1196573761,
		// Token: 0x04000063 RID: 99
		MCOR = 1296256850,
		// Token: 0x04000064 RID: 100
		GatewayClientGetOAuthResource = 1296256850,
		// Token: 0x04000065 RID: 101
		MBOC = 1296191299,
		// Token: 0x04000066 RID: 102
		AdoConnectionExecuteOpenConnection = 1296191299,
		// Token: 0x04000067 RID: 103
		MAOC = 1296125763,
		// Token: 0x04000068 RID: 104
		AdoNetProviderOpenConnection = 1296125763,
		// Token: 0x04000069 RID: 105
		MACL = 1296122700,
		// Token: 0x0400006A RID: 106
		AdoNetProviderCloseConnection = 1296122700,
		// Token: 0x0400006B RID: 107
		MADM = 1296122957,
		// Token: 0x0400006C RID: 108
		AdoNetProviderDiscoverMoniker = 1296122957,
		// Token: 0x0400006D RID: 109
		MACR = 1296122706,
		// Token: 0x0400006E RID: 110
		AdoNetProviderCreateAndExecuteDbDataReader = 1296122706,
		// Token: 0x0400006F RID: 111
		MAER = 1296123218,
		// Token: 0x04000070 RID: 112
		AdoNetProviderExecuteDbDataReader = 1296123218,
		// Token: 0x04000071 RID: 113
		MASG = 1296126791,
		// Token: 0x04000072 RID: 114
		AdoNetProviderGetStatus = 1296126791,
		// Token: 0x04000073 RID: 115
		MAES = 1296123219,
		// Token: 0x04000074 RID: 116
		AdoNetProviderExecuteScalar = 1296123219,
		// Token: 0x04000075 RID: 117
		MARP = 1296126544,
		// Token: 0x04000076 RID: 118
		AdoNetProviderReadNextPage = 1296126544,
		// Token: 0x04000077 RID: 119
		MACC = 1296122691,
		// Token: 0x04000078 RID: 120
		AdoNetProviderTransferCancelCommand = 1296122691,
		// Token: 0x04000079 RID: 121
		MACA = 1296122689,
		// Token: 0x0400007A RID: 122
		AdoNetProviderTransferCancelAsyncCommand = 1296122689,
		// Token: 0x0400007B RID: 123
		MANR = 1296125522,
		// Token: 0x0400007C RID: 124
		AdoNetProviderNextResultDbDataReader = 1296125522,
		// Token: 0x0400007D RID: 125
		MPCC = 1297105731,
		// Token: 0x0400007E RID: 126
		GatewayMashupProviderCreateCommand = 1297105731,
		// Token: 0x0400007F RID: 127
		MPCO = 1297105743,
		// Token: 0x04000080 RID: 128
		GatewayMashupProviderCreateConnection = 1297105743,
		// Token: 0x04000081 RID: 129
		MMOC = 1296912195,
		// Token: 0x04000082 RID: 130
		AdomdProviderOpenConnection = 1296912195,
		// Token: 0x04000083 RID: 131
		MMGS = 1296910163,
		// Token: 0x04000084 RID: 132
		AdomdProviderGetSchemaDataSet = 1296910163,
		// Token: 0x04000085 RID: 133
		MMCE = 1296909125,
		// Token: 0x04000086 RID: 134
		AdomdProviderChangeEffectiveUser = 1296909125,
		// Token: 0x04000087 RID: 135
		MMGE = 1296910149,
		// Token: 0x04000088 RID: 136
		AdomdProviderExecuteGetSchemaDataSet = 1296910149,
		// Token: 0x04000089 RID: 137
		MMCR = 1296909138,
		// Token: 0x0400008A RID: 138
		AdomdProviderCreateAndExecuteDbDataReader = 1296909138,
		// Token: 0x0400008B RID: 139
		MMER = 1296909650,
		// Token: 0x0400008C RID: 140
		AdomdProviderExecuteDbDataReader = 1296909650,
		// Token: 0x0400008D RID: 141
		MMES,
		// Token: 0x0400008E RID: 142
		AdomdProviderExecuteScalar = 1296909651,
		// Token: 0x0400008F RID: 143
		MMRP = 1296912976,
		// Token: 0x04000090 RID: 144
		AdomdProviderReadNextPage = 1296912976,
		// Token: 0x04000091 RID: 145
		MMDC = 1296909379,
		// Token: 0x04000092 RID: 146
		AdomdProviderDiscoverConnection = 1296909379,
		// Token: 0x04000093 RID: 147
		MOID = 1297041732,
		// Token: 0x04000094 RID: 148
		OleDbProviderTransferDataSourceInitialize = 1297041732,
		// Token: 0x04000095 RID: 149
		MOSC = 1297044291,
		// Token: 0x04000096 RID: 150
		OleDbProviderTransferSessionGetSchemaRowset = 1297044291,
		// Token: 0x04000097 RID: 151
		MOCR = 1297040210,
		// Token: 0x04000098 RID: 152
		OleDbProviderTransferCommandCreateRowset = 1297040210,
		// Token: 0x04000099 RID: 153
		MOMD = 1297042756,
		// Token: 0x0400009A RID: 154
		OleDbProviderTransferCommandCreateMdDataset = 1297042756,
		// Token: 0x0400009B RID: 155
		MOCM = 1297040205,
		// Token: 0x0400009C RID: 156
		OleDbProviderTransferCommandCreateMultipleResults = 1297040205,
		// Token: 0x0400009D RID: 157
		MOGP = 1297041232,
		// Token: 0x0400009E RID: 158
		OleDbProviderTransferCommandGetNextPacket = 1297041232,
		// Token: 0x0400009F RID: 159
		MOCC = 1297040195,
		// Token: 0x040000A0 RID: 160
		OleDbProviderTransferCancelCommand = 1297040195,
		// Token: 0x040000A1 RID: 161
		MWSR = 1297568594,
		// Token: 0x040000A2 RID: 162
		HttpWebProviderSendRequest = 1297568594,
		// Token: 0x040000A3 RID: 163
		MWRR = 1297568338,
		// Token: 0x040000A4 RID: 164
		HttpWebProviderReceiveResponse = 1297568338,
		// Token: 0x040000A5 RID: 165
		MWPR = 1297567826,
		// Token: 0x040000A6 RID: 166
		HttpWebProviderProcessRequest = 1297567826,
		// Token: 0x040000A7 RID: 167
		MWGP = 1297565520,
		// Token: 0x040000A8 RID: 168
		HttpWebProviderTransferCommandGetNextPacket = 1297565520,
		// Token: 0x040000A9 RID: 169
		MXSR = 1297634130,
		// Token: 0x040000AA RID: 170
		XmlWebProviderSendRequest = 1297634130,
		// Token: 0x040000AB RID: 171
		MXRR = 1297633874,
		// Token: 0x040000AC RID: 172
		XmlWebProviderReceiveResponse = 1297633874,
		// Token: 0x040000AD RID: 173
		MXPR = 1297633362,
		// Token: 0x040000AE RID: 174
		XmlWebProviderProcessRequest = 1297633362,
		// Token: 0x040000AF RID: 175
		MXGP = 1297631056,
		// Token: 0x040000B0 RID: 176
		XmlWebProviderTransferCommandGetNextPacket = 1297631056,
		// Token: 0x040000B1 RID: 177
		MDSR = 1296323410,
		// Token: 0x040000B2 RID: 178
		GatewayGenericDataflowSendRequest = 1296323410,
		// Token: 0x040000B3 RID: 179
		MDGR = 1296320338,
		// Token: 0x040000B4 RID: 180
		GatewayGenericDataflowGetNextResponse = 1296320338,
		// Token: 0x040000B5 RID: 181
		MCSR = 1296257874,
		// Token: 0x040000B6 RID: 182
		GatewayClientPipelineSerializeRequest = 1296257874,
		// Token: 0x040000B7 RID: 183
		MCRR = 1296257618,
		// Token: 0x040000B8 RID: 184
		GatewayClientPipelineRemoter = 1296257618,
		// Token: 0x040000B9 RID: 185
		MCGC = 1296254787,
		// Token: 0x040000BA RID: 186
		GatewayClientPipelineGetCommunicationChannel = 1296254787,
		// Token: 0x040000BB RID: 187
		MCCC = 1296253763,
		// Token: 0x040000BC RID: 188
		GatewayClientPipelineCreateCommunicationChannelActivity = 1296253763,
		// Token: 0x040000BD RID: 189
		MCCT = 1296253780,
		// Token: 0x040000BE RID: 190
		GatewayClientPipelineCreateTokenProviderActivity = 1296253780,
		// Token: 0x040000BF RID: 191
		MCCS = 1296253779,
		// Token: 0x040000C0 RID: 192
		GatewayClientPipelineCreateServiceChannelActivity = 1296253779,
		// Token: 0x040000C1 RID: 193
		MCOD = 1296256836,
		// Token: 0x040000C2 RID: 194
		GatewayClientPipelineResponseCoordinator = 1296256836,
		// Token: 0x040000C3 RID: 195
		MCIC = 1296255299,
		// Token: 0x040000C4 RID: 196
		GatewayClientPipelineInitializeCommunicationChannelActivity = 1296255299,
		// Token: 0x040000C5 RID: 197
		MCSM = 1296257869,
		// Token: 0x040000C6 RID: 198
		GatewayClientPipelineSendRequestMessage = 1296257869,
		// Token: 0x040000C7 RID: 199
		MCRP = 1296257616,
		// Token: 0x040000C8 RID: 200
		GatewayClientPipelineReceiveResponsePackets = 1296257616,
		// Token: 0x040000C9 RID: 201
		MCTP = 1296258128,
		// Token: 0x040000CA RID: 202
		GatewayClientPipelineReceiveTelemetryPackets = 1296258128,
		// Token: 0x040000CB RID: 203
		MCPS = 1296257107,
		// Token: 0x040000CC RID: 204
		GatewayClientPipelineProcessSpecificTransferCallback = 1296257107,
		// Token: 0x040000CD RID: 205
		MCRC = 1296257603,
		// Token: 0x040000CE RID: 206
		GatewayClientPipelineResponseDecompressor = 1296257603,
		// Token: 0x040000CF RID: 207
		MCRD,
		// Token: 0x040000D0 RID: 208
		GatewayClientPipelineResponseDeserializer = 1296257604,
		// Token: 0x040000D1 RID: 209
		MCRS = 1296257619,
		// Token: 0x040000D2 RID: 210
		GatewayClientPipelineResultProcessor = 1296257619,
		// Token: 0x040000D3 RID: 211
		MCRK = 1296257611,
		// Token: 0x040000D4 RID: 212
		GatewayClientPipelineResultProcessorOperationKeepAlive = 1296257611,
		// Token: 0x040000D5 RID: 213
		MCRW = 1296257623,
		// Token: 0x040000D6 RID: 214
		GatewayClientPipelineResultProcessorWaitForOperationCompleted = 1296257623,
		// Token: 0x040000D7 RID: 215
		MCRT = 1296257620,
		// Token: 0x040000D8 RID: 216
		GatewayClientPipelineResultProcessorStreamResult = 1296257620,
		// Token: 0x040000D9 RID: 217
		MGSP = 1296520016,
		// Token: 0x040000DA RID: 218
		GatewayTransferServicePing = 1296520016,
		// Token: 0x040000DB RID: 219
		MGST = 1296520020,
		// Token: 0x040000DC RID: 220
		GatewayTransferServiceTransfer = 1296520020,
		// Token: 0x040000DD RID: 221
		MGRF = 1296519750,
		// Token: 0x040000DE RID: 222
		GatewayTransferServiceTransferWaitForResponseAndSendPacket = 1296519750,
		// Token: 0x040000DF RID: 223
		MGSR = 1296520018,
		// Token: 0x040000E0 RID: 224
		GatewayTransferServiceTransferSendResponsePacket = 1296520018,
		// Token: 0x040000E1 RID: 225
		MGTT = 1296520276,
		// Token: 0x040000E2 RID: 226
		GatewayTransferServiceTransferTelemetry = 1296520276,
		// Token: 0x040000E3 RID: 227
		MGPP = 1296519248,
		// Token: 0x040000E4 RID: 228
		GatewayTransferServicePipelineDeserializeAndProcess = 1296519248,
		// Token: 0x040000E5 RID: 229
		MGOC = 1296518979,
		// Token: 0x040000E6 RID: 230
		GatewayTransferServicePipelineEnsureOpenConnection = 1296518979,
		// Token: 0x040000E7 RID: 231
		MGDX = 1296516184,
		// Token: 0x040000E8 RID: 232
		GatewayTransferServicePipelineDisposeConnection = 1296516184,
		// Token: 0x040000E9 RID: 233
		MGEQ = 1296516433,
		// Token: 0x040000EA RID: 234
		GatewayTransferServicePipelineExecuteAdoQuery = 1296516433,
		// Token: 0x040000EB RID: 235
		MGAC = 1296515395,
		// Token: 0x040000EC RID: 236
		GatewayTransferServicePipelineCancelAdoQuery = 1296515395,
		// Token: 0x040000ED RID: 237
		MGQC = 1296519491,
		// Token: 0x040000EE RID: 238
		GatewayTransferServicePipelineGetAdoConnectionStatus = 1296519491,
		// Token: 0x040000EF RID: 239
		MGQS = 1296519507,
		// Token: 0x040000F0 RID: 240
		GatewayTransferServicePipelineGetAdoQueryStatus = 1296519507,
		// Token: 0x040000F1 RID: 241
		MGEE = 1296516421,
		// Token: 0x040000F2 RID: 242
		GatewayTransferServicePipelineExecuteExternalAdoQuery = 1296516421,
		// Token: 0x040000F3 RID: 243
		MGEA = 1296516417,
		// Token: 0x040000F4 RID: 244
		GatewayTransferServicePipelineExecuteExternalAdoQueryAsync = 1296516417,
		// Token: 0x040000F5 RID: 245
		MGEO = 1296516431,
		// Token: 0x040000F6 RID: 246
		GatewayTransferServicePipelineExecuteOleDbQuery = 1296516431,
		// Token: 0x040000F7 RID: 247
		MGCQ = 1296515921,
		// Token: 0x040000F8 RID: 248
		GatewayTransferServicePipelineCancelOleDbQuery = 1296515921,
		// Token: 0x040000F9 RID: 249
		MGOP = 1296518992,
		// Token: 0x040000FA RID: 250
		GatewayTransferServicePipelineCreateOleDbProvider = 1296518992,
		// Token: 0x040000FB RID: 251
		MGID = 1296517444,
		// Token: 0x040000FC RID: 252
		GatewayTransferServicePipelineInitOleDbDataSource = 1296517444,
		// Token: 0x040000FD RID: 253
		MGSC = 1296520003,
		// Token: 0x040000FE RID: 254
		GatewayTransferServicePipelineGetOleDbSchemaRowset = 1296520003,
		// Token: 0x040000FF RID: 255
		MGON = 1296518990,
		// Token: 0x04000100 RID: 256
		GatewayTransferServicePipelineSetupOleDbConnection = 1296518990,
		// Token: 0x04000101 RID: 257
		MGOM = 1296518989,
		// Token: 0x04000102 RID: 258
		GatewayTransferServicePipelineSetupOleDbCommand = 1296518989,
		// Token: 0x04000103 RID: 259
		MGEF = 1296516422,
		// Token: 0x04000104 RID: 260
		GatewayTransferServicePipelineExecuteExternalOleDbQuery = 1296516422,
		// Token: 0x04000105 RID: 261
		MGTD = 1296520260,
		// Token: 0x04000106 RID: 262
		GatewayTransferServicePipelineTestDataSourceConnection = 1296520260,
		// Token: 0x04000107 RID: 263
		MGEC = 1296516419,
		// Token: 0x04000108 RID: 264
		GatewayTransferServicePipelineEncryptCredentials = 1296516419,
		// Token: 0x04000109 RID: 265
		MGGC = 1296516931,
		// Token: 0x0400010A RID: 266
		GatewayTransferServicePipelineGetDatabaseConnection = 1296516931,
		// Token: 0x0400010B RID: 267
		MGCC = 1296515907,
		// Token: 0x0400010C RID: 268
		GatewayTransferServicePipelineCreateDatabaseConnection = 1296515907,
		// Token: 0x0400010D RID: 269
		MGAD = 1296515396,
		// Token: 0x0400010E RID: 270
		GatewayTransferServicePipelineAddToAdoActiveRequestPool = 1296515396,
		// Token: 0x0400010F RID: 271
		MGGE = 1296516933,
		// Token: 0x04000110 RID: 272
		GatewayTransferServicePipelineGetReaderFromAdoActiveRequestPool = 1296516933,
		// Token: 0x04000111 RID: 273
		MGGI = 1296516937,
		// Token: 0x04000112 RID: 274
		GatewayTransferServicePipelineIncrementReaderResultInAdoActiveRequestPool = 1296516937,
		// Token: 0x04000113 RID: 275
		MGWO = 1296521039,
		// Token: 0x04000114 RID: 276
		GatewayTransferServicePipelineWatchRequestInActiveRequestPool = 1296521039,
		// Token: 0x04000115 RID: 277
		MGGD = 1296516932,
		// Token: 0x04000116 RID: 278
		GatewayTransferServicePipelineCancelRequestInAdoActiveRequestPool = 1296516932,
		// Token: 0x04000117 RID: 279
		MGSF = 1296520006,
		// Token: 0x04000118 RID: 280
		GatewayTransferServicePipelineGetStatusFromAdoActiveRequestPool = 1296520006,
		// Token: 0x04000119 RID: 281
		MGED = 1296516420,
		// Token: 0x0400011A RID: 282
		GatewayTransferServicePipelineSetCancelOnDataReaderInAdoActiveRequestPool = 1296516420,
		// Token: 0x0400011B RID: 283
		MGRD = 1296519748,
		// Token: 0x0400011C RID: 284
		GatewayTransferServicePipelineRemoveFromAdoActiveRequestPool = 1296519748,
		// Token: 0x0400011D RID: 285
		MGAN = 1296515406,
		// Token: 0x0400011E RID: 286
		GatewayTransferServicePipelineAddToMonitoredDbConnections = 1296515406,
		// Token: 0x0400011F RID: 287
		MGGN = 1296516942,
		// Token: 0x04000120 RID: 288
		GatewayTransferServicePipelineGetFromMonitoredDbConnections = 1296516942,
		// Token: 0x04000121 RID: 289
		MGRN = 1296519758,
		// Token: 0x04000122 RID: 290
		GatewayTransferServicePipelineRemoveFromMonitoredDbConnections = 1296519758,
		// Token: 0x04000123 RID: 291
		MGFN = 1296516686,
		// Token: 0x04000124 RID: 292
		GatewayTransferServicePipelineSafeRemoveFromMonitoredDbConnections = 1296516686,
		// Token: 0x04000125 RID: 293
		MGQN = 1296519502,
		// Token: 0x04000126 RID: 294
		GatewayTransferServicePipelineAddQueryToMonitoredDbConnections = 1296519502,
		// Token: 0x04000127 RID: 295
		MGEN = 1296516430,
		// Token: 0x04000128 RID: 296
		GatewayTransferServicePipelineRemoveQueryFromMonitoredDbConnections = 1296516430,
		// Token: 0x04000129 RID: 297
		MGSN = 1296520014,
		// Token: 0x0400012A RID: 298
		GatewayTransferServicePipelineGetStatusFromMonitoredDbConnections = 1296520014,
		// Token: 0x0400012B RID: 299
		MGGO = 1296516943,
		// Token: 0x0400012C RID: 300
		GatewayTransferServicePipelineGetOleDbDatabaseConnection = 1296516943,
		// Token: 0x0400012D RID: 301
		MGCO = 1296515919,
		// Token: 0x0400012E RID: 302
		GatewayTransferServicePipelineCreateOleDbDatabaseConnection = 1296515919,
		// Token: 0x0400012F RID: 303
		MGOA = 1296518977,
		// Token: 0x04000130 RID: 304
		GatewayTransferServicePipelineAddToOleDbActiveRequestPool = 1296518977,
		// Token: 0x04000131 RID: 305
		MGOG = 1296518983,
		// Token: 0x04000132 RID: 306
		GatewayTransferServicePipelineGetFromOleDbActiveRequestPool = 1296518983,
		// Token: 0x04000133 RID: 307
		MGOR = 1296518994,
		// Token: 0x04000134 RID: 308
		GatewayTransferServicePipelineRemoveFromOleDbActiveRequestPool = 1296518994,
		// Token: 0x04000135 RID: 309
		MGPS = 1296519251,
		// Token: 0x04000136 RID: 310
		GatewayTransferServicePipelineSerializeResponsePacket = 1296519251,
		// Token: 0x04000137 RID: 311
		MGPC = 1296519235,
		// Token: 0x04000138 RID: 312
		GatewayTransferServicePipelineCompressResponsePacket = 1296519235,
		// Token: 0x04000139 RID: 313
		MGGS = 1296516947,
		// Token: 0x0400013A RID: 314
		GatewayTransferServicePipelineExecuteGetSchemaDataSet = 1296516947,
		// Token: 0x0400013B RID: 315
		MGGM = 1296516941,
		// Token: 0x0400013C RID: 316
		GatewayTransferServicePipelineGetMashupConnection = 1296516941,
		// Token: 0x0400013D RID: 317
		MGEM = 1296516429,
		// Token: 0x0400013E RID: 318
		GatewayTransferServicePipelineExecuteMashupDbQuery = 1296516429,
		// Token: 0x0400013F RID: 319
		MGXM = 1296521293,
		// Token: 0x04000140 RID: 320
		GatewayTransferServicePipelineExecuteExternalMashupDbQueryAsync = 1296521293,
		// Token: 0x04000141 RID: 321
		MGAP = 1296515408,
		// Token: 0x04000142 RID: 322
		GatewayTransferServicePipelinePingAsyncOperation = 1296515408,
		// Token: 0x04000143 RID: 323
		MGAA = 1296515393,
		// Token: 0x04000144 RID: 324
		GatewayTransferServicePipelineAcknowledgeAsyncOperationPacket = 1296515393,
		// Token: 0x04000145 RID: 325
		MGAS = 1296515411,
		// Token: 0x04000146 RID: 326
		GatewayTransferServicePipelineStreamAsyncOperationResult = 1296515411,
		// Token: 0x04000147 RID: 327
		MGPE = 1296519237,
		// Token: 0x04000148 RID: 328
		GatewaySpoolerExecuteOperation = 1296519237,
		// Token: 0x04000149 RID: 329
		MGPW = 1296519255,
		// Token: 0x0400014A RID: 330
		GatewaySpoolerWaitForResponseAndSpoolPacket = 1296519255,
		// Token: 0x0400014B RID: 331
		MGSO = 1296520015,
		// Token: 0x0400014C RID: 332
		GatewaySpoolerSpoolingOperation = 1296520015,
		// Token: 0x0400014D RID: 333
		MGPL = 1296519244,
		// Token: 0x0400014E RID: 334
		GatewaySpoolerSpoolOperationPacket = 1296519244,
		// Token: 0x0400014F RID: 335
		MGDD = 1296516164,
		// Token: 0x04000150 RID: 336
		GatewayTransferServicePipelineDisconnect = 1296516164,
		// Token: 0x04000151 RID: 337
		MFGC = 1296451395,
		// Token: 0x04000152 RID: 338
		GatewayConfigurationServiceGetConfiguration = 1296451395,
		// Token: 0x04000153 RID: 339
		MFUC = 1296454979,
		// Token: 0x04000154 RID: 340
		GatewayConfigurationServiceUpdateConfiguration = 1296454979,
		// Token: 0x04000155 RID: 341
		MFMC = 1296452931,
		// Token: 0x04000156 RID: 342
		GatewayConfigurationServiceUpdateMashupConfigurationPropertyConfiguration = 1296452931,
		// Token: 0x04000157 RID: 343
		MFTC = 1296454723,
		// Token: 0x04000158 RID: 344
		GatewayConfigurationServiceUpdateTelemetryConfigurationPropertyConfiguration = 1296454723,
		// Token: 0x04000159 RID: 345
		MFAK = 1296449867,
		// Token: 0x0400015A RID: 346
		GatewayConfigurationServiceGenerateAsymmetricKey = 1296449867,
		// Token: 0x0400015B RID: 347
		MFTS = 1296454739,
		// Token: 0x0400015C RID: 348
		GatewayConfigurationServiceGetTransferServiceState = 1296454739,
		// Token: 0x0400015D RID: 349
		MFGA = 1296451393,
		// Token: 0x0400015E RID: 350
		GatewayConfigurationServiceGetAppDataPath = 1296451393,
		// Token: 0x0400015F RID: 351
		MFGD = 1296451396,
		// Token: 0x04000160 RID: 352
		GatewayConfigurationServiceGetMashupCustomConnectorsDirectoryPath = 1296451396,
		// Token: 0x04000161 RID: 353
		MFGM = 1296451405,
		// Token: 0x04000162 RID: 354
		GatewayConfigurationServiceGetMashupConnectorsMetadata = 1296451405,
		// Token: 0x04000163 RID: 355
		MFHC = 1296451651,
		// Token: 0x04000164 RID: 356
		GatewayConfigurationServiceHealthCheck = 1296451651,
		// Token: 0x04000165 RID: 357
		MFSC = 1296454467,
		// Token: 0x04000166 RID: 358
		GatewayConfigurationServiceGetGatewayStaticCapabilities = 1296454467,
		// Token: 0x04000167 RID: 359
		MFSD,
		// Token: 0x04000168 RID: 360
		GatewayConfigurationServiceSetMashupCustomConnectorsDirectoryPath = 1296454468,
		// Token: 0x04000169 RID: 361
		MFTR = 1296454738,
		// Token: 0x0400016A RID: 362
		GatewayConfigurationServiceGetServiceBusPortsGetTestResults = 1296454738,
		// Token: 0x0400016B RID: 363
		MFST = 1296454484,
		// Token: 0x0400016C RID: 364
		GatewayConfigurationServiceGetServiceBusPortsTestStatus = 1296454484,
		// Token: 0x0400016D RID: 365
		MFSE = 1296454469,
		// Token: 0x0400016E RID: 366
		GatewayConfigurationServiceStartServiceBusPortsTest = 1296454469,
		// Token: 0x0400016F RID: 367
		MFSB = 1296454466,
		// Token: 0x04000170 RID: 368
		GatewayConfigurationServiceGetServiceBusPortsTestSBInfo = 1296454466,
		// Token: 0x04000171 RID: 369
		MFDL = 1296450636,
		// Token: 0x04000172 RID: 370
		GatewayConfigurationDeleteLegacyKey = 1296450636,
		// Token: 0x04000173 RID: 371
		MFVW = 1296455255,
		// Token: 0x04000174 RID: 372
		GatewayConfigurationValidateWitness = 1296455255,
		// Token: 0x04000175 RID: 373
		MFGW = 1296451415,
		// Token: 0x04000176 RID: 374
		GatewayConfigurationGenerateWitness = 1296451415,
		// Token: 0x04000177 RID: 375
		MFGS = 1296451411,
		// Token: 0x04000178 RID: 376
		GatewayConfigurationGenerateSalt = 1296451411,
		// Token: 0x04000179 RID: 377
		MFGV = 1296451414,
		// Token: 0x0400017A RID: 378
		GatewayConfigurationGetGatewayVersion = 1296451414,
		// Token: 0x0400017B RID: 379
		RDSP = 1380209488,
		// Token: 0x0400017C RID: 380
		PowerBIPipelineRawDataServerPipeline = 1380209488,
		// Token: 0x0400017D RID: 381
		RDSC = 1380209475,
		// Token: 0x0400017E RID: 382
		PowerBIPipelineRawDataServerPipelineCreate = 1380209475,
		// Token: 0x0400017F RID: 383
		RDFS = 1380206163,
		// Token: 0x04000180 RID: 384
		PowerBIPipelineRawDataServerFillStream = 1380206163,
		// Token: 0x04000181 RID: 385
		RDCP = 1380205392,
		// Token: 0x04000182 RID: 386
		PowerBIPipelineRawDataClientPipeline = 1380205392,
		// Token: 0x04000183 RID: 387
		RDRS = 1380209235,
		// Token: 0x04000184 RID: 388
		PowerBIPipelineRawDataClientReadStream = 1380209235,
		// Token: 0x04000185 RID: 389
		RDPC = 1380208707,
		// Token: 0x04000186 RID: 390
		PowerBIPipelineRawDataTransferServicePipelineCompressResponsePacket = 1380208707,
		// Token: 0x04000187 RID: 391
		RDRC = 1380209219,
		// Token: 0x04000188 RID: 392
		PowerBIPipelineRawDataClientPipelineResponseDecompressor = 1380209219,
		// Token: 0x04000189 RID: 393
		RDPS = 1380208723,
		// Token: 0x0400018A RID: 394
		PowerBIPipelineRawDataTransferServicePipelineSerializeResponsePacket = 1380208723,
		// Token: 0x0400018B RID: 395
		LBEX = 1279411544,
		// Token: 0x0400018C RID: 396
		LoadBalancerExecute = 1279411544,
		// Token: 0x0400018D RID: 397
		MFRG = 1296454215,
		// Token: 0x0400018E RID: 398
		GatewayReportGeneration = 1296454215
	}
}
