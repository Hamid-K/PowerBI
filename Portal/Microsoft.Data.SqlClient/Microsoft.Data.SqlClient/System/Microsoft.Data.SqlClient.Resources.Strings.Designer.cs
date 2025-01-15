using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000007 RID: 7
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Strings
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000027D1 File Offset: 0x000009D1
		internal Strings()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000027DC File Offset: 0x000009DC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Strings.resourceMan == null)
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.Data.SqlClient.Resources.Strings", typeof(Strings).Assembly);
					Strings.resourceMan = resourceManager;
				}
				return Strings.resourceMan;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002815 File Offset: 0x00000A15
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000281C File Offset: 0x00000A1C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Strings.resourceCulture;
			}
			set
			{
				Strings.resourceCulture = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002824 File Offset: 0x00000A24
		internal static string AAD_Token_Retrieving_Timeout
		{
			get
			{
				return Strings.ResourceManager.GetString("AAD_Token_Retrieving_Timeout", Strings.resourceCulture);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000283A File Offset: 0x00000A3A
		internal static string ADP_AdapterMappingExceptionMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_AdapterMappingExceptionMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002850 File Offset: 0x00000A50
		internal static string ADP_Ascending
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_Ascending", Strings.resourceCulture);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002866 File Offset: 0x00000A66
		internal static string ADP_BadParameterName
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_BadParameterName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000287C File Offset: 0x00000A7C
		internal static string ADP_CalledTwice
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CalledTwice", Strings.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002892 File Offset: 0x00000A92
		internal static string ADP_ClosedConnectionError
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ClosedConnectionError", Strings.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000028A8 File Offset: 0x00000AA8
		internal static string ADP_CollectionIndexInt32
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CollectionIndexInt32", Strings.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000028BE File Offset: 0x00000ABE
		internal static string ADP_CollectionIndexString
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CollectionIndexString", Strings.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000028D4 File Offset: 0x00000AD4
		internal static string ADP_CollectionInvalidType
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CollectionInvalidType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000028EA File Offset: 0x00000AEA
		internal static string ADP_CollectionIsNotParent
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CollectionIsNotParent", Strings.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002900 File Offset: 0x00000B00
		internal static string ADP_CollectionIsParent
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CollectionIsParent", Strings.resourceCulture);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002916 File Offset: 0x00000B16
		internal static string ADP_CollectionNullValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CollectionNullValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000292C File Offset: 0x00000B2C
		internal static string ADP_CollectionRemoveInvalidObject
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CollectionRemoveInvalidObject", Strings.resourceCulture);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002942 File Offset: 0x00000B42
		internal static string ADP_CollectionUniqueValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CollectionUniqueValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002958 File Offset: 0x00000B58
		internal static string ADP_ColumnSchemaExpression
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ColumnSchemaExpression", Strings.resourceCulture);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000296E File Offset: 0x00000B6E
		internal static string ADP_ColumnSchemaMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ColumnSchemaMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002984 File Offset: 0x00000B84
		internal static string ADP_ColumnSchemaMissing1
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ColumnSchemaMissing1", Strings.resourceCulture);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000299A File Offset: 0x00000B9A
		internal static string ADP_ColumnSchemaMissing2
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ColumnSchemaMissing2", Strings.resourceCulture);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000029B0 File Offset: 0x00000BB0
		internal static string ADP_CommandTextRequired
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_CommandTextRequired", Strings.resourceCulture);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000029C6 File Offset: 0x00000BC6
		internal static string ADP_ComputerNameEx
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ComputerNameEx", Strings.resourceCulture);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000029DC File Offset: 0x00000BDC
		internal static string ADP_ConnecitonRequired_UpdateRows
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnecitonRequired_UpdateRows", Strings.resourceCulture);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000029F2 File Offset: 0x00000BF2
		internal static string ADP_ConnectionAlreadyOpen
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionAlreadyOpen", Strings.resourceCulture);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002A08 File Offset: 0x00000C08
		internal static string ADP_ConnectionIsDisabled
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionIsDisabled", Strings.resourceCulture);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002A1E File Offset: 0x00000C1E
		internal static string ADP_ConnectionRequired
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionRequired", Strings.resourceCulture);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002A34 File Offset: 0x00000C34
		internal static string ADP_ConnectionRequired_Batch
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionRequired_Batch", Strings.resourceCulture);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002A4A File Offset: 0x00000C4A
		internal static string ADP_ConnectionRequired_Clone
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionRequired_Clone", Strings.resourceCulture);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002A60 File Offset: 0x00000C60
		internal static string ADP_ConnectionRequired_Delete
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionRequired_Delete", Strings.resourceCulture);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002A76 File Offset: 0x00000C76
		internal static string ADP_ConnectionRequired_Fill
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionRequired_Fill", Strings.resourceCulture);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002A8C File Offset: 0x00000C8C
		internal static string ADP_ConnectionRequired_FillPage
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionRequired_FillPage", Strings.resourceCulture);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002AA2 File Offset: 0x00000CA2
		internal static string ADP_ConnectionRequired_FillSchema
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionRequired_FillSchema", Strings.resourceCulture);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002AB8 File Offset: 0x00000CB8
		internal static string ADP_ConnectionRequired_Insert
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionRequired_Insert", Strings.resourceCulture);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002ACE File Offset: 0x00000CCE
		internal static string ADP_ConnectionRequired_Update
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionRequired_Update", Strings.resourceCulture);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002AE4 File Offset: 0x00000CE4
		internal static string ADP_ConnectionStateMsg
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionStateMsg", Strings.resourceCulture);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AFA File Offset: 0x00000CFA
		internal static string ADP_ConnectionStateMsg_Closed
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionStateMsg_Closed", Strings.resourceCulture);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002B10 File Offset: 0x00000D10
		internal static string ADP_ConnectionStateMsg_Connecting
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionStateMsg_Connecting", Strings.resourceCulture);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002B26 File Offset: 0x00000D26
		internal static string ADP_ConnectionStateMsg_Open
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionStateMsg_Open", Strings.resourceCulture);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002B3C File Offset: 0x00000D3C
		internal static string ADP_ConnectionStateMsg_OpenExecuting
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionStateMsg_OpenExecuting", Strings.resourceCulture);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002B52 File Offset: 0x00000D52
		internal static string ADP_ConnectionStateMsg_OpenFetching
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionStateMsg_OpenFetching", Strings.resourceCulture);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002B68 File Offset: 0x00000D68
		internal static string ADP_ConnectionStringSyntax
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ConnectionStringSyntax", Strings.resourceCulture);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002B7E File Offset: 0x00000D7E
		internal static string ADP_DataAdapterExceptionMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DataAdapterExceptionMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002B94 File Offset: 0x00000D94
		internal static string ADP_DatabaseNameTooLong
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DatabaseNameTooLong", Strings.resourceCulture);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002BAA File Offset: 0x00000DAA
		internal static string ADP_DataReaderClosed
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DataReaderClosed", Strings.resourceCulture);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002BC0 File Offset: 0x00000DC0
		internal static string ADP_DataReaderNoData
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DataReaderNoData", Strings.resourceCulture);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002BD6 File Offset: 0x00000DD6
		internal static string ADP_DBConcurrencyExceptionMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DBConcurrencyExceptionMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002BEC File Offset: 0x00000DEC
		internal static string ADP_DbDataUpdatableRecordReadOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DbDataUpdatableRecordReadOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002C02 File Offset: 0x00000E02
		internal static string ADP_DbRecordReadOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DbRecordReadOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002C18 File Offset: 0x00000E18
		internal static string ADP_DbTypeNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DbTypeNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002C2E File Offset: 0x00000E2E
		internal static string ADP_DelegatedTransactionPresent
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DelegatedTransactionPresent", Strings.resourceCulture);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002C44 File Offset: 0x00000E44
		internal static string ADP_DeriveParametersNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DeriveParametersNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002C5A File Offset: 0x00000E5A
		internal static string ADP_Descending
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_Descending", Strings.resourceCulture);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C70 File Offset: 0x00000E70
		internal static string ADP_DoubleValuedProperty
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DoubleValuedProperty", Strings.resourceCulture);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002C86 File Offset: 0x00000E86
		internal static string ADP_DynamicSQLJoinUnsupported
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DynamicSQLJoinUnsupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002C9C File Offset: 0x00000E9C
		internal static string ADP_DynamicSQLNestedQuote
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DynamicSQLNestedQuote", Strings.resourceCulture);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002CB2 File Offset: 0x00000EB2
		internal static string ADP_DynamicSQLNoKeyInfoDelete
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DynamicSQLNoKeyInfoDelete", Strings.resourceCulture);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002CC8 File Offset: 0x00000EC8
		internal static string ADP_DynamicSQLNoKeyInfoRowVersionDelete
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DynamicSQLNoKeyInfoRowVersionDelete", Strings.resourceCulture);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002CDE File Offset: 0x00000EDE
		internal static string ADP_DynamicSQLNoKeyInfoRowVersionUpdate
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DynamicSQLNoKeyInfoRowVersionUpdate", Strings.resourceCulture);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002CF4 File Offset: 0x00000EF4
		internal static string ADP_DynamicSQLNoKeyInfoUpdate
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DynamicSQLNoKeyInfoUpdate", Strings.resourceCulture);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002D0A File Offset: 0x00000F0A
		internal static string ADP_DynamicSQLNoTableInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_DynamicSQLNoTableInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002D20 File Offset: 0x00000F20
		internal static string ADP_EmptyArray
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_EmptyArray", Strings.resourceCulture);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D36 File Offset: 0x00000F36
		internal static string ADP_EmptyDatabaseName
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_EmptyDatabaseName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D4C File Offset: 0x00000F4C
		internal static string ADP_EmptyString
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_EmptyString", Strings.resourceCulture);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D62 File Offset: 0x00000F62
		internal static string ADP_EvenLengthLiteralValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_EvenLengthLiteralValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002D78 File Offset: 0x00000F78
		internal static string ADP_FillChapterAutoIncrement
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_FillChapterAutoIncrement", Strings.resourceCulture);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002D8E File Offset: 0x00000F8E
		internal static string ADP_FillRequiresSourceTableName
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_FillRequiresSourceTableName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002DA4 File Offset: 0x00000FA4
		internal static string ADP_FillSchemaRequiresSourceTableName
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_FillSchemaRequiresSourceTableName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002DBA File Offset: 0x00000FBA
		internal static string ADP_HexDigitLiteralValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_HexDigitLiteralValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002DD0 File Offset: 0x00000FD0
		internal static string ADP_IncorrectAsyncResult
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_IncorrectAsyncResult", Strings.resourceCulture);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002DE6 File Offset: 0x00000FE6
		internal static string ADP_InternalConnectionError
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InternalConnectionError", Strings.resourceCulture);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002DFC File Offset: 0x00000FFC
		internal static string ADP_InternalProviderError
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InternalProviderError", Strings.resourceCulture);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002E12 File Offset: 0x00001012
		internal static string ADP_InvalidArgumentLength
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidArgumentLength", Strings.resourceCulture);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002E28 File Offset: 0x00001028
		internal static string ADP_InvalidArgumentValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidArgumentValue", Strings.resourceCulture);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002E3E File Offset: 0x0000103E
		internal static string ADP_InvalidBufferSizeOrIndex
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidBufferSizeOrIndex", Strings.resourceCulture);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E54 File Offset: 0x00001054
		internal static string ADP_InvalidCommandTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidCommandTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002E6A File Offset: 0x0000106A
		internal static string ADP_InvalidConnectionOptionValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidConnectionOptionValue", Strings.resourceCulture);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002E80 File Offset: 0x00001080
		internal static string ADP_InvalidConnectionOptionValueLength
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidConnectionOptionValueLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002E96 File Offset: 0x00001096
		internal static string ADP_InvalidConnectTimeoutValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidConnectTimeoutValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002EAC File Offset: 0x000010AC
		internal static string ADP_InvalidDataDirectory
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidDataDirectory", Strings.resourceCulture);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002EC2 File Offset: 0x000010C2
		internal static string ADP_InvalidDataLength
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidDataLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002ED8 File Offset: 0x000010D8
		internal static string ADP_InvalidDataLength2
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidDataLength2", Strings.resourceCulture);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002EEE File Offset: 0x000010EE
		internal static string ADP_InvalidDataType
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidDataType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002F04 File Offset: 0x00001104
		internal static string ADP_InvalidDateTimeDigits
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidDateTimeDigits", Strings.resourceCulture);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F1A File Offset: 0x0000111A
		internal static string ADP_InvalidDestinationBufferIndex
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidDestinationBufferIndex", Strings.resourceCulture);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002F30 File Offset: 0x00001130
		internal static string ADP_InvalidEnumerationValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidEnumerationValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002F46 File Offset: 0x00001146
		internal static string ADP_InvalidFormatValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidFormatValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002F5C File Offset: 0x0000115C
		internal static string ADP_InvalidImplicitConversion
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidImplicitConversion", Strings.resourceCulture);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002F72 File Offset: 0x00001172
		internal static string ADP_InvalidKey
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidKey", Strings.resourceCulture);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002F88 File Offset: 0x00001188
		internal static string ADP_InvalidMaximumScale
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMaximumScale", Strings.resourceCulture);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002F9E File Offset: 0x0000119E
		internal static string ADP_InvalidMaxRecords
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMaxRecords", Strings.resourceCulture);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002FB4 File Offset: 0x000011B4
		internal static string ADP_InvalidMetaDataValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMetaDataValue", Strings.resourceCulture);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002FCA File Offset: 0x000011CA
		internal static string ADP_InvalidMinMaxPoolSizeValues
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMinMaxPoolSizeValues", Strings.resourceCulture);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002FE0 File Offset: 0x000011E0
		internal static string ADP_InvalidMixedUsageOfAccessTokenAndAuthentication
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMixedUsageOfAccessTokenAndAuthentication", Strings.resourceCulture);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002FF6 File Offset: 0x000011F6
		internal static string ADP_InvalidMixedUsageOfAccessTokenAndContextConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMixedUsageOfAccessTokenAndContextConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000300C File Offset: 0x0000120C
		internal static string ADP_InvalidMixedUsageOfAccessTokenAndCredential
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMixedUsageOfAccessTokenAndCredential", Strings.resourceCulture);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003022 File Offset: 0x00001222
		internal static string ADP_InvalidMixedUsageOfAccessTokenAndIntegratedSecurity
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMixedUsageOfAccessTokenAndIntegratedSecurity", Strings.resourceCulture);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003038 File Offset: 0x00001238
		internal static string ADP_InvalidMixedUsageOfAccessTokenAndUserIDPassword
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMixedUsageOfAccessTokenAndUserIDPassword", Strings.resourceCulture);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000304E File Offset: 0x0000124E
		internal static string ADP_InvalidMixedUsageOfCredentialAndAccessToken
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMixedUsageOfCredentialAndAccessToken", Strings.resourceCulture);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003064 File Offset: 0x00001264
		internal static string ADP_InvalidMixedUsageOfSecureAndClearCredential
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMixedUsageOfSecureAndClearCredential", Strings.resourceCulture);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000307A File Offset: 0x0000127A
		internal static string ADP_InvalidMixedUsageOfSecureCredentialAndContextConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMixedUsageOfSecureCredentialAndContextConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003090 File Offset: 0x00001290
		internal static string ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity", Strings.resourceCulture);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000030A6 File Offset: 0x000012A6
		internal static string ADP_InvalidMultipartName
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMultipartName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000030BC File Offset: 0x000012BC
		internal static string ADP_InvalidMultipartNameQuoteUsage
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMultipartNameQuoteUsage", Strings.resourceCulture);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000030D2 File Offset: 0x000012D2
		internal static string ADP_InvalidMultipartNameToManyParts
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidMultipartNameToManyParts", Strings.resourceCulture);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000030E8 File Offset: 0x000012E8
		internal static string ADP_InvalidOffsetValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidOffsetValue", Strings.resourceCulture);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000030FE File Offset: 0x000012FE
		internal static string ADP_InvalidPrefixSuffix
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidPrefixSuffix", Strings.resourceCulture);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003114 File Offset: 0x00001314
		internal static string ADP_InvalidSeekOrigin
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidSeekOrigin", Strings.resourceCulture);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000312A File Offset: 0x0000132A
		internal static string ADP_InvalidSizeValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidSizeValue", Strings.resourceCulture);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003140 File Offset: 0x00001340
		internal static string ADP_InvalidSourceBufferIndex
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidSourceBufferIndex", Strings.resourceCulture);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003156 File Offset: 0x00001356
		internal static string ADP_InvalidSourceColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidSourceColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000316C File Offset: 0x0000136C
		internal static string ADP_InvalidSourceTable
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidSourceTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003182 File Offset: 0x00001382
		internal static string ADP_InvalidStartRecord
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidStartRecord", Strings.resourceCulture);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003198 File Offset: 0x00001398
		internal static string ADP_InvalidUDL
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidUDL", Strings.resourceCulture);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000031AE File Offset: 0x000013AE
		internal static string ADP_InvalidValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000031C4 File Offset: 0x000013C4
		internal static string ADP_InvalidXMLBadVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_InvalidXMLBadVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000031DA File Offset: 0x000013DA
		internal static string ADP_KeywordNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_KeywordNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000031F0 File Offset: 0x000013F0
		internal static string ADP_LiteralValueIsInvalid
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_LiteralValueIsInvalid", Strings.resourceCulture);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003206 File Offset: 0x00001406
		internal static string ADP_LocalTransactionPresent
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_LocalTransactionPresent", Strings.resourceCulture);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000321C File Offset: 0x0000141C
		internal static string ADP_MismatchedAsyncResult
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MismatchedAsyncResult", Strings.resourceCulture);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003232 File Offset: 0x00001432
		internal static string ADP_MissingColumnMapping
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MissingColumnMapping", Strings.resourceCulture);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003248 File Offset: 0x00001448
		internal static string ADP_MissingConnectionOptionValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MissingConnectionOptionValue", Strings.resourceCulture);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000325E File Offset: 0x0000145E
		internal static string ADP_MissingDataReaderFieldType
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MissingDataReaderFieldType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003274 File Offset: 0x00001474
		internal static string ADP_MissingSelectCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MissingSelectCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000328A File Offset: 0x0000148A
		internal static string ADP_MissingSourceCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MissingSourceCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000032A0 File Offset: 0x000014A0
		internal static string ADP_MissingSourceCommandConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MissingSourceCommandConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000032B6 File Offset: 0x000014B6
		internal static string ADP_MissingTableMapping
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MissingTableMapping", Strings.resourceCulture);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000032CC File Offset: 0x000014CC
		internal static string ADP_MissingTableMappingDestination
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MissingTableMappingDestination", Strings.resourceCulture);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000032E2 File Offset: 0x000014E2
		internal static string ADP_MissingTableSchema
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MissingTableSchema", Strings.resourceCulture);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000032F8 File Offset: 0x000014F8
		internal static string ADP_MultipleReturnValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MultipleReturnValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000330E File Offset: 0x0000150E
		internal static string ADP_MustBeReadOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_MustBeReadOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003324 File Offset: 0x00001524
		internal static string ADP_NegativeParameter
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NegativeParameter", Strings.resourceCulture);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000333A File Offset: 0x0000153A
		internal static string ADP_NoConnectionString
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NoConnectionString", Strings.resourceCulture);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003350 File Offset: 0x00001550
		internal static string ADP_NonCLSException
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NonCLSException", Strings.resourceCulture);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003366 File Offset: 0x00001566
		internal static string ADP_NonPooledOpenTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NonPooledOpenTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000337C File Offset: 0x0000157C
		internal static string ADP_NonSeqByteAccess
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NonSeqByteAccess", Strings.resourceCulture);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003392 File Offset: 0x00001592
		internal static string ADP_NonSequentialColumnAccess
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NonSequentialColumnAccess", Strings.resourceCulture);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000033A8 File Offset: 0x000015A8
		internal static string ADP_NoQuoteChange
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NoQuoteChange", Strings.resourceCulture);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000033BE File Offset: 0x000015BE
		internal static string ADP_NoStoredProcedureExists
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NoStoredProcedureExists", Strings.resourceCulture);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000033D4 File Offset: 0x000015D4
		internal static string ADP_NotAPermissionElement
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NotAPermissionElement", Strings.resourceCulture);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000033EA File Offset: 0x000015EA
		internal static string ADP_NotRowType
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NotRowType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003400 File Offset: 0x00001600
		internal static string ADP_NotSupportedEnumerationValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NotSupportedEnumerationValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003416 File Offset: 0x00001616
		internal static string ADP_NullDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NullDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000342C File Offset: 0x0000162C
		internal static string ADP_NullDataTable
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NullDataTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003442 File Offset: 0x00001642
		internal static string ADP_NumericToDecimalOverflow
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_NumericToDecimalOverflow", Strings.resourceCulture);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003458 File Offset: 0x00001658
		internal static string ADP_ObsoleteKeyword
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ObsoleteKeyword", Strings.resourceCulture);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000346E File Offset: 0x0000166E
		internal static string ADP_OdbcNoTypesFromProvider
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OdbcNoTypesFromProvider", Strings.resourceCulture);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003484 File Offset: 0x00001684
		internal static string ADP_OffsetOutOfRangeException
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OffsetOutOfRangeException", Strings.resourceCulture);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000349A File Offset: 0x0000169A
		internal static string ADP_OnlyOneTableForStartRecordOrMaxRecords
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OnlyOneTableForStartRecordOrMaxRecords", Strings.resourceCulture);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000034B0 File Offset: 0x000016B0
		internal static string ADP_OpenConnectionPropertySet
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OpenConnectionPropertySet", Strings.resourceCulture);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000034C6 File Offset: 0x000016C6
		internal static string ADP_OpenConnectionRequired
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OpenConnectionRequired", Strings.resourceCulture);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000034DC File Offset: 0x000016DC
		internal static string ADP_OpenConnectionRequired_Clone
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OpenConnectionRequired_Clone", Strings.resourceCulture);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000034F2 File Offset: 0x000016F2
		internal static string ADP_OpenConnectionRequired_Delete
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OpenConnectionRequired_Delete", Strings.resourceCulture);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003508 File Offset: 0x00001708
		internal static string ADP_OpenConnectionRequired_Insert
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OpenConnectionRequired_Insert", Strings.resourceCulture);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000351E File Offset: 0x0000171E
		internal static string ADP_OpenConnectionRequired_Update
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OpenConnectionRequired_Update", Strings.resourceCulture);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003534 File Offset: 0x00001734
		internal static string ADP_OpenReaderExists
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OpenReaderExists", Strings.resourceCulture);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000354A File Offset: 0x0000174A
		internal static string ADP_OpenResultSetExists
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OpenResultSetExists", Strings.resourceCulture);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003560 File Offset: 0x00001760
		internal static string ADP_OperationAborted
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OperationAborted", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003576 File Offset: 0x00001776
		internal static string ADP_OperationAbortedExceptionMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_OperationAbortedExceptionMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000358C File Offset: 0x0000178C
		internal static string ADP_ParallelTransactionsNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ParallelTransactionsNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000035A2 File Offset: 0x000017A2
		internal static string ADP_ParameterConversionFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ParameterConversionFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000035B8 File Offset: 0x000017B8
		internal static string ADP_ParameterValueOutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ParameterValueOutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000035CE File Offset: 0x000017CE
		internal static string ADP_PendingAsyncOperation
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_PendingAsyncOperation", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000035E4 File Offset: 0x000017E4
		internal static string ADP_PermissionTypeMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_PermissionTypeMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000035FA File Offset: 0x000017FA
		internal static string ADP_PooledOpenTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_PooledOpenTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003610 File Offset: 0x00001810
		internal static string ADP_PrepareParameterScale
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_PrepareParameterScale", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003626 File Offset: 0x00001826
		internal static string ADP_PrepareParameterSize
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_PrepareParameterSize", Strings.resourceCulture);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000363C File Offset: 0x0000183C
		internal static string ADP_PrepareParameterType
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_PrepareParameterType", Strings.resourceCulture);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003652 File Offset: 0x00001852
		internal static string ADP_PropertyNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_PropertyNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003668 File Offset: 0x00001868
		internal static string ADP_QuotePrefixNotSet
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_QuotePrefixNotSet", Strings.resourceCulture);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000367E File Offset: 0x0000187E
		internal static string ADP_ResultsNotAllowedDuringBatch
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_ResultsNotAllowedDuringBatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003694 File Offset: 0x00001894
		internal static string ADP_RowUpdatedErrors
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_RowUpdatedErrors", Strings.resourceCulture);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000036AA File Offset: 0x000018AA
		internal static string ADP_RowUpdatingErrors
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_RowUpdatingErrors", Strings.resourceCulture);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000036C0 File Offset: 0x000018C0
		internal static string ADP_SingleValuedProperty
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_SingleValuedProperty", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000036D6 File Offset: 0x000018D6
		internal static string ADP_StreamClosed
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_StreamClosed", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000036EC File Offset: 0x000018EC
		internal static string ADP_TransactionCompleted
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_TransactionCompleted", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003702 File Offset: 0x00001902
		internal static string ADP_TransactionCompletedButNotDisposed
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_TransactionCompletedButNotDisposed", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003718 File Offset: 0x00001918
		internal static string ADP_TransactionConnectionMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_TransactionConnectionMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000372E File Offset: 0x0000192E
		internal static string ADP_TransactionPresent
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_TransactionPresent", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003744 File Offset: 0x00001944
		internal static string ADP_TransactionRequired
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_TransactionRequired", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000375A File Offset: 0x0000195A
		internal static string ADP_TransactionZombied
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_TransactionZombied", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003770 File Offset: 0x00001970
		internal static string ADP_UdlFileError
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UdlFileError", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003786 File Offset: 0x00001986
		internal static string ADP_UnableToCreateBooleanLiteral
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UnableToCreateBooleanLiteral", Strings.resourceCulture);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000379C File Offset: 0x0000199C
		internal static string ADP_UninitializedParameterSize
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UninitializedParameterSize", Strings.resourceCulture);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000037B2 File Offset: 0x000019B2
		internal static string ADP_UnknownDataType
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UnknownDataType", Strings.resourceCulture);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000037C8 File Offset: 0x000019C8
		internal static string ADP_UnknownDataTypeCode
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UnknownDataTypeCode", Strings.resourceCulture);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000037DE File Offset: 0x000019DE
		internal static string ADP_UnsupportedNativeDataTypeOleDb
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UnsupportedNativeDataTypeOleDb", Strings.resourceCulture);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000037F4 File Offset: 0x000019F4
		internal static string ADP_UnwantedStatementType
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UnwantedStatementType", Strings.resourceCulture);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000380A File Offset: 0x00001A0A
		internal static string ADP_UpdateConcurrencyViolation_Batch
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateConcurrencyViolation_Batch", Strings.resourceCulture);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003820 File Offset: 0x00001A20
		internal static string ADP_UpdateConcurrencyViolation_Delete
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateConcurrencyViolation_Delete", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003836 File Offset: 0x00001A36
		internal static string ADP_UpdateConcurrencyViolation_Update
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateConcurrencyViolation_Update", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000384C File Offset: 0x00001A4C
		internal static string ADP_UpdateMismatchRowTable
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateMismatchRowTable", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003862 File Offset: 0x00001A62
		internal static string ADP_UpdateRequiresCommandClone
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateRequiresCommandClone", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003878 File Offset: 0x00001A78
		internal static string ADP_UpdateRequiresCommandDelete
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateRequiresCommandDelete", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000388E File Offset: 0x00001A8E
		internal static string ADP_UpdateRequiresCommandInsert
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateRequiresCommandInsert", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000038A4 File Offset: 0x00001AA4
		internal static string ADP_UpdateRequiresCommandSelect
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateRequiresCommandSelect", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000038BA File Offset: 0x00001ABA
		internal static string ADP_UpdateRequiresCommandUpdate
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateRequiresCommandUpdate", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000038D0 File Offset: 0x00001AD0
		internal static string ADP_UpdateRequiresSourceTable
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateRequiresSourceTable", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000038E6 File Offset: 0x00001AE6
		internal static string ADP_UpdateRequiresSourceTableName
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_UpdateRequiresSourceTableName", Strings.resourceCulture);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000038FC File Offset: 0x00001AFC
		internal static string ADP_VersionDoesNotSupportDataType
		{
			get
			{
				return Strings.ResourceManager.GetString("ADP_VersionDoesNotSupportDataType", Strings.resourceCulture);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003912 File Offset: 0x00001B12
		internal static string Arg_ArrayPlusOffTooSmall
		{
			get
			{
				return Strings.ResourceManager.GetString("Arg_ArrayPlusOffTooSmall", Strings.resourceCulture);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003928 File Offset: 0x00001B28
		internal static string Arg_RankMultiDimNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("Arg_RankMultiDimNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000393E File Offset: 0x00001B3E
		internal static string Arg_RemoveArgNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("Arg_RemoveArgNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003954 File Offset: 0x00001B54
		internal static string ArgumentOutOfRange_NeedNonNegNum
		{
			get
			{
				return Strings.ResourceManager.GetString("ArgumentOutOfRange_NeedNonNegNum", Strings.resourceCulture);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000396A File Offset: 0x00001B6A
		internal static string AttestationTokenSignatureValidationFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("AttestationTokenSignatureValidationFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003980 File Offset: 0x00001B80
		internal static string AZURESQL_ChinaEndpoint
		{
			get
			{
				return Strings.ResourceManager.GetString("AZURESQL_ChinaEndpoint", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003996 File Offset: 0x00001B96
		internal static string AZURESQL_GenericEndpoint
		{
			get
			{
				return Strings.ResourceManager.GetString("AZURESQL_GenericEndpoint", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000039AC File Offset: 0x00001BAC
		internal static string AZURESQL_GermanEndpoint
		{
			get
			{
				return Strings.ResourceManager.GetString("AZURESQL_GermanEndpoint", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000039C2 File Offset: 0x00001BC2
		internal static string AZURESQL_UsGovEndpoint
		{
			get
			{
				return Strings.ResourceManager.GetString("AZURESQL_UsGovEndpoint", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000039D8 File Offset: 0x00001BD8
		internal static string CodeGen_DuplicateTableName
		{
			get
			{
				return Strings.ResourceManager.GetString("CodeGen_DuplicateTableName", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000039EE File Offset: 0x00001BEE
		internal static string CodeGen_InvalidIdentifier
		{
			get
			{
				return Strings.ResourceManager.GetString("CodeGen_InvalidIdentifier", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003A04 File Offset: 0x00001C04
		internal static string CodeGen_NoCtor0
		{
			get
			{
				return Strings.ResourceManager.GetString("CodeGen_NoCtor0", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003A1A File Offset: 0x00001C1A
		internal static string CodeGen_NoCtor1
		{
			get
			{
				return Strings.ResourceManager.GetString("CodeGen_NoCtor1", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003A30 File Offset: 0x00001C30
		internal static string CodeGen_TypeCantBeNull
		{
			get
			{
				return Strings.ResourceManager.GetString("CodeGen_TypeCantBeNull", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003A46 File Offset: 0x00001C46
		internal static string collectionChangedEventDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("collectionChangedEventDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003A5C File Offset: 0x00001C5C
		internal static string ConfigBaseElementsOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigBaseElementsOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003A72 File Offset: 0x00001C72
		internal static string ConfigBaseNoChildNodes
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigBaseNoChildNodes", Strings.resourceCulture);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003A88 File Offset: 0x00001C88
		internal static string ConfigProviderInvalid
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigProviderInvalid", Strings.resourceCulture);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003A9E File Offset: 0x00001C9E
		internal static string ConfigProviderMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigProviderMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003AB4 File Offset: 0x00001CB4
		internal static string ConfigProviderNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigProviderNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003ACA File Offset: 0x00001CCA
		internal static string ConfigProviderNotInstalled
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigProviderNotInstalled", Strings.resourceCulture);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003AE0 File Offset: 0x00001CE0
		internal static string ConfigRequiredAttributeEmpty
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigRequiredAttributeEmpty", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003AF6 File Offset: 0x00001CF6
		internal static string ConfigRequiredAttributeMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigRequiredAttributeMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003B0C File Offset: 0x00001D0C
		internal static string ConfigSectionsUnique
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigSectionsUnique", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003B22 File Offset: 0x00001D22
		internal static string ConfigUnrecognizedAttributes
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigUnrecognizedAttributes", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003B38 File Offset: 0x00001D38
		internal static string ConfigUnrecognizedElement
		{
			get
			{
				return Strings.ResourceManager.GetString("ConfigUnrecognizedElement", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003B4E File Offset: 0x00001D4E
		internal static string ConstraintNameDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("ConstraintNameDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003B64 File Offset: 0x00001D64
		internal static string ConstraintTableDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("ConstraintTableDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003B7A File Offset: 0x00001D7A
		internal static string Data_ArgumentContainsNull
		{
			get
			{
				return Strings.ResourceManager.GetString("Data_ArgumentContainsNull", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003B90 File Offset: 0x00001D90
		internal static string Data_ArgumentNull
		{
			get
			{
				return Strings.ResourceManager.GetString("Data_ArgumentNull", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003BA6 File Offset: 0x00001DA6
		internal static string Data_ArgumentOutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("Data_ArgumentOutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003BBC File Offset: 0x00001DBC
		internal static string Data_CannotModifyCollection
		{
			get
			{
				return Strings.ResourceManager.GetString("Data_CannotModifyCollection", Strings.resourceCulture);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003BD2 File Offset: 0x00001DD2
		internal static string Data_CaseInsensitiveNameConflict
		{
			get
			{
				return Strings.ResourceManager.GetString("Data_CaseInsensitiveNameConflict", Strings.resourceCulture);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003BE8 File Offset: 0x00001DE8
		internal static string Data_EnforceConstraints
		{
			get
			{
				return Strings.ResourceManager.GetString("Data_EnforceConstraints", Strings.resourceCulture);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003BFE File Offset: 0x00001DFE
		internal static string Data_InvalidOffsetLength
		{
			get
			{
				return Strings.ResourceManager.GetString("Data_InvalidOffsetLength", Strings.resourceCulture);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003C14 File Offset: 0x00001E14
		internal static string Data_NamespaceNameConflict
		{
			get
			{
				return Strings.ResourceManager.GetString("Data_NamespaceNameConflict", Strings.resourceCulture);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003C2A File Offset: 0x00001E2A
		internal static string DataAdapter_AcceptChangesDuringFill
		{
			get
			{
				return Strings.ResourceManager.GetString("DataAdapter_AcceptChangesDuringFill", Strings.resourceCulture);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003C40 File Offset: 0x00001E40
		internal static string DataAdapter_AcceptChangesDuringUpdate
		{
			get
			{
				return Strings.ResourceManager.GetString("DataAdapter_AcceptChangesDuringUpdate", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003C56 File Offset: 0x00001E56
		internal static string DataAdapter_ContinueUpdateOnError
		{
			get
			{
				return Strings.ResourceManager.GetString("DataAdapter_ContinueUpdateOnError", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003C6C File Offset: 0x00001E6C
		internal static string DataAdapter_FillError
		{
			get
			{
				return Strings.ResourceManager.GetString("DataAdapter_FillError", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003C82 File Offset: 0x00001E82
		internal static string DataAdapter_FillLoadOption
		{
			get
			{
				return Strings.ResourceManager.GetString("DataAdapter_FillLoadOption", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003C98 File Offset: 0x00001E98
		internal static string DataAdapter_MissingMappingAction
		{
			get
			{
				return Strings.ResourceManager.GetString("DataAdapter_MissingMappingAction", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00003CAE File Offset: 0x00001EAE
		internal static string DataAdapter_MissingSchemaAction
		{
			get
			{
				return Strings.ResourceManager.GetString("DataAdapter_MissingSchemaAction", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003CC4 File Offset: 0x00001EC4
		internal static string DataAdapter_ReturnProviderSpecificTypes
		{
			get
			{
				return Strings.ResourceManager.GetString("DataAdapter_ReturnProviderSpecificTypes", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00003CDA File Offset: 0x00001EDA
		internal static string DataAdapter_TableMappings
		{
			get
			{
				return Strings.ResourceManager.GetString("DataAdapter_TableMappings", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003CF0 File Offset: 0x00001EF0
		internal static string DataCategory_Action
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Action", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003D06 File Offset: 0x00001F06
		internal static string DataCategory_Advanced
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Advanced", Strings.resourceCulture);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003D1C File Offset: 0x00001F1C
		internal static string DataCategory_Behavior
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Behavior", Strings.resourceCulture);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003D32 File Offset: 0x00001F32
		internal static string DataCategory_ConnectionResilency
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_ConnectionResilency", Strings.resourceCulture);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003D48 File Offset: 0x00001F48
		internal static string DataCategory_Context
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Context", Strings.resourceCulture);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003D5E File Offset: 0x00001F5E
		internal static string DataCategory_Data
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Data", Strings.resourceCulture);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003D74 File Offset: 0x00001F74
		internal static string DataCategory_Fill
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Fill", Strings.resourceCulture);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00003D8A File Offset: 0x00001F8A
		internal static string DataCategory_InfoMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_InfoMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003DA0 File Offset: 0x00001FA0
		internal static string DataCategory_Initialization
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Initialization", Strings.resourceCulture);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003DB6 File Offset: 0x00001FB6
		internal static string DataCategory_Mapping
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Mapping", Strings.resourceCulture);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003DCC File Offset: 0x00001FCC
		internal static string DataCategory_NamedConnectionString
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_NamedConnectionString", Strings.resourceCulture);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00003DE2 File Offset: 0x00001FE2
		internal static string DataCategory_Notification
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Notification", Strings.resourceCulture);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003DF8 File Offset: 0x00001FF8
		internal static string DataCategory_Pooling
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Pooling", Strings.resourceCulture);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00003E0E File Offset: 0x0000200E
		internal static string DataCategory_Replication
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Replication", Strings.resourceCulture);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003E24 File Offset: 0x00002024
		internal static string DataCategory_Schema
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Schema", Strings.resourceCulture);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00003E3A File Offset: 0x0000203A
		internal static string DataCategory_Security
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Security", Strings.resourceCulture);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003E50 File Offset: 0x00002050
		internal static string DataCategory_Source
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Source", Strings.resourceCulture);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00003E66 File Offset: 0x00002066
		internal static string DataCategory_StateChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_StateChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003E7C File Offset: 0x0000207C
		internal static string DataCategory_StatementCompleted
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_StatementCompleted", Strings.resourceCulture);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00003E92 File Offset: 0x00002092
		internal static string DataCategory_Udt
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Udt", Strings.resourceCulture);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003EA8 File Offset: 0x000020A8
		internal static string DataCategory_Update
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Update", Strings.resourceCulture);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00003EBE File Offset: 0x000020BE
		internal static string DataCategory_Xml
		{
			get
			{
				return Strings.ResourceManager.GetString("DataCategory_Xml", Strings.resourceCulture);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003ED4 File Offset: 0x000020D4
		internal static string DataColumn_AutoIncrementAndDefaultValue
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_AutoIncrementAndDefaultValue", Strings.resourceCulture);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003EEA File Offset: 0x000020EA
		internal static string DataColumn_AutoIncrementAndExpression
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_AutoIncrementAndExpression", Strings.resourceCulture);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003F00 File Offset: 0x00002100
		internal static string DataColumn_AutoIncrementCannotSetIfHasData
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_AutoIncrementCannotSetIfHasData", Strings.resourceCulture);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003F16 File Offset: 0x00002116
		internal static string DataColumn_AutoIncrementSeed
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_AutoIncrementSeed", Strings.resourceCulture);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003F2C File Offset: 0x0000212C
		internal static string DataColumn_CannotChangeNamespace
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_CannotChangeNamespace", Strings.resourceCulture);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003F42 File Offset: 0x00002142
		internal static string DataColumn_CannotSetDateTimeModeForNonDateTimeColumns
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_CannotSetDateTimeModeForNonDateTimeColumns", Strings.resourceCulture);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00003F58 File Offset: 0x00002158
		internal static string DataColumn_CannotSetMaxLength
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_CannotSetMaxLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00003F6E File Offset: 0x0000216E
		internal static string DataColumn_CannotSetMaxLength2
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_CannotSetMaxLength2", Strings.resourceCulture);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00003F84 File Offset: 0x00002184
		internal static string DataColumn_CannotSetToNull
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_CannotSetToNull", Strings.resourceCulture);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00003F9A File Offset: 0x0000219A
		internal static string DataColumn_CannotSimpleContent
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_CannotSimpleContent", Strings.resourceCulture);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003FB0 File Offset: 0x000021B0
		internal static string DataColumn_CannotSimpleContentType
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_CannotSimpleContentType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00003FC6 File Offset: 0x000021C6
		internal static string DataColumn_ChangeDataType
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_ChangeDataType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003FDC File Offset: 0x000021DC
		internal static string DataColumn_DateTimeMode
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_DateTimeMode", Strings.resourceCulture);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00003FF2 File Offset: 0x000021F2
		internal static string DataColumn_DefaultValueAndAutoIncrement
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_DefaultValueAndAutoIncrement", Strings.resourceCulture);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004008 File Offset: 0x00002208
		internal static string DataColumn_DefaultValueColumnDataType
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_DefaultValueColumnDataType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000401E File Offset: 0x0000221E
		internal static string DataColumn_DefaultValueDataType
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_DefaultValueDataType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004034 File Offset: 0x00002234
		internal static string DataColumn_DefaultValueDataType1
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_DefaultValueDataType1", Strings.resourceCulture);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000404A File Offset: 0x0000224A
		internal static string DataColumn_ExceedMaxLength
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_ExceedMaxLength", Strings.resourceCulture);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004060 File Offset: 0x00002260
		internal static string DataColumn_ExpressionAndConstraint
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_ExpressionAndConstraint", Strings.resourceCulture);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004076 File Offset: 0x00002276
		internal static string DataColumn_ExpressionAndReadOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_ExpressionAndReadOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000408C File Offset: 0x0000228C
		internal static string DataColumn_ExpressionAndUnique
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_ExpressionAndUnique", Strings.resourceCulture);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000040A2 File Offset: 0x000022A2
		internal static string DataColumn_ExpressionCircular
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_ExpressionCircular", Strings.resourceCulture);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000040B8 File Offset: 0x000022B8
		internal static string DataColumn_ExpressionInConstraint
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_ExpressionInConstraint", Strings.resourceCulture);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000040CE File Offset: 0x000022CE
		internal static string DataColumn_HasToBeStringType
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_HasToBeStringType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000040E4 File Offset: 0x000022E4
		internal static string DataColumn_INullableUDTwithoutStaticNull
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_INullableUDTwithoutStaticNull", Strings.resourceCulture);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000040FA File Offset: 0x000022FA
		internal static string DataColumn_InvalidDataColumnMapping
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_InvalidDataColumnMapping", Strings.resourceCulture);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004110 File Offset: 0x00002310
		internal static string DataColumn_InvalidDateTimeMode
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_InvalidDateTimeMode", Strings.resourceCulture);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004126 File Offset: 0x00002326
		internal static string DataColumn_LongerThanMaxLength
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_LongerThanMaxLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000413C File Offset: 0x0000233C
		internal static string DataColumn_NameRequired
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NameRequired", Strings.resourceCulture);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004152 File Offset: 0x00002352
		internal static string DataColumn_NonUniqueValues
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NonUniqueValues", Strings.resourceCulture);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004168 File Offset: 0x00002368
		internal static string DataColumn_NotAllowDBNull
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NotAllowDBNull", Strings.resourceCulture);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000417E File Offset: 0x0000237E
		internal static string DataColumn_NotInAnyTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NotInAnyTable", Strings.resourceCulture);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004194 File Offset: 0x00002394
		internal static string DataColumn_NotInTheTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NotInTheTable", Strings.resourceCulture);
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000041AA File Offset: 0x000023AA
		internal static string DataColumn_NotInTheUnderlyingTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NotInTheUnderlyingTable", Strings.resourceCulture);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000041C0 File Offset: 0x000023C0
		internal static string DataColumn_NullableTypesNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NullableTypesNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000041D6 File Offset: 0x000023D6
		internal static string DataColumn_NullDataType
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NullDataType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000041EC File Offset: 0x000023EC
		internal static string DataColumn_NullKeyValues
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NullKeyValues", Strings.resourceCulture);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004202 File Offset: 0x00002402
		internal static string DataColumn_NullValues
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_NullValues", Strings.resourceCulture);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004218 File Offset: 0x00002418
		internal static string DataColumn_OrdinalExceedMaximun
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_OrdinalExceedMaximun", Strings.resourceCulture);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000422E File Offset: 0x0000242E
		internal static string DataColumn_ReadOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_ReadOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004244 File Offset: 0x00002444
		internal static string DataColumn_ReadOnlyAndExpression
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_ReadOnlyAndExpression", Strings.resourceCulture);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000425A File Offset: 0x0000245A
		internal static string DataColumn_SetAddedAndModifiedCalledOnNonUnchanged
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_SetAddedAndModifiedCalledOnNonUnchanged", Strings.resourceCulture);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004270 File Offset: 0x00002470
		internal static string DataColumn_SetFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_SetFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004286 File Offset: 0x00002486
		internal static string DataColumn_UDTImplementsIChangeTrackingButnotIRevertible
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_UDTImplementsIChangeTrackingButnotIRevertible", Strings.resourceCulture);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000429C File Offset: 0x0000249C
		internal static string DataColumn_UniqueAndExpression
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumn_UniqueAndExpression", Strings.resourceCulture);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000042B2 File Offset: 0x000024B2
		internal static string DataColumnAllowNullDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnAllowNullDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000042C8 File Offset: 0x000024C8
		internal static string DataColumnAutoIncrementDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnAutoIncrementDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000042DE File Offset: 0x000024DE
		internal static string DataColumnAutoIncrementSeedDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnAutoIncrementSeedDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000042F4 File Offset: 0x000024F4
		internal static string DataColumnAutoIncrementStepDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnAutoIncrementStepDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000430A File Offset: 0x0000250A
		internal static string DataColumnCaptionDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnCaptionDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004320 File Offset: 0x00002520
		internal static string DataColumnColumnNameDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnColumnNameDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00004336 File Offset: 0x00002536
		internal static string DataColumnDataTableDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnDataTableDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000434C File Offset: 0x0000254C
		internal static string DataColumnDataTypeDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnDataTypeDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004362 File Offset: 0x00002562
		internal static string DataColumnDateTimeModeDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnDateTimeModeDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004378 File Offset: 0x00002578
		internal static string DataColumnDefaultValueDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnDefaultValueDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000438E File Offset: 0x0000258E
		internal static string DataColumnExpressionDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnExpressionDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000043A4 File Offset: 0x000025A4
		internal static string DataColumnMapping_DataSetColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnMapping_DataSetColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000043BA File Offset: 0x000025BA
		internal static string DataColumnMapping_SourceColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnMapping_SourceColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000043D0 File Offset: 0x000025D0
		internal static string DataColumnMappingDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnMappingDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000043E6 File Offset: 0x000025E6
		internal static string DataColumnMappings_Count
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnMappings_Count", Strings.resourceCulture);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000043FC File Offset: 0x000025FC
		internal static string DataColumnMappings_Item
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnMappings_Item", Strings.resourceCulture);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004412 File Offset: 0x00002612
		internal static string DataColumnMaxLengthDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnMaxLengthDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004428 File Offset: 0x00002628
		internal static string DataColumnNamespaceDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnNamespaceDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000443E File Offset: 0x0000263E
		internal static string DataColumnOrdinalDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnOrdinalDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004454 File Offset: 0x00002654
		internal static string DataColumnPrefixDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnPrefixDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000446A File Offset: 0x0000266A
		internal static string DataColumnReadOnlyDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnReadOnlyDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004480 File Offset: 0x00002680
		internal static string DataColumns_Add1
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_Add1", Strings.resourceCulture);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004496 File Offset: 0x00002696
		internal static string DataColumns_Add2
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_Add2", Strings.resourceCulture);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000044AC File Offset: 0x000026AC
		internal static string DataColumns_Add3
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_Add3", Strings.resourceCulture);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000044C2 File Offset: 0x000026C2
		internal static string DataColumns_Add4
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_Add4", Strings.resourceCulture);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000044D8 File Offset: 0x000026D8
		internal static string DataColumns_AddDuplicate
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_AddDuplicate", Strings.resourceCulture);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000044EE File Offset: 0x000026EE
		internal static string DataColumns_AddDuplicate2
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_AddDuplicate2", Strings.resourceCulture);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00004504 File Offset: 0x00002704
		internal static string DataColumns_AddDuplicate3
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_AddDuplicate3", Strings.resourceCulture);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000451A File Offset: 0x0000271A
		internal static string DataColumns_OutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_OutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00004530 File Offset: 0x00002730
		internal static string DataColumns_Remove
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_Remove", Strings.resourceCulture);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00004546 File Offset: 0x00002746
		internal static string DataColumns_RemoveChildKey
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_RemoveChildKey", Strings.resourceCulture);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000455C File Offset: 0x0000275C
		internal static string DataColumns_RemoveConstraint
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_RemoveConstraint", Strings.resourceCulture);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00004572 File Offset: 0x00002772
		internal static string DataColumns_RemoveExpression
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_RemoveExpression", Strings.resourceCulture);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00004588 File Offset: 0x00002788
		internal static string DataColumns_RemovePrimaryKey
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumns_RemovePrimaryKey", Strings.resourceCulture);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000459E File Offset: 0x0000279E
		internal static string DataColumnUniqueDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataColumnUniqueDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000045B4 File Offset: 0x000027B4
		internal static string DataConstraint_AddFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_AddFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000045CA File Offset: 0x000027CA
		internal static string DataConstraint_AddPrimaryKeyConstraint
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_AddPrimaryKeyConstraint", Strings.resourceCulture);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000045E0 File Offset: 0x000027E0
		internal static string DataConstraint_BadObjectPropertyAccess
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_BadObjectPropertyAccess", Strings.resourceCulture);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000045F6 File Offset: 0x000027F6
		internal static string DataConstraint_CantAddConstraintToMultipleNestedTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_CantAddConstraintToMultipleNestedTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000460C File Offset: 0x0000280C
		internal static string DataConstraint_CascadeDelete
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_CascadeDelete", Strings.resourceCulture);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00004622 File Offset: 0x00002822
		internal static string DataConstraint_CascadeUpdate
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_CascadeUpdate", Strings.resourceCulture);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00004638 File Offset: 0x00002838
		internal static string DataConstraint_ClearParentTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_ClearParentTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000464E File Offset: 0x0000284E
		internal static string DataConstraint_Duplicate
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_Duplicate", Strings.resourceCulture);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00004664 File Offset: 0x00002864
		internal static string DataConstraint_DuplicateName
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_DuplicateName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000467A File Offset: 0x0000287A
		internal static string DataConstraint_ForeignKeyViolation
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_ForeignKeyViolation", Strings.resourceCulture);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00004690 File Offset: 0x00002890
		internal static string DataConstraint_ForeignTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_ForeignTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000046A6 File Offset: 0x000028A6
		internal static string DataConstraint_NeededForForeignKeyConstraint
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_NeededForForeignKeyConstraint", Strings.resourceCulture);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000046BC File Offset: 0x000028BC
		internal static string DataConstraint_NoName
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_NoName", Strings.resourceCulture);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000046D2 File Offset: 0x000028D2
		internal static string DataConstraint_NotInTheTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_NotInTheTable", Strings.resourceCulture);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000046E8 File Offset: 0x000028E8
		internal static string DataConstraint_OutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_OutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000046FE File Offset: 0x000028FE
		internal static string DataConstraint_ParentValues
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_ParentValues", Strings.resourceCulture);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00004714 File Offset: 0x00002914
		internal static string DataConstraint_RemoveFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_RemoveFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000472A File Offset: 0x0000292A
		internal static string DataConstraint_RemoveParentRow
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_RemoveParentRow", Strings.resourceCulture);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00004740 File Offset: 0x00002940
		internal static string DataConstraint_UniqueViolation
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_UniqueViolation", Strings.resourceCulture);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004756 File Offset: 0x00002956
		internal static string DataConstraint_Violation
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_Violation", Strings.resourceCulture);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000476C File Offset: 0x0000296C
		internal static string DataConstraint_ViolationValue
		{
			get
			{
				return Strings.ResourceManager.GetString("DataConstraint_ViolationValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00004782 File Offset: 0x00002982
		internal static string DataDom_CloneNode
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_CloneNode", Strings.resourceCulture);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00004798 File Offset: 0x00002998
		internal static string DataDom_ColumnMappingChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_ColumnMappingChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000047AE File Offset: 0x000029AE
		internal static string DataDom_ColumnNameChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_ColumnNameChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000047C4 File Offset: 0x000029C4
		internal static string DataDom_ColumnNamespaceChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_ColumnNamespaceChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000047DA File Offset: 0x000029DA
		internal static string DataDom_DataSetNameChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_DataSetNameChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000047F0 File Offset: 0x000029F0
		internal static string DataDom_DataSetNestedRelationsChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_DataSetNestedRelationsChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00004806 File Offset: 0x00002A06
		internal static string DataDom_DataSetNull
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_DataSetNull", Strings.resourceCulture);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000481C File Offset: 0x00002A1C
		internal static string DataDom_DataSetTablesChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_DataSetTablesChange", Strings.resourceCulture);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00004832 File Offset: 0x00002A32
		internal static string DataDom_EnforceConstraintsShouldBeOff
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_EnforceConstraintsShouldBeOff", Strings.resourceCulture);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004848 File Offset: 0x00002A48
		internal static string DataDom_Foliation
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_Foliation", Strings.resourceCulture);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000485E File Offset: 0x00002A5E
		internal static string DataDom_MultipleDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_MultipleDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00004874 File Offset: 0x00002A74
		internal static string DataDom_MultipleLoad
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_MultipleLoad", Strings.resourceCulture);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000488A File Offset: 0x00002A8A
		internal static string DataDom_NotSupport_Clear
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_NotSupport_Clear", Strings.resourceCulture);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000048A0 File Offset: 0x00002AA0
		internal static string DataDom_NotSupport_EntRef
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_NotSupport_EntRef", Strings.resourceCulture);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000048B6 File Offset: 0x00002AB6
		internal static string DataDom_NotSupport_GetElementById
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_NotSupport_GetElementById", Strings.resourceCulture);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000048CC File Offset: 0x00002ACC
		internal static string DataDom_TableColumnsChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_TableColumnsChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000048E2 File Offset: 0x00002AE2
		internal static string DataDom_TableNameChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_TableNameChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000048F8 File Offset: 0x00002AF8
		internal static string DataDom_TableNamespaceChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataDom_TableNamespaceChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000490E File Offset: 0x00002B0E
		internal static string DataIndex_FindWithoutSortOrder
		{
			get
			{
				return Strings.ResourceManager.GetString("DataIndex_FindWithoutSortOrder", Strings.resourceCulture);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00004924 File Offset: 0x00002B24
		internal static string DataIndex_KeyLength
		{
			get
			{
				return Strings.ResourceManager.GetString("DataIndex_KeyLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000493A File Offset: 0x00002B3A
		internal static string DataIndex_RecordStateRange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataIndex_RecordStateRange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00004950 File Offset: 0x00002B50
		internal static string DataKey_DuplicateColumns
		{
			get
			{
				return Strings.ResourceManager.GetString("DataKey_DuplicateColumns", Strings.resourceCulture);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00004966 File Offset: 0x00002B66
		internal static string DataKey_NoColumns
		{
			get
			{
				return Strings.ResourceManager.GetString("DataKey_NoColumns", Strings.resourceCulture);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000497C File Offset: 0x00002B7C
		internal static string DataKey_RemovePrimaryKey
		{
			get
			{
				return Strings.ResourceManager.GetString("DataKey_RemovePrimaryKey", Strings.resourceCulture);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00004992 File Offset: 0x00002B92
		internal static string DataKey_RemovePrimaryKey1
		{
			get
			{
				return Strings.ResourceManager.GetString("DataKey_RemovePrimaryKey1", Strings.resourceCulture);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000049A8 File Offset: 0x00002BA8
		internal static string DataKey_TableMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataKey_TableMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000049BE File Offset: 0x00002BBE
		internal static string DataKey_TooManyColumns
		{
			get
			{
				return Strings.ResourceManager.GetString("DataKey_TooManyColumns", Strings.resourceCulture);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000049D4 File Offset: 0x00002BD4
		internal static string DataMerge_DataTypeMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataMerge_DataTypeMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000049EA File Offset: 0x00002BEA
		internal static string DataMerge_MissingColumnDefinition
		{
			get
			{
				return Strings.ResourceManager.GetString("DataMerge_MissingColumnDefinition", Strings.resourceCulture);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00004A00 File Offset: 0x00002C00
		internal static string DataMerge_MissingConstraint
		{
			get
			{
				return Strings.ResourceManager.GetString("DataMerge_MissingConstraint", Strings.resourceCulture);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00004A16 File Offset: 0x00002C16
		internal static string DataMerge_MissingDefinition
		{
			get
			{
				return Strings.ResourceManager.GetString("DataMerge_MissingDefinition", Strings.resourceCulture);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00004A2C File Offset: 0x00002C2C
		internal static string DataMerge_MissingPrimaryKeyColumnInSource
		{
			get
			{
				return Strings.ResourceManager.GetString("DataMerge_MissingPrimaryKeyColumnInSource", Strings.resourceCulture);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00004A42 File Offset: 0x00002C42
		internal static string DataMerge_PrimaryKeyColumnsMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataMerge_PrimaryKeyColumnsMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00004A58 File Offset: 0x00002C58
		internal static string DataMerge_PrimaryKeyMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataMerge_PrimaryKeyMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00004A6E File Offset: 0x00002C6E
		internal static string DataMerge_ReltionKeyColumnsMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataMerge_ReltionKeyColumnsMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00004A84 File Offset: 0x00002C84
		internal static string DataRelation_AlreadyExists
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_AlreadyExists", Strings.resourceCulture);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00004A9A File Offset: 0x00002C9A
		internal static string DataRelation_AlreadyInOtherDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_AlreadyInOtherDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004AB0 File Offset: 0x00002CB0
		internal static string DataRelation_AlreadyInTheDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_AlreadyInTheDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004AC6 File Offset: 0x00002CC6
		internal static string DataRelation_CaseLocaleMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_CaseLocaleMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00004ADC File Offset: 0x00002CDC
		internal static string DataRelation_ChildTableMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_ChildTableMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00004AF2 File Offset: 0x00002CF2
		internal static string DataRelation_ColumnsTypeMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_ColumnsTypeMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00004B08 File Offset: 0x00002D08
		internal static string DataRelation_DataSetMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_DataSetMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00004B1E File Offset: 0x00002D1E
		internal static string DataRelation_DoesNotExist
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_DoesNotExist", Strings.resourceCulture);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00004B34 File Offset: 0x00002D34
		internal static string DataRelation_DuplicateName
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_DuplicateName", Strings.resourceCulture);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00004B4A File Offset: 0x00002D4A
		internal static string DataRelation_ForeignDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_ForeignDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00004B60 File Offset: 0x00002D60
		internal static string DataRelation_ForeignRow
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_ForeignRow", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00004B76 File Offset: 0x00002D76
		internal static string DataRelation_ForeignTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_ForeignTable", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00004B8C File Offset: 0x00002D8C
		internal static string DataRelation_GetParentRowTableMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_GetParentRowTableMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004BA2 File Offset: 0x00002DA2
		internal static string DataRelation_InValidNamespaceInNestedRelation
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_InValidNamespaceInNestedRelation", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00004BB8 File Offset: 0x00002DB8
		internal static string DataRelation_InValidNestedRelation
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_InValidNestedRelation", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00004BCE File Offset: 0x00002DCE
		internal static string DataRelation_KeyColumnsIdentical
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_KeyColumnsIdentical", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00004BE4 File Offset: 0x00002DE4
		internal static string DataRelation_KeyLengthMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_KeyLengthMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00004BFA File Offset: 0x00002DFA
		internal static string DataRelation_KeyZeroLength
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_KeyZeroLength", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00004C10 File Offset: 0x00002E10
		internal static string DataRelation_LoopInNestedRelations
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_LoopInNestedRelations", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00004C26 File Offset: 0x00002E26
		internal static string DataRelation_NoName
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_NoName", Strings.resourceCulture);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00004C3C File Offset: 0x00002E3C
		internal static string DataRelation_NotInTheDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_NotInTheDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004C52 File Offset: 0x00002E52
		internal static string DataRelation_OutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_OutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00004C68 File Offset: 0x00002E68
		internal static string DataRelation_ParentOrChildColumnsDoNotHaveDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_ParentOrChildColumnsDoNotHaveDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00004C7E File Offset: 0x00002E7E
		internal static string DataRelation_ParentTableMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_ParentTableMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00004C94 File Offset: 0x00002E94
		internal static string DataRelation_RelationNestedReadOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_RelationNestedReadOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00004CAA File Offset: 0x00002EAA
		internal static string DataRelation_SetParentRowTableMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_SetParentRowTableMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00004CC0 File Offset: 0x00002EC0
		internal static string DataRelation_TableCantBeNestedInTwoTables
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_TableCantBeNestedInTwoTables", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00004CD6 File Offset: 0x00002ED6
		internal static string DataRelation_TableNull
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_TableNull", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00004CEC File Offset: 0x00002EEC
		internal static string DataRelation_TablesInDifferentSets
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_TablesInDifferentSets", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00004D02 File Offset: 0x00002F02
		internal static string DataRelation_TableWasRemoved
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelation_TableWasRemoved", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00004D18 File Offset: 0x00002F18
		internal static string DataRelationChildColumnsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelationChildColumnsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004D2E File Offset: 0x00002F2E
		internal static string DataRelationNested
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelationNested", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00004D44 File Offset: 0x00002F44
		internal static string DataRelationParentColumnsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelationParentColumnsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00004D5A File Offset: 0x00002F5A
		internal static string DataRelationRelationNameDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRelationRelationNameDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00004D70 File Offset: 0x00002F70
		internal static string DataRow_AlreadyDeleted
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_AlreadyDeleted", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00004D86 File Offset: 0x00002F86
		internal static string DataRow_AlreadyInOtherCollection
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_AlreadyInOtherCollection", Strings.resourceCulture);
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00004D9C File Offset: 0x00002F9C
		internal static string DataRow_AlreadyInTheCollection
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_AlreadyInTheCollection", Strings.resourceCulture);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00004DB2 File Offset: 0x00002FB2
		internal static string DataRow_AlreadyRemoved
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_AlreadyRemoved", Strings.resourceCulture);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00004DC8 File Offset: 0x00002FC8
		internal static string DataRow_BeginEditInRowChanging
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_BeginEditInRowChanging", Strings.resourceCulture);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00004DDE File Offset: 0x00002FDE
		internal static string DataRow_CancelEditInRowChanging
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_CancelEditInRowChanging", Strings.resourceCulture);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00004DF4 File Offset: 0x00002FF4
		internal static string DataRow_DeletedRowInaccessible
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_DeletedRowInaccessible", Strings.resourceCulture);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00004E0A File Offset: 0x0000300A
		internal static string DataRow_DeleteInRowDeleting
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_DeleteInRowDeleting", Strings.resourceCulture);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00004E20 File Offset: 0x00003020
		internal static string DataRow_EditInRowChanging
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_EditInRowChanging", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00004E36 File Offset: 0x00003036
		internal static string DataRow_Empty
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_Empty", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00004E4C File Offset: 0x0000304C
		internal static string DataRow_EndEditInRowChanging
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_EndEditInRowChanging", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004E62 File Offset: 0x00003062
		internal static string DataRow_InvalidRowBitPattern
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_InvalidRowBitPattern", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00004E78 File Offset: 0x00003078
		internal static string DataRow_InvalidVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_InvalidVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00004E8E File Offset: 0x0000308E
		internal static string DataRow_MultipleParents
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_MultipleParents", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00004EA4 File Offset: 0x000030A4
		internal static string DataRow_NoCurrentData
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_NoCurrentData", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00004EBA File Offset: 0x000030BA
		internal static string DataRow_NoOriginalData
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_NoOriginalData", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00004ED0 File Offset: 0x000030D0
		internal static string DataRow_NoProposedData
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_NoProposedData", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00004EE6 File Offset: 0x000030E6
		internal static string DataRow_NotInTheDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_NotInTheDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00004EFC File Offset: 0x000030FC
		internal static string DataRow_NotInTheTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_NotInTheTable", Strings.resourceCulture);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00004F12 File Offset: 0x00003112
		internal static string DataRow_OutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_OutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00004F28 File Offset: 0x00003128
		internal static string DataRow_ParentRowNotInTheDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_ParentRowNotInTheDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00004F3E File Offset: 0x0000313E
		internal static string DataRow_RemovedFromTheTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_RemovedFromTheTable", Strings.resourceCulture);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00004F54 File Offset: 0x00003154
		internal static string DataRow_RowInsertMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_RowInsertMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00004F6A File Offset: 0x0000316A
		internal static string DataRow_RowInsertOutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_RowInsertOutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00004F80 File Offset: 0x00003180
		internal static string DataRow_RowInsertTwice
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_RowInsertTwice", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00004F96 File Offset: 0x00003196
		internal static string DataRow_RowOutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_RowOutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00004FAC File Offset: 0x000031AC
		internal static string DataRow_ValuesArrayLength
		{
			get
			{
				return Strings.ResourceManager.GetString("DataRow_ValuesArrayLength", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00004FC2 File Offset: 0x000031C2
		internal static string DataROWView_PropertyNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("DataROWView_PropertyNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00004FD8 File Offset: 0x000031D8
		internal static string DataSet_CannotChangeCaseLocale
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_CannotChangeCaseLocale", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00004FEE File Offset: 0x000031EE
		internal static string DataSet_CannotChangeSchemaSerializationMode
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_CannotChangeSchemaSerializationMode", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00005004 File Offset: 0x00003204
		internal static string DataSet_DefaultConstraintException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultConstraintException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000501A File Offset: 0x0000321A
		internal static string DataSet_DefaultDataException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultDataException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00005030 File Offset: 0x00003230
		internal static string DataSet_DefaultDeletedRowInaccessibleException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultDeletedRowInaccessibleException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00005046 File Offset: 0x00003246
		internal static string DataSet_DefaultDuplicateNameException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultDuplicateNameException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000505C File Offset: 0x0000325C
		internal static string DataSet_DefaultInRowChangingEventException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultInRowChangingEventException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005072 File Offset: 0x00003272
		internal static string DataSet_DefaultInvalidConstraintException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultInvalidConstraintException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00005088 File Offset: 0x00003288
		internal static string DataSet_DefaultMissingPrimaryKeyException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultMissingPrimaryKeyException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000509E File Offset: 0x0000329E
		internal static string DataSet_DefaultNoNullAllowedException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultNoNullAllowedException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000050B4 File Offset: 0x000032B4
		internal static string DataSet_DefaultReadOnlyException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultReadOnlyException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000050CA File Offset: 0x000032CA
		internal static string DataSet_DefaultRowNotInTableException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultRowNotInTableException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000050E0 File Offset: 0x000032E0
		internal static string DataSet_DefaultVersionNotFoundException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_DefaultVersionNotFoundException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000050F6 File Offset: 0x000032F6
		internal static string DataSet_SetDataSetNameConflicting
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_SetDataSetNameConflicting", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000510C File Offset: 0x0000330C
		internal static string DataSet_SetNameToEmpty
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_SetNameToEmpty", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005122 File Offset: 0x00003322
		internal static string DataSet_UnsupportedSchema
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSet_UnsupportedSchema", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00005138 File Offset: 0x00003338
		internal static string DataSetCaseSensitiveDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetCaseSensitiveDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000514E File Offset: 0x0000334E
		internal static string DataSetDataSetNameDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetDataSetNameDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00005164 File Offset: 0x00003364
		internal static string DataSetDefaultViewDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetDefaultViewDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000517A File Offset: 0x0000337A
		internal static string DataSetDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00005190 File Offset: 0x00003390
		internal static string DataSetEnforceConstraintsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetEnforceConstraintsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000051A6 File Offset: 0x000033A6
		internal static string DataSetHasErrorsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetHasErrorsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000051BC File Offset: 0x000033BC
		internal static string DataSetInitializedDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetInitializedDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000051D2 File Offset: 0x000033D2
		internal static string DataSetLocaleDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetLocaleDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000051E8 File Offset: 0x000033E8
		internal static string DataSetMergeFailedDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetMergeFailedDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000051FE File Offset: 0x000033FE
		internal static string DataSetNamespaceDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetNamespaceDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00005214 File Offset: 0x00003414
		internal static string DataSetPrefixDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetPrefixDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000522A File Offset: 0x0000342A
		internal static string DataSetRelationsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetRelationsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00005240 File Offset: 0x00003440
		internal static string DataSetTablesDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataSetTablesDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00005256 File Offset: 0x00003456
		internal static string DataStorage_AggregateException
		{
			get
			{
				return Strings.ResourceManager.GetString("DataStorage_AggregateException", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000526C File Offset: 0x0000346C
		internal static string DataStorage_IComparableNotDefined
		{
			get
			{
				return Strings.ResourceManager.GetString("DataStorage_IComparableNotDefined", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00005282 File Offset: 0x00003482
		internal static string DataStorage_InvalidStorageType
		{
			get
			{
				return Strings.ResourceManager.GetString("DataStorage_InvalidStorageType", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00005298 File Offset: 0x00003498
		internal static string DataStorage_ProblematicChars
		{
			get
			{
				return Strings.ResourceManager.GetString("DataStorage_ProblematicChars", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000052AE File Offset: 0x000034AE
		internal static string DataStorage_SetInvalidDataType
		{
			get
			{
				return Strings.ResourceManager.GetString("DataStorage_SetInvalidDataType", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000052C4 File Offset: 0x000034C4
		internal static string DataTable_AlreadyInOtherDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_AlreadyInOtherDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000052DA File Offset: 0x000034DA
		internal static string DataTable_AlreadyInTheDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_AlreadyInTheDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000052F0 File Offset: 0x000034F0
		internal static string DataTable_CannotAddToSimpleContent
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_CannotAddToSimpleContent", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00005306 File Offset: 0x00003506
		internal static string DataTable_CanNotRemoteDataTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_CanNotRemoteDataTable", Strings.resourceCulture);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000531C File Offset: 0x0000351C
		internal static string DataTable_CanNotSerializeDataTableHierarchy
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_CanNotSerializeDataTableHierarchy", Strings.resourceCulture);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00005332 File Offset: 0x00003532
		internal static string DataTable_CanNotSerializeDataTableWithEmptyName
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_CanNotSerializeDataTableWithEmptyName", Strings.resourceCulture);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00005348 File Offset: 0x00003548
		internal static string DataTable_CanNotSetRemotingFormat
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_CanNotSetRemotingFormat", Strings.resourceCulture);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000535E File Offset: 0x0000355E
		internal static string DataTable_DatasetConflictingName
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_DatasetConflictingName", Strings.resourceCulture);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00005374 File Offset: 0x00003574
		internal static string DataTable_DuplicateName
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_DuplicateName", Strings.resourceCulture);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000538A File Offset: 0x0000358A
		internal static string DataTable_DuplicateName2
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_DuplicateName2", Strings.resourceCulture);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000053A0 File Offset: 0x000035A0
		internal static string DataTable_ForeignPrimaryKey
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_ForeignPrimaryKey", Strings.resourceCulture);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000053B6 File Offset: 0x000035B6
		internal static string DataTable_InConstraint
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_InConstraint", Strings.resourceCulture);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000053CC File Offset: 0x000035CC
		internal static string DataTable_InRelation
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_InRelation", Strings.resourceCulture);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000053E2 File Offset: 0x000035E2
		internal static string DataTable_InvalidSortString
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_InvalidSortString", Strings.resourceCulture);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000053F8 File Offset: 0x000035F8
		internal static string DataTable_MissingPrimaryKey
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_MissingPrimaryKey", Strings.resourceCulture);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000540E File Offset: 0x0000360E
		internal static string DataTable_MultipleSimpleContentColumns
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_MultipleSimpleContentColumns", Strings.resourceCulture);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00005424 File Offset: 0x00003624
		internal static string DataTable_NoName
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_NoName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000543A File Offset: 0x0000363A
		internal static string DataTable_NotInTheDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_NotInTheDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00005450 File Offset: 0x00003650
		internal static string DataTable_OutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_OutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00005466 File Offset: 0x00003666
		internal static string DataTable_SelfnestedDatasetConflictingName
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_SelfnestedDatasetConflictingName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000547C File Offset: 0x0000367C
		internal static string DataTable_TableNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTable_TableNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00005492 File Offset: 0x00003692
		internal static string DataTableCaseSensitiveDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableCaseSensitiveDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000054A8 File Offset: 0x000036A8
		internal static string DataTableChildRelationsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableChildRelationsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000054BE File Offset: 0x000036BE
		internal static string DataTableColumnChangedDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableColumnChangedDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000054D4 File Offset: 0x000036D4
		internal static string DataTableColumnChangingDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableColumnChangingDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000054EA File Offset: 0x000036EA
		internal static string DataTableColumnsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableColumnsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00005500 File Offset: 0x00003700
		internal static string DataTableConstraintsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableConstraintsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00005516 File Offset: 0x00003716
		internal static string DataTableDataSetDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableDataSetDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000552C File Offset: 0x0000372C
		internal static string DataTableDefaultViewDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableDefaultViewDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00005542 File Offset: 0x00003742
		internal static string DataTableDisplayExpressionDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableDisplayExpressionDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00005558 File Offset: 0x00003758
		internal static string DataTableHasErrorsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableHasErrorsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000556E File Offset: 0x0000376E
		internal static string DataTableLocaleDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableLocaleDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00005584 File Offset: 0x00003784
		internal static string DataTableMapping_ColumnMappings
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableMapping_ColumnMappings", Strings.resourceCulture);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000559A File Offset: 0x0000379A
		internal static string DataTableMapping_DataSetTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableMapping_DataSetTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000055B0 File Offset: 0x000037B0
		internal static string DataTableMapping_SourceTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableMapping_SourceTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000055C6 File Offset: 0x000037C6
		internal static string DataTableMappings_Count
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableMappings_Count", Strings.resourceCulture);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000055DC File Offset: 0x000037DC
		internal static string DataTableMappings_Item
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableMappings_Item", Strings.resourceCulture);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000055F2 File Offset: 0x000037F2
		internal static string DataTableMinimumCapacityDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableMinimumCapacityDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00005608 File Offset: 0x00003808
		internal static string DataTableNamespaceDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableNamespaceDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000561E File Offset: 0x0000381E
		internal static string DataTableParentRelationsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableParentRelationsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00005634 File Offset: 0x00003834
		internal static string DataTablePrefixDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTablePrefixDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000564A File Offset: 0x0000384A
		internal static string DataTablePrimaryKeyDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTablePrimaryKeyDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00005660 File Offset: 0x00003860
		internal static string DataTableReader_ArgumentContainsNullValue
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableReader_ArgumentContainsNullValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00005676 File Offset: 0x00003876
		internal static string DataTableReader_CannotCreateDataReaderOnEmptyDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableReader_CannotCreateDataReaderOnEmptyDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000568C File Offset: 0x0000388C
		internal static string DataTableReader_DataTableCleared
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableReader_DataTableCleared", Strings.resourceCulture);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000056A2 File Offset: 0x000038A2
		internal static string DataTableReader_DataTableReaderArgumentIsEmpty
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableReader_DataTableReaderArgumentIsEmpty", Strings.resourceCulture);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000056B8 File Offset: 0x000038B8
		internal static string DataTableReader_InvalidDataTableReader
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableReader_InvalidDataTableReader", Strings.resourceCulture);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000056CE File Offset: 0x000038CE
		internal static string DataTableReader_InvalidRowInDataTableReader
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableReader_InvalidRowInDataTableReader", Strings.resourceCulture);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000056E4 File Offset: 0x000038E4
		internal static string DataTableReader_SchemaInvalidDataTableReader
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableReader_SchemaInvalidDataTableReader", Strings.resourceCulture);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000237 RID: 567 RVA: 0x000056FA File Offset: 0x000038FA
		internal static string DataTableRowChangedDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableRowChangedDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00005710 File Offset: 0x00003910
		internal static string DataTableRowChangingDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableRowChangingDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00005726 File Offset: 0x00003926
		internal static string DataTableRowDeletedDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableRowDeletedDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000573C File Offset: 0x0000393C
		internal static string DataTableRowDeletingDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableRowDeletingDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00005752 File Offset: 0x00003952
		internal static string DataTableRowsClearedDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableRowsClearedDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00005768 File Offset: 0x00003968
		internal static string DataTableRowsClearingDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableRowsClearingDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000577E File Offset: 0x0000397E
		internal static string DataTableRowsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableRowsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00005794 File Offset: 0x00003994
		internal static string DataTableRowsNewRowDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableRowsNewRowDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000057AA File Offset: 0x000039AA
		internal static string DataTableTableNameDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataTableTableNameDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000057C0 File Offset: 0x000039C0
		internal static string DataView_AddExternalObject
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_AddExternalObject", Strings.resourceCulture);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000057D6 File Offset: 0x000039D6
		internal static string DataView_AddNewNotAllowNull
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_AddNewNotAllowNull", Strings.resourceCulture);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000057EC File Offset: 0x000039EC
		internal static string DataView_CanNotBindTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_CanNotBindTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00005802 File Offset: 0x00003A02
		internal static string DataView_CanNotClear
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_CanNotClear", Strings.resourceCulture);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00005818 File Offset: 0x00003A18
		internal static string DataView_CanNotDelete
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_CanNotDelete", Strings.resourceCulture);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000582E File Offset: 0x00003A2E
		internal static string DataView_CanNotEdit
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_CanNotEdit", Strings.resourceCulture);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00005844 File Offset: 0x00003A44
		internal static string DataView_CanNotSetDataSet
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_CanNotSetDataSet", Strings.resourceCulture);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000585A File Offset: 0x00003A5A
		internal static string DataView_CanNotSetTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_CanNotSetTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00005870 File Offset: 0x00003A70
		internal static string DataView_CanNotUse
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_CanNotUse", Strings.resourceCulture);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00005886 File Offset: 0x00003A86
		internal static string DataView_CanNotUseDataViewManager
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_CanNotUseDataViewManager", Strings.resourceCulture);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000589C File Offset: 0x00003A9C
		internal static string DataView_CreateChildView
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_CreateChildView", Strings.resourceCulture);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000058B2 File Offset: 0x00003AB2
		internal static string DataView_GetElementIndex
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_GetElementIndex", Strings.resourceCulture);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600024C RID: 588 RVA: 0x000058C8 File Offset: 0x00003AC8
		internal static string DataView_InsertExternalObject
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_InsertExternalObject", Strings.resourceCulture);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000058DE File Offset: 0x00003ADE
		internal static string DataView_NotOpen
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_NotOpen", Strings.resourceCulture);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000058F4 File Offset: 0x00003AF4
		internal static string DataView_RemoveExternalObject
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_RemoveExternalObject", Strings.resourceCulture);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000590A File Offset: 0x00003B0A
		internal static string DataView_SetDataSetFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_SetDataSetFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00005920 File Offset: 0x00003B20
		internal static string DataView_SetFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_SetFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00005936 File Offset: 0x00003B36
		internal static string DataView_SetIListObject
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_SetIListObject", Strings.resourceCulture);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000594C File Offset: 0x00003B4C
		internal static string DataView_SetRowStateFilter
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_SetRowStateFilter", Strings.resourceCulture);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00005962 File Offset: 0x00003B62
		internal static string DataView_SetTable
		{
			get
			{
				return Strings.ResourceManager.GetString("DataView_SetTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00005978 File Offset: 0x00003B78
		internal static string DataViewAllowDeleteDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewAllowDeleteDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000598E File Offset: 0x00003B8E
		internal static string DataViewAllowEditDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewAllowEditDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000059A4 File Offset: 0x00003BA4
		internal static string DataViewAllowNewDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewAllowNewDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000257 RID: 599 RVA: 0x000059BA File Offset: 0x00003BBA
		internal static string DataViewApplyDefaultSortDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewApplyDefaultSortDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000059D0 File Offset: 0x00003BD0
		internal static string DataViewCountDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewCountDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000259 RID: 601 RVA: 0x000059E6 File Offset: 0x00003BE6
		internal static string DataViewDataViewManagerDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewDataViewManagerDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000059FC File Offset: 0x00003BFC
		internal static string DataViewIsOpenDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewIsOpenDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00005A12 File Offset: 0x00003C12
		internal static string DataViewListChangedDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewListChangedDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00005A28 File Offset: 0x00003C28
		internal static string DataViewManagerDataSetDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewManagerDataSetDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00005A3E File Offset: 0x00003C3E
		internal static string DataViewManagerTableSettingsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewManagerTableSettingsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00005A54 File Offset: 0x00003C54
		internal static string DataViewRowFilterDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewRowFilterDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00005A6A File Offset: 0x00003C6A
		internal static string DataViewRowStateFilterDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewRowStateFilterDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00005A80 File Offset: 0x00003C80
		internal static string DataViewSortDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewSortDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00005A96 File Offset: 0x00003C96
		internal static string DataViewTableDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("DataViewTableDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00005AAC File Offset: 0x00003CAC
		internal static string DbCommand_CommandText
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommand_CommandText", Strings.resourceCulture);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00005AC2 File Offset: 0x00003CC2
		internal static string DbCommand_CommandTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommand_CommandTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00005AD8 File Offset: 0x00003CD8
		internal static string DbCommand_CommandType
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommand_CommandType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00005AEE File Offset: 0x00003CEE
		internal static string DbCommand_Connection
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommand_Connection", Strings.resourceCulture);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00005B04 File Offset: 0x00003D04
		internal static string DbCommand_Parameters
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommand_Parameters", Strings.resourceCulture);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00005B1A File Offset: 0x00003D1A
		internal static string DbCommand_StatementCompleted
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommand_StatementCompleted", Strings.resourceCulture);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00005B30 File Offset: 0x00003D30
		internal static string DbCommand_Transaction
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommand_Transaction", Strings.resourceCulture);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00005B46 File Offset: 0x00003D46
		internal static string DbCommand_UpdatedRowSource
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommand_UpdatedRowSource", Strings.resourceCulture);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00005B5C File Offset: 0x00003D5C
		internal static string DbCommandBuilder_CatalogLocation
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommandBuilder_CatalogLocation", Strings.resourceCulture);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00005B72 File Offset: 0x00003D72
		internal static string DbCommandBuilder_CatalogSeparator
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommandBuilder_CatalogSeparator", Strings.resourceCulture);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00005B88 File Offset: 0x00003D88
		internal static string DbCommandBuilder_ConflictOption
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommandBuilder_ConflictOption", Strings.resourceCulture);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00005B9E File Offset: 0x00003D9E
		internal static string DbCommandBuilder_DataAdapter
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommandBuilder_DataAdapter", Strings.resourceCulture);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00005BB4 File Offset: 0x00003DB4
		internal static string DbCommandBuilder_QuotePrefix
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommandBuilder_QuotePrefix", Strings.resourceCulture);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00005BCA File Offset: 0x00003DCA
		internal static string DbCommandBuilder_QuoteSuffix
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommandBuilder_QuoteSuffix", Strings.resourceCulture);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00005BE0 File Offset: 0x00003DE0
		internal static string DbCommandBuilder_SchemaLocation
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommandBuilder_SchemaLocation", Strings.resourceCulture);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00005BF6 File Offset: 0x00003DF6
		internal static string DbCommandBuilder_SchemaSeparator
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommandBuilder_SchemaSeparator", Strings.resourceCulture);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00005C0C File Offset: 0x00003E0C
		internal static string DbCommandBuilder_SetAllValues
		{
			get
			{
				return Strings.ResourceManager.GetString("DbCommandBuilder_SetAllValues", Strings.resourceCulture);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00005C22 File Offset: 0x00003E22
		internal static string DbConnection_InfoMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnection_InfoMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00005C38 File Offset: 0x00003E38
		internal static string DbConnection_State
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnection_State", Strings.resourceCulture);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00005C4E File Offset: 0x00003E4E
		internal static string DbConnection_StateChange
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnection_StateChange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00005C64 File Offset: 0x00003E64
		internal static string DbConnectionString_AdoNetPooler
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_AdoNetPooler", Strings.resourceCulture);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00005C7A File Offset: 0x00003E7A
		internal static string DbConnectionString_ApplicationIntent
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ApplicationIntent", Strings.resourceCulture);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00005C90 File Offset: 0x00003E90
		internal static string DbConnectionString_ApplicationName
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ApplicationName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00005CA6 File Offset: 0x00003EA6
		internal static string DbConnectionString_AttachDBFilename
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_AttachDBFilename", Strings.resourceCulture);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00005CBC File Offset: 0x00003EBC
		internal static string DbConnectionString_Authentication
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_Authentication", Strings.resourceCulture);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00005CD2 File Offset: 0x00003ED2
		internal static string DbConnectionString_Certificate
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_Certificate", Strings.resourceCulture);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00005CE8 File Offset: 0x00003EE8
		internal static string DbConnectionString_ConnectionReset
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ConnectionReset", Strings.resourceCulture);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00005CFE File Offset: 0x00003EFE
		internal static string DbConnectionString_ConnectionString
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ConnectionString", Strings.resourceCulture);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00005D14 File Offset: 0x00003F14
		internal static string DbConnectionString_ConnectRetryCount
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ConnectRetryCount", Strings.resourceCulture);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00005D2A File Offset: 0x00003F2A
		internal static string DbConnectionString_ConnectRetryInterval
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ConnectRetryInterval", Strings.resourceCulture);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00005D40 File Offset: 0x00003F40
		internal static string DbConnectionString_ConnectTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ConnectTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00005D56 File Offset: 0x00003F56
		internal static string DbConnectionString_ContextConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ContextConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00005D6C File Offset: 0x00003F6C
		internal static string DbConnectionString_CurrentLanguage
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_CurrentLanguage", Strings.resourceCulture);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00005D82 File Offset: 0x00003F82
		internal static string DbConnectionString_DataSource
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_DataSource", Strings.resourceCulture);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00005D98 File Offset: 0x00003F98
		internal static string DbConnectionString_Driver
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_Driver", Strings.resourceCulture);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00005DAE File Offset: 0x00003FAE
		internal static string DbConnectionString_DSN
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_DSN", Strings.resourceCulture);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00005DC4 File Offset: 0x00003FC4
		internal static string DbConnectionString_Encrypt
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_Encrypt", Strings.resourceCulture);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00005DDA File Offset: 0x00003FDA
		internal static string DbConnectionString_Enlist
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_Enlist", Strings.resourceCulture);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00005DF0 File Offset: 0x00003FF0
		internal static string DbConnectionString_FailoverPartner
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_FailoverPartner", Strings.resourceCulture);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00005E06 File Offset: 0x00004006
		internal static string DbConnectionString_FailoverPartnerSPN
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_FailoverPartnerSPN", Strings.resourceCulture);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00005E1C File Offset: 0x0000401C
		internal static string DbConnectionString_FileName
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_FileName", Strings.resourceCulture);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00005E32 File Offset: 0x00004032
		internal static string DbConnectionString_HostNameInCertificate
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_HostNameInCertificate", Strings.resourceCulture);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00005E48 File Offset: 0x00004048
		internal static string DbConnectionString_InitialCatalog
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_InitialCatalog", Strings.resourceCulture);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00005E5E File Offset: 0x0000405E
		internal static string DbConnectionString_IntegratedSecurity
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_IntegratedSecurity", Strings.resourceCulture);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00005E74 File Offset: 0x00004074
		internal static string DbConnectionString_LoadBalanceTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_LoadBalanceTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00005E8A File Offset: 0x0000408A
		internal static string DbConnectionString_MaxPoolSize
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_MaxPoolSize", Strings.resourceCulture);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00005EA0 File Offset: 0x000040A0
		internal static string DbConnectionString_MinPoolSize
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_MinPoolSize", Strings.resourceCulture);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00005EB6 File Offset: 0x000040B6
		internal static string DbConnectionString_MultipleActiveResultSets
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_MultipleActiveResultSets", Strings.resourceCulture);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00005ECC File Offset: 0x000040CC
		internal static string DbConnectionString_MultiSubnetFailover
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_MultiSubnetFailover", Strings.resourceCulture);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00005EE2 File Offset: 0x000040E2
		internal static string DbConnectionString_NetworkLibrary
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_NetworkLibrary", Strings.resourceCulture);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00005EF8 File Offset: 0x000040F8
		internal static string DbConnectionString_OleDbServices
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_OleDbServices", Strings.resourceCulture);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00005F0E File Offset: 0x0000410E
		internal static string DbConnectionString_PacketSize
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_PacketSize", Strings.resourceCulture);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00005F24 File Offset: 0x00004124
		internal static string DbConnectionString_Password
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_Password", Strings.resourceCulture);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00005F3A File Offset: 0x0000413A
		internal static string DbConnectionString_PersistSecurityInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_PersistSecurityInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00005F50 File Offset: 0x00004150
		internal static string DbConnectionString_PoolBlockingPeriod
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_PoolBlockingPeriod", Strings.resourceCulture);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00005F66 File Offset: 0x00004166
		internal static string DbConnectionString_Pooling
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_Pooling", Strings.resourceCulture);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00005F7C File Offset: 0x0000417C
		internal static string DbConnectionString_Provider
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_Provider", Strings.resourceCulture);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00005F92 File Offset: 0x00004192
		internal static string DbConnectionString_Replication
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_Replication", Strings.resourceCulture);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00005FA8 File Offset: 0x000041A8
		internal static string DbConnectionString_ServerCertificate
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ServerCertificate", Strings.resourceCulture);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00005FBE File Offset: 0x000041BE
		internal static string DbConnectionString_ServerSPN
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_ServerSPN", Strings.resourceCulture);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00005FD4 File Offset: 0x000041D4
		internal static string DbConnectionString_TransactionBinding
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_TransactionBinding", Strings.resourceCulture);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00005FEA File Offset: 0x000041EA
		internal static string DbConnectionString_TransparentNetworkIPResolution
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_TransparentNetworkIPResolution", Strings.resourceCulture);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00006000 File Offset: 0x00004200
		internal static string DbConnectionString_TrustServerCertificate
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_TrustServerCertificate", Strings.resourceCulture);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00006016 File Offset: 0x00004216
		internal static string DbConnectionString_TypeSystemVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_TypeSystemVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000602C File Offset: 0x0000422C
		internal static string DbConnectionString_UserID
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_UserID", Strings.resourceCulture);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00006042 File Offset: 0x00004242
		internal static string DbConnectionString_UserInstance
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_UserInstance", Strings.resourceCulture);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00006058 File Offset: 0x00004258
		internal static string DbConnectionString_WorkstationID
		{
			get
			{
				return Strings.ResourceManager.GetString("DbConnectionString_WorkstationID", Strings.resourceCulture);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000606E File Offset: 0x0000426E
		internal static string DbDataAdapter_DeleteCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("DbDataAdapter_DeleteCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00006084 File Offset: 0x00004284
		internal static string DbDataAdapter_InsertCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("DbDataAdapter_InsertCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000609A File Offset: 0x0000429A
		internal static string DbDataAdapter_RowUpdated
		{
			get
			{
				return Strings.ResourceManager.GetString("DbDataAdapter_RowUpdated", Strings.resourceCulture);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000060B0 File Offset: 0x000042B0
		internal static string DbDataAdapter_RowUpdating
		{
			get
			{
				return Strings.ResourceManager.GetString("DbDataAdapter_RowUpdating", Strings.resourceCulture);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000060C6 File Offset: 0x000042C6
		internal static string DbDataAdapter_SelectCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("DbDataAdapter_SelectCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000060DC File Offset: 0x000042DC
		internal static string DbDataAdapter_UpdateBatchSize
		{
			get
			{
				return Strings.ResourceManager.GetString("DbDataAdapter_UpdateBatchSize", Strings.resourceCulture);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000060F2 File Offset: 0x000042F2
		internal static string DbDataAdapter_UpdateCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("DbDataAdapter_UpdateCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00006108 File Offset: 0x00004308
		internal static string DbDataParameter_Precision
		{
			get
			{
				return Strings.ResourceManager.GetString("DbDataParameter_Precision", Strings.resourceCulture);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000611E File Offset: 0x0000431E
		internal static string DbDataParameter_Scale
		{
			get
			{
				return Strings.ResourceManager.GetString("DbDataParameter_Scale", Strings.resourceCulture);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00006134 File Offset: 0x00004334
		internal static string DbParameter_DbType
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_DbType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000614A File Offset: 0x0000434A
		internal static string DbParameter_Direction
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_Direction", Strings.resourceCulture);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00006160 File Offset: 0x00004360
		internal static string DbParameter_IsNullable
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_IsNullable", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00006176 File Offset: 0x00004376
		internal static string DbParameter_Offset
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_Offset", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000618C File Offset: 0x0000438C
		internal static string DbParameter_ParameterName
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_ParameterName", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000061A2 File Offset: 0x000043A2
		internal static string DbParameter_Size
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_Size", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000061B8 File Offset: 0x000043B8
		internal static string DbParameter_SourceColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_SourceColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000061CE File Offset: 0x000043CE
		internal static string DbParameter_SourceColumnNullMapping
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_SourceColumnNullMapping", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000061E4 File Offset: 0x000043E4
		internal static string DbParameter_SourceVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_SourceVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000061FA File Offset: 0x000043FA
		internal static string DbParameter_Value
		{
			get
			{
				return Strings.ResourceManager.GetString("DbParameter_Value", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00006210 File Offset: 0x00004410
		internal static string DbTable_ConflictDetection
		{
			get
			{
				return Strings.ResourceManager.GetString("DbTable_ConflictDetection", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00006226 File Offset: 0x00004426
		internal static string DbTable_Connection
		{
			get
			{
				return Strings.ResourceManager.GetString("DbTable_Connection", Strings.resourceCulture);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000623C File Offset: 0x0000443C
		internal static string DbTable_DeleteCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("DbTable_DeleteCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00006252 File Offset: 0x00004452
		internal static string DbTable_InsertCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("DbTable_InsertCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00006268 File Offset: 0x00004468
		internal static string DbTable_ReturnProviderSpecificTypes
		{
			get
			{
				return Strings.ResourceManager.GetString("DbTable_ReturnProviderSpecificTypes", Strings.resourceCulture);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000627E File Offset: 0x0000447E
		internal static string DbTable_SelectCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("DbTable_SelectCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00006294 File Offset: 0x00004494
		internal static string DbTable_TableMapping
		{
			get
			{
				return Strings.ResourceManager.GetString("DbTable_TableMapping", Strings.resourceCulture);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000062AA File Offset: 0x000044AA
		internal static string DbTable_UpdateBatchSize
		{
			get
			{
				return Strings.ResourceManager.GetString("DbTable_UpdateBatchSize", Strings.resourceCulture);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000062C0 File Offset: 0x000044C0
		internal static string DbTable_UpdateCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("DbTable_UpdateCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x000062D6 File Offset: 0x000044D6
		internal static string EnclaveRetrySleepInSecondsValueException
		{
			get
			{
				return Strings.ResourceManager.GetString("EnclaveRetrySleepInSecondsValueException", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x000062EC File Offset: 0x000044EC
		internal static string EnclaveSessionInvalidationFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("EnclaveSessionInvalidationFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00006302 File Offset: 0x00004502
		internal static string ExpiredAttestationToken
		{
			get
			{
				return Strings.ResourceManager.GetString("ExpiredAttestationToken", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00006318 File Offset: 0x00004518
		internal static string Expr_AggregateArgument
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_AggregateArgument", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000632E File Offset: 0x0000452E
		internal static string Expr_AggregateUnbound
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_AggregateUnbound", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00006344 File Offset: 0x00004544
		internal static string Expr_AmbiguousBinop
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_AmbiguousBinop", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000635A File Offset: 0x0000455A
		internal static string Expr_ArgumentOutofRange
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_ArgumentOutofRange", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00006370 File Offset: 0x00004570
		internal static string Expr_ArgumentType
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_ArgumentType", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00006386 File Offset: 0x00004586
		internal static string Expr_ArgumentTypeInteger
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_ArgumentTypeInteger", Strings.resourceCulture);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000639C File Offset: 0x0000459C
		internal static string Expr_BindFailure
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_BindFailure", Strings.resourceCulture);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000063B2 File Offset: 0x000045B2
		internal static string Expr_ComputeNotAggregate
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_ComputeNotAggregate", Strings.resourceCulture);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000063C8 File Offset: 0x000045C8
		internal static string Expr_DatatypeConvertion
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_DatatypeConvertion", Strings.resourceCulture);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060002CD RID: 717 RVA: 0x000063DE File Offset: 0x000045DE
		internal static string Expr_DatavalueConvertion
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_DatavalueConvertion", Strings.resourceCulture);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000063F4 File Offset: 0x000045F4
		internal static string Expr_DivideByZero
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_DivideByZero", Strings.resourceCulture);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000640A File Offset: 0x0000460A
		internal static string Expr_EvalNoContext
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_EvalNoContext", Strings.resourceCulture);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00006420 File Offset: 0x00004620
		internal static string Expr_ExpressionTooComplex
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_ExpressionTooComplex", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00006436 File Offset: 0x00004636
		internal static string Expr_ExpressionUnbound
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_ExpressionUnbound", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000644C File Offset: 0x0000464C
		internal static string Expr_FilterConvertion
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_FilterConvertion", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00006462 File Offset: 0x00004662
		internal static string Expr_FunctionArgumentCount
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_FunctionArgumentCount", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00006478 File Offset: 0x00004678
		internal static string Expr_InvalidDate
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvalidDate", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000648E File Offset: 0x0000468E
		internal static string Expr_InvalidHoursArgument
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvalidHoursArgument", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000064A4 File Offset: 0x000046A4
		internal static string Expr_InvalidMinutesArgument
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvalidMinutesArgument", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000064BA File Offset: 0x000046BA
		internal static string Expr_InvalidName
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvalidName", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000064D0 File Offset: 0x000046D0
		internal static string Expr_InvalidNameBracketing
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvalidNameBracketing", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000064E6 File Offset: 0x000046E6
		internal static string Expr_InvalidPattern
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvalidPattern", Strings.resourceCulture);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000064FC File Offset: 0x000046FC
		internal static string Expr_InvalidString
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvalidString", Strings.resourceCulture);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00006512 File Offset: 0x00004712
		internal static string Expr_InvalidTimeZoneRange
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvalidTimeZoneRange", Strings.resourceCulture);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00006528 File Offset: 0x00004728
		internal static string Expr_InvalidType
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvalidType", Strings.resourceCulture);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000653E File Offset: 0x0000473E
		internal static string Expr_InvokeArgument
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InvokeArgument", Strings.resourceCulture);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00006554 File Offset: 0x00004754
		internal static string Expr_InWithoutList
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InWithoutList", Strings.resourceCulture);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000656A File Offset: 0x0000476A
		internal static string Expr_InWithoutParentheses
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_InWithoutParentheses", Strings.resourceCulture);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00006580 File Offset: 0x00004780
		internal static string Expr_IsSyntax
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_IsSyntax", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00006596 File Offset: 0x00004796
		internal static string Expr_LookupArgument
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_LookupArgument", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000065AC File Offset: 0x000047AC
		internal static string Expr_MismatchKindandTimeSpan
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_MismatchKindandTimeSpan", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x000065C2 File Offset: 0x000047C2
		internal static string Expr_MissingOperand
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_MissingOperand", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000065D8 File Offset: 0x000047D8
		internal static string Expr_MissingOperandBefore
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_MissingOperandBefore", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000065EE File Offset: 0x000047EE
		internal static string Expr_MissingRightParen
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_MissingRightParen", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00006604 File Offset: 0x00004804
		internal static string Expr_NonConstantArgument
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_NonConstantArgument", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000661A File Offset: 0x0000481A
		internal static string Expr_NYI
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_NYI", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00006630 File Offset: 0x00004830
		internal static string Expr_Overflow
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_Overflow", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00006646 File Offset: 0x00004846
		internal static string Expr_Syntax
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_Syntax", Strings.resourceCulture);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000665C File Offset: 0x0000485C
		internal static string Expr_TooManyRightParentheses
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_TooManyRightParentheses", Strings.resourceCulture);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00006672 File Offset: 0x00004872
		internal static string Expr_TypeMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_TypeMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00006688 File Offset: 0x00004888
		internal static string Expr_TypeMismatchInBinop
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_TypeMismatchInBinop", Strings.resourceCulture);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000669E File Offset: 0x0000489E
		internal static string Expr_UnboundName
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_UnboundName", Strings.resourceCulture);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000066B4 File Offset: 0x000048B4
		internal static string Expr_UndefinedFunction
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_UndefinedFunction", Strings.resourceCulture);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000066CA File Offset: 0x000048CA
		internal static string Expr_UnknownToken
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_UnknownToken", Strings.resourceCulture);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000066E0 File Offset: 0x000048E0
		internal static string Expr_UnknownToken1
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_UnknownToken1", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000066F6 File Offset: 0x000048F6
		internal static string Expr_UnresolvedRelation
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_UnresolvedRelation", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000670C File Offset: 0x0000490C
		internal static string Expr_UnsupportedOperator
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_UnsupportedOperator", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00006722 File Offset: 0x00004922
		internal static string Expr_UnsupportedType
		{
			get
			{
				return Strings.ResourceManager.GetString("Expr_UnsupportedType", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00006738 File Offset: 0x00004938
		internal static string ExtendedPropertiesDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("ExtendedPropertiesDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000674E File Offset: 0x0000494E
		internal static string FailToCreateEnclaveSession
		{
			get
			{
				return Strings.ResourceManager.GetString("FailToCreateEnclaveSession", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00006764 File Offset: 0x00004964
		internal static string FailToParseAttestationInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("FailToParseAttestationInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000677A File Offset: 0x0000497A
		internal static string FailToParseAttestationToken
		{
			get
			{
				return Strings.ResourceManager.GetString("FailToParseAttestationToken", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00006790 File Offset: 0x00004990
		internal static string ForeignKeyConstraintAcceptRejectRuleDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("ForeignKeyConstraintAcceptRejectRuleDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000067A6 File Offset: 0x000049A6
		internal static string ForeignKeyConstraintChildColumnsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("ForeignKeyConstraintChildColumnsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060002FA RID: 762 RVA: 0x000067BC File Offset: 0x000049BC
		internal static string ForeignKeyConstraintDeleteRuleDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("ForeignKeyConstraintDeleteRuleDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000067D2 File Offset: 0x000049D2
		internal static string ForeignKeyConstraintParentColumnsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("ForeignKeyConstraintParentColumnsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000067E8 File Offset: 0x000049E8
		internal static string ForeignKeyConstraintUpdateRuleDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("ForeignKeyConstraintUpdateRuleDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000067FE File Offset: 0x000049FE
		internal static string ForeignKeyRelatedTableDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("ForeignKeyRelatedTableDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00006814 File Offset: 0x00004A14
		internal static string GetAttestationSigningCertificateFailedInvalidCertificate
		{
			get
			{
				return Strings.ResourceManager.GetString("GetAttestationSigningCertificateFailedInvalidCertificate", Strings.resourceCulture);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000682A File Offset: 0x00004A2A
		internal static string GetAttestationSigningCertificateRequestFailedFormat
		{
			get
			{
				return Strings.ResourceManager.GetString("GetAttestationSigningCertificateRequestFailedFormat", Strings.resourceCulture);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00006840 File Offset: 0x00004A40
		internal static string GetAttestationTokenSigningKeysFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("GetAttestationTokenSigningKeysFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00006856 File Offset: 0x00004A56
		internal static string GetSharedSecretFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("GetSharedSecretFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000686C File Offset: 0x00004A6C
		internal static string GT_Disabled
		{
			get
			{
				return Strings.ResourceManager.GetString("GT_Disabled", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00006882 File Offset: 0x00004A82
		internal static string GT_UnsupportedSysTxVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("GT_UnsupportedSysTxVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00006898 File Offset: 0x00004A98
		internal static string IEnumerableOfSqlDataRecordHasNoRows
		{
			get
			{
				return Strings.ResourceManager.GetString("IEnumerableOfSqlDataRecordHasNoRows", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000068AE File Offset: 0x00004AAE
		internal static string InvalidArgumentToBase64UrlDecoder
		{
			get
			{
				return Strings.ResourceManager.GetString("InvalidArgumentToBase64UrlDecoder", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000068C4 File Offset: 0x00004AC4
		internal static string InvalidArgumentToSHA256
		{
			get
			{
				return Strings.ResourceManager.GetString("InvalidArgumentToSHA256", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000307 RID: 775 RVA: 0x000068DA File Offset: 0x00004ADA
		internal static string InvalidAttestationToken
		{
			get
			{
				return Strings.ResourceManager.GetString("InvalidAttestationToken", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000068F0 File Offset: 0x00004AF0
		internal static string InvalidClaimInAttestationToken
		{
			get
			{
				return Strings.ResourceManager.GetString("InvalidClaimInAttestationToken", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00006906 File Offset: 0x00004B06
		internal static string InvalidSchemaTableOrdinals
		{
			get
			{
				return Strings.ResourceManager.GetString("InvalidSchemaTableOrdinals", Strings.resourceCulture);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000691C File Offset: 0x00004B1C
		internal static string KeyConstraintColumnsDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("KeyConstraintColumnsDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00006932 File Offset: 0x00004B32
		internal static string KeyConstraintIsPrimaryKeyDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("KeyConstraintIsPrimaryKeyDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00006948 File Offset: 0x00004B48
		internal static string Load_ReadOnlyDataModified
		{
			get
			{
				return Strings.ResourceManager.GetString("Load_ReadOnlyDataModified", Strings.resourceCulture);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000695E File Offset: 0x00004B5E
		internal static string LocalDB_BadConfigSectionType
		{
			get
			{
				return Strings.ResourceManager.GetString("LocalDB_BadConfigSectionType", Strings.resourceCulture);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00006974 File Offset: 0x00004B74
		internal static string LocalDB_CreateFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("LocalDB_CreateFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000698A File Offset: 0x00004B8A
		internal static string LocalDB_FailedGetDLLHandle
		{
			get
			{
				return Strings.ResourceManager.GetString("LocalDB_FailedGetDLLHandle", Strings.resourceCulture);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000310 RID: 784 RVA: 0x000069A0 File Offset: 0x00004BA0
		internal static string LocalDB_InvalidVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("LocalDB_InvalidVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000069B6 File Offset: 0x00004BB6
		internal static string LocalDB_MethodNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("LocalDB_MethodNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000069CC File Offset: 0x00004BCC
		internal static string LocalDB_UnobtainableMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("LocalDB_UnobtainableMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000069E2 File Offset: 0x00004BE2
		internal static string MDF_AmbiguousCollectionName
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_AmbiguousCollectionName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000069F8 File Offset: 0x00004BF8
		internal static string MDF_CollectionNameISNotUnique
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_CollectionNameISNotUnique", Strings.resourceCulture);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00006A0E File Offset: 0x00004C0E
		internal static string MDF_DataTableDoesNotExist
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_DataTableDoesNotExist", Strings.resourceCulture);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00006A24 File Offset: 0x00004C24
		internal static string MDF_IncorrectNumberOfDataSourceInformationRows
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_IncorrectNumberOfDataSourceInformationRows", Strings.resourceCulture);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00006A3A File Offset: 0x00004C3A
		internal static string MDF_InvalidRestrictionValue
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_InvalidRestrictionValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00006A50 File Offset: 0x00004C50
		internal static string MDF_InvalidXml
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_InvalidXml", Strings.resourceCulture);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00006A66 File Offset: 0x00004C66
		internal static string MDF_InvalidXmlInvalidValue
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_InvalidXmlInvalidValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00006A7C File Offset: 0x00004C7C
		internal static string MDF_InvalidXmlMissingColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_InvalidXmlMissingColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00006A92 File Offset: 0x00004C92
		internal static string MDF_MissingDataSourceInformationColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_MissingDataSourceInformationColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00006AA8 File Offset: 0x00004CA8
		internal static string MDF_MissingRestrictionColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_MissingRestrictionColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00006ABE File Offset: 0x00004CBE
		internal static string MDF_MissingRestrictionRow
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_MissingRestrictionRow", Strings.resourceCulture);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00006AD4 File Offset: 0x00004CD4
		internal static string MDF_NoColumns
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_NoColumns", Strings.resourceCulture);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00006AEA File Offset: 0x00004CEA
		internal static string MDF_QueryFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_QueryFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00006B00 File Offset: 0x00004D00
		internal static string MDF_TooManyRestrictions
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_TooManyRestrictions", Strings.resourceCulture);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00006B16 File Offset: 0x00004D16
		internal static string MDF_UnableToBuildCollection
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_UnableToBuildCollection", Strings.resourceCulture);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00006B2C File Offset: 0x00004D2C
		internal static string MDF_UndefinedCollection
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_UndefinedCollection", Strings.resourceCulture);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00006B42 File Offset: 0x00004D42
		internal static string MDF_UndefinedPopulationMechanism
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_UndefinedPopulationMechanism", Strings.resourceCulture);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00006B58 File Offset: 0x00004D58
		internal static string MDF_UnsupportedVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("MDF_UnsupportedVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00006B6E File Offset: 0x00004D6E
		internal static string MetaType_SingleValuedStructNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("MetaType_SingleValuedStructNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00006B84 File Offset: 0x00004D84
		internal static string MissingClaimInAttestationToken
		{
			get
			{
				return Strings.ResourceManager.GetString("MissingClaimInAttestationToken", Strings.resourceCulture);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00006B9A File Offset: 0x00004D9A
		internal static string NamedSimpleType_InvalidDuplicateNamedSimpleTypeDelaration
		{
			get
			{
				return Strings.ResourceManager.GetString("NamedSimpleType_InvalidDuplicateNamedSimpleTypeDelaration", Strings.resourceCulture);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00006BB0 File Offset: 0x00004DB0
		internal static string net_invalid_enum
		{
			get
			{
				return Strings.ResourceManager.GetString("net_invalid_enum", Strings.resourceCulture);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00006BC6 File Offset: 0x00004DC6
		internal static string NullSchemaTableDataTypeNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("NullSchemaTableDataTypeNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00006BDC File Offset: 0x00004DDC
		internal static string Odbc_CantAllocateEnvironmentHandle
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_CantAllocateEnvironmentHandle", Strings.resourceCulture);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00006BF2 File Offset: 0x00004DF2
		internal static string Odbc_CantEnableConnectionpooling
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_CantEnableConnectionpooling", Strings.resourceCulture);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00006C08 File Offset: 0x00004E08
		internal static string Odbc_CantSetPropertyOnOpenConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_CantSetPropertyOnOpenConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00006C1E File Offset: 0x00004E1E
		internal static string Odbc_ConnectionClosed
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_ConnectionClosed", Strings.resourceCulture);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00006C34 File Offset: 0x00004E34
		internal static string Odbc_ExceptionMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_ExceptionMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00006C4A File Offset: 0x00004E4A
		internal static string Odbc_ExceptionNoInfoMsg
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_ExceptionNoInfoMsg", Strings.resourceCulture);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00006C60 File Offset: 0x00004E60
		internal static string Odbc_FailedToGetDescriptorHandle
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_FailedToGetDescriptorHandle", Strings.resourceCulture);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00006C76 File Offset: 0x00004E76
		internal static string ODBC_GetSchemaRestrictionRequired
		{
			get
			{
				return Strings.ResourceManager.GetString("ODBC_GetSchemaRestrictionRequired", Strings.resourceCulture);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00006C8C File Offset: 0x00004E8C
		internal static string Odbc_GetTypeMapping_UnknownType
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_GetTypeMapping_UnknownType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00006CA2 File Offset: 0x00004EA2
		internal static string Odbc_MDACWrongVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_MDACWrongVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00006CB8 File Offset: 0x00004EB8
		internal static string Odbc_NegativeArgument
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_NegativeArgument", Strings.resourceCulture);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00006CCE File Offset: 0x00004ECE
		internal static string Odbc_NoMappingForSqlTransactionLevel
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_NoMappingForSqlTransactionLevel", Strings.resourceCulture);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00006CE4 File Offset: 0x00004EE4
		internal static string Odbc_NotInTransaction
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_NotInTransaction", Strings.resourceCulture);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00006CFA File Offset: 0x00004EFA
		internal static string ODBC_NotSupportedEnumerationValue
		{
			get
			{
				return Strings.ResourceManager.GetString("ODBC_NotSupportedEnumerationValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00006D10 File Offset: 0x00004F10
		internal static string Odbc_NullData
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_NullData", Strings.resourceCulture);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00006D26 File Offset: 0x00004F26
		internal static string ODBC_ODBCCommandText
		{
			get
			{
				return Strings.ResourceManager.GetString("ODBC_ODBCCommandText", Strings.resourceCulture);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00006D3C File Offset: 0x00004F3C
		internal static string Odbc_OpenConnectionNoOwner
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_OpenConnectionNoOwner", Strings.resourceCulture);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00006D52 File Offset: 0x00004F52
		internal static string Odbc_UnknownOdbcType
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_UnknownOdbcType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00006D68 File Offset: 0x00004F68
		internal static string Odbc_UnknownSQLType
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_UnknownSQLType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00006D7E File Offset: 0x00004F7E
		internal static string Odbc_UnknownURTType
		{
			get
			{
				return Strings.ResourceManager.GetString("Odbc_UnknownURTType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00006D94 File Offset: 0x00004F94
		internal static string OdbcCommandBuilder_DataAdapter
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcCommandBuilder_DataAdapter", Strings.resourceCulture);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00006DAA File Offset: 0x00004FAA
		internal static string OdbcCommandBuilder_QuotePrefix
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcCommandBuilder_QuotePrefix", Strings.resourceCulture);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00006DC0 File Offset: 0x00004FC0
		internal static string OdbcCommandBuilder_QuoteSuffix
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcCommandBuilder_QuoteSuffix", Strings.resourceCulture);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00006DD6 File Offset: 0x00004FD6
		internal static string OdbcConnection_ConnectionString
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcConnection_ConnectionString", Strings.resourceCulture);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00006DEC File Offset: 0x00004FEC
		internal static string OdbcConnection_ConnectionStringTooLong
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcConnection_ConnectionStringTooLong", Strings.resourceCulture);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00006E02 File Offset: 0x00005002
		internal static string OdbcConnection_ConnectionTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcConnection_ConnectionTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00006E18 File Offset: 0x00005018
		internal static string OdbcConnection_Database
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcConnection_Database", Strings.resourceCulture);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00006E2E File Offset: 0x0000502E
		internal static string OdbcConnection_DataSource
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcConnection_DataSource", Strings.resourceCulture);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00006E44 File Offset: 0x00005044
		internal static string OdbcConnection_Driver
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcConnection_Driver", Strings.resourceCulture);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00006E5A File Offset: 0x0000505A
		internal static string OdbcConnection_ServerVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcConnection_ServerVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00006E70 File Offset: 0x00005070
		internal static string OdbcParameter_OdbcType
		{
			get
			{
				return Strings.ResourceManager.GetString("OdbcParameter_OdbcType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00006E86 File Offset: 0x00005086
		internal static string OleDb_AsynchronousNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_AsynchronousNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00006E9C File Offset: 0x0000509C
		internal static string OleDb_BadAccessor
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_BadAccessor", Strings.resourceCulture);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00006EB2 File Offset: 0x000050B2
		internal static string OleDb_BadStatus_ParamAcc
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_BadStatus_ParamAcc", Strings.resourceCulture);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00006EC8 File Offset: 0x000050C8
		internal static string OleDb_BadStatusRowAccessor
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_BadStatusRowAccessor", Strings.resourceCulture);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00006EDE File Offset: 0x000050DE
		internal static string OleDb_CanNotDetermineDecimalSeparator
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CanNotDetermineDecimalSeparator", Strings.resourceCulture);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00006EF4 File Offset: 0x000050F4
		internal static string OleDb_CantConvertValue
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CantConvertValue", Strings.resourceCulture);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00006F0A File Offset: 0x0000510A
		internal static string OleDb_CantCreate
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CantCreate", Strings.resourceCulture);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00006F20 File Offset: 0x00005120
		internal static string OleDb_CommandParameterBadAccessor
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CommandParameterBadAccessor", Strings.resourceCulture);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00006F36 File Offset: 0x00005136
		internal static string OleDb_CommandParameterCantConvertValue
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CommandParameterCantConvertValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00006F4C File Offset: 0x0000514C
		internal static string OleDb_CommandParameterDataOverflow
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CommandParameterDataOverflow", Strings.resourceCulture);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00006F62 File Offset: 0x00005162
		internal static string OleDb_CommandParameterDefault
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CommandParameterDefault", Strings.resourceCulture);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00006F78 File Offset: 0x00005178
		internal static string OleDb_CommandParameterError
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CommandParameterError", Strings.resourceCulture);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00006F8E File Offset: 0x0000518E
		internal static string OleDb_CommandParameterSignMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CommandParameterSignMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00006FA4 File Offset: 0x000051A4
		internal static string OleDb_CommandParameterUnavailable
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CommandParameterUnavailable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00006FBA File Offset: 0x000051BA
		internal static string OleDb_CommandTextNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_CommandTextNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00006FD0 File Offset: 0x000051D0
		internal static string OleDb_ConfigUnableToLoadXmlMetaDataFile
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_ConfigUnableToLoadXmlMetaDataFile", Strings.resourceCulture);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00006FE6 File Offset: 0x000051E6
		internal static string OleDb_ConfigWrongNumberOfValues
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_ConfigWrongNumberOfValues", Strings.resourceCulture);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00006FFC File Offset: 0x000051FC
		internal static string OleDb_ConnectionStringSyntax
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_ConnectionStringSyntax", Strings.resourceCulture);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00007012 File Offset: 0x00005212
		internal static string OleDb_DataOverflow
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_DataOverflow", Strings.resourceCulture);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00007028 File Offset: 0x00005228
		internal static string OleDb_DBBindingGetVector
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_DBBindingGetVector", Strings.resourceCulture);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000703E File Offset: 0x0000523E
		internal static string OleDb_FailedGetDescription
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_FailedGetDescription", Strings.resourceCulture);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00007054 File Offset: 0x00005254
		internal static string OleDb_FailedGetSource
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_FailedGetSource", Strings.resourceCulture);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000706A File Offset: 0x0000526A
		internal static string OleDb_Fill_EmptyRecord
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_Fill_EmptyRecord", Strings.resourceCulture);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00007080 File Offset: 0x00005280
		internal static string OleDb_Fill_EmptyRecordSet
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_Fill_EmptyRecordSet", Strings.resourceCulture);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00007096 File Offset: 0x00005296
		internal static string OleDb_Fill_NotADODB
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_Fill_NotADODB", Strings.resourceCulture);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000362 RID: 866 RVA: 0x000070AC File Offset: 0x000052AC
		internal static string OleDb_GVtUnknown
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_GVtUnknown", Strings.resourceCulture);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000070C2 File Offset: 0x000052C2
		internal static string OleDb_IDBInfoNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_IDBInfoNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000364 RID: 868 RVA: 0x000070D8 File Offset: 0x000052D8
		internal static string OleDb_InvalidProviderSpecified
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_InvalidProviderSpecified", Strings.resourceCulture);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000070EE File Offset: 0x000052EE
		internal static string OleDb_InvalidRestrictionsDbInfoKeywords
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_InvalidRestrictionsDbInfoKeywords", Strings.resourceCulture);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00007104 File Offset: 0x00005304
		internal static string OleDb_InvalidRestrictionsDbInfoLiteral
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_InvalidRestrictionsDbInfoLiteral", Strings.resourceCulture);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000711A File Offset: 0x0000531A
		internal static string OleDb_InvalidRestrictionsSchemaGuids
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_InvalidRestrictionsSchemaGuids", Strings.resourceCulture);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00007130 File Offset: 0x00005330
		internal static string OleDb_ISourcesRowsetNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_ISourcesRowsetNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00007146 File Offset: 0x00005346
		internal static string OleDb_MDACNotAvailable
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_MDACNotAvailable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000715C File Offset: 0x0000535C
		internal static string OleDb_MDACWrongVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_MDACWrongVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00007172 File Offset: 0x00005372
		internal static string OleDb_MSDASQLNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_MSDASQLNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00007188 File Offset: 0x00005388
		internal static string OleDb_NoErrorInformation
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_NoErrorInformation", Strings.resourceCulture);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000719E File Offset: 0x0000539E
		internal static string OleDb_NoErrorInformation2
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_NoErrorInformation2", Strings.resourceCulture);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000071B4 File Offset: 0x000053B4
		internal static string OleDb_NoErrorMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_NoErrorMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000071CA File Offset: 0x000053CA
		internal static string OleDb_NoProviderSpecified
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_NoProviderSpecified", Strings.resourceCulture);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000071E0 File Offset: 0x000053E0
		internal static string OleDb_NoProviderSupportForParameters
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_NoProviderSupportForParameters", Strings.resourceCulture);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000071F6 File Offset: 0x000053F6
		internal static string OleDb_NoProviderSupportForSProcResetParameters
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_NoProviderSupportForSProcResetParameters", Strings.resourceCulture);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000720C File Offset: 0x0000540C
		internal static string OLEDB_NotSupportedEnumerationValue
		{
			get
			{
				return Strings.ResourceManager.GetString("OLEDB_NotSupportedEnumerationValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00007222 File Offset: 0x00005422
		internal static string OleDb_NotSupportedSchemaTable
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_NotSupportedSchemaTable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00007238 File Offset: 0x00005438
		internal static string OLEDB_OLEDBCommandText
		{
			get
			{
				return Strings.ResourceManager.GetString("OLEDB_OLEDBCommandText", Strings.resourceCulture);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000724E File Offset: 0x0000544E
		internal static string OleDb_PossiblePromptNotUserInteractive
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PossiblePromptNotUserInteractive", Strings.resourceCulture);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00007264 File Offset: 0x00005464
		internal static string OleDb_PropertyBadColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyBadColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000727A File Offset: 0x0000547A
		internal static string OleDb_PropertyBadOption
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyBadOption", Strings.resourceCulture);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00007290 File Offset: 0x00005490
		internal static string OleDb_PropertyBadValue
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyBadValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000072A6 File Offset: 0x000054A6
		internal static string OleDb_PropertyConflicting
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyConflicting", Strings.resourceCulture);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600037A RID: 890 RVA: 0x000072BC File Offset: 0x000054BC
		internal static string OleDb_PropertyNotAllSettable
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyNotAllSettable", Strings.resourceCulture);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000072D2 File Offset: 0x000054D2
		internal static string OleDb_PropertyNotAvailable
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyNotAvailable", Strings.resourceCulture);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000072E8 File Offset: 0x000054E8
		internal static string OleDb_PropertyNotSet
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyNotSet", Strings.resourceCulture);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000072FE File Offset: 0x000054FE
		internal static string OleDb_PropertyNotSettable
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyNotSettable", Strings.resourceCulture);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00007314 File Offset: 0x00005514
		internal static string OleDb_PropertyNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000732A File Offset: 0x0000552A
		internal static string OleDb_PropertyStatusUnknown
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_PropertyStatusUnknown", Strings.resourceCulture);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00007340 File Offset: 0x00005540
		internal static string OleDb_ProviderUnavailable
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_ProviderUnavailable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00007356 File Offset: 0x00005556
		internal static string OleDb_SchemaRowsetsNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_SchemaRowsetsNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000736C File Offset: 0x0000556C
		internal static string OleDb_SignMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_SignMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00007382 File Offset: 0x00005582
		internal static string OleDb_SVtUnknown
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_SVtUnknown", Strings.resourceCulture);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00007398 File Offset: 0x00005598
		internal static string OleDb_ThreadApartmentState
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_ThreadApartmentState", Strings.resourceCulture);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000385 RID: 901 RVA: 0x000073AE File Offset: 0x000055AE
		internal static string OleDb_TransactionsNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_TransactionsNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000073C4 File Offset: 0x000055C4
		internal static string OleDb_Unavailable
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_Unavailable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000073DA File Offset: 0x000055DA
		internal static string OleDb_UnexpectedStatusValue
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_UnexpectedStatusValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000073F0 File Offset: 0x000055F0
		internal static string OleDb_UninitializedParameters
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDb_UninitializedParameters", Strings.resourceCulture);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00007406 File Offset: 0x00005606
		internal static string OleDbCommandBuilder_DataAdapter
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbCommandBuilder_DataAdapter", Strings.resourceCulture);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000741C File Offset: 0x0000561C
		internal static string OleDbCommandBuilder_DecimalSeparator
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbCommandBuilder_DecimalSeparator", Strings.resourceCulture);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00007432 File Offset: 0x00005632
		internal static string OleDbCommandBuilder_QuotePrefix
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbCommandBuilder_QuotePrefix", Strings.resourceCulture);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00007448 File Offset: 0x00005648
		internal static string OleDbCommandBuilder_QuoteSuffix
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbCommandBuilder_QuoteSuffix", Strings.resourceCulture);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000745E File Offset: 0x0000565E
		internal static string OleDbConnection_ConnectionString
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbConnection_ConnectionString", Strings.resourceCulture);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00007474 File Offset: 0x00005674
		internal static string OleDbConnection_ConnectionTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbConnection_ConnectionTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000748A File Offset: 0x0000568A
		internal static string OleDbConnection_Database
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbConnection_Database", Strings.resourceCulture);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000074A0 File Offset: 0x000056A0
		internal static string OleDbConnection_DataSource
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbConnection_DataSource", Strings.resourceCulture);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000074B6 File Offset: 0x000056B6
		internal static string OleDbConnection_Provider
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbConnection_Provider", Strings.resourceCulture);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000392 RID: 914 RVA: 0x000074CC File Offset: 0x000056CC
		internal static string OleDbConnection_ServerVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbConnection_ServerVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000074E2 File Offset: 0x000056E2
		internal static string OleDbParameter_OleDbType
		{
			get
			{
				return Strings.ResourceManager.GetString("OleDbParameter_OleDbType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000394 RID: 916 RVA: 0x000074F8 File Offset: 0x000056F8
		internal static string propertyChangedEventDescr
		{
			get
			{
				return Strings.ResourceManager.GetString("propertyChangedEventDescr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000750E File Offset: 0x0000570E
		internal static string Range_Argument
		{
			get
			{
				return Strings.ResourceManager.GetString("Range_Argument", Strings.resourceCulture);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00007524 File Offset: 0x00005724
		internal static string Range_NullRange
		{
			get
			{
				return Strings.ResourceManager.GetString("Range_NullRange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000753A File Offset: 0x0000573A
		internal static string RbTree_EnumerationBroken
		{
			get
			{
				return Strings.ResourceManager.GetString("RbTree_EnumerationBroken", Strings.resourceCulture);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00007550 File Offset: 0x00005750
		internal static string RbTree_InvalidState
		{
			get
			{
				return Strings.ResourceManager.GetString("RbTree_InvalidState", Strings.resourceCulture);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00007566 File Offset: 0x00005766
		internal static string RecordManager_MinimumCapacity
		{
			get
			{
				return Strings.ResourceManager.GetString("RecordManager_MinimumCapacity", Strings.resourceCulture);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000757C File Offset: 0x0000577C
		internal static string SEC_ProtocolWarning
		{
			get
			{
				return Strings.ResourceManager.GetString("SEC_ProtocolWarning", Strings.resourceCulture);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00007592 File Offset: 0x00005792
		internal static string SNI_ERROR_1
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_1", Strings.resourceCulture);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000075A8 File Offset: 0x000057A8
		internal static string SNI_ERROR_10
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_10", Strings.resourceCulture);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000075BE File Offset: 0x000057BE
		internal static string SNI_ERROR_11
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_11", Strings.resourceCulture);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600039E RID: 926 RVA: 0x000075D4 File Offset: 0x000057D4
		internal static string SNI_ERROR_12
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_12", Strings.resourceCulture);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000075EA File Offset: 0x000057EA
		internal static string SNI_ERROR_13
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_13", Strings.resourceCulture);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00007600 File Offset: 0x00005800
		internal static string SNI_ERROR_14
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_14", Strings.resourceCulture);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00007616 File Offset: 0x00005816
		internal static string SNI_ERROR_15
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_15", Strings.resourceCulture);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000762C File Offset: 0x0000582C
		internal static string SNI_ERROR_16
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_16", Strings.resourceCulture);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00007642 File Offset: 0x00005842
		internal static string SNI_ERROR_17
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_17", Strings.resourceCulture);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00007658 File Offset: 0x00005858
		internal static string SNI_ERROR_18
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_18", Strings.resourceCulture);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000766E File Offset: 0x0000586E
		internal static string SNI_ERROR_19
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_19", Strings.resourceCulture);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00007684 File Offset: 0x00005884
		internal static string SNI_ERROR_2
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_2", Strings.resourceCulture);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000769A File Offset: 0x0000589A
		internal static string SNI_ERROR_20
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_20", Strings.resourceCulture);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000076B0 File Offset: 0x000058B0
		internal static string SNI_ERROR_21
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_21", Strings.resourceCulture);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x000076C6 File Offset: 0x000058C6
		internal static string SNI_ERROR_22
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_22", Strings.resourceCulture);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000076DC File Offset: 0x000058DC
		internal static string SNI_ERROR_23
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_23", Strings.resourceCulture);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060003AB RID: 939 RVA: 0x000076F2 File Offset: 0x000058F2
		internal static string SNI_ERROR_24
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_24", Strings.resourceCulture);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00007708 File Offset: 0x00005908
		internal static string SNI_ERROR_25
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_25", Strings.resourceCulture);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000771E File Offset: 0x0000591E
		internal static string SNI_ERROR_26
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_26", Strings.resourceCulture);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00007734 File Offset: 0x00005934
		internal static string SNI_ERROR_27
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_27", Strings.resourceCulture);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000774A File Offset: 0x0000594A
		internal static string SNI_ERROR_28
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_28", Strings.resourceCulture);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00007760 File Offset: 0x00005960
		internal static string SNI_ERROR_29
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_29", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00007776 File Offset: 0x00005976
		internal static string SNI_ERROR_3
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_3", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000778C File Offset: 0x0000598C
		internal static string SNI_ERROR_30
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_30", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000077A2 File Offset: 0x000059A2
		internal static string SNI_ERROR_31
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_31", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000077B8 File Offset: 0x000059B8
		internal static string SNI_ERROR_32
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_32", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000077CE File Offset: 0x000059CE
		internal static string SNI_ERROR_33
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_33", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000077E4 File Offset: 0x000059E4
		internal static string SNI_ERROR_34
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_34", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x000077FA File Offset: 0x000059FA
		internal static string SNI_ERROR_35
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_35", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00007810 File Offset: 0x00005A10
		internal static string SNI_ERROR_36
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_36", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00007826 File Offset: 0x00005A26
		internal static string SNI_ERROR_37
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_37", Strings.resourceCulture);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000783C File Offset: 0x00005A3C
		internal static string SNI_ERROR_38
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_38", Strings.resourceCulture);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00007852 File Offset: 0x00005A52
		internal static string SNI_ERROR_39
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_39", Strings.resourceCulture);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00007868 File Offset: 0x00005A68
		internal static string SNI_ERROR_4
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_4", Strings.resourceCulture);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000787E File Offset: 0x00005A7E
		internal static string SNI_ERROR_40
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_40", Strings.resourceCulture);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00007894 File Offset: 0x00005A94
		internal static string SNI_ERROR_41
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_41", Strings.resourceCulture);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000078AA File Offset: 0x00005AAA
		internal static string SNI_ERROR_42
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_42", Strings.resourceCulture);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000078C0 File Offset: 0x00005AC0
		internal static string SNI_ERROR_43
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_43", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000078D6 File Offset: 0x00005AD6
		internal static string SNI_ERROR_44
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_44", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x000078EC File Offset: 0x00005AEC
		internal static string SNI_ERROR_47
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_47", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00007902 File Offset: 0x00005B02
		internal static string SNI_ERROR_48
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_48", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00007918 File Offset: 0x00005B18
		internal static string SNI_ERROR_49
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_49", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000792E File Offset: 0x00005B2E
		internal static string SNI_ERROR_5
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_5", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00007944 File Offset: 0x00005B44
		internal static string SNI_ERROR_50
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_50", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000795A File Offset: 0x00005B5A
		internal static string SNI_ERROR_51
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_51", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00007970 File Offset: 0x00005B70
		internal static string SNI_ERROR_52
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_52", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00007986 File Offset: 0x00005B86
		internal static string SNI_ERROR_53
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_53", Strings.resourceCulture);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000799C File Offset: 0x00005B9C
		internal static string SNI_ERROR_54
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_54", Strings.resourceCulture);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000079B2 File Offset: 0x00005BB2
		internal static string SNI_ERROR_55
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_55", Strings.resourceCulture);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000079C8 File Offset: 0x00005BC8
		internal static string SNI_ERROR_56
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_56", Strings.resourceCulture);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000079DE File Offset: 0x00005BDE
		internal static string SNI_ERROR_57
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_57", Strings.resourceCulture);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000079F4 File Offset: 0x00005BF4
		internal static string SNI_ERROR_6
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_6", Strings.resourceCulture);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00007A0A File Offset: 0x00005C0A
		internal static string SNI_ERROR_7
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_7", Strings.resourceCulture);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00007A20 File Offset: 0x00005C20
		internal static string SNI_ERROR_8
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_8", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00007A36 File Offset: 0x00005C36
		internal static string SNI_ERROR_9
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_ERROR_9", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00007A4C File Offset: 0x00005C4C
		internal static string SNI_PlatformNotSupportedNetFx
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PlatformNotSupportedNetFx", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00007A62 File Offset: 0x00005C62
		internal static string SNI_PN0
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN0", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00007A78 File Offset: 0x00005C78
		internal static string SNI_PN1
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN1", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00007A8E File Offset: 0x00005C8E
		internal static string SNI_PN10
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN10", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00007AA4 File Offset: 0x00005CA4
		internal static string SNI_PN11
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN11", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00007ABA File Offset: 0x00005CBA
		internal static string SNI_PN2
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN2", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00007AD0 File Offset: 0x00005CD0
		internal static string SNI_PN3
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN3", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00007AE6 File Offset: 0x00005CE6
		internal static string SNI_PN4
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN4", Strings.resourceCulture);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00007AFC File Offset: 0x00005CFC
		internal static string SNI_PN5
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN5", Strings.resourceCulture);
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00007B12 File Offset: 0x00005D12
		internal static string SNI_PN6
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN6", Strings.resourceCulture);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00007B28 File Offset: 0x00005D28
		internal static string SNI_PN7
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN7", Strings.resourceCulture);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00007B3E File Offset: 0x00005D3E
		internal static string SNI_PN8
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN8", Strings.resourceCulture);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00007B54 File Offset: 0x00005D54
		internal static string SNI_PN9
		{
			get
			{
				return Strings.ResourceManager.GetString("SNI_PN9", Strings.resourceCulture);
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00007B6A File Offset: 0x00005D6A
		internal static string Snix_AutoEnlist
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_AutoEnlist", Strings.resourceCulture);
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00007B80 File Offset: 0x00005D80
		internal static string Snix_Close
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_Close", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00007B96 File Offset: 0x00005D96
		internal static string Snix_Connect
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_Connect", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00007BAC File Offset: 0x00005DAC
		internal static string Snix_EnableMars
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_EnableMars", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00007BC2 File Offset: 0x00005DC2
		internal static string Snix_Execute
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_Execute", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00007BD8 File Offset: 0x00005DD8
		internal static string Snix_GetMarsSession
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_GetMarsSession", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00007BEE File Offset: 0x00005DEE
		internal static string Snix_Login
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_Login", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00007C04 File Offset: 0x00005E04
		internal static string Snix_LoginSspi
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_LoginSspi", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00007C1A File Offset: 0x00005E1A
		internal static string Snix_PreLogin
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_PreLogin", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00007C30 File Offset: 0x00005E30
		internal static string Snix_PreLoginBeforeSuccessfullWrite
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_PreLoginBeforeSuccessfullWrite", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00007C46 File Offset: 0x00005E46
		internal static string Snix_ProcessSspi
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_ProcessSspi", Strings.resourceCulture);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00007C5C File Offset: 0x00005E5C
		internal static string Snix_Read
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_Read", Strings.resourceCulture);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00007C72 File Offset: 0x00005E72
		internal static string Snix_SendRows
		{
			get
			{
				return Strings.ResourceManager.GetString("Snix_SendRows", Strings.resourceCulture);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00007C88 File Offset: 0x00005E88
		internal static string SQL_ArgumentLengthMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ArgumentLengthMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00007C9E File Offset: 0x00005E9E
		internal static string SQL_AsyncOperationCompleted
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_AsyncOperationCompleted", Strings.resourceCulture);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00007CB4 File Offset: 0x00005EB4
		internal static string SQL_AuthenticationAndIntegratedSecurity
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_AuthenticationAndIntegratedSecurity", Strings.resourceCulture);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00007CCA File Offset: 0x00005ECA
		internal static string SQL_BatchedUpdatesNotAvailableOnContextConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BatchedUpdatesNotAvailableOnContextConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00007CE0 File Offset: 0x00005EE0
		internal static string SQL_BulkCopyDestinationTableName
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkCopyDestinationTableName", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00007CF6 File Offset: 0x00005EF6
		internal static string SQL_BulkLoadCannotConvertValue
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadCannotConvertValue", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00007D0C File Offset: 0x00005F0C
		internal static string SQL_BulkLoadCannotConvertValueWithoutRowNo
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadCannotConvertValueWithoutRowNo", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00007D22 File Offset: 0x00005F22
		internal static string SQL_BulkLoadConflictingTransactionOption
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadConflictingTransactionOption", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00007D38 File Offset: 0x00005F38
		internal static string SQL_BulkLoadExistingTransaction
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadExistingTransaction", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00007D4E File Offset: 0x00005F4E
		internal static string SQL_BulkLoadInvalidDestinationTable
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadInvalidDestinationTable", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00007D64 File Offset: 0x00005F64
		internal static string SQL_BulkLoadInvalidOperationInsideEvent
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadInvalidOperationInsideEvent", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00007D7A File Offset: 0x00005F7A
		internal static string SQL_BulkLoadInvalidOrderHint
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadInvalidOrderHint", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00007D90 File Offset: 0x00005F90
		internal static string SQL_BulkLoadInvalidTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadInvalidTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00007DA6 File Offset: 0x00005FA6
		internal static string SQL_BulkLoadInvalidVariantValue
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadInvalidVariantValue", Strings.resourceCulture);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00007DBC File Offset: 0x00005FBC
		internal static string Sql_BulkLoadLcidMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("Sql_BulkLoadLcidMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00007DD2 File Offset: 0x00005FD2
		internal static string SQL_BulkLoadMappingInaccessible
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadMappingInaccessible", Strings.resourceCulture);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00007DE8 File Offset: 0x00005FE8
		internal static string SQL_BulkLoadMappingsNamesOrOrdinalsOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadMappingsNamesOrOrdinalsOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00007DFE File Offset: 0x00005FFE
		internal static string SQL_BulkLoadMissingDestinationTable
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadMissingDestinationTable", Strings.resourceCulture);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00007E14 File Offset: 0x00006014
		internal static string SQL_BulkLoadNoCollation
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadNoCollation", Strings.resourceCulture);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00007E2A File Offset: 0x0000602A
		internal static string SQL_BulkLoadNonMatchingColumnMapping
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadNonMatchingColumnMapping", Strings.resourceCulture);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00007E40 File Offset: 0x00006040
		internal static string SQL_BulkLoadNonMatchingColumnName
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadNonMatchingColumnName", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00007E56 File Offset: 0x00006056
		internal static string SQL_BulkLoadNotAllowDBNull
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadNotAllowDBNull", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00007E6C File Offset: 0x0000606C
		internal static string SQL_BulkLoadOrderHintDuplicateColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadOrderHintDuplicateColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00007E82 File Offset: 0x00006082
		internal static string SQL_BulkLoadOrderHintInvalidColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadOrderHintInvalidColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00007E98 File Offset: 0x00006098
		internal static string SQL_BulkLoadPendingOperation
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadPendingOperation", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00007EAE File Offset: 0x000060AE
		internal static string SQL_BulkLoadStringTooLong
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadStringTooLong", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00007EC4 File Offset: 0x000060C4
		internal static string SQL_BulkLoadUnspecifiedSortOrder
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_BulkLoadUnspecifiedSortOrder", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00007EDA File Offset: 0x000060DA
		internal static string SQL_CannotCreateAuthInitializer
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CannotCreateAuthInitializer", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00007EF0 File Offset: 0x000060F0
		internal static string SQL_CannotCreateAuthProvider
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CannotCreateAuthProvider", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00007F06 File Offset: 0x00006106
		internal static string SQL_CannotCreateNormalizer
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CannotCreateNormalizer", Strings.resourceCulture);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00007F1C File Offset: 0x0000611C
		internal static string SQL_CannotFindAuthProvider
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CannotFindAuthProvider", Strings.resourceCulture);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00007F32 File Offset: 0x00006132
		internal static string SQL_CannotGetAuthProviderConfig
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CannotGetAuthProviderConfig", Strings.resourceCulture);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00007F48 File Offset: 0x00006148
		internal static string SQL_CannotGetDTCAddress
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CannotGetDTCAddress", Strings.resourceCulture);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00007F5E File Offset: 0x0000615E
		internal static string SQL_CannotInitializeAuthProvider
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CannotInitializeAuthProvider", Strings.resourceCulture);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x00007F74 File Offset: 0x00006174
		internal static string SQL_CannotModifyPropertyAsyncOperationInProgress
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CannotModifyPropertyAsyncOperationInProgress", Strings.resourceCulture);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00007F8A File Offset: 0x0000618A
		internal static string SQL_Certificate
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Certificate", Strings.resourceCulture);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00007FA0 File Offset: 0x000061A0
		internal static string SQL_ChangePasswordArgumentMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ChangePasswordArgumentMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00007FB6 File Offset: 0x000061B6
		internal static string SQL_ChangePasswordConflictsWithSSPI
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ChangePasswordConflictsWithSSPI", Strings.resourceCulture);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00007FCC File Offset: 0x000061CC
		internal static string SQL_ChangePasswordRequiresYukon
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ChangePasswordRequiresYukon", Strings.resourceCulture);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00007FE2 File Offset: 0x000061E2
		internal static string SQL_ChangePasswordUseOfUnallowedKey
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ChangePasswordUseOfUnallowedKey", Strings.resourceCulture);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00007FF8 File Offset: 0x000061F8
		internal static string SQL_ConnectionDoomed
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ConnectionDoomed", Strings.resourceCulture);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000800E File Offset: 0x0000620E
		internal static string SQL_ConnectionLockedForBcpEvent
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ConnectionLockedForBcpEvent", Strings.resourceCulture);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00008024 File Offset: 0x00006224
		internal static string SQL_ContextAllowsLimitedKeywords
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ContextAllowsLimitedKeywords", Strings.resourceCulture);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000803A File Offset: 0x0000623A
		internal static string SQL_ContextAllowsOnlyTypeSystem2005
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ContextAllowsOnlyTypeSystem2005", Strings.resourceCulture);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00008050 File Offset: 0x00006250
		internal static string SQL_ContextConnectionIsInUse
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ContextConnectionIsInUse", Strings.resourceCulture);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00008066 File Offset: 0x00006266
		internal static string SQL_ContextUnavailableOutOfProc
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ContextUnavailableOutOfProc", Strings.resourceCulture);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000807C File Offset: 0x0000627C
		internal static string SQL_ContextUnavailableWhileInProc
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ContextUnavailableWhileInProc", Strings.resourceCulture);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00008092 File Offset: 0x00006292
		internal static string SQL_CredentialsNotProvided
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CredentialsNotProvided", Strings.resourceCulture);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000080A8 File Offset: 0x000062A8
		internal static string SQL_CTAIPNotSupportedByServer
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CTAIPNotSupportedByServer", Strings.resourceCulture);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000080BE File Offset: 0x000062BE
		internal static string SQL_CultureIdError
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_CultureIdError", Strings.resourceCulture);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x000080D4 File Offset: 0x000062D4
		internal static string SQL_DeviceFlowWithUsernamePassword
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_DeviceFlowWithUsernamePassword", Strings.resourceCulture);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000080EA File Offset: 0x000062EA
		internal static string SQL_Duration_Login_Begin
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Duration_Login_Begin", Strings.resourceCulture);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x00008100 File Offset: 0x00006300
		internal static string SQL_Duration_Login_ProcessConnectionAuth
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Duration_Login_ProcessConnectionAuth", Strings.resourceCulture);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00008116 File Offset: 0x00006316
		internal static string SQL_Duration_PostLogin
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Duration_PostLogin", Strings.resourceCulture);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000812C File Offset: 0x0000632C
		internal static string SQL_Duration_PreLogin_Begin
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Duration_PreLogin_Begin", Strings.resourceCulture);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00008142 File Offset: 0x00006342
		internal static string SQL_Duration_PreLoginHandshake
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Duration_PreLoginHandshake", Strings.resourceCulture);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00008158 File Offset: 0x00006358
		internal static string SQL_EncryptionNotSupportedByClient
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_EncryptionNotSupportedByClient", Strings.resourceCulture);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000816E File Offset: 0x0000636E
		internal static string SQL_EncryptionNotSupportedByServer
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_EncryptionNotSupportedByServer", Strings.resourceCulture);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00008184 File Offset: 0x00006384
		internal static string SQL_EnumeratedRecordFieldCountChanged
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_EnumeratedRecordFieldCountChanged", Strings.resourceCulture);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000819A File Offset: 0x0000639A
		internal static string SQL_EnumeratedRecordMetaDataChanged
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_EnumeratedRecordMetaDataChanged", Strings.resourceCulture);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000081B0 File Offset: 0x000063B0
		internal static string SQL_ExceedsMaxDataLength
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ExceedsMaxDataLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000081C6 File Offset: 0x000063C6
		internal static string SQL_ExClientConnectionId
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ExClientConnectionId", Strings.resourceCulture);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x000081DC File Offset: 0x000063DC
		internal static string SQL_ExErrorNumberStateClass
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ExErrorNumberStateClass", Strings.resourceCulture);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000081F2 File Offset: 0x000063F2
		internal static string SQL_ExOriginalClientConnectionId
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ExOriginalClientConnectionId", Strings.resourceCulture);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00008208 File Offset: 0x00006408
		internal static string SQL_ExRoutingDestination
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ExRoutingDestination", Strings.resourceCulture);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000821E File Offset: 0x0000641E
		internal static string SQL_FatalTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_FatalTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00008234 File Offset: 0x00006434
		internal static string SQL_InstanceFailure
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InstanceFailure", Strings.resourceCulture);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000824A File Offset: 0x0000644A
		internal static string SQL_IntegratedWithPassword
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_IntegratedWithPassword", Strings.resourceCulture);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00008260 File Offset: 0x00006460
		internal static string SQL_InteractiveWithPassword
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InteractiveWithPassword", Strings.resourceCulture);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00008276 File Offset: 0x00006476
		internal static string Sql_InternalError
		{
			get
			{
				return Strings.ResourceManager.GetString("Sql_InternalError", Strings.resourceCulture);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000828C File Offset: 0x0000648C
		internal static string SQL_InvalidBufferSizeOrIndex
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidBufferSizeOrIndex", Strings.resourceCulture);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000082A2 File Offset: 0x000064A2
		internal static string SQL_InvalidDataLength
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidDataLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x000082B8 File Offset: 0x000064B8
		internal static string SQL_InvalidInternalPacketSize
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidInternalPacketSize", Strings.resourceCulture);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000082CE File Offset: 0x000064CE
		internal static string SQL_InvalidOptionLength
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidOptionLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x000082E4 File Offset: 0x000064E4
		internal static string SQL_InvalidPacketSizeValue
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidPacketSizeValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x000082FA File Offset: 0x000064FA
		internal static string SQL_InvalidParameterNameLength
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidParameterNameLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00008310 File Offset: 0x00006510
		internal static string SQL_InvalidParameterTypeNameFormat
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidParameterTypeNameFormat", Strings.resourceCulture);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00008326 File Offset: 0x00006526
		internal static string SQL_InvalidPartnerConfiguration
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidPartnerConfiguration", Strings.resourceCulture);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000833C File Offset: 0x0000653C
		internal static string SQL_InvalidRead
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidRead", Strings.resourceCulture);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00008352 File Offset: 0x00006552
		internal static string SQL_InvalidServerCertificate
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidServerCertificate", Strings.resourceCulture);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00008368 File Offset: 0x00006568
		internal static string SQL_InvalidSqlDbTypeWithOneAllowedType
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidSqlDbTypeWithOneAllowedType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000837E File Offset: 0x0000657E
		internal static string SQL_InvalidSQLServerVersionUnknown
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidSQLServerVersionUnknown", Strings.resourceCulture);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00008394 File Offset: 0x00006594
		internal static string SQL_InvalidSSPIPacketSize
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidSSPIPacketSize", Strings.resourceCulture);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x000083AA File Offset: 0x000065AA
		internal static string SQL_InvalidTDSPacketSize
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidTDSPacketSize", Strings.resourceCulture);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000083C0 File Offset: 0x000065C0
		internal static string SQL_InvalidTDSVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidTDSVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000083D6 File Offset: 0x000065D6
		internal static string SQL_InvalidUdt3PartNameFormat
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_InvalidUdt3PartNameFormat", Strings.resourceCulture);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000083EC File Offset: 0x000065EC
		internal static string SQL_MarsUnsupportedOnConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_MarsUnsupportedOnConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00008402 File Offset: 0x00006602
		internal static string Sql_MismatchedMetaDataDirectionArrayLengths
		{
			get
			{
				return Strings.ResourceManager.GetString("Sql_MismatchedMetaDataDirectionArrayLengths", Strings.resourceCulture);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00008418 File Offset: 0x00006618
		internal static string SQL_MoneyOverflow
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_MoneyOverflow", Strings.resourceCulture);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000842E File Offset: 0x0000662E
		internal static string SQL_MSALFailure
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_MSALFailure", Strings.resourceCulture);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00008444 File Offset: 0x00006644
		internal static string SQL_MSALInnerException
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_MSALInnerException", Strings.resourceCulture);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000845A File Offset: 0x0000665A
		internal static string SQL_NestedTransactionScopesNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NestedTransactionScopesNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00008470 File Offset: 0x00006670
		internal static string SQL_NonBlobColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NonBlobColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00008486 File Offset: 0x00006686
		internal static string SQL_NonCharColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NonCharColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000849C File Offset: 0x0000669C
		internal static string SQL_NonInteractiveWithPassword
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NonInteractiveWithPassword", Strings.resourceCulture);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x000084B2 File Offset: 0x000066B2
		internal static string SQL_NonLocalSSEInstance
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NonLocalSSEInstance", Strings.resourceCulture);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x000084C8 File Offset: 0x000066C8
		internal static string SQL_NonXmlResult
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NonXmlResult", Strings.resourceCulture);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000084DE File Offset: 0x000066DE
		internal static string SQL_NotAvailableOnContextConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NotAvailableOnContextConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000084F4 File Offset: 0x000066F4
		internal static string SQL_NotificationsNotAvailableOnContextConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NotificationsNotAvailableOnContextConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000850A File Offset: 0x0000670A
		internal static string SQL_NotificationsRequireYukon
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NotificationsRequireYukon", Strings.resourceCulture);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00008520 File Offset: 0x00006720
		internal static string SQL_NotSupportedEnumerationValue
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NotSupportedEnumerationValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00008536 File Offset: 0x00006736
		internal static string Sql_NullCommandText
		{
			get
			{
				return Strings.ResourceManager.GetString("Sql_NullCommandText", Strings.resourceCulture);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000854C File Offset: 0x0000674C
		internal static string SQL_NullEmptyTransactionName
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_NullEmptyTransactionName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00008562 File Offset: 0x00006762
		internal static string SQL_OpenResultCountExceeded
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_OpenResultCountExceeded", Strings.resourceCulture);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00008578 File Offset: 0x00006778
		internal static string SQL_OperationCancelled
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_OperationCancelled", Strings.resourceCulture);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000858E File Offset: 0x0000678E
		internal static string SQL_ParameterCannotBeEmpty
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParameterCannotBeEmpty", Strings.resourceCulture);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000085A4 File Offset: 0x000067A4
		internal static string SQL_ParameterDirectionInvalidForOptimizedBinding
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParameterDirectionInvalidForOptimizedBinding", Strings.resourceCulture);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x000085BA File Offset: 0x000067BA
		internal static string SQL_ParameterInvalidVariant
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParameterInvalidVariant", Strings.resourceCulture);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x000085D0 File Offset: 0x000067D0
		internal static string SQL_ParameterTypeNameRequired
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParameterTypeNameRequired", Strings.resourceCulture);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x000085E6 File Offset: 0x000067E6
		internal static string SQL_ParsingError
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParsingError", Strings.resourceCulture);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x000085FC File Offset: 0x000067FC
		internal static string SQL_ParsingErrorAuthLibraryType
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParsingErrorAuthLibraryType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00008612 File Offset: 0x00006812
		internal static string SQL_ParsingErrorFeatureId
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParsingErrorFeatureId", Strings.resourceCulture);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00008628 File Offset: 0x00006828
		internal static string SQL_ParsingErrorLength
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParsingErrorLength", Strings.resourceCulture);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000863E File Offset: 0x0000683E
		internal static string SQL_ParsingErrorOffset
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParsingErrorOffset", Strings.resourceCulture);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00008654 File Offset: 0x00006854
		internal static string SQL_ParsingErrorStatus
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParsingErrorStatus", Strings.resourceCulture);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000866A File Offset: 0x0000686A
		internal static string SQL_ParsingErrorToken
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParsingErrorToken", Strings.resourceCulture);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00008680 File Offset: 0x00006880
		internal static string SQL_ParsingErrorValue
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParsingErrorValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00008696 File Offset: 0x00006896
		internal static string SQL_ParsingErrorWithState
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ParsingErrorWithState", Strings.resourceCulture);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000086AC File Offset: 0x000068AC
		internal static string SQL_PendingBeginXXXExists
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_PendingBeginXXXExists", Strings.resourceCulture);
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000086C2 File Offset: 0x000068C2
		internal static string SQL_PipeErrorRequiresSendEnd
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_PipeErrorRequiresSendEnd", Strings.resourceCulture);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000086D8 File Offset: 0x000068D8
		internal static string SQL_PrecisionValueOutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_PrecisionValueOutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x000086EE File Offset: 0x000068EE
		internal static string SQL_ScaleValueOutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_ScaleValueOutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00008704 File Offset: 0x00006904
		internal static string SQL_SettingCredentialWithDeviceFlow
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SettingCredentialWithDeviceFlow", Strings.resourceCulture);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000871A File Offset: 0x0000691A
		internal static string SQL_SettingCredentialWithIntegrated
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SettingCredentialWithIntegrated", Strings.resourceCulture);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00008730 File Offset: 0x00006930
		internal static string SQL_SettingCredentialWithInteractive
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SettingCredentialWithInteractive", Strings.resourceCulture);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00008746 File Offset: 0x00006946
		internal static string SQL_SettingCredentialWithNonInteractive
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SettingCredentialWithNonInteractive", Strings.resourceCulture);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000875C File Offset: 0x0000695C
		internal static string SQL_SettingDeviceFlowWithCredential
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SettingDeviceFlowWithCredential", Strings.resourceCulture);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00008772 File Offset: 0x00006972
		internal static string SQL_SettingIntegratedWithCredential
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SettingIntegratedWithCredential", Strings.resourceCulture);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00008788 File Offset: 0x00006988
		internal static string SQL_SettingInteractiveWithCredential
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SettingInteractiveWithCredential", Strings.resourceCulture);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000879E File Offset: 0x0000699E
		internal static string SQL_SettingNonInteractiveWithCredential
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SettingNonInteractiveWithCredential", Strings.resourceCulture);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x000087B4 File Offset: 0x000069B4
		internal static string SQL_SevereError
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SevereError", Strings.resourceCulture);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000087CA File Offset: 0x000069CA
		internal static string SQL_SmallDateTimeOverflow
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SmallDateTimeOverflow", Strings.resourceCulture);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000087E0 File Offset: 0x000069E0
		internal static string SQL_SnapshotNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SnapshotNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000087F6 File Offset: 0x000069F6
		internal static string SQL_SNIPacketAllocationFailure
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SNIPacketAllocationFailure", Strings.resourceCulture);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000880C File Offset: 0x00006A0C
		internal static string SQL_SqlCommandCommandText
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlCommandCommandText", Strings.resourceCulture);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00008822 File Offset: 0x00006A22
		internal static string SQL_SqlRecordReadOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlRecordReadOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00008838 File Offset: 0x00006A38
		internal static string SQL_SqlRecordReadOnly2
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlRecordReadOnly2", Strings.resourceCulture);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000884E File Offset: 0x00006A4E
		internal static string SQL_SqlResultSetClosed
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlResultSetClosed", Strings.resourceCulture);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00008864 File Offset: 0x00006A64
		internal static string SQL_SqlResultSetClosed2
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlResultSetClosed2", Strings.resourceCulture);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000887A File Offset: 0x00006A7A
		internal static string SQL_SqlResultSetCommandNotInSameConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlResultSetCommandNotInSameConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00008890 File Offset: 0x00006A90
		internal static string SQL_SqlResultSetNoAcceptableCursor
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlResultSetNoAcceptableCursor", Strings.resourceCulture);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000088A6 File Offset: 0x00006AA6
		internal static string SQL_SqlResultSetRowDeleted
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlResultSetRowDeleted", Strings.resourceCulture);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000088BC File Offset: 0x00006ABC
		internal static string SQL_SqlResultSetRowDeleted2
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlResultSetRowDeleted2", Strings.resourceCulture);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x000088D2 File Offset: 0x00006AD2
		internal static string SQL_SqlUpdatableRecordReadOnly
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SqlUpdatableRecordReadOnly", Strings.resourceCulture);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000088E8 File Offset: 0x00006AE8
		internal static string SQL_SSPIGenerateError
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SSPIGenerateError", Strings.resourceCulture);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000088FE File Offset: 0x00006AFE
		internal static string SQL_SSPIInitializeError
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_SSPIInitializeError", Strings.resourceCulture);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00008914 File Offset: 0x00006B14
		internal static string SQL_StreamNotSupportOnColumnType
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_StreamNotSupportOnColumnType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000892A File Offset: 0x00006B2A
		internal static string SQL_StreamReadNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_StreamReadNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00008940 File Offset: 0x00006B40
		internal static string SQL_StreamSeekNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_StreamSeekNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00008956 File Offset: 0x00006B56
		internal static string SQL_StreamWriteNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_StreamWriteNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000896C File Offset: 0x00006B6C
		internal static string SQL_TDSParserTableName
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_TDSParserTableName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00008982 File Offset: 0x00006B82
		internal static string SQL_TextReaderNotSupportOnColumnType
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_TextReaderNotSupportOnColumnType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00008998 File Offset: 0x00006B98
		internal static string SQL_Timeout
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout", Strings.resourceCulture);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000089AE File Offset: 0x00006BAE
		internal static string SQL_Timeout_Active_Directory_DeviceFlow_Authentication
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_Active_Directory_DeviceFlow_Authentication", Strings.resourceCulture);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000089C4 File Offset: 0x00006BC4
		internal static string SQL_Timeout_Active_Directory_Interactive_Authentication
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_Active_Directory_Interactive_Authentication", Strings.resourceCulture);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000089DA File Offset: 0x00006BDA
		internal static string SQL_Timeout_Execution
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_Execution", Strings.resourceCulture);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000089F0 File Offset: 0x00006BF0
		internal static string SQL_Timeout_FailoverInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_FailoverInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00008A06 File Offset: 0x00006C06
		internal static string SQL_Timeout_Login_Begin
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_Login_Begin", Strings.resourceCulture);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00008A1C File Offset: 0x00006C1C
		internal static string SQL_Timeout_Login_ProcessConnectionAuth
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_Login_ProcessConnectionAuth", Strings.resourceCulture);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00008A32 File Offset: 0x00006C32
		internal static string SQL_Timeout_PostLogin
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_PostLogin", Strings.resourceCulture);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00008A48 File Offset: 0x00006C48
		internal static string SQL_Timeout_PreLogin_Begin
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_PreLogin_Begin", Strings.resourceCulture);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00008A5E File Offset: 0x00006C5E
		internal static string SQL_Timeout_PreLogin_ConsumeHandshake
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_PreLogin_ConsumeHandshake", Strings.resourceCulture);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00008A74 File Offset: 0x00006C74
		internal static string SQL_Timeout_PreLogin_InitializeConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_PreLogin_InitializeConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00008A8A File Offset: 0x00006C8A
		internal static string SQL_Timeout_PreLogin_SendHandshake
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_PreLogin_SendHandshake", Strings.resourceCulture);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00008AA0 File Offset: 0x00006CA0
		internal static string SQL_Timeout_RoutingDestinationInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_Timeout_RoutingDestinationInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00008AB6 File Offset: 0x00006CB6
		internal static string SQL_TimeOverflow
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_TimeOverflow", Strings.resourceCulture);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00008ACC File Offset: 0x00006CCC
		internal static string SQL_TimeScaleValueOutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_TimeScaleValueOutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00008AE2 File Offset: 0x00006CE2
		internal static string SQL_TooManyValues
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_TooManyValues", Strings.resourceCulture);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00008AF8 File Offset: 0x00006CF8
		internal static string SQL_TypeName
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_TypeName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00008B0E File Offset: 0x00006D0E
		internal static string SQL_UDTTypeName
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UDTTypeName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00008B24 File Offset: 0x00006D24
		internal static string SQL_UnexpectedSmiEvent
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UnexpectedSmiEvent", Strings.resourceCulture);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00008B3A File Offset: 0x00006D3A
		internal static string SQL_UnknownSysTxIsolationLevel
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UnknownSysTxIsolationLevel", Strings.resourceCulture);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00008B50 File Offset: 0x00006D50
		internal static string SQL_UnsupportedAuthentication
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UnsupportedAuthentication", Strings.resourceCulture);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00008B66 File Offset: 0x00006D66
		internal static string SQL_UnsupportedAuthenticationByProvider
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UnsupportedAuthenticationByProvider", Strings.resourceCulture);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00008B7C File Offset: 0x00006D7C
		internal static string SQL_UnsupportedAuthenticationSpecified
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UnsupportedAuthenticationSpecified", Strings.resourceCulture);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00008B92 File Offset: 0x00006D92
		internal static string SQL_UnsupportedSqlAuthenticationMethod
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UnsupportedSqlAuthenticationMethod", Strings.resourceCulture);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00008BA8 File Offset: 0x00006DA8
		internal static string SQL_UserInstanceFailoverNotCompatible
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UserInstanceFailoverNotCompatible", Strings.resourceCulture);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00008BBE File Offset: 0x00006DBE
		internal static string SQL_UserInstanceFailure
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UserInstanceFailure", Strings.resourceCulture);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00008BD4 File Offset: 0x00006DD4
		internal static string SQL_UserInstanceNotAvailableInProc
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_UserInstanceNotAvailableInProc", Strings.resourceCulture);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00008BEA File Offset: 0x00006DEA
		internal static string SQL_WrongType
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_WrongType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00008C00 File Offset: 0x00006E00
		internal static string SQL_XmlReaderNotSupportOnColumnType
		{
			get
			{
				return Strings.ResourceManager.GetString("SQL_XmlReaderNotSupportOnColumnType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00008C16 File Offset: 0x00006E16
		internal static string SqlCommand_Notification
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlCommand_Notification", Strings.resourceCulture);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00008C2C File Offset: 0x00006E2C
		internal static string SqlCommand_NotificationAutoEnlist
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlCommand_NotificationAutoEnlist", Strings.resourceCulture);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00008C42 File Offset: 0x00006E42
		internal static string SqlCommandBuilder_DataAdapter
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlCommandBuilder_DataAdapter", Strings.resourceCulture);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00008C58 File Offset: 0x00006E58
		internal static string SqlCommandBuilder_DecimalSeparator
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlCommandBuilder_DecimalSeparator", Strings.resourceCulture);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00008C6E File Offset: 0x00006E6E
		internal static string SqlCommandBuilder_QuotePrefix
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlCommandBuilder_QuotePrefix", Strings.resourceCulture);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00008C84 File Offset: 0x00006E84
		internal static string SqlCommandBuilder_QuoteSuffix
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlCommandBuilder_QuoteSuffix", Strings.resourceCulture);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00008C9A File Offset: 0x00006E9A
		internal static string SqlConnection_AccessToken
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_AccessToken", Strings.resourceCulture);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00008CB0 File Offset: 0x00006EB0
		internal static string SqlConnection_ClientConnectionId
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_ClientConnectionId", Strings.resourceCulture);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00008CC6 File Offset: 0x00006EC6
		internal static string SqlConnection_ConnectionString
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_ConnectionString", Strings.resourceCulture);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00008CDC File Offset: 0x00006EDC
		internal static string SqlConnection_ConnectionTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_ConnectionTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00008CF2 File Offset: 0x00006EF2
		internal static string SqlConnection_Credential
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_Credential", Strings.resourceCulture);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00008D08 File Offset: 0x00006F08
		internal static string SqlConnection_CustomColumnEncryptionKeyStoreProviders
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_CustomColumnEncryptionKeyStoreProviders", Strings.resourceCulture);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00008D1E File Offset: 0x00006F1E
		internal static string SqlConnection_Database
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_Database", Strings.resourceCulture);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00008D34 File Offset: 0x00006F34
		internal static string SqlConnection_DataSource
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_DataSource", Strings.resourceCulture);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00008D4A File Offset: 0x00006F4A
		internal static string SqlConnection_PacketSize
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_PacketSize", Strings.resourceCulture);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00008D60 File Offset: 0x00006F60
		internal static string SqlConnection_Replication
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_Replication", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00008D76 File Offset: 0x00006F76
		internal static string SqlConnection_ServerProcessId
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_ServerProcessId", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00008D8C File Offset: 0x00006F8C
		internal static string SqlConnection_ServerVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_ServerVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00008DA2 File Offset: 0x00006FA2
		internal static string SqlConnection_StatisticsEnabled
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_StatisticsEnabled", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00008DB8 File Offset: 0x00006FB8
		internal static string SqlConnection_WorkstationId
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConnection_WorkstationId", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00008DCE File Offset: 0x00006FCE
		internal static string SqlConvert_ConvertFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlConvert_ConvertFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00008DE4 File Offset: 0x00006FE4
		internal static string SQLCR_AllAttemptsFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLCR_AllAttemptsFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00008DFA File Offset: 0x00006FFA
		internal static string SQLCR_EncryptionChanged
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLCR_EncryptionChanged", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00008E10 File Offset: 0x00007010
		internal static string SQLCR_InvalidConnectRetryCountValue
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLCR_InvalidConnectRetryCountValue", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00008E26 File Offset: 0x00007026
		internal static string SQLCR_InvalidConnectRetryIntervalValue
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLCR_InvalidConnectRetryIntervalValue", Strings.resourceCulture);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00008E3C File Offset: 0x0000703C
		internal static string SQLCR_NextAttemptWillExceedQueryTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLCR_NextAttemptWillExceedQueryTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00008E52 File Offset: 0x00007052
		internal static string SQLCR_NoCRAckAtReconnection
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLCR_NoCRAckAtReconnection", Strings.resourceCulture);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00008E68 File Offset: 0x00007068
		internal static string SQLCR_TDSVestionNotPreserved
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLCR_TDSVestionNotPreserved", Strings.resourceCulture);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00008E7E File Offset: 0x0000707E
		internal static string SQLCR_UnrecoverableClient
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLCR_UnrecoverableClient", Strings.resourceCulture);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00008E94 File Offset: 0x00007094
		internal static string SQLCR_UnrecoverableServer
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLCR_UnrecoverableServer", Strings.resourceCulture);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00008EAA File Offset: 0x000070AA
		internal static string SqlDelegatedTransaction_PromotionFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDelegatedTransaction_PromotionFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00008EC0 File Offset: 0x000070C0
		internal static string SqlDependency_AddCommandDependency
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_AddCommandDependency", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00008ED6 File Offset: 0x000070D6
		internal static string SqlDependency_DatabaseBrokerDisabled
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_DatabaseBrokerDisabled", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00008EEC File Offset: 0x000070EC
		internal static string SqlDependency_DefaultOptionsButNoStart
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_DefaultOptionsButNoStart", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00008F02 File Offset: 0x00007102
		internal static string SqlDependency_Duplicate
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_Duplicate", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00008F18 File Offset: 0x00007118
		internal static string SqlDependency_DuplicateStart
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_DuplicateStart", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00008F2E File Offset: 0x0000712E
		internal static string SqlDependency_EventNoDuplicate
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_EventNoDuplicate", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00008F44 File Offset: 0x00007144
		internal static string SqlDependency_HasChanges
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_HasChanges", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00008F5A File Offset: 0x0000715A
		internal static string SqlDependency_Id
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_Id", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00008F70 File Offset: 0x00007170
		internal static string SqlDependency_IdMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_IdMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00008F86 File Offset: 0x00007186
		internal static string SqlDependency_InvalidTimeout
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_InvalidTimeout", Strings.resourceCulture);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00008F9C File Offset: 0x0000719C
		internal static string SqlDependency_NoMatchingServerDatabaseStart
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_NoMatchingServerDatabaseStart", Strings.resourceCulture);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00008FB2 File Offset: 0x000071B2
		internal static string SqlDependency_NoMatchingServerStart
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_NoMatchingServerStart", Strings.resourceCulture);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00008FC8 File Offset: 0x000071C8
		internal static string SqlDependency_OnChange
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_OnChange", Strings.resourceCulture);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00008FDE File Offset: 0x000071DE
		internal static string SqlDependency_SqlDependency
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_SqlDependency", Strings.resourceCulture);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00008FF4 File Offset: 0x000071F4
		internal static string SqlDependency_UnexpectedValueOnDeserialize
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlDependency_UnexpectedValueOnDeserialize", Strings.resourceCulture);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000900A File Offset: 0x0000720A
		internal static string SqlFileStream_FileAlreadyInTransaction
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlFileStream_FileAlreadyInTransaction", Strings.resourceCulture);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00009020 File Offset: 0x00007220
		internal static string SqlFileStream_InvalidParameter
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlFileStream_InvalidParameter", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00009036 File Offset: 0x00007236
		internal static string SqlFileStream_InvalidPath
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlFileStream_InvalidPath", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000904C File Offset: 0x0000724C
		internal static string SqlFileStream_PathNotValidDiskResource
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlFileStream_PathNotValidDiskResource", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00009062 File Offset: 0x00007262
		internal static string SqlMetaData_InvalidSqlDbTypeForConstructorFormat
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMetaData_InvalidSqlDbTypeForConstructorFormat", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00009078 File Offset: 0x00007278
		internal static string SqlMetaData_NameTooLong
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMetaData_NameTooLong", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000908E File Offset: 0x0000728E
		internal static string SqlMetaData_NoMetadata
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMetaData_NoMetadata", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000090A4 File Offset: 0x000072A4
		internal static string SqlMetaData_SpecifyBothSortOrderAndOrdinal
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMetaData_SpecifyBothSortOrderAndOrdinal", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000090BA File Offset: 0x000072BA
		internal static string SqlMisc_AlreadyFilledMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_AlreadyFilledMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000090D0 File Offset: 0x000072D0
		internal static string SqlMisc_ArithOverflowMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_ArithOverflowMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000090E6 File Offset: 0x000072E6
		internal static string SqlMisc_BufferInsufficientMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_BufferInsufficientMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x000090FC File Offset: 0x000072FC
		internal static string SqlMisc_ClosedXmlReaderMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_ClosedXmlReaderMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00009112 File Offset: 0x00007312
		internal static string SqlMisc_CompareDiffCollationMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_CompareDiffCollationMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00009128 File Offset: 0x00007328
		internal static string SqlMisc_ConcatDiffCollationMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_ConcatDiffCollationMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000913E File Offset: 0x0000733E
		internal static string SqlMisc_ConversionOverflowMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_ConversionOverflowMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00009154 File Offset: 0x00007354
		internal static string SqlMisc_DateTimeOverflowMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_DateTimeOverflowMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000916A File Offset: 0x0000736A
		internal static string SqlMisc_DivideByZeroMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_DivideByZeroMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00009180 File Offset: 0x00007380
		internal static string SqlMisc_FormatMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_FormatMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00009196 File Offset: 0x00007396
		internal static string SqlMisc_InvalidArraySizeMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_InvalidArraySizeMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000091AC File Offset: 0x000073AC
		internal static string SqlMisc_InvalidDateTimeMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_InvalidDateTimeMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000091C2 File Offset: 0x000073C2
		internal static string SqlMisc_InvalidFirstDayMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_InvalidFirstDayMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x000091D8 File Offset: 0x000073D8
		internal static string SqlMisc_InvalidFlagMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_InvalidFlagMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000091EE File Offset: 0x000073EE
		internal static string SqlMisc_InvalidOpStreamClosed
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_InvalidOpStreamClosed", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00009204 File Offset: 0x00007404
		internal static string SqlMisc_InvalidOpStreamNonReadable
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_InvalidOpStreamNonReadable", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000921A File Offset: 0x0000741A
		internal static string SqlMisc_InvalidOpStreamNonSeekable
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_InvalidOpStreamNonSeekable", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00009230 File Offset: 0x00007430
		internal static string SqlMisc_InvalidOpStreamNonWritable
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_InvalidOpStreamNonWritable", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00009246 File Offset: 0x00007446
		internal static string SqlMisc_InvalidPrecScaleMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_InvalidPrecScaleMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000925C File Offset: 0x0000745C
		internal static string SqlMisc_LenTooLargeMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_LenTooLargeMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00009272 File Offset: 0x00007472
		internal static string SqlMisc_MessageString
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_MessageString", Strings.resourceCulture);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00009288 File Offset: 0x00007488
		internal static string SqlMisc_NoBufferMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_NoBufferMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000929E File Offset: 0x0000749E
		internal static string SqlMisc_NotFilledMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_NotFilledMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x000092B4 File Offset: 0x000074B4
		internal static string SqlMisc_NullString
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_NullString", Strings.resourceCulture);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x000092CA File Offset: 0x000074CA
		internal static string SqlMisc_NullValueMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_NullValueMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x000092E0 File Offset: 0x000074E0
		internal static string SqlMisc_NumeToDecOverflowMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_NumeToDecOverflowMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000092F6 File Offset: 0x000074F6
		internal static string SqlMisc_SetNonZeroLenOnNullMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_SetNonZeroLenOnNullMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000930C File Offset: 0x0000750C
		internal static string SqlMisc_SqlTypeMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_SqlTypeMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00009322 File Offset: 0x00007522
		internal static string SqlMisc_StreamClosedMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_StreamClosedMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00009338 File Offset: 0x00007538
		internal static string SqlMisc_StreamErrorMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_StreamErrorMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000934E File Offset: 0x0000754E
		internal static string SqlMisc_SubclassMustOverride
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_SubclassMustOverride", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00009364 File Offset: 0x00007564
		internal static string SqlMisc_TimeZoneSpecifiedMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_TimeZoneSpecifiedMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000937A File Offset: 0x0000757A
		internal static string SqlMisc_TruncationMaxDataMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_TruncationMaxDataMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00009390 File Offset: 0x00007590
		internal static string SqlMisc_TruncationMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_TruncationMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000093A6 File Offset: 0x000075A6
		internal static string SqlMisc_WriteNonZeroOffsetOnNullMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_WriteNonZeroOffsetOnNullMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000093BC File Offset: 0x000075BC
		internal static string SqlMisc_WriteOffsetLargerThanLenMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlMisc_WriteOffsetLargerThanLenMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000093D2 File Offset: 0x000075D2
		internal static string SQLMSF_FailoverPartnerNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLMSF_FailoverPartnerNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000093E8 File Offset: 0x000075E8
		internal static string SQLNotify_AlreadyHasCommand
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLNotify_AlreadyHasCommand", Strings.resourceCulture);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000093FE File Offset: 0x000075FE
		internal static string SQLNotify_ErrorFormat
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLNotify_ErrorFormat", Strings.resourceCulture);
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00009414 File Offset: 0x00007614
		internal static string SqlNotify_SqlDepCannotBeCreatedInProc
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlNotify_SqlDepCannotBeCreatedInProc", Strings.resourceCulture);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000942A File Offset: 0x0000762A
		internal static string SqlParameter_DBNullNotSupportedForTVP
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_DBNullNotSupportedForTVP", Strings.resourceCulture);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00009440 File Offset: 0x00007640
		internal static string SqlParameter_InvalidTableDerivedPrecisionForTvp
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_InvalidTableDerivedPrecisionForTvp", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00009456 File Offset: 0x00007656
		internal static string SqlParameter_Offset
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_Offset", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000946C File Offset: 0x0000766C
		internal static string SqlParameter_ParameterName
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_ParameterName", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00009482 File Offset: 0x00007682
		internal static string SqlParameter_SqlDbType
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_SqlDbType", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00009498 File Offset: 0x00007698
		internal static string SqlParameter_TypeName
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_TypeName", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000094AE File Offset: 0x000076AE
		internal static string SqlParameter_UnexpectedTypeNameForNonStruct
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_UnexpectedTypeNameForNonStruct", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x000094C4 File Offset: 0x000076C4
		internal static string SqlParameter_UnsupportedTVPOutputParameter
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_UnsupportedTVPOutputParameter", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x000094DA File Offset: 0x000076DA
		internal static string SqlParameter_XmlSchemaCollectionDatabase
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_XmlSchemaCollectionDatabase", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000094F0 File Offset: 0x000076F0
		internal static string SqlParameter_XmlSchemaCollectionName
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_XmlSchemaCollectionName", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00009506 File Offset: 0x00007706
		internal static string SqlParameter_XmlSchemaCollectionOwningSchema
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlParameter_XmlSchemaCollectionOwningSchema", Strings.resourceCulture);
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000951C File Offset: 0x0000771C
		internal static string SqlPipe_AlreadyHasAnOpenResultSet
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlPipe_AlreadyHasAnOpenResultSet", Strings.resourceCulture);
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00009532 File Offset: 0x00007732
		internal static string SqlPipe_CommandHookedUpToNonContextConnection
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlPipe_CommandHookedUpToNonContextConnection", Strings.resourceCulture);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00009548 File Offset: 0x00007748
		internal static string SqlPipe_DoesNotHaveAnOpenResultSet
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlPipe_DoesNotHaveAnOpenResultSet", Strings.resourceCulture);
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000955E File Offset: 0x0000775E
		internal static string SqlPipe_IsBusy
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlPipe_IsBusy", Strings.resourceCulture);
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00009574 File Offset: 0x00007774
		internal static string SqlPipe_MessageTooLong
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlPipe_MessageTooLong", Strings.resourceCulture);
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000958A File Offset: 0x0000778A
		internal static string SqlProvider_DuplicateSortOrdinal
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlProvider_DuplicateSortOrdinal", Strings.resourceCulture);
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x000095A0 File Offset: 0x000077A0
		internal static string SqlProvider_InvalidDataColumnMaxLength
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlProvider_InvalidDataColumnMaxLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000095B6 File Offset: 0x000077B6
		internal static string SqlProvider_InvalidDataColumnType
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlProvider_InvalidDataColumnType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x000095CC File Offset: 0x000077CC
		internal static string SqlProvider_MissingSortOrdinal
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlProvider_MissingSortOrdinal", Strings.resourceCulture);
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x000095E2 File Offset: 0x000077E2
		internal static string SqlProvider_NotEnoughColumnsInStructuredType
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlProvider_NotEnoughColumnsInStructuredType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x000095F8 File Offset: 0x000077F8
		internal static string SqlProvider_SortOrdinalGreaterThanFieldCount
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlProvider_SortOrdinalGreaterThanFieldCount", Strings.resourceCulture);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000960E File Offset: 0x0000780E
		internal static string SqlRetryLogic_InvalidMinMaxPair
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlRetryLogic_InvalidMinMaxPair", Strings.resourceCulture);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00009624 File Offset: 0x00007824
		internal static string SqlRetryLogic_InvalidRange
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlRetryLogic_InvalidRange", Strings.resourceCulture);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000963A File Offset: 0x0000783A
		internal static string SqlRetryLogic_RetryCanceled
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlRetryLogic_RetryCanceled", Strings.resourceCulture);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00009650 File Offset: 0x00007850
		internal static string SqlRetryLogic_RetryExceeded
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlRetryLogic_RetryExceeded", Strings.resourceCulture);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00009666 File Offset: 0x00007866
		internal static string SQLROR_FailoverNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLROR_FailoverNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000967C File Offset: 0x0000787C
		internal static string SQLROR_InvalidRoutingInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLROR_InvalidRoutingInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00009692 File Offset: 0x00007892
		internal static string SQLROR_RecursiveRoutingNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLROR_RecursiveRoutingNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000096A8 File Offset: 0x000078A8
		internal static string SQLROR_TimeoutAfterRoutingInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLROR_TimeoutAfterRoutingInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000096BE File Offset: 0x000078BE
		internal static string SQLROR_UnexpectedRoutingInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLROR_UnexpectedRoutingInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000096D4 File Offset: 0x000078D4
		internal static string SQLTVP_TableTypeCanOnlyBeParameter
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLTVP_TableTypeCanOnlyBeParameter", Strings.resourceCulture);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000096EA File Offset: 0x000078EA
		internal static string SQLUDT_CantLoadAssembly
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLUDT_CantLoadAssembly", Strings.resourceCulture);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00009700 File Offset: 0x00007900
		internal static string SQLUDT_InvalidDbId
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLUDT_InvalidDbId", Strings.resourceCulture);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00009716 File Offset: 0x00007916
		internal static string SQLUDT_InvalidSize
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLUDT_InvalidSize", Strings.resourceCulture);
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000972C File Offset: 0x0000792C
		internal static string SQLUDT_InvalidSqlType
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLUDT_InvalidSqlType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00009742 File Offset: 0x00007942
		internal static string SqlUdt_InvalidUdtMessage
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdt_InvalidUdtMessage", Strings.resourceCulture);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00009758 File Offset: 0x00007958
		internal static string SQLUDT_InvalidUdtTypeName
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLUDT_InvalidUdtTypeName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000976E File Offset: 0x0000796E
		internal static string SQLUDT_InWhereClause
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLUDT_InWhereClause", Strings.resourceCulture);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00009784 File Offset: 0x00007984
		internal static string SQLUDT_MaxByteSizeValue
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLUDT_MaxByteSizeValue", Strings.resourceCulture);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000979A File Offset: 0x0000799A
		internal static string SQLUDT_Unexpected
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLUDT_Unexpected", Strings.resourceCulture);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000097B0 File Offset: 0x000079B0
		internal static string SQLUDT_UnexpectedUdtTypeName
		{
			get
			{
				return Strings.ResourceManager.GetString("SQLUDT_UnexpectedUdtTypeName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000097C6 File Offset: 0x000079C6
		internal static string SqlUdtReason_CannotSupportNative
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_CannotSupportNative", Strings.resourceCulture);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000097DC File Offset: 0x000079DC
		internal static string SqlUdtReason_CannotSupportUserDefined
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_CannotSupportUserDefined", Strings.resourceCulture);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000097F2 File Offset: 0x000079F2
		internal static string SqlUdtReason_MaplessNotYetSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_MaplessNotYetSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00009808 File Offset: 0x00007A08
		internal static string SqlUdtReason_MultipleSerFormats
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_MultipleSerFormats", Strings.resourceCulture);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000981E File Offset: 0x00007A1E
		internal static string SqlUdtReason_MultivaluedAssemblyId
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_MultivaluedAssemblyId", Strings.resourceCulture);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00009834 File Offset: 0x00007A34
		internal static string SqlUdtReason_NativeFormatExplictLayoutNotAllowed
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NativeFormatExplictLayoutNotAllowed", Strings.resourceCulture);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000984A File Offset: 0x00007A4A
		internal static string SqlUdtReason_NativeFormatNoFieldSupport
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NativeFormatNoFieldSupport", Strings.resourceCulture);
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00009860 File Offset: 0x00007A60
		internal static string SqlUdtReason_NativeUdtMaxByteSize
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NativeUdtMaxByteSize", Strings.resourceCulture);
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00009876 File Offset: 0x00007A76
		internal static string SqlUdtReason_NativeUdtNotSequentialLayout
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NativeUdtNotSequentialLayout", Strings.resourceCulture);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000988C File Offset: 0x00007A8C
		internal static string SqlUdtReason_NonSerializableField
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NonSerializableField", Strings.resourceCulture);
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x000098A2 File Offset: 0x00007AA2
		internal static string SqlUdtReason_NoPublicConstructor
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NoPublicConstructor", Strings.resourceCulture);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000098B8 File Offset: 0x00007AB8
		internal static string SqlUdtReason_NoPublicConstructors
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NoPublicConstructors", Strings.resourceCulture);
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000098CE File Offset: 0x00007ACE
		internal static string SqlUdtReason_NotNullable
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NotNullable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000098E4 File Offset: 0x00007AE4
		internal static string SqlUdtReason_NotSerializable
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NotSerializable", Strings.resourceCulture);
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000098FA File Offset: 0x00007AFA
		internal static string SqlUdtReason_NoUdtAttribute
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NoUdtAttribute", Strings.resourceCulture);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00009910 File Offset: 0x00007B10
		internal static string SqlUdtReason_NullPropertyMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_NullPropertyMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00009926 File Offset: 0x00007B26
		internal static string SqlUdtReason_ParseMethodMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_ParseMethodMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0000993C File Offset: 0x00007B3C
		internal static string SqlUdtReason_ToStringMethodMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_ToStringMethodMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00009952 File Offset: 0x00007B52
		internal static string SqlUdtReason_TypeNotPublic
		{
			get
			{
				return Strings.ResourceManager.GetString("SqlUdtReason_TypeNotPublic", Strings.resourceCulture);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00009968 File Offset: 0x00007B68
		internal static string StrongTyping_CananotAccessDBNull
		{
			get
			{
				return Strings.ResourceManager.GetString("StrongTyping_CananotAccessDBNull", Strings.resourceCulture);
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000997E File Offset: 0x00007B7E
		internal static string StrongTyping_CananotRemoveRelation
		{
			get
			{
				return Strings.ResourceManager.GetString("StrongTyping_CananotRemoveRelation", Strings.resourceCulture);
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00009994 File Offset: 0x00007B94
		internal static string StrongTyping_CannotRemoveColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("StrongTyping_CannotRemoveColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000099AA File Offset: 0x00007BAA
		internal static string TCE_AttestationInfoNotReturnedFromSQLServer
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_AttestationInfoNotReturnedFromSQLServer", Strings.resourceCulture);
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x000099C0 File Offset: 0x00007BC0
		internal static string TCE_AttestationProtocolNotSpecifiedForGeneratingEnclavePackage
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_AttestationProtocolNotSpecifiedForGeneratingEnclavePackage", Strings.resourceCulture);
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x000099D6 File Offset: 0x00007BD6
		internal static string TCE_AttestationProtocolNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_AttestationProtocolNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x000099EC File Offset: 0x00007BEC
		internal static string TCE_AttestationProtocolNotSupportEnclaveType
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_AttestationProtocolNotSupportEnclaveType", Strings.resourceCulture);
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00009A02 File Offset: 0x00007C02
		internal static string TCE_AttestationURLNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_AttestationURLNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00009A18 File Offset: 0x00007C18
		internal static string TCE_BatchedUpdateColumnEncryptionSettingMismatch
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_BatchedUpdateColumnEncryptionSettingMismatch", Strings.resourceCulture);
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00009A2E File Offset: 0x00007C2E
		internal static string TCE_CannotCreateSqlColumnEncryptionEnclaveProvider
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_CannotCreateSqlColumnEncryptionEnclaveProvider", Strings.resourceCulture);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00009A44 File Offset: 0x00007C44
		internal static string TCE_CannotGetSqlColumnEncryptionEnclaveProviderConfig
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_CannotGetSqlColumnEncryptionEnclaveProviderConfig", Strings.resourceCulture);
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00009A5A File Offset: 0x00007C5A
		internal static string TCE_CanOnlyCallOnce
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_CanOnlyCallOnce", Strings.resourceCulture);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00009A70 File Offset: 0x00007C70
		internal static string TCE_CertificateNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_CertificateNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00009A86 File Offset: 0x00007C86
		internal static string TCE_CertificateNotFoundSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_CertificateNotFoundSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00009A9C File Offset: 0x00007C9C
		internal static string TCE_CertificateWithNoPrivateKey
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_CertificateWithNoPrivateKey", Strings.resourceCulture);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00009AB2 File Offset: 0x00007CB2
		internal static string TCE_CertificateWithNoPrivateKeySysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_CertificateWithNoPrivateKeySysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00009AC8 File Offset: 0x00007CC8
		internal static string TCE_ColumnDecryptionFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ColumnDecryptionFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00009ADE File Offset: 0x00007CDE
		internal static string TCE_ColumnEncryptionKeysNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ColumnEncryptionKeysNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00009AF4 File Offset: 0x00007CF4
		internal static string TCE_ColumnMasterKeySignatureNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ColumnMasterKeySignatureNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00009B0A File Offset: 0x00007D0A
		internal static string TCE_ColumnMasterKeySignatureVerificationFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ColumnMasterKeySignatureVerificationFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00009B20 File Offset: 0x00007D20
		internal static string TCE_DbConnectionString_AttestationProtocol
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_DbConnectionString_AttestationProtocol", Strings.resourceCulture);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00009B36 File Offset: 0x00007D36
		internal static string TCE_DbConnectionString_ColumnEncryptionSetting
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_DbConnectionString_ColumnEncryptionSetting", Strings.resourceCulture);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00009B4C File Offset: 0x00007D4C
		internal static string TCE_DbConnectionString_EnclaveAttestationUrl
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_DbConnectionString_EnclaveAttestationUrl", Strings.resourceCulture);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00009B62 File Offset: 0x00007D62
		internal static string TCE_DbConnectionString_IPAddressPreference
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_DbConnectionString_IPAddressPreference", Strings.resourceCulture);
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00009B78 File Offset: 0x00007D78
		internal static string TCE_DecryptionFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_DecryptionFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00009B8E File Offset: 0x00007D8E
		internal static string TCE_EmptyArgumentInConstructorInternal
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyArgumentInConstructorInternal", Strings.resourceCulture);
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00009BA4 File Offset: 0x00007DA4
		internal static string TCE_EmptyArgumentInternal
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyArgumentInternal", Strings.resourceCulture);
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00009BBA File Offset: 0x00007DBA
		internal static string TCE_EmptyCertificateThumbprint
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCertificateThumbprint", Strings.resourceCulture);
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00009BD0 File Offset: 0x00007DD0
		internal static string TCE_EmptyCertificateThumbprintSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCertificateThumbprintSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00009BE6 File Offset: 0x00007DE6
		internal static string TCE_EmptyCngKeyId
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCngKeyId", Strings.resourceCulture);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00009BFC File Offset: 0x00007DFC
		internal static string TCE_EmptyCngKeyIdSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCngKeyIdSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00009C12 File Offset: 0x00007E12
		internal static string TCE_EmptyCngName
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCngName", Strings.resourceCulture);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00009C28 File Offset: 0x00007E28
		internal static string TCE_EmptyCngNameSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCngNameSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00009C3E File Offset: 0x00007E3E
		internal static string TCE_EmptyColumnEncryptionKey
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyColumnEncryptionKey", Strings.resourceCulture);
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00009C54 File Offset: 0x00007E54
		internal static string TCE_EmptyCspKeyId
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCspKeyId", Strings.resourceCulture);
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00009C6A File Offset: 0x00007E6A
		internal static string TCE_EmptyCspKeyIdSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCspKeyIdSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00009C80 File Offset: 0x00007E80
		internal static string TCE_EmptyCspName
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCspName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00009C96 File Offset: 0x00007E96
		internal static string TCE_EmptyCspNameSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyCspNameSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00009CAC File Offset: 0x00007EAC
		internal static string TCE_EmptyEncryptedColumnEncryptionKey
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyEncryptedColumnEncryptionKey", Strings.resourceCulture);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00009CC2 File Offset: 0x00007EC2
		internal static string TCE_EmptyProviderName
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EmptyProviderName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00009CD8 File Offset: 0x00007ED8
		internal static string TCE_EnclaveComputationsNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EnclaveComputationsNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00009CEE File Offset: 0x00007EEE
		internal static string TCE_EnclaveProviderNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EnclaveProviderNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00009D04 File Offset: 0x00007F04
		internal static string TCE_EnclaveProvidersNotConfiguredForEnclaveBasedQuery
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EnclaveProvidersNotConfiguredForEnclaveBasedQuery", Strings.resourceCulture);
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00009D1A File Offset: 0x00007F1A
		internal static string TCE_EnclaveTypeNotReturned
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EnclaveTypeNotReturned", Strings.resourceCulture);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00009D30 File Offset: 0x00007F30
		internal static string TCE_EnclaveTypeNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EnclaveTypeNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00009D46 File Offset: 0x00007F46
		internal static string TCE_EnclaveTypeNullForEnclaveBasedQuery
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_EnclaveTypeNullForEnclaveBasedQuery", Strings.resourceCulture);
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00009D5C File Offset: 0x00007F5C
		internal static string TCE_ExceptionWhenGeneratingEnclavePackage
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ExceptionWhenGeneratingEnclavePackage", Strings.resourceCulture);
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00009D72 File Offset: 0x00007F72
		internal static string TCE_FailedToEncryptRegisterRulesBytePackage
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_FailedToEncryptRegisterRulesBytePackage", Strings.resourceCulture);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00009D88 File Offset: 0x00007F88
		internal static string TCE_InsufficientBuffer
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InsufficientBuffer", Strings.resourceCulture);
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00009D9E File Offset: 0x00007F9E
		internal static string TCE_InvalidAlgorithmVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidAlgorithmVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00009DB4 File Offset: 0x00007FB4
		internal static string TCE_InvalidAlgorithmVersionInEncryptedCEK
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidAlgorithmVersionInEncryptedCEK", Strings.resourceCulture);
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00009DCA File Offset: 0x00007FCA
		internal static string TCE_InvalidAttestationParameterUnableToConvertToUnsignedInt
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidAttestationParameterUnableToConvertToUnsignedInt", Strings.resourceCulture);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00009DE0 File Offset: 0x00007FE0
		internal static string TCE_InvalidAuthenticationTag
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidAuthenticationTag", Strings.resourceCulture);
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00009DF6 File Offset: 0x00007FF6
		internal static string TCE_InvalidCertificateLocation
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCertificateLocation", Strings.resourceCulture);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00009E0C File Offset: 0x0000800C
		internal static string TCE_InvalidCertificateLocationSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCertificateLocationSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00009E22 File Offset: 0x00008022
		internal static string TCE_InvalidCertificatePath
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCertificatePath", Strings.resourceCulture);
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00009E38 File Offset: 0x00008038
		internal static string TCE_InvalidCertificatePathSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCertificatePathSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00009E4E File Offset: 0x0000804E
		internal static string TCE_InvalidCertificateSignature
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCertificateSignature", Strings.resourceCulture);
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00009E64 File Offset: 0x00008064
		internal static string TCE_InvalidCertificateStore
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCertificateStore", Strings.resourceCulture);
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00009E7A File Offset: 0x0000807A
		internal static string TCE_InvalidCertificateStoreSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCertificateStoreSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00009E90 File Offset: 0x00008090
		internal static string TCE_InvalidCiphertextLengthInEncryptedCEK
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCiphertextLengthInEncryptedCEK", Strings.resourceCulture);
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00009EA6 File Offset: 0x000080A6
		internal static string TCE_InvalidCiphertextLengthInEncryptedCEKCng
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCiphertextLengthInEncryptedCEKCng", Strings.resourceCulture);
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00009EBC File Offset: 0x000080BC
		internal static string TCE_InvalidCiphertextLengthInEncryptedCEKCsp
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCiphertextLengthInEncryptedCEKCsp", Strings.resourceCulture);
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00009ED2 File Offset: 0x000080D2
		internal static string TCE_InvalidCipherTextSize
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCipherTextSize", Strings.resourceCulture);
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00009EE8 File Offset: 0x000080E8
		internal static string TCE_InvalidCngKey
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCngKey", Strings.resourceCulture);
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00009EFE File Offset: 0x000080FE
		internal static string TCE_InvalidCngKeySysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCngKeySysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00009F14 File Offset: 0x00008114
		internal static string TCE_InvalidCngPath
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCngPath", Strings.resourceCulture);
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00009F2A File Offset: 0x0000812A
		internal static string TCE_InvalidCngPathSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCngPathSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00009F40 File Offset: 0x00008140
		internal static string TCE_InvalidCspKeyId
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCspKeyId", Strings.resourceCulture);
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00009F56 File Offset: 0x00008156
		internal static string TCE_InvalidCspKeyIdSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCspKeyIdSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00009F6C File Offset: 0x0000816C
		internal static string TCE_InvalidCspName
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCspName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00009F82 File Offset: 0x00008182
		internal static string TCE_InvalidCspNameSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCspNameSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00009F98 File Offset: 0x00008198
		internal static string TCE_InvalidCspPath
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCspPath", Strings.resourceCulture);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00009FAE File Offset: 0x000081AE
		internal static string TCE_InvalidCspPathSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCspPathSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00009FC4 File Offset: 0x000081C4
		internal static string TCE_InvalidCustomKeyStoreProviderName
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidCustomKeyStoreProviderName", Strings.resourceCulture);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00009FDA File Offset: 0x000081DA
		internal static string TCE_InvalidDatabaseIdUnableToCastToUnsignedInt
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidDatabaseIdUnableToCastToUnsignedInt", Strings.resourceCulture);
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00009FF0 File Offset: 0x000081F0
		internal static string TCE_InvalidEncryptionKeyOrdinalEnclaveMetadata
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidEncryptionKeyOrdinalEnclaveMetadata", Strings.resourceCulture);
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000A006 File Offset: 0x00008206
		internal static string TCE_InvalidEncryptionKeyOrdinalParameterMetadata
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidEncryptionKeyOrdinalParameterMetadata", Strings.resourceCulture);
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000A01C File Offset: 0x0000821C
		internal static string TCE_InvalidEncryptionType
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidEncryptionType", Strings.resourceCulture);
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0000A032 File Offset: 0x00008232
		internal static string TCE_InvalidKeyEncryptionAlgorithm
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidKeyEncryptionAlgorithm", Strings.resourceCulture);
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000A048 File Offset: 0x00008248
		internal static string TCE_InvalidKeyEncryptionAlgorithmSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidKeyEncryptionAlgorithmSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000A05E File Offset: 0x0000825E
		internal static string TCE_InvalidKeyIdUnableToCastToUnsignedShort
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidKeyIdUnableToCastToUnsignedShort", Strings.resourceCulture);
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000A074 File Offset: 0x00008274
		internal static string TCE_InvalidKeySize
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidKeySize", Strings.resourceCulture);
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000A08A File Offset: 0x0000828A
		internal static string TCE_InvalidKeyStoreProviderName
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidKeyStoreProviderName", Strings.resourceCulture);
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000A0A0 File Offset: 0x000082A0
		internal static string TCE_InvalidSignature
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidSignature", Strings.resourceCulture);
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000A0B6 File Offset: 0x000082B6
		internal static string TCE_InvalidSignatureInEncryptedCEK
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidSignatureInEncryptedCEK", Strings.resourceCulture);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000A0CC File Offset: 0x000082CC
		internal static string TCE_InvalidSignatureInEncryptedCEKCng
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidSignatureInEncryptedCEKCng", Strings.resourceCulture);
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000A0E2 File Offset: 0x000082E2
		internal static string TCE_InvalidSignatureInEncryptedCEKCsp
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_InvalidSignatureInEncryptedCEKCsp", Strings.resourceCulture);
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000A0F8 File Offset: 0x000082F8
		internal static string TCE_KeyDecryptionFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_KeyDecryptionFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000A10E File Offset: 0x0000830E
		internal static string TCE_KeyDecryptionFailedCertStore
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_KeyDecryptionFailedCertStore", Strings.resourceCulture);
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000A124 File Offset: 0x00008324
		internal static string TCE_LargeCertificatePathLength
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_LargeCertificatePathLength", Strings.resourceCulture);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000A13A File Offset: 0x0000833A
		internal static string TCE_LargeCertificatePathLengthSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_LargeCertificatePathLengthSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000A150 File Offset: 0x00008350
		internal static string TCE_MultipleRowsReturnedForAttestationInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_MultipleRowsReturnedForAttestationInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000A166 File Offset: 0x00008366
		internal static string TCE_NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NoAttestationUrlSpecifiedForEnclaveBasedQueryGeneratingEnclavePackage", Strings.resourceCulture);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000A17C File Offset: 0x0000837C
		internal static string TCE_NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NoAttestationUrlSpecifiedForEnclaveBasedQuerySpDescribe", Strings.resourceCulture);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000A192 File Offset: 0x00008392
		internal static string TCE_NotSupportedByServer
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NotSupportedByServer", Strings.resourceCulture);
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000A1A8 File Offset: 0x000083A8
		internal static string TCE_NullArgumentInConstructorInternal
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullArgumentInConstructorInternal", Strings.resourceCulture);
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000A1BE File Offset: 0x000083BE
		internal static string TCE_NullArgumentInternal
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullArgumentInternal", Strings.resourceCulture);
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000A1D4 File Offset: 0x000083D4
		internal static string TCE_NullCertificatePath
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullCertificatePath", Strings.resourceCulture);
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000A1EA File Offset: 0x000083EA
		internal static string TCE_NullCertificatePathSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullCertificatePathSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000A200 File Offset: 0x00008400
		internal static string TCE_NullCipherText
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullCipherText", Strings.resourceCulture);
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000A216 File Offset: 0x00008416
		internal static string TCE_NullCngPath
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullCngPath", Strings.resourceCulture);
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000A22C File Offset: 0x0000842C
		internal static string TCE_NullCngPathSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullCngPathSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000A242 File Offset: 0x00008442
		internal static string TCE_NullColumnEncryptionAlgorithm
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullColumnEncryptionAlgorithm", Strings.resourceCulture);
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000A258 File Offset: 0x00008458
		internal static string TCE_NullColumnEncryptionKey
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullColumnEncryptionKey", Strings.resourceCulture);
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000A26E File Offset: 0x0000846E
		internal static string TCE_NullColumnEncryptionKeySysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullColumnEncryptionKeySysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000A284 File Offset: 0x00008484
		internal static string TCE_NullCspPath
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullCspPath", Strings.resourceCulture);
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000A29A File Offset: 0x0000849A
		internal static string TCE_NullCspPathSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullCspPathSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000A2B0 File Offset: 0x000084B0
		internal static string TCE_NullCustomKeyStoreProviderDictionary
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullCustomKeyStoreProviderDictionary", Strings.resourceCulture);
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000A2C6 File Offset: 0x000084C6
		internal static string TCE_NullEnclavePackageForEnclaveBasedQuery
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullEnclavePackageForEnclaveBasedQuery", Strings.resourceCulture);
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000A2DC File Offset: 0x000084DC
		internal static string TCE_NullEnclaveSessionDuringQueryExecution
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullEnclaveSessionDuringQueryExecution", Strings.resourceCulture);
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000A2F2 File Offset: 0x000084F2
		internal static string TCE_NullEnclaveSessionReturnedFromProvider
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullEnclaveSessionReturnedFromProvider", Strings.resourceCulture);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000A308 File Offset: 0x00008508
		internal static string TCE_NullEncryptedColumnEncryptionKey
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullEncryptedColumnEncryptionKey", Strings.resourceCulture);
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000A31E File Offset: 0x0000851E
		internal static string TCE_NullKeyEncryptionAlgorithm
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullKeyEncryptionAlgorithm", Strings.resourceCulture);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000A334 File Offset: 0x00008534
		internal static string TCE_NullKeyEncryptionAlgorithmSysErr
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullKeyEncryptionAlgorithmSysErr", Strings.resourceCulture);
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000A34A File Offset: 0x0000854A
		internal static string TCE_NullPlainText
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullPlainText", Strings.resourceCulture);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000A360 File Offset: 0x00008560
		internal static string TCE_NullProviderValue
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_NullProviderValue", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000A376 File Offset: 0x00008576
		internal static string TCE_OffsetOutOfBounds
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_OffsetOutOfBounds", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000A38C File Offset: 0x0000858C
		internal static string TCE_ParamDecryptionFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ParamDecryptionFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000A3A2 File Offset: 0x000085A2
		internal static string TCE_ParamEncryptionFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ParamEncryptionFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000A3B8 File Offset: 0x000085B8
		internal static string TCE_ParamEncryptionMetaDataMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ParamEncryptionMetaDataMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000A3CE File Offset: 0x000085CE
		internal static string TCE_ParamInvalidForceColumnEncryptionSetting
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ParamInvalidForceColumnEncryptionSetting", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000A3E4 File Offset: 0x000085E4
		internal static string TCE_ParamUnExpectedEncryptionMetadata
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ParamUnExpectedEncryptionMetadata", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000A3FA File Offset: 0x000085FA
		internal static string TCE_ProcEncryptionMetaDataMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_ProcEncryptionMetaDataMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000A410 File Offset: 0x00008610
		internal static string TCE_SequentialAccessNotSupportedOnEncryptedColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_SequentialAccessNotSupportedOnEncryptedColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000A426 File Offset: 0x00008626
		internal static string TCE_SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty", Strings.resourceCulture);
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000A43C File Offset: 0x0000863C
		internal static string TCE_SqlCommand_ColumnEncryptionSetting
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_SqlCommand_ColumnEncryptionSetting", Strings.resourceCulture);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000A452 File Offset: 0x00008652
		internal static string TCE_SqlConnection_ColumnEncryptionKeyCacheTtl
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_SqlConnection_ColumnEncryptionKeyCacheTtl", Strings.resourceCulture);
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000A468 File Offset: 0x00008668
		internal static string TCE_SqlConnection_ColumnEncryptionQueryMetadataCacheEnabled
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_SqlConnection_ColumnEncryptionQueryMetadataCacheEnabled", Strings.resourceCulture);
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000A47E File Offset: 0x0000867E
		internal static string TCE_SqlConnection_TrustedColumnMasterKeyPaths
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_SqlConnection_TrustedColumnMasterKeyPaths", Strings.resourceCulture);
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000A494 File Offset: 0x00008694
		internal static string TCE_SqlParameter_ForceColumnEncryption
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_SqlParameter_ForceColumnEncryption", Strings.resourceCulture);
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000A4AA File Offset: 0x000086AA
		internal static string TCE_StreamNotSupportOnEncryptedColumn
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_StreamNotSupportOnEncryptedColumn", Strings.resourceCulture);
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0000A4C0 File Offset: 0x000086C0
		internal static string TCE_UnableToEstablishSecureChannel
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UnableToEstablishSecureChannel", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000A4D6 File Offset: 0x000086D6
		internal static string TCE_UnableToVerifyColumnMasterKeySignature
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UnableToVerifyColumnMasterKeySignature", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000A4EC File Offset: 0x000086EC
		internal static string TCE_UnexpectedDescribeParamFormatAttestationInfo
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UnexpectedDescribeParamFormatAttestationInfo", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000A502 File Offset: 0x00008702
		internal static string TCE_UnexpectedDescribeParamFormatParameterMetadata
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UnexpectedDescribeParamFormatParameterMetadata", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000A518 File Offset: 0x00008718
		internal static string TCE_UnknownColumnEncryptionAlgorithm
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UnknownColumnEncryptionAlgorithm", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000A52E File Offset: 0x0000872E
		internal static string TCE_UnknownColumnEncryptionAlgorithmId
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UnknownColumnEncryptionAlgorithmId", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000A544 File Offset: 0x00008744
		internal static string TCE_UnrecognizedKeyStoreProviderName
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UnrecognizedKeyStoreProviderName", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000A55A File Offset: 0x0000875A
		internal static string TCE_UnsupportedDatatype
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UnsupportedDatatype", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000A570 File Offset: 0x00008770
		internal static string TCE_UnsupportedNormalizationVersion
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UnsupportedNormalizationVersion", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000A586 File Offset: 0x00008786
		internal static string TCE_UntrustedKeyPath
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_UntrustedKeyPath", Strings.resourceCulture);
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0000A59C File Offset: 0x0000879C
		internal static string TCE_VeryLargeCiphertext
		{
			get
			{
				return Strings.ResourceManager.GetString("TCE_VeryLargeCiphertext", Strings.resourceCulture);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000A5B2 File Offset: 0x000087B2
		internal static string VerifyEnclaveDebuggable
		{
			get
			{
				return Strings.ResourceManager.GetString("VerifyEnclaveDebuggable", Strings.resourceCulture);
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000A5C8 File Offset: 0x000087C8
		internal static string VerifyEnclavePolicyFailedFormat
		{
			get
			{
				return Strings.ResourceManager.GetString("VerifyEnclavePolicyFailedFormat", Strings.resourceCulture);
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000A5DE File Offset: 0x000087DE
		internal static string VerifyEnclaveReportFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("VerifyEnclaveReportFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000A5F4 File Offset: 0x000087F4
		internal static string VerifyEnclaveReportFormatFailed
		{
			get
			{
				return Strings.ResourceManager.GetString("VerifyEnclaveReportFormatFailed", Strings.resourceCulture);
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000A60A File Offset: 0x0000880A
		internal static string VerifyHealthCertificateChainFormat
		{
			get
			{
				return Strings.ResourceManager.GetString("VerifyHealthCertificateChainFormat", Strings.resourceCulture);
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000A620 File Offset: 0x00008820
		internal static string Xml_AttributeValues
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_AttributeValues", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000A636 File Offset: 0x00008836
		internal static string Xml_CannotConvert
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_CannotConvert", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000A64C File Offset: 0x0000884C
		internal static string Xml_CanNotDeserializeObjectType
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_CanNotDeserializeObjectType", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000A662 File Offset: 0x00008862
		internal static string Xml_CannotInstantiateAbstract
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_CannotInstantiateAbstract", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000A678 File Offset: 0x00008878
		internal static string Xml_CircularComplexType
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_CircularComplexType", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000A68E File Offset: 0x0000888E
		internal static string Xml_ColumnConflict
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_ColumnConflict", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000A6A4 File Offset: 0x000088A4
		internal static string Xml_DataTableInferenceNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_DataTableInferenceNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000A6BA File Offset: 0x000088BA
		internal static string Xml_DatatypeNotDefined
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_DatatypeNotDefined", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000A6D0 File Offset: 0x000088D0
		internal static string Xml_DuplicateConstraint
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_DuplicateConstraint", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000A6E6 File Offset: 0x000088E6
		internal static string Xml_DynamicWithoutXmlSerializable
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_DynamicWithoutXmlSerializable", Strings.resourceCulture);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000A6FC File Offset: 0x000088FC
		internal static string Xml_ElementTypeNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_ElementTypeNotFound", Strings.resourceCulture);
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000A712 File Offset: 0x00008912
		internal static string Xml_FoundEntity
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_FoundEntity", Strings.resourceCulture);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000A728 File Offset: 0x00008928
		internal static string Xml_InvalidField
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_InvalidField", Strings.resourceCulture);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000A73E File Offset: 0x0000893E
		internal static string Xml_InvalidKey
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_InvalidKey", Strings.resourceCulture);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000A754 File Offset: 0x00008954
		internal static string Xml_InvalidPrefix
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_InvalidPrefix", Strings.resourceCulture);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000A76A File Offset: 0x0000896A
		internal static string Xml_InvalidSelector
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_InvalidSelector", Strings.resourceCulture);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000A780 File Offset: 0x00008980
		internal static string Xml_IsDataSetAttributeMissingInSchema
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_IsDataSetAttributeMissingInSchema", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000A796 File Offset: 0x00008996
		internal static string Xml_MergeDuplicateDeclaration
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_MergeDuplicateDeclaration", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000A7AC File Offset: 0x000089AC
		internal static string Xml_MismatchKeyLength
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_MismatchKeyLength", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000A7C2 File Offset: 0x000089C2
		internal static string Xml_MissingAttribute
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_MissingAttribute", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000A7D8 File Offset: 0x000089D8
		internal static string Xml_MissingRefer
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_MissingRefer", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000A7EE File Offset: 0x000089EE
		internal static string Xml_MissingSQL
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_MissingSQL", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000A804 File Offset: 0x00008A04
		internal static string Xml_MissingTable
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_MissingTable", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000A81A File Offset: 0x00008A1A
		internal static string Xml_MultipleParentRows
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_MultipleParentRows", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000A830 File Offset: 0x00008A30
		internal static string Xml_MultipleTargetConverterEmpty
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_MultipleTargetConverterEmpty", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000A846 File Offset: 0x00008A46
		internal static string Xml_MultipleTargetConverterError
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_MultipleTargetConverterError", Strings.resourceCulture);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000A85C File Offset: 0x00008A5C
		internal static string Xml_NestedCircular
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_NestedCircular", Strings.resourceCulture);
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000A872 File Offset: 0x00008A72
		internal static string Xml_PolymorphismNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_PolymorphismNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0000A888 File Offset: 0x00008A88
		internal static string Xml_RelationChildKeyMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_RelationChildKeyMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000A89E File Offset: 0x00008A9E
		internal static string Xml_RelationChildNameMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_RelationChildNameMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		internal static string Xml_RelationParentNameMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_RelationParentNameMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000A8CA File Offset: 0x00008ACA
		internal static string Xml_RelationTableKeyMissing
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_RelationTableKeyMissing", Strings.resourceCulture);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		internal static string Xml_SimpleTypeNotSupported
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_SimpleTypeNotSupported", Strings.resourceCulture);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000A8F6 File Offset: 0x00008AF6
		internal static string Xml_TooManyIsDataSetAtributeInSchema
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_TooManyIsDataSetAtributeInSchema", Strings.resourceCulture);
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000A90C File Offset: 0x00008B0C
		internal static string Xml_UndefinedDatatype
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_UndefinedDatatype", Strings.resourceCulture);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000A922 File Offset: 0x00008B22
		internal static string Xml_ValueOutOfRange
		{
			get
			{
				return Strings.ResourceManager.GetString("Xml_ValueOutOfRange", Strings.resourceCulture);
			}
		}

		// Token: 0x0400000A RID: 10
		private static ResourceManager resourceMan;

		// Token: 0x0400000B RID: 11
		private static CultureInfo resourceCulture;
	}
}
