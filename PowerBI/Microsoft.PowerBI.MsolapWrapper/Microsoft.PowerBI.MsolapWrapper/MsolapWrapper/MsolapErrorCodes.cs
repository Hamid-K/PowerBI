using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MsolapWrapper
{
	// Token: 0x0200000C RID: 12
	public static class MsolapErrorCodes
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00009F08 File Offset: 0x00009308
		internal static WrapperErrorCodes MapMsolapErrorCode(uint msolapCode, int hresult, string providerMessage, __MIDL_IASErrorInfo_0001 type, out WrapperErrorSource errorSource, out string onPremErrorCode, out WrapperErrorSourceOrigin errorSourceOrigin)
		{
			onPremErrorCode = null;
			if (hresult != -1052442606 && msolapCode != 3242524690U)
			{
				WrapperErrorCodes wrapperErrorCodes;
				WrapperErrorSource wrapperErrorSource;
				if (type != (__MIDL_IASErrorInfo_0001)0)
				{
					if (type != (__MIDL_IASErrorInfo_0001)1)
					{
						if (type != (__MIDL_IASErrorInfo_0001)2)
						{
							if (type != (__MIDL_IASErrorInfo_0001)3)
							{
								wrapperErrorCodes = WrapperErrorCodes.MsolapWrapperError;
								wrapperErrorSource = WrapperErrorSource.Unknown;
							}
							else
							{
								wrapperErrorCodes = WrapperErrorCodes.QueryExternalError;
								wrapperErrorSource = WrapperErrorSource.External;
							}
						}
						else
						{
							wrapperErrorCodes = WrapperErrorCodes.QuerySystemError;
							wrapperErrorSource = WrapperErrorSource.PowerBI;
						}
					}
					else
					{
						wrapperErrorCodes = WrapperErrorCodes.QueryUserError;
						wrapperErrorSource = WrapperErrorSource.User;
					}
				}
				else
				{
					wrapperErrorCodes = WrapperErrorCodes.MsolapWrapperError;
					wrapperErrorSource = WrapperErrorSource.Unknown;
				}
				errorSource = MsolapErrorCodes.DetermineErrorSource(wrapperErrorSource, msolapCode, hresult, out errorSourceOrigin);
				return MsolapErrorCodes.DetermineErrorCode(wrapperErrorSource, wrapperErrorCodes, msolapCode, hresult);
			}
			errorSource = OnPremiseServiceErrorExtractor.ExtractSource(providerMessage, out onPremErrorCode, out errorSourceOrigin);
			return WrapperErrorCodes.OnPremiseServiceException;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00009F80 File Offset: 0x00009380
		public static WrapperErrorCodes MapMsolapErrorCode(uint msolapCode, int hresult, string providerMessage, ASErrorType asErrorType, out WrapperErrorSource errorSource, out string onPremErrorCode, out WrapperErrorSourceOrigin errorSourceOrigin)
		{
			__MIDL_IASErrorInfo_0001 _MIDL_IASErrorInfo_ = (__MIDL_IASErrorInfo_0001)0;
			if (asErrorType != ASErrorType.ASErrorUnknown)
			{
				if (asErrorType != ASErrorType.ASErrorUser)
				{
					if (asErrorType != ASErrorType.ASErrorSystem)
					{
						if (asErrorType == ASErrorType.ASErrorExternal)
						{
							_MIDL_IASErrorInfo_ = (__MIDL_IASErrorInfo_0001)3;
						}
					}
					else
					{
						_MIDL_IASErrorInfo_ = (__MIDL_IASErrorInfo_0001)2;
					}
				}
				else
				{
					_MIDL_IASErrorInfo_ = (__MIDL_IASErrorInfo_0001)1;
				}
			}
			else
			{
				_MIDL_IASErrorInfo_ = (__MIDL_IASErrorInfo_0001)0;
			}
			return MsolapErrorCodes.MapMsolapErrorCode(msolapCode, hresult, providerMessage, _MIDL_IASErrorInfo_, out errorSource, out onPremErrorCode, out errorSourceOrigin);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009648 File Offset: 0x00008A48
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool IsUserSafeError(WrapperErrorSource asErrorSource, WrapperErrorCodes wrapperCode, uint msolapCode)
		{
			return (asErrorSource == WrapperErrorSource.User || !MsolapErrorCodes.SystemErrorCodes.Contains(msolapCode)) && !MsolapErrorCodes.UserUnsafeErrorCodes.Contains(msolapCode) && wrapperCode != WrapperErrorCodes.OnPremiseServiceException && wrapperCode != WrapperErrorCodes.MsolapWrapperError;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000956C File Offset: 0x0000896C
		internal static WrapperErrorSource MapASErrorSource(__MIDL_IASErrorInfo_0001 type, out WrapperErrorCodes wrapperCode)
		{
			if (type == (__MIDL_IASErrorInfo_0001)0)
			{
				wrapperCode = WrapperErrorCodes.MsolapWrapperError;
				return WrapperErrorSource.Unknown;
			}
			if (type == (__MIDL_IASErrorInfo_0001)1)
			{
				wrapperCode = WrapperErrorCodes.QueryUserError;
				return WrapperErrorSource.User;
			}
			if (type == (__MIDL_IASErrorInfo_0001)2)
			{
				wrapperCode = WrapperErrorCodes.QuerySystemError;
				return WrapperErrorSource.PowerBI;
			}
			if (type != (__MIDL_IASErrorInfo_0001)3)
			{
				wrapperCode = WrapperErrorCodes.MsolapWrapperError;
				return WrapperErrorSource.Unknown;
			}
			wrapperCode = WrapperErrorCodes.QueryExternalError;
			return WrapperErrorSource.External;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000095A8 File Offset: 0x000089A8
		internal static WrapperErrorCodes DetermineErrorCode(WrapperErrorSource asErrorSource, WrapperErrorCodes asErrorCode, uint msolapCode, int hresult)
		{
			WrapperErrorCodes wrapperErrorCodes = WrapperErrorCodes.MsolapWrapperError;
			if (MsolapErrorCodes.ErrorCodeMapping.TryGetValue(msolapCode, out wrapperErrorCodes))
			{
				return wrapperErrorCodes;
			}
			if (hresult == -2147217842)
			{
				return WrapperErrorCodes.OperationCancelled;
			}
			if (asErrorSource != WrapperErrorSource.Unknown)
			{
				return asErrorCode;
			}
			if (msolapCode == 0U)
			{
				return WrapperErrorCodes.MsolapWrapperError;
			}
			return MsolapErrorCodes.SystemErrorCodes.Contains(msolapCode) ? WrapperErrorCodes.QuerySystemError : WrapperErrorCodes.QueryUserError;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000095F4 File Offset: 0x000089F4
		internal static WrapperErrorSource DetermineErrorSource(WrapperErrorSource asErrorSource, uint msolapCode, int hresult, out WrapperErrorSourceOrigin errorSourceOrigin)
		{
			if (asErrorSource != WrapperErrorSource.Unknown)
			{
				errorSourceOrigin = WrapperErrorSourceOrigin.AS;
				return asErrorSource;
			}
			errorSourceOrigin = WrapperErrorSourceOrigin.MsolapWrapper;
			if (hresult == -2147217842)
			{
				return WrapperErrorSource.User;
			}
			if (msolapCode == 0U)
			{
				return WrapperErrorSource.PowerBI;
			}
			if (MsolapErrorCodes.SystemErrorCodes.Contains(msolapCode))
			{
				return WrapperErrorSource.PowerBI;
			}
			WrapperErrorSource wrapperErrorSource = WrapperErrorSource.Unknown;
			wrapperErrorSource = ((!MsolapErrorCodes.ErrorSourceMapping.TryGetValue(msolapCode, out wrapperErrorSource)) ? WrapperErrorSource.User : wrapperErrorSource);
			return wrapperErrorSource;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000968C File Offset: 0x00008A8C
		private static Dictionary<uint, WrapperErrorCodes> InitializeErrorCodeMapping()
		{
			return new Dictionary<uint, WrapperErrorCodes>
			{
				{
					16777216U,
					WrapperErrorCodes.QueryTimeoutExceeded
				},
				{
					3238789169U,
					WrapperErrorCodes.QueryTimeoutExceeded
				},
				{
					3242065922U,
					WrapperErrorCodes.QueryMemoryLimitExceeded
				},
				{
					3242065924U,
					WrapperErrorCodes.QueryMemoryLimitExceeded
				},
				{
					3242524690U,
					WrapperErrorCodes.OnPremiseServiceException
				},
				{
					3239837706U,
					WrapperErrorCodes.OperationCancelled
				},
				{
					3241804044U,
					WrapperErrorCodes.ExclusivePercentileOutOfRange
				},
				{
					3241804195U,
					WrapperErrorCodes.ProxyModelChainLimitExceeded
				}
			};
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00009708 File Offset: 0x00008B08
		private static Dictionary<uint, WrapperErrorSource> InitializeErrorSourceMapping()
		{
			return new Dictionary<uint, WrapperErrorSource>
			{
				{
					3239837706U,
					WrapperErrorSource.User
				},
				{
					3241804044U,
					WrapperErrorSource.User
				},
				{
					3241804195U,
					WrapperErrorSource.User
				},
				{
					16777216U,
					WrapperErrorSource.User
				},
				{
					3238789169U,
					WrapperErrorSource.User
				},
				{
					3242065922U,
					WrapperErrorSource.User
				},
				{
					3242065924U,
					WrapperErrorSource.User
				}
			};
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00009778 File Offset: 0x00008B78
		private static HashSet<uint> InitializeSystemErrorCodes()
		{
			return new HashSet<uint>
			{
				3238002695U, 3238461440U, 3238002710U, 3238002706U, 3238002688U, 3239903234U, 3239182517U, 3242524673U, 3239837705U, 3239837701U,
				3239968848U, 3238854665U, 3238068224U, 3238854657U, 3238854662U, 3238854658U, 3242262676U, 3239313827U, 3241541860U, 3242262614U,
				3239968891U, 3239182362U, 3239182364U, 3239182363U, 3241804119U
			};
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000098C0 File Offset: 0x00008CC0
		private static HashSet<uint> InitializeUserUnsafeErrorCodes()
		{
			return new HashSet<uint>
			{
				3240689665U, 3238395904U, 3238068259U, 3239575553U, 3239575554U, 3239575555U, 3239575556U, 3239575557U, 3239575558U, 3239575559U,
				3239575560U, 3239575561U, 3239575562U, 3239575563U, 3239575564U, 3239575565U, 3239575566U, 3239575567U, 3239575568U, 3239575569U,
				3239575570U, 3239575571U, 3239575572U, 3239575573U, 3239575574U, 3239575575U, 3239575578U, 3239575585U, 3239575588U, 3239575590U,
				3239575603U, 3239575648U, 3239575669U, 3239575670U, 3239575671U, 3239575677U, 3239575683U, 3239575685U, 3239444525U, 3239444704U,
				3239444784U, 3239444827U, 3240230992U, 3241345025U, 3241345030U
			};
		}

		// Token: 0x04000092 RID: 146
		internal static uint NO_ERROR_CODE = 0U;

		// Token: 0x04000093 RID: 147
		internal static uint QUERY_MEMORY_LIMIT_EXCEEDED = 3242065922U;

		// Token: 0x04000094 RID: 148
		internal static uint PBIDEDICATED_QUERY_MEMORY_LIMIT_EXCEEDED = 3242065924U;

		// Token: 0x04000095 RID: 149
		internal static uint TIMEOUT = 16777216U;

		// Token: 0x04000096 RID: 150
		internal static uint REQUEST_TIMEOUT = 3238789169U;

		// Token: 0x04000097 RID: 151
		internal static uint ON_PREMISE_SERVICE_EXCEPTION = 3242524690U;

		// Token: 0x04000098 RID: 152
		internal static uint OPERATION_CANCELLED_BY_USER = 3239837706U;

		// Token: 0x04000099 RID: 153
		internal static uint EXCLUSIVE_PERCENTILE_OUT_OF_RANGE = 3241804044U;

		// Token: 0x0400009A RID: 154
		internal static uint COLUMN_NOT_FOUND = 3241803780U;

		// Token: 0x0400009B RID: 155
		internal static uint TABLE_NOT_FOUND = 3241803779U;

		// Token: 0x0400009C RID: 156
		internal static uint NON_CONTIGUOUS_DATE_SELECTION = 3241803816U;

		// Token: 0x0400009D RID: 157
		internal static uint PROXY_MODEL_CHAIN_LIMIT_EXCEEDED = 3241804195U;

		// Token: 0x0400009E RID: 158
		internal static uint INTERNAL_SUPPORTING_STRUCTURES_NOT_PROCESSED = 3241541800U;

		// Token: 0x0400009F RID: 159
		internal static uint PFE_ABORT = 3238002695U;

		// Token: 0x040000A0 RID: 160
		internal static uint PFE_COM = 3238461440U;

		// Token: 0x040000A1 RID: 161
		internal static uint PFE_CRASH = 3238002710U;

		// Token: 0x040000A2 RID: 162
		internal static uint PFE_ERROR_IN_ERROR_HANDLING = 3238002706U;

		// Token: 0x040000A3 RID: 163
		internal static uint PFE_INTERNAL = 3238002688U;

		// Token: 0x040000A4 RID: 164
		internal static uint PFE_LOCK_ABORTED = 3239903234U;

		// Token: 0x040000A5 RID: 165
		internal static uint PFE_NLP_UNEXPECTED_FAILURE = 3239182517U;

		// Token: 0x040000A6 RID: 166
		internal static uint PFE_ON_PREMISE_INTERNAL_ERROR = 3242524673U;

		// Token: 0x040000A7 RID: 167
		internal static uint PFE_SERVERBASE_OPERATION_CANCELLED_DUE_TO_LOCKING_CONFLICTS = 3239837705U;

		// Token: 0x040000A8 RID: 168
		internal static uint PFE_SERVERBASE_OPERATION_CANCELLED_DUE_TO_MEMORY_PRESSURE = 3239837701U;

		// Token: 0x040000A9 RID: 169
		internal static uint PFE_BACKUP_STORAGE_FILE_NOTFOUND = 3239968848U;

		// Token: 0x040000AA RID: 170
		internal static uint PFE_NETWORK_TRANSPORT = 3238854665U;

		// Token: 0x040000AB RID: 171
		internal static uint PFE_SYSTEM = 3238068224U;

		// Token: 0x040000AC RID: 172
		internal static uint PFE_TCP_CONN_CLOSED = 3238854657U;

		// Token: 0x040000AD RID: 173
		internal static uint PFE_TCP_CONN_LOST = 3238854662U;

		// Token: 0x040000AE RID: 174
		internal static uint PFE_TCP_TIMEOUT = 3238854658U;

		// Token: 0x040000AF RID: 175
		internal static uint PFE_TM_DATABASE_CORRUPTED = 3242262676U;

		// Token: 0x040000B0 RID: 176
		internal static uint PFE_METADATA_SESS_NOTFOUND = 3239313827U;

		// Token: 0x040000B1 RID: 177
		internal static uint PFE_XM_DBCC_STRINGSTORE_CORRUPT = 3241541860U;

		// Token: 0x040000B2 RID: 178
		internal static uint PFE_TM_CORRUPT_DB_INVALID_OBJECT_ID = 3242262614U;

		// Token: 0x040000B3 RID: 179
		internal static uint PFE_BACKUP_STORAGE_HEADER_SIGNATURE_UNKNOWN = 3239968891U;

		// Token: 0x040000B4 RID: 180
		internal static uint PFE_MANAGED_READER_UNEXPECTED = 3239182362U;

		// Token: 0x040000B5 RID: 181
		internal static uint PFE_MANAGED_CMD_UNEXPECTED = 3239182364U;

		// Token: 0x040000B6 RID: 182
		internal static uint PFE_MANAGED_CONN_UNEXPECTED = 3239182363U;

		// Token: 0x040000B7 RID: 183
		internal static uint PFE_XL_EXTENSION_UNEXPECTED_FAILURE = 3241804119U;

		// Token: 0x040000B8 RID: 184
		internal static uint PFE_EXTERNAL = 3240689665U;

		// Token: 0x040000B9 RID: 185
		internal static uint PFE_OLEDB = 3238395904U;

		// Token: 0x040000BA RID: 186
		internal static uint PFE_RESTRICTROLES_ROLES_INTERSECTION_EMPTY = 3238068259U;

		// Token: 0x040000BB RID: 187
		internal static uint PFE_SEC_DB_ACCESS = 3239575553U;

		// Token: 0x040000BC RID: 188
		internal static uint PFE_SEC_PERM_ALTER = 3239575554U;

		// Token: 0x040000BD RID: 189
		internal static uint PFE_SEC_PERM_READ = 3239575555U;

		// Token: 0x040000BE RID: 190
		internal static uint PFE_SEC_PERM_PROCESS = 3239575556U;

		// Token: 0x040000BF RID: 191
		internal static uint PFE_SEC_PERM_DROP = 3239575557U;

		// Token: 0x040000C0 RID: 192
		internal static uint PFE_SEC_PERM_CREATE = 3239575558U;

		// Token: 0x040000C1 RID: 193
		internal static uint PFE_SEC_PERM_RETRIEVE = 3239575559U;

		// Token: 0x040000C2 RID: 194
		internal static uint PFE_SEC_PERM_DUPROLE = 3239575560U;

		// Token: 0x040000C3 RID: 195
		internal static uint PFE_SEC_PERM_NOROLE = 3239575561U;

		// Token: 0x040000C4 RID: 196
		internal static uint PFE_SEC_PERM_RESTORE = 3239575562U;

		// Token: 0x040000C5 RID: 197
		internal static uint PFE_SEC_PERM_BACKUP = 3239575563U;

		// Token: 0x040000C6 RID: 198
		internal static uint PFE_SEC_PERM_MERGE = 3239575564U;

		// Token: 0x040000C7 RID: 199
		internal static uint PFE_SEC_PERM_ROLECHANGED = 3239575565U;

		// Token: 0x040000C8 RID: 200
		internal static uint PFE_SEC_PERM_SUBSCRIBE = 3239575566U;

		// Token: 0x040000C9 RID: 201
		internal static uint PFE_SEC_PERM_WRITEBACK = 3239575567U;

		// Token: 0x040000CA RID: 202
		internal static uint PFE_SEC_PERM_SYNCHRONIZE = 3239575568U;

		// Token: 0x040000CB RID: 203
		internal static uint PFE_SEC_PERM_METADATA = 3239575569U;

		// Token: 0x040000CC RID: 204
		internal static uint PFE_SEC_PERM_DRILLTHRU = 3239575570U;

		// Token: 0x040000CD RID: 205
		internal static uint PFE_SEC_PERM_NODATABASE = 3239575571U;

		// Token: 0x040000CE RID: 206
		internal static uint PFE_SEC_PERM_READWRITE = 3239575572U;

		// Token: 0x040000CF RID: 207
		internal static uint PFE_SEC_PERM_CANCEL = 3239575573U;

		// Token: 0x040000D0 RID: 208
		internal static uint PFE_SEC_PERM_DISCOVER = 3239575574U;

		// Token: 0x040000D1 RID: 209
		internal static uint PFE_SEC_PERM_BROWSE = 3239575575U;

		// Token: 0x040000D2 RID: 210
		internal static uint PFE_SEC_SESS_OBJ_DATASOURCE = 3239575578U;

		// Token: 0x040000D3 RID: 211
		internal static uint PFE_SEC_PERM_CLEARCACHE = 3239575585U;

		// Token: 0x040000D4 RID: 212
		internal static uint PFE_SEC_PERM_LOCK = 3239575588U;

		// Token: 0x040000D5 RID: 213
		internal static uint PFE_SEC_INVALID_EFFECTIVE_USERNAME = 3239575590U;

		// Token: 0x040000D6 RID: 214
		internal static uint PFE_SEC_PERM_DRILLTHRU_PARENTSTRUCTURE = 3239575603U;

		// Token: 0x040000D7 RID: 215
		internal static uint PFE_SEC_PERM_CLONE_DATABASE = 3239575648U;

		// Token: 0x040000D8 RID: 216
		internal static uint PFE_SEC_PERM_TM_DDL_MODIFY = 3239575669U;

		// Token: 0x040000D9 RID: 217
		internal static uint PFE_SEC_PERM_TM_DDL_DISCOVER = 3239575670U;

		// Token: 0x040000DA RID: 218
		internal static uint PFE_SEC_PERM_TM_REFRESH = 3239575671U;

		// Token: 0x040000DB RID: 219
		internal static uint PFE_SEC_CLAIMS_TOKEN_AUTH_LOCATION_IS_NOT_SUB_DOMAIN_OF_RESOURCE_ID = 3239575677U;

		// Token: 0x040000DC RID: 220
		internal static uint PFE_CLUSTER_RESOLUTION_TCP_URL = 3239575683U;

		// Token: 0x040000DD RID: 221
		internal static uint PFE_SEC_CLAIMS_TOKEN_AUTH_DATASOURCE_IS_NOT_TRUSTED = 3239575685U;

		// Token: 0x040000DE RID: 222
		internal static uint PFE_DM_DMX_MODEL_NOT_FOUND = 3239444525U;

		// Token: 0x040000DF RID: 223
		internal static uint PFE_DM_DMX_STRUCTURE_NOT_FOUND = 3239444704U;

		// Token: 0x040000E0 RID: 224
		internal static uint PFE_DM_DMX_OBJECT_NOT_FOUND = 3239444784U;

		// Token: 0x040000E1 RID: 225
		internal static uint PFE_DM_DMX_MODELSTRUCTURE_NOT_FOUND = 3239444827U;

		// Token: 0x040000E2 RID: 226
		internal static uint PFE_SQL_IMBI_INVALID_TABLE = 3240230992U;

		// Token: 0x040000E3 RID: 227
		internal static uint PFE_SEC_PERM_DETACH = 3241345025U;

		// Token: 0x040000E4 RID: 228
		internal static uint PFE_SEC_PERM_ATTACH = 3241345030U;

		// Token: 0x040000E5 RID: 229
		private static IReadOnlyDictionary<uint, WrapperErrorCodes> ErrorCodeMapping = MsolapErrorCodes.InitializeErrorCodeMapping();

		// Token: 0x040000E6 RID: 230
		private static IReadOnlyDictionary<uint, WrapperErrorSource> ErrorSourceMapping = MsolapErrorCodes.InitializeErrorSourceMapping();

		// Token: 0x040000E7 RID: 231
		private static HashSet<uint> SystemErrorCodes = MsolapErrorCodes.InitializeSystemErrorCodes();

		// Token: 0x040000E8 RID: 232
		private static HashSet<uint> UserUnsafeErrorCodes = MsolapErrorCodes.InitializeUserUnsafeErrorCodes();
	}
}
