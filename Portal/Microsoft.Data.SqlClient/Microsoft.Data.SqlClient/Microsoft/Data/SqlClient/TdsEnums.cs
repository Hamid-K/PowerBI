using System;
using System.Data;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000A5 RID: 165
	internal static class TdsEnums
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x000271A4 File Offset: 0x000253A4
		internal static string GetSniContextEnumName(SniContext sniContext)
		{
			switch (sniContext)
			{
			case SniContext.Undefined:
				return "Undefined";
			case SniContext.Snix_Connect:
				return "Snix_Connect";
			case SniContext.Snix_PreLoginBeforeSuccessfulWrite:
				return "Snix_PreLoginBeforeSuccessfulWrite";
			case SniContext.Snix_PreLogin:
				return "Snix_PreLogin";
			case SniContext.Snix_LoginSspi:
				return "Snix_LoginSspi";
			case SniContext.Snix_ProcessSspi:
				return "Snix_ProcessSspi";
			case SniContext.Snix_Login:
				return "Snix_Login";
			case SniContext.Snix_EnableMars:
				return "Snix_EnableMars";
			case SniContext.Snix_AutoEnlist:
				return "Snix_AutoEnlist";
			case SniContext.Snix_GetMarsSession:
				return "Snix_GetMarsSession";
			case SniContext.Snix_Execute:
				return "Snix_Execute";
			case SniContext.Snix_Read:
				return "Snix_Read";
			case SniContext.Snix_Close:
				return "Snix_Close";
			case SniContext.Snix_SendRows:
				return "Snix_SendRows";
			default:
				return null;
			}
		}

		// Token: 0x0400036B RID: 875
		public const string SQL_PROVIDER_NAME = "Framework Microsoft SqlClient Data Provider";

		// Token: 0x0400036C RID: 876
		public static readonly decimal SQL_SMALL_MONEY_MIN = new decimal(-214748.3648);

		// Token: 0x0400036D RID: 877
		public static readonly decimal SQL_SMALL_MONEY_MAX = new decimal(214748.3647);

		// Token: 0x0400036E RID: 878
		public const string SDCI_MAPFILENAME = "SqlClientSSDebug";

		// Token: 0x0400036F RID: 879
		public const byte SDCI_MAX_MACHINENAME = 32;

		// Token: 0x04000370 RID: 880
		public const byte SDCI_MAX_DLLNAME = 16;

		// Token: 0x04000371 RID: 881
		public const byte SDCI_MAX_DATA = 255;

		// Token: 0x04000372 RID: 882
		public const int SQLDEBUG_OFF = 0;

		// Token: 0x04000373 RID: 883
		public const int SQLDEBUG_ON = 1;

		// Token: 0x04000374 RID: 884
		public const int SQLDEBUG_CONTEXT = 2;

		// Token: 0x04000375 RID: 885
		public const string SP_SDIDEBUG = "sp_sdidebug";

		// Token: 0x04000376 RID: 886
		public static readonly string[] SQLDEBUG_MODE_NAMES = new string[] { "off", "on", "context" };

		// Token: 0x04000377 RID: 887
		public const SqlDbType SmallVarBinary = (SqlDbType)24;

		// Token: 0x04000378 RID: 888
		public const string TCP = "tcp";

		// Token: 0x04000379 RID: 889
		public const string NP = "np";

		// Token: 0x0400037A RID: 890
		public const string RPC = "rpc";

		// Token: 0x0400037B RID: 891
		public const string BV = "bv";

		// Token: 0x0400037C RID: 892
		public const string ADSP = "adsp";

		// Token: 0x0400037D RID: 893
		public const string SPX = "spx";

		// Token: 0x0400037E RID: 894
		public const string VIA = "via";

		// Token: 0x0400037F RID: 895
		public const string LPC = "lpc";

		// Token: 0x04000380 RID: 896
		public const string ADMIN = "admin";

		// Token: 0x04000381 RID: 897
		public const string INIT_SSPI_PACKAGE = "InitSSPIPackage";

		// Token: 0x04000382 RID: 898
		public const string INIT_SESSION = "InitSession";

		// Token: 0x04000383 RID: 899
		public const string CONNECTION_GET_SVR_USER = "ConnectionGetSvrUser";

		// Token: 0x04000384 RID: 900
		public const string GEN_CLIENT_CONTEXT = "GenClientContext";

		// Token: 0x04000385 RID: 901
		public const byte SOFTFLUSH = 0;

		// Token: 0x04000386 RID: 902
		public const byte HARDFLUSH = 1;

		// Token: 0x04000387 RID: 903
		public const byte IGNORE = 2;

		// Token: 0x04000388 RID: 904
		public const int HEADER_LEN = 8;

		// Token: 0x04000389 RID: 905
		public const int HEADER_LEN_FIELD_OFFSET = 2;

		// Token: 0x0400038A RID: 906
		public const int SPID_OFFSET = 4;

		// Token: 0x0400038B RID: 907
		public const int SQL2005_HEADER_LEN = 12;

		// Token: 0x0400038C RID: 908
		public const int MARS_ID_OFFSET = 8;

		// Token: 0x0400038D RID: 909
		public const int HEADERTYPE_QNOTIFICATION = 1;

		// Token: 0x0400038E RID: 910
		public const int HEADERTYPE_MARS = 2;

		// Token: 0x0400038F RID: 911
		public const int HEADERTYPE_TRACE = 3;

		// Token: 0x04000390 RID: 912
		public const int SUCCEED = 1;

		// Token: 0x04000391 RID: 913
		public const int FAIL = 0;

		// Token: 0x04000392 RID: 914
		public const short TYPE_SIZE_LIMIT = 8000;

		// Token: 0x04000393 RID: 915
		public const int MIN_PACKET_SIZE = 512;

		// Token: 0x04000394 RID: 916
		public const int DEFAULT_LOGIN_PACKET_SIZE = 4096;

		// Token: 0x04000395 RID: 917
		public const int MAX_PRELOGIN_PAYLOAD_LENGTH = 1024;

		// Token: 0x04000396 RID: 918
		public const int MAX_PACKET_SIZE = 32768;

		// Token: 0x04000397 RID: 919
		public const int MAX_SERVER_USER_NAME = 256;

		// Token: 0x04000398 RID: 920
		public const byte MIN_ERROR_CLASS = 11;

		// Token: 0x04000399 RID: 921
		public const byte MAX_USER_CORRECTABLE_ERROR_CLASS = 16;

		// Token: 0x0400039A RID: 922
		public const byte FATAL_ERROR_CLASS = 20;

		// Token: 0x0400039B RID: 923
		public const byte MT_SQL = 1;

		// Token: 0x0400039C RID: 924
		public const byte MT_LOGIN = 2;

		// Token: 0x0400039D RID: 925
		public const byte MT_RPC = 3;

		// Token: 0x0400039E RID: 926
		public const byte MT_TOKENS = 4;

		// Token: 0x0400039F RID: 927
		public const byte MT_BINARY = 5;

		// Token: 0x040003A0 RID: 928
		public const byte MT_ATTN = 6;

		// Token: 0x040003A1 RID: 929
		public const byte MT_BULK = 7;

		// Token: 0x040003A2 RID: 930
		public const byte MT_FEDAUTH = 8;

		// Token: 0x040003A3 RID: 931
		public const byte MT_CLOSE = 9;

		// Token: 0x040003A4 RID: 932
		public const byte MT_ERROR = 10;

		// Token: 0x040003A5 RID: 933
		public const byte MT_ACK = 11;

		// Token: 0x040003A6 RID: 934
		public const byte MT_ECHO = 12;

		// Token: 0x040003A7 RID: 935
		public const byte MT_LOGOUT = 13;

		// Token: 0x040003A8 RID: 936
		public const byte MT_TRANS = 14;

		// Token: 0x040003A9 RID: 937
		public const byte MT_OLEDB = 15;

		// Token: 0x040003AA RID: 938
		public const byte MT_LOGIN7 = 16;

		// Token: 0x040003AB RID: 939
		public const byte MT_SSPI = 17;

		// Token: 0x040003AC RID: 940
		public const byte MT_PRELOGIN = 18;

		// Token: 0x040003AD RID: 941
		public const byte ST_EOM = 1;

		// Token: 0x040003AE RID: 942
		public const byte ST_AACK = 2;

		// Token: 0x040003AF RID: 943
		public const byte ST_IGNORE = 2;

		// Token: 0x040003B0 RID: 944
		public const byte ST_BATCH = 4;

		// Token: 0x040003B1 RID: 945
		public const byte ST_RESET_CONNECTION = 8;

		// Token: 0x040003B2 RID: 946
		public const byte ST_RESET_CONNECTION_PRESERVE_TRANSACTION = 16;

		// Token: 0x040003B3 RID: 947
		public const byte SQLCOLFMT = 161;

		// Token: 0x040003B4 RID: 948
		public const byte SQLPROCID = 124;

		// Token: 0x040003B5 RID: 949
		public const byte SQLCOLNAME = 160;

		// Token: 0x040003B6 RID: 950
		public const byte SQLTABNAME = 164;

		// Token: 0x040003B7 RID: 951
		public const byte SQLCOLINFO = 165;

		// Token: 0x040003B8 RID: 952
		public const byte SQLALTNAME = 167;

		// Token: 0x040003B9 RID: 953
		public const byte SQLALTFMT = 168;

		// Token: 0x040003BA RID: 954
		public const byte SQLERROR = 170;

		// Token: 0x040003BB RID: 955
		public const byte SQLINFO = 171;

		// Token: 0x040003BC RID: 956
		public const byte SQLRETURNVALUE = 172;

		// Token: 0x040003BD RID: 957
		public const byte SQLRETURNSTATUS = 121;

		// Token: 0x040003BE RID: 958
		public const byte SQLRETURNTOK = 219;

		// Token: 0x040003BF RID: 959
		public const byte SQLALTCONTROL = 175;

		// Token: 0x040003C0 RID: 960
		public const byte SQLROW = 209;

		// Token: 0x040003C1 RID: 961
		public const byte SQLNBCROW = 210;

		// Token: 0x040003C2 RID: 962
		public const byte SQLALTROW = 211;

		// Token: 0x040003C3 RID: 963
		public const byte SQLDONE = 253;

		// Token: 0x040003C4 RID: 964
		public const byte SQLDONEPROC = 254;

		// Token: 0x040003C5 RID: 965
		public const byte SQLDONEINPROC = 255;

		// Token: 0x040003C6 RID: 966
		public const byte SQLOFFSET = 120;

		// Token: 0x040003C7 RID: 967
		public const byte SQLORDER = 169;

		// Token: 0x040003C8 RID: 968
		public const byte SQLDEBUG_CMD = 96;

		// Token: 0x040003C9 RID: 969
		public const byte SQLLOGINACK = 173;

		// Token: 0x040003CA RID: 970
		public const byte SQLFEATUREEXTACK = 174;

		// Token: 0x040003CB RID: 971
		public const byte SQLSESSIONSTATE = 228;

		// Token: 0x040003CC RID: 972
		public const byte SQLENVCHANGE = 227;

		// Token: 0x040003CD RID: 973
		public const byte SQLSECLEVEL = 237;

		// Token: 0x040003CE RID: 974
		public const byte SQLROWCRC = 57;

		// Token: 0x040003CF RID: 975
		public const byte SQLCOLMETADATA = 129;

		// Token: 0x040003D0 RID: 976
		public const byte SQLALTMETADATA = 136;

		// Token: 0x040003D1 RID: 977
		public const byte SQLSSPI = 237;

		// Token: 0x040003D2 RID: 978
		public const byte SQLFEDAUTHINFO = 238;

		// Token: 0x040003D3 RID: 979
		public const byte SQLRESCOLSRCS = 162;

		// Token: 0x040003D4 RID: 980
		public const byte SQLDATACLASSIFICATION = 163;

		// Token: 0x040003D5 RID: 981
		public const byte ENV_DATABASE = 1;

		// Token: 0x040003D6 RID: 982
		public const byte ENV_LANG = 2;

		// Token: 0x040003D7 RID: 983
		public const byte ENV_CHARSET = 3;

		// Token: 0x040003D8 RID: 984
		public const byte ENV_PACKETSIZE = 4;

		// Token: 0x040003D9 RID: 985
		public const byte ENV_LOCALEID = 5;

		// Token: 0x040003DA RID: 986
		public const byte ENV_COMPFLAGS = 6;

		// Token: 0x040003DB RID: 987
		public const byte ENV_COLLATION = 7;

		// Token: 0x040003DC RID: 988
		public const byte ENV_BEGINTRAN = 8;

		// Token: 0x040003DD RID: 989
		public const byte ENV_COMMITTRAN = 9;

		// Token: 0x040003DE RID: 990
		public const byte ENV_ROLLBACKTRAN = 10;

		// Token: 0x040003DF RID: 991
		public const byte ENV_ENLISTDTC = 11;

		// Token: 0x040003E0 RID: 992
		public const byte ENV_DEFECTDTC = 12;

		// Token: 0x040003E1 RID: 993
		public const byte ENV_LOGSHIPNODE = 13;

		// Token: 0x040003E2 RID: 994
		public const byte ENV_PROMOTETRANSACTION = 15;

		// Token: 0x040003E3 RID: 995
		public const byte ENV_TRANSACTIONMANAGERADDRESS = 16;

		// Token: 0x040003E4 RID: 996
		public const byte ENV_TRANSACTIONENDED = 17;

		// Token: 0x040003E5 RID: 997
		public const byte ENV_SPRESETCONNECTIONACK = 18;

		// Token: 0x040003E6 RID: 998
		public const byte ENV_USERINSTANCE = 19;

		// Token: 0x040003E7 RID: 999
		public const byte ENV_ROUTING = 20;

		// Token: 0x040003E8 RID: 1000
		public const int DONE_MORE = 1;

		// Token: 0x040003E9 RID: 1001
		public const int DONE_ERROR = 2;

		// Token: 0x040003EA RID: 1002
		public const int DONE_INXACT = 4;

		// Token: 0x040003EB RID: 1003
		public const int DONE_PROC = 8;

		// Token: 0x040003EC RID: 1004
		public const int DONE_COUNT = 16;

		// Token: 0x040003ED RID: 1005
		public const int DONE_ATTN = 32;

		// Token: 0x040003EE RID: 1006
		public const int DONE_INPROC = 64;

		// Token: 0x040003EF RID: 1007
		public const int DONE_RPCINBATCH = 128;

		// Token: 0x040003F0 RID: 1008
		public const int DONE_SRVERROR = 256;

		// Token: 0x040003F1 RID: 1009
		public const int DONE_FMTSENT = 32768;

		// Token: 0x040003F2 RID: 1010
		public const byte FEATUREEXT_TERMINATOR = 255;

		// Token: 0x040003F3 RID: 1011
		public const byte FEATUREEXT_SRECOVERY = 1;

		// Token: 0x040003F4 RID: 1012
		public const byte FEATUREEXT_FEDAUTH = 2;

		// Token: 0x040003F5 RID: 1013
		public const byte FEATUREEXT_TCE = 4;

		// Token: 0x040003F6 RID: 1014
		public const byte FEATUREEXT_GLOBALTRANSACTIONS = 5;

		// Token: 0x040003F7 RID: 1015
		public const byte FEATUREEXT_AZURESQLSUPPORT = 8;

		// Token: 0x040003F8 RID: 1016
		public const byte FEATUREEXT_DATACLASSIFICATION = 9;

		// Token: 0x040003F9 RID: 1017
		public const byte FEATUREEXT_UTF8SUPPORT = 10;

		// Token: 0x040003FA RID: 1018
		public const byte FEATUREEXT_SQLDNSCACHING = 11;

		// Token: 0x040003FB RID: 1019
		public const uint UTF8_IN_TDSCOLLATION = 67108864U;

		// Token: 0x040003FC RID: 1020
		public const byte FEDAUTHLIB_LIVEID = 0;

		// Token: 0x040003FD RID: 1021
		public const byte FEDAUTHLIB_SECURITYTOKEN = 1;

		// Token: 0x040003FE RID: 1022
		public const byte FEDAUTHLIB_MSAL = 2;

		// Token: 0x040003FF RID: 1023
		public const byte FEDAUTHLIB_RESERVED = 127;

		// Token: 0x04000400 RID: 1024
		public const byte MSALWORKFLOW_ACTIVEDIRECTORYPASSWORD = 1;

		// Token: 0x04000401 RID: 1025
		public const byte MSALWORKFLOW_ACTIVEDIRECTORYINTEGRATED = 2;

		// Token: 0x04000402 RID: 1026
		public const byte MSALWORKFLOW_ACTIVEDIRECTORYINTERACTIVE = 3;

		// Token: 0x04000403 RID: 1027
		public const byte MSALWORKFLOW_ACTIVEDIRECTORYSERVICEPRINCIPAL = 1;

		// Token: 0x04000404 RID: 1028
		public const byte MSALWORKFLOW_ACTIVEDIRECTORYDEVICECODEFLOW = 3;

		// Token: 0x04000405 RID: 1029
		public const byte MSALWORKFLOW_ACTIVEDIRECTORYMANAGEDIDENTITY = 3;

		// Token: 0x04000406 RID: 1030
		public const byte MSALWORKFLOW_ACTIVEDIRECTORYDEFAULT = 3;

		// Token: 0x04000407 RID: 1031
		public const string NTAUTHORITYANONYMOUSLOGON = "NT Authority\\Anonymous Logon";

		// Token: 0x04000408 RID: 1032
		public const byte MAX_LOG_NAME = 30;

		// Token: 0x04000409 RID: 1033
		public const byte MAX_PROG_NAME = 10;

		// Token: 0x0400040A RID: 1034
		public const byte SEC_COMP_LEN = 8;

		// Token: 0x0400040B RID: 1035
		public const byte MAX_PK_LEN = 6;

		// Token: 0x0400040C RID: 1036
		public const byte MAX_NIC_SIZE = 6;

		// Token: 0x0400040D RID: 1037
		public const byte SQLVARIANT_SIZE = 2;

		// Token: 0x0400040E RID: 1038
		public const byte VERSION_SIZE = 4;

		// Token: 0x0400040F RID: 1039
		public const int CLIENT_PROG_VER = 100663296;

		// Token: 0x04000410 RID: 1040
		public const int SQL2005_LOG_REC_FIXED_LEN = 94;

		// Token: 0x04000411 RID: 1041
		public const int TEXT_TIME_STAMP_LEN = 8;

		// Token: 0x04000412 RID: 1042
		public const int COLLATION_INFO_LEN = 4;

		// Token: 0x04000413 RID: 1043
		public const int SQL70OR2000_MAJOR = 7;

		// Token: 0x04000414 RID: 1044
		public const int SQL70_INCREMENT = 0;

		// Token: 0x04000415 RID: 1045
		public const int SQL2000_INCREMENT = 1;

		// Token: 0x04000416 RID: 1046
		public const int DEFAULT_MINOR = 0;

		// Token: 0x04000417 RID: 1047
		public const int SQL2000SP1_MAJOR = 113;

		// Token: 0x04000418 RID: 1048
		public const int SQL2005_MAJOR = 114;

		// Token: 0x04000419 RID: 1049
		public const int SQL2008_MAJOR = 115;

		// Token: 0x0400041A RID: 1050
		public const int SQL2012_MAJOR = 116;

		// Token: 0x0400041B RID: 1051
		public const int TDS8_MAJOR = 8;

		// Token: 0x0400041C RID: 1052
		public const string TDS8_Protocol = "tds/8.0";

		// Token: 0x0400041D RID: 1053
		public const int SQL2000SP1_INCREMENT = 0;

		// Token: 0x0400041E RID: 1054
		public const int SQL2005_INCREMENT = 9;

		// Token: 0x0400041F RID: 1055
		public const int SQL2008_INCREMENT = 11;

		// Token: 0x04000420 RID: 1056
		public const int SQL2012_INCREMENT = 0;

		// Token: 0x04000421 RID: 1057
		public const int TDS8_INCREMENT = 0;

		// Token: 0x04000422 RID: 1058
		public const int SQL2000SP1_MINOR = 1;

		// Token: 0x04000423 RID: 1059
		public const int SQL2005_RTM_MINOR = 2;

		// Token: 0x04000424 RID: 1060
		public const int SQL2008_MINOR = 3;

		// Token: 0x04000425 RID: 1061
		public const int SQL2012_MINOR = 4;

		// Token: 0x04000426 RID: 1062
		public const int TDS8_MINOR = 0;

		// Token: 0x04000427 RID: 1063
		public const int ORDER_68000 = 1;

		// Token: 0x04000428 RID: 1064
		public const int USE_DB_ON = 1;

		// Token: 0x04000429 RID: 1065
		public const int INIT_DB_FATAL = 1;

		// Token: 0x0400042A RID: 1066
		public const int SET_LANG_ON = 1;

		// Token: 0x0400042B RID: 1067
		public const int INIT_LANG_FATAL = 1;

		// Token: 0x0400042C RID: 1068
		public const int ODBC_ON = 1;

		// Token: 0x0400042D RID: 1069
		public const int SSPI_ON = 1;

		// Token: 0x0400042E RID: 1070
		public const int REPL_ON = 3;

		// Token: 0x0400042F RID: 1071
		public const int READONLY_INTENT_ON = 1;

		// Token: 0x04000430 RID: 1072
		public const byte SQLLenMask = 48;

		// Token: 0x04000431 RID: 1073
		public const byte SQLFixedLen = 48;

		// Token: 0x04000432 RID: 1074
		public const byte SQLVarLen = 32;

		// Token: 0x04000433 RID: 1075
		public const byte SQLZeroLen = 16;

		// Token: 0x04000434 RID: 1076
		public const byte SQLVarCnt = 0;

		// Token: 0x04000435 RID: 1077
		public const byte SQLDifferentName = 32;

		// Token: 0x04000436 RID: 1078
		public const byte SQLExpression = 4;

		// Token: 0x04000437 RID: 1079
		public const byte SQLKey = 8;

		// Token: 0x04000438 RID: 1080
		public const byte SQLHidden = 16;

		// Token: 0x04000439 RID: 1081
		public const byte Nullable = 1;

		// Token: 0x0400043A RID: 1082
		public const byte Identity = 16;

		// Token: 0x0400043B RID: 1083
		public const byte Updatability = 11;

		// Token: 0x0400043C RID: 1084
		public const byte ClrFixedLen = 1;

		// Token: 0x0400043D RID: 1085
		public const byte IsColumnSet = 4;

		// Token: 0x0400043E RID: 1086
		public const byte IsEncrypted = 8;

		// Token: 0x0400043F RID: 1087
		public const uint VARLONGNULL = 4294967295U;

		// Token: 0x04000440 RID: 1088
		public const int VARNULL = 65535;

		// Token: 0x04000441 RID: 1089
		public const int MAXSIZE = 8000;

		// Token: 0x04000442 RID: 1090
		public const byte FIXEDNULL = 0;

		// Token: 0x04000443 RID: 1091
		public const ulong UDTNULL = 18446744073709551615UL;

		// Token: 0x04000444 RID: 1092
		public const int SQLVOID = 31;

		// Token: 0x04000445 RID: 1093
		public const int SQLTEXT = 35;

		// Token: 0x04000446 RID: 1094
		public const int SQLVARBINARY = 37;

		// Token: 0x04000447 RID: 1095
		public const int SQLINTN = 38;

		// Token: 0x04000448 RID: 1096
		public const int SQLVARCHAR = 39;

		// Token: 0x04000449 RID: 1097
		public const int SQLBINARY = 45;

		// Token: 0x0400044A RID: 1098
		public const int SQLIMAGE = 34;

		// Token: 0x0400044B RID: 1099
		public const int SQLCHAR = 47;

		// Token: 0x0400044C RID: 1100
		public const int SQLINT1 = 48;

		// Token: 0x0400044D RID: 1101
		public const int SQLBIT = 50;

		// Token: 0x0400044E RID: 1102
		public const int SQLINT2 = 52;

		// Token: 0x0400044F RID: 1103
		public const int SQLINT4 = 56;

		// Token: 0x04000450 RID: 1104
		public const int SQLMONEY = 60;

		// Token: 0x04000451 RID: 1105
		public const int SQLDATETIME = 61;

		// Token: 0x04000452 RID: 1106
		public const int SQLFLT8 = 62;

		// Token: 0x04000453 RID: 1107
		public const int SQLFLTN = 109;

		// Token: 0x04000454 RID: 1108
		public const int SQLMONEYN = 110;

		// Token: 0x04000455 RID: 1109
		public const int SQLDATETIMN = 111;

		// Token: 0x04000456 RID: 1110
		public const int SQLFLT4 = 59;

		// Token: 0x04000457 RID: 1111
		public const int SQLMONEY4 = 122;

		// Token: 0x04000458 RID: 1112
		public const int SQLDATETIM4 = 58;

		// Token: 0x04000459 RID: 1113
		public const int SQLDECIMALN = 106;

		// Token: 0x0400045A RID: 1114
		public const int SQLNUMERICN = 108;

		// Token: 0x0400045B RID: 1115
		public const int SQLUNIQUEID = 36;

		// Token: 0x0400045C RID: 1116
		public const int SQLBIGCHAR = 175;

		// Token: 0x0400045D RID: 1117
		public const int SQLBIGVARCHAR = 167;

		// Token: 0x0400045E RID: 1118
		public const int SQLBIGBINARY = 173;

		// Token: 0x0400045F RID: 1119
		public const int SQLBIGVARBINARY = 165;

		// Token: 0x04000460 RID: 1120
		public const int SQLBITN = 104;

		// Token: 0x04000461 RID: 1121
		public const int SQLNCHAR = 239;

		// Token: 0x04000462 RID: 1122
		public const int SQLNVARCHAR = 231;

		// Token: 0x04000463 RID: 1123
		public const int SQLNTEXT = 99;

		// Token: 0x04000464 RID: 1124
		public const int SQLUDT = 240;

		// Token: 0x04000465 RID: 1125
		public const int AOPCNTB = 9;

		// Token: 0x04000466 RID: 1126
		public const int AOPSTDEV = 48;

		// Token: 0x04000467 RID: 1127
		public const int AOPSTDEVP = 49;

		// Token: 0x04000468 RID: 1128
		public const int AOPVAR = 50;

		// Token: 0x04000469 RID: 1129
		public const int AOPVARP = 51;

		// Token: 0x0400046A RID: 1130
		public const int AOPCNT = 75;

		// Token: 0x0400046B RID: 1131
		public const int AOPSUM = 77;

		// Token: 0x0400046C RID: 1132
		public const int AOPAVG = 79;

		// Token: 0x0400046D RID: 1133
		public const int AOPMIN = 81;

		// Token: 0x0400046E RID: 1134
		public const int AOPMAX = 82;

		// Token: 0x0400046F RID: 1135
		public const int AOPANY = 83;

		// Token: 0x04000470 RID: 1136
		public const int AOPNOOP = 86;

		// Token: 0x04000471 RID: 1137
		public const int SQLTIMESTAMP = 80;

		// Token: 0x04000472 RID: 1138
		public const int MAX_NUMERIC_LEN = 17;

		// Token: 0x04000473 RID: 1139
		public const int DEFAULT_NUMERIC_PRECISION = 29;

		// Token: 0x04000474 RID: 1140
		public const int SQL70_DEFAULT_NUMERIC_PRECISION = 28;

		// Token: 0x04000475 RID: 1141
		public const int MAX_NUMERIC_PRECISION = 38;

		// Token: 0x04000476 RID: 1142
		public const byte UNKNOWN_PRECISION_SCALE = 255;

		// Token: 0x04000477 RID: 1143
		public const int SQLINT8 = 127;

		// Token: 0x04000478 RID: 1144
		public const int SQLVARIANT = 98;

		// Token: 0x04000479 RID: 1145
		public const int SQLXMLTYPE = 241;

		// Token: 0x0400047A RID: 1146
		public const int XMLUNICODEBOM = 65279;

		// Token: 0x0400047B RID: 1147
		public static readonly byte[] XMLUNICODEBOMBYTES = new byte[] { byte.MaxValue, 254 };

		// Token: 0x0400047C RID: 1148
		public const int SQLTABLE = 243;

		// Token: 0x0400047D RID: 1149
		public const int SQLDATE = 40;

		// Token: 0x0400047E RID: 1150
		public const int SQLTIME = 41;

		// Token: 0x0400047F RID: 1151
		public const int SQLDATETIME2 = 42;

		// Token: 0x04000480 RID: 1152
		public const int SQLDATETIMEOFFSET = 43;

		// Token: 0x04000481 RID: 1153
		public const int DEFAULT_VARTIME_SCALE = 7;

		// Token: 0x04000482 RID: 1154
		public const ulong SQL_PLP_NULL = 18446744073709551615UL;

		// Token: 0x04000483 RID: 1155
		public const ulong SQL_PLP_UNKNOWNLEN = 18446744073709551614UL;

		// Token: 0x04000484 RID: 1156
		public const int SQL_PLP_CHUNK_TERMINATOR = 0;

		// Token: 0x04000485 RID: 1157
		public const ushort SQL_USHORTVARMAXLEN = 65535;

		// Token: 0x04000486 RID: 1158
		public const byte TVP_ROWCOUNT_ESTIMATE = 18;

		// Token: 0x04000487 RID: 1159
		public const byte TVP_ROW_TOKEN = 1;

		// Token: 0x04000488 RID: 1160
		public const byte TVP_END_TOKEN = 0;

		// Token: 0x04000489 RID: 1161
		public const ushort TVP_NOMETADATA_TOKEN = 65535;

		// Token: 0x0400048A RID: 1162
		public const byte TVP_ORDER_UNIQUE_TOKEN = 16;

		// Token: 0x0400048B RID: 1163
		public const int TVP_DEFAULT_COLUMN = 512;

		// Token: 0x0400048C RID: 1164
		public const byte TVP_ORDERASC_FLAG = 1;

		// Token: 0x0400048D RID: 1165
		public const byte TVP_ORDERDESC_FLAG = 2;

		// Token: 0x0400048E RID: 1166
		public const byte TVP_UNIQUE_FLAG = 4;

		// Token: 0x0400048F RID: 1167
		public const bool Is68K = false;

		// Token: 0x04000490 RID: 1168
		public const bool TraceTDS = false;

		// Token: 0x04000491 RID: 1169
		public const string SP_EXECUTESQL = "sp_executesql";

		// Token: 0x04000492 RID: 1170
		public const string SP_PREPEXEC = "sp_prepexec";

		// Token: 0x04000493 RID: 1171
		public const string SP_PREPARE = "sp_prepare";

		// Token: 0x04000494 RID: 1172
		public const string SP_EXECUTE = "sp_execute";

		// Token: 0x04000495 RID: 1173
		public const string SP_UNPREPARE = "sp_unprepare";

		// Token: 0x04000496 RID: 1174
		public const string SP_PARAMS = "sp_procedure_params_rowset";

		// Token: 0x04000497 RID: 1175
		public const string SP_PARAMS_MANAGED = "sp_procedure_params_managed";

		// Token: 0x04000498 RID: 1176
		public const string SP_PARAMS_MGD10 = "sp_procedure_params_100_managed";

		// Token: 0x04000499 RID: 1177
		public const ushort RPC_PROCID_CURSOR = 1;

		// Token: 0x0400049A RID: 1178
		public const ushort RPC_PROCID_CURSOROPEN = 2;

		// Token: 0x0400049B RID: 1179
		public const ushort RPC_PROCID_CURSORPREPARE = 3;

		// Token: 0x0400049C RID: 1180
		public const ushort RPC_PROCID_CURSOREXECUTE = 4;

		// Token: 0x0400049D RID: 1181
		public const ushort RPC_PROCID_CURSORPREPEXEC = 5;

		// Token: 0x0400049E RID: 1182
		public const ushort RPC_PROCID_CURSORUNPREPARE = 6;

		// Token: 0x0400049F RID: 1183
		public const ushort RPC_PROCID_CURSORFETCH = 7;

		// Token: 0x040004A0 RID: 1184
		public const ushort RPC_PROCID_CURSOROPTION = 8;

		// Token: 0x040004A1 RID: 1185
		public const ushort RPC_PROCID_CURSORCLOSE = 9;

		// Token: 0x040004A2 RID: 1186
		public const ushort RPC_PROCID_EXECUTESQL = 10;

		// Token: 0x040004A3 RID: 1187
		public const ushort RPC_PROCID_PREPARE = 11;

		// Token: 0x040004A4 RID: 1188
		public const ushort RPC_PROCID_EXECUTE = 12;

		// Token: 0x040004A5 RID: 1189
		public const ushort RPC_PROCID_PREPEXEC = 13;

		// Token: 0x040004A6 RID: 1190
		public const ushort RPC_PROCID_PREPEXECRPC = 14;

		// Token: 0x040004A7 RID: 1191
		public const ushort RPC_PROCID_UNPREPARE = 15;

		// Token: 0x040004A8 RID: 1192
		public const string TRANS_BEGIN = "BEGIN TRANSACTION";

		// Token: 0x040004A9 RID: 1193
		public const string TRANS_COMMIT = "COMMIT TRANSACTION";

		// Token: 0x040004AA RID: 1194
		public const string TRANS_ROLLBACK = "ROLLBACK TRANSACTION";

		// Token: 0x040004AB RID: 1195
		public const string TRANS_IF_ROLLBACK = "IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION";

		// Token: 0x040004AC RID: 1196
		public const string TRANS_SAVE = "SAVE TRANSACTION";

		// Token: 0x040004AD RID: 1197
		public const string TRANS_READ_COMMITTED = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED";

		// Token: 0x040004AE RID: 1198
		public const string TRANS_READ_UNCOMMITTED = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";

		// Token: 0x040004AF RID: 1199
		public const string TRANS_REPEATABLE_READ = "SET TRANSACTION ISOLATION LEVEL REPEATABLE READ";

		// Token: 0x040004B0 RID: 1200
		public const string TRANS_SERIALIZABLE = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE";

		// Token: 0x040004B1 RID: 1201
		public const string TRANS_SNAPSHOT = "SET TRANSACTION ISOLATION LEVEL SNAPSHOT";

		// Token: 0x040004B2 RID: 1202
		public const byte SQL2000_RPCBATCHFLAG = 128;

		// Token: 0x040004B3 RID: 1203
		public const byte SQL2005_RPCBATCHFLAG = 255;

		// Token: 0x040004B4 RID: 1204
		public const byte RPC_RECOMPILE = 1;

		// Token: 0x040004B5 RID: 1205
		public const byte RPC_NOMETADATA = 2;

		// Token: 0x040004B6 RID: 1206
		public const byte RPC_PARAM_BYREF = 1;

		// Token: 0x040004B7 RID: 1207
		public const byte RPC_PARAM_DEFAULT = 2;

		// Token: 0x040004B8 RID: 1208
		public const byte RPC_PARAM_ENCRYPTED = 8;

		// Token: 0x040004B9 RID: 1209
		public const string PARAM_OUTPUT = "output";

		// Token: 0x040004BA RID: 1210
		public const int MAX_PARAMETER_NAME_LENGTH = 128;

		// Token: 0x040004BB RID: 1211
		public const string FMTONLY_ON = " SET FMTONLY ON;";

		// Token: 0x040004BC RID: 1212
		public const string FMTONLY_OFF = " SET FMTONLY OFF;";

		// Token: 0x040004BD RID: 1213
		public const string BROWSE_ON = " SET NO_BROWSETABLE ON;";

		// Token: 0x040004BE RID: 1214
		public const string BROWSE_OFF = " SET NO_BROWSETABLE OFF;";

		// Token: 0x040004BF RID: 1215
		public const string TABLE = "Table";

		// Token: 0x040004C0 RID: 1216
		public const int EXEC_THRESHOLD = 3;

		// Token: 0x040004C1 RID: 1217
		public const short TIMEOUT_EXPIRED = -2;

		// Token: 0x040004C2 RID: 1218
		public const short ENCRYPTION_NOT_SUPPORTED = 20;

		// Token: 0x040004C3 RID: 1219
		public const short CTAIP_NOT_SUPPORTED = 21;

		// Token: 0x040004C4 RID: 1220
		public const int LOGON_FAILED = 18456;

		// Token: 0x040004C5 RID: 1221
		public const int PASSWORD_EXPIRED = 18488;

		// Token: 0x040004C6 RID: 1222
		public const int IMPERSONATION_FAILED = 1346;

		// Token: 0x040004C7 RID: 1223
		public const int P_TOKENTOOLONG = 103;

		// Token: 0x040004C8 RID: 1224
		public const int TCE_CONVERSION_ERROR_CLIENT_RETRY = 33514;

		// Token: 0x040004C9 RID: 1225
		public const int TCE_ENCLAVE_INVALID_SESSION_HANDLE = 33195;

		// Token: 0x040004CA RID: 1226
		public const uint SNI_UNINITIALIZED = 4294967295U;

		// Token: 0x040004CB RID: 1227
		public const uint SNI_SUCCESS = 0U;

		// Token: 0x040004CC RID: 1228
		public const uint SNI_ERROR = 1U;

		// Token: 0x040004CD RID: 1229
		public const uint SNI_WAIT_TIMEOUT = 258U;

		// Token: 0x040004CE RID: 1230
		public const uint SNI_SUCCESS_IO_PENDING = 997U;

		// Token: 0x040004CF RID: 1231
		public const short SNI_WSAECONNRESET = 10054;

		// Token: 0x040004D0 RID: 1232
		public const uint SNI_QUEUE_FULL = 1048576U;

		// Token: 0x040004D1 RID: 1233
		public const uint SNI_SSL_VALIDATE_CERTIFICATE = 1U;

		// Token: 0x040004D2 RID: 1234
		public const uint SNI_SSL_USE_SCHANNEL_CACHE = 2U;

		// Token: 0x040004D3 RID: 1235
		public const uint SNI_SSL_IGNORE_CHANNEL_BINDINGS = 16U;

		// Token: 0x040004D4 RID: 1236
		public const uint SNI_SSL_SEND_ALPN_EXTENSION = 16384U;

		// Token: 0x040004D5 RID: 1237
		public const string DEFAULT_ENGLISH_CODE_PAGE_STRING = "iso_1";

		// Token: 0x040004D6 RID: 1238
		public const short DEFAULT_ENGLISH_CODE_PAGE_VALUE = 1252;

		// Token: 0x040004D7 RID: 1239
		public const short CHARSET_CODE_PAGE_OFFSET = 2;

		// Token: 0x040004D8 RID: 1240
		internal const int MAX_SERVERNAME = 255;

		// Token: 0x040004D9 RID: 1241
		internal const ushort SELECT = 193;

		// Token: 0x040004DA RID: 1242
		internal const ushort INSERT = 195;

		// Token: 0x040004DB RID: 1243
		internal const ushort DELETE = 196;

		// Token: 0x040004DC RID: 1244
		internal const ushort UPDATE = 197;

		// Token: 0x040004DD RID: 1245
		internal const ushort ABORT = 210;

		// Token: 0x040004DE RID: 1246
		internal const ushort BEGINXACT = 212;

		// Token: 0x040004DF RID: 1247
		internal const ushort ENDXACT = 213;

		// Token: 0x040004E0 RID: 1248
		internal const ushort BULKINSERT = 240;

		// Token: 0x040004E1 RID: 1249
		internal const ushort OPENCURSOR = 32;

		// Token: 0x040004E2 RID: 1250
		internal const ushort MERGE = 279;

		// Token: 0x040004E3 RID: 1251
		internal const ushort MAXLEN_HOSTNAME = 128;

		// Token: 0x040004E4 RID: 1252
		internal const ushort MAXLEN_CLIENTID = 128;

		// Token: 0x040004E5 RID: 1253
		internal const ushort MAXLEN_CLIENTSECRET = 128;

		// Token: 0x040004E6 RID: 1254
		internal const ushort MAXLEN_APPNAME = 128;

		// Token: 0x040004E7 RID: 1255
		internal const ushort MAXLEN_SERVERNAME = 128;

		// Token: 0x040004E8 RID: 1256
		internal const ushort MAXLEN_CLIENTINTERFACE = 128;

		// Token: 0x040004E9 RID: 1257
		internal const ushort MAXLEN_LANGUAGE = 128;

		// Token: 0x040004EA RID: 1258
		internal const ushort MAXLEN_DATABASE = 128;

		// Token: 0x040004EB RID: 1259
		internal const ushort MAXLEN_ATTACHDBFILE = 260;

		// Token: 0x040004EC RID: 1260
		internal const ushort MAXLEN_NEWPASSWORD = 128;

		// Token: 0x040004ED RID: 1261
		public static readonly ushort[] CODE_PAGE_FROM_SORT_ID = new ushort[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			437, 437, 437, 437, 437, 0, 0, 0, 0, 0,
			850, 850, 850, 850, 850, 0, 0, 0, 0, 850,
			1252, 1252, 1252, 1252, 1252, 850, 850, 850, 850, 850,
			850, 850, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 1252, 1252, 1252, 1252, 1252, 0, 0, 0, 0,
			1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250,
			1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250, 1250, 0,
			0, 0, 0, 0, 1251, 1251, 1251, 1251, 1251, 0,
			0, 0, 1253, 1253, 1253, 0, 0, 0, 0, 0,
			1253, 1253, 1253, 0, 1253, 0, 0, 0, 1254, 1254,
			1254, 0, 0, 0, 0, 0, 1255, 1255, 1255, 0,
			0, 0, 0, 0, 1256, 1256, 1256, 0, 0, 0,
			0, 0, 1257, 1257, 1257, 1257, 1257, 1257, 1257, 1257,
			1257, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 1252, 1252, 1252, 1252, 0, 0, 0,
			0, 0, 932, 932, 949, 949, 950, 950, 936, 936,
			932, 949, 950, 936, 874, 874, 874, 0, 0, 0,
			1252, 1252, 1252, 1252, 1252, 1252, 1252, 1252, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0
		};

		// Token: 0x040004EE RID: 1262
		internal static readonly long[] TICKS_FROM_SCALE = new long[] { 10000000L, 1000000L, 100000L, 10000L, 1000L, 100L, 10L, 1L };

		// Token: 0x040004EF RID: 1263
		internal const int MAX_TIME_SCALE = 7;

		// Token: 0x040004F0 RID: 1264
		internal const int MAX_TIME_LENGTH = 5;

		// Token: 0x040004F1 RID: 1265
		internal const int MAX_DATETIME2_LENGTH = 8;

		// Token: 0x040004F2 RID: 1266
		internal const int WHIDBEY_DATE_LENGTH = 10;

		// Token: 0x040004F3 RID: 1267
		internal static readonly int[] WHIDBEY_TIME_LENGTH = new int[] { 8, 10, 11, 12, 13, 14, 15, 16 };

		// Token: 0x040004F4 RID: 1268
		internal static readonly int[] WHIDBEY_DATETIME2_LENGTH = new int[] { 19, 21, 22, 23, 24, 25, 26, 27 };

		// Token: 0x040004F5 RID: 1269
		internal static readonly int[] WHIDBEY_DATETIMEOFFSET_LENGTH = new int[] { 26, 28, 29, 30, 31, 32, 33, 34 };

		// Token: 0x040004F6 RID: 1270
		internal const byte DATA_CLASSIFICATION_NOT_ENABLED = 0;

		// Token: 0x040004F7 RID: 1271
		internal const byte DATA_CLASSIFICATION_VERSION_WITHOUT_RANK_SUPPORT = 1;

		// Token: 0x040004F8 RID: 1272
		internal const byte DATA_CLASSIFICATION_VERSION_MAX_SUPPORTED = 2;

		// Token: 0x040004F9 RID: 1273
		internal const byte MAX_SUPPORTED_TCE_VERSION = 3;

		// Token: 0x040004FA RID: 1274
		internal const byte MIN_TCE_VERSION_WITH_ENCLAVE_SUPPORT = 2;

		// Token: 0x040004FB RID: 1275
		internal const ushort MAX_TCE_CIPHERINFO_SIZE = 2048;

		// Token: 0x040004FC RID: 1276
		internal const long MAX_TCE_CIPHERTEXT_SIZE = 2147483648L;

		// Token: 0x040004FD RID: 1277
		internal const byte CustomCipherAlgorithmId = 0;

		// Token: 0x040004FE RID: 1278
		internal const int AEAD_AES_256_CBC_HMAC_SHA256 = 2;

		// Token: 0x040004FF RID: 1279
		internal const string ENCLAVE_TYPE_VBS = "VBS";

		// Token: 0x04000500 RID: 1280
		internal const string ENCLAVE_TYPE_SGX = "SGX";

		// Token: 0x04000501 RID: 1281
		internal const string TCE_PARAM_CIPHERTEXT = "cipherText";

		// Token: 0x04000502 RID: 1282
		internal const string TCE_PARAM_CIPHER_ALGORITHM_ID = "cipherAlgorithmId";

		// Token: 0x04000503 RID: 1283
		internal const string TCE_PARAM_COLUMNENCRYPTION_KEY = "columnEncryptionKey";

		// Token: 0x04000504 RID: 1284
		internal const string TCE_PARAM_ENCRYPTION_ALGORITHM = "encryptionAlgorithm";

		// Token: 0x04000505 RID: 1285
		internal const string TCE_PARAM_ENCRYPTIONTYPE = "encryptionType";

		// Token: 0x04000506 RID: 1286
		internal const string TCE_PARAM_ENCRYPTIONKEY = "encryptionKey";

		// Token: 0x04000507 RID: 1287
		internal const string TCE_PARAM_MASTERKEY_PATH = "masterKeyPath";

		// Token: 0x04000508 RID: 1288
		internal const string TCE_PARAM_ENCRYPTED_CEK = "encryptedColumnEncryptionKey";

		// Token: 0x04000509 RID: 1289
		internal const string TCE_PARAM_CLIENT_KEYSTORE_PROVIDERS = "clientKeyStoreProviders";

		// Token: 0x0400050A RID: 1290
		internal const string TCE_PARAM_FORCE_COLUMN_ENCRYPTION = "ForceColumnEncryption(true)";

		// Token: 0x020001DB RID: 475
		public enum EnvChangeType : byte
		{
			// Token: 0x04001441 RID: 5185
			ENVCHANGE_DATABASE = 1,
			// Token: 0x04001442 RID: 5186
			ENVCHANGE_LANG,
			// Token: 0x04001443 RID: 5187
			ENVCHANGE_CHARSET,
			// Token: 0x04001444 RID: 5188
			ENVCHANGE_PACKETSIZE,
			// Token: 0x04001445 RID: 5189
			ENVCHANGE_LOCALEID,
			// Token: 0x04001446 RID: 5190
			ENVCHANGE_COMPFLAGS,
			// Token: 0x04001447 RID: 5191
			ENVCHANGE_COLLATION,
			// Token: 0x04001448 RID: 5192
			ENVCHANGE_BEGINTRAN,
			// Token: 0x04001449 RID: 5193
			ENVCHANGE_COMMITTRAN,
			// Token: 0x0400144A RID: 5194
			ENVCHANGE_ROLLBACKTRAN,
			// Token: 0x0400144B RID: 5195
			ENVCHANGE_ENLISTDTC,
			// Token: 0x0400144C RID: 5196
			ENVCHANGE_DEFECTDTC,
			// Token: 0x0400144D RID: 5197
			ENVCHANGE_LOGSHIPNODE,
			// Token: 0x0400144E RID: 5198
			ENVCHANGE_PROMOTETRANSACTION = 15,
			// Token: 0x0400144F RID: 5199
			ENVCHANGE_TRANSACTIONMANAGERADDRESS,
			// Token: 0x04001450 RID: 5200
			ENVCHANGE_TRANSACTIONENDED,
			// Token: 0x04001451 RID: 5201
			ENVCHANGE_SPRESETCONNECTIONACK,
			// Token: 0x04001452 RID: 5202
			ENVCHANGE_USERINSTANCE,
			// Token: 0x04001453 RID: 5203
			ENVCHANGE_ROUTING
		}

		// Token: 0x020001DC RID: 476
		[Flags]
		public enum FeatureExtension : uint
		{
			// Token: 0x04001455 RID: 5205
			None = 0U,
			// Token: 0x04001456 RID: 5206
			SessionRecovery = 1U,
			// Token: 0x04001457 RID: 5207
			FedAuth = 2U,
			// Token: 0x04001458 RID: 5208
			Tce = 8U,
			// Token: 0x04001459 RID: 5209
			GlobalTransactions = 16U,
			// Token: 0x0400145A RID: 5210
			AzureSQLSupport = 128U,
			// Token: 0x0400145B RID: 5211
			DataClassification = 256U,
			// Token: 0x0400145C RID: 5212
			UTF8Support = 512U,
			// Token: 0x0400145D RID: 5213
			SQLDNSCaching = 1024U
		}

		// Token: 0x020001DD RID: 477
		public enum FedAuthLibrary : byte
		{
			// Token: 0x0400145F RID: 5215
			LiveId,
			// Token: 0x04001460 RID: 5216
			SecurityToken,
			// Token: 0x04001461 RID: 5217
			MSAL,
			// Token: 0x04001462 RID: 5218
			Default = 127
		}

		// Token: 0x020001DE RID: 478
		public enum ActiveDirectoryWorkflow : byte
		{
			// Token: 0x04001464 RID: 5220
			Password = 1,
			// Token: 0x04001465 RID: 5221
			Integrated,
			// Token: 0x04001466 RID: 5222
			Interactive,
			// Token: 0x04001467 RID: 5223
			ServicePrincipal = 1,
			// Token: 0x04001468 RID: 5224
			DeviceCodeFlow = 3,
			// Token: 0x04001469 RID: 5225
			ManagedIdentity = 3,
			// Token: 0x0400146A RID: 5226
			Default = 3
		}

		// Token: 0x020001DF RID: 479
		internal enum UDTFormatType
		{
			// Token: 0x0400146C RID: 5228
			Native = 1,
			// Token: 0x0400146D RID: 5229
			UserDefined
		}

		// Token: 0x020001E0 RID: 480
		internal enum TransactionManagerRequestType
		{
			// Token: 0x0400146F RID: 5231
			GetDTCAddress,
			// Token: 0x04001470 RID: 5232
			Propagate,
			// Token: 0x04001471 RID: 5233
			Begin = 5,
			// Token: 0x04001472 RID: 5234
			Promote,
			// Token: 0x04001473 RID: 5235
			Commit,
			// Token: 0x04001474 RID: 5236
			Rollback,
			// Token: 0x04001475 RID: 5237
			Save
		}

		// Token: 0x020001E1 RID: 481
		internal enum TransactionManagerIsolationLevel
		{
			// Token: 0x04001477 RID: 5239
			Unspecified,
			// Token: 0x04001478 RID: 5240
			ReadUncommitted,
			// Token: 0x04001479 RID: 5241
			ReadCommitted,
			// Token: 0x0400147A RID: 5242
			RepeatableRead,
			// Token: 0x0400147B RID: 5243
			Serializable,
			// Token: 0x0400147C RID: 5244
			Snapshot
		}

		// Token: 0x020001E2 RID: 482
		internal enum GenericType
		{
			// Token: 0x0400147E RID: 5246
			MultiSet = 131
		}

		// Token: 0x020001E3 RID: 483
		internal enum FedAuthInfoId : byte
		{
			// Token: 0x04001480 RID: 5248
			Stsurl = 1,
			// Token: 0x04001481 RID: 5249
			Spn
		}
	}
}
