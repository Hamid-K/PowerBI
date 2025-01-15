using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x0200001A RID: 26
	internal class ProviderErrorStrings
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005D0A File Offset: 0x00003F0A
		public static ResourceManager ResourceManager
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.Resources;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005D11 File Offset: 0x00003F11
		public static string CannotReopenConnection
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CannotReopenConnection");
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005D1D File Offset: 0x00003F1D
		public static string ChangeDatabaseNotSupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ChangeDatabaseNotSupported");
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005D29 File Offset: 0x00003F29
		public static string TransactionsNotSupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("TransactionsNotSupported");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005D35 File Offset: 0x00003F35
		public static string CommandTextNotSet
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CommandTextNotSet");
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005D41 File Offset: 0x00003F41
		public static string DuplicateConnectionProperty(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("DuplicateConnectionProperty", new object[] { p0 });
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005D57 File Offset: 0x00003F57
		public static string ConnectionNotOpen(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ConnectionNotOpen", new object[] { p0 });
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005D6D File Offset: 0x00003F6D
		public static string ConnectionNotSet
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ConnectionNotSet");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005D79 File Offset: 0x00003F79
		public static string OperationRequiresClosedConnection
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("OperationRequiresClosedConnection");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005D85 File Offset: 0x00003F85
		public static string ConnectionStringNotSet
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ConnectionStringNotSet");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005D91 File Offset: 0x00003F91
		public static string DataReaderIsOpen
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataReaderIsOpen");
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005D9D File Offset: 0x00003F9D
		public static string ReasonMessageFormat(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ReasonMessageFormat", new object[] { p0, p1 });
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005DB7 File Offset: 0x00003FB7
		public static string UnknownResourceName(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnknownResourceName", new object[] { p0 });
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005DCD File Offset: 0x00003FCD
		public static string UnsupportedCommandType(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnsupportedCommandType", new object[] { p0 });
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005DE3 File Offset: 0x00003FE3
		public static string ParametersNotSupported(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ParametersNotSupported", new object[] { p0 });
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005DF9 File Offset: 0x00003FF9
		public static string CommandBehaviorNotSupported(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("CommandBehaviorNotSupported", new object[] { p0 });
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005E0F File Offset: 0x0000400F
		public static string DesignTimeVisibleNotSupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DesignTimeVisibleNotSupported");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005E1B File Offset: 0x0000401B
		public static string ErrorDuringEvaluation
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ErrorDuringEvaluation");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005E27 File Offset: 0x00004027
		public static string MashupIncompatibleVersion
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("MashupIncompatibleVersion");
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005E33 File Offset: 0x00004033
		public static string DataAdapterNotSupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataAdapterNotSupported");
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005E3F File Offset: 0x0000403F
		public static string CommandBuilderNotSupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CommandBuilderNotSupported");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005E4B File Offset: 0x0000404B
		public static string DataReaderClosed
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataReaderClosed");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005E57 File Offset: 0x00004057
		public static string CannotReadWhenNoData
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CannotReadWhenNoData");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005E63 File Offset: 0x00004063
		public static string OrdinalOutOfRange
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("OrdinalOutOfRange");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005E6F File Offset: 0x0000406F
		public static string ColumnNameCannotBeEmptyString
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ColumnNameCannotBeEmptyString");
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005E7B File Offset: 0x0000407B
		public static string ColumnNameNotValid
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ColumnNameNotValid");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005E87 File Offset: 0x00004087
		public static string CancelledCommand
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CancelledCommand");
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005E93 File Offset: 0x00004093
		public static string KeywordNotSupported(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("KeywordNotSupported", new object[] { p0 });
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005EA9 File Offset: 0x000040A9
		public static string MashupAndPackageCannotBeBothNullOrEmpty
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("MashupAndPackageCannotBeBothNullOrEmpty");
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005EB5 File Offset: 0x000040B5
		public static string ResourceNameAndLocationNeedToMatch(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ResourceNameAndLocationNeedToMatch", new object[] { p0, p1 });
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005ECF File Offset: 0x000040CF
		public static string CommandTimeoutNegative
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CommandTimeoutNegative");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005EDB File Offset: 0x000040DB
		public static string RestartEvaluation
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("RestartEvaluation");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005EE7 File Offset: 0x000040E7
		public static string ObjectNotDate
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ObjectNotDate");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005EF3 File Offset: 0x000040F3
		public static string ObjectNotTime
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ObjectNotTime");
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005EFF File Offset: 0x000040FF
		public static string MashupUnrecognizedFormat
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("MashupUnrecognizedFormat");
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005F0B File Offset: 0x0000410B
		public static string UnsupportedCredentialType(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnsupportedCredentialType", new object[] { p0 });
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005F21 File Offset: 0x00004121
		public static string UnsupportedDataSourceKind(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnsupportedDataSourceKind", new object[] { p0 });
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005F37 File Offset: 0x00004137
		public static string CredentialNotValid(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("CredentialNotValid", new object[] { p0 });
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005F4D File Offset: 0x0000414D
		public static string CreateDataSourceEnumeratorNotSupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CreateDataSourceEnumeratorNotSupported");
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005F59 File Offset: 0x00004159
		public static string UnsupportedDataSourceKindCredentialType(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnsupportedDataSourceKindCredentialType", new object[] { p0, p1 });
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005F73 File Offset: 0x00004173
		public static string CredentialInvalidString
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CredentialInvalidString");
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005F7F File Offset: 0x0000417F
		public static string ConnectionCleanOpen
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ConnectionCleanOpen");
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005F8B File Offset: 0x0000418B
		public static string ConnectionNotCleaned
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ConnectionNotCleaned");
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005F97 File Offset: 0x00004197
		public static string ExtensionFileNotFound(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ExtensionFileNotFound", new object[] { p0 });
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005FAD File Offset: 0x000041AD
		public static string InvalidSyntax(object p0, object p1, object p2)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("InvalidSyntax", new object[] { p0, p1, p2 });
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005FCB File Offset: 0x000041CB
		public static string BufferOffsetNegative
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("BufferOffsetNegative");
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005FD7 File Offset: 0x000041D7
		public static string BufferTooSmall(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("BufferTooSmall", new object[] { p0, p1 });
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005FF1 File Offset: 0x000041F1
		public static string DataOffsetNegative
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataOffsetNegative");
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005FFD File Offset: 0x000041FD
		public static string LengthNegative
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("LengthNegative");
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006009 File Offset: 0x00004209
		public static string EncryptConnectionNotSupported(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("EncryptConnectionNotSupported", new object[] { p0 });
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000601F File Offset: 0x0000421F
		public static string MinCacheSizeLimit(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("MinCacheSizeLimit", new object[] { p0, p1 });
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006039 File Offset: 0x00004239
		public static string CachePathNotValid
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CachePathNotValid");
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006045 File Offset: 0x00004245
		public static string NotExpectedJsonFormatInner
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("NotExpectedJsonFormatInner");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006051 File Offset: 0x00004251
		public static string NotExpectedJsonFormat
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("NotExpectedJsonFormat");
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000605D File Offset: 0x0000425D
		public static string NotExpectedSwitchFormat
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("NotExpectedSwitchFormat");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006069 File Offset: 0x00004269
		public static string NotExpectedConfigurationFormat
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("NotExpectedConfigurationFormat");
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006075 File Offset: 0x00004275
		public static string NotSupportedConfigurationValue(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("NotSupportedConfigurationValue", new object[] { p0 });
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000608B File Offset: 0x0000428B
		public static string DataSourceLocation_JsonFormatInner
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataSourceLocation_JsonFormatInner");
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006097 File Offset: 0x00004297
		public static string DataSourceLocation_JsonFormat
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataSourceLocation_JsonFormat");
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000060A3 File Offset: 0x000042A3
		public static string DataSourceLocation_Missing_Protocol
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataSourceLocation_Missing_Protocol");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000060AF File Offset: 0x000042AF
		public static string DataSourceLocation_Missing_Address
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataSourceLocation_Missing_Address");
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000060BB File Offset: 0x000042BB
		public static string DataSourceLocation_Address_Missing_Kind
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataSourceLocation_Address_Missing_Kind");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000060C7 File Offset: 0x000042C7
		public static string DataSourceLocation_Address_Missing_Path
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataSourceLocation_Address_Missing_Path");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000060D3 File Offset: 0x000042D3
		public static string UnrecognizedPrivacy
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("UnrecognizedPrivacy");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000060DF File Offset: 0x000042DF
		public static string ValuePropertyCannotBeNull
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ValuePropertyCannotBeNull");
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000060EB File Offset: 0x000042EB
		public static string CredentialMissingProperty(object p0, object p1, object p2)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("CredentialMissingProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006109 File Offset: 0x00004309
		public static string CredentialPropertyType(object p0, object p1, object p2, object p3, object p4)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("CredentialPropertyType", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006130 File Offset: 0x00004330
		public static string UnsupportedDataSourceKindCredential(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnsupportedDataSourceKindCredential", new object[] { p0, p1 });
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000614A File Offset: 0x0000434A
		public static string DataSourceReferenceNoDataSource
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("DataSourceReferenceNoDataSource");
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006156 File Offset: 0x00004356
		public static string DataSourceReferenceNoGeneratedCode(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("DataSourceReferenceNoGeneratedCode", new object[] { p0 });
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000616C File Offset: 0x0000436C
		public static string CredentialPropertyNullValue(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("CredentialPropertyNullValue", new object[] { p0, p1 });
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006186 File Offset: 0x00004386
		public static string InvalidPrivacyWithGroupName(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("InvalidPrivacyWithGroupName", new object[] { p0 });
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000619C File Offset: 0x0000439C
		public static string InvalidPrivacyWithIsTrusted
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("InvalidPrivacyWithIsTrusted");
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000061A8 File Offset: 0x000043A8
		public static string Evaluation_Challenge_Result_CredentialAccessDenied(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("Evaluation_Challenge_Result_CredentialAccessDenied", new object[] { p0, p1 });
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000061C2 File Offset: 0x000043C2
		public static string Evaluation_Challenge_Result_CredentialAccessForbidden(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("Evaluation_Challenge_Result_CredentialAccessForbidden", new object[] { p0, p1 });
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000061DC File Offset: 0x000043DC
		public static string Evaluation_Challenge_Result_CredentialsInvalid(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("Evaluation_Challenge_Result_CredentialsInvalid", new object[] { p0, p1 });
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000061F6 File Offset: 0x000043F6
		public static string Evaluation_Challenge_Result_EncryptedConnectionFailure(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("Evaluation_Challenge_Result_EncryptedConnectionFailure", new object[] { p0, p1 });
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006210 File Offset: 0x00004410
		public static string Evaluation_Challenge_Result_PermissionRequired(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("Evaluation_Challenge_Result_PermissionRequired", new object[] { p0, p1 });
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000622A File Offset: 0x0000442A
		public static string Evaluation_Challenge_Result_PermissionRequired_Redirect(object p0, object p1, object p2)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("Evaluation_Challenge_Result_PermissionRequired_Redirect", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006248 File Offset: 0x00004448
		public static string Evaluation_Challenge_Result_PrincipalNameMismatch(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("Evaluation_Challenge_Result_PrincipalNameMismatch", new object[] { p0 });
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000625E File Offset: 0x0000445E
		public static string Evaluation_Challenge_Result_Actions(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("Evaluation_Challenge_Result_Actions", new object[] { p0 });
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006274 File Offset: 0x00004474
		public static string NativeQuery_Challenge_Error(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("NativeQuery_Challenge_Error", new object[] { p0, p1 });
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000628E File Offset: 0x0000448E
		public static string Resource_Firewall_Flow_Error
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("Resource_Firewall_Flow_Error");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000629A File Offset: 0x0000449A
		public static string Resource_Firewall_Rule_Error
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("Resource_Firewall_Rule_Error");
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000062A6 File Offset: 0x000044A6
		public static string Resource_UnsupportedCommand(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("Resource_UnsupportedCommand", new object[] { p0 });
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000062BC File Offset: 0x000044BC
		public static string Expression_TooDeep
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("Expression_TooDeep");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000062C8 File Offset: 0x000044C8
		public static string ExpressionOnlySupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ExpressionOnlySupported");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000062D4 File Offset: 0x000044D4
		public static string MashupAndPackageCannotBeBothSpecified
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("MashupAndPackageCannotBeBothSpecified");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000062E0 File Offset: 0x000044E0
		public static string SectionOnlySupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("SectionOnlySupported");
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000062EC File Offset: 0x000044EC
		public static string MinTempSizeLimit(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("MinTempSizeLimit", new object[] { p0, p1 });
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006306 File Offset: 0x00004506
		public static string TempPathNotValid
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("TempPathNotValid");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006312 File Offset: 0x00004512
		public static string CommandTimeoutExpired
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CommandTimeoutExpired");
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000631E File Offset: 0x0000451E
		public static string ConnectionTimeoutNegative
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ConnectionTimeoutNegative");
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000632A File Offset: 0x0000452A
		public static string UnexpectedValueType(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnexpectedValueType", new object[] { p0 });
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006340 File Offset: 0x00004540
		public static string ParameterEnumeratedMultipleTimes(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ParameterEnumeratedMultipleTimes", new object[] { p0 });
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006356 File Offset: 0x00004556
		public static string ParameterError(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ParameterError", new object[] { p0 });
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000636C File Offset: 0x0000456C
		public static string UnsupportedCredentialPropertiesCombination(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnsupportedCredentialPropertiesCombination", new object[] { p0, p1 });
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006386 File Offset: 0x00004586
		public static string UnsupportedDataSourceKindPermissionKind(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnsupportedDataSourceKindPermissionKind", new object[] { p0, p1 });
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000063A0 File Offset: 0x000045A0
		public static string MissingPermission(object p0, object p1, object p2)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("MissingPermission", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000063BE File Offset: 0x000045BE
		public static string EffectiveUsernameNotSupported(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("EffectiveUsernameNotSupported", new object[] { p0 });
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000063D4 File Offset: 0x000045D4
		public static string ConnectionStringPropertyNotSupported(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ConnectionStringPropertyNotSupported", new object[] { p0, p1 });
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000063EE File Offset: 0x000045EE
		public static string OAuthRefreshFailed
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("OAuthRefreshFailed");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000063FA File Offset: 0x000045FA
		public static string AboutToExpire
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("AboutToExpire");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00006406 File Offset: 0x00004606
		public static string WildcardPathsWithPermissionsNotSupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("WildcardPathsWithPermissionsNotSupported");
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006412 File Offset: 0x00004612
		public static string DirectoryMustBeEmpty(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("DirectoryMustBeEmpty", new object[] { p0 });
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006428 File Offset: 0x00004628
		public static string ExpectedFileNotFound(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ExpectedFileNotFound", new object[] { p0 });
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000643E File Offset: 0x0000463E
		public static string PathNotValid(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("PathNotValid", new object[] { p0 });
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00006454 File Offset: 0x00004654
		public static string MachineProcessorCountExceeded
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("MachineProcessorCountExceeded");
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006460 File Offset: 0x00004660
		public static string NoProcessorAffinity
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("NoProcessorAffinity");
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000646C File Offset: 0x0000466C
		public static string UnknownPermissionProperty(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnknownPermissionProperty", new object[] { p0, p1 });
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006486 File Offset: 0x00004686
		public static string ParameterSignatureCountOrNames
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ParameterSignatureCountOrNames");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00006492 File Offset: 0x00004692
		public static string ProcessorAffinityWindowsVersionNotSupported
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ProcessorAffinityWindowsVersionNotSupported");
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000649E File Offset: 0x0000469E
		public static string GetSchemaTooManyRestrictions(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("GetSchemaTooManyRestrictions", new object[] { p0 });
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000064B4 File Offset: 0x000046B4
		public static string GetSchemaCollectionNotSupported(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("GetSchemaCollectionNotSupported", new object[] { p0 });
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000064CA File Offset: 0x000046CA
		public static string TypeNotSupported(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("TypeNotSupported", new object[] { p0 });
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000064E0 File Offset: 0x000046E0
		public static string ContainerMaxCountOutOfRange
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ContainerMaxCountOutOfRange");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000064EC File Offset: 0x000046EC
		public static string ContainerMinCountOutOfRange
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("ContainerMinCountOutOfRange");
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000064F8 File Offset: 0x000046F8
		public static string UnknownPool(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("UnknownPool", new object[] { p0 });
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000650E File Offset: 0x0000470E
		public static string JobOptionsForbidden
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("JobOptionsForbidden");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000651A File Offset: 0x0000471A
		public static string InvalidOption
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("InvalidOption");
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006526 File Offset: 0x00004726
		public static string ModuleNameDuplicate(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ModuleNameDuplicate", new object[] { p0 });
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000653C File Offset: 0x0000473C
		public static string ModuleNameChanged(object p0, object p1)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ModuleNameChanged", new object[] { p0, p1 });
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006556 File Offset: 0x00004756
		public static string ModuleNameNotFound(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("ModuleNameNotFound", new object[] { p0 });
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000656C File Offset: 0x0000476C
		public static string PropertiesAndSwitchesCannotBothBeNonEmpty
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("PropertiesAndSwitchesCannotBothBeNonEmpty");
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006578 File Offset: 0x00004778
		public static string DuplicateNameFound(object p0)
		{
			return ProviderErrorStrings.ResourceLoader.GetString("DuplicateNameFound", new object[] { p0 });
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000658E File Offset: 0x0000478E
		public static string MissingPrivacySetting
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("MissingPrivacySetting");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000659A File Offset: 0x0000479A
		public static string CannotCreateBookmark
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("CannotCreateBookmark");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000065A6 File Offset: 0x000047A6
		public static string UriCannotContainPath
		{
			get
			{
				return ProviderErrorStrings.ResourceLoader.GetString("UriCannotContainPath");
			}
		}

		// Token: 0x02000034 RID: 52
		private class ResourceLoader
		{
			// Token: 0x060001DD RID: 477 RVA: 0x000074FA File Offset: 0x000056FA
			internal ResourceLoader()
			{
				this.resources = new ResourceManager("Microsoft.Data.Mashup.ProviderCommon.ProviderErrorStrings", base.GetType().Assembly);
			}

			// Token: 0x060001DE RID: 478 RVA: 0x00007520 File Offset: 0x00005720
			private static ProviderErrorStrings.ResourceLoader GetLoader()
			{
				if (ProviderErrorStrings.ResourceLoader.instance == null)
				{
					ProviderErrorStrings.ResourceLoader resourceLoader = new ProviderErrorStrings.ResourceLoader();
					Interlocked.CompareExchange<ProviderErrorStrings.ResourceLoader>(ref ProviderErrorStrings.ResourceLoader.instance, resourceLoader, null);
				}
				return ProviderErrorStrings.ResourceLoader.instance;
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060001DF RID: 479 RVA: 0x0000754C File Offset: 0x0000574C
			public static CultureInfo Culture
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000754F File Offset: 0x0000574F
			public static ResourceManager Resources
			{
				get
				{
					return ProviderErrorStrings.ResourceLoader.GetLoader().resources;
				}
			}

			// Token: 0x060001E1 RID: 481 RVA: 0x0000755C File Offset: 0x0000575C
			public static string GetString(string name, params object[] args)
			{
				ProviderErrorStrings.ResourceLoader loader = ProviderErrorStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				string @string = loader.resources.GetString(name, ProviderErrorStrings.ResourceLoader.Culture);
				if (args != null && args.Length != 0)
				{
					return string.Format(CultureInfo.CurrentCulture, @string, args);
				}
				return @string;
			}

			// Token: 0x060001E2 RID: 482 RVA: 0x0000759C File Offset: 0x0000579C
			public static string GetString(string name)
			{
				ProviderErrorStrings.ResourceLoader loader = ProviderErrorStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetString(name, ProviderErrorStrings.ResourceLoader.Culture);
			}

			// Token: 0x060001E3 RID: 483 RVA: 0x000075C8 File Offset: 0x000057C8
			public static object GetObject(string name)
			{
				ProviderErrorStrings.ResourceLoader loader = ProviderErrorStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.resources.GetObject(name, ProviderErrorStrings.ResourceLoader.Culture);
			}

			// Token: 0x060001E4 RID: 484 RVA: 0x000075F4 File Offset: 0x000057F4
			public static T GetObject<T>(string name) where T : class
			{
				ProviderErrorStrings.ResourceLoader loader = ProviderErrorStrings.ResourceLoader.GetLoader();
				if (loader == null)
				{
					return default(T);
				}
				return (T)((object)loader.resources.GetObject(name, ProviderErrorStrings.ResourceLoader.Culture));
			}

			// Token: 0x040000D6 RID: 214
			private static ProviderErrorStrings.ResourceLoader instance;

			// Token: 0x040000D7 RID: 215
			private ResourceManager resources;
		}
	}
}
