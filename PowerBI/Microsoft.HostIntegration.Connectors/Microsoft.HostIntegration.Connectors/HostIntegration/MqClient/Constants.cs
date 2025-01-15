using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BC4 RID: 3012
	public class Constants
	{
		// Token: 0x04004EB4 RID: 20148
		public const int OpenOptionOutput = 16;

		// Token: 0x04004EB5 RID: 20149
		public const int OptionSyncpoint = 2;

		// Token: 0x04004EB6 RID: 20150
		public const int OptionNoSyncpoint = 4;

		// Token: 0x04004EB7 RID: 20151
		public const int OptionAsynchronous = 65536;

		// Token: 0x04004EB8 RID: 20152
		public const int OptionSynchronous = 131072;

		// Token: 0x04004EB9 RID: 20153
		public const int OptionWait = 1;

		// Token: 0x04004EBA RID: 20154
		public const int OptionAcceptTruncatedMessage = 64;

		// Token: 0x04004EBB RID: 20155
		public const int OptionForcePropertiesIntoRf2h = 33554432;

		// Token: 0x04004EBC RID: 20156
		public const int MaximumLengthChannelName = 20;

		// Token: 0x04004EBD RID: 20157
		public const int MaximumLengthQueueManagerName = 48;

		// Token: 0x04004EBE RID: 20158
		public const int MaximumLengthQueueName = 48;

		// Token: 0x04004EBF RID: 20159
		public const int ProductIdLength = 12;

		// Token: 0x04004EC0 RID: 20160
		public const int MaximumLengthUserId = 12;

		// Token: 0x04004EC1 RID: 20161
		public const int MaximumLengthPassword = 12;

		// Token: 0x04004EC2 RID: 20162
		public const int MaximumLengthLongUserId = 64;

		// Token: 0x04004EC3 RID: 20163
		public const int MaximumLengthApplicationName = 28;

		// Token: 0x04004EC4 RID: 20164
		public const int RequiredLengthAccountToken = 32;

		// Token: 0x04004EC5 RID: 20165
		public const int MaximumLengthMqmId = 48;

		// Token: 0x04004EC6 RID: 20166
		public const int MaximumLengthObjectName = 48;

		// Token: 0x04004EC7 RID: 20167
		public const int MaximumLengthObjectQueueManager = 48;

		// Token: 0x04004EC8 RID: 20168
		public const int MaximumLengthDynamicQueueName = 48;

		// Token: 0x04004EC9 RID: 20169
		public const int MaximumLengthDynamicQueueNameWithAsterisk = 33;

		// Token: 0x04004ECA RID: 20170
		public const int MaximumLengthMessageFormat = 8;

		// Token: 0x04004ECB RID: 20171
		public const int RequiredLengthMessageId = 24;

		// Token: 0x04004ECC RID: 20172
		public const int MaximumLengthMessageIdFConnect = 152;

		// Token: 0x04004ECD RID: 20173
		public const int RequiredLengthMessageCorrelator = 24;

		// Token: 0x04004ECE RID: 20174
		public const int MaximumLengthMessageReplyToQueue = 48;

		// Token: 0x04004ECF RID: 20175
		public const int MaximumLengthMessageReplyToQueueManager = 48;

		// Token: 0x04004ED0 RID: 20176
		public const int MaximumLengthMessageApplicationIdData = 32;

		// Token: 0x04004ED1 RID: 20177
		public const int MaximumLengthMessageApplicationOriginData = 4;

		// Token: 0x04004ED2 RID: 20178
		public const int RequiredLengthMessagePutDate = 8;

		// Token: 0x04004ED3 RID: 20179
		public const int RequiredLengthMessagePutTime = 8;

		// Token: 0x04004ED4 RID: 20180
		public const int RequiredLengthMessageGroupId = 24;

		// Token: 0x04004ED5 RID: 20181
		public const int RequiredLengthMessageTokenAsynchMessage = 16;

		// Token: 0x04004ED6 RID: 20182
		public const int MaximumLengthMessageTokenGet = 16;

		// Token: 0x04004ED7 RID: 20183
		public const int MaximumLengthMessageTokenGetReply = 16;

		// Token: 0x04004ED8 RID: 20184
		public const int MaximumSequenceNumber = 999999999;

		// Token: 0x04004ED9 RID: 20185
		public const int MaximumOffset = 999999999;

		// Token: 0x04004EDA RID: 20186
		public const int MessageDescriptorAscii = 538985549;

		// Token: 0x04004EDB RID: 20187
		public const int PutMessageOptionsAscii = 542068048;

		// Token: 0x04004EDC RID: 20188
		public const int GetMessageOptionsAscii = 542068039;

		// Token: 0x04004EDD RID: 20189
		public const string DynamicQueueNamePrefix = "AMQ.*";

		// Token: 0x04004EDE RID: 20190
		public const int Ccsid = 1252;

		// Token: 0x04004EDF RID: 20191
		public const int Encoding = 546;

		// Token: 0x04004EE0 RID: 20192
		public const char DoubleQuotationMarksCharacter = '"';

		// Token: 0x04004EE1 RID: 20193
		public const char NullCharacter = '\0';

		// Token: 0x04004EE2 RID: 20194
		public const char BlankCharacter = ' ';

		// Token: 0x04004EE3 RID: 20195
		public const string DoubleQuotationMarks = "\"";

		// Token: 0x04004EE4 RID: 20196
		public const string TwoDoubleQuotationMarks = "\"\"";

		// Token: 0x04004EE5 RID: 20197
		public const int PcfStringDefaultCcsid = 0;

		// Token: 0x04004EE6 RID: 20198
		public const int LengthOfFormatField = 8;

		// Token: 0x04004EE7 RID: 20199
		public const int MimimumHeaderLengthForRecognition = 8;

		// Token: 0x04004EE8 RID: 20200
		public const int StructureIdNotAvailable = -1;

		// Token: 0x04004EE9 RID: 20201
		public const int LengthOfMdeHeader = 72;

		// Token: 0x04004EEA RID: 20202
		public const string MdeHeaderFormat = "MQHMDE";

		// Token: 0x04004EEB RID: 20203
		public const string MdeHeaderName = "Message Descriptor Extension";

		// Token: 0x04004EEC RID: 20204
		public const int MdeHeaderStructureId = 541410381;

		// Token: 0x04004EED RID: 20205
		public const int MdeHeaderStructureIdEbcdic = 1086702804;

		// Token: 0x04004EEE RID: 20206
		public const int LengthOfV1Mqmd = 324;

		// Token: 0x04004EEF RID: 20207
		public const int LengthOfV2Mqmd = 40;

		// Token: 0x04004EF0 RID: 20208
		public const int LengthOfCicsBridgeHeader = 180;

		// Token: 0x04004EF1 RID: 20209
		public const int MinimumLengthOfCicsBridgeHeader = 164;

		// Token: 0x04004EF2 RID: 20210
		public const string CicsBridgeHeaderFormat = "MQCICS";

		// Token: 0x04004EF3 RID: 20211
		public const string CicsBridgeHeaderName = "CICS Bridge Header";

		// Token: 0x04004EF4 RID: 20212
		public const int CicsBridgeHeaderStructureId = 541608259;

		// Token: 0x04004EF5 RID: 20213
		public const int CicsBridgeHeaderStructureIdEbcdic = 1086900675;

		// Token: 0x04004EF6 RID: 20214
		public const int CicsBridgeHeaderVersion = 2;

		// Token: 0x04004EF7 RID: 20215
		public const int CicsBridgeHeaderMimimumVersion = 1;

		// Token: 0x04004EF8 RID: 20216
		public const int RequiredLengthCicsBridgeToken = 8;

		// Token: 0x04004EF9 RID: 20217
		public const int MaximumLengthCicsBridgeAuthenticator = 8;

		// Token: 0x04004EFA RID: 20218
		public const int MaximumLengthCicsBridgeReplyToFormat = 8;

		// Token: 0x04004EFB RID: 20219
		public const int MaximumLengthCicsBridgeRemoteSysId = 4;

		// Token: 0x04004EFC RID: 20220
		public const int MaximumLengthCicsBridgeTransactionid = 4;

		// Token: 0x04004EFD RID: 20221
		public const int MaximumLengthCicsBridgeFacilityIsLike = 4;

		// Token: 0x04004EFE RID: 20222
		public const int MaximumLengthCicsBridgeCancelCode = 4;

		// Token: 0x04004EFF RID: 20223
		public const int MaximumLengthCicsBridgeFunction = 4;

		// Token: 0x04004F00 RID: 20224
		public const int MaximumLengthCicsBridgeAbendCode = 4;

		// Token: 0x04004F01 RID: 20225
		public const int MaximumLengthCicsBridgeReserved1 = 8;

		// Token: 0x04004F02 RID: 20226
		public const int MaximumLengthCicsBridgeReserved2 = 8;

		// Token: 0x04004F03 RID: 20227
		public const int MaximumLengthCicsBridgeReserved3 = 8;

		// Token: 0x04004F04 RID: 20228
		public const int MaximumLengthCicsBridgeAttentionId = 4;

		// Token: 0x04004F05 RID: 20229
		public const int LengthCicsBridgeAttentionIdBlanks = 3;

		// Token: 0x04004F06 RID: 20230
		public const int MaximumLengthCicsBridgeStartCode = 4;

		// Token: 0x04004F07 RID: 20231
		public const int MaximumLengthCicsBridgeProgramName = 8;

		// Token: 0x04004F08 RID: 20232
		public const int LengthOfImsBridgeHeader = 84;

		// Token: 0x04004F09 RID: 20233
		public const string ImsBridgeHeaderFormat = "MQIMS";

		// Token: 0x04004F0A RID: 20234
		public const string ImsBridgeHeaderName = "IMS Bridge Header";

		// Token: 0x04004F0B RID: 20235
		public const int ImsBridgeHeaderStructureId = 541608265;

		// Token: 0x04004F0C RID: 20236
		public const int ImsBridgeHeaderStructureIdEbcdic = 1086900681;

		// Token: 0x04004F0D RID: 20237
		public const int ImsBridgeHeaderVersion = 1;

		// Token: 0x04004F0E RID: 20238
		public const int RequiredLengthImsBridgeTransactionInstanceId = 16;

		// Token: 0x04004F0F RID: 20239
		public const int MaximumLengthImsBridgeLTermOverride = 8;

		// Token: 0x04004F10 RID: 20240
		public const int MaximumLengthImsBridgeMfsMapName = 8;

		// Token: 0x04004F11 RID: 20241
		public const int MaximumLengthImsBridgeReplyToFormat = 8;

		// Token: 0x04004F12 RID: 20242
		public const int MaximumLengthImsBridgeAuthenticator = 8;

		// Token: 0x04004F13 RID: 20243
		public const int LengthOfDlqHeader = 172;

		// Token: 0x04004F14 RID: 20244
		public const string DlqHeaderFormat = "MQDEAD";

		// Token: 0x04004F15 RID: 20245
		public const string DlqHeaderName = "Dead Letter Header";

		// Token: 0x04004F16 RID: 20246
		public const int DlqHeaderStructureId = 541609028;

		// Token: 0x04004F17 RID: 20247
		public const int DlqHeaderStructureIdEbcdic = 1086903236;

		// Token: 0x04004F18 RID: 20248
		public const int DlqHeaderVersion = 1;

		// Token: 0x04004F19 RID: 20249
		public const int MaximumLengthDlqOriginalQueue = 48;

		// Token: 0x04004F1A RID: 20250
		public const int MaximumLengthDlqOriginalQueueManager = 48;

		// Token: 0x04004F1B RID: 20251
		public const int MaximumLengthDlqPutApplicationName = 28;

		// Token: 0x04004F1C RID: 20252
		public const int LengthOfWiHeader = 120;

		// Token: 0x04004F1D RID: 20253
		public const string WiHeaderFormat = "MQHWIH";

		// Token: 0x04004F1E RID: 20254
		public const string WiHeaderName = "Work Information Header";

		// Token: 0x04004F1F RID: 20255
		public const int WiHeaderStructureId = 541608279;

		// Token: 0x04004F20 RID: 20256
		public const int WiHeaderStructureIdEbcdic = 1086900710;

		// Token: 0x04004F21 RID: 20257
		public const int WiHeaderVersion = 1;

		// Token: 0x04004F22 RID: 20258
		public const int RequiredLengthWiMessageToken = 16;

		// Token: 0x04004F23 RID: 20259
		public const int MaximumLengthWiServiceName = 32;

		// Token: 0x04004F24 RID: 20260
		public const int MaximumLengthWiServiceStep = 8;

		// Token: 0x04004F25 RID: 20261
		public const int MaximumLengthWiReserved = 32;

		// Token: 0x04004F26 RID: 20262
		public const int FixedLengthOfRfHeader = 32;

		// Token: 0x04004F27 RID: 20263
		public const string RfHeaderFormat = "MQHRF";

		// Token: 0x04004F28 RID: 20264
		public const string RfHeaderName = "Rules and Formatting Header";

		// Token: 0x04004F29 RID: 20265
		public const int RfHeaderStructureId = 541607506;

		// Token: 0x04004F2A RID: 20266
		public const int RfHeaderStructureIdEbcdic = 1086899929;

		// Token: 0x04004F2B RID: 20267
		public const int RfHeaderVersion = 1;

		// Token: 0x04004F2C RID: 20268
		public const int FixedLengthOfRf2Header = 36;

		// Token: 0x04004F2D RID: 20269
		public const string Rf2HeaderFormat = "MQHRF2";

		// Token: 0x04004F2E RID: 20270
		public const string Rf2HeaderName = "Rules and Formatting (Version 2) Header";

		// Token: 0x04004F2F RID: 20271
		public const int Rf2HeaderStructureId = 541607506;

		// Token: 0x04004F30 RID: 20272
		public const int Rf2HeaderStructureIdEbcdic = 1086899929;

		// Token: 0x04004F31 RID: 20273
		public const int Rf2HeaderVersion = 2;

		// Token: 0x04004F32 RID: 20274
		public const int FixedLengthOfEpcfHeader = 68;

		// Token: 0x04004F33 RID: 20275
		public const string EpcfHeaderFormat = "MQHEPCF";

		// Token: 0x04004F34 RID: 20276
		public const string EpcfHeaderName = "Embedded PCF Header";

		// Token: 0x04004F35 RID: 20277
		public const int EpcfHeaderStructureId = 541610053;

		// Token: 0x04004F36 RID: 20278
		public const int EpcfHeaderStructureIdEbcdic = 1086904261;

		// Token: 0x04004F37 RID: 20279
		public const int EpcfHeaderVersion = 1;

		// Token: 0x04004F38 RID: 20280
		public const int FixedLengthOfPcfHeader = 36;

		// Token: 0x04004F39 RID: 20281
		public const string PcfHeaderFormat = "MQADMIN";

		// Token: 0x04004F3A RID: 20282
		public const string PcfHeaderName = "PCF Header";

		// Token: 0x04004F3B RID: 20283
		public const int FixedLengthOfByteStringFilterParameter = 20;

		// Token: 0x04004F3C RID: 20284
		public const int FixedLengthOfByteStringParameter = 16;

		// Token: 0x04004F3D RID: 20285
		public const int FixedLengthOfIntegerFilterParameter = 20;

		// Token: 0x04004F3E RID: 20286
		public const int FixedLengthOfIntegerListParameter = 16;

		// Token: 0x04004F3F RID: 20287
		public const int FixedLengthOfIntegerParameter = 16;

		// Token: 0x04004F40 RID: 20288
		public const int FixedLengthOfStringFilterParameter = 24;

		// Token: 0x04004F41 RID: 20289
		public const int FixedLengthOfStringListParameter = 24;

		// Token: 0x04004F42 RID: 20290
		public const int FixedLengthOfStringParameter = 20;

		// Token: 0x04004F43 RID: 20291
		public const int IntegerEncodingMask = 15;

		// Token: 0x04004F44 RID: 20292
		public const int PackedDecimalEncodingMask = 240;

		// Token: 0x04004F45 RID: 20293
		public const int FloatingPointEncodingMask = 3840;
	}
}
