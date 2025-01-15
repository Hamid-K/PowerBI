using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A3E RID: 2622
	internal class Resources
	{
		// Token: 0x060051F4 RID: 20980 RVA: 0x00002061 File Offset: 0x00000261
		private Resources()
		{
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x060051F5 RID: 20981 RVA: 0x0014E397 File Offset: 0x0014C597
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceManager == null)
				{
					Resources.resourceManager = new ResourceManager("Microsoft.HostIntegration.DrdaClient.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceManager;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x060051F6 RID: 20982 RVA: 0x0014E3C3 File Offset: 0x0014C5C3
		// (set) Token: 0x060051F7 RID: 20983 RVA: 0x0014E3CA File Offset: 0x0014C5CA
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x060051F8 RID: 20984 RVA: 0x0014E3D2 File Offset: 0x0014C5D2
		internal static string Drda_ClosedConnectionError
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_ClosedConnectionError", Resources.Culture);
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x060051F9 RID: 20985 RVA: 0x0014E3E8 File Offset: 0x0014C5E8
		internal static string Drda_NoConnectionString
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_NoConnectionString", Resources.Culture);
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x060051FA RID: 20986 RVA: 0x0014E3FE File Offset: 0x0014C5FE
		internal static string Drda_EmptyDatabaseName
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_EmptyDatabaseName", Resources.Culture);
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x060051FB RID: 20987 RVA: 0x0014E414 File Offset: 0x0014C614
		internal static string Drda_InvalidConnectTimeoutValue
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_InvalidConnectTimeoutValue", Resources.Culture);
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x060051FC RID: 20988 RVA: 0x0014E42A File Offset: 0x0014C62A
		internal static string Drda_UdlFileError
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_UdlFileError", Resources.Culture);
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x060051FD RID: 20989 RVA: 0x0014E440 File Offset: 0x0014C640
		internal static string Drda_InvalidUDL
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_InvalidUDL", Resources.Culture);
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x060051FE RID: 20990 RVA: 0x0014E456 File Offset: 0x0014C656
		internal static string Drda_TableListNotSupported
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_TableListNotSupported", Resources.Culture);
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x060051FF RID: 20991 RVA: 0x0014E46C File Offset: 0x0014C66C
		internal static string Drda_AlreadyEndTransaction
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_AlreadyEndTransaction", Resources.Culture);
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06005200 RID: 20992 RVA: 0x0014E482 File Offset: 0x0014C682
		internal static string Drda_NoActiveTransaction
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_NoActiveTransaction", Resources.Culture);
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06005201 RID: 20993 RVA: 0x0014E498 File Offset: 0x0014C698
		internal static string Drda_SchemaNotSupported
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_SchemaNotSupported", Resources.Culture);
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06005202 RID: 20994 RVA: 0x0014E4AE File Offset: 0x0014C6AE
		internal static string Drda_BadDataNoConversion
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_BadDataNoConversion", Resources.Culture);
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06005203 RID: 20995 RVA: 0x0014E4C4 File Offset: 0x0014C6C4
		internal static string Drda_BadDataOutOfRange
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_BadDataOutOfRange", Resources.Culture);
			}
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06005204 RID: 20996 RVA: 0x0014E4DA File Offset: 0x0014C6DA
		internal static string Drda_BadDataInvalidDateTime
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_BadDataInvalidDateTime", Resources.Culture);
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06005205 RID: 20997 RVA: 0x0014E4F0 File Offset: 0x0014C6F0
		internal static string Drda_BadDataInvalidCast
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_BadDataInvalidCast", Resources.Culture);
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06005206 RID: 20998 RVA: 0x0014E506 File Offset: 0x0014C706
		internal static string Drda_BadDataGeneralError
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_BadDataGeneralError", Resources.Culture);
			}
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06005207 RID: 20999 RVA: 0x0014E51C File Offset: 0x0014C71C
		internal static string Drda_DataReaderNoData
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_DataReaderNoData", Resources.Culture);
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06005208 RID: 21000 RVA: 0x0014E532 File Offset: 0x0014C732
		internal static string Drda_OpenReaderExists
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_OpenReaderExists", Resources.Culture);
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06005209 RID: 21001 RVA: 0x0014E548 File Offset: 0x0014C748
		internal static string Drda_NotPartOfTransaction
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_NotPartOfTransaction", Resources.Culture);
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x0600520A RID: 21002 RVA: 0x0014E55E File Offset: 0x0014C75E
		internal static string InternalNetworkError
		{
			get
			{
				return Resources.ResourceManager.GetString("InternalNetworkError", Resources.Culture);
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x0600520B RID: 21003 RVA: 0x0014E574 File Offset: 0x0014C774
		internal static string Drda_NoLocalTransactionInDistributedContext
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_NoLocalTransactionInDistributedContext", Resources.Culture);
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x0600520C RID: 21004 RVA: 0x0014E58A File Offset: 0x0014C78A
		internal static string Drda_NoDistributedTransactions
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_NoDistributedTransactions", Resources.Culture);
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x0600520D RID: 21005 RVA: 0x0014E5A0 File Offset: 0x0014C7A0
		internal static string Drda_NoLocalTransactions
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_NoLocalTransactions", Resources.Culture);
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x0600520E RID: 21006 RVA: 0x0014E5B6 File Offset: 0x0014C7B6
		internal static string Drda_NoMixedParameters
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_NoMixedParameters", Resources.Culture);
			}
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x0600520F RID: 21007 RVA: 0x0014E5CC File Offset: 0x0014C7CC
		internal static string Drda_CommandTextNull
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_CommandTextNull", Resources.Culture);
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06005210 RID: 21008 RVA: 0x0014E5E2 File Offset: 0x0014C7E2
		internal static string Drda_ConnectionTimedOut
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_ConnectionTimedOut", Resources.Culture);
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06005211 RID: 21009 RVA: 0x0014E5F8 File Offset: 0x0014C7F8
		internal static string Drda_NoConnectionsInPool
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_NoConnectionsInPool", Resources.Culture);
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06005212 RID: 21010 RVA: 0x0014E60E File Offset: 0x0014C80E
		internal static string Drda_BatchFailed
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_BatchFailed", Resources.Culture);
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06005213 RID: 21011 RVA: 0x0014E624 File Offset: 0x0014C824
		internal static string Drda_BulkCopyDestinationFieldsNotMatch
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_BulkCopyDestinationFieldsNotMatch", Resources.Culture);
			}
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06005214 RID: 21012 RVA: 0x0014E63A File Offset: 0x0014C83A
		internal static string Drda_InvalidBulkCopyMappingCollectionTypes
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_InvalidBulkCopyMappingCollectionTypes", Resources.Culture);
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06005215 RID: 21013 RVA: 0x0014E650 File Offset: 0x0014C850
		internal static string Drda_InvalidBatchNoParameters
		{
			get
			{
				return Resources.ResourceManager.GetString("Drda_InvalidBatchNoParameters", Resources.Culture);
			}
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x0014E666 File Offset: 0x0014C866
		internal static string Drda_ParameterValueNotSupported(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_ParameterValueNotSupported", Resources.Culture), param0);
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x0014E687 File Offset: 0x0014C887
		internal static string Drda_CollectionIndexInt32(object param0, object param1, object param2)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_CollectionIndexInt32", Resources.Culture), param0, param1, param2);
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x0014E6AA File Offset: 0x0014C8AA
		internal static string Drda_CollectionIndexString(object param0, object param1, object param2, object param3)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_CollectionIndexString", Resources.Culture), new object[] { param0, param1, param2, param3 });
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x0014E6E0 File Offset: 0x0014C8E0
		internal static string Drda_CollectionNullValue(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_CollectionNullValue", Resources.Culture), param0, param1);
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x0014E702 File Offset: 0x0014C902
		internal static string Drda_CollectionInvalidType(object param0, object param1, object param2)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_CollectionInvalidType", Resources.Culture), param0, param1, param2);
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x0014E725 File Offset: 0x0014C925
		internal static string Drda_CollectionUniqueValue(object param0, object param1, object param2)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_CollectionUniqueValue", Resources.Culture), param0, param1, param2);
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x0014E748 File Offset: 0x0014C948
		internal static string Drda_CollectionIsNotParent(object param0, object param1, object param2, object param3)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_CollectionIsNotParent", Resources.Culture), new object[] { param0, param1, param2, param3 });
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0014E77E File Offset: 0x0014C97E
		internal static string Drda_CollectionIsParent(object param0, object param1, object param2, object param3)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_CollectionIsParent", Resources.Culture), new object[] { param0, param1, param2, param3 });
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0014E7B4 File Offset: 0x0014C9B4
		internal static string Drda_CollectionRemoveInvalidObject(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_CollectionRemoveInvalidObject", Resources.Culture), param0, param1);
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x0014E7D6 File Offset: 0x0014C9D6
		internal static string Drda_InvalidDataRowVersion(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidDataRowVersion", Resources.Culture), param0, param1);
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x0014E7F8 File Offset: 0x0014C9F8
		internal static string Drda_InvalidParameterDirection(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidParameterDirection", Resources.Culture), param0, param1);
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x0014E81A File Offset: 0x0014CA1A
		internal static string Drda_InvalidDataType(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidDataType", Resources.Culture), param0);
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0014E83B File Offset: 0x0014CA3B
		internal static string Drda_UnknownDataType(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_UnknownDataType", Resources.Culture), param0, param1);
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x0014E85D File Offset: 0x0014CA5D
		internal static string Drda_UnknownDataTypeCode(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_UnknownDataTypeCode", Resources.Culture), param0);
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x0014E87E File Offset: 0x0014CA7E
		internal static string Drda_InvalidSizeValue(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidSizeValue", Resources.Culture), param0);
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x0014E89F File Offset: 0x0014CA9F
		internal static string Drda_ParallelTransactionsNotSupported(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_ParallelTransactionsNotSupported", Resources.Culture), param0);
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x0014E8C0 File Offset: 0x0014CAC0
		internal static string Drda_ConnectionAlreadyOpen(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_ConnectionAlreadyOpen", Resources.Culture), param0);
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x0014E8E1 File Offset: 0x0014CAE1
		internal static string Drda_OpenConnectionPropertySet(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_OpenConnectionPropertySet", Resources.Culture), param0, param1);
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0014E903 File Offset: 0x0014CB03
		internal static string Drda_InvalidIsolationLevel(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidIsolationLevel", Resources.Culture), param0);
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x0014E924 File Offset: 0x0014CB24
		internal static string Drda_ConnectionStringSyntax(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_ConnectionStringSyntax", Resources.Culture), param0);
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x0014E945 File Offset: 0x0014CB45
		internal static string Drda_InvalidConnectionOptionValue(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidConnectionOptionValue", Resources.Culture), param0);
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0014E966 File Offset: 0x0014CB66
		internal static string Drda_KeywordNotSupported(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_KeywordNotSupported", Resources.Culture), param0);
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x0014E987 File Offset: 0x0014CB87
		internal static string Drda_NotSupported(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_NotSupported", Resources.Culture), param0);
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x0014E9A8 File Offset: 0x0014CBA8
		internal static string Drda_BadData(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_BadData", Resources.Culture), param0, param1);
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0014E9CA File Offset: 0x0014CBCA
		internal static string Drda_InvalidCommandType(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidCommandType", Resources.Culture), param0);
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x0014E9EB File Offset: 0x0014CBEB
		internal static string Drda_InvalidUpdateRowSource(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidUpdateRowSource", Resources.Culture), param0);
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x0014EA0C File Offset: 0x0014CC0C
		internal static string Drda_DataReaderClosed(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_DataReaderClosed", Resources.Culture), param0);
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0014EA2D File Offset: 0x0014CC2D
		internal static string Drda_ConvertFailed(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_ConvertFailed", Resources.Culture), param0, param1);
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x0014EA4F File Offset: 0x0014CC4F
		internal static string Drda_InvalidConnectionOptionLength(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidConnectionOptionLength", Resources.Culture), param0, param1);
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x0014EA71 File Offset: 0x0014CC71
		internal static string Drda_InvalidDrdaType(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidDrdaType", Resources.Culture), param0);
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x0014EA92 File Offset: 0x0014CC92
		internal static string Drda_SchemaBadRestrictionCount(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_SchemaBadRestrictionCount", Resources.Culture), param0, param1);
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x0014EAB4 File Offset: 0x0014CCB4
		internal static string Drda_SchemaBadRestrictionLength(object param0, object param1, object param2)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_SchemaBadRestrictionLength", Resources.Culture), param0, param1, param2);
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x0014EAD7 File Offset: 0x0014CCD7
		internal static string Drda_SchemaBadRestrictionType(object param0, object param1, object param2)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_SchemaBadRestrictionType", Resources.Culture), param0, param1, param2);
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x0014EAFA File Offset: 0x0014CCFA
		internal static string Drda_SchemaNoCollection(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_SchemaNoCollection", Resources.Culture), param0);
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x0014EB1B File Offset: 0x0014CD1B
		internal static string InvalidQualifiedProcName(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("InvalidQualifiedProcName", Resources.Culture), param0);
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x0014EB3C File Offset: 0x0014CD3C
		internal static string InvalidQualifiedProcNameTooManyParts(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("InvalidQualifiedProcNameTooManyParts", Resources.Culture), param0);
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x0014EB5D File Offset: 0x0014CD5D
		internal static string InvalidQualifiedTableName(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("InvalidQualifiedTableName", Resources.Culture), param0);
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x0014EB7E File Offset: 0x0014CD7E
		internal static string InvalidQualifiedTableNameTooManyParts(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("InvalidQualifiedTableNameTooManyParts", Resources.Culture), param0);
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x0014EB9F File Offset: 0x0014CD9F
		internal static string Drda_CommandTimeoutInvalid(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_CommandTimeoutInvalid", Resources.Culture), param0);
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x0014EBC0 File Offset: 0x0014CDC0
		internal static string Drda_InvalidBulkCopyDestinationColumn(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidBulkCopyDestinationColumn", Resources.Culture), param0);
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x0014EBE1 File Offset: 0x0014CDE1
		internal static string Drda_InvalidBulkCopyDestinationOrdinal(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidBulkCopyDestinationOrdinal", Resources.Culture), param0);
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x0014EC02 File Offset: 0x0014CE02
		internal static string Drda_InvalidBulkCopySourceColumn(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidBulkCopySourceColumn", Resources.Culture), param0);
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0014EC23 File Offset: 0x0014CE23
		internal static string Drda_InvalidBulkCopySourceOrdinal(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidBulkCopySourceOrdinal", Resources.Culture), param0);
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x0014EC44 File Offset: 0x0014CE44
		internal static string Drda_InvalidBulkCopyTimeoutValue(object param0)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidBulkCopyTimeoutValue", Resources.Culture), param0);
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x0014EC65 File Offset: 0x0014CE65
		internal static string Drda_InvalidBatchValuesToParameters(object param0, object param1)
		{
			return string.Format(Resources.Culture, Resources.ResourceManager.GetString("Drda_InvalidBatchValuesToParameters", Resources.Culture), param0, param1);
		}

		// Token: 0x04004071 RID: 16497
		private static ResourceManager resourceManager;

		// Token: 0x04004072 RID: 16498
		private static CultureInfo resourceCulture;
	}
}
