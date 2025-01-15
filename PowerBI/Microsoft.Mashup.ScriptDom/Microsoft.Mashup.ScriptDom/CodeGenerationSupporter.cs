using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000110 RID: 272
	internal static class CodeGenerationSupporter
	{
		// Token: 0x04000B89 RID: 2953
		internal const string KB = "KB";

		// Token: 0x04000B8A RID: 2954
		internal const string MB = "MB";

		// Token: 0x04000B8B RID: 2955
		internal const string GB = "GB";

		// Token: 0x04000B8C RID: 2956
		internal const string TB = "TB";

		// Token: 0x04000B8D RID: 2957
		internal const string ACP = "ACP";

		// Token: 0x04000B8E RID: 2958
		internal const string OEM = "OEM";

		// Token: 0x04000B8F RID: 2959
		internal const string Tcp = "TCP";

		// Token: 0x04000B90 RID: 2960
		internal const string Http = "HTTP";

		// Token: 0x04000B91 RID: 2961
		internal const string Days = "DAYS";

		// Token: 0x04000B92 RID: 2962
		internal const string Hours = "HOURS";

		// Token: 0x04000B93 RID: 2963
		internal const string Minutes = "MINUTES";

		// Token: 0x04000B94 RID: 2964
		internal const string RC2 = "RC2";

		// Token: 0x04000B95 RID: 2965
		internal const string RC4 = "RC4";

		// Token: 0x04000B96 RID: 2966
		internal const string RC4_128 = "RC4_128";

		// Token: 0x04000B97 RID: 2967
		internal const string Des = "DES";

		// Token: 0x04000B98 RID: 2968
		internal const string TripleDes = "TRIPLE_DES";

		// Token: 0x04000B99 RID: 2969
		internal const string TripleDes3Key = "TRIPLE_DES_3KEY";

		// Token: 0x04000B9A RID: 2970
		internal const string DesX = "DESX";

		// Token: 0x04000B9B RID: 2971
		internal const string Aes = "AES";

		// Token: 0x04000B9C RID: 2972
		internal const string Aes128 = "AES_128";

		// Token: 0x04000B9D RID: 2973
		internal const string Aes192 = "AES_192";

		// Token: 0x04000B9E RID: 2974
		internal const string Aes256 = "AES_256";

		// Token: 0x04000B9F RID: 2975
		internal const string Rsa512 = "RSA_512";

		// Token: 0x04000BA0 RID: 2976
		internal const string Rsa1024 = "RSA_1024";

		// Token: 0x04000BA1 RID: 2977
		internal const string Rsa2048 = "RSA_2048";

		// Token: 0x04000BA2 RID: 2978
		internal const string QuotedIdentifier = "QUOTED_IDENTIFIER";

		// Token: 0x04000BA3 RID: 2979
		internal const string ConcatNullYieldsNull = "CONCAT_NULL_YIELDS_NULL";

		// Token: 0x04000BA4 RID: 2980
		internal const string ArithAbort = "ARITHABORT";

		// Token: 0x04000BA5 RID: 2981
		internal const string ArithIgnore = "ARITHIGNORE";

		// Token: 0x04000BA6 RID: 2982
		internal const string FmtOnly = "FMTONLY";

		// Token: 0x04000BA7 RID: 2983
		internal const string NoCompression = "NO_COMPRESSION";

		// Token: 0x04000BA8 RID: 2984
		internal const string NoCount = "NOCOUNT";

		// Token: 0x04000BA9 RID: 2985
		internal const string NoExec = "NOEXEC";

		// Token: 0x04000BAA RID: 2986
		internal const string NumericRoundAbort = "NUMERIC_ROUNDABORT";

		// Token: 0x04000BAB RID: 2987
		internal const string ParseOnly = "PARSEONLY";

		// Token: 0x04000BAC RID: 2988
		internal const string AnsiDefaults = "ANSI_DEFAULTS";

		// Token: 0x04000BAD RID: 2989
		internal const string AnsiNullDfltOff = "ANSI_NULL_DFLT_OFF";

		// Token: 0x04000BAE RID: 2990
		internal const string AnsiNullDfltOn = "ANSI_NULL_DFLT_ON";

		// Token: 0x04000BAF RID: 2991
		internal const string AnsiNulls = "ANSI_NULLS";

		// Token: 0x04000BB0 RID: 2992
		internal const string AnsiPadding = "ANSI_PADDING";

		// Token: 0x04000BB1 RID: 2993
		internal const string AnsiWarnings = "ANSI_WARNINGS";

		// Token: 0x04000BB2 RID: 2994
		internal const string ForcePlan = "FORCEPLAN";

		// Token: 0x04000BB3 RID: 2995
		internal const string ShowPlanAll = "SHOWPLAN_ALL";

		// Token: 0x04000BB4 RID: 2996
		internal const string ShowPlanText = "SHOWPLAN_TEXT";

		// Token: 0x04000BB5 RID: 2997
		internal const string IO = "IO";

		// Token: 0x04000BB6 RID: 2998
		internal const string Profile = "PROFILE";

		// Token: 0x04000BB7 RID: 2999
		internal const string ImplicitTransactions = "IMPLICIT_TRANSACTIONS";

		// Token: 0x04000BB8 RID: 3000
		internal const string RemoteProcTransactions = "REMOTE_PROC_TRANSACTIONS";

		// Token: 0x04000BB9 RID: 3001
		internal const string XactAbort = "XACT_ABORT";

		// Token: 0x04000BBA RID: 3002
		internal const string Absent = "ABSENT";

		// Token: 0x04000BBB RID: 3003
		internal const string Absolute = "ABSOLUTE";

		// Token: 0x04000BBC RID: 3004
		internal const string AccentSensitivity = "ACCENT_SENSITIVITY";

		// Token: 0x04000BBD RID: 3005
		internal const string Action = "ACTION";

		// Token: 0x04000BBE RID: 3006
		internal const string Active = "ACTIVE";

		// Token: 0x04000BBF RID: 3007
		internal const string Activation = "ACTIVATION";

		// Token: 0x04000BC0 RID: 3008
		internal const string Add = "ADD";

		// Token: 0x04000BC1 RID: 3009
		internal const string Address = "ADDRESS";

		// Token: 0x04000BC2 RID: 3010
		internal const string Admin = "ADMIN";

		// Token: 0x04000BC3 RID: 3011
		internal const string Affinity = "AFFINITY";

		// Token: 0x04000BC4 RID: 3012
		internal const string After = "AFTER";

		// Token: 0x04000BC5 RID: 3013
		internal const string Aggregate = "AGGREGATE";

		// Token: 0x04000BC6 RID: 3014
		internal const string Algorithm = "ALGORITHM";

		// Token: 0x04000BC7 RID: 3015
		internal const string AlterColumn = "ALTERCOLUMN";

		// Token: 0x04000BC8 RID: 3016
		internal const string All = "ALL";

		// Token: 0x04000BC9 RID: 3017
		internal const string AllConstraints = "ALL_CONSTRAINTS";

		// Token: 0x04000BCA RID: 3018
		internal const string AllErrorMessages = "ALL_ERRORMSGS";

		// Token: 0x04000BCB RID: 3019
		internal const string AllIndexes = "ALL_INDEXES";

		// Token: 0x04000BCC RID: 3020
		internal const string AllLevels = "ALL_LEVELS";

		// Token: 0x04000BCD RID: 3021
		internal const string AllowConnections = "ALLOW_CONNECTIONS";

		// Token: 0x04000BCE RID: 3022
		internal const string AllowMultipleEventLoss = "ALLOW_MULTIPLE_EVENT_LOSS";

		// Token: 0x04000BCF RID: 3023
		internal const string AllowSingleEventLoss = "ALLOW_SINGLE_EVENT_LOSS";

		// Token: 0x04000BD0 RID: 3024
		internal const string AllowSnapshotIsolation = "ALLOW_SNAPSHOT_ISOLATION";

		// Token: 0x04000BD1 RID: 3025
		internal const string AllowPageLocks = "ALLOW_PAGE_LOCKS";

		// Token: 0x04000BD2 RID: 3026
		internal const string AllowRowLocks = "ALLOW_ROW_LOCKS";

		// Token: 0x04000BD3 RID: 3027
		internal const string AllResults = "ALL_RESULTS";

		// Token: 0x04000BD4 RID: 3028
		internal const string AllSparseColumns = "ALL_SPARSE_COLUMNS";

		// Token: 0x04000BD5 RID: 3029
		internal const string Anonymous = "ANONYMOUS";

		// Token: 0x04000BD6 RID: 3030
		internal const string AnsiNullDefault = "ANSI_NULL_DEFAULT";

		// Token: 0x04000BD7 RID: 3031
		internal const string Application = "APPLICATION";

		// Token: 0x04000BD8 RID: 3032
		internal const string ApplicationLog = "APPLICATION_LOG";

		// Token: 0x04000BD9 RID: 3033
		internal const string Apply = "APPLY";

		// Token: 0x04000BDA RID: 3034
		internal const string ApplyDelay = "APPLY_DELAY";

		// Token: 0x04000BDB RID: 3035
		internal const string Assembly = "ASSEMBLY";

		// Token: 0x04000BDC RID: 3036
		internal const string Asymmetric = "ASYMMETRIC";

		// Token: 0x04000BDD RID: 3037
		internal const string AsynchronousCommit = "ASYNCHRONOUS_COMMIT";

		// Token: 0x04000BDE RID: 3038
		internal const string At = "AT";

		// Token: 0x04000BDF RID: 3039
		internal const string Attach = "ATTACH";

		// Token: 0x04000BE0 RID: 3040
		internal const string AttachRebuildLog = "ATTACH_REBUILD_LOG";

		// Token: 0x04000BE1 RID: 3041
		internal const string AttachForceRebuildLog = "ATTACH_FORCE_REBUILD_LOG";

		// Token: 0x04000BE2 RID: 3042
		internal const string Append = "APPEND";

		// Token: 0x04000BE3 RID: 3043
		internal const string Avg = "AVG";

		// Token: 0x04000BE4 RID: 3044
		internal const string Attested = "ATTESTED";

		// Token: 0x04000BE5 RID: 3045
		internal const string AuditGuid = "AUDIT_GUID";

		// Token: 0x04000BE6 RID: 3046
		internal const string Authentication = "AUTHENTICATION";

		// Token: 0x04000BE7 RID: 3047
		internal const string AuthRealm = "AUTH_REALM";

		// Token: 0x04000BE8 RID: 3048
		internal const string Auto = "AUTO";

		// Token: 0x04000BE9 RID: 3049
		internal const string AutoCleanup = "AUTO_CLEANUP";

		// Token: 0x04000BEA RID: 3050
		internal const string AutoClose = "AUTO_CLOSE";

		// Token: 0x04000BEB RID: 3051
		internal const string AutoCreateStatistics = "AUTO_CREATE_STATISTICS";

		// Token: 0x04000BEC RID: 3052
		internal const string Automatic = "AUTOMATIC";

		// Token: 0x04000BED RID: 3053
		internal const string AutoShrink = "AUTO_SHRINK";

		// Token: 0x04000BEE RID: 3054
		internal const string AutoUpdateStatistics = "AUTO_UPDATE_STATISTICS";

		// Token: 0x04000BEF RID: 3055
		internal const string AutoUpdateStatisticsAsync = "AUTO_UPDATE_STATISTICS_ASYNC";

		// Token: 0x04000BF0 RID: 3056
		internal const string Availability = "AVAILABILITY";

		// Token: 0x04000BF1 RID: 3057
		internal const string AvailabilityMode = "AVAILABILITY_MODE";

		// Token: 0x04000BF2 RID: 3058
		internal const string Base64 = "BASE64";

		// Token: 0x04000BF3 RID: 3059
		internal const string Basic = "BASIC";

		// Token: 0x04000BF4 RID: 3060
		internal const string Batches = "BATCHES";

		// Token: 0x04000BF5 RID: 3061
		internal const string BatchSize = "BATCHSIZE";

		// Token: 0x04000BF6 RID: 3062
		internal const string BeginDialog = "BEGIN_DIALOG";

		// Token: 0x04000BF7 RID: 3063
		internal const string BigInt = "BIGINT";

		// Token: 0x04000BF8 RID: 3064
		internal const string Binding = "BINDING";

		// Token: 0x04000BF9 RID: 3065
		internal const string Binary = "BINARY";

		// Token: 0x04000BFA RID: 3066
		internal const string Bit = "BIT";

		// Token: 0x04000BFB RID: 3067
		internal const string BlockSize = "BLOCKSIZE";

		// Token: 0x04000BFC RID: 3068
		internal const string BoundingBox = "BOUNDING_BOX";

		// Token: 0x04000BFD RID: 3069
		internal const string Broker = "BROKER";

		// Token: 0x04000BFE RID: 3070
		internal const string BrokerInstance = "BROKER_INSTANCE";

		// Token: 0x04000BFF RID: 3071
		internal const string BufferCount = "BUFFERCOUNT";

		// Token: 0x04000C00 RID: 3072
		internal const string BulkLogged = "BULK_LOGGED";

		// Token: 0x04000C01 RID: 3073
		internal const string Bypass = "BYPASS";

		// Token: 0x04000C02 RID: 3074
		internal const string Cache = "CACHE";

		// Token: 0x04000C03 RID: 3075
		internal const string Called = "CALLED";

		// Token: 0x04000C04 RID: 3076
		internal const string Caller = "CALLER";

		// Token: 0x04000C05 RID: 3077
		internal const string CapCpuPercent = "CAP_CPU_PERCENT";

		// Token: 0x04000C06 RID: 3078
		internal const string CapIoPercent = "CAP_IO_PERCENT";

		// Token: 0x04000C07 RID: 3079
		internal const string CardinalityTunerLimit = "CARDINALITY_TUNER_LIMIT";

		// Token: 0x04000C08 RID: 3080
		internal const string Cast = "CAST";

		// Token: 0x04000C09 RID: 3081
		internal const string Catalog = "CATALOG";

		// Token: 0x04000C0A RID: 3082
		internal const string Catch = "CATCH";

		// Token: 0x04000C0B RID: 3083
		internal const string CellsPerObject = "CELLS_PER_OBJECT";

		// Token: 0x04000C0C RID: 3084
		internal const string Certificate = "CERTIFICATE";

		// Token: 0x04000C0D RID: 3085
		internal const string Credential = "CREDENTIAL";

		// Token: 0x04000C0E RID: 3086
		internal const string ChangeRetention = "CHANGE_RETENTION";

		// Token: 0x04000C0F RID: 3087
		internal const string Changes = "CHANGES";

		// Token: 0x04000C10 RID: 3088
		internal const string ChangeTable = "CHANGETABLE";

		// Token: 0x04000C11 RID: 3089
		internal const string ChangeTracking = "CHANGE_TRACKING";

		// Token: 0x04000C12 RID: 3090
		internal const string ChangeTrackingContext = "CHANGE_TRACKING_CONTEXT";

		// Token: 0x04000C13 RID: 3091
		internal const string Char = "CHAR";

		// Token: 0x04000C14 RID: 3092
		internal const string CharacterSet = "CHARACTER_SET";

		// Token: 0x04000C15 RID: 3093
		internal const string CheckConstraints = "CHECK_CONSTRAINTS";

		// Token: 0x04000C16 RID: 3094
		internal const string CheckConstraintsHint = "CHECKCONSTRAINTS";

		// Token: 0x04000C17 RID: 3095
		internal const string CheckExpiration = "CHECK_EXPIRATION";

		// Token: 0x04000C18 RID: 3096
		internal const string CheckPolicy = "CHECK_POLICY";

		// Token: 0x04000C19 RID: 3097
		internal const string Checksum = "CHECKSUM";

		// Token: 0x04000C1A RID: 3098
		internal const string ChecksumAgg = "CHECKSUM_AGG";

		// Token: 0x04000C1B RID: 3099
		internal const string ClassifierFunction = "CLASSIFIER_FUNCTION";

		// Token: 0x04000C1C RID: 3100
		internal const string Cleanup = "CLEANUP";

		// Token: 0x04000C1D RID: 3101
		internal const string Clear = "CLEAR";

		// Token: 0x04000C1E RID: 3102
		internal const string ClearPort = "CLEAR_PORT";

		// Token: 0x04000C1F RID: 3103
		internal const string CodePage = "CODEPAGE";

		// Token: 0x04000C20 RID: 3104
		internal const string Collection = "COLLECTION";

		// Token: 0x04000C21 RID: 3105
		internal const string Columns = "COLUMNS";

		// Token: 0x04000C22 RID: 3106
		internal const string ColumnSet = "COLUMN_SET";

		// Token: 0x04000C23 RID: 3107
		internal const string ColumnStore = "COLUMNSTORE";

		// Token: 0x04000C24 RID: 3108
		internal const string CommitDifferentialBase = "COMMIT_DIFFERENTIAL_BASE";

		// Token: 0x04000C25 RID: 3109
		internal const string Committed = "COMMITTED";

		// Token: 0x04000C26 RID: 3110
		internal const string CompatibilityLevel = "COMPATIBILITY_LEVEL";

		// Token: 0x04000C27 RID: 3111
		internal const string Compression = "COMPRESSION";

		// Token: 0x04000C28 RID: 3112
		internal const string Concat = "CONCAT";

		// Token: 0x04000C29 RID: 3113
		internal const string Configuration = "CONFIGURATION";

		// Token: 0x04000C2A RID: 3114
		internal const string Containment = "CONTAINMENT";

		// Token: 0x04000C2B RID: 3115
		internal const string Content = "CONTENT";

		// Token: 0x04000C2C RID: 3116
		internal const string ContextInfo = "CONTEXT_INFO";

		// Token: 0x04000C2D RID: 3117
		internal const string ContinueAfterError = "CONTINUE_AFTER_ERROR";

		// Token: 0x04000C2E RID: 3118
		internal const string Contract = "CONTRACT";

		// Token: 0x04000C2F RID: 3119
		internal const string ContractName = "CONTRACT_NAME";

		// Token: 0x04000C30 RID: 3120
		internal const string Conversation = "CONVERSATION";

		// Token: 0x04000C31 RID: 3121
		internal const string ConversationGroupId = "CONVERSATION_GROUP_ID";

		// Token: 0x04000C32 RID: 3122
		internal const string ConversationHandle = "CONVERSATION_HANDLE";

		// Token: 0x04000C33 RID: 3123
		internal const string Cookie = "COOKIE";

		// Token: 0x04000C34 RID: 3124
		internal const string Copy = "COPY";

		// Token: 0x04000C35 RID: 3125
		internal const string CopyOnly = "COPY_ONLY";

		// Token: 0x04000C36 RID: 3126
		internal const string Correlated = "CORRELATED";

		// Token: 0x04000C37 RID: 3127
		internal const string Count = "COUNT";

		// Token: 0x04000C38 RID: 3128
		internal const string CountBig = "COUNT_BIG";

		// Token: 0x04000C39 RID: 3129
		internal const string Counter = "COUNTER";

		// Token: 0x04000C3A RID: 3130
		internal const string CountRows = "COUNT_ROWS";

		// Token: 0x04000C3B RID: 3131
		internal const string Cpu = "CPU";

		// Token: 0x04000C3C RID: 3132
		internal const string CreateNew = "CREATE_NEW";

		// Token: 0x04000C3D RID: 3133
		internal const string CreationDisposition = "CREATION_DISPOSITION";

		// Token: 0x04000C3E RID: 3134
		internal const string Cryptographic = "CRYPTOGRAPHIC";

		// Token: 0x04000C3F RID: 3135
		internal const string Cube = "CUBE";

		// Token: 0x04000C40 RID: 3136
		internal const string Cuid = "CUID";

		// Token: 0x04000C41 RID: 3137
		internal const string CursorCloseOnCommit = "CURSOR_CLOSE_ON_COMMIT";

		// Token: 0x04000C42 RID: 3138
		internal const string CursorDefault = "CURSOR_DEFAULT";

		// Token: 0x04000C43 RID: 3139
		internal const string Cycle = "Cycle";

		// Token: 0x04000C44 RID: 3140
		internal const string D = "D";

		// Token: 0x04000C45 RID: 3141
		internal const string Data = "DATA";

		// Token: 0x04000C46 RID: 3142
		internal const string Database = "DATABASE";

		// Token: 0x04000C47 RID: 3143
		internal const string DatabaseMirroring = "DATABASE_MIRRORING";

		// Token: 0x04000C48 RID: 3144
		internal const string DataCompression = "DATA_COMPRESSION";

		// Token: 0x04000C49 RID: 3145
		internal const string DataMirroring = "DATA_MIRRORING";

		// Token: 0x04000C4A RID: 3146
		internal const string DataFileType = "DATAFILETYPE";

		// Token: 0x04000C4B RID: 3147
		internal const string Date = "DATE";

		// Token: 0x04000C4C RID: 3148
		internal const string DateCorrelationOptimization = "DATE_CORRELATION_OPTIMIZATION";

		// Token: 0x04000C4D RID: 3149
		internal const string DateFirst = "DATEFIRST";

		// Token: 0x04000C4E RID: 3150
		internal const string DateFormat = "DATEFORMAT";

		// Token: 0x04000C4F RID: 3151
		internal const string DateTime = "DATETIME";

		// Token: 0x04000C50 RID: 3152
		internal const string DateTime2 = "DATETIME2";

		// Token: 0x04000C51 RID: 3153
		internal const string DateTimeOffset = "DATETIMEOFFSET";

		// Token: 0x04000C52 RID: 3154
		internal const string DatabaseSnapshot = "DATABASE_SNAPSHOT";

		// Token: 0x04000C53 RID: 3155
		internal const string DataPurity = "DATA_PURITY";

		// Token: 0x04000C54 RID: 3156
		internal const string DboOnly = "DBO_ONLY";

		// Token: 0x04000C55 RID: 3157
		internal const string DbChaining = "DB_CHAINING";

		// Token: 0x04000C56 RID: 3158
		internal const string DeadlockPriority = "DEADLOCK_PRIORITY";

		// Token: 0x04000C57 RID: 3159
		internal const string Decimal = "DECIMAL";

		// Token: 0x04000C58 RID: 3160
		internal const string Decryption = "DECRYPTION";

		// Token: 0x04000C59 RID: 3161
		internal const string Default = "DEFAULT";

		// Token: 0x04000C5A RID: 3162
		internal const string DefaultDatabase = "DEFAULT_DATABASE";

		// Token: 0x04000C5B RID: 3163
		internal const string DefaultFullTextLanguage = "DEFAULT_FULLTEXT_LANGUAGE";

		// Token: 0x04000C5C RID: 3164
		internal const string DefaultLanguage = "DEFAULT_LANGUAGE";

		// Token: 0x04000C5D RID: 3165
		internal const string DefaultSchema = "DEFAULT_SCHEMA";

		// Token: 0x04000C5E RID: 3166
		internal const string DefaultLogonDomain = "DEFAULT_LOGON_DOMAIN";

		// Token: 0x04000C5F RID: 3167
		internal const string DensityVector = "DENSITY_VECTOR";

		// Token: 0x04000C60 RID: 3168
		internal const string Dependents = "DEPENDENTS";

		// Token: 0x04000C61 RID: 3169
		internal const string Description = "DESCRIPTION";

		// Token: 0x04000C62 RID: 3170
		internal const string Delay = "DELAY";

		// Token: 0x04000C63 RID: 3171
		internal const string Dialog = "DIALOG";

		// Token: 0x04000C64 RID: 3172
		internal const string Differential = "DIFFERENTIAL";

		// Token: 0x04000C65 RID: 3173
		internal const string Digest = "DIGEST";

		// Token: 0x04000C66 RID: 3174
		internal const string DirectoryName = "DIRECTORY_NAME";

		// Token: 0x04000C67 RID: 3175
		internal const string Disable = "DISABLE";

		// Token: 0x04000C68 RID: 3176
		internal const string Disabled = "DISABLED";

		// Token: 0x04000C69 RID: 3177
		internal const string DisableBroker = "DISABLE_BROKER";

		// Token: 0x04000C6A RID: 3178
		internal const string DisableDefCnstChk = "DISABLE_DEF_CNST_CHK";

		// Token: 0x04000C6B RID: 3179
		internal const string Disk = "DISK";

		// Token: 0x04000C6C RID: 3180
		internal const string Document = "DOCUMENT";

		// Token: 0x04000C6D RID: 3181
		internal const string DollarSign = "$";

		// Token: 0x04000C6E RID: 3182
		internal const string DollarPartition = "$PARTITION";

		// Token: 0x04000C6F RID: 3183
		internal const string Drop = "DROP";

		// Token: 0x04000C70 RID: 3184
		internal const string DropExisting = "DROP_EXISTING";

		// Token: 0x04000C71 RID: 3185
		internal const string DTSBuffers = "DTS_BUFFERS";

		// Token: 0x04000C72 RID: 3186
		internal const string Dynamic = "DYNAMIC";

		// Token: 0x04000C73 RID: 3187
		internal const string Edition = "EDITION";

		// Token: 0x04000C74 RID: 3188
		internal const string Elements = "ELEMENTS";

		// Token: 0x04000C75 RID: 3189
		internal const string Emergency = "EMERGENCY";

		// Token: 0x04000C76 RID: 3190
		internal const string Empty = "EMPTY";

		// Token: 0x04000C77 RID: 3191
		internal const string Enable = "ENABLE";

		// Token: 0x04000C78 RID: 3192
		internal const string Enabled = "ENABLED";

		// Token: 0x04000C79 RID: 3193
		internal const string EnableBroker = "ENABLE_BROKER";

		// Token: 0x04000C7A RID: 3194
		internal const string Encryption = "ENCRYPTION";

		// Token: 0x04000C7B RID: 3195
		internal const string EnhancedIntegrity = "ENHANCEDINTEGRITY";

		// Token: 0x04000C7C RID: 3196
		internal const string Endpoint = "ENDPOINT";

		// Token: 0x04000C7D RID: 3197
		internal const string EndpointUrl = "ENDPOINT_URL";

		// Token: 0x04000C7E RID: 3198
		internal const string Entry = "ENTRY";

		// Token: 0x04000C7F RID: 3199
		internal const string Equal = "=";

		// Token: 0x04000C80 RID: 3200
		internal const string Error = "ERROR";

		// Token: 0x04000C81 RID: 3201
		internal const string ErrorBrokerConversations = "ERROR_BROKER_CONVERSATIONS";

		// Token: 0x04000C82 RID: 3202
		internal const string ErrorFile = "ERRORFILE";

		// Token: 0x04000C83 RID: 3203
		internal const string EstimateOnly = "ESTIMATEONLY";

		// Token: 0x04000C84 RID: 3204
		internal const string Event = "EVENT";

		// Token: 0x04000C85 RID: 3205
		internal const string EventRetentionMode = "EVENT_RETENTION_MODE";

		// Token: 0x04000C86 RID: 3206
		internal const string Exclamation = "!";

		// Token: 0x04000C87 RID: 3207
		internal const string Executable = "EXECUTABLE";

		// Token: 0x04000C88 RID: 3208
		internal const string Explicit = "EXPLICIT";

		// Token: 0x04000C89 RID: 3209
		internal const string Expand = "EXPAND";

		// Token: 0x04000C8A RID: 3210
		internal const string ExpireDate = "EXPIREDATE";

		// Token: 0x04000C8B RID: 3211
		internal const string ExpiryDate = "EXPIRY_DATE";

		// Token: 0x04000C8C RID: 3212
		internal const string ExtendedLogicalChecks = "EXTENDED_LOGICAL_CHECKS";

		// Token: 0x04000C8D RID: 3213
		internal const string External = "EXTERNAL";

		// Token: 0x04000C8E RID: 3214
		internal const string ExternalAccess = "EXTERNAL_ACCESS";

		// Token: 0x04000C8F RID: 3215
		internal const string Extract = "EXTRACT";

		// Token: 0x04000C90 RID: 3216
		internal const string FailOperation = "FAIL_OPERATION";

		// Token: 0x04000C91 RID: 3217
		internal const string Failover = "FAILOVER";

		// Token: 0x04000C92 RID: 3218
		internal const string FailoverMode = "FAILOVER_MODE";

		// Token: 0x04000C93 RID: 3219
		internal const string FanIn = "FAN_IN";

		// Token: 0x04000C94 RID: 3220
		internal const string Fast = "FAST";

		// Token: 0x04000C95 RID: 3221
		internal const string FastForward = "FAST_FORWARD";

		// Token: 0x04000C96 RID: 3222
		internal const string FastFirstRow = "FASTFIRSTROW";

		// Token: 0x04000C97 RID: 3223
		internal const string Federated = "FEDERATED";

		// Token: 0x04000C98 RID: 3224
		internal const string Federation = "FEDERATION";

		// Token: 0x04000C99 RID: 3225
		internal const string File = "FILE";

		// Token: 0x04000C9A RID: 3226
		internal const string Filegroup = "FILEGROUP";

		// Token: 0x04000C9B RID: 3227
		internal const string FileGrowth = "FILEGROWTH";

		// Token: 0x04000C9C RID: 3228
		internal const string FileListOnly = "FILELISTONLY";

		// Token: 0x04000C9D RID: 3229
		internal const string FileName = "FILENAME";

		// Token: 0x04000C9E RID: 3230
		internal const string FilePath = "FILEPATH";

		// Token: 0x04000C9F RID: 3231
		internal const string FileStream = "FILESTREAM";

		// Token: 0x04000CA0 RID: 3232
		internal const string FileStreamOn = "FILESTREAM_ON";

		// Token: 0x04000CA1 RID: 3233
		internal const string FileTable = "FILETABLE";

		// Token: 0x04000CA2 RID: 3234
		internal const string FileTableCollateFileName = "FILETABLE_COLLATE_FILENAME";

		// Token: 0x04000CA3 RID: 3235
		internal const string FileTableDirectory = "FILETABLE_DIRECTORY";

		// Token: 0x04000CA4 RID: 3236
		internal const string FileTableFullPathUniqueConstraintName = "FILETABLE_FULLPATH_UNIQUE_CONSTRAINT_NAME";

		// Token: 0x04000CA5 RID: 3237
		internal const string FileTableNamespace = "FILETABLE_NAMESPACE";

		// Token: 0x04000CA6 RID: 3238
		internal const string FileTablePrimaryKeyConstraintName = "FILETABLE_PRIMARY_KEY_CONSTRAINT_NAME";

		// Token: 0x04000CA7 RID: 3239
		internal const string FileTableStreamIdUniqueConstraintName = "FILETABLE_STREAMID_UNIQUE_CONSTRAINT_NAME";

		// Token: 0x04000CA8 RID: 3240
		internal const string FillFactor = "FILLFACTOR";

		// Token: 0x04000CA9 RID: 3241
		internal const string Filtering = "FILTERING";

		// Token: 0x04000CAA RID: 3242
		internal const string FireTriggers = "FIRE_TRIGGERS";

		// Token: 0x04000CAB RID: 3243
		internal const string FirstRow = "FIRSTROW";

		// Token: 0x04000CAC RID: 3244
		internal const string FieldTerminator = "FIELDTERMINATOR";

		// Token: 0x04000CAD RID: 3245
		internal const string FipsFlagger = "FIPS_FLAGGER";

		// Token: 0x04000CAE RID: 3246
		internal const string First = "FIRST";

		// Token: 0x04000CAF RID: 3247
		internal const string Fn = "FN";

		// Token: 0x04000CB0 RID: 3248
		internal const string Float = "FLOAT";

		// Token: 0x04000CB1 RID: 3249
		internal const string ForceFailoverAllowDataLoss = "FORCE_FAILOVER_ALLOW_DATA_LOSS";

		// Token: 0x04000CB2 RID: 3250
		internal const string ForceScan = "FORCESCAN";

		// Token: 0x04000CB3 RID: 3251
		internal const string ForceSeek = "FORCESEEK";

		// Token: 0x04000CB4 RID: 3252
		internal const string ForceServiceAllowDataLoss = "FORCE_SERVICE_ALLOW_DATA_LOSS";

		// Token: 0x04000CB5 RID: 3253
		internal const string ForwardOnly = "FORWARD_ONLY";

		// Token: 0x04000CB6 RID: 3254
		internal const string Force = "FORCE";

		// Token: 0x04000CB7 RID: 3255
		internal const string Forced = "FORCED";

		// Token: 0x04000CB8 RID: 3256
		internal const string Format = "FORMAT";

		// Token: 0x04000CB9 RID: 3257
		internal const string FormatFile = "FORMATFILE";

		// Token: 0x04000CBA RID: 3258
		internal const string Full = "FULL";

		// Token: 0x04000CBB RID: 3259
		internal const string FullScan = "FULLSCAN";

		// Token: 0x04000CBC RID: 3260
		internal const string Fulltext = "FULLTEXT";

		// Token: 0x04000CBD RID: 3261
		internal const string General = "GENERAL";

		// Token: 0x04000CBE RID: 3262
		internal const string GeographyAutoGrid = "GEOGRAPHY_AUTO_GRID";

		// Token: 0x04000CBF RID: 3263
		internal const string GeographyGrid = "GEOGRAPHY_GRID";

		// Token: 0x04000CC0 RID: 3264
		internal const string GeometryAutoGrid = "GEOMETRY_AUTO_GRID";

		// Token: 0x04000CC1 RID: 3265
		internal const string GeometryGrid = "GEOMETRY_GRID";

		// Token: 0x04000CC2 RID: 3266
		internal const string Get = "GET";

		// Token: 0x04000CC3 RID: 3267
		internal const string Global = "GLOBAL";

		// Token: 0x04000CC4 RID: 3268
		internal const string Governor = "GOVERNOR";

		// Token: 0x04000CC5 RID: 3269
		internal const string Grids = "GRIDS";

		// Token: 0x04000CC6 RID: 3270
		internal const string Group = "GROUP";

		// Token: 0x04000CC7 RID: 3271
		internal const string Grouping = "GROUPING";

		// Token: 0x04000CC8 RID: 3272
		internal const string GroupMaxRequests = "GROUP_MAX_REQUESTS";

		// Token: 0x04000CC9 RID: 3273
		internal const string GroupMinMemoryPercent = "GROUP_MIN_MEMORY_PERCENT";

		// Token: 0x04000CCA RID: 3274
		internal const string Guid = "GUID";

		// Token: 0x04000CCB RID: 3275
		internal const string Hadr = "HADR";

		// Token: 0x04000CCC RID: 3276
		internal const string Hash = "HASH";

		// Token: 0x04000CCD RID: 3277
		internal const string Hashed = "HASHED";

		// Token: 0x04000CCE RID: 3278
		internal const string HeaderLimit = "HEADER_LIMIT";

		// Token: 0x04000CCF RID: 3279
		internal const string HeaderOnly = "HEADERONLY";

		// Token: 0x04000CD0 RID: 3280
		internal const string High = "HIGH";

		// Token: 0x04000CD1 RID: 3281
		internal const string Hint = "HINT";

		// Token: 0x04000CD2 RID: 3282
		internal const string Histogram = "HISTOGRAM";

		// Token: 0x04000CD3 RID: 3283
		internal const string HistogramSteps = "HISTOGRAM_STEPS";

		// Token: 0x04000CD4 RID: 3284
		internal const string HonorBrokerPriority = "HONOR_BROKER_PRIORITY";

		// Token: 0x04000CD5 RID: 3285
		internal const string Identity = "IDENTITY";

		// Token: 0x04000CD6 RID: 3286
		internal const string IdentityValue = "IDENTITY_VALUE";

		// Token: 0x04000CD7 RID: 3287
		internal const string IgnoreConstraints = "IGNORE_CONSTRAINTS";

		// Token: 0x04000CD8 RID: 3288
		internal const string IgnoreDupKey = "IGNORE_DUP_KEY";

		// Token: 0x04000CD9 RID: 3289
		internal const string IgnoreNonClusteredColumnStoreIndex = "IGNORE_NONCLUSTERED_COLUMNSTORE_INDEX";

		// Token: 0x04000CDA RID: 3290
		internal const string IgnoreTriggers = "IGNORE_TRIGGERS";

		// Token: 0x04000CDB RID: 3291
		internal const string IIf = "IIF";

		// Token: 0x04000CDC RID: 3292
		internal const string Image = "IMAGE";

		// Token: 0x04000CDD RID: 3293
		internal const string Immediate = "IMMEDIATE";

		// Token: 0x04000CDE RID: 3294
		internal const string Importance = "IMPORTANCE";

		// Token: 0x04000CDF RID: 3295
		internal const string Include = "INCLUDE";

		// Token: 0x04000CE0 RID: 3296
		internal const string Increment = "INCREMENT";

		// Token: 0x04000CE1 RID: 3297
		internal const string Incremental = "INCREMENTAL";

		// Token: 0x04000CE2 RID: 3298
		internal const string Infinite = "INFINITE";

		// Token: 0x04000CE3 RID: 3299
		internal const string Init = "INIT";

		// Token: 0x04000CE4 RID: 3300
		internal const string Initiator = "INITIATOR";

		// Token: 0x04000CE5 RID: 3301
		internal const string Input = "INPUT";

		// Token: 0x04000CE6 RID: 3302
		internal const string Instead = "INSTEAD";

		// Token: 0x04000CE7 RID: 3303
		internal const string Int = "INT";

		// Token: 0x04000CE8 RID: 3304
		internal const string Integrated = "INTEGRATED";

		// Token: 0x04000CE9 RID: 3305
		internal const string Intermediate = "INTERMEDIATE";

		// Token: 0x04000CEA RID: 3306
		internal const string Insensitive = "INSENSITIVE";

		// Token: 0x04000CEB RID: 3307
		internal const string IRowset = "IROWSET";

		// Token: 0x04000CEC RID: 3308
		internal const string Isolation = "ISOLATION";

		// Token: 0x04000CED RID: 3309
		internal const string Job = "JOB";

		// Token: 0x04000CEE RID: 3310
		internal const string Keep = "KEEP";

		// Token: 0x04000CEF RID: 3311
		internal const string KeepDefaults = "KEEPDEFAULTS";

		// Token: 0x04000CF0 RID: 3312
		internal const string KeepFixed = "KEEPFIXED";

		// Token: 0x04000CF1 RID: 3313
		internal const string KeepIdentity = "KEEPIDENTITY";

		// Token: 0x04000CF2 RID: 3314
		internal const string KeepNulls = "KEEPNULLS";

		// Token: 0x04000CF3 RID: 3315
		internal const string KeepReplication = "KEEP_REPLICATION";

		// Token: 0x04000CF4 RID: 3316
		internal const string Kerberos = "KERBEROS";

		// Token: 0x04000CF5 RID: 3317
		internal const string Key = "KEY";

		// Token: 0x04000CF6 RID: 3318
		internal const string Keys = "KEYS";

		// Token: 0x04000CF7 RID: 3319
		internal const string Keyset = "KEYSET";

		// Token: 0x04000CF8 RID: 3320
		internal const string KeySource = "KEY_SOURCE";

		// Token: 0x04000CF9 RID: 3321
		internal const string KilobytesPerBatch = "KILOBYTES_PER_BATCH";

		// Token: 0x04000CFA RID: 3322
		internal const string LabelOnly = "LABELONLY";

		// Token: 0x04000CFB RID: 3323
		internal const string Language = "LANGUAGE";

		// Token: 0x04000CFC RID: 3324
		internal const string Last = "LAST";

		// Token: 0x04000CFD RID: 3325
		internal const string LastRow = "LASTROW";

		// Token: 0x04000CFE RID: 3326
		internal const string Level = "LEVEL";

		// Token: 0x04000CFF RID: 3327
		internal const string Level1 = "LEVEL_1";

		// Token: 0x04000D00 RID: 3328
		internal const string Level2 = "LEVEL_2";

		// Token: 0x04000D01 RID: 3329
		internal const string Level3 = "LEVEL_3";

		// Token: 0x04000D02 RID: 3330
		internal const string Level4 = "LEVEL_4";

		// Token: 0x04000D03 RID: 3331
		internal const string LifeTime = "LIFETIME";

		// Token: 0x04000D04 RID: 3332
		internal const string List = "LIST";

		// Token: 0x04000D05 RID: 3333
		internal const string ListenerIP = "LISTENER_IP";

		// Token: 0x04000D06 RID: 3334
		internal const string ListenerPort = "LISTENER_PORT";

		// Token: 0x04000D07 RID: 3335
		internal const string Load = "LOAD";

		// Token: 0x04000D08 RID: 3336
		internal const string LoadHistory = "LOADHISTORY";

		// Token: 0x04000D09 RID: 3337
		internal const string LobCompaction = "LOB_COMPACTION";

		// Token: 0x04000D0A RID: 3338
		internal const string Local = "LOCAL";

		// Token: 0x04000D0B RID: 3339
		internal const string LocalServiceName = "LOCAL_SERVICE_NAME";

		// Token: 0x04000D0C RID: 3340
		internal const string LockTimeout = "LOCK_TIMEOUT";

		// Token: 0x04000D0D RID: 3341
		internal const string Log = "LOG";

		// Token: 0x04000D0E RID: 3342
		internal const string Login = "LOGIN";

		// Token: 0x04000D0F RID: 3343
		internal const string LoginType = "LOGIN_TYPE";

		// Token: 0x04000D10 RID: 3344
		internal const string Logon = "LOGON";

		// Token: 0x04000D11 RID: 3345
		internal const string Loop = "LOOP";

		// Token: 0x04000D12 RID: 3346
		internal const string Low = "LOW";

		// Token: 0x04000D13 RID: 3347
		internal const string LSquareParen = "[";

		// Token: 0x04000D14 RID: 3348
		internal const string Manual = "MANUAL";

		// Token: 0x04000D15 RID: 3349
		internal const string Mark = "MARK";

		// Token: 0x04000D16 RID: 3350
		internal const string MarkInUseForRemoval = "MARK_IN_USE_FOR_REMOVAL";

		// Token: 0x04000D17 RID: 3351
		internal const string Master = "MASTER";

		// Token: 0x04000D18 RID: 3352
		internal const string Matched = "MATCHED";

		// Token: 0x04000D19 RID: 3353
		internal const string Max = "MAX";

		// Token: 0x04000D1A RID: 3354
		internal const string MaxCpuPercent = "MAX_CPU_PERCENT";

		// Token: 0x04000D1B RID: 3355
		internal const string MaxDispatchLatency = "MAX_DISPATCH_LATENCY";

		// Token: 0x04000D1C RID: 3356
		internal const string MaxDop = "MAXDOP";

		// Token: 0x04000D1D RID: 3357
		internal const string Max_Dop = "MAX_DOP";

		// Token: 0x04000D1E RID: 3358
		internal const string MaxErrors = "MAXERRORS";

		// Token: 0x04000D1F RID: 3359
		internal const string MaxEventSize = "MAX_EVENT_SIZE";

		// Token: 0x04000D20 RID: 3360
		internal const string MaxFiles = "MAX_FILES";

		// Token: 0x04000D21 RID: 3361
		internal const string MaxIoPercent = "MAX_IO_PERCENT";

		// Token: 0x04000D22 RID: 3362
		internal const string MaxMemory = "MAX_MEMORY";

		// Token: 0x04000D23 RID: 3363
		internal const string MaxMemoryPercent = "MAX_MEMORY_PERCENT";

		// Token: 0x04000D24 RID: 3364
		internal const string MaxQueueReaders = "MAX_QUEUE_READERS";

		// Token: 0x04000D25 RID: 3365
		internal const string MaxRecursion = "MAXRECURSION";

		// Token: 0x04000D26 RID: 3366
		internal const string MaxRolloverFiles = "MAX_ROLLOVER_FILES";

		// Token: 0x04000D27 RID: 3367
		internal const string MaxSize = "MAXSIZE";

		// Token: 0x04000D28 RID: 3368
		internal const string MaxTransferSize = "MAXTRANSFERSIZE";

		// Token: 0x04000D29 RID: 3369
		internal const string MaxValue = "MAXVALUE";

		// Token: 0x04000D2A RID: 3370
		internal const string MediaDescription = "MEDIADESCRIPTION";

		// Token: 0x04000D2B RID: 3371
		internal const string MediaName = "MEDIANAME";

		// Token: 0x04000D2C RID: 3372
		internal const string MediaPassword = "MEDIAPASSWORD";

		// Token: 0x04000D2D RID: 3373
		internal const string Medium = "MEDIUM";

		// Token: 0x04000D2E RID: 3374
		internal const string Member = "MEMBER";

		// Token: 0x04000D2F RID: 3375
		internal const string MemoryPartitionMode = "MEMORY_PARTITION_MODE";

		// Token: 0x04000D30 RID: 3376
		internal const string Merge = "MERGE";

		// Token: 0x04000D31 RID: 3377
		internal const string Message = "MESSAGE";

		// Token: 0x04000D32 RID: 3378
		internal const string MessageForwarding = "MESSAGE_FORWARDING";

		// Token: 0x04000D33 RID: 3379
		internal const string MessageForwardSize = "MESSAGE_FORWARD_SIZE";

		// Token: 0x04000D34 RID: 3380
		internal const string Min = "MIN";

		// Token: 0x04000D35 RID: 3381
		internal const string MinCpuPercent = "MIN_CPU_PERCENT";

		// Token: 0x04000D36 RID: 3382
		internal const string MinIoPercent = "MIN_IO_PERCENT";

		// Token: 0x04000D37 RID: 3383
		internal const string MinMemoryPercent = "MIN_MEMORY_PERCENT";

		// Token: 0x04000D38 RID: 3384
		internal const string MinValue = "MINVALUE";

		// Token: 0x04000D39 RID: 3385
		internal const string MirrorAddress = "MIRROR_ADDRESS";

		// Token: 0x04000D3A RID: 3386
		internal const string Mirror = "MIRROR";

		// Token: 0x04000D3B RID: 3387
		internal const string Mixed = "MIXED";

		// Token: 0x04000D3C RID: 3388
		internal const string Modify = "MODIFY";

		// Token: 0x04000D3D RID: 3389
		internal const string Money = "MONEY";

		// Token: 0x04000D3E RID: 3390
		internal const string Move = "MOVE";

		// Token: 0x04000D3F RID: 3391
		internal const string MultiUser = "MULTI_USER";

		// Token: 0x04000D40 RID: 3392
		internal const string MustChange = "MUST_CHANGE";

		// Token: 0x04000D41 RID: 3393
		internal const string Name = "NAME";

		// Token: 0x04000D42 RID: 3394
		internal const string Namespace = "NAMESPACE";

		// Token: 0x04000D43 RID: 3395
		internal const string Native = "NATIVE";

		// Token: 0x04000D44 RID: 3396
		internal const string NChar = "NCHAR";

		// Token: 0x04000D45 RID: 3397
		internal const string Negotiate = "NEGOTIATE";

		// Token: 0x04000D46 RID: 3398
		internal const string Never = "NEVER";

		// Token: 0x04000D47 RID: 3399
		internal const string NestedTriggers = "NESTED_TRIGGERS";

		// Token: 0x04000D48 RID: 3400
		internal const string NewAccount = "NEW_ACCOUNT";

		// Token: 0x04000D49 RID: 3401
		internal const string NewName = "NEWNAME";

		// Token: 0x04000D4A RID: 3402
		internal const string NewBroker = "NEW_BROKER";

		// Token: 0x04000D4B RID: 3403
		internal const string NewPassword = "NEW_PASSWORD";

		// Token: 0x04000D4C RID: 3404
		internal const string Next = "NEXT";

		// Token: 0x04000D4D RID: 3405
		internal const string No = "NO";

		// Token: 0x04000D4E RID: 3406
		internal const string NoChecksum = "NO_CHECKSUM";

		// Token: 0x04000D4F RID: 3407
		internal const string NoEventLoss = "NO_EVENT_LOSS";

		// Token: 0x04000D50 RID: 3408
		internal const string NoExpand = "NOEXPAND";

		// Token: 0x04000D51 RID: 3409
		internal const string NoFormat = "NOFORMAT";

		// Token: 0x04000D52 RID: 3410
		internal const string NoInfoMessages = "NO_INFOMSGS";

		// Token: 0x04000D53 RID: 3411
		internal const string NoInit = "NOINIT";

		// Token: 0x04000D54 RID: 3412
		internal const string NoLock = "NOLOCK";

		// Token: 0x04000D55 RID: 3413
		internal const string NoLog = "NO_LOG";

		// Token: 0x04000D56 RID: 3414
		internal const string NoBrowsetable = "NO_BROWSETABLE";

		// Token: 0x04000D57 RID: 3415
		internal const string NonTransactedAccess = "NON_TRANSACTED_ACCESS";

		// Token: 0x04000D58 RID: 3416
		internal const string NoRecompute = "NORECOMPUTE";

		// Token: 0x04000D59 RID: 3417
		internal const string NoRecovery = "NORECOVERY";

		// Token: 0x04000D5A RID: 3418
		internal const string NoReset = "NORESET";

		// Token: 0x04000D5B RID: 3419
		internal const string NoRewind = "NOREWIND";

		// Token: 0x04000D5C RID: 3420
		internal const string None = "NONE";

		// Token: 0x04000D5D RID: 3421
		internal const string NoSkip = "NOSKIP";

		// Token: 0x04000D5E RID: 3422
		internal const string NoTriggers = "NO_TRIGGERS";

		// Token: 0x04000D5F RID: 3423
		internal const string NoTruncate = "NO_TRUNCATE";

		// Token: 0x04000D60 RID: 3424
		internal const string Notification = "NOTIFICATION";

		// Token: 0x04000D61 RID: 3425
		internal const string NoWait = "NOWAIT";

		// Token: 0x04000D62 RID: 3426
		internal const string NoUnload = "NOUNLOAD";

		// Token: 0x04000D63 RID: 3427
		internal const string NoWaitAlterDb = "NO_WAIT";

		// Token: 0x04000D64 RID: 3428
		internal const string NText = "NTEXT";

		// Token: 0x04000D65 RID: 3429
		internal const string Ntlm = "NTLM";

		// Token: 0x04000D66 RID: 3430
		internal const string NumaNode = "NUMANODE";

		// Token: 0x04000D67 RID: 3431
		internal const string Numeric = "NUMERIC";

		// Token: 0x04000D68 RID: 3432
		internal const string NVarChar = "NVARCHAR";

		// Token: 0x04000D69 RID: 3433
		internal const string Object = "OBJECT";

		// Token: 0x04000D6A RID: 3434
		internal const string Off = "OFF";

		// Token: 0x04000D6B RID: 3435
		internal const string Offline = "OFFLINE";

		// Token: 0x04000D6C RID: 3436
		internal const string Offset = "OFFSET";

		// Token: 0x04000D6D RID: 3437
		internal const string Oj = "OJ";

		// Token: 0x04000D6E RID: 3438
		internal const string OldAccount = "OLD_ACCOUNT";

		// Token: 0x04000D6F RID: 3439
		internal const string OldPassword = "OLD_PASSWORD";

		// Token: 0x04000D70 RID: 3440
		internal const string OnFailure = "ON_FAILURE";

		// Token: 0x04000D71 RID: 3441
		internal const string Online = "ONLINE";

		// Token: 0x04000D72 RID: 3442
		internal const string Only = "ONLY";

		// Token: 0x04000D73 RID: 3443
		internal const string OpenExisting = "OPEN_EXISTING";

		// Token: 0x04000D74 RID: 3444
		internal const string Optimistic = "OPTIMISTIC";

		// Token: 0x04000D75 RID: 3445
		internal const string Optimize = "OPTIMIZE";

		// Token: 0x04000D76 RID: 3446
		internal const string OptimizerQueue = "OPTIMIZER_QUEUE";

		// Token: 0x04000D77 RID: 3447
		internal const string Out = "OUT";

		// Token: 0x04000D78 RID: 3448
		internal const string Output = "OUTPUT";

		// Token: 0x04000D79 RID: 3449
		internal const string Override = "OVERRIDE";

		// Token: 0x04000D7A RID: 3450
		internal const string Owner = "OWNER";

		// Token: 0x04000D7B RID: 3451
		internal const string PadIndex = "PAD_INDEX";

		// Token: 0x04000D7C RID: 3452
		internal const string Page = "PAGE";

		// Token: 0x04000D7D RID: 3453
		internal const string PageCount = "PAGECOUNT";

		// Token: 0x04000D7E RID: 3454
		internal const string PageVerify = "PAGE_VERIFY";

		// Token: 0x04000D7F RID: 3455
		internal const string PagLock = "PAGLOCK";

		// Token: 0x04000D80 RID: 3456
		internal const string Param = "PARAM";

		// Token: 0x04000D81 RID: 3457
		internal const string Parameter = "PARAMETER";

		// Token: 0x04000D82 RID: 3458
		internal const string Parameterization = "PARAMETERIZATION";

		// Token: 0x04000D83 RID: 3459
		internal const string Parse = "PARSE";

		// Token: 0x04000D84 RID: 3460
		internal const string Partition = "PARTITION";

		// Token: 0x04000D85 RID: 3461
		internal const string Partitions = "PARTITIONS";

		// Token: 0x04000D86 RID: 3462
		internal const string Partner = "PARTNER";

		// Token: 0x04000D87 RID: 3463
		internal const string Password = "PASSWORD";

		// Token: 0x04000D88 RID: 3464
		internal const string Path = "PATH";

		// Token: 0x04000D89 RID: 3465
		internal const string Partial = "PARTIAL";

		// Token: 0x04000D8A RID: 3466
		internal const string Pause = "PAUSE";

		// Token: 0x04000D8B RID: 3467
		internal const string PerCpu = "PER_CPU";

		// Token: 0x04000D8C RID: 3468
		internal const string PermissionSet = "PERMISSION_SET";

		// Token: 0x04000D8D RID: 3469
		internal const string PerNode = "PER_NODE";

		// Token: 0x04000D8E RID: 3470
		internal const string Persisted = "PERSISTED";

		// Token: 0x04000D8F RID: 3471
		internal const string PhysicalOnly = "PHYSICAL_ONLY";

		// Token: 0x04000D90 RID: 3472
		internal const string PhysName = "PHYSNAME";

		// Token: 0x04000D91 RID: 3473
		internal const string Pivot = "PIVOT";

		// Token: 0x04000D92 RID: 3474
		internal const string PoisonMessageHandling = "POISON_MESSAGE_HANDLING";

		// Token: 0x04000D93 RID: 3475
		internal const string Pool = "POOL";

		// Token: 0x04000D94 RID: 3476
		internal const string Population = "POPULATION";

		// Token: 0x04000D95 RID: 3477
		internal const string Ports = "PORTS";

		// Token: 0x04000D96 RID: 3478
		internal const string Precision = "PRECISION";

		// Token: 0x04000D97 RID: 3479
		internal const string PrimaryRole = "PRIMARY_ROLE";

		// Token: 0x04000D98 RID: 3480
		internal const string Prior = "PRIOR";

		// Token: 0x04000D99 RID: 3481
		internal const string Priority = "PRIORITY";

		// Token: 0x04000D9A RID: 3482
		internal const string PriorityLevel = "PRIORITY_LEVEL";

		// Token: 0x04000D9B RID: 3483
		internal const string Private = "PRIVATE";

		// Token: 0x04000D9C RID: 3484
		internal const string Privileges = "PRIVILEGES";

		// Token: 0x04000D9D RID: 3485
		internal const string Process = "PROCESS";

		// Token: 0x04000D9E RID: 3486
		internal const string PropertySetGuid = "PROPERTY_SET_GUID";

		// Token: 0x04000D9F RID: 3487
		internal const string PropertyIntId = "PROPERTY_INT_ID";

		// Token: 0x04000DA0 RID: 3488
		internal const string PropertyDescription = "PROPERTY_DESCRIPTION";

		// Token: 0x04000DA1 RID: 3489
		internal const string Provider = "PROVIDER";

		// Token: 0x04000DA2 RID: 3490
		internal const string ProviderKeyName = "PROVIDER_KEY_NAME";

		// Token: 0x04000DA3 RID: 3491
		internal const string Procedure = "PROCEDURE";

		// Token: 0x04000DA4 RID: 3492
		internal const string ProcedureName = "PROCEDURE_NAME";

		// Token: 0x04000DA5 RID: 3493
		internal const string Property = "PROPERTY";

		// Token: 0x04000DA6 RID: 3494
		internal const string Queue = "QUEUE";

		// Token: 0x04000DA7 RID: 3495
		internal const string QueueDelay = "QUEUE_DELAY";

		// Token: 0x04000DA8 RID: 3496
		internal const string Query = "QUERY";

		// Token: 0x04000DA9 RID: 3497
		internal const string QueryGovernorCostLimit = "QUERY_GOVERNOR_COST_LIMIT";

		// Token: 0x04000DAA RID: 3498
		internal const string QueryTraceOn = "QUERYTRACEON";

		// Token: 0x04000DAB RID: 3499
		internal const string Range = "RANGE";

		// Token: 0x04000DAC RID: 3500
		internal const string Raw = "RAW";

		// Token: 0x04000DAD RID: 3501
		internal const string ReadCommitted = "READCOMMITTED";

		// Token: 0x04000DAE RID: 3502
		internal const string ReadCommittedLock = "READCOMMITTEDLOCK";

		// Token: 0x04000DAF RID: 3503
		internal const string ReadCommittedSnapshot = "READ_COMMITTED_SNAPSHOT";

		// Token: 0x04000DB0 RID: 3504
		internal const string ReadPast = "READPAST";

		// Token: 0x04000DB1 RID: 3505
		internal const string ReadOnlyOld = "READONLY";

		// Token: 0x04000DB2 RID: 3506
		internal const string ReadOnly = "READ_ONLY";

		// Token: 0x04000DB3 RID: 3507
		internal const string ReadUncommitted = "READUNCOMMITTED";

		// Token: 0x04000DB4 RID: 3508
		internal const string ReadWrite = "READ_WRITE";

		// Token: 0x04000DB5 RID: 3509
		internal const string ReadWriteFilegroups = "READ_WRITE_FILEGROUPS";

		// Token: 0x04000DB6 RID: 3510
		internal const string ReadWriteOld = "READWRITE";

		// Token: 0x04000DB7 RID: 3511
		internal const string Real = "REAL";

		// Token: 0x04000DB8 RID: 3512
		internal const string Rebuild = "REBUILD";

		// Token: 0x04000DB9 RID: 3513
		internal const string Receive = "RECEIVE";

		// Token: 0x04000DBA RID: 3514
		internal const string Recompile = "RECOMPILE";

		// Token: 0x04000DBB RID: 3515
		internal const string RecursiveTriggers = "RECURSIVE_TRIGGERS";

		// Token: 0x04000DBC RID: 3516
		internal const string Recovery = "RECOVERY";

		// Token: 0x04000DBD RID: 3517
		internal const string Regenerate = "REGENERATE";

		// Token: 0x04000DBE RID: 3518
		internal const string RelatedConversation = "RELATED_CONVERSATION";

		// Token: 0x04000DBF RID: 3519
		internal const string RelatedConversationGroup = "RELATED_CONVERSATION_GROUP";

		// Token: 0x04000DC0 RID: 3520
		internal const string Relative = "RELATIVE";

		// Token: 0x04000DC1 RID: 3521
		internal const string Remote = "REMOTE";

		// Token: 0x04000DC2 RID: 3522
		internal const string RemoteServiceName = "REMOTE_SERVICE_NAME";

		// Token: 0x04000DC3 RID: 3523
		internal const string Remove = "REMOVE";

		// Token: 0x04000DC4 RID: 3524
		internal const string Reorganize = "REORGANIZE";

		// Token: 0x04000DC5 RID: 3525
		internal const string Repeatable = "REPEATABLE";

		// Token: 0x04000DC6 RID: 3526
		internal const string RepeatableRead = "REPEATABLEREAD";

		// Token: 0x04000DC7 RID: 3527
		internal const string Replace = "REPLACE";

		// Token: 0x04000DC8 RID: 3528
		internal const string Replica = "REPLICA";

		// Token: 0x04000DC9 RID: 3529
		internal const string Required = "REQUIRED";

		// Token: 0x04000DCA RID: 3530
		internal const string ReserveDiskSpace = "RESERVE_DISK_SPACE";

		// Token: 0x04000DCB RID: 3531
		internal const string Reset = "RESET";

		// Token: 0x04000DCC RID: 3532
		internal const string Resize = "RESIZE";

		// Token: 0x04000DCD RID: 3533
		internal const string Resource = "RESOURCE";

		// Token: 0x04000DCE RID: 3534
		internal const string RestrictedUser = "RESTRICTED_USER";

		// Token: 0x04000DCF RID: 3535
		internal const string Resume = "RESUME";

		// Token: 0x04000DD0 RID: 3536
		internal const string Result = "RESULT";

		// Token: 0x04000DD1 RID: 3537
		internal const string RetainDays = "RETAINDAYS";

		// Token: 0x04000DD2 RID: 3538
		internal const string Retention = "RETENTION";

		// Token: 0x04000DD3 RID: 3539
		internal const string Returns = "RETURNS";

		// Token: 0x04000DD4 RID: 3540
		internal const string RequestMaxCpuTimeSec = "REQUEST_MAX_CPU_TIME_SEC";

		// Token: 0x04000DD5 RID: 3541
		internal const string RequestMaxMemoryGrantPercent = "REQUEST_MAX_MEMORY_GRANT_PERCENT";

		// Token: 0x04000DD6 RID: 3542
		internal const string RequestMemoryGrantTimeoutSec = "REQUEST_MEMORY_GRANT_TIMEOUT_SEC";

		// Token: 0x04000DD7 RID: 3543
		internal const string RequiredCopiesToCommit = "REQUIRED_COPIES_TO_COMMIT";

		// Token: 0x04000DD8 RID: 3544
		internal const string Resample = "RESAMPLE";

		// Token: 0x04000DD9 RID: 3545
		internal const string Revert = "REVERT";

		// Token: 0x04000DDA RID: 3546
		internal const string Restart = "RESTART";

		// Token: 0x04000DDB RID: 3547
		internal const string Rewind = "REWIND";

		// Token: 0x04000DDC RID: 3548
		internal const string RewindOnly = "REWINDONLY";

		// Token: 0x04000DDD RID: 3549
		internal const string Robust = "ROBUST";

		// Token: 0x04000DDE RID: 3550
		internal const string Role = "ROLE";

		// Token: 0x04000DDF RID: 3551
		internal const string Rollup = "ROLLUP";

		// Token: 0x04000DE0 RID: 3552
		internal const string Root = "ROOT";

		// Token: 0x04000DE1 RID: 3553
		internal const string Route = "ROUTE";

		// Token: 0x04000DE2 RID: 3554
		internal const string Row = "ROW";

		// Token: 0x04000DE3 RID: 3555
		internal const string Rowguid = "ROWGUID";

		// Token: 0x04000DE4 RID: 3556
		internal const string RowLock = "ROWLOCK";

		// Token: 0x04000DE5 RID: 3557
		internal const string Rows = "ROWS";

		// Token: 0x04000DE6 RID: 3558
		internal const string RowsetsOnly = "ROWSETS_ONLY";

		// Token: 0x04000DE7 RID: 3559
		internal const string RowsPerBatch = "ROWS_PER_BATCH";

		// Token: 0x04000DE8 RID: 3560
		internal const string RowTerminator = "ROWTERMINATOR";

		// Token: 0x04000DE9 RID: 3561
		internal const string Rowversion = "ROWVERSION";

		// Token: 0x04000DEA RID: 3562
		internal const string RSquareParen = "]";

		// Token: 0x04000DEB RID: 3563
		internal const string Rule = "RULE";

		// Token: 0x04000DEC RID: 3564
		internal const string Safe = "SAFE";

		// Token: 0x04000DED RID: 3565
		internal const string Safety = "SAFETY";

		// Token: 0x04000DEE RID: 3566
		internal const string Sample = "SAMPLE";

		// Token: 0x04000DEF RID: 3567
		internal const string Scheduler = "SCHEDULER";

		// Token: 0x04000DF0 RID: 3568
		internal const string SchemaBinding = "SCHEMABINDING";

		// Token: 0x04000DF1 RID: 3569
		internal const string Schema = "SCHEMA";

		// Token: 0x04000DF2 RID: 3570
		internal const string Scheme = "SCHEME";

		// Token: 0x04000DF3 RID: 3571
		internal const string Scroll = "SCROLL";

		// Token: 0x04000DF4 RID: 3572
		internal const string ScrollLocks = "SCROLL_LOCKS";

		// Token: 0x04000DF5 RID: 3573
		internal const string Search = "SEARCH";

		// Token: 0x04000DF6 RID: 3574
		internal const string SecondaryRole = "SECONDARY_ROLE";

		// Token: 0x04000DF7 RID: 3575
		internal const string Seconds = "SECONDS";

		// Token: 0x04000DF8 RID: 3576
		internal const string Secret = "SECRET";

		// Token: 0x04000DF9 RID: 3577
		internal const string SecurityLog = "SECURITY_LOG";

		// Token: 0x04000DFA RID: 3578
		internal const string Self = "SELF";

		// Token: 0x04000DFB RID: 3579
		internal const string SemiColon = ";";

		// Token: 0x04000DFC RID: 3580
		internal const string Send = "SEND";

		// Token: 0x04000DFD RID: 3581
		internal const string Sent = "SENT";

		// Token: 0x04000DFE RID: 3582
		internal const string Sequence = "SEQUENCE";

		// Token: 0x04000DFF RID: 3583
		internal const string Serializable = "SERIALIZABLE";

		// Token: 0x04000E00 RID: 3584
		internal const string Server = "SERVER";

		// Token: 0x04000E01 RID: 3585
		internal const string Service = "SERVICE";

		// Token: 0x04000E02 RID: 3586
		internal const string ServiceBroker = "SERVICE_BROKER";

		// Token: 0x04000E03 RID: 3587
		internal const string ServiceName = "SERVICE_NAME";

		// Token: 0x04000E04 RID: 3588
		internal const string Session = "SESSION";

		// Token: 0x04000E05 RID: 3589
		internal const string Sessions = "SESSIONS";

		// Token: 0x04000E06 RID: 3590
		internal const string SessionTimeout = "SESSION_TIMEOUT";

		// Token: 0x04000E07 RID: 3591
		internal const string SetError = "SETERROR";

		// Token: 0x04000E08 RID: 3592
		internal const string Sets = "SETS";

		// Token: 0x04000E09 RID: 3593
		internal const string ShrinkDb = "SHRINKDB";

		// Token: 0x04000E0A RID: 3594
		internal const string Sid = "SID";

		// Token: 0x04000E0B RID: 3595
		internal const string Signature = "SIGNATURE";

		// Token: 0x04000E0C RID: 3596
		internal const string Simple = "SIMPLE";

		// Token: 0x04000E0D RID: 3597
		internal const string SingleBlob = "SINGLE_BLOB";

		// Token: 0x04000E0E RID: 3598
		internal const string SingleClob = "SINGLE_CLOB";

		// Token: 0x04000E0F RID: 3599
		internal const string SingleNClob = "SINGLE_NCLOB";

		// Token: 0x04000E10 RID: 3600
		internal const string SingleSpace = " ";

		// Token: 0x04000E11 RID: 3601
		internal const string SingleUser = "SINGLE_USER";

		// Token: 0x04000E12 RID: 3602
		internal const string Site = "SITE";

		// Token: 0x04000E13 RID: 3603
		internal const string Size = "SIZE";

		// Token: 0x04000E14 RID: 3604
		internal const string Skip = "SKIP";

		// Token: 0x04000E15 RID: 3605
		internal const string Soap = "SOAP";

		// Token: 0x04000E16 RID: 3606
		internal const string SortedData = "SORTED_DATA";

		// Token: 0x04000E17 RID: 3607
		internal const string SortedDataReorg = "SORTED_DATA_REORG";

		// Token: 0x04000E18 RID: 3608
		internal const string SortInTempDb = "SORT_IN_TEMPDB";

		// Token: 0x04000E19 RID: 3609
		internal const string Source = "SOURCE";

		// Token: 0x04000E1A RID: 3610
		internal const string SmallDateTime = "SMALLDATETIME";

		// Token: 0x04000E1B RID: 3611
		internal const string SmallInt = "SMALLINT";

		// Token: 0x04000E1C RID: 3612
		internal const string SmallMoney = "SMALLMONEY";

		// Token: 0x04000E1D RID: 3613
		internal const string Snapshot = "SNAPSHOT";

		// Token: 0x04000E1E RID: 3614
		internal const string SnapshotImport = "SNAPSHOT_IMPORT";

		// Token: 0x04000E1F RID: 3615
		internal const string SnapshotRestorePhase = "SNAPSHOTRESTOREPHASE";

		// Token: 0x04000E20 RID: 3616
		internal const string Spatial = "SPATIAL";

		// Token: 0x04000E21 RID: 3617
		internal const string SpatialWindowMaxCells = "SPATIAL_WINDOW_MAX_CELLS";

		// Token: 0x04000E22 RID: 3618
		internal const string Specification = "SPECIFICATION";

		// Token: 0x04000E23 RID: 3619
		internal const string Split = "SPLIT";

		// Token: 0x04000E24 RID: 3620
		internal const string Sql = "SQL";

		// Token: 0x04000E25 RID: 3621
		internal const string Ssl = "SSL";

		// Token: 0x04000E26 RID: 3622
		internal const string SslPort = "SSL_PORT";

		// Token: 0x04000E27 RID: 3623
		internal const string SupplementalLogging = "SUPPLEMENTAL_LOGGING";

		// Token: 0x04000E28 RID: 3624
		internal const string Standard = "STANDARD";

		// Token: 0x04000E29 RID: 3625
		internal const string Standby = "STANDBY";

		// Token: 0x04000E2A RID: 3626
		internal const string Start = "START";

		// Token: 0x04000E2B RID: 3627
		internal const string StartDate = "START_DATE";

		// Token: 0x04000E2C RID: 3628
		internal const string Started = "STARTED";

		// Token: 0x04000E2D RID: 3629
		internal const string StartupState = "STARTUP_STATE";

		// Token: 0x04000E2E RID: 3630
		internal const string Statement = "STATEMENT";

		// Token: 0x04000E2F RID: 3631
		internal const string State = "STATE";

		// Token: 0x04000E30 RID: 3632
		internal const string Static = "STATIC";

		// Token: 0x04000E31 RID: 3633
		internal const string Stats = "STATS";

		// Token: 0x04000E32 RID: 3634
		internal const string StatsStream = "STATS_STREAM";

		// Token: 0x04000E33 RID: 3635
		internal const string Stop = "STOP";

		// Token: 0x04000E34 RID: 3636
		internal const string StopAtMark = "STOPATMARK";

		// Token: 0x04000E35 RID: 3637
		internal const string StopBeforeMark = "STOPBEFOREMARK";

		// Token: 0x04000E36 RID: 3638
		internal const string StatHeader = "STAT_HEADER";

		// Token: 0x04000E37 RID: 3639
		internal const string StatisticalSemantics = "STATISTICAL_SEMANTICS";

		// Token: 0x04000E38 RID: 3640
		internal const string StatisticsNoRecompute = "STATISTICS_NORECOMPUTE";

		// Token: 0x04000E39 RID: 3641
		internal const string Status = "STATUS";

		// Token: 0x04000E3A RID: 3642
		internal const string StatusOnly = "STATUSONLY";

		// Token: 0x04000E3B RID: 3643
		internal const string Stdev = "STDEV";

		// Token: 0x04000E3C RID: 3644
		internal const string Stdevp = "STDEVP";

		// Token: 0x04000E3D RID: 3645
		internal const string StopAt = "STOPAT";

		// Token: 0x04000E3E RID: 3646
		internal const string StopList = "STOPLIST";

		// Token: 0x04000E3F RID: 3647
		internal const string Stopped = "STOPPED";

		// Token: 0x04000E40 RID: 3648
		internal const string StopOnError = "STOP_ON_ERROR";

		// Token: 0x04000E41 RID: 3649
		internal const string Style = "STYLE";

		// Token: 0x04000E42 RID: 3650
		internal const string Subject = "SUBJECT";

		// Token: 0x04000E43 RID: 3651
		internal const string Subscription = "SUBSCRIPTION";

		// Token: 0x04000E44 RID: 3652
		internal const string Sum = "SUM";

		// Token: 0x04000E45 RID: 3653
		internal const string Supported = "SUPPORTED";

		// Token: 0x04000E46 RID: 3654
		internal const string Suspend = "SUSPEND";

		// Token: 0x04000E47 RID: 3655
		internal const string Sql_Variant = "SQL_VARIANT";

		// Token: 0x04000E48 RID: 3656
		internal const string Switch = "SWITCH";

		// Token: 0x04000E49 RID: 3657
		internal const string Symmetric = "SYMMETRIC";

		// Token: 0x04000E4A RID: 3658
		internal const string SynchronousCommit = "SYNCHRONOUS_COMMIT";

		// Token: 0x04000E4B RID: 3659
		internal const string Synonym = "SYNONYM";

		// Token: 0x04000E4C RID: 3660
		internal const string Sys = "SYS";

		// Token: 0x04000E4D RID: 3661
		internal const string System = "SYSTEM";

		// Token: 0x04000E4E RID: 3662
		internal const string T = "T";

		// Token: 0x04000E4F RID: 3663
		internal const string Tab = "\t";

		// Token: 0x04000E50 RID: 3664
		internal const string TableResults = "TABLERESULTS";

		// Token: 0x04000E51 RID: 3665
		internal const string TableSample = "TABLESAMPLE";

		// Token: 0x04000E52 RID: 3666
		internal const string TabLock = "TABLOCK";

		// Token: 0x04000E53 RID: 3667
		internal const string TabLockX = "TABLOCKX";

		// Token: 0x04000E54 RID: 3668
		internal const string Tape = "TAPE";

		// Token: 0x04000E55 RID: 3669
		internal const string Target = "TARGET";

		// Token: 0x04000E56 RID: 3670
		internal const string TargetMemoryPercent = "TARGET_MEMORY_PERCENT";

		// Token: 0x04000E57 RID: 3671
		internal const string TargetRecoveryTime = "TARGET_RECOVERY_TIME";

		// Token: 0x04000E58 RID: 3672
		internal const string Text = "TEXT";

		// Token: 0x04000E59 RID: 3673
		internal const string TextImageOn = "TEXTIMAGE_ON";

		// Token: 0x04000E5A RID: 3674
		internal const string Throw = "THROW";

		// Token: 0x04000E5B RID: 3675
		internal const string Ties = "TIES";

		// Token: 0x04000E5C RID: 3676
		internal const string Time = "TIME";

		// Token: 0x04000E5D RID: 3677
		internal const string Timeout = "TIMEOUT";

		// Token: 0x04000E5E RID: 3678
		internal const string Timer = "TIMER";

		// Token: 0x04000E5F RID: 3679
		internal const string TimeStamp = "TIMESTAMP";

		// Token: 0x04000E60 RID: 3680
		internal const string TinyInt = "TINYINT";

		// Token: 0x04000E61 RID: 3681
		internal const string TornPageDetection = "TORN_PAGE_DETECTION";

		// Token: 0x04000E62 RID: 3682
		internal const string TrackCausality = "TRACK_CAUSALITY";

		// Token: 0x04000E63 RID: 3683
		internal const string TrackColumnsUpdated = "TRACK_COLUMNS_UPDATED";

		// Token: 0x04000E64 RID: 3684
		internal const string Transfer = "TRANSFER";

		// Token: 0x04000E65 RID: 3685
		internal const string TransformNoiseWords = "TRANSFORM_NOISE_WORDS";

		// Token: 0x04000E66 RID: 3686
		internal const string Trigger = "TRIGGER";

		// Token: 0x04000E67 RID: 3687
		internal const string TruncateOnly = "TRUNCATE_ONLY";

		// Token: 0x04000E68 RID: 3688
		internal const string Trustworthy = "TRUSTWORTHY";

		// Token: 0x04000E69 RID: 3689
		internal const string Try = "TRY";

		// Token: 0x04000E6A RID: 3690
		internal const string TryCast = "TRY_CAST";

		// Token: 0x04000E6B RID: 3691
		internal const string TryParse = "TRY_PARSE";

		// Token: 0x04000E6C RID: 3692
		internal const string TS = "TS";

		// Token: 0x04000E6D RID: 3693
		internal const string TSql = "TSQL";

		// Token: 0x04000E6E RID: 3694
		internal const string TwoDigitYearCutoff = "TWO_DIGIT_YEAR_CUTOFF";

		// Token: 0x04000E6F RID: 3695
		internal const string Type = "TYPE";

		// Token: 0x04000E70 RID: 3696
		internal const string TypeWarning = "TYPE_WARNING";

		// Token: 0x04000E71 RID: 3697
		internal const string Unchecked = "UNCHECKED";

		// Token: 0x04000E72 RID: 3698
		internal const string Uncommitted = "UNCOMMITTED";

		// Token: 0x04000E73 RID: 3699
		internal const string Undefined = "UNDEFINED";

		// Token: 0x04000E74 RID: 3700
		internal const string UniqueIdentifier = "UNIQUEIDENTIFIER";

		// Token: 0x04000E75 RID: 3701
		internal const string Unknown = "UNKNOWN";

		// Token: 0x04000E76 RID: 3702
		internal const string Unlimited = "UNLIMITED";

		// Token: 0x04000E77 RID: 3703
		internal const string Unload = "UNLOAD";

		// Token: 0x04000E78 RID: 3704
		internal const string Unlock = "UNLOCK";

		// Token: 0x04000E79 RID: 3705
		internal const string Unsafe = "UNSAFE";

		// Token: 0x04000E7A RID: 3706
		internal const string Unpivot = "UNPIVOT";

		// Token: 0x04000E7B RID: 3707
		internal const string UpdLock = "UPDLOCK";

		// Token: 0x04000E7C RID: 3708
		internal const string Used = "USED";

		// Token: 0x04000E7D RID: 3709
		internal const string UsePlan = "USEPLAN";

		// Token: 0x04000E7E RID: 3710
		internal const string User = "USER";

		// Token: 0x04000E7F RID: 3711
		internal const string Using = "USING";

		// Token: 0x04000E80 RID: 3712
		internal const string Validation = "VALIDATION";

		// Token: 0x04000E81 RID: 3713
		internal const string ValidXml = "VALID_XML";

		// Token: 0x04000E82 RID: 3714
		internal const string Value = "VALUE";

		// Token: 0x04000E83 RID: 3715
		internal const string Var = "VAR";

		// Token: 0x04000E84 RID: 3716
		internal const string VarBinary = "VARBINARY";

		// Token: 0x04000E85 RID: 3717
		internal const string VarChar = "VARCHAR";

		// Token: 0x04000E86 RID: 3718
		internal const string VardecimalStorageFormat = "VARDECIMAL_STORAGE_FORMAT";

		// Token: 0x04000E87 RID: 3719
		internal const string Varp = "VARP";

		// Token: 0x04000E88 RID: 3720
		internal const string VDevNo = "VDEVNO";

		// Token: 0x04000E89 RID: 3721
		internal const string Verbose = "VERBOSE";

		// Token: 0x04000E8A RID: 3722
		internal const string VerifyOnly = "VERIFYONLY";

		// Token: 0x04000E8B RID: 3723
		internal const string Version = "VERSION";

		// Token: 0x04000E8C RID: 3724
		internal const string Views = "VIEWS";

		// Token: 0x04000E8D RID: 3725
		internal const string ViewMetadata = "VIEW_METADATA";

		// Token: 0x04000E8E RID: 3726
		internal const string Visibility = "VISIBILITY";

		// Token: 0x04000E8F RID: 3727
		internal const string VirtualDevice = "VIRTUAL_DEVICE";

		// Token: 0x04000E90 RID: 3728
		internal const string VStart = "VSTART";

		// Token: 0x04000E91 RID: 3729
		internal const string WebMethod = "WEBMETHOD";

		// Token: 0x04000E92 RID: 3730
		internal const string WellFormedXml = "WELL_FORMED_XML";

		// Token: 0x04000E93 RID: 3731
		internal const string WideChar = "WIDECHAR";

		// Token: 0x04000E94 RID: 3732
		internal const string WideCharAnsi = "WIDECHAR_ANSI";

		// Token: 0x04000E95 RID: 3733
		internal const string WideNative = "WIDENATIVE";

		// Token: 0x04000E96 RID: 3734
		internal const string Windows = "WINDOWS";

		// Token: 0x04000E97 RID: 3735
		internal const string Without = "WITHOUT";

		// Token: 0x04000E98 RID: 3736
		internal const string Witness = "WITNESS";

		// Token: 0x04000E99 RID: 3737
		internal const string Work = "WORK";

		// Token: 0x04000E9A RID: 3738
		internal const string Workload = "WORKLOAD";

		// Token: 0x04000E9B RID: 3739
		internal const string Wsdl = "WSDL";

		// Token: 0x04000E9C RID: 3740
		internal const string XLock = "XLOCK";

		// Token: 0x04000E9D RID: 3741
		internal const string XMax = "XMAX";

		// Token: 0x04000E9E RID: 3742
		internal const string XMin = "XMIN";

		// Token: 0x04000E9F RID: 3743
		internal const string Xml = "XML";

		// Token: 0x04000EA0 RID: 3744
		internal const string XmlData = "XMLDATA";

		// Token: 0x04000EA1 RID: 3745
		internal const string XmlNamespaces = "XMLNAMESPACES";

		// Token: 0x04000EA2 RID: 3746
		internal const string XmlSchema = "XMLSCHEMA";

		// Token: 0x04000EA3 RID: 3747
		internal const string XsiNil = "XSINIL";

		// Token: 0x04000EA4 RID: 3748
		internal const string YMax = "YMAX";

		// Token: 0x04000EA5 RID: 3749
		internal const string YMin = "YMIN";

		// Token: 0x04000EA6 RID: 3750
		internal const string Unbounded = "UNBOUNDED";

		// Token: 0x04000EA7 RID: 3751
		internal const string Preceding = "PRECEDING";

		// Token: 0x04000EA8 RID: 3752
		internal const string Following = "FOLLOWING";

		// Token: 0x04000EA9 RID: 3753
		internal const string Within = "WITHIN";

		// Token: 0x04000EAA RID: 3754
		internal const string ActiveCursors = "ACTIVECURSORS";

		// Token: 0x04000EAB RID: 3755
		internal const string AddExtendedProc = "ADDEXTENDEDPROC";

		// Token: 0x04000EAC RID: 3756
		internal const string AddInstance = "ADDINSTANCE";

		// Token: 0x04000EAD RID: 3757
		internal const string Audit = "AUDIT";

		// Token: 0x04000EAE RID: 3758
		internal const string AuditEvent = "AUDITEVENT";

		// Token: 0x04000EAF RID: 3759
		internal const string AutoPilot = "AUTOPILOT";

		// Token: 0x04000EB0 RID: 3760
		internal const string Buffer = "BUFFER";

		// Token: 0x04000EB1 RID: 3761
		internal const string Bytes = "BYTES";

		// Token: 0x04000EB2 RID: 3762
		internal const string CacheProfile = "CACHEPROFILE";

		// Token: 0x04000EB3 RID: 3763
		internal const string CacheStats = "CACHESTATS";

		// Token: 0x04000EB4 RID: 3764
		internal const string CallFulltext = "CALLFULLTEXT";

		// Token: 0x04000EB5 RID: 3765
		internal const string CheckAlloc = "CHECKALLOC";

		// Token: 0x04000EB6 RID: 3766
		internal const string CheckCatalog = "CHECKCATALOG";

		// Token: 0x04000EB7 RID: 3767
		internal const string CheckDb = "CHECKDB";

		// Token: 0x04000EB8 RID: 3768
		internal const string CheckFilegroup = "CHECKFILEGROUP";

		// Token: 0x04000EB9 RID: 3769
		internal const string CheckIdent = "CHECKIDENT";

		// Token: 0x04000EBA RID: 3770
		internal const string CheckPrimaryFile = "CHECKPRIMARYFILE";

		// Token: 0x04000EBB RID: 3771
		internal const string CheckTable = "CHECKTABLE";

		// Token: 0x04000EBC RID: 3772
		internal const string CleanTable = "CLEANTABLE";

		// Token: 0x04000EBD RID: 3773
		internal const string ClearSpaceCaches = "CLEARSPACECACHES";

		// Token: 0x04000EBE RID: 3774
		internal const string CollectStats = "COLLECTSTATS";

		// Token: 0x04000EBF RID: 3775
		internal const string ConcurrencyViolation = "CONCURRENCYVIOLATION";

		// Token: 0x04000EC0 RID: 3776
		internal const string CursorStats = "CURSORSTATS";

		// Token: 0x04000EC1 RID: 3777
		internal const string DbRecover = "DBRECOVER";

		// Token: 0x04000EC2 RID: 3778
		internal const string DbReindex = "DBREINDEX";

		// Token: 0x04000EC3 RID: 3779
		internal const string DbReindexAll = "DBREINDEXALL";

		// Token: 0x04000EC4 RID: 3780
		internal const string DbRepair = "DBREPAIR";

		// Token: 0x04000EC5 RID: 3781
		internal const string DebugBreak = "DEBUGBREAK";

		// Token: 0x04000EC6 RID: 3782
		internal const string DeleteInstance = "DELETEINSTANCE";

		// Token: 0x04000EC7 RID: 3783
		internal const string DetachDb = "DETACHDB";

		// Token: 0x04000EC8 RID: 3784
		internal const string DropCleanBuffers = "DROPCLEANBUFFERS";

		// Token: 0x04000EC9 RID: 3785
		internal const string DropExtendedProc = "DROPEXTENDEDPROC";

		// Token: 0x04000ECA RID: 3786
		internal const string DumpConfig = "CONFIG";

		// Token: 0x04000ECB RID: 3787
		internal const string DumpDbInfo = "DBINFO";

		// Token: 0x04000ECC RID: 3788
		internal const string DumpDbTable = "DBTABLE";

		// Token: 0x04000ECD RID: 3789
		internal const string DumpLock = "LOCK";

		// Token: 0x04000ECE RID: 3790
		internal const string DumpLog = "LOG";

		// Token: 0x04000ECF RID: 3791
		internal const string DumpPage = "PAGE";

		// Token: 0x04000ED0 RID: 3792
		internal const string DumpResource = "RESOURCE";

		// Token: 0x04000ED1 RID: 3793
		internal const string DumpTrigger = "DUMPTRIGGER";

		// Token: 0x04000ED2 RID: 3794
		internal const string ExtentInfo = "EXTENTINFO";

		// Token: 0x04000ED3 RID: 3795
		internal const string FileHeader = "FILEHEADER";

		// Token: 0x04000ED4 RID: 3796
		internal const string FixAllocation = "FIXALLOCATION";

		// Token: 0x04000ED5 RID: 3797
		internal const string Flush = "FLUSH";

		// Token: 0x04000ED6 RID: 3798
		internal const string FlushProcInDb = "FLUSHPROCINDB";

		// Token: 0x04000ED7 RID: 3799
		internal const string ForceGhostCleanup = "FORCEGHOSTCLEANUP";

		// Token: 0x04000ED8 RID: 3800
		internal const string Free = "FREE";

		// Token: 0x04000ED9 RID: 3801
		internal const string FreeProcCache = "FREEPROCCACHE";

		// Token: 0x04000EDA RID: 3802
		internal const string FreeSessionCache = "FREESESSIONCACHE";

		// Token: 0x04000EDB RID: 3803
		internal const string FreeSystemCache = "FREESYSTEMCACHE";

		// Token: 0x04000EDC RID: 3804
		internal const string FreezeIo = "FREEZE_IO";

		// Token: 0x04000EDD RID: 3805
		internal const string Help = "HELP";

		// Token: 0x04000EDE RID: 3806
		internal const string IceCapQuery = "ICECAPQUERY";

		// Token: 0x04000EDF RID: 3807
		internal const string IncrementInstance = "INCREMENTINSTANCE";

		// Token: 0x04000EE0 RID: 3808
		internal const string Ind = "IND";

		// Token: 0x04000EE1 RID: 3809
		internal const string IndexDefrag = "INDEXDEFRAG";

		// Token: 0x04000EE2 RID: 3810
		internal const string InputBuffer = "INPUTBUFFER";

		// Token: 0x04000EE3 RID: 3811
		internal const string InvalidateTextptr = "INVALIDATE_TEXTPTR";

		// Token: 0x04000EE4 RID: 3812
		internal const string InvalidateTextptrObjid = "INVALIDATE_TEXTPTR_OBJID";

		// Token: 0x04000EE5 RID: 3813
		internal const string Latch = "LATCH";

		// Token: 0x04000EE6 RID: 3814
		internal const string LogInfo = "LOGINFO";

		// Token: 0x04000EE7 RID: 3815
		internal const string MapAllocUnit = "MAPALLOCUNIT";

		// Token: 0x04000EE8 RID: 3816
		internal const string MemObjList = "MEMOBJLIST";

		// Token: 0x04000EE9 RID: 3817
		internal const string MemoryMap = "MEMORYMAP";

		// Token: 0x04000EEA RID: 3818
		internal const string MemoryStatus = "MEMORYSTATUS";

		// Token: 0x04000EEB RID: 3819
		internal const string Metadata = "METADATA";

		// Token: 0x04000EEC RID: 3820
		internal const string MovePage = "MOVEPAGE";

		// Token: 0x04000EED RID: 3821
		internal const string NoTextptr = "NO_TEXTPTR";

		// Token: 0x04000EEE RID: 3822
		internal const string OpenTran = "OPENTRAN";

		// Token: 0x04000EEF RID: 3823
		internal const string OptimizerWhatIf = "OPTIMIZER_WHATIF";

		// Token: 0x04000EF0 RID: 3824
		internal const string OutputBuffer = "OUTPUTBUFFER";

		// Token: 0x04000EF1 RID: 3825
		internal const string PerfMonStats = "PERFMON";

		// Token: 0x04000EF2 RID: 3826
		internal const string PersistStackHash = "PERSISTSTACKHASH";

		// Token: 0x04000EF3 RID: 3827
		internal const string PinTable = "PINTABLE";

		// Token: 0x04000EF4 RID: 3828
		internal const string ProcCache = "PROCCACHE";

		// Token: 0x04000EF5 RID: 3829
		internal const string PrtiPage = "PRTIPAGE";

		// Token: 0x04000EF6 RID: 3830
		internal const string ReadPage = "READPAGE";

		// Token: 0x04000EF7 RID: 3831
		internal const string RenameColumn = "RENAMECOLUMN";

		// Token: 0x04000EF8 RID: 3832
		internal const string RuleOff = "RULEOFF";

		// Token: 0x04000EF9 RID: 3833
		internal const string RuleOn = "RULEON";

		// Token: 0x04000EFA RID: 3834
		internal const string SeMetadata = "SEMETADATA";

		// Token: 0x04000EFB RID: 3835
		internal const string SetCpuWeight = "SETCPUWEIGHT";

		// Token: 0x04000EFC RID: 3836
		internal const string SetInstance = "SETINSTANCE";

		// Token: 0x04000EFD RID: 3837
		internal const string SetIoWeight = "SETIOWEIGHT";

		// Token: 0x04000EFE RID: 3838
		internal const string ShowStatistics = "SHOW_STATISTICS";

		// Token: 0x04000EFF RID: 3839
		internal const string ShowContig = "SHOWCONTIG";

		// Token: 0x04000F00 RID: 3840
		internal const string ShowDbAffinity = "SHOWDBAFFINITY";

		// Token: 0x04000F01 RID: 3841
		internal const string ShowFileStats = "SHOWFILESTATS";

		// Token: 0x04000F02 RID: 3842
		internal const string ShowOffRules = "SHOWOFFRULES";

		// Token: 0x04000F03 RID: 3843
		internal const string ShowOnRules = "SHOWONRULES";

		// Token: 0x04000F04 RID: 3844
		internal const string ShowTableAffinity = "SHOWTABLEAFFINITY";

		// Token: 0x04000F05 RID: 3845
		internal const string ShowText = "SHOWTEXT";

		// Token: 0x04000F06 RID: 3846
		internal const string ShowWeights = "SHOWWEIGHTS";

		// Token: 0x04000F07 RID: 3847
		internal const string ShrinkDatabase = "SHRINKDATABASE";

		// Token: 0x04000F08 RID: 3848
		internal const string ShrinkFile = "SHRINKFILE";

		// Token: 0x04000F09 RID: 3849
		internal const string Sparse = "SPARSE";

		// Token: 0x04000F0A RID: 3850
		internal const string SqlMgrStats = "SQLMGRSTATS";

		// Token: 0x04000F0B RID: 3851
		internal const string SqlPerf = "SQLPERF";

		// Token: 0x04000F0C RID: 3852
		internal const string StackDump = "STACKDUMP";

		// Token: 0x04000F0D RID: 3853
		internal const string Tec = "TEC";

		// Token: 0x04000F0E RID: 3854
		internal const string ThawIo = "THAW_IO";

		// Token: 0x04000F0F RID: 3855
		internal const string TraceOff = "TRACEOFF";

		// Token: 0x04000F10 RID: 3856
		internal const string TraceOn = "TRACEON";

		// Token: 0x04000F11 RID: 3857
		internal const string TraceStatus = "TRACESTATUS";

		// Token: 0x04000F12 RID: 3858
		internal const string UnpinTable = "UNPINTABLE";

		// Token: 0x04000F13 RID: 3859
		internal const string UpdateUsage = "UPDATEUSAGE";

		// Token: 0x04000F14 RID: 3860
		internal const string UserOptions = "USEROPTIONS";

		// Token: 0x04000F15 RID: 3861
		internal const string WritePage = "WRITEPAGE";

		// Token: 0x04000F16 RID: 3862
		internal const string ChineseMacauSar = "CHINESE (MACAU SAR)";

		// Token: 0x04000F17 RID: 3863
		internal const string ChineseSingapore = "CHINESE (SINGAPORE)";

		// Token: 0x04000F18 RID: 3864
		internal const string SerbianCyrillic = "SERBIAN (CYRILLIC)";

		// Token: 0x04000F19 RID: 3865
		internal const string Spanish = "SPANISH";

		// Token: 0x04000F1A RID: 3866
		internal const string ChineseHongKong = "CHINESE (HONG KONG SAR, PRC)";

		// Token: 0x04000F1B RID: 3867
		internal const string SerbianLatin = "SERBIAN (LATIN)";

		// Token: 0x04000F1C RID: 3868
		internal const string Portuegese = "PORTUGUESE";

		// Token: 0x04000F1D RID: 3869
		internal const string BritishEnglish = "BRITISH ENGLISH";

		// Token: 0x04000F1E RID: 3870
		internal const string SimplifiedChinese = "SIMPLIFIED CHINESE";

		// Token: 0x04000F1F RID: 3871
		internal const string Marathi = "MARATHI";

		// Token: 0x04000F20 RID: 3872
		internal const string Malayalam = "MALAYALAM";

		// Token: 0x04000F21 RID: 3873
		internal const string Kannada = "KANNADA";

		// Token: 0x04000F22 RID: 3874
		internal const string Telugu = "TELUGU";

		// Token: 0x04000F23 RID: 3875
		internal const string Tamil = "TAMIL";

		// Token: 0x04000F24 RID: 3876
		internal const string Gujarati = "GUJARATI";

		// Token: 0x04000F25 RID: 3877
		internal const string Punjabi = "PUNJABI";

		// Token: 0x04000F26 RID: 3878
		internal const string BengaliIndia = "BENGALI (INDIA)";

		// Token: 0x04000F27 RID: 3879
		internal const string MalayMalaysia = "MALAY - MALAYSIA";

		// Token: 0x04000F28 RID: 3880
		internal const string Hindi = "HINDI";

		// Token: 0x04000F29 RID: 3881
		internal const string Vietnamese = "VIETNAMESE";

		// Token: 0x04000F2A RID: 3882
		internal const string Lithuanian = "LITHUANIAN";

		// Token: 0x04000F2B RID: 3883
		internal const string Latvian = "LATVIAN";

		// Token: 0x04000F2C RID: 3884
		internal const string Slovenian = "SLOVENIAN";

		// Token: 0x04000F2D RID: 3885
		internal const string Ukrainian = "UKRAINIAN";

		// Token: 0x04000F2E RID: 3886
		internal const string Indonesian = "INDONESIAN";

		// Token: 0x04000F2F RID: 3887
		internal const string Urdu = "URDU";

		// Token: 0x04000F30 RID: 3888
		internal const string Thai = "THAI";

		// Token: 0x04000F31 RID: 3889
		internal const string Swedish = "SWEDISH";

		// Token: 0x04000F32 RID: 3890
		internal const string Slovak = "SLOVAK";

		// Token: 0x04000F33 RID: 3891
		internal const string Croatian = "CROATIAN";

		// Token: 0x04000F34 RID: 3892
		internal const string Russian = "RUSSIAN";

		// Token: 0x04000F35 RID: 3893
		internal const string Romanian = "ROMANIAN";

		// Token: 0x04000F36 RID: 3894
		internal const string Brazilian = "BRAZILIAN";

		// Token: 0x04000F37 RID: 3895
		internal const string NorwegianBokmal = "NORWEGIAN (BOKMÅL)";

		// Token: 0x04000F38 RID: 3896
		internal const string Dutch = "DUTCH";

		// Token: 0x04000F39 RID: 3897
		internal const string Korean = "KOREAN";

		// Token: 0x04000F3A RID: 3898
		internal const string Japanese = "JAPANESE";

		// Token: 0x04000F3B RID: 3899
		internal const string Italian = "ITALIAN";

		// Token: 0x04000F3C RID: 3900
		internal const string Icelandic = "ICELANDIC";

		// Token: 0x04000F3D RID: 3901
		internal const string Hebrew = "HEBREW";

		// Token: 0x04000F3E RID: 3902
		internal const string French = "FRENCH";

		// Token: 0x04000F3F RID: 3903
		internal const string English = "ENGLISH";

		// Token: 0x04000F40 RID: 3904
		internal const string German = "GERMAN";

		// Token: 0x04000F41 RID: 3905
		internal const string TraditionalChinese = "TRADITIONAL CHINESE";

		// Token: 0x04000F42 RID: 3906
		internal const string Catalan = "CATALAN";

		// Token: 0x04000F43 RID: 3907
		internal const string Bulgarian = "BULGARIAN";

		// Token: 0x04000F44 RID: 3908
		internal const string Arabic = "ARABIC";

		// Token: 0x04000F45 RID: 3909
		internal const string Neutral = "NEUTRAL";

		// Token: 0x04000F46 RID: 3910
		internal const string AddSignature = "ADD_SIGNATURE";

		// Token: 0x04000F47 RID: 3911
		internal const string AddSignatureSchemaObject = "ADD_SIGNATURE_SCHEMA_OBJECT";

		// Token: 0x04000F48 RID: 3912
		internal const string AlterAsymmetricKey = "ALTER_ASYMMETRIC_KEY";

		// Token: 0x04000F49 RID: 3913
		internal const string AlterBrokerPriority = "ALTER_BROKER_PRIORITY";

		// Token: 0x04000F4A RID: 3914
		internal const string AlterDatabaseAuditSpecification = "ALTER_DATABASE_AUDIT_SPECIFICATION";

		// Token: 0x04000F4B RID: 3915
		internal const string AlterDatabaseEncryptionKey = "ALTER_DATABASE_ENCRYPTION_KEY";

		// Token: 0x04000F4C RID: 3916
		internal const string AlterExtendedProperty = "ALTER_EXTENDED_PROPERTY";

		// Token: 0x04000F4D RID: 3917
		internal const string AlterFullTextCatalog = "ALTER_FULLTEXT_CATALOG";

		// Token: 0x04000F4E RID: 3918
		internal const string AlterFullTextIndex = "ALTER_FULLTEXT_INDEX";

		// Token: 0x04000F4F RID: 3919
		internal const string AlterFullTextStopList = "ALTER_FULLTEXT_STOPLIST";

		// Token: 0x04000F50 RID: 3920
		internal const string AlterMasterKey = "ALTER_MASTER_KEY";

		// Token: 0x04000F51 RID: 3921
		internal const string AlterPlanGuide = "ALTER_PLAN_GUIDE";

		// Token: 0x04000F52 RID: 3922
		internal const string AlterSearchPropertyList = "ALTER_SEARCH_PROPERTY_LIST";

		// Token: 0x04000F53 RID: 3923
		internal const string AlterSequence = "ALTER_SEQUENCE";

		// Token: 0x04000F54 RID: 3924
		internal const string AlterAvailabilityGroup = "ALTER_AVAILABILITY_GROUP";

		// Token: 0x04000F55 RID: 3925
		internal const string AlterServerConfiguration = "ALTER_SERVER_CONFIGURATION";

		// Token: 0x04000F56 RID: 3926
		internal const string AlterServerRole = "ALTER_SERVER_ROLE";

		// Token: 0x04000F57 RID: 3927
		internal const string AlterSymmetricKey = "ALTER_SYMMETRIC_KEY";

		// Token: 0x04000F58 RID: 3928
		internal const string BindDefault = "BIND_DEFAULT";

		// Token: 0x04000F59 RID: 3929
		internal const string BindRule = "BIND_RULE";

		// Token: 0x04000F5A RID: 3930
		internal const string CreateAsymmetricKey = "CREATE_ASYMMETRIC_KEY";

		// Token: 0x04000F5B RID: 3931
		internal const string CreateBrokerPriority = "CREATE_BROKER_PRIORITY";

		// Token: 0x04000F5C RID: 3932
		internal const string CreateDatabaseAuditSpecification = "CREATE_DATABASE_AUDIT_SPECIFICATION";

		// Token: 0x04000F5D RID: 3933
		internal const string CreateDatabaseEncryptionKey = "CREATE_DATABASE_ENCRYPTION_KEY";

		// Token: 0x04000F5E RID: 3934
		internal const string CreateDefault = "CREATE_DEFAULT";

		// Token: 0x04000F5F RID: 3935
		internal const string CreateExtendedProperty = "CREATE_EXTENDED_PROPERTY";

		// Token: 0x04000F60 RID: 3936
		internal const string CreateFullTextCatalog = "CREATE_FULLTEXT_CATALOG";

		// Token: 0x04000F61 RID: 3937
		internal const string CreateFullTextIndex = "CREATE_FULLTEXT_INDEX";

		// Token: 0x04000F62 RID: 3938
		internal const string CreateFullTextStopList = "CREATE_FULLTEXT_STOPLIST";

		// Token: 0x04000F63 RID: 3939
		internal const string CreateMasterKey = "CREATE_MASTER_KEY";

		// Token: 0x04000F64 RID: 3940
		internal const string CreatePlanGuide = "CREATE_PLAN_GUIDE";

		// Token: 0x04000F65 RID: 3941
		internal const string CreateRule = "CREATE_RULE";

		// Token: 0x04000F66 RID: 3942
		internal const string CreateSearchPropertyList = "CREATE_SEARCH_PROPERTY_LIST";

		// Token: 0x04000F67 RID: 3943
		internal const string CreateSequence = "CREATE_SEQUENCE";

		// Token: 0x04000F68 RID: 3944
		internal const string CreateAvailabilityGroup = "CREATE_AVAILABILITY_GROUP";

		// Token: 0x04000F69 RID: 3945
		internal const string CreateServerRole = "CREATE_SERVER_ROLE";

		// Token: 0x04000F6A RID: 3946
		internal const string CreateSpatialIndex = "CREATE_SPATIAL_INDEX";

		// Token: 0x04000F6B RID: 3947
		internal const string CreateSymmetricKey = "CREATE_SYMMETRIC_KEY";

		// Token: 0x04000F6C RID: 3948
		internal const string DropAsymmetricKey = "DROP_ASYMMETRIC_KEY";

		// Token: 0x04000F6D RID: 3949
		internal const string DropBrokerPriority = "DROP_BROKER_PRIORITY";

		// Token: 0x04000F6E RID: 3950
		internal const string DropDatabaseAuditSpecification = "DROP_DATABASE_AUDIT_SPECIFICATION";

		// Token: 0x04000F6F RID: 3951
		internal const string DropDatabaseEncryptionKey = "DROP_DATABASE_ENCRYPTION_KEY";

		// Token: 0x04000F70 RID: 3952
		internal const string DropDefault = "DROP_DEFAULT";

		// Token: 0x04000F71 RID: 3953
		internal const string DropExtendedProperty = "DROP_EXTENDED_PROPERTY";

		// Token: 0x04000F72 RID: 3954
		internal const string DropFullTextCatalog = "DROP_FULLTEXT_CATALOG";

		// Token: 0x04000F73 RID: 3955
		internal const string DropFullTextIndex = "DROP_FULLTEXT_INDEX";

		// Token: 0x04000F74 RID: 3956
		internal const string DropFullTextStopList = "DROP_FULLTEXT_STOPLIST";

		// Token: 0x04000F75 RID: 3957
		internal const string DropMasterKey = "DROP_MASTER_KEY";

		// Token: 0x04000F76 RID: 3958
		internal const string DropPlanGuide = "DROP_PLAN_GUIDE";

		// Token: 0x04000F77 RID: 3959
		internal const string DropRule = "DROP_RULE";

		// Token: 0x04000F78 RID: 3960
		internal const string DropSearchPropertyList = "DROP_SEARCH_PROPERTY_LIST";

		// Token: 0x04000F79 RID: 3961
		internal const string DropSequence = "DROP_SEQUENCE";

		// Token: 0x04000F7A RID: 3962
		internal const string DropAvailabilityGroup = "DROP_AVAILABILITY_GROUP";

		// Token: 0x04000F7B RID: 3963
		internal const string DropServerRole = "DROP_SERVER_ROLE";

		// Token: 0x04000F7C RID: 3964
		internal const string DropSignature = "DROP_SIGNATURE";

		// Token: 0x04000F7D RID: 3965
		internal const string DropSignatureSchemaObject = "DROP_SIGNATURE_SCHEMA_OBJECT";

		// Token: 0x04000F7E RID: 3966
		internal const string DropSymmetricKey = "DROP_SYMMETRIC_KEY";

		// Token: 0x04000F7F RID: 3967
		internal const string Rename = "RENAME";

		// Token: 0x04000F80 RID: 3968
		internal const string UnbindDefault = "UNBIND_DEFAULT";

		// Token: 0x04000F81 RID: 3969
		internal const string UnbindRule = "UNBIND_RULE";

		// Token: 0x04000F82 RID: 3970
		internal const string AlterCredential = "ALTER_CREDENTIAL";

		// Token: 0x04000F83 RID: 3971
		internal const string AlterCryptographicProvider = "ALTER_CRYPTOGRAPHIC_PROVIDER";

		// Token: 0x04000F84 RID: 3972
		internal const string AlterEventSession = "ALTER_EVENT_SESSION";

		// Token: 0x04000F85 RID: 3973
		internal const string AlterInstance = "ALTER_INSTANCE";

		// Token: 0x04000F86 RID: 3974
		internal const string AlterLinkedServer = "ALTER_LINKED_SERVER";

		// Token: 0x04000F87 RID: 3975
		internal const string AlterMessage = "ALTER_MESSAGE";

		// Token: 0x04000F88 RID: 3976
		internal const string AlterRemoteServer = "ALTER_REMOTE_SERVER";

		// Token: 0x04000F89 RID: 3977
		internal const string AlterResourceGovernorConfig = "ALTER_RESOURCE_GOVERNOR_CONFIG";

		// Token: 0x04000F8A RID: 3978
		internal const string AlterResourcePool = "ALTER_RESOURCE_POOL";

		// Token: 0x04000F8B RID: 3979
		internal const string AlterServerAudit = "ALTER_SERVER_AUDIT";

		// Token: 0x04000F8C RID: 3980
		internal const string AlterServerAuditSpecification = "ALTER_SERVER_AUDIT_SPECIFICATION";

		// Token: 0x04000F8D RID: 3981
		internal const string AlterServiceMasterKey = "ALTER_SERVICE_MASTER_KEY";

		// Token: 0x04000F8E RID: 3982
		internal const string AlterWorkloadGroup = "ALTER_WORKLOAD_GROUP";

		// Token: 0x04000F8F RID: 3983
		internal const string CreateCredential = "CREATE_CREDENTIAL";

		// Token: 0x04000F90 RID: 3984
		internal const string CreateCryptographicProvider = "CREATE_CRYPTOGRAPHIC_PROVIDER";

		// Token: 0x04000F91 RID: 3985
		internal const string CreateEventSession = "CREATE_EVENT_SESSION";

		// Token: 0x04000F92 RID: 3986
		internal const string CreateExtendedProcedure = "CREATE_EXTENDED_PROCEDURE";

		// Token: 0x04000F93 RID: 3987
		internal const string CreateLinkedServer = "CREATE_LINKED_SERVER";

		// Token: 0x04000F94 RID: 3988
		internal const string CreateLinkedServerLogin = "CREATE_LINKED_SERVER_LOGIN";

		// Token: 0x04000F95 RID: 3989
		internal const string CreateMessage = "CREATE_MESSAGE";

		// Token: 0x04000F96 RID: 3990
		internal const string CreateRemoteServer = "CREATE_REMOTE_SERVER";

		// Token: 0x04000F97 RID: 3991
		internal const string CreateResourcePool = "CREATE_RESOURCE_POOL";

		// Token: 0x04000F98 RID: 3992
		internal const string CreateServerAudit = "CREATE_SERVER_AUDIT";

		// Token: 0x04000F99 RID: 3993
		internal const string CreateServerAuditSpecification = "CREATE_SERVER_AUDIT_SPECIFICATION";

		// Token: 0x04000F9A RID: 3994
		internal const string CreateWorkloadGroup = "CREATE_WORKLOAD_GROUP";

		// Token: 0x04000F9B RID: 3995
		internal const string DropCredential = "DROP_CREDENTIAL";

		// Token: 0x04000F9C RID: 3996
		internal const string DropCryptographicProvider = "DROP_CRYPTOGRAPHIC_PROVIDER";

		// Token: 0x04000F9D RID: 3997
		internal const string DropEventSession = "DROP_EVENT_SESSION";

		// Token: 0x04000F9E RID: 3998
		internal const string DropExtendedProcedure = "DROP_EXTENDED_PROCEDURE";

		// Token: 0x04000F9F RID: 3999
		internal const string DropLinkedServer = "DROP_LINKED_SERVER";

		// Token: 0x04000FA0 RID: 4000
		internal const string DropLinkedServerLogin = "DROP_LINKED_SERVER_LOGIN";

		// Token: 0x04000FA1 RID: 4001
		internal const string DropMessage = "DROP_MESSAGE";

		// Token: 0x04000FA2 RID: 4002
		internal const string DropRemoteServer = "DROP_REMOTE_SERVER";

		// Token: 0x04000FA3 RID: 4003
		internal const string DropResourcePool = "DROP_RESOURCE_POOL";

		// Token: 0x04000FA4 RID: 4004
		internal const string DropServerAudit = "DROP_SERVER_AUDIT";

		// Token: 0x04000FA5 RID: 4005
		internal const string DropServerAuditSpecification = "DROP_SERVER_AUDIT_SPECIFICATION";

		// Token: 0x04000FA6 RID: 4006
		internal const string DropWorkloadGroup = "DROP_WORKLOAD_GROUP";

		// Token: 0x04000FA7 RID: 4007
		internal const string CreateApplicationRole = "CREATE_APPLICATION_ROLE";

		// Token: 0x04000FA8 RID: 4008
		internal const string AlterApplicationRole = "ALTER_APPLICATION_ROLE";

		// Token: 0x04000FA9 RID: 4009
		internal const string DropApplicationRole = "DROP_APPLICATION_ROLE";

		// Token: 0x04000FAA RID: 4010
		internal const string CreateAssembly = "CREATE_ASSEMBLY";

		// Token: 0x04000FAB RID: 4011
		internal const string AlterAssembly = "ALTER_ASSEMBLY";

		// Token: 0x04000FAC RID: 4012
		internal const string DropAssembly = "DROP_ASSEMBLY";

		// Token: 0x04000FAD RID: 4013
		internal const string AlterAuthorizationDatabase = "ALTER_AUTHORIZATION_DATABASE";

		// Token: 0x04000FAE RID: 4014
		internal const string CreateCertificate = "CREATE_CERTIFICATE";

		// Token: 0x04000FAF RID: 4015
		internal const string AlterCertificate = "ALTER_CERTIFICATE";

		// Token: 0x04000FB0 RID: 4016
		internal const string DropCertificate = "DROP_CERTIFICATE";

		// Token: 0x04000FB1 RID: 4017
		internal const string CreateContract = "CREATE_CONTRACT";

		// Token: 0x04000FB2 RID: 4018
		internal const string DropContract = "DROP_CONTRACT";

		// Token: 0x04000FB3 RID: 4019
		internal const string GrantDatabase = "GRANT_DATABASE";

		// Token: 0x04000FB4 RID: 4020
		internal const string DenyDatabase = "DENY_DATABASE";

		// Token: 0x04000FB5 RID: 4021
		internal const string RevokeDatabase = "REVOKE_DATABASE";

		// Token: 0x04000FB6 RID: 4022
		internal const string CreateEventNotification = "CREATE_EVENT_NOTIFICATION";

		// Token: 0x04000FB7 RID: 4023
		internal const string DropEventNotification = "DROP_EVENT_NOTIFICATION";

		// Token: 0x04000FB8 RID: 4024
		internal const string CreateFunction = "CREATE_FUNCTION";

		// Token: 0x04000FB9 RID: 4025
		internal const string AlterFunction = "ALTER_FUNCTION";

		// Token: 0x04000FBA RID: 4026
		internal const string DropFunction = "DROP_FUNCTION";

		// Token: 0x04000FBB RID: 4027
		internal const string CreateIndex = "CREATE_INDEX";

		// Token: 0x04000FBC RID: 4028
		internal const string AlterIndex = "ALTER_INDEX";

		// Token: 0x04000FBD RID: 4029
		internal const string DropIndex = "DROP_INDEX";

		// Token: 0x04000FBE RID: 4030
		internal const string CreateMessageType = "CREATE_MESSAGE_TYPE";

		// Token: 0x04000FBF RID: 4031
		internal const string AlterMessageType = "ALTER_MESSAGE_TYPE";

		// Token: 0x04000FC0 RID: 4032
		internal const string DropMessageType = "DROP_MESSAGE_TYPE";

		// Token: 0x04000FC1 RID: 4033
		internal const string CreatePartitionFunction = "CREATE_PARTITION_FUNCTION";

		// Token: 0x04000FC2 RID: 4034
		internal const string AlterPartitionFunction = "ALTER_PARTITION_FUNCTION";

		// Token: 0x04000FC3 RID: 4035
		internal const string DropPartitionFunction = "DROP_PARTITION_FUNCTION";

		// Token: 0x04000FC4 RID: 4036
		internal const string CreatePartitionScheme = "CREATE_PARTITION_SCHEME";

		// Token: 0x04000FC5 RID: 4037
		internal const string AlterPartitionScheme = "ALTER_PARTITION_SCHEME";

		// Token: 0x04000FC6 RID: 4038
		internal const string DropPartitionScheme = "DROP_PARTITION_SCHEME";

		// Token: 0x04000FC7 RID: 4039
		internal const string CreateProcedure = "CREATE_PROCEDURE";

		// Token: 0x04000FC8 RID: 4040
		internal const string AlterProcedure = "ALTER_PROCEDURE";

		// Token: 0x04000FC9 RID: 4041
		internal const string DropProcedure = "DROP_PROCEDURE";

		// Token: 0x04000FCA RID: 4042
		internal const string CreateQueue = "CREATE_QUEUE";

		// Token: 0x04000FCB RID: 4043
		internal const string AlterQueue = "ALTER_QUEUE";

		// Token: 0x04000FCC RID: 4044
		internal const string DropQueue = "DROP_QUEUE";

		// Token: 0x04000FCD RID: 4045
		internal const string CreateRemoteServiceBinding = "CREATE_REMOTE_SERVICE_BINDING";

		// Token: 0x04000FCE RID: 4046
		internal const string AlterRemoteServiceBinding = "ALTER_REMOTE_SERVICE_BINDING";

		// Token: 0x04000FCF RID: 4047
		internal const string DropRemoteServiceBinding = "DROP_REMOTE_SERVICE_BINDING";

		// Token: 0x04000FD0 RID: 4048
		internal const string CreateRole = "CREATE_ROLE";

		// Token: 0x04000FD1 RID: 4049
		internal const string AlterRole = "ALTER_ROLE";

		// Token: 0x04000FD2 RID: 4050
		internal const string DropRole = "DROP_ROLE";

		// Token: 0x04000FD3 RID: 4051
		internal const string CreateRoute = "CREATE_ROUTE";

		// Token: 0x04000FD4 RID: 4052
		internal const string AlterRoute = "ALTER_ROUTE";

		// Token: 0x04000FD5 RID: 4053
		internal const string DropRoute = "DROP_ROUTE";

		// Token: 0x04000FD6 RID: 4054
		internal const string CreateSchema = "CREATE_SCHEMA";

		// Token: 0x04000FD7 RID: 4055
		internal const string AlterSchema = "ALTER_SCHEMA";

		// Token: 0x04000FD8 RID: 4056
		internal const string DropSchema = "DROP_SCHEMA";

		// Token: 0x04000FD9 RID: 4057
		internal const string CreateService = "CREATE_SERVICE";

		// Token: 0x04000FDA RID: 4058
		internal const string AlterService = "ALTER_SERVICE";

		// Token: 0x04000FDB RID: 4059
		internal const string DropService = "DROP_SERVICE";

		// Token: 0x04000FDC RID: 4060
		internal const string CreateStatistics = "CREATE_STATISTICS";

		// Token: 0x04000FDD RID: 4061
		internal const string DropStatistics = "DROP_STATISTICS";

		// Token: 0x04000FDE RID: 4062
		internal const string UpdateStatistics = "UPDATE_STATISTICS";

		// Token: 0x04000FDF RID: 4063
		internal const string CreateSynonym = "CREATE_SYNONYM";

		// Token: 0x04000FE0 RID: 4064
		internal const string DropSynonym = "DROP_SYNONYM";

		// Token: 0x04000FE1 RID: 4065
		internal const string CreateTable = "CREATE_TABLE";

		// Token: 0x04000FE2 RID: 4066
		internal const string AlterTable = "ALTER_TABLE";

		// Token: 0x04000FE3 RID: 4067
		internal const string DropTable = "DROP_TABLE";

		// Token: 0x04000FE4 RID: 4068
		internal const string CreateTrigger = "CREATE_TRIGGER";

		// Token: 0x04000FE5 RID: 4069
		internal const string AlterTrigger = "ALTER_TRIGGER";

		// Token: 0x04000FE6 RID: 4070
		internal const string DropTrigger = "DROP_TRIGGER";

		// Token: 0x04000FE7 RID: 4071
		internal const string CreateType = "CREATE_TYPE";

		// Token: 0x04000FE8 RID: 4072
		internal const string DropType = "DROP_TYPE";

		// Token: 0x04000FE9 RID: 4073
		internal const string CreateUser = "CREATE_USER";

		// Token: 0x04000FEA RID: 4074
		internal const string AlterUser = "ALTER_USER";

		// Token: 0x04000FEB RID: 4075
		internal const string DropUser = "DROP_USER";

		// Token: 0x04000FEC RID: 4076
		internal const string CreateView = "CREATE_VIEW";

		// Token: 0x04000FED RID: 4077
		internal const string AlterView = "ALTER_VIEW";

		// Token: 0x04000FEE RID: 4078
		internal const string DropView = "DROP_VIEW";

		// Token: 0x04000FEF RID: 4079
		internal const string CreateXmlSchemaCollection = "CREATE_XML_SCHEMA_COLLECTION";

		// Token: 0x04000FF0 RID: 4080
		internal const string AlterXmlSchemaCollection = "ALTER_XML_SCHEMA_COLLECTION";

		// Token: 0x04000FF1 RID: 4081
		internal const string DropXmlSchemaCollection = "DROP_XML_SCHEMA_COLLECTION";

		// Token: 0x04000FF2 RID: 4082
		internal const string AlterAuthorizationServer = "ALTER_AUTHORIZATION_SERVER";

		// Token: 0x04000FF3 RID: 4083
		internal const string CreateDatabase = "CREATE_DATABASE";

		// Token: 0x04000FF4 RID: 4084
		internal const string AlterDatabase = "ALTER_DATABASE";

		// Token: 0x04000FF5 RID: 4085
		internal const string DropDatabase = "DROP_DATABASE";

		// Token: 0x04000FF6 RID: 4086
		internal const string CreateLogin = "CREATE_LOGIN";

		// Token: 0x04000FF7 RID: 4087
		internal const string AlterLogin = "ALTER_LOGIN";

		// Token: 0x04000FF8 RID: 4088
		internal const string CreateEndpoint = "CREATE_ENDPOINT";

		// Token: 0x04000FF9 RID: 4089
		internal const string DropEndpoint = "DROP_ENDPOINT";

		// Token: 0x04000FFA RID: 4090
		internal const string DropLogin = "DROP_LOGIN";

		// Token: 0x04000FFB RID: 4091
		internal const string GrantServer = "GRANT_SERVER";

		// Token: 0x04000FFC RID: 4092
		internal const string DenyServer = "DENY_SERVER";

		// Token: 0x04000FFD RID: 4093
		internal const string RevokeServer = "REVOKE_SERVER";

		// Token: 0x04000FFE RID: 4094
		internal const string AddRoleMember = "ADD_ROLE_MEMBER";

		// Token: 0x04000FFF RID: 4095
		internal const string AddServerRoleMember = "ADD_SERVER_ROLE_MEMBER";

		// Token: 0x04001000 RID: 4096
		internal const string DropRoleMember = "DROP_ROLE_MEMBER";

		// Token: 0x04001001 RID: 4097
		internal const string DropServerRoleMember = "DROP_SERVER_ROLE_MEMBER";

		// Token: 0x04001002 RID: 4098
		internal const string AlterEndpoint = "ALTER_ENDPOINT";

		// Token: 0x04001003 RID: 4099
		internal const string CreateXmlIndex = "CREATE_XML_INDEX";

		// Token: 0x04001004 RID: 4100
		internal const string QueueActivation = "QUEUE_ACTIVATION";

		// Token: 0x04001005 RID: 4101
		internal const string BrokerQueueDisabled = "BROKER_QUEUE_DISABLED";

		// Token: 0x04001006 RID: 4102
		internal const string AssemblyLoad = "ASSEMBLY_LOAD";

		// Token: 0x04001007 RID: 4103
		internal const string AuditAddDbUserEvent = "AUDIT_ADD_DB_USER_EVENT";

		// Token: 0x04001008 RID: 4104
		internal const string AuditAddLoginEvent = "AUDIT_ADDLOGIN_EVENT";

		// Token: 0x04001009 RID: 4105
		internal const string AuditAddLoginToServerRoleEvent = "AUDIT_ADD_LOGIN_TO_SERVER_ROLE_EVENT";

		// Token: 0x0400100A RID: 4106
		internal const string AuditAddMemberToDbRoleEvent = "AUDIT_ADD_MEMBER_TO_DB_ROLE_EVENT";

		// Token: 0x0400100B RID: 4107
		internal const string AuditAddRoleEvent = "AUDIT_ADD_ROLE_EVENT";

		// Token: 0x0400100C RID: 4108
		internal const string AuditAppRoleChangePasswordEvent = "AUDIT_APP_ROLE_CHANGE_PASSWORD_EVENT";

		// Token: 0x0400100D RID: 4109
		internal const string AuditBackupRestoreEvent = "AUDIT_BACKUP_RESTORE_EVENT";

		// Token: 0x0400100E RID: 4110
		internal const string AuditChangeAuditEvent = "AUDIT_CHANGE_AUDIT_EVENT";

		// Token: 0x0400100F RID: 4111
		internal const string AuditChangeDatabaseOwner = "AUDIT_CHANGE_DATABASE_OWNER";

		// Token: 0x04001010 RID: 4112
		internal const string AuditDatabaseManagementEvent = "AUDIT_DATABASE_MANAGEMENT_EVENT";

		// Token: 0x04001011 RID: 4113
		internal const string AuditDatabaseObjectAccessEvent = "AUDIT_DATABASE_OBJECT_ACCESS_EVENT";

		// Token: 0x04001012 RID: 4114
		internal const string AuditDatabaseObjectGdrEvent = "AUDIT_DATABASE_OBJECT_GDR_EVENT";

		// Token: 0x04001013 RID: 4115
		internal const string AuditDatabaseObjectManagementEvent = "AUDIT_DATABASE_OBJECT_MANAGEMENT_EVENT";

		// Token: 0x04001014 RID: 4116
		internal const string AuditDatabaseObjectTakeOwnershipEvent = "AUDIT_DATABASE_OBJECT_TAKE_OWNERSHIP_EVENT";

		// Token: 0x04001015 RID: 4117
		internal const string AuditDatabaseOperationEvent = "AUDIT_DATABASE_OPERATION_EVENT";

		// Token: 0x04001016 RID: 4118
		internal const string AuditDatabasePrincipalImpersonationEvent = "AUDIT_DATABASE_PRINCIPAL_IMPERSONATION_EVENT";

		// Token: 0x04001017 RID: 4119
		internal const string AuditDatabasePrincipalManagementEvent = "AUDIT_DATABASE_PRINCIPAL_MANAGEMENT_EVENT";

		// Token: 0x04001018 RID: 4120
		internal const string AuditDatabaseScopeGdrEvent = "AUDIT_DATABASE_SCOPE_GDR_EVENT";

		// Token: 0x04001019 RID: 4121
		internal const string AuditDbccEvent = "AUDIT_DBCC_EVENT";

		// Token: 0x0400101A RID: 4122
		internal const string AuditLogin = "AUDIT_LOGIN";

		// Token: 0x0400101B RID: 4123
		internal const string AuditLoginChangePasswordEvent = "AUDIT_LOGIN_CHANGE_PASSWORD_EVENT";

		// Token: 0x0400101C RID: 4124
		internal const string AuditLoginChangePropertyEvent = "AUDIT_LOGIN_CHANGE_PROPERTY_EVENT";

		// Token: 0x0400101D RID: 4125
		internal const string AuditLoginFailed = "AUDIT_LOGIN_FAILED";

		// Token: 0x0400101E RID: 4126
		internal const string AuditLoginGdrEvent = "AUDIT_LOGIN_GDR_EVENT";

		// Token: 0x0400101F RID: 4127
		internal const string AuditLogout = "AUDIT_LOGOUT";

		// Token: 0x04001020 RID: 4128
		internal const string AuditSchemaObjectAccessEvent = "AUDIT_SCHEMA_OBJECT_ACCESS_EVENT";

		// Token: 0x04001021 RID: 4129
		internal const string AuditSchemaObjectGdrEvent = "AUDIT_SCHEMA_OBJECT_GDR_EVENT";

		// Token: 0x04001022 RID: 4130
		internal const string AuditSchemaObjectManagementEvent = "AUDIT_SCHEMA_OBJECT_MANAGEMENT_EVENT";

		// Token: 0x04001023 RID: 4131
		internal const string AuditSchemaObjectTakeOwnershipEvent = "AUDIT_SCHEMA_OBJECT_TAKE_OWNERSHIP_EVENT";

		// Token: 0x04001024 RID: 4132
		internal const string AuditServerAlterTraceEvent = "AUDIT_SERVER_ALTER_TRACE_EVENT";

		// Token: 0x04001025 RID: 4133
		internal const string AuditServerObjectGdrEvent = "AUDIT_SERVER_OBJECT_GDR_EVENT";

		// Token: 0x04001026 RID: 4134
		internal const string AuditServerObjectManagementEvent = "AUDIT_SERVER_OBJECT_MANAGEMENT_EVENT";

		// Token: 0x04001027 RID: 4135
		internal const string AuditServerObjectTakeOwnershipEvent = "AUDIT_SERVER_OBJECT_TAKE_OWNERSHIP_EVENT";

		// Token: 0x04001028 RID: 4136
		internal const string AuditServerOperationEvent = "AUDIT_SERVER_OPERATION_EVENT";

		// Token: 0x04001029 RID: 4137
		internal const string AuditServerPrincipalImpersonationEvent = "AUDIT_SERVER_PRINCIPAL_IMPERSONATION_EVENT";

		// Token: 0x0400102A RID: 4138
		internal const string AuditServerPrincipalManagementEvent = "AUDIT_SERVER_PRINCIPAL_MANAGEMENT_EVENT";

		// Token: 0x0400102B RID: 4139
		internal const string AuditServerScopeGdrEvent = "AUDIT_SERVER_SCOPE_GDR_EVENT";

		// Token: 0x0400102C RID: 4140
		internal const string BlockedProcessReport = "BLOCKED_PROCESS_REPORT";

		// Token: 0x0400102D RID: 4141
		internal const string DataFileAutoGrow = "DATA_FILE_AUTO_GROW";

		// Token: 0x0400102E RID: 4142
		internal const string DataFileAutoShrink = "DATA_FILE_AUTO_SHRINK";

		// Token: 0x0400102F RID: 4143
		internal const string DatabaseMirroringStateChange = "DATABASE_MIRRORING_STATE_CHANGE";

		// Token: 0x04001030 RID: 4144
		internal const string DeadlockGraph = "DEADLOCK_GRAPH";

		// Token: 0x04001031 RID: 4145
		internal const string DeprecationAnnouncement = "DEPRECATION_ANNOUNCEMENT";

		// Token: 0x04001032 RID: 4146
		internal const string DeprecationFinalSupport = "DEPRECATION_FINAL_SUPPORT";

		// Token: 0x04001033 RID: 4147
		internal const string ErrorLog = "ERRORLOG";

		// Token: 0x04001034 RID: 4148
		internal const string EventLog = "EVENTLOG";

		// Token: 0x04001035 RID: 4149
		internal const string Exception = "EXCEPTION";

		// Token: 0x04001036 RID: 4150
		internal const string ExchangeSpillEvent = "EXCHANGE_SPILL_EVENT";

		// Token: 0x04001037 RID: 4151
		internal const string ExecutionWarnings = "EXECUTION_WARNINGS";

		// Token: 0x04001038 RID: 4152
		internal const string FtCrawlAborted = "FT_CRAWL_ABORTED";

		// Token: 0x04001039 RID: 4153
		internal const string FtCrawlStarted = "FT_CRAWL_STARTED";

		// Token: 0x0400103A RID: 4154
		internal const string FtCrawlStopped = "FT_CRAWL_STOPPED";

		// Token: 0x0400103B RID: 4155
		internal const string HashWarning = "HASH_WARNING";

		// Token: 0x0400103C RID: 4156
		internal const string LockDeadlock = "LOCK_DEADLOCK";

		// Token: 0x0400103D RID: 4157
		internal const string LockDeadlockChain = "LOCK_DEADLOCK_CHAIN";

		// Token: 0x0400103E RID: 4158
		internal const string LockEscalation = "LOCK_ESCALATION";

		// Token: 0x0400103F RID: 4159
		internal const string LogFileAutoGrow = "LOG_FILE_AUTO_GROW";

		// Token: 0x04001040 RID: 4160
		internal const string LogFileAutoShrink = "LOG_FILE_AUTO_SHRINK";

		// Token: 0x04001041 RID: 4161
		internal const string MissingColumnStatistics = "MISSING_COLUMN_STATISTICS";

		// Token: 0x04001042 RID: 4162
		internal const string MissingJoinPredicate = "MISSING_JOIN_PREDICATE";

		// Token: 0x04001043 RID: 4163
		internal const string MountTape = "MOUNT_TAPE";

		// Token: 0x04001044 RID: 4164
		internal const string ObjectAltered = "OBJECT_ALTERED";

		// Token: 0x04001045 RID: 4165
		internal const string ObjectCreated = "OBJECT_CREATED";

		// Token: 0x04001046 RID: 4166
		internal const string ObjectDeleted = "OBJECT_DELETED";

		// Token: 0x04001047 RID: 4167
		internal const string OledbCallEvent = "OLEDB_CALL_EVENT";

		// Token: 0x04001048 RID: 4168
		internal const string OledbDataReadEvent = "OLEDB_DATAREAD_EVENT";

		// Token: 0x04001049 RID: 4169
		internal const string OledbErrors = "OLEDB_ERRORS";

		// Token: 0x0400104A RID: 4170
		internal const string OledbProviderInformation = "OLEDB_PROVIDER_INFORMATION";

		// Token: 0x0400104B RID: 4171
		internal const string OledbQueryInterfaceEvent = "OLEDB_QUERYINTERFACE_EVENT";

		// Token: 0x0400104C RID: 4172
		internal const string QnDynamics = "QN__DYNAMICS";

		// Token: 0x0400104D RID: 4173
		internal const string QnParameterTable = "QN__PARAMETER_TABLE";

		// Token: 0x0400104E RID: 4174
		internal const string QnSubscription = "QN__SUBSCRIPTION";

		// Token: 0x0400104F RID: 4175
		internal const string QnTemplate = "QN__TEMPLATE";

		// Token: 0x04001050 RID: 4176
		internal const string ServerMemoryChange = "SERVER_MEMORY_CHANGE";

		// Token: 0x04001051 RID: 4177
		internal const string ShowPlanAllForQueryCompile = "SHOWPLAN_ALL_FOR_QUERY_COMPILE";

		// Token: 0x04001052 RID: 4178
		internal const string ShowPlanXmlForQueryCompile = "SHOWPLAN_XML_FOR_QUERY_COMPILE";

		// Token: 0x04001053 RID: 4179
		internal const string ShowPlanXml = "SHOWPLAN_XML";

		// Token: 0x04001054 RID: 4180
		internal const string ShowPlanXmlStatisticsProfile = "SHOWPLAN_XML_STATISTICS_PROFILE";

		// Token: 0x04001055 RID: 4181
		internal const string SortWarnings = "SORT_WARNINGS";

		// Token: 0x04001056 RID: 4182
		internal const string SpCacheInsert = "SP_CACHEINSERT";

		// Token: 0x04001057 RID: 4183
		internal const string SpCacheMiss = "SP_CACHEMISS";

		// Token: 0x04001058 RID: 4184
		internal const string SpCacheRemove = "SP_CACHEREMOVE";

		// Token: 0x04001059 RID: 4185
		internal const string SpRecompile = "SP_RECOMPILE";

		// Token: 0x0400105A RID: 4186
		internal const string SqlStmtRecompile = "SQL_STMTRECOMPILE";

		// Token: 0x0400105B RID: 4187
		internal const string TraceFileClose = "TRACE_FILE_CLOSE";

		// Token: 0x0400105C RID: 4188
		internal const string UserErrorMessage = "USER_ERROR_MESSAGE";

		// Token: 0x0400105D RID: 4189
		internal const string UserConfigurable0 = "USERCONFIGURABLE_0";

		// Token: 0x0400105E RID: 4190
		internal const string UserConfigurable1 = "USERCONFIGURABLE_1";

		// Token: 0x0400105F RID: 4191
		internal const string UserConfigurable2 = "USERCONFIGURABLE_2";

		// Token: 0x04001060 RID: 4192
		internal const string UserConfigurable3 = "USERCONFIGURABLE_3";

		// Token: 0x04001061 RID: 4193
		internal const string UserConfigurable4 = "USERCONFIGURABLE_4";

		// Token: 0x04001062 RID: 4194
		internal const string UserConfigurable5 = "USERCONFIGURABLE_5";

		// Token: 0x04001063 RID: 4195
		internal const string UserConfigurable6 = "USERCONFIGURABLE_6";

		// Token: 0x04001064 RID: 4196
		internal const string UserConfigurable7 = "USERCONFIGURABLE_7";

		// Token: 0x04001065 RID: 4197
		internal const string UserConfigurable8 = "USERCONFIGURABLE_8";

		// Token: 0x04001066 RID: 4198
		internal const string UserConfigurable9 = "USERCONFIGURABLE_9";

		// Token: 0x04001067 RID: 4199
		internal const string XQueryStaticType = "XQUERY_STATIC_TYPE";

		// Token: 0x04001068 RID: 4200
		internal const string AuditFullText = "AUDIT_FULLTEXT";

		// Token: 0x04001069 RID: 4201
		internal const string BitmapWarning = "BITMAP_WARNING";

		// Token: 0x0400106A RID: 4202
		internal const string CpuThresholdExceeded = "CPU_THRESHOLD_EXCEEDED";

		// Token: 0x0400106B RID: 4203
		internal const string DatabaseSuspectDataPage = "DATABASE_SUSPECT_DATA_PAGE";

		// Token: 0x0400106C RID: 4204
		internal const string DdlAsymmetricKeyEvents = "DDL_ASYMMETRIC_KEY_EVENTS";

		// Token: 0x0400106D RID: 4205
		internal const string DdlBrokerPriorityEvents = "DDL_BROKER_PRIORITY_EVENTS";

		// Token: 0x0400106E RID: 4206
		internal const string DdlCryptoSignatureEvents = "DDL_CRYPTO_SIGNATURE_EVENTS";

		// Token: 0x0400106F RID: 4207
		internal const string DdlDatabaseAuditSpecificationEvents = "DDL_DATABASE_AUDIT_SPECIFICATION_EVENTS";

		// Token: 0x04001070 RID: 4208
		internal const string DdlDatabaseEncryptionKeyEvents = "DDL_DATABASE_ENCRYPTION_KEY_EVENTS";

		// Token: 0x04001071 RID: 4209
		internal const string DdlDefaultEvents = "DDL_DEFAULT_EVENTS";

		// Token: 0x04001072 RID: 4210
		internal const string DdlExtendedPropertyEvents = "DDL_EXTENDED_PROPERTY_EVENTS";

		// Token: 0x04001073 RID: 4211
		internal const string DdlFullTextCatalogEvents = "DDL_FULLTEXT_CATALOG_EVENTS";

		// Token: 0x04001074 RID: 4212
		internal const string DdlFullTextStopListEvents = "DDL_FULLTEXT_STOPLIST_EVENTS";

		// Token: 0x04001075 RID: 4213
		internal const string DdlMasterKeyEvents = "DDL_MASTER_KEY_EVENTS";

		// Token: 0x04001076 RID: 4214
		internal const string DdlPlanGuideEvents = "DDL_PLAN_GUIDE_EVENTS";

		// Token: 0x04001077 RID: 4215
		internal const string DdlRuleEvents = "DDL_RULE_EVENTS";

		// Token: 0x04001078 RID: 4216
		internal const string DdlSymmetricKeyEvents = "DDL_SYMMETRIC_KEY_EVENTS";

		// Token: 0x04001079 RID: 4217
		internal const string DdlCredentialEvents = "DDL_CREDENTIAL_EVENTS";

		// Token: 0x0400107A RID: 4218
		internal const string DdlDatabaseEvents = "DDL_DATABASE_EVENTS";

		// Token: 0x0400107B RID: 4219
		internal const string DdlCryptographicProviderEvents = "DDL_CRYPTOGRAPHIC_PROVIDER_EVENTS";

		// Token: 0x0400107C RID: 4220
		internal const string DdlEventSessionEvents = "DDL_EVENT_SESSION_EVENTS";

		// Token: 0x0400107D RID: 4221
		internal const string DdlExtendedProcedureEvents = "DDL_EXTENDED_PROCEDURE_EVENTS";

		// Token: 0x0400107E RID: 4222
		internal const string DdlLinkedServerEvents = "DDL_LINKED_SERVER_EVENTS";

		// Token: 0x0400107F RID: 4223
		internal const string DdlLinkedServerLoginEvents = "DDL_LINKED_SERVER_LOGIN_EVENTS";

		// Token: 0x04001080 RID: 4224
		internal const string DdlMessageEvents = "DDL_MESSAGE_EVENTS";

		// Token: 0x04001081 RID: 4225
		internal const string DdlRemoteServerEvents = "DDL_REMOTE_SERVER_EVENTS";

		// Token: 0x04001082 RID: 4226
		internal const string DdlResourceGovernorEvents = "DDL_RESOURCE_GOVERNOR_EVENTS";

		// Token: 0x04001083 RID: 4227
		internal const string DdlResourcePool = "DDL_RESOURCE_POOL";

		// Token: 0x04001084 RID: 4228
		internal const string DdlSearchPropertyListEvents = "DDL_SEARCH_PROPERTY_LIST_EVENTS";

		// Token: 0x04001085 RID: 4229
		internal const string DdlSequenceEvents = "DDL_SEQUENCE_EVENTS";

		// Token: 0x04001086 RID: 4230
		internal const string DdlAvailabilityGroupEvents = "DDL_AVAILABILITY_GROUP_EVENTS";

		// Token: 0x04001087 RID: 4231
		internal const string DdlServerAuditEvents = "DDL_SERVER_AUDIT_EVENTS";

		// Token: 0x04001088 RID: 4232
		internal const string DdlServerAuditSpecificationEvents = "DDL_SERVER_AUDIT_SPECIFICATION_EVENTS";

		// Token: 0x04001089 RID: 4233
		internal const string DdlServiceMasterKeyEvents = "DDL_SERVICE_MASTER_KEY_EVENTS";

		// Token: 0x0400108A RID: 4234
		internal const string DdlWorkloadGroup = "DDL_WORKLOAD_GROUP";

		// Token: 0x0400108B RID: 4235
		internal const string DdlEvents = "DDL_EVENTS";

		// Token: 0x0400108C RID: 4236
		internal const string DdlApplicationRoleEvents = "DDL_APPLICATION_ROLE_EVENTS";

		// Token: 0x0400108D RID: 4237
		internal const string DdlAssemblyEvents = "DDL_ASSEMBLY_EVENTS";

		// Token: 0x0400108E RID: 4238
		internal const string DdlAuthorizationDatabaseEvents = "DDL_AUTHORIZATION_DATABASE_EVENTS";

		// Token: 0x0400108F RID: 4239
		internal const string DdlAuthorizationServerEvents = "DDL_AUTHORIZATION_SERVER_EVENTS";

		// Token: 0x04001090 RID: 4240
		internal const string DdlCertificateEvents = "DDL_CERTIFICATE_EVENTS";

		// Token: 0x04001091 RID: 4241
		internal const string DdlContractEvents = "DDL_CONTRACT_EVENTS";

		// Token: 0x04001092 RID: 4242
		internal const string DdlDatabaseLevelEvents = "DDL_DATABASE_LEVEL_EVENTS";

		// Token: 0x04001093 RID: 4243
		internal const string DdlDatabaseSecurityEvents = "DDL_DATABASE_SECURITY_EVENTS";

		// Token: 0x04001094 RID: 4244
		internal const string DdlEndpointEvents = "DDL_ENDPOINT_EVENTS";

		// Token: 0x04001095 RID: 4245
		internal const string DdlEventNotificationEvents = "DDL_EVENT_NOTIFICATION_EVENTS";

		// Token: 0x04001096 RID: 4246
		internal const string DdlFunctionEvents = "DDL_FUNCTION_EVENTS";

		// Token: 0x04001097 RID: 4247
		internal const string DdlGdrDatabaseEvents = "DDL_GDR_DATABASE_EVENTS";

		// Token: 0x04001098 RID: 4248
		internal const string DdlGdrServerEvents = "DDL_GDR_SERVER_EVENTS";

		// Token: 0x04001099 RID: 4249
		internal const string DdlIndexEvents = "DDL_INDEX_EVENTS";

		// Token: 0x0400109A RID: 4250
		internal const string DdlLoginEvents = "DDL_LOGIN_EVENTS";

		// Token: 0x0400109B RID: 4251
		internal const string DdlMessageTypeEvents = "DDL_MESSAGE_TYPE_EVENTS";

		// Token: 0x0400109C RID: 4252
		internal const string DdlPartitionEvents = "DDL_PARTITION_EVENTS";

		// Token: 0x0400109D RID: 4253
		internal const string DdlPartitionFunctionEvents = "DDL_PARTITION_FUNCTION_EVENTS";

		// Token: 0x0400109E RID: 4254
		internal const string DdlPartitionSchemeEvents = "DDL_PARTITION_SCHEME_EVENTS";

		// Token: 0x0400109F RID: 4255
		internal const string DdlProcedureEvents = "DDL_PROCEDURE_EVENTS";

		// Token: 0x040010A0 RID: 4256
		internal const string DdlQueueEvents = "DDL_QUEUE_EVENTS";

		// Token: 0x040010A1 RID: 4257
		internal const string DdlRemoteServiceBindingEvents = "DDL_REMOTE_SERVICE_BINDING_EVENTS";

		// Token: 0x040010A2 RID: 4258
		internal const string DdlRoleEvents = "DDL_ROLE_EVENTS";

		// Token: 0x040010A3 RID: 4259
		internal const string DdlRouteEvents = "DDL_ROUTE_EVENTS";

		// Token: 0x040010A4 RID: 4260
		internal const string DdlSchemaEvents = "DDL_SCHEMA_EVENTS";

		// Token: 0x040010A5 RID: 4261
		internal const string DdlServerLevelEvents = "DDL_SERVER_LEVEL_EVENTS";

		// Token: 0x040010A6 RID: 4262
		internal const string DdlServerSecurityEvents = "DDL_SERVER_SECURITY_EVENTS";

		// Token: 0x040010A7 RID: 4263
		internal const string DdlServiceEvents = "DDL_SERVICE_EVENTS";

		// Token: 0x040010A8 RID: 4264
		internal const string DdlSsbEvents = "DDL_SSB_EVENTS";

		// Token: 0x040010A9 RID: 4265
		internal const string DdlStatisticsEvents = "DDL_STATISTICS_EVENTS";

		// Token: 0x040010AA RID: 4266
		internal const string DdlSynonymEvents = "DDL_SYNONYM_EVENTS";

		// Token: 0x040010AB RID: 4267
		internal const string DdlTableEvents = "DDL_TABLE_EVENTS";

		// Token: 0x040010AC RID: 4268
		internal const string DdlTableViewEvents = "DDL_TABLE_VIEW_EVENTS";

		// Token: 0x040010AD RID: 4269
		internal const string DdlTriggerEvents = "DDL_TRIGGER_EVENTS";

		// Token: 0x040010AE RID: 4270
		internal const string DdlTypeEvents = "DDL_TYPE_EVENTS";

		// Token: 0x040010AF RID: 4271
		internal const string DdlUserEvents = "DDL_USER_EVENTS";

		// Token: 0x040010B0 RID: 4272
		internal const string DdlViewEvents = "DDL_VIEW_EVENTS";

		// Token: 0x040010B1 RID: 4273
		internal const string DdlXmlSchemaCollectionEvents = "DDL_XML_SCHEMA_COLLECTION_EVENTS";

		// Token: 0x040010B2 RID: 4274
		internal const string TrcClr = "TRC_CLR";

		// Token: 0x040010B3 RID: 4275
		internal const string TrcDatabase = "TRC_DATABASE";

		// Token: 0x040010B4 RID: 4276
		internal const string TrcDeprecation = "TRC_DEPRECATION";

		// Token: 0x040010B5 RID: 4277
		internal const string TrcErrorsAndWarnings = "TRC_ERRORS_AND_WARNINGS";

		// Token: 0x040010B6 RID: 4278
		internal const string TrcFullText = "TRC_FULL_TEXT";

		// Token: 0x040010B7 RID: 4279
		internal const string TrcLocks = "TRC_LOCKS";

		// Token: 0x040010B8 RID: 4280
		internal const string TrcObjects = "TRC_OBJECTS";

		// Token: 0x040010B9 RID: 4281
		internal const string TrcOledb = "TRC_OLEDB";

		// Token: 0x040010BA RID: 4282
		internal const string TrcPerformance = "TRC_PERFORMANCE";

		// Token: 0x040010BB RID: 4283
		internal const string TrcQueryNotifications = "TRC_QUERY_NOTIFICATIONS";

		// Token: 0x040010BC RID: 4284
		internal const string TrcSecurityAudit = "TRC_SECURITY_AUDIT";

		// Token: 0x040010BD RID: 4285
		internal const string TrcServer = "TRC_SERVER";

		// Token: 0x040010BE RID: 4286
		internal const string TrcStoredProcedures = "TRC_STORED_PROCEDURES";

		// Token: 0x040010BF RID: 4287
		internal const string TrcTsql = "TRC_TSQL";

		// Token: 0x040010C0 RID: 4288
		internal const string TrcUserConfigurable = "TRC_USER_CONFIGURABLE";

		// Token: 0x040010C1 RID: 4289
		internal const string TrcAllEvents = "TRC_ALL_EVENTS";

		// Token: 0x040010C2 RID: 4290
		internal const string SuccessfulLoginGroup = "SUCCESSFUL_LOGIN_GROUP";

		// Token: 0x040010C3 RID: 4291
		internal const string LogoutGroup = "LOGOUT_GROUP";

		// Token: 0x040010C4 RID: 4292
		internal const string ServerStateChangeGroup = "SERVER_STATE_CHANGE_GROUP";

		// Token: 0x040010C5 RID: 4293
		internal const string FailedLoginGroup = "FAILED_LOGIN_GROUP";

		// Token: 0x040010C6 RID: 4294
		internal const string LoginChangePasswordGroup = "LOGIN_CHANGE_PASSWORD_GROUP";

		// Token: 0x040010C7 RID: 4295
		internal const string ServerRoleMemberChangeGroup = "SERVER_ROLE_MEMBER_CHANGE_GROUP";

		// Token: 0x040010C8 RID: 4296
		internal const string ServerPrincipalImpersonationGroup = "SERVER_PRINCIPAL_IMPERSONATION_GROUP";

		// Token: 0x040010C9 RID: 4297
		internal const string ServerObjectOwnershipChangeGroup = "SERVER_OBJECT_OWNERSHIP_CHANGE_GROUP";

		// Token: 0x040010CA RID: 4298
		internal const string DatabaseMirroringLoginGroup = "DATABASE_MIRRORING_LOGIN_GROUP";

		// Token: 0x040010CB RID: 4299
		internal const string BrokerLoginGroup = "BROKER_LOGIN_GROUP";

		// Token: 0x040010CC RID: 4300
		internal const string ServerPermissionChangeGroup = "SERVER_PERMISSION_CHANGE_GROUP";

		// Token: 0x040010CD RID: 4301
		internal const string ServerObjectPermissionChangeGroup = "SERVER_OBJECT_PERMISSION_CHANGE_GROUP";

		// Token: 0x040010CE RID: 4302
		internal const string ServerOperationGroup = "SERVER_OPERATION_GROUP";

		// Token: 0x040010CF RID: 4303
		internal const string TraceChangeGroup = "TRACE_CHANGE_GROUP";

		// Token: 0x040010D0 RID: 4304
		internal const string ServerObjectChangeGroup = "SERVER_OBJECT_CHANGE_GROUP";

		// Token: 0x040010D1 RID: 4305
		internal const string ServerPrincipalChangeGroup = "SERVER_PRINCIPAL_CHANGE_GROUP";

		// Token: 0x040010D2 RID: 4306
		internal const string DatabasePermissionChangeGroup = "DATABASE_PERMISSION_CHANGE_GROUP";

		// Token: 0x040010D3 RID: 4307
		internal const string SchemaObjectPermissionChangeGroup = "SCHEMA_OBJECT_PERMISSION_CHANGE_GROUP";

		// Token: 0x040010D4 RID: 4308
		internal const string DatabaseRoleMemberChangeGroup = "DATABASE_ROLE_MEMBER_CHANGE_GROUP";

		// Token: 0x040010D5 RID: 4309
		internal const string ApplicationRoleChangePasswordGroup = "APPLICATION_ROLE_CHANGE_PASSWORD_GROUP";

		// Token: 0x040010D6 RID: 4310
		internal const string SchemaObjectAccessGroup = "SCHEMA_OBJECT_ACCESS_GROUP";

		// Token: 0x040010D7 RID: 4311
		internal const string BackupRestoreGroup = "BACKUP_RESTORE_GROUP";

		// Token: 0x040010D8 RID: 4312
		internal const string DbccGroup = "DBCC_GROUP";

		// Token: 0x040010D9 RID: 4313
		internal const string AuditChangeGroup = "AUDIT_CHANGE_GROUP";

		// Token: 0x040010DA RID: 4314
		internal const string DatabaseChangeGroup = "DATABASE_CHANGE_GROUP";

		// Token: 0x040010DB RID: 4315
		internal const string DatabaseObjectChangeGroup = "DATABASE_OBJECT_CHANGE_GROUP";

		// Token: 0x040010DC RID: 4316
		internal const string DatabasePrincipalChangeGroup = "DATABASE_PRINCIPAL_CHANGE_GROUP";

		// Token: 0x040010DD RID: 4317
		internal const string SchemaObjectChangeGroup = "SCHEMA_OBJECT_CHANGE_GROUP";

		// Token: 0x040010DE RID: 4318
		internal const string DatabasePrincipalImpersonationGroup = "DATABASE_PRINCIPAL_IMPERSONATION_GROUP";

		// Token: 0x040010DF RID: 4319
		internal const string DatabaseObjectOwnershipChangeGroup = "DATABASE_OBJECT_OWNERSHIP_CHANGE_GROUP";

		// Token: 0x040010E0 RID: 4320
		internal const string DatabaseOwnershipChangeGroup = "DATABASE_OWNERSHIP_CHANGE_GROUP";

		// Token: 0x040010E1 RID: 4321
		internal const string SchemaObjectOwnershipChangeGroup = "SCHEMA_OBJECT_OWNERSHIP_CHANGE_GROUP";

		// Token: 0x040010E2 RID: 4322
		internal const string DatabaseObjectPermissionChangeGroup = "DATABASE_OBJECT_PERMISSION_CHANGE_GROUP";

		// Token: 0x040010E3 RID: 4323
		internal const string DatabaseOperationGroup = "DATABASE_OPERATION_GROUP";

		// Token: 0x040010E4 RID: 4324
		internal const string DatabaseObjectAccessGroup = "DATABASE_OBJECT_ACCESS_GROUP";

		// Token: 0x040010E5 RID: 4325
		internal const string SuccessfulDatabaseAuthenticationGroup = "SUCCESSFUL_DATABASE_AUTHENTICATION_GROUP";

		// Token: 0x040010E6 RID: 4326
		internal const string FailedDatabaseAuthenticationGroup = "FAILED_DATABASE_AUTHENTICATION_GROUP";

		// Token: 0x040010E7 RID: 4327
		internal const string DatabaseLogoutGroup = "DATABASE_LOGOUT_GROUP";

		// Token: 0x040010E8 RID: 4328
		internal const string UserChangePasswordGroup = "USER_CHANGE_PASSWORD_GROUP";

		// Token: 0x040010E9 RID: 4329
		internal const string UserDefinedAuditGroup = "USER_DEFINED_AUDIT_GROUP";
	}
}
